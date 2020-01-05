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
        private string AppendHL7stringConfig(string patid)
        {
            ORM_O01 orm = new ORM_O01();
            DataTable dtResult = dal.GetPaiban(patid);
            DataRow dr = dtResult.Rows[0];
            int ostateNum = dr["Ostate"].ToInt32();
            LogHelp.SaveLogHL7("状态" + dr["Ostate"].ToString());
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

            int Jieguo = dal.UpdatePaibanConfig(patid);//修改成 已排班

            #region SCH|
            String SCH = "SCH||||||^" + SCH_1 + "^^^原因" + "|||||";
            SCH += "^^^" + Convert.ToDateTime(dr["odate"]).AddDays(-1).ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + "|||||";
            SCH += "SZ1168^^缪兰芬" + "|||";
            SCH += "^^^^^^^^" + dr["patdpm"].ToString() + "|";
            SCH += "SZ1168^^缪兰芬" + "||||||";
            SCH += dr["patid"].ToString() + "\n";
            #endregion

            String PID = dr["PidInfo"].ToString();

            String PV1 = dr["Pv1Info"].ToString();
            #region RGS|
            String RGS = "RGS|1" + "\n";
            #endregion

            #region AIS|
            String AIS = "AIS|1||";
            AIS += dr["OperNo"].ToString() + "^" + dr["Oname"].ToString() + "|||||||";
            AIS += dr["Oroom"].ToString() + "^^^" + dr["Second"].ToString() + "|" + "\n";
            #endregion



            #region 手术医生
            String AIP = "AIP|1||";
            AIP += dr["OsNo"].ToString() + "^" + dr["OS"].ToString() + "|主刀医生" + "\n";
            #endregion

            #region 护士
            DataTable dt = dal.GetUserNoByName(dr["SN1"].ToString());
            string UserNO = " ";
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|2||";
            AIP += UserNO + "^" + dr["SN1"].ToString() + "|4^洗手护士" + "\n";

            dt = dal.GetUserNoByName(dr["SN2"].ToString());
            UserNO = " ";
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|3||";
            AIP += UserNO + "^" + dr["SN2"].ToString() + "|4^洗手护士" + "\n";

            dt = dal.GetUserNoByName(dr["ON1"].ToString());
            UserNO = " ";
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|4||";
            AIP += UserNO + "^" + dr["ON1"].ToString() + "|5^巡回护士" + "\n";

            dt = dal.GetUserNoByName(dr["ON2"].ToString());
            UserNO = " ";
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|5||";
            AIP += UserNO + "^" + dr["ON2"].ToString() + "|5^巡回护士" + "\n";

            dt = dal.GetUserNoByName(dr["ON3"].ToString());
            UserNO = " ";
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|6||";
            AIP += UserNO + "^" + dr["ON3"].ToString() + "|5^巡回护士" + "\n";
            #endregion

            #region 手术助手
            AIP += "AIP|7||";
            AIP += dr["OA1No"].ToString() + "^" + dr["OA1"].ToString() + "|2^助理医生" + "\n";
            AIP += "AIP|8||";
            AIP += dr["OA2No"].ToString() + "^" + dr["OA2"].ToString() + "|2^助理医生" + "\n";
            AIP += "AIP|9||";
            AIP += dr["OA3No"].ToString() + "^" + dr["OA3"].ToString() + "|2^助理医生" + "\n";
            #endregion
            #region 麻醉医生
            dt = dal.GetUserNoByName(dr["AP1"].ToString());
            UserNO = " ";
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|11||";
            AIP += UserNO + "^" + dr["AP1"].ToString() + "|麻醉医师" + "\n";
            dt = dal.GetUserNoByName(dr["AP2"].ToString());
            UserNO = " ";
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|12||";
            AIP += UserNO + "^" + dr["AP2"].ToString() + "|麻醉医师" + "\n";
            dt = dal.GetUserNoByName(dr["AP3"].ToString());
            UserNO = " ";
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|13||";
            AIP += UserNO + "^" + dr["AP3"].ToString() + "|麻醉医师" + "\n";
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
                    string patid = dgvOTypesetting.CurrentRow.Cells["patid"].Value.ToString();
                    string Oroom = dgvOTypesetting.CurrentRow.Cells["Oroom"].Value.ToString();
                    string Osecond = dgvOTypesetting.CurrentRow.Cells["second"].Value.ToString();
                    if (Oroom == "" && Osecond == "")
                    {
                        MessageBox.Show("手术间和台次不能都为空");
                        return;
                    }

                    string message = AppendHL7stringConfig(patid);

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


        adims_DAL.SendHl7Dal dal = new adims_DAL.SendHl7Dal();


        private void DataView_Load(object sender, EventArgs e)
        {
            BindPaibanInfo(whereNotEyeOper);

        }

        private void BindPaibanInfo(string where)
        {
            DataTable dt = dal.GetPaiBanWhere(dtOdate.Value.Date.ToString("yyyy-MM-dd"), where);
            dgvOTypesetting.DataSource = dt.DefaultView;
            if (dt != null)
            {
                tbCount.Text = dt.Rows.Count.ToString();
            }
            else
            {
                tbCount.Text = "0";
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (cbEye.Checked)
            {
                BindPaibanInfo(whereEyeOper);
            }
            else
            {
                BindPaibanInfo(whereNotEyeOper);
            }
        }
        string whereEyeOper = " and patdpm like '%眼科%' ";
        string whereNotEyeOper = " and patdpm not like '%眼科%' ";
        private void cbEye_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEye.Checked)
            {
                BindPaibanInfo(whereEyeOper);
            }
            else
            {
                BindPaibanInfo(whereNotEyeOper);
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dgvOTypesetting.Rows)
            {

                string patid = item.Cells["patid"].Value.ToString();
                string Oroom = item.Cells["Oroom"].Value.ToString();
                string Osecond = item.Cells["second"].Value.ToString();
                if (Oroom == "" && Osecond == "")
                {
                    MessageBox.Show("手术间和台次不能都为空");
                    return;
                }

                string message = AppendHL7stringConfig(patid);

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
