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

        // Tuple's first is the list of all the clients of the users of the group, the second is the list of the users that accepted the chat
        private Dictionary<int, Tuple<List<Client>, List<string>>> chatRoomAccepts;

        public Server()
        {
            users = new Dictionary<String, User>();

            chatRoomAccepts = new Dictionary<int, Tuple<List<Client>, List<string>>>();

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

        public int StartChatWithUser(string creator, string user)
        {
            List<string> userList = new List<string>();
            userList.Add(creator);
            userList.Add(user);

            return StartGroupChat(creator, userList);
        }

        // TODO: Check for duplicate chats
        public int StartGroupChat(string creator, List<string> usernames)
        {
            List<Client> clients = new List<Client>();
            int roomId = rng.Next();

            // Check for wrong usernames
            for (int i = 0; i < usernames.Count; i++)
            {
                Client cl = GetUserClient(usernames[i]);

                if (cl == null)
                    return -1;

                clients.Add(cl);
            }

            OnAskForChat(roomId, creator, usernames.ToList<string>());

            // Create roomChat and add creator to it
            Tuple<List<Client>, List<string>> tuple = new Tuple<List<Client>, List<string>>(clients, new List<string>());
            chatRoomAccepts.Add(roomId, tuple);
            AcceptChatRequest(roomId, creator);

            return roomId;
        }

        public void AcceptChatRequest(int roomId, string username)
        {
            Console.WriteLine($"User {username} accepted the group chat");

            chatRoomAccepts.TryGetValue(roomId, out Tuple<List<Client>, List<string>> tuple);
            tuple.Item2.Add(username);

            Console.WriteLine($"Users that accepted ({tuple.Item2.Count}/{tuple.Item1.Count}):");
            for (int i = 0; i < tuple.Item2.Count; i++)
            {
                Console.WriteLine($"{tuple.Item2[i]}");
            }

            if (CheckAllAccepted(tuple.Item1, tuple.Item2))
            {
                // Join chatRoom on each client in the chatroom
                for (int i = 0; i < tuple.Item1.Count; i++)
                {
                    tuple.Item2.Sort();
                    tuple.Item1[i].JoinChatRoom(roomId, tuple.Item1);
                }
                
                OnChatFinalize(roomId, true);

                Console.WriteLine("All users accepted the chat!");
            }
        }

        public bool CheckAllAccepted(List<Client> a, List<string> b)
        {
            if (a.Count != b.Count)
                return false;

            int matchCounter = 0;
            for (int i = 0; i < a.Count; i++)
            {
                for (int j = 0; j < a.Count; j++)
                {
                    if (a[i].UserName == b[j])
                        matchCounter++;
                }
            }

            return matchCounter == a.Count;
        }

        public void RejectChatRequest(int roomId, string username)
        {
            Console.WriteLine($"User {username} rejected the group chat");

            chatRoomAccepts.TryGetValue(roomId, out Tuple<List<Client>, List<string>> tuple);

            OnChatFinalize(roomId, false);

            Console.WriteLine($"User {username} has rejected the group chat!");
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

        public void Ping()
        {

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

        public delegate void ChatFinalizedEventHandler(object source, ChatFinalizedEventArgs e);
        public event ChatFinalizedEventHandler ChatFinalized;

        protected virtual void OnChatFinalize(int roomId, bool result)
        {
            ChatFinalizedEventArgs e = new ChatFinalizedEventArgs();
            e.result = result;
            e.roomId = roomId;

            chatRoomAccepts.TryGetValue(roomId, out Tuple<List<Client>, List<string>> tuple);
            e.userList = tuple.Item2;

            if (ChatAsked != null)
            {
                ChatFinalized(this, e);
            }
        }

    }


}