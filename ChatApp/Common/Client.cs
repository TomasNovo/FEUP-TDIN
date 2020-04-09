using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Serialization.Formatters;
using System.Collections;
using System.Security.Cryptography;

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

        public ArrayList onlineUsers;

        public Dictionary<int, List<Client>> chatRooms;
        public List<int> chronologicalIds;

        public Client()
        {
        }

        //-----------------------------------Local methods-----------------------------------

        public bool Connect(string IP, int port, string endpoint)
        {
            string link = $"tcp://{IP}:{port}/{endpoint}";

            try
            {
                chatRooms = new Dictionary<int, List<Client>>();

                chronologicalIds = new List<int>();

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
            if (!server.Register(username, HashString(password), RealName, this))
            {
                Console.WriteLine("username is taken!");
                return false;
            }

            ServerMessage("Registered successfully");

            this.UserName = username;
            this.RealName = RealName;

            return true;
        }

        public bool Login(string username, string password)
        {
            Console.WriteLine("[Client]: Subscribing event OnChange ...");
            server.OnlineUsersChanged += Handler;

            Console.WriteLine("Passed");

            if (!server.Login(username, HashString(password),this))
            {
                Console.WriteLine("Invalid login!");
                return false;
            }

            this.UserName = username;

            ServerMessage("Logged in successfully");

            return true;
        }

        public ArrayList GetUsers()
        {
            return server.GetDatabaseUsers();
        }

        public int StartGroupChat(string[] usernames)
        {
            return server.StartGroupChat(usernames);
        }

        public int StartChatWithUser(string username)
        {
            return server.StartChatWithUser(username);
        }

        public bool SendMessage(int id, string message)
        {
            try
            {
                List<Client> clients = chatRooms[id];

                for (int i = 0; i < clients.Count; i++)
                {
                    clients[i].Message(UserName, message);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        public void ChatLoop(int id)
        {
            Console.WriteLine("Chatting...");

            while (true)
            {
                string line = Console.ReadLine();
                SendMessage(id, line);
            }
        }


        //-----------------------------------Remote methods-----------------------------------

        public void Message(string username, string msg)
        {
            Console.WriteLine($"{username}: {msg}");
        }

        public void ServerMessage(string msg)
        {
            Console.WriteLine($"Server:{msg}");
        }

        public bool JoinChatRoom(int id, string[] usernames, List<Client> clients)
        {
            List<Client> userList = new List<Client>();
            for (int i = 0; i < usernames.Length; i++)
            {
                if (usernames[i] == UserName)
                    continue;

                userList.Add(clients[i]);
            }

            chatRooms.Add(id, userList);
            chronologicalIds.Add(id);

            return true;
        }


        //------------Hashing Functions-----------------
        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string HashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        //Handler
        public void Handler(object o, OnlineUsersEventArgs e)
        {
            Console.WriteLine("[Client]: Online users count {0} has changed.", e.ou.Count);
            onlineUsers = e.ou;
        }
    }

}
