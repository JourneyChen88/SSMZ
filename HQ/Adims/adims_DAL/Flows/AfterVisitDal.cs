using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL.Flows
{
    /// <summary>
    /// 术后访视dal
    /// </summary>
    public class AfterVisitDal
    {
        private DBConn dBConn = new DBConn();

        /// <summary>
        /// 术后访视 医师
        /// </summary>
        /// <param name="ZYH"></param>
        /// <param name="dtate"></param>
        /// <returns></returns>
        public DataTable GetAfterVisit_YS(string patid, string dtate)
        {
            string sql = "select * from Adims_AfterVisit_YS where patid='" + patid + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetAfterVisit_YS_byMzjldid(int mzid)
        {
            string sql = "select * from Adims_AfterVisit_YS where mzjldid='" + mzid + "'";
            return dBConn.GetDataTable(sql);
        }

        /// <summary>
        /// 术后访视存档
        /// </summary>
        /// <param name="mzjldId"></param>
        /// <param name="isread"></param>
        /// <param name="tdate"></param>
        /// <returns></returns>
        public int UpdateAfterVisit_HQ_YS(int mzjldId, int isread)
        {
            string sql = "UPDATE Adims_AfterVisit_YS  SET IsRead='" + isread + "' where mzjldId='" + mzjldId + "'";
            return dBConn.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 添加术前访视 医师
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertAfterVisit_HQ_YS(Dictionary<string, string> dictionary)
        {

            string _INSERT = @"INSERT INTO Adims_AfterVisit_YS(
        MzjldID
      ,[IsRead]
      ,[PatId]
      ,[mzfs]
      ,[Mzsj]
      ,[Yishi]
      ,[Huxi]
      ,[Xunhuan]
      ,[Shzysx]
      ,[Qt]
      ,[mzxgbfz]
      ,[Ms]
      ,[Cl]
      ,[jg]
      ,[Mzys]
      ,[VisitDate])
     VALUES( '{0}', '{1}' , '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' , '{10}', '{11}', '{12}', '{13}', '{14}', '{15}')";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);

        }


        /// <summary>
        ///修改 术后访视 医师
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateAfterVisit_HQ_YS(Dictionary<string, string> dictionary)
        {
            string _INSERT = @"UPDATE [Adims_AfterVisit_YS]
   SET [IsRead] = '{1}'  
 ,[patid] = '{2}'   
      ,[mzfs] = '{3}'
      ,[Mzsj] = '{4}'
      ,[Yishi] = '{5}'
      ,[Huxi] = '{6}'
      ,[Xunhuan] = '{7}'
      ,[Shzysx] = '{8}'
      ,[Qt] = '{9}'
      ,[mzxgbfz] ='{10}'
      ,[Ms] = '{11}'
      ,[Cl] = '{12}'
      ,[jg] = '{13}'
      ,[Mzys] = '{14}' 
,[VisitDate] = '{15}' 
 WHERE  [mzjldId] ='{0}' ";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);

        }
    }
}
