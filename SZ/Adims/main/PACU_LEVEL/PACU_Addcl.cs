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
    public partial class PACU_Addcl : Form
    {
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();

        List<adims_MODEL.clcxqt> clcxqt;
        DateTime sj;
        int lx;
        int mzjldid;
        public PACU_Addcl(List<adims_MODEL.clcxqt> c, DateTime d, int l, int m)
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
                int i = bll.addclcxqt_PACU(mzjldid, q);
                if (i != 1)
                {
                    MessageBox.Show("错误" + i.ToString());
                }
                q.Id = bll.clid_PACU(mzjldid, q);
                clcxqt.Add(q);

                this.Close();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.UserFunction.Text_Value_Limit(sender, e);
        }
    }
}
