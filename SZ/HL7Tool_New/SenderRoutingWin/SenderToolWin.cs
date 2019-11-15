using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NHapi.Model.V24;
using NHapi.Base.Parser;
using NHapi.Model.V24.Message;
using NHapi.Model.V24.Segment;
using MediII.Common;
using Microsoft.International.Converters.PinYinConverter;
using NHapi.Model.V24.Datatype;
using System.Configuration;


namespace SenderRoutingWin
{
    public partial class SenderToolWin : Form
    {
        string FilePath = string.Empty;
        string message = string.Empty;

        public SenderToolWin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.txtIP.Text = ConfigurationManager.AppSettings["Address"];
            this.txtPort.Text = ConfigurationManager.AppSettings["Port"];
            this.txtNum.Text = "1";
        }


        private void btnSender_Click(object sender, EventArgs e)
        {
            message = this.txtOutput.Text.Trim();
            if (message.Length > 0)
            {
                if (txtIP.Text.Trim().Length > 0 && txtPort.Text.Trim().Length > 0 && txtNum.Text.Trim().Length > 0)
                {
                    SenderRoutingLib.SocketSender send = new SenderRoutingLib.SocketSender();
                    object objResult;
                    int iResult = 0;
                    int count = Convert.ToInt32(txtNum.Text.Trim());
                    if (count < 10)
                    {
                        new System.Threading.Thread(o =>
                        {
                            for (int i = 0; i < count; i++)
                            {
                                objResult = send.Send(message, txtIP.Text.Trim(), Convert.ToInt32(txtPort.Text.Trim()));
                                string ack = objResult == null ? string.Empty : objResult.ToString();
                                if (ack.Contains("AA"))
                                {
                                    iResult++;
                                    SetText(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                }
                                else
                                {
                                    iResult++;
                                    SetText(string.Format("\r\n消息处理失败原因：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
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
                                    objResult = send.Send(message, txtIP.Text.Trim(), Convert.ToInt32(txtPort.Text.Trim()));
                                    string ack = objResult == null ? string.Empty : objResult.ToString();
                                    if (ack.Contains("AA"))
                                    {
                                        iResult++;
                                        SetText(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                    }
                                    else
                                    {
                                        iResult++;
                                        SetText(string.Format("\r\n发送失败，错误信息：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                    }
                                }
                            }).Start();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("IP地址或端口或发送次数未填写");
                }
            }
            else
            {
                MessageBox.Show("无法获取消息内容，请检查");
            }
        }
        delegate void MyInvoke(string str);
        private void SetText(string s)
        {
            if (txtOutput.InvokeRequired)
            {
                MyInvoke _myInvoke = new MyInvoke(SetText);
                this.Invoke(_myInvoke, new object[] { s });
            }
            else
            {
                this.txtOutput.AppendText(s);
                Application.DoEvents();
            }
        }
        List<MessageNode> messageList = new List<MessageNode>();
        private void button1_Click(object sender, EventArgs e)
        {
            ORM_O01 orm = new ORM_O01();
            #region 组装消息头
            orm.MSH.MessageType.MessageType.Value = "ORM";
            orm.MSH.MessageType.TriggerEvent.Value = "O01";
            orm.MSH.MessageType.MessageStructure.Value = "ORM_O01";
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
            for (int i = 0; i < messageList.Count; i++)
            {
                MessageNode node = messageList[i];

                #region 组装PID段
                PID pid = orm.PATIENT.PID;
                pid.PatientID.ID.Value = node.PIDList["PatientIdentifier"];
                pid.GetPatientIdentifierList(0).ID.Value = node.PIDList["PatientIdentifier"];
                pid.GetPatientIdentifierList(1).ID.Value = node.PIDList["jiuzhenkh"];
                pid.GetPatientIdentifierList(2).ID.Value = node.PIDList["binganhao"];
                pid.GetPatientIdentifierList(3).ID.Value = node.PIDList["shebaokh"];
                pid.GetPatientIdentifierList(4).ID.Value = node.PIDList["gongfeizh"];
                string bingRenXM = node.PIDList["bingrenxm"];
                #region 姓名转全拼
                StringBuilder sb = new StringBuilder();
                foreach (char c in bingRenXM)
                {
                    if (ChineseChar.IsValidChar(c))
                    {
                        ChineseChar pinYinConvert = new ChineseChar(c);
                        string pinYin = pinYinConvert.Pinyins[0];
                        if (!string.IsNullOrEmpty(pinYin))
                        {
                            string word = pinYin.Substring(0, pinYin.Length - 1);
                            //首字母大写，其他小写
                            StringBuilder sbW = new StringBuilder(word.Length);
                            for (int n = 0; n < word.Length; n++)
                            {
                                string s = word.Substring(n, 1);
                                if (n == 0)
                                    sbW.Append(s.ToUpper());
                                else
                                    sbW.Append(s.ToLower());
                            }
                            sb.Append(sbW.ToString());
                        }
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
                #endregion
                pid.GetPatientName(0).FamilyName.Surname.Value = sb.ToString();
                pid.GetPatientName(0).GivenName.Value = bingRenXM;
                DateTime dtCHUSHENGRQ = DateTime.Parse(node.PIDList["chushengrq"]);
                pid.DateTimeOfBirth.TimeOfAnEvent.SetLongDateWithSecond((DateTime)dtCHUSHENGRQ);//出生日期
                //if (string.IsNullOrEmpty(bingRenXx.XINGBIE))    //性别
                //    pid.AdministrativeSex.Value = "O";
                //else if (bingRenXx.XINGBIE == "男")
                //    pid.AdministrativeSex.Value = "M";
                //else if (bingRenXx.XINGBIE == "女")
                pid.AdministrativeSex.Value = node.PIDList["sex"];

                XAD address = pid.GetPatientAddress(0);
                address.City.Value = node.PIDList["city"];
                address.StateOrProvince.Value = node.PIDList["province"];
                address.StreetAddress.StreetOrMailingAddress.Value = node.PIDList["streetaddress"];
                address.ZipOrPostalCode.Value = node.PIDList["postcode"];
                address.OtherDesignation.Value = node.PIDList["postcode"];
                pid.GetPhoneNumberHome(0).PhoneNumber.Value = node.PIDList["homephone"];
                pid.GetPhoneNumberBusiness(0).PhoneNumber.Value = node.PIDList["workphone"];
                pid.MaritalStatus.Text.Value = node.PIDList["hunyin"];
                string hunYinDm = string.Empty;
                //if (bingRenXx.HUNYINDM == "0")
                //    hunYinDm = "S";
                //else if (bingRenXx.HUNYINDM == "1")
                //    hunYinDm = "M";
                //else
                hunYinDm = "O";//婚姻代码
                pid.MaritalStatus.Identifier.Value = hunYinDm;//婚姻代码
                pid.PatientAccountNumber.ID.Value = node.PIDList["yibaokh"];
                pid.SSNNumberPatient.Value = node.PIDList["idnumber"];

                pid.GetEthnicGroup(0).Identifier.Value = node.PIDList["EthicGroupID"];
                pid.GetEthnicGroup(0).Text.Value = node.PIDList["EthicGroupName"];
                pid.BirthPlace.Value = node.PIDList["BirthPlace"];
                pid.Nationality.Identifier.Value = node.PIDList["NationalityID"];
                pid.Nationality.Text.Value = node.PIDList["NationalityName"];
                #endregion
                #region 组装PV1

                PV1 pv1 = orm.PATIENT.PATIENT_VISIT.PV1;
                string menZhenZhuYuanBZ = node.PV1List["menZhenZhuYuanBZ"];
                if (menZhenZhuYuanBZ == "0")
                {
                    //门诊               
                    pv1.SetIDPV1.Value = node.PV1List["SetID"];
                    pv1.AdmissionType.Value = "R";
                    pv1.PatientClass.Value = "O"; //门诊O
                    pv1.GetAttendingDoctor(0).IDNumber.Value = node.PV1List["AdmittingDoctorID"];
                    pv1.HospitalService.Value = node.PV1List["HospitalService"];
                    pv1.PatientType.Value = node.PV1List["PatientType"];
                    pv1.VisitNumber.ID.Value = node.PV1List["VisitNumber"];
                    pv1.ServicingFacility.Value = node.PV1List["ServicingFacility"]; //上下午标志 0-上午,1-下午,2-晚上
                    pv1.PendingLocation.PointOfCare.Value = node.PV1List["PendingLocation"];
                    DateTime dtZHIDANRQ = DateTime.Parse(node.PV1List["AdmitDate"]);
                    pv1.AdmitDateTime.TimeOfAnEvent.SetLongDateWithSecond((DateTime)dtZHIDANRQ);//制单医生日期 就诊日期
                    pv1.VisitIndicator.Value = "V";
                    pv1.ChargePriceIndicator.Value = node.PV1List["ChargePriceIndicator"];
                }
                else
                {
                    //住院
                    pid.GetPatientIdentifierList(1).ID.Value = node.PIDList["jiuzhenkh"];
                    pid.GetAlternatePatientIDPID(0).ID.Value = node.PIDList["yingerbz"];//婴儿标志0非婴儿1婴儿               
                    pid.GetMotherSIdentifier(0).ID.Value = node.PIDList["MothersIdentifier"];
                    pv1.VisitNumber.ID.Value = node.PV1List["VisitNumber"];
                    pv1.SetIDPV1.Value = node.PV1List["AlternateVisitID"];//住医生院次数
                    pv1.PatientClass.Value = "I";//住院病人
                    pv1.AssignedPatientLocation.PointOfCare.Value = node.PV1List["PointOfCare"];
                    pv1.AssignedPatientLocation.Bed.Value = node.PV1List["Bed"];
                    pv1.AssignedPatientLocation.Room.Value = node.PV1List["Room"];
                    if (!String.IsNullOrEmpty(node.PV1List["Facility"]))//当前科室，当前科室名称
                    {
                        pv1.AssignedPatientLocation.Facility.NamespaceID.Value = node.PV1List["Facility"];
                    }
                    pv1.GetAttendingDoctor(0).IDNumber.Value = node.PV1List["AttendingDoctorID"];
                    pv1.GetAttendingDoctor(0).GivenName.Value = node.PV1List["AttendingDoctorFamilyName"];
                    DateTime dtRuYuanRQ = DateTime.Parse(node.PV1List["AdmitDate"]);
                    pv1.AdmitDateTime.TimeOfAnEvent.SetLongDateWithSecond((DateTime)dtRuYuanRQ);//入院日期
                    pv1.PendingLocation.PointOfCare.Value = node.PV1List["PendingLocation"];  //当前科室
                    pv1.PatientType.Value = node.PV1List["PatientType"];//费用类别
                    DateTime dtRuKeRQ = DateTime.Parse(node.PV1List["AdmitDate"]);
                    if (dtRuKeRQ != null)
                        pv1.GetContractEffectiveDate(0).Value = ((DateTime)dtRuKeRQ).ToString("yyyyMMddHHmmss");//入科日期
                    pv1.VisitIndicator.Value = "V";
                    pv1.ChargePriceIndicator.Value = node.PV1List["ChargePriceIndicator"];
                    pv1.AlternateVisitID.ID.Value = node.PV1List["AlternateVisitID"];//住院次数
                }
                #endregion
                #region 组装检验申请内容
                for (int j = 0; j < node.ORCList.Count; j++)
                {
                    ORC orc = orm.GetORDER(j).ORC;
                    orc.OrderControl.Value = "SC";
                    orc.PlacerOrderNumber.EntityIdentifier.Value = node.ORCList[j]["PlacerOrderNumber"]; //申请单ID
                    orc.FillerOrderNumber.EntityIdentifier.Value = node.ORCList[j]["FillterOrderNumber"]; //检查号
                    orc.OrderStatus.Value = "SC";
                    orc.DateTimeOfTransaction.TimeOfAnEvent.SetLongDateWithSecond(DateTime.Parse(node.ORCList[j]["DateTimeOfTransaction"]));//登记时间
                    orc.GetEnteredBy(0).IDNumber.Value = node.ORCList[j]["EnteredBy"];//录入者                  
                    orc.EnteringOrganization.Identifier.Value = node.ORCList[j]["EnteringOrganizationIdentifier"];//申请科室ID
                    orc.EnteringOrganization.Text.Value = node.ORCList[j]["EnteringOrganizationText"];//申请科室名称 
                    orc.GetOrderingProvider(j).IDNumber.Value = node.ORCList[j]["OrderingProviderID"];//申请医生ID
                    orc.GetOrderingProvider(j).GivenName.Value = node.ORCList[j]["OrderingProviderName"];//申请医生姓名
                    orc.OrderControlCodeReason.Text.Value = node.ORCList[j]["OrderControlCodeReason"];//检查目的

                    OBR obr = orm.GetORDER(j).ORDER_DETAIL.OBR;
                    obr.SetIDOBR.Value = (j + 1).ToString();
                    obr.PlacerOrderNumber.EntityIdentifier.Value = node.OBRList[j]["PlacerOrderNumber"];  //申请单ID
                    obr.FillerOrderNumber.EntityIdentifier.Value = node.OBRList[j]["FillterOrderNumber"]; //检查号
                    obr.UniversalServiceIdentifier.Identifier.Value = node.OBRList[j]["UniversalServiceIdentifier"]; //检查部位ID
                    obr.UniversalServiceIdentifier.Text.Value = node.OBRList[j]["UniversalServiceText"];  //检查部位名称
                    obr.Priority.Value = node.OBRList[j]["Priority"];//优先级
                    obr.DangerCode.Identifier.Value = node.OBRList[j]["DangerCodeIdentifier"]; ; //执行科室
                    obr.DangerCode.Text.Value = node.OBRList[j]["DangerCodeText"]; //执行科室名称
                }
                #endregion
            }
            #region 转换消息对象为字符串
            NHapi.Base.Parser.PipeParser parser = new PipeParser();
            message = parser.Encode(orm);
            this.txtOutput.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            this.txtOutput.AppendText("构建消息内容完成，消息内容如下：");
            this.txtOutput.AppendText(System.Environment.NewLine);
            this.txtOutput.AppendText(message);
            this.txtOutput.AppendText(System.Environment.NewLine);
            #endregion
        }

       
    }
}