﻿using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using System.Windows.Forms;
using System.Net.Mail;
using TTService;
using System.Drawing;

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
            users = null;
            //Hide elements
            //listBox2.Visible = false;
            //dataGridView1.Visible = false;


            UpdateByState();
        }


        // Navbar
            // Selector
        private void position(Button b)
        {
            panel2.Visible = true;
            panel2.Location = new Point(b.Location.X , b.Location.Y);
        }

            // Selected backgroud
        private void selected(Button b)
        {
            foreach(Control c in panel1.Controls)
            {
                if(c.GetType() == typeof(Button))
                {
                    if(c.Name.Equals(b.Name))
                    {
                        b.BackColor = Color.FromArgb(24, 26, 27);
                        b.ForeColor = Color.White;
                    }
                    else
                    {
                        c.BackColor = Color.Gray;
                        b.ForeColor = Color.White;
                    }
                }
            }
        }


            // View tickets
        private void button7_Click(object sender, EventArgs e)
        {
            if(state != 0)
            {
                state = 2;
                selected(button7);
                position(button7);

                UpdateByState();
            }
        }

            // Ask question
        private void button8_Click(object sender, EventArgs e)
        {
            if (state != 0)
            {
                state = 3;
                selected(button8);
                position(button8);

                UpdateByState();
            }
        }

            // Resolve ticket
        private void button9_Click(object sender, EventArgs e)
        {
            if (state != 0)
            {
                state = 4;
                selected(button9);
                position(button9);

                UpdateByState();
            }
        }

        // View by username
        private void button1_Click(object sender, EventArgs e)
        {
            users = proxy.GetUsers();
            listBox2.Items.Clear();

            for (int i = 0; i < users.Rows.Count; i++)
            {
                listBox2.Items.Add(users.Rows[i][1]);
            }

            listBox2.Visible = true;
            dataGridView1.Visible = true;
            button5.Visible = false;
            button6.Visible = true;
            //dataGridView1.Location = new Point(190, 140);
            //dataGridView1.Size = new Size(618, 244);

            if (listBox2.Items.Count == 0)
            {
                CustomOkMessageBox box = new CustomOkMessageBox("There are no users registed !");
                box.Show();
                return;
            }

            if (listBox2.SelectedIndex == -1)
                listBox2.SelectedIndex = 0;

            UpdateUserTickets();
        }

        // View all tickets
        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Visible = false;
            dataGridView1.Visible = true;
            button5.Visible = true;
            button6.Visible = true;
            //dataGridView1.Location = new Point(119, 140);
            //dataGridView1.Size = new Size(857, 244);


            dataGridView1.DataSource = proxy.GetTickets();

            users = null;

            //dataGridView1.DataSource.
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
                button5.Visible = false;
                button6.Visible = false;
                panel2.Visible = false;
                button4.Visible = false;

                label3.Visible = true;
                textBox1.Visible = true;
                button2.Visible = true;
            }
            else if (state == 1)
            {
                button1.Visible = false;
                button3.Visible = false;
                listBox2.Visible = false;
                dataGridView1.Visible = false;

                button6.Visible = false;

                label3.Location = new Point(336, 190);
                label3.Text = "Select an item from the navbar to start working !";
                textBox1.Visible = false;
                button2.Visible = false;
                button5.Visible = false;
            }
            else if(state == 2) // view tickets 
            {
                button1.Visible = true;
                button3.Visible = true;
                label3.Visible = false;

                button4.Visible = false;
            }
            else if (state == 3) // ask question
            {
                button1.Visible = false;
                button3.Visible = false;
                label3.Visible = false;

                listBox2.Visible = false;
                dataGridView1.Visible = false;
                button6.Visible = false;
                button5.Visible = false;
                button4.Visible = false;
            }
            else if (state == 4) // resolve ticket
            {
                button4.Visible = true;
                button1.Visible = false;
                button3.Visible = false;
                label3.Visible = false;
                listBox2.Visible = false;
                dataGridView1.Visible = false;
                button6.Visible = false;
                button5.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            username = textBox1.Text;
            state = 1;
            UpdateByState();

            label1.Text = $"Welcome, {username} !";
        }

        //email send
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int id = 3;
                string title = "Ganda titulo";
                string description = "Ganda descrição";
                string solution = "You should go to ISEP";
                string to = "up201604503@fe.up.pt";

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.sapo.pt");

                mail.From = new MailAddress("TicketFactoryTDIN@sapo.pt");
                mail.To.Add(to);
                mail.Subject = "[Ticket Factory] Solved Ticket with id " + id.ToString();
                mail.Body = "Recently you submitted the following ticket: \n\n"+
                   title + '\n' + description + "\n\n" + "We propose the following solution: \n" + solution;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("TicketFactoryTDIN@sapo.pt", "TDINamite420");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                CustomOkMessageBox box = new CustomOkMessageBox("Email sent to " + to + " !");
                box.Show();

            }
            catch (Exception ex)
            {
                CustomOkMessageBox box = new CustomOkMessageBox(ex.ToString());
                box.Show();
            }
        }

        // view unassigned
        private void button5_Click(object sender, EventArgs e)
        {
            DataTable all = proxy.GetTickets();

            for(int i = 0; i < all.Rows.Count; i++)
            {
                if (!all.Rows[i][5].ToString().Equals("Unassigned"))
                {
                    all.Rows.RemoveAt(i);
                }
            }

            dataGridView1.DataSource = all;
        }

        // self-assign
        private void button6_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 1)
            {
                CustomOkMessageBox box = new CustomOkMessageBox("It's better if you assign only one ticket at time!");
                box.Show();
            }
            
        }
    }
}
