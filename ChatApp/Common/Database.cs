using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections;

namespace Common
{
    class Database
    {
        private MongoClient mongo;
        private IMongoDatabase database;
        private IMongoCollection<UserInfo> users;

        public bool StartMongo()
        {
            try
            {
                mongo = new MongoClient("mongodb://localhost:27017");
                database = mongo.GetDatabase("server");
                users = database.GetCollection<UserInfo>("users");

                // Empties collection
                //users.DeleteMany(x => true);
            }
            catch (Exception e)
            {
                Console.WriteLine($"DB Error: {e.Message}");
                return false;
            }

            Console.WriteLine("Started database");
            return true;
        }

        public bool Register(string username, string password, string realname)
        {
            if (UsernameExists(username))
                return false;

            users.InsertOne(new UserInfo(username, password, realname));

            return true;
        }

        public UserInfo Login(string username, string password)
        {
            var userList = users.Find(x => (x.username == username && x.password == password)).ToList();

            if (userList.Count != 1)
                return null;

            return userList.First();
        }

        public bool UsernameExists(string username)
        {
            return (users.Find(x => x.username == username).ToList().Count == 1);
        }

        public List<string> GetUsersArraylist()
        {
            return users.Find(_ => true).ToList().Select(e => e.username).ToList();
        }
    }
}
