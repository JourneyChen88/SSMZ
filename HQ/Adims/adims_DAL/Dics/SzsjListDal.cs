using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL
{
    public class SzsjListDal
    {
        private DBConn dBConn = new DBConn();

     
        public DataTable GetSzsjListByName(string name) /// 
        {
            string sql = "SELECT * FROM Adims_szsjList  WHERE  name='" + name + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetSzsjListDistinct() /// 
        {
            string sql = "SELECT distinct TYPE FROM Adims_szsjList ";
            return dBConn.GetDataTable(sql);
        }
       
        public DataTable GetSzsjListByType(string type) /// 
        {
            string sql = "SELECT * FROM Adims_szsjList  WHERE  type='" + type + "'";
            return dBConn.GetDataTable(sql);
        }

        public DataTable GetSzsjListBySuoxie(string suoxie) /// 
        {
            string sql = "SELECT name FROM Adims_szsjList  WHERE  Suoxie like'" + suoxie + "%'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetSzsjListByNameAndType(string name, string type) /// 
        {
            string sql = "SELECT * FROM Adims_szsjList  WHERE name='" + name + "'and type='" + type + "'";
            return dBConn.GetDataTable(sql);
        }
        public int InsertSzsjList(string name, string suoxie, string type) /// 
        {
            string sql = "INSERT INTO Adims_szsjList(name,suoxie,type) VALUES ('" + name + "','" + suoxie + "','" + type + "')";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int DeleteSzsjList(int id)
        {
            string delete = "delete from Adims_szsjList where id='" + id + "' ";
            return dBConn.ExecuteNonQuery(string.Format(delete));
        }
        public int UpdateSzsjList(string name, string suoxie, string type, int id)
        {
            string SQL11 = "UPDATE Adims_szsjList  SET name='" + name + "',suoxie ='" + suoxie + "',type ='" + type + "'  WHERE id ='" + id + "' ";
            return dBConn.ExecuteNonQuery(SQL11);
        }
       
    }
}
