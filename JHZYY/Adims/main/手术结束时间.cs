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
    public partial class 手术结束时间 : Form
    {
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        int mzjldID = 0;
        public 手术结束时间(int  mzd)
        {
            mzjldID = mzd;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dal.UpdateSsjssjmenzhen(dateTimePicker1.Value, mzjldID);
        }

        private void 手术结束时间_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm";
        }
    }
}
