using System;
using System.Collections.Generic;
using System.Text;

namespace Tp1
{
    public abstract class CommunicationDecorator : Communication
    {
        
        protected Communication wrap;

        public CommunicationDecorator(Communication com)
        {
            wrap = com;
        } 

    }
}
