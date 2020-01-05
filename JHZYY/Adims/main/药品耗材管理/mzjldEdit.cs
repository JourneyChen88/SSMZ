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
using System.Diagnostics;
using System.Text.RegularExpressions;

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
        adims_MODEL.shuye t_shuye = new adims_MODEL.shuye();//单个输液模板
        adims_MODEL.shuxue t_shuxue = new adims_MODEL.shuxue();//单个输血模板
        adims_MODEL.mzpingmian t_mzpm = new adims_MODEL.mzpingmian(); //单个麻醉平面模板
        adims_MODEL.mzyt t_ydy = new adims_MODEL.mzyt(); //单个诱导药模板
        adims_MODEL.jtytsx t_jmy = new adims_MODEL.jtytsx(); //单个局麻药模板
        adims_MODEL.tsyy t_tsyy = new adims_MODEL.tsyy();//单个特殊用药模板
        adims_MODEL.szsj t_szsj = new adims_MODEL.szsj();//单个术中事件模板
        adims_MODEL.clcxqt t_chuniao = new adims_MODEL.clcxqt();//单个出尿模板
        int p2x = 0, p2y = 0;//鼠标在picturebox2上的位置
        int p3x = 0, p3y = 0;//鼠标在picturebox3上的位置
        int flagP3 = 0, typeP3 = 0;
        int ssmzcgbgFlag = 0;//手术麻醉插管拔管鼠标按下是否选中标志 

        adims_MODEL.point t_point = new adims_MODEL.point();//单个point模板       

        private List<string> jhxmAll = new List<string>();//所有备选监护项目
        private List<string> jhxmIn = new List<string>();//已添加的监护项目
        public List<adims_MODEL.point> ssyList = new List<adims_MODEL.point>();//收缩点集合
        public List<adims_MODEL.point> szyList = new List<adims_MODEL.point>();//舒张压点集合
        public List<adims_MODEL.point> xlList = new List<adims_MODEL.point>();//心率点集合
        public List<adims_MODEL.point> twList = new List<adims_MODEL.point>();//体温点集合
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
            PatientIPAddress1.Text = IPAddressInput1;
            //BedNumber1.Text = BedIDInput1;
            phillip_server = new IPEndPoint(IPAddress.Parse(PatientIPAddress1.Text.Trim()), 24105);
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
            PatientIPAddress1.Text = IPAddressInput1;
            //BedNumber1.Text = BedIDInput1;
            phillip_server = new IPEndPoint(IPAddress.Parse(PatientIPAddress1.Text.Trim()), 24105);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Remote = new IPEndPoint(IPAddress.Any, 8001);
            phillip = (EndPoint)Remote;
        }

        public mzjldEdit(string patID)
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
        public mzjldEdit(string mzjldID, string patID)
        {
            this.mzjldID = Convert.ToInt32(mzjldID);
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
            string SQL = "otime='" + dtOtime.Value + "' WHERE id='" + mzjldID + "'";
            int i = dal.UpdateMzjld(SQL);
            if (i == 0) MessageBox.Show("入室时间修改失败！");
            else
            {
                otime = Convert.ToDateTime(dtOtime.Value.ToString("yyyy-MM-dd HH:mm"));
                fristopen = otime;
            }
        }
        private void cmbSJJG_SelectedIndexChanged(object sender, EventArgs e)
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

        private void BindShijiandian()
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

        private void BindMzjldBacicInfo()
        {
            DataTable dtMzjld = bll.selectmzjld(mzjldID);
            txtHeight.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["Height"]);
            txtWeight.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["Weight"]);
            txtSqyy.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["sqyy"]);
            txtTSBQing.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["tsbq"]);
            tbChuNiao.Text = Convert.ToString(dtMzjld.Rows[0]["niaoliang"]);
            tbChuxue.Text = Convert.ToString(dtMzjld.Rows[0]["chuxue"]);
            cmbTiwei.Text = Convert.ToString(dtMzjld.Rows[0]["tw"]);
            txtShoushuFS.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["ShoushuFS"]);
            txtMazuiFS.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["mazuiFS"]);
            txtNssss.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["nssss"]);
            txtSSYS.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["ssys"]);
            txtMZYS.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["mzys"]);
            txtQXHS.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["qxhs"]);
            txtXHHS.Controls[0].Text = Convert.ToString(dtMzjld.Rows[0]["xhhs"]);
            cmbMZXG.Text = Convert.ToString(dtMzjld.Rows[0]["mzxg"]);
            cmbASA.Text = Convert.ToString(dtMzjld.Rows[0]["ASA"]);
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

        private void BindMZSSCGBG()
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

        private void GetPatBasicInfo()
        {
            DataTable dt = bll.SelectPatInfo111(patID);
            txtpatID.Controls[0].Text = Convert.ToString(dt.Rows[0]["patid"]);
            txtName.Controls[0].Text = Convert.ToString(dt.Rows[0]["patname"]);
            txtAge.Controls[0].Text = Convert.ToString(dt.Rows[0]["patage"]);
            txtSex.Controls[0].Text = Convert.ToString(dt.Rows[0]["patsex"]);
            txtHeight.Controls[0].Text = Convert.ToString(dt.Rows[0]["PatHeight"]);
            txtWeight.Controls[0].Text = Convert.ToString(dt.Rows[0]["PatWeight"]);
            this.cmbXueXing.Text = Convert.ToString(dt.Rows[0]["PatBloodType"]);
            txtZhuYuanHao.Controls[0].Text = Convert.ToString(dt.Rows[0]["PatZhuYuanID"]);
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

        }
        private void cmbTiweiBind()
        {
            DataTable dt = dal.SelectData("tiwei");
            cmbTiwei.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                cmbTiwei.Items.Add(dr[1].ToString());
            }
        }
        private void cmbQiekouTypeBind()
        {
            DataTable dt = dal.SelectData("qiekoutype");
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
            this.dtOtime.CustomFormat = "MM-dd HH:mm";
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
                }
                else if (mzjldID != 0)
                {
                    dal.UpdateShoushujianinfo(1, mzjldID, patID, Oroom);//修改手术间状态信息
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
                    mzpmList[i].Id = Convert.ToInt32(dr["id"]);
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

        private void BindYdyList()
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

        private void BindZhenTongYao()
        {
            //listBox3.Items.Clear();
            //DataTable dt = bll.getMZZTY(mzjldID);
            //foreach (DataRow dr in dt.Rows)
            //{
            //    listBox3.Items.Add(dr["name"] + " " + dr["yl"] + dr["dw"]);
            //}
        }

        private void BindSZSJ()
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

        private void LOAD_jiekou()
        {
            //byte[] s = new byte[6];
            //s[0] = 0xA5;
            //s[1] = 0x02;
            //s[2] = 0x00;
            //s[3] = 0x57;
            //s[4] = 0xFE;
            //s[5] = 0X00;
            //serialPort1.Open();
            //serialPort1.Write(s, 0, 6);
            //timer2.Enabled = false;
        }

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
                if (cmbJianhuyi.Text == "金科威监护仪")
                {
                    timerJKW.Start();
                    JkwDataResolve();
                    cmbJianhuyi.Enabled = false;
                    cmbCOM.Enabled = false;
                }
                if (cmbJianhuyi.Text == "迈瑞监护仪")
                {
                    cmbJianhuyi.Enabled = false;
                    cmbCOM.Enabled = false;
                }
                SaveMzjld();
                ksjcTime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                btnMonitor.Text = "结束监测";
                
                timer1.Interval = 10 * 1000;
                timer1.Enabled = true;
            }
            #endregion
            else
            {
                if (DialogResult.OK == MessageBox.Show("确定结束检测吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                {
                    //timer2.Enabled = false;
                    timer4.Enabled = false;
                    timer1.Enabled = false;
                    timerJKW.Stop();
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



        #region //GE监护函数
        private void timer4_Tick(object sender, EventArgs e)
        {
            timer4.Interval = 1000 * 10;
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

        private void FillipData()
        {
            bool TimerStarted = false;
            string temp1 = PatientIPAddress1.Text;
            //string temp2 = this.txtMzjldid.Controls[0].Text;
            IPAddressInput1 = PatientIPAddress1.Text;
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
                    this.SaveFillipData(PatientData, mzjldID, 0);

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
            if (dal.selectJianCeData(now, mzjldID, type).Rows.Count == 0)
            {
                int fa = 0;
                if (ABP_SYS1 != 0 && ABP_DIA1 != 0 && ABP_MAP1 != 0)
                {
                    if (type == 0)
                        fa = dal.insertJianCeDataMZJLD(mzjldID, ABP_SYS1, ABP_DIA1, ABP_MAP1, RR1, HR1, PR1, SPO21, new Random().Next(32, 35), TEMP1, now);
                    if (type == 1)
                        fa = dal.insertJianCeDataPACU(mzjldID, ABP_SYS1, ABP_DIA1, ABP_MAP1, RR1, HR1, PR1, SPO21, new Random().Next(32, 35), TEMP1, now);
                }
                else
                {
                    if (type == 0)
                        fa = dal.insertJianCeDataMZJLD(mzjldID, SYS1, DIA1, MAP1, RR1, HR1, PR1, SPO21, new Random().Next(32, 35), TEMP1, now);
                    if (type == 1)
                        fa = dal.insertJianCeDataPACU(mzjldID, SYS1, DIA1, MAP1, RR1, HR1, PR1, SPO21, new Random().Next(32, 35), TEMP1, now);
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
            string temp1 = PatientIPAddress1.Text;
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
        public void ProceedreceivedData_JKW(string tempStr)
        {
            CON++;
            SaveLog(CON.ToString()+tempStr);
            int type = 0;
            Match m = r1.Match(tempStr);
            Match m1 = r2.Match(tempStr);            
            while (m.Success && m1.Success)
            {
                string[] strValue = new string[100];
                string jiexiStr = "";
                jiexiStr = tempStr.Substring(m.Index, m1.Index + 1);
                SaveLog(jiexiStr);
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
                    if (HexToInt(strValue[2])!=0)
                        SYS = HexToInt(strValue[2]);
                    if (HexToInt(strValue[3]) != 0)
                        DIA = HexToInt(strValue[3]);
                    if (HexToInt(strValue[4]) != 0)
                        MAP = HexToInt(strValue[4]);
                    TEMP1 = float.Parse(HexToInt(strValue[5]) + "." + HexToInt(strValue[6]));
                    if (HexToInt(strValue[7]) != 0)
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
                    if ( HexToInt(strValue[1])!=0)
                        PR = HexToInt(strValue[1]);
                    if (HexToInt(strValue[2]) != 0)
                        SPO2 = HexToInt(strValue[2]);
                    //label29.Text = "SPO2为：" + SPO2.ToString();
                }
                else if (jiexiStr.Contains("AA-40"))
                {
                    jiexiStr = jiexiStr.Replace("AA-40", "");
                    jiexiStr = jiexiStr.Replace("-", "");
                    int lenth = jiexiStr.Length / 2;
                    for (int i = 0; i < lenth - 1; i++)
                    {
                        strValue[i] = jiexiStr.Substring(i * 2, 2);
                    }
                    if (HexToInt(strValue[1]) != 0)
                        RR = HexToInt(strValue[1]);
                    //label28.Text = "RR为：" + RR.ToString();
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
                    if (HexToInt(strValue[1]) != 0)
                        RR = HexToInt(strValue[1]);
                    if (HexToInt(strValue[2])!=0)
                        ETCO2 = HexToInt(strValue[2]);
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
                if (dal.selectJianCeData(now, mzjldID, type).Rows.Count == 0)
                {
                    if (ETCO2 == 0)
                        ETCO2 = new Random().Next(32, 35);
                    int fa = 0;
                    if (ABP_SYS != 0 && ABP_DIA != 0 && ABP_MAP != 0)
                    {
                        if (type == 0)
                            fa = dal.insertJianCeDataMZJLD(mzjldID, ABP_SYS, ABP_DIA, ABP_MAP, RR, HR, PR, SPO2, ETCO2, TEMP1, now);
                        if (type == 1)
                            fa = dal.insertJianCeDataPACU(mzjldID, ABP_SYS, ABP_DIA, ABP_MAP, RR, HR, PR, SPO2, ETCO2, TEMP1, now);
                    }
                    else
                    {
                        if (type == 0)
                            fa = dal.insertJianCeDataMZJLD(mzjldID, SYS, DIA, MAP, RR, HR, PR, SPO2, new Random().Next(32, 35), TEMP1, now);
                        if (type == 1)
                            fa = dal.insertJianCeDataPACU(mzjldID, SYS, DIA, MAP, RR, HR, PR, SPO2, new Random().Next(32, 35), TEMP1, now);
                    }
                }

            }
            
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
            int countN = dal.CopyData(mzjldID, ksjcTime);
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
            }

        }

        public void BindJHDian()//监护点赋值
        {
            ssyList.Clear(); szyList.Clear(); xlList.Clear(); twList.Clear(); hxlList.Clear();
            mboList.Clear(); spo2List.Clear(); etco2List.Clear(); jhxmValue.Clear();

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
            //for (int i = 0; i < datadt.Rows.Count; i++)
            //{
            //    adims_MODEL.point p5 = new adims_MODEL.point();//呼吸率记录点
            //    p5.D = Convert.ToDateTime(datadt.Rows[i][0]);
            //    p5.V = Convert.ToInt32(datadt.Rows[i][5]);
            //    p5.Lx = 5;
            //    tw.Add(p5);
            //}
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.point p6 = new adims_MODEL.point();//ETCO2记录点
                p6.D = Convert.ToDateTime(datadt.Rows[i][0]);
                p6.V = Convert.ToInt32(datadt.Rows[i]["ETCO2"]);
                p6.Lx = 6;
                etco2List.Add(p6);
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
            //for (int i = 0; i < datadt.Rows.Count; i++)
            //{
            //    adims_MODEL.jhxm jhxmt = new adims_MODEL.jhxm();
            //    jhxmt.D = Convert.ToDateTime(datadt.Rows[i][0]);
            //    jhxmt.V = Convert.ToInt32(datadt.Rows[i]["CVP"]);
            //    jhxmt.Sy = "CVP";
            //    jhxmv.Add(jhxmt);
            //}
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

        private void btnSzsj_Click(object sender, EventArgs e)
        {
            addSzsj F1 = new addSzsj(mzjldID, 0);
            F1.ShowDialog();
            BindSZSJ();
            pictureBox3.Refresh();
            listBox1.Focus();
        }

        private void btnTsyy_Click(object sender, EventArgs e)
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
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            //printDocument1.DefaultPageSettings.PaperSize =
            //           new System.Drawing.Printing.PaperSize("K16", 740, 1020);  
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("A4", 820, 1160);

        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = 10, y = 0;//整体位置控制
            string title = "        昌 吉 州 人 民 医 院 麻 醉 记 录 单";//标题
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
            Font ptzt8 = new Font("宋体", 8);//填入栏目字体
            Font ht7 = new Font("黑体", 7);//填入栏目字体
            Font ptzt7 = new Font("宋体", 7);//填入栏目字体           
            Font ptzt7_U = new Font("宋体", 7, FontStyle.Underline, GraphicsUnit.Point);//下划线字体
            Font ptzt6 = new Font("宋体", 6);//填入栏目字体
            Font ptzt5 = new Font("宋体", 5);//填入栏目字体
            Pen pred2 = new Pen(Brushes.Red, 2);
            Pen pblue2 = new Pen(Brushes.Blue, 2);
            e.Graphics.DrawString(title, ptzt13, Brushes.Black, new Point(120 + x, 20 + y));
            int Y_unLine = y + 63, YY = y + 50;
            e.Graphics.DrawString("科别：" + txtBingQu.Controls[0].Text, ptzt8, Brushes.Black, new Point(80 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(110 + x, Y_unLine), new Point(250 + x, Y_unLine));
            e.Graphics.DrawString("床号：" + txtBednumber.Controls[0].Text, ptzt8, Brushes.Black, new Point(300 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(330 + x, Y_unLine), new Point(400 + x, Y_unLine));
            e.Graphics.DrawString("病历号：" + txtZhuYuanHao.Controls[0].Text, ptzt8, Brushes.Black, new Point(450 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(500 + x, Y_unLine), new Point(650 + x, Y_unLine));
            //e.Graphics.DrawString("入室时间：" + dtOtime.Value.ToString("HH:mm"), ptzt8, Brushes.Black, new Point(630 + x, YY));
            //e.Graphics.DrawLine(ptp, new Point(680 + x, Y_unLine), new Point(730 + x, Y_unLine));

            //↑画标题一块的东西
            Y_unLine = YY + 15; YY = YY + 20;
            e.Graphics.DrawLine(pblack2, new Point(20 + x, 70 + y), new Point(780 + x, 70 + y));
            e.Graphics.DrawLine(pblack2, new Point(20 + x, 70 + y), new Point(20 + x, 1100 + y));
            e.Graphics.DrawLine(pblack2, new Point(780 + x, 70 + y), new Point(780 + x, 1100 + y));
            e.Graphics.DrawLine(pblack2, new Point(20 + x, 1100 + y), new Point(780 + x, 1100 + y));
            //↑画边框

            #region 打印病人基本信息
            YY = YY + 5; Y_unLine = YY + 13;
            e.Graphics.DrawString("姓名  " + txtName.Controls[0].Text, ptzt8, Brushes.Black, new Point(25 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(50 + x, Y_unLine), new Point(150 + x, Y_unLine));
            e.Graphics.DrawString("性别  " + txtSex.Controls[0].Text, ptzt8, Brushes.Black, new Point(160 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(185 + x, Y_unLine), new Point(220 + x, Y_unLine));
            e.Graphics.DrawString("年龄  " + txtAge.Controls[0].Text, ptzt8, Brushes.Black, new Point(230 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(255 + x, Y_unLine), new Point(290 + x, Y_unLine));
            e.Graphics.DrawString("身高  " + txtHeight.Controls[0].Text, ptzt8, Brushes.Black, new Point(300 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(325 + x, Y_unLine), new Point(360 + x, Y_unLine));
            e.Graphics.DrawString("cm   体重  " + txtWeight.Controls[0].Text, ptzt8, Brushes.Black, new Point(360 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(420 + x, Y_unLine), new Point(460 + x, Y_unLine));
            e.Graphics.DrawString("kg       手术日期：" + dtOdate.Text, ptzt8, Brushes.Black, new Point(460 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(570 + x, Y_unLine), new Point(670 + x, Y_unLine));

            YY = YY + 20; Y_unLine = YY + 13;
            e.Graphics.DrawString("ASA分级  " + cmbASA.Text, ptzt8, Brushes.Black, new Point(25 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(65 + x, Y_unLine), new Point(150 + x, Y_unLine));

            e.Graphics.DrawRectangle(ptp, 160 + x, YY, 12, 12);
            e.Graphics.DrawRectangle(ptp, 220 + x, YY, 12, 12);
            e.Graphics.DrawString("急诊", ptzt8, Brushes.Black, new Point(180 + x, YY));
            e.Graphics.DrawString("择期", ptzt8, Brushes.Black, new Point(240 + x, YY));
            if (cbJizhen.Checked)
                e.Graphics.DrawLines(pblue2, new Point[] { new Point(155 + x, YY), new Point(165 + x, YY + 12), new Point(180 + x, YY) });
            if (cbZeqi.Checked)
                e.Graphics.DrawLines(pblue2, new Point[] { new Point(215 + x, YY), new Point(225 + x, YY + 12), new Point(240 + x, YY) });

            e.Graphics.DrawString("术前禁食  " + this.cmbSQJinshi.Text, ptzt8, Brushes.Black, new Point(300 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(350 + x, Y_unLine), new Point(400 + x, Y_unLine));
            e.Graphics.DrawRectangle(ptp, 510 + x, YY - 3, 250, 78);

            e.Graphics.DrawString("术前特殊情况：", ptzt8, Brushes.Black, new Point(520 + x, YY));
            if (string.IsNullOrEmpty(txtTSBQing.Controls[0].Text.Trim()))
                txtTSBQing.Controls[0].Text = " 无";
            string str = "";
            int StrLength = txtTSBQing.Controls[0].Text.Trim().Length;
            int row = StrLength / 20;
            for (int i = 0; i <= StrLength / 20; i++)//25个字符就换行
            {
                if (i < row)
                    str += txtTSBQing.Controls[0].Text.Trim().Substring(i * 20, 20) + Environment.NewLine; //从第i*20个开始，截取20个字符串
                else
                    str += txtTSBQing.Controls[0].Text.Trim().Substring(i * 20);
            }
            e.Graphics.DrawString(str, ptzt8, Brushes.Black, new Point(515 + x, YY + 15));


            YY = YY + 20; Y_unLine = YY + 13;
            e.Graphics.DrawString("术前诊断 " + txtSqzd.Controls[0].Text, ptzt8, Brushes.Black, new Point(25 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(70 + x, Y_unLine), new Point(500 + x, Y_unLine));

            YY = YY + 20; Y_unLine = YY + 13;
            e.Graphics.DrawString("拟施手术 " + txtNssss.Controls[0].Text, ptzt8, Brushes.Black, new Point(25 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(70 + x, Y_unLine), new Point(500 + x, Y_unLine));

            YY = YY + 20; Y_unLine = YY + 13;
            if (string.IsNullOrEmpty(txtSqyy.Controls[0].Text.Trim()))
                txtSqyy.Controls[0].Text = "  无";
            e.Graphics.DrawString("麻醉前用药 " + txtSqyy.Controls[0].Text, ptzt8, Brushes.Black, new Point(25 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(80 + x, Y_unLine), new Point(300 + x, Y_unLine));

            e.Graphics.DrawString("手术体位 " + this.cmbTiwei.Text, ptzt8, Brushes.Black, new Point(310 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(360 + x, Y_unLine), new Point(500 + x, Y_unLine));
            
            YY = YY + 20; Y_unLine = YY + 13;
            e.Graphics.DrawLine(ptp, new Point(20 + x, YY), new Point(780 + x, YY));
            //↑画边框
            #endregion

            #region 打印时间和分页
            DateTime dtnow = new DateTime();//打印截止时间判断        
            DateTime pagetime = new DateTime();
            DataTable dtMax = bll.GetMaxPoint(mzjldID);
            if (dtMax.Rows[0][0].ToString() == "")
                dtnow = DateTime.Now;
            else
                dtnow = Convert.ToDateTime(dtMax.Rows[0][0]);
            pagetime = ptime; //当前打印页时间           
            e.Graphics.DrawString("时间(分钟)", ptzt8, Brushes.Black, new Point(30 + x, YY + 2));
            for (int i = 0; i < 10; i++) //打印检测时间
            {
                e.Graphics.DrawString(ptime.ToString("HH:mm"), ptzt8, Brushes.Black, new Point(110 + 66 * i + x, YY + 2));
                ptime = ptime.AddMinutes(6 * jcsjjg);
            }
            if (ptime < dtnow)
            {
                e.HasMorePages = true;
                e.Graphics.DrawString("第 " + iYema.ToString() + " 页", ptzt8, Brushes.Black, new Point(340 + x, 1105 + y));
                iYema++;
            }
            else
            {
                e.HasMorePages = false; ptime = fristopen;
                e.Graphics.DrawString("第 " + iYema.ToString() + " 页", ptzt8, Brushes.Black, new Point(340 + x, 1105 + y));
            }
            #endregion

            #region  打印用药区域
            YY = YY + 18;
            for (int i = 0; i < 37; i++)//画横实线
            {
                if (i == 0 || i == 20 || i == 22 || i == 30 || i == 34 || i == 36)
                    e.Graphics.DrawLine(ptp, new Point(20 + x, YY + 12 * i + y), new Point(780 + x, YY + 12 * i + y));
                //else if (i == 33)
                //    e.Graphics.DrawLine(ptp, new Point(35 + x, YY + 12 * i + y), new Point(700 + x, YY + 12 * i + y));
                else
                    e.Graphics.DrawLine(ptp, new Point(35 + x, YY + 12 * i + y), new Point(780 + x, YY + 12 * i + y));
            }
            e.Graphics.DrawLine(ptp, new Point(35 + x, YY + y), new Point(35 + x, YY + 36 * 12 + y));

            for (int i = 0; i < 10; i++)//画竖实线
            {
                e.Graphics.DrawLine(ptp, new Point(120 + x + 66 * i, YY - 2 + y), new Point(120 + x + 66 * i, YY + 36 * 12 + y));
            }
            for (int i = 1; i < 30; i++)//画竖虚线
            {
                e.Graphics.DrawLine(pxuxian, new Point(120 + x + 22 * i, YY + y), new Point(120 + x + 22 * i, YY + 36 * 12 + y));

            }
            e.Graphics.DrawString("全\n\n麻\n\n药", ptzt7, Brushes.Black, new Point(22 + x, YY + 42));
            e.Graphics.DrawString("局\n麻", ptzt7, Brushes.Black, new Point(22 + x, YY + 12 * 20 + 2));
            e.Graphics.DrawString("\n液\n\n体", ptzt7, Brushes.Black, new Point(22 + x, YY + 12 * 22 + 2));
            e.Graphics.DrawString("\n输\n\n血", ptzt7, Brushes.Black, new Point(22 + x, YY + 12 * 30 + 2));
            e.Graphics.DrawString("SP02(%)", ptzt7, Brushes.Black, new Point(40 + x, YY + 12 * 34 + 2));
            e.Graphics.DrawString("02 (吸入)", ptzt7, Brushes.Black, new Point(40 + x, YY + 12 * 35 + 2));
            #endregion

            #region 打印全麻药
            ArrayList sssYDY = new ArrayList();
            int ydyi = 0;
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
                    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120);
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
                    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120);
                    int y1 = YY + ydyi * 12 + 4;
                    int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 11 / jcsjjg + 120);
                    if (x1 > 120 + x && x1 < 780 + x)
                    {
                        e.Graphics.DrawString(mzyt.Yl.ToString(), ptzt7, Brushes.Blue, new Point(x1 + 3 + x, y + y1 - 6));
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), new Point(x1 - 3 + x, y1 + 6 + y), new Point(x1 + 3 + x, y1 + 6 + y) });
                    }
                    if (x2 > 120 + x && x2 < 780 + x)
                    {
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2 + x, y1 + y), new Point(x2 - 3 + x, y1 + 6 + y), new Point(x2 + 3 + x, y1 + 6 + y) });
                    }
                    if (x1 > 120 + x && x1 < 780 + x && x2 > 120 + x && x2 < 780 + x)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x1 + x, y1 + y + 3), new Point(x2 + x, y1 + y + 3));
                    }
                    if (x1 > 120 + x && x1 < 780 + x && x2 > 780 + x)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x1 + x, y1 + y + 3), new Point(780 + x, y1 + y + 3));
                    }
                    if (x1 < 120 + x && x2 > 120 + x && x2 < 780 + x)
                    {
                        e.Graphics.DrawLine(pred2, new Point(120 + x, y1 + y + 3), new Point(x2 + x, y1 + y + 3));
                    }
                    if (x1 < 120 + x && x2 > 780 + x)
                    {
                        e.Graphics.DrawLine(pred2, new Point(120 + x, y1 + y + 3), new Point(780 + x, y1 + y + 3));
                    }
                }
                ydyi++;
                sssYDY.Add(mzyt.Ytname);
            }
            #endregion

            #region 打印局麻药
            int jti = 20;  //打印局麻药
            ArrayList sssJMY = new ArrayList();
            foreach (adims_MODEL.jtytsx jt in jmyList)
            {
                if (sssJMY.Contains(jt.Name))
                    jti = jti - 1;
                if (!sssJMY.Contains(jt.Name))
                    e.Graphics.DrawString(jt.Name + " " + jt.Dw, jt.Name.Length < 7 ? ptzt7 : ptzt5, Brushes.Black, new PointF(37 + x, YY + jti * 12 + y + 2));

                TimeSpan t = new TimeSpan();
                t = jt.Kssj - pagetime;
                int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120);
                int y1 = YY + jti * 12 + 4;
                if (x1 > 120 + x && x1 < 780 + x)
                {
                    e.Graphics.DrawString(jt.Jl.ToString(), ptzt7, Brushes.Blue, new Point(x1 + 3 + x, y + y1 - 6));
                    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), new Point(x1 - 3 + x, y1 + 6 + y), new Point(x1 + 3 + x, y1 + 6 + y) });
                }
                jti++;
                sssJMY.Add(jt.Name);
            }
            #endregion

            #region 打印输液
            int syi = 22;
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
                int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120);
                int y1 = YY + syi * 12 + 5;
                int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 11 / jcsjjg + 120);

                if (x1 > 120 + x && x1 < 780 + x)
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
            int sxi = 30;
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
                int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120);
                int y1 = YY + sxi * 12 + 5;
                int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 11 / jcsjjg + 120);
                if (x1 > 120 + x && x1 < 780 + x)
                {
                    e.Graphics.DrawString(sx.Jl.ToString(), ptzt7, Brushes.Blue, x1 + x, y + y1 - 6);
                    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1 + x, y1 + y), 
                        new Point(x1 - 3 + x, y1 + 5 + y), new Point(x1 + 3 + x, y1 + 5 + y) });
                }
                sxi++;
                sssSX.Add(sx.Name);
            }
            #endregion

            #region 打印SPO2

            int jhi = 34; int spo2Count = 0;
            foreach (adims_MODEL.jhxm j in jhxmValue)
            {
                if (j.Sy == "SpO2" && j.V != 0)
                {
                    if (j.D >= pagetime && j.D <= pagetime.AddMinutes(60 * jcsjjg))
                    {
                        TimeSpan t = new TimeSpan();
                        t = j.D - pagetime;
                        float jhx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 115);
                        float jhy = YY + jhi * 12 + y + 2;
                        if (spo2Count % 2 == 0)
                        {
                            e.Graphics.FillRectangle(Brushes.Pink, jhx - 3 + x, jhy, 9, 8);
                            e.Graphics.DrawString(j.V.ToString(), j.V > 99 ? ptzt5 : ptzt7, Brushes.Black, new PointF(jhx - 4 + x, jhy));
                        }
                    }
                }
                spo2Count++;
            }

            #endregion

            #region 打印出血出尿
            Pen dakred2 = new Pen(Brushes.DarkRed, 2);
            if (tbChuNiao.Text.Trim() == "")
                tbChuNiao.Text = " 0";
            int yyyy = YY + 32 * 12 + 1;
            e.Graphics.DrawString("出尿：" + tbChuNiao.Text, ht7, Brushes.DarkRed, new Point(700 + x, yyyy));
            e.Graphics.DrawLine(dakred2, new Point(725 + x, yyyy + 11), new Point(766 + x, yyyy + 11));
            e.Graphics.DrawString("ml", ht7, Brushes.Black, new Point(765 + x, yyyy));
            yyyy = yyyy + 12;
            if (tbChuxue.Text.Trim() == "")
                tbChuxue.Text = " 0";
            e.Graphics.DrawString("出血：" + tbChuxue.Text, ht7, Brushes.DarkRed, new Point(700 + x, yyyy));
            e.Graphics.DrawLine(dakred2, new Point(725 + x, yyyy + 11), new Point(766 + x, yyyy + 11));
            e.Graphics.DrawString("ml", ht7, Brushes.Black, new Point(765 + x, yyyy));

            #endregion

            #region 打印气体
            ArrayList sssQT = new ArrayList();
            int qti = 35;   //起步位置
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
                    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120) + x;
                    int y1 = YY + qti * 12 + 4;
                    int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 11 / jcsjjg + 120) + x;
                    //double qtzongliang = (mzqt.Yl) * Convert.ToDouble((x2 - x1) / 2.5);
                    //e.Graphics.DrawString(Convert.ToInt32(qtzongliang).ToString() + "L", this.Font, Brushes.Blue, new Point(763, y1 - 2));
                    if (x1 > 120 + x && x1 < 780 + x)
                    {
                        e.Graphics.DrawString(mzqt.Yl.ToString() + " " + mzqt.Dw, ptzt6, Brushes.Blue, new Point(x1 + 3, y + y1 - 6));
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1 + y), new Point(x1 - 3, y1 + 6 + y), new Point(x1 + 3, y1 + 6 + y) });
                    }
                    if (x2 > 120 + x && x2 < 780 + x)
                    {
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1 + y), new Point(x2 - 3, y1 + 6 + y), new Point(x2 + 3, y1 + 6 + y) });
                    }
                    if (x1 > 120 + x && x1 < 780 + x && x2 > 120 + x && x2 < 780 + x)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x1, y1 + y + 3), new Point(x2, y1 + y + 3));
                    }
                    if (x1 > 120 + x && x1 < 780 + x && x2 > 780 + x)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x1, y1 + y + 3), new Point(780 + x, y1 + y + 3));
                    }
                    if (x1 < 120 + x && x2 > 120 + x && x2 < 780 + x)
                    {
                        e.Graphics.DrawLine(pred2, new Point(120 + x, y1 + y + 3), new Point(x2, y1 + y + 3));
                    }
                    if (x1 < 120 + x && x2 > 780 + x)
                    {
                        e.Graphics.DrawLine(pred2, new Point(120 + x, y1 + y + 3), new Point(780 + x, y1 + y + 3));
                    }
                }
                qti++;
                sssQT.Add(mzqt.Qtname);
            }

            #endregion

            #region//打印检测区域格子。血压体温等区域↓
            YY = YY + 12 * 36;
            e.Graphics.DrawLine(ptp, new Point(35 + x, YY), new Point(35 + x, YY + 240));
            e.Graphics.DrawString("术\n\n中\n\n监\n\n测", ptzt8, Brushes.Black, new Point(21 + x, YY + 60));

            for (int i = 0; i < 12; i++)//画横实线           
                e.Graphics.DrawLine(ptp, new Point(120 + x, YY + 20 * i), new Point(780 + x, YY + 20 * i + y));

            for (int i = 0; i < 12; i++)//画横虚线
                e.Graphics.DrawLine(pxuxian, 120 + x, YY + 20 * i + 10, 780 + x, YY + 20 * i + y + 10);

            for (int i = 0; i < 10; i++)//画竖实线
            {
                e.Graphics.DrawLine(ptp, new Point(120 + 66 * i + x, YY + y), new Point(120 + 66 * i + x, YY + 12 * 20 + y));
            }
            for (int i = 1; i < 30; i++)//画竖虚线
            {
                e.Graphics.DrawLine(pxuxian, new Point(120 + 22 * i + x, YY + y), new Point(120 + 22 * i + x, YY + 12 * 20 + y));
            }
            for (int i = 1; i < 12; i++)
                e.Graphics.DrawString((240 - i * 20).ToString(), ptzt7, Brushes.Black, new PointF(95 + x, YY + (float)20 * i + y - 5));
            for (int i = 1; i < 12; i++)
                e.Graphics.DrawString((42 - i * 2).ToString(), ptzt7, Brushes.Black, new PointF(780 + x, YY + (float)20 * i + y - 5));

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
            e.Graphics.DrawString("△体  温", ptzt7, Brushes.DarkRed, new Point(40 + x, YY + 170));
            e.Graphics.DrawString("▽ ETCO2", ptzt7, Brushes.DarkOrange, new Point(40 + x, YY + 185));
            #endregion

            #region  //打印收缩压
            float px = 0, py = 0;
            Pen p_red2 = new Pen(Brushes.Red, 2);
            foreach (adims_MODEL.point p in ssyList)
            {
                if (p.D >= pagetime && p.D <= pagetime.AddMinutes(60 * jcsjjg))
                {
                    TimeSpan t = new TimeSpan();
                    t = p.D - pagetime;
                    float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120);
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
            #endregion

            #region  //打印舒张压
            px = 0; py = 0;
            foreach (adims_MODEL.point p in szyList)
            {
                if (p.D >= pagetime && p.D <= pagetime.AddMinutes(60 * jcsjjg))
                {
                    TimeSpan t = new TimeSpan();
                    t = p.D - pagetime;
                    float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120);
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
            #endregion

            #region  //打印脉搏
            px = 0; py = 0;
            foreach (adims_MODEL.point p in mboList)
            {
                if (p.D >= pagetime && p.D <= pagetime.AddMinutes(60 * jcsjjg))
                {
                    TimeSpan t = new TimeSpan();
                    t = p.D - pagetime;
                    float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120);
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
            px = 0; py = 0;
            foreach (adims_MODEL.point p in twList)
            {
                if (p.D >= pagetime && p.D <= pagetime.AddMinutes(60 * jcsjjg))
                {
                    TimeSpan t = new TimeSpan();
                    t = p.D - pagetime;
                    float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120);
                    //float pointy = (float)((220 - p.V) * 1 + 460);
                    float pointy = 0;
                    if (p.V > 230)
                    {
                        pointy = (float)((20) * 1 + YY);
                        e.Graphics.DrawString(p.V.ToString(), ptzt7, Brushes.Blue, pointx + x, pointy + y);

                    }
                    else
                        pointy = (float)((240 - p.V) * 1 + YY);
                    e.Graphics.DrawPolygon(Pens.Maroon, new PointF[3] { new PointF(pointx - 3 + x, pointy + 5 + y), new PointF(pointx + x, pointy + y), new PointF(pointx + 3 + x, pointy + 5 + y) });
                    //e.Graphics.FillEllipse(Pens.Maroon, pointx + x - 2, pointy + y - 2, 5, 5);
                    if (px != 0)
                        e.Graphics.DrawLine(Pens.Maroon, new PointF(px + x, py + y), new PointF(pointx + x, pointy + y));

                    px = pointx;
                    py = pointy;
                }
            }
            #endregion

            #region  //打印呼吸
            px = 0; py = 0; int phuxi = 0;
            foreach (adims_MODEL.point p in hxlList)
            {
                if (p.D >= pagetime && p.D <= pagetime.AddMinutes(60 * jcsjjg))
                {
                    TimeSpan t = new TimeSpan();
                    t = p.D - pagetime;
                    float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120) + x;
                    float pointy = 0;
                    if (p.V > 230)
                    {
                        pointy = (float)((20) * 1 + YY) + y;
                        e.Graphics.DrawString(p.V.ToString(), ptzt7, Brushes.Blue, pointx, pointy);
                    }
                    else
                        pointy = (float)((240 - p.V) * 1 + YY) + y;
                    if (jkksTime < p.D && p.D < jkjsTime)
                    {
                        float xPlus = pointx - px; // 描点作图
                        PointF p1 = new PointF(px,py);
                        PointF p2 = new PointF(pointx - 2 * xPlus / 3, pointy + 6);
                        PointF p3 = new PointF(pointx - xPlus / 3, pointy - 6);
                        PointF p4 = new PointF(pointx, pointy);
                        e.Graphics.DrawBezier(Pens.DarkCyan, p1, p2, p3, p4);
                        
                    }
                    else
                        e.Graphics.DrawEllipse(Pens.DarkCyan, pointx - 2, pointy - 2, 5, 5);
                    if (px != 0 && (jkksTime > p.D || jkjsTime < p.D))
                        e.Graphics.DrawLine(Pens.DarkCyan, new PointF(px, py), new PointF(pointx, pointy));
                    px = pointx;
                    py = pointy;
                }
            }
            #endregion

            #region  //打印ETCO2
            px = 0; py = 0;
            foreach (adims_MODEL.point p in etco2List)
            {
                if (p.D >= pagetime && p.D <= pagetime.AddMinutes(60 * jcsjjg))
                {
                    TimeSpan t = new TimeSpan();
                    t = p.D - pagetime;
                    float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120);
                    float pointy = 0;
                    if (p.V > 230)
                    {
                        pointy = (float)((20) * 1 + YY);
                        e.Graphics.DrawString(p.V.ToString(), ptzt7, Brushes.Blue, pointx + x, pointy + y);
                    }
                    else
                        pointy = (float)((240 - p.V) * 1 + YY);

                    e.Graphics.DrawPolygon(Pens.DarkOrange, new PointF[3] { new PointF(pointx+ x, pointy+ y), 
                                       new PointF(pointx - 3+ x, pointy - 5+ y), new PointF(pointx + 3+ x, pointy - 5+ y) });
                    // e.Graphics.FillPolygon(Brushes.Green, new PointF[3] { new PointF(pointx+ x, pointy+ y), 
                    //         new PointF(pointx + 3+ x, pointy + 6+ y), new PointF(pointx - 3+ x, pointy + 6+ y) });

                    if (px != 0)
                        e.Graphics.DrawLine(Pens.DarkOrange, new PointF(px + x, py + y), new PointF(pointx + x, pointy + y));

                    px = pointx;
                    py = pointy;
                }

            }
            #endregion

            //↓标记区域
            YY = YY + 12 * 20;
            e.Graphics.DrawLine(ptp, new Point(20 + x, YY), new Point(780 + x, YY));
            e.Graphics.DrawString("标记", ptzt7, Brushes.Black, new Point(25 + x, YY + 3));

            #region 打印麻醉，手术，插管
            if (ssksTime >= pagetime && ssksTime <= pagetime.AddHours(jcsjjg))
            {
                TimeSpan T = ssksTime - pagetime;
                int xxx = (int)((T.Days * 24 * 60 + T.Hours * 60 + T.Minutes) * 11 / jcsjjg + 115 + x);
                e.Graphics.DrawString("⊙", ptzt8, Brushes.Black, xxx, YY + 3);
            }
            if (ssjsTime >= pagetime && ssjsTime <= pagetime.AddHours(jcsjjg))
            {
                TimeSpan T = ssjsTime - pagetime;
                int xxx = (int)((T.Days * 24 * 60 + T.Hours * 60 + T.Minutes) * 11 / jcsjjg + 115 + x);
                Image newImage = Properties.Resources.SSJS;
                e.Graphics.DrawImage(newImage,new Rectangle( xxx, YY + 3,10,10));
            }
            if (mzksTime >= pagetime && mzksTime <= pagetime.AddHours(jcsjjg))
            {
                TimeSpan T = mzksTime - pagetime;
                int xxx = (int)((T.Days * 24 * 60 + T.Hours * 60 + T.Minutes) * 11 / jcsjjg + 115 + x);
                e.Graphics.DrawString("Χ", ptzt8, Brushes.Black, xxx, YY + 3);
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
                int xxx = (int)((T.Days * 24 * 60 + T.Hours * 60 + T.Minutes) * 11 / jcsjjg + 115 + x);
                Image newImage = Properties.Resources.CG;
                e.Graphics.DrawImage(newImage, new Rectangle(xxx, YY + 3, 10, 10));
            }
            if (bgTime >= pagetime && bgTime <= pagetime.AddHours(jcsjjg))
            {
                TimeSpan T = bgTime - pagetime;
                int xxx = (int)((T.Days * 24 * 60 + T.Hours * 60 + T.Minutes) * 11 / jcsjjg + 115 + x);
                Image newImage = Properties.Resources.BG;
                e.Graphics.DrawImage(newImage, new Rectangle(xxx, YY + 3, 10, 10));
            }

            #endregion

            #region 打印特殊用药
            int tsi = 1;
            string tss1 = "";
            foreach (adims_MODEL.tsyy ts in tsyyList)
            {
                if (ts.D >= pagetime && ts.D <= pagetime.AddHours(jcsjjg))
                {
                    TimeSpan t = new TimeSpan();
                    t = ts.D - pagetime;
                    float tx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120) + x;
                    e.Graphics.FillEllipse(Brushes.LightGreen, tx - 3, YY + 3, 10, 9);
                    e.Graphics.DrawString(tsi.ToString(), ptzt7, Brushes.Black, new PointF(tx - 3, YY + 3));
                }
                tss1 = tss1 + tsi.ToString() + "." + ts.Name + " " + ts.Yl + "" + ts.Dw + "\n";
                tsi++;
            }
            e.Graphics.DrawString(tss1, ptzt7, Brushes.Black, new PointF(660 + x, YY + 65));

            #endregion

            #region 打印术中事件
            int szi = 1;
            string szss1 = "";
            foreach (adims_MODEL.szsj sz in szsjList)
            {
                if (sz.D >= pagetime && sz.D <= pagetime.AddHours(jcsjjg))
                {
                    TimeSpan t = new TimeSpan();
                    t = sz.D - pagetime;
                    float tx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120 + x);
                    e.Graphics.FillRectangle(Brushes.Pink, tx - 3, YY + 3, 9, 9);
                    e.Graphics.DrawString(szi.ToString(), ptzt7, Brushes.Black, new PointF(tx - 3, YY + 3));
                }
                szss1 = szss1 + szi.ToString() + "." + sz.Name + "\n";
                szi++;
            }
            e.Graphics.DrawString(szss1, ptzt7, Brushes.Black, new PointF(540 + x, YY + 65));
            #endregion

            #region 打印尾部区域↓
            YY = YY + 20; Y_unLine = YY + 16;
            e.Graphics.DrawLine(ptp, new Point(20 + x, YY), new Point(780 + x, YY));
            e.Graphics.DrawString("备注：" + txtRemark.Controls[0].Text, ptzt7, Brushes.Black, new Point(25 + x, YY + 3));
            e.Graphics.DrawLine(ptp, new Point(50 + x, Y_unLine), new Point(755 + x, Y_unLine));
            YY = YY + 25; Y_unLine = YY + 13;
            e.Graphics.DrawLine(ptp, new Point(20 + x, YY), new Point(780 + x, YY));
            e.Graphics.DrawLine(ptp, new Point(535 + x, YY), new Point(535 + x, 1100 + y));
            e.Graphics.DrawLine(ptp, new Point(655 + x, YY), new Point(655 + x, 1100 + y));
            e.Graphics.DrawString("术中事件", ptzt8, Brushes.Black, new Point(570 + x, YY + 2));
            e.Graphics.DrawString("其他用药", ptzt8, Brushes.Black, new Point(680 + x, YY + 2));
            YY = YY + 25; Y_unLine = YY + 14;
            e.Graphics.DrawString("手术方式 " + txtShoushuFS.Controls[0].Text, ptzt8, Brushes.Black, 25 + x, YY);
            e.Graphics.DrawLine(ptp, new Point(70 + x, Y_unLine), new Point(530 + x, Y_unLine));
            YY = 40 + YY; Y_unLine = YY + 14;
            e.Graphics.DrawString("麻醉方式 " + txtMazuiFS.Controls[0].Text, ptzt8, Brushes.Black, 25 + x, YY);
            e.Graphics.DrawLine(ptp, new Point(70 + x, Y_unLine), new Point(530 + x, Y_unLine));
            YY = 40 + YY; Y_unLine = YY + 14;
            e.Graphics.DrawString("手术医师 " + txtSSYS.Controls[0].Text, ptzt8, Brushes.Black, 25 + x, YY);
            e.Graphics.DrawLine(ptp, new Point(70 + x, Y_unLine), new Point(260 + x, Y_unLine));
            e.Graphics.DrawString("麻醉医师 " + txtMZYS.Controls[0].Text, ptzt8, Brushes.Black, 280 + x, YY);
            e.Graphics.DrawLine(ptp, new Point(325 + x, Y_unLine), new Point(530 + x, Y_unLine));
            YY = 40 + YY; Y_unLine = YY + 14;
            e.Graphics.DrawString("器械护士 " + txtQXHS.Controls[0].Text, ptzt8, Brushes.Black, 25 + x, YY);
            e.Graphics.DrawLine(ptp, new Point(70 + x, Y_unLine), new Point(260 + x, Y_unLine));
            e.Graphics.DrawString("巡回护士 " + txtXHHS.Controls[0].Text, ptzt8, Brushes.Black, 280 + x, YY);
            e.Graphics.DrawLine(ptp, new Point(325 + x, Y_unLine), new Point(530 + x, Y_unLine));
            YY = 25 + YY; Y_unLine = YY + 14;
            //e.Graphics.DrawString("切口类型 " + cmbBRQX.Text, ptzt8, Brushes.Black, 25 + x, YY);
            //e.Graphics.DrawLine(ptp, new Point(70 + x, Y_unLine), new Point(200 + x, Y_unLine));
            //e.Graphics.DrawString("切口数量 " + cmbBRQX.Text, ptzt8, Brushes.Black, 210 + x, YY);
            //e.Graphics.DrawLine(ptp, new Point(255 + x, Y_unLine), new Point(350 + x, Y_unLine));
            //e.Graphics.DrawString("病人去向 " + cmbBRQX.Text, ptzt8, Brushes.Black, 360 + x, YY);
            //e.Graphics.DrawLine(ptp, new Point(405 + x, Y_unLine), new Point(530 + x, Y_unLine));

            #endregion


        }


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
            printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
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
                        MessageBox.Show(YwName + "没用结束，不能退出");
                        e.Cancel = true;
                        return;

                    }
                    this.FormClosing -= new FormClosingEventHandler(this.mzjldEdit_FormClosing);
                    dal.UpdateShoushujianinfo(0, 0, "0", Oroom);
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
            if (!mzks)
            {
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
                //txtMZKS.Controls[0].Text = DateTime.Now.ToString("HH:mm");
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
            ssmzcgbgFlag = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.Blue;
            lab1.AutoSize = true;
            pictureBox3.Controls.Add(lab1);
            lab1.Location = new Point(lbMzjs1.Location.X, lbMzjs1.Location.Y - 10);
            xStart = lbMzjs1.Location.X;
            yStart = lbMzjs1.Location.Y;
            DateTime DTIME = otime.AddMinutes((xStart - 160) / 15 * jcsjjg);
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
                DateTime DTIME = otime.AddMinutes((xEnd - 165) / 15 * jcsjjg);
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
            //pictureBox3.Refresh();
        }
        #endregion

        #region <<拔管，标志移动>>
        private void lbBg_DoubleClick(object sender, EventArgs e)
        {
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
            //pictureBox3.Refresh();

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
                dal.UpdateShoushujianinfo(2, Oroom);
                //txtSSKS.Controls[0].Text = DateTime.Now.ToString("HH:mm");
                lbSSKS.MouseDown += new MouseEventHandler(ssks1_MouseDown);
                lbSSKS.MouseMove += new MouseEventHandler(ssks1_MouseMove);
                lbSSKS.MouseUp += new MouseEventHandler(ssks1_MouseUp);
                lbSSKS.MouseLeave += new EventHandler(ssks1_MouseLeave);
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

        }

        private void ssks1_MouseLeave(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
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

        #endregion

        #region <<手术结束，结束标志移动>>
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
            string IPaddress = "132.147.160.41";
            bool flag = TextValueLimit.PingHost(IPaddress, 1000);
            if (flag == true)
            {
                string inpatientID = patID;
                string url = "http://132.147.160.41/DicomWeb/Dicomweb.dll/login?PTNID=" + patID + "&User=sm1&Password=1";
                System.Diagnostics.Process.Start(url);
            }
            else MessageBox.Show("影响病历 数据库未连接，请检查网络");

            //yxbl yxblform = new yxbl();
            //yxblform.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)/// 检验病历
        {
            string IPaddress = "132.147.160.6";
            bool flag = TextValueLimit.PingHost(IPaddress, 1000);
            if (flag == true)
            {
                LisResult f1 = new LisResult(patID);
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
            int result = 0;
            List<string> mzdList1 = new List<string>();
            mzdList1.Add(this.cmbASA.Text.Trim());
            if (this.cbJizhen.Checked)
                mzdList1.Add("1");
            else
                mzdList1.Add("0");
            mzdList1.Add(this.cmbSQJinshi.Text.Trim());
            mzdList1.Add(this.txtTSBQing.Controls[0].Text.Trim());
            mzdList1.Add(this.txtSqzd.Controls[0].Text.Trim());
            mzdList1.Add(this.txtNssss.Controls[0].Text.Trim());
            mzdList1.Add(this.cmbTiwei.Text.Trim());
            mzdList1.Add(this.txtShoushuFS.Controls[0].Text.Trim());
            mzdList1.Add(this.txtMazuiFS.Controls[0].Text.Trim());
            mzdList1.Add(this.txtSSYS.Controls[0].Text.Trim());
            mzdList1.Add(this.txtMZYS.Controls[0].Text.Trim());
            mzdList1.Add(this.txtQXHS.Controls[0].Text.Trim());
            mzdList1.Add(this.txtXHHS.Controls[0].Text.Trim());
            mzdList1.Add(txtWeight.Controls[0].Text.Trim());
            mzdList1.Add(txtHeight.Controls[0].Text.Trim());
            mzdList1.Add(txtSqyy.Controls[0].Text.Trim());
            mzdList1.Add(tbChuNiao.Text.Trim());
            mzdList1.Add(tbChuxue.Text.Trim());
            mzdList1.Add(cmbQiekouType.Text.Trim());
            mzdList1.Add(tbQiekouCount.Text.Trim());
            mzdList1.Add(Convert.ToString(mzjldID));
            result = dal.UpdateMzjld1(mzdList1);
            return result;

        }

        private void AddPointTSMenu_Click(object sender, EventArgs e)//右键检测点添加事件
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
                if (i == 20 || i == 22 || i == 30 || i == 34 || i == 36)
                    e.Graphics.DrawLine(Pens.Black, new Point(1, 15 * i), new Point(1069, 15 * i));
                else
                    e.Graphics.DrawLine(Pens.Black, new Point(25, 15 * i), new Point(1069, 15 * i));
            }
            //↑横细线            
            for (int i = 0; i < 60; i++)
                e.Graphics.DrawLine(pxuxian, new PointF((float)(i * 90 / 6 + 169), (float)1), new PointF((float)(i * 90 / 6 + 169), (float)540));
            for (int i = 0; i < 10; i++)
                e.Graphics.DrawLine(Pens.Black, new PointF((float)(i * 90 + 169), (float)1), new PointF((float)(i * 90 + 169), (float)542));

            #region //画气体
            ArrayList sssQT = new ArrayList();
            int dy = 0;// 控制画气体输出的Y坐标           
            foreach (adims_MODEL.mzqt mz in mzqtList)
            {
                if (mz.Bz > 0)
                {
                    if (sssQT.Contains(mz.Qtname))
                        dy = dy - 1;
                    //e.Graphics.DrawString(mz.Qtname, this.Font, Brushes.Black, new Point(35, 15 * (dy + 0) + 525));
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
                    int y1 = 15 * (dy) + 528;
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
                    sssQT.Add(mz.Qtname);
                }
            }
            #endregion

            #region //画诱导药
            ArrayList sss = new ArrayList();
            int dy1 = 0;// 控制画气体输出的Y坐标           
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
                    int y1 = 15 * (dy1 + 0) + 5;
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
                        string str =(x2 - x1 + y1).ToString();
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

            #region //画局麻药

            int dy2 = 0;// 控制画局麻药输出的Y坐标
            ArrayList sssJMY = new ArrayList();
            foreach (adims_MODEL.jtytsx jt in jmyList)//画局麻药
            {
                if (jt.Bz > 0)
                {
                    if (sssJMY.Contains(jt.Name))
                    {
                        dy2 = dy2 - 1;
                    }
                    e.Graphics.DrawString(jt.Name, this.Font, Brushes.Black, new Point(37, 15 * (dy2) + 302));
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
                    int y1 = 15 * (dy2) + 305;
                    if (x1 > 170)
                    {
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                        e.Graphics.DrawString(jt.Jl.ToString() + jt.Dw.ToString(), this.Font, Brushes.Blue, new Point(x1, y1 - 8));
                        //e.Graphics.DrawString(jt.Jl.ToString() + jt.Dw.ToString(), this.Font, Brushes.Black, new Point(960, y1-2));
                    }
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
                    dy2++;
                    sssJMY.Add(jt.Name);
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
                        e.Graphics.DrawString(mz.Name, this.Font, Brushes.Black, new Point(35, 15 * (dy3) + 332));
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
                        int y1 = 15 * (dy3) + 335;
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
                        e.Graphics.DrawString(mz.Name, this.Font, Brushes.Black, new Point(35, 15 * (dy4) + 452));
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
                        int y1 = 15 * (dy4) + 455;

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

            #region //画SPO2
            foreach (adims_MODEL.jhxm jt in jhxmValue)
            {
                if (jt.Sy == "SpO2")
                {
                    TimeSpan t = new TimeSpan();
                    t = jt.D - otime;
                    TimeSpan t1 = new TimeSpan();
                    t1 = cgTime - otime;
                    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 163);
                    int y = 512;
                    int x1 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 163);
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

        private void pictureBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.X > 0 && e.X < 169)
            {
                if (e.Y > 0 && e.Y < 300)
                {
                    addYdyao mzytform = new addYdyao(mzjldID);
                    mzytform.ShowDialog();
                    BindYdyList();
                }

                else if (e.Y > 300 && e.Y < 330)
                {
                    addJuMaYao szyy = new addJuMaYao(mzjldID);
                    szyy.ShowDialog();
                    BindJmyList();
                }
                else if (e.Y > 330 && e.Y < 450)
                {
                    addShuye szyy = new addShuye(mzjldID);
                    szyy.ShowDialog();
                    BindShuyeList();
                }
                else if (e.Y > 450 && e.Y < 510)
                {
                    addShuxue szyy = new addShuxue(mzjldID);
                    szyy.ShowDialog();
                    BindShuxueList();
                }

                else if (e.Y > 525 && e.Y < 540)
                {
                    addQty qt = new addQty(mzjldID);
                    qt.ShowDialog();
                    BindQtList();
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

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            int dy = 0;
            ArrayList sssQT = new ArrayList();
            foreach (adims_MODEL.mzqt mz in mzqtList)
            {
                if (mz.Bz > 0)
                {
                    if (sssQT.Contains(mz.Qtname))
                        dy = dy - 1;
                    TimeSpan t = new TimeSpan();
                    t = mz.Sysj - otime;
                    TimeSpan t2 = new TimeSpan();
                    t2 = mz.Jssj - otime;
                    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                    int y = 15 * (dy) + 530;
                    int x2 = (int)((t2.Days * 24 * 60 + t2.Hours * 60 + t2.Minutes) * 15 / jcsjjg + 170);
                    int y2 = 15 * (dy) + 530;
                    if (p2x > x - 5 && p2x < x + 5 && p2y > y - 1 && p2y < y + 8)
                    { flagP2 = 1; typeP2 = 1; t_mzqt = mz; }
                    if (p2x > x2 - 5 && p2x < x2 + 5 && p2y > y2 - 1 && p2y < y2 + 8)
                    { flagP2 = 1; typeP2 = 11; t_mzqt = mz; }
                    dy++;
                    sssQT.Add(mz.Qtname);
                }

            }
            int dy1 = 0;
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
            int dy2 = 0;
            ArrayList sssJMY = new ArrayList();
            foreach (adims_MODEL.jtytsx jt in jmyList)
            {
                if (jt.Bz > 0)
                {
                    if (sssJMY.Contains(jt.Name))
                        dy2 = dy2 - 1;
                    TimeSpan t = new TimeSpan();
                    t = jt.Kssj - otime;
                    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 170);
                    int y = 15 * (dy2) + 305;
                    if (p2x > x - 5 && p2x < x + 5 && p2y > y - 1 && p2y < y + 8)
                    { flagP2 = 1; typeP2 = 3; t_jmy = jt; }
                    dy2++;
                    sssJMY.Add(jt.Name);
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
                    int y = 15 * (dy3) + 335;
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
                    int y = 15 * (dy4) + 455;
                    if (p2x > x - 5 && p2x < x + 5 && p2y > y - 1 && p2y < y + 8)
                    { flagP2 = 1; typeP2 = 5; t_shuxue = yt; }
                    dy4++;
                    sssSX.Add(yt.Name);
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
            if (flagP2 == 1)
            {
                if (typeP2 == 1)
                {
                    t_mzqt.Sysj = otime.AddMinutes((e.X - 170) / 15 * jcsjjg);
                }
                if (typeP2 == 11)
                {
                    t_mzqt.Jssj = otime.AddMinutes((e.X - 170) / 15 * jcsjjg);
                }
                if (typeP2 == 2)
                {
                    t_ydy.Sysj = otime.AddMinutes((e.X - 170) / 15 * jcsjjg);
                }
                if (typeP2 == 22)
                {
                    t_ydy.Jssj = otime.AddMinutes((e.X - 170) / 15 * jcsjjg);
                }
                if (typeP2 == 3)
                {
                    t_jmy.Kssj = otime.AddMinutes((e.X - 170) / 15 * jcsjjg);
                }
                if (typeP2 == 4)//输液移动
                {
                    t_shuye.Kssj = otime.AddMinutes((e.X - 170) / 15 * jcsjjg);
                }
                if (typeP2 == 5)//输血移动
                {
                    t_shuxue.Kssj = otime.AddMinutes((e.X - 170) / 15 * jcsjjg);
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
                    bll.xgqtKssj(mzjldID, t_mzqt);
                }
                if (typeP2 == 11)
                {
                    bll.xgqtJssj(mzjldID, t_mzqt);
                }
                if (typeP2 == 2)
                {
                    if (!t_ydy.Cxyy)
                        bll.xgytKssjJssj(mzjldID, t_ydy);
                    else
                        bll.xgytKssj(mzjldID, t_ydy);
                }
                if (typeP2 == 22)
                {
                    bll.xgytJssj(mzjldID, t_ydy);
                }
                if (typeP2 == 3)
                {
                    bll.xgjt(mzjldID, t_jmy);
                }
                if (typeP2 == 4)
                {
                    int i = bll.xgshuyeKSSJ(mzjldID, t_shuye);
                }
                if (typeP2 == 5)
                {
                    int i = bll.xgshuxueKSSJ(mzjldID, t_shuxue);
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

            foreach (adims_MODEL.point tp in twList)//画体温
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
                    e.Graphics.DrawPolygon(Pens.Maroon, new Point[3] { new Point(x, y), new Point(x + 4, y + 6), new Point(x - 4, y + 6) });
                    if (dyd2 != 0 && lastpoint2.X > 169)
                        e.Graphics.DrawLine(Pens.Maroon, new Point(x, y), lastpoint2);
                }
                lastpoint2.X = x + 4;
                lastpoint2.Y = y;
                dyd2++;
            }

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
            int iJikong = 0;
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
                    e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Red, x, y);
                }
                else
                    y = (int)(368 - (int)(tp.V * 1.5));

                if (x > 169)
                {

                    if (jkksTime < tp.D && jkjsTime > tp.D)
                    {
                        int xPlus = x - lastpoint4.X; // 描点作图
                        Point p1 = lastpoint4;
                        Point p2 = new Point(x - 2 * xPlus / 3, y + 7);
                        Point p3 = new Point(x - xPlus / 3, y - 7);
                        Point p4 = new Point(x, y);
                        e.Graphics.DrawBezier(Pens.DarkCyan, p1, p2, p3, p4);

                    }
                    else
                        e.Graphics.DrawEllipse(Pens.DarkCyan, new Rectangle(x - 3, y - 3, 6, 6));
                    if ((dyd4 != 0 && lastpoint4.X > 169) && (jkksTime > tp.D || jkjsTime < tp.D))
                        e.Graphics.DrawLine(Pens.DarkCyan, new Point(x, y), lastpoint4);
                }
                lastpoint4.X = x;
                lastpoint4.Y = y;
                dyd4++;
            }
            foreach (adims_MODEL.point tp in etco2List)//画ETCO2
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
                    if (tp.V > 0)
                        e.Graphics.DrawPolygon(Pens.DarkOrange, new Point[3] { new Point(x, y), new Point(x - 4, y - 6), new Point(x + 4, y - 6) });

                    if (dyd5 != 0 && lastpoint5.X > 169)
                        e.Graphics.DrawLine(Pens.DarkOrange, new Point(x, y), lastpoint5);

                }
                
                lastpoint5.X = x;
                lastpoint5.Y = y;
                dyd5++;
            }
            #endregion



            #region //画术中事件,其他用药
            int szsji = 1;
            foreach (adims_MODEL.szsj s in szsjList)//画术中事件
            {
                TimeSpan t = new TimeSpan();
                t = s.D - otime;
                int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 165);
                float y1 = (float)(370);
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
                if (tssy.D < jkksTime || tssy.D > jkjsTime)
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
                        lx = 4;
                        xgqvalue = t_point.V;
                    }
                }
            }

            foreach (adims_MODEL.point tssy in twList)
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
                    flagP3 = 1; typeP3 = 1;
                    t_point = tssy;
                    lab1.Visible = true;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.AutoSize = true;
                    pictureBox3.Controls.Add(lab1);
                    lab1.Location = new Point(x, y);
                    lab1.Text = t_point.V.ToString();
                    lx = 5;
                    xgqvalue = t_point.V;
                }
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
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 165);
                int y = 370;
                if (e.X > x - 8 && e.X < x + 8 && e.Y < y + 12 && e.Y > y)
                { flagP3 = 1; typeP3 = 2; t_szsj = sj; }
            }
            foreach (adims_MODEL.tsyy ts in tsyyList)//是否选中特殊用药
            {
                TimeSpan t = new TimeSpan();
                t = ts.D - otime;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 165);
                int y = 370;
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
                    lab1.Location = new Point(p3x, p3y);
                    lab1.BringToFront();
                    lx = t_point.Lx;
                    xghvalue = t_point.V; //得到修改后的值
                    xgdtime = Convert.ToDateTime(t_point.D);
                }
                if (typeP3 == 3)//特殊用药移动
                {
                    int t = e.X;
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
                    int t = e.X;
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
            }
            pictureBox3.Refresh();
            listBox1.Focus();
        }
        private void pictureBox4_Paint(object sender, PaintEventArgs e)
        {
            Pen p1 = new Pen(Brushes.Black);
            Pen p2 = new Pen(Brushes.Black);
            p2.Width = 2;
            e.Graphics.DrawLine(p2, new Point(1, 0), new Point(1, 225));
            e.Graphics.DrawLine(p2, new Point(1, 225), new Point(1069, 225));
            e.Graphics.DrawLine(p2, new Point(1069, 0), new Point(1069, 225));
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
                int fa = dal.insertJianCeData(mzjldID, hr, spo2, pulse, nibps, nibpd, nibpm, arts, artd, artm, etco2, ico2, rrc);

                if (fa != 1) { MessageBox.Show("错误"); }
            }

        }
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

        private void BindJikongTime()
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
            JKTimeSet f1 = new JKTimeSet(mzjldID);
            f1.ShowDialog();
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
            adims_BLL.TextValueLimit.Text_Value_Limit(sender, e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.TextValueLimit.Text_Value_Limit(sender, e);
        }

        private void btnEMR_Click(object sender, EventArgs e)
        {
            string IPaddress = "132.147.160.60";
            bool flag = TextValueLimit.PingHost(IPaddress, 1000);
            if (flag == true)
            {
                Process proc = new Process();
                proc.StartInfo.FileName = "D:\\ReadEMR\\mrequery.exe";
                proc.StartInfo.Arguments = patID;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();
            }
            else MessageBox.Show("电子病历 数据库未连接，请检查网络");
        }

        private void btnBeforeVisit_Click(object sender, EventArgs e)
        {
            BeforeVisit_YS f1 = new BeforeVisit_YS(patID);
            f1.Show();
        }

        private void btnNurseRecord_Click(object sender, EventArgs e)
        {
            NurseRecord_CJ f1 = new NurseRecord_CJ(patID, mzjldID.ToString());
            f1.Show();
        }

        private void btnMZZJ_Click(object sender, EventArgs e)
        {
            MZZJ_CJ f1 = new MZZJ_CJ(mzjldID.ToString(), patID);
            f1.Show();
        }

        private void timerJKW_Tick(object sender, EventArgs e)
        {
            //ProceedreceivedData_JKW();
            timerJKW.Interval = 5000;
        }
        /// <summary>
        /// 输血
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShuXuePG_Click(object sender, EventArgs e)
        {
            ShuXuePG f1 = new ShuXuePG(mzjldID.ToString(),patID);
            f1.Show();

        }
        /// <summary>
        /// 麻醉指标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMZZB_Click(object sender, EventArgs e)
        {
            AddMZZB f1 = new AddMZZB(mzjldID.ToString(), patID);
            f1.Show();
        }


   
    }
}

