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
    public partial class ShuXuePG : Form
    {
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        adims_DAL.PACU_DAL pdal = new adims_DAL.PACU_DAL();
        string PGID, MZID;
        public ShuXuePG( string mzid,string patid)
        {
            PGID = patid;
            MZID = mzid;
            InitializeComponent();
        }
        public ShuXuePG()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShuXuePG_Load(object sender, EventArgs e)
        {
            cmbYiShiQM.DataSource = bll.GetMZYS(1);
            cmbYiShiQM.DisplayMember = "user_name";
            BindPatInfo();
           // BindPatMZInfo();
            bindShuXueinfo();

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
        /// <summary>
        /// 保存患者的基本信息
        /// </summary>
        private void BindPatInfo()
        {
            DataTable dt = new DataTable();
            dt = DAL.GetALLPAIBAN(PGID);
            if (dt.Rows.Count > 0)
            {
                tbZhuyuanNo.Text = dt.Rows[0]["PatZhuYuanid"].ToString();
                tbPatname.Text = dt.Rows[0]["Patname"].ToString();
                tbBingqu.Text = dt.Rows[0]["Patdpm"].ToString();
                tbSex.Text = dt.Rows[0]["Patsex"].ToString();
                tbAge.Text = dt.Rows[0]["Patage"].ToString();
                //tbBedNO.Text = dt.Rows[0]["Patbedno"].ToString();
                tbSSMC.Text = dt.Rows[0]["Oname"].ToString();
                tbSZZD.Text = dt.Rows[0]["Pattmd"].ToString();
            }

        }
        /// <summary>
        /// 查询手术方式和诊断
        /// </summary>
        private void BindPatMZInfo()
        {
            DataTable dt = new DataTable();
            dt = DAL.GetMZJLD_Info(MZID);
            if (dt.Rows.Count > 0)
            {
                //dtDate.Value = Convert.ToDateTime(dt.Rows[0]["otime"]);
                //tbMZFF.Text = dt.Rows[0]["mazuifs"].ToString();
                tbSSMC.Text = dt.Rows[0]["shoushufs"].ToString();
                tbSZZD.Text = dt.Rows[0]["sqzd"].ToString();
            }
        }
        /// <summary>
        /// 窗体加载时读取信息
        /// </summary>
        private void bindShuXueinfo()
        {
            try
            {
                DataTable dt = bll.ShuXue_info(MZID);
                if (dt.Rows.Count != 0)
                {
                    txtHXB.Text = dt.Rows[0]["HXB"].ToString();
                    txtXJ.Text = dt.Rows[0]["XJ"].ToString();
                    txtXXB.Text = dt.Rows[0]["XXB"].ToString();
                    txtLCD.Text = dt.Rows[0]["LCD"].ToString();
                    cmbSXMD.Text = dt.Rows[0]["SXMD"].ToString();
                    cmbPFZMYS1.Text = dt.Rows[0]["PFZMYS1"].ToString();
                    cmbPFZMYS2.Text = dt.Rows[0]["PFZMYS2"].ToString();
                    cmbMSXH1.Text = dt.Rows[0]["MSXH1"].ToString();
                    cmbMSXH2.Text = dt.Rows[0]["MSXH2"].ToString();
                    txtXueYa1.Text = dt.Rows[0]["XueYa1"].ToString();
                    txtXueYa2.Text = dt.Rows[0]["XueYa2"].ToString();
                    txtXinLV1.Text = dt.Rows[0]["XinLV1"].ToString();
                    txtXinLV2.Text = dt.Rows[0]["XinLV2"].ToString();
                    txtSPO21.Text = dt.Rows[0]["SPO21"].ToString();
                    txtSPO22.Text = dt.Rows[0]["SPO22"].ToString();
                    txtHb1.Text = dt.Rows[0]["Hb1"].ToString();
                    txtHct1.Text = dt.Rows[0]["Hct1"].ToString();
                    txtHb2.Text = dt.Rows[0]["Hb2"].ToString();
                    txtHct2.Text = dt.Rows[0]["Hct2"].ToString();
                    txtCVP1.Text = dt.Rows[0]["CVP1"].ToString();
                    txtCVP2.Text = dt.Rows[0]["CVP2"].ToString();
                    txtNL1.Text = dt.Rows[0]["NL1"].ToString();
                    txtNL2.Text = dt.Rows[0]["NL2"].ToString();
                    txtCXL1.Text = dt.Rows[0]["CXL1"].ToString();
                    txtCXL2.Text = dt.Rows[0]["CXL2"].ToString();
                    cmbMCSX1.Text = dt.Rows[0]["MCSX1"].ToString();
                    cmbMCSX2.Text = dt.Rows[0]["MCSX2"].ToString();
                    cmbSXFY.Text = dt.Rows[0]["SXFY"].ToString();
                    txtShuXueCL.Text = dt.Rows[0]["ShuXueCL"].ToString();
                    cmbZHPJ.Text = dt.Rows[0]["ZHPJ"].ToString();
                    cmbYiShiQM.Text = dt.Rows[0]["YiShiQM"].ToString();
                    DTPGTime.Text = Convert.ToDateTime(dt.Rows[0]["PGTime"]).ToString();
                    if (Convert.ToInt32(dt.Rows[0]["IsRead"]) == 1)
                    {
                        btnSave.Enabled = false;
                    }
                }
               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 保存输血评估信息
        /// </summary>
        private void SaveBind()
        {
            try
            {
                Dictionary<string, string> ShuXuePG = new Dictionary<string, string>();
                ShuXuePG.Add("mzjldid", MZID);
                ShuXuePG.Add("patid", PGID);
                ShuXuePG.Add("HXB", txtHXB.Text.Trim());
                ShuXuePG.Add("XJ", txtXJ.Text.Trim());
                ShuXuePG.Add("XXB", txtXXB.Text.Trim());
                ShuXuePG.Add("LCD", txtLCD.Text.Trim());
                ShuXuePG.Add("SXMD", cmbSXMD.Text.Trim());
                ShuXuePG.Add("PFZMYS1", cmbPFZMYS1.Text.Trim());
                ShuXuePG.Add("PFZMYS2", cmbPFZMYS2.Text.Trim());
                ShuXuePG.Add("MSXH1", cmbMSXH1.Text.Trim());
                ShuXuePG.Add("MSXH2", cmbMSXH2.Text.Trim());
                ShuXuePG.Add("XueYa1", txtXueYa1.Text.Trim());
                ShuXuePG.Add("XueYa2", txtXueYa2.Text.Trim());
                ShuXuePG.Add("XinLV1", txtXinLV1.Text.Trim());
                ShuXuePG.Add("XinLV2", txtXinLV2.Text.Trim());
                ShuXuePG.Add("SPO21", txtSPO21.Text.Trim());
                ShuXuePG.Add("SPO22", txtSPO22.Text.Trim());
                ShuXuePG.Add("Hb1", txtHb1.Text.Trim());
                ShuXuePG.Add("Hct1", txtHct1.Text.Trim());
                ShuXuePG.Add("Hb2", txtHb2.Text.Trim());
                ShuXuePG.Add("Hct2", txtHct2.Text.Trim());
                ShuXuePG.Add("CVP1", txtCVP1.Text.Trim());
                ShuXuePG.Add("CVP2", txtCVP2.Text.Trim());
                ShuXuePG.Add("NL1", txtNL1.Text.Trim());
                ShuXuePG.Add("NL2", txtNL2.Text.Trim());
                ShuXuePG.Add("CXL1", txtCXL1.Text.Trim());
                ShuXuePG.Add("CXL2", txtCXL2.Text.Trim());
                ShuXuePG.Add("MCSX1", cmbMCSX1.Text.Trim());
                ShuXuePG.Add("MCSX2", cmbMCSX2.Text.Trim());
                ShuXuePG.Add("SXFY", cmbSXFY.Text.Trim());
                ShuXuePG.Add("ShuXueCL", txtShuXueCL.Text.Trim());
                ShuXuePG.Add("ZHPJ", cmbZHPJ.Text.Trim());
                ShuXuePG.Add("YiShiQM", cmbYiShiQM.Text.Trim());
                ShuXuePG.Add("PGTime",  Convert.ToDateTime(DTPGTime.Text.Trim()).ToString());
                ShuXuePG.Add("AddTime", DateTime.Now.ToString());
                ShuXuePG.Add("IsRead", "0");
                int flag = 0;
                if (bll.ShuXue_info(MZID).Rows.Count == 0)
                    flag = bll.InsertShuXue(ShuXuePG);
                else
                    flag = bll.UpdateShuXue(ShuXuePG);
                if (flag == 0)
                {
                    MessageBox.Show("保存失败");
                }
                else
                    MessageBox.Show("保存成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveBind();
        }
        /// <summary>
        /// 存档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLock_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = bll.ShuXue_info(MZID);
            if (dt.Rows.Count > 0)
            {
                result = bll.UpdateShuXue_State(MZID, 1);
                if (result > 0)
                {
                    MessageBox.Show("存档成功！");
                    btnSave.Enabled = false;
                }
            }
        }
        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnlock_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = bll.ShuXue_info(MZID);
            if (dt.Rows.Count > 0)
            {
                result = bll.UpdateShuXue_State(MZID, 0);
                if (result > 0)
                {
                    MessageBox.Show("解锁成功！");
                    btnSave.Enabled = true;
                }
            }
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
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
            Font zt9 = new Font(new FontFamily("宋体"), 10);
            Font ht13 = new Font(new FontFamily("黑体"), 13);
            //Font ht9 = new Font(new FontFamily("黑体"), 9);
            Pen ptp = new Pen(Brushes.Black);
            Pen pb2 = new Pen(Brushes.Black, 2);
            int x = 50, y = 50, y1 = y + 15;
            e.Graphics.DrawString("昌吉州人名医院术中输血评估表", ht13, Brushes.Black, x + 200, y);
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x+720, y));//画横线
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y+50));
            e.Graphics.DrawString("科", zt9, Brushes.Black, x + 10, y+10);
            e.Graphics.DrawString("室", zt9, Brushes.Black, x + 10, y+30);
            e.Graphics.DrawLine(ptp, new Point(x+40, y), new Point(x+40, y + 50));
            e.Graphics.DrawString(tbBingqu.Text, zt9, Brushes.Black, x + 50, y + 20);
            e.Graphics.DrawLine(ptp, new Point(x + 150, y), new Point(x + 150, y + 50));
            e.Graphics.DrawString("姓", zt9, Brushes.Black, x + 160, y + 10);
            e.Graphics.DrawString("名", zt9, Brushes.Black, x + 160, y + 30);
            e.Graphics.DrawLine(ptp, new Point(x + 180, y), new Point(x + 180, y + 50));
            e.Graphics.DrawString(tbPatname.Text, zt9, Brushes.Black, x + 190, y + 20);
            e.Graphics.DrawLine(ptp, new Point(x + 340, y), new Point(x + 340, y + 50));
            e.Graphics.DrawString("性", zt9, Brushes.Black, x + 350, y + 10);
            e.Graphics.DrawString("别", zt9, Brushes.Black, x + 350, y + 30);
            e.Graphics.DrawLine(ptp, new Point(x + 370, y), new Point(x + 370, y + 50));
            e.Graphics.DrawString("男", zt9, Brushes.Black, x + 380, y + 10);
            e.Graphics.DrawRectangle(Pens.Black, x + 400, y + 10, 12, 12);
            if (tbSex.Text == "男")
            {
                e.Graphics.DrawLine(pb2, x + 395, y + 10, x + 405, y + 10 + 12);
                e.Graphics.DrawLine(pb2, x + 405, y + 10 + 12, x + 420, y + 10 - 3);
            }
            e.Graphics.DrawString("女", zt9, Brushes.Black, x + 380, y + 30);
            e.Graphics.DrawRectangle(Pens.Black, x + 400, y + 30, 12, 12);
            if (tbSex.Text == "女")
            {
                e.Graphics.DrawLine(pb2, x + 395, y + 30, x + 405, y + 30 + 12);
                e.Graphics.DrawLine(pb2, x + 405, y + 30 + 12, x + 420, y + 30 - 3);
            }
            e.Graphics.DrawLine(ptp, new Point(x + 440, y), new Point(x + 440, y + 50));
            e.Graphics.DrawString("年", zt9, Brushes.Black, x + 450, y + 10);
            e.Graphics.DrawString("龄", zt9, Brushes.Black, x + 450, y + 30);
            e.Graphics.DrawLine(ptp, new Point(x + 470, y), new Point(x + 470, y + 50));
            e.Graphics.DrawString(tbAge.Text+" 岁", zt9, Brushes.Black, x + 480, y + 20);
            e.Graphics.DrawLine(ptp, new Point(x + 520, y), new Point(x + 520, y + 50));
            e.Graphics.DrawString("病案号", zt9, Brushes.Black, x + 530, y + 20);
            e.Graphics.DrawLine(ptp, new Point(x + 590, y), new Point(x + 590, y + 50));
            e.Graphics.DrawString(tbZhuyuanNo.Text, zt9, Brushes.Black, x + 600, y + 20);
            e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 50));
            y = y + 50; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 40));
            e.Graphics.DrawString("诊断", zt9, Brushes.Black, x + 10, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 50, y), new Point(x + 50, y + 40));
            e.Graphics.DrawString(tbSZZD.Text, zt9, Brushes.Black, x + 60, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 380, y), new Point(x + 380, y + 40));
            e.Graphics.DrawString("手术方式", zt9, Brushes.Black, x + 390, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 480, y), new Point(x + 480, y + 40));
            e.Graphics.DrawString(tbSSMC.Text, zt9, Brushes.Black, x + 490, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 40));
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 40));
            e.Graphics.DrawString("需血量", zt9, Brushes.Black, x + 10, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 80, y), new Point(x + 80, y + 40));
            e.Graphics.DrawString("红细胞  " + txtHXB.Text, zt9, Brushes.Black, x + 90, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 135, y1 + 15), new Point(x + 180, y1 + 15));
            e.Graphics.DrawString("u", zt9, Brushes.Black, x + 180, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 200, y), new Point(x + 200, y + 40));
            e.Graphics.DrawString("血浆  " +txtXJ.Text, zt9, Brushes.Black, x + 210, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 240, y1 + 15), new Point(x + 285, y1 + 15));
            e.Graphics.DrawString("ml", zt9, Brushes.Black, x + 290, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 310, y), new Point(x + 310, y + 40));
            e.Graphics.DrawString("血小板  " +txtXXB.Text, zt9, Brushes.Black, x + 320, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 365, y1 + 15), new Point(x + 410, y1 + 15));
            e.Graphics.DrawString("u", zt9, Brushes.Black, x + 410, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 480, y), new Point(x + 480, y + 40));
            e.Graphics.DrawString("冷沉淀  " + txtLCD.Text, zt9, Brushes.Black, x + 490, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 535, y1 + 15), new Point(x + 580, y1 + 15));
            e.Graphics.DrawString("u", zt9, Brushes.Black, x + 580, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 40));
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 40));
            e.Graphics.DrawString("输血目的", zt9, Brushes.Black, x + 10, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 150, y), new Point(x + 150, y + 40));
            e.Graphics.DrawString("纠正贫血", zt9, Brushes.Black, x + 230, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x +340, y + 15, 12, 12);
            if (cmbSXMD.Text == "纠正贫血")
            {
                e.Graphics.DrawLine(pb2, x + 335, y + 15, x + 345, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 345, y + 15 + 12, x + 360, y + 15 - 3);
            }
            e.Graphics.DrawLine(ptp, new Point(x + 480, y), new Point(x + 480, y + 40));
            e.Graphics.DrawString("纠正凝血功能", zt9, Brushes.Black, x + 530, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x + 650, y + 15, 12, 12);
            if (cmbSXMD.Text == "纠正凝血功能")
            {
                e.Graphics.DrawLine(pb2, x + 645, y + 15, x + 655, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 655, y + 15 + 12, x + 670, y + 15 - 3);
            }
            e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 40));
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 40));
            e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 40));
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 40));
            e.Graphics.DrawString("评估指标", zt9, Brushes.Black, x + 30, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 150, y), new Point(x + 150, y + 40));
            e.Graphics.DrawString("输血前评估", zt9, Brushes.Black, x + 250, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 460, y), new Point(x + 460, y + 40));
            e.Graphics.DrawString("输血后评估", zt9, Brushes.Black, x + 550, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 40));
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 40));
            e.Graphics.DrawString("皮肤粘膜颜色", zt9, Brushes.Black, x + 20, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 150, y), new Point(x + 150, y + 40));
            e.Graphics.DrawString("苍白", zt9, Brushes.Black, x + 190, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y + 15, 12, 12);
            if (cmbPFZMYS1.Text == "苍白")
            {
                e.Graphics.DrawLine(pb2, x + 235, y + 15, x + 245, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 245, y + 15 + 12, x + 260, y + 15 - 3);
            }
            e.Graphics.DrawString("红润", zt9, Brushes.Black, x + 300, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x + 350, y + 15, 12, 12);
            if (cmbPFZMYS1.Text == "红润")
            {
                e.Graphics.DrawLine(pb2, x + 345, y + 15, x + 355, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 355, y + 15 + 12, x + 370, y + 15 - 3);
            }
            e.Graphics.DrawLine(ptp, new Point(x + 460, y), new Point(x + 460, y + 40));
            e.Graphics.DrawString("苍白", zt9, Brushes.Black, x + 500, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x + 550, y + 15, 12, 12);
            if (cmbPFZMYS2.Text == "苍白")
            {
                e.Graphics.DrawLine(pb2, x + 545, y + 15, x + 555, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 555, y + 15 + 12, x + 570, y + 15 - 3);
            }
            e.Graphics.DrawString("红润", zt9, Brushes.Black, x + 610, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x + 660, y + 15, 12, 12);
            if (cmbPFZMYS2.Text == "红润")
            {
                e.Graphics.DrawLine(pb2, x + 655, y + 15, x + 665, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 665, y + 15 + 12, x + 680, y + 15 - 3);
            }
            e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 40));
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 40));
            e.Graphics.DrawString("末梢循环", zt9, Brushes.Black, x + 30, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 150, y), new Point(x + 150, y + 40));
            e.Graphics.DrawString("好", zt9, Brushes.Black, x + 180, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x + 210, y + 15, 12, 12);
            if (cmbMSXH1.Text == "好")
            {
                e.Graphics.DrawLine(pb2, x + 205, y + 15, x + 215, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 215, y + 15 + 12, x + 230, y + 15 - 3);
            }
            e.Graphics.DrawString("中", zt9, Brushes.Black, x + 260, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x + 290, y + 15, 12, 12);
            if (cmbMSXH1.Text == "中")
            {
                e.Graphics.DrawLine(pb2, x + 285, y + 15, x + 295, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 295, y + 15 + 12, x + 310, y + 15 - 3);
            }
            e.Graphics.DrawString("差", zt9, Brushes.Black, x + 340, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x + 370, y + 15, 12, 12);
            if (cmbMSXH1.Text == "差")
            {
                e.Graphics.DrawLine(pb2, x + 365, y + 15, x + 375, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 375, y + 15 + 12, x + 390, y + 15 - 3);
            }
            e.Graphics.DrawLine(ptp, new Point(x + 460, y), new Point(x + 460, y + 40));
            e.Graphics.DrawString("好", zt9, Brushes.Black, x + 490, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x + 520, y + 15, 12, 12);
            if (cmbMSXH2.Text == "好")
            {
                e.Graphics.DrawLine(pb2, x + 515, y + 15, x + 525, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 525, y + 15 + 12, x + 540, y + 15 - 3);
            }
            e.Graphics.DrawString("中", zt9, Brushes.Black, x + 570, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x + 600, y + 15, 12, 12);
            if (cmbMSXH2.Text == "中")
            {
                e.Graphics.DrawLine(pb2, x + 595, y + 15, x + 605, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 605, y + 15 + 12, x + 620, y + 15 - 3);
            }
            e.Graphics.DrawString("差", zt9, Brushes.Black, x + 650, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x + 680, y + 15, 12, 12);
            if (cmbMSXH2.Text == "差")
            {
                e.Graphics.DrawLine(pb2, x + 675, y + 15, x + 685, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 685, y + 15 + 12, x + 700, y + 15 - 3);
            }
            e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 40));
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 40));
            e.Graphics.DrawString("血压", zt9, Brushes.Black, x + 40, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 150, y), new Point(x + 150, y + 40));
            e.Graphics.DrawString(txtXueYa1.Text, zt9, Brushes.Black, x + 230, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 210, y1 + 15), new Point(x + 290, y1 + 15));
            e.Graphics.DrawString("mmHg", zt9, Brushes.Black, x + 290, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 460, y), new Point(x + 460, y + 40));
            e.Graphics.DrawString(txtXueYa2.Text, zt9, Brushes.Black, x + 520, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 500, y1 + 15), new Point(x + 580, y1 + 15));
            e.Graphics.DrawString("mmHg", zt9, Brushes.Black, x + 580, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 40));
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 40));
            e.Graphics.DrawString("心率", zt9, Brushes.Black, x + 40, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 150, y), new Point(x + 150, y + 40));
            e.Graphics.DrawString(txtXinLV1.Text, zt9, Brushes.Black, x + 240, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 220, y1 + 15), new Point(x + 290, y1 + 15));
            e.Graphics.DrawString("（次/分）", zt9, Brushes.Black, x + 290, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 460, y), new Point(x + 460, y + 40));
            e.Graphics.DrawString(txtXinLV2.Text, zt9, Brushes.Black, x + 530, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 510, y1 + 15), new Point(x + 580, y1 + 15));
            e.Graphics.DrawString("（次/分）", zt9, Brushes.Black, x + 580, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 40));
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 40));
            e.Graphics.DrawString("SPO2", zt9, Brushes.Black, x + 40, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 150, y), new Point(x + 150, y + 40));
            e.Graphics.DrawString(txtSPO21.Text, zt9, Brushes.Black, x + 240, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 460, y), new Point(x + 460, y + 40));
            e.Graphics.DrawString(txtSPO22.Text, zt9, Brushes.Black, x + 530, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 40));
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            //e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            //e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 40));
            //e.Graphics.DrawString("血压", zt9, Brushes.Black, x + 40, y + 15);
            //e.Graphics.DrawLine(ptp, new Point(x + 150, y), new Point(x + 150, y + 40));
            //e.Graphics.DrawLine(ptp, new Point(x + 460, y), new Point(x + 460, y + 40));
            //e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 40));
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 40));
            e.Graphics.DrawString("血气分析结果", zt9, Brushes.Black, x + 20, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 150, y), new Point(x + 150, y + 40));
            e.Graphics.DrawString("Hb "+txtHb1.Text, zt9, Brushes.Black, x + 160, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 180, y1+15), new Point(x + 220, y1+15 ));
            e.Graphics.DrawString("g/dl", zt9, Brushes.Black, x + 220, y + 15);
            e.Graphics.DrawString("Hct "+txtHct1.Text, zt9, Brushes.Black, x + 300, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 320, y1 + 15), new Point(x + 360, y1 + 15));
            e.Graphics.DrawString("g/dl", zt9, Brushes.Black, x + 360, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 460, y), new Point(x + 460, y + 40));
            e.Graphics.DrawString("Hb " + txtHb2.Text, zt9, Brushes.Black, x + 470, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 490, y1 + 15), new Point(x + 530, y1 + 15));
            e.Graphics.DrawString("g/dl", zt9, Brushes.Black, x + 530, y + 15);
            e.Graphics.DrawString("Hct " + txtHct2.Text, zt9, Brushes.Black, x + 610, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 630, y1 + 15), new Point(x + 670, y1 + 15));
            e.Graphics.DrawString("g/dl", zt9, Brushes.Black, x + 670, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 40));
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 40));
            e.Graphics.DrawString("CVP(cmH2O)", zt9, Brushes.Black, x + 20, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 150, y), new Point(x + 150, y + 40));
            e.Graphics.DrawString(txtCVP1.Text, zt9, Brushes.Black, x + 240, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 460, y), new Point(x + 460, y + 40));
            e.Graphics.DrawString(txtCVP2.Text, zt9, Brushes.Black, x + 530, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 40));
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 40));
            e.Graphics.DrawString("尿量（ml）", zt9, Brushes.Black, x + 30, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 150, y), new Point(x + 150, y + 40));
            e.Graphics.DrawString(txtNL1.Text, zt9, Brushes.Black, x + 240, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 460, y), new Point(x + 460, y + 40));
            e.Graphics.DrawString(txtNL2.Text, zt9, Brushes.Black, x + 530, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 40));
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 40));
            e.Graphics.DrawString("出血量（ml）", zt9, Brushes.Black, x + 40, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 150, y), new Point(x + 150, y + 40));
            e.Graphics.DrawString(txtCXL1.Text, zt9, Brushes.Black, x + 240, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 460, y), new Point(x + 460, y + 40));
            e.Graphics.DrawString(txtCXL2.Text, zt9, Brushes.Black, x + 530, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 40));
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 40));
            e.Graphics.DrawString("创面渗血情况", zt9, Brushes.Black, x + 20, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 150, y), new Point(x + 150, y + 40));
            e.Graphics.DrawString("轻度", zt9, Brushes.Black, x + 180, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x + 220, y + 15, 12, 12);
            if (cmbMCSX1.Text == "轻度")
            {
                e.Graphics.DrawLine(pb2, x + 215, y + 15, x + 225, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 225, y + 15 + 12, x + 240, y + 15 - 3);
            }
            e.Graphics.DrawString("中度", zt9, Brushes.Black, x + 260, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x + 300, y + 15, 12, 12);
            if (cmbMCSX1.Text == "中度")
            {
                e.Graphics.DrawLine(pb2, x + 295, y + 15, x + 305, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 305, y + 15 + 12, x + 320, y + 15 - 3);
            }
            e.Graphics.DrawString("重度", zt9, Brushes.Black, x + 340, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x + 380, y + 15, 12, 12);
            if (cmbMCSX1.Text == "重度")
            {
                e.Graphics.DrawLine(pb2, x + 375, y + 15, x + 385, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 385, y + 15 + 12, x + 400, y + 15 - 3);
            }
            e.Graphics.DrawLine(ptp, new Point(x + 460, y), new Point(x + 460, y + 40));
            e.Graphics.DrawString("轻度", zt9, Brushes.Black, x + 490, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x + 530, y + 15, 12, 12);
            if (cmbMCSX2.Text == "轻度")
            {
                e.Graphics.DrawLine(pb2, x + 525, y + 15, x + 535, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 535, y + 15 + 12, x + 550, y + 15 - 3);
            }
            e.Graphics.DrawString("中度", zt9, Brushes.Black, x + 570, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x + 610, y + 15, 12, 12);
            if (cmbMCSX2.Text == "中度")
            {
                e.Graphics.DrawLine(pb2, x + 605, y + 15, x + 615, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 615, y + 15 + 12, x + 630, y + 15 - 3);
            }
            e.Graphics.DrawString("重度", zt9, Brushes.Black, x + 650, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x + 690, y + 15, 12, 12);
            if (cmbMCSX2.Text == "重度")
            {
                e.Graphics.DrawLine(pb2, x + 685, y + 15, x + 695, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 695, y + 15 + 12, x + 710, y + 15 - 3);
            }
            e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 40));
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 40));
            e.Graphics.DrawString("输血反应", zt9, Brushes.Black, x + 30, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 150, y), new Point(x + 150, y + 40));
            e.Graphics.DrawString("无", zt9, Brushes.Black, x + 160, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x + 190, y + 15, 12, 12);
            if (cmbSXFY.Text == "无")
            {
                e.Graphics.DrawLine(pb2, x + 185, y + 15, x + 195, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 195, y + 15 + 12, x + 210, y + 15 - 3);
            }
            e.Graphics.DrawString("轻度", zt9, Brushes.Black, x + 230, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x + 270, y + 15, 12, 12);
            if (cmbSXFY.Text == "轻度")
            {
                e.Graphics.DrawLine(pb2, x + 265, y + 15, x + 275, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 275, y + 15 + 12, x + 290, y + 15 - 3);
            }
            e.Graphics.DrawString("中度", zt9, Brushes.Black, x + 310, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x + 350, y + 15, 12, 12);
            if (cmbSXFY.Text == "中度")
            {
                e.Graphics.DrawLine(pb2, x + 345, y + 15, x + 355, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 355, y + 15 + 12, x + 370, y + 15 - 3);
            }
            e.Graphics.DrawString("重度", zt9, Brushes.Black, x + 390, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x + 430, y + 15, 12, 12);
            if (cmbSXFY.Text == "重度")
            {
                e.Graphics.DrawLine(pb2, x + 425, y + 15, x + 425, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 435, y + 15 + 12, x + 450, y + 15 - 3);
            }
            e.Graphics.DrawString("处理  "+txtShuXueCL.Text, zt9, Brushes.Black, x + 480, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 505, y1 + 15), new Point(x + 700, y1 + 15));
            e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 40));
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 40));
            e.Graphics.DrawString("综合评价", zt9, Brushes.Black, x + 30, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 150, y), new Point(x + 150, y + 40));
            e.Graphics.DrawString("合理", zt9, Brushes.Black, x + 230, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x + 270, y + 15, 12, 12);
            if (cmbZHPJ.Text == "合理")
            {
                e.Graphics.DrawLine(pb2, x + 265, y + 15, x + 275, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 275, y + 15 + 12, x + 290, y + 15 - 3);
            }
            e.Graphics.DrawString("基本合理", zt9, Brushes.Black, x + 320, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x + 390, y + 15, 12, 12);
            if (cmbZHPJ.Text == "基本合理")
            {
                e.Graphics.DrawLine(pb2, x + 385, y + 15, x + 395, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 395, y + 15 + 12, x + 410, y + 15 - 3);
            }
            e.Graphics.DrawString("不合理", zt9, Brushes.Black, x + 440, y + 15);
            e.Graphics.DrawRectangle(Pens.Black, x + 510, y + 15, 12, 12);
            if (cmbZHPJ.Text == "不合理")
            {
                e.Graphics.DrawLine(pb2, x + 505, y + 15, x + 515, y + 15 + 12);
                e.Graphics.DrawLine(pb2, x + 515, y + 15 + 12, x + 530, y + 15 - 3);
            }
            e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 40));
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
            y = y + 50; y1 = y + 15;
            e.Graphics.DrawString("医师签名：  "+cmbYiShiQM.Text, zt9, Brushes.Black, x, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 55, y1 + 15), new Point(x + 250, y1 + 15));
            e.Graphics.DrawString("日期：  " + DTPGTime.Text, zt9, Brushes.Black, x+500, y + 15);
            e.Graphics.DrawLine(ptp, new Point(x + 530, y1 + 15), new Point(x + 680, y1 + 15));
        }

      
    }
}
