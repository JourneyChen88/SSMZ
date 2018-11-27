using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace adims_DAL
{

   public class DB2help
    {
       private DBConn dBConn = new DBConn();
       //string strConn = "Provider=System.Data.OleDb;Data Source=10.0.100.11,Initial Catalog=hrip_sz;UID=ssmz;PWD=ssmz;";
        //string strConn = "Provider=DB2OLEDB;Network Transport Library=TCPIP;Network Address=XXX.XXX.XXX.XXX;Initial Catalog=MyCtlg;Package Collection=MyPkgCol;Default Schema=Schema;User ID=myUsername;Password=myPassword"; 
        string strConn = "Provider=IBMDADB2;Database=hrip_sz;Hostname=10.0.100.11;Protocol=TCPIP;Port=50000; Uid=ssmz;Pwd=ssmz"; 
        public DataTable GetDataTable(string sql)
        {
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                try
                {
                    conn.Open();
                    OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    conn.Close();
                    DataTable dt = ds.Tables[0];
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 查询排班
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        /// 
       
        public DataTable GetPAIBAN(string dtime)
        {
            string sql = "SELECT oroom+'/'+second+'台' 手术间1,patname,patsex,patage,patbedno,oname,Amethod,AP1,ON1+'/'+SN1 as 器护o巡护 ,os  from Adims_OTypesetting where CONVERT(varchar, Odate , 23 ) ='" + dtime + "'" + "  order by oroom ";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetHISbyPatid(string patid)
        {
            string sql = "SELECT * from hrip.vw_op_aisth  where patid='" + patid + "'";            
            return  GetDataTable(sql);
        }
    }
}