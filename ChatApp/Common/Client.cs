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
                Console.WriteLine($"Failed to connect to {link}! {e.Message}");
                return false;
            }
            

            this.IP = IP;
            this.Port = port;
            this.EndPoint = endpoint;

            return true;
        }

        public string Register(string username, string password, string RealName)
        {
            string registerMessage = server.Register(username, HashString(password), RealName, this);
            if (registerMessage != "")
            {
                Console.WriteLine(registerMessage);
                return registerMessage;
            }

            ServerMessage("Registered successfully");

            this.UserName = username;
            this.RealName = RealName;

            return registerMessage;
        }

        public string Login(string username, string password)
        {
            string loginMessage = server.Login(username, HashString(password), this);
            if (loginMessage != "")
            {
                Console.WriteLine(loginMessage);
                return loginMessage;
            }

            AddHandlers();

            this.UserName = username;

            ServerMessage("Logged in successfully");

            return loginMessage;
        }

        public bool Logout(string username)
        {
            RemoveHandlers();

            if (!server.Logout(username))
            {
                Console.WriteLine("Something went wrong while logging out");
                return false;
            }

            ServerMessage("Logged out successfully");

            return true;
        }

        public ArrayList GetUsers()
        {
            return server.GetDatabaseUsers();
        }

        public int StartGroupChat(string[] usernames)
        {
            return server.StartGroupChat(UserName, usernames);
        }

        public int StartChatWithUser(string username)
        {
            string[] users = { UserName, username };

            return server.StartChatWithUser(UserName, users);
        }

        public void AcceptChatRequest(int roomId)
        {
            this.server.AcceptChatRequest(roomId, this.UserName);
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

        //Handler -> Client receives event and sends new event to Index

        public void AddHandlers()
        {
            server.OnlineUsersChanged += HandlerLogout;
            server.ChatAsked += HandlerAskForChat;
            server.ChatAccepted += HandlerChatAccepted;
        }

        public void RemoveHandlers()
        {
            server.OnlineUsersChanged -= HandlerLogout;
            server.ChatAsked -= HandlerAskForChat;
        }

        public delegate void OnlineUsersChangeEventHandler(object source, OnlineUsersEventArgs e);
        public event OnlineUsersChangeEventHandler OnlineUsersChanged;

        public void HandlerLogout(object o, OnlineUsersEventArgs e)
        {
            if (OnlineUsersChanged != null)
            {
                OnlineUsersChanged(this, e);
            }
        }


        public delegate void AskForChatEventHandler(object source, AskForChatEventArgs e);
        public event AskForChatEventHandler ChatAsked;

        public void HandlerAskForChat(object o, AskForChatEventArgs e)
        {
            if (ChatAsked != null)
            {
                ChatAsked(this, e);
            }
        }


        public delegate void ChatAcceptedEventHandler(object source, ChatAcceptedEventArgs e);
        public event ChatAcceptedEventHandler ChatAccepted;

        public void HandlerChatAccepted(object o, ChatAcceptedEventArgs e)
        {
            if (ChatAsked != null)
            {
                ChatAccepted(this, e);
            }
        }


    }

}
