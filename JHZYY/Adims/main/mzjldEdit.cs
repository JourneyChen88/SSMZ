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
using adims_BLL;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Runtime.InteropServices;
using adims_MODEL;
using System.Drawing.Printing;
using main.CGG;
using Adims_Utility;

namespace main
{
    public partial class mzjldEdit : Form, IMessageFilter
    {
        string patidccs;
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
        int PR = 0; bool pr_effective = false;    //脉率
        int RR = 0; bool rr_effective = false;    //呼吸频率
        int awRR = 0; bool awrr_effective = false;    //呼吸频率
        int ETCO2 = 0; bool etco2_effective = false;    //ETCO2
        int SPO2 = 0; bool spo2_effective = false;//血氧饱和度
        float TEMP = 0; bool temp_effective = false;//温度
        float TEMP1 = 0;//温度1
        float TEMP2 = 0; //温度2
        int HR = 0; bool hr_effective = false;    //心率
        int SYS = 0; bool sys_effective = false; //收缩压
        int DIA = 0; bool dia_effective = false; //舒张压
        int MAP = 0; bool map_effective = false; //平均血压
        int CVP = 0; bool CVP_effective = false; //中心静脉压
        int CVP_SYS = 0; bool CVP_sys_effective = false; //中心静脉收缩压
        int CVP_DIA = 0; bool CVP_dia_effective = false; //中心静脉舒张压
        int CVP_MAP = 0; bool CVP_map_effective = false; //中心静脉平均血压
        int ABP_SYS = 0; bool ABP_sys_effective = false; //动脉收缩压
        int ABP_DIA = 0; bool ABP_dia_effective = false; //动脉舒张压
        int ABP_MAP = 0; bool ABP_map_effective = false; //动脉平均血压
        int ART_SYS = 0; bool ART_sys_effective = false; //动脉收缩压
        int ART_DIA = 0; bool ART_dia_effective = false; //动脉舒张压
        int ART_MAP = 0; bool ART_map_effective = false; //动脉平均血压
        bool HasEffectiveData = false;
        bool HasEffectivePressure = false;
        DateTime MeasureDate;
        string TimeInput1;
        string IPAddressInput1;
        string BedIDInput1;
        int JHXcvp = 0;//监护项目
        int JHXqdy = 0;
        int JHXsdz = 0;
        int JHXjsz = 0;
        #endregion

        #region 金科威参数
        const int Waiting_Period_JKW = 200;
        Socket ServerSocket_JKW, TempSocket_JKW;
        bool ThreadExist_JKW = false;
        Thread Receiving_JKW;

        const int MaxTryTimes = 5; //尝试取值的次数
        const int JgTime = 5000; //采集间隔是5000毫秒

        Byte[] recv_Data_JKW = new Byte[8096];
        int GetTimes = 0;
        int count_JKW = 0;
        public Byte[] Raw_Data_JKW = new Byte[11000];
        int ReceivedLength_JKW = 0;
        string Laststring_JKW = ""; //上次收到的数据包
        DateTime ReceivedTime_JKW = DateTime.Now;
        public bool SockectIsException_JKW = false;

        
        #endregion

        #region 迈瑞参数
        int count_Miray = 0, MainTaincount_Miray = 0;
        int ReceivedLength_Miray = 0;
        private delegate void FlushClient(); //代理
        public bool isexist_Miray = false;
        public bool SockectIsException_Miray = false;
        public Thread SocketThread_Miray;
        static Socket ClientSocket_Miray = null;
        DateTime ReceivedTime = DateTime.Now;
        string Laststring_Miray = ""; //上次收到的数据包
        string Lasttest_Miray = "";
        public Byte[] Raw_Data_Miray = new Byte[11000];
        bool IsReceiving = false, IsSending = false;
        #endregion


        #region <<Members>>
        adims_BLL.YXL_BLL cll = new adims_BLL.YXL_BLL();
        SQLiteHelper sqlite = new adims_DAL.SQLiteHelper();
        DateTime fristopen = new DateTime();//第一页打印开始时间
        DateTime ptime = new DateTime();//每页打印开始时间
        int iYema = 1;
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_MODEL.paiban pat = new adims_MODEL.paiban();
        Label lbMzks1 = new Label();//麻醉开始lable
        Label lbMzjs1 = new Label();//麻醉结束lable
        Label lbSSKS = new Label();//手术开始lable
        Label lbSSJS = new Label();//手术结束lable
        Label lbCguan = new Label();//插管lable
        Label lbBguan = new Label();//拔管lable
        bool mzks = false, mzjs = true, ssks = false, ssjs = true, CGUAN = false, BGUAN = false;
        //麻醉开始，结束，手术开始，结束标志；插管，拔管标志   
        DateTime ssksTime = new DateTime();//手术开始事件
        DateTime ssjsTime = new DateTime();//手术结束事件
        DateTime mzksTime = new DateTime();//麻醉开始时间
        DateTime mzjsTime = new DateTime();//麻醉结束时间
        DateTime mzkszgnTime = new DateTime();//麻醉开始时间
        DateTime mzjkssjzzTime = new DateTime();//麻醉结束时间
        DateTime cgTime = new DateTime();//插管时间
        DateTime bgTime = new DateTime();//拔管时间
        DateTime jkksTime = new DateTime();//机控呼吸开始时间
        DateTime jkjsTime = new DateTime();//疾控呼吸结束时间
        DateTime ksjcTime = new DateTime();//开始检测时间参数
        public int mzjldID = 0;
        string patID = "";
        string Oroom = "";
        int flagP2 = 0, typeP2 = 0;//在pictureBox2上的选中标志，和选中类型
        adims_MODEL.mzqt t_mzqt = new adims_MODEL.mzqt();//单个气体药模板
        adims_MODEL.tw_point tw_point = new adims_MODEL.tw_point();//体温模板
        adims_MODEL.shuye t_shuye = new adims_MODEL.shuye();//单个输液模板
        adims_MODEL.shuxue t_shuxue = new adims_MODEL.shuxue();//单个输血模板
        adims_MODEL.mzpingmian t_mzpm = new adims_MODEL.mzpingmian(); //单个麻醉平面模板
        adims_MODEL.mzyt t_ydy = new adims_MODEL.mzyt(); //单个诱导药模板
        adims_MODEL.jtytsx t_jmy = new adims_MODEL.jtytsx(); //单个局麻药模板
        adims_MODEL.tsyy t_tsyy = new adims_MODEL.tsyy();//单个特殊用药模板
        adims_MODEL.szsj t_szsj = new adims_MODEL.szsj();//单个术中事件模板
        adims_MODEL.clcxqt t_chuniao = new adims_MODEL.clcxqt();//单个出尿模板
       
        adims_MODEL.clcxqt t_niaoliang = new adims_MODEL.clcxqt();
        adims_MODEL.clcxqt t_yll = new adims_MODEL.clcxqt();
        adims_MODEL.clcxqt t_shixue = new adims_MODEL.clcxqt();
        float p2x = 0, p2y = 0;//鼠标在picturebox2上的位置
        float p3x = 0, p3y = 0;//鼠标在picturebox3上的位置
        int flagP3 = 0, typeP3 = 0;
        int ssmzcgbgFlag = 0;//手术麻醉插管拔管鼠标按下是否选中标志 

        adims_MODEL.point t_point = new adims_MODEL.point();//单个point模板       
      
        private List<string> jhxmAll = new List<string>();//所有备选监护项目
        private List<string> jhxmIn = new List<string>();//已添加的监护项目
        public List<adims_MODEL.point> ssyList = new List<adims_MODEL.point>();//收缩点集合
        public List<adims_MODEL.point> szyList = new List<adims_MODEL.point>();//舒张压点集合
        public List<adims_MODEL.point> xlList = new List<adims_MODEL.point>();//心率点集合
        public List<adims_MODEL.tw_point> twList = new List<adims_MODEL.tw_point>();//体温点集合
        public List<adims_MODEL.point> mboList = new List<adims_MODEL.point>();//脉搏点集合
        public List<adims_MODEL.point> hxlList = new List<adims_MODEL.point>();//呼吸率点集合
        public List<adims_MODEL.point> spo2List = new List<adims_MODEL.point>();//spo2集合
        public List<adims_MODEL.point> etco2List = new List<adims_MODEL.point>();//etco2集合
        public List<adims_MODEL.point> BIS = new List<adims_MODEL.point>();//BIS集合
        public List<adims_MODEL.point> cvpList = new List<adims_MODEL.point>();//cvp集合
        public List<adims_MODEL.point> tofList = new List<adims_MODEL.point>();//tof集合
        private List<adims_MODEL.jhxm> jhxmValue = new List<adims_MODEL.jhxm>();//监护项目值

        public List<adims_MODEL.clcxqt> cxList = new List<adims_MODEL.clcxqt>();//出血
        public List<adims_MODEL.clcxqt> cnList = new List<adims_MODEL.clcxqt>();//出尿其他出量
        public List<adims_MODEL.clcxqt> yllList = new List<adims_MODEL.clcxqt>();


        Point lastpoint = new Point();//画线的时候保存上一个点
        Point lastpoint1 = new Point();//画线的时候保存上一个点
        Point lastpoint2 = new Point();//画线的时候保存上一个点
        Point lastpoint3 = new Point();//画线的时候保存上一个点
        Point lastpoint4 = new Point();//画线的时候保存上一个点
        Point lastpoint5 = new Point();//画线的时候保存上一个点       
        DateTime otime = new DateTime();//画图起始时间
        double syZongl = 0, sxZongl = 0;
        List<adims_MODEL.mzpingmian> mzpmList = new List<adims_MODEL.mzpingmian>();//麻醉平面
        List<adims_MODEL.mzqt> mzqtList = new List<adims_MODEL.mzqt>();//气体药集合
        List<adims_MODEL.shuxue> shuxueList = new List<adims_MODEL.shuxue>(); //输血集合    
        List<adims_MODEL.shuye> shuyeList = new List<adims_MODEL.shuye>();//输液集合
        List<adims_MODEL.jtytsx> jmyList = new List<adims_MODEL.jtytsx>();//局部麻醉药集合
        List<adims_MODEL.mzyt> ydyList = new List<adims_MODEL.mzyt>();//诱导药集合
        List<adims_MODEL.szsj> szsjList = new List<adims_MODEL.szsj>();//术中事件集合
        List<adims_MODEL.tsyy> tsyyList = new List<adims_MODEL.tsyy>();//特殊用药集合
        List<adims_MODEL.ZhenTongYao> ztyList = new List<adims_MODEL.ZhenTongYao>();//镇痛药
        List<adims_MODEL.clcxqt> chuniaoList = new List<adims_MODEL.clcxqt>();//出尿出血其他出量

        int jcsjjg = 0;//检测时间间隔分钟数
        bool isSjll = false;//是否是手术间浏览进入
        DateTime odate;
        Label lab1 = new Label();//移动提示字符
        Label lab2 = new Label();//移动提示字符

        #endregion

        #region <<Constructors>>

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="patid122"></param>
        public mzjldEdit(string patid, string oroom, DateTime dt, int mzid, bool sjllState)
        {
            odate = dt;
            isSjll = sjllState;
            Oroom = oroom;
            mzjldID = mzid;
            this.patID = patid;
            InitializeComponent();
            GetConfigure();
            tbIpAdress.Text = IPAddressInput1;
            //BedNumber1.Text = BedIDInput1;
            phillip_server = new IPEndPoint(IPAddress.Parse(tbIpAdress.Text.Trim()), 24105);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Remote = new IPEndPoint(IPAddress.Any, 8001);
            phillip = (EndPoint)Remote;
        }
        public mzjldEdit(string patid, string oroom, DateTime dt, int mzid)
        {
            odate = dt;
            mzjldID = mzid;
            Oroom = oroom;
            this.patID = patid;
            InitializeComponent();
            GetConfigure();
            tbIpAdress.Text = IPAddressInput1;
          //  BedNumber1.Text = BedIDInput1;
            phillip_server = new IPEndPoint(IPAddress.Parse(tbIpAdress.Text.Trim()), 24105);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Remote = new IPEndPoint(IPAddress.Any, 8001);
            phillip = (EndPoint)Remote;
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        public mzjldEdit(string patID)
        {
            this.patID = patID;
            InitializeComponent();
            GetConfigure();
            tbIpAdress.Text = IPAddressInput1;
            //BedNumber1.Text = BedIDInput1;
            phillip_server = new IPEndPoint(IPAddress.Parse(tbIpAdress.Text.Trim()), 24105);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Remote = new IPEndPoint(IPAddress.Any, 8001);
            phillip = (EndPoint)Remote;
        }
        public mzjldEdit(string mzjldID, string patID)
        {
            this.mzjldID = Convert.ToInt32(mzjldID);
            this.patID = patID;
            InitializeComponent();
            GetConfigure();
            tbIpAdress.Text = IPAddressInput1;
            //BedNumber1.Text = BedIDInput1;
            phillip_server = new IPEndPoint(IPAddress.Parse(tbIpAdress.Text.Trim()), 24105);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Remote = new IPEndPoint(IPAddress.Any, 8001);
            phillip = (EndPoint)Remote;
        }
        #endregion

        private void timer3_Tick(object sender, EventArgs e)//移动时候提示数据显示时间
        {
            labVisibleTimer.Interval = 2500;
            lab1.Visible = false;
            lab2.Visible = false;
            labVisibleTimer.Enabled = false;
        }
        private void btnSaveOtime_Click(object sender, EventArgs e)
        {
            string SQL = "otime='" + dtOtime.Value + "' WHERE id='" + mzjldID + "'";
            int i = dal.UpdateMzjld(SQL);
            if (i == 0) MessageBox.Show("入室时间修改失败！");
            else
            {
                otime = Convert.ToDateTime(dtOtime.Value.ToString("yyyy-MM-dd HH:mm"));
                bll.xgqtKssjasd(mzjldID, dtOtime.Value.AddMinutes(2));
                fristopen = otime;
                BindShijiandian();
                BindMZSSCGBG();
                BindQtList();
                pictureBox2.Refresh();
                pictureBox3.Refresh();
                pictureBox4.Refresh();
            }
        }

        private void cmbSJJG_SelectedIndexChanged(object sender, EventArgs e)//修改检测时间间隔
        {
            string SQL = "jcsjjg='" + cmbSJJG.Text.Trim() + "' WHERE id='" + mzjldID + "'";
            int i = dal.UpdateMzjld(SQL);
            if (i > 0)
            {
                BindShijiandian();
                pictureBox2.Refresh();
                pictureBox3.Refresh();
                BindMZSSCGBG();
            }
        }

        private void BindShijiandian()//绑定时间点坐标
        {
            fristopen = otime;
            jhxmAll.Clear();
            jhxmAll.Add("ECG");
            jhxmAll.Add("CVP");
            jhxmAll.Add("NIBP");
            jhxmAll.Add("ART");
            jhxmAll.Add("RESP");
            jhxmAll.Add("BIS");
            jhxmAll.Add("TOF");
            jhxmAll.Add("ETCO2");
            jcsjjg = Convert.ToInt32(cmbSJJG.Text.Trim());
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
        }

        private void BindMzjldBacicInfo()//加载麻醉记录单信息
        {
            DataTable dtMzjld = bll.selectmzjld(mzjldID);
            txtHeight.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["Height"]);
            txtWeight.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["Weight"]);
            
            txtTW.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["tiwen"]);
            txtXueya.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["xueya"]);
            txtHuxi.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["huxi"]);
            txtMaibo.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["maibo"]);
            cmbXueXing.Text = Convert.ToString(dtMzjld.Rows[0]["xuexing"]);
            cmbASA.Text = Convert.ToString(dtMzjld.Rows[0]["ASA"]);
            txtSqzd.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["sqzd"]);
            txtTSBQing.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["nssss"]);
            cmbTiwei.Text = Convert.ToString(dtMzjld.Rows[0]["tw"]);
            cmbSQJinshi.Text = Convert.ToString(dtMzjld.Rows[0]["SQJinshi"]);
            if (Convert.ToString(dtMzjld.Rows[0]["isjizhen"]) == "1")
                this.cbJizhen.Checked = true;
            if (Convert.ToString(dtMzjld.Rows[0]["isjizhen"]) == "0")
                this.cbZeqi.Checked = true;
            txtsshzd.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["szzd"]);
            txtShoushuFS.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["ShoushuFS"]);
            txtMazuiFS.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["mazuiFS"]);
            txtSSYS.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["ssys"]);
            txtMZYS.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["mzys"]);
            txtXHHS.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["qxhs"]);
            cmbzhentongy.Text = Convert.ToString(dtMzjld.Rows[0]["PCA"]);
            cmbmazuixiaoguo.Text = Convert.ToString(dtMzjld.Rows[0]["mzxg"]);
            cmbBRQX.Text = Convert.ToString(dtMzjld.Rows[0]["brqx"]);
            txtmazuipingfen.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["pingfen"]);
            txtzitixue.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["zitixue"]);
            txtcfsx.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["chengxue"]);
            txtjiaotixue.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["jiaotiye"]);
            txtjingtixue.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["jintiye"]);
            txtzongsrl.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["zongsrl"]);
            txtchuxuel.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["ChuXue"]);
            txtniaoliang.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["Niaoliang"]);



            //txtSqyy.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["sqyy"]);
            //txtTSBQing.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["tsbq"]);
            //tbChuNiao.Text = Convert.ToString(dtMzjld.Rows[0]["niaoliang"]);
            //tbChuxue.Text = Convert.ToString(dtMzjld.Rows[0]["chuxue"]);
            //cmbTiwei.Text = Convert.ToString(dtMzjld.Rows[0]["tw"]);
            //txtShoushuFS.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["ShoushuFS"]);
            //txtMazuiFS.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["mazuiFS"]);
            //txtNssss.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["nssss"]);
            //txtSSYS.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["ssys"]);
            //txtMZYS.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["mzys"]);
            //txtQXHS.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["qxhs"]);
            //txtXHHS.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["xhhs"]);
            //tXTPH.Text = Convert.ToString(dtMzjld.Rows[0]["PH"]);
            //txtpco2.Text = Convert.ToString(dtMzjld.Rows[0]["PCO2"]);
            //txtpo2.Text = Convert.ToString(dtMzjld.Rows[0]["PO2"]);
            //txtbe.Text = Convert.ToString(dtMzjld.Rows[0]["BE"]);
            //txthco3.Text = Convert.ToString(dtMzjld.Rows[0]["HCO3"]);
            //txthb.Text = Convert.ToString(dtMzjld.Rows[0]["K"]);
            //txtk.Text = Convert.ToString(dtMzjld.Rows[0]["HB"]);
            //txthct.Text = Convert.ToString(dtMzjld.Rows[0]["HCT"]);
            //txtxuet.Text = Convert.ToString(dtMzjld.Rows[0]["XUETANG"]);

            //cmbMZXG.Text = Convert.ToString(dtMzjld.Rows[0]["mzxg"]);
            //cmbASA.Text = Convert.ToString(dtMzjld.Rows[0]["ASA"]);
            //cmbSQJinshi.Text = Convert.ToString(dtMzjld.Rows[0]["SQJinshi"]);
            //if (Convert.ToString(dtMzjld.Rows[0]["isjizhen"]) == "1")
            //    this.cbJizhen.Checked = true;
            //if (Convert.ToString(dtMzjld.Rows[0]["isjizhen"]) == "0")
            //    this.cbZeqi.Checked = true;
            //cmbBRQX.Text = Convert.ToString(dtMzjld.Rows[0]["brqx"]);
            //this.cmbQiekouType.Text = Convert.ToString(dtMzjld.Rows[0]["QiekouType"]);
            //this.tbQiekouCount.Text = Convert.ToString(dtMzjld.Rows[0]["QiekouCount"]);
            if (Convert.ToString(dtMzjld.Rows[0]["ifxssj"]).Contains("1")) cheboxBIS.Checked = true;
            if (Convert.ToString(dtMzjld.Rows[0]["ifxssj"]).Contains("2")) checktw.Checked = true;
            if (Convert.ToString(dtMzjld.Rows[0]["ifxssj"]).Contains("3")) checkmb.Checked = true;
            if (Convert.ToString(dtMzjld.Rows[0]["ifxssj"]).Contains("4")) checkssy.Checked = true;
            if (Convert.ToString(dtMzjld.Rows[0]["ifxssj"]).Contains("5")) checkszy.Checked = true;
            if (Convert.ToString(dtMzjld.Rows[0]["ifxssj"]).Contains("6")) checkhx.Checked = true;
            otime = Convert.ToDateTime(dtMzjld.Rows[0]["otime"]);
            dtOdate.Value = otime; dtOtime.Value = otime;
            BindMZSSCGBG();//手术，麻醉，插管拔管时间赋值，坐标赋值         

            //// ↓术中事件赋值
            DataTable dtSZSJ = dal.GetSZSJ(mzjldID);
            if (dtSZSJ.Rows.Count > 0)
            {
                for (int i = 0; i < dtSZSJ.Rows.Count; i++)
                {
                    listBox1.Items.Add(i + 1 + "." + dtSZSJ.Rows[i][2]
                        + " " + Convert.ToDateTime(dtSZSJ.Rows[i][3]).ToString("HH:mm"));
                }
            }
            // ↓特殊用药赋值
            DataTable dtTSYY = dal.GetTSYY(mzjldID);
            if (dtTSYY.Rows.Count > 0)
            {
                for (int i = 0; i < dtTSYY.Rows.Count; i++)
                {
                    listBox2.Items.Add(i + 1 + "." + dtTSYY.Rows[i][2] + " "
                        + dtTSYY.Rows[i][3] + " " + dtTSYY.Rows[i][4]);
                }
            }
        }

        private void BindMZSSCGBG()//绑定麻醉手术插管拔管
        {
            DataTable dtMzjld = bll.selectmzjld(mzjldID);
            if (dtMzjld.Rows[0]["otime"].ToString() != "")
            {
                DateTime ssDate = (Convert.ToDateTime(dtMzjld.Rows[0]["otime"]));
                dtOdate.Value = otime;
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
                    lbMzks1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    lbMzks1.BackColor = Color.Transparent;
                    lbMzks1.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 160), 370);
                    this.pictureBox3.Controls.Add(lbMzks1);
                    mzks = true;
                    mzjs = false;
                    lbMzks1.MouseDown += new MouseEventHandler(lbMzks1_MouseDown);
                    lbMzks1.MouseMove += new MouseEventHandler(lbMzks1_MouseMove);
                    lbMzks1.MouseUp += new MouseEventHandler(lbMzks1_MouseUp);
                    lbMzks1.MouseLeave += new EventHandler(lbMzks1_MouseLeave);
                }

            }
            if (dtMzjld.Rows[0]["mzkszgn"].ToString() != "")
            {
                mzkszgnTime = Convert.ToDateTime(dtMzjld.Rows[0]["mzkszgn"]);
                TimeSpan t = new TimeSpan();
                t = mzkszgnTime - otime;
                if ((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) > 0)
                {
                    lbMzkszgn1.Visible = true;
                    lbMzkszgn1.Text = "X1";
                    lbMzkszgn1.AutoSize = true;
                    lbMzkszgn1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    lbMzkszgn1.BackColor = Color.Transparent;
                    lbMzkszgn1.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 160), 370);
                    this.pictureBox3.Controls.Add(lbMzkszgn1);
                    mzks = true;
                    mzjs = false;
                    lbMzkszgn1.MouseDown += new MouseEventHandler(lbMzkszgn1_MouseDown);
                    lbMzkszgn1.MouseMove += new MouseEventHandler(lbMzkszgn1_MouseMove);
                    lbMzkszgn1.MouseUp += new MouseEventHandler(lbMzkszgn1_MouseUp);
                    lbMzkszgn1.MouseLeave += new EventHandler(lbMzkszgn1_MouseLeave);
                }

            }
            if (dtMzjld.Rows[0]["mzkssjzz"].ToString() != "")
            {
                mzjkssjzzTime = Convert.ToDateTime(dtMzjld.Rows[0]["mzkssjzz"]);
                TimeSpan t = new TimeSpan();
                t = mzjkssjzzTime - otime;
                if ((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) > 0)
                {
                    mzkssjzz.Visible = true;
                    mzkssjzz.Text = "X2";
                    mzkssjzz.AutoSize = true;
                    mzkssjzz.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    mzkssjzz.BackColor = Color.Transparent;
                    mzkssjzz.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 160), 370);
                    this.pictureBox3.Controls.Add(mzkssjzz);
                    mzks = true;
                    mzjs = false;
                    mzkssjzz.MouseDown += new MouseEventHandler(mzkssjzz_MouseDown);
                    mzkssjzz.MouseMove += new MouseEventHandler(mzkssjzz_MouseMove);
                    mzkssjzz.MouseUp += new MouseEventHandler(mzkssjzz_MouseUp);
                    mzkssjzz.MouseLeave += new EventHandler(mzkssjzz_MouseLeave);
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
                    lbMzjs1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    lbMzjs1.BackColor = Color.Transparent;
                    lbMzjs1.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 160), 370);
                    this.pictureBox3.Controls.Add(lbMzjs1);
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
                    lbSSKS.Text = "⊙";
                    lbSSKS.Visible = true;
                    lbSSKS.AutoSize = true;
                    lbSSKS.BackColor = Color.Transparent;
                    lbSSKS.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    lbSSKS.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 160), 370);
                    this.pictureBox3.Controls.Add(lbSSKS);
                    ssks = true;
                    ssjs = false;
                    lbSSKS.MouseDown += new MouseEventHandler(ssks1_MouseDown);
                    lbSSKS.MouseMove += new MouseEventHandler(ssks1_MouseMove);
                    lbSSKS.MouseUp += new MouseEventHandler(ssks1_MouseUp);
                    lbSSKS.MouseLeave += new EventHandler(ssks1_MouseLeave);
                }
            }
            if (dtMzjld.Rows[0]["ssjssj"].ToString() != "")
            {
                ssjsTime = Convert.ToDateTime(dtMzjld.Rows[0]["ssjssj"]);
                TimeSpan t = new TimeSpan();
                t = ssjsTime - otime;
                if ((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) > 0)
                {
                    lbSSJS.Visible = true;
                    lbSSJS.Text = " ";
                    lbSSJS.Image = Properties.Resources.SSJS;
                    lbSSJS.AutoSize = true;
                    lbSSJS.BackColor = Color.Transparent;
                    lbSSJS.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    lbSSJS.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 160), 370);
                    this.pictureBox3.Controls.Add(lbSSJS);
                    ssjs = true;
                    lbSSJS.MouseDown += new MouseEventHandler(ssjs1_MouseDown);
                    lbSSJS.MouseMove += new MouseEventHandler(ssjs1_MouseMove);
                    lbSSJS.MouseUp += new MouseEventHandler(ssjs1_MouseUp);
                    lbSSJS.MouseLeave += new EventHandler(ssjs1_MouseLeave);
                }
            }
            if (dtMzjld.Rows[0]["cgsj"].ToString() != "")
            {
                cgTime = Convert.ToDateTime(dtMzjld.Rows[0]["cgsj"]);
                TimeSpan t = new TimeSpan();
                t = cgTime - otime;
                if ((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) > 0)
                {
                    lbCguan.Visible = true;
                    lbCguan.Text = " ";
                    lbCguan.Image = Properties.Resources.CG;
                    lbCguan.AutoSize = true;
                    lbCguan.BackColor = Color.Transparent;
                    lbCguan.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    lbCguan.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 160), 370);
                    this.pictureBox3.Controls.Add(lbCguan);
                    CGUAN = true;
                    BGUAN = false;
                    lbCguan.MouseDown += new MouseEventHandler(lb_cguan_MouseDown);
                    lbCguan.MouseMove += new MouseEventHandler(lb_cguan_MouseMove);
                    lbCguan.MouseUp += new MouseEventHandler(lb_cguan_MouseUp);
                    lbCguan.MouseLeave += new EventHandler(lb_cguan_MouseLeave);
                }


            }
            if (dtMzjld.Rows[0]["bgsj"].ToString() != "")
            {
                bgTime = Convert.ToDateTime(dtMzjld.Rows[0]["bgsj"]);
                TimeSpan t = new TimeSpan();
                t = bgTime - otime;
                if ((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) > 0)
                {
                    lbBguan.Text = " ";
                    lbBguan.Image = Properties.Resources.BG;
                    lbBguan.Visible = true;
                    lbBguan.AutoSize = true;
                    lbBguan.BackColor = Color.Transparent;
                    lbBguan.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    lbBguan.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 160), 370);
                    this.pictureBox3.Controls.Add(lbBguan);
                    BGUAN = true;
                    lbBguan.MouseDown += new MouseEventHandler(lb_bguan_MouseDown);
                    lbBguan.MouseMove += new MouseEventHandler(lb_bguan_MouseMove);
                    lbBguan.MouseUp += new MouseEventHandler(lb_bguan_MouseUp);
                    lbBguan.MouseLeave += new EventHandler(lb_bguan_MouseLeave);
                }
            }
        }

        private void GetPatBasicInfo()//加载病人基本信息
        {
            DataTable dt = bll.SelectPatInfo111(patID);
            txtpatID.Controls[0].Text = Convert.ToString(dt.Rows[0]["patid"]);
            txtName.Controls[0].Text = Convert.ToString(dt.Rows[0]["patname"]);
            txtAge.Controls[0].Text = Convert.ToString(dt.Rows[0]["patage"]);
            txtSex.Controls[0].Text = Convert.ToString(dt.Rows[0]["patsex"]);
            txtHeight.Controls[0].Text = Convert.ToString(dt.Rows[0]["PatHeight"]);
            txtWeight.Controls[0].Text = Convert.ToString(dt.Rows[0]["PatWeight"]);
            this.cmbXueXing.Text = Convert.ToString(dt.Rows[0]["PatBloodType"]);
            txtZhuYuanHao.Controls[0].Text = Convert.ToString(dt.Rows[0]["patid"]);
            patidccs= Convert.ToString(dt.Rows[0]["patid"]);
            txtsshzd.Controls[0].Text = Convert.ToString(dt.Rows[0]["pattmd"]);
            this.txtBingQu.Controls[0].Text = Convert.ToString(dt.Rows[0]["patdpm"]);
            this.txtBednumber.Controls[0].Text = Convert.ToString(dt.Rows[0]["patbedno"]);
            txtNssss.Controls[0].Text = Convert.ToString(dt.Rows[0]["oname"]);
            txtShoushuFS.Controls[0].Text = Convert.ToString(dt.Rows[0]["oname"]);
            txtSqzd.Controls[0].Text = Convert.ToString(dt.Rows[0]["pattmd"]);
            txtMazuiFS.Controls[0].Text = Convert.ToString(dt.Rows[0]["Amethod"]);
            txtSSYS.Controls[0].Text = Convert.ToString(dt.Rows[0]["os"])
                                + " " + Convert.ToString(dt.Rows[0]["os1"]);
            txtMZYS.Controls[0].Text = Convert.ToString(dt.Rows[0]["ap1"])
                                + " " + Convert.ToString(dt.Rows[0]["ap2"])
                                + " " + Convert.ToString(dt.Rows[0]["ap3"]);
            txtQXHS.Controls[0].Text = Convert.ToString(dt.Rows[0]["On1"])
                                + " " + Convert.ToString(dt.Rows[0]["on2"]);
            txtXHHS.Controls[0].Text = Convert.ToString(dt.Rows[0]["Sn1"])
                                + " " + Convert.ToString(dt.Rows[0]["sn2"]);
            dtOdate.Value = Convert.ToDateTime(dt.Rows[0]["Odate"]);
            textBsfzh.Text = Convert.ToString(dt.Rows[0]["zrys"]);
        }
        private void cmbTiweiBind()//绑定体位下拉表
        {
            DataTable dt = dal.SelectData("tiwei");
            cmbTiwei.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                cmbTiwei.Items.Add(dr[1].ToString());
            }
        }
        private void cmbQiekouTypeBind()//绑定切口类型
        {
            DataTable dt = dal.SelectData("qiekoutype");
            cmbQiekouType.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                cmbQiekouType.Items.Add(dr[1].ToString());
            }
        }
        private void BindPortName()//绑定串口COM口列表
        {
            string[] spName = System.IO.Ports.SerialPort.GetPortNames();
            if (spName.Length > 0)
            {
                cmbCOM.Items.Clear();
                cmbCOM.Items.Add("COM3");
                foreach (string portName in spName)
                {
                    cmbCOM.Items.Add(portName);
                   
                }
            }
            cmbCOM.SelectedIndex = 0;
            cmbJianhuyi.SelectedIndex = 0;
        }
        #region //IMessageFilter 成员,禁止下拉框鼠标滚动

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 522)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
        private void CheckLocalAndServerData()//检查本地和服务器的数据
        {
            DataTable ServerMax = dal.GetMaxTimeServer(mzjldID);//服务器最大时间点数据
            DataTable LoaclMax = dal.GetMaxTimeLocal(mzjldID);//本地最大时间点数据
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
                    DataTable LoacalMin = dal.GetMinTimeLocal(mzjldID);
                    ServerMaxTime = Convert.ToDateTime(LoacalMin.Rows[0][0]);
                }
                ServerMaxTime = ServerMaxTime.AddMinutes(jcsjjg);
                while (ServerMaxTime <= LocalMaxTime)
                {
                    DataTable dtIn = dal.GetMzjldPointInServer(ServerMaxTime, mzjldID);
                    if (dtIn.Rows.Count == 0)
                    {
                        int a = dal.CopyLocalToServer(ServerMaxTime.ToString("yyyy-MM-dd HH:mm"), mzjldID);
                        ServerMaxTime = ServerMaxTime.AddMinutes(jcsjjg);
                    }
                }
            }
        }
        private void mzjld_Load(object sender, EventArgs e)/// 窗体加载
        {
            //LOAD_jiekou();//加载迈瑞接口数据
            Application.AddMessageFilter(this);//禁止下拉框鼠标滚动
            //this.Text  +="(当前:  " + Program.customer.user_name + "+ 职称：" + Program.customer.position + ")";
            this.txtSqzd.Controls[0].DoubleClick += new System.EventHandler(this.txtSqzd_DoubleClick);
            this.txtSqyy.Controls[0].DoubleClick += new System.EventHandler(this.txtSqyy_DoubleClick);
            this.txtShoushuFS.Controls[0].DoubleClick += new System.EventHandler(this.txtShoushuFS_DoubleClick);
            this.txtMazuiFS.Controls[0].DoubleClick += new System.EventHandler(this.txtMazuiFS_DoubleClick);
            this.txtNssss.Controls[0].DoubleClick += new System.EventHandler(this.txtNssss_DoubleClick);
            this.txtMZYS.Controls[0].DoubleClick += new System.EventHandler(this.txtMZYS_DoubleClick);
            this.txtQXHS.Controls[0].DoubleClick += new System.EventHandler(this.txtQXHS_DoubleClick);
            this.txtXHHS.Controls[0].DoubleClick += new System.EventHandler(this.txtXHHS_DoubleClick);

            BindPortName();
            cmbTiweiBind();
            cmbQiekouTypeBind();
            this.dtOtime.Format = DateTimePickerFormat.Custom;
            this.dtOtime.CustomFormat = "yyyy-MM-dd HH:mm";
            try
            {
                GetPatBasicInfo();
                if (mzjldID == 0)
                {
                    if (DateTime.Now.Minute < 10)
                        otime = DateTime.Now.AddMinutes(-DateTime.Now.Minute - 10);
                    else if (DateTime.Now.Minute >= 10 && DateTime.Now.Minute < 20)
                        otime = DateTime.Now.AddMinutes(-DateTime.Now.Minute);
                    else if (DateTime.Now.Minute >= 20 && DateTime.Now.Minute < 30)
                        otime = DateTime.Now.AddMinutes(-DateTime.Now.Minute + 10);
                    else if (DateTime.Now.Minute >= 30 && DateTime.Now.Minute < 40)
                        otime = DateTime.Now.AddMinutes(-DateTime.Now.Minute + 20);
                    else if (DateTime.Now.Minute >= 40 && DateTime.Now.Minute < 50)
                        otime = DateTime.Now.AddMinutes(-DateTime.Now.Minute + 30);
                    else
                        otime = DateTime.Now.AddMinutes(-DateTime.Now.Minute + 40);

                    dtOtime.Value = otime;
                    bll.addmzjld(patID, otime);
                    DataTable dt = new DataTable();
                    dt = bll.selectSinglemzjld(patID, otime);
                    mzjldID = Convert.ToInt32(dt.Rows[0][0]);
                    txtMzjldid.Controls[0].Text = Convert.ToString(mzjldID);
                    dal.UpdateShoushujianinfo(0, mzjldID, patID, Oroom);//修改手术间状态信息
                    dal.UpdatePaibanInfo(2, patID);
                    jhxmIn.Clear();
                    jhxmIn.Add("SpO2");
                    int f_jhxm = 0;
                    foreach (string name in jhxmIn)
                    {
                        bll.addJhxm(mzjldID, name, 0);
                        f_jhxm++;
                    }
                    if (f_jhxm == 0)
                    {
                        MessageBox.Show("监护项目添加失败！");
                    }
                    cmbSJJG.Text = "5";
                    jcsjjg = 5;
                    BindShijiandian();
                    int Flag = SaveMzjld();
                    if (Flag == 0)
                    {
                        MessageBox.Show("请手动保存");
                    }
                    adims_MODEL.mzqt mz1 = new adims_MODEL.mzqt();//自动生成氧气
                    mz1.Qtname = "氧气";
                    mz1.Yl = Convert.ToDouble(100);
                    mz1.Dw = "%";
                    mz1.Sysj = dtOtime.Value.AddMinutes(2);
                    mz1.Bz = 1;
                    int m = bll.addqt(mzjldID, mz1);
                    if (m > 0)
                    {
                        BindQtList();
                    }
                }
                else if (mzjldID != 0)
                {
                   // if (isSjll)
                   // dal.UpdateShoushujianinfo(5, mzjldID, patID, Oroom);//修改手术间状态信息为准备手术

                    jhxmIn.Clear();
                    
                    DataTable dtJhxm = bll.GetJhxm(mzjldID);
                    for (int i = 0; i < dtJhxm.Rows.Count; i++)
                    {
                        jhxmIn.Add(dtJhxm.Rows[i][0].ToString());
                    }
                    
                    DataTable dt = new DataTable();
                    txtMzjldid.Controls[0].Text = Convert.ToString(mzjldID);
                    DataTable dtMZ_Info = bll.SelectOldPatInfo(mzjldID);
                    otime = (Convert.ToDateTime(dtMZ_Info.Rows[0]["otime"]));
                    cmbSJJG.Text = Convert.ToString(dtMZ_Info.Rows[0]["jcsjjg"]);
                    jcsjjg = Convert.ToInt32(cmbSJJG.Text);
                    dtOdate.Value = otime;
                    BindShijiandian();//绑定时间坐标                
                    BindMzjldBacicInfo();
                    BindJikongTime();
                    BindMZSSCGBG();
                    BindQtList();
                    BindYdyList();
                    BindJmyList();
                    BindTsyy();
                    BindJHDian();
                    BindCLlist();
                    BindShuyeList();
                    BindShuxueList();
                    BindSZSJ();
                    BindCxList();
                    BindYllList();
                    BindClList();
                    CheckLocalAndServerData();//检查本地数据和服务器数据是否有遗漏
                    BindZhenTongYao();
                }
                if (isSjll)//如果进入是术间浏览，禁掉修改功能
                {
                    AddPointTSMenu.Enabled = false;
                    DeleteCGBGStripMenuItem.Enabled = false;
                    jkhxToolStripMenuItem.Enabled = false;
                    cmbSJJG.Enabled = false;
                    txtSqzd.Controls[0].DoubleClick -= new System.EventHandler(this.txtSqzd_DoubleClick);
                    txtSqyy.Controls[0].DoubleClick -= new System.EventHandler(this.txtSqyy_DoubleClick);
                    txtShoushuFS.Controls[0].DoubleClick -= new System.EventHandler(this.txtShoushuFS_DoubleClick);
                    txtMazuiFS.Controls[0].DoubleClick -= new System.EventHandler(this.txtMazuiFS_DoubleClick);
                    txtNssss.Controls[0].DoubleClick -= new System.EventHandler(this.txtNssss_DoubleClick);
                    txtMZYS.Controls[0].DoubleClick -= new System.EventHandler(this.txtMZYS_DoubleClick);
                    txtQXHS.Controls[0].DoubleClick -= new System.EventHandler(this.txtQXHS_DoubleClick);
                    txtXHHS.Controls[0].DoubleClick -= new System.EventHandler(this.txtXHHS_DoubleClick);
                    button11.Enabled = false;
                    pictureBox2.MouseDoubleClick -= new MouseEventHandler(pictureBox2_MouseDoubleClick);
                    pictureBox2.MouseMove -= new MouseEventHandler(pictureBox2_MouseMove);
                    pictureBox3.MouseMove -= new MouseEventHandler(pictureBox3_MouseMove);
                    btnSave.Enabled = false;
                    btnMonitor.Enabled = false;
                    cmbCOM.Enabled = false;
                    cmbJianhuyi.Enabled = false;
                    btnTsyy.Enabled = false;
                    btnSzsj.Enabled = false;
                    this.btnPrintView.Enabled = false;
                    lbMzjs.DoubleClick -= new EventHandler(lbMzjs_DoubleClick);
                    lbMzks.DoubleClick -= new EventHandler(lbMzks_DoubleClick);
                    lbSsjs.DoubleClick -= new EventHandler(lbSsjs_DoubleClick);
                    lbSsks.DoubleClick -= new EventHandler(lbSsks_DoubleClick);
                    lbBg.DoubleClick -= new EventHandler(lbBg_DoubleClick);
                    lbCg.DoubleClick -= new EventHandler(lbCg_DoubleClick);
                    lbCguan.MouseDown -= new MouseEventHandler(lb_cguan_MouseDown);
                    lbCguan.MouseMove -= new MouseEventHandler(lb_cguan_MouseMove);
                    lbCguan.MouseUp -= new MouseEventHandler(lb_cguan_MouseUp);
                    lbCguan.MouseLeave -= new EventHandler(lb_cguan_MouseLeave);
                    lbCguan.MouseDown -= new MouseEventHandler(lb_bguan_MouseDown);
                    lbCguan.MouseMove -= new MouseEventHandler(lb_bguan_MouseMove);
                    lbCguan.MouseUp -= new MouseEventHandler(lb_bguan_MouseUp);
                    lbCguan.MouseLeave -= new EventHandler(lb_bguan_MouseLeave);
                    lbMzjs1.MouseDown -= new MouseEventHandler(lbMzjs1_MouseDown);
                    lbMzjs1.MouseMove -= new MouseEventHandler(lbMzjs1_MouseMove);
                    lbMzjs1.MouseUp -= new MouseEventHandler(lbMzjs1_MouseUp);
                    lbMzjs1.MouseLeave -= new EventHandler(lbMzjs1_MouseLeave);
                    lbMzks1.MouseDown -= new MouseEventHandler(lbMzks1_MouseDown);
                    lbMzks1.MouseMove -= new MouseEventHandler(lbMzks1_MouseMove);
                    lbMzks1.MouseUp -= new MouseEventHandler(lbMzks1_MouseUp);
                    lbMzks1.MouseLeave -= new EventHandler(lbMzks1_MouseLeave);
                    lbSSJS.MouseDown -= new MouseEventHandler(ssjs1_MouseDown);
                    lbSSJS.MouseMove -= new MouseEventHandler(ssjs1_MouseMove);
                    lbSSJS.MouseUp -= new MouseEventHandler(ssjs1_MouseUp);
                    lbSSJS.MouseLeave -= new EventHandler(ssjs1_MouseLeave);
                    lbSSKS.MouseDown -= new MouseEventHandler(ssks1_MouseDown);
                    lbSSKS.MouseMove -= new MouseEventHandler(ssks1_MouseMove);
                    lbSSKS.MouseUp -= new MouseEventHandler(ssks1_MouseUp);
                    lbSSKS.MouseLeave -= new EventHandler(ssks1_MouseLeave);
                    lbCguan.MouseDown -= new MouseEventHandler(lb_cguan_MouseDown);
                    lbCguan.MouseMove -= new MouseEventHandler(lb_cguan_MouseMove);
                    lbCguan.MouseUp -= new MouseEventHandler(lb_cguan_MouseUp);
                    lbCguan.MouseLeave -= new EventHandler(lb_cguan_MouseLeave);
                    lbBguan.MouseDown -= new MouseEventHandler(lb_bguan_MouseDown);
                    lbBguan.MouseMove -= new MouseEventHandler(lb_bguan_MouseMove);
                    lbBguan.MouseUp -= new MouseEventHandler(lb_bguan_MouseUp);
                    lbBguan.MouseLeave -= new EventHandler(lb_bguan_MouseLeave);
                    lbMzjs1.MouseDown -= new MouseEventHandler(lbMzjs1_MouseDown);
                    lbMzjs1.MouseMove -= new MouseEventHandler(lbMzjs1_MouseMove);
                    lbMzjs1.MouseUp -= new MouseEventHandler(lbMzjs1_MouseUp);
                    lbMzjs1.MouseLeave -= new EventHandler(lbMzjs1_MouseLeave);
                    lbMzks1.MouseDown -= new MouseEventHandler(lbMzks1_MouseDown);
                    lbMzks1.MouseMove -= new MouseEventHandler(lbMzks1_MouseMove);
                    lbMzks1.MouseUp -= new MouseEventHandler(lbMzks1_MouseUp);
                    lbMzks1.MouseLeave -= new EventHandler(lbMzks1_MouseLeave);
                    lbSSJS.MouseDown -= new MouseEventHandler(ssjs1_MouseDown);
                    lbSSJS.MouseMove -= new MouseEventHandler(ssjs1_MouseMove);
                    lbSSJS.MouseUp -= new MouseEventHandler(ssjs1_MouseUp);
                    lbSSJS.MouseLeave -= new EventHandler(ssjs1_MouseLeave);
                    lbSSKS.MouseDown -= new MouseEventHandler(ssks1_MouseDown);
                    lbSSKS.MouseMove -= new MouseEventHandler(ssks1_MouseMove);
                    lbSSKS.MouseUp -= new MouseEventHandler(ssks1_MouseUp);
                    lbSSKS.MouseLeave -= new EventHandler(ssks1_MouseLeave);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "出现异常，请检查！");
            }
        }

        #region //绑定用药，时间，麻醉平面等数据
        private void BindQtList()//绑定气体列表
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
                    mzqtList[i].Id = Convert.ToInt32(dr["id"]);
                    mzqtList[i].Qtname = Convert.ToString(dr["qtname"]);
                    mzqtList[i].Yl = Convert.ToDouble(dr["yl"]);
                    mzqtList[i].Dw = Convert.ToString(dr["dw"]);
                    mzqtList[i].Sysj = Convert.ToDateTime(dr["sytime"]);
                    mzqtList[i].Bz = Convert.ToInt32(dr["flags"]);
                    if (mzqtList[i].Bz == 2)
                    {
                        mzqtList[i].Jssj = Convert.ToDateTime(dr["jstime"]);
                    }
                    i++;
                }
            }
        }

        private void BindShuyeList()//绑定输液列表
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
                    shuyeList[i].Id = Convert.ToInt32(dr["id"]);
                    shuyeList[i].Name = Convert.ToString(dr["shuyename"]);
                    shuyeList[i].Jl = Convert.ToDouble(dr["Jl"]);
                    syZongl = syZongl + shuyeList[i].Jl;
                    shuyeList[i].Dw = Convert.ToString(dr["dw"]);
                    shuyeList[i].Zrfs = Convert.ToString(dr["Zrfs"]);
                    shuyeList[i].Kssj = Convert.ToDateTime(dr["kssj"]);
                    shuyeList[i].Bz = Convert.ToInt32(dr["flags"]);
                    if (shuyeList[i].Bz == 2)
                    {
                        shuyeList[i].Jssj = Convert.ToDateTime(dr["jssj"]);
                    }
                    i++;
                }
            }
        }

        private void BindShuxueList()//绑定输血列表
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
                    shuxueList[i].Id = Convert.ToInt32(dr["id"]);
                    shuxueList[i].Name = Convert.ToString(dr["shuxuename"]);
                    shuxueList[i].Jl = Convert.ToDouble(dr["Jl"]);
                    sxZongl = sxZongl + shuxueList[i].Jl;
                    shuxueList[i].Dw = Convert.ToString(dr["dw"]);
                    shuxueList[i].Zrfs = Convert.ToString(dr["Zrfs"]);
                    shuxueList[i].Kssj = Convert.ToDateTime(dr["kssj"]);
                    shuxueList[i].Bz = Convert.ToInt32(dr["flags"]);
                    if (shuxueList[i].Bz == 2)
                    {
                        shuxueList[i].Jssj = Convert.ToDateTime(dr["jssj"]);
                    }
                    i++;
                }
            }
        }

        private void BindmzpmList()//绑定麻醉平面，盛泽用
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
                    mzpmList[i].Id = Convert.ToInt32(dr["id"]);
                    i++;
                }
            }
        }

        private void BindJmyList()//绑定局麻药
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
                    jmyList[i].Id = Convert.ToInt32(dr["id"]);
                    jmyList[i].Name = Convert.ToString(dr["name"]);
                    jmyList[i].Jl = Convert.ToDouble(dr["Jl"]);
                    if (Convert.ToInt32(dr["Cxyy"]) == 1)
                        jmyList[i].Cxyy = true;
                    else
                        jmyList[i].Cxyy = false;
                    jmyList[i].Dw = Convert.ToString(dr["dw"]);
                    jmyList[i].Zrfs = Convert.ToString(dr["Zrfs"]);
                    jmyList[i].Kssj = Convert.ToDateTime(dr["kssj"]);
                    jmyList[i].Bz = Convert.ToInt32(dr["flags"]);
                    if (jmyList[i].Bz == 2)
                    {
                        jmyList[i].Jssj = Convert.ToDateTime(dr["jssj"]);
                    }
                    i++;
                }
            }
        }

        private void BindYdyList()//绑定诱导药，包括全麻药
        {
            ydyList.Clear();
            DataTable dtYDY = bll.selectYDY(mzjldID);
            if (dtYDY.Rows.Count != 0)
            {
                int i = 0;
                ydyList.Clear();
                foreach (DataRow dr in dtYDY.Rows)
                {
                    adims_MODEL.mzyt yt1 = new adims_MODEL.mzyt();
                    ydyList.Add(yt1);
                    ydyList[i].Id = Convert.ToInt32(dr["id"]);
                    ydyList[i].Ytname = Convert.ToString(dr["ydyname"]);
                    ydyList[i].Yl = Convert.ToDouble(dr["yl"]);
                    ydyList[i].Dw = Convert.ToString(dr["dw"]);
                    ydyList[i].Yyfs = Convert.ToString(dr["Yyfs"]);
                    if (Convert.ToInt32(dr["Cxyy"]) == 1)
                        ydyList[i].Cxyy = true;
                    else
                        ydyList[i].Cxyy = false;
                    ydyList[i].Sysj = Convert.ToDateTime(dr["sytime"]);
                    ydyList[i].Bz = Convert.ToInt32(dr["flags"]);
                    if (ydyList[i].Bz == 2)
                    {
                        ydyList[i].Jssj = Convert.ToDateTime(dr["jstime"]);
                    }
                    i++;
                }
            }
        }

        private void BindZhenTongYao()//绑定镇痛药
        {
            listBox3.Items.Clear();
            DataTable dt = bll.getMZZTY(mzjldID);
            foreach (DataRow dr in dt.Rows)
            {
                listBox3.Items.Add(dr["name"] + " " + dr["yl"] + dr["dw"]);
            }
        }

        private void BindSZSJ()//绑定术中事件
        {
            szsjList.Clear();
            DataTable dtSZSJ = bll.GETALLSZSJ(mzjldID);
            if (dtSZSJ.Rows.Count != 0)
            {
                int i = 0;
                foreach (DataRow dr in dtSZSJ.Rows)
                {
                    adims_MODEL.szsj yt1 = new adims_MODEL.szsj();
                    szsjList.Add(yt1);
                    szsjList[i].Id = Convert.ToInt32(dr["id"]);
                    szsjList[i].Name = Convert.ToString(dr["Name"]);
                    szsjList[i].D = Convert.ToDateTime(dr["time"]);
                    i++;
                }
            }
            listBox1.Items.Clear();
            for (int i = 0; i < szsjList.Count; i++)
            {
                listBox1.Items.Add((i + 1).ToString() + "." + szsjList[i].Name + " " + szsjList[i].D.ToString("HH:mm"));
            }
        }

        private void BindTsyy()
        {
            tsyyList.Clear();
            DataTable dt = bll.getTSYY(mzjldID);
            int i = 0;
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    adims_MODEL.tsyy yt1 = new adims_MODEL.tsyy();
                    tsyyList.Add(yt1);
                    tsyyList[i].Id = Convert.ToInt32(dr["id"]);
                    tsyyList[i].Name = Convert.ToString(dr["name"]);
                    tsyyList[i].Yl = Convert.ToDouble(dr["yl"]);
                    tsyyList[i].Dw = Convert.ToString(dr["dw"]);
                    tsyyList[i].Yyfs = Convert.ToString(dr["yyfs"]);
                    tsyyList[i].D = Convert.ToDateTime(dr["time"]);
                    i++;
                }
            }
            int i1 = 1;
            listBox2.Items.Clear();
            foreach (adims_MODEL.tsyy s in tsyyList)
            {
                listBox2.Items.Add(i1.ToString() + ". " + s.Name + " " + s.Yl.ToString() + s.Dw + s.Yyfs);
                i1++;
            }
        }
        #endregion

        

        #region //双击事件

        private void txtSqyy_DoubleClick(object sender, EventArgs e)/// 术前用药双击事件
        {
            addSqyy sqyyform = new addSqyy(txtSqyy, mzjldID);
            sqyyform.ShowDialog();
        }
        private void txtHbz_DoubleClick(object sender, EventArgs e) /// 合并症双击事件
        {
            //hbz hbzform = new hbz(txtXueXing, mzjldID);
            //hbzform.ShowDialog();
        }

        private void txtSqzd_DoubleClick(object sender, EventArgs e)// 术前诊断双击事件
        {
            SelectZhenDuan szzdform = new SelectZhenDuan(txtSqzd, mzjldID);
            szzdform.ShowDialog();
        }
        private void txtShoushuFS_DoubleClick(object sender, EventArgs e)//手术方式双击事件
        {
            SelectShoushu yssssform = new SelectShoushu(txtShoushuFS, patID);
            yssssform.ShowDialog();
        }
        private void txtMazuiFS_DoubleClick(object sender, EventArgs e)//麻醉方式双击事件
        {
            SelectMZFF mzffform = new SelectMZFF(txtMazuiFS, mzjldID);
            mzffform.ShowDialog();
        }
        private void txtNssss_DoubleClick(object sender, EventArgs e)/// 拟实施手术双击事件
        {
            SelectShoushu yssssform = new SelectShoushu(txtNssss, patID);
            yssssform.ShowDialog();
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

        private void btnMonitor_Click(object sender, EventArgs e)
        {
            #region //分类型采集
            if (btnMonitor.Text.Equals("开始监测"))
            {
                if (cmbJianhuyi.Text.IsNullOrEmpty())
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
                    }
                    _serialPort.Open();
                    timer4.Enabled = true;
                    cmbJianhuyi.Enabled = false;
                    cmbCOM.Enabled = false;

                }
                if (cmbJianhuyi.Text == "金科威监护仪")
                {
                    //timerJKW.Start();
                    JkwDataResolve();
                    cmbJianhuyi.Enabled = false;
                    cmbCOM.Enabled = false;
                }
                if (cmbJianhuyi.Text == "飞利浦监护仪")
                {
                    FillipData();
                    cmbJianhuyi.Enabled = false;
                    cmbCOM.Enabled = false;
                }
                if (cmbJianhuyi.Text == "迈瑞监护仪")
                {
                    timer_Miray.Start();
                    MaintainTimer.Start();
                    cmbJianhuyi.Enabled = false;
                    cmbCOM.Enabled = false;
                    MirayT6DataResolve();

                }
                if (cmbJianhuyi.Text == "理邦监护仪")
                {
                    this.timer_LB.Start();
                }
                SaveMzjld();
                ksjcTime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                btnMonitor.Text = "结束监测";
                timer1.Interval = 10 * 1000;
                timer1.Start();
            }
            #endregion
            else
            {
                if (DialogResult.OK == MessageBox.Show("确定结束检测吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                {
                    timer_Miray.Stop();
                    timer_LB.Stop();
                    MaintainTimer.Stop();
                    timer4.Enabled = false;
                    timer1.Enabled = false;
                    timerbis.Enabled = false;
                    timerJKW.Stop();
                    if (serialPort1.IsOpen)
                    {
                       
                        serialPort1.Close();
                    }
                  
                    if (_serialPort.IsOpen)
                    {
                        _serialPort.StopTransfer();
                        _serialPort.Close();
                    }
                  
                    if (ThreadExist)
                    {
                        ThreadExist = false;
                        Receiving_xy.Abort();//强制结束线程运行
                        Receiving_xy = null; ////在.NET中，有GC垃圾回收，如果这里不赋为null，不回收内存，甚至永远这么占着，导致内存泄漏。
                    }
                    if (ThreadExist_JKW)
                    {
                        if (ServerSocket_JKW != null)
                            ServerSocket_JKW.Close();
                        if (Receiving_JKW.ThreadState != System.Threading.ThreadState.Running)
                        {
                            Receiving_JKW.Abort();
                        }
                        Receiving_JKW.Abort();
                        Receiving_JKW = null;
                        ThreadExist_JKW = false;
                    }
                    if (isexist_Miray)
                    {
                        if (ClientSocket_Miray != null)
                            ClientSocket_Miray.Close();
                        if (SocketThread_Miray.ThreadState != System.Threading.ThreadState.Running)
                        {
                            SocketThread_Miray.Abort();
                        }
                        isexist_Miray = false;
                    }
                    btnMonitor.Text = "开始监测";
                    cmbJianhuyi.Enabled = true;
                    cmbCOM.Enabled = true;
                }
            }
        }



        #region //GE监护函数
        private void timer4_Tick(object sender, EventArgs e)
        {
            timer4.Interval = 3000 * 10;
            //if (cheboxBIS.Checked == true)
            //{
            //    ccccbis();
            //}
            try
            {
                if (_serialPort.OSIsUnix())
                {
                    dataEvent += new EventHandler(delegate(object senderGE, EventArgs eGE)
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
                (sender as DSerialPort).ReadBuffer(mzjldID, 0,rrc);
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

        int FindField(Byte[] str, Byte field1, Byte field2)
        {
            int position = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == field1)
                {
                    if (i < str.Length - 1)
                    {
                        if (str[i + 1] == field2)
                        {
                            position = i;
                            return position;
                        }

                    }
                }
            }
            return position;
        }
        private void FillipData()
        {
            try
            {
                bool TimerStarted = false;
                string temp1 = tbIpAdress.Text;
                //string temp2 = this.txtMzjldid.Controls[0].Text;
                IPAddressInput1 = tbIpAdress.Text;
                //BedIDInput1 = this.txtMzjldid.Controls[0].Text;
                if (ThreadExist)
                {
                    ThreadExist = false;
                    Receiving_xy.Abort();//强制结束线程运行
                    Receiving_xy = null; ////在.NET中，有GC垃圾回收，如果这里不赋为null，不回收内存，甚至永远这么占着，导致内存泄漏。
                }
                if (!TimerStarted)
                {
                    CheckTimer.Start();
                    TimerStarted = true;
                }
                SaveConfigure();
                GetConfigure();
                if (!ThreadExist)
                {
                    Receiving_xy = new Thread(GatherData);
                    Receiving_xy.Start();
                    ThreadExist = true;
                    //Receiving_xy.Abort();//强制结束线程运行
                    //Receiving_xy = null; ////在.NET中，有GC垃圾回收，如果这里不赋为null，有可能老半天也不回收内存，甚至永远这么占着，导致内存泄漏。
                }
            }
            catch(Exception ex)
            {
            MessageBox.Show("1."+ex);
            }
        }
        private delegate void setText();//定义一个线程委托
        public void GatherData()
        {
            try
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

                    PR = 0; pr_effective = false;    //脉率
                    RR = 0; rr_effective = false;    //呼吸频率
                    SPO2 = 0; spo2_effective = false;//血氧饱和度
                    TEMP = 0; temp_effective = false;//温度
                    HR = 0; hr_effective = false;    //心率
                    SYS = 0; sys_effective = false; //收缩压
                    DIA = 0; dia_effective = false; //舒张压
                    MAP = 0; map_effective = false; //平均血压
                    CVP = 0; CVP_effective = false; //中心静脉压
                    CVP_SYS = 0; CVP_sys_effective = false; //中心静脉收缩压
                    CVP_DIA = 0; CVP_dia_effective = false; //中心静脉舒张压
                    CVP_MAP = 0; CVP_map_effective = false; //中心静脉平均血压
                    ABP_SYS = 0; ABP_sys_effective = false; //动脉收缩压
                    ABP_DIA = 0; ABP_dia_effective = false; //动脉舒张压
                    ABP_MAP = 0; ABP_map_effective = false; //动脉平均血压
                    ART_SYS = 0; ART_sys_effective = false; //动脉收缩压
                    ART_DIA = 0; ART_dia_effective = false; //动脉舒张压
                    ART_MAP = 0; ART_map_effective = false; //动脉平均血压


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
                  //  logstr = "Send Association Request : " + MeasureDate.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + BitConverter.ToString(AssocReq).Replace("-", ",0x");

                    //Thread.Sleep(Waiting_Period);

                    //SaveLog(logstr);

                    recv_Length = 0;
                    recv_Length = server.ReceiveFrom(recv_Data, ref phillip);

                  //  logstr = "Received Association Result: " + BitConverter.ToString(recv_Data, 0, recv_Length).Replace("-", ",0x");
                   // SaveLog(logstr);
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
                            logstr = "Received MSD Create Event Report : " + BitConverter.ToString(recv_Data, 0, recv_Length).Replace("-", ",0x");
                           // SaveLog(logstr);


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
                           // logstr = "Send MSD Create Event Result: " + BitConverter.ToString(event_result).Replace("-", ",0x");
                           // SaveLog(logstr);

                            Thread.Sleep(1000);



                        }
                        //if (recv_Data[0] == 0x0C) MessageBox.Show("收到拒绝联系报文！");
                        //if (recv_Data[0] == 0x19) MessageBox.Show("收到丢弃联系报文！");
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
                       // logstr = "Send single pool ext: " + BitConverter.ToString(single_poll_exs).Replace("-", ",0x");
                       // SaveLog(logstr);
                        //Thread.Sleep(1000);
                        recv_Length = server.ReceiveFrom(recv_Data, ref phillip);
                       // logstr = "Received single pool ext response1: " + BitConverter.ToString(recv_Data, 0, recv_Length).Replace("-", ",0x");
                       // SaveLog(logstr);
                        //处理第一个消息， 该消息是一个ROLRS_APDU消息
                        if ((recv_Length > 0) && (recv_Data[4] == 0x00) && (recv_Data[5] == 0x02))
                        {
                            // Pillin new code
                            int Fieldposition = 0;
                            //PR 0x4822
                            Fieldposition = FindField(recv_Data, 0x48, 0x22);
                            if (Fieldposition > 0)
                            {
                                physio_id = recv_Data[Fieldposition] * 256 + recv_Data[Fieldposition + 1];
                                pr_effective = recv_Data[Fieldposition + 2] == 0 ? true : false;
                                if (pr_effective)
                                    PR = (int)getFloat(recv_Data, Fieldposition + 6);
                            }
                            //RR 0x500A
                            Fieldposition = FindField(recv_Data, 0x50, 0x0A);
                            if (Fieldposition > 0)
                            {
                                physio_id = recv_Data[Fieldposition] * 256 + recv_Data[Fieldposition + 1];
                                rr_effective = recv_Data[Fieldposition + 2] == 0 ? true : false;
                                if (rr_effective)
                                    RR = (int)getFloat(recv_Data, Fieldposition + 6);
                            }
                            //awRR 0x5012
                            Fieldposition = FindField(recv_Data, 0x50, 0x12);
                            if (Fieldposition > 0)
                            {
                                physio_id = recv_Data[Fieldposition] * 256 + recv_Data[Fieldposition + 1];
                                awrr_effective = recv_Data[Fieldposition + 2] == 0 ? true : false;
                                if (awrr_effective)
                                    awRR = (int)getFloat(recv_Data, Fieldposition + 6);
                            }
                            //EtCO2 0x50B0 
                            Fieldposition = FindField(recv_Data, 0x50, 0xB0);
                            if (Fieldposition > 0)
                            {
                                physio_id = recv_Data[Fieldposition] * 256 + recv_Data[Fieldposition + 1];
                                etco2_effective = recv_Data[Fieldposition + 2] == 0 ? true : false;
                                if (etco2_effective)
                                    ETCO2 = (int)getFloat(recv_Data, Fieldposition + 6);
                            }

                            //SPO2 0x4BB8
                            Fieldposition = FindField(recv_Data, 0x4B, 0xB8);
                            if (Fieldposition > 0)
                            {
                                physio_id = recv_Data[Fieldposition] * 256 + recv_Data[Fieldposition + 1];
                                spo2_effective = recv_Data[Fieldposition + 2] == 0 ? true : false;
                                if (spo2_effective)
                                    SPO2 = (int)getFloat(recv_Data, Fieldposition + 6);
                            }
                            //TEMP 0x4B60
                            Fieldposition = FindField(recv_Data, 0x4B, 0x60);
                            if (Fieldposition > 0)
                            {
                                physio_id = recv_Data[Fieldposition] * 256 + recv_Data[Fieldposition + 1];
                                temp_effective = recv_Data[Fieldposition + 2] == 0 ? true : false;
                                if (temp_effective)
                                    TEMP = (int)getFloat(recv_Data, Fieldposition + 6);
                            }
                            //HR 0x4182
                            Fieldposition = FindField(recv_Data, 0x41, 0x82);
                            if (Fieldposition > 0)
                            {
                                physio_id = recv_Data[Fieldposition] * 256 + recv_Data[Fieldposition + 1];
                                hr_effective = recv_Data[Fieldposition + 2] == 0 ? true : false;
                                if (hr_effective)
                                    HR = (int)getFloat(recv_Data, Fieldposition + 6);
                            }

                            //NBP SYS 0x4A05
                            Fieldposition = FindField(recv_Data, 0x4A, 0x05);
                            if (Fieldposition > 0)
                            {
                                physio_id = recv_Data[Fieldposition] * 256 + recv_Data[Fieldposition + 1];
                                sys_effective = recv_Data[Fieldposition + 2] == 0 ? true : false;
                                if (sys_effective)
                                    SYS = (int)getFloat(recv_Data, Fieldposition + 6);
                            }
                            //NBP DIA 0x4A06
                            Fieldposition = FindField(recv_Data, 0x4A, 0x06);
                            if (Fieldposition > 0)
                            {
                                physio_id = recv_Data[Fieldposition] * 256 + recv_Data[Fieldposition + 1];
                                dia_effective = recv_Data[Fieldposition + 2] == 0 ? true : false;
                                if (dia_effective)
                                    DIA = (int)getFloat(recv_Data, Fieldposition + 6);
                            }
                            //NBP MAP 0x4A07
                            Fieldposition = FindField(recv_Data, 0x4A, 0x07);
                            if (Fieldposition > 0)
                            {
                                physio_id = recv_Data[Fieldposition] * 256 + recv_Data[Fieldposition + 1];
                                map_effective = recv_Data[Fieldposition + 2] == 0 ? true : false;
                                if (map_effective)
                                    MAP = (int)getFloat(recv_Data, Fieldposition + 6);
                            }

                            //CVP 0x4A44
                            Fieldposition = FindField(recv_Data, 0x4A, 0x44);
                            if (Fieldposition > 0)
                            {
                                physio_id = recv_Data[Fieldposition] * 256 + recv_Data[Fieldposition + 1];
                                CVP_effective = recv_Data[Fieldposition + 2] == 0 ? true : false;
                                if (CVP_effective)
                                    CVP = (int)getFloat(recv_Data, Fieldposition + 6);
                            }
                            //CVP SYS 0x4A45
                            Fieldposition = FindField(recv_Data, 0x4A, 0x45);
                            if (Fieldposition > 0)
                            {
                                physio_id = recv_Data[Fieldposition] * 256 + recv_Data[Fieldposition + 1];
                                CVP_sys_effective = recv_Data[Fieldposition + 2] == 0 ? true : false;
                                if (CVP_sys_effective)
                                    CVP_SYS = (int)getFloat(recv_Data, Fieldposition + 6);
                            }
                            //CVP DIA 0x4A46
                            Fieldposition = FindField(recv_Data, 0x4A, 0x46);
                            if (Fieldposition > 0)
                            {
                                physio_id = recv_Data[Fieldposition] * 256 + recv_Data[Fieldposition + 1];
                                CVP_dia_effective = recv_Data[Fieldposition + 2] == 0 ? true : false;
                                if (CVP_dia_effective)
                                    CVP_DIA = (int)getFloat(recv_Data, Fieldposition + 6);
                            }
                            //NBP MAP 0x4A47
                            Fieldposition = FindField(recv_Data, 0x4A, 0x47);
                            if (Fieldposition > 0)
                            {
                                physio_id = recv_Data[Fieldposition] * 256 + recv_Data[Fieldposition + 1];
                                CVP_map_effective = recv_Data[Fieldposition + 2] == 0 ? true : false;
                                if (CVP_map_effective)
                                    CVP_MAP = (int)getFloat(recv_Data, Fieldposition + 6);
                            }


                            //ABP SYS 0x4A15
                            Fieldposition = FindField(recv_Data, 0x4A, 0x15);
                            if (Fieldposition > 0)
                            {
                                physio_id = recv_Data[Fieldposition] * 256 + recv_Data[Fieldposition + 1];
                                ABP_sys_effective = recv_Data[Fieldposition + 2] == 0 ? true : false;
                                if (ABP_sys_effective)
                                    ABP_SYS = (int)getFloat(recv_Data, Fieldposition + 6);
                            }
                            //ABP DIA 0x4A16
                            Fieldposition = FindField(recv_Data, 0x4A, 0x16);
                            if (Fieldposition > 0)
                            {
                                physio_id = recv_Data[Fieldposition] * 256 + recv_Data[Fieldposition + 1];
                                ABP_dia_effective = recv_Data[Fieldposition + 2] == 0 ? true : false;
                                if (ABP_dia_effective)
                                    ABP_DIA = (int)getFloat(recv_Data, Fieldposition + 6);
                            }
                            //ABP MAP 0x4A17
                            Fieldposition = FindField(recv_Data, 0x4A, 0x17);
                            if (Fieldposition > 0)
                            {
                                physio_id = recv_Data[Fieldposition] * 256 + recv_Data[Fieldposition + 1];
                                ABP_map_effective = recv_Data[Fieldposition + 2] == 0 ? true : false;
                                if (ABP_map_effective)
                                    ABP_MAP = (int)getFloat(recv_Data, Fieldposition + 6);
                            }

                            //ART SYS 0x4A11
                            Fieldposition = FindField(recv_Data, 0x4A, 0x11);
                            if (Fieldposition > 0)
                            {
                                physio_id = recv_Data[Fieldposition] * 256 + recv_Data[Fieldposition + 1];
                                ART_sys_effective = recv_Data[Fieldposition + 2] == 0 ? true : false;
                                if (ART_sys_effective)
                                    ART_SYS = (int)getFloat(recv_Data, Fieldposition + 6);
                            }
                            //ART DIA 0x4A12
                            Fieldposition = FindField(recv_Data, 0x4A, 0x12);
                            if (Fieldposition > 0)
                            {
                                physio_id = recv_Data[Fieldposition] * 256 + recv_Data[Fieldposition + 1];
                                ART_dia_effective = recv_Data[Fieldposition + 2] == 0 ? true : false;
                                if (ART_dia_effective)
                                    ART_DIA = (int)getFloat(recv_Data, Fieldposition + 6);
                            }
                            //ART MAP 0x4A13
                            Fieldposition = FindField(recv_Data, 0x4A, 0x13);
                            if (Fieldposition > 0)
                            {
                                physio_id = recv_Data[Fieldposition] * 256 + recv_Data[Fieldposition + 1];
                                ART_map_effective = recv_Data[Fieldposition + 2] == 0 ? true : false;
                                if (ART_map_effective)
                                    ART_MAP = (int)getFloat(recv_Data, Fieldposition + 6);
                            }

                            // Measure time 0x0990
                            Fieldposition = FindField(recv_Data, 0x09, 0x90);//Attribute ID: Absolute Time Stamp NOM_ATTR_TIME_STAMP_ABS
                            //                      if (recv_Data[contemp3] == 0x09 && recv_Data[contemp3 + 1] == 0x90)//Attribute ID: Absolute Time Stamp NOM_ATTR_TIME_STAMP_ABS
                            if (Fieldposition > 0)
                            { //时间字段  BCD格式， 需要转化为十进制
                                contemp4 = Fieldposition + 4;

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
                            //this.Invoke(d);


                            HasEffectiveData = hr_effective || rr_effective || pr_effective || temp_effective || spo2_effective || sys_effective || dia_effective || map_effective || CVP_map_effective || ABP_map_effective;
                            HasEffectivePressure = map_effective || CVP_map_effective || ABP_map_effective;

                          //  logstr = "HR=" + HR.ToString() + ";RR=" + RR.ToString() + ";SPO2=" + SPO2.ToString() + ";sys=" + SYS.ToString() + ";DIA=" + DIA.ToString() + ";MAP=" + MAP.ToString() + ";CVP_MAP=" + CVP_MAP.ToString() + ";ABP_MAP=" + ABP_MAP.ToString();
                           // SaveLog(logstr);

                        }

                        /*                    if (recv_Length > 0 && recv_Data[4] == 0x00 && recv_Data[5] == 0x02)  //第一条消息收到，有后续存在的消息要接收， 第二次接受
                                            {
                                                //第二个消息字符串， 里面有几个字节， 但是是空的
                                               // Thread.Sleep(Waiting_Period);
                                                recv_Length = server.ReceiveFrom(recv_Data, ref phillip);
                                                logstr = "Received single pool ext response2: " + BitConverter.ToString(recv_Data, 0, recv_Length).Replace("-", ",0x");
                                                SaveLog(logstr);
                                            }

                                            if (recv_Length > 0 && recv_Data[4] == 0x00 && recv_Data[5] == 0x02)  //准备接受最后一个消息内容。
                                            {
                                                //接受第三个消息字符串， 里面有几个字节， 但是没有什么内容，不予处理
                                                //Thread.Sleep(Waiting_Period);
                                                recv_Length = server.ReceiveFrom(recv_Data, ref phillip);
                                                logstr = "Received single pool ext response3: " + BitConverter.ToString(recv_Data, 0, recv_Length).Replace("-", ",0x");
                                                SaveLog(logstr);
                                            }*/
                    } //End While
                    if (HasEffectiveData)
                    {

                        //  System.DateTime currentTime = new System.DateTime();
                        //  currentTime = System.DateTime.Now;


                        PatientDataBlock PatientData;


                        PatientData.MeasureTime = MeasureDate;
                        PatientData.PR = PR; PatientData.pr_effective = pr_effective;    //脉率
                        PatientData.RR = RR; PatientData.rr_effective = rr_effective;    //呼吸频率
                        PatientData.awRR = awRR; PatientData.awrr_effective = awrr_effective;    //aw呼吸频率
                        PatientData.ETCO2 = ETCO2; PatientData.etco2_effective = etco2_effective;    //etCO2 = ETCO2??
                        PatientData.SPO2 = SPO2; PatientData.spo2_effective = spo2_effective;//血氧饱和度
                        PatientData.TEMP = TEMP; PatientData.temp_effective = temp_effective;//温度
                        PatientData.HR = HR; PatientData.hr_effective = hr_effective;    //心率
                        PatientData.SYS = SYS; PatientData.sys_effective = sys_effective; //收缩压
                        PatientData.DIA = DIA; PatientData.dia_effective = dia_effective; //舒张压
                        PatientData.MAP = MAP; PatientData.map_effective = map_effective; //平均血压
                        PatientData.CVP = CVP; PatientData.CVP_effective = CVP_effective; //中心静脉压
                        PatientData.CVP_SYS = CVP_SYS; PatientData.CVP_sys_effective = CVP_sys_effective; //中心静脉收缩压
                        PatientData.CVP_DIA = CVP_DIA; PatientData.CVP_dia_effective = CVP_dia_effective; //中心静脉舒张压
                        PatientData.CVP_MAP = CVP_MAP; PatientData.CVP_map_effective = CVP_map_effective; //中心静脉平均血压
                        PatientData.ABP_SYS = ABP_SYS; PatientData.ABP_sys_effective = ABP_sys_effective; //动脉收缩压
                        PatientData.ABP_DIA = ABP_DIA; PatientData.ABP_dia_effective = ABP_dia_effective; //动脉舒张压
                        PatientData.ABP_MAP = ABP_MAP; PatientData.ABP_map_effective = ABP_map_effective; //动脉平均血压
                        PatientData.ART_SYS = ART_SYS; PatientData.ART_sys_effective = ART_sys_effective; //动脉收缩压
                        PatientData.ART_DIA = ART_DIA; PatientData.ART_dia_effective = ART_dia_effective; //动脉舒张压
                        PatientData.ART_MAP = ART_MAP; PatientData.ART_map_effective = ART_map_effective; //动脉平均血压

                     //   logstr = "Final Saved Data: Time=" + MeasureDate.ToString("yyyy-MM-dd HH:mm:ss") + ";HR=" + HR.ToString() + ";RR=" + RR.ToString() + ";awRR=" + awRR.ToString() + ";ETCO2=" + ETCO2.ToString() + ";SPO2=" + SPO2.ToString() + ";sys=" + SYS.ToString() + ";DIA=" + DIA.ToString() + ";MAP=" + MAP.ToString() + ";CVP_MAP=" + CVP_MAP.ToString() + ";ABP_MAP=" + ABP_MAP.ToString();
                        //string logstr = "HR=" + HR.ToString() + ";RR=" + RR.ToString() + ";awRR=" + awRR.ToString() + ";ETCO2=" + ETCO2.ToString() + ";SPO2=" + SPO2.ToString() + ";sys=" + SYS.ToString() + ";DIA=" + DIA.ToString() + ";MAP=" + MAP.ToString() + ";CVP_MAP=" + CVP_MAP.ToString() + ";ABP_MAP=" + ABP_MAP.ToString();
                        //SaveLog(logstr);
                        //SaveData(PatientData);
                        this.SaveFillipData1(PatientData, mzjldID, 0);
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
                    //SaveLog(logstr);
                    // Thread.Sleep(Waiting_Period);
                    recv_Length = server.ReceiveFrom(recv_Data, ref phillip);
                  //  logstr = "Received Associate Release Result: " + BitConverter.ToString(recv_Data, 0, recv_Length).Replace("-", ",0x");
                   // SaveLog(logstr);

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
                        // Associate_Abort
                       // MessageBox.Show("收到连接中断消息！");
                    }
                    else
                    {
                        //收到无法识别的消息

                     //   MessageBox.Show("收到无法识别消息！");
                    }
                    Thread.Sleep(Get_Period);
                    GetDataTimes++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("2"+ex);
            }
        }
        public struct PatientDataBlock
        {
            public DateTime MeasureTime;
            public int PR; public bool pr_effective;    //脉率
            public int RR; public bool rr_effective;    //呼吸频率
            public int awRR; public bool awrr_effective;    //aw呼吸频率
            public int ETCO2; public bool etco2_effective;    //ETCO2呼末二氧化碳
            public float SPO2; public bool spo2_effective;//血氧饱和度
            public float TEMP; public bool temp_effective;//温度
            public int HR; public bool hr_effective;    //心率
            public int SYS; public bool sys_effective; //收缩压
            public int DIA; public bool dia_effective; //舒张压
            public int MAP; public bool map_effective; //平均血压
            public int CVP; public bool CVP_effective; //中心静脉压
            public int CVP_SYS; public bool CVP_sys_effective; //中心静脉收缩压
            public int CVP_DIA; public bool CVP_dia_effective; //中心静脉舒张压
            public int CVP_MAP; public bool CVP_map_effective; //中心静脉平均血压
            public int ABP_SYS; public bool ABP_sys_effective; //动脉收缩压
            public int ABP_DIA; public bool ABP_dia_effective; //动脉舒张压
            public int ABP_MAP; public bool ABP_map_effective; //动脉平均血压
            public int ART_SYS; public bool ART_sys_effective; //动脉收缩压
            public int ART_DIA; public bool ART_dia_effective; //动脉舒张压
            public int ART_MAP; public bool ART_map_effective; //动脉平均血压

        }

        public void SaveFillipData1(PatientDataBlock PatientInfo, int mzid, int type)
        {
            try
            {
                int PR1 = PatientInfo.PR;
                int RR1 = 0;
                if (PatientInfo.awRR != 0)
                {
                    RR = PatientInfo.awRR;
                }
                else
                {
                    if (PatientInfo.RR != 0)
                    {
                        RR = PatientInfo.RR;
                    }
                }
                if (PatientInfo.awRR == 0)
                {
                    RR = SPO2/4;
                }
                int SPO21 = (int)PatientInfo.SPO2;
                int ETCO21 = PatientInfo.ETCO2;
                double TEMP1 = PatientInfo.TEMP;
                if (TEMP1 == 0)
                {
                    TEMP1 = 36.7;
                
                }
                int HR1 = PatientInfo.HR;
                int SYS1 = PatientInfo.SYS;
                int DIA1 = PatientInfo.DIA;
                int MAP1 = PatientInfo.MAP;
                int CVP_DIA1 = PatientInfo.CVP_DIA;
                int CVP_SYS1 = PatientInfo.CVP_SYS;
                int CVP_MAP1 = PatientInfo.CVP_MAP;
                int ABP_SYS1 = PatientInfo.ABP_SYS;
                int ABP_DIA1 = PatientInfo.ABP_DIA;
                int ABP_MAP1 = PatientInfo.ABP_MAP;
                string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                if (sqlite.selectJianCeData(now, mzjldID, type).Rows.Count == 0)
                {
                    int fa = 0;
                    if (ABP_SYS1 != 0 && ABP_DIA1 != 0 && ABP_MAP1 != 0)
                    {
                        if (type == 0)
                            fa = sqlite.insertJianCeDataMZJLD(mzjldID, ABP_SYS, ABP_DIA, ABP_MAP, RR, HR, PR, SPO2, ETCO2, TEMP1,CVP_MAP1, now);
                        if (type == 1)
                            fa = sqlite.insertJianCeDataPACU(mzjldID, ABP_SYS, ABP_DIA, ABP_MAP, RR, HR, PR, SPO2, ETCO2, TEMP1, now);
                    }
                    else
                    {
                        if (type == 0)
                            fa = sqlite.insertJianCeDataMZJLD(mzjldID, SYS1, DIA1, MAP1, RR, HR, PR, SPO2, ETCO2, TEMP1,CVP_MAP1, now);
                        if (type == 1)
                            fa = sqlite.insertJianCeDataPACU(mzjldID, SYS, DIA, MAP, RR, HR, PR, SPO2, ETCO2, TEMP1, now);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("3"+ex);
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
            FileStream fs = new FileStream(Application.StartupPath + "\\Config.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.WriteLine(IpConf.PatientIPAddress);
            sw.WriteLine(IpConf.BedID);
            sw.Close();
            fs.Close();
        }
        private void GetConfigure()
        {
            IPConfigureInfo IpConf;
            FileStream fs = new FileStream(Application.StartupPath + "\\Config.txt", FileMode.Open);
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


        #region //飞利浦监护函数

            //private void FillipData()
            //{
            //    bool TimerStarted = false;
            //    string temp1 = tbIpAdress.Text;
            //    //string temp2 = this.txtMzjldid.Controls[0].Text;
            //    IPAddressInput1 = tbIpAdress.Text;
            //    //BedIDInput1 = this.txtMzjldid.Controls[0].Text;
            //    if (ThreadExist)
            //    {
            //        ThreadExist = false;
            //        Receiving_xy.Abort();//强制结束线程运行
            //        Receiving_xy = null; ////在.NET中，有GC垃圾回收，如果这里不赋为null，不回收内存，甚至永远这么占着，导致内存泄漏。
            //    }
            //    if (!TimerStarted)
            //    {
            //        CheckTimer.Start();
            //        TimerStarted = true;
            //    }
            //    SaveConfigure();
            //    GetConfigure();
            //    if (!ThreadExist)
            //    {
            //        Receiving_xy = new Thread(GatherData);
            //        Receiving_xy.Start();
            //        ThreadExist = true;
            //        //Receiving_xy.Abort();//强制结束线程运行
            //        //Receiving_xy = null; ////在.NET中，有GC垃圾回收，如果这里不赋为null，有可能老半天也不回收内存，甚至永远这么占着，导致内存泄漏。
            //    }
            //}
            //private delegate void setText();//定义一个线程委托
            //public void GatherData()
            //{
            //    int i = 0;
            //    int j = 0;
            //    int k = 0;
            //    int t = 0;
            //    //   Socket server;
            //    // ip = new IPEndPoint(IPAddress.Parse(ip_txt.Text.Trim()), 24105);
            //    //定义网络类型，数据连接类型和网络协议UDP

            //    string temp1 = IPAddressInput1;
            //    while (true)
            //    {
            //        PR = 0; pr_effective = false;    //脉率
            //        RR = 0; rr_effective = false;    //呼吸频率
            //        SPO2 = 0; spo2_effective = false;//血氧饱和度
            //        TEMP1 = 0; temp_effective = false;//温度
            //        HR = 0; hr_effective = false;    //心率
            //        SYS = 0; sys_effective = false; //收缩压
            //        DIA = 0; dia_effective = false; //舒张压
            //        MAP = 0; map_effective = false; //平均血压
            //        CVP_SYS = 0; CVP_sys_effective = false; //中心静脉收缩压
            //        CVP_DIA = 0; CVP_dia_effective = false; //中心静脉舒张压
            //        CVP_MAP = 0; CVP_map_effective = false; //中心静脉平均血压
            //        ABP_SYS = 0; ABP_sys_effective = false; //动脉收缩压
            //        ABP_DIA = 0; ABP_dia_effective = false; //动脉舒张压
            //        ABP_MAP = 0; ABP_map_effective = false; //动脉平均血压
            //        MeasureDate = System.DateTime.Now;
            //        //setText d = new setText(DisplayAllData);
            //        //this.Invoke(d);
            //        string logstr;
            //        int recv_Length = 0;
            //        /* recv_Length = server.ReceiveFrom(recv_Data,ref phillip);
            //          if (recv_Length > 0) MessageBox.Show("indication message is received");*/

            //        string response_str = "";
            //        Byte[] AssocReq = new Byte[]                   //AssocReq
            //    {
            //     0x0D,0xEC,                                   //SessionHeader  
            //     0x05,0x08,0x13,0x01,0x00,0x16,0x01,0x02,     //SessionData
            //     0x80,0x00,0x14,0x02,0x00,0x02,

            //     0xC1,0xDC,0x31,0x80,0xA0,0x80,0x80,0x01,     //PresentationHeader
            //     0x01,0x00,0x00,0xA2,0x80,0xA0,0x03,0x00,
            //     0x00,0x01,0xA4,0x80,0x30,0x80,0x02,0x01,
            //     0x01,0x06,0x04,0x52,0x01,0x00,0x01,0x30,
            //     0x80,0x06,0x02,0x51,0x01,0x00,0x00,0x00,
            //     0x00,0x30,0x80,0x02,0x01,0x02,0x06,0x0C,
            //     0x2A,0x86,0x48,0xCE,0x14,0x02,0x01,0x00,
            //     0x00,0x00,0x01,0x01,0x30,0x80,0x06,0x0C,
            //     0x2A,0x86,0x48,0xCE,0x14,0x02,0x01,0x00,
            //     0x00,0x00,0x02,0x01,0x00,0x00,0x00,0x00,
            //     0x00,0x00,0x61,0x80,0x30,0x80,0x02,0x01,
            //     0x01,0xA0,0x80,0x60,0x80,0xA1,0x80,0x06,
            //     0x0C,0x2A,0x86,0x48,0xCE,0x14,0x02,0x01,
            //     0x00,0x00,0x00,0x03,0x01,0x00,0x00,0xBE,
            //     0x80,0x28,0x80,0x06,0x0C,0x2A,0x86,0x48,
            //     0xCE,0x14,0x02,0x01,0x00,0x00,0x00,0x01,
            //     0x01,0x02,0x01,0x02,0x81,

            //     0x48,                                        //AssocReqUserData
            //     0x80,0x00,0x00,0x00,0x40,0x00,0x00,0x00,
            //     0x00,0x00,0x00,0x00,0x80,0x00,0x00,0x00,
            //     0x20,0x00,0x00,0x00,
            //     0x00,0x00,0x00,0x00,
            //     0x00,0x01,0x00,0x2c,
            //     0x00,0x01,0x00,0x28,
            //     0x80,0x00,0x00,0x00,0x00,0x00,0x09,0xc4,
            //     0x00,0x00,0x09,0xc4,0x00,0x00,0x03,0xe8,
            //     0xff,0xff,0xff,0xff,0x60,0x00,0x00,0x00,
            //     0x00,0x01,0x00,0x0c,
            //     0xf0,0x01,0x00,0x08,
            //     0x80,0x00,0x00,0x00,0x00,0x00,0x00,0x00,  //0x20 表示每60秒应答一次 0x80表示每秒应答一次。 
             
            //     0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,   //PresentationTrailer
            //     0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
            //     };

            //        server.SendTo(AssocReq, 237, SocketFlags.None, phillip_server);
            //        //logstr = "Send Association Request : " + MeasureDate.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + BitConverter.ToString(AssocReq).Replace("-", ",0x");
            //        //Thread.Sleep(Waiting_Period);
            //        //SaveLog(logstr);

            //        recv_Length = 0;
            //        recv_Length = server.ReceiveFrom(recv_Data, ref phillip);
            //        //logstr = "Received Association Result: " + BitConverter.ToString(recv_Data, 0, recv_Length).Replace("-", ",0x");
            //        //SaveLog(logstr);
            //        if (recv_Length > 0)
            //        {
            //            if (recv_Data[0] == 0x0E)  // MessageBox.Show("收到联系响应报文！");
            //            {
            //                for (i = 0; i < recv_Length; i++)
            //                {
            //                    response_str += recv_Data[i].ToString();
            //                    response_str += " ";
            //                }
            //                //   response_txt.Text = response_str;
            //                recv_Length = server.ReceiveFrom(recv_Data, ref phillip);// 接收 MDS CREATE EVENT 
            //                //logstr = "Received MSD Create Event Report : " + BitConverter.ToString(recv_Data, 0, recv_Length).Replace("-", ",0x");
            //                //SaveLog(logstr);

            //                if (recv_Length > 0)
            //                {
            //                    //   MessageBox.Show("收到事件报文！");
            //                    response_str = "";
            //                    for (i = 0; i < recv_Length; i++)
            //                    {
            //                        response_str += recv_Data[i].ToString();
            //                        response_str += " ";
            //                    }
            //                    //eveReasult_txt.Text = response_str;
            //                }
            //                Byte[] event_result = new Byte[]
            //            {
            //                0xE1,0x00,0x00,0x02,
            //                0x00,0x02,0x00,0x14,
            //                0x00,0x01,0x00,0x01,0x00,0x0e,
            //                0x00,0x21,0x00,0x00,0x00,0x00,0x00,0x48,
            //                0x47,0x00,0x0d,0x06,0x00,0x00
            //            };
            //                server.SendTo(event_result, 28, SocketFlags.None, phillip_server);
            //                //logstr = "Send MSD Create Event Result: " + BitConverter.ToString(event_result).Replace("-", ",0x");
            //                //SaveLog(logstr);

            //                Thread.Sleep(1000);
            //            }
            //            if (recv_Data[0] == 0x0C) MessageBox.Show("收到拒绝联系报文！");
            //            if (recv_Data[0] == 0x19) MessageBox.Show("收到丢弃联系报文！");
            //        }

            //        Byte[] single_poll_exs = new Byte[]
            //     {
            //         0xE1,0x00,0x00,0x02,
            //         0x00,0x01,0x00,0x20,
            //         0x00,0x01,0x00,0x07,0x00,0x1a,
            //         0x00,0x21,0x00,0x00,0x00,0x00,0x00,0x00,
            //         0x00,0x00,0xf1,0x3b,0x00,0x0c,
            //         0x00,0x01,0x00,0x01,0x00,0x06,0x08,0x03,
            //         0x00,0x00,0x00,0x00
            //      };
            //        /*   Byte[] single_pool_data_request =new Byte[]
            //           {
            //             0xE1, 0x00, 0x00, 0x02,
            //             0x00, 0x01, 0x00, 0x1c,
            //             0x00, 0x01, 0x00, 0x07, 0x00, 0x16,
            //             0x00, 0x21, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            //             0x00, 0x00, 0x0c, 0x16, 0x00, 0x08,
            //             0x00, 0x01, 0x00, 0x01, 0x00, 0x06, 0x08,0x03  //0x08, 0x03 是 polled_attr_grp， 选择NOM_ATTR_GRP_METRIC_VAL_OBS 0x0803
            //           }; */
            //        int count1 = 0;
            //        int count2 = 0;
            //        int count3 = 0;
            //        int count4 = 0;

            //        int contemp1 = 0;
            //        int contemp2 = 0;
            //        int contemp3 = 0;
            //        ////int contemp4 = 0;
            //        int contemp5 = 0;
            //        int physio_id = 0;

            //        //要采集的数据

            //        LoopCount = 0;
            //        HasEffectiveData = false;
            //        // while (true)
            //        while (LoopCount < MAX_TRY_TIMES && !HasEffectiveData)
            //        {
            //            LoopCount++;
            //            Thread.Sleep(500);
            //            server.SendTo(single_poll_exs, 40, SocketFlags.None, phillip_server);
            //            logstr = "Send single pool ext: " + BitConverter.ToString(single_poll_exs).Replace("-", ",0x");
            //            //SaveLog(logstr);
            //            //Thread.Sleep(1000);
            //            recv_Length = server.ReceiveFrom(recv_Data, ref phillip);
            //            logstr = "Received single pool ext response1: " + BitConverter.ToString(recv_Data, 0, recv_Length).Replace("-", ",0x");
            //            //SaveLog(logstr);
            //            //处理第一个消息， 该消息是一个ROLRS_APDU消息
            //            if (recv_Length > 0 && recv_Data[4] == 0x00 && recv_Data[5] == 0x05)
            //            {
            //                contemp1 = 50 + 2;
            //                count1 = recv_Data[46 + 2] * 256 + recv_Data[47 + 2];
            //                for (i = 0; i < count1; i++)
            //                {
            //                    contemp2 = contemp1 + 6;
            //                    count2 = recv_Data[contemp2 - 4] * 256 + recv_Data[contemp2 - 3];
            //                    for (j = 0; j < count2; j++)
            //                    {
            //                        contemp3 = contemp2 + 6;
            //                        count3 = recv_Data[contemp3 - 4] * 256 + recv_Data[contemp3 - 3];
            //                        for (k = 0; k < count3; k++)
            //                        {
            //                            if (recv_Data[contemp3] == 0x09 && recv_Data[contemp3 + 1] == 0x50) //Attribute ID: Numeric Observed Value NOM_ATTR_NU_VAL_OBS
            //                            {
            //                                physio_id = recv_Data[contemp3 + 4] * 256 + recv_Data[contemp3 + 5];
            //                                switch (physio_id)
            //                                {
            //                                    case 0x4822:
            //                                        pr_effective = recv_Data[contemp3 + 6] == 0 ? true : false;
            //                                        if (pr_effective)
            //                                            PR = (int)getFloat(recv_Data, contemp3 + 10);
            //                                        break;
            //                                    case 0x500A:
            //                                        rr_effective = recv_Data[contemp3 + 6] == 0 ? true : false;
            //                                        if (rr_effective)
            //                                            RR = (int)getFloat(recv_Data, contemp3 + 10);
            //                                        break;
            //                                    case 0x4BB8:
            //                                        spo2_effective = recv_Data[contemp3 + 6] == 0 ? true : false;
            //                                        if (spo2_effective)
            //                                            SPO2 = (int)getFloat(recv_Data, contemp3 + 10);
            //                                        break;
            //                                    case 0x4B60:
            //                                        temp_effective = recv_Data[contemp3 + 6] == 0 ? true : false;
            //                                        if (temp_effective)
            //                                            TEMP1 = getFloat(recv_Data, contemp3 + 10);
            //                                        break;
            //                                    case 0x4182:
            //                                        hr_effective = recv_Data[contemp3 + 6] == 0 ? true : false;
            //                                        if (hr_effective)
            //                                            HR = (int)getFloat(recv_Data, contemp3 + 10);
            //                                        break;
            //                                }
            //                            }
            //                            else

            //                                if (recv_Data[contemp3] == 0x09 && recv_Data[contemp3 + 1] == 0x4B)//Attribute ID: Compound Numeric Observed Value NOM_ATTR_NU_CMPD_VAL_OBS
            //                                {
            //                                    contemp4 = contemp3 + 4;
            //                                    count4 = recv_Data[contemp4] * 256 + recv_Data[contemp4 + 1];
            //                                    //                                    for (q = 0; q < count4; q++)
            //                                    //        {
            //                                    //            if (recv_Data[contemp4] == 0x4A && recv_Data[contemp4 + 1] == 0x04)
            //                                    //          {
            //                                    contemp5 = contemp4 + 4;
            //                                    for (t = 0; t < count4; t++)
            //                                    {
            //                                        physio_id = recv_Data[contemp5] * 256 + recv_Data[contemp5 + 1];
            //                                        switch (physio_id)
            //                                        {
            //                                            //NBP
            //                                            case 0x4A05:
            //                                                sys_effective = recv_Data[contemp5 + 2] == 0 ? true : false;
            //                                                if (sys_effective)
            //                                                    SYS = (int)getFloat(recv_Data, contemp5 + 6);
            //                                                break;
            //                                            case 0x4A06:
            //                                                dia_effective = recv_Data[contemp5 + 2] == 0 ? true : false;
            //                                                if (dia_effective)
            //                                                    DIA = (int)getFloat(recv_Data, contemp5 + 6);
            //                                                break;
            //                                            case 0x4A07:
            //                                                map_effective = recv_Data[contemp5 + 2] == 0 ? true : false;
            //                                                if (map_effective)
            //                                                    MAP = (int)getFloat(recv_Data, contemp5 + 6);
            //                                                break;

            //                                            //CVP 
            //                                            case 0x4A45:
            //                                                CVP_sys_effective = recv_Data[contemp5 + 2] == 0 ? true : false;
            //                                                if (CVP_sys_effective)
            //                                                    CVP_SYS = (int)getFloat(recv_Data, contemp5 + 6);
            //                                                break;
            //                                            case 0x4A46:
            //                                                CVP_dia_effective = recv_Data[contemp5 + 2] == 0 ? true : false;
            //                                                if (CVP_dia_effective)
            //                                                    CVP_DIA = (int)getFloat(recv_Data, contemp5 + 6);
            //                                                break;
            //                                            case 0x4A47:
            //                                                CVP_map_effective = recv_Data[contemp5 + 2] == 0 ? true : false;
            //                                                if (CVP_map_effective)
            //                                                    CVP_MAP = (int)getFloat(recv_Data, contemp5 + 6);
            //                                                break;

            //                                            //ABP
            //                                            case 0x4A15:
            //                                                ABP_sys_effective = recv_Data[contemp5 + 2] == 0 ? true : false;
            //                                                if (ABP_sys_effective)
            //                                                    ABP_SYS = (int)getFloat(recv_Data, contemp5 + 6);
            //                                                break;
            //                                            case 0x4A16:
            //                                                ABP_dia_effective = recv_Data[contemp5 + 2] == 0 ? true : false;
            //                                                if (ABP_dia_effective)
            //                                                    ABP_DIA = (int)getFloat(recv_Data, contemp5 + 6);
            //                                                break;
            //                                            case 0x4A17:
            //                                                ABP_map_effective = recv_Data[contemp5 + 2] == 0 ? true : false;
            //                                                if (ABP_map_effective)
            //                                                    ABP_MAP = (int)getFloat(recv_Data, contemp5 + 6);
            //                                                break;
            //                                        }
            //                                        contemp5 += 10;
            //                                    } //End for
            //                                }
            //                                else
            //                                    if (recv_Data[contemp3] == 0x09 && recv_Data[contemp3 + 1] == 0x90)//Attribute ID: Absolute Time Stamp NOM_ATTR_TIME_STAMP_ABS
            //                                    { //时间字段  BCD格式， 需要转化为十进制
            //                                        contemp4 = contemp3 + 4;

            //                                        byte century = recv_Data[contemp4];
            //                                        int century_dec = century / 16 * 10 + century % 16;
            //                                        byte year = recv_Data[contemp4 + 1];
            //                                        int year_dec = year / 16 * 10 + year % 16 + century_dec * 100;
            //                                        byte month = recv_Data[contemp4 + 2];
            //                                        int month_dec = month / 16 * 10 + month % 16;
            //                                        byte day = recv_Data[contemp4 + 3];
            //                                        int day_dec = day / 16 * 10 + day % 16;
            //                                        byte hour = recv_Data[contemp4 + 4];
            //                                        int hour_dec = hour / 16 * 10 + hour % 16;
            //                                        byte minute = recv_Data[contemp4 + 5];
            //                                        int minute_dec = minute / 16 * 10 + minute % 16;
            //                                        byte second = recv_Data[contemp4 + 6];
            //                                        int second_dec = second / 16 * 10 + second % 16;
            //                                        byte sec_fractions = recv_Data[contemp4 + 7];
            //                                        int sec_fractions_dec = sec_fractions / 16 * 10 + sec_fractions % 16;
            //                                        MeasureDate = new DateTime(year_dec, month_dec, day_dec, hour_dec, minute_dec, second_dec);

            //                                    }
            //                            contemp3 += 4 + recv_Data[contemp3 + 2] * 256 + recv_Data[contemp3 + 3];
            //                        }
            //                        contemp2 += 6 + recv_Data[contemp2 + 4] * 256 + recv_Data[contemp2 + 5];
            //                    }
            //                    contemp1 += 6 + recv_Data[contemp1 + 4] * 256 + recv_Data[contemp1 + 5];
            //                }
            //                //setText ttd = new setText(DisplayAllData);
            //                //this.Invoke(d);

            //                HasEffectiveData = hr_effective || rr_effective || pr_effective || temp_effective || spo2_effective || sys_effective || dia_effective || map_effective || CVP_map_effective || ABP_map_effective;
            //                HasEffectivePressure = map_effective || CVP_map_effective || ABP_map_effective;

            //                //logstr = "HR=" + HR.ToString() + ";RR=" + RR.ToString() + ";SPO2=" + SPO2.ToString() + ";sys=" + SYS.ToString() + ";DIA=" + DIA.ToString() + ";MAP=" + MAP.ToString() + ";CVP_MAP=" + CVP_MAP.ToString() + ";ABP_MAP=" + ABP_MAP.ToString();
            //                //SaveLog(logstr);

            //            }

            //            if (recv_Length > 0 && recv_Data[4] == 0x00 && recv_Data[5] == 0x05)  //第一条消息收到，有后续存在的消息要接收， 第二次接受
            //            {
            //                //第二个消息字符串， 里面有几个字节， 但是是空的
            //                // Thread.Sleep(Waiting_Period);
            //                recv_Length = server.ReceiveFrom(recv_Data, ref phillip);
            //                //logstr = "Received single pool ext response2: " + BitConverter.ToString(recv_Data, 0, recv_Length).Replace("-", ",0x");
            //                //SaveLog(logstr);
            //            }

            //            if (recv_Length > 0 && recv_Data[4] == 0x00 && recv_Data[5] == 0x05)  //准备接受最后一个消息内容。
            //            {
            //                //接受第三个消息字符串， 里面有几个字节， 但是没有什么内容，不予处理
            //                //Thread.Sleep(Waiting_Period);
            //                recv_Length = server.ReceiveFrom(recv_Data, ref phillip);
            //                //logstr = "Received single pool ext response3: " + BitConverter.ToString(recv_Data, 0, recv_Length).Replace("-", ",0x");
            //                //SaveLog(logstr);
            //            }
            //            if (recv_Length > 0 && recv_Data[4] == 0x00 && recv_Data[5] == 0x02)  //最后一条消息收到
            //            {

            //            }
            //        } //End While
            //        if (HasEffectiveData)
            //        {

            //            //  System.DateTime currentTime = new System.DateTime();
            //            //  currentTime = System.DateTime.Now;
            //            PatientDataBlock PatientData;
            //            PatientData.MeasureTime = MeasureDate;
            //            PatientData.PR = PR; PatientData.pr_effective = pr_effective;    //脉率
            //            PatientData.RR = 0; PatientData.rr_effective = rr_effective;    //呼吸频率
            //            PatientData.SPO2 = SPO2; PatientData.spo2_effective = spo2_effective;//血氧饱和度
            //            PatientData.TEMP = TEMP1; PatientData.temp_effective = temp_effective;//温度
            //            PatientData.HR = HR; PatientData.hr_effective = hr_effective;    //心率
            //            PatientData.SYS = SYS; PatientData.sys_effective = sys_effective; //收缩压
            //            PatientData.DIA = DIA; PatientData.dia_effective = dia_effective; //舒张压
            //            PatientData.MAP = MAP; PatientData.map_effective = map_effective; //平均血压
            //            PatientData.CVP_SYS = CVP_SYS; PatientData.CVP_sys_effective = CVP_sys_effective; //中心静脉收缩压
            //            PatientData.CVP_DIA = CVP_DIA; PatientData.CVP_dia_effective = CVP_dia_effective; //中心静脉舒张压
            //            PatientData.CVP_MAP = CVP_MAP; PatientData.CVP_map_effective = CVP_map_effective; //中心静脉平均血压
            //            PatientData.ABP_SYS = ABP_SYS; PatientData.ABP_sys_effective = ABP_sys_effective; //动脉收缩压
            //            PatientData.ABP_DIA = ABP_DIA; PatientData.ABP_dia_effective = ABP_dia_effective; //动脉舒张压
            //            PatientData.ABP_MAP = ABP_MAP; PatientData.ABP_map_effective = ABP_map_effective; //动脉平均血压

            //            //logstr = "Final Saved Data: Time=" + MeasureDate.ToString("yyyy-MM-dd HH:mm:ss") + ";HR=" + HR.ToString() + ";RR=" + RR.ToString() + ";SPO2=" + SPO2.ToString() + ";sys=" + SYS.ToString() + ";DIA=" + DIA.ToString() + ";MAP=" + MAP.ToString() + ";CVP_MAP=" + CVP_MAP.ToString() + ";ABP_MAP=" + ABP_MAP.ToString();
            //            //SaveLog(logstr);
            //            this.SaveFillipData(PatientData, mzjldID, 0);

            //        }
            //        Byte[] Associate_Release_Request = new Byte[]
            //     {
            //         0x09, 0x18, 
            //         0xC1, 0x16, 0x61, 0x80, 0x30, 0x80, 0x02, 0x01,
            //         0x01, 0xA0, 0x80, 0x62, 0x80, 0x80, 0x01, 0x00,
            //         0x00, 0x00, 0x00, 0x00,
            //         0x00, 0x00, 0x00, 0x00
            //      };
            //        server.SendTo(Associate_Release_Request, 26, SocketFlags.None, phillip_server);

            //        //logstr = "Send Associate Release Request: " + BitConverter.ToString(Associate_Release_Request).Replace("-", ",0x");
            //        // SaveLog(logstr);
            //        // Thread.Sleep(Waiting_Period);
            //        recv_Length = server.ReceiveFrom(recv_Data, ref phillip);
            //        //logstr = "Received Associate Release Result: " + BitConverter.ToString(recv_Data, 0, recv_Length).Replace("-", ",0x");
            //        //SaveLog(logstr);

            //        Byte[] Associate_Release_Response = new Byte[]
            //     {
            //         0x0A, 0x18, 

            //         0xC1, 0x16, 0x61, 0x80, 0x30, 0x80, 0x02, 0x01,
            //         0x01, 0xA0, 0x80, 0x63, 0x80, 0x80, 0x01, 0x00,
            //         0x00, 0x00, 0x00, 0x00,
            //         0x00, 0x00, 0x00, 0x00
            //      };

            //        Byte[] Associate_Abort = new Byte[]
            //     {
            //         0x19, 0x2E,
            //         0x11, 0x01, 0x03,
            //         0xC1, 0x29, 0xA0, 0x80, 0xA0, 0x80, 0x30, 0x80,
            //         0x02, 0x01, 0x01, 0x06, 0x02, 0x51, 0x01, 0x00,
            //         0x00, 0x00, 0x00, 0x61, 0x80, 0x30, 0x80, 0x02,
            //         0x01, 0x01, 0xA0, 0x80, 0x64, 0x80, 0x80, 0x01,
            //         0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            //         0x00, 0x00, 0x00, 0x00
            //      };
            //        if (recv_Data[0] == Associate_Release_Response[0] && recv_Data[1] == Associate_Release_Response[1])
            //        {
            //            //Received correct release Response
            //            //MessageBox.Show("连接正确退出！");
            //        }
            //        else if (recv_Data[0] == Associate_Abort[0] && recv_Data[1] == Associate_Abort[1])
            //        {
            //            MessageBox.Show("收到连接中断消息！");
            //        }
            //        else
            //        {
            //            MessageBox.Show("收到无法识别消息！");
            //        }
            //        Thread.Sleep(Get_Period);
            //        GetDataTimes++;
            //    }
            //}
            //public struct PatientDataBlock
            //{
            //    public DateTime MeasureTime;
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
     //   public void SaveFillipData(PatientDataBlock PatientInfo, int mzid, int type)
       // {
            //int PR1 = PatientInfo.PR;
            //int RR1 = PatientInfo.RR;
            //int SPO21 = (int)PatientInfo.SPO2;
            //float TEMP1 = PatientInfo.TEMP;
            //int HR1 = PatientInfo.HR;
            //int SYS1 = PatientInfo.SYS;
            //int DIA1 = PatientInfo.DIA;
            //int MAP1 = PatientInfo.MAP;
            //int CVP_DIA1 = PatientInfo.CVP_DIA;
            //int CVP_SYS1 = PatientInfo.CVP_SYS;
            //int CVP_MAP1 = PatientInfo.CVP_MAP;
            //int ABP_SYS1 = PatientInfo.ABP_SYS;
            //int ABP_DIA1 = PatientInfo.ABP_DIA;
            //int ABP_MAP1 = PatientInfo.ABP_MAP;
            ////DateTime now = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
            //string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            //if (sqlite.selectJianCeData(now, mzjldID, type).Rows.Count == 0)
            //{
            //    int fa = 0;
            //    if (ABP_SYS1 != 0 && ABP_DIA1 != 0 && ABP_MAP1 != 0)
            //    {
            //        if (type == 0)
            //        fa = sqlite.insertJianCeDataMZJLD(mzjldID, ABP_SYS1, ABP_DIA1, ABP_MAP1, RR1, HR1, PR1, SPO21, new Random().Next(32, 35), TEMP1, now);
            //        if (type == 1)
            //            fa = sqlite.insertJianCeDataPACU(mzjldID, ABP_SYS1, ABP_DIA1, ABP_MAP1, RR1, HR1, PR1, SPO21, new Random().Next(32, 35), TEMP1, now);
            //    }
            //    else
            //    {
            //        if (type == 0)
             //         sqlite.insertJianCeDataMZJLD(mzjldID, SYS1, DIA1, MAP1, RR1, HR1, PR1, SPO21, new Random().Next(32, 35), TEMP1, now);
            //        if (type == 1)
            //            fa = sqlite.insertJianCeDataPACU(mzjldID, SYS1, DIA1, MAP1, RR1, HR1, PR1, SPO21, new Random().Next(32, 35), TEMP1, now);
            //    }
            //}
      //  }

        //private float getFloat(Byte[] flt, int start)
        //{
        //    int exp = 0;
        //    long m = 0;
        //    float value;
        //    if (flt[start] > 127)
        //    {
        //        exp = -1 * (256 - flt[start]);
        //    }
        //    else
        //        exp = flt[start];
        //    if (flt[start + 1] > 127)
        //    {
        //        m = -1 * (long)(System.Math.Pow(2, 24) - flt[start + 1] * System.Math.Pow(2, 16)
        //            - flt[start + 2] * System.Math.Pow(2, 8) - flt[start + 3]);
        //    }
        //    else
        //        m = (long)(flt[start + 1] * System.Math.Pow(2, 16) + flt[start + 2] * System.Math.Pow(2, 8)
        //            + flt[start + 3]);
        //    value = (float)(m * System.Math.Pow(10, exp));
        //    return value;
        //}
        //private void SaveConfigure()
        //{
        //    IPConfigureInfo IpConf;
        //    IpConf.PatientIPAddress = IPAddressInput1; IpConf.BedID = BedIDInput1;
        //    FileStream fs = new FileStream(Application.StartupPath + "\\Config.txt", FileMode.Create);
        //    StreamWriter sw = new StreamWriter(fs, Encoding.Default);
        //    sw.WriteLine(IpConf.PatientIPAddress);
        //    sw.WriteLine(IpConf.BedID);
        //    sw.Close();
        //    fs.Close();
        //}
        //private void GetConfigure()
        //{
        //    IPConfigureInfo IpConf;
        //    FileStream fs = new FileStream(Application.StartupPath + "\\Config.txt", FileMode.Open);
        //    StreamReader sw = new StreamReader(fs, Encoding.Default);
        //    IpConf.PatientIPAddress = sw.ReadLine();
        //    IpConf.BedID = sw.ReadLine();
        //    sw.Close();
        //    fs.Close();
        //    IPAddressInput1 = IpConf.PatientIPAddress; BedIDInput1 = IpConf.BedID;

        //}
        //public struct IPConfigureInfo
        //{
        //    public string PatientIPAddress; public string BedID;
        //}
        #endregion

        #region  金科威监护函数

        private void JkwDataResolve()
        {
            bool TimerStarted = false;
            string temp1 = tbIpAdress.Text;
            IPAddressInput1 = tbIpAdress.Text;
            if (!TimerStarted)
            {
                CheckTimer.Start();
                TimerStarted = true;
            }
            if (ThreadExist_JKW)
            {
                ThreadExist_JKW = false;
                Receiving_JKW.Abort();                
                Receiving_JKW =null;               
            }
            SaveConfigure();
            GetConfigure();
            if (!ThreadExist_JKW)
            {
                Receiving_JKW = new Thread(Socket_Setup_JKW);
                Receiving_JKW.IsBackground = true;
                Receiving_JKW.Priority = ThreadPriority.AboveNormal;
                Receiving_JKW.Start();
                ThreadExist_JKW = true;
            }
        }
        
        private void Socket_Setup_JKW()
        {
            int port = 8010;
            string temp1 = tbIpAdress.Text;
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(temp1.Trim()), port);
            ServerSocket_JKW = new Socket(ipep.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                //绑定本机的一个端口，然后进行监听
                ServerSocket_JKW.Bind(ipep);
                ServerSocket_JKW.Listen(100);
                TempSocket_JKW = ServerSocket_JKW.Accept();

                while (true)
                {
                    ReceivedLength_JKW = TempSocket_JKW.Receive(Raw_Data_JKW);
                    ReceivedTime_JKW = DateTime.Now;
                    //string ReceivedString1 = System.Text.Encoding.Default.GetString(Raw_Data_JKW);
                    string ReceivedString1 =  BitConverter.ToString(Raw_Data_JKW, 0, ReceivedLength_JKW);
                    if (Laststring_JKW != ReceivedString1)
                    {
                        Laststring_JKW = ReceivedString1; 
                        ProceedreceivedData_JKW(Laststring_JKW);                        
                    }                   
                }

            }

            catch (Exception exception)
            {
                SockectIsException_JKW = true;               
            }

        }

        private void SaveLog(string text)
        {
            //string text = "abcdefg\r\n";
            int TextLength = text.Length;
            // if (TextLength < 400)
            {
                FileStream fs = new FileStream("c:\\JKWlog.txt", FileMode.Append);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                sw.Write(text + "\r\n");
                sw.Close();
                fs.Close();
            }
        }
        Regex r1 = new Regex("AA-");
        Regex r2 = new Regex("-CC");
        int CON = 0;
        public void ProceedreceivedData_JKW(string tempStr)//金科威解析
        {
            CON++;
            //int JXcount10 = 0;
            int JXcount20 = 0;
            int JXcount30 = 0;
            int JXcount40 = 0;
            int JXcount50 = 0;
            int JXcount60 = 0;
           // SaveLog(CON.ToString() + "、、、" + tempStr);
            int type = 0;
            string[] strList = Regex.Split(tempStr, "-CC",RegexOptions.IgnoreCase);

            foreach (string STR in strList)
           
            {
                //SaveLog(STR);
                string jiexiStr = "";
                string[] strValue = new string[100];
                if (STR.Contains("AA-20") && JXcount20 == 0)
                {
                    jiexiStr = STR.Replace("AA-20", "");
                    jiexiStr = jiexiStr.Replace("-", "");
                    int lenth = jiexiStr.Length / 2;
                    for (int i = 0; i < lenth; i++)
                    {
                        strValue[i] = jiexiStr.Substring(i * 2, 2);
                    }
                    if (HexToInt(strValue[2]) != 0)
                        SYS = HexToInt(strValue[2]);
                    if (HexToInt(strValue[3]) != 0)
                        DIA = HexToInt(strValue[3]);
                    if (HexToInt(strValue[4]) != 0)
                        MAP = HexToInt(strValue[4]);
                    TEMP1 = float.Parse(HexToInt(strValue[5]) + "." + HexToInt(strValue[6]));
                    //if (HexToInt(strValue[7]) != 0)
                    //    RR = HexToInt(strValue[7]);
                    TEMP2 = float.Parse(HexToInt(strValue[11]) + "." + HexToInt(strValue[12]));

                    JXcount20++;
                    ////  label23.Text = "舒张压为：" + DIA.ToString() + "  收缩压为：" + SYS.ToString()
                    //      + " 平均压为：" + MAP.ToString() + " 心率为：" + RR.ToString()
                    //      + "  体温1为：" + TEMP1.ToString() + "  体温2为：" + TEMP2.ToString();
                }
                else if (STR.Contains("AA-30") && JXcount30 == 0)
                {
                    jiexiStr = STR.Replace("AA-30", "");
                    jiexiStr = jiexiStr.Replace("-", "");
                    int lenth = jiexiStr.Length / 2;
                    for (int i = 0; i < lenth; i++)
                    {
                        strValue[i] = jiexiStr.Substring(i * 2, 2);
                    }
                    if (HexToInt(strValue[1]) != 0)
                        PR = HexToInt(strValue[1]);
                    if (HexToInt(strValue[2]) != 0)
                        SPO2 = HexToInt(strValue[2]);
                    JXcount30++;
                    //label29.Text ="脉搏为：" + PR.ToString()
                    //      + "  "SPO2为：" + SPO2.ToString();
                }
                else if (STR.Contains("AA-40") && JXcount40 == 0)
                {
                    jiexiStr = STR.Replace("AA-40", "");
                    jiexiStr = jiexiStr.Replace("-", "");
                    int lenth = jiexiStr.Length / 2;
                    for (int i = 0; i < lenth; i++)
                    {
                        strValue[i] = jiexiStr.Substring(i * 2, 2);
                    }
                    if (HexToInt(strValue[1]) != 0)
                        RR = HexToInt(strValue[1]);
                    JXcount40++;
                    //label28.Text = "RR为：" + RR.ToString();
                }
                else if (STR.Contains("AA-50") && JXcount50 == 0)
                {
                    jiexiStr = STR.Replace("AA-50", "");
                    jiexiStr = jiexiStr.Replace("-", "");
                    int lenth = jiexiStr.Length / 2;
                    for (int i = 0; i < lenth - 1; i++)
                    {
                        strValue[i] = jiexiStr.Substring(i * 2, 2);
                    }
                    //if (HexToInt(strValue[1]) != 0)
                    //    RR = HexToInt(strValue[1]);
                    if (HexToInt(strValue[2]) != 0)
                        ETCO2 = HexToInt(strValue[2]);
                    JXcount50++;
                    //label28.Text = "ETCO2为：" + ETCO2.ToString();
                }
                else if (STR.Contains("AA-60") && JXcount60 == 0)
                {
                    jiexiStr = STR.Replace("AA-60", "");
                    jiexiStr = jiexiStr.Replace("-", "");
                    int lenth = jiexiStr.Length / 2;
                    for (int i = 0; i < lenth ; i++)
                    {
                        strValue[i] = jiexiStr.Substring(i * 2, 2);
                    }
                    int ibp1sys_1 = HexToInt(strValue[3]);
                    int ibp1sys_2 = HexToInt(strValue[4]);
                    if (ibp1sys_1 != 0)
                        ABP_SYS = ibp1sys_1;
                    if (ibp1sys_2 != 0)
                        ABP_SYS = ibp1sys_2;
                    int ibp1dia_1 = HexToInt(strValue[5]);
                    int ibp1dia_2 = HexToInt(strValue[6]);
                    if (ibp1dia_1 != 0)
                        ABP_DIA = ibp1dia_1;
                    if (ibp1dia_2 != 0)
                        ABP_DIA = ibp1dia_2;
                    int ibp1map_1 = HexToInt(strValue[7]);
                    int ibp1map_2 = HexToInt(strValue[8]);
                    if (ibp1map_1 != 0)
                        ABP_MAP = ibp1map_1;
                    if (ibp1map_2 != 0)
                        ABP_MAP = ibp1map_2;
                    JXcount60++;
                }
            }
           
            #region  存入本地数据库
            //DateTime now = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            if (sqlite.selectJianCeData(now, mzjldID, type).Rows.Count == 0)
            {
                if (ETCO2 == 0)
                    ETCO2 = new Random().Next(31, 35);
                if (rrc == null)
                {
                    RR = SPO2 / 4;

                    //RR =Convert.ToInt32(rrc);

                }//机械值
                else if (rrc.IsNullOrEmpty())//根据氧和赋值
                {
                    RR = SPO2 / 4;
                }
                else
                {
                    RR = Convert.ToInt32(rrc);
                }
                if (RR == 0)//根据氧和赋值
                {
                    RR = SPO2 / 4;
                }
                int fa = 0;
                if (ABP_SYS != 0 && ABP_DIA != 0 && ABP_MAP != 0)
                {
                    if (type == 0)
                    //    fa = sqlite.insertJianCeDataMZJLD(mzjldID, ABP_SYS, ABP_DIA, ABP_MAP, RR, HR, PR, SPO2, ETCO2, TEMP1, now);
                    if (type == 1)
                        fa = sqlite.insertJianCeDataPACU(mzjldID, ABP_SYS, ABP_DIA, ABP_MAP, RR, HR, PR, SPO2, ETCO2, TEMP1, now);
                }
                else
                {
                    if (type == 0)
                     //   fa = sqlite.insertJianCeDataMZJLD(mzjldID, SYS, DIA, MAP, RR, HR, PR, SPO2, ETCO2, TEMP1, now);
                    if (type == 1)
                        fa = sqlite.insertJianCeDataPACU(mzjldID, SYS, DIA, MAP, RR, HR, PR, SPO2, ETCO2, TEMP1, now);
                }
            }
            #endregion

        }
        private int HexToInt(string shiliu)//16进制转化为10进制
        {
            int shi = 0;
            try
            {
                int Int = Convert.ToInt32(shiliu, 16);
                shi = Int;
            }
            catch (Exception)
            { return shi; }
            return shi;
        }

      

        
        #endregion

        #region  迈瑞监护函数
        private void MirayT6DataResolve()
        {
            if (!isexist_Miray)
            {
                SocketThread_Miray = new Thread(Socket_Setup_Miary);
                SocketThread_Miray.IsBackground = true;
                SocketThread_Miray.Priority = ThreadPriority.AboveNormal;
                SocketThread_Miray.Start();
                isexist_Miray = true;

            }
            else
            {
                if (SocketThread_Miray.ThreadState != System.Threading.ThreadState.Running)
                {
                    SocketThread_Miray.Abort();
                }
            }
        }
        private void Socket_Setup_Miary()
        {
            

            string temp1 = tbIpAdress.Text.Trim();
            string temp2 = "4601";
            int port = Convert.ToInt32(temp2);
            DateTime CurrentTime = DateTime.Now;
            DateTime SendQueryTime = CurrentTime;
            DateTime SendMaintainTime = CurrentTime;
            int temp_count = 0;

            IPEndPoint hostEndPoint = new IPEndPoint(IPAddress.Parse(temp1.Trim()), port);
            //ClintSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ClientSocket_Miray = new Socket(hostEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);


            try
            {
                /*   //绑定本机的一个端口，然后进行监听
                   ServerSocket.Bind(ipep);
                   Thread.Sleep(200);
                   ServerSocket.Listen(5);
                 */

                //Connect to the host using its IPEndPoint.
                ClientSocket_Miray.Connect(hostEndPoint);
                //Thread.Sleep(200);
                while (true)
                {
                   // Thread.Sleep(100);
                    for (temp_count = 0; temp_count < 2000; temp_count++)
                        Raw_Data_Miray[temp_count] = 0;


                    ReceivedLength_Miray = ClientSocket_Miray.Receive(Raw_Data_Miray);

                    ReceivedTime = DateTime.Now;
                    string ReceivedString1 = System.Text.Encoding.Default.GetString(Raw_Data_Miray);
                    string FinalString;
                    if ((ReceivedLength_Miray < 2000) && (ReceivedLength_Miray > 0))
                        FinalString = ReceivedString1.Substring(0, ReceivedLength_Miray);
                    else
                        FinalString = ReceivedString1;
                    ReceivedString1 = FinalString;
                    if (Laststring_Miray != ReceivedString1)
                        ProceedreceivedData_Miray();
                    Laststring_Miray = ReceivedString1;

                    //ReceivedLength_Miray = ClientSocket_Miray.Receive(Raw_Data_Miray);
                    //ReceivedTime = DateTime.Now;
                    //string ReceivedString1 = System.Text.Encoding.Default.GetString(Raw_Data_Miray);
                    //if (Laststring_Miray != ReceivedString1)
                    //    ProceedreceivedData_Miray();
                    //Laststring_Miray = ReceivedString1;
                }
            }
            catch (Exception exception)
            {
                SockectIsException_Miray = true;
                //ClientSocket_Miray.Close();
               // SaveLog("Socket 连接或接收发出错!!!!!" + exception.Message + "\r\n");

            }
        }
        private void ProceedreceivedData_Miray()
        {

            try
            {
                string TempString = System.Text.Encoding.Default.GetString(Raw_Data_Miray);

                string FinalString = TempString.Substring(0, ReceivedLength_Miray);
                count_Miray++;
                //string DisplayStr = ReceivedTime.ToString() + "收到数据包数" + count_Miray.ToString() +
                //       "(" + ReceivedLength_Miray.ToString() + ")字节:\r\n" + FinalString;
                ////if (this.ReceivedDataList.InvokeRequired) //异步等待
                //{
                //    FlushClient fc = new FlushClient(ProceedreceivedData_Miray);
                //    this.Invoke(fc);//通过代理调用刷新方法
                //}
                //else
                //{
                //    this.ReceivedDataList.Items.Add(DisplayStr);
                //}
                //SaveLog(DisplayStr);
                // return;

                // SaveLog("--------------------------开始解析-------------------------------\r\n");

                //解析字符串开始
                string[] StringTemp = FinalString.Split('\r');
                //有用参数定义汇总
                //PV1内有用信息
                //string OfficeName ;
                //char[] OfficeName=new char[30];
                //string OfficeName = "";
                //int BedId = 0;
                //string PatientID = "";
                //string IPSeq = "";
                string MeasureTime = DateTime.Now.ToString(); //测量时间
                //int HR = 0,  = 0, PVCs = 0, RR = 0, PR = 0, Dia = 0, Mean = 0, Sys = 0;
                //int EtCO2 = 0, IBP1m = 0, IBP1s = 0, IBP1d = 0;
                int PVCs = 0, IBP2m = 0, IBP2s = 0, IBP2d = 0,
                    IBP3m = 0, IBP3s = 0, IBP3d = 0,
                    IBP4m = 0, IBP4s = 0, IBP4d = 0;
                foreach (string BasicCommand in StringTemp)
                {

                    //"OBX||CE|2303^PACE_Switch||0^关||||||F"
                    bool IsMSH = BasicCommand.Contains("MSH");
                    bool IsPID = BasicCommand.Contains("PID");
                    bool IsPV1 = BasicCommand.Contains("PV1");
                    bool IsOBR = BasicCommand.Contains("OBR");
                    bool IsOBX = BasicCommand.Contains("OBX");
                    if (IsMSH)    //MSH|^~\&|||||||ORU^R01|103|P|2.3.1|
                    // MSH命令， 不作处理
                    {

                    }
                    if (IsPID)  //PID|||000f1405-e9c0-7202-10093701000e1000||^|||U|
                    {

                    }
                    if (IsPV1) //PV1||I|^^&&3232235608&4601&&0|||||||||||||||A|
                    {

                    }
                    if (IsOBR)  //OBR||||Mindray Monitor|||0|
                    {

                    }
                    if (IsOBX)  //"OBX||NM|101^HR|2101|70||||||F" OBX 有若干钟情况。  "OBX||CE|4"
                    {
                        int position = BasicCommand.IndexOf('^');
                        int fieldposition = position, FieldEndPosition = position, FieldStartPosition;
                        bool IsNM = BasicCommand.Contains("NM"); //是数字
                        int startposition;
                        string TestField = "", TestName = "", TestValueStr = "";
                        float TestValue = 0;
                        if ((position > 0) && IsNM)
                        {
                            //可以屏蔽掉不正常的字符串 例如"OBX||CE|4"
                            startposition = position + 1;
                            while (BasicCommand[position] != '|')
                                position++;
                            TestName = BasicCommand.Substring(startposition, position - startposition);


                            while (BasicCommand[fieldposition] != '|')
                                fieldposition--;
                            FieldStartPosition = fieldposition + 1;
                            TestField = BasicCommand.Substring(FieldStartPosition, FieldEndPosition - FieldStartPosition);


                            position++;
                            while (BasicCommand[position] != '|')
                                position++;
                            startposition = position + 1;
                            position++;
                            while ((BasicCommand[position] != '|') && (BasicCommand[position] != '^'))
                                position++;
                            TestValueStr = BasicCommand.Substring(startposition, position - startposition);
                            if (TestValueStr.IsNullOrEmpty())
                            {
                                TestValue = 0;
                            }
                            else
                            {
                                TestValue = float.Parse(TestValueStr);
                            }
                        }

                        if (TestValue > 0)
                        {
                            if (TestName == "HR") //心率 bpm
                            {
                                HR = (int)TestValue;
                            }
                            if (TestName == "PVCs") // PVC sum  /min (每分钟早期心室收缩的次数)
                            {
                                PVCs = (int)TestValue;
                            }
                            if (TestName == "RR") // RESP  rpm (用胸电极电阻变化测呼吸频率)
                            {
                                RR = (int)TestValue;
                            }
                            if (TestName == "SpO2") // 血氧 %
                            {
                                SPO2 = (int)TestValue;
                            }
                            if (TestName == "PR") // 脉率 bmp
                            {
                                PR = (int)TestValue;
                            }
                            if (TestName == "Dia") // 无创舒张压
                            {
                                DIA = (int)TestValue;
                            }
                            if (TestName == "Mean") // 平均无创血压 mmHg
                            {
                                MAP = (int)TestValue;
                            }
                            if (TestName == "Sys") // 无创收缩压
                            {
                                SYS = (int)TestValue;
                            }
                            if ((TestName == "CO2Et") || (TestField == "220")) // 呼末二氧化碳
                            {
                                ETCO2 = (int)TestValue;
                            }
                            if ((TestName == "IBP1m") || (TestField == "174"))  // IBP1m
                            {
                                ABP_MAP = (int)TestValue;
                            }
                            if ((TestName == "IBP1s") || (TestField == "175"))  // IBP1s 
                            {
                                ABP_SYS = (int)TestValue;
                            }
                            if ((TestName == "IBP1d") || (TestField == "176"))  // IBP1d
                            {
                                ABP_DIA = (int)TestValue;
                            }

                            if ((TestName == "IBP2m") || (TestField == "178"))  // IBP2m
                            {
                                IBP2m = (int)TestValue;
                            }
                            if ((TestName == "IBP2s") || (TestField == "179"))  // IBP2s 
                            {
                                IBP2s = (int)TestValue;
                            }
                            if ((TestName == "IBP2d") || (TestField == "180"))  // IBP2d
                            {
                                IBP2d = (int)TestValue;
                            }

                            if ((TestName == "IBP3m") || (TestField == "182"))  // IBP3m
                            {
                                IBP3m = (int)TestValue;
                            }
                            if ((TestName == "IBP3s") || (TestField == "183"))  // IBP3s 
                            {
                                IBP3s = (int)TestValue;
                            }
                            if ((TestName == "IBP3d") || (TestField == "184"))  // IBP3d
                            {
                                IBP3d = (int)TestValue;
                            }

                            if ((TestName == "IBP4m") || (TestField == "186"))  // IBP4m
                            {
                                IBP4m = (int)TestValue;
                            }
                            if ((TestName == "IBP4s") || (TestField == "187"))  // IBP4s 
                            {
                                IBP4s = (int)TestValue;
                            }
                            if ((TestName == "IBP4d") || (TestField == "188"))  // IBP4d
                            {
                                IBP4d = (int)TestValue;
                            }
                        }
                    }
                }
                int type = 0;
                // DateTime now = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                if (RR == 0 && HR == 0 && PR == 0 && SPO2 == 0 && ETCO2 == 0)
                {
                    return;
                }
                else
                {
                    if (sqlite.selectJianCeData(now, mzjldID, type).Rows.Count == 0)
                    {
                        int fa = 0;
                        if (rrc == null)
                        {
                            RR = SPO2 / 4;

                            //RR =Convert.ToInt32(rrc);

                        }//机械值
                        else if (rrc.IsNullOrEmpty())//根据氧和赋值
                        {
                            RR = SPO2 / 4;
                        }
                        else
                        {
                            RR = Convert.ToInt32(rrc);
                        }
                        if (RR == 0)//根据氧和赋值
                        {
                            RR = SPO2 / 4;
                        }
                       
                        Random rd = new Random();
                        double temp = 36.7;
                        if (ETCO2 == 0)
                        {
                            ETCO2 = new Random().Next(31, 35);
                        }
                        if (ABP_SYS != 0 && ABP_DIA != 0 && ABP_MAP != 0)
                        {
                            if (type == 0)
                                fa = sqlite.insertJianCeDataMZJLD(mzjldID, ABP_SYS, ABP_DIA, ABP_MAP, RR, HR, PR, SPO2, ETCO2, temp,IBP4m, now);
                            if (type == 1)
                                fa = sqlite.insertJianCeDataPACU(mzjldID, ABP_SYS, ABP_DIA, ABP_MAP, RR, HR, PR, SPO2, ETCO2, temp, now);
                        }
                        else
                        {
                            if (type == 0)
                                fa = sqlite.insertJianCeDataMZJLD(mzjldID, SYS, DIA, MAP, RR, HR, PR, SPO2, ETCO2, temp,IBP4m, now);
                            if (type == 1)
                                fa = sqlite.insertJianCeDataPACU(mzjldID, SYS, DIA, MAP, RR, HR, PR, SPO2, ETCO2, temp, now);
                        }
                    }
                }

            }
            catch (Exception)
            {


            }
        }
        private void timer_Miray_Tick(object sender, EventArgs e)
        {
            //用来检测Socket是否正常的Timer
            if (isexist_Miray & SockectIsException_Miray)
            {

                if (ClientSocket_Miray != null)
                    ClientSocket_Miray.Close();
                if (SocketThread_Miray.ThreadState != System.Threading.ThreadState.Running)
                {
                    SocketThread_Miray.Abort();
                }
                isexist_Miray = false;
                SockectIsException_Miray = false;
            }


            if (!isexist_Miray)
            {
                SockectIsException_Miray = false;
                SocketThread_Miray = new Thread(Socket_Setup_Miary);
                SocketThread_Miray.IsBackground = true;
                SocketThread_Miray.Priority = ThreadPriority.AboveNormal;
                SocketThread_Miray.Start();
                isexist_Miray = true;

            }
            else
            {
                if (SocketThread_Miray.ThreadState != System.Threading.ThreadState.Running)
                {
                    SocketThread_Miray.Abort();
                }
            }
        }

        private void MaintainTimer_Tick(object sender, EventArgs e)
        {
            if (ClientSocket_Miray == null)
                return;

            Byte[] head_maintain = new byte[] { 0x0B };
            Byte[] content_maintain = Encoding.Default.GetBytes("MSH|^~\\&|||||||ORU^R01|106|P|2.3.1|");
            Byte[] last_maintain = new byte[] { 0x1C, 0x0D };
            Byte[] full_maintain = new byte[head_maintain.Length + content_maintain.Length + last_maintain.Length];
            Stream s_maintain = new MemoryStream();
            s_maintain.Write(head_maintain, 0, 1);
            s_maintain.Write(content_maintain, 0, content_maintain.Length);
            s_maintain.Write(last_maintain, 0, 2);
            s_maintain.Position = 0;
            int r = s_maintain.Read(full_maintain, 0, full_maintain.Length);
            //            string displaystr = "发送维护连接字段:" + System.Text.Encoding.Default.GetString(full)+"\r\n";
            //            ReceivedDataList.Items.Add(displaystr);
            //            SaveLog(displaystr);
            //while (!IsReceiving)
            // {
            //    IsSending = true;
            //    ClientSocket.Send(full_maintain, full_maintain.Length, 0);
            //    IsSending = false;
            //    break;
            //}           
            //Socket存在而且没有报错, 可以发送维持消息
            if (isexist_Miray & !SockectIsException_Miray & ClientSocket_Miray.Connected)
            {
                try
                {
                    IsSending = true;
                    ClientSocket_Miray.Send(full_maintain, full_maintain.Length, 0);
                    IsSending = false;

                }
                catch (Exception exception)
                {
                    SockectIsException_Miray = true;
                    //clientSocket.Close();
                    //SaveLog("Socket 发送维持命令出错!!!!!" + exception.Message + "\r\n");
                }


            }

            //发送规定字段请求
            if (MainTaincount_Miray % 6 == 0)
            {
                //每隔30秒发送一次规定字段请求。5*6 
                DateTime SendTime = DateTime.Now;
                Byte[] head = new byte[] { 0x0B };
                Byte[] content = Encoding.Default.GetBytes("MSH|^~\\&|||||||QRY^R02|1203|P|2.3.1\r"
                                                           + "QRD|" + string.Format("{0:yyyyMMddHHmmss}", SendTime) + "|R|I|Q895200|||||RES\r"
                                                           + "QRF|MON||||0&0^1^1^0^101&102&151&160\r"
                                                           + "QRF|MON||||0&0^1^1^0^220&170&171&172\r"
                                                           + "QRF|MON||||0&0^1^1^0^161&174&175&176\r"
                                                           + "QRF|MON||||0&0^1^1^0^178&179&180\r"
                                                           + "QRF|MON||||0&0^1^1^0^182&183&184\r"
                                                           + "QRF|MON||||0&0^1^1^0^186&187&188\r");

                Byte[] last = new byte[] { 0x1C, 0x0D };
                Byte[] full = new byte[head.Length + content.Length + last.Length];
                Stream s = new MemoryStream();
                s.Write(head, 0, 1);
                s.Write(content, 0, content.Length);
                s.Write(last, 0, 2);
                s.Position = 0;
                r = s.Read(full, 0, full.Length);
                //           ReceivedDataList.Items.Add(displaystr);
                //           SaveLog(displaystr);

                //Socket存在而且没有报错, 可以发送维持消息
                if (isexist_Miray & !SockectIsException_Miray & ClientSocket_Miray.Connected)
                {
                    try
                    {
                        IsSending = true;
                        ClientSocket_Miray.Send(full, full.Length, 0);
                        IsSending = false;

                    }
                    catch (Exception exception)
                    {
                        SockectIsException_Miray = true;
                        //clientSocket.Close();
                        //SaveLog("Socket 发送查询命令出错!!!!!" + exception.Message + "\r\n");
                    }

                }

            }
            MainTaincount_Miray++;
            
        }
        #endregion
        # region 理邦监护函数
        JHdataModel jhData = new JHdataModel();
        public void ReadLBlog()
        {
            try
            {
                using (StreamReader sr = new StreamReader("C:\\LBlog.txt", Encoding.Default))
                {
                    String str;
                    List<string> strList = new List<string>();
                    jhData.PR = 0; jhData.RR = 0;
                    jhData.HR = 0; jhData.SPO2 = 0; jhData.PR = 0; jhData.NIBP_SYS = 0; jhData.NIBP_DIA = 0; jhData.NIBP_MEAN = 0;
                    jhData.IBP_ART_SYS = 0; jhData.IBP_ART_DIA = 0; jhData.IBP_ART_MEAN = 0; jhData.CO2_EtCO2 = 0; jhData.AG_EtCO2 = 0; jhData.RR = 0;
                    jhData.T1 = 0.0; jhData.T2 = 0.0;
                    while ((str = sr.ReadLine()) != null)
                    {
                        if (str.Contains("HR="))
                        {
                            int ShuJu = DataValid.ToInt32(str.Replace("HR=", ""));
                            if (ShuJu != 0)
                            {
                                jhData.HR = ShuJu;
                            }
                           
                        }
                        
                        if (str.Contains("PR=") && !str.Contains("_PR="))
                        {
                            int ShuJu = DataValid.ToInt32(str.Replace("PR=", ""));
                            if (ShuJu != 0)
                            {
                                jhData.PR = ShuJu;
                            }
                            
                        }
                        if (str.Contains("RR="))
                        {
                            int ShuJu = DataValid.ToInt32(str.Replace("RR=", ""));
                            if (ShuJu != 0)
                            {
                                jhData.RR = ShuJu;
                            }
                        }
                        if (str.Contains("SPO2="))
                        {
                            int ShuJu = DataValid.ToInt32(str.Replace("SPO2=", ""));
                            if (ShuJu != 0)
                            {
                                jhData.SPO2 = ShuJu;
                            }
                        }
                        if (str.Contains("NIBP_SYS="))
                        {
                            int ShuJu = DataValid.ToInt32(str.Replace("NIBP_SYS=", ""));
                            if (ShuJu != 0)
                            {
                                jhData.NIBP_SYS = ShuJu;
                            }
                        }
                        if (str.Contains("NIBP_DIA="))
                        {
                            int ShuJu = DataValid.ToInt32(str.Replace("NIBP_DIA=", ""));
                            if (ShuJu != 0)
                            {
                                jhData.NIBP_DIA = ShuJu;
                            }
                        }
                        if (str.Contains("NIBP_MEAN="))
                        {
                            int ShuJu = DataValid.ToInt32(str.Replace("NIBP_MEAN=", ""));
                            if (ShuJu != 0)
                            {
                                jhData.NIBP_MEAN = ShuJu;
                            }
                        }
                        if (str.Contains("T1="))
                        {
                            double ShuJu = DataValid.ToDouble(str.Replace("T1=", ""));
                            if (ShuJu != 0.0)
                            {
                                jhData.T1 = ShuJu;
                            }
                        }
                        if (str.Contains("T2="))
                        {
                            double ShuJu = DataValid.ToDouble(str.Replace("T2=", ""));
                            if (ShuJu != 0.0)
                            {
                                jhData.T2 = ShuJu;
                            }
                        }
                        if (str.Contains("CO2_EtCO2="))
                        {
                            int ShuJu = DataValid.ToInt32(str.Replace("CO2_EtCO2=", ""));
                            if (ShuJu != 0)
                            {
                                jhData.CO2_EtCO2 = ShuJu;
                            }
                        }
                        if (str.Contains("IBP_ART_SYS="))
                        {
                            int ShuJu = DataValid.ToInt32(str.Replace("IBP_ART_SYS=", ""));
                            if (ShuJu != 0)
                            {
                                jhData.IBP_ART_SYS = ShuJu;
                            }
                        }
                        if (str.Contains("IBP_ART_DIA="))
                        {
                            int ShuJu = DataValid.ToInt32(str.Replace("IBP_ART_DIA=", ""));
                            if (ShuJu != 0)
                            {
                                jhData.IBP_ART_DIA = ShuJu;
                            }
                             
                        }
                        if (str.Contains("IBP_ART_MEAN="))
                        {
                            int ShuJu =  DataValid.ToInt32(str.Replace("IBP_ART_MEAN=", ""));
                            if (ShuJu != 0)
                            {
                                jhData.IBP_ART_MEAN = ShuJu;
                            }
                        }
                        if (str.Contains("AG_EtCO2="))
                        {
                            int ShuJu = DataValid.ToInt32(str.Replace("AG_EtCO2=", ""));
                            if (ShuJu != 0)
                            {
                                jhData.AG_EtCO2 = ShuJu;
                            }
                        }

                    }
                    double temp = 0.0;
                    if (jhData.T1 >= jhData.T2)
                    {
                        temp = jhData.T1;
                    }
                    else
                    {
                        temp = jhData.T2;
                    }
                    if (jhData.CO2_EtCO2 == 0)
                    {
                        jhData.CO2_EtCO2 = new Random().Next(32, 35);
                    }
                    string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    if (sqlite.selectJianCeData(now, mzjldID, 0).Rows.Count == 0)
                    {
                        int fa = 0;
                        if (jhData.NIBP_SYS != 0 && jhData.NIBP_DIA != 0 && jhData.NIBP_MEAN != 0)
                        {
                         //   fa = sqlite.insertJianCeDataMZJLD(mzjldID, jhData.NIBP_SYS, jhData.NIBP_DIA, jhData.NIBP_MEAN, jhData.RR, jhData.HR, jhData.PR, jhData.SPO2, jhData.CO2_EtCO2, temp, now);
                        }
                        else
                        {
                          //  fa = sqlite.insertJianCeDataMZJLD(mzjldID, jhData.IBP_ART_SYS, jhData.IBP_ART_DIA, jhData.IBP_ART_MEAN, jhData.RR, jhData.HR, jhData.PR, jhData.SPO2, jhData.CO2_EtCO2, temp, now);
                        }
                    }

                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }
        }

        #endregion


        [DllImport("wininet")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reserverdaValue);
        public bool IsInNet()
        {
            int isinnet;
            bool flags = InternetGetConnectedState(out isinnet, 0);
            return flags;
        }

        private void timer1_Tick(object sender, EventArgs e)//循环取得监护仪检测数据
        {
            int countN = sqlite.CopyData(mzjldID, ksjcTime);//拷贝本地数据到服务器
           if (countN == 0)
            {
                ksjcTime = ksjcTime.AddMinutes(1);
                timer1.Interval = 1000 * 1 * 60;
            }
            else
            {
                BindJHDian();
                ksjcTime = ksjcTime.AddMinutes(jcsjjg);//设置刷新时间
                timer1.Interval = 1000 * jcsjjg * 60;
                pictureBox3.Refresh();
                pictureBox2.Refresh();
                pictureBox5.Refresh();
            }

        }

        public void BindJHDian()//监护点赋值
        {
            ssyList.Clear(); szyList.Clear(); xlList.Clear(); twList.Clear(); hxlList.Clear();
            mboList.Clear(); spo2List.Clear(); etco2List.Clear(); jhxmValue.Clear();
            BIS.Clear();
            //RecordTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP
            DataTable datadt = dal.GetAdims_mzjld_Point(mzjldID);
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.point p1 = new adims_MODEL.point();//收缩压记录点
                p1.D = Convert.ToDateTime(datadt.Rows[i][0]);
                p1.V = Convert.ToInt32(datadt.Rows[i][1]);
                p1.Lx = 1;
                ssyList.Add(p1);
            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.point p2 = new adims_MODEL.point();//舒张压记录点
                p2.D = Convert.ToDateTime(datadt.Rows[i][0]);
                p2.V = Convert.ToInt32(datadt.Rows[i][2]);
                p2.Lx = 2;
                szyList.Add(p2);
            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.point p3 = new adims_MODEL.point();//脉搏记录点
                p3.D = Convert.ToDateTime(datadt.Rows[i][0]);
                p3.V = Convert.ToInt32(datadt.Rows[i][3]);
                p3.Lx = 3;
                mboList.Add(p3);
            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.point p4 = new adims_MODEL.point();//呼吸记录点
                p4.D = Convert.ToDateTime(datadt.Rows[i][0]);
                p4.V = Convert.ToInt32(datadt.Rows[i][4]);
                p4.Lx = 4;
                hxlList.Add(p4);
            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.tw_point p5 = new adims_MODEL.tw_point();//体温
                try
                {
                    p5.D = Convert.ToDateTime(datadt.Rows[i][0]);//体温记录点
                    if (datadt.Rows[i]["TEMP"].ToString().IsNullOrEmpty())
                    {
                        p5.V = 0;
                    }
                    else
                    {
                        p5.V = Convert.ToSingle(datadt.Rows[i]["TEMP"].ToString());
                    }

                }
                catch (Exception)
                {
                    p5.V = 0;
                }
                p5.Lx = 5;

                twList.Add(p5);
            }
            //for (int i = 0; i < datadt.Rows.Count; i++)
            //{
            //    adims_MODEL.tw_point p5 = new adims_MODEL.tw_point();//体温记录点
            //    p5.D = Convert.ToDateTime(datadt.Rows[i][0]);
            //    p5.T = datadt.Rows[i][5].ToString();
            //    p5.Lx = 5;
            //    twList.Add(p5);
            //}
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.point p6 = new adims_MODEL.point();//ETCO2记录点
                p6.D = Convert.ToDateTime(datadt.Rows[i][0]);
                if (CGUAN && !BGUAN)//如果插管，未拔管
                {
                    if (p6.D > cgTime)//记录点事件大于插管时间
                    {
                        p6.V = Convert.ToInt32(datadt.Rows[i]["ETCO2"]);
                        p6.Lx = 6;
                        etco2List.Add(p6);
                    }
                }
               
                if (CGUAN && BGUAN)//如果插管，已拔管
                {
                    if (p6.D > cgTime && p6.D < bgTime)//记录点事件大于插管时间并且小于拔管时间
                    {
                        p6.V = Convert.ToInt32(datadt.Rows[i]["ETCO2"]);
                        p6.Lx = 6;
                        etco2List.Add(p6);
                    }
                }
            }
            //for (int i = 0; i < datadt.Rows.Count; i++)
            //{
            //    adims_MODEL.point p6 = new adims_MODEL.point();//SpO2记录点
            //    p6.D = Convert.ToDateTime(datadt.Rows[i][0]);
            //    p6.V = Convert.ToInt32(datadt.Rows[i][6]);
            //    p6.Lx = 6;
            //    spo2List.Add(p6);
            //}
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.jhxm jhxmt = new adims_MODEL.jhxm();
                jhxmt.D = Convert.ToDateTime(datadt.Rows[i][0]);
                jhxmt.V = Convert.ToInt32(datadt.Rows[i]["SPO2"]);
                jhxmt.Sy = "SpO2";
                jhxmValue.Add(jhxmt);
            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.jhxm jhxmt = new adims_MODEL.jhxm();
                jhxmt.D = Convert.ToDateTime(datadt.Rows[i][0]);
                jhxmt.V = Convert.ToInt32(datadt.Rows[i]["ETCO2"]);
                jhxmt.Sy = "ETCO2";
                jhxmValue.Add(jhxmt);
            }
            int biszc=0;
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.jhxm jhxmt = new adims_MODEL.jhxm();
                if (datadt.Rows[i]["BIS"].ToString() =="")
                {
                    biszc=0;
                }
                else
                {
                    biszc =Convert.ToInt32(Convert.ToDouble(datadt.Rows[i]["BIS"].ToString()));
                }
                jhxmt.D = Convert.ToDateTime(datadt.Rows[i][0]);
                jhxmt.V = Convert.ToInt16(biszc);
                jhxmt.Sy = "BIS";
                jhxmValue.Add(jhxmt);
            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.jhxm jhxmt = new adims_MODEL.jhxm();
                jhxmt.D = Convert.ToDateTime(datadt.Rows[i][0]);
                if (datadt.Rows[i]["CVP"].ToString().IsNullOrEmpty())
                {
                    jhxmt.V = 0;
                }
                else
                {
                    jhxmt.V = Convert.ToInt32(datadt.Rows[i]["CVP"]);
                }
                jhxmt.Sy = "CVP";
                jhxmValue.Add(jhxmt);
            }
            //for (int i = 0; i < datadt.Rows.Count; i++)
            //{
            //    adims_MODEL.jhxm jhxmt = new adims_MODEL.jhxm();
            //    jhxmt.D = Convert.ToDateTime(datadt.Rows[i][0]);
            //    jhxmt.V = Convert.ToInt32(datadt.Rows[i]["TOF"]);
            //    jhxmt.Sy = "TOF";
            //    jhxmv.Add(jhxmt);
            //}

        }

        public void BindCLlist()// 出量赋值
        {
            chuniaoList.Clear();
            double cnl = 0;
            DataTable dtCL = dal.GetCL(mzjldID);
            if (dtCL.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in dtCL.Rows)
                {
                    adims_MODEL.clcxqt CLCXQT = new adims_MODEL.clcxqt();
                    chuniaoList.Add(CLCXQT);
                    chuniaoList[i].Id = Convert.ToInt32(dtCL.Rows[i][0]);
                    chuniaoList[i].D = Convert.ToDateTime(dtCL.Rows[i][4]);
                    chuniaoList[i].V = Convert.ToInt32(dtCL.Rows[i][3]);
                    cnl = cnl + chuniaoList[i].V;
                    chuniaoList[i].Lx = Convert.ToInt32(dtCL.Rows[i][2]);
                    i++;
                }
                //tbChuNiao.Text = cnl.ToString();
            }
        }

        private void btnSzsj_Click(object sender, EventArgs e)//术中时间管理
        {
            addSzsj F1 = new addSzsj(mzjldID, 0);
            F1.ShowDialog();
            BindSZSJ();
            pictureBox3.Refresh();
            listBox1.Focus();
        }

        private void btnTsyy_Click(object sender, EventArgs e)//特殊用药管理
        {
            addTsyy F1 = new addTsyy(mzjldID);
            F1.ShowDialog();
            BindTsyy();
            pictureBox3.Refresh();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            addZhenTong F1 = new addZhenTong(mzjldID);
            F1.ShowDialog();
            BindZhenTongYao();
            pictureBox3.Refresh();
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            iYema = 1;
            fy = 0;
            //printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            //printPreviewDialog1.WindowState = FormWindowState.Maximized;
            ////printDocument1.DefaultPageSettings.PaperSize =
            ////           new System.Drawing.Printing.PaperSize("K16", 740, 1020);  
            //printDocument1.DefaultPageSettings.PaperSize =
            //           new System.Drawing.Printing.PaperSize("A4", 820, 1160);
           
           

        }
        int fy = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {


            int x = 20, y = 0;//整体位置控制
            string title = "        浙江省金华市中医医院醉记录单";//标题
            //string title2 = "              ";//标题
            string title3 = "              麻 醉 小 结";//标题
            Pen ptp = Pens.Black;//普通画笔
            Pen pblack2 = new Pen(Brushes.Black, 2);
            Pen pxuxian = new Pen(Brushes.Black);
            pxuxian.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Font ptzt14 = new System.Drawing.Font("宋体", 14);//标题
            Font ptzt13 = new System.Drawing.Font("宋体", 13);//标题                      
            Font ptzt12 = new Font("宋体", 10);//填入栏目字体
            Font ptzt11 = new Font("宋体", 11);//填入栏目字体
            Font ptzt10 = new Font("宋体", 10);//填入栏目字体
            Font ptzt9 = new Font("宋体", 9);//填入栏目字体          
            Font ptzt8 = new Font("宋体", 8);//填入栏目字体
            Font ht7 = new Font("黑体", 7);//填入栏目字体
            Font htc = new Font("宋体", 16, FontStyle.Bold);
            Font ptzt7 = new Font("宋体", 7);//填入栏目字体           
            Font ptzt7_U = new Font("宋体", 7, FontStyle.Underline, GraphicsUnit.Point);//下划线字体
            Font ptzt6 = new Font("宋体", 6);//填入栏目字体
            Font ptzt5 = new Font("宋体", 5);//填入栏目字体
            Pen pred2 = new Pen(Brushes.Red, 2);
            Pen pblue2 = new Pen(Brushes.Blue, 2);
            int x8 = 0;
            #region  麻醉总结
            if (fy == 0)
            {
                DataTable dt = cll.mzjiazai(patID);//判断
               
                Font ptzt08 = new Font("宋体", 8);//普通字体 

                y = 20; x8 = 10; int y1 = y + 15;
                e.Graphics.DrawLine(Pens.Black, x8 + 5, y, x8 + 780, y);
                e.Graphics.DrawLine(Pens.Black, x8 + 5, y + 1020, x8 + 780, y + 1020);
                e.Graphics.DrawLine(Pens.Black, x8 + 5, y, x8 + 5, y + 1020);
                e.Graphics.DrawLine(Pens.Black, x8 + 780, y, x8 + 780, y + 1020);
                e.Graphics.DrawLine(Pens.Black, x8 + 35, y, x8 + 35, y + 1020);
                e.Graphics.DrawLine(Pens.Black, x8 + 390, y, x8 + 390, y + 360);
                e.Graphics.DrawString("椎\n管\n内\n麻\n醉\n操\n作", ptzt13, Brushes.Black, new Point(x8 + 10, y + 10));
                e.Graphics.DrawString("①穿刺时病人体位:□坐□右左侧卧□俯卧", ptzt12, Brushes.Black, new Point(x8 + 40, y + 3));
                e.Graphics.DrawString("②穿刺点:第一点:          间隙。", ptzt12, Brushes.Black, new Point(x8 + 40, y + 30));
                e.Graphics.DrawString("□成功□失败□穿破", ptzt12, Brushes.Black, new Point(x8 + 260, y + 30));
                e.Graphics.DrawString("第二点:          间隙。", ptzt12, Brushes.Black, new Point(x8 + 105, y + 60));
                e.Graphics.DrawString("□成功□失败□穿破", ptzt12, Brushes.Black, new Point(x8 + 260, y + 60));
                e.Graphics.DrawString("③□直入法□侧入法，斜面:□向头□向骶□向侧", ptzt12, Brushes.Black, new Point(x8 + 40, y + 90));
                e.Graphics.DrawString("④黄韧带感觉：□明显□尚明显□不明显", ptzt12, Brushes.Black, new Point(x8 + 40, y + 120));
                e.Graphics.DrawString("⑤负压:□大□小□无，搏动:□大□小□无", ptzt12, Brushes.Black, new Point(x8 + 40, y + 150));
                e.Graphics.DrawString("⑥注射阻力:□很松□尚松□不松。回流：□多□少□无。", ptzt12, Brushes.Black, new Point(x8 + 410, y + 3));
                e.Graphics.DrawString("⑦沾血：□无□有□穿刺□置管□淡□全血", ptzt12, Brushes.Black, new Point(x8 + 410, y + 30));

                e.Graphics.DrawString("⑧皮肤至硬膜外腔深度", ptzt12, Brushes.Black, new Point(x8 + 410, y + 60));
                e.Graphics.DrawString("导管插入深度", ptzt12, Brushes.Black, new Point(x8 + 600, y + 60));
                e.Graphics.DrawString("⑨导管插入情况:□顺利□不顺利，原因:", ptzt12, Brushes.Black, new Point(x8 + 410, y + 90));
                e.Graphics.DrawString("麻醉平面:手术始:上达:", ptzt12, Brushes.Black, new Point(x8 + 410, y + 120));
                e.Graphics.DrawString("    下达:", ptzt12, Brushes.Black, new Point(x8 + 600, y + 120));
                e.Graphics.DrawString("手术毕:上达", ptzt12, Brushes.Black, new Point(x8 + 475, y + 150));
                e.Graphics.DrawString("    下达:", ptzt12, Brushes.Black, new Point(x8 + 600, y + 150));

                for (int i = 0; i < 7; i++)
                {
                    if (i == 0 || i == 2 || i == 6)
                    {
                        e.Graphics.DrawLine(Pens.Black, x8 + 5, y + 180, x8 + 780, y + 180);
                    }
                    e.Graphics.DrawLine(Pens.Black, x8 + 35, y + 180, x8 + 780, y + 180);
                    y = y + 30;
                }
                y = 20;
                e.Graphics.DrawString("全\n麻\n操\n作", ptzt8, Brushes.Black, new Point(x8 + 10, y + 180));
                e.Graphics.DrawString("麻\n醉\n期\n间\n并\n发\n症", ptzt9, Brushes.Black, new Point(x8 + 10, y + 240));
                e.Graphics.DrawString("□口插管", ptzt08, Brushes.Black, new Point(x8 + 40, y + 190));
                e.Graphics.DrawString("鼻插管□左□右", ptzt08, Brushes.Black, new Point(x8 + 40, y + 220));
                e.Graphics.DrawString("□牙齿损伤", ptzt08, Brushes.Black, new Point(x8 + 40, y + 250));
                e.Graphics.DrawString("□黏膜损伤", ptzt08, Brushes.Black, new Point(x8 + 40, y + 280));
                e.Graphics.DrawString("□误吸", ptzt08, Brushes.Black, new Point(x8 + 40, y + 310));
                e.Graphics.DrawString("□呕吐", ptzt08, Brushes.Black, new Point(x8 + 40, y + 340));
                e.Graphics.DrawLine(Pens.Black, x8 + 120, y + 180, x8 + 120, y + 360);
                e.Graphics.DrawString("双腔管□左□右", ptzt08, Brushes.Black, new Point(x8 + 125, y + 190));
                e.Graphics.DrawString("□纤维镜", ptzt08, Brushes.Black, new Point(x8 + 125, y + 220));
                e.Graphics.DrawString("□舌下坠", ptzt08, Brushes.Black, new Point(x8 + 125, y + 250));
                e.Graphics.DrawString("□支气管痉挛", ptzt08, Brushes.Black, new Point(x8 + 125, y + 280));
                e.Graphics.DrawString("□急性肺水肿", ptzt08, Brushes.Black, new Point(x8 + 125, y + 310));
                e.Graphics.DrawString("□肺栓塞", ptzt08, Brushes.Black, new Point(x8 + 125, y + 340));
                e.Graphics.DrawLine(Pens.Black, x8 + 210, y + 180, x8 + 210, y + 360);
                e.Graphics.DrawString("□盲插", ptzt08, Brushes.Black, new Point(x8 + 215, y + 190));
                e.Graphics.DrawString("□带管芯插", ptzt08, Brushes.Black, new Point(x8 + 215, y + 220));
                e.Graphics.DrawString("□气胸", ptzt08, Brushes.Black, new Point(x8 + 215, y + 250));
                e.Graphics.DrawString("□缺氧", ptzt08, Brushes.Black, new Point(x8 + 215, y + 280));
                e.Graphics.DrawString("□CO2蓄积", ptzt08, Brushes.Black, new Point(x8 + 215, y + 310));
                e.Graphics.DrawString("□呼吸停止", ptzt08, Brushes.Black, new Point(x8 + 215, y + 340));
                e.Graphics.DrawLine(Pens.Black, x8 + 280, y + 180, x8 + 280, y + 360);
                e.Graphics.DrawString("□导管ID", ptzt08, Brushes.Black, new Point(x8 + 285, y + 190));
                e.Graphics.DrawString("□插管困难", ptzt08, Brushes.Black, new Point(x8 + 285, y + 220));
                e.Graphics.DrawString("□高血压", ptzt08, Brushes.Black, new Point(x8 + 285, y + 250));
                e.Graphics.DrawString("□低血压", ptzt08, Brushes.Black, new Point(x8 + 285, y + 280));
                e.Graphics.DrawString("□心律失常", ptzt08, Brushes.Black, new Point(x8 + 285, y + 310));
                e.Graphics.DrawString("□心衰", ptzt08, Brushes.Black, new Point(x8 + 285, y + 340));
                e.Graphics.DrawString("麻醉诱导期:□满意□呛咳□发绀□呕吐□喉痉挛□激动", ptzt08, Brushes.Black, new Point(x8 + 395, y + 190));

                e.Graphics.DrawString("其他：", ptzt08, Brushes.Black, new Point(x8 + 395, y + 220));

                e.Graphics.DrawString("□心跳骤停", ptzt08, Brushes.Black, new Point(x8 + 395, y + 250));
                e.Graphics.DrawString("□局麻药过敏", ptzt08, Brushes.Black, new Point(x8 + 395, y + 280));
                e.Graphics.DrawString("□局麻药中毒", ptzt08, Brushes.Black, new Point(x8 + 395, y + 310));
                e.Graphics.DrawString("□膈N阻滞", ptzt08, Brushes.Black, new Point(x8 + 395, y + 340));
                e.Graphics.DrawLine(Pens.Black, x8 + 485, y + 240, x8 + 485, y + 360);
                e.Graphics.DrawString("□喉返N阻滞", ptzt08, Brushes.Black, new Point(x8 + 490, y + 250));
                e.Graphics.DrawString("□霍纳氏症", ptzt08, Brushes.Black, new Point(x8 + 490, y + 280));
                e.Graphics.DrawString("□脊N广泛阻滞", ptzt08, Brushes.Black, new Point(x8 + 490, y + 310));
                e.Graphics.DrawString("□硬膜外导管折断", ptzt08, Brushes.Black, new Point(x8 + 490, y + 340));
                e.Graphics.DrawLine(Pens.Black, x8 + 600, y + 240, x8 + 600, y + 360);
                e.Graphics.DrawString("□全脊麻", ptzt08, Brushes.Black, new Point(x8 + 605, y + 250));
                e.Graphics.DrawString("□硬膜穿破", ptzt08, Brushes.Black, new Point(x8 + 605, y + 280));
                e.Graphics.DrawString("其他：", ptzt08, Brushes.Black, new Point(x8 + 605, y + 310));
                e.Graphics.DrawLine(Pens.Black, x8 + 680, y + 240, x8 + 680, y + 300);
                e.Graphics.DrawLine(Pens.Black, x8 + 680, y + 330, x8 + 680, y + 360);
                e.Graphics.DrawString("特\n殊\n病\n人\n麻\n醉\n及\n麻\n醉\n异\n常\n情\n况\n分\n析\n总\n结", ptzt9, Brushes.Black, new Point(x8 + 10, y + 420));

                e.Graphics.DrawLine(Pens.Black, x8 + 5, y + 890, x8 + 780, y + 890);
                e.Graphics.DrawString("说\n\n\n\n明", ptzt9, Brushes.Black, new Point(x8 + 10, y + 900));
                e.Graphics.DrawString("1.安/异/地/七：指各类氟醚", ptzt10, Brushes.Black, new Point(x8 + 50, y + 900));
                e.Graphics.DrawString("2.司/本/万/阿：指肌松药的第一字", ptzt10, Brushes.Black, new Point(x8 + 50, y + 930));
                e.Graphics.DrawString("3.病人送往：1.2.3.    1指病房   2指恢复室   3指重症监护室", ptzt10, Brushes.Black, new Point(x8 + 50, y + 960));
                e.Graphics.DrawString("4.评分标准按照《临床麻醉管理与技术规范》", ptzt10, Brushes.Black, new Point(x8 + 50, y + 990));

                e.Graphics.DrawString("麻醉总结审阅者：", ptzt10, Brushes.Black, new Point(x8 + 350, y + 1044));
                e.Graphics.DrawString("麻醉总结者：", ptzt10, Brushes.Black, new Point(x8 + 600, y + 1044));
                //数据
                #region


                //for (int a = 0; a < dt.Rows.Count; a++)  //  a行 b列
                //{

                    for (int c = 1; c < dt.Columns.Count; c++) // 列
                    {
                        string datas = dt.Rows[0][c].ToString();
                        if (c == 2)
                        {
                            e.Graphics.DrawString(datas, ptzt12, Brushes.Black, x8 + 151, y + 30);
                        }
                        if (c == 3)
                        {
                            e.Graphics.DrawString(datas, ptzt12, Brushes.Black, x8 + 152, y + 60);
                        }
                        if (c == 4)
                        {
                            e.Graphics.DrawString(datas + "cm", ptzt12, Brushes.Black, x8 + 550, y + 60);
                        }
                        if (c == 5)
                        {
                            e.Graphics.DrawString(datas + "cm", ptzt12, Brushes.Black, x8 + 685, y + 60);
                        }
                        if (c == 6)
                        {
                            e.Graphics.DrawString(datas, ptzt12, Brushes.Black, x8 + 666, y + 90);
                        }
                        if (c == 7)
                        {
                            e.Graphics.DrawString(datas, ptzt12, Brushes.Black, x8 + 560, y + 120);
                        }
                        if (c == 8)
                        {
                            e.Graphics.DrawString(datas, ptzt12, Brushes.Black, x8 + 666, y + 120);
                        }
                        if (c == 9)
                        {
                            e.Graphics.DrawString(datas, ptzt12, Brushes.Black, x8 + 560, y + 150);
                        }
                        if (c == 10)
                        {
                            e.Graphics.DrawString(datas, ptzt12, Brushes.Black, x8 + 666, y + 150);
                        }
                        if (c == 11)
                        {
                            e.Graphics.DrawString(datas, ptzt08, Brushes.Black, x8 + 425, y + 220);
                        }
                        if (c == 12)
                        {
                            e.Graphics.DrawString(datas, ptzt08, Brushes.Black, x8 + 635, y + 310);
                        }
                        if (c == 13)
                        {
                            string temp = "";
                            int len1 = 0;
                            int k = len1, num2 = len1;
                            SubString(dt.Rows[0]["cctw"].ToString(), 52, k, out temp, out num2);//数据到什么位置换行
                            len1 = num2;
                            e.Graphics.DrawString(temp, ptzt10, Brushes.Black, x8 + 40, y + 370);
                        }
                        if (c == 14)
                        {
                            e.Graphics.DrawString(datas, ptzt10, Brushes.Black, x8 + 470, y + 1044);
                        }
                        if (c == 15)
                        {
                            e.Graphics.DrawString(datas, ptzt10, Brushes.Black, x8 + 690, y + 1044);
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
                                    e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 160, y));
                                }
                                else if (sj == "2")
                                {
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 188, y));
                                }
                                else if (sj == "3")
                                {
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 257, y));
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
                                   e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 261, y + 28));

                                }
                                else if (sj == "2")
                                {
                                    e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 304, y + 28));

                                }
                                else if (sj == "3")
                                {
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 346, y + 28));
                                }
                                else if (sj == "4")
                                {
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 261, y + 58));

                                }
                                else if (sj == "5")
                                {
                                    e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 304, y + 58));

                                }
                                else if (sj == "6")
                                {
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 346, y + 58));
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
                                    e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 55, y + 88));
                                }
                                else if (sj == "2")
                                {
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 112, y + 88));
                                }
                                else if (sj == "3")
                                {
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 216, y + 88));
                                }
                                else if (sj == "4")
                                {
                                    e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 258, y + 88));
                                }
                                else if (sj == "5")
                                {
                                    e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 299, y + 88));
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
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 140, y + 118));


                                }
                                else if (sj == "2")
                                {
                                    e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 181, y + 118));


                                }
                                else if (sj == "3")
                                {
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 237, y + 118));
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
                                    e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 90, y + 148));
                                }
                                else if (sj == "2")
                                {
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 119, y + 148));
                                }
                                else if (sj == "3")
                                {
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 146, y + 148));
                                }
                                else if (sj == "4")
                                {
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 222, y + 148));

                                }
                                else if (sj == "5")
                                {
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 251, y + 148));

                                }
                                else if (sj == "6")
                                {
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 280, y + 148));
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
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 488, y));
                                }
                                else if (sj == "2")
                                {
                                    e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 531, y));
                                }
                                else if (sj == "3")
                                {
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 571, y));
                                }
                                else if (sj == "4")
                                {
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 669, y));
                                }
                                else if (sj == "5")
                                {
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 697, y));
                                }
                                else if (sj == "6")
                                {
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 724, y));
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
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 468, y + 28));

                                }
                                else if (sj == "2")
                                {
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 494, y + 28));

                                }
                                else if (sj == "3")
                                {
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 524, y + 28));

                                }
                                else if (sj == "4")
                                {
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 564, y + 28));

                                }
                                else if (sj == "5")
                                {
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 606, y + 28));

                                }
                                else if (sj == "6")
                                {
                                     e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 634, y + 28));
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
                                    e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 516, y + 88));

                                }
                                else if (sj == "2")
                                {
                                    e.Graphics.DrawString("✔", ptzt10, Brushes.Black, new Point(x8 + 558, y + 88));
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
                                     e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 40, y + 187));
                                }
                                else if (sj == "2")
                                {
                                     e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 161, y + 187));
                                }
                                else if (sj == "3")
                                {
                                    e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 184, y + 187));
                                }
                                else if (sj == "4")
                                {
                                    e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 217, y + 187));
                                }
                                else if (sj == "5")
                                {
                                    e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 287, y + 187));
                                }
                                else if (sj == "6")
                                {
                                     e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 74, y + 217));

                                }
                                else if (sj == "7")
                                {
                                     e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 97, y + 217));
                                }
                                else if (sj == "8")
                                {
                                     e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 127, y + 217));
                                }
                                else if (sj == "9")
                                {
                                    e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 217, y + 217));

                                }
                                else if (sj == "a")
                                {
                                    e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 287, y + 217));
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
                                    e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 458, y + 187));
                                }
                                else if (sj == "2")
                                {
                                     e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 490, y + 187));

                                }
                                else if (sj == "3")
                                {
                                     e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 524, y + 187));
                                }
                                else if (sj == "4")
                                {
                                     e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 558, y + 187));
                                }
                                else if (sj == "5")
                                {
                                    e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 592, y + 187));

                                }
                                else if (sj == "6")
                                {
                                     e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 637, y + 187));
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
                                    e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 42, y + 247));
                                }
                                else if (sj == "2")
                                {
                                     e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 127, y + 247));
                                }
                                else if (sj == "3")
                                {
                                    e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 217, y + 247));
                                }
                                else if (sj == "4")
                                {

                                     e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 287, y + 247));
                                }
                                else if (sj == "5")
                                {
                                    e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 397, y + 247));
                                }
                                else if (sj == "6")
                                {
                                     e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 492, y + 247));
                                }
                                else if (sj == "7")
                                {

                                     e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 607, y + 247));
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
                                   e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 42, y + 277));
                                }
                                else if (sj == "2")
                                {
                                    e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 127, y + 277));
                                }
                                else if (sj == "3")
                                {

                                    e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 217, y + 277));
                                }
                                else if (sj == "4")
                                {
                                    e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 287, y + 277));
                                }
                                else if (sj == "5")
                                {
                                    e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 397, y + 277));
                                }
                                else if (sj == "6")
                                {

                                    e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 492, y + 277));
                                }
                                else if (sj == "7")
                                {
                                     e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 607, y + 277));
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
                                     e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 42, y + 307));
                                }
                                else if (sj == "2")
                                {

                                    e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 127, y + 307));
                                }
                                else if (sj == "3")
                                {
                                     e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 217, y + 307));
                                }
                                else if (sj == "4")
                                {

                                    e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 287, y + 307));
                                }
                                else if (sj == "5")
                                {
                                    e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 397, y + 307));
                                }
                                else if (sj == "6")
                                {
                                    e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 492, y + 307));
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
                                     e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 42, y + 337));
                                }
                                else if (sj == "2")
                                {
                                     e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 127, y + 337));
                                }
                                else if (sj == "3")
                                {

                                     e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 217, y + 337));
                                }
                                else if (sj == "4")
                                {
                                     e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 287, y + 337));
                                }
                                else if (sj == "5")
                                {
                                     e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 397, y + 337));
                                }
                                else if (sj == "6")
                                {
                                    e.Graphics.DrawString("✔", ptzt8, Brushes.Black, new Point(x8 + 492, y + 337));
                                }
                            }
                        }
                    
                }
                #endregion
                   // if (y + 800 > 800)
              //  {
                    e.HasMorePages = true;
                    fy = fy + 1;
                    x = 0; y = 0;
                    return;
              //  }

            }
            #endregion
            //else 
            //{
            //    e.Graphics.DrawString(title, ptzt13, Brushes.Black, new Point(120 + x, 20 + y));
            //}
            else
            {
                e.Graphics.DrawString(title, htc, Brushes.Black, new Point(155 + x, 35 + y));
                int Y_unLine = y + 83, YY = y + 70;
                e.Graphics.DrawString("科别：" + txtBingQu.Controls[0].Text, ptzt8, Brushes.Black, new Point(x, YY));
                e.Graphics.DrawLine(ptp, new Point(30 + x, Y_unLine), new Point(130 + x, Y_unLine));
                e.Graphics.DrawString("床号：" + txtBednumber.Controls[0].Text, ptzt8, Brushes.Black, new Point(160 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(190 + x, Y_unLine), new Point(230 + x, Y_unLine));
                e.Graphics.DrawString("住院号：" + txtZhuYuanHao.Controls[0].Text, ptzt8, Brushes.Black, new Point(240 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(280+ x, Y_unLine), new Point(390 + x, Y_unLine));
                e.Graphics.DrawString("日期：" + dtOdate.Value.ToString("yyyy年MM月dd日"), ptzt8, Brushes.Black, new Point(420 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(450 + x, Y_unLine), new Point(530 + x, Y_unLine));
                e.Graphics.DrawString("医疗费：" , ptzt8, Brushes.Black, new Point(550 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(590 + x, Y_unLine), new Point(730 + x, Y_unLine));
                //↑画标题一块的东西
                Y_unLine = YY + 15; YY = YY + 20;
                e.Graphics.DrawLine(pblack2, new Point(20 + x, 90 + y), new Point(770 + x, 90 + y));
                e.Graphics.DrawLine(pblack2, new Point(20 + x, 90 + y), new Point(20 + x, 1050 + y));
                e.Graphics.DrawLine(pblack2, new Point(770 + x, 90 + y), new Point(770 + x, 1050 + y));
                e.Graphics.DrawLine(pblack2, new Point(20 + x, 1050 + y), new Point(770 + x, 1050 + y));
                //↑画边框

                #region 打印病人基本信息
                YY = YY + 5; Y_unLine = YY + 13;
                e.Graphics.DrawString("姓名  " + txtName.Controls[0].Text, ptzt8, Brushes.Black, new Point(25 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(50 + x, Y_unLine), new Point(150 + x, Y_unLine));
                e.Graphics.DrawString("性别  " + txtSex.Controls[0].Text, ptzt8, Brushes.Black, new Point(160 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(185 + x, Y_unLine), new Point(220 + x, Y_unLine));
                e.Graphics.DrawString("年龄  " + txtAge.Controls[0].Text+ "岁", ptzt8, Brushes.Black, new Point(230 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(255 + x, Y_unLine), new Point(290 + x, Y_unLine));
                e.Graphics.DrawString("身高  " + txtHeight.Controls[0].Text, ptzt8, Brushes.Black, new Point(300 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(325 + x, Y_unLine), new Point(360 + x, Y_unLine));
                e.Graphics.DrawString("cm   体重  " + txtWeight.Controls[0].Text, ptzt8, Brushes.Black, new Point(360 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(420 + x, Y_unLine), new Point(460 + x, Y_unLine));
                e.Graphics.DrawString("公斤       体温：  " + txtTW.Controls[0].Text, ptzt8, Brushes.Black, new Point(460 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(560 + x, Y_unLine), new Point(670 + x, Y_unLine));
                e.Graphics.DrawString("℃", ptzt8, Brushes.Black, new Point(670 + x, YY));
                YY = YY + 20; Y_unLine = YY + 13;
                e.Graphics.DrawString("血压  " + txtXueya.Controls[0].Text, ptzt8, Brushes.Black, new Point(25 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(50 + x, Y_unLine), new Point(150 + x, Y_unLine));
                e.Graphics.DrawString("mmHg     呼吸   " + txtHuxi.Controls[0].Text, ptzt8, Brushes.Black, new Point(160 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(245 + x, Y_unLine), new Point(310 + x, Y_unLine));
                e.Graphics.DrawString("次/分    脉搏  " + txtMaibo.Controls[0].Text, ptzt8, Brushes.Black, new Point(320 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(405 + x, Y_unLine), new Point(450 + x, Y_unLine));
                e.Graphics.DrawString("次/分    血型  " + cmbXueXing.Text, ptzt8, Brushes.Black, new Point(460 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(545 + x, Y_unLine), new Point(590 + x, Y_unLine));
                e.Graphics.DrawString("ASA分级  " + cmbASA.Text, ptzt8, Brushes.Black, new Point(600 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(645 + x, Y_unLine), new Point(720 + x, Y_unLine));
                //e.Graphics.DrawRectangle(ptp, 160 + x, YY, 12, 12);
                //e.Graphics.DrawRectangle(ptp, 220 + x, YY, 12, 12);
                //e.Graphics.DrawString("急诊", ptzt8, Brushes.Black, new Point(180 + x, YY));
                //e.Graphics.DrawString("择期", ptzt8, Brushes.Black, new Point(240 + x, YY));
                //if (cbJizhen.Checked)
                //    e.Graphics.DrawLines(pblue2, new Point[] { new Point(155 + x, YY), new Point(165 + x, YY + 12), new Point(180 + x, YY) });
                //if (cbZeqi.Checked)
                //    e.Graphics.DrawLines(pblue2, new Point[] { new Point(215 + x, YY), new Point(225 + x, YY + 12), new Point(240 + x, YY) });

                //e.Graphics.DrawString("术前禁食  " + this.cmbSQJinshi.Text, ptzt8, Brushes.Black, new Point(300 + x, YY));
                //e.Graphics.DrawLine(ptp, new Point(350 + x, Y_unLine), new Point(400 + x, Y_unLine));
                //e.Graphics.DrawRectangle(ptp, 510 + x, YY - 3, 250, 78);

                //e.Graphics.DrawString("术前特殊情况：", ptzt8, Brushes.Black, new Point(520 + x, YY));
                //if (string.IsNullOrEmpty(txtTSBQing.Controls[0].Text.Trim()))
                //    txtTSBQing.Controls[0].Text = " 无";
                //string str = "";
                //int StrLength = txtTSBQing.Controls[0].Text.Trim().Length;
                //int row = StrLength / 20;
                //for (int i = 0; i <= StrLength / 20; i++)//25个字符就换行
                //{
                //    if (i < row)
                //        str += txtTSBQing.Controls[0].Text.Trim().Substring(i * 20, 20) + Environment.NewLine; //从第i*20个开始，截取20个字符串
                //    else
                //        str += txtTSBQing.Controls[0].Text.Trim().Substring(i * 20);
                //}
                //e.Graphics.DrawString(str, ptzt8, Brushes.Black, new Point(515 + x, YY + 15));


                YY = YY + 20; Y_unLine = YY + 13;
                e.Graphics.DrawString("术前诊断 " + txtSqzd.Controls[0].Text, ptzt8, Brushes.Black, new Point(25 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(70 + x, Y_unLine), new Point(400 + x, Y_unLine));
                e.Graphics.DrawString("拟施手术 " + txtNssss.Controls[0].Text, ptzt8, Brushes.Black, new Point(410 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(460 + x, Y_unLine), new Point(730 + x, Y_unLine));

                YY = YY + 20; Y_unLine = YY + 13;
                //if (string.IsNullOrEmpty(txtSqyy.Controls[0].Text.Trim()))
                //    txtSqyy.Controls[0].Text = "  无";
                //e.Graphics.DrawString("麻醉前用药 " + txtSqyy.Controls[0].Text, ptzt8, Brushes.Black, new Point(25 + x, YY));
                //e.Graphics.DrawLine(ptp, new Point(80 + x, Y_unLine), new Point(300 + x, Y_unLine));

                e.Graphics.DrawString("手术体位 " + this.cmbTiwei.Text, ptzt8, Brushes.Black, new Point(25 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(70 + x, Y_unLine), new Point(160 + x, Y_unLine));
                e.Graphics.DrawString("术前禁食  " + this.cmbSQJinshi.Text, ptzt8, Brushes.Black, new Point(200 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(250 + x, Y_unLine), new Point(300 + x, Y_unLine));

                YY = YY + 20; Y_unLine = YY + 13;
                e.Graphics.DrawLine(ptp, new Point(20 + x, YY), new Point(770 + x, YY));
                //↑画边框
                #endregion

                #region 打印时间和分页
                DateTime dtnow = new DateTime();//打印截止时间判断        
                DateTime pagetime = new DateTime();
                DataTable dtMax = bll.GetMaxPoint(mzjldID);
                if (dtMax.Rows[0][0].ToString().IsNullOrEmpty())
                    dtnow = DateTime.Now;
                else
                    dtnow = Convert.ToDateTime(dtMax.Rows[0][0]);
                pagetime = ptime; //当前打印页时间           
                e.Graphics.DrawString("时间(分钟)", ptzt8, Brushes.Black, new Point(30 + x, YY + 2));
                for (int i = 0; i < 8; i++) //打印检测时间
                {
                    e.Graphics.DrawString(ptime.ToString("HH:mm"), ptzt8, Brushes.Black, new Point(177 + 72 * i + x, YY + 2));
                    ptime = ptime.AddMinutes(6 * jcsjjg);
                }
                if (ptime < dtnow)
                {
                    e.HasMorePages = true;
                    e.Graphics.DrawString("第 " + iYema.ToString() + " 页", ptzt8, Brushes.Black, new Point(340 + x, 1075 + y));
                    iYema++;
                }
                else
                {
                    e.HasMorePages = false; ptime = fristopen;
                    e.Graphics.DrawString("第 " + iYema.ToString() + " 页", ptzt8, Brushes.Black, new Point(340 + x, 1075 + y));
                }
                #endregion

                #region  打印用药区域
                YY = YY + 18;
                for (int i = 0; i < 7; i++)//画横实线
                {
                    if (i == 0 || i == 6)//|| i == 20
                        e.Graphics.DrawLine(ptp, new Point(20 + x, YY + 12 * i + y), new Point(770 + x, YY + 12 * i + y));
                    //else if (i == 33)
                    //    e.Graphics.DrawLine(ptp, new Point(35 + x, YY + 12 * i + y), new Point(700 + x, YY + 12 * i + y));
                    else
                        e.Graphics.DrawLine(ptp, new Point(35 + x, YY + 12 * i + y), new Point(770 + x, YY + 12 * i + y));
                }
                e.Graphics.DrawLine(ptp, new Point(35 + x, YY + y), new Point(35 + x, YY + 27 * 12 + y));

                for (int i = 0; i < 10; i++)//画竖实线
                {
                    e.Graphics.DrawLine(ptp, new Point(192 + x + 72 * i, YY - 2 + y), new Point(192 + x + 72 * i, YY + 27 * 12 + y));
                }
                for (int i = 1; i < 48; i++)//画竖虚线
                {
                    e.Graphics.DrawLine(pxuxian, new PointF(192 + x + 12 * i, YY + y), new Point(192 + x + 12 * i, YY + 27 * 12 + y));

                }
                e.Graphics.DrawString("监\n\n\n测", ptzt8, Brushes.Black, new Point(21 + x, YY+10));
               // e.Graphics.DrawString("用\n\n\n\n\n\n药", ptzt7, Brushes.Black, new Point(22 + x, YY + 42));
               // e.Graphics.DrawString("输\n\n液\n\n输\n\n血", ptzt7, Brushes.Black, new Point(22 + x, YY + 12 * 13 + 2));
               //// e.Graphics.DrawString("出\n\n量", ptzt7, Brushes.Black, new Point(22 + x, YY + 12 * 20 + 2));
               // //e.Graphics.DrawString("失血量ml", ptzt7, Brushes.Black, new Point(37 + x, YY + 12 * 20 + 2));
               // //e.Graphics.DrawString("尿  量ml", ptzt7, Brushes.Black, new Point(37 + x, YY + 12 * 20 + 15));
               // //e.Graphics.DrawString("入  量ml", ptzt7, Brushes.Black, new Point(37 + x, YY + 12 * 20 + 27));
               // e.Graphics.DrawString("\n监\n\n\n\n测\n\n\n\n项\n\n\n\n目", ptzt7, Brushes.Black, new Point(22 + x, YY + 12 * 22 + 2));

               // //e.Graphics.DrawString("SP02(%)", ptzt7, Brushes.Black, new Point(40 + x, YY + 12 * 34 + 2));
               // e.Graphics.DrawString("02 (吸入)", ptzt7, Brushes.Black, new Point(40 + x, YY + 2));
               // #endregion
               // #region 打印气体
               // ArrayList sssQT = new ArrayList();
               // int qti = 0;   //起步位置
               // foreach (adims_MODEL.mzqt mzqt in mzqtList)
               // {
               //     if (sssQT.Contains(mzqt.Qtname))
               //         qti = qti - 1;
               //     //if (!sssQT.Contains(mzqt.Qtname))
               //     //    e.Graphics.DrawString(mzqt.Qtname, mzqt.Qtname.Length < 7 ? ptzt6 : ptzt5, Brushes.Black, new PointF(32 + x, YY + qti * 12 + y + 3));

               //     if (mzqt.Bz == 2)
               //     {
               //         TimeSpan t = new TimeSpan();
               //         TimeSpan t1 = new TimeSpan();
               //         t1 = mzqt.Jssj - pagetime;
               //         t = mzqt.Sysj - pagetime;
               //         int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120) + x;
               //         int y1 = YY + qti * 12 + 4;
               //         int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 11 / jcsjjg + 120) + x;
               //         //double qtzongliang = (mzqt.Yl) * Convert.ToDouble((x2 - x1) / 2.5);
               //         //e.Graphics.DrawString(Convert.ToInt32(qtzongliang).ToString() + "L", this.Font, Brushes.Blue, new Point(763, y1 - 2));
               //         if (x1 > 120 + x && x1 < 780 + x)
               //         {
               //             e.Graphics.DrawString(mzqt.Yl.ToString() + " " + mzqt.Dw, ptzt6, Brushes.Blue, new Point(x1 + 3, y + y1 - 6));
               //             e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1 + y), new Point(x1 - 3, y1 + 6 + y), new Point(x1 + 3, y1 + 6 + y) });
               //         }
               //         if (x2 > 120 + x && x2 < 780 + x)
               //         {
               //             e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1 + y), new Point(x2 - 3, y1 + 6 + y), new Point(x2 + 3, y1 + 6 + y) });
               //         }
               //         if (x1 > 120 + x && x1 < 780 + x && x2 > 120 + x && x2 < 780 + x)
               //         {
               //             e.Graphics.DrawLine(pred2, new Point(x1, y1 + y + 3), new Point(x2, y1 + y + 3));
               //         }
               //         if (x1 > 120 + x && x1 < 780 + x && x2 > 780 + x)
               //         {
               //             e.Graphics.DrawLine(pred2, new Point(x1, y1 + y + 3), new Point(780 + x, y1 + y + 3));
               //         }
               //         if (x1 < 120 + x && x2 > 120 + x && x2 < 780 + x)
               //         {
               //             e.Graphics.DrawLine(pred2, new Point(120 + x, y1 + y + 3), new Point(x2, y1 + y + 3));
               //         }
               //         if (x1 < 120 + x && x2 > 780 + x)
               //         {
               //             e.Graphics.DrawLine(pred2, new Point(120 + x, y1 + y + 3), new Point(780 + x, y1 + y + 3));
               //         }
               //     }
               //     qti++;
               //     sssQT.Add(mzqt.Qtname);
               // }

               // #endregion
               // #region 打印全麻药
               // ArrayList sssYDY = new ArrayList();
               // int ydyi = 1;
               // foreach (adims_MODEL.mzyt mzyt in ydyList) //打印全麻药
               // {
               //     if (sssYDY.Contains(mzyt.Ytname))
               //         ydyi = ydyi - 1;
               //     if (!sssYDY.Contains(mzyt.Ytname))
               //         e.Graphics.DrawString(mzyt.Ytname + " " + mzyt.Dw, mzyt.Ytname.Length < 7 ? ptzt7 : ptzt5, Brushes.Black, new PointF(37 + x, YY + ydyi * 12 + y + 3));

               //     if (mzyt.Cxyy == false)
               //     {
               //         TimeSpan t = new TimeSpan();
               //         t = mzyt.Sysj - pagetime;
               //         int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120);
               //         int y1 = YY + ydyi * 12 + 4;
               //         if (x1 > 100 + x && x1 < 700 + x)
               //         {
               //             e.Graphics.DrawString(mzyt.Yl.ToString(), ptzt7, Brushes.Blue, new Point(x1 + 3 + x, y + y1 - 6));
               //             e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), new Point(x1 - 3 + x, y1 + 6 + y), new Point(x1 + 3 + x, y1 + 6 + y) });
               //         }
               //     }
               //     if (mzyt.Cxyy == true)
               //     {
               //         TimeSpan t = new TimeSpan();
               //         TimeSpan t1 = new TimeSpan();
               //         t1 = mzyt.Jssj - pagetime;
               //         t = mzyt.Sysj - pagetime;
               //         int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120);
               //         int y1 = YY + ydyi * 12 + 4;
               //         int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 11 / jcsjjg + 120);
               //         if (x1 > 120 + x && x1 < 780 + x)
               //         {
               //             e.Graphics.DrawString(mzyt.Yl.ToString(), ptzt7, Brushes.Blue, new Point(x1 + 3 + x, y + y1 - 6));
               //             e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), new Point(x1 - 3 + x, y1 + 6 + y), new Point(x1 + 3 + x, y1 + 6 + y) });
               //         }
               //         if (x2 > 120 + x && x2 < 780 + x)
               //         {
               //             e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2 + x, y1 + y), new Point(x2 - 3 + x, y1 + 6 + y), new Point(x2 + 3 + x, y1 + 6 + y) });
               //         }
               //         if (x1 > 120 + x && x1 < 780 + x && x2 > 120 + x && x2 < 780 + x)
               //         {
               //             e.Graphics.DrawLine(pred2, new Point(x1 + x, y1 + y + 3), new Point(x2 + x, y1 + y + 3));
               //         }
               //         if (x1 > 120 + x && x1 < 780 + x && x2 > 780 + x)
               //         {
               //             e.Graphics.DrawLine(pred2, new Point(x1 + x, y1 + y + 3), new Point(780 + x, y1 + y + 3));
               //         }
               //         if (x1 < 120 + x && x2 > 120 + x && x2 < 780 + x)
               //         {
               //             e.Graphics.DrawLine(pred2, new Point(120 + x, y1 + y + 3), new Point(x2 + x, y1 + y + 3));
               //         }
               //         if (x1 < 120 + x && x2 > 780 + x)
               //         {
               //             e.Graphics.DrawLine(pred2, new Point(120 + x, y1 + y + 3), new Point(780 + x, y1 + y + 3));
               //         }
               //     }
               //     ydyi++;
               //     sssYDY.Add(mzyt.Ytname);
               // }
               // #endregion

               // #region 打印局麻药
               // //int jti = 20;  //打印局麻药
               // //ArrayList sssJMY = new ArrayList();
               // //foreach (adims_MODEL.jtytsx jt in jmyList)
               // //{
               // //    if (sssJMY.Contains(jt.Name))
               // //        jti = jti - 1;
               // //    if (!sssJMY.Contains(jt.Name))
               // //        e.Graphics.DrawString(jt.Name + " " + jt.Dw, jt.Name.Length < 7 ? ptzt7 : ptzt5, Brushes.Black, new PointF(37 + x, YY + jti * 12 + y + 2));

               // //    TimeSpan t = new TimeSpan();
               // //    t = jt.Kssj - pagetime;
               // //    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120);
               // //    int y1 = YY + jti * 12 + 4;
               // //    if (x1 > 120 + x && x1 < 780 + x)
               // //    {
               // //        e.Graphics.DrawString(jt.Jl.ToString(), ptzt7, Brushes.Blue, new Point(x1 + 3 + x, y + y1 - 6));
               // //        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), new Point(x1 - 3 + x, y1 + 6 + y), new Point(x1 + 3 + x, y1 + 6 + y) });
               // //    }
               // //    jti++;
               // //    sssJMY.Add(jt.Name);
               // //}
               // #endregion

               // #region 打印输液
               // int syi = 13;
               // ArrayList sssSY = new ArrayList();
               // foreach (adims_MODEL.shuye sx in shuyeList)
               // {
               //     if (sssSY.Contains(sx.Name))
               //         syi = syi - 1;
               //     if (!sssSY.Contains(sx.Name))
               //         e.Graphics.DrawString(sx.Name + " " + sx.Dw, sx.Name.Length < 9 ? ptzt7 : ptzt5, Brushes.Black, new PointF(37 + x, YY + syi * 12 + y + 3));

               //     TimeSpan t = new TimeSpan();
               //     TimeSpan t1 = new TimeSpan();
               //     t1 = pagetime.AddMinutes(60 * jcsjjg) - pagetime;
               //     t = sx.Kssj - pagetime;
               //     //e.Graphics.DrawString(sx.Name.ToString(), ptzt7, Brushes.Black, 37 + x, YY + syi * 12 + 2);
               //     int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120);
               //     int y1 = YY + syi * 12 + 5;
               //     int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 11 / jcsjjg + 120);

               //     if (x1 > 120 + x && x1 < 780 + x)
               //     {
               //         e.Graphics.DrawString(sx.Jl.ToString(), ptzt7, Brushes.Blue, x1 + x, y + y1 - 8);
               //         e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), 
               //         new Point(x1 - 3 + x, y1 + 5 + y), new Point(x1 + 3 + x, y1 + 5 + y) });
               //     }
               //     syi++;
               //     sssSY.Add(sx.Name);
               // }

               // #endregion

               // #region 打印输血
               // //打印输血
               // int sxi = 18;
               // ArrayList sssSX = new ArrayList();
               // foreach (adims_MODEL.shuxue sx in shuxueList)
               // {
               //     if (sssSX.Contains(sx.Name))
               //         sxi = sxi - 1;
               //     if (!sssSX.Contains(sx.Name))
               //         e.Graphics.DrawString(sx.Name + " " + sx.Dw, sx.Name.Length < 9 ? ptzt7 : ptzt5, Brushes.Black, new PointF(37 + x, YY + sxi * 12 + y + 3));

               //     TimeSpan t = new TimeSpan();
               //     TimeSpan t1 = new TimeSpan();
               //     t1 = pagetime.AddHours(jcsjjg) - pagetime;
               //     t = sx.Kssj - pagetime;
               //     //e.Graphics.DrawString(sx.Name.ToString(), ptzt7, Brushes.Black, 37 + x, YY + sxi * 12 + 2);
               //     int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120);
               //     int y1 = YY + sxi * 12 + 5;
               //     int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 11 / jcsjjg + 120);
               //     if (x1 > 120 + x && x1 < 780 + x)
               //     {
               //         e.Graphics.DrawString(sx.Jl.ToString(), ptzt7, Brushes.Blue, x1 + x, y + y1 - 6);
               //         e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), 
               //         new Point(x1 - 3 + x, y1 + 5 + y), new Point(x1 + 3 + x, y1 + 5 + y) });
               //     }
               //     sxi++;
               //     sssSX.Add(sx.Name);
               // }
               // #endregion
               // #region<<打印失血>>
               // //e.Graphics.DrawString("失 血(ml)", ptzt7, Brushes.Black, x + 37, YY + 15 * 12 + y + 3);
               // int cx_count = 0;
               // foreach (adims_MODEL.clcxqt cx in cxList)
               // {
               //     if (cx.V > 0)
               //     {
               //         if (cx.D >= pagetime && cx.D <= pagetime.AddMinutes(60 * jcsjjg))
               //         {
               //             TimeSpan t = new TimeSpan();
               //             t = cx.D - pagetime;

               //             float jhx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 132);
               //             float jhy = YY + 12 * 20;
               //             //if (cx_count % 5 == 0)
               //             {
               //                 if (cx.V.ToString().Length > 3)
               //                 {
               //                     e.Graphics.FillRectangle(Brushes.Pink, jhx + 3, jhy, 16, 9);
               //                 }
               //                 else
               //                 {
               //                     e.Graphics.FillRectangle(Brushes.Pink, jhx + 3, jhy, 9, 9);
               //                 }
               //                 e.Graphics.DrawString(cx.V.ToString(), cx.V.ToString().Length > 3 ? ptzt6 : ptzt7, Brushes.Black, new PointF(jhx + 5, jhy));

               //             }
               //             cx_count++;
               //         }
               //     }
               //     else
               //     {
               //         //int xxx = 112;
               //         //int jhyyy = 445;
               //         //e.Graphics.DrawLine(ptp, new Point(xxx, jhyyy), new Point(xxx + 30, jhyyy - 10));
               //     }
               // }
               // #endregion
               // //e.Graphics.DrawString("引流量(ml)", ptzt7, Brushes.Black, x + 37, YY + 16 * 12 + y + 3);

               // #region<<打印入量>>

               // int cn_count = 0;
               // foreach (adims_MODEL.clcxqt q in yllList)
               // {
               //     if (q.V > 0)
               //     {
               //         if (q.D >= pagetime && q.D <= pagetime.AddMinutes(60 * jcsjjg))
               //         {
               //             TimeSpan t = new TimeSpan();
               //             t = q.D - pagetime;
               //             float jhx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 132);
               //             float jhy = YY + 22 * 12;
               //             //if (cn_count % 5 == 0)
               //             {
               //                 if (q.V.ToString().Length > 3)
               //                 {
               //                     e.Graphics.FillRectangle(Brushes.Pink, jhx + 3, jhy, 16, 9);
               //                 }
               //                 else
               //                 {
               //                     e.Graphics.FillRectangle(Brushes.Pink, jhx + 3, jhy, 9, 9);
               //                 }
               //                 e.Graphics.DrawString(q.V.ToString(), q.V.ToString().Length > 3 ? ptzt6 : ptzt7, Brushes.Black, new PointF(jhx + 5, jhy));

               //             }
               //             cn_count++;
               //         }
               //     }
               //     else
               //     {
               //         //int xxx = 112;
               //         //int jhyyy = 469;
               //         //e.Graphics.DrawLine(ptp, new Point(xxx, jhyyy), new Point(xxx + 30, jhyyy - 10));
               //     }
               // }
               // #endregion
               e.Graphics.DrawString("尿量(ml)", ptzt7, Brushes.Black, x + 37, YY + 5 * 12 + y + 3);

               // #region<<打尿量>>

                int nl_count = 0;
                foreach (adims_MODEL.clcxqt cl in cnList)
                {
                    if (cl.V > 0)
                    {

                        if (cl.D >= pagetime && cl.D <= pagetime.AddMinutes(48 * jcsjjg))
                        {
                            TimeSpan t = new TimeSpan();
                            t = cl.D - pagetime;
                            float jhx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 190);
                            float jhy = YY + 5 * 12;
                            //if (nl_count % 5 == 0)

                            if (cl.V.ToString().Length > 3)
                            {
                                e.Graphics.FillRectangle(Brushes.Pink, jhx + 3, jhy, 16, 9);
                            }
                            else
                            {
                                e.Graphics.FillRectangle(Brushes.Pink, jhx + 3, jhy, 9, 9);
                            }
                            e.Graphics.DrawString(cl.V.ToString(), cl.V.ToString().Length > 3 ? ptzt6 : ptzt7, Brushes.Black, new PointF(jhx + 5, jhy));


                            nl_count++;
                        }
                    }
                    else
                    {
                        //int xxx = 112;
                        //int  jhyyy = 457;
                        //e.Graphics.DrawLine(ptp, new Point(xxx, jhyyy), new Point(xxx + 30, jhyyy - 10));
                    }

                }
                #endregion

                #region 打印监护项目

                int jhi = 0; int spo2Count = 0;
                #region 打印监护项目

                foreach (string jc in jhxmIn)
                {
                    e.Graphics.DrawString(jc, ptzt7, Brushes.Black, new PointF(35 + x, YY + jhi * 12 + y));
                    int count1 = 0;
                    foreach (adims_MODEL.jhxm j in jhxmValue)
                    {
                        if (jc == j.Sy)
                        {

                            if (j.D >= pagetime && j.D <= pagetime.AddMinutes(48* jcsjjg))
                            {
                                TimeSpan t = new TimeSpan();
                                t = j.D - pagetime;

                                float jhx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 190);
                                float jhy = YY + jhi * 12;
                                if (count1 % 2 == 0 && j.V != 0)
                                {
                                    if (j.D > cgTime && CGUAN && j.Sy == "ETCO2")
                                    {
                                        e.Graphics.FillRectangle(Brushes.Pink, jhx - 3 + x, jhy, 9, 9);
                                        e.Graphics.DrawString(j.V.ToString(), j.V > 99 ? ptzt5 : ptzt6, Brushes.Black, new PointF(jhx - 4 + x, jhy));

                                    }
                                    //if (CGUAN && BGUAN && j.Sy == "ETCO2")
                                    //{
                                    //    if (cgTime < j.D && j.D < bgTime)
                                    //    {
                                    //        e.Graphics.FillRectangle(Brushes.Pink, jhx - 3 + x, jhy, 9, 8);
                                    //        e.Graphics.DrawString(j.V.ToString(), j.V > 99 ? ptzt6 : ptzt7, Brushes.Black, new PointF(jhx - 4 + x, jhy));
                                    //    }
                                    //}
                                    else if (j.Sy != "ETCO2")
                                    {
                                        e.Graphics.FillRectangle(Brushes.Pink, jhx - 3 + x, jhy, 9, 9);
                                        e.Graphics.DrawString(j.V.ToString(), j.V > 99 ? ptzt5 : ptzt6, Brushes.Black, new PointF(jhx - 4 + x, jhy));

                                    }
                                }

                                count1++;

                            }
                            if (jc == j.Sy && j.Sy == "ECG" && jc == "ECG")
                            {

                                if (j.D >= pagetime && j.D <= pagetime.AddMinutes(48 * jcsjjg))
                                {
                                    TimeSpan t = new TimeSpan();
                                    t = j.D - pagetime;

                                    float jhx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 190);
                                    float jhy = YY + jhi * 12;
                                    string ECG = "";
                                    if (j.V == 0)
                                    {
                                        ECG = "SR";
                                    }
                                    else
                                    {
                                        ECG = j.V.ToString();
                                    }
                                    //e.Graphics.FillRectangle(Brushes.Pink, jhx - 3 + x, jhy, 9, 9);
                                    if (ECG != "")
                                        e.Graphics.DrawString(ECG, j.V > 99 ? ptzt5 : ptzt6, Brushes.Black, new PointF(jhx - 4 + x, jhy));
                                }
                            }

                        }

                    }
                    jhi++;

                }
                ///打印显示体温
                //jhi++;
               int tw_count = 0;
               if (checktw.Checked == false)
               {
                   e.Graphics.DrawString("体温", ptzt7, Brushes.Black, new PointF(35 + x, YY + jhi * 12 + y));
                   foreach (adims_MODEL.tw_point p in twList)
                   {
                       if (p.V > 0)
                       {
                           if (p.D >= pagetime && p.D <= pagetime.AddMinutes(48 * jcsjjg))
                           {
                               TimeSpan t = new TimeSpan();
                               t = p.D - pagetime;
                               float jhx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 192);
                               float jhy = YY + jhi * 12;
                               if (tw_count % 5 == 0)
                               {
                                   if (p.V.ToString().Length > 3)
                                   {
                                       e.Graphics.FillRectangle(Brushes.Pink, jhx - 3 + x, jhy, 16, 9);
                                   }
                                   else
                                   {
                                       e.Graphics.FillRectangle(Brushes.Pink, jhx - 3 + x, jhy, 9, 9);
                                   }
                                   e.Graphics.DrawString(p.V.ToString(), p.V.ToString().Length > 3 ? ptzt6 : ptzt7, Brushes.Black, new PointF(jhx - 4 + x, jhy));

                               }
                               tw_count++;
                           }
                       }


                   }
               }

                #endregion
                //foreach (adims_MODEL.jhxm j in jhxmValue)
                //{
                //    if (j.Sy == "SpO2" && j.V != 0)
                //    {
                //        if (j.D >= pagetime && j.D <= pagetime.AddMinutes(60 * jcsjjg))
                //        {
                //            TimeSpan t = new TimeSpan();
                //            t = j.D - pagetime;
                //            float jhx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 115);
                //            float jhy = YY + jhi + y + 2;
                //            if (spo2Count % 2 == 0)
                //            {
                //                e.Graphics.FillRectangle(Brushes.Pink, jhx - 3 + x, jhy, 9, 8);
                //                e.Graphics.DrawString(j.V.ToString(), j.V > 99 ? ptzt5 : ptzt7, Brushes.Black, new PointF(jhx - 4 + x, jhy));
                //            }
                //        }
                //    }
                //    spo2Count++;
                //}

                #endregion
                //tXTPH.Text = Convert.ToString(dtMzjld.Rows[0]["PH"]);
                //txtpco2.Text = Convert.ToString(dtMzjld.Rows[0]["PCO2"]);
                //txtpo2.Text = Convert.ToString(dtMzjld.Rows[0]["PO2"]);
                //txtbe.Text = Convert.ToString(dtMzjld.Rows[0]["BE"]);
                //txthco3.Text = Convert.ToString(dtMzjld.Rows[0]["HCO3"]);
                //txthb.Text = Convert.ToString(dtMzjld.Rows[0]["K"]);
                //txtk.Text = Convert.ToString(dtMzjld.Rows[0]["HB"]);
                //txthct.Text = Convert.ToString(dtMzjld.Rows[0]["HCT"]);
                //txtxuet.Text = Convert.ToString(dtMzjld.Rows[0]["XUETANG"]);
                #region 打印血气
                //Pen dakred2 = new Pen(Brushes.DarkRed, 2);
                //if (tXTPH.Text.Trim().IsNullOrEmpty())
                //    tXTPH.Text = " / ";
                //int yyyy = YY + 27 * 12 + 1;
                //e.Graphics.DrawString("PH： " + tXTPH.Text, ht7, Brushes.DarkRed, new Point(680 + x, yyyy));
                //e.Graphics.DrawLine(dakred2, new Point(705 + x, yyyy + 11), new Point(746 + x, yyyy + 11));
                //e.Graphics.DrawString("", ht7, Brushes.Black, new Point(745 + x, yyyy));
                //yyyy = yyyy + 12;
                //if (txtpco2.Text.Trim().IsNullOrEmpty())
                //    txtpco2.Text = " /";
                //e.Graphics.DrawString("PCO2:" + txtpco2.Text, ht7, Brushes.DarkRed, new Point(680 + x, yyyy));
                //e.Graphics.DrawLine(dakred2, new Point(705 + x, yyyy + 11), new Point(746 + x, yyyy + 11));
                //e.Graphics.DrawString("mmhg", ht7, Brushes.Black, new Point(745 + x, yyyy));
                //yyyy = yyyy + 12;
                //if (txtpo2.Text.Trim().IsNullOrEmpty())
                //    txtpo2.Text = "/";
                //e.Graphics.DrawString("PO2：" + txtpo2.Text, ht7, Brushes.DarkRed, new Point(680 + x, yyyy));
                //e.Graphics.DrawLine(dakred2, new Point(705 + x, yyyy + 11), new Point(746 + x, yyyy + 11));
                //e.Graphics.DrawString("mmhg", ht7, Brushes.Black, new Point(745 + x, yyyy));
                //yyyy = yyyy + 12;
                //if (txtbe.Text.Trim().IsNullOrEmpty())
                //    txtbe.Text = "/";
                //e.Graphics.DrawString("BE： " + txtbe.Text, ht7, Brushes.DarkRed, new Point(680 + x, yyyy));
                //e.Graphics.DrawLine(dakred2, new Point(705 + x, yyyy + 11), new Point(746 + x, yyyy + 11));
                //e.Graphics.DrawString("", ht7, Brushes.Black, new Point(745 + x, yyyy));
                //yyyy = yyyy + 12;
                //if (txthco3.Text.Trim().IsNullOrEmpty())
                //    txthco3.Text = "/";
                //e.Graphics.DrawString("HCO3:" + txthco3.Text, ht7, Brushes.DarkRed, new Point(680 + x, yyyy));
                //e.Graphics.DrawLine(dakred2, new Point(705 + x, yyyy + 11), new Point(746 + x, yyyy + 11));
                //e.Graphics.DrawString("mmhg/L", ht7, Brushes.Black, new Point(745 + x, yyyy));
                //yyyy = yyyy + 12;
                //if (txthb.Text.Trim().IsNullOrEmpty())
                //    txthb.Text = "/";
                //e.Graphics.DrawString("HB： " + txthb.Text, ht7, Brushes.DarkRed, new Point(680 + x, yyyy));
                //e.Graphics.DrawLine(dakred2, new Point(705 + x, yyyy + 11), new Point(746 + x, yyyy + 11));
                //e.Graphics.DrawString("g/L", ht7, Brushes.Black, new Point(745 + x, yyyy));
                //yyyy = yyyy + 12;
                //if (txtk.Text.Trim().IsNullOrEmpty())
                //    txtk.Text = "/";
                //e.Graphics.DrawString("K：  " + txtk.Text, ht7, Brushes.DarkRed, new Point(680 + x, yyyy));
                //e.Graphics.DrawLine(dakred2, new Point(705 + x, yyyy + 11), new Point(746 + x, yyyy + 11));
                //e.Graphics.DrawString("mmol/L", ht7, Brushes.Black, new Point(745 + x, yyyy));
                //yyyy = yyyy + 12;
                //if (txthct.Text.Trim().IsNullOrEmpty())
                //    txthct.Text = "/";
                //e.Graphics.DrawString("HCT：" + txthct.Text, ht7, Brushes.DarkRed, new Point(680 + x, yyyy));
                //e.Graphics.DrawLine(dakred2, new Point(705 + x, yyyy + 11), new Point(746 + x, yyyy + 11));
                //e.Graphics.DrawString("%", ht7, Brushes.Black, new Point(745 + x, yyyy));
                //yyyy = yyyy + 12;
                //if (txtxuet.Text.Trim().IsNullOrEmpty())
                //    txtxuet.Text = "/";
                //e.Graphics.DrawString("血糖:" + txtxuet.Text, ht7, Brushes.DarkRed, new Point(680 + x, yyyy));
                //e.Graphics.DrawLine(dakred2, new Point(705 + x, yyyy + 11), new Point(746 + x, yyyy + 11));
                //e.Graphics.DrawString("", ht7, Brushes.Black, new Point(745 + x, yyyy));
                #endregion



                #region//打印检测区域格子。血压体温等区域↓
                YY = YY + 12 * 7;
                e.Graphics.DrawLine(ptp, new Point(35 + x, YY), new Point(35 + x, YY + 240));
                e.Graphics.DrawString("体\n温", ptzt8, Brushes.Black, new Point(21 + x, YY-10 ));

                for (int i = 0; i < 12; i++)//画横实线           
                    e.Graphics.DrawLine(ptp, new Point(192 + x, YY + 20 * i), new Point(770 + x, YY + 20 * i + y));

                for (int i = 0; i < 12; i++)//画横虚线
                    e.Graphics.DrawLine(pxuxian, 192 + x, YY + 20 * i + 10, 770 + x, YY + 20 * i + y + 10);

                for (int i = 0; i < 8; i++)//画竖实线
                {
                    e.Graphics.DrawLine(ptp, new Point(192 + 72 * i + x, YY + y), new Point(192 + 72 * i + x, YY + 12 * 20 + y));
                }
                for (int i = 1; i < 24; i++)//画竖虚线
                {
                    e.Graphics.DrawLine(pxuxian, new Point(192 + 24 * i + x, YY + y), new Point(192 + 24 * i + x, YY + 12 * 20 + y));
                }
                for (int i = 1; i < 12; i++)
                    e.Graphics.DrawString((240 - i * 20).ToString(), ptzt7, Brushes.Black, new PointF(95 + x, YY + (float)20 * i + y - 5));
                for (int i = 1; i < 12; i++)
                    e.Graphics.DrawString((42 - i * 2).ToString(), ptzt7, Brushes.Black, new PointF(20 + x, YY + (float)20 * i + y - 5));

                e.Graphics.DrawString("∨收缩压", ptzt7, Brushes.Red, new Point(40 + x, YY + 20));
                e.Graphics.DrawString("∧舒张压", ptzt7, Brushes.Red, new Point(40 + x, YY + 35));
                e.Graphics.DrawString("●脉  搏", ptzt7, Brushes.Blue, new Point(40 + x, YY + 50));
                e.Graphics.DrawString("○呼  吸", ptzt7, Brushes.DarkCyan, new Point(40 + x, YY + 65));
                e.Graphics.DrawString("VV机械通气", ptzt7, Brushes.DarkCyan, new Point(40 + x, YY + 80));
                e.Graphics.DrawString("Ⅹ麻醉开始", ptzt7, Brushes.Black, new Point(40 + x, YY + 95));
                e.Graphics.DrawString("  置  管", ptzt7, Brushes.Black, new Point(40 + x, YY + 110));
                Image cgImage = Properties.Resources.CG;
                e.Graphics.DrawImage(cgImage, new Rectangle(43 + x, YY + 110, 9, 9));
                e.Graphics.DrawString("  拔  管", ptzt7, Brushes.Black, new Point(40 + x, YY + 125));
                Image BgImage = Properties.Resources.BG;
                e.Graphics.DrawImage(BgImage, new Rectangle(43 + x, YY + 125, 9, 9));
                e.Graphics.DrawString("⊙手术开始", ptzt7, Brushes.Black, new Point(40 + x, YY + 140));
                e.Graphics.DrawString("  手术结束", ptzt7, Brushes.Black, new Point(40 + x, YY + 155));
                Image ssjsImage = Properties.Resources.SSJS;
                e.Graphics.DrawImage(ssjsImage, new Rectangle(43 + x, YY + 155, 9, 9));
                e.Graphics.DrawString("X1 椎管内麻醉", ptzt7, Brushes.DarkRed, new Point(40 + x, YY + 170));
                e.Graphics.DrawString("X2 神经阻滞", ptzt7, Brushes.DarkOrange, new Point(40 + x, YY + 185));
                #endregion

                #region  //打印收缩压
                float px = 0, py = 0;
                Pen p_red2 = new Pen(Brushes.Red, 2);
                if (checkssy.Checked == false)
                {
                    foreach (adims_MODEL.point p in ssyList)
                    {
                        if (p.D >= pagetime && p.D <= pagetime.AddMinutes(48 * jcsjjg))
                        {
                            
                            TimeSpan t = new TimeSpan();
                            t = p.D - pagetime;
                            float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 192);
                            //int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 11 / jcsjjg + 120) + x;
                            float pointy = 0;
                            if (p.V > 230)
                            {
                                pointy = (float)((20) * 1 + YY);
                                e.Graphics.DrawString(p.V.ToString(), ptzt7, Brushes.Blue, pointx + x, pointy + y);
                            }
                            else
                                pointy = (float)((240 - p.V) * 1 + YY);
                            e.Graphics.DrawLines(p_red2, new PointF[3] { new PointF(pointx - 3 + x, pointy - 5 + y), new PointF(pointx + x, pointy + y), new PointF(pointx + 3 + x, pointy - 5 + y) });
                            if (px != 0)
                                e.Graphics.DrawLine(Pens.Red, new PointF(px + x, py + y), new PointF(pointx + x, pointy + y));

                            px = pointx;
                            py = pointy;
                        }

                    }
                }
                #endregion

                #region  //打印舒张压
                px = 0; py = 0;
                if (checkszy.Checked == false)
                {
                    foreach (adims_MODEL.point p in szyList)
                    {
                        if (p.D >= pagetime && p.D <= pagetime.AddMinutes(48 * jcsjjg))
                        {
                            TimeSpan t = new TimeSpan();
                            t = p.D - pagetime;
                            float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 192);
                            //float pointy = (float)((220 - p.V) * 1 + 460);
                            float pointy = 0;
                            if (p.V > 230)
                            {
                                pointy = (float)((20) * 1 + YY);
                                e.Graphics.DrawString(p.V.ToString(), ptzt7, Brushes.Blue, pointx + x, pointy + y);
                            }
                            else
                                pointy = (float)((240 - p.V) * 1 + YY);
                            e.Graphics.DrawLines(p_red2, new PointF[3] { new PointF(pointx - 3 + x, pointy + 5 + y), new PointF(pointx + x, pointy + y), new PointF(pointx + 3 + x, pointy + 5 + y) });
                            if (px != 0)
                                e.Graphics.DrawLine(Pens.Red, new PointF(px + x, py + y), new PointF(pointx + x, pointy + y));

                            px = pointx;
                            py = pointy;
                        }
                    }
                }
                #endregion

                #region  //打印脉搏
                px = 0; py = 0;
                if (checkmb.Checked==false)
                foreach (adims_MODEL.point p in mboList)
                {
                    if (p.D >= pagetime && p.D <= pagetime.AddMinutes(48 * jcsjjg))
                    {
                        TimeSpan t = new TimeSpan();
                        t = p.D - pagetime;
                        float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 192);
                        //float pointy = (float)((220 - p.V) * 1 + 460);
                        float pointy = 0;
                        if (p.V > 230)
                        {
                            pointy = (float)((20) * 1 + YY);
                            e.Graphics.DrawString(p.V.ToString(), ptzt7, Brushes.Blue, pointx + x, pointy + y);
                        }
                        else
                            pointy = (float)((240 - p.V) * 1 + YY);
                        e.Graphics.FillEllipse(Brushes.Blue, pointx - 2 + x, pointy - 2 + y, 5, 5);
                        if (px != 0)
                            e.Graphics.DrawLine(Pens.Blue, new PointF(px + x, py + y), new PointF(pointx + x, pointy + y));
                        px = pointx;
                        py = pointy;
                    }
                }
                #endregion

                #region  //打印体温
                //px = 0; py = 0;
                //foreach (adims_MODEL.point p in twList)
                //{
                //    if (p.D >= pagetime && p.D <= pagetime.AddMinutes(60 * jcsjjg))
                //    {
                //        TimeSpan t = new TimeSpan();
                //        t = p.D - pagetime;
                //        float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 112);
                //        //float pointy = (float)((220 - p.V) * 1 + 460);
                //        float pointy = 0;
                //        if (p.V > 230)
                //        {
                //            pointy = (float)((20) * 1 + YY);
                //            e.Graphics.DrawString(p.V.ToString(), ptzt7, Brushes.Blue, pointx + x, pointy + y);

                //        }
                //        else
                //            pointy = (float)((240 - p.V) * 1 + YY);
                //        e.Graphics.DrawPolygon(Pens.Maroon, new PointF[3] { new PointF(pointx - 3 + x, pointy + 5 + y), new PointF(pointx + x, pointy + y), new PointF(pointx + 3 + x, pointy + 5 + y) });
                //        //e.Graphics.FillEllipse(Pens.Maroon, pointx + x - 2, pointy + y - 2, 5, 5);
                //        if (px != 0)
                //            e.Graphics.DrawLine(Pens.Maroon, new PointF(px + x, py + y), new PointF(pointx + x, pointy + y));

                //        px = pointx;
                //        py = pointy;
                //       }
                //  }
                #endregion

                    #region  //打印呼吸
                px = 0; py = 0; int phuxi = 0;
                //if (checkBoxhx.Checked == false)
                //{
                    string fxs = "";
                    string tvxs = "";
                    DataTable dtMZ_Info = bll.SelectOldPatInfo(mzjldID);

                    if (dtMZ_Info.Rows.Count > 0)
                    {
                        if (this.toStr(dtMZ_Info.Rows[0]["jkvalue"]) != "")
                        {
                            fxs = dtMZ_Info.Rows[0]["jkvalue"].ToString();
                        }
                        if (this.toStr(dtMZ_Info.Rows[0]["fzvalue"]) != "")
                        {
                            tvxs = dtMZ_Info.Rows[0]["fzvalue"].ToString();
                        }
                    }
                    int dowa_sb = 0;
                    if (checkhx.Checked == false)
                    {
                        foreach (adims_MODEL.point p in hxlList)
                        {
                            if (p.D >= pagetime && p.D <= pagetime.AddMinutes(48 * jcsjjg))
                            {
                                TimeSpan t = new TimeSpan();
                                t = p.D - pagetime;

                                float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 212);
                                //float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 192);
                                float pointy = 0;
                                if (p.V > 230)
                                {
                                    //pointy = (float)((20) * 1 + YY);
                                    pointy = (float)((20) * 1 + YY) + y;
                                    e.Graphics.DrawString(p.V.ToString(), ptzt6, Brushes.Blue, pointx, pointy);
                                }
                                else
                                    pointy = (float)((240 - p.V) * 1 + YY) + y;
                                if (jkksTime < p.D && p.D < jkjsTime)
                                {
                                    //float xPlus = pointx - px; // 描点作图
                                    //PointF p1 = new PointF(px, py);
                                    //PointF p2 = new PointF(pointx - 2 * xPlus / 3, pointy + 6);
                                    //PointF p3 = new PointF(pointx - xPlus / 3, pointy - 6);
                                    //PointF p4 = new PointF(pointx, pointy);
                                    //e.Graphics.DrawBezier(Pens.DarkCyan, p1, p2, p3, p4);
                                    this.PaintSignMBreath2(e.Graphics, new PointF(pointx - 2, pointy - 4));
                                    if (dowa_sb == 0)
                                    {
                                        e.Graphics.DrawString("TV" + tvxs + "/ " + "f" + fxs, this.Font, Brushes.DarkCyan, pointx, pointy - 13);

                                        dowa_sb++;
                                    }
                                    //e.Graphics.DrawString("C", ptzt6, Brushes.DarkCyan, pointx - 2, pointy - 3);
                                }
                                //else if (fzksTime < p.D && p.D < fzjsTime)
                                //{
                                //    e.Graphics.DrawString("A", ptzt6, Brushes.DarkCyan, pointx - 2, pointy - 3);
                                //}
                                else
                                    e.Graphics.DrawEllipse(Pens.DarkCyan, pointx - 2, pointy - 2, 5, 5);
                                //if (px != 0 && (jkksTime > p.D || jkjsTime < p.D))
                                if (px != 0)
                                    e.Graphics.DrawLine(Pens.DarkCyan, new PointF(px, py), new PointF(pointx, pointy));
                                px = pointx;
                                py = pointy;
                            }
                        }
                    }
                //}
                //px = 0; py = 0; int phuxi = 0;
                //foreach (adims_MODEL.point h in hxlList)
                //{
                //    if (h.D >= pagetime && h.D <= pagetime.AddMinutes(60 * jcsjjg))
                //    {
                //        TimeSpan t = new TimeSpan();
                //        t = h.D - pagetime;
                //        float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 112);
                //        float pointy = 0;
                //        if (h.V > 230)
                //        {
                //            pointy = (float)((20) * 1 + YY) + y;
                //            e.Graphics.DrawString(h.V.ToString(), ptzt7, Brushes.Blue, pointx, pointy);
                //        }
                //        else
                //            pointy = (float)((240 - h.V) * 1 + YY) + y;
                //        if (!CGUAN)
                //        {
                //            e.Graphics.DrawEllipse(Pens.DarkCyan, pointx - 2, pointy - 2, 5, 5);
                //            if (px != 0)
                //                e.Graphics.DrawLine(Pens.DarkCyan, new PointF(px, py), new PointF(pointx, pointy));

                //        }
                //        if (CGUAN && !BGUAN)
                //        {
                //            if (cgTime < h.D)
                //            {
                //                float xPlus = pointx - px; // 描点作图
                //                PointF p1 = new PointF(px, py);
                //                PointF p2 = new PointF(pointx - 2 * xPlus / 3, pointy + 6);
                //                PointF p3 = new PointF(pointx - xPlus / 3, pointy - 6);
                //                PointF p4 = new PointF(pointx, pointy);
                //                e.Graphics.DrawBezier(Pens.DarkCyan, p1, p2, p3, p4);

                //            }
                //            else
                //                e.Graphics.DrawEllipse(Pens.DarkCyan, pointx - 2, pointy - 2, 5, 5);
                //            if (px != 0 && (cgTime > h.D))
                //                e.Graphics.DrawLine(Pens.DarkCyan, new PointF(px, py), new PointF(pointx, pointy));

                //        }
                //        if (BGUAN)
                //        {
                //            if (cgTime < h.D && h.D < bgTime && px > 120)
                //            {
                //                float xPlus = pointx - px; // 描点作图
                //                PointF p1 = new PointF(px, py);
                //                PointF p2 = new PointF(pointx - 2 * xPlus / 3, pointy + 6);
                //                PointF p3 = new PointF(pointx - xPlus / 3, pointy - 6);
                //                PointF p4 = new PointF(pointx, pointy);
                //                e.Graphics.DrawBezier(Pens.DarkCyan, p1, p2, p3, p4);

                //            }
                //            else
                //                e.Graphics.DrawEllipse(Pens.DarkCyan, pointx - 2, pointy - 2, 5, 5);
                //            if (px != 0 && (cgTime > h.D || bgTime < h.D))
                //                e.Graphics.DrawLine(Pens.DarkCyan, new PointF(px, py), new PointF(pointx, pointy));

                //        }
                //        px = pointx;
                //        py = pointy;
                //    }
                //}
                    #endregion

                    #region  //打印ETCO2
                    px = 0; py = 0;
                    //foreach (adims_MODEL.point p in etco2List)
                    //{
                    //    if (p.D >= pagetime && p.D <= pagetime.AddMinutes(60 * jcsjjg))
                    //    {
                    //        TimeSpan t = new TimeSpan();
                    //        t = p.D - pagetime;
                    //        float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 112);
                    //        float pointy = 0;
                    //        if (p.V > 230)
                    //        {
                    //            pointy = (float)((20) * 1 + YY);
                    //            e.Graphics.DrawString(p.V.ToString(), ptzt7, Brushes.Blue, pointx + x, pointy + y);
                    //        }
                    //        else
                    //            pointy = (float)((240 - p.V) * 1 + YY);

                    //        e.Graphics.DrawPolygon(Pens.DarkOrange, new PointF[3] { new PointF(pointx+ x, pointy+ y), 
                    //                       new PointF(pointx - 3+ x, pointy - 5+ y), new PointF(pointx + 3+ x, pointy - 5+ y) });
                    //        // e.Graphics.FillPolygon(Brushes.Green, new PointF[3] { new PointF(pointx+ x, pointy+ y), 
                    //        //         new PointF(pointx + 3+ x, pointy + 6+ y), new PointF(pointx - 3+ x, pointy + 6+ y) });

                    //        if (px != 0)
                    //            e.Graphics.DrawLine(Pens.DarkOrange, new PointF(px + x, py + y), new PointF(pointx + x, pointy + y));

                    //        px = pointx;
                    //        py = pointy;
                    //    }

                    //}
                    #endregion

                    //↓标记区域
                    YY = YY + 12 * 20;
                    e.Graphics.DrawLine(ptp, new Point(20 + x, YY), new Point(770 + x, YY));
                    e.Graphics.DrawString("标记", ptzt7, Brushes.Black, new Point(25 + x, YY + 3));

                    #region 打印麻醉，手术，插管
                    if (ssksTime >= pagetime && ssksTime <= pagetime.AddHours(jcsjjg))
                    {
                        TimeSpan T = ssksTime - pagetime;
                        int xxx = (int)((T.Days * 24 * 60 + T.Hours * 60 + T.Minutes) * 12 / jcsjjg + 192 + x);
                        e.Graphics.DrawString("⊙", ptzt8, Brushes.Black, xxx, YY + 3);
                    }
                    if (ssjsTime >= pagetime && ssjsTime < pagetime.AddMinutes(48 * jcsjjg))
                    {
                        TimeSpan T = ssjsTime - pagetime;
                        int xxx = (int)((T.Days * 24 * 60 + T.Hours * 60 + T.Minutes) * 12 / jcsjjg + 192 + x);
                        Image newImage = Properties.Resources.SSJS;
                        e.Graphics.DrawImage(newImage, new Rectangle(xxx, YY + 3, 10, 10));
                    }
                    if (mzksTime >= pagetime && mzksTime <= pagetime.AddHours(jcsjjg))
                    {
                        TimeSpan T = mzksTime - pagetime;
                        int xxx = (int)((T.Days * 24 * 60 + T.Hours * 60 + T.Minutes) * 12 / jcsjjg + 192 + x);
                        e.Graphics.DrawString("Χ", ptzt8, Brushes.Black, xxx, YY + 3);
                    }
                    if (mzjkssjzzTime >= pagetime && mzjkssjzzTime <= pagetime.AddHours(jcsjjg))
                    {
                        TimeSpan T = mzjkssjzzTime - pagetime;
                        int xxx = (int)((T.Days * 24 * 60 + T.Hours * 60 + T.Minutes) * 12 / jcsjjg + 192 + x);
                        e.Graphics.DrawString("Χ2", ptzt8, Brushes.Black, xxx, YY + 3);
                    }
                    if (mzkszgnTime >= pagetime && mzkszgnTime <= pagetime.AddHours(jcsjjg))
                    {
                        TimeSpan T = mzkszgnTime - pagetime;
                        int xxx = (int)((T.Days * 24 * 60 + T.Hours * 60 + T.Minutes) * 12 / jcsjjg + 192 + x);
                        e.Graphics.DrawString("Χ1", ptzt8, Brushes.Black, xxx, YY + 3);
                    }
                    //if (mzjsTime >= pagetime && mzjsTime <= pagetime.AddHours(jcsjjg))
                    //{
                    //    TimeSpan T = mzjsTime - pagetime;
                    //    int xxx = (int)((T.Days * 24 * 60 + T.Hours * 60 + T.Minutes) * 11 / jcsjjg + 115 + x);
                    //    e.Graphics.DrawString("Χ", ptzt8, Brushes.Black, xxx, YY + 3);
                    //}
                    if (cgTime >= pagetime && cgTime <= pagetime.AddHours(jcsjjg))
                    {
                        TimeSpan T = cgTime - pagetime;
                        int xxx = (int)((T.Days * 24 * 60 + T.Hours * 60 + T.Minutes) * 12 / jcsjjg + 192 + x);
                        Image newImage = Properties.Resources.CG;
                        e.Graphics.DrawImage(newImage, new Rectangle(xxx, YY + 3, 10, 10));
                    }
                    if (bgTime >= pagetime && bgTime < pagetime.AddMinutes(48 * jcsjjg))
                    {
                        TimeSpan T = bgTime - pagetime;
                        int xxx = (int)((T.Days * 24 * 60 + T.Hours * 60 + T.Minutes) * 12 / jcsjjg + 192 + x);
                        Image newImage = Properties.Resources.BG;
                        e.Graphics.DrawImage(newImage, new Rectangle(xxx, YY + 3, 10, 10));
                    }

                    #endregion
                   
                    #region 打印用药
                    YY = YY+24;
                    for (int i = 0; i < 20; i++)//画横实线
                    {
                        if (i == 0 || i == 11 || i == 16 || i == 19)//|| i == 20
                            e.Graphics.DrawLine(ptp, new Point(20 + x, YY + 12 * i + y), new Point(770 + x, YY + 12 * i + y));
                        //else if (i == 33)
                        //    e.Graphics.DrawLine(ptp, new Point(35 + x, YY + 12 * i + y), new Point(700 + x, YY + 12 * i + y));
                        else
                            e.Graphics.DrawLine(ptp, new Point(35 + x, YY + 12 * i + y), new Point(770 + x, YY + 12 * i + y));
                    }
                    e.Graphics.DrawLine(ptp, new Point(35 + x, YY + y), new Point(35 + x, YY +19 * 12 + y));

                    for (int i = 0; i < 10; i++)//画竖实线
                    {
                        e.Graphics.DrawLine(ptp, new Point(192 + x + 72 * i, YY - 2 + y), new Point(192 + x + 72 * i, YY + 19 * 12 + y));
                    }
                    for (int i = 1; i < 48; i++)//画竖虚线
                    {
                        e.Graphics.DrawLine(pxuxian, new PointF(192 + x + 12 * i, YY + y), new Point(192 + x + 12 * i, YY + 19 * 12 + y));

                    }
                    for (int i = 0; i < 16; i++)//画竖实线
                    {
                        e.Graphics.DrawLine(ptp, new Point(192 + x + 36 * i, YY + 19 * 12 + y), new Point(192 + x + 36 * i, YY + 19 * 12 + y));
                    }
                   
                    #endregion


                    #region 打印气体
                    e.Graphics.DrawString("O2", ptzt8, Brushes.Black, new Point(35 + x, YY));
                    ArrayList sssQT = new ArrayList();
                    int qti = 0;   //起步位置
                    foreach (adims_MODEL.mzqt mzqt in mzqtList)
                    {
                        if (sssQT.Contains(mzqt.Qtname))
                            qti = qti - 1;
                        //if (!sssQT.Contains(mzqt.Qtname))
                        //    e.Graphics.DrawString(mzqt.Qtname, mzqt.Qtname.Length < 7 ? ptzt6 : ptzt5, Brushes.Black, new PointF(32 + x, YY + qti * 12 + y + 3));

                        if (mzqt.Bz == 2)
                        {
                            TimeSpan t = new TimeSpan();
                            TimeSpan t1 = new TimeSpan();
                            t1 = mzqt.Jssj - pagetime;
                            t = mzqt.Sysj - pagetime;
                            int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 192) + x;
                            int y1 = YY + qti * 12 + 4;

                            int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 12 / jcsjjg + 192) + x;
                            //double qtzongliang = (mzqt.Yl) * Convert.ToDouble((x2 - x1) / 2.5);
                            //e.Graphics.DrawString(Convert.ToInt32(qtzongliang).ToString() + "L", this.Font, Brushes.Blue, new Point(763, y1 - 2));
                            if (x1 > 190 + x && x1 < 780 + x)
                            {
                                e.Graphics.DrawString(mzqt.Yl.ToString() + " " + mzqt.Dw, ptzt6, Brushes.Blue, new Point(x1 + 3, y + y1 - 6));
                                e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1 + y), new Point(x1 - 3, y1 + 6 + y), new Point(x1 + 3, y1 + 6 + y) });
                            }
                            if (x2 > 190 + x && x2 < 780 + x)
                            {
                                e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1 + y), new Point(x2 - 3, y1 + 6 + y), new Point(x2 + 3, y1 + 6 + y) });
                            }
                            if (x1 > 190 + x && x1 < 780 + x && x2 > 190 + x && x2 < 780 + x)
                            {
                                e.Graphics.DrawLine(pred2, new Point(x1, y1 + y + 3), new Point(x2, y1 + y + 3));
                            }
                            if (x1 > 190 + x && x1 < 780 + x && x2 > 780 + x)
                            {
                                e.Graphics.DrawLine(pred2, new Point(x1, y1 + y + 3), new Point(780 + x, y1 + y + 3));
                            }
                            if (x1 < 190 + x && x2 > 190 + x && x2 < 780 + x)
                            {
                                e.Graphics.DrawLine(pred2, new Point(190 + x, y1 + y + 3), new Point(x2, y1 + y + 3));
                            }
                            if (x1 < 190 + x && x2 > 780 + x)
                            {
                                e.Graphics.DrawLine(pred2, new Point(190 + x, y1 + y + 3), new Point(780 + x, y1 + y + 3));
                            }
                        }
                        qti++;
                        sssQT.Add(mzqt.Qtname);
                    }


                    #endregion
                    #region 打印全麻药
                    ArrayList sssYDY = new ArrayList();
                    int ydyi = 1;
                    foreach (adims_MODEL.mzyt mzyt in ydyList) //打印全麻药
                    {
                        if (sssYDY.Contains(mzyt.Ytname))
                            ydyi = ydyi - 1;
                        if (!sssYDY.Contains(mzyt.Ytname))
                            e.Graphics.DrawString(mzyt.Ytname + " " + mzyt.Dw, mzyt.Ytname.Length < 7 ? ptzt7 : ptzt5, Brushes.Black, new PointF(37 + x, YY + ydyi * 12 + y + 3));

                        if (mzyt.Cxyy == false)
                        {
                            TimeSpan t = new TimeSpan();
                            t = mzyt.Sysj - pagetime;
                            int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 192);
                            int y1 = YY + ydyi * 12 + 4;
                            if (x1 > 100 + x && x1 < 700 + x)
                            {
                                e.Graphics.DrawString(mzyt.Yl.ToString(), ptzt7, Brushes.Blue, new Point(x1 + 3 + x, y + y1 - 6));
                                e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), new Point(x1 - 3 + x, y1 + 6 + y), new Point(x1 + 3 + x, y1 + 6 + y) });
                            }
                        }
                        if (mzyt.Cxyy == true)
                        {
                            TimeSpan t = new TimeSpan();
                            TimeSpan t1 = new TimeSpan();
                            t1 = mzyt.Jssj - pagetime;
                            t = mzyt.Sysj - pagetime;
                            int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 192);
                            int y1 = YY + ydyi * 12 + 4;
                            int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 12 / jcsjjg + 192);
                            if (x1 > 190 + x && x1 < 780 + x)
                            {
                                e.Graphics.DrawString(mzyt.Yl.ToString(), ptzt7, Brushes.Blue, new Point(x1 + 3 + x, y + y1 - 6));
                                e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), new Point(x1 - 3 + x, y1 + 6 + y), new Point(x1 + 3 + x, y1 + 6 + y) });
                            }
                            if (x2 > 190 + x && x2 < 780 + x)
                            {
                                e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2 + x, y1 + y), new Point(x2 - 3 + x, y1 + 6 + y), new Point(x2 + 3 + x, y1 + 6 + y) });
                            }
                            if (x1 > 190 + x && x1 < 780 + x && x2 > 190 + x && x2 < 780 + x)
                            {
                                e.Graphics.DrawLine(pred2, new Point(x1 + x, y1 + y + 3), new Point(x2 + x, y1 + y + 3));
                            }
                            if (x1 > 190 + x && x1 < 780 + x && x2 > 780 + x)
                            {
                                e.Graphics.DrawLine(pred2, new Point(x1 + x, y1 + y + 3), new Point(780 + x, y1 + y + 3));
                            }
                            if (x1 < 190 + x && x2 > 120 + x && x2 < 780 + x)
                            {
                                e.Graphics.DrawLine(pred2, new Point(120 + x, y1 + y + 3), new Point(x2 + x, y1 + y + 3));
                            }
                            if (x1 < 190 + x && x2 > 780 + x)
                            {
                                e.Graphics.DrawLine(pred2, new Point(120 + x, y1 + y + 3), new Point(780 + x, y1 + y + 3));
                            }
                        }
                        ydyi++;
                        sssYDY.Add(mzyt.Ytname);
                    }

                    #endregion


                    #region 打印输液
                    int syi = 11;
                    ArrayList sssSY = new ArrayList();
                    foreach (adims_MODEL.shuye sx in shuyeList)
                    {
                        if (sssSY.Contains(sx.Name))
                            syi = syi - 1;
                        if (!sssSY.Contains(sx.Name))
                            e.Graphics.DrawString(sx.Name + " " + sx.Dw, sx.Name.Length < 9 ? ptzt7 : ptzt5, Brushes.Black, new PointF(37 + x, YY + syi * 12 + y + 3));

                        TimeSpan t = new TimeSpan();
                        TimeSpan t1 = new TimeSpan();
                        t1 = pagetime.AddMinutes(60 * jcsjjg) - pagetime;
                        t = sx.Kssj - pagetime;
                        //e.Graphics.DrawString(sx.Name.ToString(), ptzt7, Brushes.Black, 37 + x, YY + syi * 12 + 2);
                        int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 192);
                        int y1 = YY + syi * 12 + 5;
                        int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 12 / jcsjjg + 192);

                        if (x1 > 190 + x && x1 < 780 + x)
                        {
                            e.Graphics.DrawString(sx.Jl.ToString(), ptzt7, Brushes.Blue, x1 + x, y + y1 - 8);
                            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), 
                             new Point(x1 - 3 + x, y1 + 5 + y), new Point(x1 + 3 + x, y1 + 5 + y) });
                        }
                        syi++;
                        sssSY.Add(sx.Name);
                    }

                    #endregion

                    #region 打印输血
                    //打印输血
                    int sxi = 16;
                    ArrayList sssSX = new ArrayList();
                    foreach (adims_MODEL.shuxue sx in shuxueList)
                    {
                        if (sssSX.Contains(sx.Name))
                            sxi = sxi - 1;
                        if (!sssSX.Contains(sx.Name))
                            e.Graphics.DrawString(sx.Name + " " + sx.Dw, sx.Name.Length < 9 ? ptzt7 : ptzt5, Brushes.Black, new PointF(37 + x, YY + sxi * 12 + y + 3));

                        TimeSpan t = new TimeSpan();
                        TimeSpan t1 = new TimeSpan();
                        t1 = pagetime.AddHours(jcsjjg) - pagetime;
                        t = sx.Kssj - pagetime;
                        //e.Graphics.DrawString(sx.Name.ToString(), ptzt7, Brushes.Black, 37 + x, YY + sxi * 12 + 2);
                        int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 192);
                        int y1 = YY + sxi * 12 + 5;
                        int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 12 / jcsjjg + 192);
                        if (x1 > 190 + x && x1 < 780 + x)
                        {
                            e.Graphics.DrawString(sx.Jl.ToString(), ptzt7, Brushes.Blue, x1 + x, y + y1 - 6);
                            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), 
                             new Point(x1 - 3 + x, y1 + 5 + y), new Point(x1 + 3 + x, y1 + 5 + y) });
                        }
                        sxi++;
                        sssSX.Add(sx.Name);
                    }
                    #endregion
                    #region 打印输液输血
                    e.Graphics.DrawString("麻\n\n醉\n\n药", ptzt8, Brushes.Black, new Point(21 + x, YY + 40));
                    e.Graphics.DrawString("输\n\n液", ptzt8, Brushes.Black, new Point(21 + x, YY + 11 * 12 + 10));
                    e.Graphics.DrawString("输\n血", ptzt8, Brushes.Black, new Point(21 + x, YY + 16 * 12 + 5));
                    //e.Graphics.DrawString("     用药序号", ptzt10, Brushes.Black, new Point(25 + x, YY + 51));
                    #endregion


                    YY = YY + 19 * 12; Y_unLine = YY + 13;
                   #region 打印特殊用药


                    int szi = 1;
                    int szi1 = 1;
                    int tsn1 = 0;
                    string szss1 = "";
                    //显示事件标记
                    foreach (adims_MODEL.szsj sz in szsjList)
                    {
                        if (sz.D >= pagetime && sz.D <= pagetime.AddMinutes(48 * jcsjjg))
                        {
                            TimeSpan t = new TimeSpan();
                            t = sz.D - pagetime;
                            float tx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 192) + x;
                            e.Graphics.FillRectangle(Brushes.Pink, tx - 3, YY + 3 - 450, 9, 9);
                            //e.Graphics.FillRectangle(Brushes.Pink, tx - 3, BJYY + 8 + (sz.Y_zb - 315), 9, 9);
                            e.Graphics.DrawString(szi.ToString(), ptzt7, Brushes.Black, new PointF(tx - 3, YY + 3 - 450));
                            szi++;
                        }

                    }
                    //显示事件的类容
                    foreach (adims_MODEL.szsj sz in szsjList)
                    {
                        if (szi1 + tsn1 > 11)
                        {
                            break;
                        }
                        szss1 = szi1.ToString() + "." + sz.Name + " " + sz.D.ToString("HH:mm:ss") + "\n";
                        if (szi1 + tsn1 > 12)
                        {
                            string strSS1 = "";
                            int StrLengthSS = szss1.Trim().Length;//1//2
                            int rowSS = StrLengthSS / 20;
                            if (StrLengthSS < 21)
                            {
                                for (int i = 0; i <= rowSS; i++)//40个字符就换行
                                {

                                    if (i < rowSS)
                                        strSS1 = szss1.Trim().ToString().Substring(i * 20, 20); //从第i*40个开始，截取40个字符串
                                    else
                                        strSS1 = szss1.Trim().ToString().Substring(i * 20);

                                    e.Graphics.DrawString(strSS1, ptzt9, Brushes.Black, new PointF(220 + x, YY + 2 + (szi1 + tsn1 - i - 9) * 14));
                                }
                            }
                            else
                            {

                                for (int i = 0; i <= rowSS; i++)//40个字符就换行
                                {

                                    if (i < rowSS)
                                    {
                                        strSS1 = szss1.Trim().ToString().Substring(i * 20, 20); //从第i*40个开始，截取40个字符串
                                        e.Graphics.DrawString(strSS1, ptzt9, Brushes.Black, new PointF(220 + x, YY + 2 + (szi1 + tsn1 - i - 9) * 14));
                                    }
                                    else
                                    {
                                        strSS1 = szss1.Trim().ToString().Substring(i * 20);
                                        if (szi1 + tsn1 + i > 16)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            e.Graphics.DrawString(strSS1, ptzt9, Brushes.Black, new PointF(220 + x, YY + 2 + (szi1 + tsn1 + i - 9) * 14));
                                        }
                                    }
                                    //if（tsi1+tsn > 8）
                                    //{
                                    //e.Graphics.DrawString(strSS1, ptzt9, Brushes.Black, new PointF(440 + x, YY + 2 + (tsi1 - 8) * 14));//这里开始做
                                    //}
                                    //else
                                    // e.Graphics.DrawString(strSS1, ptzt9, Brushes.Black, new PointF(430 + x, YY + 27 + (tsi1 + tsn + i - 1) * 14));

                                }
                                tsn1++;
                            }

                        }
                        else
                        {

                            string strSS1 = "";
                            int StrLengthSS = szss1.Trim().Length;//1//2
                            int rowSS = StrLengthSS / 20;
                            if (StrLengthSS < 21)
                            {
                                for (int i = 0; i <= rowSS; i++)//40个字符就换行
                                {

                                    if (i < rowSS)
                                        strSS1 = szss1.Trim().ToString().Substring(i * 20, 20); //从第i*40个开始，截取40个字符串
                                    else
                                        strSS1 = szss1.Trim().ToString().Substring(i * 20);

                                    e.Graphics.DrawString(strSS1, ptzt9, Brushes.Black, new PointF(40 + x, YY + 2 + (szi1 + tsn1 - 1) * 14));
                                }
                            }
                            else
                            {

                                for (int i = 0; i <= rowSS; i++)//40个字符就换行
                                {

                                    if (i < rowSS)
                                    {
                                        strSS1 = szss1.Trim().ToString().Substring(i * 20, 20); //从第i*40个开始，截取40个字符串
                                        e.Graphics.DrawString(strSS1, ptzt9, Brushes.Black, new PointF(40 + x, YY + 2 + (szi1 + tsn1 - 1) * 14));
                                    }
                                    else
                                    {
                                        strSS1 = szss1.Trim().ToString().Substring(i * 20);
                                        if (szi1 + tsn1 + i > 8)
                                        {
                                            e.Graphics.DrawString(strSS1, ptzt9, Brushes.Black, new PointF(220 + x, YY +2+ (szi1 + tsn1 - 8) * 14));//这里开始做

                                        }
                                        else
                                        {
                                            e.Graphics.DrawString(strSS1, ptzt9, Brushes.Black, new PointF(40 + x, YY + 2 + (szi1 + tsn1 + i - 1) * 14));
                                        }
                                    }
                                    //if（tsi1+tsn > 8）
                                    //{
                                    //e.Graphics.DrawString(strSS1, ptzt9, Brushes.Black, new PointF(440 + x, YY + 2 + (tsi1 - 8) * 14));//这里开始做
                                    //}
                                    //else
                                    // e.Graphics.DrawString(strSS1, ptzt9, Brushes.Black, new PointF(430 + x, YY + 27 + (tsi1 + tsn + i - 1) * 14));
                                }
                                tsn1++;
                            }
                        }
                        szi1++;
                    }

                    //显示其他用药

                    //显示其他用药
                    int tsi = 1;
                    int tsi1 = 1;
                    int tsn = 0;
                    string tss1 = "";
                    foreach (adims_MODEL.tsyy ts in tsyyList)
                    {
                        if (ts.D >= pagetime && ts.D <= pagetime.AddMinutes(48 * jcsjjg))
                        {
                            TimeSpan t = new TimeSpan();
                            t = ts.D - pagetime;
                            float tx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 192) + x;
                            //e.Graphics.FillEllipse(Brushes.LightGreen, tx - 3, BJYY + 3 + (ts.Y_zb - 370), 10, 9);
                            e.Graphics.DrawString(tsi.ToString(), ptzt7, Brushes.Blue, new PointF(tx - 3, YY-250 + 3));
                            tsi++;
                        }
                    }
                    foreach (adims_MODEL.tsyy ts in tsyyList)
                    {
                        if (tsi1 >11)
                        {
                            break;
                        }
                        //string zsfs = "";//注射方式
                        // if (ts.Yyfs == "口服")
                        //{
                        //    zsfs = "po";
                        //}
                        //if (ts.Yyfs == "静脉滴注")
                        //{
                        //    zsfs = "ivdrip";
                        //}
                        //if (ts.Yyfs == "皮下注射")
                        //{
                        //    zsfs = "ih";
                        //}
                        //if (ts.Yyfs == "肌肉注射")
                        //{
                        //    zsfs = "im";
                        //}
                        //if (ts.Yyfs == "静脉注射")
                        //{
                        //    zsfs = "iv";
                        //}
                        //if (ts.Yyfs == "皮下注射")
                        //{
                        //    zsfs = "id";
                        //}
                        if (ts.Name.Contains("+"))
                        {
                            tss1 = tsi1.ToString() + ".[" + ts.Name + "" + ts.Yl + "" + ts.Dw + " ]" + ts.Yyfs + " " + ts.D.ToString("HH:mm") + "\n";
                        }
                        else
                        {
                            tss1 = tsi1.ToString() + "." + ts.Name + "" + ts.Yl + "" + ts.Dw + " " + ts.Yyfs + " " + ts.D.ToString("HH:mm") + "\n";
                        }
                        //string strSS1 = "";
                        //int StrLengthSS = tss1.Trim().Length;//1//2
                        //int rowSS = StrLengthSS / 18;

                        e.Graphics.DrawString(tss1, ptzt9, Brushes.Black, new PointF(320 + x, YY +2+ (tsi1 - 1) * 14));




                        tsi1++;
                    }

                    for (int i = 0; i < listBox3.Items.Count; i++)//打印镇痛药
                    {
                        if (i < 11)
                        {
                            e.Graphics.DrawString((listBox3.Items[i].ToString()),
                                ptzt7, Brushes.Black, new Point(x +575, YY +2+ i * 14));
                        }
                        if (i >=11)
                        {
                            break;
                        }


                    }
                    #endregion

                  

                    #region 打印尾部区域↓
                    //YY = YY + 70; Y_unLine = YY + 19;
                    //e.Graphics.DrawLine(ptp, new Point(20 + x, YY), new Point(770 + x, YY));
                    //e.Graphics.DrawString("备注：" + txtRemark.Controls[0].Text, ptzt7, Brushes.Black, new Point(25 + x, YY + 3));
                    //e.Graphics.DrawLine(ptp, new Point(50 + x, Y_unLine), new Point(755 + x, Y_unLine));
                   
                    e.Graphics.DrawLine(ptp, new Point(20 + x, YY), new Point(770 + x, YY));
                    e.Graphics.DrawLine(ptp, new Point(40 + x, YY), new Point(40 + x, YY + 160));
                    e.Graphics.DrawLine(ptp, new Point(300 + x, YY), new Point(300 + x, YY + 160));
                    e.Graphics.DrawLine(ptp, new Point(320 + x, YY), new Point(320 + x, YY + 160));
                    e.Graphics.DrawLine(ptp, new Point(555 + x, YY), new Point(555 + x, YY + 160));
                    e.Graphics.DrawLine(ptp, new Point(575 + x, YY), new Point(575 + x, YY + 160));
                    e.Graphics.DrawLine(ptp, new Point(20 + x, YY + 160), new Point(770 + x, YY + 160));
                    e.Graphics.DrawString("术\n\n中\n\n事\n\n件", ptzt11, Brushes.Black, new Point(x + 20, YY));
                    e.Graphics.DrawString("术\n\n中\n\n用\n\n药", ptzt11, Brushes.Black, new Point(300 + x, YY));
                    e.Graphics.DrawString("\n\n镇\n\n痛\n\n药", ptzt11, Brushes.Black, new Point(555 + x, YY));
                    YY = YY + 170; Y_unLine = YY + 14;
                    e.Graphics.DrawString("手术后诊断 " + txtsshzd.Controls[0].Text, ptzt8, Brushes.Black, 25 + x, YY);//诊断
                    e.Graphics.DrawLine(pxuxian, new Point(70 + x, Y_unLine), new Point(500 + x, Y_unLine));
                    e.Graphics.DrawString("PAC   :  " + cmbzhentongy.Text, ptzt8, Brushes.Black, 510 + x, YY);
                    e.Graphics.DrawLine(pxuxian, new Point(560 + x, Y_unLine), new Point(600 + x, Y_unLine));
                    e.Graphics.DrawString("自 体 血 : " + txtzitixue.Controls[0].Text, ptzt8, Brushes.Black, 610 + x, YY);
                    e.Graphics.DrawLine(pxuxian, new Point(670 + x, Y_unLine), new Point(720 + x, Y_unLine));
                    e.Graphics.DrawString("毫升", ptzt8, Brushes.Black, 720 + x, YY);
                    e.Graphics.DrawString("成分输血 : " + txtcfsx.Controls[0].Text, ptzt8, Brushes.Black, 610 + x, YY + 15);
                    e.Graphics.DrawLine(pxuxian, new Point(670 + x, Y_unLine + 15), new Point(720 + x, Y_unLine + 15));
                    e.Graphics.DrawString("毫升", ptzt8, Brushes.Black, 720 + x, YY + 15);
                    e.Graphics.DrawString("胶 体 液 : " + txtjiaotixue.Controls[0].Text, ptzt8, Brushes.Black, 610 + x, YY + 30);
                    e.Graphics.DrawLine(pxuxian, new Point(670 + x, Y_unLine + 30), new Point(720 + x, Y_unLine + 30));
                    e.Graphics.DrawString("毫升", ptzt8, Brushes.Black, 720 + x, YY + 30);
                    e.Graphics.DrawString("晶 体 液 : " + txtjingtixue.Controls[0].Text, ptzt8, Brushes.Black, 610 + x, YY + 45);
                    e.Graphics.DrawLine(pxuxian, new Point(670 + x, Y_unLine + 45), new Point(720 + x, Y_unLine + 45));
                    e.Graphics.DrawString("毫升", ptzt8, Brushes.Black, 720 + x, YY + 45);
                    e.Graphics.DrawString("总输入量 : " + txtzongsrl.Controls[0].Text, ptzt8, Brushes.Black, 610 + x, YY + 60);
                    e.Graphics.DrawLine(pxuxian, new Point(670 + x, Y_unLine+60), new Point(720 + x, Y_unLine+60));
                    e.Graphics.DrawString("毫升", ptzt8, Brushes.Black, 720 + x, YY +60);
                    e.Graphics.DrawString("出 血 量:  " + txtchuxuel.Controls[0].Text, ptzt8, Brushes.Black, 610 + x, YY + 75);
                    e.Graphics.DrawLine(pxuxian, new Point(670 + x, Y_unLine+75), new Point(720 + x, Y_unLine+75));
                    e.Graphics.DrawString("毫升", ptzt8, Brushes.Black, 720 + x, YY +75);
                    e.Graphics.DrawString("尿    量:  " + txtniaoliang.Controls[0].Text, ptzt8, Brushes.Black, 610 + x, YY + 90);
                    e.Graphics.DrawLine(pxuxian, new Point(670 + x, Y_unLine+90), new Point(720 + x, Y_unLine+90));
                    e.Graphics.DrawString("毫升", ptzt8, Brushes.Black, 720 + x, YY+90);
                    YY = YY + 30; Y_unLine = YY + 14;
                    e.Graphics.DrawString("手术方式 " + txtShoushuFS.Controls[0].Text, ptzt8, Brushes.Black, 25 + x, YY);
                    e.Graphics.DrawLine(pxuxian, new Point(70 + x, Y_unLine), new Point(500 + x, Y_unLine));
                    e.Graphics.DrawString("麻醉效果 " + cmbmazuixiaoguo.Text, ptzt8, Brushes.Black, 510 + x, YY);
                    e.Graphics.DrawLine(pxuxian, new Point(560 + x, Y_unLine), new Point(600 + x, Y_unLine));
                   
                    YY = YY + 30; Y_unLine = YY + 14;
                    e.Graphics.DrawString("麻醉方式 " + txtMazuiFS.Controls[0].Text, ptzt8, Brushes.Black, 25 + x, YY);
                    e.Graphics.DrawLine(pxuxian, new Point(70 + x, Y_unLine), new Point(300 + x, Y_unLine));
                    e.Graphics.DrawString("    麻醉医师    " + txtMZYS.Controls[0].Text, ptzt8, Brushes.Black, 300 + x, YY);
                    e.Graphics.DrawLine(pxuxian, new Point(380 + x, Y_unLine), new Point(500 + x, Y_unLine));
                    e.Graphics.DrawString("病人送往 " + cmbBRQX.Text, ptzt8, Brushes.Black, 510 + x, YY);
                    e.Graphics.DrawLine(pxuxian, new Point(560 + x, Y_unLine), new Point(600 + x, Y_unLine));
                    YY = 30 + YY; Y_unLine = YY + 14;
                    e.Graphics.DrawString("手术医师 " + txtSSYS.Controls[0].Text, ptzt8, Brushes.Black, 25 + x, YY);
                    e.Graphics.DrawLine(pxuxian, new Point(70 + x, Y_unLine), new Point(300 + x, Y_unLine));
                    e.Graphics.DrawString("器械、巡回护士 " + txtXHHS.Controls[0].Text, ptzt8, Brushes.Black, 300 + x, YY);
                    e.Graphics.DrawLine(pxuxian, new Point(380 + x, Y_unLine), new Point(500 + x, Y_unLine));
                    e.Graphics.DrawString("评    分： " + txtmazuipingfen.Controls[0].Text, ptzt8, Brushes.Black, 510 + x, YY);
                    e.Graphics.DrawLine(pxuxian, new Point(560 + x, Y_unLine), new Point(600 + x, Y_unLine));
                    //e.Graphics.DrawString("麻醉医师 " + txtMZYS.Controls[0].Text, ptzt8, Brushes.Black, 425 + x, YY);
                    //e.Graphics.DrawLine(pxuxian, new Point(470 + x, Y_unLine), new Point(770 + x, Y_unLine));
                    //YY = 30 + YY; Y_unLine = YY + 14;
                    //e.Graphics.DrawString("器械护士 " + txtQXHS.Controls[0].Text, ptzt8, Brushes.Black, 25 + x, YY);
                    //e.Graphics.DrawLine(pxuxian, new Point(70 + x, Y_unLine), new Point(400 + x, Y_unLine));
                    //e.Graphics.DrawString("巡回护士 " + txtXHHS.Controls[0].Text, ptzt8, Brushes.Black, 425 + x, YY);
                    //e.Graphics.DrawLine(pxuxian, new Point(470 + x, Y_unLine), new Point(770 + x, Y_unLine));
                    //YY = 25 + YY; Y_unLine = YY + 14;
                    //e.Graphics.DrawString("切口类型 " + cmbBRQX.Text, ptzt8, Brushes.Black, 25 + x, YY);
                    //e.Graphics.DrawLine(ptp, new Point(70 + x, Y_unLine), new Point(200 + x, Y_unLine));
                    //e.Graphics.DrawString("切口数量 " + cmbBRQX.Text, ptzt8, Brushes.Black, 210 + x, YY);
                    //e.Graphics.DrawLine(ptp, new Point(255 + x, Y_unLine), new Point(350 + x, Y_unLine));
                    //e.Graphics.DrawString("病人去向 " + cmbBRQX.Text, ptzt8, Brushes.Black, 360 + x, YY);
                    //e.Graphics.DrawLine(ptp, new Point(405 + x, Y_unLine), new Point(530 + x, Y_unLine));
                }
                    #endregion


            
        }

        PrintDocument printDocument = new PrintDocument();
        private void btnPrintView_Click(object sender, EventArgs e) /// 打印预览
        {
            int JsFlag = 0;
            string YwName = string.Empty;
            foreach (adims_MODEL.mzqt qt in mzqtList)//判断气体药是否结束
            {
                if (qt.Bz == 1)
                {
                    YwName = YwName + qt.Qtname + "\n";
                    JsFlag++;
                }

            }
            foreach (adims_MODEL.mzyt ydy in ydyList) //判断诱导药是否结束
            {
                if (ydy.Bz == 1 && ydy.Cxyy)
                {
                    YwName = YwName + ydy.Ytname + "\n";
                    JsFlag++;
                }
            }
            if (JsFlag > 0)
            {
                MessageBox.Show(YwName + "没用结束，不能打印");
                return;

            }
            ptime = fristopen;
            PrintDialog pd = new PrintDialog();

            pd.Document = printDocument;
            
            if (DialogResult.OK == pd.ShowDialog()) //如果确认，将会覆盖所有的打印参数设置
            {
                //页面设置对话框（可以不使用，其实PrintDialog对话框已提供页面设置）
                PageSetupDialog psd = new PageSetupDialog();
                psd.Document = printDocument;
                // this.pageSetupDialog1.ShowDialog();
                //BindJHDian();
                //pictureBox4.Refresh();
                //pictureBox3.Refresh();
                //pictureBox2.Refresh();

                 if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                    //printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
                    //printPreviewDialog1.WindowState = FormWindowState.Maximized;
                    //printDocument1.DefaultPageSettings.PaperSize =
                    //           new System.Drawing.Printing.PaperSize("K16", 740, 1020);
                }
                //if (printPreviewDialog1.ShowDialog() == DialogResult.OK) 
                //    printDocument1.Print();
            }
            iYema = 1;
        }

        private void btnLeft_Click(object sender, EventArgs e)/// 向左移动
        {
            otime = otime.AddMinutes(6 * jcsjjg);
            lbtimew1.Text = lbTime1.Text = otime.ToString("HH:mm");
            lbtimew2.Text = lbTime2.Text = otime.AddMinutes(6 * jcsjjg).ToString("HH:mm");
            lbtimew3.Text = lbTime3.Text = otime.AddMinutes(12 * jcsjjg).ToString("HH:mm");
            lbtimew4.Text = lbTime4.Text = otime.AddMinutes(18 * jcsjjg).ToString("HH:mm");
            lbtimew5.Text = lbTime5.Text = otime.AddMinutes(24 * jcsjjg).ToString("HH:mm");
            lbtimew6.Text = lbTime6.Text = otime.AddMinutes(30 * jcsjjg).ToString("HH:mm");
            lbtimew7.Text = lbTime7.Text = otime.AddMinutes(36 * jcsjjg).ToString("HH:mm");
            lbtimew8.Text = lbTime8.Text = otime.AddMinutes(42 * jcsjjg).ToString("HH:mm");
            lbtimew9.Text = lbTime9.Text = otime.AddMinutes(48 * jcsjjg).ToString("HH:mm");
            lbtimew10.Text = lbTime10.Text = otime.AddMinutes(54 * jcsjjg).ToString("HH:mm");
            lbMzks1.Location = new Point(lbMzks1.Location.X - 90, lbMzks1.Location.Y);
            lbMzjs1.Location = new Point(lbMzjs1.Location.X - 90, lbMzjs1.Location.Y);
            lbSSKS.Location = new Point(lbSSKS.Location.X - 90, lbSSKS.Location.Y);
            lbSSJS.Location = new Point(lbSSJS.Location.X - 90, lbSSJS.Location.Y);
            lbCguan.Location = new Point(lbCguan.Location.X - 90, lbCguan.Location.Y);
            lbBguan.Location = new Point(lbBguan.Location.X - 90, lbBguan.Location.Y);
            //插管，拔管时间暂时未完成
            lab1.Text = "";
            pictureBox1.Refresh();
            pictureBox2.Refresh();
            pictureBox3.Refresh();
            //pictureBox4.Refresh();
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
            lbtimew3.Text = lbTime3.Text = otime.AddMinutes(12 * jcsjjg).ToString("HH:mm");
            lbtimew4.Text = lbTime4.Text = otime.AddMinutes(18 * jcsjjg).ToString("HH:mm");
            lbtimew5.Text = lbTime5.Text = otime.AddMinutes(24 * jcsjjg).ToString("HH:mm");
            lbtimew6.Text = lbTime6.Text = otime.AddMinutes(30 * jcsjjg).ToString("HH:mm");
            lbtimew7.Text = lbTime7.Text = otime.AddMinutes(36 * jcsjjg).ToString("HH:mm");
            lbtimew8.Text = lbTime8.Text = otime.AddMinutes(42 * jcsjjg).ToString("HH:mm");
            lbtimew9.Text = lbTime9.Text = otime.AddMinutes(48 * jcsjjg).ToString("HH:mm");
            lbtimew10.Text = lbTime10.Text = otime.AddMinutes(54 * jcsjjg).ToString("HH:mm");
            lbMzks1.Location = new Point(lbMzks1.Location.X + 90, lbMzks1.Location.Y);
            lbMzjs1.Location = new Point(lbMzjs1.Location.X + 90, lbMzjs1.Location.Y);
            lbSSKS.Location = new Point(lbSSKS.Location.X + 90, lbSSKS.Location.Y);
            lbSSJS.Location = new Point(lbSSJS.Location.X + 90, lbSSJS.Location.Y);
            lbCguan.Location = new Point(lbCguan.Location.X + 90, lbCguan.Location.Y);
            lbBguan.Location = new Point(lbBguan.Location.X + 90, lbBguan.Location.Y);
            //插管，拔管时间暂时未完成
            lab1.Text = "";
            pictureBox1.Refresh();
            pictureBox2.Refresh();
            pictureBox3.Refresh();

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
                    string result = string.Empty;
                    if (cmbASA.Text.IsNullOrEmpty())
                    {
                        MessageBox.Show("ASA分级不能为空！");
                        e.Cancel = true;
                        return;
                    }
                    else
                    {
                        int i = SaveMzjld();
                    }
                    //if (!mzjs)
                    //    result += "麻醉、";
                    if (!ssjs)
                        result += "手术、";
                    if (timer1.Enabled == true)
                        result += "数据采集";
                    if (!string.IsNullOrEmpty(result))
                    {
                        MessageBox.Show(result + "没有结束，不能关闭！", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        e.Cancel = true;
                        return;
                    }
                    int JsFlag = 0;
                    string YwName = string.Empty;
                    //foreach (adims_MODEL.mzqt qt in mzqtList)//判断气体药是否结束
                    //{
                    //    if (qt.Bz == 1)
                    //    {
                    //        YwName = YwName + qt.Qtname + "\n";
                    //        JsFlag++;
                    //    }
                    //}
                    foreach (adims_MODEL.mzyt ydy in ydyList) //判断诱导药是否结束
                    {
                        if (ydy.Bz == 1 && ydy.Cxyy)
                        {
                            YwName = YwName + ydy.Ytname + "\n";
                            JsFlag++;
                        }
                    }
                    if (JsFlag > 0)
                    {
                        MessageBox.Show(YwName + "没有结束，不能退出");
                        e.Cancel = true;
                        return;

                    }
                    this.FormClosing -= new FormClosingEventHandler(this.mzjldEdit_FormClosing);
                    dal.UpdateShoushujianinfo(0, 0, "0", Oroom);//关闭手术间状态
                    this.Close();//退出  
                }
                else
                    e.Cancel = true;  //取消关闭事件   
            }
        }

        #region <<麻醉开始，标志移动>>

        /// <summary>
        /// 双击麻醉开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        int xStart, xEnd, yStart;//移动麻醉，手术，插管开始结束点
        private void lbMzks_DoubleClick(object sender, EventArgs e)
        {
            if (!IsInNet())
            {
                return;
            }
           
                //txtMzsj.Controls[0].Text = Convert.ToString(DateTime.Now);
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                lbMzks1.Visible = true;
                lbMzks1.Text = "X";
                lbMzks1.AutoSize = true;
                lbMzks1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lbMzks1.BackColor = Color.Transparent;
                lbMzks1.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 160), 370);
                this.pictureBox3.Controls.Add(lbMzks1);
                mzks = true;
                mzjs = false;
                dal.UpdateMzkssj(DateTime.Now, mzjldID);
                mzksTime = DateTime.Now;
                dal.UpdatePaibanInfo(2, patID);
                dal.UpdateShoushujianinfo(2, Oroom);
                //txtMZKS.Controls[0].Text = DateTime.Now.ToString("HH:mm");
                lbMzks1.MouseDown += new MouseEventHandler(lbMzks1_MouseDown);
                lbMzks1.MouseMove += new MouseEventHandler(lbMzks1_MouseMove);
                lbMzks1.MouseUp += new MouseEventHandler(lbMzks1_MouseUp);
                lbMzks1.MouseLeave += new EventHandler(lbMzks1_MouseLeave);
            
        }

        private void lbMzks1_MouseDown(object sender, EventArgs e)
        {
            if (!IsInNet())
            {
                return;
            }
            ssmzcgbgFlag = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.Red;
            lab1.AutoSize = true;
            pictureBox3.Controls.Add(lab1);
            lab1.Location = new Point(lbMzks1.Location.X, lbMzks1.Location.Y - 10);
            xStart = lbMzks1.Location.X;
            yStart = lbMzks1.Location.Y;
            DateTime DTIME = otime.AddMinutes((xStart - 160) / 15 * jcsjjg);
            lab1.Text = DTIME.ToString("HH:mm");
        }

        private void lbMzks1_MouseMove(object sender, MouseEventArgs e)
        {
            if (ssmzcgbgFlag == 1)
            {
                //TimeSpan t = new TimeSpan();
                //t = DateTime.Now - otime;
                lbMzks1.Location = new Point(lbMzks1.Location.X + e.X / 2 - 2, lbMzks1.Location.Y);
                xEnd = lbMzks1.Location.X;
                lab1.Visible = true;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Blue;
                lab1.AutoSize = true;
                pictureBox3.Controls.Add(lab1);
                lab1.Location = new Point(lbMzks1.Location.X, lbMzks1.Location.Y - 10);
                DateTime DTIME = otime.AddMinutes((xEnd - 160) / 15 * jcsjjg);
                lab1.Text = DTIME.ToString("HH:mm");
                labVisibleTimer.Enabled = true;
                //txtMZKS.Controls[0].Text = DTIME.ToString("HH:mm");


            }
        }

        private void lbMzks1_MouseUp(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
            if (!mzjs)
            {
                dal.UpdateMzkssj(otime.AddMinutes((xEnd - 160) / 15 * jcsjjg), mzjldID);
                mzksTime = otime.AddMinutes((xEnd - 160) / 3);
            }
            else
            {
                if (xEnd < lbMzjs1.Location.X)
                {
                    dal.UpdateMzkssj(otime.AddMinutes((xEnd - 160) / 15 * jcsjjg), mzjldID);
                    mzksTime = otime.AddMinutes((xEnd - 160) / 15 * jcsjjg);
                }
                else
                    lbMzks1.Location = new Point(xStart, yStart);
            }
            //BindMZSSCGBG();
            pictureBox3.Refresh();
        }

        private void lbMzks1_MouseLeave(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
        }

        #endregion

        #region <<麻醉结束，标志移动>>
        /// <summary>
        /// 麻醉结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbMzjs_DoubleClick(object sender, EventArgs e)
        {
            if (!IsInNet())
            {
                return;
            }
            if (mzks && !mzjs)
            {
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                mzjsTime = DateTime.Now;
                lbMzjs1.Visible = true;
                lbMzjs1.Text = "X";
                lbMzjs1.AutoSize = true;
                lbMzjs1.BackColor = Color.Transparent;
                lbMzjs1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lbMzjs1.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 160), 370);
                this.pictureBox3.Controls.Add(lbMzjs1);
                mzjs = true;
                dal.UpdateMzjssj(DateTime.Now, mzjldID);
                mzjsTime = DateTime.Now;
                //txtMZJS.Controls[0].Text = DateTime.Now.ToString("HH:mm");
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
            if (!IsInNet())
            {
                return;
            }
            ssmzcgbgFlag = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.Blue;
            lab1.AutoSize = true;
            pictureBox3.Controls.Add(lab1);
            lab1.Location = new Point(lbMzjs1.Location.X, lbMzjs1.Location.Y - 10);
            xStart = lbMzjs1.Location.X;
            yStart = lbMzjs1.Location.Y;
            DateTime DTIME = otime.AddMinutes((lbMzjs1.Location.X - 160) / 15 * jcsjjg);
            lab1.Text = DTIME.ToString("HH:mm");
        }

        private void lbMzjs1_MouseMove(object sender, MouseEventArgs e)
        {
            if (ssmzcgbgFlag == 1)
            {
                lbMzjs1.Location = new Point(lbMzjs1.Location.X + e.X / 2 - 2, lbMzjs1.Location.Y);
                xEnd = lbMzjs1.Location.X;
                lab1.Visible = true;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Blue;
                lab1.AutoSize = true;
                pictureBox3.Controls.Add(lab1);
                lab1.Location = new Point(lbMzjs1.Location.X, lbMzjs1.Location.Y - 10);
                DateTime DTIME = otime.AddMinutes((lbMzjs1.Location.X - 160) / 15 * jcsjjg);
                lab1.Text = DTIME.ToString("HH:mm");
                labVisibleTimer.Enabled = true;
                //int ssshijian = Convert.ToInt32((lbMzjs1.Location.X - lbMzks1.Location.X) / 3);//计算麻醉总时间
                //txtMZJS.Controls[0].Text =DTIME.ToString("HH:mm");

            }
        }

        private void lbMzjs1_MouseUp(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
            if (xEnd > lbMzks1.Location.X)
            {
                dal.UpdateMzjssj(otime.AddMinutes((xEnd - 160) / 15 * jcsjjg), mzjldID);
                mzjsTime = otime.AddMinutes((xEnd - 160) / 15 * jcsjjg);
            }
            else
                lbMzjs1.Location = new Point(xStart, yStart);
            pictureBox3.Refresh();            

        }

        private void lbMzjs1_MouseLeave(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
        }
        #endregion

        #region <<插管，标志移动>>
        private void lbCg_DoubleClick(object sender, EventArgs e)
        {
            if (!IsInNet())
            {
                return;
            }
            if (!CGUAN)
            {
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                cgTime = DateTime.Now;
                lbCguan.Visible = true;
                lbCguan.Text = " ";
                lbCguan.Image = Properties.Resources.CG;
                lbCguan.AutoSize = true;
                lbCguan.BackColor = Color.Transparent;
                lbCguan.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lbCguan.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 160), 370);
                this.pictureBox3.Controls.Add(lbCguan);
                CGUAN = true;
                BGUAN = false;
                JKTimeSet cg = new JKTimeSet(mzjldID);
                cg.Show();
                dal.UpdateMzCG(DateTime.Now, mzjldID);
                cgTime = DateTime.Now;
                //txtCGKS.Controls[0].Text = DateTime.Now.ToString("HH:mm");
                lbCguan.MouseDown += new MouseEventHandler(lb_cguan_MouseDown);
                lbCguan.MouseMove += new MouseEventHandler(lb_cguan_MouseMove);
                lbCguan.MouseUp += new MouseEventHandler(lb_cguan_MouseUp);
                lbCguan.MouseLeave += new EventHandler(lb_cguan_MouseLeave);
            }
            else
                MessageBox.Show("已经插管");
        }

        private void lb_cguan_MouseDown(object sender, EventArgs e)
        {
            if (!IsInNet())
            {
                return;
            }
            ssmzcgbgFlag = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.Blue;
            lab1.AutoSize = true;
            pictureBox3.Controls.Add(lab1);
            lab1.Location = new Point(lbCguan.Location.X, lbCguan.Location.Y - 10);
            xStart = lbCguan.Location.X;
            yStart = lbCguan.Location.Y;
            DateTime DTIME = otime.AddMinutes((xStart - 160) / 15 * jcsjjg);
            lab1.Text = DTIME.ToString("HH:mm");

        }

        private void lb_cguan_MouseLeave(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
        }

        private void lb_cguan_MouseMove(object sender, MouseEventArgs e)
        {
            if (ssmzcgbgFlag == 1)
            {
                lbCguan.Location = new Point(lbCguan.Location.X + e.X / 2 - 2, lbCguan.Location.Y);
                xEnd = lbCguan.Location.X;
                lab1.Visible = true;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Blue;
                lab1.AutoSize = true;
                pictureBox3.Controls.Add(lab1);
                lab1.Location = new Point(lbCguan.Location.X, lbCguan.Location.Y - 10);
                DateTime DTIME = otime.AddMinutes((xEnd - 160) / 15 * jcsjjg);
                lab1.Text = DTIME.ToString("HH:mm");
                labVisibleTimer.Enabled = true;
            }
        }

        private void lb_cguan_MouseUp(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
            if (!BGUAN)
            {
                dal.UpdateMzCG(otime.AddMinutes((xEnd - 160) / 15 * jcsjjg), mzjldID);
                cgTime = otime.AddMinutes((xEnd - 160) / 15 * jcsjjg);
            }
            else
            {
                if (xEnd < lbBguan.Location.X)
                {
                    dal.UpdateMzCG(otime.AddMinutes((xEnd - 160) / 15 * jcsjjg), mzjldID);
                    cgTime = otime.AddMinutes((xEnd - 160) / 15 * jcsjjg);
                }
                else
                
                    lbCguan.Location = new Point(xStart, yStart);
            }
            BindJHDian();
            pictureBox3.Refresh();
        }
        #endregion

        #region <<拔管，标志移动>>
        private void lbBg_DoubleClick(object sender, EventArgs e)
        {
            if (!IsInNet())
            {
                return;
            }
            if (CGUAN && !BGUAN)
            {
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                bgTime = DateTime.Now;
                lbBguan.Visible = true;
                lbBguan.Text = " ";
                lbBguan.Image = Properties.Resources.BG;
                lbBguan.AutoSize = true;
                lbBguan.BackColor = Color.Transparent;
                lbBguan.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lbBguan.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 160), 370);
                this.pictureBox3.Controls.Add(lbBguan);
                BGUAN = true;
                dal.UpdateMzBG(DateTime.Now, mzjldID);
                bgTime = DateTime.Now;
                //txtCGJS.Controls[0].Text = DateTime.Now.ToString("HH:mm");
                lbBguan.MouseDown += new MouseEventHandler(lb_bguan_MouseDown);
                lbBguan.MouseMove += new MouseEventHandler(lb_bguan_MouseMove);
                lbBguan.MouseUp += new MouseEventHandler(lb_bguan_MouseUp);
                lbBguan.MouseLeave += new EventHandler(lb_bguan_MouseLeave);
            }
            else
                MessageBox.Show("没有插管或者拔管已经结束");
        }

        private void lb_bguan_MouseDown(object sender, EventArgs e)
        {
            if (!IsInNet())
            {
                return;
            }
            ssmzcgbgFlag = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.Blue;
            lab1.AutoSize = true;
            pictureBox3.Controls.Add(lab1);
            lab1.Location = new Point(lbBguan.Location.X, lbBguan.Location.Y - 10);
            xStart = lbBguan.Location.X;
            yStart = lbBguan.Location.Y;
            DateTime DTIME = otime.AddMinutes((xStart - 160) / 15 * jcsjjg);
            lab1.Text = DTIME.ToString("HH:mm");
        }

        private void lb_bguan_MouseMove(object sender, MouseEventArgs e)
        {
            if (ssmzcgbgFlag == 1)
            {
                lbBguan.Location = new Point(lbBguan.Location.X + e.X / 2 - 2, lbBguan.Location.Y);
                xEnd = lbBguan.Location.X;
                lab1.Visible = true;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Blue;
                lab1.AutoSize = true;
                pictureBox3.Controls.Add(lab1);
                lab1.Location = new Point(lbBguan.Location.X, lbBguan.Location.Y - 10);
                DateTime DTIME = otime.AddMinutes((xEnd - 160) / 15 * jcsjjg);
                lab1.Text = DTIME.ToString("HH:mm");
                labVisibleTimer.Enabled = true;
                //int ssshijian = Convert.ToInt32((lb_bguan.Location.X - lb_bguan.Location.X) / 3);//计算拔管时间
                //txtCGJS.Controls[0].Text = DTIME.ToString("HH:mm");
            }
        }

        private void lb_bguan_MouseUp(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
            if (xEnd > lbCguan.Location.X)
            {
                dal.UpdateMzBG(otime.AddMinutes((xEnd - 160) / 15 * jcsjjg), mzjldID);
                bgTime = otime.AddMinutes((xEnd - 160) / 15 * jcsjjg);
            }
            else
                lbBguan.Location = new Point(xStart, yStart);
            BindJHDian();
            pictureBox3.Refresh();

        }

        private void lb_bguan_MouseLeave(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
        }
        #endregion

        #region <<手术开始，开始标志移动>>

        /// <summary>
        /// 双击手术开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbSsks_DoubleClick(object sender, EventArgs e)
        {
            if (!IsInNet())
            {
                return;
            }
            if (!ssks)
            {
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                lbSSKS.Visible = true;
                lbSSKS.Text = "⊙";
                lbSSKS.AutoSize = true;
                lbSSKS.BackColor = Color.Transparent;
                lbSSKS.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lbSSKS.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 160), 370);
                this.pictureBox3.Controls.Add(lbSSKS);
                ssks = true;
                ssjs = false;
                dal.UpdateSskssj(DateTime.Now, mzjldID);
                ssksTime = DateTime.Now;
                dal.UpdateShoushujianinfo(2, Oroom);//修改手术间状态表为正在手术
                //txtSSKS.Controls[0].Text = DateTime.Now.ToString("HH:mm");
                lbSSKS.MouseDown += new MouseEventHandler(ssks1_MouseDown);//注册鼠标按下事件
                lbSSKS.MouseMove += new MouseEventHandler(ssks1_MouseMove);//注册鼠标移动事件
                lbSSKS.MouseUp += new MouseEventHandler(ssks1_MouseUp);//注册鼠标按下取消事件
                lbSSKS.MouseLeave += new EventHandler(ssks1_MouseLeave);
            }
            else
                MessageBox.Show("手术已经开始");
        }

        private void ssks1_MouseDown(object sender, EventArgs e)
        {
            if (!IsInNet())
            {
                return;
            }
            ssmzcgbgFlag = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.Blue;
            lab1.AutoSize = true;
            pictureBox3.Controls.Add(lab1);
            lab1.Location = new Point(lbSSKS.Location.X, lbSSKS.Location.Y - 10);
            xStart = lbSSKS.Location.X;
            yStart = lbSSKS.Location.Y;
            DateTime DTIME = otime.AddMinutes((xStart - 160) / 15 * jcsjjg);
            lab1.Text = DTIME.ToString("HH:mm");
        }

        private void ssks1_MouseUp(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
            if (!ssjs)
            {
                dal.UpdateSskssj(otime.AddMinutes((xEnd - 160) / 15 * jcsjjg), mzjldID);
                ssksTime = otime.AddMinutes((xEnd - 160) / 15 * jcsjjg);
            }
            else
            {
                if (xEnd < lbSSJS.Location.X)
                {
                    dal.UpdateSskssj(otime.AddMinutes((xEnd - 160) / 15 * jcsjjg), mzjldID);
                    ssksTime = otime.AddMinutes((xEnd - 160) / 15 * jcsjjg);
                }
                else
                    lbSSKS.Location = new Point(xStart, yStart);
            }           
            pictureBox3.Refresh();
           

        }

        

        private void ssks1_MouseMove(object sender, MouseEventArgs e)
        {
            if (ssmzcgbgFlag == 1)
            {
                lbSSKS.Location = new Point(lbSSKS.Location.X + e.X / 2 - 2, lbSSKS.Location.Y);

                xEnd = lbSSKS.Location.X;
                lab1.Visible = true;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Blue;
                lab1.AutoSize = true;
                pictureBox3.Controls.Add(lab1);
                lab1.Location = new Point(lbSSKS.Location.X, lbSSKS.Location.Y - 10);
                DateTime DTIME = otime.AddMinutes((xEnd - 160) / 15 * jcsjjg);
                lab1.Text = DTIME.ToString("HH:mm");
                labVisibleTimer.Enabled = true;
                //txtSSKS.Controls[0].Text = DTIME.ToString("HH:mm");
            }
        }
        private void ssks1_MouseLeave(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
        }
        #endregion

        #region <<手术结束，结束标志移动>>
        /// <summary>
        /// 双击手术结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void lbSsjs_DoubleClick(object sender, EventArgs e)
        {
            if (!IsInNet())
            {
                return;
            }
            if (ssks && !ssjs)
            {
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                lbSSJS.Text = " ";
                lbSSJS.Image = Properties.Resources.SSJS;
                lbSSJS.Visible = true;
                lbSSJS.AutoSize = true;
                lbSSJS.BackColor = Color.Transparent;
                lbSSJS.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lbSSJS.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 160), 370);
                this.pictureBox3.Controls.Add(lbSSJS);
                ssjs = true;
                dal.UpdateSsjssj(DateTime.Now, mzjldID);
                dal.UpdateSsjsFlag(mzjldID);
                dal.UpdateShoushujianinfo(4, Oroom);
                ssjsTime = DateTime.Now;
                //txtSSJS.Controls[0].Text = DateTime.Now.ToString("HH:mm");
                lbSSJS.MouseDown += new MouseEventHandler(ssjs1_MouseDown);
                lbSSJS.MouseMove += new MouseEventHandler(ssjs1_MouseMove);
                lbSSJS.MouseUp += new MouseEventHandler(ssjs1_MouseUp);
                lbSSJS.MouseLeave += new EventHandler(ssjs1_MouseLeave);

            }
            else
                MessageBox.Show("手术未开始或手术已经结束！");
        }

        private void ssjs1_MouseDown(object sender, EventArgs e)
        {
            if (!IsInNet())
            {
                return;
            }
            ssmzcgbgFlag = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.Blue;
            lab1.AutoSize = true;
            pictureBox3.Controls.Add(lab1);
            lab1.Location = new Point(lbSSJS.Location.X, lbSSJS.Location.Y - 10);
            xStart = lbSSJS.Location.X;
            yStart = lbSSJS.Location.Y;
            DateTime DTIME = otime.AddMinutes((xStart - 160) / 15 * jcsjjg);
            lab1.Text = DTIME.ToString("HH:mm");

        }

        private void ssjs1_MouseMove(object sender, MouseEventArgs e)
        {
            if (ssmzcgbgFlag == 1)
            {
                lbSSJS.Location = new Point(lbSSJS.Location.X + e.X / 2 - 2, lbSSJS.Location.Y);
                xEnd = lbSSJS.Location.X;
                lab1.Visible = true;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Blue;
                lab1.AutoSize = true;
                pictureBox3.Controls.Add(lab1);
                lab1.Location = new Point(lbSSJS.Location.X, lbSSJS.Location.Y - 10);
                DateTime DTIME = otime.AddMinutes((xEnd - 160) / 15 * jcsjjg);
                lab1.Text = DTIME.ToString("HH:mm");
                labVisibleTimer.Enabled = true;
                //int ssshijian=Convert.ToInt32((ssjs1.Location.X - ssks1.Location.X) / 3.27);
                //txtSssj.Controls[0].Text = (ssshijian / 60).ToString() + "小时"+(ssshijian % 60).ToString()+"分";
                //txtSSJS.Controls[0].Text = DTIME.ToString("HH:mm");
            }
        }

        private void ssjs1_MouseUp(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
            if (xEnd > lbSSKS.Location.X)
            {
                dal.UpdateSsjssj(otime.AddMinutes((xEnd - 160) / 15 * jcsjjg), mzjldID);
                ssjsTime = otime.AddMinutes((xEnd - 160) / 15 * jcsjjg);
            }
            else
                lbSSJS.Location = new Point(xStart, yStart);
        }

        private void ssjs1_MouseLeave(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
        }

        #endregion
        
        private void button8_Click(object sender, EventArgs e)/// 影像病历
        {
            string IPaddress = "192.168.18.70";
            bool flag = DataValid.PingHost(IPaddress, 1000);
            if (flag == true)
            {

                ///这里传入的参数 1 门诊 ， 2 住院 ， 3 处方号 ， 4社保号
                ///patID需要修改为住院号
                CallPacsForm cpf = new CallPacsForm(patidccs, 2);
                cpf.Show();

                //string inpatientID = patID;
                //string url = "http://192.168.6.210:8089/PacsDBProxy/PacsDatabase/PacsDatabase.action";
                //System.Diagnostics.Process.Start(url);
                ////[DllImport("user32.dll", EntryPoint="MessageBoxA")]
                ////  static extern int MsgBox(int hWnd, string msg, string caption, int type);
                ////int WINAPI PacsView(int nPatientType , char* lpszID , int nImageType)  
                ////Process proc = new Process();
                
                ////proc.StartInfo.FileName = "D:\\Display临床客户端\\TestJoint.exe";
               
                ////proc.StartInfo.Arguments = "nPatientType=1|lpszID="+patID+",nImageType=1";
                ////proc.StartInfo.UseShellExecute = false;
                ////proc.Start();
            }
            else MessageBox.Show("影响病历 数据库未连接，请检查网络");

            //yxbl yxblform = new yxbl();
            //yxblform.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)/// 检验病历
        {
            string IPaddress = "192.168.18.55";
            bool flag = DataValid.PingHost(IPaddress, 1000);
            if (flag == true)
            {
                string inpatientID = patidccs;
                string url = "http://192.168.18.55/modules/main/rmsysMain.aspx?rmkj_userno=webreport&HIS_PARAM_MODE=3&HIS_PARAM_PATNO=" + patidccs + "";
                System.Diagnostics.Process.Start(url);
            }
            else MessageBox.Show("影响病历 数据库未连接，请检查网络");
            //string IPaddress = "132.147.160.6";
            //bool flag = DataValid.PingHost(IPaddress, 1000);
            //if (flag == true)
            //{
            //   LisResult f1 = new LisResult(patID);
            //    f1.Show();
            //}
            //else MessageBox.Show("检验病历 数据库未连接，请检查网络");

        }

        private void button10_Click(object sender, EventArgs e)// 实时数据
        {
            sssj_temp myform = new sssj_temp();
            myform.Show();
        }

        public int SaveMzjld()// 更新麻醉记录单
        {
           
            if (!IsInNet())
            {
                return 0;
            }
            int result = 0;
            List<string> mzdList1 = new List<string>();
            mzdList1.Add(txtWeight.Controls[0].Text.Trim());//身高
            mzdList1.Add(txtHeight.Controls[0].Text.Trim());//体重
            mzdList1.Add(this.txtTW.Controls[0].Text.Trim());//体温
            mzdList1.Add(this.txtXueya.Controls[0].Text.Trim());//血压
            mzdList1.Add(this.txtHuxi.Controls[0].Text.Trim());//呼吸
            mzdList1.Add(this.txtMaibo.Controls[0].Text.Trim());//脉搏
            
            mzdList1.Add(this.cmbXueXing.Text.Trim());//血型
            mzdList1.Add(this.cmbASA.Text.Trim());//asa
            if (this.cbJizhen.Checked)//急诊
                mzdList1.Add("1");
            else
                mzdList1.Add("0");
            //mzdList1.Add(this.cmbSQJinshi.Text.Trim());//术前进食
           //mzdList1.Add(this.txtTSBQing.Controls[0].Text.Trim());//特殊病情
            mzdList1.Add(this.txtSqzd.Controls[0].Text.Trim());//术前诊断
            mzdList1.Add(this.txtNssss.Controls[0].Text.Trim());//拟施手术
            mzdList1.Add(this.cmbTiwei.Text.Trim());//体位
            mzdList1.Add(this.cmbSQJinshi.Text.Trim());//术前禁食
            mzdList1.Add(this.txtsshzd.Controls[0].Text.Trim());//术中诊断
            mzdList1.Add(this.txtShoushuFS.Controls[0].Text.Trim());//手术名称
            mzdList1.Add(this.txtMazuiFS.Controls[0].Text.Trim());//麻醉方法
            mzdList1.Add(this.txtSSYS.Controls[0].Text.Trim());//手术医生
            mzdList1.Add(this.txtMZYS.Controls[0].Text.Trim());//麻醉医生
            mzdList1.Add(this.txtXHHS.Controls[0].Text.Trim());//器械巡回
            
            mzdList1.Add(this.cmbzhentongy.Text.Trim());//镇痛药
            mzdList1.Add(this.cmbmazuixiaoguo.Text.Trim());//麻醉效果评价
            mzdList1.Add(this.cmbBRQX.Text.Trim());//病人送往
            mzdList1.Add(this.txtmazuipingfen.Controls[0].Text.Trim());//评分
            mzdList1.Add(this.txtzitixue.Controls[0].Text.Trim());//自体血
            mzdList1.Add(this.txtcfsx.Controls[0].Text.Trim());//成分输血
            mzdList1.Add(this.txtjiaotixue.Controls[0].Text.Trim());//胶体液
            mzdList1.Add(this.txtjingtixue.Controls[0].Text.Trim());//晶体液
            mzdList1.Add(this.txtzongsrl.Controls[0].Text.Trim());//总输入量
            mzdList1.Add(this.txtchuxuel.Controls[0].Text.Trim());//出血量
            mzdList1.Add(this.txtniaoliang.Controls[0].Text.Trim());//尿量
      


            //mzdList1.Add(this.txtXHHS.Controls[0].Text.Trim());
            //mzdList1.Add(txtWeight.Controls[0].Text.Trim());
            //mzdList1.Add(txtHeight.Controls[0].Text.Trim());
           // mzdList1.Add(txtSqyy.Controls[0].Text.Trim());
            //mzdList1.Add(tbChuNiao.Text.Trim());
            //mzdList1.Add(tbChuxue.Text.Trim());
            //mzdList1.Add(cmbQiekouType.Text.Trim());
            //mzdList1.Add(tbQiekouCount.Text.Trim());

            //mzdList1.Add(tXTPH.Text.Trim());
            //mzdList1.Add(txtpco2.Text.Trim());
            //mzdList1.Add(txtpo2.Text.Trim());
            //mzdList1.Add(txtbe.Text.Trim());
            //mzdList1.Add(txthco3.Text.Trim());
            //mzdList1.Add(txthb.Text.Trim());
            //mzdList1.Add(txtk.Text.Trim());
            //mzdList1.Add(txthct.Text.Trim());
            //mzdList1.Add(txtxuet.Text.Trim());
            string AddItem="";
            if (cheboxBIS.Checked == true) AddItem += "1";
            if (checktw.Checked == true) AddItem += "2";
            if (checkmb.Checked == true) AddItem += "3";
            if (checkssy.Checked == true) AddItem += "4";
            if (checkszy.Checked == true) AddItem += "5";
            if (checkhx.Checked == true) AddItem += "6";
            mzdList1.Add(AddItem);
            mzdList1.Add(Convert.ToString(mzjldID));
            result = dal.UpdateMzjld1(mzdList1);
            return result;

        }

        private void AddPointTSMenu_Click(object sender, EventArgs e)//右键检测点管理
        {
            PointManage slj = new PointManage(mzjldID, 0);
            slj.ShowDialog();
            BindJHDian();
            this.pictureBox2.Refresh();
            this.pictureBox3.Refresh();

        }


        #region <<画窗体格子>>

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen pblack2 = new Pen(Brushes.Black);
            pblack2.Width = 2;
            Pen pblack = new Pen(Brushes.Black);
            e.Graphics.DrawLine(pblack2, new Point(1, 5), new Point(1069, 5));
            e.Graphics.DrawLine(pblack, new Point(1, 140), new Point(1069, 140));
            e.Graphics.DrawLine(pblack2, new Point(1, 5), new Point(1, 166));
            e.Graphics.DrawLine(pblack2, new Point(1069, 5), new Point(1069, 166));
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            Font zt8 = new Font("宋体", 8);
            Font zt7 = new Font("宋体", 7);
            Pen pred2 = new Pen(Brushes.Red);
            pred2.Width = 2;
            Pen pblack2 = new Pen(Brushes.Black);
            pblack2.Width = 2;
            Pen pxuxian = new Pen(Brushes.Black);
            Pen pblue2 = new Pen(Brushes.Green);
            pblue2.Width = 2;   //画蓝色的横线
            pxuxian.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            e.Graphics.DrawLine(pblack2, new Point(1, 0), new Point(1, 561));
            e.Graphics.DrawLine(Pens.Black, new Point(1, 0), new Point(1069, 0));
            e.Graphics.DrawLine(Pens.Black, new Point(25, 1), new Point(25, 540));
            e.Graphics.DrawLine(Pens.Black, new Point(169, 1), new Point(169, 561));
            e.Graphics.DrawLine(pblack2, new Point(1069, 0), new Point(1069, 561));
            for (int i = 0; i < 37; i++)
            {
                if (i == 13 || i == 23 || i == 36)//|| i == 20
                    e.Graphics.DrawLine(Pens.Black, new Point(1, 15 * i), new Point(1069, 15 * i));
                else
                    e.Graphics.DrawLine(Pens.Black, new Point(25, 15 * i), new Point(1069, 15 * i));
            }
            //↑横细线            
            for (int i = 0; i < 60; i++)
                e.Graphics.DrawLine(pxuxian, new PointF((float)(i * 90 / 6 + 169), (float)1), new PointF((float)(i * 90 / 6 + 169), (float)540));
            for (int i = 0; i < 10; i++)
                e.Graphics.DrawLine(Pens.Black, new PointF((float)(i * 90 + 169), (float)1), new PointF((float)(i * 90 + 169), (float)542));

            #region //画气体别人写的气体用药
            ////ArrayList sssQT = new ArrayList();//气体名字类集合
            ////int dy = 0;// 控制画气体输出的Y坐标           
            ////foreach (adims_MODEL.mzqt mz in mzqtList)
            ////{
            ////    if (mz.Bz > 0)
            ////    {
            ////        if (sssQT.Contains(mz.Qtname))//如果包含，标志行-1
            ////            dy = dy - 1;
            ////        //e.Graphics.DrawString(mz.Qtname, this.Font, Brushes.Black, new Point(35, 15 * (dy + 0) + 525));
            ////        TimeSpan t = new TimeSpan();
            ////        TimeSpan t1 = new TimeSpan();
            ////        if (mz.Bz == 1)//气体未结束
            ////        {
            ////            t = mz.Sysj - otime;//开始时间和起始时间间隔
            ////            t1 = DateTime.Now - otime;//当前时间和起始时间间隔
            ////        }
            ////        else if (mz.Bz == 2)//气体结束
            ////        {
            ////            t = mz.Sysj - otime;//开始时间和起始时间间隔
            ////            t1 = mz.Jssj - otime;//结束时间和起始时间间隔
            ////        }
            ////        float x1 = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);//气体开始坐标
            ////        float x2 = (float)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 170);//气体结束坐标
            ////        float y1 = 15 * (dy) + 528;//气体纵坐标
            ////        if (x1 > 170)
            ////        {
            ////            e.Graphics.FillPolygon(Brushes.Black, new PointF[3] { new PointF(x1, y1), new PointF(x1 - 5, y1 + 8), new PointF(x1 + 5, y1 + 8) });//气体开始点
            ////            e.Graphics.DrawString(mz.Yl.ToString() + mz.Dw.ToString(), this.Font, Brushes.Blue, new PointF(x1, y1 - 8));//气体开始点单位，用量备注
            ////        }
            ////        if (x2 - x1 > 5 && mz.Bz == 1)//未结束，画红色箭头
            ////        {
            ////            e.Graphics.DrawLine(pred2, new PointF(x2, y1 + 5), new PointF(x2 - 5, y1));
            ////            e.Graphics.DrawLine(pred2, new PointF(x2, y1 + 5), new PointF(x2 - 5, y1 + 10));
            ////        }
            ////        else if (x2 > 170 && x2 - x1 > 5 && mz.Bz == 2)//结束，画结束标志
            ////        {
            ////            e.Graphics.FillPolygon(Brushes.Black, new PointF[3] { new PointF(x2, y1), new PointF(x2 - 5, y1 + 8), new PointF(x2 + 5, y1 + 8) });
            ////            //double qtzongliang=(mz.Yl) *Convert.ToDouble( (x2 - x1)/3.27);
            ////            //e.Graphics.DrawString(Convert.ToInt32(qtzongliang).ToString() + "L", this.Font, Brushes.Blue, new PointF(960, y1 - 2));
            ////            //MessageBox.Show(mz.Dw.Split('/').ToString());
            ////        }
            ////        if (x2 - x1 > 5)
            ////        {
            ////            if (x1 > 170 && x2 > 170)//开始结束都在当前图像内，画开始结束之间红线
            ////            {
            ////                e.Graphics.DrawLine(pred2, new PointF(x1, y1 + 5), new PointF(x2, y1 + 5));
            ////            }
            ////            if (x1 < 170 && x2 > 170)//开始在起始点之前，画开始，结束之间红线
            ////            {
            ////                e.Graphics.DrawLine(pred2, new PointF(170, y1 + 5), new PointF(x2, y1 + 5));
            ////            }

            ////        }
            ////        dy++;
            ////        sssQT.Add(mz.Qtname);
            ////    }
            ////}
            //#endregion

            //#region //画气体
            //ArrayList sssQT = new ArrayList();
            //int dy = 0;// 控制画气体输出的Y坐标           
            //foreach (adims_MODEL.mzqt mz in mzqtList)
            //{
            //    if (mz.Bz > 0)
            //    {
            //        if (sssQT.Contains(mz.Qtname))
            //            dy = dy - 1;
            //        //DataTable dtM = bll.addYongyaoListzling(mzjldID, mz.Name);
            //        //string bb = dtM.Rows[0][0].ToString();
            //        //admin e.Graphics.DrawString(mz.Qtname + " " + mz.Dw.ToString(), this.Font, Brushes.Black, new Point(50, 15 * (dy + 0) + 2));
            //        TimeSpan t = new TimeSpan();
            //        TimeSpan t1 = new TimeSpan();
            //        if (mz.Bz == 1)
            //        {
            //            t = mz.Sysj - otime;
            //            if (mzjsTime > Convert.ToDateTime("1990-01-01"))
            //            {
            //                DataTable dtMax = bll.GetMaxPoint(mzjldID);
            //                DateTime dtnow;
            //                if (dtMax.Rows[0][0].ToString() != "")
            //                {
            //                    dtnow = Convert.ToDateTime(dtMax.Rows[0][0]);
            //                    if (dtnow > mzjsTime)
            //                    {
            //                        t1 = dtnow - otime;
            //                    }
            //                    else
            //                    {
            //                        t1 = mzjsTime - otime;
            //                    }
            //                }
            //                else
            //                {
            //                    t1 = mzjsTime - otime;
            //                }
            //            }
            //            else
            //            {
            //                t1 = DateTime.Now - otime;
            //            }
            //        }
            //        else if (mz.Bz == 2)
            //        {
            //            t = mz.Sysj - otime;
            //            t1 = mz.Jssj - otime;
            //        }
            //        int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 171);
            //        int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 171);
            //        int y1 = 15 * (dy) + 5;
            //        if (x1 > 170)
            //        {
            //            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
            //            e.Graphics.DrawString(mz.Yl.ToString() + mz.Dw.ToString(), this.Font, Brushes.Blue, new Point(x1, y1 - 8));
            //        }
            //        if (x2 - x1 > 5 && mz.Bz == 1)
            //        {
            //            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1));
            //            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1 + 10));
            //        }
            //        else if (x2 > 170 && x2 - x1 > 5 && mz.Bz == 2)
            //        {

            //            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 5, y1 + 8), new Point(x2 + 5, y1 + 8) });
            //            //double qtzongliang=(mz.Yl) *Convert.ToDouble( (x2 - x1)/3.27);
            //            //e.Graphics.DrawString(Convert.ToInt32(qtzongliang).ToString() + "L", this.Font, Brushes.Blue, new Point(960, y1 - 2));
            //            //MessageBox.Show(mz.Dw.Split('/').ToString());
            //        }
            //        if (x2 - x1 > 5)
            //        {
            //            if (x1 > 170 && x2 > 170)
            //            {
            //                e.Graphics.DrawLine(pred2, new Point(x1, y1 + 5), new Point(x2, y1 + 5));
            //            }
            //            if (x1 < 170 && x2 > 170)
            //            {
            //                e.Graphics.DrawLine(pred2, new Point(170, y1 + 5), new Point(x2, y1 + 5));
            //            }


            //        }
            //        dy++;
            //        //sssQT.Add(mz.Name);
            //    }
            //}
            //#endregion

            //#region //画诱导药
            //ArrayList sss = new ArrayList();
            //int dy1 = 1;// 控制画气体输出的Y坐标           
            //foreach (adims_MODEL.mzyt yt in ydyList)//画诱导药
            //{
            //    if (yt.Bz > 0)
            //    {
            //        if (sss.Contains(yt.Ytname))
            //        {
            //            dy1 = dy1 - 1;
            //        }
            //        e.Graphics.DrawString(yt.Ytname, this.Font, Brushes.Black, new Point(30, 15 * (dy1) + 2));
            //        TimeSpan t = new TimeSpan();
            //        TimeSpan t1 = new TimeSpan();
            //        if (yt.Bz == 1)
            //        {
            //            t = yt.Sysj - otime;
            //            t1 = DateTime.Now - otime;
            //        }
            //        else if (yt.Bz == 2)
            //        {
            //            t = yt.Sysj - otime;
            //            t1 = yt.Jssj - otime;
            //        }
            //        int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
            //        int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 170);
            //        int y1 = 15 * (dy1 + 0) + 5;//修改y轴的坐标的是加20
            //        if (x1 > 170)
            //        {
            //            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
            //            e.Graphics.DrawString(yt.Yl.ToString() + yt.Dw.ToString(), this.Font, Brushes.Blue, new Point(x1, y1 - 8));
            //        }
            //        if (x2 - x1 > 5 && yt.Cxyy == true)
            //        {
            //            if (yt.Bz == 1)
            //            {
            //                e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1));
            //                e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1 + 10));
            //            }
            //            if (yt.Bz == 2)
            //            {
            //                e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 5, y1 + 8), new Point(x2 + 5, y1 + 8) });
            //            }
            //            string str = (x2 - x1 + y1).ToString();
            //            str += (x2 - x1 + y1).ToString();
            //        }
            //        if (x2 - x1 > 5 && yt.Cxyy == true)
            //        {
            //            if (x1 > 170 && x2 > 170)
            //            {
            //                e.Graphics.DrawLine(pred2, new Point(x1, y1 + 5), new Point(x2, y1 + 5));
            //            }
            //            if (x1 < 170 && x2 > 170)
            //            {
            //                e.Graphics.DrawLine(pred2, new Point(170, y1 + 5), new Point(x2, y1 + 5));
            //            }
            //        }
            //        dy1++;
            //        sss.Add(yt.Ytname);

            //    }

            //}
            //#endregion

            //#region //画局麻药

            //int dy2 = 0;// 控制画局麻药输出的Y坐标
            //ArrayList sssJMY = new ArrayList();
            //foreach (adims_MODEL.jtytsx jt in jmyList)//画局麻药
            //{
            //    //if (jt.Bz > 0)
            //    //{
            //    //    if (sssJMY.Contains(jt.Name))
            //    //    {
            //    //        dy2 = dy2 - 1;
            //    //    }
            //    //    e.Graphics.DrawString(jt.Name, this.Font, Brushes.Black, new Point(37, 15 * (dy2) + 302));
            //    //    TimeSpan t = new TimeSpan();
            //    //    TimeSpan t1 = new TimeSpan();
            //    //    if (jt.Bz == 1)
            //    //    {
            //    //        t = jt.Kssj - otime;
            //    //        t1 = DateTime.Now - otime;
            //    //    }
            //    //    else if (jt.Bz == 2)
            //    //    {
            //    //        t = jt.Kssj - otime;
            //    //        t1 = jt.Jssj - otime;
            //    //    }
            //    //    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
            //    //    int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 170);
            //    //    int y1 = 15 * (dy2) + 305;
            //    //    if (x1 > 170)
            //    //    {
            //    //        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
            //    //        e.Graphics.DrawString(jt.Jl.ToString() + jt.Dw.ToString(), this.Font, Brushes.Blue, new Point(x1, y1 - 8));
            //    //        //e.Graphics.DrawString(jt.Jl.ToString() + jt.Dw.ToString(), this.Font, Brushes.Black, new Point(960, y1-2));
            //    //    }
            //    //    //if (x2 - x1 > 5 && jt.Bz == 1 && jt.Cxyy == true)
            //    //    //{
            //    //    //    e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1));
            //    //    //    e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1 + 10));
            //    //    //}
            //    //    //else if (x2 - x1 > 5 && jt.Bz == 2 && jt.Cxyy == true)
            //    //    //{
            //    //    //    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 5, y1 + 8), new Point(x2 + 5, y1 + 8) });
            //    //    //}
            //    //    //if (x2 - x1 > 5 && jt.Cxyy == true)
            //    //    //{
            //    //    //    e.Graphics.DrawLine(pred2, new Point(x1, y1 + 5), new Point(x2, y1 + 5));
            //    //    //}
            //    //    dy2++;
            //    //    sssJMY.Add(jt.Name);
            //    //}

            //}
            //#endregion

            //#region//画输液
            //ArrayList sssSY = new ArrayList();
            //int dy3 = 0;
            //foreach (adims_MODEL.shuye mz in shuyeList)  ////画输液
            //{
            //    if (sssSY.Contains(mz.Name))
            //        dy3 = dy3 - 1;
            //    if (shuyeList.Count > 0)
            //    {
            //        if (mz.Bz > 0)
            //        {
            //            e.Graphics.DrawString(mz.Name, this.Font, Brushes.Black, new Point(35, 15 * (dy3) + 197));
            //            TimeSpan t = new TimeSpan();
            //            TimeSpan t1 = new TimeSpan();
            //            if (mz.Bz == 1)
            //            {
            //                t = mz.Kssj - otime;
            //                t1 = DateTime.Now - otime;
            //            }
            //            else if (mz.Bz == 2)
            //            {
            //                t = mz.Kssj - otime;
            //                t1 = mz.Jssj - otime;
            //            }
            //            int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
            //            int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 170);
            //            int y1 = 15 * (dy3) + 200;
            //            if (x1 > 170)
            //            {
            //                e.Graphics.DrawString(mz.Jl.ToString() + mz.Dw.ToString(), this.Font, Brushes.Blue, new Point(x1 - 10, y1 - 8));
            //                e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
            //            }
            //            dy3++;
            //            sssSY.Add(mz.Name);
            //        }
            //    }
            //}
            //#endregion

            //#region//画输血
            //ArrayList sssSX = new ArrayList();
            //int dy4 = 0;
            //foreach (adims_MODEL.shuxue mz in shuxueList)  ////画输血
            //{
            //    if (sssSX.Contains(mz.Name))
            //        dy4 = dy4 - 1;
            //    if (shuxueList.Count > 0)
            //    {
            //        if (mz.Bz > 0)
            //        {
            //            e.Graphics.DrawString(mz.Name, this.Font, Brushes.Black, new Point(35, 15 * (dy4) + 272));
            //            TimeSpan t = new TimeSpan();
            //            TimeSpan t1 = new TimeSpan();
            //            if (mz.Bz == 1)
            //            {
            //                t = mz.Kssj - otime;
            //                t1 = DateTime.Now - otime;
            //            }
            //            else if (mz.Bz == 2)
            //            {
            //                t = mz.Kssj - otime;
            //                t1 = mz.Jssj - otime;
            //            }
            //            int y1 = 15 * (dy4) + 275;

            //            int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
            //            int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 170);
            //            if (x1 > 170)
            //            {
            //                e.Graphics.DrawString(mz.Jl.ToString() + mz.Dw.ToString(), this.Font, Brushes.Blue, new Point(x1 - 10, y1 - 8));
            //                e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
            //            }
            //            dy4++;
            //            sssSX.Add(mz.Name);
            //        }
            //    }
            //}
            //#endregion

            //#region  //画出血
            //int cxCOUNT = 1;
            //foreach (adims_MODEL.clcxqt cl in cxList)
            //{
            //    TimeSpan t = new TimeSpan();
            //    t = cl.D - otime;
            //    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 171);
            //    int y1 = 305;
            //    e.Graphics.FillRectangle(Brushes.Pink, x1, y1, 20, 12);
            //    e.Graphics.DrawString(cl.V.ToString(), this.Font, Brushes.Black, new Point(x1, y1));
            //    cxCOUNT++;
            //}

            //#endregion
            //#region  //画引流量
            //int clCOUNTc = 1;
            //foreach (adims_MODEL.clcxqt cl in yllList)
            //{
            //    TimeSpan t = new TimeSpan();
            //    t = cl.D - otime;
            //    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 171);
            //    int y1 = 335;
            //    e.Graphics.FillRectangle(Brushes.Pink, x1, y1, 20, 12);
            //    e.Graphics.DrawString(cl.V.ToString(), this.Font,
            //        Brushes.Black, new Point(x1, y1));
            //    clCOUNTc++;
            //}
            //#endregion
            #region  //画出尿
            int clCOUNT = 1;
            foreach (adims_MODEL.clcxqt cl in cnList)
            {
                TimeSpan t = new TimeSpan();
                t = cl.D - otime;
                int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 171);
                int y1 = 90;
                e.Graphics.FillRectangle(Brushes.Pink, x1, y1, 20, 12);
                e.Graphics.DrawString(cl.V.ToString(), this.Font,
                    Brushes.Black, new Point(x1, y1));
                clCOUNT++;
            }
            #endregion
            #endregion

            #region 画监护项目
            int i_jhxm = 0;
            foreach (string str in jhxmIn)
            {
                e.Graphics.DrawString(str, zt8, Brushes.Black, 27, i_jhxm * 15);
                i_jhxm++;
            }
            #endregion
            #region 画监护项的值
            for (int i = 0; i < jhxmIn.Count; i++)//画SPO2
            {
                //jhxmValue
                foreach (adims_MODEL.jhxm jt in jhxmValue)
                {
                    if (jt.Sy == jhxmIn[i])
                    {
                        if (jhxmIn[i] == "ECG")
                        {
                            TimeSpan t = new TimeSpan();
                            t = jt.D - otime;
                            TimeSpan t1 = new TimeSpan();
                            t1 = cgTime - otime;
                            int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 171);
                            int y =  i * 15;
                            int x1 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 170);
                            string ECG = "";
                            if (jt.V.ToString() == "0")
                            {
                                ECG = "SR";
                            }
                            else
                            {
                                ECG = jt.V.ToString();
                            }
                            if (x > 0 & jt.V >= 0)
                            {
                                e.Graphics.FillRectangle(Brushes.Pink, x, y, 14, 12);
                                e.Graphics.DrawString(ECG, (jt.V / 100 > 0 ? zt7 : this.Font), Brushes.Black,
                                new Point((jt.V / 100 > 0 ? (x - 2) : x), y));
                            }
                            //JHXcvp = 1;
                            //TimeSpan t = new TimeSpan();
                            //t = jt.D - otime;
                            //TimeSpan t1 = new TimeSpan();
                            //t1 = cgTime - otime;
                            //int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 171);
                            //int y = 2 + i * 15;
                            //int x1 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 170);
                            //DataTable DTCVP = bll.selectmzjldplcvp(mzjldID, jt.D);
                            //string ECG = DTCVP.Rows[0][0].ToString();
                            //if (ECG == "0")
                            //{
                            //    ECG = "SR";
                            //}
                            //if (x > 170)
                            //{
                            //    // e.Graphics.FillRectangle(Brushes.Pink, x, y, 14, 12);
                            //    e.Graphics.DrawString(ECG, (jt.V / 100 > 0 ? zt7 : this.Font), Brushes.Black,
                            //    new Point((jt.V / 100 > 0 ? (x - 2) : x), y));
                            //}
                        }
                        else if (jhxmIn[i] == "SP02")
                        {
                            TimeSpan t = new TimeSpan();
                            t = jt.D - otime;
                            TimeSpan t1 = new TimeSpan();
                            t1 = cgTime - otime;
                            int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 171);
                            int y =  i * 15;
                            int x1 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 170);
                            if (x >0 && jt.V > 0)
                            {
                                e.Graphics.FillRectangle(Brushes.Pink, x, y, 14, 12);
                                e.Graphics.DrawString(jt.V.ToString(), (jt.V / 100 > 0 ? zt7 : this.Font), Brushes.Black,
                                new Point((jt.V / 100 > 0 ? (x - 2) : x), y));
                            }
                         }
                        else if (jhxmIn[i] == "BIS")
                        {
                            TimeSpan t = new TimeSpan();
                            t = jt.D - otime;
                            TimeSpan t1 = new TimeSpan();
                            t1 = cgTime - otime;
                            int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 171);
                            int y =  i * 15;
                            int x1 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 170);
                            if (x > 0 && jt.V > 0)
                            {
                                e.Graphics.FillRectangle(Brushes.Pink, x, y, 14, 12);
                                e.Graphics.DrawString(jt.V.ToString(), (jt.V / 100 > 0 ? zt7 : this.Font), Brushes.Black,
                                new Point((jt.V / 100 > 0 ? (x - 2) : x), y));
                            }
                         }
                        else if (jhxmIn[i] == "ETCO2")
                        {
                            if (jt.D > cgTime)
                            {
                                TimeSpan t = new TimeSpan();
                                t = jt.D - otime;
                                TimeSpan t1 = new TimeSpan();
                                t1 = cgTime - otime;
                                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 171);
                                int y =  i * 15;
                                int x1 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 170);
                                if (x > 0 && jt.V > 0)
                                {
                                    e.Graphics.FillRectangle(Brushes.Pink, x, y, 14, 12);
                                    e.Graphics.DrawString(jt.V.ToString(), (jt.V / 100 > 0 ? zt7 : this.Font), Brushes.Black,
                                    new Point((jt.V / 100 > 0 ? (x - 2) : x), y));
                                }
                            }
                        }
                        //else if (jhxmIn[i] == "体温")
                        //{
                        //    TimeSpan t = new TimeSpan();
                        //    t = jt.D - otime;
                        //    TimeSpan t1 = new TimeSpan();
                        //    t1 = cgTime - otime;
                        //    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 171);
                        //    int y = 350 + i * 15;
                        //    int x1 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 170);
                        //    if (x > 170 && jt.V > 0)
                        //    {
                        //        e.Graphics.FillRectangle(Brushes.Pink, x, y, 14, 12);
                        //        e.Graphics.DrawString(jt.V.ToString(), (jt.V / 100 > 0 ? zt7 : this.Font), Brushes.Black,
                        //        new Point((jt.V / 100 > 0 ? (x - 2) : x), y));
                        //    }
                        //}
                        else if (jhxmIn[i] == "CVP")
                        {
                            TimeSpan t = new TimeSpan();
                            t = jt.D - otime;
                            TimeSpan t1 = new TimeSpan();
                            t1 = cgTime - otime;
                            int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 171);
                            int y =  i * 15;
                            int x1 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 170);
                            if (x > 0 && jt.V > 0)
                            {
                                e.Graphics.FillRectangle(Brushes.Pink, x, y, 14, 12);
                                e.Graphics.DrawString(jt.V.ToString(), (jt.V / 100 > 0 ? zt7 : this.Font), Brushes.Black,
                                new Point((jt.V / 100 > 0 ? (x - 2) : x), y));
                            }
                        }
                        else
                        {
                            TimeSpan t = new TimeSpan();
                            t = jt.D - otime;
                            TimeSpan t1 = new TimeSpan();
                            t1 = cgTime - otime;
                            int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 171);
                            int y = i * 15;
                            int x1 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 170);
                            if (x > 0 && jt.V > 0)
                            {
                                e.Graphics.FillRectangle(Brushes.Pink, x, y, 14, 12);
                                e.Graphics.DrawString(jt.V.ToString(), (jt.V / 100 > 0 ? zt7 : this.Font), Brushes.Black,
                                new Point((jt.V / 100 > 0 ? (x - 2) : x), y));
                            }
                        }
                      
                    }

                  
                    else if (jhxmIn[i] == "深度值")
                    {
                        JHXsdz = 1;
                    }
                    else if (jhxmIn[i] == "肌松值")
                    {
                        JHXjsz = 1;
                    }

                }
            }
            #endregion
            #region //画SPO2
            //foreach (adims_MODEL.jhxm jt in jhxmValue)
            //{
            //    if (jt.Sy == "SpO2")
            //    {
            //        TimeSpan t = new TimeSpan();
            //        t = jt.D - otime;
            //        TimeSpan t1 = new TimeSpan();
            //        t1 = cgTime - otime;
            //        int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 163);
            //        int y = 512;
            //        int x1 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 163);
            //        if (x > 170)
            //        {
            //            e.Graphics.FillRectangle(Brushes.Pink, x, y, 14, 12);
            //            e.Graphics.DrawString(jt.V.ToString(), (jt.V / 100 > 0 ? zt7 : this.Font), Brushes.Black,
            //            new Point((jt.V / 100 > 0 ? (x - 2) : x), y));
            //        }
            //    }
            //}
            #endregion

        }

        private void pictureBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.X > 0 && e.X < 169)
            {

                if (e.Y > 0 && e.Y < 90)//监护项目
                {
                    addJhxm f1 = new addJhxm(jhxmAll, jhxmIn, mzjldID, 0);
                    f1.ShowDialog();
                    pictureBox2.Refresh();
                }
                //if(e.Y>0 && e.Y < 15 )//肖写的气体在第一行的氧气.
                //{
                //    addQty qt = new addQty(mzjldID);
                //    qt.ShowDialog();
                //    BindQtList();
                // }
                //if (e.Y > 15 && e.Y < 195)//用药的事件
                //{
                //    addYdyao mzytform = new addYdyao(mzjldID);
                //    mzytform.ShowDialog();
                //    BindYdyList();
                //}

                //else if (e.Y > 300 && e.Y < 330)
                //{
                //    //addJuMaYao szyy = new addJuMaYao(mzjldID);
                //    //szyy.ShowDialog();
                //    //BindJmyList();
                //}
                //else if (e.Y > 195 && e.Y < 275)
                //{
                //    addShuye szyy = new addShuye(mzjldID);
                //    szyy.ShowDialog();
                //    BindShuyeList();
                //}
                //else if (e.Y > 275 && e.Y < 305)
                //{
                //    addShuxue szyy = new addShuxue(mzjldID);
                //    szyy.ShowDialog();
                //    BindShuxueList();
                //}
                //if (e.Y > 350 && e.Y < 400)
                //{
                //    addJhxm f1 = new addJhxm(jhxmAll, jhxmIn, mzjldID, 0);
                //    f1.ShowDialog();
                //    pictureBox2.Refresh();
                //}
                //else if (e.Y > 525 && e.Y < 540)//气体点击事件,别人写的气体点击事件
                //{
                //    //addQty qt = new addQty(mzjldID);
                //    //qt.ShowDialog();
                //    //BindQtList();
                //}
              

            }
            //if (e.X > 170 && e.Y > 305 && e.Y <350)
            //{
            //     if (e.Y > 305 && e.Y < 320)//出量
            //    {
            //        MZcl formaddcl = new MZcl(otime.AddMinutes((e.X - 165) / 15 * jcsjjg), 1, mzjldID);
            //        formaddcl.ShowDialog();
            //        BindCxList();
            //    }
            else if (e.X > 170 && e.Y > 90 && e.Y <105)
            {
                MZcl formaddcl = new MZcl(otime.AddMinutes((e.X - 165) / 15 * jcsjjg), 2, mzjldID);
                formaddcl.ShowDialog();
                BindClList();
            }
            //    else if (e.Y > 335 && e.Y < 350)
            //    {
            //        MZcl formaddcl = new MZcl(otime.AddMinutes((e.X - 165) / 15 * jcsjjg), 3, mzjldID);
            //        formaddcl.ShowDialog();
            //        BindYllList();
                   
            //    }
            //}
          
            if (e.X > 170 && e.Y > 510 && e.Y < 525)
            {
                foreach (adims_MODEL.jhxm jt in jhxmValue)
                {
                    if (jt.Sy == "SpO2")
                    {
                        TimeSpan t = new TimeSpan();
                        t = jt.D - otime;
                        int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 163);
                        int y = 512;
                        if (e.X > x && e.X < x + 13 && e.Y > y + 1 && e.Y < y + 13)
                        {
                            UpdateJHXM formxgjhsj = new UpdateJHXM(mzjldID, jt, 0);
                            formxgjhsj.ShowDialog();
                            break;
                        }
                    }
                }
            }
            pictureBox2.Refresh();
            btnLeft.Focus();
        }


        private void BindCxList()
        {
            DataTable dtCL = bll.Getcl_mzd_cl(mzjldID, 1);
            if (dtCL.Rows.Count > 0) // ↓出血赋值
            {
                int i = 0;
                cxList.Clear();
                foreach (DataRow dr in dtCL.Rows)
                {
                    adims_MODEL.clcxqt CLCXQT = new adims_MODEL.clcxqt();
                    cxList.Add(CLCXQT);
                    cxList[i].Id = Convert.ToInt32(dtCL.Rows[i]["id"]);
                    cxList[i].D = Convert.ToDateTime(dtCL.Rows[i][4]);
                    cxList[i].V = Convert.ToInt32(dtCL.Rows[i][3]);
                    i++;
                }
            }
        }
        private void BindClList()
        {
            DataTable dtCL = bll.Getcl_mzd_cl(mzjldID, 2);
            if (dtCL.Rows.Count > 0) // ↓出尿赋值
            {
                int i = 0;
                cnList.Clear();
                foreach (DataRow dr in dtCL.Rows)
                {
                    adims_MODEL.clcxqt CLCXQT = new adims_MODEL.clcxqt();
                    cnList.Add(CLCXQT);
                    cnList[i].Id = Convert.ToInt32(dtCL.Rows[i]["id"]);
                    cnList[i].D = Convert.ToDateTime(dtCL.Rows[i][4]);
                    cnList[i].V = Convert.ToInt32(dtCL.Rows[i][3]);
                    i++;
                }
            }
        }
        private void BindYllList()
        {
            DataTable dtCL = bll.Getcl_mzd_cl(mzjldID, 3);
            if (dtCL.Rows.Count > 0) // ↓引流量赋值
            {
                int i = 0;
                yllList.Clear();
                foreach (DataRow dr in dtCL.Rows)
                {
                    adims_MODEL.clcxqt CLCXQT = new adims_MODEL.clcxqt();
                    yllList.Add(CLCXQT);
                    yllList[i].Id = Convert.ToInt32(dtCL.Rows[i]["id"]);
                    yllList[i].D = Convert.ToDateTime(dtCL.Rows[i][4]);
                    yllList[i].V = Convert.ToInt32(dtCL.Rows[i][3]);
                    i++;
                }
            }
        }
        int CLid;
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            //if (!IsInNet())
            //{
            //    return;
            //}
            //int dy = 0;//气体药起始行
            //ArrayList sssQT = new ArrayList();//气体药使用种类集合
            //foreach (adims_MODEL.mzqt mz in mzqtList)//遍历绑定好的气体药集合
            //{
            //    if (mz.Bz > 0)
            //    {
            //        if (sssQT.Contains(mz.Qtname))//如果已经包含这种气体药
            //            dy = dy - 1;//画图位置减一行
            //        TimeSpan t = new TimeSpan();//气体开始时间和起始位置间隔
            //        t = mz.Sysj - otime;
            //        TimeSpan t2 = new TimeSpan();
            //        t2 = mz.Jssj - otime;
            //        float x = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);//气体开始时间所在横坐标
            //        float y = 15 * (dy) + 5;//肖写的拖动事件
            //        //float y = 15 * (dy) + 530;//别人写的拖动事件
            //        float x2 = (float)((t2.Days * 24 * 60 + t2.Hours * 60 + t2.Minutes) * 15 / jcsjjg + 170);//气体结束时间所在横坐标
            //        float y2 = 15 * (dy) + 5;//肖写的拖动事件
            //        //float y2 = 15 * (dy) + 530;//别人写的拖动事件
            //        if (p2x > x - 5 && p2x < x + 5 && p2y > y - 1 && p2y < y + 8)//如果选中气体开始标志
            //        { flagP2 = 1; typeP2 = 1; t_mzqt = mz; }//flagP2为1表示标志可移动，typeP2为1指气体开始，t_mzqt赋给遍历到的气体模型
            //        if (p2x > x2 - 5 && p2x < x2 + 5 && p2y > y2 - 1 && p2y < y2 + 8)
            //        { flagP2 = 1; typeP2 = 11; t_mzqt = mz; }//flagP2为1表示标志可移动，typeP2为11指气体结束，t_mzqt赋给遍历到的气体模型
                    
            //        dy++;
            //        sssQT.Add(mz.Qtname);//气体种类集合添加此气体
            //    }

            //}
            //int dy1 = 1;
            //ArrayList sss = new ArrayList();
            //foreach (adims_MODEL.mzyt yt in ydyList)
            //{
            //    if (yt.Bz > 0)
            //    {
            //        if (dy1 > 0 && sss.Contains(yt.Ytname))
            //        {
            //            dy1 = dy1 - 1;
            //        }
            //        TimeSpan t = new TimeSpan();
            //        t = yt.Sysj - otime;
            //        int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
            //        int y = 15 * (dy1) + 5;
            //        TimeSpan t2 = new TimeSpan();
            //        t2 = yt.Jssj - otime;
            //        int x2 = (int)((t2.Days * 24 * 60 + t2.Hours * 60 + t2.Minutes) * 15 / jcsjjg + 170);
            //        int y2 = 15 * (dy1) + 5;
            //        // MessageBox.Show(x.ToString() + "    " + y.ToString() + "   " + p2x.ToString() + "    " + p2y.ToString());
            //        if (p2x > x - 5 && p2x < x + 5 && p2y > y - 1 && p2y < y + 8)
            //        { flagP2 = 1; typeP2 = 2; t_ydy = yt; }
            //        if (p2x > x2 - 5 && p2x < x2 + 5 && p2y > y2 - 1 && p2y < y2 + 8)
            //        { flagP2 = 1; typeP2 = 22; t_ydy = yt; }
            //        dy1++;
            //        sss.Add(yt.Ytname);
            //    }
            //}
            //int dy2 = 0;
            //ArrayList sssJMY = new ArrayList();
            //foreach (adims_MODEL.jtytsx jt in jmyList)
            //{
            //    //if (jt.Bz > 0)
            //    //{
            //    //    if (sssJMY.Contains(jt.Name))
            //    //        dy2 = dy2 - 1;
            //    //    TimeSpan t = new TimeSpan();
            //    //    t = jt.Kssj - otime;
            //    //    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
            //    //    int y = 15 * (dy2) + 305;
            //    //    if (p2x > x - 5 && p2x < x + 5 && p2y > y - 1 && p2y < y + 8)
            //    //    { flagP2 = 1; typeP2 = 3; t_jmy = jt; }
            //    //    dy2++;
            //    //    sssJMY.Add(jt.Name);
            //    //}
            //}
            //int dy3 = 0;
            //ArrayList sssSY = new ArrayList();
            //foreach (adims_MODEL.shuye mz in shuyeList)
            //{
            //    if (mz.Bz > 0)
            //    {
            //        if (sssSY.Contains(mz.Name))
            //            dy3 = dy3 - 1;
            //        TimeSpan t = new TimeSpan();
            //        t = mz.Kssj - otime;
            //        int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
            //        int y = 15 * (dy3) + 200;
            //        if (p2x > x - 5 && p2x < x + 5 && p2y > y - 1 && p2y < y + 8)
            //        { flagP2 = 1; typeP2 = 4; t_shuye = mz; }
            //        dy3++;
            //        sssSY.Add(mz.Name);
            //    }
            //}
            //int dy4 = 0;
            //ArrayList sssSX = new ArrayList();
            //foreach (adims_MODEL.shuxue yt in shuxueList)
            //{
            //    if (yt.Bz > 0)
            //    {
            //        if (sssSX.Contains(yt.Name))
            //            dy4 = dy4 - 1;
            //        TimeSpan t = new TimeSpan();
            //        t = yt.Kssj - otime;
            //        int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
            //        int y = 15 * (dy4) + 275;
            //        if (p2x > x - 5 && p2x < x + 5 && p2y > y - 1 && p2y < y + 8)
            //        { flagP2 = 1; typeP2 = 5; t_shuxue = yt; }
            //        dy4++;
            //        sssSX.Add(yt.Name);
            //    }
            //}
            //foreach (adims_MODEL.clcxqt q in cxList)  // 是否选中失血
            //{
            //    TimeSpan t = new TimeSpan();
            //    t = q.D - otime;
            //    CLid = q.Id;
            //    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
            //    int y = 305;

            //    if (p2x > x - 5 && p2x < x + 5 && p2y > y - 1 && p2y < y + 8)
            //    { flagP2 = 1; typeP2 = 6; t_shixue = q; }

            //}
            //foreach (adims_MODEL.clcxqt q in yllList)  // 是否选中引流量图像
            //{
            //    TimeSpan t = new TimeSpan();
            //    t = q.D - otime;
            //    CLid = q.Id;
            //    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
            //    int y = 335;
            //    if (e.X > x - 5 && e.X < x + 12 && e.Y < y + 12 && e.Y > y)
            //    { flagP2 = 1; typeP2 = 7; t_yll = q; }

            //}
            foreach (adims_MODEL.clcxqt q in cnList)  // 是否选中尿量图像
            {
                TimeSpan t = new TimeSpan();
                t = q.D - otime;
                CLid = q.Id;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                int y = 90;
                if (e.X > x - 5 && e.X < x + 12 && e.Y < y + 12 && e.Y > y)
                { flagP2 = 1; typeP2 = 8; t_niaoliang = q; }

            }

        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            PointF pttag = new PointF(MousePosition.X, MousePosition.Y);
            Graphics g = this.CreateGraphics();
            p2x = e.X; p2y = e.Y;
            if (MousePosition.Y > e.Y)
            {
                g.Clear(this.BackColor);
            }
            if (flagP2 == 1)//可移动标志为1
            {
            //    if (typeP2 == 1)//类型1表示气体开始
            //    {
            //        t_mzqt.Sysj = otime.AddMinutes((p2x - 170) / 15 * jcsjjg);
            //    }
            //    if (typeP2 == 11)//类型11表示气体结束
            //    {
            //        t_mzqt.Jssj = otime.AddMinutes((p2x - 170) / 15 * jcsjjg);
            //    }
            //    if (typeP2 == 2)//类型2表示诱导药开始标志移动
            //    {
            //        t_ydy.Sysj = otime.AddMinutes((p2x - 170) / 15 * jcsjjg);
            //    }
            //    if (typeP2 == 22)//类型22表示诱导药结束标志移动
            //    {
            //        t_ydy.Jssj = otime.AddMinutes((p2x - 170) / 15 * jcsjjg);
            //    }
            //    if (typeP2 == 3)//类型2表示局麻药开始标志移动
            //    {
            //      //  t_jmy.Kssj = otime.AddMinutes((p2x - 170) / 15 * jcsjjg);
            //    }
            //    if (typeP2 == 4)//输液移动标志移动
            //    {
            //        t_shuye.Kssj = otime.AddMinutes((p2x - 170) / 15 * jcsjjg);
            //    }
            //    if (typeP2 == 5)//输血移动
            //    {
            //        t_shuxue.Kssj = otime.AddMinutes((p2x - 170) / 15 * jcsjjg);
            //    }
            //    if (typeP2 == 6)
            //    {
            //        if ((otime.AddMinutes((p2x - 170) / 15 * jcsjjg)) > dtOtime.Value)
            //        {
            //            t_shixue.D = otime.AddMinutes((p2x - 170) / 15 * jcsjjg);
            //        }

            //        else
            //            t_shixue.D = dtOtime.Value;

            //    }
            //    if (typeP2 == 7)
            //    {
            //        if ((otime.AddMinutes((p2x - 170) / 15 * jcsjjg)) > dtOtime.Value)
            //        {
            //            t_yll.D = otime.AddMinutes((p2x - 170) / 15 * jcsjjg);
            //        }

            //        else
            //            t_yll.D = dtOtime.Value;

            //    }
                if (typeP2 == 8)
                {
                    if ((otime.AddMinutes((p2x - 170) / 15 * jcsjjg)) > dtOtime.Value)
                    {
                        t_niaoliang.D = otime.AddMinutes((p2x - 170) / 15 * jcsjjg);
                    }

                    else
                        t_niaoliang.D = dtOtime.Value;
                }
                pictureBox2.Refresh();
            }
            pictureBox2.Refresh();
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            if (flagP2 != 0)
            {
            //    flagP2 = 0;//移动结束，标志改为0
            //    if (typeP2 == 1)//修改数据库气体开始时间
            //    {
            //        bll.xgqtKssj(mzjldID, t_mzqt);
            //    }
            //    if (typeP2 == 11)//修改数据库气体结束时间
            //    {
            //        bll.xgqtJssj(mzjldID, t_mzqt);
            //    }
            //    if (typeP2 == 2)//修改数据库诱导药时间
            //    {
            //        if (!t_ydy.Cxyy)//非持续，修改开始和结束时间
            //            bll.xgytKssjJssj(mzjldID, t_ydy);
            //        else  //持续，修改开始时间
            //            bll.xgytKssj(mzjldID, t_ydy);
            //    }
            //    if (typeP2 == 22)//修改数据库诱导药结束时间
            //    {
            //        bll.xgytJssj(mzjldID, t_ydy);
            //    }
            //    if (typeP2 == 3)//修改数据库局麻药时间
            //    {
            //       // bll.xgjt(mzjldID, t_jmy);
            //    }
            //    if (typeP2 == 4)//修改数据库局输液时间
            //    {
            //        int i = bll.xgshuyeKSSJ(mzjldID, t_shuye);
            //    }
            //    if (typeP2 == 5)//修改数据库输血时间
            //    {
            //        int i = bll.xgshuxueKSSJ(mzjldID, t_shuxue);
            //    }
            //    if (typeP2 == 6)
            //    {
            //        bll.xgclsj(mzjldID, t_shixue);
            //    }
            //    if (typeP2 == 7)
            //    {
            //        bll.xgclsj(mzjldID, t_yll);
            //    }
                if (typeP2 == 8)
                {
                    bll.xgclsj(mzjldID, t_niaoliang);
                }
            }
        }
        // 画机械呼吸
        private void PaintSignMBreath2(Graphics g, PointF v_p)
        {
            v_p.Y += 3;
            Pen pen = new Pen(Brushes.DarkCyan);
            g.DrawLines(pen, new PointF[] {
                new PointF(v_p.X - 6, v_p.Y + 8),
                new PointF(v_p.X - 3, v_p.Y),
                new PointF(v_p.X + 0, v_p.Y + 8),
                new PointF(v_p.X + 3, v_p.Y),
                new PointF(v_p.X + 6, v_p.Y + 8),

            });
        }

        private void PaintSignMBreath(Graphics g, PointF v_p)
        {
            v_p.Y += 3;
            Pen pen = new Pen(Brushes.DarkCyan);
            g.DrawLines(pen, new PointF[] {
                new PointF(v_p.X - 8, v_p.Y + 8),
                new PointF(v_p.X - 4, v_p.Y),
                new PointF(v_p.X + 0, v_p.Y + 8),
                new PointF(v_p.X + 4, v_p.Y),
                new PointF(v_p.X + 8, v_p.Y + 8),
            });
        }
        private String toStr(object o)
        {
            if (o == null) return "";
            if (o == DBNull.Value) return "";

            return o.ToString(); ;
        }
        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            int dyd = 0, dyd1 = 0, dyd2 = 0, dyd3 = 0, dyd4 = 0, dyd5 = 0;//标志是否是第一个点
            Pen pxuxian = new Pen(Brushes.Black);
            pxuxian.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Pen pblack2 = new Pen(Brushes.Black);
            pblack2.Width = 2;
            Font ptzt8 = new Font("宋体", 8);
            Font ptzt7 = new Font("宋体", 7);
            for (int i = 0; i < 10; i++)      //画格子
            {
                e.Graphics.DrawLine(Pens.Black, new Point(i * 90 + 169, 6), new Point(i * 90 + 169, 368));
                for (int j = 1; j < 6; j++)
                {
                    e.Graphics.DrawLine(pxuxian, new PointF((float)(j * 15 + i * 90 + 169), (float)8), new PointF((float)(j * 15 + i * 90 + 169), (float)368));
                }
            }
            e.Graphics.DrawLine(Pens.Black, new Point(1, 8), new Point(1069, 8));
            e.Graphics.DrawLine(Pens.Black, new Point(1, 368), new Point(1069, 368));
            for (int i = 0; i < 11; i++)
            {
                e.Graphics.DrawLine(pxuxian, new Point(169, i * 30 + 23), new Point(1069, i * 30 + 23));
                e.Graphics.DrawLine(Pens.Black, new Point(169, i * 30 + 38), new Point(1069, i * 30 + 38));
            }
            e.Graphics.DrawLine(pxuxian, new Point(169, 353), new Point(1069, 353));
            e.Graphics.DrawLine(Pens.Black, new Point(1, 390), new Point(1069, 390));
            e.Graphics.DrawLine(Pens.Black, new Point(1, 420), new Point(1069, 420));
            e.Graphics.DrawLine(pblack2, new Point(1, 0), new Point(1, 420));
            e.Graphics.DrawLine(pblack2, new Point(1069, 0), new Point(1069, 420)); //画格子


            #region//画监测点
            Pen red2 = new Pen(Brushes.Red, 2);
            if (checkssy.Checked == false)
            {
                foreach (adims_MODEL.point tp in ssyList)//画收缩压
                {
                    int x, y;
                    TimeSpan t = new TimeSpan();
                    t = tp.D - otime;
                    x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                    if (tp.V > 230)
                    {
                        y = 368 - 345;
                        e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Red, x, y);
                    }
                    else
                        y = (int)(368 - (int)(tp.V * 1.5));
                    if (MousePosition.X == x && MousePosition.Y == y)
                    {
                        e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Red, x, y);
                    }
                    if (x > 169)
                    {
                        //e.Graphics.DrawPolygon(Pens.Red, new Point[3] { new Point(x, y), new Point(x - 5, y - 5), new Point(x + 5, y - 5) });
                        e.Graphics.DrawLines(red2, new Point[3] { new Point(x - 4, y - 6), new Point(x, y), new Point(x + 4, y - 6) });

                        if (dyd != 0 && lastpoint.X > 169)
                            e.Graphics.DrawLine(Pens.Red, new Point(x, y), lastpoint);
                    }
                    lastpoint.X = x;
                    lastpoint.Y = y;
                    dyd++;
                }
            }
            if (checkszy.Checked == false)
            {
                foreach (adims_MODEL.point tp in szyList)//画舒张压
                {
                    int x, y;
                    TimeSpan t = new TimeSpan();
                    t = tp.D - otime;
                    x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                    if (tp.V > 230)
                    {
                        y = 368 - 345;
                        e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Red, x, y);
                    }
                    else
                        y = (int)(368 - (int)(tp.V * 1.5));
                    if (x > 169)
                    {
                        e.Graphics.DrawLines(red2, new Point[3] { new Point(x - 4, y + 6), new Point(x, y), new Point(x + 4, y + 6) });
                        if (dyd1 != 0 && lastpoint1.X > 169)
                            e.Graphics.DrawLine(Pens.Red, new Point(x, y), lastpoint1);
                    }
                    lastpoint1.X = x;
                    lastpoint1.Y = y;
                    dyd1++;
                }
            }
            if (checktw.Checked == false)
            {
                foreach (adims_MODEL.tw_point tp in twList)//画体温
                {
                    float x, y;
                    TimeSpan t = new TimeSpan();
                    t = tp.D - otime;
                    x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                    if (tp.V > 230)
                    {
                        y = 368 - 345;
                        e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Red, x, y);
                    }
                    else
                        y = (int)(368 - (int)(tp.V * 1.5));
                    if (x > 169)
                    {
                       e.Graphics.DrawPolygon(Pens.Maroon, new PointF[3] { new PointF(x, y), new PointF(x + 4, y + 6), new PointF(x - 4, y + 6) });
                       // e.Graphics.DrawPolygon(Pens.Maroon, new Point[3] { new Point(x, y), new Point(x + 4, y + 6), new Point(x - 4, y + 6) });
                        if (dyd2 != 0 && lastpoint2.X > 169)
                            e.Graphics.DrawLine(Pens.Maroon, new PointF(x, y), lastpoint2);
                    }
                    lastpoint2.X =(int) x;
                    lastpoint2.Y =(int) y;
                    dyd2++;
                }

            }
            if (checkmb.Checked == false)
            {
                foreach (adims_MODEL.point tp in mboList)//画脉搏
                {
                    int x, y;
                    TimeSpan t = new TimeSpan();
                    t = tp.D - otime;
                    x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                    if (tp.V > 230)
                    {
                        y = 368 - 345;
                        e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Red, x, y);
                    }
                    else
                        y = (int)(368 - (int)(tp.V * 1.5));
                    if (x > 169)
                    {
                        e.Graphics.FillEllipse(Brushes.Blue, new Rectangle(x - 4, y - 4, 8, 8));
                        if (dyd3 != 0 && lastpoint3.X > 169)
                            e.Graphics.DrawLine(Pens.Blue, new Point(x, y), lastpoint3);
                    }
                    lastpoint3.X = x + 4;
                    lastpoint3.Y = y;
                    dyd3++;
                }
            }
            int iJikong = 0;
            string fxs="", tvxs="";
            DataTable dtMZ_Info = bll.SelectOldPatInfo(mzjldID);

            if (dtMZ_Info.Rows.Count > 0)
            {
                if (this.toStr(dtMZ_Info.Rows[0]["jkvalue"]) != "")
                {
                    fxs = dtMZ_Info.Rows[0]["jkvalue"].ToString();
                }
                if (this.toStr(dtMZ_Info.Rows[0]["fzvalue"]) != "")
                {
                    tvxs = dtMZ_Info.Rows[0]["fzvalue"].ToString();
                }
            }
            if (checkhx.Checked == false)
            {
                foreach (adims_MODEL.point tp in hxlList)//画呼吸
                {
                    int x, y;
                    int x1 = 0; int x2 = 0;
                    TimeSpan t = new TimeSpan();
                    t = tp.D - otime;
                    x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                    if (tp.V > 230)
                    {
                        y = 368 - 345;
                        //e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Red, x, y);
                    }
                    else
                        y = (int)(368 - (int)(tp.V * 1.5));

                    if (x > 169)
                    {
                        if (jkksTime < tp.D && jkjsTime > tp.D)//机械呼吸
                        //if (rrc!="")//机械呼吸
                        {
                            // 
                            //int xPlus = x - lastpoint4.X; // 描点作图
                            //Point p1 = lastpoint4;
                            //Point p2 = new Point(x - 2 * xPlus / 3, y + 7);
                            //Point p3 = new Point(x - xPlus / 3, y - 7);
                            //Point p4 = new Point(x, y);
                            //e.Graphics.DrawBezier(Pens.DarkCyan, p1, p2, p3, p4);
                            this.PaintSignMBreath(e.Graphics, new Point(x - 3, y - 5));
                            if (iJikong == 0)
                            {

                                e.Graphics.DrawString("TV" + tvxs + "/ " + "f" + fxs, this.Font, Brushes.DarkCyan, x - 3, y - 15);
                                iJikong++;
                            }

                        }
                        else if (jkksTime < tp.D && jkjsTime < jkksTime)//机械呼吸
                        //if (rrc!="")//机械呼吸
                        {
                            // 
                            //int xPlus = x - lastpoint4.X; // 描点作图
                            //Point p1 = lastpoint4;
                            //Point p2 = new Point(x - 2 * xPlus / 3, y + 7);
                            //Point p3 = new Point(x - xPlus / 3, y - 7);
                            //Point p4 = new Point(x, y);
                            //e.Graphics.DrawBezier(Pens.DarkCyan, p1, p2, p3, p4);
                            //e.Graphics.DrawString("M", this.Font, Brushes.DarkCyan, x - 3, y - 5);
                            this.PaintSignMBreath(e.Graphics, new Point(x - 3, y - 5));
                            if (iJikong == 0)
                            {
                                e.Graphics.DrawString("TV" + tv + "f" + rrc, this.Font, Brushes.DarkCyan, x - 3, y - 15);
                                iJikong++;
                            }
                        }
                        //else if (fzksTime < tp.D && fzjsTime > tp.D)
                        //{
                        //    e.Graphics.DrawString("A", this.Font, Brushes.DarkCyan, x - 3, y - 5);
                        //}
                        else
                            e.Graphics.DrawEllipse(Pens.DarkCyan, new Rectangle(x - 3, y - 3, 6, 6));
                        //if ((dyd4 != 0 && lastpoint4.X > 169) && (jkksTime > tp.D || jkjsTime < tp.D))


                        if ((dyd4 != 0 && lastpoint4.X > 169)) //画呼吸节点之间的连线
                            // added by csb
                            if (!(jkksTime < tp.D && jkjsTime > tp.D))
                                e.Graphics.DrawLine(Pens.DarkCyan, new Point(x, y), lastpoint4);
                    }
                    lastpoint4.X = x;
                    lastpoint4.Y = y;
                    dyd4++;
                }
            }
            //foreach (adims_MODEL.point tp in hxlList)//画呼吸
            //{
            //    int x, y;
            //    int x1 = 0; int x2 = 0;
            //    TimeSpan t = new TimeSpan();
            //    t = tp.D - otime;
            //    x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
            //    if (tp.V > 230)
            //    {
            //        y = 368 - 345;
            //        e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Red, x, y);
            //    }
            //    else
            //        y = (int)(368 - (int)(tp.V * 1.5));

            //    if (x > 169 )
            //    {
            //        if (!CGUAN)
            //        {
            //            e.Graphics.DrawEllipse(Pens.DarkCyan, new Rectangle(x - 3, y - 3, 6, 6));
            //            if ((dyd4 != 0 && lastpoint4.X > 169))
            //                e.Graphics.DrawLine(Pens.DarkCyan, new Point(x, y), lastpoint4);
            //        }
            //        if (CGUAN&&!BGUAN)
            //        {
            //            if (cgTime < tp.D && lastpoint4.X > 169)
            //            {
            //                int xPlus = x - lastpoint4.X; // 描点作图
            //                Point p1 = lastpoint4;
            //                Point p2 = new Point(x - 2 * xPlus / 3, y + 7);
            //                Point p3 = new Point(x - xPlus / 3, y - 7);
            //                Point p4 = new Point(x, y);
            //                e.Graphics.DrawBezier(Pens.DarkCyan, p1, p2, p3, p4);

            //            }
                       
            //        }
            //        if (CGUAN && BGUAN)
            //        {
            //            if (cgTime < tp.D && tp.D < bgTime && lastpoint4.X > 169)
            //            {
            //                int xPlus = x - lastpoint4.X; // 描点作图
            //                Point p1 = lastpoint4;
            //                Point p2 = new Point(x - 2 * xPlus / 3, y + 7);
            //                Point p3 = new Point(x - xPlus / 3, y - 7);
            //                Point p4 = new Point(x, y);
            //                e.Graphics.DrawBezier(Pens.DarkCyan, p1, p2, p3, p4);


            //            }
            //            else
            //                e.Graphics.DrawEllipse(Pens.DarkCyan, new Rectangle(x - 3, y - 3, 6, 6));
            //            if ((dyd4 != 0 && lastpoint4.X > 169) && (cgTime > tp.D||bgTime<tp.D))
            //                e.Graphics.DrawLine(Pens.DarkCyan, new Point(x, y), lastpoint4);
                      
            //        }
                    
            //    }
            //    lastpoint4.X = x;
            //    lastpoint4.Y = y;
            //    dyd4++;
            //}
            //foreach (adims_MODEL.point tp in etco2List)//画ETCO2
            //{
            //    int x, y;
            //    TimeSpan t = new TimeSpan();
            //    t = tp.D - otime;
            //    x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
            //    if (tp.V > 230)
            //    {
            //        y = 368 - 345;
            //        e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Red, x, y);
            //    }
            //    else
            //        y = (int)(368 - (int)(tp.V * 1.5));
            //    if (x > 169)
            //    {
            //        if (tp.V > 0)
            //            e.Graphics.DrawPolygon(Pens.DarkOrange, new Point[3] { new Point(x, y), new Point(x - 4, y - 6), new Point(x + 4, y - 6) });

            //        if (dyd5 != 0 && lastpoint5.X > 169)
            //            e.Graphics.DrawLine(Pens.DarkOrange, new Point(x, y), lastpoint5);

            //    }
                
            //    lastpoint5.X = x;
            //    lastpoint5.Y = y;
            //    dyd5++;
            //}
            #endregion



            #region //画术中事件,其他用药
            int szsji = 1;
            foreach (adims_MODEL.szsj s in szsjList)//画术中事件
            {
                TimeSpan t = new TimeSpan();
                t = s.D - otime;
                int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 165);
                float y1 = (float)(50);
                e.Graphics.FillRectangle(Brushes.Pink, x1, y1, szsji > 9 ? 14 : 10, 12);
                e.Graphics.DrawString(szsji.ToString(), this.Font, Brushes.Black, new PointF(szsji > 9 ? x1 - 2 : x1, y1));
                szsji++;
            }
            int tsyyi = 1;
            foreach (adims_MODEL.tsyy s in tsyyList)//画其他用药
            {
                TimeSpan t = new TimeSpan();
                t = s.D - otime;
                float x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 165);
                float y1 = (float)(370);
                e.Graphics.FillEllipse(Brushes.LightGreen, x1, y1, tsyyi > 9 ? 14 : 10, 10);
                e.Graphics.DrawString(tsyyi.ToString(), this.Font, Brushes.Black, new PointF(tsyyi > 9 ? x1 - 2 : x1, y1));
                tsyyi++;
            }
            #endregion

        }
        adims_MODEL.jhxm T_etco2 = new adims_MODEL.jhxm();
        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            if (!IsInNet())
            {
                return;
            }
            #region //是否选中监测点

            foreach (adims_MODEL.point tssy in ssyList)
            {
                TimeSpan t = new TimeSpan();
                t = tssy.D - otime;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                int y = 0;
                if (tssy.V > 230)
                    y = (int)(368 - 345);
                else
                    y = (int)(368 - (int)(tssy.V * 1.5));
                if (p3x > x - 5 && p3x < x + 5 && p3y < y + 1 && p3y > y - 5)
                {
                    flagP3 = 1;
                    typeP3 = 1;
                    t_point = tssy;
                    lab1.Visible = true;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.AutoSize = true;
                    pictureBox3.Controls.Add(lab1);
                    lab1.Location = new Point(x, y);
                    lab1.Text = t_point.V.ToString();
                    lx = 1;
                    xgqvalue = t_point.V;
                }

            }
            foreach (adims_MODEL.point tssy in szyList)
            {
                TimeSpan t = new TimeSpan();
                t = tssy.D - otime;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                int y = 0;
                if (tssy.V > 230)
                    y = (int)(368 - 345);
                else
                    y = (int)(368 - (int)(tssy.V * 1.5));
                if (p3x > x - 5 && p3x < x + 5 && p3y < y + 5 && p3y > y - 1)
                {
                    flagP3 = 1;
                    typeP3 = 1;
                    t_point = tssy;
                    lab1.Visible = true;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.AutoSize = true;
                    pictureBox3.Controls.Add(lab1);
                    lab1.Location = new Point(x, y);
                    lab1.Text = t_point.V.ToString();
                    lx = 2;
                    xgqvalue = t_point.V;
                }

            }

            foreach (adims_MODEL.point tssy in mboList)
            {
                TimeSpan t = new TimeSpan();
                t = tssy.D - otime;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                int y = 0;
                if (tssy.V > 230)
                    y = (int)(368 - 345);
                else
                    y = (int)(368 - (int)(tssy.V * 1.5));
                if (p3x > x - 5 && p3x < x + 5 && p3y < y + 3 && p3y > y - 3)
                {
                    flagP3 = 1;
                    typeP3 = 1;
                    t_point = tssy;
                    lab1.Visible = true;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.AutoSize = true;
                    pictureBox3.Controls.Add(lab1);
                    lab1.Location = new Point(x, y);
                    lab1.Text = t_point.V.ToString();
                    lx = 3;
                    xgqvalue = t_point.V;
                }
            }

            foreach (adims_MODEL.point tssy in hxlList)
            {
                //if (tssy.D < jkksTime || tssy.D > jkjsTime)
                //{
                    TimeSpan t = new TimeSpan();
                    t = tssy.D - otime;
                    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                    int y = 0;
                    if (tssy.V > 230)
                        y = (int)(368 - 345);
                    else
                        y = (int)(368 - (int)(tssy.V * 1.5));
                    if (p3x > x - 5 && p3x < x + 5 && p3y < y + 3 && p3y > y - 3)
                    {
                        flagP3 = 1;
                        typeP3 = 1;
                        t_point = tssy;
                        lab1.Visible = true;
                        lab1.BackColor = Color.Transparent;
                        lab1.ForeColor = Color.Red;
                        lab1.AutoSize = true;
                        pictureBox3.Controls.Add(lab1);
                        lab1.Location = new Point(x, y);
                        lab1.Text = t_point.V.ToString();
                        lx = 4;
                        xgqvalue = t_point.V;
                    }
                //}
            }

            foreach (adims_MODEL.tw_point tssy in twList)
            {

                TimeSpan t = new TimeSpan();
                t = tssy.D - otime;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                int y = 0;
                if (tssy.V > 230)
                    y = (int)(368 - 345);
                else
                    y = (int)(368 - (int)(tssy.V * 1.5));
                if (p3x > x - 5 && p3x < x + 5 && p3y < y + 5 && p3y > y - 1)
                {
                    flagP3 = 1; typeP3 = 4;
                    tw_point = tssy;
                    lab1.Visible = true;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.AutoSize = true;
                    pictureBox3.Controls.Add(lab1);
                    lab1.Location = new Point(x, y);
                    lab1.Text = tw_point.V.ToString();
                    lx = 5;
                    xgqvalue = (int)tw_point.V;
                }

                //TimeSpan t = new TimeSpan();
                //t = tssy.D - otime;
                //int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                //int y = 0;
                //if (tssy.V > 230)
                //    y = (int)(368 - 345);
                //else
                //    y = (int)(368 - (int)(tssy.V * 1.5));
                //if (p3x > x - 5 && p3x < x + 5 && p3y < y + 5 && p3y > y - 1)
                //{
                //    flagP3 = 1; typeP3 = 1;
                //    t_point = tssy;
                //    lab1.Visible = true;
                //    lab1.BackColor = Color.Transparent;
                //    lab1.ForeColor = Color.Red;
                //    lab1.AutoSize = true;
                //    pictureBox3.Controls.Add(lab1);
                //    lab1.Location = new Point(x, y);
                //    lab1.Text = t_point.V.ToString();
                //    lx = 5;
                //    xgqvalue = t_point.V;
                //}
            }

            foreach (adims_MODEL.point tssy in etco2List)
            {
                TimeSpan t = new TimeSpan();
                t = tssy.D - otime;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                int y = 0;
                if (tssy.V > 230)
                    y = (int)(368 - 345);
                else
                    y = (int)(368 - (int)(tssy.V * 1.5));
                if (p3x > x - 5 && p3x < x + 5 && p3y < y + 1 && p3y > y - 5)
                {
                    flagP3 = 1;
                    typeP3 = 1;
                    t_point = tssy;
                    lab1.Visible = true;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.AutoSize = true;
                    pictureBox3.Controls.Add(lab1);
                    lab1.Location = new Point(x, y);
                    lab1.Text = t_point.V.ToString();
                    lx = 6;
                    xgqvalue = t_point.V;
                }

            }
            #endregion

            foreach (adims_MODEL.szsj sj in szsjList)//是否选中术中事件
            {
                TimeSpan t = new TimeSpan();
                t = sj.D - otime;
                float x = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 165);
                float y = 50;
                if (e.X > x - 8 && e.X < x + 8 && e.Y < y + 12 && e.Y > y)
                { flagP3 = 1; typeP3 = 2; t_szsj = sj; }
            }
            foreach (adims_MODEL.tsyy ts in tsyyList)//是否选中特殊用药
            {
                TimeSpan t = new TimeSpan();
                t = ts.D - otime;
                float x = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 165);
                float y = 370;
                if (e.X > x - 8 && e.X < x + 8 && e.Y < y + 12 && e.Y > y)
                { flagP3 = 1; typeP3 = 3; t_tsyy = ts; }
            }

        }

        int lx, xgqvalue, xghvalue;//生理记录检测点类型，修改前值，修改后值
        DateTime xgdtime = new DateTime();//修改点时间
        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            p3x = e.X; p3y = e.Y;
            if (flagP3 == 1)
            {
                if (typeP3 == 1)
                {
                    t_point.V = (int)((368 - p3y) / 1.5);
                    lab1.Visible = true;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.OrangeRed;
                    lab1.AutoSize = true;
                    lab1.Text = t_point.V.ToString();
                    pictureBox3.Controls.Add(lab1);
                    lab1.Location = new Point(e.X, e.Y);
                    lab1.BringToFront();
                    lx = t_point.Lx;
                    xghvalue = t_point.V; //得到修改后的值
                    xgdtime = Convert.ToDateTime(t_point.D);
                }
                if (typeP3 == 4)//体温
                {
                    string value = ((368 - p3y) / 1.5).ToString();
                    if (value.Contains("."))
                    {
                        int index = value.IndexOf(".");//查找最后一个.所在的位置
                        string ss = value.Substring(0, index + 2);   // 取子字符串。
                        tw_point.V = Convert.ToSingle(ss);
                    }
                    else
                    {
                        tw_point.V = (float)((368 - p3y) / 1.5);
                    }

                    lab1.Visible = true;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.OrangeRed;
                    lab1.AutoSize = true;
                    lab1.Text = tw_point.V.ToString();
                    pictureBox3.Controls.Add(lab1);
                    lab1.Location = new Point(e.X, e.Y);
                    lab1.BringToFront();
                    lx = tw_point.Lx;
                    xghvalue = (int)tw_point.V; //得到修改后的值
                    xgdtime = Convert.ToDateTime(tw_point.D);
                }
                if (typeP3 == 3)//特殊用药移动
                {
                    t_tsyy.D = otime.AddMinutes((p3x - 165) / 15 * jcsjjg);
                    labVisibleTimer.Enabled = true;
                    lab2.Visible = true;
                    lab2.BackColor = Color.Transparent;
                    lab2.ForeColor = Color.Blue;
                    lab2.AutoSize = true;
                    lab2.Text = t_tsyy.D.ToString("HH:mm");
                    pictureBox3.Controls.Add(lab2);
                    lab2.Location = new Point(e.X, e.Y);
                    lab2.BringToFront();
                }
                if (typeP3 == 2)//术中事件移动
                {
                    t_szsj.D = otime.AddMinutes((p3x - 165) / 15 * jcsjjg);
                    labVisibleTimer.Enabled = true;
                    lab2.Visible = true;
                    lab2.BackColor = Color.Transparent;
                    lab2.ForeColor = Color.Blue;
                    lab2.AutoSize = true;
                    lab2.Text = t_szsj.D.ToString("HH:mm");
                    pictureBox3.Controls.Add(lab2);
                    lab2.Location = new Point(e.X, e.Y);
                    lab2.BringToFront();

                }
            }
            pictureBox3.Refresh();
            labVisibleTimer.Enabled = true;
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            if (flagP3 == 1)
            {
                flagP3 = 0;
                if (typeP3 == 1)
                {
                   int i= bll.xgMZJLDpoint(mzjldID, t_point);
                   if (i>0)
                       bll.InsertxgLog(mzjldID, t_point,xgqvalue,Program.customer.user_name);
                }
                if (typeP3 == 2)
                {
                    bll.xgszsjTime(mzjldID, t_szsj);
                    BindSZSJ();
                }
                if (typeP3 == 3)
                {
                    bll.xgtsyyTime(mzjldID, t_tsyy);
                    BindTsyy();
                }
                if (typeP3 == 4)
                {
                    bll.xgMZJLDpoint_TW(mzjldID, tw_point);
                }
            }
            pictureBox3.Refresh();
            listBox1.Focus();
        }
        private void pictureBox4_Paint(object sender, PaintEventArgs e)
        {
            Pen p1 = new Pen(Brushes.Black);
            Pen p2 = new Pen(Brushes.Black);
            p2.Width = 2;
            e.Graphics.DrawLine(p2, new Point(1, 0), new Point(1, 255));
            e.Graphics.DrawLine(p2, new Point(1, 255), new Point(1069, 255));
            e.Graphics.DrawLine(p2, new Point(1069, 0), new Point(1069, 255));
        }



        #endregion
        
        #region//迈瑞函数
        //private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        //{
        //    Thread.Sleep(100);
        //    // ser.MonitorRecord MonitorRecord = new ser.MonitorRecord();
        //    int spo2 = 0, pulse = 0, nibps = 0, nibpd = 0, nibpm = 0, arts = 0, artd = 0, artm = 0, hr = 0, ico2 = 0, rrc = 0;
        //    double etco2 = 0;
        //    int fff = 0;
        //    byte[] q = new byte[500];
        //    serialPort1.Read(q, 0, serialPort1.BytesToRead);
        //    for (int i = 0; i < q.Length - 3; i++)
        //    {
        //        if (q[i] == 1 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
        //        {
        //            int j = i + 2; int m = 0;
        //            while (q[j] != 0)
        //            { m = m * 10 + q[j] - 48; j++; }
        //            i = j;
        //            //MonitorRecord.HRValue = m;
        //            hr = m;
        //            fff++;
        //            // textBox4.Text = textBox4.Text + "hr:" + m.ToString();
        //        }

        //        if (q[i] == 100 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
        //        {
        //            int j = i + 2; int m = 0;
        //            while (q[j] != 0)
        //            { m = m * 10 + q[j] - 48; j++; }
        //            i = j;
        //            // MonitorRecord.SpO2Value = m;
        //            spo2 = m;
        //            fff++;
        //            //textBox4.Text = textBox4.Text + "Spo2:" + m.ToString();
        //        }
        //        if (q[i] == 101 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
        //        {
        //            int j = i + 2; int m = 0;
        //            while (q[j] != 0)
        //            { m = m * 10 + q[j] - 48; j++; }
        //            i = j;
        //            // MonitorRecord.PulseValue = m;
        //            pulse = m;
        //            fff++;
        //            //  textBox4.Text = textBox4.Text + "Pulse:" + m.ToString();
        //        }
        //        if (q[i] == 91 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
        //        {
        //            int j = i + 2; int m = 0;
        //            while (q[j] != 0)
        //            { m = m * 10 + q[j] - 48; j++; }
        //            i = j;
        //            // MonitorRecord.NIBPSValue = m;
        //            nibps = m;
        //            fff++;
        //            //textBox4.Text = textBox4.Text + "NIBPS:" + m.ToString();
        //        }
        //        if (q[i] == 92 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
        //        {
        //            int j = i + 2; int m = 0;
        //            while (q[j] != 0)
        //            { m = m * 10 + q[j] - 48; j++; }
        //            i = j;
        //            //MonitorRecord.NIBPDValue = m;
        //            nibpd = m;
        //            fff++;
        //            // textBox4.Text = textBox4.Text + "NIBPD:" + m.ToString();
        //        }
        //        if (q[i] == 93 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
        //        {
        //            int j = i + 2; int m = 0;
        //            while (q[j] != 0)
        //            { m = m * 10 + q[j] - 48; j++; }
        //            i = j;
        //            // MonitorRecord.NIBPMValue = m;
        //            nibpm = m;
        //            fff++;
        //            // textBox4.Text = textBox4.Text + "NIBPM:" + m.ToString();
        //        }
        //        if (q[i] == 32 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
        //        {
        //            int j = i + 2; int m = 0;
        //            while (q[j] != 0)
        //            { m = m * 10 + q[j] - 48; j++; }
        //            i = j;
        //            arts = m;
        //            fff++;
        //            //textBox4.Text = textBox4.Text + "IBP1S:" + m.ToString();
        //        }
        //        if (q[i] == 33 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
        //        {
        //            int j = i + 2; int m = 0;
        //            while (q[j] != 0)
        //            { m = m * 10 + q[j] - 48; j++; }
        //            i = j;
        //            artd = m;
        //            fff++;
        //            // textBox4.Text = textBox4.Text + "IBP1D:" + m.ToString();
        //        }
        //        if (q[i] == 34 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
        //        {
        //            int j = i + 2; int m = 0;
        //            while (q[j] != 0)
        //            { m = m * 10 + q[j] - 48; j++; }
        //            i = j;
        //            artm = m;
        //            fff++;
        //        }
        //        if (q[i] == 103 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
        //        {
        //            int j = i + 2; double m = 0, n = 10;
        //            while (q[j] != 0 && q[j] != 46)
        //            { m = m * 10 + q[j] - 48; j++; }
        //            if (q[j] == 46)
        //            {
        //                j++;
        //                while (q[j] != 0)
        //                {
        //                    m = m + ((double)q[j]) / n;
        //                    n = n * 10;
        //                    j++;
        //                }
        //            }
        //            i = j;
        //            // MonitorRecord.ETCO2Value = m;
        //            etco2 = m;
        //            fff++;
        //        }
        //        if (q[i] == 105 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
        //        {
        //            int j = i + 2; int m = 0;
        //            while (q[j] != 0)
        //            { m = m * 10 + q[j] - 48; j++; }
        //            i = j;
        //            // MonitorRecord.ICO2Value = m;
        //            ico2 = m;
        //            fff++;
        //        }
        //        if (q[i] == 104 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
        //        {
        //            int j = i + 2; int m = 0;
        //            while (q[j] != 0)
        //            { m = m * 10 + q[j] - 48; j++; }
        //            i = j;
        //            // MonitorRecord.RRCValue = m;
        //            rrc = m;
        //            fff++;
        //        }
        //    }
        //    if (fff > 0)
        //    {
        //        /* MonitorRecord.MonitorCode = "test75";
        //         webser.SendRawData(MonitorRecord);
        //         */
        //        int fa = dal.insertJianCeData(mzjldID, hr, spo2, pulse, nibps, nibpd, nibpm, arts, artd, artm, etco2, ico2, rrc);

        //        if (fa != 1) { MessageBox.Show("错误"); }
        //    }

        //}
        #endregion


        private void cbmzxg_TextChanged(object sender, EventArgs e)//修改麻醉效果
        {
            string SQL = "mzxg='" + cmbMZXG.Text + "' WHERE id='" + mzjldID + "'";
            int i = dal.UpdateMzjld(SQL);
            if (i == 0)
                MessageBox.Show("选择修改不成功，请重试!");
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbASA.Text.IsNullOrEmpty())
            {
                MessageBox.Show("ASA分级不能为空！");
                return ;
            }
            int i = SaveMzjld();
            if (i > 0) MessageBox.Show("麻醉记录单修改成功！");
            else MessageBox.Show("麻醉记录单修改失败！");

        }
        private void BindXueyaMaiboHuxi()
        {
            DataTable dt = bll.GetPoint(mzjldID);
            if (dt.Rows.Count > 0)
            {
                //txtXueya.Controls[0].Text = Convert.ToString(dt.Rows[0]["NIBPS"])
                //                + " / " + Convert.ToString(dt.Rows[0]["NIBPD"]);
                //txtMaibo.Controls[0].Text = Convert.ToString(dt.Rows[0]["Pulse"]);
                //txtHuxi.Controls[0].Text = Convert.ToString(dt.Rows[0]["RRC"]);
            }
            else
                MessageBox.Show("暂无检测数据，请稍后再试！");
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
        string rrc;
        string tv; 
        private void jkhxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JKTimeSet f1 = new JKTimeSet(mzjldID);
            f1.ShowDialog();
            rrc = f1.Jkhxcv;
            tv = f1.Tv;
            BindJikongTime();
            pictureBox3.Refresh();
            btnMonitor.Focus();
        }

        private void DeleteCGBGStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = dal.Update_MZJLD_CGBGSJ(mzjldID);
            if (i == 0) MessageBox.Show("选择修改不成功，请重试!");
            else
            {
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                lbCguan.Visible = false;
                CGUAN = false;
                lbCguan.MouseDown -= new MouseEventHandler(lb_cguan_MouseDown);
                lbCguan.MouseMove -= new MouseEventHandler(lb_cguan_MouseMove);
                lbCguan.MouseUp -= new MouseEventHandler(lb_cguan_MouseUp);
                lbCguan.MouseLeave -= new EventHandler(lb_cguan_MouseLeave);
                CGUAN = false;
                BGUAN = false;
                lbBguan.Visible = false;
                lbBguan.MouseDown -= new MouseEventHandler(lb_bguan_MouseDown);
                lbBguan.MouseMove -= new MouseEventHandler(lb_bguan_MouseMove);
                lbBguan.MouseUp -= new MouseEventHandler(lb_bguan_MouseUp);
                lbBguan.MouseLeave -= new EventHandler(lb_bguan_MouseLeave);
                BindJHDian();
                pictureBox3.Refresh();
            }
            listBox1.Focus();
        }

        private void txtQXHS_DoubleClick(object sender, EventArgs e)
        {
            SelectMZYSandHushi F1 = new SelectMZYSandHushi(2, txtQXHS);
            F1.ShowDialog();
        }

        private void txtXHHS_DoubleClick(object sender, EventArgs e)
        {
            SelectMZYSandHushi F1 = new SelectMZYSandHushi(2, txtXHHS);
            F1.ShowDialog();
        }

        private void txtMZYS_DoubleClick(object sender, EventArgs e)
        {
            SelectMZYSandHushi F1 = new SelectMZYSandHushi(1, txtMZYS);
            F1.ShowDialog();
        }

        private void cbJizhen_CheckedChanged(object sender, EventArgs e)
        {
            if (cbJizhen.Checked)
                cbZeqi.Checked = false;
        }

        private void cbZeqi_CheckedChanged(object sender, EventArgs e)
        {
            if (cbZeqi.Checked)
                cbJizhen.Checked = false;
        }

        private void mzjldEdit_FormClosed(object sender, FormClosedEventArgs e)//关闭时候关闭串口和线程
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.StopTransfer();
                _serialPort.Close();
            }
            if (ThreadExist)
            {
                ThreadExist = false;
                Receiving_xy.Abort();//强制结束线程运行
                Receiving_xy = null; ////在.NET中，有GC垃圾回收，如果这里不赋为null，不回收内存，甚至永远这么占着，导致内存泄漏。
            }
            if (ThreadExist_JKW)
            {
                ThreadExist_JKW = false;
                Receiving_JKW.Abort();
                Receiving_JKW = null;
            }

            timer_Miray.Stop();
            MaintainTimer.Stop();

            if (isexist_Miray)
            {
                if (ClientSocket_Miray != null)
                    ClientSocket_Miray.Close();
                if (SocketThread_Miray.ThreadState != System.Threading.ThreadState.Running)
                {
                    SocketThread_Miray.Abort();
                }
                isexist_Miray = false;
            }
        }

        private void tbChuLiang_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.DataValid.Text_Value_Limit(sender, e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.DataValid.Text_Value_Limit(sender, e);
        }

        private void btnEMR_Click(object sender, EventArgs e)//调用EMR
        {

           // D:\MzEmrCli\MzEmrCli.exe 用户代码=0002|用户名称=张三|科室ID=1502|功能模块=病历书写|住院号=0000005620-00001
            string IPaddress = "192.168.18.14";
            bool flag = DataValid.PingHost(IPaddress, 1000);
            if (flag == true)
            {
                

                Process proc = new Process();
                proc.StartInfo.FileName = "D:\\Emr\\EmrCli.exe";
                proc.StartInfo.Arguments = "用户代码=10086|住院号=" + patidccs + "|功能模块=个人病历浏览|只读=1|是否病案号=1";
                proc.StartInfo.UseShellExecute = false;
                proc.Start();
            }
            else MessageBox.Show("电子病历 数据库未连接，请检查网络");
        }

        private void btnBeforeVisit_Click(object sender, EventArgs e)
        {
            MZsqFsd f1 = new MZsqFsd(patID);
            f1.ShowDialog();
        }
        private void btnNurseRecord_Click(object sender, EventArgs e)
        {
            CHAShouShuFXPG f1 = new CHAShouShuFXPG(patID);
            f1.ShowDialog();
        }
     
        private void btnMZZJ_Click(object sender, EventArgs e)
        {
            Zffyzqshu f1 = new Zffyzqshu(patID);
            f1.ShowDialog();
        }        
        private void timerJKW_Tick(object sender, EventArgs e)
        {
            //ProceedreceivedData_JKW();
            //timerJKW.Interval = 5000;
        }
        /// <summary>
        /// 输血
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShuXuePG_Click(object sender, EventArgs e)
        {
            ShuXuePG f1 = new ShuXuePG(mzjldID.ToString(), patidccs);
            f1.ShowDialog();

        }
        /// <summary>
        /// 麻醉指标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMZZB_Click(object sender, EventArgs e)
        {
            AddMZZB f1 = new AddMZZB(mzjldID.ToString(), patID);
            f1.ShowDialog();
        }
        /// <summary>
        /// 临时医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLIYZ_Click(object sender, EventArgs e)
        {
            LsyzForm f2 = new LsyzForm(mzjldID, patID);
            f2.ShowDialog();
        }
        /// <summary>
        /// 护理记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHLJL_Click(object sender, EventArgs e)
        {
            mazuigyishuzhiqingtongyishu f2 = new mazuigyishuzhiqingtongyishu(patID);
            f2.ShowDialog();
        }
        /// <summary>
        /// 麻醉计费
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMZJF_Click(object sender, EventArgs e)
        {
            MZYYJF f2 = new MZYYJF(mzjldID.ToString(), patID);
            f2.ShowDialog();
        }
        /// <summary>
        /// 器械扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQXSM_Click(object sender, EventArgs e)
        {
            QXBCodes f2 = new QXBCodes(patID, dtOdate.Value.ToString("yyyy-MM-dd"));
            f2.ShowDialog();
        }
        /// <summary>
        ///数据监测时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 数据监测时间设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CJTimeSet f2 = new CJTimeSet(mzjldID, dtOtime.Value.ToString("yyyy-MM-dd HH:mm"), cmbSJJG.Text);
            f2.ShowDialog();
            BindJHDian();
            this.pictureBox2.Refresh();
            this.pictureBox3.Refresh();
        }

        private void timer_LB_Tick(object sender, EventArgs e)
        {
            timer_LB.Interval = 1000 * 30;
            ReadLBlog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            JHzyymzjldBMcs f2 = new JHzyymzjldBMcs(patID);
            //BeforeVisit_HS f2 = new BeforeVisit_HS(patID);
            f2.ShowDialog();
           // mzzjtlj f1 = new mzzjtlj(patID, Convert.ToDateTime(dtOdate.Value.Date.ToString("yyyy-MM-dd")));
           //f1.ShowDialog();
        }

        private void printDocument1_EndPrint(object sender, PrintEventArgs e)
        {
            iYema =1;
            fy = 0;
        }
        string BISJC;
//        private void ccccbis()
//        {

//            serialPort1.PortName = "COM3";
//            serialPort1.BaudRate = 9600;
//            serialPort1.DataBits = 8;
//            serialPort1.Parity = Parity.None;
//            serialPort1.StopBits = StopBits.One;

//            if (!serialPort1.IsOpen)
//            {
//                serialPort1.Open();
//                serialPort1.ReadTimeout = 500;
//            }
//            byte[] ReceivedByte = new byte[5000];
//            for (int cl = 0; cl < ReceivedByte.Length; cl++)
//                ReceivedByte[cl] = 0;
//            Thread.Sleep(100);
//            int len = serialPort1.Read(ReceivedByte, 0, serialPort1.BytesToRead);
//            // if (len == 0)
//            //     return;


//            string ReceivedStr = @"05/26/2017 17:34:30|      10|      27|On      |None    |Off     |Off     |No      |     0.0|     9.1|    20ac|    97.5|    76.0|    66.8|    28.6|       2|4200800c|     0.0|     0.0|    8000|     0.0|     0.0|     0.0|    71.8|       5|4200000c|     0.0|     9.1|    20ac|    97.5|    76.0|    66.8|    28.6|       0|4200800c|
//                                   05/26/2017 17:34:35|      10|      27|On      |None    |Off     |Off     |No      |     0.0|     9.1|    20ac|    97.3|    76.0|    53.7|    28.6|       6|02000004|     0.0|     0.0|    8000|     0.0|     0.0|     0.0|    65.4|       3|02000004|     0.0|     9.1|    20ac|    97.3|    76.0|    53.7|    28.6|       0|02000004|
//                                   05/25/2017 17:34:40|      10|      27|On      |None    |Off     |Off     |No      |     0.0|     9.1|    20ac|    97.4|    76.0|    55.0|    28.6|       6|02000000|     0.0|     0.0|    8000|     0.0|     0.0|     0.0|    59.6|       4|02000000|     0.0|     9.1|    20ac|    97.4|    76.0|    55.0|    28.6|       0|02000000|
//                                   05/26/2017 17:34:45|      10|      27|On      |None    |Off     |Off     |No      |     0.0|     9.5|    20ac|    97.2|    75.5|    53.3|    22.9|       9|02000000|     0.0|     0.0|    8000|     0.0|     0.0|     0.0|    53.2|       4|02000000|     0.0|     9.5|    20ac|    97.2|    75.5|    53.3|    22.9|       0|02000000|";
//            byte[] byteArray = System.Text.Encoding.Default.GetBytes(ReceivedStr);

//            if ((len < 1000) && (len > 60))
//            {
//                ReceivedStr = System.Text.Encoding.Default.GetString(byteArray);

//                string[] StringTemp = ReceivedStr.Split('|');
//                int i = 0;
//                foreach (string BasicCommand in StringTemp)
//                {

//                    //string datestr = ReceivedTime.ToString("{0:yyyy/MM/dd}");
//                    string datestr = string.Format("{0:MM/dd/yyyy}", DateTime.Now);
//                    datestr = datestr.Replace("-", "/");
//                    bool has = BasicCommand.Contains(datestr);
//                    if (BasicCommand.Contains(datestr))
//                    {
//                        string BISstr = "";
//                        if (i + 11 < StringTemp.Length)
//                            BISstr = StringTemp[i + 11];
//                        if (IsNumberic(BISstr))
//                        {
//                            int cccbis = dal.CopyDataBISsss(mzjldID, ksjcTime,BISstr);

//                          // BISTimelabel.Text = BasicCommand.Trim('\r', '\n', ' ');
//                        }

//                    }
//                    i++;
//                }

//            }

//            serialPort1.Close();

//        }

        private void ccccbis()
        {
            if (!serialPort1.IsOpen)
            {
                serialPort1.PortName = "COM3";
                serialPort1.BaudRate = 9600;
                serialPort1.DataBits = 8;
                serialPort1.Parity = Parity.None;
                serialPort1.StopBits = StopBits.One;

                serialPort1.Open();
                serialPort1.ReadTimeout = 1500;
            }
            byte[] ReceivedByte = new byte[5000];
            for (int cl = 0; cl < ReceivedByte.Length; cl++)
                ReceivedByte[cl] = 0;
            Thread.Sleep(200);
            int len = serialPort1.Read(ReceivedByte, 0, serialPort1.BytesToRead);
            // if (len == 0)
            //     return;

            string ReceivedStr;
            /*        ReceivedStr = @"05/26/2017 17:34:30|      10|      27|On      |None    |Off     |Off     |No      |     0.0|     9.1|    20ac|    97.5|    76.0|    66.8|    28.6|       2|4200800c|     0.0|     0.0|    8000|     0.0|     0.0|     0.0|    71.8|       5|4200000c|     0.0|     9.1|    20ac|    97.5|    76.0|    66.8|    28.6|       0|4200800c|
                                           05/26/2017 17:34:35|      10|      27|On      |None    |Off     |Off     |No      |     0.0|     9.1|    20ac|    97.3|    76.0|    53.7|    28.6|       6|02000004|     0.0|     0.0|    8000|     0.0|     0.0|     0.0|    65.4|       3|02000004|     0.0|     9.1|    20ac|    97.3|    76.0|    53.7|    28.6|       0|02000004|
                                           05/25/2017 17:34:40|      10|      27|On      |None    |Off     |Off     |No      |     0.0|     9.1|    20ac|    97.4|    76.0|    55.0|    28.6|       6|02000000|     0.0|     0.0|    8000|     0.0|     0.0|     0.0|    59.6|       4|02000000|     0.0|     9.1|    20ac|    97.4|    76.0|    55.0|    28.6|       0|02000000|
                                           05/26/2017 17:34:45|      10|      27|On      |None    |Off     |Off     |No      |     0.0|     9.5|    20ac|    97.2|    75.5|    53.3|    22.9|       9|02000000|     0.0|     0.0|    8000|     0.0|     0.0|     0.0|    53.2|       4|02000000|     0.0|     9.5|    20ac|    97.2|    75.5|    53.3|    22.9|       0|02000000|";
                    byte[] byteArray = System.Text.Encoding.Default.GetBytes(ReceivedStr);*/

            if ((len < 4900) && (len > 60))
            {
                ReceivedStr = System.Text.Encoding.Default.GetString(ReceivedByte);

                string[] StringTemp = ReceivedStr.Split('|');
                int i = 0;
                foreach (string BasicCommand in StringTemp)
                {

                    //string datestr = ReceivedTime.ToString("{0:yyyy/MM/dd}");
                    string datestr = string.Format("{0:MM/dd/yyyy}", DateTime.Now);
                    datestr = datestr.Replace("-", "/");
                    bool has = BasicCommand.Contains(datestr);
                    if (BasicCommand.Contains(datestr))
                    {
                        string BISstr = "";
                        if (i + 11 < StringTemp.Length)
                            BISstr = StringTemp[i + 11];
                        if (IsNumberic(BISstr))
                        {
                           double bisstra =Convert.ToDouble(BISstr.Trim().ToString());
                            BISstr =Convert.ToString(bisstra);
                            //int cccbis = dal.CopyDataBISsss(mzjldID, ksjcTime.AddMinutes(-2d), BISstr);
                            int cccbis = dal.CopyDataBISsss(mzjldID, ksjcTime, BISstr);
                        }

                    }
                    i++;
                }

            }

           //serialPort1.Close();


        }
        public bool IsNumberic(string str)
        {
            double vsNum;
            bool isNum;
            isNum = double.TryParse(str, System.Globalization.NumberStyles.Float,
                System.Globalization.NumberFormatInfo.InvariantInfo, out vsNum);
            return isNum;
        }

        private void 机控呼吸时间设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JKTimeSetwu f1 = new JKTimeSetwu(mzjldID);
            f1.ShowDialog();
            BindJikongTime(); //机控时间
            BindJHDian(); //监护点
            pictureBox3.Refresh();
            btnMonitor.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string url = "http://192.168.18.44/MedExECGWEbSetup/Default.aspx";
                System.Diagnostics.Process.Start(url);
            
        }

        private void cheboxBIS_CheckedChanged(object sender, EventArgs e)
        {
            timerbis.Enabled = true;
        }

        private void timerbis_Tick(object sender, EventArgs e)
        {
            timerbis.Interval = 1500 * 10;
            ccccbis();
        }

        private void 修改监护一列值ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            updatejcsj f1 = new updatejcsj(mzjldID);
            f1.ShowDialog();
            BindJHDian();
            pictureBox3.Refresh();
        }

       

        private void lbMzkszgn1_DoubleClick(object sender, EventArgs e)
        {
            if (!IsInNet())
            {
                return;
            }
            
                //txtMzsj.Controls[0].Text = Convert.ToString(DateTime.Now);
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                lbMzkszgn1.Visible = true;
                lbMzkszgn1.Text = "X1";
                lbMzkszgn1.AutoSize = true;
                lbMzkszgn1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lbMzkszgn1.BackColor = Color.Transparent;
                lbMzkszgn1.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 160), 370);
                this.pictureBox3.Controls.Add(lbMzkszgn1);
                mzks = true;
                mzjs = false;
                dal.UpdateMzkssjzgn(DateTime.Now, mzjldID);
                mzkszgnTime = DateTime.Now;
                //txtMZKS.Controls[0].Text = DateTime.Now.ToString("HH:mm");
                lbMzkszgn1.MouseDown += new MouseEventHandler(lbMzkszgn1_MouseDown);
                lbMzkszgn1.MouseMove += new MouseEventHandler(lbMzkszgn1_MouseMove);
                lbMzkszgn1.MouseUp += new MouseEventHandler(lbMzkszgn1_MouseUp);
                lbMzkszgn1.MouseLeave += new EventHandler(lbMzkszgn1_MouseLeave);
            
            
        }
        private void lbMzkszgn1_MouseDown(object sender, EventArgs e)
        {
            if (!IsInNet())
            {
                return;
            }
            ssmzcgbgFlag = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.Red;
            lab1.AutoSize = true;
            pictureBox3.Controls.Add(lab1);
            lab1.Location = new Point(lbMzkszgn1.Location.X, lbMzkszgn1.Location.Y - 10);
            xStart = lbMzkszgn1.Location.X;
            yStart = lbMzkszgn1.Location.Y;
            DateTime DTIME = otime.AddMinutes((xStart - 160) / 15 * jcsjjg);
            lab1.Text = DTIME.ToString("HH:mm");
        }

        private void lbMzkszgn1_MouseMove(object sender, MouseEventArgs e)
        {
            if (ssmzcgbgFlag == 1)
            {
                //TimeSpan t = new TimeSpan();
                //t = DateTime.Now - otime;
                lbMzkszgn1.Location = new Point(lbMzkszgn1.Location.X + e.X / 2 - 2, lbMzkszgn1.Location.Y);
                xEnd = lbMzkszgn1.Location.X;
                lab1.Visible = true;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Blue;
                lab1.AutoSize = true;
                pictureBox3.Controls.Add(lab1);
                lab1.Location = new Point(lbMzkszgn1.Location.X, lbMzkszgn1.Location.Y - 10);
                DateTime DTIME = otime.AddMinutes((xEnd - 160) / 15 * jcsjjg);
                lab1.Text = DTIME.ToString("HH:mm");
                labVisibleTimer.Enabled = true;
                //txtMZKS.Controls[0].Text = DTIME.ToString("HH:mm");


            }
        }

        private void lbMzkszgn1_MouseUp(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
            if (!mzjs)
            {
                dal.UpdateMzkssjzgn(otime.AddMinutes((xEnd - 160) / 15 * jcsjjg), mzjldID);
                mzkszgnTime = otime.AddMinutes((xEnd - 160) / 3);
            }
            else
            {
                if (xEnd < lbMzjs1.Location.X)
                {
                    dal.UpdateMzkssjzgn(otime.AddMinutes((xEnd - 160) / 15 * jcsjjg), mzjldID);
                    mzkszgnTime = otime.AddMinutes((xEnd - 160) / 15 * jcsjjg);
                }
                else
                    lbMzkszgn1.Location = new Point(xStart, yStart);
            }
        }

        private void lbMzkszgn1_MouseLeave(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
        }

        private void mzkssjzz_DoubleClick(object sender, EventArgs e)
        {
            if (!IsInNet())
            {
                return;
            }
          
                //txtMzsj.Controls[0].Text = Convert.ToString(DateTime.Now);
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                mzkssjzz.Visible = true;
                mzkssjzz.Text = "X2";
                mzkssjzz.AutoSize = true;
                mzkssjzz.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                mzkssjzz.BackColor = Color.Transparent;
                mzkssjzz.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 160), 370);
                this.pictureBox3.Controls.Add(mzkssjzz);
                mzks = true;
                mzjs = false;
                dal.UpdateMzkssjsjzz(DateTime.Now, mzjldID);
                mzjkssjzzTime = DateTime.Now;
                //txtMZKS.Controls[0].Text = DateTime.Now.ToString("HH:mm");
                mzkssjzz.MouseDown += new MouseEventHandler(mzkssjzz_MouseDown);
                mzkssjzz.MouseMove += new MouseEventHandler(mzkssjzz_MouseMove);
                mzkssjzz.MouseUp += new MouseEventHandler(mzkssjzz_MouseUp);
                mzkssjzz.MouseLeave += new EventHandler(mzkssjzz_MouseLeave);
            
        }

        private void mzkssjzz_MouseDown(object sender, EventArgs e)
        {
            if (!IsInNet())
            {
                return;
            }
            ssmzcgbgFlag = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.Red;
            lab1.AutoSize = true;
            pictureBox3.Controls.Add(lab1);
            lab1.Location = new Point(mzkssjzz.Location.X, mzkssjzz.Location.Y - 10);
            xStart = mzkssjzz.Location.X;
            yStart = mzkssjzz.Location.Y;
            DateTime DTIME = otime.AddMinutes((xStart - 160) / 15 * jcsjjg);
            lab1.Text = DTIME.ToString("HH:mm");
        }

        private void mzkssjzz_MouseMove(object sender, MouseEventArgs e)
        {
            if (ssmzcgbgFlag == 1)
            {
                //TimeSpan t = new TimeSpan();
                //t = DateTime.Now - otime;
                mzkssjzz.Location = new Point(mzkssjzz.Location.X + e.X / 2 - 2, mzkssjzz.Location.Y);
                xEnd = mzkssjzz.Location.X;
                lab1.Visible = true;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Blue;
                lab1.AutoSize = true;
                pictureBox3.Controls.Add(lab1);
                lab1.Location = new Point(mzkssjzz.Location.X, mzkssjzz.Location.Y - 10);
                DateTime DTIME = otime.AddMinutes((xEnd - 160) / 15 * jcsjjg);
                lab1.Text = DTIME.ToString("HH:mm");
                labVisibleTimer.Enabled = true;
                //txtMZKS.Controls[0].Text = DTIME.ToString("HH:mm");


            }
        }

        private void mzkssjzz_MouseUp(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
            if (!mzjs)
            {
                dal.UpdateMzkssjsjzz(otime.AddMinutes((xEnd - 160) / 15 * jcsjjg), mzjldID);
                mzjkssjzzTime = otime.AddMinutes((xEnd - 160) / 3);
            }
            else
            {
                if (xEnd < lbMzjs1.Location.X)
                {
                    dal.UpdateMzkssjsjzz(otime.AddMinutes((xEnd - 160) / 15 * jcsjjg), mzjldID);
                    mzjkssjzzTime = otime.AddMinutes((xEnd - 160) / 15 * jcsjjg);
                }
                else
                    mzkssjzz.Location = new Point(xStart, yStart);
            }
        }

        private void mzkssjzz_MouseLeave(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                dal.UpdateShoushujianinfo(5, mzjldID, patID, Oroom);//修改手术间状态信息为准备手术
            }
            if (checkBox1.Checked == false)
            {
                dal.UpdateShoushujianinfo(2, mzjldID, patID, Oroom);
            }
        }

        private void pictureBox5_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.X > 0 && e.X < 169)
            {
                if (e.Y > 0 && e.Y < 15)//肖写的气体在第一行的氧气.
                {
                    addQty qt = new addQty(mzjldID);
                    qt.ShowDialog();
                    BindQtList();
                }
                else if (e.Y > 15 && e.Y < 165)//用药的事件
                {
                    addYdyao mzytform = new addYdyao(mzjldID);
                    mzytform.ShowDialog();
                    BindYdyList();
                }
                else if (e.Y > 165 && e.Y < 240)
                {
                    addShuye szyy = new addShuye(mzjldID);
                    szyy.ShowDialog();
                    BindShuyeList();
                }
                else if (e.Y > 240 && e.Y < 285)
                {
                    addShuxue szyy = new addShuxue(mzjldID);
                    szyy.ShowDialog();
                    BindShuxueList();
                }


            }
            //if (e.X > 170 && e.Y > 305 && e.Y <350)
            //{
            //     if (e.Y > 305 && e.Y < 320)//出量
            //    {
            //        MZcl formaddcl = new MZcl(otime.AddMinutes((e.X - 165) / 15 * jcsjjg), 1, mzjldID);
            //        formaddcl.ShowDialog();
            //        BindCxList();
            //    }
            //    else if (e.Y > 320 && e.Y < 335)
            //    {
            //        MZcl formaddcl = new MZcl(otime.AddMinutes((e.X - 165) / 15 * jcsjjg), 2, mzjldID);
            //        formaddcl.ShowDialog();
            //        BindClList();
            //    }
            //    else if (e.Y > 335 && e.Y < 350)
            //    {
            //        MZcl formaddcl = new MZcl(otime.AddMinutes((e.X - 165) / 15 * jcsjjg), 3, mzjldID);
            //        formaddcl.ShowDialog();
            //        BindYllList();

            //    }
            //}

          
            pictureBox5.Refresh();
            btnLeft.Focus();
        }

        private void pictureBox5_Paint(object sender, PaintEventArgs e)
        {
            Font zt8 = new Font("宋体", 8);
            Font zt7 = new Font("宋体", 7);
            Pen pred2 = new Pen(Brushes.Red);
            pred2.Width = 2;
            Pen pblack2 = new Pen(Brushes.Black);
            pblack2.Width = 2;
            Pen pxuxian = new Pen(Brushes.Black);
            Pen pblue2 = new Pen(Brushes.Green);
            pblue2.Width = 2;   //画蓝色的横线
            pxuxian.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            e.Graphics.DrawLine(pblack2, new Point(1, 0), new Point(1, 561));
            e.Graphics.DrawLine(Pens.Black, new Point(1, 0), new Point(1069, 0));
            e.Graphics.DrawLine(Pens.Black, new Point(25, 1), new Point(25, 540));
            e.Graphics.DrawLine(Pens.Black, new Point(169, 1), new Point(169, 561));
            e.Graphics.DrawLine(pblack2, new Point(1069, 0), new Point(1069, 561));
            for (int i = 0; i < 19; i++)
            {
                if (i == 11 || i == 16 || i == 19)//|| i == 20
                    e.Graphics.DrawLine(Pens.Black, new Point(1, 15 * i), new Point(1069, 15 * i));
                else
                    e.Graphics.DrawLine(Pens.Black, new Point(25, 15 * i), new Point(1069, 15 * i));
            }
            //↑横细线            
            for (int i = 0; i < 60; i++)
                e.Graphics.DrawLine(pxuxian, new PointF((float)(i * 90 / 6 + 169), (float)1), new PointF((float)(i * 90 / 6 + 169), (float)385));
            for (int i = 0; i < 10; i++)
                e.Graphics.DrawLine(Pens.Black, new PointF((float)(i * 90 + 169), (float)1), new PointF((float)(i * 90 + 169), (float)385));

     

            #region //画气体
            ArrayList sssQT = new ArrayList();
            int dy = 0;// 控制画气体输出的Y坐标           
            foreach (adims_MODEL.mzqt mz in mzqtList)
            {
                if (mz.Bz > 0)
                {
                    if (sssQT.Contains(mz.Qtname))
                        dy = dy - 1;
                    //DataTable dtM = bll.addYongyaoListzling(mzjldID, mz.Name);
                    //string bb = dtM.Rows[0][0].ToString();
                    //admin e.Graphics.DrawString(mz.Qtname + " " + mz.Dw.ToString(), this.Font, Brushes.Black, new Point(50, 15 * (dy + 0) + 2));
                    TimeSpan t = new TimeSpan();
                    TimeSpan t1 = new TimeSpan();
                    if (mz.Bz == 1)
                    {
                        t = mz.Sysj - otime;
                        if (mzjsTime > Convert.ToDateTime("1990-01-01"))
                        {
                            DataTable dtMax = bll.GetMaxPoint(mzjldID);
                            DateTime dtnow;
                            if (dtMax.Rows[0][0].ToString() != "")
                            {
                                dtnow = Convert.ToDateTime(dtMax.Rows[0][0]);
                                if (dtnow > mzjsTime)
                                {
                                    t1 = dtnow - otime;
                                }
                                else
                                {
                                    t1 = mzjsTime - otime;
                                }
                            }
                            else
                            {
                                t1 = mzjsTime - otime;
                            }
                        }
                        else
                        {
                            t1 = DateTime.Now - otime;
                        }
                    }
                    else if (mz.Bz == 2)
                    {
                        t = mz.Sysj - otime;
                        t1 = mz.Jssj - otime;
                    }
                    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 171);
                    int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 171);
                    int y1 = 15 * (dy) + 5;
                    if (x1 > 170)
                    {
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                        e.Graphics.DrawString(mz.Yl.ToString() + mz.Dw.ToString(), this.Font, Brushes.Blue, new Point(x1, y1 - 8));
                    }
                    if (x2 - x1 > 5 && mz.Bz == 1)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1));
                        e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1 + 10));
                    }
                    else if (x2 > 170 && x2 - x1 > 5 && mz.Bz == 2)
                    {

                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 5, y1 + 8), new Point(x2 + 5, y1 + 8) });
                        //double qtzongliang=(mz.Yl) *Convert.ToDouble( (x2 - x1)/3.27);
                        //e.Graphics.DrawString(Convert.ToInt32(qtzongliang).ToString() + "L", this.Font, Brushes.Blue, new Point(960, y1 - 2));
                        //MessageBox.Show(mz.Dw.Split('/').ToString());
                    }
                    if (x2 - x1 > 5)
                    {
                        if (x1 > 170 && x2 > 170)
                        {
                            e.Graphics.DrawLine(pred2, new Point(x1, y1 + 5), new Point(x2, y1 + 5));
                        }
                        if (x1 < 170 && x2 > 170)
                        {
                            e.Graphics.DrawLine(pred2, new Point(170, y1 + 5), new Point(x2, y1 + 5));
                        }


                    }
                    dy++;
                    //sssQT.Add(mz.Name);
                }
            }
            #endregion

            #region //画诱导药
            ArrayList sss = new ArrayList();
            int dy1 = 1;// 控制画气体输出的Y坐标           
            foreach (adims_MODEL.mzyt yt in ydyList)//画诱导药
            {
                if (yt.Bz > 0)
                {
                    if (sss.Contains(yt.Ytname))
                    {
                        dy1 = dy1 - 1;
                    }
                    e.Graphics.DrawString(yt.Ytname, this.Font, Brushes.Black, new Point(30, 15 * (dy1) + 2));
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
                    int y1 = 15 * (dy1 + 0) + 5;//修改y轴的坐标的是加20
                    if (x1 > 170)
                    {
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                        e.Graphics.DrawString(yt.Yl.ToString() + yt.Dw.ToString(), this.Font, Brushes.Blue, new Point(x1, y1 - 8));
                    }
                    if (x2 - x1 > 5 && yt.Cxyy == true)
                    {
                        if (yt.Bz == 1)
                        {
                            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1));
                            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1 + 10));
                        }
                        if (yt.Bz == 2)
                        {
                            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 5, y1 + 8), new Point(x2 + 5, y1 + 8) });
                        }
                        string str = (x2 - x1 + y1).ToString();
                        str += (x2 - x1 + y1).ToString();
                    }
                    if (x2 - x1 > 5 && yt.Cxyy == true)
                    {
                        if (x1 > 170 && x2 > 170)
                        {
                            e.Graphics.DrawLine(pred2, new Point(x1, y1 + 5), new Point(x2, y1 + 5));
                        }
                        if (x1 < 170 && x2 > 170)
                        {
                            e.Graphics.DrawLine(pred2, new Point(170, y1 + 5), new Point(x2, y1 + 5));
                        }
                    }
                    dy1++;
                    sss.Add(yt.Ytname);

                }

            }
            #endregion
            #region//画输液
            ArrayList sssSY = new ArrayList();
            int dy3 = 0;
            foreach (adims_MODEL.shuye mz in shuyeList)  ////画输液
            {
                if (sssSY.Contains(mz.Name))
                    dy3 = dy3 - 1;
                if (shuyeList.Count > 0)
                {
                    if (mz.Bz > 0)
                    {
                        e.Graphics.DrawString(mz.Name, this.Font, Brushes.Black, new Point(35, 15 * (dy3) + 167));
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
                        int y1 = 15 * (dy3) + 170;
                        if (x1 > 170)
                        {
                            e.Graphics.DrawString(mz.Jl.ToString() + mz.Dw.ToString(), this.Font, Brushes.Blue, new Point(x1 - 10, y1 - 8));
                            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                        }
                        dy3++;
                        sssSY.Add(mz.Name);
                    }
                }
            }
            #endregion

            #region//画输血
            ArrayList sssSX = new ArrayList();
            int dy4 = 0;
            foreach (adims_MODEL.shuxue mz in shuxueList)  ////画输血
            {
                if (sssSX.Contains(mz.Name))
                    dy4 = dy4 - 1;
                if (shuxueList.Count > 0)
                {
                    if (mz.Bz > 0)
                    {
                        e.Graphics.DrawString(mz.Name, this.Font, Brushes.Black, new Point(35, 15 * (dy4) + 242));
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
                        int y1 = 15 * (dy4) + 245;

                        int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                        int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 170);
                        if (x1 > 170)
                        {
                            e.Graphics.DrawString(mz.Jl.ToString() + mz.Dw.ToString(), this.Font, Brushes.Blue, new Point(x1 - 10, y1 - 8));
                            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                        }
                        dy4++;
                        sssSX.Add(mz.Name);
                    }
                }
            }
            #endregion
        }

        private void pictureBox5_MouseDown(object sender, MouseEventArgs e)
        {
            if (!IsInNet())
            {
                return;
            }
            int dy = 0;//气体药起始行
            ArrayList sssQT = new ArrayList();//气体药使用种类集合
            foreach (adims_MODEL.mzqt mz in mzqtList)//遍历绑定好的气体药集合
            {
                if (mz.Bz > 0)
                {
                    if (sssQT.Contains(mz.Qtname))//如果已经包含这种气体药
                        dy = dy - 1;//画图位置减一行
                    TimeSpan t = new TimeSpan();//气体开始时间和起始位置间隔
                    t = mz.Sysj - otime;
                    TimeSpan t2 = new TimeSpan();
                    t2 = mz.Jssj - otime;
                    float x = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);//气体开始时间所在横坐标
                    float y = 15 * (dy) + 5;//肖写的拖动事件
                    //float y = 15 * (dy) + 530;//别人写的拖动事件
                    float x2 = (float)((t2.Days * 24 * 60 + t2.Hours * 60 + t2.Minutes) * 15 / jcsjjg + 170);//气体结束时间所在横坐标
                    float y2 = 15 * (dy) + 5;//肖写的拖动事件
                    //float y2 = 15 * (dy) + 530;//别人写的拖动事件
                    if (p2x > x - 5 && p2x < x + 5 && p2y > y - 1 && p2y < y + 8)//如果选中气体开始标志
                    { flagP2 = 1; typeP2 = 1; t_mzqt = mz; }//flagP2为1表示标志可移动，typeP2为1指气体开始，t_mzqt赋给遍历到的气体模型
                    if (p2x > x2 - 5 && p2x < x2 + 5 && p2y > y2 - 1 && p2y < y2 + 8)
                    { flagP2 = 1; typeP2 = 11; t_mzqt = mz; }//flagP2为1表示标志可移动，typeP2为11指气体结束，t_mzqt赋给遍历到的气体模型

                    dy++;
                    sssQT.Add(mz.Qtname);//气体种类集合添加此气体
                }

            }
            int dy1 = 1;
            ArrayList sss = new ArrayList();
            foreach (adims_MODEL.mzyt yt in ydyList)
            {
                if (yt.Bz > 0)
                {
                    if (dy1 > 0 && sss.Contains(yt.Ytname))
                    {
                        dy1 = dy1 - 1;
                    }
                    TimeSpan t = new TimeSpan();
                    t = yt.Sysj - otime;
                    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                    int y = 15 * (dy1) + 5;
                    TimeSpan t2 = new TimeSpan();
                    t2 = yt.Jssj - otime;
                    int x2 = (int)((t2.Days * 24 * 60 + t2.Hours * 60 + t2.Minutes) * 15 / jcsjjg + 170);
                    int y2 = 15 * (dy1) + 5;
                    // MessageBox.Show(x.ToString() + "    " + y.ToString() + "   " + p2x.ToString() + "    " + p2y.ToString());
                    if (p2x > x - 5 && p2x < x + 5 && p2y > y - 1 && p2y < y + 8)
                    { flagP2 = 1; typeP2 = 2; t_ydy = yt; }
                    if (p2x > x2 - 5 && p2x < x2 + 5 && p2y > y2 - 1 && p2y < y2 + 8)
                    { flagP2 = 1; typeP2 = 22; t_ydy = yt; }
                    dy1++;
                    sss.Add(yt.Ytname);
                }
            }
            int dy3 = 0;
            ArrayList sssSY = new ArrayList();
            foreach (adims_MODEL.shuye mz in shuyeList)
            {
                if (mz.Bz > 0)
                {
                    if (sssSY.Contains(mz.Name))
                        dy3 = dy3 - 1;
                    TimeSpan t = new TimeSpan();
                    t = mz.Kssj - otime;
                    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                    int y = 15 * (dy3) + 170;
                    if (p2x > x - 5 && p2x < x + 5 && p2y > y - 1 && p2y < y + 8)
                    { flagP2 = 1; typeP2 = 4; t_shuye = mz; }
                    dy3++;
                    sssSY.Add(mz.Name);
                }
            }
            int dy4 = 0;
            ArrayList sssSX = new ArrayList();
            foreach (adims_MODEL.shuxue yt in shuxueList)
            {
                if (yt.Bz > 0)
                {
                    if (sssSX.Contains(yt.Name))
                        dy4 = dy4 - 1;
                    TimeSpan t = new TimeSpan();
                    t = yt.Kssj - otime;
                    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                    int y = 15 * (dy4) + 245;
                    if (p2x > x - 5 && p2x < x + 5 && p2y > y - 1 && p2y < y + 8)
                    { flagP2 = 1; typeP2 = 5; t_shuxue = yt; }
                    dy4++;
                    sssSX.Add(yt.Name);
                }
            }
        }

        private void pictureBox5_MouseMove(object sender, MouseEventArgs e)
        {
            PointF pttag = new PointF(MousePosition.X, MousePosition.Y);
            Graphics g = this.CreateGraphics();
            p2x = e.X; p2y = e.Y;
            if (MousePosition.Y > e.Y)
            {
                g.Clear(this.BackColor);
            }
            if (flagP2 == 1)//可移动标志为1
            {
                if (typeP2 == 1)//类型1表示气体开始
                {
                    t_mzqt.Sysj = otime.AddMinutes((p2x - 170) / 15 * jcsjjg);
                }
                if (typeP2 == 11)//类型11表示气体结束
                {
                    t_mzqt.Jssj = otime.AddMinutes((p2x - 170) / 15 * jcsjjg);
                }
                if (typeP2 == 2)//类型2表示诱导药开始标志移动
                {
                    t_ydy.Sysj = otime.AddMinutes((p2x - 170) / 15 * jcsjjg);
                }
                if (typeP2 == 22)//类型22表示诱导药结束标志移动
                {
                    t_ydy.Jssj = otime.AddMinutes((p2x - 170) / 15 * jcsjjg);
                }
                if (typeP2 == 3)//类型2表示局麻药开始标志移动
                {
                    //  t_jmy.Kssj = otime.AddMinutes((p2x - 170) / 15 * jcsjjg);
                }
                if (typeP2 == 4)//输液移动标志移动
                {
                    t_shuye.Kssj = otime.AddMinutes((p2x - 170) / 15 * jcsjjg);
                }
                if (typeP2 == 5)//输血移动
                {
                    t_shuxue.Kssj = otime.AddMinutes((p2x - 170) / 15 * jcsjjg);
                }
                if (typeP2 == 6)
                {
                    if ((otime.AddMinutes((p2x - 170) / 15 * jcsjjg)) > dtOtime.Value)
                    {
                        t_shixue.D = otime.AddMinutes((p2x - 170) / 15 * jcsjjg);
                    }

                    else
                        t_shixue.D = dtOtime.Value;

                }
                if (typeP2 == 7)
                {
                    if ((otime.AddMinutes((p2x - 170) / 15 * jcsjjg)) > dtOtime.Value)
                    {
                        t_yll.D = otime.AddMinutes((p2x - 170) / 15 * jcsjjg);
                    }

                    else
                        t_yll.D = dtOtime.Value;

                }
                if (typeP2 == 8)
                {
                    if ((otime.AddMinutes((p2x - 170) / 15 * jcsjjg)) > dtOtime.Value)
                    {
                        t_niaoliang.D = otime.AddMinutes((p2x - 170) / 15 * jcsjjg);
                    }

                    else
                        t_niaoliang.D = dtOtime.Value;
                }
                pictureBox5.Refresh();
            }
        }

        private void pictureBox5_MouseUp(object sender, MouseEventArgs e)
        {
            if (flagP2 != 0)
            {
                flagP2 = 0;//移动结束，标志改为0
                if (typeP2 == 1)//修改数据库气体开始时间
                {
                    bll.xgqtKssj(mzjldID, t_mzqt);
                }
                if (typeP2 == 11)//修改数据库气体结束时间
                {
                    bll.xgqtJssj(mzjldID, t_mzqt);
                }
                if (typeP2 == 2)//修改数据库诱导药时间
                {
                    if (!t_ydy.Cxyy)//非持续，修改开始和结束时间
                        bll.xgytKssjJssj(mzjldID, t_ydy);
                    else  //持续，修改开始时间
                        bll.xgytKssj(mzjldID, t_ydy);
                }
                if (typeP2 == 22)//修改数据库诱导药结束时间
                {
                    bll.xgytJssj(mzjldID, t_ydy);
                }
                if (typeP2 == 3)//修改数据库局麻药时间
                {
                    // bll.xgjt(mzjldID, t_jmy);
                }
                if (typeP2 == 4)//修改数据库局输液时间
                {
                    int i = bll.xgshuyeKSSJ(mzjldID, t_shuye);
                }
                if (typeP2 == 5)//修改数据库输血时间
                {
                    int i = bll.xgshuxueKSSJ(mzjldID, t_shuxue);
                }
                if (typeP2 == 6)
                {
                    bll.xgclsj(mzjldID, t_shixue);
                }
                if (typeP2 == 7)
                {
                    bll.xgclsj(mzjldID, t_yll);
                }
                if (typeP2 == 8)
                {
                    bll.xgclsj(mzjldID, t_niaoliang);
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            addZhenTong fromtsyy = new addZhenTong(mzjldID);
            fromtsyy.ShowDialog();
            BindZhenTongYao();
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

        private void printDocument2_BeginPrint(object sender, PrintEventArgs e)
        {
            iYema = 1;
            fy = 0;
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {


            int x = 20, y = 0;//整体位置控制
            string title = "        浙江省金华市中医医院醉记录单";//标题
            //string title2 = "              ";//标题
            string title3 = "              麻 醉 小 结";//标题
            Pen ptp = Pens.Black;//普通画笔
            Pen pblack2 = new Pen(Brushes.Black, 2);
            Pen pxuxian = new Pen(Brushes.Black);
            pxuxian.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Font ptzt14 = new System.Drawing.Font("宋体", 14,FontStyle.Bold);//标题
            Font ptzt13 = new System.Drawing.Font("宋体", 13);//标题                      
            Font ptzt12 = new Font("宋体", 10);//填入栏目字体
            Font ptzt11 = new Font("微软雅黑", 11);//填入栏目字体
            Font ptzt10 = new Font("宋体", 10);//填入栏目字体
            Font ptzt9 = new Font("宋体", 9);//填入栏目字体          
            Font ptzt8 = new Font("宋体", 8);//填入栏目字体
            Font ht7 = new Font("黑体", 7);//填入栏目字体
            Font htc = new Font("宋体", 16, FontStyle.Bold);
            Font ptzt18 = new Font("微软雅黑", 18);//普通字体   
            Font ptzt7 = new Font("宋体", 7);//填入栏目字体           
            Font ptzt7_U = new Font("宋体", 7, FontStyle.Underline, GraphicsUnit.Point);//下划线字体
            Font ptzt6 = new Font("宋体", 6);//填入栏目字体
            Font ptzt5 = new Font("宋体", 5);//填入栏目字体
            Pen pred2 = new Pen(Brushes.Red, 2);
            Pen pblue2 = new Pen(Brushes.Blue, 2);
            int x8 = 0;
            #region  麻醉总结
            if (fy == 0)
            {
                x = x + 50;
                DataTable dt = cll.mzbyjiazai(patID);
                e.Graphics.DrawString("说明：麻醉后医嘱请开在病历医嘱单上，开医嘱时参考下列内容", ptzt14, Brushes.Black, new Point(x + 50, y + 30));
            e.Graphics.DrawString("(1)                        麻醉后常规护理", ptzt11, Brushes.Black, new Point(x + 10, y + 50));
            e.Graphics.DrawLine(Pens.Black, x + 35, y + 57, x + 130, y + 57);
            e.Graphics.DrawString("(2)体    位：", ptzt11, Brushes.Black, new Point(x + 10, y + 70));
            e.Graphics.DrawString("(3)血压，脉搏，呼吸每        分钟测量一次。平稳后停止或", ptzt11, Brushes.Black, new Point(x + 10, y + 100));
            e.Graphics.DrawLine(Pens.Black, x + 171, y + 117, x + 210, y + 117);
            e.Graphics.DrawString("(4)给    氧    鼻导管           L/MIN", ptzt11, Brushes.Black, new Point(x + 10, y + 130));
            e.Graphics.DrawLine(Pens.Black, x + 145, y + 147, x + 195, y + 147);
            e.Graphics.DrawString("面罩           L/MIN", ptzt11, Brushes.Black, new Point(x + 250, y + 130));
            e.Graphics.DrawLine(Pens.Black, x + 285, y + 147, x + 340, y + 147);
            e.Graphics.DrawString("(5)吸    痰", ptzt11, Brushes.Black, new Point(x + 10, y + 160));
            
            e.Graphics.DrawString("(6)动静脉穿刺后护理", ptzt11, Brushes.Black, new Point(x + 10, y + 190));
            
            e.Graphics.DrawString("(7)机械通气", ptzt11, Brushes.Black, new Point(x + 10, y + 220));
           
            e.Graphics.DrawString("(8)鼓励病人咳嗽，作深呼吸", ptzt11, Brushes.Black, new Point(x + 10, y + 250));
            
            e.Graphics.DrawString("(9)注意椎管内麻醉病人肢体感觉和活动恢复情况", ptzt11, Brushes.Black, new Point(x + 10, y + 280));
            
            e.Graphics.DrawString("(10)其他", ptzt11, Brushes.Black, new Point(x + 10, y + 310));

            x = 10;

            e.Graphics.DrawString("麻醉术后观察记录", ptzt18, Brushes.Black, new Point(x + 260, y + 350));
            
                e.Graphics.DrawLine(Pens.Black, x + 35, y + 390, x + 780, y + 390);
                e.Graphics.DrawLine(Pens.Black, x + 35, y + 430, x + 780, y + 430);
                e.Graphics.DrawLine(Pens.Black, x + 35, y + 460, x + 780, y + 460);
                e.Graphics.DrawLine(Pens.Black, x + 35, y + 490, x + 780, y + 490);
                e.Graphics.DrawLine(Pens.Black, x + 35, y + 520, x + 780, y + 520);




                e.Graphics.DrawLine(Pens.Black, x + 35, y + 390, x + 35, y + 520);
                e.Graphics.DrawString("观察\n时间", ptzt11, Brushes.Black, new Point(x + 36, y + 390));
                e.Graphics.DrawLine(Pens.Black, x + 75, y + 390, x + 75, y + 520);
                e.Graphics.DrawString("呼吸\n抑制", ptzt11, Brushes.Black, new Point(x + 76, y + 390));
                e.Graphics.DrawLine(Pens.Black, x + 100, y + 430, x + 100, y + 520);
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 76, y + 440));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 106, y + 440));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 76, y + 470));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 106, y + 470));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 76, y + 500));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 106, y + 500));
                e.Graphics.DrawLine(Pens.Black, x + 135, y + 390, x + 135, y + 520);
                e.Graphics.DrawString("再插管", ptzt11, Brushes.Black, new Point(x + 136, y + 400));
                e.Graphics.DrawLine(Pens.Black, x + 160, y + 430, x + 160, y + 520);
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 136, y + 440));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 166, y + 440));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 136, y + 470));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 166, y + 470));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 136, y + 500));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 166, y + 500));
                e.Graphics.DrawLine(Pens.Black, x + 195, y + 390, x + 195, y + 520);
                e.Graphics.DrawString("循环\n稳定", ptzt11, Brushes.Black, new Point(x + 196, y + 390));
                e.Graphics.DrawLine(Pens.Black, x + 220, y + 430, x + 220, y + 520);
                e.Graphics.DrawString("是", ptzt11, Brushes.Black, new Point(x + 196, y + 440));
                e.Graphics.DrawString("否", ptzt11, Brushes.Black, new Point(x + 226, y + 440));
                e.Graphics.DrawString("是", ptzt11, Brushes.Black, new Point(x + 196, y + 470));
                e.Graphics.DrawString("否", ptzt11, Brushes.Black, new Point(x + 226, y + 470));
                e.Graphics.DrawString("是", ptzt11, Brushes.Black, new Point(x + 196, y + 500));
                e.Graphics.DrawString("否", ptzt11, Brushes.Black, new Point(x + 226, y + 500));
                e.Graphics.DrawLine(Pens.Black, x + 255, y + 390, x + 255, y + 520);
                e.Graphics.DrawString("咽喉痛", ptzt11, Brushes.Black, new Point(x + 256, y + 400));
                e.Graphics.DrawLine(Pens.Black, x + 280, y + 430, x + 280, y + 520);
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 256, y + 440));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 286, y + 440));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 256, y + 470));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 286, y + 470));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 256, y + 500));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 286, y + 500));
                e.Graphics.DrawLine(Pens.Black, x + 315, y + 390, x + 315, y + 520);
                e.Graphics.DrawString("恶心\n呕吐", ptzt11, Brushes.Black, new Point(x + 316, y + 390));
                e.Graphics.DrawLine(Pens.Black, x + 340, y + 430, x + 340, y + 520);
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 316, y + 440));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 346, y + 440));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 316, y + 470));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 346, y + 470));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 316, y + 500));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 346, y + 500));

                e.Graphics.DrawLine(Pens.Black, x + 375, y + 390, x + 375, y + 520);
                e.Graphics.DrawLine(Pens.Black, x + 400, y + 430, x + 400, y + 520);
                e.Graphics.DrawString("声音\n嘶哑", ptzt11, Brushes.Black, new Point(x + 375, y + 390));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 375, y + 440));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 406, y + 440));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 375, y + 470));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 406, y + 470));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 375, y + 500));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 406, y + 500));

                e.Graphics.DrawLine(Pens.Black, x + 435, y + 390, x + 435, y + 520);
                e.Graphics.DrawString("下肢肌\n力恢复", ptzt11, Brushes.Black, new Point(x + 436, y + 390));
                e.Graphics.DrawLine(Pens.Black, x + 460, y + 430, x + 460, y + 520);
                e.Graphics.DrawString("是", ptzt11, Brushes.Black, new Point(x + 436, y + 440));
                e.Graphics.DrawString("否", ptzt11, Brushes.Black, new Point(x + 466, y + 440));
                e.Graphics.DrawString("是", ptzt11, Brushes.Black, new Point(x + 436, y + 470));
                e.Graphics.DrawString("否", ptzt11, Brushes.Black, new Point(x + 466, y + 470));
                e.Graphics.DrawString("是", ptzt11, Brushes.Black, new Point(x + 436, y + 500));
                e.Graphics.DrawString("否", ptzt11, Brushes.Black, new Point(x + 466, y + 500));

                e.Graphics.DrawLine(Pens.Black, x + 495, y + 390, x + 495, y + 520);
                e.Graphics.DrawString("穿刺点\n压 痛", ptzt11, Brushes.Black, new Point(x + 496, y + 390));
                e.Graphics.DrawLine(Pens.Black, x + 540, y + 430, x + 540, y + 520);
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 496, y + 440));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 545, y + 440));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 496, y + 470));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 545, y + 470));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 496, y + 500));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 545, y + 500));
                e.Graphics.DrawLine(Pens.Black, x + 585, y + 390, x + 585, y + 520);
                e.Graphics.DrawString("脊麻后\n头 痛", ptzt11, Brushes.Black, new Point(x + 586, y + 390));
                e.Graphics.DrawLine(Pens.Black, x + 630, y + 430, x + 630, y + 520);
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 586, y + 440));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 635, y + 440));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 586, y + 470));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 635, y + 470));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 586, y + 500));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 635, y + 500));
                e.Graphics.DrawLine(Pens.Black, x + 675, y + 390, x + 675, y + 520);
                e.Graphics.DrawString("尿潴留", ptzt11, Brushes.Black, new Point(x + 676, y + 390));
                e.Graphics.DrawLine(Pens.Black, x + 700, y + 430, x + 700, y + 520);
                e.Graphics.DrawLine(Pens.Black, x + 730, y + 430, x + 730, y + 520);
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 676, y + 440));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 706, y + 440));
                e.Graphics.DrawString("导尿", ptzt11, Brushes.Black, new Point(x + 736, y + 440));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 676, y + 470));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 706, y + 470));
                e.Graphics.DrawString("导尿", ptzt11, Brushes.Black, new Point(x + 736, y + 470));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 676, y + 500));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 706, y + 500));
                e.Graphics.DrawString("导尿", ptzt11, Brushes.Black, new Point(x + 736, y + 500));
                e.Graphics.DrawLine(Pens.Black, x + 780, y + 390, x + 780, y + 520);
                e.Graphics.DrawString("其他特殊情况及处理：", ptzt11, Brushes.Black, new Point(x + 35, y + 530));
                y = 20 - 60;
            e.Graphics.DrawString("麻醉术后镇痛观察记录", ptzt18, Brushes.Black, new Point(x + 260, y + 670));
            e.Graphics.DrawString("镇痛方案：PCEA(      )；PCIA（     ）；其他", ptzt11, Brushes.Black, new Point(x + 35, y + 700));

            e.Graphics.DrawLine(Pens.Black, x + 35, y + 740, x + 780, y + 740);
            e.Graphics.DrawLine(Pens.Black, x + 105, y + 770, x + 240, y + 770);
            e.Graphics.DrawLine(Pens.Black, x + 35, y + 800, x + 780, y + 800);
            e.Graphics.DrawLine(Pens.Black, x + 35, y + 830, x + 780, y + 830);
            e.Graphics.DrawLine(Pens.Black, x + 35, y + 860, x + 780, y + 860);
            e.Graphics.DrawLine(Pens.Black, x + 35, y + 890, x + 780, y + 890);
            e.Graphics.DrawLine(Pens.Black, x + 35, y + 920, x + 780, y + 920);



            e.Graphics.DrawLine(Pens.Black, x + 35, y + 740, x + 35, y + 920);
            e.Graphics.DrawString("观察时间", ptzt11, Brushes.Black, new Point(x + 36, y + 755));

            e.Graphics.DrawLine(Pens.Black, x + 105, y + 740, x + 105, y + 920);
            e.Graphics.DrawString("痛觉评分(VAS)", ptzt11, Brushes.Black, new Point(x + 126, y + 740));
            e.Graphics.DrawString("安静时", ptzt11, Brushes.Black, new Point(x + 106, y + 771));
            e.Graphics.DrawLine(Pens.Black, x + 170, y + 770, x + 170, y + 920);
            e.Graphics.DrawString("活动时", ptzt11, Brushes.Black, new Point(x + 176, y + 771));
            e.Graphics.DrawLine(Pens.Black, x + 240, y + 740, x + 240, y + 920);
            e.Graphics.DrawString("嗜睡", ptzt11, Brushes.Black, new Point(x + 260, y + 755));
            e.Graphics.DrawLine(Pens.Black, x + 285, y + 800, x + 285, y + 920);
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 241, y + 810));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 301, y + 810));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 241, y + 840));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 301, y + 840));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 241, y + 870));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 301, y + 870));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 241, y + 900));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 301, y + 900));

            e.Graphics.DrawLine(Pens.Black, x + 330, y + 740, x + 330, y + 920);
            e.Graphics.DrawString("恶心", ptzt11, Brushes.Black, new Point(x + 350, y + 755));
            e.Graphics.DrawLine(Pens.Black, x + 375, y + 800, x + 375, y + 920);
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 331, y + 810));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 391, y + 810));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 331, y + 840));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 391, y + 840));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 331, y + 870));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 391, y + 870));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 331, y + 900));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 391, y + 900));

            e.Graphics.DrawLine(Pens.Black, x + 420, y + 740, x + 420, y + 920);
            e.Graphics.DrawString("呕吐", ptzt11, Brushes.Black, new Point(x + 440, y + 755));
            e.Graphics.DrawLine(Pens.Black, x + 465, y + 800, x + 465, y + 920);
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 421, y + 810));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 481, y + 810));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 421, y + 840));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 481, y + 840));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 421, y + 870));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 481, y + 870));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 421, y + 900));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 481, y + 900));

            e.Graphics.DrawLine(Pens.Black, x + 510, y + 740, x + 510, y + 920);
            e.Graphics.DrawString("瘙痒", ptzt11, Brushes.Black, new Point(x + 530, y + 755));
            e.Graphics.DrawLine(Pens.Black, x + 555, y + 800, x + 555, y + 920);
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 511, y + 810));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 571, y + 810));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 511, y + 840));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 571, y + 840));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 511, y + 870));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 571, y + 870));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 511, y + 900));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 571, y + 900));
            e.Graphics.DrawLine(Pens.Black, x + 600, y + 740, x + 600, y + 920);
            e.Graphics.DrawString("尿潴留", ptzt11, Brushes.Black, new Point(x + 660, y + 755));
            e.Graphics.DrawLine(Pens.Black, x + 645, y + 800, x + 645, y + 920);
            e.Graphics.DrawLine(Pens.Black, x + 705, y + 800, x + 705, y + 920);
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 601, y + 810));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 661, y + 810));
            e.Graphics.DrawString("导尿", ptzt11, Brushes.Black, new Point(x + 721, y + 810));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 601, y + 840));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 661, y + 840));
            e.Graphics.DrawString("导尿", ptzt11, Brushes.Black, new Point(x + 721, y + 840));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 601, y + 870));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 661, y + 870));
            e.Graphics.DrawString("导尿", ptzt11, Brushes.Black, new Point(x + 721, y + 870));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 601, y + 900));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 661, y + 900));
            e.Graphics.DrawString("导尿", ptzt11, Brushes.Black, new Point(x + 721, y + 900));
            e.Graphics.DrawLine(Pens.Black, x + 780, y + 740, x + 780, y + 920);
            y = 20-120;
            e.Graphics.DrawString("其他特殊情况及处理：", ptzt11, Brushes.Black, new Point(x + 35, y + 980));
            e.Graphics.DrawString("麻醉医生签字：", ptzt11, Brushes.Black, new Point(x + 505, y + 1080));
            e.Graphics.DrawString("注:麻醉术后观察记录要求在术后24小时完成，若无麻醉相关并发症发生，观察记录一次即可；麻醉术后镇痛观察", ptzt10, Brushes.Black, new Point(x + 35, y + 1110));
            e.Graphics.DrawString("记录需观察两天，每天一次；若发现有麻醉相关并发症，应及时通知经治医师共同处理，并继续观察至病情好转", ptzt10, Brushes.Black, new Point(x + 35, y + 1140));
            e.Graphics.DrawString("为止。记录时请在观察项目下打勾即可", ptzt10, Brushes.Black, new Point(x + 35, y + 1170));
            e.Graphics.DrawString("镇痛评分:向患者充分介绍VAS的相关知识，记录相应时点的VAS值。评分标准：0分 无痛；10分 强烈疼痛；", ptzt10, Brushes.Black, new Point(x + 35, y + 1200));
            e.Graphics.DrawString("1-3分 轻度疼痛；4-6分 中度疼痛；7-10分 重度疼痛。", ptzt10, Brushes.Black, new Point(x + 35, y + 1230));
            //数据
      

            y = 20;
            x = 70;
            //for (int a = 0; a < dt.Rows.Count; a++)  //  a行 b列
            //{



            e.Graphics.DrawString(dt.Rows[0]["huli"].ToString(), ptzt11, Brushes.Black, x + 35, y + 40);//麻醉后

            e.Graphics.DrawString(dt.Rows[0]["tiwei"].ToString(), ptzt11, Brushes.Black, x + 100, y + 70);//体位


            e.Graphics.DrawString(dt.Rows[0]["xueya"].ToString(), ptzt11, Brushes.Black, x + 172, y + 100);//呼吸每分钟
            e.Graphics.DrawString(dt.Rows[0]["bidaoguan"].ToString(), ptzt11, Brushes.Black, x + 160, y + 130);//导管

            e.Graphics.DrawString(dt.Rows[0]["mianzhao"].ToString(), ptzt11, Brushes.Black, x + 301, y + 130);//面罩
                
                
                    string cd = dt.Rows[0]["fuxuankuang"].ToString();
                    for (int i = 0; i < cd.Length; i++)
                    {
                        string b = cd.Substring(i, 1);
                        string sj = b;
                        if (sj == "1")
                        {
                            e.Graphics.DrawRectangle(Pens.Black, x + 100, y + 165, 10, 10);
                            e.Graphics.DrawLine(pblue2, x + 100, y + 170, x + 105, y + 175);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                            e.Graphics.DrawLine(pblue2, x + 105, y + 175, x + 110, y + 165);
                            
                        }
                        else if (sj == "2")
                        {
                            e.Graphics.DrawRectangle(Pens.Black, x + 170, y + 195, 10, 10);
                            e.Graphics.DrawLine(pblue2, x + 170, y + 200, x + 175, y + 205);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                            e.Graphics.DrawLine(pblue2, x + 175, y + 205, x + 180, y + 195);
                        }
                        else if (sj == "3")
                        {
                            e.Graphics.DrawRectangle(Pens.Black, x + 110, y + 225, 10, 10);
                            e.Graphics.DrawLine(pblue2, x + 110, y + 230, x + 115, y + 235);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                            e.Graphics.DrawLine(pblue2, x + 115, y + 235, x + 120, y + 225);
                        }
                        else if (sj == "4")
                        {
                            e.Graphics.DrawRectangle(Pens.Black, x + 220, y + 255, 10, 10);
                            e.Graphics.DrawLine(pblue2, x + 220, y + 260, x + 225, y + 265);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                            e.Graphics.DrawLine(pblue2, x + 225, y + 265, x + 230, y + 255);
                        }
                        else if (sj == "5")
                        {
                            e.Graphics.DrawRectangle(Pens.Black, x + 360, y + 285, 10, 10);
                            e.Graphics.DrawLine(pblue2, x + 360, y + 290, x + 365, y + 295);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                            e.Graphics.DrawLine(pblue2, x + 365, y + 295, x + 370, y + 285);
                        }
                        else if (sj == "6")
                        {
                            e.Graphics.DrawRectangle(Pens.Black, x + 90, y + 315, 10, 10);
                            e.Graphics.DrawLine(pblue2, x + 90, y + 320, x + 95, y + 325);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                            e.Graphics.DrawLine(pblue2, x + 95, y + 325, x + 100, y + 315);
                        }
                    }

                    e.Graphics.DrawString(dt.Rows[0]["qita"].ToString(), ptzt11, Brushes.Black, x + 101, y + 310);//其他
    
           
                    
              
        
               // if (y+310 > 800)
               // {
                    e.HasMorePages = true;
                    fy = fy + 1;
                    x = 0; y = 0;
                    return;
               // }
      
            }
            #endregion
            //else 
            //{
            //    e.Graphics.DrawString(title, ptzt13, Brushes.Black, new Point(120 + x, 20 + y));
            //}
            else
            {
                e.Graphics.DrawString(title, htc, Brushes.Black, new Point(155 + x, 35 + y));
                int Y_unLine = y + 83, YY = y + 70;
                e.Graphics.DrawString("科别：" + txtBingQu.Controls[0].Text, ptzt8, Brushes.Black, new Point(x, YY));
                e.Graphics.DrawLine(ptp, new Point(30 + x, Y_unLine), new Point(130 + x, Y_unLine));
                e.Graphics.DrawString("床号：" + txtBednumber.Controls[0].Text, ptzt8, Brushes.Black, new Point(160 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(190 + x, Y_unLine), new Point(230 + x, Y_unLine));
                e.Graphics.DrawString("住院号：" + txtZhuYuanHao.Controls[0].Text, ptzt8, Brushes.Black, new Point(240 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(280+ x, Y_unLine), new Point(390 + x, Y_unLine));
                e.Graphics.DrawString("日期：" + dtOtime.Value.ToString("yyyy年HH月dd日"), ptzt8, Brushes.Black, new Point(420 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(450 + x, Y_unLine), new Point(530 + x, Y_unLine));
                e.Graphics.DrawString("医疗费：" + dtOtime.Value.ToString("yyyy年HH月dd日"), ptzt8, Brushes.Black, new Point(550 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(590 + x, Y_unLine), new Point(730 + x, Y_unLine));
                //↑画标题一块的东西
                Y_unLine = YY + 15; YY = YY + 20;
                e.Graphics.DrawLine(pblack2, new Point(20 + x, 90 + y), new Point(770 + x, 90 + y));
                e.Graphics.DrawLine(pblack2, new Point(20 + x, 90 + y), new Point(20 + x, 1050 + y));
                e.Graphics.DrawLine(pblack2, new Point(770 + x, 90 + y), new Point(770 + x, 1050 + y));
                e.Graphics.DrawLine(pblack2, new Point(20 + x, 1050 + y), new Point(770 + x, 1050 + y));
                //↑画边框

                #region 打印病人基本信息
                YY = YY + 5; Y_unLine = YY + 13;
                e.Graphics.DrawString("姓名  " + txtName.Controls[0].Text, ptzt8, Brushes.Black, new Point(25 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(50 + x, Y_unLine), new Point(150 + x, Y_unLine));
                e.Graphics.DrawString("性别  " + txtSex.Controls[0].Text, ptzt8, Brushes.Black, new Point(160 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(185 + x, Y_unLine), new Point(220 + x, Y_unLine));
                e.Graphics.DrawString("年龄  " + txtAge.Controls[0].Text+ "岁", ptzt8, Brushes.Black, new Point(230 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(255 + x, Y_unLine), new Point(290 + x, Y_unLine));
                e.Graphics.DrawString("身高  " + txtHeight.Controls[0].Text, ptzt8, Brushes.Black, new Point(300 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(325 + x, Y_unLine), new Point(360 + x, Y_unLine));
                e.Graphics.DrawString("cm   体重  " + txtWeight.Controls[0].Text, ptzt8, Brushes.Black, new Point(360 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(420 + x, Y_unLine), new Point(460 + x, Y_unLine));
                e.Graphics.DrawString("公斤       体温：  " + txtTW.Controls[0].Text, ptzt8, Brushes.Black, new Point(460 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(560 + x, Y_unLine), new Point(670 + x, Y_unLine));
                e.Graphics.DrawString("℃", ptzt8, Brushes.Black, new Point(670 + x, YY));
                YY = YY + 20; Y_unLine = YY + 13;
                e.Graphics.DrawString("血压  " + txtXueya.Controls[0].Text, ptzt8, Brushes.Black, new Point(25 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(50 + x, Y_unLine), new Point(150 + x, Y_unLine));
                e.Graphics.DrawString("mmHg     呼吸   " + txtHuxi.Controls[0].Text, ptzt8, Brushes.Black, new Point(160 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(245 + x, Y_unLine), new Point(310 + x, Y_unLine));
                e.Graphics.DrawString("次/分    脉搏  " + txtMaibo.Controls[0].Text, ptzt8, Brushes.Black, new Point(320 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(405 + x, Y_unLine), new Point(450 + x, Y_unLine));
                e.Graphics.DrawString("次/分    血型  " + cmbXueXing.Text, ptzt8, Brushes.Black, new Point(460 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(545 + x, Y_unLine), new Point(590 + x, Y_unLine));
                e.Graphics.DrawString("ASA分级  " + cmbASA.Text, ptzt8, Brushes.Black, new Point(600 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(645 + x, Y_unLine), new Point(720 + x, Y_unLine));
                //e.Graphics.DrawRectangle(ptp, 160 + x, YY, 12, 12);
                //e.Graphics.DrawRectangle(ptp, 220 + x, YY, 12, 12);
                //e.Graphics.DrawString("急诊", ptzt8, Brushes.Black, new Point(180 + x, YY));
                //e.Graphics.DrawString("择期", ptzt8, Brushes.Black, new Point(240 + x, YY));
                //if (cbJizhen.Checked)
                //    e.Graphics.DrawLines(pblue2, new Point[] { new Point(155 + x, YY), new Point(165 + x, YY + 12), new Point(180 + x, YY) });
                //if (cbZeqi.Checked)
                //    e.Graphics.DrawLines(pblue2, new Point[] { new Point(215 + x, YY), new Point(225 + x, YY + 12), new Point(240 + x, YY) });

                //e.Graphics.DrawString("术前禁食  " + this.cmbSQJinshi.Text, ptzt8, Brushes.Black, new Point(300 + x, YY));
                //e.Graphics.DrawLine(ptp, new Point(350 + x, Y_unLine), new Point(400 + x, Y_unLine));
                //e.Graphics.DrawRectangle(ptp, 510 + x, YY - 3, 250, 78);

                //e.Graphics.DrawString("术前特殊情况：", ptzt8, Brushes.Black, new Point(520 + x, YY));
                //if (string.IsNullOrEmpty(txtTSBQing.Controls[0].Text.Trim()))
                //    txtTSBQing.Controls[0].Text = " 无";
                //string str = "";
                //int StrLength = txtTSBQing.Controls[0].Text.Trim().Length;
                //int row = StrLength / 20;
                //for (int i = 0; i <= StrLength / 20; i++)//25个字符就换行
                //{
                //    if (i < row)
                //        str += txtTSBQing.Controls[0].Text.Trim().Substring(i * 20, 20) + Environment.NewLine; //从第i*20个开始，截取20个字符串
                //    else
                //        str += txtTSBQing.Controls[0].Text.Trim().Substring(i * 20);
                //}
                //e.Graphics.DrawString(str, ptzt8, Brushes.Black, new Point(515 + x, YY + 15));


                YY = YY + 20; Y_unLine = YY + 13;
                e.Graphics.DrawString("术前诊断 " + txtSqzd.Controls[0].Text, ptzt8, Brushes.Black, new Point(25 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(70 + x, Y_unLine), new Point(400 + x, Y_unLine));
                e.Graphics.DrawString("拟施手术 " + txtNssss.Controls[0].Text, ptzt8, Brushes.Black, new Point(410 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(460 + x, Y_unLine), new Point(730 + x, Y_unLine));

                YY = YY + 20; Y_unLine = YY + 13;
                //if (string.IsNullOrEmpty(txtSqyy.Controls[0].Text.Trim()))
                //    txtSqyy.Controls[0].Text = "  无";
                //e.Graphics.DrawString("麻醉前用药 " + txtSqyy.Controls[0].Text, ptzt8, Brushes.Black, new Point(25 + x, YY));
                //e.Graphics.DrawLine(ptp, new Point(80 + x, Y_unLine), new Point(300 + x, Y_unLine));

                e.Graphics.DrawString("手术体位 " + this.cmbTiwei.Text, ptzt8, Brushes.Black, new Point(25 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(70 + x, Y_unLine), new Point(160 + x, Y_unLine));
                e.Graphics.DrawString("术前禁食  " + this.cmbSQJinshi.Text, ptzt8, Brushes.Black, new Point(200 + x, YY));
                e.Graphics.DrawLine(ptp, new Point(250 + x, Y_unLine), new Point(300 + x, Y_unLine));

                YY = YY + 20; Y_unLine = YY + 13;
                e.Graphics.DrawLine(ptp, new Point(20 + x, YY), new Point(770 + x, YY));
                //↑画边框
                #endregion

                #region 打印时间和分页
                DateTime dtnow = new DateTime();//打印截止时间判断        
                DateTime pagetime = new DateTime();
                DataTable dtMax = bll.GetMaxPoint(mzjldID);
                if (dtMax.Rows[0][0].ToString().IsNullOrEmpty())
                    dtnow = DateTime.Now;
                else
                    dtnow = Convert.ToDateTime(dtMax.Rows[0][0]);
                pagetime = ptime; //当前打印页时间           
                e.Graphics.DrawString("时间(分钟)", ptzt8, Brushes.Black, new Point(30 + x, YY + 2));
                for (int i = 0; i < 8; i++) //打印检测时间
                {
                    e.Graphics.DrawString(ptime.ToString("HH:mm"), ptzt8, Brushes.Black, new Point(177 + 72 * i + x, YY + 2));
                    ptime = ptime.AddMinutes(6 * jcsjjg);
                }
                if (ptime < dtnow)
                {
                    e.HasMorePages = true;
                    e.Graphics.DrawString("第 " + iYema.ToString() + " 页", ptzt8, Brushes.Black, new Point(340 + x, 1075 + y));
                    iYema++;
                }
                else
                {
                    e.HasMorePages = false; ptime = fristopen;
                    e.Graphics.DrawString("第 " + iYema.ToString() + " 页", ptzt8, Brushes.Black, new Point(340 + x, 1075 + y));
                }
                #endregion

                #region  打印用药区域
                YY = YY + 18;
                for (int i = 0; i < 7; i++)//画横实线
                {
                    if (i == 0 || i == 6)//|| i == 20
                        e.Graphics.DrawLine(ptp, new Point(20 + x, YY + 12 * i + y), new Point(770 + x, YY + 12 * i + y));
                    //else if (i == 33)
                    //    e.Graphics.DrawLine(ptp, new Point(35 + x, YY + 12 * i + y), new Point(700 + x, YY + 12 * i + y));
                    else
                        e.Graphics.DrawLine(ptp, new Point(35 + x, YY + 12 * i + y), new Point(770 + x, YY + 12 * i + y));
                }
                e.Graphics.DrawLine(ptp, new Point(35 + x, YY + y), new Point(35 + x, YY + 27 * 12 + y));

                for (int i = 0; i < 10; i++)//画竖实线
                {
                    e.Graphics.DrawLine(ptp, new Point(192 + x + 72 * i, YY - 2 + y), new Point(192 + x + 72 * i, YY + 27 * 12 + y));
                }
                for (int i = 1; i < 48; i++)//画竖虚线
                {
                    e.Graphics.DrawLine(pxuxian, new PointF(192 + x + 12 * i, YY + y), new Point(192 + x + 12 * i, YY + 27 * 12 + y));

                }
                e.Graphics.DrawString("监\n\n\n测", ptzt8, Brushes.Black, new Point(21 + x, YY+10));
               // e.Graphics.DrawString("用\n\n\n\n\n\n药", ptzt7, Brushes.Black, new Point(22 + x, YY + 42));
               // e.Graphics.DrawString("输\n\n液\n\n输\n\n血", ptzt7, Brushes.Black, new Point(22 + x, YY + 12 * 13 + 2));
               //// e.Graphics.DrawString("出\n\n量", ptzt7, Brushes.Black, new Point(22 + x, YY + 12 * 20 + 2));
               // //e.Graphics.DrawString("失血量ml", ptzt7, Brushes.Black, new Point(37 + x, YY + 12 * 20 + 2));
               // //e.Graphics.DrawString("尿  量ml", ptzt7, Brushes.Black, new Point(37 + x, YY + 12 * 20 + 15));
               // //e.Graphics.DrawString("入  量ml", ptzt7, Brushes.Black, new Point(37 + x, YY + 12 * 20 + 27));
               // e.Graphics.DrawString("\n监\n\n\n\n测\n\n\n\n项\n\n\n\n目", ptzt7, Brushes.Black, new Point(22 + x, YY + 12 * 22 + 2));

               // //e.Graphics.DrawString("SP02(%)", ptzt7, Brushes.Black, new Point(40 + x, YY + 12 * 34 + 2));
               // e.Graphics.DrawString("02 (吸入)", ptzt7, Brushes.Black, new Point(40 + x, YY + 2));
               // #endregion
               // #region 打印气体
               // ArrayList sssQT = new ArrayList();
               // int qti = 0;   //起步位置
               // foreach (adims_MODEL.mzqt mzqt in mzqtList)
               // {
               //     if (sssQT.Contains(mzqt.Qtname))
               //         qti = qti - 1;
               //     //if (!sssQT.Contains(mzqt.Qtname))
               //     //    e.Graphics.DrawString(mzqt.Qtname, mzqt.Qtname.Length < 7 ? ptzt6 : ptzt5, Brushes.Black, new PointF(32 + x, YY + qti * 12 + y + 3));

               //     if (mzqt.Bz == 2)
               //     {
               //         TimeSpan t = new TimeSpan();
               //         TimeSpan t1 = new TimeSpan();
               //         t1 = mzqt.Jssj - pagetime;
               //         t = mzqt.Sysj - pagetime;
               //         int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120) + x;
               //         int y1 = YY + qti * 12 + 4;
               //         int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 11 / jcsjjg + 120) + x;
               //         //double qtzongliang = (mzqt.Yl) * Convert.ToDouble((x2 - x1) / 2.5);
               //         //e.Graphics.DrawString(Convert.ToInt32(qtzongliang).ToString() + "L", this.Font, Brushes.Blue, new Point(763, y1 - 2));
               //         if (x1 > 120 + x && x1 < 780 + x)
               //         {
               //             e.Graphics.DrawString(mzqt.Yl.ToString() + " " + mzqt.Dw, ptzt6, Brushes.Blue, new Point(x1 + 3, y + y1 - 6));
               //             e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1 + y), new Point(x1 - 3, y1 + 6 + y), new Point(x1 + 3, y1 + 6 + y) });
               //         }
               //         if (x2 > 120 + x && x2 < 780 + x)
               //         {
               //             e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1 + y), new Point(x2 - 3, y1 + 6 + y), new Point(x2 + 3, y1 + 6 + y) });
               //         }
               //         if (x1 > 120 + x && x1 < 780 + x && x2 > 120 + x && x2 < 780 + x)
               //         {
               //             e.Graphics.DrawLine(pred2, new Point(x1, y1 + y + 3), new Point(x2, y1 + y + 3));
               //         }
               //         if (x1 > 120 + x && x1 < 780 + x && x2 > 780 + x)
               //         {
               //             e.Graphics.DrawLine(pred2, new Point(x1, y1 + y + 3), new Point(780 + x, y1 + y + 3));
               //         }
               //         if (x1 < 120 + x && x2 > 120 + x && x2 < 780 + x)
               //         {
               //             e.Graphics.DrawLine(pred2, new Point(120 + x, y1 + y + 3), new Point(x2, y1 + y + 3));
               //         }
               //         if (x1 < 120 + x && x2 > 780 + x)
               //         {
               //             e.Graphics.DrawLine(pred2, new Point(120 + x, y1 + y + 3), new Point(780 + x, y1 + y + 3));
               //         }
               //     }
               //     qti++;
               //     sssQT.Add(mzqt.Qtname);
               // }

               // #endregion
               // #region 打印全麻药
               // ArrayList sssYDY = new ArrayList();
               // int ydyi = 1;
               // foreach (adims_MODEL.mzyt mzyt in ydyList) //打印全麻药
               // {
               //     if (sssYDY.Contains(mzyt.Ytname))
               //         ydyi = ydyi - 1;
               //     if (!sssYDY.Contains(mzyt.Ytname))
               //         e.Graphics.DrawString(mzyt.Ytname + " " + mzyt.Dw, mzyt.Ytname.Length < 7 ? ptzt7 : ptzt5, Brushes.Black, new PointF(37 + x, YY + ydyi * 12 + y + 3));

               //     if (mzyt.Cxyy == false)
               //     {
               //         TimeSpan t = new TimeSpan();
               //         t = mzyt.Sysj - pagetime;
               //         int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120);
               //         int y1 = YY + ydyi * 12 + 4;
               //         if (x1 > 100 + x && x1 < 700 + x)
               //         {
               //             e.Graphics.DrawString(mzyt.Yl.ToString(), ptzt7, Brushes.Blue, new Point(x1 + 3 + x, y + y1 - 6));
               //             e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), new Point(x1 - 3 + x, y1 + 6 + y), new Point(x1 + 3 + x, y1 + 6 + y) });
               //         }
               //     }
               //     if (mzyt.Cxyy == true)
               //     {
               //         TimeSpan t = new TimeSpan();
               //         TimeSpan t1 = new TimeSpan();
               //         t1 = mzyt.Jssj - pagetime;
               //         t = mzyt.Sysj - pagetime;
               //         int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120);
               //         int y1 = YY + ydyi * 12 + 4;
               //         int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 11 / jcsjjg + 120);
               //         if (x1 > 120 + x && x1 < 780 + x)
               //         {
               //             e.Graphics.DrawString(mzyt.Yl.ToString(), ptzt7, Brushes.Blue, new Point(x1 + 3 + x, y + y1 - 6));
               //             e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), new Point(x1 - 3 + x, y1 + 6 + y), new Point(x1 + 3 + x, y1 + 6 + y) });
               //         }
               //         if (x2 > 120 + x && x2 < 780 + x)
               //         {
               //             e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2 + x, y1 + y), new Point(x2 - 3 + x, y1 + 6 + y), new Point(x2 + 3 + x, y1 + 6 + y) });
               //         }
               //         if (x1 > 120 + x && x1 < 780 + x && x2 > 120 + x && x2 < 780 + x)
               //         {
               //             e.Graphics.DrawLine(pred2, new Point(x1 + x, y1 + y + 3), new Point(x2 + x, y1 + y + 3));
               //         }
               //         if (x1 > 120 + x && x1 < 780 + x && x2 > 780 + x)
               //         {
               //             e.Graphics.DrawLine(pred2, new Point(x1 + x, y1 + y + 3), new Point(780 + x, y1 + y + 3));
               //         }
               //         if (x1 < 120 + x && x2 > 120 + x && x2 < 780 + x)
               //         {
               //             e.Graphics.DrawLine(pred2, new Point(120 + x, y1 + y + 3), new Point(x2 + x, y1 + y + 3));
               //         }
               //         if (x1 < 120 + x && x2 > 780 + x)
               //         {
               //             e.Graphics.DrawLine(pred2, new Point(120 + x, y1 + y + 3), new Point(780 + x, y1 + y + 3));
               //         }
               //     }
               //     ydyi++;
               //     sssYDY.Add(mzyt.Ytname);
               // }
               // #endregion

               // #region 打印局麻药
               // //int jti = 20;  //打印局麻药
               // //ArrayList sssJMY = new ArrayList();
               // //foreach (adims_MODEL.jtytsx jt in jmyList)
               // //{
               // //    if (sssJMY.Contains(jt.Name))
               // //        jti = jti - 1;
               // //    if (!sssJMY.Contains(jt.Name))
               // //        e.Graphics.DrawString(jt.Name + " " + jt.Dw, jt.Name.Length < 7 ? ptzt7 : ptzt5, Brushes.Black, new PointF(37 + x, YY + jti * 12 + y + 2));

               // //    TimeSpan t = new TimeSpan();
               // //    t = jt.Kssj - pagetime;
               // //    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120);
               // //    int y1 = YY + jti * 12 + 4;
               // //    if (x1 > 120 + x && x1 < 780 + x)
               // //    {
               // //        e.Graphics.DrawString(jt.Jl.ToString(), ptzt7, Brushes.Blue, new Point(x1 + 3 + x, y + y1 - 6));
               // //        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), new Point(x1 - 3 + x, y1 + 6 + y), new Point(x1 + 3 + x, y1 + 6 + y) });
               // //    }
               // //    jti++;
               // //    sssJMY.Add(jt.Name);
               // //}
               // #endregion

               // #region 打印输液
               // int syi = 13;
               // ArrayList sssSY = new ArrayList();
               // foreach (adims_MODEL.shuye sx in shuyeList)
               // {
               //     if (sssSY.Contains(sx.Name))
               //         syi = syi - 1;
               //     if (!sssSY.Contains(sx.Name))
               //         e.Graphics.DrawString(sx.Name + " " + sx.Dw, sx.Name.Length < 9 ? ptzt7 : ptzt5, Brushes.Black, new PointF(37 + x, YY + syi * 12 + y + 3));

               //     TimeSpan t = new TimeSpan();
               //     TimeSpan t1 = new TimeSpan();
               //     t1 = pagetime.AddMinutes(60 * jcsjjg) - pagetime;
               //     t = sx.Kssj - pagetime;
               //     //e.Graphics.DrawString(sx.Name.ToString(), ptzt7, Brushes.Black, 37 + x, YY + syi * 12 + 2);
               //     int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120);
               //     int y1 = YY + syi * 12 + 5;
               //     int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 11 / jcsjjg + 120);

               //     if (x1 > 120 + x && x1 < 780 + x)
               //     {
               //         e.Graphics.DrawString(sx.Jl.ToString(), ptzt7, Brushes.Blue, x1 + x, y + y1 - 8);
               //         e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), 
               //         new Point(x1 - 3 + x, y1 + 5 + y), new Point(x1 + 3 + x, y1 + 5 + y) });
               //     }
               //     syi++;
               //     sssSY.Add(sx.Name);
               // }

               // #endregion

               // #region 打印输血
               // //打印输血
               // int sxi = 18;
               // ArrayList sssSX = new ArrayList();
               // foreach (adims_MODEL.shuxue sx in shuxueList)
               // {
               //     if (sssSX.Contains(sx.Name))
               //         sxi = sxi - 1;
               //     if (!sssSX.Contains(sx.Name))
               //         e.Graphics.DrawString(sx.Name + " " + sx.Dw, sx.Name.Length < 9 ? ptzt7 : ptzt5, Brushes.Black, new PointF(37 + x, YY + sxi * 12 + y + 3));

               //     TimeSpan t = new TimeSpan();
               //     TimeSpan t1 = new TimeSpan();
               //     t1 = pagetime.AddHours(jcsjjg) - pagetime;
               //     t = sx.Kssj - pagetime;
               //     //e.Graphics.DrawString(sx.Name.ToString(), ptzt7, Brushes.Black, 37 + x, YY + sxi * 12 + 2);
               //     int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120);
               //     int y1 = YY + sxi * 12 + 5;
               //     int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 11 / jcsjjg + 120);
               //     if (x1 > 120 + x && x1 < 780 + x)
               //     {
               //         e.Graphics.DrawString(sx.Jl.ToString(), ptzt7, Brushes.Blue, x1 + x, y + y1 - 6);
               //         e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), 
               //         new Point(x1 - 3 + x, y1 + 5 + y), new Point(x1 + 3 + x, y1 + 5 + y) });
               //     }
               //     sxi++;
               //     sssSX.Add(sx.Name);
               // }
               // #endregion
               // #region<<打印失血>>
               // //e.Graphics.DrawString("失 血(ml)", ptzt7, Brushes.Black, x + 37, YY + 15 * 12 + y + 3);
               // int cx_count = 0;
               // foreach (adims_MODEL.clcxqt cx in cxList)
               // {
               //     if (cx.V > 0)
               //     {
               //         if (cx.D >= pagetime && cx.D <= pagetime.AddMinutes(60 * jcsjjg))
               //         {
               //             TimeSpan t = new TimeSpan();
               //             t = cx.D - pagetime;

               //             float jhx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 132);
               //             float jhy = YY + 12 * 20;
               //             //if (cx_count % 5 == 0)
               //             {
               //                 if (cx.V.ToString().Length > 3)
               //                 {
               //                     e.Graphics.FillRectangle(Brushes.Pink, jhx + 3, jhy, 16, 9);
               //                 }
               //                 else
               //                 {
               //                     e.Graphics.FillRectangle(Brushes.Pink, jhx + 3, jhy, 9, 9);
               //                 }
               //                 e.Graphics.DrawString(cx.V.ToString(), cx.V.ToString().Length > 3 ? ptzt6 : ptzt7, Brushes.Black, new PointF(jhx + 5, jhy));

               //             }
               //             cx_count++;
               //         }
               //     }
               //     else
               //     {
               //         //int xxx = 112;
               //         //int jhyyy = 445;
               //         //e.Graphics.DrawLine(ptp, new Point(xxx, jhyyy), new Point(xxx + 30, jhyyy - 10));
               //     }
               // }
               // #endregion
               // //e.Graphics.DrawString("引流量(ml)", ptzt7, Brushes.Black, x + 37, YY + 16 * 12 + y + 3);

               // #region<<打印入量>>

               // int cn_count = 0;
               // foreach (adims_MODEL.clcxqt q in yllList)
               // {
               //     if (q.V > 0)
               //     {
               //         if (q.D >= pagetime && q.D <= pagetime.AddMinutes(60 * jcsjjg))
               //         {
               //             TimeSpan t = new TimeSpan();
               //             t = q.D - pagetime;
               //             float jhx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 132);
               //             float jhy = YY + 22 * 12;
               //             //if (cn_count % 5 == 0)
               //             {
               //                 if (q.V.ToString().Length > 3)
               //                 {
               //                     e.Graphics.FillRectangle(Brushes.Pink, jhx + 3, jhy, 16, 9);
               //                 }
               //                 else
               //                 {
               //                     e.Graphics.FillRectangle(Brushes.Pink, jhx + 3, jhy, 9, 9);
               //                 }
               //                 e.Graphics.DrawString(q.V.ToString(), q.V.ToString().Length > 3 ? ptzt6 : ptzt7, Brushes.Black, new PointF(jhx + 5, jhy));

               //             }
               //             cn_count++;
               //         }
               //     }
               //     else
               //     {
               //         //int xxx = 112;
               //         //int jhyyy = 469;
               //         //e.Graphics.DrawLine(ptp, new Point(xxx, jhyyy), new Point(xxx + 30, jhyyy - 10));
               //     }
               // }
               // #endregion
               e.Graphics.DrawString("尿量(ml)", ptzt7, Brushes.Black, x + 37, YY + 5 * 12 + y + 3);

               // #region<<打尿量>>

                int nl_count = 0;
                foreach (adims_MODEL.clcxqt cl in cnList)
                {
                    if (cl.V > 0)
                    {

                        if (cl.D >= pagetime && cl.D <= pagetime.AddMinutes(48 * jcsjjg))
                        {
                            TimeSpan t = new TimeSpan();
                            t = cl.D - pagetime;
                            float jhx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 190);
                            float jhy = YY + 5 * 12;
                            //if (nl_count % 5 == 0)

                            if (cl.V.ToString().Length > 3)
                            {
                                e.Graphics.FillRectangle(Brushes.Pink, jhx + 3, jhy, 16, 9);
                            }
                            else
                            {
                                e.Graphics.FillRectangle(Brushes.Pink, jhx + 3, jhy, 9, 9);
                            }
                            e.Graphics.DrawString(cl.V.ToString(), cl.V.ToString().Length > 3 ? ptzt6 : ptzt7, Brushes.Black, new PointF(jhx + 5, jhy));


                            nl_count++;
                        }
                    }
                    else
                    {
                        //int xxx = 112;
                        //int  jhyyy = 457;
                        //e.Graphics.DrawLine(ptp, new Point(xxx, jhyyy), new Point(xxx + 30, jhyyy - 10));
                    }

                }
                #endregion

                #region 打印监护项目

                int jhi = 0; int spo2Count = 0;
                #region 打印监护项目

                foreach (string jc in jhxmIn)
                {
                    e.Graphics.DrawString(jc, ptzt7, Brushes.Black, new PointF(35 + x, YY + jhi * 12 + y));
                    int count1 = 0;
                    foreach (adims_MODEL.jhxm j in jhxmValue)
                    {
                        if (jc == j.Sy)
                        {

                            if (j.D >= pagetime && j.D <= pagetime.AddMinutes(48* jcsjjg))
                            {
                                TimeSpan t = new TimeSpan();
                                t = j.D - pagetime;

                                float jhx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 190);
                                float jhy = YY + jhi * 12;
                                if (count1 % 2 == 0 && j.V != 0)
                                {
                                    if (j.D > cgTime && CGUAN && j.Sy == "ETCO2")
                                    {
                                        e.Graphics.FillRectangle(Brushes.Pink, jhx - 3 + x, jhy, 9, 9);
                                        e.Graphics.DrawString(j.V.ToString(), j.V > 99 ? ptzt5 : ptzt6, Brushes.Black, new PointF(jhx - 4 + x, jhy));

                                    }
                                    //if (CGUAN && BGUAN && j.Sy == "ETCO2")
                                    //{
                                    //    if (cgTime < j.D && j.D < bgTime)
                                    //    {
                                    //        e.Graphics.FillRectangle(Brushes.Pink, jhx - 3 + x, jhy, 9, 8);
                                    //        e.Graphics.DrawString(j.V.ToString(), j.V > 99 ? ptzt6 : ptzt7, Brushes.Black, new PointF(jhx - 4 + x, jhy));
                                    //    }
                                    //}
                                    else if (j.Sy != "ETCO2")
                                    {
                                        e.Graphics.FillRectangle(Brushes.Pink, jhx - 3 + x, jhy, 9, 9);
                                        e.Graphics.DrawString(j.V.ToString(), j.V > 99 ? ptzt5 : ptzt6, Brushes.Black, new PointF(jhx - 4 + x, jhy));

                                    }
                                }

                                count1++;

                            }
                            if (jc == j.Sy && j.Sy == "ECG" && jc == "ECG")
                            {

                                if (j.D >= pagetime && j.D <= pagetime.AddMinutes(48 * jcsjjg))
                                {
                                    TimeSpan t = new TimeSpan();
                                    t = j.D - pagetime;

                                    float jhx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 190);
                                    float jhy = YY + jhi * 12;
                                    string ECG = "";
                                    if (j.V == 0)
                                    {
                                        ECG = "SR";
                                    }
                                    else
                                    {
                                        ECG = j.V.ToString();
                                    }
                                    //e.Graphics.FillRectangle(Brushes.Pink, jhx - 3 + x, jhy, 9, 9);
                                    if (ECG != "")
                                        e.Graphics.DrawString(ECG, j.V > 99 ? ptzt5 : ptzt6, Brushes.Black, new PointF(jhx - 4 + x, jhy));
                                }
                            }

                        }

                    }
                    jhi++;

                }
                ///打印显示体温
                //jhi++;
               int tw_count = 0;
               if (checktw.Checked == false)
               {
                   e.Graphics.DrawString("体温", ptzt7, Brushes.Black, new PointF(35 + x, YY + jhi * 12 + y));
                   foreach (adims_MODEL.tw_point p in twList)
                   {
                       if (p.V > 0)
                       {
                           if (p.D >= pagetime && p.D <= pagetime.AddMinutes(48 * jcsjjg))
                           {
                               TimeSpan t = new TimeSpan();
                               t = p.D - pagetime;
                               float jhx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 192);
                               float jhy = YY + jhi * 12;
                               if (tw_count % 5 == 0)
                               {
                                   if (p.V.ToString().Length > 3)
                                   {
                                       e.Graphics.FillRectangle(Brushes.Pink, jhx - 3 + x, jhy, 16, 9);
                                   }
                                   else
                                   {
                                       e.Graphics.FillRectangle(Brushes.Pink, jhx - 3 + x, jhy, 9, 9);
                                   }
                                   e.Graphics.DrawString(p.V.ToString(), p.V.ToString().Length > 3 ? ptzt6 : ptzt7, Brushes.Black, new PointF(jhx - 4 + x, jhy));

                               }
                               tw_count++;
                           }
                       }


                   }
               }

                #endregion
                //foreach (adims_MODEL.jhxm j in jhxmValue)
                //{
                //    if (j.Sy == "SpO2" && j.V != 0)
                //    {
                //        if (j.D >= pagetime && j.D <= pagetime.AddMinutes(60 * jcsjjg))
                //        {
                //            TimeSpan t = new TimeSpan();
                //            t = j.D - pagetime;
                //            float jhx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 115);
                //            float jhy = YY + jhi + y + 2;
                //            if (spo2Count % 2 == 0)
                //            {
                //                e.Graphics.FillRectangle(Brushes.Pink, jhx - 3 + x, jhy, 9, 8);
                //                e.Graphics.DrawString(j.V.ToString(), j.V > 99 ? ptzt5 : ptzt7, Brushes.Black, new PointF(jhx - 4 + x, jhy));
                //            }
                //        }
                //    }
                //    spo2Count++;
                //}

                #endregion
                //tXTPH.Text = Convert.ToString(dtMzjld.Rows[0]["PH"]);
                //txtpco2.Text = Convert.ToString(dtMzjld.Rows[0]["PCO2"]);
                //txtpo2.Text = Convert.ToString(dtMzjld.Rows[0]["PO2"]);
                //txtbe.Text = Convert.ToString(dtMzjld.Rows[0]["BE"]);
                //txthco3.Text = Convert.ToString(dtMzjld.Rows[0]["HCO3"]);
                //txthb.Text = Convert.ToString(dtMzjld.Rows[0]["K"]);
                //txtk.Text = Convert.ToString(dtMzjld.Rows[0]["HB"]);
                //txthct.Text = Convert.ToString(dtMzjld.Rows[0]["HCT"]);
                //txtxuet.Text = Convert.ToString(dtMzjld.Rows[0]["XUETANG"]);
                #region 打印血气
                //Pen dakred2 = new Pen(Brushes.DarkRed, 2);
                //if (tXTPH.Text.Trim().IsNullOrEmpty())
                //    tXTPH.Text = " / ";
                //int yyyy = YY + 27 * 12 + 1;
                //e.Graphics.DrawString("PH： " + tXTPH.Text, ht7, Brushes.DarkRed, new Point(680 + x, yyyy));
                //e.Graphics.DrawLine(dakred2, new Point(705 + x, yyyy + 11), new Point(746 + x, yyyy + 11));
                //e.Graphics.DrawString("", ht7, Brushes.Black, new Point(745 + x, yyyy));
                //yyyy = yyyy + 12;
                //if (txtpco2.Text.Trim().IsNullOrEmpty())
                //    txtpco2.Text = " /";
                //e.Graphics.DrawString("PCO2:" + txtpco2.Text, ht7, Brushes.DarkRed, new Point(680 + x, yyyy));
                //e.Graphics.DrawLine(dakred2, new Point(705 + x, yyyy + 11), new Point(746 + x, yyyy + 11));
                //e.Graphics.DrawString("mmhg", ht7, Brushes.Black, new Point(745 + x, yyyy));
                //yyyy = yyyy + 12;
                //if (txtpo2.Text.Trim().IsNullOrEmpty())
                //    txtpo2.Text = "/";
                //e.Graphics.DrawString("PO2：" + txtpo2.Text, ht7, Brushes.DarkRed, new Point(680 + x, yyyy));
                //e.Graphics.DrawLine(dakred2, new Point(705 + x, yyyy + 11), new Point(746 + x, yyyy + 11));
                //e.Graphics.DrawString("mmhg", ht7, Brushes.Black, new Point(745 + x, yyyy));
                //yyyy = yyyy + 12;
                //if (txtbe.Text.Trim().IsNullOrEmpty())
                //    txtbe.Text = "/";
                //e.Graphics.DrawString("BE： " + txtbe.Text, ht7, Brushes.DarkRed, new Point(680 + x, yyyy));
                //e.Graphics.DrawLine(dakred2, new Point(705 + x, yyyy + 11), new Point(746 + x, yyyy + 11));
                //e.Graphics.DrawString("", ht7, Brushes.Black, new Point(745 + x, yyyy));
                //yyyy = yyyy + 12;
                //if (txthco3.Text.Trim().IsNullOrEmpty())
                //    txthco3.Text = "/";
                //e.Graphics.DrawString("HCO3:" + txthco3.Text, ht7, Brushes.DarkRed, new Point(680 + x, yyyy));
                //e.Graphics.DrawLine(dakred2, new Point(705 + x, yyyy + 11), new Point(746 + x, yyyy + 11));
                //e.Graphics.DrawString("mmhg/L", ht7, Brushes.Black, new Point(745 + x, yyyy));
                //yyyy = yyyy + 12;
                //if (txthb.Text.Trim().IsNullOrEmpty())
                //    txthb.Text = "/";
                //e.Graphics.DrawString("HB： " + txthb.Text, ht7, Brushes.DarkRed, new Point(680 + x, yyyy));
                //e.Graphics.DrawLine(dakred2, new Point(705 + x, yyyy + 11), new Point(746 + x, yyyy + 11));
                //e.Graphics.DrawString("g/L", ht7, Brushes.Black, new Point(745 + x, yyyy));
                //yyyy = yyyy + 12;
                //if (txtk.Text.Trim().IsNullOrEmpty())
                //    txtk.Text = "/";
                //e.Graphics.DrawString("K：  " + txtk.Text, ht7, Brushes.DarkRed, new Point(680 + x, yyyy));
                //e.Graphics.DrawLine(dakred2, new Point(705 + x, yyyy + 11), new Point(746 + x, yyyy + 11));
                //e.Graphics.DrawString("mmol/L", ht7, Brushes.Black, new Point(745 + x, yyyy));
                //yyyy = yyyy + 12;
                //if (txthct.Text.Trim().IsNullOrEmpty())
                //    txthct.Text = "/";
                //e.Graphics.DrawString("HCT：" + txthct.Text, ht7, Brushes.DarkRed, new Point(680 + x, yyyy));
                //e.Graphics.DrawLine(dakred2, new Point(705 + x, yyyy + 11), new Point(746 + x, yyyy + 11));
                //e.Graphics.DrawString("%", ht7, Brushes.Black, new Point(745 + x, yyyy));
                //yyyy = yyyy + 12;
                //if (txtxuet.Text.Trim().IsNullOrEmpty())
                //    txtxuet.Text = "/";
                //e.Graphics.DrawString("血糖:" + txtxuet.Text, ht7, Brushes.DarkRed, new Point(680 + x, yyyy));
                //e.Graphics.DrawLine(dakred2, new Point(705 + x, yyyy + 11), new Point(746 + x, yyyy + 11));
                //e.Graphics.DrawString("", ht7, Brushes.Black, new Point(745 + x, yyyy));
                #endregion



                #region//打印检测区域格子。血压体温等区域↓
                YY = YY + 12 * 7;
                e.Graphics.DrawLine(ptp, new Point(35 + x, YY), new Point(35 + x, YY + 240));
                e.Graphics.DrawString("体\n温", ptzt8, Brushes.Black, new Point(21 + x, YY-10 ));

                for (int i = 0; i < 12; i++)//画横实线           
                    e.Graphics.DrawLine(ptp, new Point(192 + x, YY + 20 * i), new Point(770 + x, YY + 20 * i + y));

                for (int i = 0; i < 12; i++)//画横虚线
                    e.Graphics.DrawLine(pxuxian, 192 + x, YY + 20 * i + 10, 770 + x, YY + 20 * i + y + 10);

                for (int i = 0; i < 8; i++)//画竖实线
                {
                    e.Graphics.DrawLine(ptp, new Point(192 + 72 * i + x, YY + y), new Point(192 + 72 * i + x, YY + 12 * 20 + y));
                }
                for (int i = 1; i < 24; i++)//画竖虚线
                {
                    e.Graphics.DrawLine(pxuxian, new Point(192 + 24 * i + x, YY + y), new Point(192 + 24 * i + x, YY + 12 * 20 + y));
                }
                for (int i = 1; i < 12; i++)
                    e.Graphics.DrawString((240 - i * 20).ToString(), ptzt7, Brushes.Black, new PointF(95 + x, YY + (float)20 * i + y - 5));
                for (int i = 1; i < 12; i++)
                    e.Graphics.DrawString((42 - i * 2).ToString(), ptzt7, Brushes.Black, new PointF(20 + x, YY + (float)20 * i + y - 5));

                e.Graphics.DrawString("∨收缩压", ptzt7, Brushes.Red, new Point(40 + x, YY + 20));
                e.Graphics.DrawString("∧舒张压", ptzt7, Brushes.Red, new Point(40 + x, YY + 35));
                e.Graphics.DrawString("●脉  搏", ptzt7, Brushes.Blue, new Point(40 + x, YY + 50));
                e.Graphics.DrawString("○呼  吸", ptzt7, Brushes.DarkCyan, new Point(40 + x, YY + 65));
                e.Graphics.DrawString("VV机械通气", ptzt7, Brushes.DarkCyan, new Point(40 + x, YY + 80));
                e.Graphics.DrawString("Ⅹ麻醉开始", ptzt7, Brushes.Black, new Point(40 + x, YY + 95));
                e.Graphics.DrawString("  置  管", ptzt7, Brushes.Black, new Point(40 + x, YY + 110));
                Image cgImage = Properties.Resources.CG;
                e.Graphics.DrawImage(cgImage, new Rectangle(43 + x, YY + 110, 9, 9));
                e.Graphics.DrawString("  拔  管", ptzt7, Brushes.Black, new Point(40 + x, YY + 125));
                Image BgImage = Properties.Resources.BG;
                e.Graphics.DrawImage(BgImage, new Rectangle(43 + x, YY + 125, 9, 9));
                e.Graphics.DrawString("⊙手术开始", ptzt7, Brushes.Black, new Point(40 + x, YY + 140));
                e.Graphics.DrawString("  手术结束", ptzt7, Brushes.Black, new Point(40 + x, YY + 155));
                Image ssjsImage = Properties.Resources.SSJS;
                e.Graphics.DrawImage(ssjsImage, new Rectangle(43 + x, YY + 155, 9, 9));
                e.Graphics.DrawString("X1 椎管内麻醉", ptzt7, Brushes.DarkRed, new Point(40 + x, YY + 170));
                e.Graphics.DrawString("X2 神经阻滞", ptzt7, Brushes.DarkOrange, new Point(40 + x, YY + 185));
                #endregion

                #region  //打印收缩压
                float px = 0, py = 0;
                Pen p_red2 = new Pen(Brushes.Red, 2);
                if (checkssy.Checked == false)
                {
                    foreach (adims_MODEL.point p in ssyList)
                    {
                        if (p.D >= pagetime && p.D <= pagetime.AddMinutes(48 * jcsjjg))
                        {
                            
                            TimeSpan t = new TimeSpan();
                            t = p.D - pagetime;
                            float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 192);
                            //int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 11 / jcsjjg + 120) + x;
                            float pointy = 0;
                            if (p.V > 230)
                            {
                                pointy = (float)((20) * 1 + YY);
                                e.Graphics.DrawString(p.V.ToString(), ptzt7, Brushes.Blue, pointx + x, pointy + y);
                            }
                            else
                                pointy = (float)((240 - p.V) * 1 + YY);
                            e.Graphics.DrawLines(p_red2, new PointF[3] { new PointF(pointx - 3 + x, pointy - 5 + y), new PointF(pointx + x, pointy + y), new PointF(pointx + 3 + x, pointy - 5 + y) });
                            if (px != 0)
                                e.Graphics.DrawLine(Pens.Red, new PointF(px + x, py + y), new PointF(pointx + x, pointy + y));

                            px = pointx;
                            py = pointy;
                        }

                    }
                }
                #endregion

                #region  //打印舒张压
                px = 0; py = 0;
                if (checkszy.Checked == false)
                {
                    foreach (adims_MODEL.point p in szyList)
                    {
                        if (p.D >= pagetime && p.D <= pagetime.AddMinutes(48 * jcsjjg))
                        {
                            TimeSpan t = new TimeSpan();
                            t = p.D - pagetime;
                            float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 192);
                            //float pointy = (float)((220 - p.V) * 1 + 460);
                            float pointy = 0;
                            if (p.V > 230)
                            {
                                pointy = (float)((20) * 1 + YY);
                                e.Graphics.DrawString(p.V.ToString(), ptzt7, Brushes.Blue, pointx + x, pointy + y);
                            }
                            else
                                pointy = (float)((240 - p.V) * 1 + YY);
                            e.Graphics.DrawLines(p_red2, new PointF[3] { new PointF(pointx - 3 + x, pointy + 5 + y), new PointF(pointx + x, pointy + y), new PointF(pointx + 3 + x, pointy + 5 + y) });
                            if (px != 0)
                                e.Graphics.DrawLine(Pens.Red, new PointF(px + x, py + y), new PointF(pointx + x, pointy + y));

                            px = pointx;
                            py = pointy;
                        }
                    }
                }
                #endregion

                #region  //打印脉搏
                px = 0; py = 0;
                if (checkmb.Checked==false)
                foreach (adims_MODEL.point p in mboList)
                {
                    if (p.D >= pagetime && p.D <= pagetime.AddMinutes(48 * jcsjjg))
                    {
                        TimeSpan t = new TimeSpan();
                        t = p.D - pagetime;
                        float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 192);
                        //float pointy = (float)((220 - p.V) * 1 + 460);
                        float pointy = 0;
                        if (p.V > 230)
                        {
                            pointy = (float)((20) * 1 + YY);
                            e.Graphics.DrawString(p.V.ToString(), ptzt7, Brushes.Blue, pointx + x, pointy + y);
                        }
                        else
                            pointy = (float)((240 - p.V) * 1 + YY);
                        e.Graphics.FillEllipse(Brushes.Blue, pointx - 2 + x, pointy - 2 + y, 5, 5);
                        if (px != 0)
                            e.Graphics.DrawLine(Pens.Blue, new PointF(px + x, py + y), new PointF(pointx + x, pointy + y));
                        px = pointx;
                        py = pointy;
                    }
                }
                #endregion

                #region  //打印体温
                //px = 0; py = 0;
                //foreach (adims_MODEL.point p in twList)
                //{
                //    if (p.D >= pagetime && p.D <= pagetime.AddMinutes(60 * jcsjjg))
                //    {
                //        TimeSpan t = new TimeSpan();
                //        t = p.D - pagetime;
                //        float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 112);
                //        //float pointy = (float)((220 - p.V) * 1 + 460);
                //        float pointy = 0;
                //        if (p.V > 230)
                //        {
                //            pointy = (float)((20) * 1 + YY);
                //            e.Graphics.DrawString(p.V.ToString(), ptzt7, Brushes.Blue, pointx + x, pointy + y);

                //        }
                //        else
                //            pointy = (float)((240 - p.V) * 1 + YY);
                //        e.Graphics.DrawPolygon(Pens.Maroon, new PointF[3] { new PointF(pointx - 3 + x, pointy + 5 + y), new PointF(pointx + x, pointy + y), new PointF(pointx + 3 + x, pointy + 5 + y) });
                //        //e.Graphics.FillEllipse(Pens.Maroon, pointx + x - 2, pointy + y - 2, 5, 5);
                //        if (px != 0)
                //            e.Graphics.DrawLine(Pens.Maroon, new PointF(px + x, py + y), new PointF(pointx + x, pointy + y));

                //        px = pointx;
                //        py = pointy;
                //       }
                //  }
                #endregion

                    #region  //打印呼吸
                px = 0; py = 0; int phuxi = 0;
                //if (checkBoxhx.Checked == false)
                //{
                    string fxs = "";
                    string tvxs = "";
                    DataTable dtMZ_Info = bll.SelectOldPatInfo(mzjldID);

                    if (dtMZ_Info.Rows.Count > 0)
                    {
                        if (this.toStr(dtMZ_Info.Rows[0]["jkvalue"]) != "")
                        {
                            fxs = dtMZ_Info.Rows[0]["jkvalue"].ToString();
                        }
                        if (this.toStr(dtMZ_Info.Rows[0]["fzvalue"]) != "")
                        {
                            tvxs = dtMZ_Info.Rows[0]["fzvalue"].ToString();
                        }
                    }
                    int dowa_sb = 0;
                    if (checkhx.Checked == false)
                    {
                        foreach (adims_MODEL.point p in hxlList)
                        {
                            if (p.D >= pagetime && p.D <= pagetime.AddMinutes(48 * jcsjjg))
                            {
                                TimeSpan t = new TimeSpan();
                                t = p.D - pagetime;

                                float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 212);
                                //float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 192);
                                float pointy = 0;
                                if (p.V > 230)
                                {
                                    //pointy = (float)((20) * 1 + YY);
                                    pointy = (float)((20) * 1 + YY) + y;
                                    e.Graphics.DrawString(p.V.ToString(), ptzt6, Brushes.Blue, pointx, pointy);
                                }
                                else
                                    pointy = (float)((240 - p.V) * 1 + YY) + y;
                                if (jkksTime < p.D && p.D < jkjsTime)
                                {
                                    //float xPlus = pointx - px; // 描点作图
                                    //PointF p1 = new PointF(px, py);
                                    //PointF p2 = new PointF(pointx - 2 * xPlus / 3, pointy + 6);
                                    //PointF p3 = new PointF(pointx - xPlus / 3, pointy - 6);
                                    //PointF p4 = new PointF(pointx, pointy);
                                    //e.Graphics.DrawBezier(Pens.DarkCyan, p1, p2, p3, p4);
                                    this.PaintSignMBreath2(e.Graphics, new PointF(pointx - 2, pointy - 4));
                                    if (dowa_sb == 0)
                                    {
                                        e.Graphics.DrawString("TV" + tvxs + "/ " + "f" + fxs, this.Font, Brushes.DarkCyan, pointx, pointy - 13);

                                        dowa_sb++;
                                    }
                                    //e.Graphics.DrawString("C", ptzt6, Brushes.DarkCyan, pointx - 2, pointy - 3);
                                }
                                //else if (fzksTime < p.D && p.D < fzjsTime)
                                //{
                                //    e.Graphics.DrawString("A", ptzt6, Brushes.DarkCyan, pointx - 2, pointy - 3);
                                //}
                                else
                                    e.Graphics.DrawEllipse(Pens.DarkCyan, pointx - 2, pointy - 2, 5, 5);
                                //if (px != 0 && (jkksTime > p.D || jkjsTime < p.D))
                                if (px != 0)
                                    e.Graphics.DrawLine(Pens.DarkCyan, new PointF(px, py), new PointF(pointx, pointy));
                                px = pointx;
                                py = pointy;
                            }
                        }
                    }
                //}
                //px = 0; py = 0; int phuxi = 0;
                //foreach (adims_MODEL.point h in hxlList)
                //{
                //    if (h.D >= pagetime && h.D <= pagetime.AddMinutes(60 * jcsjjg))
                //    {
                //        TimeSpan t = new TimeSpan();
                //        t = h.D - pagetime;
                //        float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 112);
                //        float pointy = 0;
                //        if (h.V > 230)
                //        {
                //            pointy = (float)((20) * 1 + YY) + y;
                //            e.Graphics.DrawString(h.V.ToString(), ptzt7, Brushes.Blue, pointx, pointy);
                //        }
                //        else
                //            pointy = (float)((240 - h.V) * 1 + YY) + y;
                //        if (!CGUAN)
                //        {
                //            e.Graphics.DrawEllipse(Pens.DarkCyan, pointx - 2, pointy - 2, 5, 5);
                //            if (px != 0)
                //                e.Graphics.DrawLine(Pens.DarkCyan, new PointF(px, py), new PointF(pointx, pointy));

                //        }
                //        if (CGUAN && !BGUAN)
                //        {
                //            if (cgTime < h.D)
                //            {
                //                float xPlus = pointx - px; // 描点作图
                //                PointF p1 = new PointF(px, py);
                //                PointF p2 = new PointF(pointx - 2 * xPlus / 3, pointy + 6);
                //                PointF p3 = new PointF(pointx - xPlus / 3, pointy - 6);
                //                PointF p4 = new PointF(pointx, pointy);
                //                e.Graphics.DrawBezier(Pens.DarkCyan, p1, p2, p3, p4);

                //            }
                //            else
                //                e.Graphics.DrawEllipse(Pens.DarkCyan, pointx - 2, pointy - 2, 5, 5);
                //            if (px != 0 && (cgTime > h.D))
                //                e.Graphics.DrawLine(Pens.DarkCyan, new PointF(px, py), new PointF(pointx, pointy));

                //        }
                //        if (BGUAN)
                //        {
                //            if (cgTime < h.D && h.D < bgTime && px > 120)
                //            {
                //                float xPlus = pointx - px; // 描点作图
                //                PointF p1 = new PointF(px, py);
                //                PointF p2 = new PointF(pointx - 2 * xPlus / 3, pointy + 6);
                //                PointF p3 = new PointF(pointx - xPlus / 3, pointy - 6);
                //                PointF p4 = new PointF(pointx, pointy);
                //                e.Graphics.DrawBezier(Pens.DarkCyan, p1, p2, p3, p4);

                //            }
                //            else
                //                e.Graphics.DrawEllipse(Pens.DarkCyan, pointx - 2, pointy - 2, 5, 5);
                //            if (px != 0 && (cgTime > h.D || bgTime < h.D))
                //                e.Graphics.DrawLine(Pens.DarkCyan, new PointF(px, py), new PointF(pointx, pointy));

                //        }
                //        px = pointx;
                //        py = pointy;
                //    }
                //}
                    #endregion

                    #region  //打印ETCO2
                    px = 0; py = 0;
                    //foreach (adims_MODEL.point p in etco2List)
                    //{
                    //    if (p.D >= pagetime && p.D <= pagetime.AddMinutes(60 * jcsjjg))
                    //    {
                    //        TimeSpan t = new TimeSpan();
                    //        t = p.D - pagetime;
                    //        float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 112);
                    //        float pointy = 0;
                    //        if (p.V > 230)
                    //        {
                    //            pointy = (float)((20) * 1 + YY);
                    //            e.Graphics.DrawString(p.V.ToString(), ptzt7, Brushes.Blue, pointx + x, pointy + y);
                    //        }
                    //        else
                    //            pointy = (float)((240 - p.V) * 1 + YY);

                    //        e.Graphics.DrawPolygon(Pens.DarkOrange, new PointF[3] { new PointF(pointx+ x, pointy+ y), 
                    //                       new PointF(pointx - 3+ x, pointy - 5+ y), new PointF(pointx + 3+ x, pointy - 5+ y) });
                    //        // e.Graphics.FillPolygon(Brushes.Green, new PointF[3] { new PointF(pointx+ x, pointy+ y), 
                    //        //         new PointF(pointx + 3+ x, pointy + 6+ y), new PointF(pointx - 3+ x, pointy + 6+ y) });

                    //        if (px != 0)
                    //            e.Graphics.DrawLine(Pens.DarkOrange, new PointF(px + x, py + y), new PointF(pointx + x, pointy + y));

                    //        px = pointx;
                    //        py = pointy;
                    //    }

                    //}
                    #endregion

                    //↓标记区域
                    YY = YY + 12 * 20;
                    e.Graphics.DrawLine(ptp, new Point(20 + x, YY), new Point(770 + x, YY));
                    e.Graphics.DrawString("标记", ptzt7, Brushes.Black, new Point(25 + x, YY + 3));

                    #region 打印麻醉，手术，插管
                    if (ssksTime >= pagetime && ssksTime <= pagetime.AddHours(jcsjjg))
                    {
                        TimeSpan T = ssksTime - pagetime;
                        int xxx = (int)((T.Days * 24 * 60 + T.Hours * 60 + T.Minutes) * 12 / jcsjjg + 192 + x);
                        e.Graphics.DrawString("⊙", ptzt8, Brushes.Black, xxx, YY + 3);
                    }
                    if (ssjsTime >= pagetime && ssjsTime < pagetime.AddMinutes(48 * jcsjjg))
                    {
                        TimeSpan T = ssjsTime - pagetime;
                        int xxx = (int)((T.Days * 24 * 60 + T.Hours * 60 + T.Minutes) * 12 / jcsjjg + 192 + x);
                        Image newImage = Properties.Resources.SSJS;
                        e.Graphics.DrawImage(newImage, new Rectangle(xxx, YY + 3, 10, 10));
                    }
                    if (mzksTime >= pagetime && mzksTime <= pagetime.AddHours(jcsjjg))
                    {
                        TimeSpan T = mzksTime - pagetime;
                        int xxx = (int)((T.Days * 24 * 60 + T.Hours * 60 + T.Minutes) * 12 / jcsjjg + 192 + x);
                        e.Graphics.DrawString("Χ", ptzt8, Brushes.Black, xxx, YY + 3);
                    }
                    if (mzjkssjzzTime >= pagetime && mzjkssjzzTime <= pagetime.AddHours(jcsjjg))
                    {
                        TimeSpan T = mzjkssjzzTime - pagetime;
                        int xxx = (int)((T.Days * 24 * 60 + T.Hours * 60 + T.Minutes) * 12 / jcsjjg + 192 + x);
                        e.Graphics.DrawString("Χ2", ptzt8, Brushes.Black, xxx, YY + 3);
                    }
                    if (mzkszgnTime >= pagetime && mzkszgnTime <= pagetime.AddHours(jcsjjg))
                    {
                        TimeSpan T = mzkszgnTime - pagetime;
                        int xxx = (int)((T.Days * 24 * 60 + T.Hours * 60 + T.Minutes) * 12 / jcsjjg + 192 + x);
                        e.Graphics.DrawString("Χ1", ptzt8, Brushes.Black, xxx, YY + 3);
                    }
                    //if (mzjsTime >= pagetime && mzjsTime <= pagetime.AddHours(jcsjjg))
                    //{
                    //    TimeSpan T = mzjsTime - pagetime;
                    //    int xxx = (int)((T.Days * 24 * 60 + T.Hours * 60 + T.Minutes) * 11 / jcsjjg + 115 + x);
                    //    e.Graphics.DrawString("Χ", ptzt8, Brushes.Black, xxx, YY + 3);
                    //}
                    if (cgTime >= pagetime && cgTime <= pagetime.AddHours(jcsjjg))
                    {
                        TimeSpan T = cgTime - pagetime;
                        int xxx = (int)((T.Days * 24 * 60 + T.Hours * 60 + T.Minutes) * 12 / jcsjjg + 192 + x);
                        Image newImage = Properties.Resources.CG;
                        e.Graphics.DrawImage(newImage, new Rectangle(xxx, YY + 3, 10, 10));
                    }
                    if (bgTime >= pagetime && bgTime < pagetime.AddMinutes(48 * jcsjjg))
                    {
                        TimeSpan T = bgTime - pagetime;
                        int xxx = (int)((T.Days * 24 * 60 + T.Hours * 60 + T.Minutes) * 12 / jcsjjg + 192 + x);
                        Image newImage = Properties.Resources.BG;
                        e.Graphics.DrawImage(newImage, new Rectangle(xxx, YY + 3, 10, 10));
                    }

                    #endregion
                   
                    #region 打印用药
                    YY = YY+24;
                    for (int i = 0; i < 20; i++)//画横实线
                    {
                        if (i == 0 || i == 11 || i == 16 || i == 19)//|| i == 20
                            e.Graphics.DrawLine(ptp, new Point(20 + x, YY + 12 * i + y), new Point(770 + x, YY + 12 * i + y));
                        //else if (i == 33)
                        //    e.Graphics.DrawLine(ptp, new Point(35 + x, YY + 12 * i + y), new Point(700 + x, YY + 12 * i + y));
                        else
                            e.Graphics.DrawLine(ptp, new Point(35 + x, YY + 12 * i + y), new Point(770 + x, YY + 12 * i + y));
                    }
                    e.Graphics.DrawLine(ptp, new Point(35 + x, YY + y), new Point(35 + x, YY +19 * 12 + y));

                    for (int i = 0; i < 10; i++)//画竖实线
                    {
                        e.Graphics.DrawLine(ptp, new Point(192 + x + 72 * i, YY - 2 + y), new Point(192 + x + 72 * i, YY + 19 * 12 + y));
                    }
                    for (int i = 1; i < 48; i++)//画竖虚线
                    {
                        e.Graphics.DrawLine(pxuxian, new PointF(192 + x + 12 * i, YY + y), new Point(192 + x + 12 * i, YY + 19 * 12 + y));

                    }
                    for (int i = 0; i < 16; i++)//画竖实线
                    {
                        e.Graphics.DrawLine(ptp, new Point(192 + x + 36 * i, YY + 19 * 12 + y), new Point(192 + x + 36 * i, YY + 19 * 12 + y));
                    }
                   
                    #endregion


                    #region 打印气体
                    e.Graphics.DrawString("O2", ptzt8, Brushes.Black, new Point(35 + x, YY));
                    ArrayList sssQT = new ArrayList();
                    int qti = 0;   //起步位置
                    foreach (adims_MODEL.mzqt mzqt in mzqtList)
                    {
                        if (sssQT.Contains(mzqt.Qtname))
                            qti = qti - 1;
                        //if (!sssQT.Contains(mzqt.Qtname))
                        //    e.Graphics.DrawString(mzqt.Qtname, mzqt.Qtname.Length < 7 ? ptzt6 : ptzt5, Brushes.Black, new PointF(32 + x, YY + qti * 12 + y + 3));

                        if (mzqt.Bz == 2)
                        {
                            TimeSpan t = new TimeSpan();
                            TimeSpan t1 = new TimeSpan();
                            t1 = mzqt.Jssj - pagetime;
                            t = mzqt.Sysj - pagetime;
                            int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 192) + x;
                            int y1 = YY + qti * 12 + 4;

                            int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 12 / jcsjjg + 192) + x;
                            //double qtzongliang = (mzqt.Yl) * Convert.ToDouble((x2 - x1) / 2.5);
                            //e.Graphics.DrawString(Convert.ToInt32(qtzongliang).ToString() + "L", this.Font, Brushes.Blue, new Point(763, y1 - 2));
                            if (x1 > 190 + x && x1 < 780 + x)
                            {
                                e.Graphics.DrawString(mzqt.Yl.ToString() + " " + mzqt.Dw, ptzt6, Brushes.Blue, new Point(x1 + 3, y + y1 - 6));
                                e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1 + y), new Point(x1 - 3, y1 + 6 + y), new Point(x1 + 3, y1 + 6 + y) });
                            }
                            if (x2 > 190 + x && x2 < 780 + x)
                            {
                                e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1 + y), new Point(x2 - 3, y1 + 6 + y), new Point(x2 + 3, y1 + 6 + y) });
                            }
                            if (x1 > 190 + x && x1 < 780 + x && x2 > 190 + x && x2 < 780 + x)
                            {
                                e.Graphics.DrawLine(pred2, new Point(x1, y1 + y + 3), new Point(x2, y1 + y + 3));
                            }
                            if (x1 > 190 + x && x1 < 780 + x && x2 > 780 + x)
                            {
                                e.Graphics.DrawLine(pred2, new Point(x1, y1 + y + 3), new Point(780 + x, y1 + y + 3));
                            }
                            if (x1 < 190 + x && x2 > 190 + x && x2 < 780 + x)
                            {
                                e.Graphics.DrawLine(pred2, new Point(190 + x, y1 + y + 3), new Point(x2, y1 + y + 3));
                            }
                            if (x1 < 190 + x && x2 > 780 + x)
                            {
                                e.Graphics.DrawLine(pred2, new Point(190 + x, y1 + y + 3), new Point(780 + x, y1 + y + 3));
                            }
                        }
                        qti++;
                        sssQT.Add(mzqt.Qtname);
                    }


                    #endregion
                    #region 打印全麻药
                    ArrayList sssYDY = new ArrayList();
                    int ydyi = 1;
                    foreach (adims_MODEL.mzyt mzyt in ydyList) //打印全麻药
                    {
                        if (sssYDY.Contains(mzyt.Ytname))
                            ydyi = ydyi - 1;
                        if (!sssYDY.Contains(mzyt.Ytname))
                            e.Graphics.DrawString(mzyt.Ytname + " " + mzyt.Dw, mzyt.Ytname.Length < 7 ? ptzt7 : ptzt5, Brushes.Black, new PointF(37 + x, YY + ydyi * 12 + y + 3));

                        if (mzyt.Cxyy == false)
                        {
                            TimeSpan t = new TimeSpan();
                            t = mzyt.Sysj - pagetime;
                            int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 192);
                            int y1 = YY + ydyi * 12 + 4;
                            if (x1 > 100 + x && x1 < 700 + x)
                            {
                                e.Graphics.DrawString(mzyt.Yl.ToString(), ptzt7, Brushes.Blue, new Point(x1 + 3 + x, y + y1 - 6));
                                e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), new Point(x1 - 3 + x, y1 + 6 + y), new Point(x1 + 3 + x, y1 + 6 + y) });
                            }
                        }
                        if (mzyt.Cxyy == true)
                        {
                            TimeSpan t = new TimeSpan();
                            TimeSpan t1 = new TimeSpan();
                            t1 = mzyt.Jssj - pagetime;
                            t = mzyt.Sysj - pagetime;
                            int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 192);
                            int y1 = YY + ydyi * 12 + 4;
                            int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 12 / jcsjjg + 192);
                            if (x1 > 190 + x && x1 < 780 + x)
                            {
                                e.Graphics.DrawString(mzyt.Yl.ToString(), ptzt7, Brushes.Blue, new Point(x1 + 3 + x, y + y1 - 6));
                                e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), new Point(x1 - 3 + x, y1 + 6 + y), new Point(x1 + 3 + x, y1 + 6 + y) });
                            }
                            if (x2 > 190 + x && x2 < 780 + x)
                            {
                                e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2 + x, y1 + y), new Point(x2 - 3 + x, y1 + 6 + y), new Point(x2 + 3 + x, y1 + 6 + y) });
                            }
                            if (x1 > 190 + x && x1 < 780 + x && x2 > 190 + x && x2 < 780 + x)
                            {
                                e.Graphics.DrawLine(pred2, new Point(x1 + x, y1 + y + 3), new Point(x2 + x, y1 + y + 3));
                            }
                            if (x1 > 190 + x && x1 < 780 + x && x2 > 780 + x)
                            {
                                e.Graphics.DrawLine(pred2, new Point(x1 + x, y1 + y + 3), new Point(780 + x, y1 + y + 3));
                            }
                            if (x1 < 190 + x && x2 > 120 + x && x2 < 780 + x)
                            {
                                e.Graphics.DrawLine(pred2, new Point(120 + x, y1 + y + 3), new Point(x2 + x, y1 + y + 3));
                            }
                            if (x1 < 190 + x && x2 > 780 + x)
                            {
                                e.Graphics.DrawLine(pred2, new Point(120 + x, y1 + y + 3), new Point(780 + x, y1 + y + 3));
                            }
                        }
                        ydyi++;
                        sssYDY.Add(mzyt.Ytname);
                    }

                    #endregion


                    #region 打印输液
                    int syi = 11;
                    ArrayList sssSY = new ArrayList();
                    foreach (adims_MODEL.shuye sx in shuyeList)
                    {
                        if (sssSY.Contains(sx.Name))
                            syi = syi - 1;
                        if (!sssSY.Contains(sx.Name))
                            e.Graphics.DrawString(sx.Name + " " + sx.Dw, sx.Name.Length < 9 ? ptzt7 : ptzt5, Brushes.Black, new PointF(37 + x, YY + syi * 12 + y + 3));

                        TimeSpan t = new TimeSpan();
                        TimeSpan t1 = new TimeSpan();
                        t1 = pagetime.AddMinutes(60 * jcsjjg) - pagetime;
                        t = sx.Kssj - pagetime;
                        //e.Graphics.DrawString(sx.Name.ToString(), ptzt7, Brushes.Black, 37 + x, YY + syi * 12 + 2);
                        int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 192);
                        int y1 = YY + syi * 12 + 5;
                        int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 12 / jcsjjg + 192);

                        if (x1 > 190 + x && x1 < 780 + x)
                        {
                            e.Graphics.DrawString(sx.Jl.ToString(), ptzt7, Brushes.Blue, x1 + x, y + y1 - 8);
                            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), 
                             new Point(x1 - 3 + x, y1 + 5 + y), new Point(x1 + 3 + x, y1 + 5 + y) });
                        }
                        syi++;
                        sssSY.Add(sx.Name);
                    }

                    #endregion

                    #region 打印输血
                    //打印输血
                    int sxi = 16;
                    ArrayList sssSX = new ArrayList();
                    foreach (adims_MODEL.shuxue sx in shuxueList)
                    {
                        if (sssSX.Contains(sx.Name))
                            sxi = sxi - 1;
                        if (!sssSX.Contains(sx.Name))
                            e.Graphics.DrawString(sx.Name + " " + sx.Dw, sx.Name.Length < 9 ? ptzt7 : ptzt5, Brushes.Black, new PointF(37 + x, YY + sxi * 12 + y + 3));

                        TimeSpan t = new TimeSpan();
                        TimeSpan t1 = new TimeSpan();
                        t1 = pagetime.AddHours(jcsjjg) - pagetime;
                        t = sx.Kssj - pagetime;
                        //e.Graphics.DrawString(sx.Name.ToString(), ptzt7, Brushes.Black, 37 + x, YY + sxi * 12 + 2);
                        int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 192);
                        int y1 = YY + sxi * 12 + 5;
                        int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 12 / jcsjjg + 192);
                        if (x1 > 190 + x && x1 < 780 + x)
                        {
                            e.Graphics.DrawString(sx.Jl.ToString(), ptzt7, Brushes.Blue, x1 + x, y + y1 - 6);
                            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), 
                             new Point(x1 - 3 + x, y1 + 5 + y), new Point(x1 + 3 + x, y1 + 5 + y) });
                        }
                        sxi++;
                        sssSX.Add(sx.Name);
                    }
                    #endregion
                    #region 打印输液输血
                    e.Graphics.DrawString("麻\n\n醉\n\n药", ptzt8, Brushes.Black, new Point(21 + x, YY + 40));
                    e.Graphics.DrawString("输\n\n液", ptzt8, Brushes.Black, new Point(21 + x, YY + 11 * 12 + 10));
                    e.Graphics.DrawString("输\n血", ptzt8, Brushes.Black, new Point(21 + x, YY + 16 * 12 + 5));
                    //e.Graphics.DrawString("     用药序号", ptzt10, Brushes.Black, new Point(25 + x, YY + 51));
                    #endregion


                    YY = YY + 19 * 12; Y_unLine = YY + 13;
                   #region 打印特殊用药


                    int szi = 1;
                    int szi1 = 1;
                    int tsn1 = 0;
                    string szss1 = "";
                    //显示事件标记
                    foreach (adims_MODEL.szsj sz in szsjList)
                    {
                        if (sz.D >= pagetime && sz.D <= pagetime.AddMinutes(48 * jcsjjg))
                        {
                            TimeSpan t = new TimeSpan();
                            t = sz.D - pagetime;
                            float tx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 192) + x;
                            e.Graphics.FillRectangle(Brushes.Pink, tx - 3, YY + 3 - 450, 9, 9);
                            //e.Graphics.FillRectangle(Brushes.Pink, tx - 3, BJYY + 8 + (sz.Y_zb - 315), 9, 9);
                            e.Graphics.DrawString(szi.ToString(), ptzt7, Brushes.Black, new PointF(tx - 3, YY + 3 - 450));
                            szi++;
                        }

                    }
                    //显示事件的类容
                    foreach (adims_MODEL.szsj sz in szsjList)
                    {
                        if (szi1 + tsn1 > 11)
                        {
                            break;
                        }
                        szss1 = szi1.ToString() + "." + sz.Name + " " + sz.D.ToString("HH:mm:ss") + "\n";
                        if (szi1 + tsn1 > 12)
                        {
                            string strSS1 = "";
                            int StrLengthSS = szss1.Trim().Length;//1//2
                            int rowSS = StrLengthSS / 20;
                            if (StrLengthSS < 21)
                            {
                                for (int i = 0; i <= rowSS; i++)//40个字符就换行
                                {

                                    if (i < rowSS)
                                        strSS1 = szss1.Trim().ToString().Substring(i * 20, 20); //从第i*40个开始，截取40个字符串
                                    else
                                        strSS1 = szss1.Trim().ToString().Substring(i * 20);

                                    e.Graphics.DrawString(strSS1, ptzt9, Brushes.Black, new PointF(220 + x, YY + 2 + (szi1 + tsn1 - i - 9) * 14));
                                }
                            }
                            else
                            {

                                for (int i = 0; i <= rowSS; i++)//40个字符就换行
                                {

                                    if (i < rowSS)
                                    {
                                        strSS1 = szss1.Trim().ToString().Substring(i * 20, 20); //从第i*40个开始，截取40个字符串
                                        e.Graphics.DrawString(strSS1, ptzt9, Brushes.Black, new PointF(220 + x, YY + 2 + (szi1 + tsn1 - i - 9) * 14));
                                    }
                                    else
                                    {
                                        strSS1 = szss1.Trim().ToString().Substring(i * 20);
                                        if (szi1 + tsn1 + i > 16)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            e.Graphics.DrawString(strSS1, ptzt9, Brushes.Black, new PointF(220 + x, YY + 2 + (szi1 + tsn1 + i - 9) * 14));
                                        }
                                    }
                                    //if（tsi1+tsn > 8）
                                    //{
                                    //e.Graphics.DrawString(strSS1, ptzt9, Brushes.Black, new PointF(440 + x, YY + 2 + (tsi1 - 8) * 14));//这里开始做
                                    //}
                                    //else
                                    // e.Graphics.DrawString(strSS1, ptzt9, Brushes.Black, new PointF(430 + x, YY + 27 + (tsi1 + tsn + i - 1) * 14));

                                }
                                tsn1++;
                            }

                        }
                        else
                        {

                            string strSS1 = "";
                            int StrLengthSS = szss1.Trim().Length;//1//2
                            int rowSS = StrLengthSS / 20;
                            if (StrLengthSS < 21)
                            {
                                for (int i = 0; i <= rowSS; i++)//40个字符就换行
                                {

                                    if (i < rowSS)
                                        strSS1 = szss1.Trim().ToString().Substring(i * 20, 20); //从第i*40个开始，截取40个字符串
                                    else
                                        strSS1 = szss1.Trim().ToString().Substring(i * 20);

                                    e.Graphics.DrawString(strSS1, ptzt9, Brushes.Black, new PointF(40 + x, YY + 2 + (szi1 + tsn1 - 1) * 14));
                                }
                            }
                            else
                            {

                                for (int i = 0; i <= rowSS; i++)//40个字符就换行
                                {

                                    if (i < rowSS)
                                    {
                                        strSS1 = szss1.Trim().ToString().Substring(i * 20, 20); //从第i*40个开始，截取40个字符串
                                        e.Graphics.DrawString(strSS1, ptzt9, Brushes.Black, new PointF(40 + x, YY + 2 + (szi1 + tsn1 - 1) * 14));
                                    }
                                    else
                                    {
                                        strSS1 = szss1.Trim().ToString().Substring(i * 20);
                                        if (szi1 + tsn1 + i > 8)
                                        {
                                            e.Graphics.DrawString(strSS1, ptzt9, Brushes.Black, new PointF(220 + x, YY +2+ (szi1 + tsn1 - 8) * 14));//这里开始做

                                        }
                                        else
                                        {
                                            e.Graphics.DrawString(strSS1, ptzt9, Brushes.Black, new PointF(40 + x, YY + 2 + (szi1 + tsn1 + i - 1) * 14));
                                        }
                                    }
                                    //if（tsi1+tsn > 8）
                                    //{
                                    //e.Graphics.DrawString(strSS1, ptzt9, Brushes.Black, new PointF(440 + x, YY + 2 + (tsi1 - 8) * 14));//这里开始做
                                    //}
                                    //else
                                    // e.Graphics.DrawString(strSS1, ptzt9, Brushes.Black, new PointF(430 + x, YY + 27 + (tsi1 + tsn + i - 1) * 14));
                                }
                                tsn1++;
                            }
                        }
                        szi1++;
                    }

                    //显示其他用药

                    //显示其他用药
                    int tsi = 1;
                    int tsi1 = 1;
                    int tsn = 0;
                    string tss1 = "";
                    foreach (adims_MODEL.tsyy ts in tsyyList)
                    {
                        if (ts.D >= pagetime && ts.D <= pagetime.AddMinutes(48 * jcsjjg))
                        {
                            TimeSpan t = new TimeSpan();
                            t = ts.D - pagetime;
                            float tx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12 / jcsjjg + 192) + x;
                            //e.Graphics.FillEllipse(Brushes.LightGreen, tx - 3, BJYY + 3 + (ts.Y_zb - 370), 10, 9);
                            e.Graphics.DrawString(tsi.ToString(), ptzt7, Brushes.Blue, new PointF(tx - 3, YY-250 + 3));
                            tsi++;
                        }
                    }
                    foreach (adims_MODEL.tsyy ts in tsyyList)
                    {
                        if (tsi1 >11)
                        {
                            break;
                        }
                        //string zsfs = "";//注射方式
                        // if (ts.Yyfs == "口服")
                        //{
                        //    zsfs = "po";
                        //}
                        //if (ts.Yyfs == "静脉滴注")
                        //{
                        //    zsfs = "ivdrip";
                        //}
                        //if (ts.Yyfs == "皮下注射")
                        //{
                        //    zsfs = "ih";
                        //}
                        //if (ts.Yyfs == "肌肉注射")
                        //{
                        //    zsfs = "im";
                        //}
                        //if (ts.Yyfs == "静脉注射")
                        //{
                        //    zsfs = "iv";
                        //}
                        //if (ts.Yyfs == "皮下注射")
                        //{
                        //    zsfs = "id";
                        //}
                        if (ts.Name.Contains("+"))
                        {
                            tss1 = tsi1.ToString() + ".[" + ts.Name + "" + ts.Yl + "" + ts.Dw + " ]" + ts.Yyfs + " " + ts.D.ToString("HH:mm") + "\n";
                        }
                        else
                        {
                            tss1 = tsi1.ToString() + "." + ts.Name + "" + ts.Yl + "" + ts.Dw + " " + ts.Yyfs + " " + ts.D.ToString("HH:mm") + "\n";
                        }
                        //string strSS1 = "";
                        //int StrLengthSS = tss1.Trim().Length;//1//2
                        //int rowSS = StrLengthSS / 18;

                        e.Graphics.DrawString(tss1, ptzt9, Brushes.Black, new PointF(320 + x, YY +2+ (tsi1 - 1) * 14));




                        tsi1++;
                    }

                    for (int i = 0; i < listBox3.Items.Count; i++)//打印镇痛药
                    {
                        if (i < 11)
                        {
                            e.Graphics.DrawString((listBox3.Items[i].ToString()),
                                ptzt7, Brushes.Black, new Point(x +575, YY +2+ i * 14));
                        }
                        if (i >=11)
                        {
                            break;
                        }


                    }
                    #endregion

                  

                    #region 打印尾部区域↓
                    //YY = YY + 70; Y_unLine = YY + 19;
                    //e.Graphics.DrawLine(ptp, new Point(20 + x, YY), new Point(770 + x, YY));
                    //e.Graphics.DrawString("备注：" + txtRemark.Controls[0].Text, ptzt7, Brushes.Black, new Point(25 + x, YY + 3));
                    //e.Graphics.DrawLine(ptp, new Point(50 + x, Y_unLine), new Point(755 + x, Y_unLine));
                   
                    e.Graphics.DrawLine(ptp, new Point(20 + x, YY), new Point(770 + x, YY));
                    e.Graphics.DrawLine(ptp, new Point(40 + x, YY), new Point(40 + x, YY + 160));
                    e.Graphics.DrawLine(ptp, new Point(300 + x, YY), new Point(300 + x, YY + 160));
                    e.Graphics.DrawLine(ptp, new Point(320 + x, YY), new Point(320 + x, YY + 160));
                    e.Graphics.DrawLine(ptp, new Point(555 + x, YY), new Point(555 + x, YY + 160));
                    e.Graphics.DrawLine(ptp, new Point(575 + x, YY), new Point(575 + x, YY + 160));
                    e.Graphics.DrawLine(ptp, new Point(20 + x, YY + 160), new Point(770 + x, YY + 160));
                    e.Graphics.DrawString("术\n\n中\n\n事\n\n件", ptzt11, Brushes.Black, new Point(x + 20, YY));
                    e.Graphics.DrawString("术\n\n中\n\n用\n\n药", ptzt11, Brushes.Black, new Point(300 + x, YY));
                    e.Graphics.DrawString("\n\n镇\n\n痛\n\n药", ptzt11, Brushes.Black, new Point(555 + x, YY));
                    YY = YY + 170; Y_unLine = YY + 14;
                    e.Graphics.DrawString("手术后诊断 " + txtsshzd.Controls[0].Text, ptzt8, Brushes.Black, 25 + x, YY);//诊断
                    e.Graphics.DrawLine(pxuxian, new Point(70 + x, Y_unLine), new Point(500 + x, Y_unLine));
                    e.Graphics.DrawString("PAC   :  " + cmbzhentongy.Text, ptzt8, Brushes.Black, 510 + x, YY);
                    e.Graphics.DrawLine(pxuxian, new Point(560 + x, Y_unLine), new Point(600 + x, Y_unLine));
                    e.Graphics.DrawString("自 体 血 : " + txtzitixue.Controls[0].Text, ptzt8, Brushes.Black, 610 + x, YY);
                    e.Graphics.DrawLine(pxuxian, new Point(670 + x, Y_unLine), new Point(720 + x, Y_unLine));
                    e.Graphics.DrawString("毫升", ptzt8, Brushes.Black, 720 + x, YY);
                    e.Graphics.DrawString("成分输血 : " + txtcfsx.Controls[0].Text, ptzt8, Brushes.Black, 610 + x, YY + 15);
                    e.Graphics.DrawLine(pxuxian, new Point(670 + x, Y_unLine + 15), new Point(720 + x, Y_unLine + 15));
                    e.Graphics.DrawString("毫升", ptzt8, Brushes.Black, 720 + x, YY + 15);
                    e.Graphics.DrawString("胶 体 液 : " + txtjiaotixue.Controls[0].Text, ptzt8, Brushes.Black, 610 + x, YY + 30);
                    e.Graphics.DrawLine(pxuxian, new Point(670 + x, Y_unLine + 30), new Point(720 + x, Y_unLine + 30));
                    e.Graphics.DrawString("毫升", ptzt8, Brushes.Black, 720 + x, YY + 30);
                    e.Graphics.DrawString("晶 体 液 : " + txtjingtixue.Controls[0].Text, ptzt8, Brushes.Black, 610 + x, YY + 45);
                    e.Graphics.DrawLine(pxuxian, new Point(670 + x, Y_unLine + 45), new Point(720 + x, Y_unLine + 45));
                    e.Graphics.DrawString("毫升", ptzt8, Brushes.Black, 720 + x, YY + 45);
                    e.Graphics.DrawString("总输入量 : " + txtzongsrl.Controls[0].Text, ptzt8, Brushes.Black, 610 + x, YY + 60);
                    e.Graphics.DrawLine(pxuxian, new Point(670 + x, Y_unLine+60), new Point(720 + x, Y_unLine+60));
                    e.Graphics.DrawString("毫升", ptzt8, Brushes.Black, 720 + x, YY +60);
                    e.Graphics.DrawString("出 血 量:  " + txtchuxuel.Controls[0].Text, ptzt8, Brushes.Black, 610 + x, YY + 75);
                    e.Graphics.DrawLine(pxuxian, new Point(670 + x, Y_unLine+75), new Point(720 + x, Y_unLine+75));
                    e.Graphics.DrawString("毫升", ptzt8, Brushes.Black, 720 + x, YY +75);
                    e.Graphics.DrawString("尿    量:  " + txtniaoliang.Controls[0].Text, ptzt8, Brushes.Black, 610 + x, YY + 90);
                    e.Graphics.DrawLine(pxuxian, new Point(670 + x, Y_unLine+90), new Point(720 + x, Y_unLine+90));
                    e.Graphics.DrawString("毫升", ptzt8, Brushes.Black, 720 + x, YY+90);
                    YY = YY + 30; Y_unLine = YY + 14;
                    e.Graphics.DrawString("手术方式 " + txtShoushuFS.Controls[0].Text, ptzt8, Brushes.Black, 25 + x, YY);
                    e.Graphics.DrawLine(pxuxian, new Point(70 + x, Y_unLine), new Point(500 + x, Y_unLine));
                    e.Graphics.DrawString("麻醉效果 " + cmbmazuixiaoguo.Text, ptzt8, Brushes.Black, 510 + x, YY);
                    e.Graphics.DrawLine(pxuxian, new Point(560 + x, Y_unLine), new Point(600 + x, Y_unLine));
                   
                    YY = YY + 30; Y_unLine = YY + 14;
                    e.Graphics.DrawString("麻醉方式 " + txtMazuiFS.Controls[0].Text, ptzt8, Brushes.Black, 25 + x, YY);
                    e.Graphics.DrawLine(pxuxian, new Point(70 + x, Y_unLine), new Point(300 + x, Y_unLine));
                    e.Graphics.DrawString("    麻醉医师    " + txtMZYS.Controls[0].Text, ptzt8, Brushes.Black, 300 + x, YY);
                    e.Graphics.DrawLine(pxuxian, new Point(380 + x, Y_unLine), new Point(500 + x, Y_unLine));
                    e.Graphics.DrawString("病人送往 " + cmbBRQX.Text, ptzt8, Brushes.Black, 510 + x, YY);
                    e.Graphics.DrawLine(pxuxian, new Point(560 + x, Y_unLine), new Point(600 + x, Y_unLine));
                    YY = 30 + YY; Y_unLine = YY + 14;
                    e.Graphics.DrawString("手术医师 " + txtSSYS.Controls[0].Text, ptzt8, Brushes.Black, 25 + x, YY);
                    e.Graphics.DrawLine(pxuxian, new Point(70 + x, Y_unLine), new Point(300 + x, Y_unLine));
                    e.Graphics.DrawString("器械、巡回护士 " + txtXHHS.Controls[0].Text, ptzt8, Brushes.Black, 300 + x, YY);
                    e.Graphics.DrawLine(pxuxian, new Point(380 + x, Y_unLine), new Point(500 + x, Y_unLine));
                    e.Graphics.DrawString("评    分： " + txtmazuipingfen.Controls[0].Text, ptzt8, Brushes.Black, 510 + x, YY);
                    e.Graphics.DrawLine(pxuxian, new Point(560 + x, Y_unLine), new Point(600 + x, Y_unLine));
                    //e.Graphics.DrawString("麻醉医师 " + txtMZYS.Controls[0].Text, ptzt8, Brushes.Black, 425 + x, YY);
                    //e.Graphics.DrawLine(pxuxian, new Point(470 + x, Y_unLine), new Point(770 + x, Y_unLine));
                    //YY = 30 + YY; Y_unLine = YY + 14;
                    //e.Graphics.DrawString("器械护士 " + txtQXHS.Controls[0].Text, ptzt8, Brushes.Black, 25 + x, YY);
                    //e.Graphics.DrawLine(pxuxian, new Point(70 + x, Y_unLine), new Point(400 + x, Y_unLine));
                    //e.Graphics.DrawString("巡回护士 " + txtXHHS.Controls[0].Text, ptzt8, Brushes.Black, 425 + x, YY);
                    //e.Graphics.DrawLine(pxuxian, new Point(470 + x, Y_unLine), new Point(770 + x, Y_unLine));
                    //YY = 25 + YY; Y_unLine = YY + 14;
                    //e.Graphics.DrawString("切口类型 " + cmbBRQX.Text, ptzt8, Brushes.Black, 25 + x, YY);
                    //e.Graphics.DrawLine(ptp, new Point(70 + x, Y_unLine), new Point(200 + x, Y_unLine));
                    //e.Graphics.DrawString("切口数量 " + cmbBRQX.Text, ptzt8, Brushes.Black, 210 + x, YY);
                    //e.Graphics.DrawLine(ptp, new Point(255 + x, Y_unLine), new Point(350 + x, Y_unLine));
                    //e.Graphics.DrawString("病人去向 " + cmbBRQX.Text, ptzt8, Brushes.Black, 360 + x, YY);
                    //e.Graphics.DrawLine(ptp, new Point(405 + x, Y_unLine), new Point(530 + x, Y_unLine));
                }
                    #endregion


            
        }


        private void printDocument2_EndPrint(object sender, PrintEventArgs e)
        {
            iYema = 1;
            fy = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int JsFlag = 0;
            string YwName = string.Empty;
            foreach (adims_MODEL.mzqt qt in mzqtList)//判断气体药是否结束
            {
                if (qt.Bz == 1)
                {
                    YwName = YwName + qt.Qtname + "\n";
                    JsFlag++;
                }

            }
            foreach (adims_MODEL.mzyt ydy in ydyList) //判断诱导药是否结束
            {
                if (ydy.Bz == 1 && ydy.Cxyy)
                {
                    YwName = YwName + ydy.Ytname + "\n";
                    JsFlag++;
                }
            }
            if (JsFlag > 0)
            {
                MessageBox.Show(YwName + "没用结束，不能打印");
                return;

            }
            ptime = fristopen;
            PrintDialog pd = new PrintDialog();

            pd.Document = printDocument;

            if (DialogResult.OK == pd.ShowDialog()) //如果确认，将会覆盖所有的打印参数设置
            {
                //页面设置对话框（可以不使用，其实PrintDialog对话框已提供页面设置）
                PageSetupDialog psd = new PageSetupDialog();
                psd.Document = printDocument;
                // this.pageSetupDialog1.ShowDialog();
                //BindJHDian();
                //pictureBox4.Refresh();
                //pictureBox3.Refresh();
                //pictureBox2.Refresh();

                if (printPreviewDialog2.ShowDialog() == DialogResult.OK)
                {
                    printDocument2.Print();
                    //printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
                    //printPreviewDialog1.WindowState = FormWindowState.Maximized;
                    //printDocument1.DefaultPageSettings.PaperSize =
                    //           new System.Drawing.Printing.PaperSize("K16", 740, 1020);
                }
                //if (printPreviewDialog1.ShowDialog() == DialogResult.OK) 
                //    printDocument1.Print();
            }
            iYema = 1;
        }
      

       
    }
}

