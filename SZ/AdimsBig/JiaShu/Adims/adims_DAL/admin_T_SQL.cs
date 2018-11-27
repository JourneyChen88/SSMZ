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
        public int DeleteqxqdModel1(string Model1)
        {
            string delete = "delete [Adims_qxqdModel] where [Model]='" + Model1 + "'";
            return dBConn.ExecuteNonQuery(string.Format(delete));
        }
    }
}
