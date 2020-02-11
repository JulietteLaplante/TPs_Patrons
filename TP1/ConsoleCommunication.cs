using System;
using System.Collections.Generic;
using System.Text;

namespace Tp1
{
    class ConsoleCommunication : Communication
    {


        public override void Execute()
        {
            Console.WriteLine("WriteData: " + base.data);
        }

        public override void ReadData()
        {
            Console.Write("ReadData: " + base.data);
        }

        public override void WriteData(String data)
        {
            if(base.data != "")
            {
                base.data += "\n";
            }
            base.data += "Message: " + data;
            CommandExecutor.GetInstance().AddToQueue(this);
        }
    }
}
