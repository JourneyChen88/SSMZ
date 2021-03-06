﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;
using adims_MODEL;
using System.Drawing.Printing;
using adims_DAL.Flows;

namespace main.Reports
{
    public partial class MazuiMonthQuery : Form
    {
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        string YF = "";//月份
        int YFpaixu = 0;

        public MazuiMonthQuery()
        {
            InitializeComponent();
        }

        private void mzyb_Tongji_Load(object sender, EventArgs e)
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            //初始化年第一天
            // dtStart.Text = year + "/1/1";
            //初始化月份第一天
            dtStart.Text = year + "/" + month + "/1";
            cmbYear.Text = dtStart.Value.Year.ToString();
            BindData();
        }
        /// <summary>
        /// 绑定DGV
        /// </summary>
        private void BindData()
        {
            DataTable dtTJ = dal.SelectYBBTJ(cmbYear.Text.Trim());
            dataGridView1.DataSource = dtTJ.DefaultView;
        }
        AnesthesiaSummaryDal _AnesthesiaSummaryDal = new AnesthesiaSummaryDal();
        private void btnOK_Click(object sender, EventArgs e)
        {
            Clear();//清空
            btnSave.Enabled = false;
            string dt1 = dtStart.Value.ToString("yyyy-MM-dd");
            string dt2 = dtEnd.Value.ToString("yyyy-MM-dd");
            tbZGNMZ.Text = _AnesthesiaSummaryDal.GetMzzjByCondition("ZGNMZ like '%1%'", dt1, dt2);
            tbSJMCC.Text = _AnesthesiaSummaryDal.GetMzzjByCondition("SJMCCZG like '%1%'", dt1, dt2);
            tbSJZZ.Text = _AnesthesiaSummaryDal.GetMzzjByCondition("SJZZ like '%1%'", dt1, dt2);
            tbCGQM.Text = _AnesthesiaSummaryDal.GetMzzjByCondition("QSMZ like '%1%' and QGCG like '%1%'", dt1, dt2);
            tbALL.Text = dal.GetAboutMZJLD("1=1", dt1, dt2);
            tbASA1.Text = dal.GetAboutMZJLD("asa='Ⅰ'", dt1, dt2);
            tbASA2.Text = dal.GetAboutMZJLD("asa='Ⅱ'", dt1, dt2);
            tbASA3.Text = dal.GetAboutMZJLD("asa='Ⅲ'", dt1, dt2);
            tbASA4.Text = dal.GetAboutMZJLD("asa='Ⅳ'", dt1, dt2);
            tbASA5.Text = dal.GetAboutMZJLD("asa='Ⅴ'", dt1, dt2);

            tbSHZT.Text = dal.GetAboutAfterVisit("ShuhouZT !='无' and shuhouZT !=''", dt1, dt2);

            tbPACU.Text = dal.GetAboutPacu("1=1", dt1, dt2);
            MessageBox.Show("统计完成");
        }
        public string dt1, dt2;
        private void listYueFen_DoubleClick(object sender, EventArgs e)
        {           
            int index = listYueFen.SelectedIndex;
            string theYear = cmbYear.Text.Trim();
            // DateTime.DaysInMonth(dt.Year, dt.Month)
            Clear();//清空
            btnSave.Enabled = true;
            if (index == 0)
            {
                dt1 = theYear + "-01-01";
                dt2 = theYear + "-01-31";
                YFpaixu = 1;
            }
            else if (index == 1)
            {
                dt1 = theYear + "-02-01";
                dt2 = theYear + "-02-31";
                YFpaixu = 2;
            }
            else if (index == 2)
            {
                dt1 = theYear + "-03-01";
                dt2 = theYear + "-03-31";
                YFpaixu = 3;
            }
            else if (index == 3)
            {
                dt1 = theYear + "-01-01";
                dt2 = theYear + "-03-31";
                YFpaixu = 4;
            }
            else if (index == 4)
            {
                dt1 = theYear + "-04-01";
                dt2 = theYear + "-04-31";
                YFpaixu = 5;
            }
            else if (index == 5)
            {
                dt1 = theYear + "-05-01";
                dt2 = theYear + "-05-31";
                YFpaixu = 6;
            }
            else if (index == 6)
            {
                dt1 = theYear + "-06-01";
                dt2 = theYear + "-06-31";
                YFpaixu = 7;
            }
            else if (index == 7)
            {
                dt1 = theYear + "-04-01";
                dt2 = theYear + "-06-31";
                YFpaixu = 8;
            }
            else if (index == 8)
            {
                dt1 = theYear + "-07-01";
                dt2 = theYear + "-07-31";
                YFpaixu = 9;
            }
            else if (index == 9)
            {
                dt1 = theYear + "-08-01";
                dt2 = theYear + "-08-31";
                YFpaixu = 10;
            }
            else if (index == 10)
            {
                dt1 = theYear + "-09-01";
                dt2 = theYear + "-09-31";
                YFpaixu = 11;
            }
            else if (index == 11)
            {
                dt1 = theYear + "-07-01";
                dt2 = theYear + "-09-31";
                YFpaixu = 12;
            }
            else if (index == 12)
            {
                dt1 = theYear + "-10-01";
                dt2 = theYear + "-10-31";
                YFpaixu = 13;
            }
            else if (index == 13)
            {
                dt1 = theYear + "-11-01";
                dt2 = theYear + "-11-31";
                YFpaixu = 14;
            }
            else if (index == 14)
            {
                dt1 = theYear + "-12-01";
                dt2 = theYear + "-12-31";
                YFpaixu = 15;
            }
            else if (index == 15)
            {
                dt1 = theYear + "-10-01";
                dt2 = theYear + "-12-31";
                YFpaixu = 16;
            }
            else if (index == 16)
            {
                dt1 = theYear + "-01-01";
                dt2 = theYear + "-12-31";
                YFpaixu = 17;
            }
            else return;
            YF = listYueFen.Text;
            tbZGNMZ.Text = _AnesthesiaSummaryDal.GetMzzjByCondition("ZGNMZ like '%1%'", dt1, dt2);//椎管内麻醉
            tbSJMCC.Text = _AnesthesiaSummaryDal.GetMzzjByCondition("SJMCCZG like '%1%'", dt1, dt2);//深静脉穿刺
            tbSJZZ.Text = _AnesthesiaSummaryDal.GetMzzjByCondition("SJZZ like '%1%'", dt1, dt2);//神经阻滞
            tbCGQM.Text = _AnesthesiaSummaryDal.GetMzzjByCondition("QSMZ like '%1%' and QGCG like '%1%'", dt1, dt2);//插管全麻
            tbALL.Text = dal.GetAboutMZJLD("1=1", dt1, dt2);//工作总量
            tbASA1.Text = dal.GetAboutMZJLD("asa='Ⅰ'", dt1, dt2);//一级
            tbASA2.Text = dal.GetAboutMZJLD("asa='Ⅱ'", dt1, dt2);//二级
            tbASA3.Text = dal.GetAboutMZJLD("asa='Ⅲ'", dt1, dt2);//三级
            tbASA4.Text = dal.GetAboutMZJLD("asa='Ⅳ'", dt1, dt2);//四级
            tbASA5.Text = dal.GetAboutMZJLD("asa='Ⅴ'", dt1, dt2);//五级
            tbSHZT.Text = dal.GetAboutAfterVisit("ShuhouZT !='无' and shuhouZT !=''", dt1, dt2);//术后镇痛
            tbPACU.Text = dal.GetAboutPacu("1=1", dt1, dt2);//麻醉复苏
            MessageBox.Show("统计完成");
        }
        
        ///// <summary>
        ///// 判断是否是数字
        ///// </summary>
        ///// <returns></returns>
        //public  bool BoolInt() 
        //{
        //    bool nums = false;
        //    try
        //    {            
        //    if ( Convert.ToInt32(tbALL.Text)>=0||tbALL.Text.Equals(""))
        //    {
        //        nums = true;
        //    }
        //    }
        //    catch (Exception)
        //    {             
        //        MessageBox.Show("请输入数字！");
        //        nums = false;
        //    }
        //    return nums;
        //}
        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            tbALL.Text = "";
            tbCGQM.Text = "";
            tbCGQMTWXH.Text = "";
            tbZGNMZ.Text = "";
            tbSJZZ.Text = "";
            tbWTRL.Text = "";
            tbWTWJ.Text = "";
            tbSJMCC.Text = "";
            tbXFFS.Text = "";
            tbPACU.Text = "";
            tbBFZ.Text = "";
            tbSteward4.Text = "";
            tbSHZT.Text = "";
            tbQJCG.Text = "";
            tbmzfyq.Text = "";
            tbysza.Text = "";
            tbybhd.Text = "";
            tbqmjscx.Text = "";
            tbhxdza.Text = "";
            tbmzywsw.Text = "";
            tbfyqxgsj.Text = "";
            tbASA1.Text = "";
            tbASA2.Text = "";
            tbASA3.Text = "";
            tbASA4.Text = "";
            tbASA5.Text = "";
            tbASAN1.Text = "";
            tbASAN2.Text = "";
            tbASAN3.Text = "";
            tbASAN4.Text = "";
            tbASAN5.Text = "";
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void SaveTJ()
        {
            try
            {
                if (!listYueFen.Text.Trim().Equals(""))
                {
                    //if ( BoolInt() )
                    //{                       
                   
                    int result = 0;
                   
                    Dictionary<string, string> ybbList = new Dictionary<string, string>();
                    ybbList.Add("tj_date", cmbYear.Text.Trim());
                    ybbList.Add("tj_YF", YF);
                    ybbList.Add("ygzl", tbALL.Text.Trim());
                    ybbList.Add("cgqm", tbCGQM.Text.Trim());
                    ybbList.Add("twxh", tbCGQMTWXH.Text.Trim());
                    ybbList.Add("zgnmz", tbZGNMZ.Text.Trim());
                    ybbList.Add("sjzd", tbSJZZ.Text.Trim());
                    ybbList.Add("wtrl", tbWTRL.Text.Trim());
                    ybbList.Add("wtwj", tbWTWJ.Text.Trim());
                    ybbList.Add("sjmcc", tbSJMCC.Text.Trim());
                    ybbList.Add("xffs", tbXFFS.Text.Trim());
                    ybbList.Add("mzfs", tbPACU.Text.Trim());
                    ybbList.Add("yzbfz", tbBFZ.Text.Trim());
                    ybbList.Add("Steward", tbSteward4.Text.Trim());
                    ybbList.Add("shzt", tbSHZT.Text.Trim());
                    ybbList.Add("ssswqj", tbQJCG.Text.Trim());
                    ybbList.Add("mzfyq", tbmzfyq.Text.Trim());
                    ybbList.Add("ysza", tbysza.Text.Trim());
                    ybbList.Add("ybhd", tbybhd.Text.Trim());
                    ybbList.Add("qmjscx", tbqmjscx.Text.Trim());
                    ybbList.Add("hxdza", tbhxdza.Text.Trim());
                    ybbList.Add("mzywsw", tbmzywsw.Text.Trim());
                    ybbList.Add("fyqxgsj", tbfyqxgsj.Text.Trim());
                    ybbList.Add("ASAY1", tbASA1.Text.Trim());
                    ybbList.Add("ASAN1", tbASAN1.Text.Trim());
                    ybbList.Add("ASAY2", tbASA2.Text.Trim());
                    ybbList.Add("ASAN2", tbASAN2.Text.Trim());
                    ybbList.Add("ASAY3", tbASA3.Text.Trim());
                    ybbList.Add("ASAN3", tbASAN3.Text.Trim());
                    ybbList.Add("ASAY4", tbASA4.Text.Trim());
                    ybbList.Add("ASAN4", tbASAN4.Text.Trim());
                    ybbList.Add("ASAY5", tbASA5.Text.Trim());
                    ybbList.Add("ASAN5", tbASAN5.Text.Trim());
                    ybbList.Add("YFpaixu", YFpaixu.ToString());
                    DataTable dt = dal.SelectYBBTJ(YF, cmbYear.Text.Trim());
                    if (dt.Rows.Count == 0)
                        result = dal.InsertYBBTJ(ybbList, cmbYear.Text.Trim());//添加
                    else
                        result = dal.UpdateYBBTJ(ybbList, YF, cmbYear.Text.Trim());//更新
                    if (result > 0)
                    {
                        MessageBox.Show("保存成功");
                        BindData();
                    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("请输入数字");
                    //}
                }
                else
                {
                    MessageBox.Show("请选择要统计的月份");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("保存出现异常，请检查！");
            }
        }
        /// <summary>
        /// 响应保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveTJ();
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintView_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 0)
            {
                this.printPreviewDialog1.Document = printDocument1;
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                    printDocument1.Print();
            }

        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            //printDocument1.DefaultPageSettings.PaperSize = 
            //    new PaperSize("16K", 737, 1020);
            printDocument1.DefaultPageSettings.PaperSize =
               new PaperSize("A4", 1160, 820);

        }
        int count=0;
        int num = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = 40, y = 30;
            Font zt12 = new Font(new FontFamily("黑体"), 14);
            Font zt9 = new Font(new FontFamily("宋体"), 9);
            Pen ptp = new Pen(Brushes.Black);
            string data = cmbYear.Text.Trim();//获取年份
            if (num>0)
            {
                e.HasMorePages = false;
                num = 0;
                count = 0;
                x = 40; y = 30;
                y = y + 40; 
                e.Graphics.DrawLine(ptp, x, y, 1120, y);//画横线

                ////画统计标题栏
                e.Graphics.DrawLine(Pens.Black, new Point(x, y), new Point(x, y + 60));
                e.Graphics.DrawString("月份或\n季度", zt9, Brushes.Black, new Point(x + 1, y + 3));
                e.Graphics.DrawLine(Pens.Black, new Point(x + 50, y), new Point(x + 50, y + 60));
                e.Graphics.DrawString("麻醉中出现\n氧饱和度重\n度降低例数", zt9, Brushes.Black, new Point(x + 55, y + 3));
                e.Graphics.DrawLine(Pens.Black, new Point(x + 130, y), new Point(x + 130, y + 60));
                e.Graphics.DrawString("全身麻醉结\n束时使用催\n醒药物例数", zt9, Brushes.Black, new Point(x + 135, y + 3));
                e.Graphics.DrawLine(Pens.Black, new Point(x + 210, y), new Point(x + 210, y + 60));
                e.Graphics.DrawString("麻醉中因误咽\n误吸引发呼吸\n道梗阻例数", zt9, Brushes.Black, new Point(x + 215, y + 3));
                e.Graphics.DrawLine(Pens.Black, new Point(x + 300, y), new Point(x + 300, y + 60));
                e.Graphics.DrawString("麻醉意\n外死亡\n例数", zt9, Brushes.Black, new Point(x + 305, y + 3));
                e.Graphics.DrawLine(Pens.Black, new Point(x + 360, y), new Point(x + 360, y + 60));
                e.Graphics.DrawString("其他非预\n期的相关\n事件例数", zt9, Brushes.Black, new Point(x + 365, y + 3));
                e.Graphics.DrawLine(Pens.Black, new Point(x + 430, y), new Point(x + 430, y + 60));
                e.Graphics.DrawString("ASA 分\n级Ⅰ级\n例数", zt9, Brushes.Black, new Point(x + 435, y + 3));
                e.Graphics.DrawLine(Pens.Black, new Point(x + 490, y), new Point(x + 490, y + 60));
                e.Graphics.DrawString("ASA - Ⅰ\n级术后死\n亡例数", zt9, Brushes.Black, new Point(x + 495, y + 3));
                e.Graphics.DrawLine(Pens.Black, new Point(x + 560, y), new Point(x + 560, y + 60));
                e.Graphics.DrawString("ASA 分\n级Ⅱ级\n例数", zt9, Brushes.Black, new Point(x + 565, y + 3));
                e.Graphics.DrawLine(Pens.Black, new Point(x + 620, y), new Point(x + 620, y + 60));
                e.Graphics.DrawString("ASA - Ⅱ\n级术后死\n亡例数", zt9, Brushes.Black, new Point(x + 625, y + 3));
                e.Graphics.DrawLine(Pens.Black, new Point(x + 690, y), new Point(x + 690, y + 60));
                e.Graphics.DrawString("ASA 分\n级Ⅲ级\n例数", zt9, Brushes.Black, new Point(x + 695, y + 3));
                e.Graphics.DrawLine(Pens.Black, new Point(x + 750, y), new Point(x + 750, y + 60));
                e.Graphics.DrawString("ASA - Ⅲ\n级术后死\n亡例数", zt9, Brushes.Black, new Point(x + 755, y + 3));
                e.Graphics.DrawLine(Pens.Black, new Point(x + 820, y), new Point(x + 820, y + 60));
                e.Graphics.DrawString("ASA 分\n级Ⅳ级\n例数", zt9, Brushes.Black, new Point(x + 825, y + 3));
                e.Graphics.DrawLine(Pens.Black, new Point(x + 880, y), new Point(x + 880, y + 60));
                e.Graphics.DrawString("ASA - Ⅳ\n级术后死\n亡例数", zt9, Brushes.Black, new Point(x + 885, y + 3));
                e.Graphics.DrawLine(Pens.Black, new Point(x + 950, y), new Point(x + 950, y + 60));
                e.Graphics.DrawString("ASA 分\n级Ⅴ级\n例数", zt9, Brushes.Black, new Point(x + 955, y + 3));
                e.Graphics.DrawLine(Pens.Black, new Point(x + 1010, y), new Point(x + 1010, y + 60));
                e.Graphics.DrawString("ASA - Ⅴ\n级术后死\n亡例数", zt9, Brushes.Black, new Point(x + 1015, y + 3));
                e.Graphics.DrawLine(Pens.Black, new Point(x + 1080, y), new Point(x + 1080, y + 60));

                e.Graphics.DrawLine(Pens.Black, new Point(x, y + 60), new Point(1120, y + 60));//画底部截止线
                y = y + 60;
                for (; count < dataGridView1.Rows.Count; count++)
                {
                    e.Graphics.DrawLine(Pens.Black, new Point(x, y), new Point(x, y + 40));
                    e.Graphics.DrawString(dataGridView1[2, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 5, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 50, y), new Point(x + 50, y + 40));
                    e.Graphics.DrawString(dataGridView1[19, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 71, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 130, y), new Point(x + 130, y + 40));
                    e.Graphics.DrawString(dataGridView1[20, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 151, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 210, y), new Point(x + 210, y + 40));
                    e.Graphics.DrawString(dataGridView1[21, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 231, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 300, y), new Point(x + 300, y + 40));
                    e.Graphics.DrawString(dataGridView1[22, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 321, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 360, y), new Point(x + 360, y + 40));
                    e.Graphics.DrawString(dataGridView1[23, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 381, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 430, y), new Point(x + 430, y + 40));
                    e.Graphics.DrawString(dataGridView1[24, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 451, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 490, y), new Point(x + 490, y + 40));
                    e.Graphics.DrawString(dataGridView1[25, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 511, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 560, y), new Point(x + 560, y + 40));
                    e.Graphics.DrawString(dataGridView1[26, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 581, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 620, y), new Point(x + 620, y + 40));
                    e.Graphics.DrawString(dataGridView1[27, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 641, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 690, y), new Point(x + 690, y + 40));
                    e.Graphics.DrawString(dataGridView1[28, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 711, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 750, y), new Point(x + 750, y + 40));
                    e.Graphics.DrawString(dataGridView1[29, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 771, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 820, y), new Point(x + 820, y + 40));
                    e.Graphics.DrawString(dataGridView1[30, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 841, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 880, y), new Point(x + 880, y + 40));
                    e.Graphics.DrawString(dataGridView1[31, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 901, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 950, y), new Point(x + 950, y + 40));
                    e.Graphics.DrawString(dataGridView1[32, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 971, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 1010, y), new Point(x + 1010, y + 40));
                    e.Graphics.DrawString(dataGridView1[33, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 1021, y + 15));
                    //e.Graphics.DrawLine(Pens.Black, new Point(x + 1000, y), new Point(x + 1000, y + 40));
                    //e.Graphics.DrawString(dataGridView1[18, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 1021, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 1080, y), new Point(x + 1080, y + 40));

                    e.Graphics.DrawLine(Pens.Black, new Point(x, y + 40), new Point(1120, y + 40));//画底部截止线
                    y = y + 40;
               
            }
                 return;
            }
                x = 40; y = 30;
            e.Graphics.DrawString("麻醉质量管理登记本（"+data+"年）", zt12, Brushes.Black,450, y);
            x = 40; y = y+40;
            e.Graphics.DrawLine(ptp, x, y, 1120, y);//画横线

            ////画统计标题栏
            e.Graphics.DrawLine(Pens.Black, new Point(x, y), new Point(x, y + 60));
            e.Graphics.DrawString("月份或\n季度", zt9, Brushes.Black, new Point(x + 1, y + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x + 50, y), new Point(x + 50, y + 60));
            e.Graphics.DrawString("月工作\n量（例）", zt9, Brushes.Black, new Point(x + 55, y + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x + 110, y), new Point(x + 110, y + 60));
            e.Graphics.DrawString("插管全\n麻（例）", zt9, Brushes.Black, new Point(x + 115, y + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x + 170, y), new Point(x + 170, y + 60));
            e.Graphics.DrawString("插管全麻\n中体外循\n环（例）", zt9, Brushes.Black, new Point(x + 175, y + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x + 240, y), new Point(x + 240, y + 60));
            e.Graphics.DrawString("椎管内\n麻　醉\n（例）", zt9, Brushes.Black, new Point(x + 245, y + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x + 300, y), new Point(x + 300, y + 60));
            e.Graphics.DrawString("神经阻\n滞（例）", zt9, Brushes.Black, new Point(x + 305, y + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x + 360, y), new Point(x + 360, y + 60));
            e.Graphics.DrawString("无痛人\n流（例）", zt9, Brushes.Black, new Point(x + 365, y + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x + 420, y), new Point(x + 420, y + 60));
            e.Graphics.DrawString("无痛胃\n镜（例）", zt9, Brushes.Black, new Point(x + 425, y + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x + 480, y), new Point(x + 480, y + 60));
            e.Graphics.DrawString("深静脉\n穿  刺\n（例）", zt9, Brushes.Black, new Point(x + 485, y + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x + 540, y), new Point(x + 540, y + 60));
            e.Graphics.DrawString("心肺复\n苏（例）", zt9, Brushes.Black, new Point(x + 545, y + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x + 600, y), new Point(x + 600, y + 60));
            e.Graphics.DrawString("麻醉复\n苏（例）", zt9, Brushes.Black, new Point(x + 601, y + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x + 660, y), new Point(x + 660, y + 60));
            e.Graphics.DrawString("严重麻\n醉并发\n症（例）", zt9, Brushes.Black, new Point(x + 665, y + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x + 720, y), new Point(x + 720, y + 60));
            e.Graphics.DrawString("Steward\n>4分\n （例）", zt9, Brushes.Black, new Point(x + 725, y + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x + 780, y), new Point(x + 780, y + 60));
            e.Graphics.DrawString("术后镇痛\n （例）", zt9, Brushes.Black, new Point(x + 785, y + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x + 850, y), new Point(x + 850, y + 60));
            e.Graphics.DrawString("手术室外\n插管抢救", zt9, Brushes.Black, new Point(x + 855, y + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x + 920, y), new Point(x + 920, y + 60));
            e.Graphics.DrawString("麻醉非预期\n期的相关\n事件例数", zt9, Brushes.Black, new Point(x + 925, y + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x + 1000, y), new Point(x + 1000, y + 60));
            e.Graphics.DrawString("麻醉中发生\n未预期的意\n识障碍例数", zt9, Brushes.Black, new Point(x + 1005, y + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x + 1080, y), new Point(x + 1080, y + 60));
            e.Graphics.DrawLine(Pens.Black, new Point(x, y + 60), new Point(1120, y + 60));//画底部截止线
            y = y + 60;
            for (; count < dataGridView1.Rows.Count; count++)
            {               
                    e.Graphics.DrawLine(Pens.Black, new Point(x, y), new Point(x, y + 40));
                    e.Graphics.DrawString(dataGridView1[2, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 5, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x+50, y), new Point(x+50, y + 40));
                    e.Graphics.DrawString(dataGridView1[3, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 71, y+ 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 110, y), new Point(x + 110, y + 40));
                    e.Graphics.DrawString(dataGridView1[4, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 131, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 170, y), new Point(x + 170, y + 40));
                    e.Graphics.DrawString(dataGridView1[5, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 191, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 240, y), new Point(x + 240, y + 40));
                    e.Graphics.DrawString(dataGridView1[6, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 261, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 300, y), new Point(x + 300, y + 40));
                    e.Graphics.DrawString(dataGridView1[7, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 321, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 360, y), new Point(x + 360, y + 40));
                    e.Graphics.DrawString(dataGridView1[8, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 381, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 420, y), new Point(x + 420, y + 40));
                    e.Graphics.DrawString(dataGridView1[9, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 441, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 480, y), new Point(x + 480, y + 40));
                    e.Graphics.DrawString(dataGridView1[10, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 561, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 540, y), new Point(x + 540, y + 40));
                    e.Graphics.DrawString(dataGridView1[11, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 561, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 600, y), new Point(x + 600, y + 40));
                    e.Graphics.DrawString(dataGridView1[12, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 621, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 660, y), new Point(x + 660, y + 40));
                    e.Graphics.DrawString(dataGridView1[13, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 681, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 720, y), new Point(x + 720, y + 40));
                    e.Graphics.DrawString(dataGridView1[14, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 741, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 780, y), new Point(x + 780, y + 40));
                    e.Graphics.DrawString(dataGridView1[15, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 801, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 850, y), new Point(x + 850, y + 40));
                    e.Graphics.DrawString(dataGridView1[16, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 871, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 920, y), new Point(x + 920, y + 40));
                    e.Graphics.DrawString(dataGridView1[17, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 941, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 1000, y), new Point(x + 1000, y + 40));
                    e.Graphics.DrawString(dataGridView1[18, count].Value.ToString(), zt9, Brushes.Black, new Point(x + 1021, y + 15));
                    e.Graphics.DrawLine(Pens.Black, new Point(x + 1080, y), new Point(x + 1080, y + 40));

                e.Graphics.DrawLine(Pens.Black, new Point(x, y + 40), new Point(1120, y + 40));//画底部截止线
                y = y + 40;
            }
            if (true)
            {
                e.HasMorePages = true;                     

            }
            num++;
            
        }
        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        /// <summary>
        /// 年份发生变化时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>  
        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }


    }
}
