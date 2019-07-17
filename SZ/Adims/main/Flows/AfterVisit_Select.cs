using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;

namespace main
{
    public partial class AfterVisit_Select : Form
    {
        string yiyuanID = "";
        public AfterVisit_Select()
        {
            InitializeComponent();
        }
        AdimsProvider apro = new AdimsProvider();
        private void AfterVisit_Select_Load(object sender, EventArgs e)
        {
            yiyuanID = Program.Customer.yiyuanType;
            DataBind();
        }
        private void DataBind()
        {
            DataTable dt1 = apro.GetmazuizongjieList(dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"), yiyuanID);
            dataGridView1.DataSource = dt1.DefaultView;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DataBind();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count>0)
            {
                string mzjldid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string patID = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                AfterVisit_SZ f2 = new AfterVisit_SZ(mzjldid, patID);
                f2.ShowDialog();
                
            }
            else MessageBox.Show("请选择病人！");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
