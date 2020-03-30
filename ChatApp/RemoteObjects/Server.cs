using RemoteObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteObjects
{
    public class Server : ServerInterface
    {
        public String ServerName;
        public List<String> UserList;
        public Client client;

        public Server()
        {
            UserList = new List<string>();
        }

        public override bool AddUser(String username)
        {
            UserList.Add(username);

            Console.WriteLine($"Added user '{username}'! Total user count:{UserList.Count}");

            return true;
        }

        public override void ShowUsers()
        {
            Console.WriteLine("Showing users:");

            for (int i = 0; i < UserList.Count; i++)
            {
                Console.WriteLine(UserList[i]);
            }
        }

        public bool Register(String username, String password, String RealName, Client client)
        {
            //TODO: tests

            this.client = client;

            Console.WriteLine($"User '{username}' registered.");

            return true;
        }
    }
}
