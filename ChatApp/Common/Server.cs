using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Common
{
    public class Server : MarshalByRefObject
    {
        public string ServerName;
        private Dictionary<string, User> users;

        private Database db;

        private Random rng;

        // Tuple's first is the list of all the users of the group, the second is the list of users that accepted the chat
        private Dictionary<int, Tuple<List<string>, List<string>>> chatRoomAccepts;

        public Server()
        {
            users = new Dictionary<String, User>();

            chatRoomAccepts = new Dictionary<int, Tuple<List<string>, List<string>>>();

            rng = new Random();

            db = new Database();
            db.StartMongo();

            Console.WriteLine("Server is running");
        }

        //-----------------------------------Remote methods-----------------------------------

        public string Register(string username, string password, string realname, Client client)
        {
            if (!db.Register(username, password, realname))
                return "Username already exists!";

            Console.WriteLine($"User '{username}' registered.");

            return "";
        }

        public string Login(string username, string password, Client client)
        {
            if (GetUserClient(username) != null)
                return "User already logged in!";

            UserInfo userInfo = db.Login(username, password);
            
            if (userInfo == null)
                return "Username and password don't match";

            User newUser = new User(userInfo);
            newUser.client = client;

            users.Add(username, newUser);

            Console.WriteLine($"User '{username}' has logged in");
            NotifyUserLogin(username);

            OnOnlineUsersChange(GetOnlineUsers());

            return "";
        }

        public bool Logout(string username)
        {
            //NotifyUserLogout(username);
            users.Remove(username);
            Console.WriteLine($"User '{username}' has logged out");

            OnOnlineUsersChange(GetOnlineUsers());

            return true;
        }

        public void NotifyUserLogin(string username)
        {
            foreach (KeyValuePair<string, User> entry in users) // Notify all other users on user register
            {
                if (entry.Key != username)
                    entry.Value.client.ServerMessage($"User '{username}' has entered the chat");
            }
        }

        //public void NotifyUserLogout(string username)
        //{
        //    foreach (KeyValuePair<string, User> entry in users) // Notify all other users on user register
        //    {
        //        if (entry.Key != username)
        //            entry.Value.client.ServerMessage($"User '{username}' has leaved the chat");
        //    }
        //}

        public Client GetUserClient(string username)
        {
            return (users.TryGetValue(username, out User us)) ? us.client : null;
        }

        public int StartChatWithUser(string creator, string[] users)
        {
            return StartGroupChat(creator, users);
        }

        // TODO: Check for duplicate chats
        public int StartGroupChat(string creator, string[] usernames)
        {
            List<Client> clients = new List<Client>();
            int roomId = rng.Next();

            // Check for wrong usernames
            for (int i = 0; i < usernames.Length; i++)
            {
                Client cl = GetUserClient(usernames[i]);

                if (cl == null)
                    return -1;

                clients.Add(cl);
            }

            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].JoinChatRoom(roomId, usernames, clients);
            }

            OnAskForChat(roomId, creator, usernames.ToList<string>());

            // Create roomChat and add creator to it
            Tuple<List<string>, List<string>> tuple = new Tuple<List<string>, List<string>>(usernames.ToList(), new List<string>());
            chatRoomAccepts.Add(roomId, tuple);
            AcceptChatRequest(roomId, creator);

            return roomId;
        }

        public void AcceptChatRequest(int roomId, string username)
        {
            chatRoomAccepts.TryGetValue(roomId, out Tuple<List<string>, List<string>> tuple);
            tuple.Item2.Add(username);

            if (ListEquals(tuple.Item1, tuple.Item2))
                OnChatAccept(roomId);
        }

        public bool ListEquals(List<string> a, List<string> b)
        {
            if (a.Count != b.Count)
                return false;

            for (int i = 0; i < a.Count; i++)
            {
                if (a[i] != b[i])
                    return false;
            }

            return true;
        }

        public ArrayList GetDatabaseUsers()
        {
            return db.GetUsersArraylist();
        }

        public ArrayList GetOnlineUsers()
        {
            ArrayList ou = new ArrayList();

            for(int i = 0; i < users.Count; i++)
            {
                ou.Add(users.Keys.ElementAt(i));
            }

            return ou;
        }


        //---------Delegate------
        public delegate void OnlineUsersChangeEventHandler(object source, OnlineUsersEventArgs e);
        public event OnlineUsersChangeEventHandler OnlineUsersChanged;

        protected virtual void OnOnlineUsersChange(ArrayList u)
        {
            OnlineUsersEventArgs e = new OnlineUsersEventArgs(u);
            if (OnlineUsersChanged != null)
            {
                OnlineUsersChanged(this, e);
            }
        }

        public delegate void AskForChatEventHandler(object source, AskForChatEventArgs e);
        public event AskForChatEventHandler ChatAsked;

        protected virtual void OnAskForChat(int roomId, string creator, List<string> users)
        {
            AskForChatEventArgs e = new AskForChatEventArgs();
            e.roomId = roomId;
            e.creator = creator;
            e.userList = users;

            if (ChatAsked != null)
            {
                ChatAsked(this, e);
            }
        }

        public delegate void ChatAcceptedEventHandler(object source, ChatAcceptedEventArgs e);
        public event ChatAcceptedEventHandler ChatAccepted;

        protected virtual void OnChatAccept(int roomId)
        {
            ChatAcceptedEventArgs e = new ChatAcceptedEventArgs();
            e.roomId = roomId;

            chatRoomAccepts.TryGetValue(roomId, out Tuple<List<string>, List<string>> tuple);
            e.userList = tuple.Item1;


            if (ChatAsked != null)
            {
                ChatAccepted(this, e);
            }
        }

    }


}