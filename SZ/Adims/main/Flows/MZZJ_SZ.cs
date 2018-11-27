﻿using System;
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
    public partial class MZZJ_SZ : Form
    {
        string mzjldID, patID;
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        public MZZJ_SZ(string mzid, string patid)
        {
            mzjldID = mzid; patID = patid;
            InitializeComponent();
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
            //tbBingqu.Text = dt.Rows[0]["patdpm"].ToString();
            tbBedNo.Text = dt.Rows[0]["Patbedno"].ToString();
            tbZhuyuanID.Text = dt.Rows[0]["Patzhuyuanid"].ToString();


        }

        private void MZZJ_SZ_Load(object sender, EventArgs e)
        {
            this.dtBackTime.Format = DateTimePickerFormat.Custom;
            this.dtBackTime.CustomFormat = "yyyy-MM-dd HH:mm";
           
            BindPatInfo();
            cmbMZYS.Items.Clear();
            cmbYishengAfter.Items.Clear();
            DataTable dt1 = bll.selectUserName(1);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                cmbMZYS.Items.Add(dt1.Rows[i][0].ToString());
                cmbYishengAfter.Items.Add(dt1.Rows[i][0].ToString());
            }
            DataTable dt = DAL.GetMZZJ_SZ(mzjldID);
            if (dt.Rows.Count > 0)
            {
                cmbZGchuanci.Text = dt.Rows[0]["ZGchuanci"].ToString();
                cmbZGyingmowai.Text = dt.Rows[0]["ZGyingmowai"].ToString();
                cmbZGxiaoguo.Text = dt.Rows[0]["ZGxiaoguo"].ToString();
                tbZGmazuipingmian.Text = dt.Rows[0]["ZGmazuipingmian"].ToString();
                cmbQGcaozuo.Text = dt.Rows[0]["QGcaozuo"].ToString();
                cmbQGfuzhu.Text = dt.Rows[0]["QGfuzhu"].ToString();
                cmbQGtongqi.Text = dt.Rows[0]["QGtongqi"].ToString();
                string dmcci = dt.Rows[0]["DMchuanci"].ToString();
                if (dmcci.Contains("1")) cbdm1.Checked = true;
                if (dmcci.Contains("2")) cbdm2.Checked = true;
                if (dmcci.Contains("3")) cbdm3.Checked = true;
                if (dmcci.Contains("4")) cbdm4.Checked = true;
                if (dmcci.Contains("5")) cbdm5.Checked = true;
                if (dmcci.Contains("6")) cbdm6.Checked = true;
                if (dmcci.Contains("7")) cbdm7.Checked = true;
                if (dmcci.Contains("8")) cbdm8.Checked = true;
                if (dmcci.Contains("9")) cbdm9.Checked = true;
                string sjmcci = dt.Rows[0]["SJMchuanci"].ToString();
                if (sjmcci.Contains("1")) cbjm1.Checked = true;
                if (sjmcci.Contains("2")) cbjm2.Checked = true;
                if (sjmcci.Contains("3")) cbjm3.Checked = true;
                if (sjmcci.Contains("4")) cbjm4.Checked = true;
                if (sjmcci.Contains("5")) cbjm5.Checked = true;
                if (sjmcci.Contains("6")) cbjm6.Checked = true;
                if (sjmcci.Contains("7")) cbjm7.Checked = true;
                if (sjmcci.Contains("8")) cbjm8.Checked = true;
                if (sjmcci.Contains("9")) cbjm9.Checked = true;
                tbRemark.Text = dt.Rows[0]["Remark"].ToString();
                dtBackTime.Value = Convert.ToDateTime(dt.Rows[0]["backTime"]);
                cmbYishi.Text = dt.Rows[0]["Yishi"].ToString();
                tbXueyaDown.Text = dt.Rows[0]["XueyaDown"].ToString();
                tbXueyaUp.Text = dt.Rows[0]["XueyaUp"].ToString();
                tbXinlv.Text = dt.Rows[0]["Xinlv"].ToString();
                tbHuxilv.Text = dt.Rows[0]["Huxilv"].ToString();
                tbHuxiSPO2.Text = dt.Rows[0]["HuxiSPO2"].ToString();
                cmbXiyang.Text = dt.Rows[0]["Xiyang"].ToString();
                cmbTiwei.Text = dt.Rows[0]["Tiwei"].ToString();
                cmbDMCCchaichu.Text = dt.Rows[0]["DMCCchaichu"].ToString();
                cmbDMCCchaichuweizhi.Text = dt.Rows[0]["DMCCchaichuweizhi"].ToString();

                cmbZhentongB.Text = dt.Rows[0]["ZhentongB"].ToString();
                cmbJianyi.Text = dt.Rows[0]["Jianyi"].ToString();
                tbYuanyin.Text = dt.Rows[0]["Yuanyin"].ToString();
                cmbTongyishu.Text = dt.Rows[0]["Tongyishu"].ToString();
                cmbMZYS.Text = dt.Rows[0]["MZYS"].ToString();
                cmbBingfangRenyuan.Text = dt.Rows[0]["BingfangRenyuan"].ToString();
                if (dt.Rows[0]["UpdateTime"].ToString() != "")
                    dtVisitDate.Value = Convert.ToDateTime(dt.Rows[0]["UpdateTime"]);
                if (Convert.ToString(dt.Rows[0]["IsRead"]) != "")
                {
                    if (Convert.ToInt32(dt.Rows[0]["IsRead"]) == 1)
                        SaveToolStripMenuItem.Enabled = false;
                }

                DataTable dtAVYS = DAL.GetAfterVisit_YS(patID);
                if (dtAVYS.Rows.Count == 0)
                    return;
                else
                {
                    DataRow drAV = dtAVYS.Rows[0];
                    this.tbXueya1.Text = drAV["XueyaDown"].ToString();
                    tbXueya2.Text = drAV["XueyaUp"].ToString();
                    tbXinlv2.Text = drAV["Xinlv"].ToString();
                    tbHuxi.Text = drAV["Huxi"].ToString();
                    if (drAV["Yishi"].ToString().Contains("0"))
                        cbYishi0.Checked = true;
                    if (drAV["Yishi"].ToString().Contains("1"))
                        cbYishi1.Checked = true;
                    if (drAV["Yishi"].ToString().Contains("2"))
                        cbYishi2.Checked = true;

                    if (drAV["YanhouTT"].ToString().Contains("0"))
                        cbYanhouTT0.Checked = true;
                    if (drAV["YanhouTT"].ToString().Contains("1"))
                        cbYanhouTT1.Checked = true;


                    if (drAV["SYsiya"].ToString().Contains("0"))
                        cbSYsiya0.Checked = true;
                    if (drAV["SYsiya"].ToString().Contains("1"))
                        cbSYsiya1.Checked = true;


                    if (drAV["Exin"].ToString().Contains("0"))
                        cbExin0.Checked = true;
                    if (drAV["Exin"].ToString().Contains("1"))
                        cbExin1.Checked = true;

                    if (drAV["Outu"].ToString().Contains("0"))
                        cbOutu0.Checked = true;
                    if (drAV["Outu"].ToString().Contains("1"))
                        cbOutu1.Checked = true;

                    if (drAV["NiaoCL"].ToString().Contains("0"))
                        cbNiaoCL0.Checked = true;
                    if (drAV["NiaoCL"].ToString().Contains("1"))
                        cbNiaoCL1.Checked = true;

                    if (drAV["SizhiJL"].ToString().Contains("0"))
                        cbSizhiJL0.Checked = true;
                    if (drAV["SizhiJL"].ToString().Contains("1"))
                        cbSizhiJL1.Checked = true;

                    if (drAV["MZXG"].ToString().Contains("0"))
                        cbMZXG0.Checked = true;
                    if (drAV["MZXG"].ToString().Contains("1"))
                        cbMZXG1.Checked = true;
                    if (drAV["MZXG"].ToString().Contains("2"))
                        cbMZXG2.Checked = true;
                    if (drAV["MZXG"].ToString().Contains("3"))
                        cbMZXG3.Checked = true;
                    tbOther.Text = drAV["Other"].ToString();
                    cmbYishengAfter.Text = drAV["YishengAfter"].ToString();
                    dtVisitDateAfter.Value = Convert.ToDateTime(drAV["VisitDateAfter"]);
                }
            }
        }
        int BCCount = 0;
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();

        }
        private void Save()
        {

            Dictionary<string, string> zongjiedan = new Dictionary<string, string>();
            int result = 0;
            try
            {
                zongjiedan.Add("ZGchuanci", cmbZGchuanci.Text);
                zongjiedan.Add("ZGyingmowai", cmbZGyingmowai.Text);
                zongjiedan.Add("ZGxiaoguo", cmbZGxiaoguo.Text);
                zongjiedan.Add("ZGmazuipingmian", tbZGmazuipingmian.Text);
                zongjiedan.Add("QGcaozuo", cmbQGcaozuo.Text);
                zongjiedan.Add("QGfuzhu", cmbQGfuzhu.Text);
                zongjiedan.Add("QGtongqi", cmbQGtongqi.Text);
                string dmcc = string.Empty;
                if (cbdm1.Checked == true) dmcc += "1";
                if (cbdm2.Checked == true) dmcc += "2";
                if (cbdm3.Checked == true) dmcc += "3";
                if (cbdm4.Checked == true) dmcc += "4";
                if (cbdm5.Checked == true) dmcc += "5";
                if (cbdm6.Checked == true) dmcc += "6";
                if (cbdm7.Checked == true) dmcc += "7";
                if (cbdm8.Checked == true) dmcc += "8";
                if (cbdm9.Checked == true) dmcc += "9";
                zongjiedan.Add("DMchuanci", dmcc);
                string sjmcc = string.Empty;
                if (cbjm1.Checked == true) sjmcc += "1";
                if (cbjm2.Checked == true) sjmcc += "2";
                if (cbjm3.Checked == true) sjmcc += "3";
                if (cbjm4.Checked == true) sjmcc += "4";
                if (cbjm5.Checked == true) sjmcc += "5";
                if (cbjm6.Checked == true) sjmcc += "6";
                if (cbjm7.Checked == true) sjmcc += "7";
                if (cbjm8.Checked == true) sjmcc += "8";
                if (cbjm9.Checked == true) sjmcc += "9";
                zongjiedan.Add("sjmchuanci", sjmcc);
                zongjiedan.Add("Remark", tbRemark.Text);
                zongjiedan.Add("Yishi", cmbYishi.Text);
                zongjiedan.Add("XueyaDown", tbXueyaDown.Text);
                zongjiedan.Add("XueyaUp", tbXueyaUp.Text);
                zongjiedan.Add("Xinlv", tbXinlv.Text);
                zongjiedan.Add("Huxilv", tbHuxilv.Text);
                zongjiedan.Add("HuxiSPO2", tbHuxiSPO2.Text);
                zongjiedan.Add("Xiyang", cmbXiyang.Text);
                zongjiedan.Add("Tiwei", cmbTiwei.Text);
                zongjiedan.Add("DMCCchaichu", cmbDMCCchaichu.Text);
                zongjiedan.Add("DMCCchaichuweizhi", cmbDMCCchaichuweizhi.Text);
                zongjiedan.Add("ZhentongB", cmbZhentongB.Text);
                zongjiedan.Add("Jianyi", cmbJianyi.Text);
                zongjiedan.Add("Yuanyin", tbYuanyin.Text);
                zongjiedan.Add("Tongyishu", cmbTongyishu.Text);
                zongjiedan.Add("MZYS", cmbMZYS.Text);
                zongjiedan.Add("BingfangRenyuan", cmbBingfangRenyuan.Text);
                zongjiedan.Add("UpdateTime", dtVisitDate.Value.ToString());
                zongjiedan.Add("backtime", dtBackTime.Value.ToString());
                zongjiedan.Add("mzjldid", mzjldID);
                zongjiedan.Add("patid", patID);
                DataTable dt = DAL.GetMZZJ_SZ(mzjldID);
                if (dt.Rows.Count > 0)
                    result = DAL.Updatemazuizongjie_SZ(zongjiedan);
                else
                    result = DAL.Insertmazuizongjie_SZ(zongjiedan);

                Dictionary<string, string> AfterVisit = new Dictionary<string, string>();
                int result2 = 0;

                string valueStr = "";

                AfterVisit.Add("tbXueya1", tbXueya1.Text);
                AfterVisit.Add("tbXueya2", tbXueya2.Text);
                AfterVisit.Add("tbXinlv2", tbXinlv2.Text);
                AfterVisit.Add("Huxi", tbHuxi.Text);
                valueStr = "";
                if (cbYishi0.Checked)
                    valueStr += "0";
                if (cbYishi1.Checked)
                    valueStr += "1";
                if (cbYishi2.Checked)
                    valueStr += "2";
                AfterVisit.Add("Yishi", valueStr);

                valueStr = "";
                if (this.cbYanhouTT0.Checked)
                    valueStr += "0";
                if (cbYanhouTT1.Checked)
                    valueStr += "1";
                AfterVisit.Add("YanhouTT", valueStr);

                valueStr = "";
                if (this.cbSYsiya0.Checked)
                    valueStr += "0";
                if (cbSYsiya1.Checked)
                    valueStr += "1";
                AfterVisit.Add("SYsiya", valueStr);

                valueStr = "";
                if (this.cbExin0.Checked)
                    valueStr += "0";
                if (cbExin1.Checked)
                    valueStr += "1";
                AfterVisit.Add("Exin", valueStr);

                valueStr = "";
                if (this.cbOutu0.Checked)
                    valueStr += "0";
                if (cbOutu1.Checked)
                    valueStr += "1";
                AfterVisit.Add("Outu", valueStr);

                valueStr = "";
                if (this.cbNiaoCL0.Checked)
                    valueStr += "0";
                if (cbNiaoCL1.Checked)
                    valueStr += "1";
                AfterVisit.Add("NiaoCL", valueStr);

                valueStr = "";
                if (this.cbSizhiJL0.Checked)
                    valueStr += "0";
                if (cbSizhiJL1.Checked)
                    valueStr += "1";
                AfterVisit.Add("SizhiJL", valueStr);

                valueStr = "";
                if (this.cbChuanCD0.Checked)
                    valueStr += "0";
                if (cbChuanCD1.Checked)
                    valueStr += "1";
                if (this.cbChuanCD2.Checked)
                    valueStr += "2";
                if (cbChuanCD3.Checked)
                    valueStr += "3";
                if (this.cbChuanCD4.Checked)
                    valueStr += "4";
                if (cbChuanCD5.Checked)
                    valueStr += "5";
                AfterVisit.Add("ChuanCD", valueStr);


                valueStr = "";
                if (this.cbMZXG0.Checked)
                    valueStr += "0";
                if (cbMZXG1.Checked)
                    valueStr += "1";
                if (this.cbMZXG2.Checked)
                    valueStr += "2";
                if (cbMZXG3.Checked)
                    valueStr += "3";
                AfterVisit.Add("MZXG", valueStr);
                AfterVisit.Add("Other", tbOther.Text);
                AfterVisit.Add("YishengAfter", cmbYishengAfter.Text);
                AfterVisit.Add("VisitDateAfter", dtVisitDateAfter.Value.ToString());
                AfterVisit.Add("patid", patID);

                DataTable dt2 = DAL.GetAfterVisit_YS(patID);
                if (dt2.Rows.Count == 0)
                    result2 = DAL.InsertAfterVisit_YS(AfterVisit);
                else
                    result2 = DAL.UpdateAfterVisit_YS(AfterVisit);

                if (result > 0 && result2 > 0)
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
            DataTable dt = DAL.GetMZZJ_SZ(mzjldID);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("信息需要先保存到数据库，才能存档！");
            }
            else
            {
                result = DAL.UpdateMZZJ_STATE(patID);
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
            Font textfront = new Font(new FontFamily("宋体"), 8);
            Font textfront1 = new Font(new FontFamily("宋体"), 10);
            Font tagfont = new Font(new FontFamily("宋体"), 13);
            Font ptzt3 = new Font("新宋体", 13);//普通字体
            Font tagfont1 = new Font(new FontFamily("宋体"), 12);
            Font ptzt = new Font("新宋体", 9);//普通字体
            Pen pblack = Pens.Black;

            int x = 30, y = 30, y1 = y + 18;
            string title1 = "江  苏  盛  泽  医  院";
            string title2 = "江苏省人民医院盛泽分院";
            string title3 = "      麻醉总结单";
            y = y + 30; y1 = y + 18;
            e.Graphics.DrawString(title1, tagfont, Brushes.Black, x + 200, y);
            y = y + 30; y1 = y + 18;
            e.Graphics.DrawString(title2, tagfont, Brushes.Black, x + 200, y);
            y = y + 30; y1 = y + 18;
            e.Graphics.DrawString(title3, tagfont1, Brushes.Black, x + 200, y);


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
            e.Graphics.DrawString("年龄 " + tbAge.Text+" "+cmbAgeDW.Text, textfront, Brushes.Black, x + 240, y);
            e.Graphics.DrawLine(Pens.Black, x + 265, y1, x + 310, y1);
            //为空画斜杠
            if (tbAge.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(275 + x, y + 13), new Point(295 + x, y + 2));
            }
            e.Graphics.DrawString("床号 " + this.tbBedNo.Text, textfront, Brushes.Black, x + 330, y);
            e.Graphics.DrawLine(Pens.Black, x + 355, y1, x + 440, y1);
            //为空画斜杠
            if (tbBedNo.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(375 + x, y + 13), new Point(395 + x, y + 2));
            }
            e.Graphics.DrawString("日期 " + dtVisitDate.Text, textfront, Brushes.Black, x + 450, y);
            e.Graphics.DrawLine(Pens.Black, x + 475, y1, x + 600, y1); ;
            //为空画斜杠
            if (dtVisitDate.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(495 + x, y + 13), new Point(515 + x, y + 2));
            }

            y = y + 23; y1 = y + 13;
            e.Graphics.DrawString("椎管内麻醉 ", textfront1, Brushes.Black, x + 0, y);

            y = y + 23; y1 = y + 13;
            e.Graphics.DrawString("穿刺：" + cmbZGchuanci.Text, textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 30, y1, x + 220, y1);
            //为空画斜杠
            if (cmbZGchuanci.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(50 + x, y + 13), new Point(70 + x, y + 2));
            }
            e.Graphics.DrawString("硬膜外隙出血：" + cmbZGyingmowai.Text, textfront, Brushes.Black, x + 230, y);
            e.Graphics.DrawLine(Pens.Black, x + 305, y1, x + 410, y1);
            //为空画斜杠
            if (cmbZGyingmowai.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(325 + x, y + 13), new Point(345 + x, y + 2));
            }
            e.Graphics.DrawString("效果：" + cmbZGxiaoguo.Text, textfront, Brushes.Black, x + 420, y);
            e.Graphics.DrawLine(Pens.Black, x + 450, y1, x + 620, y1);

            //为空画斜杠
            if (cmbZGxiaoguo.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(470 + x, y + 13), new Point(490 + x, y + 2));
            }
            y = y + 23; y1 = y + 13;
            e.Graphics.DrawString("病人离开手术室的麻醉平面上限：" + tbZGmazuipingmian.Text, textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 160, y1, x + 620, y1);
            //为空画斜杠
            if (tbZGmazuipingmian.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(180 + x, y + 13), new Point(200 + x, y + 2));
            }

            y = y + 23; y1 = y + 13;
            e.Graphics.DrawString("气管插管全麻 ", textfront1, Brushes.Black, x + 0, y);

            y = y + 23; y1 = y + 13;
            e.Graphics.DrawString("操作：" + cmbQGcaozuo.Text, textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 30, y1, x + 220, y1);
            //为空画斜杠
            if (cmbQGcaozuo.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(50 + x, y + 13), new Point(70 + x, y + 2));
            }
            e.Graphics.DrawString("需要辅助器具：" + cmbQGfuzhu.Text, textfront, Brushes.Black, x + 230, y);
            e.Graphics.DrawLine(Pens.Black, x + 305, y1, x + 620, y1);
            //为空画斜杠
            if (cmbQGfuzhu.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(325 + x, y + 13), new Point(345 + x, y + 2));
            }
            y = y + 23; y1 = y + 13;
            e.Graphics.DrawString("困难气管插管病人有无辅助呼吸通气困难：" + cmbQGtongqi.Text, textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 210, y1, x + 620, y1);
            //为空画斜杠
            if (cmbQGtongqi.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(230 + x, y + 13), new Point(250 + x, y + 2));
            }

            y = y + 23; y1 = y + 13;
            string dmcc = string.Empty;
            if (cbdm1.Checked == true)
            {
                if (dmcc == string.Empty)
                    dmcc = cbdm1.Text;
                else
                    dmcc = dmcc + "，" + cbdm1.Text;
            }
            if (cbdm2.Checked == true)
            {
                if (dmcc == string.Empty)
                    dmcc = cbdm2.Text;
                else
                    dmcc = dmcc + "，" + cbdm2.Text;
            }
            if (cbdm3.Checked == true)
            {
                if (dmcc == string.Empty)
                    dmcc = cbdm3.Text;
                else
                    dmcc = dmcc + "，" + cbdm3.Text;
            }
            if (cbdm4.Checked == true)
            {
                if (dmcc == string.Empty)
                    dmcc = cbdm4.Text;
                else
                    dmcc = dmcc + "，" + cbdm4.Text;
            }
            if (cbdm5.Checked == true)
            {
                if (dmcc == string.Empty)
                    dmcc = cbdm5.Text;
                else
                    dmcc = dmcc + "，" + cbdm5.Text;
            }
            if (cbdm6.Checked == true)
            {
                if (dmcc == string.Empty)
                    dmcc = cbdm6.Text;
                else
                    dmcc = dmcc + "，" + cbdm6.Text;
            }
            if (cbdm7.Checked == true)
            {
                if (dmcc == string.Empty)
                    dmcc = cbdm7.Text;
                else
                    dmcc = dmcc + "，" + cbdm7.Text;
            }
            if (cbdm8.Checked == true)
            {
                if (dmcc == string.Empty)
                    dmcc = cbdm8.Text;
                else
                    dmcc = dmcc + "，" + cbdm8.Text;
            }
            if (cbdm9.Checked == true)
            {
                if (dmcc == string.Empty)
                    dmcc = cbdm9.Text;
                else
                    dmcc = dmcc + "，" + cbdm9.Text;
            }

            e.Graphics.DrawString("动脉穿刺：" + dmcc, textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 55, y1, x + 300, y1);
            //为空画斜杠
            if (dmcc == "")
            {
                e.Graphics.DrawLine(ptp, new Point(75 + x, y + 13), new Point(95 + x, y + 2));
            }
            string sjmcc = string.Empty;
            if (cbjm1.Checked == true)
            {
                if (sjmcc == string.Empty)
                    sjmcc = cbjm1.Text;
                else
                    sjmcc = sjmcc + "，" + cbjm1.Text;
            }
            if (cbjm2.Checked == true)
            {
                if (sjmcc == string.Empty)
                    sjmcc = cbjm2.Text;
                else
                    sjmcc = sjmcc + "，" + cbjm2.Text;
            }
            if (cbjm3.Checked == true)
            {
                if (sjmcc == string.Empty)
                    sjmcc = cbjm3.Text;
                else
                    sjmcc = sjmcc + "，" + cbjm3.Text;
            }
            if (cbjm4.Checked == true)
            {
                if (sjmcc == string.Empty)
                    sjmcc = cbjm4.Text;
                else
                    sjmcc = sjmcc + "，" + cbjm4.Text;
            }
            if (cbjm5.Checked == true)
            {
                if (sjmcc == string.Empty)
                    sjmcc = cbjm5.Text;
                else
                    sjmcc = sjmcc + "，" + cbjm5.Text;
            }
            if (cbjm6.Checked == true)
            {
                if (sjmcc == string.Empty)
                    sjmcc = cbjm6.Text;
                else
                    sjmcc = sjmcc + "，" + cbjm6.Text;
            }
            if (cbjm7.Checked == true)
            {
                if (sjmcc == string.Empty)
                    sjmcc = cbjm7.Text;
                else
                    sjmcc = sjmcc + "，" + cbjm7.Text;
            }
            if (cbjm8.Checked == true)
            {
                if (sjmcc == string.Empty)
                    sjmcc = cbjm8.Text;
                else
                    sjmcc = sjmcc + "，" + cbjm8.Text;
            }
            if (cbjm9.Checked == true)
            {
                if (sjmcc == string.Empty)
                    sjmcc = cbjm9.Text;
                else
                    sjmcc = sjmcc + "，" + cbjm9.Text;
            }
            e.Graphics.DrawString("深静脉穿刺：" + sjmcc, textfront, Brushes.Black, x + 310, y);
            e.Graphics.DrawLine(Pens.Black, x + 370, y1, x + 620, y1);
            //为空画斜杠
            if (sjmcc == "")
            {
                e.Graphics.DrawLine(ptp, new Point(390 + x, y + 13), new Point(410 + x, y + 2));
            }
            y = y + 23; y1 = y + 13;
            e.Graphics.DrawString("返回病房时的病情及麻醉相关注意事项 ", textfront1, Brushes.Black, x + 0, y);

            y = y + 23; y1 = y + 13;
            e.Graphics.DrawString("一、返回病房时间：" + dtBackTime.Value.ToString("yyyy/MM/dd HH:mm"), textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 100, y1, x + 280, y1);
            //为空画斜杠
            if (dtBackTime.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(120 + x, y + 13), new Point(140 + x, y + 2));
            }
            y = y + 23; y1 = y + 13;
            e.Graphics.DrawString("二、生命体征：", textfront, Brushes.Black, x + 0, y);
            y = y + 23; y1 = y + 13;
            e.Graphics.DrawString("1. 意识：" + cmbYishi.Text, textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 45, y1, x + 330, y1);
            //为空画斜杠
            if (cmbYishi.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(65 + x, y + 13), new Point(85 + x, y + 2));
            }
            y = y + 23; y1 = y + 13;
            e.Graphics.DrawString("2. 循环： 血压： " + tbXueyaDown.Text + " / " + tbXueyaUp.Text, textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 85, y1, x + 280, y1);

            e.Graphics.DrawString("mmHg； 心率： " + tbXinlv.Text, textfront, Brushes.Black, x + 290, y);
            e.Graphics.DrawLine(Pens.Black, x + 360, y1, x + 500, y1);
            //为空画斜杠
            if (tbXinlv.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(380 + x, y + 13), new Point(400 + x, y + 2));
            }
            e.Graphics.DrawString("次/分", textfront, Brushes.Black, x + 500, y);

            y = y + 23; y1 = y + 13;
            e.Graphics.DrawString("3. 呼吸： 频率： " + tbHuxilv.Text, textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 85, y1, x + 280, y1);
            //为空画斜杠
            if (tbHuxilv.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(105 + x, y + 13), new Point(125 + x, y + 2));
            }
            e.Graphics.DrawString("次/分； SpO2： " + tbHuxiSPO2.Text + " %", textfront, Brushes.Black, x + 290, y);
            e.Graphics.DrawLine(Pens.Black, x + 365, y1, x + 500, y1);

            y = y + 23; y1 = y + 13;
            e.Graphics.DrawString("三、注意事项：", textfront, Brushes.Black, x + 0, y);

            y = y + 23; y1 = y + 13;
            e.Graphics.DrawString("1. 吸氧：" + cmbXiyang.Text, textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 45, y1, x + 400, y1);
            //为空画斜杠
            if (cmbXiyang.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(65 + x, y + 13), new Point(85 + x, y + 2));
            }
            y = y + 23; y1 = y + 13;
            e.Graphics.DrawString("2. 体位：" + cmbTiwei.Text, textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 45, y1, x + 400, y1);
            //为空画斜杠
            if (cmbTiwei.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(65 + x, y + 13), new Point(85 + x, y + 2));
            }

            y = y + 23; y1 = y + 13;
            e.Graphics.DrawString("3. 动脉穿刺：" + cmbDMCCchaichu.Text, textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 70, y1, x + 210, y1);
            //为空画斜杠
            if (cmbDMCCchaichu.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(90 + x, y + 13), new Point(110 + x, y + 2));
            }
            e.Graphics.DrawString("，请于病人返回病房后30到60分钟之间解除 " + cmbDMCCchaichuweizhi.Text, textfront, Brushes.Black, x + 210, y);
            e.Graphics.DrawLine(Pens.Black, x + 430, y1, x + 500, y1);
            //为空画斜杠
            if (cmbDMCCchaichuweizhi.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(450 + x, y + 13), new Point(470 + x, y + 2));
            }
            e.Graphics.DrawString("加压泵带。", textfront, Brushes.Black, x + 500, y);

            y = y + 23; y1 = y + 13;
            e.Graphics.DrawString("4. 镇痛泵：" + cmbZhentongB.Text, textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 60, y1, x + 620, y1);
            //为空画斜杠
            if (cmbZhentongB.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(80 + x, y + 13), new Point(100 + x, y + 2));
            }

            y = y + 23; y1 = y + 13;
            //为空画斜杠
            if (cmbJianyi.Text.Trim() == "" || cmbJianyi.Text.Trim() == "无")
            {
                e.Graphics.DrawString("5. 特殊建议： 无", textfront, Brushes.Black, x + 0, y);
                //e.Graphics.DrawLine(ptp, new Point(90 + x, y + 13), new Point(110 + x, y + 2));
            }
            else if (cmbJianyi.Text.Trim() == "有")
            {
                e.Graphics.DrawString("5. 特殊建议：" + " 该病人因为(" + tbYuanyin.Text + ")需要加强生命体征监护或送ICU.", textfront, Brushes.Black, x + 0, y);
            }
            
            e.Graphics.DrawLine(Pens.Black, x + 70, y1, x + 620, y1);


            y = y + 23; y1 = y + 13;
            e.Graphics.DrawString("6. 麻醉前知情同意书： " + cmbTongyishu.Text, textfront, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 115, y1, x + 250, y1);

            //为空画斜杠
            if (cmbTongyishu.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(135 + x, y + 13), new Point(155 + x, y + 2));
            }

            y = y + 23; y1 = y + 13;
            e.Graphics.DrawString("7.术后病人出现生命体征不平稳、声音嘶哑或肌体活动障碍灯异常情况时,请及时联系麻醉值班医师,电话：6878或6002", textfront, Brushes.Black, x + 0, y);
            //y = y + 15; y1 = y + 13;
            //e.Graphics.DrawString("   ", textfront, Brushes.Black, x + 0, y);

            y = y + 23; y1 = y + 13;
            e.Graphics.DrawString("麻醉科医师：" + cmbMZYS.Text, textfront, Brushes.Black, x + 50, y);
            e.Graphics.DrawLine(Pens.Black, x + 115, y1, x + 320, y1);
           
            e.Graphics.DrawString("病房接班人员签字：" + this.cmbBingfangRenyuan.Text, textfront, Brushes.Black, x + 340, y);
            e.Graphics.DrawLine(Pens.Black, x + 440, y1, x + 620, y1);

            y = y + 40; y1 = y + 13;
            e.Graphics.DrawString("术 后 随 访", ptzt3, Brushes.Black, x + 260, y + 0);

            y = y + 30; y1 = y + 15;
            if (!string.IsNullOrEmpty(tbXueya1.Text.Trim()) && !string.IsNullOrEmpty(tbXueya1.Text.Trim()))
            {
                e.Graphics.DrawString("血压：" + this.tbXueya1.Text + " / " + tbXueya2.Text, ptzt, Brushes.Black, x + 0, y);
            }
            else
                e.Graphics.DrawString("血压：", ptzt, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 30, y1, x + 120, y1);
            e.Graphics.DrawString("mmHg；      心率：" + this.tbXinlv2.Text, ptzt, Brushes.Black, x + 120, y);
            e.Graphics.DrawLine(Pens.Black, x + 220, y1, x + 300, y1);
            e.Graphics.DrawString("次/分       呼吸：" + tbHuxi.Text, ptzt, Brushes.Black, x + 300, y);
            e.Graphics.DrawLine(Pens.Black, x + 400, y1, x + 480, y1);
            e.Graphics.DrawString("次/分", ptzt, Brushes.Black, x + 480, y);

            y = y + 22; y1 = y + 15;
            e.Graphics.DrawString("意识：□清醒  □嗜睡  □昏迷   咽喉疼痛： □有  □无   声音嘶哑： □有  □无    恶心： □有  □无 ", ptzt, Brushes.Black, x + 0, y);
            if (cbYishi0.Checked)
                e.Graphics.DrawString("      ✔", ptzt, Brushes.Black, x + 0, y);
            if (cbYishi1.Checked)
                e.Graphics.DrawString("              ✔", ptzt, Brushes.Black, x + 0, y);
            if (cbYishi2.Checked)
                e.Graphics.DrawString("                      ✔", ptzt, Brushes.Black, x + 0, y);
            if (this.cbYanhouTT0.Checked)
                e.Graphics.DrawString("                                          ✔", ptzt, Brushes.Black, x + 0, y);
            if (cbYanhouTT1.Checked)
                e.Graphics.DrawString("                                                ✔", ptzt, Brushes.Black, x + 0, y);
            if (this.cbSYsiya0.Checked)
                e.Graphics.DrawString("                                                                  ✔", ptzt, Brushes.Black, x + 0, y);
            if (cbSYsiya1.Checked)
                e.Graphics.DrawString("                                                                        ✔", ptzt, Brushes.Black, x + 0, y);
            if (this.cbExin0.Checked)
                e.Graphics.DrawString("                                                                                       ✔", ptzt, Brushes.Black, x + 0, y);
            if (cbExin1.Checked)
                e.Graphics.DrawString("                                                                                             ✔", ptzt, Brushes.Black, x + 0, y);

            y = y + 22; y1 = y + 15;
            e.Graphics.DrawString("呕吐：□有  □无   头痛： □有  □无    尿潴留： □有  □无 ", ptzt, Brushes.Black, x + 0, y);
            if (this.cbOutu0.Checked)
                e.Graphics.DrawString("      ✔", ptzt, Brushes.Black, x + 0, y);
            if (cbOutu1.Checked)
                e.Graphics.DrawString("            ✔", ptzt, Brushes.Black, x + 0, y);
            if (this.cbTouTeng0.Checked)
                e.Graphics.DrawString("                          ✔", ptzt, Brushes.Black, x + 0, y);
            if (this.cbTouTeng1.Checked)
                e.Graphics.DrawString("                                ✔", ptzt, Brushes.Black, x + 0, y);
            if (this.cbNiaoCL0.Checked)
                e.Graphics.DrawString("                                                 ✔", ptzt, Brushes.Black, x + 0, y);
            if (this.cbNiaoCL1.Checked)
                e.Graphics.DrawString("                                                       ✔", ptzt, Brushes.Black, x + 0, y);

            y = y + 22; y1 = y + 15;
            e.Graphics.DrawString("四肢肌力： □正常  □无力    感觉： □正常  □麻木", ptzt, Brushes.Black, x + 0, y);
            if (this.cbSizhiJL0.Checked)
                e.Graphics.DrawString("           ✔", ptzt, Brushes.Black, x + 0, y);
            if (this.cbSizhiJL1.Checked)
                e.Graphics.DrawString("                   ✔", ptzt, Brushes.Black, x + 0, y);
            if (this.cbGanjue0.Checked)
                e.Graphics.DrawString("                                    ✔", ptzt, Brushes.Black, x + 0, y);
            if (cbGanjue1.Checked)
                e.Graphics.DrawString("                                            ✔", ptzt, Brushes.Black, x + 0, y);

            y = y + 22; y1 = y + 15;
            e.Graphics.DrawString("穿刺点： 疼痛： □是  □否   红肿： □是  □否    感染： □有  □无", ptzt, Brushes.Black, x + 0, y);
            if (this.cbChuanCD0.Checked)
                e.Graphics.DrawString("                ✔", ptzt, Brushes.Black, x + 0, y);
            if (cbChuanCD1.Checked)
                e.Graphics.DrawString("                      ✔", ptzt, Brushes.Black, x + 0, y);
            if (this.cbChuanCD2.Checked)
                e.Graphics.DrawString("                                    ✔", ptzt, Brushes.Black, x + 0, y);
            if (this.cbChuanCD3.Checked)
                e.Graphics.DrawString("                                          ✔", ptzt, Brushes.Black, x + 0, y);
            if (this.cbChuanCD4.Checked)
                e.Graphics.DrawString("                                                         ✔", ptzt, Brushes.Black, x + 0, y);
            if (this.cbChuanCD5.Checked)
                e.Graphics.DrawString("                                                               ✔", ptzt, Brushes.Black, x + 0, y);

            y = y + 22; y1 = y + 15;
            e.Graphics.DrawString("麻醉效果： □满意   □较满意   □感觉疼痛   □不满意", ptzt, Brushes.Black, x + 0, y);
            if (this.cbMZXG0.Checked)
                e.Graphics.DrawString("           ✔", ptzt, Brushes.Black, x + 0, y);
            if (cbMZXG1.Checked)
                e.Graphics.DrawString("                    ✔", ptzt, Brushes.Black, x + 0, y);
            if (this.cbMZXG2.Checked)
                e.Graphics.DrawString("                               ✔", ptzt, Brushes.Black, x + 0, y);
            if (this.cbMZXG3.Checked)
                e.Graphics.DrawString("                                            ✔", ptzt, Brushes.Black, x + 0, y);

            y = y + 22; y1 = y + 15;
            e.Graphics.DrawString("其他：" + tbOther.Text, ptzt, Brushes.Black, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 30, y1, x + 660, y1);

            y = y + 22; y1 = y + 15;
            e.Graphics.DrawString("麻醉医师：" + cmbYishengAfter.Text, ptzt, Brushes.Black, x + 200, y);
            e.Graphics.DrawLine(Pens.Black, x + 260, y1, x + 360, y1);
            if (cmbYishengAfter.Text=="")
            {
                e.Graphics.DrawString("访视时间：" + "", ptzt, Brushes.Black, x + 400, y);
            }
            else
                e.Graphics.DrawString("访视时间：" + dtVisitDateAfter.Text, ptzt, Brushes.Black, x + 400, y);
          
            e.Graphics.DrawLine(Pens.Black, x + 460, y1, x + 560, y1);
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

        private void cmbAgeDW_SelectedIndexChanged(object sender, EventArgs e)
        {
            DAL.UpdatePaibanAgeDW(cmbAgeDW.Text, patID);
        }



    }
}
