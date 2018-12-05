using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using adims_DAL.Dics;
using adims_DAL.Flows;
using adims_Utility;
using adims_BLL;

namespace main
{
    public partial class AnesthesiaSummary : Form
    {
        OperScheduleDal _PaibanDal = new OperScheduleDal();
        MzjldDal _MzjldDal = new MzjldDal();
        DataDicDal _DataDicDal = new DataDicDal();
        string mzjldID, patID;
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        bool isRead = false;
        string Odate;
        public AnesthesiaSummary(string mzid, string patid, string date)
        {
            mzjldID = mzid; patID = patid;
            Odate = date;
            InitializeComponent();
        }
        private void MZZJ_SZ_Load(object sender, EventArgs e)
        {
            try
            {
                panel1.Left = 60;
                this.WindowState = FormWindowState.Maximized;
                BindPatInfo();
                BindMZYS();
                BindMazuipingmian();
                dtAddTime.Text = Odate;
                BindMzzjInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void BindPatInfo()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = _PaibanDal.GetPaibanByPatId(patID);
                tbPatname.Text = dt.Rows[0]["Patname"].ToString();
                tbAge.Text = dt.Rows[0]["Patage"].ToString();
                cmbSex.Text = dt.Rows[0]["Patsex"].ToString();
                tbkeshi.Text = dt.Rows[0]["patdpm"].ToString();
                tbZhuyuanID.Text = dt.Rows[0]["Patzhuyuanid"].ToString();

                DataTable dt2 = _MzjldDal.GetMzjldByMzjldId(mzjldID);
                if (dt2.Rows.Count > 0)
                {
                    DataRow dr2 = dt2.Rows[0];
                    txtASA.Text = dr2["ASA"].ToString();
                    txtSSMC.Text = dr2["ShoushuFS"].ToString();
                    cmbZGNMZFF.Text = dr2["MazuiFS"].ToString();
                    tbYaopin_ZG.Text = dr2["Yw"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void BindMazuipingmian()
        {
            DataTable dt = _DataDicDal.GetMZPM();//麻醉平面
            cmbMZPM_ZG.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                cmbMZPM_ZG.Items.Add(dr["name"].ToString());
            }
            cmbMZPM_OUT.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                cmbMZPM_OUT.Items.Add(dr["name"].ToString());
            }
            DataTable dtCC = _DataDicDal.GetMZCC();//麻醉穿刺
            cmbCCD1.Items.Clear();
            foreach (DataRow dr in dtCC.Rows)
            {
                cmbCCD1.Items.Add(dr["name"].ToString());
            }
            cmbCCD2.Items.Clear();
            foreach (DataRow dr in dtCC.Rows)
            {
                cmbCCD2.Items.Add(dr["name"].ToString());
            }
            DataTable dtmzff = _DataDicDal.GetMazuiFangfaAll();//麻醉方法
            cmbZGNMZFF.Items.Clear();
            foreach (DataRow dr in dtmzff.Rows)
            {
                cmbZGNMZFF.Items.Add(dr["name"].ToString());
            }
            cmbZGNMZFF_qm.Items.Clear();
            foreach (DataRow dr in dtmzff.Rows)
            {
                cmbZGNMZFF_qm.Items.Add(dr["name"].ToString());
            }
        }



        private void BindMzzjInfo()
        {
            DataTable dt = _MzzjDal.GetMzzjByMzjldId(mzjldID);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                if (dr["addtime"].ToString() != "")
                    dtAddTime.Value = Convert.ToDateTime(dr["addtime"]);
                if (Convert.ToString(dr["QSMZ"]) == "1")
                {
                    cbQSMZ.Checked = true;
                    if (dr["ZGNMZFF_qm"].ToString() != "")
                    {
                        cmbZGNMZFF_qm.Text = Convert.ToString(dr["ZGNMZFF_qm"]);
                    }
                }
                cmbYoudaoFF.Text = Convert.ToString(dr["YoudaoFF"]);
                if (Convert.ToString(dr["ZGNMZ"]) == "1")
                {
                    cbZGNMZ.Checked = true;
                    if (dr["ZGNMZFF"].ToString() != "")
                    {
                        cmbZGNMZFF.Text = Convert.ToString(dr["ZGNMZFF"]);
                    }
                }
                cmbCCD1.Text = Convert.ToString(dr["CCD11"]);
                tbliuzhiSD1.Text = Convert.ToString(dr["liuzhiSD1"]);
                cmbCCD2.Text = Convert.ToString(dr["CCD21"]);
                tbliuzhiSD2.Text = Convert.ToString(dr["liuzhiSD2"]);
                cmbMZPM_ZG.Text = Convert.ToString(dr["MZPM_ZG"]);
                if (dr["Yaopin_ZG"].ToString() != "")
                {
                    tbYaopin_ZG.Text = Convert.ToString(dr["Yaopin_ZG"]);
                }

                if (Convert.ToString(dr["SJZZ"]).Contains("1")) cbSJZZ.Checked = true;
                if (Convert.ToString(dr["JCSJZZ"]).Contains("1")) cbJCSJZZ.Checked = true;
                if (Convert.ToString(dr["JCSJZZ"]).Contains("2")) cbJCSJZZqcLeft.Checked = true;
                if (Convert.ToString(dr["JCSJZZ"]).Contains("3")) cbJCSJZZqcRight.Checked = true;
                if (Convert.ToString(dr["JCSJZZ"]).Contains("4")) cbJCSJZZscLeft.Checked = true;
                if (Convert.ToString(dr["JCSJZZ"]).Contains("5")) cbJCSJZZscRight.Checked = true;

                if (Convert.ToString(dr["BCSJZZ"]).Contains("1")) cbBCSJZZ.Checked = true;
                if (Convert.ToString(dr["BCSJZZ"]).Contains("2")) cbBCSJZZleft.Checked = true;
                if (Convert.ToString(dr["BCSJZZ"]).Contains("3")) cbBCSJZZright.Checked = true;
                if (Convert.ToString(dr["BCSJZZ"]).Contains("4")) cbBCSJZZjjgf.Checked = true;
                if (Convert.ToString(dr["BCSJZZ"]).Contains("5")) cbBCSJZZylf.Checked = true;
                if (Convert.ToString(dr["BCSJZZ"]).Contains("6")) cbBCSJZZsgsf.Checked = true;

                if (Convert.ToString(dr["YCSJZZ"]).Contains("1")) cbYCSJZZ.Checked = true;
                if (Convert.ToString(dr["YCSJZZ"]).Contains("2")) cbYCSJZZleft.Checked = true;
                if (Convert.ToString(dr["YCSJZZ"]).Contains("3")) cbYCSJZZright.Checked = true;

                if (Convert.ToString(dr["ZGSJZZ"]).Contains("1")) cbZGSJZZ.Checked = true;
                if (Convert.ToString(dr["ZGSJZZ"]).Contains("2")) cbZGSJZZleft.Checked = true;
                if (Convert.ToString(dr["ZGSJZZ"]).Contains("3")) cbZGSJZZright.Checked = true;

                if (Convert.ToString(dr["GSJZZ"]).Contains("1")) cbGSJZZ.Checked = true;
                if (Convert.ToString(dr["GSJZZ"]).Contains("2")) cbGSJZZleft.Checked = true;
                if (Convert.ToString(dr["GSJZZ"]).Contains("3")) cbGSJZZright.Checked = true;

                if (Convert.ToString(dr["GWCSJZZ"]).Contains("1")) cbGWCSJZZ.Checked = true;
                if (Convert.ToString(dr["GWCSJZZ"]).Contains("2")) cbGWCSJZZleft.Checked = true;
                if (Convert.ToString(dr["GWCSJZZ"]).Contains("3")) cbGWCSJZZright.Checked = true;

                if (dr["Yaopin_SJZZ"].ToString() != "")
                {
                    tbYaopin_SJZZ.Text = Convert.ToString(dr["Yaopin_SJZZ"]);
                }
                if (Convert.ToString(dr["YCCZ"]) == "1") cbYCCZ.Checked = true;
                if (Convert.ToString(dr["DMCCZG"]) == "1") cbDMCCZG.Checked = true;
                if (Convert.ToString(dr["SJMCCZG"]) == "1") cbSJMCCZG.Checked = true;
                if (Convert.ToString(dr["QTNum"]) == "1") cbQT.Checked = true;
                tbOtherZZ.Text = Convert.ToString(dr["OtherZZ"]);
                if (Convert.ToString(dr["HZ"]) == "1") cbHZ.Checked = true;
                tbHZType.Text = Convert.ToString(dr["HZType"]);
                tbWCFF.Text = Convert.ToString(dr["HZwcff"]);
                if (Convert.ToString(dr["MZJH"]) == "1") cbMZJH.Checked = true;
                tbRemarkMZ.Text = Convert.ToString(dr["RemarkMZ"]);
                cmbTaoNang.Text = Convert.ToString(dr["TaoNang"]);
                cmbJili.Text = Convert.ToString(dr["Jili"]);
                cmbKesouTunyan.Text = Convert.ToString(dr["KesouTunyan"]);
                cmbDingxiangli.Text = Convert.ToString(dr["Dingxiangli"]);
                cmbYishi.Text = Convert.ToString(dr["Yishi"]);
                cmbMZPM_OUT.Text = Convert.ToString(dr["MZPM_OUT"]);
                cmbBRQX.Text = Convert.ToString(dr["BRQX"]);
                tbRemarkOut.Text = Convert.ToString(dr["RemarkOut"]);
                tbTSQK.Text = Convert.ToString(dr["TSQK"]);
                cmbBRZKZT.Text = Convert.ToString(dr["BRZKZT"]);
                cmbMZYS.Text = Convert.ToString(dr["MZYS"]);
                if (Convert.ToString(dr["QGCG"]) == "1") cbQGCG.Checked = true;
                cmbQGCGwz1.Text = Convert.ToString(dr["QGCGwz1"]);
                cmbQGCGwz2.Text = Convert.ToString(dr["QGCGwz2"]);
                cmbQGCGwz3.Text = Convert.ToString(dr["QGCGwz3"]);
                tbQGCGtype.Text = Convert.ToString(dr["QGCGtype"]);
                tbTSQK.Text = Convert.ToString(dr["TSQK"]);
                tbQGCGsd.Text = Convert.ToString(dr["QGCGsd"]);
                cmbisYuanyin.Text = Convert.ToString(dr["isYuanyin"]);
                if (Convert.ToString(dr["IsRead"]) == "0")
                {
                    btnSave.Enabled = true;
                    isRead = false;
                }
                if (Convert.ToString(dr["IsRead"]) == "1")
                {
                    isRead = true;
                    btnSave.Enabled = false;
                }
                UserRoleBll _UserRoleBll = new UserRoleBll();
                string jurisdiction = _UserRoleBll.GetUserRole(Program.customer);
                if (jurisdiction.Contains("8"))
                {
                    btnUnlock.Visible = true;
                }
                else
                {
                    btnUnlock.Visible = false;
                }

            }
        }

        private void BindMZYS()
        {
            DataTable dt1 = _DataDicDal.GetUserByType((int)EnumCreator.UserType.麻醉医生);
            cmbMZYS.Items.Clear();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                cmbMZYS.Items.Add(dt1.Rows[i][0].ToString());
            }
        }

        int BCCount = 0;
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void Save()
        {
            Dictionary<string, string> zjdan = new Dictionary<string, string>();
            int result = 0;
            string AddItem = "";
            try
            {
                zjdan.Add("mzjldid", mzjldID);
                zjdan.Add("patid", patID);
                zjdan.Add("AddTime", dtAddTime.Value.ToString());
                AddItem = "";
                if (cbQSMZ.Checked) AddItem = "1";
                else AddItem = "0";
                zjdan.Add("QSMZ", AddItem);
                zjdan.Add("YoudaoFF", cmbYoudaoFF.Text);
                AddItem = "";
                if (cbZGNMZ.Checked) AddItem = "1";
                else AddItem = "0";
                zjdan.Add("ZGNMZ", AddItem);
                zjdan.Add("ZGNMZFF", cmbZGNMZFF.Text);
                zjdan.Add("CCD11", cmbCCD1.Text);
                zjdan.Add("CCD12", cmbCCD1.Text);
                zjdan.Add("liuzhiSD1", tbliuzhiSD1.Text.Trim());
                zjdan.Add("CCD21", cmbCCD2.Text);
                zjdan.Add("CCD22", cmbCCD2.Text);
                zjdan.Add("liuzhiSD2", tbliuzhiSD2.Text.Trim());
                zjdan.Add("MZPM_ZG", cmbMZPM_ZG.Text.Trim());
                zjdan.Add("Yaopin_ZG", tbYaopin_ZG.Text.Trim());

                AddItem = "";
                if (cbSJZZ.Checked) AddItem = "1";
                else AddItem = "0";
                zjdan.Add("SJZZ", AddItem);

                AddItem = "";
                if (cbJCSJZZ.Checked) AddItem = "1";
                else AddItem = "0";
                if (cbJCSJZZqcLeft.Checked) AddItem += "2";
                if (cbJCSJZZqcRight.Checked) AddItem += "3";
                if (cbJCSJZZscLeft.Checked) AddItem += "4";
                if (cbJCSJZZscRight.Checked) AddItem += "5";
                zjdan.Add("JCSJZZ", AddItem);
                AddItem = "";
                if (cbBCSJZZ.Checked) AddItem = "1";
                else AddItem = "0";
                if (cbBCSJZZleft.Checked) AddItem += "2";
                if (cbBCSJZZright.Checked) AddItem += "3";
                if (cbBCSJZZjjgf.Checked) AddItem += "4";
                if (cbBCSJZZylf.Checked) AddItem += "5";
                if (cbBCSJZZsgsf.Checked) AddItem += "6";
                zjdan.Add("BCSJZZ", AddItem);

                AddItem = "";
                if (cbYCSJZZ.Checked) AddItem = "1";
                else AddItem = "0";
                if (cbYCSJZZleft.Checked) AddItem += "2";
                if (cbYCSJZZright.Checked) AddItem += "3";
                zjdan.Add("YCSJZZ", AddItem);

                AddItem = "";
                if (cbZGSJZZ.Checked) AddItem = "1";
                else AddItem = "0";
                if (cbZGSJZZleft.Checked) AddItem += "2";
                if (cbZGSJZZright.Checked) AddItem += "3";
                zjdan.Add("ZGSJZZ", AddItem);

                AddItem = "";
                if (cbGSJZZ.Checked) AddItem = "1";
                else AddItem = "0";
                if (cbGSJZZleft.Checked) AddItem += "2";
                if (cbGSJZZright.Checked) AddItem += "3";
                zjdan.Add("GSJZZ", AddItem);

                AddItem = "";
                if (cbGWCSJZZ.Checked) AddItem = "1";
                else AddItem = "0";
                if (cbGWCSJZZleft.Checked) AddItem += "2";
                if (cbGWCSJZZright.Checked) AddItem += "3";
                zjdan.Add("GWCSJZZ", AddItem);

                zjdan.Add("Yaopin_SJZZ", tbYaopin_SJZZ.Text.Trim());
                AddItem = "";
                if (cbYCCZ.Checked) AddItem = "1";
                else AddItem = "0";
                zjdan.Add("YCCZ", AddItem);
                AddItem = "";
                if (cbDMCCZG.Checked) AddItem = "1";
                else AddItem = "0";
                zjdan.Add("DMCCZG", AddItem);
                AddItem = "";
                if (cbSJMCCZG.Checked) AddItem = "1";
                else AddItem = "0";
                zjdan.Add("SJMCCZG", AddItem);
                AddItem = "";
                zjdan.Add("OtherZZ", tbOtherZZ.Text.Trim());
                if (cbHZ.Checked) AddItem = "1";
                else AddItem = "0";
                zjdan.Add("HZ", AddItem);
                zjdan.Add("HZType", tbHZType.Text.Trim());
                zjdan.Add("HZwcff", tbWCFF.Text.Trim());
                AddItem = "";
                if (cbMZJH.Checked) AddItem = "1";
                else AddItem = "0";
                zjdan.Add("MZJH", AddItem);
                zjdan.Add("RemarkMZ", tbRemarkMZ.Text.Trim());
                zjdan.Add("TaoNang", cmbTaoNang.Text.Trim());
                zjdan.Add("Jili", cmbJili.Text.Trim());
                zjdan.Add("KesouTunyan", cmbKesouTunyan.Text.Trim());
                zjdan.Add("Dingxiangli", cmbDingxiangli.Text.Trim());
                zjdan.Add("Yishi", cmbYishi.Text.Trim());
                zjdan.Add("MZPM_OUT", cmbMZPM_OUT.Text.Trim());
                zjdan.Add("RemarkOut", tbRemarkOut.Text.Trim());
                zjdan.Add("BRQX", cmbBRQX.Text.Trim());
                zjdan.Add("TSQK", tbTSQK.Text.Trim());
                zjdan.Add("MZYS", cmbMZYS.Text.Trim());
                AddItem = "";
                if (cbQGCG.Checked) AddItem = "1";
                else AddItem = "0";
                zjdan.Add("QGCG", AddItem);
                zjdan.Add("QGCGwz1", cmbQGCGwz1.Text.Trim());
                zjdan.Add("QGCGwz2", cmbQGCGwz2.Text.Trim());
                zjdan.Add("QGCGwz3", cmbQGCGwz3.Text.Trim());
                zjdan.Add("QGCGtype", tbQGCGtype.Text.Trim());
                zjdan.Add("QGCGsd", tbQGCGsd.Text.Trim());
                zjdan.Add("BRZKZT", cmbBRZKZT.Text.Trim());
                zjdan.Add("isRead", "0");
                AddItem = "";
                if (cbQT.Checked) AddItem = "1";
                else AddItem = "0";
                zjdan.Add("QTNum", AddItem);
                zjdan.Add("isYuanyin", cmbisYuanyin.Text);
                zjdan.Add("Odate", Odate);
                zjdan.Add("ZGNMZFF_qm", cmbZGNMZFF_qm.Text.Trim());
                DataTable dt = _MzzjDal.GetMzzjByMzjldId(mzjldID);
                if (dt.Rows.Count > 0)
                    result = _MzzjDal.UpdateMZZJ_CJ(zjdan);
                else
                    result = _MzzjDal.InsertMZZJ_CJ(zjdan);
                if (result > 0)
                {
                    BCCount++; MessageBox.Show("保存成功！");
                }
                else MessageBox.Show("保存失败！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "保存出现异常，请检查网络！");
            }
        }


        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            printDocument1.DefaultPageSettings.PaperSize =
                new PaperSize("K16", 750, 1020);
            //printDocument1.DefaultPageSettings.PaperSize =
            //   new PaperSize("A4", 820, 1160);

        }
        int pages = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font zt8 = new Font(new FontFamily("新宋体"), 8);
            Font zt9 = new Font(new FontFamily("新宋体"), 9);
            Font ht8 = new Font(new FontFamily("黑体"), 8);
            Font ht9 = new Font(new FontFamily("黑体"), 9);
            Font zt14 = new Font(new FontFamily("新宋体"), 10);
            Font ptzt3 = new Font("新宋体", 14, FontStyle.Bold);//加粗14号
            Pen ptp = new Pen(Brushes.Black);
            Pen pb2 = new Pen(Brushes.Black, 2);
            int x = 60, y = 40, y1 = y + 15;
            //e.HasMorePages = true;
            //pages++;
            if (pages == 0)
            {
                string title1 = "天津红桥医院麻醉总结记录";
                e.Graphics.DrawString(title1, ptzt3, Brushes.Black, new Point(x + 170, y));
                y = y + 30;
                e.Graphics.DrawString("姓名：" + tbPatname.Text, ht8, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawString("性别：" + cmbSex.Text.Trim(), ht8, Brushes.Black, new Point(x + 120, y));
                e.Graphics.DrawString("年龄：" + this.tbAge.Text.Trim() + "岁", ht8, Brushes.Black, new Point(x + 210, y));
                e.Graphics.DrawString("科别：" + this.tbkeshi.Text.Trim(), ht8, Brushes.Black, new Point(x + 300, y));
                e.Graphics.DrawString("住院号：" + this.tbZhuyuanID.Text.Trim(), ht8, Brushes.Black, new Point(x + 400, y));
                e.Graphics.DrawString("ASA分级：" + this.txtASA.Text.Trim(), ht8, Brushes.Black, new Point(x + 550, y));
                y = y + 25;
                e.Graphics.DrawString("手术名称：" + txtSSMC.Text, ht8, Brushes.Black, new Point(x + 10, y));
                y = y + 25; int YYY = y;
                e.Graphics.DrawLine(ptp, x, y, x + 660, y);//画横线

                //e.Graphics.DrawLine(ptp, x, y + 5, 700, y + 5);
                y = y + 20;
                e.Graphics.DrawString("麻醉经过： ", zt14, Brushes.Black, x + 10, y);
                e.Graphics.DrawString("离   室", zt14, Brushes.Black, x + 530, y);
                y = y + 30;
                e.Graphics.DrawRectangle(Pens.Black, x + 10, y, 10, 10);
                e.Graphics.DrawString("全身麻醉 ", ht8, Brushes.Black, x + 20, y);
                if (cbQSMZ.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 10, y, x + 15, y + 10);
                    e.Graphics.DrawLine(pb2, x + 15, y + 10, x + 20, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 200, y, 10, 10);
                e.Graphics.DrawString("椎管内麻醉 ", ht8, Brushes.Black, x + 210, y);
                if (cbZGNMZ.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 200, y, x + 205, y + 10);
                    e.Graphics.DrawLine(pb2, x + 205, y + 10, x + 210, y);
                }
                e.Graphics.DrawString("肌力恢复：", zt9, Brushes.Black, x + 470, y);
                e.Graphics.DrawRectangle(Pens.Black, x + 530, y, 10, 10);
                e.Graphics.DrawString("好", zt8, Brushes.Black, x + 540, y);
                if (cmbJili.Text == "好")
                {
                    e.Graphics.DrawLine(pb2, x + 530, y, x + 535, y + 10);
                    e.Graphics.DrawLine(pb2, x + 535, y + 10, x + 540, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 560, y, 10, 10);
                e.Graphics.DrawString("差", zt8, Brushes.Black, x + 570, y);
                if (cmbJili.Text == "差")
                {
                    e.Graphics.DrawLine(pb2, x + 560, y, x + 565, y + 10);
                    e.Graphics.DrawLine(pb2, x + 565, y + 10, x + 570, y);
                }
                y = y + 20;
                e.Graphics.DrawString("麻醉方法：" + cmbZGNMZFF_qm.Text, zt8, Brushes.Black, x + 20, y);
                e.Graphics.DrawString("麻醉方式： " + cmbZGNMZFF.Text, zt8, Brushes.Black, x + 210, y);
                //e.Graphics.DrawRectangle(Pens.Black, x + 210, y, 10, 10);
                //e.Graphics.DrawString("腰麻", zt8, Brushes.Black, x + 220, y);
                //if (cmbZGNMZFF.Text=="腰麻")
                //{
                //    e.Graphics.DrawLine(pb2, x + 210, y, x + 215, y + 10);
                //    e.Graphics.DrawLine(pb2, x + 215, y + 10, x + 220, y);        
                //}
                //e.Graphics.DrawRectangle(Pens.Black, x + 260, y, 10, 10);
                //e.Graphics.DrawString("硬膜外麻醉", zt8, Brushes.Black, x + 270, y);
                //if (cmbZGNMZFF.Text=="硬膜外麻醉")
                //{
                //    e.Graphics.DrawLine(pb2, x + 260, y, x + 265, y + 10);
                //    e.Graphics.DrawLine(pb2, x + 265, y + 10, x + 270, y);    
                //}
                e.Graphics.DrawString("咳嗽吞咽反射：", zt8, Brushes.Black, x + 470, y);
                e.Graphics.DrawRectangle(Pens.Black, x + 550, y, 10, 10);
                e.Graphics.DrawString("有", zt8, Brushes.Black, x + 560, y);
                if (cmbKesouTunyan.Text == "有")
                {
                    e.Graphics.DrawLine(pb2, x + 550, y, x + 555, y + 10);
                    e.Graphics.DrawLine(pb2, x + 555, y + 10, x + 560, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 580, y, 10, 10);
                e.Graphics.DrawString("无", zt8, Brushes.Black, x + 590, y);
                if (cmbKesouTunyan.Text == "无")
                {
                    e.Graphics.DrawLine(pb2, x + 580, y, x + 585, y + 10);
                    e.Graphics.DrawLine(pb2, x + 585, y + 10, x + 590, y);
                }
                y = y + 20; y1 = y + 13;
                e.Graphics.DrawString("诱导方法：", zt8, Brushes.Black, x + 20, y);
                //e.Graphics.DrawRectangle(Pens.Black, x + 20, y, 10, 10);
                //e.Graphics.DrawString("快诱导", zt8, Brushes.Black, x + 30, y);
                //if (cmbYoudaoFF.Text=="快诱导")
                //{
                //    e.Graphics.DrawLine(pb2, x + 20, y, x + 25, y + 10);
                //    e.Graphics.DrawLine(pb2, x + 25, y + 10, x + 30, y);   
                //}
                e.Graphics.DrawString("穿刺点1：" + cmbCCD1.Text, zt8, Brushes.Black, x + 210, y);
                e.Graphics.DrawLine(ptp, new Point(x + 260, y1), new Point(x + 310, y1));
                e.Graphics.DrawString("留置深度：" + tbliuzhiSD1.Text, zt8, Brushes.Black, x + 320, y);
                e.Graphics.DrawLine(ptp, new Point(x + 370, y1), new Point(x + 420, y1));
              
                if (tbliuzhiSD1.Text.Trim() != "")
                {
                    e.Graphics.DrawString("cm", zt8, Brushes.Black, x + 420, y);
                }
                //e.Graphics.DrawRectangle(Pens.Black, x + 210, y, 10, 10);
                //e.Graphics.DrawString("腰硬联合麻醉", zt8, Brushes.Black, x + 220, y);
                //if (cmbZGNMZFF.Text=="腰硬联合麻醉")
                //{
                //    e.Graphics.DrawLine(pb2, x + 210, y, x + 215, y + 10);
                //    e.Graphics.DrawLine(pb2, x + 215, y + 10, x + 220, y);      
                //}
                //e.Graphics.DrawRectangle(Pens.Black, x + 320, y, 10, 10);
                //e.Graphics.DrawString("骶麻", zt8, Brushes.Black, x + 330, y);
                //if (cmbZGNMZFF.Text == "骶麻")
                //{
                //    e.Graphics.DrawLine(pb2, x + 320, y, x + 325, y + 10);
                //    e.Graphics.DrawLine(pb2, x + 325, y + 10, x + 330, y);      
                //}
                e.Graphics.DrawString("定向力恢复：", zt8, Brushes.Black, x + 470, y);
                e.Graphics.DrawRectangle(Pens.Black, x + 540, y, 10, 10);
                e.Graphics.DrawString("是", zt8, Brushes.Black, x + 550, y);
                if (cmbDingxiangli.Text == "是")
                {
                    e.Graphics.DrawLine(pb2, x + 540, y, x + 545, y + 10);
                    e.Graphics.DrawLine(pb2, x + 545, y + 10, x + 550, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 570, y, 10, 10);
                e.Graphics.DrawString("否", zt8, Brushes.Black, x + 580, y);
                if (cmbDingxiangli.Text == "否")
                {
                    e.Graphics.DrawLine(pb2, x + 570, y, x + 575, y + 10);
                    e.Graphics.DrawLine(pb2, x + 575, y + 10, x + 580, y);
                }
                y = y + 20; y1 = y + 13;
                e.Graphics.DrawRectangle(Pens.Black, x + 20, y, 10, 10);
                e.Graphics.DrawString("快诱导", zt8, Brushes.Black, x + 30, y);
                if (cmbYoudaoFF.Text == "快诱导")
                {
                    e.Graphics.DrawLine(pb2, x + 20, y, x + 25, y + 10);
                    e.Graphics.DrawLine(pb2, x + 25, y + 10, x + 30, y);
                }
                //e.Graphics.DrawRectangle(Pens.Black, x + 20, y, 10, 10);
                //e.Graphics.DrawString("慢诱导", zt8, Brushes.Black, x + 30, y);
                //if (cmbYoudaoFF.Text == "慢诱导")
                //{
                //    e.Graphics.DrawLine(pb2, x + 20, y, x + 25, y + 10);
                //    e.Graphics.DrawLine(pb2, x + 25, y + 10, x + 30, y);
                //}
                e.Graphics.DrawString("穿刺点2：" + cmbCCD2.Text, zt8, Brushes.Black, x + 210, y);
                e.Graphics.DrawLine(ptp, new Point(x + 260, y1), new Point(x + 310, y1));
                e.Graphics.DrawString("留置深度：" + tbliuzhiSD2.Text, zt8, Brushes.Black, x + 320, y);
                e.Graphics.DrawLine(ptp, new Point(x + 370, y1), new Point(x + 420, y1));
              
                if (tbliuzhiSD2.Text.Trim() != "")
                {
                    e.Graphics.DrawString("cm", zt8, Brushes.Black, x + 420, y);
                }

                e.Graphics.DrawString("意识：", zt8, Brushes.Black, x + 470, y);
                e.Graphics.DrawRectangle(Pens.Black, x + 510, y, 10, 10);
                e.Graphics.DrawString("清醒", zt8, Brushes.Black, x + 520, y);
                if (cmbYishi.Text == "清醒")
                {
                    e.Graphics.DrawLine(pb2, x + 510, y, x + 515, y + 10);
                    e.Graphics.DrawLine(pb2, x + 515, y + 10, x + 520, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 550, y, 10, 10);
                e.Graphics.DrawString("嗜睡", zt8, Brushes.Black, x + 560, y);
                if (cmbYishi.Text == "嗜睡")
                {
                    e.Graphics.DrawLine(pb2, x + 550, y, x + 555, y + 10);
                    e.Graphics.DrawLine(pb2, x + 555, y + 10, x + 560, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 590, y, 10, 10);
                e.Graphics.DrawString("麻醉状态", zt9, Brushes.Black, x + 600, y);
                if (cmbYishi.Text == "麻醉状态")
                {
                    e.Graphics.DrawLine(pb2, x + 590, y, x + 595, y + 10);
                    e.Graphics.DrawLine(pb2, x + 595, y + 10, x + 600, y);
                }
                y = y + 20; y1 = y + 13;
                e.Graphics.DrawRectangle(Pens.Black, x + 20, y, 10, 10);
                e.Graphics.DrawString("慢诱导", zt8, Brushes.Black, x + 30, y);
                if (cmbYoudaoFF.Text == "慢诱导")
                {
                    e.Graphics.DrawLine(pb2, x + 20, y, x + 25, y + 10);
                    e.Graphics.DrawLine(pb2, x + 25, y + 10, x + 30, y);
                }
                e.Graphics.DrawString("麻醉平面：" + cmbMZPM_ZG.Text, zt8, Brushes.Black, x + 210, y);
                e.Graphics.DrawLine(ptp, new Point(x + 260, y1), new Point(x + 440, y1));
                e.Graphics.DrawRectangle(Pens.Black, x + 510, y, 10, 10);
                e.Graphics.DrawString("谵妄", zt8, Brushes.Black, x + 520, y);
                if (cmbYishi.Text == "谵妄")
                {
                    e.Graphics.DrawLine(pb2, x + 510, y, x + 515, y + 10);
                    e.Graphics.DrawLine(pb2, x + 515, y + 10, x + 520, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 550, y, 10, 10);
                e.Graphics.DrawString("昏迷", zt8, Brushes.Black, x + 560, y);
                if (cmbYishi.Text == "昏迷")
                {
                    e.Graphics.DrawLine(pb2, x + 550, y, x + 555, y + 10);
                    e.Graphics.DrawLine(pb2, x + 555, y + 10, x + 560, y);
                }
                y = y + 20; y1 = y + 13;
                e.Graphics.DrawString("药品：" + tbYaopin_ZG.Text, zt8, Brushes.Black, x + 210, y);
                e.Graphics.DrawLine(ptp, new Point(x + 240, y1), new Point(x + 440, y1));
                e.Graphics.DrawString("病人自控镇痛：", zt8, Brushes.Black, x + 470, y);
                e.Graphics.DrawRectangle(Pens.Black, x + 560, y, 10, 10);
                e.Graphics.DrawString("是", zt8, Brushes.Black, x + 570, y);
                if (cmbBRZKZT.Text == "是")
                {
                    e.Graphics.DrawLine(pb2, x + 560, y, x + 565, y + 10);
                    e.Graphics.DrawLine(pb2, x + 565, y + 10, x + 570, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 590, y, 10, 10);
                e.Graphics.DrawString("否", zt8, Brushes.Black, x + 600, y);
                if (cmbBRZKZT.Text == "否")
                {
                    e.Graphics.DrawLine(pb2, x + 590, y, x + 595, y + 10);
                    e.Graphics.DrawLine(pb2, x + 595, y + 10, x + 600, y);
                }
                y = y + 20; y1 = y + 13;
                e.Graphics.DrawRectangle(Pens.Black, x + 10, y, 10, 10);
                e.Graphics.DrawString("气管插管 ", ht8, Brushes.Black, x + 20, y);
                if (cbQGCG.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 10, y, x + 15, y + 10);
                    e.Graphics.DrawLine(pb2, x + 15, y + 10, x + 20, y);
                }

                e.Graphics.DrawString("麻醉平面：" + cmbMZPM_OUT.Text, zt8, Brushes.Black, x + 470, y);
                e.Graphics.DrawLine(ptp, new Point(x + 520, y1), new Point(x + 650, y1));
                y = y + 20; y1 = y + 13;
                e.Graphics.DrawRectangle(Pens.Black, x + 20, y, 10, 10);
                e.Graphics.DrawString("气管内", zt9, Brushes.Black, x + 30, y);
                if (cmbQGCGwz1.Text == "气管内")
                {
                    e.Graphics.DrawLine(pb2, x + 20, y, x + 25, y + 10);
                    e.Graphics.DrawLine(pb2, x + 25, y + 10, x + 30, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 80, y, 10, 10);
                e.Graphics.DrawString("支气管内", zt8, Brushes.Black, x + 90, y);
                if (cmbQGCGwz1.Text == "支气管内")
                {
                    e.Graphics.DrawLine(pb2, x + 80, y, x + 85, y + 10);
                    e.Graphics.DrawLine(pb2, x + 85, y + 10, x + 90, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 200, y, 10, 10);
                e.Graphics.DrawString("神经阻滞", ht8, Brushes.Black, x + 210, y);
                if (cbSJZZ.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 220, y, x + 225, y + 10);
                    e.Graphics.DrawLine(pb2, x + 225, y + 10, x + 230, y);
                }
                int BZYY = y, BZYY1 = y1;
                List<string> str = new List<string>();
                string str1 = "";
                int StrLength = tbRemarkOut.Text.Trim().Length;
                int row = StrLength / 13;
                e.Graphics.DrawString("备注：", zt9, Brushes.Black, x + 470, BZYY);
                for (int i = 0; i <= row; i++)//13个字符就换行
                {
                    if (i < row)
                        str1 = tbRemarkOut.Text.Trim().ToString().Substring(i * 13, 13); //从第i*13个开始，截取13个字符串
                    else
                        str1 = tbRemarkOut.Text.Trim().ToString().Substring(i * 13);
                    e.Graphics.DrawString(str1, zt8, Brushes.Black, x + 500, BZYY);
                    BZYY = BZYY + 20; BZYY1 = BZYY + 13;
                }
                int YY = y, YY1 = y1;
                e.Graphics.DrawLine(ptp, new Point(x + 500, YY1), new Point(x + 650, YY1));
                YY = YY + 20; YY1 = YY + 13;
                e.Graphics.DrawLine(ptp, new Point(x + 500, YY1), new Point(x + 650, YY1));
                YY = YY + 20; YY1 = YY + 13;
                e.Graphics.DrawLine(ptp, new Point(x + 500, YY1), new Point(x + 650, YY1));
                YY = YY + 20; YY1 = YY + 13;
                e.Graphics.DrawLine(ptp, new Point(x + 500, YY1), new Point(x + 650, YY1));
                YY = YY + 20; YY1 = YY + 13;
                e.Graphics.DrawLine(ptp, new Point(x + 500, YY1), new Point(x + 650, YY1));
                YY = YY + 20; YY1 = YY + 13;
                e.Graphics.DrawLine(ptp, new Point(x + 500, YY1), new Point(x + 650, YY1));
                YY = YY + 20; YY1 = YY + 13;
                e.Graphics.DrawLine(ptp, new Point(x + 500, YY1), new Point(x + 650, YY1));
                YY = YY + 20; YY1 = YY + 13;
                e.Graphics.DrawLine(ptp, new Point(x + 500, YY1), new Point(x + 650, YY1));
                YY = YY + 20; YY1 = YY + 13;
                e.Graphics.DrawLine(ptp, new Point(x + 500, YY1), new Point(x + 650, YY1));
                YY = YY + 20; YY1 = YY + 13;
                e.Graphics.DrawLine(ptp, new Point(x + 500, YY1), new Point(x + 650, YY1));
                //YY = YY + 20; YY1 = YY + 13;
                //e.Graphics.DrawLine(ptp, new Point(x + 500, YY1), new Point(x + 650, YY1));       

                y = y + 20; y1 = y + 13;
                e.Graphics.DrawRectangle(Pens.Black, x + 20, y, 10, 10);
                e.Graphics.DrawString("左", zt8, Brushes.Black, x + 30, y);
                if (cmbQGCGwz2.Text == "左")
                {
                    e.Graphics.DrawLine(pb2, x + 20, y, x + 25, y + 10);
                    e.Graphics.DrawLine(pb2, x + 25, y + 10, x + 30, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 50, y, 10, 10);
                e.Graphics.DrawString("右", zt9, Brushes.Black, x + 60, y);
                if (cmbQGCGwz2.Text == "右")
                {
                    e.Graphics.DrawLine(pb2, x + 50, y, x + 55, y + 10);
                    e.Graphics.DrawLine(pb2, x + 55, y + 10, x + 60, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 210, y, 10, 10);
                e.Graphics.DrawString("颈丛神经阻滞：", zt8, Brushes.Black, x + 220, y);
                if (cbJCSJZZ.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 210, y, x + 215, y + 10);
                    e.Graphics.DrawLine(pb2, x + 215, y + 10, x + 220, y);
                }

                y = y + 20; y1 = y + 13;
                e.Graphics.DrawRectangle(Pens.Black, x + 20, y, 10, 10);
                e.Graphics.DrawString("经口", zt8, Brushes.Black, x + 30, y);
                if (cmbQGCGwz3.Text == "经口")
                {
                    e.Graphics.DrawLine(pb2, x + 20, y, x + 25, y + 10);
                    e.Graphics.DrawLine(pb2, x + 25, y + 10, x + 30, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 60, y, 10, 10);
                e.Graphics.DrawString("经鼻", zt8, Brushes.Black, x + 70, y);
                if (cmbQGCGwz3.Text == "经鼻")
                {
                    e.Graphics.DrawLine(pb2, x + 60, y, x + 65, y + 10);
                    e.Graphics.DrawLine(pb2, x + 65, y + 10, x + 70, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 100, y, 10, 10);
                e.Graphics.DrawString("经气管造口", zt8, Brushes.Black, x + 110, y);
                if (cmbQGCGwz3.Text == "经气管造口")
                {
                    e.Graphics.DrawLine(pb2, x + 100, y, x + 105, y + 10);
                    e.Graphics.DrawLine(pb2, x + 105, y + 10, x + 110, y);
                }
                e.Graphics.DrawString("浅丛", zt8, Brushes.Black, x + 220, y);
                e.Graphics.DrawRectangle(Pens.Black, x + 250, y, 10, 10);
                e.Graphics.DrawString("左", zt8, Brushes.Black, x + 260, y);
                if (cbJCSJZZqcLeft.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 250, y, x + 255, y + 10);
                    e.Graphics.DrawLine(pb2, x + 255, y + 10, x + 260, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 280, y, 10, 10);
                e.Graphics.DrawString("右", zt8, Brushes.Black, x + 290, y);
                if (cbJCSJZZqcRight.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 280, y, x + 285, y + 10);
                    e.Graphics.DrawLine(pb2, x + 285, y + 10, x + 290, y);
                }

                y = y + 20; y1 = y + 13;
                e.Graphics.DrawString("型号：" + tbQGCGtype.Text, zt8, Brushes.Black, x + 20, y);
                e.Graphics.DrawLine(ptp, new Point(x + 50, y1), new Point(x + 180, y1));
                e.Graphics.DrawString("深丛", zt8, Brushes.Black, x + 220, y);
                e.Graphics.DrawRectangle(Pens.Black, x + 250, y, 10, 10);
                e.Graphics.DrawString("左", zt8, Brushes.Black, x + 260, y);
                if (cbJCSJZZscLeft.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 250, y, x + 255, y + 10);
                    e.Graphics.DrawLine(pb2, x + 255, y + 10, x + 260, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 280, y, 10, 10);
                e.Graphics.DrawString("右", zt8, Brushes.Black, x + 290, y);
                if (cbJCSJZZscRight.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 280, y, x + 285, y + 10);
                    e.Graphics.DrawLine(pb2, x + 285, y + 10, x + 290, y);
                }
                //e.Graphics.DrawString("c", zt9, Brushes.Black, x + 420, y);
                //e.Graphics.DrawLine(ptp, new Point(x + 430, y1), new Point(x + 480, y1));

                y = y + 20; y1 = y + 13;
                e.Graphics.DrawString("深度：" + tbQGCGsd.Text, zt8, Brushes.Black, x + 20, y);
                e.Graphics.DrawLine(ptp, new Point(x + 50, y1), new Point(x + 120, y1));
                if (tbQGCGsd.Text.Trim()!="")
                {
                    e.Graphics.DrawString("cm", zt8, Brushes.Black, x + 120, y);
                }
                
                e.Graphics.DrawRectangle(Pens.Black, x + 210, y, 10, 10);
                e.Graphics.DrawString("臂丛神经阻滞：", zt8, Brushes.Black, x + 220, y);
                if (cbBCSJZZ.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 210, y, x + 215, y + 10);
                    e.Graphics.DrawLine(pb2, x + 215, y + 10, x + 220, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 300, y, 10, 10);
                e.Graphics.DrawString("左", zt8, Brushes.Black, x + 310, y);
                if (cbBCSJZZleft.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 300, y, x + 305, y + 10);
                    e.Graphics.DrawLine(pb2, x + 305, y + 10, x + 310, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 330, y, 10, 10);
                e.Graphics.DrawString("右", zt8, Brushes.Black, x + 340, y);
                if (cbBCSJZZright.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 330, y, x + 335, y + 10);
                    e.Graphics.DrawLine(pb2, x + 335, y + 10, x + 340, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 10, 10);
                e.Graphics.DrawString("肌间沟法", zt8, Brushes.Black, x + 370, y);
                if (cbBCSJZZjjgf.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 360, y, x + 365, y + 10);
                    e.Graphics.DrawLine(pb2, x + 365, y + 10, x + 370, y);
                }
                y = y + 20; y1 = y + 13;
                e.Graphics.DrawString("套囊：", zt8, Brushes.Black, x + 20, y);
                e.Graphics.DrawRectangle(Pens.Black, x + 60, y, 10, 10);
                e.Graphics.DrawString("有", zt8, Brushes.Black, x + 70, y);
                if (cmbTaoNang.Text == "有")
                {
                    e.Graphics.DrawLine(pb2, x + 60, y, x + 65, y + 10);
                    e.Graphics.DrawLine(pb2, x + 65, y + 10, x + 70, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 90, y, 10, 10);
                e.Graphics.DrawString("无", zt8, Brushes.Black, x + 100, y);
                if (cmbTaoNang.Text == "无")
                {
                    e.Graphics.DrawLine(pb2, x + 90, y, x + 95, y + 10);
                    e.Graphics.DrawLine(pb2, x + 95, y + 10, x + 100, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 220, y, 10, 10);
                e.Graphics.DrawString("腋路法", zt8, Brushes.Black, x + 230, y);
                if (cbBCSJZZylf.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 220, y, x + 225, y + 10);
                    e.Graphics.DrawLine(pb2, x + 225, y + 10, x + 230, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 270, y, 10, 10);
                e.Graphics.DrawString("锁骨上法", zt8, Brushes.Black, x + 280, y);
                if (cbBCSJZZsgsf.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 270, y, x + 275, y + 10);
                    e.Graphics.DrawLine(pb2, x + 275, y + 10, x + 280, y);
                }

                y = y + 20; y1 = y + 13;
                e.Graphics.DrawRectangle(Pens.Black, x + 210, y, 10, 10);
                e.Graphics.DrawString("腰丛神经阻滞：", zt8, Brushes.Black, x + 220, y);
                if (cbYCSJZZ.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 210, y, x + 215, y + 10);
                    e.Graphics.DrawLine(pb2, x + 215, y + 10, x + 220, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 320, y, 10, 10);
                e.Graphics.DrawString("左", zt8, Brushes.Black, x + 330, y);
                if (cbYCSJZZleft.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 320, y, x + 325, y + 10);
                    e.Graphics.DrawLine(pb2, x + 325, y + 10, x + 330, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 350, y, 10, 10);
                e.Graphics.DrawString("右", zt8, Brushes.Black, x + 360, y);
                if (cbYCSJZZright.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 350, y, x + 355, y + 10);
                    e.Graphics.DrawLine(pb2, x + 355, y + 10, x + 360, y);
                }

                y = y + 20; y1 = y + 13;
                e.Graphics.DrawRectangle(Pens.Black, x + 210, y, 10, 10);
                e.Graphics.DrawString("坐骨神经阻滞：", zt8, Brushes.Black, x + 220, y);
                if (cbZGSJZZ.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 210, y, x + 215, y + 10);
                    e.Graphics.DrawLine(pb2, x + 215, y + 10, x + 220, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 320, y, 10, 10);
                e.Graphics.DrawString("左", zt8, Brushes.Black, x + 330, y);
                if (cbZGSJZZleft.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 320, y, x + 325, y + 10);
                    e.Graphics.DrawLine(pb2, x + 325, y + 10, x + 330, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 350, y, 10, 10);
                e.Graphics.DrawString("右", zt8, Brushes.Black, x + 360, y);
                if (cbZGSJZZright.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 350, y, x + 355, y + 10);
                    e.Graphics.DrawLine(pb2, x + 355, y + 10, x + 360, y);
                }

                y = y + 20; y1 = y + 13;
                e.Graphics.DrawRectangle(Pens.Black, x + 210, y, 10, 10);
                e.Graphics.DrawString("股神经阻滞：", zt8, Brushes.Black, x + 220, y);
                if (cbGSJZZ.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 210, y, x + 215, y + 10);
                    e.Graphics.DrawLine(pb2, x + 215, y + 10, x + 220, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 320, y, 10, 10);
                e.Graphics.DrawString("左", zt8, Brushes.Black, x + 330, y);
                if (cbGSJZZleft.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 320, y, x + 325, y + 10);
                    e.Graphics.DrawLine(pb2, x + 325, y + 10, x + 330, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 350, y, 10, 10);
                e.Graphics.DrawString("右", zt8, Brushes.Black, x + 360, y);
                if (cbGSJZZright.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 350, y, x + 355, y + 10);
                    e.Graphics.DrawLine(pb2, x + 355, y + 10, x + 360, y);
                }
                y = y + 20; y1 = y + 13;
                e.Graphics.DrawRectangle(Pens.Black, x + 10, y, 10, 10);
                e.Graphics.DrawString("喉罩 ", ht8, Brushes.Black, x + 20, y);
                if (cbHZ.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 10, y, x + 15, y + 10);
                    e.Graphics.DrawLine(pb2, x + 15, y + 10, x + 20, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 210, y, 10, 10);
                e.Graphics.DrawString("股外侧皮神经阻滞：", zt8, Brushes.Black, x + 220, y);
                if (cbGWCSJZZ.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 210, y, x + 215, y + 10);
                    e.Graphics.DrawLine(pb2, x + 215, y + 10, x + 220, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 320, y, 10, 10);
                e.Graphics.DrawString("左", zt8, Brushes.Black, x + 330, y);
                if (cbGWCSJZZleft.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 320, y, x + 325, y + 10);
                    e.Graphics.DrawLine(pb2, x + 325, y + 10, x + 330, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 350, y, 10, 10);
                e.Graphics.DrawString("右", zt8, Brushes.Black, x + 360, y);
                if (cbGWCSJZZright.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 350, y, x + 355, y + 10);
                    e.Graphics.DrawLine(pb2, x + 355, y + 10, x + 360, y);
                }
                e.Graphics.DrawString("去   向", zt14, Brushes.Black, x + 530, y + 20);
                y = y + 20; y1 = y + 13;
                e.Graphics.DrawString("型号：" + tbHZType.Text, zt8, Brushes.Black, x + 20, y);
                e.Graphics.DrawLine(ptp, new Point(x + 50, y1), new Point(x + 180, y1));
                e.Graphics.DrawString("药品：" + tbYaopin_SJZZ.Text, zt8, Brushes.Black, x + 210, y);
                e.Graphics.DrawLine(ptp, new Point(x + 240, y1), new Point(x + 400, y1));
                e.Graphics.DrawRectangle(Pens.Black, x + 470, y + 30, 10, 10);
                e.Graphics.DrawString("PACU ", ht8, Brushes.Black, x + 480, y + 30);
                if (cmbBRQX.Text == "PACU")
                {
                    e.Graphics.DrawLine(pb2, x + 470, y + 30, x + 475, y + 10 + 30);
                    e.Graphics.DrawLine(pb2, x + 475, y + 10 + 30, x + 480, y + 30);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 530, y + 30, 10, 10);
                e.Graphics.DrawString("ICU ", ht8, Brushes.Black, x + 540, y + 30);
                if (cmbBRQX.Text == "ICU")
                {
                    e.Graphics.DrawLine(pb2, x + 530, y + 30, x + 535, y + 10 + 30);
                    e.Graphics.DrawLine(pb2, x + 535, y + 10 + 30, x + 540, y + 30);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 580, y + 30, 10, 10);
                e.Graphics.DrawString("病房 ", ht9, Brushes.Black, x + 590, y + 30);
                if (cmbBRQX.Text == "病房")
                {
                    e.Graphics.DrawLine(pb2, x + 580, y + 30, x + 585, y + 10 + 30);
                    e.Graphics.DrawLine(pb2, x + 585, y + 10 + 30, x + 590, y + 30);
                }
                y = y + 20; y1 = y + 13;
                e.Graphics.DrawRectangle(Pens.Black, x + 10, y, 10, 10);
                e.Graphics.DrawString("麻醉监护", zt8, Brushes.Black, x + 20, y);
                if (cbMZJH.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 10, y, x + 15, y + 10);
                    e.Graphics.DrawLine(pb2, x + 15, y + 10, x + 20, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 210, y, 10, 10);
                e.Graphics.DrawString("有创操作：", zt9, Brushes.Black, x + 220, y);
                if (cbYCCZ.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 210, y, x + 215, y + 10);
                    e.Graphics.DrawLine(pb2, x + 215, y + 10, x + 220, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 470, y + 40, 10, 10);
                e.Graphics.DrawString("门/急诊观察室", ht8, Brushes.Black, x + 480, y + 40);
                if (cmbBRQX.Text == "门/急诊观察室")
                {
                    e.Graphics.DrawLine(pb2, x + 470, y + 40, x + 475, y + 10 + 40);
                    e.Graphics.DrawLine(pb2, x + 475, y + 10 + 40, x + 480, y + 40);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 580, y + 40, 10, 10);
                e.Graphics.DrawString("返家 ", ht9, Brushes.Black, x + 590, y + 40);
                if (cmbBRQX.Text == "返家")
                {
                    e.Graphics.DrawLine(pb2, x + 580, y + 40, x + 585, y + 10 + 40);
                    e.Graphics.DrawLine(pb2, x + 585, y + 10 + 40, x + 590, y + 40);
                }
                y = y + 20; y1 = y + 13;
                e.Graphics.DrawString("维持方法：" + tbHZType.Text, zt8, Brushes.Black, x + 10, y);
                e.Graphics.DrawLine(ptp, new Point(x + 60, y1), new Point(x + 180, y1));

                e.Graphics.DrawRectangle(Pens.Black, x + 220, y, 10, 10);
                e.Graphics.DrawString("动脉穿刺置管", zt8, Brushes.Black, x + 230, y);
                if (cbDMCCZG.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 220, y, x + 225, y + 10);
                    e.Graphics.DrawLine(pb2, x + 225, y + 10, x + 230, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 300, y, 10, 10);
                e.Graphics.DrawString("深静脉穿刺置管", zt8, Brushes.Black, x + 310, y);
                if (cbSJMCCZG.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 300, y, x + 305, y + 10);
                    e.Graphics.DrawLine(pb2, x + 305, y + 10, x + 310, y);
                }
                //e.Graphics.DrawRectangle(Pens.Black, x + 550, y + 50, 12, 12);
                //e.Graphics.DrawString("返家 ", ht9, Brushes.Black, x + 570, y + 50);
                //if (cmbBRQX.Text == "返家")
                //{
                //    e.Graphics.DrawLine(pb2, x + 545, y + 50, x + 555, y + 12 + 50);
                //    e.Graphics.DrawLine(pb2, x + 555, y + 50 + 12, x + 570, y - 3 + 50);
                //}
                y = y + 20; y1 = y + 13;
                e.Graphics.DrawRectangle(Pens.Black, x + 220, y, 10, 10);
                e.Graphics.DrawString("其他：" + tbOtherZZ.Text, zt8, Brushes.Black, x + 230, y);
                if (cbQT.Checked)
                {
                    e.Graphics.DrawLine(pb2, x + 220, y, x + 225, y + 10);
                    e.Graphics.DrawLine(pb2, x + 225, y + 10, x + 230, y);
                }
                e.Graphics.DrawLine(ptp, new Point(x + 260, y1), new Point(x + 400, y1));
                e.Graphics.DrawLine(ptp, x, y + 20, x + 660, y + 20);//画右边的横线
                e.Graphics.DrawLine(ptp, x + 460, YYY, x + 460, y + 20);//画中间的竖线
                y = y + 40; y1 = y + 15;
                e.Graphics.DrawString("是否变更麻醉方法：", ht9, Brushes.Black, x + 10, y);
                e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
                e.Graphics.DrawString("是", zt8, Brushes.Black, x + 170, y);
                if (cmbisYuanyin.Text == "是")
                {
                    e.Graphics.DrawLine(pb2, x + 150, y, x + 155, y + 10);
                    e.Graphics.DrawLine(pb2, x + 155, y + 10, x + 160, y);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 190, y, 10, 10);
                e.Graphics.DrawString("否", zt8, Brushes.Black, x + 210, y);
                if (cmbisYuanyin.Text == "否")
                {
                    e.Graphics.DrawLine(pb2, x + 190, y, x + 195, y + 10);
                    e.Graphics.DrawLine(pb2, x + 195, y + 10, x + 200, y);
                }
                y = y + 25; y1 = y + 15;
                int BZYYSS = y, BZYYSS1 = y1;
                string strSS1 = "";
                int StrLengthSS = tbTSQK.Text.Trim().Length;
                int rowSS = StrLengthSS / 55;
                e.Graphics.DrawString("变更麻醉方法及原因：", ht9, Brushes.Black, x + 10, BZYYSS);
                for (int i = 0; i <= rowSS; i++)//55个字符就换行
                {
                    if (i < rowSS)
                        strSS1 = tbTSQK.Text.Trim().ToString().Substring(i * 55, 55); //从第i*55个开始，截取55个字符串
                    else
                        strSS1 = tbTSQK.Text.Trim().ToString().Substring(i * 55);

                    BZYYSS = BZYYSS + 30; BZYYSS1 = BZYYSS + 10;
                    e.Graphics.DrawString(strSS1, zt8, Brushes.Black, x + 10, BZYYSS);
                }
                y = y + 30; y1 = y + 15;
                e.Graphics.DrawLine(ptp, new Point(x + 10, y1), new Point(690, y1));
                y = y + 30; y1 = y + 15;
                e.Graphics.DrawLine(ptp, new Point(x + 10, y1), new Point(690, y1));
                y = y + 30; y1 = y + 15;
                e.Graphics.DrawLine(ptp, new Point(x + 10, y1), new Point(690, y1));
                y = y + 30; y1 = y + 15;

                y = y + 30; y1 = y + 15;
                int BZYYS = y, BZYYS1 = y1;
                //List<string> strS = new List<string>();
                string strS1 = "";
                int StrLengthS = tbRemarkMZ.Text.Trim().Length;
                int rowS = StrLengthS / 55;
                e.Graphics.DrawString("小结", ht9, Brushes.Black, x + 10, BZYYS - 30);
                for (int i = 0; i <= rowS; i++)//35个字符就换行
                {
                    if (i == 5) break;
                    if (i < rowS)
                        strS1 = tbRemarkMZ.Text.Trim().ToString().Substring(i * 55, 55); //从第i*30个开始，截取10个字符串
                    else
                        strS1 = tbRemarkMZ.Text.Trim().ToString().Substring(i * 55);
                    e.Graphics.DrawString(strS1, zt8, Brushes.Black, x + 10, BZYYS);
                    BZYYS = BZYYS + 30; BZYYS1 = BZYYS + 15;
                }
                //e.Graphics.DrawString("备注：" + tbRemarkMZ.Text, zt9, Brushes.Black, x + 20, y);
                e.Graphics.DrawLine(ptp, new Point(x + 10, y1), new Point(x + 650, y1));
                y = y + 30; y1 = y + 15;
                e.Graphics.DrawLine(ptp, new Point(x + 10, y1), new Point(x + 650, y1));
                y = y + 30; y1 = y + 15;
                e.Graphics.DrawLine(ptp, new Point(x + 10, y1), new Point(x + 650, y1));
                y = y + 30; y1 = y + 15;
                e.Graphics.DrawLine(ptp, new Point(x + 10, y1), new Point(x + 650, y1));
                y = y + 30; y1 = y + 15;
                e.Graphics.DrawLine(ptp, new Point(x + 10, y1), new Point(x + 650, y1));
                y = y + 30; y1 = y + 15;
                //e.Graphics.DrawLine(ptp, new Point(x + 10, y1), new Point(x + 650, y1));
                //y = y + 30; y1 = y + 15;
                //e.Graphics.DrawLine(pb2, x, y, 700, y);//画横线



                //e.Graphics.DrawLine(ptp, new Point(x + 10, y1), new Point(690, y1));
                //y = y + 30; y1 = y + 15;
                e.Graphics.DrawLine(ptp, x, YYY, x, y);//画竖线
                e.Graphics.DrawLine(ptp, x + 660, YYY, x + 660, y);//画竖线
                e.Graphics.DrawLine(ptp, x, y, x + 660, y);//画横线

                double hs = (double)StrLengthS / 55;
                if (hs > 5)
                {
                    e.HasMorePages = true;
                    pages++;
                    return;
                }
                else
                {
                    y = y + 20;
                    e.Graphics.DrawString("麻醉医生签名：" + cmbMZYS.Text, zt9, Brushes.Black, x + 480, y);
                    y = y + 20;
                    e.Graphics.DrawString(dtAddTime.Text, zt9, Brushes.Black, x + 520, y);
                    e.HasMorePages = false;
                    pages = 0;
                }
            }
            if (pages == 1)
            {
                int MZ_Y = 90; int MZ_Y1 = MZ_Y + 15;
                e.Graphics.DrawLine(ptp, new Point(x, MZ_Y), new Point(x + 660, MZ_Y));//画边框
                MZ_Y = MZ_Y + 10; MZ_Y1 = MZ_Y + 15;
                string MZZJ_END = tbRemarkMZ.Text.Trim().ToString().Substring(5 * 55, tbRemarkMZ.Text.Length - 5 * 55);//第二页剩余麻醉总结记录
                int BZYYS = MZ_Y, BZYYS1 = MZ_Y1;
                //List<string> strS = new List<string>();
                string strS1 = "";
                int StrLengthS = MZZJ_END.Length;
                int rowS = StrLengthS / 55;
                for (int i = 0; i <= rowS; i++)//35个字符就换行
                {
                    if (i < rowS)
                        strS1 = MZZJ_END.ToString().Substring(i * 55, 55); //从第i*30个开始，截取10个字符串
                    else
                        strS1 = MZZJ_END.ToString().Substring(i * 55);
                    e.Graphics.DrawString(strS1, zt8, Brushes.Black, x + 10, BZYYS);
                    BZYYS = BZYYS + 30; BZYYS1 = BZYYS + 15;
                }

                for (int i = 0; i < 28; i++)
                {
                    e.Graphics.DrawLine(ptp, new Point(x + 10, MZ_Y1), new Point(x + 650, MZ_Y1));
                    MZ_Y = MZ_Y + 30; MZ_Y1 = MZ_Y + 15;
                }
                e.Graphics.DrawLine(ptp, x, 90, x, MZ_Y);//画竖线
                e.Graphics.DrawLine(ptp, x + 660, MZ_Y, x + 660, 90);//画竖线
                e.Graphics.DrawLine(ptp, x, MZ_Y, x + 660, MZ_Y);//画横线

                MZ_Y = MZ_Y + 20;
                e.Graphics.DrawString("麻醉医生签名：" + cmbMZYS.Text, zt9, Brushes.Black, x + 480, MZ_Y);
                MZ_Y = MZ_Y + 20;
                e.Graphics.DrawString(dtAddTime.Text, zt9, Brushes.Black, x + 520, MZ_Y);
                e.HasMorePages = false;
                pages = 0;
            }

        }
        private void MZZJ_SZ_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isRead)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.MZZJ_SZ_FormClosing);
                this.Close();
            }
            else if (BCCount > 0)
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

        private void MZZJ_CJ_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                panel1.Left = 120;
            if (this.WindowState == FormWindowState.Normal)
                panel1.Left = 50;
        }
        adims_DAL.Flows.AnesthesiaSummaryDal _MzzjDal = new AnesthesiaSummaryDal();
        private void btnLock_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = _MzzjDal.GetMzzjByMzjldId(mzjldID);
            if (dt.Rows.Count == 0)
                MessageBox.Show("需要先保存，才能存档！");
            else
            {
                result = _MzzjDal.UpdateMZZJ_CJ(mzjldID, 1);
                if (result > 0)
                {
                    MessageBox.Show("存档成功！");
                    btnSave.Enabled = false;
                    isRead = true;
                    btnUnlock.Enabled = true;
                    btnLock.Enabled = false;
                }
            }
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = _MzjldDal.GetMzjldByMzjldId(mzjldID);
            if (dt.Rows[0]["isRead"].ToString() == "1")
            {
                result = _MzzjDal.UpdateMZZJ_CJ(mzjldID, 0);
                if (result > 0)
                {
                    MessageBox.Show("解锁成功！");
                    btnSave.Enabled = true;
                    isRead = false;
                    btnUnlock.Enabled = false;
                    btnLock.Enabled = true;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void tbliuzhiSD1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //adims_BLL.TextValueLimit.Text_Value_Limit(sender, e);
        }

        private void tbliuzhiSD2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //adims_BLL.TextValueLimit.Text_Value_Limit(sender, e);
        }

        private void tbQGCGsd_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextValueLimit.Text_Value_Limit(sender, e);
        }


        private void cmbCCD21_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                DataTable dt = _DataDicDal.GetMazuiPingmianByName(cmbCCD2.Text.Trim());
                cmbCCD2.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    cmbCCD2.Items.Add(dr["name"].ToString());
                }
                cmbCCD2.DroppedDown = true;
            }
        }

        private void cmbMZPM_ZG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                DataTable dt = _DataDicDal.GetMazuiPingmianByName(cmbMZPM_ZG.Text.Trim());
                cmbMZPM_ZG.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    cmbMZPM_ZG.Items.Add(dr["name"].ToString());
                }
                cmbMZPM_ZG.DroppedDown = true;
            }
        }

        private void cmbMZPM_OUT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                DataTable dt = _DataDicDal.GetMazuiPingmianByName(cmbMZPM_OUT.Text.Trim());
                cmbMZPM_OUT.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    cmbMZPM_OUT.Items.Add(dr["name"].ToString());
                }
                cmbMZPM_OUT.DroppedDown = true;
            }
        }

        private void cmbCCD11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                DataTable dt = _DataDicDal.GetMazuiPingmianByName(cmbCCD1.Text.Trim());
                cmbCCD1.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    cmbCCD1.Items.Add(dr["name"].ToString());
                }
                cmbCCD1.DroppedDown = true;
            }
        }

        private void cmbMZPM_OUT_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)32) || (e.KeyChar == (char)13);
        }

        private void cmbCCD11_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)32) || (e.KeyChar == (char)13);
        }

        private void cmbCCD21_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)32) || (e.KeyChar == (char)13);
        }

        private void cmbMZPM_ZG_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)32) || (e.KeyChar == (char)13);
        }
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbRemarkMZ_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Select_MZXJ f1 = new Select_MZXJ(tbRemarkMZ);
            f1.ShowDialog();
        }
        /// <summary>
        /// 留置深度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbliuzhiSD1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Select_LZSD f1 = new Select_LZSD(tbliuzhiSD1);
            f1.ShowDialog();
        }

        private void tbliuzhiSD2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Select_LZSD f1 = new Select_LZSD(tbliuzhiSD2);
            f1.ShowDialog();
        }

        private void tbTSQK_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Select_MZBGYY f1 = new Select_MZBGYY(tbTSQK);
            f1.ShowDialog();
        }
    }
}
