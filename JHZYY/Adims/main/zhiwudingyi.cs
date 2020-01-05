using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace main
{
    public partial class zhiwudingyi : Form
    {
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        public zhiwudingyi()
        {
            InitializeComponent();
        }

        private void zhiwudingyi_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = bll.selzhiwu();
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
               int n= bll.delzhiwu(Convert.ToInt32(dataGridView1.SelectedCells[0].Value));
               if (n != 1)
                   MessageBox.Show("error" + n.ToString());
               else
                   dataGridView1.DataSource = bll.selzhiwu().Tables[0].DefaultView;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                int n = bll.addzhiwu(textBox1.Text);
                if (n != 1)
                    MessageBox.Show("error" + n.ToString());
                else
                {
                    textBox1.Text = "";
                    dataGridView1.DataSource = bll.selzhiwu().Tables[0].DefaultView;
                
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
