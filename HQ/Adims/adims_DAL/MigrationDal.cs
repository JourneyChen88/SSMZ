using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace adims_DAL
{
    public class MigrationDal
    {
        private DBConn dBConn = new DBConn();     
        HisDB_Help HisHelp = new HisDB_Help();

        /// <summary>
        /// 获取HIS所有排班信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetPaibanByHisView()
        {
            //string sql = @"select patID ,patZhuYuanid from V_Operation_Interface";
            string sql = @"select op_record_ID,ZhuYuanNo,Odate,patName,CardID,patAge,
        patSex,patNation,PatHeight,PatWeight,PatBloodType,
        BedNo,Patdpm,Pattmd,Oname,OS1,OS2,OS3,OS4,OS5,
        Amethod,BX, Tiwei, GR, BX,Ocode,SSLB,SSDJ,Remarks,
        StartTime,SQSJ from V_Operation_Interface";
            return HisHelp.GetDataTable(sql);
        }
        /// <summary>
        /// 获取HIS排班固定日期排班信息
        /// </summary>
        /// <param name="dtime"></param>
        /// <returns></returns>
        public DataTable GetPaibanByHisViewOdate(string dtime)
        {
            string sql = "select patID ,ZhuYuanNo,patName,CardID,patAge,patSex,patNation,PatHeight,PatWeight,PatBloodType,BedNo,Patdpm,Pattmd,Oname,Odate,OS1,OS2,OS3,OS4,OS5,Amethod,BX, Tiwei, GR, BX,Ocode,SSLB,SSDJ,Remarks,StartTime,SQSJ"
            + " from V_Operation_Interface where Convert(varchar,Odate,23)='" + dtime + "'";
            return HisHelp.GetDataTable(sql);
        }
        /// <summary>
        /// 获取排班
        /// </summary>
        /// <returns></returns>
        public DataTable GetPaiban()
        {
            string sql = "SELECT patid,PatZhuYuanID,odate from Adims_OperSchedule ";
            return dBConn.GetDataTable(sql);
        }
   
        public int UpdatePaiban(string patdid, string PatZhuYuanID, DateTime odate)
        {

            string sql = "update Adims_OperSchedule set patID='{0}' where PatZhuYuanID='{1}' and (odate Between '{2}' and  '{3}')";
            sql = string.Format(sql, patdid, PatZhuYuanID, odate.AddDays(-1), odate.AddDays(1));
            return dBConn.ExecuteNonQuery(sql);
        }
        public void DoSql()
        {
            string sql = @"EXEC  sp_rename 'Adims_BeforeShfs_YS' ,'Adims_AfterVisit_YS' ;
EXEC  sp_rename 'Adims_Mzzj_CJ' ,'Adims_AnesthesiaSummary' ;
EXEC  sp_rename 'Adims_SHZT_YS' ,'Adims_AfterAnalgesia' ;
EXEC  sp_rename 'Adims_SHZTGCZB_YS' ,'Adims_AfterAnalgesiaDetail' ;
EXEC  sp_rename 'Adims_OTypesetting' ,'Adims_OperSchedule' ;
EXEC  sp_rename 'Adims_SSSZR' ,'Adims_OperImplant' ;

EXEC  sp_rename 'Adims_AfterVisit_YS.ZYNumber' ,'PatId' , 'COLUMN' ;
EXEC  sp_rename 'Adims_AfterVisit_YS.PayTime' ,'Odate' , 'COLUMN';
EXEC  sp_rename 'Adims_BeforeVisit_HS.ZYNumber' ,'PatId' , 'COLUMN' ;
EXEC  sp_rename 'Adims_BeforeVisit_YS.PayTime' ,'Odate' , 'COLUMN' ;
EXEC  sp_rename 'Adims_BeforeVisit_YS.ZYNumber' ,'PatId' , 'COLUMN' ;
EXEC  sp_rename 'Adims_MZZQTYS_YS.ZYNumber' ,'PatId' , 'COLUMN' ;
EXEC  sp_rename 'Adims_AfterAnalgesia.ZYNumber' ,'PatId' , 'COLUMN'; 
EXEC  sp_rename 'Adims_ZTZLZQTYS_YS.ZYNumber' ,'PatId' , 'COLUMN' ;
EXEC  sp_rename 'Adims_OperImplant.ZYNumber' ,'PatId' , 'COLUMN' ;
EXEC  sp_rename 'Adims_BeforeVisit_HS.sstiem' ,'Odate' , 'COLUMN' ;
EXEC  sp_rename 'Adims_AnesthesiaSummary.Paytime' ,'Odate' , 'COLUMN' ;
EXEC  sp_rename 'Adims_MZZQTYS_YS.Paytime' ,'Odate' , 'COLUMN' ;
EXEC  sp_rename 'Adims_NurseRecord_HQ.Paytime' ,'Odate' , 'COLUMN' ;
EXEC  sp_rename 'Adims_PACU.Paytime' ,'Odate' , 'COLUMN' ;
EXEC  sp_rename 'Adims_SHZT_YS.Paytime' ,'Odate' , 'COLUMN' ;
EXEC  sp_rename 'Adims_OperImplant.Paytime' ,'Odate' , 'COLUMN' ;
EXEC  sp_rename 'Adims_AfterVisit_YS.Mzjld' ,'MzjldID' , 'COLUMN'; 
EXEC  sp_rename 'Adims_AfterAnalgesiaDetail.Paytime' ,'VisitTime' , 'COLUMN' 
EXEC  sp_rename 'Adims_ZTZLZQTYS_YS.VisitTime' ,'Odate' , 'COLUMN' ;";
            dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 修改对应表格信息
        /// </summary>
        /// <param name="tableName">表名字</param>
        /// <param name="patdid">病人ID(手术申请ID)</param>
        /// <param name="PatZhuYuanID">住院号</param>
        /// <param name="odate">手术预计时间</param>
        /// <returns></returns>
        public int UpdateTable(string tableName, string patdid, string PatZhuYuanID, DateTime odate)
        {
            string sql = "update {0} set patID='{1}' where PatZhuYuanID='{2}' and (odate Between '{3}' and  '{4}')";
            sql = string.Format(sql, tableName, patdid, PatZhuYuanID, odate.AddDays(-1), odate.AddDays(1));
            return dBConn.ExecuteNonQuery(sql);
        }
        public int UpdateMzjldTable(string tableName, string patdid, string PatZhuYuanID, DateTime odate)
        {
            string sql = "update {0} set patID='{1}' where PatZhuYuanID='{2}' and (otime Between '{3}' and  '{4}')";
            sql = string.Format(sql, tableName, patdid, PatZhuYuanID, odate.AddDays(-1), odate.AddDays(2));
            //SaveLog(sql);
            return dBConn.ExecuteNonQuery(sql);
        }
        private void SaveLog(string text)
        {
            //string text = "abcdefg\r\n";
            int TextLength = text.Length;
            if (TextLength < 2000)
            {
                if (!File.Exists("c:\\UpdateMzjld.txt"))
                {
                    File.Create("c:\\UpdateMzjld.txt");
                }
                FileStream fs = new FileStream("c:\\UpdateMzjld.txt", FileMode.Append);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                sw.Write(text + "\r\n");
                sw.Close();
                fs.Close();
            }
        }
        public int UpdateMzjldTable(string tableName, DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            string sql = "update {0} set patID='{1}' where patid='{2}' and (otime Between '{3}' and  '{4}') ;";
            foreach (DataRow dr in dt.Rows)
            {
                DateTime odate = Convert.ToDateTime(dr["odate"].ToString());
                sql = string.Format(sql, tableName, dr["patid"], dr["PatZhuYuanID"], odate.AddDays(-1), odate.AddDays(1));
                sb.Append(sql);
            }

            return dBConn.ExecuteNonQuery(sb.ToString());
        }
        public int UpdateBeforeVisit_HS(string patdid, string PatZhuYuanID, DateTime odate)
        {
            string sql = "update Adims_BeforeVisit_HS set patID='" + patdid + "' where patid='" + PatZhuYuanID + "' and (odate Between '" + odate + "' and  '" + odate.AddDays(2) + "')";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int UpdateBeforeVisit_YS(string patdid, string PatZhuYuanID, DateTime odate)
        {
            string sql = "update Adims_BeforeVisit_YS set patID='" + patdid + "' where patid='" + PatZhuYuanID + "' and (odate Between '" + odate + "' and  '" + odate.AddDays(2) + "')";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int UpdateMZZQTYS_YS(string patdid, string PatZhuYuanID, DateTime odate)
        {
            string sql = "update Adims_MZZQTYS_YS set patID='" + patdid + "' where patid='" + PatZhuYuanID + "' and (odate Between '" + odate + "' and  '" + odate.AddDays(2) + "')";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int UpdateSHZT_YS(string patdid, string PatZhuYuanID, DateTime odate)
        {
            string sql = "update Adims_AfterAnalgesia set patID='" + patdid + "' where patid='" + PatZhuYuanID + "' and (odate Between '" + odate + "' and  '" + odate.AddDays(2) + "')";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int UpdateZTZLZQTYS_YS(string patdid, string PatZhuYuanID, DateTime odate)
        {
            string sql = "update Adims_ZTZLZQTYS_YS set patID='" + patdid + "' where patid='" + PatZhuYuanID + "' and (odate Between '" + odate + "' and  '" + odate.AddDays(2) + "')";
            return dBConn.ExecuteNonQuery(sql);
        }

        public int UpdateTable(string sql)
        {

            return dBConn.ExecuteNonQuery(sql);
        }


    }
}
