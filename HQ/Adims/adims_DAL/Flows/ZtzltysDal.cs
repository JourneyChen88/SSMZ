using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL.Flows
{
    /// <summary>
    /// 镇痛治疗同意书
    /// </summary>
    public class ZtzltysDal
    {
        private DBConn dBConn = new DBConn();
        /// <summary>
        /// 修改镇痛治疗同意书
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateZTZLTY_YS_HQ(Dictionary<string, string> dictionary)
        {
            string _INSERT = @"UPDATE [Adims_ZTZLZQTYS_YS]
   SET [Zd] = '{1}'
      ,[ZTfs] = '{2}'
      ,[Hzqm] = '{3}'
      ,[Jsqm] = '{4}'
      ,[Yhzgx] ='{5}'
      ,[MZYsqm] = '{6}'
      ,[ZLysqm] = '{7}'
      ,[VisitDate] = '{8}'
      ,[IsRead] = '{9}'
 WHERE [patid] ='{0}' and [Odate] = '{10}'";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);

        }
        /// <summary>
        /// 添加镇痛治疗同意书
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertZTZLTY_YS_HQ(Dictionary<string, string> dictionary)
        {
            string _INSERT = @"INSERT INTO [Adims_ZTZLZQTYS_YS]([patid],[Zd],[ZTfs] ,[Hzqm] ,[Jsqm],[Yhzgx] ,[MZYsqm],[ZLysqm],[VisitDate],[IsRead],[Odate])
VALUES ( '{0}','{1}', '{2}', '{3}' ,'{4}' ,'{5}','{6}' ,'{7}','{8}','{9}','{10}')";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);

        }
        /// <summary>
        /// 镇痛治疗同意书 存档
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="isread"></param>
        /// <param name="tdate"></param>
        /// <returns></returns>
        public int UpdateZQZLTYS_HQ_YS(string patid, int isread)
        {
            string sql = "UPDATE Adims_ZTZLZQTYS_YS  SET IsRead='" + isread + "' where patid='" + patid + "'  ";
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 镇痛治疗知情同意书
        /// </summary>
        /// <param name="ZYH"></param>
        /// <param name="dtate"></param>
        /// <returns></returns>
        public DataTable GetZTZLZQTYS_YS(string patid)
        {
            string sql = "select * from Adims_ZTZLZQTYS_YS where patid='" + patid + "' ";
            return dBConn.GetDataTable(sql);
        }
    }
}
