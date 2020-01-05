using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using adims_DAL;

namespace adims_BLL
{
    public class mz
    {
        private DBConn dBConn = new DBConn();
        public DataTable GetALLPAIBAN_HQ1(string zyh)
        {
            string sql = "SELECT * from Adims_OTypesetting  where  PatZhuYuanID='" + zyh + "' order by odate desc ";//方法下取sql得值
            return dBConn.GetDataTable(sql);

        }
    }
}
