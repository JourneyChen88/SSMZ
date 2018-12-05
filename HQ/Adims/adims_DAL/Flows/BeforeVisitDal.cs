using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL.Flows
{
    /// <summary>
    /// 术前访视DAL
    /// </summary>
  public  class BeforeVisitDal
    {
        private DBConn dBConn = new DBConn();
        /// <summary>
        /// 护士术前术后访视 存档
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="isread"></param>
        /// <returns></returns>
        public int UpdateBeforeVisit_HS(string patid, int isread)
        {
            string sql = "UPDATE Adims_BeforeVisit_HS  SET IsRead='" + isread + "' where patid='" + patid + "' ";
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 添加访视 护士
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertBeforeVisit_HS(Dictionary<string, string> dictionary)
        {
            string _INSERT = @"INSERT INTO [Adims_BeforeVisit_HS]([IsRead],[patid],[Yszk],[Yyzk],[Ywgms],[Yw],[Xlzk] ,[Yygt],[Zthd],[Qspf],[Psbw],[Ycbw],[Fq],[Mj],[Qt] ,[Xx],[Gy],[Brzwcd],[zwyy] ,[chzwcd]
,[Yuanyin],[fsDate],[hsqm],[Sh_ssshj],[Sh_sztw],[Sh_mycd],[Sh_Skyhqk],[Sh_yj],[Sh_sfdate],[Sh_hsqm],[odate],bqgy,Hzlxfs,Ssjb,Fxpg,Xsex,MZFS,SSLB)
VALUES('{0}' ,'{1}','{2}','{3}','{4}','{5}','{6}' ,'{7}','{8}','{9}','{10}','{11}','{12}','{13}'
,'{14}','{15}' ,'{16}','{17}' ,'{18}','{19}' ,'{20}','{21}','{22}','{23}','{24}','{25}','{26}'
,'{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}')";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);

        }

        /// <summary>
        ///修改 访视 护士
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateBeforeVisit_HS(Dictionary<string, string> dictionary, string id)
        {
            string _INSERT = @"UPDATE [Adims_BeforeVisit_HS]
   SET [IsRead] ='{0}',[Yszk] ='{2}' ,[Yyzk] ='{3}',[Ywgms] = '{4}',[Yw] = '{5}',[Xlzk] ='{6}'
,[Yygt] = '{7}',[Zthd] ='{8}',[Qspf] ='{9}',[Psbw] ='{10}',[Ycbw] ='{11}',[Fq] ='{12}',[Mj] = '{13}'
,[Qt] ='{14}',[Xx] ='{15}',[Gy] ='{16}',[Brzwcd] = '{17}',[zwyy] ='{18}',[chzwcd] ='{19}',[Yuanyin] ='{20}',[fsDate]='{21}'      
,[hsqm] = '{22}',[Sh_ssshj] = '{23}',[Sh_sztw] ='{24}' ,[Sh_mycd] = '{25}',[Sh_Skyhqk] ='{26}',[Sh_yj] ='{27}'
,[Sh_sfdate] ='{28}',[Sh_hsqm] = '{29}', [odate]='{30}',[bqgy] = '{31}',[Hzlxfs] = '{32}',[Ssjb] = '{33}',[Fxpg] = '{34}',[Xsex] = '{35}',[MZFS] = '{36}',[SSLB] = '{37}'  WHERE  [patid] ='{1}' and id='" + id + "'";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);

        }
        /// <summary>
        /// 护士访视
        /// </summary>
        /// <param name="ZYH"></param>
        /// <param name="dtate"></param>
        /// <returns></returns>
        public DataTable GetBeforeVisit_HS(string patid)
        {
            string sql = "select * from Adims_BeforeVisit_HS where patid='" + patid + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 术前访视 医师
        /// </summary>
        /// <param name="mzID"></param>
        /// <returns></returns>
        public DataTable GetBeforeVist_YS(string patid)
        {
            string sql = "select * from Adims_BeforeVisit_YS where patid='" + patid + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        ///修改 术前访视 医师
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateBeforeVist_YS(Dictionary<string, string> dictionary)
        {
            string _INSERT = @"UPDATE [Adims_BeforeVisit_YS]
   SET [IsRead] = '{1}',[Sqzd] =  '{2}',[Nsss] = '{3}',[Gms] =  '{4}',[Gxz] = '{5}',[Gqt] =  '{6}'
,[Mzs] = '{7}',[Mxz] =  '{8}',[Mqt] =  '{9}',[Jws] = '{10}',[Jxz] = '{11}',[Jqt] =  '{12}',[Tsyy] = '{13}',[Txz] =  '{14}',[Tqt] = '{15}',[Jzhdqk] = '{16}',[Zkkn] =  '{17}',[Mallampati] =  '{18}',[Knqd] = '{19}',[Yuanyin] = '{20}'
,[Hxgnpg] = '{21}',[NYHA] = '{22}',[xcg] = '{23}',[Ggn] = '{24}',[Nxgn] = '{25}',[xt] = '{26}',[Djz] = '{27}'
,[Xdt] = '{28}',[XlXZ] = '{29}',[XlQT] = '{30}',[Tsjc] = '{31}',[ASA] = '{32}',[E] = '{33}',[Mzqyy] = '{34}'
,[Yd] = '{35}',[YY] = '{36}',[Cg] = '{37}',[Dgzb] = '{38}',[Jmmzy] = '{39}',[Xrmzy] = '{40}',[zty] = '{41}'
,[Jsy] = '{42}',[Zgn] = '{43}' ,[ZgCC] = '{44}',[Qtmz] = '{45}',[mzqt] = '{46}',[Nxjcx] = '{47}',[NxjcxQT] = '{48}'
,[Szwt] =  '{49}',[sjysjjwt] =  '{50}',[Kntlyj] = '{51}',[Mzys] =  '{52}',[VisitDate] = '{53}',[Mzfxpg]='{55}' WHERE  [patid] = '{0}' ";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);

        }
      
        /// <summary>
        /// 添加术前访视(医生)
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertBeforeVisit_YS(string Patid, int isread, string odate)
        {
            string INSERT = "INSERT INTO Adims_BeforeVisit_YS(patid,isread,odate) VALUES('{0}','{1}','{2}')";
            string sql = string.Format(INSERT, Patid, isread, odate);
            return dBConn.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 术前访视存档
        /// </summary>
        /// <param name="mzid"></param>
        /// <param name="isread"></param>
        /// <returns></returns>
        public int UpdateBeforeVist_YS_HQ(string patid, int isread)
        {
            string sql = "UPDATE Adims_BeforeVisit_YS  SET IsRead='" + isread + "' where patid='" + patid ;
            return dBConn.ExecuteNonQuery(sql);
        }
    }
}
