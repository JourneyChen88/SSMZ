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
         HisDB_Help his = new HisDB_Help();
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
        #region 麻醉用药组套
        /// <summary>
        /// 添加项目表
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertMZYYXM(List<string> list1)
        {
            string _INSERT = @"INSERT INTO [HeYiAMIS_CJ].[dbo].[Adims_MZYYXM]
           ([FItemID]
           ,[item_code]
           ,[item_name]
           ,[item_spec]
           ,[unit]
           ,[item_type]
           ,[type_inpatient_receipt]
           ,[type_on_an]
           ,[ybbz]
           ,[jiage]
           ,[FSpell])"
                + "values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')";
            string INSERT = string.Format(_INSERT, list1.ToArray());
            return dBConn.ExecuteNonQuery(INSERT);
        }
        /// <summary>
        /// 添加开单科室
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertMZYYKS(List<string> list1)
        {
            string _INSERT = @"INSERT INTO [HeYiAMIS_CJ].[dbo].[Adims_MZYYKDKS]([FItemID],[office_code],[KSname],[spell_header])"
                + "values('{0}','{1}','{2}','{3}')";
            string INSERT = string.Format(_INSERT, list1.ToArray());
            return dBConn.ExecuteNonQuery(INSERT);
        }
        /// <summary>
        /// 查询类别
        /// </summary>
        /// <param name="FItemID"></param>
        /// <returns></returns>
        public DataTable GetSelectLB(string FItemID)
        {
            string sql = "SELECT * FROM Adims_MZYYLB  where id='" + FItemID + "'";
            return dBConn.GetDataTable(sql);
        }
        public int InsertLB(List<string> list1)
        {
            string _INSERT = @"INSERT INTO [HeYiAMIS_CJ].[dbo].[Adims_MZYYLB]( [id]
      ,[LB_code]
      ,[LBname]
      ,[spell_header])"
                + "values('{0}','{1}','{2}','{3}')";
            string INSERT = string.Format(_INSERT, list1.ToArray());
            return dBConn.ExecuteNonQuery(INSERT);
        }
        /// <summary>
        /// 查询科室
        /// </summary>
        /// <param name="FItemID"></param>
        /// <returns></returns>
        public DataTable GetSelectKS(string FItemID)
        {
            string sql = "SELECT * FROM Adims_MZYYKDKS  where FItemID='" + FItemID + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetSelectYYKS()
        {
            string sql = "SELECT DISTINCT KSname FROM Adims_MZYYKDKS";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetSelectSXKS(string SX)
        {
            string sql = "SELECT DISTINCT KSname FROM Adims_MZYYKDKS where spell_header like'" + SX + "%'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 查询麻醉用药计费组套
        /// </summary>
        /// <returns></returns>
        public DataTable GetSelectMZZTtype() 
        {
            string sql = "SELECT * FROM Adims_MZYYZTType";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 查询计费
        /// </summary>
        /// <returns></returns>
        public DataTable GetSelectMZJF(string ZYH,string mzid)
        {
            string sql = @"SELECT id,[expense_item_class],[item_name],[item_spec],[signer],[amount],[dj],[sl]
       ,[expense]
      ,[charges]
      ,[open_office]
      ,[exec_office] 
      ,[zycost]      
  FROM Adims_MZYYJ where illman_id='" + ZYH + "'and mzid='"+mzid+"' order by [expense_item_class] desc";
            return dBConn.GetDataTable(sql);
        }
         /// <summary>
         /// 查询his代码
         /// </summary>
         /// <param name="uid"></param>
         /// <param name="user_name"></param>
         /// <returns></returns>
        public DataTable GetSelectHisCode(string uid,string user_name)
        {
            string sql = @"SELECT [id]
      ,[uid]
      ,[password]
      ,[user_name]
      ,[position]
      ,[type]
      ,[hisCode]
  FROM [HeYiAMIS_CJ].[dbo].[Adims_User] where [uid]='" + uid + "' and [user_name]='" + user_name + "'";
            return dBConn.GetDataTable(sql);
        }
        
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public int GetUpdateMZZJF(string SQLname,string name,string id)
        {
            string sql = "update Adims_MZYYJ set " + SQLname + "='" + name + "'  where id='" + id + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 修改规格和单位
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="GG">规格</param>       
        /// <param name="DW">单位</param>
        /// <param name="DJ">单价</param>
        /// <param name="Sl">默认为1</param>
        /// <returns></returns>
        public int UpdateMZZJF(string id, string XMMC, string GG, string DW, string DJ, string item_code, string Yzbz, string FitemID, string FBatchID)
        {
            string sql = "update Adims_MZYYJ set item_name='" + XMMC + "', item_spec='" + GG + "',Signer='" + DW + "', DJ='" + DJ + "',item_code='" + item_code + "',Yzbz='" + Yzbz + "',FitemID='" + FitemID + "',FBatchID='" + FBatchID + "',Amount=null, Expense=null, Charges=null  where id='" + id + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 修改数量
        /// </summary>
        /// <param name="id"></param>
        /// <param name="SL"></param>
        /// <param name="Expense"></param>
        /// <param name="Charges"></param>
        /// <returns></returns>
        public int UpdateMZZJFS(string id, string SL,string Expense, string Charges)
        {
            string sql = "update Adims_MZYYJ set Amount='" + SL + "', Expense='" + Expense + "', Charges='" + Charges + "'  where id='" + id + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除麻醉计费
        /// </summary>
        /// <returns></returns>
        public int DeleteMZZJF(string id)
        {
            string sql = "delete Adims_MZYYJ  where id='" + id + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 类别查询
        /// </summary>
        /// <returns></returns>
        public DataTable GetSelectMZZLB(string name)
        {
            string sql = "SELECT * FROM Adims_MZYYLB where LBname='"+name+"'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 类别查询
        /// </summary>
        /// <returns></returns>
        public DataTable GetSelectMZZLB()
        {
            string sql = "SELECT DISTINCT LBname FROM Adims_MZYYLB";
            return dBConn.GetDataTable(sql);
        }
        ///// <summary>
        ///// 查询麻醉用药计费组套
        ///// </summary>
        ///// <returns></returns>
        //public DataTable GetSelectMZZT(string type)
        //{
        //    string sql = @"SELECT [id] ,[ZT_id],[LB],[XMMC],[GG] ,[DW],[SL],[DJ],[F],[YSFY],[SSFY],[KDKS] ,[ZXKS],[ZYFYLB],[item_code],[ybbz],[FSpell] FROM [HeYiAMIS_CJ].[dbo].[Adims_MZYYZT]";
        //    return dBConn.GetDataTable(sql);
        //}
        /// <summary>
        /// 查询麻醉用药类别
        /// </summary>
        /// <returns></returns>
        public DataTable GetSelectMZtype()
        {
            string sql = "SELECT DISTINCT item_type FROM Adims_MZYYXM";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetAdims_JianSuoType(string yptype, string namesx)
        {
            string sql = "SELECT * FROM Adims_MZYYXM where item_type='" + yptype + "' and FSpell like'" + namesx + "%' order by FSpell ";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetAdims_SelectKS(string name)
        {
            string sql = "SELECT * FROM Adims_MZYYKDKS where KSname='" + name + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <param name="yptype">药品类别</param>
        /// <param name="name">项目名称</param>
        /// <returns></returns>
        public DataTable GetAdims_SelectMZYY(string yptype, string name)
        {
            string sql = "SELECT * FROM Adims_MZYYXM where item_type='" + yptype + "'and item_name= '" + name + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <param name="yptype">药品类别</param>
        /// <param name="name">项目名称</param>
        /// <returns></returns>
        public DataTable GetAdims_SelectJF(string ZYH)
        {
            string sql = @"SELECT count(*) from Adims_MZYYJ where illman_id='" + ZYH + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="ZYH"></param>
        /// <returns></returns>
        public DataTable GetAdims_SelectJFID(string Id)
        {
            string sql = @"SELECT * from Adims_MZYYJ where Id='" + Id + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 添加一行
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int AddMZYYJF(Dictionary<string, string> dictionary)
        {
            string _INSERT = @"INSERT INTO Adims_MZYYJ([illman_id],[this_id] ,[expense_item_no],[billing_datetime],[operator_no],[sl],[doctor],[FIsIns],[mzid])"
                + "values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')";
            return dBConn.ExecuteNonQuery(string.Format(_INSERT, dictionary.Values.ToArray()));
        }
        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <param name="yptype">药品类别</param>
        /// <param name="name">项目名称</param>
        /// <returns></returns>
        public DataTable GetAdims_SelectYYJF(string name)
        {
            string sql = @"SELECT m.id,m.[LB],m.[XMMC],m.[GG] ,m.[DW],m.[SL],m.[DJ],m.[F],m.[YSFY],m.[SSFY],m.[KDKS],
m.[ZXKS],m.[ZYFYLB],m.[item_code],m.[ybbz],m.[FSpell],m.[expense_item_class],m.[FItemID],m.[FBatchID] from Adims_MZYYZT as m LEFT OUTER JOIN Adims_MZYYZTType as b
on m.ZT_id=b.id where b.name='" + name + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <param name="yptype">药品类别</param>
        /// <param name="name">项目名称</param>
        /// <returns></returns>
        public DataTable GetAdims_SelectDGV(string name )
        {
            string sql = @"SELECT m.id,m.[LB],m.[XMMC],m.[GG] ,m.[DW],m.[SL],m.[DJ],m.[F],m.[YSFY],m.[SSFY],m.[KDKS],
m.[ZXKS],m.[ZYFYLB] from Adims_MZYYZT as m LEFT OUTER JOIN Adims_MZYYZTType as b
on m.ZT_id=b.id where b.name='" + name + "' order by m.[LB] desc";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 麻醉用药计费组套信息保存
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertMZYYZT(Dictionary<string, string> dictionary)
        {
            string _INSERT = @"INSERT INTO [HeYiAMIS_CJ].[dbo].[Adims_MZYYZT]([ZT_id],[LB],[XMMC],[GG] ,[DW],[SL],[DJ],[F],[YSFY],[SSFY],[KDKS],[ZXKS],[ZYFYLB],[item_code],[ybbz],[FSpell],[expense_item_class],[FItemID],[FBatchID])"
                + "values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')";
            return dBConn.ExecuteNonQuery(string.Format(_INSERT, dictionary.Values.ToArray()));
        }
        /// <summary>
        /// 麻醉用药计费组套信息保存
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertMZYYJF(Dictionary<string, string> dictionary)
        {
            string _INSERT = @"INSERT INTO Adims_MZYYJ([illman_id],[this_id] ,[expense_item_no],[expense_item_class],[item_name],[item_code],[item_spec],[amount],[signer],[open_office],[exec_office],[expense]
,[charges],[billing_datetime],[operator_no],[dj],[sl],[zycost],[ybbz],[FItemID],[FBatchID],[doctor],[FIsIns],[mzid])"
                + "values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}')";
            return dBConn.ExecuteNonQuery(string.Format(_INSERT, dictionary.Values.ToArray()));
        }
        /// <summary>
        /// 保存到麻醉药房
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertMZYYJFID(Dictionary<string, string> dictionary)
        {
            string _INSERT = @"INSERT INTO [hospital].[zycost].[illman_expense_detail]([illman_id],[this_id],[expense_item_no],[expense_item_class],[item_name],[item_code],[item_spec],[amount],[signer]
,[open_office],[exec_office],[expense],[charges],[billing_datetime],[operator_no],[rcpt_no],[dj],[sl],[zycost],[yzbz],[zzy],[zzsj]
,[ybbz],[total_flag],[yizhu_no],[txm_bs],[FItemID],[FBatchID],[FInPrice],[doctor] ,[FIsIns],[fopssource],[fopscheduleid])"
                + "values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}')";
            return his.ExecuteNonQuery1(string.Format(_INSERT, dictionary.Values.ToArray()));
        }
        /// <summary>
        /// 麻醉用药计费组套信息删除
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int DelectMZYYZT(int id)
        {
            string _INSERT = "DELETE FROM [HeYiAMIS_CJ].[dbo].[Adims_MZYYZT] WHERE  id=" + id + "";
            return dBConn.ExecuteNonQuery(_INSERT);
        }
        #endregion
        #region 事件管理
        public DataTable GetAdims_szsjListInt(string name) /// 
        {
            string sql = "SELECT id FROM Adims_szsjList  WHERE  name='" + name + "'";
            return dBConn.GetDataTable(sql);
        }
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

        public DataTable GetAdims_szsjListBySuoxie(string suoxie) /// 
        {
            string sql = "SELECT name FROM Adims_szsjList  WHERE  Suoxie like'" + suoxie + "%'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetAdims_szsjListByType(string name,string type) /// 
        {
            string sql = "SELECT * FROM Adims_szsjList  WHERE name='"+name+"'and type='" + type + "'";
            return dBConn.GetDataTable(sql);
        }
        public int InsertAdims_szsjList(string name,string suoxie, string type) /// 
        {
            string sql = "INSERT INTO Adims_szsjList(name,suoxie,type) VALUES ('" + name + "','" + suoxie + "','" + type + "')";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int deleteAdims_szsjList(int id)
        {
            string delete = "delete from Adims_szsjList where id='" + id + "' ";
            return dBConn.ExecuteNonQuery(string.Format(delete));
        }
        public int UpdateAdims_szsjList(string name, string suoxie, string type, int id)
        {
            string SQL11 = "UPDATE Adims_szsjList  SET name='" + name + "',suoxie ='" + suoxie + "',type ='" + type + "'  WHERE id ='" + id + "' ";
            return dBConn.ExecuteNonQuery(SQL11);
        }
        #endregion
        #region 用量单位
         public DataTable GetAdims_DW() 
        {
            string sql = "SELECT * FROM YLDW ";
            return dBConn.GetDataTable(sql);
        }
        #endregion
         #region 用药方式
         public DataTable GetAdims_fs()
         {
             string sql = "SELECT * FROM YYFS ";
             return dBConn.GetDataTable(sql);
         }
         #endregion
        #region//药品管理
        public DataTable GetAdims_YaoPinBag(string name, string bag) /// 
        {
            string sql = "SELECT * FROM Adims_YaoPinBag  WHERE ypname='" + name + "'and bagname='" + bag + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetAdims_YaoPin(string name,string type) /// 
        {
            string sql = "SELECT * FROM Adims_YaoPin  WHERE ypname='" + name + "' and ypType='"+type+"'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetAdims_YaoPinByType(string yptype,string namesx) ///
        {
            string sql = "SELECT * FROM Adims_YaoPin where yptype='" + yptype + "'and namesx like'" + namesx + "%' order by namesx ";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetYaoPinType() ///
        {
            string sql = "SELECT name FROM YaoPinType";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetAdims_YaoPinByType(string namesx) ///
        {
            string sql = "SELECT * FROM Adims_YaoPin where yptype like'" + namesx + "%' order by namesx ";
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
        public int InsertAdims_YaoPin(string name,string namesx, string type) /// 
        {
            string sql = "INSERT INTO Adims_YaoPin(ypname,namesx,yptype) VALUES ('" + name + "','" + namesx + "','" + type + "')";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int InsertAdims_YaoPinBag(string bagname, string ypname, string yl, string dw, string zrff,int cxyy) /// 
        {
            string sql = "INSERT INTO Adims_YaoPinBag(bagname,ypname,yl,dw,zrff,cxyy) VALUES ('" + bagname + "','" + ypname + "','" + yl + "','" + dw + "','" + zrff + "','" + cxyy + "')";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int UpdateAdims_YaoPin(string name,string suoxie, string type,int id)
        {
            string SQL11 = "UPDATE Adims_YaoPin  SET ypname='" + name + "',namesx ='" + suoxie + "',yptype ='" + type + "'  WHERE id ='" + id + "' ";
            return dBConn.ExecuteNonQuery(SQL11);
        }
        #endregion

        public int DeleteMazuifangfa(int id) //删除科室名称
        {
            string sql = "delete from keshi where id='" + id + "'";
            return dBConn.ExecuteNonQuery(string.Format(sql));
        }
        public int AddMazuifangfa(string name) //添加科室名称
        {
            string sql = "insert into keshi values('" + name + "')";
            return dBConn.ExecuteNonQuery(string.Format(sql));
        }
        public int UpdateMazuifangfa(int id, string name) //修改科室名称
        {
            string sql = "update keshi set name='" + name + "' where id='" + id + "'";
            return dBConn.ExecuteNonQuery(string.Format(sql));
        }
        public DataTable SelectMazuifangfa() //查询科室名称
        {
            string sql = "select * from keshi";
            return dBConn.GetDataTable(string.Format(sql));
        }
        public DataTable SelectNameMazuifangfa(string name) //查询科室名称
        {
            string sql = "select * from keshi where name='" + name + "'";
            return dBConn.GetDataTable(string.Format(sql));
        }

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
