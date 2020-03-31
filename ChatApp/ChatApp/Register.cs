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

namespace ChatApp
{
    public partial class Register : Form
    {
        public String Username;
        public String Password;
        public String RealName;

        public Register()
        {
            InitializeComponent();

            //var client = new MongoClient();
            //var db = client.GetDatabase("chatime");
            //var coll = db.GetCollection<Users>("Users");

            //var users = coll.Find(x => x.name == "Zé");

            //Console.WriteLine("Users" + users);
        }



        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.Instance.Close();
        }

           //Register
        private void button2_Click(object sender, EventArgs e)
        {
            //MainForm.Instance.Login();
        }
    }
}