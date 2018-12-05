using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace adims_DAL
{
    public class AdimsProvider
    {
        private DBConn dBConn = new DBConn();

        #region <<方法>>

        static string strconn = ConfigurationManager.AppSettings["ConnectionString"];

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
        /// 取出排班的内容
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
            newrow["time"] = time;
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
            findrow["time"] = time;
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
        public int DeleteData(string ID, string table)
        {
            string sql = "delete from " + table + " where id='" + ID + "'";
            return dBConn.ExecuteNonQuery(sql);

        }

        public int AddData(string name, string tableName)
        {
            string sql = "insert into  " + tableName + "(name) values ('" + name + "')";
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 添加手术室
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public int AddDatas(string name, string tableName)
        {
            string sql = "insert into  " + tableName + "(name,state,mzjldid,patid) values ('" + name + "',0,0,'0')";
            return dBConn.ExecuteNonQuery(sql);
        }

        #endregion

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



        #region <<加班登记>>

        private static readonly string SQL_JBJL_INSERT = "INSERT INTO [Adims_JBJL]( [EmployeesNO],[EmployeesName],[JBDay],[startTime],[endTime],[JBReason],[creatDate])VALUES ('{0}','{1}','{2}','{3}','{4}','{5}',GETDATE())";
        private static readonly string SQL_JBJL_UPDATE = "UPDATE  [Adims_JBJL] SET [EmployeesNO] = '{0}',[EmployeesName] = '{1}',[JBDay] = '{2}',[startTime] = '{3}',[endTime] = '{4}',[JBReason]='{5}',[UpdateDate] = GETDATE() WHERE ID = '{6}'";
        private static readonly string SQL_JBJL_DELETE = "DELETE Adims_JBJL WHERE ID = '{0}'";

        #endregion

        #region <<智能排班>>


        //private static readonly string SQL_OTYPESETTING_SELECT = "SELECT oroom 手术间,second 台次, patdpm 科室,patname 病人姓名,patsex 性别,patage 年龄,patbedno 床号,os 手术医师,os1 助手,oname 手术名字,"
        //        + "ap1 主麻医师,ap2 副麻医师1,ap3 副麻医师2,on1 洗手护士1,on2 洗手护士2,sn1 巡回护士1,sn2 巡回护士2,StartTime 预计开始时间,patid 病人ID,"
        //        + "remarks 备注 from Adims_OperSchedule WITH (NOLOCK) WHERE {0} ORDER BY oroom";

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
            + "YuejingLC = '{7}',JingzhiYS = '{8}',JingmaiPG = '{9}',ZerenHushi = '{10}',Vistor = '{11}',VisitDate = '{12}'where patid = '{13}'";
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
        private static readonly string SQL_AFTERVISIT_INSERT_SZ = "INSERT INTO [Adims_AfterVisit_SZ] (XinliZK ,BingfaZ,BingfaZother,JKJY,Taidu,Zerenxin ,Jishu,ZonghePJ,ZJAQWS,Zerenhushi,Yijian,Vistor,VisitDate,patid,IsRead)"
         + "VALUES ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','0')";

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


        private static readonly string SQL_AFTERVISIT_UPDATE_SZ = "UPDATE Adims_AfterVisit_SZ WITH (ROWLOCK)  SET XinliZK= '{0}' ,BingfaZ= '{1}',BingfaZother= '{2}',"
            + "JKJY= '{3}',Taidu='{4}',Zerenxin= '{5}' ,Jishu= '{6}',ZonghePJ= '{7}',ZJAQWS= '{8}',Yijian= '{9}',Zerenhushi = '{10}',Vistor= '{11}', VisitDate= '{12}' where patid = '{13}'";
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


        #endregion

        #region <<Methods>>



        #region ZoneTimeSet

        public int Update_ZoomTime(int id, DateTime dt1, DateTime dt2, int Interval)
        {
            string sql = "UPDATE Adims_Mzjld_ZoomTime SET StartTime='" + dt1 + "' , EndTime ='" + dt2 + "',Interval='" + Interval + "' where id='" + id + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int Insert_ZoomTime(int mzid, DateTime dt1, DateTime dt2, int Interval)
        {
            string sql = "Insert into Adims_Mzjld_ZoomTime(mzjldID,StartTime,EndTime,Interval) values ('" + mzid + "' ,'" + dt1 + "' ,'" + dt2 + "' ,'" + Interval + "')";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int Del_ZoomTime(int id)
        {
            string sql = "delete from  Adims_Mzjld_ZoomTime where id='" + id + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        public DataTable Get_ZoomTime(int mzjldid, DateTime otime)
        {
            string sql = "select * from  Adims_Mzjld_ZoomTime where mzjldid='" + mzjldid + "' and StartTime>'" + otime + "' order by StartTime asc";
            return dBConn.GetDataTable(sql);
        }
        public DataTable Get_ZoomTime(int mzjldid)
        {
            string sql = "select * from  Adims_Mzjld_ZoomTime where mzjldid='" + mzjldid + "' order by StartTime asc";
            return dBConn.GetDataTable(sql);
        }
        #endregion
        #region <<基础资料>>




        /// <summary>
        /// 麻醉小结
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllYD_MZXJ()
        {
            string selectAllYS = " SELECT name as 小结 FROM MZXJ ";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }
        /// <summary>
        /// 留置深度
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllYD_LZSD()
        {
            string selectAllYS = " SELECT name as 留置深度 FROM LZSD ";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }
        /// <summary>
        /// MZ变更原因
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllYD_MZBGYY()
        {
            string selectAllYS = " SELECT name as 原因 FROM MZBGYY ";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }
        /// <summary>
        /// 注入方式
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllYD_ZRFS()
        {
            string selectAllYS = " SELECT name  FROM ZRFS ";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }

        /// <summary>
        /// 离开恢复室情况
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllLKHFSQK()
        {
            string selectAllYS = " SELECT name as 情况 FROM LKFHSQK ";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }
        /// <summary>
        /// 药物
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllYW()
        {
            string selectAllYS = " SELECT name as 药物 FROM YW ";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }

        /// <summary>
        /// 术后镇痛
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllSHZT()
        {
            string selectAllYS = " SELECT name as 术后镇痛 FROM SHZT ";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }
        /// <summary>
        /// 出室情况
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllCSQK()
        {
            string selectAllYS = " SELECT name as 出室情况 FROM CSQK ";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }
        /// <summary>
        /// 麻醉平面
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllQYZZ()
        {
            string selectAllYS = " SELECT name as 区域阻滞 FROM QYZZ ";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }


        /// <summary>
        /// 单位
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllDW()
        {
            string selectAllYS = " SELECT name FROM DW ";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }
        /// <summary>
        /// 厂家
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllCJ()
        {
            string selectAllYS = " SELECT name FROM WH_Name ";
            return dBConn.GetDataTable(string.Format(selectAllYS));
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
            string sql = "SELECT LeaveRegistrationID,EmployeesNO,EmployeesName,StartTime,EndTime,SumDay,LeaveReason FROM Adims_LeaveRegistration WITH (NOLOCK) WHERE {0}";
            return dBConn.GetDataTable(string.Format(sql, sqlWhere));
        }


        /// <summary>
        /// 查询是否已经请假
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable SlectInHoliday(string bianhao, DateTime dt1, DateTime dt2)
        {
            string countQJ = "SELECT count(*) from Adims_LeaveRegistration where EmployeesNo='" + bianhao + "' and  StartTime>='" + dt1 + "' and EndTime<='" + dt2 + "' ";
            return dBConn.GetDataTable(string.Format(countQJ));
        }
        /// <summary>
        /// 新增请假登记
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertLeaveRegistration(List<string> dictionary)
        {
            string SQL_LEAVEREGISTRATION_INSERT = "INSERT INTO Adims_LeaveRegistration(EmployeesNO,EmployeesName,StartTime,EndTime,SumDay,LeaveReason,CreateDate)VALUES ('{0}','{1}','{2}','{3}','{4}','{5}',GETDATE())";
            return dBConn.ExecuteNonQuery(string.Format(SQL_LEAVEREGISTRATION_INSERT, dictionary.ToArray()));
        }

        /// <summary>
        /// 修改请假登记
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateLeaveRegistration(List<string> dictionary)
        {
            string SQL = "UPDATE Adims_LeaveRegistration SET EmployeesNO = '{0}',EmployeesName= '{1}',StartTime = '{2}',EndTime= '{3}',SumDay = '{4}',LeaveReason = '{5}',UpdateDate = GETDATE() WHERE LeaveRegistrationID = '{6}'";

            return dBConn.ExecuteNonQuery(string.Format(SQL, dictionary.ToArray()));
        }

        /// <summary>
        /// 删除请假登记
        /// </summary>
        /// <param name="employeesID"></param>
        /// <returns></returns>
        public int DeleteLeaveRegistration(string employeesID)
        {
            string SQL_DELETE = "DELETE  from Adims_LeaveRegistration WHERE LeaveRegistrationID = '{0}'";

            return dBConn.ExecuteNonQuery(string.Format(SQL_DELETE, employeesID));
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
            string selct = "select * from Admins_qxqdType";
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
            string countQJ = "SELECT * from [Admins_qxqdType] where [qxbType]='" + mod + "'";
            return dBConn.GetDataTable(string.Format(countQJ));
        }
        /// <summary>
        /// 添加器械名称
        /// </summary>
        /// <param name="mod"></param>
        /// <param name="cmb"></param>
        /// <returns></returns>
        public int SelectqxqdModel(int mod, string lx, int bz)
        {
            string countQJ = "Insert into Adims_qxqdModel(qxqd_id,qxType,qxbj) values(" + mod + ",'" + lx + "'," + bz + ") ";
            return dBConn.ExecuteNonQuery(string.Format(countQJ));
        }
        /// <summary>
        /// 删除器械名称
        /// </summary>
        /// <param name="mod"></param>
        /// <param name="cmb"></param>
        /// <returns></returns>
        public int DeteletqxqdModel(int mod)
        {
            string countQJ = "Delete Adims_qxqdModel where id=" + mod + "";
            return dBConn.ExecuteNonQuery(string.Format(countQJ));
        }
        /// <summary>
        /// 修改器械名称
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public int UpdatesqxqdModel(int mod, string name, string text)
        {
            string countQJ = "update Adims_qxqdModel set " + name + "='" + text + "' where id=" + mod + "";
            return dBConn.ExecuteNonQuery(string.Format(countQJ));
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
        /// 查询器械清点
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetNurseRecordQX(string mzjldid)
        {
            string sql = "Select * from Adims_HLQXQD where mzjldid='" + mzjldid + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <returns></returns>
        public DataTable GetBindQX(string mzjldid)
        {
            string sql = @"SELECT [QXname],[SQ],[GQ],[GH],[Qxname1],[SQ1],[GQ1],[GH1],[Qxname2],[SQ2],[GQ2],[GH2],[Id] FROM [Adims_HLQXQD] where mzjldid='" + mzjldid + "' order by id";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetBindQX(string mzjldid, string name)
        {
            string sql = @"SELECT [QXname],[SQ],[GQ],[GH],[Qxname1],[SQ1],[GQ1],[GH1],[Qxname2],[SQ2],[GQ2],[GH2],[Id] FROM [Adims_HLQXQD] where mzjldid='" + mzjldid + "' and (QXname='" + name + "' or QXname1='" + name + "' or QXname2='" + name + "')";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 删除器械清点
        /// </summary>
        /// <param name="mzid"></param>
        /// <param name="patid"></param>
        /// <returns></returns>
        public int DeleteNurseRecordQX(string mzid, string patid)
        {
            string sql = "delete Adims_HLQXQD where patid='" + patid + "'and mzjldid='" + mzid + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 添加器械清点
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertNurseRecordQX(Dictionary<string, string> dictionary)
        {
            string _insert = @"INSERT INTO [Adims_HLQXQD]([patid],[mzjldid],[QXB],[QXname],[SQ] ,[GQ],[GH] ,[Qxname1],[SQ1] ,[GQ1],[GH1],[Qxname2],[SQ2] ,[GQ2],[GH2])
     VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')";
            string sql = string.Format(_insert, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 查询器械模板详细数据
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable SelectqxmcInmodel(string mod, string lb)
        {
            string countQJ = "select m.id,m.qxmc,m.qxCount,t.qxbType,m.qxType,m.qxbj from Adims_qxqdModel as m left OUTER JOIN Admins_qxqdType as t on t.id=m.qxqd_id where t.qxbType='" + mod + "' and m.qxType like'%" + lb + "%'";
            return dBConn.GetDataTable(string.Format(countQJ));
        }

        /// <summary>
        /// 器械清点
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public DataTable SelectqxmcInmodels(string mod)
        {
            string countQJ = "select m.qxmc,m.qxCount from Adims_qxqdModel as m left OUTER JOIN Admins_qxqdType as t on t.id=m.qxqd_id where t.qxbType='" + mod + "'";
            return dBConn.GetDataTable(string.Format(countQJ));
        }
        public DataTable SelectqxmcInmodelss(string mod, string lb)
        {
            string countQJ = "select m.qxmc,m.qxCount from Adims_qxqdModel as m left OUTER JOIN Admins_qxqdType as t on t.id=m.qxqd_id where t.qxbType='" + mod + "' and m.qxType like'%" + lb + "%'";
            return dBConn.GetDataTable(string.Format(countQJ));
        }


        /// <summary>
        /// 修改器械包
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public int UpdateQxType(string mod, string cmb)
        {
            string countQJ = "Update Admins_qxqdType set qxbType='" + mod + "' where qxbType='" + cmb + "'";
            return dBConn.ExecuteNonQuery(string.Format(countQJ));
        }

        /// <summary>
        /// 新增器械清点模板
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertqxqdModel(string mod)
        {
            string Insert = "Insert into Admins_qxqdType values('" + mod + "') ";
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
        /// <param name="employeesID"></param>
        /// <returns></returns>
        public int Deleteqxmc(string QXMC)
        {
            string delete = "delete from qxmcTable where qxmc='" + QXMC + "'";
            return dBConn.ExecuteNonQuery(string.Format(delete));
        }
        #endregion

        #region 麻醉月报表统计
        public string GetAboutMZJLD(string where, string dt1, string dt2)
        {
            string Insert = "select COUNT(*) from adims_mzjld where {0} and convert(nvarchar(10),otime,23) BETWEEN '" + dt1 + "' AND '" + dt2 + "'";
            DataTable dt = dBConn.GetDataTable(string.Format(Insert, where));
            return dt.Rows[0][0].ToString();
        }
        public string GetAboutAfterVisit(string where, string dt1, string dt2)
        {
            string Insert = "select COUNT(*) from Adims_AfterVisit_CJ where {0} and convert(nvarchar(10),VisitDate,23) BETWEEN '" + dt1 + "' AND '" + dt2 + "'";
            DataTable dt = dBConn.GetDataTable(string.Format(Insert, where));
            return dt.Rows[0][0].ToString();
        }
        public string GetAboutPacu(string where, string dt1, string dt2)
        {
            string Insert = "select COUNT(*) from Adims_Pacu where {0} and convert(nvarchar(10),Addtime,23) BETWEEN '" + dt1 + "' AND '" + dt2 + "'";
            DataTable dt = dBConn.GetDataTable(string.Format(Insert, where));
            return dt.Rows[0][0].ToString();
        }

        /// <summary>
        /// 查询统计
        /// </summary>
        /// <param name="YF"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataTable SelectYBBTJ(string date)
        {
            string sql = "SELECT * from Admins_Mbbtj  where year(tj_date)='" + date + "' order by convert(int,YFpaixu) asc";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 判断统计的数据是否存在
        /// </summary>
        /// <param name="YF"></param>
        /// <returns></returns>
        public DataTable SelectYBBTJ(string YF, string date)
        {
            string sql = "SELECT * from Admins_Mbbtj  where tj_YF='" + YF + "' and year(tj_date)='" + date + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 添加统计
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertYBBTJ(Dictionary<string, string> dictionary, string date)
        {
            string INSERT = @"INSERT INTO [Admins_Mbbtj] ([tj_date],[tj_YF],[ygzl],[cgqm],[twxh],[zgnmz],[sjzd],[wtrl],[wtwj],[sjmcc],[xffs] ,[mzfs],[yzbfz],[Steward],[shzt] ,[ssswqj],[mzfyq],[ysza],[ybhd],[qmjscx],[hxdza],[mzywsw],[fyqxgsj],[ASAY1],[ASAN1],[ASAY2],[ASAN2],[ASAY3],[ASAN3],[ASAY4],[ASAN4],[ASAY5],[ASAN5],[YFpaixu]) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}', '{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}')";
            string sql = string.Format(INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 更新统计
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateYBBTJ(Dictionary<string, string> dictionary, string YF, string date)
        {
            string INSERT = @"UPDATE [Admins_Mbbtj]SET [tj_date] ='{0}',[tj_YF] = '{1}',[ygzl] = '{2}',[cgqm] ='{3}',[twxh] = '{4}',[zgnmz] = '{5}',[sjzd] = '{6}',[wtrl] ='{7}',[wtwj] = '{8}',[sjmcc] ='{9}',[xffs] ='{10}',[mzfs] ='{11}'
,[yzbfz] ='{12}',[Steward] ='{13}',[shzt] ='{14}',[ssswqj] ='{15}',[mzfyq] ='{16}',[ysza] ='{17}',[ybhd] ='{18}'
,[qmjscx] ='{19}',[hxdza] ='{20}',[mzywsw] ='{21}',[fyqxgsj] ='{22}',[ASAY1] ='{23}',[ASAN1] ='{24}',[ASAY2] ='{25}'
,[ASAN2] ='{26}',[ASAY3] ='{27}',[ASAN3] ='{28}',[ASAY4] ='{29}',[ASAN4] ='{30}',[ASAY5] ='{31}',[ASAN5] ='{32}',[YFpaixu] ='{33}' WHERE [tj_YF]='" + YF + "' and [tj_date]='" + date + "'";
            string sql = string.Format(INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
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
            string SQL_JBJL_GETLIST = "SELECT [ID] ,[EmployeesNO] ,[EmployeesName] ,[JBDay] ,[startTime],[endTime] ,[JBReason] FROM Adims_JBJL WITH (NOLOCK) WHERE {0}";
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
        private static readonly string SQL_OTYPESETTING_SELECT = "SELECT oroom,second,StartTime,PatZhuYuanID,patdpm,patname,"
                + "patsex,patage,patbedno,patNation,pattmd,oname,Amethod,SSmzfs,os,os1,os2,on1,on2,sn1,sn2,ap1,ap2,ap3,tiwei,bx,gr,remarks,shengfen,id" + "  from Adims_OperSchedule WITH (NOLOCK) WHERE {0} ORDER BY oroom";
        public DataTable GetOTypesetting(string oroom, string dt)
        {
            string sqlWhere = "CONVERT(varchar, Odate , 23 ) = '" + dt + "'";
            if (oroom != "全部手术间")
                sqlWhere += " AND oroom = '" + oroom + "'";
            return dBConn.GetDataTable(string.Format(SQL_OTYPESETTING_SELECT, sqlWhere));
        }


        public DataTable GetPaibanResult(int ostate, string dt, string where)
        {
            string SQL_SELECT = "SELECT oroom,second,StartTime,PatZhuYuanID,patdpm,Patname + '(' + Patsex + '/' + Patage  + '岁' + ')' AS patname"
                + ",patbedno,patNation,pattmd,oname,amethod,rtrim(ltrim(isnull(os,'')+isnull(os1,'')+isnull(os2,'')+isnull(os3,'')))as os ,tiwei,bx,gr,remarks,on1+ ' '+on2 + ' '+sn1+ ' '+sn2 as hushi,ap1+ ' '+ap2+ ' '+ap3 as ap"
                + "  from Adims_OperSchedule WITH (NOLOCK) WHERE CONVERT(varchar, Odate , 23 ) = '" + dt + "' AND ostate >= '" + ostate + "' ";
            string sql = string.Format(SQL_SELECT + where);
            return dBConn.GetDataTable(sql);
        }


        static string connStr = ConfigurationManager.AppSettings["ConnectionString"];
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
        /// 修改为择期
        /// </summary>
        /// <param name="pb"></param>
        /// <returns></returns>
        public int UpdateOTypesetting_ZQ(string id)
        {
            return dBConn.ExecuteNonQuery("update Adims_OperSchedule set isjizhen='0' where id ='" + id + "'");
        }

        /// <summary>
        /// 保存排版信息
        /// </summary>
        /// <param name="pb"></param>
        /// <returns></returns>
        public int SaveOTypesetting(adims_MODEL.paiban pb)
        {
            return dBConn.ExecuteNonQuery("INSERT INTO Adims_OperSchedule(patid,oname,oroom,second,odepartment,olevel,"
                + "amethod,gl,jz,ap1,ap2,ap3,aa1,aa2,aa3,os,oa1,oa2,oa3,oa4,tp,on1,on2,sn1,sn2,sn3,remarks,ostate,"
                + "odate)values('" + pb.Patid + "','" + pb.Oname + "','" + pb.Oroom + "','" + pb.Second + "','"
                + pb.Odepartment + "','" + pb.Olevel + "','" + pb.Amethod + "','" + pb.Gl + "','" + pb.Jz + "','"
                + pb.Ap1 + "','" + pb.Ap2 + "','" + pb.Ap3 + "','" + pb.Aa1 + "','" + pb.Aa2 + "','" + pb.Aa3
                + "','" + pb.Os + "','" + pb.Oa1 + "','" + pb.Oa2 + "','" + pb.Oa3 + "','" + pb.Oa4 + "','"
                + pb.Tp + "','" + pb.On1 + "','" + pb.On2 + "','" + pb.Sn1 + "','" + pb.Sn2 + "','" + pb.Sn3
                + "','" + pb.Remarks + "','" + pb.Ostate + "','" + pb.Odate + "')");
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
        /// <summary>
        /// 查询访视信息
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetBeforeVisit_YS(string patid)
        {
            string sql = "SELECT * from Adims_BeforeVisit_YS  where patid='" + patid + "'";

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
        public int UpdateBeforeVisit_HS_STATE()
        {
            string sql = "UPDATE Adims_BeforeVisit_HS  SET IsRead='1'";
            return dBConn.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新术前访视(医生）
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateBeforeVisit_YS(Dictionary<string, string> dictionary)
        {
            string update_YS = "UPDATE Adims_BeforeVisit_YS  SET ASA='{1}',isJizhen='{2}',FeiPang='{3}',Baowei='{4}',YNYWS='{5}',Weight='{6}',Jixing='{7}',Jingzhui='{8}',ZKKN='{9}',ZKdu='{10}'"
                + ",Jiaya='{11}',HuxiKN='{12}',Mallam='{13}',Xinzang='{14}',Gaoxueya='{15}',GuanxinB='{16}',FeiGN='{17}',FeibuJB='{18}',GanGN='{19}',ShenGN='{20}'"
                + ",SJXTJB='{21}',OtherCheck='{22}',MZFXPG='{23}',Jinshi='{24}',Jinyin='{25}',OtherZhuyi='{26}',MZFZCS='{27}',MZYS='{28}',ApplyDate='{29}',MZFF='{31}' "
                + "where patid='{0}'";
            return dBConn.ExecuteNonQuery(string.Format(update_YS, dictionary.Values.ToArray()));
        }
        /// <summary>
        /// 更新术前访视(医生）存档状态
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateBeforeVisit_YS_STATE(string patid, int state)
        {
            string sql = "UPDATE Adims_BeforeVisit_YS  SET IsRead='" + state + "' where patid='" + patid + "' ";
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

        #endregion


        #region <<昌吉护理记录>>

        /// <summary>
        /// 查询护理记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetNurseRecord(string mzjldid)
        {
            string sql = "Select * from Adims_NurseRecord_CJ where mzjldid='" + mzjldid + "'";
            return dBConn.GetDataTable(sql);
        }
        public int UpdateNurseRecord_State(string mzid, int isRead)
        {
            string sql = "UPDATE Adims_NurseRecord_CJ  SET IsRead='" + isRead + "' where mzjldid='" + mzid + "'";
            return dBConn.ExecuteNonQuery(sql);
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
        public int InsertNurseRecord(string mzid, string patid, DateTime dt, int IsRead)
        {
            string sql = "INSERT INTO Adims_NurseRecord_CJ(mzjldid,patid,AddTime,IsRead) VALUES('" + mzid + "','" + patid + "','" + dt + "','" + IsRead + "') ";
            return dBConn.ExecuteNonQuery(sql);
        }


        /// <summary>
        /// 更新护理记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int UpdateNurseRecord1(Dictionary<string, string> dictionary)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_NURSERECORD_UPDATE, dictionary.Values.ToArray()));
        }
        /// <summary>
        /// 更新护理记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int UpdateNurseRecord(Dictionary<string, string> dictionary)
        {

            string SQL11 = "UPDATE Adims_NurseRecord_CJ WITH (ROWLOCK)  SET EnterTime='{2}',LeaveTime='{3}',Shenzhi='{4}',JMCC='{5}',"
            + "Daoniao='{6}',PifuIn='{7}',Weight='{8}',Kangshengsu='{9}',Guominshi='{10}',Shuye='{11}',Zhixuedai='{12}',Tiwei='{13}',Xuexing='{14}',Zitixue='{15}',ZitixueJL='{16}',Zitixuedw='{17}',"
            + "Yitixue='{18}',YitixueType='{19}',YitixueJL='{20}',YitixueDW='{21}',Yishi='{22}',PifuOut='{23}',DaoniaoOut='{24}',Niaoliang='{25}',Yinliu='{26}',BRQX='{27}',RemarkOut='{28}',Wujunbao='{29}',"
            + "XHHS='{30}',QXHS='{31}',RemarkLast='{32}',Biaoben='{33}',Diandao='{34}',Dianjiban='{35}',UpdateTime='{36}' WHERE mzjldid ='{0}' ";
            string sql = string.Format(SQL11, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }

        #endregion

        #region<<PACU盛泽>>

        public int Update_single_PACU_SZ(string ZDming, DateTime time, int mzid)
        {
            string update = "update Adims_PACU set " + ZDming + "='" + time + "' where mzjldid='" + mzid + "'";
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
        public DataTable GetAfterVisitCount_CJ(string patID)
        {
            string sql = "select *  from Adims_AfterVisit_CJ where patID='" + patID + "'";
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
        /// 添加术后随访
        /// </summary>
        /// <param name="afterVisitList"></param>
        /// <returns></returns>
        public int InsertAfterVisit_CJ(Dictionary<string, string> afterVisitList)
        {
            string sqlnsert = "INSERT INTO Adims_AfterVisit_CJ (patid,Yishi,Xueya,Huxi,Xinlv,YBZZ,OtherZZ,YishiSJ,XueyaSJ,HuxiSJ,"
             + "XinlvSJ,Exin,ChuanciBW,ZhitiHD,ShuhouZT,ZTBPF,ZTXG,MZYS,VisitDate ,isread)"
             + " VALUES ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}')";

            string sql = string.Format(sqlnsert, afterVisitList.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 更新术后随访
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int UpdateAfterVisit_CJ(Dictionary<string, string> afterVisitList)
        {
            string SQLUPDATE2 = "UPDATE Adims_AfterVisit_CJ WITH (ROWLOCK)  SET Yishi = '{1}',Xueya = '{2}',HuXi='{3}',"
            + " Xinlv= '{4}',YBZZ= '{5}',OtherZZ= '{6}',YishiSJ= '{7}',XueyaSJ= '{8}',HuxiSJ= '{9}',XinlvSJ= '{10}',Exin= '{11}',"
            + "ChuanciBW= '{12}',ZhitiHD= '{13}',ShuhouZT= '{14}',ZTBPF= '{15}',ZTXG= '{16}',MZYS= '{17}',VisitDate= '{18}'"
            + ",isread= '{19}' WHERE patID = '{0}' ";

            return dBConn.ExecuteNonQuery(string.Format(SQLUPDATE2, afterVisitList.Values.ToArray()));
        }
        public int UpdateAfterVisit_CJ(string patid, int isread)
        {
            string sql = "UPDATE Adims_AfterVisit_CJ WITH (ROWLOCK)  SET isread = '" + isread + "' WHERE patID = '" + patid + "' ";
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 添加术后随访2
        /// </summary>
        /// <param name="afterVisitList"></param>
        /// <returns></returns>
        public int InsertAfterVisit_SZ(Dictionary<string, string> afterVisitList)
        {
            string sql = string.Format(SQL_AFTERVISIT_INSERT_SZ, afterVisitList.Values.ToArray());
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
        public int UpdateAfterVisit_SZ(Dictionary<string, string> afterVisitList)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_AFTERVISIT_UPDATE_SZ, afterVisitList.Values.ToArray()));
        }

        #endregion

        #region <<麻醉记录单>>




        public DataTable GetAdims_PACU_Point(int mzid)
        {
            string sql = "select RecordTime,NIBPS,NIBPD,Pulse,RRC,TEMP,SpO2,ETCO2,CVP,TOF,NIBPM,HR from Adims_PACU_Point where mzjldid='" + mzid + "' order by RecordTime ASC";
            return dBConn.GetDataTable(sql);
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
        /// 做跟踪
        /// </summary>
        /// <param name="text"></param>
        private void SaveLog(string text)
        {
            try
            {
                FileStream fs = new FileStream("c:\\mzjldSave.txt", FileMode.Append);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                sw.Write("\r\n" + text + "\r\n");
                sw.Close();
                fs.Close();
            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// 更新麻醉记录单1
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int UpdateMzjld1(List<string> mzdList)
        {
            string MZJLD_UPDATE1 = @"UPDATE Adims_Mzjld SET asa = '{0}',isjizhen = '{1}',SQJinshi = '{2}',tsbq = '{3}',sqzd = '{4}',nssss = '{5}',tw = '{6}',Szzd = '{7}', ShoushuFS = '{8}',ssys = '{9}',mzys = '{10}', qxhs = '{11}',xhhs = '{12}', weight = '{13}', height = '{14}',SQYY='{15}',Niaoliang='{16}',ChuXue='{17}',qiekouType='{18}',qiekouCount='{19}',MazuiFS='{20}',Ssname='{21}',SsAge='{22}',SsSex='{23}',Rsssxy='{24}',Mb='{25}',Hx='{26}',Xx='{27}',mzxg='{28}',Qyzz='{29}',Mzpm='{30}',Yw='{31}',Qmyd='{32}',Qmwc='{33}',Qdytq='{34}',Cg='{35}',Mzxg2='{36}',Csqk='{37}',Sssj='{38}',Mzsj='{39}',Sxl='{40}',Hxb='{41}',Nl='{42}',Qx='{43}',Xs='{44}',Xj='{45}',Fs='{46}',Sy='{47}',Shzt='{48}',Ztyy='{49}',cgh='{50}',brqx='{51}' WHERE id = '{52}'";
            string SQL = string.Format(MZJLD_UPDATE1, mzdList.ToArray());
            SaveLog(DateTime.Now.ToString() + "   " + SQL);
            return dBConn.ExecuteNonQuery(SQL);
        }
        /// <summary>
        /// 回传值给排班
        /// </summary>
        /// <param name="mzdList"></param>
        /// <returns></returns>
        public int UpdatePanban(List<string> mzdList)
        {
            string panban = "UPDATE Adims_OperSchedule set SSmzfs = '{2}',AP1 = '{3}',AP2 = '{4}',"
            + "AP3 = '{5}',on1 = '{6}', on2 = '{7}',sn1 = '{8}',sn2 = '{9}' WHERE PatZhuYuanID = '{0}' and convert(varchar, Odate,23)='{1}'";
            string SQL = string.Format(panban, mzdList.ToArray());
            return dBConn.ExecuteNonQuery(SQL);
        }

        /// <summary>
        /// 更新麻醉记录单
        /// </summary>
        /// <param name="value">更新值</param>
        /// <param name="mzjldID">麻醉单号</param>
        /// <param name="type">类型 --- 1手术开始时间；2检测间隔时间；3</param>
        /// <returns></returns>
        public int UpdateMzjld(string value, int mzjldID, int type)
        {
            string sql = "UPDATE Adims_Mzjld SET ";
            if (type == 1)
            {
                sql += $" otime='{value}' WHERE id='{mzjldID}'";
            }
            if (type == 2)
            {
                sql += $" jcsjjg='{value}' WHERE id='{mzjldID}'";
            }

            return dBConn.ExecuteNonQuery(sql);
        }
        public int Update_MZJLD_CGBGSJ(int mzid)
        {
            DateTime dt = new DateTime(1900, 1, 1);
            string MZJLD_UPDATE1 = "UPDATE Adims_Mzjld SET cgsj='" + dt + "' , bgsj ='" + dt + "' where id='" + mzid + "'";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }


        public int Update_MZJLD_jikongTime(int mzid, DateTime dt1, DateTime dt2, string value)
        {
            string MZJLD_UPDATE1 = "UPDATE Adims_Mzjld SET jkkssj='" + dt1 + "' , jkjssj ='" + dt2 + "',jkvalue='" + value + "' where id='" + mzid + "'";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }
        /// <summary>
        /// 修改辅助呼吸时间
        /// </summary>
        /// <param name="mzid"></param>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public int Update_MZJLD_fuzhuTime(int mzid, DateTime dt1, DateTime dt2, string value)
        {
            string MZJLD_UPDATE1 = "UPDATE Adims_Mzjld SET fzkssj='" + dt1 + "' , fzjssj ='" + dt2 + "',fzvalue='" + value + "' where id='" + mzid + "'";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }
        /// <summary>
        /// 更新麻醉开始时间
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int UpdateMzkssj(DateTime dt1, int mzjldid)
        {
            string updateMzkssj = "update Adims_Mzjld SET mzkssj = '" + dt1 + "' where id = '" + mzjldid + "'";
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
            string updateSsjssj = "UPDATE ssjstate SET state = '" + state + "',mzjldid = '" + mzjldid + "',patid='" + patid + "' where NAME = '" + oroom + "'";
            return dBConn.ExecuteNonQuery(string.Format(updateSsjssj));
        }
        public int UpdateShoushujianinfo(int state, string oroom)
        {
            string updateSsjssj = "UPDATE ssjstate SET state = '" + state + "' where   name = '" + oroom + "'";
            return dBConn.ExecuteNonQuery(string.Format(updateSsjssj));
        }
        /// <summary>
        /// 修改排版状态（0未排班，1已排班，2已手术）
        /// </summary>
        /// <param name="ostate"></param>
        /// <param name="patid"></param>
        /// <returns></returns>
        public int UpdatePaibanInfo(int ostate, string patid)
        {
            string updateSsjssj = "UPDATE Adims_OperSchedule SET ostate = '" + ostate + "' where   patID = '" + patid + "'";
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



        #region<<请假加班统计>>

        /// <summary>
        /// 获取请假总数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetQJZS(DateTime dtime1, DateTime dtime2)
        {
            string QJZSselect = "select  EmployeesNO, EmployeesName ,sum(Convert(float,SumDay)) as QJZS  from Adims_LeaveRegistration where CreateDate between'" + dtime1 + "'and '" + dtime2 + "' group by EmployeesNO, EmployeesName";
            return dBConn.GetDataTable(string.Format(QJZSselect));

        }
        /// <summary>
        /// 获取请假明细
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetQJMX(DateTime dtime1, DateTime dtime2, string flag)
        {
            string QJMXselect = "select EmployeesNO, EmployeesName ,StartTime,EndTime,SumDay,LeaveReason  from Adims_LeaveRegistration where CreateDate >='" + dtime1 + "'and CreateDate <='" + dtime2 + "' and EmployeesNO='" + flag + "' ";
            return dBConn.GetDataTable(string.Format(QJMXselect));

        }
        /// <summary>
        /// 获取加班总数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetJBZS(DateTime dtime1, DateTime dtime2)
        {
            string JBZSselect = "select [EmployeesNO], [EmployeesName] ,sum(Convert(float,endTime - startTime)) as JBZS  from adims_JBJL where [JBDay] >='" + dtime1 + "'and [JBDay] <='" + dtime2 + "' group by [EmployeesNO],[EmployeesName] ";
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
            string sql = "select * from Adims_OperSchedule where patid='" + patID + "'";
            int result = Convert.ToInt32(dBConn.ExecuteScalar(sql));
            if (result == 1) return true; else return false;
        }

        private static readonly string SQL_PAIBAN_INSERT = "INSERT INTO Adims_OperSchedule(PatID,PatZhuYuanID,Patname ,Patage,Patsex "
                                + ",Patdpm  ,Patbedno ,PatWeight ,PatHeight,Oname,Pattmd,Oroom,Second,Amethod,AP1"
                                + ",ON1 ,SN1,Odate,OS,isjizhen,StartTime,BZ,GR,Ostate) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',"
                                + "'{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')";

        private static readonly string SQL_PAIBAN_UPDATE = " UPDATE Adims_OperSchedule WITH (ROWLOCK) SET "
         + "PatZhuYuanID = '{1}',Patname = '{2}',Patage = '{3}',Patsex = '{4}',Patdpm = '{5}',"
         + "Patbedno = '{6}',PatWeight = '{7}',PatHeight = '{8}',Oname = '{9}',Pattmd = '{10}',"
         + "Oroom = '{11}',Second = '{12}',Amethod = '{13}',AP1 = '{14}',ON1 = '{15}',SN1 = '{16}'"
         + ",Odate = '{17}',OS = '{18}',Ostate = '1' where PatID = '{0}'";

        /// <summary>
        /// 添加排班
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertPaiban(List<string> list1)
        {
            //string SQL_PAIBAN = "INSERT INTO Adims_OperSchedule(PatID,PatZhuYuanID,CardID,Patname ,Patage,Patsex,patNation"
            //                    + ",Patbedno,Patdpm,Pattmd ,Oname ,odate,OS,os1,os2,Amethod,BX,Tiwei,GR,remarks,StartTime,isjizhen,ostate) VALUES"
            //                    + "('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',"
            //                    + "'{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')";
            string SQL_PAIBAN = "INSERT INTO Adims_OperSchedule(PatID,PatZhuYuanID,CardID,Patname ,Patage,Patsex,patNation "
                                + ",Patbedno,Patdpm,Pattmd ,PatHeight,PatWeight,PatBloodType,Oname ,odate,OS,os1,os2,os3,os4,Amethod,BX,Tiwei,GR,remarks,StartTime,ostate,isjizhen,Ocode,SSLB,SQSJ,sn1,sn2,on1,on2,ap1,ap2,ap3) VALUES"
                                + "('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}',"
                                + " '','','','','','','')";

            string INSERT = string.Format(SQL_PAIBAN, list1.ToArray());
            return dBConn.ExecuteNonQuery(INSERT);
        }

        /// <summary>
        /// 查询排班
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetPAIBAN(string dtime)
        {
            string sql = "SELECT PatZhuYuanID ,oroom ,second ,patdpm ,patname ,patsex ,patage ,patbedno ,os ,oname ,pattmd ,amethod ,"
               + "ap1 ,on1 ,on2 ,sn1 ,sn2 "
               + " from Adims_OperSchedule where CONVERT(varchar, Odate , 23 ) ='" + dtime + "' and isjizhen='1'";
            return dBConn.GetDataTable(sql);
        }

        /// <summary>
        /// 根据id查询排班信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetALLPAIBANID(string id)
        {
            string sql = "SELECT * from Adims_OperSchedule  where id='" + id + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetPaiban(string patid, string odate)
        {
            string sql = "SELECT * from Adims_OperSchedule  where PatZhuYuanID='" + patid + "' and convert(varchar,odate,23)='" + odate + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetPaibans(string patid, string odate)
        {
            string sql = "SELECT * from Adims_OperSchedule  where PatZhuYuanID='" + patid + "' and convert(varchar,odate,23)='" + odate + "'";
            return dBConn.GetDataTable(sql);
        }



        public int UpdatePaiban(List<string> list1)
        {
            string SQL_1 = "UPDATE Adims_OperSchedule WITH (ROWLOCK) SET PatZhuYuanID='{1}',Patname='{2}' ,Patage='{3}',Patsex='{4}',"
                          + ",Patbedno='{5}',Patdpm='{6}',PatWeight='{7}',PatHeight='{8}',Oname='{9}',Pattmd='{10}',Oname='{11}',Second='{12}',Amethod='{13}',ap1='{14}',"
                          + "on1='{15}',sn1='{16}',odate='{17}',os='{18}',isjizhen='{19}',StartTime='{20}',remarks='{21}',GR='{22}',tiwei='{23}',patnation='{24}' where patid='{0}'";

            string sql = string.Format(SQL_1, list1.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        #endregion

        #region 红桥护理记录






        #endregion
    }
}
