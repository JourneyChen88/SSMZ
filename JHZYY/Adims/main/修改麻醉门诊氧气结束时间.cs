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
    public partial class 修改麻醉门诊氧气结束时间 : Form
    {
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        int mzjldID = 0;
        public 修改麻醉门诊氧气结束时间( int mzid)
        {
            mzjldID = mzid;
            InitializeComponent();
        }

        private void 修改麻醉门诊氧气结束时间_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dal.Updateyangqijismenzhen(dateTimePicker1.Value, mzjldID);
        }
    }
}
