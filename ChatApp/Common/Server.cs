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
            {
                entry.Value.client.ReceiveMessage($"User '{entry.Key}' has entered the chat");
            }

            User newUser = new User(new UserInfo(username, password, realname));
            newUser.client = client;

            client.ReceiveMessage("Registered successfully");

            Console.WriteLine($"User '{username}' registered.");
            return true;
        }

        public Client GetUser(string username)
        {
            return users[username].client;
        }

        
    }
}
