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
    public class AfterAnalgesia
    {
        private DBConn dBConn = new DBConn();

        /// <summary>
        /// 术后镇痛明细删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteAfterAnalgesiaDetail(string id)
        {
            string _INSERT = "delete from Adims_AfterAnalgesiaDetail where id='" + id + "'";
            return dBConn.ExecuteNonQuery(_INSERT);
        }
        /// <summary>
        /// 插入术后镇痛明细
        /// </summary>
        /// <param name="MZID"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public int InsertAfterAnalgesiaDetail(string MZID, string time)
        {
            string _INSERT = "INSERT INTO Adims_AfterAnalgesiaDetail(MzjldId,VisitTime) values('" + MZID + "','" + time + "')";
            return dBConn.ExecuteNonQuery(_INSERT);
        }

        /// <summary>
        /// 修改术后镇痛明细
        /// </summary>
        /// <param name="id"></param>
        /// <param name="DateType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int UpdateAfterAnalgesiaDetail(string id, string DateType, string value)
        {
            string sql = "UPDATE Adims_AfterAnalgesiaDetail WITH (ROWLOCK) SET " + DateType + " = '" + value + "' WHERE id = '" + id + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 查询术后镇痛明细表
        /// </summary>
        /// <param name="ZYH"></param>
        /// <param name="dtate"></param>
        /// <returns></returns>
        public DataTable GetAfterAnalgesiaDetail(string MzjldId)
        {
            string sql = "select   *  FROM Adims_AfterAnalgesiaDetail where MzjldId ='" + MzjldId + "' order by VisitDate";
            return dBConn.GetDataTable(sql);
        }

        /// <summary>
        /// 术后镇痛 存档
        /// </summary>
        /// <param name="mzid"></param>
        /// <param name="isread"></param>
        /// <returns></returns>
        public int UpdateAfterAnalgesia(string mzid, int isread)
        {
            string sql = "UPDATE Adims_AfterAnalgesia  SET IsRead='" + isread + "' where MzjldId='" + mzid + "'  ";
            return dBConn.ExecuteNonQuery(sql);
        }

        public DataTable GetAfterAnalgesia(string MzjldId)
        {
            string sql = "select   *  FROM Adims_AfterAnalgesia where MzjldId ='" + MzjldId + "'";
            return dBConn.GetDataTable(sql);
        }

        /// <summary>
        /// 添加术后镇痛
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertAfterAnalgesia(Dictionary<string, string> dictionary)
        {
            string _INSERT = @"INSERT INTO [Adims_AfterAnalgesia]([MzjldId],[IsRead],[patid],[Ssmc],[Mzff],[Ztff],[Ztpf]
,[ZTsj],[ZTGysd],[ZTDczj],[ZTSdsj],[ZTKssj],[ZTjssj] ,[Bfzcl],[VASpf],[VisitDate])
     VALUES ('{0}','{1}' ,'{2}','{3}' ,'{4}','{5}' ,'{6}','{7}','{8}','{9}' ,'{10}','{11}' ,'{12}','{13}'
 ,'{14}','{15}')";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);

        }
        /// <summary>
        /// 修改术后镇痛
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateAfterAnalgesia(Dictionary<string, string> dictionary)
        {
            string _INSERT = @"UPDATE [Adims_AfterAnalgesia]
            SET [IsRead] ='{1}',[patid] = '{2}',[Ssmc] = '{3}',[Mzff] ='{4}',[Ztff] ='{5}',[Ztpf] ='{6}',[ZTsj] = '{7}'
            ,[ZTGysd] = '{8}' ,[ZTDczj] ='{9}',[ZTSdsj] ='{10}',[ZTKssj] = '{11}',[ZTjssj] ='{12}',[Bfzcl] = '{13}'
            ,[VASpf] = '{14}',[VisitDate] ='{15}'  WHERE [MzjldId] = '{0}'";

            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);

        }
    }
}
