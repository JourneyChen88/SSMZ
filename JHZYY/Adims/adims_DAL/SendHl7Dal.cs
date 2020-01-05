using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL
{
   public class SendHl7Dal
    {
        private DBConn dBConn = new DBConn();

        static string strconn = ConfigurationManager.AppSettings["ConnectionString"];

        /// <summary>
        /// 根据手术登记倒序查询
        /// </summary>
        /// <param name="mzid"></param>
        /// <returns></returns>
        public  DataTable GetOperByDate(string date)
        {
            string sql = $@"
SELECT a.patid,a.id mzjldid,b.Patname,b.Odate,b.Ostate,b.AP1,c.UserNo FROM Adims_Mzjld a
INNER JOIN Adims_OTypesetting b ON a.patid=b.PatID
LEFT JOIN Adims_User c ON b.AP1=c.user_name
WHERE Convert(VARCHAR(10),b.Odate,23)= '{date}' ";

            return dBConn.GetDataTable(sql);
        }
        public DataTable GetMzffID(string MZFF)
        {
            string sql = "SELECT Mzff_No  FROM [mazuifangan] where [name]='" + MZFF + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetUserNoByName(string userName)
        {
            string sql = "select uid as UserNo from Adims_User where user_name='" + userName + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetOperNo(string OperName)
        {
            string sql = "SELECT *  FROM OperDic where OperName='" + OperName + "'";
            return dBConn.GetDataTable(sql);
        }

        public DataTable GetShoushuYishengNo(string name)
        {
            string update = "select nameNo from ShoushuYisheng where Name='" + name + "'";
            return dBConn.GetDataTable(string.Format(update));
        }

        public DataTable GetPaibanAndMZJLD(string patid)
        {
            string sql = "  SELECT O.PatID,O.ApplyID,O.Amethod,M.sskssj,M.ssjssj,M.Otime,M.ssss,O.Oroom,O.Second,O.OperNo,O.Oname,O.IsZhuYuan,O.PidInfo,O.Pv1Info,"
            + " Olevel,Amethod,GL,JZ,AP1,AP2,AP3,OA1,OA2,OA3,OA1No,OA2No,OA3No,OS,OsNo,TP,ON1,ON2,ON3,SN1,SN2,Remarks,M.asa,M.asae,Ostate,Odate "
            + " FROM HeYiAMIS.dbo.Adims_OTypesetting  as O LEFT JOIN Adims_Mzjld as M ON O.PatID =M.patid"
            + " WHERE O.patid='" + patid + "'";
            return dBConn.GetDataTable(sql);
        }

        /// <summary>
        /// 根据住院号获取排班信息
        /// </summary>
        /// <param name="zhuyuanid"></param>
        /// <returns></returns>
        public DataTable GetPaiban(string zhuyuanid)
        {
            string sql = "SELECT * from Adims_OTypesetting where PatZhuYuanId='" + zhuyuanid + "'";
            return dBConn.GetDataTable(sql);
        }
        public int UpdatePaibanConfig(string PatZhuYuanId)
        {
            string sql = "UPDATE Adims_OTypesetting WITH (ROWLOCK) Ostate='1'  WHERE PatZhuYuanId = '" + PatZhuYuanId + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        public DataTable GetPaiBanWhere(string dtime, string sqlWhere)
        {
            string sql =
                string.Format(@"SELECT id ,oroom,second ,patdpm,patbedno,patname,
                patage,patsex,oname,pattmd,os,amethod,on1,on2 ,sn1,sn2,Remarks,ap1 ,ap2 ,ap3
                ,applyID,patZhuYuanID,patid,ASAE from Adims_OTypesetting  
                where CONVERT(varchar,Odate,23) ='{0}' ", dtime)
                + sqlWhere;

            return dBConn.GetDataTable(sql);
        }
    
    }
}
