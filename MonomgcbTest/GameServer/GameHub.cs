using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.Xna.Framework;
using System.Timers;

namespace GameServer
{
    public class GameHub : Hub
    {
        Timer t;
        public GameHub()
        {
            t = new Timer(500);
            t.Elapsed += T_Elapsed;
            t.Start();

    }

        private void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            Vector2 Position = new Vector2(r.Next(50, 750), r.Next(50, 550));
            Clients.All.BroadcastPosition(Position);
        }
        
    }
}