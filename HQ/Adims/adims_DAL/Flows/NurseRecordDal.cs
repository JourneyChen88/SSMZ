using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL.Flows
{
    /// <summary>
    /// 护理记录DAL
    /// </summary>
   public class NurseRecordDal
    {
        private DBConn dBConn = new DBConn();

        public int UpdateNurseRecord_HQ(Dictionary<string, string> dictionary)
        {

            string SQL = @"UPDATE Adims_NurseRecord_HQ WITH (ROWLOCK)  SET XueType='{1}',XYCFName='{2}',XL='{3}',
SXDW='{4}',QXHS='{5}', XHHS='{6}',QXHS1='{7}', XHHS1='{8}',QXHS2='{9}', XHHS2='{10}', 
qxbName='{11}',Odate='{12}' WHERE mzjldid ='{0}' ";
            string sql = string.Format(SQL, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        public DataTable GetNurseRecord_HQ(string mzjldid)
        {
            string sql = "Select * from Adims_NurseRecord_HQ where mzjldid='" + mzjldid + "'";
            return dBConn.GetDataTable(sql);
        }
        public int InsertNurseRecord_HQ(string mzid)
        {
            string sql = "INSERT INTO Adims_NurseRecord_HQ(mzjldid ) VALUES('" + mzid + "')";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int UpdateNurseRecord_HQ(string mzid, int isRead)
        {
            string sql = "UPDATE Adims_NurseRecord_HQ  SET IsRead='" + isRead + "' where mzjldid='" + mzid + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
    }
}
