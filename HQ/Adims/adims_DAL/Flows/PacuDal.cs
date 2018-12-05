using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace adims_DAL
{
    public class PacuDal
    {
         private DBConn dBConn = new DBConn();
     

        public DataTable GetPACUList(DateTime date1, DateTime date2)
        {
            string select1 = @"SELECT row_number() over(order by P.otime) as row_id, P.mzjldid AS mzid,OT.PatZhuYuanID,Convert(nvarchar,m.otime,23) as otime,
Oroom,PatName,patsex,patage,Patdpm,(select case ot.isjizhen when 0 then '择期' else '急诊' end )as jizhen
,(left(Convert(varchar,P.rssj,108),(len(Convert(varchar,P.rssj,108))-3)))as rssj,(left(Convert(varchar,P.cssj,108),
(len(Convert(varchar,P.cssj,108))-3)))as cssj,
m.Szzd,ot.oname,ot.Amethod,m.MazuiFS,M.asa,m.Ssys,m.mzys,P.ZXHS,h.Ssjb,h.Fxpg,h.shengfen,ot.Remarks
 FROM Adims_PACU as P left join Adims_mzjld AS M on P.mzjldid=m.id  left join Adims_OperSchedule  
AS OT on M.patid = OT.patid left join Adims_BeforeVisit_HS as H on h.patid=OT.patid
where P.Odate between '" + date1 + "'and'" + date2 + "' and P.rssj>'1990-01-01' and P.cssj>'1990-01-01' and ot.odate>=1";

            return dBConn.GetDataTable(string.Format(select1));
        }
        public int UpdatePACU_Otime(string time, string mzid)
        {
            string sql = "update Adims_PACU set otime='" + time + "' where mzjldid='" + mzid + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int UpdatePACU_Weight(string Weight, string zid, string pid)
        {
            string sql = "update Adims_OperSchedule set patweight='" + Weight + "' where patzhuyuanID='" + zid + "' and patid='" + pid + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int UpdatePACU(List<string> mzdList)
        {
            string MZJLD_UPDATE1 = @"UPDATE [Adims_PACU]
   SET  [JCSJJG] ='{0}',[mzzl] = '{1}',[Sztsqk] ='{2}',[Tiwei] = '{3}',
 [qt] = '{4}' ,[Qdtq] ='{5}',[Cql] = '{6}',[Hxpl] ='{7}' ,[jcjl] ='{8}',
[Mfz] = '{9}' ,[Yszt] = '{10}',[Pf] ='{11}' ,[Tszs] ='{12}' ,[shMzys] = '{13}' ,
[Shssys] = '{14}',[Lhfsqk] ='{15}',[MZYS] = '{16}',[ZXHS] = '{17}' ,
[patid] = '{18}',[Odate] = '{20}'    
 WHERE mzjldid='{19}'";
            string SQL = string.Format(MZJLD_UPDATE1, mzdList.ToArray());
            return dBConn.ExecuteNonQuery(SQL);
        }
   
        /// <summary>
        /// 根据麻醉ID获取PACU
        /// </summary>
        /// <param name="mzid"></param>
        /// <returns></returns>
        public DataTable GetPACU_ByMzjldId(int mzid) /// 获取PACU检测点
        {
            string sql = "SELECT * FROM Adims_PACU WITH (NOLOCK) WHERE mzjldid='" + mzid + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 查询当前病人最大事件检测记录点
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>

    
        public int CopyMzjldToPacu(DateTime dtime, int mzid, int count)
        {
            int result = 0;
            string query = "select NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP from Adims_MonitorRecord "
            + " where mzjldid='" + mzid + "' order by CreateTime desc ";
            DataTable dtMZJLD = dBConn.GetDataTable(query);
            foreach (DataRow dr in dtMZJLD.Rows)
            {
                while (result < count)
                {
                    query = "insert into Adims_Pacu_Point(mzjldid,RecordTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP) "
                  + "values('" + mzid + "','" + dtime + "','" + dr["NIBPS"] + "','" + dr["NIBPD"] + "',"
                  + "'" + dr["NIBPM"] + "','" + dr["RRC"] + "','" + dr["HR"] + "','" + dr["Pulse"] + "', '" + dr["SpO2"] + "',"
                  + "'" + dr["ETCO2"] + "', '" + dr["TEMP"] + "')";
                    dBConn.ExecuteNonQuery(query);
                    result++;
                    dtime = dtime.AddMinutes(5);
                }
            }

            return result;
        }
        /// <summary>
        /// 插入PACU记录
        /// </summary>
        /// <param name="mzid"></param>
        /// <param name="otime"></param>
        /// <param name="vtime"></param>
        /// <returns></returns>
        public int InsertPACU(int mzid,DateTime otime,DateTime vtime) /// 
        {
            string sql = "INSERT INTO Adims_PACU(mzjldid,otime,savetime,jcsjjg) VALUES ('" + mzid + "','" + otime + "','" + vtime + "','5')";
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 修改PACU麻醉医生和执行护士
        /// </summary>
        /// <param name="mzys"></param>
        /// <param name="zxhs"></param>
        /// <param name="mzid"></param>
        /// <returns></returns>
        public int UpdatePACU_MzysAndZxhs(string mzys,string zxhs,int mzid)
        {
            string SQL11 = "UPDATE Adims_PACU  SET mzys='"+mzys+"',zxhs='"+zxhs+"'  WHERE mzjldid ='"+mzid+"' ";
            return dBConn.ExecuteNonQuery(SQL11);
        }
        public int AddPACU_DATA(string MZID, string time)  //病人恢复信息保存
        {
            string _INSERT = "INSERT INTO Adims_PACU_DATA(mzjldid,timeShort) values('" + MZID + "','" + time + "')";

            return dBConn.ExecuteNonQuery(_INSERT);
        }


        public int UpdatePACU_DATA(string id, DateTime shorttime)  //病人恢复信息保存
        {
            string _INSERT = "Update  Adims_PACU_DATA set timeShort='" + shorttime + "' where id='" + id + "'";
            return dBConn.ExecuteNonQuery(_INSERT);
        }
        /// <summary>
        /// 修改PACU检测事件间隔
        /// </summary>
        /// <param name="mzjld"></param>
        /// <param name="jcsjjg"></param>
        /// <returns></returns>
        public int UpdatePACU_jcsjjg(int  mzjld,string jcsjjg)
        {
            string SQL11 = "UPDATE Adims_PACU  SET jcsjjg='" + jcsjjg + "'  WHERE mzjldid ='" + mzjld + "' ";
            return dBConn.ExecuteNonQuery(SQL11);
        }
        /// <summary>
        /// 修改PACU麻醉医生
        /// </summary>
        /// <param name="mzjld"></param>
        /// <param name="mzys"></param>
        /// <returns></returns>
        public int UpdatePACU_mzys(int mzjld, string mzys)
        {
            string SQL11 = "UPDATE Adims_PACU  SET mzys='" + mzys + "'  WHERE mzjldid ='" + mzjld + "' ";
            return dBConn.ExecuteNonQuery(SQL11);
        }
        /// <summary>
        /// 修改PACU执行护士
        /// </summary>
        /// <param name="mzjld"></param>
        /// <param name="zxhs"></param>
        /// <returns></returns>
        public int UpdatePACU_zxhs(int mzjld, string zxhs)
        {
            string SQL11 = "UPDATE Adims_PACU  SET zxhs='" + zxhs + "'  WHERE mzjldid ='" + mzjld + "' ";
            return dBConn.ExecuteNonQuery(SQL11);
        }
        /// <summary>
        /// 获取PACU监护项目值
        /// </summary>
        /// <param name="mzid"></param>
        /// <returns></returns>
        public DataTable GetjhxmPACU(int mzid) /// 
        {
            string sql = "SELECT * FROM Adims_jhxm_PACU WITH (NOLOCK) WHERE mzjldid='" + mzid + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 获取PACU气体
        /// </summary>
        /// <param name="mzid"></param>
        /// <returns></returns>
        public DataTable GetqtPACU(int mzid) /// 
        {
            string sql = "SELECT * FROM Adims_PACU_qtUse WITH (NOLOCK) WHERE mzjldid='" + mzid + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 获取PACU输血
        /// </summary>
        /// <param name="mzid"></param>
        /// <returns></returns>
        public DataTable GetsxPACU(int mzid) /// 
        {
            string sql = "SELECT * FROM Adims_shuxueUSE_PACU WITH (NOLOCK) WHERE mzjldid='" + mzid + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 获取PACU输液
        /// </summary>
        /// <param name="mzid"></param>
        /// <returns></returns>
        public DataTable GetsyPACU(int mzid) /// 
        {
            string sql = "SELECT * FROM Adims_shuyeUSE_PACU WITH (NOLOCK) WHERE mzjldid='" + mzid + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 获取PACU--1出血，2出尿
        /// </summary>
        /// <param name="mzid"></param>
        /// <param name="lx"></param>
        /// <returns></returns>
        public DataTable GetclcxPACU(int mzid,int lx) 
        {
            string sql = "SELECT * FROM Adims_PACU_cl WITH (NOLOCK) WHERE mzjldid='" + mzid + "'and lx='"+lx+"'";
            return dBConn.GetDataTable(sql);
        }
       /// <summary>
       /// 删除PACU事件
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public int deletePACUsj(int id)
        {
            string delete = "delete from Adims_PACU_shijian where id='" + id + "' ";
            return dBConn.ExecuteNonQuery(string.Format(delete));
        }
        /// <summary>
        /// 获取PACU事件
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <returns></returns>
        public DataTable GetPACUsj(int mzjldid)
        {
            string select = "select * from Adims_PACU_shijian where mzjldid='" + mzjldid + "'ORDER BY TIME ASC";
            return dBConn.GetDataTable(string.Format(select));
        }
        /// <summary>
        /// 增加PACU用药
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public int addPACUyy(int mzjldid, adims_MODEL.tsyy ts)
        {
            return dBConn.ExecuteNonQuery("INSERT INTO Adims_PACU_yyUse(mzjldid,yyname,yl,dw,yyfs,yytime) "
                + "values('" + mzjldid + "','" + ts.Name + "','" + ts.Yl + "','" + ts.Dw + "','" + ts.Yyfs + "','" + ts.D + "')");
        }
        /// <summary>
        /// 删除用药
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int deletePACUyy(int id)
        {
            string delete = "delete from Adims_PACU_yyUse where id='" + id + "' ";
            return dBConn.ExecuteNonQuery(string.Format(delete));
        }
        /// <summary>
        /// 获取用药
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <returns></returns>
        public DataTable GetPACUyy(int mzjldid)
        {
            string select = "select * from Adims_PACU_yyUse where mzjldid='" + mzjldid + "' ORDER BY YYTIME ASC";
            return dBConn.GetDataTable(string.Format(select));
        }

    }
        
}
