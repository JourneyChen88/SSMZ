using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace adims_DAL
{
    public class GysHelp
    {
        private DBConn dBConn = new DBConn();
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="userno"></param>
        /// <returns></returns>
        public DataTable GetAdims_GongYS(string BarCode)
        {
            string select = "SELECT DeviceName,DeviceCount,DeviceBagName FROM Adims_GongYS where BarCode='" + BarCode + "' ";
            return dBConn.GetDataTable(select);
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="userno"></param>
        /// <returns></returns>
        public int InsAdims_GongYS(string BarCode, string DeviceName, string DeviceCount, string DeviceBagName)
        {
            string sql = "insert into Adims_GongYS(BarCode,DeviceName,DeviceCount,DeviceBagName) "
            + "values('" + BarCode + "','" + DeviceName + "','" + DeviceCount + "','" + DeviceBagName + "')";
            return dBConn.ExecuteNonQuery(sql);
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="userno"></param>
        /// <returns></returns>
        public int UpdAdims_GongYS(string BarCode, string DeviceName, string DeviceCount, string DeviceBagName)
        {
            string sql = "Update  adims_user set BarCode= '" + BarCode + "',DeviceName='" + DeviceName + "',"
            + " DeviceCount='" + DeviceCount + "',DeviceBagName='" + DeviceBagName + "' where BarCode='" + BarCode + "'";
            return dBConn.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="userno"></param>
        /// <returns></returns>
        public int DelAdims_GongYS(string id)
        {
            string sql = "delete from  adims_user where id='" + id + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
    }
}
