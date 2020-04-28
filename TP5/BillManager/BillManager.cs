using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using StockSDK;
using UserSDK;
using BillSDK;
using System.Text.Json;
using static BillSDK.Bill;
using System.Collections.Generic;

namespace BillManager
{
    class BillManager
    {
        BillManager()
        {

        }
        static void Main(string[] args)
        {
            BillManager bm = new BillManager();
            bm.AwaitAndProcessRPC();
        }

        void AwaitAndProcessRPC()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "bill_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
                var consumer = new EventingBasicConsumer(channel);
                channel.BasicQos(0, 1, false);
                channel.BasicConsume(queue: "bill_queue", autoAck: false, consumer: consumer);

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

        private string ProcessRequest(string request)
        {
            // 5 et 9.975
            try
            {
                float sum = 0f;
                List<ItemLine> itemLines = JsonSerializer.Deserialize<List<ItemLine>>(request);
                foreach (ItemLine itemLine in itemLines)
                {
                    sum += itemLine.quantity * itemLine.item.price;
                }
                float sumWithTaxes = sum * (1f + 0.05f + 0.0975f);
                return sum + ":" + sumWithTaxes.ToString();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }
    }
}
