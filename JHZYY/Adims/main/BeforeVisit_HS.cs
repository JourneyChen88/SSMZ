using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;
using Adims_Utility;

namespace main
{
    public partial class BeforeVisit_HS : Form, IMessageFilter
    {

        AdimsController bll = new AdimsController();
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        string  PATID;

        public BeforeVisit_HS(string patid)
        {
            PATID = patid;
            InitializeComponent();
        }
        int BCCount = 0;

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SAVE();

        }

        private void SAVE()//保存方法
        {
            Dictionary<string, string> beforeVisit = new Dictionary<string, string>();
            int result = 0;
            try
            {
                beforeVisit.Add("Jybs", rtbJybs.Text);
                beforeVisit.Add("Jiwangshi", this.tbJiwangshi.Text);
                beforeVisit.Add("Guominshi", tbGuominshi.Text);
                beforeVisit.Add("XinliZQ", tbXinliZQ.Text);
                beforeVisit.Add("YangXingZB", tbYangXingZB.Text);
                beforeVisit.Add("MZFF", cmbMZFF.Text);
                beforeVisit.Add("MZFFother", tbMZFFother.Text);
                beforeVisit.Add("YuejingLC", cmbYuejingLC.Text);
                beforeVisit.Add("JingzhiYS", cmbJingzhiYS.Text);
                beforeVisit.Add("JingmaiPG", cmbJingmaiPG.Text);
                beforeVisit.Add("ZerenHushi", cmbZerenHushi.Text);
                beforeVisit.Add("Vistor", cmbVistor.Text);
                beforeVisit.Add("VisitDate", dtVisitDate.Value.ToString());
                beforeVisit.Add("PatID", PATID);

                DataTable dt = DAL.GetBeforeVisit_HS(PATID);
                if (dt.Rows.Count == 0)
                    result = DAL.InsertBeforeVisit_HS(beforeVisit);
                else
                    result = DAL.UpdateBeforeVisit_HS(beforeVisit);
                if (result > 0)
                {
                    MessageBox.Show("保存成功！");
                    BCCount++;
                }
                else
                    MessageBox.Show("保存失败！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "保存出现异常，请检查！");
            }
        }

        private void exitStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BCCount > 0)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_HS_FormClosing);
                this.Close();

            }
            else
            {
                if (DialogResult.Yes == MessageBox.Show("是否保存？", "提示", MessageBoxButtons.YesNo))
                {
                    SAVE();
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_HS_FormClosing);
                    this.Close();
                }
                else
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_HS_FormClosing);
                    this.Close();

                }

            }
        }

        private void cunDangStripMenuItem_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = DAL.GetBeforeVisit_HS(PATID);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("术前访视信息需要先保存到数据库，才能存档！");
            }
            else
            {
                result = DAL.UpdateBeforeVisit_HS_STATE();
                if (result > 0)
                {
                    MessageBox.Show("存档成功！");
                    SaveToolStripMenuItem.Enabled = false;
                }
            }
        }

        private void printStripMenuItem_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("K16", 737, 1020);  
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("K16", 737, 1020);  
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
        }

      
        //private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        //{
        //    Pen ptp = Pens.Black;//普通画笔          
        //    #region 2014/03/18修改后的代码
        //    Font ptzt = new Font("新宋体", 10);//普通字体  
        //    Font ptzt1 = new Font("新宋体", 14);//普通字体   
        //    Font ptzt2 = new Font("新宋体", 13);//普通字体   
        //    int y = 60; int x = 50; int y1 = y + 35;
        //    string title1 = "江  苏  盛  泽  医  院";
        //    string title2 = "江苏省人民医院盛泽分院";
        //    string title3 = "  术  前  访  视  单";
        //    e.Graphics.DrawString(title1, ptzt1, Brushes.Black, new Point(x + 200, y));
        //    y = y + 30; y1 = y + 17;
        //    e.Graphics.DrawString(title2, ptzt1, Brushes.Black, new Point(x + 200, y));
        //    y = y + 40; y1 = y + 17;
        //    e.Graphics.DrawString(title3, ptzt2, Brushes.Black, new Point(x + 200, y));

        //    y = y + 60; y1 = y + 17;
        //    e.Graphics.DrawString("病区：" + tbBingqu.Text, ptzt, Brushes.Black, new Point(x + 20, y));
        //    e.Graphics.DrawLine(Pens.Black, x + 53, y1, x + 176, y1);
        //    //为空画斜杠
        //    if (tbBingqu.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(60 + x, y + 13), new Point(80 + x, y + 2));
        //    }
        //    e.Graphics.DrawString("床号：" + tbBedNO.Text, ptzt, Brushes.Black, new Point(x + 180, y));
        //    e.Graphics.DrawLine(Pens.Black, x + 208, y1, x + 270, y1);
        //    //为空画斜杠
        //    if (tbBedNO.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(218 + x, y + 13), new Point(238 + x, y + 2));
        //    }
        //    e.Graphics.DrawString("住院号：" + tbZhuyuanID.Text, ptzt, Brushes.Black, new Point(x + 275, y));
        //    e.Graphics.DrawLine(Pens.Black, x + 317, y1, x + 400, y1);
        //    //为空画斜杠
        //    if (tbZhuyuanID.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(327 + x, y + 13), new Point(447 + x, y + 2));
        //    }
        //    e.Graphics.DrawString("患者姓名：" + tbPatname.Text, ptzt, Brushes.Black, new Point(x + 405, y));
        //    e.Graphics.DrawLine(Pens.Black, x + 458, y1, x + 530, y1);
        //    //为空画斜杠
        //    if (tbBingqu.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(478 + x, y + 13), new Point(498 + x, y + 2));
        //    }
        //    e.Graphics.DrawString("年龄：" + tbAge.Text, ptzt, Brushes.Black, new Point(x + 530, y));
        //    e.Graphics.DrawLine(Pens.Black, x + 562, y1, x + 635, y1);
        //    //为空画斜杠
        //    if (tbAge.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(572 + x, y + 13), new Point(591 + x, y + 2));
        //    }

        //    y = y + 40; y1 = y + 17;
        //    e.Graphics.DrawString("诊断：" + tbSqzd.Text, ptzt, Brushes.Black, new Point(x + 20, y));
        //    e.Graphics.DrawLine(Pens.Black, x + 50, y1, x + 310, y1);
        //    //为空画斜杠
        //    if (tbSqzd.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(73 + x, y + 13), new Point(330 + x, y + 2));
        //    }
        //    e.Graphics.DrawString("拟定手术：" + tbNDSS.Text, ptzt, Brushes.Black, new Point(x + 320, y));
        //    e.Graphics.DrawLine(Pens.Black, x + 383, y1, x + 638, y1);
        //    //为空画斜杠
        //    if (tbNDSS.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(403 + x, y + 13), new Point(658 + x, y + 2));
        //    }

        //    y = y + 40; y1 = y + 17;
        //    e.Graphics.DrawString("简要病史：" + rtbJybs.Text, ptzt, Brushes.Black, new Point(x + 20, y));
        //    e.Graphics.DrawLine(Pens.Black, x + 85, y1, x + 637, y1);
        //    //为空画斜杠
        //    if (rtbJybs.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(110 + x, y + 13), new Point(130 + x, y + 2));
        //    }
        //    y = y + 40; y1 = y + 17;
        //    e.Graphics.DrawLine(Pens.Black, x + 85, y1, x + 637, y1);

        //    //为空画斜杠
        //    if (rtbJybs.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(110 + x, y + 13), new Point(130 + x, y + 2));
        //    }
        //    y = y + 40; y1 = y + 17;
        //    e.Graphics.DrawString("既往史：" + tbJiwangshi.Text, ptzt, Brushes.Black, new Point(x + 20, y));
        //    e.Graphics.DrawLine(Pens.Black, x + 70, y1, x + 638, y1);
        //    //为空画斜杠
        //    if (tbJiwangshi.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(110 + x, y + 13), new Point(130 + x, y + 2));

        //        y = y + 40; y1 = y + 17;
        //        e.Graphics.DrawString("心理状况：" + tbXinliZQ.Text, ptzt, Brushes.Black, new Point(x + 20, y));
        //        e.Graphics.DrawLine(Pens.Black, x + 85, y1, x + 637, y1);

        //        //为空画斜杠
        //        if (tbXinliZQ.Text == "")
        //        {
        //            e.Graphics.DrawLine(ptp, new Point(110 + x, y + 13), new Point(130 + x, y + 2));
        //        }
        //        y = y + 40; y1 = y + 17;
        //        e.Graphics.DrawString("主要阳性指标(实验室及辅助检查)：" + tbXinliZQ.Text, ptzt, Brushes.Black, new Point(x + 20, y));
        //        e.Graphics.DrawLine(Pens.Black, x + 240, y1, x + 640, y1);

        //        //为空画斜杠
        //        if (tbXinliZQ.Text == "")
        //        {
        //            e.Graphics.DrawLine(ptp, new Point(260 + x, y + 13), new Point(280 + x, y + 2));
        //        }
        //        y = y + 40; y1 = y + 17;
        //        e.Graphics.DrawString("麻醉方式：" + cmbMZFF.Text, ptzt, Brushes.Black, new Point(x + 20, y));
        //        e.Graphics.DrawLine(Pens.Black, x + 85, y1, x + 200, y1);
        //        //为空画斜杠
        //        if (cmbMZFF.Text == "")
        //        {
        //            e.Graphics.DrawLine(ptp, new Point(105 + x, y + 13), new Point(125 + x, y + 2));
        //        }
        //        e.Graphics.DrawString("其他：" + tbMZFFother.Text, ptzt, Brushes.Black, new Point(x + 240, y));
        //        e.Graphics.DrawLine(Pens.Black, x + 275, y1, x + 640, y1);
        //        //为空画斜杠
        //        if (tbMZFFother.Text == "")
        //        {
        //            e.Graphics.DrawLine(ptp, new Point(295 + x, y + 13), new Point(315 + x, y + 2));
        //        }

        //        y = y + 40; y1 = y + 17;
        //        e.Graphics.DrawString("月经来潮：" + cmbYuejingLC.Text, ptzt, Brushes.Black, new Point(x + 20, y));
        //        e.Graphics.DrawLine(Pens.Black, x + 80, y1, x + 150, y1);
        //        //为空画斜杠
        //        if (cmbYuejingLC.Text == "")
        //        {
        //            e.Graphics.DrawLine(ptp, new Point(105 + x, y + 13), new Point(125 + x, y + 2));
        //        }
        //        e.Graphics.DrawString("是否知道术前禁食、禁饮时间：" + cmbJingzhiYS.Text, ptzt, Brushes.Black, new Point(x + 180, y));
        //        e.Graphics.DrawLine(Pens.Black, x + 370, y1, x + 450, y1);
        //        //为空画斜杠
        //        if (cmbJingzhiYS.Text == "")
        //        {
        //            e.Graphics.DrawLine(ptp, new Point(390 + x, y + 13), new Point(410 + x, y + 2));
        //        }
        //        e.Graphics.DrawString("静脉条件评估：" + cmbJingzhiYS.Text, ptzt, Brushes.Black, new Point(x + 480, y));
        //        e.Graphics.DrawLine(Pens.Black, x + 570, y1, x + 640, y1);

        //        //为空画斜杠
        //        if (cmbJingzhiYS.Text == "")
        //        {
        //            e.Graphics.DrawLine(ptp, new Point(590 + x, y + 13), new Point(610 + x, y + 2));
        //        }
        //        y = y + 40; y1 = y + 17;
        //        e.Graphics.DrawString("病区责任护士签名：" + cmbZerenHushi.Text, ptzt, Brushes.Black, new Point(x + 20, y));
        //        e.Graphics.DrawLine(Pens.Black, x + 140, y1, x + 220, y1);
        //        //为空画斜杠
        //        if (cmbZerenHushi.Text == "")
        //        {
        //            e.Graphics.DrawLine(ptp, new Point(160 + x, y + 13), new Point(180 + x, y + 2));
        //        }
        //        e.Graphics.DrawString("术前访视者签名：" + cmbJingzhiYS.Text, ptzt, Brushes.Black, new Point(x + 260, y));
        //        e.Graphics.DrawLine(Pens.Black, x + 360, y1, x + 440, y1);
        //        //为空画斜杠
        //        if (cmbJingzhiYS.Text == "")
        //        {
        //            e.Graphics.DrawLine(ptp, new Point(390 + x, y + 13), new Point(410 + x, y + 2));
        //        }
        //        e.Graphics.DrawString("访视日期：" + dtVisitDate.Text, ptzt, Brushes.Black, new Point(x + 470, y));
        //        e.Graphics.DrawLine(Pens.Black, x + 535, y1, x + 640, y1);
        //        //为空画斜杠
        //        if (dtVisitDate.Text == "")
        //        {
        //            e.Graphics.DrawLine(ptp, new Point(555 + x, y + 13), new Point(575 + x, y + 2));
        //        }

        //    #endregion
        //    }
        //}
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Pen ptp = Pens.Black;//普通画笔
            #region 注释原有代码
            //Font ptzt = new Font("新宋体", 10);//普通字体  
            //Font ptzt1 = new Font("新宋体", 14);//普通字体   
            //Font ptzt2 = new Font("新宋体", 13);//普通字体   
            //int y = 80;
            //int x = 50;
            //int y1 = y+15;
            //string title1 = "江  苏  盛  泽  医  院";
            //string title2 = "江苏省人民医院盛泽分院";
            //string title3 = "  术  前  访  视  单";
            //e.Graphics.DrawString(title1, ptzt1, Brushes.Black, new Point(x + 200, y));
            //y = y + 30; y1 = y + 17;
            //e.Graphics.DrawString(title2, ptzt1, Brushes.Black, new Point(x + 200, y));
            //y = y + 40; y1 = y + 17;
            //e.Graphics.DrawString(title3, ptzt2, Brushes.Black, new Point(x + 200, y));

            //y = y + 60; y1 = y + 17;
            //e.Graphics.DrawString("病区："+tbBingqu.Text, ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 35, y1, x + 170, y1);
            //e.Graphics.DrawString("床号："+tbBedNO.Text, ptzt, Brushes.Black, new Point(x + 180, y));
            //e.Graphics.DrawLine(Pens.Black, x + 215, y1, x + 280, y1);
            //e.Graphics.DrawString("住院号：" + tbZhuyuanID.Text, ptzt, Brushes.Black, new Point(x+290, y));
            //e.Graphics.DrawLine(Pens.Black, x + 335, y1, x + 420, y1);
            //e.Graphics.DrawString("患者姓名：" + tbPatname.Text, ptzt, Brushes.Black, new Point(x + 430, y));
            //e.Graphics.DrawLine(Pens.Black, x + 490, y1, x + 595, y1); 
            //e.Graphics.DrawString("年龄：" + tbAge.Text, ptzt, Brushes.Black, new Point(x+605, y));
            //e.Graphics.DrawLine(Pens.Black, x + 640, y1, x + 680, y1);

            //y = y + 40; y1 = y + 17;
            //e.Graphics.DrawString("诊断：" + tbSqzd.Text, ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 30, y1, x + 330, y1);
            //e.Graphics.DrawString("拟定手术：" + tbNDSS.Text, ptzt, Brushes.Black, new Point(x + 350, y));
            //e.Graphics.DrawLine(Pens.Black, x + 410, y1, x + 700, y1);

            //y = y + 40; y1 = y + 17;
            //e.Graphics.DrawString("简要病史：" + rtbJybs.Text, ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 65, y1, x + 700, y1);
            //y = y + 40; y1 = y + 17;
            //e.Graphics.DrawLine(Pens.Black, x, y1, x + 700, y1);

            //y = y + 40; y1 = y + 17;
            //e.Graphics.DrawString("既往史：" + tbJiwangshi.Text, ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 50, y1, x + 700, y1);

            //y = y + 40; y1 = y + 17;
            //e.Graphics.DrawString("心理状况：" + tbXinliZQ.Text, ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 65, y1, x + 700, y1);

            //y = y + 40; y1 = y + 17;
            //e.Graphics.DrawString("主要阳性指标(实验室及辅助检查)：" + tbXinliZQ.Text, ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 220, y1, x + 700, y1);

            //y = y + 40; y1 = y + 17;
            //e.Graphics.DrawString("麻醉方式：" + cmbMZFF.Text, ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 65, y1, x + 200, y1);
            //e.Graphics.DrawString("其他：" + tbMZFFother.Text, ptzt, Brushes.Black, new Point(x + 220, y));
            //e.Graphics.DrawLine(Pens.Black, x + 255, y1, x + 600, y1);

            //y = y + 40; y1 = y + 17;
            //e.Graphics.DrawString("月经来潮：" + cmbYuejingLC.Text, ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 60, y1, x + 150, y1);
            //e.Graphics.DrawString("是否知道术前禁食、禁饮时间：" + cmbJingzhiYS.Text, ptzt, Brushes.Black, new Point(x + 160, y));
            //e.Graphics.DrawLine(Pens.Black, x + 350, y1, x + 450, y1);
            //e.Graphics.DrawString("静脉条件评估：" + cmbJingzhiYS.Text, ptzt, Brushes.Black, new Point(x + 460, y));
            //e.Graphics.DrawLine(Pens.Black, x + 550, y1, x + 630, y1);

            //y = y + 40; y1 = y + 17;
            //e.Graphics.DrawString("病区责任护士签名：" + cmbZerenHushi.Text, ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 120, y1, x + 220, y1);
            //e.Graphics.DrawString("术前访视者签名：" + cmbJingzhiYS.Text, ptzt, Brushes.Black, new Point(x + 240, y));
            //e.Graphics.DrawLine(Pens.Black, x + 340, y1, x + 440, y1);
            //e.Graphics.DrawString("访视日期：" + dtVisitDate.Text, ptzt, Brushes.Black, new Point(x + 450, y));
            //e.Graphics.DrawLine(Pens.Black, x + 515, y1, x + 650, y1);
            #endregion
            #region 2014/03/18修改后的代码
            Font ptzt = new Font("新宋体", 10);//普通字体  
            Font ptzt1 = new Font("新宋体", 14);//普通字体   
            Font ptzt2 = new Font("新宋体", 13);//普通字体   
            int y = 60; int x = 50; int y1 = y + 35;
            string title1 = "江  苏  盛  泽  医  院";
            string title2 = "江苏省人民医院盛泽分院";
            string title3 = "  术  前  访  视  单";
            e.Graphics.DrawString(title1, ptzt1, Brushes.Black, new Point(x + 200, y));
            y = y + 30; y1 = y + 17;
            e.Graphics.DrawString(title2, ptzt1, Brushes.Black, new Point(x + 200, y));
            y = y + 40; y1 = y + 17;
            e.Graphics.DrawString(title3, ptzt2, Brushes.Black, new Point(x + 200, y));

            y = y + 60; y1 = y + 17;
            e.Graphics.DrawString("病区：" + tbBingqu.Text, ptzt, Brushes.Black, new Point(x + 20, y));
            e.Graphics.DrawLine(Pens.Black, x + 53, y1, x + 176, y1);
            //为空画斜杠
            if (tbBingqu.Text.IsNullOrEmpty())
            {
                e.Graphics.DrawLine(ptp, new Point(60 + x, y + 13), new Point(80 + x, y + 2));
            }
            e.Graphics.DrawString("床号：" + tbBedNO.Text, ptzt, Brushes.Black, new Point(x + 180, y));
            e.Graphics.DrawLine(Pens.Black, x + 208, y1, x + 270, y1);
            //为空画斜杠
            if (tbBedNO.Text.IsNullOrEmpty())
            {
                e.Graphics.DrawLine(ptp, new Point(218 + x, y + 13), new Point(238 + x, y + 2));
            }
            e.Graphics.DrawString("住院号：" + tbZhuyuanID.Text, ptzt, Brushes.Black, new Point(x + 275, y));
            e.Graphics.DrawLine(Pens.Black, x + 317, y1, x + 400, y1);
            //为空画斜杠
            if (tbZhuyuanID.Text.IsNullOrEmpty())
            {
                e.Graphics.DrawLine(ptp, new Point(327 + x, y + 13), new Point(447 + x, y + 2));
            }
            e.Graphics.DrawString("患者姓名：" + tbPatname.Text, ptzt, Brushes.Black, new Point(x + 405, y));
            e.Graphics.DrawLine(Pens.Black, x + 458, y1, x + 530, y1);
            //为空画斜杠
            if (tbBingqu.Text.IsNullOrEmpty())
            {
                e.Graphics.DrawLine(ptp, new Point(478 + x, y + 13), new Point(498 + x, y + 2));
            }
            e.Graphics.DrawString("年龄：" + tbAge.Text, ptzt, Brushes.Black, new Point(x + 530, y));
            e.Graphics.DrawLine(Pens.Black, x + 562, y1, x + 635, y1);
            //为空画斜杠
            if (tbAge.Text.IsNullOrEmpty())
            {
                e.Graphics.DrawLine(ptp, new Point(572 + x, y + 13), new Point(591 + x, y + 2));
            }

            y = y + 40; y1 = y + 17;
            e.Graphics.DrawString("诊断：" + tbSqzd.Text, ptzt, Brushes.Black, new Point(x + 20, y));
            e.Graphics.DrawLine(Pens.Black, x + 50, y1, x + 310, y1);
            //为空画斜杠
            if (tbSqzd.Text.IsNullOrEmpty())
            {
                e.Graphics.DrawLine(ptp, new Point(73 + x, y + 13), new Point(330 + x, y + 2));
            }
            e.Graphics.DrawString("拟定手术：" + tbNDSS.Text, ptzt, Brushes.Black, new Point(x + 320, y));
            e.Graphics.DrawLine(Pens.Black, x + 383, y1, x + 638, y1);
            //为空画斜杠
            if (tbNDSS.Text.IsNullOrEmpty())
            {
                e.Graphics.DrawLine(ptp, new Point(403 + x, y + 13), new Point(658 + x, y + 2));
            }

            y = y + 40; y1 = y + 17;
            e.Graphics.DrawString("简要病史：" + rtbJybs.Text, ptzt, Brushes.Black, new Point(x + 20, y));
            e.Graphics.DrawLine(Pens.Black, x + 85, y1, x + 637, y1);
            //为空画斜杠
            if (rtbJybs.Text.IsNullOrEmpty())
            {
                e.Graphics.DrawLine(ptp, new Point(110 + x, y + 13), new Point(130 + x, y + 2));
            }
            y = y + 40; y1 = y + 17;
            e.Graphics.DrawLine(Pens.Black, x + 85, y1, x + 637, y1);

            //为空画斜杠
            if (rtbJybs.Text.IsNullOrEmpty())
            {
                e.Graphics.DrawLine(ptp, new Point(110 + x, y + 13), new Point(130 + x, y + 2));
            }
            y = y + 40; y1 = y + 17;
            e.Graphics.DrawString("既往史：" + tbJiwangshi.Text, ptzt, Brushes.Black, new Point(x + 20, y));
            e.Graphics.DrawLine(Pens.Black, x + 70, y1, x + 638, y1);
            //为空画斜杠
            if (tbJiwangshi.Text.IsNullOrEmpty())
            {
                e.Graphics.DrawLine(ptp, new Point(110 + x, y + 13), new Point(130 + x, y + 2));

                y = y + 40; y1 = y + 17;
                e.Graphics.DrawString("心理状况：" + tbXinliZQ.Text, ptzt, Brushes.Black, new Point(x + 20, y));
                e.Graphics.DrawLine(Pens.Black, x + 85, y1, x + 637, y1);

                //为空画斜杠
                if (tbXinliZQ.Text.IsNullOrEmpty())
                {
                    e.Graphics.DrawLine(ptp, new Point(110 + x, y + 13), new Point(130 + x, y + 2));
                }
                y = y + 40; y1 = y + 17;
                e.Graphics.DrawString("主要阳性指标(实验室及辅助检查)：" + tbXinliZQ.Text, ptzt, Brushes.Black, new Point(x + 20, y));
                e.Graphics.DrawLine(Pens.Black, x + 240, y1, x + 640, y1);

                //为空画斜杠
                if (tbXinliZQ.Text.IsNullOrEmpty())
                {
                    e.Graphics.DrawLine(ptp, new Point(260 + x, y + 13), new Point(280 + x, y + 2));
                }
                y = y + 40; y1 = y + 17;
                e.Graphics.DrawString("麻醉方式：" + cmbMZFF.Text, ptzt, Brushes.Black, new Point(x + 20, y));
                e.Graphics.DrawLine(Pens.Black, x + 85, y1, x + 200, y1);
                //为空画斜杠
                if (cmbMZFF.Text.IsNullOrEmpty())
                {
                    e.Graphics.DrawLine(ptp, new Point(105 + x, y + 13), new Point(125 + x, y + 2));
                }
                e.Graphics.DrawString("其他：" + tbMZFFother.Text, ptzt, Brushes.Black, new Point(x + 240, y));
                e.Graphics.DrawLine(Pens.Black, x + 275, y1, x + 640, y1);
                //为空画斜杠
                if (tbMZFFother.Text.IsNullOrEmpty())
                {
                    e.Graphics.DrawLine(ptp, new Point(295 + x, y + 13), new Point(315 + x, y + 2));
                }

                y = y + 40; y1 = y + 17;
                e.Graphics.DrawString("月经来潮：" + cmbYuejingLC.Text, ptzt, Brushes.Black, new Point(x + 20, y));
                e.Graphics.DrawLine(Pens.Black, x + 80, y1, x + 150, y1);
                //为空画斜杠
                if (cmbYuejingLC.Text.IsNullOrEmpty())
                {
                    e.Graphics.DrawLine(ptp, new Point(105 + x, y + 13), new Point(125 + x, y + 2));
                }
                e.Graphics.DrawString("是否知道术前禁食、禁饮时间：" + cmbJingzhiYS.Text, ptzt, Brushes.Black, new Point(x + 180, y));
                e.Graphics.DrawLine(Pens.Black, x + 370, y1, x + 450, y1);
                //为空画斜杠
                if (cmbJingzhiYS.Text.IsNullOrEmpty())
                {
                    e.Graphics.DrawLine(ptp, new Point(390 + x, y + 13), new Point(410 + x, y + 2));
                }
                e.Graphics.DrawString("静脉条件评估：" + cmbJingzhiYS.Text, ptzt, Brushes.Black, new Point(x + 480, y));
                e.Graphics.DrawLine(Pens.Black, x + 570, y1, x + 640, y1);

                //为空画斜杠
                if (cmbJingzhiYS.Text.IsNullOrEmpty())
                {
                    e.Graphics.DrawLine(ptp, new Point(590 + x, y + 13), new Point(610 + x, y + 2));
                }
                y = y + 40; y1 = y + 17;
                e.Graphics.DrawString("病区责任护士签名：" + cmbZerenHushi.Text, ptzt, Brushes.Black, new Point(x + 20, y));
                e.Graphics.DrawLine(Pens.Black, x + 140, y1, x + 220, y1);
                //为空画斜杠
                if (cmbZerenHushi.Text.IsNullOrEmpty())
                {
                    e.Graphics.DrawLine(ptp, new Point(160 + x, y + 13), new Point(180 + x, y + 2));
                }
                e.Graphics.DrawString("术前访视者签名：" + cmbJingzhiYS.Text, ptzt, Brushes.Black, new Point(x + 260, y));
                e.Graphics.DrawLine(Pens.Black, x + 360, y1, x + 440, y1);
                //为空画斜杠
                if (cmbJingzhiYS.Text.IsNullOrEmpty())
                {
                    e.Graphics.DrawLine(ptp, new Point(390 + x, y + 13), new Point(410 + x, y + 2));
                }
                e.Graphics.DrawString("访视日期：" + dtVisitDate.Text, ptzt, Brushes.Black, new Point(x + 470, y));
                e.Graphics.DrawLine(Pens.Black, x + 535, y1, x + 640, y1);
                //为空画斜杠
                if (dtVisitDate.Text.IsNullOrEmpty())
                {
                    e.Graphics.DrawLine(ptp, new Point(555 + x, y + 13), new Point(575 + x, y + 2));
                }

            #endregion
            }
        }
        
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 522)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void BindCombox3()
        {

            this.cmbZerenHushi.Items.Clear();
            this.cmbVistor.Items.Clear();
            DataTable dt = new DataTable();
            dt = DAL.GetAll_hushi();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbZerenHushi.Items.Add(dt.Rows[i][0]);
                cmbVistor.Items.Add(dt.Rows[i][0]);
            }
        }



        private void BeforeVisit_HS_Load(object sender, EventArgs e)
        {

            Application.AddMessageFilter(this);//禁止下拉框鼠标滚动
            BindCombox3();
            BindPatInfo();
            SetEditValue();
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).ReadOnly = true;
                }
            }
        }
        private void BindPatInfo()
        {
            DataTable dt = new DataTable();
            dt = DAL.GetALLPAIBAN(PATID);
            tbPatname.Text = dt.Rows[0]["Patname"].ToString();
            tbAge.Text = dt.Rows[0]["Patage"].ToString();
            tbSex.Text = dt.Rows[0]["Patsex"].ToString();
            tbBingqu.Text = dt.Rows[0]["patdpm"].ToString();
            tbBedNO.Text = dt.Rows[0]["Patbedno"].ToString();
            tbZhuyuanID.Text = dt.Rows[0]["Patzhuyuanid"].ToString();
            tbNDSS.Text = dt.Rows[0]["Oname"].ToString();
            tbSqzd.Text = dt.Rows[0]["Pattmd"].ToString();

        }
        private void SetEditValue()
        {

            DataTable dt = DAL.GetBeforeVisit_HS(PATID);
            if (dt.Rows.Count == 0)
                return;
            else
            {
                DataRow dr = dt.Rows[0];
                rtbJybs.Text = Convert.ToString(dr["Jybs"]);
                tbJiwangshi.Text = Convert.ToString(dr["Jiwangshi"]);
                tbGuominshi.Text = Convert.ToString(dr["Guominshi"]);
                tbXinliZQ.Text = Convert.ToString(dr["XinliZQ"]);
                tbYangXingZB.Text = Convert.ToString(dr["YangXingZB"]);
                cmbMZFF.Text = Convert.ToString(dr["MZFF"]);
                tbMZFFother.Text = Convert.ToString(dr["MZFFother"]);
                cmbYuejingLC.Text = Convert.ToString(dr["YuejingLC"]);
                cmbJingzhiYS.Text = Convert.ToString(dr["JingzhiYS"]);
                cmbJingmaiPG.Text = Convert.ToString(dr["JingmaiPG"]);
                cmbZerenHushi.Text = Convert.ToString(dr["ZerenHushi"]);
                cmbVistor.Text = Convert.ToString(dr["Vistor"]);
                dtVisitDate.Value = Convert.ToDateTime(dr["VisitDate"]);
                if (Convert.ToInt32(dr["IsRead"]) == 1)
                {
                    SaveToolStripMenuItem.Enabled = false;
                    foreach (Control ctrl in this.Controls)
                    {
                        if (ctrl is ComboBox || ctrl is TextBox || ctrl is DateTimePicker)
                        {
                            ctrl.Enabled = false;
                        }
                    }
                }
            }
        }

        private void cmbMZFF_TextChanged(object sender, EventArgs e)
        {
            if (cmbMZFF.Text.Trim() == "其他")
            {
                tbMZFFother.ReadOnly = false;
            }
        }

        private void BeforeVisit_HS_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (BCCount > 0)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_HS_FormClosing);
                this.Close();

            }
            else
            {
                if (DialogResult.Yes == MessageBox.Show("是否保存？", "提示", MessageBoxButtons.YesNo))
                {
                    SAVE();
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_HS_FormClosing);
                    this.Close();
                }
                else
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_HS_FormClosing);
                    this.Close();

                }

            }

        }
    }
}
