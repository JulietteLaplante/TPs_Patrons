using System;
using System.Collections.Generic;
using System.Text;

namespace Tp1
{
    class ConsoleCommunicationBuilder : CommunicationBuilder
    {
        public Communication createCommunication()
        {
            return new ConsoleCommunication();
        }

    }
}
