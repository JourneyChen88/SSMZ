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
    
    public partial class UpdateOdate : Form
    {
        string patid;
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        public UpdateOdate(string PatID)
        {
            patid = PatID;
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            int Jieguo = dal.UpdatePaibanDate(dtOdate.Value.ToString("yyyy-MM-dd"), patid);//修改成 已排班
            if (Jieguo > 0)
            {
                MessageBox.Show("手术日期修改成功！");
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
