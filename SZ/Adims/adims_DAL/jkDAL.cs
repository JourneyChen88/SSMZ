using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MODEL;
using DBUtility;
using System.Data;
namespace DAL
{
    public class jkDAL
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="NMYCtable"></param>
        /// <returns></returns>
        public static bool addjhtable(jkModel jhModel)
        {

            StringBuilder str = new StringBuilder();
            StringBuilder str1 = new StringBuilder();
            StringBuilder str2 = new StringBuilder();

            if (jhModel.OfficeName1.ToString() != null)
            {
                str1.Append("OfficeName,");
                str2.Append("'" + jhModel.OfficeName1 + "',");
            }

            if (jhModel.BedId1.ToString() != null)
            {
                str1.Append("BedId,");
                str2.Append("" + jhModel.BedId1 + ",");
            }

            if (jhModel.PatientMonitor_IP1.ToString() != null)
            {
                str1.Append("PatientMonitor_IP,");
                str2.Append("'" + jhModel.PatientMonitor_IP1 + "',");
            }

            if (jhModel.IPSeq1.ToString() != null)
            {
                str1.Append("IPSeq,");
                str2.Append("'" + jhModel.IPSeq1 + "',");
            }

            if (jhModel.MeasureTime1.ToString() != null)
            {
                str1.Append("MeasureTime,");
                str2.Append("'" + jhModel.MeasureTime1 + "',");
            }

            if (jhModel.HR1.ToString() != null)
            {
                str1.Append("HR,");
                str2.Append("'" + jhModel.HR1 + "',");
            }

            if (jhModel.SpO21.ToString() != null)
            {
                str1.Append("SpO2,");
                str2.Append("'" + jhModel.SpO21 + "',");
            }

            if (jhModel.PVCs1.ToString() != null)
            {
                str1.Append("PVCs,");
                str2.Append("'" + jhModel.PVCs1 + "',");
            }

            if (jhModel.ST_II1.ToString() != null)
            {
                str1.Append("ST_II,");
                str2.Append("'" + jhModel.ST_II1 + "',");
            }

            if (jhModel.RR1.ToString() != null)
            {
                str1.Append("RR,");
                str2.Append("'" + jhModel.RR1 + "',");
            }

            if (jhModel.PR1.ToString() != null)
            {
                str1.Append("PR,");
                str2.Append("'" + jhModel.PR1 + "',");
            }

            if (jhModel.Dia1.ToString() != null)
            {
                str1.Append("Dia,");
                str2.Append("'" + jhModel.Dia1 + "',");
            }

            if (jhModel.Mean1.ToString() != null)
            {
                str1.Append("Mean,");
                str2.Append("'" + jhModel.Mean1 + "',");
            }

            if (jhModel.Sys1.ToString() != null)
            {
                str1.Append("Sys,");
                str2.Append("'" + jhModel.Sys1 + "',");
            }

            str.Append("insert into dbo." + jhModel.TableNumber1 + " (");
            str.Append(str1.ToString().Remove(str1.Length - 1));
            str.Append(")");
            str.Append(" values (");
            str.Append(str2.ToString().Remove(str2.Length - 1));
            str.Append(")");

            return DbHelperSql.EditData(str.ToString());

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sickid"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DataSet jhtable(string TableNumber1, DateTime MeasureTime)
        {

            DbHelperSql sql = new DbHelperSql();
            DataSet date = DbHelperSql.SelectTableData("select * from dbo." + TableNumber1 + " where MeasureTime ='" + MeasureTime + "'");

            return date;
        }

    }
}
