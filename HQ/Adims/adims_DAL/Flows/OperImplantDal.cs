using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL.Flows
{
    /// <summary>
    /// 手术室植入物Dal
    /// </summary>
    public class OperImplantDal
    {
        private DBConn dBConn = new DBConn();


        /// <summary>
        /// 植入物查询
        /// </summary>
        /// <param name="name"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public DataTable GetOperImplant_ByTimeSpan(string name, DateTime date1, DateTime date2)
        {
            string select1 = @"SELECT row_number() over(order by ot.id) as row_id,OT.PatZhuYuanID,Convert(nvarchar,ot.odate,23) as otime,
                    Oroom,PatName,patsex,patage,Patdpm,oname,s.ZW_name,s.ZW_XH,s.ZW_SL,s.ZW_CJ
                     FROM Adims_OperSchedule  AS OT left join Adims_OperImplant as s 
                    on ot.PatId=s.PatId "
                    + " where s.Odate between '" + date1 + "'and'" + date2 + "'and s.ZW_CJ like '%" + name + "%'";
            return dBConn.GetDataTable(string.Format(select1));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="DateType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int UpdateOperImplant(string id, string DateType, string value)
        {
            string sql = "UPDATE Adims_OperImplant WITH (ROWLOCK) SET " + DateType + " = '" + value + "' WHERE id = '" + id + "'";
            return dBConn.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 手术室植入物
        /// </summary>
        /// <returns></returns>
        public DataTable GetOperImplant(string Patid)
        {
            string selectAllYS = " SELECT id,ZW_name,ZW_CJ,ZW_XH,ZW_SL,Odate FROM Adims_OperImplant where Patid='" + Patid + "' order by id";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public int InsertOperImplant(string patid, string Odate)
        {
            string update = "insert Adims_OperImplant(patid,Odate) values('" + patid + "','" + Odate + "')";
            return dBConn.ExecuteNonQuery(string.Format(update));
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public int DeleteOperImplant(string id)
        {
            string update = "delete Adims_OperImplant where id='" + id + "'";
            return dBConn.ExecuteNonQuery(string.Format(update));
        }

    }
}
