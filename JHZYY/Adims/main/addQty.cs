using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace main
{
    public partial class addQty : Form
    {
        int mzjldid;
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        //List<adims_MODEL.mzqt> mzqt;
        ArrayList sss = new ArrayList();
        public addQty( int id)
        {
            mzjldid = id;           
            InitializeComponent();
        }
        adims_DAL.PACU_DAL dal = new adims_DAL.PACU_DAL();
        private void BindYaoPin()
        {
            DataTable dt = dal.GetAdims_YaoPinByType("气体药", cmbName.Text.Trim());
            this.cmbName.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbName.Items.Add(dt.Rows[i]["ypname"]);
            }
        }
        private void BindQtList()
        {
            sss.Clear();
            DataTable dtYDY = bll.select_qt(mzjldid);
            dataGridView1.DataSource = dtYDY.DefaultView;
            for (int i = 0; i < dtYDY.Rows.Count; i++)
            {
                if (!sss.Contains(dtYDY.Rows[i]["qtname"].ToString()))
                {
                    sss.Add(dtYDY.Rows[i]["qtname"].ToString());
                }
            }
        }
        /// <summary>
        /// 绑定用量单位
        /// </summary>
        private void BindDW()
        {
            DataTable dt = dal.GetAdims_DW();
            comboBox2.DataSource = dt;
            comboBox2.ValueMember = "id";
            comboBox2.DisplayMember = "name";
        }
        private void addqt_Load(object sender, EventArgs e)
        {
            //BindYaoPin();
            BindQtList();
            BindDW();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sss.Count < 1 || (sss.Count == 1 &&sss.Contains(cmbName.Text.Trim())))
            {
                if (cmbName.Text != "" && tbYL.Text.Trim() != "")
                {
                    adims_MODEL.mzqt mz1 = new adims_MODEL.mzqt();
                    mz1.Qtname = cmbName.Text.Trim();
                    mz1.Yl = Convert.ToDouble(tbYL.Text);
                    mz1.Dw = comboBox2.Text;
                    mz1.Sysj = DateTime.Now;
                    mz1.Bz = 1;
                    int m = bll.addqt(mzjldid, mz1);
                    if (m > 0)
                    {
                        BindQtList();
                    }
                }
                else
                {
                    MessageBox.Show("用量和药名不能为空");
                }
            }
            else MessageBox.Show("气体数量超标！");           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows[0].Cells["flags"].Value.ToString() == "1")
            {
                int p = bll.endqt(mzjldid, DateTime.Now, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                BindQtList();
            }
            else MessageBox.Show("气体用药已经结束。");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count ==1)
            {
                if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                   int q = bll.delqt(mzjldid, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                   if (q > 0)
                   {
                       BindQtList();
                   }
                }
            }
            else
                MessageBox.Show("请选择需要删除的气体药！");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.DataValid.Text_Value_Limit(sender, e);

        }


    }

}
