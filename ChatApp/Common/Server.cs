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

        public Server()
        {
            users = new Dictionary<String, User>();
            
            rng = new Random();

            db = new Database();
            db.StartMongo();

            Console.WriteLine("Server is running");
        }

        //-----------------------------------Remote methods-----------------------------------

        public bool Register(string username, string password, string realname, Client client)
        {
            if (!db.Register(username, password, realname))
                return false;

            //NotifyUserLogin(username);
            //User newUser = new User(new UserInfo(username, password, realname));
            //newUser.client = client;
            //users.Add(username, newUser);

            Console.WriteLine($"User '{username}' registered.");

            return true;
        }

        public bool Login(string username, string password, Client client)
        {
            UserInfo userInfo = db.Login(username, password);
            
            if (userInfo == null)
                return false;

            User newUser = new User(userInfo);
            newUser.client = client;

            users.Add(username, newUser);

            Console.WriteLine($"User '{username}' has logged in");
            NotifyUserLogin(username);


            OnOnlineUsersChange();

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

        public Client GetUserClient(string username)
        {
            return (users.TryGetValue(username, out User us)) ? us.client : null;
        }

        public int StartChatWithUser(string username)
        {
            string[] arr = { username };
            return StartGroupChat(arr);
        }

        // TODO: Check for duplicate chats
        public int StartGroupChat(string[] usernames)
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

            return roomId;
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

        //--------------- Hashing Methods -------------------------------------
        static string Hash()
        {
            return Hash(DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString());
        }

        static string Hash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        //---------Delegate------
        public delegate void OnlineUsersChangeEventHandler(object source, OnlineUsersEventArgs e);
        public event OnlineUsersChangeEventHandler OnlineUsersChanged;

        protected virtual void OnOnlineUsersChange()
        {
            OnlineUsersEventArgs e = new OnlineUsersEventArgs(GetOnlineUsers());
            if (OnlineUsersChanged != null)
            {
                Console.WriteLine("[UpdateOnlineUsers]: Raising event ...");
                OnlineUsersChanged(this, e);
            }
        }
    }


}