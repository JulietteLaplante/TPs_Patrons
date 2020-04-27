using System;
using StockSDK;

namespace E_Commerce
{
    class Program
    {
        static void Main(string[] args)
        {

            StockManager sm = new StockManager();
            sm.ReserveItem(2, "peer");

            
            Console.WriteLine("Press the \"Enter\" key to exit.");
            Console.ReadLine();

        }
    }
}
