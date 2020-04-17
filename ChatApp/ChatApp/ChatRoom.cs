using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace ChatApp
{
    public partial class ChatRoom : LockedForm
    {
        int message_number = 0;
        private int roomId;
        public List<string> userList;
        private Client client;
        private bool hidden = true;

        private string ChatName;

        public ChatRoom(int RoomId, List<string>  userList)
        {
            InitializeComponent();

            this.roomId = RoomId;
            this.ChatName = "ChatName" + this.roomId;

            client = MainForm.Instance.client;
            client.MessageReceived += Client_MessageReceived;

            // Subscribe all
            //foreach (KeyValuePair<int, List<Client>> entry in client.chatRooms)
            //{
            //    for (int i = 0; i < entry.Value.Count; i++)
            //    {
            //        client.MessageReceived += entry.Value[i].HandlerMessageReceived;
            //    }
            //}

            userList.Remove(client.UserName);
            for (int i = 0; i < userList.Count; i++)
            {
                TBUserList.Text += $"{userList[i]}{Environment.NewLine}";
            }

            this.userList = userList;

            this.ControlBox = false;
        }

        private void Client_MessageReceived(object source, MessageReceivedEventArgs e)
        {
            // Ignore event when message is sent by this client
            if (e.sender == client.UserName)
                return;

            // Ignore messages belongin to other rooms
            if (e.roomId != roomId)
                return;

            DrawMessageReceived(e);
        }

        private void DrawMessageReceived(MessageReceivedEventArgs e)
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)delegate { DrawMessageReceived(e); });
            else
            {
                if(e.message.Length >= 16 &&  e.message.Substring(0,16).Equals("UpdatedChatName:"))
                {
                    this.ChatName = e.message.Substring(16);
                    TChatName.Text = this.ChatName;
                    return;
                }
                
                DrawMessage(true, e.message, e.sender);
                
            }
        }

        private void ChatRoom_Load(object sender, EventArgs e)
        {
            PMessages.AutoScroll = false;
            PMessages.HorizontalScroll.Enabled = false;
            PMessages.HorizontalScroll.Visible = false;
            PMessages.HorizontalScroll.Maximum = 0;
            PMessages.AutoScroll = true;
        }

        //Send textBox Placeholder
        private void TBSend_Enter(object sender, EventArgs e)
        {
            if (TBSend.Text == "Write here your message..")
            {
                TBSend.Text = "";
                TBSend.ForeColor = Color.Black;
            }
        }

        private void TBSend_Leave(object sender, EventArgs e)
        {
            if (TBSend.Text == "")
            {
                TBSend.Text = "Write here your message..";
                TBSend.ForeColor = Color.Black;
            }
        }

        private void BSend_Click(object sender, EventArgs e)
        {
            if (TBSend.Text == "Write here your message..")
                return;

            DrawMessage(false, TBSend.Text);
         
            MainForm.Instance.client.SendMessage(roomId, TBSend.Text);

            TBSend.Text = "Write here your message..";
            //TBSend.ForeColor = Color.Silver;
        }

        private void DrawMessage(bool left, string message, string sender = "")
        {
            Label temp = new Label();
            this.PMessages.Controls.Add(temp);
            temp.Size = new Size(490, 15);
            temp.AutoSize = false;

            if (left)
            {
                temp.Location = new Point(3, 10 + message_number * 20);
                temp.Text = $"{sender}: {message}";
            }
            else
            {
                temp.TextAlign = ContentAlignment.MiddleRight;
                temp.Location = new Point(0, 10 + message_number * 20);
                temp.Text = message;
            }

            message_number++;
        }

        public void ToggleVisibility()
        {
            if (hidden)
                this.Show();
            else
                this.Hide();

            hidden = !hidden; // Swap hidden value
        }

        private void TChatName_TextChanged(object sender, EventArgs e)
        {
            MainForm.Instance.client.SendMessage(roomId, "UpdatedChatName:" + TChatName.Text);
        }
    }
}
