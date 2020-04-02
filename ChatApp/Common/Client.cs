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
        public string IP;
        public int Port;
        public string EndPoint;

        public string UserName;
        public string RealName;

        public Server server;
        public Client otherUser;

        public Client()
        {

        }

        public bool Connect(string IP, int port, string endpoint)
        {
            string link = $"tcp://{IP}:{port}/{endpoint}";

            try
            {
                var serverProvider = new BinaryServerFormatterSinkProvider();
                serverProvider.TypeFilterLevel = TypeFilterLevel.Full;
                var clientProvider = new BinaryClientFormatterSinkProvider();

                var properties = new Hashtable();
                properties.Add("port", 0);

                TcpChannel channel = new TcpChannel(properties, clientProvider, serverProvider);
                ChannelServices.RegisterChannel(channel, false);
                this.server = (Server)Activator.GetObject(
                  typeof(Server), link );
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to connect to {link}");
                return false;
            }
            

            this.IP = IP;
            this.Port = port;
            this.EndPoint = endpoint;

            return true;
        }

        public bool Register(string username, string password, string RealName)
        {
            if (!server.Register(username, password, RealName, this))
            {
                Console.WriteLine("username is taken!");
                return false;
            }

            this.UserName = username;
            this.RealName = RealName;

            return true;
        }

        public bool Login(string username, string password)
        {
            if (!server.Login(username, password ,this))
            {
                Console.WriteLine("Invalid login!");
                return false;
            }

            return true;
        }


        public void Message(string msg)
        {
            Console.WriteLine($"Received message:{msg}");
        }

        public bool StartChatWithUser(string username)
        {
            Client cl = server.GetUserClient(username);

            if (cl == null)
                return false;

            otherUser = cl;

            return true;
        }

    }
}
