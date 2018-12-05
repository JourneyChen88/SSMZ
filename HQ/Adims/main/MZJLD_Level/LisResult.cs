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
    public partial class LisResult : Form
    {
        OperScheduleDal _PaibanDal = new OperScheduleDal();
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        adims_DAL.LIS_DB_Help ldal = new adims_DAL.LIS_DB_Help();
        string PatID; string Odate;
        public LisResult(string patid,string date)
        {
            PatID=patid;
            Odate = date;         
            InitializeComponent();
        }

        private void jybl_Load(object sender, EventArgs e)
        {
            BindPatInfo();
            //dataGridView1.DataSource = ldal.GetLIS_ReportFormFullbyPATID(DateTime.Now.ToString("yyyy-MM-dd"));
            DataTable dtReportFormFull = ldal.GetLIS_ReportFormFullbyPATID(PatID,Odate);
            dataGridView1.DataSource = dtReportFormFull;
            //dataGridView2.DataSource = ldal.GetLIS_ReportItemViewbyPATID(dtReportFormFull.Rows[0]["SerialNo"].ToString());
            //dataGridView3.DataSource = ldal.GetLIS_ReportMicroView(DateTime.Now.ToString("yyyy-MM-dd"));
        }
        private void BindPatInfo()
        {
            DataTable dt = new DataTable();
            dt = _PaibanDal.GetPaibanByPatId(PatID);
            lbPatID.Text = "病人编号：" + PatID;
            lbPatname.Text="病人姓名："+dt.Rows[0]["Patname"].ToString();
            lbAge.Text = "年龄：" + dt.Rows[0]["Patage"].ToString();
            lbSex.Text = "性别：" + dt.Rows[0]["Patsex"].ToString();
            lbKeshi.Text = "科室：" + dt.Rows[0]["patdpm"].ToString();
            lbBedNO.Text = "床号："+dt.Rows[0]["Patbedno"].ToString();
            lbSqzd.Text = "术前诊断：" + dt.Rows[0]["Pattmd"].ToString();
            dtOdate.Text = Odate;

        }

        private void dtOdate_ValueChanged(object sender, EventArgs e)
        {
            DataTable dtReportFormFull = ldal.GetLIS_ReportFormFullbyPATID(PatID, Convert.ToDateTime(dtOdate.Text).ToString("yyyy-MM-dd"));
            dataGridView1.DataSource = dtReportFormFull;
        }
    }
}
