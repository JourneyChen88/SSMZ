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
    public partial class updatejcsj : Form
    {
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        int mzjldID;
        public updatejcsj(int mzid)
        {
             mzjldID = mzid;
            InitializeComponent();
        }

        private void updatejcsj_Load(object sender, EventArgs e)
        {
            this.dtStart.Format = DateTimePickerFormat.Custom;
            this.dtStart.CustomFormat = "yyyy-MM-dd HH:mm";
            DateTime dt = DateTime.Now;

            this.dtEnd.Format = DateTimePickerFormat.Custom;
            this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm";

            this.dtEnd.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbxjhlx.Text == "收缩压")
            {
            int rs = dal.Update_MZJLD_jkpointNIBPS(mzjldID, dtStart.Value, dtEnd.Value, txtjkValue.Text);
            if (rs > 0) this.Close();
            else MessageBox.Show("收缩压：设置失败！请重试！");
            }
            else if (cmbxjhlx.Text == "舒张压")
            {
                int rs = dal.Update_MZJLD_jkpointNIBPD(mzjldID, dtStart.Value, dtEnd.Value, txtjkValue.Text);
                if (rs > 0) this.Close();
                else MessageBox.Show("舒张压：设置失败！请重试！");
            }
            else if (cmbxjhlx.Text == "呼吸")
            {
                int rs = dal.Update_MZJLD_jkpointRRC(mzjldID, dtStart.Value, dtEnd.Value, txtjkValue.Text);
                if (rs > 0) this.Close();
                else MessageBox.Show("呼吸：设置失败！请重试！");
            }
            else if (cmbxjhlx.Text == "脉搏")
            {
                int rs = dal.Update_MZJLD_jkpointPULSE(mzjldID, dtStart.Value, dtEnd.Value, txtjkValue.Text);
                if (rs > 0) this.Close();
                else MessageBox.Show("脉搏：设置失败！请重试！");
            }
            else if (cmbxjhlx.Text == "体温")
            {
                int rs = dal.Update_MZJLD_jkpointTEMP(mzjldID, dtStart.Value, dtEnd.Value, txtjkValue.Text);
                if (rs > 0) this.Close();
                else MessageBox.Show("体温：设置失败！请重试！");
            }
            else if (cmbxjhlx.Text == "SPO2")
            {
                int rs = dal.Update_MZJLD_jkpointSPO2(mzjldID, dtStart.Value, dtEnd.Value, txtjkValue.Text);
                if (rs > 0) this.Close();
                else MessageBox.Show("SPO2：设置失败！请重试！");
            }
            else if (cmbxjhlx.Text == "ETCO2")
            {
                int rs = dal.Update_MZJLD_jkpointETCO2(mzjldID, dtStart.Value, dtEnd.Value, txtjkValue.Text);
                if (rs > 0) this.Close();
                else MessageBox.Show("ETCO2：设置失败！请重试！");
            }
            else if (cmbxjhlx.Text == "CVP")
            {
                int rs = dal.Update_MZJLD_jkpointCVP(mzjldID, dtStart.Value, dtEnd.Value, txtjkValue.Text);
                if (rs > 0) this.Close();
                else MessageBox.Show("CVP：设置失败！请重试！");
            }
            else if (cmbxjhlx.Text == "BIS")
            {
                int rs = dal.Update_MZJLD_jkpointBIS(mzjldID, dtStart.Value, dtEnd.Value, txtjkValue.Text);
                if (rs > 0) this.Close();
                else MessageBox.Show("BIS：设置失败！请重试！");
            }
        }
    }
}
