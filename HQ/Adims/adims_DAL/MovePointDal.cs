using adims_MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace adims_DAL
{
    public class MovePointDal
    {
        private DBConn dBConn = new DBConn();     

        /// <summary>
        /// 获取HIS所有排班信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetAdims_mzjld_PointCount()
        {
            string sql = @"  select count(a.id)  from adims_mzjld a 
inner join Adims_mzjld_Point b on a.id=b.mzjldid
where a.IsZoom=1";
            return dBConn.GetDataTable(sql);
        }

     
        public DataTable GetAdims_MonitorRecordCount()
        {
            string sql = @"
   select count(a.id)  from adims_mzjld a 
inner join Adims_MonitorRecord b on a.id=b.mzjldid
where a.IsZoom=1";
            return dBConn.GetDataTable(sql);
        }
   

        public int InsertAdims_MonitorRecord(Mzjld_Point mr)
        {
            string sql = $@"    INSERT INTO dbo.Adims_MonitorRecord(mzjldid, RecordTime, NIBPS, 
NIBPD, NIBPM, RRC, HR, Pulse, SpO2,
ETCO2, TEMP,  CVP)
VALUES(
'{mr.Mzjldid}','{mr.RecordTime}',
'{mr.NIBPS}','{mr.NIBPD}',
'{mr.NIBPM}','{mr.RRC}','{mr.HR}',
'{mr.Pulse}','{mr.SpO2}','{mr.ETCO2}',
'{mr.Temp}','{mr.CVP}'  )";
            return dBConn.ExecuteNonQuery(sql);
        }
         public int UpdateAdims_OperSchedule( )
        {
            string sql = @"UPDATE Adims_OperSchedule SET Ostate=2 WHERE Ostate=1 AND Odate<'2019-01-01'";
            return dBConn.ExecuteNonQuery(sql);
        }

        public int InsertAdims_mzjld_Point(MonitorRecord mr)
        {
            string sql = $@"    INSERT INTO dbo.Adims_mzjld_Point(mzjldid, RecordTime, NIBPS, 
NIBPD, NIBPM, RRC, HR, Pulse, SpO2,
ETCO2, TEMP,  CVP)
VALUES(
'{mr.Mzjldid}','{mr.RecordTime}',
'{mr.NIBPS}','{mr.NIBPD}',
'{mr.NIBPM}','{mr.RRC}','{mr.HR}',
'{mr.Pulse}','{mr.SpO2}','{mr.ETCO2}',
'{mr.TEMP}','{mr.CVP}'  )";
            return dBConn.ExecuteNonQuery(sql);
        }
  
    }
}
