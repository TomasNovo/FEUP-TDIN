using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using System.Windows.Forms;
using TTService;

namespace TTClient
{
    public partial class Form2 : Form
    {
        TTProxy proxy;
        List<string> users;

        public Form2()
        {
            InitializeComponent();
            proxy = new TTProxy();

            users = proxy.GetUsersMongo();

            //Hide elements
            listBox2.Visible = false;
            dataGridView1.Visible = false;

            for (int i = 0; i < users.Count; i++)
            {
                listBox2.Items.Add(users[i]);
            }
        }

        // View by username
        private void button1_Click(object sender, EventArgs e)
        {
            listBox2.Visible = true;
            dataGridView1.Visible = true;
        }

        // View all tickets
        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Visible = false;
            dataGridView1.Visible = false;
        }
    }

    class TTProxy : ClientBase<ITTService>, ITTService
    {
        public DataTable GetUsers()
        {
            return Channel.GetUsers();
        }

        public DataTable GetTickets(string author)
        {
            return Channel.GetTickets(author);
        }

        public int AddTicket(string author, string desc)
        {
            return Channel.AddTicket(author, desc);
        }

        //our methods
        public int AddUserToDB(string username, string email)
        {
            return Channel.AddUserToDB(username, email);
        }

        public int AddTicketToDB(string username, System.DateTime date, string title, string description)
        {
            return Channel.AddTicketToDB(username, date, title, description);
        }
        public List<string> GetUsersMongo()
        {
            return Channel.GetUsersMongo();
        }

    }
}
