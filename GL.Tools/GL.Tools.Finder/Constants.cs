﻿namespace GL.Tools.Finder
{
    internal class Constants
    {
        /// <summary>
        /// Gets the clash of clans constant key bytes.
        /// </summary>
        /// <value>
        /// The clash of clans constant key bytes.
        /// </value>
        public static byte[] ClashOfClans
        {
            get
            {
                return new byte[]
                {
                    0x01, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF
                };
            }
        }

        /// <summary>
        /// Gets the boom beach constant key bytes.
        /// </summary>
        /// <value>
        /// The boom beach constant key bytes.
        /// </value>
        public static byte[] BoomBeach
        {
            get
            {
                return new byte[]
                {
                    0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF
                };
            }
        }

        /// <summary>
        /// Gets the clash royale constant key bytes.
        /// </summary>
        /// <value>
        /// The clash royale constant key bytes.
        /// </value>
        public static byte[] ClashRoyale
        {
            get
            {
                return new byte[]
                {
                    0x00, 0x00, 0x80, 0x3F, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF
                };
            }
        }

        public static byte[] ModdedKey
        {
            get
            {
                return new byte[]
                {
                    0x72, 0xF1, 0xA4, 0xA4, 0xC4, 0x8E, 0x44, 0xDA, 0x0C, 0x42, 0x31, 0x0F, 0x80, 0x0E, 0x96, 0x62, 0x4E, 0x6D, 0xC6, 0xA6, 0x41, 0xA9, 0xD4, 0x1C, 0x3B, 0x50, 0x39, 0xD8, 0xDF, 0xAD, 0xC2, 0x7E
                };
            }
        }
    }
}