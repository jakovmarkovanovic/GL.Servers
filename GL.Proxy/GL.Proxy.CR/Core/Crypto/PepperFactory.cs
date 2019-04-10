﻿namespace GL.Proxy.CR.Core.Crypto
{
    using GL.Servers.Library.TweetNaCl;

    internal static class PepperFactory
    {
        internal static readonly byte[] ServerSecretKey =
        {
            0x24, 0x95, 0xA0, 0x86, 0xF1, 0x08, 0x92, 0xD5,
            0x81, 0x58, 0x60, 0xEB, 0x2F, 0x66, 0x91, 0xF1,
            0x77, 0x18, 0x95, 0x1E, 0x18, 0x12, 0xBC, 0x94,
            0x25, 0xF5, 0x0A, 0x4B, 0x59, 0x14, 0xBA, 0xD9
        };

        internal static readonly byte[] SupercellServerPublicKey =
        {
            0x98, 0x0C, 0xF7, 0xBB, 0x72, 0x62, 0xB3, 0x86,
            0xFE, 0xA6, 0x10, 0x34, 0xAB, 0xA7, 0x37, 0x06,
            0x13, 0x62, 0x79, 0x19, 0x66, 0x6B, 0x34, 0xE6,
            0xEC, 0xF6, 0x63, 0x07, 0xA3, 0x81, 0xDD, 0x61
        };

        internal static byte[] ServerPublicKey
        {
            get
            {
                byte[] k = new byte[32];
                curve25519xsalsa20poly1305.crypto_box_getpublickey(k, PepperFactory.ServerSecretKey);
                return k;
            }
        }
    }
}