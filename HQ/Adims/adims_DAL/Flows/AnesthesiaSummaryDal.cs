using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL.Flows
{
    /// <summary>
    /// 麻醉总结Dal
    /// </summary>
    public class AnesthesiaSummaryDal
    {
        private DBConn dBConn = new DBConn();

     
        /// <summary>
        /// 插入麻醉总结
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertMZZJ_CJ(Dictionary<string, string> dictionary)
        {
            string _INSERT = "INSERT INTO Adims_AnesthesiaSummary(mzjldID,patID,AddTime,QSMZ,YoudaoFF,ZGNMZ,ZGNMZFF,CCD11,CCD12"
                + ",liuzhiSD1,CCD21,CCD22,liuzhiSD2,MZPM_ZG,Yaopin_ZG,SJZZ,JCSJZZ"
                + ",BCSJZZ,YCSJZZ,ZGSJZZ,GSJZZ,GWCSJZZ,Yaopin_SJZZ,YCCZ,DMCCZG,SJMCCZG,OtherZZ,HZ,HZType,HZwcff,MZJH,RemarkMZ"
                + ",TaoNang,Jili,KesouTunyan,Dingxiangli,Yishi,MZPM_OUT,RemarkOut,BRQX,TSQK,MZYS"
                + ",QGCG,QGCGwz1,QGCGwz2,QGCGwz3,QGCGtype,QGCGsd,BRZKZT,isRead,QTNum,isYuanyin,Odate,ZGNMZFF_qm) "
                + "values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}',"
                + "'{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}','{51}','{52}','{53}')";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);

        }
        /// <summary>
        /// 修改啊名嘴总结
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateMZZJ_CJ(Dictionary<string, string> dictionary)
        {
            string upMZZJ = "UPDATE Adims_AnesthesiaSummary WITH (ROWLOCK) SET AddTime='{2}' ,QSMZ= '{3}',YoudaoFF= '{4}',ZGNMZ= '{5}',ZGNMZFF= '{6}',CCD11= '{7}',CCD12= '{8}',liuzhiSD1= '{9}'"
                + ",CCD21= '{10}',CCD22= '{11}',liuzhiSD2= '{12}',MZPM_ZG= '{13}',Yaopin_ZG= '{14}',SJZZ= '{15}',JCSJZZ= '{16}',BCSJZZ= '{17}',YCSJZZ= '{18}',ZGSJZZ= '{19}'"
                + ",GSJZZ= '{20}',GWCSJZZ= '{21}',Yaopin_SJZZ= '{22}',YCCZ= '{23}',DMCCZG= '{24}',SJMCCZG= '{25}',OtherZZ= '{26}',HZ= '{27}',HZType= '{28}',HZwcff= '{29}'"
                + ",MZJH= '{30}',RemarkMZ= '{31}',TaoNang= '{32}',Jili= '{33}',KesouTunyan= '{34}',Dingxiangli= '{35}',Yishi= '{36}',MZPM_OUT= '{37}',RemarkOut= '{38}',BRQX= '{39}'"
                + ",TSQK= '{40}',MZYS= '{41}',QGCG= '{42}',QGCGwz1= '{43}',QGCGwz2= '{44}',QGCGwz3= '{45}',QGCGtype= '{46}',QGCGsd= '{47}',BRZKZT= '{48}',isRead= '{49}',QTNum='{50}',isYuanyin='{51}',Odate='{52}',ZGNMZFF_qm='{53}' WHERE mzjldID ='{0}' ";
            string upMZZJ1 = string.Format(upMZZJ, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(upMZZJ1);

        }
        /// <summary>
        /// 修改麻醉总结可读性
        /// </summary>
        /// <param name="mzid"></param>
        /// <param name="isread"></param>
        /// <returns></returns>
        public int UpdateMZZJ_CJ(string mzid, int isread)
        {
            string sql = "UPDATE Adims_AnesthesiaSummary  SET IsRead='" + isread + "' where mzjldid='" + mzid + "' ";
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 通过麻醉记录单ID 查询麻醉总结
        /// </summary>
        /// <param name="mzjldID"></param>
        /// <returns></returns>
        public DataTable GetMzzjByMzjldId(string mzjldID)
        {
            string sql = "select * from Adims_AnesthesiaSummary where mzjldID='" + mzjldID + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 查询麻醉总结--通过条件
        /// </summary>
        /// <param name="where"></param>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public string GetMzzjByCondition(string where, string dt1, string dt2)
        {
            string Insert = "select COUNT(*) from Adims_AnesthesiaSummary where {0} and convert(nvarchar(10),Addtime,23) BETWEEN '" + dt1 + "' AND '" + dt2 + "'";
            DataTable dt = dBConn.GetDataTable(string.Format(Insert, where));
            return dt.Rows[0][0].ToString();
        }
        /// <summary>
        /// 通过病人ID 查询麻醉总结
        /// </summary>
        /// <param name="patID"></param>
        /// <returns></returns>
        public DataTable GetMzzjByPatID(string patID)
        {
            string sql = "select * from Adims_AnesthesiaSummary where patID='" + patID + "'";
            return dBConn.GetDataTable(sql);
        }
    }
}
