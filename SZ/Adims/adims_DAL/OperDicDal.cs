using adims_MODEL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL
{

    public static class OperDicDal
    {
        private static DBConn dBConn = new DBConn();
        static string strconn = ConfigurationManager.AppSettings["ConnectionString"];

        /// <summary>
        /// 根据手术登记倒序查询
        /// </summary>
        /// <param name="mzid"></param>
        /// <returns></returns>
        public static DataTable GetOperDic()
        {
            string sql = "select * from OperDic   order by QuickInput  ";

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
