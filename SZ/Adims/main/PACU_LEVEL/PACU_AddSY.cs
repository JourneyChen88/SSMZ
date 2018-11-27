using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace main.PACU_LEVEL
{
    public partial class PACU_AddSY : Form
    {

        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        List<adims_MODEL.shuye> SYlist;
        int mzjldid;
        DateTime ksTime;
        public PACU_AddSY(List<adims_MODEL.shuye> list, DateTime dt, int id)
        {
            SYlist = list;
            ksTime = dt;
            mzjldid = id;
            InitializeComponent();
        }
        adims_DAL.PACU_DAL dal = new adims_DAL.PACU_DAL();
        private void BindYaoPin()
        {
            DataTable dt = dal.GetAdims_YaoPinByType("输液");
            this.cmbName.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbName.Items.Add(dt.Rows[i]["ypname"]);
            }
        }
        private void BindShuyeList()
        {
            DataTable dtSX = bll.selectSY_pacu(mzjldid);
            dataGridView1.DataSource = dtSX.DefaultView;
        }
        private void PACU_AddSY_Load(object sender, EventArgs e)
        {
            BindYaoPin();
            BindShuyeList();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.UserFunction.Text_Value_Limit(sender,e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                adims_MODEL.shuye jtytsx = new adims_MODEL.shuye();
                jtytsx.Bz = 1;
                jtytsx.Name = cmbName.Text;
                jtytsx.Jl = Convert.ToInt32(textBox1.Text);
                jtytsx.Dw = cmbDW.Text;
                jtytsx.Zrfs = cmbZRFS.Text;
                jtytsx.Kssj = ksTime;
                int q = bll.addshuyePACU(mzjldid, jtytsx);
                if (q > 0)
                    BindShuyeList();
                else
                    MessageBox.Show("添加失败，请重试。");
            }
            else
            {
                MessageBox.Show("剂量不能为空！");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.SelectedRows.Count > 0)
            //{
            //    foreach (adims_MODEL.shuye szyy in SYlist)
            //    {
            //        if (szyy.Bz == 1 && szyy.Name == dataGridView1.SelectedRows[0].Cells[0].Value.ToString())
            //        {
            //            szyy.Jssj = DateTime.Now;
            //            szyy.Bz = 2;
            //            int q = bll.endshuyePACU(mzjldid, szyy);
            //            if (q != 1)
            //            { MessageBox.Show("错误" + q.ToString()); }
            //        }
            //    }
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int q = bll.delshuyePACU(mzjldid, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                MessageBox.Show("删除成功！");
                BindShuyeList();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
