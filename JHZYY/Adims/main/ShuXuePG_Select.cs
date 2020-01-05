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
    public partial class ShuXuePG_Select : Form
    {
        AdimsProvider apro = new AdimsProvider();
        public ShuXuePG_Select()
        {
            InitializeComponent();
        }

        private void ShuXuePG_Select_Load(object sender, EventArgs e)
        {
            DataBind();
        }
        /// <summary>
        /// 绑定DGV
        /// </summary>
        private void DataBind()
        {
            DataTable dt1 = apro.GetmazuizongjieList(dateTimePicker2.Value.ToString("yyyy-MM-dd"));
            dataGridView1.DataSource = dt1.DefaultView;
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
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
                ShuXuePG f2 = new ShuXuePG(mzjldid, patID);
                f2.ShowDialog();
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DataBind();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
