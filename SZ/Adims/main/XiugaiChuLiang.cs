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
    public partial class XiugaiChuLiang : Form
    {
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        int mzjldid;
        adims_MODEL.clcxqt clcxqt;
        int type;
        private List<adims_MODEL.clcxqt> clcxqtlist = new List<adims_MODEL.clcxqt>();//出尿出血其他出量

        public XiugaiChuLiang(int m, adims_MODEL.clcxqt c, int tp)
        {
            type = tp;
            mzjldid = m;
            clcxqt = c;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //int i = bll.DelteCl(mzjldid, clcxqt);
            //if (i != 1)
            //{
            //    MessageBox.Show("删除失败，请重试！");
            //}
            //else
            //{
            //    //clcxqtlist.Remove(clcxqt);
            //    this.Close();
            //}
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clcxqt.V = Convert.ToInt32(textBox1.Text);
            int i = bll.xgclValue(mzjldid, type, clcxqt);
            if (i != 1)
            {
                MessageBox.Show("修改失败，请重试！");
            }
            this.Close();
        }

        private void xgcl_Load(object sender, EventArgs e)
        {

        }
       
    }
}
