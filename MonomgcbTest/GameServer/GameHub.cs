using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.Xna.Framework;
using System.Timers;
using Shared;

namespace GameServer
{
    public class GameHub : Hub
    {
        bool playing = false;

        List<Player> players = new List<Player>()
        {
            new Player
            {
                playerID = Guid.NewGuid(),
                FirstName ="First",
                SecondName ="Player",
                GamerTag = "player 1",
                UserName = "p1",
                XP = 400,
                scale = 1 
            }
        };

        List<Player> joined = new List<Player>();

        public GameHub()
        {
        }

        public Player join(string username)
        {
            if (!playing)
            {
                Player p = players.FirstOrDefault(pl => pl.UserName == username);
                if (p != null && !joined.Contains(p)) // Valid player
                {
                    p.clientID = Context.ConnectionId;
                    joined.Add(p);
                    if (joined.Count() > 1)
                    {
                        Clients.All.play(joined); // Note clients must subscribe to this event
                        playing = true;
                    }
                    return p;
                }
                else return null;
            }
            else return null;
        }

        public void update()
        {
            if (playing)
            {
                foreach (Player p in players)
                {
                    if (p.clientID == Context.ConnectionId)
                    {
                        Clients.Others.getUpdate();
                    }
                }
            }
        }

    }
}