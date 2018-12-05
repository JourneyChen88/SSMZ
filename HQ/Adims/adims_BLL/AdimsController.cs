using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using adims_MODEL;
using adims_DAL;


namespace adims_BLL
{
    public class AdimsController
    {
        #region <<Members>>

        DBConn dBConn = new DBConn();
        AdimsProvider adimsProvider = new AdimsProvider();

        #endregion
        

        #region <<员工排班>>

        /// <summary>
        /// 查询员工排班
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetEmployeesList(string sqlWhere)
        {
            return adimsProvider.GetEmployeesList(sqlWhere);
        }

        /// <summary>
        /// 新增员工排班
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertEmployees(Dictionary<string, string> dictionary)
        {
            return adimsProvider.InsertEmployees(dictionary);
        }

        /// <summary>
        /// 修改员工排班
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateEmployees(Dictionary<string, string> dictionary)
        {
            return adimsProvider.UpdateEmployees(dictionary);
        }

        /// <summary>
        /// 删除员工排班
        /// </summary>
        /// <param name="employeesID"></param>
        /// <returns></returns>
        public int DeleteEmployees(string employeesID)
        {
            return adimsProvider.DeleteEmployees(employeesID);
        }

        #endregion

        #region <<请假登记>>

        /// <summary>
        /// 查询请假登记
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetLeaveRegistrationList(string sqlWhere)
        {
            return adimsProvider.GetLeaveRegistrationList(sqlWhere);
        }

        /// <summary>
        /// 新增请假登记
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertLeaveRegistration(List<string> dictionary)
        {
            return adimsProvider.InsertLeaveRegistration(dictionary);
        }

        /// <summary>
        /// 修改请假登记
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateLeaveRegistration(List<string> dictionary)
        {
            return adimsProvider.UpdateLeaveRegistration(dictionary);
        }

        /// <summary>
        /// 删除请假登记
        /// </summary>
        /// <param name="employeesID"></param>
        /// <returns></returns>
        public int DeleteLeaveRegistration(string employeesID)
        {
            return adimsProvider.DeleteLeaveRegistration(employeesID);
        }

        #endregion

        #region <<加班登记>>

        /// <summary>
        /// 查询加班登记
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetJBJLList(string sqlWhere)
        {
            return adimsProvider.GetJBJLList(sqlWhere);
        }

        /// <summary>
        /// 新增加班登记
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertJBJL(List<string> dictionary)
        {
            return adimsProvider.InsertJBJL(dictionary);
        }

        /// <summary>
        /// 修改加班登记
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateJBJL(List<string> dictionary)
        {
            return adimsProvider.UpdateJBJL(dictionary);
        }

        /// <summary>
        /// 删除加班登记
        /// </summary>
        /// <param name="employeesID"></param>
        /// <returns></returns>
        public int DeleteJBJL(string employeesID)
        {
            return adimsProvider.DeleteJBJL(employeesID);
        }

        #endregion

        #region <<智能排班>>

        /// <summary>
        /// 查询手术排班
        /// </summary>
        /// <param name="oroom"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable GetOTypesetting(string oroom, string dt)
        {
            return adimsProvider.GetOTypesetting(oroom, dt);
        }

        public void UpdateOTypesetting(string oroom, string dt, object[] doctors)
        {
            adimsProvider.UpdateOTypesetting(oroom, dt, doctors);
        }

        /// <summary>
        /// 保存排版信息
        /// </summary>
        /// <param name="pb"></param>
        /// <returns></returns>
        public int SaveOTypesetting(adims_MODEL.paiban pb)
        {
            return adimsProvider.SaveOTypesetting(pb);
        }

        /// <summary>
        /// 更新排班信息
        /// </summary>
        /// <param name="patID"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
       

        public DataTable select_staff(int posttype)
        {
            return dBConn.GetDataTable("select * from adims_surgerystaff where posttype='" + posttype + "'");
        }
        #endregion
            
    
        #region 麻醉登记
          public DataTable Get_mzdj(string dtpay)
        {
            string sql = "select * from Adims_MZDJ where convert(varchar,time,23)='" + dtpay + "' order by time";
            return dBConn.GetDataTable(sql);
             
        }

          public DataTable Get_mzdj(DateTime dt1, DateTime dt2)
          {
              string sql = "select  row_number() over(order by id) as xuhao,id,[time],[name],[age],[mzfs] ,[bz] from Adims_MZDJ where time between '" + dt1 + "'and'" + dt2 + "' ";
              return dBConn.GetDataTable(sql);

          }
          public DataTable Get_mzdjs()
          {
              string sql = "select count(*) from Adims_MZDJ ";
              return dBConn.GetDataTable(sql);

          }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dtpay"></param>
        /// <returns></returns>
          public int Insert_mzdj(string dtpay)
          {
              string _INSERT = "INSERT INTO Adims_MZDJ(time) values('" + dtpay + "')";

              return dBConn.ExecuteNonQuery(_INSERT);
          }
          public int delete_mzdj(string id)
          {
              string _INSERT = "delete Adims_MZDJ where id='"+id+"'";

              return dBConn.ExecuteNonQuery(_INSERT);
          }
          public int Updatemzdj_Data(string id, string DateType, string value)
          {
              string sql = "UPDATE Adims_MZDJ WITH (ROWLOCK) SET " + DateType + " = '" + value + "' WHERE id = '" + id + "'";
              return dBConn.ExecuteNonQuery(sql);
          }
        #endregion
      
        #region    //药品管理模块中使用的方法
        #region     //麻醉科药品管理


        public DataSet ypxhtj_table()
        {
            return dBConn.GetDataSet("select ypbh 药品编号,ypname 药品名称,ypGg 药品规格,pyzt 拼音字头,dl 毒理,zt 状态,xisl 消耗数量 from ypxhtj");
        }
    
        public int Add_surgery_stock(surgery_stock stock)
        {
            string coAdims_str = "insert into adims_surgery_stock(surgery_id,medicine_number,count) values('" +
       stock.surgery_id + "','" + stock.medicine_number + "','" + stock.count + "')";
            return dBConn.ExecuteNonQuery(coAdims_str);
        }
        public int Add_medicine_info(medicine_info medicine)
        {
            string coAdims_str = String.Format("select count(*) from adims_medicine_info where medicine_number='{0}'", medicine.medicine_number);
            int count = (int)dBConn.ExecuteScalar(coAdims_str);
            if (0 == count)
            {
                coAdims_str = "insert into adims_medicine_info(medicine_number,medicine_name,phonetic_prefix," +
             "toxicology,state,dosagy_form,specification,produce_time,deadline,batch_number,origin_place) values('" +
              medicine.medicine_number + "','" + medicine.medicine_name + "','" + medicine.phonetic_prefix + "','" +
             medicine.toxicology + "','" + medicine.state + "','" + medicine.dosagy_form + "','" + medicine.specification + "','" +
             medicine.produce_time + "','" + medicine.deadline + "','" + medicine.batch_number + "','" + medicine.origin_place + "')";
                return dBConn.ExecuteNonQuery(coAdims_str);
            }
            else
                return 1;
        }
        public int Add_surgery_input(surgery_input input)
        {
            string coAdims_str = "insert into adims_surgery_input(surgery_id,medicine_number,input_count,input_time,confirm_person) values('" +
        input.surgery_id + "','" + input.medicine_number + "','" + input.input_count + "','" + input.input_time + "','" + input.confirm_person + "')";
            return dBConn.ExecuteNonQuery(coAdims_str);
        }
        public int UpdateAdd_surgery_stock(surgery_stock stock)
        {
            string coAdims_str = "select count(*) from adims_surgery_stock where medicine_number='" + stock.medicine_number + "'" + " And surgery_id='" + stock.surgery_id + "'";
            int count = (int)dBConn.ExecuteScalar(coAdims_str);
            if (0 == count) { return Add_surgery_stock(stock); }
            else
            {
                coAdims_str = "update adims_surgery_stock set count=count+'" + stock.count + "'" + " where medicine_number='" +
               stock.medicine_number + "'" + " And surgery_id='" + stock.surgery_id + "'";
                return dBConn.ExecuteNonQuery(coAdims_str);
            }
        }
        public DataSet select_surgery_input()
        {
            string coAdims_str = "select Adims_surgery_input.medicine_number 药品编号," +
                "Adims_surgery_input.input_count 输入数量,Adims_surgery_input.input_time 输入时间,Adims_surgery_input.confirm_person 确认人," +
                "medicine_name 药品名称,phonetic_prefix 拼音字头,toxicology 毒理,state 状态," +
                "dosagy_form 剂型,specification 规格,produce_time 生产日期,deadline 有效日期," +
                "batch_number 批次,origin_place 产地 from Adims_medicine_info,Adims_surgery_input " +
                "where Adims_medicine_info.medicine_number=Adims_surgery_input.medicine_number order by Adims_surgery_input.input_time DESC";
            return dBConn.GetDataSet(coAdims_str);
        }
        public int TestSub_surgery_stock(surgery_output output)
        {
            string coAdims_str = "select count(*) from adims_surgery_stock where medicine_number='" + output.medicine_number + "'" +
                " And surgery_id='" + output.surgery_id + "'";
            int count = (int)dBConn.ExecuteScalar(coAdims_str);
            if (count == 1)
            {
                coAdims_str = "select count(*) from adims_surgery_stock where medicine_number='" + output.medicine_number + "'" +
                    " And surgery_id='" + output.surgery_id + "'" +
                   " And count>='" + output.output_count + "'";
                count = (int)dBConn.ExecuteScalar(coAdims_str);
                if (count == 1) return 2;
                else return 1;
            }

            else return 0;
        }
        public int Add_surgery_output(surgery_output output)
        {
            if (TestSub_surgery_stock(output) == 2)
            {
                string coAdims_str = "insert into adims_surgery_output(surgery_id,room_id,medicine_number,output_count,output_time,confirm_person) values('" +
        output.surgery_id + "','" + output.room_id + "','" + output.medicine_number + "','" + output.output_count + "','" + output.output_time + "','" + output.confirm_person + "')";
                return dBConn.ExecuteNonQuery(coAdims_str);
            }
            else
                return -2;
        }
        public int UpdateSub_surgery_stock(surgery_output output)
        {
            if (TestSub_surgery_stock(output) == 2)
            {
                string coAdims_str = "update adims_surgery_stock set count=count-'" + output.output_count + "'" + " where medicine_number='" +
                output.medicine_number + "'" + " And surgery_id='" + output.surgery_id + "'";
                return dBConn.ExecuteNonQuery(coAdims_str);
            }
            return 0;
        }
        public DataSet select_surgery_output()
        {
            string coAdims_str = "select Adims_surgery_output.room_id 手术间号,Adims_surgery_output.medicine_number 药品编号," +
                "Adims_surgery_output.output_count 输出数量,Adims_surgery_output.output_time 输出时间,Adims_surgery_output.confirm_person 确认人," +
                "medicine_name 药品名称,phonetic_prefix 拼音字头,toxicology 毒理,state 状态," +
                "dosagy_form 剂型,specification 规格,produce_time 生产日期,deadline 有效日期," +
                "batch_number 批次,origin_place 产地 from adims_medicine_info,adims_surgery_output " +
                "where adims_medicine_info.medicine_number=adims_surgery_output.medicine_number order by Adims_surgery_output.output_time DESC";
            return dBConn.GetDataSet(coAdims_str);
        }
        public int Test_surgery_checkin(surgery_checkin checkin)
        {
            string coAdims_str = "select count(*) from adims_surgery_output where surgery_id='" + checkin.surgery_id +
                "' And room_id='" + checkin.room_id + "' And medicine_number='" + checkin.medicine_number + "'";
            int count = (int)dBConn.ExecuteScalar(coAdims_str);
            if (count > 0) return 1;
            else return 0;
            // 这只是对核销的有效性进行的简单测试，还有待完善   
        }
        public int Add_surgery_checkin(surgery_checkin checkin)
        {
            if (Test_surgery_checkin(checkin) == 1)
            {
                string coAdims_str = "insert into adims_surgery_checkin(surgery_id,room_id,medicine_number,count,confirm_person,checkin_time) values('" +
                  checkin.surgery_id + "','" + checkin.room_id + "','" + checkin.medicine_number + "','" + checkin.count + "','" + checkin.confirm_person +
                  "','" + checkin.checkin_time + "')";
                return dBConn.ExecuteNonQuery(coAdims_str);
            }
            else
                return 0;
        }
        public int Update_surgery_stock(surgery_checkin checkin)
        {
            if (Test_surgery_checkin(checkin) == 1)
            {
                string coAdims_str = "update adims_surgery_stock set count=count+'" + checkin.count + "' where medicine_number='" +
              checkin.medicine_number + "' And surgery_id='" + checkin.surgery_id + "'";
                return dBConn.ExecuteNonQuery(coAdims_str);
            }
            return 0;
        }
        public DataSet select_surgery_checkin()
        {
            string coAdims_str = "select Adims_surgery_checkin.surgery_id 手术室号,Adims_surgery_checkin.room_id 手术间号,Adims_surgery_checkin.medicine_number 药品编号," +
               "Adims_surgery_checkin.count 核销数量,Adims_surgery_checkin.checkin_time 核销时间,Adims_surgery_checkin.confirm_person 确认人," +
               "medicine_name 药品名称,phonetic_prefix 拼音字头,toxicology 毒理,state 状态," +
               "dosagy_form 剂型,specification 规格,produce_time 生产日期,deadline 有效日期," +
               "batch_number 批次,origin_place 产地 from adims_medicine_info,adims_surgery_checkin " +
               "where adims_medicine_info.medicine_number=adims_surgery_checkin.medicine_number order by Adims_surgery_checkin.checkin_time DESC";
            return dBConn.GetDataSet(coAdims_str);
        }
        public DataSet select_surgery_stock()
        {
            string coAdims_str = "select Adims_surgery_stock.medicine_number 药品编号, medicine_name 药品名称,count 数量," +
                "toxicology 毒理,state 状态,dosagy_form 剂型,specification 规格,batch_number 批次,produce_time 生产日期,deadline 有效日期 " +
                "from Adims_surgery_stock,Adims_medicine_info where Adims_medicine_info.medicine_number=Adims_surgery_stock.medicine_number";
            return dBConn.GetDataSet(coAdims_str);
        }
        #endregion
      
      
        #endregion
       
        #region <<合并方法>>
      

        /// <summary>
        /// 是否应该排班
        /// </summary>
        /// <param name="riqi"></param>
        /// <returns></returns>
        public static bool isPaiBan(string riqi)
        {
            bool b = adims_DAL.AdimsProvider.isPaiBan(riqi);
            return b;
        }
      
        /// <summary>
        /// 判断table的name是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsHaveName(string name, string table)
        {
            return adims_DAL.AdimsProvider.IsHaveName(name, table);

        }





        #endregion

        #region <<方法>>




        public int AddPointPACU(int mzjldid, DateTime dt, string nibps, string nibpd, string rrc, string pulse, string spo2, string etco2, string cvp, string temp)//增加监测点
        {
            string insert = "insert into Adims_Pacu_Point(mzjldid,RecordTime,NIBPS,NIBPD,RRC,Pulse,SpO2,ETCO2,cvp,temp) values ('" + mzjldid + "','" + dt + "','" + nibps + "',"
            + "'" + nibpd + "','" + rrc + "','" + pulse + "','" + spo2 + "','" + etco2 + "','" + cvp + "','" + temp + "')";
            return dBConn.ExecuteNonQuery(insert);
        }









        public DataTable Select_YongYaoList(int mzjldid,int type)
        {
            string SQL = "SELECT name,yl,dw FROM [Adims_YongYaoList] WHERE mzjldid = '" + mzjldid + "' and ypType='"+type+"' ORDER by id";
            return dBConn.GetDataTable(SQL);
        }
        public DataTable SQYY(int mzjldid)
        {
            string SQL = "SELECT ypname, yl,dw FROM Adims_sqyyUse WHERE mzjldid = '" + mzjldid + "' ORDER by id";
            return dBConn.GetDataTable(SQL);
        }
        public DataTable SelectQT(int mzjldid)
        {
            string SQL = "SELECT qtname,yl*DATEDIFF(hh,sytime,jstime) as yl,dw FROM Adims_qtUSE WHERE mzjldid = '" + mzjldid + "' ORDER by qtname";
            return dBConn.GetDataTable(SQL);
        }
        public DataTable select_qt(int mzjldid)
        {
            string SQL = "SELECT * FROM Adims_qtUSE WHERE mzjldid = '" + mzjldid + "' order by qtname";
            return dBConn.GetDataTable(SQL);
        }
        public DataTable select_JMY(int mzjldid)
        {
            string SQL = "SELECT id,mzjldid,name,jl,dw,kssj FROM Adims_jmyUSE WHERE mzjldid = '" + mzjldid + "'order by name asc";
            return dBConn.GetDataTable(SQL);
        }
        public DataTable selectYDY(int mzjldid)
        {
            string SQL = "SELECT * FROM Adims_ydyUSE WHERE mzjldid = '" + mzjldid + "'order by ydyname asc";
            return dBConn.GetDataTable(SQL);
        }
        public int addYongyaoList1(int mzjldid, adims_MODEL.Yongyao yt)
        {
            string sql = "INSERT INTO Adims_YongyaoList(mzjldid,yptype,name,yl,dw,yyfs,cxyy,kstime,flags,Y_zb,hxb,quanxue,xuejiang) "
                + "values('" + mzjldid + "','" + yt.YpType + "','" + yt.Name + "','" + yt.Yl + "','" + yt.Dw + "','" + yt.Yyfs + "','" + (yt.Cxyy ? 1 : 0) + "','" + yt.KsTime + "','1','" + yt.Y_zb + "','" + yt.Hxb + "','" + yt.Quanxue + "','" + yt.Xuejiang + "')";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int addYongyaoList2(int mzjldid, adims_MODEL.Yongyao yt)
        {
            string sql = "INSERT INTO Adims_YongyaoList(mzjldid,yptype,name,yl,dw,yyfs,cxyy,kstime,jstime,flags,Y_zb,hxb,quanxue,xuejiang) "
                + "values('" + mzjldid + "','" + yt.YpType + "','" + yt.Name + "','" + yt.Yl + "','" + yt.Dw + "','" + yt.Yyfs + "','" + (yt.Cxyy ? 1 : 0) + "','" + yt.KsTime + "','" + yt.JsTime + "','2','" + yt.Y_zb + "','" + yt.Hxb + "','" + yt.Quanxue + "','" + yt.Xuejiang + "')";
            return dBConn.ExecuteNonQuery(sql);
        }
         /// <summary>
        /// 结束全麻用药
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="yt"></param>
        /// <returns></returns>
        public int endYongyaoList(int mzjldid, int id)
        {
            string sql = "update Adims_yongyaolist set jstime='" + DateTime.Now + "'"
               +" ,flags='2' where mzjldid='" + mzjldid + "'and id='" + id + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        ///// <summary>
        ///// 结束输液用药
        ///// </summary>
        ///// <param name="mzjldid"></param>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public int endYongyaoListSY(int mzjldid, int id)
        //{
        //    string sql = "update Adims_yongyaolist set jstime='" + DateTime.Now + "'"
        //       + " ,flags='4' where mzjldid='" + mzjldid + "'and id='" + id + "'";
        //    return dBConn.ExecuteNonQuery(sql);
        //}
        /// <summary>
        /// 删除用药
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="yt"></param>
        /// <returns></returns>
        public int deleteYaopinList(int mzjldid, int id)
        {
            return dBConn.ExecuteNonQuery("DELETE FROM Adims_yongyaolist WHERE mzjldid='" + mzjldid + "'and id = '" + id + "' ");
        }

        public int addjt(int mzjldid, adims_MODEL.jtytsx jt)//添加局麻药
        {
            return dBConn.ExecuteNonQuery("INSERT INTO Adims_jmyUSE(mzjldid,name,Cxyy,jl,dw,zrfs,kssj,flags) values('" + mzjldid + "','" + jt.Name + "','" + (jt.Cxyy ? 1 : 0) + "','" + jt.Jl + "','" + jt.Dw + "','" + jt.Zrfs + "','" + jt.Kssj + "','1')");
        }
        public DataTable selectJumaYao(int mzjldid)//添加局麻药
        {
            return dBConn.GetDataTable("select * from Adims_jmyUSE where mzjldid='" + mzjldid + "'");
        }
        public int addshuxue(int mzjldid, adims_MODEL.shuxue jt)//添加输血
        {
            string SQL = "INSERT INTO Adims_shuxueUse(mzjldid,shuxuename,jl,dw,zrfs,kssj,flags) values('" + mzjldid + "','" + jt.Name + "','" + jt.Jl + "','" + jt.Dw + "','" + jt.Zrfs + "','" + jt.Kssj + "','1')";
            return dBConn.ExecuteNonQuery(SQL);
        }
        public int addshuxuePACU(int mzjldid, adims_MODEL.shuxue jt)//添加输血
        {           
            string SQL = "INSERT INTO Adims_shuxueUse_PACU(mzjldid,shuxuename,jl,dw,zrfs,kssj,cxyy,flags) values('" + mzjldid + "','" + jt.Name + "','" + jt.Jl + "','" + jt.Dw + "','" + jt.Zrfs + "','" + jt.Kssj + "','" + (jt.Cxyy ? 1 : 0) + "','1')";
            return dBConn.ExecuteNonQuery(SQL);
        }
        public int addshuxuePACU2(int mzjldid, adims_MODEL.shuxue jt)//添加输血
        {          
            string SQL = "INSERT INTO Adims_shuxueUse_PACU(mzjldid,shuxuename,jl,dw,zrfs,kssj,jssj,cxyy,flags) values('" + mzjldid + "','" + jt.Name + "','" + jt.Jl + "','" + jt.Dw + "','" + jt.Zrfs + "','" + jt.Kssj + "','" + jt.Jssj + "','" + (jt.Cxyy ? 1 : 0) + "','2')";
            return dBConn.ExecuteNonQuery(SQL);
        }
        public int addshuye(int mzjldid, adims_MODEL.shuye jt)//添加输液
        {
            string SQL="INSERT INTO Adims_shuyeUse(mzjldid,shuyename,jl,dw,zrfs,kssj,flags) values('" + mzjldid + "','" + jt.Name + "','" + jt.Jl + "','" + jt.Dw + "','" + jt.Zrfs + "','" + jt.Kssj + "','1')";
            return dBConn.ExecuteNonQuery(SQL);
        }
        public DataTable selectSX(int mzjldid)//查找输血
        {
            string SQL = "select * from Adims_shuxueUse where mzjldid='" + mzjldid + "' ORDER BY shuxuename ASC";
            return dBConn.GetDataTable(SQL);
        }
         public DataTable selectSX1(int mzjldid)//查找输血
        {
            string SQL = "select id,mzjldid,shuxuename,jl,dw,zrfs,kssj from Adims_shuxueUse where mzjldid='" + mzjldid + "'";
            return dBConn.GetDataTable(SQL);
        }
         public DataTable selectSY1(int mzjldid)//查找输液
         {
             string SQL = "select id,mzjldid,shuyename,jl,dw,zrfs,kssj from Adims_shuyeUse where mzjldid='" + mzjldid + "'";
             return dBConn.GetDataTable(SQL);
         }
         public DataTable selectSX_pacu(int mzjldid)//查找pacu输血
         {
             string SQL = "select id,mzjldid,shuxuename,jl,dw,zrfs,kssj,jssj,cxyy,flags from Adims_shuxueUse_PACU where mzjldid='" + mzjldid + "'";
             return dBConn.GetDataTable(SQL);
         }
         public DataTable selectSY_pacu(int mzjldid)//查找输血
         {
             string SQL = "select id,mzjldid,shuyename,jl,dw,zrfs,kssj,jssj,cxyy,flags from Adims_shuyeUse_PACU where mzjldid='" + mzjldid + "'";
             return dBConn.GetDataTable(SQL);
         }

        
        public DataTable selectSY(int mzjldid)//查找输液
        {
            string SQL = "select * from Adims_shuyeUse where mzjldid='" + mzjldid + "' ORDER BY shuyename ASC";
            return dBConn.GetDataTable(SQL);
        }
        public int addshuyePACU(int mzjldid, adims_MODEL.shuye jt)//添加输液
        {
            //string sql = "INSERT INTO Adims_YongyaoList(mzjldid,yptype,name,yl,dw,yyfs,cxyy,kstime,flags) "
            // + "values('" + mzjldid + "','" + yt.YpType + "','" + yt.Name + "','" + yt.Yl + "','" + yt.Dw + "','" + yt.Yyfs + "','" + (yt.Cxyy ? 1 : 0) + "','" + yt.KsTime + "','1')";
            string SQL = "INSERT INTO Adims_shuyeUse_PACU(mzjldid,shuyename,jl,dw,zrfs,kssj,cxyy,flags) values('" + mzjldid + "','" + jt.Name + "','" + jt.Jl + "','" + jt.Dw + "','" + jt.Zrfs + "','" + jt.Kssj + "','" + (jt.Cxyy ? 1 : 0) + "','1')";
            return dBConn.ExecuteNonQuery(SQL);
        }
        public int addshuyePACU2(int mzjldid, adims_MODEL.shuye jt)//添加输液
        {
            //string sql = "INSERT INTO Adims_YongyaoList(mzjldid,yptype,name,yl,dw,yyfs,cxyy,kstime,jstime,flags) "
            //   + "values('" + mzjldid + "','" + yt.YpType + "','" + yt.Name + "','" + yt.Yl + "','" + yt.Dw + "','" + yt.Yyfs + "','" + (yt.Cxyy ? 1 : 0) + "','" + yt.KsTime + "','" + yt.JsTime + "','2')";
            string SQL = "INSERT INTO Adims_shuyeUse_PACU(mzjldid,shuyename,jl,dw,zrfs,kssj,jssj,cxyy,flags) values('" + mzjldid + "','" + jt.Name + "','" + jt.Jl + "','" + jt.Dw + "','" + jt.Zrfs + "','" + jt.Kssj + "','"+jt.Jssj+"','" + (jt.Cxyy ? 1 : 0) + "','2')";
            return dBConn.ExecuteNonQuery(SQL);
        }
      
        public DataTable GETsxsy_PACU(int mzjldid,int TYPE)//添加晶体胶体输血
        {
            return dBConn.GetDataTable("select * from Adims_PACU_sxsy where mzjldid='" + mzjldid + "'and TYPE='" + TYPE + "'");
        }
        public int endsxsy_PACU(int mzjldid, adims_MODEL.jtytsx jt)//结束晶体等
        {
            string SQL="update Adims_PACU_sxsy set jssj='" + jt.Jssj + "',flags='2' where mzjldid='" + mzjldid + "'and name='" + jt.Name + "'";

            return dBConn.ExecuteNonQuery(SQL);
        }
        public int endjt(int mzjldid, DateTime dt,int id)//结束局麻药
        {
            return dBConn.ExecuteNonQuery("update Adims_jmyUSE set jssj='" + dt + "',flags='2' where id='" + id + "'");
        }
        public int endshuxue(int mzjldid, int id)//结束输血
        {
            return dBConn.ExecuteNonQuery("update Adims_shuxueUSE set jssj='" + DateTime.Now+ "',flags='2' where id='" + id + "'");
        }
        public int endshuxuePACU(int mzjldid, adims_MODEL.shuxue jt)//结束输血
        {
            return dBConn.ExecuteNonQuery("update Adims_shuxueUSE_PACU set jssj='" + jt.Jssj + "',flags='2' where mzjldid='" + mzjldid + "'and shuxuename='" + jt.Name + "'");
        }
        public int endshuxuePACUs(int mzjldid, int id)//结束输血
        {
            return dBConn.ExecuteNonQuery("update Adims_shuxueUSE_PACU set jssj='" + DateTime.Now + "',flags='2' where mzjldid='" + mzjldid + "'and id='" + id + "'");
        }
        public int endshuye(int mzjldid, int id)//结束输液
        {
            return dBConn.ExecuteNonQuery("update Adims_shuyeUSE set jssj='" + DateTime.Now + "',flags='2' where id='" + id + "'");
        }
        public int endshuyePACU(int mzjldid, adims_MODEL.shuye jt)//结束输液
        {
            return dBConn.ExecuteNonQuery("update Adims_shuyeUSE_PACU set jssj='" + jt.Jssj + "',flags='2' where mzjldid='" + mzjldid + "'and shuyename='" + jt.Name + "'");
        }
        public int endshuyePACUs(int mzjldid, int id)//结束输液
        {
            //string sql = "update Adims_yongyaolist set jstime='" + DateTime.Now + "'"
            //  + " ,flags='2' where mzjldid='" + mzjldid + "'and id='" + id + "'";
            return dBConn.ExecuteNonQuery("update Adims_shuyeUSE_PACU set jssj='" + DateTime.Now + "',flags='2' where mzjldid='" + mzjldid + "'and id='" + id + "'");
        }

        public int deljt(int id)//删除晶体等
        {
            return dBConn.ExecuteNonQuery("DELETE FROM Adims_jmyUSE WHERE id='" + id + "'");
        }
        public int delshuxue(int mzjldid, int id)//删除输血
        {
            return dBConn.ExecuteNonQuery("DELETE FROM Adims_shuxueUSE WHERE mzjldid='" + mzjldid + "'and id = '" + id + "'  ");
        }
        public int delshuxuePACU(int mzjldid, int id)//删除输血
        {
            return dBConn.ExecuteNonQuery("DELETE FROM Adims_shuxueUSE_PACU WHERE mzjldid='" + mzjldid + "'and id = '" + id + "'  ");
        }
        public int delshuye(int mzjldid, int id)//删除输液
        {
            return dBConn.ExecuteNonQuery("DELETE FROM Adims_shuyeUSE WHERE mzjldid='" + mzjldid + "'and id = '" + id + "'  ");
        }
        public int delshuyePACU(int mzjldid, int id)//删除输液
        {
            return dBConn.ExecuteNonQuery("DELETE FROM Adims_shuyeUSE_PACU WHERE mzjldid='" + mzjldid + "'and id = '" + id + "'  ");
        }

        public int addpoint_PACU(int mzjldid, List<adims_MODEL.point> pointList)//添加点
        {
            string sql = string.Empty;
            foreach (adims_MODEL.point p in pointList)
            {
                sql += "INSERT INTO Adims_Point_PACU(mzjldid,lx,value,time) values('" + mzjldid + "','" + p.Lx + "','" + p.V + "','" + p.D + "')";
            }
            return dBConn.ExecuteNonQuery(sql);
        }
        public int addpoint(int mzjldid, List<adims_MODEL.point> pointList)//添加点
        {
            string sql = string.Empty;
            foreach (adims_MODEL.point p in pointList)
            {
                sql += "INSERT INTO Adims_Point(mzjldid,lx,value,time) values('" + mzjldid + "','" + p.Lx + "','" + p.V + "','" + p.D + "')";
            }
            return dBConn.ExecuteNonQuery(sql);
        }

        public int xgqtKssj(int mzjldid, adims_MODEL.Yongyao qt)//修改气体
        {
            return dBConn.ExecuteNonQuery("update Adims_Qtuse set sytime='" + qt.KsTime + "' where mzjldid='" + mzjldid + "'and id='" + qt.Id + "'");
        }

        public int xgqtJssj(int mzjldid, adims_MODEL.Yongyao qt)//修改气体
        {
            return dBConn.ExecuteNonQuery("update Adims_Qtuse set jstime='" + qt.JsTime + "' where mzjldid='" + mzjldid + "'and id='" + qt.Id + "'");
        }
        public int xgqtPACU(int mzjldid, adims_MODEL.mzqt qt)//修改气体
        {
            return dBConn.ExecuteNonQuery("update Adims_PACU_qtUse set sytime='" + qt.Sysj + "' where mzjldid='" + mzjldid + "'and id='" + qt.Id+ "'");
        }
        public int xgqtPACU1(int mzjldid, adims_MODEL.mzqt qt)//修改气体
        {
            return dBConn.ExecuteNonQuery("update Adims_PACU_qtUse set jstime='" + qt.Jssj + "' where mzjldid='" + mzjldid + "'and id='" + qt.Id + "'");
        }

        public int xgshuyeKSSJ(int mzjldid, adims_MODEL.Yongyao qt)//修改输液时间
        {
            return dBConn.ExecuteNonQuery("update Adims_shuyeUSE set KSSJ='" + qt.KsTime + "',jssj='" + qt.JsTime + "' where mzjldid='" + mzjldid + "'and id='" + qt.Id + "'");
        }
        public int xgshuxueKSSJ(int mzjldid, adims_MODEL.Yongyao qt)//修改输血时间
        {
            return dBConn.ExecuteNonQuery("update Adims_shuxueUSE set KSSJ='" + qt.KsTime + "',jssj='" + qt.JsTime + "' where mzjldid='" + mzjldid + "'and id='" + qt.Id + "'");
        }
        public int xgszsjTime(int mzjldid, adims_MODEL.szsj szsj1)//修改输血时间
        {
            return dBConn.ExecuteNonQuery("update Adims_szsj set time='" + szsj1.D + "',y_zb='"+szsj1.Y_zb+"' where mzjldid='" + mzjldid + "'and id='" + szsj1.Id + "'");
        }
        public int xgszsjTimePacu(int mzjldid, adims_MODEL.szsj szsj1,DateTime DT)//修改输血时间
        {
           string update=" update Adims_pacu_shijian set time='" + DT + "' where id='" + szsj1.Id + "'";

           return dBConn.ExecuteNonQuery(update);
        }
        //public int xgtsyyTime(int mzjldid, adims_MODEL.Yongyao tsyy1)
        //{
        //    //return dBConn.ExecuteNonQuery("update Adims_tsyy set time='" + tsyy1.KsTime + "' where mzjldid='" + mzjldid + "'and id='" + tsyy1.Id + "'");
        //    return dBConn.ExecuteNonQuery("update Adims_YongYaoList set ksTime='" + tsyy1.KsTime + "',Y_zb='" + tsyy1.Y_zb + "' where mzjldid='" + mzjldid + "'and id='" + tsyy1.Id + "'");
        //}
        /// <summary>
        /// 修改特殊用药坐标
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="tsyy1"></param>
        /// <returns></returns>
        public int xgtsyyZB(int mzjldid, adims_MODEL.Yongyao tsyy1)
        {
            return dBConn.ExecuteNonQuery("update Adims_YongYaoList set  ksTime='" + tsyy1.KsTime + "',Y_zb='" + tsyy1.Y_zb + "' where mzjldid='" + mzjldid + "'and id='" + tsyy1.Id + "'");
        }
        public int xgtsyyTimePACU(int mzjldid, adims_MODEL.tsyy tsyy1, DateTime DT)//修改输血时间
        {
            string sql = "update Adims_PACU_yyUse set yytime='" + DT + "' where id='" + tsyy1.Id + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int xgmzpmTime(int mzjldid, adims_MODEL.mzpingmian qt)//修改麻醉平面时间
        {
            return dBConn.ExecuteNonQuery("update Adims_mzpm set time='" + qt.D + "' where mzjldid='" + mzjldid + "'and id='" + qt.Id + "'");
        }

        public int UpdateYongyaoKssj(int mzjldid, adims_MODEL.Yongyao yt)//修改用药开始时间
        {
            string sql = "update Adims_YongyaoList set kstime='" + yt.KsTime + "' where mzjldid='" + mzjldid + "'and id='" + yt.Id + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int UpdateYongyaoKssjJssj(int mzjldid, adims_MODEL.Yongyao yt)//修改用药开始和结束时间
        {
            string sql = "update Adims_YongyaoList set kstime='" + yt.KsTime + "',jstime='" + yt.KsTime + "' where id='" + yt.Id + "'";
       
            return dBConn.ExecuteNonQuery(sql);
        }
        public int UpdateYongyaoJssj(int mzjldid, adims_MODEL.Yongyao yt)//修改用药结束时间
        {
            string sql = "update Adims_YongyaoList set jstime='" + yt.JsTime + "' where mzjldid='" + mzjldid + "'and id='" + yt.Id + "'";
            return dBConn.ExecuteNonQuery(sql);
        }

        public int xgjmyKssjJssj(int mzjldid, adims_MODEL.Yongyao jt)//修改晶体等
        {
            return dBConn.ExecuteNonQuery("update Adims_jmyUSE set kstime='" + jt.KsTime + "'，kssj='" + jt.KsTime + "' where mzjldid='" + mzjldid + "'and name='" + jt.Name + "'");
        }
        public int xg_PACU_sx(int mzjldid, adims_MODEL.shuxue jt)//修改输血 
        {
            return dBConn.ExecuteNonQuery("update Adims_shuxueUSE_PACU set kssj='" + jt.Kssj + "' where mzjldid='" + mzjldid + "'and id='" + jt.Id + "'");
        }
        public int xg_PACU_sxs(int mzjldid, adims_MODEL.shuxue jt)//修改输血
        {
            return dBConn.ExecuteNonQuery("update Adims_shuxueUSE_PACU set jssj='" + jt.Jssj + "' where mzjldid='" + mzjldid + "'and id='" + jt.Id + "'");
        }
        public int xg_PACU_sy(int mzjldid, adims_MODEL.shuye jt)//修改输液
        {     
            return dBConn.ExecuteNonQuery("update Adims_shuyeUSE_PACU set kssj='" + jt.Kssj + "' where mzjldid='" + mzjldid + "'and id='" + jt.Id + "'");
        }
        public int xg_PACU_sys(int mzjldid, adims_MODEL.shuye jt)//修改输液
        {        
            return dBConn.ExecuteNonQuery("update Adims_shuyeUSE_PACU set jssj='" + jt.Jssj + "' where mzjldid='" + mzjldid + "'and id='" + jt.Id + "'");
        }
    
        public DataTable GetPointPacu(int mzjldid)//查询监测点
        {
            string insert = @"select mzjldid,RecordTime,NIBPS,NIBPD,RRC,Pulse,SpO2,ETCO2,CVP,qdy,sdz,jsz,temp 
from Adims_pacu_Point where mzjldid='" + mzjldid + "' ORDER BY RecordTime ASC";
            return dBConn.GetDataTable(insert);
        }
     
        public DataTable GetMaxPointPacu(int mzjldid)//查询监测点
        {
            string insert = "select max(RecordTime) from Adims_Pacu_Point where mzjldid='" + mzjldid + "'";
            return dBConn.GetDataTable(insert);
        }
    
        public DataTable GetPointSinglePACU(int mzjldid, DateTime dt)//查询监测点
        {
            string insert = "select mzjldid,RecordTime,NIBPS,NIBPD,RRC,Pulse,SpO2,ETCO2 from Adims_PACU_Point where mzjldid='" + mzjldid + "' and RecordTime='" + dt + "'";
            return dBConn.GetDataTable(insert);
        }
        public DataTable GetPointSinglePacu(int mzjldid, DateTime dt)//查询监测点
        {
            string insert = "select mzjldid,RecordTime,NIBPS,NIBPD,RRC,Pulse,SpO2,ETCO2 from Adims_PACU_Point where mzjldid='" + mzjldid + "' and RecordTime='" + dt + "'";
            return dBConn.GetDataTable(insert);
        }
       
        public int AddPointPacu(int mzjldid, DateTime dt, string nibps, string nibpd, string rrc, string pulse, string spo2, string etco2, string cvp, string qdy, string sdz, string jsz, string temp)//增加监测点
        {
            string insert = "insert into Adims_Pacu_Point(mzjldid,RecordTime,NIBPS,NIBPD,RRC,Pulse,SpO2,ETCO2,cvp,qdy,sdz,jsz,temp) values ('" + mzjldid + "','" + dt + "','" + nibps + "',"
            + "'" + nibpd + "','" + rrc + "','" + pulse + "','" + spo2 + "','" + etco2 + "','" + cvp + "','" + qdy + "','" + sdz + "','" + jsz + "','" + temp + "')";
            return dBConn.ExecuteNonQuery(insert);
        }
     
        public int UpdatePointPACU(int mzjldid, DateTime dt, string nibps, string nibpd, string rrc, string pulse, string spo2, string etco2, string temp, string cvp, string qdy, string sdz, string jsz)//修改监测点
        {
            string insert = "UPDATE  Adims_PACU_Point set NIBPS = '" + nibps + "',NIBPD= '" + nibpd + "',RRC= '" + rrc + "',Pulse= '" + pulse + "',"
            + "SpO2= '" + spo2 + "',ETCO2= '" + etco2 + "',temp= '" + temp + "',cvp= '" + cvp + "',qdy= '" + qdy + "',sdz= '" + sdz + "',jsz= '" + jsz + "'  where mzjldid='" + mzjldid + "' and RecordTime='" + dt + "'";
            return dBConn.ExecuteNonQuery(insert);
        }
      
        public int DeletePointPACU(int mzjldid, DateTime dt)//删除监测点
        {
            string insert = "delete from  Adims_PACU_Point where mzjldid='" + mzjldid + "' and RecordTime='" + dt + "'";
            return dBConn.ExecuteNonQuery(insert);
        }
 
     

 
        public int xgpointPACU(int mzjldid, adims_MODEL.point p)//修改点
        {
            string SQL = "";
            if (p.Lx == 1)
                SQL = "update Adims_PACU_Point set NIBPS='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            else if (p.Lx == 2)
                SQL = "update Adims_PACU_Point set NIBPD='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            else if (p.Lx == 3)
                SQL = "update Adims_PACU_Point set pulse='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            else if (p.Lx == 4)
                SQL = "update Adims_PACU_Point set RRC='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            else if (p.Lx == 5)
                SQL = "update Adims_PACU_Point set TEMP='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            //else if (p.Lx == 6)
            //    SQL = "update Adims_PACU_Point set SpO2='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            //else if (p.Lx == 7)
            //    SQL = "update Adims_PACU_Point set ETCO2='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            //else if (p.Lx == 8)
            //    SQL = "update Adims_PACU_Point set CVP='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            //else if (p.Lx == 9)
            //    SQL = "update Adims_PACU_Point set TOF='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";

            return dBConn.ExecuteNonQuery(SQL);
        }
        public int xgpointPACU_TW(int mzjldid, adims_MODEL.tw_point p)//修改点
        {
            string SQL = "";            
             if (p.Lx == 5)
                SQL = "update Adims_PACU_Point set TEMP='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";          
            return dBConn.ExecuteNonQuery(SQL);
        }
       

        //public int addjhxm(int mzjldid, adims_MODEL.jhxm jh)//添加监护数据
        //{
        //    return dBConn.ExecuteNonQuery("INSERT INTO Adims_jhxm(mzjldid,lx,value,time) values('" + mzjldid + "','" + jh.Sy + "','" + jh.V + "','" + jh.D + "')");
        //}
        public int addjhxm_PACU(int mzjldid, adims_MODEL.jhxm jh)//添加监护数据
        {
            return dBConn.ExecuteNonQuery("INSERT INTO Adims_jhxm_PACU(mzjldid,lx,value,time) values('" + mzjldid + "','" + jh.Sy + "','" + jh.V + "','" + jh.D + "')");
        }

      

        public int addclcxqt(int mzjldid, adims_MODEL.clcxqt cl)//添加出血量和尿量
        {
            return dBConn.ExecuteNonQuery("INSERT INTO Adims_cl(mzjldid,lx,value,time) values('" + mzjldid + "','" + cl.Lx + "','" + cl.V + "','" + cl.D + "')");
        }
        public int addmzpm(int mzjldid, adims_MODEL.mzpingmian mzpm)//添加出血量和尿量
        {
            string sqladd="INSERT INTO Adims_mzpm(mzjldid,mzpmName,time) values('" + mzjldid + "','" + mzpm.mzpmName + "','" + mzpm.D + "')";
            return dBConn.ExecuteNonQuery(sqladd);
        }
        public DataTable GetJhxm(int mzjldid)//查询监护项目
        {
            string sqladd = "select jhName from Adims_mzjld_jhxm where mzjldid='" + mzjldid + "'";
            return dBConn.GetDataTable(sqladd);
        }
        public DataTable GetJhxmPACU(int mzjldid)//查询监护项目
        {
            string sqladd = "select jhName from Adims_PACU_jhxm where mzjldid='" + mzjldid + "'";
            return dBConn.GetDataTable(sqladd);
        }
        public int addJhxm(int mzjldid, string jhname,int type)//添加出血量和尿量
        {
            string sqladd ="";
            if (type==0)
                   sqladd = "INSERT INTO Adims_mzjld_jhxm(mzjldid,jhName) values('" + mzjldid + "','" + jhname + "')";
            else if (type==1)
                   sqladd = "INSERT INTO Adims_Pacu_jhxm(mzjldid,jhName) values('" + mzjldid + "','" + jhname + "')";
            return dBConn.ExecuteNonQuery(sqladd);
        }
        public int addJhxmPACU(int mzjldid, string jhname)//添加出血量和尿量
        {
            string sqladd = "INSERT INTO Adims_PACU_jhxm(mzjldid,jhName) values('" + mzjldid + "','" + jhname + "')";
            return dBConn.ExecuteNonQuery(sqladd);
        }
        public int DeleteJhxm(int mzjldid, string jhname,int type)//添加出血量和尿量
        {
            string sqladd ="";
            if (type==0)
                 sqladd = "delete from Adims_mzjld_jhxm where mzjldid='" + mzjldid + "'and jhName='" + jhname + "'";
            if (type == 1)
                sqladd = "delete from Adims_PACU_jhxm where mzjldid='" + mzjldid + "'and jhName='" + jhname + "'";
            return dBConn.ExecuteNonQuery(sqladd);
        }
        /// <summary>
        /// 出量查询
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public DataTable Getcl_PACU(int mzjldid, int lx)
        {
            string sqladd = "select * from Adims_PACU_cl where mzjldid='" + mzjldid + "' and lx='" + lx + "' order by time";
            return dBConn.GetDataTable(sqladd);
        }
        public int addclcxqt_PACU(int mzjldid, adims_MODEL.clcxqt cl)//添加出血量和尿量
        {
            return dBConn.ExecuteNonQuery("INSERT INTO Adims_PACU_cl(mzjldid,lx,value,time) values('" + mzjldid + "','" + cl.Lx + "','" + cl.V + "','" + cl.D + "')");
        }
        public int updateclcxqt_PACU(int id, string value)
        {
            return dBConn.ExecuteNonQuery("update Adims_PACU_cl set value ='"+value+"' where id='"+id+"'");
        }
        public int deleteclcxqt_PACU(int id )
        {
            return dBConn.ExecuteNonQuery("delete Adims_PACU_cl  where id='" + id + "'");
        }
        public int xgclsj(int mzjldid, adims_MODEL.clcxqt cl)//修改出量时间
        {
            string sqlU = "update Adims_cl set time='" + cl.D + "' where id='" + cl.Id + "'";
            return dBConn.ExecuteNonQuery(sqlU);
        }
        public int xgclValue(int mzjldid,int type, adims_MODEL.clcxqt cl)//修改出量时间
        {
            string sqlU = "";
            if (type == 1)
                sqlU = "update Adims_cl set Value='" + cl.V + "' where id='" + cl.Id + "'";
            else if (type == 2)
                sqlU = "update Adims_PACU_cl set Value='" + cl.V + "' where id='" + cl.Id + "'";
            return dBConn.ExecuteNonQuery(sqlU);
        }
        public int DelteCl(int mzjldid, adims_MODEL.clcxqt cl)//修改出量时间
        {
            string sqlU = "delete from Adims_cl  where id='" + cl.Id + "'";
            return dBConn.ExecuteNonQuery(sqlU);
        }
        public int xgclsjPACU(int mzjldid, adims_MODEL.clcxqt cl)//修改出量时间
        {
          string SQL= "update Adims_PACU_cl set time='" + cl.D + "' where id='" + cl.Id + "'";
         return dBConn.ExecuteNonQuery(SQL);
        }

        public int clid(int mzjldid, adims_MODEL.clcxqt cl)//查询出量ID
        {
            return (int)dBConn.ExecuteScalar("select id from Adims_cl where mzjldid='" + mzjldid + "' and lx='" + cl.Lx + "' and value='" + cl.V + "' and time='" + cl.D + "'");
        }
        public int GETmzpm_ID(int mzjldid, adims_MODEL.mzpingmian mzpm)//查询麻醉平面ID
        {
            return (int)dBConn.ExecuteScalar("select id from Adims_mzpm where mzjldid='" + mzjldid + "' and mzpmName='" + mzpm.mzpmName + "' and time='" + mzpm.D + "'");
        }        
        public int delmzpm(int mzjldid, adims_MODEL.mzpingmian qt)
        {
            string sql="DELETE FROM Adims_mzpm WHERE mzjldid='" + mzjldid + "'and id = '" + qt.Id + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        public DataTable GETALLmzpm(int mzjldid)//查询麻醉平面ID
        {
            return dBConn.GetDataTable("select * from Adims_mzpm where mzjldid='" + mzjldid + "' ");
        }
        public DataTable GETALLSZSJ(int mzjldid)//查询麻醉平面ID
        {
            return dBConn.GetDataTable("select * from Adims_SZSJ where mzjldid='" + mzjldid + "' ORDER BY TIME ASC");
        }
        public int clid_PACU(int mzjldid, adims_MODEL.clcxqt cl)//查询出量ID
        {
            string SQL = "select id from Adims_PACU_cl where mzjldid='" + mzjldid + "' and lx='" + cl.Lx + "' and value='" + cl.V + "' and time='" + cl.D + "'";
            return (int)dBConn.ExecuteScalar(SQL);
        }

      

        public void seloroomstate(List<adims_MODEL.oroomstate> oroom) //手术室个数
        {
            DataSet ds = new DataSet();
            ds = dBConn.GetDataSet("select name,state,mzjldid,patid from ssjstate");
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                adims_MODEL.oroomstate o = new adims_MODEL.oroomstate();
                o.Mzjldid = r["mzjldid"].ToString();
                o.Ostate = Convert.ToInt32(r["state"].ToString());
                o.Oname = r["name"].ToString();
                o.Patid = r["patid"].ToString();                
                oroom.Add(o);
            }

        }
        public DataTable slectOroomINFO(string oroom) //手术室信息
        {
            string sql = "select state,mzjldid,patid from ssjstate where  name='" + oroom + "'";
            return dBConn.GetDataTable(sql);
        }
        //public void oroombaseinfo(adims_MODEL.ssqk ssqk) //手术室情况
        //{
        //    DataSet ds = new DataSet();
        //    ds = dBConn.GetDataSet("select Adims_OperSchedule.patname,Adims_Mzjld.yssss,Adims_mzks.time,Adims_Mzjld.mzys from Adims_OperSchedule,Adims_Mzjld,Adims_mzks where Adims_Mzjld.id='" + ssqk.Mzjldid + "'and Adims_Mzjld.patid = Adims_OperSchedule.id and Adims_mzks.mzjldid=Adims_Mzjld.id and Adims_mzks.sj=1");
        //    if (ds.Tables[0].Rows.Count == 0) return;
        //    ssqk.Hzname = ds.Tables[0].Rows[0][0].ToString();
        //    ssqk.Oname = ds.Tables[0].Rows[0][1].ToString();
        //    ssqk.Kssj = Convert.ToDateTime(ds.Tables[0].Rows[0][2]);
        //    ssqk.Mzys = ds.Tables[0].Rows[0][3].ToString();

        //}

        public int xgsqzd(string patid, string sqzd) //修改术前诊断
        {
            return dBConn.ExecuteNonQuery("update Adims_OperSchedule set pattmd='" + sqzd + "' where patid='" + patid + "'");
        }

        public int xgszzd(string mzjldid, string szzd)//修改术中诊断
        {
            return dBConn.ExecuteNonQuery("update Adims_Mzjld set szzd='" + szzd + "' where id='" + mzjldid + "'");
        }

        public int xgsqyy(int mzjldid, string sqyy)//修改麻醉记录表中的术前用药
        {
            return dBConn.ExecuteNonQuery("update Adims_Mzjld set sqyy='" + sqyy + "' where id='" + mzjldid + "'");
        }
        /// <summary>
        /// 新增术前用药表的数据
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public DataTable GetsqyyUse(int mzid)//术前用药
        {
            return dBConn.GetDataTable("select * from Adims_sqyyUse where mzjldid='" + mzid + "'");
        }
        public DataTable GetsqyyUse(int mzid,string name)//术前用药
        {
            return dBConn.GetDataTable("select * from Adims_sqyyUse where mzjldid='" + mzid + "' and ypname='" + name + "'");
        }
        public int InsertIntoSQYY(int mzjld,string name,string fs,string YL, string dw)
        {
            string insert = "insert into Adims_sqyyUse (mzjldid,ypName,yyfs,yl,dw) values('" + mzjld + "','" + name + "','" + fs + "','" + YL + "','" + dw + "')";
           
            return dBConn.ExecuteNonQuery(string.Format(insert));
        }
        public int deleteSQYY(int mzjld, string id)
        {
            string insert = "delete from Adims_sqyyUse where mzjldid='" + mzjld + "' and id='" + id + "'";
            return dBConn.ExecuteNonQuery(string.Format(insert));
        }
        public int addMZZTY(int mzjld,adims_MODEL.ZhenTongYao ZTY)//增加镇痛药
        {
            string insert = "insert into Adims_mzZTYao (mzjldid,Name,yl,dw) values('" + mzjld + "','" + ZTY.Name + "','" + ZTY.Yl + "','" + ZTY.Dw + "')";
            return dBConn.ExecuteNonQuery(string.Format(insert));
        }
        public int deleteMZZTY(int id)//修改麻醉记录表中的术前用药
        {
            return dBConn.ExecuteNonQuery("delete from Adims_mzZTYao where id='" + id + "'");
        }

        public DataTable getMZZTY(int mzjld)//增加镇痛药
        {
            string insert = "select id,mzjldid,name,yl,dw from Adims_mzZTYao where mzjldid='" + mzjld + "'";
            return dBConn.GetDataTable(string.Format(insert));
        }
        public DataTable getTSYY(int mzjld)//获取特殊用药
        {
            string insert = "select id,mzjldid,name,yl,dw,yyfs,time from Adims_tsyy where mzjldid='" + mzjld + "' ORDER BY TIME ASC";
            return dBConn.GetDataTable(string.Format(insert));
        }
        public int xghbz(int mzjldid, string hbz)//修改合并症
        {
            return dBConn.ExecuteNonQuery("update Adims_Mzjld set hbz='" + hbz + "' where id='" + mzjldid + "'");
        }

        public int xgmzff(int mzjldid, string mzff)//修改麻醉方法
        {
            return dBConn.ExecuteNonQuery("update Adims_Mzjld set mzfa='" + mzff + "' where id='" + mzjldid + "'");
        }

        public int xgnssss(string patid, string nssss)//修改拟实施手术
        {
            return dBConn.ExecuteNonQuery("update Adims_OperSchedule set oname='" + nssss + "' where patid='" + patid + "'");
        }

        public int xgyssss(string patid, string yssss)//修改已实施手术
        {
            return dBConn.ExecuteNonQuery("update Adims_Mzjld set yssss='" + yssss + "' where patid='" + patid + "'");
        }

        public int xgtw(int mzjldid, string tw)//修改体位
        {
            return dBConn.ExecuteNonQuery("update Adims_Mzjld set tw='" + tw + "' where id='" + mzjldid + "'");
        }
        public int clearpaiban(string d)//清空排班
        {
            return dBConn.ExecuteNonQuery("UPDATE Adims_OperSchedule WITH (ROWLOCK) set ap1='',ap2='',ap3='' where CONVERT(varchar, Odate , 23 )='" + d + "'");
        }

        public void quanxian(List<adims_MODEL.quanxian> qx, int zw) //查权限
        {
            DataSet ds = new DataSet();
            ds = dBConn.GetDataSet("select mulu.fml,mulu.zml from mulu,quanxian,zhiwu where quanxian.sy='" + zw + "' and quanxian.muid=mulu.id");
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                adims_MODEL.quanxian t_qx = new adims_MODEL.quanxian();
                t_qx.Ml1 = Convert.ToInt32(r[0]);
                t_qx.Ml2 = Convert.ToInt32(r[1]);
                qx.Add(t_qx);

            }
        }

        public DataSet allzhiwu()
        {

            return dBConn.GetDataSet("select name from zhiwu");

        }
        public DataTable GetAllNSSS()//查询所有实施手术
        {
            string insert = "select name from SSSS ";
            return dBConn.GetDataTable(string.Format(insert));
        }
      
        public DataTable GetSF()
        {
            string insert = "select name from SF ";
            return dBConn.GetDataTable(string.Format(insert));
        }
        public DataTable GetCJ()//厂家
        {
            string insert = "select name from WH_Name";
            return dBConn.GetDataTable(string.Format(insert));
        }
        public DataSet fquanxian(string zhiwu) //没有的权限
        {
            return dBConn.GetDataSet("select mulu.name from quanxian,mulu,zhiwu where zhiwu.name='" + zhiwu + "' and zhiwu.id=quanxian.sy and quanxian.muid=mulu.id");

        }

        public void inss(string s, int i1, int i2) //插入新目录  s名字，i1父目录  i2子目录  i1为-1时 为根目录
        {
            dBConn.ExecuteNonQuery("INSERT INTO mulu(name,fml,zml) values('" + s + "','" + i1 + "','" + i2 + "')");

        }

        public int insertfquanxian(string mlname, string zhiwu)//给指定职务插入不可用权限
        {
            return dBConn.ExecuteNonQuery("INSERT INTO quanxian(sy,muid) select name.id,mulu.id from zhiwu,mulu where zhiwu.name='" + zhiwu + "' and mulu.name='" + mlname + "'");
        }

        public int delfquanxian(string mlname, string zhiwu)//删除指定职务插入不可用权限
        {
            /*
                      int mlid, zhiwuid;
                 zhiwuid= (int)dBConn.ExecuteScalar("select id from zhiwu where zhiwu='" + zhiwu + "'");
                mlid= (int)dBConn.ExecuteScalar("select id from mulu where name='" + mlname + "'");
                return dBConn.ExecuteNonQuery("DELETE FROM quanxian WHERE sy='"+zhiwuid+"' and muid='"+mlid+"' ");*/
            return dBConn.ExecuteNonQuery("DELETE FROM quanxian WHERE sy=(select id from zhiwu where name='" + zhiwu + "') and muid=(select id from mulu where name='" + mlname + "')");
        }

        /// <summary>
        /// 查看职务
        /// </summary>
        /// <returns></returns>
        public DataSet selzhiwu()
        {
            return dBConn.GetDataSet("select id,name from zhiwu");

        }

        /// <summary>
        /// 删除职务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int delzhiwu(int id)
        {

            return dBConn.ExecuteNonQuery("DELETE FROM zhiwu WHERE id='" + id + "'");
        }

        /// <summary>
        /// 删除职务
        /// </summary>
        /// <param name="zhiwu"></param>
        /// <returns></returns>
        public int addzhiwu(string zhiwu)
        {

            return dBConn.ExecuteNonQuery("INSERT INTO zhiwu(name) values('" + zhiwu + "')");
        }

        /// <summary>
        /// 登录判断
        /// </summary>
        /// <param name="zh">用户名</param>
        /// <param name="pw">密码</param>
        /// <returns></returns>
        public bool GetUsers(string zh, string pw)
        {
            string sql = "select Count(*) from Adims_Users WITH (NOLOCK) where User_ID='" + zh + "' and User_pwd='" + pw + "'";
            int result = Convert.ToInt32(dBConn.ExecuteScalar(sql));
            if (result == 1) return true;
            else return false;
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="zh"></param>
        /// <returns></returns>
        public DataSet yhxinxi(string zh)
        {
            return dBConn.GetDataSet("select User_Name, 1 from Adims_Users where User_ID='" + zh + "'");
        }


        #endregion
    }
}
