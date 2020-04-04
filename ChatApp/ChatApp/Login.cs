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
    public partial class Login : Form
    {
        public String Username;
        public String Password;
        public String RealName;

        public Login()
        {
            InitializeComponent();
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

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    MainForm.Instance.Login();
        //}

        private void BLogin_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Wrong Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            MainForm.Instance.Login();
        }
    }
}