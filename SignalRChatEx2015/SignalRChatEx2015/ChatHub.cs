using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalRChatEx2015
{
    public class ChatHub : Hub
    {
        public ChatHub()
        {
            
        }
        public void Send(string name, string message)
        {
            Clients.All.broadcastMessage(name, message);
        }

        public void SendInts(int a, int b)
        {
            Clients.All.broadcastScores(a, b);
        }

        
    }
}