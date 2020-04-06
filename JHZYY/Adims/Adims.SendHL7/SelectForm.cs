using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Adims.SendHL7
{
    public partial class SelectForm : Form
    {
        public SelectForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendPaiBan f = new SendPaiBan();
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendOperConfig f = new SendOperConfig();
            f.ShowDialog();
        }
    }
}
