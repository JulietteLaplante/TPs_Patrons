using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace StockManager
{
    class StockService
    {
        Dictionary<string, StockItem> stock;


        static void Main(string[] args)
        {

            StockService s = new StockService();
            s.AwaitAndProcessRPC();
        }

        StockService()
        {
            stock = new Dictionary<string, StockItem>();
            InitStock();
        }

        private void InitStock()
        {
            // load stock from json
            string[] files = Directory.GetFiles("stock", "*stock.json");

            foreach (string file in files)
            {
                Console.WriteLine("Fetch stock from the file " + file + ".");
                StockItem[] items = JsonSerializer.Deserialize<StockItem[]>(File.ReadAllText(file));
                foreach (StockItem item in items )
                {
                    stock.Add(item.name, item);
                    Console.WriteLine(item.quantity + " " + item.name + "(s) added to stock.");
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

                channel.BasicConsume(queue: "stock_queue", autoAck: true, consumer: consumer);

                Console.WriteLine("\nAwaiting RPC requests");

                consumer.Received += (sender, e) =>
                {
                    var orderBytes = e.Body;
                    string response = null;
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
                        channel.BasicPublish(exchange: "", routingKey: e.BasicProperties.ReplyTo, basicProperties: channel.CreateBasicProperties(), body: responseBytes);
                    }

                };

                Console.WriteLine("Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        // expect the request to look like "get|put|info:productName(:quantity)"
        // "get:apple:12"  will try to remove 12 apple from the stock (to be put in the user's cart for example) 
        // and return the individual price of the item
        // "put:apple:5" will add 5 apples to the stock and return the individual price
        // "info:peer" will return "price:quantityAvailable"
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
                    return stock[itemRequested].price.ToString();
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
                return stock[itemRequested].price +":"+ stock[itemRequested].quantity;
            }
            else
            {
                throw new Exception("Request Type not recognized.");
            }
        }
    }
}
