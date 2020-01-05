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
    public partial class MZcl : Form
    {
        public MZcl(DateTime d, int l, int m)
        {

            lx = l;
            //clcxqt = c;
            sj = d;
            mzjldid = m;
            InitializeComponent();
            if (lx == 1)
            {
                groupBox1.Text = "失血量";
            }
            if (lx == 3)
            {
                groupBox1.Text = "引流量";
            }
        }
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();

        List<adims_MODEL.clcxqt> clcxqt;
        DateTime sj;
        int lx;
        int mzjldid;

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void MZcl_Load(object sender, EventArgs e)
        {
            BindDGV();
        }
        /// <summary>
        /// 绑定DGV
        /// </summary>
        private void BindDGV()
        {
            DataTable dt = bll.Getcl_mzd_cl(mzjldid, lx);
            dataGridView1.DataSource = dt;
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.TextValueLimit.Text_Value_Limit(sender, e);
        }
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["value"].Value.ToString();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnadd_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                adims_MODEL.clcxqt q = new adims_MODEL.clcxqt();
                q.V = Convert.ToInt32(textBox1.Text);
                q.D = sj;
                q.Lx = lx;
                int i = bll.addclcxqt(mzjldid, q);
                if (i != 1)
                {
                    MessageBox.Show("错误" + i.ToString());
                }
                q.Id = bll.clid(mzjldid, q);
                BindDGV();
                this.Close();
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int c = 1;
            if (dataGridView1.Rows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
                int i = bll.updateclcxqt_mzd(id, textBox1.Text);
                if (i != 1)
                {
                    MessageBox.Show("错误" + i.ToString());
                }
                else
                {
                    BindDGV();
                }
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
                int i = bll.DelteCl(id);
                if (i != 1)
                {
                    MessageBox.Show("错误" + i.ToString());
                }
                else
                {
                    BindDGV();
                }

            }
        }
    }

}
