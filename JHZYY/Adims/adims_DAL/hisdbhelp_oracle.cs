using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

namespace adims_DAL
{
    public class hisdbhelp_oracle
    {
        public static string strConn = "Data Source=his3;User Id=hysm;Password=hysm;";

        // string strConn = "Provider=IBMDADB2;Database=dbserver;Hostname=192.168.18.19;Protocol=TCPIP;Port=50000; Uid=SYSTEM;Pwd=LIUSUN";
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
        public DataTable GetHISbyPatid(string patid)
        {
            string sql = "select * from v_operation_emrs where zhuyuanhao='" + patid + "'";
            return this.GetDataTable(sql);
        }
        //public DataTable GetHISpaiban(string dtime)
        //{
        //    string sql = "select * from v_operation_interface  where to_char(Odate,'yyyy-MM-dd')='" + dtime + "'";
        //    return this.GetDataTable(sql);
        //}
        //public DataTable GetHISpaiban(string dtime)
        //{
        //    string sql = "select * from v_operation_interface where to_char(Odate,'yyyy-MM-dd')='" + dtime + "'";
        //    return this.GetDataTable(sql);
        //}
        public DataTable GetHISpaiban(string dtime)
        {
            string sql = "select ZYID,ZHUYUANNO||sqcs as zhuyuanid,CARDID,PATNAME,PATSEX,PATAGE,PATNATION,BedNo,PATTMD,ONAME,AMETHOD,OS,OS1,OS2,OS3,OS4,SUBSTR(to_char(ODATE,'yyyymmdd hh24:mi:ss'),10,17 ) as StartTime,ODATE,TIWEI,SSLB from v_operation_interface where to_char(Odate,'yyyy-MM-dd')='" + dtime + "'";
            return this.GetDataTable(sql);
        }
        public DataTable GetHISpaibanmenzhen(string dtime)
        {
            string sql = "select * from v_operation_menzhen  where patient_id='" + dtime + "' order by VISIT_DATE desc ";
            return this.GetDataTable(sql);
        }

        public DataTable GetHISpaibanzhuyuan(string dtime)
        {
            string sql = "select * from v_operation_hospital  where patient_id='" + dtime + "' order by VISIT_ID desc ";
            return this.GetDataTable(sql);
        }

        //        public DataTable GetHishaochai(string date1, string date2)
        //        {
        //            string sql = "SELECT T.ITEM_SPEC,T.ITEM_NAME, COUNT(T.ITEM_SPEC) FROM ( " +
        //"select * from medrec.v_operation_billdetail where DEPT_NAME='麻醉科' AND CLASS_NAME='材料' AND " +
        //"BILLING_DATE_TIME <to_date('"+date1+"', 'yyyy-mm-dd hh24:mi:ss') AND " +
        //"BILLING_DATE_TIME>to_date('" + date2 + "', 'yyyy-mm-dd hh24:mi:ss') " +
        //")T GROUP BY T.ITEM_SPEC,T.ITEM_NAME ";
        //            return this.GetDataTable(sql);
        //        }    

        public DataTable GetHishaochai(string date1, string date2)
        {
            string sql = "SELECT T.ITEM_SPEC as 厂家,T.ITEM_NAME as 材料, COUNT(T.ITEM_SPEC) as 数量 FROM ( " +
"select * from medrec.v_operation_billdetail where DEPT_NAME='麻醉科' AND CLASS_NAME='材料' AND " +
"BILLING_DATE_TIME <to_date('" + date2 + "', 'yyyy-mm-dd hh24:mi:ss') AND " +
"BILLING_DATE_TIME>to_date('" + date1 + "', 'yyyy-mm-dd hh24:mi:ss') " +
")T GROUP BY T.ITEM_SPEC,T.ITEM_NAME ";
            return this.GetDataTable(sql);
        }

        public DataTable GetHishaochainame()
        {
            string sql = "select CLASS_NAME from medrec.v_operation_billdetail  group by CLASS_NAME";
            //"select * from medrec.v_operation_billdetail where DEPT_NAME='麻醉科' AND CLASS_NAME='材料' AND " +
            //"BILLING_DATE_TIME <to_date('"+date1+"', 'yyyy-mm-dd hh24:mi:ss') AND " +
            //"BILLING_DATE_TIME>to_date('" + date2 + "', 'yyyy-mm-dd hh24:mi:ss') " +
            //")T GROUP BY T.ITEM_SPEC,T.ITEM_NAME ";
            return this.GetDataTable(sql);
        }

        public DataTable GetHishaochai1(string cailiao, string date1, string date2)
        {
            string sql = "SELECT T.ITEM_SPEC as 厂家,T.ITEM_NAME as 材料, COUNT(T.ITEM_SPEC) as 数量 FROM ( " +
"select * from medrec.v_operation_billdetail where DEPT_NAME='麻醉科' AND CLASS_NAME='" + cailiao + "' AND " +
"BILLING_DATE_TIME <to_date('" + date2 + "', 'yyyy-mm-dd hh24:mi:ss') AND " +
"BILLING_DATE_TIME>to_date('" + date1 + "', 'yyyy-mm-dd hh24:mi:ss') " +
")T GROUP BY T.ITEM_SPEC,T.ITEM_NAME ";
            return this.GetDataTable(sql);
        }
    }
}