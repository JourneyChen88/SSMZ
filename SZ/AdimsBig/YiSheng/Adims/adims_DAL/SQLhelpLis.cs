using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace adims_DAL
{
    public class SQLhelpLis
    {
        string connStr = "Data Source=10.0.100.31;Initial Catalog=LIS;User ID=sa;Password=palgain";
        
        public DataTable GetDataTable(string sql)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                try
                {                   
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);                   
                    DataTable dt = ds.Tables[0];
                    return dt;
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                { conn.Close(); }
            }
            
        }

        public DataTable GetLISBYAGE(string dtime)
        {
            //string sql = "SELECT oroom 手术间,second 台次, patid 病人ID,patdpm 科室,patname 病人,patsex 性别,patage 年龄,patbedno 床号,patzhuyuanID 住院号,os 手术医师,oname 手术名字,pattmd 术前诊断,amethod 麻醉方法"
            //   + ",Otime"
            //string sql = "select * from hrip.vw_op_aisth where CONVERT(varchar, Odate , 23 ) ='" + dtime + "'";
            string sql = "select top 10( PATIENT_NAME,GENDER,WARD,BED_NO,AGE,DEPARTMENT,ITMENA,CURRENT_RESULT,OD_VALUE,CUTEOFF_VALUE,REPORT_LIMIT) from dbo.LABVWSTAT1 where AGE ='" + dtime + "'";
            return this.GetDataTable(sql);
        }
    }
}
