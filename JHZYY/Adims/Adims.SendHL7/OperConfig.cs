using adims_BLL;
using adims_DAL;
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
    public partial class OperConfig : Form
    {

        DataTable queryTable = new DataTable();
        public OperConfig()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvOper.SelectedRows.Count == 1)
            {
                #region 发送HL7
                string HL7IPaddress = ConfigurationManager.AppSettings["HL7IPaddress"];

                string message = AppendHL7stringOperConfig(dgvOper.SelectedRows[0].Cells["patid"].Value.ToString(),
                    dgvOper.SelectedRows[0].Cells["userNo"].Value.ToString(),
                    dgvOper.SelectedRows[0].Cells["ap1"].Value.ToString());
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
      
        adims_DAL.SendHl7Dal dal = new adims_DAL.SendHl7Dal();
        /// <summary>
        /// 手术消息反馈
        /// </summary>
        /// <returns></returns>
        private string AppendHL7stringOperConfig(string patID, string userNo, string userName)
        {
            ORM_O01 orm = new ORM_O01();
            #region 组装消息头
            orm.MSH.MessageType.MessageType.Value = "SIU";
            orm.MSH.MessageType.TriggerEvent.Value = "S21";
            orm.MSH.MessageType.MessageStructure.Value = "SIU_S12";
            orm.MSH.FieldSeparator.Value = MessageConstant.FieldSeparator;
            orm.MSH.SendingApplication.NamespaceID.Value = "SSMZ1";
            orm.MSH.SendingFacility.NamespaceID.Value = "SSMZ1";
            orm.MSH.ReceivingApplication.NamespaceID.Value = "MediII";
            orm.MSH.ReceivingFacility.NamespaceID.Value = "MediII";
            orm.MSH.EncodingCharacters.Value = MessageConstant.EncodingCharacters;
            orm.MSH.VersionID.VersionID.Value = MessageConstant.VersionID;
            orm.MSH.DateTimeOfMessage.TimeOfAnEvent.SetLongDateWithSecond(DateTime.Now);
            orm.MSH.MessageControlID.Value = MediII.Common.GUIDHelper.NewGUID();
            orm.MSH.ProcessingID.ProcessingID.Value = MessageConstant.ProcessingID;

            #endregion

            DataTable dtResult = dal.GetPaibanAndMZJLD(patID);
            DataRow dr = dtResult.Rows[0];

            #region SCH|
            String SCH = "SCH||1||||";
            string mzff = dr["Amethod"].ToString();
            DataTable dtMzff = dal.GetMzffID(mzff);
            string mzff_No = "00";
            if (dtMzff.Rows.Count > 0)
            {
                mzff_No = dtMzff.Rows[0][0].ToString();
            }
            SCH += "SSMZ" + "^^^" + mzff_No + "^" + mzff + "|||||";
            SCH += "^^^" + Convert.ToDateTime(dr["sskssj"]).ToString("yyyyMMddHHmmss") + "^" + Convert.ToDateTime(dr["ssjssj"]).ToString("yyyyMMddHHmmss");
            SCH += "~^^^" + Convert.ToDateTime(dr["otime"]).ToString("yyyyMMddHHmmss") + "^" + DateTime.Now.ToString("yyyyMMddHHmmss") + "|||||";
            SCH += userNo + "^^" + userName + "||||";
            SCH += userNo + "^^" + userName + "||||||";
            SCH += dr["patid"].ToString() + "\n";
            #endregion

            #region NTE|
            String NTE = "NTE|1|||~" + "\n";
            #endregion


            string PID = dr["PidInfo"].ToString();
            string PV1 = dr["Pv1Info"].ToString();


            string RGS = "RGS|1" + "\n";

            #region AIS|
            String AIS = "AIS|1||";
            string OperName = dr["Oname"].ToString();
            DataTable dtSSSS = dal.GetOperNo(OperName);
            string OperCode = "00";
            if (dtSSSS.Rows.Count > 0)
            {
                OperCode = dtSSSS.Rows[0]["OperCode"].ToString();
            }
            AIS += OperCode + "^" + OperName + "||||||";
            AIS += dr["GL"].ToString() + "|";
            AIS += dr["Oroom"].ToString() + "^" + dr["Second"].ToString() + "\n";
            #endregion

            #region 手术医生

            String AIP = "AIP|1||";
            DataTable dtOsNo = dal.GetShoushuYishengNo(dr["OS"].ToString());
            string OsNO = "";
            if (dtOsNo.Rows.Count > 0)
            {
                OsNO = dtOsNo.Rows[0]["nameNo"].ToString();
            }
            AIP += OsNO + "^" + dr["OS"].ToString() + "|主刀医生" + "\n";

            AIP += "AIP|7||";
            dtOsNo = dal.GetShoushuYishengNo(dr["OA1"].ToString());
            OsNO = "";
            if (dtOsNo.Rows.Count > 0)
            {
                OsNO = dtOsNo.Rows[0]["nameNo"].ToString();
            }
            AIP += OsNO + "^" + dr["OA1"].ToString() + "|2^助理医生" + "\n";

            AIP += "AIP|8||";
            dtOsNo = dal.GetShoushuYishengNo(dr["OA2"].ToString());
            OsNO = "";
            if (dtOsNo.Rows.Count > 0)
            {
                OsNO = dtOsNo.Rows[0]["nameNo"].ToString();
            }
            AIP += OsNO + "^" + dr["OA2"].ToString() + "|2^助理医生" + "\n";

            AIP += "AIP|9||";
            dtOsNo = dal.GetShoushuYishengNo(dr["OA3"].ToString());
            OsNO = "";
            if (dtOsNo.Rows.Count > 0)
            {
                OsNO = dtOsNo.Rows[0]["nameNo"].ToString();
            }
            AIP += OsNO + "^" + dr["OA3"].ToString() + "|2^助理医生" + "\n";
            #endregion

            #region 护士
            DataTable dt = dal.GetUserNoByName(dr["SN1"].ToString());
            string UserNO = string.Empty;
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|2||";
            AIP += UserNO + "^" + dr["SN1"].ToString() + "|4^洗手护士" + "\n";
            dt = dal.GetUserNoByName(dr["SN2"].ToString());
            UserNO = string.Empty;
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|3||";
            AIP += UserNO + "^" + dr["SN2"].ToString() + "|4^洗手护士" + "\n";
            dt = dal.GetUserNoByName(dr["ON1"].ToString());
            UserNO = string.Empty;
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|4||";
            AIP += UserNO + "^" + dr["ON1"].ToString() + "|5^巡回护士" + "\n";
            dt = dal.GetUserNoByName(dr["ON2"].ToString());
            UserNO = string.Empty;
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|5||";
            AIP += UserNO + "^" + dr["ON2"].ToString() + "|5^巡回护士" + "\n";

            dt = dal.GetUserNoByName(dr["ON3"].ToString());
            UserNO = string.Empty;
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|6||";
            AIP += UserNO + "^" + dr["ON3"].ToString() + "|5^巡回护士" + "\n";
            #endregion

            AIP += "AIP|10|||6^体外循环师" + "\n";

            #region 麻醉医生
            dt = dal.GetUserNoByName(dr["AP1"].ToString());
            UserNO = string.Empty;
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|11||";
            AIP += UserNO + "^" + dr["AP1"].ToString() + "|3^麻醉师" + "\n";
            dt = dal.GetUserNoByName(dr["AP2"].ToString());
            UserNO = string.Empty;
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|12||";
            AIP += UserNO + "^" + dr["AP2"].ToString() + "|7^助理麻醉师" + "\n";
            dt = dal.GetUserNoByName(dr["AP3"].ToString());
            UserNO = string.Empty;
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|13||";
            AIP += UserNO + "^" + dr["AP3"].ToString() + "|7^助理麻醉师" + "\n";
            #endregion



            #region 转换消息对象为字符串
            String hl7Message = SCH + NTE + PID + PV1 + RGS + AIS + AIP;
            NHapi.Base.Parser.PipeParser parser = new NHapi.Base.Parser.PipeParser();
            string message = parser.Encode(orm) + hl7Message;
            return message;
            #endregion

        }
      

        private void btnAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dgvOper.Rows)
            {

           
                #region 发送HL7
                string HL7IPaddress = ConfigurationManager.AppSettings["HL7IPaddress"];

                string message = AppendHL7stringOperConfig(item.Cells["patid"].Value.ToString(),
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
            var res = dal.GetOperByDate(dtOdate.Value.ToString("yyyy-MM-dd"));
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            BindDatasource();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
          
        }
    }
}
