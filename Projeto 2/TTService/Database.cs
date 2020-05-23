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
    public class Database
    {
        private MongoClient mongo;
        private IMongoDatabase database;
        private IMongoCollection<User> users;
        private IMongoCollection<Ticket> tickets;
        private IMongoCollection<SecondaryTicket> secondaryTickets;

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
                secondaryTickets = database.GetCollection<SecondaryTicket>("secondaryTickets");
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

        public bool AssignSolver(string solver, string id)
        {
            var filter = Builders<Ticket>.Filter.Eq("Id", new ObjectId(id));
            var update = Builders<Ticket>.Update.Set("solver", solver).Set("status", TicketStatus.Assigned);

            tickets.UpdateOne(filter, update);

            return true;
        }

        public void ChangeTicketStatus(string id, TicketStatus status)
        {
            var filter = Builders<Ticket>.Filter.Eq("Id", new ObjectId(id));
            var update = Builders<Ticket>.Update.Set("status", status);

            tickets.UpdateOne(filter, update);
        }

        public void ChangeSecondaryTicketAnswer(string originalTicketId, string response)
        {
            var filter = Builders<SecondaryTicket>.Filter.Eq("originalTicketId", new ObjectId(originalTicketId));

            List<SecondaryTicket> s = secondaryTickets.Find(x => x.originalTicketId == new ObjectId(originalTicketId)).ToList();

            s[0].answers.RemoveAt(s[0].answers.Count - 1);
            s[0].answers.Add(response);

            var update = Builders<SecondaryTicket>.Update.Set("answers", s[0].answers);

            secondaryTickets.UpdateOne(filter, update);
        }

        public List<Ticket> GetTicketsSolver(string s)
        {
            return tickets.Find(x => x.solver == s).ToList();
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

        public SecondaryTicket AddSecondaryTicket(string originalTicketId, string solver, string secondarySolver, string title, string question)
        {

            SecondaryTicket st = new SecondaryTicket(ObjectId.GenerateNewId(), new ObjectId(originalTicketId), solver, secondarySolver, title, question);
            secondaryTickets.InsertOne(st);

            return st;
        }

        public SecondaryTicket AddSecondaryTicketNewQuestions(string originalTicketId, string solver, string secondarySolver, string title, List<string> questions, List<string> answers)
        {

            SecondaryTicket st = new SecondaryTicket(ObjectId.GenerateNewId(), new ObjectId(originalTicketId), solver, secondarySolver, title, questions, answers);
            secondaryTickets.InsertOne(st);

            return st;
        }


        public int DeleteSecondaryTicket(string originalTicketId)
        {
            var filter = Builders<SecondaryTicket>.Filter.Eq("originalTicketId", new ObjectId(originalTicketId));
            secondaryTickets.DeleteOne(filter);
            return 0;
        }

        public List<SecondaryTicket> GetSecondaryTickets()
        {
            return secondaryTickets.Find(x => true).ToList();
        }

        public List<SecondaryTicket> GetSecondaryTicketsBySolver(string solver)
        {
            return secondaryTickets.Find(x => x.solver == solver).ToList();
        }

        public List<SecondaryTicket> GetSecondaryTicketsBySecondarySolver(string secondarySolver)
        {
            return secondaryTickets.Find(x => x.secondarySolver == secondarySolver).ToList();
        }

        public List<SecondaryTicket> GetSecondaryTicketInfoByID(string id)
        {
            return secondaryTickets.Find(x => x.Id == new ObjectId(id)).ToList();
        }

        public void SetSecondaryTicketReceived(string id, bool value)
        {
            var filter = Builders<SecondaryTicket>.Filter.Eq("Id", new ObjectId(id));
            var update = Builders<SecondaryTicket>.Update.Set("received", value);

            secondaryTickets.UpdateOne(filter, update);
        }

    }
}
