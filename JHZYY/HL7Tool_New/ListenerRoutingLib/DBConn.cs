
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
        public int InsertOperDic(OperDicModel dic)
        {
            string sql = $@" INSERT INTO OperDic_NEW(OperName, OperCode, Operlevel, CutType, QuickInput)
                            VALUES('{dic.OperName}', '{dic.OperCode}','{dic.Operlevel}', '{dic.CutType}', '{dic.QuickInput}' )";
            SaveLog(sql);
            return ExecuteNonQuery(sql);
        }

        public int InsertPaiban(paibanModel pb)
        {
            string SQL_PAIBAN = $@"INSERT INTO dbo.Adims_OTypesetting
(PatID, PatZhuYuanID, CardID, Patname, Patsex, Patage, PatNation, Patbedno, Patdpm, Pattmd, Oname, Amethod, Tiwei, Second, Oroom
, GR, BX, AP1, AP2, AP3, OS, OS1, OS2, OS3, OS4, ON1, ON2, SN1, SN2, Remarks, Ostate, StartTime, Olevel, isjizhen, PatHeight, PatWeight
, PatBloodType, asa, Odate, asae, xueya, maibo, huxi, tiwen, Ocode, CFSS, SSLB, QKDJ, isQXSM, sqys, zrys, osdm, qxbs, expertName, 
zycs, SSDJ, yiliao, SFZH, chexiao, BRSYH, ZYSJ, PidInfo, Pv1Info)
VALUES
('{pb.PatID}','{pb.PatZhuYuanID}','{pb.CardID}','{pb.Patname}','{pb.Patsex}','{pb.Patage}','{pb.PatNation}','{pb.Patbedno}'
,'{pb.Patdpm}','{pb.Pattmd}','{pb.Oname}','{pb.Amethod}','{pb.Tiwei}','{pb.Second}','{pb.Oroom}','{pb.GR}','{pb.BX}','{pb.AP1}'
,'{pb.AP2}','{pb.AP3}','{pb.OS}','{pb.OS1}','{pb.OS2}','{pb.OS3}','{pb.OS4}','{pb.ON1}','{pb.ON2}','{pb.SN1}','{pb.SN2}'
,'{pb.Remarks}','{pb.Ostate}','{pb.StartTime}','{pb.Olevel}','{pb.isjizhen}','{pb.PatHeight}','{pb.PatWeight}','{pb.PatBloodType}'
,'{pb.asa}','{pb.Odate}','{pb.asae}','{pb.xueya}','{pb.maibo}','{pb.huxi}','{pb.tiwen}','{pb.Ocode}','{pb.CFSS}','{pb.SSLB}'
,'{pb.QKDJ}','{pb.isQXSM}','{pb.sqys}','{pb.zrys}','{pb.osdm}','{pb.qxbs}','{pb.expertName}','{pb.zycs}','{pb.SSDJ}','{pb.yiliao}'
,'{pb.SFZH}','{pb.chexiao}','{pb.BRSYH}','{pb.ZYSJ}','{pb.PidInfo}','{pb.Pv1Info}')";
            SaveLog(SQL_PAIBAN);
            return ExecuteNonQuery(SQL_PAIBAN);
        }
        /// <summary>
        /// 修改排班所有信息
        /// </summary>
        /// <param name="pb"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public int UpdatePaibanAll(paibanModel pb)
        {
            string update = $@"
UPDATE dbo.Adims_OTypesetting
   SET PatID = '{pb.PatID}'
      ,CardID = '{pb.CardID}'
      ,Patname = '{pb.Patname}'
      ,Patsex = '{pb.Patsex}'
      ,Patage = '{pb.Patage}'
      ,PatNation = '{pb.PatNation}'
      ,Patbedno = '{pb.Patbedno}'
      ,Patdpm = '{pb.Patdpm}'
      ,Pattmd = '{pb.Pattmd}'
      ,Oname = '{pb.Oname}'
      ,Amethod = '{pb.Amethod}'
      ,Tiwei = '{pb.Tiwei}'
      ,Second = '{pb.Second}'
      ,Oroom = '{pb.Oroom}'
      ,GR = '{pb.GR}'
      ,BX = '{pb.BX}'
      ,AP1 = '{pb.AP1}'
      ,AP2 = '{pb.AP2}'
      ,AP3 = '{pb.AP3}'
      ,OS = '{pb.OS}'
      ,OS1 = '{pb.OS1}'
      ,OS2 = '{pb.OS2}'
      ,OS3 = '{pb.OS3}'
      ,OS4 = '{pb.OS4}'
      ,ON1 = '{pb.ON1}'
      ,ON2 = '{pb.ON2}'
      ,SN1 = '{pb.SN1}'
      ,SN2 = '{pb.SN2}'
      ,Remarks = '{pb.Remarks}'
      ,Ostate = '{pb.Ostate}'
      ,StartTime = '{pb.StartTime}'
      ,Olevel = '{pb.Olevel}'
      ,isjizhen = '{pb.isjizhen}'
      ,PatHeight = '{pb.PatHeight}'
      ,PatWeight = '{pb.PatWeight}'
      ,PatBloodType = '{pb.PatBloodType}'
      ,asa = '{pb.asa}'
      ,Odate = '{pb.Odate}'
      ,asae = '{pb.asae}'
      ,xueya = '{pb.xueya}'
      ,maibo = '{pb.maibo}'
      ,huxi = '{pb.huxi}'
      ,tiwen = '{pb.tiwen}'
      ,Ocode = '{pb.Ocode}'
      ,CFSS = '{pb.CFSS}'
      ,SSLB = '{pb.SSLB}'
      ,QKDJ = '{pb.QKDJ}'
      ,isQXSM = '{pb.isQXSM}'
      ,sqys = '{pb.sqys}'
      ,zrys = '{pb.zrys}'
      ,osdm = '{pb.osdm}'
      ,qxbs = '{pb.qxbs}'
      ,expertName = '{pb.expertName}'
      ,zycs = '{pb.zycs}'
      ,SSDJ = '{pb.SSDJ}'
      ,yiliao = '{pb.yiliao}'
      ,SFZH = '{pb.SFZH}'
      ,chexiao = '{pb.chexiao}'
      ,BRSYH = '{pb.BRSYH}'
      ,ZYSJ = '{pb.ZYSJ}'
      ,PidInfo = '{pb.PidInfo}'
      ,Pv1Info = '{pb.Pv1Info}'
 WHERE PatZhuYuanID='{pb.PatZhuYuanID}'";
            SaveLog(update);
            return ExecuteNonQuery(update);

        }
        /// <summary>
        /// 修改排版状态
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="PATID"></param>
        /// <returns></returns>
        public int UpdatePaibanOstate(string zhuyuanid)
        {
            string SQL_PAIBAN = "Update Adims_OTypesetting set ostate=0,isValid=0 where PatZhuYuanID='" + zhuyuanid + "'";
            string INSERT = string.Format(SQL_PAIBAN);
            SaveLog(INSERT);
            return ExecuteNonQuery(INSERT);
        }

        public DataTable GetPaiban(paibanModel pb)
        {
            string SQL_PAIBAN = "select * from Adims_OTypesetting where PatZhuYuanID='" + pb.PatZhuYuanID + "'";
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

