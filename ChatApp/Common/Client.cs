using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Serialization.Formatters;
using System.Collections;
using System.Security.Cryptography;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Common
{
    [Serializable]
    public class Client : MarshalByRefObject
    {
        public string ServerIP;
        public int Port;
        public string EndPoint;

        public string IPAddress;

        public string UserName;
        public string RealName;

        public Server server;

        public ArrayList onlineUsers;

        public Dictionary<int, ChatRoomInfo> chatRooms;
        public List<int> chronologicalIds;

        public Client()
        {
        }

        //-----------------------------------Local methods-----------------------------------

        public bool Connect(string ServerIP, int port, string endpoint)
        {
            string link = $"tcp://{ServerIP}:{port}/{endpoint}";

            try
            {
                chatRooms = new Dictionary<int, ChatRoomInfo>();

                chronologicalIds = new List<int>();

                var serverProvider = new BinaryServerFormatterSinkProvider();
                serverProvider.TypeFilterLevel = TypeFilterLevel.Full;
                var clientProvider = new BinaryClientFormatterSinkProvider();

                var properties = new Hashtable();
                properties.Add("port", 0);
                properties.Add("name", "server");

                TcpChannel channel = new TcpChannel(properties, clientProvider, serverProvider);
                ChannelServices.RegisterChannel(channel, false);
                this.server = (Server)Activator.GetObject(typeof(Server), link);

                GetLocalIPAddress();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to connect to {link}! {e.Message}");
                return false;
            }
            

            this.ServerIP = ServerIP;
            this.Port = port;
            this.EndPoint = endpoint;

            this.IPAddress = GetLocalIPAddress();

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

        public bool Logout()
        {
            RemoveHandlers();

            if (this.UserName != null && !server.Logout(this.UserName))
            {
                Console.WriteLine("Something went wrong while logging out");
                return false;
            }

            ServerMessage("Logged out successfully");

            return true;
        }

        public void Ping()
        {
            server.Ping();
        }

        public List<string> GetUsers()
        {
            return server.GetDatabaseUsers();
        }

        public int StartChat(string username)
        {
            List<string> userList = new List<string>();
            userList.Add(username);
            return StartChat(userList);
        }

        public int StartChat(List<string> usernames)
        {
            if (usernames.IndexOf(this.UserName) == -1)
                usernames.Add(this.UserName);

            usernames.Sort();

            int roomId = Server.HashUsers(usernames);
            if (chatRooms.ContainsKey(roomId))
                return -2;

            return server.StartGroupChat(UserName, usernames);
        }


        public void AcceptChatRequest(int roomId)
        {
            this.server.AcceptChatRequest(roomId, this.UserName);
        }

        public void RejectChatRequest(int roomId)
        {
            this.server.RejectChatRequest(roomId, this.UserName);
        }

        public bool SendMessage(int roomId, string message)
        {
            try
            {
                List<Client> clients = chatRooms[roomId].clients;

                for (int i = 0; i < clients.Count; i++)
                {
                    clients[i].OnMessageSend(roomId, this.UserName, message);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        public bool SendFile(int roomId, string message, byte[] file)
        {
            try
            {
                List<Client> clients = chatRooms[roomId].clients;

                for (int i = 0; i < clients.Count; i++)
                {
                    clients[i].OnMessageSend(roomId, this.UserName, message, file);
                }
            }
            catch (Exception e)
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

        public static string GetLocalIPAddress()
        {

            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();

                return localIP;
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

        public bool JoinChatRoom(int roomId, ChatRoomInfo info)
        {
            chatRooms.Add(roomId, info);

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

        public static int HashToInt(string inputString)
        {
            byte[] byteArr = GetHash(inputString);
            byte[] intByteArr = new byte[4];

            for (int i = 0; i < byteArr.Length && i < intByteArr.Length; i++)
                intByteArr[i] = byteArr[i];

            int result = BitConverter.ToInt32(byteArr, 0);

            return result;
        }

        //-------------Events----------------------------

        public void AddHandlers()
        {
            server.OnlineUsersChanged += HandlerLogout;
            server.ChatAsked += HandlerAskForChat;
            server.ChatFinalized += HandlerChatFinalized;
            MessageReceived += MessageReceivedHandler;
        }

        public void RemoveHandlers()
        {
            server.OnlineUsersChanged -= HandlerLogout;
            server.ChatAsked -= HandlerAskForChat;
            server.ChatFinalized -= HandlerChatFinalized;
            MessageReceived -= MessageReceivedHandler;
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


        public delegate void ChatFinalizedEventHandler(object source, ChatFinalizedEventArgs e);
        public event ChatFinalizedEventHandler ChatFinalized;

        public void HandlerChatFinalized(object o, ChatFinalizedEventArgs e)
        {
            if (ChatFinalized != null)
            {
                ChatFinalized(this, e);
            }
        }

        public delegate void MessageReceivedEventHandler(object source, MessageReceivedEventArgs e);
        public event MessageReceivedEventHandler MessageReceived;

        public void OnMessageSend(int roomId, string sender, string message, byte[] file = null, string timestamp = "")
        {
            MessageReceivedEventArgs e = new MessageReceivedEventArgs();
            e.roomId = roomId;
            e.sender = sender;
            e.message = message;
            e.file = file;
            // e.timestamp = ...; ? 

            if (MessageReceived != null)
            {
                MessageReceived(this, e);
            }
        }

        public void MessageReceivedHandler(object source, MessageReceivedEventArgs e)
        {
            //if (chatRooms.ContainsKey(e.roomId))
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }

}
