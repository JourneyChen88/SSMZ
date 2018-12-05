using adims_DAL;
using adims_DAL.Flows;
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
        MzjldDal _MzjldDal = new MzjldDal();
        private void JKTimeSet_Load(object sender, EventArgs e)
        {
            this.dtStart.Format = DateTimePickerFormat.Custom;
            this.dtStart.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtEnd.Format = DateTimePickerFormat.Custom;
            this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm";
            DataTable dtMZ_Info = _MzjldDal.GetMzjldByMzjldId(mzjldID);
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
                txtjkValue.Text = dtMZ_Info.Rows[0]["jkvalue"].ToString();
            }

        }
        MzjldPointDal mpdal = new MzjldPointDal();
        private void button1_Click(object sender, EventArgs e)
        {
            int  i=dal.Update_MZJLD_jikongTime(mzjldID, dtStart.Value, dtEnd.Value,txtjkValue.Text);
            if (i > 0)
            {
                int rs = mpdal.UpdateMzjldPointJKHX(mzjldID, dtStart.Value, dtEnd.Value, txtjkValue.Text);
                if (rs > 0) this.Close();
                else MessageBox.Show("设置失败！请重试！");
            }
            else MessageBox.Show("设置失败！请重试！");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
