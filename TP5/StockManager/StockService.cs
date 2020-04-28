using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StockSDK;

namespace StockManagerService
{
    class StockService
    {
        Dictionary<string, ItemLine> stock;


        static void Main(string[] args)
        {

            StockService s = new StockService();
            s.AwaitAndProcessRPC();
        }

        StockService()
        {
            stock = new Dictionary<string, ItemLine>();
            InitStock();
        }

        private void InitStock()
        {
            // load stock from json
            string[] files = Directory.GetFiles("stock", "*stock.json");

            foreach (string file in files)
            {
                Console.WriteLine("Fetch stock from the file " + file + ".");
                ItemLine[] itemLines = JsonSerializer.Deserialize<ItemLine[]>(File.ReadAllText(file));
                foreach (ItemLine itemLine in itemLines)
                {
                    stock.Add(itemLine.item.name, itemLine);
                    Console.WriteLine(itemLine.quantity + " " + itemLine.item.name + "(s) added to stock.");
                }

            }
        }

         void AwaitAndProcessRPC ()
         {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "stock_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
                var consumer = new EventingBasicConsumer(channel);
                channel.BasicQos(0, 1, false);
                channel.BasicConsume(queue: "stock_queue", autoAck: false, consumer: consumer);

                Console.WriteLine("\nAwaiting RPC requests");

                consumer.Received += (sender, e) =>
                {
                    Console.WriteLine("Request received.");
                    var orderBytes = e.Body;
                    string response = null;
                    var props = e.BasicProperties;
                    var replyProps = channel.CreateBasicProperties();
                    replyProps.CorrelationId = props.CorrelationId;
                    try
                    {
                        string request = Encoding.UTF8.GetString(orderBytes.ToArray());
                        response = ProcessRequest(request);

                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                        response = "";
                    }
                    finally
                    {
                        var responseBytes = Encoding.UTF8.GetBytes(response);
                        channel.BasicPublish(exchange: "", routingKey: props.ReplyTo, basicProperties: replyProps, body: responseBytes);
                        channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
                        Console.WriteLine("Response sent.");

                    }

                };

                Console.WriteLine("Press [enter] to exit...");
                Console.ReadLine();
            }
        }

        // expect the request to look like "get|put|info:productName(:quantity)"
        // "get:apple:12"  will try to remove 12 apple from the stock (to be put in the user's cart for example) 
        // and return the individual price of the item
        // "put:apple:5" will add 5 apples to the stock and return the individual price
        // "info" will return the stock (if one want to know all available products their price and the remaning quantity for example)
        string ProcessRequest (string request)
        {
            string[] orderParts = request.Split(':');
            string requestType = orderParts[0];
            string itemRequested = orderParts[1];
            if (requestType == "get")
            {
                int quantityRequested = int.Parse(orderParts[2]);
                // we remove from the stock the quantity requested
                // if there is enought we proceed
                if ((stock[itemRequested].quantity -= quantityRequested) >= 0)
                {
                    return stock[itemRequested].item.price.ToString();
                }
                // else we refuse the request
                else
                {
                    throw new Exception("Not enought items available.");
                }
            } else if (requestType == "put")
            {
                int quantityRequested = int.Parse(orderParts[2]);
                stock[itemRequested].quantity += quantityRequested;
                return stock[itemRequested].quantity.ToString();
            } else if (requestType == "info")
            {
                return JsonSerializer.Serialize(stock);
            }
            else
            {
                throw new Exception("Request Type not recognized.");
            }
        }
    }
}
