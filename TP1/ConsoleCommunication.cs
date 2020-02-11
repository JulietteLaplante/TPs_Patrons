using System;
using System.Collections.Generic;
using System.Text;

namespace Tp1
{
    class ConsoleCommunication : Communication
    {
        public ConsoleCommunication()
        {
            base.readData = "";
            base.writeData = "";

        }


        public override void Execute()
        {
            System.Threading.Thread.Sleep(500);
            // Console.WriteLine("WriteData: " + base.writeData + "\n");
            base.readData = base.writeData;
            base.writeData = "";
        }

        public override String ReadData()
        {
            String r = base.readData;
            return r;
        }

        public override void WriteData(String data)
        {
            Console.WriteLine("WriteData: " + data + "\n");
            if (!base.writeData.Equals(""))
            {
                base.writeData += "\n";
            }
            base.writeData += "Message: " + data;
            CommandExecutor.GetInstance().AddToQueue(this);
        }
    }
}
