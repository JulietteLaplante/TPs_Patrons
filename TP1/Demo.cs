using System;
using System.Text;

namespace Tp1
{
    class Demo
    {
        static void Main(string[] args)
        {
            
            ConsoleCommunicationBuilder ccb = new ConsoleCommunicationBuilder();
            CommunicationDirector cd = new CommunicationDirector(ccb);
            Communication communication = cd.Make();

            Console.WriteLine("Demonstration: ");

            string data = "Hellow World";
            communication.WriteData(data);


            communication.ReadData();

            String readData;
            while ((readData = communication.ReadData()) == "");
            {
                Console.Write("ReadData: " + readData + "\n");
            }
        }
    }
}
