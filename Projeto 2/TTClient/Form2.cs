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
        DataTable users;
        int state = 0; // 0 = login // 1 = ?
        string username;

        public Form2()
        {
            InitializeComponent();
            proxy = new TTProxy();

            users = proxy.GetUsers();

            //Hide elements
            listBox2.Visible = false;
            dataGridView1.Visible = false;

            for (int i = 0; i < users.Rows.Count; i++)
            {
                listBox2.Items.Add(users.Rows[i][1]);
            }

            UpdateByState();
        }

        // View by username
        private void button1_Click(object sender, EventArgs e)
        {
            listBox2.Visible = true;
            dataGridView1.Visible = true;

            if (listBox2.SelectedIndex == -1)
                listBox2.SelectedIndex = 0;

            UpdateUserTickets();
        }

        // View all tickets
        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Visible = false;
            dataGridView1.Visible = true;

            dataGridView1.DataSource = proxy.GetTickets();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUserTickets();
        }

        private void UpdateUserTickets()
        {
            DataTable tickets = proxy.GetTicketsByUser((string)users.Rows[listBox2.SelectedIndex][1]);
            dataGridView1.DataSource = tickets;
        }

        private void UpdateByState()
        {
            if (state == 0)
            {
                button1.Visible = false;
                button3.Visible = false;
                listBox2.Visible = false;
                dataGridView1.Visible = false;

                label3.Visible = true;
                textBox1.Visible = true;
                button2.Visible = true;
            }
            else if (state == 1)
            {
                button1.Visible = true;
                button3.Visible = true;
                listBox2.Visible = true;
                dataGridView1.Visible = true;

                label3.Visible = false;
                textBox1.Visible = false;
                button2.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            username = textBox1.Text;
            state = 1;
            UpdateByState();

            label1.Text = $"Welcome, {username} !";
        }
    }
}
