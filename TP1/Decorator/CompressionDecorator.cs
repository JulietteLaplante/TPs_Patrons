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

        public override void ReadData()
        {
            //TODO : faut-il un traitement pour enlever le "Compressed" au debut du message ?
            wrap.ReadData();
        }

        public override void WriteData(byte[] data)
        {
            //string "Compressed" à afficher au debut du message compressé
            byte[] enc = Encoding.ASCII.GetBytes("COMPRESSED ");

            // data = enc + data
            byte[] compressedData = enc.Concat(data).ToArray();

            wrap.WriteData(compressedData);
        }

    }
}