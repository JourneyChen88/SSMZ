using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;
using main.CGG;

namespace main
{
    public partial class NurseRecord_Select : Form
    {
        AdimsProvider apro = new AdimsProvider();
        adims_BLL.AdimsController acon = new adims_BLL.AdimsController();
        public NurseRecord_Select()
        {
            InitializeComponent();
        }

        private void NurseRecord_Select_Load(object sender, EventArgs e)
        {
            DataBind();
        }
        private void DataBind()
        {
            DataTable dt1 = acon.Sqfs1(dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"));
            dataGridView1.DataSource = dt1.DefaultView;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DataBind();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count >0)
            {
               // string mzjldid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string patID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
               // string patNAME = dataGridView1.CurrentRow.Cells[2].Value.ToString();
               // string patSEX = dataGridView1.CurrentRow.Cells[3].Value.ToString();
              //  string patAGE = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                SHHLJLdan f2 = new SHHLJLdan(patID);
                f2.ShowDialog();
            }
            else MessageBox.Show("请选择病人！");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string mzjldid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string patID = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string patNAME = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                string patSEX = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                string patAGE = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                NurseRecord_CJ f2 = new NurseRecord_CJ(patID, mzjldid);
                f2.ShowDialog();
            }
            else MessageBox.Show("请选择病人！");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string patID = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                BLbbsjdan f2 = new BLbbsjdan(patID);
                //BeforeVisit_HS f2 = new BeforeVisit_HS(patID);
                f2.ShowDialog();
            }
            else
            {

                MessageBox.Show("请选择病人");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string patID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                TNzuTiaoxingma f2 = new TNzuTiaoxingma(patID);
                //BeforeVisit_HS f2 = new BeforeVisit_HS(patID);
                f2.ShowDialog();
            }
            else
            {

                MessageBox.Show("请选择病人");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string patID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                SXhuliJudan f2 = new SXhuliJudan(patID);
                //BeforeVisit_HS f2 = new BeforeVisit_HS(patID);
                f2.ShowDialog();
            }
            else
            {

                MessageBox.Show("请选择病人");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string patID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                ShouShuAQHCB f2 = new ShouShuAQHCB(patID);
                //BeforeVisit_HS f2 = new BeforeVisit_HS(patID);
                f2.ShowDialog();
            }
            else
            {

                MessageBox.Show("请选择病人");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string patID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                CHAShouShuFXPG f2 = new CHAShouShuFXPG(patID);
                //BeforeVisit_HS f2 = new BeforeVisit_HS(patID);
                f2.ShowDialog();
            }
            else
            {

                MessageBox.Show("请选择病人");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string patID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            SHHZJEJLdan f2 = new SHHZJEJLdan(patID);
                //BeforeVisit_HS f2 = new BeforeVisit_HS(patID);
                f2.ShowDialog();
             
        }

     
        private void button8_Click_1(object sender, EventArgs e)
        {
            string patID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            SSsunshangpgb f2 = new SSsunshangpgb(patID);
            //BeforeVisit_HS f2 = new BeforeVisit_HS(patID);
            f2.ShowDialog();
        }

        

        private void button9_Click(object sender, EventArgs e)
        {

            string patID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            shousjf f1 = new shousjf(patID);
            f1.Show();
        }
    }
}
