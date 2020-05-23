using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTClient;
using TTService;

namespace Department
{
    public partial class Form1 : Form
    {
        TTProxy proxy;
        int state = 0;
        string username;
        string department;
        DataTable tickets;

        DepartmentSocketClient socketClient = new DepartmentSocketClient();

        public Form1()
        {
            InitializeComponent();

            proxy = new TTProxy();
            tickets = proxy.GetTickets();

            UpdateByState();

        }

        private void UpdateByState()
        {
            if (state == 0)
            {
                label1.Visible = true;
                label3.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                button2.Visible = true;
                dataGridView2.Visible = false;
                label5.Visible = false;
                label7.Visible = false;
                label6.Visible = false;
                label8.Visible = false;
                textBox3.Visible = false;
                textBox4.Visible = false;
                button1.Visible = false;
                button13.Visible = false;

                //info
                label2.Visible = false;
                label4.Visible = false;
            }
            else if(state == 1)
            {
                label3.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                button2.Visible = false;
                dataGridView2.Visible = false;
                label5.Visible = false;
                label7.Visible = false;
                label6.Visible = false;
                label8.Visible = false;
                textBox3.Visible = false;
                textBox4.Visible = false;
                button1.Visible = false;
                button13.Visible = false;

                //info
                label2.Visible = true;
                label4.Visible = true;

            }
            else if(state == 2) // view all questions to take
            {
                dataGridView2.Visible = true;
                label1.Visible = true;
                label5.Visible = false;
                label7.Visible = false;
                label6.Visible = false;
                label8.Visible = false;
                textBox3.Visible = false;
                textBox4.Visible = false;
                button13.Visible = true;

                label3.Text = "Select original ticket id to see its info or its talks";
                label3.Visible = true;

                button1.Visible = false;

            }
            else if (state == 3) // view all current questions taked
            {
                label1.Visible = false;
                label3.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                button2.Visible = false;
                dataGridView2.Visible = false;
                label5.Visible = true;
                label7.Visible = true;
                label6.Visible = true;
                label8.Visible = true;
                textBox3.ReadOnly = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                button1.Visible = true;
                button13.Visible = true;
                socketClient.StartClient();

            }
        }

        //Navbar
        // Selector
        private void position(Button b)
        {
            panel2.Visible = true;
            panel2.Location = new Point(b.Location.X, b.Location.Y);
        }

        // Selected backgroud
        private void selected(Button b)
        {
            foreach (Control c in panel1.Controls)
            {
                if (c.GetType() == typeof(Button))
                {
                    if (c.Name.Equals(b.Name))
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

        //submit auth
        private void button2_Click(object sender, EventArgs e)
        {
            username = textBox1.Text;
            department = textBox2.Text;
            socketClient.department = department;

            label4.Text += username;
            label2.Text += department;

            state = 1;
            UpdateByState();

            label1.Text = $"Welcome, {username} !";

        }

        //received questions
        private void button7_Click(object sender, EventArgs e)
        {
            if (state != 0)
            {
                state = 2;
                selected(button7);
                position(button7);

                dataGridView2.DataSource = proxy.GetSecondaryTicketsBySecondarySolver(department);

                UpdateByState();
            }
        }

        // ticket talks
        private void button10_Click(object sender, EventArgs e)
        {
            if (state != 0)
            {
                state = 3;
                selected(button10);
                position(button10);

                if (dataGridView2.SelectedRows.Count > 1)
                {
                    CustomOkMessageBox box = new CustomOkMessageBox("It's better if you assign select one ticket at time!");
                    box.Show();
                    return;
                }

                if (TicketSelected())
                {
                    int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView2.Rows[selectedrowindex];
                    string id = Convert.ToString(selectedRow.Cells["originalTicketId"].Value);
                    string title = Convert.ToString(selectedRow.Cells["title"].Value); 
                    string description = Convert.ToString(selectedRow.Cells["description"].Value);

                    //proxy.AddSecondaryTicket(id, username, textBox4.Text, textBox2.Text, textBox3.Text);
                     
                    label7.Text = "Ticket ID: " + id;
                    label5.Text = "Ticket Title: " + title;
                    textBox3.Text = description;

                }


                UpdateByState();
            }
        }

        private bool TicketSelected()
        {
            return dataGridView2.SelectedCells.Count == 1;
        }

        // submit answer
        private void button1_Click(object sender, EventArgs e)
        {
            string id = label7.Text.Substring(11);
            Console.WriteLine(id);

            string response = textBox4.Text;

            proxy.ChangeSecondaryTicketAnswer(id, response);

            CustomOkMessageBox box = new CustomOkMessageBox("Response submitted with success");
            box.Show();

            state = 1;
        }

        // Ticket info
        private void button13_Click(object sender, EventArgs e)
        {
            string username = "";
            string id = "";
            string title = "";
            string description = "";
            string date = "";


            for (int i = 0; i < tickets.Rows.Count; i++)
            {
                if(tickets.Rows[i][0].Equals((string)dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["originalticketId"].Value))
                {
                    id = tickets.Rows[i][0].ToString();
                    username = tickets.Rows[i][1].ToString();
                    date = tickets.Rows[i][2].ToString();
                    title = tickets.Rows[i][3].ToString();
                    description = tickets.Rows[i][4].ToString();
                }
            }

           

            TicketInfo box = new TicketInfo(id, username, date, title, description);
            box.Show();
        }
    }
}
