using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;

namespace main
{
    public partial class AfterVisit_JDZY : Form
    {
        AdimsController bll = new AdimsController();
        adims_DAL.AdimsProvider apro = new adims_DAL.AdimsProvider();
        string MZID, PATID;
        public AfterVisit_JDZY()
        {
            InitializeComponent();
        }
        public AfterVisit_JDZY(string mzid,string patid)
        {
            MZID = mzid;
            PATID = patid;
            InitializeComponent();
        }

        private void AfterVisit_JDZY_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            SetEditValue();
            BindPatInfo();
        }
        private void BindPatInfo()
        {
            DataTable dt = new DataTable();
            dt = apro.GetALLPAIBAN(PATID);            
            txtPatname.Controls[0].Text = dt.Rows[0]["Patname"].ToString();
            txtZhuyuanNo.Controls[0].Text = dt.Rows[0]["PatZhuYuanID"].ToString();            
            txtBednumber.Controls[0].Text = dt.Rows[0]["Patbedno"].ToString();
            txtNXSS.Controls[0].Text = dt.Rows[0]["Oname"].ToString();
            txtSQZD.Controls[0].Text = dt.Rows[0]["Pattmd"].ToString();
        }
        
        /// <summary>
        /// 清空控件
        /// </summary>
        private void ClearEdit()
        {
           
            txtPatname.Controls[0].Text = string.Empty;            
            txtBednumber.Controls[0].Text = string.Empty;
            txtZhuyuanNo.Controls[0].Text = string.Empty;
            txtSQZD.Controls[0].Text = string.Empty;
            txtNXSS.Controls[0].Text = string.Empty;
            txtXueYa.Controls[0].Text = string.Empty;
            txtXinLv.Controls[0].Text = string.Empty;
            txtHuXi.Controls[0].Text = string.Empty;
            cmbYHTT.Text = string.Empty;
            cmbSYSY.Text = string.Empty;
            cmbEX.Text = string.Empty;
            cmbOTu.Text = string.Empty;
            cmbYShi.Text = string.Empty;
            cmbTouTeng.Text = string.Empty;
            txtSiZhiFeel.Controls[0].Text = string.Empty;
            cmbNCN.Text = string.Empty;
            cmbTengTong.Text = string.Empty;
            cmbHongZhong.Text = string.Empty;
            txtOther.Controls[0].Text = string.Empty;
            txtYiSheng.Controls[0].Text = string.Empty;

        }
        /// <summary>
        /// 控件赋值
        /// </summary>
        private void SetEditValue( )
        {
            ClearEdit();
            DataTable dt = apro.GetAfterVisitCount2(PATID);
            if (dt.Rows.Count > 0)
            {

                txtXueYa.Controls[0].Text = Convert.ToString(dt.Rows[0]["XueYa"]);
                txtXinLv.Controls[0].Text = Convert.ToString(dt.Rows[0]["XinLv"]);
                txtHuXi.Controls[0].Text = Convert.ToString(dt.Rows[0]["HuXi"]);
                cmbYHTT.Text = Convert.ToString(dt.Rows[0]["YHTT"]);
                cmbSYSY.Text = Convert.ToString(dt.Rows[0]["SYSY"]);
                cmbEX.Text = Convert.ToString(dt.Rows[0]["EX"]);
                cmbOTu.Text = Convert.ToString(dt.Rows[0]["OTu"]);
                cmbYShi.Text = Convert.ToString(dt.Rows[0]["YShi"]);
                cmbTouTeng.Text = Convert.ToString(dt.Rows[0]["TouTeng"]);
                txtSiZhiFeel.Controls[0].Text = Convert.ToString(dt.Rows[0]["SiZhiFeel"]);
                cmbNCN.Text = Convert.ToString(dt.Rows[0]["NCN"]);
                cmbTengTong.Text = Convert.ToString(dt.Rows[0]["TengTong"]);
                cmbHongZhong.Text = Convert.ToString(dt.Rows[0]["HongZhong"]);
                txtOther.Controls[0].Text = Convert.ToString(dt.Rows[0]["Other"]);
                txtYiSheng.Controls[0].Text = Convert.ToString(dt.Rows[0]["YiSheng"]);
                dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0]["VisitTime"]);
            }
            else return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveAfterVisit();
        }
        private void SaveAfterVisit()
        {
            if (!ValiDateAfterVisit()) return;
            Dictionary<string, string> AfterVisitList = new Dictionary<string, string>();
            int result = 0;
            try
            {
                AfterVisitList.Add("patID", PATID);
                AfterVisitList.Add("XueYa", txtXueYa.Controls[0].Text);
                AfterVisitList.Add("XinLv", txtXinLv.Controls[0].Text);
                AfterVisitList.Add("HuXi", txtHuXi.Controls[0].Text);
                AfterVisitList.Add("YHTT", cmbYHTT.Text);
                AfterVisitList.Add("SYSY", cmbSYSY.Text);
                AfterVisitList.Add("EX", cmbEX.Text);
                AfterVisitList.Add("OTu", cmbOTu.Text);
                AfterVisitList.Add("YShi", cmbYShi.Text);
                AfterVisitList.Add("TouTeng", cmbTouTeng.Text);
                AfterVisitList.Add("NCN", cmbNCN.Text);
                AfterVisitList.Add("TengTong", cmbTengTong.Text);
                AfterVisitList.Add("HongZhong", cmbHongZhong.Text);
                AfterVisitList.Add("SiZhiFeel", txtSiZhiFeel.Controls[0].Text);
                AfterVisitList.Add("Other", txtOther.Controls[0].Text);
                AfterVisitList.Add("YiSheng", txtYiSheng.Controls[0].Text);
                AfterVisitList.Add("VisitTime", dateTimePicker1.Value.ToString());

                DataTable dt = apro.GetAfterVisitCount2(PATID);
                int i=dt.Rows.Count;
                if (i==0)// 新增
                    result = apro.InsertAfterVisit2(AfterVisitList);
                else
                    result = apro.UpdateAfterVisit2(AfterVisitList);
                if (result > 0)
                    MessageBox.Show("保存成功！");
                else
                    MessageBox.Show("保存失败！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "保存出现异常，请检查！");
            }
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private bool ValiDateAfterVisit()
        {
            if (!ValidationRegex.ValidteData(txtXueYa.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtXueYa.Controls[0].Text))
            {
                MessageBox.Show("血压值填写有误，请检查！");
                txtXueYa.Focus();
                return false;
            }
            if (!ValidationRegex.ValidteData(txtXinLv.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtXinLv.Controls[0].Text))
            {
                MessageBox.Show("心率值填写有误，请检查！");
                txtXinLv.Focus();
                return false;
            }
            if (!ValidationRegex.ValidteData(txtHuXi.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtHuXi.Controls[0].Text))
            {
                MessageBox.Show("呼吸值填写有误，请检查！");
                txtHuXi.Focus();
                return false;
            }
               
          
            return true;
        }

        private void btnPrintView_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.0;
            
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = e.MarginBounds.X;
            int y = 200;
            int JG = 40;//行间距
            int width = e.MarginBounds.Width;
            int height = e.MarginBounds.Height;
            Font textfront = new Font(new FontFamily("宋体"), 11);
            Font tagfont = new Font(new FontFamily("宋体"), 16);
            string title = "嘉定区中医医院麻醉术后随访记录";
            e.Graphics.DrawString(title, tagfont, System.Drawing.Brushes.Black, 220, 100);
            
            //第一行
            e.Graphics.DrawString("姓名： " + txtPatname.Controls[0].Text, textfront, System.Drawing.Brushes.Black, x, y);
            e.Graphics.DrawLine(Pens.Black, x + 40, y + 20, x + 140, y + 20);
            e.Graphics.DrawString("床号： " + txtBednumber.Controls[0].Text, textfront, System.Drawing.Brushes.Black, x + 150, y);
            e.Graphics.DrawLine(Pens.Black, x + 190, y + 20, x + 290, y + 20);
            e.Graphics.DrawString("住院号： " + txtZhuyuanNo.Controls[0].Text, textfront, System.Drawing.Brushes.Black, x + 300, y);
            e.Graphics.DrawLine(Pens.Black, x + 360, y + 20, x + 600, y + 20);

            //第二行
            e.Graphics.DrawString("术后诊断：" + txtSQZD.Controls[0].Text, textfront, System.Drawing.Brushes.Black, x, y + JG);
            e.Graphics.DrawLine(Pens.Black, x + 70, y + 20 + JG, x + 290, y + 20 + JG);
            e.Graphics.DrawString("拟行手术： " + txtNXSS.Controls[0].Text, textfront, System.Drawing.Brushes.Black, x + 300, y + JG);
            e.Graphics.DrawLine(Pens.Black, x + 370, y + 20 + JG, x + 600, y + 20 + JG);

            //第三行
            e.Graphics.DrawString(label8.Text +" "+ txtXueYa.Controls[0].Text , textfront, System.Drawing.Brushes.Black, x, y + JG * 2);
            e.Graphics.DrawLine(Pens.Black, x + 130, y + 20 + JG * 2, x + 290, y + 20 + JG * 2);
            e.Graphics.DrawString("mmHg", textfront, System.Drawing.Brushes.Black, x+290, y + JG * 2);
            
            //第四行
            e.Graphics.DrawString(label5.Text + " " + txtXinLv.Controls[0].Text , textfront, System.Drawing.Brushes.Black, x, y + JG * 3);
            e.Graphics.DrawLine(Pens.Black, x + 60, y + 20 + JG * 3, x + 220, y + 20 + JG * 3);
            e.Graphics.DrawString("次/分", textfront, System.Drawing.Brushes.Black, x + 220, y + JG * 3);
            
            //第五行
            e.Graphics.DrawString(label6.Text + " " + txtHuXi.Controls[0].Text , textfront, System.Drawing.Brushes.Black, x, y + JG * 4);
            e.Graphics.DrawLine(Pens.Black, x + 135, y + 20 + JG * 4, x + 310, y + 20 + JG * 4);
            e.Graphics.DrawString("次/分", textfront, System.Drawing.Brushes.Black, x + 310, y + JG * 4);
            
            //第六行
            e.Graphics.DrawString(label10.Text + cmbYHTT.Text, textfront, System.Drawing.Brushes.Black, x, y + JG * 5);
            e.Graphics.DrawLine(Pens.Black, x + 100, y + 20 + JG * 5, x + 160, y + 20 + JG * 5);
            e.Graphics.DrawString(label11.Text + cmbSYSY.Text, textfront, System.Drawing.Brushes.Black, x+240, y + JG * 5);
            e.Graphics.DrawLine(Pens.Black, x + 310, y + 20 + JG * 5, x + 370, y + 20 + JG * 5);
            
            //第七行
            e.Graphics.DrawString(label13.Text + cmbEX.Text, textfront, System.Drawing.Brushes.Black, x, y + JG * 6);
            e.Graphics.DrawLine(Pens.Black, x + 160, y + 20 + JG * 6, x + 220, y + 20 + JG * 6);
            e.Graphics.DrawString(label12.Text + cmbOTu.Text, textfront, System.Drawing.Brushes.Black, x+240, y + JG * 6);
            e.Graphics.DrawLine(Pens.Black, x + 275, y + 20 + JG * 6, x + 335, y + 20 + JG * 6);
            
            //第八行
            e.Graphics.DrawString(label15.Text + cmbYShi.Text , textfront, System.Drawing.Brushes.Black, x, y + JG * 7);
            e.Graphics.DrawLine(Pens.Black, x + 160, y + 20 + JG * 7, x + 220, y + 20 + JG * 7);
            e.Graphics.DrawString(label14.Text + cmbTouTeng.Text, textfront, System.Drawing.Brushes.Black, x+240, y + JG * 7);
            e.Graphics.DrawLine(Pens.Black, x + 275, y + 20 + JG * 7, x + 335, y + 20 + JG * 7);
            //第九行
            e.Graphics.DrawString(label18.Text + txtSiZhiFeel.Controls[0].Text, textfront, System.Drawing.Brushes.Black, x+100, y + JG * 8);
            e.Graphics.DrawLine(Pens.Black, x + 220, y + 20 + JG * 8, x + 600, y + 20 + JG * 8);

            //第十行
            e.Graphics.DrawString(label19.Text + cmbNCN.Text , textfront, System.Drawing.Brushes.Black, x, y + JG * 9);
            e.Graphics.DrawLine(Pens.Black, x + 80, y + 20 + JG * 9, x + 140, y + 20 + JG * 9);
            //第十一行
            e.Graphics.DrawString(label20.Text + cmbTengTong.Text , textfront, System.Drawing.Brushes.Black, x, y + JG * 10);
            e.Graphics.DrawLine(Pens.Black, x + 140, y + 20 + JG * 10, x + 200, y + 20 + JG * 10);
            e.Graphics.DrawString(label16.Text + cmbHongZhong.Text, textfront, System.Drawing.Brushes.Black, x+210, y + JG * 10);
            e.Graphics.DrawLine(Pens.Black, x + 250, y + 20 + JG * 10, x + 310, y + 20 + JG * 10);
            //第十二行
            e.Graphics.DrawString(label17.Text + txtOther.Controls[0].Text , textfront, System.Drawing.Brushes.Black, x, y + JG * 11);
            e.Graphics.DrawLine(Pens.Black, x + 60, y + 20 + JG * 11, x + 600, y + 20 + JG * 11);
            //第十三行
            e.Graphics.DrawString("访视日期： " + dateTimePicker1.Text, textfront, System.Drawing.Brushes.Black, x + 150, y + 30 + JG * 12);
            e.Graphics.DrawLine(Pens.Black, x + 220, y + 50 + JG * 12, x + 345, y + 50 + JG * 12);
            e.Graphics.DrawString("医师： " + txtYiSheng.Controls[0].Text, textfront, System.Drawing.Brushes.Black, x+370, y +30+ JG * 12);
            e.Graphics.DrawLine(Pens.Black, x + 410, y + 50 + JG * 12, x + 600, y + 50 + JG * 12);

        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.0;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
        }

    }
}
