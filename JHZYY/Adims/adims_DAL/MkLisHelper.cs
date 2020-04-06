using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
namespace adims_DAL
{
    public  class MkLisHelper
    {
        public static string strConn = "Data Source=MKLIS;User Id=JHSMXT;Password=JHSMXT;";

        public DataTable GetDataTable(string sql)
        {
            using (OracleConnection conn = new OracleConnection(strConn))
            {
                OracleCommand cmd = new OracleCommand(sql, conn);
                try
                {
                    conn.Open();
                    OracleDataAdapter adp = new OracleDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    conn.Close();
                    DataTable dt = ds.Tables[0];
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable GetLIS_ReportMicroView1(string id,string mc)
        {

            string sql = "select * from vi_smxt where Patid='" + id + "' and ENGLISHAB like'%" + mc + "%'";
            //string sql = "select * from vi_smxt where Patid='" + id + "' and ENGLISHAB like '%HB%'";
            //string sql = "select * from vi_smxt" where Patid='" + id + "'";
            return this.GetDataTable(sql);
        }
        public DataTable GetLIS_ReportMicroView2(string id, string mc,string mcs)
        {
            string sql = "select * from vi_smxt where Patid='" + id + "'";
            //string sql = "select * from vi_smxt where Patid='" + id + "'and ENGLISHAB like '%" + mc + "%' or ENGLISHAB like '%" + mcs + "%'";
            return this.GetDataTable(sql);
        }
    }
}
