

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace ListenerRoutingLib
{
    public class SqlServerLink
    {
        #region <<Members>>

        private static readonly string connStr = ConfigurationManager.AppSettings["ConnectionString"];
        

        #endregion

        #region <<Methods>>

        /// <summary>
        /// 返回DataSet
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// 
        public int Upadte_jurisdiction_DAL(string sql, int module_index, int[] be_able)
        {
            SqlConnection con = new SqlConnection(connStr);
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            int i = 0, j = 0;
            char[] source;
            int src_count = 0;
            DataTable pj = new DataTable();
            pj = ds.Tables[0];
            for (i = 0; i < count; i++)
            {
                source = (pj.Rows[i][2].ToString()).ToCharArray();
                source[module_index] = ((be_able[i] == 1) ? '1' : '0');
                src_count = source.Length;
                StringBuilder destination = new StringBuilder();
                for (j = 0; j < src_count; j++) destination.Append(source[j]);
                pj.Rows[i][2] = destination.ToString();
            }
            return adp.Update(pj);
        }
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
                throw ex;
            }
        }

        #endregion

        #region <<数据操作>>
        public int InsertPaiban(List<string> list1, string tableName)
        {
            string SQL_PAIBAN = "insert into '" + tableName + "' (applyid,Cardno, PatZhuYuanID, Patname,Patage,Patsex,patdpm,Pattmd,Oname,Odate,os,Amethod,Patbedno,patid,asae,operaddress,ostate,oroom,ap1,ap2,ap3,sn1,sn2,on1,on2)"
                + "values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}', '{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','','','','','','','','')";
            string INSERT = string.Format(SQL_PAIBAN, list1.ToArray());
            return ExecuteNonQuery(INSERT);
        }

        public DataTable GetPaiban(string patid, string tableName)
        {
            string sql = "SELECT * from '" + tableName + "'  where patid='" + patid + "'";
            return GetDataTable(sql);
        }
        #endregion

    }
}

