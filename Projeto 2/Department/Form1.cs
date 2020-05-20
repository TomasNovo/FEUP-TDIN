using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTService;

namespace Department
{
    public partial class Form1 : Form
    {
        TTProxy proxy;
        int state = 0;
        string username;
        string department;

        DepartmentSocketClient socketClient = new DepartmentSocketClient();

        public Form1()
        {
            InitializeComponent();

            proxy = new TTProxy();

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

                //info
                label2.Visible = true;
                label4.Visible = true;

            }
            else if(state == 2) // view all questions to take
            {
                dataGridView2.Visible = true;

            }
            else if (state == 3) // view all current questions taked
            {
                dataGridView2.Visible = false;
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

        //submit
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

                UpdateByState();
            }
        }
    }
}
