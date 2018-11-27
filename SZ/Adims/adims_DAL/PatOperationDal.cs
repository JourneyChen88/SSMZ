using adims_MODEL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL
{

    public class PatOperationDal
    {
        private DBConn dBConn = new DBConn();
        static string strconn = ConfigurationManager.AppSettings["ConnectionString"];

        /// <summary>
        /// 根据手术登记倒序查询
        /// </summary>
        /// <param name="mzid"></param>
        /// <returns></returns>
        public DataTable GetPatOperation(int mzid)
        {
            string sql = "select * from PatOperation  where mzjldid='" + mzid + "'  ";
            string orderby = @" order by ( 
case operlevel        
when '一级' then 1        
when '二级' then 2        
when '三级' then 3        
when '四级' then 4    
else '0' 
end 
) desc";
            return dBConn.GetDataTable(sql+orderby);
        }
        public DataTable GetPatOperationByCode(int mzid, string code)
        {
            string sql = "select * from PatOperation  where mzjldid='" + mzid + "'  and opercode= '" + code + "' ";
            return dBConn.GetDataTable(sql);
        }

        public DataTable GetPatOperation2(int mzid)
        {
            string sql = "select Id,operName,operCode from PatOperation  where mzjldid='" + mzid + "' order by OperLevel desc ";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 插入病人手术列表
        /// </summary>
        /// <param name="po"></param>
        /// <param name="mzjldId"></param>
        /// <returns></returns>
        public int InsertPatOperation(PatOperation po, int mzjldId)
        {
            string sql = string.Format("insert into  PatOperation(mzjldId,OperCode,OperName,OperLevel,CutType) values('{0}','{1}','{2}','{3}','{4}')  ",
                                                        mzjldId, po.OperCode, po.OperName, po.OperLevel, po.CutType);
            return dBConn.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除病人手术列表
        /// </summary>
        /// <param name="po"></param>
        /// <param name="mzjldId"></param>
        /// <returns></returns>
        public int DelPatOperation(int id)
        {

            string sql = string.Format("delete from  PatOperation where id= '{0}' ", id);

            return dBConn.ExecuteNonQuery(sql);
        }
    }
}
