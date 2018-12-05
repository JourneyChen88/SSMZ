using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL.Flows;
using adims_DAL.Dics;
using adims_BLL;

namespace main
{
    public partial class AfterVisit_HQ : Form
    {
        DataDicDal _DataDicDal = new DataDicDal();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_DAL.Flows.AfterVisitDal _AfterVisitDal = new AfterVisitDal();
        adims_DAL.PacuDal _PacuDal = new adims_DAL.PacuDal();
        OperScheduleDal _PaibanDal = new OperScheduleDal();
        MzjldDal _MzjldDal = new MzjldDal();
        int MzjldId;
        string PatId, Odate;
        bool isRead = false;
        public AfterVisit_HQ(int mzjldId, string patId)
        {
            MzjldId = mzjldId;
            PatId = patId;
            InitializeComponent();
        }


        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeforeShfs_HQ_Load(object sender, EventArgs e)
        {

            Load_info();
            BindMZYY();
            cmbMzys.Text = Program.customer.user_name;
            LodSQFS();
        }
        /// <summary>
        /// 绑定麻醉医师
        /// </summary>
        private void BindMZYY()
        {
            DataTable dtMZYS = _DataDicDal.GetAllMZYS();
            cmbMzys.Items.Clear();
            for (int i = 0; i < dtMZYS.Rows.Count; i++)
            {
                this.cmbMzys.Items.Add(dtMZYS.Rows[i][0]);
            }
        }
        /// 加载选择病人信息
        /// </summary>
        public void Load_info()
        {
            DataTable dt1 = _PaibanDal.GetPaibanByPatId(PatId);
            DataRow dr1 = dt1.Rows[0];
            tbKeshi.Text = dr1["patdpm"].ToString();
            tbPatname.Text = dr1["Patname"].ToString();
            tbPatsex.Text = dr1["patsex"].ToString();
            tbPatage.Text = dr1["patage"].ToString();
            tbZhuyuanID.Text = dr1["patZhuyuanID"].ToString();
            dtVisitDate.Text = Odate;
            DataTable dt2 = _MzjldDal.GetMzjldByPatId(PatId);
            //DataTable dt3= DAL.GetMzjld_HQ(PatId, Convert.ToDateTime(Odate).AddDays(1).ToString("yyyy-MM-dd"));
            if (dt2.Rows.Count > 0)
            {
                DataRow dr2 = dt2.Rows[0];
                cmbmzfs.Text = dr2["MazuiFS"].ToString();
                txtMzsj.Text = dr2["Mzsj"].ToString();
            }
            //else if (dt3.Rows.Count > 0)
            //{
            //    DataRow dr3 = dt3.Rows[0];
            //    cmbmzfs.Text = dr3["MazuiFS"].ToString();
            //    txtMzsj.Text = dr3["Mzsj"].ToString();
            //}
        }
        int BCcount = 0;
        /// <summary>
        /// 赋值
        /// </summary>
        private void LodSQFS()
        {
            DataTable dt = _AfterVisitDal.GetAfterVisit_YS_byMzjldid(MzjldId);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                cmbmzfs.Text = dr["mzfs"].ToString();
                if (dr["Mzsj"].ToString() != "")
                {
                    txtMzsj.Text = dr["Mzsj"].ToString();
                }
                cmbYishi.Text = dr["Yishi"].ToString();
                cmbHuxi.Text = dr["Huxi"].ToString();
                cmbXunhuan.Text = dr["Xunhuan"].ToString();
                if (Convert.ToString(dr["Shzysx"]).Contains("1")) Shzysx1.Checked = true;
                if (Convert.ToString(dr["Shzysx"]).Contains("2")) Shzysx2.Checked = true;
                if (Convert.ToString(dr["Shzysx"]).Contains("3")) Shzysx3.Checked = true;
                txtQt.Text = dr["Qt"].ToString();
                cmbmzxgbfz.Text = dr["mzxgbfz"].ToString();
                txtMs.Text = dr["Ms"].ToString();
                txtcl.Text = dr["cl"].ToString();
                txtjg.Text = dr["jg"].ToString();
                if (dr["Mzys"].ToString() != "")
                {
                    cmbMzys.Text = dr["Mzys"].ToString();
                }
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
            Dictionary<string, string> SHFS = new Dictionary<string, string>();
            int result = 0;
            string AddItem = "";

            try
            {
                SHFS.Add("mzjld", MzjldId.ToString());

                if (btnSave.Enabled)
                {
                    SHFS.Add("IsRead", "0");
                }
                else
                {
                    SHFS.Add("IsRead", "1");
                }
                SHFS.Add("patId", PatId);
                SHFS.Add("mzfs", cmbmzfs.Text);
                SHFS.Add("Mzsj", txtMzsj.Text);
                SHFS.Add("Yishi", cmbYishi.Text);
                SHFS.Add("Huxi", cmbHuxi.Text);
                SHFS.Add("Xunhuan", cmbXunhuan.Text);
                AddItem = "";
                if (Shzysx1.Checked) AddItem += "1";
                if (Shzysx2.Checked) AddItem += "2";
                if (Shzysx3.Checked) AddItem += "3";
                SHFS.Add("Shzysx", AddItem);
                SHFS.Add("Qt", txtQt.Text);
                SHFS.Add("mzxgbfz", cmbmzxgbfz.Text);
                SHFS.Add("Ms", txtMs.Text);
                SHFS.Add("Cl", txtcl.Text);
                SHFS.Add("jg", txtjg.Text);
                SHFS.Add("Mzys", cmbMzys.Text);
                SHFS.Add("VisitDate", Convert.ToDateTime(dtVisitDate.Text.ToString()).ToString("yyyy-MM-dd"));
                SHFS.Add("Odate", Convert.ToDateTime(Odate).ToString("yyyy-MM-dd"));
                DataTable dt = _AfterVisitDal.GetAfterVisit_YS_byMzjldid(MzjldId);
                if (dt.Rows.Count > 0)
                    result = _AfterVisitDal.UpdateAfterVisit_HQ_YS(SHFS);
                else
                    result = _AfterVisitDal.InsertAfterVisit_HQ_YS(SHFS);
                if (result > 0)
                {
                    BCcount++; MessageBox.Show("保存成功！");
                }
                else MessageBox.Show("保存失败！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存异常，请检查网络！" + ex.ToString());
            }
        }
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
            DataTable dt = _AfterVisitDal.GetAfterVisit_YS_byMzjldid(MzjldId);
            if (dt.Rows.Count == 0)
                MessageBox.Show("需要先保存，才能存档！");
            else
            {
                result = _AfterVisitDal.UpdateAfterVisit_HQ_YS(MzjldId, 1);
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
            DataTable dt = _AfterVisitDal.GetAfterVisit_YS_byMzjldid(MzjldId);
            if (dt.Rows[0]["isRead"].ToString() == "1")
            {
                result = _AfterVisitDal.UpdateAfterVisit_HQ_YS(MzjldId, 0);
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
        /// 退出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeforeShfs_HQ_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isRead)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeShfs_HQ_FormClosing);
                this.Close();
            }
            else if (BCcount > 0)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeShfs_HQ_FormClosing);
                this.Close();
            }
            else
            {
                if (MessageBox.Show("保存退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Save();
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeShfs_HQ_FormClosing);
                    this.Close();
                }
                else
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeShfs_HQ_FormClosing);
                    this.Close();
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
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeShfs_HQ_FormClosing);
                this.Close();
            }
            else if (BCcount > 0)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeShfs_HQ_FormClosing);
                this.Close();
            }
            else
            {
                if (MessageBox.Show("保存退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Save();
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeShfs_HQ_FormClosing);
                    this.Close();
                }
                else
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeShfs_HQ_FormClosing);
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDY_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
            //printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            //printDocument1.DefaultPageSettings.PaperSize =
            //      new System.Drawing.Printing.PaperSize("K16", 740, 1020); 
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
            Font ptzt2 = new Font("黑体", 10, FontStyle.Bold);//加粗十号
            Font ptzt3 = new Font("新宋体", 14, FontStyle.Bold);//加粗14号
            int x = 80; int y = 40;
            string title1 = "天津红桥医院麻醉术后访视记录";
            e.Graphics.DrawString(title1, ptzt3, Brushes.Black, new Point(x + 150, y));
            y = y + 40;
            e.Graphics.DrawString("姓名：" + tbPatname.Text, ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("性别：" + tbPatsex.Text.Trim(), ptzt, Brushes.Black, new Point(x + 120, y));
            e.Graphics.DrawString("年龄：" + this.tbPatage.Text.Trim() + "岁", ptzt, Brushes.Black, new Point(x + 210, y));
            e.Graphics.DrawString("科别：" + this.tbKeshi.Text.Trim(), ptzt, Brushes.Black, new Point(x + 300, y));
            e.Graphics.DrawString("住院号：" + this.tbZhuyuanID.Text.Trim(), ptzt, Brushes.Black, new Point(x + 400, y));
            y = y + 30;
            e.Graphics.DrawLine(Pens.Black, x, y - 5, x + 590, y - 5);
            e.Graphics.DrawString("麻醉方法：" + cmbmzfs.Text, ptzt2, Brushes.Black, new Point(x + 10, y));
            y = y + 40;
            e.Graphics.DrawString("麻醉时间：" + txtMzsj.Text, ptzt2, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("min", ptzt2, Brushes.Black, new Point(x + 160, y));
            y = y + 40;
            e.Graphics.DrawString("离开手术室状态：", ptzt2, Brushes.Black, new Point(x + 10, y));
            y = y + 25;
            e.Graphics.DrawString("意识：", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("清醒 ", ptzt, Brushes.Black, x + 50, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 90, y, 12, 12);
            if (cmbYishi.Text == "清醒")
            {
                e.Graphics.DrawLine(pblue2, x + 90, y, x + 95, y + 12);
                e.Graphics.DrawLine(pblue2, x + 95, y + 12, x + 102, y - 1);
            }
            e.Graphics.DrawString("能唤醒 ", ptzt, Brushes.Black, x + 110, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 160, y, 12, 12);
            if (cmbYishi.Text == "能唤醒")
            {
                e.Graphics.DrawLine(pblue2, x + 160, y, x + 165, y + 12);
                e.Graphics.DrawLine(pblue2, x + 165, y + 12, x + 172, y - 1);
            }
            e.Graphics.DrawString("不能唤醒 ", ptzt, Brushes.Black, x + 180, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 12, 12);
            if (cmbYishi.Text == "不能唤醒")
            {
                e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 12);
                e.Graphics.DrawLine(pblue2, x + 245, y + 12, x + 252, y - 1);
            }
            e.Graphics.DrawString("昏迷 ", ptzt, Brushes.Black, x + 260, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 300, y, 12, 12);
            if (cmbYishi.Text == "昏迷")
            {
                e.Graphics.DrawLine(pblue2, x + 300, y, x + 305, y + 12);
                e.Graphics.DrawLine(pblue2, x + 305, y + 12, x + 312, y - 1);
            }
            y = y + 25;
            e.Graphics.DrawString("呼吸：", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("带气管插管", ptzt, Brushes.Black, x + 60, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 140, y, 12, 12);
            if (cmbHuxi.Text == "带气管插管")
            {
                e.Graphics.DrawLine(pblue2, x + 140, y, x + 145, y + 12);
                e.Graphics.DrawLine(pblue2, x + 145, y + 12, x + 152, y - 1);
            }
            e.Graphics.DrawString("拔管 ", ptzt, Brushes.Black, x + 160, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 200, y, 12, 12);
            if (cmbHuxi.Text == "拔管")
            {
                e.Graphics.DrawLine(pblue2, x + 200, y, x + 205, y + 12);
                e.Graphics.DrawLine(pblue2, x + 205, y + 12, x + 212, y - 1);
            }
            e.Graphics.DrawString("自主呼吸 ", ptzt, Brushes.Black, x + 220, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 280, y, 12, 12);
            if (cmbHuxi.Text == "自主呼吸")
            {
                e.Graphics.DrawLine(pblue2, x + 280, y, x + 285, y + 12);
                e.Graphics.DrawLine(pblue2, x + 285, y + 12, x + 292, y - 1);
            }
            e.Graphics.DrawString("辅助呼吸 ", ptzt, Brushes.Black, x + 300, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 12, 12);
            if (cmbHuxi.Text == "辅助呼吸")
            {
                e.Graphics.DrawLine(pblue2, x + 360, y, x + 365, y + 12);
                e.Graphics.DrawLine(pblue2, x + 365, y + 12, x + 372, y - 1);
            }
            y = y + 25;
            e.Graphics.DrawString("循环：", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("稳定 ", ptzt, Brushes.Black, x + 60, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 100, y, 12, 12);
            if (cmbXunhuan.Text == "稳定")
            {
                e.Graphics.DrawLine(pblue2, x + 100, y, x + 105, y + 12);
                e.Graphics.DrawLine(pblue2, x + 105, y + 12, x + 112, y - 1);
            }
            e.Graphics.DrawString("血管活性药物辅助", ptzt, Brushes.Black, x + 120, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 12, 12);
            if (cmbXunhuan.Text == "血管活性药物辅助")
            {
                e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 12);
                e.Graphics.DrawLine(pblue2, x + 245, y + 12, x + 252, y - 1);
            }
            y = y + 40;
            e.Graphics.DrawString("术后注意事项：", ptzt2, Brushes.Black, new Point(x + 10, y));
            y = y + 25;
            e.Graphics.DrawString("生命体征监测", ptzt, Brushes.Black, x + 20, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 110, y, 12, 12);
            if (Shzysx1.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 110, y, x + 115, y + 12);
                e.Graphics.DrawLine(pblue2, x + 115, y + 12, x + 122, y - 1);
            }
            e.Graphics.DrawString("保持呼吸道通畅 ", ptzt, Brushes.Black, x + 130, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 12, 12);
            if (Shzysx2.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 12);
                e.Graphics.DrawLine(pblue2, x + 245, y + 12, x + 252, y - 1);
            }
            e.Graphics.DrawString("椎管内麻醉术后去枕平卧", ptzt, Brushes.Black, x + 260, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 420, y, 12, 12);
            if (Shzysx3.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 420, y, x + 425, y + 12);
                e.Graphics.DrawLine(pblue2, x + 425, y + 12, x + 432, y - 1);
            }
            y = y + 25;
            e.Graphics.DrawString("其它  " + txtQt.Text, ptzt, Brushes.Black, x + 20, y);
            y = y + 40;
            e.Graphics.DrawLine(Pens.Black, x, y - 5, x + 590, y - 5);
            e.Graphics.DrawString("麻醉相关并发症：", ptzt2, Brushes.Black, x + 10, y);
            y = y + 40;
            e.Graphics.DrawString("无 ", ptzt, Brushes.Black, x + 20, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 50, y, 12, 12);
            if (cmbmzxgbfz.Text == "无")
            {
                e.Graphics.DrawLine(pblue2, x + 50, y, x + 55, y + 12);
                e.Graphics.DrawLine(pblue2, x + 55, y + 12, x + 62, y - 1);
            }
            e.Graphics.DrawString("有 ", ptzt, Brushes.Black, x + 70, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 100, y, 12, 12);
            if (cmbmzxgbfz.Text == "有")
            {
                e.Graphics.DrawLine(pblue2, x + 100, y, x + 105, y + 12);
                e.Graphics.DrawLine(pblue2, x + 105, y + 12, x + 112, y - 1);
            }
            y = y + 40;
            int BZYYSS = y;
            string strSS1 = "";
            int StrLengthSS = txtMs.Text.Trim().Length;
            int rowSS = StrLengthSS / 35;
            e.Graphics.DrawString("描述：", ptzt, Brushes.Black, x + 10, y);
            for (int i = 0; i <= rowSS; i++)//50个字符就换行
            {
                if (i < rowSS)
                    strSS1 = txtMs.Text.Replace("\r", " ").Replace("\n", " ").Trim().ToString().Substring(i * 35, 35); //从第i*50个开始，截取50个字符串
                else
                    strSS1 = txtMs.Text.Replace("\r", " ").Replace("\n", " ").Trim().ToString().Substring(i * 35);

                e.Graphics.DrawString(strSS1, ptzt, Brushes.Black, x + 60, BZYYSS);
                BZYYSS = BZYYSS + 25;
            }
            y = y + 180;
            int BZYYSS1 = y;
            string strSS11 = "";
            int StrLengthSS1 = txtcl.Text.Trim().Length;
            int rowSS1 = StrLengthSS1 / 35;
            e.Graphics.DrawString("处理：", ptzt, Brushes.Black, x + 10, y);
            for (int i = 0; i <= rowSS1; i++)//50个字符就换行
            {
                if (i < rowSS1)
                    strSS11 = txtcl.Text.Replace("\r", " ").Replace("\n", " ").Trim().ToString().Substring(i * 35, 35); //从第i*50个开始，截取50个字符串
                else
                    strSS11 = txtcl.Text.Replace("\r", " ").Replace("\n", " ").Trim().ToString().Substring(i * 35);

                e.Graphics.DrawString(strSS11, ptzt, Brushes.Black, x + 60, BZYYSS1);
                BZYYSS1 = BZYYSS1 + 25;
            }
            y = y + 160;
            int BZYYSS2 = y;
            string strSS12 = "";
            int StrLengthSS2 = txtjg.Text.Trim().Length;
            int rowSS2 = StrLengthSS2 / 35;
            e.Graphics.DrawString("结果：", ptzt, Brushes.Black, x + 10, y);
            for (int i = 0; i <= rowSS2; i++)//50个字符就换行
            {
                if (i < rowSS2)
                    strSS12 = txtjg.Text.Replace("\r", " ").Replace("\n", " ").Trim().ToString().Substring(i * 35, 35); //从第i*50个开始，截取50个字符串
                else
                    strSS12 = txtjg.Text.Replace("\r", " ").Replace("\n", " ").Trim().ToString().Substring(i * 35);

                e.Graphics.DrawString(strSS12, ptzt, Brushes.Black, x + 60, BZYYSS2);
                BZYYSS2 = BZYYSS2 + 25;
            }
            y = y + 110;
            e.Graphics.DrawLine(Pens.Black, x, y - 5, x + 590, y - 5);
            e.Graphics.DrawLine(Pens.Black, x, 110 - 5, x, y - 5);
            e.Graphics.DrawLine(Pens.Black, x + 590, 110 - 5, x + 590, y - 5);
            y = y + 20;
            e.Graphics.DrawString("麻醉医师：" + cmbMzys.Text, ptzt, Brushes.Black, x + 100, y);
            e.Graphics.DrawString("日期：" + dtVisitDate.Value.Date.ToString("yyyy年MM月dd日"), ptzt, Brushes.Black, x + 330, y);
        }

    }
}
