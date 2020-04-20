using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Collections;
using System.Runtime.Serialization.Formatters;
using Common;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            PublishServer();

            //Console.ReadKey();

            //server.clients..ReceiveMessage("Hello from the server side");

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        static void PublishServer()
        {
            var serverProvider = new BinaryServerFormatterSinkProvider();
            serverProvider.TypeFilterLevel = TypeFilterLevel.Full;
            var clientProvider = new BinaryClientFormatterSinkProvider();

            var properties = new Hashtable();
            properties.Add("port", 8080);
            properties.Add("name", "server");

            String endpoint = "server";
            TcpChannel channel = new TcpChannel(properties, clientProvider, serverProvider);
            ChannelServices.RegisterChannel(channel, false);

            Server server = new Server();

            RemotingServices.Marshal(server, endpoint);
        }

    }
}
