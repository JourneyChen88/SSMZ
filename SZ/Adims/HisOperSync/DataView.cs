using adims_DAL;
using Adims_Utility;
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
        InfeconDal _InfeconDal = new InfeconDal();

        DataTable queryTable = new DataTable();
        public DataView()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            queryTable = _InfeconDal.GetFlowUseList(dtStart.Value.Date, dtEnd.Value.Date.AddDays(1));
            dataGridView.DataSource = queryTable;
            //dataGridView2.DataSource = _InfeconDal.GetSysLogByTime(dtStart.Value.Date, dtEnd.Value.Date.AddDays(1));
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
                btnInsert.Enabled = isEnable;
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

                        _InfeconDal.InsertSyslog(sysLog);
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

        }
    }
}
