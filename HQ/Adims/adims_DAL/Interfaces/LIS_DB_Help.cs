using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace adims_DAL
{
    public class LIS_DB_Help
    {
        #region <<Members>>

        private static readonly string connStr = ConfigurationManager.AppSettings["ConnectionStringLIS"];


        #endregion

        #region <<Methods>>

        

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
                DalLogger.WriteErrorLog(ex);
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
                DalLogger.WriteErrorLog(ex);
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
                DalLogger.WriteErrorLog(ex);
                throw ex;
            }
        }

        #endregion

        public DataTable GetLIS_ReportMicroView(string dtime)
        {
            string sql = "select ReceiveDate,SectionNo,TestTypeNo,SampleNo,ResultNo,ItemNo,ItemName,DescNo,DescName"
            +",MicroNo,MicroName,MicroDesc,AntiNo,AntiName,Suscept,SusQuan,SusDesc,RefRange,ItemDate,"
            +"ItemDesc,SerialNo,ItemTime  from ReportMicroView  where Convert(varchar,ItemDate,23)='" + dtime + "'";
            return this.GetDataTable(sql);
        }

        public DataTable GetLIS_ReportFormFull2(string dtime)
        {
            string sql = " select  ReceiveDate,SectionNo,SectionName,TestTypeNo,TestTypeName,PatNo,CName,CollectTime,"
            +"CollectDate,SampleNo,SerialNo from  ReportFormFull    where ReceiveDate='" + dtime + "'";
            return this.GetDataTable(sql);
        }        
         public DataTable GetLIS_ReportFormFull(string dtime)
        {
            string sql = " select  SampleNo 样本编号,PatNo 病历号,CName 病人姓名,GenderName 病人性别,Age 病人年龄,"
            + "Bed 病床,DeptName 科室,SerialNo 申请单号 from  ReportFormFull  where  ReceiveDate='" + dtime + "'";
            return this.GetDataTable(sql);
        }
         public DataTable GetLIS_ReportFormFullbyPATID(string PATID,string date)
         {
             string sql = "select 序号 = row_number() over(order by ParItemNo),ParItemName 组合项目名称,ItemNo 项目编号,TestItemName 项目名称,"
                        +" TestItemSName 英文名称,ReportValueAll 检验结果,RefRange 参考值范围,CONVERT(VARCHAR,ItemDate,23) AS 接收日期,"
                        + " ResultStatus 结果状态 from  ReportItemView   where  SerialNo  IN (select SerialNo from ReportFormFull  where PatNo='" + PATID + "') and CONVERT(VARCHAR,ItemDate,23)='" + date + "'";
             return this.GetDataTable(sql);
         }
      
        public DataTable GetLIS_ReportItemView(string dtime)
        {
            string sql = "select  ParItemNo 组合项目编号,ParItemName 组合项目名称,ItemNo 项目编号,TestItemName 项目名称,"
        +"TestItemSName 英文名称,ReportValueAll 检验结果,RefRange 参考值范围,ItemDate 接收日期,"
        +"ResultStatus 结果状态,SerialNo 申请单号  from  ReportItemView   where  ReceiveDate='" + dtime + "'";
            return this.GetDataTable(sql);
        }
        public DataTable GetLIS_ReportItemViewbyPATID(string SerialNo)
        {
            string sql = "select  ParItemNo 组合项目编号,ParItemName 组合项目名称,ItemNo 项目编号,TestItemName 项目名称,"
        + "TestItemSName 英文名称,ReportValueAll 检验结果,RefRange 参考值范围,ItemDate 接收日期,"
        + "ResultStatus 结果状态,SerialNo 申请单号  from  ReportItemView   where  SerialNo='" + SerialNo + "'";
            return this.GetDataTable(sql);
        }
        
    }
}
