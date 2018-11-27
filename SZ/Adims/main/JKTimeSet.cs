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
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        int mzjldID;
        int JKtype;
        DateTime jkksTime = new DateTime();
        DateTime jkjsTime = new DateTime();
        public JKTimeSet(int mzid, int type)
        {
            mzjldID = mzid;
            JKtype = type;
            InitializeComponent();
        }

        private void JKTimeSet_Load(object sender, EventArgs e)
        {
            this.dtStart.Format = DateTimePickerFormat.Custom;
            this.dtStart.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtEnd.Format = DateTimePickerFormat.Custom;
            this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm";
            if (JKtype == 0)
            {

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
            else
            {
                DataTable dtMZ_Info = bll.SelectOldPatInfo1(mzjldID);
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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (JKtype == 0)
            {
                int i = dal.Update_MZJLD_jikongTime(mzjldID, dtStart.Value, dtEnd.Value);
                if (i > 0) this.Close(); else MessageBox.Show("设置失败！请重试！");
            }
            else
            {
                int i = dal.Update_PACU_jikongTime(mzjldID, dtStart.Value, dtEnd.Value);
                if (i > 0) this.Close(); else MessageBox.Show("设置失败！请重试！");
            }

        }


    }
}
