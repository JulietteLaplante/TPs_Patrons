using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RPCSDK;

namespace StockManager
{
    class StockManager
    {

        public StockManager()
        {


        }


        public ItemLine ReserveItem(int quantity, string name)
        {
            var rpcClient = new RpcClient();

            Console.WriteLine(" [x] Reserving " + quantity + " " + name + "(s)");
            string request = "get:" + name + ":" + quantity;
            var response = rpcClient.Call(request, "stock_queue");

            Console.WriteLine(" [.] Got '{0}'", response);
            rpcClient.Close();

            //if the request wasn't processed correctly
            if (response == "")
                return null;

            
            return new ItemLine(name, float.Parse(response), quantity);
        }

        public void ReleaseItem(ItemLine itemline)
        {
        }
    }
}
