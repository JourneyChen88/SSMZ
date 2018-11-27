using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace adims_DAL
{
    public class PACU_DAL
    {
         private DBConn dBConn = new DBConn();
         static string strconn = ConfigurationManager.AppSettings["ConnectionString"];

        #region <<方法>>

        
        /// <summary>
        /// 从table中获取数据
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DataTable GetData(string table)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter("select * from " + table, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            return dt;
        }
        public static DataTable GetData1(string table, string name)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter("select ID 编号,name " + name + " from " + table, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        public static DataTable GetData1(string table, string name,string belong)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter("select ID 编号,name " + name + ",belong "+belong+" from " + table, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            return dt;
        }

      

        public static DataTable GetData1(string sql)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 向table表里面插入数据
        /// </summary>
        /// <param name="name">插入值</param>
        /// <param name="table">数据表</param>
        public static void AddData1(string name, string table)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from " + table, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataRow newrow = dt.NewRow();
            newrow["Name"] = name;
            dt.Rows.Add(newrow);
            adapter.Update(dt);
        }

        public static void AddData1(string name, string belong,string table)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from " + table, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataRow newrow = dt.NewRow();
            newrow["Name"] = name;
            newrow["belong"] = belong;
            dt.Rows.Add(newrow);
            adapter.Update(dt);
        }
        
        /// <summary>
        /// 更新table表的中的name字段
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        /// <param name="table"></param>
        public static void UpdateData1(string ID, string Name, string table)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from " + table, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataColumn[] keys = new DataColumn[1];
            keys[0] = dt.Columns["ID"];
            dt.PrimaryKey = keys;
            DataRow findrow = dt.Rows.Find(ID);
            findrow["Name"] = Name;
            adapter.Update(dt);
        }
        public static void UpdateData1(string ID, string Name, string belong,string table)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from " + table, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataColumn[] keys = new DataColumn[1];
            keys[0] = dt.Columns["ID"];
            dt.PrimaryKey = keys;
            DataRow findrow = dt.Rows.Find(ID);
            findrow["Name"] = Name;
            findrow["belong"] = belong;
            adapter.Update(dt);


        }

        /// <summary>
        /// 判断table表的name是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static bool IsHaveName(string name, string table)
        {
            string sql = "select * from " + table + " where Name='" + name + "'";
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            if (count < 1)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// 删除table表的ID行
        /// </summary>
        /// <param name="ID">ID</param>
        public static void DeleteData(string ID, string table)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from " + table, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataColumn[] keys = new DataColumn[1];
            keys[0] = dt.Columns["ID"];
            dt.PrimaryKey = keys;
            DataRow findrow = dt.Rows.Find(ID);
            findrow.Delete();
            adapter.Update(dt);
        }
        #endregion

        #region//事件管理

        public DataTable GetAdims_szsjListSingleType() /// 
        {
            string sql = "SELECT distinct TYPE FROM Adims_szsjList ";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetAdims_szsjList(string name) /// 
        {
            string sql = "SELECT * FROM Adims_szsjList  WHERE name='" + name + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetAdims_szsjListByType(string type) /// 
        {
            string sql = "SELECT * FROM Adims_szsjList  WHERE  type='" + type + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetAdims_szsjListByType(string name,string type) /// 
        {
            string sql = "SELECT * FROM Adims_szsjList  WHERE name='"+name+"'and type='" + type + "'";
            return dBConn.GetDataTable(sql);
        }
        public int InsertAdims_szsjList(string name, string type) /// 
        {
            string sql = "INSERT INTO Adims_szsjList(name,type) VALUES ('" + name + "','" + type + "')";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int deleteAdims_szsjList(int id)
        {
            string delete = "delete from Adims_szsjList where id='" + id + "' ";
            return dBConn.ExecuteNonQuery(string.Format(delete));
        }
        public int UpdateAdims_szsjList(string name, string type, int id)
        {
            string SQL11 = "UPDATE Adims_szsjList  SET name='" + name + "',type ='" + type + "'  WHERE id ='" + id + "' ";
            return dBConn.ExecuteNonQuery(SQL11);
        }
        #endregion

        #region//药品管理
        public DataTable GetAdims_YaoPinBag(string name, string bag) /// 
        {
            string sql = "SELECT * FROM Adims_YaoPinBag  WHERE ypname='" + name + "'and bagname='" + bag + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetAdims_YaoPin(string name, string yptype) /// 
        {
            string sql = "SELECT * FROM Adims_YaoPin  WHERE ypname='" + name + "'and yptype='" + yptype + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetAdims_YaoPinByType(string yptype) ///
        {
            string sql = "SELECT * FROM Adims_YaoPin where yptype='" + yptype + "' ";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetYongYaoBag(string bagname) ///
        {
            string sql = "SELECT ypname,yl,dw,zrff,cxyy FROM Adims_YaoPinBag where bagname='" + bagname + "' ";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetAdims_YaoPinBagByType(string bagname) ///
        {
            string sql = "SELECT * FROM Adims_YaoPinBag where bagname='" + bagname + "' ";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetAdims_ALLBagName() ///
        {
            string sql = "SELECT DISTINCT BAGNAME FROM Adims_YaoPinBag ";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetALLAdims_YaoPin( ) ///
        {
            string sql = "SELECT * FROM Adims_YaoPin ";
            return dBConn.GetDataTable(sql);
        }
        public int deleteAdims_YaoPin(int id)
        {
            string delete = "delete from Adims_YaoPin where id='" + id + "' ";
            return dBConn.ExecuteNonQuery(string.Format(delete));
        }
        public int deleteAdims_YaoPinBag(int id)
        {
            string delete = "delete from Adims_YaoPinBag where id='" + id + "' ";
            return dBConn.ExecuteNonQuery(string.Format(delete));
        }
        public int InsertAdims_YaoPin(string name, string type) /// 
        {
            string sql = "INSERT INTO Adims_YaoPin(ypname,yptype) VALUES ('" + name + "','" + type + "')";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int InsertAdims_YaoPinBag(string bagname, string ypname, string yl, string dw, string zrff,int cxyy) /// 
        {
            string sql = "INSERT INTO Adims_YaoPinBag(bagname,ypname,yl,dw,zrff,cxyy) VALUES ('" + bagname + "','" + ypname + "','" + yl + "','" + dw + "','" + zrff + "','" + cxyy + "')";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int UpdateAdims_YaoPin(string name, string type,int id)
        {
            string SQL11 = "UPDATE Adims_YaoPin  SET ypname='" + name + "',yptype ='" + type + "'  WHERE id ='" + id + "' ";
            return dBConn.ExecuteNonQuery(SQL11);
        }
        #endregion


        public DataTable GetPACU(int mzid) /// 获取PACU检测点
        {
            string sql = "SELECT * FROM Adims_PACU_SZ WITH (NOLOCK) WHERE mzjldid='" + mzid + "'";
            return dBConn.GetDataTable(sql);
        }
        public int InsertPACU(int mzid,DateTime otime,DateTime vtime) /// 插入PACU单子
        {
            string sql = "INSERT INTO Adims_PACU_SZ(mzjldid,otime,savetime) VALUES ('" + mzid + "','" + otime + "','" + vtime + "')";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int UpdatePACU(string mzys,string zxhs,int mzid)
        {
            string SQL11 = "UPDATE Adims_PACU_SZ  SET mzys='"+mzys+"',zxhs='"+zxhs+"'  WHERE mzjldid ='"+mzid+"' ";
            return dBConn.ExecuteNonQuery(SQL11);
        }
        public int UpdatePACU_jcsjjg(int  mzjld,string jcsjjg)
        {
            string SQL11 = "UPDATE Adims_PACU_SZ  SET jcsjjg='" + jcsjjg + "'  WHERE mzjldid ='" + mzjld + "' ";
            return dBConn.ExecuteNonQuery(SQL11);
        }
        public int UpdatePACU_mzys(int mzjld, string mzys)
        {
            string SQL11 = "UPDATE Adims_PACU_SZ  SET mzys='" + mzys + "'  WHERE mzjldid ='" + mzjld + "' ";
            return dBConn.ExecuteNonQuery(SQL11);
        }
        public int UpdatePACU_zxhs(int mzjld, string zxhs)
        {
            string SQL11 = "UPDATE Adims_PACU_SZ  SET zxhs='" + zxhs + "'  WHERE mzjldid ='" + mzjld + "' ";
            return dBConn.ExecuteNonQuery(SQL11);
        }
       
        public DataTable GetjhxmPACU(int mzid) /// 获取PACU监护项目值
        {
            string sql = "SELECT * FROM Adims_jhxm_PACU WITH (NOLOCK) WHERE mzjldid='" + mzid + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetqtPACU(int mzid) /// 获取PACU气体
        {
            string sql = "SELECT * FROM Adims_PACU_qtUse WITH (NOLOCK) WHERE mzjldid='" + mzid + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetsxPACU(int mzid) /// 获取PACU输血
        {
            string sql = "SELECT * FROM Adims_shuxueUSE_PACU WITH (NOLOCK) WHERE mzjldid='" + mzid + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetsyPACU(int mzid) /// 获取PACU输血
        {
            string sql = "SELECT * FROM Adims_shuyeUSE_PACU WITH (NOLOCK) WHERE mzjldid='" + mzid + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetclPACU(int mzid) /// 获取PACU出尿
        {
            string sql = "SELECT * FROM Adims_PACU_cl WITH (NOLOCK) WHERE mzjldid='" + mzid + "'";
            return dBConn.GetDataTable(sql);
        }
       
        public int deletePACUsj(int id)
        {
            string delete = "delete from Adims_PACU_shijian where id='" + id + "' ";
            return dBConn.ExecuteNonQuery(string.Format(delete));
        }
        public DataTable GetPACUsj(int mzjldid)
        {
            string select = "select * from Adims_PACU_shijian where mzjldid='" + mzjldid + "'ORDER BY TIME ASC";
            return dBConn.GetDataTable(string.Format(select));
        }
        public int addPACUyy(int mzjldid, adims_MODEL.tsyy ts)
        {
            return dBConn.ExecuteNonQuery("INSERT INTO Adims_PACU_yyUse(mzjldid,yyname,yl,dw,yyfs,yytime) "
                + "values('" + mzjldid + "','" + ts.Name + "','" + ts.Yl + "','" + ts.Dw + "','" + ts.Yyfs + "','" + ts.D + "')");
        }
        public int deletePACUyy(int id)
        {
            string delete = "delete from Adims_PACU_yyUse where id='" + id + "' ";
            return dBConn.ExecuteNonQuery(string.Format(delete));
        }
        public DataTable GetPACUyy(int mzjldid)
        {
            string select = "select * from Adims_PACU_yyUse where mzjldid='" + mzjldid + "' ORDER BY YYTIME ASC";
            return dBConn.GetDataTable(string.Format(select));
        }

    }
        
}
