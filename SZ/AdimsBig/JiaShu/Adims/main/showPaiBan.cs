using adims_BLL;
using adims_DAL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace main
{
    public partial class showPaiBan : Form
    {
        DB2help db2 = new DB2help();
        AdimsController bll = new AdimsController();
        AdimsProvider DAL = new AdimsProvider();
        string patname1 = "";
        bool pingIP = false;
        public showPaiBan()
        {
            InitializeComponent();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            this.label3.Left -= 1;
            if (this.label3.Right < 20)
            {
                this.label3.Left = this.Width;
            }
        }

        private void timerInnet_Tick(object sender, EventArgs e)
        {
            pingIP = PingHost("10.0.100.87");
            if (pingIP)
            {
                showPaiBan_Load(sender, e);
            }
            
        }
        private void showPaiBan_Load(object sender, EventArgs e)
        {
            timerInnet.Start();
            this.timer1.Interval = 15000;
            this.label1.Visible = true;
            if (true)
            {
                timerInnet.Stop();
                this.timer1.Start();//定时器启动
                this.timer2.Start();//定时器启动
                this.label1.Visible = false;
                #region 行高
                DataGridViewRow row = this.JSdgv.RowTemplate;
                row.Height = 85;
                #endregion

                #region 数据的轮换
                if (count >= MaxCount)
                {
                    count = 0;
                }
                paibanDataBind();
                if (count >= MaxCount)
                {
                    count = 0;
                }
                else
                {
                    count = count + Num;
                }
                #endregion
            }
           

            #region 时间
           
            this.label3.Text = DateTime.Now.Year + "年" + DateTime.Now.Month + "月" + DateTime.Now.Day + "日 " + DateTime.Now.ToString("HH:mm") + "    请耐心等待...";
            #endregion
        }
        public bool PingHost(string Address, int TimeOut = 1000)
        {
            try
            {
                using (System.Net.NetworkInformation.Ping PingSender = new System.Net.NetworkInformation.Ping())
                {
                    PingOptions Options = new PingOptions();
                    Options.DontFragment = true;
                    string Data = "test";
                    byte[] DataBuffer = Encoding.ASCII.GetBytes(Data);
                    PingReply Reply = PingSender.Send(Address, TimeOut, DataBuffer, Options);
                    if (Reply.Status == IPStatus.Success)
                        return true;
                    return false;
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("网络连接异常");
                return false;
            }

        }
        int MaxCount = 0;//最大数据行术 滚动参数
        int count = 0;//数据行数  滚动参数
        int Num = 6;//每次换动的数据行数  滚动参数
        string conString = ConfigurationManager.AppSettings["ConnectionString"];
        string num = "手术结束";
       
        private int paibanDataBind()
        {
            //string conString = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection myConn = new SqlConnection(conString);
            try
            {
                #region 循环获取数据
                string dtime = DateTime.Now.ToString("yyyy-MM-dd");
                myConn.Open();
                string sql1 = @" select S.id,A.patname,A.patdpm,A.patbedno, 
                                 case S.state 
                                 when '1'  then  '手术准备'   
                                 when  '2'  then '麻醉开始' 
                                 when '3' then '手术中'  
                                 when '4' then '手术结束' end state 
                                 from  Adims_OTypesetting as  A join ssjstate as  S  
                                 on A.patid=S.patid
                                 where S.state!='0' and A.odate ='" + dtime + "'";
                string sql2 = "  and A.operaddress='010601' order by S.state desc ";
                string sql = sql1 + sql2;
                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter(sql, myConn);
                sda.Fill(ds, "Adims_OTypesetting");
                JSdgv.Rows.Clear();
                int temp = 0;
                for (int i = count; i < ds.Tables[0].Rows.Count; i++)
                {
                    JSdgv.Rows.Add();
                    JSdgv.Rows[temp].Cells["id"].Value = ds.Tables[0].Rows[i]["id"] + "";
                    JSdgv.Rows[temp].Cells["patname"].Value = ds.Tables[0].Rows[i]["patname"] + "";
                    JSdgv.Rows[temp].Cells["patdpm"].Value = ds.Tables[0].Rows[i]["patdpm"] + "";
                    JSdgv.Rows[temp].Cells["patbedno"].Value = ds.Tables[0].Rows[i]["patbedno"] + "";
                    JSdgv.Rows[temp].Cells["state"].Value = ds.Tables[0].Rows[i]["state"] + "";
                    temp++;
                    if (temp == Num)
                    {
                        break;
                    }
                }
                #endregion
                MaxCount = ds.Tables[0].Rows.Count;
            }
            catch (Exception ex)
            {
         
                HisMessage.ShowHintByTime("连接数据库失败！错误原因：" + ex.Message, 3000);
            }
            #region 名字中的*
            for (int i = 0; i < JSdgv.Rows.Count; i++)
            {
                if (JSdgv.Rows[i].Cells["patname"].Value.ToString() != "" || JSdgv.Rows[i].Cells["patname"].Value.ToString() != null)
                {
                    string s = JSdgv.Rows[i].Cells["patname"].Value.ToString();
                    if (s.Length >= 2)
                    {
                        JSdgv.Rows[i].Cells["patname"].Value = s.Substring(0, 1) + "*" + s.Substring(2);
                    }
                }
            }
            #endregion
            #region 显示
            for (int i = 0; i < JSdgv.Rows.Count; i++)
            {
                string ZT = JSdgv.Rows[i].Cells["state"].Value.ToString();
                if (ZT == "麻醉复苏")
                {
                    label3.Text = "请  " + JSdgv.Rows[i].Cells["patname"].Value.ToString() + "  的家属准备接病人";
                }
                else
                {
                    #region 时间
                    string Minute;
                    if (DateTime.Now.Minute <= 9)
                    {
                        Minute = "0" + DateTime.Now.Minute;
                    }
                    else
                    {
                        Minute = "" + DateTime.Now.Minute;
                    }
                    this.label3.Text = DateTime.Now.Year + "年" + DateTime.Now.Month + "月" + DateTime.Now.Day + "日 " + DateTime.Now.Hour + ":" + Minute + "    请耐心等待...";
                    #endregion
                }
            }
            #endregion
            return MaxCount;
        }
      
        private void timer3_Tick_1(object sender, EventArgs e)
        {
            // 停止定时器
            timer3.Stop();
            // 向对话框发送按键 Enter
            SendKeys.Send("ENTER");
        }
        //private void StartKiller()
        //{
        //    System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        //    timer.Interval = 3000; //3秒启动
        //    timer.Tick += new EventHandler(Timer_Tick);
        //    timer.Start();
        //}

        //private void Timer_Tick(object sender, EventArgs e)
        //{
        //    KillMessageBox();
        //    //停止Timer
        //    ((System.Windows.Forms.Timer)sender).Stop();
        //}
        //[DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        //private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        //public const int WM_CLOSE = 0x10;
        //private void KillMessageBox()
        //{
        //    //按照MessageBox的标题，找到MessageBox的窗口
        //    cIntPtr ptr = FindWindow("提示", "MessageBox");
        //    if (ptr == IntPtr.Zero)
        //    {
        //        //找到则关闭MessageBox窗口
        //        PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        //    }
        //}

      
    }
}
