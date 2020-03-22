using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

namespace ChatApp
{
    public partial class Client : Form
    {
        string IP;
        string Port;


        TcpClient socket = new TcpClient();
        NetworkStream server = default(NetworkStream);
        string message = null;


        public Client()
        {
            InitializeComponent();
        }

        private void Client_Load(object sender, EventArgs e)
        {

        }

        // Connect button
        private void button1_Click(object sender, EventArgs e)
        {
            IP = textBox1.Text;
            Port = textBox2.Text;
        }

        

        // Send button
        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
