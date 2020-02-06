using System;
using System.Collections.Generic;
using System.Text;

namespace Tp1
{
    public abstract class CommunicationDecorator : Communication
    {
        private Communication wrap;

        public CommunicationDecorator()
        {
        }

        public void execute(){
        
        }

        public abstract byte[] ReadData();

        public abstract void WriteData(byte[] data);
    }
}
