using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp
{
    public sealed partial class MainForm : LockedForm
    {
        private static MainForm instance = null;

        private Login _loginForm;
        private Index _indexForm;
        private Register _registerForm;


        public Client client;

        public static MainForm Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainForm();
                }

                return instance;
            }
        }

        private MainForm()
        {
            InitializeComponent();

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ShowInTaskbar = false;

            _loginForm = new Login();
            _indexForm = new Index();
            _registerForm = new Register();
            //_chatRoom = new ChatRoom();
    }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Size = new Size(0, 0);

            _loginForm.Show();

            client = new Client();

            //// Ping
            //Thread thread1 = new Thread(client.Ping);
            //thread1.Start();

        }

        // Changes forms
        public void Login()
        {
            var Username = _loginForm.Username;
            var Password = _loginForm.Password;

            _loginForm.Hide();

            _indexForm.Show();
        }

        public void SwitchToLogin()
        {
            _registerForm.Hide();
            _loginForm.Show();
        }

        public void SwitchToRegister()
        {
            _loginForm.Hide();
            _registerForm.Show();
        }

        public void Connect()
        {
            if (!client.connected)
            {
                string serverIP = "localhost";
                if (GetServerIP() != "")
                    serverIP = GetServerIP();

                MainForm.Instance.client.Connect(serverIP, 8080, "server");
            }
        }

        public string GetServerIP()
        {
            return _loginForm.GetServerIP();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.Logout();
        }
    }
}