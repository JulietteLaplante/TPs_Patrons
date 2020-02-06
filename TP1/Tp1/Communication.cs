using System;
using System.Collections.Generic;
using System.Text;

namespace Tp1
{
    public abstract class Communication : Command
    {
        protected byte[] data;

        //write in the Data buffer
        public abstract void WriteData(byte[] data);

        public abstract byte[] ReadData();

        //send the communication
        public void execute()
        {
            
        }
    }
}
