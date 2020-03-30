using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using RemoteObjects;
using System.Threading;

namespace ChatClient
{
    class Program
    {

        static void Main(string[] args)
        {


            //Console.WriteLine("Waiting...");
            //Console.ReadKey();

            Client client = new Client("localhost", 8080, "server");
            String username = DateTime.Now.ToString();
            client.Register(username, "", "");
            //Console.WriteLine($"Added user '{username}'! Total user count:{server.UserList.Count}");

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        public void ReceiveMessage(String msg)
        {
            Console.WriteLine(msg);
        }
    }
}
