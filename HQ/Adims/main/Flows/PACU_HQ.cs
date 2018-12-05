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
using adims_MODEL;
using MODEL;
using System.Runtime.InteropServices;
using WindowsFormsControlLibrary5;
using System.Net.NetworkInformation;
using adims_DAL.Flows;
using adims_DAL.Dics;
using adims_BLL;
using adims_Utility;

namespace main
{
    public partial class PACU_HQ : Form, IMessageFilter 

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
        int HR = 0; bool hr_effective = false;    //心率
        int ETCO2 = 0;
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
        OperScheduleDal _PaibanDal = new OperScheduleDal();
        PacuDal _PacuDal = new PacuDal();
        DateTime rsTime = new DateTime();
        DateTime csTime = new DateTime();
        DateTime cgTime = new DateTime();
        DateTime bgTime = new DateTime();
        DateTime ksjcTime = new DateTime();
        Label lab1 = new Label();
        int jcsjjg = 0;
        DataDicDal _DataDicDal = new DataDicDal();
        List<adims_MODEL.point> pointList = new List<adims_MODEL.point>();
        DateTime fristopen = new DateTime();
        DateTime ptime = new DateTime();
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        MzjldPointDal mpdal = new MzjldPointDal();
        adims_DAL.PacuDal pacuDal = new adims_DAL.PacuDal();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_MODEL.paiban pat = new adims_MODEL.paiban();
        Label rushiFlag = new Label();
        Label chushiFlag = new Label();
        Label ssks1 = new Label();
        Label ssjs1 = new Label();
        Label lb_cguan = new Label();
        Label lb_bguan = new Label();
        int mzjldID = 0;
        int patID = 0;
        string Oroom = "";
       
        adims_MODEL.mzqt t_mzqt = new adims_MODEL.mzqt();
        adims_MODEL.mzyt t_mzyt = new adims_MODEL.mzyt();
        adims_MODEL.jtytsx t_jtytsx = new adims_MODEL.jtytsx();
        adims_MODEL.shuye t_shuye = new adims_MODEL.shuye();
        adims_MODEL.shuxue t_shuxue = new adims_MODEL.shuxue();
        adims_MODEL.shuxue t_yongyao = new adims_MODEL.shuxue();
        adims_MODEL.clcxqt t_niaoliang = new adims_MODEL.clcxqt();
        adims_MODEL.clcxqt t_yll = new adims_MODEL.clcxqt();
        adims_MODEL.clcxqt t_shixue = new adims_MODEL.clcxqt();
        adims_MODEL.tsyy t_tsyy = new adims_MODEL.tsyy();
        adims_MODEL.szsj t_szsj = new adims_MODEL.szsj();
        int flagP1 = 0, TypeP1 = 0;
        int flagPoint = 0;
        int p3lf1 = 0, p3lf2 = 0;//入室茶馆开始结束标签鼠标按下标志
        //int flagP1 = 0, p4lx = 0;//取点标志，取点的类型
        //int p5f = 0, p5lx = 0;//取点标志，取点的类型
        //int p2f = 0, p2lx = 0, p2xi = 0;
        bool rushi = false, chushi = true, ssks = false, ssjs = true, CGUAN = false, BGUAN = false;


        adims_MODEL.point p3t;
        adims_MODEL.tw_point tw_p3t;
        float p3x = 0; float p3y = 0;
        private List<string> jhxma = new List<string>();//所有监护项目
        private List<string> jhxmy = new List<string>();//已添加监护项目
        public List<adims_MODEL.point> ssy = new List<adims_MODEL.point>();//收缩的点
        public List<adims_MODEL.point> szy = new List<adims_MODEL.point>();//舒张压的点
        public List<adims_MODEL.point> mbo = new List<adims_MODEL.point>();//脉搏的点
        public List<adims_MODEL.point> etco2List = new List<adims_MODEL.point>();//
        public List<adims_MODEL.point> spo2List = new List<adims_MODEL.point>();//
        public List<adims_MODEL.point> xl = new List<adims_MODEL.point>();//心率
        public List<adims_MODEL.tw_point> tw = new List<adims_MODEL.tw_point>();//体温的点
        public List<adims_MODEL.point> hxl = new List<adims_MODEL.point>();//呼吸率的点
        public List<adims_MODEL.jhxm> jhxmv = new List<adims_MODEL.jhxm>();//监护项目值
        public List<adims_MODEL.szsj> sjList = new List<adims_MODEL.szsj>();//术中事件
        public List<adims_MODEL.clcxqt> cxList = new List<adims_MODEL.clcxqt>();//出血
        public List<adims_MODEL.clcxqt> cnList = new List<adims_MODEL.clcxqt>();//出尿其他出量
        public List<adims_MODEL.clcxqt> yllList = new List<adims_MODEL.clcxqt>();//引流量
        public List<adims_MODEL.mzqt> qtLIST = new List<adims_MODEL.mzqt>();//出尿出血其他出量
        public List<adims_MODEL.mzqt> mzqtList = new List<adims_MODEL.mzqt>();
        public List<adims_MODEL.mzyt> mzytList = new List<adims_MODEL.mzyt>();
        public List<adims_MODEL.jtytsx> mzjtList = new List<adims_MODEL.jtytsx>();
        public List<adims_MODEL.jtytsx> jiaot1 = new List<adims_MODEL.jtytsx>();
        public List<adims_MODEL.shuye> sy1ist = new List<adims_MODEL.shuye>();
        public List<adims_MODEL.shuxue> sx1ist = new List<adims_MODEL.shuxue>();
        public List<adims_MODEL.tsyy> yyList = new List<adims_MODEL.tsyy>();
        Point lastpoint = new Point();//画线的时候保存上一个点
        Point lastpoint1 = new Point();//画线的时候保存上一个点
        Point lastpoint2 = new Point();//画线的时候保存上一个点
        Point lastpoint3 = new Point();//画线的时候保存上一个点
        Point lastpoint4 = new Point();//画线的时候保存上一个点

        DateTime otime = new DateTime();

        string PatID, MzjldID;

        #endregion
        MzjldDal _MzjldDal = new MzjldDal();
        DateTime MzjldMaxTime = new DateTime();
        DateTime jkksTime = new DateTime();
        DateTime jkjsTime = new DateTime();
        #region IMessageFilter 成员          
        public bool PreFilterMessage(ref Message m) 
        {             
            if (m.Msg == 522) 
            { return true; } 
            else 
            { return false; }  
        }           
        #endregion
        string Odate;
        public PACU_HQ(string patid, string mzid,string date)
        {
            PatID = patid;
            MzjldID = mzid;
            Odate = date;
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
            int x = 0, y = 0;
            e.Graphics.DrawLine(Pens.Black, x, y, x, y + 800);//画第一条竖线
            e.Graphics.DrawLine(Pens.Black, x, y, x + 99, y);            
            for (int i = 0; i <= 11; i++)
            {
                e.Graphics.DrawLine(Pens.Black, x, y + 360 + i * 20, x + 99, y + 360 + i * 20);
               
            } 
            //int jhy = 0;
            //foreach (string s in jhxmy)
            //{
            //    e.Graphics.DrawString(s, this.Font, Brushes.Black, new Point(x + 10, jhy * 20 + y + 375));
            //    jhy++;
            //}
        }
        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            int x = 0, y = 0;
            e.Graphics.DrawLine(Pens.Black, x, y, x, y + 800);//画第一条竖线
            e.Graphics.DrawLine(Pens.Black, x + 1060, y, x + 1060, y + 800);//画第二条竖线
            e.Graphics.DrawLine(Pens.Black, x, y, x + 1060, y);//画第一条竖线
            //e.Graphics.DrawLine(Pens.Black, x + 1060, y, x + 1060, y + 800);//画第二条竖线           
        }

        private void BindShuxueList()
        {
            DataTable dtSx = pacuDal.GetsxPACU(mzjldID);
            if (dtSx.Rows.Count != 0)// 输血赋值
            {
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
                    if (Convert.ToInt32(dr["Cxyy"]) == 1)
                        sx1ist[j].Cxyy = true;
                    else
                        sx1ist[j].Cxyy = false;
                    if (sx1ist[j].Bz == 2)
                    {
                        sx1ist[j].Jssj = Convert.ToDateTime(dr["jssj"]);
                    }
                    j++;
                }
            }
        }
       
        private void BindShuyeList()
        {
            DataTable dtSy = pacuDal.GetsyPACU(mzjldID);
            if (dtSy.Rows.Count != 0)// 液体赋值
            {
                int j = 0;
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
                    if (Convert.ToInt32(dr["Cxyy"]) == 1)
                        sy1ist[j].Cxyy = true;
                    else
                        sy1ist[j].Cxyy = false;
                    if (sy1ist[j].Bz == 2)
                    {
                        sy1ist[j].Jssj = Convert.ToDateTime(dr["jssj"]);
                    }
                    j++;
                }
            }
        }

        private void BindClList()
        {
            DataTable dtCL = pacuDal.GetclcxPACU(mzjldID,2);
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
            DataTable dtCL = pacuDal.GetclcxPACU(mzjldID, 3);
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
        private void BindCxList()
        {
            DataTable dtCL = pacuDal.GetclcxPACU(mzjldID,1);
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
        private void BindQtList()
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

        private void BindPatInfo()
        {
            DataTable dt = new DataTable();
            dt = _PaibanDal.GetPaibanByPatId(PatID);
            tbPatname.Text = dt.Rows[0]["Patname"].ToString();
            tbZhuyuanID.Text = dt.Rows[0]["PatZhuYuanID"].ToString();
            tbBedNO.Text = dt.Rows[0]["Patbedno"].ToString();
            //tbShoushuName.Text = dt.Rows[0]["Oname"].ToString();
            tbBingqu.Text = dt.Rows[0]["Patdpm"].ToString();
            tbSex.Text = dt.Rows[0]["patsex"].ToString();
            tbAge.Text = dt.Rows[0]["patage"].ToString();
            tbWeight.Text = dt.Rows[0]["patWeight"].ToString();
        }
        private void BindShijian()
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
        private void BindYongyao()
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
            string zsfs = "";//注射方式
            listBox1.Items.Clear();
            foreach (adims_MODEL.tsyy s in yyList)
            {
                if (s.Yyfs == "口服")
                {
                    zsfs = "po";
                }
                if (s.Yyfs == "静脉滴注")
                {
                    zsfs = "ivdrip";
                }
                if (s.Yyfs == "皮下注射")
                {
                    zsfs = "ih";
                }
                if (s.Yyfs == "肌肉注射")
                {
                    zsfs = "im";
                }
                if (s.Yyfs == "静脉注射")
                {
                    zsfs = "iv";
                }
                if (s.Yyfs == "皮下注射")
                {
                    zsfs = "id";
                }
                listBox1.Items.Add(j.ToString() + "." + s.Name + " " + s.Yl.ToString() + s.Dw +" "+ zsfs);
                j++;
            }
        }
        private void BindShijiandian()
        {
            jhxma.Clear();
            //jhxma.Add("ECG");
            //jhxma.Add("CVP");
            //jhxma.Add("NIBP");
            //jhxma.Add("ART");
            //jhxma.Add("RESP");
            //jhxma.Add("BIS");
            //jhxma.Add("TOF");
            jhxma.Add("ETCO2");
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
       
        /// <summary>
        /// 显示界面
        /// </summary>
        private void BindFrom() 
        {
            DataTable dt = _PacuDal.GetPACU_ByMzjldId(Convert.ToInt32(MzjldID));
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                if (Convert.ToString(dr["mzzl"]).Contains("1")) cbMZFF1.Checked = true;
                if (Convert.ToString(dr["mzzl"]).Contains("2")) cbMZFF2.Checked = true;
                if (Convert.ToString(dr["mzzl"]).Contains("3")) cbMZFF3.Checked = true;
                if (Convert.ToString(dr["mzzl"]).Contains("4")) cbMZFF4.Checked = true;
                if (Convert.ToString(dr["mzzl"]).Contains("5")) cbMZFF5.Checked = true;
                if (Convert.ToString(dr["mzzl"]).Contains("6")) cbMZFF6.Checked = true;
                if (Convert.ToString(dr["mzzl"]).Contains("7")) cbMZFF7.Checked = true;
                if (Convert.ToString(dr["mzzl"]).Contains("8")) cbMZFF8.Checked = true;
                if (Convert.ToString(dr["mzzl"]).Contains("9")) cbMZFF9.Checked = true;
                if (Convert.ToString(dr["mzzl"]).Contains("a")) cbMZFFa.Checked = true;
                if (Convert.ToString(dr["mzzl"]).Contains("b")) cbMZFFb.Checked = true;
                if (Convert.ToString(dr["mzzl"]).Contains("c")) cbMZFFc.Checked = true;

                if (Convert.ToString(dr["Sztsqk"]).Contains("1")) cbSQTSQK1.Checked = true;
                if (Convert.ToString(dr["Sztsqk"]).Contains("2")) cbSQTSQK2.Checked = true;
                if (Convert.ToString(dr["Sztsqk"]).Contains("3")) cbSQTSQK3.Checked = true;
                if (Convert.ToString(dr["Sztsqk"]).Contains("4")) cbSQTSQK4.Checked = true;
                if (Convert.ToString(dr["Sztsqk"]).Contains("5")) cbSQTSQK5.Checked = true;
                if (Convert.ToString(dr["Sztsqk"]).Contains("6")) cbSQTSQK6.Checked = true;
                if (Convert.ToString(dr["Sztsqk"]).Contains("7")) cbSQTSQK7.Checked = true;

                if (Convert.ToString(dr["Tiwei"]).Contains("1")) cbTiwei1.Checked = true;
                if (Convert.ToString(dr["Tiwei"]).Contains("2")) cbTiwei2.Checked = true;
                if (Convert.ToString(dr["Tiwei"]).Contains("3")) cbTiwei3.Checked = true;
                if (Convert.ToString(dr["Tiwei"]).Contains("4")) cbTiwei4.Checked = true;
                if (Convert.ToString(dr["Tiwei"]).Contains("5")) cbTiwei5.Checked = true;
                if (Convert.ToString(dr["Tiwei"]).Contains("6")) cbTiwei6.Checked = true;
                if (Convert.ToString(dr["Tiwei"]).Contains("7")) cbTiwei7.Checked = true;
                if (Convert.ToString(dr["Tiwei"]).Contains("8")) cbTiwei8.Checked = true;
                if (Convert.ToString(dr["Tiwei"]).Contains("9")) cbTiwei9.Checked = true;
                this.tbTiweiOther.Text = dr["qt"].ToString();

                if (Convert.ToString(dr["Qdtq"]).Contains("1")) cbQDTQ1.Checked = true;
                if (Convert.ToString(dr["Qdtq"]).Contains("2")) cbQDTQ2.Checked = true;
                if (Convert.ToString(dr["Qdtq"]).Contains("3")) cbQDTQ3.Checked = true;
                if (Convert.ToString(dr["Qdtq"]).Contains("4")) cbQDTQ4.Checked = true;
                if (Convert.ToString(dr["Qdtq"]).Contains("5")) cbQDTQ5.Checked = true;
                if (Convert.ToString(dr["Qdtq"]).Contains("6")) cbQDTQ6.Checked = true;
                if (Convert.ToString(dr["Qdtq"]).Contains("7")) cbQDTQ7.Checked = true;
                if (Convert.ToString(dr["Qdtq"]).Contains("8")) cbQDTQ8.Checked = true;
                if (Convert.ToString(dr["Qdtq"]).Contains("9")) cbQDTQ9.Checked = true;
                if (Convert.ToString(dr["Qdtq"]).Contains("a")) cbQDTQa.Checked = true;
                if (Convert.ToString(dr["Qdtq"]).Contains("b")) cbQDTQb.Checked = true;
                if (Convert.ToString(dr["Qdtq"]).Contains("c")) cbQDTQc.Checked = true;
                if (Convert.ToString(dr["Qdtq"]).Contains("d")) cbQDTQd.Checked = true;
                this.txtCQL.Text = dr["Cql"].ToString();
                this.txtHXPL.Text = dr["Hxpl"].ToString();

                if (Convert.ToString(dr["jcjl"]).Contains("1")) cbJCJL1.Checked = true;
                if (Convert.ToString(dr["jcjl"]).Contains("2")) cbJCJL2.Checked = true;
                if (Convert.ToString(dr["jcjl"]).Contains("3")) cbJCJL3.Checked = true;
                if (Convert.ToString(dr["jcjl"]).Contains("4")) cbJCJL4.Checked = true;
                if (Convert.ToString(dr["jcjl"]).Contains("5")) cbJCJL5.Checked = true;
                if (Convert.ToString(dr["jcjl"]).Contains("6")) cbJCJL6.Checked = true;
                if (Convert.ToString(dr["jcjl"]).Contains("7")) cbSP02.Checked = true;
                if (Convert.ToString(dr["jcjl"]).Contains("8")) cbXinDT.Checked = true;
                if (Convert.ToString(dr["jcjl"]).Contains("9")) cbOTHER.Checked = true;
                this.txtmFZ.Text = dr["Mfz"].ToString();
                if (Convert.ToString(dr["Yszt"]).Contains("1")) cbYishi1.Checked = true;
                if (Convert.ToString(dr["Yszt"]).Contains("2")) cbYishi2.Checked = true;
                if (Convert.ToString(dr["Yszt"]).Contains("3")) cbYishi3.Checked = true;

                if (Convert.ToString(dr["Pf"]).Contains("1")) cbPifu1.Checked = true;
                if (Convert.ToString(dr["Pf"]).Contains("2")) cbPifu2.Checked = true;
                if (Convert.ToString(dr["Pf"]).Contains("3")) cbPifu3.Checked = true;
                if (Convert.ToString(dr["Pf"]).Contains("4")) cbPifu4.Checked = true;
                if (Convert.ToString(dr["Pf"]).Contains("5")) cbPifu5.Checked = true;
                this.tbTSZS.Text = dr["Tszs"].ToString();                
                this.cmbMZYS.Text = dr["shMzys"].ToString();
                if ( dr["Shssys"].ToString()!="")
                {
                    this.cmbSSYS.Text = dr["Shssys"].ToString();
                }              
                this.tbHFSQK.Text = dr["Lhfsqk"].ToString();
                this.txtZXHS.Text = dr["ZXHS"].ToString();
                this.txtMZYS.Text = dr["MZYS"].ToString();
                this.dtInRoomTime.Text = dr["otime"].ToString();
              ///调用麻醉记录单的数据
                DataTable dtMzjld = _MzjldDal.GetMzjldByMzjldId(mzjldID);
                if (dtMzjld.Rows.Count>0)
                {
                    this.cmbSSYS.Text = dtMzjld.Rows[0]["ssys"].ToString();
                    this.tbShoushuName.Text = dtMzjld.Rows[0]["ShoushuFS"].ToString();
                }
            }
        }
        /// <summary>
        /// 绑定麻醉医师
        /// </summary>
        private void BindMZYY()
        {
            DataTable dtMZYS = _DataDicDal.GetAllMZYS();
            cmbMZYS.Items.Clear();
            for (int i = 0; i < dtMZYS.Rows.Count; i++)
            {
                this.cmbMZYS.Items.Add(dtMZYS.Rows[i][0]);
            }
        }      
        private void PACU_SZ_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(this); 
            this.txtMZYS.DoubleClick += new System.EventHandler(this.txtMZYS_DoubleClick);
            this.txtZXHS.DoubleClick += new System.EventHandler(this.txtZXHS_DoubleClick);
            this.txtMZYS.TextChanged += new System.EventHandler(this.txtMZYS_TextChanged);
            this.txtZXHS.TextChanged += new System.EventHandler(this.txtZXHS_TextChanged);
            this.tbHFSQK.DoubleClick += new System.EventHandler(this.tbHFSQK_DoubleClick);
            BindPortName();
            BindMZYY();//绑定麻醉医师
            mzjldID = Convert.ToInt32(MzjldID);
            BindPatInfo();
            DataTable dt = pacuDal.GetPACU_ByMzjldId(mzjldID);
            if (dt.Rows.Count == 0)
            {
                jcsjjg = 5;
                cmbSJJG.Text = "5";
               // CheckMzjldAndPacuData();//加载读取麻醉记录单的监护项值

                //if (DateTime.Now.Minute > 30)
                //    otime = DateTime.Now.AddMinutes(-DateTime.Now.Minute + 30);
                //else
                //    otime = DateTime.Now.AddMinutes(-DateTime.Now.Minute);
                DateTime Comparetime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                if (MzjldMaxTime >Comparetime)
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
            else
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
                BindFrom();//界面显示
                BindShijiandian();//绑定时间坐标
            
                BindJikongTime();
                BindRSCSCGBG();
                BindClList();
                BindCxList();
                BindYllList();
                BindQtList();
                BindShuxueList();
                BindShuyeList();
                BindJHDian();
                BindYongyao();
                BindShijian();
                dtVisitDate.Value = Convert.ToDateTime(dt.Rows[0]["SaveTime"]);
                txtMZYS.Text = Convert.ToString(dt.Rows[0]["mzys"]);
                txtZXHS.Text = Convert.ToString(dt.Rows[0]["zxhs"]);

            }
            this.WindowState = FormWindowState.Maximized;
            this.dtInRoomTime.Format = DateTimePickerFormat.Custom;
            this.dtInRoomTime.CustomFormat = "MM-dd HH:mm";
            dtInRoomTime.Value = otime;           


        }

        private void CheckMzjldAndPacuData()
        {
            DataTable ServerMax = mpdal.GetMaxPoint(mzjldID);
            //DataTable LoaclMax = dal.GetMaxTimeLocal(mzjldID);
           
            DateTime PacuInTime = DateTime.Now;

            if (ServerMax.Rows[0][0].ToString() != "")
            {
                MzjldMaxTime = Convert.ToDateTime(ServerMax.Rows[0][0].ToString());
                MzjldMaxTime = MzjldMaxTime.AddMinutes(jcsjjg);
                TimeSpan t1 = PacuInTime - MzjldMaxTime;
                int TimePlus = t1.Days * 60 * 60 + t1.Hours * 60 + t1.Minutes;
                int DataCount = TimePlus / jcsjjg;
                int a = pacuDal.CopyMzjldToPacu(MzjldMaxTime, mzjldID, DataCount);                      
            }
        }
        
        private void BindRSCSCGBG()
        {
            DataTable dt = pacuDal.GetPACU_ByMzjldId(mzjldID);
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
                rushiFlag.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100), 365);
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
                chushiFlag.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100), 365);
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
                lb_cguan.Text = "Θ";
                lb_cguan.AutoSize = true;
                lb_cguan.BackColor = Color.Transparent;
                lb_cguan.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                lb_cguan.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 90), 365);      
                this.pictureBox1.Controls.Add(lb_cguan);
                CGUAN = true;
                BGUAN = false;
                lb_cguan.MouseDown += new MouseEventHandler(lb_cguan_MouseDown);
                lb_cguan.MouseMove += new MouseEventHandler(lb_cguan_MouseMove);
                lb_cguan.MouseUp += new MouseEventHandler(lb_cguan_MouseUp);
                lb_cguan.MouseLeave += new EventHandler(lb_cguan_MouseLeave);
            }
            
            if (dt.Rows[0]["bgsj"].ToString() != "")
            {
                bgTime = Convert.ToDateTime(dt.Rows[0]["bgsj"]);
                TimeSpan t = new TimeSpan();
                t = bgTime - otime;
                lb_bguan.Text = "Φ";               
                lb_bguan.AutoSize = true;
                lb_bguan.BackColor = Color.Transparent;
                lb_bguan.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lb_bguan.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 90), 365);
                
                this.pictureBox1.Controls.Add(lb_bguan);
                BGUAN = true;
                //txtCGJS.Text = DateTime.Now.ToString("HH:mm");
                lb_bguan.MouseDown += new MouseEventHandler(lb_bguan_MouseDown);
                lb_bguan.MouseMove += new MouseEventHandler(lb_bguan_MouseMove);
                lb_bguan.MouseUp += new MouseEventHandler(lb_bguan_MouseUp);
                lb_bguan.MouseLeave += new EventHandler(lb_bguan_MouseLeave);
            }
        }
        MzjldPointDal sh = new MzjldPointDal();
        [DllImport("wininet")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reserverdaValue);
        private void timer1_Tick(object sender, EventArgs e)
        {
            #region 模拟监测点
            //adims_MODEL.point p = new adims_MODEL.point();//收缩压记录点
            //adims_MODEL.point p1 = new adims_MODEL.point();//舒张压记录点
            //adims_MODEL.point p2 = new adims_MODEL.point();//体温记录点
            //adims_MODEL.point p3 = new adims_MODEL.point();//脉搏记录点
            //adims_MODEL.point p4 = new adims_MODEL.point();//呼吸率记录点
            //pointList.Clear();
            //Random r = new Random();//舒张压
            //p.D = test;
            //p.V = r.Next(80, 220);
            //p.Lx = 1;
            //pointList.Add(p);
            ////ssy.Add(p);
            //Random r1 = new Random();//收缩压
            //p1.D = test;
            //p1.V = r1.Next(40, 100);
            //p1.Lx = 2;
            //pointList.Add(p1);
            ////szy.Add(p1);
            //Random r2 = new Random();//体温
            //p2.D = test;
            //p2.V = r2.Next(34, 41);
            //p2.Lx = 3;
            //pointList.Add(p2);
            ////xl.Add(p2);
            //Random r3 = new Random();//心率
            //p3.D = test;
            //p3.V = r3.Next(60, 100);
            //p3.Lx = 4;
            //pointList.Add(p3);
            ////tw.Add(p3);
            //Random r4 = new Random();//呼吸率
            //p4.D = test;
            //p4.V = r4.Next(10, 20);
            //p4.Lx = 5;
            //pointList.Add(p4);
            ////hxl.Add(p4);
            //bll.addpoint_PACU(mzjldID, pointList); // 添加坐标
            //foreach (string s in jhxmy)
            //{
            //    adims_MODEL.jhxm jhxmt = new adims_MODEL.jhxm();
            //    jhxmt.D = test;
            //    jhxmt.V = r.Next(90, 101);
            //    jhxmt.Sy = s;
            //    jhxmv.Add(jhxmt);
            //    // 添加监护数据
            //    int q = bll.addjhxm_PACU(mzjldID, jhxmt);
            //    if (q != 1)
            //    {
            //        MessageBox.Show("错误" + q.ToString());
            //    }
            //}
            #endregion
            int isinnet;
            if (!InternetGetConnectedState(out isinnet, 0))
            {
                // timer1.Enabled = false;
                MessageBox.Show("服务器中断，请检查网线");
                return;
            }
            bool pingIP = TextValueLimit.PingHost("192.168.1.80");
            if ( true)
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
            //CreateTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP
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

                p3.Lx = 3;
                mbo.Add(p3);
                //p3.V = Convert.ToInt32(datadt.Rows[i][3]);
                //p3.Lx = 3;
                //mbo.Add(p3);
            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.point p4 = new adims_MODEL.point();//呼吸记录点
                p4.D = Convert.ToDateTime(datadt.Rows[i][0]);
                p4.V = Convert.ToInt32(datadt.Rows[i][4]);
                p4.Lx = 4;
                hxl.Add(p4);
            }
            for (int i = 0; i < datadt.Rows.Count; i++)
            {
                adims_MODEL.tw_point p5 = new adims_MODEL.tw_point();//体温
                p5.D = Convert.ToDateTime(datadt.Rows[i][0]);
                if (datadt.Rows[i][5].ToString()=="")
                {
                    p5.V = 0;
                }
                else
                {
                    p5.V = Convert.ToSingle(datadt.Rows[i][5]);
                }
             
                p5.Lx = 5;
                tw.Add(p5);
            }
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
            //string temp2 = this.txtMzjldid.Text;
            IPAddressInput1 = IP_address.Text;
            //BedIDInput1 = this.txtMzjldid.Text;
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
                                                    SPO2 =(int)getFloat(recv_Data, contemp3 + 10);
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
                    this.SaveFillipData(PatientData, mzjldID, 1);

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
            //DateTime now = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            if (sh.selectJianCeData(now, mzjldID, type).Rows.Count == 0)
            {
                int fa = 0;
                if (ABP_SYS1 != 0 && ABP_DIA1 != 0 && ABP_MAP1 != 0)
                {
                    if (type == 0)
                        fa = sh.insertJianCeDataMZJLD(mzjldID, ABP_SYS1, ABP_DIA1, ABP_MAP1, RR1, HR1, PR1, SPO21, new Random().Next(32, 35), TEMP1, now);
                    if (type == 1)
                        fa = sh.insertJianCeDataPACU(mzjldID, ABP_SYS1, ABP_DIA1, ABP_MAP1, RR1, HR1, PR1, SPO21, new Random().Next(32, 35), TEMP1, now);
                }
                else
                {
                    if (type == 0)
                        fa = sh.insertJianCeDataMZJLD(mzjldID, SYS1, DIA1, MAP1, RR1, HR1, PR1, SPO21, new Random().Next(32, 35), TEMP1, now);
                    if (type == 1)
                        fa = sh.insertJianCeDataPACU(mzjldID, SYS1, DIA1, MAP1, RR1, HR1, PR1, SPO21, new Random().Next(32, 35), TEMP1, now);
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
            FileStream fs = new FileStream(Application.StartupPath+"\\Config.txt", FileMode.Open);
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
        private void Socket_Setup()
        {
            string temp1 = "192.168.6.221";
            string temp2 = "8000";
            int port = Convert.ToInt32(temp2);
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
                        ProceedreceivedData(1);
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

                MirayModel jhModel = new MirayModel();
                jhModel.OfficeName1 = OfficeName;
                jhModel.BedId1 = mzjldID;
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
                if (dal.selectJianCeData(now, mzjldID, type).Rows.Count == 0)
                {

                    int fa = 0;

                    if (type == 0)
                        //fa = sh.insertJianCeDataMZJLD(mzjldID, Sys, Dia, Mean, RR, HR, PR, SpO2, new Random().Next(32, 35), 0, now);
                        fa = mpdal.insertJianCeDataMZJLD(mzjldID, Sys, Dia, Mean, RR, HR, PR, SpO2, new Random().Next(32, 35), 0, now);
                    if (type == 1)
                        //fa = sh.insertJianCeDataPACU(mzjldID, Sys, Dia, Mean, RR, HR, PR, SpO2, new Random().Next(32, 35), 0, now);
                        fa = dal.insertJianCeDataPACU(mzjldID, Sys, Dia, Mean, RR, HR, PR, SpO2, new Random().Next(32, 35), 0, now);
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

        private void BindPortName()
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
            if (btnMonitor.Text.Equals("开始监测"))
            {
                if (cmbJianhuyi.Text == "")
                {
                    MessageBox.Show("请选择监护仪机型！");
                    return;
                }

                //if (cmbJianhuyi.Text == "GE监护仪")
                //{
                //    _serialPort.PortName = cmbCOM.Text.Trim();
                //    if (_serialPort.IsOpen)
                //    {
                //        _serialPort.StopTransfer();
                //        _serialPort.Close();
                //    }
                //    _serialPort.Open();
                //    GE_Jianhuyi();
                //    cmbJianhuyi.Enabled = false;
                //}
                if (cmbJianhuyi.Text == "宝莱特")
                {
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo.FileName = Application.StartupPath + "\\BLT\\TestHL7SDK.exe";
                    proc.StartInfo.UseShellExecute = false;
                    proc.Start();
                    timerBLT.Interval = 10000;
                    timerBLT.Start();
                    cmbJianhuyi.Enabled = false;
                    cmbCOM.Enabled = false;
                }
                if (cmbJianhuyi.Text == "迈瑞监护仪")
                {
                    MR = 1;
                    procs = new System.Diagnostics.Process();
                    procs.StartInfo.FileName = Application.StartupPath + "\\Mindray\\Mindray.exe";
                    string cz = "0" + "," + mzjldID.ToString();
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
                    //        // SocketThread.Abort();
                    //    }
                    //}
                    cmbJianhuyi.Enabled = false;
                    cmbCOM.Enabled = false;
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
            //if (e.X > 0 && e.X < 100)
            //{
            //    if (e.Y > 475 && e.Y < 575)
            //    {
            //        addJhxm fromjhxm = new addJhxm(jhxma, jhxmy, mzjldID, 1);
            //        fromjhxm.ShowDialog();

            //    }
            //    pictureBox1.Refresh();
            //}
            //pictureBox2.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen pred2 = new Pen(Brushes.Red);
            pred2.Width = 2;
            Pen pxuxian = new Pen(Brushes.Black);
            pxuxian.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            int x_Line = 0, y_Line = 0;
            e.Graphics.DrawLine(Pens.Black, x_Line, y_Line, x_Line, y_Line + 770);//画第一条竖线
            e.Graphics.DrawLine(Pens.Black, x_Line + 1060, y_Line, x_Line + 1060, y_Line + 770);//画右边竖线
            e.Graphics.DrawLine(Pens.Black, x_Line, y_Line, x_Line + 1060, y_Line);//画横线

            for (int i = 0; i <= 24; i++)//画横线
            {           
                    e.Graphics.DrawLine(Pens.Black, x_Line, y_Line + i * 15, x_Line + 1060, y_Line + i * 15);
            }

            for (int i = 0; i <=10; i++)//画横线
            {
                e.Graphics.DrawLine(Pens.Black, x_Line, y_Line + i * 20 + 380, x_Line + 1060, y_Line + i * 20 + 380);
            }
            e.Graphics.DrawLine(Pens.Black, x_Line + 100, y_Line, x_Line + 100, y_Line + 365);
                
            for (int i = 0; i < 9; i++)//画竖线
            {
                e.Graphics.DrawLine(Pens.Black, x_Line + 100 + i * 120, y_Line, x_Line + 100 + i * 120, y_Line + 360);
                e.Graphics.DrawLine(Pens.Black, x_Line + 100 + i * 120, y_Line + 380, x_Line + 100 + i * 120, y_Line + 365);
            }
            for (int i = 0; i < 49; i++)//画竖线
            {
                e.Graphics.DrawLine(pxuxian, x_Line + 100 + i * 20 + 20, y_Line, x_Line + 100 + i * 20 + 20, y_Line + 360);
                e.Graphics.DrawLine(pxuxian, x_Line + 100 + i * 20 + 20, y_Line + 380, x_Line + 100 + i * 20 + 20, y_Line + 365);
            }
            e.Graphics.DrawLine(Pens.Black, x_Line, 770, x_Line + 1060, 770);//画底部横线
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
                        if (csTime > Convert.ToDateTime("1990-01-01"))
                        {
                            t1 = csTime - otime;
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
                    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                    int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 20 / jcsjjg + 100);
                    int y1 = 10;
                    e.Graphics.DrawString(mz.Qtname+mz.Yl.ToString() + mz.Dw.ToString(), this.Font, Brushes.Blue, new Point(x1 - 10, y1 - 8));
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
            Pen red2 = new Pen(Brushes.Red, 2);
            #region  //画收缩压

            foreach (adims_MODEL.point tp in ssy)
            {
                int x, y;
                TimeSpan t = new TimeSpan();
                t = tp.D - otime;
                x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                if (tp.V > 220)
                {
                    y = 30;
                    e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Red, x, y);
                }
                else
                    y = (int)(360 - (int)(tp.V * 1.5));

                //e.Graphics.DrawPolygon(Pens.Red, new Point[3] { new Point(x, y), new Point(x + 5, y + 5), new Point(x - 5, y + 5) });
                e.Graphics.DrawLines(red2, new Point[3] { new Point(x - 4, y - 6), new Point(x, y), new Point(x + 4, y - 6) });

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
                if (tp.V > 220)
                {
                    y = 30 ;
                    e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Red, x, y);
                }
                else
                    y = (int)(360 - (int)(tp.V * 1.5));

                //e.Graphics.DrawPolygon(Pens.Red, new Point[3] { new Point(x, y), new Point(x - 5, y - 5), new Point(x + 5, y - 5) });
                e.Graphics.DrawLines(red2, new Point[3] { new Point(x - 4, y + 6), new Point(x, y), new Point(x + 4, y + 6) });
                if (dyd1 != 0)
                    e.Graphics.DrawLine(Pens.Red, new Point(x, y), lastpoint1);
                lastpoint1.X = x;
                lastpoint1.Y = y;
                dyd1++;
            }
            #endregion

            #region  //画体温
            foreach (adims_MODEL.tw_point tp in tw)
            {
                if (tp.V > 0)
                {
                    float x, y;
                    TimeSpan t = new TimeSpan();
                    t = tp.D - otime;
                    x = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                    if (tp.V > 41)
                    {
                        y = 30;
                        e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Green, x, y);
                    }
                    else
                        y = (float)(360 - (tp.V - 30) * 30);
                    e.Graphics.DrawPolygon(Pens.Maroon, new PointF[3] { new PointF(x, y), new PointF(x + 4, y + 6), new PointF(x - 4, y + 6) });
                    //e.Graphics.FillRectangle(Brushes.Green, new RectangleF(x - 3, y - 3, 6, 6));
                    if (dyd2 != 0)
                        e.Graphics.DrawLine(Pens.Maroon, new PointF(x, y), lastpoint2);
                    lastpoint2.X = (int)(x + 1);
                    lastpoint2.Y = (int)y;
                    dyd2++;
                }
            }
            #endregion

            #region  //画心率
            foreach (adims_MODEL.point tp in mbo)
            {
                int x, y;
                TimeSpan t = new TimeSpan();
                t = tp.D - otime;
                x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                if (tp.V > 220)
                {
                    y = 30;
                    e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Blue, x, y);
                }
                else
                    y = (int)(360 - (int)(tp.V * 1.5));

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
                if (tp.V > 220)
                {
                    y = 30;
                    e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.DarkCyan, x, y);
                }
                else
                    y = (int)(360 - (int)(tp.V * 1.5));

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

            #region  //画术中事件
            int szsji = 1;
            foreach (adims_MODEL.szsj s in sjList)//画术中事件
            {
                TimeSpan t = new TimeSpan();
                t = s.D - otime;
                int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                float y1 = (float)(365);
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
                float y1 = (float)(365);
                //e.Graphics.FillEllipse(Brushes.LightGreen, x1, y1, tsyyi > 9 ? 14 : 10, 10);
                e.Graphics.DrawString(tsyyi.ToString(), this.Font, Brushes.Red, new PointF(tsyyi > 9 ? x1 - 2 : x1, y1));
                tsyyi++;
            }
            #endregion

            #region  //画监护项目
            int jhi = 0;
           
            Font fsmall = new Font("宋体", 7);
            foreach (string s in jhxmy)
            {
                //int count1 = 0;
                e.Graphics.DrawString(s, this.Font, Brushes.Black, new Point(30, jhi * 20 + 2));
                foreach (adims_MODEL.jhxm jt in jhxmv)
                {
                    if (jt.Sy == s)
                    {
                        TimeSpan t = new TimeSpan();
                        t = jt.D - otime;
                        int x_jh = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 90);
                        int y_jh = jhi * 20 + 385;
                        //if (count1 % 2 == 0)
                        //{
                            e.Graphics.FillRectangle(Brushes.Pink, x_jh, y_jh, 14, 12);
                            e.Graphics.DrawString(jt.V.ToString(), (jt.V / 100 > 0 ? fsmall : this.Font), Brushes.Black, new Point((jt.V / 100 > 0 ? x_jh - 2 : x_jh), y_jh));
                        //}
                        //count1++;
                    }
                }
                jhi++;

            }
            #endregion

            #region  //画输液
            int dy3 = 0;
            ArrayList sssSY = new ArrayList();
            foreach (adims_MODEL.shuye sy in sy1ist)//画输液
            {
                if (dy3>3)
                {
                    break;
                }
                if (sy.Bz > 0)
                {
                    if (sssSY.Contains(sy.Name))
                    {
                        dy3 = dy3 - 1;
                    }
                    //e.Graphics.DrawString(sy.Name, this.Font, Brushes.Black, new Point(37, 575 + 5));
                    TimeSpan t = new TimeSpan();
                    TimeSpan t1 = new TimeSpan();
                    if (sy.Bz == 1)
                    {
                        t = sy.Kssj - otime;
                        if (csTime > Convert.ToDateTime("1990-01-01"))
                        {
                            t1 = csTime - otime;
                        }
                        else
                        {
                            t1 = DateTime.Now - otime;
                        }                     
                    }
                    else if (sy.Bz == 2)
                    {
                        t = sy.Kssj - otime;
                        t1 = sy.Jssj - otime;
                    }
                    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                    int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 20 / jcsjjg + 100);
                    int y1 = 420 + 10 + 20 * (dy3 + 0);
                
                     if (x1 > 100)
                     {
                         e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                         e.Graphics.DrawString(sy.Name + sy.Jl.ToString(), this.Font, Brushes.Blue, new Point(x1-5, y1 - 8));
                     }
                     if (x2 - x1 > 5 && sy.Cxyy == true)
                     {
                         if (sy.Bz == 1)
                         {
                             e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1));
                             e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1 + 10));
                         }
                         if (sy.Bz == 2)
                         {
                             e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 5, y1 + 8), new Point(x2 + 5, y1 + 8) });
                         }
                         string str = (x2 - x1 + y1).ToString();
                         str += (x2 - x1 + y1).ToString();
                     }
                     if (x2 - x1 > 5 && sy.Cxyy == true)
                     {
                         if (x1 > 100 && x2 > 100)
                         {
                             e.Graphics.DrawLine(pred2, new Point(x1, y1 + 5), new Point(x2, y1 + 5));
                         }
                         if (x1 < 100 && x2 > 100)
                         {
                             e.Graphics.DrawLine(pred2, new Point(100, y1 + 5), new Point(x2, y1 + 5));
                         }
                     }
                }
                dy3++;
                sssSY.Add(sy.Name);
              
            }

            #endregion

            #region  //画出血
            int cxCOUNT = 1;
            foreach (adims_MODEL.clcxqt cl in cxList)
            {
                TimeSpan t = new TimeSpan();
                t = cl.D - otime;
                int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                int y1 = 445+20*2;
                e.Graphics.FillRectangle(Brushes.Pink, x1, y1, 20, 12);
                e.Graphics.DrawString(cl.V.ToString(), this.Font, Brushes.Black, new Point(x1, y1));
                cxCOUNT++;
            }
            #endregion

            #region  //画输血
            int sxi = 0;
            ArrayList sssSX = new ArrayList();
            foreach (adims_MODEL.shuxue sx in sx1ist)//画输血
            {
                if (sxi>2)
                {
                    break;
                }
                if (sx.Bz > 0)
                {
                    if (sssSX.Contains(sx.Name))
                    {
                        sxi = sxi - 1;
                    }
                    //e.Graphics.DrawString(sx.Name, this.Font, Brushes.Black, new Point(37, 465 + 5));
                    TimeSpan t = new TimeSpan();
                    TimeSpan t1 = new TimeSpan();
                    if (sx.Bz == 1)
                    {

                        t = sx.Kssj - otime;
                        if (csTime > Convert.ToDateTime("1990-01-01"))
                        {
                            t1 = csTime - otime;
                        }
                        else
                        {
                            t1 = DateTime.Now - otime;
                        }           
                    }
                    else if (sx.Bz == 2)
                    {
                        t = sx.Kssj - otime;
                        t1 = sx.Jssj - otime;
                    }
                    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                    int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 20 / jcsjjg + 100);
                    int y1 = y1 = 505 + sxi*20;                   
                    //e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                    //e.Graphics.DrawString(sx.Name + sx.Jl.ToString(), this.Font, Brushes.Blue, new Point(x1 - 5, y1 - 8));
                    if (x1 > 100)
                    {
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 5, y1 + 8), new Point(x1 + 5, y1 + 8) });
                        e.Graphics.DrawString(sx.Name + sx.Jl.ToString(), this.Font, Brushes.Blue, new Point(x1-5, y1 - 8));
                    }
                    if (x2 - x1 > 5 && sx.Cxyy == true)
                    {
                        if (sx.Bz == 1)
                        {
                            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1));
                            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 5), new Point(x2 - 5, y1 + 10));
                        }
                        if (sx.Bz == 2)
                        {
                            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 5, y1 + 8), new Point(x2 + 5, y1 + 8) });
                        }
                        string str = (x2 - x1 + y1).ToString();
                        str += (x2 - x1 + y1).ToString();
                    }
                    if (x2 - x1 > 5 && sx.Cxyy == true)
                    {
                        if (x1 > 100 && x2 > 100)
                        {
                            e.Graphics.DrawLine(pred2, new Point(x1, y1 + 5), new Point(x2, y1 + 5));
                        }
                        if (x1 < 100 && x2 > 100)
                        {
                            e.Graphics.DrawLine(pred2, new Point(100, y1 + 5), new Point(x2, y1 + 5));
                        }
                    }

                }
                sxi++;
                sssSX.Add(sx.Name);
            }
            #endregion

            #region  //画出尿
            int clCOUNT = 1;
            foreach (adims_MODEL.clcxqt cl in cnList)
            {
                TimeSpan t = new TimeSpan();
                t = cl.D - otime;
                int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                int y1=525+20;
                e.Graphics.FillRectangle(Brushes.Pink, x1, y1, 20, 12);
                e.Graphics.DrawString(cl.V.ToString(), this.Font,
                    Brushes.Black, new Point(x1, y1));
                clCOUNT++;
            }
            #endregion
            #region  //画引流量
            foreach (adims_MODEL.clcxqt cl in yllList)
            {
                TimeSpan t = new TimeSpan();
                t = cl.D - otime;
                int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                int y1 = 525 + 40;
                e.Graphics.FillRectangle(Brushes.Pink, x1, y1, 20, 12);
                e.Graphics.DrawString(cl.V.ToString(), this.Font,
                    Brushes.Black, new Point(x1, y1));
               
            }
            #endregion

            

        }

        int xMax = 1060;
        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.X > 100 && e.X < xMax)
            {
                if (e.Y > 0 && e.Y < 30)
                {
                    PACUaddQt qt = new PACUaddQt(otime.AddMinutes((e.X - 100) / 20 * jcsjjg), mzjldID);
                    qt.ShowDialog();
                    BindQtList();
                }
                if (e.Y > 380 && e.Y < 420)
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
                                    UpdateJHXM formxgjhsj = new UpdateJHXM(mzjldID, jhxmz, 1);
                                    formxgjhsj.ShowDialog();
                                }
                            }
                        }
                        jhxmCount++;
                    }
                }
                if (e.Y > 420 && e.Y < 480)
                {
                    PACU_AddSY f2 = new PACU_AddSY(otime.AddMinutes((e.X - 100) / 20 * jcsjjg), mzjldID);
                    f2.ShowDialog();
                    BindShuyeList();
                }
                if (e.Y > 480 && e.Y < 500)
                {
                    //if (cxList.Count == 0)
                    //{
                        PACU_Addcl formaddcl = new PACU_Addcl(otime.AddMinutes((e.X - 100) / 20 * jcsjjg), 1, mzjldID);
                        formaddcl.ShowDialog();
                        BindCxList();
                    //}
                    //else
                    //{
                    //    foreach (adims_MODEL.clcxqt cn1 in cxList)
                    //    {
                    //        UpdateChuliang formxgjhsj = new UpdateChuliang(mzjldID, cn1, 1);
                    //        formxgjhsj.ShowDialog();
                    //    }
                    //}
                }
                if (e.Y > 500 && e.Y < 540)
                {
                    PACU_Add_SX f2 = new PACU_Add_SX( otime.AddMinutes((e.X - 100) / 20 * jcsjjg), mzjldID);
                    f2.ShowDialog();
                    BindShuxueList();
                }
               
                if (e.Y > 540 && e.Y < 560)
                {
                //    if (cnList.Count == 0)
                //    {
                        PACU_Addcl formaddcl = new PACU_Addcl(otime.AddMinutes((e.X - 100) / 20 * jcsjjg), 2, mzjldID);
                        formaddcl.ShowDialog();
                        BindClList();
                    //}
                    //else
                    //{
                    //    foreach (adims_MODEL.clcxqt cn1 in cnList)
                    //    {
                    //        UpdateChuliang formxgjhsj = new UpdateChuliang(mzjldID, cn1, 2);
                    //        formxgjhsj.ShowDialog();
                    //    }
                    //}
                }
                ///引流量
                if (e.Y > 560 && e.Y < 580)
                {
                    //if (yllList.Count == 0)
                    //{
                        PACU_Addcl formaddcl = new PACU_Addcl(otime.AddMinutes((e.X - 100) / 20 * jcsjjg), 3, mzjldID);
                        formaddcl.ShowDialog();
                        BindYllList();
                    //}
                    //else
                    //{
                    //    foreach (adims_MODEL.clcxqt cn1 in cnList)
                    //    {
                    //        UpdateChuliang formxgjhsj = new UpdateChuliang(mzjldID, cn1, 3);
                    //        formxgjhsj.ShowDialog();
                    //    }
                    //}
                }
            }
            pictureBox1.Refresh();
        }

        int lx, CLid, xgqvalue, xghvalue;//生理记录检测点类型，修改前值，修改后值
        DateTime xgdtime = new DateTime();//修改点时间
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int ii = 0;
            p3x = e.X; p3y = e.Y;
            foreach (adims_MODEL.point tssy in ssy)
            {
                TimeSpan t = new TimeSpan();
                t = tssy.D - otime;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                int y = 0;
                if (tssy.V > 220)
                    y = 30;
                else
                    y = (int)(360 - (int)(tssy.V * 1.5));
                if (p3x > x - 5 && p3x < x + 5 && p3y < y + 1 && p3y > y - 5)
                {
                    flagPoint = 1; p3t = tssy; lx = 1; xgqvalue = p3t.V;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.AutoSize = true;
                    pictureBox1.Controls.Add(lab1);
                    lab1.Location = new Point(x, y);
                    lab1.Text = p3t.V.ToString();
                }
            }
            foreach (adims_MODEL.point tssy in szy)
            {
                TimeSpan t = new TimeSpan();
                t = tssy.D - otime;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                int y = 0;
                if (tssy.V > 220)
                    y = 30;
                else
                    y = (int)(360 - (int)(tssy.V * 1.5));
                if (p3x > x - 5 && p3x < x + 5 && p3y < y + 5 && p3y > y - 1)
                {
                    flagPoint = 1; p3t = tssy; lx = 2; xgqvalue = p3t.V;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.AutoSize = true;
                    pictureBox1.Controls.Add(lab1);
                    lab1.Location = new Point(x, y);
                    lab1.Text = p3t.V.ToString();
                }
            }
            foreach (adims_MODEL.point tssy in mbo)
            {
                TimeSpan t = new TimeSpan();
                t = tssy.D - otime;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                int y = 0;
                if (tssy.V > 220)
                    y = 30;
                else
                    y = (int)(360 - (int)(tssy.V * 1.5));
                if (p3x > x - 5 && p3x < x + 5 && p3y < y + 3 && p3y > y - 3)
                {
                    flagPoint = 1; p3t = tssy; lx = 3; xgqvalue = p3t.V;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.AutoSize = true;
                    pictureBox1.Controls.Add(lab1);
                    lab1.Location = new Point(x, y);
                    lab1.Text = p3t.V.ToString();
                }
            }
            foreach (adims_MODEL.point tssy in hxl)
            {
                TimeSpan t = new TimeSpan();
                t = tssy.D - otime;
                int y;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                if (tssy.V > 220)
                    y = 30;
                else
                    y = (int)(360 - (int)(tssy.V * 1.5));
                if (p3x > x - 5 && p3x < x + 5 && p3y < y + 3 && p3y > y - 3)
                {
                    flagPoint = 1; p3t = tssy; lx = 4; xgqvalue = p3t.V;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.AutoSize = true;
                    pictureBox1.Controls.Add(lab1);
                    lab1.Location = new Point(x, y);
                    lab1.Text = p3t.V.ToString();
                }
            }
            foreach (adims_MODEL.tw_point tssy in tw)
            {
                TimeSpan t = new TimeSpan();
                t = tssy.D - otime;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                int y = 0;
                if (tssy.V > 41)
                    y = 30;
                else
                    y = (int)(360 - (tssy.V - 30) * 30);
                if (p3x > x - 5 && p3x < x + 5 && p3y < y + 5 && p3y > y - 5)
                {
                    flagPoint = 8; tw_p3t = tssy; lx = 5; xgqvalue =(int)tw_p3t.V;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.AutoSize = true;
                    pictureBox1.Controls.Add(lab1);
                    lab1.Location = new Point(x, y);
                    lab1.Text = tw_p3t.V.ToString();
                }
            }
            foreach (adims_MODEL.mzqt q in mzqtList)  // 是否选中气体开始标志
            {
                TimeSpan t = new TimeSpan();
                t = q.Sysj - otime;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                int y = 10;
                TimeSpan t1 = new TimeSpan();
                t1 = q.Jssj - otime;
                int x1 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 20 / jcsjjg + 100);
                if (e.X > x - 5 && e.X < x + 5 && e.Y > y - 1 && e.Y < y + 8)
                { flagP1 = 1; TypeP1 = 7; t_mzqt = q; }
                if (e.X > x1 - 5 && e.X < x1 + 5 && e.Y > y - 1 && e.Y < y + 8)
                { flagP1 = 1; TypeP1 = 77; t_mzqt = q; }

            }
            int dy3 = 0;
            ArrayList sssSY = new ArrayList();
            foreach (adims_MODEL.shuye yt in sy1ist)//是否选中输液
            {
                if (yt.Bz > 0)
                {
                    if (dy3 > 0 && sssSY.Contains(yt.Name))
                    {
                        dy3 = dy3 - 1;
                    }
                    TimeSpan t = new TimeSpan();
                    t = yt.Kssj - otime;
                    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                    int y = 420 + 10 + 20 * (dy3 + 0);
                    TimeSpan t2 = new TimeSpan();
                    t2 = yt.Jssj - otime;
                    int x2 = (int)((t2.Days * 24 * 60 + t2.Hours * 60 + t2.Minutes) * 20 / jcsjjg + 100);
                    int y2 = 420 + 10 + 20 * (dy3 + 0);
                    if (e.X > x - 5 && e.X < x + 5 && e.Y > y - 1 && e.Y < y + 8)
                    { flagP1 = 1; TypeP1 = 1; t_shuye = yt; }
                    if (e.X > x2 - 5 && e.X < x2 + 5 && e.Y > y2 - 1 && e.Y < y2 + 8)
                    { flagP1 = 1; TypeP1 = 11; t_shuye = yt; }
                    dy3++;
                    sssSY.Add(yt.Name);
                    //TimeSpan t = new TimeSpan();
                    //t = yt.Kssj - otime;
                    //int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                    //int y = 0;
                    //if (syi % 2 == 0)
                    //    y = 420 + 10;
                    //else
                    //    y = 420 + 10;
                    //if (e.X > x - 5 && e.X < x + 5 && e.Y > y - 1 && e.Y < y + 8)
                    //{ flagP1 = 1; TypeP1 = 1; t_shuye = yt; }
                }
                
            }
            foreach (adims_MODEL.clcxqt q in cxList)  // 是否选中失血
            {
                TimeSpan t = new TimeSpan();
                t = q.D - otime;
                CLid = q.Id;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                int y = 445 + 20 * 2;
                if (e.X > x - 5 && e.X < x + 12 && e.Y < y + 12 && e.Y > y)
                { flagP1 = 1; TypeP1 = 2; t_shixue = q; }

            }
            int dy4 = 0;
            ArrayList sssSX = new ArrayList();
            foreach (adims_MODEL.shuxue sx in sx1ist)// 是否选中输血
            {
                if (sx.Bz > 0)
                {
                    if (dy4 > 0 && sssSX.Contains(sx.Name))
                    {
                        dy4 = dy4 - 1;
                    }
                    TimeSpan t = new TimeSpan();
                    t = sx.Kssj - otime;
                    int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                    int y = 505 + dy4 * 20;
                    TimeSpan t2 = new TimeSpan();
                    t2 = sx.Jssj - otime;
                    int x2 = (int)((t2.Days * 24 * 60 + t2.Hours * 60 + t2.Minutes) * 20 / jcsjjg + 100);
                    int y2 = 505 + dy4 * 20;     
                    if (e.X > x - 5 && e.X < x + 5 && e.Y > y - 1 && e.Y < y + 8)
                    { flagP1 = 1; TypeP1 = 3; t_shuxue = sx; }
                    if (e.X > x2 - 5 && e.X < x2 + 5 && e.Y > y2 - 1 && e.Y < y2 + 8)
                    { flagP1 = 1; TypeP1 = 33; t_shuxue = sx; }
                    dy4++;
                    sssSX.Add(sx.Name);
                    //TimeSpan t = new TimeSpan();
                    //t = sx.Kssj - otime;
                    //int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                    //int y = 0;
                    //if (sxi % 2 == 0)
                    //    y = 460 + 10;
                    //else
                    //    y = 460 + 10;
                    //if (e.X > x - 5 && e.X < x + 5 && e.Y > y - 1 && e.Y < y + 8)
                    //{ flagP1 = 1; TypeP1 = 3; t_shuxue = sx; }
                }
             
            }
            foreach (adims_MODEL.clcxqt q in cnList)  // 是否选中尿量图像
            {
                TimeSpan t = new TimeSpan();
                t = q.D - otime;
                CLid = q.Id;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                int y = 525 + 20;
                if (e.X > x - 5 && e.X < x + 12 && e.Y < y + 12 && e.Y > y)
                { flagP1 = 1; TypeP1 = 4; t_niaoliang = q; }

            }
            foreach (adims_MODEL.clcxqt q in yllList)  // 是否选中引流量图像
            {
                TimeSpan t = new TimeSpan();
                t = q.D - otime;
                CLid = q.Id;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                int y = 525 + 40;
                if (e.X > x - 5 && e.X < x + 12 && e.Y < y + 12 && e.Y > y)
                { flagP1 = 1; TypeP1 = 44; t_yll = q; }

            }
            foreach (adims_MODEL.szsj sj in sjList)//是否选中事件
            {
                TimeSpan t = new TimeSpan();
                t = sj.D - otime;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                int y = 360 + 5;
                if (e.X > x - 8 && e.X < x + 8 && e.Y < y + 12 && e.Y > y)
                { flagP1 = 1; TypeP1 = 5; t_szsj = sj; ii = 1; }
            }
            foreach (adims_MODEL.tsyy ts in yyList)//是否选中用药
            {
                TimeSpan t = new TimeSpan();
                t = ts.D - otime;
                int x = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 20 / jcsjjg + 100);
                int y = 360 + 5;
                if (e.X > x - 8 && e.X < x + 8 && e.Y < y + 12 && e.Y > y)
                { flagP1 = 1; TypeP1 = 6; t_tsyy = ts; ii = 1; }
            }

        }
        DateTime dtjieshu = new DateTime();
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            PointF pttag = new PointF(MousePosition.X, MousePosition.Y);
            Graphics g = this.CreateGraphics();
            g.Clear(this.BackColor);
            //if (MousePosition.Y > e.Y)
            //{
            //}
            //if (MousePosition.X > e.X)
            //{
            //    g.Clear(this.BackColor);
            //}
            p3x = e.X; p3y = e.Y;
            if (flagPoint == 1)//移动监护点
            {
                p3t.V = (int)((360 - p3y) / 1.5);
                lab1.Visible = true;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Red;
                lab1.AutoSize = true;
                pictureBox1.Controls.Add(lab1);
                lab1.Location = new Point(e.X, e.Y);
                lab1.Text = p3t.V.ToString();
                lx = p3t.Lx;
                pictureBox1.Refresh();
                xghvalue = p3t.V; //得到修改后的值
                xgdtime = Convert.ToDateTime(p3t.D);
            }
            if (flagPoint == 8)//移动体温
            {
                string value = ((360 - p3y) / 30 + 30).ToString();
                if (value.Contains("."))
                {
                    int index = value.IndexOf(".");//查找最后一个.所在的位置
                    string ss = value.Substring(0, index + 2);   // 取子字符串。
                    tw_p3t.V = Convert.ToSingle(ss);
                }
                else
                {
                    tw_p3t.V = (float)((360 - p3y) / 30 + 30);
                }
                lab1.Visible = true;
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Red;
                lab1.AutoSize = true;
                pictureBox1.Controls.Add(lab1);
                lab1.Location = new Point(e.X, e.Y);
                lab1.Text = tw_p3t.V.ToString();
                lx = tw_p3t.Lx;
                pictureBox1.Refresh();
                xghvalue = (int)tw_p3t.V; //得到修改后的值
                xgdtime = Convert.ToDateTime(tw_p3t.D);
            }
            if (flagP1 == 1)
            {
                 if (TypeP1 == 7)
                {
                    t_mzqt.Sysj = otime.AddMinutes((p3x - 100) / 20 * jcsjjg);
                    lab1.Visible = true;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Blue;
                    lab1.AutoSize = true;
                    pictureBox1.Controls.Add(lab1);
                    lab1.Text = t_mzqt.Sysj.ToString("HH:mm");
                    lab1.Location = new Point(e.X, e.Y);
                    lab1.BringToFront();
                }
                else if (TypeP1 == 77)
                {
                    t_mzqt.Jssj = otime.AddMinutes((p3x - 100) / 20 * jcsjjg);
                    lab1.Visible = true;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Blue;
                    lab1.AutoSize = true;
                    pictureBox1.Controls.Add(lab1);
                    lab1.Text = t_mzqt.Jssj.ToString("HH:mm");
                    lab1.Location = new Point(e.X, e.Y);
                    lab1.BringToFront();
                }
                 else if (TypeP1 == 1)//移动输液
                 {
                     t_shuye.Kssj = otime.AddMinutes((p3x - 100) / 20 * jcsjjg);
                     lab1.Visible = true;
                     lab1.BackColor = Color.Transparent;
                     lab1.ForeColor = Color.Red;
                     lab1.AutoSize = true;
                     pictureBox1.Controls.Add(lab1);
                     lab1.Text = t_shuye.Kssj.ToString("HH:mm");
                     lab1.Location = new Point(e.X, e.Y);
                     lab1.BringToFront();
                 }
                 else if (TypeP1 == 11)//移动输液
                 {
                     t_shuye.Jssj = otime.AddMinutes((p3x - 100) / 20 * jcsjjg);
                     lab1.Visible = true;
                     lab1.BackColor = Color.Transparent;
                     lab1.ForeColor = Color.Red;
                     lab1.AutoSize = true;
                     pictureBox1.Controls.Add(lab1);
                     lab1.Text = t_shuye.Jssj.ToString("HH:mm");
                     lab1.Location = new Point(e.X, e.Y);
                     lab1.BringToFront();
                 }
                 else if (TypeP1 == 2)//移动失血
                 {
                     t_shixue.D = otime.AddMinutes((p3x - 100) / 20 * jcsjjg);
                     lab1.Visible = true;
                     lab1.BackColor = Color.Transparent;
                     lab1.ForeColor = Color.Red;
                     lab1.AutoSize = true;
                     pictureBox1.Controls.Add(lab1);
                     lab1.Text = t_shixue.D.ToString("HH:mm");
                     lab1.Location = new Point(e.X, e.Y);
                     lab1.BringToFront();
                 }
                 else if (TypeP1 == 3)//移动输血
                {
                    t_shuxue.Kssj = otime.AddMinutes((p3x - 100) / 20 * jcsjjg);
                    lab1.Visible = true;
                    lab1.BackColor = Color.Transparent;
                    lab1.ForeColor = Color.Red;
                    lab1.AutoSize = true;
                    pictureBox1.Controls.Add(lab1);
                    lab1.Text = t_shuxue.Kssj.ToString("HH:mm");
                    lab1.Location = new Point(e.X, e.Y);
                    lab1.BringToFront();
                }
                 else if (TypeP1 == 33)//移动输血
                 {
                     t_shuxue.Jssj = otime.AddMinutes((p3x - 100) / 20 * jcsjjg);
                     lab1.Visible = true;
                     lab1.BackColor = Color.Transparent;
                     lab1.ForeColor = Color.Red;
                     lab1.AutoSize = true;
                     pictureBox1.Controls.Add(lab1);
                     lab1.Text = t_shuxue.Jssj.ToString("HH:mm");
                     lab1.Location = new Point(e.X, e.Y);
                     lab1.BringToFront();
                 }
                 else if (TypeP1 == 4)//移动出尿
                 {
                     t_niaoliang.D = otime.AddMinutes((p3x - 100) / 20 * jcsjjg);
                     lab1.Visible = true;
                     lab1.BackColor = Color.Transparent;
                     lab1.ForeColor = Color.Red;
                     lab1.AutoSize = true;
                     pictureBox1.Controls.Add(lab1);
                     lab1.Text = t_niaoliang.D.ToString("HH:mm");
                     lab1.Location = new Point(e.X, e.Y);
                     lab1.BringToFront();
                 }
                 else if (TypeP1 == 44)//移动引流量
                 {
                     t_yll.D = otime.AddMinutes((p3x - 100) / 20 * jcsjjg);
                     lab1.Visible = true;
                     lab1.BackColor = Color.Transparent;
                     lab1.ForeColor = Color.Red;
                     lab1.AutoSize = true;
                     pictureBox1.Controls.Add(lab1);
                     lab1.Text = t_yll.D.ToString("HH:mm");
                     lab1.Location = new Point(e.X, e.Y);
                     lab1.BringToFront();
                 }
                 else if (TypeP1 == 5)//术中事件移动
                 {
                     int X = e.X;
                     dtjieshu = otime.AddMinutes((p3x - 100) / 20 * jcsjjg);
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
                    t_tsyy.D = otime.AddMinutes((p3x - 100) / 20 * jcsjjg);
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
            if (flagPoint == 1)
            {
                p3t.V = xghvalue;
                p3t.D = xgdtime;
                p3t.Lx = lx;
                bll.xgpointPACU(mzjldID, p3t);
                flagPoint = 0;
            }
            if (flagPoint == 8)
            {
                //tw_p3t.V = xghvalue;
                //tw_p3t.D = xgdtime;
                //tw_p3t.Lx = lx;
                bll.xgpointPACU_TW(mzjldID, tw_p3t);
                flagPoint = 0;
            }
            if (flagP1 == 1)
            {
                if (TypeP1 == 7)
                {
                    bll.xgqtPACU(mzjldID, t_mzqt);
                }
                else if (TypeP1 == 77)
                {
                    bll.xgqtPACU1(mzjldID, t_mzqt);
                }
                else if (TypeP1 == 1)
                {
                    bll.xg_PACU_sy(mzjldID, t_shuye);
                }
                else if (TypeP1 == 11)
                {
                    bll.xg_PACU_sys(mzjldID, t_shuye);
                }
                else if (TypeP1 == 2)
                {
                    bll.xgclsjPACU(mzjldID, t_shixue);
                }
                else if (TypeP1 == 3)
                {
                    bll.xg_PACU_sx(mzjldID, t_shuxue);
                }
                else if (TypeP1 == 33)
                {
                    bll.xg_PACU_sxs(mzjldID, t_shuxue);
                }
                else if (TypeP1 == 4)
                {
                    bll.xgclsjPACU(mzjldID, t_niaoliang);
                }
                else if (TypeP1 == 44)
                {
                    bll.xgclsjPACU(mzjldID, t_yll);
                }
                else if (TypeP1 == 5)
                {
                    bll.xgszsjTimePacu(mzjldID, t_szsj, dtjieshu);
                    BindShijian();
                }
                else if (TypeP1 == 6)
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
                //txtMzsj.Text = Convert.ToString(DateTime.Now);
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                rushiFlag.Visible = true;
                rushiFlag.Text = "＞";
                rushiFlag.AutoSize = true;
                rushiFlag.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                rushiFlag.ForeColor = Color.Purple;
                rushiFlag.BackColor = Color.Transparent;
                rushiFlag.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes)
                                   * 20 / jcsjjg + 90), 365);
                //rushiFlag.Location = new Point(186, 650);
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


        int xStart, xEnd;//移动麻醉，手术，插管开始结束点
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
            DateTime DTIME = otime.AddMinutes((xStart - 90) / 20 * jcsjjg);
            lab1.Text = DTIME.ToString("HH:mm");
        }

        private void rushiFlag_MouseUp(object sender, EventArgs e)
        {
            p3lf1 = 0;
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
                string zdming = "rssj";
                dal.Update_single_PACU_SZ(zdming, otime.AddMinutes((xEnd - 90) / 20 * jcsjjg), mzjldID);
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Red;
                lab1.AutoSize = true;
                lab1.Visible = true;
                pictureBox1.Controls.Add(lab1);
                lab1.Location = new Point(rushiFlag.Location.X, rushiFlag.Location.Y - 15);
                DateTime DTIME = otime.AddMinutes((xEnd - 90) / 20 * jcsjjg);
                lab1.Text = DTIME.ToString("HH:mm");
                //int ssshijian = Convert.ToInt32((chushiFlag.Location.X - rushiFlag.Location.X) / 4);//计算麻醉总时间
                //txtMZJS.Text = DTIME.ToString("HH:mm");

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
                chushiFlag.BackColor = Color.Transparent;
                chushiFlag.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                chushiFlag.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60
                    + t.Minutes) * 20 / jcsjjg + 90), 365);
                //chushiFlag.Location = new Point(186, 650);
                this.pictureBox1.Controls.Add(chushiFlag);
                chushi = true;
                string zdming = "cssj";
                dal.Update_single_PACU_SZ(zdming, DateTime.Now, mzjldID);
                //txtMZJS.Text = DateTime.Now.ToString("HH:mm");
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
            DateTime DTIME = otime.AddMinutes((xStart - 90) / 20 * jcsjjg);
            lab1.Text = DTIME.ToString("HH:mm");
        }

        private void chushiFlag_MouseUp(object sender, EventArgs e)
        {
            p3lf2 = 0;
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
                string zdming = "cssj";
                dal.Update_single_PACU_SZ(zdming, otime.AddMinutes((xEnd - 90) / 20 * jcsjjg), mzjldID);
                csTime = otime.AddMinutes((xEnd - 90) / 20 * jcsjjg);
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Red;
                lab1.AutoSize = true;
                lab1.Visible = true;
                pictureBox1.Controls.Add(lab1);
                lab1.Location = new Point(chushiFlag.Location.X, chushiFlag.Location.Y - 15);
                DateTime DTIME = otime.AddMinutes((xEnd - 90) / 20 * jcsjjg);
                lab1.Text = DTIME.ToString("HH:mm");
                //int ssshijian = Convert.ToInt32((chushiFlag.Location.X - rushiFlag.Location.X) / 4);//计算麻醉总时间
                //txtMZJS.Text = DTIME.ToString("HH:mm");
                pictureBox1.Refresh();
            }
        }

        #endregion

        #region <<插管>>
        private void lbCg_DoubleClick(object sender, EventArgs e)
        {
            if (!CGUAN)
            {
                //txtMzsj.Text = Convert.ToString(DateTime.Now);
                TimeSpan t = new TimeSpan();
                t = DateTime.Now - otime;
                lb_cguan.Text = "Θ";
                lb_cguan.AutoSize = true;
                lb_cguan.BackColor = Color.Transparent;
                lb_cguan.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                lb_cguan.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60
                    + t.Minutes) * 20 / jcsjjg + 90), 365);
                this.pictureBox1.Controls.Add(lb_cguan);
                CGUAN = true;
                BGUAN = false;
                string zdming = "cgsj";
                dal.Update_single_PACU_SZ(zdming, DateTime.Now, mzjldID);
                //txtCGKS.Text = DateTime.Now.ToString("HH:mm");
                lb_cguan.MouseDown += new MouseEventHandler(lb_cguan_MouseDown);
                lb_cguan.MouseMove += new MouseEventHandler(lb_cguan_MouseMove);
                lb_cguan.MouseUp += new MouseEventHandler(lb_cguan_MouseUp);
                lb_cguan.MouseLeave += new EventHandler(lb_cguan_MouseLeave);
            }
            else
                MessageBox.Show("已经插管！");
        }
        private void lb_cguan_MouseDown(object sender, EventArgs e)
        {
            p3lf1 = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.LightYellow;
            lab1.AutoSize = true;
            lab1.Visible = true;
            pictureBox1.Controls.Add(lab1);
            lab1.Location = new Point(lb_cguan.Location.X, lb_cguan.Location.Y - 15);
            xStart = lb_cguan.Location.X;
            DateTime DTIME = otime.AddMinutes((xStart - 90) / 20 * jcsjjg);
            lab1.Text = DTIME.ToString("HH:mm");
        }

        private void lb_cguan_MouseUp(object sender, EventArgs e)
        {
            p3lf1 = 0;
            timer2.Enabled = true;
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
                string zdming = "cgsj";
                dal.Update_single_PACU_SZ(zdming, otime.AddMinutes((xEnd - 90) / 20 * jcsjjg), mzjldID);
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Red;
                lab1.AutoSize = true;
                lab1.Visible = true;
                pictureBox1.Controls.Add(lab1);
                lab1.Location = new Point(lb_cguan.Location.X, lb_cguan.Location.Y - 15);
                DateTime DTIME = otime.AddMinutes((xEnd - 90) / 20 * jcsjjg);
                lab1.Text = DTIME.ToString("HH:mm");
                //txtCGKS.Text = DTIME.ToString("HH:mm");


            }
        }
        #endregion

        #region <<拔管>>

        private void lbBg_DoubleClick(object sender, EventArgs e)
        {
            //if (CGUAN && !BGUAN)
            //{
            TimeSpan t = new TimeSpan();
            t = DateTime.Now - otime;            
            lb_bguan.Text = "Φ";
            lb_bguan.AutoSize = true;
            lb_bguan.BackColor = Color.Transparent;
            lb_bguan.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lb_bguan.Location = new Point((int)((t.Days * 24 * 60 + t.Hours * 60
                + t.Minutes) * 20 / jcsjjg + 90), 365);
            this.pictureBox1.Controls.Add(lb_bguan);
            BGUAN = true;
            string zdming = "bgsj";
            dal.Update_single_PACU_SZ(zdming, DateTime.Now, mzjldID);
            //txtCGJS.Text = DateTime.Now.ToString("HH:mm");
            lb_bguan.MouseDown += new MouseEventHandler(lb_bguan_MouseDown);
            lb_bguan.MouseMove += new MouseEventHandler(lb_bguan_MouseMove);
            lb_bguan.MouseUp += new MouseEventHandler(lb_bguan_MouseUp);
            lb_bguan.MouseLeave += new EventHandler(lb_bguan_MouseLeave);
            //}
            //else
            //    MessageBox.Show("还未插管或已经拔管！");
        }


        private void lb_bguan_MouseDown(object sender, EventArgs e)
        {
            p3lf2 = 1;
            lab1.BackColor = Color.Transparent;
            lab1.ForeColor = Color.LightYellow;
            lab1.AutoSize = true;
            lab1.Visible = true;
            pictureBox1.Controls.Add(lab1);
            lab1.Location = new Point(lb_bguan.Location.X, lb_bguan.Location.Y - 15);
            xStart = lb_bguan.Location.X;
            DateTime DTIME = otime.AddMinutes((xStart - 90) / 20 * jcsjjg);
            lab1.Text = DTIME.ToString("HH:mm");
        }

        private void lb_bguan_MouseUp(object sender, EventArgs e)
        {
            p3lf2 = 0;
            timer2.Enabled = true;
        }

        private void lb_bguan_MouseLeave(object sender, EventArgs e)
        {
            p3lf2 = 0;
        }

        private void lb_bguan_MouseMove(object sender, MouseEventArgs e)
        {
            if (p3lf2 == 1)
            {

                lb_bguan.Location = new Point(lb_bguan.Location.X + e.X / 2 - 2, lb_bguan.Location.Y);
                xEnd = lb_bguan.Location.X;
                string zdming = "bgsj";
                dal.Update_single_PACU_SZ(zdming, otime.AddMinutes((xEnd - 90) / 20 * jcsjjg), mzjldID);
                lab1.BackColor = Color.Transparent;
                lab1.ForeColor = Color.Red;
                lab1.AutoSize = true;
                lab1.Visible = true;
                pictureBox1.Controls.Add(lab1);
                lab1.Location = new Point(lb_bguan.Location.X, lb_bguan.Location.Y - 15);
                DateTime DTIME = otime.AddMinutes((xEnd - 90) / 20 * jcsjjg);
                lab1.Text = DTIME.ToString("HH:mm");
                //int ssshijian = Convert.ToInt32((lb_bguan.Location.X - lb_bguan.Location.X) / 3.3);//计算麻醉总时间
                //txtCGJS.Text = DTIME.ToString("HH:mm");

            }
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
            int JsFlag = 0;
            string YwName = string.Empty;
            foreach (adims_MODEL.mzqt qt in mzqtList)
            {
                //if (qt.Bz == 1)
                //{
                //    YwName = YwName + qt.Qtname + "\n";
                //    JsFlag++;
                //}
            }
            if (JsFlag > 0)
            {
                MessageBox.Show(YwName + "没有标记结束。");
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
                       new System.Drawing.Printing.PaperSize("K16", 780, 1020);
            //printDocument1.DefaultPageSettings.PaperSize =
            //      new System.Drawing.Printing.PaperSize("A4", 820, 1160);
        }
        int iYema = 1;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font textfront = new Font(new FontFamily("宋体"), 8);
            Font ptzt8 = new Font(new FontFamily("宋体"), 8);
            Font ptzt7 = new Font(new FontFamily("宋体"), 7);
            Font ptzt6 = new Font(new FontFamily("宋体"), 6);
            Font ptzt5 = new Font(new FontFamily("宋体"), 5);
            Font flagF = new Font(new FontFamily("宋体"), 12);
            Font txText = new Font(new FontFamily("宋体"), 9);
            Font tagfont = new Font(new FontFamily("宋体"), 13);
            Pen pb2 = new Pen(Brushes.Black);
            pb2.Width = 2;
            Pen pb1 = new Pen(Brushes.Black);
            pb1.Width = 1;
            Pen pxuxian = new Pen(Brushes.Black);
            pxuxian.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Pen pred2 = new Pen(Brushes.Red);
            pred2.Width = 2;

            int dyd = 0, dyd1 = 0, dyd2 = 0, dyd3 = 0, dyd4 = 0;//标志是否是第一个点
            int dy = 0;//   气体数量初始值
            Font fsmall = new Font("宋体", 7);
            Font fsmall8 = new Font("宋体", 8);
            int x = 80, y = 0, yUnder = y + 18;
            string title1 = "天津市红桥医院麻醉后恢复室记录单";
            //string title2 = "麻醉后恢复室记录单";

            y = y + 40; yUnder = y + 18;
            e.Graphics.DrawString(title1, tagfont, Brushes.Black, x + 170, y);
            //y = y + 30; yUnder = y + 18;
            //e.Graphics.DrawString(title2, tagfont, Brushes.Black, x + 450, y);

            y = y + 30; yUnder = y + 15;
            //e.Graphics.DrawLine(pb2, x, y, x, y + 800);
            //e.Graphics.DrawLine(pb2, x, y, x + 700, y);
            //e.Graphics.DrawLine(pb2, x, y + 800, x + 700, y + 800);
            //e.Graphics.DrawLine(pb2, x + 700, y, x + 700, y + 800);

            #region //打印基本信息
            y = y + 5; yUnder = y + 15;
            e.Graphics.DrawString("姓名 " + tbPatname.Text, textfront, Brushes.Black, x + 5, y);
            e.Graphics.DrawLine(Pens.Black, x + 35, yUnder, x + 80, yUnder);
            e.Graphics.DrawString("性别 " + tbSex.Text, textfront, Brushes.Black, x + 90, y);
            e.Graphics.DrawLine(Pens.Black, x + 110, yUnder, x + 150, yUnder);
            e.Graphics.DrawString("年龄 " + tbAge.Text + "岁", textfront, Brushes.Black, x + 160, y);
            e.Graphics.DrawLine(Pens.Black, x + 180, yUnder, x + 230, yUnder);
            e.Graphics.DrawString("病区 " + tbBingqu.Text, textfront, Brushes.Black, x + 240, y);
            e.Graphics.DrawLine(Pens.Black, x + 260, yUnder, x + 350, yUnder);
            e.Graphics.DrawString("床号 " + tbBedNO.Text, textfront, Brushes.Black, x + 360, y);
            e.Graphics.DrawLine(Pens.Black, x + 380, yUnder, x + 440, yUnder);
            e.Graphics.DrawString("住院号 " + tbZhuyuanID.Text, textfront, Brushes.Black, x + 450, y);
            e.Graphics.DrawLine(Pens.Black, x + 490, yUnder, x + 600, yUnder);
            y = y + 20; yUnder = y + 13;
            //e.Graphics.DrawString("体重 " + tbWeight.Text, textfront, Brushes.Black, x + 5, y);
            //e.Graphics.DrawLine(Pens.Black, x + 30, yUnder, x + 100, yUnder);
            e.Graphics.DrawString("手术名称 " + tbShoushuName.Text, textfront, Brushes.Black, x + 5, y);
            e.Graphics.DrawLine(Pens.Black, x + 60, yUnder, x + 400, yUnder);
            e.Graphics.DrawString("入室日期 " + dtInRoomTime.Text, textfront, Brushes.Black, x + 410, y);
            e.Graphics.DrawLine(Pens.Black, x + 460, yUnder, x + 600, yUnder);
            y = y + 20; yUnder = y + 13;
            e.Graphics.DrawString("麻醉种类：", textfront, Brushes.Black, x + 5, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 70, y, 10, 10);
            e.Graphics.DrawString("全静脉", textfront, Brushes.Black, x + 85, y);
            if (cbMZFF1.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 70, y, x + 75, y + 10);
                e.Graphics.DrawLine(pb2, x + 75, y + 10, x + 80, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 125, y, 10, 10);
            e.Graphics.DrawString("靶控", textfront, Brushes.Black, x + 140, y);
            if (cbMZFF2.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 125, y, x + 130, y + 10);
                e.Graphics.DrawLine(pb2, x + 130, y + 10, x + 135, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 170, y, 10, 10);
            e.Graphics.DrawString("静吸复合", textfront, Brushes.Black, x + 185, y);
            if (cbMZFF3.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 170, y, x + 175, y + 10);
                e.Graphics.DrawLine(pb2, x + 175, y + 10, x + 180, y );
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 235, y, 10, 10);
            e.Graphics.DrawString("吸入", textfront, Brushes.Black, x + 250, y);
            if (cbMZFF4.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 235, y, x + 240, y + 10);
                e.Graphics.DrawLine(pb2, x + 240, y + 10, x + 245, y );
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 280, y, 10, 10);
            e.Graphics.DrawString("硬外", textfront, Brushes.Black, x + 295, y);
            if (cbMZFF5.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 280, y, x + 285, y + 10);
                e.Graphics.DrawLine(pb2, x + 285, y + 10, x + 290, y );
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 325, y, 10, 10);
            e.Graphics.DrawString("腰麻", textfront, Brushes.Black, x + 340, y);
            if (cbMZFF6.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 325, y, x + 330, y + 10);
                e.Graphics.DrawLine(pb2, x + 330, y + 10, x + 335, y );
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 370, y, 10, 10);
            e.Graphics.DrawString("腰硬联合", textfront, Brushes.Black, x + 385, y);
            if (cbMZFF7.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 370, y, x + 375, y + 10);
                e.Graphics.DrawLine(pb2, x + 375, y + 10, x + 380, y );
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 435, y, 10, 10);
            e.Graphics.DrawString("骶麻", textfront, Brushes.Black, x + 450, y);
            if (cbMZFF8.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 435, y, x + 440, y + 10);
                e.Graphics.DrawLine(pb2, x + 440, y + 10, x + 445, y );
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 480, y, 10, 10);
            e.Graphics.DrawString("颈丛", textfront, Brushes.Black, x + 495, y);
            if (cbMZFF9.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 480, y, x + 485, y + 10);
                e.Graphics.DrawLine(pb2, x + 485, y + 10, x + 490, y );
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 525, y, 10, 10);
            e.Graphics.DrawString("臂丛", textfront, Brushes.Black, x + 540, y);
            if (cbMZFFa.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 525, y, x + 530, y + 10);
                e.Graphics.DrawLine(pb2, x + 530, y + 10, x + 535, y );
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 570, y, 10, 10);
            e.Graphics.DrawString("神经阻滞", textfront, Brushes.Black, x + 585, y);
            if (cbMZFFb.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 570, y, x + 575, y + 10);
                e.Graphics.DrawLine(pb2, x + 575, y + 10, x + 580, y );
            }
            //e.Graphics.DrawRectangle(Pens.Black, x + 635, y, 10, 10);
            //e.Graphics.DrawString("局麻", textfront, Brushes.Black, x + 650, y);
            //if (cbMZFFc.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 635, y, x + 640, y + 10);
            //    e.Graphics.DrawLine(pb2, x + 640, y + 10, x + 645, y );
            //}
            y = y + 20; yUnder = y + 13;
            e.Graphics.DrawRectangle(Pens.Black, x + 10, y, 10, 10);
            e.Graphics.DrawString("局麻", textfront, Brushes.Black, x + 25, y);
            if (cbMZFFc.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 10, y, x + 15, y + 10);
                e.Graphics.DrawLine(pb2, x + 15, y + 10, x + 25, y);
            }
            e.Graphics.DrawString("术中特殊情况：", textfront, Brushes.Black, x + 55, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 140, y, 10, 10);
            e.Graphics.DrawString("顺利", textfront, Brushes.Black, x + 155, y);
            if (cbSQTSQK1.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 140, y, x + 145, y + 10);
                e.Graphics.DrawLine(pb2, x + 145, y + 10, x + 150, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 185, y, 10, 10);
            e.Graphics.DrawString("低血压", textfront, Brushes.Black, x + 200, y);
            if (cbSQTSQK2.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 185, y, x + 190, y + 10);
                e.Graphics.DrawLine(pb2, x + 190, y + 10, x + 195, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 10, 10);
            e.Graphics.DrawString("高血压", textfront, Brushes.Black, x + 255, y);
            if (cbSQTSQK3.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 240, y, x + 245, y + 10);
                e.Graphics.DrawLine(pb2, x + 245, y + 10, x + 250, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 295, y, 10, 10);
            e.Graphics.DrawString("心律失常", textfront, Brushes.Black, x + 310, y);
            if (cbSQTSQK4.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 295, y, x + 300, y + 10);
                e.Graphics.DrawLine(pb2, x + 300, y + 10, x + 305, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 10, 10);
            e.Graphics.DrawString("休克", textfront, Brushes.Black, x + 375, y);
            if (cbSQTSQK5.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 360, y, x + 365, y + 10);
                e.Graphics.DrawLine(pb2, x + 365, y + 10, x + 370, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 405, y, 10, 10);
            e.Graphics.DrawString("哮喘", textfront, Brushes.Black, x + 420, y);
            if (cbSQTSQK6.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 405, y, x + 410, y + 10);
                e.Graphics.DrawLine(pb2, x + 410, y + 10, x + 415, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 450, y, 10, 10);
            e.Graphics.DrawString("过敏", textfront, Brushes.Black, x + 465, y);
            if (cbSQTSQK7.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 450, y, x + 455, y + 10);
                e.Graphics.DrawLine(pb2, x + 455, y + 10, x + 460, y - 3);
            }
            y = y + 20; yUnder = y + 13;
            e.Graphics.DrawLine(pb1, x, y, x + 640, y);//画线
            e.Graphics.DrawLine(pb1, x, y, x, y + 170);
            e.Graphics.DrawLine(pb1, x + 640, y, x + 640, y + 170);
            y = y + 5; yUnder = y + 15;
            e.Graphics.DrawString("麻醉后医嘱：", textfront, Brushes.Black, x + 5, y);
            y = y + 20; yUnder = y + 13;
            e.Graphics.DrawString("体位：", textfront, Brushes.Black, x + 20, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 70, y, 10, 10);
            e.Graphics.DrawString("仰卧", textfront, Brushes.Black, x + 85, y);
            if (cbTiwei1.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 70, y, x + 75, y + 10);
                e.Graphics.DrawLine(pb2, x + 75, y + 10, x + 80, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 115, y, 10, 10);
            e.Graphics.DrawString("去枕", textfront, Brushes.Black, x + 130, y);
            if (cbTiwei2.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 115, y, x + 120, y + 10);
                e.Graphics.DrawLine(pb2, x + 120, y + 10, x + 125, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 170, y, 10, 10);
            e.Graphics.DrawString("左侧卧", textfront, Brushes.Black, x + 185, y);
            if (cbTiwei3.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 170, y, x + 175, y + 10);
                e.Graphics.DrawLine(pb2, x + 175, y + 10, x + 180, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 225, y, 10, 10);
            e.Graphics.DrawString("右侧卧", textfront, Brushes.Black, x + 240, y);
            if (cbTiwei4.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 225, y, x + 230, y + 10);
                e.Graphics.DrawLine(pb2, x + 230, y + 10, x + 235, y );
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 280, y, 10, 10);
            e.Graphics.DrawString("俯卧", textfront, Brushes.Black, x + 295, y);
            if (cbTiwei5.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 280, y, x + 285, y + 10);
                e.Graphics.DrawLine(pb2, x + 285, y + 10, x + 290, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 325, y, 10, 10);
            e.Graphics.DrawString("头低位", textfront, Brushes.Black, x + 340, y);
            if (cbTiwei6.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 325, y, x + 330, y + 10);
                e.Graphics.DrawLine(pb2, x + 330, y + 10, x + 335, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 380, y, 10, 10);
            e.Graphics.DrawString("头高位", textfront, Brushes.Black, x + 395, y);
            if (cbTiwei7.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 380, y, x + 385, y + 10);
                e.Graphics.DrawLine(pb2, x + 385, y + 10, x + 390, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 435, y, 10, 10);
            e.Graphics.DrawString("半坐位", textfront, Brushes.Black, x + 450, y);
            if (cbTiwei8.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 435, y, x + 440, y + 10);
                e.Graphics.DrawLine(pb2, x + 440, y + 10, x + 445, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 490, y, 10, 10);
            e.Graphics.DrawString("其他", textfront, Brushes.Black, x + 505, y);
            if (cbTiwei9.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 490, y, x + 495, y + 10);
                e.Graphics.DrawLine(pb2, x + 495, y + 10, x + 500, y);
            }

            y = y + 20; yUnder = y + 13;
            e.Graphics.DrawString("气道与通气：", textfront, Brushes.Black, x + 20, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 90, y, 10, 10);
            e.Graphics.DrawString("简易面罩", textfront, Brushes.Black, x + 105, y);
            if (cbQDTQ1.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 90, y, x + 95, y + 10);
                e.Graphics.DrawLine(pb2, x + 95, y + 10, x + 100, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 155, y, 10, 10);
            e.Graphics.DrawString("鼻塞管", textfront, Brushes.Black, x + 170, y);
            if (cbQDTQ2.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 155, y, x + 160, y + 10);
                e.Graphics.DrawLine(pb2, x + 160, y + 10, x + 165, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 210, y, 10, 10);
            e.Graphics.DrawString("咽通气道", textfront, Brushes.Black, x + 225, y);
            if (cbQDTQ3.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 210, y, x + 215, y + 10);
                e.Graphics.DrawLine(pb2, x + 215, y + 10, x + 220, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 275, y, 10, 10);
            e.Graphics.DrawString("鼻咽通气道", textfront, Brushes.Black, x + 290, y);
            if (cbQDTQ4.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 275, y, x + 280, y + 10);
                e.Graphics.DrawLine(pb2, x + 280, y + 10, x + 285, y );
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 350, y, 10, 10);
            e.Graphics.DrawString("鼻导管", textfront, Brushes.Black, x + 365, y);
            if (cbQDTQ5.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 350, y, x + 355, y + 10);
                e.Graphics.DrawLine(pb2, x + 355, y + 10, x + 360, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 405, y, 10, 10);
            e.Graphics.DrawString("T型管", textfront, Brushes.Black, x + 420, y);
            if (cbQDTQ6.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 405, y, x + 410, y + 10);
                e.Graphics.DrawLine(pb2, x + 410, y + 10, x + 415, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 460, y, 10, 10);
            e.Graphics.DrawString("背因回路", textfront, Brushes.Black, x + 475, y);
            if (cbQDTQ7.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 460, y, x + 465, y + 10);
                e.Graphics.DrawLine(pb2, x + 465, y + 10, x + 470, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 525, y, 10, 10);
            e.Graphics.DrawString("气管切开", textfront, Brushes.Black, x + 540, y);
            if (cbQDTQ8.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 525, y, x + 530, y + 10);
                e.Graphics.DrawLine(pb2, x + 530, y + 10, x + 535, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 590, y, 10, 10);
            e.Graphics.DrawString("呼吸机", textfront, Brushes.Black, x + 605, y);
            if (cbQDTQ9.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 590, y, x + 595, y + 10);
                e.Graphics.DrawLine(pb2, x + 595, y + 10, x + 600, y);
            }
            y = y + 20; yUnder = y + 13;    
            e.Graphics.DrawRectangle(Pens.Black, x + 90, y, 10, 10);
            e.Graphics.DrawString("自主呼吸", textfront, Brushes.Black, x + 105, y);
            if (cbQDTQa.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 90, y, x + 95, y + 10);
                e.Graphics.DrawLine(pb2, x + 95, y + 10, x + 100, y );
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 155, y, 10, 10);
            e.Graphics.DrawString("充分", textfront, Brushes.Black, x + 170, y);
            if (cbQDTQb.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 155, y, x + 160, y + 10);
                e.Graphics.DrawLine(pb2, x + 160, y + 10, x + 165, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 200, y, 10, 10);
            e.Graphics.DrawString("气道梗阻", textfront, Brushes.Black, x + 215, y);
            if (cbQDTQc.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 200, y, x + 205, y + 10);
                e.Graphics.DrawLine(pb2, x + 205, y + 10, x + 210, y );
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 265, y, 10, 10);
            e.Graphics.DrawString("辅助呼吸", textfront, Brushes.Black, x + 280, y);
            if (cbQDTQd.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 265, y, x + 270, y + 10);
                e.Graphics.DrawLine(pb2, x + 270, y + 10, x + 275, y );
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 330, y, 10, 10);
            e.Graphics.DrawString("机械通气", textfront, Brushes.Black, x + 345, y);
            if (cbQDTQe.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 330, y, x + 335, y + 10);
                e.Graphics.DrawLine(pb2, x + 335, y + 10, x + 340, y);
            }
            e.Graphics.DrawString("潮气量  " +this.txtCQL.Text+"  ml", textfront, Brushes.Black, x + 405, y);
            e.Graphics.DrawString("呼吸频率  " + this.txtHXPL.Text + "  次/分", textfront, Brushes.Black, x + 500, y);
            y = y + 20; yUnder = y + 13;
            e.Graphics.DrawString("监测、记录：", textfront, Brushes.Black, x + 20, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 90, y, 10, 10);
            e.Graphics.DrawString("无创血压", textfront, Brushes.Black, x + 105, y);
            if (cbJCJL1.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 90, y, x + 95, y + 10);
                e.Graphics.DrawLine(pb2, x + 95, y + 10, x + 100, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 155, y, 10, 10);
            e.Graphics.DrawString("心率", textfront, Brushes.Black, x + 170, y);
            if (cbJCJL2.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 155, y, x + 160, y + 10);
                e.Graphics.DrawLine(pb2, x + 160, y + 10, x + 165, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 200, y, 10, 10);
            e.Graphics.DrawString("呼吸", textfront, Brushes.Black, x + 215, y);
            if (cbJCJL3.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 200, y, x + 205, y + 10);
                e.Graphics.DrawLine(pb2, x + 205, y + 10, x + 210, y);
            }
            e.Graphics.DrawString("每  " +this.txtmFZ.Text+"    分钟", textfront, Brushes.Black, x + 255, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 350, y, 10, 10);
            e.Graphics.DrawString("有创压", textfront, Brushes.Black, x + 365, y);
            if (cbJCJL4.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 350, y, x + 355, y + 10);
                e.Graphics.DrawLine(pb2, x + 355, y + 10, x + 360, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 405, y, 10, 10);
            e.Graphics.DrawString("CVP", textfront, Brushes.Black, x + 420, y);
            if (cbJCJL5.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 405, y, x + 410, y + 10);
                e.Graphics.DrawLine(pb2, x + 410, y + 10, x + 415, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 450, y, 10, 10);
            e.Graphics.DrawString("脉搏", textfront, Brushes.Black, x + 465, y);
            if (cbJCJL6.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 450, y, x + 455, y + 10);
                e.Graphics.DrawLine(pb2, x + 455, y + 10, x + 460, y);
            }

            e.Graphics.DrawRectangle(Pens.Black, x + 535, y, 10, 10);
            e.Graphics.DrawString("SPO2", textfront, Brushes.Black, x + 550, y);
            if (cbSP02.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 535, y, x + 540, y + 10);
                e.Graphics.DrawLine(pb2, x + 540, y + 10, x + 545, y);
            }

            e.Graphics.DrawRectangle(Pens.Black, x + 590, y, 10, 10);
            e.Graphics.DrawString("心电图", textfront, Brushes.Black, x + 605, y);
            if (cbXinDT.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 590, y, x + 595, y + 10);
                e.Graphics.DrawLine(pb2, x + 595, y + 10, x + 600, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 650, y, 10, 10);
            e.Graphics.DrawString("其他", textfront, Brushes.Black, x + 665, y);
            if (cbOTHER.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 650, y, x + 655, y + 10);
                e.Graphics.DrawLine(pb2, x + 655, y + 10, x + 660, y);
            }


            y = y + 20; yUnder = y + 13;
            e.Graphics.DrawString("意识状态：", textfront, Brushes.Black, x + 20, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 90, y, 10, 10);
            e.Graphics.DrawString("清醒", textfront, Brushes.Black, x + 105, y);
            if (cbYishi1.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 90, y, x + 95, y + 10);
                e.Graphics.DrawLine(pb2, x + 95, y + 10, x + 100, y );
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 135, y, 10, 10);
            e.Graphics.DrawString("能唤醒", textfront, Brushes.Black, x + 150, y);
            if (cbYishi2.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 135, y, x + 140, y + 10);
                e.Graphics.DrawLine(pb2, x + 140, y + 10, x + 145, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 190, y, 10, 10);
            e.Graphics.DrawString("不能唤醒", textfront, Brushes.Black, x + 205, y);
            if (cbYishi3.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 190, y, x + 195, y + 10);
                e.Graphics.DrawLine(pb2, x + 195, y + 10, x + 200, y);
            }            
            e.Graphics.DrawString("皮肤：", textfront, Brushes.Black, x + 270, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 300, y, 10, 10);
            e.Graphics.DrawString("红润", textfront, Brushes.Black, x + 315, y);
            if (cbPifu1.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 300, y, x + 305, y + 10);
                e.Graphics.DrawLine(pb2, x + 305, y + 10, x + 310, y );
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 345, y, 10, 10);
            e.Graphics.DrawString("温暖", textfront, Brushes.Black, x + 360, y);
            if (cbPifu2.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 345, y, x + 350, y + 10);
                e.Graphics.DrawLine(pb2, x + 350, y + 10, x + 355, y );
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 390, y, 10, 10);
            e.Graphics.DrawString("苍白", textfront, Brushes.Black, x + 405, y);
            if (cbPifu3.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 390, y, x + 395, y + 10);
                e.Graphics.DrawLine(pb2, x + 395, y + 10, x + 400, y);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 435, y, 10, 10);
            e.Graphics.DrawString("凉", textfront, Brushes.Black, x + 450, y);
            if (cbPifu4.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 435, y, x + 440, y + 10);
                e.Graphics.DrawLine(pb2, x + 440, y + 10, x + 445, y );
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 470, y, 10, 10);
            e.Graphics.DrawString("发绀", textfront, Brushes.Black, x + 485, y);
            if (cbPifu5.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 470, y, x + 475, y + 10);
                e.Graphics.DrawLine(pb2, x + 475, y + 10, x + 480, y);
            }
            y = y + 20; yUnder = y + 13;
            e.Graphics.DrawString("特殊指示：  " + this.tbTSZS.Text, textfront, Brushes.Black, x + 5, y);
            y = y + 25; yUnder = y + 15;
            e.Graphics.DrawString("麻醉医师：  " + this.cmbMZYS.Text, textfront, Brushes.Black, x + 200, y);
            e.Graphics.DrawString("手术医师：  " + this.cmbSSYS.Text, textfront, Brushes.Black, x + 400, y);
            y = y + 20; yUnder = y + 13;
            e.Graphics.DrawLine(pb1, x, y, x + 640, y);//画线    
            e.Graphics.DrawString("记录符号：血压 ∧ ∨ 入室 ＞ 出室 ＜ 呼吸 ○ 心率 ● 体温 △ 插管Θ 拔管 Φ", txText, Brushes.Black, x + 80, y+3);
            #endregion
            DateTime dtEnd = new DateTime();//打印截止时间判断        
            DateTime pagetime = new DateTime();
            DataTable dtMax = bll.GetMaxPointPacu(mzjldID);
            if (dtMax.Rows[0][0].ToString() == "")
                dtEnd = DateTime.Now;
            else
                dtEnd = Convert.ToDateTime(dtMax.Rows[0][0]);
            pagetime = ptime; //当前打印页时间
           y = y + 20; yUnder = y + 13;
           e.Graphics.DrawLine(pb1, x, y, x, y + 610);//画线
           e.Graphics.DrawLine(pb1, x + 640, y, x + 640, y + 610);//画线
           e.Graphics.DrawLine(Pens.Black, x, y, x + 640, y);
           e.Graphics.DrawLine(Pens.Black, x, y + 610, x + 640, y + 610);
            e.Graphics.DrawString(" 时 间", txText, Brushes.Black, x + 5, y + 3);
            for (int i = 0; i < 9; i++)
            {
                e.Graphics.DrawString(ptime.ToString("HH:mm"), txText, Brushes.Black, x + 85 + 60 * i, y + 3);
                ptime = ptime.AddMinutes(6 * jcsjjg);
            }
            y = y + 20; yUnder = y + 13;
            e.Graphics.DrawLine(Pens.Black, x, y, x + 640, y);
            //e.Graphics.DrawString("氧 气", ptzt8, Brushes.Black, x + 5, y +3);
            #region<<打印气体>>
            ArrayList ssQT = new ArrayList();
            foreach (adims_MODEL.mzqt mzqt in mzqtList)
            {
                TimeSpan t = new TimeSpan();
                TimeSpan t1 = new TimeSpan();
                if (mzqt.Bz == 1)
                {
                    t = mzqt.Sysj - pagetime;
                    if (csTime > Convert.ToDateTime("1990-01-01"))
                    {
                        t1 = csTime - pagetime;
                    }
                    else
                    {
                        t1 = DateTime.Now - pagetime;
                    }
                }
                if (mzqt.Bz == 2)
                {

                    t = mzqt.Sysj - pagetime;
                    t1 = mzqt.Jssj - pagetime;
                }
                int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100) + x;
                int y1 = y + 8;
                int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 10 / jcsjjg + 100) + x;
                if (x1 > 100 + x && x1 < 640 + x)
                {
                    e.Graphics.DrawString(mzqt.Qtname.ToString() + " " + mzqt.Yl.ToString()+mzqt.Dw, ptzt7, Brushes.Blue, x1 - 10, y1 - 8);
                    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 3, y1 + 6), new Point(x1 + 3, y1 + 6) });
                }
                if (x2 > 100 + x && x2 < 640 + x)
                {
                    if (mzqt.Bz == 1)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x2, y1 + 3), new Point(x2 - 5, y1));
                        e.Graphics.DrawLine(pred2, new Point(x2, y1 + 3), new Point(x2 - 5, y1 + 6));
                    } if (mzqt.Bz == 2)
                    {
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 3, y1 + 6), new Point(x2 + 3, y1 + 6) });
                    }
                }
                if (x1 > 100 + x && x1 < 500 + x && x2 > 100 + x && x2 < 640 + x)
                {
                    e.Graphics.DrawLine(pred2, new Point(x1, y1 + 3), new Point(x2, y1 + 3));
                }
                if (x1 > 100 + x && x1 < 640 + x && x2 > 640 + x)
                {
                    e.Graphics.DrawLine(pred2, new Point(x1, y1 + 3), new Point(640 + x, y1 + 3));
                }
                if (x1 < 100 + x && x2 > 100 + x && x2 < 640 + x)
                {
                    e.Graphics.DrawLine(pred2, new Point(100 + x, y1 + 3), new Point(x2, y1 + 3));
                }
                if (x1 < 100 + x && x2 > 640 + x)
                {
                    e.Graphics.DrawLine(pred2, new Point(100 + x, y1 + 3), new Point(640 + x, y1 + 3));
                }
                dy++;
              
            }

            #endregion
            y = y + 20; yUnder = y + 13;
            //e.Graphics.DrawLine(Pens.Black, x, y, x + 700, y);
            //e.Graphics.DrawString("T", ptzt8, Brushes.Black, x + 10, y + 3);
            //e.Graphics.DrawString("Bp ", ptzt8, Brushes.Black, x + 50, y + 3);
            //e.Graphics.DrawString("RR", ptzt8, Brushes.Black, x + 10, y + 18);
            //e.Graphics.DrawString("HR", ptzt8, Brushes.Black, x + 50, y + 18);
            for (int i = 0; i < 11; i++)
            {
                e.Graphics.DrawLine(Pens.Black, x + 100, y + i * 20, x + 640, y + i * 20);
            }
            int jaa = 41, kaa = 220;
            for (int i = 0; i < 11; i++)
            {
                e.Graphics.DrawString(jaa.ToString(), txText, Brushes.Black, x + 10, y-10 + i * 20+5);
                e.Graphics.DrawString(kaa.ToString(), txText, Brushes.Black, x + 50, y-10  + i * 20+5);
                jaa = jaa - 1; kaa = kaa - 20;
            }
            for (int i = 0; i < 54; i++)
            {
                e.Graphics.DrawLine(pxuxian, (float)(x + 100 + 10 * i), (float)(y - 20), (float)(x + 100 + 10 * i), (float)(y + 20 * 11));
                if (i % 3 == 0)
                    e.Graphics.DrawLine(Pens.Black, (float)(x + 100 + 10 * i), (float)(y - 22), (float)(x + 100 + 10 * i), (float)(y + 20 * 11));

            }
            #region<<打印point>>
            Pen p_red2 = new Pen(Brushes.Red, 2);
            foreach (adims_MODEL.point tp in ssy)
            {
                int x1, y1;
                TimeSpan t = new TimeSpan();
                if (tp.D >= pagetime && tp.D <= pagetime.AddMinutes(54 * jcsjjg))
                {
                    t = tp.D - pagetime;
                    x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100 + x);
                    if (tp.V > 240)
                    {
                        y1 = (int)(y + 20 * 11 - 240);
                        e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Red, x1, y1);
                    }
                    else
                        y1 = (int)(y + 20 * 11 - (int)(tp.V * 1));
                    //e.Graphics.DrawPolygon(Pens.Red, new Point[3] { new Point(x1, y1), new Point(x1 - 4, y1 - 4), new Point(x1 + 4, y1 - 4) });
                    e.Graphics.DrawLines(p_red2, new PointF[3] { new PointF(x1 - 3, y1 - 5), new PointF(x1, y1), new PointF(x1 + 3, y1 - 5) });
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
                if (tp.D >= pagetime && tp.D <= pagetime.AddMinutes(54 * jcsjjg))
                {
                    int x1, y1;
                    TimeSpan t = new TimeSpan();
                    t = tp.D - pagetime;
                    x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100 + x);
                    if (tp.V > 240)
                    {
                        y1 = (int)(y + 20 * 11 - 240);
                        e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Red, x1, y1);
                    }
                    else
                        y1 = (int)(y + 20 * 11 - (int)(tp.V * 1));
                    //e.Graphics.DrawPolygon(Pens.Red, new Point[3] { new Point(x1, y1), new Point(x1 - 4, y1 - 4), new Point(x1 + 4, y1 - 4) });
                    e.Graphics.DrawLines(p_red2, new PointF[3] { new PointF(x1 - 3, y1 + 5), new PointF(x1, y1), new PointF(x1 + 3, y1 + 5) });
                    if (dyd1 != 0)
                        e.Graphics.DrawLine(Pens.Red, new Point(x1, y1), lastpoint1);
                    lastpoint1.X = x1;
                    lastpoint1.Y = y1;
                    dyd1++;
                }

            }
            foreach (adims_MODEL.point tp in mbo)
            {
                if (tp.D >= pagetime && tp.D <= pagetime.AddMinutes(54 * jcsjjg))
                {
                    int x1, y1;
                    TimeSpan t = new TimeSpan();
                    t = tp.D - pagetime;
                    x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100 + x);
                    if (tp.V > 240)
                    {
                        y1 = (int)(y + 20 * 11 - 240);
                        e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Green, x1, y1);
                    }
                    else
                        y1 = (int)(y + 20 * 11 - (int)(tp.V * 1));
                    e.Graphics.FillEllipse(Brushes.Blue, new Rectangle(x1 - 2, y1 - 2, 3, 3));
                    if (dyd2 != 0)
                        e.Graphics.DrawLine(Pens.Blue, new Point(x1, y1), lastpoint2);
                    lastpoint2.X = x1;
                    lastpoint2.Y = y1;
                    dyd2++;
                }
            }
            //float px = 0,py = 0;
            //foreach (adims_MODEL.tw_point tp in tw)
            //{
            //    if (tp.V > 0)
            //    {
            //        if (tp.D >= pagetime && tp.D <= pagetime.AddMinutes(54 * jcsjjg))
            //        {
            //            float x1, y1;
            //            TimeSpan t = new TimeSpan();
            //            t = tp.D - pagetime;
            //            x1 = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100 + x);
            //            if (tp.V > 41)
            //            {
            //                y1 = (float)(y + 20 * 11 - 240);
            //                e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.Green, x1, y1);
            //            }
            //            else
            //                y1 = (float)(y + (41 - tp.V) * 20);
            //            e.Graphics.DrawPolygon(Pens.Maroon, new PointF[3] { new PointF(x1 - 3, y1 + 5), new PointF(x1, y1), new PointF(x1 + 3, y1 + 5) });
            //            //e.Graphics.FillEllipse(Brushes.Blue, new RectangleF(x1 - 2, y1 - 2, 5, 5));
            //            if (dyd2 != 0 && px != 0)
            //                e.Graphics.DrawLine(Pens.Blue, new PointF(x1, y1), new PointF(px, py));
            //            px = (int)x1;
            //            py = (int)y1;
            //            dyd2++;
            //        }
            //    }
            //}
            foreach (adims_MODEL.point tp in hxl)
            {
                if (tp.D >= pagetime && tp.D <= pagetime.AddMinutes(54 * jcsjjg))
                {
                    int x1, y1;
                    TimeSpan t = new TimeSpan();
                    t = tp.D - pagetime;
                    x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100 + x);
                    if (tp.V > 240)
                    {
                        y1 = (int)(y + 20 * 11 - 240);
                        e.Graphics.DrawString(tp.V.ToString(), this.Font, Brushes.DarkCyan, x1, y1);
                    }
                    else
                        y1 = (int)(y + 20 * 11 - (int)(tp.V * 1));
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

            y = y + 20 * 11; yUnder = y + 13;
            for (int i = 0; i < 12; i++)
            {
                ////if (i == 9 || i == 7)
                ////{
                ////    e.Graphics.DrawLine(Pens.Black, x + 100, y + i * 15, x + 640, y + i * 15);
                ////}
                //else
                    e.Graphics.DrawLine(Pens.Black, x, y + i * 15, x + 640, y + i * 15);
            }
            for (int i = 0; i < 54; i++)
            {
                e.Graphics.DrawLine(pxuxian, (float)(x + 100 + 10 * i), (float)(y), (float)(x + 100 + 10 * i), (float)(y + 15 * 11));
                if (i % 3 == 0)
                    e.Graphics.DrawLine(Pens.Black, (float)(x + 100 + 10 * i), (float)(y), (float)(x + 100 + 10 * i), (float)(y + 15 * 11));

            }
            e.Graphics.DrawString("尿 量(ml)", ptzt8, Brushes.Black, x + 10, y + 3);

            #region<<打印尿量>>

            int clCOUNT = 1;
            foreach (adims_MODEL.clcxqt cl in cnList)
            {
                TimeSpan t = new TimeSpan();
                t = cl.D - pagetime;
                int x_nl = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100 + x);
                if (x_nl > 100 + x && x_nl < 700 + x)
                {
                    e.Graphics.FillRectangle(Brushes.Pink, x_nl, y + 3, 18, 11);
                    e.Graphics.DrawString(cl.V.ToString(), this.Font, Brushes.Black, new Point(x_nl, y + 3));
                    clCOUNT++;
                }
            }
            #endregion

            #region<<打印监护项目>>

            int jhy = 0;
            foreach (string s in jhxmy)
            {
                e.Graphics.DrawString(s, ptzt8, Brushes.Black, new Point(x + 10, jhy * 15 + y + 18));
                int count1 = 0;
                foreach (adims_MODEL.jhxm jt in jhxmv)
                {
                   
                    if (jt.Sy == s&&jt.V!=0)
                    {
                        if (jt.D >= pagetime && jt.D <= pagetime.AddMinutes(54 * jcsjjg))
                        {
                            TimeSpan t = new TimeSpan();
                            t = jt.D - pagetime;
                            int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100 + x);
                            int y1 = y + 18 + jhy * 15;
                            if (count1 % 2 == 0)
                            {                              
                            e.Graphics.FillRectangle(Brushes.Pink, x1, y1, 12, 11);
                            e.Graphics.DrawString(jt.V.ToString(), (jt.V / 100 > 0 ? ptzt7 : ptzt8), Brushes.Black, new Point((jt.V / 100 > 0 ? x1 - 2 : x1), y1));
                            }
                            count1++;
                        }
                    }
                }
                jhy++;
            }
            ///体温
            int tw_count = 0;
             e.Graphics.DrawString("体温", ptzt8, Brushes.Black, new Point(x + 10, jhy * 15 + y + 18));
            foreach (adims_MODEL.tw_point tp in tw)
            {
                if (tp.V > 0)
                {
                    if (tp.D >= pagetime && tp.D <= pagetime.AddMinutes(54 * jcsjjg))
                    {
                        TimeSpan t = new TimeSpan();
                        t = tp.D - pagetime;
                        int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100 + x);
                        int y1 = y + 18 + jhy * 15;
                        if (tw_count % 2 == 0)
                        {
                            if (tp.V.ToString().Length > 3 )
                            {
                                e.Graphics.FillRectangle(Brushes.Pink, x1, y1, 15, 11);
                            }
                            else
                            {
                                e.Graphics.FillRectangle(Brushes.Pink, x1, y1, 12, 11);
                            }
                           
                            e.Graphics.DrawString(tp.V.ToString(), (tp.V.ToString().Length > 3 ? ptzt6 : ptzt7), Brushes.Black, new Point((tp.V.ToString().Length > 3 ? x1 - 2 : x1), y1));
                        }
                        tw_count++;
                    }
                }
            }
            #endregion
            e.Graphics.DrawString("输 血(ml)", ptzt8, Brushes.Black, new Point(x + 10, y + 15 * 4));

            #region<<打印输血>> 
            int sxi = 0;
            ArrayList sssSX = new ArrayList();
            foreach (adims_MODEL.shuxue sx in sx1ist)//画输血
            {
                if (sxi > 2)
                {
                    break;
                }
                if (sssSX.Contains(sx.Name))
                    sxi = sxi - 1;
                if (sx.Cxyy == false)
                {
                    TimeSpan t = new TimeSpan();
                    t = sx.Kssj - pagetime;
                    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100) + x;
                    int y1 = y + 15 * 4 + 7 + sxi * 15;
                    if (x1 > 100 + x && x1 < 500 + x)
                    {
                        e.Graphics.DrawString(sx.Name.ToString() + " " + sx.Jl.ToString(), ptzt7, Brushes.Blue, x1 - 10, y1 - 8); e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 3, y1 + 6), new Point(x1 + 3, y1 + 6) });
                    }
                }
                if (sx.Cxyy == true)
                {
                    TimeSpan t = new TimeSpan();
                    TimeSpan t1 = new TimeSpan();
                    if (sx.Bz == 1)
                    {
                        t = sx.Kssj - pagetime;
                        if (csTime > Convert.ToDateTime("1990-01-01"))
                        {
                            t1 = csTime - pagetime;
                        }
                        else
                        {
                            t1 = DateTime.Now - pagetime;
                        }
                    }
                    if (sx.Bz == 2)
                    {
                        t = sx.Kssj - pagetime;
                        t1 = sx.Jssj - pagetime;
                    }
                    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100) + x;
                    int y1 = y + 15 * 4 + 7 + sxi * 15;
                    int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 10 / jcsjjg + 100) + x;
                    if (x1 > 100 + x && x1 < 640 + x)
                    {
                        e.Graphics.DrawString(sx.Name.ToString() + " " + sx.Jl.ToString(), ptzt7, Brushes.Blue, x1 - 10, y1 - 8);
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 3, y1 + 6), new Point(x1 + 3, y1 + 6) });
                    }
                    if (x2 > 100 + x && x2 < 640 + x)
                    {
                        if (sx.Bz == 1)
                        {
                            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 3), new Point(x2 - 5, y1));
                            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 3), new Point(x2 - 5, y1 + 6));
                        } if (sx.Bz == 2)
                        {
                            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 3, y1 + 6), new Point(x2 + 3, y1 + 6) });
                        }
                    }
                    if (x1 > 100 + x && x1 < 500 + x && x2 > 100 + x && x2 < 640 + x)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x1, y1 + 3), new Point(x2, y1 + 3));
                    }
                    if (x1 > 100 + x && x1 < 640 + x && x2 > 640 + x)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x1, y1 + 3), new Point(640 + x, y1 + 3));
                    }
                    if (x1 < 100 + x && x2 > 100 + x && x2 < 640 + x)
                    {
                        e.Graphics.DrawLine(pred2, new Point(100 + x, y1 + 3), new Point(x2, y1 + 3));
                    }
                    if (x1 < 100 + x && x2 > 640 + x)
                    {
                        e.Graphics.DrawLine(pred2, new Point(100 + x, y1 + 3), new Point(640 + x, y1 + 3));
                    }
                }
                sxi++;
                sssSX.Add(sx.Name);
                //TimeSpan t = new TimeSpan();
                //t = sx.Kssj - pagetime;
                //int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100) + x;
                //int y1 = 0;
                //if (sxCount % 2 == 0)
                //    y1 = y + 15 * 6 + 7;
                //else
                //    y1 = y + 15 * 7 + 8;
                //if (x1 > 100 + x && x1 < 700 + x)
                //{
                //    e.Graphics.DrawString(sx.Name.ToString() + " " + sx.Jl.ToString(), ptzt7, Brushes.Blue, x1 - 10, y1 - 8);
                //    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 3, y1 + 6), new Point(x1 + 3, y1 + 6) });
                //}
            }

            #endregion
            e.Graphics.DrawString("输 液(ml)", ptzt8, Brushes.Black, new Point(x + 10, y + 15 * 6));

            #region<<打印输液>>

            int syi = 0;
            ArrayList sssSY = new ArrayList();
            foreach (adims_MODEL.shuye sy in sy1ist)
            {
                if (syi>3)
                {
                    break; 
                }
                if (sssSY.Contains(sy.Name))
                    syi = syi - 1;
                if (sy.Cxyy == false)
                {
                    TimeSpan t = new TimeSpan();
                    t = sy.Kssj - pagetime;
                    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100) + x;
                    int y1 = y + 15 * 6 + 7+15*syi;
                    if (x1 > 100 + x && x1 < 500 + x)
                    {
                      e.Graphics.DrawString(sy.Name.ToString() + " " + sy.Jl.ToString(), ptzt7, Brushes.Blue, x1 - 10, y1 - 8);                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 3, y1 + 6), new Point(x1 + 3, y1 + 6) });
                    }
                }
                if (sy.Cxyy == true)
                {
                    TimeSpan t = new TimeSpan();
                    TimeSpan t1 = new TimeSpan();
                    if (sy.Bz == 1)
                    {
                        t = sy.Kssj - pagetime;
                        if (csTime > Convert.ToDateTime("1990-01-01"))
                        {
                            t1 = csTime - pagetime;
                        }
                        else
                        {
                            t1 = DateTime.Now - pagetime;
                        }
                    }
                    if (sy.Bz == 2)
                    {
                        t = sy.Kssj - pagetime;
                        t1 = sy.Jssj - pagetime;
                    }                 
                    //t = sy.Kssj - pagetime;
                    //t1 = sy.Jssj - pagetime;
                    int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100) + x;
                    int y1 = y + 15 * 6 + 7 + 15 * syi;
                    int x2 = (int)((t1.Days * 24 * 60 + t1.Hours * 60 + t1.Minutes) * 10 / jcsjjg + 100) + x;
                    if (x1 > 100 + x && x1 < 640 + x)
                    {
                        e.Graphics.DrawString(sy.Name.ToString() + " " + sy.Jl.ToString(), ptzt7, Brushes.Blue, x1 - 10, y1 - 8);
                        e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 3, y1 + 6), new Point(x1 + 3, y1 + 6) });
                    }
                    if (x2 > 100 + x && x2 < 640 + x)
                    {
                        if (sy.Bz == 1)
                        {
                            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 3), new Point(x2 - 5, y1));
                            e.Graphics.DrawLine(pred2, new Point(x2, y1 + 3), new Point(x2 - 5, y1 + 6));
                        } if (sy.Bz == 2)
                        {
                            e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x2, y1), new Point(x2 - 3, y1 + 6), new Point(x2 + 3, y1 + 6) });
                        }
                    }
                    if (x1 > 100 + x && x1 < 500 + x && x2 > 100 + x && x2 < 640 + x)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x1, y1 + 3), new Point(x2, y1  + 3));
                    }
                    if (x1 > 100 + x && x1 < 640 + x && x2 > 640 + x)
                    {
                        e.Graphics.DrawLine(pred2, new Point(x1, y1  + 3), new Point(640 + x, y1 + 3));
                    }
                    if (x1 < 100 + x && x2 > 100 + x && x2 < 640 + x)
                    {
                        e.Graphics.DrawLine(pred2, new Point(100 + x, y1 + 3), new Point(x2, y1 + 3));
                    }
                    if (x1 < 100 + x && x2 > 640 + x)
                    {
                        e.Graphics.DrawLine(pred2, new Point(100 + x, y1 + 3), new Point(640 + x, y1+ 3));
                    }
                }
                syi++;
                sssSY.Add(sy.Name);
                //TimeSpan t = new TimeSpan();
                //t = sy.Kssj - pagetime;
                //int x1 = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100) + x;
                //int y1 = 0;
                ////if (syCount % 2 == 0)
                //    y1 = y + 15 * 7 + 7;
                ////else
                ////    y1 = y + 15 * 9 + 8;
                //if (x1 > 100 + x && x1 < 640 + x)
                //{
                //    e.Graphics.DrawString(sy.Name.ToString() + " " + sy.Jl.ToString(), ptzt7, Brushes.Blue, x1 - 10, y1 - 8);
                //    e.Graphics.FillPolygon(Brushes.Black, new Point[3] { new Point(x1, y1), new Point(x1 - 3, y1 + 6), new Point(x1 + 3, y1 + 6) });
                //}
               
            }
            #endregion
            e.Graphics.DrawString("失 血(ml)", ptzt8, Brushes.Black, x + 10, y + 15 * 9);

             #region<<打印失血>>
            foreach (adims_MODEL.clcxqt cx in cxList)
            {
                TimeSpan t = new TimeSpan();
                t = cx.D - otime;
                int x_cx = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100 + x);
                if (x_cx > 100 + x && x_cx < 640 + x)
                {
                    e.Graphics.FillRectangle(Brushes.Pink, x_cx, y + 15 * 9, 18, 11);
                    e.Graphics.DrawString(cx.V.ToString(), this.Font, Brushes.Black, new Point(x_cx, y+15 * 9));
                   
                }
            }
            #endregion
            e.Graphics.DrawString("引流量(ml)", ptzt8, Brushes.Black, x + 10, y + 15 * 10);
            #region 引流量
            foreach (adims_MODEL.clcxqt cx in yllList)
            {
                TimeSpan t = new TimeSpan();
                t = cx.D - otime;
                int x_cx = (int)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 100 + x);
                if (x_cx > 100 + x && x_cx < 640 + x)
                {
                    e.Graphics.FillRectangle(Brushes.Pink, x_cx, y + 15 * 10, 18, 11);
                    e.Graphics.DrawString(cx.V.ToString(), this.Font, Brushes.Black, new Point(x_cx, y + 15 * 10));

                }
            }
            #endregion
            #region<<打印入出室，插拔管>>
            y = y + 150+15; yUnder = y + 15;
            e.Graphics.DrawLine(Pens.Black, x, y, x + 640, y);
            e.Graphics.DrawString("事件", ptzt8, Brushes.Black, x + 20, y + 2);
            DataTable dt = pacuDal.GetPACU_ByMzjldId(mzjldID);
            //术中事件
            int num=1;
            foreach (adims_MODEL.szsj s in sjList)
            {
                if (s.D >= pagetime && s.D <= pagetime.AddHours(jcsjjg))
                {
                    TimeSpan t = s.D - pagetime;
                    float tx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 95 + x);
                    e.Graphics.FillRectangle(Brushes.Pink, tx, y + 5, 9, 9);
                    e.Graphics.DrawString(num.ToString(), ptzt7, Brushes.Black, tx,y+5);
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
                    float tx = (float)((t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) * 10 / jcsjjg + 95 + x);
                    //e.Graphics.FillRectangle(Brushes.Green, tx, y + 5, 9, 9);
                    e.Graphics.DrawString(num1.ToString(), ptzt7, Brushes.Green, tx, y + 5);
                    num1++;
                }
            }
            if (dt.Rows.Count != 0)
            { 
                if (dt.Rows[0]["rssj"].ToString() != "")
                {
                    rsTime = Convert.ToDateTime(dt.Rows[0]["rssj"]);
                    TimeSpan ts1 = rsTime - pagetime;
                    int xx1 = (int)((ts1.Days * 24 * 60 + ts1.Hours * 60 + ts1.Minutes) * 10 / jcsjjg + 95 + x);
                    if (xx1 >= 95 + x && xx1 < 700 + x)
                    {
                        e.Graphics.DrawString("＞", this.Font, Brushes.Black, xx1, y+5);
                    }
                }
                if (dt.Rows[0]["cssj"].ToString() != "")
                {
                    csTime = Convert.ToDateTime(dt.Rows[0]["cssj"]);
                    TimeSpan ts2 = csTime - pagetime;
                    int xx1 = (int)((ts2.Days * 24 * 60 + ts2.Hours * 60 + ts2.Minutes) * 10 / jcsjjg + 95 + x);
                    if (xx1 >=95 + x && xx1 < 700 + x)
                    {
                        e.Graphics.DrawString("＜", this.Font, Brushes.Black, xx1, y+5);
                    }

                }

                if (dt.Rows[0]["CGSJ"].ToString() != "")
                {
                    cgTime = Convert.ToDateTime(dt.Rows[0]["CGSJ"]);
                    TimeSpan ts3 = cgTime - pagetime;
                    int xx1 = (int)((ts3.Days * 24 * 60 + ts3.Hours * 60 + ts3.Minutes) * 10 / jcsjjg + 95 + x);
                    if (xx1 >= 95 + x && xx1 < 700 + x)
                    {
                        e.Graphics.DrawString("Θ", this.Font, Brushes.Black, xx1, y+5);
                    }

                }
                if (dt.Rows[0]["bgsj"].ToString() != "")
                {
                    bgTime = Convert.ToDateTime(dt.Rows[0]["bgsj"]);
                    TimeSpan ts4 = bgTime - pagetime;
                    int xx1 = (int)((ts4.Days * 24 * 60 + ts4.Hours * 60 + ts4.Minutes) * 10 / jcsjjg + 95 + x);
                    if (xx1 >= 95 + x && xx1 < 700 + x)
                    {
                        e.Graphics.DrawString("Φ", this.Font, Brushes.Black, xx1, y+5);
                    }
                }
            }

            #endregion

            y = y + 20; yUnder = y + 15;
            e.Graphics.DrawLine(Pens.Black, x, y, x + 640, y);
            e.Graphics.DrawString("用药记录", textfront, Brushes.Black, x + 110, y + 3);
            e.Graphics.DrawString("治疗记录", textfront, Brushes.Black, x + 450, y + 3);
            e.Graphics.DrawLine(Pens.Black, x + 320, y, x + 320, y + 180-15);
            y = y + 20; yUnder = y + 15;
            e.Graphics.DrawLine(Pens.Black, x, y, x + 640, y);
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                e.Graphics.DrawString(listBox1.Items[i].ToString(), txText, Brushes.Black, x + 10, y -130 + (i + 9) * 15);
            }
            for (int i = 0; i < listBox3.Items.Count; i++)
            {
                e.Graphics.DrawString(listBox3.Items[i].ToString(), txText, Brushes.Black, x + 340, y - 130 + (i + 9) * 15);
            }
            y = y + 170-15; yUnder = y + 15;
            e.Graphics.DrawString("离开恢复室情况： " + tbHFSQK.Text, textfront, Brushes.Black, x + 5, y);           
          
            e.Graphics.DrawString("责任护士 " + txtZXHS.Text, textfront, Brushes.Black, x + 300, y);
            e.Graphics.DrawLine(Pens.Black, x + 350, yUnder, x + 500, yUnder);
            e.Graphics.DrawString("麻醉医师 " + txtMZYS.Text, textfront, Brushes.Black, x + 510, y);
            e.Graphics.DrawLine(Pens.Black, x + 560, yUnder, x + 640, yUnder);
            //e.Graphics.DrawString("记录符号：血压 ∧ ∨ 入室 ＞ 出室 ＜ 呼吸 ○ 心率 ● 体温 △ 置管Θ 拔管 Φ", txText, Brushes.Black, x + 80, 930);

            if (ptime < dtEnd)
            {
                e.HasMorePages = true;
                //e.Graphics.DrawString("第 " + iYema.ToString() + " 页", ptzt8, Brushes.Black, new Point(330 + x, 950));
                ////e.Graphics.DrawLine(ptp, new Point(350 + x, 1005 + y), new Point(365 + x, 1005 + y));
                iYema++;
            }
            else
            {
                e.HasMorePages = false; ptime = fristopen;
                //e.Graphics.DrawString("第 " + iYema.ToString() + " 页", ptzt8, Brushes.Black, new Point(330 + x, 950));
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
            //string zid = this.tbZhuyuanID.Text;
            //string pid = PatID;
            //int a = at.update_PACU(this.tbWeight.Text.Trim(), zid, pid);
            int a = SavePACU();
            if (a >= 1)
            {
                MessageBox.Show("保存PACU记录单成功");
            }
            else
            {
                MessageBox.Show("保存PACU记录单失败！");
            }
        }

        /// <summary>
        /// 保存PACU单
        /// </summary>
        /// <returns></returns>
        public int SavePACU() 
        {
            int result = 0;
            List<string> pacu = new List<string>();
            string AddItem = "";           
            pacu.Add(this.cmbSJJG.Text);//间隔时间
            AddItem = "";//麻醉种类
            if (cbMZFF1.Checked) AddItem += "1";
            if (cbMZFF2.Checked) AddItem += "2";
            if (cbMZFF3.Checked) AddItem += "3";
            if (cbMZFF4.Checked) AddItem += "4";
            if (cbMZFF5.Checked) AddItem += "5";
            if (cbMZFF6.Checked) AddItem += "6";
            if (cbMZFF7.Checked) AddItem += "7";
            if (cbMZFF8.Checked) AddItem += "8";
            if (cbMZFF9.Checked) AddItem += "9";
            if (cbMZFFa.Checked) AddItem += "a";
            if (cbMZFFb.Checked) AddItem += "b";
            if (cbMZFFc.Checked) AddItem += "c";
            pacu.Add(AddItem);
            AddItem = "";//术中特殊情况
            if (cbSQTSQK1.Checked) AddItem += "1";
            if (cbSQTSQK2.Checked) AddItem += "2";
            if (cbSQTSQK3.Checked) AddItem += "3";
            if (cbSQTSQK4.Checked) AddItem += "4";
            if (cbSQTSQK5.Checked) AddItem += "5";
            if (cbSQTSQK6.Checked) AddItem += "6";
            if (cbSQTSQK7.Checked) AddItem += "7";
            pacu.Add(AddItem);
            AddItem = "";//体位
            if (cbTiwei1.Checked) AddItem += "1";
            if (cbTiwei2.Checked) AddItem += "2";
            if (cbTiwei3.Checked) AddItem += "3";
            if (cbTiwei4.Checked) AddItem += "4";
            if (cbTiwei5.Checked) AddItem += "5";
            if (cbTiwei6.Checked) AddItem += "6";
            if (cbTiwei7.Checked) AddItem += "7";
            if (cbTiwei8.Checked) AddItem += "8";
            if (cbTiwei9.Checked) AddItem += "9";
            pacu.Add(AddItem);
            pacu.Add(this.tbTiweiOther.Text);
            AddItem = "";//气道与通气
            if (cbQDTQ1.Checked) AddItem += "1";
            if (cbQDTQ2.Checked) AddItem += "2";
            if (cbQDTQ3.Checked) AddItem += "3";
            if (cbQDTQ4.Checked) AddItem += "4";
            if (cbQDTQ5.Checked) AddItem += "5";
            if (cbQDTQ6.Checked) AddItem += "6";
            if (cbQDTQ7.Checked) AddItem += "7";
            if (cbQDTQ8.Checked) AddItem += "8";
            if (cbQDTQ9.Checked) AddItem += "9";
            if (cbQDTQa.Checked) AddItem += "a";
            if (cbQDTQb.Checked) AddItem += "b";
            if (cbQDTQc.Checked) AddItem += "c";
            if (cbQDTQd.Checked) AddItem += "d";
            if (cbQDTQe.Checked) AddItem += "e";
            pacu.Add(AddItem);
            pacu.Add(this.txtCQL.Text);//潮气量
            pacu.Add(this.txtCQL.Text);//呼吸频率
            AddItem = "";//监测、记录
            if (cbJCJL1.Checked) AddItem += "1";
            if (cbJCJL2.Checked) AddItem += "2";
            if (cbJCJL3.Checked) AddItem += "3";
            if (cbJCJL4.Checked) AddItem += "4";
            if (cbJCJL5.Checked) AddItem += "5";
            if (cbJCJL6.Checked) AddItem += "6";
            if (cbSP02.Checked) AddItem += "7";
            if (cbXinDT.Checked) AddItem += "8";
            if (cbOTHER.Checked) AddItem += "9";
            pacu.Add(AddItem);
            pacu.Add(this.txtmFZ.Text);//每分钟
            AddItem = "";//意识状态
            if (cbYishi1.Checked) AddItem += "1";
            if (cbYishi2.Checked) AddItem += "2";
            if (cbYishi3.Checked) AddItem += "3";
            pacu.Add(AddItem);
            AddItem = "";//皮肤
            
            if (cbPifu1.Checked) AddItem += "1";
            if (cbPifu2.Checked) AddItem += "2";
            if (cbPifu3.Checked) AddItem += "3";
            if (cbPifu4.Checked) AddItem += "4";
            if (cbPifu5.Checked) AddItem += "5";
            pacu.Add(AddItem);
            pacu.Add(this.tbTSZS.Text);
            pacu.Add(this.cmbMZYS.Text);
            pacu.Add(this.cmbSSYS.Text);
            pacu.Add(this.tbHFSQK.Text);           
            pacu.Add(this.txtMZYS.Text);
            pacu.Add(this.txtZXHS.Text);
            pacu.Add(this.tbZhuyuanID.Text);//住院号
            pacu.Add(Convert.ToString(mzjldID));
            pacu.Add(Odate);
            result = _PacuDal.UpdatePACU(pacu);
            return result;
        }
        private void btnTsyy_Click(object sender, EventArgs e)
        {
            addSzsj fromszsj = new addSzsj(mzjldID, 1);
            fromszsj.ShowDialog();
            BindShijian();
            pictureBox1.Refresh();
        }

        private void txtMZYS_DoubleClick(object sender, EventArgs e)
        {
            SelectMZYSandHushi F1 = new SelectMZYSandHushi(1, txtMZYS);
            F1.ShowDialog();
        }

        private void txtMZYS_TextChanged(object sender, EventArgs e)
        {
            int i = pacuDal.UpdatePACU_mzys(mzjldID, txtMZYS.Text);
        }

        private void txtZXHS_TextChanged(object sender, EventArgs e)
        {
            int i = pacuDal.UpdatePACU_zxhs(mzjldID, txtZXHS.Text);
        }

        private void txtZXHS_DoubleClick(object sender, EventArgs e)
        {
            SelectMZYSandHushi F1 = new SelectMZYSandHushi(2, txtZXHS);
            F1.ShowDialog();
        }

        private void btnLeft_Click(object sender, EventArgs e)
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
            lb_cguan.Location = new Point(lb_cguan.Location.X - 120, lb_cguan.Location.Y);
            lb_bguan.Location = new Point(lb_bguan.Location.X - 120, lb_bguan.Location.Y);
            //插管，拔管时间暂时未完成
            lab1.Text = "";
            pictureBox1.Refresh();
        }

        private void btnRight_Click(object sender, EventArgs e)
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
            lb_cguan.Location = new Point(lb_cguan.Location.X + 120, lb_cguan.Location.Y);
            lb_bguan.Location = new Point(lb_bguan.Location.X + 120, lb_bguan.Location.Y);
            lab1.Text = "";
            pictureBox1.Refresh();
        }

        private void cmbSJJG_SelectedIndexChanged(object sender, EventArgs e)
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
            PointManage slj = new PointManage(mzjldID, 1,0,0,0,0);
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
       
        private void BindJikongTime()
        {
            DataTable dtMZ_Info = _MzjldDal.GetMzjldByMzjldId(mzjldID);
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
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int a = _PacuDal.UpdatePACU_Otime(this.dtInRoomTime.Value.ToString("yyyy-MM-dd HH:mm"), MzjldID);
            if (a > 0)
            {
                otime = this.dtInRoomTime.Value;
                BindShijiandian();
                BindRSCSCGBG();
                pictureBox2.Refresh();
            }
        }
        /// <summary>
        /// 宝莱特
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerBLT_Tick(object sender, EventArgs e)
        {
            ProceedreceivedDate_BLT();
            int s = Convert.ToInt32(cmbSJJG.Text);
            timerBLT.Interval = s * 60 * 1000;
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
                            if (sss>30)
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
                            if (sss > TEMP1&&sss>30)
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
            //DateTime now = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));          
            if (dal.selectJianCeData(ksjcTime, mzjldID, 1).Rows.Count == 0)
            {
                int fa = dal.insertJianCeDataPACU(mzjldID, ABP_SYS, ABP_DIA, ABP_MAP, RR, HR, PR, SPO2, ETCO2, TEMP1, ksjcTime);
            }
            sr.Close();
            fs.Close();
        }
        /// <summary>
        /// 迈瑞
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
                    //       SocketThread.Abort();
                }
            }
            //int s = Convert.ToInt32(cmbSJJG.Text);
            //timerMR.Interval = s * 60 * 1000;
        }    
        private void tbHFSQK_DoubleClick(object sender, EventArgs e)
        {
            Select_lifhsqk f1 = new Select_lifhsqk(tbHFSQK);
            f1.ShowDialog();
        }
  

    }
}

