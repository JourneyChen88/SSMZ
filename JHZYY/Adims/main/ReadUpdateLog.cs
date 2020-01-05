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
    public partial class ReadUpdateLog : Form
    {
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        public ReadUpdateLog()
        {
            InitializeComponent();
        }
        private void BindUpdateLog()
        {
            dgvLog.DataSource = bll.GetxgLog(dtOprDate.Value.ToString("yyyy-MM-dd"));
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            BindUpdateLog();
        }

        private void ReadUpdateLog_Load(object sender, EventArgs e)
        {
            BindUpdateLog();
        }

        private void dgvLog_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int mzjldID = int.Parse(dgvLog.CurrentRow.Cells["mzjldid"].Value.ToString());
            DataTable dtMzjld = bll.selectmzjld(mzjldID);
            lbSSMC.Text = "手术名称："+Convert.ToString(dtMzjld.Rows[0]["ShoushuFS"]);
            lbMZFF.Text = "麻醉方法：" + Convert.ToString(dtMzjld.Rows[0]["mazuiFS"]);
        }
    }
}
