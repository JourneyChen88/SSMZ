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
    public partial class NurseRecord_Select : Form
    {
        string operAddress = "";
        AdimsProvider apro = new AdimsProvider();
        public NurseRecord_Select()
        {
            InitializeComponent();
        }

        private void NurseRecord_Select_Load(object sender, EventArgs e)
        {
            operAddress = Program.customer.yiyuanType;
            DataBind();
        }
        private void DataBind()
        {
            DataTable dt1 = apro.GetmazuizongjieList(dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"),operAddress);
            dataGridView1.DataSource = dt1.DefaultView;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DataBind();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string mzjldid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string patID = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string patNAME = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                string patSEX = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                string patAGE = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                if (cbEye.Checked)
                {
                    NurseRecord_SZ_Eye f1 = new NurseRecord_SZ_Eye(patID, mzjldid);
                    f1.ShowDialog();
                }
                else
                {
                    NurseRecord_SZ f2 = new NurseRecord_SZ(patID, mzjldid);
                    f2.ShowDialog();
                }
            }
            else MessageBox.Show("请选择病人！");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
