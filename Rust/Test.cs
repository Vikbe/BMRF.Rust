using Network;
using Oxide.Core;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Oxide.Plugins
{

    [Info("Sweet stuff", "Vajk bajk", 1.0)]
    public class Test : RustPlugin
    {


        object OnPlayerDie(BasePlayer player, HitInfo info)
        {
            
            if (!storedData.Players.ContainsKey(player.userID)) {
                var newPlayer = new PlayerInfo(player);
                storedData.Players.Add(player.userID, newPlayer);

            }
           
            storedData.Players[player.userID].IncrementDeath();
            SaveFile();
            PrintToChat(player, "Saving your data to the file");
            return null;
        }


        class StoredData
        {
            public Dictionary<ulong,PlayerInfo> Players = new Dictionary<ulong, PlayerInfo>();

            public StoredData()
            {
            }
        }

        class PlayerInfo
        {
            public ulong UserId;
            public string Name;
            public int Kills;
            public int Deaths;

            public PlayerInfo()
            {
            }

            public PlayerInfo(BasePlayer player)
            {
                UserId = player.userID;
                Name = player.displayName;
                Kills = 0;
                Deaths = 0;
            } 

            public void IncrementDeath()
            {
                Deaths++;
                
            }

        }

        public void SaveFile()
        {
            Interface.Oxide.DataFileSystem.WriteObject("MyDataFile", storedData);
            
        }

        StoredData storedData;

        void Loaded()
        {
            storedData = Interface.Oxide.DataFileSystem.ReadObject<StoredData>("MyDataFile");
        }





    }
}
