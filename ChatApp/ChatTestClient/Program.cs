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
                Client client = new Client("localhost", 8080, "server");
                String username = "a";

                client.Register(username, "", "");
            }

            else if (args[0] == "b")
            {
                Client client = new Client("localhost", 8080, "server");
                String username = "b";

                client.Register(username, "", "");

                client.server.GetUser("a").ReceiveMessage("Hello from b!");
            }

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        public void ReceiveMessage(String msg)
        {
            Console.WriteLine(msg);
        }
    }
}
