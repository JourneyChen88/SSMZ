using adims_BLL;
using adims_DAL.Dics;
using adims_DAL.Flows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace main
{
    public partial class BeforeVisit_HS_HQ : Form
    {
        adims_BLL.AdimsController Bll = new adims_BLL.AdimsController();
        BeforeVisitDal _BeforeVisitDal = new BeforeVisitDal();
        adims_DAL.Dics.DataDicDal _DataDicDal = new DataDicDal();
        OperScheduleDal _PaibanDal = new OperScheduleDal();
        adims_DAL.PacuDal pdal = new adims_DAL.PacuDal();
        string PatId, Odate;
        bool isRead = false;
        public BeforeVisit_HS_HQ(string patid, string date)
        {
            PatId = patid;
            Odate = date;
            InitializeComponent();
        }

        private void BeforeVisit_HQ_Load(object sender, EventArgs e)
        {
            dtSQFS_Time.Text = Odate;
            dtSHFS_Time.Text = Odate;
            Load_info();
            BindHS1();
            BindHS2();
            BindSSJB();
            BindFXPG();
            cmbTiweiBind();
            BindSSLB();
            BindSF();
            cmbSHSF_HS.Text = Program.customer.user_name;
            cmbSQFS_HS.Text = Program.customer.user_name;
            LodSQFS_HS();//控件赋值
        }
        /// <summary>
        /// 体位
        /// </summary>
        private void cmbTiweiBind()
        {
            DataTable dt = _DataDicDal.SelectDataByTableName("tiwei");
            tbTiwei.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                tbTiwei.Items.Add(dr[1].ToString());
            }
        }
        /// <summary>
        /// 绑定护士 术后
        /// </summary>
        private void BindHS1()
        {
            DataTable dtHS = _DataDicDal.GetAll_hushi();
            cmbSHSF_HS.Items.Clear();
            for (int i = 0; i < dtHS.Rows.Count; i++)
            {
                this.cmbSHSF_HS.Items.Add(dtHS.Rows[i][0]);
            }
        }
        /// <summary>
        /// 绑定护士  术前
        /// </summary>
        private void BindHS2()
        {
            DataTable dtHS = _DataDicDal.GetAll_hushi();
            cmbSQFS_HS.Items.Clear();
            for (int i = 0; i < dtHS.Rows.Count; i++)
            {
                this.cmbSQFS_HS.Items.Add(dtHS.Rows[i][0]);
            }

        }
        /// <summary>
        /// 手术级别
        /// </summary>
        private void BindSSJB()
        {
            DataTable dtssjb = _DataDicDal.GetAllSSJB();
            cmbSsjb.Items.Clear();
            for (int i = 0; i < dtssjb.Rows.Count; i++)
            {
                this.cmbSsjb.Items.Add(dtssjb.Rows[i][0]);
            }

        }
        /// <summary>
        /// 手术类别
        /// </summary>
        private void BindSSLB()
        {
            DataTable dtssjb = _DataDicDal.GetAllSSLB();
            cmbSSLB.Items.Clear();
            for (int i = 0; i < dtssjb.Rows.Count; i++)
            {
                this.cmbSSLB.Items.Add(dtssjb.Rows[i][0]);
            }

        }
        /// <summary>
        /// 身份
        /// </summary>
        private void BindSF()
        {
            //DataTable dtssjb = _DataDicDal.GetAllSF();
            //cmbSF.Items.Clear();
            //for (int i = 0; i < dtssjb.Rows.Count; i++)
            //{
            //    this.cmbSF.Items.Add(dtssjb.Rows[i][0]);
            //}

        }
        private void BindFXPG()
        {
            DataTable dtFxpg = _DataDicDal.GetAllFXPG();
            cmbFxpg.Items.Clear();
            for (int i = 0; i < dtFxpg.Rows.Count; i++)
            {
                this.cmbFxpg.Items.Add(dtFxpg.Rows[i][0]);
            }

        }
        /// <summary>
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
            tbSQZD.Text = dr1["pattmd"].ToString();
            tbTiwei.Text = dr1["Tiwei"].ToString();
            tbNDSS.Text = dr1["oname"].ToString();
            dtFSTime.Text = dr1["odate"].ToString(); ;
        }
        int BCcount = 0;
        /// <summary>
        /// 赋值
        /// </summary>
        private void LodSQFS_HS()
        {
            DataTable dt = _BeforeVisitDal.GetBeforeVisit_HS(PatId);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                ///术前访视
                if (Convert.ToString(dr["Yszk"]).Contains("1")) chkYS_QC.Checked = true;
                if (Convert.ToString(dr["Yszk"]).Contains("2")) chkYS_MH.Checked = true;

                if (Convert.ToString(dr["Yyzk"]).Contains("1")) chkYY_ZC.Checked = true;
                if (Convert.ToString(dr["Yyzk"]).Contains("2")) chkYY_FP.Checked = true;
                if (Convert.ToString(dr["Yyzk"]).Contains("3")) chkYY_XS.Checked = true;
                if (Convert.ToString(dr["Yyzk"]).Contains("4")) chkYY_BL.Checked = true;
                if (Convert.ToString(dr["Yyzk"]).Contains("5")) chkYY_QT.Checked = true;

                if (Convert.ToString(dr["Ywgms"]).Contains("1")) chkGMS_W.Checked = true;
                if (Convert.ToString(dr["Ywgms"]).Contains("2")) chkGMS_Y.Checked = true;
                tbGMS_YW.Text = dr["Yw"].ToString();
                if (Convert.ToString(dr["Xlzk"]).Contains("1")) chkXL_PJ.Checked = true;
                if (Convert.ToString(dr["Xlzk"]).Contains("2")) chkXL_JZ.Checked = true;
                if (Convert.ToString(dr["Xlzk"]).Contains("3")) chkXL_KJ.Checked = true;
                if (Convert.ToString(dr["Xlzk"]).Contains("4")) chkXL_JL.Checked = true;
                if (Convert.ToString(dr["Xlzk"]).Contains("5")) chkXL_QT.Checked = true;

                if (Convert.ToString(dr["Yygt"]).Contains("1")) chkYYGT_ZC.Checked = true;
                if (Convert.ToString(dr["Yygt"]).Contains("2")) chkYYGT_ZAI.Checked = true;

                if (Convert.ToString(dr["Zthd"]).Contains("1")) chkZTHD_LH.Checked = true;
                if (Convert.ToString(dr["Zthd"]).Contains("2")) chkZTHD_SX.Checked = true;
                if (Convert.ToString(dr["Zthd"]).Contains("3")) chkZTHD_GJJZ.Checked = true;

                if (Convert.ToString(dr["Qspf"]).Contains("1")) chkPF_WZ.Checked = true;
                if (Convert.ToString(dr["Qspf"]).Contains("2")) chkPF_PS.Checked = true;
                if (Convert.ToString(dr["Qspf"]).Contains("3")) chkPF_YC.Checked = true;
                tbPF_PS_BW.Text = dr["Psbw"].ToString();
                tbPF_YC_PW.Text = dr["Ycbw"].ToString();
                tbPF_YC_FQ.Text = dr["Fq"].ToString();
                tbPF_YC_MJ.Text = dr["Mj"].ToString();
                tbPF_QT.Text = dr["Qt"].ToString();
                //if (Convert.ToString(dr["Xx"]).Contains("1")) chkHYJC_XX_A.Checked = true;
                //if (Convert.ToString(dr["Xx"]).Contains("2")) chkHYJC_XX_B.Checked = true;
                //if (Convert.ToString(dr["Xx"]).Contains("3")) chkHYJC_XX_AB.Checked = true;
                //if (Convert.ToString(dr["Xx"]).Contains("4")) chkHYJC_XX_O.Checked = true;
                cmbXX.Text = dr["Xx"].ToString();
                if (Convert.ToString(dr["Gy"]).Contains("1")) chkHYJC_GY_Z.Checked = true;
                if (Convert.ToString(dr["Gy"]).Contains("2")) chkHYJC_GY_F.Checked = true;
                if (Convert.ToString(dr["Gy"]).Contains("3")) chkHYJC_GY_WC.Checked = true;
                if (Convert.ToString(dr["Gy"]).Contains("4")) chkHYJC_GY_QT.Checked = true;
                if (Convert.ToString(dr["Gy"]).Contains("5")) chkHYJC_GY_HCV.Checked = true;
                if (Convert.ToString(dr["Gy"]).Contains("6")) chkHYJC_GY_HIV.Checked = true;
                if (Convert.ToString(dr["Gy"]).Contains("7")) chkHYJC_GY_RPR.Checked = true;

                if (Convert.ToString(dr["Brzwcd"]).Contains("1")) chkSSJX_ZBXZ_WQ.Checked = true;
                if (Convert.ToString(dr["Brzwcd"]).Contains("2")) chkSSJX_ZBXZ_BF.Checked = true;
                if (Convert.ToString(dr["Brzwcd"]).Contains("3")) chkSSJX_ZBXZ_WN.Checked = true;
                tbSSJX_ZBXZ_YY.Text = dr["zwyy"].ToString();
                if (Convert.ToString(dr["chzwcd"]).Contains("1")) chkSSJX_SSNR_WQ.Checked = true;
                if (Convert.ToString(dr["chzwcd"]).Contains("2")) chkSSJX_SSNR_BF.Checked = true;
                if (Convert.ToString(dr["chzwcd"]).Contains("3")) chkSSJX_SSNR_WN.Checked = true;
                tbSSJX_SSNR_YY.Text = dr["Yuanyin"].ToString();
                dtSQFS_Time.Text = dr["fsDate"].ToString();
                if (dr["hsqm"].ToString() != "")
                {
                    cmbSQFS_HS.Text = dr["hsqm"].ToString();
                }
                ///术后随访
                if (Convert.ToString(dr["Sh_ssshj"]).Contains("1")) chkBRMYD_SSHJ_SS.Checked = true;
                if (Convert.ToString(dr["Sh_ssshj"]).Contains("2")) chkBRMYD_SSHJ_JBSS.Checked = true;
                if (Convert.ToString(dr["Sh_ssshj"]).Contains("3")) chkBRMYD_SSHJ_BSS.Checked = true;

                if (Convert.ToString(dr["Sh_sztw"]).Contains("1")) chkBRMYD_SZTW_SS.Checked = true;
                if (Convert.ToString(dr["Sh_sztw"]).Contains("2")) chkBRMYD_SZTW_JBSS.Checked = true;
                if (Convert.ToString(dr["Sh_sztw"]).Contains("3")) chkBRMYD_SZTW_BSS.Checked = true;

                if (Convert.ToString(dr["Sh_mycd"]).Contains("1")) chkBRMYD_HLGZ_MY.Checked = true;
                if (Convert.ToString(dr["Sh_mycd"]).Contains("2")) chkBRMYD_HLGZ_JBMY.Checked = true;
                if (Convert.ToString(dr["Sh_mycd"]).Contains("3")) chkBRMYD_HLGZ_BMY.Checked = true;

                //if (Convert.ToString(dr["Sh_Skyhqk"]).Contains("1")) chkSKYH_JJ.Checked = true;
                //if (Convert.ToString(dr["Sh_Skyhqk"]).Contains("2")) chkSKYH_YJ.Checked = true;
                //if (Convert.ToString(dr["Sh_Skyhqk"]).Contains("3")) chkSKYH_BJ.Checked = true;
                txtSh_Skyhqk.Text = dr["Sh_Skyhqk"].ToString();
                tbAddvice.Text = dr["Sh_yj"].ToString();
                dtSHFS_Time.Text = dr["Sh_sfdate"].ToString();
                dtFSTime.Text = dr["odate"].ToString();
                if (dr["Sh_hsqm"].ToString() != "")
                {
                    cmbSHSF_HS.Text = dr["Sh_hsqm"].ToString();
                }
                txtbqgy.Text = dr["bqgy"].ToString();
                txtHzlxfs.Text = dr["Hzlxfs"].ToString();
                cmbSsjb.Text = dr["Ssjb"].ToString();
                cmbFxpg.Text = dr["Fxpg"].ToString();
                cmbXsex.Text = dr["Xsex"].ToString();
                txtMZFS.Text = dr["MZFS"].ToString();
                cmbSSLB.Text = dr["SSLB"].ToString();
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
            DataTable dts = _PaibanDal.GetPaibanByPatId(PatId);
            if (dts.Rows.Count > 0)
            {
                int res = _PaibanDal.UpdatePabanTiwei(tbTiwei.Text, PatId);
            }
            string AddItem = "";
            try
            {
                if (btnSave.Enabled)
                {
                    SHFS.Add("IsRead", "0");
                }
                else
                {
                    SHFS.Add("IsRead", "1");
                }
                SHFS.Add("patid", PatId);
                AddItem = "";
                if (chkYS_QC.Checked) AddItem += "1";
                if (chkYS_MH.Checked) AddItem += "2";
                SHFS.Add("Yszk", AddItem);
                AddItem = "";
                if (chkYY_ZC.Checked) AddItem += "1";
                if (chkYY_FP.Checked) AddItem += "2";
                if (chkYY_XS.Checked) AddItem += "3";
                if (chkYY_BL.Checked) AddItem += "4";
                if (chkYY_QT.Checked) AddItem += "5";
                SHFS.Add("Yyzk", AddItem);
                AddItem = "";
                if (chkGMS_W.Checked) AddItem += "1";
                if (chkGMS_Y.Checked) AddItem += "2";
                SHFS.Add("Ywgms", AddItem);
                SHFS.Add("Yw", tbGMS_YW.Text);
                AddItem = "";
                if (chkXL_PJ.Checked) AddItem += "1";
                if (chkXL_JZ.Checked) AddItem += "2";
                if (chkXL_KJ.Checked) AddItem += "3";
                if (chkXL_JL.Checked) AddItem += "4";
                if (chkXL_QT.Checked) AddItem += "5";
                SHFS.Add("Xlzk", AddItem);
                AddItem = "";
                if (chkYYGT_ZC.Checked) AddItem += "1";
                if (chkYYGT_ZAI.Checked) AddItem += "2";
                SHFS.Add("Yygt", AddItem);
                AddItem = "";
                if (chkZTHD_LH.Checked) AddItem += "1";
                if (chkZTHD_SX.Checked) AddItem += "2";
                if (chkZTHD_GJJZ.Checked) AddItem += "3";
                SHFS.Add("Zthd", AddItem);
                AddItem = "";
                if (chkPF_WZ.Checked) AddItem += "1";
                if (chkPF_PS.Checked) AddItem += "2";
                if (chkPF_YC.Checked) AddItem += "3";
                SHFS.Add("Qspf", AddItem);
                SHFS.Add("Psbw", tbPF_PS_BW.Text);
                SHFS.Add("Ycbw", tbPF_YC_PW.Text);
                SHFS.Add("Fq", tbPF_YC_FQ.Text);
                SHFS.Add("Mj", tbPF_YC_MJ.Text);
                SHFS.Add("Qt", tbPF_QT.Text);
                //AddItem = "";
                //if (chkHYJC_XX_A.Checked) AddItem += "1";
                //if (chkHYJC_XX_B.Checked) AddItem += "2";
                //if (chkHYJC_XX_AB.Checked) AddItem += "3";
                //if (chkHYJC_XX_O.Checked) AddItem += "4";
                SHFS.Add("Xx", cmbXX.Text);
                AddItem = "";
                if (chkHYJC_GY_Z.Checked) AddItem += "1";
                if (chkHYJC_GY_F.Checked) AddItem += "2";
                if (chkHYJC_GY_WC.Checked) AddItem += "3";
                if (chkHYJC_GY_QT.Checked) AddItem += "4";
                if (chkHYJC_GY_HCV.Checked) AddItem += "5";
                if (chkHYJC_GY_HIV.Checked) AddItem += "6";
                if (chkHYJC_GY_RPR.Checked) AddItem += "7";
                SHFS.Add("Gy", AddItem);
                AddItem = "";
                if (chkSSJX_ZBXZ_WQ.Checked) AddItem += "1";
                if (chkSSJX_ZBXZ_BF.Checked) AddItem += "2";
                if (chkSSJX_ZBXZ_WN.Checked) AddItem += "3";
                SHFS.Add("Brzwcd", AddItem);
                SHFS.Add("zwyy", tbSSJX_ZBXZ_YY.Text);
                AddItem = "";
                if (chkSSJX_SSNR_WQ.Checked) AddItem += "1";
                if (chkSSJX_SSNR_BF.Checked) AddItem += "2";
                if (chkSSJX_SSNR_WN.Checked) AddItem += "3";
                SHFS.Add("chzwcd", AddItem);
                SHFS.Add("Yuanyin", tbSSJX_SSNR_YY.Text);
                SHFS.Add("fsDate", Convert.ToDateTime(dtSQFS_Time.Value.ToString()).ToString("yyyy-MM-dd"));
                SHFS.Add("hsqm", cmbSQFS_HS.Text);
                AddItem = "";
                if (chkBRMYD_SSHJ_SS.Checked) AddItem += "1";
                if (chkBRMYD_SSHJ_JBSS.Checked) AddItem += "2";
                if (chkBRMYD_SSHJ_BSS.Checked) AddItem += "3";
                SHFS.Add("Sh_ssshj", AddItem);
                AddItem = "";
                if (chkBRMYD_SZTW_SS.Checked) AddItem += "1";
                if (chkBRMYD_SZTW_JBSS.Checked) AddItem += "2";
                if (chkBRMYD_SZTW_BSS.Checked) AddItem += "3";
                SHFS.Add("Sh_sztw", AddItem);
                AddItem = "";
                if (chkBRMYD_HLGZ_MY.Checked) AddItem += "1";
                if (chkBRMYD_HLGZ_JBMY.Checked) AddItem += "2";
                if (chkBRMYD_HLGZ_BMY.Checked) AddItem += "3";
                SHFS.Add("Sh_mycd", AddItem);
                SHFS.Add("Sh_Skyhqk", txtSh_Skyhqk.Text);
                SHFS.Add("Sh_yj", tbAddvice.Text);
                SHFS.Add("Sh_sfdate", Convert.ToDateTime(dtSHFS_Time.Value.ToString()).ToString("yyyy-MM-dd"));
                SHFS.Add("Sh_hsqm", cmbSHSF_HS.Text);
                SHFS.Add("odate", Convert.ToDateTime(dtFSTime.Value.ToString()).ToString());
                SHFS.Add("bqgy", txtbqgy.Text);
                SHFS.Add("Hzlxfs", txtHzlxfs.Text);
                SHFS.Add("Ssjb", cmbSsjb.Text);
                SHFS.Add("Fxpg", cmbFxpg.Text);
                SHFS.Add("Xsex", cmbXsex.Text);
                SHFS.Add("MZFS", txtMZFS.Text);
                SHFS.Add("SSLB", cmbSSLB.Text);
                DataTable dt = _BeforeVisitDal.GetBeforeVisit_HS(PatId);
                if (dt.Rows.Count > 0)
                {
                    string id = dt.Rows[0]["id"].ToString();
                    result = _BeforeVisitDal.UpdateBeforeVisit_HS(SHFS, id);
                }

                else
                    result = _BeforeVisitDal.InsertBeforeVisit_HS(SHFS);
                if (result > 0)
                {
                    BCcount++; //MessageBox.Show("保存成功！");
                }
                else MessageBox.Show("保存失败！");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
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
            //printDocument1.DefaultPageSettings.PaperSize =
            //           new System.Drawing.Printing.PaperSize("A4", 820, 1160);
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Pen ptp = Pens.Black;//普通画笔
            Pen pblue2 = new Pen(Brushes.Blue);
            pblue2.Width = 2;
            Pen pblack = new Pen(Brushes.Black);
            Pen pblack2 = new Pen(Brushes.Black, 2);
            Font ptzt = new Font("新宋体", 9);//普通字体
            Font ptzt7 = new Font("新宋体", 7);//普通字体
            Font ptzt2 = new Font("黑体", 10, FontStyle.Bold);//加粗十号
            Font ptzt3 = new Font("新宋体", 14, FontStyle.Bold);//加粗14号
            int y = 30; int x = 40; int y1 = 0;
            string title1 = "手术患者围术期访视记录单";
            StringFormat strFor = new StringFormat();
            strFor.Alignment = StringAlignment.Center;
            Rectangle rect = new Rectangle(x + 30, y, 600, 30);
            e.Graphics.DrawString(title1, ptzt3, Brushes.Black, rect, strFor);
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawString("姓名：" + tbPatname.Text, ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 30, y1, x + 80, y1);
            e.Graphics.DrawString("性别：" + tbPatsex.Text.Trim(), ptzt, Brushes.Black, new Point(x + 95, y));
            e.Graphics.DrawLine(Pens.Black, x + 130, y1, x + 160, y1);
            e.Graphics.DrawString("年龄：" + this.tbPatage.Text.Trim() + " 岁", ptzt, Brushes.Black, new Point(x + 165, y));
            e.Graphics.DrawLine(Pens.Black, x + 200, y1, x + 250, y1);
            e.Graphics.DrawString("住院号：" + this.tbZhuyuanID.Text.Trim(), ptzt, Brushes.Black, new Point(x + 255, y));
            e.Graphics.DrawLine(Pens.Black, x + 300, y1, x + 360, y1);
            e.Graphics.DrawString("科室：" + this.tbKeshi.Text.Trim(), ptzt, Brushes.Black, new Point(x + 365, y));
            e.Graphics.DrawLine(Pens.Black, x + 400, y1, x + 470, y1);
            e.Graphics.DrawString("体位：" + this.tbTiwei.Text.Trim(), ptzt, Brushes.Black, new Point(x + 475, y));
            e.Graphics.DrawLine(Pens.Black, x + 510, y1, x + 630, y1);
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("拟麻醉方式：" + this.txtMZFS.Text.Trim(), ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 65, y1, x + 630, y1);
            y = y + 25; y1 = y + 15;
            string str1_zd = "";
            int StrLength_zd = tbSQZD.Text.Trim().Length;
            int row_zd = StrLength_zd / 45;
            e.Graphics.DrawString("诊断：", ptzt, Brushes.Black, x, y);
            for (int i = 0; i <= row_zd;)//13个字符就换行
            {
                if (i < row_zd)
                    str1_zd = tbSQZD.Text.ToString().Substring(i * 45, 45); //从第i*13个开始，截取13个字符串
                else
                    str1_zd = tbSQZD.Text.ToString().Substring(i * 45);
                e.Graphics.DrawString(str1_zd, ptzt, Brushes.Black, x + 30, y);
                e.Graphics.DrawLine(Pens.Black, x + 30, y1, x + 630, y1);
                i++;
                if (i > row_zd)
                {

                }
                else
                {
                    y = y + 25; y1 = y + 15;
                }

            }

            //e.Graphics.DrawString("诊断：" + tbSQZD.Text, ptzt7, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 30, y1, x + 630, y1);

            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("拟定方式：" + tbNDSS.Text.Trim(), ptzt, Brushes.Black, new Point(x + 0, y));
            e.Graphics.DrawLine(Pens.Black, x + 60, y1, x + 400, y1);
            e.Graphics.DrawString("手术时间：" + Convert.ToDateTime(dtFSTime.Text).ToString("yyyy年MM月dd日 HH:mm"), ptzt, Brushes.Black, new Point(x + 420, y));
            e.Graphics.DrawLine(Pens.Black, x + 480, y1, x + 630, y1);
            y = y + 25; y1 = y + 15;
            int yStart = y;
            int yEnd = y;
            e.Graphics.DrawLine(Pens.Black, x, y, x + 650, y);
            //int QS_Y = y;
            ////e.Graphics.DrawString("病\n情\n概\n要", ptzt2, Brushes.Black, new Point(x + 10, y+5));
            ////int BZYYS_gy= y, BZYYS1_gy = y1;
            //////List<string> strS = new List<string>();
            ////string strS1_gy = "";
            ////int StrLengthS_gy = txtbqgy.Text.Trim().Length;
            ////int rowS_gy = StrLengthS_gy / 45;
            ////for (int i = 0; i <= rowS_gy; i++)//45个字符就换行
            ////{
            ////    if (i < rowS_gy)
            ////        strS1_gy = txtbqgy.Text.Trim().ToString().Substring(i * 45, 45); //从第i*45个开始，截取45个字符串
            ////    else
            ////        strS1_gy = txtbqgy.Text.Trim().ToString().Substring(i * 45);
            ////    e.Graphics.DrawString(strS1_gy, ptzt, Brushes.Black, x + 60, BZYYS_gy+5);
            ////    BZYYS_gy = BZYYS_gy + 15; 
            ////}
            //y = y + 100;
            //e.Graphics.DrawLine(Pens.Black, x, y, x + 650, y);
            y = y + 5; y1 = y + 15;
            int x1 = x + 50;
            e.Graphics.DrawString("术\n\n前\n\n访\n\n视", ptzt2, Brushes.Black, new Point(x + 10, y + 80));

            e.Graphics.DrawString("1.术前评估：", ptzt, Brushes.Black, new Point(x1, y));
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("意识状况：", ptzt, Brushes.Black, new Point(x1 + 20, y));
            e.Graphics.DrawRectangle(Pens.Black, x1 + 100, y, 12, 12);
            e.Graphics.DrawString("意识清楚 ", ptzt, Brushes.Black, x1 + 120, y);
            if (chkYS_QC.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 100, y, x1 + 105, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 105, y + 12, x1 + 112, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 220, y, 12, 12);
            e.Graphics.DrawString("意识模糊 ", ptzt, Brushes.Black, x1 + 240, y);
            if (chkYS_MH.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 220, y, x1 + 225, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 225, y + 12, x1 + 232, y - 1);
            }
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("营养状况：", ptzt, Brushes.Black, new Point(x1 + 20, y));
            e.Graphics.DrawRectangle(Pens.Black, x1 + 100, y, 12, 12);
            e.Graphics.DrawString("正常 ", ptzt, Brushes.Black, x1 + 120, y);
            if (chkYY_ZC.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 100, y, x1 + 105, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 105, y + 12, x1 + 112, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 160, y, 12, 12);
            e.Graphics.DrawString("肥胖 ", ptzt, Brushes.Black, x1 + 180, y);
            if (chkYY_FP.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 160, y, x1 + 165, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 165, y + 12, x1 + 172, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 220, y, 12, 12);
            e.Graphics.DrawString("消瘦 ", ptzt, Brushes.Black, x1 + 240, y);
            if (chkYY_XS.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 220, y, x1 + 225, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 225, y + 12, x1 + 232, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 290, y, 12, 12);
            e.Graphics.DrawString("营养不良 ", ptzt, Brushes.Black, x1 + 310, y);
            if (chkYY_BL.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 290, y, x1 + 295, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 295, y + 12, x1 + 302, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 380, y, 12, 12);
            e.Graphics.DrawString("其他 ", ptzt, Brushes.Black, x1 + 400, y);
            if (chkYY_QT.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 380, y, x1 + 385, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 385, y + 12, x1 + 392, y - 1);
            }

            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("药物过敏史：", ptzt, Brushes.Black, new Point(x1 + 20, y));
            e.Graphics.DrawRectangle(Pens.Black, x1 + 100, y, 12, 12);
            e.Graphics.DrawString("无 ", ptzt, Brushes.Black, x1 + 120, y);
            if (chkGMS_W.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 100, y, x1 + 105, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 105, y + 12, x1 + 112, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 160, y, 12, 12);
            e.Graphics.DrawString("有 ", ptzt, Brushes.Black, x1 + 180, y);
            if (chkGMS_Y.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 160, y, x1 + 165, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 165, y + 12, x1 + 172, y - 1);
            }
            e.Graphics.DrawString("(药物：" + tbGMS_YW.Text.Trim() + "     )", ptzt, Brushes.Black, new Point(x1 + 240, y));
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("心里状况：", ptzt, Brushes.Black, new Point(x1 + 20, y));
            e.Graphics.DrawRectangle(Pens.Black, x1 + 100, y, 12, 12);
            e.Graphics.DrawString("平静 ", ptzt, Brushes.Black, x1 + 120, y);
            if (chkXL_PJ.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 100, y, x1 + 105, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 105, y + 12, x1 + 112, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 160, y, 12, 12);
            e.Graphics.DrawString("紧张 ", ptzt, Brushes.Black, x1 + 180, y);
            if (chkXL_JZ.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 160, y, x1 + 165, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 165, y + 12, x1 + 172, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 220, y, 12, 12);
            e.Graphics.DrawString("恐惧 ", ptzt, Brushes.Black, x1 + 240, y);
            if (chkXL_KJ.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 220, y, x1 + 225, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 225, y + 12, x1 + 232, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 290, y, 12, 12);
            e.Graphics.DrawString("焦虑 ", ptzt, Brushes.Black, x1 + 310, y);
            if (chkXL_JL.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 290, y, x1 + 295, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 295, y + 12, x1 + 302, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 380, y, 12, 12);
            e.Graphics.DrawString("其他 ", ptzt, Brushes.Black, x1 + 400, y);
            if (chkXL_QT.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 380, y, x1 + 385, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 385, y + 12, x1 + 392, y - 1);
            }
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("语言沟通：", ptzt, Brushes.Black, new Point(x1 + 20, y));
            e.Graphics.DrawRectangle(Pens.Black, x1 + 100, y, 12, 12);
            e.Graphics.DrawString("沟通正常 ", ptzt, Brushes.Black, x1 + 120, y);
            if (chkYYGT_ZC.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 100, y, x1 + 105, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 105, y + 12, x1 + 112, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 220, y, 12, 12);
            e.Graphics.DrawString("沟通障碍 ", ptzt, Brushes.Black, x1 + 240, y);
            if (chkYYGT_ZAI.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 220, y, x1 + 225, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 225, y + 12, x1 + 232, y - 1);
            }
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("肢体活动：", ptzt, Brushes.Black, new Point(x1 + 20, y));
            e.Graphics.DrawRectangle(Pens.Black, x1 + 100, y, 12, 12);
            e.Graphics.DrawString("灵活 ", ptzt, Brushes.Black, x1 + 120, y);
            if (chkZTHD_LH.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 100, y, x1 + 105, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 105, y + 12, x1 + 112, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 160, y, 12, 12);
            e.Graphics.DrawString("活动受限 ", ptzt, Brushes.Black, x1 + 180, y);
            if (chkZTHD_SX.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 160, y, x1 + 165, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 165, y + 12, x1 + 172, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 250, y, 12, 12);
            e.Graphics.DrawString("关节僵直 ", ptzt, Brushes.Black, x1 + 270, y);
            if (chkZTHD_GJJZ.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 250, y, x1 + 255, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 255, y + 12, x1 + 262, y - 1);
            }
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("全身皮肤：", ptzt, Brushes.Black, new Point(x1 + 20, y));
            e.Graphics.DrawRectangle(Pens.Black, x1 + 100, y, 12, 12);
            e.Graphics.DrawString("完整 ", ptzt, Brushes.Black, x1 + 120, y);
            if (chkPF_WZ.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 100, y, x1 + 105, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 105, y + 12, x1 + 112, y - 1);
            }
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x1 + 100, y, 12, 12);
            e.Graphics.DrawString("破损 ", ptzt, Brushes.Black, x1 + 120, y);
            if (chkPF_PS.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 100, y, x1 + 105, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 105, y + 12, x1 + 112, y - 1);
            }
            e.Graphics.DrawString("部位：" + tbPF_PS_BW.Text.Trim(), ptzt, Brushes.Black, new Point(x1 + 200, y));
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x1 + 100, y, 12, 12);
            e.Graphics.DrawString("压疮 ", ptzt, Brushes.Black, x1 + 120, y);
            if (chkPF_YC.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 100, y, x1 + 105, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 105, y + 12, x1 + 112, y - 1);
            }
            e.Graphics.DrawString("部位：" + tbPF_YC_PW.Text.Trim(), ptzt, Brushes.Black, new Point(x1 + 200, y));
            e.Graphics.DrawString("分期：" + tbPF_YC_FQ.Text.Trim(), ptzt, Brushes.Black, new Point(x1 + 300, y));
            e.Graphics.DrawString("面积：" + tbPF_YC_MJ.Text.Trim(), ptzt, Brushes.Black, new Point(x1 + 400, y));
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("其他：" + tbPF_QT.Text.Trim(), ptzt, Brushes.Black, new Point(x1 + 100, y));
            //e.Graphics.DrawLine(Pens.Black, x1 + 130, y1, x1 + 630, y1);

            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("化验检查：", ptzt, Brushes.Black, new Point(x1 + 20, y));
            e.Graphics.DrawString("血型：  " + cmbXX.Text, ptzt, Brushes.Black, new Point(x1 + 100, y));
            //e.Graphics.DrawRectangle(Pens.Black, x1 + 140, y, 12, 12);
            //e.Graphics.DrawString("A ", ptzt, Brushes.Black, x1 + 160, y);
            //if (chkHYJC_XX_A.Checked)
            //{
            //    e.Graphics.DrawLine(pblue2, x1 + 140, y, x1 + 145, y + 12);
            //    e.Graphics.DrawLine(pblue2, x1 + 145, y + 12, x1 + 152, y - 1);
            //}
            //e.Graphics.DrawRectangle(Pens.Black, x1 + 190, y, 12, 12);
            //e.Graphics.DrawString("B ", ptzt, Brushes.Black, x1 + 210, y);
            //if (chkHYJC_XX_B.Checked)
            //{
            //    e.Graphics.DrawLine(pblue2, x1 + 190, y, x1 + 195, y + 12);
            //    e.Graphics.DrawLine(pblue2, x1 + 195, y + 12, x1 + 202, y - 1);
            //}
            //e.Graphics.DrawRectangle(Pens.Black, x1 + 240, y, 12, 12);
            //e.Graphics.DrawString("AB ", ptzt, Brushes.Black, x1 + 260, y);
            //if (chkHYJC_XX_AB.Checked)
            //{
            //    e.Graphics.DrawLine(pblue2, x1 + 240, y, x1 + 245, y + 12);
            //    e.Graphics.DrawLine(pblue2, x1 + 245, y + 12, x1 + 252, y - 1);
            //}
            //e.Graphics.DrawRectangle(Pens.Black, x1 + 290, y, 12, 12);
            //e.Graphics.DrawString("O ", ptzt, Brushes.Black, x1 + 310, y);
            //if (chkHYJC_XX_O.Checked)
            //{
            //    e.Graphics.DrawLine(pblue2, x1 + 290, y, x1 + 295, y + 12);
            //    e.Graphics.DrawLine(pblue2, x1 + 295, y + 12, x1 + 302, y - 1);
            //}         
            y = y + 20; y1 = y + 15;

            e.Graphics.DrawString("肝炎全项：", ptzt, Brushes.Black, new Point(x1 + 100, y));
            e.Graphics.DrawRectangle(Pens.Black, x1 + 160, y, 12, 12);
            e.Graphics.DrawString("+ ", ptzt, Brushes.Black, x1 + 180, y);
            if (chkHYJC_GY_Z.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 160, y, x1 + 165, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 165, y + 12, x1 + 172, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 220, y, 12, 12);
            e.Graphics.DrawString("- ", ptzt, Brushes.Black, x1 + 240, y);
            if (chkHYJC_GY_F.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 220, y, x1 + 225, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 225, y + 12, x1 + 232, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 280, y, 12, 12);
            e.Graphics.DrawString("未查 ", ptzt, Brushes.Black, x1 + 300, y);
            if (chkHYJC_GY_WC.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 280, y, x1 + 285, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 285, y + 12, x1 + 292, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 340, y, 12, 12);
            e.Graphics.DrawString("其他 ", ptzt, Brushes.Black, x1 + 360, y);
            if (chkHYJC_GY_QT.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 340, y, x1 + 345, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 345, y + 12, x1 + 352, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 390, y, 12, 12);
            e.Graphics.DrawString("HCV", ptzt, Brushes.Black, x1 + 410, y);
            if (chkHYJC_GY_HCV.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 390, y, x1 + 395, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 395, y + 12, x1 + 402, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 450, y, 12, 12);
            e.Graphics.DrawString("HIV", ptzt, Brushes.Black, x1 + 470, y);
            if (chkHYJC_GY_HIV.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 450, y, x1 + 455, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 455, y + 12, x1 + 462, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 500, y, 12, 12);
            e.Graphics.DrawString("RPR", ptzt, Brushes.Black, x1 + 520, y);
            if (chkHYJC_GY_RPR.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 500, y, x1 + 505, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 505, y + 12, x1 + 512, y - 1);
            }
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("2.手术宣教：", ptzt, Brushes.Black, new Point(x1, y));
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("(1)按照《手术病人术前准备须知》向病人做术前准备的极少，病人掌握程度：", ptzt, Brushes.Black, new Point(x1 + 30, y));
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x1 + 40, y, 12, 12);
            e.Graphics.DrawString("完全掌握 ", ptzt, Brushes.Black, x1 + 60, y);
            if (chkSSJX_ZBXZ_WQ.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 40, y, x1 + 45, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 45, y + 12, x1 + 52, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 140, y, 12, 12);
            e.Graphics.DrawString("部分掌握 ", ptzt, Brushes.Black, x1 + 160, y);
            if (chkSSJX_ZBXZ_BF.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 140, y, x1 + 145, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 145, y + 12, x1 + 152, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 240, y, 12, 12);
            e.Graphics.DrawString("未能掌握 ", ptzt, Brushes.Black, x1 + 260, y);
            if (chkSSJX_ZBXZ_WN.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 240, y, x1 + 245, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 245, y + 12, x1 + 252, y - 1);
            }
            e.Graphics.DrawString("(原因：" + tbSSJX_ZBXZ_YY.Text.Trim() + "     )", ptzt, Brushes.Black, new Point(x1 + 320, y));
            //e.Graphics.DrawLine(Pens.Black, x1 + 410, y1, x1 + 700, y1);
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("(1)向患者进行手术先关内容宣教，包括手术护士，手术室环境，\n体位（手术和麻醉），约束等相关知识宣教，病人掌握程度：", ptzt, Brushes.Black, new Point(x1 + 30, y));
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x1 + 40, y, 12, 12);
            e.Graphics.DrawString("完全掌握 ", ptzt, Brushes.Black, x1 + 60, y);
            if (chkSSJX_SSNR_WQ.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 40, y, x1 + 45, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 45, y + 12, x1 + 52, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 140, y, 12, 12);
            e.Graphics.DrawString("部分掌握 ", ptzt, Brushes.Black, x1 + 160, y);
            if (chkSSJX_SSNR_BF.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 140, y, x1 + 145, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 145, y + 12, x1 + 152, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 240, y, 12, 12);
            e.Graphics.DrawString("未能掌握 ", ptzt, Brushes.Black, x1 + 260, y);
            if (chkSSJX_SSNR_WN.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 240, y, x1 + 245, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 245, y + 12, x1 + 252, y - 1);
            }
            e.Graphics.DrawString("(原因：" + tbSSJX_SSNR_YY.Text.Trim() + "     )", ptzt, Brushes.Black, new Point(x1 + 320, y));
            //e.Graphics.DrawLine(Pens.Black, x1 + 410, y1, x1 + 700, y1);
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("访视时间：" + dtSQFS_Time.Text, ptzt, Brushes.Black, new Point(x1 + 200, y));
            e.Graphics.DrawLine(Pens.Black, x1 + 260, y1, x1 + 350, y1);
            e.Graphics.DrawString("护士签名：" + cmbSQFS_HS.Text.Trim(), ptzt, Brushes.Black, new Point(x1 + 380, y));
            e.Graphics.DrawLine(Pens.Black, x1 + 440, y1, x1 + 570, y1);
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawLine(Pens.Black, x + 0, y, x + 650, y);
            e.Graphics.DrawString("术\n\n后\n\n随\n\n访", ptzt2, Brushes.Black, new Point(x + 10, y + 60));
            y = y + 5;
            e.Graphics.DrawString("患者手术满意程度：", ptzt, Brushes.Black, new Point(x1 + 0, y));
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString(" （1）手术室环境：", ptzt, Brushes.Black, new Point(x1 + 0, y));
            e.Graphics.DrawRectangle(Pens.Black, x1 + 200, y, 12, 12);
            e.Graphics.DrawString("舒适", ptzt, Brushes.Black, x1 + 220, y);
            if (chkBRMYD_SSHJ_SS.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 200, y, x1 + 205, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 205, y + 12, x1 + 212, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 280, y, 12, 12);
            e.Graphics.DrawString("基本舒适", ptzt, Brushes.Black, x1 + 300, y);
            if (chkBRMYD_SSHJ_JBSS.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 280, y, x1 + 285, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 285, y + 12, x1 + 292, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 380, y, 12, 12);
            e.Graphics.DrawString("不舒适", ptzt, Brushes.Black, x1 + 400, y);
            if (chkBRMYD_SSHJ_BSS.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 380, y, x1 + 385, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 385, y + 12, x1 + 392, y - 1);
            }
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString(" （2）术中体位：", ptzt, Brushes.Black, new Point(x1 + 0, y));
            e.Graphics.DrawRectangle(Pens.Black, x1 + 200, y, 12, 12);
            e.Graphics.DrawString("舒适", ptzt, Brushes.Black, x1 + 220, y);
            if (chkBRMYD_SZTW_SS.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 200, y, x1 + 205, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 205, y + 12, x1 + 212, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 280, y, 12, 12);
            e.Graphics.DrawString("基本舒适", ptzt, Brushes.Black, x1 + 300, y);
            if (chkBRMYD_SZTW_JBSS.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 280, y, x1 + 285, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 285, y + 12, x1 + 292, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x1 + 380, y, 12, 12);
            e.Graphics.DrawString("不舒适", ptzt, Brushes.Black, x1 + 400, y);
            if (chkBRMYD_SZTW_BSS.Checked)
            {
                e.Graphics.DrawLine(pblue2, x1 + 380, y, x1 + 385, y + 12);
                e.Graphics.DrawLine(pblue2, x1 + 385, y + 12, x1 + 392, y - 1);
            }
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString(" （3）对手术室护理工作满意程度：", ptzt, Brushes.Black, new Point(x1 + 0, y));
            e.Graphics.DrawRectangle(Pens.Black, x1 + 200, y, 12, 12);
            e.Graphics.DrawString("满意", ptzt, Brushes.Black, x1 + 220, y);
            //if (chkBRMYD_HLGZ_MY.Checked)
            //{
            //    e.Graphics.DrawLine(pblue2, x1 + 200, y, x1 + 205, y + 12);
            //    e.Graphics.DrawLine(pblue2, x1 + 205, y + 12, x1 + 212, y - 1);
            //}
            e.Graphics.DrawRectangle(Pens.Black, x1 + 280, y, 12, 12);
            e.Graphics.DrawString("基本满意", ptzt, Brushes.Black, x1 + 300, y);
            //if (chkBRMYD_HLGZ_JBMY.Checked)
            //{
            //    e.Graphics.DrawLine(pblue2, x1 + 280, y, x1 + 285, y + 12);
            //    e.Graphics.DrawLine(pblue2, x1 + 285, y + 12, x1 + 292, y - 1);
            //}
            e.Graphics.DrawRectangle(Pens.Black, x1 + 380, y, 12, 12);
            e.Graphics.DrawString("不满意", ptzt, Brushes.Black, x1 + 400, y);
            //if (chkBRMYD_HLGZ_BMY.Checked)
            //{
            //    e.Graphics.DrawLine(pblue2, x1 + 380, y, x1 + 385, y + 12);
            //    e.Graphics.DrawLine(pblue2, x1 + 385, y + 12, x1 + 392, y - 1);
            //}


            y = y + 20; y1 = y + 15;
            e.Graphics.DrawLine(Pens.Black, x, yStart, x, y);
            e.Graphics.DrawLine(Pens.Black, x1 - 10, yStart, x1 - 10, y);
            e.Graphics.DrawLine(Pens.Black, x + 650, yStart, x + 650, y);
            e.Graphics.DrawLine(Pens.Black, x1 - 10, y, x + 650, y);
            yStart = y;
            y = y + 5; y1 = y + 15;
            e.Graphics.DrawString("1.伤口愈合情况：" + txtSh_Skyhqk.Text, ptzt, Brushes.Black, new Point(x1 + 0, y));
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("2.患者以及家属的意见：", ptzt, Brushes.Black, new Point(x1 + 0, y + 3));
            y = y + 25; y1 = y + 15;
            //int BZYYSS = y;
            //string strSS1 = "";
            //int StrLengthSS = tbAddvice.Text.Trim().Length;
            //int rowSS = StrLengthSS / 45;
            //for (int i = 0; i <= rowSS; i++)//45个字符就换行
            //{
            //    if (i < rowSS)
            //        strSS1 = tbAddvice.Text.Trim().ToString().Substring(i * 45, 45); //从第i*45个开始，截取45个字符串
            //    else
            //        strSS1 = tbAddvice.Text.Trim().ToString().Substring(i * 45);             
            //    e.Graphics.DrawString(strSS1, ptzt, Brushes.Black, x1 + 10, BZYYSS);
            //    BZYYSS = BZYYSS + 25;
            //}

            y = y + 100; y1 = y + 15;
            e.Graphics.DrawString("3.患者联系方式：", ptzt, Brushes.Black, new Point(x1 + 0, y + 3));
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("随访时间：", ptzt, Brushes.Black, new Point(x1 + 200, y));
            e.Graphics.DrawLine(Pens.Black, x1 + 260, y1, x1 + 350, y1);
            e.Graphics.DrawString("护士签名：", ptzt, Brushes.Black, new Point(x1 + 380, y));
            e.Graphics.DrawLine(Pens.Black, x1 + 440, y1, x1 + 580, y1);
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawLine(Pens.Black, x + 0, y, x + 650, y);
            e.Graphics.DrawLine(Pens.Black, x + 0, yStart, x + 0, y);
            e.Graphics.DrawLine(Pens.Black, x + 40, yStart, x + 40, y);
            e.Graphics.DrawLine(Pens.Black, x + 650, yStart, x + 650, y);
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
            DataTable dt = _BeforeVisitDal.GetBeforeVisit_HS(PatId);
            if (dt.Rows.Count == 0)
                MessageBox.Show("需要先保存，才能存档！");
            else
            {
                result = _BeforeVisitDal.UpdateBeforeVisit_HS(PatId, 1);
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
            DataTable dt = _BeforeVisitDal.GetBeforeVisit_HS(PatId);
            if (dt.Rows[0]["isRead"].ToString() == "1")
            {
                result = _BeforeVisitDal.UpdateBeforeVisit_HS(PatId, 1);
                if (result > 0)
                {
                    MessageBox.Show("解锁成功！");
                    btnSave.Enabled = true;
                    isRead = false;
                    btnCD.Enabled = true;
                }
            }
        }
        private void BeforeVisit_HS_HQ_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isRead)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_HS_HQ_FormClosing);
                this.Close();
            }
            else if (BCcount > 0)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_HS_HQ_FormClosing);
                this.Close();
            }
            else
            {
                if (MessageBox.Show("保存退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Save();
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_HS_HQ_FormClosing);
                    this.Close();
                }
                else
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_HS_HQ_FormClosing);
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
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_HS_HQ_FormClosing);
                this.Close();
            }
            else if (BCcount > 0)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_HS_HQ_FormClosing);
                this.Close();
            }
            else
            {
                if (MessageBox.Show("保存退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Save();
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_HS_HQ_FormClosing);
                    this.Close();
                }
                else
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_HS_HQ_FormClosing);
                    this.Close();
                }
            }
        }


    }


}
