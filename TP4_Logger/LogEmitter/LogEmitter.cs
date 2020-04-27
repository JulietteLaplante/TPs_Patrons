using System;
using RabbitMQ.Client;
using System.Text;
using System.Timers;

namespace LogEmitter
{
    class Program
    {
        static void Main(string[] args)
        {

            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "directLogs", type: "direct");

            Timer timer = new Timer();
            timer.Elapsed += (sender, e) => createLog(channel);
            timer.Interval = 1000; // in miliseconds
            timer.Start();

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

            connection.Close();
        }

        static void createLog(IModel channel)
        {
            string level;

            //création du log
            //tirage du log aléatoirement
            var rand = new Random();
            string[] differentLevel = { "Info", "Warning", "Error", "Critical" };
            int index = rand.Next(differentLevel.Length);
            level = differentLevel[index];
            //création du message
            string message = "Hello " + level;
            var body = Encoding.UTF8.GetBytes(message);

            //envoi du log
            channel.BasicPublish(exchange: "directLogs", routingKey: level, basicProperties: null, body: body);
            Console.WriteLine(" [x] Sent {0}", message);
        }

    }
}
