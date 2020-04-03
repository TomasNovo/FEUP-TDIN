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
            if (args[0] == "a")
            {
                Client client = new Client();
                client.Connect("localhost", 8080, "server");
                String username = "a";

                client.Login(username, "");
                
                // User "a" starts the group chat with user "b" and "c"
                Console.ReadKey();
                string[] groupUsers = { "a", "b", "c" };
                int id = client.StartGroupChat(groupUsers);

                client.ChatLoop(id);
            }

            else if (args[0] == "b")
            {
                Client client = new Client();
                client.Connect("localhost", 8080, "server");
                String username = "b";

                client.Login(username, "");

                Console.ReadKey();
                client.ChatLoop(client.chronologicalIds.FirstOrDefault());
            }

            else if (args[0] == "c")
            {
                Client client = new Client();
                client.Connect("localhost", 8080, "server");
                String username = "c";

                client.Login(username, "");

                Console.ReadKey();
                client.ChatLoop(client.chronologicalIds.FirstOrDefault());
            }
        }

        
    }
}
