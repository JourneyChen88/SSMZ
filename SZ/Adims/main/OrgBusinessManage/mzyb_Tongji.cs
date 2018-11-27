using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;

namespace main.OrgBusinessManage
{
    public partial class mzyb_Tongji : Form
    {
        admin_T_SQL at = new admin_T_SQL();
        AdimsProvider apro = new AdimsProvider();
        public mzyb_Tongji()
        {
            InitializeComponent();
        }

        private void mzyb_Tongji_Load(object sender, EventArgs e)
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            //初始化年第一天
            dtstart.Text = year + "/1/1";
            //初始化月份第一天
            dtstart.Text = year + "/" + month + "/1";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string date1 = dtstart.Value.Date.ToString("yyyy-MM-dd ");
            string date2 = dtEnd.Value.Date.ToString("yyyy-MM-dd ");
            #region 手术等级统计
            object num1 = at.MZ_Y_TJ_I(date1, date2);
            tb1.Text = num1.ToString();

            object num2 = at.MZ_Y_TJ_Ⅱ(date1, date2);
            tb2.Text = num2.ToString();

            object num3 = at.MZ_Y_TJ_Ⅲ(date1, date2);
            tb3.Text = num3.ToString();

            object num4 = at.MZ_Y_TJ_Ⅳ(date1, date2);
            tb4.Text = num4.ToString();

            object num5 = at.MZ_Y_TJ_Ⅴ(date1, date2);
            tb5.Text = num5.ToString();

            object num6 = at.MZ_Y_TJ_E(date1, date2);
            tbE.Text = num6.ToString();

            object num7 = at.MZ_Y_TJ_RR(date1, date2);
            tbRR.Text = num7.ToString();

            object lnmz = at.LaoNianMazui(date1, date2);
            tbLNMZ.Text = lnmz.ToString();

            object semz = at.ShaoErMazui(date1, date2);
            tbSEMZ.Text = semz.ToString();

            object ckmz = at.ChankeMazui(date1, date2);
            tbCKMZ.Text = ckmz.ToString();

            tbDMCC.Text = at.DongMaiCC(date1, date2).ToString();

            tbSJMCC.Text = at.ShenJingMaiCC(date1, date2).ToString();

            #endregion
            #region 手术室内麻醉统计
            object MZ_ALL = at.MZ_Y_TJ_ALL(date1, date2);
            this.tbALL.Text = MZ_ALL.ToString();
            //全麻
            object qm_TJ = at.MZ_Y_QM_TJ(date1, date2);
            string qm = qm_TJ.ToString();
            if (qm != "0")
            {
                if (tbALL.Text.Trim() != "0")
                {
                    this.tbqmbl.Text = ((float.Parse(qm) / float.Parse(this.tbALL.Text)) * 100).ToString("0.00") + "%";
                }
            }
            else
            {
                this.tbqmbl.Text = "0.00" + "%";
            }

            //吸入
            object XR_TJ = at.MZ_Y_XRMZ_TJ(date1, date2);
            this.tbxrmz.Text = XR_TJ.ToString();
            //静脉
            object JM_TJ = at.MZ_Y_JMMZ_TJ(date1, date2);
            this.tbJmmz.Text = JM_TJ.ToString();
            //静吸复合
            object JXFHMZ_TJ = at.MZ_Y_JXFHMZ_TJ(date1, date2);
            this.tbJxfhmz.Text = JXFHMZ_TJ.ToString();
            //联合
            object LHMZ_TJ = at.MZ_Y_LHMZ_TJ(date1, date2);
            this.tbLhmz.Text = LHMZ_TJ.ToString();
            //腰麻
            object YMZ_TJ = at.MZ_Y_YMZ_TJ(date1, date2);
            this.tbYM.Text = YMZ_TJ.ToString();
            //硬膜外
            object YMWMZ_TJ = at.MZ_Y_YMWMZ_TJ(date1, date2);
            this.tbYMW.Text = YMWMZ_TJ.ToString();
            //骶骨
            object DGMZ_TJ = at.MZ_Y_DGMZ_TJ(date1, date2);
            this.tbDG.Text = DGMZ_TJ.ToString();
            //腰硬联合
            object YYLH_TJ = at.MZ_Y_YYLHMZ_TJ(date1, date2);
            this.tbYYLH.Text = YYLH_TJ.ToString();
            //半身麻醉
            object BSMZ_TJ = at.MZ_Y_BSMZ_TJ(date1, date2);
            this.tbBSMZ.Text = BSMZ_TJ.ToString();
            //臂丛神经阻滞
            object BCSJZZ_TJ = at.MZ_Y_BCSJZZMZ_TJ(date1, date2);
            this.tbBCSJZZ.Text = BCSJZZ_TJ.ToString();
            //局部浸润
            object JBMZ_TJ = at.MZ_Y_JBJRMZ_TJ(date1, date2);
            this.tbJBQR.Text = JBMZ_TJ.ToString();

            //其他
            object QTMZ_TJ = at.MZ_Y_QTMZ_TJ(date1, date2);
            this.tbOther.Text = QTMZ_TJ.ToString();
            #endregion
            #region 专科麻醉
            // 耳鼻喉科
            tbEBHK.Text = at.MZYBTJ_EBHK(date1, date2).ToString();

            // 泌尿外科
            tbMNWK.Text = at.MZYBTJ_MNWK(date1, date2).ToString();

            // 肛肠外科
            tbGCWK.Text = at.MZYBTJ_GCWK(date1, date2).ToString();

            // 甲乳外科
            tbJRWK.Text = at.MZYBTJ_JRWK(date1, date2).ToString();

            // 神经外科
            tbSJWK.Text = at.MZYBTJ_SJWK(date1, date2).ToString();

            // 整形美容烧伤科
            tbMRSSK.Text = at.MZYBTJ_ZXMRSHWK(date1, date2).ToString();

            // 普外科一
            tbP1.Text = at.MZYBTJ_P1(date1, date2).ToString();

            // 普外科二
            tbP2.Text = at.MZYBTJ_P2(date1, date2).ToString();

            // 普外科三
            tbP3.Text = at.MZYBTJ_P3(date1, date2).ToString();

            // 骨科
            tbGK.Text = at.MZYBTJ_GK(date1, date2).ToString();

            // 妇幼妇产科
            tbFYFCK.Text = at.MZYBTJ_FYFCK(date1, date2).ToString();

            // 消化内科
            tbXHNK.Text = at.MZYBTJ_XHNK(date1, date2).ToString();

            // 胸心外科
            tbXXWK.Text = at.MZYBTJ_XXWK(date1, date2).ToString();

            #endregion
            #region 硬膜外阻滞成功率
            //成功数
            object zzcgs = at.YMWZZCGL_TJ(date1, date2);
            this.tbZzcgs.Text = zzcgs.ToString();
            //总数
            object zzzs = at.YMWZZ_TJ(date1, date2);
            this.tbYMWZS1.Text = zzzs.ToString();
            this.tbZZcgl.Text = "0.00" + "%";
            if (tbZzcgs.Text.Trim() != "0")
            {
                if (tbYMWZS1.Text.Trim() != "0")
                {
                    //成功率
                    this.tbZZcgl.Text = (((float.Parse(tbYMWZS1.Text) / float.Parse(tbZzcgs.Text)) * 100)).ToString("0.00") + "%";
                }
            }
            #endregion
            #region 术前检查评估准备率
            //总数
            object SQPG_TJ = at.SQFSZS_Y_TJ(date1, date2);
            this.tbSQPGzs.Text = SQPG_TJ.ToString();
            //例数
            object SQPGLS_TJ = at.SQFSZBS_Y_TJ(date1, date2);
            this.tbSQPGls.Text = SQPGLS_TJ.ToString();
            //比例
            if (tbSQPGls.Text.Trim() != "0")
            {
                if (tbSQPGzs.Text.Trim() != "0")
                {
                    this.tbSQPGzbl.Text = (((float.Parse(tbSQPGls.Text) / float.Parse(tbSQPGzs.Text)) * 100)).ToString("0.00") + "%";
                }
            }
            else
            {
                this.tbSQPGzbl.Text = "0.00" + "%";
            }
            #endregion
            #region 术后访视率
            //总数
            object SHFSZS_TJ = at.SHFSZS_Y_TJ(date1, date2);
            this.tbSHSFzs.Text = SHFSZS_TJ.ToString();
            //列数
            object SHFSLS_TJ = at.SHFSLS_Y_TJ(date1, date2);
            this.tbSHSFls.Text = SHFSLS_TJ.ToString();
            //比例
            if (tbSHSFls.Text.Trim() != "0")
            {
                if (tbSHSFzs.Text.Trim() != "0")
                {
                    this.tbSHSFfsl.Text = (((float.Parse(tbSHSFls.Text) / float.Parse(tbSHSFzs.Text)) * 100)).ToString("0.00") + "%";
                }
            }
            else
            {
                this.tbSHSFfsl.Text = "0.00" + "%";
            }
            #endregion
            #region 全麻术中知晓率
            //总数
            object QMZS_TJ = at.QMZSZS_Y_TJ(date1, date2);
            this.tbQMZXzs.Text = QMZS_TJ.ToString();
            //知情列数
            object QMZQLS_TJ = at.QMZSLS_Y_TJ(date1, date2);
            this.tbQMZXls.Text = QMZQLS_TJ.ToString();
            //比例

            if (tbQMZXzs.Text.Trim() != "0")
            {
                if (tbQMZXls.Text.Trim() != "0")
                {
                    this.tbQMZXzxl.Text = (((float.Parse(tbQMZXls.Text) / float.Parse(tbQMZXzs.Text)) * 100)).ToString("0.00") + "%";
                }
            }
            else
            {
                this.tbQMZXzxl.Text = "0.00" + "%";
            }
            #endregion
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("K16", 780, 1080);
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = 30, y = 0;//整体位置控制
            int PerRow = 20;
            string title = "江苏盛泽医院、江苏省人民医院盛泽分院";//标题  
            string title1 = "麻醉记录统计单";//标题  
            Pen ptp = Pens.Black;//普通画笔
            Pen pblack2 = new Pen(Brushes.Black, 2);
            Pen pxuxian = new Pen(Brushes.Black);
            pxuxian.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Font ptzt1 = new System.Drawing.Font("宋体", 14);//标题
            Font ptzt2 = new System.Drawing.Font("宋体", 13);//标题   
            Font ptzt3 = new Font("宋体", 8);//填入栏目字体
            Font ptzt4 = new System.Drawing.Font("宋体", 9);
            Pen pred2 = new Pen(Brushes.Red, 2);
            e.Graphics.DrawString(title, ptzt1, Brushes.Black, new Point(190 + x, 20 + y));
            e.Graphics.DrawString(title1, ptzt2, Brushes.Black, new Point(300 + x, 45 + y));
            Pen blackPen = new Pen(Color.Black, 1);
            y = y + 80;
            //e.Graphics.DrawLine(blackPen, x, y, x + 700, y);
            Rectangle rect = new Rectangle(x + 0, y, 700, 20);
            e.Graphics.DrawRectangle(blackPen, rect);
            e.Graphics.DrawString("  一、手术台及床位设置（台/张）", ptzt4, Brushes.Black, new Point(x, y + 3));
            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x, y, 300, PerRow));
            e.Graphics.DrawString("手术台数（台)", ptzt4, Brushes.Black, new Point(x + 50, y + 3));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 300, y, 400, PerRow));
            e.Graphics.DrawString("床位数（张）", ptzt4, Brushes.Black, new Point(x + 460, y + 3));
            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 150, PerRow));
            e.Graphics.DrawString("手术室内手术台数", ptzt3, Brushes.Black, x + 10, y + 3);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 150, y, 150, PerRow));
            e.Graphics.DrawString("手术室外手术台数", ptzt3, Brushes.Black, x + 180, y + 3);

            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 300, y, 130, PerRow * 2));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 430, y, 130, PerRow * 2));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 560, y, 140, PerRow * 2));
            e.Graphics.DrawString("麻醉恢复室\n   (RR)", ptzt3, Brushes.Black, x + 350, y + 10);
            e.Graphics.DrawString("麻醉科ICU\n  AICU)", ptzt3, Brushes.Black, x + 480, y + 10);
            e.Graphics.DrawString("疼痛病房", ptzt3, Brushes.Black, x + 610, y + 10);
            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 75, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 75, y, 75, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 150, y, 75, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 225, y, 75, PerRow));
            e.Graphics.DrawString(" 设置数", ptzt3, Brushes.Black, x + 10, y + 3);
            e.Graphics.DrawString(" 开放数", ptzt3, Brushes.Black, x + 80, y + 3);
            e.Graphics.DrawString(" 设置数", ptzt3, Brushes.Black, x + 155, y + 3);
            e.Graphics.DrawString(" 开放数", ptzt3, Brushes.Black, x + 230, y + 3);

            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 75, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 75, y, 75, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 150, y, 75, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 225, y, 75, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 300, y, 130, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 430, y, 130, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 560, y, 140, PerRow));

            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 700, PerRow));
            e.Graphics.DrawString("二、手术室内麻醉数（例数）", ptzt4, Brushes.Black, x + 10, y + 3);

            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 700, PerRow));
            e.Graphics.DrawString("ASA分级手术病人数", ptzt4, Brushes.Black, x + 10, y + 3);
            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 70, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 140, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 210, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 280, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 350, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 420, y, 280, PerRow));
            e.Graphics.DrawString("麻醉总数", ptzt3, Brushes.Black, x + 10, y + 3);
            e.Graphics.DrawString("   I ", ptzt3, Brushes.Black, x + 90, y + 3);
            e.Graphics.DrawString("   II ", ptzt3, Brushes.Black, x + 160, y + 3);
            e.Graphics.DrawString("   III ", ptzt3, Brushes.Black, x + 230, y + 3);
            e.Graphics.DrawString("   IV", ptzt3, Brushes.Black, x + 300, y + 3);
            e.Graphics.DrawString("   V  ", ptzt3, Brushes.Black, x + 370, y + 3);
            e.Graphics.DrawString("   E(急诊) ", ptzt3, Brushes.Black, x + 460, y + 3);

            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 70, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 140, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 210, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 280, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 350, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 420, y, 280, PerRow));

            //数据
            e.Graphics.DrawString(tbALL.Text, ptzt3, Brushes.Black, x + 10, y + 3);
            e.Graphics.DrawString(tb1.Text, ptzt3, Brushes.Black, x + 80, y + 3);
            e.Graphics.DrawString(tb2.Text, ptzt3, Brushes.Black, x + 150, y + 3);
            e.Graphics.DrawString(tb3.Text, ptzt3, Brushes.Black, x + 220, y + 3);
            e.Graphics.DrawString(tb4.Text, ptzt3, Brushes.Black, x + 290, y + 3);
            e.Graphics.DrawString(tb5.Text, ptzt3, Brushes.Black, x + 360, y + 3);
            e.Graphics.DrawString(tbE.Text, ptzt3, Brushes.Black, x + 430, y + 3);
            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 700, PerRow));
            e.Graphics.DrawString("麻醉分类数", ptzt4, Brushes.Black, x + 10, y + 3);
            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 350, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 350, y, 290, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 640, y, 60, PerRow * 2));
            e.Graphics.DrawString("  全身麻醉数", ptzt3, Brushes.Black, x + 50, y + 3);
            e.Graphics.DrawString(" 椎管内麻醉数", ptzt3, Brushes.Black, x + 380, y + 3);
            e.Graphics.DrawString("其他\n麻醉数", ptzt3, Brushes.Black, x + 650, y + 3);
            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 70, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 140, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 210, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 280, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 350, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 420, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 490, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 560, y, 80, PerRow));
            e.Graphics.DrawString("全麻总数", ptzt3, Brushes.Black, x + 10, y + 3);
            e.Graphics.DrawString("吸入麻醉", ptzt3, Brushes.Black, x + 80, y + 3);
            e.Graphics.DrawString("静脉麻醉", ptzt3, Brushes.Black, x + 150, y + 3);
            e.Graphics.DrawString("静吸复合", ptzt3, Brushes.Black, x + 220, y + 3);
            e.Graphics.DrawString("联合麻醉", ptzt3, Brushes.Black, x + 290, y + 3);
            e.Graphics.DrawString("腰麻", ptzt3, Brushes.Black, x + 360, y + 3);
            e.Graphics.DrawString("硬模", ptzt3, Brushes.Black, x + 430, y + 3);
            e.Graphics.DrawString("骶管", ptzt3, Brushes.Black, x + 500, y + 3);
            e.Graphics.DrawString("腰硬联合", ptzt3, Brushes.Black, x + 570, y + 3);
            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 70, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 140, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 210, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 280, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 350, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 420, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 490, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 560, y, 80, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 640, y, 60, PerRow));

            //数据
            e.Graphics.DrawString(tbxrmz.Text, ptzt3, Brushes.Black, x + 90, y + 3);
            e.Graphics.DrawString(tbJmmz.Text, ptzt3, Brushes.Black, x + 160, y + 3);
            e.Graphics.DrawString(tbJxfhmz.Text, ptzt3, Brushes.Black, x + 230, y + 3);
            e.Graphics.DrawString(tbLhmz.Text, ptzt3, Brushes.Black, x + 300, y + 3);
            e.Graphics.DrawString(tbYM.Text, ptzt3, Brushes.Black, x + 370, y + 3);
            e.Graphics.DrawString(tbYMW.Text, ptzt3, Brushes.Black, x + 440, y + 3);
            e.Graphics.DrawString(tbDG.Text, ptzt3, Brushes.Black, x + 510, y + 3);
            e.Graphics.DrawString(tbYYLH.Text, ptzt3, Brushes.Black, x + 580, y + 3);
            e.Graphics.DrawString(tbOther.Text, ptzt3, Brushes.Black, x + 650, y + 3);


            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 700, PerRow));
            e.Graphics.DrawString("专科麻醉数", ptzt4, Brushes.Black, x + 10, y + 3);
            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 60, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 60, y, 60, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 120, y, 60, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 180, y, 60, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 240, y, 50, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 290, y, 50, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 340, y, 50, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 390, y, 50, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 440, y, 60, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 500, y, 60, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 560, y, 60, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 620, y, 80, PerRow));
            e.Graphics.DrawString("耳鼻喉科", ptzt3, Brushes.Black, x + 5, y + 3);
            e.Graphics.DrawString("泌尿外科", ptzt3, Brushes.Black, x + 65, y + 3);
            e.Graphics.DrawString("肛肠外科", ptzt3, Brushes.Black, x + 125, y + 3);
            e.Graphics.DrawString("神经外科", ptzt4, Brushes.Black, x + 185, y + 3);
            e.Graphics.DrawString("烧伤科", ptzt3, Brushes.Black, x + 245, y + 3);
            e.Graphics.DrawString("普外一", ptzt3, Brushes.Black, x + 295, y + 3);
            e.Graphics.DrawString("普外二", ptzt3, Brushes.Black, x + 345, y + 3);
            e.Graphics.DrawString("普外三", ptzt3, Brushes.Black, x + 395, y + 3);
            e.Graphics.DrawString("骨科", ptzt3, Brushes.Black, x + 445, y + 3);
            e.Graphics.DrawString("妇产科", ptzt3, Brushes.Black, x + 505, y + 3);
            e.Graphics.DrawString("消化内科", ptzt3, Brushes.Black, x + 565, y + 3);
            e.Graphics.DrawString("胸心外科", ptzt3, Brushes.Black, x + 625, y + 3);
            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 60, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 60, y, 60, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 120, y, 60, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 180, y, 60, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 240, y, 50, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 290, y, 50, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 340, y, 50, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 390, y, 50, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 440, y, 60, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 500, y, 60, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 560, y, 60, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 620, y, 80, PerRow));
            //数据
            e.Graphics.DrawString(tbEBHK.Text, ptzt3, Brushes.Black, x + 10, y + 3);
            e.Graphics.DrawString(tbMNWK.Text, ptzt3, Brushes.Black, x + 70, y + 3);
            e.Graphics.DrawString(tbGCWK.Text, ptzt3, Brushes.Black, x + 130, y + 3);
            e.Graphics.DrawString(tbSJWK.Text, ptzt3, Brushes.Black, x + 190, y + 3);
            e.Graphics.DrawString(tbMRSSK.Text, ptzt3, Brushes.Black, x + 250, y + 3);
            e.Graphics.DrawString(tbP1.Text, ptzt3, Brushes.Black, x + 300, y + 3);
            e.Graphics.DrawString(tbP2.Text, ptzt3, Brushes.Black, x + 350, y + 3);
            e.Graphics.DrawString(tbP3.Text, ptzt3, Brushes.Black, x + 400, y + 3);
            e.Graphics.DrawString(tbGK.Text, ptzt3, Brushes.Black, x + 450, y + 3);
            e.Graphics.DrawString(tbFYFCK.Text, ptzt3, Brushes.Black, x + 510, y + 3);
            e.Graphics.DrawString(tbXHNK.Text, ptzt3, Brushes.Black, x + 570, y + 3);
            e.Graphics.DrawString(tbXXWK.Text, ptzt3, Brushes.Black, x + 630, y + 3);


            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 60, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 60, y, 60, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 120, y, 60, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 180, y, 60, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 240, y, 80, PerRow));
            e.Graphics.DrawString("老年麻醉", ptzt3, Brushes.Black, x + 5, y + 3);
            e.Graphics.DrawString("少儿麻醉", ptzt3, Brushes.Black, x + 65, y + 3);
            e.Graphics.DrawString("产科麻醉", ptzt3, Brushes.Black, x + 125, y + 3);
            e.Graphics.DrawString("动脉穿刺", ptzt4, Brushes.Black, x + 185, y + 3);
            e.Graphics.DrawString("深静脉穿刺", ptzt3, Brushes.Black, x + 245, y + 3);

            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 60, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 60, y, 60, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 120, y, 60, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 180, y, 60, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 240, y, 80, PerRow));

            //数据
            e.Graphics.DrawString(this.tbLNMZ.Text, ptzt3, Brushes.Black, x + 10, y + 3);
            e.Graphics.DrawString(tbSEMZ.Text, ptzt3, Brushes.Black, x + 70, y + 3);
            e.Graphics.DrawString(this.tbCKMZ.Text, ptzt3, Brushes.Black, x + 130, y + 3);
            e.Graphics.DrawString(this.tbDMCC.Text, ptzt3, Brushes.Black, x + 190, y + 3);
            e.Graphics.DrawString(this.tbSJMCC.Text, ptzt3, Brushes.Black, x + 250, y + 3);
           


            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 700, PerRow));
            e.Graphics.DrawString("三、手术室外麻醉与镇痛数（例次）", ptzt4, Brushes.Black, x + 10, y + 3);
            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 400, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 400, y, 150, PerRow * 2));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 550, y, 90, PerRow * 2));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 640, y, 60, PerRow * 2));

            e.Graphics.DrawString("  有创或者无创检查", ptzt4, Brushes.Black, x + 10, y + 3);
            e.Graphics.DrawString("  分娩镇痛\n（含人流）", ptzt4, Brushes.Black, x + 450, y + 3);
            e.Graphics.DrawString("  介入治疗", ptzt4, Brushes.Black, x + 560, y + 13);
            e.Graphics.DrawString("  其他", ptzt4, Brushes.Black, x + 650, y + 13);
            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 140, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 140, y, 140, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 280, y, 120, PerRow));

            e.Graphics.DrawString("  胃肠系统检查", ptzt4, Brushes.Black, x + 10, y + 3);
            e.Graphics.DrawString("  呼吸系统检查", ptzt4, Brushes.Black, x + 150, y + 3);
            e.Graphics.DrawString("  其他", ptzt4, Brushes.Black, x + 300, y + 3);

            y = y + PerRow;

            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 140, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 140, y, 140, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 280, y, 120, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 400, y, 150, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 550, y, 90, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 640, y, 60, PerRow));

            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 700, PerRow));
            e.Graphics.DrawString("四、麻醉成功率（例次）", ptzt4, Brushes.Black, x + 10, y + 3);

            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 240, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 240, y, 220, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 460, y, 240, PerRow));
            e.Graphics.DrawString("  神经(含神经丛)阻滞成功率", ptzt4, Brushes.Black, x + 10, y + 3);
            e.Graphics.DrawString("   硬膜外阻滞成功率", ptzt4, Brushes.Black, x + 230, y + 3);
            e.Graphics.DrawString("  困难气道阻滞成功率", ptzt4, Brushes.Black, x + 480, y + 3);
            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 80, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 80, y, 80, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 160, y, 80, PerRow));

            e.Graphics.DrawString("成功例数", ptzt4, Brushes.Black, x + 10, y + 3);
            e.Graphics.DrawString("同期总数", ptzt4, Brushes.Black, x + 90, y + 3);
            e.Graphics.DrawString("成功率%", ptzt4, Brushes.Black, x + 170, y + 3);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 240, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 310, y, 80, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 390, y, 70, PerRow));
            //数据
            e.Graphics.DrawString(tbZzcgs.Text, ptzt4, Brushes.Black, x + 240, y + 20);
            e.Graphics.DrawString(tbYMWZS1.Text, ptzt4, Brushes.Black, x + 310, y + 20);
            e.Graphics.DrawString(tbZZcgl.Text, ptzt4, Brushes.Black, x + 390, y + 20);

            e.Graphics.DrawString("成功例数", ptzt4, Brushes.Black, x + 250, y + 3);
            e.Graphics.DrawString("同期总数", ptzt4, Brushes.Black, x + 320, y + 3);
            e.Graphics.DrawString("成功率%", ptzt4, Brushes.Black, x + 400, y + 3);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 460, y, 80, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 540, y, 80, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 620, y, 80, PerRow));
            e.Graphics.DrawString("成功例数", ptzt4, Brushes.Black, x + 470, y + 3);
            e.Graphics.DrawString("同期总数", ptzt4, Brushes.Black, x + 550, y + 3);
            e.Graphics.DrawString("成功率%", ptzt4, Brushes.Black, x + 630, y + 3);
            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 80, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 80, y, 80, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 160, y, 80, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 240, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 310, y, 80, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 390, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 460, y, 80, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 540, y, 80, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 620, y, 80, PerRow));

            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 700, PerRow));
            e.Graphics.DrawString("四、麻醉死亡率，并发症发生率（例次）", ptzt4, Brushes.Black, x + 10, y + 3);


            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 240, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 240, y, 220, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 460, y, 240, PerRow));
            e.Graphics.DrawString("麻醉相关死亡率", ptzt4, Brushes.Black, x + 10, y + 3);
            e.Graphics.DrawString("    有创性麻醉操作感染发生率", ptzt4, Brushes.Black, x + 230, y + 3);
            e.Graphics.DrawString("有创性检测操作感染发生率", ptzt4, Brushes.Black, x + 480, y + 3);

            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 80, PerRow + 15));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 80, y, 80, PerRow + 15));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 160, y, 80, PerRow + 15));
            e.Graphics.DrawString("死亡例数", ptzt4, Brushes.Black, x + 10, y + 3);
            e.Graphics.DrawString("同期麻醉\n  总数", ptzt4, Brushes.Black, x + 90, y + 3);
            e.Graphics.DrawString("死亡率%", ptzt4, Brushes.Black, x + 170, y + 3);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 240, y, 70, PerRow + 15));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 310, y, 80, PerRow + 15));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 390, y, 70, PerRow + 15));
            e.Graphics.DrawString("感染例数", ptzt4, Brushes.Black, x + 250, y + 3);
            e.Graphics.DrawString("同期操作\n  总数", ptzt4, Brushes.Black, x + 320, y + 3);
            e.Graphics.DrawString("感染率%", ptzt4, Brushes.Black, x + 400, y + 3);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 460, y, 80, PerRow + 15));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 540, y, 80, PerRow + 15));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 620, y, 80, PerRow + 15));
            e.Graphics.DrawString("发生例数", ptzt4, Brushes.Black, x + 470, y + 3);
            e.Graphics.DrawString("同期操作\n  总数", ptzt4, Brushes.Black, x + 550, y + 3);
            e.Graphics.DrawString("感染率%", ptzt4, Brushes.Black, x + 630, y + 3);

            y = y + PerRow + 15;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 80, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 80, y, 80, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 160, y, 80, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 240, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 310, y, 80, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 390, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 460, y, 80, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 540, y, 80, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 620, y, 80, PerRow));


            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 240, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 240, y, 220, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 460, y, 240, PerRow));
            e.Graphics.DrawString("术前检查评估准备率", ptzt4, Brushes.Black, x + 10, y + 3);
            e.Graphics.DrawString("      术后访视率", ptzt4, Brushes.Black, x + 230, y + 3);
            e.Graphics.DrawString("椎管内麻醉神经并发症发生率", ptzt4, Brushes.Black, x + 470, y + 3);

            y = y + PerRow;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 80, PerRow + 15));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 80, y, 80, PerRow + 15));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 160, y, 80, PerRow + 15));
            e.Graphics.DrawString("评估例数", ptzt4, Brushes.Black, x + 10, y + 3);
            e.Graphics.DrawString("同期麻醉\n  总数", ptzt4, Brushes.Black, x + 90, y + 3);
            e.Graphics.DrawString("准备率%", ptzt4, Brushes.Black, x + 170, y + 3);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 240, y, 70, PerRow + 15));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 310, y, 80, PerRow + 15));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 390, y, 70, PerRow + 15));
            //数据
            e.Graphics.DrawString(tbSQPGls.Text, ptzt4, Brushes.Black, x + 10, y + 36);
            e.Graphics.DrawString(tbSQPGzs.Text, ptzt4, Brushes.Black, x + 90, y + 36);
            e.Graphics.DrawString(tbSQPGzbl.Text, ptzt4, Brushes.Black, x + 170, y + 36);

            e.Graphics.DrawString("访视例数", ptzt4, Brushes.Black, x + 250, y + 3);
            e.Graphics.DrawString("同期麻醉\n  总数", ptzt4, Brushes.Black, x + 320, y + 3);
            e.Graphics.DrawString("访视率%", ptzt4, Brushes.Black, x + 400, y + 3);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 460, y, 80, PerRow + 15));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 540, y, 80, PerRow + 15));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 620, y, 80, PerRow + 15));
            //数据
            e.Graphics.DrawString(tbSHSFls.Text, ptzt4, Brushes.Black, x + 250, y + 36);
            e.Graphics.DrawString(tbSHSFzs.Text, ptzt4, Brushes.Black, x + 320, y + 36);
            e.Graphics.DrawString(tbSHSFfsl.Text, ptzt4, Brushes.Black, x + 400, y + 36);

            e.Graphics.DrawString("发生例数", ptzt4, Brushes.Black, x + 470, y + 3);
            e.Graphics.DrawString("同期椎骨内\n  麻醉总数", ptzt4, Brushes.Black, x + 545, y + 3);
            e.Graphics.DrawString("发生率%", ptzt4, Brushes.Black, x + 630, y + 3);

          

            y = y + PerRow + 15;
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 0, y, 80, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 80, y, 80, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 160, y, 80, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 240, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 310, y, 80, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 390, y, 70, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 460, y, 80, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 540, y, 80, PerRow));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(x + 620, y, 80, PerRow));
        }

       
        private void button3_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

    }
}
