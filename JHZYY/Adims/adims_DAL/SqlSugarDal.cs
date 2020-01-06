using adims_MODEL;
using adims_MODEL.Dtos;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL
{
    public class SqlSugarDal
    {
        private static readonly string connStr = ConfigurationManager.AppSettings["ConnectionString"];
        SqlSugarClient db = new SqlSugarClient(
                                new ConnectionConfig()
                                {
                                    ConnectionString = connStr,
                                    DbType = SqlSugar.DbType.SqlServer,//设置数据库类型
                                    IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                                    InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
                                });

        /// <summary>
        /// 根据手术登记倒序查询
        /// </summary>
        /// <param name="mzid"></param>
        /// <returns></returns>
        public List<QueryMzjldDto> GetOperByDate(string date)
        {
            var list = db.Queryable<OTypesetting, Adims_Mzjld>((st, sc) => new object[] {
                    JoinType.Inner,sc.Patid==st.PatZhuYuanID})
                    .Where((st, sc) => st.Odate.ToString("yyyy-MM-dd") == date)
                    .Select((a, b) => new QueryMzjldDto
                    {
                        Patid = b.Patid,
                        Mzjldid = b.Id,
                        Patname = a.Patname,
                        Odate = a.Odate,
                        Ostate = a.Ostate,
                        AP1 = a.AP1
                    }).OrderBy(a => a.Odate).ToList();
            //            string sql = $@"
            //SELECT a.patid,a.id mzjldid,b.Patname,b.Odate,b.Ostate,b.AP1,c.UserNo FROM Adims_Mzjld a
            //INNER JOIN Adims_OTypesetting b ON a.patid=b.PatID
            //LEFT JOIN Adims_User c ON b.AP1=c.user_name
            //WHERE Convert(VARCHAR(10),b.Odate,23)= '{date}' ";

            return list;
        }
        //public DataTable GetMzffID(string MZFF)
        //{
        //    string sql = "SELECT Mzff_No  FROM [mazuifangan] where [name]='" + MZFF + "'";
        //    return dBConn.GetDataTable(sql);
        //}
        public List<Adims_User> GetUser()
        {
            return db.Queryable<Adims_User>().ToList();
            // string sql = "select uid as UserNo from Adims_User where user_name='" + userName + "'";

        }
        //public DataTable GetOperNo(string OperName)
        //{
        //    string sql = "SELECT *  FROM OperDic where OperName='" + OperName + "'";
        //    return dBConn.GetDataTable(sql);
        //}



        public MzjldDto GetPaibanAndMzjld(string patzhuyuanId)
        {
            var list = db.Queryable<OTypesetting, Adims_Mzjld>((st, sc) => new object[] {
                    JoinType.Inner,sc.Patid==st.PatZhuYuanID})
                   .Where((st, sc) => st.PatZhuYuanID == patzhuyuanId)
                   .Select((a, b) => new MzjldDto
                   {
                       Patid = b.Patid,
                       sskssj = b.Sskssj,
                       ssjssj = b.Ssjssj,
                       Otime = b.Otime,
                       ssss = b.Ssss,
                       Oroom = a.Oroom,
                       Second = a.Second,
                       OperNo = a.OperNo,
                       Oname = a.Oname,
                       PidInfo = a.PidInfo,
                       Pv1Info = a.Pv1Info,
                       SSDJ = a.SSDJ,
                       Amethod = a.Amethod,
                       AP2 = a.AP2,
                       AP3 = a.AP3,
                       OS = a.OS,
                       OS1 = a.OS1,
                       OS2 = a.OS2,
                       OS3 = a.OS3,
                       ON1 = a.ON1,
                       ON2 = a.ON2,
                       SN1 = a.SN1,
                       SN2 = a.SN2,
                       asa = b.ASA,
                       asae = b.ASAE,
                       Mzjldid = b.Id,
                       Patname = a.Patname,
                       Odate = a.Odate,
                       Ostate = a.Ostate,
                       AP1 = a.AP1
                   }).OrderBy(a => a.Odate).First();
            //            string sql =$@"SELECT o.PatID,o.Amethod,m.sskssj,m.ssjssj,m.Otime,m.ssss,o.Oroom,
            //o.Second,o.OperNo,o.Oname,o.PidInfo,o.Pv1Info, SSDJ,Amethod,
            //AP1,AP2,AP3,OS,ON1,ON2,
            //SN1,SN2,m.asa,m.asae,Ostate,Odate
            //FROM Adims_OTypesetting as O
            //inner JOIN Adims_Mzjld as M ON o.PatZhuYuanID = m.patid
            //WHERE o.PatZhuYuanID='{patzhuyuanId}";
            return list;
        }

        /// <summary>
        /// 根据住院号获取排班信息
        /// </summary>
        /// <param name="zhuyuanid"></param>
        /// <returns></returns>
        public OTypesetting GetPaiban(string zhuyuanid)
        {
            return db.Queryable<OTypesetting>().Where(a => a.PatZhuYuanID == zhuyuanid).First();
        }
        public int UpdatePaibanConfig(string PatZhuYuanId)
        {
            var entity = db.Queryable<OTypesetting>().Where(a => a.PatZhuYuanID == PatZhuYuanId).First();
            entity.Ostate = 1;
            return db.Updateable(entity).UpdateColumns(it => new { it.Ostate }).ExecuteCommand();
        }
        public List<PaibanDto> GetPaiBanByOdate(string dtime)
        {
            var list = db.Queryable<OTypesetting>()
                   .Where(st => st.Odate.ToString("yyyy-MM-dd") == dtime)
                   .Select(a => new PaibanDto
                   {
                       ID = a.ID,
                       PatID = a.PatID,
                       PatZhuYuanID = a.PatZhuYuanID,
                       CardID = a.CardID,
                       Patname = a.Patname,
                       Patsex = a.Patsex,
                       Patage = a.Patage,
                       PatNation = a.PatNation,
                       Patbedno = a.Patbedno,
                       Patdpm = a.Patdpm,
                       Pattmd = a.Pattmd,
                       Oname = a.Oname,
                       Amethod = a.Amethod,
                       ApplyDate = a.ApplyDate,
                       Odate = a.Odate,
                       Second = a.Second,
                       Oroom = a.Oroom
                   }).OrderBy(a => a.Odate).ToList();


            return list;
        }
    }
}
