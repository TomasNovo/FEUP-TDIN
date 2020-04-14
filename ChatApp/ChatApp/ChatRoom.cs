using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp
{
    public partial class ChatRoom : Form
    {
        int message_number = 0;

        public ChatRoom()
        {
            InitializeComponent();
        }

        private void ChatRoom_Load(object sender, EventArgs e)
        {

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
            
            Label temp = new System.Windows.Forms.Label();
            this.PMessages.Controls.Add(temp);
            temp.Parent.Controls.SetChildIndex(temp, 2);
            temp.Size = new System.Drawing.Size(70, 15);
            temp.Location = new Point(210, message_number * 25);
            message_number++;
            temp.Text = TBSend.Text;
            TBSend.Text = "Write here your message..";
            //TBSend.ForeColor = Color.Silver;

        }
    }
}
