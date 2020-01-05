using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;

namespace adims_DAL
{
   public class pacsdbhelp_oracle
    {
       public static string strConn = "Data Source=pacs;Password=pacs50;User ID=pacs50;";
       // string strConn = "Provider=IBMDADB2;Database=hrip_sz;Hostname=10.0.100.11;Protocol=TCPIP;Port=50000; Uid=ssmz;Pwd=ssmz";
        public DataTable GetDataTable(string sql)
        {
            using (OracleConnection conn = new OracleConnection(strConn))
            {
                OracleCommand cmd = new OracleCommand(sql, conn);
                try
                {
                    conn.Open();
                    OracleDataAdapter adp = new OracleDataAdapter(cmd);
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
            string sql = "select patID 病人编号,ZhuYuanNo 住院号,patName 病人姓名,patAge 病人年龄,patSex 病人性别,BedNo 床号,Patdpm 病区,SQZD 术前诊断,Oname 手术名称,Otime 手术日期,Oroom 手术间,OS 手术医生,mzfa 麻醉方法"
            + " from hrip.vw_op_aisth where Otime='" + dtime + "'";
            return this.GetDataTable(sql);
        }
        public DataTable GetpacsbyPatid(string patid)
        {
            string sql = "select * from emr_view_pacsinfo  where people_jianchabuwei='胸部'and people_zhuyuanno='" + patid + "'and rownum=1 order by apply_date desc";
            return this.GetDataTable(sql);
        }

    }
}