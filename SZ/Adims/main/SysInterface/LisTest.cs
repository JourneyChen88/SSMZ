using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;
using adims_BLL;

namespace main
{
    public partial class LisTest : Form
    {
        SQLhelpLis lishelp = new SQLhelpLis();

        string applyid;
        string patID;
        public LisTest(string Applyid,string patid)
        {
            patID = patid;
            applyid = Applyid;
            InitializeComponent();
        }
        AdimsController ac = new AdimsController();
        private void LisTest_Load(object sender, EventArgs e)
        {
            this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            this.Top = 0;
            databind();
            DataTable dt = ac.SelectPatInfo(patID);
            txtName.Controls[0].Text = Convert.ToString(dt.Rows[0]["patname"]);
            txtAge.Controls[0].Text = Convert.ToString(dt.Rows[0]["patage"]);
            txtSex.Controls[0].Text = Convert.ToString(dt.Rows[0]["patsex"]);
            txtZhuYuanHao.Controls[0].Text = Convert.ToString(dt.Rows[0]["PatZhuYuanID"]);
            this.txtBingQu.Controls[0].Text = Convert.ToString(dt.Rows[0]["patdpm"]);
            this.txtBednumber.Controls[0].Text = Convert.ToString(dt.Rows[0]["patbedno"]);
        }
        DB2help hishelp = new DB2help();
        private void databind()
        {
            dataGridView1.Rows.Clear();

            DataTable dt1 = hishelp.GetLisbyPatid(applyid);
            if (dt1.Rows.Count == 0)
                MessageBox.Show("无检验报告！");
            else
                dataGridView1.DataSource = dt1.DefaultView;
        }
                private void button1_Click(object sender, EventArgs e)
        {
            databind();
        }
    }
}
