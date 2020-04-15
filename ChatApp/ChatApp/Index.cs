using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections;
using Common;

namespace ChatApp
{
    public partial class Index : LockedForm
    {
        private Dictionary<int, ChatRoom> _chatRooms;


        public Index()
        {
            InitializeComponent();

            _chatRooms = new Dictionary<int, ChatRoom>();
        }

        private void Index_Load(object sender, EventArgs e)
        {
            //MainForm.Instance.client.server.OnlineUsersChanged += IndexHandler;
            PUsers.AutoScroll = false;
            PUsers.HorizontalScroll.Enabled = false;
            PUsers.HorizontalScroll.Visible = false;
            PUsers.HorizontalScroll.Maximum = 0;
            PUsers.AutoScroll = true;


            DrawUserPanel();
            MainForm.Instance.client.OnlineUsersChanged += IndexOnlineUsersChangeHandler;

            MainForm.Instance.client.ChatAsked += IndexAskForChatHandler;

            MainForm.Instance.client.ChatAccepted += IndexChatAcceptedHandler;

        }


        private void DrawUserPanel()
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)delegate { DrawUserPanel(); });
            else
            {
                ArrayList users;
                ArrayList onlineUsers;

                PUsers.Controls.Clear();

                Label label1 = new Label();
                label1.Font = new Font("Microsoft Sans Serif", 12);
                label1.Text = "Users";
                label1.Size = new Size(51, 20);
                label1.Location = new Point(22, 17);
                PUsers.Controls.Add(label1);

                label2.Text = "Do you wanna chat, " + MainForm.Instance.client.UserName + " ?";

                users = MainForm.Instance.client.GetUsers();
                onlineUsers = MainForm.Instance.client.server.GetOnlineUsers();
                users.Remove(MainForm.Instance.client.UserName);
                onlineUsers.Remove(MainForm.Instance.client.UserName);

                OrderOnlineFirst(users, onlineUsers);
                

                for (int i = 0; i < users.Count; i++)
                {
                    CircularButton cb = new CircularButton();
                    cb.FlatStyle = FlatStyle.Flat;
                    cb.FlatAppearance.BorderSize = 0;
                    cb.Size = new System.Drawing.Size(10, 10);
                    cb.TabStop = false;
                    cb.Location = new Point(15, 57 + i * 25);

                    // Draws username
                    Label temp = new System.Windows.Forms.Label();
                    temp.DoubleClick += Temp_DoubleClick;
                    temp.Size = new System.Drawing.Size(70, 15);
                    temp.Location = new Point(28, 55 + i * 25);
                    temp.BackColor = Color.DarkGray;
                    temp.BorderStyle = BorderStyle.None;
                    temp.Text = users[i].ToString();

                    cb.BackColor = Color.Gray;

                    // Draws green(online) or gray circular button  
                    for (int j = 0; j < onlineUsers.Count; j++)
                    {
                        if (users[i].Equals(onlineUsers[j].ToString()))
                        {
                            cb.BackColor = Color.Green;
                            break;
                        }
                    }

                    this.PUsers.Controls.Add(cb);
                    this.PUsers.Controls.Add(temp);

                }
            }
        }

        private void DrawChatRooms()
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)delegate { DrawChatRooms(); });
            else
            {

                PChatRooms.Controls.Clear();

                const int heightSpacing = 45;
                const int widthSpacing = 45;

                int i = 0;
                foreach (KeyValuePair<int, ChatRoom> entry in _chatRooms)
                {
                    // do something with entry.Value or entry.Key

                    CircularButton cb = new CircularButton();
                    cb.FlatStyle = FlatStyle.Flat;
                    cb.FlatAppearance.BorderSize = 0;
                    cb.TabStop = false;
                    cb.ForeColor = Color.SlateBlue;
                    cb.BackColor = Color.Black;
                    cb.Text = $"Chat{Environment.NewLine}{i+1}";
                    cb.Size = new Size(40, 40);
                    cb.Location = new Point(25 + widthSpacing * (i / 3), 25 + heightSpacing * (i % 3));
                    cb.Name = $"{entry.Key}";
                    cb.Click += Cb_Click;

                    PChatRooms.Controls.Add(cb);
                    i++;
                }
            }
        }

        private void Cb_Click(object sender, EventArgs e)
        {
            int roomId = Int32.Parse(((CircularButton)sender).Name);

            _chatRooms[roomId].ToggleVisibility();
        }

        private void Temp_DoubleClick(object sender, EventArgs e)
        {
            string username = ((Label)sender).Text;

            if (!MainForm.Instance.client.server.GetOnlineUsers().Contains(username))
                return;

            int id = MainForm.Instance.client.StartChatWithUser(username);
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
        public void IndexOnlineUsersChangeHandler(object o, OnlineUsersEventArgs e)
        {
            DrawUserPanel();
        }

        public void IndexAskForChatHandler(object o, AskForChatEventArgs e)
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)delegate { IndexAskForChatHandler(o, e); });
            else
            {
                // If user is the one who created the chat do nothing
                if (e.creator == MainForm.Instance.client.UserName)
                    return;

                // If username is not contained in the userList, the chat request does not concern this user
                if (!e.userList.Contains(MainForm.Instance.client.UserName))
                    return;

                e.userList.Remove(MainForm.Instance.client.UserName);

                string message = "Do you want to chat with ";

                for (int i = 0; i < e.userList.Count; i++)
                {
                    message += e.userList[i];

                    if (i < e.userList.Count - 1)
                        message += ", ";
                }

                message += "?";

                DialogResult response = MessageBox.Show(message, "", MessageBoxButtons.YesNo);
                bool responseBool = response.HasFlag(DialogResult.Yes);

                if (responseBool)
                    MainForm.Instance.client.AcceptChatRequest(e.roomId);
            }
        }

        public void IndexChatAcceptedHandler(object o, ChatAcceptedEventArgs e)
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)delegate { IndexChatAcceptedHandler(o, e); });
            else
            {
                // If username is not contained in the userList, the chat request does not concern this user
                if (!e.userList.Contains(MainForm.Instance.client.UserName))
                    return;

                MessageBox.Show("Everyone accepted the chat");

                // TODO: Create chat window

                ChatRoom chatRoom = new ChatRoom(e.roomId, e.userList);
                _chatRooms.Add(e.roomId, chatRoom);

                DrawChatRooms();
            }
        }


        private void Index_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.Instance.client.Logout(MainForm.Instance.client.UserName);
            MainForm.Instance.Close();
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
