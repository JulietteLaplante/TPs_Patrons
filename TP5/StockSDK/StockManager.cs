using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RPCSDK;

namespace StockSDK
{
    public class StockManager
    {

        public StockManager()
        {


        }


        public ItemLine ReserveItem(int quantity, string name)
        {
            var rpcClient = new RpcClient();

            Console.WriteLine(" Trying to reserve" + quantity + " " + name + "(s) ...");
            string request = "get:" + name + ":" + quantity;
            var response = rpcClient.Call(request, "stock_queue");

            rpcClient.Close();

            //if the request wasn't processed correctly
            if (response == "")
                return null;

            Console.WriteLine(" Sucess.\n", response);
            
            return new ItemLine(name, float.Parse(response), quantity);
        }

        public void ReleaseItem(ItemLine itemline)
        {
            var rpcClient = new RpcClient();

            Console.WriteLine(" Releasing " + itemline.quantity + " " + itemline.item.name + "(s) ...");
            string request = "put:" + itemline.item.name + ":" + itemline.quantity;
            var response = rpcClient.Call(request, "stock_queue");

            rpcClient.Close();

            //if the request wasn't processed correctly
            if (response == "")
                Console.WriteLine(" Something went wrong...\n", response);

            Console.WriteLine(" Done.\n", response);
        }

        public void PrintCatalog()
        {
            var rpcClient = new RpcClient();

            string request = "info:all";
            var response = rpcClient.Call(request, "stock_queue");

            rpcClient.Close();

            //if the request wasn't processed correctly
            if (response == "")
                Console.WriteLine(" Something went wrong whilke getting the catalog...\n", response);

            try
            {
                Console.WriteLine("Name \t Quantity \t Price");
                ItemLine[] stock = JsonSerializer.Deserialize<ItemLine[]>(response);
                foreach (ItemLine itemLine in stock)
                {
                    Console.WriteLine(itemLine.item.name + "\t" + itemLine.quantity + "\t" + itemLine.item.price);
                }
                Console.WriteLine("\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
