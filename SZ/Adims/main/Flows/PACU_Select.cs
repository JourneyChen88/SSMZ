using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using main.UserSecurity;

namespace main
{
    public partial class PACU_Select : Form
    {
        public PACU_Select()
        {
            InitializeComponent();
        }
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0)
            {

                MessageBox.Show("请选择病人！");
            }
            else
            {
                string patID = dataGridView1.CurrentRow.Cells["病人编号"].Value.ToString();
                string mzID = dataGridView1.CurrentRow.Cells["麻醉编号"].Value.ToString();

                PACU_SZ pacuform = new PACU_SZ(patID, mzID);
                pacuform.ShowDialog();

            }
        }
        private void DataBind()
        {
            DataSet ds = new DataSet();
            ds = bll.PACU_table1(dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"));
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DataBind();
        }

        private void PACU_Select_Load(object sender, EventArgs e)
        {
            DataBind();
        }

    }
}
