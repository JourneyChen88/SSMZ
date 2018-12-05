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
    public partial class Select_MZTJ : Form
    {
        public Select_MZTJ()
        {
            InitializeComponent();
        }
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        private void button1_Click(object sender, EventArgs e)
        {
            BindDGV();
        }
        /// <summary>
        /// 绑定dgv
        /// </summary>
        private void BindDGV()
        {
            DateTime date1 = Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd 0:00"));
            DateTime date2 = Convert.ToDateTime(dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59"));
            DataTable dt = bll.Get_mzdj(date1, date2);
            dataGridView1.DataSource = dt.DefaultView;
        }

        private void btnPrintResult_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                PrintTJ.Print(dataGridView1, "天津红桥麻醉登记", "", 1);
            } 
        }
    }
}
