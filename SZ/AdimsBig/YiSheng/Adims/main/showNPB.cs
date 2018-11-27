using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;
using adims_DAL;
using System.Configuration;

using System.Net.NetworkInformation;
using System.Threading;


using System.Data.SqlClient;

using WindowsFormsControlLibrary5;

namespace main
{
    public partial class showNPB : Form
    {
        public showNPB()
        {
            InitializeComponent();
        }
        private static string connStr = ConfigurationManager.AppSettings["ConnectionString"];
        

        DB2help db2 = new DB2help();
        AdimsController bll = new AdimsController();
        AdimsProvider DAL = new AdimsProvider();
        bool isInNet = false;
        /// <summary>
        /// 医师试图
        /// </summary>
        int MaxCount = 0;//最大数据行术 滚动参数
        int count = 0;//数据行数  滚动参数
        int Num = 12;//每次换动的数据行数  滚动参数
        int temp = 0;
        //int pagCount;
        //int pagDQcount=1;
        private int DataBind()
        {
            //手动绑定数据
            SqlConnection myConn = new SqlConnection(connStr);
            //myConn = new SqlConnection("Server=.;DataBase=HeYiAMIS;Uid=sa;PassWord=sa");
            try
            {
                #region 循环获取绑定数据
                string dtime = DateTime.Now.ToString("yyyy-MM-dd");
                string nowdtie = DateTime.Now.AddDays(+1).ToString("yyyy-MM-dd");
                myConn.Open();
                string sql = "SELECT  ROW_NUMBER()over(order by odate) as rd, Oroom + '/' + Second + '台' AS 手术间1, Patname +"
                        +"'(' + Patsex + '/' + CAST(Patage AS nvarchar) + '岁' + ')' AS "
                        +"patname,Patbedno,substring(CONVERT(varchar, Odate , 23 ),6,5)"
                        +"as Odate, Oname, Amethod, AP1+'、'+AP2+'、'+AP3 as ap1, ON1 + '/' + ON2 AS qxhs, "
                        + "OS,ostate,SN1 + '/' + SN2 AS xhhs FROM  Adims_OTypesetting where ostate='1' and operaddress='010601' and CONVERT(nvarchar, Odate , 23 ) "
                        +"between '" + dtime + "' and '" + nowdtie + "' order by oroom,second  asc";
                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter(sql, myConn);
                sda.Fill(ds, "Adims_OTypesetting");
                YSdgv.Rows.Clear();
                temp = 0;
                for (int i = count; i < ds.Tables[0].Rows.Count; i++)
                {
                    YSdgv.Rows.Add();
                    YSdgv.Rows[temp].Cells["rd"].Value = temp + 1;
                    YSdgv.Rows[temp].Cells["手术间1"].Value = ds.Tables[0].Rows[i]["手术间1"] + "";
                    YSdgv.Rows[temp].Cells["patname"].Value = ds.Tables[0].Rows[i]["patname"] + "";
                    YSdgv.Rows[temp].Cells["odate"].Value = ds.Tables[0].Rows[i]["odate"] + "";
                    YSdgv.Rows[temp].Cells["patbedno"].Value = ds.Tables[0].Rows[i]["patbedno"] + "";
                    YSdgv.Rows[temp].Cells["patname"].Value = ds.Tables[0].Rows[i]["patname"] + "";
                    YSdgv.Rows[temp].Cells["oname"].Value = ds.Tables[0].Rows[i]["oname"] + "";
                    YSdgv.Rows[temp].Cells["Amethod"].Value = ds.Tables[0].Rows[i]["Amethod"] + "";
                    YSdgv.Rows[temp].Cells["AP1"].Value = ds.Tables[0].Rows[i]["AP1"] + "";
                    YSdgv.Rows[temp].Cells["os"].Value = ds.Tables[0].Rows[i]["os"] + "";
                    YSdgv.Rows[temp].Cells["qxhs"].Value = ds.Tables[0].Rows[i]["qxhs"] + "";
                    YSdgv.Rows[temp].Cells["xhhs"].Value = ds.Tables[0].Rows[i]["xhhs"] + "";
                    
                    temp++;
                    if (temp == Num)
                    {
                        break;
                    }
                }
                #endregion
                MaxCount = ds.Tables[0].Rows.Count;
            }
            #region catch
            catch (Exception ex)
            {
                throw new Exception("连接数据库失败！错误原因：" + ex.Message);
            }
            #endregion
            #region 简缩
            for (int i = 0; i < YSdgv.Rows.Count; i++)
            {
                if (YSdgv.Rows[i].Cells["Amethod"].Value.ToString() != "" || YSdgv.Rows[i].Cells["Amethod"].Value.ToString() != null || YSdgv.Rows[i].Cells["Amethod"].Value.ToString() != " ")
                {
                    string s = YSdgv.Rows[i].Cells["Amethod"].Value.ToString();
                    if (s == "腰麻硬膜外联合阻滞")
                    {
                        YSdgv.Rows[i].Cells["Amethod"].Value = "腰硬联合";
                    }
                    if (s == "全身麻醉")
                    {
                        YSdgv.Rows[i].Cells["Amethod"].Value = "全麻";
                    }
                    if (s == "局部浸润麻醉")
                    {
                        YSdgv.Rows[i].Cells["Amethod"].Value = "局麻";
                    }
                }
            }
            #endregion

            return MaxCount;
        }
        /// <summary>
        /// ping 连通服务器
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="TimeOut"></param>
        /// <returns></returns>
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
        private void showNPB_Load(object sender, EventArgs e)
        {           
            #region 行高
            DataGridViewRow row = this.YSdgv.RowTemplate;
            row.Height = 50; //每行的高度
            YSdgv.Columns[3].CellTemplate.Style.WrapMode = DataGridViewTriState.True;
            YSdgv.Columns[6].CellTemplate.Style.WrapMode = DataGridViewTriState.True;
            YSdgv.Columns[7].CellTemplate.Style.WrapMode = DataGridViewTriState.True;
            YSdgv.Columns[8].CellTemplate.Style.WrapMode = DataGridViewTriState.True;
            YSdgv.Columns[9].CellTemplate.Style.WrapMode = DataGridViewTriState.True;           
            this.label1.Text = DateTime.Now.Year + "年" + DateTime.Now.Month + "月" + DateTime.Now.Day + "日 " + DateTime.Now.ToString("HH:mm");
           
            #endregion
           
            timerInNet.Start();
            if (isInNet)
            
            {
                this.lbFalse.Visible = false;
                timerInNet.Stop();
                if (count >= MaxCount)
                {
                    count = 0;
                }
                DataBind();

                if (count >= MaxCount)
                {
                    count = 0;
                }
                else
                {
                    count = count + Num;
                }
                this.timer1.Start();//定时器启动
                this.timer1.Interval = 15000;
            }
            else
            {
                this.lbFalse.Visible = true;
            }

           

        }

        private void timerInNet_Tick(object sender, EventArgs e)
        {  
         
            isInNet = PingHost("10.0.100.87");
            if (isInNet)
            {
                showNPB_Load(sender, e);
            }
            
        }

       
    }
}
