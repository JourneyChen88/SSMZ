using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL.Flows
{
    public class OperScheduleDal
    {
        private DBConn dBConn = new DBConn();

        /// <summary>
        /// 更新排班
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdatePAIBAN(Dictionary<string, string> dictionary)
        {
            string update = " UPDATE Adims_OperSchedule WITH (ROWLOCK) SET "
          + "PatZhuYuanID = '{1}',Patname = '{2}',Patage = '{3}',Patsex = '{4}',Patdpm = '{5}',"
          + "Patbedno = '{6}',PatWeight = '{7}',PatHeight = '{8}',Oname = '{9}',Pattmd = '{10}',"
          + "Oroom = '{11}',Second = '{12}',Amethod = '{13}',AP1 = '{14}',ON1 = '{15}',SN1 = '{16}'"
          + ",Odate = '{17}',OS = '{18}',Ostate = '1' where PatID = '{0}'";
            return dBConn.ExecuteNonQuery(string.Format(update, dictionary.Values.ToArray()));
        }

        public int InsertPAIBAN(Dictionary<string, string> dictionary)
        {
            string SQL_INSERT = "INSERT INTO Adims_OperSchedule(PatID,PatZhuYuanID,Patname ,Patage,Patsex "
                                + ",Patdpm  ,Patbedno ,PatWeight ,PatHeight,Oname,Pattmd,Oroom,Second,Amethod,AP1"
                                + ",ON1 ,SN1,Odate,OS,isjizhen,StartTime,remarks,GR,tiwei,PatNation,Ostate) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',"
                                + "'{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','1')";

            string INSERT = string.Format(SQL_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(INSERT);
        }
        public DataTable GetPaiBanSort(string dt, string where)
        {
            string SQL_SELECT = "SELECT oroom,second,StartTime,PatZhuYuanID,patdpm,patname,"
                + "patsex,patage,patbedno,patNation,pattmd,oname,Amethod,SSmzfs,os,os1,os2,on1,on2,sn1,sn2,ap1,ap2,ap3,tiwei,bx,gr,remarks,shengfen,id"
                + "  from Adims_OperSchedule WITH (NOLOCK) WHERE CONVERT(varchar, Odate , 23 ) = '" + dt + "' ";
            string sql = string.Format(SQL_SELECT + where);
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetPaiBanSort(int ostate, string dt, string where)
        {
            string SQL_SELECT = "SELECT oroom,second,StartTime,PatZhuYuanID,patdpm,patname,"
                + "patsex,patage,patbedno,patNation,pattmd,oname,Amethod,SSmzfs,os,os1,os2,on1,on2,sn1,sn2,ap1,ap2,ap3,tiwei,bx,gr,remarks,shengfen,id"
                + "  from Adims_OperSchedule WITH (NOLOCK) WHERE CONVERT(varchar, Odate , 23 ) = '" + dt + "' AND ostate = '" + ostate + "' ";
            string sql = string.Format(SQL_SELECT + where);
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 修改排班体位
        /// </summary>
        /// <param name="tw"></param>
        /// <param name="patid"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public int UpdatePabanTiwei(string tiwei, string patid)
        {
            string sql = "update Adims_OperSchedule set tiwei='" + tiwei + "' where PatZhuYuanID='" + patid + "' ";
            return dBConn.ExecuteNonQuery(sql);
        }
        public DataTable GetPaibanByOroomAndTaici(string oroom, string second, DateTime dt, string patid)
        {
            string sql = "SELECT * from Adims_OperSchedule  where oroom='" + oroom + "' and second='" + second + "'and Odate ='" + dt + "'and PatZhuYuanID!='" + patid + "'";
            return dBConn.GetDataTable(sql);
        }

        /// <summary>
        /// 修改排班
        /// </summary>
        /// <param name="heigth"></param>
        /// <param name="weith"></param>
        /// <param name="patid"></param>
        /// <returns></returns>
        public int UpdatePaiban(string heigth, string weith, string patid)
        {
            string sql = "update Adims_OperSchedule set PatHeight='" + heigth + "',PatWeight='" + weith + "' where patID='" + patid + "'";
            return dBConn.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 通过时间查询手术排班病人列表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable GetPaibanByOdate(string dtime)
        {
            string sqls = @" select patzhuyuanid 住院号,patbedno 床号,patname 病人,patdpm 科室,
                                        patage 年龄,patsex 性别,oname 手术名称,pattmd 术前诊断,amethod 麻醉方式,
                                        PatID 病人编号 from Adims_OperSchedule where CONVERT(varchar, Odate , 23 ) = '{0}'";
            string exc = string.Format(sqls, dtime);

            return dBConn.GetDataTable(exc);
        }

        public DataTable GetPaibanByPatId(string patid)
        {
            string sqls = "select * from Adims_OperSchedule where patid = '{0}'";
            return dBConn.GetDataTable(string.Format(sqls, patid));
        }
        /// <summary>
        /// 根据id查询排班信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetPaibanByID(string id)
        {
            string sql = "SELECT * from Adims_OperSchedule  where id='" + id + "'";
            return dBConn.GetDataTable(sql);
        }

        public DataTable GetIsPaiban(int ostate, string dt)
        {
            string sqlWhere = "CONVERT(varchar, Odate , 23 ) = '" + dt + "'";
            if (ostate == 0)
            {
                sqlWhere += " AND ostate = '" + ostate + "'";
            }
            else
                sqlWhere += " AND ostate >= '" + ostate + "'";


            return dBConn.GetDataTable(string.Format(_SELECT, sqlWhere));
        }
        public DataTable GetPaibanByJizhen(string dt)
        {
            string sqlWhere = "CONVERT(varchar, Odate , 23 ) = '" + dt + "'";
            sqlWhere += " AND isjizhen = '1'";
            return dBConn.GetDataTable(string.Format(_SELECT, sqlWhere));
        }
        public DataTable GetPaibanByOdateAndOstate(string dt, int ostate)
        {
            string sql = "SELECT Oroom 手术间名称,patdpm 科室,PatZhuYuanID 住院号,patbedno 床位号, Patname 病人姓名,patsex 性别, patage 年龄,pattmd 主要诊断,"
                + "oname 手术名称,PatId 病人编号  from Adims_OperSchedule WITH (NOLOCK) where "
            + "CONVERT(varchar, Odate , 23 ) = '" + dt + "' and ostate='" + ostate + "'";
            return dBConn.GetDataTable(sql);
        }
        private static readonly string _SELECT = "SELECT oroom,second,StartTime,PatZhuYuanID,patdpm,patname,"
              + "patsex,patage,patbedno,patNation,pattmd,oname,Amethod,SSmzfs,os,os1,os2,on1,on2,sn1,sn2,ap1,ap2,ap3,tiwei,bx,gr,remarks,shengfen,patid,id"
              + "  from Adims_OperSchedule WITH (NOLOCK) WHERE {0} ORDER BY oroom";
        //private static readonly string _SELECT = "SELECT oroom,second,StartTime,PatZhuYuanID,patdpm,patname,"
        //  + "patsex,patage,patbedno,patNation,pattmd,oname,os,on1,on2,sn1,sn2,ap1,ap2,ap3,tiwei,bx,gr,remarks,id"
        //  + "  from Adims_OperSchedule WITH (NOLOCK) WHERE {0} ORDER BY oroom";
        public DataTable GetByRoomAndOdate(string oroom, string dt)
        {
            string sqlWhere = "CONVERT(varchar,Odate,23) = '" + dt + "'";
            if (oroom != "全部手术间")
                sqlWhere += " AND oroom = '" + oroom + "'";
            return dBConn.GetDataTable(string.Format(_SELECT, sqlWhere));
        }
        /// <summary>
        /// 添加排班
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertPaiban(List<string> list1)
        {
            string SQL_PAIBAN = @"INSERT INTO Adims_OperSchedule(
PatID,PatZhuYuanID,CardID,Patname,Patage,Patsex,patNation,
Patbedno,Patdpm,Pattmd ,PatHeight,PatWeight,PatBloodType,
Oname ,odate,OS,os1,os2,os3,os4,Amethod,BX,Tiwei,GR,remarks,
StartTime,ostate,isjizhen,Ocode,SSLB,SQSJ,
sn1,sn2,on1,on2,ap1,ap2,ap3,ms) VALUES 
('{0}','{1}','{2}','{3}','{4}','{5}','{6}',
'{7}','{8}','{9}','{10}','{11}','{12}',
'{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}',
'{25}','{26}','{27}','{28}','{29}','{30}',
'','','','','','','','{31}')";

            string INSERT = string.Format(SQL_PAIBAN, list1.ToArray());
            return dBConn.ExecuteNonQuery(INSERT);
        }

        public int UpdatePaiban(List<string> list1)
        {
            string SQL_PAIBAN = @"update Adims_OperSchedule set 
PatZhuYuanID='{1}',
CardID='{2}',
Patname='{3}'
,Patage='{4}'
,Patsex='{5}'
,patNation='{6}'
,Patbedno='{7}'
,Patdpm='{8}'
,Pattmd='{9}'
,PatHeight='{10}'
,PatWeight='{11}'
,PatBloodType='{12}'
,Oname ='{13}'
,odate='{14}'
,OS='{15}'
,os1='{16}'
,os2='{17}'
,os3='{18}'
,os4='{19}'
,Amethod='{20}'
,BX='{21}'
,Tiwei='{22}'
,GR='{23}'
,remarks='{24}'
,StartTime='{25}'
,ostate='{26}'
,isjizhen='{27}'
,Ocode='{28}'
,SSLB='{29}'
,SQSJ='{30}'
,sn1=''
,sn2='',on1='',on2'=',
ap1'=',ap2'=',ap3'=' where PatID='{0}'";

            string INSERT = string.Format(SQL_PAIBAN, list1.ToArray());
            return dBConn.ExecuteNonQuery(INSERT);
        }
        /// <summary>
        /// 更新排班信息
        /// </summary>
        /// <param name="patID"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int UpdatePaiban(string id, string DateType, string value, string ostate, string Odate)
        {
            string sql = "UPDATE Adims_OperSchedule WITH (ROWLOCK) SET " + DateType + " = '" + value + "',ostate='" + ostate + "' WHERE id = '" + id + "' and convert(varchar, Odate,23)='" + Odate + "'";
            return dBConn.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 修改为急诊
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdatePaibanJizhen(string id, int IsJizhen)
        {
            string sql = "update Adims_OperSchedule set isjizhen='" + IsJizhen + "' where id ='" + id + "'";
            return dBConn.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeletePaiban(string id)
        {
            return dBConn.ExecuteNonQuery("delete Adims_OperSchedule where id ='" + id + "'");
        }

    }
}
