
using adims_DAL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
namespace Adims.LargeScreenWaitZone
{
    public partial class CurrentOperationInfo : Form
    {
        
        string patname1 = "";
        bool pingIP = false;
        public CurrentOperationInfo()
        {
            InitializeComponent();

        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            this.labelTime.Left -= 1;
            if (this.labelTime.Right < 20)
            {
                this.labelTime.Left = this.Width;
            }
        }


        private void showPaiBan_Load(object sender, EventArgs e)
        {

            this.timer1.Interval = 2000;
            this.timer1.Start();//定时器启动
            this.timer2.Start();//定时器启动
            pingIP = PingHost(ServerIP);
            if (pingIP)
            {
                lbError.Visible = false;
                lbError.Text = "";

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
                    count = count + PageNum;
                }
                #endregion
            }
            else
            {
                lbError.Visible = true;
                lbError.Text = "服务器链接失败！！！";
            }


            #region 时间

            this.labelTime.Text = DateTime.Now.Year + "年" + DateTime.Now.Month + "月" + DateTime.Now.Day + "日 " + DateTime.Now.ToString("HH:mm") + "    请耐心等待...";
            #endregion
        }
        DBConn dbCon = new DBConn();
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
                lbError.Visible = true;
                lbError.Text = "网络连接异常,请检查！！！";
                //MessageBox.Show("网络连接异常");
                return false;
            }

        }
        int MaxCount = 0;//最大数据行术 滚动参数
        int count = 0;//数据行数  滚动参数
        int PageNum = 6;//每次换动的数据行数  滚动参数
        string ServerIP = ConfigurationManager.AppSettings["ServerIP"];
        string conString = ConfigurationManager.AppSettings["ConnectionString"];
        string num = "手术结束";

        private int paibanDataBind()
        {

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
                DataTable dt = dbCon.GetDataTable(sql);
                JSdgv.Rows.Clear();
                int temp = 0;
                for (int i = count; i < dt.Rows.Count; i++)
                {
                    JSdgv.Rows.Add();
                    JSdgv.Rows[temp].Cells["id"].Value = dt.Rows[i]["id"] + "";
                    JSdgv.Rows[temp].Cells["patname"].Value = dt.Rows[i]["patname"] + "";
                    JSdgv.Rows[temp].Cells["patdpm"].Value = dt.Rows[i]["patdpm"] + "";
                    JSdgv.Rows[temp].Cells["patbedno"].Value = dt.Rows[i]["patbedno"] + "";
                    JSdgv.Rows[temp].Cells["state"].Value = dt.Rows[i]["state"] + "";
                    temp++;
                    if (temp == PageNum)
                    {
                        break;
                    }
                }
                #endregion
                MaxCount = dt.Rows.Count;
            }
            #region MyRegion
            catch (Exception ex)
            {
                lbError.Visible = true;
                lbError.Text = "数据库链接失败,请检查！！！";
            }
            #endregion
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
                    labelTime.Text = "请  " + JSdgv.Rows[i].Cells["patname"].Value.ToString() + "  的家属准备接病人";
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
                    this.labelTime.Text = DateTime.Now.Year + "年" + DateTime.Now.Month + "月" + DateTime.Now.Day + "日 " + DateTime.Now.Hour + ":" + Minute + "    请耐心等待...";
                    #endregion
                }
            }
            #endregion
            return MaxCount;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
