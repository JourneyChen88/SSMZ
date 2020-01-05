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
    public partial class JKTimeSetwu : Form
    {

        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        int mzjldID;
        //DateTime jkksTime = new DateTime();
        //DateTime jkjsTime = new DateTime();
        public JKTimeSetwu(int mzid)
        {
            mzjldID = mzid;

            InitializeComponent();
        }

        private void JKTimeSetwu_Load(object sender, EventArgs e)
        {
            this.dtStart.Format = DateTimePickerFormat.Custom;
            this.dtStart.CustomFormat = "yyyy-MM-dd HH:mm";
            DateTime dt = DateTime.Now;

            this.dtEnd.Format = DateTimePickerFormat.Custom;
            this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm";

            this.dtEnd.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00"));
            //this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm";

            DataTable dtMZ_Info = bll.SelectOldPatInfo(mzjldID);

            if (dtMZ_Info.Rows.Count > 0)
            {
                if (dtMZ_Info.Rows[0]["jkkssj"].ToString() != "")
                {
                    dtStart.Value = Convert.ToDateTime(dtMZ_Info.Rows[0]["jkkssj"]);
                }
                if (dtMZ_Info.Rows[0]["jkjssj"].ToString() != "")
                {
                    dtEnd.Value = Convert.ToDateTime(dtMZ_Info.Rows[0]["jkjssj"]);
                }
                if (dtStart.Value > dtEnd.Value)
                {

                    this.dtEnd.Format = DateTimePickerFormat.Custom;
                    this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm";

                    this.dtEnd.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00"));
                }
                txtjkValue.Text = dtMZ_Info.Rows[0]["jkvalue"].ToString();
                textTV.Text = dtMZ_Info.Rows[0]["fzvalue"].ToString();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = dal.Update_MZJLD_jikongTime1(mzjldID, dtStart.Value, dtEnd.Value, txtjkValue.Text, textTV.Text);
            if (i > 0)
            {
                int rs = dal.Update_MZJLD_jkpoint(mzjldID, dtStart.Value, dtEnd.Value, txtjkValue.Text);
                if (rs > 0) this.Close();
                else MessageBox.Show("设置失败！请重试！");
            }
            else MessageBox.Show("设置失败！请重试！");


            //if (i > 0)
            //{
            //    int rs = dal.Update_MZJLD_jkpoint(mzjldID, dtStart.Value, dtEnd.Value, txtjkValue.Text);
            //    if (rs > 0) this.Close();
            //    else MessageBox.Show("设置失败！请重试！");
            //}
            //else MessageBox.Show("设置失败！请重试！");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        





    }
}
