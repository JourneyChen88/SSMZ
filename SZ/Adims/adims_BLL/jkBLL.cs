using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MODEL;
using DAL;
using System.Data;
namespace BLL
{
    public class jkBLL
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sickpeople"></param>
        /// <returns></returns>
        public static bool addjhtable(jkModel jhModel)
        {
            return jkDAL.addjhtable(jhModel);
        }


        /// <summary>
        /// 显示
        /// </summary>
        /// <returns></returns>
        public static DataSet jhtable(string TableNumber1, DateTime MeasureTime)
        {
            return jkDAL.jhtable(TableNumber1, MeasureTime);
        }
    }
}
