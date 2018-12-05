using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL
{
    public class YongyaoListDal
    {
        private DBConn dBConn = new DBConn();

        /// <summary>
        /// 修改诱导药用量
        /// </summary>
        /// <param name="yl"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateYDY_yl(string yl, int id)
        {
            string sql = "update  Adims_ydyUSE set yl='" + yl + "'where id='" + id + "'";
            return dBConn.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 增加用药
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="yt"></param>
        /// <returns></returns>

        public DataTable GetYongyaoList(int mzjldid, int type)
        {
            string SQL = "SELECT * FROM Adims_YongyaoList WHERE mzjldid = '" + mzjldid + "' and yptype='" + type + "' order by name asc";
            return dBConn.GetDataTable(SQL);
        }
        public DataTable GetYongyaoListOrderbyStartTime(int mzjldid, int type)
        {
            string SQL = "SELECT * FROM Adims_YongyaoList WHERE mzjldid = '" + mzjldid + "' and yptype='" + type + "' order by ksTime asc";
            return dBConn.GetDataTable(SQL);
        }

        /// <summary>
        /// 添加PACU气体药
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="qt"></param>
        /// <returns></returns>
        public int InsertPACU_qt(int mzjldid, adims_MODEL.mzqt qt)
        {
            return dBConn.ExecuteNonQuery("INSERT INTO Adims_PACU_qtUse(mzjldid,qtname,yl,dw,sytime,flags) "
                + "values('" + mzjldid + "','" + qt.Qtname + "','" + qt.Yl + "','" + qt.Dw + "','" + qt.Sysj + "',1)");
        }
        public DataTable GetPACU_qt(int mzjldid)
        {
            string SQL = "SELECT * FROM Adims_PACU_qtUse WHERE mzjldid = '" + mzjldid + "'order by qtname asc";
            return dBConn.GetDataTable(SQL);
        }


        public int UpdatePACU_qt_EndTime(int mzjldid, DateTime dt, int id)
        {
            return dBConn.ExecuteNonQuery("update Adims_PACU_qtUse set jstime='" + dt + "',flags='2' where id='" + id + "'");
        }

        /// <summary>
        /// 删除麻醉记录单气体
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="qt"></param>
        /// <returns></returns>
        public int Delete_Qt(int mzjldid, int id)
        {
            return dBConn.ExecuteNonQuery("DELETE FROM Adims_Qtuse WHERE mzjldid='" + mzjldid + "'and id = '" + id + "' ");
        }
        /// <summary>
        /// 删除PACU气体药
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeletePACU_Qt(int mzjldid, int id)
        {
            return dBConn.ExecuteNonQuery("DELETE FROM Adims_PACU_qtUse WHERE mzjldid='" + mzjldid + "'and id = '" + id + "' ");
        }
        /// <summary>
        /// 新增诱导药的使用
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertYDY(int mzjld, string name, string fs, string yl, string dw, DateTime now)
        {
            string insert = "insert into [Adims_ydyUse] (mzjldid,ydyname,yyfs,yl,dw,time) values('" + mzjld + "','" + name + "','" + fs + "','" + yl + "','" + dw + "','" + DateTime.Now + "')";

            return dBConn.ExecuteNonQuery(string.Format(insert));
        }
        /// <summary>
        /// 新增术中时间使用
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertIntoSZSJ(int mzjld, string sjname, DateTime now, int type, string id)
        {
            string insert = "";
            if (type == 0)
                insert = "insert into [Adims_Szsj] (mzjldid,name,time,Szsj_id,Y_zb) values('" + mzjld + "','" + sjname + "','" + DateTime.Now + "','" + id + "','370')";
            if (type == 1)
                insert = "insert into [Adims_PACU_shijian] (mzjldid,name,time) values('" + mzjld + "','" + sjname + "','" + DateTime.Now + "')";

            return dBConn.ExecuteNonQuery(string.Format(insert));
        }
        /// <summary>
        /// 删除诱导药的使用
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int deleteYDYUse(int id)
        {
            string delete = "delete from [Adims_ydyUse] where id='" + id + "' ";
            return dBConn.ExecuteNonQuery(string.Format(delete));
        }
        /// <summary>
        /// 删除术中事件
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int deleteSZSJ(int id, int type)
        {
            string delete = "";
            if (type == 0)
                delete = "delete from [Adims_Szsj] where id='" + id + "' ";
            if (type == 1)
                delete = "delete from [Adims_PACU_shijian] where id='" + id + "' ";

            return dBConn.ExecuteNonQuery(string.Format(delete));
        }
        /// <summary>
        /// 删除特殊用药
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int deleteTSYY(int id)
        {
            string delete = "delete from Adims_Tsyy where id='" + id + "' ";
            return dBConn.ExecuteNonQuery(string.Format(delete));
        }


       

    }
}
