using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using adims_MODEL;

namespace adims_DAL
{
    public class DataManageClass
    {
        private DBConn dBConn = new DBConn();
        /// <summary>
        /// 查询用户编号
        /// </summary>
        /// <param name="userno"></param>
        /// <returns></returns>
        public DataTable selectCount_user(string userno)
        {
            string select = "select count(user_name) from  adims_user where userno='" + userno + "' ";
            return dBConn.GetDataTable(select);
        }
        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="userno"></param>
        /// <returns></returns>
        public int Add_user(user_info user)
        {
            string sql = "insert into adims_user(userno,uid,password,user_name,position,type) values('" + user.userno + "','" +
            user.uid + "','" + user.password + "','" + user.user_name + "','" + user.position + "','" + user.type + "')";
            return dBConn.ExecuteNonQuery(sql);
        }


        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="userno"></param>
        /// <returns></returns>
        public int Update_user(user_info user)
        {
            string sql = "Update  adims_user set uid='" + user.uid + "',password='" + user.password + "',"
            + "position='" + user.position + "',type='" + user.type + "' where userno='" + user.userno + "'";
            return dBConn.ExecuteNonQuery(sql);
        }

        public DataTable select_user()
        {
            string sql = "select userno 员工编号,user_name 用户名,uid 登录账号,password 登录密码,position 职位 from adims_user";
            return dBConn.GetDataTable(sql);
        }
       
    }
}
