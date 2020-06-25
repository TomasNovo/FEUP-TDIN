using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTClient
{
    public partial class TicketInfo : Form
    {
        public TicketInfo(string id, string author, string date, string title, string description)
        {
            InitializeComponent();

            this.label1.Text += id;
            this.label2.Text += author;
            this.label3.Text += date;
            this.label5.Text += title;
            this.textBox3.ReadOnly = true;
            this.textBox3.Text = description;

        }

        // OK
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
