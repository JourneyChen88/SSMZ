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
           string sql = "select patName ,patAge ,patSex ,Patdpm ,SQZD ,Oname ,Otime ,OS ,mzfa ,patID ,ZhuYuanNo ,BedNo "
            + ",applyid ,Cardno,asae,OPERADDRESS  from hrip.vw_op_aisth where Otime='" + dtime + "'";
            return this.GetDataTable(sql);
        }

        public DataTable GetHISbyPatid(string patid)
        {
            string sql = "SELECT * from hrip.vw_op_aisth  where patid='" + patid + "'";            
            return this.GetDataTable(sql);
        }
        public DataTable GetLisbyPatid(string serialnumber)
        {
            string sql = "select indextypename,indexname,retvalue,indexunit,retstatus"
            + ",retref from hrip.vw_op_lis_result where serialnumber='" + serialnumber + "' ";
            return this.GetDataTable(sql);
        }
        public DataTable GetHisMzff(string condition)
        {
            string sql = "SELECT Mzff_No,Mzff_Name from hrip.V_HisToSsmz_mzff  where 1=1 " + condition;
            return this.GetDataTable(sql);
        }
        public DataTable GetHisJbzd(string condition)
        {
            string sql = "SELECT * from hrip.V_HisToSsmz_jbzd  where 1=1 " + condition;
            return this.GetDataTable(sql);
        }
        public DataTable GetHisUser(string condition)
        {
            string sql = "SELECT * from hrip.V_HisToSsmz_Users  where 1=1 " + condition;
            return this.GetDataTable(sql);
        }

        /// <summary>
        /// 获取HIS手术列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable GetHisOperName(string condition)
        {
            string sql = "SELECT ITEMID 编码, ITEMNAME 手术名称,SSDJ 手术等级,SSQK 手术切口,INPUTSTR  from hrip.V_HISTOSSMZ_SSZD  where 1=1 " + condition;
            return this.GetDataTable(sql);
        }
        public DataTable GetHisOperNameAll(string condition)
        {
            string sql = "SELECT ITEMID , ITEMNAME ,SSDJ ,SSQK ,INPUTSTR  from hrip.V_HISTOSSMZ_SSZD  where 1=1 " + condition;
            return this.GetDataTable(sql);
        }

    }
}