using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;

namespace main
{
    public partial class xgjhsj : Form
    {
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_MODEL.jhxm jhxm;
        int mzjldid;
        int type;
        public xgjhsj( int m,adims_MODEL.jhxm j,int lx1)
        {
            type = lx1;
            mzjldid = m;
            jhxm = j;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "")
            {
                jhxm.V = Convert.ToInt32(textBox1.Text.Trim());
                int q = bll.xgjhsj(mzjldid, jhxm,type);
                if (q > 0)
                {
                    this.Close();
                }               
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            UserFunction.Text_Value_Limit(sender, e); 
        }

    }
}
