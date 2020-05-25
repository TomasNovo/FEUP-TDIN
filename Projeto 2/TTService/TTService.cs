using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.ServiceModel;

namespace TTService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class TTService : ITTService
    {

        private Database db = new Database();
        private DepartmentQueue departmentQueue = new DepartmentQueue();

        public TTService()
        {
            departmentQueue.db = db;
            departmentQueue.FetchSecondaryTickets();
            departmentQueue.Listen();
        }

        public int AddTicket(string author, string email, string title, string description)
        {
            DateTime date = DateTime.Now;

            db.Register(author, email);

            db.AddTicket(author, date, title, description);

            return 0;
        }

        public DataTable GetUsers()
        {
            List<User> users = db.GetUsers();
            DataTable dt = new DataTable("users");

            dt.Columns.Add("id");
            dt.Columns.Add("username");
            dt.Columns.Add("email");

            for (int i = 0; i < users.Count; i++)
            {
                User user = users[i];

                List<string> arr = new List<string>();
                arr.Add(user.Id.ToString());
                arr.Add(user.username);
                arr.Add(user.email);

                dt.Rows.Add(arr.ToArray());
            }

            return dt;
        }

        public DataTable GetTicketsByUser(string author)
        {

            List<Ticket> tickets;
            if (author == "")
                tickets = db.GetTickets();
            else
                tickets = db.GetTickets(author);

            DataTable dt = new DataTable("tickets");

            dt.Columns.Add("id");
            dt.Columns.Add("username");
            dt.Columns.Add("date");
            dt.Columns.Add("title");
            dt.Columns.Add("description");
            dt.Columns.Add("status");
            dt.Columns.Add("solver");

            for (int i = 0; i < tickets.Count; i++)
            {
                Ticket ticket = tickets[i];

                List<string> arr = new List<string>();
                arr.Add(ticket.Id.ToString());
                arr.Add(ticket.user);
                arr.Add(ticket.date.ToString());
                arr.Add(ticket.title);
                arr.Add(ticket.description);
                arr.Add(ticket.status.ToString());
                arr.Add(ticket.solver);

                dt.Rows.Add(arr.ToArray());
            }

            return dt;
        }

        public DataTable GetTickets()
        {
            return GetTicketsByUser("");
        }

        //Our methods
        public int AddUserToDB(string username, string email)
        {
            db.Register(username, email);
            return 0;
        }

        public int AddTicketToDB(string username, DateTime date, string title, string description)
        {
            db.AddTicket(username, date, title, description);
            return 0;
        }

        public int AssignSolver(string solver, string id)
        {
            db.AssignSolver(solver, id);
            return 0;
        }

        public DataTable GetTicketsSolver(string s)
        {
            //return db.GetTicketsSolver(s);
            List<Ticket> tickets = db.GetTicketsSolver(s);

            DataTable dt = new DataTable("tickets");

            dt.Columns.Add("id");
            dt.Columns.Add("username");
            dt.Columns.Add("date");
            dt.Columns.Add("title");
            dt.Columns.Add("description");
            dt.Columns.Add("status");
            dt.Columns.Add("solver");

            for (int i = 0; i < tickets.Count; i++)
            {
                Ticket ticket = tickets[i];

                List<string> arr = new List<string>();
                arr.Add(ticket.Id.ToString());
                arr.Add(ticket.user);
                arr.Add(ticket.date.ToString());
                arr.Add(ticket.title);
                arr.Add(ticket.description);
                arr.Add(ticket.status.ToString());
                arr.Add(ticket.solver);

                dt.Rows.Add(arr.ToArray());
            }

            return dt;
        }


        public List<User> GetUsersMongo()
        {
            return db.GetUsers();
        }

        public int AddSecondaryTicket(string originalTicketId, string solver, string secondarySolver, string title, string question)
        {
            SecondaryTicket st = db.AddSecondaryTicket(originalTicketId, solver, secondarySolver, title, question);

            db.ChangeTicketStatus(originalTicketId, TicketStatus.WaitingForAnswers);

            //secondaryTickets.Enqueue(st);

            return 0;
        }

        public int AddSecondaryTicketNewQuestions(string id, string originalTicketId, string solver, string secondarySolver, string title, List<string> questions, List<string> answers)
        {
            db.AddSecondaryTicketNewQuestions(id, solver, secondarySolver, title, questions, answers);

            db.ChangeTicketStatus(originalTicketId, TicketStatus.WaitingForAnswers);

            //secondaryTickets.Enqueue(st);

            return 0;
        }

        public int ChangeSecondaryTicketAnswer(string id, string response)
        {
            db.ChangeSecondaryTicketAnswer(id, response);
            return 0;
        }

        public DataTable GetSecondaryTickets()
        {
            List<SecondaryTicket> tickets = db.GetSecondaryTickets();

            DataTable dt = new DataTable("secondaryTickets");

            dt.Columns.Add("id");
            dt.Columns.Add("originalticketId");
            dt.Columns.Add("solver");
            dt.Columns.Add("secondarysolver");
            dt.Columns.Add("date");
            dt.Columns.Add("title");

            for (int i = 0; i < tickets.Count; i++)
            {
                SecondaryTicket ticket = tickets[i];

                List<string> arr = new List<string>();
                arr.Add(ticket.Id.ToString());
                arr.Add(ticket.originalTicketId.ToString());
                arr.Add(ticket.solver);
                arr.Add(ticket.secondarySolver);
                arr.Add(ticket.date.ToString());
                arr.Add(ticket.title);

                for(int u = 0; u < ticket.questions.Count; u++)
                {
                    dt.Columns.Add("question:" + u);
                    dt.Columns.Add("answers: " + u);
                    arr.Add(ticket.questions[u]);
                    arr.Add(ticket.answers[u]);
                }

                dt.Rows.Add(arr.ToArray());
            }

            return dt;
        }

        public DataTable GetSecondaryTicketsBySolver(string solver)
        {
            try
            {
                List<SecondaryTicket> tickets = db.GetSecondaryTicketsBySolver(solver);

                DataTable dt = new DataTable("secondaryTickets");

                dt.Columns.Add("id");
                dt.Columns.Add("originalticketId");
                dt.Columns.Add("solver");
                dt.Columns.Add("secondarysolver");
                dt.Columns.Add("date");
                dt.Columns.Add("title");

                for (int i = 0; i < tickets.Count; i++)
                {
                    SecondaryTicket ticket = tickets[i];

                    List<string> arr = new List<string>();
                    arr.Add(ticket.Id.ToString());
                    arr.Add(ticket.originalTicketId.ToString());
                    arr.Add(ticket.solver);
                    arr.Add(ticket.secondarySolver);
                    arr.Add(ticket.date.ToString());
                    arr.Add(ticket.title);

                    for (int u = 0; u < ticket.questions.Count; u++)
                    {
                        if (!dt.Columns.Contains("question:" + u))
                        {
                            dt.Columns.Add("question:" + u);
                        }

                        if (!dt.Columns.Contains("answers: " + u))
                        {
                            dt.Columns.Add("answers: " + u);
                        }

                        arr.Add(ticket.questions[u]);
                        arr.Add(ticket.answers[u]);
                    }


                    dt.Rows.Add(arr.ToArray());
                }

                return dt;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        public DataTable GetSecondaryTicketsBySecondarySolver(string secondarySolver)
        {
            List<SecondaryTicket> tickets = db.GetSecondaryTicketsBySecondarySolver(secondarySolver);
            Console.WriteLine(tickets.Count);

            DataTable dt = new DataTable("secondaryTickets");

            dt.Columns.Add("id");
            dt.Columns.Add("originalticketId");
            dt.Columns.Add("solver");
            dt.Columns.Add("secondarysolver");
            dt.Columns.Add("date");
            dt.Columns.Add("title");

            for (int i = 0; i < tickets.Count; i++)
            {
                SecondaryTicket ticket = tickets[i];

                List<string> arr = new List<string>();
                arr.Add(ticket.Id.ToString());
                arr.Add(ticket.originalTicketId.ToString());
                arr.Add(ticket.solver);
                arr.Add(ticket.secondarySolver);
                arr.Add(ticket.date.ToString());
                arr.Add(ticket.title);

                for (int u = 0; u < ticket.questions.Count; u++)
                {
                    dt.Columns.Add("question:" + u);
                    dt.Columns.Add("answers: " + u);
                    arr.Add(ticket.questions[u]);
                    arr.Add(ticket.answers[u]);
                }

                dt.Rows.Add(arr.ToArray());
            }

            return dt;
        }

        public int DeleteSecondaryTicket(string originalTicketId)
        {
            db.DeleteSecondaryTicket(originalTicketId);
            return 0;
        }

        public int ChangeTicketStatus(string id, TicketStatus status)
        {
            db.ChangeTicketStatus(id, status);
            return 0;
        }

        public void Ping()
        {

        }

    }
}
