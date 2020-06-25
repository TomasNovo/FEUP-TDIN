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
    public partial class CustomOkMessageBox : Form
    {
        public CustomOkMessageBox(string m)
        {
            InitializeComponent();

            this.label1.Text = m;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
