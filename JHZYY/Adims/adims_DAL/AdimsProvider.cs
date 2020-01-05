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

        public static DataTable GetData1(string table, string name,string belong)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter("select ID 编号,name " + name + ",belong "+belong+" from " + table, conn);
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
        
        public static void AddData1(string name, string belong,string table)
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
        public static void UpdateData1(string ID, string Name, string belong,string table)
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
        public  DataTable SelectData(string table)
        {
            string sql = "select id 编号,name 名称  from   " + table;
            return dBConn.GetDataTable(sql);
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
        //        + "remarks 备注 from Adims_OTypesetting WITH (NOLOCK) WHERE {0} ORDER BY oroom";

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

        #region <<麻醉记录单>>

        // 麻醉记录单查询
        private static readonly string SQL_MZJLD_GETLIST = "SELECT M.id AS MID,OT.patid,PatName,[time],ssmc,mzfa,ap1,patdpm,pattmd,szzd,patsex,"
            + "patage,os FROM Adims_mzjld AS M WITH (NOLOCK) RIGHT JOIN Adims_OTypesetting  AS OT WITH (NOLOCK) ON M.patid = OT.patid WHERE {0}";
        // 气体查询
        private static readonly string SQL_MZJLD_GETQTLIST = "SELECT qtname,yl,dw,sytime,jstime,flags FROM Adims_Qtuse WITH (NOLOCK) WHERE mzjldid='{0}'";
        // 液体查询
        private static readonly string SQL_MZJLD_GETYTLIST = "SELECT ytname,yl,dw,yyfs,cxyy,sytime,jstime,flags FROM Adims_Ytuse WITH (NOLOCK) WHERE mzjldid='{0}'";
        // 晶体查询
        private static readonly string SQL_MZJLD_GETJTLIST = "SELECT Cxyy,name,jl,dw,zrfs,kssj,jssj,flags FROM Adims_jmyUSE WITH (NOLOCK) WHERE mzjldid='{0}'";
        // 监护项目查询
        private static readonly string SQL_MZJLD_GETJHXMLIST = "SELECT lx,value,time FROM Adims_jhxm WITH (NOLOCK) WHERE mzjldid='{0}'";
        // 坐标点查询
        private static readonly string SQL_MZJLD_GETPOINTLIST = "SELECT lx,value,time FROM Adims_Point WITH (NOLOCK) WHERE mzjldid='{0}'";
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
            + "mzfa = '{5}',mzys = '{6}', ssys = '{7}',XueXing = '{8}', mzxg = '{9}',qm = '{10}',[time] = GETDATE() WHERE id = '{11}'";

       // 更新麻醉记录单1
        private static readonly string SQL_MZJLD_UPDATE1 = "UPDATE Adims_Mzjld SET asa = '{0}',asae = '{1}',isjizhen = '{2}',SQJinshi = '{3}',tsbq = '{4}',sqzd = '{5}',"
            + "nssss = '{6}',tw = '{7}',shoushufs = '{8}', mazuifs = '{9}',ssys = '{10}',mzys = '{11}', qxhs = '{12}',xhhs = '{13}', sqyy = '{14}'"
            + ", weight = '{15}', height = '{16}', chuxue = '{17}' WHERE id = '{18}'";
        // 更新麻醉记录单2
        private static readonly string SQL_MZJLD_UPDATE2 = "UPDATE Adims_Mzjld SET tw = '{0}',mzfa = '{1}',zzff = '{2}',ccdianUp1 = '{3}',ccdianUp2 = '{4}',zhiguanUp = '{5}',"
            + "ccdianDown1 = '{6}',ccdianDown2 = '{7}',zhiguanDown = '{8}', szzd = '{9}',ssss = '{10}',ssys = '{11}', mzys = '{12}',qxhs = '{13}', xhhs = '{14}'"
            + ", sqyy = '{15}', XueXing = '{16}', ChaGuanFF = '{17}', tsbq = '{18}',ASA= '{19}',ASAE= '{20}',Weight= '{22}',Height= '{23}',tiwen= '{24}',ChuXue= '{25}' WHERE id = '{21}'";
        
        
        #endregion

        #endregion

        #region <<Methods>>
        #region 临时医嘱单方法
        /// <summary>
        /// 新增临时医嘱
        /// </summary>
        public int InsertLSYZ(int mazjldid,string paid, string kldate, string kltime)
        {
            string Insert = "Insert into Adims_LSYZ(mzjldid,patid,klDate,klTime) values('" + mazjldid + "','" + paid + "','" + kldate + "','" + kltime + "') ";
            return dBConn.ExecuteNonQuery(string.Format(Insert));
        }
        /// <summary>
        /// 新增临时医嘱
        /// </summary>
        public int InsertLSYZ(Dictionary<string, string> dictionary)
        {
            string _INSERT = @"INSERT INTO [HeYiAMIS_CJ].[dbo].[Adims_LSYZ]([mzjldid],[patid],[klDate] ,[klTime],[yizhu] ,[yisheng]
,[zxDate],[zxTime],[Remark])
VALUES ('{0}' ,'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}') ";
            return dBConn.ExecuteNonQuery(string.Format(_INSERT, dictionary.Values.ToArray()));
        }
        // <summary>
        /// 新增临时医嘱包
        /// </summary>
        public int InsertLSYZBao(string kldate, string kltime,string YizhuType_id)
        {
            string Insert = "Insert into Adims_LSYZZT(klDate,klTime,YizhuType_id) values('" + kldate + "','" + kltime + "','" + YizhuType_id + "') ";
            return dBConn.ExecuteNonQuery(string.Format(Insert));
        }
        /// <summary>
        /// 修改临时医嘱
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public int updateLSYZ(int id, string DataType, string DataValue)
        {
            string countQJ = "Update Adims_LSYZ set  " + DataType + " = '" + DataValue + "'  where id='" + id + "'";
            return dBConn.ExecuteNonQuery(string.Format(countQJ));
        }
        /// <summary>
        /// 修改临时医嘱包
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public int updateLSYZbao(int id, string DataType, string DataValue)
        {
            string countQJ = "Update Adims_LSYZZT set  " + DataType + " = '" + DataValue + "'  where id='" + id + "'";
            return dBConn.ExecuteNonQuery(string.Format(countQJ));
        }
        public int updateLSYZdatetime(int id, string zxDate, string zxTime)
        {
            string countQJ = "Update Adims_LSYZ set  zxDate = '" + zxDate + "', zxTime = '" + zxTime + "'  where id='" + id + "'";
            return dBConn.ExecuteNonQuery(string.Format(countQJ));
        }
        /// <summary>
        /// 修改执行日期
        /// </summary>
        /// <param name="id"></param>
        /// <param name="zxDate"></param>
        /// <param name="zxTime"></param>
        /// <returns></returns>
        public int updateLSYZbaodatetime(int id, string zxDate, string zxTime)
        {
            string countQJ = "Update Adims_LSYZZT set  zxDate = '" + zxDate + "', zxTime = '" + zxTime + "'  where id='" + id + "'";
            return dBConn.ExecuteNonQuery(string.Format(countQJ));
        }
        /// <summary>
        /// shanchu临时医嘱
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public int deleteLSYZ(int id,int mid)
        {
            string countQJ = "delete from Adims_LSYZ where id='" + id + "' and mzjldid='" + mid + "'";
            return dBConn.ExecuteNonQuery(string.Format(countQJ));
        }
        public DataTable SelectLSYZ(int id, int mid)
        {
            string countQJ = "select * from Adims_LSYZ  where id='" + id + "' and mzjldid='" + mid + "'";
            return dBConn.GetDataTable(string.Format(countQJ));
        }
        /// <summary>
        /// 删除医嘱包
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int deleteLSYZBao(int id)
        {
            string countQJ = "delete from Adims_LSYZZT  where id='" + id + "'";
            return dBConn.ExecuteNonQuery(string.Format(countQJ));
        }
        /// <summary>
        /// 查询临时医嘱
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public DataTable SelectLSYZ(int mzjldid)
        {
            string countQJ = "select id,mzjldid,kldate,kltime,yizhu,yisheng,zxdate,zxtime,remark from Adims_LSYZ where mzjldid='" + mzjldid + "' ";
            return dBConn.GetDataTable(string.Format(countQJ));
        }
        public DataTable Selectshuye(int mzjldid)
        {
           
            string countQJ = "insert into Adims_LSYZ "+
"select a.mzjldid,'',(cast(a.yue as varchar(255)) +'-'+ cast(a.tian as varchar(255)))as kldate,(cast(a.shi as varchar(255))+':'+ cast(a.fen as varchar(255))) as kltime, a.shuyename+' '+a.jl+' '+ a.dw + ' 静点' as yizhu,'','','','','' from "+
"(select id,mzjldid,DATEPART(MONTH,kssj)as yue,DATEPART(DAY,kssj) as tian ,DATEPART(HOUR,kssj) as shi "+
",DATEPART(MINUTE,kssj) as fen,shuyename,jl, dw from Adims_shuyeUse) a where mzjldid='" + mzjldid + "' ";
            return dBConn.GetDataTable(string.Format(countQJ));
        }
        /// <summary>
        /// 查询临时医嘱包
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public DataTable SelectLSYZBao(string name)
        {
            string countQJ = @"SELECT l.[id]
      ,l.[klDate]
      ,l.[klTime]
      ,l.[yizhu]
      ,l.[yisheng]
      ,l.[hushi]
      ,l.[zxDate]
      ,l.[zxTime]
      ,l.[Remark]
      ,l.[YizhuType_id]
  FROM [HeYiAMIS_CJ].[dbo].[Adims_LSYZZT] as l left outer join Adims_YZBao as b on l.YizhuType_id=b.id where b.name='" + name + "' ";
            return dBConn.GetDataTable(string.Format(countQJ));
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
        public int UpdateSHXJ(string name,string neirong)
        {
            string update = "update [Adims_SHXJ] set [shxjInfo] ='" + neirong + "' where  patName='" + name + "' ";
            return dBConn.ExecuteNonQuery(string.Format(update));
        }

        /// <summary>
        /// 查询工作人员
        /// </summary>
        /// <returns></returns>
        public DataTable GetSurgeryStaff(string sqlWhere)
        {
            string sql = "SELECT uid,user_name FROM Adims_User WITH (NOLOCK) WHERE {0} ORDER BY type";
            return dBConn.GetDataTable(string.Format(sql, sqlWhere));
        }
        /// <summary>
        /// 查询医嘱包
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllYZBao()
        {
            string selectAllYS = " SELECT id, name FROM Adims_YZBao ";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }
        /// <summary>
        /// 查询医嘱
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllYZtype()
        {
            string selectAllYS = " SELECT id, name FROM Adims_YZtype ";
            return dBConn.GetDataTable(string.Format(selectAllYS));
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
        public DataTable GetAllMZYSsdsd()
        {
            string selectAllYS = " SELECT user_name FROM Adims_user";
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
        /// 查询切口等级
        /// </summary>
        /// <returns></returns>
        public DataTable GetAll_QKDJ()
        {
            string selectAllYS = " SELECT DISTINCT QKDJ FROM Adims_OTypesetting where QKDJ is not null";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }
        /// <summary>
        /// 手术类别
        /// </summary>
        /// <returns></returns>
        public DataTable GetAll_SSLB()
        {
            //string selectAllYS = "SELECT DISTINCT SSLB FROM Adims_OTypesetting where SSLB is not null";
            string selectAllYS = @" SELECT DISTINCT SSLB FROM Adims_OTypesetting where SSLB is not null and SSLB 
not in 
(SELECT SSLB FROM Adims_OTypesetting where SSLB='')";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }
        public DataTable GetZhenduanByNAME(string str)
        {
            string selectAllYS = " SELECT ZDNAME FROM JiBingZhenduan where SUOXIE like '%" + str + "%'";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }
        /// <summary>
        /// 根据时间查询手术总量
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllMZLbyTime(DateTime dt1,DateTime  dt2)
        {
            string selectbyTime = "select count(*) from Adims_Mzjld  WHERE  otime between '"+dt1+"'and'"+dt2+"'";
            return dBConn.GetDataTable(string.Format(selectbyTime));
        }
      
        /// <summary>
        /// 根据医生查询手术总量
        /// </summary>
        /// <returns></returns>
        public DataTable GetYSMZLbyTime( int sunall,DateTime dt1,DateTime  dt2,string name)
        {
            string selectysbyTime = "SELECT sum(DATEDIFF ( mi , mzkssj , mzjssj )),count(*),SUBSTRING(cast(count(*)*100.0/" + sunall + "  as nvarchar),1,4) +' %' from Adims_Mzjld "
                    + " WHERE   Otime between '" + dt1 + "'and'" + dt2 + "' and mzys like '%" + name + "%'";
            return dBConn.GetDataTable(string.Format(selectysbyTime));
        }
        /// <summary>
        /// 根据手术间查询手术总量
        /// </summary>
        /// <returns></returns>
        public DataTable GetYSMZLbyOroom(int sunall, DateTime dt1, DateTime dt2, string name)
        {

            string selectysbyTime = "SELECT  sum(DATEDIFF ( mi , mzkssj , ssjssj )) as mzsj,count(*) as ssNum,SUBSTRING(cast(count(*)*100.0/" + sunall + " as nvarchar),1,4) +' %' as Probability  from Adims_Mzjld as M join Adims_OTypesetting as O on O.PatID=M.PatID WHERE  Otime between '" + dt1 + "'and'" + dt2 + "' and O.Oroom like '%" + name + "%' ";
            return dBConn.GetDataTable(string.Format(selectysbyTime));
        }
        public DataTable GetYSMZLbyOroom1(int sunall, DateTime dt1, DateTime dt2) 
        {

            string selectysbyTime = "SELECT oroom, sum(DATEDIFF ( mi , mzkssj , ssjssj )) as mzsj,count(*) as ssNum,SUBSTRING(cast(count(*)*100.0/" + sunall + " as nvarchar),1,4) +' %' as Probability  from Adims_Mzjld as M join Adims_OTypesetting as O on O.PatID=M.PatID WHERE  Otime between '" + dt1 + "'and'" + dt2 + "' group by oroom ";
            return dBConn.GetDataTable(string.Format(selectysbyTime));
        }

        /// <summary>
        /// 根据科室查询手术总量
        /// </summary>
        /// <returns></returns>
        public DataTable GetYSMZLbyOKeshi(int sunall, DateTime dt1, DateTime dt2, string name)
        {
            string selectysbyTime = "SELECT sum(DATEDIFF ( mi , mzkssj , mzjssj )),count(*),count(*)/'" + sunall + "' from Adims_Mzjld as M join Adims_OTypesetting as O on O.PatID=M.PatID"
                                + " WHERE   time between '" + dt1 + "'and'" + dt2 + "' and O.Patdpm like '%" + name + "%' ";
           return dBConn.GetDataTable(string.Format(selectysbyTime));
        }
        public DataTable GetYSMZLbyOKeshi1( DateTime dt1, DateTime dt2)
        {
            string selectysbyTime = "select ao.patdpm, sum(DATEDIFF ( mi , mzkssj , mzjssj )),count(*) from Adims_OTypesetting as ao join Adims_Mzjld as am on ao.patid=am.patid "
                                   + " WHERE   otime between '" + dt1 + "'and'" + dt2 + "' group by ao.Patdpm "; 
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
        public DataTable GetIsEmployeesList(string divis,string time )
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
        public DataTable GetallQxModel( )
        {
            string selct = "select * from Admins_qxqdType ";
            return dBConn.GetDataTable(string.Format(selct));
        }
        // <summary>
        /// 查询所有器械模板名
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetallQxModelss(string name)
        {
            string selct = "select * from Admins_qxqdType where id not in (select id from Admins_qxqdType where qxbType='"+name+"')";
            return dBConn.GetDataTable(string.Format(selct));
        }
        public DataTable GetallQxModelss()
        {
            string selct = "select top 1 * from Admins_qxqdType order by id ";
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
        public int SelectqxqdModel(int mod)
        {
            string countQJ = "Insert into Adims_qxqdModel(qxqd_id) values(" + mod + ") ";
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
        public int UpdatesqxqdModel(int mod,string name,string text)
        {
            string countQJ = "update Adims_qxqdModel set " + name + "='" + text + "' where id="+mod+"";
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
        /// 查询器械模板详细数据
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable SelectqxmcInmodel(string mod)
        {
            string countQJ = "select m.id,m.qxmc,m.qxCount,t.qxbType from Adims_qxqdModel as m left OUTER JOIN Admins_qxqdType as t on t.id=m.qxqd_id where t.qxbType='" + mod + "'";
            return dBConn.GetDataTable(string.Format(countQJ));
        }
        /// <summary>
        /// 统计个数
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public DataTable SelectNum(string mod)
        {
            string countQJ = "select count(*) from V_QXB where qxbType='" + mod + "'";
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
       /// <summary>
       /// 查询器械包
       /// </summary>
       /// <param name="mod"></param>
       /// <returns></returns>
        public DataTable SelectqxmType(string mod)
        {
            string countQJ = "select id,qxbType from Admins_qxqdType where qxbType='" + mod + "' ";
            return dBConn.GetDataTable(string.Format(countQJ));
        }
        /// <summary>
        /// 修改器械包
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public int SelectqxType(string mod,string cmb)
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
        #region 麻醉月报表统计

       
        public DataTable GetMazuifangfa(string where)
        {
            string Insert = "select * from mazuifangfa where {0}";
            return dBConn.GetDataTable(string.Format(Insert,where));
        }
        public string GetAboutMZJLD(string where,string  dt1,string dt2)
        {
            string Insert = "select COUNT(*) from adims_mzjld where {0} and convert(nvarchar(10),otime,23) BETWEEN '" + dt1 + "' AND '" + dt2 + "'";
            DataTable dt= dBConn.GetDataTable(string.Format(Insert, where));
            return dt.Rows[0][0].ToString();
        }
          public string GetAboutAfterVisit(string where,string  dt1,string dt2)
        {
            string Insert = "select COUNT(*) from Adims_AfterVisit_CJ where {0} and convert(nvarchar(10),VisitDate,23) BETWEEN '" + dt1 + "' AND '" + dt2 + "'";
            DataTable dt= dBConn.GetDataTable(string.Format(Insert, where));
            return dt.Rows[0][0].ToString();
        }
          public string GetAboutAfterVisittlj(string where, string dt1, string dt2)
          {
              string Insert = "select COUNT(*) from tljmzxj left join adims_mzjld on tljmzxj.zhuyuanhao=adims_mzjld.patid where {0} and convert(nvarchar(10),adims_mzjld.otime,23) BETWEEN '" + dt1 + "' AND '" + dt2 + "'";
              DataTable dt = dBConn.GetDataTable(string.Format(Insert, where));
              return dt.Rows[0][0].ToString();
          }

          public string GetAboutAftermenzhen(string where, string dt1, string dt2)
          {
              string Insert = "select COUNT(*) from MZmzsqfs where {0} and convert(nvarchar(10),riqi,23) BETWEEN '" + dt1 + "' AND '" + dt2 + "'";
              DataTable dt = dBConn.GetDataTable(string.Format(Insert, where));
              return dt.Rows[0][0].ToString();
          }
        public string GetAboutMZZJ(string where, string dt1, string dt2)
        {
            string Insert = "select COUNT(*) from Adims_Mzjld where  {0} and convert(nvarchar(10),otime,23) BETWEEN '" + dt1 + "' AND '" + dt2 + "'";
            DataTable dt = dBConn.GetDataTable(string.Format(Insert, where));
            return dt.Rows[0][0].ToString();
        }
        public string GetAboutMZZB(string where, string dt1, string dt2)
        {
            string Insert = "select COUNT(*) from Tsmztj where {0} and convert(nvarchar(10),shiian,23) BETWEEN '" + dt1 + "' AND '" + dt2 + "'";
            DataTable dt = dBConn.GetDataTable(string.Format(Insert, where));
            return dt.Rows[0][0].ToString();
        }
        public string GetAboutPACU(string where, string dt1, string dt2)
        {
            string Insert = "select COUNT(*) from Adims_PACU where {0} and convert(nvarchar(10),RSTime,23) BETWEEN '" + dt1 + "' AND '" + dt2 + "'";
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
            string sql = "SELECT * from Admins_Mbbtj  where year(tj_date)='" + date + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 判断统计的数据是否存在
        /// </summary>
        /// <param name="YF"></param>
        /// <returns></returns>
        public DataTable SelectYBBTJ(string YF,string date)
        {
            string sql = "SELECT * from Admins_Mbbtj  where tj_YF='" + YF + "' and year(tj_date)='" + date + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 添加统计
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertYBBTJ(Dictionary<string, string> dictionary,string date)
        {
            string INSERT = @"INSERT INTO [Admins_Mbbtj] ([tj_date]
           ,[tj_YF]
           ,[ygzl]
           ,[sjzz]
           ,[xfff]
           ,[wutwjing]
           ,[asa1]
           ,[asa2]
           ,[asa3]
           ,[asa4]
           ,[asa5]
           ,[yitixue]
           ,[chagqm]
           ,[wudrenliu]
           ,[smazuiff]
           ,[shuszt]
           ,[mzksssqx]
           ,[zpacuys]
           ,[pacutiwend]
           ,[fjhzricu]
           ,[fjheccg]
           ,[zitixue]
           ,[mazuixfhm]
           ,[cgdzsysy]
           ,[zfyqxgsj]
           ,[mzcxspo2d]
           ,[qifyqsj]
           ,[mazui24xzzt]
           ,[mzqjgmfy]
           ,[sjmcc]
           ,[stw4]
           ,[mzuiysza]
           ,[mzsycxyw]
           ,[mzywsw]
           ,[mz24xssw]
           ,[zgnmzyzbfz]
           ,[zhongxinjmbfz]) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}', '{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}')";
            string sql = string.Format(INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 更新统计
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateYBBTJ(Dictionary<string, string> dictionary,string YF,string date)
        {
            string INSERT = @"UPDATE [Admins_Mbbtj] SET [tj_date] = '{0}'
      ,[tj_YF] ='{1}'
      ,[ygzl] = '{2}'
      ,[sjzz] = '{3}'
      ,[xfff] = '{4}'
      ,[wutwjing] = '{5}'
      ,[asa1] ='{6}'
      ,[asa2] = '{7}'
      ,[asa3] = '{8}'
      ,[asa4] = '{9}'
      ,[asa5] ='{10}'
      ,[yitixue] = '{11}'
      ,[chagqm] = '{12}'
      ,[wudrenliu] = '{13}'
      ,[smazuiff] = '{14}'
      ,[shuszt] = '{15}'
      ,[mzksssqx] = '{16}'
      ,[zpacuys] = '{17}'
      ,[pacutiwend] = '{18}'
      ,[fjhzricu] = '{19}'
      ,[fjheccg] = '{20}'
      ,[zitixue] = '{21}'
      ,[mazuixfhm] = '{22}'
      ,[cgdzsysy] = '{23}'
      ,[zfyqxgsj] = '{24}'
      ,[mzcxspo2d] = '{25}'
      ,[qifyqsj] = '{26}'
      ,[mazui24xzzt] = '{27}'
      ,[mzqjgmfy] = '{28}'
      ,[sjmcc] = '{29}'
      ,[stw4] = '{30}'
      ,[mzuiysza] = '{31}'
      ,[mzsycxyw] = '{32}'
      ,[mzywsw] = '{33}'
      ,[mz24xssw] = '{34}'
      ,[zgnmzyzbfz] = '{35}'
      ,[zhongxinjmbfz] = '{36}'
  WHERE [tj_YF]='" + YF + "' and [tj_date]='" + date + "'";
            string sql = string.Format(INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        #endregion
        public int Insertmazuifangfa(string name)
        {
            string Insert = "Insert into mazuifangfa(name) values('" + name + "') ";
            return dBConn.ExecuteNonQuery(string.Format(Insert));
        }
        public int DeleteMazuifangfa(int id)
        {
            string Insert = "delete from mazuifangfa where id='"+id+"' ";
            return dBConn.ExecuteNonQuery(string.Format(Insert));
        }

        /// <summary>
        /// 修改器械清点模板
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateqxqdModel(List<string> dictionary)
        {
            string SQL_LEAVEREGISTRATION_UPDATE = "UPDATE  [Adims_LeaveRegistration] SET [EmployeesNO] = '{0}',[EmployeesName] = '{1}',[StartDate] = '{2}',[EndDate] = '{3}',[SumDay] = '{4}',[LeaveReason] = '{5}',[UpdateDate] = GETDATE() WHERE LeaveRegistrationID = '{6}'";
       
            return dBConn.ExecuteNonQuery(string.Format(SQL_LEAVEREGISTRATION_UPDATE, dictionary.ToArray()));
        }

        /// <summary>
        /// 删除器械模板
        /// </summary>
        /// <param name="employeesID"></param>
        /// <returns></returns>
        public int DeleteqxqdModel(string employeesID)
        {
            string sql = "";
            return dBConn.ExecuteNonQuery(string.Format(sql, employeesID));
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
        #region 手术登记
        /// <summary>
        /// 按时间查询排班信息
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
         public DataTable GetHSDJ(string dt)
        {
            string SQL_SELECT = "SELECT * from Adims_OTypesetting WITH (NOLOCK) WHERE CONVERT(varchar, Odate , 23 ) = '" + dt + "' ";            
            return dBConn.GetDataTable(SQL_SELECT);
        }

         public DataTable SelectMZ(string patid, string odate)
         {
             string sql = "SELECT * from Adims_Mzjld  where patid='" + patid + "' and DtTime='" + odate + "'";
             return dBConn.GetDataTable(sql);
         }
        /// <summary>
        /// 查询手术登记信息
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="odate"></param>
        /// <returns></returns>
         public DataTable SelectHSDJs(string patid, string odate)
         {
             string sql = "SELECT * from Adims_HSDJ  where patid='" + patid + "' and DtTime='" + odate + "'";
             return dBConn.GetDataTable(sql);
         }
         public DataTable SelectHSDJ(string odate,string ks)
         {
             string sql = @"SELECT [id],CONVERT(varchar(12) , DtTime, 111 ) as DtTime,[keshi] ,[SSJ] ,[CH],[DJNames],[patid],[Age],[Sex],[SSName] ,[SSRY] ,[MZS],[QXHS],[XHHS],[SY],[JZ],[ZJ],[SanSJ],[QK],[BZ] 
  FROM [HeYiAMIS_CJ].[dbo].[Adims_HSDJ] where  DtTime='" + odate + "' and keshi='" + ks + "'";
             return dBConn.GetDataTable(sql);
         }
         public DataTable SelectHSDJ(string odate)
         {
             string sql = @"SELECT [id],CONVERT(varchar(12) , DtTime, 111 ) as DtTime ,[keshi],[SSJ] ,[CH],[DJNames],[patid],[Age],[Sex],[SSName] ,[SSRY] ,[MZS],[QXHS],[XHHS],[SY],[JZ],[ZJ],[SanSJ],[QK],[BZ] 
  FROM [HeYiAMIS_CJ].[dbo].[Adims_HSDJ] where  DtTime='" + odate + "'";
             return dBConn.GetDataTable(sql);
         }
        /// <summary>
        /// 护士统计功能
        /// </summary>
        /// <param name="odate"></param>
        /// <returns></returns>
         public DataTable SelectHushiTJ(string date1, string date2)
         {
             string sql = @"SELECT [id],CONVERT(nvarchar(12) , DtTime, 23 ) as DtTime ,[keshi],[SSJ] ,[CH],[DJNames],[patid],[Age],[Sex],[SSName] ,[SSRY],[SY],[JZ],[ZJ],[SanSJ],[QK] 
  FROM [HeYiAMIS_CJ].[dbo].[Adims_HSDJ] where  DtTime between '" + date1 + "'and'" + date2 + "'";
             return dBConn.GetDataTable(sql);
         }
        /// <summary>
        /// 按要求查询
        /// </summary>
        /// <param name="name"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
         public DataTable HuShiName(string name, string date1, string date2)
         {
             string select1 = @"SELECT [id],CONVERT(nvarchar(12) , DtTime, 23 ) as DtTime ,[keshi],[SSJ] ,[CH],[DJNames],[patid],[Age],[Sex],[SSName] ,[SSRY],[SY],[JZ],[ZJ],[SanSJ],[QK] 
  FROM [HeYiAMIS_CJ].[dbo].[Adims_HSDJ] where  DtTime between '" + date1 + "'and'" + date2 + "'and ("+name+"='是' or "+name+" is not null)";
             return dBConn.GetDataTable(string.Format(select1));

         }

        /// <summary>
        /// 查询排班科室
        /// </summary>
        /// <param name="odate"></param>
        /// <returns></returns>
         public DataTable SelectHSDJKS()
         {
             string sql = "SELECT distinct Patdpm from Adims_OTypesetting ";
             return dBConn.GetDataTable(sql);
         }
        /// <summary>
         ///添加护士登记信息 distinct
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
         public int InsertHSDj(List<string> list1)
         {
             string _INSERT = @"INSERT INTO [HeYiAMIS_CJ].[dbo].[Adims_HSDJ]([DtTime],[SSJ],[CH],[DJNames],[patid],[Age],[Sex],[SSName],[SSRY]
 ,[MZS],[QXHS],[XHHS],[JZ],[keshi])"
                      + " VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')";
             string INSERT = string.Format(_INSERT, list1.ToArray());
             return dBConn.ExecuteNonQuery(INSERT);
         }
        /// <summary>
        /// 修改护士登记
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="DateType"></param>
        /// <param name="value"></param>
        /// <param name="Odate"></param>
        /// <returns></returns>
         public int UpdateHSDJ(string ID, string DateType, string value, string Odate)
         {
             string sql = "UPDATE Adims_HSDJ WITH (ROWLOCK) SET " + DateType + " = '" + value + "'  WHERE id = '" + ID + "' and DtTime='" + Odate + "'";
             return dBConn.ExecuteNonQuery(sql);
         }
        /// <summary>
        /// 删除护士登记
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
         public int DeleteHSDJ(string ID)
         {
             string sql = "delete Adims_HSDJ  WHERE id = '" + ID + "'";
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
        private static readonly string SQL_OTYPESETTING_SELECT = "SELECT oroom,second,StartTime,patid,patdpm,patname,"
                + "patsex,patage,patbedno,patNation,pattmd,oname,os,on1,on2,sn1,sn2,ap1,ap2,ap3,tiwei,bx,gr,remarks,sqys,zrys,id,Amethod,expertName"
                + "  from Adims_OTypesetting WITH (NOLOCK) WHERE {0} ORDER BY oroom";
        public DataTable GetOTypesetting(string oroom, string dt)
        {
            string sqlWhere = "CONVERT(varchar, Odate , 23 ) = '" + dt + "'";
            if (oroom != "全部手术间")
                sqlWhere += " AND oroom = '" + oroom + "'";
            return dBConn.GetDataTable(string.Format(SQL_OTYPESETTING_SELECT,sqlWhere));
        }
        public DataTable GetIsJZPaiban(string dt)
        {
            string sqlWhere = "CONVERT(varchar, Odate , 23 ) = '" + dt + "'";
            sqlWhere += " AND isjizhen = '1'";
            return dBConn.GetDataTable(string.Format(SQL_OTYPESETTING_SELECT, sqlWhere));
        }
        public DataTable GetIsPaiban(int ostate, string dt)
        {
            string sqlWhere = "CONVERT(varchar, Odate , 23 ) = '" + dt + "'";
            sqlWhere += " AND ostate >= '" + ostate + "'";
            return dBConn.GetDataTable(string.Format(SQL_OTYPESETTING_SELECT, sqlWhere));
        }
        public DataTable GetIsPaibans(int ostate, string dt)
        {
            string sqlWhere = "CONVERT(varchar, Odate , 23 ) = '" + dt + "'";
            sqlWhere += " AND ostate = '" + ostate + "'";
            return dBConn.GetDataTable(string.Format(SQL_OTYPESETTING_SELECT, sqlWhere));
        }
        public DataTable GetPaibanResult(int ostate, string dt,string where)
        {
            string SQL_SELECT = "SELECT oroom,second,StartTime,substring(patid,3,10) as patid,patdpm,Patname + '(' + Patsex + '/' + Patage  + '岁' + ')' AS patname"
                + ",patbedno,patNation,pattmd,case when isjizhen='0' then '择期' when isjizhen='1' then '急诊' end isjizhen,oname,amethod, convert(nvarchar,os)+' '+convert(nvarchar,os1)+' '+convert(nvarchar,os2) as os,tiwei,bx,gr,remarks,on1+ ' '+on2 + ' '+sn1+ ' '+sn2 as hushi,ap1+ ' '+ap2+ ' '+ap3 as ap,sqys,zrys"
                + "  from Adims_OTypesetting WITH (NOLOCK) WHERE CONVERT(varchar, Odate , 23 ) = '" + dt + "' AND ostate >= '" + ostate + "' ";
            string sql = string.Format(SQL_SELECT + where);
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetPaiXu(string dt, string where)
        {
            string SQL_SELECT = "SELECT oroom,second,StartTime,patid,patdpm,patname,"
                + "patsex,patage,patbedno,patNation,pattmd,oname,os,on1,on2,sn1,sn2,ap1,ap2,ap3,tiwei,bx,gr,remarks,sqys,zrys,Amethod,id"
                + "  from Adims_OTypesetting WITH (NOLOCK) WHERE CONVERT(varchar, Odate , 23 ) = '" + dt + "' ";
            string sql = string.Format(SQL_SELECT + where);
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetPaiXu(int ostate, string dt, string where)
        {
            string SQL_SELECT = "SELECT oroom,second,StartTime,patid,patdpm,patname,"
                + "patsex,patage,patbedno,patNation,pattmd,oname,os,on1,on2,sn1,sn2,ap1,ap2,ap3,tiwei,bx,gr,remarks,sqys,zrys,Amethod,id"
                + "  from Adims_OTypesetting WITH (NOLOCK) WHERE CONVERT(varchar, Odate , 23 ) = '" + dt + "' AND ostate = '" + ostate + "' ";
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
            DataTable mydt=new DataTable();
            mydt=ds.Tables[0];
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
        public int SaveOTypesetting(adims_MODEL.paiban pb)
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
        public int UpdatePaiban(string patID, string DateType, string value, string ostate, DateTime Odate)
        {
            string sql = "UPDATE Adims_OTypesetting WITH (ROWLOCK) SET " + DateType + " = '" + value + "',ostate='" + ostate + "' WHERE patid = '" + patID + "' and Odate='" + Odate + "'";
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
        /// <summary>
        /// 查询麻醉会诊单
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        public DataTable GetMZHZD(string patid, string CardID)
        {
            string sql = "SELECT * from Adims_MZHZD  where patid='" + patid + "' and sqdh='" + CardID + "'";

            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 查询访视信息
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetBeforeVisit_YS(string patid,string CardID)
        {
            string sql = "SELECT * from Adims_BeforeVisit_YS  where patid='" + patid + "' and sqdh='" + CardID + "'";

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
        /// 添加麻醉会诊单
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertMZHZD(Dictionary<string, string> dictionary)
        {
            string INSERT = @"INSERT INTO [HeYiAMIS_CJ].[dbo].[Adims_MZHZD]([IsRead],[patid] ,[SSlx],[SQQK],[YBQK],[T],[BP],[R],[P],[TZ]
,[XSS],[HXB],[XJ],[XN] ,[XT],[XZGN],[FGN] ,[SGN],[GGN],[TSQK],[QT],[YYQZ],[DaTime],[sqdh]) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}',"
                     + "'{21}','{22}','{23}')";
            string sql = string.Format(INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 添加术前访视(医生)
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertBeforeVisit_YS(Dictionary<string, string> dictionary)
        {
           string INSERT= "INSERT INTO Adims_BeforeVisit_YS(patid,ASA,isJizhen,FeiPang,Baowei,YNYWS,Weight,Jixing,Jingzhui,ZKKN,ZKdu,Jiaya,HuxiKN,"
                    + " Mallam,Xinzang,Gaoxueya,GuanxinB,FeiGN,FeibuJB,GanGN,ShenGN,SJXTJB,OtherCheck ,MZFXPG,Jinshi,Jinyin,OtherZhuyi,MZFZCS,MZYS,ApplyDate,IsRead,MZFF,sqdh)"
                    + " VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}',"
                    + "'{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}')";
           string sql = string.Format(INSERT, dictionary.Values.ToArray());
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
        public int UpdateBeforeVisit_HS_STATE()
        {
            string sql = "UPDATE Adims_BeforeVisit_HS  SET IsRead='1'";
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 更新麻醉会诊单
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateMZHZD(Dictionary<string, string> dictionary)
        {
            string update_MZHZD = @"UPDATE [HeYiAMIS_CJ].[dbo].[Adims_MZHZD] SET [IsRead] ='{0}',[SSlx] ='{2}',[SQQK] ='{3}',[YBQK] ='{4}',[T] ='{5}',[BP] ='{6}',[R] ='{7}',[P] ='{8}',[TZ] ='{9}' ,[XSS] ='{10}',[HXB] ='{11}',[XJ] ='{12}',[XN] ='{13}',[XT] ='{14}',[XZGN] ='{15}' ,[FGN] ='{16}',[SGN] ='{17}',[GGN] ='{18}',[TSQK] ='{19}',[QT] ='{20}',[YYQZ] ='{21}'
,[DaTime] ='{22}',[sqdh] ='{23}' WHERE [patid] = '{1}'";
            return dBConn.ExecuteNonQuery(string.Format(update_MZHZD, dictionary.Values.ToArray()));
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
                + ",SJXTJB='{21}',OtherCheck='{22}',MZFXPG='{23}',Jinshi='{24}',Jinyin='{25}',OtherZhuyi='{26}',MZFZCS='{27}',MZYS='{28}',ApplyDate='{29}',MZFF='{31}',sqdh='{31}' "
                + "where patid='{0}'";
            return dBConn.ExecuteNonQuery(string.Format(update_YS, dictionary.Values.ToArray()));
        }
        /// <summary>
        /// 更新术前访视(医生）存档状态
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateBeforeVisit_YS_STATE(string patid,int state)
        {
            string sql = "UPDATE Adims_BeforeVisit_YS  SET IsRead='"+state+"' where sqdh='"+patid+"' ";
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 更新随访第二天 
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int UpdateBeforeVisit_YS_STATE2(string patid, int state)
        {
            string sql = "UPDATE Adims_AfterVisit_CJ2  SET IsRead='" + state + "' where sqdh='" + patid + "' ";
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 更新麻醉计划单状态（存档，解锁）
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int UpdateMZJHD(string patid, int state)
        {
            string sql = "UPDATE Adims_MZJHD  SET IsRead='" + state + "' where sqdh='" + patid + "' ";
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 麻醉会诊单 存档
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int UpdateMZHZD_STATE(string patid, int state)
        {
            string sql = "UPDATE Adims_MZHZD  SET IsRead='" + state + "' where sqdh='" + patid + "'";
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

        #region <<麻醉总结>>

        /// <summary>
        /// 麻醉总结名单查询
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetmazuizongjieList(string dt)
        {
            string sql = "select  A.id ,B.patid,B.patname,B.patzhuyuanid,B.Patbedno,B.patsex,B.patage from Adims_Mzjld as A LEFT join "
                    + "Adims_OTypesetting AS B ON A.PATID=B.PATID where CONVERT(varchar, A.Otime , 23 ) = '" + dt + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetmazuizongjieListss(string dt)
        {
            string sql = "select  A.id ,B.patid,B.patname,B.patzhuyuanid,B.Patbedno,B.patsex,B.patage,B.CardID from Adims_Mzjld as A LEFT join "
                    + "Adims_OTypesetting AS B ON A.PATID=B.PATID where CONVERT(varchar, A.Otime , 23 ) = '" + dt + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetMzjldBYtime(string dt1)
        {
            string sql = "select  A.id ,B.patid,B.patname,B.patzhuyuanid,B.Patbedno,B.patsex,B.patage from Adims_Mzjld as A LEFT join "
                    + "Adims_OTypesetting AS B ON A.PATID=B.PATID where CONVERT(varchar, A.Otime , 23 ) = '" + dt1 + "'";
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
        public DataTable GetMzjldByPatid(string Patid)
        {
            string sql = "select * from Adims_mzjld where Patid='" + Patid + "'";
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
        /// <summary>
        /// 验证是否存在病人麻醉镇痛info
        /// </summary>
        /// <param name="patID">病人ID</param>
        /// <returns></returns>
        public DataTable GetmzzhentonginfoCount(string mzID,string type)
        {
            string sql = "select * from Adims_mzzhengtongInfo where mzjldID='" + mzID + "'and dataType='" + type + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetMZZJ_SZ(string mzID)
        {
            string sql = "select * from Adims_mzzongjie_SZ where mzjldID='" + mzID + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetMZZJ_CJ(string mzID)
        {
            string sql = "select * from Adims_Mzzj_CJ where mzjldID='" + mzID + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetMazuiPingmian(string mzID)
        {
            string sql = "select name from MazuiPingmian where Name like '" + mzID + "%'";
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
            string inst=string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);

        }
        public int InsertMZZJ_CJ(Dictionary<string, string> dictionary)
        {
            string _INSERT = "INSERT INTO Adims_Mzzj_CJ(mzjldID,patID,AddTime,QSMZ,YoudaoFF,ZGNMZ,ZGNMZFF,CCD11,CCD12"
                + ",liuzhiSD1,CCD21,CCD22,liuzhiSD2,MZPM_ZG,Yaopin_ZG,SJZZ,JCSJZZ"
                + ",BCSJZZ,YCSJZZ,ZGSJZZ,GSJZZ,GWCSJZZ,Yaopin_SJZZ,YCCZ,DMCCZG,SJMCCZG,OtherZZ,HZ,HZType,HZwcff,MZJH,RemarkMZ"
                + ",TaoNang,Jili,KesouTunyan,Dingxiangli,Yishi,MZPM_OUT,RemarkOut,BRQX,TSQK,MZYS"
                + ",QGCG,QGCGwz1,QGCGwz2,QGCGwz3,QGCGtype,QGCGsd,BRZKZT,isRead,QTNum) "
                + "values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}',"
                + "'{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}')";
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
        public int UpdateMZZJ_CJ(Dictionary<string, string> dictionary)
        {
            string upMZZJ = "UPDATE Adims_Mzzj_CJ WITH (ROWLOCK) SET UpdateTime= GETDATE() ,QSMZ= '{3}',YoudaoFF= '{4}',ZGNMZ= '{5}',ZGNMZFF= '{6}',CCD11= '{7}',CCD12= '{8}',liuzhiSD1= '{9}'"
                + ",CCD21= '{10}',CCD22= '{11}',liuzhiSD2= '{12}',MZPM_ZG= '{13}',Yaopin_ZG= '{14}',SJZZ= '{15}',JCSJZZ= '{16}',BCSJZZ= '{17}',YCSJZZ= '{18}',ZGSJZZ= '{19}'"
                + ",GSJZZ= '{20}',GWCSJZZ= '{21}',Yaopin_SJZZ= '{22}',YCCZ= '{23}',DMCCZG= '{24}',SJMCCZG= '{25}',OtherZZ= '{26}',HZ= '{27}',HZType= '{28}',HZwcff= '{29}'"
                + ",MZJH= '{30}',RemarkMZ= '{31}',TaoNang= '{32}',Jili= '{33}',KesouTunyan= '{34}',Dingxiangli= '{35}',Yishi= '{36}',MZPM_OUT= '{37}',RemarkOut= '{38}',BRQX= '{39}'"
                + ",TSQK= '{40}',MZYS= '{41}',QGCG= '{42}',QGCGwz1= '{43}',QGCGwz2= '{44}',QGCGwz3= '{45}',QGCGtype= '{46}',QGCGsd= '{47}',BRZKZT= '{48}',isRead= '{49}',QTNum='{50}' WHERE mzjldID ='{0}' ";
            string upMZZJ1 = string.Format(upMZZJ, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(upMZZJ1);

        }
        public int UpdateMZZJ_CJ(string mzid,int isread)
        {
            string sql = "UPDATE Adims_MZZJ_CJ  SET IsRead='" + isread + "' where mzjldid='" + mzid + "' ";
            return dBConn.ExecuteNonQuery(sql);
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
            +"remarkday='" + remarkday + "'where mzjldID='" + mzid + "'and dataType='" + type + "'";

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
        public DataTable GetNurseRecord(string mzjldid)
        {
            string sql = "Select * from Adims_NurseRecord_CJ where mzjldid='" + mzjldid + "'";
            return dBConn.GetDataTable(sql);
        }
        public int UpdateNurseRecord_State(string mzid, int isRead)
        {
            string sql = "UPDATE Adims_NurseRecord_CJ  SET IsRead='"+isRead+"' where mzjldid='" + mzid + "'";
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
            string sql = @"SELECT [QXname],[SQ],[GQ],[GH],[Qxname1],[SQ1],[GQ1],[GH1],[Qxname2],[SQ2],[GQ2],[GH2],[Id] FROM [HeYiAMIS_CJ].[dbo].[Adims_HLQXQD] where mzjldid='" + mzjldid + "' order by id";
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
            string _insert = @"INSERT INTO [HeYiAMIS_CJ].[dbo].[Adims_HLQXQD]([patid],[mzjldid],[QXB],[QXname],[SQ] ,[GQ],[GH] ,[Qxname1],[SQ1] ,[GQ1],[GH1],[Qxname2],[SQ2] ,[GQ2],[GH2])
     VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')";
            string sql = string.Format(_insert, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        #endregion
        #region<<PACU盛泽>>
        public DataTable GetPACU_SZ(int  mzid)
        {
            string select = "select * FROM Adims_PACU_SZ  where mzjldid='" + mzid + "'";
            return dBConn.GetDataTable(string.Format(select));
        }
        public int InsertPACU_SZ(int  mzid,DateTime savetime)
        {
            string insert = "INSERT INTO Adims_PACU_SZ (mzjldid,SaveTime) values('" + mzid + "','" + savetime + "')";
            return dBConn.ExecuteNonQuery(string.Format(insert));
        }
        public int Update_single_PACU_SZ(string ZDming, DateTime time,int mzid)
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
        public DataTable GetAllFileNameByClass(string  fileClass)
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
            string insert = "INSERT INTO Adims_SourceFileName (SourceFileName ,FileClass,CreatAuthor,CreatTime)VALUES('" + filename + "','" + fileclass + "','" + CreatAuthor + "','"+creattime+"')";
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
        public int UpdateAdims_SourceFile(string rowtext,int indexrow)
        {
            string update = "update Adims_SourceFile set";
            return dBConn.ExecuteNonQuery(string.Format(update));
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="employeesID"></param>
        /// <returns></returns>
        public int DeleteAdims_SourceFileName (string filename)
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
        public DataTable GetYTYYreport(int MZID )
        {
            string STR = "SELECT * FROM Adims_Ytuse WHERE mzjldid='" + MZID + "'";
            return dBConn.GetDataTable(string.Format(STR));
        }
         public DataTable GetTSYYreport(int MZID )
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
        public DataTable GetAfterVisitCount_CJ(string patID, string CardID)
        {
            string sql = "select *  from Adims_AfterVisit_CJ where patID='" + patID + "' and sqdh='" + CardID + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 麻醉计划单
        /// </summary>
        /// <param name="patID"></param>
        /// <returns></returns>
        public DataTable GetMZJHD(string patID, string CardID)
        {
            string sql = "select *  from Adims_MZJHD where patID='" + patID + "' and sqdh='" + CardID + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 验证病人术后第二天信息是否存在
        /// </summary>
        /// <param name="patID">病人ID</param>
        /// <returns></returns>
        public DataTable GetAfterVisitCount_CJ2(string patID, string CardID)
        {
            string sql = "select *  from Adims_AfterVisit_CJ2 where patID='" + patID + "'  and sqdh='" + CardID + "'";
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
        /// 麻醉计划单
        /// </summary>
        /// <param name="afterVisitList"></param>
        /// <returns></returns>
        public int InsertMZJHD(Dictionary<string, string> afterVisitList)
        {
            string sqlnsert = @"INSERT INTO [HeYiAMIS_CJ].[dbo].[Adims_MZJHD]([IsRead] ,[patid] ,[NXmzfs] ,[NCCD],[BXmzfs],[BCCD],[NXmzyp],[NXqtyp],[NJCXM],[QT],[Mzfzcs],[SZfxkl] ,[NseBei],[SHzr],[MZYS],[MzjhDate],[sqdh])
     VALUES( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}')";
            string sql = string.Format(sqlnsert, afterVisitList.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 添加术后第一天随访
        /// </summary>
        /// <param name="afterVisitList"></param>
        /// <returns></returns>
        public int InsertAfterVisit_CJ(Dictionary<string, string> afterVisitList)
        {
           string sqlnsert = "INSERT INTO Adims_AfterVisit_CJ (patid,Yishi,Xueya,Huxi,Xinlv,YBZZ,OtherZZ,YishiSJ,XueyaSJ,HuxiSJ,"
            + "XinlvSJ,Exin,ChuanciBW,ZhitiHD,ShuhouZT,ZTBPF,ZTXG,MZYS,VisitDate ,isread,BZ,sqdh)"
            + " VALUES ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}')";

           string sql = string.Format(sqlnsert, afterVisitList.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 添加术后第二天随访
        /// </summary>
        /// <param name="afterVisitList"></param>
        /// <returns></returns>
        public int InsertAfterVisit_CJ2(Dictionary<string, string> afterVisitList)
        {
            string sqlnsert = "INSERT INTO Adims_AfterVisit_CJ2 (patid,Yishi,Xueya,Huxi,Xinlv,YBZZ,OtherZZ,YishiSJ,XueyaSJ,HuxiSJ,"
             + "XinlvSJ,Exin,ChuanciBW,ZhitiHD,ShuhouZT,ZTBPF,ZTXG,MZYS,VisitDate ,isread,BZ,sqdh)"
             + " VALUES ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}')";

            string sql = string.Format(sqlnsert, afterVisitList.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 更新麻醉计划单
        /// </summary>
        /// <param name="afterVisitList"></param>
        /// <returns></returns>
        public int UpdateMZJHD(Dictionary<string, string> afterVisitList)
        {
            string SQLUPDATE2 = @"UPDATE [HeYiAMIS_CJ].[dbo].[Adims_MZJHD] SET [IsRead] ='{0}' ,[NXmzfs] ='{2}' ,[NCCD] ='{3}',[BXmzfs] ='{4}',[BCCD] ='{5}',[NXmzyp] ='{6}',[NXqtyp] ='{7}',[NJCXM] ='{8}',[QT] ='{9}',[Mzfzcs] ='{10}',[SZfxkl] ='{11}',[NseBei] ='{12}',[SHzr] ='{13}',[MZYS] ='{14}',[MzjhDate] ='{15}',[sqdh] ='{16}'  WHERE [patid] ='{1}'";
            return dBConn.ExecuteNonQuery(string.Format(SQLUPDATE2, afterVisitList.Values.ToArray()));
        }
        /// <summary>
        /// 更新术后第一天随访
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int UpdateAfterVisit_CJ(Dictionary<string, string> afterVisitList)
        {
            string SQLUPDATE2 = "UPDATE Adims_AfterVisit_CJ WITH (ROWLOCK)  SET Yishi = '{1}',Xueya = '{2}',HuXi='{3}',"
            + " Xinlv= '{4}',YBZZ= '{5}',OtherZZ= '{6}',YishiSJ= '{7}',XueyaSJ= '{8}',HuxiSJ= '{9}',XinlvSJ= '{10}',Exin= '{11}',"
            + "ChuanciBW= '{12}',ZhitiHD= '{13}',ShuhouZT= '{14}',ZTBPF= '{15}',ZTXG= '{16}',MZYS= '{17}',VisitDate= '{18}'"
            + ",isread= '{19}',BZ= '{20}',sqdh= '{21}'  WHERE patID = '{0}' ";

            return dBConn.ExecuteNonQuery(string.Format(SQLUPDATE2, afterVisitList.Values.ToArray()));
        }
        /// <summary>
        /// 更新术后第二天随访
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int UpdateAfterVisit_CJ2(Dictionary<string, string> afterVisitList)
        {
            string SQLUPDATE2 = "UPDATE Adims_AfterVisit_CJ2 WITH (ROWLOCK)  SET Yishi = '{1}',Xueya = '{2}',HuXi='{3}',"
            + " Xinlv= '{4}',YBZZ= '{5}',OtherZZ= '{6}',YishiSJ= '{7}',XueyaSJ= '{8}',HuxiSJ= '{9}',XinlvSJ= '{10}',Exin= '{11}',"
            + "ChuanciBW= '{12}',ZhitiHD= '{13}',ShuhouZT= '{14}',ZTBPF= '{15}',ZTXG= '{16}',MZYS= '{17}',VisitDate= '{18}'"
            + ",isread= '{19}',BZ= '{20}',sqdh= '{21}'  WHERE patID = '{0}' ";

            return dBConn.ExecuteNonQuery(string.Format(SQLUPDATE2, afterVisitList.Values.ToArray()));
        }
        public int UpdateAfterVisit_CJ(string patid, int isread)
        {
            string sql = "UPDATE Adims_AfterVisit_CJ WITH (ROWLOCK)  SET isread = '" + isread + "' WHERE sqdh = '" + patid + "' ";
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
        public DataTable GetMzjldList2(string date1,string date2 )
        {
            string select1 = @"SELECT  * from V_MZZB_MZTJ
               where otime between '" + date1 + "'and'" + date2 + "' order by otime desc";
            return dBConn.GetDataTable(string.Format(select1));
        }
        /// <summary>
        /// 删除多余的新麻醉记录单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int delectadmin_JS_xin(int id)
        {
            string delectmzjldcxtj = " delete from  Adims_mzjld where id=" + id;
            return dBConn.ExecuteNonQuery(delectmzjldcxtj);
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
        public DataTable GetALL_Point(int mzid,int lx)
        {
            string sql ="select * from Adims_Point where mzjldid='"+mzid+"' and lx='"+lx+"'order by time desc";
            return dBConn.GetDataTable(sql);
        }
      
        /// <summary>
        /// 查询检测记录点
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetAdims_mzjld_Point(int mzid)
        {
            string sql = "select RecordTime,NIBPS,NIBPD,Pulse,RRC,TEMP,SpO2,ETCO2,CVP,TOF,NIBPM,HR,BIS from Adims_mzjld_Point where mzjldid='" + mzid + "' order by RecordTime ASC";
            return dBConn.GetDataTable(sql);
        }
        
        public DataTable GetAdims_PACU_Point(int mzid)
        {
            string sql = "select RecordTime,NIBPS,NIBPD,Pulse,RRC,TEMP,SpO2,ETCO2,CVP,TOF,NIBPM,HR from Adims_Pacu_Point where mzjldid='" + mzid + "' order by RecordTime ASC";
            return dBConn.GetDataTable(sql);
        }
        //拷贝检测表到显示表
        public int CopyDataBISsss(int mzjldid, DateTime dtime,string bis)
        {
            string sql = "UPDATE Adims_mzjld_Point set BIS='" + bis + "' where mzjldid='" + mzjldid + "' and RecordTime='" + dtime + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int CopyData(int mzjldid, DateTime dtime)
        {
            string query = "select mzjldid,CreateTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP from Adims_MonitorRecord "
                + " where mzjldid='" + mzjldid + "' and Adims_MonitorRecord.CreateTime='" + dtime + "'";
            DataTable dt = dBConn.GetDataTable(query);
            int i = 0;
            if (dt.Rows.Count>0)
            {
                string copy = "insert into Adims_mzjld_Point(mzjldid,RecordTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP) "
                    + "select mzjldid,CreateTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP from Adims_MonitorRecord "
                    + " where mzjldid='" + mzjldid + "' and Adims_MonitorRecord.CreateTime='" + dtime + "'";
                i= dBConn.ExecuteNonQuery(copy);
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
        public int update_Point(int xghValue , int mzid, int lx,DateTime time)
        {
            string sql = "UPDATE Adims_Point set value='" + xghValue + "' where mzjldid='" + mzid + "' and lx='" + lx + "'and time='" + time + "'";
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
        public DataTable GetSZSJbytype(int mzjldid,int type)
        {
            string select = "";
            if (type==0)
                select = "select * from Adims_Szsj where mzjldid='" + mzjldid + "'";
            if (type == 1)
                select = "select * from Adims_pacu_shijian where mzjldid='" + mzjldid + "'";
            
            return dBConn.GetDataTable(string.Format(select));
        }
        public DataTable GetSZSJ(int mzjldid)
        {
             string   select = "select * from Adims_Szsj where mzjldid='" + mzjldid + "'";
            
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
        /// 修改机控呼吸监测点
        /// </summary>
        /// <param name="mzid"></param>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Update_MZJLD_jkpoint(int mzid, DateTime dt1, DateTime dt2, string value)
        {
            string MZJLD_UPDATE1 = "UPDATE Adims_mzjld_Point SET RRC='" + value
                + "' where mzjldid='" + mzid + "' and RecordTime>'" + dt1 + "' and RecordTime<'" + dt2 + "'";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
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
        public int UpdateMzjld1(List<string> mzdList)
        {
            string MZJLD_UPDATE1 = "UPDATE Adims_Mzjld SET [Height] =  '{0}',[Weight] = '{1}',[tiwen] = '{2}',[xueya] = '{3}',[huxi] = '{4}',[maibo] = '{5}',[xuexing] = '{6}',[ASA] = '{7}' "
      +",[isJizhen] = '{8}',[sqzd] =  '{9}',[nssss] = '{10}',[tw] = '{11}',[SQJinshi] = '{12}',[szzd] = '{13}',[ShoushuFS] ='{14}',[MazuiFS] = '{15}' "
     +" ,[ssys] = '{16}',[mzys] ='{17}',[qxhs] = '{18}',[PCA] = '{19}',[mzxg] = '{20}',[brqx] = '{21}',[pingfen] = '{22}',[zitixue] = '{23}' "
     +" ,[chengxue] = '{24}',[jiaotiye] = '{25}',[jintiye] = '{26}',[zongsrl]= '{27}',[ChuXue] = '{28}',[Niaoliang] ='{29}',ifxssj='{30}'WHERE id = '{31}'";
            string SQL = string.Format(MZJLD_UPDATE1, mzdList.ToArray());
            return dBConn.ExecuteNonQuery(SQL);
        }
        /// <summary>
        /// 更新麻醉记录单出室情况
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int UpdateMzjld(string sqlset)
        {
            string MZJLD_UPDATE1 = "UPDATE Adims_Mzjld SET " + sqlset;
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }
        public int Update_MZJLD_CGBGSJ(int mzid)
        {
            DateTime dt = new DateTime(1900,1,1);
            string MZJLD_UPDATE1 = "UPDATE Adims_Mzjld SET cgsj='" + dt + "' , bgsj ='" + dt + "' where id='" + mzid + "'";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }
        public int Update_MZJLD_jikongTime(int mzid,DateTime dt1,DateTime dt2)
        {
            string MZJLD_UPDATE1 = "UPDATE Adims_Mzjld SET jkkssj='" + dt1 + "' , jkjssj ='" + dt2 + "' where id='" + mzid + "'";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }
        public int Update_MZJLD_jikongTime1(int mzid, DateTime dt1, DateTime dt2, string value, string fz)
        {
            string MZJLD_UPDATE1 = "UPDATE Adims_Mzjld SET jkkssj='" + dt1 + "' , jkjssj ='" + dt2 + "',jkvalue='" + value + "',fzvalue='" + fz + "' where id='" + mzid + "'";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }
        /// <summary>
        /// 更新麻醉开始时间
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int UpdateMzkssj( DateTime  dt1, int mzjldid)
        {
            string updateMzkssj = "update Adims_Mzjld SET mzkssj = '" + dt1 + "' where id = '" + mzjldid + "'";
            return dBConn.ExecuteNonQuery(string.Format(updateMzkssj));
        }
        /// <summary>
        /// 更新麻醉开始时间椎管内
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int UpdateMzkssjzgn(DateTime dt1, int mzjldid)
        {
            string updateMzkssj = "update Adims_Mzjld SET mzkszgn = '" + dt1 + "' where id = '" + mzjldid + "'";
            return dBConn.ExecuteNonQuery(string.Format(updateMzkssj));
        }

        public int UpdateMzkssjsjzz(DateTime dt1, int mzjldid)
        {
            string updateMzkssj = "update Adims_Mzjld SET mzkssjzz = '" + dt1 + "' where id = '" + mzjldid + "'";
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
        public int UpdatePaibanInfo(int ostate, string patid)
        {
            string updateSsjssj = "UPDATE Adims_OTypesetting SET ostate = '" + ostate + "' where   patzhuyuanid = '" + patid + "'";
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
        public int insertJianCeDataLB(int mzjldid, int hr, int spo2, int pulse, int nibps, int nibpd, int nibpm, int arts, int artd, int artm, double etco2, int ico2, int rrc,double temp)
        {
            string insert = "insert into Adims_MonitorRecord(mzjldid,HR,SpO2,Pulse,NIBPS,NIBPD,NIBPM,ARTS,ARTD,ARTM,ETCO2,ICO2,RRC,CreateTime) values(" + mzjldid + "," + hr + "," + spo2 + "," + pulse + "," + nibps + "," + nibpd + "," + nibpm + "," + arts + "," + artd + "," + artm + "," + etco2 + "," + ico2 + "," + rrc + "," + temp + ",'" + DateTime.Now + "')";

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
        public DataTable selectJianCeData(DateTime dt,int mzjldid,int TYPE)
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
        public DataTable GetSSJname()
        {
            return dBConn.GetDataTable(string.Format("select Name from ssjstate "));

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
        /// 
        public DataTable GetMZname()
        {
            return dBConn.GetDataTable(string.Format("select name from mazuifangfa "));

        }
        public DataTable GetKeshi()
        {
            return dBConn.GetDataTable(string.Format("select name from Keshi "));
        }
        public DataTable GetTiwei()
        {
            return dBConn.GetDataTable(string.Format("select name from Tiwei "));
        }
        public DataTable GetASAname()
        {
            return dBConn.GetDataTable(string.Format("select name from ASA "));

        }
        public DataTable GetOROOM()
        {
            return dBConn.GetDataTable(string.Format("select distinct NAME from SSJSTATE "));

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
        public DataTable GetByYSName(string name,string date1,string date2)
        {
            string select1 = "SELECT  * from V_MZZB_MZTJ"
            + " where otime between '" + date1 + "' and '" + date2 + "'and (mazuifs like '%" + name + "%'or asa like '%" + name + "%'or mzys like '%" + name + "%'or patdpm like '%" + name + "%'or Oroom like '%" + name + "%'or qxhs like '%" + name + "%'or xhhs like '%" + name + "%') order by otime desc";
            return dBConn.GetDataTable(string.Format(select1));

        }
        public DataTable GetByYSName(string name,string LName, string date1, string date2)
        {
            string select1 = "SELECT  * from V_MZZB_MZTJ "
            + " where otime between '" + date1 + "'and'" + date2 + "'and (" + LName + " like '" + name + "') order by otime desc";
            return dBConn.GetDataTable(string.Format(select1));

        }
        /// <summary>
        /// 3小时内
        /// </summary>
        /// <param name="shijian"></param>
        /// <returns></returns>
        public DataTable GetByShiJian(string date1,string date2)
        {
            string select1 = "select * from V_MZZB_MZTJ where otime between '2014-09-01'and'2014-09-25' and shijian<=3 order by otime desc";
            return dBConn.GetDataTable(string.Format(select1));

        }
        /// <summary>
        /// 3小时外
        /// </summary>
        /// <param name="shijian"></param>
        /// <returns></returns>
        public DataTable GetByShiJian2(string date1, string date2)
        {
            string select1 = "select * from V_MZZB_MZTJ where otime between '2014-09-01'and'2014-09-25' and shijian>3 order by otime desc";
            return dBConn.GetDataTable(string.Format(select1));

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
        public DataTable GetQJMX(DateTime dtime1, DateTime dtime2,string  flag)
        {
            string QJMXselect = "select EmployeesNO, EmployeesName ,StartTime,EndTime,SumDay,LeaveReason  from Adims_LeaveRegistration where CreateDate >='" + dtime1 + "'and CreateDate <='" + dtime2 + "' and EmployeesNO='" + flag + "' ";
            return dBConn.GetDataTable(string.Format(QJMXselect));

        }
        /// <summary>
        /// 获取加班总数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetJBZS(DateTime dtime1,DateTime dtime2)
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
            string sql = "select * from Adims_OTypesetting where patid='" + patID + "'";
            int result = Convert.ToInt32(dBConn.ExecuteScalar(sql));
            if (result == 1) return true; else return false;
        }

        private static readonly string SQL_PAIBAN_INSERT = "INSERT INTO Adims_OTypesetting(PatID,PatZhuYuanID,Patname ,Patage,Patsex "
                                + ",Patdpm  ,Patbedno ,PatWeight ,PatHeight,Oname,Pattmd,Oroom,Second,Amethod,AP1"
                                + ",ON1 ,SN1,Odate,OS,isjizhen,StartTime,BZ,GR,Ostate) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',"
                                + "'{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')";

        private static readonly string SQL_PAIBAN_UPDATE = " UPDATE Adims_OTypesetting WITH (ROWLOCK) SET "
         + "PatZhuYuanID = '{1}',Patname = '{2}',Patage = '{3}',Patsex = '{4}',Patdpm = '{5}',"
         + "Patbedno = '{6}',PatWeight = '{7}',PatHeight = '{8}',Oname = '{9}',Pattmd = '{10}',"
         + "Oroom = '{11}',Second = '{12}',Amethod = '{13}',AP1 = '{14}',ON1 = '{15}',SN1 = '{16}'"
         + ",Odate = '{17}',OS = '{18}',Ostate = '1' where PatID = '{0}'";

        /// <summary>
        /// 添加排班
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertPaiban(List< string> list1)
        {
            //string SQL_PAIBAN = "INSERT INTO Adims_OTypesetting(PatID,PatZhuYuanID,CardID,Patname ,Patage,Patsex,patNation "
            //                    + ",Patbedno,Patdpm,Pattmd ,Oname ,odate,OS,os1,os2,Amethod,BX,Tiwei,GR,remarks,StartTime,isjizhen,ostate) VALUES"
            //                    + "('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',"
            //                    + "'{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')";
            //string SQL_PAIBAN = "INSERT INTO Adims_OTypesetting(PatID,CardID,PatZhuYuanID,OROOM,second,Patname,Patsex,Patage,PatNation,Patdpm,Patbedno,Pattmd,Oname,Amethod,StartTime,"
            //                    + "OS,OS1,OS2,OS3,OS4,BX,AP1,AP2,AP3,ON1,ON2,SN1,SN2,Olevel,Odate,isjizhen,PatBloodType,osdm,qxbs,expertName,zycs,zrys,ostate) VALUES"
            //                    + "('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}',"
            //                    + " '{31}','{32}','{33}','{34}','{35}','{36}','{37}')";
            string SQL_PAIBAN = "INSERT INTO Adims_OTypesetting( PatID,PatZhuYuanID,CardID,Patname,Patsex,Patage,PatNation,Patbedno,Pattmd,Oname,Amethod,OS,OS1,OS2,OS3,OS4,StartTime,Odate,tiwen,SSLB )VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}')";
            string INSERT = string.Format(SQL_PAIBAN, list1.ToArray());
            return dBConn.ExecuteNonQuery(INSERT);
        }

        public int InsertPAIBAN(Dictionary<string, string> dictionary)
        {
            string SQL_INSERT = "INSERT INTO Adims_OTypesetting(PatID,PatZhuYuanID,Patname ,Patage,Patsex "
                                + ",Patdpm  ,Patbedno ,PatWeight ,PatHeight,Oname,Pattmd,Oroom,Second,Amethod,AP1"
                                + ",ON1 ,SN1,Odate,OS,isjizhen,StartTime,remarks,GR,tiwei,PatNation,Ostate,AP2,AP3,OS1,OS2,OS3,OS4,ON2,SN2) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',"
                                + "'{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','1','','','','','','','','')";
            string INSERT = string.Format(SQL_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(INSERT);
        }
        /// <summary>
        /// 查询排班
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetPAIBAN(string dtime)
        {
            string sql = "SELECT patid ,oroom ,second ,patdpm ,patname ,patsex ,patage ,patbedno ,os ,oname ,pattmd ,amethod ,"
               + "ap1 ,on1 ,on2 ,sn1 ,sn2 "
               + " from Adims_OTypesetting where CONVERT(varchar, Odate , 23 ) ='" + dtime + "' and isjizhen='1'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 查询排班
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetALLPAIBANyz(string patid)
        {
            string sql = "SELECT * from Adims_OTypesetting  where patzhuyuanid='" + patid + "'";               
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetALLPAIBAN(string patid)
        {
            string sql = "SELECT * from Adims_OTypesetting  where patid='" + patid + "'";               
            return dBConn.GetDataTable(sql);
        }

        
        public DataTable GetALLPAIBANs(string patid, string Odate)
        {
            string sql = "SELECT * from Adims_OTypesetting  where patzhuyuanid='" + patid + "' and  Convert(varchar,Odate,23)='" + Odate + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetPaiban(string patid,string odate)
        {
            string sql = "SELECT * from Adims_OTypesetting  where patid='" + patid + "' and odate='" + odate + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetpaibanByOroomandTaici(string oroom,string second,DateTime dt,string patid)
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
        public int UpdatePaiban(List<string> list1)
        {
            string SQL_1 = "UPDATE Adims_OTypesetting WITH (ROWLOCK) SET PatZhuYuanID='{1}',Patname='{2}' ,Patage='{3}',Patsex='{4}',"
                          + ",Patbedno='{5}',Patdpm='{6}',PatWeight='{7}',PatHeight='{8}',Oname='{9}',Pattmd='{10}',Oname='{11}',Second='{12}',Amethod='{13}',ap1='{14}',"
                          + "on1='{15}',sn1='{16}',odate='{17}',os='{18}',isjizhen='{19}',StartTime='{20}',remarks='{21}',GR='{22}',tiwei='{23}',patnation='{24}' where patid='{0}'";

            string sql = string.Format(SQL_1, list1.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        #endregion
        #region 本地和服务器数据检查，是否有遗漏
        public DataTable GetMzjldPointInServer(DateTime dt, int mzjldid)//检查服务器时间点
        {
            string sql = "select RecordTime from Adims_mzjld_Point where RecordTime='" + dt + "' and  mzjldid='" + mzjldid + "'";
            return dBConn.GetDataTable(sql);
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
        #endregion
        public DataTable gettsqkdj(string zyh)
        {
            string sql = "select * from Tsmztj where zhuyuanhao='" + zyh + "'";
            return dBConn.GetDataTable(sql);
        }
        public int UpdatemaTsmztj(Dictionary<string, string> dictionary)
        {
            string upMZZJ = "UPDATE Tsmztj WITH (ROWLOCK) SET [xingmingname] ='{1}',[shiian] = '{2}',[fswyqysza] = '{3}',[cxybhdjd] = '{4}',[qsmzycxy] = '{5}'"+
     " ,[ywxwyyfhxdz] = '{6}',[mzywsw] = '{7}',[qtfyqsj] = '{8}',[mzkssswks] = '{9}',[rpacu3xs] ='{10}',[pacudtw] = '{11}'"+
      ",[fjheccg] = '{12}',[mz24xssw] = '{13}',[mz24xsxzzt] = '{14}',[mzqgmfy] = '{15}',[zgnmzhyzsjbfz] = '{16}'"+
      ",[szzxjmccyzbfz] = '{17}',[mzxfhm] = '{18}',[fjhzricu] = '{19}',[qmcghsysy] = '{20}'"+
      ",[shhuzztx] = '{21}',[szytx] = '{22}',[wu] = '{23}',[fangshiren]='{24}',[babengren]='{25}',[babengshijian]='{26}',[tesuqingk]='{27}' where zhuyuanhao='{0}'";
            return dBConn.ExecuteNonQuery(string.Format(upMZZJ, dictionary.Values.ToArray()));
        }
        public int InsertTsmztj(Dictionary<string, string> dictionary)
        {
            string _INSERT = "INSERT INTO Tsmztj(zhuyuanhao ,xingmingname ,shiian ,fswyqysza  ,cxybhdjd,"
                + "qsmzycxy ,ywxwyyfhxdz ,mzywsw ,qtfyqsj ,mzkssswks  ,rpacu3xs ,pacudtw ,fjheccg ,"
               + "mz24xssw ,mz24xsxzzt  ,mzqgmfy  ,zgnmzhyzsjbfz  ,szzxjmccyzbfz  ,mzxfhm,fjhzricu,qmcghsysy,shhuzztx,szytx,wu,fangshiren,babengren,babengshijian,tesuqingk )"
               + " values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}',"
              + "'{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}')";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);

        }
        /// <summary>
        /// 修改监护监测点值
        /// </summary>
        /// <param name="mzid"></param>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Update_MZJLD_jkpointNIBPS(int mzid, DateTime dt1, DateTime dt2, string value)
        {
            string MZJLD_UPDATE1 = "UPDATE Adims_mzjld_Point SET NIBPS='" + value
                + "' where mzjldid='" + mzid + "' and RecordTime>'" + dt1 + "' and RecordTime<'" + dt2 + "'";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }
        public int Update_MZJLD_jkpointNIBPD(int mzid, DateTime dt1, DateTime dt2, string value)
        {
            string MZJLD_UPDATE1 = "UPDATE Adims_mzjld_Point SET NIBPD='" + value
                + "' where mzjldid='" + mzid + "' and RecordTime>'" + dt1 + "' and RecordTime<'" + dt2 + "'";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }
        public int Update_MZJLD_jkpointRRC(int mzid, DateTime dt1, DateTime dt2, string value)
        {
            string MZJLD_UPDATE1 = "UPDATE Adims_mzjld_Point SET RRC='" + value
                + "' where mzjldid='" + mzid + "' and RecordTime>'" + dt1 + "' and RecordTime<'" + dt2 + "'";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }
        public int Update_MZJLD_jkpointPULSE(int mzid, DateTime dt1, DateTime dt2, string value)
        {
            string MZJLD_UPDATE1 = "UPDATE Adims_mzjld_Point SET Pulse='" + value
                + "' where mzjldid='" + mzid + "' and RecordTime>'" + dt1 + "' and RecordTime<'" + dt2 + "'";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }
        public int Update_MZJLD_jkpointHR(int mzid, DateTime dt1, DateTime dt2, string value)
        {
            string MZJLD_UPDATE1 = "UPDATE Adims_mzjld_Point SET HR='" + value
                + "' where mzjldid='" + mzid + "' and RecordTime>'" + dt1 + "' and RecordTime<'" + dt2 + "'";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }
        public int Update_MZJLD_jkpointTEMP(int mzid, DateTime dt1, DateTime dt2, string value)
        {
            string MZJLD_UPDATE1 = "UPDATE Adims_mzjld_Point SET Temp='" + value
                + "' where mzjldid='" + mzid + "' and RecordTime>'" + dt1 + "' and RecordTime<'" + dt2 + "'";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }
        public int Update_MZJLD_jkpointSPO2(int mzid, DateTime dt1, DateTime dt2, string value)
        {
            string MZJLD_UPDATE1 = "UPDATE Adims_mzjld_Point SET SpO2='" + value
                + "' where mzjldid='" + mzid + "' and RecordTime>'" + dt1 + "' and RecordTime<'" + dt2 + "'";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }
        public int Update_MZJLD_jkpointETCO2(int mzid, DateTime dt1, DateTime dt2, string value)
        {
            string MZJLD_UPDATE1 = "UPDATE Adims_mzjld_Point SET ETCO2='" + value
                + "' where mzjldid='" + mzid + "' and RecordTime>'" + dt1 + "' and RecordTime<'" + dt2 + "'";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }
        public int Update_MZJLD_jkpointCVP(int mzid, DateTime dt1, DateTime dt2, string value)
        {
            string MZJLD_UPDATE1 = "UPDATE Adims_mzjld_Point SET CVP='" + value
                + "' where mzjldid='" + mzid + "' and RecordTime>'" + dt1 + "' and RecordTime<'" + dt2 + "'";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }
        public int Update_MZJLD_jkpointBIS(int mzid, DateTime dt1, DateTime dt2, string value)
        {
            string MZJLD_UPDATE1 = "UPDATE Adims_mzjld_Point SET BIS='" + value
                + "' where mzjldid='" + mzid + "' and RecordTime>'" + dt1 + "' and RecordTime<'" + dt2 + "'";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }
        /// <summary>
        /// 添加门诊麻醉病人
        /// </summary>
        /// <param name="mzid"></param>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable Getpaibanmenzhenbr(DateTime dt, string patid)
        {
            string sql = "select * from admin_mazuimenzhen  where  dengjishijian ='" + dt + "'and menzhenhaocishu='" + patid + "'";
            return dBConn.GetDataTable(sql);
        }
        public int InsertPAIBANmenzhen(Dictionary<string, string> dictionary)
        {
            string SQL_INSERT = "INSERT INTO [admin_mazuimenzhen]([menzhenhao],[menzhenhaocishu],[xingming]"
           + ",[age],[tizhong],[sex],[zhenduan],[mazuifangshi],[keshi],[mingzu]"
           + ",[dengjishijian],[visid],[chuanghao])VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',"
                                + "'{10}','{11}','{12}')";
            string INSERT = string.Format(SQL_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(INSERT);
        }
        public DataTable GetALLPAIBANmenzhen(string patid)
        {
            string sql = "SELECT * from admin_mazuimenzhen  where menzhenhaocishu='" + patid + "'";
            return dBConn.GetDataTable(sql);
        }
        public int UpdatePaibanmenzhen(Dictionary<string, string> dictionary)
        {
            string SQL_1 = "UPDATE [admin_mazuimenzhen] " +
  " SET [menzhenhao] = '{0}',[xingming] = '{2}',[age] = '{3}' " +
     " ,[tizhong] = '{4}',[sex] = '{5}',[zhenduan] = '{6}',[mazuifangshi] = '{7}',[keshi] = '{8}' " +
     " ,[mingzu] = '{9}',[dengjishijian] = '{10}',[visid] = '{11}' WHERE [menzhenhaocishu] = '{1}'";

            string sql = string.Format(SQL_1);
            return dBConn.ExecuteNonQuery(sql);
        }

        public DataTable Getpaibanmenzhenmax(string patid)
        {
            string sql = "select MAX(visid) as visid from admin_mazuimenzhen  where menzhenhao='" + patid + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 查询门诊登记信息
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetPAIBANmenzhen(string dtime)
        {
            string sql = "SELECT [menzhenhao],[menzhenhaocishu],[xingming] "+
      ",[age],[tizhong],[sex],[zhenduan],[mazuifangshi],[keshi],[mingzu],[dengjishijian],[visid],[chuanghao] "+
      " FROM [admin_mazuimenzhen] where CONVERT(varchar, dengjishijian , 23 ) ='" + dtime + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetPAIBANmenzhenhao(string dtime)
        {
            string sql = "SELECT [menzhenhao],[menzhenhaocishu],[xingming] " +
      ",[age],[tizhong],[sex],[zhenduan],[mazuifangshi],[keshi],[mingzu],[dengjishijian],[visid],[chuanghao] " +
      " FROM [admin_mazuimenzhen] where menzhenhao='" + dtime + "'  order by dengjishijian desc";
            return dBConn.GetDataTable(sql);
        }




        /// <summary>
        /// 更新麻醉开始时间门诊
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int UpdateMzkssjmenzhen(DateTime dt1, int mzjldid)
        {
            string updateMzkssj = "update admin_menzhenjld SET mzkstime = '" + dt1 + "' where menzhenid = '" + mzjldid + "'";
            return dBConn.ExecuteNonQuery(string.Format(updateMzkssj));
        }

        public int Updateyangqijismenzhen(DateTime dt1, int mzjldid)
        {
            string updateMzkssj = "update admin_menzhenjld SET yangqijiesushijian = '" + dt1 + "' where menzhenid = '" + mzjldid + "'";
            return dBConn.ExecuteNonQuery(string.Format(updateMzkssj));
        }
        /// <summary>
        /// 更新麻醉结束时间门诊
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int UpdateMzjssjmenzhen(DateTime dt1, int mzjldid)
        {
            string updateMzjssj = "update admin_menzhenjld SET mzjstime = '" + dt1 + "' where menzhenid = '" + mzjldid + "'";
            return dBConn.ExecuteNonQuery(string.Format(updateMzjssj));
        }
        /// <summary>
        /// 更新手术开始时间门诊
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int UpdateSskssjmenzhen(DateTime dt1, int mzjldid)
        {
            string updateSskssj = "update admin_menzhenjld SET sskstime = '" + dt1 + "' where menzhenid = '" + mzjldid + "'";
            return dBConn.ExecuteNonQuery(string.Format(updateSskssj));
        }
        /// <summary>
        /// 更新手术结束时间门诊
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int UpdateSsjssjmenzhen(DateTime dt1, int mzjldid)
        {
            string updateSsjssj = "update admin_menzhenjld SET ssjstime = '" + dt1 + "' where menzhenid = '" + mzjldid + "'";
            return dBConn.ExecuteNonQuery(string.Format(updateSsjssj));
        }
        /// <summary>
        /// 更新手术结束标志
        /// </summary>门诊
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int UpdateSsjsFlagmenzhen(int mzjldid)
        {
            string updateSsjssj = "update admin_menzhenjld SET flags = '1' where menzhenid = '" + mzjldid + "'";
            return dBConn.ExecuteNonQuery(string.Format(updateSsjssj));
        }
    
        /// <summary>
        /// 更新插管时间门诊
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int UpdateMzCGmenzhen(DateTime dt1, int mzjldid)
        {
            string updateMzCG = "update admin_menzhenjld SET cgkstime = '" + dt1 + "' where menzhenid = '" + mzjldid + "'";
            return dBConn.ExecuteNonQuery(string.Format(updateMzCG));
        }
        /// <summary>
        /// 更新拔管时间门诊
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int UpdateMzBGmenzhen(DateTime dt1, int mzjldid)
        {
            string updateMzBG = "update admin_menzhenjld SET bgjstime = '" + dt1 + "' where menzhenid = '" + mzjldid + "'";
            return dBConn.ExecuteNonQuery(string.Format(updateMzBG));
        }

        public int UpdateMzjldmenzhen(string sqlset)
        {
            string MZJLD_UPDATE1 = "UPDATE admin_menzhenjld SET " + sqlset;
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }

        public int UpdateMzjld1menzhen(List<string> mzdList)//更新门诊麻醉单
        {
            string MZJLD_UPDATE1 = "UPDATE [admin_menzhenjld] "
  +" SET [yisidadsqsp] = '{0}',[spo2zd] = '{1}',[exin] = '{2}',[vaspingfen] = '{3}',[steward] ='{4}',[fsys] = '{5}' "
   +"   ,[fshs] = '{6}',[tesqingkang] = '{7}',[shuzhongjl] = '{8}' ,[lhuana] = '{9}' "
   +"   ,[bbf] = '{10}',[qitayao1] = '{11}',[qitayao2] = '{12}',[qitayao3] = '{13}',[qitayao4] = '{14}' "
   + "   ,[qitayao5] = '{15}',[qitayao6] = '{16}',[isxs]='{17}' WHERE  menzhenid = '{18}'";
            string SQL = string.Format(MZJLD_UPDATE1, mzdList.ToArray());
            return dBConn.ExecuteNonQuery(SQL);
        }

        public DataTable GetMzjldListmazui(string date1, string date2)
        {
            string select1 = @"    select distinct dense_rank() over( order by Adims_Mzjld.otime asc ) as rowid,Adims_Mzjld.Otime,Adims_OTypesetting.Patdpm,Adims_OTypesetting.patbedno
,Adims_OTypesetting.Patname,Adims_Mzjld.mzys,Adims_Mzjld.ShoushuFS,Adims_Mzjld.MazuiFS,Adims_Mzjld.ASA , case  CHARINDEX ( '1' , tljmzxj.shzt ) when 0 then '无' else '有' end as zhentb,Tsmztj.fangshiren,Tsmztj.tesuqingk,Tsmztj.babengren,Tsmztj.babengshijian
 from Adims_Mzjld left join Adims_OTypesetting  
on Adims_Mzjld.patid=Adims_OTypesetting.PatZhuYuanID
left join tljmzxj on Adims_Mzjld.patid=tljmzxj.zhuyuanhao   
left join Tsmztj on Adims_Mzjld.patid=Tsmztj.zhuyuanhao 
               where Adims_Mzjld.otime between '" + date1 + "'and'" + date2 + "' order by otime asc";
            return dBConn.GetDataTable(string.Format(select1));
        }

        public DataTable GetMzjldListmazui111(string date1, string date2)
        {
            string select1 = @"        select distinct dense_rank() over( order by Adims_Mzjld.otime asc ) as 序号,Adims_Mzjld.Otime as 日期,
 tljmzxj.rstime as 入室时间,
 case CHARINDEX('6',tljmzxj.qsmz) when 0 then '否' else '是' end as 插管,
 case CHARINDEX('c',tljmzxj.tnsmcg) when 0 then '否' else '是' end as 喉罩,
CONVERT(varchar(100), Adims_Mzjld.BGSJ, 108) as 拔管时间,
Adims_OTypesetting.Patdpm +Adims_OTypesetting.patbedno as 科室床号,
   tljmzxj.SPO2 as SPO2,
  tljmzxj.BP as 血压, tljmzxj.PR as 心率,
 case  CHARINDEX ( '1' , tljmzxj.shzt ) when 0 then '无' else '有' end as 镇痛泵,
  Adims_Mzjld.ASA as ASA分级,
 Adims_Mzjld.tsbq as 术中特殊情况,
Adims_OTypesetting.Patname as 姓名,Adims_Mzjld.mzys as 交班医生,
tljmzxj.fsys as 接班医生,tljmzxj.fshs as 接班护士,tljmzxj.cstime as 出室时间,tljmzxj.BP as 血压,tljmzxj.SPO2 as SPO2, tljmzxj.PR as 心率, tljmzxj.steward as Stward,
tljmzxj.fsqbfzjclbz as 特殊情况,tljmzxj.fshs as 结单护士,'' as 转送人员
from Adims_Mzjld left join Adims_OTypesetting  
on Adims_Mzjld.patid=Adims_OTypesetting.PatZhuYuanID
left join tljmzxj on Adims_Mzjld.patid=tljmzxj.zhuyuanhao   
left join Tsmztj on Adims_Mzjld.patid=Tsmztj.zhuyuanhao 
where tljmzxj.mzxgpj like '%h%' and
 Adims_Mzjld.otime between '" + date1 + "'and'" + date2 + "' order by otime asc";
            return dBConn.GetDataTable(string.Format(select1));
        }
        public DataTable GetMzjldList手术室(string date1)
        {
            string select1 = @"select Patdpm as 科室,Patbedno as 床号,PatID as 住院号,Patname as 患者姓名,Patsex as 性别,
Patage as 年龄,Oname as 手术名称,'' as 过敏史,'' as 感染史,'' as 患者或者家属签字,
'' as 受托人,'' as 宣教人,'' as 皮肤是否完整,'' as 皮肤有无损伤,'' as 患者家属签名,
'' as 受托人签名,'' as 回访人,'' as 备注 from Adims_OTypesetting where Convert(varchar,Odate,23)='" + date1 + "'";
            return dBConn.GetDataTable(string.Format(select1));
        }

        public DataTable GetMzjldListshoushushigzl(string date1, string date2)
        {
            string select1 = @"select Adims_Mzjld.Otime as 手术时间,Adims_OTypesetting.Patname as 姓名,Adims_OTypesetting.Patage as 年龄,
Adims_OTypesetting.Patsex as 性别
,Adims_OTypesetting.Patdpm as 科室,Adims_OTypesetting.patid as 住院号,Adims_OTypesetting.isjizhen as 急诊择期,
Adims_OTypesetting.patbedno as 床号
,Adims_OTypesetting.Pattmd as 术前诊断,Adims_Mzjld.ShoushuFS as 手术名称 ,Adims_Mzjld.MazuiFS as 麻醉方式,chafenxianpinggu.ssdj 手术等级,
 CASE WHEN PATINDEX('%1%', baochunckes) =1 THEN '1'
WHEN PATINDEX('%2%', baochunckes) =1 THEN '2'
WHEN PATINDEX('%3%', baochunckes) =1 THEN '3'
WHEN PATINDEX('%4%', baochunckes) =1 THEN '4'
ELSE'数据错误' END as 手术切口清洁程度,chafenxianpinggu.defen as NNIS分级,chafenxianpinggu.mazuiasa as 麻醉ASA,Adims_OTypesetting.OS as 手术者,Adims_OTypesetting.OS1 as 一助
,Adims_OTypesetting.OS2 as 二助,Adims_OTypesetting.OS3 as 三助
,Adims_Mzjld.qxhs as 器械护士,Adims_Mzjld.xhhs as 巡回护士,DATEDIFF( Minute, Adims_Mzjld.Otime, Adims_Mzjld.ssjssj) as 在室时间
,Adims_Mzjld.sskssj as 手术开始,Adims_Mzjld.ssjssj as 手术结束,DATEDIFF( Minute, Adims_Mzjld.sskssj, Adims_Mzjld.ssjssj) as 手术时间
 from Adims_Mzjld left join Adims_OTypesetting  
 on Adims_Mzjld.patid=Adims_OTypesetting.PatZhuYuanID
 left join chafenxianpinggu on Adims_Mzjld.patid=chafenxianpinggu.zhuyuanhao  
  where Adims_Mzjld.otime between '" + date1 + "'and'" + date2 + "' order by otime asc";
            return dBConn.GetDataTable(string.Format(select1));
        }

        public DataTable GetMzjldListshoushushissjl(string date1, string date2)
        {
            string select1 = @"select Adims_Mzjld.Otime as 手术时间,Adims_OTypesetting.Patname as 姓名,Adims_OTypesetting.Patage as 年龄,
Adims_OTypesetting.Patsex as 性别
,Adims_OTypesetting.Patdpm as 科室,Adims_OTypesetting.patid as 住院号,Adims_OTypesetting.isjizhen as 急诊择期,
Adims_OTypesetting.patbedno as 床号
,Adims_OTypesetting.Pattmd as 术前诊断,Adims_Mzjld.ShoushuFS as 手术名称 ,Adims_Mzjld.MazuiFS as 麻醉方式,chafenxianpinggu.ssdj 手术等级,
 CASE WHEN PATINDEX('%1%', baochunckes) =1 THEN '1'
WHEN PATINDEX('%2%', baochunckes) =1 THEN '2'
WHEN PATINDEX('%3%', baochunckes) =1 THEN '3'
WHEN PATINDEX('%4%', baochunckes) =1 THEN '4'
ELSE'数据错误' END as 手术切口清洁程度,Adims_OTypesetting.OS as 手术者,Adims_OTypesetting.OS1 as 一助
,Adims_Mzjld.mzys as 麻醉医生,tljmzxj.rbc as 红悬 ,tljmzxj.xuejiang as 血浆 ,Adims_Mzjld.xhhs as 巡回护士
 from Adims_Mzjld left join Adims_OTypesetting  
 on Adims_Mzjld.patid=Adims_OTypesetting.PatZhuYuanID
 left join chafenxianpinggu on Adims_Mzjld.patid=chafenxianpinggu.zhuyuanhao
 left join   tljmzxj on Adims_Mzjld.patid=tljmzxj.zhuyuanhao  where IsNull(RTRIM(tljmzxj.rbc), '') != ' ' and tljmzxj.rbc!='0'
 and  Adims_Mzjld.otime between '" + date1 + "'and'" + date2 + "' order by otime asc";
            return dBConn.GetDataTable(string.Format(select1));
        }
        public DataTable GetMzjldListsankeshitongjisudufenji(string date1, string date2)
        {
            string select1 = @"select Patdpm as 科室,COUNT(a) as Ⅰ类,COUNT(b) as Ⅱ类,COUNT(c) AS Ⅲ类,COUNT(d) AS Ⅳ类,COUNT(e) as Ⅴ类
 
 from 
( select Patdpm,case when chafenxianpinggu .ssdj='Ⅰ' then '1'  end a,
case when chafenxianpinggu .ssdj='Ⅱ' then '2'  end b,
case when chafenxianpinggu .ssdj='Ⅲ' then '3' end c,
case when chafenxianpinggu .ssdj='Ⅳ' then '4' end d,
case when chafenxianpinggu .ssdj='Ⅴ' then '5'  end e
 from chafenxianpinggu  left join Adims_OTypesetting  on chafenxianpinggu .zhuyuanhao=Adims_OTypesetting.PatZhuYuanID
 where  chafenxianpinggu .shoushuriqi between '" + date1 + "'and'" + date2 + "' ) a group by Patdpm";
            return dBConn.GetDataTable(string.Format(select1));
        }

        public DataTable GetMzjldcxdtbr(string date1, string date2)
        {
            string select1 = @"select  Oroom as 手术间,时间 as 时间,o.patid as 住院号,Patbedno as 科室, Patname+'/'+Patsex+'/'+Patage+'岁' AS 患者信息,Patbedno as 床号,
PatNation as 民族,Pattmd as 术前诊断,ShoushuFS as 手术名称,MazuiFS as 麻醉方法,ssys as 手术者,mzys as 麻醉者,m.isJizhen as 急诊 

 from Adims_Mzjld m left join Adims_OTypesetting o on m.patid=o.PatZhuYuanID
left join (select mzjldid,DATEDIFF( Minute, min(RecordTime), max(RecordTime)) AS 时间  from Adims_mzjld_Point GROUP BY mzjldid)p   on m.id=p.mzjldid

where m.Otime  between '" + date1 + "'and'" + date2 + "' order by otime asc";
            return dBConn.GetDataTable(string.Format(select1));
        }

        public DataTable GetMzjldbinfren(string date1, string date2)
        {
            string select1 = @"SELECT Row_number() over(order by riqi desc ) as ID, menzhenhao as 门诊及住院号,xingming as 姓名,dengjishijian 时间 FROM MZmzsqfs  left join admin_mazuimenzhen  on MZmzsqfs.zhuyuanhao=admin_mazuimenzhen.menzhenhaocishu
where MZmzsqfs.nixzlff not in ('无痛宫腔镜','无痛人流术') and riqi BETWEEN '" + date1 + "' AND '" + date2 + "' ";
            return dBConn.GetDataTable(string.Format(select1));
        }
        #region<<浙江金华手术室护理记录单>>
        #region<<麻醉术前访视单>>
        public int Updatemkcpacuxinxi修改4(string zhuyuanhao, string a, string biandanleixing)//修改
        {

            string inst = "UPDATE tijiaoshenqing SET [leibie]= '" + a + "' where zhuyuanhao='" + zhuyuanhao + "'and biandanleixing='" + biandanleixing + "'";

            return dBConn.ExecuteNonQuery(inst);
        }
        public DataTable Getteshenhe(string zyh, string ccc, string bb)
        {
            string sql = "SELECT * from tijiaoshenqing  where  zhuyuanhao='" + zyh + "'and biandanleixing='" + ccc + "' and shenqingren='" + bb + "'";
            return dBConn.GetDataTable(sql);
        }
        public int inserttijiao(string zhuyuanhao, string shenqingren, string shenqingyuanyin, string biaodan, string leib, DateTime dd)
        {
            string _INSERT = "INSERT INTO tijiaoshenqing ([zhuyuanhao],[shenqingren],[jiesuoyuanyin],[biandanleixing],[leibie],[tijiaoshijian]) VALUES( '" + zhuyuanhao + "', '" + shenqingren + "' , '" + shenqingyuanyin + "', '" + biaodan + "','" + leib + "','" + dd + "')";
            return dBConn.ExecuteNonQuery(_INSERT);
        }
        public int Updatayishengshuqianfs(string ZYNumber, int isread)
        {
            string sql = "UPDATE KCyshengfangshi SET IsRead='" + isread + "' where zhuyuanhao='" + ZYNumber + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int dx(string lieming, string ZYNumber, string isread)
        {
            string sql = "UPDATE KCyshengfangshi SET " + lieming + "='" + isread + "' where zhuyuanhao='" + ZYNumber + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        public DataTable Getteykcwts(string zyh)
        {
            string sql = "SELECT * from Adims_OTypesetting  where  PatZhuYuanID='" + zyh + "'";
            return dBConn.GetDataTable(sql);
        }

        public DataTable Getkcysname(string name)
        {
            string sql = "select user_name from Adims_User where uid='" + name + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable Getkcyshengshuqianfs(string zhuyuanhao)
        {
            string sql = "select * from KCyshengfangshi where zhuyuanhao='" + zhuyuanhao + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable Getkcyshengshuqianfs(string zhuyuanhao, string odate)
        {
            string sql = "select * from KCyshengfangshi where zhuyuanhao  like '%" + zhuyuanhao + "'and CONVERT(varchar, RQ , 23 )='" + odate + "' ";
            return dBConn.GetDataTable(sql);
        }
        public int Deletekcyishengshuqianfs(string zhuyuanhao)
        {
            string sql = "delete KCyshengfangshi where zhuyuanhao='" + zhuyuanhao + "' ";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int KCInsertyshengshuqianfs(Dictionary<string, string> dictionary)
        {

            string insert = @"Insert into KCyshengfangshi (zhuyuanhao,shengao,tizhong,NXSSFS,zhushu,BP,R,P,T,qita,
        xingxueg,xingxueg1,xingxueg2,fhx,fhx1,fhx2,biniao,biniao1,
        biniao2,xiaohua,xiaohua1,xiaohua2,sjjr,sjjr1,sjjr2,xuey,xuey1,
        xuey2,nfb,nfb1,nfb2,js,js1,js2,ck,ck1,ck2,xysj,xysj1,xysj2,gns,
        gns1,gns2,mzs,mzs1,mzs2,ycb,ycb1,ycb2,mqyw,mqyw1,mqyw2,qsqk,qsqk1,
        qsqk2,yszk,yszk1,yszk2,qdtcd,qdtcd1,yc,yc1,yk,yk1,yk2,mzccw,mzccw1,
        mzccw2,xp,xp1,xdt,xdt1,HB,WBC,PLT,K,NA,C1,GLU,SGPT,BUN,CR,PT,APTT,PAO2,
        ASAFJ,ASAFJ1,NYBW,MZJH,WTJY,YSQZ,RQ,IsRead) Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',
'{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}',
'{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}',
'{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}',
'{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}',
'{50}','{51}','{52}','{53}','{54}','{55}','{56}','{57}','{58}','{59}',
'{60}','{61}','{62}','{63}','{64}','{65}','{66}','{67}','{68}','{69}',
'{70}','{71}','{72}','{73}','{74}','{75}','{76}','{77}','{78}','{79}',
'{80}','{81}','{82}','{83}','{84}','{85}','{86}','{87}','{88}','{89}','{90}','{91}','{92}')";
            string sql = string.Format(insert, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        public int updatexiugaijilu(DateTime dangqianshiian, string xiugainame, string namezhuyunahao, string xiugaiyy, string xiugaineirong, string xiugaileibie)
        {
            string sql = "insert into xiugaijilu values('" + dangqianshiian + "','" + xiugainame + "','" + namezhuyunahao + "','" + xiugaiyy + "','" + xiugaineirong + "','" + xiugaileibie + "')";
            return dBConn.ExecuteNonQuery(sql);
        }
        #endregion
        #region<<麻醉后恢复室记录单>>
        public DataTable Getkcpacu(string zhuyuanhao)
        {
            string sql = "select * from kcpacu where zhuyuanhao='" + zhuyuanhao + "'";
            return dBConn.GetDataTable(sql);
        }
        public int Deletempacu(string MZID)
        {
            string sql = "delete kcpacu where zhuyuanhao='" + MZID + "' ";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int Insertkcpacu(Dictionary<string, string> dictionary)
        {
            string insert = "insert into kcpacu (zhuyuanhao,shijian,tiwen,yishi,BP,HR,RR,xiyang,SPO2,vt,f,fio2,xitan,tkdx,tkfg,zj,zhent,pangg,bingqingycljl) Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')";
            string sql = string.Format(insert, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        public DataTable Getkcpacuxinxi(string zhuyuanhao)
        {
            string sql = "select * from kcpacuxinxi where zhuyuanhao='" + zhuyuanhao + "'";
            return dBConn.GetDataTable(sql);
        }
        public int Updatemkcpacuxinxi(Dictionary<string, string> dictionary)//修改
        {

            string _INSERT = @"UPDATE [dbo].[kcpacuxinxi]
   SET [zrl]= '{1}',[jtl]= '{2}',[jiaoti]= '{3}',[xxjiang]= '{4}',[zcl]= '{5}',[nl]= '{6}'
,[yll]= '{7}',[qt]= '{8}',[rs]= '{9}',[cs]= '{10}',[csqx]= '{11}',[fangs]= '{12}'
,[fangshiqt]= '{13}',[zhentengpeifang]= '{14}',[zysx]= '{15}',[zysxqt]= '{16}',[mazuiys]='{17}',[mzfssys]='{18}',[hushi]='{19}',[riqi]='{20}',[IsRead]='{21}'
where [zhuyuanhao]= '{0}'";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);
        }
        public int KCInserskcpacuxinxi(Dictionary<string, string> dictionary)
        {
            string insert = @"Insert into kcpacuxinxi (zhuyuanhao,zrl,jtl,jiaoti,xxjiang,zcl,nl,yll,qt,rs,cs,csqx,fangs,
fangshiqt,zhentengpeifang,zysx,zysxqt,mazuiys,mzfssys,hushi,riqi,IsRead) Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',
'{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}')";
            string sql = string.Format(insert, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        public DataTable GetALMZFS(string patid)
        {
            string sql = "select MazuiFS from Adims_Mzjld where patid='" + patid + "'";
            return dBConn.GetDataTable(sql);
        }

        public int Addkcpacu(string zhuyuanhao)
        {
            string insert = "Insert into kcpacu(zhuyuanhao) Values ('" + zhuyuanhao + "')";
            return dBConn.ExecuteNonQuery(insert);
        }
        public int Updatakcpacuxinxiv(string ZYNumber, int isread)
        {
            string sql = "UPDATE kcpacuxinxi  SET IsRead='" + isread + "' where zhuyuanhao='" + ZYNumber + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int Deletekcpacu(string id)
        {
            string delete = "delete from kcpacu where id='" + id + "'";
            return dBConn.ExecuteNonQuery(delete);
        }
        public DataTable mzff()
        {
            string sql = "select name from MazuiFangfa";
            return dBConn.GetDataTable(sql);
        }
        public static int djtzfs(string ValueType, string value, int id)
        {
            string sql = string.Format("update kcpacu set " + ValueType + "=N'" + value + "'  where ID='" + id + "' ");
            return adims_DAL.DBConn.Update(sql, null);
        }
        public static int ddzcbdxz(string zyno, string sj)
        {
            string sql = string.Format("insert into kcpacu(zhuyuanhao,shijian) values('" + zyno + "','" + sj + "')");
            int i = adims_DAL.DBConn.Update(sql, null);
            return i;
        }
        public DataTable zqtzs(string zhuyuanhao)
        {
            string sql = "select * from JHzqtzs where zhuyuanhao='" + zhuyuanhao + "'";
            return dBConn.GetDataTable(sql);
        }
        public int zqtzsxz(Dictionary<string, string> dictionary)
        {
            string insert = @"INSERT INTO JHzqtzs(zhuyuanhao,mzxz,mzxzqt,qtsx,yqshzt,mzysqz,rq)VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
            string sql = string.Format(insert, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        public int zqtzsxg(Dictionary<string, string> dictionary)
        {
            string insert = @"UPDATE JHzqtzs SET mzxz = '{1}',mzxzqt = '{2}',qtsx = '{3}',yqshzt = '{4}',mzysqz = '{5}',rq = '{6}' WHERE zhuyuanhao ='{0}'";
            string sql = string.Format(insert, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        #endregion
        #endregion
    }
}
