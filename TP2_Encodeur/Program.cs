using System;
using System.Collections.Generic;

namespace EncodeurJSON
{
    class Program
    {
        static void Main(string[] args)
        {
            // A wonderful and mysterious object 'A'
            A a = new A();

            // This object serialized in a dictionnary
            Dictionary<string, object> output = JSON.Serialize(a);

            Console.WriteLine("Merci d'ouvrir le debuggeur pour pouvoir observer le contenu du dictionnaire 'output'");
        }
    }
}
