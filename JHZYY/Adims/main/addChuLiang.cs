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
    public partial class addChuLiang : Form
    {
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();

        List<adims_MODEL.clcxqt> clcxqt;
        DateTime sj;
        int lx;
        int mzjldid;
        public addChuLiang(List<adims_MODEL.clcxqt> c, DateTime d, int l, int m)
        {

            lx = l;
            clcxqt = c;
            sj = d;
            mzjldid = m;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox1.Text != "0")
            {
                adims_MODEL.clcxqt q = new adims_MODEL.clcxqt();
                q.V = Convert.ToInt32(textBox1.Text);
                q.D = sj;
                q.Lx = lx;

                int i = bll.addclcxqt(mzjldid, q);
                if (i != 1)
                {
                    MessageBox.Show("添加失败，请重试");
                }
                q.Id = bll.clid(mzjldid, q);
                clcxqt.Add(q);

                this.Close();
            }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }

        private void addcl_Load(object sender, EventArgs e)
        {
            if (lx == 3)
            {
                label1.Text = "其  它:";
            }
            else if (lx == 2)
            {
                label1.Text = "出血量:";
            }
            else
            {
                label1.Text = "尿  量:";
            }
        }

       
    }
}
