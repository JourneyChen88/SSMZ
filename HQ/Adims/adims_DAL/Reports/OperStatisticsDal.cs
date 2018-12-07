using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL
{
   public  class OperStatisticsDal
    {
        private DBConn dBConn = new DBConn();
        /// <summary>
        /// 根据时间查询手术总量
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllMZLbyTime(DateTime dt1, DateTime dt2)
        {
            string selectbyTime = "select count(*) from Adims_Mzjld  WHERE  otime between '" + dt1 + "'and'" + dt2 + "'  and mzkssj>'1990-01-01' and ssjssj>'1990-01-01'";
            return dBConn.GetDataTable(string.Format(selectbyTime));
        }
        /// <summary>
        /// 根据护士查询手术总量
        /// </summary>
        /// <param name="sunall"></param>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataTable GetHSMZLbyTime(int sunall, DateTime dt1, DateTime dt2, string name)
        {
            string selectysbyTime = @"SELECT sum(DATEDIFF ( mi , mzkssj , ssjssj )),count(*),SUBSTRING(cast(count(*)*100.0/" + sunall + "  as nvarchar),1,4) +' %',(select count(*) from Adims_Mzjld where  qxhs like '%" + name + "%' and Otime between '" + dt1 + "'and '" + dt2 + "' and mzkssj>'1990-01-01' and ssjssj>'1990-01-01')as qxhs,(select count(*) from Adims_Mzjld where  xhhs like '%" + name + "%' and Otime between '" + dt1 + "'and '" + dt2 + "' and mzkssj>'1990-01-01' and ssjssj>'1990-01-01')as xhhs   from Adims_Mzjld  WHERE   Otime between '" + dt1 + "'and'" + dt2 + "' and (qxhs like '%" + name + "%' or xhhs like '%" + name + "%') and mzkssj>'1990-01-01' and ssjssj>'1990-01-01'";
            return dBConn.GetDataTable(string.Format(selectysbyTime));
        }
        /// <summary>
        /// 根据医生查询手术总量
        /// </summary>
        /// <returns></returns>
        public DataTable GetYSMZLbyTime(int sunall, DateTime dt1, DateTime dt2, string name)
        {
            string selectysbyTime = @"SELECT sum(DATEDIFF ( mi , mzkssj , ssjssj )),count(*),SUBSTRING(cast(count(*)*100.0/" + sunall + "  as nvarchar),1,4) +' %',(select count(*) from Adims_Mzjld where  mzys like '" + name + "%' and Otime between '" + dt1 + "'and '" + dt2 + "' and mzkssj>'1990-01-01' and ssjssj>'1990-01-01')as zmzys,(count(*)-(select count(*) from Adims_Mzjld where  mzys like '" + name + "%' and Otime between '" + dt1 + "'and '" + dt2 + "' and mzkssj>'1990-01-01' and ssjssj>'1990-01-01'))as fmzys   from Adims_Mzjld  WHERE   Otime between '" + dt1 + "'and'" + dt2 + "' and mzys like '%" + name + "%' and mzkssj>'1990-01-01' and ssjssj>'1990-01-01'";
            return dBConn.GetDataTable(string.Format(selectysbyTime));
        }
        /// <summary>
        /// 手术医生查询工作总量
        /// </summary>
        /// <param name="sunall"></param>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataTable GetSSYSbyTime(int sunall, DateTime dt1, DateTime dt2, string name)
        {
            string selectysbyTime = @"SELECT sum(DATEDIFF ( mi , mzkssj , ssjssj )),count(*),SUBSTRING(cast(count(*)*100.0/" + sunall + "  as nvarchar),1,4) +' %',(select count(*) from Adims_Mzjld where  ssys like '" + name + "%' and Otime between '" + dt1 + "'and '" + dt2 + "' and mzkssj>'1990-01-01' and ssjssj>'1990-01-01')as zmzys,(count(*)-(select count(*) from Adims_Mzjld where  ssys like '" + name + "%' and Otime between '" + dt1 + "'and '" + dt2 + "' and mzkssj>'1990-01-01' and ssjssj>'1990-01-01'))as fmzys   from Adims_Mzjld  WHERE   Otime between '" + dt1 + "'and'" + dt2 + "' and ssys like '%" + name + "%' and mzkssj>'1990-01-01' and ssjssj>'1990-01-01'";
            return dBConn.GetDataTable(string.Format(selectysbyTime));
        }
       
        public DataTable GetYSMZLbyOroom(DateTime dt1, DateTime dt2)
        {

            string selectysbyTime = "SELECT oroom, sum(DATEDIFF ( mi , mzkssj , ssjssj )) as mzsj,count(*) as ssNum,SUBSTRING(cast(count(*) as nvarchar),1,4) +' %' as Probability  from Adims_Mzjld as M join Adims_OperSchedule as O on O.PatZhuYuanID=M.PatID WHERE  Otime between '" + dt1 + "'and'" + dt2 + "' and mzkssj>'1990-01-01' and ssjssj>'1990-01-01' group by oroom ";
            return dBConn.GetDataTable(string.Format(selectysbyTime));
        }

     
        public DataTable GetYSMZLbyKeshi(DateTime dt1, DateTime dt2)
        {
            string selectysbyTime = "select ao.patdpm, sum(DATEDIFF ( mi , mzkssj , ssjssj )),count(*) from Adims_OperSchedule as ao join Adims_Mzjld as am on ao.PatZhuYuanID=am.patid "
                                   + " WHERE   otime between '" + dt1 + "'and'" + dt2 + "' and mzkssj>'1990-01-01' and ssjssj>'1990-01-01' group by ao.Patdpm ";
            return dBConn.GetDataTable(string.Format(selectysbyTime));
        }

        /// <summary>
        /// 麻醉总结
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public DataTable GetMzzj_Status(DateTime date1, DateTime date2)
        {
            string select1 = @"SELECT row_number() over(order by ot.odate)as row_id, OT.PatZhuYuanID,Convert(nvarchar,ot.odate,23) as otime,
 OT.Oroom,OT.PatName,OT.patsex,OT.patage,OT.Patdpm,m.Szzd,ot.oname,ot.Amethod,m.MazuiFS,M.asa,m.Ssys,m.mzys,m.qxhs,m.Xhhs ,
 (case when(select count(*) from Adims_AnesthesiaSummary where patid=m.patid  )>0 then '已做' else '未做' end) as yizuo
 FROM Adims_OperSchedule AS OT  left join  Adims_mzjld AS M on M.patid = OT.PatId 
 where (otime between '" + date1 + "'and'" + date2 + "') and ot.ostate>=1";
            return dBConn.GetDataTable(string.Format(select1));
        }

        /// <summary>
        /// 术后访视状态
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public DataTable GetAfterVisit_YS_Status(DateTime date1, DateTime date2)
        {
            string select1 = @"SELECT row_number() over(order by ot.odate)as row_id, OT.PatZhuYuanID,Convert(nvarchar,ot.odate,23) as otime,
OT.Oroom,OT.PatName,OT.patsex,OT.patage,OT.Patdpm,m.Szzd,ot.oname,ot.Amethod,m.MazuiFS,M.asa,m.Ssys,m.mzys,m.qxhs,m.Xhhs ,
(case when(select count(*) from Adims_AfterVisit_YS where patid=m.patid  )>0 then '已做' else '未做' end) as yizuo
 FROM Adims_OperSchedule  
AS OT  left join  Adims_mzjld AS M on M.patid = OT.patid 
 where (otime between '" + date1 + "'and'" + date2 + "') and ot.ostate>=1";
            return dBConn.GetDataTable(string.Format(select1));
        }
        /// <summary>
        /// 护士术前访视状态
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public DataTable GetBeforeVisit_HS_Status(DateTime date1, DateTime date2)
        {
            string select1 = @"SELECT row_number() over(order by ot.odate)as row_id, OT.PatZhuYuanID,Convert(nvarchar,ot.odate,23) as otime,
OT.Oroom,OT.PatName,OT.patsex,OT.patage,OT.Patdpm,
m.Szzd,ot.oname,ot.Amethod,m.MazuiFS,M.asa,m.Ssys,m.mzys,m.qxhs,m.Xhhs ,
(case when(select count(*) from Adims_BeforeVisit_HS where patid= OT.patid )>0 then '已做' else '未做' end) as yizuo
 FROM Adims_OperSchedule  
AS OT  left join  Adims_mzjld AS M on M.patid = OT.patid 
 where (otime between '" + date1 + "'and'" + date2 + "') and ot.ostate>=1";
            return dBConn.GetDataTable(string.Format(select1));
        }

        /// <summary>
        /// 医生术前访视状态
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public DataTable GetBeforeVisit_YS_Status(DateTime date1, DateTime date2)
        {
            string select1 = @"SELECT row_number() over(order by ot.odate)as row_id, OT.PatZhuYuanID,Convert(nvarchar,ot.odate,23) as otime,
            OT.Oroom,OT.PatName,OT.patsex,OT.patage,OT.Patdpm,
            m.Szzd,ot.oname,ot.Amethod,m.MazuiFS,M.asa,m.Ssys,m.mzys,m.qxhs,m.Xhhs ,
            (case when(select count(*) from Adims_BeforeVisit_YS where patid= OT.patid)>0 then '已做' else '未做' end) as yizuo
             FROM Adims_OperSchedule  
            AS OT  left join  Adims_mzjld AS M on M.patid = OT.patid 
             where otime between '" + date1 + "'and'" + date2 + "' and ot.ostate>=1";
            return dBConn.GetDataTable(string.Format(select1));
        }
        /// <summary>
        /// PACU查询
        /// </summary>
        /// <param name="name"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public DataTable GetPACU_Name(string name, DateTime date1, DateTime date2)
        {
            string select1 = @"SELECT row_number() over(order by P.otime) as row_id, P.mzjldid AS mzid,OT.PatZhuYuanID,Convert(nvarchar,m.otime,23) as otime,
Oroom,PatName,patsex,patage,Patdpm,(select case ot.isjizhen when 0 then '择期' else '急诊' end )as jizhen
,(left(Convert(varchar,P.rssj,108),(len(Convert(varchar,P.rssj,108))-3)))as rssj,(left(Convert(varchar,P.cssj,108),
(len(Convert(varchar,P.cssj,108))-3)))as cssj,
m.Szzd,ot.oname,ot.Amethod,m.MazuiFS,M.asa,m.Ssys,m.mzys,P.ZXHS,h.Ssjb,h.Fxpg,h.shengfen,ot.Remarks
 FROM Adims_PACU as P left join Adims_mzjld AS M on P.mzjldid=m.id  left join Adims_OperSchedule  
AS OT on M.patid = OT.PatZhuYuanID left join Adims_BeforeVisit_HS as H on h.patid=OT.patid"
            + " where P.Odate between '" + date1 + "'and'" + date2 + "'and (m.mazuifs like '%" + name + "%'or M.asa like '%" + name + "%' or m.mzys like '%" + name + "%'or patdpm like '%" + name + "%'or Oroom like '%" + name + "%' or mzxg like '%" + name + "%' or P.ZXHS like '%" + name + "%' or h.ssjb like '%" + name + "%'or h.SSLB like '%" + name + "%' or h.Fxpg like '%" + name + "%' or  m.Ssys like '%" + name + "%' or  h.shengfen like '%" + name + "%') and  P.rssj>'1990-01-01' and P.cssj>'1990-01-01' and ot.odate>=1";
            return dBConn.GetDataTable(string.Format(select1));

        }

        /// <summary>
        /// 麻醉记录统计查询
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable GetMzjldByCondition(string condition)
        {
            string str = @"  select row_number() over(order by b.odate)as row_id, A.id,A.patid, 
            convert(varchar,b.odate,23) AS OTIME,oroom,patdpm,patname,patsex,patage,A.asa,shoushufs,tw, 
            mazuifs,ssys,mzys,sqzd from adims_mzjld as A left join Adims_OperSchedule as B on A.patid=B.patid  
            WHERE b.ostate>=1 and   a.mzkssj>'1990-01-01' and a.ssjssj>'1990-01-01' and ";
            string str1 = str + condition;
            return dBConn.GetDataTable(str1);
        }
        /// <summary>
        /// 分麻醉医生统计信息
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetByYSName(string name, DateTime date1, DateTime date2)
        {
            string select1 = @"SELECT row_number() over(order by m.otime) as row_id, M.id AS mzid,OT.PatZhuYuanID,Convert(nvarchar,otime,23) as otime,
Oroom,PatName,patsex,patage,Patdpm,(select case ot.isjizhen when 0 then '择期' else '急诊' end )as jizhen
,(left(Convert(varchar,otime,108),(len(Convert(varchar,otime,108))-3)))as rssj,(left(Convert(varchar,sskssj,108),
(len(Convert(varchar,sskssj,108))-3)))as kssj,(left(Convert(varchar,(select max(RecordTime)
 from Adims_MonitorRecord where mzjldid=m.id),108),
(len(Convert(varchar,(select max(RecordTime)
 from Adims_MonitorRecord where mzjldid=m.id),108))-3)))as jssj
,Szzd,ot.oname,ot.Amethod,MazuiFS,M.asa,Ssys,mzys,qxhs,Xhhs,h.Ssjb,h.Fxpg,h.shengfen,ot.Remarks
 FROM Adims_mzjld AS M  left join Adims_OperSchedule  
AS OT on M.patid = OT.PatId left join Adims_BeforeVisit_HS as H on h.PatId=OT.PatId "
            + " where otime between '" + date1 + "'and'" + date2 + "'"
            + " and (mazuifs like '%" + name + "%'or M.asa like '%" + name + "%'or mzys like '%" + name + "%'or patdpm like '%" + name + "%'or Oroom like '%" + name + "%' or mzxg like '%" + name + "%' or Xhhs like '%" + name + "%' or qxhs like '%" + name + "%' or h.ssjb like '%" + name + "%' or h.SSLB like '%" + name + "%' or h.Fxpg like '%" + name + "%' or  m.Ssys like '%" + name + "%' or  h.shengfen like '%" + name + "%') "
            + " and  mzkssj>'1990-01-01' and ssjssj>'1990-01-01' and ot.odate>=1 ";
            return dBConn.GetDataTable(string.Format(select1));

        }
        /// <summary>
        /// 查询麻醉记录
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public DataTable GetFromStartToEndTime(DateTime date1, DateTime date2)
        {
            string select1 = @"SELECT row_number() over(order by m.otime) as row_id, M.id AS mzid,OT.PatZhuYuanID,Convert(nvarchar,otime,23) as otime,
Oroom,PatName,patsex,patage,Patdpm,(select case ot.isjizhen when 0 then '择期' else '急诊' end )as jizhen
,(left(Convert(varchar,otime,108),(len(Convert(varchar,otime,108))-3)))as rssj,(left(Convert(varchar,sskssj,108),
(len(Convert(varchar,sskssj,108))-3)))as kssj,(left(Convert(varchar,(select max(RecordTime)
 from Adims_MonitorRecord where mzjldid=m.id),108),
(len(Convert(varchar,(select max(RecordTime)
 from Adims_MonitorRecord where mzjldid=m.id),108))-3)))as jssj
,Szzd,ot.oname,ot.Amethod,MazuiFS,M.asa,Ssys,mzys,qxhs,Xhhs,h.Ssjb,h.Fxpg,h.shengfen,ot.Remarks
 FROM Adims_mzjld AS M  left join Adims_OperSchedule  
AS OT on M.patid = OT.patid left join Adims_BeforeVisit_HS as H on h.patid=OT.patid"
+ " where otime between '" + date1 + "'and'" + date2 + "' "
+ " and mzkssj>'1990-01-01' and ssjssj>'1990-01-01'  and ot.odate>=1 ";

            return dBConn.GetDataTable(string.Format(select1));
        }
    }
}
