using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;
using adims_Utility;

namespace main
{
    public partial class ZoomTimeSet : Form
    {
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        int MzjldID;
        //DateTime jkksTime = new DateTime();
        //DateTime jkjsTime = new DateTime();



        public ZoomTimeSet(int mzid)
        {
            MzjldID = mzid;
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextValueLimit.Int_Limit(sender, e);
        }

        private void ZoomTimeSet_Load(object sender, EventArgs e)
        {
            this.dtStart.Format = DateTimePickerFormat.Custom;
            this.dtStart.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtEnd.Format = DateTimePickerFormat.Custom;
            this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm";
            dtEnd.Value = dtStart.Value.AddMinutes(20);
            DataBind();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbInterval.Text == "")
            {
                MessageBox.Show("间隔时间不能为空");
                tbInterval.Focus();
                return;
            }
            DataTable dt = dal.Get_ZoomTime(MzjldID);
            foreach (DataRow dr in dt.Rows)
            {
                DateTime start = Convert.ToDateTime(dr["StartTime"]);
                DateTime end = Convert.ToDateTime(dr["EndTime"]);
                if (end > dtStart.Value && dtStart.Value > start)
                {
                    MessageBox.Show("开始时间设置不合理");
                    dtStart.Focus();
                    return;
                }
                if (end > dtEnd.Value && dtEnd.Value > start)
                {
                    MessageBox.Show("结束时间设置不合理");
                    dtEnd.Focus();
                    return;
                }
            }
            int flag = dal.Insert_ZoomTime(MzjldID, dtStart.Value, dtEnd.Value, Convert.ToInt32(tbInterval.Text));
            if (flag > 0)
            {
                DataBind();
            }
        }
        private void DataBind()
        {
            DataTable dt = dal.Get_ZoomTime(MzjldID);
            dgvZoomTime.DataSource = dt;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvZoomTime.SelectedRows.Count == 1)
            {
                int flag = dal.Del_ZoomTime(Convert.ToInt32(dgvZoomTime.CurrentRow.Cells["id"].Value));
                if (flag > 0)
                {
                    DataBind();
                }
            }
        }

        private void dgvZoomTime_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvZoomTime.CurrentRow;
            if (row != null)
            {
                dtStart.Value = Convert.ToDateTime(row.Cells["StartTime"].Value);
                dtEnd.Value = Convert.ToDateTime(row.Cells["EndTime"].Value);
                tbInterval.Value = Convert.ToInt32(row.Cells["Interval"].Value);
            }
        }
    }
}
