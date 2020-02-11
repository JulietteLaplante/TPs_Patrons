using System.Text;

namespace Tp1
{
    class Demo
    {
        static void Main(string[] args)
        {
            ConsoleCommunicationBuilder ccb = new ConsoleCommunicationBuilder();
            Communication communication = ccb.createCommunication();

            string data = "Voici un string";
            communication.WriteData(data);
        }
    }
}
