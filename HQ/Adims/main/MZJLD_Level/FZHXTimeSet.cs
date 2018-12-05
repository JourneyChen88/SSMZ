using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL.Flows;

namespace main
{
    public partial class FZHXTimeSet : Form
    {
        MzjldDal _MzjldDal = new MzjldDal();
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        int mzjldID;
        public FZHXTimeSet(int mzid)
        {
            mzjldID = mzid;
            InitializeComponent();
        }

        private void FZHXTime_Load(object sender, EventArgs e)
        {
            this.dtStart.Format = DateTimePickerFormat.Custom;
            this.dtStart.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtEnd.Format = DateTimePickerFormat.Custom;
            this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm";
            DataTable dtMZ_Info = _MzjldDal.GetMzjldByMzjldId(mzjldID);
            if (dtMZ_Info.Rows.Count > 0)
            {
                if (dtMZ_Info.Rows[0]["fzkssj"].ToString() != "")
                {
                    dtStart.Value = Convert.ToDateTime(dtMZ_Info.Rows[0]["fzkssj"]);
                }
                if (dtMZ_Info.Rows[0]["fzjssj"].ToString() != "")
                {
                    dtEnd.Value = Convert.ToDateTime(dtMZ_Info.Rows[0]["fzjssj"]);
                }
                txtfzValue.Text = dtMZ_Info.Rows[0]["fzvalue"].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = dal.Update_MZJLD_fuzhuTime(mzjldID, dtStart.Value, dtEnd.Value,txtfzValue.Text);
            if (i > 0)
            {
                //int rs = dal.Update_MZJLD_fzpoint(mzjldID, dtStart.Value, dtEnd.Value, txtfzValue.Text);
                //if (rs > 0) 
                    this.Close();
                //else MessageBox.Show("设置失败！请重试！");
            }
            else MessageBox.Show("设置失败！请重试！");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
