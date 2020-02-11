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
            Console.WriteLine("WriteData: " + base.writeData + "\n");
            base.readData = base.writeData;
            base.writeData = "";
            Console.WriteLine("Command Executed");
        }

        public override String ReadData()
        {
            String r = base.readData;
            return r;
        }

        public override void WriteData(String data)
        {
            if(!base.writeData.Equals(""))
            {
                base.writeData += "\n";
            }
            base.writeData += "Message: " + data;
            CommandExecutor.GetInstance().AddToQueue(this);
        }
    }
}
