using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using System.Windows.Forms;
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
        MailTicket m = null;

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

        // Make ENTER key submit
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button2_Click(null, null);
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

            // My tickets 
        private void button10_Click(object sender, EventArgs e)
        {
            if (state != 0)
            {
                state = 5;
                selected(button10);
                position(button10);


                DataTable tickets = proxy.GetTicketsSolver(username);
                dataGridView1.DataSource = tickets;
                dataGridView1.Visible = true;

                UpdateByState();
            }
        }

        // View by username
        private void button1_Click(object sender, EventArgs e)
        {
            if (state == 2)
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
            else if (state == 6)
            {
                DataTable dt = proxy.GetSecondaryTicketsBySolver(username);
                string ticketId = textBox4.Text;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if ((string)dt.Rows[i][1] != ticketId)
                    {
                        dt.Rows.RemoveAt(i);
                        i--;
                    }
                }

                dataGridView2.DataSource = dt;
            }

        }

        // View all tickets
        private void button3_Click(object sender, EventArgs e)
        {
            if (state == 2)
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
            else if (state == 6)
            {
                dataGridView2.DataSource = proxy.GetSecondaryTicketsBySolver(username);
            }
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
                textBox2.Visible = false;
                textBox3.Visible = false;

                label3.Visible = true;
                textBox1.Visible = true;
                button2.Visible = true;

                label2.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                textBox2.Visible = false;
                textBox4.Visible = false;
                button11.Visible = false;
                button12.Visible = false;
                dataGridView2.Visible = false;
                label7.Visible = false;
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

                textBox2.Visible = false;
                textBox3.Visible = false;

                label2.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                textBox2.Visible = false;
                textBox4.Visible = false;
                button11.Visible = false;
                button12.Visible = false;
                dataGridView2.Visible = false;
                label7.Visible = false;
            }
            else if(state == 2) // view tickets 
            {
                button3.Text = "View all tickets by ID";
                button1.Text = "View tickets by Username";
                button1.Visible = true;
                button3.Visible = true;
                label3.Visible = false;
                label1.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = false;
                dataGridView1.Visible = false;
                button4.Visible = false;

                label2.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                textBox2.Visible = false;
                textBox4.Visible = false;
                button11.Visible = false;
                button12.Visible = false;
                dataGridView2.Visible = false;
                label7.Visible = false;

                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
            else if (state == 3) // ask secondary question
            {
                label1.Text = "Ask a secondary question, " + username;
                textBox2.Visible = true;
                textBox3.Visible = true;

                button1.Visible = false;
                button3.Visible = false;
                label3.Visible = false;
                label1.Visible = true;

                listBox2.Visible = false;
                dataGridView1.Visible = false;
                button6.Visible = false;
                button5.Visible = false;
                button4.Visible = false;

                label2.Visible = true;
                label5.Text = "Department ID:";
                label5.Visible = true;
                label6.Visible = true;
                textBox2.Visible = true;
                textBox4.Visible = true;
                button11.Visible = true;
                button12.Visible = true;
                button12.Text = "View Secondary Tickets";
                dataGridView2.Visible = false;
                
                label7.Visible = true;
                if (TicketSelected())
                {
                    string id = (string)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["Id"].Value;
                    //string num = Convert.ToString(selectedRow.Cells["status"].Value);
                    label7.Text = $"Ticket ID: {id}";
                }

                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
            else if (state == 4) // resolve ticket
            {
                label1.Visible = false;
                button4.Visible = true;
                button1.Visible = false;
                button3.Visible = false;
                label3.Visible = false;
                listBox2.Visible = false;
                dataGridView1.Visible = false;
                button6.Visible = false;
                button5.Visible = false;

                label2.Visible = false;
                label6.Visible = false;
                textBox2.Visible = false;
                textBox4.Visible = false;
                button11.Visible = false;
                button12.Visible = false;
                dataGridView2.Visible = false;
                
                label7.Visible = true;

                string username = "";
                string mail = "";
                string id = ""; 
                string title = "";
                string description = "";

                if (TicketSelected())
                {
                    id = (string)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["Id"].Value;
                    username = (string)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["username"].Value;
                    title = (string)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["title"].Value;
                    description = (string)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["description"].Value;
                    label7.Text = $"Ticket ID: {id}";
                }

                users = proxy.GetUsers();

                for (int i = 0; i < users.Rows.Count; i++)
                {
                    if (users.Rows[i][1].Equals(username))
                    {
                        mail = users.Rows[i][2].ToString();
                    }
                }

                textBox3.Visible = true;
                textBox2.Visible = true;
                label5.Text = "Mail to: " + mail;
                label5.Visible = true;

                m = new MailTicket(id, title, description, mail, textBox3.Text);

            }
            else if (state == 5) // my tickets
            {
                textBox2.Visible = false;
                textBox3.Visible = false;
                panel2.Visible = true;
                label1.Text = "See your tickets, " + username;
                label1.Visible = true;
                button4.Visible = false;
                button1.Visible = false;
                button3.Visible = false;
                label3.Visible = false;
                listBox2.Visible = false;
                dataGridView1.Visible = true;
                button6.Visible = false;
                button5.Visible = false;

                label2.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                textBox2.Visible = false;
                textBox4.Visible = false;
                button11.Visible = false;
                button12.Visible = false;
                dataGridView2.Visible = false;
                label7.Visible = false;
            }

            else if (state == 6) // view secondary tickets
            {
                label1.Text = "See your secondary tickets, " + username;
                label1.Visible = true;
                textBox2.Visible = false;
                textBox3.Visible = false;

                button1.Text = "View by ticket id";
                button3.Text = "View all";
                button1.Visible = true;
                button3.Visible = true;
                label3.Visible = false;

                listBox2.Visible = false;
                dataGridView1.Visible = true;
                button6.Visible = false;
                button5.Visible = false;
                button4.Visible = false;

                label2.Visible = false;
                label5.Text = "Ticket ID:";
                label5.Visible = true;
                label6.Visible = false;
                textBox2.Visible = false;
                textBox4.Visible = true;
                button11.Visible = false;
                button12.Visible = true;
                button12.Text = "Submit Secondary Ticket";
                dataGridView2.Visible = true;
                label7.Visible = false;

                textBox4.Text = "";
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
            m.sendMail();
        }

        // view unassigned
        private void button5_Click(object sender, EventArgs e)
        {
            DataTable all = proxy.GetTickets();


            for (int i = all.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = all.Rows[i];
                if (!all.Rows[i][5].ToString().Equals(TicketStatus.Unassigned.ToString()))
                    dr.Delete();
            }
            all.AcceptChanges();
           
            dataGridView1.DataSource = all;
        }

        // self-assign
        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 1)
            {
                CustomOkMessageBox box = new CustomOkMessageBox("It's better if you assign only one ticket at time!");
                box.Show();
                return;
            }

            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            string id = Convert.ToString(selectedRow.Cells["Id"].Value);
            string num = Convert.ToString( selectedRow.Cells["status"].Value);



            Console.WriteLine(num);

            if(!num.Equals(TicketStatus.Unassigned.ToString()))
            {
                CustomOkMessageBox box = new CustomOkMessageBox("Ticket is not available to self assign");
                box.Show();
                return;
            }

            proxy.AssignSolver(username, id);
        }

        // Submit question
        private void button11_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 1)
            {
                CustomOkMessageBox box = new CustomOkMessageBox("It's better if you assign select one ticket at time!");
                box.Show();
                return;
            }

            if (TicketSelected())
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                string id = Convert.ToString(selectedRow.Cells["Id"].Value);

                proxy.AddSecondaryTicket(id, username, textBox4.Text, textBox2.Text, textBox3.Text);

                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";

            }
        }

        private bool TicketSelected()
        {
            return dataGridView1.SelectedCells.Count == 1;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (state == 3)
            {
                state = 6;
                UpdateByState();

                dataGridView2.DataSource = proxy.GetSecondaryTicketsBySolver(username);
            }
            else if (state == 6)
            {
                state = 3;
                UpdateByState();
            }
        }

    }
}
