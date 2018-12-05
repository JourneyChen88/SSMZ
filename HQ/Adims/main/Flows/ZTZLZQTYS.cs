using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL.Flows;
using adims_BLL;

namespace main
{
    public partial class ZTZLZQTYS : Form
    {
        OperScheduleDal _paibanDal = new OperScheduleDal();
        ZtzltysDal _ZtzltysDal = new ZtzltysDal();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_DAL.PacuDal pdal = new adims_DAL.PacuDal();
        string _PatId, Odate;
        bool isRead = false;
        public ZTZLZQTYS(string patid, string date)
        {
            _PatId = patid;
            Odate = date;
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZTZLZQTYS_Load(object sender, EventArgs e)
        {
            Load_info();
            LodSQFS();
        }
        /// 加载选择病人信息
        /// </summary>
        public void Load_info()
        {
            DataTable dt1 = _paibanDal.GetPaibanByPatId(_PatId);
            DataRow dr1 = dt1.Rows[0];      
            tbPatname.Text = dr1["Patname"].ToString();
            tbPatsex.Text = dr1["patsex"].ToString();
            tbPatage.Text = dr1["patage"].ToString();
            tbZhuyuanID.Text = dr1["patZhuyuanID"].ToString();
            tbSQZD.Text = dr1["pattmd"].ToString();
           
        }
        int BCcount = 0;
        /// <summary>
        /// 赋值
        /// </summary>
        private void LodSQFS()
        {
            DataTable dt = _ZtzltysDal.GetZTZLZQTYS_YS(_PatId);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                tbSQZD.Text = dr["Zd"].ToString();
                txtZT.Text = dr["ZTfs"].ToString();
                txtHzqm2.Text = dr["Hzqm"].ToString();              
                txtJsqm.Text = dr["Jsqm"].ToString();
                txtYhzgx.Text = dr["Yhzgx"].ToString();
                txtMZYsqm.Text = dr["MZYsqm"].ToString();
                txtZLysqm.Text = dr["ZLysqm"].ToString();    
                dtVisitDate.Text = dr["VisitDate"].ToString();
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
                    btnJS.Visible = true;
                }
                else
                {
                    btnJS.Visible = false;
                }
            }

        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            Dictionary<string, string> MZZQTY = new Dictionary<string, string>();
            int result = 0;
            try
            {
                MZZQTY.Add("_PatId", tbZhuyuanID.Text);
                MZZQTY.Add("Zd", tbSQZD.Text);
                MZZQTY.Add("ZTfs", txtZT.Text);
                MZZQTY.Add("Hzqm", txtHzqm2.Text);             
                MZZQTY.Add("Jsqm", txtJsqm.Text);
                MZZQTY.Add("Yhzgx", txtYhzgx.Text);
                MZZQTY.Add("MZYsqm", txtMZYsqm.Text);
                MZZQTY.Add("ZLysqm", txtZLysqm.Text);
                MZZQTY.Add("VisitDate", Convert.ToDateTime(dtVisitDate.Value.ToString()).ToString("yyyy-MM-dd"));
                if (btnSave.Enabled)
                {
                    MZZQTY.Add("IsRead", "0");
                }
                else
                {
                    MZZQTY.Add("IsRead", "1");
                }
                MZZQTY.Add("Odate", Convert.ToDateTime(Odate).ToString("yyyy-MM-dd"));
                DataTable dt = _ZtzltysDal.GetZTZLZQTYS_YS(_PatId);
                if (dt.Rows.Count > 0)
                    result = _ZtzltysDal.UpdateZTZLTY_YS_HQ(MZZQTY);
                else
                    result = _ZtzltysDal.InsertZTZLTY_YS_HQ(MZZQTY);
                if (result > 0)
                {
                    BCcount++; MessageBox.Show("保存成功！");
                }
                else MessageBox.Show("保存失败！");
            }
            catch (Exception)
            {
                MessageBox.Show("保存异常，请检查网络！");
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        /// <summary>
        /// 存档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCD_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = _ZtzltysDal.GetZTZLZQTYS_YS(_PatId);
            if (dt.Rows.Count == 0)
                MessageBox.Show("需要先保存，才能存档！");
            else
            {
                result = _ZtzltysDal.UpdateZQZLTYS_HQ_YS(_PatId, 1);
                if (result > 0)
                {
                    MessageBox.Show("存档成功！");
                    isRead = true;
                    btnSave.Enabled = false;
                    btnCD.Enabled = false;
                }
            }
        }
        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJS_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = _ZtzltysDal.GetZTZLZQTYS_YS(_PatId);
            if (dt.Rows[0]["isRead"].ToString() == "1")
            {
                result = _ZtzltysDal.UpdateZQZLTYS_HQ_YS(_PatId, 0);
                if (result > 0)
                {
                    MessageBox.Show("解锁成功！");
                    btnSave.Enabled = true;
                    isRead = false;
                    btnCD.Enabled = true;                    
                }
            }
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (isRead)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.ZTZLZQTYS_FormClosing);
                this.Close();
            }
            else if (BCcount > 0)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.ZTZLZQTYS_FormClosing);
                this.Close();
            }
            else
            {
                if (MessageBox.Show("保存退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Save();
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.ZTZLZQTYS_FormClosing);
                    this.Close();
                }
                else
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.ZTZLZQTYS_FormClosing);
                    this.Close();
                }
            }
        }

        private void ZTZLZQTYS_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isRead)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.ZTZLZQTYS_FormClosing);
                this.Close();
            }
            else if (BCcount > 0)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.ZTZLZQTYS_FormClosing);
                this.Close();
            }
            else
            {
                if (MessageBox.Show("保存退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Save();
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.ZTZLZQTYS_FormClosing);
                    this.Close();
                }
                else
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.ZTZLZQTYS_FormClosing);
                    this.Close();
                }
            }
        }
        private void btnDY_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();  
        }
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("K16", 740, 1020);  
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Pen ptp = Pens.Black;//普通画笔
            Pen pblue2 = new Pen(Brushes.Blue);
            pblue2.Width = 2;
            Pen pblack = new Pen(Brushes.Black);
            Pen pblack2 = new Pen(Brushes.Black, 2);
            Font ptzt = new Font("新宋体", 10);//普通字体
            Font ptzt1 = new Font("宋体", 10);//普通字体
            Font ptzt2 = new Font("黑体", 10, FontStyle.Bold);//加粗十号
            Font ptzt3 = new Font("新宋体", 14, FontStyle.Bold);//加粗14号
            int y = 80; int x = 60; int y1 = 0;
            string title1 = "天津红桥医院镇痛治疗知情同意书";
            e.Graphics.DrawString(title1, ptzt3, Brushes.Black, new Point(x + 170, y));
            y = y + 40;
            e.Graphics.DrawLine(Pens.Black, x, y, x + 600, y);
            e.Graphics.DrawString("姓名 ", ptzt, Brushes.Black, new Point(x + 10, y + 10));
            e.Graphics.DrawLine(Pens.Black, x + 60, y, x + 60, y + 30);
            e.Graphics.DrawString(tbPatname.Text, ptzt, Brushes.Black, new Point(x + 70, y + 10));
            e.Graphics.DrawLine(Pens.Black, x + 150, y, x + 150, y + 30);
            e.Graphics.DrawString("性别 ", ptzt, Brushes.Black, new Point(x + 160, y + 10));
            e.Graphics.DrawLine(Pens.Black, x + 210, y, x + 210, y + 30);
            e.Graphics.DrawString( tbPatsex.Text.Trim(), ptzt, Brushes.Black, new Point(x + 220, y + 10));
            e.Graphics.DrawLine(Pens.Black, x + 300, y, x + 300, y + 30);
            e.Graphics.DrawString("年龄 " , ptzt, Brushes.Black, new Point(x + 310, y + 10));
            e.Graphics.DrawLine(Pens.Black, x + 360, y, x + 360, y + 30);
            e.Graphics.DrawString(this.tbPatage.Text.Trim() + "岁", ptzt, Brushes.Black, new Point(x + 370, y + 10));
            e.Graphics.DrawLine(Pens.Black, x + 420, y, x + 420, y + 30);
            e.Graphics.DrawString("住院号 ", ptzt, Brushes.Black, new Point(x + 430, y + 10));
            e.Graphics.DrawLine(Pens.Black, x + 480, y, x + 480, y + 30);
            e.Graphics.DrawString( this.tbZhuyuanID.Text.Trim(), ptzt, Brushes.Black, new Point(x + 490, y + 10));
            y = y + 30;
            e.Graphics.DrawLine(Pens.Black, x, y, x + 600, y);
            y = y + 10;
            string str1_zd = "";
            int StrLength_zd = tbSQZD.Text.Trim().Length;
            int row_zd = StrLength_zd / 38;
            e.Graphics.DrawString("诊断 ", ptzt, Brushes.Black, x + 10, y);
            for (int i = 0; i <= row_zd; )//50个字符就换行
            {
                if (i < row_zd)
                    str1_zd = tbSQZD.Text.ToString().Substring(i * 38, 38); //从第i*50个开始，截取50个字符串
                else
                    str1_zd = tbSQZD.Text.ToString().Substring(i * 38);
                e.Graphics.DrawString(str1_zd, ptzt, Brushes.Black, x + 50, y);
                i++;
                if (i > row_zd)
                {

                }
                else
                {
                    y = y + 15;
                }

            }           
            //e.Graphics.DrawString("诊断  "+tbSQZD.Text, ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 25;
            e.Graphics.DrawLine(Pens.Black, x, y, x + 600, y);
            y = y + 10;
            e.Graphics.DrawString("镇痛方式  " + txtZT.Text, ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 25;
            e.Graphics.DrawLine(Pens.Black, x, y, x + 600, y);
            y = y + 10;
            e.Graphics.DrawString("镇痛治疗因用药、有创操作及您原有并存病，可能发生以下危险及并发症：" , ptzt, Brushes.Black, new Point(x + 40, y));
            y = y + 30;
            e.Graphics.DrawString("一.    药物过敏反应、毒性反应；", ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 30;
            e.Graphics.DrawString("二.    呼吸抑制、循环抑制；", ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 30;
            e.Graphics.DrawString("三.    恶心呕吐或反流误吸；", ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 30;
            e.Graphics.DrawString("四.    镇痛不全；", ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 30;
            e.Graphics.DrawString("五.    可能原有并存病的加重；", ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 30;
            e.Graphics.DrawString("六.    由于机体抵抗力低下，可发生注射部位感染；", ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 30;
            e.Graphics.DrawString("七.    采用椎管内阻滞技术时，可发生腰痛、头痛、神经损伤、硬膜外血肿、感染、排尿困", ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 30;
            e.Graphics.DrawString("       难及硬膜外导管脱出等；", ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 30;
            e.Graphics.DrawString("八.    因病情需要，可能使用医保范围以外的自费药或物品；", ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 30;
            e.Graphics.DrawString("九.    其他相关问题；", ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 50;
            e.Graphics.DrawLine(Pens.Black, x, y, x + 600, y);
            y = y + 10;
            e.Graphics.DrawString("我对上述可能发生的危险及并发症已经十分清楚，愿意接受此次镇痛治疗。", ptzt, Brushes.Black, new Point(x + 40, y));
            y = y + 50;
            e.Graphics.DrawString("病人签字： " + txtHzqm2.Text, ptzt, Brushes.Black, new Point(x + 40, y));
            y = y + 30;
            e.Graphics.DrawString("或受委托人（与病人关系  " + txtYhzgx.Text+"      )", ptzt, Brushes.Black, new Point(x + 40, y));
            e.Graphics.DrawString("签字： "+txtJsqm.Text, ptzt, Brushes.Black, new Point(x + 350, y));
            y = y + 30;
            e.Graphics.DrawString("麻醉医师签字： " + txtMZYsqm.Text, ptzt, Brushes.Black, new Point(x + 40, y));
            y = y + 30;
            e.Graphics.DrawString("经治医师签字： " + txtZLysqm.Text, ptzt, Brushes.Black, new Point(x + 40, y));
            y = y + 60;
            e.Graphics.DrawString(dtVisitDate.Text, ptzt, Brushes.Black, new Point(x + 440, y));
            y = y + 30;
            e.Graphics.DrawLine(Pens.Black, x, y, x + 600, y);
            e.Graphics.DrawLine(Pens.Black, x, 120, x , y);
            e.Graphics.DrawLine(Pens.Black, x+600, 120, x+600, y);
        }


     
    }
}
