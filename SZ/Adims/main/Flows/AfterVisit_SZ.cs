using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;
using adims_DAL;

namespace main
{
    public partial class AfterVisit_SZ : Form
    {
        DB2help db2help = new DB2help();
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        string PATID, MZID;
        public AfterVisit_SZ()
        {
        }
        public AfterVisit_SZ(string mzid, string patid)
        {
            MZID = mzid;
            PATID = patid;
            InitializeComponent();
        }

        private void AfterVisit_SZ_Load(object sender, EventArgs e)
        {
            DataTable dt = DAL.GetAfterVisitCount_SZ(PATID);
            if (dt.Rows.Count == 0)
            {
                DAL.InsertAfterVisit_SZ(PATID);
            }
            else
            {
                SetEditValue();
            }

            BindPatInfo();
            BindMzjldInfo();
        }
        private void BindMzjldInfo()
        {
            DataTable dt = DAL.GetMZJLD_Info(MZID);
            //cmbMZFF.Text = dt.Rows[0]["mzfa"].ToString();
            tbSZZD.Text = dt.Rows[0]["szzd"].ToString();
            tbShoushuName.Text = dt.Rows[0]["ssss"].ToString();
        }
        private void BindPatInfo()
        {
            DataTable dt = new DataTable();
            dt = DAL.GetALLPAIBAN(PATID);
            tbPatname.Text = dt.Rows[0]["Patname"].ToString();
            tbZhuyuanID.Text = dt.Rows[0]["PatZhuYuanID"].ToString();
            tbBedNO.Text = dt.Rows[0]["Patbedno"].ToString();
            tbShoushuName.Text = dt.Rows[0]["Oname"].ToString();
            tbSex.Text = dt.Rows[0]["patsex"].ToString();
            tbAge.Text = dt.Rows[0]["patage"].ToString();
            tbBingqu.Text = dt.Rows[0]["Patdpm"].ToString();
            //cmbMZFF.Text = dt.Rows[0]["Amethod"].ToString();

        }
        private void SetEditValue()
        {
            DataTable dt = DAL.GetAfterVisitCount_SZ(PATID);
            if (dt.Rows.Count > 0)
            {
                tbXinliZK.Text = Convert.ToString(dt.Rows[0]["XinliZK"]);
                tbBingfaZother.Text = Convert.ToString(dt.Rows[0]["BingfaZother"]);
                rtbJKJY.Text = Convert.ToString(dt.Rows[0]["JKJY"]);
                string taidu = Convert.ToString(dt.Rows[0]["Taidu"]);
                string zerenxin = Convert.ToString(dt.Rows[0]["Zerenxin"]);
                string jishu = Convert.ToString(dt.Rows[0]["Jishu"]);
                string zonghePJ = Convert.ToString(dt.Rows[0]["ZonghePJ"]);
                string Weisheng = Convert.ToString(dt.Rows[0]["ZJAQWS"]);
                rtbYijian.Text = Convert.ToString(dt.Rows[0]["Yijian"]);
                cmbZerenhushi.Text = Convert.ToString(dt.Rows[0]["Zerenhushi"]);
                cmbVistor.Text = Convert.ToString(dt.Rows[0]["Vistor"]);
                dtVisitDate.Value = Convert.ToDateTime(dt.Rows[0]["VisitDate"]);
                string pifu = Convert.ToString(dt.Rows[0]["pifu"]);
                if (pifu == "有")
                {
                    cbPifu1.Checked = true;
                }
                else if (pifu == "无")
                {
                    cbPifu2.Checked = true;
                }

                //ShenZhi='{12}', JingShen='{13}',QiekouTT='{14}',PiFu='{15}',GouTong='{16}',WuguanHuati
                string ShenZhi = Convert.ToString(dt.Rows[0]["ShenZhi"]);
                if (ShenZhi == "清醒")
                {
                    cbShenzhi1.Checked = true;
                }
                else if (ShenZhi == "嗜睡")
                {
                    cbShenzhi2.Checked = true;
                }
                else if (ShenZhi == "昏迷")
                {
                    cbShenzhi3.Checked = true;
                }
                string JingShen = Convert.ToString(dt.Rows[0]["JingShen"]);
                if (JingShen == "好")
                {
                    cbJingshen1.Checked = true;
                }
                else if (JingShen == "欠佳")
                {
                    cbJingshen2.Checked = true;
                }
                else if (JingShen == "萎靡")
                {
                    cbJingshen3.Checked = true;
                }

                string QiekouTT = Convert.ToString(dt.Rows[0]["QiekouTT"]);
                if (QiekouTT == "有")
                {
                    cbQiekouTT1.Checked = true;
                }
                else if (QiekouTT == "无")
                {
                    cbQiekouTT2.Checked = true;
                }

            }

        }
        private void SAVE()//保存方法
        {
            Dictionary<string, string> A_VISIT = new Dictionary<string, string>();
            int result = 0;
            string ValueS = "";
            try
            {
                A_VISIT.Add("XinliZK", tbXinliZK.Text);
                A_VISIT.Add("BingfaZother", tbBingfaZother.Text);
                A_VISIT.Add("JKJY", rtbJKJY.Text);
                if (cbTaidu1.Checked)
                    ValueS = cbTaidu1.Text;
                if (cbTaidu2.Checked)
                    ValueS = cbTaidu2.Text;
                if (cbTaidu3.Checked)
                    ValueS = cbTaidu3.Text;
                A_VISIT.Add("Taidu", ValueS);
                ValueS = "";
                if (cbZRxin1.Checked)
                    ValueS = cbZRxin1.Text;
                if (cbZRxin2.Checked)
                    ValueS = cbZRxin2.Text;
                if (cbZRxin3.Checked)
                    ValueS = cbZRxin3.Text;
                A_VISIT.Add("Zerenxin", ValueS);
                ValueS = "";
                if (cbJishu1.Checked)
                    ValueS = cbJishu1.Text;
                if (cbJishu2.Checked)
                    ValueS = cbJishu2.Text;
                if (cbJishu3.Checked)
                    ValueS = cbJishu3.Text;
                A_VISIT.Add("Jishu", ValueS);
                ValueS = "";
                if (cbBRPinjia1.Checked)
                    ValueS = cbBRPinjia1.Text;
                if (cbBRPinjia2.Checked)
                    ValueS = cbJishu2.Text;
                if (cbJishu3.Checked)
                    ValueS = cbJishu3.Text;
                A_VISIT.Add("ZonghePJ", ValueS);
                ValueS = "";
                if (cbWeisheng1.Checked)
                    ValueS = cbWeisheng1.Text;
                if (cbWeisheng2.Checked)
                    ValueS = cbJishu2.Text;
                if (cbJishu3.Checked)
                    ValueS = cbJishu3.Text;
                A_VISIT.Add("ZJAQWS", ValueS);
                A_VISIT.Add("Yijian", this.rtbYijian.Text);
                A_VISIT.Add("Zerenhushi", cmbZerenhushi.Text);
                A_VISIT.Add("Vistor", cmbVistor.Text);
                A_VISIT.Add("VisitDate", dtVisitDate.Value.ToString());


                ValueS = "";
                if (cbShenzhi1.Checked)
                    ValueS = cbShenzhi1.Text;
                if (cbShenzhi2.Checked)
                    ValueS = cbShenzhi2.Text;
                if (cbShenzhi3.Checked)
                    ValueS = cbShenzhi3.Text;
                A_VISIT.Add("ShenZhi", ValueS);

                ValueS = "";
                if (cbJingshen1.Checked)
                    ValueS = cbJingshen1.Text;
                if (cbJingshen2.Checked)
                    ValueS = cbJingshen2.Text;
                if (cbJingshen3.Checked)
                    ValueS = cbJingshen3.Text;
                A_VISIT.Add("JingShen", ValueS);

                ValueS = "";
                if (cbQiekouTT1.Checked)
                    ValueS = cbQiekouTT1.Text;
                if (cbQiekouTT2.Checked)
                    ValueS = cbQiekouTT2.Text;
                A_VISIT.Add("QiekouTT", ValueS);

                ValueS = "";
                if (cbPifu1.Checked)
                    ValueS = cbPifu1.Text;
                if (cbPifu2.Checked)
                    ValueS = cbPifu2.Text;
                A_VISIT.Add("PiFu", ValueS);

                ValueS = "";
                if (cbGoutong1.Checked)
                    ValueS = cbGoutong1.Text;
                if (cbGoutong2.Checked)
                    ValueS = cbGoutong2.Text;
                if (cbGoutong3.Checked)
                    ValueS = cbGoutong3.Text;
                A_VISIT.Add("GouTong", ValueS);

                ValueS = "";
                if (cbWuguanHuati1.Checked)
                    ValueS = cbWuguanHuati1.Text;
                if (cbWuguanHuati2.Checked)
                    ValueS = cbWuguanHuati2.Text;
                A_VISIT.Add("WuguanHuati", ValueS);

                A_VISIT.Add("PatID", PATID);


                result = DAL.UpdateAfterVisit_SZ(A_VISIT);
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
        int BCCount = 0;
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SAVE();

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

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Pen ptp = Pens.Black;//普通画笔


            Font textfront = new Font(new FontFamily("宋体"), 9);
            Font tagfont = new Font(new FontFamily("宋体"), 14);
            Pen pen2 = new Pen(Color.Black, 2);

            int x = 50, y = 30, y1 = y + 18;
            string title1 = "江  苏  盛  泽  医  院";
            string title2 = "江苏省人民医院盛泽分院";
            string title3 = "      术后访视单";
            y = y + 30; y1 = y + 18;
            e.Graphics.DrawString(title1, tagfont, Brushes.Black, x + 220, y);
            y = y + 30; y1 = y + 18;
            e.Graphics.DrawString(title2, tagfont, Brushes.Black, x + 220, y);
            y = y + 30; y1 = y + 18;
            e.Graphics.DrawString(title3, tagfont, Brushes.Black, x + 220, y);


            y = y + 50; y1 = y + 15;
            e.Graphics.DrawString("病区：" + tbBingqu.Text, textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 30, y1, x + 150, y1);
            //为空画斜杠
            if (tbBingqu.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(50 + x, y + 13), new Point(70 + x, y + 2));
            }
            e.Graphics.DrawString("床号：" + tbBedNO.Text, textfront, Brushes.Black, x + 160, y);
            e.Graphics.DrawLine(Pens.Black, x + 190, y1, x + 240, y1);
            //为空画斜杠
            if (tbBedNO.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(200 + x, y + 13), new Point(220 + x, y + 2));
            }
            e.Graphics.DrawString("住院号：" + tbZhuyuanID.Text, textfront, Brushes.Black, x + 250, y);
            e.Graphics.DrawLine(Pens.Black, x + 290, y1, x + 367, y1);
            //为空画斜杠
            if (tbZhuyuanID.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(320 + x, y + 13), new Point(340 + x, y + 2));
            }
            e.Graphics.DrawString("患者姓名：" + tbPatname.Text, textfront, Brushes.Black, x + 380, y);
            e.Graphics.DrawLine(Pens.Black, x + 430, y1, x + 530, y1);
            //为空画斜杠
            if (tbPatname.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(470 + x, y + 13), new Point(490 + x, y + 2));
            }
            e.Graphics.DrawString("年龄：" + tbAge.Text, textfront, Brushes.Black, x + 540, y);
            e.Graphics.DrawLine(Pens.Black, x + 570, y1, x + 650, y1);
            //为空画斜杠
            if (tbAge.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(623 + x, y + 13), new Point(643 + x, y + 2));
            }

            y = y + 40; y1 = y + 15;
            e.Graphics.DrawString("诊断：" + tbSZZD.Text, textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 30, y1, x + 350, y1);
            //为空画斜杠
            if (tbSZZD.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(50 + x, y + 13), new Point(70 + x, y + 2));
            }
            e.Graphics.DrawString("手术名称：" + tbShoushuName.Text, textfront, Brushes.Black, x + 360, y);
            e.Graphics.DrawLine(Pens.Black, x + 410, y1, x + 650, y1);
            //为空画斜杠
            if (tbShoushuName.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(480 + x, y + 13), new Point(500 + x, y + 2));
            }


            y = y + 40; y1 = y + 10;
            e.Graphics.DrawString("麻醉方式：  □ 局麻    □ 腰麻    □ 硬麻    □ 全麻", textfront, Brushes.Black, x + 0, y);
            Point[] pointJM = { new Point(x + 80, y), new Point(x + 85, y1), new Point(x + 95, y) };
            if (cbJuma.Checked)
                e.Graphics.DrawLines(pen2, pointJM);
            Point[] pointYM = { new Point(x + 150, y), new Point(x + 155, y1), new Point(x + 165, y) };
            if (cbJuma.Checked)
                e.Graphics.DrawLines(pen2, pointYM);
            Point[] pointYINGM = { new Point(x + 220, y), new Point(x + 225, y1), new Point(x + 235, y) };
            if (cbJuma.Checked)
                e.Graphics.DrawLines(pen2, pointYINGM);
            Point[] pointQM = { new Point(x + 290, y), new Point(x + 295, y1), new Point(x + 305, y) };
            if (cbJuma.Checked)
                e.Graphics.DrawLines(pen2, pointQM);

            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("心里状况：" + tbXinliZK.Text, textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 60, y1, x + 650, y1);
            //为空画斜杠
            if (tbXinliZK.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(80 + x, y + 13), new Point(100 + x, y + 2));
            }

            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("手术并发症：" + tbBingfaZother.Text, textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 75, y1, x + 650, y1);


            //为空画斜杠
            if (tbBingfaZother.Text.Trim() == "")
            {
                e.Graphics.DrawLine(ptp, new Point(120 + x, y + 13), new Point(140 + x, y + 2));
            }
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawString("对病人进行健康教育的内容：", textfront, Brushes.Black, x + 0, y);

            y = y + 20; y1 = y + 15;
            if (rtbJKJY.Text.Trim() == "")
                e.Graphics.DrawLine(ptp, new Point(150 + x, y + 13), new Point(190 + x, y + 2));

            List<string> strJWS = adims_BLL.UserFunction.SplitByLen(rtbJKJY.Text, 45);

            for (int i = 0; i < strJWS.Count; i++)
            {
                e.Graphics.DrawString(strJWS[i], textfront, Brushes.Black, x + 10, y + i * 20);
                e.Graphics.DrawLine(Pens.Black, x + 0, y1 + i * 20, x + 650, y1 + i * 20);
            }

            y = y + 35 + strJWS.Count * 20; y1 = y + 10;
            e.Graphics.DrawString("患者神志：  □ 清醒    □ 嗜睡    □ 昏迷", textfront, Brushes.Black, x + 0, y);
            if (cbShenzhi1.Checked)
            {
                e.Graphics.DrawLines(pen2, new Point[] { new Point(x + 80, y), new Point(x + 85, y1), new Point(x + 95, y) });
            }
            else if (cbShenzhi2.Checked)
            {
                e.Graphics.DrawLines(pen2, new Point[] { new Point(x + 150, y), new Point(x + 155, y1), new Point(x + 165, y) });
            }
            else if (cbShenzhi3.Checked)
            {
                e.Graphics.DrawLines(pen2, new Point[] { new Point(x + 220, y), new Point(x + 225, y1), new Point(x + 235, y) });
            }

            y = y + 25; y1 = y + 10;
            e.Graphics.DrawString("精神状态：  □ 好      □ 欠佳    □ 萎靡", textfront, Brushes.Black, x + 0, y);
           
            if (cbJingshen1.Checked)
            {
                e.Graphics.DrawLines(pen2, new Point[] { new Point(x + 80, y), new Point(x + 85, y1), new Point(x + 95, y) });
            }
            else if (cbJingshen2.Checked)
            {
                e.Graphics.DrawLines(pen2, new Point[] { new Point(x + 150, y), new Point(x + 155, y1), new Point(x + 165, y) });
            }
            else if (cbJingshen3.Checked)
            {
                e.Graphics.DrawLines(pen2, new Point[] { new Point(x + 220, y), new Point(x + 225, y1), new Point(x + 235, y) });
            }

            y = y + 25; y1 = y + 10;
            e.Graphics.DrawString("切口疼痛：  □ 有      □ 无", textfront, Brushes.Black, x + 0, y);

            if (cbQiekouTT1.Checked)
            {
                e.Graphics.DrawLines(pen2, new Point[] { new Point(x + 80, y), new Point(x + 85, y1), new Point(x + 95, y) });
                // e.Graphics.DrawString("                   √", textfront, Brushes.Black, x + 0, y);
            }
            else if (cbQiekouTT2.Checked)
            {
                e.Graphics.DrawLines(pen2, new Point[] { new Point(x + 150, y), new Point(x + 155, y1), new Point(x + 165, y) });
                //e.Graphics.DrawString("                                √", textfront, Brushes.Black, x + 0, y);
            }

            y = y + 25; y1 = y + 10;
            e.Graphics.DrawString("皮肤破损灼伤：  □ 有      □ 无", textfront, Brushes.Black, x + 0, y);

            if (cbPifu1.Checked)
            {
                e.Graphics.DrawLines(pen2, new Point[] { new Point(x + 100, y), new Point(x + 105, y1), new Point(x + 115, y) });
                //e.Graphics.DrawString("                          √", textfront, Brushes.Black, x + 0, y);
            }
            else if (cbPifu2.Checked)
            {
                e.Graphics.DrawLines(pen2, new Point[] { new Point(x + 170, y), new Point(x + 175, y1), new Point(x + 185, y) });

            }


            y = y + 25; y1 = y + 10;
            e.Graphics.DrawString("病人对手术室的综合评价：    □ 满意    □ 基本满意     □ 不满意", textfront, Brushes.Black, x + 0, y);
            Point[] pointBRpinjia = { new Point(x + 180, y), new Point(x + 185, y1), new Point(x + 195, y) };
            if (this.cbBRPinjia1.Checked)
                e.Graphics.DrawLines(pen2, pointBRpinjia);
            if (this.cbBRPinjia2.Checked)
                e.Graphics.DrawLines(pen2, new Point[] { new Point(x + 250, y), new Point(x + 255, y1), new Point(x + 265, y) });
            if (this.cbBRPinjia3.Checked)
                e.Graphics.DrawLines(pen2, new Point[] { new Point(x + 350, y), new Point(x + 355, y1), new Point(x + 365, y) });
            y = y + 25; y1 = y + 10;
            e.Graphics.DrawString("护士态度：  □ 热情    □ 一般    □ 冷淡", textfront, Brushes.Black, x + 0, y);
            Point[] pointTaidu1 = { new Point(x + 80, y), new Point(x + 85, y1), new Point(x + 95, y) };
            if (this.cbTaidu1.Checked)
                e.Graphics.DrawLines(pen2, pointTaidu1);
            Point[] pointTaidu2 = { new Point(x + 150, y), new Point(x + 155, y1), new Point(x + 165, y) };
            if (cbTaidu2.Checked)
                e.Graphics.DrawLines(pen2, pointTaidu2);
            Point[] pointTaidu3 = { new Point(x + 220, y), new Point(x + 225, y1), new Point(x + 235, y) };
            if (cbTaidu3.Checked)
                e.Graphics.DrawLines(pen2, pointTaidu3);

            y = y + 25; y1 = y + 10;
            e.Graphics.DrawString("护士责任心：□ 强      □ 一般    □ 不负责", textfront, Brushes.Black, x + 0, y);
            Point[] pointZRX1 = { new Point(x + 80, y), new Point(x + 85, y1), new Point(x + 95, y) };
            if (this.cbZRxin1.Checked)
                e.Graphics.DrawLines(pen2, pointZRX1);
            Point[] pointZRX2 = { new Point(x + 150, y), new Point(x + 155, y1), new Point(x + 165, y) };
            if (cbZRxin2.Checked)
                e.Graphics.DrawLines(pen2, pointZRX2);
            Point[] pointZRX3 = { new Point(x + 220, y), new Point(x + 225, y1), new Point(x + 235, y) };
            if (cbZRxin3.Checked)
                e.Graphics.DrawLines(pen2, pointZRX3);

            y = y + 25; y1 = y + 10;
            e.Graphics.DrawString("护士的技术：□ 好      □ 一般    □ 差", textfront, Brushes.Black, x + 0, y);
            Point[] pointJishu1 = { new Point(x + 80, y), new Point(x + 85, y1), new Point(x + 95, y) };
            if (this.cbJishu1.Checked)
                e.Graphics.DrawLines(pen2, pointJishu1);
            Point[] pointJishu2 = { new Point(x + 150, y), new Point(x + 155, y1), new Point(x + 165, y) };
            if (cbJishu2.Checked)
                e.Graphics.DrawLines(pen2, pointJishu2);
            Point[] pointJishu3 = { new Point(x + 220, y), new Point(x + 225, y1), new Point(x + 235, y) };
            if (cbJishu3.Checked)
                e.Graphics.DrawLines(pen2, pointJishu3);


            y = y + 25; y1 = y + 10;
            e.Graphics.DrawString("护士与您的沟通：  □ 好       □ 一般       □ 无", textfront, Brushes.Black, x + 0, y);

            if (cbGoutong1.Checked)
            {
                Point[] pointGT = { new Point(x + 120, y), new Point(x + 125, y1), new Point(x + 135, y) };
                e.Graphics.DrawLines(pen2, pointGT);
            }
            else if (cbGoutong2.Checked)
            {
                Point[] pointGT = { new Point(x + 190, y), new Point(x + 195, y1), new Point(x + 205, y) };
                e.Graphics.DrawLines(pen2, pointGT);
            }
            else if (cbGoutong3.Checked)
            {
                Point[] pointGT = { new Point(x + 280, y), new Point(x + 285, y1), new Point(x + 295, y) };
                e.Graphics.DrawLines(pen2, pointGT);
            }

            y = y + 25; y1 = y + 10;
            e.Graphics.DrawString("护士是否谈论与手术无关话题：  □ 有       □ 无", textfront, Brushes.Black, x + 0, y);

            if (cbWuguanHuati1.Checked)
            {
                Point[] pointGT = { new Point(x + 195, y), new Point(x + 200, y1), new Point(x + 210, y) };
                e.Graphics.DrawLines(pen2, pointGT);
            }
            else if (cbWuguanHuati2.Checked)
            {
                Point[] pointGT = { new Point(x + 270, y), new Point(x + 275, y1), new Point(x + 285, y) };
                e.Graphics.DrawLines(pen2, pointGT);
            }



            y = y + 25; y1 = y + 8;
            e.Graphics.DrawString("手术室的环境是否整洁、安静、安全：    □ 好      □ 一般    □ 差", textfront, Brushes.Black, x + 0, y);
            Point[] pointWS1 = { new Point(x + 240, y), new Point(x + 245, y1), new Point(x + 255, y) };
            if (this.cbJishu1.Checked)
                e.Graphics.DrawLines(pen2, pointWS1);
            Point[] pointWS2 = { new Point(x + 310, y), new Point(x + 315, y1), new Point(x + 325, y) };
            if (this.cbJishu1.Checked)
                e.Graphics.DrawLines(pen2, pointWS2);
            Point[] pointWS3 = { new Point(x + 380, y), new Point(x + 385, y1), new Point(x + 395, y) };
            if (this.cbJishu1.Checked)
                e.Graphics.DrawLines(pen2, pointWS3);

            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("其他意见和建议：", textfront, Brushes.Black, x + 0, y);
            y = y + 20; y1 = y + 15;
            if (rtbYijian.Text.Trim() == "")
                e.Graphics.DrawLine(ptp, new Point(150 + x, y + 13), new Point(190 + x, y + 2));

            List<string> strYijian = adims_BLL.UserFunction.SplitByLen(rtbYijian.Text, 45);


            for (int i = 0; i < strYijian.Count; i++)
            {
                e.Graphics.DrawString(strYijian[i], textfront, Brushes.Black, x + 10, y + i * 20);
                e.Graphics.DrawLine(Pens.Black, x + 0, y1 + i * 20, x + 650, y1 + i * 20);
            }



            y = y + 30 + strYijian.Count * 20; y1 = y + 15;
            e.Graphics.DrawString("病区责任护士签名：" + cmbZerenhushi.Text, textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 110, y1, x + 250, y1);
            //为空画斜杠
            //if (cmbZerenhushi.Text == "")
            //    e.Graphics.DrawLine(ptp, new Point(130 + x, y + 13), new Point(150 + x, y + 2));
            e.Graphics.DrawString("术后访视者签名：" + cmbVistor.Text, textfront, Brushes.Black, x + 270, y);
            e.Graphics.DrawLine(Pens.Black, x + 370, y1, x + 470, y1);
            //为空画斜杠
            //if (cmbZJAQWS.Text == "")
            //    e.Graphics.DrawLine(ptp, new Point(390 + x, y + 13), new Point(410 + x, y + 2));
            e.Graphics.DrawString("访视日期：" + dtVisitDate.Text, textfront, Brushes.Black, x + 490, y);
            e.Graphics.DrawLine(Pens.Black, x + 550, y1, x + 650, y1);



        }


        private void exitStripMenuItem_Click(object sender, EventArgs e)
        {

            if (BCCount > 0)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.AfterVisit_SZ_FormClosing);
                this.Close();
            }
            else
            {
                if (MessageBox.Show("保存退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SAVE();
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.AfterVisit_SZ_FormClosing);
                    this.Close();
                }
                else
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.AfterVisit_SZ_FormClosing);
                    this.Close();
                }

            }
        }

        private void cunDangStripMenuItem_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = DAL.GetAfterVisitCount_SZ(PATID);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("访视信息需要先保存到数据库，才能存档！");
            }
            else
            {
                result = DAL.UpdateAdims_AfterVisit_SZ_STATE();
                if (result > 0)
                {
                    MessageBox.Show("存档成功！");
                    SaveToolStripMenuItem.Enabled = false;
                }
            }
        }

        private void AfterVisit_SZ_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (BCCount > 0)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.AfterVisit_SZ_FormClosing);
                this.Close();
            }
            else
            {
                if (MessageBox.Show("保存退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SAVE();
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.AfterVisit_SZ_FormClosing);
                    this.Close();
                }
                else
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.AfterVisit_SZ_FormClosing);
                    this.Close();
                }

            }
        }
    }
}
