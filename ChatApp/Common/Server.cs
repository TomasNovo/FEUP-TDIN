using System;
using System.Collections.Generic;
using System.Linq;
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

        public Server()
        {
            users = new Dictionary<String, User>();

            db = new Database();
            db.StartMongo();

            Console.WriteLine("Server is running");
        }


        public bool Register(string username, string password, string realname, Client client)
        {
            if (!db.Register(username, password, realname))
                return false;

            foreach (KeyValuePair<string, User> entry in users) // Notify all other users on user register
                entry.Value.client.Message($"User '{username}' has entered the chat");

            User newUser = new User(new UserInfo(username, password, realname));
            newUser.client = client;

            users.Add(username, newUser);
            client.Message("Registered successfully");
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
            client.Message("Logged in successfully");

            return true;
        }

        public Client GetUserClient(string username)
        {
            return (users.TryGetValue(username, out User us)) ? us.client : null;
        }

        public User GetUser(string username)
        {
            return (users.TryGetValue(username, out User us)) ? us : null;
        }

        public void AskUserForChat(string username)
        {
            //users.
        }
                
    }
}