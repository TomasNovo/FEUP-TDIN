using Common;
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
    public partial class CustomYesNoMessageBox : LockedForm
    {
        Client client;
        int roomId;
        string message;

        public CustomYesNoMessageBox(Client c, int id, string m)
        {
            InitializeComponent();
            this.client = c;
            this.roomId = id;
            this.message = m;

            this.BYes.TabStop = false;
            this.BNo.TabStop = false;
            this.label1.Text = this.message;

            this.Text = "New Chat Proposal !!";
        }

        private void BYes_Click(object sender, EventArgs e)
        {
            client.AcceptChatRequest(roomId);
            this.Close();
        }

        private void BNo_Click(object sender, EventArgs e)
        {
            client.RejectChatRequest(roomId);
            this.Close();
        }
    }
}
