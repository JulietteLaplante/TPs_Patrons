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

            string data = "Hellow World";
            
            Console.WriteLine("Demonstration: ");
            communication.SetOnCompleted(() =>
            {
                Console.WriteLine("Command executed.");
                Console.WriteLine("ReadData: " + communication.ReadData() + "\n");
            });
            communication.WriteData(data);

        }
    }
}
