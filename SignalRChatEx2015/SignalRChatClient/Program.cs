using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace SignalRChatClient
{
    class Program
    {
        static string Name;
        static string Input;
        static IHubProxy proxy;
        

        static void Main(string[] args)
        {
            Console.Write("Enter name: ");
            Name = Console.ReadLine();

            HubConnection connection = new HubConnection("http://localhost:4039");
            proxy = connection.CreateHubProxy("ChatHub");

            //connection.Received += Connection_Received;
            Action<string, string> SendMessageRecieved = recieved_a_message;
            proxy.On("broadcastMessage", SendMessageRecieved);

            connection.Start().Wait();

            while (true)
            {
                while ((Input = Console.ReadLine()) != null)
                {
                    proxy.Invoke("Send", new object[] { Name, Input });
                }
            }
        }

        private static void recieved_a_message(string sender, string message)
        {
            Console.WriteLine("{0}: {1}", sender, message);
        }

        //private static void Connection_Received(string obj)
        //{
        //    Console.WriteLine("Message Received: {0}",obj);
        //}


    }
}