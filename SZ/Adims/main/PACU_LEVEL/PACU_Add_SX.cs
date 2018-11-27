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
    public partial class PACU_Add_SX : Form
    {
          #region <<Members>>

        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        adims_DAL.PACU_DAL pdal = new adims_DAL.PACU_DAL();
        List<adims_MODEL.shuxue> SXlist;
        int type, mzjldid;
        DateTime ksTime;
       

        #endregion

        #region <<Constructors>>

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="jys"></param>
        /// <param name="t"></param>
        /// <param name="m"></param>
        public PACU_Add_SX(List<adims_MODEL.shuxue> jys,DateTime dt, int m)
        {
            //otime = dt;
            mzjldid = m;
            SXlist = jys;
            ksTime = dt;
            InitializeComponent();
        }

        #endregion
         private void BindYaoPin()
        {
            DataTable dt = pdal.GetAdims_YaoPinByType("输血");
            this.cmbName.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbName.Items.Add(dt.Rows[i]["ypname"]);
            }
        }
         private void BindShuxueList()
         {
             DataTable dtSX = bll.selectSX_pacu(mzjldid);
             dataGridView1.DataSource = dtSX.DefaultView;
         }
        private void PACU_Add_SX_Load(object sender, EventArgs e)
        {
            BindYaoPin();
            BindShuxueList();        
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.UserFunction.Text_Value_Limit(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                adims_MODEL.shuxue jtytsx = new adims_MODEL.shuxue();
                jtytsx.Bz = 1;
                jtytsx.Name = cmbName.Text;
                jtytsx.Jl = Convert.ToInt32(textBox1.Text);
                jtytsx.Dw = cmbDW.Text;
                jtytsx.Zrfs = cmbZRFS.Text;
                jtytsx.Kssj = ksTime;
                int q = bll.addshuxuePACU(mzjldid, jtytsx);
                if (q > 0)
                    BindShuxueList();
                else
                    MessageBox.Show("添加失败，请重试。");
               }
            else MessageBox.Show("剂量不能为空");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.SelectedRows.Count > 0)
            //{
            //    foreach (adims_MODEL.shuxue szyy in SXlist)
            //    {
            //        if (szyy.Bz == 1 && szyy.Name == dataGridView1.SelectedRows[0].Cells[0].Value.ToString())
            //        {
            //            szyy.Jssj = DateTime.Now;
            //            szyy.Bz = 2;
            //            int q = bll.endshuxuePACU(mzjldid, szyy);
            //            if (q != 1)
            //            { MessageBox.Show("错误" + q.ToString()); }
            //        }
            //    }
            //    dataGridView1.SelectedRows[0].Cells[5].Value = DateTime.Now;
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int q = bll.delshuxuePACU(mzjldid, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                if (q >0)
                    BindShuxueList();   
                else
                    MessageBox.Show("删除失败！");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }


      
       

        
    }
}
