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
    public partial class JKTimeSet : Form
    {
        adims_DAL.AdimsProvider dal=new  adims_DAL.AdimsProvider ();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        int mzjldID;
        //DateTime jkksTime = new DateTime();
        //DateTime jkjsTime = new DateTime();
        public JKTimeSet(int mzid)
        {
            mzjldID=mzid;
            InitializeComponent();
        }

        private void JKTimeSet_Load(object sender, EventArgs e)
        {
            this.dtStart.Format = DateTimePickerFormat.Custom;
            this.dtStart.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtEnd.Format = DateTimePickerFormat.Custom;
            this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm";
            DataTable dtMZ_Info = bll.SelectOldPatInfo(mzjldID);
            if (dtMZ_Info.Rows.Count > 0)
            {
                if (dtMZ_Info.Rows[0]["jkkssj"].ToString() != "")
                {
                    dtStart.Value = Convert.ToDateTime(dtMZ_Info.Rows[0]["jkkssj"]);
                }
                if (dtMZ_Info.Rows[0]["jkkssj"].ToString() != "")
                {
                    dtEnd.Value = Convert.ToDateTime(dtMZ_Info.Rows[0]["jkjssj"]);
                }
            }

        }
        private string jkhxcv;
        public string Jkhxcv
        {
            get { return jkhxcv; }
            set { jkhxcv = value; }
        }
        private string tv;
        public string Tv
        {
            get { return tv; }
            set { tv = value; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int i = dal.Update_MZJLD_jikongTime1(mzjldID, dtStart.Value, dtEnd.Value, txtjkValue.Text, textTV.Text);
            jkhxcv = this.txtjkValue.Text;
            tv = this.textTV.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = dal.Update_MZJLD_jikongTime(mzjldID, dtStart.Value, dtEnd.Value);
            jkhxcv = this.txtjkValue.Text = "";
            this.DialogResult = DialogResult.OK;
        }

        
    }
}
