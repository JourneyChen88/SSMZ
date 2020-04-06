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



        public DataTable jichujiazai(string zhuyuanhao)
        {
            string sql = "select * from Adims_OTypesetting where PatZhuYuanID='" + zhuyuanhao + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable jiazai(string zhuyuanhao)
        {
            string sql = "select * from jhszyyhljld where zyblh='" + zhuyuanhao + "'";
            return dBConn.GetDataTable(sql);
        }//加载
        public DataTable hdqd(string id)
        {
            string sql = "SELECT * FROM jhqdhd where zyblh='" + id + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable jhqixiefuliao1(string id)
        {
            string sql = "select * from jhqxpm where zyblh='" + id + "'";
            //string sql = "select qxpm,sqqd,gqhd,ghhd,sbhd,qxpm1,sqqd1,gqhd1,ghhd1,sbhd1,qxpm2,sqqd2,gqhd2,ghhd2,sbhd2 from jhqxpm where zyblh='"+id+"'";
            return dBConn.GetDataTable(sql);
        }//器械加载
        public DataTable jhqixiefuliao(string id)
        {
            //string sql = "select pm,sqqd,gqhd,ghhd,sbhd,pm1,sqqd1,gqhd1,ghhd1,sbhd1 from jhflpm where zyblh='" + id + "'";
            string sql = "select * from jhflpm where zyblh='" + id + "'";
            return dBConn.GetDataTable(sql);
        }//敷料加载
        public int jhXiuGai(Dictionary<string, string> dictionary)
        {
            string ins = @" UPDATE jhszyyhljld SET [xm] = '{0}',[xb] = '{1}',[ln] = '{2}',[bq] = '{3}',[ch] = '{4}',[ssrq] = '{6}',[sqzd] = '{7}',[ssmc] = '{8}',[ssj] = '{9}',[rssj] = '{10}',[sqycfxpg] = '{11}',[pfqk] = '{12}',[ywgms] = '{13}',[tw] = '{14}',[yl] = '{15}',[sj] = '{16}',[zrwpmc] = '{17}',[bbmc] = '{18}',[ysqk] = '{19}',[sbpfqk] = '{20}',[lssj] = '{21}',[sbqt] = '{22}',[ylg] = '{23}',[bw] = '{24}',[zywjb] = '{25}',[qm] = '{26}',[fxk] = '{27}'WHERE [zyblh] = '{5}'";
            string sql1 = string.Format(ins, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql1);
        }//修改
        public int jhXingZeng(Dictionary<string, string> dictionary)
        {
            string insert = @"INSERT INTO jhszyyhljld ([xm],[xb],[ln],[bq],[ch],[zyblh],[ssrq],[sqzd],[ssmc],[ssj],[rssj],[sqycfxpg],[pfqk],[ywgms],[tw],[yl],[sj],[zrwpmc],[bbmc],[ysqk],[sbpfqk],[lssj],[sbqt],[ylg],[bw],[zywjb],[qm],[fxk]) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}')";
            string sql = string.Format(insert, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }//新增
        public int jhqxXingZeng(Dictionary<string, string> dictionary)
        {
            string insert = @"INSERT INTO jhqxpm([zyblh],[qxpm],[sqqd],[gqhd],[ghhd],[sbhd],[qxpm1],[sqqd1],[gqhd1],[ghhd1],[sbhd1],[qxpm2],[sqqd2],[gqhd2],[ghhd2],[sbhd2])VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}')";
            string sql = string.Format(insert, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }//器械新增
        public int jhQXXiuGai(Dictionary<string, string> dictionary)
        {
            string ins = @"UPDATE jhqxpm SET [qxpm] = '{2}',[sqqd] ='{3}',[gqhd] = '{4}',[ghhd] = '{5}',[sbhd] = '{6}',[qxpm1] = '{7}',[sqqd1] = '{8}',[gqhd1] = '{9}',[ghhd1] = '{10}',[sbhd1] = '{11}',[qxpm2] = '{12}',[sqqd2] = '{13}',[gqhd2] = '{14}',[ghhd2] = '{15}',[sbhd2] = '{16}'WHERE [zyblh] = '{0}' and ID='{1}'";
            string sql1 = string.Format(ins, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql1);
        }//器械修改
        public int jhflXingZeng(Dictionary<string, string> dictionary)
        {
            string insert = @"INSERT INTO jhflpm([zyblh],[pm],[sqqd],[gqhd],[ghhd],[sbhd],[pm1],[sqqd1],[gqhd1],[ghhd1],[sbhd1])
                              VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')";
            string sql = string.Format(insert, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }//敷料新增
        public int jhFLXiuGai(Dictionary<string, string> dictionary)
        {
            string ins = @"UPDATE jhflpm SET [pm] = '{2}',[sqqd] = '{3}',[gqhd] = '{4}',[ghhd] = '{5}',[sbhd] = '{6}',[pm1] = '{7}',[sqqd1] = '{8}',[gqhd1] = '{9}'
      ,[ghhd1] = '{10}',[sbhd1] ='{11}' WHERE [zyblh] = '{0}' and ID='{1}'";
            string sql1 = string.Format(ins, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql1);
        }//敷料修改
        public int jhqmhdXingZeng(Dictionary<string, string> dictionary)
        {
            string insert = @"INSERT INTO jhqdhd([zyblh],[zrr],[sqqd],[gqhd],[ghhd],[sbhd])VALUES('{0}','{1}','{2}','{3}','{4}','{5}')";
            string sql = string.Format(insert, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }//签名核对新增
        public int jhqmhdXiuGai(Dictionary<string, string> dictionary)
        {
            string ins = @"UPDATE jhqdhd SET [zrr] = '{1}',[sqqd] = '{2}',[gqhd] = '{3}',[ghhd] = '{4}',[sbhd] = '{5}' WHERE [zyblh] = '{0}'";
            string sql1 = string.Format(ins, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql1);
        }//签名核对修改
    }
}
