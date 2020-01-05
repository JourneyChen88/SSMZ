using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace adims_DAL
{
    public class YXL_DAL
    {

        static DBConn dBConn = new DBConn();// 链接方式

        #region  CHA


        /// <summary>
        /// 获取病人信息学
        /// </summary>
        /// <param name="id">住院号</param>
        /// <returns></returns>
        public static DataTable GetPenpleInfo(string id)
        {
            string sql = "SELECT * from Adims_OTypesetting where  PatZhuYuanID='" + id + "'";
            return dBConn.GetDataTable(sql);
        }
        // 获取CHA数据
        public static DataTable GetCHAInfo(string id)
        {
            string sql = "SELECT * from YXL_CHA where  PatZhuYuanID='" + id + "' order by   Createdatetime desc";
            return dBConn.GetDataTable(sql);
        }
        // 创建CHA数据
        public static int CreateInfo(string id)
        {
            string sql = "insert into  YXL_CHA  (PatZhuYuanID , Createdatetime) VALUES ('" + id + "',CONVERT(nvarchar(50), GETDATE(), 20) ) ";
            return dBConn.ExecuteNonQuery(sql);
        }
        //  UpdateCHAInfo
       
        #endregion

        #region    手术室术前、术后访视单
        public static DataTable SQGetCInfo(string id)
        {
            string sql = "SELECT * from YXL_sqshfw where  PatZhuYuanID='" + id + "' order by   Createdatetime desc";
            return dBConn.GetDataTable(sql);
        }
        public static int SQCreateInfo(string id)
        {
            string sql = "insert into  YXL_sqshfw  (PatZhuYuanID , Createdatetime) VALUES ('" + id + "',CONVERT(nvarchar(50), GETDATE(), 20) ) ";
            return dBConn.ExecuteNonQuery(sql);
        }
        public static int SQUpdateInfo(string id, string Myparam, string str1)
        {
            string sql = "update YXL_sqshfw set " + Myparam + "='" + str1 + "'  where  PatZhuYuanID ='" + id + " ' ";
            return dBConn.ExecuteNonQuery(sql);
        }
        #endregion
        public  DataTable GetPenpleInfofengxianpg(string id)
        {
            string sql = "SELECT * from chafenxianpinggu where  zhuyuanhao='" + id + "'";
            return dBConn.GetDataTable(sql);
        }
        public int Updatefengxianpg(Dictionary<string, string> dictionary)
        {
            string sql = @"UPDATE [chafenxianpinggu]
   SET [shoushuriqi] = '{0}'
      
      ,[baochunckes] = '{2}'
      ,[shoushuyisheng] = '{3}'
      ,[mazuiyisheng] = '{4}'
      ,[xvhuihushi] = '{5}'
      ,[shoushuqingjiedu] = '{6}'
      ,[mazuiasa] = '{7}'
      ,[sscxsjf] = '{8}'
      ,[defen] = '{9}'
,[ssdj]='{10}',[mazfangshi]='{11}',[shishissmc]='{12}'
       WHERE [zhuyuanhao] = '{1}'";
            string sql1 = string.Format(sql, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql1);
        }
        public int TljInfengxianpinggu(Dictionary<string, string> dictionary)
        {

            string insert = @"INSERT INTO [chafenxianpinggu]
           ([shoushuriqi]
           ,[zhuyuanhao]
           ,[baochunckes]
           ,[shoushuyisheng]
           ,[mazuiyisheng]
           ,[xvhuihushi]
           ,[shoushuqingjiedu]
           ,[mazuiasa]
           ,[sscxsjf]
           ,[defen],[ssdj],[mazfangshi],[shishissmc]) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}'
           )";
            string sql = string.Format(insert, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        #region    手术患者压力性损伤评估表

        public DataTable GetPenpleInfoylsspgb(string id)
        {
            string sql = "SELECT * from ssylsspgb where  zhuyuanhao='" + id + "'";
            return dBConn.GetDataTable(sql);
        }

        public int Updateylsspgb(Dictionary<string, string> dictionary)
        {
            string sql = @"UPDATE [ssylsspgb]
   SET [shoushuriqi] = '{0}'
      
      ,[ssmingcheng] = '{2}'
      ,[txtzsg] ='{3}'
      ,[pflx] = '{4}'
      ,[xbnl] = '{5}'
      ,[yybl] = '{6}'
      ,[kbnl] = '{7}'
      ,[ydnl] = '{8}'
      ,[ys] = '{9}'
      ,[sjxza] = '{10}'
      ,[zpf] = '{11}'
      ,[pghs] = '{12}'
      ,[pfbzfl] = '{13}'
      ,[yfcs] = '{14}'
 WHERE [zhuyuanhao] = '{1}'";
            string sql1 = string.Format(sql, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql1);
        }
        public int TljInylsspgb(Dictionary<string, string> dictionary)
        {

            string insert = @"INSERT INTO [ssylsspgb]
           ([shoushuriqi]
           ,[zhuyuanhao]
           ,[ssmingcheng]
           ,[txtzsg]
           ,[pflx]
           ,[xbnl]
           ,[yybl]
           ,[kbnl]
           ,[ydnl]
           ,[ys]
           ,[sjxza]
           ,[zpf]
           ,[pghs]
           ,[pfbzfl]
           ,[yfcs])
     VALUES
          ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')";
            string sql = string.Format(insert, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        #endregion
    }
}
