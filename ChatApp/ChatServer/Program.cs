using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using RemoteObjects;
using System.Collections;
using System.Runtime.Serialization.Formatters;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var serverProvider = new BinaryServerFormatterSinkProvider();
            serverProvider.TypeFilterLevel = TypeFilterLevel.Full;

            var clientProvider = new BinaryClientFormatterSinkProvider();

            var properties = new Hashtable();

            properties.Add("port", 8080);

            String endpoint = "server";
            TcpChannel channel = new TcpChannel(properties, clientProvider, serverProvider);
            ChannelServices.RegisterChannel(channel, false);

            Server server = new Server();

            RemotingServices.Marshal(server, endpoint);

            Console.WriteLine("Server is running");
            Console.ReadKey();

            server.client.ReceiveMessage("Hello from the server side");

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        static void ReceiveMessage(String msg)
        {
            Console.WriteLine(msg);
        }
    }
}
