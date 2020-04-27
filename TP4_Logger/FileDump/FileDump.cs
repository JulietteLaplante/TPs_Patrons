using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.IO;

namespace FileDump
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamWriter file = new System.IO.StreamWriter(@"FileDump.log", true);
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "directLogs", type: "direct");

                channel.QueueDeclare(queue: "FileDump", exclusive: true);

                channel.QueueBind(queue: "FileDump", exchange: "directLogs", routingKey: "Warning");
                channel.QueueBind(queue: "FileDump", exchange: "directLogs", routingKey: "Error");
                channel.QueueBind(queue: "FileDump", exchange: "directLogs", routingKey: "Critical");

                Console.WriteLine(" [*] Waiting for messages.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    Console.WriteLine(" [x] Received {0}", message);

                    //ecrire dans le fichier
                    file.WriteLine("[" + DateTime.Now.ToString() + "]" + " [" + ea.RoutingKey.ToString() + "] " + message);
                    file.Flush();


                };
                channel.BasicConsume(queue: "FileDump", autoAck: true, consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
            file.Close();
        }
    }
}
