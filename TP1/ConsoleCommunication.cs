using System;
using System.Collections.Generic;
using System.Text;

namespace Tp1
{
    class ConsoleCommunication : Communication
    {
        public new void Execute()
        {
            throw new NotImplementedException();
        }

        public override void ReadData()
        {
            Console.Write(base.data);
        }

        public override void WriteData(byte[] data)
        {
            base.data = data;
        }
    }
}
