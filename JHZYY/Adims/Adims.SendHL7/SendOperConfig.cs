using adims_BLL;
using adims_DAL;
using adims_MODEL;
using Adims_Utility;
using MediII.Common;
using MODEL;
using NHapi.Model.V24.Message;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adims.SendHL7
{
    public partial class SendOperConfig : Form
    {

        DataTable queryTable = new DataTable();
        public SendOperConfig()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvOper.SelectedRows.Count == 1)
            {
                #region 发送HL7
                string HL7IPaddress = ConfigurationManager.AppSettings["HL7IPaddress"];

                List<QueryMzjldDto> list = (List<QueryMzjldDto>)dgvOper.DataSource;
                QueryMzjldDto model = list[dgvOper.CurrentRow.Index];
                string message = Hl7Bll.AppendHL7stringOperConfig(model.Patid,
                    "0001",
                    "admin");
                LogHelp.SaveLogHL7(message);
                // if (UserFunction.PingHost(HL7IPaddress))
                if (true)
                {
                    if (message.Length > 0)
                    {
                        string HL7port = ConfigurationManager.AppSettings["HL7port"];
                        SenderRoutingLib.SocketSender send = new SenderRoutingLib.SocketSender();
                        object objResult;
                        int iResult = 0;
                        int count = 1;
                        if (count < 10)
                        {
                            new System.Threading.Thread(o =>
                            {
                                for (int i = 0; i < count; i++)
                                {
                                    objResult = send.Send(message, HL7IPaddress, HL7port.ToInt32());
                                    string ack = objResult == null ? string.Empty : objResult.ToString();
                                    if (ack.Contains("AA"))
                                    {
                                        iResult++;
                                        LogHelp.SaveLogHL7(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                    }
                                    else
                                    {
                                        iResult++;
                                        LogHelp.SaveLogHL7(string.Format("\r\n消息处理失败原因：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                    }
                                }
                            }).Start();
                        }

                    }
                    else
                    {
                        LogHelp.SaveLogHL7(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " IP地址或端口错误");
                    }

                }
                #endregion

            }

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

        }

        adims_DAL.SqlSugarDal dal = new adims_DAL.SqlSugarDal();
       

        private void btnAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dgvOper.Rows)
            {


                #region 发送HL7
                string HL7IPaddress = ConfigurationManager.AppSettings["HL7IPaddress"];

                string message = Hl7Bll.AppendHL7stringOperConfig(item.Cells["patid"].Value.ToString(),
                  item.Cells["userNo"].Value.ToString(),
                  item.Cells["ap1"].Value.ToString());
                LogHelp.SaveLogHL7(message);
                // if (UserFunction.PingHost(HL7IPaddress))
                if (true)
                {
                    if (message.Length > 0)
                    {
                        string HL7port = ConfigurationManager.AppSettings["HL7port"];
                        SenderRoutingLib.SocketSender send = new SenderRoutingLib.SocketSender();
                        object objResult;
                        int iResult = 0;
                        int count = 1;
                        if (count < 10)
                        {
                            new System.Threading.Thread(o =>
                            {
                                for (int i = 0; i < count; i++)
                                {
                                    objResult = send.Send(message, HL7IPaddress, HL7port.ToInt32());
                                    string ack = objResult == null ? string.Empty : objResult.ToString();
                                    if (ack.Contains("AA"))
                                    {
                                        iResult++;
                                        LogHelp.SaveLogHL7(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                    }
                                    else
                                    {
                                        iResult++;
                                        LogHelp.SaveLogHL7(string.Format("\r\n消息处理失败原因：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                    }
                                }
                            }).Start();
                        }

                    }
                    else
                    {
                        LogHelp.SaveLogHL7(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " IP地址或端口错误");
                    }

                }
                #endregion

            }


        }

        private void DataView_Load(object sender, EventArgs e)
        {
            BindDatasource();

        }

        private void BindDatasource()
        {
            var res = dal.GetOperByDate(dtOdate.Value.Date);
            dgvOper.DataSource = res;
            if (res != null)
            {
                tbCount.Text = res.Count.ToString();
            }
            else
            {
                tbCount.Text = "0";
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            BindDatasource();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
