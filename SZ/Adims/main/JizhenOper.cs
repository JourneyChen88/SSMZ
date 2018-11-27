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
    public partial class JizhenOper : Form
    {
        string yiyuanID;//新医院或者旧医院
        public JizhenOper()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        private void btnConfirm_Click(object sender, EventArgs e)
        {

            string patid = dtOdate.Value.ToString("yyyyMMddHHmmss");
            string oroom = cmbOroom.Text.Trim();
            string patname = tbPatname.Text.Trim();
            DateTime odate = DateTime.Parse(dtOdate.Value.Date.ToString("yyyy-MM-dd"));
           // int ASAE = 1;
            if (oroom=="")
            {
                MessageBox.Show("手术间不能为空！");
                cmbOroom.Focus();
                return;
            }
            int i = dal.InsertPAIBAN(patid, patname, oroom, odate,yiyuanID);//插入急诊排班
            if (i>0)
            {
                mzjldEdit mzjld1 = new mzjldEdit(patid, oroom, odate, 0);
                mzjld1.Owner = this;
                this.Hide();
                
                mzjld1.ShowDialog();
                Application.ExitThread();
                //this.Close();
            }
        }

        private void JizhenOper_Load(object sender, EventArgs e)
        {
            yiyuanID = Program.customer.yiyuanType;
            cmbOroom.Items.Clear();
            DataTable dt1 = dal.GetOROOM(yiyuanID);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                this.cmbOroom.Items.Add(dt1.Rows[i][0]);
            }
        }
    }
}
