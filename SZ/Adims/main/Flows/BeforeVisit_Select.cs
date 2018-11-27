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
    public partial class BeforeVisit_Select : Form
    {
        string yiyuanID =""; 
        adims_DAL.AdimsProvider apro = new adims_DAL.AdimsProvider();
        adims_BLL.AdimsController acon = new adims_BLL.AdimsController();
        public BeforeVisit_Select()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count >= 1)
            {
                string patID = dataGridView1.CurrentRow.Cells["病人编号"].Value.ToString();
                BeforeVisit_YS f2 = new BeforeVisit_YS(patID);
                f2.ShowDialog();
            }
            else MessageBox.Show("请选择病人！");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void DataBind()
        {
            DataTable dt1 = acon.Sqfs1(dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"), yiyuanID);
            dataGridView1.DataSource = dt1.DefaultView;
        }

        private void BeforeVisit_Select_Load(object sender, EventArgs e)
        {
            yiyuanID = Program.customer.yiyuanType;
            DataBind();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DataBind();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count >= 1)
            {
                string patID = dataGridView1.CurrentRow.Cells["病人编号"].Value.ToString();
                BeforeVisit_HS f2 = new BeforeVisit_HS(patID);
                f2.ShowDialog();
                

            }
            else MessageBox.Show("请选择病人！");
        }
    }
}
