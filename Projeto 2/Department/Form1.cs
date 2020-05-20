using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Department
{
    public partial class Form1 : Form
    {
        int state = 0;
        string username;
        string department;
        public Form1()
        {
            InitializeComponent();
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

                //info
                label2.Visible = true;
                label4.Visible = true;
            }
            else if(state == 2) // view all questions to take
            {

            }
            else if (state == 3) // view all current questions taked
            {

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
