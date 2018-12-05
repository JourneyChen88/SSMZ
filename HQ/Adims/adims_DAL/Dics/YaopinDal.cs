using adims_MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL
{
    public class YaopinDal
    {
        private DBConn dBConn = new DBConn();

      
        /// <summary>
        /// 获取药品包
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bag"></param>
        /// <returns></returns>
        public DataTable GetYaoPinBag(string name, string bag) /// 
        {
            string sql = "SELECT * FROM Adims_YaoPinBag  WHERE ypname='" + name + "'and bagname='" + bag + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 根据包名字获取药品包
        /// </summary>
        /// <param name="bag"></param>
        /// <returns></returns>
        public DataTable GetYaoPinBagByBagName(string bag) /// 
        {
            string sql = "SELECT * FROM Adims_YaoPinBag  WHERE  bagname='" + bag + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 根据药品名获取药品
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataTable GetYaoPinByName(string name) /// 
        {
            string sql = "SELECT * FROM Adims_YaoPin  WHERE ypname='" + name + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 根据多个药品类型获取药品
        /// </summary>
        /// <param name="yptype"></param>
        /// <param name="namesx"></param>
        /// <returns></returns>
        public DataTable GetYaoPinByTypeList(string yptype, string namesx) ///
        {
            string sql = "SELECT * FROM Adims_YaoPin where (yptype='" + yptype + "'or yptype='局麻药') and namesx like'" + namesx + "%' order by namesx ";
            return dBConn.GetDataTable(sql);
        }

        /// <summary>
        /// 根据单个药品类型获取药品
        /// </summary>
        /// <param name="yptype"></param>
        /// <param name="namesx"></param>
        /// <returns></returns>
        public DataTable GetYaoPinByType(string yptype, string namesx) ///
        {
            string sql = "SELECT * FROM Adims_YaoPin where yptype='" + yptype + "' and namesx like'" + namesx + "%' order by namesx ";
            return dBConn.GetDataTable(sql);
        }

        /// <summary>
        /// 获取药品类型字典
        /// </summary>
        /// <returns></returns>
        public DataTable GetYaoPinType() ///
        {
            string sql = "SELECT name FROM YaoPinType";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 根据药品缩写获取药品 
        /// </summary>
        /// <param name="namesx"></param>
        /// <returns></returns>
        public DataTable GetYaoPinByPinyin(string namesx) ///
        {
            string sql = "SELECT * FROM Adims_YaoPin where namesx like'" + namesx + "%' order by namesx ";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 根据包名字获取药品包
        /// </summary>
        /// <param name="bagname"></param>
        /// <returns></returns>
        //public DataTable GetYongYaoBagByBagName(string bagname) ///
        //{
        //    string sql = "SELECT ypname,yl,dw,zrff,cxyy FROM Adims_YaoPinBag where bagname='" + bagname + "' ";
        //    return dBConn.GetDataTable(sql);
        //}
        public DataTable GetYongYaoBagByBagName(string bagname) ///
        {
            string sql = "SELECT * FROM Adims_YaoPinBag where bagname='" + bagname + "' ";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 获取所有药品包
        /// </summary>
        /// <returns></returns>
        public DataTable GetYaopinBagAll() ///
        {
            string sql = "SELECT DISTINCT BAGNAME FROM Adims_YaoPinBag ";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 获取所有药品
        /// </summary>
        /// <returns></returns>
        public DataTable GetYaoPinAll() ///
        {
            string sql = "SELECT * FROM Adims_YaoPin ";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 删除药品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteYaoPin(int id)
        {
            string delete = "delete from Adims_YaoPin where id='" + id + "' ";
            return dBConn.ExecuteNonQuery(string.Format(delete));
        }
        /// <summary>
        /// 删除药品包
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteYaoPinBag(int id)
        {
            string delete = "delete from Adims_YaoPinBag where id='" + id + "' ";
            return dBConn.ExecuteNonQuery(string.Format(delete));
        }

        /// <summary>
        /// 插入药品
        /// </summary>
        /// <param name="name"></param>
        /// <param name="namesx"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int InsertYaoPin(string name, string namesx, string type) /// 
        {
            string sql = "INSERT INTO Adims_YaoPin(ypname,namesx,yptype) VALUES ('" + name + "','" + namesx + "','" + type + "')";
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 插入药品包
        /// </summary>
        /// <param name="bagname"></param>
        /// <param name="ypname"></param>
        /// <param name="yl"></param>
        /// <param name="dw"></param>
        /// <param name="zrff"></param>
        /// <param name="cxyy"></param>
        /// <returns></returns>
        public int InsertYaoPinBag(string bagname, string ypname, string yl, string dw, string zrff, int cxyy) /// 
        {
            string sql = "INSERT INTO Adims_YaoPinBag(bagname,ypname,yl,dw,zrff,cxyy) VALUES ('" + bagname + "','" + ypname + "','" + yl + "','" + dw + "','" + zrff + "','" + cxyy + "')";
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 修改药品
        /// </summary>
        /// <param name="name"></param>
        /// <param name="suoxie"></param>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateYaoPin(string name, string suoxie, string type, int id)
        {
            string SQL11 = "UPDATE Adims_YaoPin  SET ypname='" + name + "',namesx ='" + suoxie + "',yptype ='" + type + "'  WHERE id ='" + id + "' ";
            return dBConn.ExecuteNonQuery(SQL11);
        }
      
    }
}
