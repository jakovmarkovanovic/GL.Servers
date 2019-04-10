﻿namespace GL.Servers.CoC.Packets.Messages.Server.Home
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Enums;

    using GL.Servers.Extensions.List;

    internal class Own_Home_Data_Message : Message
    {
        private int Timestamp;
        private int SecondsSinceLastSave;

        private Home Home;
        private Player Player;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24101;
            }
        }

        /// <summary>
        /// Gets a value indicating the service node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Avatar;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Own_Home_Data_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Own_Home_Data_Message(Device Device, Home Home, Player Player, int Timestamp, int SecondsSinceLastSave) : base (Device)
        {
            this.Home       = Home;
            this.Player     = Player;
            this.Timestamp  = Timestamp;
            this.SecondsSinceLastSave = SecondsSinceLastSave;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(this.SecondsSinceLastSave);
            this.Data.AddInt(-1);
            this.Data.AddInt(this.Timestamp);

            this.Data.AddRange("00-00-00-00-01-84-95-61-00-03-F2-74-00-00-07-08-00-04-2A-B4-01-00-00-07-D1-4D-26-00-00-78-9C-95-5A-6D-6F-DB-36-10-FE-2F-FA-EC-02-E2-8B-28-2A-1F-D7-62-C0-80-7D-18-D6-61-28-30-04-82-62-2B-A9-57-45-32-64-D9-69-10-E4-BF-8F-12-A9-F8-5E-18-33-8B-9B-C6-90-1E-3E-3A-DE-1D-8F-77-47-BD-64-ED-CF-43-7D-6E-C7-EC-46-6C-B2-A6-DF-8D-C3-7E-57-6F-BB-7D-DB-4F-D9-CD-34-9E-5A-77-75-3B-ED-CF-6D-DD-35-CF-C3-C9-5D-CC-97-2B-75-27-97-AF-FE-6A-7D-9C-9A-A9-CD-6E-FE-C9-37-E8-73-8B-01-F2-1A-62-3B-0C-DD-6E-78-EA-A3-98-BB-D3-BE-DB-ED-FB-87-A3-BB-FB-92-ED-9A-A9-71-02-E7-F3-8F-13-7B-BF-CB-6E-8A-3C-FC-38-BE-73-B7-88-F6-33-BB-91-7A-93-3D-BB-3F-EA-75-83-47-69-3C-4A-E0-51-6A-19-25-AA-4D-36-B6-C7-7A-DA-3F-BA-A9-D9-C2-56-94-25-C7-2C-12-B3-54-FE-D9-EE-EA-A9-DF-4F-B3-E4-B7-84-40-14-98-40-61-02-11-17-5E-10-E1-F5-65-54-37-6C-7F-B4-BB-D5-70-33-87-5D-38-54-F1-EA-94-38-DC-39-2B-6C-BB-16-2A-D1-2E-14-E5-CA-A8-D6-79-B9-B1-C6-6B-C1-5C-1E-1F-05-8B-05-2C-C2-83-18-DA-62-B4-5C-D0-E5-02-AE-28-B6-C0-58-B5-60-55-B1-80-0B-0A-36-18-AC-A1-18-5A-53-74-8E-D1-05-34-34-03-13-99-0D-A4-96-4C-10-22-75-B9-A0-B5-A7-96-22-A1-3D-0B-F4-C1-E4-50-18-5B-79-7D-78-9F-16-92-A2-35-42-0B-B4-02-38-1A-CF-51-78-2B-2A-2F-88-64-92-E0-39-0A-89-D4-C7-B8-B1-DC-22-D8-51-7A-0F-29-13-DC-DE-90-DE-E8-8A-51-13-B0-B7-A3-F0-2B-45-A8-04-DA-1B-32-C4-84-9C-82-25-06-97-50-0E-26-34-01-7B-33-DA-77-B4-47-C0-DE-8E-B3-81-3E-B0-62-64-0E-E4-D0-6C-86-02-83-05-D4-07-F3-54-02-F6-56-14-7E-9D-2B-9B-40-2B-28-B5-4E-F8-B5-D4-40-D5-29-57-95-C1-8A-DE-41-24-53-08-41-1B-A8-6B-16-43-B0-EF-C9-B0-1A-F3-0F-AD-5D-69-E1-2A-E0-1A-21-68-6F-47-ED-C5-16-CC-EA-58-7F-CA-DB-51-78-6E-91-D0-B6-12-90-9B-8A-2D-B0-43-A9-B0-1C-83-D8-D4-47-28-DA-1B-52-7B-1F-A1-72-08-1C-28-95-86-7B-19-B3-3A-45-17-70-8E-54-21-02-9B-46-19-38-45-E6-D9-14-5D-42-67-65-1E-25-B0-8F-28-6F-48-A1-E2-4B-9D-A2-2B-38-49-C1-02-03-DE-69-74-0E-03-BC-62-DC-D8-90-DA-1B-52-9A-78-1C-A1-68-64-49-CD-24-C1-EA-D6-21-B0-E6-EF-B8-2B-41-6B-94-11-A4-D0-05-D4-09-F3-29-B2-DC-B5-41-B3-4C-04-1E-ED-6D-A9-BC-07-2A-E6-27-D8-3A-DA-22-74-62-47-D0-68-51-72-49-30-BA-C8-51-08-CC-E7-2C-69-1A-9B-C3-92-AC-6D-B2-5D-BB-1D-C2-D7-F3-70-F7-2F-CC-9B-54-85-95-F6-A6-43-98-C0-85-CC-05-4C-30-0C-93-78-18-4E-3F-C3-06-1F-19-A6-F1-30-9C-6F-1A-B9-66-1C-B7-4B-DE-7A-68-9E-FA-BF-9B-D1-09-FD-92-1D-DD-44-FA-DD-F1-D7-71-78-FC-BD-39-4E-7F-FA-BB-B3-17-BC-41-BF-B6-73-E2-F8-49-0A-69-0B-55-9A-E2-92-2E-7E-EE-DA-66-FC-3C-9C-FA-69-2E-13-DC-C3-E6-84-B8-9E-86-FA-A1-7D-BC-1B-7E-D6-AE-66-38-CC-1A-17-62-16-61-B9-B9-EF-D7-9B-87-76-DC-0F-BB-39-E0-29-93-3B-7F-5E-73-E1-97-E5-4B-7D-18-E7-9B-2F-AF-EE-C6-F1-D0-76-5D-F4-CE-5B-F2-2F-69-F6-AF-48-2A-1C-D5-BF-04-2E-23-D6-08-08-47-99-CB-A8-A7-DF-96-BC-F7-E9-8F-A5-1E-7A-FA-F6-66-93-90-8B-DB-04-55-C9-A9-BE-2D-54-A0-20-48-71-D8-28-87-8F-0C-4A-7D-8C-A3-8A-72-A0-78-91-E2-10-79-94-43-C3-64-2E-C5-A1-88-6A-55-54-B7-9A-A5-4D-51-AE-32-C2-F5-A6-DC-95-24-61-6B-65-E3-24-12-91-98-04-49-15-27-51-88-84-96-6D-84-44-E7-71-12-8D-48-0A-4A-82-2B-C6-79-99-BE-5F-FB-05-F7-37-B0-8C-CD-09-A1-46-35-AC-10-57-09-43-A6-63-37-68-89-66-8F-8B-11-96-4B-D3-F3-61-79-08-7D-8A-C6-4F-31-57-9E-B2-BA-27-73-87-0A-73-94-57-38-C2-C6-AF-68-C5-5E-28-CC-61-AF-70-84-0D-5E-50-39-B4-C1-1C-D5-35-8D-E5-71-87-D2-12-71-C8-FC-1A-47-48-79-DC-7C-4F-07-E9-43-22-56-4C-89-C9-E4-35-B2-2A-D2-DA-10-A5-B6-D4-D1-0A-8B-49-D5-15-D2-20-A0-C6-7E-06-1B-0E-92-76-1C-34-CE-49-43-13-41-BD-93-53-E9-0A-A3-C3-6A-0D-25-00-CD-A9-0A-52-EB-2B-94-D4-A7-D0-68-FD-D1-52-84-82-0B-98-52-B1-C2-85-A2-7D-92-A4-E2-A9-B4-C6-39-52-68-23-98-78-8D-48-C1-16-E6-DD-2C-B5-23-E8-50-EB-AF-BD-31-5A-41-51-B4-84-E5-02-4B-D6-28-5A-C1-B5-93-A2-46-79-20-35-0C-05-87-39-C6-BB-41-04-1C-8A-E6-F2-23-C4-B8-B0-65-ED-2E-8A-D6-C0-88-2A-31-C1-B5-B0-35-F1-24-9A-A2-4B-A4-E9-84-15-D7-5A-55-C7-13-5D-8D-13-F4-50-22-AE-31-8D-26-D1-14-AD-A0-5F-B3-FE-0E-45-6B-D8-74-60-0B-8C-A2-BD-4E-AA-78-AB-84-82-61-C7-86-29-04-47-05-85-1A-36-6C-79-11-B0-85-3D-3D-16-13-48-C5-57-40-D7-E3-11-84-A0-C3-32-AF-E2-AA-A6-E8-12-2A-8F-55-AA-45-B4-BA-09-DB-0A-E7-C6-DA-2B-04-D4-08-55-1F-05-4B-E0-D8-32-C5-1C-D6-8C-8E-2E-73-0A-D6-30-00-53-8B-53-70-58-04-A1-73-C4-74-4D-D0-16-D5-E2-29-EE-0A-B5-27-98-AE-31-DA-08-B4-85-53-8D-18-BC-1C-8D-44-D5-35-95-DB-E0-FD-CE-28-54-5D-D3-C5-4B-D1-1A-D6-B4-6C-CD-50-74-01-C3-3B-A3-C6-7B-92-31-C8-FD-98-D8-04-8D-9C-95-B5-6C-28-DA-42-53-32-A9-09-B8-02-61-81-B5-60-0A-BC-7A-4B-18-DE-F9-FE-4F-C0-02-46-EC-14-58-42-C7-66-33-A4-68-05-D3-16-B6-A5-53-B4-46-FA-60-CE-4A-D0-06-9D-23-B0-35-46-D0-A8-CF-99-12-DB-C2-74-9E-C5-05-02-AE-D0-F9-CE-75-B0-CD-61-10-49-58-D1-A2-3C-44-53-4F-25-81-D2-86-B0-1A-92-CD-14-BA-84-53-E4-81-81-A0-2D-2C-98-78-60-20-E8-0A-7A-5F-22-BE-57-E8-30-90-47-6C-DC-EC-AF-04-2A-81-52-68-09-C5-66-0D-68-8A-0E-31-BB-8A-37-A0-89-71-2A-0D-53-17-BE-C6-70-F4-AB-50-E3-97-45-A8-4B-D9-E5-D1-E8-40-6D-E9-33-3C-9E-DD-30-BA-65-E3-3E-60-55-42-77-99-63-50-74-14-95-0C-6D-F4-F2-BD-67-19-22-61-D8-67-45-DC-D9-30-5A-84-F3-D2-B5-3C-4E-A1-83-DB-F3-F2-D2-AE-C1-1E-A2-25-E2-A6-21-86-A2-15-92-9B-A1-89-24-1A-49-C2-E4-26-DC-05-92-84-E6-C9-14-6D-90-B7-31-7D-13-74-89-D0-6C-63-21-68-8B-66-C9-32-76-E4-F7-22-D4-02-6A-ED-86-24-D0-A8-12-E5-C5-11-41-4B-C4-CD-92-70-82-46-07-DA-92-95-03-04-AD-61-2C-48-CE-B2-80-B5-14-CB-9B-28-DA-C0-2E-1E-43-E3-BD-59-E0-43-34-26-49-41-D0-16-D5-53-6C-C7-10-18-5D-81-0C-9F-53-63-87-55-70-DF-E7-52-63-E6-70-84-F6-CE-81-11-2E-34-C4-5A-1F-05-85-54-6F-07-01-12-9C-04-48-70-14-20-F9-59-C0-DB-E3-63-67-01-21-A3-28-80-3A-C2-30-85-87-E1-B3-00-BF-20-0A-33-CB-73-96-73-33-7F-AE-5C-E6-6F-4B-BB-5E-CC-D5-F9-59-6E-B7-B0-3B-FF-70-6C-B7-1E-37-7F-9D-61-EE-EA-FA-76-4D-38-CE-E8-DB-A7-AF-DF-87-C3-2F-E0-B5-9A-F9-55-1B-E1-3E-EB-FF-12-BD-84-A3-37-F9-FF-FC-88-F0-7B-F5-73-11-E5-AF-70-EC-F2-E1-01-5F-C2-E1-8C-08-B2-25-9F-F5-01-89-D9-2C-E7-26-D6-FD-FD-AC-D9-17-FF-25-BC-C6-93-75-CD-71-AA-BB-B6-79-38-B5-F5-D8-F4-3F-C2-CB-50-EE-62-D3-75-FB-A6-DF-B6-EE-EE-B9-ED-96-D6-28-04-1F-BF-9F-EE-EF-BB-F6-82-3F-B6-CD-71-E8-DD-9F-B6-CF-6E-3E-AD-68-37-C9-63-B8-26-E6-CA-E9-A9-19-EB-E9-34-0D-E3-BE-E9-D6-1B-B9-BF-7C-D7-1C-1D-DD-BD-BB-3E-BF-AC-35-8E-75-E4-E2-E3-73-DD-37-8F-CB-9B-3F-59-B6-59-FF-DD-CE-AF-72-2D-BE-53-DF-77-CD-EC-05-8E-F2-CE-79-4A-78-D9-AB-6E-77-7B-27-E0-77-E7-37-75-3B-42-46-37-B5-7E-79-A5-49-E6-CA-B1-CC-C4-F9-EB-7F-CD-A1-80-88-00-01-00-00-01-ED-1F-04-00-00-78-9C-9D-53-6B-6F-9B-30-14-FD-2F-FE-CC-5A-1B-5C-02-F9-56-48-AA-21-D1-50-B5-74-93-B6-56-C8-D8-4E-67-89-D8-11-36-49-BB-6A-FF-7D-D7-21-AA-9A-64-DA-0B-C9-0F-EE-3D-F7-E8-5C-1F-FB-15-C9-8D-D4-CE-A2-E9-D7-57-E4-5E-D6-12-4D-71-80-94-40-53-12-46-01-DA-C8-DE-2A-A3-D1-34-84-BD-B2-AA-ED-64-AD-56-00-42-21-26-13-82-09-AE-F1-04-C3-77-06-E3-0B-0A-90-75-AC-77-87-10-7A-04-91-5A-1C-02-E2-23-00-EF-98-FD-96-99-E7-B9-76-FD-CB-82-ED-A0-BE-CE-0B-7D-1F-AB-7B-63-D6-33-65-B9-19-B4-FB-08-93-00-94-D2-ED-BE-B2-F0-4D-04-48-1B-A7-96-8A-33-07-7D-D4-BE-31-54-17-B3-A6-AC-F2-CB-B2-59-54-75-71-55-E4-97-75-51-2D-9A-F9-A7-F9-A2-6E-EA-DB-AA-BA-69-66-C5-5D-5E-DD-2F-6A-4F-B8-62-4F-7B-05-96-8F-6B-67-38-EB-D4-F7-1D-E5-18-59-0E-9A-FB-BF-F1-1C-F5-A8-EF-DE-CA-9D-44-C8-AF-59-0F-31-07-A7-09-00-44-7D-B7-64-02-F1-08-46-88-FD-C6-4F-04-63-F4-F8-23-78-23-38-68-F0-37-2C-C4-97-ED-0A-47-07-E9-E8-60-88-FF-C6-41-F2-67-07-8F-21-27-0E-26-FF-E9-60-3E-58-67-56-77-79-69-98-50-FA-E9-C4-BE-0F-FF-EE-5F-36-A8-4E-C8-3E-63-56-36-B7-72-CB-7A-61-BD-D6-FE-17-4E-3E-9C-5F-44-2C-4D-49-4B-A9-58-8A-24-24-2D-8F-2E-62-8A-E3-89-A0-71-4C-65-1A-37-59-D6-94-C6-B8-2B-69-DD-19-D4-9C-58-FF-70-4E-B9-90-2C-66-09-E6-93-30-4A-D3-44-24-04-C7-21-65-31-4D-79-94-44-A2-69-47-41-4D-EB-15-75-40-B6-F4-64-DC-6E-8E-AE-CD-A3-27-37-AE-54-2B-E5-72-63-3A-61-B6-BA-D0-D7-4A-0F-4E-42-9E-26-F0-2E-C5-20-BB-CC-E8-C1-DE-C8-9E-C3-59-7E-56-20-02-6E-CD-69-A6-34-76-7C-CA-C7-89-59-CF-B6-87-89-6B-F6-3C-53-6C-65-B4-C8-8D-75-7B-18-9A-46-11-5C-A9-9F-B4-EA-46-79-01-00-00-01-53-EC-03-00-00-78-9C-8D-93-51-4F-C2-30-14-85-FF-8A-B9-CF-7B-E8-06-03-DC-A3-4C-D1-64-44-22-46-1F-0C-0F-65-5C-47-63-E9-35-6B-11-0C-D9-7F-B7-9B-4B-60-5B-E3-7C-BE-5F-4F-4F-CF-B9-3D-C1-4C-D2-9A-4B-0D-D1-09-E2-3D-CA-1B-52-7B-3D-E7-C7-58-F0-1D-A9-CD-94-B4-59-60-9E-A2-32-10-F9-8C-79-30-13-EF-66-C1-D3-8F-DB-A3-41-A5-05-29-88-E0-0A-BC-F3-E1-1A-8F-73-7E-80-88-5D-0C-12-B1-13-E6-55-A8-92-88-F9-37-44-83-DF-61-42-64-AA-D9-94-48-6E-E8-A0-1E-D4-5C-A8-BD-41-6B-CA-1F-04-AC-AB-9D-90-C6-A6-76-3D-B0-EA-95-CD-C2-83-17-21-25-CF-30-28-1F-B6-34-39-AA-CC-6C-9F-B8-CA-F0-8E-F2-65-4A-B9-15-78-3B-41-7D-CE-82-10-4D-AC-E0-5C-48-D4-86-54-29-5F-78-7F-CE-03-D6-21-AA-80-2E-90-A1-03-09-9A-C8-C8-81-0C-9B-C8-C4-81-8C-9A-88-BD-B8-CB-B4-FC-FA-0E-C3-41-CB-B0-EF-70-3C-6C-33-0E-CB-A3-36-E3-F2-CC-58-27-40-27-D4-7D-1A-2B-56-1E-3C-DB-CD-B8-E7-52-DA-E5-4C-F0-0B-25-44-63-0F-AA-26-A7-DB-BA-57-BB-17-42-65-9D-62-7B-7A-1D-F4-96-16-86-7D-85-8C-FB-B3-9E-84-BD-31-5E-87-FF-09-A8-C9-94-76-57-E7-7D-F7-AB-7D-FF-C4-54-70-F9-B8-D6-86-A7-12-AB-1F-5A-14-3F-B9-5A-49-CF-00-00-00-00-01-84-95-61-00-00-00-00-01-84-95-61-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-FF-FF-FF-FF-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-06-47-69-73-65-6D-69-FF-FF-FF-FF-00-00-00-01-00-00-00-00-00-00-01-F4-00-00-01-F4-00-00-04-B0-00-00-00-3C-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-01-00-00-00-DC-6C-F5-EB-48-00-FF-FF-FF-FF-00-00-00-00-00-00-00-00-00-00-00-01-00-00-00-00-00-00-00-00-00-00-00-00-08-00-2D-C6-C1-00-00-03-E8-00-2D-C6-C2-00-00-03-E8-00-2D-C6-C3-00-00-00-00-00-2D-C6-C4-00-00-03-E8-00-2D-C6-C5-00-00-03-E8-00-2D-C6-C6-00-00-00-00-00-2D-C6-C7-00-00-C3-50-00-2D-C6-C8-00-00-C3-50-00-00-00-04-00-2D-C6-C1-00-00-02-EE-00-2D-C6-C2-00-00-02-EE-00-2D-C6-C7-00-00-13-88-00-2D-C6-C8-00-00-13-88-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-03-01-5E-F3-C6-00-00-00-01-01-5E-F3-C7-00-00-00-01-01-5E-F3-C8-00-00-00-01-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-02-02-34-93-42-00-00-00-00-02-34-93-4E-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-01-5F-20-5B-20-E0-00-00-01-5F-20-5B-20-E0-00-00-01-5F-20-76-98-20-00-00-00-00".HexaToBytes());

            return;

            this.Home.Encode(this.Data);
            this.Player.Encode(this.Data);

            this.Data.AddInt(0);
            this.Data.AddInt(0);

            {
                this.Data.AddInt(345);
                this.Data.AddInt(896691880);
            }
            {
                this.Data.AddInt(345);
                this.Data.AddInt(896691880);
            }
            {
                this.Data.AddInt(345);
                this.Data.AddInt(896691880);
            }

            this.Data.AddInt(0);
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            this.Device.GameMode.LoadHomeState(this.Home, this.Player, this.Timestamp, this.SecondsSinceLastSave);
        }
    }
}
