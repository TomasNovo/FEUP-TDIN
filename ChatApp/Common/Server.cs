using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Common
{
    [Serializable]
    public class Server : MarshalByRefObject
    {
        public string ServerName;
        private Dictionary<string, User> users;

        private Database db;

        private Random rng;

        // Tuple's first is the list of all the clients of the users of the group, the second is the list of the users that accepted the chat
        private Dictionary<int, ChatRoomInfo> chatRoomAccepts;

        public Server()
        {
            users = new Dictionary<String, User>();

            chatRoomAccepts = new Dictionary<int, ChatRoomInfo>();

            rng = new Random();

            db = new Database();
            db.StartMongo();

            Console.WriteLine("Server is running");
        }

        //-----------------------------------Local methods-----------------------------------

        private Client GetUserClient(string username)
        {
            return (users.TryGetValue(username, out User us)) ? us.client : null;
        }

        private bool CheckAllAccepted(List<Client> a, List<string> b)
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

        public int StartGroupChat(string creator, List<string> usernames)
        {
            usernames.Sort();

            List<Client> clients = new List<Client>();
            int roomId = HashUsers(usernames);

            if (chatRoomAccepts.ContainsKey(roomId))
            {
                Console.WriteLine("ChatRoom already exists! Removing...");
                chatRoomAccepts.Remove(roomId);
            }

            Console.WriteLine($"roomId: {roomId}");
            ChatRoomInfo info = new ChatRoomInfo();
            chatRoomAccepts.Add(roomId, info);

            // Check for wrong usernames
            for (int i = 0; i < usernames.Count; i++)
            {
                Client cl = GetUserClient(usernames[i]);

                if (cl == null)
                    return -1;

                clients.Add(cl);
            }

            // Create roomChat and add creator to it
            info.roomId = roomId;
            info.creator = creator;
            info.clients = clients;

            AcceptChatRequest(roomId, creator);
            OnAskForChat(roomId, creator, usernames.ToList());

            return roomId;
        }

        public void AcceptChatRequest(int roomId, string username)
        {
            //Console.WriteLine($"User {username} accepted the group chat");

            chatRoomAccepts.TryGetValue(roomId, out ChatRoomInfo info);
            info.accepted.Add(username);

            //Console.WriteLine($"Users that accepted ({info.accepted.Count}/{info.clients.Count}):");
            //for (int i = 0; i < info.accepted.Count; i++)
            //{
            //    Console.WriteLine($"{info.accepted[i]}");
            //}

            if (CheckAllAccepted(info.clients, info.accepted))
            {
                // Join chatRoom on each client in the chatroom
                for (int i = 0; i < info.clients.Count; i++)
                {
                    info.clients[i].JoinChatRoom(roomId, info);
                }
                
                OnChatFinalize(roomId, true);

                Console.WriteLine("All users accepted the chat!");
            }
        }

        public void RejectChatRequest(int roomId, string username)
        {
            Console.WriteLine($"User {username} rejected the group chat");

            OnChatFinalize(roomId, false);

            chatRoomAccepts.Remove(roomId);

            Console.WriteLine($"User {username} has rejected the group chat!");
        }

        public void RegisterMessageLog(int roomId, string )
        {
            //db
        }

        public List<string> GetDatabaseUsers()
        {
            return db.GetUsersArraylist();
        }

        public List<string> GetOnlineUsers()
        {
            return users.Keys.ToList();
        }

        public static int HashUsers(List<string> usernames)
        {
            int roomId = 0;

            for (int i = 0; i < usernames.Count; i++)
                roomId += Client.HashToInt(usernames[i]);

            if (roomId < 0)
                roomId *= -1;

            return roomId;
        }

        public void Ping()
        {

        }


        //---------Delegate------
        public delegate void OnlineUsersChangeEventHandler(object source, OnlineUsersEventArgs e);
        public event OnlineUsersChangeEventHandler OnlineUsersChanged;

        protected virtual void OnOnlineUsersChange(List<string> u)
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

            Console.WriteLine(chatRoomAccepts.TryGetValue(roomId, out ChatRoomInfo info));
            e.userList = info.clients.Select(element => element.UserName).ToList();

            if (ChatFinalized != null)
            {
                ChatFinalized(this, e);
            }
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

    }
}