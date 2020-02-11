using System;
using System.Collections.Generic;
using System.Text;

namespace Tp1
{
    public abstract class Communication : Command
    {
        private event EventHandler evt;

        protected String readData;
        protected String writeData;


        //write in the Data buffer
        public abstract void WriteData(String data);

        public abstract String ReadData();

        public abstract void Execute();

        public void SetOnCompleted(Action action)
        {
            // TODO: add method like 
            evt += action;
        }

        public void Completed()
        {
            evt.Invoke(null, EventArgs.Empty);
        }
    }
}
