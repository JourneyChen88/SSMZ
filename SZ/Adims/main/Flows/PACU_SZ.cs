using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;
using main.PACU_LEVEL;
using System.IO.Ports;
using System.Threading;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.IO;
using MODEL;

using System.Runtime.InteropServices;
using WindowsFormsControlLibrary5;
using System.Net.NetworkInformation;
using System.Threading;
using adims_MODEL;
namespace main
{
    public partial class PACU_SZ : Form, IMessageFilter

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
        int TOF = 0; bool tof_effective = false;    //脉率
        int BIS = 0; bool bis_effective = false;    //脉率
        int RR = 0; bool rr_effective = false;    //呼吸频率
        float SPO2 = 0; bool spo2_effective = false;//血氧饱和度
        float TEMP = 0; bool temp_effective = false;//温度
        int HR = 0; bool hr_effective = false;    //心率
        int PR = 0; bool pr_effective = false;    //心率
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
        #region 《自定义参数》
        admin_T_SQL at = new admin_T_SQL();
        DateTime rsTime = new DateTime();//入室时间
        DateTime csTime = new DateTime();//出室时间
        DateTime cgTime = new DateTime();//插管时间
        DateTime bgTime = new DateTime();//拔管时间
        DateTime ksjcTime = new DateTime();//开始检测时间
        Label lab1 = new Label();//移动提醒标签
        int jcsjjg = 0;//检测时间间隔

        List<adims_MODEL.point> pointList = new List<adims_MODEL.point>();//检测点集合
        DateTime fristopen = new DateTime();//首页打印开始时间
        DateTime ptime = new DateTime();//每一页打印开始时间
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        adims_DAL.PACU_DAL pacuDal = new adims_DAL.PACU_DAL();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_MODEL.OldPBModel pat = new adims_MODEL.OldPBModel();
        Label rushiFlag = new Label();//入室标记
        Label chushiFlag = new Label();//出室标记
        Label cgFlag = new Label();//插管标记
        Label bgFlag = new Label();//拔管标记
        int mzjldID = 0;//麻醉记录单编号

        adims_MODEL.mzqt t_mzqt = new adims_MODEL.mzqt();//单个麻醉气体模板
        adims_MODEL.shuye t_shuye = new adims_MODEL.shuye();//单个输液模板
        adims_MODEL.shuxue t_shuxue = new adims_MODEL.shuxue();//单个输血模板
        adims_MODEL.shuxue t_yongyao = new adims_MODEL.shuxue();//单个用药模板

        adims_MODEL.clcxqt t_niaoliang = new adims_MODEL.clcxqt();//单个出尿模板
        adims_MODEL.tsyy t_tsyy = new adims_MODEL.tsyy();//单个特殊用药模板
        adims_MODEL.szsj t_szsj = new adims_MODEL.szsj();//单个特殊事件模板
        int flagP1 = 0, TypeP1 = 0;//图标选中标志，选中类型
        int PointCheckFlag = 0;//监测点选中类型
        int p3lf1 = 0, p3lf2 = 0;//入室出室插管拔管开始结束标签鼠标按下标志
        bool rushi = false, chushi = true, CGUAN = false, BGUAN = false;


        adims_MODEL.point p3t;//单个监测点模板
        int Pic1_X = 0; int Pic1_Y = 0;//鼠标在pictureBox1上的位置
        private List<string> jhxma = new List<string>();//所有监护项目
        private List<string> jhxmy = new List<string>();//已添加监护项目
        public List<adims_MODEL.point> ssy = new List<adims_MODEL.point>();//收缩的点
        public List<adims_MODEL.point> szy = new List<adims_MODEL.point>();//舒张压的点
        public List<adims_MODEL.point> mbo = new List<adims_MODEL.point>();//脉搏的点
        public List<adims_MODEL.point> etco2List = new List<adims_MODEL.point>();//
        public List<adims_MODEL.point> spo2List = new List<adims_MODEL.point>();//
        public List<adims_MODEL.point> xl = new List<adims_MODEL.point>();//心率
        public List<adims_MODEL.point> tw = new List<adims_MODEL.point>();//体温的点
        public List<adims_MODEL.point> hxl = new List<adims_MODEL.point>();//呼吸率的点
        public List<adims_MODEL.jhxm> jhxmv = new List<adims_MODEL.jhxm>();//监护项目值
        public List<adims_MODEL.szsj> sjList = new List<adims_MODEL.szsj>();//术中事件
        public List<adims_MODEL.clcxqt> cn = new List<adims_MODEL.clcxqt>();//出尿出血其他出量
        public List<adims_MODEL.mzqt> qtLIST = new List<adims_MODEL.mzqt>();//出尿出血其他出量
        public List<adims_MODEL.mzqt> mzqtList = new List<adims_MODEL.mzqt>();//麻醉气体药集合
        public List<adims_MODEL.shuye> sy1ist = new List<adims_MODEL.shuye>();//输液集合
        public List<adims_MODEL.shuxue> sx1ist = new List<adims_MODEL.shuxue>();//输血集合
        public List<adims_MODEL.tsyy> yyList = new List<adims_MODEL.tsyy>();//用药集合
        Point lastpoint = new Point();//画线的时候保存上一个点
        Point lastpoint1 = new Point();//画线的时候保存上一个点
        Point lastpoint2 = new Point();//画线的时候保存上一个点
        Point lastpoint3 = new Point();//画线的时候保存上一个点
        Point lastpoint4 = new Point();//画线的时候保存上一个点

        DateTime otime = new DateTime();//入室内时间

        string PatID, MzjldID;//病人编号，字符串麻醉编号
        DateTime MzjldMaxTime = new DateTime();//麻醉单检测最大时间，用于打印结束为止
        DateTime jkksTime = new DateTime();//机控开始时间
        DateTime jkjsTime = new DateTime();//机控结束时间，机控时间范围内才显示ETCO2的值
        #endregion


        #region IMessageFilter 成员          
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 522)
            { return true; }
            else
            { return false; }
        }
        #endregion
        public PACU_SZ(string patid, string mzid)
        {
            PatID = patid;
            MzjldID = mzid;
            InitializeComponent();
            GetConfigure();
            IP_address.Text = IPAddressInput1;
            //BedNumber1.Text = BedIDInput1;
            phillip_server = new IPEndPoint(IPAddress.Parse(IP_address.Text.Trim()), 24105);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Remote = new IPEndPoint(IPAddress.Any, 8001);
            phillip = (EndPoint)Remote;
        }
        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            int x = 0, y = 100;
            e.Graphics.DrawLine(Pens.Black, x, y, x, y + 800);//画第一条竖线
            e.Graphics.DrawLine(Pens.Black, x, y, x + 99, y);
            e.Graphics.DrawLine(Pens.Black, x, y + 25, x + 99, y + 25);
            for (int i = 0; i < 12; i++)
            {
                if (i == 7 || i == 9)
                    e.Graphics.DrawLine(Pens.Black, x + 150, y + 350 + i * 20, x + 99, y + 350 + i * 20);
                else
                    e.Graphics.DrawLine(Pens.Black, x, y + 350 + i * 20, x + 99, y + 350 + i * 20);
            }
            int jhy = 0;
            foreach (string s in jhxmy)
            {
                e.Graphics.DrawString(s, this.Font, Brushes.Black, new Point(x + 10, jhy * 20 + y + 375));
                jhy++;
            }
        }

        private void BindShuxueList()// 绑定输血集合
        {
            DataTable dtSx = pacuDal.GetsxPACU(mzjldID);
            int j = 0;
            sx1ist.Clear();
            foreach (DataRow dr in dtSx.Rows)
            {
                adims_MODEL.shuxue mzyt = new adims_MODEL.shuxue();
                sx1ist.Add(mzyt);
                sx1ist[j].Id = Convert.ToInt32(dr["id"]);
                sx1ist[j].Name = Convert.ToString(dr["shuxuename"]);
                sx1ist[j].Jl = Convert.ToDouble(dr["jl"]);
                sx1ist[j].Dw = Convert.ToString(dr["dw"]);
                sx1ist[j].Zrfs = Convert.ToString(dr["Zrfs"]);
                sx1ist[j].Kssj = Convert.ToDateTime(dr["kssj"]);
                sx1ist[j].Bz = Convert.ToInt32(dr["flags"]);
                if (sx1ist[j].Bz == 2)
                {
                    sx1ist[j].Jssj = Convert.ToDateTime(dr["jssj"]);
                }
                j++;
            }
        }

        private void BindShuyeList()// 绑定输液集合
        {
            DataTable dtSy = pacuDal.GetsyPACU(mzjldID);
            int j = 0;
            double zl = 0;
            sy1ist.Clear();
            foreach (DataRow dr in dtSy.Rows)
            {
                adims_MODEL.shuye mzyt = new adims_MODEL.shuye();
                sy1ist.Add(mzyt);
                sy1ist[j].Id = Convert.ToInt32(dr["id"]);
                sy1ist[j].Name = Convert.ToString(dr["shuyename"]);
                sy1ist[j].Jl = Convert.ToDouble(dr["jl"]);
                sy1ist[j].Dw = Convert.ToString(dr["dw"]);
                sy1ist[j].Zrfs = Convert.ToString(dr["Zrfs"]);
                sy1ist[j].Kssj = Convert.ToDateTime(dr["kssj"]);
                sy1ist[j].Bz = Convert.ToInt32(dr["flags"]);
                zl += sy1ist[j].Jl;
                if (sy1ist[j].Bz == 2)
                {
                    sy1ist[j].Jssj = Convert.ToDateTime(dr["jssj"]);
                }
                j++;
            }
            tbShuye.Text = zl.ToString();
        }

        private void BindClList()// 绑定出血集合
        {
            DataTable dtCL = pacuDal.GetclPACU(mzjldID);

            int i = 0;
            int zl = 0;
            cn.Clear();
            foreach (DataRow dr in dtCL.Rows)
            {
                adims_MODEL.clcxqt CLCXQT = new adims_MODEL.clcxqt();
                cn.Add(CLCXQT);
                cn[i].Id = Convert.ToInt32(dtCL.Rows[i]["id"]);
                cn[i].D = Convert.ToDateTime(dtCL.Rows[i][4]);
                cn[i].V = Convert.ToInt32(dtCL.Rows[i][3]);
                cn[i].Lx = Convert.ToInt32(dtCL.Rows[i][2]);
                zl += cn[i].V;
                i++;
            }
            tbNiaoliang.Text = zl.ToString();

        }

        private void BindQtList()// 绑定麻醉气体药集合
        {
            DataTable dtQT = pacuDal.GetqtPACU(mzjldID);
            int i = 0;
            mzqtList.Clear();
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

        private void BindPatInfo()// 绑定病人基本信息
        {
            DataTable dt = new DataTable();
            dt = dal.GetALLPAIBAN(PatID);
            tbPatname.Text = dt.Rows[0]["Patname"].ToString();
            tbZhuyuanID.Text = dt.Rows[0]["PatZhuYuanID"].ToString();
            tbBedNO.Text = dt.Rows[0]["Patbedno"].ToString();
            tbShoushuName.Text = dt.Rows[0]["Oname"].ToString();
            tbBingqu.Text = dt.Rows[0]["Patdpm"].ToString();
            tbSex.Text = dt.Rows[0]["patsex"].ToString();
            tbAge.Text = dt.Rows[0]["patage"].ToString();
            cmbAgeDW.Text = dt.Rows[0]["ageDW"].ToString();
            tbWeight.Text = dt.Rows[0]["patWeight"].ToString();
            DataTable dtMZ = dal.GetMZJLD_Info(MzjldID);
            if (dtMZ.Rows.Count > 0 && dtMZ.Rows[0]["ssss"].ToString() != "")
            {
                tbShoushuName.Text = dtMZ.Rows[0]["ssss"].ToString();
            }
        }

        private void BindShijian()// 绑定事件集合
        {
            sjList.Clear();
            DataTable dtSJ = pacuDal.GetPACUsj(mzjldID);//事件
            for (int i = 0; i < dtSJ.Rows.Count; i++)
            {
                adims_MODEL.szsj yysj = new adims_MODEL.szsj();
                yysj.Id = Convert.ToInt32(dtSJ.Rows[i]["id"]);
                yysj.D = Convert.ToDateTime(dtSJ.Rows[i]["time"]);
                yysj.Name = Convert.ToString(dtSJ.Rows[i]["name"]);
                sjList.Add(yysj);
            }
            int k = 1;
            listBox3.Items.Clear();
            foreach (adims_MODEL.szsj s in sjList)
            {
                listBox3.Items.Add(k.ToString() + "." + s.Name + " " + s.D.ToString("HH:mm"));
                k++;
            }
        }

        private void BindYongyao()// 绑定特殊用药集合
        {
            yyList.Clear();
            DataTable dtYY = pacuDal.GetPACUyy(mzjldID);//用药
            for (int i = 0; i < dtYY.Rows.Count; i++)
            {
                adims_MODEL.tsyy yysj = new adims_MODEL.tsyy();
                yysj.Id = Convert.ToInt32(dtYY.Rows[i]["id"]);
                yysj.D = Convert.ToDateTime(dtYY.Rows[i]["yytime"]);
                yysj.Name = Convert.ToString(dtYY.Rows[i]["yyname"]);
                yysj.Dw = Convert.ToString(dtYY.Rows[i]["dw"]);
                yysj.Yl = float.Parse(dtYY.Rows[i]["yl"].ToString());
                yysj.Yyfs = Convert.ToString(dtYY.Rows[i]["Yyfs"]);
                yyList.Add(yysj);
            }

            int j = 1;
            listBox1.Items.Clear();
            foreach (adims_MODEL.tsyy s in yyList)
            {
                listBox1.Items.Add(j.ToString() + "." + s.Name + " " + s.Yl + s.Dw + " " + s.D.ToString("HH:mm"));
                j++;
            }

        }

        private void BindShijiandian()//绑定画图时间点
        {
            jhxma.Clear();
            jhxma.Add("ECG");
            jhxma.Add("CVP");
            jhxma.Add("NIBP");
            jhxma.Add("ART");
            jhxma.Add("RESP");
            jhxma.Add("BIS");
            jhxma.Add("TOF");
            jcsjjg = Convert.ToInt32(cmbSJJG.Text.Trim());
            lbTime0.Text = otime.ToString("HH:mm");
            lbTime1.Text = otime.AddMinutes(6 * jcsjjg).ToString("HH:mm");
            lbTime2.Text = otime.AddMinutes(12 * jcsjjg).ToString("HH:mm");
            lbTime3.Text = otime.AddMinutes(18 * jcsjjg).ToString("HH:mm");
            lbTime4.Text = otime.AddMinutes(24 * jcsjjg).ToString("HH:mm");
            lbTime5.Text = otime.AddMinutes(30 * jcsjjg).ToString("HH:mm");
            lbTime6.Text = otime.AddMinutes(36 * jcsjjg).ToString("HH:mm");
            lbTime7.Text = otime.AddMinutes(42 * jcsjjg).ToString("HH:mm");

        }

        private void PACU_SZ_Load(object sender, EventArgs e)
        {

            try
            {
                Application.AddMessageFilter(this);

                this.txtMZYS.Controls[0].DoubleClick += new System.EventHandler(this.txtMZYS_DoubleClick);
                this.txtZXHS.Controls[0].DoubleClick += new System.EventHandler(this.txtZXHS_DoubleClick);
                this.txtMZYS.Controls[0].TextChanged += new System.EventHandler(this.txtMZYS_TextChanged);
                this.txtZXHS.Controls[0].TextChanged += new System.EventHandler(this.txtZXHS_TextChanged);
                BindPortName();
                mzjldID = Convert.ToInt32(MzjldID);
                BindPatInfo();
                DataTable dt = pacuDal.GetPACU(mzjldID);
                if (dt.Rows.Count == 0)//不存在病人PACU单就新建保存一份
                {
                    jcsjjg = 5;
                    cmbSJJG.Text = "5";
                    //CheckMzjldAndPacuData();//检查麻醉记录单最后检测时间与进入PACU时间
                    //if (DateTime.Now.Minute > 30)
                    //    otime = DateTime.Now.AddMinutes(-DateTime.Now.Minute + 30);
                    //else
                    //    otime = DateTime.Now.AddMinutes(-DateTime.Now.Minute);
                    DateTime Comparetime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    if (MzjldMaxTime > Comparetime)
                        otime = MzjldMaxTime.AddMinutes(-10);
                    else
                        otime = DateTime.Now.AddMinutes(-DateTime.Now.Minute);

                    fristopen = otime;
                    pacuDal.InsertPACU(mzjldID, otime, dtVisitDate.Value);
                    jhxmy.Clear();
                    jhxmy.Add("SpO2");
                    jhxmy.Add("ETCO2");
                    int f_jhxm = 0;
                    foreach (string name in jhxmy)
                    {
                        bll.addJhxmPACU(mzjldID, name);
                        f_jhxm++;
                    }
                    if (f_jhxm == 0)
                    {
                        MessageBox.Show("监护项目添加失败！");
                    }

                    BindShijiandian();//绑定时间坐标
                    BindJHDian();

                }
                else//存在病人PACU单就读取数据
                {
                    jhxmy.Clear();
                    DataTable dtJhxm = bll.GetJhxmPACU(mzjldID);
                    for (int i = 0; i < dtJhxm.Rows.Count; i++)
                    {
                        jhxmy.Add(dtJhxm.Rows[i][0].ToString());
                    }
                    otime = Convert.ToDateTime(dt.Rows[0]["otime"]);
                    fristopen = otime;
                    cmbSJJG.Text = Convert.ToString(dt.Rows[0]["jcsjjg"]);
                    jcsjjg = Convert.ToInt32(dt.Rows[0]["jcsjjg"]);
                    BindShijiandian();//绑定时间坐标
                    BindJikongTime();
                    BindRSCSCGBG();
                    BindClList();
                    BindQtList();
                    BindShuxueList();
                    BindShuyeList();
                    BindJHDian();
                    BindYongyao();
                    BindShijian();

                    dtVisitDate.Value = Convert.ToDateTime(dt.Rows[0]["SaveTime"]);
                    txtMZYS.Controls[0].Text = Convert.ToString(dt.Rows[0]["mzys"]);
                    txtZXHS.Controls[0].Text = Convert.ToString(dt.Rows[0]["zxhs"]);

                }
                this.WindowState = FormWindowState.Maximized;
                this.dtInRoomTime.Format = DateTimePickerFormat.Custom;
                this.dtInRoomTime.CustomFormat = "MM-dd HH:mm";
                dtInRoomTime.Value = otime;
            }
            catch (Exception ee)
            {

                MessageBox.Show(ee.ToString());
            }

        }

        private void CheckMzjldAndPacuData()//检查麻醉记录单检测点最大时间点和PACU最小时间点，用于截取模拟数据录入PACU
        {
            DataTable ServerMax = dal.GetMaxTimeServer(mzjldID);
            //DataTable LoaclMax = dal.GetMaxTimeLocal(mzjldID);

            DateTime PacuInTime = DateTime.Now;

            if (ServerMax.Rows[0][0].ToString() != "")
            {
                MzjldMaxTime = Convert.ToDateTime(ServerMax.Rows[0][0].ToString());
                MzjldMaxTime = MzjldMaxTime.AddMinutes(jcsjjg);
                int copycount = 0;
                TimeSpan t1 = PacuInTime - MzjldMaxTime;
                int TimePlus = t1.Days * 60 * 60 + t1.Hours * 60 + t1.Minutes;
                int DataCount = TimePlus / jcsjjg;
                int a = dal.CopyMzjldToPacu(MzjldMaxTime, mzjldID, DataCount);

            }
        }

        private void BindRSCSCGBG()//绑定入室出室插管拔管标记位置
        {
            DataTable dt = pacuDal.GetPACU(mzjldID);

            if (dt.Rows[0]["rssj"].ToString() != "")
            {
                rsTime = Convert.ToDateTime(dt.Rows[0]["rssj"]);
                TimeSpan t = new TimeSpan();
                t = rsTime - otime;
                rushiFlag.Text = "＞";
                rushiFlag.AutoSize = true;
                rushiFlag.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                rushiFlag.ForeColor = Color.Purple;
                rushiFlag.BackColor = Color.Transparent;
                rushiFlag.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100), 650);
                this.pictureBox1.Controls.Add(rushiFlag);
                rushi = true;
                chushi = false;
                //string zdming = "rssj";
                //DAL.Update_single_PACU_SZ(zdming, DateTime.Now, mzjldID);

                rushiFlag.MouseDown += new MouseEventHandler(rushiFlag_MouseDown);
                rushiFlag.MouseMove += new MouseEventHandler(rushiFlag_MouseMove);
                rushiFlag.MouseUp += new MouseEventHandler(rushiFlag_MouseUp);
                rushiFlag.MouseLeave += new EventHandler(rushiFlag_MouseLeave);
            }

            if (dt.Rows[0]["cssj"].ToString() != "")
            {
                csTime = Convert.ToDateTime(dt.Rows[0]["cssj"]);
                TimeSpan t = new TimeSpan();
                t = csTime - otime;
                chushiFlag.Text = "＜";
                chushiFlag.AutoSize = true;
                chushiFlag.BackColor = Color.Transparent;
                chushiFlag.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                chushiFlag.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100), 650);
                this.pictureBox1.Controls.Add(chushiFlag);
                chushi = true;

                chushiFlag.MouseDown += new MouseEventHandler(chushiFlag_MouseDown);
                chushiFlag.MouseMove += new MouseEventHandler(chushiFlag_MouseMove);
                chushiFlag.MouseUp += new MouseEventHandler(chushiFlag_MouseUp);
                chushiFlag.MouseLeave += new EventHandler(chushiFlag_MouseLeave);
            }
            if (dt.Rows[0]["cgsj"].ToString() != "")
            {
                cgTime = Convert.ToDateTime(dt.Rows[0]["cgsj"]);
                TimeSpan t = new TimeSpan();
                t = cgTime - otime;
                cgFlag.Text = "Θ";
                cgFlag.AutoSize = true;
                cgFlag.BackColor = Color.Transparent;
                cgFlag.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                cgFlag.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 90), 650);
                this.pictureBox1.Controls.Add(cgFlag);
                CGUAN = true;
                BGUAN = false;

                cgFlag.MouseDown += new MouseEventHandler(cgFlag_MouseDown);
                cgFlag.MouseMove += new MouseEventHandler(cgFlag_MouseMove);
                cgFlag.MouseUp += new MouseEventHandler(cgFlag_MouseUp);
                cgFlag.MouseLeave += new EventHandler(cgFlag_MouseLeave);
            }
            if (dt.Rows[0]["bgsj"].ToString() != "")
            {
                bgTime = Convert.ToDateTime(dt.Rows[0]["bgsj"]);
                TimeSpan t = new TimeSpan();
                t = bgTime - otime;
                bgFlag.Text = "Φ";
                bgFlag.AutoSize = true;
                bgFlag.BackColor = Color.Transparent;
                bgFlag.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                bgFlag.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 90), 650);
                this.pictureBox1.Controls.Add(bgFlag);
                BGUAN = true;
                //txtCGJS.Controls[0].Text = DateTime.Now.ToString("HH:mm");
                bgFlag.MouseDown += new MouseEventHandler(bgFlag_MouseDown);
                bgFlag.MouseMove += new MouseEventHandler(bgFlag_MouseMove);
                bgFlag.MouseUp += new MouseEventHandler(bgFlag_MouseUp);
                bgFlag.MouseLeave += new EventHandler(bgFlag_MouseLeave);
            }
        }
        SQLiteHelper sh = new SQLiteHelper();
        [DllImport("wininet")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reserverdaValue);
        private void timer1_Tick(object sender, EventArgs e)//检查网络是否连接
        {
            int isinnet;
            if (!InternetGetConnectedState(out isinnet, 0))
            {
                // timer1.Enabled = false;
                MessageBox.Show("服务器中断，请检查网线");
                return;
            }
            bool pingIP = adims_BLL.UserFunction.PingHost(Program.Globals.SeverIp);
            if (pingIP == true)
            {


                //int countN = dal.CopyDataPacu(mzjldID, ksjcTime);
                int countN = sh.CopyDataPacu(mzjldID, ksjcTime);//挑监护数据插入服务器的Adims_Pacu_Point表里
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
                    pictureBox1.Refresh();
                }
            }
            else
            {
                MessageBox.Show("网络中断请检查网络");
                return;
            }
        }

        public void BindJHDian()//监护点赋值
        {
            ssy.Clear(); szy.Clear(); xl.Clear(); tw.Clear(); hxl.Clear();
            mbo.Clear(); spo2List.Clear();
            etco2List.Clear();
            //RecordTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP
            DataTable datadt = dal.GetAdims_PACU_Point(mzjldID);
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.point p1 = new adims_MODEL.point();//收缩压记录点
                p1.D = Convert.ToDateTime(datadt.Rows[i][0]);
                p1.V = Convert.ToInt32(datadt.Rows[i][1]);
                p1.Lx = 1;
                ssy.Add(p1);
            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.point p2 = new adims_MODEL.point();//舒张压记录点
                p2.D = Convert.ToDateTime(datadt.Rows[i][0]);
                p2.V = Convert.ToInt32(datadt.Rows[i][2]);
                p2.Lx = 2;
                szy.Add(p2);
            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.point p3 = new adims_MODEL.point();//脉搏记录点
                p3.D = Convert.ToDateTime(datadt.Rows[i][0]);
                p3.V = Convert.ToInt32(datadt.Rows[i][3]);
                p3.Lx = 3;
                mbo.Add(p3);
            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.point p4 = new adims_MODEL.point();//呼吸记录点
                p4.D = Convert.ToDateTime(datadt.Rows[i][0]);
                p4.V = Convert.ToInt32(datadt.Rows[i][4]);
                p4.Lx = 4;
                hxl.Add(p4);
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
                adims_MODEL.jhxm jhxmt = new adims_MODEL.jhxm();
                jhxmt.D = Convert.ToDateTime(datadt.Rows[i][0]);
                jhxmt.V = Convert.ToInt32(datadt.Rows[i]["SPO2"]);
                jhxmt.Sy = "SpO2";
                jhxmv.Add(jhxmt);
            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.jhxm jhxmt = new adims_MODEL.jhxm();
                jhxmt.D = Convert.ToDateTime(datadt.Rows[i][0]);
                jhxmt.V = Convert.ToInt32(datadt.Rows[i]["ETCO2"]);
                jhxmt.Sy = "ETCO2";
                jhxmv.Add(jhxmt);
            }


            #region//模拟数据点
            ////↓查询收缩压记录点集合
            //DataTable ssydt = dal.GetALL_Point(mzjldID, 1);
            //for (int i = 0; i < ssydt.Rows.Count; i++)
            //{
            //    adims_MODEL.point p = new adims_MODEL.point();//收缩压记录点
            //    p.D = Convert.ToDateTime(ssydt.Rows[i][4]);
            //    p.V = Convert.ToInt32(ssydt.Rows[i][3]);
            //    p.Lx = 1;
            //    ssy.Add(p);
            //}

            ////↓查询舒张压压记录点集合
            //DataTable szydt = dal.GetALL_Point(mzjldID, 2);
            //for (int i = 0; i < szydt.Rows.Count; i++)
            //{
            //    adims_MODEL.point p1 = new adims_MODEL.point();//舒张压记录点
            //    p1.D = Convert.ToDateTime(szydt.Rows[i][4]);
            //    p1.V = Convert.ToInt32(szydt.Rows[i][3]);
            //    p1.Lx = 2;
            //    szy.Add(p1);

            //}
            ////↓查询心率记录点集合
            //DataTable xldt = dal.GetALL_Point(mzjldID, 3);
            //for (int i = 0; i < xldt.Rows.Count; i++)
            //{
            //    adims_MODEL.point p2 = new adims_MODEL.point();//心率记录点
            //    p2.D = Convert.ToDateTime(xldt.Rows[i][4]);
            //    p2.V = Convert.ToInt32(xldt.Rows[i][3]);
            //    p2.Lx = 3;
            //    xl.Add(p2);

            //}
            ////↓查询体温集合
            //DataTable twdt = dal.GetALL_Point(mzjldID, 4);
            //for (int i = 0; i < twdt.Rows.Count; i++)
            //{
            //    adims_MODEL.point p4 = new adims_MODEL.point();//体温记录点
            //    p4.D = Convert.ToDateTime(twdt.Rows[i][4]);
            //    p4.V = Convert.ToInt32(twdt.Rows[i][3]);
            //    p4.Lx = 4;
            //    tw.Add(p4);

            //}
            ////↓查询呼吸率记录点集合
            //DataTable hxdt = dal.GetALL_Point(mzjldID, 5);
            //for (int i = 0; i < hxdt.Rows.Count; i++)
            //{
            //    adims_MODEL.point p3 = new adims_MODEL.point();//呼吸率记录点

            //    p3.D = Convert.ToDateTime(hxdt.Rows[i][4]);
            //    p3.V = Convert.ToInt32(hxdt.Rows[i][3]);
            //    p3.Lx = 5;
            //    hxl.Add(p3);
            //}
            #endregion

        }


        #region //GE监护函数
        static EventHandler dataEvent;
        DSerialPort _serialPort = DSerialPort.getInstance;

        private void GE_Jianhuyi()
        {
            //timer4.Interval = 1000 * 10;
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
                (sender as DSerialPort).ReadBuffer(mzjldID, 1);
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

        private void FillipJianhuyi()
        {
            bool TimerStarted = false;
            string temp1 = IP_address.Text;
            //string temp2 = this.txtMzjldid.Controls[0].Text;
            IPAddressInput1 = IP_address.Text;
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
                    mRecord.RR = RR;
                    mRecord.HR = HR; ;
                    mRecord.Pulse = PR;
                    mRecord.SPO2 = (int)SPO2;
                    mRecord.ETCO2 = new Random().Next(32, 35);
                    mRecord.TEMP = TEMP;
                    mRecord.BIS = BIS;
                    mRecord.TOF = TOF;



                    this.SaveFillipData(mRecord, 1);

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
            FileStream fs = new FileStream("c:\\Config.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.WriteLine(IpConf.PatientIPAddress);
            sw.WriteLine(IpConf.BedID);
            sw.Close();
            fs.Close();
        }
        private void GetConfigure()
        {
            IPConfigureInfo IpConf;
            FileStream fs = new FileStream("c:\\Config.txt", FileMode.Open);
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


        private void BindPortName()//绑定机器的所有COM端口
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

        private void btnMonitor_Click(object sender, EventArgs e)
        {
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
                    GE_Jianhuyi();
                    cmbJianhuyi.Enabled = false;
                }
                if (cmbJianhuyi.Text == "飞利浦监护仪")
                {
                    FillipJianhuyi();
                    cmbJianhuyi.Enabled = false;
                }
                if (cmbJianhuyi.Text == "迈瑞监护仪")
                {
                    cmbJianhuyi.Enabled = false;
                }
                ksjcTime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                timer1.Interval = 1000 * 20;
                timer1.Enabled = true;
                btnMonitor.Text = "结束监测";
            }
            else
            {
                if (DialogResult.OK == MessageBox.Show("确定结束检测吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                {
                    timer1.Enabled = false;
                    btnMonitor.Text = "开始监测";
                    cmbJianhuyi.Enabled = true;
                    _serialPort.StopTransfer();//2014-05-13修改
                    _serialPort.Close();
                    if (ThreadExist)
                    {
                        ThreadExist = false;
                        Receiving_xy.Abort();//强制结束线程运行
                        Receiving_xy = null; ////在.NET中，有GC垃圾回收，如果这里不赋为null，有可能老半天也不回收内存，甚至永远这么占着，导致内存泄漏。
                    }
                }
            }
        }

        private void pictureBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.X > 0 && e.X < 100)
            {
                if (e.Y > 475 && e.Y < 575)//增加监护项目
                {
                    addjhxm fromjhxm = new addjhxm(jhxma, jhxmy, mzjldID, 1);
                    fromjhxm.ShowDialog();

                }
                pictureBox1.Refresh();
            }
            pictureBox2.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen pred2 = new Pen(Brushes.Red);
            pred2.Width = 2;
            Pen pxuxian = new Pen(Brushes.Black);
            pxuxian.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            int x_Line = 0, y_Line = 100;
            e.Graphics.DrawLine(Pens.Black, x_Line, y_Line, x_Line, y_Line + 770);//画第一条竖线
            e.Graphics.DrawLine(Pens.Black, x_Line + 1060, y_Line, x_Line + 1060, y_Line + 770);//画右边竖线
            e.Graphics.DrawLine(Pens.Black, x_Line, y_Line + 770, x_Line + 1060, y_Line + 770);//画底部横线

            for (int i = 0; i < 15; i++)//画横线
            {
                e.Graphics.DrawLine(Pens.Black, x_Line, y_Line + i * 25, x_Line + 1060, y_Line + i * 25);
            }
            for (int i = 1; i < 12; i++)//画横线
            {
                e.Graphics.DrawLine(Pens.Black, x_Line, y_Line + i * 20 + 350, x_Line + 1060, y_Line + i * 20 + 350);
            }
            for (int i = 0; i < 9; i++)//画竖线
            {
                e.Graphics.DrawLine(Pens.Black, x_Line + 100 + i * 120, y_Line, x_Line + 100 + i * 120, y_Line + 550);
            }
            for (int i = 0; i < 49; i++)//画竖线
            {
                e.Graphics.DrawLine(pxuxian, x_Line + 100 + i * 20 + 20, y_Line, x_Line + 100 + i * 20 + 20, y_Line + 550);
            }

            int dyd = 0, dyd1 = 0, dyd2 = 0, dyd3 = 0, dyd4 = 0;//标志是否是第一个点
            int dy = 0;//   气体数量初始值

            Pen pblack2 = new Pen(Brushes.Black);
            pblack2.Width = 2;
            #region  //画气体
            ArrayList ssQT = new ArrayList();
            foreach (adims_MODEL.mzqt mz in mzqtList)
            {
                if (mz.Bz > 0)
                {
                    //e.Graphics.DrawString(mz.Qtname, this.Font, Brushes.Black, new Point(10, 25 * (dy + 1) + 110));
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
                    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                    int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 20 / jcsjjg + 100);
                    int y1 = 110;
                    e.Graphics.DrawString(mz.Yl.ToString() + mz.Dw.ToString(), this.Font, Brushes.Blue, new Point(x1 - 10, y1 - 8));
                    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });

                    if (x2 - x1 > 5 && mz.Bz == 1)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1));
                        e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1 + 10));
                    }
                    else if (x2 - x1 > 5 && mz.Bz == 2)
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 5, y1 + 8), new Point(x2 + 5, y1 + 8) });

                    if (x2 - x1 > 5)
                        e.Graphics.DrawLine(pred2, new Point(x1, y1 + 5), new Point(x2, y1 + 5));

                    dy++;
                    ssQT.Add(mz.Qtname);
                }
            }
            #endregion

            #region  //画收缩压

            foreach (adims_MODEL.point tp in ssy)
            {
                int x, y;
                TimeSpan t = new TimeSpan();
                t = tp.D - otime;
                x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                if (tp.V > 251)
                {
                    y = 450 - 312;
                    e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Red, x, y);
                }
                else
                    y = (int)(450 - (int)(tp.V * 1.25));

                e.Graphics.DrawPolygon(Pens.Red, new Point[3] { new Point(x, y), new Point(x - 5, y - 5), new Point(x + 5, y - 5) });


                if (MousePosition.X == x && MousePosition.Y == y)
                {

                    e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Red, x, y);
                }

                if (dyd != 0)
                    e.Graphics.DrawLine(Pens.Red, new Point(x, y), lastpoint);
                lastpoint.X = x;
                lastpoint.Y = y;
                dyd++;
            }
            #endregion

            #region  //画舒张压
            foreach (adims_MODEL.point tp in szy)
            {
                int x, y;
                TimeSpan t = new TimeSpan();
                t = tp.D - otime;
                x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                if (tp.V > 251)
                {
                    y = 450 - 312;
                    e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Red, x, y);
                }
                else
                    y = (int)(450 - (int)(tp.V * 1.25));
                e.Graphics.DrawPolygon(Pens.Red, new Point[3] { new Point(x, y), new Point(x + 5, y + 5), new Point(x - 5, y + 5) });
                if (dyd1 != 0)
                    e.Graphics.DrawLine(Pens.Red, new Point(x, y), lastpoint1);
                lastpoint1.X = x;
                lastpoint1.Y = y;
                dyd1++;
            }
            #endregion

            #region  //画体温
            foreach (adims_MODEL.point tp in tw)
            {
                int x, y;
                TimeSpan t = new TimeSpan();
                t = tp.D - otime;
                x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                if (tp.V > 251)
                {
                    y = 450 - 312;
                    e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Green, x, y);
                }
                else
                    y = (int)(450 - (int)(tp.V * 1.25));
                e.Graphics.FillRectangle(Brushes.Green, new Rectangle(x - 3, y - 3, 6, 6));
                if (dyd2 != 0)
                    e.Graphics.DrawLine(Pens.Green, new Point(x, y), lastpoint2);
                lastpoint2.X = x + 3;
                lastpoint2.Y = y;
                dyd2++;
            }
            #endregion

            #region  //画心率
            foreach (adims_MODEL.point tp in mbo)
            {
                int x, y;
                TimeSpan t = new TimeSpan();
                t = tp.D - otime;
                x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                if (tp.V > 251)
                {
                    y = 450 - 312;
                    e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Blue, x, y);
                }
                else
                    y = (int)(450 - (int)(tp.V * 1.25));

                e.Graphics.FillEllipse(Brushes.Blue, new Rectangle(x - 5, y - 5, 10, 10));
                if (dyd3 != 0)
                    e.Graphics.DrawLine(Pens.Blue, new Point(x - 5, y), lastpoint3);
                lastpoint3.X = x + 5;
                lastpoint3.Y = y;
                dyd3++;
            }
            #endregion

            #region  //画呼吸率
            Font ptzt7 = new Font("宋体", 7);
            foreach (adims_MODEL.point tp in hxl)
            {
                int x, y;
                TimeSpan t = new TimeSpan();
                t = tp.D - otime;
                x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                if (tp.V > 251)
                {
                    y = 450 - 312;
                    e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.DarkCyan, x, y);
                }
                else
                    y = (int)(450 - (int)(tp.V * 1.25));

                if (jkksTime < tp.D && jkjsTime > tp.D)
                    e.Graphics.DrawString("CR", ptzt7, Brushes.DarkCyan, x - 5, y - 5);
                else
                    e.Graphics.DrawEllipse(Pens.DarkCyan, new Rectangle(x - 3, y - 3, 6, 6));
                if (dyd4 != 0)
                    e.Graphics.DrawLine(Pens.DarkCyan, new Point(x - 3, y), lastpoint4);
                lastpoint4.X = x + 3;
                lastpoint4.Y = y;
                dyd4++;
            }
            #endregion

            #region  //画监护项目
            int jhi = 0;
            Font fsmall = new Font("宋体", 7);
            foreach (string s in jhxmy)
            {
                e.Graphics.DrawString(s, this.Font, Brushes.Black, new Point(30, jhi * 20 + 2));
                foreach (adims_MODEL.jhxm jt in jhxmv)
                {
                    if (jt.Sy == s)
                    {
                        TimeSpan t = new TimeSpan();
                        t = jt.D - otime;
                        int jhx = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 90);
                        if (jt.Sy == "ETCO2")
                        {
                            if (CGUAN && cgTime < jt.D && !BGUAN)
                            {
                                e.Graphics.FillRectangle(Brushes.Pink, jhx, jhi * 20 + 475, 14, 12);
                                e.Graphics.DrawString(jt.V.ToString(), (jt.V / 100 > 0 ? fsmall : this.Font), Brushes.Black, new Point((jt.V / 100 > 0 ? jhx - 2 : jhx), jhi * 20 + 475));


                            }
                            if (BGUAN && cgTime < jt.D && jt.D < bgTime)
                            {
                                e.Graphics.FillRectangle(Brushes.Pink, jhx, jhi * 20 + 475, 14, 12);
                                e.Graphics.DrawString(jt.V.ToString(), (jt.V / 100 > 0 ? fsmall : this.Font), Brushes.Black, new Point((jt.V / 100 > 0 ? jhx - 2 : jhx), jhi * 20 + 475));

                            }
                        }
                        else if (jt.Sy != "ETCO2")
                        {
                            e.Graphics.FillRectangle(Brushes.Pink, jhx, jhi * 20 + 475, 14, 12);
                            e.Graphics.DrawString(jt.V.ToString(), (jt.V / 100 > 0 ? fsmall : this.Font), Brushes.Black, new Point((jt.V / 100 > 0 ? jhx - 2 : jhx), jhi * 20 + 475));
                        }
                    }

                }
                jhi++;
            }
            #endregion

            #region  //画出尿
            int clCOUNT = 1;
            foreach (adims_MODEL.clcxqt cl in cn)
            {
                TimeSpan t = new TimeSpan();
                t = cl.D - otime;
                int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                e.Graphics.FillRectangle(Brushes.Pink, x1, 455, 20, 12);
                e.Graphics.DrawString(cl.V.ToString(), this.Font, Brushes.Black, new Point(x1, 455));
                clCOUNT++;
            }
            #endregion

            #region  //画输血
            int sxi = 0;
            foreach (adims_MODEL.shuxue sx in sx1ist)//画输血
            {
                if (sx.Bz > 0)
                {
                    e.Graphics.DrawString(sx.Name, this.Font, Brushes.Black, new Point(37, 550 + 5));
                    TimeSpan t = new TimeSpan();
                    TimeSpan t1 = new TimeSpan();
                    if (sx.Bz == 1)
                    {
                        t = sx.Kssj - otime;
                        t1 = DateTime.Now - otime;
                    }
                    else if (sx.Bz == 2)
                    {
                        t = sx.Kssj - otime;
                        t1 = sx.Jssj - otime;
                    }
                    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                    int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 20 / jcsjjg + 100);
                    int y1 = 0;
                    if (sxi % 2 == 0)
                        y1 = 460 + 20 * 6;
                    else
                        y1 = 460 + 20 * 7;
                    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                    e.Graphics.DrawString(sx.Name + sx.Jl.ToString(), this.Font, Brushes.Blue, new Point(x1 - 5, y1 - 8));

                }
                sxi++;

            }
            #endregion

            #region  //画输液
            int syi = 0;
            foreach (adims_MODEL.shuye sy in sy1ist)//画输液
            {
                if (sy.Bz > 0)
                {
                    //e.Graphics.DrawString(sy.Name, this.Font, Brushes.Black, new Point(37, 575 + 5));
                    TimeSpan t = new TimeSpan();
                    TimeSpan t1 = new TimeSpan();
                    if (sy.Bz == 1)
                    {
                        t = sy.Kssj - otime;
                        t1 = DateTime.Now - otime;
                    }
                    else if (sy.Bz == 2)
                    {
                        t = sy.Kssj - otime;
                        t1 = sy.Jssj - otime;
                    }
                    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                    int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 20 / jcsjjg + 100);
                    int y1 = 0;
                    if (syi % 2 == 0)
                        y1 = 460 + 20 * 8;
                    else
                        y1 = 460 + 20 * 9;
                    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                    e.Graphics.DrawString(sy.Name + sy.Jl.ToString(), this.Font, Brushes.Blue, new Point(x1 - 5, y1 - 8));
                }
                syi++;
            }

            #endregion

            #region  //画术中事件
            int szsji = 1;
            foreach (adims_MODEL.szsj s in sjList)//画术中事件
            {
                TimeSpan t = new TimeSpan();
                t = s.D - otime;
                int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                float y1 = (float)(455 + 20 * 10);
                e.Graphics.FillRectangle(Brushes.Pink, x1, y1, szsji > 9 ? 14 : 10, 12);
                e.Graphics.DrawString(szsji.ToString(), this.Font, Brushes.Black, new PointF(szsji > 9 ? x1 - 2 : x1, y1));
                szsji++;
            }
            int tsyyi = 1;
            foreach (adims_MODEL.tsyy s in yyList)//画特殊用药
            {
                TimeSpan t = new TimeSpan();
                t = s.D - otime;
                float x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                float y1 = (float)(455 + 20 * 10);
                e.Graphics.FillEllipse(Brushes.LightGreen, x1, y1, tsyyi > 9 ? 14 : 10, 10);
                e.Graphics.DrawString(tsyyi.ToString(), this.Font, Brushes.Black, new PointF(tsyyi > 9 ? x1 - 2 : x1, y1));
                tsyyi++;
            }
            #endregion

        }

        int xMax = 1060;
        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.X > 100 && e.X < xMax)
            {
                if (e.Y > 100 && e.Y < 125)//添加麻醉气体药
                {
                    PACUaddQt qt = new PACUaddQt(otime.AddMinutes((e.X - 100) / 20 * jcsjjg), mzjldID);
                    qt.ShowDialog();
                    BindQtList();
                }
                if (e.Y > 450 && e.Y < 470)//增加出血量
                {
                    if (cn.Count == 0)
                    {
                        PACU_Addcl formaddcl = new PACU_Addcl(cn, otime.AddMinutes((e.X - 100) / 20 * jcsjjg), 2, mzjldID);
                        formaddcl.ShowDialog();
                    }
                    else
                    {
                        foreach (adims_MODEL.clcxqt cn1 in cn)//修改出血量
                        {
                            XiugaiChuLiang formxgjhsj = new XiugaiChuLiang(mzjldID, cn1, 2);
                            formxgjhsj.ShowDialog();
                        }
                    }
                }
                if (e.Y > 570 && e.Y < 610)//增加输血
                {
                    PACU_Add_SX f2 = new PACU_Add_SX(sx1ist, otime.AddMinutes((e.X - 100) / 20 * jcsjjg), mzjldID);
                    f2.ShowDialog();
                    BindShuxueList();
                }
                if (e.Y > 610 && e.Y < 650)//添加输液
                {
                    PACU_AddSY f2 = new PACU_AddSY(sy1ist, otime.AddMinutes((e.X - 100) / 20 * jcsjjg), mzjldID);
                    f2.ShowDialog();
                    BindShuyeList();
                }
                if (e.Y > 470 && e.Y < 570)//修改监护项目值
                {
                    int jhxmCount = 1;
                    foreach (string s in jhxmy)
                    {
                        foreach (adims_MODEL.jhxm jhxmz in jhxmv)
                        {
                            if (jhxmz.Sy == s)
                            {
                                TimeSpan t = new TimeSpan();
                                t = jhxmz.D - otime;
                                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 90);
                                int y = jhxmCount * 20 + 450;
                                if (e.X > x && e.X < x + 13 && e.Y > y + 1 && e.Y < y + 13)
                                {
                                    xgjhsj formxgjhsj = new xgjhsj(mzjldID, jhxmz, 1);
                                    formxgjhsj.ShowDialog();
                                }
                            }
                        }
                        jhxmCount++;
                    }
                }

            }
            pictureBox1.Refresh();
            pictureBox2.Refresh();
        }

        int lx, CLid, xgqvalue, xghvalue;//生理记录检测点类型，修改前值，修改后值
        DateTime xgdtime = new DateTime();//修改点时间
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int ii = 0;
            Pic1_X = e.X; Pic1_Y = e.Y;
            foreach (adims_MODEL.point tssy in ssy)//判断收缩压是否选中
            {
                TimeSpan t = new TimeSpan();
                t = tssy.D - otime;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                int y = 0;
                if (tssy.V > 251)
                    y = 450 - 312;
                else
                    y = (int)(450 - (int)(tssy.V * 1.25));
                if (Pic1_X > x - 5 && Pic1_X < x + 5
                    && Pic1_Y < y + 1 && Pic1_Y > y - 5)
                {
                    PointCheckFlag = 1; p3t = tssy; lx = 1; xgqvalue = p3t.V;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.AutoSize = true;
                    pictureBox1.Controls.Add(lab1);
                    lab1.Location = new Point(x, y);
                    lab1.Text = p3t.V.ToString();
                }
            }
            foreach (adims_MODEL.point tssy in szy)//判断舒展压是否选中
            {
                TimeSpan t = new TimeSpan();
                t = tssy.D - otime;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                int y = 0;
                if (tssy.V > 251)
                    y = 450 - 312;
                else
                    y = (int)(450 - (int)(tssy.V * 1.25));
                if (Pic1_X > x - 5 && Pic1_X < x + 5 && Pic1_Y < y + 5 && Pic1_Y > y - 1)
                {
                    PointCheckFlag = 1; p3t = tssy; lx = 2; xgqvalue = p3t.V;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.AutoSize = true;
                    pictureBox1.Controls.Add(lab1);
                    lab1.Location = new Point(x, y);
                    lab1.Text = p3t.V.ToString();
                }
            }
            foreach (adims_MODEL.point tssy in mbo)//判断脉搏是否选中
            {
                TimeSpan t = new TimeSpan();
                t = tssy.D - otime;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                int y = 0;
                if (tssy.V > 251)
                    y = 450 - 312;
                else
                    y = (int)(450 - (int)(tssy.V * 1.25));
                if (Pic1_X > x - 5 && Pic1_X < x + 5 && Pic1_Y < y + 3 && Pic1_Y > y - 3)
                {
                    PointCheckFlag = 1; p3t = tssy; lx = 3; xgqvalue = p3t.V;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.AutoSize = true;
                    pictureBox1.Controls.Add(lab1);
                    lab1.Location = new Point(x, y);
                    lab1.Text = p3t.V.ToString();
                }
            }
            foreach (adims_MODEL.point tssy in hxl)//判断呼吸率是否选中
            {
                TimeSpan t = new TimeSpan();
                t = tssy.D - otime;
                int y;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                if (tssy.V > 251)
                    y = 450 - 312;
                else
                    y = (int)(450 - (int)(tssy.V * 1.25));
                if (Pic1_X > x - 5 && Pic1_X < x + 5 && Pic1_Y < y + 3 && Pic1_Y > y - 3)
                {
                    PointCheckFlag = 1; p3t = tssy; lx = 4; xgqvalue = p3t.V;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.AutoSize = true;
                    pictureBox1.Controls.Add(lab1);
                    lab1.Location = new Point(x, y);
                    lab1.Text = p3t.V.ToString();
                }
            }

            foreach (adims_MODEL.point tssy in tw)//判断体温是否选中
            {
                TimeSpan t = new TimeSpan();
                t = tssy.D - otime;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                int y = 0;
                if (tssy.V > 251)
                    y = 450 - 312;
                else
                    y = (int)(450 - (int)(tssy.V * 1.25));
                if (Pic1_X > x - 5 && Pic1_X < x + 5 && Pic1_Y < y + 5 && Pic1_Y > y - 5)
                {
                    PointCheckFlag = 1; p3t = tssy; lx = 5; xgqvalue = p3t.V;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.AutoSize = true;
                    pictureBox1.Controls.Add(lab1);
                    lab1.Location = new Point(x, y);
                    lab1.Text = p3t.V.ToString();
                }
            }

            foreach (adims_MODEL.mzqt q in mzqtList)  // 是否选中气体
            {
                TimeSpan t = new TimeSpan();
                t = q.Sysj - otime;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                int y = 110;
                TimeSpan t1 = new TimeSpan();
                t1 = q.Jssj - otime;
                int x1 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 20 / jcsjjg + 100);
                if (e.X > x - 5 && e.X < x + 5 && e.Y > y - 1 && e.Y < y + 8)
                { flagP1 = 1; TypeP1 = 1; t_mzqt = q; }
                if (e.X > x1 - 5 && e.X < x1 + 5 && e.Y > y - 1 && e.Y < y + 8)
                { flagP1 = 1; TypeP1 = 11; t_mzqt = q; }

            }

            foreach (adims_MODEL.clcxqt q in cn)  // 是否选中尿量图标
            {
                TimeSpan t = new TimeSpan();
                t = q.D - otime;
                CLid = q.Id;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                int y = 457;
                if (e.X > x - 5 && e.X < x + 12 && e.Y < y + 12 && e.Y > y)
                { flagP1 = 1; TypeP1 = 2; t_niaoliang = q; }

            }

            int sxi = 0;
            foreach (adims_MODEL.shuxue sx in sx1ist)// 是否选中输血
            {
                if (sx.Bz > 0)
                {
                    TimeSpan t = new TimeSpan();
                    t = sx.Kssj - otime;
                    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                    int y = 0;
                    if (sxi % 2 == 0)
                        y = 460 + 20 * 6;
                    else
                        y = 460 + 20 * 7;
                    if (e.X > x - 5 && e.X < x + 5 && e.Y > y - 1 && e.Y < y + 8)
                    { flagP1 = 1; TypeP1 = 3; t_shuxue = sx; }
                }
                sxi++;
            }

            int syi = 0;
            foreach (adims_MODEL.shuye yt in sy1ist)//是否选中输液
            {
                if (yt.Bz > 0)
                {
                    TimeSpan t = new TimeSpan();
                    t = yt.Kssj - otime;
                    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                    int y = 0;
                    if (syi % 2 == 0)
                        y = 460 + 20 * 8;
                    else
                        y = 460 + 20 * 9;
                    if (e.X > x - 5 && e.X < x + 5 && e.Y > y - 1 && e.Y < y + 8)
                    { flagP1 = 1; TypeP1 = 4; t_shuye = yt; }
                }
                syi++;
            }

            foreach (adims_MODEL.szsj sj in sjList)//是否选中事件
            {
                TimeSpan t = new TimeSpan();
                t = sj.D - otime;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                int y = 455 + 20 * 10;
                if (e.X > x - 8 && e.X < x + 8 && e.Y < y + 12 && e.Y > y)
                { flagP1 = 1; TypeP1 = 5; t_szsj = sj; ii = 1; }
            }
            foreach (adims_MODEL.tsyy ts in yyList)//是否选中用药
            {
                TimeSpan t = new TimeSpan();
                t = ts.D - otime;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                int y = 455 + 20 * 10;
                if (e.X > x - 8 && e.X < x + 8 && e.Y < y + 12 && e.Y > y)
                { flagP1 = 1; TypeP1 = 6; t_tsyy = ts; ii = 1; }
            }

        }
        DateTime dtjieshu = new DateTime();
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Point pttag = new Point(MousePosition.X, MousePosition.Y);
            Graphics g = this.CreateGraphics();
            g.Clear(this.BackColor);
            //if (MousePosition.Y > e.Y)
            //{
            //}
            //if (MousePosition.X > e.X)
            //{
            //    g.Clear(this.BackColor);
            //}
            Pic1_X = e.X; Pic1_Y = e.Y;

            if (PointCheckFlag == 1)//标志为1,移动监测点
            {
                p3t.V = (int)((450 - Pic1_Y) / 1.25);
                lab1.Visible = true;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Red;
                lab1.AutoSize = true;
                pictureBox1.Controls.Add(lab1);
                lab1.Location = new Point(Pic1_X, Pic1_Y);
                lab1.Text = p3t.V.ToString();
                lx = p3t.Lx;
                pictureBox1.Refresh();
                xghvalue = p3t.V; //得到修改后的值
                xgdtime = Convert.ToDateTime(p3t.D);
            }
            if (flagP1 == 1)
            {
                if (TypeP1 == 1)//移动气体开始
                {
                    t_mzqt.Sysj = otime.AddMinutes((Pic1_X - 100) / 20 * jcsjjg);
                    lab1.Visible = true;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Blue;
                    lab1.AutoSize = true;
                    pictureBox1.Controls.Add(lab1);
                    lab1.Text = t_mzqt.Sysj.ToString("HH:mm");
                    lab1.Location = new Point(e.X, e.Y);
                    lab1.BringToFront();
                }
                else if (TypeP1 == 11)//移动气体结束
                {
                    t_mzqt.Jssj = otime.AddMinutes((Pic1_X - 100) / 20 * jcsjjg);
                    lab1.Visible = true;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Blue;
                    lab1.AutoSize = true;
                    pictureBox1.Controls.Add(lab1);
                    lab1.Text = t_mzqt.Jssj.ToString("HH:mm");
                    lab1.Location = new Point(e.X, e.Y);
                    lab1.BringToFront();
                }
                else if (TypeP1 == 2)//移动尿量
                {
                    t_niaoliang.D = otime.AddMinutes((Pic1_X - 100) / 20 * jcsjjg);
                    lab1.Visible = true;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.AutoSize = true;
                    pictureBox1.Controls.Add(lab1);
                    lab1.Text = t_niaoliang.D.ToString("HH:mm");
                    lab1.Location = new Point(e.X, e.Y);
                    lab1.BringToFront();
                }

                else if (TypeP1 == 3)//移动输血
                {
                    t_shuxue.Kssj = otime.AddMinutes((Pic1_X - 100) / 20 * jcsjjg);
                    lab1.Visible = true;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.AutoSize = true;
                    pictureBox1.Controls.Add(lab1);
                    lab1.Text = t_shuxue.Kssj.ToString("HH:mm");
                    lab1.Location = new Point(e.X, e.Y);
                    lab1.BringToFront();
                }
                else if (TypeP1 == 4)//移动输液
                {
                    t_shuye.Kssj = otime.AddMinutes((Pic1_X - 100) / 20 * jcsjjg);
                    lab1.Visible = true;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.AutoSize = true;
                    pictureBox1.Controls.Add(lab1);
                    lab1.Text = t_shuye.Kssj.ToString("HH:mm");
                    lab1.Location = new Point(e.X, e.Y);
                    lab1.BringToFront();
                }
                else if (TypeP1 == 5)//术中事件移动
                {
                    int X = e.X;
                    dtjieshu = otime.AddMinutes((X - 100) / 20 * jcsjjg);
                    t_szsj.D = dtjieshu;
                    lab1.Visible = true;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Blue;
                    lab1.AutoSize = true;
                    pictureBox1.Controls.Add(lab1);
                    lab1.Text = t_szsj.D.ToString("HH:mm");
                    lab1.Location = new Point(e.X, e.Y);
                    lab1.BringToFront();
                    pictureBox1.Refresh();
                }


                else if (TypeP1 == 6)//用药移动
                {
                    int X = e.X;
                    dtjieshu = otime.AddMinutes((X - 100) / 20 * jcsjjg);
                    t_tsyy.D = dtjieshu;
                    lab1.Visible = true;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Blue;
                    lab1.AutoSize = true;
                    pictureBox1.Controls.Add(lab1);
                    lab1.Text = t_tsyy.D.ToString("HH:mm");
                    lab1.Location = new Point(e.X, e.Y);
                    lab1.BringToFront();
                    pictureBox1.Refresh();
                }

            }
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (PointCheckFlag == 1)//保存移动后的监测点值
            {
                p3t.V = xghvalue;
                p3t.D = xgdtime;
                p3t.Lx = lx;
                bll.xgpointPACU(mzjldID, p3t);
                PointCheckFlag = 0;
            }
            if (flagP1 == 1)
            {
                if (TypeP1 == 1)//修改移动后的麻醉气体开始时间
                {
                    bll.xgqtPACU(mzjldID, t_mzqt);
                }
                else if (TypeP1 == 11)//修改移动后的麻醉气体结束时间
                {
                    bll.xgqtPACU1(mzjldID, t_mzqt);
                }
                else if (TypeP1 == 2)//修改移动后的出血时间
                {
                    bll.xgclsjPACU(mzjldID, t_niaoliang);
                }
                else if (TypeP1 == 3)//修改移动后的输血时间
                {
                    bll.xg_PACU_sx(mzjldID, t_shuxue);
                }
                else if (TypeP1 == 4)//修改移动后的输液时间
                {
                    bll.xg_PACU_sy(mzjldID, t_shuye);
                }

                else if (TypeP1 == 5)//修改移动后的术中时间时间
                {
                    bll.xgszsjTimePacu(mzjldID, t_szsj, dtjieshu);
                    BindShijian();
                }
                else if (TypeP1 == 6)//修改移动后的特殊用药时间
                {
                    bll.xgtsyyTimePACU(mzjldID, t_tsyy, dtjieshu);
                    BindYongyao();
                }
                flagP1 = 0;
            }
            timer2.Enabled = true;
            pictureBox1.Refresh();

        }


        #region<<入室标志>>
        private void lb_InRoom_DoubleClick(object sender, EventArgs e)//入室双击
        {
            if (!rushi)
            {
                //txtMzsj.Controls[0].Text = Convert.ToString(DateTime.Now);
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                rushiFlag.Text = "＞";
                rushiFlag.Visible = true;
                rushiFlag.AutoSize = true;
                rushiFlag.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                rushiFlag.ForeColor = Color.Purple;
                rushiFlag.BackColor = Color.Transparent;
                rushiFlag.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes)
                                   * 20 / jcsjjg + 90), 650);
                this.pictureBox1.Controls.Add(rushiFlag);
                rushi = true;
                chushi = false;
                string zdming = "rssj";
                dal.Update_single_PACU_SZ(zdming, DateTime.Now, mzjldID);

                rushiFlag.MouseDown += new MouseEventHandler(rushiFlag_MouseDown);
                rushiFlag.MouseMove += new MouseEventHandler(rushiFlag_MouseMove);
                rushiFlag.MouseUp += new MouseEventHandler(rushiFlag_MouseUp);
                rushiFlag.MouseLeave += new EventHandler(rushiFlag_MouseLeave);
            }
            else
                MessageBox.Show("麻醉已经开始");
        }


        int xStart, xEnd, yStart;//移动麻醉，手术，插管开始结束点
        string zdming = "";
        private void rushiFlag_MouseDown(object sender, EventArgs e)
        {
            p3lf1 = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.LightYellow;
            lab1.AutoSize = true;
            lab1.Visible = true;
            pictureBox1.Controls.Add(lab1);
            lab1.Location = new Point(rushiFlag.Location.X, rushiFlag.Location.Y - 15);
            xStart = rushiFlag.Location.X;
            yStart = rushiFlag.Location.Y;
            DateTime DTIME = otime.AddMinutes((xStart - 90) / 20 * jcsjjg);
            lab1.Text = DTIME.ToString("HH:mm");
        }

        private void rushiFlag_MouseUp(object sender, EventArgs e)
        {
            zdming = "rssj";

            p3lf1 = 0;
            if (!chushi)
            {
                dal.Update_single_PACU_SZ(zdming, otime.AddMinutes((xEnd - 90) / 20 * jcsjjg), mzjldID);
                rsTime = otime.AddMinutes((xEnd - 90) / 20 * jcsjjg);
            }
            else
            {
                if (xEnd < chushiFlag.Location.X)
                {
                    dal.Update_single_PACU_SZ(zdming, otime.AddMinutes((xEnd - 90) / 20 * jcsjjg), mzjldID);
                    rsTime = otime.AddMinutes((xEnd - 90) / 20 * jcsjjg);

                }
                else
                    rushiFlag.Location = new Point(xStart, yStart);
            }
            timer2.Enabled = true;
        }

        private void rushiFlag_MouseLeave(object sender, EventArgs e)
        {
            p3lf1 = 0;

        }

        private void rushiFlag_MouseMove(object sender, MouseEventArgs e)
        {
            if (p3lf1 == 1)
            {

                rushiFlag.Location = new Point(rushiFlag.Location.X + e.X / 2 - 2, rushiFlag.Location.Y);
                xEnd = rushiFlag.Location.X;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Red;
                lab1.AutoSize = true;
                lab1.Visible = true;
                pictureBox1.Controls.Add(lab1);
                lab1.Location = new Point(rushiFlag.Location.X, rushiFlag.Location.Y - 15);
                DateTime DTIME = otime.AddMinutes((xEnd - 90) / 20 * jcsjjg);
                lab1.Text = DTIME.ToString("HH:mm");
                //int ssshijian = Convert.ToInt32((chushiFlag.Location.X - rushiFlag.Location.X) / 4);//计算麻醉总时间
                //txtMZJS.Controls[0].Text = DTIME.ToString("HH:mm");

            }
        }

        #endregion

        #region<<出室标志>>
        private void lb_LeaveRoom_DoubleClick(object sender, EventArgs e)
        {
            if (rushi && !chushi)
            {
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                chushiFlag.Text = "＜";
                chushiFlag.AutoSize = true;
                chushiFlag.Visible = true;
                chushiFlag.BackColor = Color.Transparent;
                chushiFlag.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                chushiFlag.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60
                    + t.Minutes) * 20 / jcsjjg + 90), 650);
                this.pictureBox1.Controls.Add(chushiFlag);
                chushi = true;
                string zdming = "cssj";
                dal.Update_single_PACU_SZ(zdming, DateTime.Now, mzjldID);
                //txtMZJS.Controls[0].Text = DateTime.Now.ToString("HH:mm");
                chushiFlag.MouseDown += new MouseEventHandler(chushiFlag_MouseDown);
                chushiFlag.MouseMove += new MouseEventHandler(chushiFlag_MouseMove);
                chushiFlag.MouseUp += new MouseEventHandler(chushiFlag_MouseUp);
                chushiFlag.MouseLeave += new EventHandler(chushiFlag_MouseLeave);
            }
            else
                MessageBox.Show("未填写入室时间或已填写出室时间");
        }

        private void chushiFlag_MouseDown(object sender, EventArgs e)
        {
            p3lf2 = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.LightYellow;
            lab1.AutoSize = true;
            lab1.Visible = true;

            pictureBox1.Controls.Add(lab1);
            lab1.Location = new Point(chushiFlag.Location.X, chushiFlag.Location.Y - 15);
            xStart = chushiFlag.Location.X;
            yStart = chushiFlag.Location.Y;
            DateTime DTIME = otime.AddMinutes((xStart - 90) / 20 * jcsjjg);
            lab1.Text = DTIME.ToString("HH:mm");
        }

        private void chushiFlag_MouseUp(object sender, EventArgs e)
        {
            p3lf2 = 0;
            if (xEnd > rushiFlag.Location.X)
            {
                zdming = "cssj";
                dal.Update_single_PACU_SZ(zdming, otime.AddMinutes((xEnd - 90) / 20 * jcsjjg), mzjldID);
                csTime = otime.AddMinutes((xEnd - 90) / 20 * jcsjjg);
            }
            else
                chushiFlag.Location = new Point(xStart, yStart);

            timer2.Enabled = true;
        }

        private void chushiFlag_MouseLeave(object sender, EventArgs e)
        {
            p3lf2 = 0;
        }

        private void chushiFlag_MouseMove(object sender, MouseEventArgs e)
        {
            if (p3lf2 == 1)
            {
                chushiFlag.Location = new Point(chushiFlag.Location.X + e.X / 2 - 2, chushiFlag.Location.Y);
                xEnd = chushiFlag.Location.X;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Red;
                lab1.AutoSize = true;
                lab1.Visible = true;
                pictureBox1.Controls.Add(lab1);
                lab1.Location = new Point(chushiFlag.Location.X, chushiFlag.Location.Y - 15);
                DateTime DTIME = otime.AddMinutes((xEnd - 90) / 20 * jcsjjg);
                lab1.Text = DTIME.ToString("HH:mm");
                //int ssshijian = Convert.ToInt32((chushiFlag.Location.X - rushiFlag.Location.X) / 4);//计算麻醉总时间
                //txtMZJS.Controls[0].Text = DTIME.ToString("HH:mm");

            }
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
                cgFlag.Text = "Θ";
                cgFlag.Visible = true;
                cgFlag.AutoSize = true;
                cgFlag.BackColor = Color.Transparent;
                cgFlag.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                cgFlag.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60
                    + t.Minutes) * 20 / jcsjjg + 90), 650);
                this.pictureBox1.Controls.Add(cgFlag);
                CGUAN = true;
                BGUAN = false;
                string zdming = "cgsj";
                dal.Update_single_PACU_SZ(zdming, DateTime.Now, mzjldID);
                //txtCGKS.Controls[0].Text = DateTime.Now.ToString("HH:mm");
                cgFlag.MouseDown += new MouseEventHandler(cgFlag_MouseDown);
                cgFlag.MouseMove += new MouseEventHandler(cgFlag_MouseMove);
                cgFlag.MouseUp += new MouseEventHandler(cgFlag_MouseUp);
                cgFlag.MouseLeave += new EventHandler(cgFlag_MouseLeave);
            }
            else
                MessageBox.Show("已经插管！");
        }
        private void cgFlag_MouseDown(object sender, EventArgs e)
        {
            p3lf1 = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.LightYellow;
            lab1.AutoSize = true;
            lab1.Visible = true;
            pictureBox1.Controls.Add(lab1);
            lab1.Location = new Point(cgFlag.Location.X, cgFlag.Location.Y - 15);
            xStart = cgFlag.Location.X;
            yStart = cgFlag.Location.Y;
            DateTime DTIME = otime.AddMinutes((xStart - 90) / 20 * jcsjjg);
            lab1.Text = DTIME.ToString("HH:mm");
        }
        private void cgFlag_MouseMove(object sender, MouseEventArgs e)
        {
            if (p3lf1 == 1)
            {
                //TimeSpan t = new TimeSpan();
                //t = DateTime.Now - otime;
                cgFlag.Location = new Point(cgFlag.Location.X + e.X / 2 - 2, cgFlag.Location.Y);
                xEnd = cgFlag.Location.X;

                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Red;
                lab1.AutoSize = true;
                lab1.Visible = true;
                pictureBox1.Controls.Add(lab1);
                lab1.Location = new Point(cgFlag.Location.X, cgFlag.Location.Y - 15);
                DateTime DTIME = otime.AddMinutes((xEnd - 90) / 20 * jcsjjg);
                lab1.Text = DTIME.ToString("HH:mm");
                //txtCGKS.Controls[0].Text = DTIME.ToString("HH:mm");


            }
        }

        private void cgFlag_MouseUp(object sender, EventArgs e)
        {
            zdming = "cgsj";
            p3lf1 = 0;
            if (!BGUAN)
            {
                dal.Update_single_PACU_SZ(zdming, otime.AddMinutes((xEnd - 90) / 20 * jcsjjg), mzjldID);

                cgTime = otime.AddMinutes((xEnd - 90) / 20 * jcsjjg);
            }
            else
            {
                if (xEnd < bgFlag.Location.X)
                {
                    dal.Update_single_PACU_SZ(zdming, otime.AddMinutes((xEnd - 90) / 20 * jcsjjg), mzjldID);

                    cgTime = otime.AddMinutes((xEnd - 90) / 20 * jcsjjg);
                }
                else
                    cgFlag.Location = new Point(xStart, yStart);
            }
            timer2.Enabled = true;
        }

        private void cgFlag_MouseLeave(object sender, EventArgs e)
        {
            p3lf1 = 0;
        }


        #endregion

        #region <<拔管>>

        private void lbBg_DoubleClick(object sender, EventArgs e)
        {
            //if (CGUAN && !BGUAN)
            //{
            TimeSpan t = new TimeSpan();
            t = DateTime.Now - otime;
            bgFlag.Text = "Φ";
            bgFlag.AutoSize = true;
            bgFlag.Visible = true;
            bgFlag.BackColor = Color.Transparent;
            bgFlag.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            bgFlag.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60
                + t.Minutes) * 20 / jcsjjg + 90), 650);
            this.pictureBox1.Controls.Add(bgFlag);
            BGUAN = true;
            string zdming = "bgsj";
            dal.Update_single_PACU_SZ(zdming, DateTime.Now, mzjldID);
            //txtCGJS.Controls[0].Text = DateTime.Now.ToString("HH:mm");
            bgFlag.MouseDown += new MouseEventHandler(bgFlag_MouseDown);
            bgFlag.MouseMove += new MouseEventHandler(bgFlag_MouseMove);
            bgFlag.MouseUp += new MouseEventHandler(bgFlag_MouseUp);
            bgFlag.MouseLeave += new EventHandler(bgFlag_MouseLeave);
            //}
            //else
            //    MessageBox.Show("还未插管或已经拔管！");
        }


        private void bgFlag_MouseDown(object sender, EventArgs e)
        {
            p3lf2 = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.LightYellow;
            lab1.AutoSize = true;
            lab1.Visible = true;
            pictureBox1.Controls.Add(lab1);
            lab1.Location = new Point(bgFlag.Location.X, bgFlag.Location.Y - 15);
            xStart = bgFlag.Location.X;
            yStart = cgFlag.Location.Y;
            DateTime DTIME = otime.AddMinutes((xStart - 90) / 20 * jcsjjg);
            lab1.Text = DTIME.ToString("HH:mm");
        }
        private void bgFlag_MouseMove(object sender, MouseEventArgs e)
        {
            if (p3lf2 == 1)
            {

                bgFlag.Location = new Point(bgFlag.Location.X + e.X / 2 - 2, bgFlag.Location.Y);
                xEnd = bgFlag.Location.X;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Red;
                lab1.AutoSize = true;
                lab1.Visible = true;
                pictureBox1.Controls.Add(lab1);
                lab1.Location = new Point(bgFlag.Location.X, bgFlag.Location.Y - 15);
                DateTime DTIME = otime.AddMinutes((xEnd - 90) / 20 * jcsjjg);
                lab1.Text = DTIME.ToString("HH:mm");
                //int ssshijian = Convert.ToInt32((bgFlag.Location.X - bgFlag.Location.X) / 3.3);//计算麻醉总时间
                //txtCGJS.Controls[0].Text = DTIME.ToString("HH:mm");

            }
        }
        private void bgFlag_MouseUp(object sender, EventArgs e)
        {
            zdming = "bgsj";
            p3lf2 = 0;
            if (xEnd > cgFlag.Location.X)
            {
                dal.Update_single_PACU_SZ(zdming, otime.AddMinutes((xEnd - 90) / 20 * jcsjjg), mzjldID);

                bgTime = otime.AddMinutes((xEnd - 90) / 20 * jcsjjg);
            }
            else
                bgFlag.Location = new Point(xStart, yStart);
            timer2.Enabled = true;
        }

        private void bgFlag_MouseLeave(object sender, EventArgs e)
        {
            p3lf2 = 0;
        }


        #endregion

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Interval = 2000;
            lab1.Visible = false;
            timer2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (btnMonitor.Text == "结束监测")
            {
                MessageBox.Show("检测没有结束，不能打印");
                return;
            }
            if (!rushi)
            {
                MessageBox.Show("入室没有标记，不能打印");
                return;
            }
            if (!chushi)
            {
                MessageBox.Show("出室没有标记，不能打印");
                return;
            }
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
            if (JsFlag > 0)
            {
                MessageBox.Show(YwName + "没有标记结束，不能打印");
                return;
            }
            ptime = fristopen;
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("K16", 780, 1080);
        }
        int iYema = 1;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font textfront = new Font(new FontFamily("宋体"), 9);
            Font ptzt8 = new Font(new FontFamily("宋体"), 8);
            Font ptzt7 = new Font(new FontFamily("宋体"), 7);
            Font ptzt6 = new Font(new FontFamily("宋体"), 6);
            Font ptzt5 = new Font(new FontFamily("宋体"), 5);
            Font flagF = new Font(new FontFamily("宋体"), 12);
            Font txText = new Font(new FontFamily("宋体"), 9);
            Font tagfont = new Font(new FontFamily("宋体"), 13);
            Pen pb2 = new Pen(Brushes.Black);
            pb2.Width = 2;
            Pen pxuxian = new Pen(Brushes.Black);
            pxuxian.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Pen pred2 = new Pen(Brushes.Red);
            pred2.Width = 2;

            int dyd = 0, dyd1 = 0, dyd2 = 0, dyd3 = 0, dyd4 = 0;//标志是否是第一个点
            int dy = 0;//   气体数量初始值
            Font fsmall = new Font("宋体", 7);
            Font fsmall8 = new Font("宋体", 8);
            int x = 20, y = 30, yUnder = y + 18;
            string title1 = "江苏盛泽医院、江苏省人民医院盛泽分院";
            string title2 = "        麻醉后恢复室记录单";

            y = y + 30; yUnder = y + 18;
            e.Graphics.DrawString(title1, tagfont, Brushes.Black, x + 160, y);
            y = y + 30; yUnder = y + 18;
            e.Graphics.DrawString(title2, tagfont, Brushes.Black, x + 160, y);

            y = y + 30; yUnder = y + 15;
            e.Graphics.DrawLine(pb2, x, y, x, y + 800);
            e.Graphics.DrawLine(pb2, x, y, x + 700, y);
            e.Graphics.DrawLine(pb2, x, y + 800, x + 700, y + 800);
            e.Graphics.DrawLine(pb2, x + 700, y, x + 700, y + 800);

            #region //打印基本信息
            y = y + 5; yUnder = y + 15;
            e.Graphics.DrawString("姓名 " + tbPatname.Text, textfront, Brushes.Black, x + 5, y);
            e.Graphics.DrawLine(Pens.Black, x + 35, yUnder, x + 110, yUnder);
            e.Graphics.DrawString("性别 " + tbSex.Text, textfront, Brushes.Black, x + 120, y);
            e.Graphics.DrawLine(Pens.Black, x + 155, yUnder, x + 210, yUnder);
            e.Graphics.DrawString("年龄 " + tbAge.Text + " " + cmbAgeDW.Text, textfront, Brushes.Black, x + 220, y);
            e.Graphics.DrawLine(Pens.Black, x + 255, yUnder, x + 310, yUnder);
            e.Graphics.DrawString("病区 " + tbBingqu.Text, textfront, Brushes.Black, x + 320, y);
            e.Graphics.DrawLine(Pens.Black, x + 355, yUnder, x + 450, yUnder);
            e.Graphics.DrawString("床号 " + tbBedNO.Text, textfront, Brushes.Black, x + 460, y);
            e.Graphics.DrawLine(Pens.Black, x + 485, yUnder, x + 550, yUnder);
            e.Graphics.DrawString("住院号 " + tbZhuyuanID.Text, textfront, Brushes.Black, x + 560, y);
            e.Graphics.DrawLine(Pens.Black, x + 605, yUnder, x + 695, yUnder);
            y = y + 30; yUnder = y + 15;
            e.Graphics.DrawString("体重 " + tbWeight.Text, textfront, Brushes.Black, x + 5, y);
            e.Graphics.DrawLine(Pens.Black, x + 35, yUnder, x + 80, yUnder);
            e.Graphics.DrawString("KG  手术名称 " + tbShoushuName.Text, textfront, Brushes.Black, x + 80, y);
            e.Graphics.DrawLine(Pens.Black, x + 165, yUnder, x + 550, yUnder);
            e.Graphics.DrawString("日期 " + dtVisitDate.Text, textfront, Brushes.Black, x + 560, y);
            e.Graphics.DrawLine(Pens.Black, x + 590, yUnder, x + 695, yUnder);
            #endregion
            DateTime dtEnd = new DateTime();//打印截止时间判断        
            DateTime pagetime = new DateTime();
            DataTable dtMax = bll.GetMaxPointPacu(mzjldID);
            if (dtMax.Rows[0][0].ToString() == "")
                dtEnd = DateTime.Now;
            else
                dtEnd = Convert.ToDateTime(dtMax.Rows[0][0]);
            pagetime = ptime; //当前打印页时间
            y = y + 25; yUnder = y + 15;
            e.Graphics.DrawLine(Pens.Black, x, y, x + 700, y);
            e.Graphics.DrawString(" 时 间", txText, Brushes.Black, x + 5, y + 3);
            for (int i = 0; i < 8; i++)
            {
                e.Graphics.DrawString(ptime.ToString("HH:mm"), txText, Brushes.Black, x + 85 + 75 * i, y + 3);
                ptime = ptime.AddMinutes(6 * jcsjjg);
            }
            y = y + 20; yUnder = y + 13;
            e.Graphics.DrawLine(Pens.Black, x, y, x + 700, y);
            //e.Graphics.DrawString("氧 气", ptzt8, Brushes.Black, x + 5, y +3);

            #region<<打印气体>>
            ArrayList ssQT = new ArrayList();
            foreach (adims_MODEL.mzqt mzqt in mzqtList)
            {
                e.Graphics.DrawString(mzqt.Qtname, this.Font, Brushes.Black, new Point(x + 30, y + 3));

                if (mzqt.Jssj > pagetime)
                {
                    if (mzqt.Jssj < pagetime.AddMinutes(48 * jcsjjg))
                    {
                        TimeSpan t = new TimeSpan();
                        TimeSpan t1 = new TimeSpan();
                        t1 = mzqt.Jssj - pagetime;
                        t = mzqt.Sysj - pagetime;
                        int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12.5 / jcsjjg + 100) + x;
                        int y1 = y + 8;
                        int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 12.5 / jcsjjg + 100) + x;
                        if (x1 > 100 + x)
                            e.Graphics.DrawString(mzqt.Yl.ToString() + " " + mzqt.Dw, ptzt7, Brushes.Blue, new Point(x1 + 3, y1 - 6));

                        if (t.Days * 24 * 60 + t.Hours * 60 + t.Minutes >= 0)
                        {
                            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 3, y1 + 6), new Point(x1 + 3, y1 + 6) });
                            e.Graphics.DrawLine(pred2, new Point(x1, y1 + 3), new Point(x2, y1 + 3));
                            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 3, y1 + 6), new Point(x2 + 3, y1 + 6) });
                        }
                        else
                        {
                            e.Graphics.DrawLine(pred2, new Point(100 + x, y1 + 3), new Point((int)(x2), y1 + 3));
                            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 3, y1 + 6), new Point(x2 + 3, y1 + 6) });
                        }
                    }
                    else
                    {
                        TimeSpan t = new TimeSpan();
                        TimeSpan t1 = new TimeSpan();
                        t1 = mzqt.Jssj - pagetime;
                        t = mzqt.Sysj - pagetime;
                        int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12.5 / jcsjjg + 100) + x;
                        int y1 = y + 8;
                        int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 12.5 / jcsjjg + 100) + x;
                        if (x1 > 100 + x)
                            e.Graphics.DrawString(mzqt.Yl.ToString() + " " + mzqt.Dw, ptzt6, Brushes.Blue, new Point(x1 + 3, y1 - 6));

                        if (t.Days * 24 * 60 + t.Hours * 60 + t.Minutes >= 0)
                        {
                            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 3, y1 + 6), new Point(x1 + 3, y1 + 6) });
                            e.Graphics.DrawLine(pred2, new Point(x1, y1 + 3), new Point(700 + x, y1 + 3));
                        }
                        else
                            e.Graphics.DrawLine(pred2, new Point(100, y1 + 3), new Point(x2, y1 + 3));
                    }
                }
                dy++;
            }

            #endregion

            #region 打印监测点区域画图
            y = y + 20; yUnder = y + 13;
            e.Graphics.DrawLine(Pens.Black, x, y, x + 700, y);
            e.Graphics.DrawString("T", ptzt8, Brushes.Black, x + 10, y + 3);
            e.Graphics.DrawString("Bp ", ptzt8, Brushes.Black, x + 50, y + 3);
            e.Graphics.DrawString("RR", ptzt8, Brushes.Black, x + 10, y + 18);
            e.Graphics.DrawString("HR", ptzt8, Brushes.Black, x + 50, y + 18);
            for (int i = 0; i < 14; i++)
            {
                e.Graphics.DrawLine(Pens.Black, x + 100, y + i * 20, x + 700, y + i * 20);
            }
            int jaa = 110, kaa = 220;
            for (int i = 0; i < 11; i++)
            {
                e.Graphics.DrawString(jaa.ToString(), txText, Brushes.Black, x + 10, y + 33 + i * 20);
                e.Graphics.DrawString(kaa.ToString(), txText, Brushes.Black, x + 50, y + 33 + i * 20);
                jaa = jaa - 10; kaa = kaa - 20;
            }
            for (int i = 0; i < 48; i++)
            {
                e.Graphics.DrawLine(pxuxian, (float)(x + 100 + 12.5 * i), (float)(y - 20), (float)(x + 100 + 12.5 * i), (float)(y + 20 * 13));
                if (i % 3 == 0)
                    e.Graphics.DrawLine(Pens.Black, (float)(x + 100 + 12.5 * i), (float)(y - 22), (float)(x + 100 + 12.5 * i), (float)(y + 20 * 13));

            }
            #endregion

            #region<<打印point>>

            foreach (adims_MODEL.point tp in ssy)
            {
                int x1, y1;
                TimeSpan t = new TimeSpan();
                if (tp.D >= pagetime && tp.D <= pagetime.AddMinutes(48 * jcsjjg))
                {
                    t = tp.D - pagetime;
                    x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12.5 / jcsjjg + 100 + x);
                    if (tp.V > 240)
                    {
                        y1 = (int)(y + 20 * 13 - 240);
                        e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Red, x1, y1);
                    }
                    else
                        y1 = (int)(y + 20 * 13 - (int)(tp.V * 1));
                    e.Graphics.DrawPolygon(Pens.Red, new Point[3] { new Point(x1, y1), new Point(x1 - 4, y1 - 4), new Point(x1 + 4, y1 - 4) });

                    if (MousePosition.X == x1 && MousePosition.Y == y1)
                    {
                        e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Red, x1, y1);
                    }
                    if (dyd != 0)
                        e.Graphics.DrawLine(Pens.Red, new Point(x1, y1), lastpoint);
                    lastpoint.X = x1;
                    lastpoint.Y = y1;
                    dyd++;
                }

            }

            foreach (adims_MODEL.point tp in szy)
            {
                if (tp.D >= pagetime && tp.D <= pagetime.AddMinutes(48 * jcsjjg))
                {
                    int x1, y1;
                    TimeSpan t = new TimeSpan();
                    t = tp.D - pagetime;
                    x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12.5 / jcsjjg + 100 + x);
                    if (tp.V > 240)
                    {
                        y1 = (int)(y + 20 * 13 - 240);
                        e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Red, x1, y1);
                    }
                    else
                        y1 = (int)(y + 20 * 13 - (int)(tp.V * 1));
                    e.Graphics.DrawPolygon(Pens.Red, new Point[3] { new Point(x1, y1), new Point(x1 - 4, y1 - 4), new Point(x1 + 4, y1 - 4) });
                    if (dyd1 != 0)
                        e.Graphics.DrawLine(Pens.Red, new Point(x1, y1), lastpoint1);
                    lastpoint1.X = x1;
                    lastpoint1.Y = y1;
                    dyd1++;
                }

            }
            foreach (adims_MODEL.point tp in xl)
            {
                if (tp.D >= pagetime && tp.D <= pagetime.AddMinutes(48 * jcsjjg))
                {
                    int x1, y1;
                    TimeSpan t = new TimeSpan();
                    t = tp.D - pagetime;
                    x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12.5 / jcsjjg + 100 + x);
                    if (tp.V > 240)
                    {
                        y1 = (int)(y + 20 * 13 - 240);
                        e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Green, x1, y1);
                    }
                    else
                        y1 = (int)(y + 20 * 13 - (int)(tp.V * 1));
                    e.Graphics.FillEllipse(Brushes.Blue, new Rectangle(x1 - 2, y1 - 2, 5, 5));
                    if (dyd2 != 0)
                        e.Graphics.DrawLine(Pens.Blue, new Point(x1, y1), lastpoint2);
                    lastpoint2.X = x1 + 3;
                    lastpoint2.Y = y1;
                    dyd2++;
                }
            }
            foreach (adims_MODEL.point tp in hxl)
            {
                if (tp.D >= pagetime && tp.D <= pagetime.AddMinutes(48 * jcsjjg))
                {
                    int x1, y1;
                    TimeSpan t = new TimeSpan();
                    t = tp.D - pagetime;
                    x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12.5 / jcsjjg + 100 + x);
                    if (tp.V > 240)
                    {
                        y1 = (int)(y + 20 * 13 - 240);
                        e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.DarkCyan, x1, y1);
                    }
                    else
                        y1 = (int)(y + 20 * 13 - (int)(tp.V * 1));
                    if (jkksTime < tp.D && jkjsTime > tp.D)
                        e.Graphics.DrawString("CR", ptzt7, Brushes.DarkCyan, x1 - 2, y1 - 2);
                    else
                        e.Graphics.DrawEllipse(Pens.DarkCyan, new Rectangle(x1 - 2, y1 - 2, 5, 5));
                    if (dyd4 != 0)
                        e.Graphics.DrawLine(Pens.DarkCyan, new Point(x1, y1), lastpoint4);
                    lastpoint4.X = x1 + 3;
                    lastpoint4.Y = y1;
                    dyd4++;
                }
            }

            #endregion

            #region 打印尿量输液输血区域画图
            y = y + 20 * 13; yUnder = y + 13;
            for (int i = 0; i < 11; i++)
            {
                if (i == 9 || i == 7)
                {
                    e.Graphics.DrawLine(Pens.Black, x + 100, y + i * 15, x + 700, y + i * 15);
                }
                else
                    e.Graphics.DrawLine(Pens.Black, x, y + i * 15, x + 700, y + i * 15);
            }
            for (int i = 0; i < 48; i++)
            {
                e.Graphics.DrawLine(pxuxian, (float)(x + 100 + 12.5 * i), (float)(y), (float)(x + 100 + 12.5 * i), (float)(y + 15 * 10));
                if (i % 3 == 0)
                    e.Graphics.DrawLine(Pens.Black, (float)(x + 100 + 12.5 * i), (float)(y), (float)(x + 100 + 12.5 * i), (float)(y + 15 * 10));

            }
            e.Graphics.DrawString("尿 量(ml)", ptzt8, Brushes.Black, x + 10, y + 3);
            #endregion

            #region<<打印尿量>>

            int clCOUNT = 1;
            foreach (adims_MODEL.clcxqt cl in cn)
            {
                TimeSpan t = new TimeSpan();
                t = cl.D - otime;
                int x_nl = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12.5 / jcsjjg + 100 + x);
                if (x_nl > 100 + x && x_nl < 700 + x)
                {
                    e.Graphics.FillRectangle(Brushes.Pink, x_nl, y + 3, 16, 10);
                    e.Graphics.DrawString(cl.V.ToString(), this.Font, Brushes.Black, new Point(x_nl - 2, y + 3));
                    clCOUNT++;
                }
            }
            #endregion

            #region<<打印监护项目>>

            int jhy = 0;
            foreach (string s in jhxmy)
            {
                e.Graphics.DrawString(s, ptzt8, Brushes.Black, new Point(x + 10, jhy * 15 + y + 18));
                foreach (adims_MODEL.jhxm jt in jhxmv)
                {
                    if (jt.Sy == s && jt.V != 0)
                    {
                        if (jt.D >= pagetime && jt.D <= pagetime.AddMinutes(60 * jcsjjg))
                        {
                            TimeSpan t = new TimeSpan();
                            t = jt.D - otime;
                            int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12.5 / jcsjjg + 100 + x);
                            int y1 = y + 18 + jhy * 15;
                            if (CGUAN && !BGUAN && jt.Sy == "ETCO2")
                            {
                                if (cgTime < jt.D)
                                {
                                    e.Graphics.FillRectangle(Brushes.Pink, x1, y1, 10, 10);
                                    e.Graphics.DrawString(jt.V.ToString(), (jt.V / 100 > 0 ? ptzt6 : ptzt7), Brushes.Black, new Point((jt.V / 100 > 0 ? x1 - 4 : x1 - 2), y1));
                                }
                            }
                            if (CGUAN && BGUAN && jt.Sy == "ETCO2")
                            {
                                if (cgTime < jt.D && jt.D < bgTime)
                                {
                                    e.Graphics.FillRectangle(Brushes.Pink, x1, y1, 10, 10);
                                    e.Graphics.DrawString(jt.V.ToString(), (jt.V / 100 > 0 ? ptzt6 : ptzt7), Brushes.Black, new Point((jt.V / 100 > 0 ? x1 - 4 : x1 - 2), y1));
                                }
                            }
                            else
                            {
                                e.Graphics.FillRectangle(Brushes.Pink, x1, y1, 10, 10);
                                e.Graphics.DrawString(jt.V.ToString(), (jt.V / 100 > 0 ? ptzt6 : ptzt7), Brushes.Black, new Point((jt.V / 100 > 0 ? x1 - 4 : x1 - 2), y1));
                            }

                        }
                    }
                }
                jhy++;
            }
            #endregion
            e.Graphics.DrawString("输 血(ml)", ptzt8, Brushes.Black, new Point(x + 10, y + 15 * 6 + 10));

            #region<<打印输血>>
            int sxCount = 0;
            foreach (adims_MODEL.shuxue sx in sx1ist)//画输血
            {
                TimeSpan t = new TimeSpan();
                t = sx.Kssj - pagetime;
                int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12.5 / jcsjjg + 100) + x;
                int y1 = 0;
                if (sxCount % 2 == 0)
                    y1 = y + 15 * 6 + 7;
                else
                    y1 = y + 15 * 7 + 8;
                if (x1 > 100 + x && x1 < 700 + x)
                {
                    e.Graphics.DrawString(sx.Name.ToString() + " " + sx.Jl.ToString(), ptzt7, Brushes.Blue, x1 - 10, y1 - 8);
                    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 3, y1 + 6), new Point(x1 + 3, y1 + 6) });
                }
            }

            #endregion
            e.Graphics.DrawString("输 液(ml)", ptzt8, Brushes.Black, new Point(x + 10, y + 15 * 8 + 10));

            #region<<打印输液>>

            int syCount = 0;
            foreach (adims_MODEL.shuye sy in sy1ist)
            {
                TimeSpan t = new TimeSpan();
                t = sy.Kssj - pagetime;
                int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12.5 / jcsjjg + 100) + x;
                int y1 = 0;
                if (syCount % 2 == 0)
                    y1 = y + 15 * 8 + 7;
                else
                    y1 = y + 15 * 9 + 8;
                if (x1 > 100 + x && x1 < 700 + x)
                {
                    e.Graphics.DrawString(sy.Name.ToString() + " " + sy.Jl.ToString(), ptzt7, Brushes.Blue, x1 - 10, y1 - 8);
                    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 3, y1 + 6), new Point(x1 + 3, y1 + 6) });
                }
                syCount++;
            }
            #endregion

            #region<<打印入出室，插拔管>>
            y = y + 150; yUnder = y + 15;
            e.Graphics.DrawLine(Pens.Black, x, y, x + 700, y);
            e.Graphics.DrawString("治疗序号", ptzt8, Brushes.Black, x + 20, y + 2);
            DataTable dt = pacuDal.GetPACU(mzjldID);
            //术中事件
            int num = 1;
            foreach (adims_MODEL.szsj s in sjList)
            {
                if (s.D >= pagetime && s.D <= pagetime.AddHours(jcsjjg))
                {
                    TimeSpan t = s.D - pagetime;
                    float tx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12.5 / jcsjjg + 95 + x);
                    e.Graphics.FillEllipse(Brushes.Pink, tx, y + 8, 9, 9);
                    e.Graphics.DrawString(num.ToString(), ptzt7, Brushes.Black, tx, y + 8);
                    num++;
                }
            }
            //用药记录
            int num1 = 1;
            foreach (adims_MODEL.tsyy s in yyList)
            {
                if (s.D >= pagetime && s.D <= pagetime.AddHours(jcsjjg))
                {
                    TimeSpan t = s.D - pagetime;
                    float tx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 12.5 / jcsjjg + 95 + x);
                    e.Graphics.FillRectangle(Brushes.LightGreen, tx, y + 8, 9, 9);
                    e.Graphics.DrawString(num1.ToString(), ptzt7, Brushes.Black, tx, y + 8);
                    num1++;
                }
            }
            if (dt.Rows.Count != 0)
            {
                if (dt.Rows[0]["rssj"].ToString() != "")
                {
                    rsTime = Convert.ToDateTime(dt.Rows[0]["rssj"]);
                    TimeSpan ts1 = rsTime - pagetime;
                    int xx1 = (int)((ts1.Days * 24 * 60 + ts1.Hours * 60 + ts1.Minutes) * 12.5 / jcsjjg + 95 + x);
                    if (xx1 >= 95 + x && xx1 < 700 + x)
                    {
                        e.Graphics.DrawString("＞", this.Font, Brushes.Black, xx1, y);
                    }
                }
                if (dt.Rows[0]["cssj"].ToString() != "")
                {
                    csTime = Convert.ToDateTime(dt.Rows[0]["cssj"]);
                    TimeSpan ts2 = csTime - pagetime;
                    int xx1 = (int)((ts2.Days * 24 * 60 + ts2.Hours * 60 + ts2.Minutes) * 12.5 / jcsjjg + 95 + x);
                    if (xx1 >= 95 + x && xx1 < 700 + x)
                    {
                        e.Graphics.DrawString("＜", this.Font, Brushes.Black, xx1, y);
                    }
                }

                if (dt.Rows[0]["CGSJ"].ToString() != "")
                {
                    cgTime = Convert.ToDateTime(dt.Rows[0]["CGSJ"]);
                    TimeSpan ts3 = cgTime - pagetime;
                    int xx1 = (int)((ts3.Days * 24 * 60 + ts3.Hours * 60 + ts3.Minutes) * 12.5 / jcsjjg + 95 + x);
                    if (xx1 >= 95 + x && xx1 < 700 + x)
                    {
                        e.Graphics.DrawString("Θ", this.Font, Brushes.Black, xx1, y);
                    }

                }
                if (dt.Rows[0]["bgsj"].ToString() != "")
                {
                    bgTime = Convert.ToDateTime(dt.Rows[0]["bgsj"]);
                    TimeSpan ts4 = bgTime - pagetime;
                    int xx1 = (int)((ts4.Days * 24 * 60 + ts4.Hours * 60 + ts4.Minutes) * 12.5 / jcsjjg + 95 + x);
                    if (xx1 >= 95 + x && xx1 < 700 + x)
                    {
                        e.Graphics.DrawString("Φ", this.Font, Brushes.Black, xx1, y);
                    }
                }
            }

            #endregion

            y = y + 20; yUnder = y + 15;
            e.Graphics.DrawLine(Pens.Black, x, y, x + 700, y);
            e.Graphics.DrawString("用药记录", textfront, Brushes.Black, x + 100, y + 3);
            e.Graphics.DrawString("治疗记录", textfront, Brushes.Black, x + 500, y + 3);
            e.Graphics.DrawLine(Pens.Black, x + 350, y, x + 350, y + 270);
            y = y + 20; yUnder = y + 15;
            e.Graphics.DrawLine(Pens.Black, x, y, x + 700, y);
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                e.Graphics.DrawString(listBox1.Items[i].ToString(), ptzt8, Brushes.Black, x + 10, y - 130 + (i + 9) * 15);
            }
            for (int i = 0; i < listBox3.Items.Count; i++)
            {
                e.Graphics.DrawString(listBox3.Items[i].ToString(), ptzt8, Brushes.Black, x + 350, y - 130 + (i + 9) * 15);
            }
            y = y + 220; yUnder = y + 15;
            e.Graphics.DrawString("尿量 " + tbNiaoliang.Text + "  ml", textfront, Brushes.Black, x + 30, y);
            e.Graphics.DrawLine(Pens.Black, x + 60, yUnder, x + 160, yUnder);
            e.Graphics.DrawString("输液 " + tbShuye.Text + "  ml", textfront, Brushes.Black, x + 200, y);
            e.Graphics.DrawLine(Pens.Black, x + 230, yUnder, x + 330, yUnder);
            e.Graphics.DrawString("麻醉医师 " + txtMZYS.Controls[0].Text, textfront, Brushes.Black, x + 350, y);
            e.Graphics.DrawLine(Pens.Black, x + 400, yUnder, x + 520, yUnder);
            e.Graphics.DrawString("执行护士 " + txtZXHS.Controls[0].Text, textfront, Brushes.Black, x + 530, y);
            e.Graphics.DrawLine(Pens.Black, x + 580, yUnder, x + 690, yUnder);
            e.Graphics.DrawString("记录符号：血压 ∧ ∨ 入室 ＞ 出室 ＜ 呼吸 ○ 心率 ● 体温 △ 置管Θ 拔管 Φ", txText, Brushes.Black, x + 80, 930);

            if (ptime < dtEnd)//判断是否打印多页
            {
                e.HasMorePages = true;
                e.Graphics.DrawString("第 " + iYema.ToString() + " 页", ptzt8, Brushes.Black, new Point(330 + x, 950));
                //e.Graphics.DrawLine(ptp, new Point(350 + x, 1005 + y), new Point(365 + x, 1005 + y));
                iYema++;
            }
            else
            {
                e.HasMorePages = false; ptime = fristopen;
                e.Graphics.DrawString("第 " + iYema.ToString() + " 页", ptzt8, Brushes.Black, new Point(330 + x, 950));
            }
        }
        private void btnSzsj_Click(object sender, EventArgs e)
        {
            PACU_AddYyao fromszsj = new PACU_AddYyao(yyList, mzjldID);
            fromszsj.ShowDialog();
            BindYongyao();
            pictureBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string zid = this.tbZhuyuanID.Text;
            string pid = PatID;
            int a = at.update_PACU(this.tbWeight.Text.Trim(), zid, pid);
            if (a >= 1)
            {
                MessageBox.Show("保存PACU记录单成功");
            }
            else
            {
                MessageBox.Show("保存PACU记录单失败！");
            }
        }

        private void btnTsyy_Click(object sender, EventArgs e)
        {
            addszsj fromszsj = new addszsj(sjList, mzjldID, 1);
            fromszsj.ShowDialog();
            BindShijian();
            pictureBox1.Refresh();
        }

        private void txtMZYS_DoubleClick(object sender, EventArgs e)
        {
            SelectYSorHS F1 = new SelectYSorHS(1, txtMZYS, PatID);
            F1.ShowDialog();
        }

        private void txtMZYS_TextChanged(object sender, EventArgs e)
        {
            int i = pacuDal.UpdatePACU_mzys(mzjldID, txtMZYS.Controls[0].Text);
        }

        private void txtZXHS_TextChanged(object sender, EventArgs e)
        {
            int i = pacuDal.UpdatePACU_zxhs(mzjldID, txtZXHS.Controls[0].Text);
        }

        private void txtZXHS_DoubleClick(object sender, EventArgs e)
        {
            SelectYSorHS F1 = new SelectYSorHS(2, txtZXHS, PatID);
            F1.ShowDialog();
        }

        private void btnLeft_Click(object sender, EventArgs e)//向左移动
        {
            otime = otime.AddMinutes(jcsjjg * 6);
            lbTime0.Text = otime.ToString("HH:mm");
            lbTime1.Text = otime.AddMinutes(jcsjjg * 6).ToString("HH:mm");
            lbTime2.Text = otime.AddMinutes(jcsjjg * 12).ToString("HH:mm");
            lbTime3.Text = otime.AddMinutes(jcsjjg * 18).ToString("HH:mm");
            lbTime4.Text = otime.AddMinutes(jcsjjg * 24).ToString("HH:mm");
            lbTime5.Text = otime.AddMinutes(jcsjjg * 30).ToString("HH:mm");
            lbTime6.Text = otime.AddMinutes(jcsjjg * 36).ToString("HH:mm");
            lbTime7.Text = otime.AddMinutes(jcsjjg * 42).ToString("HH:mm");
            rushiFlag.Location = new Point(rushiFlag.Location.X - 120, rushiFlag.Location.Y);
            chushiFlag.Location = new Point(chushiFlag.Location.X - 120, chushiFlag.Location.Y);
            //ssks1.Location = new Point(ssks1.Location.X - 120, ssks1.Location.Y);
            //ssjs1.Location = new Point(ssjs1.Location.X - 120, ssjs1.Location.Y);
            cgFlag.Location = new Point(cgFlag.Location.X - 120, cgFlag.Location.Y);
            bgFlag.Location = new Point(bgFlag.Location.X - 120, bgFlag.Location.Y);
            //插管，拔管时间暂时未完成
            lab1.Text = "";
            pictureBox1.Refresh();
        }

        private void btnRight_Click(object sender, EventArgs e)//向右移动
        {
            otime = otime.AddMinutes(-jcsjjg * 6);
            lbTime0.Text = otime.ToString("HH:mm");
            lbTime1.Text = otime.AddMinutes(jcsjjg * 6).ToString("HH:mm");
            lbTime2.Text = otime.AddMinutes(jcsjjg * 12).ToString("HH:mm");
            lbTime3.Text = otime.AddMinutes(jcsjjg * 18).ToString("HH:mm");
            lbTime4.Text = otime.AddMinutes(jcsjjg * 24).ToString("HH:mm");
            lbTime5.Text = otime.AddMinutes(jcsjjg * 30).ToString("HH:mm");
            lbTime6.Text = otime.AddMinutes(jcsjjg * 36).ToString("HH:mm");
            lbTime7.Text = otime.AddMinutes(jcsjjg * 42).ToString("HH:mm");
            rushiFlag.Location = new Point(rushiFlag.Location.X + 120, rushiFlag.Location.Y);
            chushiFlag.Location = new Point(chushiFlag.Location.X + 120, chushiFlag.Location.Y);
            //ssks1.Location = new Point(ssks1.Location.X + 20 * jcsjjg, ssks1.Location.Y);
            //ssjs1.Location = new Point(ssjs1.Location.X + 20 * jcsjjg, ssjs1.Location.Y);
            cgFlag.Location = new Point(cgFlag.Location.X + 120, cgFlag.Location.Y);
            bgFlag.Location = new Point(bgFlag.Location.X + 120, bgFlag.Location.Y);
            lab1.Text = "";
            pictureBox1.Refresh();
        }

        private void cmbSJJG_SelectedIndexChanged(object sender, EventArgs e)//修改检测间隔时间
        {
            int i = pacuDal.UpdatePACU_jcsjjg(mzjldID, cmbSJJG.Text.Trim());
            if (i > 0)
            {
                BindShijiandian();
                BindRSCSCGBG();
                pictureBox1.Refresh();
            }
        }

        private void AddPointTSMenu_Click(object sender, EventArgs e)
        {
            PointManage slj = new PointManage(mzjldID, 1);
            slj.ShowDialog();
            BindJHDian();
            this.pictureBox1.Refresh();
        }

        private void PACU_SZ_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.StopTransfer();
                _serialPort.Close();
            }
            if (ThreadExist)
            {
                ThreadExist = false;
                Receiving_xy.Abort();//制结束线程运行
                Receiving_xy = null; ////在.NET中，有GC垃圾回收，如果这里不赋为null，有可能老半天也不回收内存，甚至永远这么占着，导致内存泄漏。
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BindJikongTime()//绑定机控开始结束时间
        {
            DataTable dtMZ_Info = bll.SelectOldPatInfo1(mzjldID);
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
            JKTimeSet f1 = new JKTimeSet(mzjldID, 1);
            f1.ShowDialog();
            BindJikongTime();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int a = at.update_otimePACU(this.dtInRoomTime.Value.ToString("yyyy-MM-dd HH:mm"), MzjldID);
            if (a > 0)
            {
                otime = this.dtInRoomTime.Value;
                BindShijiandian();
                BindRSCSCGBG();
                pictureBox2.Refresh();
            }
        }

        private void PACU_SZ_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnMonitor.Text == "结束监测")
            {
                MessageBox.Show("检测没有结束，不能关闭");
                e.Cancel = true;
                return;
            }
            if (!rushi)
            {
                MessageBox.Show("入室没有标记，不能结束");
                e.Cancel = true;
                return;
            }
            if (!chushi)
            {
                MessageBox.Show("出室没有标记，不能结束");
                e.Cancel = true;
                return;
            }
            foreach (adims_MODEL.mzqt mzqt in mzqtList)
            {
                if (mzqt.Bz == 1)
                {
                    MessageBox.Show("气体使用没有结束，不能关闭");
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void DeleteCGBGStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = dal.Update_PACU_CGBGSJ(mzjldID);
            if (i == 0)
            {
                MessageBox.Show("选择修改不成功，请重试!");
            }
            else
            {
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                bgFlag.Visible = false;
                CGUAN = false;
                cgTime = new DateTime(1900, 1, 1);
                bgTime = new DateTime(1900, 1, 1);
                bgFlag.MouseDown -= new MouseEventHandler(bgFlag_MouseDown);
                bgFlag.MouseMove -= new MouseEventHandler(bgFlag_MouseMove);
                bgFlag.MouseUp -= new MouseEventHandler(bgFlag_MouseUp);
                bgFlag.MouseLeave -= new EventHandler(bgFlag_MouseLeave);

                BGUAN = false;
                cgFlag.Visible = false;
                cgFlag.MouseDown -= new MouseEventHandler(cgFlag_MouseDown);
                cgFlag.MouseMove -= new MouseEventHandler(cgFlag_MouseMove);
                cgFlag.MouseUp -= new MouseEventHandler(cgFlag_MouseUp);
                cgFlag.MouseLeave -= new EventHandler(cgFlag_MouseLeave);

                pictureBox2.Refresh();
                pictureBox1.Refresh();
            }
        }


    }
}

