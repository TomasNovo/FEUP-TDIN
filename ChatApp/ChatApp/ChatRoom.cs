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
        private Client client;

        public ChatRoom(int RoomId)
        {
            InitializeComponent();

            roomId = RoomId;

            client = MainForm.Instance.client;
            client.MessageReceived += Client_MessageReceived;
        }

        private void Client_MessageReceived(object source, MessageReceivedEventArgs e)
        {
            // Ignore event when message is sent by this client
            if (e.sender == client.UserName)
                return;

            // Ignore messages belongin to other rooms
            if (e.roomId == roomId)
                return;

            DrawMessages(e);
        }

        private void DrawMessages(MessageReceivedEventArgs e)
        {

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

            Label temp = new System.Windows.Forms.Label();
            this.PMessages.Controls.Add(temp);
            temp.Parent.Controls.SetChildIndex(temp, 2);
            temp.Size = new System.Drawing.Size(70, 15);

            if (message_number == 0)
            {
                temp.Location = new Point(210, 10);
            }
            else
            {
                temp.Location = new Point(210, 10 + message_number * 15);
            }
            message_number++;
            temp.Text = TBSend.Text;

            MainForm.Instance.client.SendMessage(roomId, temp.Text);

            TBSend.Text = "Write here your message..";
            //TBSend.ForeColor = Color.Silver;

        }
    }
}
