using System;
using System.Collections.Generic;
using System.Text;

namespace Tp1
{
    public abstract class Communication : Command
    {
        private byte[] writeData;
        private byte[] readData;


        public abstract void WriteData(byte[] data);
        public abstract byte[] ReadData();

        public void execute()
        {

        }
    }
}
