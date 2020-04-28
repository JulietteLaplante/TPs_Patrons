using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using UserSDK;

namespace UserManager
{
    class UserManager
    {
        Dictionary<string, User> users;
        UserManager()
        {
            users = new Dictionary<string, User>();
            InitUsers();
        }
        static void Main(string[] args)
        {
            UserManager um = new UserManager();
            um.AwaitAndProcessRPC();
        }

        private void InitUsers()
        {
            // load stock from json
            string[] files = Directory.GetFiles("users", "*.json");

            foreach (string file in files)
            {
                Console.WriteLine("Fetch stock from the file " + file + ".");
                User[] users = JsonSerializer.Deserialize<User[]>(File.ReadAllText(file));
                foreach (User user in users)
                {
                    this.users.Add(user.username, user);
                    Console.WriteLine("User " + user.username+ " added to user base.");
                }

            }
        }

        // Could be in a library, same code in the 3 services
        void AwaitAndProcessRPC()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "user_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
                var consumer = new EventingBasicConsumer(channel);
                channel.BasicQos(0, 1, false);
                channel.BasicConsume(queue: "user_queue", autoAck: false, consumer: consumer);

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
        // "info:peer" will return "price:quantityAvailable"
        string ProcessRequest(string request)
        {
            try
            {
                return JsonSerializer.Serialize(users[request]);
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }
    }
}
