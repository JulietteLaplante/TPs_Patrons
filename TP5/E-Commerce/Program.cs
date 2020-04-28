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
            ecom.Start();

        }
        void Start()
        {
            Authenticate();
            PrintMenu();
            sm.PrintCatalog();
            Console.WriteLine("What do you want to do ?");
            string input = Console.ReadLine();
            while (input != "e")
            {
                string[] command = input.Split(' ');
                if (command[0] == "b")
                {
                    try
                    {
                        ItemLine i = sm.ReserveItem(int.Parse(command[1]), command[2]);
                        if (i == null)
                        {
                            Console.WriteLine("Item does not exist or not in the quantity specified.");
                        }
                        else
                        {
                            cart.Add(i);
                            Console.WriteLine("Item(s) added to your cart.");
                        }
                    } catch (Exception e)
                    {
                        Console.WriteLine("Something went wrong with your purchase. \nDid you enter the command correctly ?");
                    }
                }
                else if (command[0] == "p")
                {
                    Bill b = Bill.CreateBill(user, cart);
                    Console.WriteLine("Total WITHOUT taxes: " + b.subTotalWithoutTaxes);
                    Console.WriteLine("Total WITH taxes: " + b.TotalWithTaxes);
                } 
                else if (command[0] == "h")
                {
                    PrintMenu();
                } 
                else if (command[0] == "c")
                {
                    sm.PrintCatalog();
                }
                Console.WriteLine(" What do you want to do ?");
                input = Console.ReadLine();
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
            Console.WriteLine(" Enter the corresponding command to use this app: ");
            Console.WriteLine(" [b quantity itemName] Buy things");
            Console.WriteLine(" [p] Get the bill");
            Console.WriteLine(" [h] Print this Help menu");
            Console.WriteLine(" [c] Print the catalalog again");
            Console.WriteLine(" [e] Exit");
            Console.WriteLine("\n");

        }
    }
}
