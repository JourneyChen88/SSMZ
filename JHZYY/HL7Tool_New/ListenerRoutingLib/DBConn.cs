
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using SqlSugar;

namespace ListenerRoutingLib
{
    public class DBConn
    {

        private static readonly string connStr = ConfigurationManager.AppSettings["ConnectionString"];
        SqlSugarClient db = new SqlSugarClient(
                                new ConnectionConfig()
                                {
                                    ConnectionString = connStr,
                                    DbType = SqlSugar.DbType.SqlServer,//设置数据库类型
                                    IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                                    InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
                                });
        #region <<Methods>>


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
        public int InsertOperDic(OperDicModel dic)
        {
            string sql = $@" INSERT INTO OperDic_NEW(OperName, OperCode, Operlevel, CutType, QuickInput)
                            VALUES('{dic.OperName}', '{dic.OperCode}','{dic.Operlevel}', '{dic.CutType}', '{dic.QuickInput}' )";
            SaveLog(sql);
            return ExecuteNonQuery(sql);
        }

        public int InsertPaiban(OTypesetting pb)
        {
            return db.Insertable(pb).ExecuteReturnIdentity();

        }
        /// <summary>
        /// 修改排班所有信息
        /// </summary>
        /// <param name="pb"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public int UpdatePaiban(OTypesetting pb)
        {
            return db.Updateable(pb).ExecuteCommand();


        }
        /// <summary>
        /// 修改排版状态
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="PATID"></param>
        /// <returns></returns>
        public int UpdatePaibanOstate(string zhuyuanid, int Ostate)
        {
            string SQL_PAIBAN = $@"Update Adims_OTypesetting set ostate={Ostate} where PatZhuYuanID='{ zhuyuanid }'";
            string INSERT = string.Format(SQL_PAIBAN);
            SaveLog(INSERT);
            return ExecuteNonQuery(INSERT);
        }

        public OTypesetting GetPaiban(string PatZhuYuanID)
        {
            return db.Queryable<OTypesetting>().Where(a => a.PatZhuYuanID == PatZhuYuanID).First();

        }
        public static void SaveLog(string s)
        {
            try
            {
                string fileFolder = AppDomain.CurrentDomain.BaseDirectory.ToString() + "log";
                if (!Directory.Exists(fileFolder))
                {
                    Directory.CreateDirectory(fileFolder);
                }
                string filePath = fileFolder + "\\" + GetLogfile();
                FileStream fs;
                if (!File.Exists(filePath))
                {
                    fs = File.Create(filePath);
                }
                else
                {
                    fs = File.Open(filePath, FileMode.Append);
                }
                string strToWrite = "\r\n" + System.DateTime.Now.ToString() + "\r\n" + s + "\r\n";
                byte[] b = System.Text.Encoding.Default.GetBytes(strToWrite);
                fs.Write(b, 0, b.Length);
                fs.Close();
            }
            catch
            { }
        }
        //获取日志的名称，按天
        public static string GetLogfile()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("");
            string date = System.DateTime.Today.ToString("yyyy-MM-dd");
            sb.Append(date);
            sb.Append(".log");

            return sb.ToString();
        }


    }
}

