using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL.Flows
{
    /// <summary>
    /// 术后镇痛dal
    /// </summary>
  public  class TransfusionEvaluationDal
    {
      private  DBConn dBConn = new DBConn();


        #region 输血评估模块中使用的方法
        /// <summary>
        /// 按麻醉ID查询输血评估信息
        /// </summary>
        /// <param name="mzid"></param>
        /// <returns></returns>
        public DataTable GetByMzjldid(string mzid)
        {
            string sql = "select * from Adims_TransfusionEvaluation where mzjldid='" + mzid + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 输血评估信息保存
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int Insert(Dictionary<string, string> dictionary)
        {
            string _INSERT = @"INSERT INTO [Adims_TransfusionEvaluation]([mzjldid],[PatID],[HXB],[XJ],[XXB],[LCD],[SXMD] ,[PFZMYS1],[PFZMYS2],[MSXH1],[MSXH2],[XueYa1],[XueYa2],[XinLV1] ,[XinLV2],[SPO21],[SPO22],[Hb1],[HCt1],[Hb2],[HCt2],[CVP1],[CVP2] ,[NL1],
[NL2],[CXL1],[CXL2],[MCSX1] ,[MCSX2],[SXFY],[ShuXueCL],[ZHPJ],[YiShiQM],[PGTime],[AddTime],[IsRead])"
                + "values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}')";

            return dBConn.ExecuteNonQuery(string.Format(_INSERT, dictionary.Values.ToArray()));
        }
        /// <summary>
        ///修改输血评估信息
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int Update(Dictionary<string, string> dictionary)
        {
            string sql = @"Update  [Adims_TransfusionEvaluation] set [PatID] ='{1}',[HXB] ='{2}',[XJ] ='{3}',[XXB] ='{4}'
,[LCD] ='{5}',[SXMD] ='{6}',[PFZMYS1] = '{7}',[PFZMYS2] ='{8}',[MSXH1] ='{9}',[MSXH2] ='{10}'
,[XueYa1] ='{11}',[XueYa2] ='{12}',[XinLV1] ='{13}' ,[XinLV2] ='{14}',[SPO21] ='{15}',[SPO22] ='{16}'
,[Hb1] ='{17}',[HCt1] ='{18}',[Hb2] = '{19}',[HCt2] ='{20}',[CVP1] ='{21}',[CVP2] ='{22}',[NL1] ='{23}'
,[NL2] ='{24}',[CXL1] ='{25}',[CXL2] ='{26}',[MCSX1] = '{27}',[MCSX2] ='{28}',[SXFY] ='{29}',[ShuXueCL] ='{30}'
,[ZHPJ] ='{31}',[YiShiQM] = '{32}',[PGTime] = '{33}',[UpdateTime] ='{34}',[IsRead] = '{35}' where mzjldid='{0}'";
            string sql1 = string.Format(sql, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql1);
        }
        /// <summary>
        /// 用于存档和解锁
        /// </summary>
        /// <param name="mzid"></param>
        /// <param name="isRead"></param>
        /// <returns></returns>
        public int UpdateIsRead(string mzid, int isRead)
        {
            string sql = "UPDATE Adims_TransfusionEvaluation  SET IsRead='" + isRead + "' where mzjldid='" + mzid + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        #endregion

    }
}
