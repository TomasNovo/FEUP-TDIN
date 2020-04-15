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
    public sealed partial class MainForm : LockedForm
    {
        private static MainForm instance = null;

        private Login _loginForm;
        private Index _indexForm;
        private Register _registerForm;

        //Testing
        private ChatRoom _chatRoom;

        public Common.Client client;

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

            client = new Common.Client();
            client.Connect("localhost", 8080, "server");

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ShowInTaskbar = false;

            _loginForm = new Login();
            _indexForm = new Index();
            _registerForm = new Register();
            _chatRoom = new ChatRoom();
    }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Size = new Size(0, 0);

            //_chatRoom.Show();
            _loginForm.Show();

            //client.server.OnlineUsersChanged += _indexForm.IndexHandler;
            //_indexForm.Show();
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

    }
}