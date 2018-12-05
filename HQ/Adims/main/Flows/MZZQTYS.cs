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
    public partial class MZZQTYS : Form
    {
        DataDicDal _DataDicDal = new DataDicDal();
        OperScheduleDal _PaibanDal = new OperScheduleDal();
        MzzqtysDal _MzzqtysDal = new MzzqtysDal();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_DAL.PacuDal _PacuDal = new adims_DAL.PacuDal();
        string PatId, Odate;
        bool isRead = false;
        public MZZQTYS(string patid, string date)
        {
            PatId = patid;
            Odate = date;
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MZZQTYS_Load(object sender, EventArgs e)
        {
            try
            {
                Load_info();
                LodSQFS();
                LoadMazuiFangfa();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void LoadMazuiFangfa()
        {
            DataTable dtmzff = _DataDicDal.GetMazuiFangfaAll();//麻醉方法
            txtYsjy.Items.Clear();
            foreach (DataRow dr in dtmzff.Rows)
            {
                txtYsjy.Items.Add(dr["name"].ToString());
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
            txtHuanb.Text = dr1["pattmd"].ToString();
            txtNxss.Text = dr1["oname"].ToString();
            dtVisitDate.Text = Odate;
        }
        int BCcount = 0;
        /// <summary>
        /// 赋值
        /// </summary>
        private void LodSQFS()
        {
            DataTable dt = _MzzqtysDal.GetMZZQTYS_YS(PatId);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                txtYsjy.Text = dr["Ysjy"].ToString();
                txtHuanb.Text = dr["Huanb"].ToString();
                txtNxss.Text = dr["Nxss"].ToString();
                txtTsfx.Text = dr["Tsfx"].ToString();
                txtTsfx.Text = dr["Tsfx"].ToString();
                cmbMzff.Text = dr["Mzff"].ToString();
                txtHzqm1.Text = dr["Hzqm1"].ToString();
                dtHzdate1.Text = dr["Hzdate1"].ToString();
                cmbMzty.Text = dr["Mzty"].ToString();
                txtHzqm2.Text = dr["Hzqm2"].ToString();
                dtHzdate2.Text = dr["Hzdate2"].ToString();
                txtJsqm.Text = dr["Jsqm"].ToString();
                txtYhzgx.Text = dr["Yhzgx"].ToString();
                dtJsdate.Text = dr["Jsdate"].ToString();
                txtYsqm.Text = dr["Ysqm"].ToString();
                dtQmrq.Text = dr["Qmrq"].ToString();
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
                MZZQTY.Add("PatId", PatId);
                MZZQTY.Add("Ysjy", txtYsjy.Text);
                MZZQTY.Add("Huanb", txtHuanb.Text);
                MZZQTY.Add("Nxss", txtNxss.Text);
                MZZQTY.Add("Tsfx", txtTsfx.Text);
                MZZQTY.Add("Mzff", cmbMzff.Text);
                MZZQTY.Add("Hzqm1", txtHzqm1.Text);
                MZZQTY.Add("Hzdate1", Convert.ToDateTime(dtHzdate1.Value.ToString()).ToString("yyyy-MM-dd"));
                MZZQTY.Add("Mzty", cmbMzty.Text);
                MZZQTY.Add("Hzqm2", txtHzqm2.Text);
                MZZQTY.Add("Hzdate2", Convert.ToDateTime(dtHzdate2.Value.ToString()).ToString("yyyy-MM-dd"));
                MZZQTY.Add("Jsqm", txtJsqm.Text);
                MZZQTY.Add("Yhzgx", txtYhzgx.Text);
                MZZQTY.Add("Jsdate", Convert.ToDateTime(dtJsdate.Value.ToString()).ToString("yyyy-MM-dd"));
                MZZQTY.Add("Ysqm", txtYsqm.Text);
                MZZQTY.Add("Qmrq", Convert.ToDateTime(dtQmrq.Value.ToString()).ToString("yyyy-MM-dd"));
                MZZQTY.Add("VisitDate", Convert.ToDateTime(dtVisitDate.Value.ToString()).ToString("yyyy-MM-dd"));
                if (btnSave.Enabled)
                {
                    MZZQTY.Add("IsRead", "0");
                }
                else
                {
                    MZZQTY.Add("IsRead", "1");
                }
                //MZZQTY.Add("Odate", Convert.ToDateTime(Odate).ToString("yyyy-MM-dd"));
                DataTable dt = _MzzqtysDal.GetMZZQTYS_YS(PatId);
                if (dt.Rows.Count > 0)
                    result = _MzzqtysDal.UpdateMZZQTY_YS_HQ(MZZQTY);
                else
                    result = _MzzqtysDal.InsertMZZQTY_YS_HQ(MZZQTY);
                if (result > 0)
                {
                    BCcount++; MessageBox.Show("保存成功！");
                }
                else MessageBox.Show("保存失败！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存异常!" + ex.ToString());
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
            DataTable dt = _MzzqtysDal.GetMZZQTYS_YS(PatId);
            if (dt.Rows.Count == 0)
                MessageBox.Show("需要先保存，才能存档！");
            else
            {
                result = _MzzqtysDal.UpdateMZZQTY_HQ_YS(PatId, 1);
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
            DataTable dt = _MzzqtysDal.GetMZZQTYS_YS(PatId);
            if (dt.Rows[0]["isRead"].ToString() == "1")
            {
                result = _MzzqtysDal.UpdateMZZQTY_HQ_YS(PatId, 0);
                if (result > 0)
                {
                    MessageBox.Show("解锁成功！");
                    btnSave.Enabled = true;
                    isRead = false;
                    btnCD.Enabled = true;
                }
            }
        }

        private void MZZQTYS_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isRead)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.MZZQTYS_FormClosing);
                this.Close();
            }
            else if (BCcount > 0)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.MZZQTYS_FormClosing);
                this.Close();
            }
            else
            {
                if (MessageBox.Show("保存退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Save();
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.MZZQTYS_FormClosing);
                    this.Close();
                }
                else
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.MZZQTYS_FormClosing);
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
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.MZZQTYS_FormClosing);
                this.Close();
            }
            else if (BCcount > 0)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.MZZQTYS_FormClosing);
                this.Close();
            }
            else
            {
                if (MessageBox.Show("保存退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Save();
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.MZZQTYS_FormClosing);
                    this.Close();
                }
                else
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.MZZQTYS_FormClosing);
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
        }
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("K16", 740, 1020);
        }
        int pages = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.HasMorePages = true;
            pages++;
            Pen ptp = Pens.Black;//普通画笔
            Pen pblue2 = new Pen(Brushes.Blue);
            pblue2.Width = 2;
            Pen pblack = new Pen(Brushes.Black);
            Pen pblack2 = new Pen(Brushes.Black, 2);
            Font ptzt = new Font("新宋体", 10);//普通字体
            Font ptzt1 = new Font("宋体", 10);//普通字体
            Font ptzt2 = new Font("黑体", 10, FontStyle.Bold);//加粗十号
            Font ptzt3 = new Font("新宋体", 14, FontStyle.Bold);//加粗14号
            if (pages == 1)
            {
                #region 第一页

                int y = 50; int x = 90; int y1 = 0;
                string title1 = "天津红桥医院麻醉知情同意书";
                e.Graphics.DrawString(title1, ptzt3, Brushes.Black, new Point(x + 180, y));
                y = y + 40; int YY = y;
                e.Graphics.DrawLine(Pens.Black, x, y, x + 610, y);
                e.Graphics.DrawString("患者姓名 " + tbPatname.Text, ptzt, Brushes.Black, new Point(x + 10, y + 10));
                e.Graphics.DrawLine(Pens.Black, x + 150, y, x + 150, y + 30);
                e.Graphics.DrawString("性别 " + tbPatsex.Text.Trim(), ptzt, Brushes.Black, new Point(x + 160, y + 10));
                e.Graphics.DrawLine(Pens.Black, x + 240, y, x + 240, y + 30);
                e.Graphics.DrawString("年龄 " + this.tbPatage.Text.Trim() + "岁", ptzt, Brushes.Black, new Point(x + 250, y + 10));
                e.Graphics.DrawLine(Pens.Black, x + 340, y, x + 340, y + 30);
                e.Graphics.DrawString("科别 " + this.tbKeshi.Text.Trim(), ptzt, Brushes.Black, new Point(x + 350, y + 10));
                e.Graphics.DrawLine(Pens.Black, x + 480, y, x + 480, y + 30);
                e.Graphics.DrawString("住院号 " + this.tbZhuyuanID.Text.Trim(), ptzt, Brushes.Black, new Point(x + 490, y + 10));
                y = y + 30;
                e.Graphics.DrawLine(Pens.Black, x, y, x + 610, y);
                y = y + 8;
                e.Graphics.DrawString("麻醉介绍与医师建议：", ptzt2, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("1. 麻醉作用的产生主要是利用麻醉药使中枢神经系统或神经系统中某些部位受到抑制的结果。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("临床麻醉的主要任务是：消除手术疼痛，监测和调控生理功能，保护患者安全，并为手术创造条", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("件。手术是治疗外科疾病的有效方法，但手术引起的创伤和失血可使患者的生理功能处于应激状", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("态，都需要麻醉医师在整个手术过程为您监护与调控。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("2. 外科疾病本身所引起的疾病生理改变，以及并存的非外科疾病所导致的器官功能损害等，都", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 25;
                e.Graphics.DrawString("是围手术期潜在危险因素。麻醉的风险与手术大小并非完全一致，复杂的手术固然可使麻醉的风", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("险性增加，而有时手术并非很复杂，但由于患者的病情和并存疾病的影响，可为麻醉带来更大的", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("风险。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("3. 为了保证您手术时无痛和医疗安全，手术需要在麻醉和严密监测条件下进行。我已经详细阅", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("读了您的病历及各项检查结果并给您进一步进行了体格检查，通过我们的检查和全面评估并结合", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20; y1 = y + 15;
                e.Graphics.DrawString("手术方式对麻醉的要求，", ptzt1, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawString("我建议您在", ptzt2, Brushes.Black, new Point(x + 160, y));
                e.Graphics.DrawString(txtYsjy.Text, ptzt1, Brushes.Black, new Point(x + 235, y));
                e.Graphics.DrawLine(Pens.Black, x + 235, y1, x + 410, y1);
                e.Graphics.DrawString("麻醉下完成本次手术治疗。", ptzt2, Brushes.Black, new Point(x + 415, y));
                y = y + 20;
                e.Graphics.DrawString("4. 手术后镇痛：为了减轻您的术后疼痛，促进康复，我们向您介绍了术后疼痛治疗的优点、方", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("法可能引起的意外与并发症，建议您进行术后疼痛治疗，但该项目属自选项目。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("5. 为了您手术和麻醉的安全，我们将严格遵循麻醉操作规范和用药原则，在您手术麻醉期间，", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("我们始终在现场严密监测您的生命体征，并履行医师职责，对异常情况及时进行治疗和处理。但", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("任何麻醉方法都存在一定风险性。因受医学科学技术条件限制目前尚难以完全避免发生一些医疗", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("意外或并发症。如合并其它疾病，麻醉可诱发或加重已有症状，相关并发症和麻醉风险性也显著", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("增加。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawLine(Pens.Black, x, y, x + 610, y);
                y = y + 8;
                e.Graphics.DrawString("麻醉潜在风险和对策", ptzt2, Brushes.Black, new Point(x + 10, y));
                y = y + 20; y1 = y + 15;
                string str1_zd = "";
                int StrLength_zd = txtHuanb.Text.Trim().Length;
                int row_zd = StrLength_zd / 35;
                e.Graphics.DrawString("您患有 ", ptzt1, Brushes.Black, new Point(x + 40, y));
                for (int i = 0; i <= row_zd;)//50个字符就换行
                {
                    if (i < row_zd)
                    {
                        str1_zd = txtHuanb.Text.ToString().Substring(i * 35, 35); //从第i*50个开始，截取50个字符串
                    }
                    else
                    {
                        str1_zd = txtHuanb.Text.ToString().Substring(i * 35);
                    }
                    if (i == 0)
                    {
                        e.Graphics.DrawString(str1_zd, ptzt, Brushes.Black, x + 90, y);
                        e.Graphics.DrawLine(Pens.Black, x + 80, y1, x + 600, y1);
                    }
                    else
                    {
                        e.Graphics.DrawString(str1_zd, ptzt, Brushes.Black, x + 15, y);
                        e.Graphics.DrawLine(Pens.Black, x + 10, y1, x + 600, y1);
                    }
                    y = y + 20; y1 = y + 15;
                    i++;
                    //if (i > row_zd)
                    //{

                    //}
                    //else
                    //{
                    //    y = y + 15;
                    //}

                }

                //y = y + 20; y1 = y + 15;
                e.Graphics.DrawString("拟行", ptzt1, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawString(txtNxss.Text, ptzt1, Brushes.Black, new Point(x + 55, y));
                e.Graphics.DrawLine(Pens.Black, x + 50, y1, x + 460, y1);
                e.Graphics.DrawString("手术，需要接受麻醉。", ptzt1, Brushes.Black, new Point(x + 465, y));
                y = y + 20;
                e.Graphics.DrawString("因受医学科学技术条件限制，并目前尚难以完成完全避免以下麻醉意外和并发症等风险：", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("1.与原发病或并存疾病相关：脑出血，脑梗塞，脑水肿；严重心律失常，心肌缺血/梗死，心力衰", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("竭；肺不张，肺水肿，肺栓塞，呼吸衰竭；肾功能障碍或衰竭等。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("2.与药物相关：过敏反应或过敏性休克，局麻药全身毒性反应和神经毒性，严重呼吸和循环抑", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("制，循环骤停，器官功能损害或衰竭，精神异常，恶性高热等。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("3.与不同麻醉方法相关：", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("（1）神经阻滞（颈丛、臂丛、腰丛、闭孔神经）：血肿，气胸，神经功能损害，喉返神经麻醉", ptzt1, Brushes.Black, new Point(x + 15, y));
                y = y + 20;
                e.Graphics.DrawString("痹，全脊麻等。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("（2）椎管内麻醉（腰麻、硬膜外麻醉）：腰背痛，尿失禁或尿潴留，腰麻后头痛，颅神经麻痹，", ptzt1, Brushes.Black, new Point(x + 15, y));
                y = y + 20;
                e.Graphics.DrawString("脊神经或脊髓损伤，呼吸和循环抑制，全脊麻醉甚至循环骤停，硬膜外血肿、脓肿甚至截瘫，", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("穿刺部位或椎管内感染，硬膜外导管滞留或断裂，麻醉不完善或失败等。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("（3）全身麻醉：呕吐、误吸，喉痉挛，急性上呼吸道梗阻，气管内插管失败，术后咽痛，声带", ptzt1, Brushes.Black, new Point(x + 15, y));
                y = y + 20;
                e.Graphics.DrawString("损伤环灼关节脱位，牙齿损伤或脱落，苏醒延迟等。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("（4）超浅麻醉（中、深度静脉/镇痛）：呕吐、误吸，喉痉挛，支气管痉挛，急性上呼吸道梗", ptzt1, Brushes.Black, new Point(x + 15, y));
                y = y + 20;
                e.Graphics.DrawString("阻，呼吸抑制，苏醒延迟等。", ptzt1, Brushes.Black, new Point(x + 10, y));

                y = y + 20;
                e.Graphics.DrawLine(Pens.Black, x, y, x + 610, y);
                e.Graphics.DrawLine(Pens.Black, x, 90, x, y);
                e.Graphics.DrawLine(Pens.Black, x + 610, 90, x + 610, y);
                #endregion
            }
            else if (pages == 2)
            {
                int y = 90; int x = 90; int y1 = 0;
                e.Graphics.DrawLine(Pens.Black, x, y, x + 610, y);
                y = y + 8;
                //y = y + 20;
                e.Graphics.DrawString("4.与有创伤性监测相关：局部血肿，纵膈血/气肿，血/气胸，感染，心率失常，血栓形成或肺栓", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("塞，心包填塞，导管打结或断裂，胸导管损伤，神经损伤等。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("5.与输液、输血及血液制品相关：血源性传染病，热源反应，过敏反应，凝血病等。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("6.与手术相关：失血性休克，严重迷走神经反射引起的呼吸心跳骤停，压迫心脏或大血管引起的", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("严重循环抑制及其并发症等。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("7.与急诊手术相关：以上医疗意外和并发症均可发生于急诊手术病人，且发生率较择期手术明显", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("升高。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                //e.Graphics.DrawString("8.与术后镇痛相关：呼吸、循环抑制，恶心呕吐，镇痛不全，硬膜外导管脱出等。", ptzt1, Brushes.Black, new Point(x + 10, y));
                //y = y + 20;
                e.Graphics.DrawString("特殊风险或主要高危因素：", ptzt2, Brushes.Black, new Point(x + 10, y));
                y = y + 25; y1 = y + 15;
                int BZYYSS = y;
                string strSS1 = "";
                int StrLengthSS = txtTsfx.Text.Trim().Length;
                int rowSS = StrLengthSS / 40;
                for (int i = 0; i <= rowSS; i++)//40个字符就换行
                {
                    if (i < rowSS)
                        strSS1 = txtTsfx.Text.Trim().ToString().Substring(i * 40, 40); //从第i*40个开始，截取40个字符串
                    else
                        strSS1 = txtTsfx.Text.Trim().ToString().Substring(i * 40);

                    e.Graphics.DrawString(strSS1, ptzt, Brushes.Black, x + 10, BZYYSS);
                    BZYYSS = BZYYSS + 25;
                }
                e.Graphics.DrawLine(Pens.Black, x + 10, y1, x + 590, y1);
                y = y + 25; y1 = y + 15;
                e.Graphics.DrawLine(Pens.Black, x + 10, y1, x + 590, y1);
                y = y + 25; y1 = y + 15;
                e.Graphics.DrawLine(Pens.Black, x + 10, y1, x + 590, y1);
                y = y + 25;
                e.Graphics.DrawString("一旦发生上述风险和意外，麻醉医师会采取积极应对措施。", ptzt2, Brushes.Black, new Point(x + 10, y));
                y = y + 25;
                e.Graphics.DrawLine(Pens.Black, x, y, x + 610, y);
                y = y + 8;
                e.Graphics.DrawString("患者知情选择", ptzt2, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("●  为了维护我的知情选择权，在保证手术方式对麻醉需求和安全的前提下，", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("我选择以下麻醉方法：", ptzt2, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawRectangle(Pens.Black, x + 20, y, 12, 12);
                e.Graphics.DrawString("全身麻醉；", ptzt1, Brushes.Black, x + 40, y);
                if (cmbMzff.Text == "全身麻醉")
                {
                    e.Graphics.DrawLine(pblue2, x + 15, y, x + 25, y + 12);
                    e.Graphics.DrawLine(pblue2, x + 25, y + 12, x + 40, y - 3);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 120, y, 12, 12);
                e.Graphics.DrawString("全麻+硬膜外麻醉；", ptzt1, Brushes.Black, x + 140, y);
                if (cmbMzff.Text == "全麻+硬膜外麻醉")
                {
                    e.Graphics.DrawLine(pblue2, x + 115, y, x + 125, y + 12);
                    e.Graphics.DrawLine(pblue2, x + 125, y + 12, x + 140, y - 3);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 270, y, 12, 12);
                e.Graphics.DrawString("椎管内麻醉；", ptzt1, Brushes.Black, x + 290, y);
                if (cmbMzff.Text == "椎管内麻醉")
                {
                    e.Graphics.DrawLine(pblue2, x + 265, y, x + 275, y + 12);
                    e.Graphics.DrawLine(pblue2, x + 275, y + 12, x + 290, y - 3);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 380, y, 12, 12);
                e.Graphics.DrawString("神经阻滞；", ptzt1, Brushes.Black, x + 400, y);
                if (cmbMzff.Text == "神经阻滞")
                {
                    e.Graphics.DrawLine(pblue2, x + 375, y, x + 385, y + 12);
                    e.Graphics.DrawLine(pblue2, x + 385, y + 12, x + 400, y - 3);
                }
                e.Graphics.DrawRectangle(Pens.Black, x + 480, y, 12, 12);
                e.Graphics.DrawString("超浅麻醉；", ptzt1, Brushes.Black, x + 500, y);
                if (cmbMzff.Text == "超浅麻醉")
                {
                    e.Graphics.DrawLine(pblue2, x + 475, y, x + 485, y + 12);
                    e.Graphics.DrawLine(pblue2, x + 485, y + 12, x + 500, y - 3);
                }
                y = y + 20;
                e.Graphics.DrawString("我们会尊重您的选择", ptzt1, Brushes.Black, new Point(x + 20, y));
                y = y + 20;
                e.Graphics.DrawString("●  麻醉医师已经告诉我将要施行的麻醉及麻醉后可能发生的并发症和风险、可能存在的其它麻", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("醉方法并且解答了我关于此次麻醉的相关问题。我对麻醉医师所告知的“因受医学科学技术条件", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("限制，目前尚难以完全避免上述麻醉意外和并发症“表示理解并同意此次麻醉。我相信麻醉医师", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("会给我采取积极有效措施加以避免。", ptzt1, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawString("如果发生紧急情况，医师无法或来不及证得本人或家属意", ptzt2, Brushes.Black, new Point(x + 230, y));
                y = y + 20;
                e.Graphics.DrawString("见时，我授权麻醉医师按照医学常规予以紧急处理和全力救治。", ptzt2, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("●  我同意在治疗中医生可以根据我的病情对预定的麻醉方式做出调整。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("●  我理解在我的麻醉期间需要多位医生共同进行。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("●  我并未得到治疗百分之百无风险的许诺。", ptzt1, Brushes.Black, new Point(x + 10, y));
                //y = y + 40; y1 = y + 15;
                //e.Graphics.DrawString("患者签名    " + txtHzqm1.Text, ptzt1, Brushes.Black, new Point(x + 10, y));
                //e.Graphics.DrawLine(Pens.Black, x+70, y1, x + 250, y1);
                //e.Graphics.DrawString("签名日期  " + dtHzdate1.Value.Date.ToString("yyyy年MM月dd日"), ptzt1, Brushes.Black, new Point(x + 380, y));
                //e.Graphics.DrawLine(Pens.Black, x + 435, y1, x + 570, y1);
                //y = y + 25; y1 = y + 15;
                //e.Graphics.DrawString("我很清楚术后镇痛的好处与相关不良反应。", ptzt1, Brushes.Black, new Point(x + 10, y));
                //e.Graphics.DrawString("我", ptzt2, Brushes.Black, new Point(x + 280, y));
                //e.Graphics.DrawString(cmbMzty.Text, ptzt1, Brushes.Black, new Point(x + 295, y));
                //e.Graphics.DrawLine(Pens.Black, x + 295, y1, x + 340, y1);
                //e.Graphics.DrawString("（患者或被授权者写同意或不同意）术后", ptzt2, Brushes.Black, new Point(x + 335, y));
                //y = y + 25;
                //e.Graphics.DrawString("镇痛。", ptzt2, Brushes.Black, new Point(x + 10, y));
                y = y + 50; y1 = y + 15;
                e.Graphics.DrawString("患者签名    " + txtHzqm2.Text, ptzt1, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawLine(Pens.Black, x + 70, y1, x + 250, y1);
                e.Graphics.DrawString("签名日期  " + dtHzdate2.Value.Date.ToString("yyyy年MM月dd日"), ptzt1, Brushes.Black, new Point(x + 380, y));
                e.Graphics.DrawLine(Pens.Black, x + 435, y1, x + 570, y1);
                y = y + 40; y1 = y + 15;
                e.Graphics.DrawString("被授权人签名  " + txtJsqm.Text, ptzt1, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawLine(Pens.Black, x + 100, y1, x + 180, y1);
                e.Graphics.DrawString("与患者关系  " + txtYhzgx.Text, ptzt1, Brushes.Black, new Point(x + 190, y));
                e.Graphics.DrawLine(Pens.Black, x + 270, y1, x + 350, y1);
                e.Graphics.DrawString("签名日期  " + dtJsdate.Value.Date.ToString("yyyy年MM月dd日"), ptzt1, Brushes.Black, new Point(x + 380, y));
                e.Graphics.DrawLine(Pens.Black, x + 435, y1, x + 570, y1);
                y = y + 25;
                e.Graphics.DrawLine(Pens.Black, x, y, x + 610, y);
                y = y + 8;
                e.Graphics.DrawString("医生陈述", ptzt2, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("我已经告知患者将要施行的麻醉方式、此次麻醉及麻醉后可能发生的并发症和风险、根据", ptzt1, Brushes.Black, new Point(x + 40, y));
                y = y + 20;
                e.Graphics.DrawString("手术治疗的需要更改为其他麻醉方式的可能性，并且解答了患者关于此次麻醉的相关问题及采取", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString("积极有效措施加以避免并发症。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 40; y1 = y + 15;
                e.Graphics.DrawString("医生签名    " + txtYsqm.Text, ptzt1, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawLine(Pens.Black, x + 70, y1, x + 250, y1);
                e.Graphics.DrawString("签名日期  " + dtQmrq.Value.Date.ToString("yyyy年MM月dd日"), ptzt1, Brushes.Black, new Point(x + 380, y));
                e.Graphics.DrawLine(Pens.Black, x + 435, y1, x + 570, y1);
                y = y + 20;
                e.Graphics.DrawLine(Pens.Black, x, y, x + 610, y);
                e.Graphics.DrawLine(Pens.Black, x, 90, x, y);
                e.Graphics.DrawLine(Pens.Black, x + 610, 90, x + 610, y);

                pages = 0;
                e.HasMorePages = false; //关掉多页打印属性     
            }
        }

    }
}
