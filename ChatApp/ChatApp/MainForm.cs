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
    public sealed partial class MainForm : Form
    {
        private static MainForm instance = null;

        private Login _loginForm;
        private Client _clientForm;
        private Register _registerForm;

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
            _clientForm = new Client();
            _registerForm = new Register();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Size = new Size(0, 0);

            _loginForm.Show();

            //_registerForm.Show();

            // _loginForm.Close();
        }

        // Changes forms
        public void Login()
        {
            var Username = _loginForm.Username;
            var Password = _loginForm.Password;
            var RealName = _loginForm.RealName;

            _loginForm.Hide();

            _clientForm.Show();
        }

        public void Register()
        {
            var Username = _registerForm.Username;
            var Password = _registerForm.Password;

            //_loginForm.Hide();

            //_registerForm.Show();
        }

    }
}