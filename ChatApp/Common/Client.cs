using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Serialization.Formatters;
using System.Collections;

namespace Common
{
    public class Client : MarshalByRefObject
    {
        public String IP;
        public int Port;
        public String EndPoint;

        public String UserName;
        public String Password;
        public String RealName;

        public Server server;

        public Client(String IP, int port, String endpoint)
        {
            this.IP = IP;
            this.Port = port;
            this.EndPoint = endpoint;

            this.Start();
        }

        public void Start()
        {
            var serverProvider = new BinaryServerFormatterSinkProvider();
            serverProvider.TypeFilterLevel = TypeFilterLevel.Full;
            var clientProvider = new BinaryClientFormatterSinkProvider();

            var properties = new Hashtable();
            properties.Add("port", 0);

            TcpChannel channel = new TcpChannel(properties, clientProvider, serverProvider);
            ChannelServices.RegisterChannel(channel, false);
            this.server = (Server)Activator.GetObject(
              typeof(Server), $"tcp://{this.IP}:{this.Port}/" + this.EndPoint);

        }

        public bool Register(String username, String password, String RealName)
        {
            if (!server.Register(username, password, RealName, this))
            {
                Console.WriteLine("username is taken!");
                return false;
            }

            // TODO asssign username, etc

            return true;
        }


        public void ReceiveMessage(String msg)
        {
            Console.WriteLine($"Received message:{msg}");
        }

    }
}
