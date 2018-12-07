using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace adims_DAL
{
    public class HisDB_Help
    {
        #region <<Members>>

        private static readonly string connStr = ConfigurationManager.AppSettings["ConnectionStringHis"];


        #endregion

        #region <<Methods>>

        /// <summary>
        /// 返回DataSet
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// 
        public DataSet GetDataSet(string sql)
        {
            try
            {
                SqlConnection con = new SqlConnection(connStr);
                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                DalLogger.WriteErrorLog(ex);
                throw ex;
            }
        }

        public static void TestConnectDB()
        {
            SqlConnection myConn = new SqlConnection(connStr);
            try
            {
                myConn.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("连接数据库失败！错误原因：" + ex.Message);
            }
            finally
            {
                myConn.Close();
            }
        }
        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql)
        {
            try
            {
                SqlConnection con = new SqlConnection(connStr);
                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                DalLogger.WriteErrorLog(ex);
                throw ex;
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
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    i = cmd.ExecuteNonQuery();
                }
                return i;
            }
            catch (Exception ex)
            {
                DalLogger.WriteErrorLog(ex);
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
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    obj = cmd.ExecuteScalar();
                }
                return obj;
            }
            catch (Exception ex)
            {
                DalLogger.WriteErrorLog(ex);
                throw ex;
            }
        }

        #endregion

        public DataTable GetHisInfo(string dtime)
        {//convert(nvarchar(5),otime,24)as starttime,
            string sql = "select op_record_ID as patID ,ZhuYuanNo,patName,CardID,patAge,patSex,patNation,PatHeight,PatWeight,PatBloodType,BedNo,Patdpm,Pattmd,Oname,Odate,OS1,OS2,OS3,OS4,OS5,Amethod,BX, Tiwei, GR, BX,Ocode,SSLB,SSDJ,Remarks,StartTime,SQSJ"
            + " from V_Operation_Interface where Convert(varchar,Odate,23)='" + dtime + "'";
            return this.GetDataTable(sql);
        }
        public DataTable GetHisInfoByPatID(string PATID)
        {//convert(nvarchar(5),otime,24)as starttime,
            string sql = "select patID ,ZhuYuanNo,patName,CardID,patAge,patSex,patNation,BedNo,Patdpm,PatBingqu,SQZD,Oname,otime,OS,mzff,BX, TW, GR, BZ "
            + " from V_EMR_Operation_Interface WHERE PATID LIKE '%" + PATID + "'";
            return this.GetDataTable(sql);
        }
        public DataTable GetHisZhenduan(string sxname)
        {
            string sql = "SELECT FName,FSpell FROM V_EMR_Operation_Interface_Diag_list where FSpell like'" + sxname + "%'";
            return this.GetDataTable(sql);
        }
        public DataTable GetHisShoushu(string sxname)
        {
            string sql = "SELECT FName as 手术名称,flevel as 手术等级 FROM V_EMR_Operation_Interface_operation_list where FSpell like'" + sxname + "%'";
            return this.GetDataTable(sql);
        }
    }
}
