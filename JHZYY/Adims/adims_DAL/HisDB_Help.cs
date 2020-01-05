using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace adims_DAL
{
    public class HisDB_Help
    {
        #region <<Members>>

        private static readonly string connStr = ConfigurationManager.AppSettings["ConnectionStringHis"];

        private static readonly string connStr1 = ConfigurationManager.AppSettings["ConnectionStringHisHospital"];

        #endregion

        #region <<Methods>>

        /// <summary>
        /// 返回DataSet
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// 
        public DataSet GetDataSet(string sql)
        {
            try
            {
                SqlConnection con = new SqlConnection(connStr);
                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void TestConnectDB()
        {
            SqlConnection myConn = new SqlConnection(connStr);
            try
            {
                myConn.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("连接数据库失败！错误原因：" + ex.Message);
            }
            finally
            {
                myConn.Close();
            }
        }
        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql)
        {
            try
            {
                SqlConnection con = new SqlConnection(connStr);
                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 返回DataTable（麻醉用药计费）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public DataTable GetDataTable1(string sql)
        {
            try
            {
                SqlConnection con = new SqlConnection(connStr1);
                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 增删改、返回影响行数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql)
        {
            try
            {
                int i = 0;
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    i = cmd.ExecuteNonQuery();
                }
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 增删改、返回影响行数（麻醉用药计费）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int ExecuteNonQuery1(string sql)
        {
            try
            {
                int i = 0;
                using (SqlConnection con = new SqlConnection(connStr1))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    i = cmd.ExecuteNonQuery();
                }
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 返回第一行第一列
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public object ExecuteScalar(string sql)
        {
            try
            {
                object obj = null;
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    obj = cmd.ExecuteScalar();
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        public DataTable GetXM()
        {
            string sql = @"SELECT [FItemID]
      ,[item_code]
      ,[item_name]
      ,[item_spec]
      ,[unit]
      ,[item_type]
      ,[type_inpatient_receipt]
      ,[type_on_an]
      ,[ybbz]
      ,[jiage]
      ,[FSpell]
  FROM [hospital].[dbo].[V_Operation_Interface_Item] ";
            return this.GetDataTable1(sql);
        }
        /// <summary>
        /// 按条件查询 distinct item_name
        /// </summary>
        /// <param name="yptype">药品类别</param>
        /// <param name="name">项目名称</param>
        /// <returns></returns>
        public DataTable GetAdims_SelectMZYY(string yptype, string name)
        {
            string sql = "SELECT * FROM [hospital].[dbo].[V_Operation_Interface_Item] where [type_on_an]='" + yptype + "'and item_name= '" + name + "'";
            return this.GetDataTable1(sql);
        }
        /// <summary>
        /// 查全部
        /// </summary>
        /// <param name="yptype"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataTable GetAdims_SelectMZYYALL(string yptype, string name)
        {
            string sql = "SELECT * FROM [hospital].[dbo].[V_Operation_Interface_Item_all] where [type_on_an]='" + yptype + "'and item_name= '" + name + "'";
            return this.GetDataTable1(sql);
        }
        public DataTable GetAdims_SelectrowsMZYY(string yptype, string name,string GG)
        {
            string sql = "SELECT * FROM [hospital].[dbo].[V_Operation_Interface_Item] where [type_on_an]='" + yptype + "'and item_name= '" + name + "' and item_spec='" + GG + "'";
            return this.GetDataTable1(sql);
        }
        public DataTable GetAdims_SelectrowsMZYYDW(string yptype, string name, string GG,string DW)
        {
            string sql = "SELECT * FROM [hospital].[dbo].[V_Operation_Interface_Item] where [type_on_an]='" + yptype + "'and item_name= '" + name + "' and item_spec='" + GG + "' and unit='" + DW + "'";
            return this.GetDataTable1(sql);
        }
        public DataTable GetAdims_SelectrowsMZYYDWALL(string yptype, string name, string GG, string DW)
        {
            string sql = "SELECT * FROM [hospital].[dbo].[V_Operation_Interface_Item_all] where [type_on_an]='" + yptype + "'and item_name= '" + name + "' and item_spec='" + GG + "' and unit='" + DW + "'";
            return this.GetDataTable1(sql);
        }
        public DataTable GetAdims_SelectrowsMZYYALL(string yptype, string name, string GG)
        {
            string sql = "SELECT * FROM [hospital].[dbo].[V_Operation_Interface_Item_all] where [type_on_an]='" + yptype + "'and item_name= '" + name + "' and item_spec='" + GG + "'";
            return this.GetDataTable1(sql);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="yptype"></param>
        /// <returns></returns>
        public DataTable GetAdims_SelectLB(string yptype)
        {
            string sql = "SELECT distinct item_name  FROM [hospital].[dbo].[V_Operation_Interface_Item] where [type_on_an]='" + yptype + "'";
            return this.GetDataTable1(sql);
        }
        /// <summary>
        /// 查询全部要
        /// </summary>
        /// <param name="yptype"></param>
        /// <returns></returns>
        public DataTable GetAdims_SelectLBALL(string yptype)
        {
            string sql = "SELECT distinct item_name  FROM [hospital].[dbo].[V_Operation_Interface_Item_all] where [type_on_an]='" + yptype + "'";
            return this.GetDataTable1(sql);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="yptype">药品类别</param>
        /// <param name="name">项目名称</param>
        /// <returns></returns>
        public DataTable GetAdims_SelectGG(string yptype, string name)
        {
            string sql = "SELECT distinct [item_spec] FROM [hospital].[dbo].[V_Operation_Interface_Item] where [type_on_an]='" + yptype + "'and item_name= '" + name + "'";
            return this.GetDataTable1(sql);
        }
        public DataTable GetAdims_SelectGGALL(string yptype, string name)
        {
            string sql = "SELECT distinct [item_spec] FROM [hospital].[dbo].[V_Operation_Interface_Item_all] where [type_on_an]='" + yptype + "'and item_name= '" + name + "'";
            return this.GetDataTable1(sql);
        }
        public DataTable GetAdims_SelectDW(string yptype, string name,string GG)
        {
            string sql = "SELECT distinct [unit] FROM [hospital].[dbo].[V_Operation_Interface_Item] where [type_on_an]='" + yptype + "'and item_name= '" + name + "' and [item_spec]='" + GG + "'";
            return this.GetDataTable1(sql);
        }
        public DataTable GetAdims_SelectDWALL(string yptype, string name, string GG)
        {
            string sql = "SELECT distinct [unit] FROM [hospital].[dbo].[V_Operation_Interface_Item_all] where [type_on_an]='" + yptype + "'and item_name= '" + name + "' and [item_spec]='" + GG + "'";
            return this.GetDataTable1(sql);
        }
        public DataTable JianSuoType(string yptype, string namesx)
        {
            string sql = "select   distinct item_name  FROM [hospital].[dbo].[V_Operation_Interface_Item] where [type_on_an]='" + yptype + "' and FSpell like'" + namesx + "%'";
            return this.GetDataTable1(sql);
        }
        /// <summary>
        /// 全部
        /// </summary>
        /// <param name="yptype"></param>
        /// <param name="namesx"></param>
        /// <returns></returns>
        public DataTable JianSuoTypeALL(string yptype, string namesx)
        {
            string sql = "select   distinct item_name  FROM [hospital].[dbo].[V_Operation_Interface_Item_all] where [type_on_an]='" + yptype + "' and FSpell like'" + namesx + "%'";
            return this.GetDataTable1(sql);
        }
        public DataTable JianSuoType(string LB)
        {
            string sql = "select   distinct item_name  FROM [hospital].[dbo].[V_Operation_Interface_Item] where [type_on_an]='" + LB + "'";
            return this.GetDataTable1(sql);
        }
        public DataTable JianSuoTypeALL(string LB)
        {
            string sql = "select   distinct item_name  FROM [hospital].[dbo].[V_Operation_Interface_Item_all] where [type_on_an]='" + LB + "'";
            return this.GetDataTable1(sql);
        }
        /// <summary>
        /// 查询药品分类
        /// </summary>
        /// <returns></returns>
        public DataTable GetYP()
        {
            string sql = "select DISTINCT type_on_an  FROM [hospital].[dbo].[V_Operation_Interface_Item] ";
            return this.GetDataTable1(sql);
        }
        public DataTable GetLB()
        {
            string sql = @"SELECT [number]
      ,[an_cost_type_code]
      ,[an_cost_type_name]
      ,[spell_header]
  FROM [hospital].[basic].[an_cost_type_list]";
            return this.GetDataTable1(sql);
        }
        /// <summary>
        /// 查询开单科室
        /// </summary>
        /// <returns></returns>
        public DataTable GetKS() 
        {
            string sql = "select [FItemID],[office_code],[office_name],[spell_header]  FROM [hospital].[basic].[office_list] ";
            return this.GetDataTable1(sql);
        }
        public DataTable GetHisInfo(string dtime)
        {//convert(nvarchar(5),otime,24)as starttime,
            string sql = "select patID ,ZhuYuanNo,patName,CardID,patAge,patSex,patNation,BedNo,Patdpm,PatBingqu,SQZD,Oname,otime,OS,OS1,OS2,mzff,BX, TW, GR, BZ,Ocode,CFSS,SSLX,SSLB,QKDJ,sqys,zrys,osdm,zycs,expertName"
            + " from V_EMR_Operation_Interface where Convert(varchar,Otime,23)='" + dtime + "'";
            return  this.GetDataTable(sql);
        }
        public DataTable GetHisInfoByPatID(string PATID)
        {//convert(nvarchar(5),otime,24)as starttime,
            //string sql = "select patID ,ZhuYuanNo,patName,CardID,patAge,patSex,patNation,BedNo,Patdpm,PatBingqu,SQZD,Oname,otime,OS,mzff,BX, TW, GR, BZ "
            //+ " from V_EMR_Operation_Interface WHERE PATID LIKE '%" + PATID + "'";
            string sql = "select [BRBH],[BRXM],[BRXB],[dmmc],[RYNL],[CWHM],[ksmc] from V_ZY_BRXX_SM where BRBH='" + PATID + "'";
            return this.GetDataTable(sql);
        }
        public DataTable GetHisZhenduan(string sxname)
        {
            string sql = "SELECT FName,FSpell FROM V_EMR_Operation_Interface_Diag_list where FSpell like'" + sxname + "%'";
            return  this.GetDataTable(sql);
        }
         public DataTable GetHisShoushu(string sxname)
        {
            string sql = "SELECT FName as 手术名称,flevel as 手术等级 FROM V_EMR_Operation_Interface_operation_list where FSpell like'" + sxname + "%'";
            return  this.GetDataTable(sql);
        }

       
    }
}
