using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_DAL
{

    public class admin_T_SQL
    {
        private DBConn dBConn = new DBConn();

        /// <summary>
        /// 器械清点的删除QiXieQD
        /// </summary>
        /// <param name="Model1"></param>
        /// <returns></returns>
        public int DeleteqxqdType(string Model1)
        {
            string delete = "delete [Admins_qxqdType] where [qxbType]='" + Model1 + "'";
            return dBConn.ExecuteNonQuery(string.Format(delete));
        }
        public int DeleteqxqdModel(int id)
        {
            string delete = "delete [Adims_qxqdModel] where qxqd_id=" + id + "";
            return dBConn.ExecuteNonQuery(string.Format(delete));
        }
        public int updateqxqdModel(string Model1)
        {
            string delete = "delete [Adims_qxqdModel] where [Model]='" + Model1 + "'";
            return dBConn.ExecuteNonQuery(string.Format(delete));
        }
    }
}
