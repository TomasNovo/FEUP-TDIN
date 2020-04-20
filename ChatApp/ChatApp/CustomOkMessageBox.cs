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
    public partial class CustomOkMessageBox : LockedForm
    {
        string message;

        public CustomOkMessageBox(string type, string m)
        {
            InitializeComponent();
            this.message = m;
            
            this.label1.Text = this.message;

            switch(type)
            {
                case "errorRegister":
                    this.Text = "Error: Register";
                    this.label1.Location = new Point(80, 145);
                    this.pictureBox2.Hide();
                    this.pictureBox1.Show();
                    break;

                case "errorLogin":
                    this.Text = "Error: Login";
                    this.pictureBox2.Hide();
                    this.pictureBox1.Show();
                    break;


                case "errorChatProposal":
                    this.Text = "Error: Chat Proposal Failed";
                    this.label1.Location = new Point(60, 145);
                    this.pictureBox2.Hide();
                    this.pictureBox1.Show();
                    break;

                case "acceptChatProposal":
                    this.Text = "Chat Proposal Accepted !!";
                    this.pictureBox2.Location = new Point(30, 130);
                    this.label1.Location = new Point(70, 134);
                    this.pictureBox1.Hide();
                    this.pictureBox2.Show();
                    break;


                default: break;
            }
        }
        

        private void BOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
