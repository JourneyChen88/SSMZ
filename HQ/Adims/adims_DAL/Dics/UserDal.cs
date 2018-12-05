using adims_MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL.Dics
{
    public class UserDal
    {
        private DBConn dBConn = new DBConn();

        /// <summary>
        /// 根据ID修改用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateUserByID(UserInfo user, string id)
        {
            string coAdims_str = "update adims_user set password='" + user.password + "',user_name='" + user.user_name +
                "',position='" + user.position + "',type='" + user.type + "',uid='" + user.uid + "' where id='" + id + "'";
            return dBConn.ExecuteNonQuery(coAdims_str);
        }
        /// <summary>
        /// 根据用户名修改用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int UpdateUserByUserName(UserInfo user)
        {
            string coAdims_str = "update adims_user set password='" + user.password + "',user_name='" + user.user_name +
                "',position='" + user.position + "',type='" + user.type + "'where uid='" + user.uid + "'";
            return dBConn.ExecuteNonQuery(coAdims_str);
        }
        /// <summary>
        /// 查询用户总数--根据用户名
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public DataTable GetUserSum(string username)
        {
            string select = "select count(user_name) from  adims_user where user_name='" + username + "' ";
            return dBConn.GetDataTable(select);
        }
        /// <summary>
        /// 根据类型查找用户
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable GetUserByType(int type)
        {
            string select = "select user_name as 姓名 from adims_user where type='" + type + "' ";
            return dBConn.GetDataTable(select);
        }
        /// <summary>
        /// 查询工作人员
        /// </summary>
        /// <returns></returns>
        public DataTable GetUserByCondition(string sqlWhere)
        {
            string sql = "SELECT uid,user_name FROM Adims_User WITH (NOLOCK) WHERE {0} ORDER BY type";
            return dBConn.GetDataTable(string.Format(sql, sqlWhere));
        }
        public int UpdateUserPosition(string oldPosition, string newPosiotion)
        {
            string coAdims_str = "update adims_user set position='" + newPosiotion + "'" + " where position='" + oldPosition + "'";
            return dBConn.ExecuteNonQuery(coAdims_str);
        }
        public int DeleteUserByPosition(string oldPosition)
        {
            string coAdims_str = "update adims_user set position='(已删除)'" + " where position='" + oldPosition + "'";
            return dBConn.ExecuteNonQuery(coAdims_str);
        }
        /// <summary>
        /// 查询多有用户
        /// </summary>
        /// <returns></returns>
        public DataTable GetUserAll()
        {
            string coAdims_str = "select id,uid,password ,user_name,position,type from adims_user";
            return dBConn.GetDataTable(coAdims_str);
        }
        /// <summary>
        /// 根据条件查询用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataTable GetUserInfo(UserInfo user)
        {
            string coAdims_str = "select uid,password,user_name,position,type from adims_user where uid='" + user.uid +
                "' And password='" + user.password + "'";
            return dBConn.GetDataTable(coAdims_str);

        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int InsertUser(UserInfo user)
        {
            string coAdims_str = "insert into adims_user(uid,password,user_name,position,type) values('" +
           user.uid + "','" + user.password + "','" + user.user_name + "','" + user.position + "','" + user.type + "')";
            return dBConn.ExecuteNonQuery(coAdims_str);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int DeleteUser(UserInfo user)
        {
            string coAdims_str = "delete from adims_user where id='" + user.Id + "'";
            return dBConn.ExecuteNonQuery(coAdims_str);
        }


        /// <summary>
        /// 修改职位权限
        /// </summary>
        /// <param name="pj"></param>
        /// <returns></returns>
        public int UpdatePositionJurisdiction(PositionJurisdictionModel pj)
        {
            string coAdims_str = "update adims_position_jurisdiction set position='" + pj.position + "' where id='" +
                pj.ID + "'";
            return dBConn.ExecuteNonQuery(coAdims_str);
        }

        /// <summary>
        /// 删除职位权限
        /// </summary>
        /// <param name="pj"></param>
        /// <returns></returns>
        public int DeletePositionJurisdiction(PositionJurisdictionModel pj)
        {
            string coAdims_str = "delete from adims_position_jurisdiction where id='" + pj.ID + "'";
            return dBConn.ExecuteNonQuery(coAdims_str);
        }
        /// <summary>
        /// 查找职位权限
        /// </summary>
        /// <returns></returns>
        public DataSet GetPositionJurisdictionAll()
        {
            string coAdims_str = "select ID 岗位ID,position 岗位名称 from adims_position_jurisdiction";
            return dBConn.GetDataSet(coAdims_str);
        }
        /// <summary>
        /// 根据职位查找权限
        /// </summary>
        /// <param name="zhiwu"></param>
        /// <returns></returns>
        public DataTable GetPositionJurisdictionByPosition(string zhiwu)
        {
            string sql = "select *  from adims_position_jurisdiction where position ='" + zhiwu + "' ";
            return dBConn.GetDataTable(sql);

        }
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="quanxian"></param>
        /// <param name="zhiwu"></param>
        /// <returns></returns>
        public int UpadtePositionJurisdiction(string quanxian, string zhiwu)
        {
            string sql = "update  adims_position_jurisdiction set jurisdiction='" + quanxian + "' where position ='" + zhiwu + "' ";
            return dBConn.ExecuteNonQuery(sql);

        }
        public int InsertPositionJurisdiction(string quanxian, string zhiwu)
        {
            string sql = "insert into  adims_position_jurisdiction (position,jurisdiction) values ('" + zhiwu + "' ,'" + quanxian + "' )";
            return dBConn.ExecuteNonQuery(sql);

        }
        public int AddPositionJurisdiction(PositionJurisdictionModel pj)
        {
            pj.jurisdiction = "0000000000000000000000000000";
            string coAdims_str = "insert into adims_position_jurisdiction(position,jurisdiction) values('" +
                pj.position + "','" + pj.jurisdiction + "')";
            return dBConn.ExecuteNonQuery(coAdims_str);
        }
    }
}
