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

                users.DeleteMany(x => true);
            }
            catch (Exception e)
            {
                Console.WriteLine($"DB Error: {e.Message}");
                return false;
            }

            Console.WriteLine("Started database");
            return true;
        }

        public bool Register(String username, String password, String realname)
        {
            if (UsernameExists(username))
                return false;

            users.InsertOne(new UserInfo(username, password, realname));

            return true;
        }

        public bool UsernameExists(String username)
        {
            return (users.Find(x => x.username == username).ToList().Count == 1);
        }
    }
}
