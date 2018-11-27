using adims_MODEL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL
{

    public class PaibanDal
    {
        private static DBConn dBConn = new DBConn();
        static string strconn = ConfigurationManager.AppSettings["ConnectionString"];

        public static DataTable GetOsAndSSYS()
        {
            string sql = $@" select os,oa1,oa2,oa3,ssys,a.PatID from Adims_OTypesetting a
 inner join Adims_Mzjld b on a.PatID=b.patid
where os+'、'+oa1+'、'+oa2+'、'+oa3!=ssys
and os+'、'+oa1+'、'+oa2!=ssys
and os+'、'+oa1!=ssys
and os!=ssys ";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 查询病人的手术医生和助手
        /// </summary>
        /// <param name="mzid"></param>
        /// <returns></returns>
        public static DataTable GetPiabanOsAndNo(string patid)
        {
            string sql = $@"select os DoctorName,osno DoctorNo from Adims_OTypesetting 
                            where patid = '{patid}' and os!= ''
                            union all
                            select OA1, OA1No from Adims_OTypesetting
                             where patid = '{patid}' and OA1!= ''
                            union all
                            select OA2, OA2No from Adims_OTypesetting
                             where patid = '{patid}' and OA2!= ''
                            union all
                            select OA3, OA3No from Adims_OTypesetting
                             where patid = '{patid}' and OA3!= '' ";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 获取麻醉医生
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        public static DataTable GetPiabanOA(string patid)
        {
            string sql = $@"select AP1 DoctorName,AP1NO DoctorNo from Adims_OTypesetting 
                            where patid = '{patid}' and AP1!= ''
                            union all
                            select AP2, AP2NO from Adims_OTypesetting
                             where patid = '{patid}' and AP2!= ''
                            union all
                            select AP3, AP3NO from Adims_OTypesetting
                             where patid = '{patid}' and AP3!= '' ";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 获取巡回护士
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        public static DataTable GetPiabanON(string patid)
        {
            string sql = $@"select ON1 DoctorName,ON1NO DoctorNo from Adims_OTypesetting 
                            where patid = '{patid}' and ON1!= ''
                            union all
                            select ON2, ON2NO from Adims_OTypesetting
                             where patid = '{patid}' and ON2!= ''  ";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 获取器械护士
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        public static DataTable GetPiabanSN(string patid)
        {
            string sql = $@"select SN1 DoctorName,SN1NO DoctorNo from Adims_OTypesetting 
                            where patid = '{patid}' and SN1!= ''
                            union all
                            select SN2, SN1NO from Adims_OTypesetting
                             where patid = '{patid}' and SN2!= ''  ";
            return dBConn.GetDataTable(sql);
        }

        public static int ExecuteUpdate(string sql)
        {
            return dBConn.ExecuteNonQuery(sql);
        }
    }
}
