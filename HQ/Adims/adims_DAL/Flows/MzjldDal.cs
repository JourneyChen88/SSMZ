using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL.Flows
{
    public class MzjldDal
    {
        private DBConn dBConn = new DBConn();


        /// <summary>
        /// 添加麻醉记录单
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="otime"></param>
        /// <returns></returns>
        public int AddMzjld(string patid, DateTime otime,int isZoom=1,int jcsjjg = 5)
        {
            return dBConn.ExecuteNonQuery("INSERT INTO Adims_Mzjld(patid,otime,IsZoom,jcsjjg) values('" + patid + "','" + otime + "','" + isZoom + "','" + jcsjjg + "')");
        }
        /// <summary>
        /// 通过时间查询手术病人信息
        /// </summary>
        /// <param name="Otime"></param>
        /// <returns></returns>
        //public DataTable GetMzjldByOtime(string Otime)
        //{
        //    string sql = "select  A.id as mzjldid,B.patid ,B.patname ,B.patzhuyuanid ,"
        //            + "B.Patbedno ,B.patsex ,B.patage  from Adims_Mzjld as A LEFT join "
        //            + "Adims_OperSchedule AS B ON A.PATID=B.PATID where CONVERT(varchar, A.Otime , 23 ) = '" + Otime + "'";
        //    return dBConn.GetDataTable(sql);
        //}
        public DataTable GetMzjldByOtime(string Otime)
        {
            string sql = @"select  A.id as 麻醉编号,B.patid 病人编号,B.patname 姓名,B.patzhuyuanid 住院号,
                B.Patbedno 床号,B.patsex 性别,B.patage 年龄 ,B.Oroom 手术间名称 from Adims_Mzjld as A  
                inner join    Adims_OperSchedule AS B ON A.PATID=B.PATID where CONVERT(varchar, A.Otime , 23 ) = '" + Otime + "'";
            return dBConn.GetDataTable(sql);
        }

        public DataTable GetMzjldByPatId(string patid)
        {
            string sql = "select * from Adims_Mzjld where  patid='" + patid + "' ";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetMzjldByMzjldId(string mzjldid)
        {
            string sql = "select * from Adims_Mzjld where  id='" + mzjldid + "' ";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetMzjldByMzjldId(int mzjldid)
        {
            string sql = "select * from Adims_Mzjld where  id='" + mzjldid + "' ";
            return dBConn.GetDataTable(sql);
        }
        public int DeleteMzjldById(int id)
        {
            string sql = " delete from  Adims_mzjld where id=" + id;
            return dBConn.ExecuteNonQuery(sql);
        }
    }
}
