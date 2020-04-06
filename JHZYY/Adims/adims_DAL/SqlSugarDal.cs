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
        public List<QueryMzjldDto> GetOperByDate(DateTime date)
        {
            var paibanList = db.Queryable<OTypesetting>().Where(a => a.Odate >= date && a.Odate < date.AddDays(1)).ToList();
            var list = (from a in paibanList
                        join b in db.Queryable<Adims_Mzjld>().ToList() on a.PatZhuYuanID equals b.Patid
                        select new QueryMzjldDto
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
            var list = db.Queryable<OTypesetting, Adims_Mzjld>((a, b) => new object[] {
                    JoinType.Inner,a.PatZhuYuanID==b.Patid})
                   .Where((a, b) => a.PatZhuYuanID == patzhuyuanId)
                   .Select((a, b) => new MzjldDto
                   {
                       ApplyId=a.ApplyId,
                       Patid = b.Patid,
                       sskssj = b.Sskssj,
                       ssjssj = b.Ssjssj,
                       Otime = b.Otime,
                       ssss = b.Ssss,
                       Oroom = a.Oroom,
                       Second = a.Second,
                       Ocode = a.Ocode,
                       Oname = a.Oname,
                       PidInfo = a.PidInfo,
                       Pv1Info = a.Pv1Info,
                       SSDJ = a.SSDJ,
                       Amethod = a.Amethod,
                       AP1 = a.AP1,
                       AP2 = a.AP2,
                       AP3 = a.AP3,
                       AP1No = a.AP1No,
                       AP2No = a.AP2No,
                       AP3No = a.AP3No,
                       OS = a.OS,
                       OS1 = a.OS1,
                       OsNo=a.OsNo,
                       Os1No = a.Os1No,
                       Os2No = a.Os2No,
                       Os3No = a.Os3No,
                       OS2 = a.OS2,
                       OS3 = a.OS3,
                       ON1 = a.ON1,
                       ON2 = a.ON2,
                       SN1 = a.SN1,
                       SN2 = a.SN2,
                       ON1No = a.ON1No,
                       ON2No = a.ON2No,
                       SN1No = a.SN1No,
                       SN2No = a.SN2No,
                       asa = b.ASA,
                       asae = b.ASAE,
                       Mzjldid = b.Id,
                       Patname = a.Patname,
                       Odate = a.Odate,
                       Ostate = a.Ostate
                      
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
        public OTypesetting GetPaiban(int id)
        {
            return db.Queryable<OTypesetting>().Where(a => a.ID == id).First();
        }
        public OTypesetting GetPaibanByPatZhuYuanID(string PatZhuYuanID)
        {
            return db.Queryable<OTypesetting>().Where(a => a.PatZhuYuanID == PatZhuYuanID).First();
        }
        public int UpdatePaiban(OTypesetting model)
        {
            return db.Updateable<OTypesetting>(model).ExecuteCommand();
            
        }
        public int UpdatePaibanConfig(int id)
        {
            var entity = db.Queryable<OTypesetting>().Where(a => a.ID == id).First();
            entity.Ostate = 1;
            return db.Updateable(entity).UpdateColumns(it => new { it.Ostate }).ExecuteCommand();
        }
        public List<PaibanDto> GetPaiBanByOdate(DateTime date)
        {
            var list = db.Queryable<OTypesetting>()
                   .Where(a => a.Odate >= date && a.Odate < date.AddDays(1))
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
