using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace adims_DAL
{
    public class AdimsProvider
    {
        private DBConn dBConn = new DBConn();
        private SQLiteHelper sh = new SQLiteHelper();

        #region <<方法>>

        static string strconn = ConfigurationManager.AppSettings["ConnectionString"];
        static string connStr = ConfigurationManager.AppSettings["ConnectionString"];

        /// <summary>
        /// 从table中获取数据
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DataTable GetData(string table)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter("select * from " + table, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            return dt;
        }
        public static DataTable GetData1(string table, string name)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter("select ID 编号,name " + name + " from " + table, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        public static DataTable GetData1(string table, string name, string belong)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter("select ID 编号,name " + name + ",belong " + belong + " from " + table, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 取出排班的类容
        /// </summary>
        /// <param name="riqi">日期</param>
        /// <returns></returns>
        public static string GetContent(string riqi)
        {
            string content = "";
            DataTable dt = GetData1("select neirong from yuangongpaiban where riqi='" + riqi + "'");
            if (dt.Rows.Count > 0)
            { content = dt.Rows[0][0].ToString(); }
            return content;


        }

        public static DataTable GetData1(string sql)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 向table表里面插入数据
        /// </summary>
        /// <param name="name">插入值</param>
        /// <param name="table">数据表</param>
        public static void AddData1(string name, string table)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from " + table, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataRow newrow = dt.NewRow();
            newrow["Name"] = name;
            dt.Rows.Add(newrow);
            adapter.Update(dt);
        }

        public static void AddData1(string name, string belong, string table)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from " + table, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataRow newrow = dt.NewRow();
            newrow["Name"] = name;
            newrow["belong"] = belong;
            dt.Rows.Add(newrow);
            adapter.Update(dt);
        }
        /// <summary>
        /// 向请假表里面添加数据
        /// </summary>
        /// <param name="ygbh"></param>
        /// <param name="ygName"></param>
        /// <param name="qjDate"></param>
        /// <param name="qjNum"></param>
        /// <param name="qjWhy"></param>
        public static void AddData2(string ygbh, string ygName, string qjDate, string qjNum, string qjWhy)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from qingjia", conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataRow newrow = dt.NewRow();
            newrow["ygbh"] = ygbh;
            newrow["ygName"] = ygName;
            newrow["qjDate"] = qjDate;
            newrow["qjNum"] = Convert.ToInt32(qjNum);
            newrow["qjWhy"] = qjWhy;
            dt.Rows.Add(newrow);
            adapter.Update(dt);

        }

        /// <summary>
        /// 更新table表的中的name字段
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        /// <param name="table"></param>
        public static void UpdateData1(string ID, string Name, string table)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from " + table, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataColumn[] keys = new DataColumn[1];
            keys[0] = dt.Columns["ID"];
            dt.PrimaryKey = keys;
            DataRow findrow = dt.Rows.Find(ID);
            findrow["Name"] = Name;
            adapter.Update(dt);
        }
        public static void UpdateData1(string ID, string Name, string belong, string table)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from " + table, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataColumn[] keys = new DataColumn[1];
            keys[0] = dt.Columns["ID"];
            dt.PrimaryKey = keys;
            DataRow findrow = dt.Rows.Find(ID);
            findrow["Name"] = Name;
            findrow["belong"] = belong;
            adapter.Update(dt);


        }

        /// <summary>
        /// 判断table表的name是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static bool IsHaveName(string name, string table)
        {
            string sql = "select * from " + table + " where Name='" + name + "'";
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            if (count < 1)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="neirong">排班内容</param>
        /// <param name="riqi">日期</param>
        public static void AddData(string time, string Date, string mzyi, string fs)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from yuangongpaiban1", conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataRow newrow = dt.NewRow();
            newrow["Otime"] = time;
            newrow["Date"] = Date;
            newrow["mzyi"] = mzyi;
            newrow["fs"] = fs;
            dt.Rows.Add(newrow);
            adapter.Update(dt);

        }

        /// <summary>
        /// 更新内容
        /// </summary>
        /// <param name="neirong"></param>
        /// <param name="riqi"></param>
        public static void UpdateData(string time, string Date, string mzyi, string fs, string ID)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from yuangongpaiban1", conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataColumn[] keys = new DataColumn[1];
            keys[0] = dt.Columns["ID"];
            dt.PrimaryKey = keys;
            DataRow findrow = dt.Rows.Find(ID);
            findrow["Otime"] = time;
            findrow["Date"] = Date;
            findrow["mzyi"] = mzyi;
            findrow["fs"] = fs;
            adapter.Update(dt);


        }

        /// <summary>
        /// 修改请假表
        /// </summary>
        /// <param name="ygbh"></param>
        /// <param name="ygName"></param>
        /// <param name="qjDate"></param>
        /// <param name="qjNum"></param>
        /// <param name="qjWhy"></param>
        /// <param name="ID"></param>
        public static void UpdateDate1(string ygbh, string ygName, string qjDate, string qjNum, string qjWhy, string ID)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from qingjia", conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataColumn[] keys = new DataColumn[1];
            keys[0] = dt.Columns["ID"];
            dt.PrimaryKey = keys;
            DataRow findrow = dt.Rows.Find(ID);
            findrow["ygName"] = ygName;
            findrow["ygbh"] = ygbh;
            findrow["qjDate"] = qjDate;
            findrow["qjNum"] = qjNum;
            findrow["qjWhy"] = qjWhy;
            adapter.Update(dt);

        }


        /// <summary>
        /// 是否应该排班
        /// </summary>
        /// <param name="riqi"></param>
        /// <returns></returns>
        public static bool isPaiBan(string riqi)
        {
            SqlConnection conn = new SqlConnection(strconn);
            string sql = "select * from yuangongpaiban where riqi='" + riqi + "'";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            int rowscount = ds.Tables[0].Rows.Count;
            if (rowscount < 1)
            { return true; }
            else
            { return false; }

        }

        /// <summary>
        /// 删除table表的ID行
        /// </summary>
        /// <param name="ID">ID</param>
        public static void DeleteData(string ID, string table)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from " + table, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataColumn[] keys = new DataColumn[1];
            keys[0] = dt.Columns["ID"];
            dt.PrimaryKey = keys;
            DataRow findrow = dt.Rows.Find(ID);
            findrow.Delete();
            adapter.Update(dt);
        }
        #endregion

        public DataTable GetHisOperName(string condition)
        {
            string sql = "SELECT ITEMID 编码,ITEMNAME 手术名称,SSDJ 手术等级,SSQK 手术切口,INPUTSTR  from V_HISTOSSMZ_SSZD  where 1=1 " + condition;
            return dBConn.GetDataTable(sql);
        }

        #region <<T_SQL>>

        #region <<基础资料>>

        private static readonly string SQL_SURGERYSTAFF_GETLIST = "SELECT MedicalNO,MedicalName,CONVERT(NVARCHAR,PostType) AS PostType FROM Adims_SurgeryStaff WITH (NOLOCK) WHERE {0} ORDER BY PostType";
        private static readonly string SQL_MASTER_LIST = " SELECT ID,Name,'0' AS SelectRowState FROM {0} WITH (NOLOCK)";

        #endregion

        #region <<员工排班>>

        private static readonly string SQL_EMPLOYEES_GETLIST = "SELECT EmployeesID,AnesthesiaDoctor,Nurse,Divisions,[Time] FROM Adims_Employees WITH (NOLOCK) WHERE {0} ORDER BY [Time]";
        private static readonly string SQL_EMPLOYEES_INSERT = "INSERT INTO Adims_Employees (AnesthesiaDoctor,Nurse,Divisions,[Time]) VALUES('{0}','{1}','{2}','{3}')";
        private static readonly string SQL_EMPLOYEES_UPDATE = "UPDATE Adims_Employees SET AnesthesiaDoctor = '{0}',Nurse = '{1}',Divisions = '{2}',[Time] = '{3}' WHERE EmployeesID = {4}";
        private static readonly string SQL_EMPLOYEES_DELETE = "DELETE Adims_Employees WITH (ROWLOCK) WHERE EmployeesID = '{0}'";

        #endregion

        #region <<请假登记>>

        private static readonly string SQL_LEAVEREGISTRATION_GETLIST = "SELECT LeaveRegistrationID,EmployeesNO,EmployeesName,[StartDate],[StartTime],[EndDate],[EndTime],[SumDay],LeaveReason FROM Adims_LeaveRegistration WITH (NOLOCK) WHERE {0}";
        private static readonly string SQL_LEAVEREGISTRATION_INSERT = "INSERT INTO [Adims_LeaveRegistration]( [EmployeesNO],[EmployeesName],[StartDate],[StartTime],[EndDate],[EndTime],[SumDay],[LeaveReason],[CreateDate])VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',GETDATE())";
        private static readonly string SQL_LEAVEREGISTRATION_UPDATE = "UPDATE  [Adims_LeaveRegistration] SET [EmployeesNO] = '{0}',[EmployeesName] = '{1}',[StartDate] = '{2}',[StartTime] = '{3}',[EndDate] = '{4}',[EndTime] = '{5}',[SumDay] = '{6}',[LeaveReason] = '{7}',[UpdateDate] = GETDATE() WHERE LeaveRegistrationID = '{8}'";
        private static readonly string SQL_LEAVEREGISTRATION_DELETE = "DELETE Adims_LeaveRegistration WHERE LeaveRegistrationID = '{0}'";

        #endregion

        #region <<加班登记>>

        private static readonly string SQL_JBJL_GETLIST = "SELECT [ID] ,[EmployeesNO] ,[EmployeesName] ,[JBDay] ,[startTime],[endTime] ,[JBReason] FROM Adims_JBJL WITH (NOLOCK) WHERE {0}";
        private static readonly string SQL_JBJL_INSERT = "INSERT INTO [Adims_JBJL]( [EmployeesNO],[EmployeesName],[JBDay],[startTime],[endTime],[JBReason],[creatDate])VALUES ('{0}','{1}','{2}','{3}','{4}','{5}',GETDATE())";
        private static readonly string SQL_JBJL_UPDATE = "UPDATE  [Adims_JBJL] SET [EmployeesNO] = '{0}',[EmployeesName] = '{1}',[JBDay] = '{2}',[startTime] = '{3}',[endTime] = '{4}',[JBReason]='{5}',[UpdateDate] = GETDATE() WHERE ID = '{6}'";
        private static readonly string SQL_JBJL_DELETE = "DELETE Adims_JBJL WHERE ID = '{0}'";

        #endregion

        #region <<智能排班>>

        private static readonly string SQL_OTYPESETTING_SELECT = "SELECT oroom 手术间,second 台次, patid 病人ID,patdpm 科室,patname 病人,patsex 性别,patage 年龄,patbedno 床号,patzhuyuanID 住院号,os 手术医师,oname 手术名字,pattmd 术前诊断,amethod 麻醉方法,"
                + "ap1 主麻医师,ap2 副麻医师1,ap3 副麻医师2,on1 洗手护士1,on2 洗手护士2,sn1 巡回护士1,sn2 巡回护士2,SSStartTime 预计手术开始时间,SSEndTime 预计手术结束时间,"
                + "remarks 备注 from Adims_OTypesetting WITH (NOLOCK) WHERE {0} ORDER BY oroom";

        #endregion

        #region <<术前访视>>

        private static readonly string SQL_BEFOREVISIT_GETLIST = " SELECT BeforeVisitID,PatID,Weight,Blood,HeartRate,Pulse,"
            + "Breathing,BT,Awareness,SSMode,HistoryDrugs,IsAnesthesiaHistory,AnesthesiaHistoryRemark,IsAllergyHistory,"
            + "AllergyHistoryRemark,HeadNeck,Dehisce,Tooth,HLAuscultation,ASAClassification,E,MuscleFeeling,FeelingAbnormal,"
            + "MuscleDrop,Other,PeripheralVenous,SpineCondition,HLClassification,LungFunction,ECG,Chest,LiverFunction,"
            + "LiverFunctionRemark,KidneyFunction,KidneyFunctionRemark,Hemoglobin,Erythrocyte,Hematocrit,BTB,FG,APTT,ThrombinDate,"
            + "Potassium,Hyponatremia,SerumChloride,BloodSugar,OtherAbnormal,AProgram,AMethod,ADrug,MProjects,ProblemHandle,ProtectiveMeasures,Physician,AccessDate,"
            + "CreateDate,UpdateDate,SpineConditionOther  FROM Adims_BeforeVisit WITH (NOLOCK) WHERE {0}";
        private static readonly string SQL_BEFOREVISIT_COUNT = " SELECT Count(*) FROM Adims_BeforeVisit WITH (NOLOCK) WHERE {0}";
        private static readonly string SQL_BEFOREVISIT_INSERT = "INSERT INTO Adims_BeforeVisit(Weight,Blood,HeartRate,Pulse,"
            + "Breathing,BT,Awareness,SSMode,HistoryDrugs,IsAnesthesiaHistory,AnesthesiaHistoryRemark,IsAllergyHistory,"
            + "AllergyHistoryRemark,HeadNeck,Dehisce,Tooth,HLAuscultation,ASAClassification,E,MuscleFeeling,"
            + "FeelingAbnormal,MuscleDrop,Other,PeripheralVenous,SpineCondition,HLClassification,LungFunction,ECG,"
            + "Chest,LiverFunction,LiverFunctionRemark,KidneyFunction,KidneyFunctionRemark,Hemoglobin,Erythrocyte,"
            + "Hematocrit,BTB,FG,APTT,ThrombinDate,Potassium,Hyponatremia,SerumChloride,BloodSugar,OtherAbnormal,AProgram,AMethod,ADrug,MProjects,ProblemHandle,"
            + "ProtectiveMeasures,Physician,AccessDate,PatID,SpineConditionOther,CreateDate) VALUES('{0}','{1}','{2}','{3}','{4}','{5}',"
            + "'{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}',"
            + "'{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}',"
            + "'{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}',"
            + "'{49}','{50}','{51}','{52}','{53}','{54}',GETDATE())";

        private static readonly string SQL_BEFOREVISIT_INSERT_HS = "INSERT INTO Adims_BeforeVisit_HS(Jybs,Jiwangshi,Guominshi,XinliZQ,YangXingZB,MZFF,MZFFother,YuejingLC,JingzhiYS,JingmaiPG,ZerenHushi,Vistor,VisitDate,patid,IsRead) "
            + " VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','0')";
        private static readonly string SQL_BEFOREVISIT_UPDATE = " UPDATE Adims_BeforeVisit WITH (ROWLOCK) SET Weight = '{0}',"
            + "Blood = '{1}',HeartRate = '{2}',Pulse = '{3}',Breathing = '{4}',BT = '{5}',Awareness = '{6}',"
            + "SSMode = '{7}',HistoryDrugs = '{8}',IsAnesthesiaHistory = '{9}',AnesthesiaHistoryRemark = '{10}',"
            + "IsAllergyHistory = '{11}',AllergyHistoryRemark = '{12}',HeadNeck = '{13}',Dehisce = '{14}',"
            + "Tooth = '{15}',HLAuscultation = '{16}',ASAClassification = '{17}',E = '{18}',MuscleFeeling = '{19}',"
            + "FeelingAbnormal = '{20}',MuscleDrop = '{21}',Other = '{22}',PeripheralVenous = '{23}',"
            + "SpineCondition = '{24}',HLClassification = '{25}',LungFunction = '{26}',ECG = '{27}',Chest = '{28}',"
            + "LiverFunction = '{29}',LiverFunctionRemark = '{30}',KidneyFunction = '{31}',KidneyFunctionRemark = '{32}',"
            + "Hemoglobin = '{33}',Erythrocyte = '{34}',Hematocrit = '{35}',BTB = '{36}',FG = '{37}',APTT = '{38}',"
            + "ThrombinDate = '{39}',Potassium = '{40}',Hyponatremia = '{41}',SerumChloride = '{42}',BloodSugar = '{43}',"
            + "OtherAbnormal = '{44}',AProgram = '{45}',AMethod = '{46}',ADrug = '{47}',MProjects = '{48}',ProblemHandle = '{49}',"
            + "ProtectiveMeasures = '{50}',Physician = '{51}',AccessDate = '{52}',PatID = '{53}',SpineConditionOther = '{54}',UpdateDate = GETDATE()";

        private static readonly string SQL_BEFOREVISIT_UPDATE_HS = " UPDATE Adims_BeforeVisit_HS WITH (ROWLOCK) SET Jybs = '{0}',Jiwangshi = '{1}',Guominshi = '{2}',XinliZQ = '{3}',YangXingZB = '{4}',MZFF = '{5}',MZFFother = '{6}',"
            + "YuejingLC = '{7}',JingzhiYS = '{8}',JingmaiPG = '{9}',ZerenHushi = '{10}',Vistor = '{11}',VisitDate = '{12}'  where patid = '{13}'";
        #endregion

        #region <<术后随访>>

        private static readonly string SQL_AFTERVISIT_COUNT = "SELECT Count(*) FROM Adims_AfterVisit WITH (NOLOCK) WHERE PatID = '{0}'";
        private static readonly string SQL_AFTERVISIT_SELECT = "SELECT AfterVisitID,PatID,EnterBP,EnterHeartRate,EnterSpo2,"
            + "OpenVein,MainDrug,Intubate,AnesthesiaInduction,SZOutAmount,SZInAmount,SBSituation,SBWhereabouts,SZBP,"
            + "SZBPCauses,SZHeartRate,SZHeartRateCauses,SZSpo2,SZSpo2Causes,Nausea1,NauseaValue1,Vomit1,VomitValue1,"
            + "Headache1,HeadacheValue1,WaistlegsPain1,WaistlegsPainValue1,URetention1,URetentionValue1,BPReduce1,"
            + "BPReduceValue1,BPElevated1,BPElevatedValue1,HHyperlipidemia1,HHyperlipidemiaValue1,BSuppression1,"
            + "BSuppressionValue1,ThroatPain1,ThroatPainValue1,LNumbness1,LNumbnessValue1,Other11,Other1Flag1,"
            + "Other1Value1,Other21,Other2Flag1,Other2Value1,Other31,Other3Flag1,Other3Value1,AnalgesicScore1,"
            + "Nausea2,NauseaValue2,Vomit2,VomitValue2,Headache2,HeadacheValue2,WaistlegsPain2,WaistlegsPainValue2,"
            + "URetention2,URetentionValue2,BPReduce2,BPReduceValue2,BPElevated2,BPElevatedValue2,HHyperlipidemia2,"
            + "HHyperlipidemiaValue2,BSuppression2,BSuppressionValue2,ThroatPain2,ThroatPainValue2,LNumbness2,"
            + "LNumbnessValue2,Other12,Other1Flag2,Other1Value2,Other22,Other2Flag2,Other2Value2,Other32,Other3Flag2,"
            + "Other3Value2,AnalgesicScore2,Nausea3,NauseaValue3,Vomit3,VomitValue3,Headache3,HeadacheValue3,"
            + "WaistlegsPain3,WaistlegsPainValue3,URetention3,URetentionValue3,BPReduce3,BPReduceValue3,BPElevated3,"
            + "BPElevatedValue3,HHyperlipidemia3,HHyperlipidemiaValue3,BSuppression3,BSuppressionValue3,ThroatPain3,"
            + "ThroatPainValue3,LNumbness3,LNumbnessValue3,Other13,Other1Flag3,Other1Value3,Other23,Other2Flag3,"
            + "Other2Value3,Other33,Other3Flag3,Other3Value3,AnalgesicScore3  FROM Adims_AfterVisit WITH (NOLOCK) WHERE {0}";
        private static readonly string SQL_AFTERVISIT_INSERT = "INSERT INTO Adims_AfterVisit(EnterBP,EnterHeartRate,"
            + "EnterSpo2,OpenVein,MainDrug,Intubate,AnesthesiaInduction,SZOutAmount,SZInAmount,SBSituation,SBWhereabouts,"
            + "SZBP,SZBPCauses,SZHeartRate,SZHeartRateCauses,SZSpo2,SZSpo2Causes,Nausea1,NauseaValue1,Vomit1,VomitValue1,"
            + "Headache1,HeadacheValue1,WaistlegsPain1,WaistlegsPainValue1,URetention1,URetentionValue1,BPReduce1,"
            + "BPReduceValue1,BPElevated1,BPElevatedValue1,HHyperlipidemia1,HHyperlipidemiaValue1,BSuppression1,"
            + "BSuppressionValue1,ThroatPain1,ThroatPainValue1,LNumbness1,LNumbnessValue1,Other11,Other1Flag1,"
            + "Other1Value1,Other21,Other2Flag1,Other2Value1,Other31,Other3Flag1,Other3Value1,AnalgesicScore1,"
            + "Nausea2,NauseaValue2,Vomit2,VomitValue2,Headache2,HeadacheValue2,WaistlegsPain2,WaistlegsPainValue2,"
            + "URetention2,URetentionValue2,BPReduce2,BPReduceValue2,BPElevated2,BPElevatedValue2,HHyperlipidemia2,"
            + "HHyperlipidemiaValue2,BSuppression2,BSuppressionValue2,ThroatPain2,ThroatPainValue2,LNumbness2,"
            + "LNumbnessValue2,Other12,Other1Flag2,Other1Value2,Other22,Other2Flag2,Other2Value2,Other32,Other3Flag2,"
            + "Other3Value2,AnalgesicScore2,Nausea3,NauseaValue3,Vomit3,VomitValue3,Headache3,HeadacheValue3,"
            + "WaistlegsPain3,WaistlegsPainValue3,URetention3,URetentionValue3,BPReduce3,BPReduceValue3,BPElevated3,"
            + "BPElevatedValue3,HHyperlipidemia3,HHyperlipidemiaValue3,BSuppression3,BSuppressionValue3,ThroatPain3,"
            + "ThroatPainValue3,LNumbness3,LNumbnessValue3,Other13,Other1Flag3,Other1Value3,Other23,Other2Flag3,"
            + "Other2Value3,Other33,Other3Flag3,Other3Value3,AnalgesicScore3,PatID,CreateDate) VALUES ('{0}','{1}','{2}',"
            + "'{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}',"
            + "'{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}',"
            + "'{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}',"
            + "'{46}','{47}','{48}','{49}','{50}','{51}','{52}','{53}','{54}','{55}','{56}','{57}','{58}','{59}',"
            + "'{60}','{61}','{62}','{63}','{64}','{65}','{66}','{67}','{68}','{69}','{70}','{71}','{72}','{73}',"
            + "'{74}','{75}','{76}','{77}','{78}','{79}','{80}','{81}','{82}','{83}','{84}','{85}','{86}','{87}',"
            + "'{88}','{89}','{90}','{91}','{92}','{93}','{94}','{95}','{96}','{97}','{98}','{99}','{100}','{101}',"
            + "'{102}','{103}','{104}','{105}','{106}','{107}','{108}','{109}','{110}','{111}','{112}','{113}',GETDATE()) ";
        private static readonly string SQL_AFTERVISIT_INSERT2 = "INSERT INTO [Adims_AfterVisit2] ([patID] ,[XueYa]  ,[XinLv] ,[HuXi] ,[YHTT] ,[SYSY]"
           + " ,[EX] ,[OTu] ,[YShi],[TouTeng] ,[NCN] ,[TengTong] ,[HongZhong] ,[SiZhiFeel],[Other],[YiSheng] ,[VisitTime] ,[CreatTime] )"
           + "VALUES ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}' ,GETDATE())";
        private static readonly string SQL_AFTERVISIT_INSERT_SZ = "INSERT INTO [Adims_AfterVisit_SZ] (XinliZK ,BingfaZother,JKJY,Taidu,Zerenxin ,Jishu,ZonghePJ,ZJAQWS,Zerenhushi,Yijian,Vistor,VisitDate,patid,IsRead)"
         + "VALUES ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','0')";

        private static readonly string SQL_AFTERVISIT_UPDATE = "UPDATE Adims_AfterVisit WITH (ROWLOCK)  SET EnterBP = '{0}',"
            + "EnterHeartRate = '{1}',EnterSpo2 = '{2}',OpenVein = '{3}',MainDrug = '{4}',Intubate = '{5}',AnesthesiaInduction = '{6}',"
            + "SZOutAmount = '{7}',SZInAmount = '{8}',SBSituation = '{9}',SBWhereabouts = '{10}',SZBP = '{11}',SZBPCauses = '{12}',"
            + "SZHeartRate = '{13}',SZHeartRateCauses = '{14}',SZSpo2 = '{15}',SZSpo2Causes = '{16}',Nausea1 = '{17}',"
            + "NauseaValue1 = '{18}',Vomit1 = '{19}',VomitValue1 ='{20}',Headache1 = '{21}',HeadacheValue1 = '{22}',"
            + "WaistlegsPain1 ='{23}' ,WaistlegsPainValue1 = '{24}',URetention1 = '{25}',URetentionValue1 = '{26}',"
            + "BPReduce1 = '{27}',BPReduceValue1 = '{28}',BPElevated1 = '{29}',BPElevatedValue1 ='{30}',HHyperlipidemia1 = '{31}',"
            + "HHyperlipidemiaValue1 = '{32}',BSuppression1 = '{33}',BSuppressionValue1 = '{34}',ThroatPain1 = '{35}',"
            + "ThroatPainValue1 = '{36}',LNumbness1 = '{37}',LNumbnessValue1 = '{38}',Other11 = '{39}',Other1Flag1 = '{40}',"
            + "Other1Value1 = '{41}',Other21 = '{42}',Other2Flag1 = '{43}',Other2Value1 = '{44}',Other31 = '{45}',Other3Flag1 = '{46}',"
            + "Other3Value1 = '{47}',AnalgesicScore1 = '{48}',Nausea2 = '{49}',NauseaValue2 = '{50}',Vomit2 = '{51}',VomitValue2 ='{52}',"
            + "Headache2 = '{53}',HeadacheValue2 = '{54}',WaistlegsPain2 = '{55}',WaistlegsPainValue2 = '{56}',URetention2 = '{57}',"
            + "URetentionValue2 = '{58}',BPReduce2 = '{59}',BPReduceValue2 = '{60}',BPElevated2 = '{61}',BPElevatedValue2 = '{62}',"
            + "HHyperlipidemia2 = '{63}',HHyperlipidemiaValue2 = '{64}',BSuppression2 = '{65}',BSuppressionValue2 = '{66}',"
            + "ThroatPain2 = '{67}',ThroatPainValue2 = '{68}',LNumbness2 = '{69}',LNumbnessValue2 = '{70}',Other12 = '{71}',"
            + "Other1Flag2 = '{72}',Other1Value2 ='{73}',Other22 = '{74}',Other2Flag2 = '{75}',Other2Value2 = '{76}',Other32 = '{77}'"
            + ",Other3Flag2 = '{78}',Other3Value2 = '{79}',AnalgesicScore2 = '{80}',Nausea3 = '{81}',NauseaValue3 = '{82}',"
            + "Vomit3 = '{83}',VomitValue3 = '{84}',Headache3 = '{85}',HeadacheValue3 ='{86}',WaistlegsPain3 = '{87}',"
            + "WaistlegsPainValue3 = '{88}',URetention3 = '{89}',URetentionValue3 = '{90}',BPReduce3 = '{91}',BPReduceValue3 = '{92}',"
            + "BPElevated3 = '{93}',BPElevatedValue3 = '{94}',HHyperlipidemia3 = '{95}',HHyperlipidemiaValue3 = '{96}',BSuppression3 = '{97}',"
            + "BSuppressionValue3 = '{98}',ThroatPain3 = '{99}',ThroatPainValue3 = '{100}',LNumbness3 ='{101}',LNumbnessValue3 ='{102}',"
            + "Other13 ='{105}',Other1Flag3 ='{104}',Other1Value3 = '{105}',Other23 = '{106}',Other2Flag3 = '{107}',Other2Value3 = '{108}',"
            + "Other33 = '{109}',Other3Flag3 = '{110}',Other3Value3 ='{111}',AnalgesicScore3 = '{112}',UpdateDate = GETDATE() WHERE PatID = '{113}'";

        private static readonly string SQL_AFTERVISIT_UPDATE2 = "UPDATE Adims_AfterVisit2 WITH (ROWLOCK)  SET XinLv = '{1}',XueYa = '{2}',[HuXi]='{3}',"
            + "[YHTT]= '{4}',[SYSY]= '{5}', [EX] = '{6}',[OTu] = '{7}',[YShi]= '{8}',[TouTeng] = '{9}',[NCN] = '{10}',[TengTong]= '{11}' ,[HongZhong]= '{12}' ,"
            + "[SiZhiFeel]= '{13}',[Other]= '{14}',[YiSheng] = '{15}',[VisitTime]='{16}',[UpdateTime]= GETDATE() WHERE patID = '{0}' ";

        private static readonly string SQL_AFTERVISIT_UPDATE_SZ = "UPDATE Adims_AfterVisit_SZ WITH (ROWLOCK)  SET XinliZK= '{0}' ,BingfaZother= '{1}',"
            + "JKJY= '{2}',Taidu='{3}',Zerenxin= '{4}' ,Jishu= '{5}',ZonghePJ= '{6}',ZJAQWS= '{7}',Yijian= '{8}',Zerenhushi = '{9}',Vistor= '{10}', VisitDate= '{11}' ,"
            + "ShenZhi='{12}', JingShen='{13}',QiekouTT='{14}',PiFu='{15}',GouTong='{16}',WuguanHuati='{17}' where patid = '{18}'";
        #endregion

        #region <<麻醉总结>>
        private static readonly string SQL_mazuizongjie_INSERT = "INSERT INTO Adims_mzzongjie([shoushuHistory],[HeartClass],[gxyHistory],"
            + "[tnbHistory],[hxFunction],[gsFunction],[ydcgSpeed],[ydcgStatus],[ydcgPosition],[ydcgSee],[ydcgSelction],"
            + "[cghTingzhen],[cgRemark],[jzccPostural],[ymwccUp],[ymwccUpSelect],[ymwccUpLong],[ymwccDown],[ymwccDownSelect],"
            + "[ymwccDownLong],[zwmxqDrug],[zwmxqDosage],[zwmxqDiluteBy],[zwmxqDiluteDosage],[zwmxqDrugSpeed],[zwmxqDrugTime],[diguan],"
            + "[mzpmssStartUp],[mzpmssStartDown],[sjzzRemark],[jcsjzzSelect1],[jcsjzzSelect2],[jcsjzzSelectqc],[jcsjzzSelectsc],"
            + "[bcsjzzSelect],[OthersjzzSelect],[othersszz],[ccqk],[ccqkyc],[yaowu],[zzEffect],[Remark],[jmHocus],[MAC],[sjmcc],[ccdm],[ccdmOther],[ccdmRemark],[mafei],"
            + "[fentaini],[qumaduo],[bubikayin],[nailepin],[dingkayin],[fupailiduo],[zongliang],[chixujiliang],[PCA],[PCAtime],[mzSpecialSituation],[mzys],[mzjldID],patID,shunli,yigan,[AddTime])"
            + "VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}',"
            + "'{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}',"
            + "'{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}',"
            + "'{49}','{50}','{51}','{52}','{53}','{54}','{55}','{56}','{57}','{58}','{59}','{60}','{61}','{62}','{63}','{64}',GETDATE())";
        private static readonly string SQL_mazuizongjie_UPDATE = " UPDATE Adims_mzzongjie WITH (ROWLOCK) SET [shoushuHistory] = '{0}',[HeartClass] = '{1}'"
            + ",[gxyHistory] = '{2}',[tnbHistory] = '{3}',[hxFunction] = '{4}',[gsFunction] = '{5}',[ydcgSpeed] = '{6}',[ydcgStatus] = '{7}',[ydcgPosition] ='{8}'"
            + ",[ydcgSee] = '{9}',[ydcgSelction] = '{10}',[cghTingzhen] = '{11}',[cgRemark] = '{12}',[jzccPostural] = '{13}',[ymwccUp] = '{14}',[ymwccUpSelect] ='{15}'"
            + ",[ymwccUpLong] = '{16}',[ymwccDown] = '{17}',[ymwccDownSelect] = '{18}',[ymwccDownLong] = '{19}',[zwmxqDrug] = '{20}' ,[zwmxqDosage] = '{21}',[zwmxqDiluteBy] = '{22}'"
            + ",[zwmxqDiluteDosage] = '{23}',[zwmxqDrugSpeed] = '{24}',[zwmxqDrugTime] = '{25}',[diguan] ='{26}' ,[mzpmssStartUp] = '{27}',[mzpmssStartDown] = '{28}',[sjzzRemark] = '{29}'"
            + ",[jcsjzzSelect1] = '{30}',[jcsjzzSelect2] = '{31}',[jcsjzzSelectqc] = '{32}',[jcsjzzSelectsc] = '{33}',[bcsjzzSelect] = '{34}',[OthersjzzSelect] = '{35}',[othersszz] = '{36}'"
            + ",[ccqk] = '{37}',[ccqkyc] = '{38}',[yaowu] = '{39}',[zzEffect] = '{40}',[Remark] = '{41}',[jmHocus] = '{42}',[MAC] = '{43}',[sjmcc] ='{44}',[ccdm] = '{45}',[ccdmOther] = '{46}'"
            + ",[ccdmRemark] = '{47}',[mafei] = '{48}',[fentaini] = '{49}',[qumaduo] = '{50}',[bubikayin] = '{51}',[nailepin]='{52}',[dingkayin] = '{53}',[fupailiduo] = '{54}',[zongliang] = '{55}',[chixujiliang] = '{56}'"
            + ",[PCA] = '{57}',[PCAtime] = '{58}',[mzSpecialSituation] ='{59}' ,[mzys] = '{60}',[shunli] = '{63}',[yigan] = '{64}',[UpdateTime] =  GETDATE() WHERE [mzjldID]='{61}'";

        # endregion

        #region <<麻醉镇痛>>
        private static readonly string SQL_mazuizhentong_INSERT = "INSERT INTO Adims_mzzhentong(szFenTaini,szRuiFenTaini,lastMedic,ZhenTongFS,DGWZT,DGWZL,JMDGWZ,JiXing,MaFei"
            + ",FenTaiNi,ShuFenTaiNi,QuMaDuo,BuBiKaYin,LuoPaiKaYin,OtherMedic,ZongRongLiang,dayStart,dayEnd,FirstJL,ChiXuJL,PCAJL,SuoDingTime,DaoGuanFlag ,ManYiDu,mzjldID,someHour,remarkDay,AddTime)"
            + "VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}',GETDATE())";
        private static readonly string SQL_mazuizhentong_UPDATE = " UPDATE Adims_mzzhentong WITH (ROWLOCK) SET szFenTaini = '{0}',szRuiFenTaini = '{1}'"
            + ",lastMedic = '{2}',ZhenTongFS = '{3}',DGWZT = '{4}',DGWZL = '{5}',JMDGWZ = '{6}',JiXing = '{7}',MaFei ='{8}'"
            + ",FenTaiNi = '{9}',ShuFenTaiNi = '{10}',QuMaDuo = '{11}',BuBiKaYin = '{12}',LuoPaiKaYin = '{13}',OtherMedic = '{14}',ZongRongLiang ='{15}'"
            + ",dayStart = '{16}',dayEnd = '{17}',FirstJL = '{18}',ChiXuJL = '{19}',PCAJL = '{20}' ,SuoDingTime = '{21}',DaoGuanFlag = '{22}'"
            + ",ManYiDu = '{23}',someHour = '{25}', remarkDay = '{26}',  UpdateTime =  GETDATE() WHERE mzjldID='{24}'";

        # endregion

        #region <<护理记录>>

        private static readonly string SQL_NURSERECORD_GETLIST = "SELECT NurseRecordID,PatID,EnterTime,LeaveTime,PatAbouts,"
            + "AboutsValue,SPostural,PosturalValue,IsInfusion,InfusionSum,IsBlood,BWhole,BPlasma,BPlatelet,Cryoprecipitate,"
            + "BloodValue,SBefore,BeforeValue,SAfter,AfterValue,IsElectricKnife,PasteParts,PastePartsValue,IsTourniquet,"
            + "Position,PositionValue,HeatingDevice,HeatingValue,Indwelling,IsSample,SampleNum,IsSlice,SliceNum,IsDrainage,"
            + "DrainageValue,CreateDate,UpdateDate FROM Adims_NurseRecord WITH (NOLOCK) WHERE {0}";
        private static readonly string SQL_NURSERECORD_COUNT = "SELECT Count(*) FROM Adims_NurseRecord WHERE PatID = '{0}'";
        private static readonly string SQL_NURSERECORD_INSERT = "INSERT INTO Adims_NurseRecord(PatID,EnterTime,LeaveTime,"
            + "PatAbouts,AboutsValue,SPostural,PosturalValue,IsInfusion,InfusionSum,IsBlood,BWhole,"
            + "BPlasma,BPlatelet,Cryoprecipitate,BloodValue,SBefore,BeforeValue,SAfter,AfterValue,"
            + "IsElectricKnife,PasteParts,PastePartsValue,IsTourniquet,Position,PositionValue,"
            + "HeatingDevice,HeatingValue,Indwelling,IsSample,SampleNum,IsSlice,SliceNum,IsDrainage,"
            + "DrainageValue,CreateDate) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',"
            + "'{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}',"
            + "'{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}',GETDATE())";
        private static readonly string SQL_NURSERECORD_INSERT_SZ = "INSERT INTO Adims_NurseRecord_SZ(PatID,EnterTime,LeaveTime,"
         + "PatAbouts,AboutsValue,SPostural,PosturalValue,IsInfusion,InfusionSum,IsBlood,BWhole,"
         + "BPlasma,BPlatelet,Cryoprecipitate,BloodValue,SBefore,BeforeValue,SAfter,AfterValue,"
         + "IsElectricKnife,PasteParts,PastePartsValue,IsTourniquet,Position,PositionValue,"
         + "HeatingDevice,HeatingValue,Indwelling,IsSample,SampleNum,IsSlice,SliceNum,IsDrainage,"
         + "DrainageValue,CreateDate) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',"
         + "'{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}',"
         + "'{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}',GETDATE())";


        private static readonly string SQL_NURSERECORD_UPDATE = "UPDATE Adims_NurseRecord WITH (ROWLOCK)  SET EnterTime = '{0}',"
            + "LeaveTime = '{1}',PatAbouts = '{2}',AboutsValue = '{3}',SPostural = '{4}',PosturalValue = '{5}',"
            + "IsInfusion ='{6}',InfusionSum = '{7}',IsBlood = '{8}',BWhole = '{9}',BPlasma ='{10}',BPlatelet = '{11}',"
            + "Cryoprecipitate = '{12}',BloodValue = '{13}',SBefore = '{14}',BeforeValue = '{15}',SAfter = '{16}',"
            + "AfterValue = '{17}',IsElectricKnife = '{18}',PasteParts = '{19}',PastePartsValue = '{20}',IsTourniquet = '{21}',"
            + "Position = '{22}',PositionValue = '{23}',HeatingDevice = '{24}',HeatingValue = '{25}',Indwelling = '{26}',"
            + "IsSample = '{27}',SampleNum = '{28}',IsSlice = '{29}',SliceNum = '{30}',IsDrainage = '{31}',DrainageValue = '{32}',"
            + "UpdateDate = GETDATE()  WHERE PatID = '{33}'";

        #endregion

        #region <<麻醉记录单>>

        // 麻醉记录单查询
        private static readonly string SQL_MZJLD_GETLIST = "SELECT M.id AS MID,OT.patid,PatName,[Otime],ssmc,mzfa,ap1,patdpm,pattmd,szzd,patsex,"
            + "patage,os FROM Adims_mzjld AS M WITH (NOLOCK) RIGHT JOIN Adims_OTypesetting  AS OT WITH (NOLOCK) ON M.patid = OT.patid WHERE {0}";
        // 气体查询
        private static readonly string SQL_MZJLD_GETQTLIST = "SELECT qtname,yl,dw,sytime,jstime,flags FROM Adims_Qtuse WITH (NOLOCK) WHERE mzjldid='{0}'";
        // 液体查询
        private static readonly string SQL_MZJLD_GETYTLIST = "SELECT ytname,yl,dw,yyfs,cxyy,sytime,jstime,flags FROM Adims_Ytuse WITH (NOLOCK) WHERE mzjldid='{0}'";
        // 晶体查询
        private static readonly string SQL_MZJLD_GETJTLIST = "SELECT Cxyy,name,jl,dw,zrfs,kssj,jssj,flags FROM Adims_jmyUSE WITH (NOLOCK) WHERE mzjldid='{0}'";
        // 监护项目查询
        private static readonly string SQL_MZJLD_GETJHXMLIST = "SELECT lx,value,Otime FROM Adims_jhxm WITH (NOLOCK) WHERE mzjldid='{0}'";
        // 坐标点查询
        private static readonly string SQL_MZJLD_GETPOINTLIST = "SELECT lx,value,Otime FROM Adims_Point WITH (NOLOCK) WHERE mzjldid='{0}'";
        // 麻醉记录单明细
        private static readonly string SQL_MZJLD_GETSINGLE = "SELECT *  FROM Adims_Mzjld WHERE id = '{0}'";

        // 根据麻醉医师统计
        private static readonly string SQL_MZJLD_BYAP = "SELECT ap1,SUM(1) AS ssNum , CONVERT(NUMERIC(12,2),SUM(1)/CONVERT(NUMERIC(12,2),(SELECT COUNT(*) "
            + "FROM Adims_mzjld AS M WITH (NOLOCK) INNER JOIN Adims_OTypesetting  AS OT WITH (NOLOCK) ON M.patid = OT.patid WHERE {0}))) AS Probability "
            + "FROM Adims_mzjld AS M WITH (NOLOCK) INNER JOIN Adims_OTypesetting  AS OT WITH (NOLOCK) ON M.patid = OT.patid WHERE {1}  GROUP BY ap1";
        // 根据手术间统计
        private static readonly string SQL_MZJLD_BYSSJ = "SELECT Oroom,SUM(1) AS ssNum , CONVERT(NUMERIC(12,2),SUM(1)/CONVERT(NUMERIC(12,2),(SELECT COUNT(*) "
            + "FROM Adims_mzjld AS M WITH (NOLOCK) INNER JOIN Adims_OTypesetting  AS OT WITH (NOLOCK) ON M.patid = OT.patid WHERE {0}))) AS Probability "
            + "FROM Adims_mzjld AS M WITH (NOLOCK) INNER JOIN Adims_OTypesetting  AS OT WITH (NOLOCK) ON M.patid = OT.patid WHERE {1} GROUP BY Oroom";
        // 根据科室统计
        private static readonly string SQL_MAZLD_DPM = "SELECT patdpm,SUM(1) AS ssNum FROM Adims_mzjld AS M WITH (NOLOCK) INNER JOIN Adims_OTypesetting  AS OT WITH (NOLOCK) ON M.patid = OT.patid WHERE {0} GROUP BY patdpm";

        // 更新麻醉记录单
        private static readonly string SQL_MZJLD_UPDATE = "UPDATE Adims_Mzjld SET  sqzd= '{0}',szzd = '{1}', Nssss= '{2}',Yssss = '{3}',sqyy = '{4}',"
            + "mzfa = '{5}',mzys = '{6}', ssys = '{7}',XueXing = '{8}', mzxg = '{9}',qm = '{10}',[Otime] = GETDATE() WHERE id = '{11}'";



        #endregion

        #endregion



        #region <<Methods>>
        #region  锁屏间隔时间
        /// <summary>
        /// 查询间隔时间
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable SelectLockTime()
        {
            string countQJ = "SELECT LockTime from Adims_LockTime";
            return dBConn.GetDataTable(string.Format(countQJ));
        }

        /// <summary>
        /// 修改间隔时间
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateLockTime(int locktime)
        {

            string Insert = "update Adims_LockTime set LockTime='" + locktime + "'";
            return dBConn.ExecuteNonQuery(string.Format(Insert));
        }
        #endregion


        #region <<基础资料>>

        /// <summary>
        /// 获取病人信息
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetPat(string sqlWhere)
        {
            return dBConn.GetDataTable(string.Format("select patid 病人ID,patname 姓名,patage 年龄,patsex 性别,patdpm 病区,patbedno 床号,"
                + "oname 拟实施手术,pattmd 主要诊断 FROM Adims_OTypesetting WITH (NOLOCK) where {0}", sqlWhere));
        }
        public DataTable GetSHPat()
        {
            return dBConn.GetDataTable(string.Format("select patid 病人ID,[patName]  姓名 from [Adims_SHXJ]"));
        }
        public DataTable GetSHPatinfo(string brname)
        {
            return dBConn.GetDataTable(string.Format("select *   from [Adims_SHXJ] where patName='" + brname + "'"));
        }
        /// <summary>
        /// 修改术后小节
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateSHXJ(string name, string neirong)
        {
            string update = "update [Adims_SHXJ] set [shxjInfo] ='" + neirong + "' where  patName='" + name + "' ";
            return dBConn.ExecuteNonQuery(string.Format(update));
        }

        public int InsertOperName(string OperNo, string OperName, string NameSuoxie)
        {
            string update = "Insert into OperationName(OperNo,ONname,OSpell) Values('" + OperNo + "','" + OperName + "','" + NameSuoxie + "')";
            return dBConn.ExecuteNonQuery(string.Format(update));
        }
        public int InsertShoushuYisheng(string nameNo, string name, string NameSuoxie)
        {
            string update = "Insert into ShoushuYisheng(nameNo,name,NameSuoxie) Values('" + nameNo + "','" + name + "','" + NameSuoxie + "')";
            return dBConn.ExecuteNonQuery(string.Format(update));
        }
        public DataTable GetShoushuYisheng(string suoxie)
        {
            string update = "select name,nameNo,NameSuoxie  from ShoushuYisheng where NameSuoxie like'" + suoxie + "%'";
            return dBConn.GetDataTable(string.Format(update));
        }
        public DataTable GetShoushuYishengNo(string name)
        {
            string update = "select nameNo from ShoushuYisheng where Name='" + name + "'";
            return dBConn.GetDataTable(string.Format(update));
        }
        /// <summary>
        /// 查询工作人员
        /// </summary>
        /// <returns></returns>
        public DataTable GetSurgeryStaff(string sqlWhere)
        {
            return dBConn.GetDataTable(string.Format(SQL_SURGERYSTAFF_GETLIST, sqlWhere));
        }


        /// <summary>
        /// 查询所有麻醉医生
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllMZYS()
        {
            string selectAllYS = " SELECT user_name FROM Adims_user where type='1'";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }
        /// <summary>
        /// 查询所有手术医生
        /// </summary>
        /// <returns></returns>
        public DataTable GetAll_shoushuyisheng()
        {
            string selectAllYS = " SELECT MedicalName FROM Adims_SurgeryStaff where PostType='1'";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }
        /// <summary>
        /// 查询所有护士
        /// </summary>
        /// <returns></returns>
        public DataTable GetAll_hushi()
        {
            string selectAllYS = " SELECT user_name FROM Adims_user where Type='2'";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }
        /// <summary>
        /// 根据时间查询手术总量
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllMZLbyTime(DateTime dt1, DateTime dt2)
        {
            string selectbyTime = "select count(*) from Adims_Mzjld  WHERE   Otime between '" + dt1 + "'and'" + dt2 + "'";
            return dBConn.GetDataTable(string.Format(selectbyTime));
        }

        /// <summary>
        /// 根据医生查询手术总量
        /// </summary>
        /// <returns></returns>
        public DataTable GetYSMZLbyTime(int sunall, DateTime dt1, DateTime dt2, string name, string address)
        {
            string selectysbyTime = "SELECT sum(DATEDIFF ( hh , mzkssj , mzjssj )),count(*),SUBSTRING(cast(count(*)*100.0/" + sunall + "  as nvarchar),1,4) +' %' from Adims_Mzjld ,Adims_OTypesetting"
                                + " WHERE   Otime between '" + dt1 + "'and'" + dt2 + "' and mzys like '%" + name + "%' and Adims_OTypesetting.operaddress='" + address + "' and  Adims_OTypesetting.patid=Adims_Mzjld.patid";
            return dBConn.GetDataTable(string.Format(selectysbyTime));
        }
        /// <summary>
        /// 麻醉护士
        /// </summary>
        /// <param name="sunall"></param>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataTable GetHSMZLbyTime(int sunall, DateTime dt1, DateTime dt2, string name, string address)
        {
            string selectysbyTime = "SELECT sum(DATEDIFF ( hh , mzkssj , mzjssj )),count(*),SUBSTRING(cast(count(*)*100.0/" + sunall + "  as nvarchar),1,4) +' %' from Adims_Mzjld,Adims_OTypesetting"
                                + " WHERE   Otime between '" + dt1 + "'and'" + dt2 + "' and (qxhs like '%" + name + "%' or xhhs like '%" + name + "%')  and Adims_OTypesetting.operaddress='" + address + "' and  Adims_OTypesetting.patid=Adims_Mzjld.patid";
            return dBConn.GetDataTable(string.Format(selectysbyTime));
        }


        /// <summary>
        /// 根据手术间查询手术总量
        /// </summary>
        /// <returns></returns>
        public DataTable GetYSMZLbyOroom(int sunall, DateTime dt1, DateTime dt2, string name)
        {
            //string selectysbyTime = "SELECT sum(DATEDIFF ( mi , mzkssj , mzjssj )),count(*),count(*)/'" + sunall + "' from Adims_Mzjld as M join Adims_OTypesetting as O on O.PatID=M.PatID"
            //                    + " WHERE   time between '" + dt1 + "'and'" + dt2 + "' and O.Oroom like '%" + name + "%' ";
            string selectysbyTime = "SELECT sum(DATEDIFF ( hh , mzkssj , mzjssj )) as mzsj,count(*) as ssNum,SUBSTRING(cast(count(*)*100.0/" + sunall + " as nvarchar),1,4) +' %' as Probability  from Adims_Mzjld as M join Adims_OTypesetting as O on O.PatID=M.PatID WHERE   Otime between '" + dt1 + "'and'" + dt2 + "' and O.Oroom like '%" + name + "%' ";
            return dBConn.GetDataTable(string.Format(selectysbyTime));
        }

        /// <summary>
        /// 根据科室查询手术总量
        /// </summary>
        /// <returns></returns>
        public DataTable GetYSMZLbyOKeshi(int sunall, DateTime dt1, DateTime dt2, string name)
        {
            string selectysbyTime = "SELECT sum(DATEDIFF ( hh , mzkssj , mzjssj )),SUBSTRING(cast(count(*)*100.0/" + sunall + "  as nvarchar),1,4) +' %' from Adims_Mzjld as M join Adims_OTypesetting as O on O.PatID=M.PatID"
                                + " WHERE   Otime between '" + dt1 + "'and'" + dt2 + "' and O.Patdpm like '%" + name + "%' ";
            return dBConn.GetDataTable(string.Format(selectysbyTime));
        }
        public DataTable GetYSMZLbyOKeshi1(DateTime dt1, DateTime dt2, string address)
        {
            string selectysbyTime = "select ao.patdpm, sum(DATEDIFF ( hh , mzkssj , mzjssj )),count(*) from Adims_OTypesetting as ao join Adims_Mzjld as am on ao.patid=am.patid "
                                    + " WHERE  Convert(nvarchar, otime,23) between '" + dt1.ToString("yyyy-MM-dd") + "'and'" + dt2.ToString("yyyy-MM-dd") + "' and ao.operaddress='" + address + "' group by ao.Patdpm ";
            return dBConn.GetDataTable(string.Format(selectysbyTime));
        }

        /// <summary>
        /// 根据表名查询对应的基础信息
        /// </summary>
        /// <param name="dtName"></param>
        /// <returns></returns>
        public DataTable GetMasterList(string dtName)
        {
            return dBConn.GetDataTable(string.Format(SQL_MASTER_LIST, dtName));
        }

        #endregion

        #region <<员工排班>>

        /// <summary>
        /// 查询员工排班
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetEmployeesList(string sqlWhere)
        {
            return dBConn.GetDataTable(string.Format(SQL_EMPLOYEES_GETLIST, sqlWhere));
        }
        /// <summary>
        /// 验证是否已经排班
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetIsEmployeesList(string divis, string time)
        {
            string select = "select * FROM Adims_Employees  where [Time]='" + time + "'and Divisions='" + divis + "'";
            return dBConn.GetDataTable(string.Format(select));
        }

        /// <summary>
        /// 新增员工排班
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertEmployees(Dictionary<string, string> dictionary)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_EMPLOYEES_INSERT, dictionary.Values.ToArray()));
        }

        /// <summary>
        /// 修改员工排班
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateEmployees(Dictionary<string, string> dictionary)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_EMPLOYEES_UPDATE, dictionary.Values.ToArray()));
        }

        /// <summary>
        /// 删除员工排班
        /// </summary>
        /// <param name="employeesID"></param>
        /// <returns></returns>
        public int DeleteEmployees(string employeesID)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_EMPLOYEES_DELETE, employeesID));
        }

        #endregion

        #region <<请假登记>>

        /// <summary>
        /// 查询请假登记
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetLeaveRegistrationList(string sqlWhere)
        {
            return dBConn.GetDataTable(string.Format(SQL_LEAVEREGISTRATION_GETLIST, sqlWhere));
        }


        /// <summary>
        /// 查询是否已经请假
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable SlectInHoliday(string bianhao, int dt1, int dt2)
        {
            string countQJ = "SELECT count(*) from [Adims_LeaveRegistration] where [EmployeesNo]='" + bianhao + "' and  StartDate>='" + dt1 + "' and EndDate<='" + dt2 + "' ";
            return dBConn.GetDataTable(string.Format(countQJ));
        }
        /// <summary>
        /// 新增请假登记
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertLeaveRegistration(List<string> dictionary)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_LEAVEREGISTRATION_INSERT, dictionary.ToArray()));
        }

        /// <summary>
        /// 修改请假登记
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateLeaveRegistration(List<string> dictionary)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_LEAVEREGISTRATION_UPDATE, dictionary.ToArray()));
        }

        /// <summary>
        /// 删除请假登记
        /// </summary>
        /// <param name="employeesID"></param>
        /// <returns></returns>
        public int DeleteLeaveRegistration(string employeesID)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_LEAVEREGISTRATION_DELETE, employeesID));
        }

        #endregion

        #region <<器械清点>>

        /// <summary>
        /// 查询所有器械模板名
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetallQxModel()
        {
            string selct = "select modelname from Adims_qxModelname";
            return dBConn.GetDataTable(string.Format(selct));
        }

        /// <summary>
        /// 查询所有器械名称集合
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetallQxmc()
        {
            string selct = "select * from qxmcTable";
            return dBConn.GetDataTable(string.Format(selct));
        }

        /// <summary>
        /// 查询是否已经存在清点模板
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable SlectInqxModel(string mod)
        {
            string countQJ = "SELECT count(*) from [Adims_qxqdModel] where [Model]='" + mod + "'";
            return dBConn.GetDataTable(string.Format(countQJ));
        }
        public DataTable SlectqxModel(string mod)
        {
            string countQJ = "SELECT * from adims_qxModelName where [ModelName]='" + mod + "'";
            return dBConn.GetDataTable(string.Format(countQJ));
        }
        /// <summary>
        /// 查询器械是否存在
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetIsInQxmc(string QXMC)
        {
            string selct = "select * from qxmcTable where qxmc='" + QXMC + "'";
            return dBConn.GetDataTable(string.Format(selct));
        }
        /// <summary>
        /// 查询器械模板详细数据
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable SelectqxmcInmodel(string mod)
        {
            string countQJ = "SELECT qxmc,qxCount,id,model from [Adims_qxqdModel] where [Model]='" + mod + "' order by id desc";
            return dBConn.GetDataTable(string.Format(countQJ));
        }

        /// <summary>
        /// 新增器械清点模板
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertqxqdModel(string QXmc, string mod, int count)
        {

            string Insert = "Insert into Adims_qxqdModel(qxmc,Model,qxCount) values('" + QXmc + "','" + mod + "','" + count + "') ";
            return dBConn.ExecuteNonQuery(string.Format(Insert));
        }
        /// <summary>
        /// 修改器械
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateqxqdModel(string QXmc, string mod, int count, int id)
        {

            string Insert = "update Adims_qxqdModel set qxmc='" + QXmc + "', Model='" + mod + "', qxCount='" + count + "' where id='" + id + "'";
            return dBConn.ExecuteNonQuery(string.Format(Insert));
        }
        /// <summary>
        /// 新增器械
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertQxmc(string QXmc, string mcsx)
        {
            string Insert = "Insert into qxmcTable(qxmc,qxmcsx) values('" + QXmc + "','" + mcsx + "') ";
            return dBConn.ExecuteNonQuery(string.Format(Insert));
        }
        /// <summary>
        /// 删除器械
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int deleteQxmc(int id)
        {
            string Insert = "delete  from  Adims_qxqdModel where id='" + id + "' ";
            return dBConn.ExecuteNonQuery(string.Format(Insert));
        }
        /// <summary>
        /// 修改器械清点模板
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateqxqdModel(List<string> dictionary)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_LEAVEREGISTRATION_UPDATE, dictionary.ToArray()));
        }

        /// <summary>
        /// 删除器械模板
        /// </summary>
        /// <param name="employeesID"></param>
        /// <returns></returns>
        public int DeleteqxqdModel(string employeesID)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_LEAVEREGISTRATION_DELETE, employeesID));
        }
        /// <summary>
        /// 删除器械
        /// </summary>
        /// <param name="employeesID"></param>
        /// <returns></returns>
        public int Deleteqxmc(string QXMC)
        {
            string delete = "delete from qxmcTable where qxmc='" + QXMC + "'";
            return dBConn.ExecuteNonQuery(string.Format(delete));
        }

        #endregion

        #region 反馈HIS方法
        /// <summary>
        /// 根据员工名字返回编号
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public string SelectUserNo(string username)
        {
            string userno = "";
            string countQJ = "SELECT UserNo from Adims_User where user_name ='" + username + "'";
            DataTable dt = dBConn.GetDataTable(string.Format(countQJ));
            if (dt.Rows.Count > 0)
            {
                userno = dt.Rows[0][0].ToString();
            }
            return userno;
        }
        /// <summary>
        /// 查询手术排版信息
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable SelectOperConfirm(string patid)
        {
            string countQJ = "SELECT Patid as ApplyID,convert(nvarchar,odate,23) as ApplyDate,ReplyDate,oroom as OPPlaceName,"
                            + "second as OPNo,ap1 as EmpID_Aisth,on1 as EmpID_Tool,sn1 as EmpID_Tour from Adims_OTypesetting where patid='" + patid + "'";
            return dBConn.GetDataTable(string.Format(countQJ));
        }
        /// <summary>
        /// 查询手术结束信息
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable SelectOperEnd(string patid)
        {
            string countQJ = "SELECT patid as ApplyID,otime as PatiArrDate,otime as PatiBackDate,mzkssj as AisthBeginDate,"
                        + "mzjssj as AisthEndDate,sskssj as OPBeginDate,ssjssj as OPEndDate from Adims_Mzjld  where patid='" + patid + "'";
            return dBConn.GetDataTable(string.Format(countQJ));
        }


        #endregion

        #region <<加班登记>>

        /// <summary>
        /// 查询加班登记
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetJBJLList(string sqlWhere)
        {
            return dBConn.GetDataTable(string.Format(SQL_JBJL_GETLIST, sqlWhere));
        }

        /// <summary>
        /// 新增加班登记
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertJBJL(List<string> dictionary)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_JBJL_INSERT, dictionary.ToArray()));
        }

        /// <summary>
        /// 修改加班登记
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateJBJL(List<string> dictionary)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_JBJL_UPDATE, dictionary.ToArray()));
        }

        /// <summary>
        /// 删除加班登记
        /// </summary>
        /// <param name="employeesID"></param>
        /// <returns></returns>
        public int DeleteJBJL(string employeesID)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_JBJL_DELETE, employeesID));
        }

        #endregion

        #region <<智能排班>>

        /// <summary>
        /// 查询手术排班
        /// </summary>
        /// <param name="oroom"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        /// 
        public DataTable GetOTypesetting(string oroom, string dt)
        {
            string sqlWhere = "CONVERT(varchar, Odate , 23 ) <= '" + dt + "'";
            if (oroom != "全部手术间")
                sqlWhere += " AND oroom = '" + oroom + "'";
            return dBConn.GetDataTable(string.Format(SQL_OTYPESETTING_SELECT, sqlWhere));
        }

        public void UpdateOTypesetting(string oroom, string dt, object[] doctors)
        {
            string sqlWhere = "CONVERT(varchar, Odate , 23 ) = '" + dt + "'";
            if (oroom != "全部手术间")
                sqlWhere += " AND oroom = '" + oroom + "'";
            string sql = string.Format(SQL_OTYPESETTING_SELECT, sqlWhere);
            SqlConnection con = new SqlConnection(connStr);
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable mydt = new DataTable();
            mydt = ds.Tables[0];
            for (int i = 0; i < mydt.Rows.Count; i++)
            {
                mydt.Rows[i][12] = doctors[i];
            }
            adp.Update(mydt);

        }


        /// <summary>
        /// 保存排版信息
        /// </summary>
        /// <param name="pb"></param>
        /// <returns></returns>
        public int SaveOTypesetting(adims_MODEL.OldPBModel pb)
        {
            return dBConn.ExecuteNonQuery("INSERT INTO Adims_OTypesetting(patid,oname,oroom,second,odepartment,olevel,"
                + "amethod,gl,jz,ap1,ap2,ap3,aa1,aa2,aa3,os,oa1,oa2,oa3,oa4,tp,on1,on2,sn1,sn2,sn3,remarks,ostate,"
                + "odate)values('" + pb.Patid + "','" + pb.Oname + "','" + pb.Oroom + "','" + pb.Second + "','"
                + pb.Odepartment + "','" + pb.Olevel + "','" + pb.Amethod + "','" + pb.Gl + "','" + pb.Jz + "','"
                + pb.Ap1 + "','" + pb.Ap2 + "','" + pb.Ap3 + "','" + pb.Aa1 + "','" + pb.Aa2 + "','" + pb.Aa3
                + "','" + pb.Os + "','" + pb.Oa1 + "','" + pb.Oa2 + "','" + pb.Oa3 + "','" + pb.Oa4 + "','"
                + pb.Tp + "','" + pb.On1 + "','" + pb.On2 + "','" + pb.Sn1 + "','" + pb.Sn2 + "','" + pb.Sn3
                + "','" + pb.Remarks + "','" + pb.Ostate + "','" + pb.Odate + "')");
        }

        /// <summary>
        /// 更新排班信息
        /// </summary>
        /// <param name="patID"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int UpdateOTypesetting(string patID, string column, string value)
        {
            string sql = "UPDATE Adims_OTypesetting WITH (ROWLOCK) SET " + column + " = '" + value + "' WHERE patid = '" + patID + "'";
            return dBConn.ExecuteNonQuery(sql);
        }

        #endregion
        #region <<术前访视>>

        /// <summary>
        /// 查询术前访视清单
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetBeforeVisitList(string sqlWhere)
        {
            return dBConn.GetDataTable(string.Format(SQL_BEFOREVISIT_GETLIST, sqlWhere));
        }

        /// <summary>
        /// 查询访视信息
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetBeforeVisit_HS(string patid)
        {
            string sql = "SELECT * from Adims_BeforeVisit_HS  where patid='" + patid + "'";

            return dBConn.GetDataTable(sql);
        }

        public DataTable GetBeforeVisit_YS(string patid)
        {
            string sql = "SELECT * from Adims_BeforeVisit_YS  where patid='" + patid + "'";

            return dBConn.GetDataTable(sql);
        }
        public DataTable GetAfterVisit_YS(string patid)
        {
            string sql = "SELECT * from Adims_AfterVisit_YS  where patid='" + patid + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 判断病人访视信息是否存在
        /// </summary>
        /// <param name="patID"></param>
        /// <returns></returns>
        public bool GetBeforeVisitCount(string patID)
        {

            int result = Convert.ToInt32(dBConn.ExecuteScalar(string.Format(SQL_BEFOREVISIT_COUNT, patID)));

            if (result == 1) return true; else return false;
        }


        /// <summary>
        /// 添加术前访视
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertBeforeVisit(Dictionary<string, string> dictionary)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_BEFOREVISIT_INSERT, dictionary.Values.ToArray()));
        }
        /// <summary>
        /// 添加术前访视(医生)
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertBeforeVisit_YS(Dictionary<string, string> dictionary)
        {
            string SQL_BEFOREVISIT_INSERT_YS = "INSERT INTO Adims_BeforeVisit_YS(Shoushushi,MZFF ,MZFF_Ci,Xuezhipin ,Xiyan  ,XiyanYear,Xiyanliang ,JieyanDay ,Xiaochuan ,XiaochuanChuli,Ganmao ,GanmaoDay,Kesou ,Dahu,"
            + "Tilihuodong,Xiongmen,Fangshe,Huanjie,Gaoxueya ,GXY_High_down ,GXY_High_up,GXY_Low_down,GXY_Low_up,XueyaUnderDown  ,XueyaUnderUp  ,PJXueyaDown,PJXueyaUp ,Sizhi,Jingshenbing,Yunjue   ,Qinguanyan ,TangniaoB ,Yinshi ,Kuiyang12,"
            + "Yinjiu ,ChuxueQX,Guomin,Guomingyao ,Guominshiwu,Guominwu ,Jinqifuyao ,JinqifuyaoOther,Yaotong ,Jingqi,Huaiyun ,YingErCS ,YEhuodong,Koucunfazi,QSMZS ,QSMZS_Other ,Remark ,Tongkong ,Kaikoudu,Mallampati ,ToujingHD ,QiguanJZ,YA ,HuodongYA ,YSS_YA ,VisitDate,patid,MZYS,ShoushushiName,IsRead)"
           + " VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}',"
           + "'{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}','{51}','{52}','{53}','{54}','{55}','{56}','{57}','{58}','{59}','{60}','{61}','{62}','0')";

            return dBConn.ExecuteNonQuery(string.Format(SQL_BEFOREVISIT_INSERT_YS, dictionary.Values.ToArray()));
        }

        public int InsertAfterVisit_YS(Dictionary<string, string> dictionary)
        {
            string sql = "INSERT INTO Adims_AfterVisit_YS(XueyaDown,XueyaUp,Xinlv,Huxi,Yishi,YanhouTT,SYsiya,Exin,Outu,NiaoCL,SizhiJL,ChuanCD,MZXG,Other,YishengAfter,VisitDateAfter,patID)"
           + " VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}')";
            sql = string.Format(sql, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 添加术前访视(护士)
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertBeforeVisit_HS(Dictionary<string, string> dictionary)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_BEFOREVISIT_INSERT_HS, dictionary.Values.ToArray()));
        }

        /// <summary>
        /// 更新术前访视
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateBeforeVisit(Dictionary<string, string> dictionary)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_BEFOREVISIT_UPDATE, dictionary.Values.ToArray()));
        }
        /// <summary>
        /// 更新术前访视(护士）
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateBeforeVisit_HS(Dictionary<string, string> dictionary)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_BEFOREVISIT_UPDATE_HS, dictionary.Values.ToArray()));
        }
        /// <summary>
        /// 更新术前访视(护士）存档状态
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateBeforeVisit_HS_STATE(int IsNum)
        {
            string sql = "UPDATE Adims_BeforeVisit_HS  SET IsRead='" + IsNum + "'";
            return dBConn.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新术前访视(医生）
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateBeforeVisit_YS(Dictionary<string, string> dictionary)
        {
            string SQL_BEFOREVISIT_UPDATE_YS = "UPDATE Adims_BeforeVisit_YS WITH (ROWLOCK) SET Shoushushi = '{0}',MZFF = '{1}' ,MZFF_Ci = '{2}',Xuezhipin  = '{3}',Xiyan = '{4}',XiyanYear = '{5}',Xiyanliang = '{6}' ,JieyanDay = '{7}' ,Xiaochuan = '{8}' ,XiaochuanChuli = '{9}',Ganmao = '{10}' ,GanmaoDay = '{11}',Kesou = '{12}' ,Dahu = '{13}',"
            + "Tilihuodong = '{14}',Xiongmen = '{15}',Fangshe = '{16}',Huanjie= '{17}',Gaoxueya= '{18}' ,GXY_High_down= '{19}' ,GXY_High_up= '{20}',GXY_Low_down= '{21}',GXY_Low_up= '{22}',XueyaUnderDown= '{23}'  ,XueyaUnderUp= '{24}'  ,PJXueyaDown= '{25}',PJXueyaUp = '{26}',Sizhi= '{27}',Jingshenbing= '{28}',Yunjue= '{29}' ,Qinguanyan= '{30}' ,TangniaoB= '{31}' ,Yinshi = '{32}',Kuiyang12= '{33}',"
            + "Yinjiu= '{34}' ,ChuxueQX= '{35}',Guomin= '{36}',Guomingyao= '{37}' ,Guominshiwu= '{38}',Guominwu= '{39}' ,Jinqifuyao= '{40}' ,JinqifuyaoOther= '{41}',Yaotong= '{42}' ,Jingqi= '{43}',Huaiyun= '{44}' ,YingErCS= '{45}' ,YEhuodong= '{46}',Koucunfazi= '{47}',QSMZS= '{48}' ,QSMZS_Other= '{49}' ,Remark= '{50}' ,Tongkong= '{51}' ,Kaikoudu= '{52}',Mallampati= '{53}' ,ToujingHD = '{54}',QiguanJZ= '{55}',"
            + "YA= '{56}' ,HuodongYA= '{57}' ,YSS_YA = '{58}',VisitDate= '{59}',MZYS='{61}',ShoushushiName='{62}',IsRead='0' where patid= '{60}'";

            return dBConn.ExecuteNonQuery(string.Format(SQL_BEFOREVISIT_UPDATE_YS, dictionary.Values.ToArray()));
        }
        public int UpdateAfterVisit_YS(Dictionary<string, string> dictionary)
        {

            string sql = "UPDATE Adims_AfterVisit_YS WITH (ROWLOCK) SET XueyaDown = '{0}',XueyaUp = '{1}' ,Xinlv = '{2}',HUXI  = '{3}',Yishi = '{4}',YanhouTT = '{5}',"
            + "SYsiya = '{6}' ,Exin = '{7}' ,Outu = '{8}' ,NiaoCL = '{9}',ChuanCD = '{10}' ,SizhiJL = '{11}',MZXG = '{12}' ,Other = '{13}',YishengAfter = '{14}',VisitDateAfter = '{15}' where patid= '{16}'";
            sql = string.Format(sql, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 更新术前访视(医生）存档状态
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateBeforeVisit_YS_STATE(string patid)
        {
            string sql = "UPDATE Adims_BeforeVisit_YS  SET IsRead='1' where patid='" + patid + "' ";
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 更新术前访视(医生）存档状态
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateMZZJ_STATE(string mzid)
        {
            string sql = "UPDATE Adims_mzzongjie_SZ  SET IsRead='1' where mzjldid='" + mzid + "' ";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int UpdateMZJHS_state(string PATID)
        {
            string sql = "UPDATE Adims_MZJHS  SET IsRead='1' where PATID='" + PATID + "' ";
            return dBConn.ExecuteNonQuery(sql);
        }
        #endregion
        #region <<麻醉总结>>

        /// <summary>
        /// 麻醉总结名单查询
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetmazuizongjieList(string dt, string operAddress)
        {
            string sql = "select  A.id ,B.patid,B.patname,B.patzhuyuanid,B.Patbedno,B.patsex,B.patage from Adims_Mzjld as A LEFT join "
                    + "Adims_OTypesetting AS B ON A.PATID=B.PATID where CONVERT(varchar, A.Otime , 23 ) = '" + dt + "' and B.operAddress='" + operAddress + "' and B.Ostate>='0'";
            return dBConn.GetDataTable(sql);
        }

        /// <summary>
        /// 验证是否存在病人麻醉总结
        /// </summary>
        /// <param name="patID">病人ID</param>
        /// <returns></returns>
        public DataTable GetmazuizongjieCount(string mzID)
        {
            string sql = "select * from Adims_mzzongjie where mzjldID='" + mzID + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 验证是否存在病人麻醉总结
        /// </summary>
        /// <param name="patID">病人ID</param>
        /// <returns></returns>
        public DataTable GetMZJLD_Info(string mzID)
        {
            string sql = "select * from Adims_mzjld where id='" + mzID + "'";
            return dBConn.GetDataTable(sql);
        }

        public DataTable GetMZJLDbyPatID(string patId)
        {
            string sql = "select * from Adims_mzjld where patId='" + patId + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 验证是否存在病人麻醉镇痛
        /// </summary>
        /// <param name="patID">病人ID</param>
        /// <returns></returns>
        public DataTable GetmzzhentongCount(string mzID)
        {
            string sql = "select * from Adims_mzzhentong where mzjldID='" + mzID + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetUserNoByName(string userName)
        {
            string sql = "select UserNo from Adims_User where user_name='" + userName + "'";
            return dBConn.GetDataTable(sql);
        }
        public object GetOstate(string patid)
        {
            string sql = "select ostate FROM Adims_OTypesetting where patid='" + patid + "'";
            return dBConn.ExecuteScalar(sql);
        }
        /// <summary>
        /// 验证是否存在病人麻醉镇痛info
        /// </summary>
        /// <param name="patID">病人ID</param>
        /// <returns></returns>
        public DataTable GetmzzhentonginfoCount(string mzID, string type)
        {
            string sql = "select * from Adims_mzzhengtongInfo where mzjldID='" + mzID + "'and dataType='" + type + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetMZZJ_SZ(string mzID)
        {
            string sql = "select * from Adims_mzzongjie_SZ where mzjldID='" + mzID + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetMZJHS(string patid)
        {
            string sql = "select * from Adims_MZJHS where PATID='" + patid + "'";
            return dBConn.GetDataTable(sql);
        }

        /// <summary>
        ///查询麻醉镇痛信息
        /// </summary>
        /// <param name="patID">病人ID</param>
        /// <returns></returns>
        public DataTable Getmzzhentonginfo(string mzID)
        {
            string sql = "select DataType,ChuShi,SomeHour,FirstDay,SecondDay,RemarkDay from Adims_mzzhengtongInfo where mzjldID='" + mzID + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 添加麻醉总结
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int Insertmazuizongjie(Dictionary<string, string> dictionary)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_mazuizongjie_INSERT, dictionary.Values.ToArray()));

        }

        /// <summary>
        /// 添加麻醉镇痛
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int Insertmazuizhentong(Dictionary<string, string> dictionary)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_mazuizhentong_INSERT, dictionary.Values.ToArray()));

        }
        /// <summary>
        /// 添加麻醉镇痛2
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertmazuizhentongINFO(string mzid, string type, string chushi, string SomeHour, string FirstDay, string SecondDay, string remarkday)
        {
            string sql = "INSERT INTO Adims_mzzhengtongInfo(mzjldID,DataType,ChuShi,SomeHour,FirstDay,SecondDay,RemarkDay) VALUES ('" + mzid + "',"
            + "'" + type + "','" + chushi + "','" + SomeHour + "','" + FirstDay + "','" + SecondDay + "','" + remarkday + "')";
            return dBConn.ExecuteNonQuery(sql);

        }
        /// <summary>
        /// 更新麻醉总结
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int Updatemazuizongjie(Dictionary<string, string> dictionary)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_mazuizongjie_UPDATE, dictionary.Values.ToArray()));
        }
        /// <summary>
        /// 添加麻醉总结
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int Insertmazuizongjie_SZ(Dictionary<string, string> dictionary)
        {
            string _INSERT = "INSERT INTO Adims_mzzongjie_SZ(ZGchuanci ,ZGyingmowai  ,ZGxiaoguo ,ZGmazuipingmian  ,QGcaozuo,"
                + "QGfuzhu ,QGtongqi ,DMchuanci ,SJMchuanci ,Remark ,Yishi ,XueyaDown ,XueyaUp , Xinlv ,Huxilv  ,HuxiSPO2 ,Xiyang ,Tiwei ,"
               + "DMCCchaichu ,DMCCchaichuweizhi  ,ZhentongB  ,Jianyi  ,Yuanyin  ,Tongyishu  ,MZYS  ,BingfangRenyuan ,UpdateTime ,backtime,mzjldid,patid)"
               + " values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}',"
              + "'{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}')";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);

        }
        public int InsertMZJHS(Dictionary<string, string> dictionary)
        {
            string _INSERT = "INSERT INTO Adims_MZJHS(ASA,NXMZFF,SJMZFF,MZFFBGYY,JHXM,ZGNMZCCD,TSCZ ,MZQX ,JMY,ZJY,ZTY,FZYY,JJYP,Shuye,Hongxibao,Xuejiang,Wentiduice,MZYS,RecordTime,patid)"
               + " values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}')";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);

        }

        /// <summary>
        /// 更新麻醉总结
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int Updatemazuizongjie_SZ(Dictionary<string, string> dictionary)
        {
            string upMZZJ = "UPDATE Adims_mzzongjie_SZ WITH (ROWLOCK) SET ZGchuanci ='{0}',ZGyingmowai ='{1}' ,ZGxiaoguo = '{2}',ZGmazuipingmian ='{3}' ,QGcaozuo = '{4}',QGfuzhu = '{5}',QGtongqi = '{6}',DMchuanci = '{7}',SJMchuanci = '{8}',Remark = '{9}',Yishi ='{10}' ,XueyaDown = '{11}',XueyaUp ='{12}' "
                + ",Xinlv = '{13}',Huxilv ='{14}' ,HuxiSPO2 = '{15}',Xiyang ='{16}',Tiwei = '{17}',DMCCchaichu = '{18}',DMCCchaichuweizhi ='{19}' ,ZhentongB ='{20}' ,Jianyi ='{21}' ,Yuanyin ='{22}' ,Tongyishu ='{23}' ,MZYS ='{24}' ,BingfangRenyuan = '{25}',UpdateTime ='{26}',backtime ='{27}' WHERE mzjldID ='{28}' ";
            return dBConn.ExecuteNonQuery(string.Format(upMZZJ, dictionary.Values.ToArray()));
        }
        public int UpdateMZJHS(Dictionary<string, string> dictionary)
        {
            string upMZZJ = "UPDATE Adims_MZJHS WITH (ROWLOCK) SET ASA ='{0}',NXMZFF ='{1}' ,SJMZFF = '{2}',MZFFBGYY ='{3}' ,JHXM = '{4}',ZGNMZCCD = '{5}',TSCZ = '{6}',MZQX = '{7}',JMY = '{8}',ZJY = '{9}',ZTY ='{10}' ,FZYY = '{11}',JJYP ='{12}' "
                + ",Shuye = '{13}',Hongxibao ='{14}' ,Xuejiang = '{15}',Wentiduice ='{16}',MZYS = '{17}',RecordTime = '{18}' WHERE PATID ='{19}' ";
            return dBConn.ExecuteNonQuery(string.Format(upMZZJ, dictionary.Values.ToArray()));
        }
        /// <summary>
        /// 更新麻醉镇痛
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int Updatemazuizhentong(Dictionary<string, string> dictionary)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_mazuizhentong_UPDATE, dictionary.Values.ToArray()));
        }
        /// <summary>
        /// 更新麻醉镇痛信息
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdatemazuizhentongInfo(string mzid, string type, string chushi, string SomeHour, string FirstDay, string SecondDay, string remarkday)
        {
            string sql = "update  Adims_mzzhengtongInfo set chushi='" + chushi + "',SomeHour='" + SomeHour + "',FirstDay='" + FirstDay + "',SecondDay='" + SecondDay + "',"
            + "remarkday='" + remarkday + "'where mzjldID='" + mzid + "'and dataType='" + type + "'";

            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 添加护理记录
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        //public int InsertNurseRecord(Dictionary<string, string> dictionary)
        //{
        //    string sql = string.Format(SQL_NURSERECORD_INSERT, dictionary.Values.ToArray());
        //    return dBConn.ExecuteNonQuery(sql);
        //}

        /// <summary>
        /// 更新护理记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        //public int UpdateNurseRecord(Dictionary<string, string> dictionary)
        //{
        //    return dBConn.ExecuteNonQuery(string.Format(SQL_NURSERECORD_UPDATE, dictionary.Values.ToArray()));
        //}

        #endregion
        #region <<护理记录>>

        /// <summary>
        /// 查询护理记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetNurseRecord_SZ(string patid)
        {
            string sql = "Select * from Adims_NurseRecord_SZ where patid='" + patid + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetNurseRecord_SZ_QXQD(string patid)
        {
            string qxname1 = "";
            string sql = "SELECT qxName,sqCount,szCount,gtqCount  FROM Adims_NurseRecord_SZ_QXQD WHERE PatID='" + patid + "'and qxName!='" + qxname1 + "' ";
            return dBConn.GetDataTable(sql);
        }
        public int InsertNurseRecord_SZ_QXQD(string patid, string qxName, string sqCount, string szCount, string gtqCount)
        {
            string sql = "INSERT INTO Adims_NurseRecord_SZ_qxqd(PATID,qxName,sqCount,szCount,gtqCount) "
            + "VALUES('" + patid + "','" + qxName + "','" + sqCount + "','" + szCount + "','" + gtqCount + "') ";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int DeleteNurseRecord_SZ_QXQD(string patid)
        {
            string sql = "delete from Adims_NurseRecord_SZ_qxqd WHERE PatID='" + patid + "' ";
            return dBConn.ExecuteNonQuery(sql);
        }
        public object Get_SSYS(int id)
        {
            string sql = "select ssys from adims_mzjld where id='" + id + "'";
            return dBConn.ExecuteScalar(sql);
        }
        public int UpdateNurseRecord_SZ_STATE(string patid)
        {
            string sql = "UPDATE Adims_NurseRecord_SZ  SET IsRead='1' where patid='" + patid + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 验证是否存在病人护理记录
        /// </summary>
        /// <param name="patID">病人ID</param>
        /// <returns></returns>
        public bool GetNurseRecordCount(string patID)
        {
            int result = Convert.ToInt32(dBConn.ExecuteScalar(string.Format(SQL_NURSERECORD_COUNT, patID)));
            if (result == 1) return true; else return false;
        }



        /// <summary>
        /// 添加护理记录
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertNurseRecord(Dictionary<string, string> dictionary)
        {
            string sql = string.Format(SQL_NURSERECORD_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 添加护理记录
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertNurseRecord_SZ(string patid, DateTime dt, int flags)
        {
            string sql = "INSERT INTO Adims_NurseRecord_SZ(PATID,visitDate,ISREAD) VALUES('" + patid + "','" + dt + "','" + flags + "') ";
            return dBConn.ExecuteNonQuery(sql);
        }


        /// <summary>
        /// 更新护理记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int UpdateNurseRecord(Dictionary<string, string> dictionary)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_NURSERECORD_UPDATE, dictionary.Values.ToArray()));
        }
        /// <summary>
        /// 更新护理记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int UpdateNurseRecord_SZ(Dictionary<string, string> dictionary)
        {

            string SQL11 = "UPDATE Adims_NurseRecord_SZ WITH (ROWLOCK)  SET Jingmaitonglu ='{0}' ,Baonuan = '{1}',Tiwei = '{2}',Dianjiban ='{3}' ,TiweiGaibian ='{4}' ,"
            + "SQpifu ='{5}' ,SHpifu ='{6}' ,FusushiXZP ='{7}' ,BingfangXZP = '{8}',Yinliu ='{9}' ,Zhiruwu = '{10}',Remark ='{11}' ,SSYSqian = '{12}',SSYShou ='{13}' ,"
            + "XSHSqian = '{14}',XSHShou = '{15}',XHHSqian = '{16}',XHHShou ='{17}' ,VisitDate ='{18}',SHHL='{20}',BB='{21}',BefWaterlow='{22}', AfterWaterlow='{23}',QmDoctor='{24}' WHERE PatID ='{19}' ";

            string str = string.Format(SQL11, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(str);
        }

        #endregion
        #region<<PACU盛泽>>
        public DataTable GetPACU_SZ(int mzid)
        {
            string select = "select * FROM Adims_PACU_SZ  where mzjldid='" + mzid + "'";
            return dBConn.GetDataTable(string.Format(select));
        }
        public int InsertPACU_SZ(int mzid, DateTime savetime)
        {
            string insert = "INSERT INTO Adims_PACU_SZ (mzjldid,SaveTime) values('" + mzid + "','" + savetime + "')";
            return dBConn.ExecuteNonQuery(string.Format(insert));
        }
        public int Update_single_PACU_SZ(string ZDming, DateTime time, int mzid)
        {
            string update = "update Adims_PACU_SZ set " + ZDming + "='" + time + "' where mzjldid='" + mzid + "'";
            return dBConn.ExecuteNonQuery(string.Format(update));
        }

        #endregion
        #region <<资源文件管理>>

        /// <summary>
        /// 验证是否存在文件名字
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetAdims_SourceFile(string name)
        {
            string select = "select * FROM Adims_SourceFileName  where [SourceFileName]='" + name + "'";
            return dBConn.GetDataTable(string.Format(select));
        }

        /// <summary>
        /// 获取文章内容
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetAdims_SourceFileContent(string name)
        {
            string select = "select RowIndex,RowText FROM Adims_SourceFileContent as A  "
            + "join Adims_SourceFileName as B  on A.FileID=B.id  where [SourceFileName]='" + name + "'";

            return dBConn.GetDataTable(string.Format(select));
        }

        /// <summary>
        /// 获取所有File分类
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetAllFileClass()
        {
            string select = "select distinct FileClass FROM Adims_SourceFileName  ";
            return dBConn.GetDataTable(string.Format(select));
        }
        /// <summary>
        /// 根据File分类获取fileName
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetAllFileNameByClass(string fileClass)
        {
            string select = "select SourceFileName   FROM Adims_SourceFileName where FileClass='" + fileClass + "' ";
            return dBConn.GetDataTable(string.Format(select));
        }

        /// <summary>
        /// 新增文件
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertAdims_SourceFileName(string filename, string fileclass, string CreatAuthor, DateTime creattime)
        {
            string insert = "INSERT INTO Adims_SourceFileName (SourceFileName ,FileClass,CreatAuthor,CreatTime)VALUES('" + filename + "','" + fileclass + "','" + CreatAuthor + "','" + creattime + "')";
            return dBConn.ExecuteNonQuery(string.Format(insert));
        }
        /// <summary>
        /// 新增文件内容
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertAdims_SourceFileContent(int FileID, int RowIndex, string RowText)
        {
            string insert = "INSERT INTO Adims_SourceFileContent (FileID ,RowIndex,RowText)VALUES('" + FileID + "','" + RowIndex + "','" + RowText + "')";
            return dBConn.ExecuteNonQuery(string.Format(insert));
        }

        /// <summary>
        /// 修改员工排班
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateAdims_SourceFile(string rowtext, int indexrow)
        {
            string update = "update Adims_SourceFile set";
            return dBConn.ExecuteNonQuery(string.Format(update));
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="employeesID"></param>
        /// <returns></returns>
        public int DeleteAdims_SourceFileName(string filename)
        {
            string DELETE = "Delete from Adims_SourceFileName where SourceFileName='" + filename + "'";
            return dBConn.ExecuteNonQuery(string.Format(DELETE));
        }
        /// <summary>
        /// 删除文章内容
        /// </summary>
        /// <param name="employeesID"></param>
        /// <returns></returns>
        public int DeleteAdims_SourceFileNameontent(string filename)
        {
            string DELETE = "Delete from Adims_SourceFileContent where FileID=(select ID from Adims_SourceFileName where SourceFileName= '" + filename + "')";
            return dBConn.ExecuteNonQuery(string.Format(DELETE));
        }
        #endregion
        #region <<报表数据查询>>

        /// <summary>
        /// 根据麻醉id查询用药
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetYTYYreport(int MZID)
        {
            string STR = "SELECT * FROM Adims_Ytuse WHERE mzjldid='" + MZID + "'";
            return dBConn.GetDataTable(string.Format(STR));
        }
        public DataTable GetTSYYreport(int MZID)
        {
            string STR = "SELECT * FROM Adims_Tsyy WHERE mzjldid='" + MZID + "'";
            return dBConn.GetDataTable(string.Format(STR));
        }


        #endregion
        #region <<术后随访>>

        /// <summary>
        /// 验证病人信息是否存在
        /// </summary>
        /// <param name="patID">病人ID</param>
        /// <returns></returns>
        public bool GetAfterVisitCount(string patID)
        {
            int result = Convert.ToInt32(dBConn.ExecuteScalar(string.Format(SQL_AFTERVISIT_COUNT, patID)));
            if (result == 1) return true; else return false;
        }
        /// <summary>
        /// 验证病人信息是否存在2
        /// </summary>
        /// <param name="patID">病人ID</param>
        /// <returns></returns>
        public DataTable GetAfterVisitCount2(string patID)
        {
            string sql = "select *  from Adims_AfterVisit2 where patID='" + patID + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 验证病人信息是否存在2
        /// </summary>
        /// <param name="patID">病人ID</param>
        /// <returns></returns>
        public DataTable GetAfterVisitCount_SZ(string patID)
        {
            string sql = "select *  from Adims_AfterVisit_SZ where patID='" + patID + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 更新术后随访存档状态
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateAdims_AfterVisit_SZ_STATE()
        {
            string sql = "UPDATE Adims_AfterVisit_SZ  SET IsRead='1'";
            return dBConn.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 查询术后随访
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetAfterVisit(string sqlWhere)
        {
            return dBConn.GetDataTable(string.Format(SQL_AFTERVISIT_SELECT, sqlWhere));
        }

        /// <summary>
        /// 添加术后随访
        /// </summary>
        /// <param name="afterVisitList"></param>
        /// <returns></returns>
        public int InsertAfterVisit(Dictionary<string, string> afterVisitList)
        {
            string sql = string.Format(SQL_AFTERVISIT_INSERT, afterVisitList.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 添加术后随访2
        /// </summary>
        /// <param name="afterVisitList"></param>
        /// <returns></returns>
        public int InsertAfterVisit2(Dictionary<string, string> afterVisitList)
        {
            string sql = string.Format(SQL_AFTERVISIT_INSERT2, afterVisitList.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 添加术后随访2
        /// </summary>
        /// <param name="afterVisitList"></param>
        /// <returns></returns>
        public int InsertAfterVisit_SZ(string patid)
        {
            string sql = string.Format("INSERT INTO [Adims_AfterVisit_SZ] (patid,IsRead,VisitDate) values ('{0}',0,getdate())", patid);
            return dBConn.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新术后随访
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int UpdateAfterVisit(Dictionary<string, string> afterVisitList)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_AFTERVISIT_UPDATE, afterVisitList.Values.ToArray()));
        }
        /// <summary>
        /// 更新术后随访
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int UpdateAfterVisit2(Dictionary<string, string> afterVisitList)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_AFTERVISIT_UPDATE2, afterVisitList.Values.ToArray()));
        }
        /// <summary>
        /// 更新术后随访
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int UpdateAfterVisit_SZ(Dictionary<string, string> afterVisitList)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_AFTERVISIT_UPDATE_SZ, afterVisitList.Values.ToArray()));
        }

        #endregion
        #region <<麻醉记录单>>

        /// <summary>
        /// 查询麻醉记录单
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public DataTable GetMzjldList(string sqlWhere)
        {
            return dBConn.GetDataTable(string.Format(SQL_MZJLD_GETLIST, sqlWhere));
        }
        /// <summary>
        /// 查询麻醉记录
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public DataTable GetMzjldList2(string date1, string date2, string address)
        {
            string select = "SELECT M.id AS MID,OT.patid,PatName,[Otime],ssss,mzfa,ap1,patdpm,pattmd,szzd,patsex,Oroom,"
            + "patage,os FROM Adims_mzjld AS M WITH (NOLOCK) right JOIN Adims_OTypesetting  AS OT WITH (NOLOCK) ON M.patid = OT.patid "
            + "WHERE Otime between '" + date1 + "'and'" + date2 + "' and ot.operaddress='" + address + "'";
            return dBConn.GetDataTable(string.Format(select));
        }

        /// <summary>
        /// 根据麻醉医师统计
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetByApList(string sqlWhere)
        {
            //string GZXSforAP = "select ";

            //     "SELECT ap1,SUM(1) AS ssNum , CONVERT(NUMERIC(12,2),SUM(1)/CONVERT(NUMERIC(12,2),(SELECT COUNT(*) "
            //+ "FROM Adims_mzjld AS M WITH (NOLOCK) INNER JOIN Adims_OTypesetting  AS OT WITH (NOLOCK) ON M.patid = OT.patid WHERE {0}))) AS Probability ";
            //return dBConn.GetDataTable(string.Format(GZXSforAP, sqlWhere, sqlWhere));
            return dBConn.GetDataTable(string.Format(SQL_MZJLD_BYAP, sqlWhere, sqlWhere));
        }
        /// <summary>
        /// 根据手术间统计
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetBySSJList(string sqlWhere)
        {
            return dBConn.GetDataTable(string.Format(SQL_MZJLD_BYSSJ, sqlWhere, sqlWhere));
        }

        /// <summary>
        /// 根据科室统计
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetByDpmList(string sqlWhere)
        {
            return dBConn.GetDataTable(string.Format(SQL_MAZLD_DPM, sqlWhere));
        }
        /// <summary>
        /// 查询记录点
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetALL_Point(int mzid, int lx)
        {
            string sql = "select * from Adims_Point where mzjldid='" + mzid + "' and lx='" + lx + "'order by Otime desc";
            return dBConn.GetDataTable(sql);
        }

        /// <summary>
        /// 查询检测记录点
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetAdims_mzjld_Point(int mzid)
        {
            string sql = "select RecordTime,NIBPS,NIBPD,Pulse,RRC,TEMP,SpO2,ETCO2,BIS,TOF,NIBPM,HR from Adims_mzjld_Point where mzjldid='" + mzid + "' order by RecordTime ASC";

            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 查询检测记录点1
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>

        public DataTable GetMaxTimeServer(int mzid)
        {
            string sql = "select Max(RecordTime) from Adims_mzjld_Point where mzjldid='" + mzid + "' ";
            return dBConn.GetDataTable(sql);
        }

        public DataTable GetMaxTimeLocal(int mzid)
        {
            string sql = "select Max(CreateTime) from Adims_MonitorRecord where mzjldid='" + mzid + "' ";
            return SQLiteHelper.GetDataTable(sql);

        }
        public DataTable GetMinTimeLocal(int mzid)
        {
            string sql = "select min(CreateTime) from Adims_MonitorRecord where mzjldid='" + mzid + "' ";
            return SQLiteHelper.GetDataTable(sql);

        }
        public int CopyLocalToServer(string dtimer, int mzid)
        {
            int result = 0;
            string query = "select mzjldid,CreateTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP from Adims_MonitorRecord "
            + " where mzjldid='" + mzid + "' and CreateTime='" + dtimer + "'";
            DataTable dtLoacal = SQLiteHelper.GetDataTable(query);
            DataTable dtServer = dBConn.GetDataTable(query);
            if (dtServer.Rows.Count == 0 && dtLoacal.Rows.Count > 0)
            {
                query = "insert into Adims_mzjld_Point(mzjldid,RecordTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP) "
               + "values('" + dtLoacal.Rows[0]["mzjldid"] + "','" + Convert.ToDateTime(dtLoacal.Rows[0]["CreateTime"]) + "','" + dtLoacal.Rows[0]["NIBPS"] + "','" + dtLoacal.Rows[0]["NIBPD"] + "',"
               + "'" + dtLoacal.Rows[0]["NIBPM"] + "','" + dtLoacal.Rows[0]["RRC"] + "','" + dtLoacal.Rows[0]["HR"] + "','" + dtLoacal.Rows[0]["Pulse"] + "', '" + dtLoacal.Rows[0]["SpO2"] + "',"
               + "'" + dtLoacal.Rows[0]["ETCO2"] + "', '" + dtLoacal.Rows[0]["TEMP"] + "')";
                result = dBConn.ExecuteNonQuery(query);
            }
            return result;
        }
        public int CopyMzjldToPacu(DateTime dtime, int mzid, int count)
        {
            int result = 0;
            string query = "select NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP from Adims_mzjld_Point "
            + " where mzjldid='" + mzid + "' order by RecordTime desc ";
            DataTable dtMZJLD = dBConn.GetDataTable(query);
            foreach (DataRow dr in dtMZJLD.Rows)
            {
                while (result < count)
                {
                    query = "insert into Adims_Pacu_Point(mzjldid,RecordTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP) "
                  + "values('" + mzid + "','" + dtime + "','" + dr["NIBPS"] + "','" + dr["NIBPD"] + "',"
                  + "'" + dr["NIBPM"] + "','" + dr["RRC"] + "','" + dr["HR"] + "','" + dr["Pulse"] + "', '" + dr["SpO2"] + "',"
                  + "'" + dr["ETCO2"] + "', '" + dr["TEMP"] + "')";
                    dBConn.ExecuteNonQuery(query);
                    result++;
                    dtime = dtime.AddMinutes(5);
                }
            }

            return result;
        }
        public DataTable GetAdims_PACU_Point(int mzid)
        {
            string sql = "select RecordTime,NIBPS,NIBPD,Pulse,RRC,TEMP,SpO2,ETCO2,CVP,TOF,NIBPM,HR from Adims_PACU_Point where mzjldid='" + mzid + "' order by RecordTime ASC";
            return dBConn.GetDataTable(sql);
        }
        //拷贝检测表到显示表
        public int CopyData(int mzjldid, DateTime dtime)
        {
            string query = "select mzjldid,CreateTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP from Adims_MonitorRecord "
                + " where mzjldid='" + mzjldid + "' and Adims_MonitorRecord.CreateTime='" + dtime + "'";
            DataTable dt = dBConn.GetDataTable(query);
            int i = 0;
            if (dt.Rows.Count > 0)
            {
                string copy = "insert into Adims_mzjld_Point(mzjldid,RecordTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP) "
                    + "select mzjldid,CreateTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP from Adims_MonitorRecord "
                    + " where mzjldid='" + mzjldid + "' and Adims_MonitorRecord.CreateTime='" + dtime + "'";
                i = dBConn.ExecuteNonQuery(copy);
            }
            return i;

        }
        public int CopyDataPacu(int mzjldid, DateTime dtime)
        {
            string query = "select mzjldid,CreateTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP from Adims_MonitorRecord_PACU "
                + " where mzjldid='" + mzjldid + "' and Adims_MonitorRecord_PACU.CreateTime='" + dtime + "'";
            DataTable dt = dBConn.GetDataTable(query);
            int i = 0;
            if (dt.Rows.Count > 0)
            {
                string copy = "insert into Adims_Pacu_Point(mzjldid,RecordTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP) "
                    + "select mzjldid,CreateTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP from Adims_MonitorRecord_PACU "
                    + " where mzjldid='" + mzjldid + "' and Adims_MonitorRecord_PACU.CreateTime='" + dtime + "'";
                i = dBConn.ExecuteNonQuery(copy);
            }
            return i;
        }

        /// <summary>
        /// 修改记录点
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int update_Point(int xghValue, int mzid, int lx, DateTime time)
        {
            string sql = "UPDATE Adims_Point set value='" + xghValue + "' where mzjldid='" + mzid + "' and lx='" + lx + "'and Otime='" + time + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 根据麻醉ID查询诱导药使用情况
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetMzydyUSE(int mzjldid)
        {
            string selectYDY = "select * from Adims_ydyUse where mzjldid='" + mzjldid + "'";
            return dBConn.GetDataTable(string.Format(selectYDY));
        }
        /// <summary>
        /// 根据麻醉ID查询术中事件
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetSZSJbytype(int mzjldid, int type)
        {
            string select = "";
            if (type == 0)
                select = "select * from Adims_Szsj where mzjldid='" + mzjldid + "'";
            if (type == 1)
                select = "select * from Adims_pacu_shijian where mzjldid='" + mzjldid + "'";

            return dBConn.GetDataTable(string.Format(select));
        }
        public DataTable GetSZSJ(int mzjldid)
        {
            string select = "select * from Adims_Szsj where mzjldid='" + mzjldid + "'";

            return dBConn.GetDataTable(string.Format(select));
        }
        /// <summary>
        /// 根据麻醉ID查询特殊用药
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetTSYY(int mzjldid)
        {
            string select = "select * from Adims_Tsyy where mzjldid='" + mzjldid + "'";
            return dBConn.GetDataTable(string.Format(select));
        }
        /// <summary>
        /// 根据麻醉ID查询出量
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetCL(int mzjldid)
        {
            string select = "select * from Adims_Cl where mzjldid='" + mzjldid + "'";
            return dBConn.GetDataTable(string.Format(select));
        }

        /// <summary>
        /// 查找指定日期的病人姓名
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public DataTable GetPat(DateTime d)
        {
            return dBConn.GetDataTable("select DISTINCT Adims_OTypesetting.patid,Adims_OTypesetting.patname from Adims_OTypesetting,Adims_Mzjld where Adims_Mzjld.patid = Adims_OTypesetting.patid ");//and Adims_Mzjld.time='"+d.Date+"'");
        }

        /// <summary>
        /// 获取麻醉记录单所有详情
        /// </summary>
        /// <param name="mzjldID"></param>
        /// <returns></returns>
        public DataSet GetSingleMzjld(int mzjldID)
        {
            string sql = string.Format(SQL_MZJLD_GETQTLIST, mzjldID) + "; " // 气体
                + string.Format(SQL_MZJLD_GETJTLIST, mzjldID) + "; "  // 晶体
                + string.Format(SQL_MZJLD_GETJHXMLIST, mzjldID) + "; "  // 监护项目
                + string.Format(SQL_MZJLD_GETPOINTLIST, mzjldID) + "; "  // 坐标点
                + string.Format(SQL_MZJLD_GETSINGLE, mzjldID);
            return dBConn.GetDataSet(sql);
        }

        /// <summary>
        /// 更新麻醉记录单
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int UpdateMzjld(List<string> mzdList)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_MZJLD_UPDATE, mzdList.ToArray()));
        }
        /// <summary>
        /// 更新麻醉记录单1
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int UpdateMzjldInfo(List<string> mzdList)
        {
            string SQL_MZJLD_UPDATE1 = "UPDATE Adims_Mzjld SET tw = '{0}',mzfa = '{1}',zzff = '{2}',ccdianUp1 = '{3}',ccdianUp2 = '{4}',zhiguanUp = '{5}',"
            + "ccdianDown1 = '{6}',ccdianDown2 = '{7}',zhiguanDown = '{8}', szzd = '{9}',ssss = '{10}',ssys = '{11}', mzys = '{12}',qxhs = '{13}', xhhs = '{14}',"
            + "sqyy = '{15}', XueXing = '{16}', ChaGuanFF = '{17}', tsbq = '{18}',ASA= '{19}',ASAE= '{20}',Weight= '{22}',Height= '{23}',tiwen= '{24}',"
            + "ChuXue= '{25}',MZXG= '{26}',NiaoLiang='{27}',eyeoper='{28}',OperLevel='{29}',CutType='{30}'  WHERE id = '{21}'";
            string sql = string.Format(SQL_MZJLD_UPDATE1, mzdList.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 更新麻醉记录单出室情况
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        /// 
        public int UpdateMzjld_patid(string patid1, string patid2) //patid2替换为patid1
        {
            string MZJLD_UPDATE1 = "UPDATE Adims_Mzjld SET patid ='" + patid1 + "' where id = (select id from Adims_Mzjld where patid='" + patid2 + "')";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }
        //public int update_HBBE_ostate(string patid,string patName) 
        //{
        //    string sql = " UPDATE Adims_OTypesetting SET ostate='4' where patid='" + patid + "' and patname='" + patName + "'";
        //    return dBConn.ExecuteNonQuery(sql);
        //}
        public int UpdateMzjld_CSQK(string sqlset)
        {
            string MZJLD_UPDATE1 = "UPDATE Adims_Mzjld SET " + sqlset;
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }
        public int Update_MZJLD_CGBGSJ(int mzid)
        {
            DateTime dt = new DateTime(1900, 1, 1);
            string MZJLD_UPDATE1 = "UPDATE Adims_Mzjld SET cgsj='" + dt + "' , bgsj ='" + dt + "' where id='" + mzid + "'";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }
        public int Update_PACU_CGBGSJ(int mzid)
        {
            DateTime dt = new DateTime(1900, 1, 1);
            string MZJLD_UPDATE1 = "UPDATE Adims_PACU_SZ SET cgsj='" + dt + "' , bgsj ='" + dt + "' where mzjldid='" + mzid + "'";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }
        public int Update_MZJLD_EyeOper(int eyeoper, int mzid)
        {
            string MZJLD_UPDATE1 = "UPDATE Adims_Mzjld SET eyeoper='" + eyeoper + "' where id='" + mzid + "'";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }
        public int Update_MZJLD_isTiwenView(int eyeoper, int mzid)
        {
            string MZJLD_UPDATE1 = "UPDATE Adims_Mzjld SET isTiwenView='" + eyeoper + "' where id='" + mzid + "'";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }
        public int Update_MZJLD_jikongTime(int mzid, DateTime dt1, DateTime dt2)
        {
            string MZJLD_UPDATE1 = "UPDATE Adims_Mzjld SET jkkssj='" + dt1 + "' , jkjssj ='" + dt2 + "' where id='" + mzid + "'";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }
        public int Update_PACU_jikongTime(int mzid, DateTime dt1, DateTime dt2)
        {
            string MZJLD_UPDATE1 = "UPDATE Adims_PACU_SZ SET jkkssj='" + dt1 + "' , jkjssj ='" + dt2 + "' where mzjldid='" + mzid + "'";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }
        /// <summary>
        /// 更新麻醉开始时间
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int UpdateMzkssj(DateTime dt1, int mzjldid)
        {
            string updateMzkssj = "update Adims_Mzjld SET mzkssj = '" + dt1 + "'  where id = '" + mzjldid + "'";
            return dBConn.ExecuteNonQuery(string.Format(updateMzkssj));
        }
        /// <summary>
        /// 更新麻醉结束时间
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int UpdateMzjssj(DateTime dt1, int mzjldid)
        {
            string updateMzjssj = "update Adims_Mzjld SET mzjssj = '" + dt1 + "' where id = '" + mzjldid + "'";
            return dBConn.ExecuteNonQuery(string.Format(updateMzjssj));
        }
        /// <summary>
        /// 更新手术开始时间
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int UpdateSskssj(DateTime dt1, int mzjldid)
        {
            string updateSskssj = "update Adims_Mzjld SET sskssj = '" + dt1 + "' where id = '" + mzjldid + "'";
            return dBConn.ExecuteNonQuery(string.Format(updateSskssj));
        }
        /// <summary>
        /// 更新手术结束时间
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int UpdateSsjssj(DateTime dt1, int mzjldid)
        {
            string updateSsjssj = "update Adims_Mzjld SET ssjssj = '" + dt1 + "' where id = '" + mzjldid + "'";
            return dBConn.ExecuteNonQuery(string.Format(updateSsjssj));
        }
        /// <summary>
        /// 更新手术结束标志
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int UpdateSsjsFlag(int mzjldid)
        {
            string updateSsjssj = "update Adims_Mzjld SET flags = '1' where id = '" + mzjldid + "'";
            return dBConn.ExecuteNonQuery(string.Format(updateSsjssj));
        }
        /// <summary>
        /// 更新手术间标志
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int UpdateShoushujianinfo(int state, int mzjldid, string patid, string oroom)
        {
            string updateSsjssj = "UPDATE ssjstate SET state = '" + state + "',mzjldid = '" + mzjldid + "',patid='" + patid + "' where   oNAME = '" + oroom + "'";
            return dBConn.ExecuteNonQuery(string.Format(updateSsjssj));
        }
        public int UpdateShoushujianinfo(int state, string oroom)
        {
            string updateSsjssj = "UPDATE ssjstate SET state = '" + state + "'  where   oNAME = '" + oroom + "'";
            return dBConn.ExecuteNonQuery(string.Format(updateSsjssj));
        }
        public int UpdatePaibanInfo(int ostate, string patid)
        {
            string updateSsjssj = "UPDATE Adims_OTypesetting SET ostate = '" + ostate + "' where patid = '" + patid + "'";
            return dBConn.ExecuteNonQuery(string.Format(updateSsjssj));
        }
        public int UpdatePaibanDate(string odate, string patid)
        {
            string updateSsjssj = "UPDATE Adims_OTypesetting SET odate = '" + odate + "' where patid = '" + patid + "'";
            return dBConn.ExecuteNonQuery(string.Format(updateSsjssj));
        }
        public int UpdateMzjldFalgs(int flags, int mzid)
        {
            string updateSsjssj = "UPDATE adims_mzjld SET flags = '" + flags + "' where id = '" + mzid + "'";
            return dBConn.ExecuteNonQuery(string.Format(updateSsjssj));
        }


        /// <summary>
        /// 更新插管时间
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int UpdateMzCG(DateTime dt1, int mzjldid)
        {
            string updateMzCG = "update Adims_Mzjld SET CGSJ = '" + dt1 + "' where id = '" + mzjldid + "'";
            return dBConn.ExecuteNonQuery(string.Format(updateMzCG));
        }
        /// <summary>
        /// 更新拔管时间
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int UpdateMzBG(DateTime dt1, int mzjldid)
        {
            string updateMzBG = "update Adims_Mzjld SET BGSJ = '" + dt1 + "' where id = '" + mzjldid + "'";
            return dBConn.ExecuteNonQuery(string.Format(updateMzBG));
        }
        /// <summary>
        /// 插入检测数据
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int insertJianCeData(int mzjldid, int hr, int spo2, int pulse, int nibps, int nibpd, int nibpm, int arts, int artd, int artm, double etco2, int ico2, int rrc)
        {
            string insert = "insert into Adims_MonitorRecord(mzjldid,HR,SpO2,Pulse,NIBPS,NIBPD,NIBPM,ARTS,ARTD,ARTM,ETCO2,ICO2,RRC,CreateTime) values(" + mzjldid + "," + hr + "," + spo2 + "," + pulse + "," + nibps + "," + nibpd + "," + nibpm + "," + arts + "," + artd + "," + artm + "," + etco2 + "," + ico2 + "," + rrc + ",'" + DateTime.Now + "')";

            return dBConn.ExecuteNonQuery(insert);
        }
        public int insertJianCeDataMZJLD(int mzjldid, int nibps, int nibpd, int nibpm, int rrc, int hr, int pulse, int spo2, int etco2, double temp, DateTime now)
        {
            string insert = "insert into Adims_MonitorRecord(mzjldid,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP,CreateTime) values(" + mzjldid + "," + nibps + "," + nibpd + "," + nibpm + "," + rrc + "," + hr + "," + pulse + "," + spo2 + "," + etco2 + "," + temp + ",'" + now + "')";
            return dBConn.ExecuteNonQuery(insert);
        }
        public int insertJianCeDataPACU(int mzjldid, int nibps, int nibpd, int nibpm, int rrc, int hr, int pulse, int spo2, int etco2, double temp, DateTime now)
        {
            string insert = "insert into Adims_MonitorRecord_PACU(mzjldid,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP,CreateTime) values(" + mzjldid + "," + nibps + "," + nibpd + "," + nibpm + "," + rrc + "," + hr + "," + pulse + "," + spo2 + "," + etco2 + "," + temp + ",'" + now + "')";
            return dBConn.ExecuteNonQuery(insert);
        }
        public DataTable selectJianCeData(DateTime dt, int mzjldid, int TYPE)
        {
            string sel = "";
            if (TYPE == 0)
                sel = "select * from Adims_MonitorRecord where CreateTime='" + dt + "'and mzjldid='" + mzjldid + "'";
            if (TYPE == 1)
                sel = "select * from Adims_MonitorRecord_PACU where CreateTime='" + dt + "'and mzjldid='" + mzjldid + "'";

            return dBConn.GetDataTable(sel);
        }

        #endregion
        #region<<麻醉统计 >>
        /// <summary>
        /// 获取医生名字
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetYSname()
        {
            return dBConn.GetDataTable(string.Format("select user_name from Adims_User where uid!='1'"));

        }
        /// <summary>
        /// 获取手术间名字
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetSSJname(string operaddress)
        {
            if (operaddress == "010601")
                return dBConn.GetDataTable(string.Format("select oname from ssjstate where oname like '新·%' "));
            else
                return dBConn.GetDataTable(string.Format("select oname from ssjstate where oname like '老·%' "));

        }
        /// <summary>
        /// 获取科室名字
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetKShiname()
        {
            return dBConn.GetDataTable(string.Format("select Name from keshi "));

        }
        /// <summary>
        /// 获取麻醉名字
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetMZname()
        {
            return dBConn.GetDataTable(string.Format("select AMname from AMethod "));

        }
        public DataTable GetOROOM(string yiyuanID)
        {
            string sql = "";

            if (yiyuanID == "010604")
                sql = "select ONAME from SSJSTATE where oname like '%老·%'";
            else
                sql = "select ONAME from SSJSTATE where oname like '%新·%'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetOROOM1()
        {
            return dBConn.GetDataTable(string.Format("select ONAME from SSJSTATE where oname like '%老·%'"));
        }
        public DataTable GetOROOM_all()
        {
            return dBConn.GetDataTable(string.Format("select ONAME from SSJSTATE "));
        }
        /// <summary>
        /// 获取麻醉记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetKSname()
        {
            return dBConn.GetDataTable(string.Format("select Name from keshi "));

        }
        /// <summary>
        /// 分麻醉医生统计信息
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetByYSName(string name, string date1, string date2)
        {
            string select = "SELECT M.id AS MID,OT.patid,PatName,olevel,[Otime],ssss,mzfa,ap1,patdpm,pattmd,szzd,patsex,Oroom,"
            + "patage,os FROM Adims_mzjld AS M WITH (NOLOCK) right JOIN Adims_OTypesetting  AS OT WITH (NOLOCK) ON M.patid = OT.patid "
            + "WHERE   CONVERT(varchar, otime , 23 )  between '" + date1 + "'and'" + date2 + "'and (olevel like '%" + name + "%'or mzys like '%" + name + "%'"
            + "or mzfa like '%" + name + "%'or patdpm like '%" + name + "%'or Oroom like '%" + name + "%' or Amethod like '%" + name + "%')";

            //string select = "SELECT M.id AS MID,OT.patid,PatName,[time],ssmc,mzfa,ap1,patdpm,pattmd,szzd,patsex,"
            //+ "patage,os FROM Adims_mzjld AS M ,Adims_OTypesetting  AS OT where M.patid = OT.patid "
            //+ "and time between '" + date1 + "'and'" + date2 + "'and mzys like '%" + name + "%'or patdpm like '%" + name + "%'or Oroom like '%" + name + "%'";

            return dBConn.GetDataTable(string.Format(select));

        }

        #endregion
        #region<<请假加班统计>>

        /// <summary>
        /// 获取请假总数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetQJZS(DateTime dtime1, DateTime dtime2)
        {
            string QJZSselect = "select [EmployeesNO], [EmployeesName] ,sum([SumDay]) as QJZS  from [Adims_LeaveRegistration] where [CreateDate] >='" + dtime1 + "'and [CreateDate] <='" + dtime2 + "' group by [EmployeesNO],[EmployeesName] ";
            return dBConn.GetDataTable(string.Format(QJZSselect));

        }
        /// <summary>
        /// 获取请假明细
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetQJMX(DateTime dtime1, DateTime dtime2, string flag)
        {
            string QJMXselect = "select [EmployeesNO], [EmployeesName] ,[StartDate], [StartTime],[EndDate],[EndTime],[SumDay],[LeaveReason]  from [Adims_LeaveRegistration] where [CreateDate] >='" + dtime1 + "'and [CreateDate] <='" + dtime2 + "' and EmployeesNO='" + flag + "' ";
            return dBConn.GetDataTable(string.Format(QJMXselect));

        }
        /// <summary>
        /// 获取加班总数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetJBZS(DateTime dtime1, DateTime dtime2)
        {
            string JBZSselect = "select [EmployeesNO], [EmployeesName] ,sum([endTime] -startTime) as JBZS  from adims_JBJL where [JBDay] >='" + dtime1 + "'and [JBDay] <='" + dtime2 + "' group by [EmployeesNO],[EmployeesName] ";
            return dBConn.GetDataTable(string.Format(JBZSselect));

        }
        /// <summary>
        /// 获取加班详细
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetJBMX(DateTime dtime1, DateTime dtime2, string flag)
        {
            string QJMXselect = "select [EmployeesNO], [EmployeesName] ,[JBDay] ,startTime, [endTime],[JBReason]  from [adims_JBJL] where [JBDay] >='" + dtime1 + "'and [JBDay] <='" + dtime2 + "' and EmployeesNO='" + flag + "' ";
            return dBConn.GetDataTable(string.Format(QJMXselect));

        }

        #endregion

        #endregion

        #region<<手动排班>>

        /// 判断病人访视信息是否存在
        /// </summary>
        /// <param name="patID"></param>
        /// <returns></returns>
        public bool GetPatPaiBanInfo(string patID)
        {
            string sql = "select * from Adims_OTypesetting where patid='" + patID + "'";
            int result = Convert.ToInt32(dBConn.ExecuteScalar(sql));
            if (result == 1) return true; else return false;
        }

        private static readonly string SQL_PAIBAN_INSERT = "INSERT INTO Adims_OTypesetting(PatID,PatZhuYuanID,Patname ,Patage,Patsex "
                                + ",Patdpm  ,Patbedno ,PatWeight ,PatHeight,Oname,Pattmd,Oroom,Second,Amethod,AP1"
                                + ",ON1 ,SN1,Odate,OS,ostate,applyid,Cardno,ReplyDate) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',"
                                + "'{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}',{19},'{20}','{21}','{22}')";

        private static readonly string SQL_PAIBAN_UPDATE = " UPDATE Adims_OTypesetting WITH (ROWLOCK) SET "
         + "PatZhuYuanID = '{1}',Patname = '{2}',Patage = '{3}',Patsex = '{4}',Patdpm = '{5}',"
         + "Patbedno = '{6}',PatWeight = '{7}',PatHeight = '{8}',Oname = '{9}',Pattmd = '{10}',"
         + "Oroom = '{11}',Second = '{12}',Amethod = '{13}',AP1 = '{14}',ON1 = '{15}',SN1 = '{16}'"
         + ",Odate = '{17}',OS = '{18}' where PatID = '{0}'";

        /// <summary>
        /// 添加排班
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertPAIBAN(Dictionary<string, string> dictionary)
        {
            string INSERT = string.Format(SQL_PAIBAN_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(INSERT);
        }
        public int InsertPAIBAN(string patid, string patname, string oroom, DateTime odate, string operAddress)
        {
            string INSERT = "insert into Adims_OTypesetting"
                + " (patid,patname,oroom,odate,operAddress,ASAE,ostate,patage,patsex,IsValid,patdpm)"
                + " values('" + patid + "','" + patname + "','" + oroom + "','" + odate + "','" + operAddress + "','1','2','','','1','')";
            return dBConn.ExecuteNonQuery(INSERT);
        }
        /// <summary>
        /// 添加排班
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertPaiban(List<string> list1)
        {
            string SQL_PAIBAN = "insert into Adims_OTypesetting (applyid,Cardno, PatZhuYuanID, Patname,Patage,Patsex,patdpm,Pattmd,Oname,Odate,os,Amethod,Patbedno,patid,asae,operaddress,ostate,oroom,ap1,ap2,ap3,sn1,sn2,on1,on2)"
                + "values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}', '{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','','','','','','','','')";
            string INSERT = string.Format(SQL_PAIBAN, list1.ToArray());
            return dBConn.ExecuteNonQuery(INSERT);
        }
        /// <summary>
        /// 查询排班
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        //public DataTable GetPAIBAN(string dtime)
        //{
        //    string sql = "SELECT patid,oroom ,second ,patdpm ,patname ,patsex ,patage ,patbedno ,os ,oname ,pattmd ,amethod ,"
        //       + "ap1 ,ap2 ,ap3 ,on1,on2 ,sn1,sn2"
        //       + ",applyID ,id   from Adims_OTypesetting where CONVERT(varchar, Odate , 23 ) ='" + dtime + "' and oroom like '%新·%'";
        //    return dBConn.GetDataTable(sql);
        //}

        public DataTable GetPAIBAN(string dtime, string operAddress, string sqlWhere)
        {
            string sql =
                string.Format(@"SELECT id ,oroom,second ,patdpm,patbedno,patname,
                patage,patsex,oname,pattmd,os,amethod,on1,on2 ,sn1,sn2,Remarks,ap1 ,ap2 ,ap3
                ,applyID,patZhuYuanID,patid,ASAE from Adims_OTypesetting  
                where CONVERT(varchar,Odate,23) ='{0}'and operAddress='{1}' and IsValid='1'", dtime, operAddress)
                + sqlWhere;

            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 查询排班
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetALLPAIBAN(string patid)
        {
            string sql = "SELECT * from Adims_OTypesetting  where patid='" + patid + "'";

            return dBConn.GetDataTable(sql);
        }
        public DataTable GetPaiban(string patid, string odate)
        {
            string sql = "SELECT * from Adims_OTypesetting  where patid='" + patid + "' and odate='" + odate + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetPaiban(string patid)
        {
            string sql = "SELECT * from Adims_OTypesetting where patid='" + patid + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetMzffID(string MZFF)
        {
            string sql = "SELECT Mzff_No  FROM [mazuifangan] where [name]='" + MZFF + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetOperNo(string SSMZ)
        {
            string sql = "SELECT OperNo  FROM OperationName where ONname='" + SSMZ + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetPaibanAndMZJLD(string patid)
        {
            string sql = "  SELECT O.PatID,O.ApplyID,O.Amethod,M.sskssj,M.ssjssj,M.Otime,M.ssss,O.Oroom,O.Second,O.Oname,O.IsZhuYuan,"
            + " Olevel,Amethod,GL,JZ,AP1,AP2,AP3,OA1,OA2,OA3,OA1No,OA2No,OA3No,OS,OsNo,TP,ON1,ON2,SN1,SN2,Remarks,M.asa,M.asae,Ostate,Odate "
            + " FROM HeYiAMIS.dbo.Adims_OTypesetting  as O LEFT JOIN Adims_Mzjld as M ON O.PatID =M.patid"
            + " WHERE O.patid='" + patid + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetPaibanDayAndName(string patname, DateTime odate)
        {
            string sql = "SELECT * from Adims_OTypesetting  where patname='" + patname + "' and odate='" + odate + "' ";
            return dBConn.GetDataTable(sql);
        }
        public int DeletePaibanPatidOdate(string Patid, DateTime odate)
        {
            string sql = "delete  from Adims_OTypesetting  where Patid='" + Patid + "' and odate='" + odate + "' and patSex ='' ";
            return dBConn.ExecuteNonQuery(sql);
        }
        public DataTable GetpaibanByOroomandTaici(string oroom, string second, DateTime dt, string patid)
        {
            string sql = "SELECT * from Adims_OTypesetting  where oroom='" + oroom + "' and second='" + second + "'and Odate ='" + dt + "'and patid!='" + patid + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 更新排班
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdatePAIBAN(Dictionary<string, string> dictionary)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_PAIBAN_UPDATE, dictionary.Values.ToArray()));
        }

        public int UpdatePaiban(string patID, string DateType, string value)
        {
            string sql = "UPDATE Adims_OTypesetting WITH (ROWLOCK) SET " + DateType + " = '" + value + "' WHERE patid = '" + patID + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int UpdatePaibanWhere(string patID, string AddString)
        {
            string sql = "UPDATE Adims_OTypesetting WITH (ROWLOCK) SET " + AddString + " WHERE patid = '" + patID + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int UpdatePaibanHeightWeight(string patID, string height, string weight)
        {
            string sql = "UPDATE Adims_OTypesetting WITH (ROWLOCK) SET PatHeight = '" + height + "',PatWeight= '" + weight + "' WHERE patid = '" + patID + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int UpdatePaibanConfig(string patID)
        {
            string sql = "UPDATE Adims_OTypesetting WITH (ROWLOCK) SET IsValid= '1',Ostate='1'  WHERE patid = '" + patID + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int UpdatePaibanCancel(string patID)
        {
            string sql = "UPDATE Adims_OTypesetting WITH (ROWLOCK) SET IsValid= '1',Oroom='',Second='',Ostate='0'  WHERE patid = '" + patID + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        public DataTable GetPaiXu(string dt, string where)
        {
            string SQL_SELECT = "SELECT oroom,second,StartTime,patid,patdpm,patname,"
                + "patsex,patage,patbedno,patNation,pattmd,oname,os,on1,on2,sn1,sn2,ap1,ap2,ap3,tiwei,bx,gr,remarks,id"
                + "  from Adims_OTypesetting WITH (NOLOCK) WHERE CONVERT(varchar, Odate , 23 ) = '" + dt + "' ";
            string sql = string.Format(SQL_SELECT + where);
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetPaiXu(int ostate, string dt, string where)
        {
            string SQL_SELECT = "SELECT oroom,second,StartTime,patid,patdpm,patname,"
                + "patsex,patage,patbedno,patNation,pattmd,oname,os,on1,on2,sn1,sn2,ap1,ap2,ap3,tiwei,bx,gr,remarks,id"
                + "  from Adims_OTypesetting WITH (NOLOCK) WHERE CONVERT(varchar, Odate , 23 ) = '" + dt + "' AND ostate = '" + ostate + "' ";
            string sql = string.Format(SQL_SELECT + where);
            return dBConn.GetDataTable(sql);
        }
        #endregion

        public int UpdatePaibanAgeDW(string AgeDW, string patid)
        {
            string INSERT = "Update Adims_OTypesetting set agedw='" + AgeDW + "' where patid='" + patid + "'";
            return dBConn.ExecuteNonQuery(INSERT);
        }

    }
}
