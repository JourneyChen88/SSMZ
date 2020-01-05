using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace adims_DAL
{
    /// <summary>
    /// 器械包追溯接口
    /// </summary>
   public class Wzdb_DB_Help
    {
       private static readonly string connStr = ConfigurationManager.AppSettings["ConnectionStringWzdb"];
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
       public int UpdateQXB(string patid,string code) 
       {
           string sql = "update 供应室_追溯管理表 set 住院号='" + patid + "' where  条码号='" + code + "'";
           return ExecuteNonQuery(sql);
       }
    }
}
