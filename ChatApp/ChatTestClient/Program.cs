using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Threading;
using Common;

namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {


            //Console.WriteLine("Waiting...");
            //Console.ReadKey();

            if (args[0] == "a")
            {
                Client client = new Client();
                client.Connect("localhost", 8080, "server");
                String username = "a";

                client.Login(username, "");
                Console.ReadKey();

                client.StartChatWithUser("b");
                //client.otherUser.Message("Hello from a!");

                ChatLoop(client);
            }

            else if (args[0] == "b")
            {
                Client client = new Client();
                client.Connect("localhost", 8080, "server");
                String username = "b";

                client.Login(username, "");
                client.StartChatWithUser("a");

                ChatLoop(client);
            }

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        public static void ChatLoop(Client client)
        {
            Console.WriteLine("Chatting...");

            while (true)
            {
                string line = Console.ReadLine();

                client.otherUser.Message(line);
            }
        }
    }
}
