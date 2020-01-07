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
    public partial class PaiBanForm : Form
    {

        DataTable queryTable = new DataTable();
        public PaiBanForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 确定手术安排
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        private string AppendHL7stringConfig(int id)
        {
            ORM_O01 orm = new ORM_O01();
            var paiban = dal.GetPaiban(id);
            int ostateNum = paiban.Ostate.ToInt32();
            LogHelp.SaveLogHL7("状态" + ostateNum.ToString());
            string SCH_1 = "";
            #region 组装消息头
            SCH_1 = "确定手术安排";
            orm.MSH.MessageType.TriggerEvent.Value = "S18";
            orm.MSH.MessageType.MessageStructure.Value = "SIU_S12";
            //if (ostateNum == 0)
            //{
            //    SCH_1 = "确定手术安排";
            //    orm.MSH.MessageType.TriggerEvent.Value = "S18";
            //    orm.MSH.MessageType.MessageStructure.Value = "SIU_S12";
            //}
            //else
            //{
            //    SCH_1 = "修改手术安排";
            //    orm.MSH.MessageType.TriggerEvent.Value = "S19";
            //    orm.MSH.MessageType.MessageStructure.Value = "SIU_S12";
            //}


            orm.MSH.MessageType.MessageType.Value = "SIU";

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

            int Jieguo = dal.UpdatePaibanConfig(id);//修改成 已排班

            #region SCH|
            String SCH = "SCH||||||^" + SCH_1 + "^^^原因" + "|||||";
            SCH += "^^^" + paiban.Odate.AddDays(-1).ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + "|||||";
            SCH += "SZ1168^^缪兰芬" + "|||";
            SCH += "^^^^^^^^" + paiban.Patdpm.ToString() + "|";
            SCH += "SZ1168^^缪兰芬" + "||||||";
            SCH += paiban.PatID + "\n";
            #endregion

            String PID = paiban.PidInfo;

            String PV1 = paiban.Pv1Info;
            #region RGS|
            String RGS = "RGS|1" + "\n";
            #endregion

            #region AIS|
            String AIS = "AIS|1||";
            AIS += paiban.OperNo+ "^" + paiban.Oname + "|||||||";
            AIS += paiban.Oroom + "^^^" + paiban.Second + "|" + "\n";
            #endregion


            #region 手术医生

            String AIP = "AIP|1||";
            var listUser = dal.GetUser();
            AIP += paiban.OsNo + "^" + paiban.OS + " |主刀医生" + "\n";

            AIP += "AIP|7||";
            string OsNO = string.Empty;
            AIP += OsNO + "^" + paiban.OS1 + "|2^助理医生" + "\n";

            AIP += "AIP|8||";
            AIP += OsNO + "^" + paiban.OS2 + "|2^助理医生" + "\n";

            AIP += "AIP|9||";
            AIP += OsNO + "^" + paiban.OS3 + "|2^助理医生" + "\n";
            #endregion

            #region 护士

            string UserNO = string.Empty;
            var user = listUser.FirstOrDefault(a => a.User_name == paiban.SN1);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|2||";
            AIP += UserNO + "^" + paiban.SN1 + "|4^洗手护士" + "\n";

            UserNO = string.Empty;
            user = listUser.FirstOrDefault(a => a.User_name == paiban.SN2);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|3||";
            AIP += UserNO + "^" + paiban.SN2 + "|4^洗手护士" + "\n";

            UserNO = string.Empty;
            user = listUser.FirstOrDefault(a => a.User_name == paiban.ON1);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|4||";
            AIP += UserNO + "^" + paiban.ON1 + "|5^巡回护士" + "\n";
            UserNO = string.Empty;
            user = listUser.FirstOrDefault(a => a.User_name == paiban.ON2);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|5||";
            AIP += UserNO + "^" + paiban.ON2 + "|5^巡回护士" + "\n";


            AIP += "AIP|6||";
            AIP += UserNO + "^" + "" + "|5^巡回护士" + "\n";
            #endregion

            AIP += "AIP|10|||6^体外循环师" + "\n";

            #region 麻醉医生

            UserNO = string.Empty;
            user = listUser.FirstOrDefault(a => a.User_name == paiban.AP1);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|11||";
            AIP += UserNO + "^" + paiban.AP1 + "|3^麻醉师" + "\n";

            UserNO = string.Empty;
            user = listUser.FirstOrDefault(a => a.User_name == paiban.AP2);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|12||";
            AIP += UserNO + "^" + paiban.AP2 + "|7^助理麻醉师" + "\n";

            UserNO = string.Empty;
            user = listUser.FirstOrDefault(a => a.User_name == paiban.AP3);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|13||";
            AIP += UserNO + "^" + paiban.AP3 + "|7^助理麻醉师" + "\n";
            #endregion


            #region 转换消息对象为字符串
            String hl7Message = SCH + PID + PV1 + RGS + AIS + AIP;
            NHapi.Base.Parser.PipeParser parser = new NHapi.Base.Parser.PipeParser();
            string message = parser.Encode(orm) + hl7Message;
            return message;
            #endregion

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

                    string message = AppendHL7stringConfig(model.ID);

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

                string message = AppendHL7stringConfig(item.ID);

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
