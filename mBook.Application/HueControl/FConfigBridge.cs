using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MBook
{
    public partial class FConfigBridge : Form
    {
        private string m_sIP = "";

        public string IP
        {
            get { return m_sIP;}
        }

        public FConfigBridge()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
                m_sIP = textBox1.Text;
            else
                MessageBox.Show("IP não informado");

            this.Close();
        }
    }
}
