using adims_DAL;
using MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InsertSysLogTool
{
    public partial class DataView : Form
    {

        DataTable queryTable = new DataTable();
        public DataView()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BindDatasource();
        }
        // 1,声明一个委托
        public delegate void UpDateInfo(string strinfo);
        public delegate void UpdateStatus(bool isEnable);
        //2,定义一个函数,作用就是在函数中使用委托对属性值进行设置

        private void UpdateStatusFuction(bool isEnable)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new UpdateStatus(UpdateStatusFuction), new object[] { isEnable });
                Thread.Sleep(5);
            }
            else
            {
                //btnInsert.Enabled = isEnable;
                btnQUERY.Enabled = isEnable;
            }
        }
        private void UpDateText(string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new UpDateInfo(UpDateText), new object[] { text });
                Thread.Sleep(5);
            }
            else
            {
                richTextBox1.AppendText(text + "\n");
            }
        }
        //3,在线程中调用UpDateText函数

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Thread th = new Thread(new ThreadStart(DataFind));
                th.Start();
                th.IsBackground = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void DataFind()
        {
            try
            {
                int i = 0;
                UpdateStatusFuction(false);

                foreach (DataRow dr in queryTable.Rows)
                {
                    if (dr["keyId1"].ToString() == "")
                    {
                        Guid useId = new Guid();
                        useId = new Guid(dr["UseID"].ToString());


                        SysLog sysLog = new SysLog();
                        sysLog.IsManualAdd = 1;
                        sysLog.Barcode = dr["Barcode"].ToString();
                        sysLog.LogID = Guid.NewGuid();
                        sysLog.Operator = new Guid(dr["OPID"].ToString());
                        sysLog.LogLevel = 1;
                        sysLog.LogType = 8;
                        sysLog.LogTime = Convert.ToDateTime(dr["UseDate"]);
                        sysLog.KeyID1 = new Guid(dr["StorageID"].ToString());
                        sysLog.KeyID2 = new Guid(dr["StorageID"].ToString());
                        sysLog.Content = dr["ContentMerge"].ToString();
                        sysLog.FlowOrgID = new Guid(dr["OrgID"].ToString());

                       // OperDicDal.InsertSyslog(sysLog);
                        LogHelp.WriteLog(string.Format("插入包条码为 {0}，包ID为 {1} 的使用日志记录", sysLog.Barcode, sysLog.KeyID1));
                        UpDateText(string.Format("插入包条码为 {0}，包ID为 {1} 的使用日志记录", sysLog.Barcode, sysLog.KeyID1));
                        //break;
                        i++;
                    }



                }
                LogHelp.WriteLog(string.Format("本次插入{0}条记录", i));

                MessageBox.Show(string.Format("插入使用日志：{0}条记录", i));


                UpdateStatusFuction(true);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                UpdateStatusFuction(true);
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            try
            {
                OperDicDal.DelOperDic(DateTime.Now);
                DB2help db2 = new DB2help();
                var dt = db2.GetHisOperNameAll("");
                foreach (DataRow dr in dt.Rows)
                {
                    //LogHelp.WriteLog(dr["ITEMNAME"].ToString(),
                    //   dr["ITEMID"].ToString(),
                    //   dr["SSDJ"].ToString(),
                    //   dr["SSQK"].ToString(),
                    //   dr["INPUTSTR"].ToString());
                    //ITEMID 编码, ITEMNAME 手术名称,SSDJ 手术等级,SSQK 手术切口,INPUTSTR 
                    var res = OperDicDal.InsertOperDic(
                          dr["ITEMNAME"].ToString(),
                          dr["ITEMID"].ToString(),
                          dr["SSDJ"].ToString(),
                          dr["SSQK"].ToString(),
                          dr["INPUTSTR"].ToString()
                          );

                 
                }

                BindDatasource();
            }
            catch (Exception ex)
            {
                LogHelp.WriteLog(ex.ToString());
                MessageBox.Show(ex.Message);
            }

        }

        private void DataView_Load(object sender, EventArgs e)
        {
            BindDatasource();

        }

        private void BindDatasource()
        {
            var res = OperDicDal.GetOperDic();
            dgvOper.DataSource = res;
            if (res != null)
            {
                tbCount.Text = res.Rows.Count.ToString();
            }
            else
            {
                tbCount.Text = "0";
            }
        }
    }
}
