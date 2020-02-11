using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tp1
{
    public class CompressionDecorator : CommunicationDecorator
    {

        public CompressionDecorator(Communication com) : base(com)
        {
        }

        public override void Execute()
        {
            wrap.Execute();
        }

        public override String ReadData()
        {
            //TODO : faut-il un traitement pour enlever le "Compressed" au debut du message ?
            return wrap.ReadData();
        }

        public override void WriteData(String data)
        {
            wrap.WriteData("COMPRESSED " + data);
        }

    }
}