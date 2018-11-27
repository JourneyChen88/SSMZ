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

        public DataTable GetPAIBAN(string dtime)
        {
            //string sql = "SELECT oroom 手术间,second 台次, patid 病人ID,patdpm 科室,patname 病人,patsex 性别,patage 年龄,patbedno 床号,patzhuyuanID 住院号,os 手术医师,oname 手术名字,pattmd 术前诊断,amethod 麻醉方法"
            //   + ",Otime"
            //string sql = "select * from hrip.vw_op_aisth where CONVERT(varchar, Odate , 23 ) ='" + dtime + "'";
            string sql = "select patName 病人姓名,BedNo 床号,patAge 病人年龄,patSex 病人性别,Patdpm 病区,SQZD 术前诊断,Oname 手术名称,Otime 手术日期,Oroom 手术间,mzfa 麻醉方法,OS 手术医生"
            + " from hrip.vw_op_aisth where Otime='" + dtime + "'";
            return this.GetDataTable(sql);
        }
        public DataTable GetHISbyPatid(string patid)
        {
            string sql = "SELECT * from hrip.vw_op_aisth  where patid='" + patid + "'";            
            return this.GetDataTable(sql);
        }
    }
}