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
    public partial class menzhenfashidan : Form
    {
        AdimsController bll = new AdimsController();
        AdimsProvider DAL = new AdimsProvider();
        public menzhenfashidan()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = DAL.GetPAIBANmenzhen(dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"));
            dgvJizhenPaiban.DataSource = dt.DefaultView;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = DAL.GetPAIBANmenzhenhao(menzhenhao.Text);
            dgvJizhenPaiban.DataSource = dt.DefaultView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string menzhenhao = dgvJizhenPaiban.CurrentRow.Cells["patid"].Value.ToString();
            string menzhencishu = dgvJizhenPaiban.CurrentRow.Cells["menzhenhaocishu"].Value.ToString();
            menzhenmazuijld f2 = new menzhenmazuijld(menzhenhao, menzhencishu);
            f2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string menzhenhao = dgvJizhenPaiban.CurrentRow.Cells["patid"].Value.ToString();
            string menzhencishu = dgvJizhenPaiban.CurrentRow.Cells["menzhenhaocishu"].Value.ToString();
            DataTable dt = bll.selectmenzhenjldsfcz(menzhencishu);
            if (dt.Rows.Count == 0)
            {
                menzhenjiludan f2 = new menzhenjiludan(menzhencishu);
                f2.Show();
            }
            else {

            string mzjldID = dt.Rows[0]["menzhenid"].ToString();
              menzhenjiludan f2 = new menzhenjiludan(mzjldID, menzhencishu);
                f2.Show();
            }
           // mzjldID = Convert.ToInt32(dt.Rows[0][0]);
           
        }

        
    }
}
