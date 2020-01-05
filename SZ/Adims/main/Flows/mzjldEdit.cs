using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;
using System.Threading;
using System.IO.Ports;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.IO;
using MODEL;
using adims_MODEL;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using WindowsFormsControlLibrary5;
using System.Net.NetworkInformation;
using System.Threading;
using System.Xml.Linq;
using System.Xml;
using main.HisWebReference;
using NHapi.Model.V24.Message;
using MediII.Common;
using adims_BLL;
using System.Configuration;
using main.MZJLDs;
using Adims_Utility;

namespace main
{
    public partial class mzjldEdit : Form, IMessageFilter
    {
        #region //飞利浦参数

        const int Waiting_Period = 200;
        Socket server;
        bool ThreadExist = false;
        Thread Receiving_xy;
        const int MAX_TRY_TIMES = 5; //尝试取值的次数
        const int Get_Period = 5000; //采集间隔是5000毫秒
        IPEndPoint phillip_server;
        IPEndPoint Remote;
        EndPoint phillip;
        Byte[] recv_Data = new Byte[8096];
        int GetDataTimes = 0;
        int LoopCount = 0;
        int TOF = 0; bool tof_effective = false;
        int BIS = 0; bool bis_effective = false;
        int PR = 0; bool pr_effective = false;    //脉率
        int RR = 0; bool rr_effective = false;    //呼吸频率
        float SPO2 = 0; bool spo2_effective = false;//血氧饱和度
        float TEMP = 0; bool temp_effective = false;//温度
        int HR = 0; bool hr_effective = false;    //心率
        int SYS = 0; bool sys_effective = false; //收缩压
        int DIA = 0; bool dia_effective = false; //舒张压
        int MAP = 0; bool map_effective = false; //平均血压
        int CVP_SYS = 0; bool CVP_sys_effective = false; //中心静脉收缩压
        int CVP_DIA = 0; bool CVP_dia_effective = false; //中心静脉舒张压
        int CVP_MAP = 0; bool CVP_map_effective = false; //中心静脉平均血压
        int ABP_SYS = 0; bool ABP_sys_effective = false; //动脉收缩压
        int ABP_DIA = 0; bool ABP_dia_effective = false; //动脉舒张压
        int ABP_MAP = 0; bool ABP_map_effective = false; //动脉平均血压
        bool HasEffectiveData = false;
        bool HasEffectivePressure = false;
        DateTime MeasureDate;
        string IPAddressInput1;
        string BedIDInput1;
        SQLiteHelper sh = new adims_DAL.SQLiteHelper();
        adims_DAL.AdimsProvider ap = new adims_DAL.AdimsProvider();
        #endregion

        #region <<Members>>

        DateTime ssksTime = new DateTime();//手术开始时间
        DateTime ssjsTime = new DateTime();//手术结束时间
        DateTime mzksTime = new DateTime();//麻醉开始时间
        DateTime mzjsTime = new DateTime();//麻醉结束时间
        DateTime cgTime = new DateTime();//插管时间
        DateTime bgTime = new DateTime();//拔管时间
        DateTime jkksTime = new DateTime();//机控开始时间
        DateTime jkjsTime = new DateTime();//机控结束时间

        List<adims_MODEL.point> pointList = new List<adims_MODEL.point>();//监测点集合
        DateTime fristopen = new DateTime();//总体打印开始时间
        DateTime ptime = new DateTime();//每一页打印开始时间
        int iYema = 1;//打印页码标记
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        adims_DAL.DB2help his = new DB2help();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_MODEL.OldPBModel pat = new adims_MODEL.OldPBModel();
        Label lbMzks1 = new Label();//麻醉开始图标
        Label lbMzjs1 = new Label();//麻醉结束图标
        Label ssks1 = new Label();//手术开始图标
        Label ssjs1 = new Label();//手术结束图标
        Label lb_cguan = new Label();//插管图标
        Label lb_bguan = new Label();//拔管图标

        DateTime ksjcTime = new DateTime();//开始检测时间参数

        public int mzjldID = 0;//麻醉编号

        string patID = "";//病人编号
        string Oroom = "";//手术间名字
        int p2f = 0, p2lx = 0, p2xi = 0;//点在pictureBox2上的标志，类型，出现次数

        adims_MODEL.mzqt t_mzqt = new adims_MODEL.mzqt();//单个麻醉气体
        adims_MODEL.mzqt t_mzqt2 = new adims_MODEL.mzqt();//单个麻醉气体2
        adims_MODEL.shuye t_shuye = new adims_MODEL.shuye();//单个输液
        adims_MODEL.shuxue t_shuxue = new adims_MODEL.shuxue();//单个输血
        adims_MODEL.mzpingmian t_mzpm = new adims_MODEL.mzpingmian();//单个麻醉平面
        adims_MODEL.mzyt t_mzyt = new adims_MODEL.mzyt();//单个麻醉诱导药
        adims_MODEL.jtytsx t_jtytsx = new adims_MODEL.jtytsx();//单个局麻药
        adims_MODEL.clcxqt p4t1 = new adims_MODEL.clcxqt();//单个出血
        int p3f = 0;
        int p3lf1 = 0, p3lf2 = 0;//开始结束标签鼠标按下标志
        int p4f = 0, p4lx = 0;
        bool mzks = false, mzjs = true, ssks = false, ssjs = true, CGUAN = false, BGUAN = false;


        adims_MODEL.tsyy p4t2;//单个特殊用药
        adims_MODEL.szsj p4t3;//单个术中事件
        adims_MODEL.point p3t;//单个监测点
        adims_MODEL.pointF p3tF;//单个监测点
        int qtsl = 0; //气体数量
        int p2x = 0, p2y = 0;//鼠标在picturebox2上的位置
        int p3x = 0, p3y = 0;//鼠标在picturebox3上的位置
        int p4x = 0, p4y = 0;//鼠标在picturebox4上的位置

        int p8x = 0, p8y = 0;//鼠标在picturebox8上的位置

        private List<string> jhxma = new List<string>();//所有监护项目
        private List<string> jhxmy = new List<string>();//已添加监护项目
        public List<adims_MODEL.point> ssyPoint = new List<adims_MODEL.point>();//收缩的点
        public List<adims_MODEL.point> szyPoint = new List<adims_MODEL.point>();//舒张压的点
        //public List<adims_MODEL.point> xlPoint = new List<adims_MODEL.point>();//心率
        public List<adims_MODEL.pointF> twPoint = new List<adims_MODEL.pointF>();//体温的点
        public List<adims_MODEL.point> mboPoint = new List<adims_MODEL.point>();//脉搏
        public List<adims_MODEL.point> hxlPoint = new List<adims_MODEL.point>();//呼吸率的点
        public List<adims_MODEL.point> spo2List = new List<adims_MODEL.point>();//spo2列表
        public List<adims_MODEL.point> etco2List = new List<adims_MODEL.point>();//etco2列表

        public List<adims_MODEL.point> cvpList = new List<adims_MODEL.point>();//spo2列表
        public List<adims_MODEL.point> bisList = new List<adims_MODEL.point>();//spo2列表
        private List<adims_MODEL.jhxm> jhxmValueList = new List<adims_MODEL.jhxm>();//监护项目值
        private List<adims_MODEL.szsj> szsj = new List<adims_MODEL.szsj>();//术中事件
        private List<adims_MODEL.tsyy> tsyy = new List<adims_MODEL.tsyy>();//特殊用药
        private List<adims_MODEL.ZhenTongYao> ztyList = new List<adims_MODEL.ZhenTongYao>();//镇痛药
        private List<adims_MODEL.clcxqt> clcxqt = new List<adims_MODEL.clcxqt>();//出尿出血其他出量

        Point lastpoint = new Point();//画线的时候保存上一个点
        Point lastpoint1 = new Point();//画线的时候保存上一个点
        PointF lastpoint2 = new PointF();//画线的时候保存上一个点
        Point lastpoint3 = new Point();//画线的时候保存上一个点
        Point lastpoint4 = new Point();//画线的时候保存上一个点
        string MZYS = "";
        string SSYS = "";
        DateTime otime = new DateTime();//入室时间
        private List<adims_MODEL.mzpingmian> mzpmList = new List<adims_MODEL.mzpingmian>();//麻醉平面集合

        List<adims_MODEL.mzqt> mzqtList = new List<adims_MODEL.mzqt>();//麻醉气体集合
        List<adims_MODEL.shuxue> shuxueList = new List<adims_MODEL.shuxue>();//输血集合
        double syZongl = 0, sxZongl = 0;//输液总量，输血总量
        List<adims_MODEL.shuye> shuyeList = new List<adims_MODEL.shuye>();//输液集合
        List<adims_MODEL.mzyt> ydyList = new List<adims_MODEL.mzyt>();//诱导药集合

        List<adims_MODEL.jtytsx> jmyList = new List<adims_MODEL.jtytsx>();//局麻药集合
        List<adims_MODEL.jtytsx> jiaot1 = new List<adims_MODEL.jtytsx>();
        List<adims_MODEL.jtytsx> sx1 = new List<adims_MODEL.jtytsx>();
        int jcsjjg = 1;//检测时间间隔
        bool isSjll = false;//是否术间浏览
        DateTime odate;//手术日期

        #endregion

        #region <<Constructors>>

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="patid122"></param>
        public mzjldEdit(string patid, string oroom, DateTime dt, int mzid, bool sjllState)
        {
            InitializeComponent();
            odate = dt;
            isSjll = sjllState;
            Oroom = oroom;
            mzjldID = mzid;
            this.patID = patid;

            GetConfigure();
            IPaddress.Text = IPAddressInput1;
            //BedNumber1.Text = BedIDInput1;
            phillip_server = new IPEndPoint(IPAddress.Parse(IPaddress.Text.Trim()), 24105);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Remote = new IPEndPoint(IPAddress.Any, 8001);
            phillip = (EndPoint)Remote;
        }
        public mzjldEdit(string patid, string oroom, DateTime dt, int mzid)
        {
            InitializeComponent();
            odate = dt;
            mzjldID = mzid;
            Oroom = oroom;
            this.patID = patid;
            GetConfigure();
            IPaddress.Text = IPAddressInput1;
            //BedNumber1.Text = BedIDInput1;
            phillip_server = new IPEndPoint(IPAddress.Parse(IPaddress.Text.Trim()), 24105);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Remote = new IPEndPoint(IPAddress.Any, 8001);
            phillip = (EndPoint)Remote;

        }

        public mzjldEdit(string patID)
        {
            InitializeComponent();
            this.patID = patID;
            GetConfigure();
            IPaddress.Text = IPAddressInput1;
            //BedNumber1.Text = BedIDInput1;
            phillip_server = new IPEndPoint(IPAddress.Parse(IPaddress.Text.Trim()), 24105);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Remote = new IPEndPoint(IPAddress.Any, 8001);
            phillip = (EndPoint)Remote;

        }
        public mzjldEdit(DateTime otime, string patID)
        {
            this.patID = patID;
            this.otime = otime;
            InitializeComponent();
            GetConfigure();
            IPaddress.Text = IPAddressInput1;
            //BedNumber1.Text = BedIDInput1;
            phillip_server = new IPEndPoint(IPAddress.Parse(IPaddress.Text.Trim()), 24105);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Remote = new IPEndPoint(IPAddress.Any, 8001);
            phillip = (EndPoint)Remote;
        }
        #endregion

        #region <<Events>>
        private void timer3_Tick(object sender, EventArgs e)
        {
            timer3.Interval = 2000;
            lab1.Visible = false;
            lab2.Visible = false;
            timer3.Enabled = false;
        }
        private void cmbSJJG_SelectedIndexChanged(object sender, EventArgs e)//修改检测时间间隔
        {
            string SQL = "jcsjjg='" + cmbSJJG.Text.Trim() + "' WHERE id='" + mzjldID + "'";
            int i = dal.UpdateMzjld_CSQK(SQL);
            if (i > 0)
            {
                BindShijiandian();
                pictureBox2.Refresh();
                pictureBox3.Refresh();
                pictureBox4.Refresh();
                BindMZSSCGBG();
            }
        }

        private void BindShijiandian()//绑定时间点信息
        {
            fristopen = otime;
            jhxma.Clear();
            jhxma.Add("ECG");
            jhxma.Add("CVP");
            jhxma.Add("NIBP");
            jhxma.Add("ART");
            jhxma.Add("RESP");
            jhxma.Add("BIS");
            jhxma.Add("TOF");
            jcsjjg = cmbSJJG.Text.Trim().ToInt32();
            lbtimew1.Text = lbTime1.Text = otime.ToString("HH:mm");
            lbtimew2.Text = lbTime2.Text = otime.AddMinutes(6 * jcsjjg).ToString("HH:mm");
            //+ ":" + (otime.AddMinutes(6 * jcsjjs).Minute == 0 ? "00" : "30");
            lbtimew3.Text = lbTime3.Text = otime.AddMinutes(12 * jcsjjg).ToString("HH:mm");
            lbtimew4.Text = lbTime4.Text = otime.AddMinutes(18 * jcsjjg).ToString("HH:mm");
            lbtimew5.Text = lbTime5.Text = otime.AddMinutes(24 * jcsjjg).ToString("HH:mm");
            lbtimew6.Text = lbTime6.Text = otime.AddMinutes(30 * jcsjjg).ToString("HH:mm");
            lbtimew7.Text = lbTime7.Text = otime.AddMinutes(36 * jcsjjg).ToString("HH:mm");
            lbtimew8.Text = lbTime8.Text = otime.AddMinutes(42 * jcsjjg).ToString("HH:mm");
            lbtimew9.Text = lbTime9.Text = otime.AddMinutes(48 * jcsjjg).ToString("HH:mm");
            lbtimew10.Text = lbTime10.Text = otime.AddMinutes(54 * jcsjjg).ToString("HH:mm");
            lbtimew11.Text = lbTime11.Text = otime.AddMinutes(60 * jcsjjg).ToString("HH:mm");
        }

        private void BindMzjldBacicInfo()//绑定麻醉记录单信息
        {

            DataTable dtMzjld = bll.SelectOldPatInfo(mzjldID);
            otime = (Convert.ToDateTime(dtMzjld.Rows[0]["otime"]));
            ucOperDate.Controls[0].Text = otime.ToShortDateString();
            cmbSJJG.Text = Convert.ToString(dtMzjld.Rows[0]["jcsjjg"]);
            if (dtMzjld.Rows[0]["isTiwenView"].ToInt32() == 1)
            {
                isTiwenView = true;
                cbIsTiwen.Checked = true;
            }
            else if (dtMzjld.Rows[0]["isTiwenView"].ToInt32() == 0)
            {
                isTiwenView = false;
                cbIsTiwen.Checked = false;
            }
            ucHeight.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["Height"]);
            ucWeight.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["weight"]);
            ucSqyy.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["sqyy"]);
            ucTSBQ.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["tsbq"]);
            ucTemp.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["tiwen"]);
            cmbXueXing.Text = Convert.ToString(dtMzjld.Rows[0]["XueXing"]);
            cmbTiwei.Text = Convert.ToString(dtMzjld.Rows[0]["tw"]);
            cmbMZFF.Text = Convert.ToString(dtMzjld.Rows[0]["mzfa"]);
            cmbChaguan.Text = Convert.ToString(dtMzjld.Rows[0]["ChaGuanFF"]);
            cmbZZFF.Text = Convert.ToString(dtMzjld.Rows[0]["zzff"]);
            cmbCCdianUp1.Text = Convert.ToString(dtMzjld.Rows[0]["ccdianup1"]);
            cmbCCdianUp2.Text = Convert.ToString(dtMzjld.Rows[0]["ccdianup2"]);
            ucZhiGuanUp.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["zhiguanup"]);
            cmbCCdianDown1.Text = Convert.ToString(dtMzjld.Rows[0]["ccdiandown1"]);
            cmbCCdianDown2.Text = Convert.ToString(dtMzjld.Rows[0]["ccdiandown2"]);
            ucZhiGuanDown.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["zhiguandown"]);
            ucSHZD.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["szzd"]);
            ucSSSS.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["SSSS"]);
            ucChuxue.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["chuxue"]);
            cmbMZXG.Text = Convert.ToString(dtMzjld.Rows[0]["mzxg"]);
            cmbBingRenQuXiang.Text = Convert.ToString(dtMzjld.Rows[0]["brqx"]);
            ucNiaoLiang.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["NiaoLiang"]);
            if (Convert.ToString(dtMzjld.Rows[0]["eyeoper"]) == "0")
            {
                cbEyeOper.Checked = false;
            }
            if (Convert.ToString(dtMzjld.Rows[0]["eyeoper"]) == "1")
            {
                cbEyeOper.Checked = true;
            }
            ucSSYS.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["ssys"]);
            ucMZYS.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["mzys"]);
            ucQXHS.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["qxhs"]);
            ucXHHS.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["xhhs"]);
            cmbCutType.Text = Convert.ToString(dtMzjld.Rows[0]["CutType"]);
            cmbOperLevel.Text = Convert.ToString(dtMzjld.Rows[0]["OperLevel"]);
            //cmbASA.Text = Convert.ToString(dtMzjld.Rows[0]["ASA"]);
            //if (Convert.ToString(dtMzjld.Rows[0]["asae"]) == "1")
            //{
            //    cbE.Checked = true;
            //}
            //else
            //    cbE.Checked = false;

            BindMZSSCGBG();//手术，麻醉，插管拔管时间赋值，坐标赋值

            #region↓术中事件赋值
            //// 
            //DataTable dtSZSJ = dal.GetSZSJ(mzjldID);
            //int i_szsj = 0;
            //foreach (DataRow dr in dtSZSJ.Rows)
            //{
            //    if (i_szsj<=10)
            //    {
            //        listBox2.Items.Add(i_szsj + 1 + "." + dr[2] + " " + Convert.ToDateTime(dr[3]).ToString("MM:dd"));
            //    }
            //    else
            //        listBox3.Items.Add(i_szsj + 1 + "." + dr[2] + " " + Convert.ToDateTime(dr[3]).ToString("MM:dd"));

            //    i_szsj++;
            //}
            //// ↓特殊用药赋值
            //DataTable dtTSYY = dal.GetTSYY(mzjldID);
            //int i_tsyy = 0;
            //foreach (DataRow dr in dtTSYY.Rows)
            //{
            //    listBox4.Items.Add(i_tsyy + 1 + "." + dr[2] + " " + dr[3] + " " + dr[4]);
            //    i_tsyy++;
            //}
            #endregion

        }

        private void BindMZSSCGBG()//绑定麻醉，手术开始结束，插管拔管时间，及坐标
        {
            DataTable dtMzjld = bll.SelectOldPatInfo(mzjldID);
            if (dtMzjld.Rows[0]["otime"].ToString() != "")
            {
                DateTime ssDate = (Convert.ToDateTime(dtMzjld.Rows[0]["otime"]));
                ucOperDate.Controls[0].Text = ssDate.ToShortDateString();
            }
            if (dtMzjld.Rows[0]["mzkssj"].ToString() != "")
            {
                mzksTime = Convert.ToDateTime(dtMzjld.Rows[0]["mzkssj"]);
                TimeSpan t = new TimeSpan();
                t = mzksTime - otime;
                if ((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) > 0)
                {
                    lbMzks1.Visible = true;
                    lbMzks1.Text = "X";
                    lbMzks1.AutoSize = true;
                    lbMzks1.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    lbMzks1.BackColor = Color.Transparent;
                    lbMzks1.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 165), 180);
                    this.pictureBox4.Controls.Add(lbMzks1);
                    mzks = true;
                    mzjs = false;
                    lbMzks1.MouseDown += new MouseEventHandler(lbMzks1_MouseDown);
                    lbMzks1.MouseMove += new MouseEventHandler(lbMzks1_MouseMove);
                    lbMzks1.MouseUp += new MouseEventHandler(lbMzks1_MouseUp);
                    lbMzks1.MouseLeave += new EventHandler(lbMzks1_MouseLeave);
                }

            }
            if (dtMzjld.Rows[0]["mzjssj"].ToString() != "")
            {
                mzjsTime = Convert.ToDateTime(dtMzjld.Rows[0]["mzjssj"]);
                TimeSpan t = new TimeSpan();
                t = mzjsTime - otime;
                if ((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) > 0)
                {
                    lbMzjs1.Visible = true;
                    lbMzjs1.Text = "X";
                    lbMzjs1.AutoSize = true;
                    lbMzjs1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    lbMzjs1.BackColor = Color.Transparent;
                    lbMzjs1.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 165), 180);
                    this.pictureBox4.Controls.Add(lbMzjs1);
                    mzjs = true;
                    lbMzjs1.MouseDown += new MouseEventHandler(lbMzjs1_MouseDown);
                    lbMzjs1.MouseMove += new MouseEventHandler(lbMzjs1_MouseMove);
                    lbMzjs1.MouseUp += new MouseEventHandler(lbMzjs1_MouseUp);
                    lbMzjs1.MouseLeave += new EventHandler(lbMzjs1_MouseLeave);
                }
            }
            if (dtMzjld.Rows[0]["sskssj"].ToString() != "")
            {
                ssksTime = Convert.ToDateTime(dtMzjld.Rows[0]["sskssj"]);
                TimeSpan t = new TimeSpan();
                t = ssksTime - otime;
                if ((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) > 0)
                {
                    ssks1.Text = "⊙";
                    ssks1.Visible = true;
                    ssks1.AutoSize = true;
                    ssks1.BackColor = Color.Transparent;
                    ssks1.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    ssks1.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 165), 180);
                    this.pictureBox4.Controls.Add(ssks1);
                    ssks = true;
                    ssjs = false;
                    ssks1.MouseDown += new MouseEventHandler(ssks1_MouseDown);
                    ssks1.MouseMove += new MouseEventHandler(ssks1_MouseMove);
                    ssks1.MouseUp += new MouseEventHandler(ssks1_MouseUp);
                    ssks1.MouseLeave += new EventHandler(ssks1_MouseLeave);
                }
            }
            if (dtMzjld.Rows[0]["ssjssj"].ToString() != "")
            {
                ssjsTime = Convert.ToDateTime(dtMzjld.Rows[0]["ssjssj"]);
                TimeSpan t = new TimeSpan();
                t = ssjsTime - otime;
                if ((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) > 0)
                {
                    ssjs1.Visible = true;
                    ssjs1.Text = "⊙";
                    ssjs1.AutoSize = true;
                    ssjs1.BackColor = Color.Transparent;
                    ssjs1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                    ssjs1.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 165), 180);
                    this.pictureBox4.Controls.Add(ssjs1);
                    ssjs = true;
                    ssjs1.MouseDown += new MouseEventHandler(ssjs1_MouseDown);
                    ssjs1.MouseMove += new MouseEventHandler(ssjs1_MouseMove);
                    ssjs1.MouseUp += new MouseEventHandler(ssjs1_MouseUp);
                    ssjs1.MouseLeave += new EventHandler(ssjs1_MouseLeave);
                }
            }
            if (dtMzjld.Rows[0]["cgsj"].ToString() != "")
            {
                cgTime = Convert.ToDateTime(dtMzjld.Rows[0]["cgsj"]);
                TimeSpan t = new TimeSpan();
                t = cgTime - otime;
                if ((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) > 0)
                {
                    lb_cguan.Visible = true;
                    lb_cguan.Text = "Θ";
                    lb_cguan.AutoSize = true;
                    lb_cguan.BackColor = Color.Transparent;
                    lb_cguan.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    lb_cguan.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 165), 180);
                    this.pictureBox4.Controls.Add(lb_cguan);
                    CGUAN = true;
                    BGUAN = false;
                    lb_cguan.MouseDown += new MouseEventHandler(lb_cguan_MouseDown);
                    lb_cguan.MouseMove += new MouseEventHandler(lb_cguan_MouseMove);
                    lb_cguan.MouseUp += new MouseEventHandler(lb_cguan_MouseUp);
                    lb_cguan.MouseLeave += new EventHandler(lb_cguan_MouseLeave);
                }
            }
            if (dtMzjld.Rows[0]["bgsj"].ToString() != "")
            {
                bgTime = Convert.ToDateTime(dtMzjld.Rows[0]["bgsj"]);
                TimeSpan t = new TimeSpan();
                t = bgTime - otime;
                if ((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) > 0)
                {
                    lb_bguan.Text = "Φ";
                    lb_bguan.Visible = true;
                    lb_bguan.AutoSize = true;
                    lb_bguan.BackColor = Color.Transparent;
                    lb_bguan.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    lb_bguan.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 165), 180);
                    this.pictureBox4.Controls.Add(lb_bguan);
                    BGUAN = true;
                    lb_bguan.MouseDown += new MouseEventHandler(lb_bguan_MouseDown);
                    lb_bguan.MouseMove += new MouseEventHandler(lb_bguan_MouseMove);
                    lb_bguan.MouseUp += new MouseEventHandler(lb_bguan_MouseUp);
                    lb_bguan.MouseLeave += new EventHandler(lb_bguan_MouseLeave);
                }
            }
        }

        private void GetPatBasicInfo()//绑定病人基本信息
        {
            DataTable dt = bll.SelectPatInfo(patID);
            if (dt.Rows.Count == 0)
            {
                return;
            }
            ucPatId.Controls[0].Text = Convert.ToString(dt.Rows[0]["patid"]);
            ucName.Controls[0].Text = Convert.ToString(dt.Rows[0]["patname"]);
            ucAge.Controls[0].Text = Convert.ToString(dt.Rows[0]["patage"]);
            ucSex.Controls[0].Text = Convert.ToString(dt.Rows[0]["patsex"]);
            ucHeight.Controls[0].Text = Convert.ToString(dt.Rows[0]["PatHeight"]);
            ucWeight.Controls[0].Text = Convert.ToString(dt.Rows[0]["PatWeight"]);
            ucXueya.Controls[0].Text = Convert.ToString(dt.Rows[0]["xueya"]);
            ucMaibo.Controls[0].Text = Convert.ToString(dt.Rows[0]["maibo"]);
            ucHuxi.Controls[0].Text = Convert.ToString(dt.Rows[0]["huxi"]);
            ucTemp.Controls[0].Text = Convert.ToString(dt.Rows[0]["tiwen"]);
            this.cmbXueXing.Text = Convert.ToString(dt.Rows[0]["PatBloodType"]);
            ucZhuyuanNo.Controls[0].Text = Convert.ToString(dt.Rows[0]["PatZhuYuanID"]);
            this.ucBingQu.Controls[0].Text = Convert.ToString(dt.Rows[0]["patdpm"]);
            this.ucBedNo.Controls[0].Text = Convert.ToString(dt.Rows[0]["patbedno"]);
            this.cmbASA.Text = Convert.ToString(dt.Rows[0]["asa"]);
            string SSYS = dt.Rows[0]["OS"].ToString();
            if (dt.Rows[0]["OA1"].ToString() != "")
            {
                SSYS += "、" + dt.Rows[0]["OA1"].ToString();
            }
            if (dt.Rows[0]["OA2"].ToString() != "")
            {
                SSYS += "、" + dt.Rows[0]["OA2"].ToString();
            }
            if (dt.Rows[0]["OA3"].ToString() != "")
            {
                SSYS += "、" + dt.Rows[0]["OA3"].ToString();
            }
            string MZYS = dt.Rows[0]["AP1"].ToString();
            if (dt.Rows[0]["AP2"].ToString() != "")
            {
                MZYS += "、" + dt.Rows[0]["AP2"].ToString();
            }
            if (dt.Rows[0]["AP3"].ToString() != "")
            {
                MZYS += "、" + dt.Rows[0]["AP3"].ToString();
            }
            string XHHS = dt.Rows[0]["ON1"].ToString();
            if (dt.Rows[0]["ON2"].ToString() != "")
            {
                XHHS += "、" + dt.Rows[0]["ON2"].ToString();
            }
            string QXHS = dt.Rows[0]["SN1"].ToString();
            if (dt.Rows[0]["SN2"].ToString() != "")
            {
                QXHS += "、" + dt.Rows[0]["SN2"].ToString();
            }
            ucMZYS.Controls[0].Text = SSYS;
            ucMZYS.Controls[0].Text = MZYS;
            ucQXHS.Controls[0].Text = QXHS;
            ucXHHS.Controls[0].Text = XHHS;
            if (dt.Rows[0]["ASAE"].ToString() != "")
            {
                if (dt.Rows[0]["ASAE"].ToInt32() == 1)
                    cbE.Checked = true;
                else
                    cbE.Checked = false;
            }
            this.ucSSSS.Controls[0].Text = Convert.ToString(dt.Rows[0]["oname"]);
            ucSHZD.Controls[0].Text = Convert.ToString(dt.Rows[0]["pattmd"]);
            ucSqzd.Controls[0].Text = Convert.ToString(dt.Rows[0]["pattmd"]);
            ucOperDate.Controls[0].Text =
                Convert.ToDateTime(dt.Rows[0]["Odate"]).ToShortDateString();

        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        private void BindPortName()//绑定串口端口
        {
            string[] spName = System.IO.Ports.SerialPort.GetPortNames();
            if (spName.Length > 0)
            {
                cmbCOM.Items.Clear();
                foreach (string portName in spName)
                {
                    cmbCOM.Items.Add(portName);
                    cmbCOM.SelectedIndex = 0;
                }
            }
        }
        #region //IMessageFilter 成员,禁止下拉框鼠标滚动

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 522) return true;
            else return false;
        }

        #endregion
        admin_T_SQL at = new adims_DAL.admin_T_SQL();
        public void mzjld_Load(object sender, EventArgs e)
        {

            timer5.Start();
            timer5.Interval = 30000;
            dal.UpdatePaibanInfo(2, patID);//修改排班状态--手术准备
            dal.UpdateShoushujianinfo(1, Oroom);//修改手术间列表状态--手术准备
            timer2.Start();
            timer2.Interval = 20 * 1000;
            Application.AddMessageFilter(this);//禁止下拉框鼠标滚动
            //this.txtSqzd.Controls[0].DoubleClick += new System.EventHandler(this.txtSqzd_DoubleClick);
            this.ucSqyy.Controls[0].DoubleClick += new System.EventHandler(this.txtSqyy_DoubleClick);
            //this.txtNssss.Controls[0].DoubleClick += new System.EventHandler(this.txtNssss_DoubleClick);
            this.ucSHZD.Controls[0].DoubleClick += new System.EventHandler(this.txtSzzd_DoubleClick);
            //this.txtMzff.Controls[0].DoubleClick += new System.EventHandler(this.txtMzff_DoubleClick);
            this.ucSSSS.Controls[0].DoubleClick += new System.EventHandler(this.txtYssss_DoubleClick);
            this.ucMZYS.Controls[0].DoubleClick += new System.EventHandler(this.txtMZYS_DoubleClick);
            this.ucSSYS.Controls[0].DoubleClick += new System.EventHandler(this.txtSSYS_DoubleClick);
            this.ucQXHS.Controls[0].DoubleClick += new System.EventHandler(this.txtQXHS_DoubleClick);
            this.ucXHHS.Controls[0].DoubleClick += new System.EventHandler(this.txtXHHS_DoubleClick);
            this.ucSSYS.Controls[0].KeyPress += new KeyPressEventHandler(txtSSYS_KeyPress);
            this.ucMZYS.Controls[0].KeyPress += new KeyPressEventHandler(txtSSYS_KeyPress);
            this.ucQXHS.Controls[0].KeyPress += new KeyPressEventHandler(txtSSYS_KeyPress);
            this.ucXHHS.Controls[0].KeyPress += new KeyPressEventHandler(txtSSYS_KeyPress);

            BindPortName();
            try
            {
                GetPatBasicInfo();
                if (mzjldID == 0)//麻醉编号为0，则是新建麻醉记录单
                {
                    if (DateTime.Now.Minute > 20)
                        otime = DateTime.Now.AddMinutes(-DateTime.Now.Minute);
                    else
                        otime = DateTime.Now.AddMinutes(-DateTime.Now.Minute - 30);

                    int insertFlag = bll.addMzjld(patID, otime);
                    if (insertFlag == 0)
                    {
                        MessageBox.Show("服务器连接失败。请重新打开麻醉记录单！");
                        this.Close();
                        return;
                    }
                    DataTable dt = bll.selectSinglemzjld(patID, otime);
                    mzjldID = dt.Rows[0][0].ToInt32();

                    ucOperDate.Controls[0].Text = odate.ToShortDateString();
                    ucMzjldId.Controls[0].Text = Convert.ToString(mzjldID);
                    dal.UpdateShoushujianinfo(1, mzjldID, patID, Oroom);//修改手术间状态信息
                    jhxmy.Clear();
                    jhxmy.Add("SpO2");
                    jhxmy.Add("ETCO2");
                    int f_jhxm = 0;
                    foreach (string name in jhxmy)
                    {
                        bll.addJhxm(mzjldID, name, 0);
                        f_jhxm++;
                    }
                    if (f_jhxm == 0)
                        MessageBox.Show("监护项目添加失败！");
                    cmbSJJG.Text = "5";
                    BindShijiandian();
                    SaveMzjld();

                }
                else if (mzjldID != 0)//麻醉编号不为0，则是进入旧麻醉记录单
                {
                    dal.UpdateShoushujianinfo(1, mzjldID, patID, Oroom);//修改手术间状态信息
                    BindMzjldBacicInfo();
                    jhxmy.Clear();
                    DataTable dtJhxm = bll.GetJhxm(mzjldID);
                    for (int i = 0; i < dtJhxm.Rows.Count; i++)
                    {
                        jhxmy.Add(dtJhxm.Rows[i][0].ToString());
                    }
                    BindXueyaMaiboHuxi();
                    ucMzjldId.Controls[0].Text = Convert.ToString(mzjldID);

                    BindShijiandian();//绑定时间坐标                

                    BindJikongTime();
                    BindQtList();
                    BindYdyList();
                    BindJmyList();
                    BindTsyy();
                    CheckLocalAndServerData();
                    BindJHDian();
                    BindCLlist();
                    BindShuyeList();
                    BindShuxueList();
                    ucZongLiang.Controls[0].Text = (sxZongl + syZongl).ToString();
                    BindmzpmList();
                    BindOperInfo();
                    BindSZSJ();
                    BindZhenTongYao();
                }
                if (isSjll)//如果是术间浏览，则隐藏修改功能
                {
                    AddPointTSMenu.Enabled = false;
                    FirstToolStripMenuItem.Enabled = false;
                    DeleteCGBGStripMenuItem.Enabled = false;
                    jkhxToolStripMenuItem.Enabled = false;
                    cmbSJJG.Enabled = false;
                    this.ucSqyy.Controls[0].DoubleClick -= new System.EventHandler(this.txtSqyy_DoubleClick);
                    this.ucSHZD.Controls[0].DoubleClick -= new System.EventHandler(this.txtSzzd_DoubleClick);
                    this.ucMZYS.Controls[0].DoubleClick -= new System.EventHandler(this.txtSSYS_DoubleClick);
                    this.ucSSSS.Controls[0].DoubleClick -= new System.EventHandler(this.txtYssss_DoubleClick);
                    this.ucMZYS.Controls[0].DoubleClick -= new System.EventHandler(this.txtMZYS_DoubleClick);
                    this.ucQXHS.Controls[0].DoubleClick -= new System.EventHandler(this.txtQXHS_DoubleClick);
                    this.ucXHHS.Controls[0].DoubleClick -= new System.EventHandler(this.txtXHHS_DoubleClick);

                    pictureBox6.MouseDoubleClick -= new MouseEventHandler(pictureBox6_MouseDoubleClick);
                    pictureBox4.MouseDoubleClick -= new MouseEventHandler(pictureBox4_MouseDoubleClick);
                    pictureBox8.DoubleClick -= new EventHandler(pictureBox8_DoubleClick);
                    pictureBox4.DoubleClick -= new EventHandler(pictureBox4_DoubleClick);
                    pictureBox2.MouseMove -= new MouseEventHandler(pictureBox2_MouseMove);
                    pictureBox3.MouseMove -= new MouseEventHandler(pictureBox3_MouseMove);
                    pictureBox4.MouseMove -= new MouseEventHandler(pictureBox4_MouseMove);
                    btnSave.Enabled = false;
                    btnMonitor.Enabled = false;
                    cmbCOM.Enabled = false;
                    cmbJianhuyi.Enabled = false;
                    btnTsyy.Enabled = false;
                    btnSzsj.Enabled = false;
                    //this.btnPrintView.Enabled = false;
                    this.dtInRoomTime.Enabled = false;//2014-06-16
                    this.btnQXQD.Enabled = false;//2014-06-16
                    this.button2.Enabled = false;//2014-06-16
                    this.btnzongjie.Enabled = false;//2014-06-16
                    this.cmbBingRenQuXiang.Enabled = false;//2014-06-16
                    this.cmbTiwei.Enabled = false;//2014-06-16
                    this.cmbZZFF.Enabled = false;//2014-06-16
                    this.cmbMZFF.Enabled = false;//2014-06-16
                    this.cmbCCdianUp1.Enabled = false;//2014-06-16
                    this.cmbCCdianUp2.Enabled = false;//2014-06-16
                    this.cmbChaguan.Enabled = false;//2014-06-16
                    this.cmbCCdianDown1.Enabled = false;//2014-06-16
                    this.cmbCCdianDown2.Enabled = false;//2014-06-16
                    btnZhenTongyao.Enabled = false;
                    lbMzjs.DoubleClick -= new EventHandler(lbMzjs_DoubleClick);
                    lbMzks.DoubleClick -= new EventHandler(lbMzks_DoubleClick);
                    lbSsjs.DoubleClick -= new EventHandler(lbSsjs_DoubleClick);
                    lbSsks.DoubleClick -= new EventHandler(lbSsks_DoubleClick);
                    lbBg.DoubleClick -= new EventHandler(lbBg_DoubleClick);
                    lbCg.DoubleClick -= new EventHandler(lbCg_DoubleClick);
                    lb_cguan.MouseDown -= new MouseEventHandler(lb_cguan_MouseDown);
                    lb_cguan.MouseMove -= new MouseEventHandler(lb_cguan_MouseMove);
                    lb_cguan.MouseUp -= new MouseEventHandler(lb_cguan_MouseUp);
                    lb_cguan.MouseLeave -= new EventHandler(lb_cguan_MouseLeave);
                    lb_bguan.MouseDown -= new MouseEventHandler(lb_bguan_MouseDown);
                    lb_bguan.MouseMove -= new MouseEventHandler(lb_bguan_MouseMove);
                    lb_bguan.MouseUp -= new MouseEventHandler(lb_bguan_MouseUp);
                    lb_bguan.MouseLeave -= new EventHandler(lb_bguan_MouseLeave);
                    lbMzjs1.MouseDown -= new MouseEventHandler(lbMzjs1_MouseDown);
                    lbMzjs1.MouseMove -= new MouseEventHandler(lbMzjs1_MouseMove);
                    lbMzjs1.MouseUp -= new MouseEventHandler(lbMzjs1_MouseUp);
                    lbMzjs1.MouseLeave -= new EventHandler(lbMzjs1_MouseLeave);
                    lbMzks1.MouseDown -= new MouseEventHandler(lbMzks1_MouseDown);
                    lbMzks1.MouseMove -= new MouseEventHandler(lbMzks1_MouseMove);
                    lbMzks1.MouseUp -= new MouseEventHandler(lbMzks1_MouseUp);
                    lbMzks1.MouseLeave -= new EventHandler(lbMzks1_MouseLeave);
                    ssjs1.MouseDown -= new MouseEventHandler(ssjs1_MouseDown);
                    ssjs1.MouseMove -= new MouseEventHandler(ssjs1_MouseMove);
                    ssjs1.MouseUp -= new MouseEventHandler(ssjs1_MouseUp);
                    ssjs1.MouseLeave -= new EventHandler(ssjs1_MouseLeave);
                    ssks1.MouseDown -= new MouseEventHandler(ssks1_MouseDown);
                    ssks1.MouseMove -= new MouseEventHandler(ssks1_MouseMove);
                    ssks1.MouseUp -= new MouseEventHandler(ssks1_MouseUp);
                    ssks1.MouseLeave -= new EventHandler(ssks1_MouseLeave);
                    lb_cguan.MouseDown -= new MouseEventHandler(lb_cguan_MouseDown);
                    lb_cguan.MouseMove -= new MouseEventHandler(lb_cguan_MouseMove);
                    lb_cguan.MouseUp -= new MouseEventHandler(lb_cguan_MouseUp);
                    lb_cguan.MouseLeave -= new EventHandler(lb_cguan_MouseLeave);
                    lb_bguan.MouseDown -= new MouseEventHandler(lb_bguan_MouseDown);
                    lb_bguan.MouseMove -= new MouseEventHandler(lb_bguan_MouseMove);
                    lb_bguan.MouseUp -= new MouseEventHandler(lb_bguan_MouseUp);
                    lb_bguan.MouseLeave -= new EventHandler(lb_bguan_MouseLeave);
                    lbMzjs1.MouseDown -= new MouseEventHandler(lbMzjs1_MouseDown);
                    lbMzjs1.MouseMove -= new MouseEventHandler(lbMzjs1_MouseMove);
                    lbMzjs1.MouseUp -= new MouseEventHandler(lbMzjs1_MouseUp);
                    lbMzjs1.MouseLeave -= new EventHandler(lbMzjs1_MouseLeave);
                    lbMzks1.MouseDown -= new MouseEventHandler(lbMzks1_MouseDown);
                    lbMzks1.MouseMove -= new MouseEventHandler(lbMzks1_MouseMove);
                    lbMzks1.MouseUp -= new MouseEventHandler(lbMzks1_MouseUp);
                    lbMzks1.MouseLeave -= new EventHandler(lbMzks1_MouseLeave);
                    ssjs1.MouseDown -= new MouseEventHandler(ssjs1_MouseDown);
                    ssjs1.MouseMove -= new MouseEventHandler(ssjs1_MouseMove);
                    ssjs1.MouseUp -= new MouseEventHandler(ssjs1_MouseUp);
                    ssjs1.MouseLeave -= new EventHandler(ssjs1_MouseLeave);
                    ssks1.MouseDown -= new MouseEventHandler(ssks1_MouseDown);
                    ssks1.MouseMove -= new MouseEventHandler(ssks1_MouseMove);
                    ssks1.MouseUp -= new MouseEventHandler(ssks1_MouseUp);
                    ssks1.MouseLeave -= new EventHandler(ssks1_MouseLeave);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "出现异常，请检查！");
            }
            this.dtInRoomTime.Format = DateTimePickerFormat.Custom;
            this.dtInRoomTime.CustomFormat = "MM-dd HH:mm";
            dtInRoomTime.Value = otime;


        }

        private void CheckLocalAndServerData()//检查本地和服务器的数据
        {
            DataTable ServerMax = ap.GetMaxTimeServer(mzjldID);
            DataTable LoaclMax = ap.GetMaxTimeLocal(mzjldID);
            DateTime ServerMaxTime = new DateTime();
            DateTime LocalMaxTime = new DateTime();

            if (LoaclMax.Rows[0][0].ToString() != "")
            {
                LocalMaxTime = Convert.ToDateTime(LoaclMax.Rows[0][0].ToString());
                if (ServerMax.Rows[0][0].ToString() != "")
                {
                    ServerMaxTime = Convert.ToDateTime(ServerMax.Rows[0][0].ToString());
                }
                else
                {
                    DataTable LoacalMin = ap.GetMinTimeLocal(mzjldID);
                    ServerMaxTime = Convert.ToDateTime(LoacalMin.Rows[0][0]);
                }
                ServerMaxTime = ServerMaxTime.AddMinutes(jcsjjg);
                while (ServerMaxTime <= LocalMaxTime)
                {
                    DataTable dtIn = at.GetMzjldPointInServer(ServerMaxTime, mzjldID);
                    if (dtIn.Rows.Count == 0)
                    {
                        int a = ap.CopyLocalToServer(ServerMaxTime.ToString("yyyy-MM-dd HH:mm"), mzjldID);
                        ServerMaxTime = ServerMaxTime.AddMinutes(jcsjjg);
                    }
                }
            }
        }

        #region //绑定用药，时间，麻醉平面等数据
        private void BindQtList()
        {
            mzqtList.Clear();
            DataTable dtQT = bll.select_qt(mzjldID);
            if (dtQT.Rows.Count != 0)
            {
                int i = 0;
                foreach (DataRow dr in dtQT.Rows)
                {
                    adims_MODEL.mzqt mzqt = new adims_MODEL.mzqt();
                    mzqtList.Add(mzqt);
                    mzqtList[i].Id = dr["id"].ToInt32();
                    mzqtList[i].Qtname = Convert.ToString(dr["qtname"]);
                    mzqtList[i].Yl = dr["yl"].ToDouble();
                    mzqtList[i].Dw = Convert.ToString(dr["dw"]);
                    mzqtList[i].Sysj = Convert.ToDateTime(dr["sytime"]);
                    mzqtList[i].Bz = dr["flags"].ToInt32();
                    if (mzqtList[i].Bz == 2)
                    {
                        mzqtList[i].Jssj = Convert.ToDateTime(dr["jstime"]);
                    }
                    i++;
                }
            }
        }

        private void BindShuyeList()
        {
            syZongl = 0;
            shuyeList.Clear();
            DataTable dtSY = bll.selectSY(mzjldID);
            if (dtSY.Rows.Count != 0)
            {
                int i = 0;
                foreach (DataRow dr in dtSY.Rows)
                {
                    adims_MODEL.shuye yt1 = new adims_MODEL.shuye();
                    shuyeList.Add(yt1);
                    shuyeList[i].Id = dr["id"].ToInt32();
                    shuyeList[i].Name = Convert.ToString(dr["shuyename"]);
                    shuyeList[i].Jl = dr["Jl"].ToDouble();
                    syZongl = syZongl + shuyeList[i].Jl;
                    shuyeList[i].Dw = Convert.ToString(dr["dw"]);
                    shuyeList[i].Zrfs = Convert.ToString(dr["Zrfs"]);
                    shuyeList[i].Kssj = Convert.ToDateTime(dr["kssj"]);
                    shuyeList[i].Bz = dr["flags"].ToInt32();
                    if (shuyeList[i].Bz == 2)
                    {
                        shuyeList[i].Jssj = Convert.ToDateTime(dr["jssj"]);
                    }
                    i++;
                }
            }
        }

        private void BindShuxueList()
        {
            sxZongl = 0;
            shuxueList.Clear();
            DataTable dtSX = bll.selectSX(mzjldID);
            if (dtSX.Rows.Count != 0)
            {
                int i = 0;
                foreach (DataRow dr in dtSX.Rows)
                {
                    adims_MODEL.shuxue yt1 = new adims_MODEL.shuxue();
                    shuxueList.Add(yt1);
                    //shuxueList[i].Id = Convert.ToString(dr["id"]);
                    shuxueList[i].Id = dr["id"].ToInt32();
                    shuxueList[i].Name = Convert.ToString(dr["shuxuename"]);
                    shuxueList[i].Jl = dr["Jl"].ToDouble();
                    sxZongl = sxZongl + shuxueList[i].Jl;
                    shuxueList[i].Dw = Convert.ToString(dr["dw"]);
                    shuxueList[i].Zrfs = Convert.ToString(dr["Zrfs"]);
                    shuxueList[i].Kssj = Convert.ToDateTime(dr["kssj"]);
                    shuxueList[i].Bz = dr["flags"].ToInt32();
                    if (shuxueList[i].Bz == 2)
                    {
                        shuxueList[i].Jssj = Convert.ToDateTime(dr["jssj"]);
                    }
                    i++;
                }
            }
        }

        private void BindmzpmList()
        {
            mzpmList.Clear();
            DataTable dtMZPM = bll.GETALLmzpm(mzjldID);
            if (dtMZPM.Rows.Count != 0)
            {
                int i = 0;
                foreach (DataRow dr in dtMZPM.Rows)
                {
                    adims_MODEL.mzpingmian yt1 = new adims_MODEL.mzpingmian();
                    mzpmList.Add(yt1);
                    mzpmList[i].mzpmName = Convert.ToString(dr["mzpmName"]);
                    mzpmList[i].D = Convert.ToDateTime(dr["time"]);
                    mzpmList[i].Id = dr["id"].ToInt32();
                    i++;
                }
            }
        }

        private void BindJmyList()
        {
            jmyList.Clear();
            DataTable dtJMY = bll.selectJumaYao(mzjldID);
            if (dtJMY.Rows.Count != 0)
            {
                int i = 0;
                foreach (DataRow dr in dtJMY.Rows)
                {
                    adims_MODEL.jtytsx yt1 = new adims_MODEL.jtytsx();
                    jmyList.Add(yt1);
                    jmyList[i].Id = dr["Id"].ToInt32();
                    jmyList[i].Name = Convert.ToString(dr["name"]);
                    jmyList[i].Jl = dr["Jl"].ToDouble();
                    if (dr["Cxyy"].ToInt32() == 1)
                        jmyList[i].Cxyy = true;
                    else
                        jmyList[i].Cxyy = false;
                    jmyList[i].Dw = Convert.ToString(dr["dw"]);
                    jmyList[i].Zrfs = Convert.ToString(dr["Zrfs"]);
                    jmyList[i].Kssj = Convert.ToDateTime(dr["kssj"]);
                    jmyList[i].Bz = dr["flags"].ToInt32();
                    if (jmyList[i].Bz == 2)
                    {
                        jmyList[i].Jssj = Convert.ToDateTime(dr["jssj"]);
                    }
                    i++;
                }
            }
        }

        private void BindYdyList()
        {
            DataTable dtYDY = bll.selectYDY(mzjldID);
            if (dtYDY.Rows.Count != 0)
            {
                int i = 0;
                ydyList.Clear();
                foreach (DataRow dr in dtYDY.Rows)
                {
                    adims_MODEL.mzyt yt1 = new adims_MODEL.mzyt();
                    ydyList.Add(yt1);
                    ydyList[i].Id = dr["id"].ToInt32();
                    ydyList[i].Ytname = Convert.ToString(dr["ydyname"]);
                    ydyList[i].Yl = dr["yl"].ToDouble();
                    ydyList[i].Dw = Convert.ToString(dr["dw"]);
                    ydyList[i].Yyfs = Convert.ToString(dr["Yyfs"]);
                    if (dr["Cxyy"].ToInt32() == 1)
                        ydyList[i].Cxyy = true;
                    else
                        ydyList[i].Cxyy = false;
                    ydyList[i].Sysj = Convert.ToDateTime(dr["sytime"]);
                    ydyList[i].Bz = dr["flags"].ToInt32();
                    if (ydyList[i].Bz == 2)
                    {
                        ydyList[i].Jssj = Convert.ToDateTime(dr["jstime"]);
                    }
                    i++;
                }
            }
        }

        private void BindZhenTongYao()
        {
            listBox5.Items.Clear();
            DataTable dt = bll.getMZZTY(mzjldID);
            foreach (DataRow dr in dt.Rows)
            {
                listBox5.Items.Add(dr["name"] + " " + dr["yl"] + dr["dw"]);

            }

        }
        PatOperationDal _PatOperationDal = new PatOperationDal();
        /// <summary>
        /// 绑定手术信息
        /// </summary>
        private void BindOperInfo()
        {
            DataTable dt = _PatOperationDal.GetPatOperation(mzjldID);
            if (dt.Rows.Count > 0)
            {
                string ssss = "";
                foreach (DataRow item in dt.Rows)
                {
                    if (ssss != "")
                    {
                        ssss += " + " + item["OperName"].ToString();
                    }
                    else
                    {
                        ssss += item["OperName"].ToString();
                    }

                }
                ucSSSS.Controls[0].Text = ssss;
                cmbOperLevel.Text = dt.Rows[0]["OperLevel"].ToString();

                cmbCutType.Text = dt.Rows[0]["CutType"].ToString();
            }

        }
        private void BindSZSJ()
        {
            szsj.Clear();
            DataTable dtSZSJ = bll.GETALLSZSJ(mzjldID);
            if (dtSZSJ.Rows.Count != 0)
            {
                int i = 0;
                foreach (DataRow dr in dtSZSJ.Rows)
                {
                    adims_MODEL.szsj yt1 = new adims_MODEL.szsj();
                    szsj.Add(yt1);
                    szsj[i].Id = dr["id"].ToInt32();
                    szsj[i].Name = Convert.ToString(dr["Name"]);
                    szsj[i].D = Convert.ToDateTime(dr["time"]);
                    //szsj[i].Id = Convert.ToDateTime(dr["id"]);
                    i++;
                }
            }
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            for (int i = 0; i < szsj.Count; i++)
            {
                string str11 = SwitchNumToEnglish(i + 1);

                if (i < 10)
                    listBox2.Items.Add(str11 + "." + szsj[i].Name + " " + szsj[i].D.ToString("HH:mm"));
                else
                    listBox3.Items.Add(str11 + "." + szsj[i].Name + "" + szsj[i].D.ToString("HH:mm"));
            }

        }

        private void BindTsyy()
        {
            tsyy.Clear();
            DataTable dt = bll.getTSYY(mzjldID);
            int i = 0;
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    adims_MODEL.tsyy yt1 = new adims_MODEL.tsyy();
                    tsyy.Add(yt1);
                    tsyy[i].Id = dr["ID"].ToInt32();
                    tsyy[i].Name = Convert.ToString(dr["name"]);
                    tsyy[i].Yl = float.Parse(dr["yl"].ToString());
                    tsyy[i].Dw = Convert.ToString(dr["dw"]);
                    tsyy[i].Yyfs = Convert.ToString(dr["yyfs"]);
                    tsyy[i].D = Convert.ToDateTime(dr["time"]);
                    i++;
                }
            }
            int i1 = 1;
            listBox4.Items.Clear();
            foreach (adims_MODEL.tsyy s in tsyy)
            {
                listBox4.Items.Add(i1.ToString() + ". " + s.Name + " " + s.Yl.ToString() + s.Dw + s.Yyfs);
                i1++;
            }
        }
        #endregion


        #region //双击事件
        /// <summary>
        /// 术前诊断双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSqzd_DoubleClick(object sender, EventArgs e)
        {
            SelectSqzd sqzdform = new SelectSqzd(ucSqzd, patID);
            sqzdform.ShowDialog();
        }

        /// <summary>
        /// 术前用药双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSqyy_DoubleClick(object sender, EventArgs e)
        {
            addSqyy sqyyform = new addSqyy(ucSqyy, mzjldID);
            sqyyform.ShowDialog();

        }

        /// <summary>
        /// 合并症双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHbz_DoubleClick(object sender, EventArgs e)
        {
            //hbz hbzform = new hbz(txtXueXing, mzjldID);
            //hbzform.ShowDialog();
        }

        /// <summary>
        /// 拟实手术
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNssss_DoubleClick(object sender, EventArgs e)
        {
            //osel oselform = new osel(txtNssss, patID);
            //oselform.ShowDialog();

        }

        /// <summary>
        /// 术中诊断双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSzzd_DoubleClick(object sender, EventArgs e)
        {
            SelectSqzd szzdform = new SelectSqzd(ucSHZD, mzjldID);
            szzdform.ShowDialog();

        }

        /// <summary>
        /// 麻醉方法双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMzff_DoubleClick(object sender, EventArgs e)
        {
            //mzff mzffform = new mzff(txtMzff, mzjldID);
            //mzffform.ShowDialog();
        }

        /// <summary>
        /// 已实施手术双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtYssss_DoubleClick(object sender, EventArgs e)
        {
            AddOperName yssssform = new AddOperName(ucSSSS, mzjldID);
            yssssform.ShowDialog();
            BindOperInfo();
        }



        //private void txtMzys_DoubleClick(object sender, EventArgs e)
        //{
        //    ssysform ssysform1 = new ssysform(txtMzys);
        //    ssysform1.ShowDialog();
        //}

        /// <summary>
        /// 监护项目双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtJcxm_DoubleClick(object sender, EventArgs e)
        {
            //addjhxm fromjhxm = new addjhxm(jhxma, jhxmy);
            //fromjhxm.ShowDialog();
            //string ssss = "";
            //foreach (string s in jhxmy)
            //{
            //    if (ssss == "")
            //        ssss = s;
            //    else
            //        ssss = ssss + "," + s;
            //}
            //txtSqyy.Controls[0].Text = ssss;
            //pictureBox4.Refresh();

        }

        /// <summary>
        /// 体位双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTw_DoubleClick(object sender, EventArgs e)
        {
            SelectTiwei twform1 = new SelectTiwei(ucTemp, mzjldID);
            twform1.ShowDialog();
        }
        #endregion

        /// <summary>
        /// 监测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        static EventHandler dataEvent;
        DSerialPort _serialPort = DSerialPort.getInstance;


        public bool isexist = false;
        public bool SockectIsException = false;
        public Thread SocketThread;
        static Socket ServerSocket, TempSocket;
        int ReceivedLength = 0;
        public Byte[] Raw_Data = new Byte[11000];
        DateTime ReceivedTime = DateTime.Now;
        string Laststring = ""; //上次收到的数据包
        int count = 0;
        private delegate void FlushClient(); //代理

        private void btnMonitor_Click(object sender, EventArgs e)
        {
            #region //分监护仪类型检测
            if (btnMonitor.Text.Equals("开始监测"))
            {
                if (cmbJianhuyi.Text == "")
                {
                    MessageBox.Show("请选择监护仪机型！");
                    return;

                }
                if (cmbJianhuyi.Text == "GE监护仪")
                {

                    _serialPort.PortName = cmbCOM.Text.Trim();
                    if (_serialPort.IsOpen)
                    {
                        _serialPort.StopTransfer();
                        _serialPort.Close();
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        cmbJianhuyi.Enabled = false;
                        _serialPort.Open();
                        GEmethod();
                    }
                }
                if (cmbJianhuyi.Text == "飞利浦监护仪")
                {
                    FillipFunction();
                    cmbJianhuyi.Enabled = false;
                }
                if (cmbJianhuyi.Text == "迈瑞监护仪")
                {
                    timer2.Stop();
                    timer2.Start();
                    if (!isexist)
                    {

                        cmbJianhuyi.Enabled = false;
                        SocketThread = new Thread(Miray_Socket_Setup);
                        SocketThread.IsBackground = true;
                        SocketThread.Priority = ThreadPriority.AboveNormal;
                        SocketThread.Start();
                        isexist = true;

                    }
                    else
                    {
                        if (SocketThread.ThreadState != ThreadState.Running)
                        {
                            SocketThread.Abort();
                        }
                    }
                    cmbJianhuyi.Enabled = false;
                }
                SaveMzjld();
                ksjcTime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                dal.UpdateMzjldFalgs(1, mzjldID);//2014-05-17
                btnMonitor.Text = "结束监测";
                timer1.Interval = 10 * 1000;
                at.update_mzjld_ZT(mzjldID);//修改麻醉单的状态
                timer1.Enabled = true;
            }
            #endregion
            else
            {
                if (DialogResult.OK == MessageBox.Show("确定结束检测吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                {
                    //迈瑞

                    if (isexist)
                    {
                        if (TempSocket != null)
                            TempSocket.Close();
                        if (ServerSocket != null)
                            ServerSocket.Close();
                        if (SocketThread.ThreadState != ThreadState.Running)
                        {
                            SocketThread.Abort();
                            SocketThread = null; ////在.NET中，有GC垃圾回收，如果这里不赋为null，有可能老半天也不回收内存，甚至永远这么占着，导致内存泄漏。
                        }
                        isexist = false;
                    }
                    timer4.Enabled = false;
                    timer1.Enabled = false;
                    _serialPort.StopTransfer();//2014-04-25修改
                    _serialPort.Close();
                    if (ThreadExist)
                    {
                        ThreadExist = false;
                        Receiving_xy.Abort();//强制结束线程运行
                        Receiving_xy = null; ////在.NET中，有GC垃圾回收，如果这里不赋为null，有可能老半天也不回收内存，甚至永远这么占着，导致内存泄漏。
                    }
                    btnMonitor.Text = "开始监测";
                    cmbJianhuyi.Enabled = true;
                }
            }
        }


        /// <summary>
        /// Time 控件事件
        /// </summary>
        /// <param name="sender"></param>


        #region //GE监护函数
        private void timer4_Tick(object sender, EventArgs e)
        {
            timer4.Interval = 1000 * 10;

        }
        public void GEmethod()
        {
            timer4.Start();
            timer2.Stop();
            timer2.Start();
            try
            {
                if (_serialPort.OSIsUnix())
                {
                    dataEvent += new EventHandler(delegate (object senderGE, EventArgs eGE)
                    {
                        ReadData(senderGE);
                    });
                }
                if (!_serialPort.OSIsUnix())
                {
                    _serialPort.DataReceived += new SerialDataReceivedEventHandler(p_DataReceived);
                }
                short nInterval = 10;
                //string sInterval = Console.ReadLine();
                //if (sInterval != "") nInterval = Convert.ToInt16(sInterval);
                _serialPort.RequestTransfer(DataConstants.DRI_PH_DISPL, nInterval, DataConstants.DRI_LEVEL_2005); // Add Request Transmission
                _serialPort.RequestTransfer(DataConstants.DRI_PH_DISPL, nInterval, DataConstants.DRI_LEVEL_2003); // Add Request Transmission
                _serialPort.RequestTransfer(DataConstants.DRI_PH_DISPL, nInterval, DataConstants.DRI_LEVEL_2001); // Add Request Transmission

                if (_serialPort.OSIsUnix())
                {
                    if (_serialPort.BytesToRead != 0)
                    {
                        dataEvent.Invoke(_serialPort, new EventArgs());
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("COM端口错误！");
            }
        }
        public void p_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            ReadData(sender);
        }

        public void ReadData(object sender)
        {
            try
            {
                // _serialPort.ReadBuffer();
                (sender as DSerialPort).ReadBuffer(mzjldID, 0);
            }
            catch (TimeoutException) { }
        }

        public static void WaitForSeconds(int nsec)
        {
            DateTime dt = DateTime.Now;
            DateTime dt2 = dt.AddSeconds(nsec);
            do
            {
                dt = DateTime.Now;
            }
            while (dt2 > dt);
        }
        #endregion

        #region //飞利浦监护函数

        private void FillipFunction()
        {
            timer2.Stop();
            timer2.Start();
            bool TimerStarted = false;
            string temp1 = IPaddress.Text;
            //string temp2 = this.txtMzjldid.Controls[0].Text;
            IPAddressInput1 = IPaddress.Text;
            //BedIDInput1 = this.txtMzjldid.Controls[0].Text;
            if (!TimerStarted)
            {
                CheckTimer.Start();
                TimerStarted = true;
            }
            SaveConfigure();
            GetConfigure();
            if (!ThreadExist)
            {
                Receiving_xy = new Thread(GatherFillipData);
                Receiving_xy.Start();
                ThreadExist = true;
                //Receiving_xy.Abort();//强制结束线程运行
                //Receiving_xy = null; ////在.NET中，有GC垃圾回收，如果这里不赋为null，有可能老半天也不回收内存，甚至永远这么占着，导致内存泄漏。
            }
        }
        private delegate void setText();//定义一个线程委托
        public void GatherFillipData()
        {
            int i = 0;
            int j = 0;
            int k = 0;
            int t = 0;
            //   Socket server;
            // ip = new IPEndPoint(IPAddress.Parse(ip_txt.Text.Trim()), 24105);
            //定义网络类型，数据连接类型和网络协议UDP

            string temp1 = IPAddressInput1;
            while (true)
            {
                BIS = 0; bis_effective = false;
                PR = 0; pr_effective = false;    //脉率
                RR = 0; rr_effective = false;    //呼吸频率
                SPO2 = 0; spo2_effective = false;//血氧饱和度
                TEMP = 0; temp_effective = false;//温度
                HR = 0; hr_effective = false;    //心率
                SYS = 0; sys_effective = false; //收缩压
                DIA = 0; dia_effective = false; //舒张压
                MAP = 0; map_effective = false; //平均血压
                CVP_SYS = 0; CVP_sys_effective = false; //中心静脉收缩压
                CVP_DIA = 0; CVP_dia_effective = false; //中心静脉舒张压
                CVP_MAP = 0; CVP_map_effective = false; //中心静脉平均血压
                ABP_SYS = 0; ABP_sys_effective = false; //动脉收缩压
                ABP_DIA = 0; ABP_dia_effective = false; //动脉舒张压
                ABP_MAP = 0; ABP_map_effective = false; //动脉平均血压
                MeasureDate = System.DateTime.Now;
                //setText d = new setText(DisplayAllData);
                //this.Invoke(d);
                string logstr;
                int recv_Length = 0;
                /* recv_Length = server.ReceiveFrom(recv_Data,ref phillip);
                  if (recv_Length > 0) MessageBox.Show("indication message is received");*/

                string response_str = "";
                Byte[] AssocReq = new Byte[]                   //AssocReq
            {
             0x0D,0xEC,                                   //SessionHeader  
             0x05,0x08,0x13,0x01,0x00,0x16,0x01,0x02,     //SessionData
             0x80,0x00,0x14,0x02,0x00,0x02,

             0xC1,0xDC,0x31,0x80,0xA0,0x80,0x80,0x01,     //PresentationHeader
             0x01,0x00,0x00,0xA2,0x80,0xA0,0x03,0x00,
             0x00,0x01,0xA4,0x80,0x30,0x80,0x02,0x01,
             0x01,0x06,0x04,0x52,0x01,0x00,0x01,0x30,
             0x80,0x06,0x02,0x51,0x01,0x00,0x00,0x00,
             0x00,0x30,0x80,0x02,0x01,0x02,0x06,0x0C,
             0x2A,0x86,0x48,0xCE,0x14,0x02,0x01,0x00,
             0x00,0x00,0x01,0x01,0x30,0x80,0x06,0x0C,
             0x2A,0x86,0x48,0xCE,0x14,0x02,0x01,0x00,
             0x00,0x00,0x02,0x01,0x00,0x00,0x00,0x00,
             0x00,0x00,0x61,0x80,0x30,0x80,0x02,0x01,
             0x01,0xA0,0x80,0x60,0x80,0xA1,0x80,0x06,
             0x0C,0x2A,0x86,0x48,0xCE,0x14,0x02,0x01,
             0x00,0x00,0x00,0x03,0x01,0x00,0x00,0xBE,
             0x80,0x28,0x80,0x06,0x0C,0x2A,0x86,0x48,
             0xCE,0x14,0x02,0x01,0x00,0x00,0x00,0x01,
             0x01,0x02,0x01,0x02,0x81,

             0x48,                                        //AssocReqUserData
             0x80,0x00,0x00,0x00,0x40,0x00,0x00,0x00,
             0x00,0x00,0x00,0x00,0x80,0x00,0x00,0x00,
             0x20,0x00,0x00,0x00,
             0x00,0x00,0x00,0x00,
             0x00,0x01,0x00,0x2c,
             0x00,0x01,0x00,0x28,
             0x80,0x00,0x00,0x00,0x00,0x00,0x09,0xc4,
             0x00,0x00,0x09,0xc4,0x00,0x00,0x03,0xe8,
             0xff,0xff,0xff,0xff,0x60,0x00,0x00,0x00,
             0x00,0x01,0x00,0x0c,
             0xf0,0x01,0x00,0x08,
             0x80,0x00,0x00,0x00,0x00,0x00,0x00,0x00,  //0x20 表示每60秒应答一次 0x80表示每秒应答一次。 
             
             0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,   //PresentationTrailer
             0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
             };

                server.SendTo(AssocReq, 237, SocketFlags.None, phillip_server);
                //logstr = "Send Association Request : " + MeasureDate.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + BitConverter.ToString(AssocReq).Replace("-", ",0x");
                //Thread.Sleep(Waiting_Period);
                //SaveLog(logstr);

                recv_Length = 0;
                recv_Length = server.ReceiveFrom(recv_Data, ref phillip);
                //logstr = "Received Association Result: " + BitConverter.ToString(recv_Data, 0, recv_Length).Replace("-", ",0x");
                //SaveLog(logstr);
                if (recv_Length > 0)
                {
                    if (recv_Data[0] == 0x0E)  // MessageBox.Show("收到联系响应报文！");
                    {
                        for (i = 0; i < recv_Length; i++)
                        {
                            response_str += recv_Data[i].ToString();
                            response_str += " ";
                        }
                        //   response_txt.Text = response_str;
                        recv_Length = server.ReceiveFrom(recv_Data, ref phillip);// 接收 MDS CREATE EVENT 
                        //logstr = "Received MSD Create Event Report : " + BitConverter.ToString(recv_Data, 0, recv_Length).Replace("-", ",0x");
                        //SaveLog(logstr);

                        if (recv_Length > 0)
                        {
                            //   MessageBox.Show("收到事件报文！");
                            response_str = "";
                            for (i = 0; i < recv_Length; i++)
                            {
                                response_str += recv_Data[i].ToString();
                                response_str += " ";
                            }
                            //eveReasult_txt.Text = response_str;
                        }
                        Byte[] event_result = new Byte[]
                    {
                        0xE1,0x00,0x00,0x02,
                        0x00,0x02,0x00,0x14,
                        0x00,0x01,0x00,0x01,0x00,0x0e,
                        0x00,0x21,0x00,0x00,0x00,0x00,0x00,0x48,
                        0x47,0x00,0x0d,0x06,0x00,0x00
                    };
                        server.SendTo(event_result, 28, SocketFlags.None, phillip_server);
                        //logstr = "Send MSD Create Event Result: " + BitConverter.ToString(event_result).Replace("-", ",0x");
                        //SaveLog(logstr);

                        Thread.Sleep(1000);
                    }
                    if (recv_Data[0] == 0x0C)
                        //MessageBox.Show("收到拒绝联系报文！");
                        if (recv_Data[0] == 0x19)
                        //MessageBox.Show("收到丢弃联系报文！");
                        { }
                }

                Byte[] single_poll_exs = new Byte[]
             {
                 0xE1,0x00,0x00,0x02,
                 0x00,0x01,0x00,0x20,
                 0x00,0x01,0x00,0x07,0x00,0x1a,
                 0x00,0x21,0x00,0x00,0x00,0x00,0x00,0x00,
                 0x00,0x00,0xf1,0x3b,0x00,0x0c,
                 0x00,0x01,0x00,0x01,0x00,0x06,0x08,0x03,
                 0x00,0x00,0x00,0x00
              };
                /*   Byte[] single_pool_data_request =new Byte[]
                   {
                     0xE1, 0x00, 0x00, 0x02,
                     0x00, 0x01, 0x00, 0x1c,
                     0x00, 0x01, 0x00, 0x07, 0x00, 0x16,
                     0x00, 0x21, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                     0x00, 0x00, 0x0c, 0x16, 0x00, 0x08,
                     0x00, 0x01, 0x00, 0x01, 0x00, 0x06, 0x08,0x03  //0x08, 0x03 是 polled_attr_grp， 选择NOM_ATTR_GRP_METRIC_VAL_OBS 0x0803
                   }; */
                int count1 = 0;
                int count2 = 0;
                int count3 = 0;
                int count4 = 0;
                int contemp1 = 0;
                int contemp2 = 0;
                int contemp3 = 0;
                int contemp4 = 0;
                int contemp5 = 0;
                int physio_id = 0;
                //要采集的数据
                LoopCount = 0;
                HasEffectiveData = false;
                // while (true)
                while (LoopCount < MAX_TRY_TIMES && !HasEffectiveData)
                {
                    LoopCount++;
                    Thread.Sleep(500);
                    server.SendTo(single_poll_exs, 40, SocketFlags.None, phillip_server);
                    logstr = "Send single pool ext: " + BitConverter.ToString(single_poll_exs).Replace("-", ",0x");
                    //SaveLog(logstr);
                    //Thread.Sleep(1000);
                    recv_Length = server.ReceiveFrom(recv_Data, ref phillip);
                    logstr = "Received single pool ext response1: " + BitConverter.ToString(recv_Data, 0, recv_Length).Replace("-", ",0x");
                    //SaveLog(logstr);
                    //处理第一个消息， 该消息是一个ROLRS_APDU消息
                    if (recv_Length > 0 && recv_Data[4] == 0x00 && recv_Data[5] == 0x05)
                    {
                        contemp1 = 50 + 2;
                        count1 = recv_Data[46 + 2] * 256 + recv_Data[47 + 2];
                        for (i = 0; i < count1; i++)
                        {
                            contemp2 = contemp1 + 6;
                            count2 = recv_Data[contemp2 - 4] * 256 + recv_Data[contemp2 - 3];
                            for (j = 0; j < count2; j++)
                            {
                                contemp3 = contemp2 + 6;
                                count3 = recv_Data[contemp3 - 4] * 256 + recv_Data[contemp3 - 3];
                                for (k = 0; k < count3; k++)
                                {
                                    if (recv_Data[contemp3] == 0x09 && recv_Data[contemp3 + 1] == 0x50) //Attribute ID: Numeric Observed Value NOM_ATTR_NU_VAL_OBS
                                    {
                                        physio_id = recv_Data[contemp3 + 4] * 256 + recv_Data[contemp3 + 5];
                                        switch (physio_id)
                                        {
                                            //BIS 添加开始
                                            case 0xF04E:
                                                bis_effective = recv_Data[contemp3 + 6] == 0 ? true : false;
                                                if (bis_effective)
                                                    BIS = (int)getFloat(recv_Data, contemp3 + 10);
                                                break;
                                            // 添加结束
                                            case 0x4822:
                                                pr_effective = recv_Data[contemp3 + 6] == 0 ? true : false;
                                                if (pr_effective)
                                                    PR = (int)getFloat(recv_Data, contemp3 + 10);
                                                break;
                                            case 0x500A:
                                                rr_effective = recv_Data[contemp3 + 6] == 0 ? true : false;
                                                if (rr_effective)
                                                    RR = (int)getFloat(recv_Data, contemp3 + 10);
                                                break;
                                            case 0x4BB8:
                                                spo2_effective = recv_Data[contemp3 + 6] == 0 ? true : false;
                                                if (spo2_effective)
                                                    SPO2 = getFloat(recv_Data, contemp3 + 10);
                                                break;
                                            case 0x4B60:
                                                temp_effective = recv_Data[contemp3 + 6] == 0 ? true : false;
                                                if (temp_effective)
                                                    TEMP = getFloat(recv_Data, contemp3 + 10);
                                                break;
                                            case 0x4182:
                                                hr_effective = recv_Data[contemp3 + 6] == 0 ? true : false;
                                                if (hr_effective)
                                                    HR = (int)getFloat(recv_Data, contemp3 + 10);
                                                break;
                                        }
                                    }
                                    else

                                        if (recv_Data[contemp3] == 0x09 && recv_Data[contemp3 + 1] == 0x4B)//Attribute ID: Compound Numeric Observed Value NOM_ATTR_NU_CMPD_VAL_OBS
                                    {
                                        contemp4 = contemp3 + 4;
                                        count4 = recv_Data[contemp4] * 256 + recv_Data[contemp4 + 1];
                                        //                                    for (q = 0; q < count4; q++)
                                        //        {
                                        //            if (recv_Data[contemp4] == 0x4A && recv_Data[contemp4 + 1] == 0x04)
                                        //          {
                                        contemp5 = contemp4 + 4;
                                        for (t = 0; t < count4; t++)
                                        {
                                            physio_id = recv_Data[contemp5] * 256 + recv_Data[contemp5 + 1];
                                            switch (physio_id)
                                            {
                                                //NBP
                                                case 0x4A05:
                                                    sys_effective = recv_Data[contemp5 + 2] == 0 ? true : false;
                                                    if (sys_effective)
                                                        SYS = (int)getFloat(recv_Data, contemp5 + 6);
                                                    break;
                                                case 0x4A06:
                                                    dia_effective = recv_Data[contemp5 + 2] == 0 ? true : false;
                                                    if (dia_effective)
                                                        DIA = (int)getFloat(recv_Data, contemp5 + 6);
                                                    break;
                                                case 0x4A07:
                                                    map_effective = recv_Data[contemp5 + 2] == 0 ? true : false;
                                                    if (map_effective)
                                                        MAP = (int)getFloat(recv_Data, contemp5 + 6);
                                                    break;

                                                //CVP 
                                                case 0x4A45:
                                                    CVP_sys_effective = recv_Data[contemp5 + 2] == 0 ? true : false;
                                                    if (CVP_sys_effective)
                                                        CVP_SYS = (int)getFloat(recv_Data, contemp5 + 6);
                                                    break;
                                                case 0x4A46:
                                                    CVP_dia_effective = recv_Data[contemp5 + 2] == 0 ? true : false;
                                                    if (CVP_dia_effective)
                                                        CVP_DIA = (int)getFloat(recv_Data, contemp5 + 6);
                                                    break;
                                                case 0x4A47:
                                                    CVP_map_effective = recv_Data[contemp5 + 2] == 0 ? true : false;
                                                    if (CVP_map_effective)
                                                        CVP_MAP = (int)getFloat(recv_Data, contemp5 + 6);
                                                    break;
                                                //ABP
                                                case 0x4A15:
                                                    ABP_sys_effective = recv_Data[contemp5 + 2] == 0 ? true : false;
                                                    if (ABP_sys_effective)
                                                        ABP_SYS = (int)getFloat(recv_Data, contemp5 + 6);
                                                    break;
                                                case 0x4A16:
                                                    ABP_dia_effective = recv_Data[contemp5 + 2] == 0 ? true : false;
                                                    if (ABP_dia_effective)
                                                        ABP_DIA = (int)getFloat(recv_Data, contemp5 + 6);
                                                    break;
                                                case 0x4A17:
                                                    ABP_map_effective = recv_Data[contemp5 + 2] == 0 ? true : false;
                                                    if (ABP_map_effective)
                                                        ABP_MAP = (int)getFloat(recv_Data, contemp5 + 6);
                                                    break;
                                            }
                                            contemp5 += 10;
                                        } //End for
                                    }
                                    else
                                            if (recv_Data[contemp3] == 0x09 && recv_Data[contemp3 + 1] == 0x90)//Attribute ID: Absolute Time Stamp NOM_ATTR_TIME_STAMP_ABS
                                    {
                                        //时间字段  BCD格式， 需要转化为十进制
                                        contemp4 = contemp3 + 4;
                                        byte century = recv_Data[contemp4];
                                        int century_dec = century / 16 * 10 + century % 16;
                                        byte year = recv_Data[contemp4 + 1];
                                        int year_dec = year / 16 * 10 + year % 16 + century_dec * 100;
                                        byte month = recv_Data[contemp4 + 2];
                                        int month_dec = month / 16 * 10 + month % 16;
                                        byte day = recv_Data[contemp4 + 3];
                                        int day_dec = day / 16 * 10 + day % 16;
                                        byte hour = recv_Data[contemp4 + 4];
                                        int hour_dec = hour / 16 * 10 + hour % 16;
                                        byte minute = recv_Data[contemp4 + 5];
                                        int minute_dec = minute / 16 * 10 + minute % 16;
                                        byte second = recv_Data[contemp4 + 6];
                                        int second_dec = second / 16 * 10 + second % 16;
                                        byte sec_fractions = recv_Data[contemp4 + 7];
                                        int sec_fractions_dec = sec_fractions / 16 * 10 + sec_fractions % 16;
                                        MeasureDate = new DateTime(year_dec, month_dec, day_dec, hour_dec, minute_dec, second_dec);
                                    }
                                    contemp3 += 4 + recv_Data[contemp3 + 2] * 256 + recv_Data[contemp3 + 3];
                                }
                                contemp2 += 6 + recv_Data[contemp2 + 4] * 256 + recv_Data[contemp2 + 5];
                            }
                            contemp1 += 6 + recv_Data[contemp1 + 4] * 256 + recv_Data[contemp1 + 5];
                        }
                        //setText ttd = new setText(DisplayAllData);
                        //this.Invoke(d);
                        HasEffectiveData = bis_effective || hr_effective || rr_effective || pr_effective || temp_effective || spo2_effective || sys_effective || dia_effective || map_effective || CVP_map_effective || ABP_map_effective;
                        HasEffectivePressure = map_effective || CVP_map_effective || ABP_map_effective;
                        //logstr = "HR=" + HR.ToString() + ";RR=" + RR.ToString() + ";SPO2=" + SPO2.ToString() + ";sys=" + SYS.ToString() + ";DIA=" + DIA.ToString() + ";MAP=" + MAP.ToString() + ";CVP_MAP=" + CVP_MAP.ToString() + ";ABP_MAP=" + ABP_MAP.ToString();
                        //SaveLog(logstr);
                    }
                    if (recv_Length > 0 && recv_Data[4] == 0x00 && recv_Data[5] == 0x05)  //第一条消息收到，有后续存在的消息要接收， 第二次接受
                    {
                        //第二个消息字符串， 里面有几个字节， 但是是空的
                        // Thread.Sleep(Waiting_Period);
                        recv_Length = server.ReceiveFrom(recv_Data, ref phillip);
                        //logstr = "Received single pool ext response2: " + BitConverter.ToString(recv_Data, 0, recv_Length).Replace("-", ",0x");
                        //SaveLog(logstr);
                    }
                    if (recv_Length > 0 && recv_Data[4] == 0x00 && recv_Data[5] == 0x05)  //准备接受最后一个消息内容。
                    {
                        //接受第三个消息字符串， 里面有几个字节， 但是没有什么内容，不予处理
                        //Thread.Sleep(Waiting_Period);
                        recv_Length = server.ReceiveFrom(recv_Data, ref phillip);
                        //logstr = "Received single pool ext response3: " + BitConverter.ToString(recv_Data, 0, recv_Length).Replace("-", ",0x");
                        //SaveLog(logstr);
                    }
                    if (recv_Length > 0 && recv_Data[4] == 0x00 && recv_Data[5] == 0x02)  //最后一条消息收到
                    {

                    }
                }
                int b = 0;
                if (HasEffectiveData)
                {
                    string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    MonitorRecord mRecord = new MonitorRecord();
                    mRecord.MZJLDID = mzjldID;
                    mRecord.TIME = now;
                    if (ABP_SYS != 0 && ABP_DIA != 0 && ABP_MAP != 0)
                    {
                        mRecord.SYS = ABP_SYS;
                        mRecord.DIA = ABP_DIA;
                        mRecord.MEAN = ABP_MAP;
                    }
                    else
                    {
                        mRecord.SYS = SYS;
                        mRecord.DIA = DIA;
                        mRecord.MEAN = MAP;
                    }
                    //如果呼吸小于0，就模拟数据
                    if (RR <= 0)
                    {
                        RR = (int)(new Random().Next(14, 20));
                    }
                    mRecord.RR = RR;
                    mRecord.HR = HR;
                    mRecord.Pulse = PR;
                    mRecord.SPO2 = (int)SPO2;
                    mRecord.ETCO2 = new Random().Next(32, 35);
                    mRecord.TEMP = TEMP;
                    mRecord.BIS = BIS;
                    mRecord.TOF = TOF;



                    this.SaveFillipData(mRecord, 0);
                }
                Byte[] Associate_Release_Request = new Byte[]
             {
                 0x09, 0x18,
                 0xC1, 0x16, 0x61, 0x80, 0x30, 0x80, 0x02, 0x01,
                 0x01, 0xA0, 0x80, 0x62, 0x80, 0x80, 0x01, 0x00,
                 0x00, 0x00, 0x00, 0x00,
                 0x00, 0x00, 0x00, 0x00
              };
                server.SendTo(Associate_Release_Request, 26, SocketFlags.None, phillip_server);

                //logstr = "Send Associate Release Request: " + BitConverter.ToString(Associate_Release_Request).Replace("-", ",0x");
                // SaveLog(logstr);
                // Thread.Sleep(Waiting_Period);
                recv_Length = server.ReceiveFrom(recv_Data, ref phillip);
                //logstr = "Received Associate Release Result: " + BitConverter.ToString(recv_Data, 0, recv_Length).Replace("-", ",0x");
                //SaveLog(logstr);

                Byte[] Associate_Release_Response = new Byte[]
             {
                 0x0A, 0x18,

                 0xC1, 0x16, 0x61, 0x80, 0x30, 0x80, 0x02, 0x01,
                 0x01, 0xA0, 0x80, 0x63, 0x80, 0x80, 0x01, 0x00,
                 0x00, 0x00, 0x00, 0x00,
                 0x00, 0x00, 0x00, 0x00
              };

                Byte[] Associate_Abort = new Byte[]
             {
                 0x19, 0x2E,
                 0x11, 0x01, 0x03,
                 0xC1, 0x29, 0xA0, 0x80, 0xA0, 0x80, 0x30, 0x80,
                 0x02, 0x01, 0x01, 0x06, 0x02, 0x51, 0x01, 0x00,
                 0x00, 0x00, 0x00, 0x61, 0x80, 0x30, 0x80, 0x02,
                 0x01, 0x01, 0xA0, 0x80, 0x64, 0x80, 0x80, 0x01,
                 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                 0x00, 0x00, 0x00, 0x00
              };
                if (recv_Data[0] == Associate_Release_Response[0] && recv_Data[1] == Associate_Release_Response[1])
                {
                    //Received correct release Response
                    //MessageBox.Show("连接正确退出！");
                }
                else if (recv_Data[0] == Associate_Abort[0] && recv_Data[1] == Associate_Abort[1])
                {
                    //MessageBox.Show("收到连接中断消息！");
                }
                else
                {
                    //MessageBox.Show("收到无法识别消息！");
                }
                Thread.Sleep(Get_Period);
                GetDataTimes++;
            }
        }
        //public struct PatientDataBlock
        //{
        //    public DateTime MeasureTime;
        //    public int BIS; public bool bis_effective;    //麻醉深度
        //    public int TOF; public bool tof_effective;    //肌松恢复
        //    public int ETCO2; public bool etco2_effective;    //etco2
        //    public int PR; public bool pr_effective;    //脉率
        //    public int RR; public bool rr_effective;    //呼吸频率
        //    public float SPO2; public bool spo2_effective;//血氧饱和度
        //    public float TEMP; public bool temp_effective;//温度
        //    public int HR; public bool hr_effective;    //心率
        //    public int SYS; public bool sys_effective; //收缩压
        //    public int DIA; public bool dia_effective; //舒张压
        //    public int MAP; public bool map_effective; //平均血压
        //    public int CVP_SYS; public bool CVP_sys_effective; //中心静脉收缩压
        //    public int CVP_DIA; public bool CVP_dia_effective; //中心静脉舒张压
        //    public int CVP_MAP; public bool CVP_map_effective; //中心静脉平均血压
        //    public int ABP_SYS; public bool ABP_sys_effective; //动脉收缩压
        //    public int ABP_DIA; public bool ABP_dia_effective; //动脉舒张压
        //    public int ABP_MAP; public bool ABP_map_effective; //动脉平均血压

        //}
        public void SaveFillipData(MonitorRecord info, int type)
        {


            //adims_BLL.UserFunction.SaveMonitorLog(string.Format("SYS:{0} DIA:{1} MAP:{2} RR:{3} HR:{4} PR:{5} SPO2:{6} TEMP:{7}", ABP_SYS1, ABP_DIA1, ABP_MAP1, RR1, HR1, PR1, SPO21,TEMP1));
            if (sh.selectJianCeData(info.TIME.ToString(), mzjldID, type).Rows.Count == 0)
            {
                int fa = 0;
                if (type == 0)
                    fa = sh.insertMonitorRecord(info);
                //fa = sh.insertJianCeDataMZJLD(mzjldID, ABP_SYS1, ABP_DIA1, ABP_MAP1, RR1, HR1, PR1, SPO21, , TEMP1,BIS1, now);
                if (type == 1)
                    fa = sh.insertMonitorRecord_PACU(info);
                //fa = sh.insertJianCeDataPACU(mzjldID, ABP_SYS1, ABP_DIA1, ABP_MAP1, RR1, HR1, PR1, SPO21, new Random().Next(32, 35), TEMP1, now);

            }
        }

        private float getFloat(Byte[] flt, int start)
        {
            int exp = 0;
            long m = 0;
            float value;
            if (flt[start] > 127)
            {
                exp = -1 * (256 - flt[start]);
            }
            else
                exp = flt[start];
            if (flt[start + 1] > 127)
            {
                m = -1 * (long)(System.Math.Pow(2, 24) - flt[start + 1] * System.Math.Pow(2, 16)
                    - flt[start + 2] * System.Math.Pow(2, 8) - flt[start + 3]);
            }
            else
                m = (long)(flt[start + 1] * System.Math.Pow(2, 16) + flt[start + 2] * System.Math.Pow(2, 8)
                    + flt[start + 3]);
            value = (float)(m * System.Math.Pow(10, exp));
            return value;
        }
        private void SaveConfigure()
        {
            IPConfigureInfo IpConf;
            IpConf.PatientIPAddress = IPAddressInput1; IpConf.BedID = BedIDInput1;
            FileStream fs = new FileStream(Application.StartupPath + "\\Config.txt", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.WriteLine(IpConf.PatientIPAddress);
            sw.WriteLine(IpConf.BedID);
            sw.Close();
            fs.Close();
        }
        private void GetConfigure()
        {
            IPConfigureInfo IpConf;
            FileStream fs = new FileStream(Application.StartupPath + "\\Config.txt", FileMode.OpenOrCreate);
            StreamReader sw = new StreamReader(fs, Encoding.Default);
            IpConf.PatientIPAddress = sw.ReadLine();
            IpConf.BedID = sw.ReadLine();
            sw.Close();
            fs.Close();
            IPAddressInput1 = IpConf.PatientIPAddress; BedIDInput1 = IpConf.BedID;

        }
        public struct IPConfigureInfo
        {
            public string PatientIPAddress; public string BedID;
        }
        #endregion

        #region 迈瑞监护函数
        private void Miray_Socket_Setup()
        {
            string temp1 = "10.3.13.195";
            string temp2 = "4602";
            int port = temp2.ToInt32();
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(temp1.Trim()), port);
            ServerSocket = new Socket(ipep.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {


                //绑定本机的一个端口，然后进行监听
                ServerSocket.Bind(ipep);
                Thread.Sleep(200);
                ServerSocket.Listen(5);

                TempSocket = ServerSocket.Accept();
                while (true)
                {

                    ReceivedLength = TempSocket.Receive(Raw_Data);
                    ReceivedTime = DateTime.Now;
                    string ReceivedString1 = System.Text.Encoding.Default.GetString(Raw_Data);
                    if (Laststring != ReceivedString1)
                        MirayReceivedData(0);
                    Laststring = ReceivedString1;
                }
            }
            catch (Exception)
            {
                SockectIsException = true;

                //MessageBox.Show("Socket 连接或接收发出错!!!!!" + exception.Message + "\r\n");
                //clientSocket.Close();

                //SaveLog("Socket 连接或接收发出错!!!!!" + exception.Message + "\r\n");

            }
        }
        private void MirayReceivedData(int type)
        {

            try
            {


                string TempString = System.Text.Encoding.Default.GetString(Raw_Data);
                string FinalString = TempString.Substring(0, ReceivedLength);
                string DisplayStr = "数据包数" + count.ToString() +
                       "(" + ReceivedLength.ToString() + ")字节:\r\n" + FinalString;
                //if (this.ReceivedDataList.InvokeRequired) //异步等待
                //{
                //    FlushClient fc = new FlushClient(ProceedreceivedData);
                //    this.Invoke(fc);//通过代理调用刷新方法
                //}
                //else
                //{
                //    count++;

                //    this.ReceivedDataList.Items.Add(DisplayStr);

                //}
                //SaveLog(DisplayStr);
                //SaveLog("--------------------------开始解析-------------------------------\r\n");

                //解析字符串开始
                string[] StringTemp = FinalString.Split('\r');
                //有用参数定义汇总
                //PV1内有用信息
                //string OfficeName ;
                //char[] OfficeName=new char[30];
                string OfficeName = "";
                int BedId = 0, MSH_Count = 0;
                string PatientID = "";
                string IPSeq = "";
                string MeasureTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm"); //测量时间

                int HR = 0, SpO2 = 0, PVCs = 0, RR = 0, PR = 0, Dia = 0, Mean = 0, Sys = 0, CVPm = 0, EtCO2 = 0, Bis = 0, Tof = 0;
                foreach (string BasicCommand in StringTemp)
                {
                    //if (this.ReceivedDataList.InvokeRequired) //异步等待
                    //{
                    //    FlushClient fc = new FlushClient(ProceedreceivedData);
                    //    this.Invoke(fc);//通过代理调用刷新方法
                    //}
                    //else
                    //{
                    //    this.ReceivedDataList.Items.Add(BasicCommand);

                    //}
                    //SaveLog(BasicCommand);

                    //"OBX||CE|2303^PACE_Switch||0^关||||||F"
                    bool IsMSH = BasicCommand.Contains("MSH");
                    bool IsPID = BasicCommand.Contains("PID");
                    bool IsPV1 = BasicCommand.Contains("PV1");
                    bool IsOBR = BasicCommand.Contains("OBR");
                    bool IsOBX = BasicCommand.Contains("OBX");
                    if (IsMSH)   // MSH命令， 不作处理
                    {
                        MSH_Count++;
                        if (MSH_Count > 1)
                        {
                            //有两个以上MSH信息， 需要处理和存储
                            string Testresultstr = "测试结果为:" +
                                "|PatientID=" + PatientID.ToString() +
                                "|BedID=" + BedId.ToString() +
                                "|MeasureTime=" + MeasureTime +
                                "|HR=" + HR.ToString() +
                                "|SpO2=" + SpO2.ToString() +
                                "|PVCs=" + PVCs.ToString() +
                                "|RR=" + RR.ToString() +
                                "|PR=" + PR.ToString() +
                                "|Dia=" + Dia.ToString() +
                                "|Mean=" + Mean.ToString() +
                                "|Sys=" + Sys.ToString() +
                                "|CVPm=" + CVPm.ToString() +
                                "|EtCO2=" + EtCO2.ToString(); ;

                            SaveMirayData(OfficeName, BedId, PatientID, IPSeq, MeasureTime, HR, SpO2, PVCs, RR, PR, Dia, Mean, Sys, CVPm, EtCO2);
                            OfficeName = "";
                            PatientID = "";
                            BedId = 0;
                            IPSeq = "";
                            MeasureTime = ""; //测量时间
                            HR = 0; SpO2 = 0; PVCs = 0; RR = 0; PR = 0; Dia = 0; Mean = 0; Sys = 0;
                            CVPm = 0; EtCO2 = 0;
                        }
                    }
                    if (IsPID)  //"PID|||837132^^^Hospital^PI||王^书梅^^^^^L|||F","
                    {
                        int position = BasicCommand.IndexOf('^');
                        PatientID = BasicCommand.Substring(6, position - 6);
                    }
                    if (IsPV1) //"PV1||I|ICU^^12"
                    {
                        int position = BasicCommand.LastIndexOf('^');
                        string tmp = BasicCommand.Substring(position + 1, BasicCommand.Length - position - 1);
                        if (IsNumberic(tmp))
                            BedId = tmp.ToInt32();

                    }
                    if (IsOBR)  //"OBR|1|15419^MINDRAY_EGATEWAY^00A0370027000143^EUI-64|15419^MINDRAY_EGATEWAY^00A0370027000143^EUI-64|69952^MDC_DEV_MON_PT_PHYSIO_MULTI_PARAM^MDC|||20140606112039+0800"
                    {
                        int position = BasicCommand.LastIndexOf('|');
                        if (position > 0)
                        {
                            MeasureTime = BasicCommand.Substring(position + 1, 14);
                        }
                    }
                    if (IsOBX)  //"OBX|1|NM|147842^MDC_ECG_HEART_RATE^MDC|1.7.4.147842|125|264864^MDC_DIM_BEAT_PER_MIN^MDC|||||R|||20140606112039+0800||||F037EF2X^SHENZHEN_DEVICE^mindray.com^DNS"
                    {
                        int position = BasicCommand.IndexOf('^');
                        int startposition;
                        string TestName = "", TestValueStr = "";
                        int TestValue = 0;
                        if (position > 0)
                        {
                            //???可以屏蔽掉不正常的字符串 例如"OBX||CE|4"
                            startposition = position + 1;
                            position++;
                            while (BasicCommand[position] != '^')
                                position++;
                            TestName = BasicCommand.Substring(startposition, position - startposition);

                            position++;
                            while (BasicCommand[position] != '|')
                                position++;
                            startposition = position + 1;
                            position++;
                            while (BasicCommand[position] != '|')
                                position++;

                            startposition = position + 1;
                            position++;
                            while (BasicCommand[position] != '|')
                                position++;
                            TestValueStr = BasicCommand.Substring(startposition, position - startposition);
                            if (TestValueStr == "")
                            {
                                TestValue = 0;
                            }
                            else
                            {
                                TestValue = int.Parse(TestValueStr);
                            }
                        }
                        if (TestName == "MDC_ECG_HEART_RATE") //心率 bpm
                        {
                            HR = TestValue;
                        }
                        if (TestName == "MDC_TTHOR_RESP_RATE") // RESP  rpm (用胸电极电阻变化测呼吸频率)
                        {
                            RR = TestValue;
                        }
                        if (TestName == "MDC_PULS_OXIM_SAT_O2") // 血氧 %
                        {
                            SpO2 = TestValue;
                        }
                        if (TestName == "MDC_PULS_OXIM_PULS_RATE") // 脉率 bmp
                        {
                            PR = TestValue;
                        }
                        if (TestName == "MDC_PRESS_BLD_VEN_CENT_MEAN") // CVPm 中心静脉压平均 mmHg
                        {
                            CVPm = TestValue;
                        }
                        if (TestName == "MDC_CONC_AWAY_CO2_ET") // 呼末二氧化碳 %
                        {
                            EtCO2 = TestValue;
                        }
                        if (TestName == "MDC_ECG_V_P_C_RATE") // PVC sum  /min (每分钟早期心室收缩的次数)
                        {
                            PVCs = TestValue;
                        }
                        if (TestName == "MDC_PRESS_BLD_VEN_CENT_MEAN") // CVPm 中心静脉压平均 mmHg
                        {
                            CVPm = TestValue;
                        }

                        if (TestName == "MDC_PRESS_BLD_VEN_CENT_MEAN") // CVPm 中心静脉压平均 mmHg
                        {
                            CVPm = TestValue;
                        }
                        if (TestName == "MDC_PRESS_BLD_VEN_CENT_MEAN") // CVPm 中心静脉压平均 mmHg
                        {
                            CVPm = TestValue;
                        }
                        if (TestName == "MDC_PRESS_CUFF_DIA") // 舒张压
                        {
                            Dia = TestValue;
                        }
                        if (TestName == "MDC_PRESS_CUFF_MEAN") // 平均无创血压 mmHg
                        {
                            Mean = TestValue;
                        }
                        if (TestName == "MDC_PRESS_CUFF_SYS") // 收缩压
                        {
                            Sys = TestValue;
                        }
                    }
                }
                string Testresultstr1 = "测试结果为:" +
                          "|PatientID=" + PatientID.ToString() +
                          "|BedID=" + BedId.ToString() +
                          "|MeasureTime=" + MeasureTime +
                          "|HR=" + HR.ToString() +
                          "|SpO2=" + SpO2.ToString() +
                          "|PVCs=" + PVCs.ToString() +
                          "|RR=" + RR.ToString() +
                          "|PR=" + PR.ToString() +
                          "|Dia=" + Dia.ToString() +
                          "|Mean=" + Mean.ToString() +
                          "|Sys=" + Sys.ToString() +
                          "|CVPm=" + CVPm.ToString() +
                          "|EtCO2=" + EtCO2.ToString();

                MirayModel jhModel = new MirayModel();
                jhModel.OfficeName1 = OfficeName;
                jhModel.BedId1 = mzjldID;
                //jhModel.PatientMonitor_IP1 = PatientMonitor_IP;
                jhModel.IPSeq1 = IPSeq;

                jhModel.HR1 = HR;
                jhModel.SpO21 = SpO2;
                jhModel.PVCs1 = PVCs;
                jhModel.RR1 = RR;
                jhModel.PR1 = PR;
                jhModel.Dia1 = Dia;
                jhModel.Mean1 = Mean;
                jhModel.Sys1 = Sys;
                string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                MonitorRecord mRecord = new MonitorRecord();
                mRecord.MZJLDID = mzjldID;
                mRecord.TIME = now;
                mRecord.SYS = Sys;
                mRecord.DIA = Dia;
                mRecord.MEAN = Mean;
                if (RR <= 0)
                {
                    RR = (int)(new Random().Next(14, 20));
                }
                mRecord.RR = RR;
                mRecord.HR = HR;
                mRecord.Pulse = PR;
                mRecord.SPO2 = SpO2;
                mRecord.ETCO2 = EtCO2;
                mRecord.TEMP = 0;
                mRecord.BIS = Bis;
                mRecord.TOF = Tof;
                //float temp = float.Parse((37.5).ToString());                
                //DateTime now = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                if (sh.selectJianCeData(now, mzjldID, type).Rows.Count == 0)
                {
                    int fa = 0;

                    if (type == 0)
                        fa = sh.insertMonitorRecord(mRecord);
                    //fa = sh.insertJianCeDataMZJLD(mzjldID, Sys, Dia, Mean, RR, HR, PR, SpO2, new Random().Next(32, 35), 0,BIS, now);
                    if (type == 1)
                        fa = sh.insertJianCeDataPACU(mzjldID, Sys, Dia, Mean, RR, HR, PR, SpO2, new Random().Next(32, 35), 0, now);
                }

            }
            catch (Exception)
            {


            }
        }
        /// <summary>
        /// 判断字符串是否可以转化为数字
        /// </summary>
        /// <param name="str">要检查的字符串
        /// <returns>true:可以转换为数字；false：不是数字</returns>
        public bool IsNumberic(string str)
        {
            double vsNum;
            bool isNum;
            isNum = double.TryParse(str, System.Globalization.NumberStyles.Float,
                System.Globalization.NumberFormatInfo.InvariantInfo, out vsNum);
            return isNum;
        }
        private void SaveMirayData(string OfficeName, int BedId, string PatientID, string IPSeq, string MeasureTime, float HR, float SpO2, float PVCs, float RR, float PR, float Dia, float Mean, float Sys, float CVPm, float EtCO2)
        {
            //存储测量结果到数据库中... 根据需要自己定义数据库字段
            //return;

        }


        #endregion

        int warmingCount = 0;
        private void timer1_Tick(object sender, EventArgs e)//循环监测点记录表，摘录数据到显示表
        {
            //int isinnet;
            //if (!InternetGetConnectedState(out isinnet, 0))
            //{
            //    // timer1.Enabled = false;
            //    if (warmingCount == 0)
            //    {
            //        MessageBox.Show("服务器中断，请检查网线");
            //        warmingCount++;
            //    }
            //    return;
            //}
            //if (!UserFunction.PingHost(Program.Globals.SeverIp))
            //{
            //    return;
            //}
            //int countN = sh.CopyData(mzjldID, ksjcTime);//挑监护数据插入服务器的Adims_mzjld_Point表里
            //if (countN == 0)
            //{
            //    ksjcTime = ksjcTime.AddMinutes(1);
            //    timer1.Interval = 1000 * 1 * 60;
            //}
            //else
            //{
            //    ksjcTime = ksjcTime.AddMinutes(jcsjjg);//设置刷新时间
            //    timer1.Interval = 1000 * jcsjjg * 60;
            //    BindJHDian();
            //}
            //pictureBox3.Refresh();
            //pictureBox4.Refresh();
        }
        [DllImport("wininet")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reserverdaValue);
        public void BindJHDian()//监护点赋值
        {
            jhxmValueList.Clear();
            ssyPoint.Clear(); szyPoint.Clear(); twPoint.Clear(); hxlPoint.Clear();
            mboPoint.Clear(); spo2List.Clear();
            etco2List.Clear(); bisList.Clear(); cvpList.Clear();
            //RecordTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP
            DataTable datadt = dal.GetAdims_mzjld_Point(mzjldID);
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.point p1 = new adims_MODEL.point();//收缩压记录点
                p1.D = Convert.ToDateTime(datadt.Rows[i][0]);
                p1.V = UserFunction.ToInt32(datadt.Rows[i][1]);
                p1.Lx = 1;
                ssyPoint.Add(p1);
            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.point p2 = new adims_MODEL.point();//舒张压记录点
                p2.D = Convert.ToDateTime(datadt.Rows[i][0]);
                p2.V = UserFunction.ToInt32(datadt.Rows[i][2]);
                p2.Lx = 2;
                szyPoint.Add(p2);
            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.point p3 = new adims_MODEL.point();//脉搏记录点
                p3.D = Convert.ToDateTime(datadt.Rows[i][0]);
                p3.V = UserFunction.ToInt32(datadt.Rows[i][3]);
                p3.Lx = 3;
                mboPoint.Add(p3);
            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.point p4 = new adims_MODEL.point();//呼吸记录点
                p4.D = Convert.ToDateTime(datadt.Rows[i][0]);
                p4.V = UserFunction.ToInt32(datadt.Rows[i][4]);
                p4.Lx = 4;
                hxlPoint.Add(p4);
            }
            if (isTiwenView)
            {
                for (int i = 0; i < datadt.Rows.Count; i++)
                {
                    adims_MODEL.pointF p5 = new adims_MODEL.pointF();//体温
                    p5.D = Convert.ToDateTime(datadt.Rows[i][0]);
                    p5.V = (float)UserFunction.ToDouble(datadt.Rows[i][5]);
                    p5.Lx = 5;
                    if (p5.V != 0)
                    {
                        twPoint.Add(p5);
                    }

                }
            }

            for (int i = 0; i < datadt.Rows.Count; i++)
            {

                adims_MODEL.jhxm jhxmt = new adims_MODEL.jhxm();
                jhxmt.D = Convert.ToDateTime(datadt.Rows[i][0]);
                jhxmt.V = UserFunction.ToInt32(datadt.Rows[i]["SPO2"]);
                jhxmt.Sy = "SpO2";
                jhxmValueList.Add(jhxmt);
            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.jhxm jhxmt = new adims_MODEL.jhxm();
                jhxmt.D = Convert.ToDateTime(datadt.Rows[i][0]);
                jhxmt.V = UserFunction.ToInt32(datadt.Rows[i]["ETCO2"]);
                jhxmt.Sy = "ETCO2";
                jhxmValueList.Add(jhxmt);
            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.jhxm jhxmt = new adims_MODEL.jhxm();
                jhxmt.D = Convert.ToDateTime(datadt.Rows[i][0]);
                jhxmt.V = UserFunction.ToInt32(datadt.Rows[i]["TOF"]);
                jhxmt.Sy = "TOF";
                jhxmValueList.Add(jhxmt);
            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.jhxm jhxmt = new adims_MODEL.jhxm();
                jhxmt.D = Convert.ToDateTime(datadt.Rows[i][0]);
                jhxmt.V = UserFunction.ToInt32(datadt.Rows[i]["BIS"]);
                jhxmt.Sy = "BIS";
                jhxmValueList.Add(jhxmt);
            }


            #region//模拟数据点
            ////↓查询收缩压记录点集合
            //DataTable ssydt = dal.GetALL_Point(mzjldID, 1);
            //for (int i = 0; i < ssydt.Rows.Count; i++)
            //{
            //    adims_MODEL.point p = new adims_MODEL.point();//收缩压记录点
            //    p.D = Convert.ToDateTime(ssydt.Rows[i][4]);
            //    p.V = ssydt.Rows[i][3]);
            //    p.Lx = 1;
            //    ssy.Add(p);
            //}

            ////↓查询舒张压压记录点集合
            //DataTable szydt = dal.GetALL_Point(mzjldID, 2);
            //for (int i = 0; i < szydt.Rows.Count; i++)
            //{
            //    adims_MODEL.point p1 = new adims_MODEL.point();//舒张压记录点
            //    p1.D = Convert.ToDateTime(szydt.Rows[i][4]);
            //    p1.V = szydt.Rows[i][3]);
            //    p1.Lx = 2;
            //    szy.Add(p1);

            //}
            ////↓查询心率记录点集合
            //DataTable xldt = dal.GetALL_Point(mzjldID, 3);
            //for (int i = 0; i < xldt.Rows.Count; i++)
            //{
            //    adims_MODEL.point p2 = new adims_MODEL.point();//心率记录点
            //    p2.D = Convert.ToDateTime(xldt.Rows[i][4]);
            //    p2.V = xldt.Rows[i][3]);
            //    p2.Lx = 3;
            //    xl.Add(p2);

            //}
            ////↓查询体温集合
            //DataTable twdt = dal.GetALL_Point(mzjldID, 4);
            //for (int i = 0; i < twdt.Rows.Count; i++)
            //{
            //    adims_MODEL.point p4 = new adims_MODEL.point();//体温记录点
            //    p4.D = Convert.ToDateTime(twdt.Rows[i][4]);
            //    p4.V = twdt.Rows[i][3]);
            //    p4.Lx = 4;
            //    tw.Add(p4);

            //}
            ////↓查询呼吸率记录点集合
            //DataTable hxdt = dal.GetALL_Point(mzjldID, 5);
            //for (int i = 0; i < hxdt.Rows.Count; i++)
            //{
            //    adims_MODEL.point p3 = new adims_MODEL.point();//呼吸率记录点

            //    p3.D = Convert.ToDateTime(hxdt.Rows[i][4]);
            //    p3.V = hxdt.Rows[i][3]);
            //    p3.Lx = 5;
            //    hxl.Add(p3);
            //}
            #endregion

        }

        public void BindCLlist()// 出量赋值
        {
            clcxqt.Clear();
            double cnl = 0;
            DataTable dtCL = dal.GetCL(mzjldID);
            if (dtCL.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in dtCL.Rows)
                {
                    adims_MODEL.clcxqt CLCXQT = new adims_MODEL.clcxqt();
                    clcxqt.Add(CLCXQT);
                    clcxqt[i].Id = dtCL.Rows[i][0].ToInt32();
                    clcxqt[i].D = Convert.ToDateTime(dtCL.Rows[i][4]);
                    clcxqt[i].V = dtCL.Rows[i][3].ToInt32();
                    cnl = cnl + clcxqt[i].V;
                    clcxqt[i].Lx = dtCL.Rows[i][2].ToInt32();
                    i++;
                }
                if (cnl > 0)
                {
                    ucNiaoLiang.Controls[0].Text = cnl.ToString();
                }


            }
        }

        private void btnSzsj_Click(object sender, EventArgs e)
        {
            addszsj fromszsj = new addszsj(szsj, mzjldID, 0);
            fromszsj.ShowDialog();
            BindSZSJ();
            pictureBox4.Refresh();
            listBox2.Focus();
        }

        private void btnTsyy_Click(object sender, EventArgs e)
        {
            addtsyy fromtsyy = new addtsyy(tsyy, mzjldID);
            fromtsyy.ShowDialog();
            BindTsyy();
            pictureBox4.Refresh();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            addZhenTong fromtsyy = new addZhenTong(mzjldID);
            fromtsyy.ShowDialog();
            BindZhenTongYao();
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreDia.PrintPreviewControl.Zoom = 1.5;
            printPreDia.WindowState = FormWindowState.Maximized;
            printDoc.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("K16", 780, 1080);

        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = 5, y = 0;//整体位置控制
            string title = "江苏盛泽医院、江苏省人民医院盛泽分院";//标题  
            string title1 = "麻醉记录单";//标题  
            Pen ptp = Pens.Black;//普通画笔
            Pen pblack2 = new Pen(Brushes.Black, 2);
            Pen pxuxian = new Pen(Brushes.Black);
            pxuxian.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Font ptzt14 = new System.Drawing.Font("宋体", 14);//标题
            Font ptzt13 = new System.Drawing.Font("宋体", 13);//标题                      
            Font ptzt12 = new Font("宋体", 12);//填入栏目字体
            Font ptzt11 = new Font("宋体", 11);//填入栏目字体
            Font ptzt10 = new Font("宋体", 10);//填入栏目字体
            Font ptzt9 = new Font("宋体", 9);//填入栏目字体
            Font ptzt7 = new Font("宋体", 7);//填入栏目字体           
            Font ptzt7_U = new Font("宋体", 7, FontStyle.Underline, GraphicsUnit.Point);
            Font ptzt8 = new Font("宋体", 7);//填入栏目字体
            Font ht6 = new Font("微软雅黑", 6);//填入栏目字体
            Font ht7 = new Font("微软雅黑", 7);//填入栏目字体
            Font ptzt6 = new Font("宋体", 5);//填入栏目字体
            Font ptzt5 = new Font("宋体", 5);//填入栏目字体
            Pen pred2 = new Pen(Brushes.Red, 2);
            e.Graphics.DrawString(title, ptzt13, Brushes.Black, new Point(190 + x, 20 + y));
            e.Graphics.DrawString(title1, ptzt13, Brushes.Black, new Point(300 + x, 45 + y));

            int Y_unLine = y + 63, YY = y + 50;
            e.Graphics.DrawString("病区 " + this.ucBingQu.Controls[0].Text, ptzt8, Brushes.Black, new Point(30 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(55 + x, Y_unLine), new Point(180 + x, Y_unLine));
            e.Graphics.DrawString("床号 " + ucBedNo.Controls[0].Text, ptzt8, Brushes.Black, new Point(190 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(213 + x, Y_unLine), new Point(270 + x, Y_unLine));
            e.Graphics.DrawString("住院号 " + ucZhuyuanNo.Controls[0].Text, ptzt8, Brushes.Black, new Point(465 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(500 + x, Y_unLine), new Point(560 + x, Y_unLine));
            e.Graphics.DrawString("血型 " + cmbXueXing.Text, ptzt8, Brushes.Black, new Point(570 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(595 + x, Y_unLine), new Point(670 + x, Y_unLine));
            if (cmbXueXing.Text.Trim() == "")
                e.Graphics.DrawLine(ptp, new Point(620 + x, Y_unLine - 13), new Point(650 + x, Y_unLine - 2));

            //↑画标题一块的东西


            e.Graphics.DrawLine(pblack2, new Point(20 + x, 70 + y), new Point(700 + x, 70 + y));
            e.Graphics.DrawLine(pblack2, new Point(20 + x, 70 + y), new Point(20 + x, 1000 + y));
            e.Graphics.DrawLine(pblack2, new Point(700 + x, 70 + y), new Point(700 + x, 1000 + y));
            e.Graphics.DrawLine(pblack2, new Point(20 + x, 1000 + y), new Point(700 + x, 1000 + y));
            //↑画边框

            #region 打印基本信息
            YY = YY + 23; Y_unLine = YY + 13;
            e.Graphics.DrawString("姓名  " + ucName.Controls[0].Text, ptzt8, Brushes.Black, new Point(25 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(50 + x, Y_unLine), new Point(110 + x, Y_unLine));
            e.Graphics.DrawString("性别  " + ucSex.Controls[0].Text, ptzt8, Brushes.Black, new Point(120 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(145 + x, Y_unLine), new Point(180 + x, Y_unLine));
            e.Graphics.DrawString("年龄  " + ucAge.Controls[0].Text, ptzt8, Brushes.Black, new Point(190 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(215 + x, Y_unLine), new Point(250 + x, Y_unLine));
            e.Graphics.DrawString("岁", ptzt8, Brushes.Black, new Point(250, 74));

            e.Graphics.DrawString("身高  " + ucHeight.Controls[0].Text, ptzt8, Brushes.Black, new Point(270 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(295 + x, Y_unLine), new Point(350 + x, Y_unLine));
            e.Graphics.DrawString("cm 体重  " + ucWeight.Controls[0].Text, ptzt8, Brushes.Black, new Point(350 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(395 + x, Y_unLine), new Point(445 + x, Y_unLine));
            e.Graphics.DrawString("kg 体温  " + ucTemp.Controls[0].Text, ptzt8, Brushes.Black, new Point(445 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(490 + x, Y_unLine), new Point(540 + x, Y_unLine));
            e.Graphics.DrawString("℃   手术日期 " + ucOperDate.Controls[0].Text, ptzt8, Brushes.Black, new Point(540 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(605 + x, Y_unLine), new Point(690 + x, Y_unLine));

            YY = YY + 18; Y_unLine = YY + 13;
            e.Graphics.DrawString("血压  " + ucXueya.Controls[0].Text, ptzt8, Brushes.Black, new Point(25 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(50 + x, Y_unLine), new Point(140 + x, Y_unLine));
            e.Graphics.DrawString("mmHg 脉搏  " + ucMaibo.Controls[0].Text, ptzt8, Brushes.Black, new Point(140 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(190 + x, Y_unLine), new Point(280 + x, Y_unLine));
            e.Graphics.DrawString("次/分 呼吸  " + ucHuxi.Controls[0].Text, ptzt8, Brushes.Black, new Point(280 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(335 + x, Y_unLine), new Point(425 + x, Y_unLine));
            e.Graphics.DrawString("次/分       ASA  " + cmbASA.Text, ptzt8, Brushes.Black, new Point(425 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(510 + x, Y_unLine), new Point(600 + x, Y_unLine));
            e.Graphics.DrawString("□ E", ptzt9, Brushes.Black, new Point(630 + x, YY));
            if (cbE.Checked == true)
                e.Graphics.DrawString("✔", ptzt9, Brushes.Black, new Point(628 + x, YY));

            YY = YY + 18; Y_unLine = YY + 13;
            e.Graphics.DrawString("术前诊断 " + ucSqzd.Controls[0].Text, ptzt8, Brushes.Black, new Point(25 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(65 + x, Y_unLine), new Point(400 + x, Y_unLine));
            if (ucTSBQ.Controls[0].Text.Trim() == "")
                ucTSBQ.Controls[0].Text = " 无";
            e.Graphics.DrawString("特殊病情 " + ucTSBQ.Controls[0].Text, ptzt8, Brushes.Black, new Point(410 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(450 + x, Y_unLine), new Point(690 + x, Y_unLine));

            YY = YY + 18; Y_unLine = YY + 13;
            if (ucSqyy.Controls[0].Text.Trim() == "")
                ucSqyy.Controls[0].Text = " 无";
            e.Graphics.DrawString("术前用药 " + ucSqyy.Controls[0].Text, ptzt8, Brushes.Black, new Point(25 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(65 + x, Y_unLine), new Point(570 + x, Y_unLine));
            e.Graphics.DrawString("麻醉效果  " + this.cmbMZXG.Text, ptzt8, Brushes.Black, new Point(580 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(620 + x, Y_unLine), new Point(690 + x, Y_unLine));

            #endregion

            YY = YY + 18; Y_unLine = YY + 13;
            e.Graphics.DrawLine(ptp, new Point(20 + x, YY), new Point(700 + x, YY));
            //↑画边框

            //----------用药区域   
            DateTime dtnow = new DateTime();//打印截止时间判断        
            DateTime pagetime = new DateTime();
            DataTable dtMax = bll.GetMaxPoint(mzjldID);

            if (dtMax.Rows[0][0].ToString() == "")
                dtnow = DateTime.Now;
            else
                dtnow = Convert.ToDateTime(dtMax.Rows[0][0]);

            #region 打印页数
            pagetime = ptime; //当前打印页时间
            YY = YY + 3; Y_unLine = YY + 13;
            e.Graphics.DrawString("时 间", ptzt8, Brushes.Black, new Point(35 + x, YY + y));
            for (int i = 0; i < 10; i++) //打印检测时间
            {
                e.Graphics.DrawString(ptime.ToString("HH:mm"), ptzt8, Brushes.Black, new Point(85 + 60 * i + x, YY));
                ptime = ptime.AddMinutes(6 * jcsjjg);
            }
            if (ptime < dtnow)
            {
                e.HasMorePages = true;
                e.Graphics.DrawString("第 " + iYema.ToString() + " 页", ptzt7, Brushes.Black, new Point(340 + x, 1005 + y));
                iYema++;
            }
            else
            {
                e.HasMorePages = false; ptime = fristopen;
                e.Graphics.DrawString("第 " + iYema.ToString() + " 页", ptzt7, Brushes.Black, new Point(340 + x, 1005 + y));
            }
            #endregion

            #region 画框体
            YY = YY + 18;
            e.Graphics.DrawLine(ptp, new Point(20 + x, YY), new Point(700 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(20 + x, YY + 12 * 3), new Point(35 + x, YY + 12 * 3));
            e.Graphics.DrawLine(ptp, new Point(20 + x, YY + 12 * 14), new Point(35 + x, YY + 12 * 14));
            e.Graphics.DrawLine(ptp, new Point(20 + x, YY + 12 * 17), new Point(35 + x, YY + 12 * 17));

            for (int i = 0; i < 18; i++)//画横实线
            {
                e.Graphics.DrawLine(ptp, new Point(33 + x, YY + 12 * i + y), new Point(700 + x, YY + 12 * i + y));
            }

            e.Graphics.DrawLine(ptp, new Point(33 + x, YY + y), new Point(33 + x, YY + 17 * 12 + y));

            for (int i = 0; i < 10; i++)//画竖实线
            {
                e.Graphics.DrawLine(ptp, new Point(100 + x + 60 * i, YY - 2 + y), new Point(100 + x + 60 * i, YY + 17 * 12 + y));
            }
            for (int i = 1; i < 30; i++)//画竖虚线
            {
                e.Graphics.DrawLine(pxuxian, new Point(100 + x + 20 * i, YY + y), new Point(100 + x + 20 * i, YY + 17 * 12 + y));

            }
            e.Graphics.DrawString("气\n吸", ptzt7, Brushes.Black, new Point(21 + x, YY + 5));
            e.Graphics.DrawString("用\n药\n情\n况", ptzt7, Brushes.Black, new Point(21 + x, YY + 12 * 3 + 25));
            e.Graphics.DrawString("局\n麻\n药", ptzt7, Brushes.Black, new Point(21 + x, YY + 12 * 14 + 3));
            #endregion

            #region 打印气体
            ArrayList sssQT = new ArrayList();
            int qti = 0;   //打印气体
            foreach (adims_MODEL.mzqt mzqt in mzqtList)
            {
                if (sssQT.Contains(mzqt.Qtname))
                {
                    qti = sssQT.IndexOf(mzqt.Qtname);
                }
                else
                {
                    qti = sssQT.Count;
                    e.Graphics.DrawString(mzqt.Qtname, mzqt.Qtname.Length < 7 ? ptzt6 : ptzt5, Brushes.Black, new PointF(32 + x, YY + qti * 12 + y + 3));
                    sssQT.Add(mzqt.Qtname);

                }

                if (mzqt.Bz == 2)
                {
                    TimeSpan t = new TimeSpan();
                    TimeSpan t1 = new TimeSpan();
                    t1 = mzqt.Jssj - pagetime;
                    t = mzqt.Sysj - pagetime;
                    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100);
                    int y1 = YY + qti * 12 + 4;
                    int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 10 / jcsjjg + 100);
                    //double qtzongliang = (mzqt.Yl) * (x2 - x1) / 2.5);
                    //e.Graphics.DrawString(qtzongliang).ToString() + "L", this.Font, Brushes.Blue, new Point(763, y1 - 2));
                    if (x1 > 100 + x && x1 < 700 + x)
                    {
                        e.Graphics.DrawString(mzqt.Yl.ToString() + " " + mzqt.Dw, ht6, Brushes.Blue, new Point(x1 + x + (x2 - x1) / 2, y + y1 - 8)); //new Point(x1 -3 + x, y + y1 - 6));
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), new Point(x1 - 3 + x, y1 + 6 + y), new Point(x1 + 3 + x, y1 + 6 + y) });
                    }
                    if (x2 > 100 + x && x2 < 700 + x)
                    {
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2 + x, y1 + y), new Point(x2 - 3 + x, y1 + 6 + y), new Point(x2 + 3 + x, y1 + 6 + y) });
                    }
                    if (x1 > 100 + x && x1 < 700 + x && x2 > 100 + x && x2 < 700 + x)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x1 + x, y1 + y + 3), new Point(x2 + x, y1 + y + 3));
                    }
                    if (x1 > 100 + x && x1 < 700 + x && x2 > 700 + x)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x1 + x, y1 + y + 3), new Point(700 + x, y1 + y + 3));
                    }
                    if (x1 < 100 + x && x2 > 100 + x && x2 < 700 + x)
                    {
                        e.Graphics.DrawLine(pred2, new Point(100 + x, y1 + y + 3), new Point(x2 + x, y1 + y + 3));
                    }
                    if (x1 < 100 + x && x2 > 700 + x)
                    {
                        e.Graphics.DrawLine(pred2, new Point(100 + x, y1 + y + 3), new Point(700 + x, y1 + y + 3));
                    }
                }
                //qti++;
                //sssQT.Add(mzqt.Qtname);
            }

            #endregion

            #region 打印诱导药
            //诱导药不重名字符集合
            ArrayList ydyListStr = new ArrayList();
            int BeginLine = 3;
            int ydyi = BeginLine;
            foreach (adims_MODEL.mzyt mzyt in ydyList) //打印诱导药
            {
                if (ydyListStr.Contains(mzyt.Ytname))
                {
                    ydyi = ydyListStr.IndexOf(mzyt.Ytname) + 3;

                }
                else
                {
                    ydyi = ydyListStr.Count + 3;
                    e.Graphics.DrawString(mzyt.Ytname + " " + mzyt.Dw + " (" + mzyt.Yyfs + ")", mzyt.Ytname.Length < 7 ? ptzt6 : ptzt5, Brushes.Black, new PointF(32 + x, YY + ydyi * 12 + y + 3));
                    ydyListStr.Add(mzyt.Ytname);
                }

                if (mzyt.Cxyy == false)
                {
                    TimeSpan t = new TimeSpan();
                    t = mzyt.Sysj - pagetime;
                    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100);
                    int y1 = YY + ydyi * 12 + 4;
                    if (x1 > 100 + x && x1 < 700 + x)
                    {
                        e.Graphics.DrawString(mzyt.Yl.ToString(), ht6, Brushes.Blue, new Point(x1 - 3 + x, y + y1 - 8));
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), new Point(x1 - 3 + x, y1 + 6 + y), new Point(x1 + 3 + x, y1 + 6 + y) });
                    }
                }
                if (mzyt.Cxyy == true)
                {
                    TimeSpan t = new TimeSpan();
                    TimeSpan t1 = new TimeSpan();
                    t1 = mzyt.Jssj - pagetime;
                    t = mzyt.Sysj - pagetime;
                    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100);
                    int y1 = YY + ydyi * 12 + 4;
                    int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 10 / jcsjjg + 100);
                    if (x1 > 100 + x && x1 < 700 + x)
                    {
                        e.Graphics.DrawString(mzyt.Yl.ToString(), ht6, Brushes.Blue, new Point(x1 + x + (x2 - x1) / 2, y + y1 - 8));
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), new Point(x1 - 3 + x, y1 + 6 + y), new Point(x1 + 3 + x, y1 + 6 + y) });
                    }
                    if (x2 > 100 + x && x2 < 700 + x)
                    {
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2 + x, y1 + y), new Point(x2 - 3 + x, y1 + 6 + y), new Point(x2 + 3 + x, y1 + 6 + y) });
                    }
                    if (x1 > 100 + x && x1 < 700 + x && x2 > 100 + x && x2 < 700 + x)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x1 + x, y1 + y + 3), new Point(x2 + x, y1 + y + 3));
                    }
                    if (x1 > 100 + x && x1 < 700 + x && x2 > 700 + x)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x1 + x, y1 + y + 3), new Point(700 + x, y1 + y + 3));
                    }
                    if (x1 < 100 + x && x2 > 100 + x && x2 < 700 + x)
                    {
                        e.Graphics.DrawLine(pred2, new Point(100 + x, y1 + y + 3), new Point(x2 + x, y1 + y + 3));
                    }
                    if (x1 < 100 + x && x2 > 700 + x)
                    {
                        e.Graphics.DrawLine(pred2, new Point(100 + x, y1 + y + 3), new Point(700 + x, y1 + y + 3));
                    }
                }


            }
            #endregion

            #region 打印局麻药
            int jti = 14;  //打印局麻药
            ArrayList sssJMY = new ArrayList();
            foreach (adims_MODEL.jtytsx jt in jmyList)
            {
                if (sssJMY.Contains(jt.Name))
                {
                    jti = 14 + sssJMY.IndexOf(jt.Name);
                }

                else
                {
                    jti = 14 + sssJMY.Count;
                    string zrfs = jt.Zrfs;
                    if (zrfs == "硬膜外")
                    {
                        zrfs = "EA";
                    }
                    if (zrfs == "静注")
                    {
                        zrfs = "iv";
                    }
                    e.Graphics.DrawString(jt.Name + jt.Dw + " " + zrfs, jt.Name.Length < 7 ? ptzt6 : ptzt5, Brushes.Black, new PointF(32 + x, YY + jti * 12 + y + 3));
                    sssJMY.Add(jt.Name);
                }
                TimeSpan t = new TimeSpan();
                t = jt.Kssj - pagetime;
                int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100);
                int y1 = YY + jti * 12 + 4;
                if (x1 > 100 + x && x1 < 700 + x)
                {
                    e.Graphics.DrawString(jt.Jl.ToString(), ht6, Brushes.Blue, new Point(x1 - 3 + x, y + y1 - 8));
                    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), new Point(x1 - 3 + x, y1 + 6 + y), new Point(x1 + 3 + x, y1 + 6 + y) });
                }
                //jti++;
                //sssJMY.Add(jt.Name);
            }
            #endregion

            #region//打印检测区域格子。血压体温等区域↓
            YY = YY + 12 * 17;
            e.Graphics.DrawLine(ptp, new Point(100 + x, YY), new Point(100 + x, YY + 12 * 18));
            for (int i = 0; i < 12; i++)//画横实线
            {
                e.Graphics.DrawLine(ptp, new Point(100 + x, YY + 18 * i), new Point(700 + x, YY + 18 * i + y));
            }
            for (int i = 0; i < 10; i++)//画竖实线
            {
                e.Graphics.DrawLine(ptp, new Point(100 + 60 * i + x, YY + y), new Point(100 + 60 * i + x, YY + 12 * 18 + y));
            }
            for (int i = 1; i < 30; i++)//画竖虚线
            {
                e.Graphics.DrawLine(pxuxian, new Point(100 + 20 * i + x, YY + y), new Point(100 + 20 * i + x, YY + 12 * 18 + y));
            }
            for (int i = 1; i < 12; i++)
                e.Graphics.DrawString((240 - i * 20).ToString(), ptzt7, Brushes.Black, new PointF(80 + x, YY + (float)18 * i + y - 5));
            for (int i = 1; i < 12; i++)
                e.Graphics.DrawString((48 - i * 4).ToString(), ptzt7, Brushes.Black, new PointF(65 + x, YY + (float)18 * i + y - 5));

            e.Graphics.DrawString("Ⅹ 麻醉", ptzt7, Brushes.Black, new Point(21 + x, YY + 20 + y));
            e.Graphics.DrawString("⊙ 手术", ptzt7, Brushes.Black, new Point(21 + x, YY + 40 + y));
            e.Graphics.DrawString("Θ 置管", ptzt7, Brushes.Black, new Point(21 + x, YY + 60 + y));
            e.Graphics.DrawString("Φ 拔管", ptzt7, Brushes.Black, new Point(21 + x, YY + 80 + y));
            e.Graphics.DrawString(label84.Text, ptzt7, Brushes.Red, new Point(21 + x, YY + 100 + y));
            e.Graphics.DrawString("收缩压", ptzt7, Brushes.Red, new Point(32 + x, YY + 100 + y));
            e.Graphics.DrawString(label85.Text, ptzt7, Brushes.Red, new Point(21 + x, YY + 120 + y));
            e.Graphics.DrawString("舒张压", ptzt7, Brushes.Red, new Point(32 + x, YY + 120 + y));
            e.Graphics.DrawString(label86.Text, ptzt6, Brushes.Green, new Point(21 + x, YY + 140 + y));
            e.Graphics.DrawString("体温", ptzt7, Brushes.Green, new Point(37 + x, YY + 140 + y));
            e.Graphics.DrawString(label87.Text, ptzt7, Brushes.Blue, new Point(21 + x, YY + 160 + y));
            e.Graphics.DrawString("脉搏", ptzt7, Brushes.Blue, new Point(37 + x, YY + 160 + y));
            e.Graphics.DrawString(label88.Text, ptzt7, Brushes.DarkCyan, new Point(21 + x, YY + 180 + y));
            e.Graphics.DrawString("自主呼吸", ptzt6, Brushes.DarkCyan, new Point(32 + x, YY + 180 + y));
            e.Graphics.DrawString("CR 控制呼吸", ptzt6, Brushes.DarkCyan, new Point(21 + x, YY + 200 + y));

            #endregion

            #region 打印监测点曲线
            float px = 0, py = 0;
            Pen sys_pen = Pens.Red;                     //打印收缩压
            foreach (adims_MODEL.point p in ssyPoint)
            {
                if (p.D >= pagetime && p.D <= pagetime.AddMinutes(60 * jcsjjg))
                {
                    TimeSpan t = new TimeSpan();
                    t = p.D - pagetime;
                    float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100);
                    float pointy = 0;
                    if (p.V > 220)
                    {
                        pointy = (float)((18) * 0.9 + YY);
                        e.Graphics.DrawString(p.V.ToString(), ptzt7, Brushes.Blue, pointx + x, pointy + y);
                    }
                    else
                        pointy = (float)((240 - p.V) * 0.9 + YY);
                    e.Graphics.DrawPolygon(sys_pen, new PointF[3] { new PointF(pointx + x, pointy + y), new PointF(pointx - 3 + x, pointy - 3 + y), new PointF(pointx + 3 + x, pointy - 3 + y) });
                    if (px != 0)
                        e.Graphics.DrawLine(sys_pen, new PointF(px + x, py + y), new PointF(pointx + x, pointy + y));

                    px = pointx;
                    py = pointy;
                }

            }
            px = 0;                                      //打印舒张压
            py = 0;
            Pen dia_pen = Pens.Red;
            foreach (adims_MODEL.point p in szyPoint)
            {
                if (p.D >= pagetime && p.D <= pagetime.AddMinutes(60 * jcsjjg))
                {
                    TimeSpan t = new TimeSpan();
                    t = p.D - pagetime;
                    float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100);
                    //float pointy = (float)((220 - p.V) * 1 + 460);
                    float pointy = 0;
                    if (p.V > 220)
                    {
                        pointy = (float)(float)((18) * 0.9 + YY);
                        e.Graphics.DrawString(p.V.ToString(), ptzt7, Brushes.Blue, pointx + x, pointy + y);

                    }
                    else
                        pointy = (float)((240 - p.V) * 0.9 + YY);
                    e.Graphics.DrawPolygon(dia_pen, new PointF[3] { new PointF(pointx + x, pointy + y), new PointF(pointx - 3 + x, pointy + 3 + y), new PointF(pointx + 3 + x, pointy + 3 + y) });
                    if (px != 0)
                        e.Graphics.DrawLine(dia_pen, new PointF(px + x, py + y), new PointF(pointx + x, pointy + y));

                    px = pointx;
                    py = pointy;
                }

            }
            px = 0;
            py = 0;
            Pen hr_pen = Pens.Green;
            Brush hr_brush = Brushes.Green;
            foreach (adims_MODEL.pointF p in twPoint)//打印体温
            {
                if (p.D >= pagetime && p.D <= pagetime.AddMinutes(60 * jcsjjg))
                {
                    TimeSpan t = new TimeSpan();
                    t = p.D - pagetime;
                    float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100);
                    //float pointy = (float)((220 - p.V) * 1 + 460);
                    float pointy = 0;
                    if (p.V > 44)
                    {
                        pointy = (float)((18) * 0.9 + YY);
                        e.Graphics.DrawString(p.V.ToString(), ht6, Brushes.Blue, pointx + x, pointy + y);
                    }
                    else
                        pointy = (float)((44 - p.V) * 5.4 + YY);
                    e.Graphics.FillPolygon(hr_brush, new PointF[4]
                      { new PointF(pointx - 2 + x, pointy - 2 + y),
                        new PointF(pointx + 2 + x, pointy - 2 + y),
                        new PointF(pointx + 2 + x, pointy + 2 + y),
                        new PointF(pointx - 2 + x, pointy + 2 + y) });
                    if (px != 0)
                        e.Graphics.DrawLine(hr_pen, new PointF(px + x, py + y),
                                                    new PointF(pointx + x, pointy + y));

                    px = pointx;
                    py = pointy;
                }
            }

            px = 0;
            py = 0;
            Pen temp_pen = Pens.Blue;
            Brush temp_brush = Brushes.Blue;
            foreach (adims_MODEL.point p in mboPoint)//打印脉搏
            {
                if (p.D >= pagetime && p.D <= pagetime.AddMinutes(60 * jcsjjg))
                {
                    TimeSpan t = new TimeSpan();
                    t = p.D - pagetime;
                    float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100);
                    //float pointy = (float)((220 - p.V) * 1 + 460);
                    float pointy = 0;
                    if (p.V > 220)
                    {
                        pointy = (float)((18) * 0.9 + YY);
                        e.Graphics.DrawString(p.V.ToString(), ptzt7, Brushes.Blue, pointx + x, pointy + y);

                    }
                    else
                        pointy = (float)((240 - p.V) * 0.9 + YY);
                    e.Graphics.FillEllipse(temp_brush, pointx + x - 2, pointy + y - 2, 5, 5);
                    if (px != 0)
                        e.Graphics.DrawLine(temp_pen, new PointF(px + x, py + y), new PointF(pointx + x, pointy + y));

                    px = pointx;
                    py = pointy;
                }
            }

            px = 0;                                      //打印呼吸率
            py = 0;
            Pen hxl_pen = Pens.DarkCyan;
            int phuxi = 0;
            foreach (adims_MODEL.point p in hxlPoint)
            {
                if (p.D >= pagetime && p.D <= pagetime.AddMinutes(60 * jcsjjg))
                {
                    TimeSpan t = new TimeSpan();
                    t = p.D - pagetime;
                    float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100);
                    float pointy = 0;
                    if (p.V > 220)
                    {
                        pointy = (float)((18) * 0.9 + YY);
                        e.Graphics.DrawString(p.V.ToString(), ptzt7, Brushes.Blue, pointx + x, pointy + y);
                    }
                    else
                        pointy = (float)((240 - p.V) * 0.9 + YY);
                    if (jkksTime < p.D && p.D < jkjsTime)
                    {
                        if (phuxi % 2 == 0)
                        {
                            e.Graphics.DrawString("CR", ptzt6, Brushes.DarkCyan, pointx + x - 4, pointy + y - 3);
                            if (px != 0)
                                e.Graphics.DrawLine(hxl_pen, new PointF(px + x, py + y), new PointF(pointx + x, pointy + y));
                            px = pointx;
                            py = pointy;
                        }
                        phuxi++;
                    }
                    else
                    {
                        e.Graphics.DrawEllipse(hxl_pen, pointx + x - 2, pointy + y - 2, 5, 5);
                        if (px != 0)
                            e.Graphics.DrawLine(hxl_pen, new PointF(px + x, py + y), new PointF(pointx + x, pointy + y));
                        px = pointx;
                        py = pointy;
                    }
                }

            }

            #endregion

            #region//尿量下区域
            YY = YY + 12 * 18;
            for (int i = 0; i < 13; i++)
            {
                if (i == 8 || i == 10)
                    e.Graphics.DrawLine(ptp, new Point(100 + x, YY + 12 * i + y), new Point(700 + x, YY + 12 * i + y));
                else
                    e.Graphics.DrawLine(ptp, new Point(20 + x, YY + 12 * i + y), new Point(700 + x, YY + 12 * i + y));

            }

            for (int i = 0; i < 10; i++)//画竖实线
            {
                e.Graphics.DrawLine(ptp, new Point(100 + 60 * i + x, YY + y), new Point(100 + 60 * i + x, YY + 12 * 12 + y));
            }
            for (int i = 1; i < 30; i++)//画竖虚线
            {
                e.Graphics.DrawLine(pxuxian, new Point(100 + 20 * i + x, YY + y), new Point(100 + 20 * i + x, YY + 12 * 12 + y));
            }
            e.Graphics.DrawString("尿量(ml)", ptzt7, Brushes.Black, new Point(25 + x, YY + y));
            e.Graphics.DrawString("输液(ml)", ptzt7, Brushes.Black, new Point(25 + x, YY + 12 * 7 + 8 + y));
            e.Graphics.DrawString("输血(ml)", ptzt7, Brushes.Black, new Point(25 + x, YY + 12 * 9 + 8 + y));
            e.Graphics.DrawString("麻醉平面", ptzt7, Brushes.Black, new Point(25 + x, YY + 12 * 11 + y));
            e.Graphics.DrawString("治疗序号", ptzt7, Brushes.Black, new Point(25 + x, YY + 12 * 12 + y + 8));
            #endregion

            #region 打印尿量
            foreach (adims_MODEL.clcxqt c in clcxqt)
            {
                if (c.D >= pagetime && c.D <= pagetime.AddMinutes(60 * jcsjjg))
                {
                    TimeSpan t = new TimeSpan();
                    t = c.D - pagetime;
                    float cx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100);
                    if (c.Lx == 1)
                    {
                        e.Graphics.FillRectangle(Brushes.Pink, cx - 5 + x, YY + y + 2, 16, 8);
                        e.Graphics.DrawString(c.V.ToString(), ptzt7, Brushes.Black, new PointF(cx - 5 + x, YY + y + 2));
                    }
                }
            }
            #endregion

            #region //打印检测项目

            int jhi = 1;
            foreach (string jc in jhxmy)
            {
                e.Graphics.DrawString(jc, ptzt7, Brushes.Black, new PointF(35 + x, YY + jhi * 12 + y));
                int count1 = 0;
                foreach (adims_MODEL.jhxm j in jhxmValueList)
                {
                    if (jc == j.Sy && j.V != 0)
                    {
                        if (j.D >= pagetime && j.D <= pagetime.AddMinutes(60 * jcsjjg))
                        {
                            TimeSpan t = new TimeSpan();
                            t = j.D - pagetime;
                            float jhx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100);
                            float jhy = YY + jhi * 12 + y + 2;
                            if (count1 % 2 == 0)
                            {
                                if (CGUAN && !BGUAN && j.Sy == "ETCO2")
                                {
                                    if (cgTime < j.D)
                                    {
                                        e.Graphics.FillRectangle(Brushes.Pink, jhx - 3 + x, jhy, 9, 8);
                                        e.Graphics.DrawString(j.V.ToString(), j.V > 99 ? ptzt5 : ptzt7, Brushes.Black, new PointF(jhx - 4 + x, jhy));
                                    }
                                }
                                if (CGUAN && BGUAN && j.Sy == "ETCO2")
                                {
                                    if (cgTime < j.D && j.D < bgTime)
                                    {
                                        e.Graphics.FillRectangle(Brushes.Pink, jhx - 3 + x, jhy, 9, 8);
                                        e.Graphics.DrawString(j.V.ToString(), j.V > 99 ? ptzt5 : ptzt7, Brushes.Black, new PointF(jhx - 4 + x, jhy));
                                    }
                                }
                                else if (j.Sy != "ETCO2")
                                {
                                    e.Graphics.FillRectangle(Brushes.Pink, jhx - 3 + x, jhy, 9, 9);
                                    e.Graphics.DrawString(j.V.ToString(), j.V > 99 ? ptzt5 : ptzt7, Brushes.Black, new PointF(jhx - 4 + x, jhy));
                                }
                            }
                            count1++;
                        }
                    }
                }
                jhi++;

            }
            #endregion

            #region 打印输液
            int sxi = 7;
            ArrayList sss_sx = new ArrayList();
            foreach (adims_MODEL.shuye sx in shuyeList)
            {

                TimeSpan t = new TimeSpan();
                TimeSpan t1 = new TimeSpan();
                t1 = pagetime.AddMinutes(60 * jcsjjg) - pagetime;
                t = sx.Kssj - pagetime;
                int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100);
                int y1 = 0;
                if (sxi % 2 == 1)
                    y1 = YY + 7 * 12 + 5;
                else
                    y1 = YY + 8 * 12 + 5;
                int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 10 / jcsjjg + 100);
                if (x1 > 100 + x && x1 < 700 + x)
                {
                    e.Graphics.DrawString(sx.Name.ToString() + " " + sx.Jl.ToString(), ptzt7, Brushes.Blue, x1 + x, y + y1 - 8);
                    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), new Point(x1 - 3 + x, y1 + 5 + y), new Point(x1 + 3 + x, y1 + 5 + y) });
                }
                sxi++;
            }

            #endregion

            #region 打印输血
            //打印输血
            sxi = 9;
            foreach (adims_MODEL.shuxue sx in shuxueList)
            {
                TimeSpan t = new TimeSpan();
                TimeSpan t1 = new TimeSpan();
                t1 = pagetime.AddHours(jcsjjg) - pagetime;
                t = sx.Kssj - pagetime;
                int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100);
                int y1 = 0;
                if (sxi % 2 == 1)
                    y1 = YY + 9 * 12 + 5;
                else
                    y1 = YY + 10 * 12 + 5;
                int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 10 / jcsjjg + 100);
                if (x1 > 100 + x && x1 < 700 + x)
                {
                    e.Graphics.DrawString(sx.Name.ToString() + " " + sx.Jl.ToString(), ptzt7, Brushes.Blue, x1 + x, y + y1 - 6);
                    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), new Point(x1 - 3 + x, y1 + 5 + y), new Point(x1 + 3 + x, y1 + 5 + y) });
                }

                sxi++;
            }
            #endregion

            #region 打印麻醉平面
            int mzpmi = 1;
            foreach (adims_MODEL.mzpingmian sz in mzpmList)
            {
                if (sz.D >= pagetime && sz.D <= pagetime.AddHours(jcsjjg))
                {
                    TimeSpan t = new TimeSpan();
                    t = sz.D - pagetime;
                    float tx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100 + x);
                    float ty = YY + 11 * 12 + y + 2;
                    e.Graphics.FillRectangle(Brushes.Pink, tx - 6, ty, 33, 8);
                    e.Graphics.DrawString(sz.mzpmName.ToString(), ptzt7, Brushes.Black, new PointF(tx - 7, ty - 2));
                }
                mzpmi++;
            }

            #endregion

            #region 打印术中事件
            int szi = 1;
            int szi1 = 1;
            string Str = "";
            foreach (adims_MODEL.szsj sz in szsj)
            {
                Str = SwitchNumToEnglish(szi);//转换字母
                if (sz.D >= pagetime && sz.D <= pagetime.AddHours(jcsjjg))
                {
                    TimeSpan t = new TimeSpan();
                    t = sz.D - pagetime;
                    float tx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100 + x);
                    float xPrint = tx - 3;
                    float yPrint = YY + 12 * 12 + y + 6;
                    e.Graphics.FillRectangle(Brushes.Pink, xPrint, yPrint, 8, 8);
                    e.Graphics.DrawString(Str, ptzt7, Brushes.Black, new PointF(xPrint, yPrint));

                    e.Graphics.DrawString(Str + "." + sz.Name, ptzt7, Brushes.Black, new PointF(25 + x, YY + 14 * 12 + y + 1 + szi1 * 10));
                    szi1++;

                }
                szi++;

            }



            #endregion

            #region 打印特殊用药
            int tsi = 1;
            int tsi1 = 1;
            foreach (adims_MODEL.tsyy ts in tsyy)
            {
                if (ts.D >= pagetime && ts.D <= pagetime.AddHours(jcsjjg))
                {
                    TimeSpan t = new TimeSpan();
                    t = ts.D - pagetime;
                    float tx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100) + x;
                    float xPrint = tx - 3;
                    float yPrint = YY + 12 * 12 + y + 12;
                    e.Graphics.FillEllipse(Brushes.LightGreen, xPrint, yPrint, 8, 8);
                    e.Graphics.DrawString(tsi.ToString(), ptzt7, Brushes.Black, new PointF(xPrint, yPrint));
                    e.Graphics.DrawString(tsi.ToString() + "." + ts.Name + " " + ts.Yl.ToString() + ts.Dw + ts.Yyfs, ptzt6, Brushes.Black, new PointF(300 + x, YY + 14 * 12 + y + 3 + tsi1 * 10));
                    tsi1++;
                }

                tsi++;
            }
            #endregion

            #region 打印麻醉，手术，插管
            int yPrint1 = YY + 12 * 12 + y + 0;
            if (ssksTime >= pagetime && ssksTime <= pagetime.AddHours(jcsjjg))
            {
                TimeSpan ts1 = ssksTime - pagetime;
                int xx1 = (int)((ts1.Days * 24 * 60 + ts1.Hours * 60 + ts1.Minutes) * 10 / jcsjjg + 95 + x);
                e.Graphics.DrawString("⊙", ptzt7, Brushes.Black, xx1, yPrint1);
            }
            if (ssjsTime >= pagetime && ssjsTime <= pagetime.AddHours(jcsjjg))
            {
                TimeSpan ts2 = ssjsTime - pagetime;
                int xx2 = (int)((ts2.Days * 24 * 60 + ts2.Hours * 60 + ts2.Minutes) * 10 / jcsjjg + 95 + x);
                e.Graphics.DrawString("⊙", ptzt7, Brushes.Black, xx2, yPrint1);
            }
            if (mzksTime >= pagetime && mzksTime <= pagetime.AddHours(jcsjjg))
            {
                TimeSpan ts3 = mzksTime - pagetime;
                int xx3 = (int)((ts3.Days * 24 * 60 + ts3.Hours * 60 + ts3.Minutes) * 10 / jcsjjg + 95 + x);
                e.Graphics.DrawString("Χ", ptzt7, Brushes.Black, xx3, yPrint1);
            }
            if (mzjsTime >= pagetime && mzjsTime <= pagetime.AddHours(jcsjjg))
            {
                TimeSpan ts4 = mzjsTime - pagetime;
                int xx4 = (int)((ts4.Days * 24 * 60 + ts4.Hours * 60 + ts4.Minutes) * 10 / jcsjjg + 95 + x);
                e.Graphics.DrawString("Χ", ptzt7, Brushes.Black, xx4, yPrint1);
            }
            if (cgTime >= pagetime && cgTime <= pagetime.AddHours(jcsjjg))
            {
                TimeSpan ts5 = cgTime - pagetime;
                int xx5 = (int)((ts5.Days * 24 * 60 + ts5.Hours * 60 + ts5.Minutes) * 10 / jcsjjg + 95 + x);
                e.Graphics.DrawString("Θ", ptzt7, Brushes.Black, xx5, yPrint1);
            }
            if (bgTime >= pagetime && bgTime <= pagetime.AddHours(jcsjjg))
            {
                TimeSpan ts6 = bgTime - pagetime;
                int xx6 = (int)((ts6.Days * 24 * 60 + ts6.Hours * 60 + ts6.Minutes) * 10 / jcsjjg + 95 + x);
                e.Graphics.DrawString("Φ", ptzt7, Brushes.Black, xx6, yPrint1);
            }
            #endregion



            #endregion
            YY = YY + 12 * 12;
            e.Graphics.DrawLine(ptp, new Point(20 + x, YY + y), new Point(700 + x, YY + y));
            YY = YY + 22;
            e.Graphics.DrawLine(ptp, new Point(20 + x, YY + y), new Point(700 + x, YY + y));
            e.Graphics.DrawString("术中事件和特殊用药", ptzt7, Brushes.Black, new Point(150 + x, YY + y));
            //e.Graphics.DrawString("特殊用药", ptzt7, Brushes.Black, new Point(350 + x, YY + y));
            e.Graphics.DrawString("术后镇痛信息", ptzt7, Brushes.Black, new Point(580 + x, YY + y));
            YY = YY + 12;
            e.Graphics.DrawLine(ptp, new Point(20 + x, YY + y), new Point(700 + x, YY + y));

            for (int i = 0; i < listBox5.Items.Count; i++)//打印镇痛药
            {
                e.Graphics.DrawString(listBox5.Items[i].ToString(),
                    ptzt7, Brushes.Black, new Point(580 + x, YY + 3 + i * 12 + y));
            }


            int X6 = x; int Y6 = YY + 120; Y_unLine = Y6 + 13;
            e.Graphics.DrawLine(ptp, new Point(20 + x, Y6 - 3), new Point(700 + x, Y6 - 3));
            e.Graphics.DrawLine(ptp, new Point(580 + x, Y6 - 3), new Point(580 + x, 1000 + y));
            e.Graphics.DrawString("手术体位 " + cmbTiwei.Text, ptzt8, Brushes.Black, 22 + X6, Y6);
            e.Graphics.DrawLine(ptp, new Point(68 + X6, Y_unLine), new Point(140 + X6, Y_unLine));
            if (cmbTiwei.Text.Trim() == "")
                e.Graphics.DrawLine(ptp, new Point(100 + X6, Y_unLine - 11), new Point(130 + X6, Y_unLine - 2));

            e.Graphics.DrawString("全身麻醉方法 " + cmbMZFF.Text, ptzt8, Brushes.Black, 150 + X6, Y6);
            e.Graphics.DrawLine(ptp, new Point(218 + X6, Y_unLine), new Point(300 + X6, Y_unLine));
            if (cmbMZFF.Text.Trim() == "")
                e.Graphics.DrawLine(ptp, new Point(250 + X6, Y_unLine - 11), new Point(280 + X6, Y_unLine - 2));

            e.Graphics.DrawString("插管 " + cmbChaguan.Text, ptzt8, Brushes.Black, 310 + X6, Y6);
            e.Graphics.DrawLine(ptp, new Point(335 + X6, Y_unLine), new Point(575 + X6, Y_unLine));
            if (cmbChaguan.Text.Trim() == "")
                e.Graphics.DrawLine(ptp, new Point(360 + X6, Y_unLine - 11), new Point(390 + X6, Y_unLine - 2));


            Y6 = 16 + Y6; Y_unLine = Y6 + 12;
            e.Graphics.DrawString("神经阻滞方法 " + cmbZZFF.Text, ptzt8, Brushes.Black, 22 + X6, Y6);
            e.Graphics.DrawLine(ptp, new Point(90 + X6, Y_unLine), new Point(180 + X6, Y_unLine));
            if (cmbZZFF.Text.Trim() == "")
                e.Graphics.DrawLine(ptp, new Point(100 + X6, Y_unLine - 11), new Point(130 + X6, Y_unLine - 2));

            e.Graphics.DrawString("穿刺点 " + cmbCCdianUp1.Text + cmbCCdianUp2.Text, ptzt8, Brushes.Black, 190 + X6, Y6);
            e.Graphics.DrawLine(ptp, new Point(230 + X6, Y_unLine), new Point(300 + X6, Y_unLine));
            if (cmbCCdianUp1.Text.Trim() == "" && cmbCCdianUp2.Text.Trim() == "")
                e.Graphics.DrawLine(ptp, new Point(250 + X6, Y_unLine - 11), new Point(280 + X6, Y_unLine - 2));

            e.Graphics.DrawString("置管↑ " + ucZhiGuanUp.Controls[0].Text, ptzt8, Brushes.Black, 300 + X6, Y6);
            e.Graphics.DrawLine(ptp, new Point(340 + X6, Y_unLine), new Point(400 + X6, Y_unLine));
            e.Graphics.DrawString("cm", ptzt8, Brushes.Black, 400 + X6, Y6);
            if (ucZhiGuanUp.Controls[0].Text.Trim() == "")
                e.Graphics.DrawLine(ptp, new Point(360 + X6, Y_unLine - 11), new Point(390 + X6, Y_unLine - 2));


            string chuxue = "0";
            if (!string.IsNullOrEmpty(ucChuxue.Controls[0].Text))
                chuxue = ucChuxue.Controls[0].Text;
            e.Graphics.DrawString("失血：" + chuxue + " ml", ptzt8, Brushes.Black, new Point(600 + X6, Y6));
            e.Graphics.DrawLine(ptp, new Point(625 + X6, Y_unLine), new Point(660 + X6, Y_unLine));

            string nialiang = "0";
            if (!string.IsNullOrEmpty(ucNiaoLiang.Controls[0].Text))
                nialiang = ucNiaoLiang.Controls[0].Text;

            e.Graphics.DrawString("尿量：" + nialiang + " ml", ptzt8, Brushes.Black, new Point(600 + X6, Y6 + 20));
            e.Graphics.DrawLine(ptp, new Point(625 + X6, Y_unLine + 20), new Point(660 + X6, Y_unLine + 20));

            string zongliang = "0";
            if (!string.IsNullOrEmpty(ucZongLiang.Controls[0].Text))
                zongliang = ucZongLiang.Controls[0].Text;

            e.Graphics.DrawString("入量合计：" + zongliang + " ml", ptzt8, Brushes.Black, new Point(590 + X6, Y6 + 40));

            e.Graphics.DrawLine(ptp, new Point(625 + X6, Y_unLine + 40), new Point(660 + X6, Y_unLine + 40));



            Y6 = 16 + Y6; Y_unLine = Y6 + 12;
            e.Graphics.DrawString("穿刺点 " + cmbCCdianDown1.Text + cmbCCdianDown2.Text, ptzt8, Brushes.Black, 190 + X6, Y6);
            e.Graphics.DrawLine(ptp, new Point(230 + X6, Y_unLine), new Point(300 + X6, Y_unLine));
            if (cmbCCdianDown1.Text.Trim() == "" && cmbCCdianDown2.Text.Trim() == "")
                e.Graphics.DrawLine(ptp, new Point(250 + X6, Y_unLine - 11), new Point(280 + X6, Y_unLine - 2));

            e.Graphics.DrawString("置管↓ " + ucZhiGuanDown.Controls[0].Text, ptzt8, Brushes.Black, 300 + X6, Y6);
            e.Graphics.DrawLine(ptp, new Point(340 + X6, Y_unLine), new Point(400 + X6, Y_unLine));
            e.Graphics.DrawString("cm", ptzt8, Brushes.Black, 400 + X6, Y6);
            if (ucZhiGuanDown.Controls[0].Text.Trim() == "")
                e.Graphics.DrawLine(ptp, new Point(360 + X6, Y_unLine - 11), new Point(390 + X6, Y_unLine - 2));
            e.Graphics.DrawString("病人去向 " + cmbBingRenQuXiang.Text, ptzt8, Brushes.Black, 420 + X6, Y6);
            e.Graphics.DrawLine(ptp, new Point(460 + X6, Y_unLine), new Point(575 + X6, Y_unLine));
            if (cmbBingRenQuXiang.Text.Trim() == "")
                e.Graphics.DrawLine(ptp, new Point(500 + X6, Y_unLine - 11), new Point(540 + X6, Y_unLine - 2));

            if (ucNiaoLiang.Controls[0].Text.Trim() == "")
            {
                ucNiaoLiang.Controls[0].Text = "0";
            }

            Y6 = 16 + Y6; Y_unLine = Y6 + 12;
            e.Graphics.DrawString("术后诊断 " + ucSHZD.Controls[0].Text, ptzt8, Brushes.Black, 25 + X6, Y6);
            e.Graphics.DrawLine(ptp, new Point(70 + X6, Y_unLine), new Point(400 + X6, Y_unLine));

            e.Graphics.DrawString("切口类型 " + cmbCutType.Text, ptzt8, Brushes.Black, 400 + X6, Y6);
            e.Graphics.DrawLine(ptp, new Point(445 + X6, Y_unLine), new Point(490 + X6, Y_unLine));

            e.Graphics.DrawString("手术等级 " + cmbOperLevel.Text, ptzt8, Brushes.Black, 490 + X6, Y6);
            e.Graphics.DrawLine(ptp, new Point(535 + X6, Y_unLine), new Point(575 + X6, Y_unLine));


            Y6 = 16 + Y6; Y_unLine = Y6 + 12;
            e.Graphics.DrawString("实施手术 " + ucSSSS.Controls[0].Text, ptzt8, Brushes.Black, 25 + X6, Y6);
            e.Graphics.DrawLine(ptp, new Point(70 + X6, Y_unLine), new Point(575 + X6, Y_unLine));

            Y6 = 16 + Y6; Y_unLine = Y6 + 12;
            e.Graphics.DrawString("手术医师 " + ucSSYS.Controls[0].Text, ptzt8, Brushes.Black, 25 + X6, Y6);
            e.Graphics.DrawLine(ptp, new Point(70 + X6, Y_unLine), new Point(320 + X6, Y_unLine));
            e.Graphics.DrawString("器械护士 " + ucQXHS.Controls[0].Text, ptzt8, Brushes.Black, 330 + X6, Y6);
            e.Graphics.DrawLine(ptp, new Point(375 + X6, Y_unLine), new Point(575 + X6, Y_unLine));
            Y6 = 16 + Y6; Y_unLine = Y6 + 12;
            e.Graphics.DrawString("麻醉医师 " + ucMZYS.Controls[0].Text, ptzt8, Brushes.Black, 25 + X6, Y6);
            e.Graphics.DrawLine(ptp, new Point(70 + X6, Y_unLine), new Point(320 + X6, Y_unLine));
            e.Graphics.DrawString("巡回护士 " + ucXHHS.Controls[0].Text, ptzt8, Brushes.Black, 330 + X6, Y6);
            e.Graphics.DrawLine(ptp, new Point(375 + X6, Y_unLine), new Point(575 + X6, Y_unLine));
            if (ucZongLiang.Controls[0].Text.Trim() == "")
            {
                ucZongLiang.Controls[0].Text = "  0";
            }

            //int X7 = x; int Y7 = YY + 135;
            //string RLlist = "";
            //for (int i = 0; i < listBox1.Items.Count; i++)//打印输液输血清单
            //{
            //    RLlist = RLlist + listBox1.Items[i].ToString() + "\n";

            //}
            //e.Graphics.DrawString(RLlist, ptzt7, Brushes.Black, new Point(530 + X7, Y7));




        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintView_Click(object sender, EventArgs e)
        {
            int JsFlag = 0;
            string YwName = string.Empty;
            foreach (adims_MODEL.mzqt qt in mzqtList)
            {
                if (qt.Bz == 1)
                {
                    YwName = YwName + qt.Qtname + "\n";
                    JsFlag++;
                }
            }
            foreach (adims_MODEL.mzyt ydy in ydyList) //打印诱导药
            {
                if (ydy.Bz == 1 && ydy.Cxyy)
                {
                    YwName = YwName + ydy.Ytname + "\n";
                    JsFlag++;
                }
            }

            if (JsFlag > 0)
            {
                MessageBox.Show(YwName + "没有标记结束。");
                return;
            }
            ptime = fristopen;
            //ptime = jkksTime;
            printPreDia.Document = printDoc;
            if (printPreDia.ShowDialog() == DialogResult.OK)
                printDoc.Print();
            iYema = 1;
        }

        /// <summary>
        /// 向左移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        private void btnLeft_Click(object sender, EventArgs e)
        {
            otime = otime.AddMinutes(6 * jcsjjg);
            lbtimew1.Text = lbTime1.Text = otime.ToString("HH:mm");
            lbtimew2.Text = lbTime2.Text = otime.AddMinutes(6 * jcsjjg).ToString("HH:mm");
            //+ ":" + (otime.AddMinutes(6 * jcsjjs).Minute == 0 ? "00" : "30");
            lbtimew3.Text = lbTime3.Text = otime.AddMinutes(12 * jcsjjg).ToString("HH:mm");
            lbtimew4.Text = lbTime4.Text = otime.AddMinutes(18 * jcsjjg).ToString("HH:mm");
            lbtimew5.Text = lbTime5.Text = otime.AddMinutes(24 * jcsjjg).ToString("HH:mm");
            lbtimew6.Text = lbTime6.Text = otime.AddMinutes(30 * jcsjjg).ToString("HH:mm");
            lbtimew7.Text = lbTime7.Text = otime.AddMinutes(36 * jcsjjg).ToString("HH:mm");
            lbtimew8.Text = lbTime8.Text = otime.AddMinutes(42 * jcsjjg).ToString("HH:mm");
            lbtimew9.Text = lbTime9.Text = otime.AddMinutes(48 * jcsjjg).ToString("HH:mm");
            lbtimew10.Text = lbTime10.Text = otime.AddMinutes(54 * jcsjjg).ToString("HH:mm");
            lbtimew11.Text = lbTime11.Text = otime.AddMinutes(60 * jcsjjg).ToString("HH:mm");
            lbMzks1.Location = new Point(lbMzks1.Location.X - 90, lbMzks1.Location.Y);
            lbMzjs1.Location = new Point(lbMzjs1.Location.X - 90, lbMzjs1.Location.Y);
            ssks1.Location = new Point(ssks1.Location.X - 90, ssks1.Location.Y);
            ssjs1.Location = new Point(ssjs1.Location.X - 90, ssjs1.Location.Y);
            lb_cguan.Location = new Point(lb_cguan.Location.X - 90, lb_cguan.Location.Y);
            lb_bguan.Location = new Point(lb_bguan.Location.X - 90, lb_bguan.Location.Y);
            //插管，拔管时间暂时未完成
            lab1.Text = "";
            pictureBox1.Refresh();
            pictureBox2.Refresh();
            pictureBox3.Refresh();
            pictureBox4.Refresh();
        }

        /// <summary>
        /// 向右移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRight_Click(object sender, EventArgs e)
        {
            otime = otime.AddMinutes(-6 * jcsjjg);
            lbtimew1.Text = lbTime1.Text = otime.ToString("HH:mm");
            lbtimew2.Text = lbTime2.Text = otime.AddMinutes(6 * jcsjjg).ToString("HH:mm");
            //+ ":" + (otime.AddMinutes(6 * jcsjjs).Minute == 0 ? "00" : "30");
            lbtimew3.Text = lbTime3.Text = otime.AddMinutes(12 * jcsjjg).ToString("HH:mm");
            lbtimew4.Text = lbTime4.Text = otime.AddMinutes(18 * jcsjjg).ToString("HH:mm");
            lbtimew5.Text = lbTime5.Text = otime.AddMinutes(24 * jcsjjg).ToString("HH:mm");
            lbtimew6.Text = lbTime6.Text = otime.AddMinutes(30 * jcsjjg).ToString("HH:mm");
            lbtimew7.Text = lbTime7.Text = otime.AddMinutes(36 * jcsjjg).ToString("HH:mm");
            lbtimew8.Text = lbTime8.Text = otime.AddMinutes(42 * jcsjjg).ToString("HH:mm");
            lbtimew9.Text = lbTime9.Text = otime.AddMinutes(48 * jcsjjg).ToString("HH:mm");
            lbtimew10.Text = lbTime10.Text = otime.AddMinutes(54 * jcsjjg).ToString("HH:mm");
            lbtimew11.Text = lbTime11.Text = otime.AddMinutes(60 * jcsjjg).ToString("HH:mm");
            lbMzks1.Location = new Point(lbMzks1.Location.X + 90, lbMzks1.Location.Y);
            lbMzjs1.Location = new Point(lbMzjs1.Location.X + 90, lbMzjs1.Location.Y);
            ssks1.Location = new Point(ssks1.Location.X + 90, ssks1.Location.Y);
            ssjs1.Location = new Point(ssjs1.Location.X + 90, ssjs1.Location.Y);
            lb_cguan.Location = new Point(lb_cguan.Location.X + 90, lb_cguan.Location.Y);
            lb_bguan.Location = new Point(lb_bguan.Location.X + 90, lb_bguan.Location.Y);
            //插管，拔管时间暂时未完成
            lab1.Text = "";
            pictureBox1.Refresh();
            pictureBox2.Refresh();
            pictureBox3.Refresh();
            pictureBox4.Refresh();
        }


        private void mzjldEdit_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (isSjll)
            {
                this.FormClosing -= new FormClosingEventHandler(this.mzjldEdit_FormClosing);
                this.Close();
            }
            else
            {
                if (DialogResult.OK == MessageBox.Show("确定退出麻醉记录单？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    if (ssks == false)
                    {
                        MessageBox.Show("手术开始没有选择，不能关闭！", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        e.Cancel = true;
                        return;
                    }
                    if (ssjs == false)
                    {
                        MessageBox.Show("手术结束没有选择，不能关闭！", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        e.Cancel = true;
                        return;
                    }
                    string result = string.Empty;
                    if (!mzjs)
                        result += "麻醉、";
                    if (!ssjs)
                        result += "手术、";
                    if (timer1.Enabled == true)
                        result += "监护采集、";
                    if (cmbBingRenQuXiang.Text == "")
                        result += "病人去向";
                    if (!string.IsNullOrEmpty(result))
                    {
                        if (result == "病人去向")
                            MessageBox.Show(result + " 没有选择，不能关闭！", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        else
                            MessageBox.Show(result + " 没有结束，不能关闭！", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        e.Cancel = true;
                        return;
                    }
                    this.FormClosing -= new FormClosingEventHandler(this.mzjldEdit_FormClosing);
                    dal.UpdateShoushujianinfo(0, 0, "0", Oroom);//修改手术间状态,关联家属大屏幕
                    dal.UpdatePaibanInfo(3, patID);//修改排班状态，关联医生大屏幕
                    #region 发送HL7
                    string HL7IPaddress = ConfigurationManager.AppSettings["HL7IPaddress"];

                    string message = AppendHL7stringOperConfig();
                    LogHelp.SaveLogHL7(message);
                    // if (UserFunction.PingHost(HL7IPaddress))
                    if (true)
                    {
                        if (message.Length > 0)
                        {
                            string HL7port = ConfigurationManager.AppSettings["HL7port"];
                            SenderRoutingLib.SocketSender send = new SenderRoutingLib.SocketSender();
                            object objResult;
                            int iResult = 0;
                            int count = 1;
                            if (count < 10)
                            {
                                new System.Threading.Thread(o =>
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        objResult = send.Send(message, HL7IPaddress, HL7port.ToInt32());
                                        string ack = objResult == null ? string.Empty : objResult.ToString();
                                        if (ack.Contains("AA"))
                                        {
                                            iResult++;
                                            LogHelp.SaveLogHL7(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                        }
                                        else
                                        {
                                            iResult++;
                                            LogHelp.SaveLogHL7(string.Format("\r\n消息处理失败原因：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                        }
                                    }
                                }).Start();
                            }

                        }
                        else
                        {
                            LogHelp.SaveLogHL7(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " IP地址或端口错误");
                        }

                    }
                    #endregion
                    this.timer5.Stop();//动态保存麻醉单信息关闭
                    this.Close();//退出  
                    if (_serialPort.IsOpen)
                    {
                        _serialPort.StopTransfer();
                        _serialPort.Close();
                        _serialPort.Dispose();
                    }
                }
                else
                    e.Cancel = true;  //取消关闭事件   
            }
        }
        /// <summary>
        /// 手术消息反馈
        /// </summary>
        /// <returns></returns>
        private string AppendHL7stringOperConfig()
        {
            ORM_O01 orm = new ORM_O01();
            #region 组装消息头
            orm.MSH.MessageType.MessageType.Value = "SIU";
            orm.MSH.MessageType.TriggerEvent.Value = "S21";
            orm.MSH.MessageType.MessageStructure.Value = "SIU_S12";
            orm.MSH.FieldSeparator.Value = MessageConstant.FieldSeparator;
            orm.MSH.SendingApplication.NamespaceID.Value = "SSMZ1";
            orm.MSH.SendingFacility.NamespaceID.Value = "SSMZ1";
            orm.MSH.ReceivingApplication.NamespaceID.Value = "MediII";
            orm.MSH.ReceivingFacility.NamespaceID.Value = "MediII";
            orm.MSH.EncodingCharacters.Value = MessageConstant.EncodingCharacters;
            orm.MSH.VersionID.VersionID.Value = MessageConstant.VersionID;
            orm.MSH.DateTimeOfMessage.TimeOfAnEvent.SetLongDateWithSecond(DateTime.Now);
            orm.MSH.MessageControlID.Value = MediII.Common.GUIDHelper.NewGUID();
            orm.MSH.ProcessingID.ProcessingID.Value = MessageConstant.ProcessingID;

            #endregion

            DataTable dtResult = dal.GetPaibanAndMZJLD(patID);
            DataRow dr = dtResult.Rows[0];

            #region SCH|
            String SCH = "SCH||1||||";
            string mzff = dr["Amethod"].ToString();
            DataTable dtMzff = dal.GetMzffID(mzff);
            string mzff_No = "00";
            if (dtMzff.Rows.Count > 0)
            {
                mzff_No = dtMzff.Rows[0][0].ToString();
            }
            SCH += "SSMZ" + "^^^" + mzff_No + "^" + mzff + "|||||";
            SCH += "^^^" + Convert.ToDateTime(dr["sskssj"]).ToString("yyyyMMddHHmmss") + "^" + Convert.ToDateTime(dr["ssjssj"]).ToString("yyyyMMddHHmmss");
            SCH += "~^^^" + Convert.ToDateTime(dr["otime"]).ToString("yyyyMMddHHmmss") + "^" + DateTime.Now.ToString("yyyyMMddHHmmss") + "|||||";
            SCH += Program.Customer.userno + "^^" + Program.Customer.user_name + "||||";
            SCH += Program.Customer.userno + "^^" + Program.Customer.user_name + "||||||";
            SCH += dr["patid"].ToString() + "\n";
            #endregion

            #region NTE|
            String NTE = "NTE|1|||~" + "\n";
            #endregion
         

            string PID = dr["PidInfo"].ToString();
            string PV1 = dr["Pv1Info"].ToString();
           

           string RGS= "RGS|1" + "\n";

            #region AIS|
            String AIS = "AIS|1||";
            string OperName = dr["Oname"].ToString();
            DataTable dtSSSS = dal.GetOperNo(OperName);
            string OperCode = "00";
            if (dtSSSS.Rows.Count > 0)
            {
                OperCode = dtSSSS.Rows[0]["OperCode"].ToString();
            }
            AIS += OperCode + "^" + OperName + "||||||";
            AIS += dr["GL"].ToString() + "|";
            AIS += dr["Oroom"].ToString() + "^" + dr["Second"].ToString() + "\n";
            #endregion
         
            #region 手术医生

            String AIP = "AIP|1||";
            DataTable dtOsNo = dal.GetShoushuYishengNo(dr["OS"].ToString());
            string OsNO = "";
            if (dtOsNo.Rows.Count > 0)
            {
                OsNO = dtOsNo.Rows[0]["nameNo"].ToString();
            }
            AIP += OsNO + "^" + dr["OS"].ToString() + "|主刀医生" + "\n";

            AIP += "AIP|7||";
            dtOsNo = dal.GetShoushuYishengNo(dr["OA1"].ToString());
            OsNO = "";
            if (dtOsNo.Rows.Count > 0)
            {
                OsNO = dtOsNo.Rows[0]["nameNo"].ToString();
            }
            AIP += OsNO + "^" + dr["OA1"].ToString() + "|2^助理医生" + "\n";

            AIP += "AIP|8||";
            dtOsNo = dal.GetShoushuYishengNo(dr["OA2"].ToString());
            OsNO = "";
            if (dtOsNo.Rows.Count > 0)
            {
                OsNO = dtOsNo.Rows[0]["nameNo"].ToString();
            }
            AIP += OsNO + "^" + dr["OA2"].ToString() + "|2^助理医生" + "\n";

            AIP += "AIP|9||";
            dtOsNo = dal.GetShoushuYishengNo(dr["OA3"].ToString());
            OsNO = "";
            if (dtOsNo.Rows.Count > 0)
            {
                OsNO = dtOsNo.Rows[0]["nameNo"].ToString();
            }
            AIP += OsNO + "^" + dr["OA3"].ToString() + "|2^助理医生" + "\n";
            #endregion

            #region 护士
            DataTable dt = dal.GetUserNoByName(dr["SN1"].ToString());
            string UserNO = string.Empty;
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|2||";
            AIP += UserNO + "^" + dr["SN1"].ToString() + "|4^洗手护士" + "\n";
            dt = dal.GetUserNoByName(dr["SN2"].ToString());
            UserNO = string.Empty;
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|3||";
            AIP += UserNO + "^" + dr["SN2"].ToString() + "|4^洗手护士" + "\n";
            dt = dal.GetUserNoByName(dr["ON1"].ToString());
            UserNO = string.Empty;
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|4||";
            AIP += UserNO + "^" + dr["ON1"].ToString() + "|5^巡回护士" + "\n";
            dt = dal.GetUserNoByName(dr["ON2"].ToString());
            UserNO = string.Empty;
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|5||";
            AIP += UserNO + "^" + dr["ON2"].ToString() + "|5^巡回护士" + "\n";

            dt = dal.GetUserNoByName(dr["ON3"].ToString());
            UserNO = string.Empty;
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|6||";
            AIP += UserNO + "^" + dr["ON3"].ToString() + "|5^巡回护士" + "\n";
            #endregion

            AIP += "AIP|10|||6^体外循环师" + "\n";

            #region 麻醉医生
            dt = dal.GetUserNoByName(dr["AP1"].ToString());
            UserNO = string.Empty;
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|11||";
            AIP += UserNO + "^" + dr["AP1"].ToString() + "|3^麻醉师" + "\n";
            dt = dal.GetUserNoByName(dr["AP2"].ToString());
            UserNO = string.Empty;
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|12||";
            AIP += UserNO + "^" + dr["AP2"].ToString() + "|7^助理麻醉师" + "\n";
            dt = dal.GetUserNoByName(dr["AP3"].ToString());
            UserNO = string.Empty;
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|13||";
            AIP += UserNO + "^" + dr["AP3"].ToString() + "|7^助理麻醉师" + "\n";
            #endregion

     

            #region 转换消息对象为字符串
            String hl7Message = SCH+ NTE+ PID + PV1 + RGS + AIS  + AIP;
            NHapi.Base.Parser.PipeParser parser = new NHapi.Base.Parser.PipeParser();
            string message = parser.Encode(orm) + hl7Message;
            return message;
            #endregion

        }
        private void BackToHisYSandHS()
        {
            OperAisthService hisWS = new OperAisthService();
            string Emp_Aisth = ucMZYS.Controls[0].Text.Trim();
            string EmpID_Aisth = "";
            if (Emp_Aisth != "")
            {
                if (Emp_Aisth.Contains('、'))
                    EmpID_Aisth = dal.SelectUserNo(Emp_Aisth.Substring(0, Emp_Aisth.IndexOf('、')));
                else
                    EmpID_Aisth = dal.SelectUserNo(Emp_Aisth);
            }

            string Emp_Tool = ucQXHS.Controls[0].Text.Trim();
            string EmpID_Tool = "";
            if (Emp_Tool != "")
            {
                if (Emp_Tool.Contains('、'))
                    EmpID_Tool = dal.SelectUserNo(Emp_Tool.Substring(0, Emp_Tool.IndexOf('、')));
                else
                    EmpID_Tool = dal.SelectUserNo(Emp_Tool);
            }
            string Emp_Tour = ucXHHS.Controls[0].Text.Trim();
            string EmpID_Tour = "";
            if (Emp_Tour != "")
            {
                if (Emp_Tour.Contains('、'))
                    EmpID_Tour = dal.SelectUserNo(Emp_Tour.Substring(0, Emp_Tour.IndexOf('、')));
                else
                    EmpID_Tour = dal.SelectUserNo(Emp_Tour);
            }
            string AisthBeginDate = "";
            string AisthEndDate = "";
            string OPBeginDate = "";
            string OPEndDate = "";
            if (mzksTime != null)
            {
                AisthBeginDate = mzksTime.ToString("yyyy-MM-dd HH:mm:00");
            }
            if (mzjsTime != null)
            {
                AisthEndDate = mzjsTime.ToString("yyyy-MM-dd HH:mm:00");
            }
            if (ssksTime != null)
            {
                OPBeginDate = ssksTime.ToString("yyyy-MM-dd HH:mm:00");
            }
            if (ssjsTime != null)
            {
                OPEndDate = ssjsTime.ToString("yyyy-MM-dd HH:mm:00");

            }
            //手术排班信息
            var doc = new XDocument(
                            new XElement("Params",
                                    new XElement("ApplyID", patID),
                                    new XElement("EmpID_Aisth", EmpID_Aisth),
                                    new XElement("EmpID_Tool", EmpID_Tool),
                                    new XElement("EmpID_Tour", EmpID_Tour),
                                    new XElement("AisthBeginDate", AisthBeginDate),
                                    new XElement("AisthEndDate", AisthEndDate),
                                    new XElement("OPBeginDate", OPBeginDate),
                                    new XElement("OPEndDate", OPEndDate)
                                         )
                                    );

            string operinfo = doc.ToString();
            string resultXML = hisWS.OperationEnd(operinfo);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(resultXML);
            string result = "";
            XmlNodeList nodeList = xmldoc.SelectSingleNode("Szyy").ChildNodes;
            foreach (XmlNode node in nodeList)//遍历所有子节点
            {
                if (node.Name == "Status")
                {
                    result = node.InnerText;
                    break;
                }
            }
            //if (result != "1")
            //{
            //    //MessageBox.Show("反馈失败");
            //}
            ////return result;
        }

        private void BackToTime()
        {
            OperAisthService hisWS = new OperAisthService();
            string AisthBeginDate = mzksTime.ToString();
            string AisthEndDate = mzjsTime.ToString();
            string OPBeginDate = ssksTime.ToString();
            string OPEndDate = ssjsTime.ToString();
            //手术排班信息
            var doc = new XDocument(
                            new XElement("Params",
                                    new XElement("ApplyID", patID),
                                    new XElement("AisthBeginDate", AisthBeginDate),
                                    new XElement("AisthEndDate", AisthEndDate),
                                    new XElement("OPBeginDate", OPEndDate),
                                    new XElement("OPEndDate", OPEndDate)
                                         )
                                    );

            string operinfo = doc.ToString();
            //hisWS.OperCancle(patID);//手术是否取消
            string resultXML = hisWS.OperationEnd(operinfo);//麻醉手术等时间信息
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(resultXML);
            string result = "";
            XmlNodeList nodeList = xmldoc.SelectSingleNode("Szyy").ChildNodes;
            foreach (XmlNode node in nodeList)//遍历所有子节点
            {
                if (node.Name == "Status")
                {
                    result = node.InnerText;
                    break;
                }
            }
            //if (result != "1")
            //{
            //    MessageBox.Show("反馈失败");
            //}
            //return result;
        }
        #region <<麻醉开始>>

        /// <summary>
        /// 双击麻醉开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        int xStart, xEnd, yStart;//移动麻醉，手术，插管开始结束点
        private void lbMzks_DoubleClick(object sender, EventArgs e)
        {
            if (!mzks)
            {
                //txtMzsj.Controls[0].Text = Convert.ToString(DateTime.Now);
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                mzksTime = DateTime.Now;
                lbMzks1.Visible = true;
                lbMzks1.Text = "X";
                lbMzks1.AutoSize = true;
                lbMzks1.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lbMzks1.BackColor = Color.Transparent;
                lbMzks1.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 165), 180);
                this.pictureBox4.Controls.Add(lbMzks1);
                mzks = true;
                mzjs = false;
                dal.UpdateMzkssj(DateTime.Now, mzjldID);
                dal.UpdateShoushujianinfo(2, Oroom);//修改手术间列表状态--麻醉开始
                lbMzks1.MouseDown += new MouseEventHandler(lbMzks1_MouseDown);
                lbMzks1.MouseMove += new MouseEventHandler(lbMzks1_MouseMove);
                lbMzks1.MouseUp += new MouseEventHandler(lbMzks1_MouseUp);
                lbMzks1.MouseLeave += new EventHandler(lbMzks1_MouseLeave);
            }
            else
                MessageBox.Show("麻醉已经开始");
        }

        private void lbMzks1_MouseDown(object sender, EventArgs e)
        {
            p3lf1 = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.Red;
            lab1.AutoSize = true;
            pictureBox4.Controls.Add(lab1);
            lab1.Location = new Point(lbMzks1.Location.X, lbMzks1.Location.Y - 10);
            xStart = lbMzks1.Location.X;
            yStart = lbMzks1.Location.Y;
            DateTime DTIME = otime.AddMinutes((xStart - 165) / 15 * jcsjjg);
            lab1.Text = DTIME.ToString("HH:mm");
        }

        private void lbMzks1_MouseMove(object sender, MouseEventArgs e)
        {
            if (p3lf1 == 1)
            {
                //TimeSpan t = new TimeSpan();
                //t = DateTime.Now - otime;
                lbMzks1.Location = new Point(lbMzks1.Location.X + e.X / 2 - 2, lbMzks1.Location.Y);
                xEnd = lbMzks1.Location.X;
                lab1.Visible = true;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Blue;
                lab1.AutoSize = true;
                pictureBox4.Controls.Add(lab1);
                lab1.Location = new Point(lbMzks1.Location.X, lbMzks1.Location.Y - 10);
                DateTime DTIME = otime.AddMinutes((xEnd - 165) / 15 * jcsjjg);
                lab1.Text = DTIME.ToString("HH:mm");
                timer3.Enabled = true;
                //txtMZKS.Controls[0].Text = DTIME.ToString("HH:mm");


            }
        }

        private void lbMzks1_MouseUp(object sender, EventArgs e)
        {
            p3lf1 = 0;
            if (!mzjs)
            {
                dal.UpdateMzkssj(otime.AddMinutes((xEnd - 165) / 15 * jcsjjg), mzjldID);
                mzksTime = otime.AddMinutes((xEnd - 165) / 15 * jcsjjg);
            }
            else
            {
                if (xEnd < lbMzjs1.Location.X)
                {
                    dal.UpdateMzkssj(otime.AddMinutes((xEnd - 165) / 15 * jcsjjg), mzjldID);
                    mzksTime = otime.AddMinutes((xEnd - 165) / 15 * jcsjjg);
                }
                else
                    lbMzks1.Location = new Point(xStart, yStart);
            }
        }

        private void lbMzks1_MouseLeave(object sender, EventArgs e)
        {
            p3lf1 = 0;
        }

        #endregion

        #region <<麻醉结束>>
        /// <summary>
        /// 麻醉结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbMzjs_DoubleClick(object sender, EventArgs e)
        {
            if (mzks && !mzjs)
            {
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                mzjsTime = DateTime.Now;
                lbMzjs1.Visible = true;
                lbMzjs1.Text = "X";
                lbMzjs1.AutoSize = true;
                lbMzjs1.BackColor = Color.Transparent;
                lbMzjs1.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lbMzjs1.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 165), 180);
                this.pictureBox4.Controls.Add(lbMzjs1);
                mzjs = true;
                dal.UpdateMzjssj(DateTime.Now, mzjldID);
                //dal.UpdateShoushujianinfo(4, Oroom);//修改手术间状态信息--麻醉恢复中结束
                lbMzjs1.MouseDown += new MouseEventHandler(lbMzjs1_MouseDown);
                lbMzjs1.MouseMove += new MouseEventHandler(lbMzjs1_MouseMove);
                lbMzjs1.MouseUp += new MouseEventHandler(lbMzjs1_MouseUp);
                lbMzjs1.MouseLeave += new EventHandler(lbMzjs1_MouseLeave);
            }
            else
                MessageBox.Show("麻醉未开始不能结束或者已经结束");
        }

        private void lbMzjs1_MouseDown(object sender, EventArgs e)
        {
            p3lf2 = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.Blue;
            lab1.AutoSize = true;
            pictureBox4.Controls.Add(lab1);
            lab1.Location = new Point(lbMzjs1.Location.X, lbMzjs1.Location.Y - 10);
            xStart = lbMzjs1.Location.X;
            yStart = lbMzjs1.Location.Y;
            DateTime DTIME = otime.AddMinutes((xStart - 165) / 15 * jcsjjg);
            lab1.Text = DTIME.ToString("HH:mm");
        }

        private void lbMzjs1_MouseMove(object sender, MouseEventArgs e)
        {
            if (p3lf2 == 1)
            {
                lbMzjs1.Location = new Point(lbMzjs1.Location.X + e.X / 2 - 2, lbMzjs1.Location.Y);
                xEnd = lbMzjs1.Location.X;
                lab1.Visible = true;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Blue;
                lab1.AutoSize = true;
                pictureBox4.Controls.Add(lab1);
                lab1.Location = new Point(lbMzjs1.Location.X, lbMzjs1.Location.Y - 10);
                DateTime DTIME = otime.AddMinutes((xEnd - 165) / 15 * jcsjjg);
                lab1.Text = DTIME.ToString("HH:mm");
                timer3.Enabled = true;
                //int ssshijian = (lbMzjs1.Location.X - lbMzks1.Location.X) / 3);//计算麻醉总时间
                //txtMZJS.Controls[0].Text =DTIME.ToString("HH:mm");

            }
        }

        private void lbMzjs1_MouseUp(object sender, EventArgs e)
        {
            p3lf2 = 0;
            if (xEnd > lbMzks1.Location.X)
            {
                dal.UpdateMzjssj(otime.AddMinutes((xEnd - 165) / 15 * jcsjjg), mzjldID);
                mzjsTime = otime.AddMinutes((xEnd - 165) / 15 * jcsjjg);
            }
            else
                lbMzjs1.Location = new Point(xStart, yStart);
            pictureBox4.Refresh();
            pictureBox3.Refresh();


        }

        private void lbMzjs1_MouseLeave(object sender, EventArgs e)
        {
            p3lf2 = 0;
        }
        #endregion

        #region <<插管>>
        private void lbCg_DoubleClick(object sender, EventArgs e)
        {
            if (!CGUAN)
            {
                //txtMzsj.Controls[0].Text = Convert.ToString(DateTime.Now);
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                cgTime = DateTime.Now;
                lb_cguan.Visible = true;
                lb_cguan.Text = "Θ";
                lb_cguan.AutoSize = true;
                lb_cguan.BackColor = Color.Transparent;
                lb_cguan.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lb_cguan.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 165), 180);
                this.pictureBox4.Controls.Add(lb_cguan);
                CGUAN = true;
                BGUAN = false;
                dal.UpdateMzCG(DateTime.Now, mzjldID);
                //txtCGKS.Controls[0].Text = DateTime.Now.ToString("HH:mm");
                lb_cguan.MouseDown += new MouseEventHandler(lb_cguan_MouseDown);
                lb_cguan.MouseMove += new MouseEventHandler(lb_cguan_MouseMove);
                lb_cguan.MouseUp += new MouseEventHandler(lb_cguan_MouseUp);
                lb_cguan.MouseLeave += new EventHandler(lb_cguan_MouseLeave);
                pictureBox4.Refresh();
            }
            else
                MessageBox.Show("已经插管");
        }

        private void lb_cguan_MouseDown(object sender, EventArgs e)
        {
            p3lf1 = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.Blue;
            lab1.AutoSize = true;
            pictureBox4.Controls.Add(lab1);
            lab1.Location = new Point(lb_cguan.Location.X, lb_cguan.Location.Y - 10);
            xStart = lb_cguan.Location.X;
            yStart = lb_cguan.Location.Y;
            DateTime DTIME = otime.AddMinutes((xStart - 165) / 15 * jcsjjg);
            lab1.Text = DTIME.ToString("HH:mm");

        }

        private void lb_cguan_MouseLeave(object sender, EventArgs e)
        {
            p3lf1 = 0;

        }

        private void lb_cguan_MouseMove(object sender, MouseEventArgs e)
        {
            if (p3lf1 == 1)
            {
                //TimeSpan t = new TimeSpan();
                //t = DateTime.Now - otime;
                lb_cguan.Location = new Point(lb_cguan.Location.X + e.X / 2 - 2, lb_cguan.Location.Y);
                xEnd = lb_cguan.Location.X;
                lab1.Visible = true;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Blue;
                lab1.AutoSize = true;
                pictureBox4.Controls.Add(lab1);
                lab1.Location = new Point(lb_cguan.Location.X, lb_cguan.Location.Y - 10);
                DateTime DTIME = otime.AddMinutes((xEnd - 165) / 15 * jcsjjg);
                lab1.Text = DTIME.ToString("HH:mm");
                timer3.Enabled = true;
                //txtCGKS.Controls[0].Text = DTIME.ToString("HH:mm");
            }
        }

        private void lb_cguan_MouseUp(object sender, EventArgs e)
        {
            p3lf1 = 0;
            if (!BGUAN)
            {
                dal.UpdateMzCG(otime.AddMinutes((xEnd - 165) / 15 * jcsjjg), mzjldID);
                cgTime = otime.AddMinutes((xEnd - 165) / 15 * jcsjjg);
            }
            else
            {
                if (xEnd < lb_bguan.Location.X)
                {
                    dal.UpdateMzCG(otime.AddMinutes((xEnd - 165) / 15 * jcsjjg), mzjldID);
                    cgTime = otime.AddMinutes((xEnd - 165) / 15 * jcsjjg);
                }
                else
                    lb_cguan.Location = new Point(xStart, yStart);
            }
            pictureBox4.Refresh();
            pictureBox3.Refresh();
        }
        #endregion

        #region <<拔管>>
        private void lbBg_DoubleClick(object sender, EventArgs e)
        {
            if (CGUAN && !BGUAN)
            {
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                bgTime = DateTime.Now;
                lb_bguan.Visible = true;
                lb_bguan.Text = "Φ";
                lb_bguan.AutoSize = true;
                lb_bguan.BackColor = Color.Transparent;
                lb_bguan.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lb_bguan.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 165), 180);
                this.pictureBox4.Controls.Add(lb_bguan);
                BGUAN = true;
                dal.UpdateMzBG(DateTime.Now, mzjldID);
                //txtCGJS.Controls[0].Text = DateTime.Now.ToString("HH:mm");
                lb_bguan.MouseDown += new MouseEventHandler(lb_bguan_MouseDown);
                lb_bguan.MouseMove += new MouseEventHandler(lb_bguan_MouseMove);
                lb_bguan.MouseUp += new MouseEventHandler(lb_bguan_MouseUp);
                lb_bguan.MouseLeave += new EventHandler(lb_bguan_MouseLeave);
            }
            else
                MessageBox.Show("没有插管或者拔管已经结束");
        }

        private void lb_bguan_MouseDown(object sender, EventArgs e)
        {
            p3lf2 = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.Blue;
            lab1.AutoSize = true;
            pictureBox4.Controls.Add(lab1);
            lab1.Location = new Point(lb_bguan.Location.X, lb_bguan.Location.Y - 10);
            xStart = lb_bguan.Location.X;
            yStart = lb_bguan.Location.Y;
            DateTime DTIME = otime.AddMinutes((xStart - 165) / 15 * jcsjjg);
            lab1.Text = DTIME.ToString("HH:mm");
        }

        private void lb_bguan_MouseMove(object sender, MouseEventArgs e)
        {
            if (p3lf2 == 1)
            {
                lb_bguan.Location = new Point(lb_bguan.Location.X + e.X / 2 - 2, lb_bguan.Location.Y);
                xEnd = lb_bguan.Location.X;
                lab1.Visible = true;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Blue;
                lab1.AutoSize = true;
                pictureBox4.Controls.Add(lab1);
                lab1.Location = new Point(lb_bguan.Location.X, lb_bguan.Location.Y - 10);
                DateTime DTIME = otime.AddMinutes((xEnd - 165) / 15 * jcsjjg);
                lab1.Text = DTIME.ToString("HH:mm");
                //int ssshijian = (lb_bguan.Location.X - lb_bguan.Location.X) / 3);//计算拔管时间
                timer3.Enabled = true;
                //txtCGJS.Controls[0].Text = DTIME.ToString("HH:mm");
            }
        }

        private void lb_bguan_MouseUp(object sender, EventArgs e)
        {
            p3lf2 = 0;
            if (xEnd > lb_cguan.Location.X)
            {
                dal.UpdateMzBG(otime.AddMinutes((xEnd - 165) / 15 * jcsjjg), mzjldID);
                bgTime = otime.AddMinutes((xEnd - 165) / 15 * jcsjjg);
            }
            else
                lb_bguan.Location = new Point(xStart, yStart);
            pictureBox4.Refresh();
            pictureBox3.Refresh();

        }

        private void lb_bguan_MouseLeave(object sender, EventArgs e)
        {
            p3lf2 = 0;
        }
        #endregion

        #region <<手术开始>>


        private void lbSsks_DoubleClick(object sender, EventArgs e)
        {
            if (!ssks)
            {
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                ssksTime = DateTime.Now;
                ssks1.Visible = true;
                ssks1.Text = "⊙";
                ssks1.AutoSize = true;
                ssks1.BackColor = Color.Transparent;
                ssks1.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                ssks1.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 165), 180);
                this.pictureBox4.Controls.Add(ssks1);
                ssks = true;
                ssjs = false;
                dal.UpdateSskssj(DateTime.Now, mzjldID);
                dal.UpdateShoushujianinfo(3, Oroom);//修改手术间状态信息--手术开始    

                ssks1.MouseDown += new MouseEventHandler(ssks1_MouseDown);
                ssks1.MouseMove += new MouseEventHandler(ssks1_MouseMove);
                ssks1.MouseUp += new MouseEventHandler(ssks1_MouseUp);
                ssks1.MouseLeave += new EventHandler(ssks1_MouseLeave);
            }
            else
                MessageBox.Show("手术已经开始");
        }

        private void ssks1_MouseDown(object sender, EventArgs e)
        {
            p3lf1 = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.Blue;
            lab1.AutoSize = true;
            pictureBox4.Controls.Add(lab1);
            lab1.Location = new Point(ssks1.Location.X, ssks1.Location.Y - 10);
            xStart = ssks1.Location.X;
            yStart = ssks1.Location.Y;
            DateTime DTIME = otime.AddMinutes((xStart - 165) / 15 * jcsjjg);
            lab1.Text = DTIME.ToString("HH:mm");
        }

        private void ssks1_MouseUp(object sender, EventArgs e)
        {
            p3lf1 = 0;
            if (!ssjs)
            {
                dal.UpdateSskssj(otime.AddMinutes((xEnd - 165) / 15 * jcsjjg), mzjldID);
                ssksTime = otime.AddMinutes((xEnd - 165) / 15 * jcsjjg);

            }
            else
            {
                if (xEnd < ssjs1.Location.X)
                {
                    dal.UpdateSskssj(otime.AddMinutes((xEnd - 165) / 15 * jcsjjg), mzjldID);
                    ssksTime = otime.AddMinutes((xEnd - 165) / 15 * jcsjjg);

                }
                else
                    ssks1.Location = new Point(xStart, yStart);
            }

        }

        private void ssks1_MouseLeave(object sender, EventArgs e)
        {
            p3lf1 = 0;
        }

        private void ssks1_MouseMove(object sender, MouseEventArgs e)
        {
            if (p3lf1 == 1)
            {
                ssks1.Location = new Point(ssks1.Location.X + e.X / 2 - 2, ssks1.Location.Y);
                xEnd = ssks1.Location.X;
                lab1.Visible = true;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Blue;
                lab1.AutoSize = true;
                pictureBox4.Controls.Add(lab1);
                lab1.Location = new Point(ssks1.Location.X, ssks1.Location.Y - 10);
                DateTime DTIME = otime.AddMinutes((xEnd - 165) / 15 * jcsjjg);
                lab1.Text = DTIME.ToString("HH:mm");
                timer3.Enabled = true;
                //txtSSKS.Controls[0].Text = DTIME.ToString("HH:mm");
            }
        }

        #endregion

        #region <<手术结束>>
        /// <summary>
        /// 双击手术结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void lbSsjs_DoubleClick(object sender, EventArgs e)
        {
            if (ssks && !ssjs)
            {
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                ssjsTime = DateTime.Now;
                ssjs1.Text = "⊙";
                ssjs1.Visible = true;
                ssjs1.AutoSize = true;
                ssjs1.BackColor = Color.Transparent;
                ssjs1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                ssjs1.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 165), 180);
                this.pictureBox4.Controls.Add(ssjs1);
                ssjs = true;
                dal.UpdateSsjssj(DateTime.Now, mzjldID);
                dal.UpdateSsjsFlag(mzjldID);
                dal.UpdateShoushujianinfo(4, Oroom);//修改手术间状态信息--手术结束
                //txtSSJS.Controls[0].Text = DateTime.Now.ToString("HH:mm");
                ssjs1.MouseDown += new MouseEventHandler(ssjs1_MouseDown);
                ssjs1.MouseMove += new MouseEventHandler(ssjs1_MouseMove);
                ssjs1.MouseUp += new MouseEventHandler(ssjs1_MouseUp);
                ssjs1.MouseLeave += new EventHandler(ssjs1_MouseLeave);

                this.timer5.Stop();
            }
            else
                MessageBox.Show("手术未开始或手术已经结束！");
        }

        private void ssjs1_MouseDown(object sender, EventArgs e)
        {
            p3lf2 = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.Blue;
            lab1.AutoSize = true;
            pictureBox4.Controls.Add(lab1);
            lab1.Location = new Point(ssjs1.Location.X, ssjs1.Location.Y - 10);
            xStart = ssjs1.Location.X;
            yStart = ssjs1.Location.Y;
            DateTime DTIME = otime.AddMinutes((xStart - 165) / 15 * jcsjjg);
            lab1.Text = DTIME.ToString("HH:mm");

        }

        private void ssjs1_MouseMove(object sender, MouseEventArgs e)
        {
            if (p3lf2 == 1)
            {
                ssjs1.Location = new Point(ssjs1.Location.X + e.X / 2 - 2, ssjs1.Location.Y);
                xEnd = ssjs1.Location.X;
                lab1.Visible = true;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Blue;
                lab1.AutoSize = true;
                pictureBox4.Controls.Add(lab1);
                lab1.Location = new Point(ssjs1.Location.X, ssjs1.Location.Y - 10);
                DateTime DTIME = otime.AddMinutes((xEnd - 165) / 15 * jcsjjg);
                lab1.Text = DTIME.ToString("HH:mm");
                timer3.Enabled = true;
                //int ssshijian=(ssjs1.Location.X - ssks1.Location.X) / 3.27);
                //txtSssj.Controls[0].Text = (ssshijian / 60).ToString() + "小时"+(ssshijian % 60).ToString()+"分";
                //txtSSJS.Controls[0].Text = DTIME.ToString("HH:mm");
            }
        }

        private void ssjs1_MouseUp(object sender, EventArgs e)
        {
            p3lf2 = 0;
            if (xEnd > ssks1.Location.X)
            {
                dal.UpdateSsjssj(otime.AddMinutes((xEnd - 165) / 15 * jcsjjg), mzjldID);
                ssjsTime = otime.AddMinutes((xEnd - 165) / 15 * jcsjjg);
            }
            else
                ssjs1.Location = new Point(xStart, yStart);
        }

        private void ssjs1_MouseLeave(object sender, EventArgs e)
        {
            p3lf2 = 0;
        }


        #endregion
        /// <summary>
        /// 影像病历
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            string cardno = at.GetCardno(patID).ToString();
            //PacsForm yxblform = new PacsForm(cardno);
            //yxblform.Show();

            var remotepath = ConfigurationManager.AppSettings["PACS"];
            string http = remotepath + cardno;
            BrowserHelper.OpenDefaultBrowserUrl(http);
        }

        /// <summary>
        /// 检验病历
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void button9_Click(object sender, EventArgs e)
        {
            string applyid = at.GeApplyid(patID).ToString();
            LisTest f1 = new LisTest(applyid, patID);
            f1.Show();

        }

        /// <summary>
        /// 实时数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            /*  
             sssj sssjform = new sssj();
              sssjform.Show();
             */
            sssj_temp myform = new sssj_temp();
            myform.Show();
        }



        #region <<Methods>>

        /// <summary>
        /// 更新麻醉记录单
        /// </summary>
        public int SaveMzjld()
        {
            int result = 0;
            List<string> mzdList1 = new List<string>();
            mzdList1.Add(this.cmbTiwei.Text.Trim());
            mzdList1.Add(this.cmbMZFF.Text.Trim());
            mzdList1.Add(this.cmbZZFF.Text.Trim());
            mzdList1.Add(this.cmbCCdianUp1.Text.Trim());
            mzdList1.Add(cmbCCdianUp2.Text.Trim());
            mzdList1.Add(this.ucZhiGuanUp.Controls[0].Text.Trim());
            mzdList1.Add(this.cmbCCdianDown1.Text.Trim());
            mzdList1.Add(cmbCCdianUp2.Text.Trim());
            mzdList1.Add(this.ucZhiGuanDown.Controls[0].Text.Trim());
            mzdList1.Add(this.ucSHZD.Controls[0].Text.Trim());
            mzdList1.Add(this.ucSSSS.Controls[0].Text.Trim());
            mzdList1.Add(this.ucSSYS.Controls[0].Text.Trim());
            mzdList1.Add(this.ucMZYS.Controls[0].Text.Trim());
            mzdList1.Add(this.ucQXHS.Controls[0].Text.Trim());
            mzdList1.Add(this.ucXHHS.Controls[0].Text.Trim());
            mzdList1.Add(ucSqyy.Controls[0].Text.Trim());
            mzdList1.Add(cmbXueXing.Text.Trim());
            mzdList1.Add(this.cmbChaguan.Text.Trim());
            mzdList1.Add(ucTSBQ.Controls[0].Text.Trim());
            mzdList1.Add(this.cmbASA.Text.Trim());
            if (cbE.Checked)
                mzdList1.Add("1");
            else
                mzdList1.Add("0");
            mzdList1.Add(Convert.ToString(mzjldID));
            mzdList1.Add(ucWeight.Controls[0].Text.Trim());
            mzdList1.Add(ucHeight.Controls[0].Text.Trim());
            mzdList1.Add(ucTemp.Controls[0].Text.Trim());
            mzdList1.Add(this.ucChuxue.Controls[0].Text.Trim());
            mzdList1.Add(this.cmbMZXG.Text.Trim());
            mzdList1.Add(this.ucNiaoLiang.Controls[0].Text.Trim());
            string isEye = "0";
            if (cbEyeOper.Checked)
            {
                isEye = "1";
            }
            mzdList1.Add(isEye);
            mzdList1.Add(cmbOperLevel.Text);
            mzdList1.Add(cmbCutType.Text);
            result = dal.UpdateMzjldInfo(mzdList1);
            return result;
        }
        public int SavePaibanInfo()
        {
            int result = 0;

            string height = ucHeight.Controls[0].Text.Trim();
            string weight = ucWeight.Controls[0].Text.Trim();
            string sqlWhere = $" PatHeight = '{height}',PatWeight= '{weight}' ";

            result = dal.UpdatePaibanWhere(patID, sqlWhere);
            return result;
        }


        #endregion

        private string SwitchNumToEnglish(int i)
        {
            string Str = "";
            switch (i)
            {
                case 1: Str = "a"; break;
                case 2: Str = "b"; break;
                case 3: Str = "c"; break;
                case 4: Str = "d"; break;
                case 5: Str = "e"; break;
                case 6: Str = "f"; break;
                case 7: Str = "g"; break;
                case 8: Str = "h"; break;
                case 9: Str = "i"; break;
                case 10: Str = "j"; break;
                case 11: Str = "k"; break;
                case 12: Str = "L"; break;
                case 13: Str = "m"; break;
                case 14: Str = "n"; break;
                case 15: Str = "o"; break;
                case 16: Str = "p"; break;
                case 17: Str = "q"; break;
                case 18: Str = "r"; break;
                case 19: Str = "s"; break;
                case 20: Str = "t"; break;
                case 21: Str = "u"; break;
                case 22: Str = "v"; break;
                case 23: Str = "w"; break;
                case 24: Str = "x"; break;
                case 25: Str = "y"; break;
                case 26: Str = "z"; break;
            }
            return Str;
        }

        #region <<画窗体格子>>

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen pblack2 = new Pen(Brushes.Black);
            pblack2.Width = 2;
            e.Graphics.DrawLine(pblack2, new Point(8, 5), new Point(1069, 5));
            e.Graphics.DrawLine(pblack2, new Point(8, 140), new Point(1069, 140));
            e.Graphics.DrawLine(pblack2, new Point(9, 5), new Point(9, 166));
            e.Graphics.DrawLine(pblack2, new Point(1069, 5), new Point(1069, 166));
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // label9.Text = e.X.ToString() + "    " + e.Y.ToString();
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            Font ht9 = new System.Drawing.Font("微软雅黑", 9);

            Pen pred2 = new Pen(Brushes.Red);
            pred2.Width = 2;
            Pen pblack2 = new Pen(Brushes.Black);
            pblack2.Width = 2;
            Pen pxuxian = new Pen(Brushes.Black);
            pxuxian.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            e.Graphics.DrawLine(pblack2, new Point(8, 13), new Point(1069, 13));
            //e.Graphics.DrawLine(Pens.Black, new Point(8, 28), new Point(954, 28));
            for (int i = 0; i < 17; i++)
                e.Graphics.DrawLine(Pens.Black, new Point(28, 28 + 15 * i), new Point(1069, 28 + 15 * i));
            //↑横细线
            e.Graphics.DrawLine(Pens.Black, new Point(169, 9), new Point(169, 283));

            //for (int i = 0; i < 8; i++)
            //{
            //    e.Graphics.DrawLine(pxuxian, new PointF((float)(  i * 98 + 169), (float)16), new PointF((float)( i * 98 + 169), (float)347));
            //}
            for (int i = 0; i < 60; i++)
                e.Graphics.DrawLine(pxuxian, new PointF((float)(i * 90 / 6 + 169), (float)12), new PointF((float)(i * 90 / 6 + 169), (float)283));
            for (int i = 0; i < 10; i++)
                e.Graphics.DrawLine(Pens.Black, new PointF((float)(i * 90 + 169), (float)12), new PointF((float)(i * 90 + 169), (float)283));


            e.Graphics.DrawLine(Pens.Black, new Point(9, 28), new Point(28, 28));
            e.Graphics.DrawLine(Pens.Black, new Point(9, 178), new Point(28, 178));
            e.Graphics.DrawLine(Pens.Black, new Point(9, 208), new Point(28, 208));
            e.Graphics.DrawLine(Pens.Black, new Point(9, 238), new Point(28, 238));
            e.Graphics.DrawLine(Pens.Black, new Point(9, 283), new Point(28, 283));

            e.Graphics.DrawLine(pblack2, new Point(9, 0), new Point(9, 292));
            e.Graphics.DrawLine(pblack2, new Point(1069, 0), new Point(1069, 292));
            //e.Graphics.DrawLine(pblack2, new Point(1040, 0), new Point(1040, 292));

            e.Graphics.DrawLine(Pens.Black, new Point(28, 12), new Point(28, 283));
            Pen pblue2 = new Pen(Brushes.Green);

            pblue2.Width = 2;   //画蓝色的横线
            #region //画气体
            ArrayList sssQT = new ArrayList();
            int dy = 0;// 控制画气体输出的Y坐标           
            foreach (adims_MODEL.mzqt mz in mzqtList)
            {
                if (mz.Bz > 0)
                {
                    if (sssQT.Contains(mz.Qtname))
                    {
                        dy = sssQT.IndexOf(mz.Qtname);
                    }
                    else
                    {
                        dy = sssQT.Count;
                        sssQT.Add(mz.Qtname);
                    }
                    TimeSpan t = new TimeSpan();
                    TimeSpan t1 = new TimeSpan();
                    if (mz.Bz == 1)
                    {
                        t = mz.Sysj - otime;
                        t1 = DateTime.Now - otime;
                    }
                    else if (mz.Bz == 2)
                    {
                        t = mz.Sysj - otime;
                        t1 = mz.Jssj - otime;
                    }
                    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                    int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 170);
                    int y1 = 15 * (dy) + 18;
                    string drawStr = mz.Yl.ToString() + mz.Dw.ToString();
                    e.Graphics.DrawString(drawStr, ht9, Brushes.Blue, new Point(x1, y1 - 10));
                    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });

                    if (x2 - x1 > 5 && mz.Bz == 1)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1));
                        e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1 + 10));
                    }
                    else if (x2 - x1 > 5 && mz.Bz == 2)
                    {
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 5, y1 + 8), new Point(x2 + 5, y1 + 8) });

                    }
                    if (x2 - x1 > 5)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x1, y1 + 5), new Point(x2, y1 + 5));

                    }
                    //dy++;
                    //sssQT.Add(mz.Qtname);
                }
            }
            #endregion

            #region //画诱导药
            ArrayList sssYDY = new ArrayList();
            int dy1 = 3;// 控制画气体输出的Y坐标  
            foreach (adims_MODEL.mzyt yt in ydyList)//画诱导药
            {
                if (yt.Bz > 0)
                {
                    if (sssYDY.Contains(yt.Ytname))
                    {
                        dy1 = sssYDY.IndexOf(yt.Ytname) + 3;
                    }
                    else
                    {
                        dy1 = sssYDY.Count + 3;
                        sssYDY.Add(yt.Ytname);
                        e.Graphics.DrawString(yt.Ytname, this.Font, Brushes.Black, new Point(37, 15 * (dy1 + 0) + 15));
                    }

                    TimeSpan t = new TimeSpan();
                    TimeSpan t1 = new TimeSpan();
                    if (yt.Bz == 1)
                    {
                        t = yt.Sysj - otime;
                        t1 = DateTime.Now - otime;
                    }
                    else if (yt.Bz == 2)
                    {
                        t = yt.Sysj - otime;
                        t1 = yt.Jssj - otime;
                    }
                    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                    int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 170);
                    int y1 = 15 * (dy1 + 0) + 18;
                    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                    string drawStr = yt.Yl.ToString() + yt.Dw.ToString();
                    if (drawStr.Contains("．"))
                    {
                        drawStr = drawStr.Replace("．", ".");
                    }
                    e.Graphics.DrawString(drawStr, ht9, Brushes.Blue, new Point(x1, y1 - 8));

                    if (x2 - x1 > 5 && yt.Bz == 1 && yt.Cxyy == true)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1));
                        e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1 + 10));
                    }
                    else if (x2 - x1 > 5 && yt.Bz == 2 && yt.Cxyy == true)
                    {
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 5, y1 + 8), new Point(x2 + 5, y1 + 8) });
                    }
                    if (x2 - x1 > 5 && yt.Cxyy == true)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x1, y1 + 5), new Point(x2, y1 + 5));
                    }
                    //dy1++;
                    //sss.Add(yt.Ytname);

                }

            }
            #endregion

            #region //画局麻药

            int dy2 = 0;// 控制画局麻药的Y坐标
            ArrayList sssJMY = new ArrayList();
            foreach (adims_MODEL.jtytsx jt in jmyList)//画局麻药
            {
                if (jt.Bz > 0)
                {
                    if (sssJMY.Contains(jt.Name))
                    {
                        dy2 = sssJMY.IndexOf(jt.Name);
                    }
                    else
                    {
                        dy2 = sssJMY.Count;
                        e.Graphics.DrawString(jt.Name, this.Font, Brushes.Black, new Point(37, 15 * (dy2 + 14) + 15));
                        sssJMY.Add(jt.Name);
                    }
                    TimeSpan t = new TimeSpan();
                    TimeSpan t1 = new TimeSpan();
                    if (jt.Bz == 1)
                    {
                        t = jt.Kssj - otime;
                        t1 = DateTime.Now - otime;
                    }
                    else if (jt.Bz == 2)
                    {
                        t = jt.Kssj - otime;
                        t1 = jt.Jssj - otime;
                    }
                    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                    int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 170);
                    int y1 = 15 * (dy2 + 14) + 18;
                    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                    string drawStr = jt.Jl.ToString() + jt.Dw.ToString();
                    if (drawStr.Contains("．"))
                    {
                        drawStr = drawStr.Replace("．", ".");
                    }
                    e.Graphics.DrawString(drawStr, ht9, Brushes.Blue, new Point(x1, y1 - 8));
                    //e.Graphics.DrawString(jt.Jl.ToString() + jt.Dw.ToString(), this.Font, Brushes.Black, new Point(960, y1-2));

                    //if (x2 - x1 > 5 && jt.Bz == 1 && jt.Cxyy == true)
                    //{
                    //    e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1));
                    //    e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1 + 10));
                    //}
                    //else if (x2 - x1 > 5 && jt.Bz == 2 && jt.Cxyy == true)
                    //{
                    //    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 5, y1 + 8), new Point(x2 + 5, y1 + 8) });
                    //}
                    //if (x2 - x1 > 5 && jt.Cxyy == true)
                    //{
                    //    e.Graphics.DrawLine(pred2, new Point(x1, y1 + 5), new Point(x2, y1 + 5));
                    //}
                    //dy2++;
                    //sssJMY.Add(jt.Name);
                }

            }
            #endregion


        }
        # region pictureBox2
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (!IsInNet())
                return;
            int dy = 0;
            int dy1 = 3;
            int dy2 = 14;
            ArrayList sssQT = new ArrayList();
            foreach (adims_MODEL.mzqt mz in mzqtList)
            {
                if (mz.Bz > 0)
                {
                    if (sssQT.Contains(mz.Qtname))
                    {
                        dy = sssQT.IndexOf(mz.Qtname);
                    }
                    else
                    {
                        dy = sssQT.Count;
                        sssQT.Add(mz.Qtname);
                    }
                    TimeSpan t = new TimeSpan();
                    t = mz.Sysj - otime;
                    TimeSpan t2 = new TimeSpan();
                    t2 = mz.Jssj - otime;
                    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                    int y = 15 * (dy + 0) + 18;
                    int x2 = (int)((t2.Days * 24 * 60 + t2.Hours * 60 + t2.Minutes) * 15 / jcsjjg + 170);
                    int y2 = 15 * (dy + 0) + 18;
                    if (!IsInNet())
                        break;
                    if (p2x > x - 5 && p2x < x + 5 && p2y > y - 1 && p2y < y + 8)
                    { p2f = 1; p2lx = 1; t_mzqt = mz; }
                    if (p2x > x2 - 5 && p2x < x2 + 5 && p2y > y2 - 1 && p2y < y2 + 8)
                    { p2f = 1; p2lx = 11; t_mzqt2 = mz; }
                    //dy++;
                    //sssQT.Add(mz.Qtname);
                }

            }

            ArrayList sssYDY = new ArrayList();
            foreach (adims_MODEL.mzyt yt in ydyList)
            {
                if (yt.Bz > 0)
                {
                    if (dy1 > 0 && sssYDY.Contains(yt.Ytname))
                    {
                        dy1 = sssYDY.IndexOf(yt.Ytname) + 3;
                    }

                    else
                    {
                        dy1 = sssYDY.Count + 3;
                        sssYDY.Add(yt.Ytname);
                    }
                    TimeSpan t = new TimeSpan();
                    t = yt.Sysj - otime;
                    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                    int y = 15 * (dy1 + 0) + 18;
                    TimeSpan t2 = new TimeSpan();
                    t2 = yt.Jssj - otime;
                    int x2 = (int)((t2.Days * 24 * 60 + t2.Hours * 60 + t2.Minutes) * 15 / jcsjjg + 170);
                    int y2 = 15 * (dy1 + 0) + 18;
                    // MessageBox.Show(x.ToString() + "    " + y.ToString() + "   " + p2x.ToString() + "    " + p2y.ToString());
                    if (!IsInNet())
                        break;
                    if (p2x > x - 5 && p2x < x + 5 && p2y > y - 1 && p2y < y + 8)
                    { p2f = 1; p2lx = 2; t_mzyt = yt; }
                    if (p2x > x2 - 5 && p2x < x2 + 5 && p2y > y2 - 1 && p2y < y2 + 8)
                    { p2f = 1; p2lx = 22; t_mzyt = yt; }

                }
            }
            ArrayList sssJMY = new ArrayList();
            foreach (adims_MODEL.jtytsx jt in jmyList)
            {
                if (jt.Bz > 0)
                {
                    if (sssJMY.Contains(jt.Name))
                    {
                        dy2 = sssJMY.IndexOf(jt.Name) + 14;
                    }

                    else
                    {
                        dy2 = sssJMY.Count + 14;
                        sssJMY.Add(jt.Name);
                    }
                    TimeSpan t = new TimeSpan();
                    t = jt.Kssj - otime;
                    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                    int y = 15 * (dy2 + 0) + 18;
                    if (!IsInNet())
                        break;
                    if (p2x > x - 5 && p2x < x + 5 && p2y > y - 1 && p2y < y + 8)
                    { p2f = 1; p2lx = 3; t_jtytsx = jt; }

                }
            }
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            Point pttag = new Point(MousePosition.X, MousePosition.Y);
            Graphics g = this.CreateGraphics();
            p2x = e.X; p2y = e.Y;
            if (MousePosition.Y > e.Y)
            {
                g.Clear(this.BackColor);
            }
            if (p2f == 1)
            {
                if (p2lx == 1)
                {
                    int t = e.X;
                    t_mzqt.Sysj = otime.AddMinutes((t - 170) / 15 * jcsjjg);

                }
                if (p2lx == 11)
                {
                    int t = e.X;
                    t_mzqt2.Jssj = otime.AddMinutes((t - 170) / 15 * jcsjjg);

                }
                if (p2lx == 2)
                {
                    int t = e.X;
                    t_mzyt.Sysj = otime.AddMinutes((t - 170) / 15 * jcsjjg);

                }
                if (p2lx == 22)
                {
                    int t = e.X;
                    t_mzyt.Jssj = otime.AddMinutes((t - 170) / 15 * jcsjjg);

                }
                if (p2lx == 3)
                {
                    int t = e.X;
                    t_jtytsx.Kssj = otime.AddMinutes((t - 170) / 15 * jcsjjg);

                }
                if (p2lx == 4)
                {
                    int t = e.X;
                    t_jtytsx.Kssj = otime.AddMinutes((t - 170) / 15 * jcsjjg);

                }
                if (p2lx == 5)
                {
                    int t = e.X;
                    t_jtytsx.Kssj = otime.AddMinutes((t - 170) / 15 * jcsjjg);

                }
                pictureBox2.Refresh();
            }

        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            if (p2f != 0)
            {
                p2f = 0;
                if (p2lx == 1)
                {
                    bll.xgqtKssj(mzjldID, t_mzqt);
                }
                if (p2lx == 11)
                {
                    bll.xgqtJssj(mzjldID, t_mzqt2);
                }
                if (p2lx == 2)
                {
                    if (!t_mzyt.Cxyy)
                        bll.xgytKssjJssj(mzjldID, t_mzyt);
                    else
                        bll.xgytKssj(mzjldID, t_mzyt);
                }
                if (p2lx == 22)
                {
                    bll.xgytJssj(mzjldID, t_mzyt);
                }
                if (p2lx == 3)
                {
                    bll.xgjt(mzjldID, t_jtytsx);
                }
            }
        }
        # endregion


        private void AddPointTSMenu_Click(object sender, EventArgs e)//右键检测点添加事件
        {
            PointManage slj = new PointManage(mzjldID, 0);
            slj.ShowDialog();
            BindJHDian();
            this.pictureBox3.Refresh();
            this.pictureBox4.Refresh();
        }
        //private void pictureBox3_Paint(object sender, PaintEventArgs e)
        //{
        //    Pen p1 = new Pen(Brushes.Black);
        //    p1.Width = 2;
        //    e.Graphics.DrawLine(p1, new Point(5, 0), new Point(5, 31));
        //    e.Graphics.DrawLine(p1, new Point(950, 0), new Point(950, 31));

        //}

        private void pictureBox3_Paint_1(object sender, PaintEventArgs e)
        {
            int dyd = 0, dyd1 = 0, dyd2 = 0, dyd3 = 0, dyd4 = 0;//标志是否是第一个点
            Pen pxuxian = new Pen(Brushes.Black);
            pxuxian.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Pen pblack2 = new Pen(Brushes.Black);
            pblack2.Width = 2;
            Font ptzt8 = new Font("宋体", 8);
            Font yh8 = new Font("微软雅黑", 8);
            Font ptzt7 = new Font("宋体", 7);

            for (int i = 0; i < 10; i++)      //画格子
            {
                e.Graphics.DrawLine(Pens.Black, new Point(i * 90 + 169, 12), new Point(i * 90 + 169, 347));
                for (int j = 1; j < 6; j++)
                {
                    e.Graphics.DrawLine(pxuxian, new PointF((float)(j * 15 + i * 90 + 169), (float)16), new PointF((float)(j * 15 + i * 90 + 169), (float)347));
                }
            }
            e.Graphics.DrawLine(Pens.Black, new Point(9, 18), new Point(1069, 18));
            e.Graphics.DrawLine(Pens.Black, new Point(9, 347), new Point(1069, 347));
            for (int i = 0; i < 10; i++)
            {
                e.Graphics.DrawLine(pxuxian, new Point(169, i * 30 + 31), new Point(1069, i * 30 + 31));
                e.Graphics.DrawLine(Pens.Black, new Point(169, i * 30 + 46), new Point(1069, i * 30 + 46));
            }
            e.Graphics.DrawLine(pxuxian, new Point(169, 10 * 30 + 31), new Point(1069, 10 * 30 + 31));
            e.Graphics.DrawLine(pblack2, new Point(9, 0), new Point(9, 347));
            e.Graphics.DrawLine(pblack2, new Point(1069, 0), new Point(1069, 347)); //画格子



            foreach (adims_MODEL.point tp in ssyPoint)
            {
                int x, y;
                TimeSpan t = new TimeSpan();
                t = tp.D - otime;
                x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                if (tp.V > 210)
                {
                    y = 348 - 315;
                    e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Red, x, y);
                }
                else
                    y = (int)(348 - (int)(tp.V * 1.5));
                e.Graphics.DrawPolygon(Pens.Red, new Point[3] { new Point(x, y), new Point(x - 5, y - 5), new Point(x + 5, y - 5) });

                if (MousePosition.X == x && MousePosition.Y == y)
                {
                    e.Graphics.DrawString(tp.V.ToString(), yh8, Brushes.Red, x, y);
                }

                if (dyd != 0)
                    e.Graphics.DrawLine(Pens.Red, new Point(x, y), lastpoint);
                lastpoint.X = x;
                lastpoint.Y = y;
                dyd++;
            }

            foreach (adims_MODEL.point tp in szyPoint)
            {
                int x, y;
                TimeSpan t = new TimeSpan();
                t = tp.D - otime;
                x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                if (tp.V > 210)
                {
                    y = 348 - 315;
                    e.Graphics.DrawString(tp.V.ToString(), yh8, Brushes.Red, x, y);
                }
                else
                    y = (int)(348 - (int)(tp.V * 1.5));
                // e.Graphics.DrawRectangle(Pens.Red, (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 3.3 + 170), 100, 30, 30);
                e.Graphics.DrawPolygon(Pens.Red, new Point[3] { new Point(x, y), new Point(x + 5, y + 5), new Point(x - 5, y + 5) });
                if (dyd1 != 0)
                    e.Graphics.DrawLine(Pens.Red, new Point(x, y), lastpoint1);

                lastpoint1.X = x;
                lastpoint1.Y = y;
                dyd1++;
            }
            foreach (adims_MODEL.pointF tp in twPoint)//画体温
            {


                float x, y;
                TimeSpan t = new TimeSpan();
                t = tp.D - otime;
                x = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                if (tp.V > 44)
                {
                    y = 348 - 330;
                    e.Graphics.DrawString(tp.V.ToString(), yh8, Brushes.Red, x, y);
                }
                else
                {
                    decimal value = Convert.ToDecimal(tp.V * 7.5);
                    y = 348 - (float)Math.Round(value, 1);
                }
                e.Graphics.FillRectangle(Brushes.Green, new RectangleF(x - 3, y - 3, 6, 6));
                if (dyd2 != 0)
                    e.Graphics.DrawLine(Pens.Green, new PointF(x, y), lastpoint2);
                lastpoint2.X = (float)x + 3;
                lastpoint2.Y = (float)y;
                dyd2++;
            }

            foreach (adims_MODEL.point tp in mboPoint)//画脉搏
            {
                int x, y;
                TimeSpan t = new TimeSpan();
                t = tp.D - otime;
                x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                if (tp.V > 210)
                {
                    y = 348 - 315;
                    e.Graphics.DrawString(tp.V.ToString(), yh8, Brushes.Red, x, y);
                }
                else
                    y = (int)(348 - (int)(tp.V * 1.5));
                e.Graphics.FillEllipse(Brushes.Blue, new Rectangle(x - 4, y - 4, 8, 8));
                if (dyd3 != 0)
                    e.Graphics.DrawLine(Pens.Blue, new Point(x, y), lastpoint3);
                lastpoint3.X = x + 4;
                lastpoint3.Y = y;
                dyd3++;
            }
            foreach (adims_MODEL.point tp in hxlPoint)//画呼吸
            {
                int x, y;
                TimeSpan t = new TimeSpan();
                t = tp.D - otime;

                x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                if (tp.V > 210)
                {
                    y = 348 - 315;
                    e.Graphics.DrawString(tp.V.ToString(), yh8, Brushes.Red, x, y);
                }
                else
                    y = (int)(348 - (int)(tp.V * 1.5));
                if (jkksTime < tp.D && jkjsTime > tp.D)
                    e.Graphics.DrawString("CR", ptzt7, Brushes.DarkCyan, x - 5, y - 5);
                //if (CGUAN && cgTime < tp.D&&!mzjs)
                //    e.Graphics.DrawString("CR", ptzt7, Brushes.DarkCyan, x - 5, y - 5);
                //else if (CGUAN && cgTime < tp.D && mzjs&&mzjsTime>tp.D)
                //    e.Graphics.DrawString("CR", ptzt7, Brushes.DarkCyan, x - 5, y - 5);
                else
                    e.Graphics.DrawEllipse(Pens.DarkCyan, new Rectangle(x - 3, y - 3, 6, 6));
                if (dyd4 != 0)
                    e.Graphics.DrawLine(Pens.DarkCyan, new Point(x, y), lastpoint4);
                lastpoint4.X = x + 3;
                lastpoint4.Y = y;
                dyd4++;
            }
        }
        Label lab1 = new Label();
        Label lab2 = new Label();
        int lx, valueAfter, valueBefore;//生理记录检测点类型，修改前值，修改后值
        float valueAfterF, valueBeforeF;
        DateTime xgdtime = new DateTime();//修改点时间
        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            p3x = e.X; p3y = e.Y;
            if (p3f == 1)
            {
                lab1.Visible = true;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.OrangeRed;
                lab1.AutoSize = true;
                lab1.BringToFront();
                lab1.Font = new Font("微软雅黑", 8);
                pictureBox3.Controls.Add(lab1);
                if (lx == 5)//体温移动
                {
                    p3tF.V = (float)decimal.Round(Convert.ToDecimal((348 - p3y) / 7.5), 1);
                    lab1.Text = p3tF.V.ToString();
                    lab1.Location = new Point(p3x, p3y);
                    valueAfterF = p3tF.V; //得到修改后的值
                    xgdtime = Convert.ToDateTime(p3tF.D);
                }
                else //其他移动
                {
                    p3t.V = (int)((348 - p3y) / 1.5);
                    lab1.Location = new Point(p3x, p3y);
                    //lx = p3t.Lx;
                    valueAfter = p3t.V; //得到修改后的值
                    xgdtime = Convert.ToDateTime(p3t.D);
                }

            }
            pictureBox3.Refresh();
            timer3.Enabled = true;
        }

        /// <summary>
        /// 拖动点出发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            if (p3f == 1)
            {
                if (lx == 5)
                {
                    bll.xgMZJLDpointF(mzjldID, p3tF);
                }
                else
                    bll.xgMZJLDpoint(mzjldID, p3t);
                p3f = 0;
            }

        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            lab1.Font = new Font("微软雅黑", 8);
            //if (e.Button == MouseButtons.Right)
            //{
            //    pictureBox3.ContextMenuStrip = contextMenuStrip1;
            //    contextMenuStrip1.Show(pictureBox3, new Point(e.X, e.Y));
            //}
            if (!IsInNet())
                return;

            foreach (adims_MODEL.point tssy in ssyPoint)
            {
                TimeSpan t = new TimeSpan();
                t = tssy.D - otime;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                int y = 0;
                if (tssy.V > 210)
                    y = (int)(348 - 315);
                else
                    y = (int)(348 - (int)(tssy.V * 1.5));

                if (p3x > x - 5 && p3x < x + 5 && p3y < y + 1 && p3y > y - 5)
                {
                    p3f = 1;
                    p3t = tssy;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.AutoSize = true;
                    pictureBox3.Controls.Add(lab1);
                    lab1.Location = new Point(x, y);
                    lab1.Text = p3t.V.ToString();
                    lx = 1;
                    valueBefore = p3t.V;
                }

            }
            foreach (adims_MODEL.point tssy in szyPoint)
            {
                TimeSpan t = new TimeSpan();
                t = tssy.D - otime;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                int y = 0;
                if (tssy.V > 210)
                    y = (int)(348 - 315);
                else
                    y = (int)(348 - (int)(tssy.V * 1.5));

                if (p3x > x - 5 && p3x < x + 5 && p3y < y + 5 && p3y > y - 1)
                {
                    p3f = 1;
                    p3t = tssy;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.AutoSize = true;
                    pictureBox3.Controls.Add(lab1);
                    lab1.Location = new Point(x, y);
                    lab1.Text = p3t.V.ToString();
                    lx = 2;
                    valueBefore = p3t.V;
                }

            }

            foreach (adims_MODEL.point tssy in mboPoint)
            {
                TimeSpan t = new TimeSpan();
                t = tssy.D - otime;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                int y = 0;
                if (tssy.V > 210)
                    y = (int)(348 - 315);
                else
                    y = (int)(348 - (int)(tssy.V * 1.5));
                if (!IsInNet())
                    break;
                if (p3x > x - 5 && p3x < x + 5 && p3y < y + 3 && p3y > y - 3)
                {
                    p3f = 1;
                    p3t = tssy;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.AutoSize = true;
                    pictureBox3.Controls.Add(lab1);
                    lab1.Location = new Point(x, y);
                    lab1.Text = p3t.V.ToString();
                    lx = 3;
                    valueBefore = p3t.V;
                }
            }

            foreach (adims_MODEL.point tssy in hxlPoint)
            {
                TimeSpan t = new TimeSpan();
                t = tssy.D - otime;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                int y = 0;
                if (tssy.V > 210)
                    y = (int)(348 - 315);
                else
                    y = (int)(348 - (int)(tssy.V * 1.5));

                if (p3x > x - 5 && p3x < x + 5 && p3y < y + 3 && p3y > y - 3)
                {
                    p3f = 1;
                    p3t = tssy;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.AutoSize = true;
                    pictureBox3.Controls.Add(lab1);
                    lab1.Location = new Point(x, y);
                    lab1.Text = p3t.V.ToString();
                    lx = 4;
                    valueBefore = p3t.V;
                }
            }

            foreach (adims_MODEL.pointF tssy in twPoint)//移动体温
            {
                TimeSpan t = new TimeSpan();
                t = tssy.D - otime;
                float x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                float y = 0;
                if (tssy.V > 44)
                    y = 348 - 330;
                else
                    y = (float)(348 - (float)(tssy.V * 7.5));

                if (p3x > x - 5 && p3x < x + 5 && p3y < y + 5 && p3y > y - 5)
                {
                    p3f = 1;
                    p3tF = tssy;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.Font = new Font("微软雅黑", 8);
                    lab1.AutoSize = true;
                    pictureBox3.Controls.Add(lab1);
                    lab1.Location = new Point((int)x, (int)y);
                    lab1.Text = p3tF.V.ToString();

                    lx = 5;
                    valueBeforeF = p3tF.V;
                }
            }
        }
        public bool IsInNet()
        {
            int isinnet;
            bool flags = InternetGetConnectedState(out isinnet, 0);
            return flags;
        }

        private void pictureBox4_Paint(object sender, PaintEventArgs e)
        {
            Pen pxuxian = new Pen(Brushes.Black);
            pxuxian.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Pen pblack2 = new Pen(Brushes.Black);
            pblack2.Width = 2;

            e.Graphics.DrawLine(pblack2, new Point(9, 0), new Point(9, 266));
            e.Graphics.DrawLine(pblack2, new Point(1069, 0), new Point(1069, 250));

            for (int i = 0; i < 7; i++)
                e.Graphics.DrawLine(Pens.Black, new Point(28, i * 15 + 15), new Point(1069, i * 15 + 15));
            for (int i = 0; i < 5; i++)
                e.Graphics.DrawLine(Pens.Black, new Point(9, i * 15 + 120), new Point(1069, i * 15 + 120));

            e.Graphics.DrawLine(Pens.Black, new Point(169, 0), new Point(169, 210));
            for (int i = 0; i < 10; i++)
            {
                e.Graphics.DrawLine(pxuxian, new Point(i * 90 + 218, 0), new Point(i * 90 + 218, 180));
            }
            for (int i = 0; i < 10; i++)
                e.Graphics.DrawLine(Pens.Black, new Point(i * 90 + 169, 0), new Point(i * 90 + 169, 180));

            e.Graphics.DrawLine(Pens.Black, new Point(28, 0), new Point(28, 120));   //画格子

            //int jhCount = 0; 
            Font sti8 = new Font("宋体", 8);
            Font fsmall = new Font("宋体", 7);

            int jhy = 0;
            foreach (string s in jhxmy)//画监护项目
            {
                e.Graphics.DrawString(s, this.Font, Brushes.Black, new Point(30, jhy * 15 + 17));
                foreach (adims_MODEL.jhxm jt in jhxmValueList)
                {
                    if (jt.Sy == s)
                    {
                        TimeSpan t = new TimeSpan();
                        t = jt.D - otime;
                        TimeSpan t1 = new TimeSpan();
                        t1 = cgTime - otime;
                        int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 163);
                        int y = jhy * 15 + 17;
                        int x1 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 163);
                        if (jt.Sy == "ETCO2")
                        {
                            if (CGUAN && cgTime < jt.D && !BGUAN)
                            {
                                e.Graphics.FillRectangle(Brushes.Pink, x, y, 14, 12);
                                e.Graphics.DrawString(jt.V.ToString(), (jt.V / 100 > 0 ? fsmall : this.Font), Brushes.Black,
                                    new Point((jt.V / 100 > 0 ? (x - 2) : x), y));

                            }
                            if (BGUAN && cgTime < jt.D && jt.D < bgTime)
                            {
                                e.Graphics.FillRectangle(Brushes.Pink, x, y, 14, 12);
                                e.Graphics.DrawString(jt.V.ToString(), (jt.V / 100 > 0 ? fsmall : this.Font), Brushes.Black,
                                    new Point((jt.V / 100 > 0 ? (x - 2) : x), y));
                            }
                        }
                        else if (jt.Sy != "ETCO2")
                        {
                            e.Graphics.FillRectangle(Brushes.Pink, x, y, 14, 12);
                            e.Graphics.DrawString(jt.V.ToString(), (jt.V / 100 > 0 ? fsmall : this.Font), Brushes.Black,
                                new Point((jt.V / 100 > 0 ? (x - 2) : x), y));
                        }
                    }
                }
                jhy++;
            }

            Pen pred2 = new Pen(Brushes.Red);
            pred2.Width = 2;
            int dy = 0;
            listBox1.Items.Clear();
            foreach (adims_MODEL.shuye mz in shuyeList)  ////画输液
            {
                if (shuyeList.Count > 0)
                {
                    listBox1.Items.Add(mz.Name + " " + mz.Jl + mz.Dw);
                    if (mz.Bz > 0)
                    {
                        //e.Graphics.DrawString(mz.Name, this.Font, Brushes.Black, new Point(37, 15 * (dy + 0) + 2));
                        TimeSpan t = new TimeSpan();
                        TimeSpan t1 = new TimeSpan();
                        if (mz.Bz == 1)
                        {
                            t = mz.Kssj - otime;
                            t1 = DateTime.Now - otime;
                        }
                        else if (mz.Bz == 2)
                        {
                            t = mz.Kssj - otime;
                            t1 = mz.Jssj - otime;
                        }
                        int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                        int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 170);
                        int y1 = 0;
                        if (dy % 2 == 0)
                        {
                            y1 = 15 * (7) + 5;
                        }
                        else
                        {
                            y1 = 15 * (8) + 5;
                        }

                        e.Graphics.DrawString(mz.Name + " " + mz.Jl.ToString() + mz.Dw.ToString(), sti8, Brushes.Blue, new Point(x1 - 10, y1 - 8));
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                        //if (x2 - x1 > 5 && mz.Bz == 1)
                        //{
                        //    e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1));
                        //    e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1 + 10));
                        //}
                        //else if (x2 - x1 > 5 && mz.Bz == 2)
                        //{
                        //    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 5, y1 + 8), new Point(x2 + 5, y1 + 8) });

                        //}
                        //if (x2 - x1 > 5)
                        //{
                        //    e.Graphics.DrawLine(pred2, new Point(x1, y1 + 5), new Point(x2, y1 + 5));
                        //}
                        dy++;

                    }
                }
            }
            int dy1 = 0;

            foreach (adims_MODEL.shuxue mz in shuxueList)  ////画输血
            {
                if (shuxueList.Count > 0)
                {
                    listBox1.Items.Add(mz.Name + " " + mz.Jl + mz.Dw);
                    if (mz.Bz > 0)
                    {
                        //e.Graphics.DrawString(mz.Name, this.Font, Brushes.Black, new Point(37, 15 * (dy1 + 0) + 2));
                        TimeSpan t = new TimeSpan();
                        TimeSpan t1 = new TimeSpan();
                        if (mz.Bz == 1)
                        {
                            t = mz.Kssj - otime;
                            t1 = DateTime.Now - otime;
                        }
                        else if (mz.Bz == 2)
                        {
                            t = mz.Kssj - otime;
                            t1 = mz.Jssj - otime;
                        }
                        int y1 = 0;
                        if (dy1 % 2 == 0)
                        {
                            y1 = 15 * (9) + 5;
                        }
                        else
                        {
                            y1 = 15 * (10) + 5;
                        }
                        int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                        int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 170);

                        e.Graphics.DrawString(mz.Name + " " + mz.Jl.ToString() + mz.Dw.ToString(), sti8, Brushes.Blue, new Point(x1 - 10, y1 - 8));
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                        //if (x2 - x1 > 5 && mz.Bz == 1)
                        //{
                        //    e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1));
                        //    e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1 + 10));
                        //}
                        //else if (x2 - x1 > 5 && mz.Bz == 2)
                        //{
                        //    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 5, y1 + 8), new Point(x2 + 5, y1 + 8) });

                        //}
                        //if (x2 - x1 > 5)
                        //{
                        //    e.Graphics.DrawLine(pred2, new Point(x1, y1 + 5), new Point(x2, y1 + 5));
                        // }
                        dy1++;
                    }
                }
            }

            int szsji = 1;

            foreach (adims_MODEL.szsj s in szsj)//画术中事件
            {
                TimeSpan t = new TimeSpan();
                t = s.D - otime;
                int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 165);
                float y1 = (float)(12 * 15 + 5);
                e.Graphics.FillRectangle(Brushes.Pink, x1, y1, 10, 10);
                string zm = "";
                zm = SwitchNumToEnglish(szsji);//转换字母

                e.Graphics.DrawString(zm, this.Font, Brushes.Black, new PointF(x1, y1 - 3));
                szsji++;
            }
            int tsyyi = 1;
            foreach (adims_MODEL.tsyy s in tsyy)//画特殊用药
            {
                TimeSpan t = new TimeSpan();
                t = s.D - otime;
                float x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 165);
                float y1 = (float)(12 * 15 + 10);
                //float y1 = (float)(12 * 15 + 5);
                e.Graphics.FillEllipse(Brushes.LightGreen, x1, y1, tsyyi > 9 ? 14 : 10, 10);
                e.Graphics.DrawString(tsyyi.ToString(), this.Font, Brushes.Black, new PointF(tsyyi > 9 ? x1 - 2 : x1, y1));
                tsyyi++;
            }
            int clCOUNT = 1;
            foreach (adims_MODEL.clcxqt cl in clcxqt)//画出尿
            {
                TimeSpan t = new TimeSpan();
                t = cl.D - otime;
                if (cl.Lx == 1)
                {
                    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 163);
                    e.Graphics.FillRectangle(Brushes.Pink, x1, 2, 20, 12);
                    e.Graphics.DrawString(cl.V.ToString(), this.Font, Brushes.Black, new Point(x1, 2));
                }
                clCOUNT++;
            }
            int mzpmCount = 1;
            foreach (adims_MODEL.mzpingmian pingmian in mzpmList)//画麻醉平面
            {
                TimeSpan t = new TimeSpan();
                t = pingmian.D - otime;
                int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 163);
                e.Graphics.FillRectangle(Brushes.Pink, x1, 2 + 11 * 15, 42, 12);
                e.Graphics.DrawString(pingmian.mzpmName.ToString(), this.Font, Brushes.Black, new Point(x1, 2 + 11 * 15));
                mzpmCount++;
            }

        }

        private void pictureBox4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.X > 170 && e.X < 950 && e.Y > 2 && e.Y < 13 * 15)
            {
                int j = 0;
                foreach (adims_MODEL.clcxqt cn in clcxqt)
                {
                    TimeSpan t = new TimeSpan();
                    t = cn.D - otime;
                    int x1 = (t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 163;
                    if (e.X > x1 && e.X < x1 + 18 && e.Y > 2 && e.Y < 15)
                    {
                        XiugaiChuLiang formxgjhsj = new XiugaiChuLiang(mzjldID, cn, 1);
                        formxgjhsj.ShowDialog();
                        //BindCLlist();
                        int cxl = 0, cnl = 0;
                        foreach (adims_MODEL.clcxqt cl in clcxqt)
                        {
                            if (cl.Lx == 1)
                                cnl = cnl + cl.V;
                            else if (cl.Lx == 2)
                                cxl = cxl + cl.V;
                        }
                        //ucChuxue.Controls[0].Text = cxl.ToString();
                        ucNiaoLiang.Controls[0].Text = cnl.ToString();
                        j++;
                    }
                }
                int jhxmCount = 0;
                foreach (string s in jhxmy)
                {
                    foreach (adims_MODEL.jhxm jhxmz in jhxmValueList)
                    {
                        if (jhxmz.Sy == s)
                        {
                            TimeSpan t = new TimeSpan();
                            t = jhxmz.D - otime;
                            int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 163);
                            int y = jhxmCount * 15 + 15;
                            if (e.X > x && e.X < x + 13 && e.Y > y + 1 && e.Y < y + 13)
                            {
                                xgjhsj formxgjhsj = new xgjhsj(mzjldID, jhxmz, 0);
                                formxgjhsj.ShowDialog();
                            }
                        }
                    }
                    jhxmCount++;
                }

                if (j == 0)
                {
                    if (e.Y < 16)
                    {
                        addcl formaddcl = new addcl(clcxqt, otime.AddMinutes((e.X - 163) / 15 * jcsjjg), 1, mzjldID);
                        formaddcl.ShowDialog();
                        int cxl = 0, cnl = 0;
                        foreach (adims_MODEL.clcxqt cl in clcxqt)
                        {
                            if (cl.Lx == 1)
                                cnl = cnl + cl.V;
                            else if (cl.Lx == 2)
                                cxl = cxl + cl.V;
                        }
                        ucNiaoLiang.Controls[0].Text = cnl.ToString();
                    }

                    else if (e.Y > (11 * 15 + 2) && e.Y < (12 * 15 + 2))
                    {
                        addMZPM formaddcl = new addMZPM(mzpmList, otime.AddMinutes((e.X - 162) / 15 * jcsjjg), mzjldID);
                        formaddcl.ShowDialog();

                    }
                    else if (e.Y > 106 && e.Y < 135)
                    {
                        addshuye fromjhxm = new addshuye(shuyeList, otime.AddMinutes((e.X - 170) / 15 * jcsjjg), mzjldID);
                        fromjhxm.ShowDialog();
                        BindShuyeList();
                        ucZongLiang.Controls[0].Text = (sxZongl + syZongl).ToString();

                    }
                    else if (e.Y > 136 && e.Y < 165)
                    {
                        addshuxue fromjhxm = new addshuxue(shuxueList, otime.AddMinutes((e.X - 170) / 15 * jcsjjg), mzjldID);
                        fromjhxm.ShowDialog();
                        BindShuxueList();
                        ucZongLiang.Controls[0].Text = (sxZongl + syZongl).ToString();
                    }
                }
            }
            pictureBox4.Refresh();
            listBox1.Focus();
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            if (!IsInNet())
                return;
            int ii = 0;
            int dy = 7;
            int dy1 = 9;
            foreach (adims_MODEL.shuye mz in shuyeList)
            {
                if (mz.Bz > 0)
                {
                    TimeSpan t = new TimeSpan();
                    t = mz.Kssj - otime;
                    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                    int y = 0;
                    if (dy % 2 == 1)
                        y = 15 * (7 + 0) + 5;
                    else
                        y = 15 * (8 + 0) + 5;
                    if (!IsInNet())
                        break;
                    if (p4x > x - 5 && p4x < x + 5 && p4y > y - 1 && p4y < y + 8)
                    { p4f = 1; p4lx = 4; t_shuye = mz; }
                    dy++;
                }
            }

            foreach (adims_MODEL.shuxue yt in shuxueList)
            {
                if (yt.Bz > 0)
                {
                    TimeSpan t = new TimeSpan();
                    t = yt.Kssj - otime;
                    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                    int y = 0;
                    if (dy1 % 2 == 1)
                        y = 15 * (9 + 0) + 5;
                    else
                        y = 15 * (10 + 0) + 5;
                    if (!IsInNet())
                        break;
                    if (p4x > x - 5 && p4x < x + 5 && p4y > y - 1 && p4y < y + 8)
                    { p4f = 1; p4lx = 5; t_shuxue = yt; }
                    dy1++;
                }
            }


            foreach (adims_MODEL.clcxqt q in clcxqt)
            {
                TimeSpan t = new TimeSpan();
                t = q.D - otime;
                int i = q.Lx == 1 ? 0 : (q.Lx == 2 ? 9 : 10);
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 165);
                int y = i * 15 + 2;
                if (!IsInNet())
                    break;
                if (e.X > x - 8 && e.X < x + 8 && e.Y < y + 12 && e.Y > y)
                { p4f = 1; p4lx = 1; p4t1 = q; ii = 1; }

            }

            if (ii != 1)
            {
                foreach (adims_MODEL.szsj sj in szsj)
                {

                    TimeSpan t = new TimeSpan();
                    t = sj.D - otime;
                    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                    int y = 12 * 15 + 5;
                    if (!IsInNet())
                        break;
                    if (e.X > x - 8 && e.X < x + 8 && e.Y < y + 12 && e.Y > y)
                    { p4f = 1; p4lx = 3; p4t3 = sj; ii = 1; }
                }
                foreach (adims_MODEL.tsyy ts in tsyy)
                {
                    TimeSpan t = new TimeSpan();
                    t = ts.D - otime;
                    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                    int y = 12 * 15 + 5;
                    if (!IsInNet())
                        break;
                    if (e.X > x - 8 && e.X < x + 8 && e.Y < y + 12 && e.Y > y)
                    { p4f = 1; p4lx = 2; p4t2 = ts; ii = 1; }

                }
                foreach (adims_MODEL.mzpingmian sj in mzpmList)
                {
                    TimeSpan t = new TimeSpan();
                    t = sj.D - otime;
                    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 163);
                    int y = 11 * 15 + 5;
                    if (!IsInNet())
                        break;
                    if (e.X > x - 8 && e.X < x + 35 && e.Y < y + 12 && e.Y > y)
                    { p4f = 1; p4lx = 6; t_mzpm = sj; ii = 1; }
                }
            }
        }

        private void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            p4x = e.X; p4y = e.Y;
            if (p4f == 1)
            {
                if (p4lx == 4)//输液移动
                {
                    int t = e.X;
                    t_shuye.Kssj = otime.AddMinutes((t - 170) / 15 * jcsjjg);
                    lab2.Visible = true;
                    lab2.BackColor = Color.Transparent;
                    lab2.ForeColor = Color.Blue;
                    lab2.AutoSize = true;
                    lab2.Text = t_shuye.Kssj.ToString("HH:mm");
                    pictureBox4.Controls.Add(lab2);
                    lab2.Location = new Point(p4x, p4y);
                    lab2.BringToFront();

                }
                else if (p4lx == 5)//输血移动
                {
                    int t = e.X;
                    t_shuxue.Kssj = otime.AddMinutes((t - 170) / 15 * jcsjjg);
                    lab2.Visible = true;
                    lab2.BackColor = Color.Transparent;
                    lab2.ForeColor = Color.Blue;
                    lab2.AutoSize = true;
                    lab2.Text = t_shuxue.Kssj.ToString("HH:mm");
                    pictureBox4.Controls.Add(lab2);
                    lab2.Location = new Point(p4x, p4y);
                    lab2.BringToFront();

                }
                else if (p4lx == 1)//出尿移动
                {
                    p4t1.D = otime.AddMinutes((p4x - 165) / 15 * jcsjjg);
                    lab2.Visible = true;
                    lab2.BackColor = Color.Transparent;
                    lab2.ForeColor = Color.Blue;
                    lab2.AutoSize = true;
                    lab2.Text = p4t1.D.ToString("HH:mm");
                    pictureBox4.Controls.Add(lab2);
                    lab2.Location = new Point(p4x, p4y);
                    lab2.BringToFront();

                }
                else if (p4lx == 2)//特殊用药移动
                {
                    p4t2.D = otime.AddMinutes((p4x - 170) / 15 * jcsjjg);
                    lab2.Visible = true;
                    lab2.BackColor = Color.Transparent;
                    lab2.ForeColor = Color.Blue;
                    lab2.AutoSize = true;
                    lab2.Text = p4t2.D.ToString("HH:mm");
                    pictureBox4.Controls.Add(lab2);
                    lab2.Location = new Point(p4x, p4y);
                    lab2.BringToFront();
                }
                else if (p4lx == 3)//术中事件移动
                {
                    p4t3.D = otime.AddMinutes((p4x - 170) / 15 * jcsjjg);
                    lab2.Visible = true;
                    lab2.BackColor = Color.Transparent;
                    lab2.ForeColor = Color.Blue;
                    lab2.AutoSize = true;
                    lab2.Text = p4t3.D.ToString("HH:mm");
                    pictureBox4.Controls.Add(lab2);
                    lab2.Location = new Point(p4x, p4y);
                    lab2.BringToFront();
                }
                else if (p4lx == 6)//麻醉平面移动
                {
                    t_mzpm.D = otime.AddMinutes((p4x - 163) / 15 * jcsjjg);
                    lab2.Visible = true;
                    lab2.BackColor = Color.Transparent;
                    lab2.ForeColor = Color.Blue;
                    lab2.AutoSize = true;
                    lab2.Text = t_mzpm.D.ToString("HH:mm");
                    pictureBox4.Controls.Add(lab2);
                    lab2.Location = new Point(p4x, p4y);
                    lab2.BringToFront();
                }
                timer3.Enabled = true;
                pictureBox4.Refresh();
            }
        }

        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            if (p4f == 1)
            {
                p4f = 0;
                if (p4lx == 1)
                {
                    int i = bll.xgclsj(mzjldID, p4t1);
                    if (i != 1)
                        MessageBox.Show("错误" + i.ToString());
                }
                if (p4lx == 4)
                {
                    int i = bll.xgshuyeKSSJ(mzjldID, t_shuye);
                    if (i != 1)
                        MessageBox.Show("错误" + i.ToString());
                }
                if (p4lx == 5)
                {
                    int i = bll.xgshuxueKSSJ(mzjldID, t_shuxue);
                    if (i != 1)
                        MessageBox.Show("错误" + i.ToString());
                }
                if (p4lx == 6)
                {
                    int i = bll.xgmzpmTime(mzjldID, t_mzpm);
                    if (i != 1)
                        MessageBox.Show("错误" + i.ToString());
                }
                if (p4lx == 2)
                {
                    bll.xgtsyyTime(mzjldID, p4t2);
                    BindTsyy();
                    pictureBox4.Refresh();

                }
                if (p4lx == 3)
                {
                    bll.xgszsjTime(mzjldID, p4t3);
                    BindSZSJ();
                    pictureBox4.Refresh();
                }
            }
        }

        private void pictureBox4_DoubleClick(object sender, EventArgs e)
        {
            if (p4x > 28 && p4x < 169)
            {
                if (p4y > 17 && p4y < 105)
                {
                    addjhxm fromjhxm = new addjhxm(jhxma, jhxmy, mzjldID, 0);
                    fromjhxm.ShowDialog();
                    string ssss = "";
                    foreach (string s in jhxmy)
                    {
                        if (ssss == "")
                            ssss = s;
                        else
                            ssss = ssss + "," + s;
                    }
                }
                pictureBox4.Refresh();
            }
        }

        private void pictureBox5_Paint(object sender, PaintEventArgs e)
        {
            Pen p1 = new Pen(Brushes.Black);
            Pen p2 = new Pen(Brushes.Black);
            p2.Width = 2;
            e.Graphics.DrawLine(p1, new Point(9, 1), new Point(1069, 1));

            e.Graphics.DrawLine(p1, new Point(9, 20), new Point(1069, 20));
            e.Graphics.DrawLine(p1, new Point(9, 170), new Point(1069, 170));
            e.Graphics.DrawLine(p1, new Point(745, 170), new Point(745, 350));
            e.Graphics.DrawLine(p2, new Point(9, 0), new Point(9, 350));
            e.Graphics.DrawLine(p2, new Point(9, 350), new Point(1069, 350));
            e.Graphics.DrawLine(p2, new Point(1069, 0), new Point(1069, 350));


        }

        private void pictureBox6_Paint_1(object sender, PaintEventArgs e)
        {

            Pen pred2 = new Pen(Brushes.Red);
            pred2.Width = 2;
            Pen pblack2 = new Pen(Brushes.Black);
            pblack2.Width = 2;
            Pen pxuxian = new Pen(Brushes.Black);
            Font ptzt8 = new Font("宋体", 9);//填入栏目字体  
            Font ptzt7 = new Font("宋体", 7);//填入栏目字体  
            pxuxian.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            e.Graphics.DrawLine(pblack2, new Point(8, 13), new Point(954, 13));
            //e.Graphics.DrawLine(Pens.Black, new Point(8, 28), new Point(954, 28));
            for (int i = 0; i < 17; i++)
                e.Graphics.DrawLine(Pens.Black, new Point(28, 28 + 15 * i), new Point(954, 28 + 15 * i));
            e.Graphics.DrawLine(Pens.Black, new Point(169, 9), new Point(169, 283));
            e.Graphics.DrawLine(Pens.Black, new Point(9, 13 + 15 * 3), new Point(169, 13 + 15 * 3));
            e.Graphics.DrawLine(Pens.Black, new Point(9, 13 + 15 * 14), new Point(169, 13 + 15 * 14));
            e.Graphics.DrawLine(Pens.Black, new Point(9, 13 + 15 * 17), new Point(169, 13 + 15 * 17));

            e.Graphics.DrawLine(pblack2, new Point(9, 0), new Point(9, 292));
            e.Graphics.DrawLine(pblack2, new Point(954, 0), new Point(954, 292));
            e.Graphics.DrawLine(Pens.Black, new Point(28, 12), new Point(28, 283));

            int dy = 0;// 控制气体药瓶输出的Y坐标         
            ArrayList ssQT = new ArrayList();
            foreach (adims_MODEL.mzqt mz in mzqtList)  //画气体名字
            {
                if (ssQT.Contains(mz.Qtname))
                {
                    dy = ssQT.IndexOf(mz.Qtname);
                }
                else
                {
                    dy = ssQT.Count;
                    ssQT.Add(mz.Qtname);
                }
                e.Graphics.DrawString(mz.Qtname, ptzt8, Brushes.Black, new Point(35, 15 * (dy + 0) + 17));


            }
            int dy1 = 3;// 控制吸入诱导药的Y坐标    
            ArrayList sss = new ArrayList();
            foreach (adims_MODEL.mzyt yt in ydyList)//画诱导药名字
            {
                if (sss.Contains(yt.Ytname))
                {
                    dy1 = sss.IndexOf(yt.Ytname) + 3;
                }
                else
                {
                    dy1 = sss.Count + 3;
                    sss.Add(yt.Ytname);
                    e.Graphics.DrawString(yt.Ytname, ptzt8, Brushes.Black, new Point(35, 15 * (dy1 + 0) + 17));
                }
            }


            int dy2 = 14;// 控制局部麻醉药输出的Y坐标  
            ArrayList sssJMY = new ArrayList();
            foreach (adims_MODEL.jtytsx jt in jmyList)//画局部麻醉药
            {
                if (sssJMY.Contains(jt.Name))
                {
                    dy2 = sssJMY.IndexOf(jt.Name) + 14;
                }
                else
                {
                    dy2 = sssJMY.Count + 14;
                    sssJMY.Add(jt.Name);
                    e.Graphics.DrawString(jt.Name, ptzt8, Brushes.Black, new Point(35, 15 * (dy2 + 0) + 17));
                }
            }

        }

        private void pictureBox6_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.X > 28 && e.X < 168)
            {
                if (e.Y > 15 && e.Y < 60)
                {
                    AddQt qt = new AddQt(mzqtList, mzjldID);
                    qt.ShowDialog();
                    BindQtList();
                }
                else if (e.Y > 60 && e.Y < 225)
                {
                    addYDyao mzytform = new addYDyao(mzjldID, ydyList);
                    mzytform.ShowDialog();
                    BindYdyList();
                }
                else if (e.Y > 225 && e.Y < 270)
                {
                    addJuMaYao szyy = new addJuMaYao(jmyList, mzjldID);
                    szyy.ShowDialog();
                    BindJmyList();
                }
                pictureBox2.Refresh();
                LB_SHIJIAN.Focus();
            }


            pictureBox6.Refresh();

        }

        private void pictureBox6_Paint(object sender, PaintEventArgs e)
        {
            int dy = 0;// 控制输出的Y坐标
            qtsl = 0; //气体数量
            for (int i = 0; i < 10; i++)
                if (mzqtList[i].Bz > 0)
                    qtsl++;
            Pen pred2 = new Pen(Brushes.Red);
            pred2.Width = 2;
            Pen pblack2 = new Pen(Brushes.Black);
            pblack2.Width = 2;
            Pen pxuxian = new Pen(Brushes.Black);
            pxuxian.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            e.Graphics.DrawLine(pblack2, new Point(8, 13), new Point(954, 13));
            for (int i = 0; i < 14; i++)
                e.Graphics.DrawLine(Pens.Black, new Point(28, 28 + 15 * i), new Point(954, 28 + 15 * i));
            e.Graphics.DrawLine(Pens.Black, new Point(169, 9), new Point(169, 283));
            for (int i = 0; i < 7; i++)
                e.Graphics.DrawLine(pxuxian, new Point(i * 90 + 267, 12), new Point(i * 90 + 267, 283));
            e.Graphics.DrawLine(Pens.Black, new Point(9, 28), new Point(28, 28));
            e.Graphics.DrawLine(Pens.Black, new Point(9, 178), new Point(28, 178));
            e.Graphics.DrawLine(Pens.Black, new Point(9, 208), new Point(28, 208));
            e.Graphics.DrawLine(Pens.Black, new Point(9, 238), new Point(28, 238));
            e.Graphics.DrawLine(Pens.Black, new Point(9, 283), new Point(28, 283));
            e.Graphics.DrawLine(pblack2, new Point(9, 0), new Point(9, 292));
            e.Graphics.DrawLine(pblack2, new Point(954, 0), new Point(954, 292));
            e.Graphics.DrawLine(Pens.Black, new Point(28, 12), new Point(28, 283));

            Pen pblue2 = new Pen(Brushes.Blue);
            pblue2.Width = 2;   //画蓝色的横线
            if (qtsl == 0)
                e.Graphics.DrawLine(pblue2, new Point(28, 45 - 1), new Point(953, 45 - 1));
            else
                e.Graphics.DrawLine(pblue2, new Point(28, 15 * qtsl + 29), new Point(953, 15 * qtsl + 29));
            for (int i = 0; i < 10; i++) //画气体
            {
                if (mzqtList[i].Bz > 0)
                {
                    e.Graphics.DrawString(mzqtList[i].Qtname, this.Font, Brushes.Black, new Point(37, 15 * (dy + 1) + 15));
                    TimeSpan t = new TimeSpan();
                    TimeSpan t1 = new TimeSpan();
                    if (mzqtList[i].Bz == 1)
                    {
                        t = mzqtList[i].Sysj - otime;
                        t1 = DateTime.Now - otime;
                    }
                    else if (mzqtList[i].Bz == 2)
                    {
                        t = mzqtList[i].Sysj - otime;
                        t1 = mzqtList[i].Jssj - otime;
                    }
                    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 3.3 + 170);
                    int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 3.3 + 170);
                    int y1 = 15 * (dy + 1) + 18;
                    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                    if (x2 - x1 > 5 && mzqtList[i].Bz == 1)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x2, y1), new Point(x2 - 5, y1 - 5));
                        e.Graphics.DrawLine(pred2, new Point(x2, y1), new Point(x2 - 5, y1 + 5));
                    }
                    else if (x2 - x1 > 5 && mzqtList[i].Bz == 2)
                    {
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 5, y1 + 8), new Point(x2 + 5, y1 + 8) });
                    }
                    e.Graphics.DrawLine(pred2, new Point(x1, y1), new Point(x2, y1));
                    dy++;
                }
            }
            for (int i = 0; i < 10; i++)//画诱导药
            {
                if (dy == 0)
                    dy++;
                if (ydyList[i].Bz > 0)
                {
                    e.Graphics.DrawString(ydyList[i].Ytname, this.Font, Brushes.Black, new Point(37, 15 * (dy + 1) + 15));
                    TimeSpan t = new TimeSpan();
                    TimeSpan t1 = new TimeSpan();
                    if (ydyList[i].Bz == 1)
                    {
                        t = ydyList[i].Sysj - otime;
                        t1 = DateTime.Now - otime;
                    }
                    else if (ydyList[i].Bz == 2)
                    {
                        t = ydyList[i].Sysj - otime;
                        t1 = ydyList[i].Jssj - otime;
                    }
                    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 3.3 + 170);
                    int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 3.3 + 170);
                    int y1 = 15 * (dy + 1) + 18;
                    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                    if (x2 - x1 > 5 && ydyList[i].Bz == 1)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x2, y1), new Point(x2 - 5, y1 - 5));
                        e.Graphics.DrawLine(pred2, new Point(x2, y1), new Point(x2 - 5, y1 + 5));
                    }
                    else if (x2 - x1 > 5 && ydyList[i].Bz == 2)
                    {
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 5, y1 + 8), new Point(x2 + 5, y1 + 8) });
                    }
                    e.Graphics.DrawLine(pred2, new Point(x1, y1), new Point(x2, y1));
                    dy++;
                }
            }
            int jty = 10;
            for (int i = 0; i < 2; i++) //画局部麻醉药
            {
                if (jmyList[i].Bz > 0)
                {
                    e.Graphics.DrawString(jmyList[i].Name, this.Font, Brushes.Black, new Point(37, 15 * (jty + 1) + 15));
                    TimeSpan t = new TimeSpan();
                    TimeSpan t1 = new TimeSpan();
                    if (jmyList[i].Bz == 1)
                    {
                        t = jmyList[i].Kssj - otime;
                        t1 = DateTime.Now - otime;
                    }
                    else if (jmyList[i].Bz == 2)
                    {
                        t = jmyList[i].Kssj - otime;
                        t1 = jmyList[i].Jssj - otime;
                    }
                    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 3.3 + 170);
                    int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 3.3 + 170);
                    int y1 = 15 * (jty + 1) + 18;
                    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                    if (x2 - x1 > 5 && jmyList[i].Bz == 1)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x2, y1), new Point(x2 - 5, y1 - 5));
                        e.Graphics.DrawLine(pred2, new Point(x2, y1), new Point(x2 - 5, y1 + 5));
                    }
                    else if (x2 - x1 > 5 && jmyList[i].Bz == 2)
                    {
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 5, y1 + 8), new Point(x2 + 5, y1 + 8) });
                    }
                    e.Graphics.DrawLine(pred2, new Point(x1, y1), new Point(x2, y1));
                    jty++;
                }
            }


        }

        private void pictureBox6_DoubleClick(object sender, EventArgs e)
        {
            if (p2x > 28 && p2x < 168 && p2y > 28 && p2y < 178)
            {
                if (qtsl == 0)
                {
                    if (p2y < 43)
                    {
                        AddQt qt = new AddQt(mzqtList, mzjldID);
                        qt.ShowDialog();
                    }
                    else
                    {
                        addYDyao mzytform = new addYDyao(mzjldID, ydyList);
                        mzytform.ShowDialog();
                    }

                }
                else
                {
                    if (p2y < (qtsl * 15 + 28))
                    {
                        AddQt qt = new AddQt(mzqtList, mzjldID);
                        qt.ShowDialog();
                    }
                    else
                    {
                        addYDyao mzytform = new addYDyao(mzjldID, ydyList);
                        mzytform.ShowDialog();
                    }
                }
                pictureBox2.Refresh();
            }
            if (p2x > 28 && p2x < 168 && p2y > 178 && p2y < 208)
            {
                addJuMaYao szyy = new addJuMaYao(jmyList, mzjldID);
                szyy.ShowDialog();
                pictureBox2.Refresh();

            }
            else if (p2x > 28 && p2x < 168 && p2y > 208 && p2y < 238)
            {
                addJuMaYao szyy = new addJuMaYao(jiaot1, mzjldID);
                szyy.ShowDialog();
                pictureBox2.Refresh();
            }

            else if (p2x > 28 && p2x < 168 && p2y > 238 && p2y < 283)
            {
                addJuMaYao szyy = new addJuMaYao(sx1, mzjldID);
                szyy.ShowDialog();
                pictureBox2.Refresh();
            }
        }

        private void pictureBox8_Paint(object sender, PaintEventArgs e)
        {
            //Pen pxuxian = new Pen(Brushes.Black);
            //pxuxian.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Pen pblack2 = new Pen(Brushes.Black);
            pblack2.Width = 2;

            e.Graphics.DrawLine(pblack2, new Point(9, 0), new Point(9, 210));

            for (int i = 0; i < 12; i++)
                e.Graphics.DrawLine(Pens.Black, new Point(28, i * 15 + 15), new Point(954, i * 15 + 15));

            e.Graphics.DrawLine(Pens.Black, new Point(9, 0 + 15), new Point(954, 0 + 15));
            e.Graphics.DrawLine(Pens.Black, new Point(9, 90 + 15), new Point(954, 90 + 15));
            e.Graphics.DrawLine(Pens.Black, new Point(9, 120 + 15), new Point(954, 120 + 15));
            e.Graphics.DrawLine(Pens.Black, new Point(9, 150 + 15), new Point(954, 150 + 15));
            e.Graphics.DrawLine(Pens.Black, new Point(9, 165 + 15), new Point(954, 165 + 15));

            e.Graphics.DrawLine(Pens.Black, new Point(28, 15), new Point(28, 165));   //画格子
            int jhy = 0;

            foreach (string s in jhxmy)
            {

                e.Graphics.DrawString(s, this.Font, Brushes.Black, new Point(30, jhy * 15 + 17));

                jhy++;
            }
            Pen pred2 = new Pen(Brushes.Red);
            pred2.Width = 2;


        }

        private void pictureBox8_DoubleClick(object sender, EventArgs e)
        {
            if (p8x > 28 && p8x < 169)
            {
                if (p8y > 16 && p8y < 105)
                {
                    addjhxm fromjhxm = new addjhxm(jhxma, jhxmy, mzjldID, 0);
                    fromjhxm.ShowDialog();
                    LB_JIANCEXM.Focus();

                }
                pictureBox4.Refresh();
            }
            pictureBox8.Refresh();
        }

        private void pictureBox8_MouseMove(object sender, MouseEventArgs e)
        {
            p8x = e.X; p8y = e.Y;

        }

        private void pictureBox9_Paint(object sender, PaintEventArgs e)
        {
            Pen pblack2 = new Pen(Brushes.Black);
            pblack2.Width = 2;
            Pen pblack = new Pen(Brushes.Black);
            for (int i = 0; i < 18; i++)
                e.Graphics.DrawLine(Pens.Black, new Point(0, 28 + 15 * i), new Point(87, 28 + 15 * i));

            e.Graphics.DrawLine(pblack2, new Point(0, 0), new Point(0, 1103));
            e.Graphics.DrawLine(pblack2, new Point(87, 0), new Point(87, 1103));
            e.Graphics.DrawLine(pblack2, new Point(0, 13), new Point(87, 13));
            e.Graphics.DrawLine(pblack, new Point(0, 638), new Point(87, 638));
            e.Graphics.DrawLine(pblack, new Point(0, 668), new Point(87, 668));
            e.Graphics.DrawLine(pblack, new Point(0, 832), new Point(87, 832));
            e.Graphics.DrawLine(pblack, new Point(0, 862), new Point(87, 862));

            e.Graphics.DrawLine(pblack2, new Point(0, 1103), new Point(87, 1103));
            int dy = 0;
            foreach (adims_MODEL.mzqt mz in mzqtList)  //画气体
            {
                if (mz.Bz == 2)
                {
                    int KAISHI = mz.Sysj.Hour * 60 + mz.Sysj.Minute;
                    int JIESHU = mz.Jssj.Hour * 60 + mz.Jssj.Minute;
                    int y1 = 15 * (dy + 1) + 18;
                    double qtzongliang = (mz.Yl) * (JIESHU - KAISHI) * 3.27;
                    e.Graphics.DrawString(qtzongliang.ToString() + "L", this.Font, Brushes.Blue, new Point(5, y1 - 2));
                    //MessageBox.Show(mz.Dw.Split('/').ToString());
                }
                dy++;

            }

            foreach (adims_MODEL.mzyt yt in ydyList)//画诱导药
            {
                if (dy == 0)
                {
                    dy++;
                }
                int y1 = 15 * (dy + 1) + 18;
                e.Graphics.DrawString(yt.Yl.ToString() + yt.Dw.ToString(), this.Font, Brushes.Black, new Point(5, y1 - 2));

                dy++;

            }
            int jty = 10;
            foreach (adims_MODEL.jtytsx jt in jmyList)//局麻药
            {

                int y1 = 15 * (jty + 1) + 18;
                e.Graphics.DrawString(jt.Jl.ToString() + jt.Dw.ToString(), this.Font, Brushes.Black, new Point(5, y1 - 2));

                jty++;


            }
            int jiaoty = 12;
            foreach (adims_MODEL.jtytsx jiaot in jiaot1) //画胶体
            {

                int y1 = 15 * (jiaoty + 1) + 18;
                e.Graphics.DrawString(jiaot.Jl.ToString() + jiaot.Dw.ToString(), this.Font, Brushes.Black, new Point(5, y1 - 2));

                jiaoty++;


            }
            int sxy = 14;
            foreach (adims_MODEL.jtytsx sx in sx1)//画输血
            {


                int y1 = 15 * (sxy + 1) + 18;
                e.Graphics.DrawString(sx.Jl.ToString() + sx.Dw.ToString(), this.Font, Brushes.Black, new Point(960, y1 - 2));


                sxy++;

            }
        }


        #endregion


        //#region《出室情况修改》
        //private void cmbDingXiangLi_TextChanged(object sender, EventArgs e)
        //{
        //    string SQL = "DingXiangLi='" + cmbDingXiangLi.Text + "' WHERE id='" + mzjldID + "'";
        //    int i = dal.UpdateMzjld_CSQK(SQL);
        //    if (i == 0)
        //    {
        //        MessageBox.Show("选择修改不成功，请重试!");
        //    }
        //}

        //private void cmbYishi_TextChanged(object sender, EventArgs e)
        //{
        //    string SQL = "Yishi='" + cmbYishi.Text + "' WHERE id='" + mzjldID + "'";
        //    int i = dal.UpdateMzjld_CSQK(SQL);
        //    if (i == 0)
        //    {
        //        MessageBox.Show("选择修改不成功，请重试!");
        //    }
        //}

        //private void cmbMaZuiPingMian_TextChanged(object sender, EventArgs e)
        //{
        //    string SQL = "MaZuiPingMianUP='" + cmbMaZuiPingMianUP.Text + "' WHERE id='" + mzjldID + "'";
        //    int i = dal.UpdateMzjld_CSQK(SQL);
        //    if (i == 0)
        //    {
        //        MessageBox.Show("选择修改不成功，请重试!");
        //    }
        //}
        //private void cmbMaZuiPingMianDOWN_TextChanged(object sender, EventArgs e)
        //{
        //    string SQL = "MaZuiPingMianDown='" + cmbMaZuiPingMianUP.Text + "' WHERE id='" + mzjldID + "'";
        //    int i = dal.UpdateMzjld_CSQK(SQL);
        //    if (i == 0)
        //    {
        //        MessageBox.Show("选择修改不成功，请重试!");
        //    }
        //}
        //private void cmbTengTong_TextChanged(object sender, EventArgs e)
        //{
        //    string SQL = "TengTong='" + cmbTengTong.Text + "' WHERE id='" + mzjldID + "'";
        //    int i = dal.UpdateMzjld_CSQK(SQL);
        //    if (i == 0)
        //    {
        //        MessageBox.Show("选择修改不成功，请重试!");
        //    }
        //}




        //private void cmbJiSongHuiFu_TextChanged(object sender, EventArgs e)
        //{
        //    string SQL = "JiSongHuiFu='" + cmbJiSongHuiFu.Text + "' WHERE id='" + mzjldID + "'";
        //    int i = dal.UpdateMzjld_CSQK(SQL);
        //    if (i == 0)
        //    {
        //        MessageBox.Show("选择修改不成功，请重试!");
        //    }
        //}




        //private void cmbTaiTou5Miao_TextChanged(object sender, EventArgs e)
        //{
        //    string SQL = "TaiTou5Miao='" + cmbTaiTou5Miao.Text + "' WHERE id='" + mzjldID + "'";
        //    int i = dal.UpdateMzjld_CSQK(SQL);
        //    if (i == 0)
        //    {
        //        MessageBox.Show("选择修改不成功，请重试!");
        //    }
        //}



        //private void cmbKeSouTunYan_TextChanged(object sender, EventArgs e)
        //{
        //    string SQL = "KeSouTunYan='" + cmbKeSouTunYan.Text + "' WHERE id='" + mzjldID + "'";
        //    int i = dal.UpdateMzjld_CSQK(SQL);
        //    if (i == 0)
        //    {
        //        MessageBox.Show("选择修改不成功，请重试!");
        //    }
        //}

        //private void cmbJingMaiTongChang_TextChanged(object sender, EventArgs e)
        //{
        //    string SQL = "JingMaiTongChang='" + cmbJingMaiTongChang.Text + "' WHERE id='" + mzjldID + "'";
        //    int i = dal.UpdateMzjld_CSQK(SQL);
        //    if (i == 0)
        //    {
        //        MessageBox.Show("选择修改不成功，请重试!");
        //    }
        //}

        //private void cmbEXinOuTu_TextChanged(object sender, EventArgs e)
        //{
        //    string SQL = "EXinOuTu='" + cmbEXinOuTu.Text + "' WHERE id='" + mzjldID + "'";
        //    int i = dal.UpdateMzjld_CSQK(SQL);
        //    if (i == 0)
        //    {
        //        MessageBox.Show("选择修改不成功，请重试!");
        //    }
        //}

        //private void cmbZhenTongFangFa_TextChanged(object sender, EventArgs e)
        //{
        //    string SQL = "ZhenTongFangFa='" + cmbZhenTongFangFa.Text + "' WHERE id='" + mzjldID + "'";
        //    int i = dal.UpdateMzjld_CSQK(SQL);
        //    if (i == 0)
        //    {
        //        MessageBox.Show("选择修改不成功，请重试!");
        //    }
        //}


        //private void cbmzxg_TextChanged(object sender, EventArgs e)
        //{
        //    string SQL = "mzxg='" + cmbMZXG.Text + "' WHERE id='" + mzjldID + "'";
        //    int i = dal.UpdateMzjld_CSQK(SQL);
        //    if (i == 0)
        //    {
        //        MessageBox.Show("选择修改不成功，请重试!");
        //    }
        //}
        //private void cmbMZXGPJ_TextChanged(object sender, EventArgs e)
        //{
        //    string SQL = "mzxgPingJi='" + cmbMZXGPJ.Text + "' WHERE id='" + mzjldID + "'";
        //    int i = dal.UpdateMzjld_CSQK(SQL);
        //    if (i == 0)
        //    {
        //        MessageBox.Show("选择修改不成功，请重试!");
        //    }
        //}
        //private void comboBox1_TextChanged(object sender, EventArgs e)
        //{
        //    string SQL = "mzxgPingJiSelect='" + this.comboBox1.Text + "' WHERE id='" + mzjldID + "'";
        //    int i = dal.UpdateMzjld_CSQK(SQL);
        //    if (i == 0)
        //    {
        //        MessageBox.Show("选择修改不成功，请重试!");
        //    }
        //}

        //#endregion



        private void btnSave_Click(object sender, EventArgs e)
        {
            int i = SaveMzjld();
            SavePaibanInfo();
            if (i > 0) MessageBox.Show("麻醉记录单修改成功！");
            else MessageBox.Show("麻醉记录单修改失败！");

        }

        private void BindXueyaMaiboHuxi()//绑定第一次检测到的血压脉搏呼吸
        {
            DataTable dt = bll.GetPoint(mzjldID);
            if (dt.Rows.Count > 0)
            {
                ucXueya.Controls[0].Text = Convert.ToString(dt.Rows[0]["NIBPS"])
                                + " / " + Convert.ToString(dt.Rows[0]["NIBPD"]);
                ucMaibo.Controls[0].Text = Convert.ToString(dt.Rows[0]["Pulse"]);
                ucHuxi.Controls[0].Text = Convert.ToString(dt.Rows[0]["RRC"]);
                //ucTemp.Controls[0].Text = Convert.ToString(dt.Rows[0]["RRC"]);
            }
            //else
            //    MessageBox.Show("暂无检测数据，请稍后再试！");
        }
        private void FirstToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BindXueyaMaiboHuxi();

        }
        private void BindJikongTime()//绑定机控呼吸开始结束时间
        {
            DataTable dtMZ_Info = bll.SelectOldPatInfo(mzjldID);
            if (dtMZ_Info.Rows.Count > 0)
            {
                if (dtMZ_Info.Rows[0]["jkkssj"].ToString() != "")
                {
                    jkksTime = Convert.ToDateTime(dtMZ_Info.Rows[0]["jkkssj"]);
                }
                if (dtMZ_Info.Rows[0]["jkkssj"].ToString() != "")
                {
                    jkjsTime = Convert.ToDateTime(dtMZ_Info.Rows[0]["jkjssj"]);
                }
            }

        }
        private void jkhxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JKTimeSet f1 = new JKTimeSet(mzjldID, 0);
            f1.ShowDialog();
            BindJikongTime();
            pictureBox3.Refresh();
            btnMonitor.Focus();
        }
        private void DeleteCGBGStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = dal.Update_MZJLD_CGBGSJ(mzjldID);
            if (i == 0)
            {
                MessageBox.Show("选择修改不成功，请重试!");
            }
            else
            {
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                lb_cguan.Visible = false;
                CGUAN = false;
                cgTime = new DateTime(1900, 1, 1);
                bgTime = new DateTime(1900, 1, 1);
                lb_cguan.MouseDown -= new MouseEventHandler(lb_cguan_MouseDown);
                lb_cguan.MouseMove -= new MouseEventHandler(lb_cguan_MouseMove);
                lb_cguan.MouseUp -= new MouseEventHandler(lb_cguan_MouseUp);
                lb_cguan.MouseLeave -= new EventHandler(lb_cguan_MouseLeave);
                CGUAN = false;
                BGUAN = false;
                lb_bguan.Visible = false;
                lb_bguan.MouseDown -= new MouseEventHandler(lb_bguan_MouseDown);
                lb_bguan.MouseMove -= new MouseEventHandler(lb_bguan_MouseMove);
                lb_bguan.MouseUp -= new MouseEventHandler(lb_bguan_MouseUp);
                lb_bguan.MouseLeave -= new EventHandler(lb_bguan_MouseLeave);

                pictureBox4.Refresh();
            }
            listBox1.Focus();
        }

        private void txtQXHS_DoubleClick(object sender, EventArgs e)
        {
            SelectYSorHS F1 = new SelectYSorHS(3, ucQXHS, patID);
            F1.ShowDialog();
            //string[] UpdStr = ucQXHS.Controls[0].Text.Split(new char[3] { '、', ',', '，' });
            string sqlWhere = "";
            int i = 0;
            foreach (var dr in F1.DoctotList)
            {
                if (i == 0)
                {
                    sqlWhere += $"  SN1='{dr.DoctorName}',SN1NO='{dr.DoctorNo}'";
                }
                if (i == 1)
                {
                    sqlWhere += $",SN2='{dr.DoctorName}',SN2No='{dr.DoctorNo}'";
                }

                i++;
            }
            //switch (UpdStr.Length)
            //{
            //    default:
            //        sqlWhere = " SN1='" + UpdStr[0] + "',SN2=''";
            //        break;
            //    case 2:
            //        sqlWhere = " SN1='" + UpdStr[0] + "',SN2='" + UpdStr[1] + "'";
            //        break;
            //}
            dal.UpdatePaibanWhere(patID, sqlWhere);
        }

        private void txtXHHS_DoubleClick(object sender, EventArgs e)
        {
            SelectYSorHS F1 = new SelectYSorHS(2, ucXHHS, patID);
            F1.ShowDialog();
            //string[] UpdStr = ucXHHS.Controls[0].Text.Split(new char[3] { '、', ',', '，' });
            string sqlWhere = "";
            //switch (UpdStr.Length)
            //{
            //    default:
            //        sqlWhere = " ON1='" + UpdStr[0] + "',ON2=''";
            //        break;
            //    case 2:
            //        sqlWhere = " ON1='" + UpdStr[0] + "',ON2='" + UpdStr[1] + "'";
            //        break;
            //}
            int i = 0;
            foreach (var dr in F1.DoctotList)
            {
                if (i == 0)
                {
                    sqlWhere += $"  ON1='{dr.DoctorName}',ON1NO='{dr.DoctorNo}'";
                }
                if (i == 1)
                {
                    sqlWhere += $",ON2='{dr.DoctorName}',ON2No='{dr.DoctorNo}'";
                }

                i++;
            }

            dal.UpdatePaibanWhere(patID, sqlWhere);
        }

        private void btnConsumablesUse_Click(object sender, EventArgs e)
        {
            ConsumablesUseForm cuf = new ConsumablesUseForm(mzjldID, patID);
            cuf.ShowDialog();
        }

        private void btnEHR_Click(object sender, EventArgs e)
        {

            //  EHRForm form = new EHRForm(patID);
            //form.Show();
            var remotepath = ConfigurationManager.AppSettings["EHR"];
            string http = remotepath;
            BrowserHelper.OpenDefaultBrowserUrl(http);
        }

        private void btnElectrocardiogram_Click(object sender, EventArgs e)
        {
            //ElectrocardiogramForm form = new ElectrocardiogramForm(patID);
            //form.Show();
            var res = dal.GetPaiban(patID);
            string PatientNo = res.Rows[0]["PatientNo"].ToString();
            var remotepath = ConfigurationManager.AppSettings["Electrocardiogram"];
            string http = remotepath + PatientNo;
            BrowserHelper.OpenDefaultBrowserUrl(http);
        }

        private void btnPathological_Click(object sender, EventArgs e)
        {
            //PathologicalForm form = new PathologicalForm(patID);
            //form.Show();
            var remotepath = ConfigurationManager.AppSettings["Pathological"];
            //help.GetXmlNodeInnerText(filename, "Pathological");
            string http = remotepath;
            BrowserHelper.OpenDefaultBrowserUrl(http);
        }

        private void txtSSYS_DoubleClick(object sender, EventArgs e)
        {
            SelectSSYS ssysform1 = new SelectSSYS(ucSSYS, patID);
            ssysform1.ShowDialog();
            //string[] UpdStr = ucSSYS.Controls[0].Text.Split(new char[3] { '、', ',', '，' });
            string sqlWhere = "";
            int i = 0;
            foreach (var dr in ssysform1.DoctotList)
            {
                if (i == 0)
                {
                    sqlWhere += $"  OS='{dr.DoctorName}',OsNo='{dr.DoctorNo}'";
                }
                if (i == 1)
                {
                    sqlWhere += $",OA1='{dr.DoctorName}',OA1No='{dr.DoctorNo}'";
                }
                if (i == 2)
                {
                    sqlWhere += $",OA2='{dr.DoctorName}',OA2No='{dr.DoctorNo}'";
                }
                if (i == 3)
                {
                    sqlWhere += $",OA3='{dr.DoctorName}',OA3No='{dr.DoctorNo}'";
                }
                i++;
            }

            //switch (UpdStr.Length)
            //{
            //    default:
            //        sqlWhere = " OS='" + UpdStr[0] + "',OA1='',OA2='',OA3=''";
            //        break;
            //    case 2:
            //        sqlWhere = " OS='" + UpdStr[0] + "',OA1='" + UpdStr[1] + "',OA2='',OA3=''";
            //        break;
            //    case 3:
            //        sqlWhere = " OS='" + UpdStr[0] + "',OA1='" + UpdStr[1] + "',OA2='" + UpdStr[2] + "',OA3=''";
            //        break;
            //    case 4:
            //        sqlWhere = " OS='" + UpdStr[0] + "',OA1='" + UpdStr[1] + "',OA2='" + UpdStr[2] + "',OA3='" + UpdStr[3] + "'";
            //        break;

            //}
            dal.UpdatePaibanWhere(patID, sqlWhere);
        }
        private void txtMZYS_DoubleClick(object sender, EventArgs e)
        {
            SelectYSorHS F1 = new SelectYSorHS(1, ucMZYS, patID);
            F1.ShowDialog();
            string[] UpdStr = ucMZYS.Controls[0].Text.Split(new char[3] { '、', ',', '，' });
            string sqlWhere = "";
            int i = 0;
            foreach (var dr in F1.DoctotList)
            {
                if (i == 0)
                {
                    sqlWhere += $"  AP1='{dr.DoctorName}',AP1NO='{dr.DoctorNo}'";
                }
                if (i == 1)
                {
                    sqlWhere += $",AP2='{dr.DoctorName}',AP2No='{dr.DoctorNo}'";
                }
                if (i == 2)
                {
                    sqlWhere += $",AP3='{dr.DoctorName}',AP3No='{dr.DoctorNo}'";
                }
                i++;
            }
            //switch (UpdStr.Length)
            //{
            //    default:
            //        sqlWhere = " AP1='" + UpdStr[0] + "',AP2='',AP3=''";
            //        break;
            //    case 2:
            //        sqlWhere = " AP1='" + UpdStr[0] + "',AP2='" + UpdStr[1] + "',AP3=''";
            //        break;
            //    case 3:
            //        sqlWhere = " AP1='" + UpdStr[0] + "',AP2='" + UpdStr[1] + "',AP3='" + UpdStr[2] + "'";
            //        break;

            //}
            dal.UpdatePaibanWhere(patID, sqlWhere);
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            timer5.Interval = 120 * 1000;
            if (!adims_BLL.UserFunction.PingHost(Program.Globals.SeverIp))
            {
                return;
            }
            SaveMzjld();
        }

        private void mzjldEdit_FormClosed(object sender, FormClosedEventArgs e)
        {

            if (_serialPort.IsOpen)
            {
                _serialPort.StopTransfer();
                _serialPort.Close();
                _serialPort.Dispose();
            }
            if (ThreadExist)
            {
                ThreadExist = false;
                Receiving_xy.Abort();//制结束线程运行
                Receiving_xy = null; ////在.NET中，有GC垃圾回收，如果这里不赋为null，有可能老半天也不回收内存，甚至永远这么占着，导致内存泄漏。
            }
        }
        private void btnzongjie_Click(object sender, EventArgs e)
        {
            string mzjldid = mzjldID.ToString();
            string patid = patID;
            MZZJ_SZ f2 = new MZZJ_SZ(mzjldid, patID);
            f2.Show();
        }


        private void button2_Click(object sender, EventArgs e)//修改入室时间
        {
            int a = at.update_otime(this.dtInRoomTime.Value.ToString("yyyy-MM-dd HH:mm"), ucMzjldId.Controls[0].Text);
            if (a > 0)
            {
                otime = this.dtInRoomTime.Value;
                BindShijiandian();
                BindMZSSCGBG();
                pictureBox2.Refresh();
                pictureBox3.Refresh();
                pictureBox4.Refresh();
            }
        }

        private void btnQXQD_Click(object sender, EventArgs e)
        {
            string mzjldid = mzjldID.ToString();
            string patid = patID;
            if (cbEyeOper.Checked)
            {
                NurseRecord_SZ_Eye f1 = new NurseRecord_SZ_Eye(patID, mzjldid);
                f1.ShowDialog();
            }
            else
            {
                NurseRecord_SZ f2 = new NurseRecord_SZ(patID, mzjldid);
                f2.ShowDialog();
            }

        }

        private void btnEmr_Click(object sender, EventArgs e)//链接电子病历
        {
            string userNO = Program.Customer.userno;            
            var remotepath = ConfigurationManager.AppSettings["EMR"];
            string http = remotepath + userNO;
            BrowserHelper.OpenDefaultBrowserUrl(http);
        }

        private void isEyeOper_CheckedChanged(object sender, EventArgs e)
        {
            //int isEye = 0;
            //if (isEyeOper.Checked)
            //{
            //    isEye = 1;
            //}
            //dal.Update_MZJLD_EyeOper(isEye,mzjldID);

        }
        /// <summary>
        /// 拷贝时间点
        /// </summary>
        DateTime CopyTime = new DateTime();
        private void btnMZJHS_Click(object sender, EventArgs e)
        {
            MZJHS F1 = new MZJHS(patID);
            F1.Show();
        }
        /// <summary>
        /// 查询本地采集记录的当前时间点次数
        /// </summary>
        int RepeatTimes = 0;
        /// <summary>
        /// 循环查找本地监测数据插入到服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Interval = 30 * 1000;
            int isinnet;
            if (!InternetGetConnectedState(out isinnet, 0))
            {
                if (warmingCount == 0)
                {
                    MessageBox.Show("服务器中断，请检查网线");
                    warmingCount++;
                }
                string errorStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 服务器连接失败!!";
                SaveLog(errorStr);
                return;
            }
            if (!adims_BLL.UserFunction.PingHost(Program.Globals.SeverIp))
            {
                string errorStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 服务器连接失败!!";
                SaveLog(errorStr);
                return;
            }

            DataTable dtServer = sh.MaxSeverTimeTable(mzjldID);
            if (dtServer.Rows.Count > 0 && dtServer.Rows[0][0].ToString() != "")
            {
                RepeatTimes++;
                CopyTime = Convert.ToDateTime(dtServer.Rows[0][0].ToString()).AddMinutes(jcsjjg);//设置下一个挑选点
                string QueryTime = CopyTime.ToString("yyyy-MM-dd HH:mm");
                DataTable dtLocal = sh.GetLocalByCreateTime(mzjldID, QueryTime, jcsjjg);
                if (dtLocal.Rows.Count > 0)
                {
                    DataRow dr = dtLocal.Rows[0];
                    int countN = sh.CopyDataFromDataRow(dr);//挑监护数据插入服务器的Adims_mzjld_Point表里
                    if (countN > 0)
                    {
                        BindJHDian();
                        RepeatTimes = 0;//还原查询次数
                        pictureBox3.Refresh();
                        pictureBox4.Refresh();
                    }
                }
            }
            else
            {
                RepeatTimes++;
                DateTime culTime = DateTime.Now;
                DataTable dtLocal = sh.GetLocalDataSortBy(mzjldID);
                if (dtLocal.Rows.Count > 0)
                {
                    culTime = Convert.ToDateTime(dtLocal.Rows[0]["CreateTime"].ToString());
                }
                int countN = sh.CopyData(mzjldID, culTime);//挑监护数据插入服务器的Adims_mzjld_Point表里
                if (countN > 0)
                {
                    BindJHDian();
                    RepeatTimes = 0;//还原查询次数
                    pictureBox3.Refresh();
                    pictureBox4.Refresh();
                }
            }


        }

        private void SaveLog(string str)
        {
            FileStream fs = new FileStream(Application.StartupPath + "\\ErrorLinkInternet.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.WriteLine(str);
            sw.Close();
            fs.Close();
        }
        bool isTiwenView = false;
        private void cbIsTiwen_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIsTiwen.Checked)
            {
                isTiwenView = true;
                dal.Update_MZJLD_isTiwenView(1, mzjldID);

            }
            else
            {
                dal.Update_MZJLD_isTiwenView(0, mzjldID);
                isTiwenView = false;
            }

            BindJHDian();
            pictureBox3.Refresh();
        }

        private void cmbBingRenQuXiang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SQL = "brqx='" + cmbBingRenQuXiang.Text + "' WHERE id='" + mzjldID + "'";
            int i = dal.UpdateMzjld_CSQK(SQL);
            if (i == 0)
            {
                MessageBox.Show("选择修改不成功，请重试!");
            }
        }

        private void cbE_CheckedChanged(object sender, EventArgs e)
        {
            string where = "";
            if (cbE.Checked)
            {
                where = " ASAE='1'";
            }
            else
            {
                where = " ASAE='0'";
            }

            dal.UpdatePaibanWhere(patID, where);
        }

        private void cmbASA_SelectedIndexChanged(object sender, EventArgs e)
        {
            string where = " ASA='" + cmbASA.Text + "'";

            dal.UpdatePaibanWhere(patID, where);
        }

        private void txtSSYS_KeyPress(object sender, KeyPressEventArgs e)
        {
            UserFunction.NoKeyboard(sender, e);
        }



    }
}

