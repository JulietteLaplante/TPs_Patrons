﻿using System;
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

        public override byte[] ReadData()
        {
            //TODO : faut-il un traitement pour enlever le "Encrypted" au debut du message ?
            return ((EncryptionDecorator)base.wrap).data;
        }

        public override void WriteData(byte[] data)
        {
            //string "Encrypted" à afficher au debut du message compressé
            byte[] enc = Encoding.ASCII.GetBytes("ENCRYPTED ");

            // data = enc + data
            ((EncryptionDecorator)base.wrap).data = enc.Concat(((EncryptionDecorator)base.wrap).data).ToArray();
        }

    }
}
