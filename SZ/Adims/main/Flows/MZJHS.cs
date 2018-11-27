using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace main
{
    public partial class MZJHS : Form
    {
        string patID;
        public bool SaveDoc = false;
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        public MZJHS(string patid)
        {patID = patid;
            InitializeComponent();
        }

        private void BindMzjhsInfo()
        {
            DataTable dt = DAL.GetMZJHS(patID);
            if (dt.Rows.Count > 0)
            {
                cmbASA.Text = dt.Rows[0]["ASA"].ToString();
                tbNXMZFF.Text = dt.Rows[0]["NXMZFF"].ToString();
                tbSJMZFF.Text = dt.Rows[0]["SJMZFF"].ToString();
                tbMZFFBGYY.Text = dt.Rows[0]["MZFFBGYY"].ToString();
                tbZGNMZCCD.Text = dt.Rows[0]["ZGNMZCCD"].ToString();
                string valueStr = dt.Rows[0]["JHXM"].ToString();
                if (valueStr.Contains("0")) cbJHXM0.Checked = true;
                if (valueStr.Contains("1")) cbJHXM1.Checked = true;
                if (valueStr.Contains("2")) cbJHXM2.Checked = true;
                if (valueStr.Contains("3")) cbJHXM3.Checked = true;
                if (valueStr.Contains("4")) cbJHXM4.Checked = true;
                if (valueStr.Contains("5")) cbJHXM5.Checked = true;
                if (valueStr.Contains("6")) cbJHXM6.Checked = true;
                if (valueStr.Contains("7")) cbJHXM7.Checked = true;
                if (valueStr.Contains("8")) cbJHXM8.Checked = true;
                if (valueStr.Contains("9")) cbJHXM9.Checked = true;
                if (valueStr.Contains("A")) cbJHXM_A.Checked = true;


                valueStr = dt.Rows[0]["TSCZ"].ToString();
                if (valueStr.Contains("0")) cbTSCZ0.Checked = true;
                if (valueStr.Contains("1")) cbTSCZ1.Checked = true;
                if (valueStr.Contains("2")) cbTSCZ2.Checked = true;
                if (valueStr.Contains("3")) cbTSCZ3.Checked = true;
                if (valueStr.Contains("4")) cbTSCZ4.Checked = true;
                if (valueStr.Contains("5")) cbTSCZ5.Checked = true;
                if (valueStr.Contains("6")) cbTSCZ6.Checked = true;
                if (valueStr.Contains("7")) cbTSCZ7.Checked = true;
                if (valueStr.Contains("8")) cbTSCZ8.Checked = true;
                if (valueStr.Contains("9")) cbTSCZ9.Checked = true;
                if (valueStr.Contains("A")) cbTSCZ_A.Checked = true;
                if (valueStr.Contains("B")) cbTSCZ_B.Checked = true;
                if (valueStr.Contains("C")) cbTSCZ_C.Checked = true;
                if (valueStr.Contains("D")) cbTSCZ_D.Checked = true;
                if (valueStr.Contains("E")) cbTSCZ_D.Checked = true;
                if (valueStr.Contains("F")) cbTSCZ_E.Checked = true;
               
                valueStr = dt.Rows[0]["MZQX"].ToString();
                if (valueStr.Contains("0")) cbMZQX0.Checked = true;
                if (valueStr.Contains("1")) cbMZQX1.Checked = true;
                if (valueStr.Contains("2")) cbMZQX2.Checked = true;
                if (valueStr.Contains("3")) cbMZQX3.Checked = true;
                if (valueStr.Contains("4")) cbMZQX4.Checked = true;
                if (valueStr.Contains("5")) cbMZQX5.Checked = true;

                valueStr = dt.Rows[0]["JMY"].ToString();
                if (valueStr.Contains("0")) cbJMY0.Checked = true;
                if (valueStr.Contains("1")) cbJMY1.Checked = true;
                if (valueStr.Contains("2")) cbJMY2.Checked = true;
                if (valueStr.Contains("3")) cbJMY3.Checked = true;


                valueStr = dt.Rows[0]["ZJY"].ToString();
                if (valueStr.Contains("0")) cbZJY0.Checked = true;
                if (valueStr.Contains("1")) cbZJY1.Checked = true;
                if (valueStr.Contains("2")) cbZJY2.Checked = true;

                valueStr = dt.Rows[0]["ZTY"].ToString();
                if (valueStr.Contains("0")) cbZTY0.Checked = true;
                if (valueStr.Contains("1")) cbZTY1.Checked = true;
                if (valueStr.Contains("2")) cbZTY2.Checked = true;
                if (valueStr.Contains("3")) cbZTY3.Checked = true;
                if (valueStr.Contains("4")) cbZTY4.Checked = true;

                valueStr = dt.Rows[0]["FZYY"].ToString();
                if (valueStr.Contains("0")) cbFZYY0.Checked = true;
                if (valueStr.Contains("1")) cbFZYY1.Checked = true;
                if (valueStr.Contains("2")) cbFZYY2.Checked = true;
                if (valueStr.Contains("3")) cbFZYY3.Checked = true;
                if (valueStr.Contains("4")) cbFZYY4.Checked = true;
                if (valueStr.Contains("5")) cbFZYY5.Checked = true;
                if (valueStr.Contains("6")) cbFZYY6.Checked = true;
                if (valueStr.Contains("7")) cbFZYY7.Checked = true;
                if (valueStr.Contains("8")) cbFZYY8.Checked = true;
                if (valueStr.Contains("9")) cbFZYY9.Checked = true;
                if (valueStr.Contains("A")) cbFZYY_A.Checked = true;
                if (valueStr.Contains("B")) cbFZYY_B.Checked = true;
                if (valueStr.Contains("C")) cbFZYY_C.Checked = true;


                valueStr = dt.Rows[0]["JJYP"].ToString();
                if (valueStr.Contains("0")) cbJJYP0.Checked = true;
                if (valueStr.Contains("1")) cbJJYP1.Checked = true;
                if (valueStr.Contains("2")) cbJJYP2.Checked = true;
                if (valueStr.Contains("3")) cbJJYP3.Checked = true;
                if (valueStr.Contains("4")) cbJJYP4.Checked = true;
                if (valueStr.Contains("5")) cbJJYP5.Checked = true;
                if (valueStr.Contains("6")) cbJJYP6.Checked = true;
                if (valueStr.Contains("7")) cbJJYP7.Checked = true;
                if (valueStr.Contains("8")) cbJJYP8.Checked = true;
                if (valueStr.Contains("9")) cbJJYP9.Checked = true;
                if (valueStr.Contains("A")) cbJJYP_A.Checked = true;
                if (valueStr.Contains("B")) cbJJYP_B.Checked = true;
                if (valueStr.Contains("C")) cbJJYP_C.Checked = true;
                if (valueStr.Contains("D")) cbJJYP_D.Checked = true;
                if (valueStr.Contains("E")) cbJJYP_E.Checked = true;
                if (valueStr.Contains("F")) cbJJYP_F.Checked = true;

                valueStr = dt.Rows[0]["Shuye"].ToString();
                if (valueStr.Contains("0")) cbShuYe0.Checked = true;
                if (valueStr.Contains("1")) cbShuYe1.Checked = true;
                if (valueStr.Contains("2")) cbShuYe2.Checked = true;
                if (valueStr.Contains("3")) cbShuYe3.Checked = true;

                tbHongxibao.Text = dt.Rows[0]["Hongxibao"].ToString();
                tbXuejiang.Text = dt.Rows[0]["Xuejiang"].ToString();
                cmbMZYS.Text = dt.Rows[0]["mzys"].ToString();
                dtRecordTime.Value = Convert.ToDateTime(dt.Rows[0]["RecordTime"].ToString());
                tbWentiduice.Text = dt.Rows[0]["Wentiduice"].ToString();
                if (Convert.ToString(dt.Rows[0]["IsRead"]) != "")
                {
                    if (Convert.ToInt32(dt.Rows[0]["IsRead"]) == 1)
                    {
                        SaveToolStripMenuItem.Enabled = false;
                        SaveDoc = true;
                    }
                }
            }
        }
        private void BindPatInfo()
        {
            DataTable dt = new DataTable();
            dt = DAL.GetALLPAIBAN(patID);

            tbPatname.Text = dt.Rows[0]["Patname"].ToString();
            tbAge.Text = dt.Rows[0]["Patage"].ToString();
            cmbSex.Text = dt.Rows[0]["Patsex"].ToString();
            cmbAgeDW.Text = dt.Rows[0]["ageDW"].ToString();
            if ( cmbAgeDW.Text=="")
            {
                 cmbAgeDW.Text = "岁";
            }
            tbKeshi.Text = dt.Rows[0]["patdpm"].ToString();
            tbBedNo.Text = dt.Rows[0]["Patbedno"].ToString();
            tbZhuyuanID.Text = dt.Rows[0]["Patzhuyuanid"].ToString();
            tbSQZD.Text = dt.Rows[0]["pattmd"].ToString();
            tbNSSSS.Text = dt.Rows[0]["Oname"].ToString();
        }
        private void MZZJ_SZ_Load(object sender, EventArgs e)
        {
           
            BindPatInfo();

            cmbMZYS.Items.Clear();
            DataTable dt1 = bll.selectUserName(1);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                cmbMZYS.Items.Add(dt1.Rows[i][0].ToString());

            }
            BindMzjhsInfo();
            
        }
        int BCCount = 0;
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();

        }
        private void Save()
        {
            string StrValue = string.Empty;
            Dictionary<string, string> zongjiedan = new Dictionary<string, string>();
            int result = 0;
            try
            {
                zongjiedan.Add("ASA", cmbASA.Text);
                zongjiedan.Add("NXMZFF", tbNXMZFF.Text);
                zongjiedan.Add("SJMZFF", tbSJMZFF.Text);
                zongjiedan.Add("MZFFBGYY", tbMZFFBGYY.Text);
                StrValue = string.Empty;
                if (cbJHXM0.Checked == true) StrValue += "0";
                if (cbJHXM1.Checked == true) StrValue += "1";
                if (cbJHXM2.Checked == true) StrValue += "2";
                if (cbJHXM3.Checked == true) StrValue += "3";
                if (cbJHXM4.Checked == true) StrValue += "4";
                if (cbJHXM5.Checked == true) StrValue += "5";
                if (cbJHXM6.Checked == true) StrValue += "6";
                if (cbJHXM7.Checked == true) StrValue += "7";
                if (cbJHXM8.Checked == true) StrValue += "8";
                if (cbJHXM9.Checked == true) StrValue += "9";
                if (cbJHXM_A.Checked == true) StrValue += "A";
                zongjiedan.Add("jhxm", StrValue);
                zongjiedan.Add("ZGNMZCCD", tbZGNMZCCD.Text);
               

                StrValue = string.Empty;
                if (cbTSCZ0.Checked == true) StrValue += "0";
                if (cbTSCZ1.Checked == true) StrValue += "1";
                if (cbTSCZ2.Checked == true) StrValue += "2";
                if (cbTSCZ3.Checked == true) StrValue += "3";
                if (cbTSCZ4.Checked == true) StrValue += "4";
                if (cbTSCZ5.Checked == true) StrValue += "5";
                if (cbTSCZ6.Checked == true) StrValue += "6";
                if (cbTSCZ7.Checked == true) StrValue += "7";
                if (cbTSCZ8.Checked == true) StrValue += "8";
                if (cbTSCZ9.Checked == true) StrValue += "9";
                if (cbTSCZ_A.Checked == true) StrValue += "A";
                if (cbTSCZ_B.Checked == true) StrValue += "B";
                if (cbTSCZ_C.Checked == true) StrValue += "C";
                if (cbTSCZ_D.Checked == true) StrValue += "D";
                if (cbTSCZ_D.Checked == true) StrValue += "E";
                if (cbTSCZ_E.Checked == true) StrValue += "F";
                zongjiedan.Add("tscz", StrValue);

                StrValue = string.Empty;
                if (cbMZQX0.Checked == true) StrValue += "0";
                if (cbMZQX1.Checked == true) StrValue += "1";
                if (cbMZQX2.Checked == true) StrValue += "2";
                if (cbMZQX3.Checked == true) StrValue += "3";
                if (cbMZQX4.Checked == true) StrValue += "4";
                if (cbMZQX5.Checked == true) StrValue += "5";
                zongjiedan.Add("MZQX", StrValue);

                StrValue = string.Empty;
                if (cbJMY0.Checked == true) StrValue += "0";
                if (cbJMY1.Checked == true) StrValue += "1";
                if (cbJMY2.Checked == true) StrValue += "2";
                if (cbJMY3.Checked == true) StrValue += "3";
                zongjiedan.Add("JMY", StrValue);

                StrValue = string.Empty;
                if (cbZJY0.Checked == true) StrValue += "0";
                if (cbZJY1.Checked == true) StrValue += "1";
                if (cbZJY2.Checked == true) StrValue += "2";
                zongjiedan.Add("ZJY", StrValue);

                StrValue = string.Empty;
                if (cbZTY0.Checked == true) StrValue += "0";
                if (cbZTY1.Checked == true) StrValue += "1";
                if (cbZTY2.Checked == true) StrValue += "2";
                if (cbZTY3.Checked == true) StrValue += "3";
                if (cbZTY4.Checked == true) StrValue += "4";
                zongjiedan.Add("ZTY", StrValue);

                StrValue = string.Empty;
                if (cbFZYY0.Checked == true) StrValue += "0";
                if (cbFZYY1.Checked == true) StrValue += "1";
                if (cbFZYY2.Checked == true) StrValue += "2";
                if (cbFZYY3.Checked == true) StrValue += "3";
                if (cbFZYY4.Checked == true) StrValue += "4";
                if (cbFZYY5.Checked == true) StrValue += "5";
                if (cbFZYY6.Checked == true) StrValue += "6";
                if (cbFZYY7.Checked == true) StrValue += "7";
                if (cbFZYY8.Checked == true) StrValue += "8";
                if (cbFZYY9.Checked == true) StrValue += "9";
                if (cbFZYY_A.Checked == true) StrValue += "A";
                if (cbFZYY_B.Checked == true) StrValue += "B";
                if (cbFZYY_C.Checked == true) StrValue += "C";
                zongjiedan.Add("FZYY", StrValue);

                StrValue = string.Empty;
                if (cbJJYP0.Checked == true) StrValue += "0";
                if (cbJJYP1.Checked == true) StrValue += "1";
                if (cbJJYP2.Checked == true) StrValue += "2";
                if (cbJJYP3.Checked == true) StrValue += "3";
                if (cbJJYP4.Checked == true) StrValue += "4";
                if (cbJJYP5.Checked == true) StrValue += "5";
                if (cbJJYP6.Checked == true) StrValue += "6";
                if (cbJJYP7.Checked == true) StrValue += "7";
                if (cbJJYP8.Checked == true) StrValue += "8";
                if (cbJJYP9.Checked == true) StrValue += "9";
                if (cbJJYP_A.Checked == true) StrValue += "A";
                if (cbJJYP_B.Checked == true) StrValue += "B";
                if (cbJJYP_C.Checked == true) StrValue += "C";
                if (cbJJYP_D.Checked == true) StrValue += "D";
                if (cbJJYP_E.Checked == true) StrValue += "E";
                if (cbJJYP_F.Checked == true) StrValue += "F";
                zongjiedan.Add("JJYP", StrValue);

                StrValue = string.Empty;
                if (cbShuYe0.Checked == true) StrValue += "0";
                if (cbShuYe1.Checked == true) StrValue += "1";
                if (cbShuYe2.Checked == true) StrValue += "2";
                if (cbShuYe3.Checked == true) StrValue += "3";
                zongjiedan.Add("shuye", StrValue);
                zongjiedan.Add("Hongxibao", tbHongxibao.Text);
                zongjiedan.Add("Xuejiang", tbXuejiang.Text);
                zongjiedan.Add("Wentiduice", tbWentiduice.Text);
                zongjiedan.Add("mzys", cmbMZYS.Text);
                zongjiedan.Add("RecordTime", dtRecordTime.Value.ToString());
                zongjiedan.Add("patid", patID);
                DataTable dt = DAL.GetMZJHS(patID);
                if (dt.Rows.Count > 0)
                    result = DAL.UpdateMZJHS(zongjiedan);
                else
                    result = DAL.InsertMZJHS(zongjiedan);
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

        private void cunDangStripMenuItem_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = DAL.GetMZJHS(patID);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("信息需要先保存到数据库，才能存档！");
            }
            else
            {
                result = DAL.UpdateMZJHS_state(patID);
                if (result > 0)
                {
                    MessageBox.Show("存档成功！");
                    SaveToolStripMenuItem.Enabled = false;
                }
            }
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("16K", 737, 1020);

        }

        private void printStripMenuItem_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("16K", 737, 1020);
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Pen ptp = Pens.Black;//普通画笔
            Font FroYH = new Font(new FontFamily("微软雅黑"), 7);
            Font textfront = new Font(new FontFamily("宋体"), 8);
            Font textfront1 = new Font(new FontFamily("宋体"), 10);
            Font tagfont = new Font(new FontFamily("宋体"), 13);
            Font tagfont1 = new Font(new FontFamily("宋体"), 12);
            Pen pblack = Pens.Black;

            int x = 30, y = 30, y1 = y + 18;
            string title1 = "江  苏  盛  泽  医  院";
            string title2 = "江苏省人民医院盛泽分院";
            string title3 = "    麻醉计划书";
            y = y + 30; y1 = y + 18;
            e.Graphics.DrawString(title1, tagfont1, Brushes.Black, x + 230, y);
            y = y + 30; y1 = y + 18;
            e.Graphics.DrawString(title2, tagfont1, Brushes.Black, x + 230, y);
            y = y + 30; y1 = y + 18;
            e.Graphics.DrawString(title3, tagfont, Brushes.Black, x + 230, y);


            y = y + 40; y1 = y + 13;
            e.Graphics.DrawString("姓名 " + tbPatname.Text, textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 25, y1, x + 110, y1);
            //为空画斜杠
            if (tbPatname.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(60 + x, y + 13), new Point(80 + x, y + 2));
            }
            e.Graphics.DrawString("性别 " + cmbSex.Text, textfront, Brushes.Black, x + 120, y);
            e.Graphics.DrawLine(Pens.Black, x + 145, y1, x + 230, y1);
            //为空画斜杠
            if (cmbSex.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(155 + x, y + 13), new Point(175 + x, y + 2));
            }
            e.Graphics.DrawString("年龄 " + tbAge.Text + " " + cmbAgeDW.Text, textfront, Brushes.Black, x + 240, y);
            e.Graphics.DrawLine(Pens.Black, x + 265, y1, x + 310, y1);
            //为空画斜杠
            if (tbAge.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(275 + x, y + 13), new Point(295 + x, y + 2));
            }
            e.Graphics.DrawString("床号：" + this.tbBedNo.Text, textfront, Brushes.Black, x + 330, y);
            e.Graphics.DrawLine(Pens.Black, x + 355, y1, x + 440, y1);
            e.Graphics.DrawString("住院号：" + this.tbZhuyuanID.Text, textfront, Brushes.Black, x + 460, y);
            e.Graphics.DrawLine(Pens.Black, x + 495, y1, x + 660, y1);

            y = y + 30; y1 = y + 13;
            e.Graphics.DrawString("术前诊断：" + this.tbSQZD.Text, textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 55, y1, x + 300, y1);
            e.Graphics.DrawString("拟施手术：" + this.tbNSSSS.Text, textfront, Brushes.Black, x + 320, y);
            e.Graphics.DrawLine(Pens.Black, x + 370, y1, x + 660, y1);

            y = y + 30; y1 = y + 13;
            e.Graphics.DrawString("ASA分级：" + this.cmbASA.Text, textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 45, y1, x + 100, y1);
            e.Graphics.DrawString("拟行麻醉方法：" + this.tbNXMZFF.Text, textfront, Brushes.Black, x + 120, y);
            e.Graphics.DrawLine(Pens.Black, x + 190, y1, x + 350, y1);
            e.Graphics.DrawString("实际麻醉方法：" + this.tbSJMZFF.Text, textfront, Brushes.Black, x + 370, y);
            e.Graphics.DrawLine(Pens.Black, x + 440, y1, x + 660, y1);

            y = y + 25; y1 = y + 13;
            e.Graphics.DrawString("麻醉方法变更原因：" + this.tbMZFFBGYY.Text, textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 100, y1, x + 660, y1);

            y = y + 25; y1 = y + 13;           
            e.Graphics.DrawString("监护项目： □心电图  □无创血压  □脉搏氧饱和度  □呼气末二氧化碳分压  □连续有创血压 ", textfront, Brushes.Black, x + 0, y);
            if (cbJHXM0.Checked)
                e.Graphics.DrawString("           ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJHXM1.Checked)
                e.Graphics.DrawString("                     ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJHXM2.Checked)
                e.Graphics.DrawString("                                 ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJHXM3.Checked)
                e.Graphics.DrawString("                                                ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJHXM4.Checked)
                e.Graphics.DrawString("                                                                     ✔", textfront, Brushes.Black, x + 0, y);
            
            y = y + 25; y1 = y + 13;
           e.Graphics.DrawString("           □肌松监测 □中心静脉压  □BIS　□体温　□血糖　□动脉血气分析 ", textfront, Brushes.Black, x + 0, y);
            if (cbJHXM5.Checked)
                e.Graphics.DrawString("           ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJHXM6.Checked)
                e.Graphics.DrawString("                      ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJHXM7.Checked)
                e.Graphics.DrawString("                                    ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJHXM8.Checked)
                e.Graphics.DrawString("                                           ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJHXM9.Checked)
                e.Graphics.DrawString("                                                   ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJHXM_A.Checked)
                e.Graphics.DrawString("                                                           ✔", textfront, Brushes.Black, x + 0, y);


            y = y + 25; y1 = y + 13;
            e.Graphics.DrawString("椎管内麻醉穿刺点：" + this.tbZGNMZCCD.Text, textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 100, y1, x + 660, y1);

            y = y + 25; y1 = y + 13;
            e.Graphics.DrawString("特殊操作： □颈内静脉穿刺置管 □动脉穿刺置管 □锁骨下静脉穿刺置管 □股静脉穿刺置管  □双腔气管插管 □术中自体血回收", textfront, Brushes.Black, x + 0, y);
            if (this.cbMZQX0.Checked)
                e.Graphics.DrawString("           ✔", textfront, Brushes.Black, x + 0, y);
            if (cbMZQX1.Checked)
                e.Graphics.DrawString("                              ✔", textfront, Brushes.Black, x + 0, y);
            if (cbMZQX2.Checked)
                e.Graphics.DrawString("                                            ✔", textfront, Brushes.Black, x + 0, y);
            if (cbMZQX3.Checked)
                e.Graphics.DrawString("                                                                 ✔", textfront, Brushes.Black, x + 0, y);
            if (cbMZQX4.Checked)
                e.Graphics.DrawString("                                                                                   ✔", textfront, Brushes.Black, x + 0, y);
            if (cbMZQX5.Checked)
                e.Graphics.DrawString("                                                                                                  ✔", textfront, Brushes.Black, x + 0, y);

            y = y + 25; y1 = y + 13;
            e.Graphics.DrawString("麻醉器械： □麻醉机 □监护仪 □螺纹管 □面罩 □硬膜外穿刺包 □腰硬联合穿刺包  □深静脉穿刺包", textfront, Brushes.Black, x + 0, y);
            if (this.cbTSCZ0.Checked)
                e.Graphics.DrawString("           ✔", textfront, Brushes.Black, x + 0, y);
            if (cbTSCZ1.Checked)
                e.Graphics.DrawString("                    ✔", textfront, Brushes.Black, x + 0, y);
            if (cbTSCZ2.Checked)
                e.Graphics.DrawString("                             ✔", textfront, Brushes.Black, x + 0, y);
            if (cbTSCZ3.Checked)
                e.Graphics.DrawString("                                      ✔", textfront, Brushes.Black, x + 0, y);
            if (cbTSCZ4.Checked)
                e.Graphics.DrawString("                                            ✔", textfront, Brushes.Black, x + 0, y);
            if (cbTSCZ5.Checked)
                e.Graphics.DrawString("                                                          ✔", textfront, Brushes.Black, x + 0, y);
            if (cbTSCZ6.Checked)
                e.Graphics.DrawString("                                                                            ✔", textfront, Brushes.Black, x + 0, y);
           
            y = y + 25; y1 = y + 13;
            e.Graphics.DrawString("           □喉镜 □吸痰用具 □气管导管 □喉罩 □压力传感器 □可视喉镜 □可视光棒 □镇痛泵  □动脉穿刺针", textfront, Brushes.Black, x + 0, y);
            if (cbTSCZ7.Checked)
                e.Graphics.DrawString("           ✔", textfront, Brushes.Black, x + 0, y);
            if (cbTSCZ8.Checked)
                e.Graphics.DrawString("                  ✔", textfront, Brushes.Black, x + 0, y);
            if (cbTSCZ9.Checked)
                e.Graphics.DrawString("                             ✔", textfront, Brushes.Black, x + 0, y);
            if (cbTSCZ_A.Checked)
                e.Graphics.DrawString("                                        ✔", textfront, Brushes.Black, x + 0, y);
            if (cbTSCZ_B.Checked)
                e.Graphics.DrawString("                                               ✔", textfront, Brushes.Black, x + 0, y);
            if (cbTSCZ_C.Checked)
                e.Graphics.DrawString("                                                           ✔", textfront, Brushes.Black, x + 0, y);
            if (cbTSCZ_D.Checked)
                e.Graphics.DrawString("                                                                      ✔", textfront, Brushes.Black, x + 0, y);
            if (cbTSCZ_D.Checked)
                e.Graphics.DrawString("                                                                                ✔", textfront, Brushes.Black, x + 0, y);
            if (cbTSCZ_E.Checked)
                e.Graphics.DrawString("                                                                                          ✔", textfront, Brushes.Black, x + 0, y);

            y = y + 25; y1 = y + 13;
            e.Graphics.DrawString("局麻药：  □利多卡因　□布比卡因　□罗哌卡因　□左布比卡因", textfront, Brushes.Black, x + 0, y);
            if (this.cbJMY0.Checked)
                e.Graphics.DrawString("          ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJMY1.Checked)
                e.Graphics.DrawString("                      ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJMY2.Checked)
                e.Graphics.DrawString("                                  ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJMY3.Checked)
                e.Graphics.DrawString("                                              ✔", textfront, Brushes.Black, x + 0, y);

            y = y + 25; y1 = y + 13;
            e.Graphics.DrawString("镇静药：　□咪达唑仑　□右美托咪定　□丙泊酚", textfront, Brushes.Black, x + 0, y);
            if (this.cbZJY0.Checked)
                e.Graphics.DrawString("          ✔", textfront, Brushes.Black, x + 0, y);
            if (cbZJY1.Checked)
                e.Graphics.DrawString("                      ✔", textfront, Brushes.Black, x + 0, y);
            if (cbZJY2.Checked)
                e.Graphics.DrawString("                                    ✔", textfront, Brushes.Black, x + 0, y);

            y = y + 25; y1 = y + 13;
            e.Graphics.DrawString("镇痛药：　□芬太尼　□瑞芬太尼　□吗啡　□哌替啶　□氯胺酮", textfront, Brushes.Black, x + 0, y);
            if (this.cbZTY0.Checked)
                e.Graphics.DrawString("          ✔", textfront, Brushes.Black, x + 0, y);
            if (cbZTY1.Checked)
                e.Graphics.DrawString("                    ✔", textfront, Brushes.Black, x + 0, y);
            if (cbZTY2.Checked)
                e.Graphics.DrawString("                                ✔", textfront, Brushes.Black, x + 0, y);
            if (cbZTY3.Checked)
                e.Graphics.DrawString("                                        ✔", textfront, Brushes.Black, x + 0, y);
            if (cbZTY4.Checked)
                e.Graphics.DrawString("                                                  ✔", textfront, Brushes.Black, x + 0, y);

            y = y + 25; y1 = y + 13;
            e.Graphics.DrawString("辅助用药： □地佐辛　□特耐　□曲马多　□阿扎司琼　□长托宁　□肝素　□氟哌利多　□新斯的明", textfront, Brushes.Black, x + 0, y);
            if (this.cbFZYY0.Checked)
                e.Graphics.DrawString("           ✔", textfront, Brushes.Black, x + 0, y);
            if (cbFZYY1.Checked)
                e.Graphics.DrawString("                     ✔", textfront, Brushes.Black, x + 0, y);
            if (cbFZYY2.Checked)
                e.Graphics.DrawString("                             ✔", textfront, Brushes.Black, x + 0, y);
            if (cbFZYY3.Checked)
                e.Graphics.DrawString("                                       ✔", textfront, Brushes.Black, x + 0, y);
            if (cbFZYY4.Checked)
                e.Graphics.DrawString("                                                   ✔", textfront, Brushes.Black, x + 0, y);
            if (cbFZYY5.Checked)
                e.Graphics.DrawString("                                                             ✔", textfront, Brushes.Black, x + 0, y);
            if (cbFZYY6.Checked)
                e.Graphics.DrawString("                                                                     ✔", textfront, Brushes.Black, x + 0, y);
            if (this.cbFZYY7.Checked)
                e.Graphics.DrawString("                                                                                 ✔", textfront, Brushes.Black, x + 0, y);
            
            y = y + 25; y1 = y + 13;
            e.Graphics.DrawString("           □纳洛酮　□凯纷　□氟马西尼　□帕洛诺司琼  □血凝酶", textfront, Brushes.Black, x + 0, y);
            if (cbFZYY8.Checked)
                e.Graphics.DrawString("           ✔", textfront, Brushes.Black, x + 0, y);
            if (cbFZYY9.Checked)
                e.Graphics.DrawString("                     ✔", textfront, Brushes.Black, x + 0, y);
            if (cbFZYY_A.Checked)
                e.Graphics.DrawString("                             ✔", textfront, Brushes.Black, x + 0, y);
            if (cbFZYY_B.Checked)
                e.Graphics.DrawString("                                         ✔", textfront, Brushes.Black, x + 0, y);
            if (cbFZYY_C.Checked)
                e.Graphics.DrawString("                                                       ✔", textfront, Brushes.Black, x + 0, y);



            y = y + 25; y1 = y + 13;
            e.Graphics.DrawString("急救药品： □麻黄素　□阿托品　□多巴胺　□间羟胺　□去氧肾上腺素　□肾上腺素　□葡萄糖酸钙", textfront, Brushes.Black, x + 0, y);
            if (this.cbJJYP0.Checked)
                e.Graphics.DrawString("           ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJJYP1.Checked)
                e.Graphics.DrawString("                     ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJJYP2.Checked)
                e.Graphics.DrawString("                               ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJJYP3.Checked)
                e.Graphics.DrawString("                                         ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJJYP4.Checked)
                e.Graphics.DrawString("                                                   ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJJYP5.Checked)
                e.Graphics.DrawString("                                                                   ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJJYP6.Checked)
                e.Graphics.DrawString("                                                                               ✔", textfront, Brushes.Black, x + 0, y);


            y = y + 25; y1 = y + 13;
            e.Graphics.DrawString("          □硝酸甘油 □艾司洛尔　□胺碘酮　□地塞米松　□氨茶碱　□米力农 □速尿 □尼卡地平  □去甲肾上腺素", textfront, Brushes.Black, x + 0, y);
            if (this.cbJJYP7.Checked)
                e.Graphics.DrawString("          ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJJYP8.Checked)
                e.Graphics.DrawString("                     ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJJYP9.Checked)
                e.Graphics.DrawString("                                 ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJJYP_A.Checked)
                e.Graphics.DrawString("                                           ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJJYP_B.Checked)
                e.Graphics.DrawString("                                                       ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJJYP_C.Checked)
                e.Graphics.DrawString("                                                                 ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJJYP_D.Checked)
                e.Graphics.DrawString("                                                                          ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJJYP_E.Checked)
                e.Graphics.DrawString("                                                                                 ✔", textfront, Brushes.Black, x + 0, y);
            if (cbJJYP_F.Checked)
                e.Graphics.DrawString("                                                                                             ✔", textfront, Brushes.Black, x + 0, y);

            y = y + 25; y1 = y + 13;
            e.Graphics.DrawString("输液：　　□乳酸钠林格氏液　□代血浆　□生理盐水　□电解质", textfront, Brushes.Black, x + 0, y);
            if (this.cbShuYe0.Checked)
                e.Graphics.DrawString("          ✔", textfront, Brushes.Black, x + 0, y);
            if (cbShuYe1.Checked)
                e.Graphics.DrawString("                            ✔", textfront, Brushes.Black, x + 0, y);
            if (cbShuYe2.Checked)
                e.Graphics.DrawString("                                      ✔", textfront, Brushes.Black, x + 0, y);
            if (cbShuYe3.Checked)
                e.Graphics.DrawString("                                                  ✔", textfront, Brushes.Black, x + 0, y);

            y = y + 25; y1 = y + 13;
            e.Graphics.DrawString("备血：　　红细胞 " + this.tbHongxibao.Text, textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 100, y1, x + 160, y1);
            e.Graphics.DrawString("U，血浆 " + this.tbXuejiang.Text, textfront, Brushes.Black, x + 160, y);
            e.Graphics.DrawLine(Pens.Black, x + 200, y1, x + 260, y1);
            e.Graphics.DrawString("ml。", textfront, Brushes.Black, x + 260, y);

            y = y + 25; y1 = y + 13;
            e.Graphics.DrawString("可能出现的问题和对策：", FroYH, Brushes.Black, x + 0, y);
            y = y + 20; y1 = y + 13;
            e.Graphics.DrawString("1、麻醉平面过广", FroYH, Brushes.Black, x + 0, y);
            y = y + 20; y1 = y + 13;
            e.Graphics.DrawString("对策：谨慎给药，积极预防；面罩给氧；维持通气，及时快速补液，辅助血管活性药物维持循环平稳，必要时给予气管插管。", FroYH, Brushes.Black, x + 20, y);
            y = y + 20; y1 = y + 13;
            e.Graphics.DrawString("2、麻醉效果欠佳", FroYH, Brushes.Black, x + 0, y);
            y = y + 20; y1 = y + 13;
            e.Graphics.DrawString("对策：重新穿刺，辅助静脉镇痛镇静药物或改用全身麻醉。", FroYH, Brushes.Black, x + 20, y);
            y = y + 20; y1 = y + 13;
            e.Graphics.DrawString("3、局麻药中毒", FroYH, Brushes.Black, x + 0, y);
            y = y + 20; y1 = y + 13;
            e.Graphics.DrawString("对策：积极预防；立即停止给药；辅助静脉镇静药物；面罩给氧；维持通气，必要给予肌松药气管插管；", FroYH, Brushes.Black, x + 20, y);
            y = y + 20; y1 = y + 13;
            e.Graphics.DrawString("及时快速补液；辅助血管活性药物维持循环平稳；给予利尿药。", FroYH, Brushes.Black, x + 20, y);
            y = y + 20; y1 = y + 13;
            e.Graphics.DrawString("4、手术失血过多", FroYH, Brushes.Black, x + 0, y);
            y = y + 20; y1 = y + 13;
            e.Graphics.DrawString("对策：及时发现，快速补液，及时输血，辅助血管活性药物维持循环平稳；必要时暂停手术，压迫止血；改用全身麻醉；辅助止血药。", FroYH, Brushes.Black, x + 20, y);
            y = y + 25; y1 = y + 13;
            e.Graphics.DrawString("其他问题和对策："+tbWentiduice.Text, FroYH, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 80, y1, x + 660, y1);

            y = y + 30; y1 = y + 13;
            e.Graphics.DrawString("麻醉科医师：" + cmbMZYS.Text, textfront, Brushes.Black, x + 150, y);
            e.Graphics.DrawLine(Pens.Black, x + 215, y1, x + 350, y1);
            e.Graphics.DrawString("日期：" + this.dtRecordTime.Text, textfront, Brushes.Black, x + 400, y);
            e.Graphics.DrawLine(Pens.Black, x + 430, y1, x + 600, y1);
        }


        private void exitStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BCCount > 0)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.MZZJ_SZ_FormClosing);
                this.Close();
            }
            else
            {
                if (MessageBox.Show("保存退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Save();
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.MZZJ_SZ_FormClosing);
                    this.Close();
                }
                else
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.MZZJ_SZ_FormClosing);
                    this.Close();
                }

            }
        }

        private void MZZJ_SZ_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SaveDoc == true)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.MZZJ_SZ_FormClosing);

                this.Close();
            }
            else
            {
                if (BCCount > 0)
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.MZZJ_SZ_FormClosing);
                    this.Close();
                }
                else
                {
                    if (MessageBox.Show("保存退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Save();
                        this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.MZZJ_SZ_FormClosing);
                        this.Close();
                    }
                    else
                    {
                        this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.MZZJ_SZ_FormClosing);
                        this.Close();
                    }

                }
            }
        }

        private void cmbAgeDW_SelectedIndexChanged(object sender, EventArgs e)
        {
            DAL.UpdatePaibanAgeDW(cmbAgeDW.Text, patID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            foreach (Control ct in groupBox1.Controls)
            {
                if (ct is CheckBox)
                {
                    ((CheckBox)ct).Checked = true;
                }
            }
            foreach (Control ct in groupBox2.Controls)
            {
                if (ct is CheckBox)
                {
                    ((CheckBox)ct).Checked = true;
                }
            }
            foreach (Control ct in groupBox3.Controls)
            {
                if (ct is CheckBox)
                {
                    ((CheckBox)ct).Checked = true;
                }
            }
            foreach (Control ct in groupBox4.Controls)
            {
                if (ct is CheckBox)
                {
                    ((CheckBox)ct).Checked = true;
                }
            }
            foreach (Control ct in groupBox5.Controls)
            {
                if (ct is CheckBox)
                {
                    ((CheckBox)ct).Checked = true;
                }
            }
            foreach (Control ct in groupBox6.Controls)
            {
                if (ct is CheckBox)
                {
                    ((CheckBox)ct).Checked = true;
                }
            }
            foreach (Control ct in groupBox7.Controls)
            {
                if (ct is CheckBox)
                {
                    ((CheckBox)ct).Checked = true;
                }
            }
            foreach (Control ct in groupBox8.Controls)
            {
                if (ct is CheckBox)
                {
                    ((CheckBox)ct).Checked = true;
                }
            }
            foreach (Control ct in groupBox9.Controls)
            {
                if (ct is CheckBox)
                {
                    ((CheckBox)ct).Checked = true;
                }
            }
            foreach (Control ct in groupBox10.Controls)
            {
                if (ct is CheckBox)
                {
                    ((CheckBox)ct).Checked = true;
                }
            } 
            
        }

       


    }
}
