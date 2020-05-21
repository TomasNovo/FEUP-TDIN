﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TTService
{
    public class TTService : ITTService
    {

        private static Database db = new Database();
        private static DepartmentQueue departmentQueue;

        TTService()
        {
            FetchSecondaryTickets();
        }


        private void FetchSecondaryTickets()
        {
            departmentQueue = new DepartmentQueue();
            departmentQueue.db = db;

            List<SecondaryTicket> tickets = db.GetSecondaryTickets();

            for (int i = 0; i < tickets.Count; i++)
            {
                SecondaryTicket ticket = tickets[i];

                if (!ticket.received)
                    Console.WriteLine(departmentQueue.AddSecondaryTicket(ticket));
            }

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

        public int AddSecondaryTicket(string originalTicketId, string solver, string secondarySolver, string title, string description)
        {
            SecondaryTicket st = db.AddSecondaryTicket(originalTicketId, solver, secondarySolver, title, description);

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
            dt.Columns.Add("description");
            dt.Columns.Add("response");

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
                arr.Add(ticket.description);
                arr.Add(ticket.response);

                dt.Rows.Add(arr.ToArray());
            }

            return dt;
        }

        public DataTable GetSecondaryTicketsBySolver(string solver)
        {
            List<SecondaryTicket> tickets = db.GetSecondaryTicketsBySolver(solver);

            DataTable dt = new DataTable("secondaryTickets");

            dt.Columns.Add("id");
            dt.Columns.Add("originalticketId");
            dt.Columns.Add("solver");
            dt.Columns.Add("secondarysolver");
            dt.Columns.Add("date");
            dt.Columns.Add("title");
            dt.Columns.Add("description");
            dt.Columns.Add("response");

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
                arr.Add(ticket.description);
                arr.Add(ticket.response);

                dt.Rows.Add(arr.ToArray());
            }

            return dt;
        }

        public DataTable GetSecondaryTicketsBySecondarySolver(string secondarySolver)
        {
            List<SecondaryTicket> tickets = db.GetSecondaryTicketsBySecondarySolver(secondarySolver);

            DataTable dt = new DataTable("secondaryTickets");

            dt.Columns.Add("id");
            dt.Columns.Add("originalticketId");
            dt.Columns.Add("solver");
            dt.Columns.Add("secondarysolver");
            dt.Columns.Add("date");
            dt.Columns.Add("title");
            dt.Columns.Add("description");
            dt.Columns.Add("response");

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
                arr.Add(ticket.description);
                arr.Add(ticket.response);

                dt.Rows.Add(arr.ToArray());
            }

            return dt;
        }

    }
}
