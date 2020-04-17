using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoDB.Bson;
using Common;

namespace ChatApp
{
    public partial class Register : LockedForm
    {
        public String Username;
        public String Password;
        public String RealName;
        
        public Register()
        {
            InitializeComponent();
        }

        //Real Name Placeholder
        private void TBRealname_Enter(object sender, EventArgs e)
        {
            if(TBRealname.Text == "Enter Real Name")
            {
                TBRealname.Text = "";
                TBRealname.ForeColor = Color.White;
            }
        }

        private void TBRealname_Leave(object sender, EventArgs e)
        {
            if (TBRealname.Text == "")
            {
                TBRealname.Text = "Enter Real Name";
                TBRealname.ForeColor = Color.Silver;
            }
        }

        //User Name Placeholder
        private void TBUsername_Enter(object sender, EventArgs e)
        {
            if (TBUsername.Text == "Enter Username")
            {
                TBUsername.Text = "";
                TBUsername.ForeColor = Color.White;
            }
        }

        private void TBUsername_Leave(object sender, EventArgs e)
        {
            if (TBUsername.Text == "")
            {
                TBUsername.Text = "Enter Username";
                TBUsername.ForeColor = Color.Silver;
            }
        }

        //Password Placeholder
        private void TBPass_Enter(object sender, EventArgs e)
        {
            if (TBPass.Text == "Enter Password")
            {
                TBPass.Text = "";
                TBPass.ForeColor = Color.White;
                TBPass.PasswordChar = '*';
            }
        }

        private void TBPass_Leave(object sender, EventArgs e)
        {
            if (TBPass.Text == "")
            {
                TBPass.Text = "Enter Password";
                TBPass.ForeColor = Color.Silver;
                TBPass.PasswordChar = '\0';
            }
        }

        //Confirm Password Placeholder
        private void TBPassConfirm_Enter(object sender, EventArgs e)
        {
            if (TBPassConfirm.Text == "Confirm Password")
            {
                TBPassConfirm.Text = "";
                TBPassConfirm.ForeColor = Color.White;
                TBPassConfirm.PasswordChar = '*';
            }
        }

        private void TBPassConfirm_Leave(object sender, EventArgs e)
        {
            if (TBPassConfirm.Text == "")
            {
                TBPassConfirm.Text = "Confirm Password";
                TBPassConfirm.ForeColor = Color.Silver;
                TBPassConfirm.PasswordChar = '\0';
            }
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void Register_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.Instance.Close();
        }
        
        private void BLogin_Click(object sender, EventArgs e)
        {
            MainForm.Instance.SwitchToLogin();
        }

        private void BRegister_Click(object sender, EventArgs e)
        {
            RealName = TBRealname.Text;
            Username = TBUsername.Text;
            Password = TBPass.Text;
            String confirm = TBPassConfirm.Text;

            if(Password != confirm)
            {
                MessageBox.Show("Passwords must be equal !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string registerMessage = MainForm.Instance.client.Register(Username, Password, RealName);

            if (registerMessage != "")
            {
                MessageBox.Show(registerMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Registered with success !");

            MainForm.Instance.SwitchToLogin();
        }
    }
}