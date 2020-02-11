using System;
using System.Collections.Generic;
using System.Text;

namespace Tp1
{
    public abstract class Communication : Command
    {
        protected String data;

        //write in the Data buffer
        public abstract void WriteData(String data);

        public abstract void ReadData();

        public abstract void Execute();
    }
}
