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

namespace main
{
    public partial class menzhenjiludan : Form
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
        #region <<Members>>
        adims_DAL.mz dalll = new adims_DAL.mz();
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
        DateTime yangqijssj = new DateTime();//麻醉开始时间
        DateTime mzjsTime = new DateTime();//麻醉结束时间
        DateTime cgTime = new DateTime();//插管时间
        DateTime bgTime = new DateTime();//拔管时间
        DateTime jkksTime = new DateTime();//机控呼吸开始时间
        DateTime jkjsTime = new DateTime();//疾控呼吸结束时间
        DateTime ksjcTime = new DateTime();//开始检测时间参数
        public int mzjldID = 0;

        int xxx = 0;
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
        public menzhenjiludan()
        {
            InitializeComponent();
        }
        public menzhenjiludan(string mzjldID, string patID)
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
        public menzhenjiludan(string patID)
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
        private void BindCombox3()
        {
            // mazyscom.Items.Clear();
            fsys.Items.Clear();
            DataTable dt = new DataTable();
            dt = dal.GetAllMZYS();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                fsys.Items.Add(dt.Rows[i][0]);
                // mazyscom.Items.Add(dt.Rows[i][0]);
            }

           
        }
         private void BindComboxhushi()
        {
            // mazyscom.Items.Clear();
            comfuhs.Items.Clear();
            DataTable dt = new DataTable();
            dt = dal.GetAll_hushi();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comfuhs.Items.Add(dt.Rows[i][0]);
                // mazyscom.Items.Add(dt.Rows[i][0]);
            }

            
        }
        private void menzhenjiludan_Load(object sender, EventArgs e)
        {
            this.dtOtime.Format = DateTimePickerFormat.Custom;
            this.dtOtime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
           
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
                bll.addmenzhenmzjld(patID, otime);
                DataTable dt = new DataTable();
                dt = bll.selectmenzhenjld(patID, otime);
                mzjldID = Convert.ToInt32(dt.Rows[0][0]);
                cmbSJJG.Text = "5";
                texnsml.Text = "500";
                texbbf.Text = "100";
                checkysspyes.Checked = true;
                exoutuwu.Checked = true;
                textspo2.Text = "100";
                textvaspf.Text = "0";
                textsteward.Text = "6";
                jcsjjg = 5;
                BindShijiandian();
                BindJjibenshujv();
                BindCombox3();
                BindComboxhushi();
                
            }
            else if (mzjldID != 0)
            {
                //DataTable dt = new DataTable();
                //// txtMzjldid.Controls[0].Text = Convert.ToString(mzjldID);
                DataTable dtMZ_Info = bll.SelectOldPatInfomenzhen(mzjldID);
                otime = (Convert.ToDateTime(dtMZ_Info.Rows[0]["rushishijian"]));
                cmbSJJG.Text = "5";
                jcsjjg = Convert.ToInt32(cmbSJJG.Text);
                dtOtime.Value = otime;
                BindShijiandian();//绑定时间坐标     
                BindJHDian();
                BindJjibenshujv();
                BindMzjldBacicInfo();
                BindMZSSCGBG();
            }
        }
        public void BindJjibenshujv()//绑定基本数据
        {
            DataTable dt1 = dalll.GetMazuimzpgdan(patID);
            DataRow dr1 = dt1.Rows[0];
            textzhuyuanhao.Text = dr1["menzhenhao"].ToString();
            txbname.Text = dr1["xingming"].ToString();
            txbage.Text = dr1["age"].ToString();
            txbsex.Text = dr1["sex"].ToString();
            txbtizhong.Text = dr1["tizhong"].ToString();
            textminzu.Text = dr1["mingzu"].ToString();
            txbkeshi.Text = dr1["keshi"].ToString();
            texzhenduan.Text = dr1["zhenduan"].ToString();
            DataTable dt2 = bll.Tljselectljmzmenzhensqfs(patID);
            if (dt1.Rows.Count > 0)
            {
                DataRow dr3= dt2.Rows[0];
                comasafj.Text = dr3["ASAfj"].ToString();
                cmbnxzlff.Text = dr3["nixzlff"].ToString();
                comzfs.Text = dr3["mzff"].ToString();
                mazyscom.Text = dr3["mzys"].ToString();
            }
        }
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

                if (cmbJianhuyi.Text == "模拟监护仪")
                {
                    cmbSJJG.Enabled = true;
                    cmbJianhuyi.Enabled = false;
                    //btnPrint.Enabled = false;
                    //btnSave.Enabled = false;
                    timer2.Enabled = true;
                    //pick_count = 0;
                    //max_pickCount = int.Parse(cmbSJJG.Text.Trim()) * 60;
                    timer2.Interval = 1000 * int.Parse(cmbSJJG.Text);//改变timer的间隔时间

                }
                if (cmbJianhuyi.Text == "金科威监护仪")
                {
                    ////timerJKW.Start();
                    //JkwDataResolve();
                    //cmbJianhuyi.Enabled = false;
                    //cmbCOM.Enabled = false;
                }
                if (cmbJianhuyi.Text == "飞利浦监护仪")
                {
                    FillipData();
                    cmbJianhuyi.Enabled = false;
                    cmbCOM.Enabled = false;
                    timer1.Interval = 10 * 1000;
                    timer1.Start();
                }
                if (cmbJianhuyi.Text == "迈瑞监护仪")
                {
                    //timer_Miray.Start();
                    //MaintainTimer.Start();
                    //cmbJianhuyi.Enabled = false;
                    //cmbCOM.Enabled = false;
                    //MirayT6DataResolve();

                }
                if (cmbJianhuyi.Text == "理邦监护仪")
                {
                    //this.timer_LB.Start();
                }

                // SaveMzjld();
                ksjcTime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                btnMonitor.Text = "结束监测";
                
            }
            #endregion
            else
            {
                if (DialogResult.OK == MessageBox.Show("确定结束检测吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                {
                    //timer_Miray.Stop();
                    //timer_LB.Stop();
                    //MaintainTimer.Stop();
                    //timer4.Enabled = false;
                    timer1.Enabled = false;
                    timer2.Enabled = false;
                    //timerbis.Enabled = false;
                    //timerJKW.Stop();
                    //if (serialPort1.IsOpen)
                    //{

                    //    serialPort1.Close();
                    //}

                    //if (_serialPort.IsOpen)
                    //{
                    //    _serialPort.StopTransfer();
                    //    _serialPort.Close();
                    //}

                    if (ThreadExist)
                    {
                        ThreadExist = false;
                        Receiving_xy.Abort();//强制结束线程运行
                        Receiving_xy = null; ////在.NET中，有GC垃圾回收，如果这里不赋为null，不回收内存，甚至永远这么占着，导致内存泄漏。
                    }
                    //if (ThreadExist_JKW)
                    //{
                    //    if (ServerSocket_JKW != null)
                    //        ServerSocket_JKW.Close();
                    //    if (Receiving_JKW.ThreadState != System.Threading.ThreadState.Running)
                    //    {
                    //        Receiving_JKW.Abort();
                    //    }
                    //    Receiving_JKW.Abort();
                    //    Receiving_JKW = null;
                    //    ThreadExist_JKW = false;
                    //}
                    //if (isexist_Miray)
                    //{
                    //    if (ClientSocket_Miray != null)
                    //        ClientSocket_Miray.Close();
                    //    if (SocketThread_Miray.ThreadState != System.Threading.ThreadState.Running)
                    //    {
                    //        SocketThread_Miray.Abort();
                    //    }
                    //    isexist_Miray = false;
                    //}
                    btnMonitor.Text = "开始监测";
                    cmbJianhuyi.Enabled = true;
                    cmbCOM.Enabled = true;
                }
            }
        }

        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            int dyd = 0, dyd1 = 0, dyd2 = 0, dyd3 = 0, dyd4 = 0, dyd5 = 0;//标志是否是第一个点
            Pen pred2 = new Pen(Brushes.Red);
            pred2.Width = 2;
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
            #region //画气体
            //ArrayList sssQT = new ArrayList();
            //int dy = 0;// 控制画气体输出的Y坐标     
            DataTable dtMzjld = bll.selectmzjldmenzhenid(mzjldID);
            if (dtMzjld.Rows[0]["yangqijiesushijian"].ToString() != "")
            {
                yangqijssj = Convert.ToDateTime(dtMzjld.Rows[0]["yangqijiesushijian"]);
                TimeSpan t = new TimeSpan();
                t = yangqijssj - otime;
                TimeSpan t1 = new TimeSpan();
                t1 = yangqijssj - otime;
               // xxx = (t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) + 170;

                int x1 = (int)(171);
                       int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 171);
                int y1 = 15;
                //        if (x1 > 170)
                //        {
                e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                e.Graphics.DrawString("100 %", this.Font, Brushes.Blue, new Point(x1, y1 - 8));
                //        }
                //        if (x2 - x1 > 5 && mz.Bz == 1)
                //        {
                //            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1));
                //            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1 + 10));
                //        }
                //        else if (x2 > 170 && x2 - x1 > 5 && mz.Bz == 2)
                //        {

                e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 5, y1 + 8), new Point(x2 + 5, y1 + 8) });
                //double qtzongliang=(mz.Yl) *Convert.ToDouble( (x2 - x1)/3.27);
                //e.Graphics.DrawString(Convert.ToInt32(qtzongliang).ToString() + "L", this.Font, Brushes.Blue, new Point(960, y1 - 2));
                //MessageBox.Show(mz.Dw.Split('/').ToString());
                //        }
                //        if (x2 - x1 > 5)
                //        {
                if (x1 > 170 && x2 > 170)
                {
                    e.Graphics.DrawLine(pred2, new Point(x1, y1 + 5), new Point(x2, y1 + 5));
                }
            }
            else
            {
                int x1 = (int)(171);
                //        int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 171);
                int y1 = 15;
                //        if (x1 > 170)
                //        {
                e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                e.Graphics.DrawString("100 %", this.Font, Brushes.Blue, new Point(x1, y1 - 8));
                TimeSpan t1 = new TimeSpan();
                t1 =DateTime.Now-otime;
                int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 171);
                 if (x2 - x1 > 5 )
                 {
                     e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1));
                     e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1 + 10));
                 }
                 if (x1 > 170 && x2 > 170)
                 {
                     e.Graphics.DrawLine(pred2, new Point(x1, y1 + 5), new Point(x2, y1 + 5));
                 }
            }
            


            //        }
            //        dy++;
            //        //sssQT.Add(mz.Name);
            //    }
            //}
            #endregion

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
                    lastpoint2.X = (int)x;
                    lastpoint2.Y = (int)y;
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
            //int iJikong = 0;
            //string fxs = "", tvxs = "";
            //DataTable dtMZ_Info = bll.SelectOldPatInfo(mzjldID);

            //if (dtMZ_Info.Rows.Count > 0)
            //{
            //    if (this.toStr(dtMZ_Info.Rows[0]["jkvalue"]) != "")
            //    {
            //        fxs = dtMZ_Info.Rows[0]["jkvalue"].ToString();
            //    }
            //    if (this.toStr(dtMZ_Info.Rows[0]["fzvalue"]) != "")
            //    {
            //        tvxs = dtMZ_Info.Rows[0]["fzvalue"].ToString();
            //    }
            //}
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
                        //if (jkksTime < tp.D && jkjsTime > tp.D)//机械呼吸
                        ////if (rrc!="")//机械呼吸
                        //{
                        //    // 
                        //    //int xPlus = x - lastpoint4.X; // 描点作图
                        //    //Point p1 = lastpoint4;
                        //    //Point p2 = new Point(x - 2 * xPlus / 3, y + 7);
                        //    //Point p3 = new Point(x - xPlus / 3, y - 7);
                        //    //Point p4 = new Point(x, y);
                        //    //e.Graphics.DrawBezier(Pens.DarkCyan, p1, p2, p3, p4);
                        //    this.PaintSignMBreath(e.Graphics, new Point(x - 3, y - 5));
                        //    if (iJikong == 0)
                        //    {

                        //        e.Graphics.DrawString("TV" + tvxs + "/ " + "f" + fxs, this.Font, Brushes.DarkCyan, x - 3, y - 15);
                        //        iJikong++;
                        //    }

                        //}
                        //else if (jkksTime < tp.D && jkjsTime < jkksTime)//机械呼吸
                        ////if (rrc!="")//机械呼吸
                        //{
                        //    // 
                        //    //int xPlus = x - lastpoint4.X; // 描点作图
                        //    //Point p1 = lastpoint4;
                        //    //Point p2 = new Point(x - 2 * xPlus / 3, y + 7);
                        //    //Point p3 = new Point(x - xPlus / 3, y - 7);
                        //    //Point p4 = new Point(x, y);
                        //    //e.Graphics.DrawBezier(Pens.DarkCyan, p1, p2, p3, p4);
                        //    //e.Graphics.DrawString("M", this.Font, Brushes.DarkCyan, x - 3, y - 5);
                        //    this.PaintSignMBreath(e.Graphics, new Point(x - 3, y - 5));
                        //    if (iJikong == 0)
                        //    {
                        //        e.Graphics.DrawString("TV" + tv + "f" + rrc, this.Font, Brushes.DarkCyan, x - 3, y - 15);
                        //        iJikong++;
                        //    }
                        //}
                        //else if (fzksTime < tp.D && fzjsTime > tp.D)
                        //{
                        //    e.Graphics.DrawString("A", this.Font, Brushes.DarkCyan, x - 3, y - 5);
                        //}
                        //else
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
            foreach (adims_MODEL.point tp in spo2List)//画spo2
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

        }

        private void labVisibleTimer_Tick(object sender, EventArgs e)
        {
            labVisibleTimer.Interval = 2500;
            lab1.Visible = false;
            lab2.Visible = false;
            labVisibleTimer.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int countN = sqlite.CopyDataPacu(mzjldID, ksjcTime);//拷贝本地数据到服务器
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
                //pictureBox2.Refresh();
            }
        }
        public void BindJHDian()//监护点赋值
        {
            ssyList.Clear(); szyList.Clear(); xlList.Clear(); twList.Clear(); hxlList.Clear();
            mboList.Clear(); spo2List.Clear(); etco2List.Clear(); jhxmValue.Clear();
            BIS.Clear();
            //RecordTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP
            DataTable datadt = dal.GetAdims_PACU_Point(mzjldID);
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
            //    adims_MODEL.tw_point p5 = new adims_MODEL.tw_point();//体温
            //    try
            //    {
            //        p5.D = Convert.ToDateTime(datadt.Rows[i][0]);//体温记录点
            //        if (datadt.Rows[i]["TEMP"].ToString().IsNullOrEmpty())
            //        {
            //            p5.V = 0;
            //        }
            //        else
            //        {
            //            p5.V = Convert.ToSingle(datadt.Rows[i]["TEMP"].ToString());
            //        }

            //    }
            //    catch (Exception)
            //    {
            //        p5.V = 0;
            //    }
            //    p5.Lx = 5;

            //    twList.Add(p5);
            //}
            //for (int i = 0; i < datadt.Rows.Count; i++)
            //{
            //    adims_MODEL.tw_point p5 = new adims_MODEL.tw_point();//体温记录点
            //    p5.D = Convert.ToDateTime(datadt.Rows[i][0]);
            //    p5.T = datadt.Rows[i][5].ToString();
            //    p5.Lx = 5;
            //    twList.Add(p5);
            //}
            //for (int i = 0; i < datadt.Rows.Count; i++)
            //{
            //    adims_MODEL.point p6 = new adims_MODEL.point();//ETCO2记录点
            //    p6.D = Convert.ToDateTime(datadt.Rows[i][0]);
            //    if (CGUAN && !BGUAN)//如果插管，未拔管
            //    {
            //        if (p6.D > cgTime)//记录点事件大于插管时间
            //        {
            //            p6.V = Convert.ToInt32(datadt.Rows[i]["ETCO2"]);
            //            p6.Lx = 6;
            //            etco2List.Add(p6);
            //        }
            //    }

            //    if (CGUAN && BGUAN)//如果插管，已拔管
            //    {
            //        if (p6.D > cgTime && p6.D < bgTime)//记录点事件大于插管时间并且小于拔管时间
            //        {
            //            p6.V = Convert.ToInt32(datadt.Rows[i]["ETCO2"]);
            //            p6.Lx = 6;
            //            etco2List.Add(p6);
            //        }
            //    }
            //}
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.point p6 = new adims_MODEL.point();//SpO2记录点
                p6.D = Convert.ToDateTime(datadt.Rows[i][0]);
                p6.V = Convert.ToInt32(datadt.Rows[i][6]);
                p6.Lx = 6;
                spo2List.Add(p6);
            }
            //for (int i = 0; i < datadt.Rows.Count; i++)
            //{
            //    adims_MODEL.jhxm jhxmt = new adims_MODEL.jhxm();
            //    jhxmt.D = Convert.ToDateTime(datadt.Rows[i][0]);
            //    jhxmt.V = Convert.ToInt32(datadt.Rows[i]["SPO2"]);
            //    jhxmt.Sy = "SpO2";
            //    jhxmValue.Add(jhxmt);
            //}
            //for (int i = 0; i < datadt.Rows.Count; i++)
            //{
            //    adims_MODEL.jhxm jhxmt = new adims_MODEL.jhxm();
            //    jhxmt.D = Convert.ToDateTime(datadt.Rows[i][0]);
            //    jhxmt.V = Convert.ToInt32(datadt.Rows[i]["ETCO2"]);
            //    jhxmt.Sy = "ETCO2";
            //    jhxmValue.Add(jhxmt);
            //}
            //int biszc = 0;
            //for (int i = 0; i < datadt.Rows.Count; i++)
            //{
            //    adims_MODEL.jhxm jhxmt = new adims_MODEL.jhxm();
            //    if (datadt.Rows[i]["BIS"].ToString().IsNullOrEmpty())
            //    {
            //        biszc = 0;
            //    }
            //    else
            //    {
            //        biszc = Convert.ToInt32(Convert.ToDouble(datadt.Rows[i]["BIS"].ToString()));
            //    }
            //    jhxmt.D = Convert.ToDateTime(datadt.Rows[i][0]);
            //    jhxmt.V = Convert.ToInt16(biszc);
            //    jhxmt.Sy = "BIS";
            //    jhxmValue.Add(jhxmt);
            //}
            //for (int i = 0; i < datadt.Rows.Count; i++)
            //{
            //    adims_MODEL.jhxm jhxmt = new adims_MODEL.jhxm();
            //    jhxmt.D = Convert.ToDateTime(datadt.Rows[i][0]);
            //    if (datadt.Rows[i]["CVP"].ToString().IsNullOrEmpty())
            //    {
            //        jhxmt.V = 0;
            //    }
            //    else
            //    {
            //        jhxmt.V = Convert.ToInt32(datadt.Rows[i]["CVP"]);
            //    }
            //    jhxmt.Sy = "CVP";
            //    jhxmValue.Add(jhxmt);
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
            catch (Exception ex)
            {
                MessageBox.Show("1." + ex);
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
                        this.SaveFillipData1(PatientData, mzjldID, 1);
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
                MessageBox.Show("2" + ex);
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
                    RR = SPO2 / 4;
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
                            fa = sqlite.insertJianCeDataMZJLD(mzjldID, ABP_SYS, ABP_DIA, ABP_MAP, RR, HR, PR, SPO2, ETCO2, TEMP1, CVP_MAP1, now);
                        if (type == 1)
                            fa = sqlite.insertJianCeDataPACU(mzjldID, ABP_SYS, ABP_DIA, ABP_MAP, RR, HR, PR, SPO2, ETCO2, TEMP1, now);
                    }
                    else
                    {
                        if (type == 0)
                            fa = sqlite.insertJianCeDataMZJLD(mzjldID, SYS1, DIA1, MAP1, RR, HR, PR, SPO2, ETCO2, TEMP1, CVP_MAP1, now);
                        if (type == 1)
                            fa = sqlite.insertJianCeDataPACU(mzjldID, SYS, DIA, MAP, RR, HR, PR, SPO2, ETCO2, TEMP1, now);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("3" + ex);
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
        public struct IPConfigureInfo
        {
            public string PatientIPAddress; public string BedID;
        }
        #endregion

        private void btnSaveOtime_Click(object sender, EventArgs e)
        {
            string SQL = "rushishijian='" + dtOtime.Value + "' WHERE menzhenid='" + mzjldID + "'";
            int i = dal.UpdateMzjldmenzhen(SQL);
            if (i == 0) MessageBox.Show("入室时间修改失败！");
            else
            {
                otime = Convert.ToDateTime(dtOtime.Value.ToString("yyyy-MM-dd HH:mm"));
                fristopen = otime;
                BindShijiandian();
                BindMZSSCGBG();
                //pictureBox2.Refresh();
                pictureBox3.Refresh();
                // pictureBox4.Refresh();
            }
        }

        
        private void BindShijiandian()//绑定时间点坐标
        {
            fristopen = otime;
            //jhxmAll.Clear();
            //jhxmAll.Add("ECG");
            //jhxmAll.Add("CVP");
            //jhxmAll.Add("NIBP");
            //jhxmAll.Add("ART");
            //jhxmAll.Add("RESP");
            //jhxmAll.Add("BIS");
            //jhxmAll.Add("TOF");
            //jhxmAll.Add("ETCO2");
            jcsjjg = Convert.ToInt32(cmbSJJG.Text.Trim());
            lbTime1.Text = otime.ToString("HH:mm");
            lbTime2.Text = otime.AddMinutes(6 * jcsjjg).ToString("HH:mm");
            //+ ":" + (otime.AddMinutes(6 * jcsjjs).Minute == 0 ? "00" : "30");
            lbTime3.Text = otime.AddMinutes(12 * jcsjjg).ToString("HH:mm");
            lbTime4.Text = otime.AddMinutes(18 * jcsjjg).ToString("HH:mm");
            lbTime5.Text = otime.AddMinutes(24 * jcsjjg).ToString("HH:mm");
            lbTime6.Text = otime.AddMinutes(30 * jcsjjg).ToString("HH:mm");
            //lbTime7.Text = otime.AddMinutes(36 * jcsjjg).ToString("HH:mm");
            //lbTime8.Text = otime.AddMinutes(42 * jcsjjg).ToString("HH:mm");
            //lbtimew9.Text = lbTime9.Text = otime.AddMinutes(48 * jcsjjg).ToString("HH:mm");
            //lbtimew10.Text = lbTime10.Text = otime.AddMinutes(54 * jcsjjg).ToString("HH:mm");
        }
        [DllImport("wininet")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reserverdaValue);
        public bool IsInNet()
        {
            int isinnet;
            bool flags = InternetGetConnectedState(out isinnet, 0);
            return flags;
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
        //    if (!IsInNet())
        //    {
        //        return;
        //    }
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
                dal.UpdateMzkssjmenzhen(DateTime.Now, mzjldID);
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
            //if (!IsInNet())
            //{
            //    return;
            //}
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
                dal.UpdateMzkssjmenzhen(otime.AddMinutes((xEnd - 160) / 15 * jcsjjg), mzjldID);
                mzksTime = otime.AddMinutes((xEnd - 160) / 3);
            }
            else
            {
                if (xEnd < lbMzjs1.Location.X)
                {
                    dal.UpdateMzkssjmenzhen(otime.AddMinutes((xEnd - 160) / 15 * jcsjjg), mzjldID);
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
        /// 
        private void lbMzjs_DoubleClick(object sender, EventArgs e)
        {
            //if (!IsInNet())
            //{
            //    return;
            //}
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
                dal.UpdateMzjssjmenzhen(DateTime.Now, mzjldID);
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
            //if (!IsInNet())
            //{
            //    return;
            //}
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
                dal.UpdateMzjssjmenzhen(otime.AddMinutes((xEnd - 160) / 15 * jcsjjg), mzjldID);
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
            //if (!IsInNet())
            //{
            //    return;
            //}
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
                //JKTimeSet cg = new JKTimeSet(mzjldID);
                //cg.Show();
                dal.UpdateMzCGmenzhen(DateTime.Now, mzjldID);
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
            //if (!IsInNet())
            //{
            //    return;
            //}
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
                dal.UpdateMzCGmenzhen(otime.AddMinutes((xEnd - 160) / 15 * jcsjjg), mzjldID);
                cgTime = otime.AddMinutes((xEnd - 160) / 15 * jcsjjg);
            }
            else
            {
                if (xEnd < lbBguan.Location.X)
                {
                    dal.UpdateMzCGmenzhen(otime.AddMinutes((xEnd - 160) / 15 * jcsjjg), mzjldID);
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
                dal.UpdateMzBGmenzhen(DateTime.Now, mzjldID);
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
                dal.UpdateMzBGmenzhen(otime.AddMinutes((xEnd - 160) / 15 * jcsjjg), mzjldID);
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
            //if (!IsInNet())
            //{
            //    return;
            //}
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
                dal.UpdateSskssjmenzhen(DateTime.Now, mzjldID);
                ssksTime = DateTime.Now;
                // dal.UpdateShoushujianinfo(2, Oroom);//修改手术间状态表为正在手术
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
            //if (!IsInNet())
            //{
            //    return;
            //}
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
                dal.UpdateSskssjmenzhen(otime.AddMinutes((xEnd - 160) / 15 * jcsjjg), mzjldID);
                ssksTime = otime.AddMinutes((xEnd - 160) / 15 * jcsjjg);
            }
            else
            {
                if (xEnd < lbSSJS.Location.X)
                {
                    dal.UpdateSskssjmenzhen(otime.AddMinutes((xEnd - 160) / 15 * jcsjjg), mzjldID);
                    ssksTime = otime.AddMinutes((xEnd - 160) / 15 * jcsjjg);
                }
                else
                    lbSSKS.Location = new Point(xStart, yStart);
            }
            pictureBox3.Refresh();

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
            //if (!IsInNet())
            //{
            //    return;
            //}
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
                dal.UpdateSsjssjmenzhen(DateTime.Now, mzjldID);
                dal.UpdateSsjsFlagmenzhen(mzjldID);
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
            //if (!IsInNet())
            //{
            //    return;
            //}
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
                //int ssshijian = Convert.ToInt32((ssjs1.Location.X - ssks1.Location.X) / 3.27);
                //txtSssj.Controls[0].Text = (ssshijian / 60).ToString() + "小时" + (ssshijian % 60).ToString() + "分";
                //txtSSJS.Controls[0].Text = DTIME.ToString("HH:mm");
            }
        }

        private void ssjs1_MouseUp(object sender, EventArgs e)
        {
            ssmzcgbgFlag = 0;
            if (xEnd > lbSSKS.Location.X)
            {
                dal.UpdateSsjssjmenzhen(otime.AddMinutes((xEnd - 160) / 15 * jcsjjg), mzjldID);
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

        private void 监测点管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PointManage slj = new PointManage(mzjldID, 1);
            slj.ShowDialog();
            BindJHDian();
            //this.pictureBox2.Refresh();
            this.pictureBox3.Refresh();

        }
        int lx, xgqvalue, xghvalue;//生理记录检测点类型，修改前值，修改后值
        DateTime xgdtime = new DateTime();//修改点时间
        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            if (!IsInNet())
            {
                return;
            }


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

            }


        }

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
                //if (typeP3 == 4)//体温
                //{
                //    string value = ((368 - p3y) / 1.5).ToString();
                //    if (value.Contains("."))
                //    {
                //        int index = value.IndexOf(".");//查找最后一个.所在的位置
                //        string ss = value.Substring(0, index + 2);   // 取子字符串。
                //        tw_point.V = Convert.ToSingle(ss);
                //    }
                //    else
                //    {
                //        tw_point.V = (float)((368 - p3y) / 1.5);
                //    }

                //    lab1.Visible = true;
                //    lab1.BackColor = Color.Transparent;
                //    lab1.ForeColor = Color.OrangeRed;
                //    lab1.AutoSize = true;
                //    lab1.Text = tw_point.V.ToString();
                //    pictureBox3.Controls.Add(lab1);
                //    lab1.Location = new Point(e.X, e.Y);
                //    lab1.BringToFront();
                //    lx = tw_point.Lx;
                //    xghvalue = (int)tw_point.V; //得到修改后的值
                //    xgdtime = Convert.ToDateTime(tw_point.D);
                //}
            }
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            if (flagP3 == 1)
            {
                flagP3 = 0;
                if (typeP3 == 1)
                {
                    int i = bll.xgpointPACU(mzjldID, t_point);
                    //if (i > 0)
                    //    bll.InsertxgLog(mzjldID, t_point, xgqvalue, Program.customer.user_name);
                }
                pictureBox3.Refresh();
                //if (typeP3 == 4)
                //{
                //    bll.xgMZJLDpoint_TW(mzjldID, tw_point);
                //}
            }
            pictureBox3.Refresh();
        }

        //private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        //{
        //   // fy = 0;
        ////   printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;//打印
        ////   printPreviewDialog1.WindowState = FormWindowState.Maximized;
        ////   //           //printDocument1.DefaultPageSettings.PaperSize =
        ////   //    new System.Drawing.Printing.PaperSize("K16", 740, 1020);
        ////   printDocument1.DefaultPageSettings.PaperSize =
        ////              new System.Drawing.Printing.PaperSize("A4", 820, 1160);
        //}
        int fy = 0;
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int x =20, y = 0;//整体位置控制
            string title = "新疆医科大学第五附属医院无痛诊疗麻醉记录单";//标题
            string title2 = "              醉 记 录 单";//标题
            string title3 = "              醉 醉 小 结";//标题
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
            Font htc = new Font("宋体", 16, FontStyle.Bold);
            Font ptzt7 = new Font("宋体", 7);//填入栏目字体           
            Font ptzt7_U = new Font("宋体", 7, FontStyle.Underline, GraphicsUnit.Point);//下划线字体
            Font ptzt6 = new Font("宋体", 6);//填入栏目字体
            Font ptzt5 = new Font("宋体", 5);//填入栏目字体
           int y1 = y + 120;
           e.Graphics.DrawString("姓名:" + txbname.Text.Trim(), ptzt11, Brushes.Black, new Point(x+15, y1));
           e.Graphics.DrawLine(Pens.Black,x+15+ 35, y1 + 15,x+15+ 200, y1 + 15);
           e.Graphics.DrawString("性别:" + txbsex.Text.Trim(), ptzt11, Brushes.Black, new Point(x +15 +200, y1));
           e.Graphics.DrawLine(Pens.Black,x+15+ 240, y1 + 15,x+15+ 280, y1 + 15);

           e.Graphics.DrawString("年龄:" + txbage.Text.Trim() + "岁", ptzt11, Brushes.Black, new Point(x +15+ 290, y1));
           e.Graphics.DrawString("族别:" + textminzu.Text.Trim(), ptzt11, Brushes.Black, new Point(x +15+ 380, y1));
           e.Graphics.DrawLine(Pens.Black,x+15+ 420, y1 + 15,x+15+ 480, y1 + 15);
           e.Graphics.DrawString("体重:" + txbtizhong.Text.Trim(), ptzt11, Brushes.Black, new Point(x + 15+500, y1));
           e.Graphics.DrawLine(Pens.Black,x+15+ 540, y1 + 15,x+15+ 580, y1 + 15);
           e.Graphics.DrawString("kg", ptzt11, Brushes.Black, new Point(x + 15+580, y1));
           e.Graphics.DrawString("日期:" + dtOtime.Value.ToString("yyyy年MM月dd日"), ptzt11, Brushes.Black, new Point(x + 15 + 600, y1));
           e.Graphics.DrawLine(Pens.Black, x + 15 + 630, y1 + 15, x + 15 + 740, y1 + 15);
  
           y1 = y1 + 30;
           e.Graphics.DrawString("科室/床号:" + txbkeshi.Text.Trim() + " " + textch.Text, ptzt11, Brushes.Black, new Point(x+15, y1));
           e.Graphics.DrawLine(Pens.Black,x+15+ 72, y1 + 15,x+15+ 245, y1 + 15);
           e.Graphics.DrawString("住院号/门诊号:" + textzhuyuanhao.Text, ptzt11, Brushes.Black, new Point(x +15+ 250, y1));
           e.Graphics.DrawLine(Pens.Black,x+15+ 340, y1 + 15,x+15+ 445, y1 + 15);
           e.Graphics.DrawString("ASA分级:" + comasafj.Text, ptzt11, Brushes.Black, new Point(x + 15+480, y1));
           e.Graphics.DrawLine(Pens.Black,x+15+ 540, y1 + 15,x+15+ 705, y1 + 15);
           y1 = y1 + 30;
           e.Graphics.DrawString("诊    断:" + texzhenduan.Text.Trim(), ptzt11, Brushes.Black, new Point(x+15, y1));
           e.Graphics.DrawLine(Pens.Black,x+15+ 72, y1 + 15,x+15+ 445, y1 + 15);
           e.Graphics.DrawString("拟行治疗方法:" + cmbnxzlff.Text, ptzt11, Brushes.Black, new Point(x + 15+450, y1));
           e.Graphics.DrawLine(Pens.Black,x+15+ 552, y1 + 15,x+15+ 705, y1 + 15);
           y1 = y1 + 30;
           //1999年
           e.Graphics.DrawString("麻醉方法:" + comzfs.Text.Trim(), ptzt11, Brushes.Black, new Point(x+15, y1));
           e.Graphics.DrawLine(Pens.Black,x+15+ 72, y1 + 15,x+15+ 445, y1 + 15);
           e.Graphics.DrawString("麻醉医生:", ptzt11, Brushes.Black, new Point(x + 15+450, y1));
           e.Graphics.DrawLine(Pens.Black,x+15+ 522, y1 + 15,x+15+ 705, y1 + 15);




            int Y_unLine = y + 193, YY = y + 180;


           e.Graphics.DrawString(title, htc, Brushes.Black, new Point(140 + x, 70 + y));
           // y = y + 30; int y1 = y + 13;
           //e.Graphics.DrawString("科别：" + txbkeshi.Text, ptzt8, Brushes.Black, new Point(70 + x, YY+50));
           //e.Graphics.DrawLine(ptp, new Point(110 + x, Y_unLine + 50), new Point(250 + x, Y_unLine + 50));
           //e.Graphics.DrawString("姓名：" + txbname.Text, ptzt8, Brushes.Black, new Point(300 + x, YY + 50));
           //e.Graphics.DrawLine(ptp, new Point(330 + x, Y_unLine + 50), new Point(400 + x, Y_unLine + 50));
           //e.Graphics.DrawString("病历号：" + textzhuyuanhao.Text, ptzt8, Brushes.Black, new Point(450 + x, YY + 50));
           //e.Graphics.DrawLine(ptp, new Point(500 + x, Y_unLine + 50), new Point(630 + x, Y_unLine + 50));
           // e.Graphics.DrawString("入室时间：" + dtOtime.Value.ToString("HH:mm"), ptzt8, Brushes.Black, new Point(630 + x, YY + 50));
           // e.Graphics.DrawLine(ptp, new Point(680 + x, Y_unLine + 50), new Point(730 + x, Y_unLine + 50));
                
                Pen pred2 = new Pen(Brushes.Red, 2);
                Pen pblue2 = new Pen(Brushes.Blue, 2);
                DateTime dtnow = new DateTime();//打印截止时间判断        
                DateTime pagetime = new DateTime();
                DataTable dtMax = bll.GetMaxPointPacu(mzjldID);
                if (dtMax.Rows[0][0].ToString().IsNullOrEmpty())
                    dtnow = DateTime.Now;
                else
                    dtnow = Convert.ToDateTime(dtMax.Rows[0][0]);
                pagetime = dtOtime.Value; //当前打印页时间    
                DateTime shijian = new DateTime();
               shijian = dtOtime.Value;
                e.Graphics.DrawString("时间(分钟)", ptzt8, Brushes.Black, new Point(30 + x, YY+80 + 2));
                for (int i = 0; i < 7; i++) //打印检测时间
                {
                    e.Graphics.DrawString(shijian.ToString("HH:mm"), ptzt8, Brushes.Black, new Point(110 + 66 * i + x, YY + 80 + 2));
                    shijian = shijian.AddMinutes(6 * jcsjjg);
                }

                #region//打印检测区域格子。血压体温等区域↓
                YY = YY + 3*36;
                e.Graphics.DrawLine(ptp, new Point(35 + x, YY), new Point(35 + x, YY + 240));
                e.Graphics.DrawString("术\n\n中\n\n监\n\n测", ptzt8, Brushes.Black, new Point(21 + x, YY + 60));

                for (int i = 0; i < 12; i++)//画横实线           
                    e.Graphics.DrawLine(ptp, new Point(120 + x, YY + 20 * i), new Point(516 + x, YY + 20 * i + y));

                for (int i = 0; i < 12; i++)//画横虚线
                    e.Graphics.DrawLine(pxuxian, 120 + x, YY + 20 * i + 10, 516 + x, YY + 20 * i + y + 10);

                for (int i = 0; i < 7; i++)//画竖实线
                {
                    e.Graphics.DrawLine(ptp, new Point(120 + 66 * i + x, YY + y), new Point(120 + 66 * i + x, YY + 12 * 20 + y));
                }
                for (int i = 1; i < 19; i++)//画竖虚线
                {
                    e.Graphics.DrawLine(pxuxian, new Point(120 + 22 * i + x, YY + y), new Point(120 + 22 * i + x, YY + 12 * 20 + y));
                }
                for (int i = 1; i < 12; i++)
                    e.Graphics.DrawString((240 - i * 20).ToString(), ptzt7, Brushes.Black, new PointF(95 + x, YY + (float)20 * i + y - 5));
                //for (int i = 1; i < 12; i++)
                //    e.Graphics.DrawString((42 - i * 2).ToString(), ptzt7, Brushes.Black, new PointF(780 + x, YY + (float)20 * i + y - 5));
                DataTable dtMZ_Info = bll.SelectOldPatInfomenzhen(mzjldID);
           
                e.Graphics.DrawLine(ptp, new Point(526 + x, YY + 0), new Point(716 + x, YY + 0));
                e.Graphics.DrawLine(ptp, new Point(526 + x, YY +0), new Point(526 + x, YY + 240));
                e.Graphics.DrawLine(ptp, new Point(716 + x, YY + 0), new Point(716 + x, YY + 240));
                e.Graphics.DrawLine(ptp, new Point(526 + x, YY + 240), new Point(716 + x, YY + 240));
                e.Graphics.DrawString("术中用药", ptzt7, Brushes.Red, new Point(590 + x, YY + 5));
                e.Graphics.DrawString("0.9%N.S  " + texnsml.Text + " ml  " + Convert.ToDateTime(dtMZ_Info.Rows[0]["rushishijian"]).ToString("HH:mm"), ptzt7, Brushes.Black, new Point(540 + x, YY + 20));
                e.Graphics.DrawString(" 丙泊酚  " + texbbf.Text + " mg   " + Convert.ToDateTime(dtMZ_Info.Rows[0]["sskstime"]).ToString("HH:mm"), ptzt7, Brushes.Black, new Point(540 + x, YY + 35));
                e.Graphics.DrawString(comqtyp1.Text, ptzt7, Brushes.Black, new Point(540 + x, YY + 50));
                e.Graphics.DrawString(comqtyp2.Text, ptzt7, Brushes.Black, new Point(540 + x, YY + 65));
                e.Graphics.DrawString(comqtyp3.Text, ptzt7, Brushes.Black, new Point(540 + x, YY + 80));
                e.Graphics.DrawString(comqtyp4.Text, ptzt7, Brushes.Black, new Point(540 + x, YY + 95));
                e.Graphics.DrawString(comqtyp5.Text, ptzt7, Brushes.Black, new Point(540 + x, YY + 110));
                e.Graphics.DrawString("          氧气", ptzt7, Brushes.Red, new Point(40 + x, YY));
                e.Graphics.DrawString("∨收缩压", ptzt7, Brushes.Red, new Point(40 + x, YY + 20));
                e.Graphics.DrawString("∧舒张压", ptzt7, Brushes.Red, new Point(40 + x, YY + 35));
                e.Graphics.DrawString("●脉  搏", ptzt7, Brushes.Blue, new Point(40 + x, YY + 50));
                e.Graphics.DrawString("○呼  吸", ptzt7, Brushes.DarkCyan, new Point(40 + x, YY + 65));
              //  e.Graphics.DrawString("▽ SPO2", ptzt7, Brushes.DarkCyan, new Point(40 + x, YY + 80));
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
                //  e.Graphics.DrawString("△体  温", ptzt7, Brushes.DarkRed, new Point(40 + x, YY + 170));
                //e.Graphics.DrawString("▽ ETCO2", ptzt7, Brushes.DarkOrange, new Point(40 + x, YY + 185));
                #endregion
                #region  //打印氧气
                DataTable dtMzjld = bll.selectmzjldmenzhenid(mzjldID);
                if (dtMzjld.Rows[0]["yangqijiesushijian"].ToString() != "")
                {
                    yangqijssj = Convert.ToDateTime(dtMzjld.Rows[0]["yangqijiesushijian"]);
                    TimeSpan t = new TimeSpan();
                    t = yangqijssj - otime;
                    TimeSpan t1 = new TimeSpan();
                    t1 = yangqijssj - otime;
                    // xxx = (t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) + 170;

                    int x1 = (int)(120+x);
                   // int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 15 / jcsjjg + 171);
                    int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 11 / jcsjjg + 120) + x;
                    //        if (x1 > 170)
                    //        {
                    e.Graphics.DrawString("100%", ptzt6, Brushes.Blue, new Point(x1 + 3, YY - 6));
                    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, YY), new Point(x1 - 3, 6 + YY), new Point(x1 + 3, YY + 6 ) });
                    //        }
                    //        if (x2 - x1 > 5 && mz.Bz == 1)
                    //        {
                    //            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1));
                    //            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1 + 10));
                    //        }
                    //        else if (x2 > 170 && x2 - x1 > 5 && mz.Bz == 2)
                    //        {
                    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, YY), new Point(x2 - 3, YY + 6), new Point(x2 + 3, YY + 6) });
                    e.Graphics.DrawLine(pred2, new Point(x1, YY + 3), new Point(x2, YY + 3));
                    //double qtzongliang=(mz.Yl) *Convert.ToDouble( (x2 - x1)/3.27);
                    //e.Graphics.DrawString(Convert.ToInt32(qtzongliang).ToString() + "L", this.Font, Brushes.Blue, new Point(960, y1 - 2));
                    //MessageBox.Show(mz.Dw.Split('/').ToString());
                    //        }
                    //        if (x2 - x1 > 5)
                    //        {
                    //if (x1 > 170 && x2 > 170)
                    //{
                    //    e.Graphics.DrawLine(pred2, new Point(x1, y1 + 5), new Point(x2, y1 + 5));
                    //}
                }
                

                #endregion
                int spo2_count = 0;
                e.Graphics.DrawString("Spo2", ptzt7, Brushes.Black, new PointF(75 + x, YY +12));
                foreach (adims_MODEL.point p in spo2List)
                {
                    if (p.V > 0)
                    {
                        if (p.D >= pagetime && p.D <= pagetime.AddMinutes(60 * jcsjjg))
                        {
                            TimeSpan t = new TimeSpan();
                            t = p.D - pagetime;
                            float jhx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120);
                            float jhy = YY + 12;
                            if (spo2_count % 2 == 0)
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
                            spo2_count++;
                        }
                    }


                }
                #region  //打印收缩压
                float px = 0, py = 0;
                Pen p_red2 = new Pen(Brushes.Red, 2);
                if (checkssy.Checked == false)
                {
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
                }
                #endregion

                #region  //打印舒张压
                px = 0; py = 0;
                if (checkszy.Checked == false)
                {
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
                }
                #endregion

                #region  //打印脉搏
                px = 0; py = 0;
                if (checkmb.Checked == false)
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
                px = 0; py = 0; 
               
                if (checkhx.Checked == false)
                {

                    foreach (adims_MODEL.point p in hxlList)
                    {
                        if (p.D >= pagetime && p.D <= pagetime.AddMinutes(60 * jcsjjg))
                        {
                            TimeSpan t = new TimeSpan();
                            t = p.D - pagetime;
                            float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 139);
                            //float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 112);
                            //float pointy = (float)((220 - p.V) * 1 + 460);
                            float pointy = 0;
                            if (p.V > 230)
                            {
                                pointy = (float)((20) * 1 + YY);
                                e.Graphics.DrawString(p.V.ToString(), ptzt7, Brushes.Blue, pointx + x, pointy + y);
                            }
                            else
                                pointy = (float)((240 - p.V) * 1 + YY);
                            e.Graphics.DrawEllipse(Pens.DarkCyan, pointx - 2, pointy - 2, 5, 5);
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

                #region  //打印spo2
                //px = 0; py = 0;
                //foreach (adims_MODEL.point p in spo2List)
                //{
                //    if (p.D >= pagetime && p.D <= pagetime.AddMinutes(60 * jcsjjg))
                //    {
                //        TimeSpan t = new TimeSpan();
                //        t = p.D - pagetime;
                //        float pointx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 11 / jcsjjg + 120);
                //        float pointy = 0;
                //        if (p.V > 230)
                //        {
                //            pointy = (float)((20) * 1 + YY);
                //            e.Graphics.DrawString(p.V.ToString(), ptzt7, Brushes.Blue, pointx + x, pointy + y);
                //        }
                //        else
                //            pointy = (float)((240 - p.V) * 1 + YY);

                //        e.Graphics.DrawPolygon(Pens.DarkOrange, new PointF[3] { new PointF(pointx+ x, pointy+ y), 
                //                   new PointF(pointx - 3+ x, pointy - 5+ y), new PointF(pointx + 3+ x, pointy - 5+ y) });
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
                e.Graphics.DrawLine(ptp, new Point(20 + x, YY), new Point(516 + x, YY));
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
                    e.Graphics.DrawImage(newImage, new Rectangle(xxx, YY + 3, 10, 10));
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


                e.Graphics.DrawString("术中记录" , ptzt11, Brushes.Black, new Point(5 + x, YY + 33));
                e.Graphics.DrawLine(ptp, new Point(70 + x, YY + 50), new Point(750 + x, YY + 50));
                e.Graphics.DrawLine(ptp, new Point(70 + x, YY + 72), new Point(750 + x, YY + 72));
                string str1_zd = "";
                int StrLength_zd = textszjl.Text.Trim().Length;
                int row_zd = StrLength_zd / 40;

                for (int i = 0; i <= row_zd; )//50个字符就换行
                {
                    if (i < row_zd)
                        str1_zd = textszjl.Text.ToString().Substring(i * 40, 40); //从第i*50个开始，截取50个字符串
                    else
                        str1_zd = textszjl.Text.ToString().Substring(i * 40);
                    e.Graphics.DrawString(str1_zd, ptzt11, Brushes.Black, x + 80, YY + 33);
                    i++;
                    if (i > row_zd)
                    {

                    }
                    else
                    {
                        YY = YY + 20;
                    }

                }

                e.Graphics.DrawString("离室前评估", htc, Brushes.Black, new Point(300 + x, YY + 93));
                e.Graphics.DrawString("意识达到术前水平：", ptzt11, Brushes.Black, new Point(45 + x, YY + 123));
                e.Graphics.DrawRectangle(Pens.Black, x + 205, YY + 123, 10, 10);
                if (checkysspyes.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 205, YY + 123, x + 210, YY + 123 + 10);
                    e.Graphics.DrawLine(pblue2, x + 210, YY + 123 + 10, x + 215, YY + 123);
                }
                e.Graphics.DrawString("是", ptzt10, Brushes.Black, new Point(x + 215, YY + 123));
                e.Graphics.DrawRectangle(Pens.Black, x + 250, YY + 123, 10, 10);
                if (checkysspno.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 250, YY + 123, x + 255, YY + 123 + 10);
                    e.Graphics.DrawLine(pblue2, x + 255, YY + 123 + 10, x + 260, YY + 123);
                }
                e.Graphics.DrawString("否", ptzt10, Brushes.Black, new Point(x + 260, YY + 123));

                e.Graphics.DrawString("吸空气10分钟最低SPO2：" + textspo2.Text+"%", ptzt11, Brushes.Black, new Point(350 + x, YY + 123));
                e.Graphics.DrawString("恶心呕吐：", ptzt11, Brushes.Black, new Point(45 + x, YY + 153));
                e.Graphics.DrawRectangle(Pens.Black, x + 125, YY + 153, 10, 10);
                if (exoutuyou.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 125, YY + 153, x + 130, YY + 153 + 10);
                    e.Graphics.DrawLine(pblue2, x + 130, YY + 153 + 10, x + 135, YY + 153);
                }
                e.Graphics.DrawString("有", ptzt10, Brushes.Black, new Point(x + 135, YY + 153));
                e.Graphics.DrawRectangle(Pens.Black, x + 160, YY + 153, 10, 10);
                if (exoutuwu.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 160, YY + 153, x +165, YY + 153 + 10);
                    e.Graphics.DrawLine(pblue2, x + 165, YY + 153 + 10, x + 170, YY + 153);
                }
                e.Graphics.DrawString("无", ptzt10, Brushes.Black, new Point(x + 170, YY + 153));

                e.Graphics.DrawString("VAS评分： " + textvaspf.Text + " 分，         Steward评分： " + textsteward.Text+" 分", ptzt11, Brushes.Black, new Point(250 + x, YY + 153));

                e.Graphics.DrawString("复苏医生：                 " +"复苏护士： ", ptzt11, Brushes.Black, new Point(330 + x, YY + 183));

                string str1_zd1 = "";
                int StrLength_zd1 = texttsqk.Text.Trim().Length;
                int row_zd1 = StrLength_zd1 / 40;

                for (int i = 0; i <= row_zd1; )//50个字符就换行
                {
                    if (i < row_zd1)
                        str1_zd1 = texttsqk.Text.ToString().Substring(i * 40, 40); //从第i*50个开始，截取50个字符串
                    else
                        str1_zd1 = texttsqk.Text.ToString().Substring(i * 40);
                    e.Graphics.DrawString(str1_zd1, ptzt11, Brushes.Black, x + 50, YY + 183+ 60);
                    i++;
                    if (i > row_zd1)
                    {

                    }
                    else
                    {
                        YY = YY + 30;
                    }

                }
                e.Graphics.DrawLine(ptp, new Point(50 + x, YY + 183 + 50), new Point(750 + x, YY + 183 + 50));
                e.Graphics.DrawLine(ptp, new Point(50 + x, YY + 183 + 80), new Point(750 + x, YY + 183 + 80));
                e.Graphics.DrawLine(ptp, new Point(50 + x, YY + 183 + 110), new Point(750 + x, YY + 183 + 110));
                e.Graphics.DrawLine(ptp, new Point(50 + x, YY + 183 + 140), new Point(750 + x, YY + 183 + 140));
                e.Graphics.DrawLine(ptp, new Point(50 + x, YY + 183 + 170), new Point(750 + x, YY + 183 + 170));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dtMZ_Info = bll.SelectOldPatInfomenzhen(mzjldID);
            if (dtMZ_Info.Rows[0]["ssjstime"].ToString().IsNullOrEmpty())
            {
                MessageBox.Show("手术结束时间不能为空禁止打印");
                return;
            }
            if (Convert.ToDateTime(dtMZ_Info.Rows[0]["ssjstime"]) < Convert.ToDateTime(dtMZ_Info.Rows[0]["rushishijian"]))
            {
                MessageBox.Show("手术结束时间小于入室时间禁止打印");
                return;
            }
            int fy = 0;
            ptime = fristopen;
            PrintDialog pd = new PrintDialog();

           // pd.Document = printDocument;

            if (DialogResult.OK == pd.ShowDialog()) //如果确认，将会覆盖所有的打印参数设置
            {
                //页面设置对话框（可以不使用，其实PrintDialog对话框已提供页面设置）
                PageSetupDialog psd = new PageSetupDialog();
               //psd.Document = printDocument;
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
            iYema = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            save();
        }
        private int save()
        {
            if (!IsInNet())
            {
                return 0;
            }
            int result = 0;
            List<string> mzdList1 = new List<string>();
            string AddItem = "";
            if (checkysspyes.Checked == true) AddItem += "1";
            if (checkysspno.Checked == true) AddItem += "0";
            mzdList1.Add(AddItem);
            
            mzdList1.Add(this.textspo2.Text.Trim());
             AddItem = "";
            if (exoutuyou.Checked == true) AddItem += "1";
            if (exoutuwu.Checked == true) AddItem += "0";
            mzdList1.Add(AddItem);
            mzdList1.Add(textvaspf.Text.Trim());
            mzdList1.Add(this.textsteward.Text.Trim());
            mzdList1.Add(this.fsys.Text.Trim());
            mzdList1.Add(this.comfuhs.Text.Trim());
            mzdList1.Add(this.texttsqk.Text.Trim());
            mzdList1.Add(this.textszjl.Text.Trim());
            mzdList1.Add(this.texnsml.Text.Trim());
            mzdList1.Add(this.texbbf.Text.Trim());
            mzdList1.Add(this.comqtyp1.Text.Trim());
            mzdList1.Add(this.comqtyp2.Text.Trim());
            mzdList1.Add(comqtyp3.Text.Trim());
            mzdList1.Add(comqtyp4.Text.Trim());
            mzdList1.Add(comqtyp5.Text.Trim());
            mzdList1.Add(comqtyp6.Text.Trim());
           
            AddItem = "";
            if (cheboxBIS.Checked == true) AddItem += "1";
            if (checktw.Checked == true) AddItem += "2";
            if (checkmb.Checked == true) AddItem += "3";
            if (checkssy.Checked == true) AddItem += "4";
            if (checkszy.Checked == true) AddItem += "5";
            if (checkhx.Checked == true) AddItem += "6";
            mzdList1.Add(AddItem);
            mzdList1.Add(Convert.ToString(mzjldID));
            result = dal.UpdateMzjld1menzhen(mzdList1);
            if (result > 0)
            {
                MessageBox.Show("保存成功");
            }
            return result;
        }

        private void BindMzjldBacicInfo()//加载麻醉记录单信息
        {
            DataTable dtMzjld = bll.selectmzjldmenzhen(mzjldID);
            if (Convert.ToString(dtMzjld.Rows[0]["yisidadsqsp"]).Contains("1")) checkysspyes.Checked = true;
            if (Convert.ToString(dtMzjld.Rows[0]["yisidadsqsp"]).Contains("0")) checkysspno.Checked = true;
            textspo2.Text = Convert.ToString(dtMzjld.Rows[0]["spo2zd"]);
            if (Convert.ToString(dtMzjld.Rows[0]["exin"]).Contains("1")) exoutuyou.Checked = true;
            if (Convert.ToString(dtMzjld.Rows[0]["exin"]).Contains("0")) exoutuwu.Checked = true;
            textvaspf.Text = Convert.ToString(dtMzjld.Rows[0]["vaspingfen"]);
            textsteward.Text = Convert.ToString(dtMzjld.Rows[0]["steward"]);
            fsys.Text = Convert.ToString(dtMzjld.Rows[0]["fsys"]);
            comfuhs.Text = Convert.ToString(dtMzjld.Rows[0]["fshs"]);
            texttsqk.Text = Convert.ToString(dtMzjld.Rows[0]["tesqingkang"]);
            textszjl.Text = Convert.ToString(dtMzjld.Rows[0]["shuzhongjl"]);
            texnsml.Text = Convert.ToString(dtMzjld.Rows[0]["lhuana"]);
            texbbf.Text = Convert.ToString(dtMzjld.Rows[0]["bbf"]);
            comqtyp1.Text = Convert.ToString(dtMzjld.Rows[0]["qitayao1"]);
            comqtyp2.Text = Convert.ToString(dtMzjld.Rows[0]["qitayao2"]);
            comqtyp3.Text = Convert.ToString(dtMzjld.Rows[0]["qitayao3"]);
            comqtyp4.Text = Convert.ToString(dtMzjld.Rows[0]["qitayao4"]);
            comqtyp5.Text = Convert.ToString(dtMzjld.Rows[0]["qitayao5"]);
            comqtyp6.Text = Convert.ToString(dtMzjld.Rows[0]["qitayao6"]);
            if (Convert.ToString(dtMzjld.Rows[0]["isxs"]).Contains("1")) cheboxBIS.Checked = true;
            if (Convert.ToString(dtMzjld.Rows[0]["isxs"]).Contains("2")) checktw.Checked = true;
            if (Convert.ToString(dtMzjld.Rows[0]["isxs"]).Contains("3")) checkmb.Checked = true;
            if (Convert.ToString(dtMzjld.Rows[0]["isxs"]).Contains("4")) checkssy.Checked = true;
            if (Convert.ToString(dtMzjld.Rows[0]["isxs"]).Contains("5")) checkszy.Checked = true;
            if (Convert.ToString(dtMzjld.Rows[0]["isxs"]).Contains("6")) checkhx.Checked = true;
  
        }

        private void 添加采集时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menzhencj f2 = new menzhencj(mzjldID, dtOtime.Value.ToString("yyyy-MM-dd HH:mm"), cmbSJJG.Text);
            f2.ShowDialog();
            BindJHDian();
           
            this.pictureBox3.Refresh();
        }

        private void pictureBox3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.X > 0 && e.X < 169)
            {
                if (e.Y > 0 && e.Y < 15)//肖写的气体在第一行的氧气.
                {
                    addQty qt = new addQty(mzjldID);
                    qt.ShowDialog();
                    //BindQtList();
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Interval = 1000 * 60 * int.Parse(cmbSJJG.Text);//改变timer的间隔时间            
            Random rd = new Random();//随机模拟数据
            DateTime time = DateTime.Now;
            int xueya1 = rd.Next(60, 80);
            int xueya2 = rd.Next(100, 150);
            //string xueya = xueya1.ToString() + "/" + xueya2.ToString();
            int miaobo = rd.Next(60, 90);
            int huxi = rd.Next(20, 60);
            int spo2 = rd.Next(90, 100);
            int xx = bll.AddPACU_menzhen3333(mzjldID,time,xueya2,xueya1, huxi, miaobo, spo2);
            BindJHDian();
            pictureBox3.Refresh();
        }

        private void BindMZSSCGBG()//绑定麻醉手术插管拔管
        {
            DataTable dtMzjld = bll.selectmzjldmenzhenid(mzjldID);
            if (dtMzjld.Rows[0]["rushishijian"].ToString() != "")
            {
                DateTime ssDate = (Convert.ToDateTime(dtMzjld.Rows[0]["rushishijian"]));
                dtOtime.Value = otime;
            }
            if (dtMzjld.Rows[0]["mzkstime"].ToString() != "")
            {
                mzksTime = Convert.ToDateTime(dtMzjld.Rows[0]["mzkstime"]);
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
            //if (dtMzjld.Rows[0]["yangqijiesushijian"].ToString() != "")
            //{
            //    yangqijssj = Convert.ToDateTime(dtMzjld.Rows[0]["yangqijiesushijian"]);
            //    TimeSpan t = new TimeSpan();
            //    t = yangqijssj - otime;
            //    xxx = (t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) + 170;
            //    if ((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) > 0)
            //    {


            //        //lbMzkszgn1.Visible = true;
            //        //lbMzkszgn1.Text = "X1";
            //        //lbMzkszgn1.AutoSize = true;
            //        //lbMzkszgn1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //        //lbMzkszgn1.BackColor = Color.Transparent;
            //        //lbMzkszgn1.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 160), 370);
            //        //this.pictureBox3.Controls.Add(lbMzkszgn1);
            //        //mzks = true;
            //        //mzjs = false;
            //        //lbMzkszgn1.MouseDown += new MouseEventHandler(lbMzkszgn1_MouseDown);
            //        //lbMzkszgn1.MouseMove += new MouseEventHandler(lbMzkszgn1_MouseMove);
            //        //lbMzkszgn1.MouseUp += new MouseEventHandler(lbMzkszgn1_MouseUp);
            //        //lbMzkszgn1.MouseLeave += new EventHandler(lbMzkszgn1_MouseLeave);
            //    }

            //}
            //if (dtMzjld.Rows[0]["mzkssjzz"].ToString() != "")
            //{
            //    mzjkssjzzTime = Convert.ToDateTime(dtMzjld.Rows[0]["mzkssjzz"]);
            //    TimeSpan t = new TimeSpan();
            //    t = mzjkssjzzTime - otime;
            //    if ((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) > 0)
            //    {
            //        mzkssjzz.Visible = true;
            //        mzkssjzz.Text = "X2";
            //        mzkssjzz.AutoSize = true;
            //        mzkssjzz.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //        mzkssjzz.BackColor = Color.Transparent;
            //        mzkssjzz.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 15 / jcsjjg + 160), 370);
            //        this.pictureBox3.Controls.Add(mzkssjzz);
            //        mzks = true;
            //        mzjs = false;
            //        mzkssjzz.MouseDown += new MouseEventHandler(mzkssjzz_MouseDown);
            //        mzkssjzz.MouseMove += new MouseEventHandler(mzkssjzz_MouseMove);
            //        mzkssjzz.MouseUp += new MouseEventHandler(mzkssjzz_MouseUp);
            //        mzkssjzz.MouseLeave += new EventHandler(mzkssjzz_MouseLeave);
            //    }

            //}
            if (dtMzjld.Rows[0]["mzjstime"].ToString() != "")
            {
                mzjsTime = Convert.ToDateTime(dtMzjld.Rows[0]["mzjstime"]);
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
            if (dtMzjld.Rows[0]["sskstime"].ToString() != "")
            {
                ssksTime = Convert.ToDateTime(dtMzjld.Rows[0]["sskstime"]);
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
            if (dtMzjld.Rows[0]["ssjstime"].ToString() != "")
            {
                ssjsTime = Convert.ToDateTime(dtMzjld.Rows[0]["ssjstime"]);
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
            if (dtMzjld.Rows[0]["cgkstime"].ToString() != "")
            {
                cgTime = Convert.ToDateTime(dtMzjld.Rows[0]["cgkstime"]);
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
            if (dtMzjld.Rows[0]["bgjstime"].ToString() != "")
            {
                bgTime = Convert.ToDateTime(dtMzjld.Rows[0]["bgjstime"]);
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

        private void 结束氧气ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dal.Updateyangqijismenzhen(DateTime.Now, mzjldID);
            this.pictureBox3.Refresh();
        }

        private void 修改氧气结束时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            修改麻醉门诊氧气结束时间 ac=new 修改麻醉门诊氧气结束时间(mzjldID);
            ac.ShowDialog();
            this.pictureBox3.Refresh();
        }

        private void 修改麻醉开始时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            麻醉开始时间 ac = new 麻醉开始时间(mzjldID);
            ac.ShowDialog();
            BindMZSSCGBG();
            this.pictureBox3.Refresh();
        }

        private void 修改手术结束时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            手术结束时间 ac = new 手术结束时间(mzjldID);
            ac.ShowDialog();
            BindMZSSCGBG();
            this.pictureBox3.Refresh();
        }

        private void 修改手术开始时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            手术开始时间 ac = new 手术开始时间(mzjldID);
            ac.ShowDialog();
            BindMZSSCGBG();
            this.pictureBox3.Refresh();
        }
    }
}
