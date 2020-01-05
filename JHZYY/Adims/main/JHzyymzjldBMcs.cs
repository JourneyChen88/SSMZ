using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using adims_BLL;
using adims_DAL;
namespace main
{
    public partial class JHzyymzjldBMcs : Form
    {
        string patid;
        public JHzyymzjldBMcs(string ID)
        {
            patid = ID;

            InitializeComponent();
        }
        adims_BLL.YXL_BLL cll = new adims_BLL.YXL_BLL();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                Dictionary<string, string> SQFS = new Dictionary<string, string>();
                string AddItem = "";
                AddItem = "";
                SQFS.Add("zyh", patid);
                SQFS.Add("ccdyd", ccdyd.Text);
                SQFS.Add("ccdrd", ccdrd.Text);
                SQFS.Add("wqsd", wqsd.Text);
                SQFS.Add("dgcrsd", dgcrsd.Text);
                SQFS.Add("dgcryy", dgcryy.Text);
                SQFS.Add("ssssd", ssssd.Text);
                SQFS.Add("sssxd", sssxd.Text);
                SQFS.Add("ssbsd", ssbsd.Text);
                SQFS.Add("ssbxd", ssbxd.Text);
                SQFS.Add("qmczqt", qmqt.Text);
                SQFS.Add("mzqjbfzqt", qt.Text);
                SQFS.Add("qkfxzj", fxzj.Text);
                SQFS.Add("mzzjsyz", mzzjsyz.Text);
                SQFS.Add("mzzjz", mzzjz.Text);
                AddItem = "";
                if (ccz.Checked) AddItem += "1";
                if (ccyzcw.Checked) AddItem += "2";
                if (ccfw.Checked) AddItem += "3";
                SQFS.Add("cctw", AddItem);

                AddItem = "";
                if (ccdycg.Checked) AddItem += "1";
                if (ccdysb.Checked) AddItem += "2";
                if (ccdycp.Checked) AddItem += "3";
                if (ccdrdcg.Checked) AddItem += "4";
                if (ccdrdsb.Checked) AddItem += "5";
                if (ccdrdcp.Checked) AddItem += "6";
                SQFS.Add("ccd", AddItem);

                AddItem = "";
                if (zrf.Checked) AddItem += "1";
                if (crf.Checked) AddItem += "2";
                if (xt.Checked) AddItem += "3";
                if (xd.Checked) AddItem += "4";
                if (xc.Checked) AddItem += "5";
                SQFS.Add("zrf", AddItem);

                AddItem = "";
                if (hrdmx.Checked) AddItem += "1";
                if (hrdsmx.Checked) AddItem += "2";
                if (hrdbmx.Checked) AddItem += "3";
                SQFS.Add("hrdgj", AddItem);

                AddItem = "";
                if (fyd.Checked) AddItem += "1";
                if (fyx.Checked) AddItem += "2";
                if (fyw.Checked) AddItem += "3";
                if (bdd.Checked) AddItem += "4";
                if (bdx.Checked) AddItem += "5";
                if (bdw.Checked) AddItem += "6";
                SQFS.Add("fybd", AddItem);

                AddItem = "";
                if (zshs.Checked) AddItem += "1";
                if (zsss.Checked) AddItem += "2";
                if (zsbs.Checked) AddItem += "3";
                if (hld.Checked) AddItem += "4";
                if (hls.Checked) AddItem += "5";
                if (hlw.Checked) AddItem += "6";
                SQFS.Add("zszl", AddItem);

                AddItem = "";
                if (zxw.Checked) AddItem += "1";
                if (zxy.Checked) AddItem += "2";
                if (zxcc.Checked) AddItem += "3";
                if (zxzg.Checked) AddItem += "4";
                if (zxd.Checked) AddItem += "5";
                if (zxqx.Checked) AddItem += "6";
                SQFS.Add("zx", AddItem);

                AddItem = "";
                if (cgcrsl.Checked) AddItem += "1";
                if (dgcrbsl.Checked) AddItem += "2";
                SQFS.Add("dgcrqk", AddItem);

                

                //全麻操作
                AddItem = "";
                if (kcg.Checked) AddItem += "1";
                if (sqgz.Checked) AddItem += "2";
                if (sqgy.Checked) AddItem += "3";
                if (mc.Checked) AddItem += "4";
                if (dgID.Checked) AddItem += "5";
                if (bcgz.Checked) AddItem += "6";
                if (bcgy.Checked) AddItem += "7";
                if (qwj.Checked) AddItem += "8";
                if (dgxc.Checked) AddItem += "9";
                if (cgkn.Checked) AddItem += "a";
                SQFS.Add("qmcz", AddItem);
                //麻醉诱导期
                AddItem = "";
                if (mzydmy.Checked) AddItem += "1";
                if (mzydqk.Checked) AddItem += "2";
                if (mzydfq.Checked) AddItem += "3";
                if (mzydot.Checked) AddItem += "4";
                if (mzydhjl.Checked) AddItem += "5";
                if (mzydjd.Checked) AddItem += "6";
                SQFS.Add("mzydq", AddItem);


                //麻醉期间并发症
                AddItem = "";
                if (ycss.Checked) AddItem += "1";
                if (sxc.Checked) AddItem += "2";
                if (qx.Checked) AddItem += "3";
                if (gxy.Checked) AddItem += "4";
                if (xtzt.Checked) AddItem += "5";
                if (hfNzz.Checked) AddItem += "6";
                if (qjm.Checked) AddItem += "7";
                SQFS.Add("mzqjbfz1", AddItem);
                AddItem = "";
                if (lmss.Checked) AddItem += "1";
                if (zqgjl.Checked) AddItem += "2";
                if (qy.Checked) AddItem += "3";
                if (dxy.Checked) AddItem += "4";
                if (jmygm.Checked) AddItem += "5";
                if (hnsz.Checked) AddItem += "6";
                if (ymcp.Checked) AddItem += "7";
                SQFS.Add("mzqjbfz2", AddItem);
                AddItem = "";

                if (wx.Checked) AddItem += "1";
                if (jxfsz.Checked) AddItem += "2";
                if (xj.Checked) AddItem += "3";
                if (xlsc.Checked) AddItem += "4";
                if (jmyzd.Checked) AddItem += "5";
                if (jNgfzz.Checked) AddItem += "6";
                SQFS.Add("mzqjbfz3", AddItem);
                AddItem = "";

                if (ot.Checked) AddItem += "1";
                if (fss.Checked) AddItem += "2";
                if (hxtz.Checked) AddItem += "3";
                if (xs.Checked) AddItem += "4";
                if (gNzz.Checked) AddItem += "5";
                if (ymwdgzd.Checked) AddItem += "6";
                SQFS.Add("mzqjbfz4", AddItem);

                
                DataTable dt = cll.mzjiazai(patid);
                if (dt.Rows.Count > 0)
                {

                    result = cll.jhMZXiuGai(SQFS);

                }
                else
                    result = cll.jhMZXingZeng(SQFS);
                if (result > 0)
                {
                    baocun();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex + "请核对信息！！！！");
            }
        }
        private void baocun()
        {
            try
            {
                int result = 0;
                Dictionary<string, string> SQFS = new Dictionary<string, string>();
                string AddItem = "";
                AddItem = "";
                SQFS.Add("zyh", patid);
                SQFS.Add("huli", textcghl.Text);
                SQFS.Add("tiwei", comboBoxtw.Text);
                SQFS.Add("xueya", textxymb.Text);
                SQFS.Add("bidaoguan", comboBoxjy.Text);
                SQFS.Add("mianzhao", comboBoxmz.Text);
                AddItem = "";
                if (checkBoxxt.Checked) AddItem += "1";
                if (checkBoxdjmcchhl.Checked) AddItem += "2";
                if (checkBoxjxtq.Checked) AddItem += "3";
                if (checkBoxglbrks.Checked) AddItem += "4";
                if (checkBoxzyzgnmz.Checked) AddItem += "5";
                if (checkBoxqt.Checked) AddItem += "6";
                if (ymcp.Checked) AddItem += "7";
                SQFS.Add("fuxuankuang", AddItem);
                SQFS.Add("qita", textBoxqt.Text);
                DataTable dt = cll.mzbyjiazai(patid);
                if (dt.Rows.Count > 0)
                {

                    result = cll.jhMZBYXiuGai(SQFS);

                }
                else
                    result = cll.jhMZBYXingZeng(SQFS);
                if (result > 0)
                {
                    MessageBox.Show("保存成功！");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex + "请核对信息！！！！");
            }
        }
        private void JHzyymzjldBMcs_Load(object sender, EventArgs e)
        {
              DataTable dt = cll.mzjiazai(patid);
              if (dt.Rows.Count > 0)
              {
                  DataRow da = dt.Rows[0];

                  if (Convert.ToString(da["cctw"]).Contains("1")) ccz.Checked = true;
                  if (Convert.ToString(da["cctw"]).Contains("2")) ccyzcw.Checked = true;
                  if (Convert.ToString(da["cctw"]).Contains("3")) ccfw.Checked = true;

                  if (Convert.ToString(da["zszl"]).Contains("1")) zshs.Checked = true;
                  if (Convert.ToString(da["zszl"]).Contains("2")) zsss.Checked = true;
                  if (Convert.ToString(da["zszl"]).Contains("3")) zsbs.Checked = true;
                  if (Convert.ToString(da["zszl"]).Contains("4")) hld.Checked = true;
                  if (Convert.ToString(da["zszl"]).Contains("5")) hls.Checked = true;
                  if (Convert.ToString(da["zszl"]).Contains("6")) hlw.Checked = true;

                  if (Convert.ToString(da["zrf"]).Contains("1")) zrf.Checked = true;
                  if (Convert.ToString(da["zrf"]).Contains("2")) crf.Checked = true;
                  if (Convert.ToString(da["zrf"]).Contains("3")) xt.Checked = true;
                  if (Convert.ToString(da["zrf"]).Contains("4")) xd.Checked = true;
                  if (Convert.ToString(da["zrf"]).Contains("5")) xc.Checked = true;

                  if (Convert.ToString(da["ccd"]).Contains("1")) ccdycg.Checked = true;
                  if (Convert.ToString(da["ccd"]).Contains("2")) ccdysb.Checked = true;
                  if (Convert.ToString(da["ccd"]).Contains("3")) ccdycp.Checked = true;
                  if (Convert.ToString(da["ccd"]).Contains("4")) ccdrdcg.Checked = true;
                  if (Convert.ToString(da["ccd"]).Contains("5")) ccdrdsb.Checked = true;
                  if (Convert.ToString(da["ccd"]).Contains("6")) ccdrdcp.Checked = true;

                  if (Convert.ToString(da["zx"]).Contains("1")) zxw.Checked = true;
                  if (Convert.ToString(da["zx"]).Contains("2")) zxy.Checked = true;
                  if (Convert.ToString(da["zx"]).Contains("3")) zxcc.Checked = true;
                  if (Convert.ToString(da["zx"]).Contains("4")) zxzg.Checked = true;
                  if (Convert.ToString(da["zx"]).Contains("5")) zxd.Checked = true;
                  if (Convert.ToString(da["zx"]).Contains("6")) zxqx.Checked = true;

                  if (Convert.ToString(da["hrdgj"]).Contains("1")) hrdmx.Checked = true;
                  if (Convert.ToString(da["hrdgj"]).Contains("2")) hrdsmx.Checked = true;
                  if (Convert.ToString(da["hrdgj"]).Contains("3")) hrdbmx.Checked = true;

                  if (Convert.ToString(da["dgcrqk"]).Contains("1")) cgcrsl.Checked = true;
                  if (Convert.ToString(da["dgcrqk"]).Contains("2")) dgcrbsl.Checked = true;

                  if (Convert.ToString(da["fybd"]).Contains("1")) fyd.Checked = true;
                  if (Convert.ToString(da["fybd"]).Contains("2")) fyx.Checked = true;
                  if (Convert.ToString(da["fybd"]).Contains("3")) fyw.Checked = true;
                  if (Convert.ToString(da["fybd"]).Contains("4")) bdd.Checked = true;
                  if (Convert.ToString(da["fybd"]).Contains("5")) bdx.Checked = true;
                  if (Convert.ToString(da["fybd"]).Contains("6")) bdw.Checked = true;

                  if (Convert.ToString(da["qmcz"]).Contains("1")) kcg.Checked = true;
                  if (Convert.ToString(da["qmcz"]).Contains("2")) sqgz.Checked = true;
                  if (Convert.ToString(da["qmcz"]).Contains("3")) sqgy.Checked = true;
                  if (Convert.ToString(da["qmcz"]).Contains("4")) mc.Checked = true;
                  if (Convert.ToString(da["qmcz"]).Contains("5")) dgID.Checked = true;
                  if (Convert.ToString(da["qmcz"]).Contains("6")) bcgz.Checked = true;
                  if (Convert.ToString(da["qmcz"]).Contains("7")) bcgy.Checked = true;
                  if (Convert.ToString(da["qmcz"]).Contains("8")) qwj.Checked = true;
                  if (Convert.ToString(da["qmcz"]).Contains("9")) dgxc.Checked = true;
                  if (Convert.ToString(da["qmcz"]).Contains("a")) cgkn.Checked = true;

                  if (Convert.ToString(da["mzydq"]).Contains("1")) mzydmy.Checked = true;
                  if (Convert.ToString(da["mzydq"]).Contains("2")) mzydqk.Checked = true;
                  if (Convert.ToString(da["mzydq"]).Contains("3")) mzydfq.Checked = true;
                  if (Convert.ToString(da["mzydq"]).Contains("4")) mzydot.Checked = true;
                  if (Convert.ToString(da["mzydq"]).Contains("5")) mzydhjl.Checked = true;
                  if (Convert.ToString(da["mzydq"]).Contains("6")) mzydjd.Checked = true;

                  if (Convert.ToString(da["mzqjbfz1"]).Contains("1")) ycss.Checked = true;
                  if (Convert.ToString(da["mzqjbfz1"]).Contains("2")) sxc.Checked = true;
                  if (Convert.ToString(da["mzqjbfz1"]).Contains("3")) qx.Checked = true;
                  if (Convert.ToString(da["mzqjbfz1"]).Contains("4")) gxy.Checked = true;
                  if (Convert.ToString(da["mzqjbfz1"]).Contains("5")) xtzt.Checked = true;
                  if (Convert.ToString(da["mzqjbfz1"]).Contains("6")) hfNzz.Checked = true;
                  if (Convert.ToString(da["mzqjbfz1"]).Contains("7")) qjm.Checked = true;

                  if (Convert.ToString(da["mzqjbfz2"]).Contains("1")) lmss.Checked = true;
                  if (Convert.ToString(da["mzqjbfz2"]).Contains("2")) zqgjl.Checked = true;
                  if (Convert.ToString(da["mzqjbfz2"]).Contains("3")) qy.Checked = true;
                  if (Convert.ToString(da["mzqjbfz2"]).Contains("4")) dxy.Checked = true;
                  if (Convert.ToString(da["mzqjbfz2"]).Contains("5")) jmygm.Checked = true;
                  if (Convert.ToString(da["mzqjbfz2"]).Contains("6")) hnsz.Checked = true;
                  if (Convert.ToString(da["mzqjbfz2"]).Contains("7")) ymcp.Checked = true;

                  if (Convert.ToString(da["mzqjbfz3"]).Contains("1")) wx.Checked = true;
                  if (Convert.ToString(da["mzqjbfz3"]).Contains("2")) jxfsz.Checked = true;
                  if (Convert.ToString(da["mzqjbfz3"]).Contains("3")) xj.Checked = true;
                  if (Convert.ToString(da["mzqjbfz3"]).Contains("4")) xlsc.Checked = true;
                  if (Convert.ToString(da["mzqjbfz3"]).Contains("5")) jmyzd.Checked = true;
                  if (Convert.ToString(da["mzqjbfz3"]).Contains("6")) jNgfzz.Checked = true;

                  if (Convert.ToString(da["mzqjbfz4"]).Contains("1")) ot.Checked = true;
                  if (Convert.ToString(da["mzqjbfz4"]).Contains("2")) fss.Checked = true;
                  if (Convert.ToString(da["mzqjbfz4"]).Contains("3")) hxtz.Checked = true;
                  if (Convert.ToString(da["mzqjbfz4"]).Contains("4")) xs.Checked = true;
                  if (Convert.ToString(da["mzqjbfz4"]).Contains("5")) gNzz.Checked = true;
                  if (Convert.ToString(da["mzqjbfz4"]).Contains("6")) ymwdgzd.Checked = true;

                  ccdyd.Text = da["ccdyd"].ToString();
                  ccdrd.Text = da["ccdrd"].ToString();
                  wqsd.Text = da["wqsd"].ToString();
                  dgcrsd.Text = da["dgcrsd"].ToString();
                  dgcryy.Text = da["dgcryy"].ToString();
                  ssssd.Text = da["ssssd"].ToString();
                  sssxd.Text = da["sssxd"].ToString();
                  ssbsd.Text = da["ssbsd"].ToString();
                  ssbxd.Text = da["ssbxd"].ToString();
                  qmqt.Text = da["qmczqt"].ToString();
                  qt.Text = da["mzqjbfzqt"].ToString();
                   string qkzj= da["qkfxzj"].ToString();
                  mzzjsyz.Text = da["mzzjsyz"].ToString();
                  mzzjz.Text = da["mzzjz"].ToString();
                  if (qkzj == "" || qkzj == null)
                  {
                      FXZJ();
                  }
                  else
                  {
                      fxzj.Text = qkzj;
                  }

                  MZDBY();
                  
              }
        }
        private void FXZJ()
        {
            DataTable QK = cll.mzjlqkfxzj();
            fxzj.Items.Clear();
            for (int i = 0; i < QK.Rows.Count; i++)
            {
                this.fxzj.Items.Add(QK.Rows[i][0]);
            }
        }
        private void MZDBY()
        {
            DataTable dt = cll.mzbyjiazai(patid);
            if (dt.Rows.Count > 0)
            {
                DataRow da = dt.Rows[0];
                textcghl.Text = da["huli"].ToString();
                comboBoxtw.Text = da["tiwei"].ToString();
                textxymb.Text = da["xueya"].ToString();
                comboBoxjy.Text = da["bidaoguan"].ToString();
                comboBoxmz.Text = da["mianzhao"].ToString();
                if (Convert.ToString(da["fuxuankuang"]).Contains("1")) checkBoxxt.Checked = true;
                if (Convert.ToString(da["fuxuankuang"]).Contains("2")) checkBoxdjmcchhl.Checked = true;
                if (Convert.ToString(da["fuxuankuang"]).Contains("3")) checkBoxjxtq.Checked = true;
                if (Convert.ToString(da["fuxuankuang"]).Contains("4")) checkBoxglbrks.Checked = true;
                if (Convert.ToString(da["fuxuankuang"]).Contains("5")) checkBoxzyzgnmz.Checked = true;
                if (Convert.ToString(da["fuxuankuang"]).Contains("6")) checkBoxqt.Checked = true;
                if (Convert.ToString(da["fuxuankuang"]).Contains("7")) ymcp.Checked = true;
                textBoxqt.Text = da["qita"].ToString();
               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.printDocument1.PrintPage += new PrintPageEventHandler(this.MyPrintDocument_PrintPage);
            //将写好的格式给打印预览控件以便预览
            printPreviewDialog1.Document = printDocument1;
            //显示打印预览
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            DialogResult result = printPreviewDialog1.ShowDialog();
            if (result == DialogResult.OK)
                this.printDocument1.Print();
        }
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }
        private void MyPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            DataTable dt = cll.mzjiazai(patid);
            Font ptzt8 = new Font("微软雅黑", 8);//普通字体 
            Font ptzt9 = new Font("微软雅黑", 9);//普通字体  
            Font ptzt10 = new Font("微软雅黑", 10);//普通字体 
            Font ptzt11 = new Font("微软雅黑", 11);//普通字体 
            Font ptzt12 = new Font("宋体", 10);//普通字体 
            Font ptzt08 = new Font("宋体", 8);//普通字体 
            Font ptzt13 = new Font("微软雅黑", 13);//普通字体   
            Font ptzt18 = new Font("微软雅黑", 18);//普通字体   
            int y = 20; int x = 10; int y1 = y + 15;
            e.Graphics.DrawLine(Pens.Black, x + 5, y, x + 780, y);
            e.Graphics.DrawLine(Pens.Black, x + 5, y + 1020, x + 780, y + 1020);
            e.Graphics.DrawLine(Pens.Black, x + 5, y, x + 5, y + 1020);
            e.Graphics.DrawLine(Pens.Black, x + 780, y, x + 780, y + 1020);
            e.Graphics.DrawLine(Pens.Black, x + 35, y, x + 35, y + 1020);
            e.Graphics.DrawLine(Pens.Black, x + 390, y, x + 390, y + 360);
            e.Graphics.DrawString("椎\n管\n内\n麻\n醉\n操\n作", ptzt13, Brushes.Black, new Point(x + 10, y + 10));
            e.Graphics.DrawString("①穿刺时病人体位:□坐□右左侧卧□俯卧", ptzt12, Brushes.Black, new Point(x + 40, y + 3));
            e.Graphics.DrawString("②穿刺点:第一点:          间隙。", ptzt12, Brushes.Black, new Point(x + 40, y + 30));
            e.Graphics.DrawString("□成功□失败□穿破", ptzt12, Brushes.Black, new Point(x + 260, y + 30));
            e.Graphics.DrawString("第二点:          间隙。", ptzt12, Brushes.Black, new Point(x + 105, y + 60));
            e.Graphics.DrawString("□成功□失败□穿破", ptzt12, Brushes.Black, new Point(x + 260, y + 60));
            e.Graphics.DrawString("③□直入法□侧入法，斜面:□向头□向骶□向侧", ptzt12, Brushes.Black, new Point(x + 40, y + 90));
            e.Graphics.DrawString("④黄韧带感觉：□明显□尚明显□不明显", ptzt12, Brushes.Black, new Point(x + 40, y + 120));
            e.Graphics.DrawString("⑤负压:□大□小□无，搏动:□大□小□无", ptzt12, Brushes.Black, new Point(x + 40, y + 150));
            e.Graphics.DrawString("⑥注射阻力:□很松□尚松□不松。回流：□多□少□无。", ptzt12, Brushes.Black, new Point(x + 410, y + 3));
            e.Graphics.DrawString("⑦沾血：□无□有□穿刺□置管□淡□全血", ptzt12, Brushes.Black, new Point(x + 410, y + 30));
            
            e.Graphics.DrawString("⑧皮肤至硬膜外腔深度", ptzt12, Brushes.Black, new Point(x + 410, y + 60));
            e.Graphics.DrawString("导管插入深度", ptzt12, Brushes.Black, new Point(x + 600, y + 60));
            e.Graphics.DrawString("⑨导管插入情况:□顺利□不顺利，原因:", ptzt12, Brushes.Black, new Point(x + 410, y + 90));
            e.Graphics.DrawString("麻醉平面:手术始:上达:", ptzt12, Brushes.Black, new Point(x + 410, y + 120));
            e.Graphics.DrawString("    下达:", ptzt12, Brushes.Black, new Point(x + 600, y + 120));
            e.Graphics.DrawString("手术毕:上达", ptzt12, Brushes.Black, new Point(x + 475, y + 150));
            e.Graphics.DrawString("    下达:", ptzt12, Brushes.Black, new Point(x + 600, y + 150));

            for (int i = 0; i < 7; i++)
            {
                if (i == 0 || i == 2 || i == 6)
                {
                    e.Graphics.DrawLine(Pens.Black, x + 5, y + 180, x + 780, y + 180);
                }
                e.Graphics.DrawLine(Pens.Black, x + 35, y + 180, x + 780, y + 180);
                y = y + 30;
            }
            y = 20;
            e.Graphics.DrawString("全\n麻\n操\n作", ptzt8, Brushes.Black, new Point(x + 10, y + 180));
            e.Graphics.DrawString("麻\n醉\n期\n间\n并\n发\n症", ptzt9, Brushes.Black, new Point(x + 10, y + 240));
            e.Graphics.DrawString("□口插管", ptzt08, Brushes.Black, new Point(x + 40, y + 190));
            e.Graphics.DrawString("鼻插管□左□右", ptzt08, Brushes.Black, new Point(x + 40, y + 220));
            e.Graphics.DrawString("□牙齿损伤", ptzt08, Brushes.Black, new Point(x + 40, y + 250));
            e.Graphics.DrawString("□黏膜损伤", ptzt08, Brushes.Black, new Point(x + 40, y + 280));
            e.Graphics.DrawString("□误吸", ptzt08, Brushes.Black, new Point(x + 40, y + 310));
            e.Graphics.DrawString("□呕吐", ptzt08, Brushes.Black, new Point(x + 40, y + 340));
            e.Graphics.DrawLine(Pens.Black, x + 120, y + 180, x + 120, y + 360);
            e.Graphics.DrawString("双腔管□左□右", ptzt08, Brushes.Black, new Point(x + 125, y + 190));
            e.Graphics.DrawString("□纤维镜", ptzt08, Brushes.Black, new Point(x + 125, y + 220));
            e.Graphics.DrawString("□舌下坠", ptzt08, Brushes.Black, new Point(x + 125, y + 250));
            e.Graphics.DrawString("□支气管痉挛", ptzt08, Brushes.Black, new Point(x + 125, y + 280));
            e.Graphics.DrawString("□急性肺水肿", ptzt08, Brushes.Black, new Point(x + 125, y + 310));
            e.Graphics.DrawString("□肺栓塞", ptzt08, Brushes.Black, new Point(x + 125, y + 340));
            e.Graphics.DrawLine(Pens.Black, x + 210, y + 180, x + 210, y + 360);
            e.Graphics.DrawString("□盲插", ptzt08, Brushes.Black, new Point(x + 215, y + 190));
            e.Graphics.DrawString("□带管芯插", ptzt08, Brushes.Black, new Point(x + 215, y + 220));
            e.Graphics.DrawString("□气胸", ptzt08, Brushes.Black, new Point(x + 215, y + 250));
            e.Graphics.DrawString("□缺氧", ptzt08, Brushes.Black, new Point(x + 215, y + 280));
            e.Graphics.DrawString("□CO2蓄积", ptzt08, Brushes.Black, new Point(x + 215, y + 310));
            e.Graphics.DrawString("□呼吸停止", ptzt08, Brushes.Black, new Point(x + 215, y + 340));
            e.Graphics.DrawLine(Pens.Black, x + 280, y + 180, x + 280, y + 360);
            e.Graphics.DrawString("□导管ID", ptzt08, Brushes.Black, new Point(x + 285, y + 190));
            e.Graphics.DrawString("□插管困难", ptzt08, Brushes.Black, new Point(x + 285, y + 220));
            e.Graphics.DrawString("□高血压", ptzt08, Brushes.Black, new Point(x + 285, y + 250));
            e.Graphics.DrawString("□低血压", ptzt08, Brushes.Black, new Point(x + 285, y + 280));
            e.Graphics.DrawString("□心律失常", ptzt08, Brushes.Black, new Point(x + 285, y + 310));
            e.Graphics.DrawString("□心衰", ptzt08, Brushes.Black, new Point(x + 285, y + 340));
            e.Graphics.DrawString("麻醉诱导期:□满意□呛咳□发绀□呕吐□喉痉挛□激动", ptzt08, Brushes.Black, new Point(x + 395, y + 190));

            e.Graphics.DrawString("其他：", ptzt08, Brushes.Black, new Point(x + 395, y + 220));

            e.Graphics.DrawString("□心跳骤停", ptzt08, Brushes.Black, new Point(x + 395, y + 250));
            e.Graphics.DrawString("□局麻药过敏", ptzt08, Brushes.Black, new Point(x + 395, y + 280));
            e.Graphics.DrawString("□局麻药中毒", ptzt08, Brushes.Black, new Point(x + 395, y + 310));
            e.Graphics.DrawString("□膈N阻滞", ptzt08, Brushes.Black, new Point(x + 395, y + 340));
            e.Graphics.DrawLine(Pens.Black, x + 485, y + 240, x + 485, y + 360);
            e.Graphics.DrawString("□喉返N阻滞", ptzt08, Brushes.Black, new Point(x + 490, y + 250));
            e.Graphics.DrawString("□霍纳氏症", ptzt08, Brushes.Black, new Point(x + 490, y + 280));
            e.Graphics.DrawString("□脊N广泛阻滞", ptzt08, Brushes.Black, new Point(x + 490, y + 310));
            e.Graphics.DrawString("□硬膜外导管折断", ptzt08, Brushes.Black, new Point(x + 490, y + 340));
            e.Graphics.DrawLine(Pens.Black, x + 600, y + 240, x + 600, y + 360);
            e.Graphics.DrawString("□全脊麻", ptzt08, Brushes.Black, new Point(x + 605, y + 250));
            e.Graphics.DrawString("□硬膜穿破", ptzt08, Brushes.Black, new Point(x + 605, y + 280));
            e.Graphics.DrawString("其他：", ptzt08, Brushes.Black, new Point(x + 605, y + 310));
            e.Graphics.DrawLine(Pens.Black, x + 680, y + 240, x + 680, y + 300);
            e.Graphics.DrawLine(Pens.Black, x + 680, y + 330, x + 680, y + 360);
            e.Graphics.DrawString("特\n殊\n病\n人\n麻\n醉\n及\n麻\n醉\n异\n常\n情\n况\n分\n析\n总\n结", ptzt9, Brushes.Black, new Point(x + 10, y + 420));

            e.Graphics.DrawLine(Pens.Black, x + 5, y + 890, x + 780, y + 890);
            e.Graphics.DrawString("说\n\n\n\n明", ptzt9, Brushes.Black, new Point(x + 10, y + 900));
            e.Graphics.DrawString("1.安/异/地/七：指各类氟醚", ptzt10, Brushes.Black, new Point(x + 50, y + 900));
            e.Graphics.DrawString("2.司/本/万/阿：指肌松药的第一字", ptzt10, Brushes.Black, new Point(x + 50, y + 930));
            e.Graphics.DrawString("3.病人送往：1.2.3.    1指病房   2指恢复室   3指重症监护室", ptzt10, Brushes.Black, new Point(x + 50, y + 960));
            e.Graphics.DrawString("4.评分标准按照《临床麻醉管理与技术规范》", ptzt10, Brushes.Black, new Point(x + 50, y + 990));

            e.Graphics.DrawString("麻醉总结审阅者：", ptzt10, Brushes.Black, new Point(x + 350, y + 1044));
            e.Graphics.DrawString("麻醉总结者：", ptzt10, Brushes.Black, new Point(x + 600, y + 1044));
            //数据
            #region    
           
            
            for (int a = 0; a < dt.Rows.Count; a++)  //  a行 b列
            {
                
                for (int c = 1; c < dt.Columns.Count; c++) // 列
                {
                    string datas = dt.Rows[a][c].ToString();
                    if (c == 2)
                    {
                        e.Graphics.DrawString(datas, ptzt12, Brushes.Black, x + 151, y + 30);
                    }
                    if (c == 3)
                    {
                        e.Graphics.DrawString(datas, ptzt12, Brushes.Black, x + 152, y + 60);
                    }
                    if (c == 4)
                    {
                        e.Graphics.DrawString(datas + "cm", ptzt12, Brushes.Black, x + 550, y + 60);
                    }
                    if (c == 5)
                    {
                        e.Graphics.DrawString(datas+"cm", ptzt12, Brushes.Black, x + 685, y + 60);
                    }
                    if (c == 6)
                    {
                        e.Graphics.DrawString(datas, ptzt12, Brushes.Black, x + 666, y + 90);
                    }
                    if (c == 7)
                    {
                        e.Graphics.DrawString(datas, ptzt12, Brushes.Black, x + 560, y + 120);
                    }
                    if(c==8)
                    {
                        e.Graphics.DrawString(datas, ptzt12, Brushes.Black, x + 666, y + 120);
                    }
                    if(c==9)
                    {
                        e.Graphics.DrawString(datas, ptzt12, Brushes.Black, x + 560, y + 150);
                    }
                    if(c==10)
                    {
                        e.Graphics.DrawString(datas, ptzt12, Brushes.Black, x + 666, y + 150);
                    }
                    if (c == 11)
                    {
                        e.Graphics.DrawString(datas, ptzt08, Brushes.Black, x + 425, y + 220);
                    }
                    if (c == 12)
                    {
                        e.Graphics.DrawString(datas, ptzt08, Brushes.Black, x + 635, y + 310);
                    }
                    if (c == 13)
                    {
                        string temp = "";
                        int len1 = 0;
                        int k = len1, num2 = len1;
                        SubString(fxzj.Text, 52, k, out temp, out num2);//数据到什么位置换行
                        len1 = num2;
                        e.Graphics.DrawString(temp, ptzt10, Brushes.Black, x + 40, y + 370);
                    }
                    if (c == 14)
                    {
                        e.Graphics.DrawString(datas, ptzt10, Brushes.Black, x + 470, y + 1044);
                    } 
                    if (c == 15)
                    {
                        e.Graphics.DrawString(datas, ptzt10, Brushes.Black, x + 690, y + 1044);
                    }
                    //复选框，逐一取出每一个字符进行判断
                    if (c == 16)
                    {
                        string cd = dt.Rows[0]["cctw"].ToString();
                        for (int i = 0; i < cd.Length; i++)
                        {
                            string b = cd.Substring(i, 1);
                            string sj = b;
                            if (sj == "1")
                            {
                                e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 160, y));
                            }
                            else if (sj == "2")
                            {
                                if (ccyzcw.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 188, y));
                            }
                            else if (sj == "3")
                            {
                                if (ccfw.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 257, y));
                            }
                        }
                    }
                   if (c == 17)
                    {
                        string cd = dt.Rows[0]["ccd"].ToString();
                        for (int i = 0; i < cd.Length; i++)
                        {
                            string b = cd.Substring(i, 1);
                            string sj = b;
                               
                            if (sj == "1")
                            {
                                if (ccdycg.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 261, y + 28));

                            }
                            else if (sj == "2")
                            {
                                if (ccdysb.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 304, y + 28));

                            }
                            else if (sj == "3")
                            {
                                if (ccdycp.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 346, y + 28));
                            }
                            else if (sj == "4")
                            {
                                if (ccdrdcg.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 261, y + 58));

                            }
                            else if (sj == "5")
                            {
                                if (ccdrdsb.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 304, y + 58));

                            }
                            else if (sj == "6")
                            {
                                if (ccdrdcp.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 346, y + 58));
                            }
                             
                        }
                   }
                   if (c == 18)
                   {
                       string cd = dt.Rows[0]["zrf"].ToString();
                       for (int i = 0; i < cd.Length; i++)
                       {
                           string b = cd.Substring(i, 1);
                           string sj = b;
                            if (sj == "1")
                            {
                                if (zrf.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 55, y + 88));
                            }
                            else if (sj == "2")
                            {
                                if (crf.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 112, y + 88));
                            }
                            else if (sj == "3")
                            {
                                if (xt.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 216, y + 88));
                            }
                            else if (sj == "4")
                            {
                                if (xd.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 258, y + 88));
                            }
                            else if (sj == "5")
                            {
                                if (xc.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 299, y + 88));
                            }
                       }
                   }
                    if (c == 19)
                   {
                       string cd = dt.Rows[0]["hrdgj"].ToString();
                       for (int i = 0; i < cd.Length; i++)
                       {
                           string b = cd.Substring(i, 1);
                           string sj = b;
                            if (sj == "1")
                            {
                                if (hrdmx.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 140, y + 118));


                            }
                            else if (sj == "2")
                            {
                                if (hrdsmx.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 181, y + 118));


                            }
                            else if (sj == "3")
                            {
                                if (hrdbmx.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 237, y + 118));
                            }
                       }
                   }
                    if (c == 20)
                   {
                       string cd = dt.Rows[0]["fybd"].ToString();
                       for (int i = 0; i < cd.Length; i++)
                       {
                           string b = cd.Substring(i, 1);
                           string sj = b;
                            if (sj == "1")
                            {
                                if (fyd.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 90, y + 148));
                            }
                            else if (sj == "2")
                            {
                                if (fyx.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 119, y + 148));
                            }
                            else if (sj == "3")
                            {
                                if (fyw.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 146, y + 148));
                            }
                            else if (sj == "4")
                            {
                                if (bdd.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 222, y + 148));

                            }
                            else if (sj == "5")
                            {
                                if (bdx.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 251, y + 148));

                            }
                            else if (sj == "6")
                            {
                                if (bdw.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 280, y + 148));
                            }
                       }
                   }
                    if (c == 21)
                   {
                       string cd = dt.Rows[0]["zszl"].ToString();
                       for (int i = 0; i < cd.Length; i++)
                       {
                           string b = cd.Substring(i, 1);
                           string sj = b;
                           if (sj == "1")
                            {
                                if (zshs.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 488, y));
                            }
                            else if (sj == "2")
                            {
                                if (zsss.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 531, y));
                            }
                            else if (sj == "3")
                            {
                                if (zsbs.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 571, y));
                            }
                            else if (sj == "4")
                            {
                                if (hld.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 669, y));
                            }
                            else if (sj == "5")
                            {
                                if (hls.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 697, y));
                            }
                            else if (sj == "6")
                            {
                                if (hlw.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 724, y));
                            }
                       }
                   }
                    if (c == 22)
                   {
                       string cd = dt.Rows[0]["zx"].ToString();
                       for (int i = 0; i < cd.Length; i++)
                       {
                           string b = cd.Substring(i, 1);
                           string sj = b;
                            if (sj == "1")
                            {
                                if (zxw.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 468, y + 28));

                            }
                            else if (sj == "2")
                            {
                                if (zxy.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 494, y + 28));

                            }
                            else if (sj == "3")
                            {
                                if (zxcc.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 524, y + 28));

                            }
                            else if (sj == "4")
                            {
                                if (zxzg.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 564, y + 28));

                            }
                            else if (sj == "5")
                            {
                                if (zxd.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 606, y + 28));

                            }
                            else if (sj == "6")
                            {
                                if (zxqx.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 634, y + 28));
                            }
                       }
                   }
                    if (c == 23)
                   {
                       string cd = dt.Rows[0]["dgcrqk"].ToString();
                       for (int i = 0; i < cd.Length; i++)
                       {
                           string b = cd.Substring(i, 1);
                           string sj = b;
                            if (sj == "1")
                            {
                                if (cgcrsl.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 516, y + 88));

                            }
                            else if (sj == "2")
                            {
                                if (dgcrbsl.Checked) e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x + 558, y + 88));
                            }
                       }
                   }
                    if (c == 24)
                   {
                       string cd = dt.Rows[0]["qmcz"].ToString();
                       for (int i = 0; i < cd.Length; i++)
                       {
                           string b = cd.Substring(i, 1);
                           string sj = b;
                            if (sj == "1")
                            {
                                if (kcg.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 40, y + 187));
                            }
                            else if (sj == "2")
                            {
                                if (sqgz.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 161, y + 187));
                            }
                            else if (sj == "3")
                            {
                                if (sqgy.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 184, y + 187));
                            }
                            else if (sj == "4")
                            {
                                if (mc.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 217, y + 187));
                            }
                            else if (sj == "5")
                            {
                                if (dgID.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 287, y + 187));
                            }else if (sj == "6")
                            {
                                if (bcgz.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 74, y + 217));

                            }
                            else if (sj == "7")
                            {
                                if (bcgy.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 97, y + 217));
                            }
                            else if (sj == "8")
                            {
                                if (qwj.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 127, y + 217));
                            }
                            else if (sj == "9")
                            {
                                if (dgxc.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 217, y + 217));

                            }
                            else if (sj == "a")
                            {
                                if (cgkn.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 287, y + 217));
                            }
                       }
                   }
                    if (c == 25)
                   {
                       string cd = dt.Rows[0]["mzydq"].ToString();
                       for (int i = 0; i < cd.Length; i++)
                       {
                           string b = cd.Substring(i, 1);
                           string sj = b;
                             if (sj == "1")
                            {
                                if (mzydmy.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 458, y + 187));
                            }
                            else if (sj == "2")
                            {
                                if (mzydqk.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 490, y + 187));

                            }
                            else if (sj == "3")
                            {
                                if (mzydfq.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 524, y + 187));
                            }
                            else if (sj == "4")
                            {
                                if (mzydot.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 558, y + 187));
                            }
                            else if (sj == "5")
                            {
                                if (mzydhjl.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 592, y + 187));

                            }
                            else if (sj == "6")
                            {
                                if (mzydjd.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 637, y + 187));
                            }
                       }
                   }
                    if (c == 26)
                   {
                       string cd = dt.Rows[0]["mzqjbfz1"].ToString();
                       for (int i = 0; i < cd.Length; i++)
                       {
                           string b = cd.Substring(i, 1);
                           string sj = b;
                             if (sj == "1")
                            {
                                if (ycss.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 42, y + 247));
                            }
                            else if (sj == "2")
                            {
                                if (sxc.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 127, y + 247));
                            }
                            else if (sj == "3")
                            {
                                if (qx.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 217, y + 247));
                            }
                            else if (sj == "4")
                            {

                                if (gxy.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 287, y + 247));
                            }
                            else if (sj == "5")
                            {
                                if (xtzt.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 397, y + 247));
                            }
                            else if (sj == "6")
                            {
                                if (hfNzz.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 492, y + 247));
                            }
                            else if (sj == "7")
                            {

                                if (qjm.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 607, y + 247));
                            }
                       }
                   }
                    if (c == 27)
                   {
                       string cd = dt.Rows[0]["mzqjbfz2"].ToString();
                       for (int i = 0; i < cd.Length; i++)
                       {
                           string b = cd.Substring(i, 1);
                           string sj = b;
                            if (sj == "1")
                            {
                                if (lmss.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 42, y + 277));
                            }
                            else if (sj == "2")
                            {
                                if (zqgjl.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 127, y + 277));
                            }
                            else if (sj == "3")
                            {

                                if (qy.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 217, y + 277));
                            }
                            else if (sj == "4")
                            {
                                if (dxy.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 287, y + 277));
                            }
                            else if (sj == "5")
                            {
                                if (jmygm.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 397, y + 277));
                            }
                            else if (sj == "6")
                            {

                                if (hnsz.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 492, y + 277));
                            }
                            else if (sj == "7")
                            {
                                if (ymcp.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 607, y + 277));
                            }
                       }
                   }
                    if (c == 28)
                   {
                       string cd = dt.Rows[0]["mzqjbfz3"].ToString();
                       for (int i = 0; i < cd.Length; i++)
                       {
                           string b = cd.Substring(i, 1);
                           string sj = b;
                            if (sj == "1")
                            {
                                if (wx.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 42, y + 307));
                            }
                            else if (sj == "2")
                            {

                                if (jxfsz.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 127, y + 307));
                            }
                            else if (sj == "3")
                            {
                                if (xj.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 217, y + 307));
                            }
                            else if (sj == "4")
                            {

                                if (xlsc.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 287, y + 307));
                            }
                            else if (sj == "5")
                            {
                                if (jmyzd.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 397, y + 307));
                            }
                            else if (sj == "6")
                            {
                                if (jNgfzz.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 492, y + 307));
                            }
                       }
                   }
                    if (c == 29)
                   {
                       string cd = dt.Rows[0]["mzqjbfz4"].ToString();
                       for (int i = 0; i < cd.Length; i++)
                       {
                           string b = cd.Substring(i, 1);
                           string sj = b;
                             if (sj == "1")
                            {
                                if (ot.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 42, y + 337));
                            }
                            else if (sj == "2")
                            {
                                if (fss.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 127, y + 337));
                            }
                            else if (sj == "3")
                            {

                                if (hxtz.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 217, y + 337));
                            }
                            else if (sj == "4")
                            {
                                if (xs.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 287, y + 337));
                            }
                            else if (sj == "5")
                            {
                                if (gNzz.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 397, y + 337));
                            }
                            else if (sj == "6")
                            {
                                if (ymwdgzd.Checked) e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x + 492, y + 337));
                            }
                       }
                   }
                }
            }
            #endregion
        }
        private void SubString(string datas, int num, int len1, out string ReString, out int lens)//字体换行
        {
            if (datas.Length > num)
            {
                string str = "";
                int len = datas.Length / num;
                if (len > len1)
                {
                    if (datas.Length % num > 0)
                    {
                        len1 = len + 1;
                    }
                    else
                        len1 = len;
                }
                for (int i = 0; i <= datas.Length / num; i++)//num个字符就换行
                {
                    if (i < len)
                    {
                        str += datas.Substring(i * num, num) + Environment.NewLine; //从第i*5个开始，截取5个字符串
                    }
                    else
                    {
                        str += datas.Substring(i * num);
                    }
                }
                ReString = str;
                lens = len1;
            }
            else
            {
                ReString = datas;
                lens = len1;
            }


        }


    }
}
