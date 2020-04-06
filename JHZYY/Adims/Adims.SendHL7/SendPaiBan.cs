using adims_BLL;
using adims_DAL;
using adims_MODEL;
using adims_MODEL.Dtos;
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
    public partial class SendPaiBan : Form
    {
        
        DataTable queryTable = new DataTable();
        public SendPaiBan()
        {
            InitializeComponent();
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvOTypesetting.SelectedCells.Count == 1)
                {
                    List<PaibanDto> list = (List<PaibanDto>)dgvOTypesetting.DataSource;
                    PaibanDto model = list[dgvOTypesetting.CurrentRow.Index];
                    string patid = model.PatID; 
                    string Oroom = model.Oroom;
                    string Osecond = model.Second;
                    //string patid = dgvOTypesetting.CurrentRow.Cells["patid"].Value.ToString();
                    //string Oroom = dgvOTypesetting.CurrentRow.Cells["Oroom"].Value.ToString();
                    //string Osecond = dgvOTypesetting.CurrentRow.Cells["second"].Value.ToString();
                    if (Oroom.IsNullOrEmpty() && Osecond.IsNullOrEmpty())
                    {
                        MessageBox.Show("手术间和台次不能都为空");
                        return;
                    }
                  
                    string message = Hl7Bll.AppendHL7stringConfig(model.ID, "卢赛芳", "486");

                    LogHelp.SaveLogHL7(message);
                    string HL7IPaddress = ConfigurationManager.AppSettings["HL7IPaddress"];
                    //if (message.Length > 0 && UserFunction.PingHost(HL7IPaddress))
                    if (true)
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
                                    objResult = send.Send(message, HL7IPaddress, Convert.ToInt32(HL7port));
                                    string ack = objResult == null ? string.Empty : objResult.ToString();
                                    if (ack.Contains("AA"))
                                    {
                                        iResult++;
                                        LogHelp.SaveLogHL7(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                        // SetText(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                    }
                                    else
                                    {
                                        iResult++;
                                        LogHelp.SaveLogHL7(string.Format("\r\n消息处理失败原因：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                        // SetText(string.Format("\r\n消息处理失败原因：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                    }
                                }
                            }).Start();
                        }
                        else
                        {
                            for (int j = 0; j < 10; j++)
                            {
                                new System.Threading.Thread(o =>
                                {
                                    for (int i = 0; i < count / 10; i++)
                                    {
                                        objResult = send.Send(message, HL7IPaddress, Convert.ToInt32(HL7port));
                                        string ack = objResult == null ? string.Empty : objResult.ToString();
                                        if (ack.Contains("AA"))
                                        {
                                            iResult++;
                                            // SetText(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                        }
                                        else
                                        {
                                            iResult++;
                                            //SetText(string.Format("\r\n发送失败，错误信息：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                        }
                                    }
                                }).Start();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("IP地址或端口错误");
                    }
                }
                else
                    MessageBox.Show("请选择一位病人");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
        //3,在线程中调用UpDateText函数


        adims_DAL.SqlSugarDal dal = new adims_DAL.SqlSugarDal();
      

        private void DataView_Load(object sender, EventArgs e)
        {
            BindPaibanInfo();

        }

        private void BindPaibanInfo()
        {
            var dt = dal.GetPaiBanByOdate(dtOdate.Value.Date);
            dgvOTypesetting.DataSource = dt;
            if (dt != null)
            {
                tbCount.Text = dt.Count.ToString();
            }
            else
            {
                tbCount.Text = "0";
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)

        { 
            BindPaibanInfo();
           
        }
        
        private void btnSync_Click(object sender, EventArgs e)
        {
            List<PaibanDto> listPaiban = (List<PaibanDto>)dgvOTypesetting.DataSource;
            foreach (var item in listPaiban)
            {

                string patid = item.PatID;
                string Oroom = item.Oroom;
                string Osecond = item.Second;
                if (Oroom.IsNullOrEmpty() && Osecond.IsNullOrEmpty())
                {
                    MessageBox.Show("手术间和台次不能都为空");
                    return;
                }

                string message = Hl7Bll.AppendHL7stringConfig(item.ID, "卢赛芳", "486");

                LogHelp.SaveLogHL7(message);
                string HL7IPaddress = ConfigurationManager.AppSettings["HL7IPaddress"];
                //if (message.Length > 0 && UserFunction.PingHost(HL7IPaddress))
                if (true)
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
                                objResult = send.Send(message, HL7IPaddress, Convert.ToInt32(HL7port));
                                string ack = objResult == null ? string.Empty : objResult.ToString();
                                if (ack.Contains("AA"))
                                {
                                    iResult++;
                                    LogHelp.SaveLogHL7(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                        // SetText(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                    }
                                else
                                {
                                    iResult++;
                                    LogHelp.SaveLogHL7(string.Format("\r\n消息处理失败原因：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                        // SetText(string.Format("\r\n消息处理失败原因：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                    }
                            }
                        }).Start();
                    }
                    else
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            new System.Threading.Thread(o =>
                            {
                                for (int i = 0; i < count / 10; i++)
                                {
                                    objResult = send.Send(message, HL7IPaddress, Convert.ToInt32(HL7port));
                                    string ack = objResult == null ? string.Empty : objResult.ToString();
                                    if (ack.Contains("AA"))
                                    {
                                        iResult++;
                                            // SetText(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                        }
                                    else
                                    {
                                        iResult++;
                                            //SetText(string.Format("\r\n发送失败，错误信息：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                        }
                                }
                            }).Start();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("IP地址或端口错误");
                }
            }
        }
    }
}
