namespace GL.Servers.BS.Logic
{
    using System;
    using System.Collections.Generic;
    
    using GL.Servers.Extensions.List;

    using GL.Servers.BS.Logic.Slots;

    using Newtonsoft.Json;

    internal class Objects
    {
        [JsonProperty("player")]    internal Player Player;
        [JsonProperty("maps")]      internal Maps Maps;

        /// <summary>
        /// Initializes a new instance of the <see cref="Objects"/> class.
        /// </summary>
        internal Objects()
        {
            this.Maps       = new Maps(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Objects"/> class.
        /// </summary>
        /// <param name="Player">The player.</param>
        internal Objects(Player Player) : this()
        {
            this.Player     = Player;
        }

        internal byte[] ToBytes
        {
            get
            {
                DateTime UTCNow   = DateTime.UtcNow;
                List<byte> Packet = new List<byte>();

                this.Player.Info.Encode(Packet);

                // sub_656E0 (Event Info ?)

                Packet.AddVInt(UTCNow.Year * 1000 + UTCNow.DayOfYear);

                /*
                Packet.AddVInt(100);
                Packet.AddVInt(10);
                Packet.AddVInt(50);
                Packet.AddVInt(50);
                Packet.AddVInt(50);
                Packet.AddVInt(1000);
                Packet.AddVInt(14);
                Packet.AddVInt(1);
                Packet.AddVInt(2);
                Packet.AddVInt(5);
                Packet.AddVInt(10);
                Packet.AddVInt(3);

                Packet.AddVInt(0);
                Packet.AddVInt(0);
                Packet.AddVInt(0);
                */

                Packet.AddHexa("A4-01-0A-90-01-0A-14-32-32-A8-0F-A8-02-B4-07-32-8F-9C-01-06-01-02-04-0A-19-3C-06-03-0A-14-3C-88-03-B4-07-06-00-1E-90-01-AA-02-00-00");

                Packet.AddVInt(4);
                {
                    // LogicTimer::encode()
                    // LogicTimer::encode()
                    // LogicTimer::encode()
                    // LogicTimer::encode()

                    Packet.AddHexa("0100");
                    Packet.AddHexa("0203");
                    Packet.AddHexa("0305");
                    Packet.AddHexa("0408");
                }

                Packet.AddRange(this.Maps.ToBytes);

                Packet.AddVInt(0);
                {
                    /*
                    Packet.AddVInt(0);
                    Packet.AddVInt(0);
                    Packet.AddVInt(0);
                    Packet.AddVInt(0);
                    Packet.AddVInt(0);
                    Packet.AddVInt(0);
                    Packet.AddVInt(0);

                    Packet.AddBool(false);
                    Packet.AddBool(false);

                    Packet.AddVInt(0); // DataReference
                    Packet.AddVInt(0);

                    Packet.AddVInt(0);
                    Packet.AddVInt(0);

                    Packet.AddInt(0); // int or string
                    Packet.AddBool(false);
                    */
                }

                Packet.AddVInt(5);

                for (int i = 1; i < 6; i++)
                {
                    Packet.AddVInt(i); // 01-02-03-04-05
                }

                // sub_D59F4 (in sub_656E0) : Patchs

                Packet.AddVInt(518);
                {
                    /*
                    Packet.AddVInt(0);
                    Packet.AddVInt(0);
                    Packet.AddVInt(0);
                    Packet.AddVInt(0);

                    Packet.AddVInt(0); // Data Reference
                    Packet.AddVInt(0);

                    // sub_33834

                    Packet.AddVInt(0);
                    {
                        Packet.AddVInt(0);
                        Packet.AddVInt(0);

                        Packet.AddVInt(0); // Data Reference
                        Packet.AddVInt(0);
                    }
                    */
                }
                
                Packet.AddHexa("01    00-00-0A       00-01-01-14-05-01");
                Packet.AddHexa("01    01-0A-0A       00-01-01-14-05-01");
                Packet.AddHexa("01    02-14-0A       00-01-01-14-05-01");
                Packet.AddHexa("01    03-1E-0A       00-01-01-14-05-01");
                Packet.AddHexa("01    04-28-14       00-01-01-14-05-01");
                Packet.AddHexa("01    05-3C-14       00-01-01-14-05-01");

                Packet.AddHexa("01    06-90-01-14    00-01-01-14-05-01");
                Packet.AddHexa("01    07-A4-01-14    00-01-01-14-05-01");
                Packet.AddHexa("01    08-B8-01-14    00-01-01-14-05-01");
                Packet.AddHexa("01    09-8C-02-14    00-01-01-14-05-01");
                Packet.AddHexa("01    0A-A0-02-14    00-01-01-14-05-01");
                Packet.AddHexa("01    0B-B4-02-28    00-01-01-14-05-01");
                Packet.AddHexa("01    0C-9C-03-28    00-01-01-14-05-01");
                Packet.AddHexa("01    0D-84-04-28    00-01-01-14-05-01");
                Packet.AddHexa("01    0E-AC-04-28    00-01-01-14-05-01");
                Packet.AddHexa("01    0F-94-05-28    00-01-01-14-05-01");
                Packet.AddHexa("01    10-BC-05-28    00-01-01-14-05-01");
                Packet.AddHexa("01    11-A4-06-28    00-01-01-14-05-01");
                Packet.AddHexa("01    12-8C-07-28    00-01-01-14-05-01");

                Packet.AddHexa("05    00-00-28          00-01-01-14-05-01");
                Packet.AddHexa("05    01-28-32          00-01-01-14-05-01");
                Packet.AddHexa("05    02-9A-01-3C       00-01-01-14-05-01");

                Packet.AddHexa("05    03-96-02-86-01    00-01-01-14-05-01");
                Packet.AddHexa("05    04-9C-03-90-01    00-01-01-14-05-01");
                Packet.AddHexa("05    05-AC-04-9A-01    00-01-01-14-05-01");
                Packet.AddHexa("05    06-86-06-A4-01    00-01-01-14-05-01");
                Packet.AddHexa("05    07-AA-07-AE-01    00-01-01-14-05-01");
                Packet.AddHexa("05    08-98-09-B8-01    00-01-01-14-05-01");
                Packet.AddHexa("05    09-90-0B-82-02    00-01-01-14-05-01");
                Packet.AddHexa("05    0A-92-0D-8C-02    00-01-01-14-05-01");
                Packet.AddHexa("05    0B-9E-0F-96-02    00-01-01-14-05-01");
                Packet.AddHexa("05    0C-B4-11-A0-02    00-01-01-14-05-01");
                Packet.AddHexa("05    0D-94-14-AA-02    00-01-01-14-05-01");
                Packet.AddHexa("05    0E-BE-16-B4-02    00-01-01-14-05-01");
                Packet.AddHexa("05    0F-B2-19-BE-02    00-01-01-14-05-01");
                Packet.AddHexa("05    10-B0-1C-88-03    00-01-01-14-05-01");
                Packet.AddHexa("05    11-B8-1F-92-03    00-01-01-14-05-01");
                Packet.AddHexa("05    12-8A-23-9C-03    00-01-01-14-05-01");
                Packet.AddHexa("05    13-A6-26-A6-03    00-01-01-14-05-01");
                Packet.AddHexa("05    14-8C-2A-B0-03    00-01-01-14-05-01");
                Packet.AddHexa("05    15-BC-2D-BA-03    00-01-01-14-05-01");
                Packet.AddHexa("05    16-B6-31-84-04    00-01-01-14-05-01");
                Packet.AddHexa("05    17-BA-35-8E-04    00-01-01-14-05-01");
                Packet.AddHexa("05    18-88-3A-98-04    00-01-01-14-05-01");
                Packet.AddHexa("05    19-A0-3E-A2-04    00-01-01-14-05-01");
                Packet.AddHexa("05    1A-82-43-AC-04    00-01-01-14-05-01");
                Packet.AddHexa("05    1B-AE-47-B6-04    00-01-01-14-05-01");
                Packet.AddHexa("05    1C-A4-4C-80-05    00-01-01-14-05-01");
                Packet.AddHexa("05    1D-A4-51-8A-05    00-01-01-14-05-01");
                Packet.AddHexa("05    1E-AE-56-94-05    00-01-01-14-05-01");
                Packet.AddHexa("05    1F-82-5C-9E-05    00-01-01-14-05-01");
                Packet.AddHexa("05    20-A0-61-A8-05    00-01-01-14-05-01");
                Packet.AddHexa("05    21-88-67-B2-05    00-01-01-14-05-01");
                Packet.AddHexa("05    22-BA-6C-BC-05    00-01-01-14-05-01");
                Packet.AddHexa("05    23-B6-72-86-06    00-01-01-14-05-01");
                Packet.AddHexa("05    24-BC-78-90-06    00-01-01-14-05-01");
                Packet.AddHexa("05    25-8C-7F-9A-06    00-01-01-14-05-01");

                Packet.AddHexa("05    26-A6-85-01-A4-06    00-01-01-14-05-01");
                Packet.AddHexa("05    27-8A-8C-01-AE-06    00-01-01-14-05-01");
                Packet.AddHexa("05    28-B8-92-01-B8-06    00-01-01-14-05-01");
                Packet.AddHexa("05    29-B0-99-01-82-07    00-01-01-14-05-01");
                Packet.AddHexa("05    2A-B2-A0-01-8C-07    00-01-01-14-05-01");
                Packet.AddHexa("05    2B-BE-A7-01-96-07    00-01-01-14-05-01");
                Packet.AddHexa("05    2C-94-AF-01-A0-07    00-01-01-14-05-01");
                Packet.AddHexa("05    2D-B4-B6-01-AA-07    00-01-01-14-05-01");
                Packet.AddHexa("05    2E-9E-BE-01-B4-07    00-01-01-14-05-01");
                Packet.AddHexa("05    2F-92-C6-01-BE-07    00-01-01-14-05-01");
                Packet.AddHexa("05    30-90-CE-01-88-08    00-01-01-14-05-01");
                Packet.AddHexa("05    31-98-D6-01-92-08    00-01-01-14-05-01");
                Packet.AddHexa("05    32-AA-DE-01-9C-08    00-01-01-14-05-01");
                Packet.AddHexa("05    33-86-E7-01-A6-08    00-01-01-14-05-01");
                Packet.AddHexa("05    34-AC-EF-01-B0-08    00-01-01-14-05-01");
                Packet.AddHexa("05    35-9C-F8-01-BA-08    00-01-01-14-05-01");
                Packet.AddHexa("05    36-96-81-02-84-09    00-01-01-14-05-01");
                Packet.AddHexa("05    37-9A-8A-02-8E-09    00-01-01-14-05-01");
                Packet.AddHexa("05    38-A8-93-02-98-09    00-01-01-14-05-01");
                Packet.AddHexa("05    39-80-9D-02-A2-09    00-01-01-14-05-01");
                Packet.AddHexa("05    3A-A2-A6-02-AC-09    00-01-01-14-05-01");
                Packet.AddHexa("05    3B-8E-B0-02-B6-09    00-01-01-14-05-01");
                Packet.AddHexa("05    3C-84-BA-02-80-0A    00-01-01-14-05-01");
                Packet.AddHexa("05    3D-84-C4-02-8A-0A    00-01-01-14-05-01");
                Packet.AddHexa("05    3E-8E-CE-02-94-0A    00-01-01-14-05-01");
                Packet.AddHexa("05    3F-A2-D8-02-9E-0A    00-01-01-14-05-01");

                Packet.AddHexa("05    80-01-80-E3-02-A8-0A    00-01-01-14-05-01");
                Packet.AddHexa("05    81-01-A8-ED-02-B2-0A    00-01-01-14-05-01");
                Packet.AddHexa("05    82-01-9A-F8-02-BC-0A    00-01-01-14-05-01");
                Packet.AddHexa("05    83-01-96-83-03-86-0B    00-01-01-14-05-01");
                Packet.AddHexa("05    84-01-9C-8E-03-90-0B    00-01-01-14-05-01");
                Packet.AddHexa("05    85-01-AC-99-03-9A-0B    00-01-01-14-05-01");
                Packet.AddHexa("05    86-01-86-A5-03-A4-0B    00-01-01-14-05-01");
                Packet.AddHexa("05    87-01-AA-B0-03-AE-0B    00-01-01-14-05-01");
                Packet.AddHexa("05    88-01-98-BC-03-B8-0B    00-01-01-14-05-01");
                Packet.AddHexa("05    89-01-90-C8-03-82-0C    00-01-01-14-05-01");
                Packet.AddHexa("05    8A-01-92-D4-03-8C-0C    00-01-01-14-05-01");
                Packet.AddHexa("05    8B-01-9E-E0-03-96-0C    00-01-01-14-05-01");
                Packet.AddHexa("05    8C-01-B4-EC-03-A0-0C    00-01-01-14-05-01");
                Packet.AddHexa("05    8D-01-94-F9-03-AA-0C    00-01-01-14-05-01");
                Packet.AddHexa("05    8E-01-BE-85-04-B4-0C    00-01-01-14-05-01");
                Packet.AddHexa("05    8F-01-B2-92-04-BE-0C    00-01-01-14-05-01");
                Packet.AddHexa("05    90-01-B0-9F-04-88-0D    00-01-01-14-05-01");
                Packet.AddHexa("05    91-01-B8-AC-04-92-0D    00-01-01-14-05-01");
                Packet.AddHexa("05    92-01-8A-BA-04-9C-0D    00-01-01-14-05-01");
                Packet.AddHexa("05    93-01-A6-C7-04-A6-0D    00-01-01-14-05-01");
                Packet.AddHexa("05    94-01-8C-D5-04-B0-0D    00-01-01-14-05-01");
                Packet.AddHexa("05    95-01-BC-E2-04-BA-0D    00-01-01-14-05-01");
                Packet.AddHexa("05    96-01-B6-F0-04-84-0E    00-01-01-14-05-01");
                Packet.AddHexa("05    97-01-BA-FE-04-8E-0E    00-01-01-14-05-01");
                Packet.AddHexa("05    98-01-88-8D-05-98-0E    00-01-01-14-05-01");
                Packet.AddHexa("05    99-01-A0-9B-05-A2-0E    00-01-01-14-05-01");
                Packet.AddHexa("05    9A-01-82-AA-05-AC-0E    00-01-01-14-05-01");
                Packet.AddHexa("05    9B-01-AE-B8-05-B6-0E    00-01-01-14-05-01");
                Packet.AddHexa("05    9C-01-A4-C7-05-80-0F    00-01-01-14-05-01");
                Packet.AddHexa("05    9D-01-A4-D6-05-8A-0F    00-01-01-14-05-01");
                Packet.AddHexa("05    9E-01-AE-E5-05-94-0F    00-01-01-14-05-01");
                Packet.AddHexa("05    9F-01-82-F5-05-9E-0F    00-01-01-14-05-01");
                Packet.AddHexa("05    A0-01-A0-84-06-A8-0F    00-01-01-14-05-01");
                Packet.AddHexa("05    A1-01-88-94-06-B2-0F    00-01-01-14-05-01");
                Packet.AddHexa("05    A2-01-BA-A3-06-BC-0F    00-01-01-14-05-01");
                Packet.AddHexa("05    A3-01-B6-B3-06-86-10    00-01-01-14-05-01");
                Packet.AddHexa("05    A4-01-BC-C3-06-90-10    00-01-01-14-05-01");
                Packet.AddHexa("05    A5-01-8C-D4-06-9A-10    00-01-01-14-05-01");
                Packet.AddHexa("05    A6-01-A6-E4-06-A4-10    00-01-01-14-05-01");
                Packet.AddHexa("05    A7-01-8A-F5-06-AE-10    00-01-01-14-05-01");
                Packet.AddHexa("05    A8-01-B8-85-07-B8-10    00-01-01-14-05-01");
                Packet.AddHexa("05    A9-01-B0-96-07-82-11    00-01-01-14-05-01");
                Packet.AddHexa("05    AA-01-B2-A7-07-8C-11    00-01-01-14-05-01");
                Packet.AddHexa("05    AB-01-BE-B8-07-96-11    00-01-01-14-05-01");
                Packet.AddHexa("05    AC-01-94-CA-07-A0-11    00-01-01-14-05-01");
                Packet.AddHexa("05    AD-01-B4-DB-07-AA-11    00-01-01-14-05-01");
                Packet.AddHexa("05    AE-01-9E-ED-07-B4-11    00-01-01-14-05-01");
                Packet.AddHexa("05    AF-01-92-FF-07-BE-11    00-01-01-14-05-01");
                Packet.AddHexa("05    B0-01-90-91-08-88-12    00-01-01-14-05-01");
                Packet.AddHexa("05    B1-01-98-A3-08-92-12    00-01-01-14-05-01");
                Packet.AddHexa("05    B2-01-AA-B5-08-9C-12    00-01-01-14-05-01");
                Packet.AddHexa("05    B3-01-86-C8-08-A6-12    00-01-01-14-05-01");
                Packet.AddHexa("05    B4-01-AC-DA-08-B0-12    00-01-01-14-05-01");
                Packet.AddHexa("05    B5-01-9C-ED-08-BA-12    00-01-01-14-05-01");
                Packet.AddHexa("05    B6-01-96-80-09-84-13    00-01-01-14-05-01");
                Packet.AddHexa("05    B7-01-9A-93-09-8E-13    00-01-01-14-05-01");
                Packet.AddHexa("05    B8-01-A8-A6-09-98-13    00-01-01-14-05-01");
                Packet.AddHexa("05    B9-01-80-BA-09-A2-13    00-01-01-14-05-01");
                Packet.AddHexa("05    BA-01-A2-CD-09-AC-13    00-01-01-14-05-01");
                Packet.AddHexa("05    BB-01-8E-E1-09-B6-13    00-01-01-14-05-01");
                Packet.AddHexa("05    BC-01-84-F5-09-80-14    00-01-01-14-05-01");
                Packet.AddHexa("05    BD-01-84-89-0A-8A-14    00-01-01-14-05-01");
                Packet.AddHexa("05    BE-01-8E-9D-0A-94-14    00-01-01-14-05-01");
                Packet.AddHexa("05    BF-01-A2-B1-0A-9E-14    00-01-01-14-05-01");
                Packet.AddHexa("05    80-02-80-C6-0A-A8-14    00-01-01-14-05-01");
                Packet.AddHexa("05    81-02-A8-DA-0A-B2-14    00-01-01-14-05-01");
                Packet.AddHexa("05    82-02-9A-EF-0A-BC-14    00-01-01-14-05-01");
                Packet.AddHexa("05    83-02-96-84-0B-86-15    00-01-01-14-05-01");
                Packet.AddHexa("05    84-02-9C-99-0B-90-15    00-01-01-14-05-01");
                Packet.AddHexa("05    85-02-AC-AE-0B-9A-15    00-01-01-14-05-01");
                Packet.AddHexa("05    86-02-86-C4-0B-A4-15    00-01-01-14-05-01");
                Packet.AddHexa("05    87-02-AA-D9-0B-AE-15    00-01-01-14-05-01");
                Packet.AddHexa("05    88-02-98-EF-0B-B8-15    00-01-01-14-05-01");
                Packet.AddHexa("05    89-02-90-85-0C-82-16    00-01-01-14-05-01");
                Packet.AddHexa("05    8A-02-92-9B-0C-8C-16    00-01-01-14-05-01");
                Packet.AddHexa("05    8B-02-9E-B1-0C-96-16    00-01-01-14-05-01");
                Packet.AddHexa("05    8C-02-B4-C7-0C-A0-16    00-01-01-14-05-01");
                Packet.AddHexa("05    8D-02-94-DE-0C-AA-16    00-01-01-14-05-01");
                Packet.AddHexa("05    8E-02-BE-F4-0C-B4-16    00-01-01-14-05-01");
                Packet.AddHexa("05    8F-02-B2-8B-0D-BE-16    00-01-01-14-05-01");
                Packet.AddHexa("05    90-02-B0-A2-0D-88-17    00-01-01-14-05-01");
                Packet.AddHexa("05    91-02-B8-B9-0D-92-17    00-01-01-14-05-01");
                Packet.AddHexa("05    92-02-8A-D1-0D-9C-17    00-01-01-14-05-01");
                Packet.AddHexa("05    93-02-A6-E8-0D-A6-17    00-01-01-14-05-01");
                Packet.AddHexa("05    94-02-8C-80-0E-B0-17    00-01-01-14-05-01");
                Packet.AddHexa("05    95-02-BC-97-0E-BA-17    00-01-01-14-05-01");
                Packet.AddHexa("05    96-02-B6-AF-0E-84-18    00-01-01-14-05-01");
                Packet.AddHexa("05    97-02-BA-C7-0E-8E-18    00-01-01-14-05-01");
                Packet.AddHexa("05    98-02-88-E0-0E-98-18    00-01-01-14-05-01");
                Packet.AddHexa("05    99-02-A0-F8-0E-A2-18    00-01-01-14-05-01");
                Packet.AddHexa("05    9A-02-82-91-0F-AC-18    00-01-01-14-05-01");
                Packet.AddHexa("05    9B-02-AE-A9-0F-B6-18    00-01-01-14-05-01");
                Packet.AddHexa("05    9C-02-A4-C2-0F-80-19    00-01-01-14-05-01");
                Packet.AddHexa("05    9D-02-A4-DB-0F-8A-19    00-01-01-14-05-01");
                Packet.AddHexa("05    9E-02-AE-F4-0F-94-19    00-01-01-14-05-01");
                Packet.AddHexa("05    9F-02-82-8E-10-9E-19    00-01-01-14-05-01");
                Packet.AddHexa("05    A0-02-A0-A7-10-A8-19    00-01-01-14-05-01");
                Packet.AddHexa("05    A1-02-88-C1-10-B2-19    00-01-01-14-05-01");
                Packet.AddHexa("05    A2-02-BA-DA-10-BC-19    00-01-01-14-05-01");
                Packet.AddHexa("05    A3-02-B6-F4-10-86-1A    00-01-01-14-05-01");
                Packet.AddHexa("05    A4-02-BC-8E-11-90-1A    00-01-01-14-05-01");
                Packet.AddHexa("05    A5-02-8C-A9-11-9A-1A    00-01-01-14-05-01");
                Packet.AddHexa("05    A6-02-A6-C3-11-A4-1A    00-01-01-14-05-01");
                Packet.AddHexa("05    A7-02-8A-DE-11-AE-1A    00-01-01-14-05-01");
                Packet.AddHexa("05    A8-02-B8-F8-11-B8-1A    00-01-01-14-05-01");
                Packet.AddHexa("05    A9-02-B0-93-12-82-1B    00-01-01-14-05-01");
                Packet.AddHexa("05    AA-02-B2-AE-12-8C-1B    00-01-01-14-05-01");
                Packet.AddHexa("05    AB-02-BE-C9-12-96-1B    00-01-01-14-05-01");
                Packet.AddHexa("05    AC-02-94-E5-12-A0-1B    00-01-01-14-05-01");
                Packet.AddHexa("05    AD-02-B4-80-13-AA-1B    00-01-01-14-05-01");
                Packet.AddHexa("05    AE-02-9E-9C-13-B4-1B    00-01-01-14-05-01");
                Packet.AddHexa("05    AF-02-92-B8-13-BE-1B    00-01-01-14-05-01");
                Packet.AddHexa("05    B0-02-90-D4-13-88-1C    00-01-01-14-05-01");
                Packet.AddHexa("05    B1-02-98-F0-13-92-1C    00-01-01-14-05-01");
                Packet.AddHexa("05    B2-02-AA-8C-14-9C-1C    00-01-01-14-05-01");
                Packet.AddHexa("05    B3-02-86-A9-14-A6-1C    00-01-01-14-05-01");
                Packet.AddHexa("05    B4-02-AC-C5-14-B0-1C    00-01-01-14-05-01");
                Packet.AddHexa("05    B5-02-9C-E2-14-BA-1C    00-01-01-14-05-01");
                Packet.AddHexa("05    B6-02-96-FF-14-84-1D    00-01-01-14-05-01");
                Packet.AddHexa("05    B7-02-9A-9C-15-8E-1D    00-01-01-14-05-01");
                Packet.AddHexa("05    B8-02-A8-B9-15-98-1D    00-01-01-14-05-01");
                Packet.AddHexa("05    B9-02-80-D7-15-A2-1D    00-01-01-14-05-01");
                Packet.AddHexa("05    BA-02-A2-F4-15-AC-1D    00-01-01-14-05-01");
                Packet.AddHexa("05    BB-02-8E-92-16-B6-1D    00-01-01-14-05-01");
                Packet.AddHexa("05    BC-02-84-B0-16-80-1E    00-01-01-14-05-01");
                Packet.AddHexa("05    BD-02-84-CE-16-8A-1E    00-01-01-14-05-01");
                Packet.AddHexa("05    BE-02-8E-EC-16-94-1E    00-01-01-14-05-01");
                Packet.AddHexa("05    BF-02-A2-8A-17-9E-1E    00-01-01-14-05-01");
                Packet.AddHexa("05    80-03-80-A9-17-A8-1E    00-01-01-14-05-01");
                Packet.AddHexa("05    81-03-A8-C7-17-B2-1E    00-01-01-14-05-01");
                Packet.AddHexa("05    82-03-9A-E6-17-BC-1E    00-01-01-14-05-01");
                Packet.AddHexa("05    83-03-96-85-18-86-1F    00-01-01-14-05-01");
                Packet.AddHexa("05    84-03-9C-A4-18-90-1F    00-01-01-14-05-01");
                Packet.AddHexa("05    85-03-AC-C3-18-9A-1F    00-01-01-14-05-01");
                Packet.AddHexa("05    86-03-86-E3-18-A4-1F    00-01-01-14-05-01");
                Packet.AddHexa("05    87-03-AA-82-19-AE-1F    00-01-01-14-05-01");
                Packet.AddHexa("05    88-03-98-A2-19-B8-1F    00-01-01-14-05-01");
                Packet.AddHexa("05    89-03-90-C2-19-82-20    00-01-01-14-05-01");
                Packet.AddHexa("05    8A-03-92-E2-19-8C-20    00-01-01-14-05-01");
                Packet.AddHexa("05    8B-03-9E-82-1A-96-20    00-01-01-14-05-01");
                Packet.AddHexa("05    8C-03-B4-A2-1A-A0-20    00-01-01-14-05-01");
                Packet.AddHexa("05    8D-03-94-C3-1A-AA-20    00-01-01-14-05-01");
                Packet.AddHexa("05    8E-03-BE-E3-1A-B4-20    00-01-01-14-05-01");
                Packet.AddHexa("05    8F-03-B2-84-1B-BE-20    00-01-01-14-05-01");
                Packet.AddHexa("05    90-03-B0-A5-1B-88-21    00-01-01-14-05-01");
                Packet.AddHexa("05    91-03-B8-C6-1B-92-21    00-01-01-14-05-01");
                Packet.AddHexa("05    92-03-8A-E8-1B-9C-21    00-01-01-14-05-01");
                Packet.AddHexa("05    93-03-A6-89-1C-A6-21    00-01-01-14-05-01");
                Packet.AddHexa("05    94-03-8C-AB-1C-B0-21    00-01-01-14-05-01");
                Packet.AddHexa("05    95-03-BC-CC-1C-BA-21    00-01-01-14-05-01");
                Packet.AddHexa("05    96-03-B6-EE-1C-84-22    00-01-01-14-05-01");
                Packet.AddHexa("05    97-03-BA-90-1D-8E-22    00-01-01-14-05-01");
                Packet.AddHexa("05    98-03-88-B3-1D-98-22    00-01-01-14-05-01");
                Packet.AddHexa("05    99-03-A0-D5-1D-A2-22    00-01-01-14-05-01");
                Packet.AddHexa("05    9A-03-82-F8-1D-AC-22    00-01-01-14-05-01");
                Packet.AddHexa("05    9B-03-AE-9A-1E-B6-22    00-01-01-14-05-01");
                Packet.AddHexa("05    9C-03-A4-BD-1E-80-23    00-01-01-14-05-01");
                Packet.AddHexa("05    9D-03-A4-E0-1E-8A-23    00-01-01-14-05-01");
                Packet.AddHexa("05    9E-03-AE-83-1F-94-23    00-01-01-14-05-01");
                Packet.AddHexa("05    9F-03-82-A7-1F-9E-23    00-01-01-14-05-01");
                Packet.AddHexa("05    A0-03-A0-CA-1F-A8-23    00-01-01-14-05-01");
                Packet.AddHexa("05    A1-03-88-EE-1F-B2-23    00-01-01-14-05-01");
                Packet.AddHexa("05    A2-03-BA-91-20-BC-23    00-01-01-14-05-01");
                Packet.AddHexa("05    A3-03-B6-B5-20-86-24    00-01-01-14-05-01");
                Packet.AddHexa("05    A4-03-BC-D9-20-90-24    00-01-01-14-05-01");
                Packet.AddHexa("05    A5-03-8C-FE-20-9A-24    00-01-01-14-05-01");
                Packet.AddHexa("05    A6-03-A6-A2-21-A4-24    00-01-01-14-05-01");
                Packet.AddHexa("05    A7-03-8A-C7-21-AE-24    00-01-01-14-05-01");
                Packet.AddHexa("05    A8-03-B8-EB-21-B8-24    00-01-01-14-05-01");
                Packet.AddHexa("05    A9-03-B0-90-22-82-25    00-01-01-14-05-01");
                Packet.AddHexa("05    AA-03-B2-B5-22-8C-25    00-01-01-14-05-01");
                Packet.AddHexa("05    AB-03-BE-DA-22-96-25    00-01-01-14-05-01");
                Packet.AddHexa("05    AC-03-94-80-23-A0-25    00-01-01-14-05-01");
                Packet.AddHexa("05    AD-03-B4-A5-23-AA-25    00-01-01-14-05-01");
                Packet.AddHexa("05    AE-03-9E-CB-23-B4-25    00-01-01-14-05-01");
                Packet.AddHexa("05    AF-03-92-F1-23-BE-25    00-01-01-14-05-01");
                Packet.AddHexa("05    B0-03-90-97-24-88-26    00-01-01-14-05-01");
                Packet.AddHexa("05    B1-03-98-BD-24-92-26    00-01-01-14-05-01");
                Packet.AddHexa("05    B2-03-AA-E3-24-9C-26    00-01-01-14-05-01");
                Packet.AddHexa("05    B3-03-86-8A-25-A6-26    00-01-01-14-05-01");
                Packet.AddHexa("05    B4-03-AC-B0-25-B0-26    00-01-01-14-05-01");
                Packet.AddHexa("05    B5-03-9C-D7-25-BA-26    00-01-01-14-05-01");
                Packet.AddHexa("05    B6-03-96-FE-25-84-27    00-01-01-14-05-01");
                Packet.AddHexa("05    B7-03-9A-A5-26-8E-27    00-01-01-14-05-01");
                Packet.AddHexa("05    B8-03-A8-CC-26-98-27    00-01-01-14-05-01");
                Packet.AddHexa("05    B9-03-80-F4-26-A2-27    00-01-01-14-05-01");
                Packet.AddHexa("05    BA-03-A2-9B-27-AC-27    00-01-01-14-05-01");
                Packet.AddHexa("05    BB-03-8E-C3-27-B6-27    00-01-01-14-05-01");
                Packet.AddHexa("05    BC-03-84-EB-27-80-28    00-01-01-14-05-01");
                Packet.AddHexa("05    BD-03-84-93-28-8A-28    00-01-01-14-05-01");
                Packet.AddHexa("05    BE-03-8E-BB-28-94-28    00-01-01-14-05-01");
                Packet.AddHexa("05    BF-03-A2-E3-28-9E-28    00-01-01-14-05-01");
                Packet.AddHexa("05    80-04-80-8C-29-A8-28    00-01-01-14-05-01");
                Packet.AddHexa("05    81-04-A8-B4-29-B2-28    00-01-01-14-05-01");
                Packet.AddHexa("05    82-04-9A-DD-29-BC-28    00-01-01-14-05-01");
                Packet.AddHexa("05    83-04-96-86-2A-86-29    00-01-01-14-05-01");
                Packet.AddHexa("05    84-04-9C-AF-2A-90-29    00-01-01-14-05-01");
                Packet.AddHexa("05    85-04-AC-D8-2A-9A-29    00-01-01-14-05-01");
                Packet.AddHexa("05    86-04-86-82-2B-A4-29    00-01-01-14-05-01");
                Packet.AddHexa("05    87-04-AA-AB-2B-AE-29    00-01-01-14-05-01");
                Packet.AddHexa("05    88-04-98-D5-2B-B8-29    00-01-01-14-05-01");
                Packet.AddHexa("05    89-04-90-FF-2B-82-2A    00-01-01-14-05-01");
                Packet.AddHexa("05    8A-04-92-A9-2C-8C-2A    00-01-01-14-05-01");
                Packet.AddHexa("05    8B-04-9E-D3-2C-96-2A    00-01-01-14-05-01");
                Packet.AddHexa("05    8C-04-B4-FD-2C-A0-2A    00-01-01-14-05-01");
                Packet.AddHexa("05    8D-04-94-A8-2D-AA-2A    00-01-01-14-05-01");
                Packet.AddHexa("05    8E-04-BE-D2-2D-B4-2A    00-01-01-14-05-01");
                Packet.AddHexa("05    8F-04-B2-FD-2D-BE-2A    00-01-01-14-05-01");
                Packet.AddHexa("05    90-04-B0-A8-2E-88-2B    00-01-01-14-05-01");
                Packet.AddHexa("05    91-04-B8-D3-2E-92-2B    00-01-01-14-05-01");
                Packet.AddHexa("05    92-04-8A-FF-2E-9C-2B    00-01-01-14-05-01");
                Packet.AddHexa("05    93-04-A6-AA-2F-A6-2B    00-01-01-14-05-01");
                Packet.AddHexa("05    94-04-8C-D6-2F-B0-2B    00-01-01-14-05-01");
                Packet.AddHexa("05    95-04-BC-81-30-BA-2B    00-01-01-14-05-01");
                Packet.AddHexa("05    96-04-B6-AD-30-84-2C    00-01-01-14-05-01");
                Packet.AddHexa("05    97-04-BA-D9-30-8E-2C    00-01-01-14-05-01");
                Packet.AddHexa("05    98-04-88-86-31-98-2C    00-01-01-14-05-01");
                Packet.AddHexa("05    99-04-A0-B2-31-A2-2C    00-01-01-14-05-01");
                Packet.AddHexa("05    9A-04-82-DF-31-AC-2C    00-01-01-14-05-01");
                Packet.AddHexa("05    9B-04-AE-8B-32-B6-2C    00-01-01-14-05-01");
                Packet.AddHexa("05    9C-04-A4-B8-32-80-2D    00-01-01-14-05-01");
                Packet.AddHexa("05    9D-04-A4-E5-32-8A-2D    00-01-01-14-05-01");
                Packet.AddHexa("05    9E-04-AE-92-33-94-2D    00-01-01-14-05-01");
                Packet.AddHexa("05    9F-04-82-C0-33-9E-2D    00-01-01-14-05-01");
                Packet.AddHexa("05    A0-04-A0-ED-33-A8-2D    00-01-01-14-05-01");
                Packet.AddHexa("05    A1-04-88-9B-34-B2-2D    00-01-01-14-05-01");
                Packet.AddHexa("05    A2-04-BA-C8-34-BC-2D    00-01-01-14-05-01");
                Packet.AddHexa("05    A3-04-B6-F6-34-86-2E    00-01-01-14-05-01");
                Packet.AddHexa("05    A4-04-BC-A4-35-90-2E    00-01-01-14-05-01");
                Packet.AddHexa("05    A5-04-8C-D3-35-9A-2E    00-01-01-14-05-01");
                Packet.AddHexa("05    A6-04-A6-81-36-A4-2E    00-01-01-14-05-01");
                Packet.AddHexa("05    A7-04-8A-B0-36-AE-2E    00-01-01-14-05-01");
                Packet.AddHexa("05    A8-04-B8-DE-36-B8-2E    00-01-01-14-05-01");
                Packet.AddHexa("05    A9-04-B0-8D-37-82-2F    00-01-01-14-05-01");
                Packet.AddHexa("05    AA-04-B2-BC-37-8C-2F    00-01-01-14-05-01");
                Packet.AddHexa("05    AB-04-BE-EB-37-96-2F    00-01-01-14-05-01");
                Packet.AddHexa("05    AC-04-94-9B-38-A0-2F    00-01-01-14-05-01");
                Packet.AddHexa("05    AD-04-B4-CA-38-AA-2F    00-01-01-14-05-01");
                Packet.AddHexa("05    AE-04-9E-FA-38-B4-2F    00-01-01-14-05-01");
                Packet.AddHexa("05    AF-04-92-AA-39-BE-2F    00-01-01-14-05-01");
                Packet.AddHexa("05    B0-04-90-DA-39-88-30    00-01-01-14-05-01");
                Packet.AddHexa("05    B1-04-98-8A-3A-92-30    00-01-01-14-05-01");
                Packet.AddHexa("05    B2-04-AA-BA-3A-9C-30    00-01-01-14-05-01");
                Packet.AddHexa("05    B3-04-86-EB-3A-A6-30    00-01-01-14-05-01");
                Packet.AddHexa("05    B4-04-AC-9B-3B-B0-30    00-01-01-14-05-01");
                Packet.AddHexa("05    B5-04-9C-CC-3B-BA-30    00-01-01-14-05-01");
                Packet.AddHexa("05    B6-04-96-FD-3B-84-31    00-01-01-14-05-01");
                Packet.AddHexa("05    B7-04-9A-AE-3C-8E-31    00-01-01-14-05-01");
                Packet.AddHexa("05    B8-04-A8-DF-3C-98-31    00-01-01-14-05-01");
                Packet.AddHexa("05    B9-04-80-91-3D-A2-31    00-01-01-14-05-01");
                Packet.AddHexa("05    BA-04-A2-C2-3D-AC-31    00-01-01-14-05-01");
                Packet.AddHexa("05    BB-04-8E-F4-3D-B6-31    00-01-01-14-05-01");
                Packet.AddHexa("05    BC-04-84-A6-3E-80-32    00-01-01-14-05-01");
                Packet.AddHexa("05    BD-04-84-D8-3E-8A-32    00-01-01-14-05-01");
                Packet.AddHexa("05    BE-04-8E-8A-3F-94-32    00-01-01-14-05-01");
                Packet.AddHexa("05    BF-04-A2-BC-3F-9E-32    00-01-01-14-05-01");
                Packet.AddHexa("05    80-05-80-EF-3F-A8-32    00-01-01-14-05-01");
                Packet.AddHexa("05    81-05-A8-A1-40-B2-32    00-01-01-14-05-01");
                Packet.AddHexa("05    82-05-9A-D4-40-BC-32    00-01-01-14-05-01");
                Packet.AddHexa("05    83-05-96-87-41-86-33    00-01-01-14-05-01");
                Packet.AddHexa("05    84-05-9C-BA-41-90-33    00-01-01-14-05-01");
                Packet.AddHexa("05    85-05-AC-ED-41-9A-33    00-01-01-14-05-01");
                Packet.AddHexa("05    86-05-86-A1-42-A4-33    00-01-01-14-05-01");
                Packet.AddHexa("05    87-05-AA-D4-42-AE-33    00-01-01-14-05-01");
                Packet.AddHexa("05    88-05-98-88-43-B8-33    00-01-01-14-05-01");
                Packet.AddHexa("05    89-05-90-BC-43-82-34    00-01-01-14-05-01");
                Packet.AddHexa("05    8A-05-92-F0-43-8C-34    00-01-01-14-05-01");
                Packet.AddHexa("05    8B-05-9E-A4-44-96-34    00-01-01-14-05-01");
                Packet.AddHexa("05    8C-05-B4-D8-44-A0-34    00-01-01-14-05-01");
                Packet.AddHexa("05    8D-05-94-8D-45-AA-34    00-01-01-14-05-01");
                Packet.AddHexa("05    8E-05-BE-C1-45-B4-34    00-01-01-14-05-01");
                Packet.AddHexa("05    8F-05-B2-F6-45-BE-34    00-01-01-14-05-01");
                Packet.AddHexa("05    90-05-B0-AB-46-88-35    00-01-01-14-05-01");
                Packet.AddHexa("05    91-05-B8-E0-46-92-35    00-01-01-14-05-01");
                Packet.AddHexa("05    92-05-8A-96-47-9C-35    00-01-01-14-05-01");
                Packet.AddHexa("05    93-05-A6-CB-47-A6-35    00-01-01-14-05-01");
                Packet.AddHexa("05    94-05-8C-81-48-B0-35    00-01-01-14-05-01");
                Packet.AddHexa("05    95-05-BC-B6-48-BA-35    00-01-01-14-05-01");
                Packet.AddHexa("05    96-05-B6-EC-48-84-36    00-01-01-14-05-01");
                Packet.AddHexa("05    97-05-BA-A2-49-8E-36    00-01-01-14-05-01");
                Packet.AddHexa("05    98-05-88-D9-49-98-36    00-01-01-14-05-01");
                Packet.AddHexa("05    99-05-A0-8F-4A-A2-36    00-01-01-14-05-01");
                Packet.AddHexa("05    9A-05-82-C6-4A-AC-36    00-01-01-14-05-01");
                Packet.AddHexa("05    9B-05-AE-FC-4A-B6-36    00-01-01-14-05-01");
                Packet.AddHexa("05    9C-05-A4-B3-4B-80-37    00-01-01-14-05-01");
                Packet.AddHexa("05    9D-05-A4-EA-4B-8A-37    00-01-01-14-05-01");
                Packet.AddHexa("05    9E-05-AE-A1-4C-94-37    00-01-01-14-05-01");
                Packet.AddHexa("05    9F-05-82-D9-4C-9E-37    00-01-01-14-05-01");
                Packet.AddHexa("05    A0-05-A0-90-4D-A8-37    00-01-01-14-05-01");
                Packet.AddHexa("05    A1-05-88-C8-4D-B2-37    00-01-01-14-05-01");
                Packet.AddHexa("05    A2-05-BA-FF-4D-BC-37    00-01-01-14-05-01");
                Packet.AddHexa("05    A3-05-B6-B7-4E-86-38    00-01-01-14-05-01");
                Packet.AddHexa("05    A4-05-BC-EF-4E-90-38    00-01-01-14-05-01");
                Packet.AddHexa("05    A5-05-8C-A8-4F-9A-38    00-01-01-14-05-01");
                Packet.AddHexa("05    A6-05-A6-E0-4F-A4-38    00-01-01-14-05-01");
                Packet.AddHexa("05    A7-05-8A-99-50-AE-38    00-01-01-14-05-01");
                Packet.AddHexa("05    A8-05-B8-D1-50-B8-38    00-01-01-14-05-01");
                Packet.AddHexa("05    A9-05-B0-8A-51-82-39    00-01-01-14-05-01");
                Packet.AddHexa("05    AA-05-B2-C3-51-8C-39    00-01-01-14-05-01");
                Packet.AddHexa("05    AB-05-BE-FC-51-96-39    00-01-01-14-05-01");
                Packet.AddHexa("05    AC-05-94-B6-52-A0-39    00-01-01-14-05-01");
                Packet.AddHexa("05    AD-05-B4-EF-52-AA-39    00-01-01-14-05-01");
                Packet.AddHexa("05    AE-05-9E-A9-53-B4-39    00-01-01-14-05-01");
                Packet.AddHexa("05    AF-05-92-E3-53-BE-39    00-01-01-14-05-01");
                Packet.AddHexa("05    B0-05-90-9D-54-88-3A    00-01-01-14-05-01");
                Packet.AddHexa("05    B1-05-98-D7-54-92-3A    00-01-01-14-05-01");
                Packet.AddHexa("05    B2-05-AA-91-55-9C-3A    00-01-01-14-05-01");
                Packet.AddHexa("05    B3-05-86-CC-55-A6-3A    00-01-01-14-05-01");
                Packet.AddHexa("05    B4-05-AC-86-56-B0-3A    00-01-01-14-05-01");
                Packet.AddHexa("05    B5-05-9C-C1-56-BA-3A    00-01-01-14-05-01");
                Packet.AddHexa("05    B6-05-96-FC-56-84-3B    00-01-01-14-05-01");
                Packet.AddHexa("05    B7-05-9A-B7-57-8E-3B    00-01-01-14-05-01");
                Packet.AddHexa("05    B8-05-A8-F2-57-98-3B    00-01-01-14-05-01");
                Packet.AddHexa("05    B9-05-80-AE-58-A2-3B    00-01-01-14-05-01");
                Packet.AddHexa("05    BA-05-A2-E9-58-AC-3B    00-01-01-14-05-01");
                Packet.AddHexa("05    BB-05-8E-A5-59-B6-3B    00-01-01-14-05-01");
                Packet.AddHexa("05    BC-05-84-E1-59-80-3C    00-01-01-14-05-01");
                Packet.AddHexa("05    BD-05-84-9D-5A-8A-3C    00-01-01-14-05-01");
                Packet.AddHexa("05    BE-05-8E-D9-5A-94-3C    00-01-01-14-05-01");
                Packet.AddHexa("05    BF-05-A2-95-5B-9E-3C    00-01-01-14-05-01");
                Packet.AddHexa("05    80-06-80-D2-5B-A8-3C    00-01-01-14-05-01");
                Packet.AddHexa("05    81-06-A8-8E-5C-B2-3C    00-01-01-14-05-01");
                Packet.AddHexa("05    82-06-9A-CB-5C-BC-3C    00-01-01-14-05-01");
                Packet.AddHexa("05    83-06-96-88-5D-86-3D    00-01-01-14-05-01");
                Packet.AddHexa("05    84-06-9C-C5-5D-90-3D    00-01-01-14-05-01");
                Packet.AddHexa("05    85-06-AC-82-5E-9A-3D    00-01-01-14-05-01");
                Packet.AddHexa("05    86-06-86-C0-5E-A4-3D    00-01-01-14-05-01");
                Packet.AddHexa("05    87-06-AA-FD-5E-AE-3D    00-01-01-14-05-01");
                Packet.AddHexa("05    88-06-98-BB-5F-B8-3D    00-01-01-14-05-01");
                Packet.AddHexa("05    89-06-90-F9-5F-82-3E    00-01-01-14-05-01");
                Packet.AddHexa("05    8A-06-92-B7-60-8C-3E    00-01-01-14-05-01");
                Packet.AddHexa("05    8B-06-9E-F5-60-96-3E    00-01-01-14-05-01");
                Packet.AddHexa("05    8C-06-B4-B3-61-A0-3E    00-01-01-14-05-01");
                Packet.AddHexa("05    8D-06-94-F2-61-AA-3E    00-01-01-14-05-01");
                Packet.AddHexa("05    8E-06-BE-B0-62-B4-3E    00-01-01-14-05-01");
                Packet.AddHexa("05    8F-06-B2-EF-62-BE-3E    00-01-01-14-05-01");
                Packet.AddHexa("05    90-06-B0-AE-63-88-3F    00-01-01-14-05-01");
                Packet.AddHexa("05    91-06-B8-ED-63-92-3F    00-01-01-14-05-01");
                Packet.AddHexa("05    92-06-8A-AD-64-9C-3F    00-01-01-14-05-01");
                Packet.AddHexa("05    93-06-A6-EC-64-A6-3F    00-01-01-14-05-01");
                Packet.AddHexa("05    94-06-8C-AC-65-B0-3F    00-01-01-14-05-01");
                Packet.AddHexa("05    95-06-BC-EB-65-BA-3F    00-01-01-14-05-01");
                Packet.AddHexa("05    96-06-B6-AB-66-84-40    00-01-01-14-05-01");
                Packet.AddHexa("05    97-06-BA-EB-66-8E-40    00-01-01-14-05-01");
                Packet.AddHexa("05    98-06-88-AC-67-98-40    00-01-01-14-05-01");
                Packet.AddHexa("05    99-06-A0-EC-67-A2-40    00-01-01-14-05-01");
                Packet.AddHexa("05    9A-06-82-AD-68-AC-40    00-01-01-14-05-01");
                Packet.AddHexa("05    9B-06-AE-ED-68-B6-40    00-01-01-14-05-01");
                Packet.AddHexa("05    9C-06-A4-AE-69-80-41    00-01-01-14-05-01");
                Packet.AddHexa("05    9D-06-A4-EF-69-8A-41    00-01-01-14-05-01");
                Packet.AddHexa("05    9E-06-AE-B0-6A-94-41    00-01-01-14-05-01");
                Packet.AddHexa("05    9F-06-82-F2-6A-9E-41    00-01-01-14-05-01");
                Packet.AddHexa("05    A0-06-A0-B3-6B-A8-41    00-01-01-14-05-01");
                Packet.AddHexa("05    A1-06-88-F5-6B-B2-41    00-01-01-14-05-01");
                Packet.AddHexa("05    A2-06-BA-B6-6C-BC-41    00-01-01-14-05-01");
                Packet.AddHexa("05    A3-06-B6-F8-6C-86-42    00-01-01-14-05-01");
                Packet.AddHexa("05    A4-06-BC-BA-6D-90-42    00-01-01-14-05-01");
                Packet.AddHexa("05    A5-06-8C-FD-6D-9A-42    00-01-01-14-05-01");
                Packet.AddHexa("05    A6-06-A6-BF-6E-A4-42    00-01-01-14-05-01");
                Packet.AddHexa("05    A7-06-8A-82-6F-AE-42    00-01-01-14-05-01");
                Packet.AddHexa("05    A8-06-B8-C4-6F-B8-42    00-01-01-14-05-01");
                Packet.AddHexa("05    A9-06-B0-87-70-82-43    00-01-01-14-05-01");
                Packet.AddHexa("05    AA-06-B2-CA-70-8C-43    00-01-01-14-05-01");
                Packet.AddHexa("05    AB-06-BE-8D-71-96-43    00-01-01-14-05-01");
                Packet.AddHexa("05    AC-06-94-D1-71-A0-43    00-01-01-14-05-01");
                Packet.AddHexa("05    AD-06-B4-94-72-AA-43    00-01-01-14-05-01");
                Packet.AddHexa("05    AE-06-9E-D8-72-B4-43    00-01-01-14-05-01");
                Packet.AddHexa("05    AF-06-92-9C-73-BE-43    00-01-01-14-05-01");
                Packet.AddHexa("05    B0-06-90-E0-73-88-44    00-01-01-14-05-01");
                Packet.AddHexa("05    B1-06-98-A4-74-92-44    00-01-01-14-05-01");
                Packet.AddHexa("05    B2-06-AA-E8-74-9C-44    00-01-01-14-05-01");
                Packet.AddHexa("05    B3-06-86-AD-75-A6-44    00-01-01-14-05-01");
                Packet.AddHexa("05    B4-06-AC-F1-75-B0-44    00-01-01-14-05-01");
                Packet.AddHexa("05    B5-06-9C-B6-76-BA-44    00-01-01-14-05-01");
                Packet.AddHexa("05    B6-06-96-FB-76-84-45    00-01-01-14-05-01");
                Packet.AddHexa("05    B7-06-9A-C0-77-8E-45    00-01-01-14-05-01");
                Packet.AddHexa("05    B8-06-A8-85-78-98-45    00-01-01-14-05-01");
                Packet.AddHexa("05    B9-06-80-CB-78-A2-45    00-01-01-14-05-01");
                Packet.AddHexa("05    BA-06-A2-90-79-AC-45    00-01-01-14-05-01");
                Packet.AddHexa("05    BB-06-8E-D6-79-B6-45    00-01-01-14-05-01");
                Packet.AddHexa("05    BC-06-84-9C-7A-80-46    00-01-01-14-05-01");
                Packet.AddHexa("05    BD-06-84-E2-7A-8A-46    00-01-01-14-05-01");
                Packet.AddHexa("05    BE-06-8E-A8-7B-94-46    00-01-01-14-05-01");
                Packet.AddHexa("05    BF-06-A2-EE-7B-9E-46    00-01-01-14-05-01");
                Packet.AddHexa("05    80-07-80-B5-7C-A8-46    00-01-01-14-05-01");
                Packet.AddHexa("05    81-07-A8-FB-7C-B2-46    00-01-01-14-05-01");
                Packet.AddHexa("05    82-07-9A-C2-7D-BC-46    00-01-01-14-05-01");
                Packet.AddHexa("05    83-07-96-89-7E-86-47    00-01-01-14-05-01");
                Packet.AddHexa("05    84-07-9C-D0-7E-90-47    00-01-01-14-05-01");
                Packet.AddHexa("05    85-07-AC-97-7F-9A-47    00-01-01-14-05-01");
                Packet.AddHexa("05    86-07-86-DF-7F-A4-47    00-01-01-14-05-01");

                Packet.AddHexa("05    87-07-AA-A6-80-01-AE-47    00-01-01-14-05-01");
                Packet.AddHexa("05    88-07-98-EE-80-01-B8-47    00-01-01-14-05-01");
                Packet.AddHexa("05    89-07-90-B6-81-01-82-48    00-01-01-14-05-01");
                Packet.AddHexa("05    8A-07-92-FE-81-01-8C-48    00-01-01-14-05-01");
                Packet.AddHexa("05    8B-07-9E-C6-82-01-96-48    00-01-01-14-05-01");
                Packet.AddHexa("05    8C-07-B4-8E-83-01-A0-48    00-01-01-14-05-01");
                Packet.AddHexa("05    8D-07-94-D7-83-01-AA-48    00-01-01-14-05-01");
                Packet.AddHexa("05    8E-07-BE-9F-84-01-B4-48    00-01-01-14-05-01");
                Packet.AddHexa("05    8F-07-B2-E8-84-01-BE-48    00-01-01-14-05-01");
                Packet.AddHexa("05    90-07-B0-B1-85-01-88-49    00-01-01-14-05-01");
                Packet.AddHexa("05    91-07-B8-FA-85-01-92-49    00-01-01-14-05-01");
                Packet.AddHexa("05    92-07-8A-C4-86-01-9C-49    00-01-01-14-05-01");
                Packet.AddHexa("05    93-07-A6-8D-87-01-A6-49    00-01-01-14-05-01");
                Packet.AddHexa("05    94-07-8C-D7-87-01-B0-49    00-01-01-14-05-01");
                Packet.AddHexa("05    95-07-BC-A0-88-01-BA-49    00-01-01-14-05-01");
                Packet.AddHexa("05    96-07-B6-EA-88-01-84-4A    00-01-01-14-05-01");
                Packet.AddHexa("05    97-07-BA-B4-89-01-8E-4A    00-01-01-14-05-01");
                Packet.AddHexa("05    98-07-88-FF-89-01-98-4A    00-01-01-14-05-01");
                Packet.AddHexa("05    99-07-A0-C9-8A-01-A2-4A    00-01-01-14-05-01");
                Packet.AddHexa("05    9A-07-82-94-8B-01-AC-4A    00-01-01-14-05-01");
                Packet.AddHexa("05    9B-07-AE-DE-8B-01-B6-4A    00-01-01-14-05-01");
                Packet.AddHexa("05    9C-07-A4-A9-8C-01-80-4B    00-01-01-14-05-01");
                Packet.AddHexa("05    9D-07-A4-F4-8C-01-8A-4B    00-01-01-14-05-01");
                Packet.AddHexa("05    9E-07-AE-BF-8D-01-94-4B    00-01-01-14-05-01");
                Packet.AddHexa("05    9F-07-82-8B-8E-01-9E-4B    00-01-01-14-05-01");
                Packet.AddHexa("05    A0-07-A0-D6-8E-01-A8-4B    00-01-01-14-05-01");
                Packet.AddHexa("05    A1-07-88-A2-8F-01-B2-4B    00-01-01-14-05-01");
                Packet.AddHexa("05    A2-07-BA-ED-8F-01-BC-4B    00-01-01-14-05-01");
                Packet.AddHexa("05    A3-07-B6-B9-90-01-86-4C    00-01-01-14-05-01");
                Packet.AddHexa("05    A4-07-BC-85-91-01-90-4C    00-01-01-14-05-01");
                Packet.AddHexa("05    A5-07-8C-D2-91-01-9A-4C    00-01-01-14-05-01");
                Packet.AddHexa("05    A6-07-A6-9E-92-01-A4-4C    00-01-01-14-05-01");
                Packet.AddHexa("05    A7-07-8A-EB-92-01-AE-4C    00-01-01-14-05-01");
                Packet.AddHexa("05    A8-07-B8-B7-93-01-B8-4C    00-01-01-14-05-01");
                Packet.AddHexa("05    A9-07-B0-84-94-01-82-4D    00-01-01-14-05-01");
                Packet.AddHexa("05    AA-07-B2-D1-94-01-8C-4D    00-01-01-14-05-01");
                Packet.AddHexa("05    AB-07-BE-9E-95-01-96-4D    00-01-01-14-05-01");
                Packet.AddHexa("05    AC-07-94-EC-95-01-A0-4D    00-01-01-14-05-01");
                Packet.AddHexa("05    AD-07-B4-B9-96-01-AA-4D    00-01-01-14-05-01");
                Packet.AddHexa("05    AE-07-9E-87-97-01-B4-4D    00-01-01-14-05-01");
                Packet.AddHexa("05    AF-07-92-D5-97-01-BE-4D    00-01-01-14-05-01");
                Packet.AddHexa("05    B0-07-90-A3-98-01-88-4E    00-01-01-14-05-01");
                Packet.AddHexa("05    B1-07-98-F1-98-01-92-4E    00-01-01-14-05-01");
                Packet.AddHexa("05    B2-07-AA-BF-99-01-9C-4E    00-01-01-14-05-01");

                Packet.AddLong(this.Player.PlayerID);

                return Packet.ToArray();
            }
        }

        internal void Tick()
        {
            this.Player.Info.Tick();
        }
    }
}
    