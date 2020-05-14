using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TTService {
    public class TTService : ITTService {

        private static Database db = new Database();

        TTService() {
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

        public DataTable GetTicketsByUser(string author) {

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

        public List<User> GetUsersMongo()
        {
            return db.GetUsers();
        }
    }
}
