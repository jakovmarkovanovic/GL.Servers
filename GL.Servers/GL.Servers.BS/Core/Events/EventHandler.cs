﻿namespace GL.Servers.BS.Core.Events
{
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    using GL.Servers.BS.Core.Network;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Packets.Messages.Server;
    using GL.Servers.Logic.Enums;

    internal class EventsHandler
    {
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler Handler, bool Enabled);

        private static EventHandler EHandler;
        private delegate void EventHandler();

        /// <summary>
        /// Initializes a new instance of the <see cref="EventsHandler"/> class.
        /// </summary>
        internal static void Initialize()
        {
            EventsHandler.EHandler += EventsHandler.Process;
            EventsHandler.SetConsoleCtrlHandler(EventsHandler.EHandler, true);
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal static void Process()
        {
            Console.WriteLine("- SHUTDOWN -");

            Logging.Error(typeof(EventHandler), "Server is shutting down, warning player about the maintenance...");

            Task WTask = Task.Run(() =>
            {
                foreach (Player Player in Resources.Players.Values.ToArray())
                {
                    if (Player.Device != null && Player.Device.State >= State.LOGGED)
                    {
                        new Server_Shutdown(Player.Device).Send();
                    }
                }

                Logging.Error(typeof(EventHandler), "Warned every players about the maintenance.");
            });

            bool SavePlayers = false;
            bool SaveClans = false;
            bool Faster = true;

            if (Resources.Players.Count > 0)
            {
                SavePlayers = true;
            }

            if (Resources.Clans.Count > 0)
            {
                SaveClans = true;
            }

            if (SavePlayers || SaveClans)
            {
                Task PTask = new Task(() => Resources.Players.Save());
                Task CTask = new Task(() => Resources.Clans.Save());

                if (SavePlayers)
                    PTask.Start();
                if (SaveClans)
                    CTask.Start();


                if (SavePlayers)
                    PTask.Wait();

                if (SaveClans)
                    CTask.Wait();

                Faster = false;
            }
            else
            {
                Logging.Error(typeof(EventHandler), "Server has nothing to save, shutting down immediatly.");
            }

            Task STask = Task.Run(() =>
            {
                foreach (Player Player in Resources.Players.Values.ToArray())
                {
                    Player.Device.Socket.Close();
                }
            });

            WTask.Wait(5000);
            STask.Wait(10000);

            // Thread.Sleep(5000 - (Faster ? 3000 : 0));

            Environment.Exit(0);
        }
    }
}