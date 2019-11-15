
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Windows.Forms;


namespace ListenerRoutingLib
{
    public class DBConn
    {
        #region <<Members>>

        private static readonly string connStr = ConfigurationManager.AppSettings["ConnectionString"];


        #endregion

        #region <<Methods>>

        /// <summary>
        /// 返回DataSet
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// 
        public int Upadte_jurisdiction_DAL(string sql, int module_index, int[] be_able)
        {
            SqlConnection con = new SqlConnection(connStr);
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            int i = 0, j = 0;
            char[] source;
            int src_count = 0;
            DataTable pj = new DataTable();
            pj = ds.Tables[0];
            for (i = 0; i < count; i++)
            {
                source = (pj.Rows[i][2].ToString()).ToCharArray();
                source[module_index] = ((be_able[i] == 1) ? '1' : '0');
                src_count = source.Length;
                StringBuilder destination = new StringBuilder();
                for (j = 0; j < src_count; j++) destination.Append(source[j]);
                pj.Rows[i][2] = destination.ToString();
            }
            return adp.Update(pj);
        }
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
        public  int InsertOperDic(OperDicModel dic)
        {
            string sql = $@" INSERT INTO OperDic_NEW(OperName, OperCode, Operlevel, CutType, QuickInput)
                            VALUES('{dic.OperName}', '{dic.OperCode}','{dic.Operlevel}', '{dic.CutType}', '{dic.QuickInput}' )";
            SaveLog(sql);
            return ExecuteNonQuery(sql);
        }

        public int InsertPaiban(paibanModel pb, string tableName)
        {
            string SQL_PAIBAN = "insert into " + tableName + " (PatientNo,applyid,Cardno, PatZhuYuanID,IsZhuYuan, Patname,Patage,Patsex,patWeight,patHeight,patdpm,Pattmd,OperNo,Oname,Odate,ApplyDate,os,OsNo,OA1,OA2,OA3,OA1No,OA2No,OA3No,Amethod,Patbedno,patid,asae,operaddress,ostate,oroom,ap1,ap2,ap3,sn1,sn2,on1,on2,PidInfo,Pv1Info)"
                + "values('" + pb.PatientNo + "','" + pb.applyID + "','" + pb.cardNO + "','" + pb.zhuyuanNO + "','" + pb.IsZhuYuan + "','" + pb.patName + "','" + pb.patage + "','" + pb.patsex + "','" + pb.Weight + "','" + pb.Height + "','" + pb.patdpm + "','" + pb.SQZD + "','" + pb.OperNo + "','" + pb.Oname + "','" + pb.Odate + "','" + pb.ApplyDate + "',"
            + "'" + pb.OS + "','" + pb.OsNo + "','" + pb.OA1 + "','" + pb.OA2 + "','" + pb.OA3 + "','" + pb.OA1No + "','" + pb.OA2No + "','" + pb.OA3No + "','" + pb.MZFA + "','" + pb.BedNo + "','" + pb.PATID + "','" + pb.ASAE + "','" + pb.operAddress + "','0','','','','','','','','','" + pb.PidInfo + "','" + pb.Pv1Info + "')";
            string INSERT = string.Format(SQL_PAIBAN);
            SaveLog(INSERT);
            return ExecuteNonQuery(INSERT);
        }
        /// <summary>
        /// 修改排班所有信息
        /// </summary>
        /// <param name="pb"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public int UpdatePaibanAll(paibanModel pb, string tableName)
        {
            string SQL_PAIBAN = "Update " + tableName + " set   PatientNo='" + pb.PatientNo + "',applyid='" + pb.applyID + "',Cardno='" + pb.cardNO + "', PatZhuYuanID='" + pb.zhuyuanNO + "',"
            + "IsZhuYuan='" + pb.IsZhuYuan + "', Patname='" + pb.patName + "',Patage='" + pb.patage + "',Patsex='" + pb.patsex + "',patWeight='" + pb.Weight + "',"
            + "patHeight='" + pb.Height + "',patdpm='" + pb.patdpm + "',Pattmd='" + pb.SQZD + "',OperNo='" + pb.OperNo + "',Oname='" + pb.Oname + "',os='" + pb.OS + "',Odate='" + pb.Odate + "',ApplyDate='" + pb.ApplyDate + "',"
            + "OsNo='" + pb.OsNo + "',OA1='" + pb.OA1 + "',OA2='" + pb.OA2 + "',OA3='" + pb.OA3 + "',OA1No='" + pb.OA1No + "',OA2No='" + pb.OA2No + "',OA3No='" + pb.OA3No + "',"
            + "Amethod='" + pb.MZFA + "',Patbedno='" + pb.BedNo + "',asae='" + pb.ASAE + "',operaddress='" + pb.operAddress + "',PidInfo='" + pb.PidInfo + "',Pv1Info='" + pb.Pv1Info + "' where patid='" + pb.PATID + "'";
            string INSERT = string.Format(SQL_PAIBAN);
            SaveLog(INSERT);
            return ExecuteNonQuery(INSERT);

        }
        /// <summary>
        /// 修改排版状态
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="PATID"></param>
        /// <returns></returns>
        public int UpdatePaibanOstate(string tableName, string PATID)
        {
            string SQL_PAIBAN = "Update " + tableName + " set ostate=0,isValid=0 where patid='" + PATID + "'";
            string INSERT = string.Format(SQL_PAIBAN);
            SaveLog(INSERT);
            return ExecuteNonQuery(INSERT);
        }

        public DataTable GetPaiban(paibanModel pb, string tableName)
        {
            string SQL_PAIBAN = "select * from " + tableName + " where patid='" + pb.PATID+ "'";
            string INSERT = string.Format(SQL_PAIBAN);
            return GetDataTable(INSERT);
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

