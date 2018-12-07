using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL
{

    public class MzjldPointDal
    {
        DBConn dBConn = new DBConn();

        public DataTable GetMzjldPoint(DateTime dt, int mzjldid)
        {
            string sel = "select * from Adims_MonitorRecord where RecordTime='" + dt + "'and mzjldid='" + mzjldid + "'";
            return dBConn.GetDataTable(sel);
        }
        public int insertJianCeDataMZJLD(int mzjldid, int nibps, int nibpd, int nibpm, int rrc, int hr, int pulse, int spo2, int etco2, double temp, DateTime now)
        {
            string insert = "insert into Adims_MonitorRecord(mzjldid,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP,RecordTime) values(" + mzjldid + "," + nibps + "," + nibpd + "," + nibpm + "," + rrc + "," + hr + "," + pulse + "," + spo2 + "," + etco2 + "," + temp + ",'" + now + "')";
            return dBConn.ExecuteNonQuery(insert);
        }
        /// <summary>
        /// 插入检测数据
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int insertJianCeData(int mzjldid, int hr, int spo2, int pulse, int nibps, int nibpd, int nibpm, int arts, int artd, int artm, double etco2, int ico2, int rrc)
        {
            string insert = "insert into Adims_MonitorRecord(mzjldid,HR,SpO2,Pulse,NIBPS,NIBPD,NIBPM,ARTS,ARTD,ARTM,ETCO2,ICO2,RRC,RecordTime) values(" + mzjldid + "," + hr + "," + spo2 + "," + pulse + "," + nibps + "," + nibpd + "," + nibpm + "," + arts + "," + artd + "," + artm + "," + etco2 + "," + ico2 + "," + rrc + ",'" + DateTime.Now + "')";

            return dBConn.ExecuteNonQuery(insert);
        }
        public int insertJianCeDataMZJLD(int mzjldid, int nibps, int nibpd, int nibpm, int rrc, int hr, int pulse, int spo2, int etco2, double temp, string now)
        {
            string insert = "insert into Adims_MonitorRecord(mzjldid,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP,RecordTime) values(" + mzjldid + "," + nibps + "," + nibpd + "," + nibpm + "," + rrc + "," + hr + "," + pulse + "," + spo2 + "," + etco2 + "," + temp + ",'" + now + "')";
            return dBConn.ExecuteNonQuery(insert);
        }
        public int insertJianCeDataPACU(int mzjldid, int nibps, int nibpd, int nibpm, int rrc, int hr, int pulse, int spo2, int etco2, double temp, string now)
        {
            string insert = "insert into Adims_MonitorRecord_PACU(mzjldid,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP,RecordTime) values(" + mzjldid + "," + nibps + "," + nibpd + "," + nibpm + "," + rrc + "," + hr + "," + pulse + "," + spo2 + "," + etco2 + "," + temp + ",'" + now + "')";
            return dBConn.ExecuteNonQuery(insert);
        }

        /// <summary>
        /// 判断本地数据库是否有这个间隔时间的数据
        /// </summary>
        public DataTable selectJianCeData(string dt, int mzjldid, int TYPE)
        {
            string sel = "";
            if (TYPE == 0)
                sel = "select * from Adims_MonitorRecord where RecordTime='" + dt + "'and mzjldid='" + mzjldid + "'";
            if (TYPE == 1)
                sel = "select * from Adims_MonitorRecord_PACU where RecordTime='" + dt + "'and mzjldid='" + mzjldid + "'";
            return dBConn.GetDataTable(sel);
        }


        /// <summary>
        /// 拷贝Adims_MonitorRecord数据 到PACU检测表到显示表
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="dtime"></param>
        /// <returns></returns>
        public int CopyDataPacu(int mzjldid, DateTime dtime)
        {
            string query = "select mzjldid,RecordTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP from Adims_MonitorRecord_PACU "
                + " where mzjldid='" + mzjldid + "' and RecordTime='" + dtime.ToString("yyyy-MM-dd HH:mm") + "'";
            DataTable dt = dBConn.GetDataTable(query);
            string queryServer = "select mzjldid,RecordTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP from Adims_PACU_Point "
               + " where mzjldid='" + mzjldid + "' and RecordTime='" + dtime + "'";
            DataTable dtServer = dBConn.GetDataTable(queryServer);
            int i = 0;
            if (dt.Rows.Count > 0 && dtServer.Rows.Count == 0)
            {
                string copy = "insert into Adims_Pacu_Point(mzjldid,RecordTime,NIBPS,NIBPD,NIBPM,RRC,HR,Pulse,SpO2,ETCO2,TEMP) "
                    + "values('" + dt.Rows[0]["mzjldid"] + "','" + Convert.ToDateTime(dt.Rows[0]["RecordTime"]) + "','" + dt.Rows[0]["NIBPS"] + "','" + dt.Rows[0]["NIBPD"] + "',"
                    + "'" + dt.Rows[0]["NIBPM"] + "','" + dt.Rows[0]["RRC"] + "','" + dt.Rows[0]["HR"] + "','" + dt.Rows[0]["Pulse"] + "', '" + dt.Rows[0]["SpO2"] + "',"
                    + "'" + dt.Rows[0]["ETCO2"] + "', '" + dt.Rows[0]["TEMP"] + "')";
                i = dBConn.ExecuteNonQuery(copy);
            }
            return i;
        }
        /// <summary>
        /// 修改监护数据
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="jh"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int UpdateMzjldPointSpO2andETCO2(int mzjldid, adims_MODEL.jhxm jh, int type)//
        {

            string sqlU = "";
            if (type == 0)
            {
                if (jh.Sy == "SpO2")
                    sqlU = "update Adims_MonitorRecord set spo2='" + jh.V + "' where mzjldid='"
                            + mzjldid + "' and RecordTime='" + jh.D + "'";
                else if (jh.Sy == "ETCO2")
                    sqlU = "update Adims_MonitorRecord set spo2='" + jh.V + "' where mzjldid='"
                            + mzjldid + "' and RecordTime='" + jh.D + "'";

            }
            if (type == 1)
            {
                if (jh.Sy == "SpO2")
                    sqlU = "update Adims_pacu_Point set spo2='" + jh.V + "' where mzjldid='"
                        + mzjldid + "' and RecordTime='" + jh.D + "'";
                else if (jh.Sy == "ETCO2")
                    sqlU = "update Adims_pacu_Point set spo2='" + jh.V + "' where mzjldid='"
                        + mzjldid + "' and RecordTime='" + jh.D + "'";
            }
            return dBConn.ExecuteNonQuery(sqlU);
        }
        /// <summary>
        /// 查询最大时间监护点
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <returns></returns>
        public DataTable GetMaxPoint(int mzjldid)//查询监测点
        {
            string insert = "select max(RecordTime) from Adims_MonitorRecord where mzjldid='" + mzjldid + "'";
            return dBConn.GetDataTable(insert);
        }
        /// <summary>
        /// 查询单个显示监测点
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable GetSingle(int mzjldid, DateTime dt)//查询监测点
        {
            string insert = "select * from Adims_MonitorRecord where mzjldid='" + mzjldid + "' and RecordTime='" + dt + "'";
            return dBConn.GetDataTable(insert);
        }
        /// <summary>
        /// 查询监测点
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <returns></returns>
        public DataTable GetPoint(int mzjldid)//
        {
            string insert = "select mzjldid,RecordTime,NIBPS,NIBPD,RRC,Pulse,SpO2,ETCO2,CVP,qdy,sdz,jsz,temp " +
                " from Adims_MonitorRecord where mzjldid='" + mzjldid + "' ORDER BY RecordTime ASC";
            return dBConn.GetDataTable(insert);
        }
        /// <summary>
        /// 修改显示监测点
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="dt"></param>
        /// <param name="nibps"></param>
        /// <param name="nibpd"></param>
        /// <param name="rrc"></param>
        /// <param name="pulse"></param>
        /// <param name="spo2"></param>
        /// <param name="etco2"></param>
        /// <param name="temp"></param>
        /// <param name="cvp"></param>
        /// <returns></returns>
        public int Update(int mzjldid, DateTime dt, string nibps, string nibpd, string rrc, string pulse, string spo2, string etco2, string temp, string cvp)//修改监测点
        {
            string insert = "UPDATE  Adims_MonitorRecord set NIBPS = '" + nibps + "',NIBPD= '" + nibpd + "',RRC= '" + rrc + "',Pulse= '" + pulse + "',"
            + "SpO2= '" + spo2 + "',ETCO2= '" + etco2 + "',temp= '" + temp + "',cvp= '" + cvp + "'"
            + " where mzjldid='" + mzjldid + "' and RecordTime='" + dt + "'";
            return dBConn.ExecuteNonQuery(insert);
        }
        public DataTable GetByMzjldID(int mzjldid)//查询显示监测点1
        {
            string sql = $"Select  RecordTime ,NIBPS,NIBPD,Pulse,RRC,TEMP,SpO2,ETCO2,CVP,NIBPM,HR from Adims_MonitorRecord  Where mzjldid ='{mzjldid}' order by RecordTime ASC";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 获取监测点--通过时间间隔
        /// </summary>
        /// <param name="mzjldid">麻醉编号</param>
        /// <param name="Start">开始时间</param>
        /// <param name="End">结束时间</param>
        /// <param name="Intervel">间隔时间</param>
        /// <returns></returns>
        public DataTable GetByTimeSpan(int mzjldid, DateTime Start, DateTime End, int Intervel)//查询显示监测点1
        {
            string sql = "Select  RecordTime ,NIBPS,NIBPD,Pulse,RRC,TEMP,SpO2,ETCO2,CVP,NIBPM,HR from Adims_MonitorRecord  ";
            sql += " Where mzjldid ='" + mzjldid + "'  AND RecordTime between '" + Start + "' AND '" + End + "'";
            sql += " And DateDiff(mi,'" + Start + "',RecordTime) % '" + Intervel + "'=0 order by RecordTime ASC";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 删除监测点
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int Delete(int mzjldid, DateTime dt)//
        {
            string insert = "delete from  Adims_MonitorRecord where mzjldid='" + mzjldid + "' and RecordTime='" + dt + "'";
            return dBConn.ExecuteNonQuery(insert);
        }
        /// <summary>
        /// 增加显示监测点
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="dt"></param>
        /// <param name="nibps"></param>
        /// <param name="nibpd"></param>
        /// <param name="rrc"></param>
        /// <param name="pulse"></param>
        /// <param name="spo2"></param>
        /// <param name="etco2"></param>
        /// <param name="cvp"></param>
        /// <param name="temp"></param>
        /// <returns></returns>
        public int Add(int mzjldid, DateTime dt, string nibps, string nibpd, string rrc, string pulse, string spo2, string etco2, string cvp, string temp)//增加监测点
        {
            string insert = "insert into Adims_MonitorRecord(mzjldid,RecordTime,NIBPS,NIBPD,RRC,Pulse,SpO2,ETCO2,cvp,Temp) values ('" + mzjldid + "','" + dt + "','" + nibps + "',"
            + "'" + nibpd + "','" + rrc + "','" + pulse + "','" + spo2 + "','" + etco2 + "','" + cvp + "','" + temp + "')";
            return dBConn.ExecuteNonQuery(insert);
        }
        /// <summary>
        /// 修改机控呼吸监测点
        /// </summary>
        /// <param name="mzid"></param>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int UpdateMzjldPointJKHX(int mzid, DateTime dt1, DateTime dt2, string value)
        {
            string MZJLD_UPDATE1 = "UPDATE Adims_MonitorRecord SET RRC='" + value + "' where mzjldid='" + mzid + "' and RecordTime>'" + dt1 + "' and RecordTime<'" + dt2 + "'";
            return dBConn.ExecuteNonQuery(MZJLD_UPDATE1);
        }

        /// <summary>
        /// 修改体温记录点
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public int UpdateMzjldPointTemp(int mzjldid, adims_MODEL.tw_point p)//修改点
        {
            string SQL = "";
            if (p.Lx == 5)
                SQL = "update Adims_MonitorRecord set TEMP='" + p.V + "' where mzjldid='"
                    + mzjldid + "' and RecordTime='" + p.D + "'";
            return dBConn.ExecuteNonQuery(SQL);
        }
        public int UpdateMzjldPoint(int mzjldid, adims_MODEL.point p)//修改点
        {
            string SQL = "";
            if (p.Lx == 1)
                SQL = "update Adims_MonitorRecord set NIBPS='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            else if (p.Lx == 2)
                SQL = "update Adims_MonitorRecord set NIBPD='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            else if (p.Lx == 3)
                SQL = "update Adims_MonitorRecord set pulse='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            else if (p.Lx == 4)
                SQL = "update Adims_MonitorRecord set RRC='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            else if (p.Lx == 5)
                SQL = "update Adims_MonitorRecord set TEMP='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            else if (p.Lx == 6)
                SQL = "update Adims_MonitorRecord set ETCO2='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            //else if (p.Lx == 7)
            //    SQL = "update Adims_MonitorRecord set SpO2='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            //else if (p.Lx == 8)
            //    SQL = "update Adims_MonitorRecord set CVP='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            //else if (p.Lx == 9)
            //    SQL = "update Adims_MonitorRecord set TOF='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";

            return dBConn.ExecuteNonQuery(SQL);
        }

    }
}
