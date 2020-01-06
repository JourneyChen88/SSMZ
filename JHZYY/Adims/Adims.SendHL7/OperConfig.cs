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

        adims_DAL.SqlSugarDal dal = new adims_DAL.SqlSugarDal();
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

            var mzjldDto = dal.GetPaibanAndMzjld(patID);

            #region SCH|
            String SCH = "SCH||1||||";
            string mzff = mzjldDto.Amethod;
            //DataTable dtMzff = dal.GetMzffID(mzff);
            string mzff_No = "00";

            SCH += "SSMZ" + "^^^" + mzff_No + "^" + mzff + "|||||";
            SCH += "^^^" + mzjldDto.sskssj.Value.ToString("yyyyMMddHHmmss") + "^" + mzjldDto.ssjssj.Value.ToString("yyyyMMddHHmmss");
            SCH += "~^^^" + mzjldDto.Otime.Value.ToString("yyyyMMddHHmmss") + "^" + DateTime.Now.ToString("yyyyMMddHHmmss") + "|||||";
            SCH += userNo + "^^" + userName + "||||";
            SCH += userNo + "^^" + userName + "||||||";
            SCH += mzjldDto.Patid + "\n";
            #endregion

            #region NTE|
            String NTE = "NTE|1|||~" + "\n";
            #endregion


            string PID = mzjldDto.PidInfo;
            string PV1 = mzjldDto.Pv1Info;


            string RGS = "RGS|1" + "\n";

            #region AIS|
            String AIS = "AIS|1||";
            string OperName = mzjldDto.Oname;
        
            AIS += mzjldDto.OperNo + "^" + mzjldDto.Oname + "|||||||";
            AIS += mzjldDto.Oroom + "^" + mzjldDto.Second+ "\n";
            #endregion

            #region 手术医生

            String AIP = "AIP|1||";
            var listUser = dal.GetUser();
            AIP += mzjldDto.OsNo + "^" + mzjldDto.OS + " |主刀医生" + "\n";

            AIP += "AIP|7||";
            string OsNO = string.Empty;
            AIP += OsNO + "^" + mzjldDto.OS1 + "|2^助理医生" + "\n";

            AIP += "AIP|8||";
            AIP += OsNO + "^" + mzjldDto.OS2 + "|2^助理医生" + "\n";

            AIP += "AIP|9||";            
            AIP += OsNO + "^" + mzjldDto.OS3 + "|2^助理医生" + "\n";
            #endregion

            #region 护士

            string UserNO = string.Empty;
            var user = listUser.FirstOrDefault(a=>a.User_name==mzjldDto.SN1);
            if (user!=null) UserNO = user.Uid;
            AIP += "AIP|2||";
            AIP += UserNO + "^" + mzjldDto.SN1 + "|4^洗手护士" + "\n";
         
            UserNO = string.Empty;
            user = listUser.FirstOrDefault(a => a.User_name == mzjldDto.SN2);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|3||";
            AIP += UserNO + "^" + mzjldDto.SN2+ "|4^洗手护士" + "\n";

            UserNO = string.Empty;
            user = listUser.FirstOrDefault(a => a.User_name == mzjldDto.ON1);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|4||";
            AIP += UserNO + "^" + mzjldDto.ON1 + "|5^巡回护士" + "\n";
            UserNO = string.Empty;
            user = listUser.FirstOrDefault(a => a.User_name == mzjldDto.ON2);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|5||";
            AIP += UserNO + "^" + mzjldDto.ON2 + "|5^巡回护士" + "\n";

           
            AIP += "AIP|6||";
            AIP += UserNO + "^" + ""+ "|5^巡回护士" + "\n";
            #endregion

            AIP += "AIP|10|||6^体外循环师" + "\n";

            #region 麻醉医生

            UserNO = string.Empty;
            user = listUser.FirstOrDefault(a => a.User_name == mzjldDto.AP1);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|11||";
            AIP += UserNO + "^" + mzjldDto.AP1 + "|3^麻醉师" + "\n";

            UserNO = string.Empty;
            user = listUser.FirstOrDefault(a => a.User_name == mzjldDto.AP2);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|12||";
            AIP += UserNO + "^" + mzjldDto.AP2 + "|7^助理麻醉师" + "\n";

            UserNO = string.Empty;
            user = listUser.FirstOrDefault(a => a.User_name == mzjldDto.AP3);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|13||";
            AIP += UserNO + "^" + mzjldDto.AP3 + "|7^助理麻醉师" + "\n";
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
