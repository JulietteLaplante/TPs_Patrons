using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace StockManager
{
    class StockManager
    {
        Dictionary<string, ItemLine> stock;


        StockManager()
        {
            stock = new Dictionary<string, ItemLine>();
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
        static void Main(string[] args)
        {

            new StockManager();

            //// Await RPC calls

            //var factory = new ConnectionFactory() { HostName = "localhost" };
            //using (var connection = factory.CreateConnection())
            //using (var channel = connection.CreateModel())
            //{
            //    channel.QueueDeclare(queue: "stock_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
            //    var consumer = new EventingBasicConsumer(channel);

            //    channel.BasicConsume(queue: "stock_queue", autoAck: true, consumer: consumer);

            //    Console.WriteLine("\nAwaiting RPC requests");

            //    consumer.Received += (sender, e) =>
            //    {
            //        var orderBytes = e.Body;
            //        string response = null;
            //        try
            //        {
            //            string request = Encoding.UTF8.GetString(orderBytes.ToArray());
            //            response = ProcessRequest(stock, request);

            //        }
            //        catch (Exception exception)
            //        {
            //            Console.WriteLine(exception.Message);
            //            response = "";
            //        }
            //        finally
            //        {
            //            var responseBytes = Encoding.UTF8.GetBytes(response);
            //            channel.BasicPublish(exchange: "", routingKey: e.BasicProperties.ReplyTo, basicProperties: channel.CreateBasicProperties(), body: responseBytes);
            //        }

            //    };

            //    Console.WriteLine("Press [enter] to exit.");
            //    Console.ReadLine();
            //}



        }

        /*static string ProcessRequest (Dictionary<string, Item> stock, string request)
        {
            // expect the request to look like "get|put|info:productName:quantity"
            // "get:apple:12"  will try to remove 12 apple from the stock (to be put in the user's cart for example) 
            // and return the quantity that has actually been removed
            // "put:apple:5" will add 5 apples to the stock and return the new quantity
            // "info:peer" will return "price:quantityAvailable"
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
                    return quantityRequested.ToString();
                }
                // else we return the maximum quantity
                else
                {
                    stock[itemRequested].quantity += quantityRequested;
                    return stock[itemRequested].quantity.ToString();
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
        }*/


        public ItemLine ReserveItem(int quantity, string name)
        {
            return null;
        }
    }
}
