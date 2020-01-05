using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using adims_DAL;

namespace adims_BLL
{
    public class YXL_BLL
    {
        //获取病人信息
        DBConn dBConn = new DBConn();
        public DataTable qixiefuliao1(string id)
        {
            string sql = "select * from BJsyflqd where binganhao='" + id + "'";
            return dBConn.GetDataTable(sql);
        }//敷料加载


        public DataTable BCHqixiefuliao(string id)
        {
            string sql = "select * from BJsysshqxqd where binganhao='" + id + "'";
            return dBConn.GetDataTable(sql);
        }//器械加载
        public DataTable SelectPatInfo111(string patid)
        {
            return adims_DAL.YXL_DAL.GetPenpleInfo(patid);
        }
        #region  CHA函授
        //获取CHA 数据
        public DataTable GetCHAInfo(string patid)
        {
            return adims_DAL.YXL_DAL.GetCHAInfo(patid);
        }
        //  CreateInfo
        public int CreateInfo(string patid)
        {
            return adims_DAL.YXL_DAL.CreateInfo(patid);
        }
        // UpdateCHAInfo         
        
        #endregion

        #region  手术室术前、术后访视单
        public DataTable SQGetInfo(string patid)
        {
            return adims_DAL.YXL_DAL.SQGetCInfo(patid);
        }

        public int SQCreateInfo(string patid)
        {
            return adims_DAL.YXL_DAL.SQCreateInfo(patid);
        }
        public int SQUpdateInfo(string patid, string Myparams, string str1)
        {
            return adims_DAL.YXL_DAL.SQUpdateInfo(patid, Myparams, str1);
        }
        #endregion




        public DataTable shoushufei(string id)
        {
            string sql = "select * from mazuikeshoufei where ITEM_NO='" + id + "'";
            return dBConn.GetDataTable(sql);
        }//敷料加载

        public DataTable mzjiazai(string zhuyuanhao)
        {
            string sql = "select * from JHmzjld where zyh='" + zhuyuanhao + "'";
            return dBConn.GetDataTable(sql);
        }//麻醉加载
        public DataTable mzjlqkfxzj()
        {
            string sql = "select mzjlfxqkzj from Mzjlqkfxzj";
            return dBConn.GetDataTable(sql);
        }
        public int jhMZXingZeng(Dictionary<string, string> dictionary)
        {
            string insert = @"INSERT INTO JHmzjld(zyh,ccdyd,ccdrd,wqsd,dgcrsd,dgcryy,ssssd,sssxd,ssbsd,ssbxd,qmczqt,mzqjbfzqt,qkfxzj,mzzjsyz,mzzjz,cctw,ccd,zrf,hrdgj,fybd,zszl,zx,dgcrqk,qmcz,mzydq,mzqjbfz1,mzqjbfz2,mzqjbfz3,mzqjbfz4)VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}')";
            string sql = string.Format(insert, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }//麻醉新增
        public int jhMZXiuGai(Dictionary<string, string> dictionary)
        {
            string ins = @"UPDATE JHmzjld SET ccdyd = '{1}',ccdrd = '{2}',wqsd = '{3}',dgcrsd = '{4}',dgcryy = '{5}',ssssd = '{6}',sssxd = '{7}',ssbsd = '{8}',ssbxd = '{9}',qmczqt = '{10}',mzqjbfzqt ='{11}',qkfxzj = '{12}',mzzjsyz = '{13}',mzzjz = '{14}',cctw = '{15}',ccd = '{16}',zrf = '{17}',hrdgj = '{18}',fybd = '{19}',zszl = '{20}',zx = '{21}',dgcrqk = '{22}',qmcz = '{23}',mzydq = '{24}',mzqjbfz1 = '{25}',mzqjbfz2 = '{26}',mzqjbfz3 = '{27}',mzqjbfz4 = '{28}'WHERE zyh = '{0}'";
            string sql1 = string.Format(ins, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql1);
        }


        public DataTable mzbyjiazai(string zhuyuanhao)
        {
            string sql = "select * from JHMZdby where zyh='" + zhuyuanhao + "'";
            return dBConn.GetDataTable(sql);
        }//麻醉单背页加载
        public int jhMZBYXingZeng(Dictionary<string, string> dictionary)
        {
            string insert = @"INSERT INTO JHMZdby (zyh,huli,tiwei,xueya,bidaoguan,mianzhao,fuxuankuang,qita)VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";
            string sql = string.Format(insert, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }//麻醉单背页新增
        public int jhMZBYXiuGai(Dictionary<string, string> dictionary)
        {
            string ins = @"UPDATE JHMZdby SET huli = '{1}',tiwei = '{2}',xueya = '{3}',bidaoguan = '{4}',mianzhao = '{5}',fuxuankuang = '{6}',qita = '{7}'WHERE zyh = '{0}'";
            string sql1 = string.Format(ins, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql1);
        }//麻醉单背页修改
    }
}
