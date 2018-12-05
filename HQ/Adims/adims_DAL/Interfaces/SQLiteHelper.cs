using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;
using adims_MODEL;
using System.Data.Common;
namespace adims_DAL
{
    public class SQLiteHelper
    {
        /// <summary> 
        /// ConnectionString样例：Data Source=Test.db3;
        /// </summary> 
        public static string ConnectionString = "Data Source=" + System.IO.Directory.GetCurrentDirectory() + "\\Adims_Local.db;Version=3;";
        //public static string ConnectionString = "D:\\Adims_Local.db;";

        public static string getConnectionString(string path)
        {

            return getConnectionString(path, null);

        }
        DBConn dbconn = new DBConn();
        public static string getConnectionString(string path, string password)
        {

            if (string.IsNullOrEmpty(password))

                return "Data Source=" + path;

            return "Data Source=" + path + ";Password=" + password;

        }




        private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, string cmdText, params object[] p)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 30;
            if (p != null)
            {
                foreach (object parm in p)
                    cmd.Parameters.AddWithValue(string.Empty, parm);
            }
        }
        /// <summary>
        /// 增删改、返回影响行数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql)
        {
            try
            {
                int i = 0;
                using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
                    i = cmd.ExecuteNonQuery();
                }
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 返回第一行第一列
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public object ExecuteScalar(string sql)
        {
            try
            {
                object obj = null;
                using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
                    obj = cmd.ExecuteScalar();
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet datasetExecuteQuery(string cmdText, params object[] p)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand())
                {
                    DataSet ds = new DataSet();
                    PrepareCommand(command, conn, cmdText, p);
                    SQLiteDataAdapter da = new SQLiteDataAdapter(command);
                    da.Fill(ds);
                    return ds;
                }
            }
        }

        public static int ExecuteNonQuery(string cmdText, params object[] p)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand())
                {
                    PrepareCommand(command, conn, cmdText, p);
                    return command.ExecuteNonQuery();
                }
            }
        }
        public static SQLiteDataReader ExecuteReader(string cmdText, params object[] p)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand())
                {
                    PrepareCommand(command, conn, cmdText, p);
                    return command.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
        }
        public static object ExecuteScalar(string cmdText, params object[] p)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand())
                {
                    PrepareCommand(command, conn, cmdText, p);
                    return command.ExecuteScalar();
                }
            }
        }

        //public  DataTable GetDataTable(string cmdText, params object[] p)
        //{
        //    using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
        //    {
        //        using (SQLiteCommand command = new SQLiteCommand())
        //        {
        //            DataSet ds = new DataSet();
        //            PrepareCommand(command, conn, cmdText, p);
        //            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
        //            da.Fill(ds);
        //            return ds.Tables[0];
        //        }
        //    }
        //}
        public static DataTable GetDataTable(string sql)
        {
            try
            {
                SQLiteConnection con = new SQLiteConnection(ConnectionString);
                SQLiteDataAdapter adp = new SQLiteDataAdapter(sql, con);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable QueryMonitor(string dt)
        {

            string sql = "";
            return GetDataTable(sql);
        }
        #region 迈瑞
        //监护仪所有数据保存本地数据库
        public int InsertMonitor(MirayModel jhModel)
        {
            StringBuilder str = new StringBuilder();
            StringBuilder str1 = new StringBuilder();
            StringBuilder str2 = new StringBuilder();
            //if (jhModel.OfficeName1.ToString() != null)
            //{
            //    str1.Append("OfficeName,");
            //    str2.Append("'" + jhModel.OfficeName1 + "',");
            //}
            if (jhModel.BedId1.ToString() != null)
            {
                str1.Append("mzjldid,");
                str2.Append("" + jhModel.BedId1 + ",");
            }
            //if (jhModel.PatientMonitor_IP1.ToString() != null)
            //{
            //    str1.Append("PatientMonitor_IP,");
            //    str2.Append("'" + jhModel.PatientMonitor_IP1 + "',");
            //}

            //if (jhModel.IPSeq1.ToString() != null)
            //{
            //    str1.Append("IPSeq,");
            //    str2.Append("'" + jhModel.IPSeq1 + "',");
            //}

            if (jhModel.MeasureTime1.ToString() != null)
            {
                str1.Append("CreateTime,");
                str2.Append("'" + jhModel.MeasureTime1 + "',");
            }

            if (jhModel.HR1.ToString() != null)
            {
                str1.Append("HR,");
                str2.Append("'" + jhModel.HR1 + "',");
            }

            if (jhModel.SpO21.ToString() != null)
            {
                str1.Append("SpO2,");
                str2.Append("'" + jhModel.SpO21 + "',");
            }

            //if (jhModel.PVCs1.ToString() != null)
            //{
            //    str1.Append("PVCs,");
            //    str2.Append("'" + jhModel.PVCs1 + "',");
            //}

            //if (jhModel.ST_II1.ToString() != null)
            //{
            //    str1.Append("ST_II,");
            //    str2.Append("'" + jhModel.ST_II1 + "',");
            //}

            if (jhModel.RR1.ToString() != null)
            {
                str1.Append("RRC,");
                str2.Append("'" + jhModel.RR1 + "',");
            }

            if (jhModel.PR1.ToString() != null)
            {
                str1.Append("purse,");
                str2.Append("'" + jhModel.PR1 + "',");
            }

            if (jhModel.Dia1.ToString() != null)
            {
                str1.Append("NIBPD,");
                str2.Append("'" + jhModel.Dia1 + "',");
            }

            if (jhModel.Mean1.ToString() != null)
            {
                str1.Append("NIBPM,");
                str2.Append("'" + jhModel.Mean1 + "',");
            }

            if (jhModel.Sys1.ToString() != null)
            {
                str1.Append("NIBPS,");
                str2.Append("'" + jhModel.Sys1 + "',");
            }

            str.Append("insert into Adims_mzjld_Point (");
            str.Append(str1.ToString().Remove(str1.Length - 1));
            str.Append(")");
            str.Append(" values (");
            str.Append(str2.ToString().Remove(str2.Length - 1));
            str.Append(")");

            return ExecuteNonQuery(str.ToString());

        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sickid"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public DataTable GetMonitor(int mzjldid, DateTime MeasureTime)
        {

            string sql = @"select * from Adims_mzjld_Point where RecordTime ='"
+ MeasureTime + "'and mzjldid=" + mzjldid + " ";

            return GetDataTable(sql);
        }
      
    
        public int CopyDataPacu(int mzjldid, DateTime dtime)
        {
            string query = "select mzjldid,CreateTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP from Adims_MonitorRecord_PACU "
                + " where mzjldid='" + mzjldid + "' and CreateTime='" + dtime.ToString("yyyy-MM-dd HH:mm") + "'";
            DataTable dt = dbconn.GetDataTable(query);
            string queryServer = "select mzjldid,RecordTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP from Adims_PACU_Point "
               + " where mzjldid='" + mzjldid + "' and RecordTime='" + dtime + "'";
            DataTable dtServer = dbconn.GetDataTable(queryServer);
            int i = 0;
            if (dt.Rows.Count > 0 && dtServer.Rows.Count == 0)
            {
                string copy = "insert into Adims_Pacu_Point(mzjldid,RecordTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP) "
                    + "values('" + dt.Rows[0]["mzjldid"] + "','" + Convert.ToDateTime(dt.Rows[0]["CreateTime"]) + "','" + dt.Rows[0]["NIBPS"] + "','" + dt.Rows[0]["NIBPD"] + "',"
                    + "'" + dt.Rows[0]["NIBPM"] + "','" + dt.Rows[0]["RRC"] + "','" + dt.Rows[0]["HR"] + "','" + dt.Rows[0]["Pulse"] + "', '" + dt.Rows[0]["SpO2"] + "',"
                    + "'" + dt.Rows[0]["ETCO2"] + "', '" + dt.Rows[0]["TEMP"] + "')";
                i = dbconn.ExecuteNonQuery(copy);
            }
            return i;
        }
        #endregion
        #region GE监护仪
        /// <summary>
        /// GE  总数据
        /// </summary>
        public int insertJianCeDataMZJLD(int mzjldid, int nibps, int nibpd, int nibpm, int rrc, int hr, int pulse, int spo2, int etco2, double temp, string now)
        {
            string insert = "insert into Adims_mzjld_Point(mzjldid,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP,RecordTime) values(" + mzjldid + "," + nibps + "," + nibpd + "," + nibpm + "," + rrc + "," + hr + "," + pulse + "," + spo2 + "," + etco2 + "," + temp + ",'" + now + "')";
            return ExecuteNonQuery(insert);
        }
      
        /// <summary>
        /// 判断本地数据库是否有这个间隔时间的数据
        /// </summary>
        public DataTable selectJianCeData(string dt, int mzjldid, int TYPE)
        {
            string sel = "";
            if (TYPE == 0)
                sel = "select * from Adims_mzjld_Point where RecordTime='" + dt + "'and mzjldid='" + mzjldid + "'";
            if (TYPE == 1)
                sel = "select * from Adims_MonitorRecord_PACU where CreateTime='" + dt + "'and mzjldid='" + mzjldid + "'";
            return GetDataTable(sel);
        }

        public int insertJianCeDataPACU(int mzjldid, int nibps, int nibpd, int nibpm, int rrc, int hr, int pulse, int spo2, int etco2, double temp, string now)
        {
            string insert = "insert into Adims_MonitorRecord_PACU(mzjldid,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP,CreateTime) values(" + mzjldid + "," + nibps + "," + nibpd + "," + nibpm + "," + rrc + "," + hr + "," + pulse + "," + spo2 + "," + etco2 + "," + temp + ",'" + now + "')";
            return ExecuteNonQuery(insert);
        }
        //public int insertJianCeDataPACU(int mzjldid, int nibps, int nibpd, int nibpm, int rrc, int hr, int pulse, int spo2, int etco2, double temp, DateTime now)
        //{
        //    string insert = "insert into Adims_MonitorRecord_PACU(mzjldid,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP,CreateTime) values(" + mzjldid + "," + nibps + "," + nibpd + "," + nibpm + "," + rrc + "," + hr + "," + pulse + "," + spo2 + "," + etco2 + "," + temp + ",'" + now + "')";
        //    return ExecuteNonQuery(insert);
        //}
        #endregion
    }
}