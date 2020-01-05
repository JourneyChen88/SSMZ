using adims_MODEL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL
{

    public static class MzjldDal
    {
        private static DBConn dBConn = new DBConn();
        static string strconn = ConfigurationManager.AppSettings["ConnectionString"];

        /// <summary>
        /// 根据手术登记倒序查询
        /// </summary>
        /// <param name="mzid"></param>
        /// <returns></returns>
        public static DataTable GetOperByDate(string date)
        {
            string sql = $@"
SELECT a.patid,a.id mzjldid,b.Patname,b.Odate,b.Ostate,b.AP1,c.UserNo FROM Adims_Mzjld a
INNER JOIN Adims_OTypesetting b ON a.patid=b.PatID
LEFT JOIN Adims_User c ON b.AP1=c.user_name
WHERE Convert(VARCHAR(10),b.Odate,23)= '{date}' ";

            return dBConn.GetDataTable(sql);
        }
        public static DataTable GetOperDicByName(string OperName)
        {
            string sql = "select * from OperDic  where OperName='" + OperName + "' ";
            return dBConn.GetDataTable(sql);
        }

        public static int DelOperDic(DateTime time)
        {
            string sql = "delete  from OperDic  where CreateTime < ='" + time + "' ";
            return dBConn.ExecuteNonQuery(sql);
        }

        public static int InsertOperDic(string OperName, string OperCode, string Operlevel, string CutType, string QuickInput)
        {
            string sql = $@" INSERT INTO OperDic(OperName, OperCode, Operlevel, CutType, QuickInput)
                            VALUES('{OperName}', '{OperCode}','{Operlevel}', '{CutType}', '{QuickInput}' )";
            return dBConn.ExecuteNonQuery(sql);
        }
    }
}
