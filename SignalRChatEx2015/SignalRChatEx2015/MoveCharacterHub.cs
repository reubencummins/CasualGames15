using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.Xna.Framework;
using System.Timers;

namespace SignalRChatEx2015
{
    public class MoveCharacterHub : Hub
    {
        static Timer t;
        static Random r;
        public MoveCharacterHub() : base()
        {
            r = new Random(DateTime.Now.Millisecond);
            t = new Timer(500);
            t.Elapsed += T_Elapsed;
            t.Start();

        }

        private void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            Clients.All.broadcastposition(r.Next(0,800),r.Next(0,600));
        }

        public void Update(Matrix transform,string id)
        {
            Clients.All.broadcastPosition(transform, id);
        }
    }
}