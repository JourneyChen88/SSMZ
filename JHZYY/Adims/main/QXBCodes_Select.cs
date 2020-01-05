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
    public partial class QXBCodes_Select : Form
    {
        int isN = -1;
        adims_BLL.AdimsController dal = new adims_BLL.AdimsController();
        public QXBCodes_Select()
        {
            InitializeComponent();
        }

        private void QXBCodes_Select_Load(object sender, EventArgs e)
        {
            isN = 0;
            DataBind();
        }
        /// <summary>
        /// 绑定dgv
        /// </summary>
        private void DataBind()
        {
            DataTable dt1 = dal.BingRen(dtDATE.Value.Date.ToString("yyyy-MM-dd"),isN);
            dataGridView1.DataSource = dt1.DefaultView;
        }
        /// <summary>
        /// 已扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdOld_CheckedChanged(object sender, EventArgs e)
        {
            if (rdOld.Checked)
            {
                isN = 1;
                dataGridView1.DataSource = dal.BingRen(dtDATE.Value.ToString("yyyy-MM-dd"), isN).DefaultView;
            }
        }
        /// <summary>
        /// 未扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdNew_CheckedChanged(object sender, EventArgs e)
        {
            if (rdNew.Checked)
            {
                isN = 0;
                dataGridView1.DataSource = dal.BingRen(dtDATE.Value.ToString("yyyy-MM-dd"), isN).DefaultView;
            }
        }

        private void dtDATE_ValueChanged(object sender, EventArgs e)
        {
            DataBind();
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count>0)
            {
                string patID = dataGridView1.CurrentRow.Cells["住院号"].Value.ToString();
                QXBCodes f2 = new QXBCodes(patID, dtDATE.Value.ToString("yyyy-MM-dd"));
                f2.ShowDialog();
            }
        
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                string patID = dataGridView1.CurrentRow.Cells["住院号"].Value.ToString();
                QXBCodes f2 = new QXBCodes(patID, dtDATE.Value.ToString("yyyy-MM-dd"));
                f2.ShowDialog();
            }
        }
    }
}
