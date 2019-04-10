namespace GL.Servers.SP.Logic.Slots
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq.Expressions;
    using System.Collections.Generic;
    using System.Collections.Concurrent;

    using GL.Servers.SP.Core;
    using GL.Servers.SP.Core.Database;
    using GL.Servers.SP.Core.Database.Models.MySQL;

    using GL.Servers.Logic.Enums;
    using GL.Servers.SP.Files;
    using GL.Servers.SP.Files.CSV_Helpers;
    using GL.Servers.SP.Logic;
    using GL.Servers.SP.Logic.Mode;
    using MongoDB.Bson;
    using MongoDB.Driver;

    using Newtonsoft.Json;

    using SResources = GL.Servers.SP.Core.Resources;

    internal class Players : ConcurrentDictionary<long, Player>
    {
        /// <summary>
        /// The settings for the <see cref="JsonConvert" /> class.
        /// </summary>
        private readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            TypeNameHandling            = TypeNameHandling.Auto,            MissingMemberHandling   = MissingMemberHandling.Ignore,
            DefaultValueHandling        = DefaultValueHandling.Include,     NullValueHandling       = NullValueHandling.Ignore,
            /*PreserveReferencesHandling  = PreserveReferencesHandling.All,*/   ReferenceLoopHandling   = ReferenceLoopHandling.Ignore,
            Converters                  = new List<JsonConverter> { new DataConverter() },
            Formatting                  = Formatting.None
        };

        private int Seed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Players"/> class.
        /// </summary>
        internal Players()
        {
            if (Constants.Database == DBMS.Mongo)
            {
                this.Seed = Mongo.PlayerSeed;
            }
            else this.Seed = MySQL_Backup.GetSeed("Players");
        }

        /// <summary>
        /// Adds the specified player.
        /// </summary>
        /// <param name="Player">The player.</param>
        internal void Add(Player Player)
        {
            if (this.ContainsKey(Player.PlayerID))
            {
                if (!this.TryUpdate(Player.PlayerID, Player, Player))
                {
                    Logging.Error(this.GetType(), "Unsuccessfuly updated the specified player to the dictionnary.");
                }
            }
            else
            {
                if (!this.TryAdd(Player.PlayerID, Player))
                {
                    Logging.Error(this.GetType(), "Unsuccessfuly added the specified player to the dictionnary.");
                }
            }
        }

        /// <summary>
        /// Removes the specified player.
        /// </summary>
        /// <param name="Player">The player.</param>
        internal void Remove(Player Player)
        {
            Player TmpPlayer;

            if (this.ContainsKey(Player.PlayerID))
            {
                if (!this.TryRemove(Player.PlayerID, out TmpPlayer))
                {
                    Logging.Error(this.GetType(), "Unsuccessfuly removed the specified player from the dictionnary.");
                }
                else
                {
                    if (!TmpPlayer.Equals(Player))
                    {
                        Logging.Error(this.GetType(), "We successfully removed a player from the list but the returned player was not equal to our player.");
                    }
                }
            }
            
            this.Save(Player);
        }

        /// <summary>
        /// Gets the player using the specified identifier in the specified database.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="HighID">The high identifier.</param>
        /// <param name="LowID">The low identifier.</param>
        /// <param name="DBMS">The DBMS.</param>
        /// <param name="Store">if set to <c>true</c> [store].</param>
        internal async Task<Player> GetAsync(int HighID, int LowID, bool Store = true, DBMS DBMS = Constants.Database)
        {
            long Id = (long) HighID << 32 | (uint) LowID;

            if (!this.TryGetValue(Id, out Player Player))
            {
                switch (DBMS)
                {
                    case DBMS.MySQL:
                    {
                        using (GBS_MySQL Database = new GBS_MySQL())
                        {
                            var Data = await Database.Players.FindAsync(HighID, LowID);

                            if (Data != null)
                            {
                                if (!string.IsNullOrEmpty(Data.Data))
                                {
                                    Player = JsonConvert.DeserializeObject<Player>(Data.Data, this.Settings);

                                    if (Player.AllianceLowID > 0)
                                    {
                                        // Player.Alliance = await Resources.Clans.GetAsync(Player.AllianceHighID, Player.AllianceLowID);
                                    }

                                    if (Store)
                                    {
                                        this.Add(Player);
                                    }
                                }
                                else
                                {
                                    Logging.Error(this.GetType(), "The data returned wasn't null but empty, at Get(" + HighID + ", " + LowID + ", MySQL, " + Store + ").");
                                }
                            }
                        }

                        break;
                    }

                    case DBMS.Redis:
                    {
                        string Data = await Redis.Players.StringGetAsync(HighID + "-" + LowID);

                        if (!string.IsNullOrEmpty(Data))
                        {
                            Player = JsonConvert.DeserializeObject<Player>(Data, this.Settings);

                            if (Player.AllianceLowID > 0)
                            {
                                // Player.Alliance = await Resources.Clans.GetAsync(Player.AllianceHighID, Player.AllianceLowID);
                            }

                            if (Store)
                            {
                                this.Add(Player);
                            }
                        }

                        break;
                    }

                    case DBMS.Both:
                    {
                        Player = await this.GetAsync(HighID, LowID, Store, DBMS.Redis);

                        if (Player == null)
                        {
                            Player = await this.GetAsync(HighID, LowID, Store, DBMS.MySQL);

                            if (Player != null)
                            {
                                this.Save(Player, DBMS.Redis);
                            }
                        }

                        break;
                    }

                    case DBMS.File:
                    {
                        if (File.Exists(Directory.GetCurrentDirectory() + "\\Saves\\Players\\" + HighID + "-" + LowID + ".json"))
                        {
                            string JSON = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Saves\\Players\\" + HighID + "-" + LowID + ".json");

                            if (!string.IsNullOrWhiteSpace(JSON))
                            {
                                Player = JsonConvert.DeserializeObject<Player>(JSON, this.Settings);

                                if (Player.AllianceLowID > 0)
                                {
                                    // Player.Alliance = await Resources.Clans.GetAsync(Player.AllianceHighID, Player.AllianceLowID);
                                }

                                if (Store)
                                {
                                    this.Add(Player);
                                }
                            }
                            else
                            {
                                Logging.Error(this.GetType(), "The data returned wasn't null but empty, at Get(" + HighID + ", " + LowID + ", File, " + Store + ").");
                            }
                        }

                        break;
                    }

                    case DBMS.Mongo:
                    {
                        Core.Database.Models.Mongo.Players Save = await Mongo.Players.Find(T => T.HighID == HighID && T.LowID == LowID).Limit(1).SingleOrDefaultAsync();

                        if (Save != null)
                        {
                            Player = JsonConvert.DeserializeObject<Player>(Save.Player.ToJson(), this.Settings);

                            if (Player.AllianceLowID > 0)
                            {
                                // Player.Alliance = await Resources.Clans.GetAsync(Player.AllianceHighID, Player.AllianceLowID);
                            }

                            if (Store)
                            {
                                this.Add(Player);
                            }
                        }

                        break;
                    }
                }
            }

            return Player;
        }

                /// <summary>
        /// Gets the player using the specified identifier in the specified database.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="HighID">The high identifier.</param>
        /// <param name="LowID">The low identifier.</param>
        /// <param name="DBMS">The DBMS.</param>
        /// <param name="Store">if set to <c>true</c> [store].</param>
        internal Player Get(int HighID, int LowID, bool Store = true, DBMS DBMS = Constants.Database)
        {
            long Id = (long) HighID << 32 | (uint) LowID;

            if (!this.TryGetValue(Id, out Player Player))
            {
                switch (DBMS)
                {
                    case DBMS.MySQL:
                    {
                        using (GBS_MySQL Database = new GBS_MySQL())
                        {
                            var Data = Database.Players.Find(HighID, LowID);

                            if (Data != null)
                            {
                                if (!string.IsNullOrEmpty(Data.Data))
                                {
                                    Player = JsonConvert.DeserializeObject<Player>(Data.Data, this.Settings);

                                    if (Player.AllianceLowID > 0)
                                    {
                                        // Player.Alliance = Resources.Clans.Get(Player.AllianceHighID, Player.AllianceLowID);
                                    }

                                    if (Store)
                                    {
                                        this.Add(Player);
                                    }
                                }
                                else
                                {
                                    Logging.Error(this.GetType(), "The data returned wasn't null but empty, at Get(" + HighID + ", " + LowID + ", MySQL, " + Store + ").");
                                }
                            }
                        }

                        break;
                    }

                    case DBMS.Redis:
                    {
                        string Data = Redis.Players.StringGet(HighID + "-" + LowID);

                        if (!string.IsNullOrEmpty(Data))
                        {
                            Player = JsonConvert.DeserializeObject<Player>(Data, this.Settings);

                            if (Player.AllianceLowID > 0)
                            {
                                // Player.Alliance = Resources.Clans.Get(Player.AllianceHighID, Player.AllianceLowID);
                            }

                            if (Store)
                            {
                                this.Add(Player);
                            }
                        }

                        break;
                    }

                    case DBMS.Both:
                    {
                        Player = this.Get(HighID, LowID, Store, DBMS.Redis);

                        if (Player == null)
                        {
                            Player = this.Get(HighID, LowID, Store, DBMS.MySQL);

                            if (Player != null)
                            {
                                this.Save(Player, DBMS.Redis);
                            }
                        }

                        break;
                    }

                    case DBMS.File:
                    {
                        if (File.Exists(Directory.GetCurrentDirectory() + "\\Saves\\Players\\" + HighID + "-" + LowID + ".json"))
                        {
                            string JSON = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Saves\\Players\\" + HighID + "-" + LowID + ".json");

                            if (!string.IsNullOrWhiteSpace(JSON))
                            {
                                Player = JsonConvert.DeserializeObject<Player>(JSON, this.Settings);

                                if (Player.AllianceLowID > 0)
                                {
                                    // Player.Alliance = Resources.Clans.Get(Player.AllianceHighID, Player.AllianceLowID);
                                }

                                if (Store)
                                {
                                    this.Add(Player);
                                }
                            }
                            else
                            {
                                Logging.Error(this.GetType(), "The data returned wasn't null but empty, at Get(" + HighID + ", " + LowID + ", File, " + Store + ").");
                            }
                        }

                        break;
                    }

                    case DBMS.Mongo:
                    {
                        Core.Database.Models.Mongo.Players Save = Mongo.Players.Find(T => T.HighID == HighID && T.LowID == LowID).Limit(1).SingleOrDefault();

                        if (Save != null)
                        {
                            Player = JsonConvert.DeserializeObject<Player>(Save.Player.ToJson(), this.Settings);

                            if (Player.AllianceLowID > 0)
                            {
                                // Player.Alliance = Resources.Clans.Get(Player.AllianceHighID, Player.AllianceLowID);
                            }

                            if (Store)
                            {
                                this.Add(Player);
                            }
                        }

                        break;
                    }
                }
            }

            return Player;
        }
        
        /// <summary>
        /// Creates a new player using the specified identifier in the specified database.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="HighID">The high identifier.</param>
        /// <param name="LowID">The low identifier.</param>
        /// <param name="DBMS">The DBMS.</param>
        /// <param name="Store">if set to <c>true</c> [store].</param>
        internal Player New(GameMode GameMode, int HighID = Constants.ServerID, int LowID = 0, DBMS DBMS = Constants.Database, bool Store = true)
        {
            Player Player = new Player(GameMode, HighID, Interlocked.Increment(ref this.Seed));

            Player.Home.LastSave = LevelFile.StartingHome;

            for (int i = 0; i < 40; i++)
            {
                char Letter      = (char) SResources.Random.Next('A', 'Z');
                Player.PassToken = Player.PassToken + Letter;
            }
            
            switch (DBMS)
            {
                case DBMS.MySQL:
                {
                    using (GBS_MySQL Database = new GBS_MySQL())
                    {
                        Database.Players.Add(new Core.Database.Models.MySQL.Players
                        {
                            HighID  = Player.HighID,
                            LowID   = Player.LowID,
                            Data    = JsonConvert.SerializeObject(Player, this.Settings)
                        });

                        Database.SaveChangesAsync();
                    }

                    if (Store)
                    {
                        this.Add(Player);
                    }

                    break;
                }

                case DBMS.Redis:
                {
                    this.Save(Player, DBMS);

                    if (Store)
                    {
                        this.Add(Player);
                    }

                    break;
                }

                case DBMS.Both:
                {
                    this.Save(Player, DBMS);

                    using (GBS_MySQL Database = new GBS_MySQL())
                    {
                        Database.Players.Add(new Core.Database.Models.MySQL.Players
                        {
                            HighID  = Player.HighID,
                            LowID   = Player.LowID,
                            Data    = JsonConvert.SerializeObject(Player, this.Settings)
                        });

                        Database.SaveChangesAsync();
                    }

                    if (Store)
                    {
                        this.Add(Player);
                    }

                    break;
                }

                case DBMS.File:
                {
                    if (!File.Exists(Directory.GetCurrentDirectory() + "\\Saves\\Players\\" + Player + ".json"))
                    {
                        File.WriteAllText(Directory.GetCurrentDirectory() + "\\Saves\\Players\\" + Player + ".json", JsonConvert.SerializeObject(Player, this.Settings));
                    }

                    break;
                }

                case DBMS.Mongo:
                {
                    Mongo.Players.InsertOne(new Core.Database.Models.Mongo.Players
                    {
                        HighID = Player.HighID,
                        LowID  = Player.LowID,
                        Player = BsonDocument.Parse(JsonConvert.SerializeObject(Player, this.Settings))
                    });

                    if (Store)
                    {
                        this.Add(Player);
                    }

                    break;
                }
            }

            return Player;
        }

        /// <summary>
        /// Saves the specified player in the specified database.
        /// </summary>
        /// <param name="Player">The player.</param>
        /// <param name="DBMS">The DBMS.</param>
        internal void Save(Player Player, DBMS DBMS = Constants.Database)
        {
            switch (DBMS)
            {
                case DBMS.MySQL:
                {
                    using (GBS_MySQL Database = new GBS_MySQL())
                    {
                        var Data        = Database.Players.Find(Player.HighID, Player.LowID);

                        if (Data != null)
                        {
                            Data.HighID = Player.HighID;
                            Data.LowID  = Player.LowID;
                            Data.Data   = JsonConvert.SerializeObject(Player, this.Settings);
                        }
                        else
                        {
                            Logging.Error(this.GetType(), "The database returned a null value when we tried to get a player.");
                        }

                        Database.SaveChangesAsync();
                    }

                    break;
                }

                case DBMS.Redis:
                {
                    Redis.Players.StringSetAsync(Player.ToString(), JsonConvert.SerializeObject(Player, this.Settings), TimeSpan.FromMinutes(30));
                    break;
                }

                case DBMS.Both:
                {
                    this.Save(Player, DBMS.MySQL);
                    this.Save(Player, DBMS.Redis);
                    break;
                }

                case DBMS.File:
                {
                    File.WriteAllText(Directory.GetCurrentDirectory() + "\\Saves\\Players\\" + Player + ".json", JsonConvert.SerializeObject(Player, this.Settings));
                    break;
                }

                case DBMS.Mongo:
                {
                    Mongo.Players.UpdateOne(T => T.HighID == Player.HighID && T.LowID == Player.LowID, Builders<Core.Database.Models.Mongo.Players>.Update.Set(T => T.Player, BsonDocument.Parse(JsonConvert.SerializeObject(Player, this.Settings))));
                    
                    break;
                }
            }
        }

        internal async Task<Player> FindPlayer(Expression<Func<Core.Database.Models.Mongo.Players, bool>> Match)
        {
            Core.Database.Models.Mongo.Players Result = await Mongo.Players.Find(Match).Limit(1).SingleOrDefaultAsync();

            if (Result != null)
            {
                if (Result.Player != null)
                {
                    return JsonConvert.DeserializeObject<Player>(Result.Player.ToJson(), this.Settings);
                }
            }

            return null;
        }

        internal void EditPlayers(Expression<Func<Core.Database.Models.Mongo.Players, bool>> Filters, UpdateDefinition<Core.Database.Models.Mongo.Players> Edit)
        {
            Mongo.Players.UpdateMany(Filters, Edit);
        }
        
        /// <summary>
        /// Saves the specified DBMS.
        /// </summary>
        /// <param name="DBMS">The DBMS.</param>
        internal void Save(DBMS DBMS = Constants.Database)
        {
            if (DBMS == DBMS.Both)
            {
                DBMS = DBMS.MySQL;
            }

            Player[] Players = this.Values.ToArray();

            Parallel.ForEach(Players, Player =>
            {
                try
                {
                    this.Save(Player, DBMS);
                }
                catch (Exception Exception)
                {
                    Console.WriteLine(Exception);
                    Logging.Error(this.GetType(), Exception.GetType().Name + ", did not successed to save a player at shutdown.");
                }
            });
            
            Logging.Info(this.GetType(), Players.Length + " players saved.");
        }
    }
}