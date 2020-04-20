using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections;
using Common;
using System.Runtime.Remoting.Channels;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting;

namespace ChatApp
{
    public partial class Index : LockedForm
    {
        private Dictionary<int, ChatRoom> _chatRooms;
        private bool selectingGroupChat = false;
        private List<string> groupChatUserListBuffer;

        private const string LWelcomeText = "Double click on a user to chat or click the button to start a group chat";
        private const string BGroupChatText = "Start group chat";

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

            LUser.Text += ", " + MainForm.Instance.client.UserName + " !";

            MainForm.Instance.client.OnlineUsersChanged += IndexOnlineUsersChangeHandler;
            MainForm.Instance.client.ChatAsked += IndexAskForChatHandler;
            MainForm.Instance.client.ChatFinalized += IndexChatFinalizedHandler;
        }


        private void DrawUserPanel()
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)delegate { DrawUserPanel(); });
            else
            {
                List<string> users;
                List<string> onlineUsers;

                PUsers.Controls.Clear();

                Label label1 = new Label();
                label1.Font = new Font("Microsoft Sans Serif", 12);
                label1.Text = "Users";
                label1.Size = new Size(51, 20);
                label1.Location = new Point(22, 17);
                PUsers.Controls.Add(label1);

                LWelcome.Text = LWelcomeText;

                users = MainForm.Instance.client.GetUsers();
                onlineUsers = MainForm.Instance.client.server.GetOnlineUsers();
                users.Remove(MainForm.Instance.client.UserName);
                onlineUsers.Remove(MainForm.Instance.client.UserName);

                List<Tuple<string, bool>> mergedUsers = users.Select(e => new Tuple<string, bool>(e, onlineUsers.IndexOf(e) != -1)).ToList();

                mergedUsers.Sort((Tuple<string, bool> a, Tuple<string, bool> b) => 
                { 
                    if (a.Item2 == b.Item2)
                    {
                        return a.Item1.CompareTo(b.Item1);
                    }
                    else
                    {
                        if (a.Item2)
                            return -1;
                        else
                            return 1;
                    }
                });

                for (int i = 0; i < mergedUsers.Count; i++)
                {
                    DrawUser(i, mergedUsers[i].Item1, mergedUsers[i].Item2);
                }
            }
        }

        private void DrawUser(int i, string username, bool online)
        {
            CircularButton cb = new CircularButton();
            cb.FlatStyle = FlatStyle.Flat;
            cb.FlatAppearance.BorderSize = 0;
            cb.Size = new Size(10, 10);
            cb.TabStop = false;
            cb.Location = new Point(15, 57 + i * 25);

            // Draws username
            Label temp = new Label();
            temp.Size = new Size(70, 15);
            temp.Location = new Point(28, 55 + i * 25);
            temp.BackColor = Color.DarkGray;
            temp.BorderStyle = BorderStyle.None;
            temp.Text = username;

            if (online)
            {
                if (selectingGroupChat)
                    temp.Click += UserLabelGroupChatClick;
                else
                    temp.DoubleClick += UserDoubleClick;

                cb.BackColor = Color.Green;
            }
            else
                cb.BackColor = Color.Gray;

            this.PUsers.Controls.Add(cb);
            this.PUsers.Controls.Add(temp);
        }

        private void UserDoubleClick(object sender, EventArgs e)
        {
            string username = ((Label)sender).Text;

            if (!MainForm.Instance.client.server.GetOnlineUsers().Contains(username))
                return;

            int roomId = MainForm.Instance.client.StartChat(username);

            if (roomId == -1) // Server error
                MessageBox.Show("Failed to start chat");
            else if (roomId == -2) // chat room already exists
                MessageBox.Show("Chat room already exists!");
        }

        private void UserLabelGroupChatClick(object sender, EventArgs e)
        {
            Label label = (Label)sender;

            if (groupChatUserListBuffer.IndexOf(label.Text) != -1)
            {
                groupChatUserListBuffer.Remove(label.Text);
                label.ForeColor = Color.White;
            }
            else
            {
                groupChatUserListBuffer.Add(label.Text);
                label.ForeColor = Color.RoyalBlue;
            }
        }

        private void DrawChatRoomPanel()
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)delegate { DrawChatRoomPanel(); });
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
                    cb.ForeColor = Color.White;
                    cb.BackColor = Color.RoyalBlue;
                    cb.Text = $"Chat{Environment.NewLine}{i + 1}";
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

                CustomYesNoMessageBox yesno = new CustomYesNoMessageBox(MainForm.Instance.client ,e.roomId, message);
                yesno.Show();
            }
        }

        public void IndexChatFinalizedHandler(object o, ChatFinalizedEventArgs e)
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)delegate { IndexChatFinalizedHandler(o, e); });
            else
            {
                // If username is not contained in the userList, the chat request does not concern this user
                if (!e.userList.Contains(MainForm.Instance.client.UserName))
                    return;

                if (!e.result)
                {
                    CustomOkMessageBox co = new CustomOkMessageBox("errorChatProposal", "Someone rejected the group chat!");
                    co.Show();                    
                }
                else
                {

	                CustomOkMessageBox cok = new CustomOkMessageBox("acceptChatProposal", "Everyone accepted the chat!");
	                cok.Show();

	                ChatRoom chatRoom = new ChatRoom(e.roomId, e.userList);
	                _chatRooms.Add(e.roomId, chatRoom);

                    if (selectingGroupChat)
                        BDiscard_Click(null, null);
                    else
                        DrawChatRoomPanel();
                }
            }
        }


        private void BGroupChat_Click(object sender, EventArgs e)
        {
            if (selectingGroupChat)
            {
                if (groupChatUserListBuffer.Count == 0)
                    BDiscard_Click(null, null);
                else
                {
                    int roomId = MainForm.Instance.client.StartChat(groupChatUserListBuffer);
                    
                    if (roomId == -1) // Server error
                    {
                        BDiscard_Click(null, null);
                        MessageBox.Show("Failed to start chat");
                    }
                    else if (roomId == -2) // chat room already exists
                    {
                        BDiscard_Click(null, null);
                        MessageBox.Show("Chat room already exists!");
                    }
                }
            }
            else
            {
                LWelcome.Text = "Click on a user to add him to a group chat";
                BGroupChat.Text = "Accept";
                BDiscard.Show();

                groupChatUserListBuffer = new List<string>();

                selectingGroupChat = true;

                DrawUserPanel();
            }

        }

        private void BDiscard_Click(object sender, EventArgs e)
        {
            selectingGroupChat = false;

            LWelcome.Text = LWelcomeText;
            BGroupChat.Text = BGroupChatText;
            BDiscard.Hide();

            DrawChatRoomPanel();
            DrawUserPanel();
        }

        private void Index_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.Instance.Close();
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
}
