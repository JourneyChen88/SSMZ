///************************************
///Updated By        : Senvi
///************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace main
{
    public partial class BeforeVisit_YS : Form
    {
        #region <<Members>>

        AdimsController bll = new AdimsController();
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();

        #endregion

        #region <<Constructors>>

        string patID;//住院号
        string daTime;//时间
        string CardID;//病人的唯一标示
        /// <summary>
        /// 构造函数
        /// </summary>
        public BeforeVisit_YS(string patid,string dtTime)
        {
            patID = patid;
            daTime = dtTime;
            InitializeComponent();

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public BeforeVisit_YS()
        {

            InitializeComponent();

        }

        #endregion

        #region <<Events>>

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeforeVisit_Load(object sender, EventArgs e)
        {
            //this.tabControl1.TabPages.Remove(this.tabPage1);
            this.WindowState = FormWindowState.Maximized;
            BindMZYS();
            BindPatInfo();
            BindMzjldInfo();
            SetMZHZD();
            SetEditValue();
            SetMZJHD();
            SetEditAfterValue();
            SetEditAfterValue2();
            string jurisdiction = bll.Get_user_jurisdiction(Program.customer);
            if (jurisdiction.Contains("8"))
            {
                btnUnlock.Visible = true;
                btnUnlock1.Visible = true;
                btnUnlock2.Visible = true;
                btnUnlock3.Visible = true;
                btnUnlock4.Visible = true;
            }
            else
            {
                btnUnlock.Visible = false;
                btnUnlock1.Visible = false;
                btnUnlock2.Visible = false;
                btnUnlock3.Visible = false;
                btnUnlock4.Visible = false;
            }

        }

        //private void button4_Click(object sender, EventArgs e)
        //{
        //    this.SetVisibleCore(false);
        //    this.FormBorderStyle = FormBorderStyle.None;
        //    this.WindowState = FormWindowState.Maximized;
        //    this.SetVisibleCore(true);
        //}

        private void BindPatInfo()
        {
            DataTable dt = new DataTable();
            dt = DAL.GetALLPAIBANs(patID, daTime);
            tbPatname.Text = dt.Rows[0]["Patname"].ToString();
            tbAge.Text = dt.Rows[0]["Patage"].ToString();
            cmbSex.Text = dt.Rows[0]["Patsex"].ToString();
            tbBingqu.Text = dt.Rows[0]["patdpm"].ToString();
            tbBedNO.Text = dt.Rows[0]["Patbedno"].ToString();
            tbZhuyuanID.Text = dt.Rows[0]["Patzhuyuanid"].ToString();
            tbNssss.Text = dt.Rows[0]["Oname"].ToString();            
            tbSqzd.Text = dt.Rows[0]["Pattmd"].ToString();
            tbSqzd1.Text = dt.Rows[0]["Pattmd"].ToString();
            tbShoushuMC.Text = dt.Rows[0]["Oname"].ToString();
            txtLCZD.Text = dt.Rows[0]["Pattmd"].ToString();
            txtNXSS.Text = dt.Rows[0]["Oname"].ToString();
            CardID = dt.Rows[0]["CardID"].ToString(); //病人唯一标示
        }
        private void BindMzjldInfo()
        {
            DataTable dt = DAL.GetMzjldByPatid(patID);
            if (dt.Rows.Count > 0)
            {
                tbYssss.Text = dt.Rows[0]["shoushufs"].ToString();
                tbMazuiFS.Text = dt.Rows[0]["MazuiFS"].ToString();
            }
        }
        private void BindMZYS()
        {
            DataTable dt = dt = DAL.GetAllMZYS();
            cmbMZYS.Items.Clear();
            cmbMZYS1.Items.Clear();
            cmbMZJHDYY.Items.Clear();
            cmbMZYS11.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                cmbMZYS.Items.Add(dr[0].ToString());
                cmbMZYS1.Items.Add(dr[0].ToString());
                cmbMZJHDYY.Items.Add(dr[0].ToString());
                cmbMZYS11.Items.Add(dr[0].ToString());
            }
        }

   



        #endregion

        #region <<Methods>>控件赋值
        /// <summary>
        /// 麻醉会诊单
        /// </summary>
        private void SetMZHZD() 
        {
            DataTable dt = DAL.GetMZHZD(patID, CardID);
            if (dt.Rows.Count==0)
            {
                return;
            }
            else
            {
                DataRow dr = dt.Rows[0];
                if (dr["SSlx"].ToString().Contains("0")) ckXZX.Checked = true;
                if (dr["SSlx"].ToString().Contains("1")) ckJZSS.Checked = true;
                this.txtSQQk.Text = dr["SQQK"].ToString();
                this.txtYBQk.Text = dr["YBQK"].ToString();
                this.txtTW.Text = dr["T"].ToString();
                this.tbGXY_High_up.Text = dr["BP"].ToString();
                this.txtHX.Text = dr["R"].ToString();
                this.txtMB.Text = dr["P"].ToString();
                this.txtTZ.Text = dr["TZ"].ToString();
                this.txtXSS.Text = dr["XSS"].ToString();
                this.txtHXBYJ.Text = dr["HXB"].ToString();
                this.txtXJ.Text = dr["XJ"].ToString();
                this.txtXN.Text = dr["XN"].ToString();
                this.txtXT.Text = dr["XT"].ToString();
                this.cmbShoushushi.Text = dr["XZGN"].ToString();
                this.cmbFGN.Text = dr["FGN"].ToString();
                this.cmbSGN.Text = dr["SGN"].ToString();
                this.cmbGGN.Text = dr["GGN"].ToString();
                this.txtTSQK.Text = dr["TSQK"].ToString();
                this.txtQTYC.Text = dr["QT"].ToString();
                this.cmbmZHZYY.Text = dr["YYQZ"].ToString();
                this.dtVisitDate1.Text = dr["DaTime"].ToString();
                if (Convert.ToInt32(dr["IsRead"]) == 1)
                {
                    btnSve.Enabled = false;
                    btnLock3.Enabled = false;
                    btnUnlock3.Enabled = true;
                }
                else
                {
                    btnSve.Enabled = true;
                    btnLock3.Enabled = true;
                    btnUnlock3.Enabled = false;
                }
            }
        }
        /// <summary>
        /// 术前方式
        /// </summary>
        private void SetEditValue()
        {
            DataTable dt = DAL.GetBeforeVisit_YS(patID, CardID);
            if (dt.Rows.Count == 0)
                return;
            else
            {
                DataRow dr = dt.Rows[0];
                if (dr["MZFF"].ToString().Contains("1")) cbMZFF1.Checked = true;
                if (dr["MZFF"].ToString().Contains("2")) cbMZFF2.Checked = true;
                if (dr["MZFF"].ToString().Contains("3")) cbMZFF3.Checked = true;
                if (dr["MZFF"].ToString().Contains("4")) cbMZFF4.Checked = true;
                if (dr["MZFF"].ToString().Contains("5")) cbMZFF5.Checked = true; 
                this.cmbASA.Text=dr["ASA"].ToString();
                if (Convert.ToInt32(dr["IsJizhen"]) == 1) cbJizhen.Checked = true;                   
                this.cmbFeiPang.Text=dr["FeiPang"].ToString();
                this.cmbBaowei.Text=dr["Baowei"].ToString();
                this.cmbYNYWS.Text=dr["YNYWS"].ToString();
                this.tbWeight.Text=dr["Weight"].ToString();
                this.cmbJixing.Text=dr["Jixing"].ToString();
                this.cmbJingzhui.Text=dr["Jingzhui"].ToString();
                this.cmbZKKN.Text=dr["ZKKN"].ToString();
                this.cmbZKdu.Text=dr["ZKdu"].ToString();
                this.cmbJiaya.Text=dr["Jiaya"].ToString();
                this.cmbHuxiKN.Text=dr["HuxiKN"].ToString();
                this.cmbMallam.Text=dr["Mallam"].ToString();
                this.cmbXinzang.Text=dr["Xinzang"].ToString();
                cmbGaoxueya.Text=dr["Gaoxueya"].ToString();
                cmbGuanxinB.Text=dr["GuanxinB"].ToString();
                cmbFeiGN.Text=dr["FeiGN"].ToString();
                cmbFeibuJB.Text=dr["FeibuJB"].ToString();
                cmbGanGN.Text=dr["GanGN"].ToString();
                cmbShenGN.Text=dr["ShenGN"].ToString();
                cmbSJXTJB.Text=dr["SJXTJB"].ToString();
                tbOtherCheck.Text=dr["OtherCheck"].ToString();
                cmbMZFXPG.Text=dr["MZFXPG"].ToString();
                tbJinshi.Text=dr["Jinshi"].ToString();
                tbJinyin.Text=dr["Jinyin"].ToString();
                tbOtherZhuyi.Text=dr["OtherZhuyi"].ToString();                
                if (dr["MZFZCS"].ToString().Contains("1")) cbKZDXY.Checked=true; 
                if (dr["MZFZCS"].ToString().Contains("2")) cbRGJW.Checked=true;
                if (dr["MZFZCS"].ToString().Contains("3")) cbCVPchuanci.Checked=true;
                if (dr["MZFZCS"].ToString().Contains("4")) cbDMCC.Checked=true;                
                this.cmbMZYS.Text=dr["MZYS"].ToString();
                this.dtApplyDate.Value=Convert.ToDateTime(dr["ApplyDate"].ToString());
                if (Convert.ToInt32(dr["IsRead"]) == 1)
                {
                    btnSave1.Enabled = false;
                    btnLock1.Enabled = false;
                    btnUnlock1.Enabled = true;
                }
                else
                {
                    btnSave1.Enabled = true;
                    btnLock1.Enabled = true;
                    btnUnlock1.Enabled = false;
                }
            }
        }
        /// <summary>
        /// 麻醉计划单
        /// </summary>
        private void SetMZJHD() 
        {
            DataTable dt = DAL.GetMZJHD(patID, CardID);
            if (dt.Rows.Count==0)
            {
                return;
            }
            else
            {
                DataRow dr = dt.Rows[0];
                if (dr["NXmzfs"].ToString().Contains("1")) ckQM.Checked = true;
                if (dr["NXmzfs"].ToString().Contains("2")) ckYMW.Checked = true;
                if (dr["NXmzfs"].ToString().Contains("3")) ckYYLH.Checked = true;
                if (dr["NXmzfs"].ToString().Contains("4")) ckYM.Checked = true;                
                if (dr["NXmzfs"].ToString().Contains("5")) ckZCSJ.Checked = true;
                if (dr["NXmzfs"].ToString().Contains("6")) ckBCSJ.Checked = true;
                if (dr["NXmzfs"].ToString().Contains("7")) ckJH.Checked = true;
                if (dr["NXmzfs"].ToString().Contains("8")) ckJM.Checked = true;
                if (dr["NXmzfs"].ToString().Contains("9")) ckW.Checked = true;
                if ( ckYM.Checked==true)
                {
                    this.cmbCC.Text = dr["NCCD"].ToString();
                }
                if (dr["BXmzfs"].ToString().Contains("1")) ckQM1.Checked = true;
                if (dr["BXmzfs"].ToString().Contains("2")) ckYMW1.Checked = true;
                if (dr["BXmzfs"].ToString().Contains("3")) ckYYLH1.Checked = true;
                if (dr["BXmzfs"].ToString().Contains("4")) ckYM1.Checked = true;
                if (dr["BXmzfs"].ToString().Contains("5")) ckZCSJ1.Checked = true;
                if (dr["BXmzfs"].ToString().Contains("6")) ckBCSJ1.Checked = true;
                if (dr["BXmzfs"].ToString().Contains("7")) ckJH1.Checked = true;
                if (dr["BXmzfs"].ToString().Contains("8")) ckJM1.Checked = true;
                if (dr["BXmzfs"].ToString().Contains("9")) ckW1.Checked = true;
                if (ckYM1.Checked == true)
                {
                    this.cmbCC1.Text = dr["BCCD"].ToString();
                }
                if (dr["NXmzyp"].ToString().Contains("A")) ckBBF.Checked = true;
                if (dr["NXmzyp"].ToString().Contains("B")) ckYTMZ.Checked = true;
                if (dr["NXmzyp"].ToString().Contains("C")) ckMDZl.Checked = true;
                if (dr["NXmzyp"].ToString().Contains("D")) ckSFTN.Checked = true;
                if (dr["NXmzyp"].ToString().Contains("E")) ckFTN.Checked = true;
                if (dr["NXmzyp"].ToString().Contains("F")) ckYFW.Checked = true;
                if (dr["NXmzyp"].ToString().Contains("G")) ckQFW.Checked = true;
                if (dr["NXmzyp"].ToString().Contains("H")) ckWKXA.Checked = true;
                if (dr["NXmzyp"].ToString().Contains("Y")) ckYSLDKY.Checked = true;
                if (dr["NXmzyp"].ToString().Contains("J")) ckNLP.Checked = true;
                if (dr["NXmzyp"].ToString().Contains("K")) ckZXBBKY.Checked = true;
                if (dr["NXmzyp"].ToString().Contains("L")) ckJHS.Checked = true;
                if (dr["NXmzyp"].ToString().Contains("M")) ckLAT.Checked = true;
                if (dr["NXmzyp"].ToString().Contains("N")) ckSSAQ.Checked = true;
                if (dr["NXmzyp"].ToString().Contains("O")) ckQTYY.Checked = true;
                if (ckQTYY.Checked == true)
                {
                    this.txtQTYY.Text = dr["NXqtyp"].ToString();
                }
                if (dr["NJCXM"].ToString().Contains("A")) ckNIBP.Checked = true;
                if (dr["NJCXM"].ToString().Contains("B")) ckECG.Checked = true;
                if (dr["NJCXM"].ToString().Contains("C")) ckSPO2.Checked = true;
                if (dr["NJCXM"].ToString().Contains("D")) ckPetCO2.Checked = true;
                if (dr["NJCXM"].ToString().Contains("E")) ckNL.Checked = true;
                if (dr["NJCXM"].ToString().Contains("F")) ckIBP.Checked = true;
                if (dr["NJCXM"].ToString().Contains("G")) ckCVP.Checked = true;
                if (dr["NJCXM"].ToString().Contains("H")) ckXQFX.Checked = true;
                if (dr["NJCXM"].ToString().Contains("Y")) ckDJZ.Checked = true;
                if (dr["NJCXM"].ToString().Contains("J")) ckXT.Checked = true;
                if (dr["NJCXM"].ToString().Contains("K")) ckXGN.Checked = true;
                if (dr["NJCXM"].ToString().Contains("L")) ckQT.Checked = true;
                if (ckQT.Checked == true)
                {
                    this.txtQT.Text = dr["QT"].ToString();
                }
                if (dr["Mzfzcs"].ToString().Contains("1")) ckKZXDXY.Checked = true;
                if (dr["Mzfzcs"].ToString().Contains("2")) ckRGDW.Checked = true;
                if (dr["Mzfzcs"].ToString().Contains("3")) ckZXJMCCZG.Checked = true;
                if (dr["Mzfzcs"].ToString().Contains("4")) ckDMCCZG.Checked = true;
                this.txtSZFXKL.Text = dr["SZfxkl"].ToString();
                this.txtJJ.Text = dr["NseBei"].ToString();
                this.cmbZR.Text = dr["SHzr"].ToString();
                this.cmbMZJHDYY.Text = dr["MZYS"].ToString();
                this.dtMZJHD.Value = Convert.ToDateTime(dr["MzjhDate"].ToString());
                if (Convert.ToString(dr["IsRead"]) == "1")
                {
                    btnSave4.Enabled = false;
                    btnLock4.Enabled = false;
                    btnUnlock4.Enabled = true;
                }
                else
                {
                    btnSave4.Enabled = true;
                    btnLock4.Enabled = true;
                    btnUnlock4.Enabled = false;
                }
            }
        }
        /// <summary>
        /// 术后第一天
        /// </summary>
        private void SetEditAfterValue()
        {
            DataTable dt = DAL.GetAfterVisitCount_CJ(patID, CardID);
            if (dt.Rows.Count == 0)
                return;
            else
            {
                DataRow dr = dt.Rows[0];
                this.cmbYishi.Text = dr["Yishi"].ToString();
                this.tbXueya.Text = dr["Xueya"].ToString();
                this.tbHuxi.Text = dr["Huxi"].ToString();
                this.tbXinlv.Text = dr["Xinlv"].ToString();
                if (dr["YBZZ"].ToString().Contains("1")) cbYBZZ1.Checked = true;
                if (dr["YBZZ"].ToString().Contains("2")) cbYBZZ2.Checked = true;
                if (dr["YBZZ"].ToString().Contains("3")) cbYBZZ3.Checked = true;
                if (dr["YBZZ"].ToString().Contains("4")) cbYBZZ4.Checked = true;
                if (dr["YBZZ"].ToString().Contains("5")) cbYBZZ5.Checked = true;
                if (dr["YBZZ"].ToString().Contains("6")) cbYBZZ6.Checked = true;
                if (dr["YBZZ"].ToString().Contains("7")) cbYBZZ7.Checked = true;
                if (dr["YBZZ"].ToString().Contains("8")) cbYBZZ8.Checked = true;
                this.tbOtherZZ.Text = dr["OtherZZ"].ToString();
                this.cmbYishiSJ.Text = dr["YishiSJ"].ToString();
                this.tbXueyaSJ.Text = dr["XueyaSJ"].ToString();
                this.tbHuxiSJ.Text = dr["HuxiSJ"].ToString();
                this.tbXinlvSJ.Text = dr["XinlvSJ"].ToString();
                this.cmbExin.Text = dr["Exin"].ToString();
                this.cmbChuanciBW.Text = dr["ChuanciBW"].ToString();
                this.cmbZhitiHD.Text = dr["ZhitiHD"].ToString();
                this.cmbShuhouZT.Text = dr["ShuhouZT"].ToString();
                this.tbZTBPF.Text = dr["ZTBPF"].ToString();
                cmbZTXG.Text = dr["ZTXG"].ToString();
                cmbMZYS1.Text = dr["MZYS"].ToString();
                this.dtVisitDate.Value = Convert.ToDateTime(dr["VisitDate"].ToString());
                this.tbRemark.Text = dr["BZ"].ToString();
                if (Convert.ToString(dr["IsRead"]) == "1")
                {
                    btnSave.Enabled = false;
                    btnLock.Enabled = false;
                    btnUnlock.Enabled = true;
                }
                else
                {
                    btnSave.Enabled = true;
                    btnLock.Enabled = true;
                    btnUnlock.Enabled = false;
                }
            }
        }
        /// <summary>
        /// 术后第二天
        /// </summary>
        private void SetEditAfterValue2()
        {
            DataTable dt = DAL.GetAfterVisitCount_CJ2(patID,CardID);
            if (dt.Rows.Count == 0)
                return;
            else
            {
                DataRow dr = dt.Rows[0];
                this.cmbYishi1.Text = dr["Yishi"].ToString();
                this.tbXueya1.Text = dr["Xueya"].ToString();
                this.tbHuxi1.Text = dr["Huxi"].ToString();
                this.tbXinlv1.Text = dr["Xinlv"].ToString();
                if (dr["YBZZ"].ToString().Contains("1")) cbYBZZ11.Checked = true;
                if (dr["YBZZ"].ToString().Contains("2")) cbYBZZ21.Checked = true;
                if (dr["YBZZ"].ToString().Contains("3")) cbYBZZ31.Checked = true;
                if (dr["YBZZ"].ToString().Contains("4")) cbYBZZ41.Checked = true;
                if (dr["YBZZ"].ToString().Contains("5")) cbYBZZ51.Checked = true;
                if (dr["YBZZ"].ToString().Contains("6")) cbYBZZ61.Checked = true;
                if (dr["YBZZ"].ToString().Contains("7")) cbYBZZ71.Checked = true;
                if (dr["YBZZ"].ToString().Contains("8")) cbYBZZ81.Checked = true;
                this.tbOtherZZ1.Text = dr["OtherZZ"].ToString();
                this.cmbYishiSJ1.Text = dr["YishiSJ"].ToString();
                this.tbXueyaSJ1.Text = dr["XueyaSJ"].ToString();
                this.tbHuxiSJ1.Text = dr["HuxiSJ"].ToString();
                this.tbXinlvSJ1.Text = dr["XinlvSJ"].ToString();
                this.cmbExin1.Text = dr["Exin"].ToString();
                this.cmbChuanciBW1.Text = dr["ChuanciBW"].ToString();
                this.cmbZhitiHD1.Text = dr["ZhitiHD"].ToString();
                this.cmbShuhouZT1.Text = dr["ShuhouZT"].ToString();
                this.tbZTBPF1.Text = dr["ZTBPF"].ToString();
                cmbZTXG1.Text = dr["ZTXG"].ToString();
                cmbMZYS11.Text = dr["MZYS"].ToString();
                this.dtVisitDate11.Value = Convert.ToDateTime(dr["VisitDate"].ToString());
                this.tbRemark1.Text = dr["BZ"].ToString();
                if (Convert.ToString(dr["IsRead"]) == "1")
                {
                    btnSave2.Enabled = false;
                    btnLock2.Enabled = false;
                    btnUnlock2.Enabled = true;
                }
                else
                {
                    btnSave2.Enabled = true;
                    btnLock2.Enabled = true;
                    btnUnlock2.Enabled = false;
                }
            }
        }
        /// <summary>
        /// 保存方法 麻醉会诊单
        /// </summary>     
        int mzhzd = 0;
        private void SaveMZHZD() 
        {
            Dictionary<string, string> MZHZD = new Dictionary<string, string>();
            int result = 0;
            try
            {
                MZHZD.Add("IsRead","0");
                MZHZD.Add("patid", patID);
                string SSLX = string.Empty;
                if (ckXZX.Checked==true)
                {
                    SSLX = "0";
                }
                if (ckJZSS.Checked==true)
                {
                     SSLX = "1";
                }
                MZHZD.Add("SSlx", SSLX);
                MZHZD.Add("SQQK",this.txtSQQk.Text);
                MZHZD.Add("YBQK",this.txtYBQk.Text);
                MZHZD.Add("T",this.txtTW.Text);
                MZHZD.Add("BP", this.tbGXY_High_up.Text);
                MZHZD.Add("R", this.txtHX.Text);
                MZHZD.Add("P",this.txtMB.Text);
                MZHZD.Add("TZ",this.txtTZ.Text);
                MZHZD.Add("XSS", this.txtXSS.Text);
                MZHZD.Add("HXB",this.txtHXBYJ.Text);
                MZHZD.Add("XJ",this.txtXJ.Text);
                MZHZD.Add("XN",this.txtXN.Text);
                MZHZD.Add("XT",this.txtXT.Text);
                MZHZD.Add("XZGN", this.cmbShoushushi.Text);
                MZHZD.Add("FGN", this.cmbFGN.Text);
                MZHZD.Add("SGN", this.cmbSGN.Text);
                MZHZD.Add("GGN", this.cmbGGN.Text);
                MZHZD.Add("TSQK", this.txtTSQK.Text);
                MZHZD.Add("QT", this.txtQTYC.Text);
                MZHZD.Add("YYQZ", this.cmbmZHZYY.Text);
                MZHZD.Add("DaTime", this.dtVisitDate1.Value.ToString());
                MZHZD.Add("sqdh", CardID);
                DataTable dt = DAL.GetMZHZD(patID,CardID);
                if (dt.Rows.Count == 0)
                    result = DAL.InsertMZHZD(MZHZD);
                else
                    result = DAL.UpdateMZHZD(MZHZD);
                if (result > 0)
                {
                    MessageBox.Show("保存成功。");
                    mzhzd++;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(), "保存出现异常，请检查！");
            }
        }
        /// <summary>
        /// 保存方法 术前访视
        /// </summary>
        /// <returns></returns>
        int BCCount = 0;
        private void SaveBeforeVisit()
        {
            if (!ValiDateBeforeVisit()) return;
            Dictionary<string, string> beforeVisit = new Dictionary<string, string>();
            int result = 0;
            try
            {
                beforeVisit.Add("patid", patID);
                beforeVisit.Add("ASA", this.cmbASA.Text);
                if(cbJizhen.Checked)
                    beforeVisit.Add("isJizhen", "1");
                else
                    beforeVisit.Add("isJizhen", "0");
                beforeVisit.Add("FeiPang", this.cmbFeiPang.Text);
                beforeVisit.Add("Baowei", this.cmbBaowei.Text);
                beforeVisit.Add("YNYWS", this.cmbYNYWS.Text);
                beforeVisit.Add("Weight", this.tbWeight.Text.Trim());
                beforeVisit.Add("Jixing", this.cmbJixing.Text);
                beforeVisit.Add("Jingzhui", this.cmbJingzhui.Text);
                beforeVisit.Add("ZKKN", this.cmbZKKN.Text);
                beforeVisit.Add("ZKdu", this.cmbZKdu.Text);
                beforeVisit.Add("Jiaya", this.cmbJiaya.Text);
                beforeVisit.Add("HuxiKN", this.cmbHuxiKN.Text);
                beforeVisit.Add("Mallam", this.cmbMallam.Text);
                beforeVisit.Add("Xinzang", this.cmbXinzang.Text);
                beforeVisit.Add("Gaoxueya", cmbGaoxueya.Text);
                beforeVisit.Add("GuanxinB", cmbGuanxinB.Text);
                beforeVisit.Add("FeiGN", cmbFeiGN.Text);
                beforeVisit.Add("FeibuJB", cmbFeibuJB.Text);
                beforeVisit.Add("GanGN", cmbGanGN.Text);
                beforeVisit.Add("ShenGN", cmbShenGN.Text);
                beforeVisit.Add("SJXTJB", cmbSJXTJB.Text);
                beforeVisit.Add("OtherCheck", tbOtherCheck.Text.Trim());
                beforeVisit.Add("MZFXPG", cmbMZFXPG.Text.Trim());
                beforeVisit.Add("Jinshi", tbJinshi.Text.Trim());
                beforeVisit.Add("Jinyin", tbJinyin.Text.Trim());
                beforeVisit.Add("OtherZhuyi", tbOtherZhuyi.Text.Trim());
                string mzfzcs = string.Empty;
                if (this.cbKZDXY.Checked) mzfzcs += "1";
                if (this.cbRGJW.Checked) mzfzcs += "2";                    
                if (this.cbCVPchuanci.Checked) mzfzcs += "3";
                if (this.cbDMCC.Checked) mzfzcs += "4";
                beforeVisit.Add("MZFZCS", mzfzcs);
                beforeVisit.Add("MZYS", this.cmbMZYS.Text);
                beforeVisit.Add("ApplyDate", this.dtApplyDate.Value.ToString());
                beforeVisit.Add("IsRead", "0");
                string mzff1 = string.Empty;
                if (cbMZFF1.Checked) mzff1 += "1";
                if (cbMZFF2.Checked) mzff1 += "2"; 
                if (cbMZFF3.Checked) mzff1 += "3";
                if (cbMZFF4.Checked) mzff1 += "4";
                if (cbMZFF5.Checked) mzff1 += "5";
                beforeVisit.Add("MZFF", mzff1);
                beforeVisit.Add("sqdh", CardID);
                DataTable dt = DAL.GetBeforeVisit_YS(patID, CardID);
                if (dt.Rows.Count == 0)
                    result = DAL.InsertBeforeVisit_YS(beforeVisit);
                else
                    result = DAL.UpdateBeforeVisit_YS(beforeVisit);
                if (result > 0)
                {
                    MessageBox.Show("保存成功。");
                    BCCount++;
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "保存出现异常，请检查！");
            }
        }
        /// <summary>
        ///保存  麻醉计划单
        /// </summary>
        int mzjhd = 0;
        private void SaveMZJHD() 
        {
            Dictionary<string, string> MZJHD = new Dictionary<string, string>();
            int result = 0;
            try
            {
                MZJHD.Add("IsRead","0");
                MZJHD.Add("patid",patID);
                string NXmzfs = string.Empty;
                if (ckQM.Checked) NXmzfs += "1";
                if (ckYMW.Checked) NXmzfs += "2";
                if (ckYYLH.Checked) NXmzfs += "3";
                if (ckYM.Checked) NXmzfs += "4";
                if (ckZCSJ.Checked) NXmzfs += "5";
                if (ckBCSJ.Checked) NXmzfs += "6";
                if (ckJH.Checked) NXmzfs += "7";
                if (ckJM.Checked) NXmzfs += "8";
                if (ckW.Checked) NXmzfs += "9";
                MZJHD.Add("NXmzfs", NXmzfs);
                string NCCD = "";
                if (ckYM.Checked) NCCD = this.cmbCC.Text;
                MZJHD.Add("NCCD", NCCD);
                string BXmzfs = string.Empty;
                if (ckQM1.Checked) BXmzfs += "1";
                if (ckYMW1.Checked) BXmzfs += "2";
                if (ckYYLH1.Checked) BXmzfs += "3";
                if (ckYM1.Checked) BXmzfs += "4";
                if (ckZCSJ1.Checked) BXmzfs += "5";
                if (ckBCSJ1.Checked) BXmzfs += "6";
                if (ckJH1.Checked) BXmzfs += "7";
                if (ckJM1.Checked) BXmzfs += "8";
                if (ckW1.Checked) BXmzfs += "9";
                MZJHD.Add("BXmzfs", BXmzfs);
                string BCCD = "";
                if (ckYM1.Checked) BCCD = this.cmbCC1.Text;
                MZJHD.Add("BCCD", BCCD);
                string NXmzyp = string.Empty;
                if (ckBBF.Checked) NXmzyp += "A";
                if (ckYTMZ.Checked) NXmzyp += "B ";
                if (ckMDZl.Checked) NXmzyp += "C";
                if (ckSFTN.Checked) NXmzyp += "D";
                if (ckFTN.Checked) NXmzyp += "E";
                if (ckYFW.Checked) NXmzyp += "F";
                if (ckQFW.Checked) NXmzyp += "G";
                if (ckWKXA.Checked) NXmzyp += "H";
                if (ckYSLDKY.Checked) NXmzyp += "Y";
                if (ckNLP.Checked) NXmzyp += "J";
                if (ckZXBBKY.Checked) NXmzyp += "K";
                if (ckJHS.Checked) NXmzyp += "L";
                if (ckLAT.Checked) NXmzyp += "M";
                if (ckSSAQ.Checked) NXmzyp += "N";
                if (ckQTYY.Checked) NXmzyp += "O";
                MZJHD.Add("NXmzyp", NXmzyp);
                string NXqtyp = "";
                if (ckQTYY.Checked) NXqtyp = txtQTYY.Text.Trim();
                MZJHD.Add("NXqtyp", NXqtyp);
                string NJCXM = string.Empty;
                if (ckNIBP.Checked) NJCXM += "A";
                if (ckECG.Checked) NJCXM += "B";
                if (ckSPO2.Checked) NJCXM += "C";
                if (ckPetCO2.Checked) NJCXM += "D";
                if (ckNL.Checked) NJCXM += "E";
                if (ckIBP.Checked) NJCXM += "F";
                if (ckCVP.Checked) NJCXM += "J";
                if (ckXQFX.Checked) NJCXM += "H";
                if (ckDJZ.Checked) NJCXM += "Y";
                if (ckXT.Checked) NJCXM += "J";
                if (ckXGN.Checked) NJCXM += "K";
                if (ckQT.Checked) NJCXM += "L";
                MZJHD.Add("NJCXM", NJCXM);
                string QT = "";
                if (ckQT.Checked) QT = txtQT.Text.Trim();
                MZJHD.Add("QT", QT);
                string Mzfzcs = string.Empty;
                if (ckKZXDXY.Checked) Mzfzcs += "1";
                if (ckRGDW.Checked) Mzfzcs += "2";
                if (ckZXJMCCZG.Checked) Mzfzcs += "3";
                if (ckDMCCZG.Checked) Mzfzcs += "4";
                MZJHD.Add("Mzfzcs", Mzfzcs);
                MZJHD.Add("SZfxkl", this.txtSZFXKL.Text);
                MZJHD.Add("NseBei",this.txtJJ.Text);
                MZJHD.Add("SHzr",this.cmbZR.Text);
                MZJHD.Add("MZYS",cmbMZJHDYY.Text);
                MZJHD.Add("MzjhDate",this.dtMZJHD.Value.ToString());
                MZJHD.Add("sqdh", CardID);
                DataTable dt = DAL.GetMZJHD(patID,CardID);
                if (dt.Rows.Count == 0)
                    result = DAL.InsertMZJHD(MZJHD);
                else
                    result = DAL.UpdateMZJHD(MZJHD);
                if (result > 0)
                {
                    MessageBox.Show("保存成功！");
                    mzjhd++;
                }
            }
            catch (Exception ex)
            {
                
                 MessageBox.Show(ex.ToString(), "保存出现异常，请检查！");
            }
        }

        int AVcount = 0;
        /// <summary>
        /// 保存 术后第一天
        /// </summary>
        private void SaveAfterVisit()
        {
            if (!ValiDateBeforeVisit()) return;
            Dictionary<string, string> afVisit = new Dictionary<string, string>();
            int result = 0;
            try
            {
                afVisit.Add("patid", patID);
                afVisit.Add("Yishi", this.cmbYishi.Text);
                afVisit.Add("Xueya", this.tbXueya.Text.Trim());
                afVisit.Add("Huxi", this.tbHuxi.Text.Trim());
                afVisit.Add("Xinlv", this.tbXinlv.Text.Trim());
                string ybzz = string.Empty;
                if (this.cbYBZZ1.Checked) ybzz += "1";
                if (this.cbYBZZ2.Checked) ybzz += "2";
                if (this.cbYBZZ3.Checked) ybzz += "3";
                if (this.cbYBZZ4.Checked) ybzz += "4";
                if (this.cbYBZZ5.Checked) ybzz += "5";
                if (this.cbYBZZ6.Checked) ybzz += "6";
                if (this.cbYBZZ7.Checked) ybzz += "7";
                if (this.cbYBZZ8.Checked) ybzz += "8";
                afVisit.Add("YBZZ", ybzz);
                afVisit.Add("OtherZZ", this.tbOtherZZ.Text.Trim());
                afVisit.Add("YishiSJ", this.cmbYishiSJ.Text);
                afVisit.Add("XueyaSJ", this.tbXueyaSJ.Text.Trim());
                afVisit.Add("HuxiSJ", this.tbHuxiSJ.Text.Trim());
                afVisit.Add("XinlvSJ", this.tbXinlvSJ.Text.Trim());
                afVisit.Add("Exin", this.cmbExin.Text);
                afVisit.Add("ChuanciBW", this.cmbChuanciBW.Text);
                afVisit.Add("ZhitiHD", this.cmbZhitiHD.Text);
                afVisit.Add("ShuhouZT", this.cmbShuhouZT.Text);
                afVisit.Add("ZTBPF", this.tbZTBPF.Text.Trim());
                afVisit.Add("ZTXG", this.cmbZTXG.Text);
                afVisit.Add("MZYS", this.cmbMZYS1.Text);              
                afVisit.Add("VisitDate", this.dtVisitDate.Value.ToString());
                afVisit.Add("IsRead", "0");
                afVisit.Add("BZ", this.tbRemark.Text);
                afVisit.Add("sqdh", CardID);
                DataTable dt = DAL.GetAfterVisitCount_CJ(patID,CardID);
                if (dt.Rows.Count == 0)
                    result = DAL.InsertAfterVisit_CJ(afVisit);
                else
                    result = DAL.UpdateAfterVisit_CJ(afVisit);
                if (result > 0)
                {
                    MessageBox.Show("保存成功！");
                    AVcount++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "保存出现异常，请检查！");
            }
        }
        int AVcount2 = 0;
        /// <summary>
        /// 保存 术后第二天
        /// </summary>
        private void SaveAfterVisit2()
        {
            if (!ValiDateBeforeVisit()) return;
            Dictionary<string, string> afVisit = new Dictionary<string, string>();
            int result = 0;
            try
            {
                afVisit.Add("patid", patID);
                afVisit.Add("Yishi", this.cmbYishi1.Text);
                afVisit.Add("Xueya", this.tbXueya1.Text.Trim());
                afVisit.Add("Huxi", this.tbHuxi1.Text.Trim());
                afVisit.Add("Xinlv", this.tbXinlv1.Text.Trim());
                string ybzz = string.Empty;
                if (this.cbYBZZ11.Checked) ybzz += "1";
                if (this.cbYBZZ21.Checked) ybzz += "2";
                if (this.cbYBZZ31.Checked) ybzz += "3";
                if (this.cbYBZZ41.Checked) ybzz += "4";
                if (this.cbYBZZ51.Checked) ybzz += "5";
                if (this.cbYBZZ61.Checked) ybzz += "6";
                if (this.cbYBZZ71.Checked) ybzz += "7";
                if (this.cbYBZZ81.Checked) ybzz += "8";
                afVisit.Add("YBZZ", ybzz);
                afVisit.Add("OtherZZ", this.tbOtherZZ1.Text.Trim());
                afVisit.Add("YishiSJ", this.cmbYishiSJ1.Text);
                afVisit.Add("XueyaSJ", this.tbXueyaSJ1.Text.Trim());
                afVisit.Add("HuxiSJ", this.tbHuxiSJ1.Text.Trim());
                afVisit.Add("XinlvSJ", this.tbXinlvSJ1.Text.Trim());
                afVisit.Add("Exin", this.cmbExin1.Text);
                afVisit.Add("ChuanciBW", this.cmbChuanciBW1.Text);
                afVisit.Add("ZhitiHD", this.cmbZhitiHD1.Text);
                afVisit.Add("ShuhouZT", this.cmbShuhouZT1.Text);
                afVisit.Add("ZTBPF", this.tbZTBPF1.Text.Trim());
                afVisit.Add("ZTXG", this.cmbZTXG1.Text);
                afVisit.Add("MZYS", this.cmbMZYS11.Text);
                afVisit.Add("VisitDate", this.dtVisitDate11.Value.ToString());
                afVisit.Add("IsRead", "0");
                afVisit.Add("BZ", this.tbRemark1.Text);
                afVisit.Add("sqdh", CardID);
                DataTable dt = DAL.GetAfterVisitCount_CJ2(patID,CardID);
                if (dt.Rows.Count == 0)
                    result = DAL.InsertAfterVisit_CJ2(afVisit);
                else
                    result = DAL.UpdateAfterVisit_CJ2(afVisit);
                if (result > 0)
                {
                    MessageBox.Show("保存成功！");
                    AVcount2++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "保存出现异常，请检查！");
            }
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private bool ValiDateBeforeVisit()
        {           
            //if (!ValidationRegex.ValidteData(tbXiyanliang.Text))
            //{
            //    MessageBox.Show("吸烟量填写有误，请检查！");
            //    tbJieyanDay.Focus();
            //    return false;
            //}
            //if (!ValidationRegex.ValidteDouble(tbXiyanYear.Text))
            //{
            //    MessageBox.Show("吸烟年数填写有误，请检查！");
            //    tbJieyanDay.Focus();
            //    return false;
            //}
            return true;
        }

        #endregion

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.0;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            //printDocument1.DefaultPageSettings.PaperSize =
            //           new System.Drawing.Printing.PaperSize("K16", 740, 1020);  
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("A4", 820, 1160);  
        }


        private void printStripMenuItem_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
           
        }


        private void BeforeVisit_YS_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnSave.Enabled == false)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                this.Close();
            }
            else
            {
                if (BCCount > 0)
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                    this.Close();
                }
                else
                {
                    if (DialogResult.Yes == MessageBox.Show("请确认所填的内容是否已经保存？", "提示", MessageBoxButtons.YesNo))
                    {
                        //SaveBeforeVisit();
                        this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                        this.Close();
                    }
                    else
                    {
                        this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                        this.Close();
                    }
                }
            }
        }

        private void btnExit1_Click(object sender, EventArgs e)
        {
            if (btnSave.Enabled == false)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                this.Close();
            }
            else
            {
                if (BCCount > 0)
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                    this.Close();
                }
                else
                {
                    if (DialogResult.Yes == MessageBox.Show("是否保存术前访视单？", "提示", MessageBoxButtons.YesNo))
                    {
                        SaveBeforeVisit();
                        this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                        this.Close();
                    }
                    else
                    {
                        this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                        this.Close();
                    }
                }
            }
        }

      
        /// <summary>
        /// 保存术前访视
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave1_Click(object sender, EventArgs e)
        {
            SaveBeforeVisit();
        }

        private void btnLock1_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = DAL.GetBeforeVisit_YS(patID,CardID);
            if (dt.Rows.Count == 0)
                MessageBox.Show("术前访视信息需要先保存到数据库，才能存档！");
            else
            {
                result = DAL.UpdateBeforeVisit_YS_STATE(CardID, 1);
                if (result > 0)
                {
                    MessageBox.Show("存档成功！");
                    btnSave1.Enabled = false;
                    
                }
            }
        }
        private void btnUnlock1_Click(object sender, EventArgs e)
        {
            int result = DAL.UpdateBeforeVisit_YS_STATE(CardID, 0);
            if (result > 0)
            {
                MessageBox.Show("解锁成功！");
                btnSave1.Enabled = true;
                btnLock1.Enabled = true;

            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (btnSave.Enabled == false)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                this.Close();
            }
            else
            {
                if (AVcount > 0)
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                    this.Close();
                }
                else
                {
                    if (DialogResult.Yes == MessageBox.Show("是否保存术后第一天？", "提示", MessageBoxButtons.YesNo))
                    {
                        SaveAfterVisit();
                        this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                        this.Close();
                    }
                    else
                    {
                        this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                        this.Close();
                    }
                }
            }
            this.Close();
        }
        /// <summary>
        /// 术后第一天 存档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLock_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = DAL.GetAfterVisitCount_CJ(patID,CardID);
            if (dt.Rows.Count == 0)
                MessageBox.Show("术后第一天访视信息需要先保存到数据库，才能存档！");
            else
            {
                result = DAL.UpdateAfterVisit_CJ(CardID, 1);
                if (result > 0)
                {
                    MessageBox.Show("存档成功！");
                    btnSave.Enabled = false;
                }
            }
        }
        /// <summary>
        /// 术后第一天 解锁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnlock_Click(object sender, EventArgs e)
        {
            int result = DAL.UpdateAfterVisit_CJ(CardID, 0);
            if (result > 0)
            {
                MessageBox.Show("解锁成功！");
                btnSave.Enabled = true;
                btnLock.Enabled = true;
            }
        }
       
        /// <summary>
        /// 保存术后第一天
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveAfterVisit();
        }

       

        #region //输入限制
        private void tbWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.DataValid.Text_Value_Limit(sender, e);
        }

        private void tbJinshi_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.DataValid.Text_Value_Limit(sender, e);
        }

        private void tbJinyin_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.DataValid.Text_Value_Limit(sender, e);
        }

        private void tbHuxi_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.DataValid.Text_Value_Limit(sender, e);
        }

        private void tbXinlv_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.DataValid.Text_Value_Limit(sender, e);
        }

        private void tbHuxiSJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.DataValid.Text_Value_Limit(sender, e);
        }

        private void tbXinlvSJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.DataValid.Text_Value_Limit(sender, e);
        }

#endregion

        private void BeforeVisit_YS_Resize(object sender, EventArgs e)
        {          
            if (WindowState == FormWindowState.Maximized)
            {
                panel1.Location = new Point(120, 50);
            }
            else if (WindowState == FormWindowState.Normal)
            {
                panel1.Location = new Point(10, 50);
            } 
        }

        private void btnPACS_Click(object sender, EventArgs e)
        {
            string IPaddress = "132.147.160.41";
            bool flag = DataValid.PingHost(IPaddress, 1000);
            if (flag == true)
            {
                string inpatientID = patID;
                string url = "http://132.147.160.41/DicomWeb/Dicomweb.dll/login?PTNID=" + patID + "&User=sm1&Password=1";
                System.Diagnostics.Process.Start(url);
            }
            else MessageBox.Show("影响病历数据库未连接，请检查网络");
            
        }

        private void btnLIS_Click(object sender, EventArgs e)
        {
            string IPaddress = "132.147.160.6";
            bool flag = DataValid.PingHost(IPaddress, 1000);
            if (flag == true)
            {
                LisResult f1 = new LisResult(patID);
                f1.Show();
            }
            else MessageBox.Show("检查病历数据库未连接，请检查网络");
           
        }

        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern int ShowWindow(IntPtr hwnd, int ncmdShow);

        private void btnEMR_Click(object sender, EventArgs e)
        {
            string IPaddress = "132.147.160.60";
            bool flag = DataValid.PingHost(IPaddress, 1000);
            if (flag == true)
            {
                Process proc = new Process();
                proc.StartInfo.FileName = "D:\\ReadEMR\\mrequery.exe";
                proc.StartInfo.Arguments = patID;
                proc.StartInfo.UseShellExecute = false;
                proc.Start(); 
            }
            else MessageBox.Show("电子病历数据库未连接，请检查网络");
           
            
        }
        /// <summary>
        /// 保存术后第二天
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave2_Click(object sender, EventArgs e)
        {
            SaveAfterVisit2();
        }
        /// <summary>
        /// 保存麻醉会诊单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSve_Click(object sender, EventArgs e)
        {
            SaveMZHZD();
        }
        /// <summary>
        /// 麻醉会诊单的存档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLock3_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = DAL.GetMZHZD(patID,CardID);
            if (dt.Rows.Count ==0 )
                MessageBox.Show("麻醉会诊单信息需要先保存到数据库，才能存档！");
            else
            {
                result = DAL.UpdateMZHZD_STATE(CardID, 1);
                if (result > 0)
                {
                    MessageBox.Show("存档成功！");
                    btnSve.Enabled = false;

                }
            }
        }
        /// <summary>
        /// 麻醉会诊单的解锁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnlock3_Click(object sender, EventArgs e)
        {
            int result = DAL.UpdateMZHZD_STATE(CardID, 0);
            if (result > 0)
            {
                MessageBox.Show("解锁成功！");
                btnSve.Enabled = true;
                btnLock3.Enabled = true;
            }
        }
        /// <summary>
        /// 麻醉会诊单退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit3_Click(object sender, EventArgs e)
        {
            if (btnSve.Enabled == false)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                this.Close();
            }
            else
            {
                if (mzhzd > 0)
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                    this.Close();
                }
                else
                {
                    if (DialogResult.Yes == MessageBox.Show("是否保存麻醉会诊单？", "提示", MessageBoxButtons.YesNo))
                    {
                        SaveMZHZD();
                        this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                        this.Close();
                    }
                    else
                    {
                        this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                        this.Close();
                    }
                }
            }
        }
        /// <summary>
        /// 保存麻醉计划单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave4_Click(object sender, EventArgs e)
        {
            SaveMZJHD();
        }
        /// <summary>
        /// 术后随访第二天的解锁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnlock2_Click(object sender, EventArgs e)
        {
            int result = DAL.UpdateBeforeVisit_YS_STATE2(CardID, 0);
            if (result > 0)
            {
                MessageBox.Show("解锁成功！");
                btnSave2.Enabled = true;
                btnLock2.Enabled = true;
            }
        }
        /// <summary>
        /// 术后随访第二天的存档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLock2_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = DAL.GetAfterVisitCount_CJ2(patID,CardID);
            if (dt.Rows.Count == 0)
                MessageBox.Show("术后第二天访视信息需要先保存到数据库，才能存档！");
            else
            {
                result = DAL.UpdateBeforeVisit_YS_STATE2(CardID, 1);
                if (result > 0)
                {
                    MessageBox.Show("存档成功！");
                    btnSave2.Enabled = false;
                }
            }
        }
        /// <summary>
        /// 麻醉计划单的解锁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnlock4_Click(object sender, EventArgs e)
        {
            int result = DAL.UpdateMZJHD(CardID, 0);
            if (result > 0)
            {
                MessageBox.Show("解锁成功！");
                btnSave4.Enabled = true;
                btnLock4.Enabled = true;
            }
        }
        /// <summary>
        /// 麻醉计划单的存档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLock4_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = DAL.GetMZJHD(patID,CardID);
            if (dt.Rows.Count == 0)
                MessageBox.Show("麻醉计划单信息需要先保存到数据库，才能存档！");
            else
            {
                result = DAL.UpdateMZJHD(CardID, 1);
                if (result > 0)
                {
                    MessageBox.Show("存档成功！");
                    btnSave4.Enabled = false;
                }
            }
        }
        /// <summary>
        /// 术后第二天 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit2_Click(object sender, EventArgs e)
        {
            if (btnSave2.Enabled == false)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                this.Close();
            }
            else
            {
                if (AVcount2 > 0)
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                    this.Close();
                }
                else
                {
                    if (DialogResult.Yes == MessageBox.Show("是否保存术后第二天？", "提示", MessageBoxButtons.YesNo))
                    {
                        SaveAfterVisit2();
                        this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                        this.Close();
                    }
                    else
                    {
                        this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                        this.Close();
                    }
                }
            }
            this.Close();
        }
        /// <summary>
        /// 麻醉计划单 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit4_Click(object sender, EventArgs e)
        {
            if (btnSave4.Enabled == false)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                this.Close();
            }
            else
            {
                if (mzjhd > 0)
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                    this.Close();
                }
                else
                {
                    if (DialogResult.Yes == MessageBox.Show("是否保存麻醉计划单？", "提示", MessageBoxButtons.YesNo))
                    {
                        SaveMZJHD();
                        this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                        this.Close();
                    }
                    else
                    {
                        this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                        this.Close();
                    }
                }
            }
        }
        /// <summary>
        /// 打印麻醉会诊单和术前访视单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint1_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.0;
            printDocument1.DefaultPageSettings.PaperSize =
                     new System.Drawing.Printing.PaperSize("A4", 820, 1160);

        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            #region  （以前的）术前方式单 A4
            //Pen ptp = Pens.Black;//普通画笔
            //Pen pblue2 = new Pen(Brushes.Blue);
            //pblue2.Width = 2;
            //Pen pblack = new Pen(Brushes.Black);
            //Pen pblack2 = new Pen(Brushes.Black,2);
            //Font ptzt = new Font("新宋体", 9);//普通字体
            //Font ptzt2 = new Font("黑体", 9 , FontStyle.Bold);//加粗十号
            //Font ptzt3 = new Font("新宋体", 14, FontStyle.Bold);//加粗14号
            //int y = 30; int x = 40; int y1 = 0;
            //string title1 = "麻  醉  术  前  访  视  单";           
            //e.Graphics.DrawString(title1, ptzt3, Brushes.Black, new Point(x + 240, y)); 

            //y = y + 40; y1 = y + 15;
            //if (tbPatname.Text == "") tbPatname.Text = "   无 ";
            //e.Graphics.DrawString("病人姓名：" + tbPatname.Text, ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 50, y1, x + 150, y1);
            //if (cmbSex.Text == "") cmbSex.Text = "   无 ";
            //e.Graphics.DrawString("性别：" + cmbSex.Text.Trim(), ptzt, Brushes.Black, new Point(x + 160, y));
            //e.Graphics.DrawLine(Pens.Black, x + 190, y1, x + 230, y1);
            //if (tbAge.Text == "") tbAge.Text = "   无 ";
            //e.Graphics.DrawString("年龄：" + this.tbAge.Text.Trim()+" 岁", ptzt, Brushes.Black, new Point(x + 240, y));
            //e.Graphics.DrawLine(Pens.Black, x + 270, y1, x + 330, y1);
            //if (tbBingqu.Text == "") tbBingqu.Text = "   无 ";
            //e.Graphics.DrawString("科室：" + tbBingqu.Text.Trim(), ptzt, Brushes.Black, new Point(x + 340, y));
            //e.Graphics.DrawLine(Pens.Black, x + 370, y1, x + 470, y1);
            //if (tbBedNO.Text == "") tbBedNO.Text = "   无 ";
            //e.Graphics.DrawString("床号：" + this.tbBedNO.Text.Trim(), ptzt, Brushes.Black, new Point(x + 480, y));
            //e.Graphics.DrawLine(Pens.Black, x + 510, y1, x + 580, y1);            
            //if (tbZhuyuanID.Text == "") tbZhuyuanID.Text = "   无 ";
            //e.Graphics.DrawString("住院号：" + tbZhuyuanID.Text.Trim(), ptzt, Brushes.Black, new Point(x + 590, y));
            //e.Graphics.DrawLine(Pens.Black, x + 630, y1, x + 740, y1);

            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawString("临床诊断："+tbSqzd.Text.Trim(), ptzt, Brushes.Black, new Point(x, y));
            // e.Graphics.DrawLine(Pens.Black, x + 60, y1, x + 740, y1);

            // y = y + 30; y1 = y + 15;
            // e.Graphics.DrawString("拟行手术方式："+tbNssss.Text.Trim(), ptzt, Brushes.Black, new Point(x, y));
            // e.Graphics.DrawLine(Pens.Black, x + 90, y1, x + 740, y1);

            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawString("拟行麻醉方式：", ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 120, y, 12, 12);
            //e.Graphics.DrawString("全身麻醉", ptzt, Brushes.Black, new Point(x + 140, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 12, 12);
            //e.Graphics.DrawString("全麻+椎管内麻醉", ptzt, Brushes.Black, new Point(x + 260, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 400, y, 12, 12);
            //e.Graphics.DrawString("椎管内麻醉", ptzt, Brushes.Black, new Point(x + 420, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 520, y, 12, 12);
            //e.Graphics.DrawString("神经麻醉", ptzt, Brushes.Black, new Point(x + 540, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 640, y, 12, 12);
            //e.Graphics.DrawString("联合麻醉", ptzt, Brushes.Black, new Point(x + 660, y));
            //if (cbMZFF1.Checked)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 115, y, x + 125, y + 12);
            //    e.Graphics.DrawLine(pblack2, x + 125, y+12, x + 140, y - 3);
            //}
            //if (cbMZFF2.Checked)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 235, y, x + 245, y + 12);
            //    e.Graphics.DrawLine(pblack2, x + 245, y + 12, x + 260, y - 3);
            //}
            //if (cbMZFF3.Checked)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 395, y, x + 405, y + 12);
            //    e.Graphics.DrawLine(pblack2, x + 405, y + 12, x + 420, y - 3);
            //}
            //if (cbMZFF4.Checked)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 515, y, x + 525, y + 12);
            //    e.Graphics.DrawLine(pblack2, x + 525, y + 12, x + 540, y - 3);
            //}
            //if (cbMZFF5.Checked)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 635, y, x + 645, y + 12);
            //    e.Graphics.DrawLine(pblack2, x + 645, y + 12, x + 660, y - 3);
            //}

            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawString("一般情况", ptzt2, Brushes.Black, new Point(x, y));

            //y = y + 35; y1 = y + 15;
            //e.Graphics.DrawString("ASA 分级："+cmbASA.Text, ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 55, y1, x + 120, y1);
            //if (cmbFeiPang.Text == "") cmbFeiPang.Text = "   无 ";
            //e.Graphics.DrawString("肥胖：" + this.cmbFeiPang.Text, ptzt, Brushes.Black, new Point(x + 140, y));
            //e.Graphics.DrawLine(Pens.Black, x + 170, y1, x + 270, y1);
            //if (cmbBaowei.Text == "") cmbBaowei.Text = "   无 ";
            //e.Graphics.DrawString("饱胃：" + this.cmbBaowei.Text, ptzt, Brushes.Black, new Point(x + 290, y));
            //e.Graphics.DrawLine(Pens.Black, x + 320, y1, x + 400, y1);

            //y = y + 30; y1 = y + 15;
            //if (cmbYNYWS.Text == "") cmbYNYWS.Text = "   无 ";
            //e.Graphics.DrawString("依耐性药物用药史：" + this.cmbYNYWS.Text, ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 110, y1, x + 200, y1);
            //e.Graphics.DrawString("体重：" + this.tbWeight.Text.Trim() + " Kg", ptzt, Brushes.Black, new Point(x + 220, y));
            //e.Graphics.DrawLine(Pens.Black, x + 245, y1, x + 350, y1);

            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawString("体格检查：", ptzt2, Brushes.Black, new Point(x, y));

            //y = y + 35; y1 = y + 15;
            //if (cmbJixing.Text == "") cmbJixing.Text = "   无 ";
            //e.Graphics.DrawString("身体有无畸形：" + this.cmbJixing.Text, ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 85, y1, x + 185, y1);
            //e.Graphics.DrawString("颈椎活动情况：" + this.cmbJingzhui.Text, ptzt, Brushes.Black, new Point(x + 200, y));
            //e.Graphics.DrawLine(Pens.Black, x + 280, y1, x + 380, y1);
            //if (cmbZKKN.Text == "") cmbZKKN.Text = "   无 ";
            //e.Graphics.DrawString("张口困难：" + this.cmbZKKN.Text, ptzt, Brushes.Black, new Point(x + 400, y));
            //e.Graphics.DrawLine(Pens.Black, x + 455, y1, x + 555, y1);
            //e.Graphics.DrawString("张口度：" + this.cmbZKdu.Text, ptzt, Brushes.Black, new Point(x + 580, y));
            //e.Graphics.DrawLine(Pens.Black, x + 620, y1, x + 720, y1);

            //y = y + 30; y1 = y + 15;
            //if (cmbJiaya.Text == "") cmbJiaya.Text = "   无 ";
            //e.Graphics.DrawString("假牙：" + this.cmbJiaya.Text, ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 30, y1, x + 130, y1);
            //if (cmbHuxiKN.Text == "") cmbHuxiKN.Text = "   无 ";
            //e.Graphics.DrawString("呼吸困难：" + this.cmbHuxiKN.Text, ptzt, Brushes.Black, new Point(x + 150, y));
            //e.Graphics.DrawLine(Pens.Black, x + 205, y1, x + 300, y1);           
            //e.Graphics.DrawString("气道情况： Mallampati分级：" + this.cmbMallam.Text, ptzt, Brushes.Black, new Point(x + 320, y));
            //e.Graphics.DrawLine(Pens.Black, x + 480, y1, x + 580, y1);

            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawString("病人重要器官功能、疾病情况：", ptzt2, Brushes.Black, new Point(x, y));

            //y = y + 35; y1 = y + 15;
            //e.Graphics.DrawString("心脏功能：" + this.cmbXinzang.Text, ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 55, y1, x + 150, y1);
            //if (cmbGaoxueya.Text == "") cmbGaoxueya.Text = "   无 ";
            //e.Graphics.DrawString("高血压病：" + this.cmbGaoxueya.Text, ptzt, Brushes.Black, new Point(x + 170, y));
            //e.Graphics.DrawLine(Pens.Black, x + 225, y1, x + 320, y1);
            //if (cmbGuanxinB.Text == "") cmbGuanxinB.Text = "   无 ";
            //e.Graphics.DrawString("冠心病：" + this.cmbGuanxinB.Text, ptzt, Brushes.Black, new Point(x + 340, y));
            //e.Graphics.DrawLine(Pens.Black, x + 383, y1, x + 480, y1);
            //if (cmbFeibuJB.Text == "") cmbFeibuJB.Text = "   无 ";
            //e.Graphics.DrawString("肺部疾患：" + this.cmbFeibuJB.Text, ptzt, Brushes.Black, new Point(x + 500, y));
            //e.Graphics.DrawLine(Pens.Black, x + 555, y1, x + 650, y1);

            //y = y + 30; y1 = y + 15;
            //if (tbOtherCheck.Text == "") tbOtherCheck.Text = "   无 ";
            //e.Graphics.DrawString("其他辅助检查情况：" + this.tbOtherCheck.Text, ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 110, y1, x + 740, y1);

            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawString("手术麻醉风险评估：", ptzt2, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 120, y1, x + 740, y1);
            //e.Graphics.DrawString(cmbMZFXPG.Text.Trim(), ptzt, Brushes.Black, new Point(x+125, y));

            //y = y + 35; y1 = y + 15;
            //e.Graphics.DrawString("术前麻醉医师医嘱：", ptzt2, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawString("禁食： " + this.tbJinshi.Text.Trim(), ptzt, Brushes.Black, new Point(x+120, y));
            //e.Graphics.DrawLine(Pens.Black, x + 155, y1, x + 200, y1);
            //e.Graphics.DrawString("小时； 禁饮：" + this.tbJinyin.Text.Trim(), ptzt, Brushes.Black, new Point(x + 200, y));
            //e.Graphics.DrawLine(Pens.Black, x + 273, y1, x + 320, y1);
            //e.Graphics.DrawString("小时； 其他：" + this.tbOtherZhuyi.Text.Trim(), ptzt, Brushes.Black, new Point(x + 320, y));
            //e.Graphics.DrawLine(Pens.Black, x + 395, y1, x + 740, y1);

            //y = y + 35; y1 = y + 15;
            //e.Graphics.DrawString("麻醉辅助措施：" , ptzt2, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 120, y, 12, 12);
            //e.Graphics.DrawString("控制性低血压", ptzt, Brushes.Black, new Point(x + 140, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 12, 12);
            //e.Graphics.DrawString("人工低温", ptzt, Brushes.Black, new Point(x + 260, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 12, 12);
            //e.Graphics.DrawString("中心静脉穿刺置管", ptzt, Brushes.Black, new Point(x + 380, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 520, y, 12, 12);
            //e.Graphics.DrawString("动脉穿刺置管", ptzt, Brushes.Black, new Point(x + 540, y));
            //if (cbKZDXY.Checked)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 115, y, x + 125, y + 12);
            //    e.Graphics.DrawLine(pblack2, x + 125, y + 12, x + 140, y - 3);
            //}
            //if (cbRGJW.Checked)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 235, y, x + 245, y + 12);
            //    e.Graphics.DrawLine(pblack2, x + 245, y + 12, x + 260, y - 3);
            //}
            //if (cbCVPchuanci.Checked)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 355, y, x + 365, y + 12);
            //    e.Graphics.DrawLine(pblack2, x + 365, y + 12, x + 380, y - 3);
            //}
            //if (cbDMCC.Checked)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 515, y, x + 525, y + 12);
            //    e.Graphics.DrawLine(pblack2, x + 525, y+12, x + 540, y - 3);
            //}
            //y = y + 35; y1 = y + 15;
            //e.Graphics.DrawString("麻醉医师：" + this.cmbMZYS.Text.Trim(), ptzt, Brushes.Black, new Point(x + 200, y));
            //e.Graphics.DrawLine(Pens.Black, x + 265, y1, x + 400, y1);
            //e.Graphics.DrawString("访视日期：" + this.dtVisitDate1.Text, ptzt, Brushes.Black, new Point(x + 430, y));
            //e.Graphics.DrawLine(Pens.Black, x + 490, y1, x + 660, y1);
            #endregion
            #region （以前的）术后随访
            //y = y + 50; y1 = y + 15;
            //string title2 = "麻 醉 术 后 访 视 记 录 单";
            //e.Graphics.DrawString(title2, ptzt3, Brushes.Black, new Point(x + 240, y));

            //y = y + 40; y1 = y + 15;
            //if (tbYssss.Text == "") tbYssss.Text = "   无 ";
            //e.Graphics.DrawString("实施手术名称：" + tbYssss.Text, ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 80, y1, x + 740, y1);

            //y = y + 30; y1 = y + 15;
            //if (tbMazuiFS.Text == "") tbMazuiFS.Text = "   无 ";
            //e.Graphics.DrawString("术前实施麻醉方式：" + this.tbMazuiFS.Text.Trim(), ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 110, y1, x + 740, y1);

            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawString("全身麻醉：", ptzt2, Brushes.Black, new Point(x, y));

            //y = y + 35; y1 = y + 15;
            //e.Graphics.DrawString("生命体征：  意识：" + cmbYishi.Text, ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 110, y1, x + 210, y1);
            //if (tbXueya.Text == "") tbXueya.Text = "   ";
            //e.Graphics.DrawString("血压：" + this.tbXueya.Text, ptzt, Brushes.Black, new Point(x + 230, y));
            //e.Graphics.DrawLine(Pens.Black, x + 260, y1, x + 340, y1);
            //if (tbHuxi.Text == "") tbHuxi.Text = "    ";
            //e.Graphics.DrawString("mmHg：  呼吸：" + this.tbHuxi.Text, ptzt, Brushes.Black, new Point(x + 340, y));
            //e.Graphics.DrawLine(Pens.Black, x + 420, y1, x + 500, y1);
            //e.Graphics.DrawString("次/分：  心率：" + this.tbXinlv.Text, ptzt, Brushes.Black, new Point(x + 500, y));
            //e.Graphics.DrawLine(Pens.Black, x + 580, y1, x + 660, y1);
            //e.Graphics.DrawString("次/分", ptzt, Brushes.Black, new Point(x + 660, y));

            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawString("咽喉痛", ptzt, Brushes.Black, new Point(x + 30, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 10, y, 12, 12);
            //e.Graphics.DrawString("声嘶", ptzt, Brushes.Black, new Point(x + 120, y));           
            //e.Graphics.DrawRectangle(Pens.Black, x + 100, y, 12, 12);
            //e.Graphics.DrawString("后水肿", ptzt, Brushes.Black, new Point(x + 210, y));            
            //e.Graphics.DrawRectangle(Pens.Black, x + 190, y, 12, 12);
            //e.Graphics.DrawString("肺部感染", ptzt, Brushes.Black, new Point(x + 300, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 280, y, 12, 12);
            //e.Graphics.DrawString("肺不张", ptzt, Brushes.Black, new Point(x + 390, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 370, y, 12, 12);
            //e.Graphics.DrawString("心率失常", ptzt, Brushes.Black, new Point(x + 480, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 460, y, 12, 12);
            //e.Graphics.DrawString("腹胀", ptzt, Brushes.Black, new Point(x + 570, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 550, y, 12, 12);
            //e.Graphics.DrawString("恶心呕吐", ptzt, Brushes.Black, new Point(x + 660, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 640, y, 12, 12);
            //if (cbYBZZ1.Checked)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 5, y, x + 15, y + 12);
            //    e.Graphics.DrawLine(pblack2, x + 15, y + 12, x + 30, y - 3);
            //}
            //if (cbYBZZ2.Checked)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 95, y, x + 105, y + 12);
            //    e.Graphics.DrawLine(pblack2, x + 105, y + 12, x + 115, y - 3);
            //}
            //if (cbYBZZ3.Checked)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 185, y, x + 195, y + 12);
            //    e.Graphics.DrawLine(pblack2, x + 195, y + 12, x + 205, y - 3);
            //}
            //if (cbYBZZ4.Checked)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 275, y, x + 285, y + 12);
            //    e.Graphics.DrawLine(pblack2, x + 285, y + 12, x + 295, y - 3);
            //}
            //if (cbYBZZ5.Checked)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 365, y, x + 375, y + 12);
            //    e.Graphics.DrawLine(pblack2, x + 375, y + 12, x + 385, y - 3);
            //}
            //if (cbYBZZ6.Checked)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 455, y, x + 465, y + 12);
            //    e.Graphics.DrawLine(pblack2, x + 475, y + 12, x + 485, y - 3);
            //} 
            //if (cbYBZZ7.Checked)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 545, y, x + 555, y + 12);
            //    e.Graphics.DrawLine(pblack2, x + 555, y + 12, x + 565, y - 3);
            //}
            //if (cbYBZZ8.Checked)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 635, y, x + 645, y + 12);
            //    e.Graphics.DrawLine(pblack2, x + 645, y + 12, x + 655, y - 3);
            //}
            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawString("其他：", ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 30, y1, x + 740, y1);

            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawString("神经阻滞麻醉及椎管内麻醉：", ptzt2, Brushes.Black, new Point(x, y));

            //y = y + 35; y1 = y + 15;
            //e.Graphics.DrawString("生命体征：  意识：" + cmbYishiSJ.Text, ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 110, y1, x + 210, y1);
            //if (tbXueyaSJ.Text == "") tbXueyaSJ.Text = "   ";
            //e.Graphics.DrawString("血压：" + this.tbXueyaSJ.Text, ptzt, Brushes.Black, new Point(x + 230, y));
            //e.Graphics.DrawLine(Pens.Black, x + 260, y1, x + 340, y1);
            //if (tbHuxiSJ.Text == "") tbHuxiSJ.Text = "    ";
            //e.Graphics.DrawString("mmHg：  呼吸：" + this.tbHuxiSJ.Text, ptzt, Brushes.Black, new Point(x + 340, y));
            //e.Graphics.DrawLine(Pens.Black, x + 420, y1, x + 500, y1);
            //e.Graphics.DrawString("次/分：  心率：" + this.tbXinlvSJ.Text, ptzt, Brushes.Black, new Point(x + 500, y));
            //e.Graphics.DrawLine(Pens.Black, x + 580, y1, x + 660, y1);
            //e.Graphics.DrawString("次/分", ptzt, Brushes.Black, new Point(x + 660, y));

            //y = y + 30; y1 = y + 15;
            //if (cmbExin.Text == "") cmbExin.Text = "   无 ";
            //e.Graphics.DrawString("恶心：" + this.cmbExin.Text, ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 30, y1, x + 130, y1);
            //if (cmbChuanciBW.Text == "") cmbChuanciBW.Text = "  正常";
            //e.Graphics.DrawString("穿刺部位：" + this.cmbChuanciBW.Text, ptzt, Brushes.Black, new Point(x + 150, y));
            //e.Graphics.DrawLine(Pens.Black, x + 205, y1, x + 300, y1);
            //if (cmbZhitiHD.Text == "") cmbZhitiHD.Text = "  正常";
            //e.Graphics.DrawString("肢体活动：" + this.cmbZhitiHD.Text, ptzt, Brushes.Black, new Point(x + 320, y));
            //e.Graphics.DrawLine(Pens.Black, x + 380, y1, x + 460, y1);
            //if (cmbShuhouZT.Text == "") cmbShuhouZT.Text = "  无";
            //e.Graphics.DrawString("术后镇痛方式：" + this.cmbShuhouZT.Text, ptzt, Brushes.Black, new Point(x + 480, y));
            //e.Graphics.DrawLine(Pens.Black, x + 560, y1, x + 720, y1);

            //y = y + 30; y1 = y + 15;            
            //e.Graphics.DrawString("镇痛泵配方：" + this.tbZTBPF.Text.Trim(), ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 62, y1, x + 740, y1);

            //y = y + 30; y1 = y + 15;
            //if (cmbZTXG.Text == "") cmbZTXG.Text = "   优 ";
            //e.Graphics.DrawString("镇痛效果：" + this.cmbZTXG.Text, ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 65, y1, x + 400, y1);

            //y = y + 30; y1 = y + 15;
            //if (tbRemark.Text.Trim() == "") tbRemark.Text = "   优 ";
            //e.Graphics.DrawString("备注： " + tbRemark.Text.Trim(), ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawLine(Pens.Black, x + 30, y1, x + 740, y1);

            //y = y + 35; y1 = y + 15;
            //e.Graphics.DrawString("麻醉医师：" + this.cmbMZYS1.Text.Trim(), ptzt, Brushes.Black, new Point(x + 200, y));
            //e.Graphics.DrawLine(Pens.Black, x + 265, y1, x + 400, y1);
            //e.Graphics.DrawString("访视日期：" + this.dtVisitDate1.Text, ptzt, Brushes.Black, new Point(x + 430, y));
            //e.Graphics.DrawLine(Pens.Black, x + 490, y1, x + 660, y1);


            #endregion
            #region 麻醉会诊单
            Pen ptp = Pens.Black;//普通画笔
            Pen pblue2 = new Pen(Brushes.Blue);
            pblue2.Width = 2;
            Pen pblack = new Pen(Brushes.Black);
            Pen pblack2 = new Pen(Brushes.Black, 2);
            //Font ptzt = new Font(new FontFamily("宋体"), 9);
            //Font ptzt2 = new Font(new FontFamily("黑体"), 9);
            //Font ptzt3 = new Font(new FontFamily("宋体"), 14);
            Font ptzt = new Font("宋体", 10);//普通字体
            Font ptzt2 = new Font("黑体", 10, FontStyle.Bold);//加粗十号
            Font ptzt3 = new Font("新宋体", 14, FontStyle.Bold);//加粗14号
            int y = 30; int x = 0; int y1= 0;
            string title = "昌  吉  回  族  自  治  州  人  民  医  院";
            e.Graphics.DrawString(title, ptzt3, Brushes.Black, new Point(x + 120, y)); 
            y = y + 40; y1 = y + 15;
            string title1 = "麻  醉  会  诊  单";
            e.Graphics.DrawString(title1, ptzt3, Brushes.Black, new Point(x + 240, y));
            y = y + 50; y1 = y + 15;
            e.Graphics.DrawString("患者姓名： " + tbPatname.Text + "  性别： " + cmbSex.Text + "  年龄： " + tbAge.Text + "  科别: " + tbBingqu.Text + "  床号 " + tbBedNO.Text + "  住院号： " + tbZhuyuanID.Text, ptzt, Brushes.Black, new Point(x + 50, y));
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("术前诊断:           " + tbSqzd1.Text, ptzt, Brushes.Black, new Point(x + 50, y));
            e.Graphics.DrawLine(Pens.Black, x + 120, y1,750, y1);
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("拟行手术名称:           " + tbShoushuMC.Text, ptzt, Brushes.Black, new Point(x + 50, y));
            e.Graphics.DrawLine(Pens.Black, x + 150, y1, 750, y1);
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x + 200, y, 12, 12);
            e.Graphics.DrawString("选择性手术 ", ptzt, Brushes.Black, x + 220, y);
            if (ckXZX.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 195, y, x + 205, y + 12);
                e.Graphics.DrawLine(pblack2, x + 205, y + 12, x + 220, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 300, y, 12, 12);
            e.Graphics.DrawString("急诊手术 ", ptzt, Brushes.Black, x + 320, y);
            if (ckXZX.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 295, y, x + 305, y + 12);
                e.Graphics.DrawLine(pblack2, x + 305, y + 12, x + 320, y - 3);
            }
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("术前情况:           " + txtSQQk.Text,   ptzt, Brushes.Black, new Point(x + 50, y));
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("一般情况:           " + txtYBQk.Text, ptzt, Brushes.Black, new Point(x + 50, y));
            y = y + 25; y1 = y + 15;          
            e.Graphics.DrawString("T:"+txtTW.Text, ptzt, Brushes.Black, x + 50, y);
            e.Graphics.DrawString("°C  BP:" + tbGXY_High_up.Text, ptzt, Brushes.Black, x + 90, y);
            e.Graphics.DrawString("mmHg  R：" + txtHX.Text, ptzt, Brushes.Black, x + 220, y);
            e.Graphics.DrawString("次/分  P：" + txtMB.Text, ptzt, Brushes.Black, x + 320, y);
            e.Graphics.DrawString("次/分  体重：" + txtTZ.Text+"   Kg",ptzt, Brushes.Black, x + 420, y);
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("血红素:" + txtXSS.Text, ptzt, Brushes.Black, x + 50, y);
            e.Graphics.DrawString("g/L  红细胞压积：" + txtHXBYJ.Text, ptzt, Brushes.Black, x + 150, y);
            e.Graphics.DrawString("%  血钾：" + txtXJ.Text, ptzt, Brushes.Black, x + 300, y);
            e.Graphics.DrawString("mmol/L  血钠：" + txtXN.Text+"   mmol/L", ptzt, Brushes.Black, x + 400, y);
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("血糖:" + txtXT.Text+"    mmol/L", ptzt, Brushes.Black, x + 50, y);
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("心脏功能:" , ptzt, Brushes.Black, x + 50, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 120, y, 12, 12);
            e.Graphics.DrawString("1 ", ptzt, Brushes.Black, x + 140, y);
            if (cmbShoushushi.Text=="1级")
            {
                e.Graphics.DrawLine(pblack2, x + 115, y, x + 125, y + 12);
                e.Graphics.DrawLine(pblack2, x + 125, y + 12, x + 140, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 160, y, 12, 12);
            e.Graphics.DrawString("2 ", ptzt, Brushes.Black, x + 180, y);
            if (cmbShoushushi.Text == "2级")
            {
                e.Graphics.DrawLine(pblack2, x + 155, y, x + 165, y + 12);
                e.Graphics.DrawLine(pblack2, x + 165, y + 12, x + 180, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 200, y, 12, 12);
            e.Graphics.DrawString("3 ", ptzt, Brushes.Black, x + 220, y);
            if (cmbShoushushi.Text == "3级")
            {
                e.Graphics.DrawLine(pblack2, x + 195, y, x + 205, y + 12);
                e.Graphics.DrawLine(pblack2, x + 205, y + 12, x + 220, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 12, 12);
            e.Graphics.DrawString("4级 ", ptzt, Brushes.Black, x + 260, y);
            if (cmbShoushushi.Text == "4级")
            {
                e.Graphics.DrawLine(pblack2, x + 235, y, x + 245, y + 12);
                e.Graphics.DrawLine(pblack2, x + 245, y + 12, x + 260, y - 3);
            }
            e.Graphics.DrawString("肺功能（", ptzt, Brushes.Black, x + 300, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 12, 12);
            e.Graphics.DrawString("正常 ", ptzt, Brushes.Black, x + 380, y);
            if (cmbFGN.Text == "正常")
            {
                e.Graphics.DrawLine(pblack2, x + 355, y, x + 365, y + 12);
                e.Graphics.DrawLine(pblack2, x + 365, y + 12, x + 380, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 420, y, 12, 12);
            e.Graphics.DrawString("异常） ", ptzt, Brushes.Black, x + 440, y);
            if (cmbFGN.Text == "异常")
            {
                e.Graphics.DrawLine(pblack2, x + 415, y, x + 425, y + 12);
                e.Graphics.DrawLine(pblack2, x + 425, y + 12, x + 440, y - 3);
            }
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("肾功能（", ptzt, Brushes.Black, x + 50, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 110, y, 12, 12);
            e.Graphics.DrawString("正常 ", ptzt, Brushes.Black, x + 130, y);
            if (cmbSGN.Text == "正常")
            {
                e.Graphics.DrawLine(pblack2, x + 105, y, x + 115, y + 12);
                e.Graphics.DrawLine(pblack2, x + 115, y + 12, x + 130, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 170, y, 12, 12);
            e.Graphics.DrawString("异常） ", ptzt, Brushes.Black, x + 190, y);
            if (cmbSGN.Text == "异常")
            {
                e.Graphics.DrawLine(pblack2, x + 165, y, x + 175, y + 12);
                e.Graphics.DrawLine(pblack2, x + 175, y + 12, x + 190, y - 3);
            }
            e.Graphics.DrawString("肝功能（", ptzt, Brushes.Black, x + 300, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 12, 12);
            e.Graphics.DrawString("正常 ", ptzt, Brushes.Black, x + 380, y);
            if (cmbGGN.Text == "正常")
            {
                e.Graphics.DrawLine(pblack2, x + 355, y, x + 365, y + 12);
                e.Graphics.DrawLine(pblack2, x + 365, y + 12, x + 380, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 420, y, 12, 12);
            e.Graphics.DrawString("异常） ", ptzt, Brushes.Black, x + 440, y);
            if (cmbGGN.Text == "异常")
            {
                e.Graphics.DrawLine(pblack2, x + 415, y, x + 425, y + 12);
                e.Graphics.DrawLine(pblack2, x + 425, y + 12, x + 440, y - 3);
            }
            y = y + 25; y1 = y + 15;
            if (txtTSQK.Text==""||txtTSQK.Text=="无")
            {
                e.Graphics.DrawString("特殊情况：无（高血压、心脏病、糖尿病、血液病、肾病、肝病、休克、脱水）", ptzt, Brushes.Black, x + 50, y);
            }
            else
            {
                e.Graphics.DrawString("特殊情况：" + txtTSQK.Text, ptzt, Brushes.Black, x + 50, y);
            }
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("其他检查异常指标：" + txtQTYC.Text, ptzt, Brushes.Black, x + 50, y);
            e.Graphics.DrawLine(Pens.Black, x + 170, y1, 750, y1);
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("医师签字：" + cmbmZHZYY.Text, ptzt, Brushes.Black, x + 50, y);
            e.Graphics.DrawLine(Pens.Black, x + 110, y1, 300, y1);
            e.Graphics.DrawString("申请日期：" + dtVisitDate1.Text, ptzt, Brushes.Black, x + 350, y);
            e.Graphics.DrawLine(Pens.Black, x + 410, y1, 700, y1);

            #endregion
            #region 麻醉术前访视单
            y = y + 80; y1 = y + 15;
            string title2 = "麻  醉  术  前  访  视  记  录";
            e.Graphics.DrawString(title2, ptzt3, Brushes.Black, new Point(x + 200, y));
            y = y + 50; y1 = y + 15;
            e.Graphics.DrawString("拟行麻醉方式：", ptzt2, Brushes.Black, x + 50, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 12, 12);
            e.Graphics.DrawString("全身麻醉；", ptzt, Brushes.Black, x + 170, y);
            if (cbMZFF1.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 145, y, x + 155, y + 12);
                e.Graphics.DrawLine(pblack2, x + 155, y + 12, x + 170, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 12, 12);
            e.Graphics.DrawString("全麻+椎管内麻醉；", ptzt, Brushes.Black, x + 260, y);
            if (cbMZFF2.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 235, y, x + 245, y + 12);
                e.Graphics.DrawLine(pblack2, x + 245, y + 12, x + 260, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 380, y, 12, 12);
            e.Graphics.DrawString("椎管内麻醉；", ptzt, Brushes.Black, x + 400, y);
            if (cbMZFF3.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 375, y, x + 385, y + 12);
                e.Graphics.DrawLine(pblack2, x + 385, y + 12, x + 400, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 480, y, 12, 12);
            e.Graphics.DrawString("神经阻滞；", ptzt, Brushes.Black, x + 500, y);
            if (cbMZFF4.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 475, y, x + 485, y + 12);
                e.Graphics.DrawLine(pblack2, x + 485, y + 12, x + 500, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 570, y, 12, 12);
            e.Graphics.DrawString("联合麻醉；", ptzt, Brushes.Black, x + 590, y);
            if (cbMZFF5.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 565, y, x + 575, y + 12);
                e.Graphics.DrawLine(pblack2, x + 575, y + 12, x + 590, y - 3);
            }
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("一般情况：", ptzt2, Brushes.Black, x + 50, y);
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("病人体格情况：(ASA分级):", ptzt, Brushes.Black, x + 50, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 230, y, 12, 12);
            e.Graphics.DrawString("Ⅰ", ptzt, Brushes.Black, x + 250, y);
            if (cmbASA.Text == "Ⅰ")
            {
                e.Graphics.DrawLine(pblack2, x + 225, y, x + 235, y + 12);
                e.Graphics.DrawLine(pblack2, x + 235, y + 12, x + 250, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 270, y, 12, 12);
            e.Graphics.DrawString("Ⅱ", ptzt, Brushes.Black, x + 290, y);
            if (cmbASA.Text == "Ⅱ")
            {
                e.Graphics.DrawLine(pblack2, x + 265, y, x + 275, y + 12);
                e.Graphics.DrawLine(pblack2, x + 275, y + 12, x + 290, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 320, y, 12, 12);
            e.Graphics.DrawString("Ⅲ", ptzt, Brushes.Black, x + 340, y);
            if (cmbASA.Text == "Ⅲ")
            {
                e.Graphics.DrawLine(pblack2, x + 315, y, x + 325, y + 12);
                e.Graphics.DrawLine(pblack2, x + 325, y + 12, x + 340, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 370, y, 12, 12);
            e.Graphics.DrawString("Ⅳ", ptzt, Brushes.Black, x + 390, y);
            if (cmbASA.Text == "Ⅳ")
            {
                e.Graphics.DrawLine(pblack2, x + 365, y, x + 375, y + 12);
                e.Graphics.DrawLine(pblack2, x + 375, y + 12, x + 390, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 420, y, 12, 12);
            e.Graphics.DrawString("Ⅴ", ptzt, Brushes.Black, x + 440, y);
            if (cmbASA.Text == "Ⅴ")
            {
                e.Graphics.DrawLine(pblack2, x + 415, y, x + 425, y + 12);
                e.Graphics.DrawLine(pblack2, x + 425, y + 12, x + 440, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 470, y, 12, 12);
            e.Graphics.DrawString("E", ptzt, Brushes.Black, x + 490, y);
            if (cbJizhen.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 465, y, x + 475, y + 12);
                e.Graphics.DrawLine(pblack2, x + 475, y + 12, x + 490, y - 3);
            }
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("肥胖:（", ptzt, Brushes.Black, x + 50, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 110, y, 12, 12);
            e.Graphics.DrawString("是", ptzt, Brushes.Black, x + 130, y);
            if (cmbFeiPang.Text=="是")
            {
                e.Graphics.DrawLine(pblack2, x + 105, y, x + 115, y + 12);
                e.Graphics.DrawLine(pblack2, x + 115, y + 12, x + 130, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 160, y, 12, 12);
            e.Graphics.DrawString("否 ）", ptzt, Brushes.Black, x + 180, y);
            if (cmbFeiPang.Text == "否")
            {
                e.Graphics.DrawLine(pblack2, x + 155, y, x + 165, y + 12);
                e.Graphics.DrawLine(pblack2, x + 165, y + 12, x + 180, y - 3);
            }
            e.Graphics.DrawString("饱胃:", ptzt, Brushes.Black, x + 280, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 330, y, 12, 12);
            e.Graphics.DrawString("是 ", ptzt, Brushes.Black, x + 350, y);
            if (cmbBaowei.Text == "是")
            {
                e.Graphics.DrawLine(pblack2, x + 325, y, x + 335, y + 12);
                e.Graphics.DrawLine(pblack2, x + 335, y + 12, x + 350, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 380, y, 12, 12);
            e.Graphics.DrawString("否", ptzt, Brushes.Black, x + 400, y);
            if (cmbBaowei.Text == "否")
            {
                e.Graphics.DrawLine(pblack2, x + 375, y, x + 385, y + 12);
                e.Graphics.DrawLine(pblack2, x + 385, y + 12, x + 400, y - 3);
            }
            e.Graphics.DrawString("体重:" + this.tbWeight.Text, ptzt, Brushes.Black, x + 500, y);
            e.Graphics.DrawString("kg", ptzt, Brushes.Black, x + 560, y);
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("依赖性药物用药史:（", ptzt, Brushes.Black, x + 50, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 190, y, 12, 12);
            e.Graphics.DrawString("有 ", ptzt, Brushes.Black, x + 210, y);
            if (cmbYNYWS.Text == "有")
            {
                e.Graphics.DrawLine(pblack2, x + 185, y, x + 195, y + 12);
                e.Graphics.DrawLine(pblack2, x + 195, y + 12, x + 210, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 12, 12);
            e.Graphics.DrawString("无 ）", ptzt, Brushes.Black, x + 260, y);
            if (cmbYNYWS.Text == "无")
            {
                e.Graphics.DrawLine(pblack2, x + 235, y, x + 245, y + 12);
                e.Graphics.DrawLine(pblack2, x + 245, y + 12, x + 260, y - 3);
            }
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("体格检查：", ptzt2, Brushes.Black, x + 50, y);
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("身体有无畸形:（", ptzt, Brushes.Black, x + 50, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 160, y, 12, 12);
            e.Graphics.DrawString("有 ", ptzt, Brushes.Black, x + 180, y);
            if (cmbJixing.Text == "有")
            {
                e.Graphics.DrawLine(pblack2, x + 155, y, x + 165, y + 12);
                e.Graphics.DrawLine(pblack2, x + 165, y + 12, x + 180, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 210, y, 12, 12);
            e.Graphics.DrawString("无 ）", ptzt, Brushes.Black, x + 230, y);
            if (cmbJixing.Text == "无")
            {
                e.Graphics.DrawLine(pblack2, x + 205, y, x + 215, y + 12);
                e.Graphics.DrawLine(pblack2, x + 215, y + 12, x + 230, y - 3);
            }
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("颈椎活动情况:（", ptzt, Brushes.Black, x + 50, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 160, y, 12, 12);
            e.Graphics.DrawString("正常 ", ptzt, Brushes.Black, x + 180, y);
            if (cmbJingzhui.Text == "正常")
            {
                e.Graphics.DrawLine(pblack2, x + 155, y, x + 165, y + 12);
                e.Graphics.DrawLine(pblack2, x + 165, y + 12, x + 180, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 220, y, 12, 12);
            e.Graphics.DrawString("异常 ）", ptzt, Brushes.Black, x + 240, y);
            if (cmbJingzhui.Text == "异常")
            {
                e.Graphics.DrawLine(pblack2, x + 215, y, x + 225, y + 12);
                e.Graphics.DrawLine(pblack2, x + 225, y + 12, x + 240, y - 3);
            }
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("张口困难:（", ptzt, Brushes.Black, x + 50, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 140, y, 12, 12);
            e.Graphics.DrawString("有 ", ptzt, Brushes.Black, x + 160, y);
            if (cmbZKKN.Text == "有")
            {
                e.Graphics.DrawLine(pblack2, x + 135, y, x + 145, y + 12);
                e.Graphics.DrawLine(pblack2, x + 145, y + 12, x + 160, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 190, y, 12, 12);
            e.Graphics.DrawString("无 ）", ptzt, Brushes.Black, x + 210, y);
            if (cmbZKKN.Text == "无")
            {
                e.Graphics.DrawLine(pblack2, x + 185, y, x + 195, y + 12);
                e.Graphics.DrawLine(pblack2, x + 195, y + 12, x + 210, y - 3);
            }
            e.Graphics.DrawString("张口度:（", ptzt, Brushes.Black, x + 260, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 330, y, 12, 12);
            e.Graphics.DrawString("1指", ptzt, Brushes.Black, x + 350, y);
            if (cmbZKdu.Text == "1指")
            {
                e.Graphics.DrawLine(pblack2, x + 325, y, x + 335, y + 12);
                e.Graphics.DrawLine(pblack2, x + 335, y + 12, x + 350, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 380, y, 12, 12);
            e.Graphics.DrawString("2指", ptzt, Brushes.Black, x + 400, y);
            if (cmbZKdu.Text == "2指")
            {
                e.Graphics.DrawLine(pblack2, x + 375, y, x + 385, y + 12);
                e.Graphics.DrawLine(pblack2, x + 385, y + 12, x + 400, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 430, y, 12, 12);
            e.Graphics.DrawString("3指）", ptzt, Brushes.Black, x + 450, y);
            if (cmbZKdu.Text == "3指")
            {
                e.Graphics.DrawLine(pblack2, x + 425, y, x + 435, y + 12);
                e.Graphics.DrawLine(pblack2, x + 435, y + 12, x + 450, y - 3);
            }
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("假牙:（", ptzt, Brushes.Black, x + 50, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 120, y, 12, 12);         
            e.Graphics.DrawString("有 ", ptzt, Brushes.Black, x + 140, y);
            if (cmbJiaya.Text == "有")
            {
                e.Graphics.DrawLine(pblack2, x + 115, y, x + 125, y + 12);
                e.Graphics.DrawLine(pblack2, x + 125, y + 12, x + 140, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 170, y, 12, 12);
            e.Graphics.DrawString("无 ）", ptzt, Brushes.Black, x + 190, y);
            if (cmbJiaya.Text == "无")
            {
                e.Graphics.DrawLine(pblack2, x + 165, y, x + 175, y + 12);
                e.Graphics.DrawLine(pblack2, x + 175, y + 12, x + 190, y - 3);
            }
            e.Graphics.DrawString("呼吸困难:（", ptzt, Brushes.Black, x + 240, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 330, y, 12, 12);
            e.Graphics.DrawString("有 ", ptzt, Brushes.Black, x + 350, y);
            if (comboBox28.Text == "有")
            {
                e.Graphics.DrawLine(pblack2, x + 325, y, x + 335, y + 12);
                e.Graphics.DrawLine(pblack2, x + 335, y + 12, x + 350, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 380, y, 12, 12);
            e.Graphics.DrawString("无 ）", ptzt, Brushes.Black, x + 400, y);
            if (comboBox28.Text == "无")
            {
                e.Graphics.DrawLine(pblack2, x + 375, y, x + 385, y + 12);
                e.Graphics.DrawLine(pblack2, x + 385, y + 12, x + 400, y - 3);
            }
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("气道情况:Mallampati分级:（", ptzt, Brushes.Black, x + 50, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 250, y, 12, 12);
            e.Graphics.DrawString("Ⅰ", ptzt, Brushes.Black, x + 270, y);
            if (cmbMallam.Text == "Ⅰ")
            {
                e.Graphics.DrawLine(pblack2, x + 245, y, x + 255, y + 12);
                e.Graphics.DrawLine(pblack2, x + 255, y + 12, x + 270, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 290, y, 12, 12);
            e.Graphics.DrawString("Ⅱ", ptzt, Brushes.Black, x + 310, y);
            if (cmbMallam.Text == "Ⅱ")
            {
                e.Graphics.DrawLine(pblack2, x + 285, y, x + 295, y + 12);
                e.Graphics.DrawLine(pblack2, x + 295, y + 12, x + 310, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 340, y, 12, 12);
            e.Graphics.DrawString("Ⅲ", ptzt, Brushes.Black, x + 360, y);
            if (cmbMallam.Text == "Ⅲ")
            {
                e.Graphics.DrawLine(pblack2, x + 335, y, x + 345, y + 12);
                e.Graphics.DrawLine(pblack2, x + 345, y + 12, x + 360, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 390, y, 12, 12);
            e.Graphics.DrawString("Ⅳ）", ptzt, Brushes.Black, x + 410, y);
            if (cmbMallam.Text == "Ⅳ")
            {
                e.Graphics.DrawLine(pblack2, x + 385, y, x + 395, y + 12);
                e.Graphics.DrawLine(pblack2, x + 395, y + 12, x + 410, y - 3);
            }
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("病人重要器官功能、疾病情况：", ptzt2, Brushes.Black, x + 50, y);
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("心脏功能:", ptzt, Brushes.Black, x + 50, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 120, y, 12, 12);
            e.Graphics.DrawString("1 ", ptzt, Brushes.Black, x + 140, y);
            if (cmbXinzang.Text == "1级")
            {
                e.Graphics.DrawLine(pblack2, x + 115, y, x + 125, y + 12);
                e.Graphics.DrawLine(pblack2, x + 125, y + 12, x + 140, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 160, y, 12, 12);
            e.Graphics.DrawString("2 ", ptzt, Brushes.Black, x + 180, y);
            if (cmbXinzang.Text == "2级")
            {
                e.Graphics.DrawLine(pblack2, x + 155, y, x + 165, y + 12);
                e.Graphics.DrawLine(pblack2, x + 165, y + 12, x + 180, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 200, y, 12, 12);
            e.Graphics.DrawString("3 ", ptzt, Brushes.Black, x + 220, y);
            if (cmbXinzang.Text == "3级")
            {
                e.Graphics.DrawLine(pblack2, x + 195, y, x + 205, y + 12);
                e.Graphics.DrawLine(pblack2, x + 205, y + 12, x + 220, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 12, 12);
            e.Graphics.DrawString("4级", ptzt, Brushes.Black, x + 260, y);
            if (cmbXinzang.Text == "4级")
            {
                e.Graphics.DrawLine(pblack2, x + 235, y, x + 245, y + 12);
                e.Graphics.DrawLine(pblack2, x + 245, y + 12, x + 260, y - 3);
            }
            e.Graphics.DrawString("高血压（", ptzt, Brushes.Black, x + 300, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 12, 12);
            e.Graphics.DrawString("无 ", ptzt, Brushes.Black, x + 380, y);
            if (cmbGaoxueya.Text == "无")
            {
                e.Graphics.DrawLine(pblack2, x + 355, y, x + 365, y + 12);
                e.Graphics.DrawLine(pblack2, x + 365, y + 12, x + 380, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 410, y, 12, 12);
            e.Graphics.DrawString("有） ", ptzt, Brushes.Black, x + 430, y);
            if (cmbGaoxueya.Text == "有")
            {
                e.Graphics.DrawLine(pblack2, x + 405, y, x + 415, y + 12);
                e.Graphics.DrawLine(pblack2, x + 415, y + 12, x + 430, y - 3);
            }
            e.Graphics.DrawString("冠心病（", ptzt, Brushes.Black, x + 490, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 550, y, 12, 12);
            e.Graphics.DrawString("有 ", ptzt, Brushes.Black, x + 570, y);
            if (cmbGuanxinB.Text == "有")
            {
                e.Graphics.DrawLine(pblack2, x + 545, y, x + 555, y + 12);
                e.Graphics.DrawLine(pblack2, x + 555, y + 12, x + 570, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 600, y, 12, 12);
            e.Graphics.DrawString("无） ", ptzt, Brushes.Black, x + 620, y);
            if (cmbGuanxinB.Text == "无")
            {
                e.Graphics.DrawLine(pblack2, x + 595, y, x + 605, y + 12);
                e.Graphics.DrawLine(pblack2, x + 605, y + 12, x + 620, y - 3);
            }
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("肺功能（", ptzt, Brushes.Black, x + 50, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 110, y, 12, 12);
            e.Graphics.DrawString("正常 ", ptzt, Brushes.Black, x + 130, y);
            if (cmbFeiGN.Text == "正常")
            {
                e.Graphics.DrawLine(pblack2, x + 105, y, x + 115, y + 12);
                e.Graphics.DrawLine(pblack2, x + 115, y + 12, x + 130, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 170, y, 12, 12);
            e.Graphics.DrawString("异常） ", ptzt, Brushes.Black, x + 190, y);
            if (cmbFeiGN.Text == "异常")
            {
                e.Graphics.DrawLine(pblack2, x + 165, y, x + 175, y + 12);
                e.Graphics.DrawLine(pblack2, x + 175, y + 12, x + 190, y - 3);
            }
            e.Graphics.DrawString("肺部疾病（", ptzt, Brushes.Black, x + 300, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 370, y, 12, 12);
            e.Graphics.DrawString("无 ", ptzt, Brushes.Black, x + 390, y);
            if (cmbFeibuJB.Text == "无")
            {
                e.Graphics.DrawLine(pblack2, x + 365, y, x + 375, y + 12);
                e.Graphics.DrawLine(pblack2, x + 375, y + 12, x + 390, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 420, y, 12, 12);
            e.Graphics.DrawString("有）", ptzt, Brushes.Black, x + 440, y);
            if (cmbFeibuJB.Text == "有")
            {
                e.Graphics.DrawLine(pblack2, x + 415, y, x + 425, y + 12);
                e.Graphics.DrawLine(pblack2, x + 425, y + 12, x + 440, y - 3);
            }
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("肝功能（", ptzt, Brushes.Black, x + 50, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 110, y, 12, 12);
            e.Graphics.DrawString("正常 ", ptzt, Brushes.Black, x + 130, y);
            if (cmbGanGN.Text == "正常")
            {
                e.Graphics.DrawLine(pblack2, x + 105, y, x + 115, y + 12);
                e.Graphics.DrawLine(pblack2, x + 115, y + 12, x + 130, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 170, y, 12, 12);
            e.Graphics.DrawString("异常） ", ptzt, Brushes.Black, x + 190, y);
            if (cmbGanGN.Text == "异常")
            {
                e.Graphics.DrawLine(pblack2, x + 165, y, x + 175, y + 12);
                e.Graphics.DrawLine(pblack2, x + 175, y + 12, x + 190, y - 3);
            }
            e.Graphics.DrawString("肾功能（", ptzt, Brushes.Black, x + 300, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 12, 12);
            e.Graphics.DrawString("正常 ", ptzt, Brushes.Black, x + 380, y);
            if (cmbShenGN.Text == "正常")
            {
                e.Graphics.DrawLine(pblack2, x + 355, y, x + 365, y + 12);
                e.Graphics.DrawLine(pblack2, x + 365, y + 12, x + 380, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 420, y, 12, 12);
            e.Graphics.DrawString("异常） ", ptzt, Brushes.Black, x + 440, y);
            if (cmbShenGN.Text == "异常")
            {
                e.Graphics.DrawLine(pblack2, x + 415, y, x + 425, y + 12);
                e.Graphics.DrawLine(pblack2, x + 425, y + 12, x + 440, y - 3);
            }
            e.Graphics.DrawString("神级系统疾病（", ptzt, Brushes.Black, x + 490, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 590, y, 12, 12);
            e.Graphics.DrawString("无 ", ptzt, Brushes.Black, x + 610, y);
            if (cmbFeibuJB.Text == "无")
            {
                e.Graphics.DrawLine(pblack2, x + 585, y, x + 595, y + 12);
                e.Graphics.DrawLine(pblack2, x + 595, y + 12, x + 610, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 640, y, 12, 12);
            e.Graphics.DrawString("有）", ptzt, Brushes.Black, x + 660, y);
            if (cmbFeibuJB.Text == "有")
            {
                e.Graphics.DrawLine(pblack2, x + 635, y, x + 645, y + 12);
                e.Graphics.DrawLine(pblack2, x + 645, y + 12, x + 660, y - 3);
            }
            y = y + 25; y1 = y + 15;
            if (tbOtherCheck.Text == "") tbOtherCheck.Text = "   无 ";
            e.Graphics.DrawString("其他辅助检查情况：" + this.tbOtherCheck.Text, ptzt, Brushes.Black, new Point(x+50, y));
            e.Graphics.DrawLine(Pens.Black, x + 170, y1, x + 740, y1);
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("术前麻醉医师医嘱：", ptzt2, Brushes.Black, new Point(x+50, y));
            e.Graphics.DrawString("禁食： " + this.tbJinshi.Text.Trim(), ptzt, Brushes.Black, new Point(x + 170, y));
            e.Graphics.DrawLine(Pens.Black, x + 205, y1, x + 250, y1);
            e.Graphics.DrawString("小时； 禁饮：" + this.tbJinyin.Text.Trim(), ptzt, Brushes.Black, new Point(x + 250, y));
            e.Graphics.DrawLine(Pens.Black, x + 330, y1, x + 370, y1);
            e.Graphics.DrawString("小时； 其他：" + this.tbOtherZhuyi.Text.Trim(), ptzt, Brushes.Black, new Point(x + 370, y));
            e.Graphics.DrawLine(Pens.Black, x + 450, y1, x + 740, y1);

            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("手术麻醉风险评估：", ptzt2, Brushes.Black, new Point(x+50, y));
            e.Graphics.DrawLine(Pens.Black, x + 180, y1, x + 450, y1);
            e.Graphics.DrawString(cmbMZFXPG.Text.Trim(), ptzt, Brushes.Black, new Point(x + 175, y));
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("麻醉医师：" + cmbMZYS.Text, ptzt, Brushes.Black, x + 50, y);
            e.Graphics.DrawLine(Pens.Black, x + 110, y1, 250, y1);
            e.Graphics.DrawString(dtApplyDate.Text, ptzt, Brushes.Black, x + 500, y);
            e.Graphics.DrawLine(Pens.Black, x + 500, y1, 700, y1);
           
          
            #endregion
        }

        private void printDocument2_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog2.PrintPreviewControl.Zoom = 1.0;
            printPreviewDialog2.WindowState = FormWindowState.Maximized;
            //printDocument1.DefaultPageSettings.PaperSize =
            //           new System.Drawing.Printing.PaperSize("K16", 740, 1020);  
            printDocument2.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("A4", 820, 1160);  
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            #region 麻醉计划单           
            Pen ptp = Pens.Black;//普通画笔
            Pen pblue2 = new Pen(Brushes.Blue);
            pblue2.Width = 2;
            Pen pblack = new Pen(Brushes.Black);
            Pen pblack2 = new Pen(Brushes.Black, 2);
            //Font ptzt = new Font(new FontFamily("宋体"), 9);
            //Font ptzt2 = new Font(new FontFamily("黑体"), 9);
            //Font ptzt3 = new Font(new FontFamily("宋体"), 14);
            Font ptzt = new Font("宋体", 10);//普通字体
            Font ptzt2 = new Font("黑体", 10, FontStyle.Bold);//加粗十号
            Font ptzt3 = new Font("新宋体", 14, FontStyle.Bold);//加粗14号
            int y = 30; int x = 0; int y1 = 0;
            string title = "昌  吉  回  族  自  治  州  人  民  医  院";
            e.Graphics.DrawString(title, ptzt3, Brushes.Black, new Point(x + 180, y));
            y = y + 40; y1 = y + 15;
            string title1 = "麻  醉  计  划  单";
            e.Graphics.DrawString(title1, ptzt3, Brushes.Black, new Point(x + 300, y));
            y = y + 50; y1 = y + 15;
            e.Graphics.DrawString("患者姓名： " + tbPatname.Text + "  性别： " + cmbSex.Text + "  年龄： " + tbAge.Text + "  科别: " + tbBingqu.Text + "  床号 " + tbBedNO.Text + "  住院号： " + tbZhuyuanID.Text, ptzt, Brushes.Black, new Point(x + 50, y));
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("术前诊断:           " + txtLCZD.Text, ptzt, Brushes.Black, new Point(x + 50, y));
            e.Graphics.DrawLine(Pens.Black, x + 120, y1, 750, y1);
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("拟行手术名称:           " + txtNXSS.Text, ptzt, Brushes.Black, new Point(x + 50, y));
            e.Graphics.DrawLine(Pens.Black, x + 150, y1, 750, y1);
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("拟行麻醉方式：", ptzt2, Brushes.Black, new Point(x + 50, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 160, y, 12, 12);
            e.Graphics.DrawString("全麻（气管插管）", ptzt, Brushes.Black, x + 180, y);
            if (ckQM.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 155, y, x + 165, y + 12);
                e.Graphics.DrawLine(pblack2, x + 165, y + 12, x + 180, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 300, y, 12, 12);
            e.Graphics.DrawString("硬膜外麻醉", ptzt, Brushes.Black, x + 320, y);
            if (ckYMW.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 295, y, x + 305, y + 12);
                e.Graphics.DrawLine(pblack2, x + 305, y + 12, x + 320, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 420, y, 12, 12);
            e.Graphics.DrawString("腰硬联合麻醉", ptzt, Brushes.Black, x + 440, y);
            if (ckYYLH.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 415, y, x + 425, y + 12);
                e.Graphics.DrawLine(pblack2, x + 425, y + 12, x + 440, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 540, y, 12, 12);
            e.Graphics.DrawString("腰麻（穿刺点", ptzt, Brushes.Black, x + 560, y);
            if (ckYM.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 535, y, x + 545, y + 12);
                e.Graphics.DrawLine(pblack2, x + 545, y + 12, x + 560, y - 3);
                if (cmbCC.Text == "") cmbCC.Text = " /   ";
                e.Graphics.DrawString(this.cmbCC.Text + "）", ptzt, Brushes.Black, x + 650, y);
            }
            else
            {
                cmbCC.Text = " /   ";                           
                e.Graphics.DrawString(this.cmbCC.Text+"）", ptzt, Brushes.Black, x + 650, y);
            }
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x + 160, y, 12, 12);
            e.Graphics.DrawString("颈丛神经阻滞", ptzt, Brushes.Black, x + 180, y);
            if (ckZCSJ.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 155, y, x + 165, y + 12);
                e.Graphics.DrawLine(pblack2, x + 165, y + 12, x + 180, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 300, y, 12, 12);
            e.Graphics.DrawString("臂丛神经阻滞", ptzt, Brushes.Black, x + 320, y);
            if (ckBCSJ.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 295, y, x + 305, y + 12);
                e.Graphics.DrawLine(pblack2, x + 305, y + 12, x + 320, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 420, y, 12, 12);
            e.Graphics.DrawString("监护麻醉", ptzt, Brushes.Black, x + 440, y);
            if (ckJH.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 415, y, x + 425, y + 12);
                e.Graphics.DrawLine(pblack2, x + 425, y + 12, x + 440, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 540, y, 12, 12);
            e.Graphics.DrawString("静脉麻醉", ptzt, Brushes.Black, x + 560, y);
            if (ckJM.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 535, y, x + 545, y + 12);
                e.Graphics.DrawLine(pblack2, x + 545, y + 12, x + 560, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 640, y, 12, 12);
            e.Graphics.DrawString("无", ptzt, Brushes.Black, x + 660, y);
            if (ckW.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 635, y, x + 645, y + 12);
                e.Graphics.DrawLine(pblack2, x + 645, y + 12, x + 660, y - 3);
            }
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("备选麻醉方式：", ptzt2, Brushes.Black, new Point(x + 50, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 160, y, 12, 12);
            e.Graphics.DrawString("全麻（气管插管）", ptzt, Brushes.Black, x + 180, y);
            if (ckQM1.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 155, y, x + 165, y + 12);
                e.Graphics.DrawLine(pblack2, x + 165, y + 12, x + 180, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 300, y, 12, 12);
            e.Graphics.DrawString("硬膜外麻醉", ptzt, Brushes.Black, x + 320, y);
            if (ckYMW1.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 295, y, x + 305, y + 12);
                e.Graphics.DrawLine(pblack2, x + 305, y + 12, x + 320, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 420, y, 12, 12);
            e.Graphics.DrawString("腰硬联合麻醉", ptzt, Brushes.Black, x + 440, y);
            if (ckYYLH1.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 415, y, x + 425, y + 12);
                e.Graphics.DrawLine(pblack2, x + 425, y + 12, x + 440, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 540, y, 12, 12);
            e.Graphics.DrawString("腰麻（穿刺点", ptzt, Brushes.Black, x + 560, y);
            if (ckYM1.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 535, y, x + 545, y + 12);
                e.Graphics.DrawLine(pblack2, x + 545, y + 12, x + 560, y - 3);
                if (cmbCC1.Text == "") cmbCC1.Text = " /   ";
                e.Graphics.DrawString(this.cmbCC1.Text + "）", ptzt, Brushes.Black, x + 650, y);
            }
            else
            {
                cmbCC1.Text = " /   ";
                e.Graphics.DrawString(this.cmbCC1.Text + "）", ptzt, Brushes.Black, x + 650, y);
            }
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x + 160, y, 12, 12);
            e.Graphics.DrawString("颈丛神经阻滞", ptzt, Brushes.Black, x + 180, y);
            if (ckZCSJ1.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 155, y, x + 165, y + 12);
                e.Graphics.DrawLine(pblack2, x + 165, y + 12, x + 180, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 300, y, 12, 12);
            e.Graphics.DrawString("臂丛神经阻滞", ptzt, Brushes.Black, x + 320, y);
            if (ckBCSJ1.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 295, y, x + 305, y + 12);
                e.Graphics.DrawLine(pblack2, x + 305, y + 12, x + 320, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 420, y, 12, 12);
            e.Graphics.DrawString("监护麻醉", ptzt, Brushes.Black, x + 440, y);
            if (ckJH1.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 415, y, x + 425, y + 12);
                e.Graphics.DrawLine(pblack2, x + 425, y + 12, x + 440, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 540, y, 12, 12);
            e.Graphics.DrawString("静脉麻醉", ptzt, Brushes.Black, x + 560, y);
            if (ckJM1.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 535, y, x + 545, y + 12);
                e.Graphics.DrawLine(pblack2, x + 545, y + 12, x + 560, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 640, y, 12, 12);
            e.Graphics.DrawString("无", ptzt, Brushes.Black, x + 660, y);
            if (ckW1.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 635, y, x + 645, y + 12);
                e.Graphics.DrawLine(pblack2, x + 645, y + 12, x + 660, y - 3);
            }
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("拟选麻醉药品：", ptzt2, Brushes.Black, new Point(x + 50, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 160, y, 12, 12);
            e.Graphics.DrawString("丙泊酚", ptzt, Brushes.Black, x + 180, y);
            if (ckBBF.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 155, y, x + 165, y + 12);
                e.Graphics.DrawLine(pblack2, x + 165, y + 12, x + 180, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 12, 12);
            e.Graphics.DrawString("依托咪酯", ptzt, Brushes.Black, x + 260, y);
            if (ckYTMZ.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 235, y, x + 245, y + 12);
                e.Graphics.DrawLine(pblack2, x + 245, y + 12, x + 260, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 330, y, 12, 12);
            e.Graphics.DrawString("咪达唑仑", ptzt, Brushes.Black, x + 350, y);
            if (ckMDZl.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 325, y, x + 335, y + 12);
                e.Graphics.DrawLine(pblack2, x + 335, y + 12, x + 350, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 420, y, 12, 12);
            e.Graphics.DrawString("舒芬太尼", ptzt, Brushes.Black, x + 440, y);
            if (ckSFTN.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 415, y, x + 425, y + 12);
                e.Graphics.DrawLine(pblack2, x + 425, y + 12, x + 440, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 510, y, 12, 12);
            e.Graphics.DrawString("芬太尼", ptzt, Brushes.Black, x + 530, y);
            if (ckFTN.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 505, y, x + 515, y + 12);
                e.Graphics.DrawLine(pblack2, x + 515, y + 12, x + 530, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 590, y, 12, 12);
            e.Graphics.DrawString("异氟烷", ptzt, Brushes.Black, x + 610, y);
            if (ckYFW.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 585, y, x + 595, y + 12);
                e.Graphics.DrawLine(pblack2, x + 595, y + 12, x + 610, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 670, y, 12, 12);
            e.Graphics.DrawString("异氟烷", ptzt, Brushes.Black, x + 690, y);
            if (ckQFW.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 665, y, x + 675, y + 12);
                e.Graphics.DrawLine(pblack2, x + 675, y + 12, x + 690, y - 3);
            }
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x + 160, y, 12, 12);
            e.Graphics.DrawString("维库溴铵", ptzt, Brushes.Black, x + 180, y);
            if (ckWKXA.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 155, y, x + 165, y + 12);
                e.Graphics.DrawLine(pblack2, x + 165, y + 12, x + 180, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 250, y, 12, 12);
            e.Graphics.DrawString("盐酸利多卡因", ptzt, Brushes.Black, x + 270, y);
            if (ckYSLDKY.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 245, y, x + 255, y + 12);
                e.Graphics.DrawLine(pblack2, x + 255, y + 12, x + 270, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 12, 12);
            e.Graphics.DrawString("耐乐品", ptzt, Brushes.Black, x + 380, y);
            if (ckNLP.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 355, y, x + 365, y + 12);
                e.Graphics.DrawLine(pblack2, x + 365, y + 12, x + 380, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 430, y, 12, 12);
            e.Graphics.DrawString("左旋布比卡因", ptzt, Brushes.Black, x + 450, y);
            if (ckZXBBKY.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 425, y, x + 435, y + 12);
                e.Graphics.DrawLine(pblack2, x + 435, y + 12, x + 450, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 550, y, 12, 12);
            e.Graphics.DrawString("甲磺酸罗哌卡因", ptzt, Brushes.Black, x + 570, y);
            if (ckJHS.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 545, y, x + 555, y + 12);
                e.Graphics.DrawLine(pblack2, x + 555, y + 12, x + 570, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 680, y, 12, 12);
            e.Graphics.DrawString("氯胺酮", ptzt, Brushes.Black, x + 700, y);
            if (ckLAT.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 675, y, x + 685, y + 12);
                e.Graphics.DrawLine(pblack2, x + 685, y + 12, x + 700, y - 3);
            }
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x + 160, y, 12, 12);
            e.Graphics.DrawString("顺式阿曲库铵", ptzt, Brushes.Black, x + 180, y);
            if (ckSSAQ.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 155, y, x + 165, y + 12);
                e.Graphics.DrawLine(pblack2, x + 165, y + 12, x + 180, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 280, y, 12, 12);
            e.Graphics.DrawString("其他药物", ptzt, Brushes.Black, x + 300, y);
            if (ckQTYY.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 275, y, x + 285, y + 12);
                e.Graphics.DrawLine(pblack2, x + 285, y + 12, x + 300, y - 3);
                e.Graphics.DrawString(txtQTYY.Text, ptzt, Brushes.Black, x + 370, y);
                e.Graphics.DrawLine(Pens.Black, x + 355, y1, 750, y1);
            }
            else
            {
                e.Graphics.DrawLine(Pens.Black, x + 355, y1, 750, y1);
            }
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("拟检测项目：", ptzt2, Brushes.Black, new Point(x + 50, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 160, y, 12, 12);
            e.Graphics.DrawString("NIBP", ptzt, Brushes.Black, x + 180, y);
            if (ckNIBP.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 155, y, x + 165, y + 12);
                e.Graphics.DrawLine(pblack2, x + 165, y + 12, x + 180, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 220, y, 12, 12);
            e.Graphics.DrawString("ECG", ptzt, Brushes.Black, x + 240, y);
            if (ckECG.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 215, y, x + 225, y + 12);
                e.Graphics.DrawLine(pblack2, x + 225, y + 12, x + 240, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 280, y, 12, 12);
            e.Graphics.DrawString("SPO2", ptzt, Brushes.Black, x + 300, y);
            if (ckECG.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 275, y, x + 285, y + 12);
                e.Graphics.DrawLine(pblack2, x + 285, y + 12, x + 300, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 340, y, 12, 12);
            e.Graphics.DrawString("PetCO2", ptzt, Brushes.Black, x + 360, y);
            if (ckPetCO2.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 335, y, x + 345, y + 12);
                e.Graphics.DrawLine(pblack2, x + 345, y + 12, x + 360, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 410, y, 12, 12);
            e.Graphics.DrawString("尿量", ptzt, Brushes.Black, x + 430, y);
            if (ckNL.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 405, y, x + 415, y + 12);
                e.Graphics.DrawLine(pblack2, x + 415, y + 12, x + 430, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 470, y, 12, 12);
            e.Graphics.DrawString("IBP", ptzt, Brushes.Black, x + 490, y);
            if (ckIBP.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 465, y, x + 475, y + 12);
                e.Graphics.DrawLine(pblack2, x + 475, y + 12, x + 490, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 520, y, 12, 12);
            e.Graphics.DrawString("CVP", ptzt, Brushes.Black, x + 540, y);
            if (ckCVP.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 515, y, x + 525, y + 12);
                e.Graphics.DrawLine(pblack2, x + 525, y + 12, x + 540, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 570, y, 12, 12);
            e.Graphics.DrawString("血气分析", ptzt, Brushes.Black, x + 590, y);
            if (ckXQFX.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 565, y, x + 575, y + 12);
                e.Graphics.DrawLine(pblack2, x + 575, y + 12, x + 590, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 650, y, 12, 12);
            e.Graphics.DrawString("电解质", ptzt, Brushes.Black, x + 670, y);
            if (ckDJZ.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 645, y, x + 655, y + 12);
                e.Graphics.DrawLine(pblack2, x + 655, y + 12, x + 670, y - 3);
            }
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x + 160, y, 12, 12);
            e.Graphics.DrawString("血糖", ptzt, Brushes.Black, x + 180, y);
            if (ckXT.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 155, y, x + 165, y + 12);
                e.Graphics.DrawLine(pblack2, x + 165, y + 12, x + 180, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 220, y, 12, 12);
            e.Graphics.DrawString("心功能监测", ptzt, Brushes.Black, x + 240, y);
            if (ckXGN.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 215, y, x + 225, y + 12);
                e.Graphics.DrawLine(pblack2, x + 225, y + 12, x + 240, y - 3);
            }
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x + 160, y, 12, 12);
            e.Graphics.DrawString("其它", ptzt, Brushes.Black, x + 180, y);
            if (ckQT.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 155, y, x + 165, y + 12);
                e.Graphics.DrawLine(pblack2, x + 165, y + 12, x + 180, y - 3);
                e.Graphics.DrawString(txtQT.Text, ptzt, Brushes.Black, x + 220, y);
                e.Graphics.DrawLine(Pens.Black, x + 205, y1, 750, y1);
            }
            else
            {
                e.Graphics.DrawLine(Pens.Black, x + 205, y1, 750, y1);
            }
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("麻醉辅助措施：", ptzt2, Brushes.Black, new Point(x + 50, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 160, y, 12, 12);
            e.Graphics.DrawString("控制性低血压", ptzt, Brushes.Black, x + 180, y);
            if (ckKZXDXY.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 155, y, x + 165, y + 12);
                e.Graphics.DrawLine(pblack2, x + 165, y + 12, x + 180, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 280, y, 12, 12);
            e.Graphics.DrawString("人工低温", ptzt, Brushes.Black, x + 300, y);
            if (ckRGDW.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 275, y, x + 285, y + 12);
                e.Graphics.DrawLine(pblack2, x + 285, y + 12, x + 300, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 12, 12);
            e.Graphics.DrawString("中心静脉穿刺置管", ptzt, Brushes.Black, x + 380, y);
            if (ckZXJMCCZG.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 355, y, x + 365, y + 12);
                e.Graphics.DrawLine(pblack2, x + 365, y + 12, x + 380, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 510, y, 12, 12);
            e.Graphics.DrawString("动脉穿刺置管", ptzt, Brushes.Black, x + 530, y);
            if (ckDMCCZG.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 505, y, x + 515, y + 12);
                e.Graphics.DrawLine(pblack2, x + 515, y + 12, x + 530, y - 3);
            }
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("术中风险考虑：   "+this.txtSZFXKL.Text, ptzt, Brushes.Black, new Point(x + 50, y));
            e.Graphics.DrawLine(Pens.Black, x + 150, y1, 750, y1);
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("拟准备特殊急救药品及设备：   " + this.txtJJ.Text, ptzt, Brushes.Black, new Point(x + 50, y));
            e.Graphics.DrawLine(Pens.Black, x + 230, y1, 750, y1);
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("术后患者拟转入单元：", ptzt, Brushes.Black, new Point(x + 50, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 220, y, 12, 12);
            e.Graphics.DrawString("病房", ptzt, Brushes.Black, x + 240, y);
            if (cmbZR.Text=="病房")
            {
                e.Graphics.DrawLine(pblack2, x + 215, y, x + 225, y + 12);
                e.Graphics.DrawLine(pblack2, x + 225, y + 12, x + 240, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 290, y, 12, 12);
            e.Graphics.DrawString("PACU", ptzt, Brushes.Black, x + 310, y);
            if (cmbZR.Text == "PACU")
            {
                e.Graphics.DrawLine(pblack2, x + 285, y, x + 295, y + 12);
                e.Graphics.DrawLine(pblack2, x + 295, y + 12, x + 310, y - 3);
            }
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("麻醉医师：" + cmbMZJHDYY.Text, ptzt, Brushes.Black, x + 50, y);
            e.Graphics.DrawLine(Pens.Black, x + 110, y1, 250, y1);
            e.Graphics.DrawString(dtMZJHD.Text, ptzt, Brushes.Black, x + 500, y);
            e.Graphics.DrawLine(Pens.Black, x + 500, y1, 700, y1);          
            #endregion
            #region 麻醉术后访视记录（术后第一天）
              y = y + 50; y1 = y + 15;
            e.Graphics.DrawString("麻醉术后访视记录（术后第一天）", ptzt3, Brushes.Black, x + 250, y);
            //y = y + 50; y1 = y + 15;
            //string title2 = "麻 醉 术 后 访 视 记 录 单";
            //e.Graphics.DrawString(title2, ptzt3, Brushes.Black, new Point(x + 240, y));
            y = y + 40; y1 = y + 15; x =50;
            if (tbYssss.Text == "") tbYssss.Text = "   无 ";
            e.Graphics.DrawString("实施手术名称：" + tbYssss.Text, ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 80, y1, x + 740, y1);

            y = y + 20; y1 = y + 15;
            if (tbMazuiFS.Text == "") tbMazuiFS.Text = "   无 ";
            e.Graphics.DrawString("术前实施麻醉方式：" + this.tbMazuiFS.Text.Trim(), ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 110, y1, x + 740, y1);

            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("全身麻醉：", ptzt2, Brushes.Black, new Point(x, y));

            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("生命体征：  意识：" + cmbYishi.Text, ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 110, y1, x + 210, y1);
            if (tbXueya.Text == "") tbXueya.Text = "   ";
            e.Graphics.DrawString("血压：" + this.tbXueya.Text, ptzt, Brushes.Black, new Point(x + 230, y));
            e.Graphics.DrawLine(Pens.Black, x + 260, y1, x + 340, y1);
            if (tbHuxi.Text == "") tbHuxi.Text = "    ";
            e.Graphics.DrawString("mmHg：  呼吸：" + this.tbHuxi.Text, ptzt, Brushes.Black, new Point(x + 340, y));
            e.Graphics.DrawLine(Pens.Black, x + 420, y1, x + 500, y1);
            e.Graphics.DrawString("次/分：  心率：" + this.tbXinlv.Text, ptzt, Brushes.Black, new Point(x + 500, y));
            e.Graphics.DrawLine(Pens.Black, x + 580, y1, x + 660, y1);
            e.Graphics.DrawString("次/分", ptzt, Brushes.Black, new Point(x + 660, y));

            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("咽喉痛", ptzt, Brushes.Black, new Point(x + 30, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 10, y, 12, 12);
            e.Graphics.DrawString("声嘶", ptzt, Brushes.Black, new Point(x + 120, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 100, y, 12, 12);
            e.Graphics.DrawString("后水肿", ptzt, Brushes.Black, new Point(x + 210, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 190, y, 12, 12);
            e.Graphics.DrawString("肺部感染", ptzt, Brushes.Black, new Point(x + 300, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 280, y, 12, 12);
            e.Graphics.DrawString("肺不张", ptzt, Brushes.Black, new Point(x + 390, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 370, y, 12, 12);
            e.Graphics.DrawString("心率失常", ptzt, Brushes.Black, new Point(x + 480, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 460, y, 12, 12);
            e.Graphics.DrawString("腹胀", ptzt, Brushes.Black, new Point(x + 570, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 550, y, 12, 12);
            e.Graphics.DrawString("恶心呕吐", ptzt, Brushes.Black, new Point(x + 660, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 640, y, 12, 12);
            if (cbYBZZ1.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 5, y, x + 15, y + 12);
                e.Graphics.DrawLine(pblack2, x + 15, y + 12, x + 30, y - 3);
            }
            if (cbYBZZ2.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 95, y, x + 105, y + 12);
                e.Graphics.DrawLine(pblack2, x + 105, y + 12, x + 115, y - 3);
            }
            if (cbYBZZ3.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 185, y, x + 195, y + 12);
                e.Graphics.DrawLine(pblack2, x + 195, y + 12, x + 205, y - 3);
            }
            if (cbYBZZ4.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 275, y, x + 285, y + 12);
                e.Graphics.DrawLine(pblack2, x + 285, y + 12, x + 295, y - 3);
            }
            if (cbYBZZ5.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 365, y, x + 375, y + 12);
                e.Graphics.DrawLine(pblack2, x + 375, y + 12, x + 385, y - 3);
            }
            if (cbYBZZ6.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 455, y, x + 465, y + 12);
                e.Graphics.DrawLine(pblack2, x + 475, y + 12, x + 485, y - 3);
            }
            if (cbYBZZ7.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 545, y, x + 555, y + 12);
                e.Graphics.DrawLine(pblack2, x + 555, y + 12, x + 565, y - 3);
            }
            if (cbYBZZ8.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 635, y, x + 645, y + 12);
                e.Graphics.DrawLine(pblack2, x + 645, y + 12, x + 655, y - 3);
            }
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("其他： " +tbOtherZZ.Text, ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 30, y1, x + 740, y1);

            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("神经阻滞麻醉及椎管内麻醉：", ptzt2, Brushes.Black, new Point(x, y));

            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("生命体征：  意识：" + cmbYishiSJ.Text, ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 110, y1, x + 210, y1);
            if (tbXueyaSJ.Text == "") tbXueyaSJ.Text = "   ";
            e.Graphics.DrawString("血压：" + this.tbXueyaSJ.Text, ptzt, Brushes.Black, new Point(x + 230, y));
            e.Graphics.DrawLine(Pens.Black, x + 260, y1, x + 340, y1);
            if (tbHuxiSJ.Text == "") tbHuxiSJ.Text = "    ";
            e.Graphics.DrawString("mmHg：  呼吸：" + this.tbHuxiSJ.Text, ptzt, Brushes.Black, new Point(x + 340, y));
            e.Graphics.DrawLine(Pens.Black, x + 420, y1, x + 500, y1);
            e.Graphics.DrawString("次/分：  心率：" + this.tbXinlvSJ.Text, ptzt, Brushes.Black, new Point(x + 500, y));
            e.Graphics.DrawLine(Pens.Black, x + 580, y1, x + 660, y1);
            e.Graphics.DrawString("次/分", ptzt, Brushes.Black, new Point(x + 660, y));

            y = y + 20; y1 = y + 15;
            if (cmbExin.Text == "") cmbExin.Text = "   无 ";
            e.Graphics.DrawString("恶心：" + this.cmbExin.Text, ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 30, y1, x + 130, y1);
            if (cmbChuanciBW.Text == "") cmbChuanciBW.Text = "  正常";
            e.Graphics.DrawString("穿刺部位：" + this.cmbChuanciBW.Text, ptzt, Brushes.Black, new Point(x + 150, y));
            e.Graphics.DrawLine(Pens.Black, x + 205, y1, x + 300, y1);
            if (cmbZhitiHD.Text == "") cmbZhitiHD.Text = "  正常";
            e.Graphics.DrawString("肢体活动：" + this.cmbZhitiHD.Text, ptzt, Brushes.Black, new Point(x + 320, y));
            e.Graphics.DrawLine(Pens.Black, x + 380, y1, x + 460, y1);
            if (cmbShuhouZT.Text == "") cmbShuhouZT.Text = "  无";
            e.Graphics.DrawString("术后镇痛方式：" + this.cmbShuhouZT.Text, ptzt, Brushes.Black, new Point(x + 480, y));
            e.Graphics.DrawLine(Pens.Black, x + 565, y1, x + 720, y1);

            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("镇痛泵配方：" + this.tbZTBPF.Text.Trim(), ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 70, y1, x + 740, y1);

            y = y + 20; y1 = y + 15;
            if (cmbZTXG.Text == "") cmbZTXG.Text = "   优 ";
            e.Graphics.DrawString("镇痛效果：" + this.cmbZTXG.Text, ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 65, y1, x + 400, y1);

            y = y + 20; y1 = y + 15;
            if (tbRemark.Text.Trim() == "") tbRemark.Text = "   优 ";
            e.Graphics.DrawString("备注： " + tbRemark.Text.Trim(), ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 30, y1, x + 740, y1);

            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("麻醉医师：" + this.cmbMZYS1.Text.Trim(), ptzt, Brushes.Black, new Point(x + 200, y));
            e.Graphics.DrawLine(Pens.Black, x + 265, y1, x + 400, y1);
            e.Graphics.DrawString("访视日期：" + this.dtVisitDate1.Text, ptzt, Brushes.Black, new Point(x + 430, y));
            e.Graphics.DrawLine(Pens.Black, x + 490, y1, x + 660, y1);

            #endregion
            #region 麻醉术后访视记录（术后第二天）
            y = y + 50; y1 = y + 15;
            e.Graphics.DrawString("（术后第二天）", ptzt3, Brushes.Black, x + 300, y);          
            y = y + 40; y1 = y + 15;           
            e.Graphics.DrawString("全身麻醉：", ptzt2, Brushes.Black, new Point(x, y));
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("生命体征：  意识：" + cmbYishi1.Text, ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 110, y1, x + 210, y1);
            if (tbXueya1.Text == "") tbXueya1.Text = "   ";
            e.Graphics.DrawString("血压：" + this.tbXueya1.Text, ptzt, Brushes.Black, new Point(x + 230, y));
            e.Graphics.DrawLine(Pens.Black, x + 260, y1, x + 340, y1);
            if (tbHuxi1.Text == "") tbHuxi1.Text = "    ";
            e.Graphics.DrawString("mmHg：  呼吸：" + this.tbHuxi1.Text, ptzt, Brushes.Black, new Point(x + 340, y));
            e.Graphics.DrawLine(Pens.Black, x + 420, y1, x + 500, y1);
            e.Graphics.DrawString("次/分：  心率：" + this.tbXinlv1.Text, ptzt, Brushes.Black, new Point(x + 500, y));
            e.Graphics.DrawLine(Pens.Black, x + 580, y1, x + 660, y1);
            e.Graphics.DrawString("次/分", ptzt, Brushes.Black, new Point(x + 660, y));

            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("咽喉痛", ptzt, Brushes.Black, new Point(x + 30, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 10, y, 12, 12);
            e.Graphics.DrawString("声嘶", ptzt, Brushes.Black, new Point(x + 120, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 100, y, 12, 12);
            e.Graphics.DrawString("后水肿", ptzt, Brushes.Black, new Point(x + 210, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 190, y, 12, 12);
            e.Graphics.DrawString("肺部感染", ptzt, Brushes.Black, new Point(x + 300, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 280, y, 12, 12);
            e.Graphics.DrawString("肺不张", ptzt, Brushes.Black, new Point(x + 390, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 370, y, 12, 12);
            e.Graphics.DrawString("心率失常", ptzt, Brushes.Black, new Point(x + 480, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 460, y, 12, 12);
            e.Graphics.DrawString("腹胀", ptzt, Brushes.Black, new Point(x + 570, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 550, y, 12, 12);
            e.Graphics.DrawString("恶心呕吐", ptzt, Brushes.Black, new Point(x + 660, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 640, y, 12, 12);
            if (cbYBZZ11.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 5, y, x + 15, y + 12);
                e.Graphics.DrawLine(pblack2, x + 15, y + 12, x + 30, y - 3);
            }
            if (cbYBZZ21.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 95, y, x + 105, y + 12);
                e.Graphics.DrawLine(pblack2, x + 105, y + 12, x + 115, y - 3);
            }
            if (cbYBZZ31.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 185, y, x + 195, y + 12);
                e.Graphics.DrawLine(pblack2, x + 195, y + 12, x + 205, y - 3);
            }
            if (cbYBZZ41.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 275, y, x + 285, y + 12);
                e.Graphics.DrawLine(pblack2, x + 285, y + 12, x + 295, y - 3);
            }
            if (cbYBZZ51.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 365, y, x + 375, y + 12);
                e.Graphics.DrawLine(pblack2, x + 375, y + 12, x + 385, y - 3);
            }
            if (cbYBZZ61.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 455, y, x + 465, y + 12);
                e.Graphics.DrawLine(pblack2, x + 475, y + 12, x + 485, y - 3);
            }
            if (cbYBZZ71.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 545, y, x + 555, y + 12);
                e.Graphics.DrawLine(pblack2, x + 555, y + 12, x + 565, y - 3);
            }
            if (cbYBZZ81.Checked)
            {
                e.Graphics.DrawLine(pblack2, x + 635, y, x + 645, y + 12);
                e.Graphics.DrawLine(pblack2, x + 645, y + 12, x + 655, y - 3);
            }
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("其他：  " + tbOtherZZ1.Text, ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 30, y1, x + 740, y1);

            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("神经阻滞麻醉及椎管内麻醉：", ptzt2, Brushes.Black, new Point(x, y));

            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("生命体征：  意识：" + cmbYishiSJ1.Text, ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 110, y1, x + 210, y1);
            if (tbXueyaSJ1.Text == "") tbXueyaSJ1.Text = "   ";
            e.Graphics.DrawString("血压：" + this.tbXueyaSJ1.Text, ptzt, Brushes.Black, new Point(x + 230, y));
            e.Graphics.DrawLine(Pens.Black, x + 260, y1, x + 340, y1);
            if (tbHuxiSJ1.Text == "") tbHuxiSJ1.Text = "    ";
            e.Graphics.DrawString("mmHg：  呼吸：" + this.tbHuxiSJ1.Text, ptzt, Brushes.Black, new Point(x + 340, y));
            e.Graphics.DrawLine(Pens.Black, x + 420, y1, x + 500, y1);
            e.Graphics.DrawString("次/分：  心率：" + this.tbXinlvSJ1.Text, ptzt, Brushes.Black, new Point(x + 500, y));
            e.Graphics.DrawLine(Pens.Black, x + 580, y1, x + 660, y1);
            e.Graphics.DrawString("次/分", ptzt, Brushes.Black, new Point(x + 660, y));

            y = y + 20; y1 = y + 15;
            if (cmbExin1.Text == "") cmbExin1.Text = "   无 ";
            e.Graphics.DrawString("恶心：" + this.cmbExin1.Text, ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 30, y1, x + 130, y1);
            if (cmbChuanciBW1.Text == "") cmbChuanciBW1.Text = "  正常";
            e.Graphics.DrawString("穿刺部位：" + this.cmbChuanciBW1.Text, ptzt, Brushes.Black, new Point(x + 150, y));
            e.Graphics.DrawLine(Pens.Black, x + 205, y1, x + 300, y1);
            if (cmbZhitiHD1.Text == "") cmbZhitiHD1.Text = "  正常";
            e.Graphics.DrawString("肢体活动：" + this.cmbZhitiHD1.Text, ptzt, Brushes.Black, new Point(x + 320, y));
            e.Graphics.DrawLine(Pens.Black, x + 380, y1, x + 460, y1);
            if (cmbShuhouZT1.Text == "") cmbShuhouZT1.Text = "  无";
            e.Graphics.DrawString("术后镇痛方式：" + this.cmbShuhouZT1.Text, ptzt, Brushes.Black, new Point(x + 480, y));
            e.Graphics.DrawLine(Pens.Black, x + 565, y1, x + 720, y1);

            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("镇痛泵配方：" + this.tbZTBPF1.Text.Trim(), ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 70, y1, x + 740, y1);

            y = y + 20; y1 = y + 15;
            if (cmbZTXG1.Text == "") cmbZTXG1.Text = "   优 ";
            e.Graphics.DrawString("镇痛效果：" + this.cmbZTXG1.Text, ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 65, y1, x + 400, y1);

            y = y + 20; y1 = y + 15;
            if (tbRemark1.Text.Trim() == "") tbRemark1.Text = "   优 ";
            e.Graphics.DrawString("备注： " + tbRemark1.Text.Trim(), ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 30, y1, x + 740, y1);

            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("麻醉医师：" + this.cmbMZYS11.Text.Trim(), ptzt, Brushes.Black, new Point(x + 200, y));
            e.Graphics.DrawLine(Pens.Black, x + 265, y1, x + 400, y1);
            e.Graphics.DrawString("访视日期：" + this.dtVisitDate11.Text, ptzt, Brushes.Black, new Point(x + 430, y));
            e.Graphics.DrawLine(Pens.Black, x + 490, y1, x + 660, y1);

            #endregion
        }
        /// <summary>
        /// 打印麻醉计划单和术后随访单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button14_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog2.Document = printDocument2;
            if (printPreviewDialog2.ShowDialog() == DialogResult.OK)
                printDocument2.Print();
            printPreviewDialog2.PrintPreviewControl.Zoom = 1.0;
            printDocument2.DefaultPageSettings.PaperSize =
                     new System.Drawing.Printing.PaperSize("A4", 820, 1160);
        }
        /// <summary>
        /// 术后第一天
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog2.Document = printDocument2;
            if (printPreviewDialog2.ShowDialog() == DialogResult.OK)
                printDocument2.Print();
            printPreviewDialog2.PrintPreviewControl.Zoom = 1.0;
            printDocument2.DefaultPageSettings.PaperSize =
                     new System.Drawing.Printing.PaperSize("A4", 820, 1160);
        }
        /// <summary>
        /// 术后第二天
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog2.Document = printDocument2;
            if (printPreviewDialog2.ShowDialog() == DialogResult.OK)
                printDocument2.Print();
            printPreviewDialog2.PrintPreviewControl.Zoom = 1.0;
            printDocument2.DefaultPageSettings.PaperSize =
                     new System.Drawing.Printing.PaperSize("A4", 820, 1160);
        }
        /// <summary>
        /// 麻醉会诊单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.0;
            printDocument1.DefaultPageSettings.PaperSize =
                     new System.Drawing.Printing.PaperSize("A4", 820, 1160);

        }
      
        
    }
}
