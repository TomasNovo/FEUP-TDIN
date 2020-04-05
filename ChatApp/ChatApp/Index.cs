using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Collections;

namespace ChatApp
{
    public partial class Index : Form
    {
        string IP;
        int Port;
        TcpClient tcp;
        
        string message = "Hello I'm the client";


        public Index()
        {
            InitializeComponent();
        }

        private void Index_Load(object sender, EventArgs e)
        {
            DrawUsers();
        }

        // Connect button
        private void button1_Click(object sender, EventArgs e)
        {
            //IP = textBox1.Text;
            //Port = Int32.Parse(textBox2.Text);

            tcp = new TcpClient(IP, Port);
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(message);

            //---send the text---
            Console.WriteLine("Sending : " + message);
            NetworkStream nwStream = tcp.GetStream();
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

            //---read back the text---
            byte[] bytesToRead = new byte[tcp.ReceiveBufferSize];
            int bytesRead = nwStream.Read(bytesToRead, 0, tcp.ReceiveBufferSize);
            Console.WriteLine("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
            Console.ReadLine();
            tcp.Close();

        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.Instance.Close();
        }

        private void DrawUsers()
        {
            ArrayList users = MainForm.Instance.client.GetUsers();

            for (int i = 0; i < users.Count; i++)
            {
                CircularButton cb = new CircularButton();
                TextBox temp = new System.Windows.Forms.TextBox();
                this.Controls.Add(temp);
                temp.Parent.Controls.SetChildIndex(temp, 2);
                cb.Parent = this;
                cb.Parent.Controls.SetChildIndex(cb, 2);
                cb.FlatStyle = FlatStyle.Flat;
                cb.FlatAppearance.BorderSize = 0;
                cb.Size = new System.Drawing.Size(10, 10);
                cb.TabStop = false;
                temp.Size = new System.Drawing.Size(70, 10);

                if (i == 0)
                {
                    cb.Location = new Point(575, 57);
                    cb.BackColor = Color.Green;
                    temp.Location = new Point(500, 55);
                }
                else
                {
                    cb.Location = new Point(575, 57 + i*25);
                    cb.BackColor = Color.Gray;
                    temp.Location = new Point(500, 55 + i * 25);

                }

                temp.BackColor = Color.DarkGray;
                temp.BorderStyle = BorderStyle.None;
                temp.Text = " " + users[i].ToString();
            }
        }

        private void Icon_Click(object sender, EventArgs e)
        {
            // Each icon will be a group chat
        }
    }


    public class CircularButton : Button
    {
        protected override void OnPaint(PaintEventArgs pevent)
        {
            GraphicsPath grpath = new GraphicsPath();
            grpath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            this.Region = new System.Drawing.Region(grpath);
            base.OnPaint(pevent);
        }
    }
}
