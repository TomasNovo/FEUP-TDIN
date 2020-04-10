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
using Common;
using System.Threading;

namespace ChatApp
{
    public partial class Index : Form
    {
        string IP;
        int Port;
        TcpClient tcp;
        ArrayList users;
        ArrayList onlineUsers;
        
        string message = "Hello I'm the client";

        public Index()
        {
            InitializeComponent();


            //MainForm.Instance.client.OnlineUsersChanged += IndexHandler;
        }

        private void Index_Load(object sender, EventArgs e)
        {
            //MainForm.Instance.client.server.OnlineUsersChanged += IndexHandler;

            DrawUsers();
            MainForm.Instance.client.OnlineUsersChanged += IndexHandler;
            //MainForm.Instance.client.server.OnlineUsersChanged += IndexHandler;
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
            Console.WriteLine("Index was closed");
            MainForm.Instance.Close();
        }

        private void DrawUsers()
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)delegate { DrawUsers(); });
            else
            {
                TUsername.Text = "Do you wanna chat, " + MainForm.Instance.client.UserName + " ?";

                users = MainForm.Instance.client.GetUsers();
                onlineUsers = MainForm.Instance.client.server.GetOnlineUsers();
                users.Remove(MainForm.Instance.client.UserName);
                onlineUsers.Remove(MainForm.Instance.client.UserName);

                OrderOnlineFirst(users, onlineUsers);
                

                for (int i = 0; i < users.Count; i++)
                {
                    CircularButton cb = new CircularButton();
                    cb.Parent = this;
                    cb.Parent.Controls.SetChildIndex(cb, 2);
                    cb.FlatStyle = FlatStyle.Flat;
                    cb.FlatAppearance.BorderSize = 0;
                    cb.Size = new System.Drawing.Size(10, 10);
                    cb.TabStop = false;

                    // Draws username
                    TextBox temp = new System.Windows.Forms.TextBox();
                    this.Controls.Add(temp);
                    temp.Parent.Controls.SetChildIndex(temp, 2);
                    temp.Size = new System.Drawing.Size(70, 10);

                    if (i == 0)
                    {
                        temp.Location = new Point(500, 55);
                        cb.Location = new Point(575, 57);
                    }
                    else
                    {
                        temp.Location = new Point(500, 55 + i * 25);
                        cb.Location = new Point(575, 57 + i * 25);
                    }

                    temp.BackColor = Color.DarkGray;
                    temp.BorderStyle = BorderStyle.None;
                    temp.Text = " " + users[i].ToString();

                    // Draws green(online) or gray circular button  
                    for (int j = 0; j < onlineUsers.Count; j++)
                    {
                        if (users[i].Equals(onlineUsers[j].ToString()))
                        {
                            cb.BackColor = Color.Green;
                            break;
                        }
                        else
                        {
                            cb.BackColor = Color.Gray;
                        }
                    }




                }

            }
        }
        
        private void OrderOnlineFirst(ArrayList toOrder, ArrayList online)
        {
            ArrayList indexes = new ArrayList();
            for(int i = 0; i < toOrder.Count; i++)
            {
                for(int j = 0; j < online.Count; j++)
                {
                    if(toOrder[i].ToString().Equals(online[j].ToString()))
                    {
                        indexes.Add(i);
                    }
                }
            }



            for(int i = 0; i < indexes.Count; i++)
            {
                object temp = toOrder[i];
                int index = Int32.Parse(indexes[i].ToString());
                toOrder[i] = toOrder[index];
                toOrder[index] = temp;
            }
        }

        private void Icon_Click(object sender, EventArgs e)
        {
            // Each icon will be a group chat
        }

        //----------Delegates----------
        //public void OnOnlineUsersChange(object source, EventArgs e)
        //{
        //    Console.WriteLine("Online users changed");
        //}

        // Handler
        public void IndexHandler(object o, Common.OnlineUsersEventArgs e)
        {
            DrawUsers();
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
