using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;
using Adims_Utility;

namespace main
{
    public partial class MZYYJF_Select : Form
    {
        public MZYYJF_Select()
        {
            InitializeComponent();
        }
        AdimsProvider apro = new AdimsProvider();
        PACU_DAL pacu = new PACU_DAL();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        /// <summary>
        /// 绑定DGV
        /// </summary>
        private void DataBind()
        {
            DataTable dt = bll.xssbr2(dateTimePicker2.Value.ToString("yyyy-MM-dd"));
            dataGridView1.DataSource = dt.DefaultView;
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MZYYZT_Select_Load(object sender, EventArgs e)
        {
            DataBind();
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {

                MessageBox.Show("请选择病人！");
            }
            else
            {
                string mzjldid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                //string patID = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string patID = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                //string patSEX = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                //string patAGE = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                //string uid = Program.customer.uid;
                //string name = Program.customer.user_name;
                //DataTable dt = pacu.GetSelectHisCode(uid, name);
                //if (dt.Rows.Count > 0)
                //{
                //    string admin= dt.Rows[0]["hisCode"].ToString();
                //    if (admin.IsNullOrEmpty())
                //    {
                //        MessageBox.Show("该操作员的his代码为空，禁止操作！");
                //        return;
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("操作错误，请重新操作");
                //    return;
                //}AddMZZB
                AddMZZB f2 = new AddMZZB(mzjldid, patID);
                f2.Show();
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DataBind();
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("请选择病人！");
            }
            else
            {
                string uid = Program.customer.uid;
                string name = Program.customer.user_name;
                DataTable dt = pacu.GetSelectHisCode(uid, name);
                if (dt.Rows.Count > 0)
                {
                    string admin = dt.Rows[0]["hisCode"].ToString();
                    if (admin.IsNullOrEmpty())
                    {
                        MessageBox.Show("该操作员的his代码为空，禁止操作！");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("操作错误，请重新操作");
                    return;
                }
                string mzjldid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string patID = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string patNAME = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                string patSEX = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                string patAGE = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                MZYYJF f2 = new MZYYJF(mzjldid, patID);
                f2.Show();
            }
        }
    }
}
