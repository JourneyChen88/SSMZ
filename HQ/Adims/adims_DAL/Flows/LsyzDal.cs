using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL.Flows
{
  public  class LsyzDal
    {
        private DBConn dBConn = new DBConn();
        

        /// <summary>
        /// 查询临时医嘱
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public DataTable SelectLSYZ(string mzjldid)
        {
            string countQJ = "select id,mzjldid,kldate,kltime,yizhu,yisheng,hushi,zxdate,zxtime,remark from Adims_LSYZ where mzjldid='" + mzjldid + "' ";
            return dBConn.GetDataTable(string.Format(countQJ));
        }
        /// <summary>
        /// 新增临时医嘱
        /// </summary>
        public int InsertLSYZ(string mzjldid, string kldate, string kltime)
        {
            string Insert = "Insert into Adims_LSYZ(mzjldid,klDate,klTime) values('" + mzjldid + "','" + kldate + "','" + kltime + "') ";
            return dBConn.ExecuteNonQuery(string.Format(Insert));
        }
        /// <summary>
        /// 修改临时医嘱
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public int updateLSYZ(int id, string DataType, string DataValue)
        {
            string countQJ = "Update Adims_LSYZ set  " + DataType + " = '" + DataValue + "'  where id='" + id + "'";
            return dBConn.ExecuteNonQuery(string.Format(countQJ));
        }
        public int updateLSYZdatetime(int id, string zxDate, string zxTime)
        {
            string countQJ = "Update Adims_LSYZ set  zxDate = '" + zxDate + "', zxTime = '" + zxTime + "'  where id='" + id + "'";
            return dBConn.ExecuteNonQuery(string.Format(countQJ));
        }
        /// <summary>
        /// shanchu临时医嘱
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public int deleteLSYZ(int id)
        {
            string countQJ = "delete from Adims_LSYZ  where id='" + id + "'";
            return dBConn.ExecuteNonQuery(string.Format(countQJ));
        }

    }
}
