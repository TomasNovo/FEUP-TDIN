using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections;


namespace TTService
{
    class Database
    {
        private MongoClient mongo;
        private IMongoDatabase database;
        private IMongoCollection<User> users;
        private IMongoCollection<Ticket> tickets;
        //private IMongoCollection<ChatRoomLog> chatRoomLogs;

        public Database()
        {
            StartMongo();
        }

        public bool StartMongo()
        {
            try
            {
                mongo = new MongoClient("mongodb://localhost:27017");
                database = mongo.GetDatabase("service");
                users = database.GetCollection<User>("users");
                tickets = database.GetCollection<Ticket>("tickets");
            }
            catch (Exception e)
            {
                Console.WriteLine($"DB Error: {e.Message}");
                return false;
            }

            Console.WriteLine("Started database");
            return true;
        }

        public bool Register(string username, string email)
        {
            if (UsernameExists(username))
                return false;

            users.InsertOne(new User(username, email));

            return true;
        }

        public bool AddTicket(string username, DateTime date, string title, string description)
        {
            tickets.InsertOne(new Ticket(username, date, title, description));

            return true;
        }

        public List<User> GetUsers()
        {
            List<User> u = (from x in users.AsQueryable<User>() select x).ToList();
            return u;
        }

        public List<Ticket> GetTickets()
        {
            List<Ticket> u = (from x in tickets.AsQueryable<Ticket>() select x).ToList();
            return u;
        }

        public List<Ticket> GetTickets(string username)
        {
            return tickets.Find(x => x.user == username).ToList();
        }


        //public UserInfo Login(string username, string password)
        //{
        //    var userList = users.Find(x => (x.username == username && x.password == password)).ToList();

        //    if (userList.Count != 1)
        //        return null;

        //    return userList.First();
        //}

        public bool UsernameExists(string username)
        {
            return (users.Find(x => x.username == username).ToList().Count == 1);
        }
    }
}
