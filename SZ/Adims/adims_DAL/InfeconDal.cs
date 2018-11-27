using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MODEL;
using System.IO;

namespace adims_DAL
{
    public class InfeconDal
    {
        private DBConn dBConn = new DBConn();
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="userno"></param>
        /// <returns></returns>
        public DataTable GetFlowUse(string BarCode)
        {
            string select = "SELECT * FROM flowUse where BarCode='" + BarCode + "' ";
            return dBConn.GetDataTable(select);
        }

        public DataTable GetFlowUse(DateTime start, DateTime end)
        {
            string select = "SELECT * FROM flowUse where UseDate between '" + start + "'  and  '" + end + "'";
            return dBConn.GetDataTable(select);
        }

        public DataTable GetFlowUseList(string BarCode)
        {
            string select = "SELECT * FROM FlowUseList where BarCode='" + BarCode + "' ";
            return dBConn.GetDataTable(select);
        }

        public DataTable GetFlowUseAndList(DateTime start, DateTime end)
        {
            string select = @"select a.UseID, b.StorageID from FlowUse a
                inner join FlowUseList b  on a.UseID= b.UseID
                where a.UseDate between '{0}'  and  '{1}'";
            select = string.Format(select, start, end);
            return dBConn.GetDataTable(select);
        }

        public DataTable GetFlowUseList(DateTime start, DateTime end)
        {
            string select = @"select * from(
             select a.UseID,a.OrgID,a.OPID,a.UseDate, b.StorageID, c.Barcode
             ,'病人：'+d.PatientName+' '+d.PatientSex  as ContentMerge 
             from FlowUse a
            inner join FlowUseList b  on a.UseID= b.UseID
            inner join PatientList d on a.PatientID=d.PatientID
            inner join StorageList c on b.StorageID= c.StorageID 
            where a.UseDate between '{0}'  and  '{1}') as a
            left join
            (
            SELECT distinct Content ,KeyID1 
             FROM SysLog where (LogTime  between '{0}'  and  '{1}') AND LogType = 8) AS B
              ON A.StorageID=B.KeyID1 order by a.Barcode";
            select = string.Format(select, start, end);
            return dBConn.GetDataTable(select);
        }



        public DataTable GetSysLogByTime(DateTime start, DateTime end)
        {
            string select = @"select KeyId1 from SysLog a where LogType=8 and (LogTime between '{0}'  and  '{1}')";
            select = string.Format(select, start, end);
            return dBConn.GetDataTable(select);
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="userno"></param>
        /// <returns></returns>
        public int InsertSyslog(SysLog log)
        {
            string sql = string.Format(@"insert into SysLog(IsManualAdd,LogID,LogType,Content,LogTime,Operator,KeyID1,KeyID2,LogLevel,FlowOrgID) 
                                                    values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')"
                , log.IsManualAdd, log.LogID, log.LogType, log.Content, log.LogTime, log.Operator, log.KeyID1, log.KeyID2, log.LogLevel, log.FlowOrgID);

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
