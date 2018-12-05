using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL.Flows
{
    /// <summary>
    /// 麻醉手术知情同意书
    /// </summary>
    public class MzzqtysDal
    {
        private DBConn dBConn = new DBConn();

        /// <summary>
        /// 麻醉知情同意书
        /// </summary>
        /// <param name="ZYH"></param>
        /// <param name="dtate"></param>
        /// <returns></returns>
        public DataTable GetMZZQTYS_YS(string patId)
        {
            string sql = "select * from Adims_MZZQTYS_YS where patId='" + patId + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 修改麻醉知情同意书
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateMZZQTY_YS_HQ(Dictionary<string, string> dictionary)
        {
            string _INSERT = @"UPDATE [Adims_MZZQTYS_YS]
                             SET [Ysjy] = '{1}'
                              ,[Huanb] ='{2}'
                              ,[Nxss] = '{3}'
                              ,[Tsfx] ='{4}'
                              ,[Mzff] ='{5}'
                              ,[Hzqm1] ='{6}'
                              ,[Hzdate1] = '{7}'
                              ,[Mzty] = '{8}'
                              ,[Hzqm2] = '{9}'
                              ,[Hzdate2] ='{10}'
                              ,[Jsqm] ='{11}'
                              ,[Yhzgx] ='{12}'
                              ,[Jsdate] ='{13}'
                              ,[Ysqm] = '{14}'
                              ,[Qmrq] ='{15}'
                              ,[VisitDate] ='{16}'     
                              ,[IsRead] ='{17}'
                                WHERE [patid] = '{0}'";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);

        }
        /// <summary>
        /// 麻醉知情同意书 存档
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="isread"></param>
        /// <param name="tdate"></param>
        /// <returns></returns>
        public int UpdateMZZQTY_HQ_YS(string patid, int isread)
        {
            string sql = "UPDATE Adims_MZZQTYS_YS  SET IsRead='" + isread + "' where patid='" + patid + "'";
            return dBConn.ExecuteNonQuery(sql);
        }


        /// <summary>
        /// 添加麻醉知情同意书
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertMZZQTY_YS_HQ(Dictionary<string, string> dictionary)
        {
            string _INSERT = @"INSERT INTO [Adims_MZZQTYS_YS]
                ([patid] ,[Ysjy],[Huanb],[Nxss],[Tsfx],[Mzff],[Hzqm1] ,[Hzdate1],[Mzty],[Hzqm2],[Hzdate2]
                ,[Jsqm],[Yhzgx],[Jsdate],[Ysqm],[Qmrq],[VisitDate],[IsRead])
                VALUES ('{0}' ,'{1}','{2}','{3}','{4}','{5}','{6}' ,'{7}','{8}','{9}','{10}','{11}','{12}','{13}'
                ,'{14}','{15}','{16}','{17}')";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);

        }
    }
}
