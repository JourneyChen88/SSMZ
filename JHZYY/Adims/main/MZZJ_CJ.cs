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
    public partial class MZZJ_CJ : Form
    {
        string mzjldID, patID;
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        bool isRead = false;
        public MZZJ_CJ(string mzid, string patid)
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
            tbBedNo.Text = dt.Rows[0]["Patbedno"].ToString();
            tbZhuyuanID.Text = dt.Rows[0]["Patzhuyuanid"].ToString();


        }
        private void BindMazuipingmian()
        {
            DataTable dt = DAL.GetMazuiPingmian("");
            cmbCCD11.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                cmbCCD11.Items.Add(dr["name"].ToString());
            }
            cmbCCD21.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                cmbCCD21.Items.Add(dr["name"].ToString());
            }
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
        }
             
        private void MZZJ_SZ_Load(object sender, EventArgs e)
        {
            panel1.Left = 60;   
            this.WindowState = FormWindowState.Maximized;                     
            BindPatInfo();
            DataTable dt1 = bll.selectUserName(1);
            cmbMZYS.Items.Clear();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                cmbMZYS.Items.Add(dt1.Rows[i][0].ToString());
            }
            BindMazuipingmian();
            DataTable dt = DAL.GetMZZJ_CJ(mzjldID);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                if (dr["addtime"].ToString() != "")
                    dtAddTime.Value = Convert.ToDateTime(dr["addtime"]);
                //if (dr["UpdateTime"].ToString() != "")
                //    dtAddTime.Value = Convert.ToDateTime(dt.Rows[0]["UpdateTime"]);
                if (Convert.ToString(dr["QSMZ"]) == "1") cbQSMZ.Checked = true;
                cmbYoudaoFF.Text=Convert.ToString(dr["YoudaoFF"]);
                if (Convert.ToString(dr["ZGNMZ"]) == "1") cbZGNMZ.Checked = true;
                cmbZGNMZFF.Text = Convert.ToString(dr["ZGNMZFF"]);
                cmbCCD11.Text = Convert.ToString(dr["CCD11"]);
                tbliuzhiSD1.Text = Convert.ToString(dr["liuzhiSD1"]);
                cmbCCD21.Text = Convert.ToString(dr["CCD21"]);
                tbliuzhiSD2.Text = Convert.ToString(dr["liuzhiSD2"]);
                cmbMZPM_ZG.Text = Convert.ToString(dr["MZPM_ZG"]);
                tbYaopin_ZG.Text = Convert.ToString(dr["Yaopin_ZG"]);

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
               
                tbYaopin_SJZZ.Text = Convert.ToString(dr["Yaopin_SJZZ"]);
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
                string jurisdiction = bll.Get_user_jurisdiction(Program.customer);
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
                zjdan.Add("CCD11", cmbCCD11.Text);
                zjdan.Add("CCD12", cmbCCD11.Text);
                zjdan.Add("liuzhiSD1", tbliuzhiSD1.Text.Trim());              
                zjdan.Add("CCD21", cmbCCD21.Text);
                zjdan.Add("CCD22", cmbCCD21.Text);
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
                DataTable dt = DAL.GetMZZJ_CJ(mzjldID);
                if (dt.Rows.Count > 0)
                    result = DAL.UpdateMZZJ_CJ(zjdan);
                else
                    result = DAL.InsertMZZJ_CJ(zjdan);
                if (result > 0)
                {
                    BCCount++; MessageBox.Show("保存成功！");
                }
                else MessageBox.Show("保存失败！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()+"保存出现异常，请检查网络！");
            }
        }

        
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            //printDocument1.DefaultPageSettings.PaperSize = 
            //    new PaperSize("16K", 737, 1020);
            printDocument1.DefaultPageSettings.PaperSize =
               new PaperSize("A4", 820, 1160);

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font zt8 = new Font(new FontFamily("宋体"),8);
            Font zt9 = new Font(new FontFamily("宋体"), 9);
            Font ht9 = new Font(new FontFamily("黑体"), 9);
            Font zt14 = new Font(new FontFamily("宋体"), 12);

            Pen ptp = new Pen(Brushes.Black);
            Pen pb2 = new Pen(Brushes.Black, 2);
            int x = 40, y = 30, y1 = y + 15;
            e.Graphics.DrawLine(pb2, x, y, 790, y);//画横线

            e.Graphics.DrawLine(pb2, x, y + 5, 790, y + 5);
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("麻醉经过： ", zt14, Brushes.Black, x + 20, y);
            e.Graphics.DrawString("离   室", zt14, Brushes.Black, x + 610, y);
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x+20 , y, 12, 12);
            e.Graphics.DrawString("全身麻醉 ", ht9, Brushes.Black, x + 40, y);
            if (cbQSMZ.Checked)
            {
                e.Graphics.DrawLine(pb2, x +15, y, x + 25, y + 12);
                e.Graphics.DrawLine(pb2, x + 25, y + 12, x + 40, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black,x+230,y,12,12);
            e.Graphics.DrawString("椎管内麻醉 ", ht9, Brushes.Black, x + 250, y);
            if (cbZGNMZ.Checked)
            {
                e.Graphics.DrawLine(pb2, x +225, y, x + 235, y + 12);
                e.Graphics.DrawLine(pb2, x + 235, y + 12, x + 250, y - 3);
            }
            e.Graphics.DrawString("肌力恢复：", zt9, Brushes.Black, x + 550, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 660, y, 12, 12);
            e.Graphics.DrawString("好", zt9, Brushes.Black, x + 680, y);
            if (cmbJili.Text == "好")
            {
                e.Graphics.DrawLine(pb2, x + 655, y, x + 665, y + 12);
                e.Graphics.DrawLine(pb2, x + 665, y + 12, x + 680, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 700, y, 12, 12);
            e.Graphics.DrawString("差", zt9, Brushes.Black, x + 720, y);
            if (cmbJili.Text == "差")
            {
                e.Graphics.DrawLine(pb2, x + 695, y, x + 705, y + 12);
                e.Graphics.DrawLine(pb2, x + 705, y + 12, x + 720, y - 3);
            }
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("诱导方法：",zt9,Brushes.Black,x+40,y);
            e.Graphics.DrawRectangle(Pens.Black, x + 250, y, 12, 12);
            e.Graphics.DrawString("腰麻", zt9, Brushes.Black, x + 270, y);
            if (cmbZGNMZFF.Text=="腰麻")
            {
                e.Graphics.DrawLine(pb2, x +245, y, x + 255, y + 12);
                e.Graphics.DrawLine(pb2, x + 255, y + 12, x + 270, y - 3);           
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 350, y, 12, 12);
            e.Graphics.DrawString("硬膜外麻醉", zt9, Brushes.Black, x + 370, y);
            if (cmbZGNMZFF.Text=="硬膜外麻醉")
            {
                e.Graphics.DrawLine(pb2, x +345, y, x + 355, y + 12);
                e.Graphics.DrawLine(pb2, x + 355, y + 12, x + 370, y - 3); 
            }
            e.Graphics.DrawString("咳嗽吞咽反射：", zt9, Brushes.Black, x + 550, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 660, y, 12, 12);
            e.Graphics.DrawString("有", zt9, Brushes.Black, x + 680, y);
            if (cmbKesouTunyan.Text == "有")
            {
                e.Graphics.DrawLine(pb2, x + 655, y, x + 665, y + 12);
                e.Graphics.DrawLine(pb2, x + 665, y + 12, x + 680, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 700, y, 12, 12);
            e.Graphics.DrawString("无", zt9, Brushes.Black, x + 720, y);
            if (cmbKesouTunyan.Text == "无")
            {
                e.Graphics.DrawLine(pb2, x + 695, y, x + 705, y + 12);
                e.Graphics.DrawLine(pb2, x + 705, y + 12, x + 720, y - 3);
            }
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x + 40, y, 12, 12);
            e.Graphics.DrawString("快诱导", zt9, Brushes.Black, x + 60, y);
            if (cmbYoudaoFF.Text=="快诱导")
            {
                e.Graphics.DrawLine(pb2, x +35, y, x + 45, y + 12);
                e.Graphics.DrawLine(pb2, x + 45, y + 12, x + 60, y - 3); 
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 250, y-5, 12, 12);
            e.Graphics.DrawString("腰硬联合麻醉", zt9, Brushes.Black, x + 270, y-5);
            if (cmbZGNMZFF.Text=="腰硬联合麻醉")
            {
                e.Graphics.DrawLine(pb2, x +245, y-5, x + 255, y + 12-5);
                e.Graphics.DrawLine(pb2, x + 255, y-5 + 12, x + 270, y - 3-5);     
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 350, y-5, 12, 12);
            e.Graphics.DrawString("骶麻", zt9, Brushes.Black, x + 370, y-5);
            if (cmbZGNMZFF.Text == "骶麻")
            {
                e.Graphics.DrawLine(pb2, x + 345, y-5, x + 355, y + 12-5);
                e.Graphics.DrawLine(pb2, x + 355, y + 12-5, x + 370, y - 3-5);
            }
            e.Graphics.DrawString("定向力恢复：", zt9, Brushes.Black, x + 550, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 660, y, 12, 12);
            e.Graphics.DrawString("是", zt9, Brushes.Black, x + 680, y);
            if (cmbDingxiangli.Text == "是")
            {
                e.Graphics.DrawLine(pb2, x + 655, y, x + 665, y + 12);
                e.Graphics.DrawLine(pb2, x + 665, y + 12, x + 680, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 700, y, 12, 12);
            e.Graphics.DrawString("否", zt9, Brushes.Black, x + 720, y);
            if (cmbDingxiangli.Text == "否")
            {
                e.Graphics.DrawLine(pb2, x + 695, y, x + 705, y + 12);
                e.Graphics.DrawLine(pb2, x + 705, y + 12, x + 720, y - 3);
            }
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x + 40, y, 12, 12);
            e.Graphics.DrawString("慢诱导", zt9, Brushes.Black, x + 60, y);
            if (cmbYoudaoFF.Text == "慢诱导")
            {
                e.Graphics.DrawLine(pb2, x + 35, y, x + 45, y + 12);
                e.Graphics.DrawLine(pb2, x + 45, y + 12, x + 60, y - 3);
            }
            e.Graphics.DrawString("穿刺点1：" + cmbCCD11.Text, zt9, Brushes.Black, x + 250, y - 5);
            e.Graphics.DrawLine(ptp,new Point(x + 300,y1-5),new Point(x+350,y1-5));
            e.Graphics.DrawString("留置深度：" + tbliuzhiSD1.Text+"     cm", zt9, Brushes.Black, x + 360, y - 5);
            e.Graphics.DrawLine(ptp, new Point(x + 410, y1 - 5), new Point(x + 480, y1 - 5));
            //e.Graphics.DrawString("意识：□ 清醒  □ 嗜睡  □ 麻醉状态", zt9, Brushes.Black, x + 550, y);
            //if (cmbYishi.Text == "清醒")
            //{
            //    e.Graphics.DrawLines(pb2, new Point[] { new Point(x + 580, y), new Point(x + 590, y + 12), new Point(x + 605, y) });
            //}
            //if (cmbYishi.Text == "嗜睡")
            //{
            //    e.Graphics.DrawLines(pb2, new Point[] { new Point(x + 630, y), new Point(x + 640, y + 12), new Point(x + 655, y) });
            //}
            //if (cmbYishi.Text == "麻醉状态")
            //{
            //    e.Graphics.DrawLines(pb2, new Point[] { new Point(x + 680, y), new Point(x + 690, y + 12), new Point(x + 705, y) });
            //}

            e.Graphics.DrawString("意识：", zt9, Brushes.Black, x + 550, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 590, y, 12, 12);
            e.Graphics.DrawString("清醒", zt9, Brushes.Black, x + 610, y);
            if (cmbYishi.Text == "清醒")
            {
                e.Graphics.DrawLine(pb2, x + 585, y, x + 595, y + 12);
                e.Graphics.DrawLine(pb2, x + 595, y + 12, x + 610, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 650, y, 12, 12);
            e.Graphics.DrawString("嗜睡", zt9, Brushes.Black, x + 670, y);
            if (cmbYishi.Text == "嗜睡")
            {
                e.Graphics.DrawLine(pb2, x + 645, y, x + 655, y + 12);
                e.Graphics.DrawLine(pb2, x + 655, y + 12, x + 670, y - 3);
            }
            //e.Graphics.DrawRectangle(Pens.Black, x + 690, y, 12, 12);
            //e.Graphics.DrawString("麻醉状态", zt9, Brushes.Black, x + 710, y);
            //if (cmbYishi.Text == "麻醉状态")
            //{
            //    e.Graphics.DrawLine(pb2, x + 685, y, x + 695, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 695, y + 12, x + 710, y - 3);
            //}
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("穿刺点2：" + cmbCCD21.Text, zt9, Brushes.Black, x + 250, y - 5);
            e.Graphics.DrawLine(ptp, new Point(x + 300, y1 - 5), new Point(x + 350, y1 - 5));
            e.Graphics.DrawString("留置深度：" + tbliuzhiSD2.Text + "     cm", zt9, Brushes.Black, x + 360, y - 5);
            e.Graphics.DrawLine(ptp, new Point(x + 410, y1 - 5), new Point(x + 480, y1 - 5));
            e.Graphics.DrawRectangle(Pens.Black, x + 560, y, 12, 12);
            e.Graphics.DrawString("麻醉状态", zt9, Brushes.Black, x + 580, y);
            if (cmbYishi.Text == "麻醉状态")
            {
                e.Graphics.DrawLine(pb2, x + 555, y, x + 565, y + 12);
                e.Graphics.DrawLine(pb2, x + 565, y + 12, x + 580, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 640, y, 12, 12);
            e.Graphics.DrawString("谵妄", zt9, Brushes.Black, x + 660, y);
            if (cmbYishi.Text == "谵妄")
            {
                e.Graphics.DrawLine(pb2, x + 635, y, x + 645, y + 12);
                e.Graphics.DrawLine(pb2, x + 645, y + 12, x + 660, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 690, y, 12, 12);
            e.Graphics.DrawString("昏迷", zt9, Brushes.Black, x + 710, y);
            if (cmbYishi.Text == "昏迷")
            {
                e.Graphics.DrawLine(pb2, x + 685, y, x + 695, y + 12);
                e.Graphics.DrawLine(pb2, x + 695, y + 12, x + 710, y - 3);
            }
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("麻醉平面：" + cmbMZPM_ZG.Text, zt9, Brushes.Black, x + 250, y - 5);
            e.Graphics.DrawLine(ptp, new Point(x + 300, y1 - 5), new Point(x + 480, y1 - 5));
            e.Graphics.DrawString("病人自控镇痛：", zt9, Brushes.Black, x + 550, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 660, y, 12, 12);
            e.Graphics.DrawString("是", zt9, Brushes.Black, x + 680, y);
            if (cmbBRZKZT.Text == "是")
            {
                e.Graphics.DrawLine(pb2, x + 655, y, x + 665, y + 12);
                e.Graphics.DrawLine(pb2, x + 665, y + 12, x + 680, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 700, y, 12, 12);
            e.Graphics.DrawString("否", zt9, Brushes.Black, x + 720, y);
            if (cmbBRZKZT.Text == "否")
            {
                e.Graphics.DrawLine(pb2, x + 695, y, x + 705, y + 12);
                e.Graphics.DrawLine(pb2, x + 705, y + 12, x + 720, y - 3);
            }
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x + 20, y, 12, 12);
            e.Graphics.DrawString("气管插管 ", ht9, Brushes.Black, x + 40, y);
            if (cbQGCG.Checked)
            {
                e.Graphics.DrawLine(pb2, x +15, y, x + 25, y + 12);
                e.Graphics.DrawLine(pb2, x + 25, y + 12, x + 40, y - 3);
            }
            e.Graphics.DrawString("药品：" + tbYaopin_ZG.Text, zt9, Brushes.Black, x + 250, y - 5);
            e.Graphics.DrawLine(ptp, new Point(x + 280, y1 - 5), new Point(x + 480, y1 - 5));
            e.Graphics.DrawString("麻醉平面：" + cmbMZPM_OUT.Text, zt9, Brushes.Black, x + 550, y);
            e.Graphics.DrawLine(ptp, new Point(x + 600, y1 ), new Point(x + 720, y1));
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x + 40, y, 12, 12);
            e.Graphics.DrawString("气管内", zt9, Brushes.Black, x + 60, y);
            if (cmbQGCGwz1.Text=="气管内")
            {
                e.Graphics.DrawLine(pb2, x +35, y, x + 45, y + 12);
                e.Graphics.DrawLine(pb2, x + 45, y + 12, x + 60, y - 3); 
            }
            //计算字数换行
            //int BZYY = y, BZYY1 = y1;
            //List<string> str = new List<string>();
            //int StrLength = tbRemarkOut.Text.Trim().Length;
            //int row = StrLength / 10;
            //for (int i = 0; i <= row; i++)//25个字符就换行
            //{
            //    str[i] = tbRemarkOut.Text.Trim().ToString().Substring(i * 10, 10); //从第i*10个开始，截取10个字符串
            //    e.Graphics.DrawString("备注：" + str[i], zt9, Brushes.Black, x + 550, BZYY);
            //    BZYY = BZYY + 30; BZYY1 = BZYY + 15;
            //}
            int BZYY = y, BZYY1 = y1;
            List<string> str = new List<string>();
            string str1 = "";
            int StrLength = tbRemarkOut.Text.Trim().Length;
            int row = StrLength / 11;
            e.Graphics.DrawString("备注：", zt9, Brushes.Black, x + 550, BZYY);
            for (int i = 0; i <= row; i++)//11个字符就换行
            {
                if (i < row)
                    str1 = tbRemarkOut.Text.Trim().ToString().Substring(i * 11, 11); //从第i*11个开始，截取11个字符串
                else
                    str1 = tbRemarkOut.Text.Trim().ToString().Substring(i * 11);
                e.Graphics.DrawString(str1, zt9, Brushes.Black, x + 580, BZYY);
                BZYY = BZYY + 30; BZYY1 = BZYY + 15;
            }
           // e.Graphics.DrawString("备注：" + tbRemarkOut.Text, zt9, Brushes.Black, x + 550, y);
            int YY = y, YY1 = y1;
            e.Graphics.DrawLine(ptp, new Point(x + 580, y1), new Point(x + 720, y1 ));
            YY = YY+30; YY1 = YY + 15;
            e.Graphics.DrawLine(ptp, new Point(x + 580, YY1), new Point(x + 720, YY1));
            YY = YY + 30; YY1 = YY + 15;
            e.Graphics.DrawLine(ptp, new Point(x + 580, YY1), new Point(x + 720, YY1));
            YY = YY + 30; YY1 = YY + 15;
            e.Graphics.DrawLine(ptp, new Point(x + 580, YY1), new Point(x + 720, YY1));
            YY = YY + 30; YY1 = YY + 15;
            e.Graphics.DrawLine(ptp, new Point(x + 580, YY1), new Point(x + 720, YY1));
            YY = YY + 30; YY1 = YY + 15;
            e.Graphics.DrawLine(ptp, new Point(x + 580, YY1), new Point(x + 720, YY1));
            YY = YY + 30; YY1 = YY + 15;
            e.Graphics.DrawLine(ptp, new Point(x + 580, YY1), new Point(x + 720, YY1));
            YY = YY + 30; YY1 = YY + 15;
            e.Graphics.DrawLine(ptp, new Point(x + 580, YY1), new Point(x + 720, YY1));
            YY = YY + 30; YY1 = YY + 15;
            e.Graphics.DrawLine(ptp, new Point(x + 580, YY1), new Point(x + 720, YY1));
            YY = YY + 30; YY1 = YY + 15;
            e.Graphics.DrawLine(ptp, new Point(x + 580, YY1), new Point(x + 720, YY1));
 
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x + 40, y, 12, 12);
            e.Graphics.DrawString("支气管内", zt9, Brushes.Black, x + 60, y);
            
            if (cmbQGCGwz1.Text == "支气管内")
            {
                e.Graphics.DrawLine(pb2, x + 35, y, x + 45, y + 12);
                e.Graphics.DrawLine(pb2, x + 45, y + 12, x + 60, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 250, y, 12, 12);
            e.Graphics.DrawString("神经阻滞", zt9, Brushes.Black, x + 270, y);
            if (cbSJZZ.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 245, y , x + 255, y + 12);
                e.Graphics.DrawLine(pb2, x + 255, y + 12, x + 270, y - 3 );
            }
         
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x + 40, y, 12, 12);
            e.Graphics.DrawString("左", zt9, Brushes.Black, x + 60, y);
            if (cmbQGCGwz2.Text=="左")
            {
                e.Graphics.DrawLine(pb2, x + 35, y, x + 45, y + 12);
                e.Graphics.DrawLine(pb2, x + 45, y + 12, x + 60, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 80, y, 12, 12);
            e.Graphics.DrawString("右", zt9, Brushes.Black, x + 100, y);
            if (cmbQGCGwz2.Text == "右")
            {
                e.Graphics.DrawLine(pb2, x + 75, y, x + 85, y + 12);
                e.Graphics.DrawLine(pb2, x + 85, y + 12, x + 100, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 270, y, 12, 12);
            e.Graphics.DrawString("颈丛神经阻滞：", zt9, Brushes.Black, x + 290, y);
            if (cbJCSJZZ.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 265, y, x + 275, y + 12);
                e.Graphics.DrawLine(pb2, x + 275, y + 12, x + 290, y - 3);
            }
     
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x + 40, y, 12, 12);
            e.Graphics.DrawString("经口", zt9, Brushes.Black, x + 60, y);
            if (cmbQGCGwz3.Text == "经口")
            {
                e.Graphics.DrawLine(pb2, x + 35, y, x + 45, y + 12);
                e.Graphics.DrawLine(pb2, x + 45, y + 12, x + 60, y - 3);
            }
            //e.Graphics.DrawLine(ptp, x + 65, y, x + 70, y - 5);
            e.Graphics.DrawRectangle(Pens.Black, x + 100, y, 12, 12);
            e.Graphics.DrawString("经鼻", zt9, Brushes.Black, x + 120, y);
            if (cmbQGCGwz3.Text == "经鼻")
            {
                e.Graphics.DrawLine(pb2, x + 95, y, x + 105, y + 12);
                e.Graphics.DrawLine(pb2, x + 105, y + 12, x + 120, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 160, y, 12, 12);
            e.Graphics.DrawString("经气管造口", zt9, Brushes.Black, x + 180, y);
            if (cmbQGCGwz3.Text == "经气管造口")
            {
                e.Graphics.DrawLine(pb2, x + 155, y, x + 165, y + 12);
                e.Graphics.DrawLine(pb2, x + 165, y + 12, x + 180, y - 3);
            }
            e.Graphics.DrawString("浅丛", zt9, Brushes.Black, x + 290, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 320, y, 12, 12);
            e.Graphics.DrawString("左", zt9, Brushes.Black, x + 340, y);
            if (cbJCSJZZqcLeft.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 315, y, x + 325, y + 12);
                e.Graphics.DrawLine(pb2, x + 325, y + 12, x + 340, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 12, 12);
            e.Graphics.DrawString("右", zt9, Brushes.Black, x + 380, y);
            if (cbJCSJZZqcRight.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 355, y, x + 365, y + 12);
                e.Graphics.DrawLine(pb2, x + 365, y + 12, x + 380, y - 3);
            }

            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("型号：" + tbQGCGtype.Text, zt9, Brushes.Black, x + 40, y);
            e.Graphics.DrawLine(ptp, new Point(x + 70, y1), new Point(x + 200, y1));
            e.Graphics.DrawString("浅丛", zt9, Brushes.Black, x + 290, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 320, y, 12, 12);
            e.Graphics.DrawString("左", zt9, Brushes.Black, x + 340, y);
            if (cbJCSJZZscLeft.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 315, y, x + 325, y + 12);
                e.Graphics.DrawLine(pb2, x + 325, y + 12, x + 340, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 12, 12);
            e.Graphics.DrawString("右", zt9, Brushes.Black, x + 380, y);
            if (cbJCSJZZscRight.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 355, y, x + 365, y + 12);
                e.Graphics.DrawLine(pb2, x + 365, y + 12, x + 380, y - 3);
            }
            e.Graphics.DrawString("c", zt9, Brushes.Black, x + 420, y);
            e.Graphics.DrawLine(ptp, new Point(x + 430, y1), new Point(x + 480, y1));
       
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("深度：" + tbQGCGsd.Text+"        cm", zt9, Brushes.Black, x + 40, y);
            e.Graphics.DrawLine(ptp, new Point(x + 70, y1), new Point(x + 150, y1));
            e.Graphics.DrawRectangle(Pens.Black, x + 270, y, 12, 12);
            e.Graphics.DrawString("臂丛神经阻滞：", zt9, Brushes.Black, x + 290, y);
            if (cbBCSJZZ.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 265, y, x + 275, y + 12);
                e.Graphics.DrawLine(pb2, x + 275, y + 12, x + 290, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 420, y, 12, 12);
            e.Graphics.DrawString("左", zt9, Brushes.Black, x + 440, y);
            if (cbBCSJZZleft.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 415, y, x + 425, y + 12);
                e.Graphics.DrawLine(pb2, x + 425, y + 12, x + 440, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 460, y, 12, 12);
            e.Graphics.DrawString("右", zt9, Brushes.Black, x + 480, y);
            if (cbBCSJZZright.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 455, y, x + 465, y + 12);
                e.Graphics.DrawLine(pb2, x + 465, y + 12, x + 480, y - 3);
            }
        
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("套囊：", zt9, Brushes.Black, x + 40, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 80, y, 12, 12);
            e.Graphics.DrawString("有", zt9, Brushes.Black, x + 100, y);
            if (cmbTaoNang.Text=="有")
            {
                e.Graphics.DrawLine(pb2, x + 75, y, x + 85, y + 12);
                e.Graphics.DrawLine(pb2, x + 85, y + 12, x + 100, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 120, y, 12, 12);
            e.Graphics.DrawString("无", zt9, Brushes.Black, x + 140, y);
            if (cmbTaoNang.Text == "无")
            {
                e.Graphics.DrawLine(pb2, x + 115, y, x + 125, y + 12);
                e.Graphics.DrawLine(pb2, x + 125, y + 12, x + 140, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 290, y, 12, 12);
            e.Graphics.DrawString("肌间沟法", zt9, Brushes.Black, x + 310, y);
            if (cbBCSJZZjjgf.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 285, y, x + 295, y + 12);
                e.Graphics.DrawLine(pb2, x + 295, y + 12, x + 310, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 370, y, 12, 12);
            e.Graphics.DrawString("腋路法", zt9, Brushes.Black, x + 390, y);
            if (cbBCSJZZylf.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 365, y, x + 375, y + 12);
                e.Graphics.DrawLine(pb2, x + 375, y + 12, x + 390, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 440, y, 12, 12);
            e.Graphics.DrawString("锁骨上法", zt9, Brushes.Black, x + 460, y);
            if (cbBCSJZZsgsf.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 435, y, x + 445, y + 12);
                e.Graphics.DrawLine(pb2, x + 445, y + 12, x + 460, y - 3);
            }
           
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x + 270, y, 12, 12);
            e.Graphics.DrawString("腰丛神经阻滞：", zt9, Brushes.Black, x + 290, y);
            if (cbYCSJZZ.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 265, y, x + 275, y + 12);
                e.Graphics.DrawLine(pb2, x + 275, y + 12, x + 290, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 420, y, 12, 12);
            e.Graphics.DrawString("左", zt9, Brushes.Black, x + 440, y);
            if (cbYCSJZZleft.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 415, y, x + 425, y + 12);
                e.Graphics.DrawLine(pb2, x + 425, y + 12, x + 440, y - 3);
            }

            e.Graphics.DrawRectangle(Pens.Black, x + 460, y, 12, 12);
            e.Graphics.DrawString("右", zt9, Brushes.Black, x + 480, y);
            if (cbYCSJZZright.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 455, y, x + 465, y + 12);
                e.Graphics.DrawLine(pb2, x + 465, y + 12, x + 480, y - 3);
            }

            y = y + 30; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x + 270, y, 12, 12);
            e.Graphics.DrawString("坐骨神经阻滞：", zt9, Brushes.Black, x + 290, y);
            if (cbZGSJZZ.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 265, y, x + 275, y + 12);
                e.Graphics.DrawLine(pb2, x + 275, y + 12, x + 290, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 420, y, 12, 12);
            e.Graphics.DrawString("左", zt9, Brushes.Black, x + 440, y);
            if (cbZGSJZZleft.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 415, y, x + 425, y + 12);
                e.Graphics.DrawLine(pb2, x + 425, y + 12, x + 440, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 460, y, 12, 12);
            e.Graphics.DrawString("右", zt9, Brushes.Black, x + 480, y);
            if (cbZGSJZZright.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 455, y, x + 465, y + 12);
                e.Graphics.DrawLine(pb2, x + 465, y + 12, x + 480, y - 3);
            }
      
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x + 270, y, 12, 12);
            e.Graphics.DrawString("股神经阻滞：", zt9, Brushes.Black, x + 290, y);
            if (cbGSJZZ.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 265, y, x + 275, y + 12);
                e.Graphics.DrawLine(pb2, x + 275, y + 12, x + 290, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 420, y, 12, 12);
            e.Graphics.DrawString("左", zt9, Brushes.Black, x + 440, y);
            if (cbGSJZZleft.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 415, y, x + 425, y + 12);
                e.Graphics.DrawLine(pb2, x + 425, y + 12, x + 440, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 460, y, 12, 12);
            e.Graphics.DrawString("右", zt9, Brushes.Black, x + 480, y);
            if (cbGSJZZright.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 455, y, x + 465, y + 12);
                e.Graphics.DrawLine(pb2, x + 465, y + 12, x + 480, y - 3);
            }
           
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x + 20, y, 12, 12);
            e.Graphics.DrawString("喉罩 ", ht9, Brushes.Black, x + 40, y);
            if (cbHZ.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 15, y, x + 25, y + 12);
                e.Graphics.DrawLine(pb2, x + 25, y + 12, x + 40, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 270, y, 12, 12);
            e.Graphics.DrawString("股外侧皮神经阻滞：", zt9, Brushes.Black, x + 290, y);
            if (cbGWCSJZZ.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 265, y, x + 275, y + 12);
                e.Graphics.DrawLine(pb2, x + 275, y + 12, x + 290, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 420, y, 12, 12);
            e.Graphics.DrawString("左", zt9, Brushes.Black, x + 440, y);
            if (cbGWCSJZZleft.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 415, y, x + 425, y + 12);
                e.Graphics.DrawLine(pb2, x + 425, y + 12, x + 440, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 460, y, 12, 12);
            e.Graphics.DrawString("右", zt9, Brushes.Black, x + 480, y);
            if (cbGWCSJZZright.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 455, y, x + 465, y + 12);
                e.Graphics.DrawLine(pb2, x + 465, y + 12, x + 480, y - 3);
            }
            e.Graphics.DrawString("去   向", zt14, Brushes.Black, x + 610, y+20);
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("型号：" + tbHZType.Text, zt9, Brushes.Black, x + 40, y);
            e.Graphics.DrawLine(ptp, new Point(x + 70, y1), new Point(x + 200, y1));
            e.Graphics.DrawString("药品：" + tbYaopin_SJZZ.Text, zt9, Brushes.Black, x + 270, y);
            e.Graphics.DrawLine(ptp, new Point(x + 300, y1), new Point(x + 480, y1));
            e.Graphics.DrawRectangle(Pens.Black, x + 550, y+30, 12, 12);
            e.Graphics.DrawString("PACU ", ht9, Brushes.Black, x + 570, y+30);
            if (cmbBRQX.Text=="PACU")
            {
                e.Graphics.DrawLine(pb2, x + 545, y+30, x + 555, y + 12+30);
                e.Graphics.DrawLine(pb2, x + 555, y+30 + 12, x + 570, y - 3+30);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 630, y+30, 12, 12);
            e.Graphics.DrawString("ICU ", ht9, Brushes.Black, x + 650, y+30);
            if (cmbBRQX.Text == "ICU")
            {
                e.Graphics.DrawLine(pb2, x + 625, y+30, x + 635, y + 12+30);
                e.Graphics.DrawLine(pb2, x + 635, y +30+ 12, x + 650, y - 3+30);
            }
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("维持方法：" + tbWCFF.Text, zt9, Brushes.Black, x + 20, y);
            e.Graphics.DrawLine(ptp, new Point(x + 70, y1), new Point(x + 200, y1));
            e.Graphics.DrawRectangle(Pens.Black, x + 270, y, 12, 12);
            e.Graphics.DrawString("有创操作：", zt9, Brushes.Black, x + 290, y);
            if (cbYCCZ.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 265, y, x + 275, y + 12);
                e.Graphics.DrawLine(pb2, x + 275, y + 12, x + 290, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 550, y + 40, 12, 12);
            e.Graphics.DrawString("病房 ", ht9, Brushes.Black, x + 570, y + 40);
            if (cmbBRQX.Text == "病房")
            {
                e.Graphics.DrawLine(pb2, x + 545, y + 40, x + 555, y + 12 + 40);
                e.Graphics.DrawLine(pb2, x + 555, y + 40 + 12, x + 570, y - 3 + 40);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 630, y + 40, 12, 12);
            e.Graphics.DrawString("门/急诊观察室", ht9, Brushes.Black, x + 650, y + 40);
            if (cmbBRQX.Text == "门/急诊观察室")
            {
                e.Graphics.DrawLine(pb2, x + 625, y + 40, x + 635, y + 12 + 40);
                e.Graphics.DrawLine(pb2, x + 635, y + 40 + 12, x + 650, y - 3 + 40);
            }
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x + 40, y, 12, 12);
            e.Graphics.DrawString("麻醉监护", zt9, Brushes.Black, x + 60, y);
            if (cbMZJH.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 35, y, x + 45, y + 12);
                e.Graphics.DrawLine(pb2, x + 45, y + 12, x + 60, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 290, y, 12, 12);
            e.Graphics.DrawString("动脉穿刺置管：", zt9, Brushes.Black, x + 310, y);
            if (cbDMCCZG.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 285, y, x + 295, y + 12);
                e.Graphics.DrawLine(pb2, x + 295, y + 12, x + 310, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 400, y, 12, 12);
            e.Graphics.DrawString("深静脉穿刺置管：", zt9, Brushes.Black, x + 420, y);
            if (cbSJMCCZG.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 395, y, x + 405, y + 12);
                e.Graphics.DrawLine(pb2, x + 405, y + 12, x + 420, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 550, y + 50, 12, 12);
            e.Graphics.DrawString("返家 ", ht9, Brushes.Black, x + 570, y + 50);
            if (cmbBRQX.Text == "返家")
            {
                e.Graphics.DrawLine(pb2, x + 545, y + 50, x + 555, y + 12 + 50);
                e.Graphics.DrawLine(pb2, x + 555, y + 50 + 12, x + 570, y - 3 + 50);
            }
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x + 290, y, 12, 12);
            e.Graphics.DrawString("其他：" + tbOtherZZ.Text, zt9, Brushes.Black, x + 310, y);
            if (cbQT.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 285, y, x + 295, y + 12);
                e.Graphics.DrawLine(pb2, x + 295, y + 12, x + 310, y - 3);
            }
            e.Graphics.DrawLine(ptp, new Point(x + 340, y1), new Point(x + 480, y1));
            y = y + 60; y1 = y + 15;
            int BZYYS = y, BZYYS1 = y1;
            //List<string> strS = new List<string>();
            string strS1 = "";
            int StrLengthS = tbRemarkMZ.Text.Trim().Length;
            int rowS = StrLengthS / 34;
            e.Graphics.DrawString("备注：", zt9, Brushes.Black, x + 20, BZYYS);
            for (int i = 0; i <= rowS; i++)//25个字符就换行
            {
                if (i < rowS)
                    strS1 = tbRemarkMZ.Text.Trim().ToString().Substring(i * 34, 34); //从第i*10个开始，截取10个字符串
                else
                    strS1 = tbRemarkMZ.Text.Trim().ToString().Substring(i * 34);
                e.Graphics.DrawString(strS1, zt9, Brushes.Black, x + 50, BZYYS);
                BZYYS = BZYYS + 30; BZYYS1 = BZYYS + 15;
            }
            //e.Graphics.DrawString("备注：" + tbRemarkMZ.Text, zt9, Brushes.Black, x + 20, y);
            e.Graphics.DrawLine(ptp, new Point(x + 50, y1), new Point(x + 480, y1));
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x + 20, y1), new Point(x + 480, y1));

            y = y + 30; y1 = y + 15;
            e.Graphics.DrawLine(pb2, x, y, 790, y);//画横线
            e.Graphics.DrawLine(pb2, 580, 35, 580, y);//画中间的竖线
            y = y + 40; y1 = y + 15;
            int BZYYSS = y, BZYYSS1 = y1;            
            string strSS1 = "";
            int StrLengthSS = tbTSQK.Text.Trim().Length;
            int rowSS = StrLengthSS / 54;
            e.Graphics.DrawString("变更麻醉方法及原因：", zt14, Brushes.Black, x + 20, BZYYSS);
            for (int i = 0; i <= rowSS; i++)//54个字符就换行
            {
                if (i < rowSS)
                    strSS1 = tbTSQK.Text.Trim().ToString().Substring(i * 54, 54); //从第i*54个开始，截取54个字符串
                else
                    strSS1 = tbTSQK.Text.Trim().ToString().Substring(i * 54);

                BZYYSS = BZYYSS + 40; BZYYSS1 = BZYYSS + 15;
                e.Graphics.DrawString(strSS1, zt9, Brushes.Black, x + 20, BZYYSS);
               // BZYYSS = BZYYSS + 40; BZYYSS1 = BZYYSS + 15;
            }
            //e.Graphics.DrawString("变更麻醉方法及原因：" + tbTSQK.Text, zt14, Brushes.Black, x + 20, y);
            //e.Graphics.DrawLine(ptp, new Point(x + 50, y1), new Point(750, y1));
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x + 20, y1), new Point( 750, y1));
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x + 20, y1), new Point(750, y1));
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x + 20, y1), new Point(750, y1));
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawLine(pb2, x, 30, x, y);//画竖线
            e.Graphics.DrawLine(pb2, 790, 30, 790, y);//画竖线
            e.Graphics.DrawLine(pb2, x, y, 790, y);//画横线
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("麻醉医生签名：" + cmbMZYS.Text, zt9, Brushes.Black, x + 580, y);
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString(dtAddTime.Text, zt9, Brushes.Black, x + 640, y);

            //Font zt8 = new Font(new FontFamily("宋体"), 8);
            //Font zt9 = new Font(new FontFamily("宋体"), 9);
            //Font ht9 = new Font(new FontFamily("黑体"), 9);
            //Font zt14 = new Font(new FontFamily("宋体"), 13,FontStyle.Bold);

            //Pen ptp = new Pen(Brushes.Black);
            //Pen pb2 = new Pen(Brushes.Black, 2);
           
            //int x = 40, y = 30, y1 = y + 15;
           
            //string title1 = "昌吉州人民医院麻醉总结单";
            //e.Graphics.DrawString(title1, zt14, Brushes.Black, x + 200, y);
           


            //y = y + 50; y1 = y + 13;
            //e.Graphics.DrawString("姓名 " + tbPatname.Text, zt9, Brushes.Black, x + 0, y);
            //e.Graphics.DrawLine(Pens.Black, x + 25, y1, x + 110, y1);           
            //e.Graphics.DrawString("性别 " + cmbSex.Text, zt9, Brushes.Black, x + 120, y);
            //e.Graphics.DrawLine(Pens.Black, x + 145, y1, x + 230, y1);            
            //e.Graphics.DrawString("年龄 " + tbAge.Text, zt9, Brushes.Black, x + 240, y);
            //e.Graphics.DrawLine(Pens.Black, x + 265, y1, x + 310, y1);            
            //e.Graphics.DrawString("床号 " + this.tbBedNo.Text, zt9, Brushes.Black, x + 330, y);
            //e.Graphics.DrawLine(Pens.Black, x + 355, y1, x + 440, y1);           
            //e.Graphics.DrawString("日期 " + dtAddTime.Text, zt9, Brushes.Black, x + 450, y);
            //e.Graphics.DrawLine(Pens.Black, x + 475, y1, x + 600, y1); ;
            ////为空画斜杠
            //if (dtAddTime.Text == "")
            //    e.Graphics.DrawLine(ptp, new Point(495 + x, y + 13), new Point(515 + x, y + 2));
            //y = y + 33; y1 = y + 15;
            //e.Graphics.DrawString("麻醉经过： ", ht9, Brushes.Black, x + 0, y); 
            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawRectangle(Pens.Black, x + 5, y, 12, 12);
            //e.Graphics.DrawString("全身麻醉 ", ht9, Brushes.Black, x + 25, y); 
            //if (cbQSMZ.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 0, y, x + 10, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 10, y + 12, x + 25, y - 3);
            //}
            //e.Graphics.DrawString("诱导方法："+cmbYoudaoFF.Text, zt9, Brushes.Black, x + 110, y);
            //e.Graphics.DrawLine(ptp, new Point(170 + x, y1), new Point(300 + x, y1));
           
            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawRectangle(Pens.Black, x + 5, y, 12, 12);
            //e.Graphics.DrawString("椎管内麻醉 ", ht9, Brushes.Black, x + 25, y);
            //if (cbZGNMZ.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 0, y, x + 10, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 10, y + 12, x + 25, y - 3);
            //}
            //e.Graphics.DrawString("麻醉方法：" + cmbZGNMZFF.Text, zt8, Brushes.Black, x + 110, y);
            //e.Graphics.DrawLine(ptp, new Point(160 + x, y1), new Point(260 + x, y1));
            //e.Graphics.DrawString("穿刺点1：" + cmbCCD11.Text, zt8, Brushes.Black, x + 270, y);
            //e.Graphics.DrawLine(ptp, new Point(315 + x, y1), new Point(380 + x, y1));
            //e.Graphics.DrawString("留置深度：" + tbliuzhiSD1.Text, zt8, Brushes.Black, x + 380, y);
            //e.Graphics.DrawLine(ptp, new Point(430 + x, y1), new Point(480 + x, y1));
            //e.Graphics.DrawString("cm  穿刺点2：" + cmbCCD21.Text, zt8, Brushes.Black, x + 480, y);
            //e.Graphics.DrawLine(ptp, new Point(550 + x, y1), new Point(620 + x, y1));
            //e.Graphics.DrawString("留置深度：" + tbliuzhiSD1.Text, zt8, Brushes.Black, x + 620, y);
            //e.Graphics.DrawLine(ptp, new Point(670 + x, y1), new Point(710 + x, y1));
            //e.Graphics.DrawString("cm ", zt9, Brushes.Black, x + 710, y);          

            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawString("麻醉平面：" + cmbMZPM_ZG.Text, zt8, Brushes.Black, x + 110, y);
            //e.Graphics.DrawLine(ptp, new Point(160 + x, y1), new Point(260 + x, y1));
            //e.Graphics.DrawString("药品：" + tbYaopin_ZG.Text , zt8, Brushes.Black, x + 270, y);
            //e.Graphics.DrawLine(ptp, new Point(300 + x, y1), new Point(720 + x, y1));
           

            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawRectangle(Pens.Black, x + 5, y, 12, 12);
            //e.Graphics.DrawString("气管插管 ", ht9, Brushes.Black, x + 25, y);
            //if (cbQGCG.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 0, y, x + 10, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 10, y + 12, x + 25, y - 3);
            //}
            //e.Graphics.DrawString("插管位置：" + cmbQGCGwz1.Text + "、" 
            //    + cmbQGCGwz2.Text + "、"+cmbQGCGwz3.Text, zt8, Brushes.Black, x + 110, y);
            //e.Graphics.DrawLine(ptp, new Point(160 + x, y1), new Point(320 + x, y1));
            //e.Graphics.DrawString("型号：" + tbQGCGtype.Text, zt8, Brushes.Black, x + 330, y);
            //e.Graphics.DrawLine(ptp, new Point(360 + x, y1), new Point(540 + x, y1));
            //e.Graphics.DrawString("深度：" + tbQGCGsd.Text, zt8, Brushes.Black, x + 550, y);
            //e.Graphics.DrawLine(ptp, new Point(580 + x, y1), new Point(620 + x, y1));
            //e.Graphics.DrawString("cm   套囊：" + cmbTaoNang.Text, zt8, Brushes.Black, x + 620, y);
            //e.Graphics.DrawLine(ptp, new Point(680 + x, y1), new Point(720 + x, y1));

            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawRectangle(Pens.Black, x + 5, y, 12, 12);
            //e.Graphics.DrawString("喉罩 ", ht9, Brushes.Black, x + 25, y);
            //if (cbHZ.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 0, y, x + 10, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 10, y + 12, x + 25, y - 3);
            //}
            //e.Graphics.DrawString("型号:" + tbHZType.Text.Trim(), zt8, Brushes.Black, x + 100, y);
            //e.Graphics.DrawLine(Pens.Black, x + 130, y1, x + 500, y1);
           
            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawRectangle(Pens.Black, x + 5, y, 12, 12);
            //e.Graphics.DrawString("神经阻滞 ", ht9, Brushes.Black, x + 25, y);
            //if (cbSJZZ.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 0, y, x + 10, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 10, y + 12, x + 25, y);
            //}
            //e.Graphics.DrawRectangle(Pens.Black, x + 110, y, 12, 12);
            //e.Graphics.DrawString("颈丛神经阻滞 ", zt8, Brushes.Black, x + 130, y);
            //if (cbJCSJZZ.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 105, y, x + 115, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 115, y + 12, x + 130, y);
            //}
            //e.Graphics.DrawRectangle(Pens.Black, x + 270, y, 12, 12);
            //e.Graphics.DrawRectangle(Pens.Black, x + 310, y, 12, 12);
            //e.Graphics.DrawString("浅丛：     左      右" ,zt8, Brushes.Black, x + 220, y);
           
            //if (cbJCSJZZqcLeft.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 265, y, x + 275, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 275, y + 12, x + 290, y-2);
            //}
            //if (cbJCSJZZqcRight.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 305, y, x + 315, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 315, y + 12, x + 330, y - 2);
            //}
            //e.Graphics.DrawRectangle(Pens.Black, x + 390, y, 12, 12);
            //e.Graphics.DrawRectangle(Pens.Black, x + 430, y, 12, 12);
            //e.Graphics.DrawString("深丛：    左      右", zt8, Brushes.Black, x + 350, y);           
            //if (cbJCSJZZscLeft.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 385, y, x + 395, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 395, y + 12, x + 410, y - 2);
            //}
            //if (cbJCSJZZscRight.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 425, y, x + 435, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 435, y + 12, x + 450, y - 2);
            //}
            

            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawRectangle(Pens.Black, x + 110, y, 12, 12);
            //e.Graphics.DrawString("臂丛神经阻滞 ", zt8, Brushes.Black, x + 130, y);
            //if (cbBCSJZZ.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 105, y, x + 115, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 115, y + 12, x + 130, y - 3);
            //}
            //e.Graphics.DrawRectangle(Pens.Black, x + 230, y, 12, 12);
            //e.Graphics.DrawString("左", zt8, Brushes.Black, x + 250, y);
            //if (cbBCSJZZleft.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 225, y, x + 235, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 235, y + 12, x + 250, y - 3);
            //}
            //e.Graphics.DrawRectangle(Pens.Black, x + 280, y, 12, 12);
            //e.Graphics.DrawString("右", zt8, Brushes.Black, x + 300, y);
            //if (cbBCSJZZright.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 275, y, x + 285, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 285, y + 12, x + 300, y - 3);
            //}
            //e.Graphics.DrawRectangle(Pens.Black, x + 330, y, 12, 12);
            //e.Graphics.DrawString("肌间钩法", zt8, Brushes.Black, x + 350, y);
            //if (cbBCSJZZjjgf.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 325, y, x + 335, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 335, y + 12, x + 350, y - 3);
            //}
            //e.Graphics.DrawRectangle(Pens.Black, x + 410, y, 12, 12);
            //e.Graphics.DrawString("腋路法", zt8, Brushes.Black, x + 430, y);
            //if (cbBCSJZZylf.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 405, y, x + 415, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 415, y + 12, x + 430, y - 3);
            //}
            //e.Graphics.DrawRectangle(Pens.Black, x + 500, y, 12, 12);
            //e.Graphics.DrawString("锁骨上法", zt8, Brushes.Black, x + 520, y);
            //if (cbBCSJZZsgsf.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 495, y, x + 505, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 505, y + 12, x + 520, y - 3);
            //}
           
            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawRectangle(Pens.Black, x + 110, y, 12, 12);
            //e.Graphics.DrawString("腰丛神经阻滞 ", zt9, Brushes.Black, x + 130, y);
            //if (cbYCSJZZ.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 105, y, x + 115, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 115, y + 12, x + 130, y - 3);
            //}
            //e.Graphics.DrawRectangle(Pens.Black, x + 230, y, 12, 12);
            //e.Graphics.DrawString("左", zt8, Brushes.Black, x + 250, y);
            //if (cbYCSJZZleft.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 225, y, x + 235, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 235, y + 12, x + 250, y - 3);
            //}
            //e.Graphics.DrawRectangle(Pens.Black, x + 310, y, 12, 12);
            //e.Graphics.DrawString("右", zt8, Brushes.Black, x + 330, y);
            //if (cbYCSJZZright.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 305, y, x + 315, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 315, y + 12, x + 330, y - 3);
            //}


            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawRectangle(Pens.Black, x + 110, y, 12, 12);
            //e.Graphics.DrawString("坐骨神经阻滞 ", zt8, Brushes.Black, x + 130, y);
            //if (cbZGSJZZ.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 105, y, x + 115, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 115, y + 12, x + 130, y - 3);
            //}
            //e.Graphics.DrawRectangle(Pens.Black, x + 230, y, 12, 12);
            //e.Graphics.DrawString("左", zt8, Brushes.Black, x + 250, y);
            //if (cbZGSJZZleft.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 225, y, x + 235, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 235, y + 12, x + 250, y - 3);
            //}
            //e.Graphics.DrawRectangle(Pens.Black, x + 310, y, 12, 12);
            //e.Graphics.DrawString("右", zt8, Brushes.Black, x + 330, y);
            //if (cbZGSJZZright.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 305, y, x + 315, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 315, y + 12, x + 330, y - 3);
            //}

            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawRectangle(Pens.Black, x + 110, y, 12, 12);
            //e.Graphics.DrawString("股神经阻滞 ", zt8, Brushes.Black, x + 130, y);
            //if (cbGSJZZ.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 105, y, x + 115, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 115, y + 12, x + 130, y - 3);
            //}
            //e.Graphics.DrawRectangle(Pens.Black, x + 230, y, 12, 12);
            //e.Graphics.DrawString("左", zt8, Brushes.Black, x + 250, y);
            //if (cbGSJZZleft.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 225, y, x + 235, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 235, y + 12, x + 250, y - 3);
            //}
            //e.Graphics.DrawRectangle(Pens.Black, x + 310, y, 12, 12);
            //e.Graphics.DrawString("右", zt8, Brushes.Black, x + 330, y);
            //if (cbGSJZZright.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 305, y, x + 315, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 315, y + 12, x + 330, y - 3);
            //}
            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawRectangle(Pens.Black, x + 110, y, 12, 12);
            //e.Graphics.DrawString("股外侧神经阻滞 ", zt8, Brushes.Black, x + 130, y);
            //if (cbGWCSJZZ.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 105, y, x + 115, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 115, y + 12, x + 130, y - 3);
            //}
            //e.Graphics.DrawRectangle(Pens.Black, x + 230, y, 12, 12);
            //e.Graphics.DrawString("左", zt8, Brushes.Black, x + 250, y);
            //if (cbGWCSJZZleft.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 225, y, x + 235, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 235, y + 12, x + 250, y - 3);
            //}
            //e.Graphics.DrawRectangle(Pens.Black, x + 310, y, 12, 12);
            //e.Graphics.DrawString("右", zt8, Brushes.Black, x + 330, y);
            //if (cbGWCSJZZright.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 305, y, x + 315, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 315, y + 12, x + 330, y - 3);
            //}

            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawRectangle(Pens.Black, x + 110, y, 12, 12);
            //e.Graphics.DrawString("有创操作 ", zt8, Brushes.Black, x + 130, y);
            //if (cbYCCZ.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 105, y, x + 115, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 115, y + 12, x + 130, y - 3);
            //}
            //e.Graphics.DrawRectangle(Pens.Black, x + 230, y, 12, 12);
            //e.Graphics.DrawString("动脉穿刺置管 ", zt8, Brushes.Black, x + 250, y);
            //if (cbDMCCZG.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 225, y, x + 235, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 235, y + 12, x + 250, y - 3);
            //}
            //e.Graphics.DrawRectangle(Pens.Black, x + 330, y, 12, 12);
            //e.Graphics.DrawString("深静脉穿刺置管 ", zt8, Brushes.Black, x + 350, y);
            //if (cbSJMCCZG.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 325, y, x + 335, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 335, y + 12, x + 350, y - 3);
            //}
            //e.Graphics.DrawString("其他："+tbOtherZZ.Text.Trim(), zt8, Brushes.Black, x + 450, y);
            //e.Graphics.DrawLine(ptp, new Point(480 + x, y1), new Point(720 + x, y1));

            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawString("维持方法：" + tbWCFF.Text.Trim(), zt8, Brushes.Black, x, y);
            //e.Graphics.DrawLine(Pens.Black, x + 55, y1, x + 500, y1);
            //e.Graphics.DrawRectangle(Pens.Black, x + 530, y, 12, 12);
            //e.Graphics.DrawString("麻醉监护", zt8, Brushes.Black, x + 550, y);
            //if (cbMZJH.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 525, y, x + 535, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 535, y + 12, x + 550, y - 3);
            //}
            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawString("特殊情况："+tbRemarkMZ.Text.Trim(), zt8, Brushes.Black, x , y);
            //e.Graphics.DrawLine(Pens.Black, x + 30, y1, x + 720, y1);


            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawString("离室病人情况：" , ht9, Brushes.Black, x + 0, y);
            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawString("肌力恢复：" +cmbJili.Text.Trim(), zt8, Brushes.Black, x + 0, y);
            //e.Graphics.DrawLine(Pens.Black, x + 50, y1, x + 100, y1);
            //e.Graphics.DrawString("咳嗽吞咽反应：" + cmbKesouTunyan.Text.Trim(), zt8, Brushes.Black, x + 110, y);
            //e.Graphics.DrawLine(Pens.Black, x + 180, y1, x + 220, y1);
            //e.Graphics.DrawString("定向力恢复：" + cmbDingxiangli.Text.Trim(), zt8, Brushes.Black, x + 230, y);
            //e.Graphics.DrawLine(Pens.Black, x + 290, y1, x + 340, y1);
            //e.Graphics.DrawString("意识：" + cmbYishi.Text.Trim(), zt8, Brushes.Black, x + 350, y);
            //e.Graphics.DrawLine(Pens.Black, x + 380, y1, x + 430, y1);
            //e.Graphics.DrawString("病人自控镇痛：" + cmbBRZKZT.Text.Trim(), zt8, Brushes.Black, x + 440, y);
            //e.Graphics.DrawLine(Pens.Black, x + 510, y1, x + 560, y1);
            //e.Graphics.DrawString("麻醉平面：" + cmbMZPM_OUT.Text.Trim(), zt8, Brushes.Black, x + 570, y);
            //e.Graphics.DrawLine(Pens.Black, x + 620, y1, x + 700, y1);

            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawString("备注：" + tbRemarkOut.Text.Trim(), zt9, Brushes.Black, x, y);
            //e.Graphics.DrawLine(Pens.Black, x + 30, y1, x + 720, y1);
            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawString("病人去向：" + cmbBRQX.Text.Trim(), zt9, Brushes.Black, x, y);
            //e.Graphics.DrawLine(Pens.Black, x + 55, y1, x + 200, y1);
            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawString("麻醉方式变更原因：" + tbRemarkOut.Text.Trim(), zt9, Brushes.Black, x, y);
            //e.Graphics.DrawLine(Pens.Black, x + 100, y1, x + 720, y1);
            
            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawString("麻醉科医师：" + cmbMZYS.Text, zt9, Brushes.Black, x + 50, y);
            //e.Graphics.DrawLine(Pens.Black, x + 115, y1, x + 300, y1);
            //e.Graphics.DrawString("日期：" + dtAddTime.Text, zt9, Brushes.Black, x + 330, y);
            //e.Graphics.DrawLine(Pens.Black, x + 360, y1, x + 460, y1);
            
        }
        
        private void MZZJ_SZ_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ( isRead)
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

        private void btnLock_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = DAL.GetMZZJ_CJ(mzjldID);
            if (dt.Rows.Count == 0)
                MessageBox.Show("需要先保存，才能存档！");
            else
            {
                result = DAL.UpdateMZZJ_CJ(mzjldID,1);
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
            DataTable dt = DAL.GetMZZJ_CJ(mzjldID);
            if (dt.Rows[0]["isRead"].ToString()=="1")
            {
                result = DAL.UpdateMZZJ_CJ(mzjldID,0);
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
            adims_BLL.DataValid.Text_Value_Limit(sender, e);
        }

        private void tbliuzhiSD2_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.DataValid.Text_Value_Limit(sender, e);
        }

        private void tbQGCGsd_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.DataValid.Text_Value_Limit(sender, e);
        }
               

        private void cmbCCD21_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                DataTable dt = DAL.GetMazuiPingmian(cmbCCD21.Text.Trim());
                cmbCCD21.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    cmbCCD21.Items.Add(dr["name"].ToString());
                }
                cmbCCD21.DroppedDown = true;
            }
        }

        private void cmbMZPM_ZG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                DataTable dt = DAL.GetMazuiPingmian(cmbMZPM_ZG.Text.Trim());
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
                DataTable dt = DAL.GetMazuiPingmian(cmbMZPM_OUT.Text.Trim());
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
                DataTable dt = DAL.GetMazuiPingmian(cmbCCD11.Text.Trim());
                cmbCCD11.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    cmbCCD11.Items.Add(dr["name"].ToString());
                }
                cmbCCD11.DroppedDown = true;
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




    }
}
