using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL.Dics
{
    public class DataDicDal
    {
        private DBConn dBConn = new DBConn();


        /// <summary>
        /// 查询手术名字通过拼音
        /// </summary>
        /// <param name="pinyin"></param>
        /// <returns></returns>
        public DataTable GetOperationNameByPinyin(string pinyin)
        {
            return dBConn.GetDataTable("select onname 手术名称,olevel 手术等级 from OperationName where ospell like'%" + pinyin + "%' ");
        }
        public DataTable GetMazuiPingmianByName(string mzID)
        {
            string sql = "select name from MazuiPingmian where Name like '" + mzID + "%'";
            return dBConn.GetDataTable(sql);
        }

      
        public DataTable GetKeshi()
        {
            return dBConn.GetDataTable(string.Format("select name from Keshi "));
        }
        public DataTable GetKeshi(string name)
        {
            return dBConn.GetDataTable(string.Format("select name from Keshi where name=''"+ name + ""));
        }
        public int InsertKeshi(string name)
        {
            string sql = "insert into  keshi (name) values('" + name + "' ) ";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int DeleteKeshi(int id)
        {
            string sql = "delete from keshi where  id='" + id + "' ";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int UpdateKeshi(int id,string name)
        {
            string sql = "Update keshi set  name='" + name + "' where   id='" + id + "' ";
            return dBConn.ExecuteNonQuery(sql);
        }
        public DataTable GetTiwei()
        {
            return dBConn.GetDataTable(string.Format("select name from Tiwei "));
        }
        public DataTable GetASAname()
        {
            return dBConn.GetDataTable(string.Format("select name from ASA "));

        }
        public DataTable GetOroom()
        {
            return dBConn.GetDataTable(string.Format("select distinct NAME from SSJSTATE "));
        }
        /// <summary>
        /// 查询诱导用药
        /// </summary>
        /// <returns></returns>
        public DataTable GetYoudaoYao()
        {
            string selectAllYS = " SELECT name as 诱导用药 FROM YD_YY ";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }

        /// <summary>
        /// 麻醉平面
        /// </summary>
        /// <returns></returns>
        public DataTable GetMZPM()
        {
            string sql = "select name from MZPM ";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 麻醉穿刺
        /// </summary>
        /// <returns></returns>
        public DataTable GetMZCC()
        {
            string sql = "select name from MZCC ";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 查询数据表-根据tableName
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public DataTable SelectDataByTableName(string tableName)
        {
            string sql = "select id 编号,name 名称  from   " + tableName;
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 查找科室
        /// </summary>
        /// <returns></returns>
        public DataTable Select_Keshi()
        {
            string str = "select Name from keshi";
            return dBConn.GetDataTable(str);
        }
        /// <summary>
        /// 查询手术医生
        /// </summary>
        /// <returns></returns>
        public DataTable Select_SSYS()
        {
            string str = "select name from SSYS";
            return dBConn.GetDataTable(str);
        }
      

        /// <summary>
        /// 更具类型查找用户（）
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable GetUserByType(int type)
        {
            string select = "select user_name from adims_user where type='" + type + "' ";
            return dBConn.GetDataTable(select);
        }
        /// <summary>
        /// 身份
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllSF()
        {
            string selectAllYS = " SELECT name  FROM SF ";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }
        /// <summary>
        /// 风险评估
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllFXPG()
        {
            string selectAllYS = " SELECT name  FROM FXPG";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }
        /// <summary>
        /// 手术医生
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllSSYS()
        {
            string selectAllYS = " SELECT name  FROM SSYS";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }

        /// <summary>
        /// 手术级别
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllSSJB()
        {
            string selectAllYS = " SELECT name  FROM SSJB ";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }
        /// <summary>
        /// 手术类别
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllSSLB()
        {
            string selectAllYS = " SELECT name  FROM SSLB ";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }
        /// <summary>
        /// 麻醉穿刺
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllMZCC()
        {
            string selectAllYS = " SELECT name as 麻醉穿刺 FROM MZCC ";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }
        /// <summary>
        /// 查询所有麻醉医生
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllMZYS()
        {
            string selectAllYS = " SELECT user_name FROM Adims_user where type='1'";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }

        /// <summary>
        /// 查询所有手术医生
        /// </summary>
        /// <returns></returns>
        public DataTable GetAll_shoushuyisheng()
        {
            string selectAllYS = " SELECT MedicalName FROM Adims_SurgeryStaff where PostType='1'";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }
        /// <summary>
        /// 查询所有护士
        /// </summary>
        /// <returns></returns>
        public DataTable GetAll_hushi()
        {
            string selectAllYS = " SELECT user_name FROM Adims_user where Type='2'";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }
       
        /// <summary>
        /// 查找所有麻醉方法
        /// </summary>
        /// <returns></returns>
        public DataTable GetMazuiFangfaAll()
        {
            string sql = "select name from MazuiFangfa ";
            return dBConn.GetDataTable(sql);
        }
    }
}
