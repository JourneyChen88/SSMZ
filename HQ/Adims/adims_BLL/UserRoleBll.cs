using adims_DAL;
using adims_MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_BLL
{
    public class UserRoleBll
    {


        DBConn dBConn = new DBConn();
        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GetUserRole(UserInfo user)
        {
            string coAdims_str = "select jurisdiction from adims_position_jurisdiction where position='" + user.position + "'";
            DataTable user_jurisdiction = dBConn.GetDataTable(coAdims_str);
            if (user_jurisdiction == null) return null;
            else
            {
                return user_jurisdiction.Rows[0][0].ToString();
            }
        }
    }
}
