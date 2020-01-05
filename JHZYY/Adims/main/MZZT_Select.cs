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
    public partial class MZZT_Select : Form
    {
        public MZZT_Select()
        {
            InitializeComponent();
        }
        AdimsProvider apro=new AdimsProvider();

        private void MZ_zhentong_Load(object sender, EventArgs e)
        {
            DataBind();
        }
        private void DataBind()
        {
            DataTable dt1 = apro.GetmazuizongjieList(dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"));
            dataGridView1.DataSource = dt1.DefaultView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells[0].Value == null)
            {

                MessageBox.Show("请选择病人！");
            }
            else
            {
                string mzjldid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string patID = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string patNAME = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                string patSEX = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                string patAGE = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                MZZT f2 = new MZZT(mzjldid, patID);
                f2.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DataBind();
        }
    }
}
