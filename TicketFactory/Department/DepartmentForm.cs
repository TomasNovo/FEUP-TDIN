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
    public partial class DepartmentForm : Form
    {
        int state = 0;
        string username;
        string department;
        DataTable tickets;

        DepartmentSocketClient socketClient;

        public DepartmentForm()
        {
            InitializeComponent();

            panel3.AutoScroll = false;
            panel3.HorizontalScroll.Enabled = false;
            panel3.HorizontalScroll.Visible = false;
            panel3.HorizontalScroll.Maximum = 0;
            panel3.AutoScroll = true;

            UpdateByState();

        }

        private void UpdateByState()
        {
            if (state == 0)
            {
                panel2.Visible = false;
                label1.Visible = true;
                label3.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                button2.Visible = true;
                dataGridView2.Visible = false;
                label5.Visible = false;
                label7.Visible = false;
                //label6.Visible = false;
                label8.Visible = false;
                //textBox3.Visible = false;
                textBox4.Visible = false;
                button1.Visible = false;
                button13.Visible = false;
                panel3.Visible = false;

                //info
                label2.Visible = false;
                label4.Visible = false;
            }
            else if(state == 1)
            {
                panel2.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                button2.Visible = false;
                dataGridView2.Visible = false;
                label5.Visible = false;
                label7.Visible = false;
                //label6.Visible = false;
                label8.Visible = false;
                //textBox3.Visible = false;
                textBox4.Visible = false;
                button1.Visible = false;
                button13.Visible = false;
                panel3.Visible = false;

                label3.Text = "Select an item from the navbar to start working !";

                //info
                label2.Visible = true;
                label4.Visible = true;

            }
            else if(state == 2) // view all questions to take
            {

                panel2.Visible = true;
                dataGridView2.Visible = true;
                label1.Visible = true;
                label5.Visible = false;
                label7.Visible = false;
                //label6.Visible = false;
                label8.Visible = false;
                //textBox3.Visible = false;
                textBox4.Visible = false;
                button13.Visible = true;
                panel3.Visible = false;

                label3.Text = "Select original ticket id to see its info or its talks";
                label3.Visible = true;

                button1.Visible = false;

            }
            else if (state == 3) // view all current questions taked
            {

                panel2.Visible = true;
                label1.Visible = false;
                label3.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                button2.Visible = false;
                dataGridView2.Visible = false;
                label5.Visible = true;
                label7.Visible = true;
                //label6.Visible = true;
                label8.Visible = true;
                //textBox3.ReadOnly = true;
                //textBox3.Visible = true;
                textBox4.Visible = true;
                button1.Visible = true;
                button13.Visible = true;
                panel3.Visible = true;
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

            socketClient = new DepartmentSocketClient(department);

            label4.Text += username;
            label2.Text += department;

            state = 1;
            UpdateByState();

            label1.Text = $"Welcome, {username} !";

            button7_Click(this, EventArgs.Empty);
        }

        //received questions
        private void button7_Click(object sender, EventArgs e)
        {
            if (state != 0)
            {
                state = 2;
                selected(button7);
                position(button7);

                socketClient.StartClient($"{SocketConstants.ID} {department}", () =>
                {
                    InvokeIfRequired(() =>
                    {
                        GetSecondaryTicketsFromSocket();
                        UpdateByState();
                    });
                });

            }
        }

        public void GetSecondaryTicketsFromSocket()
        {
            InvokeIfRequired(() =>
            {
                tickets = new DataTable("secondaryTickets");

                tickets.Columns.Add("id");
                tickets.Columns.Add("originalticketId");
                tickets.Columns.Add("solver");
                tickets.Columns.Add("secondarysolver");
                tickets.Columns.Add("date");
                tickets.Columns.Add("title");

                for (int i = 0; i < socketClient.secondaryTickets.Count; i++)
                {
                    SecondaryTicket ticket = socketClient.secondaryTickets[i];

                    List<string> arr = new List<string>();
                    arr.Add(ticket.Id.ToString());
                    arr.Add(ticket.originalTicketId.ToString());
                    arr.Add(ticket.solver);
                    arr.Add(ticket.secondarySolver);
                    arr.Add(ticket.date.ToString());
                    arr.Add(ticket.title);

                    for (int u = 0; u < ticket.questions.Count; u++)
                    {
                        if (!tickets.Columns.Contains("question:" + u))
                            tickets.Columns.Add("question:" + u);

                        if (!tickets.Columns.Contains("answers: " + u))
                            tickets.Columns.Add("answers: " + u);

                        arr.Add(ticket.questions[u]);
                        arr.Add(ticket.answers[u]);
                    }

                    tickets.Rows.Add(arr.ToArray());
                }

                dataGridView2.DataSource = tickets;
            });

           
        }

        private delegate void MainThreadCode();

        private void InvokeIfRequired(MainThreadCode code)
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)delegate { code(); });
            else
                code();
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
                    string solver = Convert.ToString(selectedRow.Cells["solver"].Value);

                    SecondaryTicket st = socketClient.secondaryTickets.Find(x => x.Id.ToString() == (string)selectedRow.Cells["id"].Value);

                    List<string> q = st.questions;
                    List<string> a = st.answers;

                    string question = Convert.ToString(selectedRow.Cells["solver"].Value);

                    label7.Text = "Ticket ID: " + id;
                    label5.Text = "Ticket Title: " + title;
                    textBox4.Text = "";

                    //q.Reverse();
                    //a.Reverse();

                    // Chat
                    this.panel3.Controls.Clear();
                    for (int i = 0; i < q.Count; i++)
                    {
                        TextBox temp = new TextBox();
                        temp.ReadOnly = true;
                        temp.Multiline = true;
                        temp.Location = new Point(0, 0 + 40 * i);
                        temp.Size = new Size(806, 20);
                        temp.ForeColor = Color.White;
                        temp.BackColor = Color.FromArgb(24, 26, 27);
                        temp.Text = solver + ": " + q[i];
                        temp.BorderStyle = BorderStyle.None;

                        this.panel3.Controls.Add(temp);

                        if (!a[i].Equals("waiting for answer"))
                        {
                            TextBox temp2 = new TextBox();
                            temp2.ReadOnly = true;
                            temp2.Multiline = true;
                            temp2.Location = new Point(0, 20 + 40 * i);
                            temp2.Size = new Size(806, 20);
                            temp2.ForeColor = Color.White;
                            temp2.BackColor = Color.FromArgb(24, 26, 27);
                            temp2.BorderStyle = BorderStyle.None;
                            temp2.Text = department + ": " + a[i];
                            this.panel3.Controls.Add(temp2);
                        }
                    }
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
            int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView2.Rows[selectedrowindex];
            string id = (string)selectedRow.Cells[0].Value;

            string response = textBox4.Text;

            socketClient.StartClient($"{SocketConstants.ANSWER} {id} {response}", () => 
            {
                InvokeIfRequired(() =>
                {
                    List<SecondaryTicket> tickets = socketClient.secondaryTickets;
                    int index = tickets.FindIndex(x => x.Id.ToString() == id);

                    if (index != -1)
                        tickets[index].answers[tickets[index].answers.Count - 1] = response;

                    socketClient.UpdateSecondaryTicketsFile();

                    CustomOkMessageBox box = new CustomOkMessageBox("Answer submitted with success");
                    box.Show();

                    button10_Click(this, EventArgs.Empty);
                });
            }
            );
            
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
