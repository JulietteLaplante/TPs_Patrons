using System;
using System.Collections.Generic;
using System.Text;

namespace Tp1
{
    class CommunicationDirector
    {
        private CommunicationBuilder builder;

        public CommunicationDirector(CommunicationBuilder builder)
        {
            this.builder = builder;
        }

        //dans l'exemple du prof il donne la variable this.builder, why ?
        public void ChangeBuilder(CommunicationBuilder builder)
        {
            this.builder = builder;
        }

        public Communication Make()
        {
            return builder.createCommunication();
        }
    }
}
