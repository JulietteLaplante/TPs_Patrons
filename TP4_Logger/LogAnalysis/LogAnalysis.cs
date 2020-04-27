using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Timers;
using System.Collections.Generic;

namespace LogAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            //initialisation du dictionnaire
            Dictionary<string, int> rapport = new Dictionary<string, int>();
            rapport["Info"] = 0;
            rapport["Warning"] = 0;
            rapport["Error"] = 0;
            rapport["Critical"] = 0;


            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //parametrisation de la connection
                channel.ExchangeDeclare(exchange: "directLogs", type: "direct");

                channel.QueueDeclare(queue: "LogAnalysis", exclusive: true);

                channel.QueueBind(queue: "LogAnalysis", exchange: "directLogs", routingKey: "Info");
                channel.QueueBind(queue: "LogAnalysis", exchange: "directLogs", routingKey: "Warning");
                channel.QueueBind(queue: "LogAnalysis", exchange: "directLogs", routingKey: "Error");
                channel.QueueBind(queue: "LogAnalysis", exchange: "directLogs", routingKey: "Critical");

                //lecture des evenements
                Console.WriteLine(" [*] Waiting for messages.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    rapport[ea.RoutingKey.ToString()] += 1;
                };
                channel.BasicConsume(queue: "LogAnalysis", autoAck: true, consumer: consumer);

                Timer timer = new Timer();
                timer.Elapsed += (sender, e) => printRapport(rapport);
                timer.Interval = 10000; // in miliseconds
                timer.Start();

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }

            static void printRapport(Dictionary<string, int> rapport)
            {
                foreach (KeyValuePair<string, int> entry in rapport)
                {
                    Console.WriteLine(entry.Key + " : " + entry.Value);
                }
            }
        }
    }
}
