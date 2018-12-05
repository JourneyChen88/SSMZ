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
        adims_DAL.PacuDal pdal = new adims_DAL.PacuDal();
        adims_DAL.YaopinDal _YaopinDal = new adims_DAL.YaopinDal();
        List<adims_MODEL.shuxue> SXlist;
        int  mzjldid;
        DateTime ksTime;
       

        #endregion

        #region <<Constructors>>

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="jys"></param>
        /// <param name="t"></param>
        /// <param name="m"></param>
        public PACU_Add_SX(DateTime dt, int m)
        {
            //otime = dt;
            mzjldid = m;
            ksTime = dt;
            InitializeComponent();
        }

        #endregion
         private void BindYaoPin()
        {
            DataTable dt = _YaopinDal.GetYaoPinByType("输血", cmbName.Text.Trim());
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
         /// <summary>
         /// 单位
         /// </summary>
         private void BindDW()
         {
             DataTable dtdw = dal.GetAllDW();
             cmbDW.Items.Clear();
             for (int i = 0; i < dtdw.Rows.Count; i++)
             {
                 this.cmbDW.Items.Add(dtdw.Rows[i][0]);
             }
         }
         /// <summary>
         /// 注入方式
         /// </summary>
         private void BindZRFS()
         {
             DataTable dtdw = dal.GetAllYD_ZRFS();
             cmbZRFS.Items.Clear();
             for (int i = 0; i < dtdw.Rows.Count; i++)
             {
                 this.cmbZRFS.Items.Add(dtdw.Rows[i][0]);
             }
         }
        private void PACU_Add_SX_Load(object sender, EventArgs e)
        {
            BindYaoPin();
            BindShuxueList();
            BindDW();
            BindZRFS();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_Utility.TextValueLimit.Text_Value_Limit(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || cmbName.Text.Trim() != "")
            {
                adims_MODEL.shuxue jtytsx = new adims_MODEL.shuxue();
                jtytsx.Bz = 1;
                jtytsx.Name = cmbName.Text;
                jtytsx.Jl = Convert.ToInt32(textBox1.Text);
                jtytsx.Dw = cmbDW.Text;
                jtytsx.Zrfs = cmbZRFS.Text;
                jtytsx.Kssj = ksTime;
                jtytsx.Cxyy = cbCXYY.Checked;
                 int m = 0;
                    if (cbCXYY.Checked)
                    {
                        jtytsx.Bz = 1;
                        m = bll.addshuxuePACU(mzjldid, jtytsx);
                    }
                    else
                    {
                        jtytsx.Bz = 2; jtytsx.Jssj = jtytsx.Kssj;
                        m = bll.addshuxuePACU2(mzjldid, jtytsx);
                    }
                    if (m > 0)
                        BindShuxueList();
                    else MessageBox.Show(jtytsx.Name + "—添加失败请重试！");
               // int q = bll.addshuxuePACU(mzjldid, jtytsx);
               // if (q > 0)
               //     BindShuxueList();
               // else
               //     MessageBox.Show("添加失败，请重试。");
               }
            else MessageBox.Show("剂量、输血名不能为空");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (dataGridView1.SelectedRows[0].Cells["flags"].Value.ToString() == "1")
                {
                    int m = bll.endshuxuePACUs(mzjldid, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                    BindShuxueList();
                }
                else
                    MessageBox.Show("输血已经结束！");
            }         
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
