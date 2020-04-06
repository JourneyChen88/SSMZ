using Adims_Utility;
using MediII.Common;
using NHapi.Model.V24.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_BLL
{
    public class Hl7Bll
    {
        public static adims_DAL.SqlSugarDal dal = new adims_DAL.SqlSugarDal();
        /// <summary>
        /// 确定手术安排
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName">用户名</param>
        /// <param name="uid">用户ID</param>
        /// <returns></returns>
        public static string AppendHL7stringConfig(int id, string userName, string uid)
        {
            ORM_O01 orm = new ORM_O01();
            var paiban = dal.GetPaiban(id);
            int ostateNum = paiban.Ostate.ToInt32();
            LogHelp.SaveLogHL7("状态" + ostateNum.ToString());
            string SCH_1 = "";
            #region 组装消息头
            if (paiban.IsSend == 0)
            {
                SCH_1 = "确定手术安排";
                orm.MSH.MessageType.TriggerEvent.Value = "S18";
                orm.MSH.MessageType.MessageStructure.Value = "SIU_S12";
            }
            else
            {
                SCH_1 = "修改手术安排";
                orm.MSH.MessageType.TriggerEvent.Value = "S19";
                orm.MSH.MessageType.MessageStructure.Value = "SIU_S12";
            }


            orm.MSH.MessageType.MessageType.Value = "SIU";

            orm.MSH.FieldSeparator.Value = MessageConstant.FieldSeparator;
            orm.MSH.SendingApplication.NamespaceID.Value = "SSMZ";
            orm.MSH.SendingFacility.NamespaceID.Value = "SSMZ";
            orm.MSH.ReceivingApplication.NamespaceID.Value = "MediII";
            orm.MSH.ReceivingFacility.NamespaceID.Value = "MediII";
            orm.MSH.EncodingCharacters.Value = MessageConstant.EncodingCharacters;
            orm.MSH.VersionID.VersionID.Value = MessageConstant.VersionID;
            orm.MSH.DateTimeOfMessage.TimeOfAnEvent.SetLongDateWithSecond(DateTime.Now);
            orm.MSH.MessageControlID.Value = MediII.Common.GUIDHelper.NewGUID();
            orm.MSH.ProcessingID.ProcessingID.Value = MessageConstant.ProcessingID;

            #endregion

            // int Jieguo = dal.UpdatePaibanConfig(id);//修改成 已排班

            #region SCH|
            String SCH = "SCH||||||^" + SCH_1 + "^^^原因" + "|||||";
            SCH += "^^^" + paiban.Odate.AddDays(-1).ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + "|||||";
            SCH += uid + "^^" + userName + "|||";
            SCH += "^^^^^^^^" + paiban.Patdpm.ToString() + "|";
            SCH += uid + "^^" + userName + "||||||";
            SCH += paiban.ApplyId + "\n";
            #endregion

            string PID = paiban.PidInfo + "\n";
            string PV1 = paiban.Pv1Info + "\n";
            #region RGS|
            String RGS = "RGS|1" + "\n";
            #endregion

            #region AIS|
            String AIS = "AIS|1||";
            AIS += paiban.OperNo + "^" + paiban.Oname + "|||||||";
            AIS += paiban.Oroom + "^^^" + paiban.Second + "|" + "\n";
            #endregion


            #region 手术医生

            String AIP = "AIP|1||";
            var listUser = dal.GetUser();
            AIP += paiban.OsNo + "^" + paiban.OS + " |主刀医生" + "\n";

            AIP += "AIP|7||";
            AIP += paiban.Os1No + "^" + paiban.OS1 + "|2^助理医生" + "\n";

            AIP += "AIP|8||";
            AIP += paiban.Os2No + "^" + paiban.OS2 + "|2^助理医生" + "\n";

            AIP += "AIP|9||";
            AIP += paiban.Os3No + "^" + paiban.OS3 + "|2^助理医生" + "\n";
            #endregion

            #region 护士

            string UserNO = string.Empty;
            var user = listUser.FirstOrDefault(a => a.User_name == paiban.SN1);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|2||";
            AIP += paiban.SN1No + "^" + paiban.SN1 + "|4^洗手护士" + "\n";

            UserNO = string.Empty;
            user = listUser.FirstOrDefault(a => a.User_name == paiban.SN2);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|3||";
            AIP += paiban.SN2No + "^" + paiban.SN2 + "|4^洗手护士" + "\n";

            UserNO = string.Empty;
            user = listUser.FirstOrDefault(a => a.User_name == paiban.ON1);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|4||";
            AIP += paiban.ON1No + "^" + paiban.ON1 + "|5^巡回护士" + "\n";
            UserNO = string.Empty;
            user = listUser.FirstOrDefault(a => a.User_name == paiban.ON2);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|5||";
            AIP += paiban.ON2No + "^" + paiban.ON2 + "|5^巡回护士" + "\n";


            AIP += "AIP|6||";
            AIP += "^" + "" + "|5^巡回护士" + "\n";
            #endregion

            AIP += "AIP|10|||6^体外循环师" + "\n";

            #region 麻醉医生

            UserNO = string.Empty;
            user = listUser.FirstOrDefault(a => a.User_name == paiban.AP1);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|11||";
            AIP += paiban.AP1No + "^" + paiban.AP1 + "|3^麻醉师" + "\n";

            UserNO = string.Empty;
            user = listUser.FirstOrDefault(a => a.User_name == paiban.AP2);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|12||";
            AIP += paiban.AP2No + "^" + paiban.AP2 + "|7^助理麻醉师" + "\n";

            UserNO = string.Empty;
            user = listUser.FirstOrDefault(a => a.User_name == paiban.AP3);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|13||";
            AIP += paiban.AP3No + "^" + paiban.AP3 + "|7^助理麻醉师" + "\n";
            #endregion


            #region 转换消息对象为字符串
            String hl7Message = SCH + PID + PV1 + RGS + AIS + AIP;
            NHapi.Base.Parser.PipeParser parser = new NHapi.Base.Parser.PipeParser();
            string message = parser.Encode(orm) + hl7Message;
            return message;
            #endregion

        }

        /// <summary>
        /// 手术消息反馈
        /// </summary>
        /// <returns></returns>
        public static string AppendHL7stringOperConfig(string patzhuyuanId, string userNo, string userName)
        {
            ORM_O01 orm = new ORM_O01();
            #region 组装消息头
            orm.MSH.MessageType.MessageType.Value = "SIU";
            orm.MSH.MessageType.TriggerEvent.Value = "S21";
            orm.MSH.MessageType.MessageStructure.Value = "SIU_S12";
            orm.MSH.FieldSeparator.Value = MessageConstant.FieldSeparator;
            orm.MSH.SendingApplication.NamespaceID.Value = "SSMZ";
            orm.MSH.SendingFacility.NamespaceID.Value = "SSMZ";
            orm.MSH.ReceivingApplication.NamespaceID.Value = "MediII";
            orm.MSH.ReceivingFacility.NamespaceID.Value = "MediII";
            orm.MSH.EncodingCharacters.Value = MessageConstant.EncodingCharacters;
            orm.MSH.VersionID.VersionID.Value = MessageConstant.VersionID;
            orm.MSH.DateTimeOfMessage.TimeOfAnEvent.SetLongDateWithSecond(DateTime.Now);
            orm.MSH.MessageControlID.Value = MediII.Common.GUIDHelper.NewGUID();
            orm.MSH.ProcessingID.ProcessingID.Value = MessageConstant.ProcessingID;

            #endregion

            var mzjldDto = dal.GetPaibanAndMzjld(patzhuyuanId);

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
            SCH += mzjldDto.ApplyId + "\n";
            #endregion

            #region NTE|
            String NTE = "NTE|1|||~" + "\n";
            #endregion


            string PID = mzjldDto.PidInfo + "\n";
            string PV1 = mzjldDto.Pv1Info + "\n";


            string RGS = "RGS|1" + "\n";

            #region AIS|
            String AIS = "AIS|1||";
            string OperName = mzjldDto.Oname;

            AIS += mzjldDto.Ocode + "^" + mzjldDto.Oname + "|||||||";
            AIS += mzjldDto.Oroom + "^" + mzjldDto.Second + "\n";
            #endregion

            #region 手术医生

            String AIP = "AIP|1||";
            var listUser = dal.GetUser();
            AIP += mzjldDto.OsNo + "^" + mzjldDto.OS + " |主刀医生" + "\n";

            AIP += "AIP|7||";
            AIP += mzjldDto.Os1No + "^" + mzjldDto.OS1 + "|2^助理医生" + "\n";

            AIP += "AIP|8||";
            AIP += mzjldDto.Os2No + "^" + mzjldDto.OS2 + "|2^助理医生" + "\n";

            AIP += "AIP|9||";
            AIP += mzjldDto.Os2No + "^" + mzjldDto.OS3 + "|2^助理医生" + "\n";
            #endregion

            #region 护士

            string UserNO = string.Empty;
            var user = listUser.FirstOrDefault(a => a.User_name == mzjldDto.SN1);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|2||";
            AIP += mzjldDto.SN1No + "^" + mzjldDto.SN1 + "|4^洗手护士" + "\n";

            UserNO = string.Empty;
            user = listUser.FirstOrDefault(a => a.User_name == mzjldDto.SN2);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|3||";
            AIP += mzjldDto.SN2No + "^" + mzjldDto.SN2 + "|4^洗手护士" + "\n";

            UserNO = string.Empty;
            user = listUser.FirstOrDefault(a => a.User_name == mzjldDto.ON1);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|4||";
            AIP += mzjldDto.ON1No + "^" + mzjldDto.ON1 + "|5^巡回护士" + "\n";
            UserNO = string.Empty;
            user = listUser.FirstOrDefault(a => a.User_name == mzjldDto.ON2);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|5||";
            AIP += mzjldDto.ON2No + "^" + mzjldDto.ON2 + "|5^巡回护士" + "\n";


            AIP += "AIP|6||";
            AIP += "^" + "" + "|5^巡回护士" + "\n";
            #endregion

            AIP += "AIP|10|||6^体外循环师" + "\n";

            #region 麻醉医生

            UserNO = string.Empty;
            user = listUser.FirstOrDefault(a => a.User_name == mzjldDto.AP1);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|11||";
            AIP += mzjldDto.AP1No + "^" + mzjldDto.AP1 + "|3^麻醉师" + "\n";

            UserNO = string.Empty;
            user = listUser.FirstOrDefault(a => a.User_name == mzjldDto.AP2);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|12||";
            AIP += mzjldDto.AP2No + "^" + mzjldDto.AP2 + "|7^助理麻醉师" + "\n";

            UserNO = string.Empty;
            user = listUser.FirstOrDefault(a => a.User_name == mzjldDto.AP3);
            if (user != null) UserNO = user.Uid;
            AIP += "AIP|13||";
            AIP += mzjldDto.AP3No + "^" + mzjldDto.AP3 + "|7^助理麻醉师" + "\n";
            #endregion



            #region 转换消息对象为字符串
            String hl7Message = SCH + NTE + PID + PV1 + RGS + AIS + AIP;
            NHapi.Base.Parser.PipeParser parser = new NHapi.Base.Parser.PipeParser();
            string message = parser.Encode(orm) + hl7Message;
            return message;
            #endregion

        }

    }
}
