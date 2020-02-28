using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tp1
{
    public class EncryptionDecorator : CommunicationDecorator
    {

        public EncryptionDecorator(Communication com) : base(com)
        {
        }

        public override void Execute()
        {
            wrap.Execute();
        }

        public override string ReadData()
        {
            //TODO : faut-il un traitement pour enlever le "Encrypted" au debut du message ?
            return wrap.ReadData();
        }

        public override void WriteData(string data)
        {
            wrap.WriteData("ENCRYPTED " + data);
        }

    }
}
