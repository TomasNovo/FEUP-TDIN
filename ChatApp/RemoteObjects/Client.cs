using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;

namespace RemoteObjects
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
            thiendpoint = "server";
            TcpChannel chan = new TcpChannel();
            ChannelServices.RegisterChannel(chan, false);
            Server server = (Server)Activator.GetObject(
              typeof(Server), "tcp://localhost:8080/" + endpoint);
        }


        public void ReceiveMessage(String msg)
        {
            Console.WriteLine($"Received message:{msg}");
        }

    }
}
