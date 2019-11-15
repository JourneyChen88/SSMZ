///************************************
///Updated By        : Senvi
///************************************

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
    public partial class UpdateOroom : Form
    {
        #region <<Members>>

     
        admin_T_SQL at = new admin_T_SQL();
        adims_DAL.AdimsProvider dal = new AdimsProvider();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        int dr, dc;
        DataGridView dgv;
        string patID,Odate;
        #endregion

        #region <<Constructors>>

        public UpdateOroom(DataGridView d, int r, int c)
        {
            dgv = d;
            dr = r;
            dc = c;
            InitializeComponent();
        }
        public UpdateOroom(string patid1,string dtime)
        {
            patID = patid1;
            Odate = dtime;
            InitializeComponent();
        }

        #endregion

        #region <<Events>>

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selaa_Load(object sender, EventArgs e)
        {
         
            cmbOroom.Items.Clear();
            DataTable dt1 = dal.GetOROOM();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                this.cmbOroom.Items.Add(dt1.Rows[i][0]);
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region MyRegion
            if (cmbOroom.Text.Trim()=="")
            {
                MessageBox.Show("手术间不能为空");
                cmbOroom.Focus();
                return;                
            }
            if (cmbSecond.Text.Trim() == "")
            {
                MessageBox.Show("台次不能为空");
                cmbSecond.Focus();
                return;
            }
            DataTable dt = dal.GetpaibanByOroomandTaici(cmbOroom.Text.Trim(), cmbSecond.Text.Trim(),DateTime.Parse(Odate), patID);
            if (dt.Rows.Count != 0)
            {
                MessageBox.Show("当前手术前当前台次已存在，请重新选择台次！");
                cmbSecond.Focus();
                return;
            }
            int result = at.updatePaibanOroom(patID, cmbOroom.Text.Trim(), cmbSecond.Text.Trim());
            if (result == 0)
                MessageBox.Show("修改失败");
            else
                this.Close();
           
            #endregion
            //switch (dc)
            //{
            //    case 2:
            //        dgv.Rows[dr].Cells[dc].Value = dataGridView1.SelectedCells[1].Value;
            //        this.Close();
            //        break;
            //    case 3:
            //       dgv.Rows[dr].Cells[dc].Value = dataGridView1.SelectedCells[1].Value;
            //       this.Close();
            //       break;
            //    case 4:
            //        dgv.Rows[dr].Cells[dc].Value = dataGridView1.SelectedCells[1].Value;
            //        this.Close();
            //        break;                
            //}
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
