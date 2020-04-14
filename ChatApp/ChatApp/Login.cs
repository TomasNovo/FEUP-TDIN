﻿using System;
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
    public partial class Login : Form
    {
        public String Username;
        public String Password;
        public String RealName;

        public Login()
        {
            InitializeComponent();

            // Fixed size
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        
        
        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.Instance.Close();
        }

        //User Name Placeholder
        private void TBUsername_Enter(object sender, EventArgs e)
        {
            if (TBUsername.Text == "Username")
            {
                TBUsername.Text = "";
                TBUsername.ForeColor = Color.White;
            }
        }

        private void TBUsername_Leave(object sender, EventArgs e)
        {
            if (TBUsername.Text == "")
            {
                TBUsername.Text = "Username";
                TBUsername.ForeColor = Color.Silver;
            }
        }

        //Password Placeholder
        private void TBPass_Enter(object sender, EventArgs e)
        {
            if (TBPass.Text == "Password")
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
                TBPass.Text = "Password";
                TBPass.ForeColor = Color.Silver;
                TBPass.PasswordChar = '\0';
            }
        }

        private void BLogin_Click(object sender, EventArgs e)
        {
            LoginUser();
        }

        private void LoginUser()
        {
            string loginMessage = MainForm.Instance.client.Login(TBUsername.Text, TBPass.Text);
            if (loginMessage != "")
            {
                MessageBox.Show(loginMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MainForm.Instance.Login();
        }

        private void BRegister_Click(object sender, EventArgs e)
        {
            MainForm.Instance.SwitchToRegister();
        }

        private void TBUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                LoginUser();
            }
        }

        private void TBPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                LoginUser();
            }
        }
    }
}