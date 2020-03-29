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
        int Port;
        TcpClient tcp;
        
        string message = "Hello I'm the client";


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
            Port = Int32.Parse(textBox2.Text);

            tcp = new TcpClient(IP, Port);
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(message);

            //---send the text---
            Console.WriteLine("Sending : " + message);
            NetworkStream nwStream = tcp.GetStream();
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

            //---read back the text---
            byte[] bytesToRead = new byte[tcp.ReceiveBufferSize];
            int bytesRead = nwStream.Read(bytesToRead, 0, tcp.ReceiveBufferSize);
            Console.WriteLine("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
            Console.ReadLine();
            tcp.Close();

        }

        

        // Send button
        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
