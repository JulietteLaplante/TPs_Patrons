using System;
using System.Collections.Generic;
using StockSDK;
using UserSDK;
using BillSDK;

namespace E_Commerce
{
    class Ecom
    {
        List<ItemLine> cart;
        User user;
        StockManager sm;
        Ecom()
        {
            cart = new List<ItemLine>();
            sm = new StockManager();
        }
        static void Main(string[] args)
        {
            Ecom ecom = new Ecom();
            // ecom.Authenticate();
            ecom.PrintMenu();
            ecom.sm.PrintCatalog();
            ConsoleKeyInfo k = Console.ReadKey();
            while (k.Key != ConsoleKey.E)
            {
                // if()
            }


        }

        void Authenticate()
        {
            Console.WriteLine("Enter your username (you can use \"superPapi1954\"): ");
            user = User.GetUser(Console.ReadLine());
            while (user == null)
            {
                Console.WriteLine("Username not recognised.");
                Console.WriteLine("Enter your username: ");
                User.GetUser(Console.ReadLine());
            }
        }

        void PrintMenu()
        {
            Console.WriteLine("Press the corresponding key to navigate in the menu: ");
            Console.WriteLine("[b] Buy things");
            Console.WriteLine("[p] Get the bill to Pay");
            Console.WriteLine("[e] Exit");

        }
    }
}
