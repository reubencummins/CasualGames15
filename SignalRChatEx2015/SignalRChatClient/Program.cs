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
        static bool Running;

        static void Main(string[] args)
        {
            Console.Write("Enter name: ");
            Name = Console.ReadLine();

            HubConnection connection = new HubConnection("http://localhost:4039");
            proxy = connection.CreateHubProxy("ChatHub");

            connection.StateChanged += Connection_StateChanged;
            connection.Received += Connection_Received;
            Action<string, string> SendMessageRecieved = recieved_a_message;
            proxy.On("broadcastMessage", SendMessageRecieved);

            Action<int, int> IntsReceived = received_ints;
            proxy.On("broadcastScores", IntsReceived);

            connection.Start().Wait();

            Running = true;
            
            
            while (Running)
            {
                while ((Input = Console.ReadLine()) != null)
                {
                    try
                    {
                        int a, b;
                        a = Convert.ToInt32(Input);
                        Console.Write(" + ");
                        b = Convert.ToInt32(Console.ReadLine());
                        proxy.Invoke("SendInts", new object[] { a, b });
                    }
                    catch (Exception)
                    {
                        
                    }
                    proxy.Invoke("Send", new object[] { Name, Input });
                }

            }
        }

        private static void Connection_StateChanged(StateChange obj)
        {
            Console.WriteLine(obj.NewState);
        }

        private static void received_ints(int arg1, int arg2)
        {
            Console.WriteLine("{0} : {1}", arg1, arg2);
        }
        

        private static void recieved_a_message(string sender, string message)
        {
            Console.WriteLine("{0}: {1}", sender, message);
        }

        private static void Connection_Received(string obj)
        {
        }
        
    }
}