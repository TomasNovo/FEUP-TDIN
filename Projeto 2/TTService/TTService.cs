﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TTService {
    public class TTService : ITTService {
        readonly string database;

        private static Database db = new Database();

        TTService() {
            string connection = ConfigurationManager.ConnectionStrings["TTs"].ConnectionString;
            database = String.Format(connection, AppDomain.CurrentDomain.BaseDirectory);
        }

        public int AddTicket(string author, string problem) {
            int id = 0;

            using (SqlConnection c = new SqlConnection(database)) {
                try {
                    c.Open();
                    string sql = "insert into TTickets(Author, Problem, Answer, Status) values (@a1, @p1, '', 1)"; // injection protection
                    SqlCommand cmd = new SqlCommand(sql, c);                                                       // injection protection
                    cmd.Parameters.AddWithValue("@a1", author);                                                    // injection protection
                    cmd.Parameters.AddWithValue("@p1", problem);                                                   // injection protection
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "select max(Id) from TTickets";
                    id = (int)cmd.ExecuteScalar();
                }
                catch (SqlException) {
                }
                finally {
                    c.Close();
                }
            }
            return id;
        }

        public DataTable GetTickets(string author) {
            DataTable result = new DataTable("TTickets");

            using (SqlConnection c = new SqlConnection(database)) {
                try {
                    c.Open();
                    string sql = "select Id, Problem, Status, Answer from TTickets where Author=@a1";
                    SqlCommand cmd = new SqlCommand(sql, c);
                    cmd.Parameters.AddWithValue("@a1", author);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(result);
                }
                catch (SqlException ex) {
                }
                finally {
                    c.Close();
                }
            }
            return result;
        }

        public DataTable GetUsers() {
            DataTable result = new DataTable("Users");

            using (SqlConnection c = new SqlConnection(database)) {
                try {
                    c.Open();
                    string sql = "select * from Employees";
                    SqlCommand cmd = new SqlCommand(sql, c);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(result);
                }
                catch (SqlException) {
                }
                finally {
                    c.Close();
                }
            }
            return result;
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

        public List<string> GetUsersMongo()
        {
            return db.GetUsers();
        }
    }
}
