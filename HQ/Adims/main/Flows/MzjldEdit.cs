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
using adims_BLL;
//using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using adims_DAL.Dics;
using adims_DAL.Flows;
using adims_Utility;

namespace main
{
    public partial class MzjldEdit : Form, IMessageFilter
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
        int PR = 0; bool pr_effective = false;    //脉率
        int RR = 0; bool rr_effective = false;    //呼吸频率
        int SPO2 = 0; bool spo2_effective = false;//血氧饱和度
        float TEMP1 = 0; bool temp_effective = false;//温度1
        float TEMP2 = 0; //温度2
        int ETCO2 = 0;
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

        #region <<Members>>
        YongyaoListDal _YongyaoListDal = new YongyaoListDal();
        MzjldDal _MzjldDal = new MzjldDal();
        OperScheduleDal _PaibanDal = new OperScheduleDal();
        BeforeVisitDal _BeforeVisitDal = new BeforeVisitDal();
        DateTime FirstOpen = new DateTime();//第一页打印开始时间
        DateTime ptime = new DateTime();//每页打印开始时间
        /// <summary>
        /// 打印页码
        /// </summary>
        int iYema = 1;
        /// <summary>
        /// 打印术中事件序号
        /// </summary>
        int PrintSzsjIndex = 0;
        DataDicDal _DataDicDal = new DataDicDal();
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_MODEL.paiban pat = new adims_MODEL.paiban();
        Label LableMZKS = new Label();//麻醉开始lable
        Label LableMZJS = new Label();//麻醉结束lable
        Label LableSSKS = new Label();//手术开始lable
        Label LableSSJS = new Label();//手术结束lable
        Label LableCG = new Label();//插管lable
        Label LableBG = new Label();//拔管lable

        //麻醉开始，结束，手术开始，结束标志；插管，拔管标志                             
        bool IsMzStart = false;
        bool IsMzEnd = true;
        bool IsOperStart = false;
        bool IsOperEnd = true;
        bool IsBg = false;
        bool IsCg = false;

        DateTime ssksTime = new DateTime();//手术开始事件
        DateTime ssjsTime = new DateTime();//手术结束事件
        DateTime mzksTime = new DateTime();//麻醉开始时间
        DateTime mzjsTime = new DateTime();//麻醉结束时间
        DateTime _CgTime = new DateTime();//插管时间
        DateTime _BgTime = new DateTime();//拔管时间
        DateTime jkksTime = new DateTime();//机控呼吸开始时间
        DateTime jkjsTime = new DateTime();//机控呼吸结束时间
        DateTime fzksTime = new DateTime();//辅助呼吸开始时间
        DateTime fzjsTime = new DateTime();//辅助呼吸结束时间
        DateTime ksjcTime = new DateTime();//开始检测时间参数
        public int _MzjldId = 0;
        string patID = "";
        string Oroom = "";
        int flagP2 = 0, typeP2 = 0;//在pictureBox2上的选中标志，和选中类型

        float p2x = 0, p2y = 0;//鼠标在picturebox2上的位置
        float p3x = 0, p3y = 0;//鼠标在picturebox3上的位置
        int flagP3 = 0, typeP3 = 0;
        int ssmzcgbgFlag = 0;//手术麻醉插管拔管鼠标按下是否选中标志 

        adims_MODEL.point t_point = new adims_MODEL.point();//单个point模板       
        adims_MODEL.tw_point tw_point = new adims_MODEL.tw_point();//体温模板
        private List<string> jhxmAll = new List<string>();//所有备选监护项目
        private List<string> jhxmIn = new List<string>();//已添加的监护项目
        adims_MODEL.Yongyao t_mzqt = new adims_MODEL.Yongyao();//单个气体药模板
        adims_MODEL.Yongyao t_shuye = new adims_MODEL.Yongyao();//单个输液模板
        adims_MODEL.Yongyao t_shuxue = new adims_MODEL.Yongyao();//单个输血模板
        adims_MODEL.mzpingmian t_mzpm = new adims_MODEL.mzpingmian(); //单个麻醉平面模板
        adims_MODEL.Yongyao t_ydy = new adims_MODEL.Yongyao(); //单个诱导药模板
        adims_MODEL.Yongyao t_jmy = new adims_MODEL.Yongyao(); //单个局麻药模板
        adims_MODEL.Yongyao t_tsyy = new adims_MODEL.Yongyao();//单个特殊用药模板
        adims_MODEL.szsj t_szsj = new adims_MODEL.szsj();//单个术中事件模板
        adims_MODEL.clcxqt t_chuniao = new adims_MODEL.clcxqt();//单个出尿模板
        public List<adims_MODEL.point> ssyList = new List<adims_MODEL.point>();//收缩点集合
        public List<adims_MODEL.point> szyList = new List<adims_MODEL.point>();//舒张压点集合
        public List<adims_MODEL.point> xlList = new List<adims_MODEL.point>();//心率点集合
        public List<adims_MODEL.tw_point> twList = new List<adims_MODEL.tw_point>();//体温点集合
        public List<adims_MODEL.point> mboList = new List<adims_MODEL.point>();//脉搏点集合
        public List<adims_MODEL.point> hxlList = new List<adims_MODEL.point>();//呼吸率点集合
        public List<adims_MODEL.point> spo2List = new List<adims_MODEL.point>();//spo2集合
        public List<adims_MODEL.point> etco2List = new List<adims_MODEL.point>();//etco2集合
        public List<adims_MODEL.point> cvpList = new List<adims_MODEL.point>();//cvp集合
        public List<adims_MODEL.point> tofList = new List<adims_MODEL.point>();//tof集合
        private List<adims_MODEL.jhxm> jhxmValue = new List<adims_MODEL.jhxm>();//监护项目值
        Point lastpoint = new Point();//画线的时候保存上一个点
        Point lastpoint1 = new Point();//画线的时候保存上一个点
        Point lastpoint2 = new Point();//画线的时候保存上一个点
        Point lastpoint3 = new Point();//画线的时候保存上一个点
        Point lastpoint4 = new Point();//画线的时候保存上一个点
        Point lastpoint5 = new Point();//画线的时候保存上一个点 
        Point lastpoint6 = new Point();//画线的时候保存上一个点  
        Point lastpoint7 = new Point();//画线的时候保存上一个点  
        Point lastpoint8 = new Point();//画线的时候保存上一个点  
        Point lastpoint9 = new Point();//画线的时候保存上一个点  
        Point lastpoint10 = new Point();//画线的时候保存上一个点  
        List<ZoomRegion> ZoomRegionList = new List<ZoomRegion>();//分段时间设置
        List<ZoomRegion> ZoomRegionListPrint = new List<ZoomRegion>();//分段时间设置打印
        string ZoomStr = "";
        DateTime otime = new DateTime();//画图起始时间
        DateTime InRoomTime = new DateTime();//入室时间
        double syZongl = 0, sxZongl = 0;
        List<adims_MODEL.mzpingmian> mzpmList = new List<adims_MODEL.mzpingmian>();//麻醉平面
        List<adims_MODEL.Yongyao> mzqtList = new List<adims_MODEL.Yongyao>();//气体药集合
        List<adims_MODEL.Yongyao> shuxueList = new List<adims_MODEL.Yongyao>(); //输血集合    
        List<adims_MODEL.Yongyao> shuyeList = new List<adims_MODEL.Yongyao>();//输液集合
        List<adims_MODEL.Yongyao> jmyList = new List<adims_MODEL.Yongyao>();//局部麻醉药集合
        List<adims_MODEL.Yongyao> ydyList = new List<adims_MODEL.Yongyao>();//诱导药集合
        List<adims_MODEL.Yongyao> tsyyList = new List<adims_MODEL.Yongyao>();//特殊用药集合
        List<adims_MODEL.Yongyao> ztyList = new List<adims_MODEL.Yongyao>();//镇痛药
        List<adims_MODEL.clcxqt> chuniaoList = new List<adims_MODEL.clcxqt>();//出尿出血其他出量

        List<adims_MODEL.szsj> szsjList = new List<adims_MODEL.szsj>();//术中事件集合
        int _MonitorInterval = 5;//检测时间间隔分钟数
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
        public MzjldEdit(string patid, string oroom, DateTime dt, int mzid, bool sjllState)
        {
            odate = dt;
            isSjll = sjllState;
            Oroom = oroom;
            _MzjldId = mzid;
            this.patID = patid;
            InitializeComponent();
            GetConfigure();
            PatientIPAddress1.Text = IPAddressInput1;
            //BedNumber1.Text = BedIDInput1;
            phillip_server = new IPEndPoint(IPAddress.Parse(PatientIPAddress1.Text.Trim()), 24105);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Remote = new IPEndPoint(IPAddress.Any, 8001);
            phillip = (EndPoint)Remote;
        }
        public MzjldEdit(string patid, string oroom, DateTime dt, int mzid)
        {
            odate = dt;
            _MzjldId = mzid;
            Oroom = oroom;
            this.patID = patid;
            InitializeComponent();
            GetConfigure();
            PatientIPAddress1.Text = IPAddressInput1;
            //BedNumber1.Text = BedIDInput1;
            phillip_server = new IPEndPoint(IPAddress.Parse(PatientIPAddress1.Text.Trim()), 24105);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Remote = new IPEndPoint(IPAddress.Any, 8001);
            phillip = (EndPoint)Remote;
        }

        public MzjldEdit(string patID)
        {
            this.patID = patID;
            InitializeComponent();
            GetConfigure();
            PatientIPAddress1.Text = IPAddressInput1;
            //BedNumber1.Text = BedIDInput1;
            phillip_server = new IPEndPoint(IPAddress.Parse(PatientIPAddress1.Text.Trim()), 24105);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Remote = new IPEndPoint(IPAddress.Any, 8001);
            phillip = (EndPoint)Remote;
        }
        public MzjldEdit(string _MzjldId, string patID)
        {
            this._MzjldId = Convert.ToInt32(_MzjldId);
            this.patID = patID;
            InitializeComponent();
            GetConfigure();
            PatientIPAddress1.Text = IPAddressInput1;
            //BedNumber1.Text = BedIDInput1;
            phillip_server = new IPEndPoint(IPAddress.Parse(PatientIPAddress1.Text.Trim()), 24105);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Remote = new IPEndPoint(IPAddress.Any, 8001);
            phillip = (EndPoint)Remote;
        }
        #endregion

        private void timer3_Tick(object sender, EventArgs e)
        {
            labVisibleTimer.Interval = 2500;
            lab1.Visible = false;
            lab2.Visible = false;
            labVisibleTimer.Enabled = false;
        }
        private void btnSaveOtime_Click(object sender, EventArgs e)
        {

            int i = dal.UpdateMzjld(dtOtime.Value.ToString("yyyy-MM-dd HH:mm"), _MzjldId, 1);
            if (i == 0) MessageBox.Show("入室时间修改失败！");
            else
            {
                otime = Convert.ToDateTime(dtOtime.Value.ToString("yyyy-MM-dd HH:mm"));
                InRoomTime = Convert.ToDateTime(dtOtime.Value.ToString("yyyy-MM-dd HH:mm"));
                BindZoomRegionList();
                BindJHDian();
                BindZoomRegionListPrint(otime);
                FirstOpen = otime;
                BindShijiandian();
                pictureBox2.Refresh();
                pictureBox3.Refresh();
                pictureBox4.Refresh();
                BindMZSSCGBG();

            }
        }
        private void cmbSJJG_SelectedIndexChanged(object sender, EventArgs e)
        {

            int i = dal.UpdateMzjld(cmbSJJG.Text.Trim(), _MzjldId, 2);
            if (i > 0)
            {
                BindShijiandian();
                BindJHDian();
                BindMZSSCGBG();
                pictureBox2.Refresh();
                pictureBox3.Refresh();
                pictureBox4.Refresh();

            }
        }

        private void BindShijiandian()
        {
            FirstOpen = otime;
            BindZoomRegionList();
            BindZoomRegionListPrint(otime);
            jhxmAll.Clear();
            //jhxmAll.Add("ECG");
            jhxmAll.Add("SpO2");
            jhxmAll.Add("CVP");
            //jhxmAll.Add("NIBP");
            //jhxmAll.Add("ART");
            //jhxmAll.Add("RESP");
            //jhxmAll.Add("BIS");
            //jhxmAll.Add("TOF");
            jhxmAll.Add("ETCO2");
            jhxmAll.Add("深度值");
            jhxmAll.Add("肌松值");
            jhxmAll.Add("气道压");
            _MonitorInterval = Convert.ToInt32(cmbSJJG.Text.Trim());

        }

        private void BindMzjldBacicInfo()
        {
            bingSQFS();//术前访视的数据
            DataTable dtMzjld = _MzjldDal.GetMzjldByMzjldId(_MzjldId);
            if (Convert.ToString(dtMzjld.Rows[0]["Height"]) != "")
            {
                txtHeight.Text = Convert.ToString(dtMzjld.Rows[0]["Height"]);
            }
            if (Convert.ToString(dtMzjld.Rows[0]["Weight"]) != "")
            {

                txtWeight.Text = Convert.ToString(dtMzjld.Rows[0]["Weight"]);
            }
            if (dtMzjld.Rows[0]["sqyy"].ToString() != "")
            {
                txtSqyy.Text = Convert.ToString(dtMzjld.Rows[0]["sqyy"]);
            }
            if (dtMzjld.Rows[0]["sqzd"].ToString() != "")
            {
                txtSqzd.Text = Convert.ToString(dtMzjld.Rows[0]["sqzd"]);
            }
            txtTSBQing.Text = Convert.ToString(dtMzjld.Rows[0]["tsbq"]);
            tbChuNiao.Text = Convert.ToString(dtMzjld.Rows[0]["niaoliang"]);
            tbChuxue.Text = Convert.ToString(dtMzjld.Rows[0]["chuxue"]);
            cmbTiwei.Text = Convert.ToString(dtMzjld.Rows[0]["tw"]);
            txtSZZD.Text = Convert.ToString(dtMzjld.Rows[0]["Szzd"]);
            if (Convert.ToString(dtMzjld.Rows[0]["ShoushuFS"]) != "")
            {
                txtSSSS.Text = Convert.ToString(dtMzjld.Rows[0]["ShoushuFS"]);
            }
            txtNssss.Text = Convert.ToString(dtMzjld.Rows[0]["nssss"]);
            if (dtMzjld.Rows[0]["ssys"].ToString() != "")
            {
                txtSSYS.Text = Convert.ToString(dtMzjld.Rows[0]["ssys"]);
            }
            if (Convert.ToString(dtMzjld.Rows[0]["mzys"]) != "")
            {
                txtMZYS.Text = Convert.ToString(dtMzjld.Rows[0]["mzys"]);
            }
            if (Convert.ToString(dtMzjld.Rows[0]["qxhs"]) != "")
            {
                txtQXHS.Text = Convert.ToString(dtMzjld.Rows[0]["qxhs"]);
            }
            if (Convert.ToString(dtMzjld.Rows[0]["xhhs"]) != "")
            {
                txtXHHS.Text = Convert.ToString(dtMzjld.Rows[0]["xhhs"]);
            }
            cmbMZXG.Text = Convert.ToString(dtMzjld.Rows[0]["mzxg"]);
            if (dtMzjld.Rows[0]["ASA"].ToString() != "")
            {
                cmbASA.Text = Convert.ToString(dtMzjld.Rows[0]["ASA"]);
            }
            cmbSQJinshi.Text = Convert.ToString(dtMzjld.Rows[0]["SQJinshi"]);
            if (Convert.ToString(dtMzjld.Rows[0]["isjizhen"]) == "1")
                this.cbJizhen.Checked = true;
            if (Convert.ToString(dtMzjld.Rows[0]["isjizhen"]) == "0")
                this.cbZeqi.Checked = true;
            cmbBRQX.Text = Convert.ToString(dtMzjld.Rows[0]["brqx"]);
            this.cmbQiekouType.Text = Convert.ToString(dtMzjld.Rows[0]["QiekouType"]);
            this.tbQiekouCount.Text = Convert.ToString(dtMzjld.Rows[0]["QiekouCount"]);
            otime = Convert.ToDateTime(dtMzjld.Rows[0]["otime"]);
            dtOdate.Value = otime; dtOtime.Value = otime;
            txtName.Text = Convert.ToString(dtMzjld.Rows[0]["Ssname"]);//姓名
            txtAge.Text = Convert.ToString(dtMzjld.Rows[0]["SsAge"]);//年龄
            txtSex.Text = Convert.ToString(dtMzjld.Rows[0]["SsSex"]);//性别
            txtXueya.Text = Convert.ToString(dtMzjld.Rows[0]["Rsssxy"]);
            txtMaibo.Text = Convert.ToString(dtMzjld.Rows[0]["Mb"]);
            txtHuxi.Text = Convert.ToString(dtMzjld.Rows[0]["Hx"]);
            cmbXueXing.Text = Convert.ToString(dtMzjld.Rows[0]["Xx"]);
            txtQYZZ.Text = Convert.ToString(dtMzjld.Rows[0]["Qyzz"]);
            if (Convert.ToString(dtMzjld.Rows[0]["MazuiFS"]) != "")
            {
                txtMZFF.Text = Convert.ToString(dtMzjld.Rows[0]["MazuiFS"]);//麻醉方式
            }
            cmbMZPM.Text = Convert.ToString(dtMzjld.Rows[0]["Mzpm"]);
            txtMZyaowu.Text = Convert.ToString(dtMzjld.Rows[0]["Yw"]);
            cmbQMYD.Text = Convert.ToString(dtMzjld.Rows[0]["Qmyd"]);
            cmbQMWC.Text = Convert.ToString(dtMzjld.Rows[0]["Qmwc"]);
            txtQDTQ.Text = Convert.ToString(dtMzjld.Rows[0]["Qdytq"]);
            UserCG.Text = Convert.ToString(dtMzjld.Rows[0]["Cg"]);
            if (dtMzjld.Rows[0]["Mzxg2"].ToString() != "")
            {
                userMZXG.Text = Convert.ToString(dtMzjld.Rows[0]["Mzxg2"]);
            }
            userCSQK.Text = Convert.ToString(dtMzjld.Rows[0]["Csqk"]);
            userSSSJ.Text = Convert.ToString(dtMzjld.Rows[0]["Sssj"]);
            userMZSJ.Text = Convert.ToString(dtMzjld.Rows[0]["Mzsj"]);
            txtShixue.Text = Convert.ToString(dtMzjld.Rows[0]["Sxl"]);
            txtHongxibao.Text = Convert.ToString(dtMzjld.Rows[0]["Hxb"]);
            txtChuniao.Text = Convert.ToString(dtMzjld.Rows[0]["Nl"]);
            txtQuanxue.Text = Convert.ToString(dtMzjld.Rows[0]["Qx"]);
            txtXiongshui.Text = Convert.ToString(dtMzjld.Rows[0]["Xs"]);
            txtXuejiang.Text = Convert.ToString(dtMzjld.Rows[0]["Xj"]);
            txtFushui.Text = Convert.ToString(dtMzjld.Rows[0]["Fs"]);
            txtShuye.Text = Convert.ToString(dtMzjld.Rows[0]["Sy"]);
            userSHZT.Text = Convert.ToString(dtMzjld.Rows[0]["Shzt"]);
            userZTYY.Text = Convert.ToString(dtMzjld.Rows[0]["Ztyy"]);
            usercgh.Text = Convert.ToString(dtMzjld.Rows[0]["cgh"]);
            BindMZSSCGBG();//手术，麻醉，插管拔管时间赋值，坐标赋值         

            //// ↓术中事件赋值
            DataTable dtSZSJ = dal.GetSZSJ(_MzjldId);
            if (dtSZSJ.Rows.Count > 0)
            {
                for (int i = 0; i < dtSZSJ.Rows.Count; i++)
                {
                    listBoxSZSJ.Items.Add(i + 1 + "." + dtSZSJ.Rows[i][2]
                        + " " + Convert.ToDateTime(dtSZSJ.Rows[i][3]).ToString("HH:mm"));
                }
            }

            //// ↓特殊用药赋值
            //DataTable dtTSYY = dal.GetTSYY(_MzjldId);
            //if (dtTSYY.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtTSYY.Rows.Count; i++)
            //    {
            //        listBox2.Items.Add(i + 1 + "." + dtTSYY.Rows[i][2] + " "
            //            + dtTSYY.Rows[i][3] + " " + dtTSYY.Rows[i][4]);
            //    }
            //}
            //BindTsyy();//绑定特殊用药
        }

        private void BindMZSSCGBG()
        {
            DataTable dtMzjld = _MzjldDal.GetMzjldByMzjldId(_MzjldId);
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
                    LableMZKS.Visible = true;
                    LableMZKS.Text = "X";
                    LableMZKS.AutoSize = true;
                    LableMZKS.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    LableMZKS.BackColor = Color.Transparent;
                    LableMZKS.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 160), 370);
                    this.pictureBox3.Controls.Add(LableMZKS);
                    IsMzEnd = true;
                    IsMzEnd = false;
                    LableMZKS.MouseDown += new MouseEventHandler(lbMzks1_MouseDown);
                    LableMZKS.MouseMove += new MouseEventHandler(lbMzks1_MouseMove);
                    LableMZKS.MouseUp += new MouseEventHandler(lbMzks1_MouseUp);
                    LableMZKS.MouseLeave += new EventHandler(lbMzks1_MouseLeave);
                }

            }
            if (dtMzjld.Rows[0]["mzjssj"].ToString() != "")
            {
                mzjsTime = Convert.ToDateTime(dtMzjld.Rows[0]["mzjssj"]);
                TimeSpan t = new TimeSpan();
                t = mzjsTime - otime;
                if ((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) > 0)
                {
                    LableMZJS.Visible = true;
                    LableMZJS.Text = "X";
                    LableMZJS.AutoSize = true;
                    LableMZJS.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    LableMZJS.BackColor = Color.Transparent;
                    LableMZJS.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 160), 370);
                    this.pictureBox3.Controls.Add(LableMZJS);
                    IsMzStart = true;
                    LableMZJS.MouseDown += new MouseEventHandler(lbMzjs1_MouseDown);
                    LableMZJS.MouseMove += new MouseEventHandler(lbMzjs1_MouseMove);
                    LableMZJS.MouseUp += new MouseEventHandler(lbMzjs1_MouseUp);
                    LableMZJS.MouseLeave += new EventHandler(lbMzjs1_MouseLeave);
                }
            }
            if (dtMzjld.Rows[0]["sskssj"].ToString() != "")
            {
                ssksTime = Convert.ToDateTime(dtMzjld.Rows[0]["sskssj"]);
                TimeSpan t = new TimeSpan();
                t = ssksTime - otime;
                if ((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) > 0)
                {
                    LableSSKS.Text = "⊙";
                    LableSSKS.Visible = true;
                    LableSSKS.AutoSize = true;
                    LableSSKS.BackColor = Color.Transparent;
                    LableSSKS.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    LableSSKS.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 160), 370);
                    this.pictureBox3.Controls.Add(LableSSKS);
                    IsOperStart = true;
                    IsOperEnd = false;
                    LableSSKS.MouseDown += new MouseEventHandler(ssks1_MouseDown);
                    LableSSKS.MouseMove += new MouseEventHandler(ssks1_MouseMove);
                    LableSSKS.MouseUp += new MouseEventHandler(ssks1_MouseUp);
                    LableSSKS.MouseLeave += new EventHandler(ssks1_MouseLeave);
                }
            }
            if (dtMzjld.Rows[0]["ssjssj"].ToString() != "")
            {
                ssjsTime = Convert.ToDateTime(dtMzjld.Rows[0]["ssjssj"]);
                TimeSpan t = new TimeSpan();
                t = ssjsTime - otime;
                if ((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) > 0)
                {
                    LableSSJS.Visible = true;
                    LableSSJS.Text = " ";
                    LableSSJS.Image = Properties.Resources.SSJS;
                    LableSSJS.AutoSize = true;
                    LableSSJS.BackColor = Color.Transparent;
                    LableSSJS.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    LableSSJS.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 160), 370);

                    this.pictureBox3.Controls.Add(LableSSJS);
                    IsOperEnd = true;
                    LableSSJS.MouseDown += new MouseEventHandler(ssjs1_MouseDown);
                    LableSSJS.MouseMove += new MouseEventHandler(ssjs1_MouseMove);
                    LableSSJS.MouseUp += new MouseEventHandler(ssjs1_MouseUp);
                    LableSSJS.MouseLeave += new EventHandler(ssjs1_MouseLeave);
                }
            }
            if (dtMzjld.Rows[0]["cgsj"].ToString() != "")
            {
                _CgTime = Convert.ToDateTime(dtMzjld.Rows[0]["cgsj"]);
                TimeSpan t = new TimeSpan();
                t = _CgTime - otime;
                if ((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) > 0)
                {
                    LableCG.Visible = true;
                    LableCG.Text = " ";
                    LableCG.Image = Properties.Resources.CG;
                    LableCG.AutoSize = true;
                    LableCG.BackColor = Color.Transparent;
                    LableCG.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    LableCG.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 160), 370);
                    this.pictureBox3.Controls.Add(LableCG);
                    IsCg = true;
                    IsBg = false;
                    LableCG.MouseDown += new MouseEventHandler(lb_cguan_MouseDown);
                    LableCG.MouseMove += new MouseEventHandler(lb_cguan_MouseMove);
                    LableCG.MouseUp += new MouseEventHandler(lb_cguan_MouseUp);
                    LableCG.MouseLeave += new EventHandler(lb_cguan_MouseLeave);
                }
            }
            if (dtMzjld.Rows[0]["bgsj"].ToString() != "")
            {
                _BgTime = Convert.ToDateTime(dtMzjld.Rows[0]["bgsj"]);
                TimeSpan t = new TimeSpan();
                t = _BgTime - otime;
                if ((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) > 0)
                {
                    LableBG.Text = " ";
                    LableBG.Image = Properties.Resources.BG;
                    LableBG.Visible = true;
                    LableBG.AutoSize = true;
                    LableBG.BackColor = Color.Transparent;
                    LableBG.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    LableBG.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 160), 370);
                    this.pictureBox3.Controls.Add(LableBG);
                    IsBg = true;
                    LableBG.MouseDown += new MouseEventHandler(lb_bguan_MouseDown);
                    LableBG.MouseMove += new MouseEventHandler(lb_bguan_MouseMove);
                    LableBG.MouseUp += new MouseEventHandler(lb_bguan_MouseUp);
                    LableBG.MouseLeave += new EventHandler(lb_bguan_MouseLeave);
                }
            }
        }

        private void GetPatBasicInfo()
        {
            DataTable dt = _PaibanDal.GetPaibanByPatId(patID);
            if (dt.Rows.Count == 0)
            {
                return;
            }
            txtpatID.Text = Convert.ToString(dt.Rows[0]["patid"]);
            txtName.Text = Convert.ToString(dt.Rows[0]["patname"]);
            txtAge.Text = Convert.ToString(dt.Rows[0]["patage"]);
            txtSex.Text = Convert.ToString(dt.Rows[0]["patsex"]);
            txtHeight.Text = Convert.ToString(dt.Rows[0]["PatHeight"]);
            txtWeight.Text = Convert.ToString(dt.Rows[0]["PatWeight"]);
            cmbXueXing.Text = Convert.ToString(dt.Rows[0]["PatBloodType"]);
            txtZhuYuanHao.Text = Convert.ToString(dt.Rows[0]["PatZhuYuanID"]);
            txtBingQu.Text = Convert.ToString(dt.Rows[0]["patdpm"]);
            txtBednumber.Text = Convert.ToString(dt.Rows[0]["patbedno"]);
            txtNssss.Text = Convert.ToString(dt.Rows[0]["oname"]);
            //txtSZZD.Text = Convert.ToString(dt.Rows[0]["oname"]);
            txtMZFF.Text = Convert.ToString(dt.Rows[0]["SSmzfs"]);
            txtSqzd.Text = Convert.ToString(dt.Rows[0]["pattmd"]);
            txtSSSS.Text = Convert.ToString(dt.Rows[0]["Oname"]);
            txtSSYS.Text = Convert.ToString(dt.Rows[0]["os"].ToString().Trim())
                                + " " + Convert.ToString(dt.Rows[0]["os1"].ToString().Trim())
            + " " + Convert.ToString(dt.Rows[0]["os2"].ToString().Trim());
            txtMZYS.Text = Convert.ToString(dt.Rows[0]["ap1"].ToString().Trim())
                                + " " + Convert.ToString(dt.Rows[0]["ap2"].ToString().Trim())
                                + " " + Convert.ToString(dt.Rows[0]["ap3"].ToString().Trim());
            txtQXHS.Text = Convert.ToString(dt.Rows[0]["On1"].ToString().Trim())
                                + " " + Convert.ToString(dt.Rows[0]["on2"].ToString().Trim());
            txtXHHS.Text = Convert.ToString(dt.Rows[0]["Sn1"].ToString().Trim())
                                + " " + Convert.ToString(dt.Rows[0]["sn2"].ToString().Trim());
            dtOdate.Value = Convert.ToDateTime(dt.Rows[0]["Odate"]);

        }
        private void bingSQFS()
        {
            ///调用术前访视的数据
            DataTable dtsq = _BeforeVisitDal.GetBeforeVist_YS(patID);
            if (dtsq.Rows.Count > 0)
            {
                if (dtsq.Rows[0]["ASA"].ToString() != "")
                {
                    cmbASA.Text = dtsq.Rows[0]["ASA"].ToString();
                }
                if (dtsq.Rows[0]["Mzqyy"].ToString() != "")
                {
                    txtSqyy.Text = dtsq.Rows[0]["Mzqyy"].ToString();
                }
                if (dtsq.Rows[0]["ZgCC"].ToString() != "")
                {
                    userMZXG.Text = dtsq.Rows[0]["ZgCC"].ToString();
                }

            }
        }
        /// <summary>
        /// 体位
        /// </summary>
        private void cmbTiweiBind()
        {
            DataTable dt = _DataDicDal.SelectDataByTableName("tiwei");
            cmbTiwei.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                cmbTiwei.Items.Add(dr[1].ToString());
            }
        }
        /// <summary>
        /// 全麻维持
        /// </summary>
        private void cmbqmwcBind()
        {
            DataTable dt = _DataDicDal.SelectDataByTableName("QM_WC");
            cmbQMWC.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                cmbQMWC.Items.Add(dr[1].ToString());
            }
        }

        /// <summary>
        /// 麻醉平面
        /// </summary>
        private void cmbmzpmBind()
        {
            DataTable dt = _DataDicDal.SelectDataByTableName("MZPM");
            cmbMZPM.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                cmbMZPM.Items.Add(dr[1].ToString());
            }
        }
        /// <summary>
        /// 全麻诱导
        /// </summary>
        private void cmbqmydBind()
        {
            DataTable dt = _DataDicDal.SelectDataByTableName("QM_YD");
            cmbQMYD.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                cmbQMYD.Items.Add(dr[1].ToString());
            }
        }
        private void cmbQiekouTypeBind()
        {
            DataTable dt = _DataDicDal.SelectDataByTableName("qiekoutype");
            cmbQiekouType.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                cmbQiekouType.Items.Add(dr[1].ToString());
            }
        }
        private void BindPortName()
        {
            string[] spName = System.IO.Ports.SerialPort.GetPortNames();
            if (spName.Length > 0)
            {
                cmbCOM.Items.Clear();
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

        private void mzjld_Load(object sender, EventArgs e)/// 窗体加载
        {
            //LOAD_jiekou();//加载迈瑞接口数据
            Application.AddMessageFilter(this);//禁止下拉框鼠标滚动
            //this.jkhxToolStripMenuItem_Click
            ZoomMemu.Click += new EventHandler(ZoomMemu_Click);

            this.fzhxtoolStripMenuItem.Click += new System.EventHandler(fzhxToolStripMenuItem_Click);
            this.txtSqzd.LineBoxDoubleClick += new System.EventHandler(this.txtSqzd_DoubleClick);
            this.txtSqyy.LineBoxDoubleClick += new System.EventHandler(this.txtSqyy_DoubleClick);
            this.txtSZZD.LineBoxDoubleClick += new System.EventHandler(this.txtShoushuFS_DoubleClick);
            this.txtMZFF.LineBoxDoubleClick += new System.EventHandler(this.txtMazuiFS_DoubleClick);
            this.txtNssss.LineBoxDoubleClick += new System.EventHandler(this.txtNssss_DoubleClick);
            this.txtMZYS.LineBoxDoubleClick += new System.EventHandler(this.txtMZYS_DoubleClick);
            this.txtQXHS.LineBoxDoubleClick += new System.EventHandler(this.txtQXHS_DoubleClick);
            this.txtXHHS.LineBoxDoubleClick += new System.EventHandler(this.txtXHHS_DoubleClick);
            this.UserCG.LineBoxDoubleClick += new System.EventHandler(this.UserCG_DoubleClick);
            this.txtQYZZ.LineBoxDoubleClick += new System.EventHandler(this.txtQYZZ_DoubleClick);
            this.txtQDTQ.LineBoxDoubleClick += new System.EventHandler(this.txtQDTQ_DoubleClick);
            this.txtSSSS.LineBoxDoubleClick += new System.EventHandler(this.txtSSSS_DoubleClick);
            this.userCSQK.LineBoxDoubleClick += new System.EventHandler(this.userCSQK_DoubleClick);
            this.userSHZT.LineBoxDoubleClick += new System.EventHandler(this.userSHZT_DoubleClick);
            this.userMZXG.LineBoxDoubleClick += new System.EventHandler(this.userMZXG_DoubleClick);
            this.txtMZyaowu.LineBoxDoubleClick += new System.EventHandler(this.txtMZyaowu_DoubleClick);
            BindPortName();
            cmbTiweiBind();
            cmbqmwcBind();
            cmbmzpmBind();
            cmbqmydBind();
            cmbQiekouTypeBind();
            this.dtOtime.Format = DateTimePickerFormat.Custom;
            this.dtOtime.CustomFormat = "MM-dd HH:mm";
            try
            {
                GetPatBasicInfo();
                if (_MzjldId == 0)
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
                    InRoomTime = otime;
                    _MzjldDal.AddMzjld(patID, otime);
                    DataTable dt = new DataTable();
                    dt = _MzjldDal.GetMzjldByPatId(patID);
                    _MzjldId = Convert.ToInt32(dt.Rows[0][0]);
                    cmbSJJG.Text = "5";
                    _MonitorInterval = 5;
                    //BindZoomRegionList();
                    txtMzjldid.Text = Convert.ToString(_MzjldId);
                    dal.UpdateShoushujianinfo(0, _MzjldId, patID, Oroom);//修改手术间状态信息
                    dal.UpdatePaibanInfo(2, patID);
                    jhxmIn.Clear();
                    jhxmIn.Add("SpO2");
                    int f_jhxm = 0;
                    foreach (string name in jhxmIn)
                    {
                        bll.addJhxm(_MzjldId, name, 0);
                        f_jhxm++;
                    }
                    if (f_jhxm == 0)
                    {
                        MessageBox.Show("监护项目添加失败！");
                    }
                    BindShijiandian();
                    int Flag = SaveMzjld();
                    if (Flag == 0)
                    {
                        MessageBox.Show("请手动保存");
                    }
                }
                else if (_MzjldId != 0)
                {
                    dal.UpdateShoushujianinfo(1, _MzjldId, patID, Oroom);//修改手术间状态信息
                    jhxmIn.Clear();
                    DataTable dtJhxm = bll.GetJhxm(_MzjldId);
                    for (int i = 0; i < dtJhxm.Rows.Count; i++)
                    {
                        jhxmIn.Add(dtJhxm.Rows[i][0].ToString());
                    }
                    DataTable dt = new DataTable();
                    txtMzjldid.Text = Convert.ToString(_MzjldId);
                    DataTable dtMZ_Info = _MzjldDal.GetMzjldByMzjldId(_MzjldId);
                    _MonitorInterval = Convert.ToInt32(dtMZ_Info.Rows[0]["jcsjjg"].ToString());
                    otime = (Convert.ToDateTime(dtMZ_Info.Rows[0]["otime"]));
                    InRoomTime = (Convert.ToDateTime(dtMZ_Info.Rows[0]["otime"]));
                    //BindZoomRegionList();
                    cmbSJJG.Text = _MonitorInterval.ToString();
                    dtOdate.Value = otime;
                    BindShijiandian();//绑定时间坐标                
                    BindMzjldBacicInfo();
                    BindJikongTime();
                    BindFuZhuTime();
                    BindQtList();
                    BindYdyList();
                    BindJmyList();
                    BindTsyy();
                    BindJHDian();
                    BindCLlist();
                    BindShuyeList();
                    BindShuxueList();
                    BindSZSJ();
                }
                if (isSjll)
                {
                    ZoomMemu.Enabled = false;
                    AddPointTSMenu.Enabled = false;
                    DeleteCGBGStripMenuItem.Enabled = false;
                    jkhxToolStripMenuItem.Enabled = false;
                    fzhxtoolStripMenuItem.Enabled = false;
                    cmbSJJG.Enabled = false;
                    this.fzhxtoolStripMenuItem.Click -= new System.EventHandler(fzhxToolStripMenuItem_Click);
                    txtSqzd.LineBoxDoubleClick -= new System.EventHandler(this.txtSqzd_DoubleClick);
                    txtSqyy.LineBoxDoubleClick -= new System.EventHandler(this.txtSqyy_DoubleClick);
                    txtSZZD.LineBoxDoubleClick -= new System.EventHandler(this.txtShoushuFS_DoubleClick);
                    txtMZFF.LineBoxDoubleClick -= new System.EventHandler(this.txtMazuiFS_DoubleClick);
                    txtNssss.LineBoxDoubleClick -= new System.EventHandler(this.txtNssss_DoubleClick);
                    txtMZYS.LineBoxDoubleClick -= new System.EventHandler(this.txtMZYS_DoubleClick);
                    txtQXHS.LineBoxDoubleClick -= new System.EventHandler(this.txtQXHS_DoubleClick);
                    txtXHHS.LineBoxDoubleClick -= new System.EventHandler(this.txtXHHS_DoubleClick);
                    this.UserCG.LineBoxDoubleClick -= new System.EventHandler(this.UserCG_DoubleClick);
                    this.txtQYZZ.LineBoxDoubleClick -= new System.EventHandler(this.txtQYZZ_DoubleClick);
                    this.txtQDTQ.LineBoxDoubleClick -= new System.EventHandler(this.txtQDTQ_DoubleClick);
                    this.userMZXG.LineBoxDoubleClick -= new System.EventHandler(this.userMZXG_DoubleClick);
                    this.txtMZyaowu.LineBoxDoubleClick -= new System.EventHandler(this.txtMZyaowu_DoubleClick);
                    this.userCSQK.LineBoxDoubleClick -= new System.EventHandler(this.userCSQK_DoubleClick);
                    this.userSHZT.LineBoxDoubleClick -= new System.EventHandler(this.userSHZT_DoubleClick);
                    pictureBox2.MouseDoubleClick -= new MouseEventHandler(pictureBox2_MouseDoubleClick);
                    pictureBox2.MouseMove -= new MouseEventHandler(pictureBox2_MouseMove);
                    pictureBox3.MouseMove -= new MouseEventHandler(pictureBox3_MouseMove);
                    btnSave.Enabled = false;
                    btnMonitor.Enabled = false;
                    cmbCOM.Enabled = false;
                    cmbJianhuyi.Enabled = false;
                    btnTsyy.Enabled = false;
                    btnSzsj.Enabled = false;
                    //this.btnPrintView.Enabled = false;
                    lbMzjs.DoubleClick -= new EventHandler(lbMzjs_DoubleClick);
                    lbMzks.DoubleClick -= new EventHandler(lbMzks_DoubleClick);
                    lbSsjs.DoubleClick -= new EventHandler(lbSsjs_DoubleClick);
                    lbSsks.DoubleClick -= new EventHandler(lbSsks_DoubleClick);
                    lbBg.DoubleClick -= new EventHandler(lbBg_DoubleClick);
                    lbCg.DoubleClick -= new EventHandler(lbCg_DoubleClick);
                    LableCG.MouseDown -= new MouseEventHandler(lb_cguan_MouseDown);
                    LableCG.MouseMove -= new MouseEventHandler(lb_cguan_MouseMove);
                    LableCG.MouseUp -= new MouseEventHandler(lb_cguan_MouseUp);
                    LableCG.MouseLeave -= new EventHandler(lb_cguan_MouseLeave);
                    LableCG.MouseDown -= new MouseEventHandler(lb_bguan_MouseDown);
                    LableCG.MouseMove -= new MouseEventHandler(lb_bguan_MouseMove);
                    LableCG.MouseUp -= new MouseEventHandler(lb_bguan_MouseUp);
                    LableCG.MouseLeave -= new EventHandler(lb_bguan_MouseLeave);
                    LableMZJS.MouseDown -= new MouseEventHandler(lbMzjs1_MouseDown);
                    LableMZJS.MouseMove -= new MouseEventHandler(lbMzjs1_MouseMove);
                    LableMZJS.MouseUp -= new MouseEventHandler(lbMzjs1_MouseUp);
                    LableMZJS.MouseLeave -= new EventHandler(lbMzjs1_MouseLeave);
                    LableMZKS.MouseDown -= new MouseEventHandler(lbMzks1_MouseDown);
                    LableMZKS.MouseMove -= new MouseEventHandler(lbMzks1_MouseMove);
                    LableMZKS.MouseUp -= new MouseEventHandler(lbMzks1_MouseUp);
                    LableMZKS.MouseLeave -= new EventHandler(lbMzks1_MouseLeave);
                    LableSSJS.MouseDown -= new MouseEventHandler(ssjs1_MouseDown);
                    LableSSJS.MouseMove -= new MouseEventHandler(ssjs1_MouseMove);
                    LableSSJS.MouseUp -= new MouseEventHandler(ssjs1_MouseUp);
                    LableSSJS.MouseLeave -= new EventHandler(ssjs1_MouseLeave);
                    LableSSKS.MouseDown -= new MouseEventHandler(ssks1_MouseDown);
                    LableSSKS.MouseMove -= new MouseEventHandler(ssks1_MouseMove);
                    LableSSKS.MouseUp -= new MouseEventHandler(ssks1_MouseUp);
                    LableSSKS.MouseLeave -= new EventHandler(ssks1_MouseLeave);
                    LableCG.MouseDown -= new MouseEventHandler(lb_cguan_MouseDown);
                    LableCG.MouseMove -= new MouseEventHandler(lb_cguan_MouseMove);
                    LableCG.MouseUp -= new MouseEventHandler(lb_cguan_MouseUp);
                    LableCG.MouseLeave -= new EventHandler(lb_cguan_MouseLeave);
                    LableBG.MouseDown -= new MouseEventHandler(lb_bguan_MouseDown);
                    LableBG.MouseMove -= new MouseEventHandler(lb_bguan_MouseMove);
                    LableBG.MouseUp -= new MouseEventHandler(lb_bguan_MouseUp);
                    LableBG.MouseLeave -= new EventHandler(lb_bguan_MouseLeave);
                    LableMZJS.MouseDown -= new MouseEventHandler(lbMzjs1_MouseDown);
                    LableMZJS.MouseMove -= new MouseEventHandler(lbMzjs1_MouseMove);
                    LableMZJS.MouseUp -= new MouseEventHandler(lbMzjs1_MouseUp);
                    LableMZJS.MouseLeave -= new EventHandler(lbMzjs1_MouseLeave);
                    LableMZKS.MouseDown -= new MouseEventHandler(lbMzks1_MouseDown);
                    LableMZKS.MouseMove -= new MouseEventHandler(lbMzks1_MouseMove);
                    LableMZKS.MouseUp -= new MouseEventHandler(lbMzks1_MouseUp);
                    LableMZKS.MouseLeave -= new EventHandler(lbMzks1_MouseLeave);
                    LableSSJS.MouseDown -= new MouseEventHandler(ssjs1_MouseDown);
                    LableSSJS.MouseMove -= new MouseEventHandler(ssjs1_MouseMove);
                    LableSSJS.MouseUp -= new MouseEventHandler(ssjs1_MouseUp);
                    LableSSJS.MouseLeave -= new EventHandler(ssjs1_MouseLeave);
                    LableSSKS.MouseDown -= new MouseEventHandler(ssks1_MouseDown);
                    LableSSKS.MouseMove -= new MouseEventHandler(ssks1_MouseMove);
                    LableSSKS.MouseUp -= new MouseEventHandler(ssks1_MouseUp);
                    LableSSKS.MouseLeave -= new EventHandler(ssks1_MouseLeave);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "出现异常，请检查！");
            }
        }
        private void BindZoomRegionListPrint(DateTime StartTime)
        {
            DataTable dt5 = dal.Get_ZoomTime(_MzjldId, StartTime);
            ZoomRegionListPrint.Clear();

            if (dt5.Rows.Count == 0)
            {
                ZoomRegion zr = new ZoomRegion();
                zr.AStartTime = StartTime;
                zr.EndTime = StartTime.AddDays(2);
                zr.Interval = _MonitorInterval;
                zr.RegionSize = TextValueLimit.GetRegionSizePrint(zr);
                ZoomRegionListPrint.Add(zr);
            }
            else
            {

                for (int i = 0; i < dt5.Rows.Count; i++)
                {
                    if (i == 0 && otime < Convert.ToDateTime(dt5.Rows[0]["StartTime"]))
                    {
                        ZoomRegion zr2 = new ZoomRegion();
                        zr2.AStartTime = StartTime;
                        zr2.EndTime = Convert.ToDateTime(dt5.Rows[0]["StartTime"]);
                        zr2.Interval = _MonitorInterval;
                        zr2.RegionSize = TextValueLimit.GetRegionSizePrint(zr2);
                        ZoomRegionListPrint.Add(zr2);
                    }
                    if (i > 0)
                    {
                        ZoomRegion zr2 = new ZoomRegion();
                        zr2.AStartTime = Convert.ToDateTime(dt5.Rows[i - 1]["EndTime"]);
                        zr2.EndTime = Convert.ToDateTime(dt5.Rows[i]["StartTime"]);
                        zr2.Interval = _MonitorInterval;
                        zr2.RegionSize = TextValueLimit.GetRegionSizePrint(zr2);
                        ZoomRegionListPrint.Add(zr2);
                    }
                    ZoomRegion zr1 = new ZoomRegion();
                    zr1.AStartTime = Convert.ToDateTime(dt5.Rows[i]["StartTime"]);
                    zr1.EndTime = Convert.ToDateTime(dt5.Rows[i]["EndTime"]);
                    zr1.Interval = Convert.ToInt32(dt5.Rows[i]["Interval"]);
                    zr1.RegionSize = TextValueLimit.GetRegionSizePrint(zr1);
                    ZoomRegionListPrint.Add(zr1);
                }

                ZoomRegion zr3 = new ZoomRegion();
                zr3.AStartTime = Convert.ToDateTime(dt5.Rows[dt5.Rows.Count - 1]["EndTime"]); ;
                zr3.EndTime = StartTime.AddDays(2);
                zr3.Interval = _MonitorInterval;
                zr3.RegionSize = TextValueLimit.GetRegionSizePrint(zr3);
                ZoomRegionListPrint.Add(zr3);
            }
        }
        private void BindZoomRegionList()
        {
            DataTable dt5 = dal.Get_ZoomTime(_MzjldId, otime);
            ZoomRegionList.Clear();
            if (dt5.Rows.Count == 0)
            {
                ZoomRegion zr = new ZoomRegion();
                zr.AStartTime = otime;
                zr.EndTime = InRoomTime.AddDays(2);
                zr.Interval = _MonitorInterval;
                zr.RegionSize = TextValueLimit.GetRegionSize(zr);
                ZoomRegionList.Add(zr);
            }
            else
            {

                for (int i = 0; i < dt5.Rows.Count; i++)
                {
                    if (i == 0 && otime < Convert.ToDateTime(dt5.Rows[0]["StartTime"]))
                    {
                        ZoomRegion zr2 = new ZoomRegion();
                        zr2.AStartTime = otime;
                        zr2.EndTime = Convert.ToDateTime(dt5.Rows[0]["StartTime"]);
                        zr2.Interval = _MonitorInterval;
                        zr2.RegionSize = TextValueLimit.GetRegionSize(zr2);
                        ZoomRegionList.Add(zr2);
                    }
                    if (i > 0)
                    {
                        ZoomRegion zr2 = new ZoomRegion();
                        zr2.AStartTime = Convert.ToDateTime(dt5.Rows[i - 1]["EndTime"]);
                        zr2.EndTime = Convert.ToDateTime(dt5.Rows[i]["StartTime"]);
                        zr2.Interval = _MonitorInterval;
                        zr2.RegionSize = TextValueLimit.GetRegionSize(zr2);
                        ZoomRegionList.Add(zr2);
                    }
                    ZoomRegion zr1 = new ZoomRegion();
                    zr1.AStartTime = Convert.ToDateTime(dt5.Rows[i]["StartTime"]);
                    zr1.EndTime = Convert.ToDateTime(dt5.Rows[i]["EndTime"]);
                    zr1.Interval = Convert.ToInt32(dt5.Rows[i]["Interval"]); ;
                    zr1.RegionSize = TextValueLimit.GetRegionSize(zr1);
                    ZoomRegionList.Add(zr1);
                }

                ZoomRegion zr3 = new ZoomRegion();
                zr3.AStartTime = Convert.ToDateTime(dt5.Rows[dt5.Rows.Count - 1]["EndTime"]); ;
                zr3.EndTime = otime.AddDays(2);
                zr3.Interval = _MonitorInterval;
                zr3.RegionSize = TextValueLimit.GetRegionSize(zr3);
                ZoomRegionList.Add(zr3);
            }

            lbTime00.Text = lbTime0.Text = otime.ToString("HH:mm");
            int ADD = TextValueLimit.XtoMinute(90, ZoomRegionList);
            lbTime11.Text = lbTime1.Text = otime.AddMinutes(ADD).ToString("HH:mm");
            ADD = TextValueLimit.XtoMinute(180, ZoomRegionList);
            lbTime22.Text = lbTime2.Text = otime.AddMinutes(ADD).ToString("HH:mm");
            ADD = TextValueLimit.XtoMinute(270, ZoomRegionList);
            lbTime33.Text = lbTime3.Text = otime.AddMinutes(ADD).ToString("HH:mm");
            ADD = TextValueLimit.XtoMinute(360, ZoomRegionList);
            lbTime44.Text = lbTime4.Text = otime.AddMinutes(ADD).ToString("HH:mm");
            ADD = TextValueLimit.XtoMinute(450, ZoomRegionList);
            lbTime55.Text = lbTime5.Text = otime.AddMinutes(ADD).ToString("HH:mm");
            ADD = TextValueLimit.XtoMinute(540, ZoomRegionList);
            lbTime66.Text = lbTime6.Text = otime.AddMinutes(ADD).ToString("HH:mm");
            ADD = TextValueLimit.XtoMinute(630, ZoomRegionList);
            lbTime77.Text = lbTime7.Text = otime.AddMinutes(ADD).ToString("HH:mm");
            ADD = TextValueLimit.XtoMinute(720, ZoomRegionList);
            lbTime88.Text = lbTime8.Text = otime.AddMinutes(ADD).ToString("HH:mm");
            ADD = TextValueLimit.XtoMinute(810, ZoomRegionList);
            lbTime99.Text = lbTime9.Text = otime.AddMinutes(ADD).ToString("HH:mm");

        }
        #region //绑定用药，时间，麻醉平面等数据
        private void BindQtList()
        {
            mzqtList.Clear();
            DataTable dtQT = _YongyaoListDal.GetYongyaoList(_MzjldId, (int)EnumCreator.YongyaoType.气体药);//1气体药
            if (dtQT.Rows.Count != 0)
            {
                int i = 0;
                foreach (DataRow dr in dtQT.Rows)
                {
                    adims_MODEL.Yongyao mzqt = new adims_MODEL.Yongyao();
                    mzqtList.Add(mzqt);
                    mzqtList[i].Id = Convert.ToInt32(dr["id"]);
                    mzqtList[i].Name = Convert.ToString(dr["name"]);
                    mzqtList[i].Yl = Convert.ToDouble(dr["yl"]);
                    mzqtList[i].Dw = Convert.ToString(dr["dw"]);
                    mzqtList[i].KsTime = Convert.ToDateTime(dr["kstime"]);
                    mzqtList[i].Bz = Convert.ToInt32(dr["flags"]);
                    if (mzqtList[i].Bz == 2)
                    {
                        mzqtList[i].JsTime = Convert.ToDateTime(dr["jstime"]);
                    }
                    i++;
                }
            }
        }

        private void BindShuyeList()
        {
            syZongl = 0;
            shuyeList.Clear();
            DataTable dtSY = _YongyaoListDal.GetYongyaoList(_MzjldId, (int)EnumCreator.YongyaoType.输液);
            if (dtSY.Rows.Count != 0)
            {
                int i = 0;
                foreach (DataRow dr in dtSY.Rows)
                {
                    adims_MODEL.Yongyao yt1 = new adims_MODEL.Yongyao();
                    shuyeList.Add(yt1);
                    shuyeList[i].Id = Convert.ToInt32(dr["id"]);
                    shuyeList[i].Name = Convert.ToString(dr["name"]);
                    shuyeList[i].Yl = Convert.ToDouble(dr["Yl"]);
                    syZongl = syZongl + shuyeList[i].Yl;
                    shuyeList[i].Dw = Convert.ToString(dr["dw"]);
                    shuyeList[i].Yyfs = Convert.ToString(dr["Yyfs"]);
                    shuyeList[i].KsTime = Convert.ToDateTime(dr["KsTime"]);
                    if (Convert.ToInt32(dr["Cxyy"]) == 1)
                        shuyeList[i].Cxyy = true;
                    else
                        shuyeList[i].Cxyy = false;
                    shuyeList[i].Bz = Convert.ToInt32(dr["flags"]);
                    if (shuyeList[i].Bz == 2)
                    {
                        shuyeList[i].JsTime = Convert.ToDateTime(dr["JsTime"]);
                    }
                    i++;
                }
            }
        }

        private void BindShuxueList()
        {
            sxZongl = 0;
            shuxueList.Clear();
            DataTable dtSX = _YongyaoListDal.GetYongyaoList(_MzjldId, (int)EnumCreator.YongyaoType.输血);
            if (dtSX.Rows.Count != 0)
            {
                int i = 0;
                foreach (DataRow dr in dtSX.Rows)
                {
                    adims_MODEL.Yongyao yt1 = new adims_MODEL.Yongyao();
                    shuxueList.Add(yt1);
                    shuxueList[i].Id = Convert.ToInt32(dr["id"]);
                    shuxueList[i].Name = Convert.ToString(dr["name"]);
                    shuxueList[i].Yl = Convert.ToDouble(dr["yl"]);
                    sxZongl = sxZongl + shuxueList[i].Yl;
                    shuxueList[i].Dw = Convert.ToString(dr["dw"]);
                    shuxueList[i].Yyfs = Convert.ToString(dr["yyfs"]);
                    if (Convert.ToInt32(dr["Cxyy"]) == 1)
                        shuxueList[i].Cxyy = true;
                    else
                        shuxueList[i].Cxyy = false;
                    shuxueList[i].KsTime = Convert.ToDateTime(dr["KsTime"]);
                    shuxueList[i].Bz = Convert.ToInt32(dr["flags"]);
                    if (shuxueList[i].Bz == 2)
                    {
                        shuxueList[i].JsTime = Convert.ToDateTime(dr["JsTime"]);
                    }
                    i++;
                }
            }
        }

        private void BindmzpmList()
        {
            mzpmList.Clear();
            DataTable dtMZPM = bll.GETALLmzpm(_MzjldId);
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

        private void BindJmyList()
        {
            jmyList.Clear();
            DataTable dtJMY = _YongyaoListDal.GetYongyaoList(_MzjldId, (int)EnumCreator.YongyaoType.局麻药);
            if (dtJMY.Rows.Count != 0)
            {
                int i = 0;
                foreach (DataRow dr in dtJMY.Rows)
                {
                    adims_MODEL.Yongyao yt1 = new adims_MODEL.Yongyao();
                    jmyList.Add(yt1);
                    jmyList[i].Name = Convert.ToString(dr["name"]);
                    jmyList[i].Yl = Convert.ToDouble(dr["yl"]);
                    if (Convert.ToInt32(dr["Cxyy"]) == 1)
                        jmyList[i].Cxyy = true;
                    else
                        jmyList[i].Cxyy = false;
                    jmyList[i].Dw = Convert.ToString(dr["dw"]);
                    jmyList[i].Yyfs = Convert.ToString(dr["yyfs"]);
                    jmyList[i].KsTime = Convert.ToDateTime(dr["KsTime"]);
                    jmyList[i].Bz = Convert.ToInt32(dr["flags"]);
                    if (jmyList[i].Bz == 2)
                    {
                        jmyList[i].JsTime = Convert.ToDateTime(dr["JsTime"]);
                    }
                    i++;
                }
            }
        }

        private void BindYdyList()
        {
            ydyList.Clear();
            DataTable dtYDY = _YongyaoListDal.GetYongyaoList(_MzjldId, (int)EnumCreator.YongyaoType.诱导药);
            if (dtYDY.Rows.Count != 0)
            {
                int i = 0;
                foreach (DataRow dr in dtYDY.Rows)
                {
                    adims_MODEL.Yongyao yt1 = new adims_MODEL.Yongyao();
                    yt1.Id = Convert.ToInt32(dr["id"]);
                    yt1.Name = Convert.ToString(dr["name"]);
                    yt1.Yl = Convert.ToDouble(dr["yl"]);
                    yt1.Dw = Convert.ToString(dr["dw"]);
                    yt1.Yyfs = Convert.ToString(dr["Yyfs"]);
                    if (Convert.ToInt32(dr["Cxyy"]) == 1)
                        yt1.Cxyy = true;
                    else
                        yt1.Cxyy = false;
                    yt1.KsTime = Convert.ToDateTime(dr["kstime"]);
                    yt1.Bz = Convert.ToInt32(dr["flags"]);
                    if (yt1.Bz == 2)
                        yt1.JsTime = Convert.ToDateTime(dr["jstime"]);
                    ydyList.Add(yt1);
                    i++;
                }
            }
        }

        private void BindZhenTongYao()
        {
            //listBox3.Items.Clear();
            //DataTable dt = bll.getMZZTY(_MzjldId);
            //foreach (DataRow dr in dt.Rows)
            //{
            //    listBox3.Items.Add(dr["name"] + " " + dr["yl"] + dr["dw"]);
            //}
        }
        /// <summary>
        /// 绑定术中事件
        /// </summary>
        private void BindSZSJ()
        {
            szsjList.Clear();
            DataTable dtSZSJ = bll.GETALLSZSJ(_MzjldId);
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
                    if (dr["Y_zb"].ToString() == "")
                    {
                        szsjList[i].Y_zb = 370;
                    }
                    else
                    {
                        szsjList[i].Y_zb = Convert.ToInt32(dr["Y_zb"]);
                    }

                    i++;
                }
            }
            listBoxSZSJ.Items.Clear();
            for (int i = 0; i < szsjList.Count; i++)
            {
                string szsjSort = ArrayHelper.ReplaceNumToLetter(i);
                listBoxSZSJ.Items.Add(szsjSort + "." + szsjList[i].Name + " " + szsjList[i].D.ToString("HH:mm"));
            }
        }
        /// <summary>
        /// 绑定其他用药
        /// </summary>
        private void BindTsyy()
        {
            tsyyList.Clear();
            DataTable dt = _YongyaoListDal.GetYongyaoListOrderbyStartTime(_MzjldId, 6);
            int i = 0;
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    adims_MODEL.Yongyao yt1 = new adims_MODEL.Yongyao();
                    tsyyList.Add(yt1);
                    tsyyList[i].Id = Convert.ToInt32(dr["id"]);
                    tsyyList[i].Name = Convert.ToString(dr["name"]);
                    tsyyList[i].Yl = Convert.ToDouble(dr["yl"]);
                    tsyyList[i].Dw = Convert.ToString(dr["dw"]);
                    tsyyList[i].Yyfs = Convert.ToString(dr["yyfs"]);
                    tsyyList[i].KsTime = Convert.ToDateTime(dr["kstime"]);
                    if (dr["Y_zb"].ToString() == "")
                    {
                        tsyyList[i].Y_zb = 370;
                    }
                    else
                    {
                        tsyyList[i].Y_zb = Convert.ToInt32(dr["Y_zb"]);
                    }

                    i++;
                }
            }
            int i1 = 1;
            listBoxOtherMedical.Items.Clear();
            foreach (adims_MODEL.Yongyao s in tsyyList)
            {
                listBoxOtherMedical.Items.Add(i1.ToString() + ". " + s.Name + " " + s.Yl.ToString() + s.Dw + s.Yyfs);
                i1++;
            }
        }
        #endregion


        #region //双击事件

        private void txtSqyy_DoubleClick(object sender, EventArgs e)/// 术前用药双击事件
        {
            addSqyy sqyyform = new addSqyy(txtSqyy, _MzjldId);
            sqyyform.ShowDialog();
        }
        private void txtHbz_DoubleClick(object sender, EventArgs e) /// 合并症双击事件
        {
            //hbz hbzform = new hbz(txtXueXing, _MzjldId);
            //hbzform.ShowDialog();
        }

        private void txtSqzd_DoubleClick(object sender, EventArgs e)// 术前诊断双击事件
        {
            SelectZhenDuan szzdform = new SelectZhenDuan(txtSqzd, _MzjldId);
            szzdform.ShowDialog();
        }
        private void txtShoushuFS_DoubleClick(object sender, EventArgs e)//手术方式双击事件
        {
            SelectShoushu yssssform = new SelectShoushu(txtSZZD, patID);
            yssssform.ShowDialog();
        }
        private void txtMazuiFS_DoubleClick(object sender, EventArgs e)//麻醉方式双击事件
        {
            SelectMZFF mzffform = new SelectMZFF(txtMZFF, _MzjldId);
            mzffform.ShowDialog();
        }
        private void txtNssss_DoubleClick(object sender, EventArgs e)/// 拟实施手术双击事件
        {
            SelectShoushu yssssform = new SelectShoushu(txtNssss, patID);
            yssssform.ShowDialog();
        }
        /// <summary>
        /// 插管
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserCG_DoubleClick(object sender, EventArgs e)
        {
            AddMZCG F1 = new AddMZCG(UserCG, UserCG.Text);
            F1.ShowDialog();
        }
        /// <summary>
        /// 区域阻滞
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void txtQYZZ_DoubleClick(object sender, EventArgs e)
        //{
        //    AddMZqyzz F1 = new AddMZqyzz(txtQYZZ, txtQYZZ.Text);
        //    F1.ShowDialog();
        //}
        /// <summary>
        /// 气道与通气
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQDTQ_DoubleClick(object sender, EventArgs e)
        {
            addMZqdtq F1 = new addMZqdtq(txtQDTQ, txtQDTQ.Text);
            F1.ShowDialog();
        }
        /// <summary>
        /// 实施手术
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSSSS_DoubleClick(object sender, EventArgs e)
        {
            SelectNSSS F1 = new SelectNSSS(txtSSSS, _MzjldId);
            F1.ShowDialog();
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
        int MR = 0;//控制迈瑞采集开始和结束
        System.Diagnostics.Process procs;
        private void btnMonitor_Click(object sender, EventArgs e)
        {
            #region //分类型采集

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
                    }
                    _serialPort.Open();
                    timer4.Enabled = true;
                    cmbJianhuyi.Enabled = false;
                    cmbCOM.Enabled = false;
                }
                if (cmbJianhuyi.Text == "宝莱特")
                {
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo.FileName = Application.StartupPath + "\\BLT\\TestHL7SDK.exe";
                    proc.StartInfo.UseShellExecute = false;
                    proc.Start();
                    timerBLT.Interval = 20000;
                    timerBLT.Start();
                    cmbJianhuyi.Enabled = false;
                    cmbCOM.Enabled = false;
                }
                if (cmbJianhuyi.Text == "迈瑞监护仪")
                {
                    MR = 1;
                    procs = new System.Diagnostics.Process();
                    procs.StartInfo.FileName = Application.StartupPath + "\\Mindray\\Mindray.exe";
                    string cz = "0" + "," + _MzjldId.ToString();
                    procs.StartInfo.Arguments = cz;
                    procs.StartInfo.UseShellExecute = false;
                    procs.Start();

                    //timerMR.Interval = 5000;

                    //timerMR.Start();
                    //if (!isexist)
                    //{
                    //    cmbJianhuyi.Enabled = false;
                    //    SocketThread = new Thread(Socket_Setup);
                    //    SocketThread.IsBackground = true;
                    //    SocketThread.Priority = ThreadPriority.AboveNormal;
                    //    SocketThread.Start();
                    //    isexist = true;
                    //}
                    //else
                    //{
                    //    if (SocketThread.ThreadState != ThreadState.Running)
                    //    {
                    //       // SocketThread.Abort();
                    //    }
                    //}
                    cmbJianhuyi.Enabled = false;
                    cmbCOM.Enabled = false;
                }
                SaveMzjld();
                ksjcTime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                btnMonitor.Text = "结束监测";
                timer1.Interval = 30 * 1000;
                timer1.Enabled = true;
            }
            #endregion
            else
            {
                if (DialogResult.OK == MessageBox.Show("确定结束检测吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                {
                    //迈瑞
                    if (MR == 1)
                    {
                        procs.Kill();

                    }
                    //if (isexist)
                    //{
                    //    if (TempSocket != null)
                    //        TempSocket.Close();
                    //    if (ServerSocket != null)
                    //        ServerSocket.Close();
                    //    if (SocketThread.ThreadState != ThreadState.Running)
                    //    {
                    //        SocketThread.Abort();
                    //        SocketThread = null; ////在.NET中，有GC垃圾回收，如果这里不赋为null，有可能老半天也不回收内存，甚至永远这么占着，导致内存泄漏。
                    //    }
                    //    isexist = false;
                    //}
                    //timer2.Enabled = false;
                    timer4.Enabled = false;
                    timer1.Enabled = false;
                    // timerBLT.Stop();
                    timerMR.Stop();
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
                    btnMonitor.Text = "开始监测";
                    cmbJianhuyi.Enabled = true;
                    cmbCOM.Enabled = true;

                }
            }
        }


        #region 迈瑞监护函数
        private void SaveLog(string text)
        {
            //string text = "abcdefg\r\n";
            int TextLength = text.Length;
            if (TextLength < 2000)
            {
                FileStream fs = new FileStream("c:\\Mindraytestlog_CS.txt", FileMode.Append);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                sw.Write(text + "\r\n");
                sw.Close();
                fs.Close();
            }
        }
        private void Socket_Setup()
        {
            //string temp1 = "192.168.6.221";
            string temp2 = "8000";
            int port = Convert.ToInt32(temp2);
            string temp1 = PatientIPAddress1.Text;
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
                        ProceedreceivedData(0);
                    Laststring = ReceivedString1;
                }
            }
            catch (Exception exception)
            {
                SockectIsException = true;

                //MessageBox.Show("Socket 连接或接收发出错!!!!!" + exception.Message + "\r\n");
                //clientSocket.Close();

                SaveLog("Socket 连接或接收发出错!!!!!" + exception.Message + "\r\n");

            }
        }
        int sys1 = 0, dia1 = 0, mean1 = 0;
        private void ProceedreceivedData(int type)
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
                SaveLog(DisplayStr);
                SaveLog("--------------------------开始解析-------------------------------\r\n");

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

                int HR = 0, SpO2 = 0, PVCs = 0, RR = 0, PR = 0, Dia = 0, Mean = 0, Sys = 0, CVPm = 0, EtCO2 = 0;
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
                            SaveLog(Testresultstr);
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
                            BedId = Convert.ToInt32(tmp);

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
                SaveLog(Testresultstr1);
                MirayModel jhModel = new MirayModel();
                jhModel.OfficeName1 = OfficeName;
                jhModel.BedId1 = _MzjldId;
                //jhModel.PatientMonitor_IP1 = PatientMonitor_IP;
                jhModel.IPSeq1 = IPSeq;

                jhModel.HR1 = HR;
                jhModel.SpO21 = SpO2;
                jhModel.PVCs1 = PVCs;
                //jhModel.ST_II1 = ST_II;
                jhModel.RR1 = RR;
                jhModel.PR1 = PR;
                jhModel.Dia1 = Dia;
                jhModel.Mean1 = Mean;
                jhModel.Sys1 = Sys;
                if (Sys == 0)
                {
                    Sys = sys1;
                    Dia = dia1;
                    Mean = mean1;
                }
                //float temp = float.Parse((37.5).ToString());
                DateTime now = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                if (mpdal.GetMzjldPoint(now, _MzjldId).Rows.Count == 0)
                {
                    int fa = 0;

                    if (type == 0)
                        //fa = sh.insertJianCeDataMZJLD(_MzjldId, Sys, Dia, Mean, RR, HR, PR, SpO2, new Random().Next(32, 35), 0, now);
                        fa = mpdal.insertJianCeDataMZJLD(_MzjldId, Sys, Dia, Mean, RR, HR, PR, SpO2, new Random().Next(32, 35), 0, now);
                    if (type == 1)
                        //fa = sh.insertJianCeDataPACU(_MzjldId, Sys, Dia, Mean, RR, HR, PR, SpO2, new Random().Next(32, 35), 0, now);
                        fa = dal.insertJianCeDataPACU(_MzjldId, Sys, Dia, Mean, RR, HR, PR, SpO2, new Random().Next(32, 35), 0, now);
                }
                if (Sys != 0)
                {
                    sys1 = Sys;
                    dia1 = Dia;
                    mean1 = Mean;
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
        #region //GE监护函数
        private void timer4_Tick(object sender, EventArgs e)
        {
            timer4.Interval = 1000 * 10;
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
                (sender as DSerialPort).ReadBuffer(_MzjldId, 0);
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

        private void FillipData()
        {
            bool TimerStarted = false;
            string temp1 = PatientIPAddress1.Text;
            //string temp2 = this.txtMzjldid.Text;
            IPAddressInput1 = PatientIPAddress1.Text;
            //BedIDInput1 = this.txtMzjldid.Text;
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
        private delegate void setText();//定义一个线程委托
        public void GatherData()
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
                TEMP1 = 0; temp_effective = false;//温度
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
                    if (recv_Data[0] == 0x0C) MessageBox.Show("收到拒绝联系报文！");
                    if (recv_Data[0] == 0x19) MessageBox.Show("收到丢弃联系报文！");
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
                                                    SPO2 = (int)getFloat(recv_Data, contemp3 + 10);
                                                break;
                                            case 0x4B60:
                                                temp_effective = recv_Data[contemp3 + 6] == 0 ? true : false;
                                                if (temp_effective)
                                                    TEMP1 = getFloat(recv_Data, contemp3 + 10);
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
                                    { //时间字段  BCD格式， 需要转化为十进制
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

                        HasEffectiveData = hr_effective || rr_effective || pr_effective || temp_effective || spo2_effective || sys_effective || dia_effective || map_effective || CVP_map_effective || ABP_map_effective;
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
                } //End While
                if (HasEffectiveData)
                {

                    //  System.DateTime currentTime = new System.DateTime();
                    //  currentTime = System.DateTime.Now;
                    PatientDataBlock PatientData;
                    PatientData.MeasureTime = MeasureDate;
                    PatientData.PR = PR; PatientData.pr_effective = pr_effective;    //脉率
                    PatientData.RR = 0; PatientData.rr_effective = rr_effective;    //呼吸频率
                    PatientData.SPO2 = SPO2; PatientData.spo2_effective = spo2_effective;//血氧饱和度
                    PatientData.TEMP = TEMP1; PatientData.temp_effective = temp_effective;//温度
                    PatientData.HR = HR; PatientData.hr_effective = hr_effective;    //心率
                    PatientData.SYS = SYS; PatientData.sys_effective = sys_effective; //收缩压
                    PatientData.DIA = DIA; PatientData.dia_effective = dia_effective; //舒张压
                    PatientData.MAP = MAP; PatientData.map_effective = map_effective; //平均血压
                    PatientData.CVP_SYS = CVP_SYS; PatientData.CVP_sys_effective = CVP_sys_effective; //中心静脉收缩压
                    PatientData.CVP_DIA = CVP_DIA; PatientData.CVP_dia_effective = CVP_dia_effective; //中心静脉舒张压
                    PatientData.CVP_MAP = CVP_MAP; PatientData.CVP_map_effective = CVP_map_effective; //中心静脉平均血压
                    PatientData.ABP_SYS = ABP_SYS; PatientData.ABP_sys_effective = ABP_sys_effective; //动脉收缩压
                    PatientData.ABP_DIA = ABP_DIA; PatientData.ABP_dia_effective = ABP_dia_effective; //动脉舒张压
                    PatientData.ABP_MAP = ABP_MAP; PatientData.ABP_map_effective = ABP_map_effective; //动脉平均血压

                    //logstr = "Final Saved Data: Time=" + MeasureDate.ToString("yyyy-MM-dd HH:mm:ss") + ";HR=" + HR.ToString() + ";RR=" + RR.ToString() + ";SPO2=" + SPO2.ToString() + ";sys=" + SYS.ToString() + ";DIA=" + DIA.ToString() + ";MAP=" + MAP.ToString() + ";CVP_MAP=" + CVP_MAP.ToString() + ";ABP_MAP=" + ABP_MAP.ToString();
                    //SaveLog(logstr);
                    this.SaveFillipData(PatientData, _MzjldId, 0);

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
                    MessageBox.Show("收到连接中断消息！");
                }
                else
                {
                    MessageBox.Show("收到无法识别消息！");
                }
                Thread.Sleep(Get_Period);
                GetDataTimes++;
            }
        }
        public struct PatientDataBlock
        {
            public DateTime MeasureTime;
            public int PR; public bool pr_effective;    //脉率
            public int RR; public bool rr_effective;    //呼吸频率
            public float SPO2; public bool spo2_effective;//血氧饱和度
            public float TEMP; public bool temp_effective;//温度
            public int HR; public bool hr_effective;    //心率
            public int SYS; public bool sys_effective; //收缩压
            public int DIA; public bool dia_effective; //舒张压
            public int MAP; public bool map_effective; //平均血压
            public int CVP_SYS; public bool CVP_sys_effective; //中心静脉收缩压
            public int CVP_DIA; public bool CVP_dia_effective; //中心静脉舒张压
            public int CVP_MAP; public bool CVP_map_effective; //中心静脉平均血压
            public int ABP_SYS; public bool ABP_sys_effective; //动脉收缩压
            public int ABP_DIA; public bool ABP_dia_effective; //动脉舒张压
            public int ABP_MAP; public bool ABP_map_effective; //动脉平均血压

        }
        public void SaveFillipData(PatientDataBlock PatientInfo, int mzid, int type)
        {
            int PR1 = PatientInfo.PR;
            int RR1 = PatientInfo.RR;
            int SPO21 = (int)PatientInfo.SPO2;
            float TEMP1 = PatientInfo.TEMP;
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
            DateTime now = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
            if (mpdal.GetMzjldPoint(now, _MzjldId).Rows.Count == 0)
            {
                int fa = 0;
                if (ABP_SYS1 != 0 && ABP_DIA1 != 0 && ABP_MAP1 != 0)
                {
                    if (type == 0)
                        fa = mpdal.insertJianCeDataMZJLD(_MzjldId, ABP_SYS1, ABP_DIA1, ABP_MAP1, RR1, HR1, PR1, SPO21, new Random().Next(32, 35), TEMP1, now);
                    if (type == 1)
                        fa = dal.insertJianCeDataPACU(_MzjldId, ABP_SYS1, ABP_DIA1, ABP_MAP1, RR1, HR1, PR1, SPO21, new Random().Next(32, 35), TEMP1, now);
                }
                else
                {
                    if (type == 0)
                        fa = mpdal.insertJianCeDataMZJLD(_MzjldId, SYS1, DIA1, MAP1, RR1, HR1, PR1, SPO21, new Random().Next(32, 35), TEMP1, now);
                    if (type == 1)
                        fa = dal.insertJianCeDataPACU(_MzjldId, SYS1, DIA1, MAP1, RR1, HR1, PR1, SPO21, new Random().Next(32, 35), TEMP1, now);
                }
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

        #region  金科威监护函数

        private void JkwDataResolve()
        {
            bool TimerStarted = false;
            string temp1 = PatientIPAddress1.Text;
            IPAddressInput1 = PatientIPAddress1.Text;
            if (!TimerStarted)
            {
                CheckTimer.Start();
                TimerStarted = true;
            }
            if (ThreadExist_JKW)
            {
                ThreadExist_JKW = false;
                Receiving_JKW.Abort();

                Receiving_JKW = null;
                //Receiving_JKW.IsBackground = true;
                //Receiving_JKW.Priority = ThreadPriority.AboveNormal;

            }
            //if (ThreadExist)
            //{
            //    ThreadExist = false;
            //    Receiving_xy.Abort();//强制结束线程运行
            //    Receiving_xy = null; ////在.NET中，有GC垃圾回收，如果这里不赋为null，不回收内存，甚至永远这么占着，导致内存泄漏。
            //}
            if (!TimerStarted)
            {
                CheckTimer.Start();
                TimerStarted = true;
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
            string temp1 = PatientIPAddress1.Text;
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(temp1.Trim()), port);
            ServerSocket_JKW = new Socket(ipep.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                //绑定本机的一个端口，然后进行监听
                ServerSocket_JKW.Bind(ipep);
                ServerSocket_JKW.Listen(5);
                TempSocket_JKW = ServerSocket_JKW.Accept();

                while (true)
                {
                    ReceivedLength_JKW = TempSocket_JKW.Receive(Raw_Data_JKW);
                    ReceivedTime_JKW = DateTime.Now;
                    string ReceivedString1 = System.Text.Encoding.Default.GetString(Raw_Data_JKW);
                    if (Laststring_JKW != ReceivedString1)
                        ProceedreceivedData_JKW();
                    Laststring_JKW = ReceivedString1;
                }

            }

            catch (Exception exception)
            {
                SockectIsException_JKW = true;
            }

        }
        public void ProceedreceivedData_JKW()
        {
            int type = 0;
            Regex r1 = new Regex("AA-");
            Regex r2 = new Regex("-CC");
            string tempStr = BitConverter.ToString(Raw_Data_JKW, 0, ReceivedLength_JKW);
            Match m = r1.Match(tempStr);
            Match m1 = r2.Match(tempStr);

            if (m.Success && m1.Success)
            {
                string[] strValue = new string[100];

                string jiexiStr = "";
                //if (m1.Index + 1 > tempStr.Length)
                //    jiexiStr = tempStr.Substring(m.Index, m1.Index);
                //else
                //    jiexiStr = tempStr.Substring(m.Index, m1.Index + 1);
                jiexiStr = tempStr.Substring(m.Index, m1.Index + 1);
                if (m1.Index + 2 > tempStr.Length)
                    tempStr = "";
                else
                    tempStr = tempStr.Substring(m1.Index + 2);
                if (jiexiStr.Contains("AA-20"))
                {
                    jiexiStr = jiexiStr.Replace("AA-20", "");
                    jiexiStr = jiexiStr.Replace("-", "");
                    int lenth = jiexiStr.Length / 2;
                    for (int i = 0; i < lenth - 1; i++)
                    {
                        strValue[i] = jiexiStr.Substring(i * 2, 2);
                    }
                    //PR = HexToInt(strValue[1]);
                    SYS = HexToInt(strValue[2]);
                    DIA = HexToInt(strValue[3]);
                    MAP = HexToInt(strValue[4]);
                    TEMP1 = float.Parse(HexToInt(strValue[5]) + "." + HexToInt(strValue[6]));
                    RR = HexToInt(strValue[7]);
                    TEMP2 = float.Parse(HexToInt(strValue[11]) + "." + HexToInt(strValue[12]));
                    ////  label23.Text = "脉搏为：" + PR.ToString()
                    //      + " 舒张压为：" + DIA.ToString() + "  收缩压为：" + SYS.ToString()
                    //      + " 平均压为：" + MAP.ToString() + " 心率为：" + RR.ToString()
                    //      + "  体温1为：" + TEMP1.ToString() + "  体温2为：" + TEMP2.ToString();
                }
                else if (jiexiStr.Contains("AA-30"))
                {
                    jiexiStr = jiexiStr.Replace("AA-30", "");
                    jiexiStr = jiexiStr.Replace("-", "");
                    int lenth = jiexiStr.Length / 2;
                    for (int i = 0; i < lenth - 1; i++)
                    {
                        strValue[i] = jiexiStr.Substring(i * 2, 2);
                    }
                    PR = HexToInt(strValue[1]);
                    SPO2 = HexToInt(strValue[2]);
                    //label29.Text = "SPO2为：" + SPO2.ToString();
                }
                else if (jiexiStr.Contains("AA-50"))
                {
                    jiexiStr = jiexiStr.Replace("AA-50", "");
                    jiexiStr = jiexiStr.Replace("-", "");
                    int lenth = jiexiStr.Length / 2;
                    for (int i = 0; i < lenth - 1; i++)
                    {
                        strValue[i] = jiexiStr.Substring(i * 2, 2);
                    }
                    ETCO2 = HexToInt(strValue[1]);
                    //label28.Text = "ETCO2为：" + ETCO2.ToString();
                }
                else if (jiexiStr.Contains("AA-60"))
                {
                    jiexiStr = jiexiStr.Replace("AA-60", "");
                    jiexiStr = jiexiStr.Replace("-", "");
                    int lenth = jiexiStr.Length / 2;
                    for (int i = 0; i < lenth - 1; i++)
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

                }
                DateTime now = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                if (mpdal.GetMzjldPoint(now, _MzjldId).Rows.Count == 0)
                {
                    int fa = 0;
                    if (ABP_SYS != 0 && ABP_DIA != 0 && ABP_MAP != 0)
                    {
                        if (type == 0)
                            fa = mpdal.insertJianCeDataMZJLD(_MzjldId, ABP_SYS, ABP_DIA, ABP_MAP, RR, HR, PR, SPO2, new Random().Next(32, 35), TEMP1, now);
                        if (type == 1)
                            fa = dal.insertJianCeDataPACU(_MzjldId, ABP_SYS, ABP_DIA, ABP_MAP, RR, HR, PR, SPO2, new Random().Next(32, 35), TEMP1, now);
                    }
                    else
                    {
                        if (type == 0)
                            fa = mpdal.insertJianCeDataMZJLD(_MzjldId, SYS, DIA, MAP, RR, HR, PR, SPO2, new Random().Next(32, 35), TEMP1, now);
                        if (type == 1)
                            fa = dal.insertJianCeDataPACU(_MzjldId, SYS, DIA, MAP, RR, HR, PR, SPO2, new Random().Next(32, 35), TEMP1, now);
                    }
                }

            }
            //int type = 0;
            //string tempStr =BitConverter.ToString(Raw_Data_JKW, 0, ReceivedLength_JKW);

            //while (true)
            //{
            //    string[] strValue = new string[100];
            //    Regex r1 = new Regex("AA");
            //    Regex r2 = new Regex("CC");
            //    Match m = r1.Match(tempStr);
            //    Match m1 = r2.Match(tempStr);
            //    if (m.Success && m1.Success)
            //    {
            //        string jiexiStr = "";
            //        if (m1.Index + 1 > tempStr.Length)
            //            jiexiStr = tempStr.Substring(m.Index, m1.Index);
            //        else
            //            jiexiStr = tempStr.Substring(m.Index, m1.Index + 1);
            //        tempStr = tempStr.Substring(m1.Index + 4);
            //        if (jiexiStr.Contains("AA-20"))
            //        {
            //            jiexiStr = jiexiStr.Replace("AA-20", "");
            //            jiexiStr = jiexiStr.Replace("-", "");
            //            int lenth = jiexiStr.Length / 2;
            //            for (int i = 0; i < lenth - 1; i++)
            //            {
            //                strValue[i] = jiexiStr.Substring(i * 2, 2);
            //            }
            //            PR = HexToInt(strValue[1]);
            //            SYS = HexToInt(strValue[2]);
            //            DIA = HexToInt(strValue[3]);
            //            MAP = HexToInt(strValue[4]);
            //            TEMP1 = float.Parse(HexToInt(strValue[5]) + "." + HexToInt(strValue[6]));
            //            RR = HexToInt(strValue[7]);
            //            TEMP2 = float.Parse(HexToInt(strValue[11]) + "." + HexToInt(strValue[12]));
            //          ////  label23.Text = "脉搏为：" + PR.ToString()
            //          //      + " 舒张压为：" + DIA.ToString() + "  收缩压为：" + SYS.ToString()
            //          //      + " 平均压为：" + MAP.ToString() + " 心率为：" + RR.ToString()
            //          //      + "  体温1为：" + TEMP1.ToString() + "  体温2为：" + TEMP2.ToString();
            //        }
            //        else if (jiexiStr.Contains("AA-30"))
            //        {
            //            jiexiStr = jiexiStr.Replace("AA-30", "");
            //            jiexiStr = jiexiStr.Replace("-", "");
            //            int lenth = jiexiStr.Length / 2;
            //            for (int i = 0; i < lenth - 1; i++)
            //            {
            //                strValue[i] = jiexiStr.Substring(i * 2, 2);
            //            }
            //            SPO2 = HexToInt(strValue[1]);
            //            //label29.Text = "SPO2为：" + SPO2.ToString();
            //        }
            //        else if (jiexiStr.Contains("AA-50"))
            //        {
            //            jiexiStr = jiexiStr.Replace("AA-50","");
            //            jiexiStr = jiexiStr.Replace("-", "");
            //            int lenth = jiexiStr.Length / 2;
            //            for (int i = 0; i < lenth - 1; i++)
            //            {
            //                strValue[i] = jiexiStr.Substring(i * 2, 2);
            //            }
            //            ETCO2 = HexToInt(strValue[1]);
            //            //label28.Text = "ETCO2为：" + ETCO2.ToString();
            //        }
            //        DateTime now = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
            //        if (dal.selectJianCeData(now, _MzjldId, type).Rows.Count == 0)
            //        {
            //            int fa = 0;
            //            if (ABP_SYS != 0 && ABP_DIA != 0 && ABP_MAP != 0)
            //            {
            //                if (type == 0)
            //                    fa = dal.insertJianCeDataMZJLD(_MzjldId, ABP_SYS, ABP_DIA, ABP_MAP, RR, HR, PR, SPO2, new Random().Next(32, 35), TEMP1, now);
            //                if (type == 1)
            //                    fa = dal.insertJianCeDataPACU(_MzjldId, ABP_SYS, ABP_DIA, ABP_MAP, RR, HR, PR, SPO2, new Random().Next(32, 35), TEMP1, now);
            //            }
            //            else
            //            {
            //                if (type == 0)
            //                    fa = dal.insertJianCeDataMZJLD(_MzjldId, SYS, DIA, MAP, RR, HR, PR, SPO2, new Random().Next(32, 35), TEMP1, now);
            //                if (type == 1)
            //                    fa = dal.insertJianCeDataPACU(_MzjldId, SYS, DIA, MAP, RR, HR, PR, SPO2, new Random().Next(32, 35), TEMP1, now);
            //            }
            //        }
            //    }
            //    else
            //        break;
            //}
        }

        private int HexToInt(string shiliu)
        {
            int shi = 0;
            try
            {
                int Int = Convert.ToInt32(shiliu, 16);
                shi = Int;
            }
            catch (Exception Err)
            { MessageBox.Show(Err.Message); }
            return shi;
        }


        #endregion

        private void timer1_Tick(object sender, EventArgs e)//循环取得监护仪检测数据
        {
            DateTime Now = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
            int countN = 1;//dal.CopyData(_MzjldId, ksjcTime);
            if (countN == 0)
            {
                ksjcTime = ksjcTime.AddMinutes(1);
                timer1.Interval = 1000 * 60;
            }
            else
            {
                BindJHDian();
                foreach (ZoomRegion ZR in ZoomRegionList)
                {
                    if (Now >= ZR.AStartTime && Now < ZR.EndTime)
                    {
                        timer1.Interval = 800 * ZR.Interval * 60;//1000 * ZR.Interval * 60;
                    }
                }
                pictureBox4.Refresh();
                pictureBox3.Refresh();
                pictureBox2.Refresh();
            }
        }

        public void BindJHDian()//监护点赋值
        {
            ssyList.Clear(); szyList.Clear(); xlList.Clear(); twList.Clear(); hxlList.Clear();
            mboList.Clear(); spo2List.Clear(); etco2List.Clear(); jhxmValue.Clear();

            //RecordTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP
            DataTable datadt = new DataTable();

            var dtmz = _MzjldDal.GetMzjldByMzjldId(_MzjldId);
            if (dtmz.Rows.Count > 0 && dtmz.Rows[0]["IsZoom"].ToString() == "0")
            {
                var zr = ZoomRegionList.FirstOrDefault();
                datadt = mpdal.GetByMzjldID(_MzjldId, zr.AStartTime, zr.EndTime, zr.Interval);
            }
            else
            {
                foreach (ZoomRegion zr in ZoomRegionList)
                {

                    DataTable dt = mpdal.GetByTimeSpan(_MzjldId, zr.AStartTime, zr.EndTime, zr.Interval);
                    if (dt.Rows.Count > 0)
                    {
                        if (datadt != null)
                        {
                            datadt.Merge(dt);
                        }
                        else
                        {
                            datadt = dt;
                        }
                    }
                }
            }

            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.point p1 = new adims_MODEL.point();//收缩压记录点
                try
                {
                    p1.D = Convert.ToDateTime(datadt.Rows[i][0]);
                    if (datadt.Rows[i]["NIBPS"].ToString() == "")
                    {
                        p1.V = 0;
                    }
                    else
                    {
                        p1.V = Convert.ToInt32(datadt.Rows[i]["NIBPS"]);
                    }
                }
                catch (Exception)
                {
                    p1.V = 0;
                }

                p1.Lx = 1;
                ssyList.Add(p1);
            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.point p2 = new adims_MODEL.point();//舒张压记录点
                try
                {
                    p2.D = Convert.ToDateTime(datadt.Rows[i][0]);
                    if (datadt.Rows[i]["NIBPD"].ToString() == "")

                    {
                        p2.V = 0;
                    }
                    else
                    {
                        p2.V = Convert.ToInt32(datadt.Rows[i]["NIBPD"]);
                    }
                }
                catch (Exception)
                {
                    p2.V = 0;
                }
                p2.Lx = 2;
                szyList.Add(p2);
            }

            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.point p3 = new adims_MODEL.point();//脉搏记录点
                try
                {
                    p3.D = Convert.ToDateTime(datadt.Rows[i][0]);
                    if (datadt.Rows[i][3].ToString() == "")
                    {
                        if (datadt.Rows[i]["HR"].ToString() != "")
                        {
                            p3.V = Convert.ToInt32(datadt.Rows[i]["HR"]);
                        }
                        else
                        {
                            p3.V = 0;
                        }
                    }
                    else
                    {
                        p3.V = Convert.ToInt32(datadt.Rows[i][3]);
                    }
                }
                catch (Exception)
                {

                    p3.V = 0;
                }
                p3.Lx = 3;
                mboList.Add(p3);
            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.point p4 = new adims_MODEL.point();//呼吸记录点
                try
                {
                    p4.D = Convert.ToDateTime(datadt.Rows[i][0]);
                    if (datadt.Rows[i]["RRC"].ToString() == "")
                    {
                        p4.V = 0;
                    }
                    else
                    {
                        p4.V = Convert.ToInt32(datadt.Rows[i]["RRC"]);
                    }

                }
                catch (Exception)
                {
                    p4.V = 0;
                }
                p4.Lx = 4;
                hxlList.Add(p4);
            }

            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.tw_point p5 = new adims_MODEL.tw_point();//体温
                try
                {
                    p5.D = Convert.ToDateTime(datadt.Rows[i][0]);
                    if (datadt.Rows[i]["TEMP"].ToString() == "")
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
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.point p6 = new adims_MODEL.point();//ETCO2记录点
                try
                {
                    p6.D = Convert.ToDateTime(datadt.Rows[i][0]);
                    if (datadt.Rows[i]["ETCO2"].ToString() == "")
                    {
                        p6.V = 0;
                    }
                    else
                    {
                        p6.V = Convert.ToInt32(datadt.Rows[i]["ETCO2"]);
                    }
                }
                catch (Exception)
                {

                    p6.V = 0;
                }
                p6.Lx = 6;
                etco2List.Add(p6);
            }

            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.jhxm jhxmt = new adims_MODEL.jhxm();
                try
                {
                    jhxmt.D = Convert.ToDateTime(datadt.Rows[i][0]);
                    if (datadt.Rows[i]["SPO2"].ToString() == "")
                    {
                        jhxmt.V = 0;
                    }
                    else
                    {
                        jhxmt.V = Convert.ToInt32(datadt.Rows[i]["SPO2"]);
                    }

                }
                catch (Exception)
                {

                    jhxmt.V = 0;
                }
                jhxmt.Sy = "SpO2";
                jhxmValue.Add(jhxmt);
            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.jhxm jhxmt = new adims_MODEL.jhxm();
                try
                {
                    jhxmt.D = Convert.ToDateTime(datadt.Rows[i][0]);
                    if (datadt.Rows[i]["ETCO2"].ToString() == "")
                    {
                        jhxmt.V = 0;
                    }
                    else
                    {
                        jhxmt.V = Convert.ToInt32(datadt.Rows[i]["ETCO2"]);
                    }
                }
                catch (Exception)
                {
                    jhxmt.V = 0;
                }
                jhxmt.Sy = "ETCO2";
                jhxmValue.Add(jhxmt);

            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.jhxm jhxmt = new adims_MODEL.jhxm();
                try
                {
                    jhxmt.D = Convert.ToDateTime(datadt.Rows[i][0]);
                    if (datadt.Rows[i]["CVP"].ToString() == "")
                    {
                        jhxmt.V = 0;
                    }
                    else
                    {
                        jhxmt.V = Convert.ToInt32(datadt.Rows[i]["CVP"]);
                    }
                }
                catch (Exception)
                {

                    jhxmt.V = 0;
                }
                jhxmt.Sy = "CVP";
                jhxmValue.Add(jhxmt);
            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.jhxm jhxmt = new adims_MODEL.jhxm();
                try
                {
                    jhxmt.D = Convert.ToDateTime(datadt.Rows[i][0]);
                    if (datadt.Rows[i]["qdy"].ToString() == "")
                    {
                        jhxmt.V = 0;
                    }
                    else
                    {
                        jhxmt.V = Convert.ToInt32(datadt.Rows[i]["qdy"]);
                    }
                }
                catch (Exception)
                {
                    jhxmt.V = 0;
                }
                jhxmt.Sy = "气道压";
                jhxmValue.Add(jhxmt);
            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.jhxm jhxmt = new adims_MODEL.jhxm();
                try
                {
                    jhxmt.D = Convert.ToDateTime(datadt.Rows[i][0]);
                    if (datadt.Rows[i]["sdz"].ToString() == "")
                    {
                        jhxmt.V = 0;
                    }
                    else
                    {
                        jhxmt.V = Convert.ToInt32(datadt.Rows[i]["sdz"]);
                    }
                }
                catch (Exception)
                {

                    jhxmt.V = 0;
                }
                jhxmt.Sy = "深度值";
                jhxmValue.Add(jhxmt);
            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.jhxm jhxmt = new adims_MODEL.jhxm();
                try
                {


                    jhxmt.D = Convert.ToDateTime(datadt.Rows[i][0]);
                    if (datadt.Rows[i]["jsz"].ToString() == "")
                    {
                        jhxmt.V = 0;
                    }
                    else
                    {
                        jhxmt.V = Convert.ToInt32(datadt.Rows[i]["jsz"]);
                    }
                }
                catch (Exception)
                {
                    jhxmt.V = 0;
                }
                jhxmt.Sy = "肌松值";
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
            DataTable dtCL = dal.GetCL(_MzjldId);
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
        private void btnSzsj_Click(object sender, EventArgs e)
        {
            addSzsj F1 = new addSzsj(_MzjldId, 0);
            F1.ShowDialog();
            BindSZSJ();
            pictureBox3.Refresh();
            listBoxSZSJ.Focus();
        }

        private void btnTsyy_Click(object sender, EventArgs e)
        {
            addTsyy F1 = new addTsyy(_MzjldId);
            F1.ShowDialog();
            BindTsyy();
            pictureBox3.Refresh();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            addZhenTong F1 = new addZhenTong(_MzjldId);
            F1.ShowDialog();
            BindZhenTongYao();
            pictureBox3.Refresh();
        }
        DateTime MaxPointTime = new DateTime();
        private void btnPrintView_Click(object sender, EventArgs e) /// 打印预览
        {
            int JsFlag = 0;
            string YwName = string.Empty;

            DataTable dtMax = mpdal.GetMaxPoint(_MzjldId);
            if (dtMax.Rows[0][0].ToString() != "")
            {
                MaxPointTime = Convert.ToDateTime(dtMax.Rows[0][0].ToString());
            }
            else
            {
                MaxPointTime = DateTime.Now;
            }
            if (_BgTime > MaxPointTime)
            {
                MaxPointTime = _BgTime;
            }

            if (JsFlag > 0)
            {
                MessageBox.Show(YwName + "没用结束，不能打印");
                return;
            }
            FirstOpen = dtOtime.Value;
            ptime = FirstOpen;
            BindZoomRegionListPrint(FirstOpen);
            printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
            iYema = 1;
            PrintSzsjIndex = 0;
        }
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("K16", 760, 1020);
            iYema = 1;
            PrintSzsjIndex = 0;
            ptime = otime;
            //printDocument1.DefaultPageSettings.PaperSize =
            //           new System.Drawing.Printing.PaperSize("A4", 820, 1160);

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int xLeft = 70, yTop = 0;//整体位置控制
            int xStart = xLeft + 130;
            int xEnd = xStart + 420;
            int xRight = xEnd + 110;
            string title = "        天 津 红 桥 医 院 麻 醉 记 录 单";//标题       
            Pen ptp = Pens.Gray;//普通画笔
            Pen ptp1 = Pens.Black;//普通画笔
            //Pen ptp = new Pen(Brushes.Black, 0.1f);
            Pen pblack2 = new Pen(Brushes.Black, 2);
            Pen pxuxian = new Pen(Brushes.Gray);
            pxuxian.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Font ptzt14 = new System.Drawing.Font("宋体", 14);//标题
            Font ptzt13 = new System.Drawing.Font("宋体", 13);//标题                      
            Font ptzt12 = new Font("宋体", 12);//填入栏目字体
            Font ptzt11 = new Font("宋体", 11);//填入栏目字体
            Font ptzt10 = new Font("宋体", 10);//填入栏目字体
            Font ptzt9 = new Font("宋体", 9);//填入栏目字体          
            Font ptzt8 = new Font("宋体", 8);//填入栏目字体
            Font ptzt88 = new Font("宋体", 8);//填入栏目字体
            Font ht7 = new Font("黑体", 7);//填入栏目字体
            Font ptzt7 = new Font("宋体", 7);//填入栏目字体 
            Font ptzt7_U = new Font("宋体", 7, FontStyle.Underline, GraphicsUnit.Point);//下划线字体
            Font ptzt6 = new Font("宋体", 6);//填入栏目字体
            Font ptzt5 = new Font("宋体", 5);//填入栏目字体
            Font ptzt4 = new Font("宋体", 4);//填入栏目字体
            Pen pred2 = new Pen(Brushes.Red, 2);
            Pen pblue2 = new Pen(Brushes.Blue, 1);
            e.Graphics.DrawString(title, ptzt13, Brushes.Black, new Point(80 + xLeft, 20 + yTop));
            int Y_unLine = yTop + 63;
            int YY = yTop + 50;
            int SZYY = 0;
            int FYYY = YY;//控制分页y

            #region 页头
            e.Graphics.DrawString("科别：", ptzt8, Brushes.Black, new Point(80 + xLeft, YY));
            e.Graphics.DrawString(txtBingQu.Text == "" ? "/" : txtBingQu.Text, ptzt8, Brushes.Black, new Point(115 + xLeft, YY));

            e.Graphics.DrawLine(ptp, new Point(110 + xLeft, Y_unLine), new Point(210 + xLeft, Y_unLine));
            e.Graphics.DrawString("床号：", ptzt8, Brushes.Black, new Point(220 + xLeft, YY));
            e.Graphics.DrawString(txtBednumber.Text == "" ? "/" : txtBednumber.Text, ptzt8, Brushes.Black, new Point(255 + xLeft, YY));
            e.Graphics.DrawLine(ptp, new Point(250 + xLeft, Y_unLine), new Point(300 + xLeft, Y_unLine));
            e.Graphics.DrawString("病历号：", ptzt8, Brushes.Black, new Point(340 + xLeft, YY));
            e.Graphics.DrawString(txtZhuYuanHao.Text == "" ? "/" : txtZhuYuanHao.Text, ptzt8, Brushes.Black, new Point(385 + xLeft, YY));
            e.Graphics.DrawLine(ptp, new Point(380 + xLeft, Y_unLine), new Point(470 + xLeft, Y_unLine));

            #endregion
            //↑画标题一块的东西
            Y_unLine = YY + 15; YY = YY + 20;


            #region 打印病人基本信息
            YY = YY + 5; Y_unLine = YY + 13;
            e.Graphics.DrawString("姓名 ", ptzt8, Brushes.Black, new Point(5 + xLeft, YY));
            e.Graphics.DrawString(txtName.Text == "" ? "/" : txtName.Text, ptzt8, Brushes.Black, new Point(35 + xLeft, YY));
            e.Graphics.DrawLine(ptp, new Point(30 + xLeft, Y_unLine), new Point(100 + xLeft, Y_unLine));
            e.Graphics.DrawString("年龄 ", ptzt8, Brushes.Black, new Point(110 + xLeft, YY));
            e.Graphics.DrawString(txtAge.Text == "" ? "/" : txtAge.Text, ptzt8, Brushes.Black, new Point(140 + xLeft, YY));
            e.Graphics.DrawLine(ptp, new Point(135 + xLeft, Y_unLine), new Point(165 + xLeft, Y_unLine));
            e.Graphics.DrawString("岁  性别 ", ptzt8, Brushes.Black, new Point(170 + xLeft, YY));
            e.Graphics.DrawString(txtSex.Text == "" ? "/" : txtSex.Text, ptzt8, Brushes.Black, new Point(225 + xLeft, YY));
            e.Graphics.DrawLine(ptp, new Point(220 + xLeft, Y_unLine), new Point(240 + xLeft, Y_unLine));
            e.Graphics.DrawString("身高 ", ptzt8, Brushes.Black, new Point(250 + xLeft, YY));
            e.Graphics.DrawString(txtHeight.Text == "" ? "/" : txtHeight.Text, ptzt8, Brushes.Black, new Point(285 + xLeft, YY));
            e.Graphics.DrawLine(ptp, new Point(280 + xLeft, Y_unLine), new Point(310 + xLeft, Y_unLine));
            e.Graphics.DrawString("cm  体重 ", ptzt8, Brushes.Black, new Point(315 + xLeft, YY));
            e.Graphics.DrawString(txtWeight.Text == "" ? "/" : txtWeight.Text, ptzt8, Brushes.Black, new Point(365 + xLeft, YY));
            e.Graphics.DrawLine(ptp, new Point(360 + xLeft, Y_unLine), new Point(390 + xLeft, Y_unLine));
            e.Graphics.DrawString("kg  ASA分级 ", ptzt8, Brushes.Black, new Point(395 + xLeft, YY));
            e.Graphics.DrawString(cmbASA.Text == "" ? "/" : cmbASA.Text, ptzt8, Brushes.Black, new Point(465 + xLeft, YY));
            e.Graphics.DrawLine(ptp, new Point(460 + xLeft, Y_unLine), new Point(500 + xLeft, Y_unLine));

            e.Graphics.DrawString("□ 急诊", ptzt8, Brushes.Black, new Point(520 + xLeft, YY));
            e.Graphics.DrawString("□ 择期", ptzt8, Brushes.Black, new Point(570 + xLeft, YY));

            if (cbJizhen.Checked)
                e.Graphics.DrawLines(pblue2, new Point[] { new Point(520 + xLeft, YY), new Point(525 + xLeft, YY + 10), new Point(535 + xLeft, YY) });
            if (cbZeqi.Checked)
                e.Graphics.DrawLines(pblue2, new Point[] { new Point(570 + xLeft, YY), new Point(575 + xLeft, YY + 8), new Point(585 + xLeft, YY) });

            YY = YY + 20; Y_unLine = YY + 13;
            e.Graphics.DrawString("入手术室血压 ", ptzt8, Brushes.Black, new Point(5 + xLeft, YY));
            e.Graphics.DrawString(txtXueya.Text == "" ? "/" : txtXueya.Text, ptzt8, Brushes.Black, new Point(80 + xLeft, YY));
            e.Graphics.DrawLine(ptp, new Point(75 + xLeft, Y_unLine), new Point(150 + xLeft, Y_unLine));
            e.Graphics.DrawString("mmHg  脉搏 ", ptzt8, Brushes.Black, new Point(155 + xLeft, YY));
            e.Graphics.DrawString(txtMaibo.Text == "" ? "/" : txtMaibo.Text, ptzt8, Brushes.Black, new Point(225 + xLeft, YY));
            e.Graphics.DrawLine(ptp, new Point(220 + xLeft, Y_unLine), new Point(250 + xLeft, Y_unLine));
            e.Graphics.DrawString("次/分  呼吸 ", ptzt8, Brushes.Black, new Point(255 + xLeft, YY));
            e.Graphics.DrawString(txtHuxi.Text == "" ? "/" : txtHuxi.Text, ptzt8, Brushes.Black, new Point(325 + xLeft, YY));
            e.Graphics.DrawLine(ptp, new Point(320 + xLeft, Y_unLine), new Point(350 + xLeft, Y_unLine));
            e.Graphics.DrawString("次/分  血型 ", ptzt8, Brushes.Black, new Point(355 + xLeft, YY));
            e.Graphics.DrawString(cmbXueXing.Text == "" ? "/" : cmbXueXing.Text, ptzt8, Brushes.Black, new Point(425 + xLeft, YY));
            e.Graphics.DrawLine(ptp, new Point(420 + xLeft, Y_unLine), new Point(470 + xLeft, Y_unLine));
            e.Graphics.DrawString("手术日期 " + dtOdate.Text, ptzt8, Brushes.Black, new Point(490 + xLeft, YY));
            e.Graphics.DrawLine(ptp, new Point(540 + xLeft, Y_unLine), new Point(xRight - 5, Y_unLine));

            YY = YY + 20; Y_unLine = YY + 13;
            e.Graphics.DrawString("手术前诊断  ", ptzt8, Brushes.Black, new Point(5 + xLeft, YY));
            if (txtSqzd.Text.Trim() != "")
            {
                if (txtSqzd.Text.Trim().Length <= 130)
                {

                    string str1_zd = "";
                    int StrLength_zd = txtSqzd.Text.Trim().Length;
                    int row_zd = StrLength_zd / 65;

                    for (int i = 0; i <= row_zd;)//49个字符就换行
                    {

                        if (i < row_zd)
                            str1_zd = txtSqzd.Text.ToString().Substring(i * 65, 65); //从i*65个开始，截取65个字符串
                        else
                            str1_zd = txtSqzd.Text.ToString().Substring(i * 65);
                        e.Graphics.DrawString(str1_zd, ptzt7, Brushes.Black, xLeft + 75, YY);
                        e.Graphics.DrawLine(ptp, new Point(70 + xLeft, Y_unLine), new Point(xRight - 5, Y_unLine));
                        i++;
                        if (i > row_zd)
                        {

                        }
                        else
                        {
                            if (i == 2)
                            {
                                break;
                            }
                            YY = YY + 20; Y_unLine = YY + 13;
                        }

                    }
                }
                else
                {
                    string str1_zd = "";
                    int StrLength_zd = txtSqzd.Text.Trim().Length;
                    int row_zd = StrLength_zd / 90;

                    for (int i = 0; i <= row_zd;)//85个字符就换行
                    {

                        if (i < row_zd)
                            str1_zd = txtSqzd.Text.ToString().Substring(i * 90, 90); //从i*49个开始，截取49个字符串
                        else
                            str1_zd = txtSqzd.Text.ToString().Substring(i * 90);
                        e.Graphics.DrawString(str1_zd, ptzt5, Brushes.Black, xLeft + 75, YY);
                        e.Graphics.DrawLine(ptp, new Point(70 + xLeft, Y_unLine), new Point(xRight - 5, Y_unLine));
                        i++;
                        if (i > row_zd)
                        {

                        }
                        else
                        {
                            if (i == 2)
                            {
                                break;
                            }
                            YY = YY + 20; Y_unLine = YY + 13;
                        }

                    }
                }
            }
            else
            {

                e.Graphics.DrawString("/", ptzt8, Brushes.Black, new Point(75 + xLeft, YY));
                e.Graphics.DrawLine(ptp, new Point(70 + xLeft, Y_unLine), new Point(xRight - 5, Y_unLine));
            }

            YY = YY + 20; Y_unLine = YY + 13;
            e.Graphics.DrawString("拟施手术  ", ptzt8, Brushes.Black, new Point(5 + xLeft, YY));
            e.Graphics.DrawString(txtNssss.Text == "" ? "/" : txtNssss.Text, ptzt8, Brushes.Black, new Point(55 + xLeft, YY));
            e.Graphics.DrawLine(ptp, new Point(50 + xLeft, Y_unLine), new Point(xRight - 5, Y_unLine));

            YY = YY + 20; Y_unLine = YY + 13;
            if (string.IsNullOrEmpty(txtSqyy.Text.Trim()))
                txtSqyy.Text = " 无";
            e.Graphics.DrawString("术前用药  " + txtSqyy.Text, ptzt8, Brushes.Black, new Point(5 + xLeft, YY));
            e.Graphics.DrawLine(ptp, new Point(50 + xLeft, Y_unLine), new Point(350 + xLeft, Y_unLine));

            //e.Graphics.DrawString("手术体位 " + this.cmbTiwei.Text, ptzt8, Brushes.Black, new Point(310 + xLeft, YY));
            //e.Graphics.DrawLine(ptp, new Point(360 + xLeft, Y_unLine), new Point(500 + xLeft, Y_unLine));
            if (string.IsNullOrEmpty(txtTSBQing.Text.Trim()))
                txtTSBQing.Text = " 无";
            e.Graphics.DrawString("特殊情况  " + txtTSBQing.Text.Trim(), ptzt8, Brushes.Black, new Point(360 + xLeft, YY));
            e.Graphics.DrawLine(ptp, new Point(410 + xLeft, Y_unLine), new Point(xRight - 5, Y_unLine));


            YY = YY + 25; Y_unLine = YY + 13;
            e.Graphics.DrawLine(ptp, new Point(xLeft, YY), new Point(xRight, YY));
            //↑画边框
            #endregion

            #region 打印时间和分页   
            DateTime PrintEndTime = new DateTime();//打印截止时间判断        
            DateTime PageStartTime = new DateTime();//当前页开始时间
            PageStartTime = ptime; //当前打印页时间开始时间 
            PrintEndTime = MaxPointTime;
            //DataTable dtMax = mpdal.GetMaxPoint(_MzjldId);

            //if (dtMax.Rows[0][0].ToString() == "")
            //    PrintEndTime = DateTime.Now;
            //else
            //    PrintEndTime =MaxPointTime

            int addMin = TextValueLimit.XtoMinutePrint(2 * 210, ZoomRegionListPrint);//210分钟*每分钟2像素
            DateTime PageEndTime = PageStartTime.AddMinutes(addMin);

            e.Graphics.DrawString("时间(分钟)", ptzt8, Brushes.Black, new Point(10 + xLeft, YY + 2));
            for (int i = 0; i < 8; i++) //打印检测时间
            {
                ptime = PageStartTime;
                int add = TextValueLimit.XtoMinutePrint(i * 60, ZoomRegionListPrint);
                ptime = ptime.AddMinutes(add);
                e.Graphics.DrawString(ptime.ToString("HH:mm"), ptzt7, Brushes.Black, new Point(xStart - 10 + 60 * i, YY + 2));

            }

            #endregion

            #region  打印用药区域
            YY = YY + 18; SZYY = YY;
            for (int i = 0; i < 22; i++)//画横实线
            {
                if (i == 0)
                    e.Graphics.DrawLine(ptp1, new Point(xLeft, YY + 12 * i + yTop), new Point(xRight, YY + 12 * i + yTop));
                else if (i == 12 || i == 21)
                    e.Graphics.DrawLine(ptp1, new Point(xLeft, YY + 12 * i + yTop), new Point(xEnd, YY + 12 * i + yTop));

                else
                    e.Graphics.DrawLine(ptp, new Point(15 + xLeft, YY + 12 * i + yTop), new Point(xEnd, YY + 12 * i + yTop));
            }
            e.Graphics.DrawLine(ptp, new Point(15 + xLeft, YY + yTop), new Point(15 + xLeft, YY + 28 * 12 + yTop));


            for (int i = 0; i <= 42; i++)
            {
                if (i % 3 == 0)//画竖实线
                {
                    e.Graphics.DrawLine(ptp, new Point(xStart + 10 * i, YY + yTop), new Point(xStart + 10 * i, YY + 28 * 12 + yTop));
                }
                else//画竖虚线
                {
                    e.Graphics.DrawLine(pxuxian, new Point(xStart + 10 * i, YY + yTop), new Point(xStart + 10 * i, YY + 28 * 12 + yTop));
                }
            }
            //e.Graphics.DrawString("气\n吸\n药", ptzt7, Brushes.Black, new Point(22 + xLeft, YY ));
            e.Graphics.DrawString("\n\n\n全\n麻\n局\n麻\n药", ptzt7, Brushes.Black, new Point(2 + xLeft, YY + 12 * 1));
            e.Graphics.DrawString("输\n液\n输\n血", ptzt7, Brushes.Black, new Point(2 + xLeft, YY + 12 * 15));
            //e.Graphics.DrawString("输\n血", ptzt7, Brushes.Black, new Point(22 + xLeft, YY + 12 * 24 + 10));
            #endregion

            #region 打印气体
            ArrayList sssQT = new ArrayList();
            int qti = 0;   //起步位置
            foreach (adims_MODEL.Yongyao mzqt in mzqtList)
            {
                if (sssQT.Contains(mzqt.Name))
                    qti = qti - 1;
                if (!sssQT.Contains(mzqt.Name))
                    e.Graphics.DrawString(mzqt.Name, mzqt.Name.Length < 7 ? ptzt7 : ptzt7,
                        Brushes.Black, new PointF(20 + xLeft, YY + qti * 12 + yTop + 3));

                int x1 = (TextValueLimit.TimeToXprint(mzqt.KsTime, ZoomRegionListPrint) + xStart);
                int y1 = YY + qti * 12 + 4;
                DateTime dtend = DateTime.Now;
                if (mzqt.Bz == 1)
                {
                    dtend = MaxPointTime;
                }
                else
                {
                    dtend = mzqt.JsTime;
                }
                if (mzqt.JsTime > MaxPointTime)
                {
                    dtend = MaxPointTime;
                }
                int x2 = (TextValueLimit.TimeToXprint(dtend, ZoomRegionListPrint) + xStart);
                if (x1 >= xStart && x1 < xEnd)
                {
                    e.Graphics.DrawString(mzqt.Yl.ToString() + " " + mzqt.Dw, ptzt7, Brushes.Blue, new Point(x1 + 3, yTop + y1 - 6));
                    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1 + yTop), new Point(x1 - 3, y1 + 6 + yTop), new Point(x1 + 3, y1 + 6 + yTop) });
                }

                if (x2 > xStart && x2 <= xEnd)
                {
                    if (mzqt.Bz == 1)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x2, y1 + 3), new Point(x2 - 5, y1));
                        e.Graphics.DrawLine(pred2, new Point(x2, y1 + 3), new Point(x2 - 5, y1 + 6));
                    }
                    if (mzqt.Bz == 2)
                    {
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1 + yTop), new Point(x2 - 3, y1 + 6 + yTop), new Point(x2 + 3, y1 + 6 + yTop) });
                    }

                }
                if (x1 >= xStart && x1 < xEnd && x2 > xStart && x2 < xEnd)
                {
                    e.Graphics.DrawLine(pred2, new Point(x1, y1 + yTop + 3), new Point(x2, y1 + yTop + 3));
                }
                if (x1 >= xStart && x1 < xEnd && x2 > xEnd)
                {
                    e.Graphics.DrawLine(pred2, new Point(x1, y1 + yTop + 3), new Point(xEnd, y1 + yTop + 3));
                }
                if (x1 < xStart && x2 > xStart && x2 <= xEnd)
                {
                    e.Graphics.DrawLine(pred2, new Point(xStart, y1 + yTop + 3), new Point(x2, y1 + yTop + 3));
                }
                if (x1 < xStart && x2 > xEnd)
                {
                    e.Graphics.DrawLine(pred2, new Point(xStart, y1 + yTop + 3), new Point(xEnd, y1 + yTop + 3));
                }

                qti++;
                sssQT.Add(mzqt.Name);
            }

            #endregion

            #region 打印全麻药
            ArrayList sssYDY = new ArrayList();
            int ydyi = qti;
            foreach (adims_MODEL.Yongyao mzyt in ydyList)
            {
                if (sssYDY.Contains(mzyt.Name))
                    ydyi = ydyi - 1;
                if (!sssYDY.Contains(mzyt.Name))
                    e.Graphics.DrawString(mzyt.Name + " " + mzyt.Dw, mzyt.Name.Length < 7 ? ptzt7 : ptzt7,
                        Brushes.Black, new PointF(20 + xLeft, YY + ydyi * 12 + yTop + 3));

                if (mzyt.Cxyy == false)
                {
                    TimeSpan t = new TimeSpan();
                    t = mzyt.KsTime - PageStartTime;
                    int x1 = (TextValueLimit.TimeToXprint(mzyt.KsTime, ZoomRegionListPrint) + xStart);
                    int y1 = YY + ydyi * 12 + 4;
                    if (x1 >= xStart && x1 < xEnd)
                    {
                        e.Graphics.DrawString(mzyt.Yl.ToString(), ptzt7, Brushes.Blue, new Point(x1 + 3, yTop + y1 - 6));
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1 + yTop), new Point(x1 - 3, y1 + 6 + yTop), new Point(x1 + 3, y1 + 6 + yTop) });
                    }
                }
                if (mzyt.Cxyy == true)
                {

                    int x1 = (TextValueLimit.TimeToXprint(mzyt.KsTime, ZoomRegionListPrint) + xStart);
                    int y1 = YY + ydyi * 12 + 4;
                    DateTime dtend = DateTime.Now;
                    if (mzyt.Bz == 1)
                    {
                        dtend = MaxPointTime;
                    }
                    else
                    {
                        dtend = mzyt.JsTime;
                    }
                    if (mzyt.JsTime > MaxPointTime)
                    {
                        dtend = MaxPointTime;
                    }
                    int x2 = (TextValueLimit.TimeToXprint(dtend, ZoomRegionListPrint) + xStart);
                    if (x1 >= xStart && x1 < xEnd)
                    {
                        e.Graphics.DrawString(mzyt.Yl.ToString(), ptzt7, Brushes.Blue, new Point(x1 + 3, yTop + y1 - 6));
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1 + yTop), new Point(x1 - 3, y1 + 6 + yTop), new Point(x1 + 3, y1 + 6 + yTop) });
                    }
                    if (x2 > xStart && x2 <= xEnd)
                    {
                        if (mzyt.Bz == 1)
                        {
                            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 3), new Point(x2 - 5, y1));
                            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 3), new Point(x2 - 5, y1 + 6));
                        }
                        if (mzyt.Bz == 2)
                        {
                            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1 + yTop), new Point(x2 - 3, y1 + 6 + yTop), new Point(x2 + 3, y1 + 6 + yTop) });
                        }
                    }
                    if (x1 >= xStart && x1 < xEnd && x2 > xStart && x2 <= xEnd)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x1, y1 + yTop + 3), new Point(x2, y1 + yTop + 3));
                    }
                    if (x1 >= xStart && x1 < xEnd && x2 > xEnd)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x1, y1 + yTop + 3), new Point(xEnd, y1 + yTop + 3));
                    }
                    if (x1 < xStart && x2 > xStart && x2 <= xEnd)
                    {
                        e.Graphics.DrawLine(pred2, new Point(xStart, y1 + yTop + 3), new Point(x2, y1 + yTop + 3));
                    }
                    if (x1 < xStart && x2 > xEnd)
                    {
                        e.Graphics.DrawLine(pred2, new Point(xStart, y1 + yTop + 3), new Point(xEnd, y1 + yTop + 3));
                    }
                }
                ydyi++;
                sssYDY.Add(mzyt.Name);
            }
            #endregion

            #region 打印局麻药
            //int jti = 20;  //打印局麻药
            //ArrayList sssJMY = new ArrayList();
            //foreach (adims_MODEL.Yongyao jt in jmyList)
            //{
            //    if (sssJMY.Contains(jt.Name))
            //        jti = jti - 1;
            //    if (!sssJMY.Contains(jt.Name))
            //        e.Graphics.DrawString(jt.Name + " " + jt.Dw, jt.Name.Length < 7 ? ptzt7 : ptzt5, Brushes.Black, new PointF(37 + xLeft, YY + jti * 12 + yTop + 2));

            //    TimeSpan t = new TimeSpan();
            //    t = jt.KsTime - pagetime;
            //    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 9 / _MonitorInterval + 120);
            //    int y1 = YY + jti * 12 + 4;
            //    if (x1 > 120 + xLeft && x1 < 780 + xLeft)
            //    {
            //        e.Graphics.DrawString(jt.Yl.ToString(), ptzt7, Brushes.Blue, new Point(x1 + 3 + xLeft, yTop + y1 - 6));
            //        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + xLeft, y1 + yTop), new Point(x1 - 3 + xLeft, y1 + 6 + yTop), new Point(x1 + 3 + xLeft, y1 + 6 + yTop) });
            //    }
            //    jti++;
            //    sssJMY.Add(jt.Name);
            //}
            #endregion

            #region 打印输液
            int syi = 12;
            ArrayList sssSY = new ArrayList();
            foreach (adims_MODEL.Yongyao sx in shuyeList)
            {
                if (sssSY.Contains(sx.Name))
                    syi = syi - 1;
                if (!sssSY.Contains(sx.Name))
                    e.Graphics.DrawString(sx.Name + " " + sx.Dw, sx.Name.Length < 7 ? ptzt7 : ptzt7,
                        Brushes.Black, new PointF(20 + xLeft, YY + syi * 12 + yTop + 3));

                if (sx.Cxyy == false)
                {
                    TimeSpan t = new TimeSpan();
                    t = sx.KsTime - PageStartTime;
                    int x1 = (TextValueLimit.TimeToXprint(sx.KsTime, ZoomRegionListPrint) + xStart);
                    int y1 = YY + syi * 12 + 4;
                    if (x1 >= xStart && x1 < xEnd)
                    {
                        e.Graphics.DrawString(sx.Yl.ToString(), ptzt7, Brushes.Blue, new Point(x1 + 3, yTop + y1 - 6));
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1 + yTop), new Point(x1 - 3, y1 + 6 + yTop), new Point(x1 + 3, y1 + 6 + yTop) });
                    }
                }
                if (sx.Cxyy == true)
                {

                    int x1 = (TextValueLimit.TimeToXprint(sx.KsTime, ZoomRegionListPrint) + xStart);
                    int y1 = YY + syi * 12 + 4;

                    DateTime dtend = DateTime.Now;
                    if (sx.Bz == 1)
                    {
                        dtend = MaxPointTime;
                    }
                    else
                    {
                        dtend = sx.JsTime;
                    }

                    if (sx.JsTime > MaxPointTime)
                    {
                        dtend = MaxPointTime;
                    }
                    int x2 = (TextValueLimit.TimeToXprint(dtend, ZoomRegionListPrint) + xStart);
                    if (x1 >= xStart && x1 < xEnd)
                    {
                        e.Graphics.DrawString(sx.Yl.ToString(), ptzt7, Brushes.Blue, new Point(x1 + 3, yTop + y1 - 6));
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1 + yTop), new Point(x1 - 3, y1 + 6 + yTop), new Point(x1 + 3, y1 + 6 + yTop) });
                    }
                    if (x2 > xStart && x2 <= xEnd)
                    {
                        if (sx.Bz == 1)
                        {
                            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 3), new Point(x2 - 5, y1));
                            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 3), new Point(x2 - 5, y1 + 6));
                        }
                        if (sx.Bz == 2)
                        {
                            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1 + yTop), new Point(x2 - 3, y1 + 6 + yTop), new Point(x2 + 3, y1 + 6 + yTop) });
                        }
                    }
                    if (x1 >= xStart && x1 < xEnd && x2 > xStart && x2 <= xEnd)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x1, y1 + yTop + 3), new Point(x2, y1 + yTop + 3));
                    }
                    if (x1 >= xStart && x1 < xEnd && x2 > xEnd)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x1, y1 + yTop + 3), new Point(xEnd, y1 + yTop + 3));
                    }
                    if (x1 < xStart && x2 > xStart && x2 <= xEnd)
                    {
                        e.Graphics.DrawLine(pred2, new Point(xStart, y1 + yTop + 3), new Point(x2, y1 + yTop + 3));
                    }
                    if (x1 < xStart && x2 > xEnd)
                    {
                        e.Graphics.DrawLine(pred2, new Point(xStart, y1 + yTop + 3), new Point(xEnd, y1 + yTop + 3));
                    }
                }
                syi++;
                sssSY.Add(sx.Name);

            }

            #endregion

            #region 打印输血
            //打印输血
            int sxi = syi;
            ArrayList sssSX = new ArrayList();
            foreach (adims_MODEL.Yongyao sx in shuxueList)
            {
                if (sssSX.Contains(sx.Name))
                    sxi = sxi - 1;
                if (!sssSX.Contains(sx.Name))
                    e.Graphics.DrawString(sx.Name + " " + sx.Dw, sx.Name.Length < 7 ? ptzt7 : ptzt7,
                        Brushes.Black, new PointF(20 + xLeft, YY + sxi * 12 + yTop + 3));

                if (sx.Cxyy == false)
                {
                    TimeSpan t = new TimeSpan();
                    t = sx.KsTime - PageStartTime;
                    int x1 = (TextValueLimit.TimeToXprint(sx.KsTime, ZoomRegionListPrint) + xStart);
                    int y1 = YY + sxi * 12 + 4;
                    if (x1 > 100 + xLeft && x1 < xEnd)
                    {
                        e.Graphics.DrawString(sx.Yl.ToString(), ptzt7, Brushes.Blue, new Point(x1 + 3, yTop + y1 - 6));
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1 + yTop), new Point(x1 - 3, y1 + 6 + yTop), new Point(x1 + 3, y1 + 6 + yTop) });
                    }
                }
                if (sx.Cxyy == true)
                {

                    int x1 = (TextValueLimit.TimeToXprint(sx.KsTime, ZoomRegionListPrint) + xStart);
                    int y1 = YY + sxi * 12 + 4;
                    DateTime dtend = DateTime.Now;
                    if (sx.Bz == 1)
                    {
                        dtend = MaxPointTime;
                    }
                    else
                    {
                        dtend = sx.JsTime;
                    }
                    if (sx.JsTime > MaxPointTime)
                    {
                        dtend = MaxPointTime;
                    }
                    int x2 = (TextValueLimit.TimeToXprint(dtend, ZoomRegionListPrint) + xStart);
                    if (x1 >= xStart && x1 < xEnd)
                    {
                        e.Graphics.DrawString(sx.Yl.ToString(), ptzt7, Brushes.Blue, new Point(x1 + 3, yTop + y1 - 6));
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1 + yTop), new Point(x1 - 3, y1 + 6 + yTop), new Point(x1 + 3, y1 + 6 + yTop) });
                    }
                    if (x2 > xStart && x2 <= xEnd)
                    {
                        if (sx.Bz == 1)
                        {
                            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 3), new Point(x2 - 5, y1));
                            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 3), new Point(x2 - 5, y1 + 6));
                        }
                        if (sx.Bz == 2)
                        {
                            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1 + yTop), new Point(x2 - 3, y1 + 6 + yTop), new Point(x2 + 3, y1 + 6 + yTop) });
                        }
                    }
                    if (x1 >= xStart && x1 < xEnd && x2 > xStart && x2 <= xEnd)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x1, y1 + yTop + 3), new Point(x2, y1 + yTop + 3));
                    }
                    if (x1 >= xStart && x1 < xEnd && x2 > xEnd)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x1, y1 + yTop + 3), new Point(xEnd, y1 + yTop + 3));
                    }
                    if (x1 < xStart && x2 > xStart && x2 <= xEnd)
                    {
                        e.Graphics.DrawLine(pred2, new Point(xStart, y1 + yTop + 3), new Point(x2, y1 + yTop + 3));
                    }
                    if (x1 < xStart && x2 > xEnd)
                    {
                        e.Graphics.DrawLine(pred2, new Point(xStart, y1 + yTop + 3), new Point(xEnd, y1 + yTop + 3));
                    }
                }
                sxi++;
                sssSX.Add(sx.Name);

            }
            #endregion


            #region 打印检测区域格子，血压体温等区域↓
            YY = YY + 12 * 21;
            e.Graphics.DrawLine(ptp, new Point(15 + xLeft, YY), new Point(15 + xLeft, YY + 8 * 26));
            e.Graphics.DrawString("术\n\n中\n\n监\n\n测", ptzt8, Brushes.Black, new Point(2 + xLeft, YY + 60));
            for (int i = 1; i < 14; i++)//画横实线   
            {
                e.Graphics.DrawLine(ptp, new Point(xStart, YY + 16 * i), new Point(xEnd, YY + 16 * i + yTop));
            }
            for (int i = 0; i < 13; i++)//画横虚线
                e.Graphics.DrawLine(pxuxian, xStart, YY + 16 * i + 8, xEnd, YY + 16 * i + yTop + 8);



            for (int i = 0; i <= 42; i++)
            {
                if (i % 3 == 0)//画竖实线
                {
                    e.Graphics.DrawLine(ptp, new Point(xStart + 10 * i, YY + yTop), new Point(xStart + 10 * i, YY + 8 * 26 + yTop));
                }
                else//画竖虚线
                {
                    e.Graphics.DrawLine(pxuxian, new Point(xStart + 10 * i, YY + yTop), new Point(xStart + 10 * i, YY + 8 * 26 + yTop));
                }

            }
            for (int i = 1; i < 13; i++)
            {
                e.Graphics.DrawString((41 - i * 1).ToString(), ptzt7, Brushes.Black, new PointF(85 + xLeft, YY + (float)16 * i + yTop - 5));
                e.Graphics.DrawString((260 - i * 20).ToString(), ptzt7, Brushes.Black, new PointF(100 + xLeft, YY + (float)16 * i + yTop - 5));
            }



            e.Graphics.DrawString("∨收缩压", ptzt7, Brushes.Red, new Point(20 + xLeft, YY + 10));
            e.Graphics.DrawString("∧舒张压", ptzt7, Brushes.Red, new Point(20 + xLeft, YY + 25));
            e.Graphics.DrawString("●脉  搏", ptzt7, Brushes.Blue, new Point(20 + xLeft, YY + 40));
            e.Graphics.DrawString("○呼  吸", ptzt7, Brushes.DarkCyan, new Point(20 + xLeft, YY + 55));
            e.Graphics.DrawString("C机械呼吸", ptzt7, Brushes.DarkCyan, new Point(20 + xLeft, YY + 70));
            e.Graphics.DrawString("Ⅹ麻醉开始", ptzt7, Brushes.Black, new Point(20 + xLeft, YY + 85));
            e.Graphics.DrawString("   插  管", ptzt7, Brushes.Black, new Point(20 + xLeft, YY + 100));
            Image cgImage = Properties.Resources.CG;
            e.Graphics.DrawImage(cgImage, new Rectangle(20 + xLeft, YY + 100, 9, 9));
            e.Graphics.DrawString("  拔  管", ptzt7, Brushes.Black, new Point(20 + xLeft, YY + 115));
            Image BgImage = Properties.Resources.BG;
            e.Graphics.DrawImage(BgImage, new Rectangle(20 + xLeft, YY + 115, 9, 9));
            e.Graphics.DrawString("⊙手术开始", ptzt7, Brushes.Black, new Point(20 + xLeft, YY + 130));
            e.Graphics.DrawString("   手术结束", ptzt7, Brushes.Black, new Point(20 + xLeft, YY + 145));
            Image ssjsImage = Properties.Resources.SSJS;
            e.Graphics.DrawImage(ssjsImage, new Rectangle(20 + xLeft, YY + 145, 9, 9));
            e.Graphics.DrawString("   体  温", ptzt7, Brushes.DarkRed, new Point(20 + xLeft, YY + 160));
            e.Graphics.DrawString("   ETCO2", ptzt7, Brushes.DarkOrange, new Point(20 + xLeft, YY + 175));
            #endregion

            #region  //打印收缩压
            float px = 0, py = 0;
            Pen p_red2 = new Pen(Brushes.Red, 2);
            foreach (adims_MODEL.point p in ssyList)
            {
                if (p.D >= PageStartTime && p.D <= PageEndTime)
                {
                    TimeSpan t = new TimeSpan();
                    t = p.D - PageStartTime;
                    float pointx = (float)(TextValueLimit.TimeToXprint(p.D, ZoomRegionListPrint) + xStart);
                    float pointy = 0;
                    if (p.V > 250)
                    {
                        pointy = (float)((20) * 0.2 + YY);
                        e.Graphics.DrawString(p.V.ToString(), ptzt6, Brushes.Blue, new PointF(pointx, pointy));
                    }
                    else
                        pointy = (float)((260 - p.V) * 0.8 + YY);
                    e.Graphics.DrawLines(p_red2, new PointF[3] { new PointF(pointx - 3, pointy - 5), new PointF(pointx, pointy), new PointF(pointx + 3, pointy - 5) });
                    if (px != 0)
                        e.Graphics.DrawLine(Pens.Red, new PointF(px, py), new PointF(pointx, pointy));

                    px = pointx;
                    py = pointy;
                }

            }
            #endregion

            #region  //打印舒张压
            px = 0; py = 0;
            foreach (adims_MODEL.point p in szyList)
            {

                if (p.D >= PageStartTime && p.D <= PageEndTime)
                {
                    float pointx = (float)(TextValueLimit.TimeToXprint(p.D, ZoomRegionListPrint) + xStart);
                    //float pointy = (float)((220 - p.V) * 1 + 460);
                    float pointy = 0;
                    if (p.V > 250)
                    {
                        pointy = (float)((20) * 0.2 + YY);
                        e.Graphics.DrawString(p.V.ToString(), ptzt6, Brushes.Blue, new PointF(pointx, pointy));
                    }
                    else
                        pointy = (float)((260 - p.V) * 0.8 + YY);
                    e.Graphics.DrawLines(p_red2, new PointF[3] { new PointF(pointx - 3, pointy + 5), new PointF(pointx, pointy), new PointF(pointx + 3, pointy + 5) });
                    if (px != 0)
                        e.Graphics.DrawLine(Pens.Red, new PointF(px, py), new PointF(pointx, pointy));

                    px = pointx;
                    py = pointy;
                }
            }

            #endregion

            #region  //打印脉搏
            px = 0; py = 0;
            foreach (adims_MODEL.point p in mboList)
            {
                if (p.D >= PageStartTime && p.D <= PageEndTime)
                {

                    float pointx = (float)(TextValueLimit.TimeToXprint(p.D, ZoomRegionListPrint) + xStart);
                    float pointy = 0;
                    if (p.V > 250)
                    {
                        pointy = (float)((20) * 0.2 + YY);
                        e.Graphics.DrawString(p.V.ToString(), ptzt6, Brushes.Blue, new PointF(pointx, pointy));
                    }
                    else
                        pointy = (float)((260 - p.V) * 0.8 + YY);
                    e.Graphics.FillEllipse(Brushes.Blue, pointx - 2, pointy - 2, 3, 3);
                    if (px != 0)
                        e.Graphics.DrawLine(Pens.Blue, new PointF(px, py), new PointF(pointx, pointy));
                    px = pointx;
                    py = pointy;
                }
            }

            #endregion



            #region  //打印体温
            //px = 0; py = 0;
            //foreach (adims_MODEL.tw_point p in twList)
            //{
            //    if (p.V > 0)
            //    {
            //        if (p.D >= PageStartTime && p.D <= PageEndTime)
            //        {
            //            TimeSpan t = new TimeSpan();
            //            t = p.D - PageStartTime;
            //             float pointx = (float)(TextValueLimit.TimeToX(p.D, ZoomRegionListPrint)  + x);
            //            //float pointy = (float)((220 - p.V) * 1 + 460);
            //            float pointy = 0;
            //            if (p.V > 41)
            //            {
            //                pointy = (float)((20) *0.2+ YY);
            //                e.Graphics.DrawString(p.V.ToString(), ptzt6, Brushes.Blue, pointx + x, pointy + y);

            //            }
            //            else
            //                pointy = (float)((41 - p.V) * 16+ YY);
            //            e.Graphics.DrawPolygon(Pens.Maroon, new PointF[3] { new PointF(pointx - 3 + x, pointy + 5 + y), new PointF(pointx + x, pointy + y), new PointF(pointx + 3 + x, pointy + 5 + y) });
            //            //e.Graphics.FillEllipse(Pens.Maroon, pointx + x - 2, pointy + y - 2, 5, 5);
            //            if (px != 0)
            //                e.Graphics.DrawLine(Pens.Maroon, new PointF(px + x, py + y), new PointF(pointx + x, pointy + y));

            //            px = pointx;
            //            py = pointy;
            //        }
            //    }
            //}
            #endregion

            #region  //打印呼吸
            px = 0; py = 0;
            foreach (adims_MODEL.point p in hxlList)
            {
                if (p.D >= PageStartTime && p.D <= PageEndTime)
                {

                    float pointx = (float)(TextValueLimit.TimeToXprint(p.D, ZoomRegionListPrint) + xStart);
                    float pointy = 0;
                    if (p.V > 250)
                    {
                        pointy = ((20) * 1 + YY) + yTop;
                        e.Graphics.DrawString(p.V.ToString(), ptzt6, Brushes.Blue, pointx, pointy);
                    }
                    else
                        pointy = (float)((260 - p.V) * 0.8 + YY) + yTop;
                    if (jkksTime < p.D && p.D < jkjsTime)
                    {
                        e.Graphics.DrawString("C", ptzt6, Brushes.DarkCyan, pointx - 2, pointy - 3);
                    }
                    else if (fzksTime < p.D && p.D < fzjsTime)
                    {
                        e.Graphics.DrawString("A", ptzt6, Brushes.DarkCyan, pointx - 2, pointy - 3);
                    }
                    else
                    {

                        e.Graphics.DrawEllipse(Pens.DarkCyan, pointx - 2, pointy - 2, 5, 5);
                    }
                    if (px != 0)
                        e.Graphics.DrawLine(Pens.DarkCyan, new PointF(px, py), new PointF(pointx, pointy));
                    px = pointx;
                    py = pointy;
                }
            }
            #endregion

            #region 打印标记
            //↓标记区域
            YY = YY + 8 * 26;
            int y_Bz = YY + 20;
            e.Graphics.DrawLine(ptp, new Point(xLeft, YY), new Point(xEnd, YY));
            e.Graphics.DrawString("标注", ptzt8, Brushes.Black, new Point(5 + xLeft, YY + 10));
            int y_tsyy = YY;
            // 打印其他用药标记
            int tsyyIndex = 1;
            foreach (adims_MODEL.Yongyao ts in tsyyList)
            {
                float xxx = (float)(TextValueLimit.TimeToXprint(ts.KsTime, ZoomRegionListPrint) + xStart - 3);
                if (ts.KsTime >= PageStartTime && ts.KsTime <= PageEndTime)
                {
                    if (tsyyIndex % 2 == 1)
                    {
                        e.Graphics.DrawString(tsyyIndex.ToString(), ptzt7, Brushes.Black, new PointF(xxx, y_tsyy));
                    }
                    else
                    {
                        e.Graphics.DrawString(tsyyIndex.ToString(), ptzt7, Brushes.Black, new PointF(xxx, y_tsyy + 10));
                    }
                }
                tsyyIndex++;
            }

            //打印手术开始标记
            if (ssksTime >= PageStartTime && ssksTime <= PageEndTime)
            {
                int xxx = (TextValueLimit.TimeToXprint(ssksTime, ZoomRegionListPrint) + xStart - 5);
                e.Graphics.DrawString("⊙", ptzt8, Brushes.Black, xxx, y_Bz);
            }
            //打印手术结束标记
            if (ssjsTime >= PageStartTime && ssjsTime <= PageEndTime)
            {
                int xxx = 0;
                if (ssjsTime >= MaxPointTime)
                {
                    xxx = (TextValueLimit.TimeToXprint(MaxPointTime, ZoomRegionListPrint) + xStart - 5);
                }
                else
                {
                    xxx = (TextValueLimit.TimeToXprint(ssjsTime, ZoomRegionListPrint) + xStart - 5);
                }

                Image newImage = Properties.Resources.SSJS;
                e.Graphics.DrawImage(newImage, new Rectangle(xxx, y_Bz, 10, 10));
            }
            //打印麻醉开始标记
            if (mzksTime >= PageStartTime && mzksTime <= PageEndTime)
            {
                int xxx = (TextValueLimit.TimeToXprint(mzksTime, ZoomRegionListPrint) + xStart - 5);
                e.Graphics.DrawString("Χ", ptzt8, Brushes.Black, xxx, y_Bz);
            }
            //打印麻醉结束标记
            //if (mzjsTime >= pagetime && mzjsTime <= pagetime.AddHours(_MonitorInterval))
            //{
            //     int xxx = (TextValueLimit.TimeToXprint(mzjsTime, ZoomRegionListPrint) + xStart  - 5);
            //     e.Graphics.DrawString("Χ", ptzt8, Brushes.Black, xxx, YY + 3);
            //}
            //打印插管标记
            if (_CgTime >= PageStartTime && _CgTime <= PageEndTime)
            {
                int xxx = (TextValueLimit.TimeToXprint(_CgTime, ZoomRegionListPrint) + xStart - 5);
                Image newImage = Properties.Resources.CG;
                e.Graphics.DrawImage(newImage, new Rectangle(xxx, y_Bz, 10, 10));
            }
            //打印拔管标记
            if (_BgTime >= PageStartTime && _BgTime <= PageEndTime)
            {
                int xxx = (TextValueLimit.TimeToXprint(_BgTime, ZoomRegionListPrint) + xStart - 5);
                Image newImage = Properties.Resources.BG;
                if ((_BgTime - ksjcTime).TotalMinutes <= 2)
                {
                    e.Graphics.DrawImage(newImage, new Rectangle(xxx, y_Bz - 10, 10, 10));
                }
                else
                {
                    e.Graphics.DrawImage(newImage, new Rectangle(xxx, y_Bz, 10, 10));
                }

            }
            //打印术中事件标记

            foreach (adims_MODEL.szsj sz in szsjList)
            {
                if (sz.D >= PageStartTime && sz.D <= PageEndTime)
                {
                    float x_szsj = (float)(TextValueLimit.TimeToXprint(sz.D, ZoomRegionListPrint) + xStart - 3);
                    //e.Graphics.FillRectangle(Brushes.Pink, x_szsj - 1, y_Bz, 8, 8);
                    string szsjSort = ArrayHelper.ReplaceNumToLetter(PrintSzsjIndex);
                    e.Graphics.DrawString(szsjSort, ptzt7, Brushes.Black, new PointF(x_szsj, y_Bz + 10));

                    PrintSzsjIndex++;
                }
            }

            #endregion



            #region 打印出血出尿
            //Pen dakred2 = new Pen(Brushes.DarkRed, 2);
            //if (tbChuNiao.Text.Trim() == "")
            //    tbChuNiao.Text = " 0";
            //int yyyy = YY + 32 * 12 + 1;
            //e.Graphics.DrawString("出尿：" + tbChuNiao.Text, ht7, Brushes.DarkRed, new Point(700 + xLeft, yyyy));
            //e.Graphics.DrawLine(dakred2, new Point(725 + xLeft, yyyy + 11), new Point(766 + xLeft, yyyy + 11));
            //e.Graphics.DrawString("ml", ht7, Brushes.Black, new Point(765 + xLeft, yyyy));
            //yyyy = yyyy + 12;
            //if (tbChuxue.Text.Trim() == "")
            //    tbChuxue.Text = " 0";
            //e.Graphics.DrawString("出血：" + tbChuxue.Text, ht7, Brushes.DarkRed, new Point(700 + xLeft, yyyy));
            //e.Graphics.DrawLine(dakred2, new Point(725 + xLeft, yyyy + 11), new Point(766 + xLeft, yyyy + 11));
            //e.Graphics.DrawString("ml", ht7, Brushes.Black, new Point(765 + xLeft, yyyy));

            #endregion

            #region  打印监护项目区域
            YY = YY + 40;
            //e.Graphics.DrawString("\n检\n测\n项\n目", ptzt8, Brushes.Black, new Point(20 + xLeft, YY + 5));
            for (int i = 0; i <= 6; i++)//画横实线 
            {
                if (i == 6)
                {
                    e.Graphics.DrawLine(ptp, new Point(xLeft, YY + 12 * i), new Point(xRight, YY + 12 * i + yTop));
                }
                else
                {
                    e.Graphics.DrawLine(ptp, new Point(xLeft, YY + 12 * i), new Point(xEnd, YY + 12 * i + yTop));

                }

            }

            for (int i = 0; i <= 42; i++)
            {
                if (i % 9 == 0)//画竖实线
                {
                    e.Graphics.DrawLine(ptp, new Point(xStart + 10 * i, YY + yTop), new Point(xStart + 10 * i, YY + 6 * 12 + yTop));
                }
                else//画竖虚线
                {
                    e.Graphics.DrawLine(pxuxian, new Point(xStart + 10 * i, YY + yTop), new Point(xStart + 10 * i, YY + 6 * 12 + yTop));
                }

            }






            #endregion

            e.Graphics.DrawLine(ptp, new Point(xEnd, SZYY + yTop), new Point(xEnd, YY + 6 * 12 + yTop));
            #region 打印监护项目

            #region  //打印ETCO2
            //px = 0; py = 0;
            //foreach (adims_MODEL.point p in etco2List)
            //{
            //    if (p.D >= pagetime && p.D <= pagetime.AddMinutes(60 * _MonitorInterval))
            //    {
            //        TimeSpan t = new TimeSpan();
            //        t = p.D - pagetime;
            //        float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / _MonitorInterval + 120);
            //        float pointy = 0;
            //        if (p.V > 230)
            //        {
            //            pointy = (float)((20) * 1 + YY);
            //            e.Graphics.DrawString(p.V.ToString(), ptzt7, Brushes.Blue, pointx + xLeft, pointy + yTop);
            //        }
            //        else
            //            pointy = (float)((240 - p.V) * 1 + YY);

            //        e.Graphics.DrawPolygon(Pens.DarkOrange, new PointF[3] { new PointF(pointx+ xLeft, pointy+ yTop), 
            //                           new PointF(pointx - 3+ xLeft, pointy - 5+ yTop), new PointF(pointx + 3+ xLeft, pointy - 5+ yTop) });
            //        // e.Graphics.FillPolygon(Brushes.Green, new PointF[3] { new PointF(pointx+ xLeft, pointy+ yTop), 
            //        //         new PointF(pointx + 3+ xLeft, pointy + 6+ yTop), new PointF(pointx - 3+ xLeft, pointy + 6+ yTop) });

            //        if (px != 0)
            //            e.Graphics.DrawLine(Pens.DarkOrange, new PointF(px + xLeft, py + yTop), new PointF(pointx + xLeft, pointy + yTop));

            //        px = pointx;
            //        py = pointy;
            //    }
            //}

            #endregion

            #region 打印监护项目
            int jhi = 0;
            foreach (string jc in jhxmIn)
            {
                e.Graphics.DrawString(jc, ptzt7, Brushes.Black, new PointF(20 + xLeft, YY + jhi * 12 + yTop));
                int count1 = 0;
                foreach (adims_MODEL.jhxm j in jhxmValue)
                {
                    if (jc == j.Sy && j.V != 0)
                    {
                        if (j.D >= PageStartTime && j.D <= PageEndTime)
                        {
                            TimeSpan t = new TimeSpan();
                            t = j.D - PageStartTime;
                            float jhx = (float)(TextValueLimit.TimeToXprint(j.D, ZoomRegionListPrint) + xStart - 5);
                            float jhy = YY + jhi * 12;
                            if (count1 % 2 == 0)
                            {
                                e.Graphics.FillRectangle(Brushes.Pink, jhx - 3, jhy, 12, 9);
                                e.Graphics.DrawString(j.V.ToString(), j.V > 99 ? ptzt5 : ptzt6, Brushes.Black, new PointF(jhx - 4, jhy));
                                //}
                            }
                            count1++;
                        }
                    }
                }
                jhi++;

            }
            ///打印显示体温
            //jhi++;
            int tw_count = 0;
            e.Graphics.DrawString("体温", ptzt7, Brushes.Black, new PointF(20 + xLeft, YY + jhi * 12 + yTop));
            foreach (adims_MODEL.tw_point p in twList)
            {
                if (p.V > 0)
                {
                    if (p.D >= PageStartTime && p.D <= PageEndTime)
                    {
                        TimeSpan t = new TimeSpan();
                        t = p.D - PageStartTime;
                        float jhx = (float)(TextValueLimit.TimeToXprint(p.D, ZoomRegionListPrint) + xStart - 5);
                        float jhy = YY + jhi * 12;
                        if (tw_count % 5 == 0)
                        {
                            if (p.V.ToString().Length > 3)
                            {
                                e.Graphics.FillRectangle(Brushes.Pink, jhx - 3, jhy, 16, 9);
                            }
                            else
                            {
                                e.Graphics.FillRectangle(Brushes.Pink, jhx - 3, jhy, 12, 9);
                            }
                            e.Graphics.DrawString(p.V.ToString(), p.V.ToString().Length > 3 ? ptzt6 : ptzt7, Brushes.Black, new PointF(jhx - 4, jhy));

                        }
                        tw_count++;
                    }
                }

            }


            #endregion
            #endregion
            #region 打印右部分的内容
            #region 打印术中事件内容
            e.Graphics.DrawString("术中事件", ptzt9, Brushes.Black, new Point(xEnd + 10, SZYY + 5));
            e.Graphics.DrawLine(ptp, new Point(xEnd, SZYY + 24), new Point(xEnd, SZYY + 24));
            SZYY = SZYY + 24;
            int szi = 0;
            string szss1 = "";
            int szszWidth = 105;
            SizeF size = new SizeF(szszWidth, 300);
            //打印的起点
            PointF pSZSJ = new PointF(xEnd + 5, SZYY + 5);
            foreach (adims_MODEL.szsj sz in szsjList)
            {
                string szsjSort = ArrayHelper.ReplaceNumToLetter(szi);
                szss1 += szsjSort + "." + sz.Name + " " + sz.D.ToString("HH:mm") + "\n";
                szi++;
            }
            int charNum, lineNum;
            //测量需要画几行
            e.Graphics.MeasureString(szss1, ptzt7, size, StringFormat.GenericTypographic, out charNum, out lineNum);
            //获取字体的高度
            float fontHeight = ptzt7.GetHeight(e.Graphics);
            size = new SizeF(szszWidth, lineNum * fontHeight + 12);
            e.Graphics.DrawString(szss1, ptzt7, Brushes.Black, new RectangleF(pSZSJ, size), StringFormat.GenericTypographic);

            SZYY = SZYY + (int)size.Height;
            e.Graphics.DrawLine(ptp, new Point(xEnd, SZYY), new Point(xEnd, SZYY));
            #endregion
            #region 打印其他用药内容
            e.Graphics.DrawString("其他用药", ptzt9, Brushes.Black, new Point(xEnd + 10, SZYY + 10));
            e.Graphics.DrawLine(ptp, new Point(xEnd, SZYY + 24), new Point(xEnd, SZYY + 24));
            SZYY = SZYY + 30;
            int tsi = 1;
            string tss1 = "";
            foreach (adims_MODEL.Yongyao ts in tsyyList)
            {
                string zsfs = "";//注射方式

                if (ts.Yyfs == "口服")
                {
                    zsfs = "po";
                }
                if (ts.Yyfs == "静脉滴注")
                {
                    zsfs = "ivdrip";
                }
                if (ts.Yyfs == "皮下注射")
                {
                    zsfs = "ih";
                }
                if (ts.Yyfs == "肌肉注射")
                {
                    zsfs = "im";
                }
                if (ts.Yyfs == "静脉注射")
                {
                    zsfs = "iv";
                }
                if (ts.Yyfs == "皮下注射")
                {
                    zsfs = "id";
                }
                tss1 = tsi.ToString() + "." + ts.Name + "" + ts.Yl + "" + ts.Dw + " " + zsfs + "\n";
                e.Graphics.DrawString(tss1, ptzt7, Brushes.Black, new PointF(xEnd + 3, SZYY));
                SZYY = SZYY + 15;
                if (SZYY > 775)
                {
                    break;
                }
                tsi++;
            }
            //e.Graphics.DrawLine(ptp, new Point(xLeft + 480, SZYY + 204), new Point(xLeft + 660, SZYY + 204));
            #endregion

            #endregion

            #region 打印尾部区域↓
            int y_weibu = YY + 6 * 12; //尾部开始位置Y坐标
            //Y_unLine = y_weibu + 6;
            //e.Graphics.DrawLine(ptp, new Point(20 + xLeft, y_weibu), new Point(660 + xLeft, y_weibu));
            y_weibu = y_weibu + 5; Y_unLine = y_weibu + 13;
            if (txtSZZD.Text.Trim() != "")
            {
                if (txtSZZD.Text.Trim().Length <= 130)
                {

                    string str1_zd = "";
                    int StrLength_zd = txtSZZD.Text.Trim().Length;
                    int row_zd = StrLength_zd / 65;
                    e.Graphics.DrawString("术中诊断 ", ptzt8, Brushes.Black, xLeft + 5, y_weibu);
                    for (int i = 0; i <= row_zd;)//49个字符就换行
                    {

                        if (i < row_zd)
                            str1_zd = txtSZZD.Text.ToString().Substring(i * 65, 65); //从i*65个开始，截取65个字符串
                        else
                            str1_zd = txtSZZD.Text.ToString().Substring(i * 65);
                        e.Graphics.DrawString(str1_zd, ptzt7, Brushes.Black, xLeft + 80, y_weibu);
                        e.Graphics.DrawLine(ptp, new Point(50 + xLeft, Y_unLine), new Point(xRight - 5, Y_unLine));
                        i++;
                        if (i > row_zd)
                        {

                        }
                        else
                        {
                            if (i == 2)
                            {
                                break;
                            }
                            y_weibu = y_weibu + 20; Y_unLine = y_weibu + 13;
                        }

                    }
                }
                else
                {
                    string str1_zd = "";
                    int StrLength_zd = txtSZZD.Text.Trim().Length;
                    int row_zd = StrLength_zd / 90;
                    e.Graphics.DrawString("术中诊断 ", ptzt8, Brushes.Black, xLeft + 5, y_weibu);
                    for (int i = 0; i <= row_zd;)//85个字符就换行
                    {

                        if (i < row_zd)
                            str1_zd = txtSZZD.Text.ToString().Substring(i * 90, 90); //从i*49个开始，截取49个字符串
                        else
                            str1_zd = txtSZZD.Text.ToString().Substring(i * 90);
                        e.Graphics.DrawString(str1_zd, ptzt8, Brushes.Black, xLeft + 60, y_weibu);
                        e.Graphics.DrawLine(ptp, new Point(50 + xLeft, Y_unLine), new Point(xRight - 5, Y_unLine));
                        i++;
                        if (i > row_zd)
                        {

                        }
                        else
                        {
                            if (i == 2)
                            {
                                break;
                            }
                            YY = YY + 20; Y_unLine = YY + 13;
                        }

                    }
                }
            }
            else
            {
                e.Graphics.DrawString("术中诊断 ", ptzt8, Brushes.Black, 5 + xLeft, y_weibu);
                e.Graphics.DrawString(txtSZZD.Text == "" ? "/" : txtSZZD.Text, ptzt8, Brushes.Black, 60 + xLeft, y_weibu);
                e.Graphics.DrawLine(ptp, new Point(50 + xLeft, Y_unLine), new Point(xRight - 5, Y_unLine));
            }
            y_weibu = y_weibu + 20; Y_unLine = y_weibu + 13;
            e.Graphics.DrawString("实施手术 ", ptzt8, Brushes.Black, 5 + xLeft, y_weibu);
            e.Graphics.DrawString(txtSSSS.Text == "" ? "/" : txtSSSS.Text, ptzt8, Brushes.Black, 60 + xLeft, y_weibu);
            e.Graphics.DrawLine(ptp, new Point(50 + xLeft, Y_unLine), new Point(xRight - 5, Y_unLine));

            y_weibu = y_weibu + 20; Y_unLine = y_weibu + 13;
            e.Graphics.DrawString("药物 ", ptzt8, Brushes.Black, 5 + xLeft, y_weibu);
            e.Graphics.DrawString(txtMZyaowu.Text == "" ? "  /" : txtMZyaowu.Text, ptzt7, Brushes.Black, 35 + xLeft, y_weibu);
            e.Graphics.DrawLine(ptp, new Point(30 + xLeft, Y_unLine), new Point(xRight - 5, Y_unLine));

            y_weibu = y_weibu + 20; Y_unLine = y_weibu + 13;
            e.Graphics.DrawString("麻醉方法 ", ptzt8, Brushes.Black, 5 + xLeft, y_weibu);
            e.Graphics.DrawString(txtMZFF.Text == "" ? "/" : txtMZFF.Text, ptzt8, Brushes.Black, 55 + xLeft, y_weibu);
            e.Graphics.DrawLine(ptp, new Point(50 + xLeft, Y_unLine), new Point(320 + xLeft, Y_unLine));
            e.Graphics.DrawString("手术医师 ", ptzt8, Brushes.Black, 320 + xLeft, y_weibu);
            e.Graphics.DrawString(txtSSYS.Text == "" ? "/" : txtSSYS.Text, txtSSYS.Text.Length > 10 ? ptzt7 : ptzt8, Brushes.Black, 370 + xLeft, y_weibu);
            e.Graphics.DrawLine(ptp, new Point(365 + xLeft, Y_unLine), new Point(480 + xLeft, Y_unLine));

            y_weibu = y_weibu + 20; Y_unLine = y_weibu + 13;
            e.Graphics.DrawString("区域阻滞 ", ptzt8, Brushes.Black, 5 + xLeft, y_weibu);
            e.Graphics.DrawString(txtQYZZ.Text == "" ? "/" : txtQYZZ.Text, ptzt8, Brushes.Black, 60 + xLeft, y_weibu);
            e.Graphics.DrawLine(ptp, new Point(50 + xLeft, Y_unLine), new Point(320 + xLeft, Y_unLine));


            e.Graphics.DrawString("麻醉医师 ", ptzt8, Brushes.Black, 320 + xLeft, y_weibu);
            e.Graphics.DrawString(txtMZYS.Text == "" ? "/" : txtMZYS.Text, txtMZYS.Text.Length > 10 ? ptzt7 : ptzt8, Brushes.Black, 370 + xLeft, y_weibu);
            e.Graphics.DrawLine(ptp, new Point(365 + xLeft, Y_unLine), new Point(480 + xLeft, Y_unLine));

            y_weibu = y_weibu + 20; Y_unLine = y_weibu + 13;

            e.Graphics.DrawString("麻醉平面 ", ptzt8, Brushes.Black, 5 + xLeft, y_weibu);
            e.Graphics.DrawString(cmbMZPM.Text == "" ? "/" : cmbMZPM.Text, ptzt8, Brushes.Black, 60 + xLeft, y_weibu);
            e.Graphics.DrawLine(ptp, new Point(50 + xLeft, Y_unLine), new Point(320 + xLeft, Y_unLine));


            e.Graphics.DrawString("器械护士 ", ptzt8, Brushes.Black, 320 + xLeft, y_weibu);
            e.Graphics.DrawString(txtQXHS.Text == "" ? "/" : txtQXHS.Text, txtQXHS.Text.Length > 10 ? ptzt7 : ptzt8, Brushes.Black, 370 + xLeft, y_weibu);
            e.Graphics.DrawLine(ptp, new Point(365 + xLeft, Y_unLine), new Point(480 + xLeft, Y_unLine));
            y_weibu = y_weibu + 20; Y_unLine = y_weibu + 13;
            e.Graphics.DrawString("术后镇痛 ", ptzt8, Brushes.Black, 5 + xLeft, y_weibu);
            e.Graphics.DrawString(userSHZT.Text == "" ? "/" : userSHZT.Text, ptzt8, Brushes.Black, 60 + xLeft, y_weibu);
            e.Graphics.DrawLine(ptp, new Point(50 + xLeft, Y_unLine), new Point(150 + xLeft, Y_unLine));
            e.Graphics.DrawString("手术体位 ", ptzt8, Brushes.Black, 150 + xLeft, y_weibu);
            e.Graphics.DrawString(cmbTiwei.Text == "" ? "/" : cmbTiwei.Text, ptzt8, Brushes.Black, 200 + xLeft, y_weibu);
            e.Graphics.DrawLine(ptp, new Point(195 + xLeft, Y_unLine), new Point(320 + xLeft, Y_unLine));
            e.Graphics.DrawString("巡回护士 ", ptzt8, Brushes.Black, 320 + xLeft, y_weibu);
            e.Graphics.DrawString(txtXHHS.Text == "" ? "/" : txtXHHS.Text, txtXHHS.Text.Length > 10 ? ptzt7 : ptzt8, Brushes.Black, 370 + xLeft, y_weibu);
            e.Graphics.DrawLine(ptp, new Point(365 + xLeft, Y_unLine), new Point(480 + xLeft, Y_unLine));

            y_weibu = y_weibu + 20; Y_unLine = y_weibu + 13;
            e.Graphics.DrawString("全麻维持 ", ptzt8, Brushes.Black, 5 + xLeft, y_weibu);
            e.Graphics.DrawString(cmbQMWC.Text == "" ? "/" : cmbQMWC.Text, ptzt8, Brushes.Black, 60 + xLeft, y_weibu);
            e.Graphics.DrawLine(ptp, new Point(50 + xLeft, Y_unLine), new Point(xRight - 5, Y_unLine));
            y_weibu = y_weibu + 20; Y_unLine = y_weibu + 13;
            e.Graphics.DrawString("全麻诱导 ", ptzt8, Brushes.Black, 5 + xLeft, y_weibu);
            e.Graphics.DrawString(cmbQMYD.Text == "" ? "/" : cmbQMYD.Text, ptzt8, Brushes.Black, 60 + xLeft, y_weibu);
            e.Graphics.DrawLine(ptp, new Point(50 + xLeft, Y_unLine), new Point(xRight - 5, Y_unLine));
            y_weibu = y_weibu + 20; Y_unLine = y_weibu + 13;
            e.Graphics.DrawString("气道与通气 ", ptzt8, Brushes.Black, 5 + xLeft, y_weibu);
            e.Graphics.DrawString(txtQDTQ.Text == "" ? "/" : txtQDTQ.Text, ptzt8, Brushes.Black, 65 + xLeft, y_weibu);
            e.Graphics.DrawLine(ptp, new Point(60 + xLeft, Y_unLine), new Point(250 + xLeft, Y_unLine));
            e.Graphics.DrawString("插管 ", ptzt8, Brushes.Black, 260 + xLeft, y_weibu);
            e.Graphics.DrawString(UserCG.Text == "" ? "/" : UserCG.Text, ptzt8, Brushes.Black, 290 + xLeft, y_weibu);
            e.Graphics.DrawLine(ptp, new Point(285 + xLeft, Y_unLine), new Point(450 + xLeft, Y_unLine));
            e.Graphics.DrawString("插管号 ", ptzt8, Brushes.Black, 460 + xLeft, y_weibu);
            e.Graphics.DrawString(usercgh.Text == "" ? "/" : usercgh.Text, ptzt8, Brushes.Black, 500 + xLeft, y_weibu);
            e.Graphics.DrawLine(ptp, new Point(495 + xLeft, Y_unLine), new Point(530 + xLeft, Y_unLine));
            e.Graphics.DrawString("手术时间 ", ptzt8, Brushes.Black, 530 + xLeft, y_weibu);
            e.Graphics.DrawString(userSSSJ.Text == "" ? "/" : userSSSJ.Text, ptzt8, Brushes.Black, 580 + xLeft, y_weibu);
            e.Graphics.DrawLine(ptp, new Point(580 + xLeft, Y_unLine), new Point(xRight - 5, Y_unLine));
            y_weibu = y_weibu + 20; Y_unLine = y_weibu + 13;
            e.Graphics.DrawString("麻醉时间 ", ptzt8, Brushes.Black, 5 + xLeft, y_weibu);
            e.Graphics.DrawString(userMZSJ.Text == "" ? "/" : userMZSJ.Text, ptzt8, Brushes.Black, 60 + xLeft, y_weibu);
            e.Graphics.DrawLine(ptp, new Point(50 + xLeft, Y_unLine), new Point(145 + xLeft, Y_unLine));
            e.Graphics.DrawString("麻醉效果 ", ptzt8, Brushes.Black, 150 + xLeft, y_weibu);
            e.Graphics.DrawString(cmbMZXG.Text == "" ? "/" : cmbMZXG.Text, ptzt8, Brushes.Black, 200 + xLeft, y_weibu);
            e.Graphics.DrawLine(ptp, new Point(195 + xLeft, Y_unLine), new Point(230 + xLeft, Y_unLine));
            e.Graphics.DrawString("麻醉穿刺间隙 ", ptzt8, Brushes.Black, 240 + xLeft, y_weibu);
            e.Graphics.DrawString(userMZXG.Text == "" ? "/" : userMZXG.Text, ptzt8, Brushes.Black, 315 + xLeft, y_weibu);
            e.Graphics.DrawLine(ptp, new Point(310 + xLeft, Y_unLine), new Point(370 + xLeft, Y_unLine));
            e.Graphics.DrawString("出室情况 ", ptzt8, Brushes.Black, 380 + xLeft, y_weibu);
            e.Graphics.DrawString(userCSQK.Text == "" ? "/" : userCSQK.Text, ptzt8, Brushes.Black, 430 + xLeft, y_weibu);
            e.Graphics.DrawLine(ptp, new Point(425 + xLeft, Y_unLine), new Point(550 + xLeft, Y_unLine));
            e.Graphics.DrawString("病人去向 ", ptzt8, Brushes.Black, 550 + xLeft, y_weibu);
            e.Graphics.DrawString(cmbBRQX.Text == "" ? "/" : cmbBRQX.Text, ptzt8, Brushes.Black, 600 + xLeft, y_weibu);
            e.Graphics.DrawLine(ptp, new Point(595 + xLeft, Y_unLine), new Point(xRight - 5, Y_unLine));
            int y_weibu1 = YY + 6 * 12 + 5; //尾部开始位置Y坐标
            //Y_unLine = y_weibu1 + 6;
            //e.Graphics.DrawString("出量 ", ptzt8, Brushes.Black, 500 + xLeft, y_weibu1);
            //e.Graphics.DrawString("入量 ", ptzt8, Brushes.Black, 580 + xLeft, y_weibu1);
            y_weibu1 = y_weibu1 + 20; Y_unLine = y_weibu1 + 13;
            y_weibu1 = y_weibu1 + 20; Y_unLine = y_weibu1 + 13;
            y_weibu1 = y_weibu1 + 20; Y_unLine = y_weibu1 + 13;
            int xShixue = xLeft + 490;
            e.Graphics.DrawString("失血量", ptzt8, Brushes.Black, xShixue + 0, y_weibu1);
            e.Graphics.DrawString(txtShixue.Text == "" ? "/" : txtShixue.Text, txtShixue.Text.Length < 3 ? ptzt7 : ptzt6, Brushes.Black, xShixue + 35, y_weibu1);
            e.Graphics.DrawLine(ptp, new Point(xShixue + 30, Y_unLine), new Point(xShixue + 60, Y_unLine));
            e.Graphics.DrawString("ml, 红细胞 ", ptzt8, Brushes.Black, xShixue + 60, y_weibu1);
            e.Graphics.DrawString(txtHongxibao.Text == "" ? "/" : txtHongxibao.Text, txtHongxibao.Text.Length < 4 ? ptzt7 : ptzt6, Brushes.Black, xShixue + 120, y_weibu1);
            e.Graphics.DrawLine(ptp, new Point(xShixue + 120, Y_unLine), new Point(xShixue + 150, Y_unLine));
            e.Graphics.DrawString("u ", ptzt8, Brushes.Black, xShixue + 150, y_weibu1);

            y_weibu1 = y_weibu1 + 20; Y_unLine = y_weibu1 + 13;
            e.Graphics.DrawString("尿量 ", ptzt8, Brushes.Black, xShixue + 0, y_weibu1);
            e.Graphics.DrawString(txtChuniao.Text == "" ? "/" : txtChuniao.Text, txtChuniao.Text.Length < 4 ? ptzt7 : ptzt6, Brushes.Black, xShixue + 35, y_weibu1);
            e.Graphics.DrawLine(ptp, new Point(xShixue + 30, Y_unLine), new Point(xShixue + 60, Y_unLine));
            e.Graphics.DrawString("ml, 全血 ", ptzt8, Brushes.Black, xShixue + 60, y_weibu1);
            e.Graphics.DrawString(txtQuanxue.Text == "" ? "/" : txtQuanxue.Text, txtQuanxue.Text.Length < 4 ? ptzt7 : ptzt6, Brushes.Black, xShixue + 120, y_weibu1);
            e.Graphics.DrawLine(ptp, new Point(xShixue + 120, Y_unLine), new Point(xShixue + 150, Y_unLine));
            e.Graphics.DrawString("ml ", ptzt8, Brushes.Black, xShixue + 150, y_weibu1);

            y_weibu1 = y_weibu1 + 20; Y_unLine = y_weibu1 + 13;
            e.Graphics.DrawString("胸水 ", ptzt8, Brushes.Black, xShixue + 0, y_weibu1);
            e.Graphics.DrawString(txtXiongshui.Text == "" ? "/" : txtXiongshui.Text, txtXiongshui.Text.Length < 4 ? ptzt7 : ptzt6, Brushes.Black, xShixue + 35, y_weibu1);
            e.Graphics.DrawLine(ptp, new Point(xShixue + 30, Y_unLine), new Point(xShixue + 60, Y_unLine));
            e.Graphics.DrawString("ml, 血浆", ptzt8, Brushes.Black, xShixue + 60, y_weibu1);
            e.Graphics.DrawString(txtXuejiang.Text == "" ? "/" : txtXuejiang.Text, txtXuejiang.Text.Length < 4 ? ptzt7 : ptzt6, Brushes.Black, xShixue + 120, y_weibu1);
            e.Graphics.DrawLine(ptp, new Point(xShixue + 120, Y_unLine), new Point(xShixue + 150, Y_unLine));
            e.Graphics.DrawString("ml ", ptzt8, Brushes.Black, xShixue + 150, y_weibu1);

            y_weibu1 = y_weibu1 + 20; Y_unLine = y_weibu1 + 13;
            e.Graphics.DrawString("腹水 ", ptzt8, Brushes.Black, xShixue + 0, y_weibu1);
            e.Graphics.DrawString(txtFushui.Text == "" ? "/" : txtFushui.Text, txtFushui.Text.Length < 4 ? ptzt7 : ptzt6, Brushes.Black, xShixue + 35, y_weibu1);
            e.Graphics.DrawLine(ptp, new Point(xShixue + 30, Y_unLine), new Point(xShixue + 60, Y_unLine));
            e.Graphics.DrawString("ml, 输液 ", ptzt8, Brushes.Black, xShixue + 60, y_weibu1);
            e.Graphics.DrawString(txtShuye.Text == "" ? "/" : txtShuye.Text, txtShuye.Text.Length < 4 ? ptzt7 : ptzt6, Brushes.Black, xShixue + 120, y_weibu1);
            e.Graphics.DrawLine(ptp, new Point(xShixue + 120, Y_unLine), new Point(xShixue + 150, Y_unLine));
            e.Graphics.DrawString("ml ", ptzt8, Brushes.Black, xShixue + 150, y_weibu1);
        
            #endregion
            y_weibu = y_weibu + 20;
            e.Graphics.DrawLine(pblack2, new Point(xLeft, 70 + yTop), new Point(xRight, 70 + yTop));
            e.Graphics.DrawLine(pblack2, new Point(xLeft, 70 + yTop), new Point(xLeft, y_weibu));
            e.Graphics.DrawLine(pblack2, new Point(xRight, 70 + yTop), new Point(xRight, y_weibu));
            e.Graphics.DrawLine(pblack2, new Point(xLeft, y_weibu), new Point(xRight, y_weibu));
            y_weibu = y_weibu + 20;
            if (PageEndTime < PrintEndTime)
            {

                e.HasMorePages = true;
                ptime = PageEndTime;
                FirstOpen = PageEndTime;
                BindZoomRegionListPrint(ptime);
                e.Graphics.DrawString("第 " + iYema.ToString() + " 页", ptzt8, Brushes.Black, new Point(480 + xLeft, y_weibu));
                iYema++;
            }
            else
            {
                e.HasMorePages = false;
                BindZoomRegionListPrint(InRoomTime);
                FirstOpen = ZoomRegionListPrint[0].AStartTime;
                ptime = FirstOpen;


                if (iYema > 1)
                {
                    e.Graphics.DrawString("第 " + iYema.ToString() + " 页", ptzt8, Brushes.Black, new Point(480 + xLeft, y_weibu));
                }
            }
        }



        private void btnLeft_Click(object sender, EventArgs e)/// 向左移动
        {
            int ADD = TextValueLimit.XtoMinute(90, ZoomRegionList);
            otime = otime.AddMinutes(ADD);
            BindZoomRegionList();
            BindJHDian();

            //LableMZKS.Location = new Point(LableMZKS.Location.X - 90, LableMZKS.Location.Y);
            //LableMZJS.Location = new Point(LableMZJS.Location.X - 90, LableMZJS.Location.Y);
            //LableSSKS.Location = new Point(LableSSKS.Location.X - 90, LableSSKS.Location.Y);
            //LableSSJS.Location = new Point(LableSSJS.Location.X - 90, LableSSJS.Location.Y);
            //LableCG.Location = new Point(LableCG.Location.X - 90, LableCG.Location.Y);
            //LableBG.Location = new Point(LableBG.Location.X - 90, LableBG.Location.Y);
            IsViewSS_MZ_CG_TAG(-90);
            //插管，拔管时间暂时未完成
            lab1.Text = "";
            pictureBox1.Refresh();
            pictureBox2.Refresh();
            pictureBox3.Refresh();
            pictureBox4.Refresh();
        }

        private void IsViewSS_MZ_CG_TAG(int x)
        {
            LableMZKS.Location = new Point(LableMZKS.Location.X + x, LableMZKS.Location.Y);
            if (LableMZKS.Location.X > xStartMove)
            {
                LableMZKS.Visible = true;
            }
            else
            {
                LableMZKS.Visible = false;
            }
            LableMZJS.Location = new Point(LableMZJS.Location.X + x, LableMZJS.Location.Y);
            if (LableMZJS.Location.X > xStartMove)
            {
                LableMZJS.Visible = true;
            }
            else
            {
                LableMZJS.Visible = false;
            }
            LableSSKS.Location = new Point(LableSSKS.Location.X + x, LableSSKS.Location.Y);
            if (LableSSKS.Location.X > xStartMove)
            {
                LableSSKS.Visible = true;
            }
            else
            {
                LableSSKS.Visible = false;
            }
            LableSSJS.Location = new Point(LableSSJS.Location.X + x, LableSSJS.Location.Y);
            if (LableSSJS.Location.X > xStartMove)
            {
                LableSSJS.Visible = true;
            }
            else
            {
                LableSSJS.Visible = false;
            }
            LableCG.Location = new Point(LableCG.Location.X + x, LableCG.Location.Y);
            if (LableCG.Location.X > xStartMove)
            {
                LableCG.Visible = true;
            }
            else
            {
                LableCG.Visible = false;
            }
            LableBG.Location = new Point(LableBG.Location.X + x, LableBG.Location.Y);
            if (LableBG.Location.X > xStartMove)
            {
                LableBG.Visible = true;
            }
            else
            {
                LableBG.Visible = false;
            }
        }

        //向右移动
        private void btnRight_Click(object sender, EventArgs e)
        {
            int ADD = TextValueLimit.XtoMinute(90, ZoomRegionList);
            otime = otime.AddMinutes(-ADD);
            BindZoomRegionList();
            BindJHDian();
            //LableMZKS.Location = new Point(LableMZKS.Location.X + 90, LableMZKS.Location.Y);
            //LableMZJS.Location = new Point(LableMZJS.Location.X + 90, LableMZJS.Location.Y);
            //LableSSKS.Location = new Point(LableSSKS.Location.X + 90, LableSSKS.Location.Y);
            //LableSSJS.Location = new Point(LableSSJS.Location.X + 90, LableSSJS.Location.Y);
            //LableCG.Location = new Point(LableCG.Location.X + 90, LableCG.Location.Y);
            //LableBG.Location = new Point(LableBG.Location.X + 90, LableBG.Location.Y);
            IsViewSS_MZ_CG_TAG(90);
            //插管，拔管时间暂时未完成
            lab1.Text = "";

            pictureBox1.Refresh();
            pictureBox2.Refresh();
            pictureBox3.Refresh();
            pictureBox4.Refresh();
            pictureBox1.Show();
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
                    //if (!mzjs)
                    //    result += "麻醉、";
                    if (!IsOperEnd)
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
                    foreach (adims_MODEL.Yongyao qt in mzqtList)//判断气体药是否结束
                    {
                        if (qt.Bz == 1)
                        {
                            YwName = YwName + qt.Name + "\n";
                            JsFlag++;
                        }
                    }
                    foreach (adims_MODEL.Yongyao ydy in ydyList) //判断诱导药是否结束
                    {
                        if (ydy.Bz == 1 && ydy.Cxyy)
                        {
                            YwName = YwName + ydy.Name + "\n";
                            JsFlag++;
                        }
                    }
                    if (JsFlag > 0)
                    {
                        //MessageBox.Show(YwName + "没用结束，不能退出");
                        if (DialogResult.OK == MessageBox.Show(YwName + "没用结束，确定退出麻醉记录单？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                        {
                            this.FormClosing -= new FormClosingEventHandler(this.mzjldEdit_FormClosing);
                            dal.UpdateShoushujianinfo(0, 0, "0", Oroom);

                            int i = SaveMzjld();
                            if (i > 0)
                            {

                                this.Close();//退出
                            }
                            else
                            {
                                MessageBox.Show("麻醉记录单修改失败！");
                                e.Cancel = true;
                                return;
                            }
                        }
                        else
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                    else
                    {
                        this.FormClosing -= new FormClosingEventHandler(this.mzjldEdit_FormClosing);
                        dal.UpdateShoushujianinfo(0, 0, "0", Oroom);
                        int i = SaveMzjld();
                        if (i > 0)
                        {

                            this.Close();//退出
                        }
                        else
                        {
                            MessageBox.Show("麻醉记录单修改失败！");
                            e.Cancel = true;
                            return;
                        }
                    }

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
        int xStartMove, xEndMove, yStartMove;//移动麻醉，手术，插管开始结束点
        private void lbMzks_DoubleClick(object sender, EventArgs e)
        {
            if (!IsMzStart)
            {
                //txtMzsj.Text = Convert.ToString(DateTime.Now);
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                LableMZKS.Visible = true;
                LableMZKS.Text = "X";
                LableMZKS.AutoSize = true;
                LableMZKS.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                LableMZKS.BackColor = Color.Transparent;
                int addM = TextValueLimit.TimeToX(DateTime.Now, ZoomRegionList);
                LableMZKS.Location = new Point((int)(addM + 160), 370);
                this.pictureBox3.Controls.Add(LableMZKS);
                IsMzStart = true;
                IsMzEnd = false;
                dal.UpdateMzkssj(DateTime.Now, _MzjldId);
                mzksTime = DateTime.Now;
                //txtMZKS.Text = DateTime.Now.ToString("HH:mm");
                LableMZKS.MouseDown += new MouseEventHandler(lbMzks1_MouseDown);
                LableMZKS.MouseMove += new MouseEventHandler(lbMzks1_MouseMove);
                LableMZKS.MouseUp += new MouseEventHandler(lbMzks1_MouseUp);
                LableMZKS.MouseLeave += new EventHandler(lbMzks1_MouseLeave);
            }
            else
                MessageBox.Show("麻醉已经开始");
        }

        private void lbMzks1_MouseDown(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.Red;
            lab1.AutoSize = true;
            pictureBox3.Controls.Add(lab1);
            lab1.Location = new Point(LableMZKS.Location.X, LableMZKS.Location.Y - 10);
            xStartMove = LableMZKS.Location.X;
            yStartMove = LableMZKS.Location.Y;
            int addMin = TextValueLimit.XtoMinute(xStartMove - 160, ZoomRegionList);
            DateTime DTIME = otime.AddMinutes(addMin);
            lab1.Text = DTIME.ToString("HH:mm");
        }

        private void lbMzks1_MouseMove(object sender, MouseEventArgs e)
        {
            if (ssmzcgbgFlag == 1)
            {
                //TimeSpan t = new TimeSpan();
                //t = DateTime.Now - otime;
                LableMZKS.Location = new Point(LableMZKS.Location.X + e.X / 2 - 2, LableMZKS.Location.Y);
                xEndMove = LableMZKS.Location.X;
                lab1.Visible = true;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Blue;
                lab1.AutoSize = true;
                pictureBox3.Controls.Add(lab1);
                lab1.Location = new Point(LableMZKS.Location.X, LableMZKS.Location.Y - 10);
                int addMin = TextValueLimit.XtoMinute(xEndMove - 160, ZoomRegionList);
                DateTime DTIME = otime.AddMinutes(addMin);
                lab1.Text = DTIME.ToString("HH:mm");
                labVisibleTimer.Enabled = true;
                mzksTime = otime.AddMinutes(addMin);
                //txtMZKS.Text = DTIME.ToString("HH:mm");


            }
        }

        private void lbMzks1_MouseUp(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
            if (!IsMzEnd)
            {
                int addMin = TextValueLimit.XtoMinute(xEndMove - 160, ZoomRegionList);
                DateTime DTIME = otime.AddMinutes(addMin);
                dal.UpdateMzkssj(DTIME, _MzjldId);
                mzksTime = DTIME;
            }
            else
            {
                if (xEndMove < LableMZJS.Location.X)
                {
                    int addMin = TextValueLimit.XtoMinute(xEndMove - 160, ZoomRegionList);
                    DateTime DTIME = otime.AddMinutes(addMin);
                    dal.UpdateMzkssj(DTIME, _MzjldId);
                    mzksTime = DTIME;
                }
                else
                    LableMZKS.Location = new Point(xStartMove, yStartMove);
            }
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
            if (IsMzStart && !IsMzEnd)
            {
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                mzjsTime = DateTime.Now;
                LableMZJS.Visible = true;
                LableMZJS.Text = "X";
                LableMZJS.AutoSize = true;
                LableMZJS.BackColor = Color.Transparent;
                LableMZJS.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                int addM = TextValueLimit.TimeToX(DateTime.Now, ZoomRegionList);
                LableMZJS.Location = new Point((int)(addM + xStartMove), 370);
                //LableMZJS.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 160), 370);
                this.pictureBox3.Controls.Add(LableMZJS);
                IsMzEnd = true;
                dal.UpdateMzjssj(DateTime.Now, _MzjldId);
                mzjsTime = DateTime.Now;
                //txtMZJS.Text = DateTime.Now.ToString("HH:mm");
                LableMZJS.MouseDown += new MouseEventHandler(lbMzjs1_MouseDown);
                LableMZJS.MouseMove += new MouseEventHandler(lbMzjs1_MouseMove);
                LableMZJS.MouseUp += new MouseEventHandler(lbMzjs1_MouseUp);
                LableMZJS.MouseLeave += new EventHandler(lbMzjs1_MouseLeave);
            }
            else
                MessageBox.Show("麻醉未开始不能结束或者已经结束");
        }

        private void lbMzjs1_MouseDown(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.Blue;
            lab1.AutoSize = true;
            pictureBox3.Controls.Add(lab1);
            lab1.Location = new Point(LableMZJS.Location.X, LableMZJS.Location.Y - 10);
            xStartMove = LableMZJS.Location.X;
            yStartMove = LableMZJS.Location.Y;
            int addMin = TextValueLimit.XtoMinute(xStartMove - xEndMove, ZoomRegionList);
            DateTime DTIME = otime.AddMinutes(addMin);
            lab1.Text = DTIME.ToString("HH:mm");
        }

        private void lbMzjs1_MouseMove(object sender, MouseEventArgs e)
        {
            if (ssmzcgbgFlag == 1)
            {
                LableMZJS.Location = new Point(LableMZJS.Location.X + e.X / 2 - 2, LableMZJS.Location.Y);
                xEndMove = LableMZJS.Location.X;
                lab1.Visible = true;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Blue;
                lab1.AutoSize = true;
                pictureBox3.Controls.Add(lab1);
                lab1.Location = new Point(LableMZJS.Location.X, LableMZJS.Location.Y - 10);
                int addMin = TextValueLimit.XtoMinute(xEndMove - xStartMove, ZoomRegionList);
                DateTime DTIME = otime.AddMinutes(addMin);
                lab1.Text = DTIME.ToString("HH:mm");
                labVisibleTimer.Enabled = true;

            }
        }

        private void lbMzjs1_MouseUp(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
            if (xEndMove > LableMZKS.Location.X)
            {
                int addMin = TextValueLimit.XtoMinute(xEndMove - xStartMove, ZoomRegionList);
                DateTime DTIME = otime.AddMinutes(addMin);
                dal.UpdateMzjssj(otime.AddMinutes((xEndMove - xStartMove) / 15 * _MonitorInterval), _MzjldId);
                mzjsTime = DTIME;
            }
            else
                LableMZJS.Location = new Point(xStartMove, yStartMove);
            //pictureBox3.Refresh();            

        }

        private void lbMzjs1_MouseLeave(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
        }
        #endregion

        #region <<插管，标志移动>>
        private void lbCg_DoubleClick(object sender, EventArgs e)
        {
            if (!IsCg)
            {
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                _CgTime = DateTime.Now;
                LableCG.Visible = true;
                LableCG.Text = " ";
                LableCG.Image = Properties.Resources.CG;
                LableCG.AutoSize = true;
                LableCG.BackColor = Color.Transparent;
                LableCG.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                int addM = TextValueLimit.TimeToX(DateTime.Now, ZoomRegionList);
                LableCG.Location = new Point((int)(addM + 160), 370);
                this.pictureBox3.Controls.Add(LableCG);
                IsCg = true;
                IsBg = false;
                dal.UpdateMzCG(DateTime.Now, _MzjldId);
                _CgTime = DateTime.Now;
                //txtCGKS.Text = DateTime.Now.ToString("HH:mm");
                LableCG.MouseDown += new MouseEventHandler(lb_cguan_MouseDown);
                LableCG.MouseMove += new MouseEventHandler(lb_cguan_MouseMove);
                LableCG.MouseUp += new MouseEventHandler(lb_cguan_MouseUp);
                LableCG.MouseLeave += new EventHandler(lb_cguan_MouseLeave);
            }
            else
                MessageBox.Show("已经插管");
        }

        private void lb_cguan_MouseDown(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.Blue;
            lab1.AutoSize = true;
            pictureBox3.Controls.Add(lab1);
            lab1.Location = new Point(LableCG.Location.X, LableCG.Location.Y - 10);
            xStartMove = LableCG.Location.X;
            yStartMove = LableCG.Location.Y;
            int addMin = TextValueLimit.XtoMinute(xStartMove - 160, ZoomRegionList);
            DateTime DTIME = otime.AddMinutes(addMin);
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
                LableCG.Location = new Point(LableCG.Location.X + e.X / 2 - 2, LableCG.Location.Y);
                xEndMove = LableCG.Location.X;
                lab1.Visible = true;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Blue;
                lab1.AutoSize = true;
                pictureBox3.Controls.Add(lab1);
                lab1.Location = new Point(LableCG.Location.X, LableCG.Location.Y - 10);
                int addMin = TextValueLimit.XtoMinute(xEndMove - 160, ZoomRegionList);
                DateTime DTIME = otime.AddMinutes(addMin);
                lab1.Text = DTIME.ToString("HH:mm");
                labVisibleTimer.Enabled = true;
            }
        }

        private void lb_cguan_MouseUp(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
            if (!IsBg)
            {
                int addMin = TextValueLimit.XtoMinute(xEndMove - 160, ZoomRegionList);
                DateTime DTIME = otime.AddMinutes(addMin);
                dal.UpdateMzCG(DTIME, _MzjldId);
                _CgTime = DTIME;
            }
            else
            {
                if (xEndMove < LableBG.Location.X)
                {
                    int addMin = TextValueLimit.XtoMinute(xEndMove - 160, ZoomRegionList);
                    DateTime DTIME = otime.AddMinutes(addMin);
                    dal.UpdateMzCG(DTIME, _MzjldId);
                    _CgTime = DTIME;
                }
                else
                    LableCG.Location = new Point(xStartMove, yStartMove);
            }
            //pictureBox3.Refresh();
        }
        #endregion

        #region <<拔管标志移动>>
        private void lbBg_DoubleClick(object sender, EventArgs e)
        {
            if (IsCg && !IsBg)
            {
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                _BgTime = DateTime.Now;
                LableBG.Visible = true;
                LableBG.Text = " ";
                LableBG.Image = Properties.Resources.BG;
                LableBG.AutoSize = true;
                LableBG.BackColor = Color.Transparent;
                LableBG.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                int addM = TextValueLimit.TimeToX(DateTime.Now, ZoomRegionList);
                LableBG.Location = new Point((int)(addM + 160), 370);
                this.pictureBox3.Controls.Add(LableBG);
                IsBg = true;
                dal.UpdateMzBG(DateTime.Now, _MzjldId);
                _BgTime = DateTime.Now;
                //txtCGJS.Text = DateTime.Now.ToString("HH:mm");
                LableBG.MouseDown += new MouseEventHandler(lb_bguan_MouseDown);
                LableBG.MouseMove += new MouseEventHandler(lb_bguan_MouseMove);
                LableBG.MouseUp += new MouseEventHandler(lb_bguan_MouseUp);
                LableBG.MouseLeave += new EventHandler(lb_bguan_MouseLeave);
            }
            else
                MessageBox.Show("没有插管或者拔管已经结束");
        }

        private void lb_bguan_MouseDown(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.Blue;
            lab1.AutoSize = true;
            pictureBox3.Controls.Add(lab1);
            lab1.Location = new Point(LableBG.Location.X, LableBG.Location.Y - 10);
            xStartMove = LableBG.Location.X;
            yStartMove = LableBG.Location.Y;
            int addMin = TextValueLimit.XtoMinute(xStartMove - 160, ZoomRegionList);
            DateTime DTIME = otime.AddMinutes(addMin);
            lab1.Text = DTIME.ToString("HH:mm");
        }

        private void lb_bguan_MouseMove(object sender, MouseEventArgs e)
        {
            if (ssmzcgbgFlag == 1)
            {
                LableBG.Location = new Point(LableBG.Location.X + e.X / 2 - 2, LableBG.Location.Y);
                xEndMove = LableBG.Location.X;
                lab1.Visible = true;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Blue;
                lab1.AutoSize = true;
                pictureBox3.Controls.Add(lab1);
                lab1.Location = new Point(LableBG.Location.X, LableBG.Location.Y - 10);
                int addMin = TextValueLimit.XtoMinute(xEndMove - 160, ZoomRegionList);
                DateTime DTIME = otime.AddMinutes(addMin);
                lab1.Text = DTIME.ToString("HH:mm");
                labVisibleTimer.Enabled = true;
                //int ssshijian = Convert.ToInt32((lb_bguan.Location.X - lb_bguan.Location.X) / 3);//计算拔管时间
                //txtCGJS.Text = DTIME.ToString("HH:mm");
            }
        }

        private void lb_bguan_MouseUp(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
            if (xEndMove > LableCG.Location.X)
            {
                int addMin = TextValueLimit.XtoMinute(xEndMove - 160, ZoomRegionList);
                DateTime DTIME = otime.AddMinutes(addMin);
                dal.UpdateMzBG(DTIME, _MzjldId);
                _BgTime = DTIME;
            }
            else
                LableBG.Location = new Point(xStartMove, yStartMove);
            //pictureBox3.Refresh();

        }

        private void lb_bguan_MouseLeave(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
        }
        #endregion

        #region <<手术开始，手术开始标志移动>>

        /// <summary>
        /// 双击手术开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbSsks_DoubleClick(object sender, EventArgs e)
        {
            if (!IsOperStart)
            {
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                LableSSKS.Visible = true;
                LableSSKS.Text = "⊙";
                LableSSKS.AutoSize = true;
                LableSSKS.BackColor = Color.Transparent;
                LableSSKS.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                int addM = TextValueLimit.TimeToX(DateTime.Now, ZoomRegionList);
                LableSSKS.Location = new Point((int)(addM + 160), 370);
                this.pictureBox3.Controls.Add(LableSSKS);
                IsOperStart = true;
                IsOperEnd = false;
                dal.UpdateSskssj(DateTime.Now, _MzjldId);
                ssksTime = DateTime.Now;
                dal.UpdateShoushujianinfo(2, Oroom);
                //txtSSKS.Text = DateTime.Now.ToString("HH:mm");
                LableSSKS.MouseDown += new MouseEventHandler(ssks1_MouseDown);
                LableSSKS.MouseMove += new MouseEventHandler(ssks1_MouseMove);
                LableSSKS.MouseUp += new MouseEventHandler(ssks1_MouseUp);
                LableSSKS.MouseLeave += new EventHandler(ssks1_MouseLeave);
            }
            else
                MessageBox.Show("手术已经开始");
        }

        private void ssks1_MouseDown(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.Blue;
            lab1.AutoSize = true;
            pictureBox3.Controls.Add(lab1);
            lab1.Location = new Point(LableSSKS.Location.X, LableSSKS.Location.Y - 10);
            xStartMove = LableSSKS.Location.X;
            yStartMove = LableSSKS.Location.Y;
            int addMin = TextValueLimit.XtoMinute(xStartMove - 160, ZoomRegionList);
            DateTime DTIME = otime.AddMinutes(addMin);
            lab1.Text = DTIME.ToString("HH:mm");
        }

        private void ssks1_MouseUp(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
            if (!IsOperEnd)
            {
                int addMin = TextValueLimit.XtoMinute(xEndMove - 160, ZoomRegionList);
                DateTime DTIME = otime.AddMinutes(addMin);
                dal.UpdateSskssj(DTIME, _MzjldId);
                ssksTime = DTIME;
            }
            else
            {
                if (xEndMove < LableSSJS.Location.X)
                {
                    int addMin = TextValueLimit.XtoMinute(xEndMove - 160, ZoomRegionList);
                    DateTime DTIME = otime.AddMinutes(addMin);
                    dal.UpdateSskssj(DTIME, _MzjldId);
                    ssksTime = DTIME;
                }
                else
                    LableSSKS.Location = new Point(xStartMove, yStartMove);
            }

        }

        private void ssks1_MouseLeave(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
        }

        private void ssks1_MouseMove(object sender, MouseEventArgs e)
        {
            if (ssmzcgbgFlag == 1)
            {
                LableSSKS.Location = new Point(LableSSKS.Location.X + e.X / 2 - 2, LableSSKS.Location.Y);

                xEndMove = LableSSKS.Location.X;
                lab1.Visible = true;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Blue;
                lab1.AutoSize = true;
                pictureBox3.Controls.Add(lab1);
                lab1.Location = new Point(LableSSKS.Location.X, LableSSKS.Location.Y - 10);
                int addMin = TextValueLimit.XtoMinute(xEndMove - 160, ZoomRegionList);
                DateTime DTIME = otime.AddMinutes(addMin);
                lab1.Text = DTIME.ToString("HH:mm");
                labVisibleTimer.Enabled = true;
                ssksTime = otime.AddMinutes(addMin);
                //txtSSKS.Text = DTIME.ToString("HH:mm");
            }
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
            if (IsOperStart && !IsOperEnd)
            {
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                LableSSJS.Text = " ";
                LableSSJS.Image = Properties.Resources.SSJS;
                LableSSJS.Visible = true;
                LableSSJS.AutoSize = true;
                LableSSJS.BackColor = Color.Transparent;
                LableSSJS.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                int addM = TextValueLimit.TimeToX(DateTime.Now, ZoomRegionList);
                LableSSJS.Location = new Point((int)(addM + 160), 370);
                this.pictureBox3.Controls.Add(LableSSJS);
                IsOperEnd = true;
                dal.UpdateSsjssj(DateTime.Now, _MzjldId);
                dal.UpdateSsjsFlag(_MzjldId);
                ssjsTime = DateTime.Now;
                //txtSSJS.Text = DateTime.Now.ToString("HH:mm");
                LableSSJS.MouseDown += new MouseEventHandler(ssjs1_MouseDown);
                LableSSJS.MouseMove += new MouseEventHandler(ssjs1_MouseMove);
                LableSSJS.MouseUp += new MouseEventHandler(ssjs1_MouseUp);
                LableSSJS.MouseLeave += new EventHandler(ssjs1_MouseLeave);
                pictureBox2.Refresh();
            }
            else
                MessageBox.Show("手术未开始或手术已经结束！");
        }

        private void ssjs1_MouseDown(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.Blue;
            lab1.AutoSize = true;
            pictureBox3.Controls.Add(lab1);
            lab1.Location = new Point(LableSSJS.Location.X, LableSSJS.Location.Y - 10);
            xStartMove = LableSSJS.Location.X;
            yStartMove = LableSSJS.Location.Y;
            int addMin = TextValueLimit.XtoMinute(xStartMove - 160, ZoomRegionList);
            DateTime DTIME = otime.AddMinutes(addMin);
            lab1.Text = DTIME.ToString("HH:mm");

        }

        private void ssjs1_MouseMove(object sender, MouseEventArgs e)
        {
            if (ssmzcgbgFlag == 1)
            {
                LableSSJS.Location = new Point(LableSSJS.Location.X + e.X / 2 - 2, LableSSJS.Location.Y);
                xEndMove = LableSSJS.Location.X;
                lab1.Visible = true;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Blue;
                lab1.AutoSize = true;
                pictureBox3.Controls.Add(lab1);
                lab1.Location = new Point(LableSSJS.Location.X, LableSSJS.Location.Y - 10);
                int addMin = TextValueLimit.XtoMinute(xEndMove - 160, ZoomRegionList);
                DateTime DTIME = otime.AddMinutes(addMin);
                lab1.Text = DTIME.ToString("HH:mm");
                labVisibleTimer.Enabled = true;
                // ssjsTime = otime.AddMinutes(addMin);
                //int ssshijian=Convert.ToInt32((ssjs1.Location.X - ssks1.Location.X) / 3.27);
                //txtSssj.Text = (ssshijian / 60).ToString() + "小时"+(ssshijian % 60).ToString()+"分";
                //txtSSJS.Text = DTIME.ToString("HH:mm");
            }
        }

        private void ssjs1_MouseUp(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
            if (xEndMove > LableSSKS.Location.X)
            {
                int addMin = TextValueLimit.XtoMinute(xEndMove - 160, ZoomRegionList);
                DateTime DTIME = otime.AddMinutes(addMin);
                dal.UpdateSsjssj(DTIME, _MzjldId);
                ssjsTime = DTIME;

            }
            else
                LableSSJS.Location = new Point(xStartMove, yStartMove);
            pictureBox2.Refresh();
            TimeSpan dtsstime = new TimeSpan();//手术时间
            TimeSpan dtmztime = new TimeSpan();//麻醉时间
            dtsstime = ssjsTime - ssksTime;
            dtmztime = ssjsTime - mzksTime;
            if (dtsstime.Hours > 0)
            {
                userSSSJ.Text = dtsstime.Hours + "小时" + dtsstime.Minutes + "分钟";
            }
            else
            {
                userSSSJ.Text = dtsstime.Minutes + "分钟";
            }
            if (dtmztime.Hours > 0)
            {
                userMZSJ.Text = dtmztime.Hours + "小时" + dtmztime.Minutes + "分钟";
            }
            else
            {
                userMZSJ.Text = dtmztime.Minutes + "分钟";
            }

        }

        private void ssjs1_MouseLeave(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
        }

        #endregion

        private void button8_Click(object sender, EventArgs e)/// 影像病历
        {
            PACS_DB_Help db = new PACS_DB_Help();
            string IPaddress = "192.168.1.65";
            bool flag = TextValueLimit.PingHost(IPaddress, 1000);
            if (flag == true)
            {
                string inpatientID = patID;
                string yxh = "";//影像号
                DataTable dt = db.GetPACS(patID);
                if (dt.Rows.Count > 0)
                {
                    yxh = dt.Rows[0]["patientid"].ToString();
                }
                else
                {
                    MessageBox.Show("没有找到住院号对应的影像号！");
                    return;
                }
                string url = "http://192.168.1.65/DicomWeb/Dicomweb.dll/login?PTNID=" + yxh + "&User=1&Password=1";
                System.Diagnostics.Process.Start(url);
            }
            else MessageBox.Show("影响病历 数据库未连接，请检查网络");

            //string IPaddress = "132.147.160.41";
            //bool flag = TextValueLimit.PingHost(IPaddress, 1000);
            //if (flag == true)
            //{
            //    string inpatientID = patID;
            //    string url = "http://132.147.160.41/DicomWeb/Dicomweb.dll/login?PTNID=" + patID + "&User=sm1&Password=1";
            //    System.Diagnostics.Process.Start(url);
            //}
            //else MessageBox.Show("影响病历 数据库未连接，请检查网络");

            //yxbl yxblform = new yxbl();
            //yxblform.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)/// 检验病历
        {
            string IPaddress = "192.168.1.8";
            bool flag = TextValueLimit.PingHost(IPaddress, 1000);
            if (flag == true)
            {
                LisResult f1 = new LisResult(patID, odate.ToString("yyyy-MM-dd"));
                f1.Show();
            }
            else MessageBox.Show("检验病历 数据库未连接，请检查网络");
        }

        private void button10_Click(object sender, EventArgs e)// 实时数据
        {
            sssj_temp myform = new sssj_temp();
            myform.Show();
        }

        public int SaveMzjld()// 更新麻醉记录单
        {
            try
            {
                //赋值回传给手术申请单
                string ap1 = "";
                string ap2 = "";
                string ap3 = "";
                string on1 = "";
                string on2 = "";
                string sn1 = "";
                string sn2 = "";
                List<string> sssq = new List<string>();
                sssq.Add(patID);
                sssq.Add(dtOtime.Value.Date.ToString("yyyy-MM-dd"));
                sssq.Add(txtMZFF.Text);
                string[] mmys;//分割麻醉医师
                if (txtMZYS.Text.Contains("、"))
                {
                    mmys = txtMZYS.Text.Split('、');
                }
                else if (txtMZYS.Text.Contains(","))
                {
                    mmys = txtMZYS.Text.Split(',');
                }
                else
                {
                    mmys = txtMZYS.Text.Split(' ');
                }
                for (int i = 0; i < mmys.Length; i++)
                {
                    if (i == 0)
                    {
                        ap1 = mmys[i];
                    }
                    if (i == 1)
                    {
                        ap2 = mmys[i];
                    }
                    if (i == 2)
                    {
                        ap3 = mmys[i];

                    }

                }
                sssq.Add(ap1);
                sssq.Add(ap2);
                sssq.Add(ap3);
                string[] qxhs;//分割器械护士
                if (txtQXHS.Text.Contains("、"))
                {
                    qxhs = txtQXHS.Text.Split('、');
                }
                else if (txtQXHS.Text.Contains(","))
                {
                    qxhs = txtQXHS.Text.Split(',');
                }
                else
                {
                    qxhs = txtQXHS.Text.Split(' ');
                }
                for (int i = 0; i < qxhs.Length; i++)
                {
                    if (i == 0)
                    {
                        on1 = qxhs[i];
                    }
                    if (i == 1)
                    {
                        on2 = qxhs[i];
                    }
                }
                sssq.Add(on1);
                sssq.Add(on2);
                string[] xhhs;//分割巡回护士
                if (txtXHHS.Text.Contains("、"))
                {
                    xhhs = txtXHHS.Text.Split('、');
                }
                else if (txtXHHS.Text.Contains(","))
                {
                    xhhs = txtXHHS.Text.Split(',');
                }
                else
                {
                    xhhs = txtXHHS.Text.Split(' ');
                }
                for (int i = 0; i < xhhs.Length; i++)
                {
                    if (i == 0)
                    {
                        sn1 = xhhs[i];
                    }
                    if (i == 1)
                    {
                        sn2 = xhhs[i];
                    }

                }
                sssq.Add(sn1);
                sssq.Add(sn2);
                DataTable dt = dal.GetPaiban(patID, dtOtime.Value.Date.ToString("yyyy-MM-dd"));
                if (dt.Rows.Count > 0)
                {
                    int results = dal.UpdatePanban(sssq);
                }
                ///保存麻醉记录单
                int result = 0;
                List<string> mzdList1 = new List<string>();
                mzdList1.Add(this.cmbASA.Text.Trim());
                if (this.cbJizhen.Checked)
                    mzdList1.Add("1");
                else
                    mzdList1.Add("0");
                mzdList1.Add(this.cmbSQJinshi.Text.Trim());
                mzdList1.Add(this.txtTSBQing.Text.Trim());
                mzdList1.Add(this.txtSqzd.Text.Trim());
                mzdList1.Add(this.txtNssss.Text.Trim());
                mzdList1.Add(this.cmbTiwei.Text.Trim());
                mzdList1.Add(this.txtSZZD.Text.Trim());
                mzdList1.Add(this.txtSSSS.Text.Trim());
                mzdList1.Add(this.txtSSYS.Text.Trim());
                mzdList1.Add(this.txtMZYS.Text.Trim());
                mzdList1.Add(this.txtQXHS.Text.Trim());
                mzdList1.Add(this.txtXHHS.Text.Trim());
                mzdList1.Add(this.txtWeight.Text.Trim());
                mzdList1.Add(this.txtHeight.Text.Trim());
                mzdList1.Add(this.txtSqyy.Text.Trim());
                mzdList1.Add(this.tbChuNiao.Text.Trim());
                mzdList1.Add(this.tbChuxue.Text.Trim());
                mzdList1.Add(this.cmbQiekouType.Text.Trim());
                mzdList1.Add(this.tbQiekouCount.Text.Trim());
                mzdList1.Add(this.txtMZFF.Text.Trim());//麻醉方法
                mzdList1.Add(this.txtName.Text.Trim());
                mzdList1.Add(this.txtAge.Text.Trim());
                mzdList1.Add(this.txtSex.Text.Trim());
                mzdList1.Add(this.txtXueya.Text.Trim());
                mzdList1.Add(this.txtMaibo.Text.Trim());
                mzdList1.Add(this.txtHuxi.Text.Trim());
                mzdList1.Add(this.cmbXueXing.Text.Trim());
                mzdList1.Add(this.cmbMZXG.Text.Trim());//麻醉效果
                mzdList1.Add(this.txtQYZZ.Text.Trim());
                mzdList1.Add(this.cmbMZPM.Text.Trim());
                mzdList1.Add(this.txtMZyaowu.Text.Trim());
                mzdList1.Add(this.cmbQMYD.Text.Trim());
                mzdList1.Add(this.cmbQMWC.Text.Trim());
                mzdList1.Add(this.txtQDTQ.Text.Trim());
                mzdList1.Add(this.UserCG.Text.Trim());
                mzdList1.Add(this.userMZXG.Text.Trim());
                mzdList1.Add(this.userCSQK.Text.Trim());
                mzdList1.Add(this.userSSSJ.Text.Trim());
                mzdList1.Add(this.userMZSJ.Text.Trim());
                mzdList1.Add(this.txtShixue.Text.Trim());
                mzdList1.Add(this.txtHongxibao.Text.Trim());
                mzdList1.Add(this.txtChuniao.Text.Trim());
                mzdList1.Add(this.txtQuanxue.Text.Trim());
                mzdList1.Add(this.txtXiongshui.Text.Trim());
                mzdList1.Add(this.txtXuejiang.Text.Trim());
                mzdList1.Add(this.txtFushui.Text.Trim());
                mzdList1.Add(this.txtShuye.Text.Trim());
                mzdList1.Add(this.userSHZT.Text.Trim());
                mzdList1.Add(this.userZTYY.Text.Trim());
                mzdList1.Add(this.usercgh.Text.Trim());
                mzdList1.Add(cmbBRQX.Text.Trim());
                mzdList1.Add(Convert.ToString(_MzjldId));
                result = dal.UpdateMzjld1(mzdList1);
                return result;
            }
            catch (Exception)
            {

                MessageBox.Show("保存异常！");
                return 0;
            }
        }

        private void AddPointTSMenu_Click(object sender, EventArgs e)//右键检测点添加事件
        {
            PointManage slj = new PointManage(_MzjldId, 0, JHXcvp, JHXqdy, JHXsdz, JHXjsz, ZoomRegionList);
            slj.ShowDialog();
            BindJHDian();
            this.pictureBox2.Refresh();
            this.pictureBox3.Refresh();
            this.pictureBox4.Refresh();
        }


        #region <<画窗体格子>>

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen pblack2 = new Pen(Brushes.Black);
            pblack2.Width = 2;
            Pen pblack = new Pen(Brushes.Black);
            e.Graphics.DrawLine(pblack2, new Point(1, 5), new Point(1069, 5));
            e.Graphics.DrawLine(pblack, new Point(1, 150), new Point(1069, 150));
            e.Graphics.DrawLine(pblack2, new Point(1, 5), new Point(1, 166));
            e.Graphics.DrawLine(pblack2, new Point(1069, 5), new Point(1069, 166));
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {

            DataTable dtMax = mpdal.GetMaxPoint(_MzjldId);
            if (dtMax.Rows[0][0].ToString() != "")
            {
                MaxPointTime = Convert.ToDateTime(dtMax.Rows[0][0].ToString());
            }
            else
            {
                MaxPointTime = DateTime.Now;
            }
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
            e.Graphics.DrawLine(pblack2, new Point(1, 0), new Point(1, 560));
            e.Graphics.DrawLine(Pens.Black, new Point(1, 0), new Point(1069, 0));
            e.Graphics.DrawLine(Pens.Black, new Point(25, 1), new Point(25, 442));
            e.Graphics.DrawLine(Pens.Black, new Point(169, 1), new Point(169, 442));
            e.Graphics.DrawLine(pblack2, new Point(1069, 0), new Point(1069, 560));
            for (int i = 0; i <= 28; i++)
            {
                if (i == 3 || i == 17 || i == 24 || i == 28)
                    e.Graphics.DrawLine(Pens.Black, new Point(1, 15 * i), new Point(1069, 15 * i));
                else
                    e.Graphics.DrawLine(Pens.Black, new Point(25, 15 * i), new Point(1069, 15 * i));
            }
            //↑横细线            
            for (int i = 0; i < 60; i++)
                e.Graphics.DrawLine(pxuxian, new PointF((float)(i * 90 / 6 + 169), (float)1), new PointF((float)(i * 90 / 6 + 169), (float)422));
            for (int i = 0; i < 10; i++)
                e.Graphics.DrawLine(Pens.Black, new PointF((float)(i * 90 + 169), (float)1), new PointF((float)(i * 90 + 169), (float)422));

            #region //画气体
            ArrayList sssQT = new ArrayList();
            int dy = 0;// 控制画气体输出的Y坐标           
            foreach (adims_MODEL.Yongyao mz in mzqtList)
            {
                if (mz.Bz > 0)
                {
                    int x1 = 0;
                    int x2 = 0;
                    if (sssQT.Contains(mz.Name))
                        dy = dy - 1;
                    e.Graphics.DrawString(mz.Name, this.Font, Brushes.Black, new Point(35, 15 * (dy + 0) + 2));

                    if (mz.Bz == 1)
                    {
                        DateTime dtend = dtend = MaxPointTime;
                        x2 = TextValueLimit.TimeToX(dtend, ZoomRegionList) + 170;
                    }
                    else if (mz.Bz == 2)
                    {
                        x2 = TextValueLimit.TimeToX(mz.JsTime, ZoomRegionList) + 170;
                    }
                    x1 = TextValueLimit.TimeToX(mz.KsTime, ZoomRegionList) + 170;
                    /*
                            int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 170);
                            int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / _MonitorInterval + 170);
                            */
                    int y1 = 15 * (dy) + 5;
                    if (x1 > 170)
                    {
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                        e.Graphics.DrawString(mz.Yl.ToString() + mz.Dw.ToString(), this.Font, Brushes.Blue, new Point(x1, y1 - 8));
                    }
                    if (x2 >= 170 && x2 - x1 > 5)
                    {
                        if (mz.Bz == 1)
                        {
                            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1));
                            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1 + 10));
                        }
                        if (mz.Bz == 2)
                        {
                            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 5, y1 + 8), new Point(x2 + 5, y1 + 8) });
                        }
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
                    sssQT.Add(mz.Name);
                }
            }
            #endregion

            #region //画诱导药
            ArrayList sss = new ArrayList();
            int dy1 = 0;// 控制画气体输出的Y坐标           
            foreach (adims_MODEL.Yongyao yt in ydyList)//画诱导药
            {
                if (yt.Bz > 0)
                {
                    int x1 = 0;
                    int x2 = 0;
                    if (sss.Contains(yt.Name))
                    {
                        dy1 = dy1 - 1;
                    }
                    e.Graphics.DrawString(yt.Name, this.Font, Brushes.Black, new Point(30, 15 * (dy1) + 47));

                    if (yt.Bz == 1)
                    {
                        DateTime dtend = dtend = MaxPointTime;
                        x2 = TextValueLimit.TimeToX(dtend, ZoomRegionList) + 170;
                        //if (ssjsTime > Convert.ToDateTime("1990-01-01"))
                        //{
                        //    DataTable dtMax = mpdal.GetMaxPoint(_MzjldId);
                        //    if (dtMax.Rows[0][0].ToString() != "")
                        //    {
                        //        DateTime MaxTime = Convert.ToDateTime(dtMax.Rows[0][0]);
                        //        if (MaxTime > ssjsTime)
                        //        {
                        //            x2 = TextValueLimit.TimeToX(MaxTime, ZoomRegionList) + 170;
                        //        }
                        //        else
                        //        {
                        //            x2 = TextValueLimit.TimeToX(ssjsTime, ZoomRegionList) + 170;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        x2 = TextValueLimit.TimeToX(ssjsTime, ZoomRegionList) + 170;
                        //    }
                        //}
                        //else
                        //{
                        //    x2 = TextValueLimit.TimeToX(DateTime.Now, ZoomRegionList) + 170;
                        //}
                    }
                    else if (yt.Bz == 2)
                    {
                        x2 = TextValueLimit.TimeToX(yt.JsTime, ZoomRegionList) + 170;
                    }
                    x1 = TextValueLimit.TimeToX(yt.KsTime, ZoomRegionList) + 170;
                    int y1 = 15 * (dy1 + 0) + 50;
                    if (x1 > 170)
                    {
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                        e.Graphics.DrawString(yt.Yl.ToString() + yt.Dw.ToString(), this.Font, Brushes.Blue, new Point(x1, y1 - 8));
                    }
                    if (x2 >= 170 && x2 - x1 > 5 && yt.Cxyy == true)
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
                    sss.Add(yt.Name);

                }

            }
            #endregion

            #region //画局麻药

            //int dy2 = 0;// 控制画局麻药输出的Y坐标
            //ArrayList sssJMY = new ArrayList();
            //foreach (adims_MODEL.jtytsx jt in jmyList)//画局麻药
            //{
            //    if (jt.Bz > 0)
            //    {
            //        if (sssJMY.Contains(jt.Name))
            //        {
            //            dy2 = dy2 - 1;
            //        }
            //        e.Graphics.DrawString(jt.Name, this.Font, Brushes.Black, new Point(37, 15 * (dy2) + 302));
            //        TimeSpan t = new TimeSpan();
            //        TimeSpan t1 = new TimeSpan();
            //        if (jt.Bz == 1)
            //        {
            //            t = jt.Kssj - otime;
            //            t1 = DateTime.Now - otime;
            //        }
            //        else if (jt.Bz == 2)
            //        {
            //            t = jt.Kssj - otime;
            //            t1 = jt.Jssj - otime;
            //        }
            //        int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 170);
            //        int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / _MonitorInterval + 170);
            //        int y1 = 15 * (dy2) + 305;
            //        if (x1 > 170)
            //        {
            //            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
            //            e.Graphics.DrawString(jt.Jl.ToString() + jt.Dw.ToString(), this.Font, Brushes.Blue, new Point(x1, y1 - 8));
            //            //e.Graphics.DrawString(jt.Jl.ToString() + jt.Dw.ToString(), this.Font, Brushes.Black, new Point(960, y1-2));
            //        }
            //        //if (x2 - x1 > 5 && jt.Bz == 1 && jt.Cxyy == true)
            //        //{
            //        //    e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1));
            //        //    e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1 + 10));
            //        //}
            //        //else if (x2 - x1 > 5 && jt.Bz == 2 && jt.Cxyy == true)
            //        //{
            //        //    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 5, y1 + 8), new Point(x2 + 5, y1 + 8) });
            //        //}
            //        //if (x2 - x1 > 5 && jt.Cxyy == true)
            //        //{
            //        //    e.Graphics.DrawLine(pred2, new Point(x1, y1 + 5), new Point(x2, y1 + 5));
            //        //}
            //        dy2++;
            //        sssJMY.Add(jt.Name);
            //    }

            //}
            #endregion

            #region//画输液
            ArrayList sssSY = new ArrayList();
            int dy3 = 0;
            foreach (adims_MODEL.Yongyao mz in shuyeList)  ////画输液
            {
                int x1 = 0;
                int x2 = 0;
                if (mz.Bz > 0)
                {
                    if (sssSY.Contains(mz.Name))
                    {
                        dy3 = dy3 - 1;
                    }
                    e.Graphics.DrawString(mz.Name, this.Font, Brushes.Black, new Point(30, 15 * (dy3) + 258));

                    if (mz.Bz == 1)
                    {
                        DateTime dtend = dtend = MaxPointTime;
                        x2 = TextValueLimit.TimeToX(dtend, ZoomRegionList) + 170;
                    }
                    else if (mz.Bz == 2)
                    {
                        x2 = TextValueLimit.TimeToX(mz.JsTime, ZoomRegionList) + 170;
                    }
                    x1 = TextValueLimit.TimeToX(mz.KsTime, ZoomRegionList) + 170;
                    int y1 = 15 * (dy3 + 0) + 260;
                    if (x1 > 170)
                    {
                        e.Graphics.FillPolygon(Brushes.Black,
                            new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                        e.Graphics.DrawString(mz.Yl.ToString() + mz.Dw.ToString() + mz.Yyfs.ToString(),
                            this.Font, Brushes.Blue, new Point(x1, y1 - 8));
                    }
                    if (x2 >= 170 && x2 - x1 > 5 && mz.Cxyy == true)
                    {
                        if (mz.Bz == 1)
                        {
                            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1));
                            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1 + 10));
                        }
                        if (mz.Bz == 2)
                        {
                            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 5, y1 + 8), new Point(x2 + 5, y1 + 8) });
                        }
                        string str = (x2 - x1 + y1).ToString();
                        str += (x2 - x1 + y1).ToString();
                    }
                    if (x2 - x1 > 5 && mz.Cxyy == true)
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
                    dy3++;
                    sssSY.Add(mz.Name);

                }
                //if (sssSY.Contains(mz.Name))
                //    dy3 = dy3 - 1;
                //if (shuyeList.Count > 0)
                //{
                //    if (mz.Bz > 0)
                //    {
                //        e.Graphics.DrawString(mz.Name, this.Font, Brushes.Black, new Point(35, 15 * (dy3) + 258));
                //        TimeSpan t = new TimeSpan();
                //        TimeSpan t1 = new TimeSpan();
                //        if (mz.Bz == 1)
                //        {
                //            t = mz.KsTime - otime;
                //            t1 = DateTime.Now - otime;
                //        }
                //        else if (mz.Bz == 2)
                //        {
                //            t = mz.KsTime - otime;
                //            t1 = mz.JsTime - otime;
                //        }
                //        int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 170);
                //        int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / _MonitorInterval + 170);
                //        int y1 = 15 * (dy3) + 260;
                //        if (x1 > 170)
                //        {
                //            e.Graphics.DrawString(mz.Yl.ToString() + mz.Dw.ToString(), this.Font, Brushes.Blue, new Point(x1 - 10, y1 - 8));
                //            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                //        }
                //        dy3++;
                //        sssSY.Add(mz.Name);
                //    }
                //}
            }
            #endregion

            #region//画输血
            ArrayList sssSX = new ArrayList();
            int dy4 = 0;
            foreach (adims_MODEL.Yongyao mz in shuxueList)  ////画输血
            {
                if (mz.Bz > 0)
                {
                    int x1 = 0;
                    int x2 = 0;
                    if (sssSX.Contains(mz.Name))
                    {
                        dy4 = dy4 - 1;
                    }
                    e.Graphics.DrawString(mz.Name, this.Font, Brushes.Black, new Point(30, 15 * (dy4) + 362));
                    if (mz.Bz == 1)
                    {
                        DateTime dtend = dtend = MaxPointTime;
                        x2 = TextValueLimit.TimeToX(dtend, ZoomRegionList) + 170;

                    }
                    else if (mz.Bz == 2)
                    {
                        //t = mz.KsTime - otime;
                        //t1 = mz.JsTime - otime;
                        x2 = TextValueLimit.TimeToX(mz.JsTime, ZoomRegionList) + 170;
                    }
                    x1 = TextValueLimit.TimeToX(mz.KsTime, ZoomRegionList) + 170;
                    int y1 = 15 * (dy4 + 0) + 365;
                    if (x1 > 170)
                    {
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                        e.Graphics.DrawString(mz.Yl.ToString() + mz.Dw.ToString(), this.Font, Brushes.Blue, new Point(x1, y1 - 8));
                    }
                    if (x2 - x1 > 5 && mz.Cxyy == true)
                    {
                        if (mz.Bz == 1)
                        {
                            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1));
                            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1 + 10));
                        }
                        if (mz.Bz == 2)
                        {
                            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 5, y1 + 8), new Point(x2 + 5, y1 + 8) });
                        }
                        string str = (x2 - x1 + y1).ToString();
                        str += (x2 - x1 + y1).ToString();
                    }
                    if (x2 - x1 > 5 && mz.Cxyy == true)
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
                    dy4++;
                    sssSX.Add(mz.Name);

                }
                //if (sssSX.Contains(mz.Name))
                //    dy4 = dy4 - 1;
                //if (shuxueList.Count > 0)
                //{
                //    if (mz.Bz > 0)
                //    {
                //        e.Graphics.DrawString(mz.Name, this.Font, Brushes.Black, new Point(35, 15 * (dy4) + 362));
                //        TimeSpan t = new TimeSpan();
                //        TimeSpan t1 = new TimeSpan();
                //        if (mz.Bz == 1)
                //        {
                //            t = mz.KsTime - otime;
                //            t1 = DateTime.Now - otime;
                //        }
                //        else if (mz.Bz == 2)
                //        {
                //            t = mz.KsTime - otime;
                //            t1 = mz.JsTime - otime;
                //        }
                //        int y1 = 15 * (dy4) + 365;

                //        int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 170);
                //        int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / _MonitorInterval + 170);
                //        if (x1 > 170)
                //        {
                //            e.Graphics.DrawString(mz.Yl.ToString() + mz.Dw.ToString(), this.Font, Brushes.Blue, new Point(x1 - 10, y1 - 8));
                //            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                //        }
                //        dy4++;
                //        sssSX.Add(mz.Name);
                //    }
                //}
            }
            #endregion

            #region //画SPO2
            foreach (adims_MODEL.jhxm jt in jhxmValue)
            {
                if (jt.Sy == "SpO2")
                {
                    TimeSpan t = new TimeSpan();
                    t = jt.D - otime;
                    TimeSpan t1 = new TimeSpan();
                    t1 = _CgTime - otime;
                    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 163);
                    int y = 512;
                    int x1 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / _MonitorInterval + 163);
                    if (x > 170)
                    {
                        e.Graphics.FillRectangle(Brushes.Pink, x, y, 14, 12);
                        e.Graphics.DrawString(jt.V.ToString(), (jt.V / 100 > 0 ? zt7 : this.Font), Brushes.Black,
                        new Point((jt.V / 100 > 0 ? (x - 2) : x), y));
                    }
                }
            }
            #endregion

        }
        /// <summary>
        /// 计算输液总量
        /// </summary>
        private void SumShuye()
        {
            int sumsy = 0;
            DataTable dtSy = _YongyaoListDal.GetYongyaoList(_MzjldId, (int)EnumCreator.YongyaoType.输液);//4输液
            for (int i = 0; i < dtSy.Rows.Count; i++)
            {
                if (dtSy.Rows[i]["yl"].ToString() != "")
                {
                    sumsy += Convert.ToInt32(dtSy.Rows[i]["yl"]);
                }
            }
            txtShuye.Text = sumsy.ToString();
        }
        /// <summary>
        /// 计算输血总量
        /// </summary>
        private void SumShuxue()
        {
            int sumhxb = 0;
            int sumquanxue = 0;
            int sumxuejiang = 0;
            DataTable dtSy = _YongyaoListDal.GetYongyaoList(_MzjldId, (int)EnumCreator.YongyaoType.输血);//5输血
            for (int i = 0; i < dtSy.Rows.Count; i++)
            {
                if (dtSy.Rows[i]["hxb"].ToString() != "")
                {
                    sumhxb += Convert.ToInt32(dtSy.Rows[i]["hxb"]);
                }
                if (dtSy.Rows[i]["xuejiang"].ToString() != "")
                {
                    sumxuejiang += Convert.ToInt32(dtSy.Rows[i]["xuejiang"]);
                }
                if (dtSy.Rows[i]["quanxue"].ToString() != "")
                {
                    sumquanxue += Convert.ToInt32(dtSy.Rows[i]["quanxue"]);
                }
            }
            txtHongxibao.Text = sumhxb.ToString();
            txtQuanxue.Text = sumquanxue.ToString();
            txtXuejiang.Text = sumxuejiang.ToString();
        }
        private void pictureBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.X > 0 && e.X < 169)
            {
                if (e.Y > 0 && e.Y < 45)
                {

                    addQty qt = new addQty(_MzjldId);
                    qt.ShowDialog();
                    BindQtList();
                }
                else if (e.Y > 45 && e.Y < 255)
                {
                    addYdyao mzytform = new addYdyao(_MzjldId);
                    mzytform.ShowDialog();
                    BindYdyList();
                }

                //else if (e.Y > 300 && e.Y < 330)
                //{
                //    addJuMaYao szyy = new addJuMaYao(_MzjldId);
                //    szyy.ShowDialog();
                //    BindJmyList();
                //}
                else if (e.Y > 255 && e.Y < 360)
                {
                    addShuye szyy = new addShuye(_MzjldId);
                    szyy.ShowDialog();
                    BindShuyeList();
                    SumShuye();//输液总量
                }
                else if (e.Y > 360 && e.Y < 420)
                {
                    addShuxue szyy = new addShuxue(_MzjldId);
                    szyy.ShowDialog();
                    BindShuxueList();
                    SumShuxue();
                }

            }
            if (e.X > 170 && e.Y > 510 && e.Y < 525)
            {
                foreach (adims_MODEL.jhxm jt in jhxmValue)
                {
                    if (jt.Sy == "SpO2")
                    {
                        TimeSpan t = new TimeSpan();
                        t = jt.D - otime;
                        int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 163);
                        int y = 512;
                        if (e.X > x && e.X < x + 13 && e.Y > y + 1 && e.Y < y + 13)
                        {
                            UpdateJHXM formxgjhsj = new UpdateJHXM(_MzjldId, jt, 0);
                            formxgjhsj.ShowDialog();
                            break;
                        }
                    }
                }
            }
            pictureBox2.Refresh();
            btnLeft.Focus();
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            p2x = e.X; p2y = e.Y;
            int dy = 0;
            ArrayList sssQT = new ArrayList();
            foreach (adims_MODEL.Yongyao qty in mzqtList)
            {
                if (qty.Bz > 0)
                {
                    if (sssQT.Contains(qty.Name))
                        dy = dy - 1;
                    TimeSpan t = new TimeSpan();
                    t = qty.KsTime - otime;
                    TimeSpan t2 = new TimeSpan();
                    t2 = qty.JsTime - otime;
                    int x = TextValueLimit.TimeToX(qty.KsTime, ZoomRegionList) + 170;
                    //(int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 170);
                    int y = 15 * (dy) + 5;
                    int x2 = TextValueLimit.TimeToX(qty.JsTime, ZoomRegionList) + 170;
                    //(int)((t2.Days * 24 * 60 + t2.Hours * 60 + t2.Minutes) * 15 / _MonitorInterval + 170);
                    int y2 = 15 * (dy) + 5;
                    if (p2x > x - 5 && p2x < x + 5 && p2y > y - 1 && p2y < y + 8)
                    { flagP2 = 1; typeP2 = 1; t_mzqt = qty; }
                    if (p2x > x2 - 5 && p2x < x2 + 5 && p2y > y2 - 1 && p2y < y2 + 8)
                    { flagP2 = 1; typeP2 = 11; t_mzqt = qty; }
                    dy++;
                    sssQT.Add(qty.Name);
                }

            }
            int dy1 = 0;
            ArrayList sss = new ArrayList();
            foreach (adims_MODEL.Yongyao ydy in ydyList)
            {
                if (ydy.Bz > 0)
                {
                    if (dy1 > 0 && sss.Contains(ydy.Name))
                    {
                        dy1 = dy1 - 1;
                    }
                    TimeSpan t = new TimeSpan();
                    t = ydy.KsTime - otime;
                    int x = TextValueLimit.TimeToX(ydy.KsTime, ZoomRegionList) + 170;
                    //(int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 170);
                    int y = 15 * (dy1) + 50;
                    TimeSpan t2 = new TimeSpan();
                    t2 = ydy.JsTime - otime;
                    int x2 = TextValueLimit.TimeToX(ydy.JsTime, ZoomRegionList) + 170;
                    //(int)((t2.Days * 24 * 60 + t2.Hours * 60 + t2.Minutes) * 15 / _MonitorInterval + 170);
                    int y2 = 15 * (dy1) + 50;
                    // MessageBox.Show(x.ToString() + "    " + y.ToString() + "   " + p2x.ToString() + "    " + p2y.ToString());
                    if (p2x > x - 5 && p2x < x + 5 && p2y > y - 1 && p2y < y + 8)
                    { flagP2 = 1; typeP2 = 2; t_ydy = ydy; }
                    if (p2x > x2 - 5 && p2x < x2 + 5 && p2y > y2 - 1 && p2y < y2 + 8)
                    { flagP2 = 1; typeP2 = 22; t_ydy = ydy; }
                    dy1++;
                    sss.Add(ydy.Name);
                }
            }
            //int dy2 = 0;
            //ArrayList sssJMY = new ArrayList();
            //foreach (adims_MODEL.Yongyao jmy in jmyList)
            //{
            //    if (jmy.Bz > 0)
            //    {
            //        if (sssJMY.Contains(jmy.Name))
            //            dy2 = dy2 - 1;
            //        TimeSpan t = new TimeSpan();
            //        t = jmy.KsTime - otime;
            //        int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 170);
            //        int y = 15 * (dy2) + 260;
            //        if (p2x > x - 5 && p2x < x + 5 && p2y > y - 1 && p2y < y + 8)
            //        { flagP2 = 1; typeP2 = 3; t_jmy = jmy; }
            //        dy2++;
            //        sssJMY.Add(jmy.Name);
            //    }
            //}
            int dy3 = 0;
            ArrayList sssSY = new ArrayList();
            foreach (adims_MODEL.Yongyao sye in shuyeList)
            {
                if (sye.Bz > 0)
                {
                    if (dy3 > 0 && sssSY.Contains(sye.Name))
                    {
                        dy3 = dy3 - 1;
                    }
                    TimeSpan t = new TimeSpan();
                    t = sye.KsTime - otime;
                    int x = TextValueLimit.TimeToX(sye.KsTime, ZoomRegionList) + 170;
                    //(int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 170);
                    int y = 15 * (dy3) + 260;
                    TimeSpan t2 = new TimeSpan();
                    t2 = sye.JsTime - otime;
                    int x2 = TextValueLimit.TimeToX(sye.JsTime, ZoomRegionList) + 170;
                    //(int)((t2.Days * 24 * 60 + t2.Hours * 60 + t2.Minutes) * 15 / _MonitorInterval + 170);
                    int y2 = 15 * (dy3) + 260;

                    if (p2x > x - 5 && p2x < x + 5 && p2y > y - 1 && p2y < y + 8)
                    { flagP2 = 1; typeP2 = 4; t_shuye = sye; }
                    if (p2x > x2 - 5 && p2x < x2 + 5 && p2y > y2 - 1 && p2y < y2 + 8)
                    { flagP2 = 1; typeP2 = 44; t_shuye = sye; }
                    dy3++;
                    sssSY.Add(sye.Name);
                }
                //if (sye.Bz > 0)
                //{
                //    if (sssSY.Contains(sye.Name))
                //        dy3 = dy3 - 1;
                //    TimeSpan t = new TimeSpan();
                //    t = sye.KsTime - otime;
                //    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 170);
                //    int y = 15 * (dy3) + 260;
                //    if (p2x > x - 5 && p2x < x + 5 && p2y > y - 1 && p2y < y + 8)
                //    { flagP2 = 1; typeP2 = 4; t_shuye = sye; }
                //    dy3++;
                //    sssSY.Add(sye.Name);
                //}
            }
            int dy4 = 0;
            ArrayList sssSX = new ArrayList();
            foreach (adims_MODEL.Yongyao sxue in shuxueList)
            {
                if (sxue.Bz > 0)
                {
                    if (dy4 > 0 && sssSX.Contains(sxue.Name))
                    {
                        dy4 = dy4 - 1;
                    }
                    TimeSpan t = new TimeSpan();
                    t = sxue.KsTime - otime;
                    int x = TextValueLimit.TimeToX(sxue.KsTime, ZoomRegionList) + 170;
                    //(int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 170);
                    int y = 15 * (dy4) + 365;
                    TimeSpan t2 = new TimeSpan();
                    t2 = sxue.JsTime - otime;
                    int x2 = TextValueLimit.TimeToX(sxue.JsTime, ZoomRegionList) + 170;
                    //(int)((t2.Days * 24 * 60 + t2.Hours * 60 + t2.Minutes) * 15 / _MonitorInterval + 170);
                    int y2 = 15 * (dy4) + 365;

                    if (p2x > x - 5 && p2x < x + 5 && p2y > y - 1 && p2y < y + 8)
                    { flagP2 = 1; typeP2 = 5; t_shuxue = sxue; }
                    if (p2x > x2 - 5 && p2x < x2 + 5 && p2y > y2 - 1 && p2y < y2 + 8)
                    { flagP2 = 1; typeP2 = 55; t_shuxue = sxue; }
                    dy4++;
                    sssSX.Add(sxue.Name);
                }
                //if (sxue.Bz > 0)
                //{
                //    if (sssSX.Contains(sxue.Name))
                //        dy4 = dy4 - 1;
                //    TimeSpan t = new TimeSpan();
                //    t = sxue.KsTime - otime;
                //    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 170);
                //    int y = 15 * (dy4) + 365;
                //    if (p2x > x - 5 && p2x < x + 5 && p2y > y - 1 && p2y < y + 8)
                //    { flagP2 = 1; typeP2 = 5; t_shuxue = sxue; }
                //    dy4++;
                //    sssSX.Add(sxue.Name);
                //}
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
            if (flagP2 == 1)
            {
                if (typeP2 == 1)
                {
                    int ADD = TextValueLimit.XtoMinute((int)p2x - 170, ZoomRegionList);
                    t_mzqt.KsTime = otime.AddMinutes(ADD);
                    //t_mzqt.KsTime = otime.AddMinutes((p2x - 170) / 15 * _MonitorInterval);
                }
                if (typeP2 == 11)
                {
                    int ADD = TextValueLimit.XtoMinute((int)p2x - 170, ZoomRegionList);
                    t_mzqt.JsTime = otime.AddMinutes(ADD);
                }
                if (typeP2 == 2)
                {
                    int ADD = TextValueLimit.XtoMinute((int)p2x - 170, ZoomRegionList);
                    t_ydy.KsTime = otime.AddMinutes(ADD);
                    //t_ydy.KsTime = otime.AddMinutes((p2x - 170) / 15 * _MonitorInterval);
                }
                if (typeP2 == 22)
                {
                    int ADD = TextValueLimit.XtoMinute((int)p2x - 170, ZoomRegionList);
                    t_ydy.JsTime = otime.AddMinutes(ADD);
                    //t_ydy.JsTime = otime.AddMinutes((p2x - 170) / 15 * _MonitorInterval);
                }
                if (typeP2 == 3)
                {
                    int ADD = TextValueLimit.XtoMinute((int)p2x - 170, ZoomRegionList);
                    t_jmy.KsTime = otime.AddMinutes(ADD);
                    // t_jmy.KsTime = otime.AddMinutes((p2x - 170) / 15 * _MonitorInterval);
                }
                if (typeP2 == 4)//输液移动
                {
                    int ADD = TextValueLimit.XtoMinute((int)p2x - 170, ZoomRegionList);
                    t_shuye.KsTime = otime.AddMinutes(ADD);
                    //t_shuye.KsTime = otime.AddMinutes((p2x - 170) / 15 * _MonitorInterval);
                }
                if (typeP2 == 44)
                {
                    int ADD = TextValueLimit.XtoMinute((int)p2x - 170, ZoomRegionList);
                    t_shuye.JsTime = otime.AddMinutes(ADD);
                    //t_shuye.JsTime = otime.AddMinutes((p2x - 170) / 15 * _MonitorInterval);
                }
                if (typeP2 == 5)//输血移动
                {
                    int ADD = TextValueLimit.XtoMinute((int)p2x - 170, ZoomRegionList);
                    t_shuxue.KsTime = otime.AddMinutes(ADD);
                    //t_shuxue.KsTime = otime.AddMinutes((p2x - 170) / 15 * _MonitorInterval);
                }
                if (typeP2 == 55)
                {
                    int ADD = TextValueLimit.XtoMinute((int)p2x - 170, ZoomRegionList);
                    t_shuxue.JsTime = otime.AddMinutes(ADD);
                    //t_shuxue.JsTime = otime.AddMinutes((p2x - 170) / 15 * _MonitorInterval);
                }
                pictureBox2.Refresh();
            }

        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            if (flagP2 != 0)
            {
                flagP2 = 0;
                if (typeP2 == 1)
                {
                    bll.UpdateYongyaoKssj(_MzjldId, t_mzqt);
                }
                if (typeP2 == 11)
                {
                    bll.UpdateYongyaoJssj(_MzjldId, t_mzqt);
                }
                if (typeP2 == 2)
                {
                    if (!t_ydy.Cxyy)
                        bll.UpdateYongyaoKssjJssj(_MzjldId, t_ydy);
                    else
                        bll.UpdateYongyaoKssj(_MzjldId, t_ydy);
                }
                if (typeP2 == 22)
                {
                    bll.UpdateYongyaoJssj(_MzjldId, t_ydy);
                }
                if (typeP2 == 3)
                {
                    bll.UpdateYongyaoKssjJssj(_MzjldId, t_jmy);
                }
                if (typeP2 == 4)
                {
                    if (!t_shuye.Cxyy)
                        bll.UpdateYongyaoKssjJssj(_MzjldId, t_shuye);
                    else
                        bll.UpdateYongyaoKssj(_MzjldId, t_shuye);
                }
                if (typeP2 == 44)
                {
                    bll.UpdateYongyaoJssj(_MzjldId, t_shuye);
                }
                if (typeP2 == 5)
                {
                    if (!t_shuxue.Cxyy)
                        bll.UpdateYongyaoKssjJssj(_MzjldId, t_shuxue);
                    else
                        bll.UpdateYongyaoKssj(_MzjldId, t_shuxue);
                }
                if (typeP2 == 55)
                {
                    bll.UpdateYongyaoJssj(_MzjldId, t_shuxue);
                }
            }
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
            foreach (adims_MODEL.point tp in ssyList)//画收缩压
            {
                int x = 0, y = 0;
                TimeSpan t = new TimeSpan();
                t = tp.D - otime;
                x = TextValueLimit.TimeToX(tp.D, ZoomRegionList) + 170;
                //x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 170);
                if (tp.V > 230)
                {
                    y = 368 - 345;
                    //e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Red, x, y);
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

            foreach (adims_MODEL.point tp in szyList)//画舒张压
            {
                int x = 0, y = 0;
                TimeSpan t = new TimeSpan();
                t = tp.D - otime;
                x = TextValueLimit.TimeToX(tp.D, ZoomRegionList) + 170;
                //x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 170);
                if (tp.V > 230)
                {
                    y = 368 - 345;
                    // e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Red, x, y);
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

            foreach (adims_MODEL.tw_point tp in twList)//画体温
            {
                if (tp.V > 0)
                {
                    float x, y;
                    TimeSpan t = new TimeSpan();
                    t = tp.D - otime;
                    x = TextValueLimit.TimeToX(tp.D, ZoomRegionList) + 170;
                    //x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 170);
                    if (tp.V > 40)
                    {
                        y = 368 - 345;
                        //e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Red, x, y);
                    }
                    else
                        y = (float)(368 - ((tp.V - 29) * 30));
                    if (x > 169)
                    {
                        e.Graphics.DrawPolygon(Pens.Maroon, new PointF[3] { new PointF(x, y), new PointF(x + 4, y + 6), new PointF(x - 4, y + 6) });
                        if (dyd2 != 0 && lastpoint2.X > 169)
                            e.Graphics.DrawLine(Pens.Maroon, new PointF(x, y), lastpoint2);
                    }
                    lastpoint2.X = (int)(x + 2);
                    lastpoint2.Y = (int)y;
                    dyd2++;
                }
            }

            foreach (adims_MODEL.point tp in mboList)//画脉搏
            {
                int x = 0, y = 0;
                TimeSpan t = new TimeSpan();
                t = tp.D - otime;
                x = TextValueLimit.TimeToX(tp.D, ZoomRegionList) + 170;
                //x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 170);
                if (tp.V > 230)
                {
                    y = 368 - 345;
                    //e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Red, x, y);
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
            int iJikong = 0;
            foreach (adims_MODEL.point tp in hxlList)//画呼吸
            {

                int x = 0, y = 0;
                TimeSpan t = new TimeSpan();
                t = tp.D - otime;
                x = TextValueLimit.TimeToX(tp.D, ZoomRegionList) + 170;
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
                    {
                        e.Graphics.DrawString("C", this.Font, Brushes.DarkCyan, x - 3, y - 5);
                    }
                    else if (fzksTime < tp.D && fzjsTime > tp.D)
                    {
                        e.Graphics.DrawString("A", this.Font, Brushes.DarkCyan, x - 3, y - 5);
                    }
                    else
                        e.Graphics.DrawEllipse(Pens.DarkCyan, new Rectangle(x - 3, y - 3, 6, 6));
                    if ((dyd4 != 0 && lastpoint4.X > 169))
                        e.Graphics.DrawLine(Pens.DarkCyan, new Point(x, y), lastpoint4);
                }
                lastpoint4.X = x;
                lastpoint4.Y = y;
                dyd4++;
            }
            //foreach (adims_MODEL.point tp in etco2List)//画ETCO2
            //{
            //    int x, y;
            //    TimeSpan t = new TimeSpan();
            //    t = tp.D - otime;
            //    x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 170);
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
            int szsji = 0;
            foreach (adims_MODEL.szsj s in szsjList)//画术中事件
            {
                int x1 = 0;
                TimeSpan t = new TimeSpan();
                t = s.D - otime;
                x1 = TextValueLimit.TimeToX(s.D, ZoomRegionList) + 165;
                float y1 = (float)(s.Y_zb);

                if (x1 > 165)
                {
                    //e.Graphics.FillRectangle(Brushes.Pink, x1, y1, szsji > 9 ? 14 : 10, 12);
                    e.Graphics.DrawString(ArrayHelper.ReplaceNumToLetter(szsji), this.Font, Brushes.Black, new PointF(x1, y1));
                }
                szsji++;
            }
            int tsyyi = 1;
            foreach (adims_MODEL.Yongyao s in tsyyList)//画其他用药
            {
                int x1 = 0;
                TimeSpan t = new TimeSpan();
                t = s.KsTime - otime;
                x1 = TextValueLimit.TimeToX(s.KsTime, ZoomRegionList) + 165;
                // x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 165);
                int y1 = s.Y_zb;
                if (x1 > 165)
                {
                    //e.Graphics.FillEllipse(Brushes.LightGreen, x1, y1, tsyyi > 9 ? 14 : 10, 10);
                    e.Graphics.DrawString(tsyyi.ToString(), this.Font, Brushes.Blue, new Point(tsyyi > 9 ? x1 - 2 : x1, y1));
                }
                tsyyi++;
            }
            #endregion

        }
        adims_MODEL.jhxm T_etco2 = new adims_MODEL.jhxm();
        int typeP4 = 0;//用来判断特殊用药坐标移动
        int typeP5 = 0;//用来判断术中事件坐标移动
        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            #region //是否选中监测点

            foreach (adims_MODEL.point tssy in ssyList)
            {
                TimeSpan t = new TimeSpan();
                t = tssy.D - otime;
                int x = TextValueLimit.TimeToX(tssy.D, ZoomRegionList) + 170;
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
                int x = TextValueLimit.TimeToX(tssy.D, ZoomRegionList) + 170;
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
                int x = TextValueLimit.TimeToX(tssy.D, ZoomRegionList) + 170;
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
                int x = TextValueLimit.TimeToX(tssy.D, ZoomRegionList) + 170;
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
                int x = TextValueLimit.TimeToX(tssy.D, ZoomRegionList) + 170;
                int y = 0;
                if (tssy.V > 40)
                    y = (int)(368 - 345);
                else
                    y = (int)(368 - ((tssy.V - 29) * 30));
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
            }

            foreach (adims_MODEL.point tssy in etco2List)
            {
                TimeSpan t = new TimeSpan();
                t = tssy.D - otime;
                int x = TextValueLimit.TimeToX(tssy.D, ZoomRegionList) + 170;
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

            foreach (adims_MODEL.szsj sj in szsjList)//是否选中术中事件X坐标
            {
                TimeSpan t = new TimeSpan();
                t = sj.D - otime;
                int x = TextValueLimit.TimeToX(sj.D, ZoomRegionList) + 165;
                int y = sj.Y_zb;
                if (e.X > x - 8 && e.X < x + 8 && e.Y < y + 12 && e.Y > y)
                { flagP3 = 1; typeP3 = 2; t_szsj = sj; }
            }
            foreach (adims_MODEL.szsj sj in szsjList)//是否选中术中事件Y坐标
            {
                TimeSpan t = new TimeSpan();
                t = sj.D - otime;
                int x = TextValueLimit.TimeToX(sj.D, ZoomRegionList) + 165;
                int y = sj.Y_zb;
                if (e.X > x - 8 && e.X < x + 8 && e.Y < y + 12 && e.Y > y)
                { flagP3 = 1; typeP5 = 22; t_szsj = sj; }
            }
            foreach (adims_MODEL.Yongyao ts in tsyyList)//是否选中特殊用药X坐标
            {
                TimeSpan t = new TimeSpan();
                t = ts.KsTime - otime;
                int x = TextValueLimit.TimeToX(ts.KsTime, ZoomRegionList) + 165;
                int y = ts.Y_zb;
                if (e.X > x - 8 && e.X < x + 8 && e.Y < y + 12 && e.Y > y)
                { flagP3 = 1; typeP3 = 3; t_tsyy = ts; }
            }
            foreach (adims_MODEL.Yongyao ts in tsyyList)//是否选中特殊用药Y坐标
            {
                TimeSpan t = new TimeSpan();
                t = ts.KsTime - otime;
                int x = TextValueLimit.TimeToX(ts.KsTime, ZoomRegionList) + 165;
                int y = ts.Y_zb;
                if (e.X > x - 8 && e.X < x + 8 && e.Y < y + 12 && e.Y > y)
                { flagP3 = 1; typeP4 = 33; t_tsyy = ts; }
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
                    string value = ((368 - p3y) / 30 + 29).ToString();
                    if (value.Contains("."))
                    {
                        int index = value.IndexOf(".");//查找最后一个.所在的位置
                        string ss = value.Substring(0, index + 2);   // 取子字符串。
                        tw_point.V = Convert.ToSingle(ss);
                    }
                    else
                    {
                        tw_point.V = (float)((368 - p3y) / 30 + 29);
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
                    int t = e.X;
                    int addM = TextValueLimit.XtoMinute((int)p3x - 165, ZoomRegionList);
                    t_tsyy.KsTime = otime.AddMinutes(addM);
                    labVisibleTimer.Enabled = true;
                    lab2.Visible = true;
                    lab2.BackColor = Color.Transparent;
                    lab2.ForeColor = Color.Blue;
                    lab2.AutoSize = true;
                    lab2.Text = t_tsyy.KsTime.ToString("HH:mm");
                    pictureBox3.Controls.Add(lab2);
                    lab2.Location = new Point(e.X, e.Y);
                    lab2.BringToFront();
                }
                if (typeP4 == 33)//特殊用药移动
                {
                    t_tsyy.Y_zb = e.Y;
                    labVisibleTimer.Enabled = true;
                    lab2.Visible = true;
                    lab2.BackColor = Color.Transparent;
                    lab2.ForeColor = Color.Blue;
                    lab2.AutoSize = true;
                    lab2.Text = t_tsyy.Y_zb.ToString();
                    pictureBox3.Controls.Add(lab2);
                    lab2.Location = new Point(e.X, e.Y);
                    lab2.BringToFront();
                }
                if (typeP3 == 2)//术中事件移动
                {
                    int t = e.X;
                    int addM = TextValueLimit.XtoMinute((int)p3x - 165, ZoomRegionList);
                    t_szsj.D = otime.AddMinutes(addM);
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
                if (typeP5 == 22)//术中事件移动
                {
                    t_szsj.Y_zb = e.Y;
                    labVisibleTimer.Enabled = true;
                    lab2.Visible = true;
                    lab2.BackColor = Color.Transparent;
                    lab2.ForeColor = Color.Blue;
                    lab2.AutoSize = true;
                    lab2.Text = t_szsj.Y_zb.ToString();
                    pictureBox3.Controls.Add(lab2);
                    lab2.Location = new Point(e.X, e.Y);
                    lab2.BringToFront();
                }
            }
            pictureBox3.Refresh();
            labVisibleTimer.Enabled = true;
        }
        MzjldPointDal mpdal = new MzjldPointDal();
        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            if (flagP3 == 1)
            {
                flagP3 = 0;
                if (typeP3 == 1)
                {
                    mpdal.UpdateMzjldPoint(_MzjldId, t_point);
                }
                if (typeP3 == 4)
                {
                    mpdal.UpdateMzjldPointTemp(_MzjldId, tw_point);
                }
                if (typeP3 == 2 && typeP5 == 22)
                {
                    bll.xgszsjTime(_MzjldId, t_szsj);
                    BindSZSJ();
                }
                //if (typeP3 == 3)
                //{
                //    bll.xgtsyyTime(_MzjldId, t_tsyy);
                //    BindTsyy();
                //}
                if (typeP4 == 33 && typeP3 == 3)
                {
                    bll.xgtsyyZB(_MzjldId, t_tsyy);
                    BindTsyy();
                }
            }
            pictureBox3.Refresh();
            listBoxSZSJ.Focus();
            listBoxOtherMedical.Focus();
        }

        private void pictureBox4_Paint(object sender, PaintEventArgs e)
        {
            JHXcvp = 0;
            JHXqdy = 0;
            JHXsdz = 0;
            JHXjsz = 0;
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
            e.Graphics.DrawLine(Pens.Black, new Point(1, 0), new Point(1069, 0));
            e.Graphics.DrawLine(Pens.Black, new Point(25, 1), new Point(25, 90));
            e.Graphics.DrawLine(Pens.Black, new Point(169, 1), new Point(169, 90));

            e.Graphics.DrawLine(Pens.Black, new Point(1, 90), new Point(1069, 90));
            e.Graphics.DrawLine(Pens.Black, new Point(1, 210), new Point(1069, 210));

            e.Graphics.DrawLine(pblack2, new Point(1, 0), new Point(1, 420));
            e.Graphics.DrawLine(pblack2, new Point(1069, 0), new Point(1069, 420));
            e.Graphics.DrawLine(pblack2, new Point(1, 420), new Point(1069, 420));
            for (int i = 0; i <= 6; i++)
            {
                if (i == 6 || i == 20 || i == 24 || i == 28)
                    e.Graphics.DrawLine(Pens.Black, new Point(1, 15 * i), new Point(1069, 15 * i));
                else
                    e.Graphics.DrawLine(Pens.Black, new Point(25, 15 * i), new Point(1069, 15 * i));
            }
            //↑横细线            
            for (int i = 0; i < 60; i++)
                e.Graphics.DrawLine(pxuxian, new PointF((float)(i * 90 / 6 + 169), (float)1), new PointF((float)(i * 90 / 6 + 169), (float)90));
            for (int i = 0; i < 10; i++)
                e.Graphics.DrawLine(Pens.Black, new PointF((float)(i * 90 + 169), (float)1), new PointF((float)(i * 90 + 169), (float)90));

            #region 画监护项目
            int i_jhxm = 0;
            foreach (string str in jhxmIn)
            {
                e.Graphics.DrawString(str, zt8, Brushes.Black, 27, 2 + i_jhxm * 15);
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
                        int x = 0; int x1 = 0;
                        int y = 2 + i * 15;
                        TimeSpan t = new TimeSpan();
                        t = jt.D - otime;
                        TimeSpan t1 = new TimeSpan();
                        t1 = _CgTime - otime;
                        x = TextValueLimit.TimeToX(jt.D, ZoomRegionList) + 163;
                        //x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / _MonitorInterval + 163);
                        x1 = TextValueLimit.TimeToX(_CgTime, ZoomRegionList) + 163;
                        //x1 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / _MonitorInterval + 163);
                        if (x > 170)
                        {
                            e.Graphics.FillRectangle(Brushes.Pink, x, y, 14, 12);
                            e.Graphics.DrawString(jt.V.ToString(), (jt.V / 100 > 0 ? zt7 : this.Font), Brushes.Black,
                            new Point((jt.V / 100 > 0 ? (x - 2) : x), y));
                        }
                    }
                    if (jhxmIn[i] == "CVP")
                    {
                        JHXcvp = 1;
                    }
                    else if (jhxmIn[i] == "气道压")
                    {
                        JHXqdy = 1;
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
        }

        private void pictureBox4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int x = e.X, y = e.Y;
            if (x > 25 && x < 170 && y > 0 && y < 90)
            {
                addJhxm f1 = new addJhxm(jhxmAll, jhxmIn, _MzjldId, 0);
                f1.ShowDialog();
                pictureBox4.Refresh();
            }
        }

        #endregion



        #region//迈瑞函数
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(100);
            // ser.MonitorRecord MonitorRecord = new ser.MonitorRecord();
            int spo2 = 0, pulse = 0, nibps = 0, nibpd = 0, nibpm = 0, arts = 0, artd = 0, artm = 0, hr = 0, ico2 = 0, rrc = 0;
            double etco2 = 0;
            int fff = 0;
            byte[] q = new byte[500];
            serialPort1.Read(q, 0, serialPort1.BytesToRead);
            for (int i = 0; i < q.Length - 3; i++)
            {
                if (q[i] == 1 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
                {
                    int j = i + 2; int m = 0;
                    while (q[j] != 0)
                    { m = m * 10 + q[j] - 48; j++; }
                    i = j;
                    //MonitorRecord.HRValue = m;
                    hr = m;
                    fff++;
                    // textBox4.Text = textBox4.Text + "hr:" + m.ToString();
                }

                if (q[i] == 100 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
                {
                    int j = i + 2; int m = 0;
                    while (q[j] != 0)
                    { m = m * 10 + q[j] - 48; j++; }
                    i = j;
                    // MonitorRecord.SpO2Value = m;
                    spo2 = m;
                    fff++;
                    //textBox4.Text = textBox4.Text + "Spo2:" + m.ToString();
                }
                if (q[i] == 101 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
                {
                    int j = i + 2; int m = 0;
                    while (q[j] != 0)
                    { m = m * 10 + q[j] - 48; j++; }
                    i = j;
                    // MonitorRecord.PulseValue = m;
                    pulse = m;
                    fff++;
                    //  textBox4.Text = textBox4.Text + "Pulse:" + m.ToString();
                }
                if (q[i] == 91 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
                {
                    int j = i + 2; int m = 0;
                    while (q[j] != 0)
                    { m = m * 10 + q[j] - 48; j++; }
                    i = j;
                    // MonitorRecord.NIBPSValue = m;
                    nibps = m;
                    fff++;
                    //textBox4.Text = textBox4.Text + "NIBPS:" + m.ToString();
                }
                if (q[i] == 92 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
                {
                    int j = i + 2; int m = 0;
                    while (q[j] != 0)
                    { m = m * 10 + q[j] - 48; j++; }
                    i = j;
                    //MonitorRecord.NIBPDValue = m;
                    nibpd = m;
                    fff++;
                    // textBox4.Text = textBox4.Text + "NIBPD:" + m.ToString();
                }
                if (q[i] == 93 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
                {
                    int j = i + 2; int m = 0;
                    while (q[j] != 0)
                    { m = m * 10 + q[j] - 48; j++; }
                    i = j;
                    // MonitorRecord.NIBPMValue = m;
                    nibpm = m;
                    fff++;
                    // textBox4.Text = textBox4.Text + "NIBPM:" + m.ToString();
                }
                if (q[i] == 32 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
                {
                    int j = i + 2; int m = 0;
                    while (q[j] != 0)
                    { m = m * 10 + q[j] - 48; j++; }
                    i = j;
                    arts = m;
                    fff++;
                    //textBox4.Text = textBox4.Text + "IBP1S:" + m.ToString();
                }
                if (q[i] == 33 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
                {
                    int j = i + 2; int m = 0;
                    while (q[j] != 0)
                    { m = m * 10 + q[j] - 48; j++; }
                    i = j;
                    artd = m;
                    fff++;
                    // textBox4.Text = textBox4.Text + "IBP1D:" + m.ToString();
                }
                if (q[i] == 34 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
                {
                    int j = i + 2; int m = 0;
                    while (q[j] != 0)
                    { m = m * 10 + q[j] - 48; j++; }
                    i = j;
                    artm = m;
                    fff++;
                }
                if (q[i] == 103 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
                {
                    int j = i + 2; double m = 0, n = 10;
                    while (q[j] != 0 && q[j] != 46)
                    { m = m * 10 + q[j] - 48; j++; }
                    if (q[j] == 46)
                    {
                        j++;
                        while (q[j] != 0)
                        {
                            m = m + ((double)q[j]) / n;
                            n = n * 10;
                            j++;
                        }
                    }
                    i = j;
                    // MonitorRecord.ETCO2Value = m;
                    etco2 = m;
                    fff++;
                }
                if (q[i] == 105 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
                {
                    int j = i + 2; int m = 0;
                    while (q[j] != 0)
                    { m = m * 10 + q[j] - 48; j++; }
                    i = j;
                    // MonitorRecord.ICO2Value = m;
                    ico2 = m;
                    fff++;
                }
                if (q[i] == 104 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
                {
                    int j = i + 2; int m = 0;
                    while (q[j] != 0)
                    { m = m * 10 + q[j] - 48; j++; }
                    i = j;
                    // MonitorRecord.RRCValue = m;
                    rrc = m;
                    fff++;
                }
            }
            if (fff > 0)
            {
                /* MonitorRecord.MonitorCode = "test75";
                 webser.SendRawData(MonitorRecord);
                 */
                int fa = mpdal.insertJianCeData(_MzjldId, hr, spo2, pulse, nibps, nibpd, nibpm, arts, artd, artm, etco2, ico2, rrc);

                if (fa != 1) { MessageBox.Show("错误"); }
            }

        }
        #endregion


        private void btnSave_Click(object sender, EventArgs e)
        {
            int i = SaveMzjld();
            if (i > 0) MessageBox.Show("麻醉记录单修改成功！");
            else MessageBox.Show("麻醉记录单修改失败！");

        }
        private void BindXueyaMaiboHuxi()
        {
            DataTable dt = mpdal.GetPoint(_MzjldId);
            if (dt.Rows.Count > 0)
            {
                //txtXueya.Text = Convert.ToString(dt.Rows[0]["NIBPS"])
                //                + " / " + Convert.ToString(dt.Rows[0]["NIBPD"]);
                //txtMaibo.Text = Convert.ToString(dt.Rows[0]["Pulse"]);
                //txtHuxi.Text = Convert.ToString(dt.Rows[0]["RRC"]);
            }
            else
                MessageBox.Show("暂无检测数据，请稍后再试！");
        }
        /// <summary>
        /// 机控呼吸赋值
        /// </summary>
        private void BindJikongTime()
        {
            DataTable dtMZ_Info = _MzjldDal.GetMzjldByMzjldId(_MzjldId);
            if (dtMZ_Info.Rows.Count > 0)
            {
                if (dtMZ_Info.Rows[0]["jkkssj"].ToString() != "")
                {
                    jkksTime = Convert.ToDateTime(dtMZ_Info.Rows[0]["jkkssj"]);
                }
                if (dtMZ_Info.Rows[0]["jkjssj"].ToString() != "")
                {
                    jkjsTime = Convert.ToDateTime(dtMZ_Info.Rows[0]["jkjssj"]);
                }
            }
        }
        /// <summary>
        /// 辅助呼吸赋值
        /// </summary>
        private void BindFuZhuTime()
        {
            DataTable dtMZ_Info = _MzjldDal.GetMzjldByMzjldId(_MzjldId);
            if (dtMZ_Info.Rows.Count > 0)
            {
                if (dtMZ_Info.Rows[0]["fzkssj"].ToString() != "")
                {
                    fzksTime = Convert.ToDateTime(dtMZ_Info.Rows[0]["fzkssj"]);
                }
                if (dtMZ_Info.Rows[0]["fzjssj"].ToString() != "")
                {
                    fzjsTime = Convert.ToDateTime(dtMZ_Info.Rows[0]["fzjssj"]);
                }
            }
        }
        /// <summary>
        /// 机控呼吸
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void jkhxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JKTimeSet f1 = new JKTimeSet(_MzjldId);
            f1.ShowDialog();
            BindJikongTime();
            BindJHDian();
            pictureBox3.Refresh();
            btnMonitor.Focus();
        }
        /// <summary>
        /// 辅助呼吸
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fzhxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FZHXTimeSet f1 = new FZHXTimeSet(_MzjldId);
            f1.ShowDialog();
            BindFuZhuTime();
            BindJHDian();
            pictureBox3.Refresh();
            btnMonitor.Focus();
        }
        private void DeleteCGBGStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = dal.Update_MZJLD_CGBGSJ(_MzjldId);
            if (i == 0) MessageBox.Show("选择修改不成功，请重试!");
            else
            {
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                LableCG.Visible = false;
                IsCg = false;
                LableCG.MouseDown -= new MouseEventHandler(lb_cguan_MouseDown);
                LableCG.MouseMove -= new MouseEventHandler(lb_cguan_MouseMove);
                LableCG.MouseUp -= new MouseEventHandler(lb_cguan_MouseUp);
                LableCG.MouseLeave -= new EventHandler(lb_cguan_MouseLeave);
                IsCg = false;
                IsBg = false;
                LableBG.Visible = false;
                LableBG.MouseDown -= new MouseEventHandler(lb_bguan_MouseDown);
                LableBG.MouseMove -= new MouseEventHandler(lb_bguan_MouseMove);
                LableBG.MouseUp -= new MouseEventHandler(lb_bguan_MouseUp);
                LableBG.MouseLeave -= new EventHandler(lb_bguan_MouseLeave);
                pictureBox3.Refresh();
            }
            listBoxSZSJ.Focus();
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

        private void txtSSYS_DoubleClick(object sender, EventArgs e)
        {

        }

        private void mzjldEdit_FormClosed(object sender, FormClosedEventArgs e)
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
        }
        private void tbChuLiang_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextValueLimit.Text_Value_Limit(sender, e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextValueLimit.Text_Value_Limit(sender, e);
        }
        private void btnEMR_Click(object sender, EventArgs e)
        {
            string IPaddress = "132.147.160.60";
            bool flag = TextValueLimit.PingHost(IPaddress, 1000);
            if (flag == true)
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = "D:\\ReadEMR\\mrequery.exe";
                proc.StartInfo.Arguments = patID;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();
            }
            else MessageBox.Show("电子病历 数据库未连接，请检查网络");
        }
        /// <summary>
        /// 术前访视
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBeforeVisit_Click(object sender, EventArgs e)
        {
            BeforeVisit_HQ f2 = new BeforeVisit_HQ(patID, odate.ToString("yyyy-MM-dd"));
            f2.ShowDialog();
        }

        /// <summary>
        /// 护理记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNurseRecord_Click(object sender, EventArgs e)
        {
            NurseRecord_HQ f1 = new NurseRecord_HQ(_MzjldId.ToString(), patID, odate.ToString("yyyy-MM-dd"));
            f1.ShowDialog();
        }
        /// <summary>
        /// 麻醉总结
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMZZJ_Click(object sender, EventArgs e)
        {
            AnesthesiaSummary f1 = new AnesthesiaSummary(_MzjldId.ToString(), patID, odate.ToString("yyyy-MM-dd"));
            f1.ShowDialog();
        }
        private void timerJKW_Tick(object sender, EventArgs e)
        {
            //ProceedreceivedData_JKW();
            ////JkwDataResolve();
            try
            {
                ProceedreceivedDate_BLT();
            }
            catch (Exception)
            {


            }
            //int s = Convert.ToInt32(cmbSJJG.Text);
            //timerBLT.Interval = s * 60 * 1000;
        }
        /// <summary>
        /// 宝莱特
        /// </summary>
        private void ProceedreceivedDate_BLT()
        {
            FileStream fs = new FileStream("c:\\BLTLog.txt", FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs);
            string str = sr.ReadLine();
            string values;
            int PDhr = 0;//判断HR的值        
            int ss;
            //if (str.Length==14)
            //{
            //    //pay = str.Substring(0,12);
            //    //dtpaytiem = Convert.ToDateTime(pay);
            //    dtpaytiem = DateTime.ParseExact(str, "yyyyMMddHHmmss", null);
            //}
            while (str != null)
            {
                str = sr.ReadLine();
                if (str == null)
                    continue;
                if (str.Contains("HR"))
                {
                    values = str.Replace("HR", "");
                    if (values.Trim() != "")
                    {
                        try
                        {
                            ss = int.Parse(values.Trim());
                            if (PDhr == 0 && Convert.ToInt32(values.Trim()) > 1)
                            {
                                HR = Convert.ToInt32(values.Trim());
                                PDhr++;
                            }

                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (str.Contains("SPO2"))
                {
                    values = str.Replace("SPO2", "");
                    if (values.Trim() != "")
                    {
                        try
                        {
                            ss = int.Parse(values.Trim());
                            SPO2 = Convert.ToInt32(values.Trim());
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (str.Contains("NIBP_SYS"))
                {
                    values = str.Replace("NIBP_SYS", "");
                    if (values.Trim() != "")
                    {
                        try
                        {
                            ss = int.Parse(values.Trim());
                            ABP_SYS = Convert.ToInt32(values.Trim());
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                else if (str.Contains("sSys P1"))
                {
                    values = str.Replace("sSys P1 ", "");
                    if (values.Trim() != "")
                    {
                        try
                        {
                            ss = int.Parse(values.Trim());
                            ABP_SYS = Convert.ToInt32(values.Trim()) - 200;
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                else if (str.Contains("sSys P2"))
                {
                    values = str.Replace("sSys P2", "");
                    if (values.Trim() != "")
                    {
                        try
                        {
                            ss = int.Parse(values.Trim());
                            ABP_SYS = Convert.ToInt32(values.Trim()) - 200;
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (str.Contains("NIBP_Mean"))
                {
                    values = str.Replace("NIBP_Mean", "");
                    if (values.Trim() != "")
                    {
                        try
                        {
                            ss = int.Parse(values.Trim());
                            ABP_MAP = Convert.ToInt32(values.Trim());
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                else if (str.Contains("sMean P1"))
                {
                    values = str.Replace("sMean P1", "");
                    if (values.Trim() != "")
                    {
                        try
                        {
                            ss = int.Parse(values.Trim());
                            ABP_MAP = Convert.ToInt32(values.Trim()) - 200;
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                else if (str.Contains("sMean P2"))
                {
                    values = str.Replace("sMean P2", "");
                    if (values.Trim() != "")
                    {
                        try
                        {
                            ss = int.Parse(values.Trim());
                            ABP_MAP = Convert.ToInt32(values.Trim()) - 200;
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (str.Contains("NIBP_DIA"))
                {
                    values = str.Replace("NIBP_DIA", "");
                    if (values.Trim() != "")
                    {
                        try
                        {
                            ss = int.Parse(values.Trim());
                            ABP_DIA = Convert.ToInt32(values.Trim());
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                else if (str.Contains("sDia P1"))
                {
                    values = str.Replace("sDia P1", "");
                    if (values.Trim() != "")
                    {
                        try
                        {
                            ss = int.Parse(values.Trim());
                            ABP_DIA = Convert.ToInt32(values.Trim()) - 200;
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                else if (str.Contains("sDia P2"))
                {
                    values = str.Replace("sDia P2", "");
                    if (values.Trim() != "")
                    {
                        try
                        {
                            ss = int.Parse(values.Trim());
                            ABP_DIA = Convert.ToInt32(values.Trim()) - 200;
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (str.Contains("EtCO2"))
                {
                    values = str.Replace("EtCO2", "");
                    if (values.Trim() != "")
                    {
                        try
                        {
                            ss = int.Parse(values.Trim());
                            if (ss > 0)
                            {
                                ETCO2 = Convert.ToInt32(ss * 0.76);
                            }

                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (str.Contains("PR"))
                {
                    values = str.Replace("PR", "");
                    if (values.Trim() != "")
                    {
                        try
                        {
                            ss = int.Parse(values.Trim());
                            PR = Convert.ToInt32(values.Trim());
                        }
                        catch (Exception)
                        {
                        }

                    }
                }
                if (str.Contains("RESP"))
                {
                    values = str.Replace("RESP", "");
                    if (values.Trim() != "")
                    {
                        try
                        {
                            ss = int.Parse(values.Trim());
                            RR = Convert.ToInt32(values.Trim());
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                if (str.Contains("T1"))
                {
                    values = str.Replace("T1", "");
                    if (values.Trim() != "")
                    {
                        try
                        {
                            float sss = float.Parse(values.Trim());
                            if (sss > 30)
                            {
                                TEMP1 = float.Parse(values.Trim());
                            }

                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                if (str.Contains("T2"))
                {
                    values = str.Replace("T2", "");
                    if (values.Trim() != "")
                    {
                        try
                        {
                            float sss = float.Parse(values.Trim());
                            if (sss > TEMP1 && sss > 30)
                            {
                                TEMP1 = float.Parse(values.Trim());
                            }

                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
            DateTime now = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
            //DateTime now = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
            if (mpdal.GetMzjldPoint(now, _MzjldId).Rows.Count == 0)
            {
                int fa = mpdal.insertJianCeDataMZJLD(_MzjldId, ABP_SYS, ABP_DIA, ABP_MAP, RR, HR, PR, SPO2, ETCO2, TEMP1, now);
            }
            sr.Close();
            fs.Close();
        }
        private void btnShuXuePG_Click(object sender, EventArgs e)
        {
            ShuXuePG f1 = new ShuXuePG(_MzjldId.ToString(), patID);
            f1.Show();
        }
        /// <summary>
        /// 术后访视
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            AfterVisit_HQ f2 = new AfterVisit_HQ(_MzjldId, patID);
            f2.ShowDialog();
        }
        /// <summary>
        /// 迈瑞时间控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerMR_Tick(object sender, EventArgs e)
        {
            if (isexist & SockectIsException)
            {
                if (TempSocket != null)
                    TempSocket.Close();
                if (ServerSocket != null)
                    ServerSocket.Close();
                if (SocketThread.ThreadState != ThreadState.Running)
                {
                    SocketThread.Abort();
                }
                isexist = false;
                SockectIsException = false;
            }
            if (!isexist)
            {
                SockectIsException = false;
                SocketThread = new Thread(Socket_Setup);
                SocketThread.IsBackground = true;
                SocketThread.Priority = ThreadPriority.AboveNormal;
                SocketThread.Start();
                isexist = true;

            }
            else
            {
                if (SocketThread.ThreadState != ThreadState.Running)
                {
                    //SocketThread.Abort();
                }
            }

        }
        /// <summary>
        /// 穿刺
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userMZXG_DoubleClick(object sender, EventArgs e)
        {
            Select_MZCC F1 = new Select_MZCC(userMZXG);
            F1.ShowDialog();
        }
        /// <summary>
        /// 区域阻滞
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQYZZ_DoubleClick(object sender, EventArgs e)
        {
            Select_QYZZ F1 = new Select_QYZZ(txtQYZZ);
            F1.ShowDialog();
        }
        private void ZoomMemu_Click(object sender, EventArgs e)
        {
            ZoomTimeSet f1 = new ZoomTimeSet(_MzjldId);
            f1.ShowDialog();
            BindZoomRegionList();
            BindJHDian();
            BindMZSSCGBG();
            BindZoomRegionListPrint(InRoomTime);
            pictureBox2.Refresh();
            pictureBox3.Refresh();
            pictureBox4.Refresh();
        }
        /// <summary>
        /// 出室情况
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userCSQK_DoubleClick(object sender, EventArgs e)
        {
            Select_CSQK F1 = new Select_CSQK(userCSQK);
            F1.ShowDialog();
        }
        /// <summary>
        /// 术后镇痛
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userSHZT_DoubleClick(object sender, EventArgs e)
        {
            Select_SHZT F1 = new Select_SHZT(userSHZT);
            F1.ShowDialog();
        }
        /// <summary>
        /// 药物
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMZyaowu_DoubleClick(object sender, EventArgs e)
        {
            Select_YW F1 = new Select_YW(txtMZyaowu);
            F1.ShowDialog();
        }

        private void btnSHZT_Click(object sender, EventArgs e)
        {
            AfterAnalgesia f1 = new AfterAnalgesia(_MzjldId.ToString(), patID, odate.ToString("yyyy-MM-dd"));
            f1.ShowDialog();
        }


    }
}

