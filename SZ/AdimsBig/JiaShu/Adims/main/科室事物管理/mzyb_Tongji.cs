using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;

namespace main.科室事物管理
{
    public partial class mzyb_Tongji : Form
    {
        AdimsProvider apro = new AdimsProvider();
        public mzyb_Tongji()
        {
            InitializeComponent();
        }

        private void mzyb_Tongji_Load(object sender, EventArgs e)
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            //初始化年第一天
            dtstart.Text = year + "/1/1";
            //初始化月份第一天
            dtstart.Text = year + "/" + month + "/1";            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime date1 = dtstart.Value;
            DateTime date2 = dtEnd.Value;
            DataTable dt = apro.GetYSMZLbyOKeshi1(date1, date2);
        }
    }
}
