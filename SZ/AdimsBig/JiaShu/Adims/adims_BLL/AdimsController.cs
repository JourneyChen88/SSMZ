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
        //SQLiteHelper sqlitehelper = new SQLiteHelper();
        AdimsProvider adimsProvider = new AdimsProvider();

        #endregion

        #region <<Methods>>

        #region <<基础资料>>

        /// <summary>
        /// 获取病人信息
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetPat(string sqlWhere)
        {
            return adimsProvider.GetPat(sqlWhere);
        }

        /// <summary>
        /// 查询工作人员
        /// </summary>
        /// <returns></returns>
        public DataTable GetSurgeryStaff(string sqlWhere)
        {
            return adimsProvider.GetSurgeryStaff(sqlWhere);
        }

        /// <summary>
        /// 根据表名查询对应的基础信息
        /// </summary>
        /// <param name="dtName"></param>
        /// <returns></returns>
        public DataTable GetMasterList(string dtName)
        {
            return adimsProvider.GetMasterList(dtName);
        }

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
        public DataSet GetOTypesetting(string oroom, string dt)
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
        public int UpdateOTypesetting(string patID, string column, string value)
        {
            return adimsProvider.UpdateOTypesetting(patID, column, value);
        }

        public DataTable select_staff(int posttype)
        {
            return dBConn.GetDataTable("select * from adims_surgerystaff where posttype='" + posttype + "'");
        }
        #endregion

        #region <<术前访视>>

        /// <summary>
        /// 查询术前访视清单
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetBeforeVisitList(string sqlWhere)
        {
            return adimsProvider.GetBeforeVisitList(sqlWhere);
        }

        /// <summary>
        /// 判断病人访视信息是否存在
        /// </summary>
        /// <param name="patID"></param>
        /// <returns></returns>
        public bool GetBeforeVisitCount(string patID)
        {
            return adimsProvider.GetBeforeVisitCount(patID);
        }
       
        /// <summary>
        /// 添加术前访视
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertBeforeVisit(Dictionary<string, string> dictionary)
        {
            return adimsProvider.InsertBeforeVisit(dictionary);
        }

        /// <summary>
        /// 更新术前访视
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateBeforeVisit(Dictionary<string, string> dictionary)
        {
            return adimsProvider.UpdateBeforeVisit(dictionary);
        }

        #endregion

        #region <<护理记录>>

        /// <summary>
        /// 验证是否存在病人护理记录
        /// </summary>
        /// <param name="patID">病人ID</param>
        /// <returns></returns>
        public bool GetNurseRecordCount(string patID)
        {
            return adimsProvider.GetNurseRecordCount(patID);
        }

        /// <summary>
        /// 添加护理记录
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertNurseRecord(Dictionary<string, string> dictionary)
        {
            return adimsProvider.InsertNurseRecord(dictionary);
        }

        /// <summary>
        /// 更新护理记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int UpdateNurseRecord(Dictionary<string, string> dictionary)
        {
            return adimsProvider.UpdateNurseRecord(dictionary);
        }

        #endregion

        #region <<术后随访>>

        /// <summary>
        /// 验证病人信息是否存在
        /// </summary>
        /// <param name="patID">病人ID</param>
        /// <returns></returns>
        public bool GetAfterVisitCount(string patID)
        {
            return adimsProvider.GetAfterVisitCount(patID);
        }

        /// <summary>
        /// 查询术后随访
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetAfterVisit(string sqlWhere)
        {
            return adimsProvider.GetAfterVisit(sqlWhere);
        }

        /// <summary>
        /// 添加术后随访
        /// </summary>
        /// <param name="afterVisitList"></param>
        /// <returns></returns>
        public int InsertAfterVisit(Dictionary<string, string> afterVisitList)
        {
            return adimsProvider.InsertAfterVisit(afterVisitList);
        }

        /// <summary>
        /// 更新术后随访
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int UpdateAfterVisit(Dictionary<string, string> afterVisitList)
        {
            return adimsProvider.UpdateAfterVisit(afterVisitList);
        }

        #endregion

        #region <<麻醉记录单>>

        /// <summary>
        /// 查询麻醉记录单
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public DataTable GetMzjldList(string sqlWhere)
        {
            return adimsProvider.GetMzjldList(sqlWhere);
        }

        /// <summary>
        /// 根据麻醉医师统计
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetByApList(string sqlWhere)
        {
            return adimsProvider.GetByApList(sqlWhere);
        }
         /// <summary>
        /// 根据手术间统计
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetBySSJList(string sqlWhere)
        {
            return adimsProvider.GetBySSJList(sqlWhere);
        }

        
        /// <summary>
        /// 根据科室统计
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetByDpmList(string sqlWhere)
        {
            return adimsProvider.GetByDpmList(sqlWhere);
        }

        /// <summary>
        /// 获取麻醉记录单所有详情
        /// </summary>
        /// <param name="mzjldID"></param>
        /// <returns></returns>
        public DataSet GetSingleMzjld(int mzjldID)
        {
            return adimsProvider.GetSingleMzjld(mzjldID);
        }

        /// <summary>
        /// 更新麻醉记录单
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int UpdateMzjld(List<string> mzdList)
        {
            return adimsProvider.UpdateMzjld(mzdList);
        }
        /// <summary>
        /// 更新麻醉记录单1
        /// </summary>
        /// <param name="listStr"></param>
        /// <returns></returns>
        public int UpdateMzjld1(List<string> mzdList)
        {
            return adimsProvider.UpdateMzjld1(mzdList);
        }

      
        /// <summary>
        /// 查找指定日期的病人姓名
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public DataTable GetPat(DateTime d)
        {
            return adimsProvider.GetPat("1=1");
        }
       
      
        /// <summary>
        /// 新增诱导药的使用
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertIntoYDY(int mzjld, string name, string fs, string yl, string dw,DateTime now)
        {
            string insert = "insert into [Adims_ydyUse] (mzjldid,ydyname,yyfs,yl,dw,time) values('" + mzjld + "','" + name + "','" + fs + "','" + yl + "','" + dw + "','"+DateTime.Now+"')";

            return dBConn.ExecuteNonQuery(string.Format(insert));
        }
        /// <summary>
        /// 新增术中时间使用
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertIntoSZSJ(int mzjld,string sjname, DateTime now,int type)
        {
            string insert = "";
            if (type == 0)
                insert = "insert into [Adims_Szsj] (mzjldid,name,time) values('" + mzjld + "','" + sjname + "','" + DateTime.Now + "')";
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
            string delete = "delete from [Adims_ydyUse] where id='"+id+"' ";
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
            string delete = "delete from [Adims_Tsyy] where id='" + id + "' ";
            return dBConn.ExecuteNonQuery(string.Format(delete));
        }


        #endregion

        #endregion

        #region     //PACU模块中使用的方法

       
          
        public DataTable PACU_info(string mzid)
        {
            string sql = "select * from Adims_PACU where mzjldid='" + mzid + "'";
            return dBConn.GetDataTable(sql);
        }
        public int SavaPACU(Dictionary<string, string> dictionary)  //病人恢复信息保存
        {
            string _INSERT = "INSERT INTO Adims_PACU(Jisonghuifu,Taitou5miao,Keshoutunyan,Yishi,Qiguandaoguan,Dingxiangli,MZpingmianUP,MZpingmianDOWN,Tengtong,Exin,Jingmaitongchang,ZhentongFF,visitDate,mzjldid)"
                + "values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')";

            return dBConn.ExecuteNonQuery(string.Format(_INSERT, dictionary.Values.ToArray()));
        }
        public int SavaPACU_DATA( Dictionary<string, string> dictionary,string mzid)  //病人恢复信息保存
        {
              string _INSERT = "INSERT INTO Adims_PACU_DATA(mzjldid,timeShort,XueYa,MaiBo,HuXi,SpO2,EKG,HuXiO2,MZpingmian,XueQiFenXi,YongYao,Remark)"
                  +"values('"+mzid+"','{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')";
                   
            return dBConn.ExecuteNonQuery(string.Format(_INSERT, dictionary.Values.ToArray()));
        }
        public int UpdatePACU_DATA(Dictionary<string, string> dictionary, string mzid)  //病人恢复信息保存
        {
            string _INSERT = "Update  Adims_PACU_DATA set XueYa='{1}',MaiBo='{2}',HuXi='{3}',SpO2='{4}',EKG='{5}',HuXiO2='{6}',MZpingmian='{7}',"
            + "XueQiFenXi='{8}',YongYao='{9}',Remark='{10}'  where  mzjldid='" + mzid + "'and timeShort='{0}'";

            return dBConn.ExecuteNonQuery(string.Format(_INSERT, dictionary.Values.ToArray()));
        }
        public DataTable PACU_data(string mzid, string timeShort)
        {
            string sql = "select * from Adims_PACU_DATA where mzjldid='" + mzid + "'and timeShort='" + timeShort + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable allPACU_data(string mzid)
        {
            string sql = "select timeshort,xueya,maibo,huxi,spo2,ekg,huxio2,mzpingmian xueqifenxi,yongyao,remark from Adims_PACU_DATA where mzjldid='" + mzid + "'";
            return dBConn.GetDataTable(sql);
        }
        public int UpdatePACU(Dictionary<string, string> dictionary)  //病人恢复信息修改
        {
            string sql = "Update  Adims_PACU set Jisonghuifu='{0}',Taitou5miao='{1}',Keshoutunyan='{2}',Yishi='{3}',Qiguandaoguan='{4}',Dingxiangli='{5}',MZpingmianUP='{6}',MZpingmianDOWN='{7}',"
            + "Tengtong='{8}',Exin='{9}',Jingmaitongchang='{10}',ZhentongFF='{11}',visitDate='{12}'  where mzjldid='{13}'";

            return dBConn.ExecuteNonQuery(string.Format(sql, dictionary.Values.ToArray()));
        }
        public DataSet PACU_table1( string dtime)
        {
            string SQL = "select  A.patzhuyuanid 住院号,A.patbedno 床位号,A.patname 姓名,A.patsex 性别,B.ID 麻醉编号,B.patid 病人编号,B.brqx 病人去向,A.patage 年龄"
                    + " from Adims_Otypesetting as A LEFT join Adims_Mzjld AS B ON A.Patid=B.Patid WHERE B.brqx='PACU'"
                    + " and CONVERT(varchar, B.Otime , 23 )='" + dtime + "' order by B.time DESC";
            return dBConn.GetDataSet(SQL);
        }

        public DataSet PACU_table2()
        {
            return dBConn.GetDataSet("select B.ID 手术单号, A.patname 姓名,A.patsex 性别,A.patage 年龄,A.patbedno 床号,B.ssmc 手术名称 , B.szzd 术中诊断, B.mzfa 麻醉方式,B.time 手术日期,"
            + "A.patdpm 病区,B.ssys 手术医生,B.mzys 麻醉医生,B.patid 患者ID from Adims_Otypesetting as A,Adims_Mzjld AS B where A.Patid=B.Patid and B.brqx='PACU ' order by time DESC");
        }
        #endregion
        #region   //权限管理
        public DataTable SelectQuanxian(string zhiwu)
        {
            string sql = "select *  from adims_position_jurisdiction where position ='" + zhiwu + "' ";
            return dBConn.GetDataTable(sql);

        }
        public int Upadte_jurisdiction1(string quanxian, string zhiwu)
        {
            string sql = "update  adims_position_jurisdiction set jurisdiction='" + quanxian + "' where position ='" + zhiwu + "' ";
            return dBConn.ExecuteNonQuery(sql);

        }
        public int InsertQuanxian(string quanxian, string zhiwu)
        {
            string sql = "insert into  adims_position_jurisdiction (position,jurisdiction) values ('" + zhiwu + "' ,'" + quanxian + "' )";
            return dBConn.ExecuteNonQuery(sql);

        }
        public int Add_position(position_jurisdiction pj)
        {
            pj.jurisdiction = "0000000000000000000000000000";
            string coAdims_str = "insert into adims_position_jurisdiction(position,jurisdiction) values('" +
                pj.position + "','" + pj.jurisdiction + "')";
            return dBConn.ExecuteNonQuery(coAdims_str);
        }
        public DataSet Select_PJ()
        {
            string coAdims_str = "select ID 岗位ID,position 岗位名称 from adims_position_jurisdiction";
            return dBConn.GetDataSet(coAdims_str);
        }

        public DataSet Select_position_jurisdiction()
        {
            string coAdims_str = "select position ,jurisdiction from adims_position_jurisdiction";
            return dBConn.GetDataSet(coAdims_str);
        }

        public int Delete_PJ(position_jurisdiction pj)
        {
            string coAdims_str = "delete from adims_position_jurisdiction where id='" + pj.ID + "'";
            return dBConn.ExecuteNonQuery(coAdims_str);
        }
        public int Update_position(position_jurisdiction pj)
        {
            string coAdims_str = "update adims_position_jurisdiction set position='" + pj.position + "' where id='" +
                pj.ID + "'";
            return dBConn.ExecuteNonQuery(coAdims_str);
        }
        public int Upadte_jurisdiction(int module_index, int[] be_able)
        {
            string sql = "select * from adims_position_jurisdiction";
            return dBConn.Upadte_jurisdiction_DAL(sql, module_index, be_able);

        }

        public int Add_user(user_info user)
        {
            string coAdims_str = "insert into adims_user(uid,password,user_name,position,type) values('" +
           user.uid + "','" + user.password + "','" + user.user_name + "','" + user.position + "','" + user.type + "')";
            return dBConn.ExecuteNonQuery(coAdims_str);
        }

        public int Delete_user(user_info user)
        {
            string coAdims_str = "delete from adims_user where uid='" + user.uid + "'";
            return dBConn.ExecuteNonQuery(coAdims_str);
        }

        public int Update_user(user_info user)
        {
            string coAdims_str = "update adims_user set password='" + user.password + "',user_name='" + user.user_name +
                "',position='" + user.position + "',type='" + user.type + "' where uid='" + user.uid + "'";
            return dBConn.ExecuteNonQuery(coAdims_str);
        }
        public DataTable selectCount_user(string username)
        {
            string select = "select count(user_name) from  adims_user where user_name='" + username + "' ";
            return dBConn.GetDataTable(select);
        }
        public DataTable selectUserName(int type)
        {
            string select = "select user_name as 姓名 from adims_user where type='" + type + "' ";
            return dBConn.GetDataTable(select);
        }
        public int Update_user_position(string oldPosition, string newPosiotion)
        {
            string coAdims_str = "update adims_user set position='" + newPosiotion + "'" + " where position='" + oldPosition + "'";
            return dBConn.ExecuteNonQuery(coAdims_str);
        }
        public int Delete_user_position(string oldPosition)
        {
            string coAdims_str = "update adims_user set position='(已删除)'" + " where position='" + oldPosition + "'";
            return dBConn.ExecuteNonQuery(coAdims_str);
        }
        public DataSet select_user()
        {
            string coAdims_str = "select uid 登录账号,password 登录密码,user_name 用户名,position 职位 from adims_user";
            return dBConn.GetDataSet(coAdims_str);

        }
        public user_info Get_user_info(user_info user)
        {
            string coAdims_str = "select uid,password,user_name,position from adims_user where uid='" + user.uid +
                "' And password='" + user.password + "'";
            DataSet user_set = dBConn.GetDataSet(coAdims_str);
            if (user_set == null) return null;
            else
            {
                user.user_name = user_set.Tables[0].Rows[0][2].ToString();
                user.position = user_set.Tables[0].Rows[0][3].ToString();
                return user;
            }
        }

        public string Get_user_jurisdiction(user_info user)
        {
            string coAdims_str = "select jurisdiction from adims_position_jurisdiction where position='" + user.position + "'";
            DataSet user_jurisdiction = dBConn.GetDataSet(coAdims_str);
            if (user_jurisdiction == null) return null;
            else
            {
                return user_jurisdiction.Tables[0].Rows[0][0].ToString();
            }
        }
        #endregion
        #region    //药品管理模块中使用的方法
        #region     //麻醉科药品管理


        public DataSet ypxhtj_table()
        {
            return dBConn.GetDataSet("select ypbh 药品编号,ypname 药品名称,ypGg 药品规格,pyzt 拼音字头,dl 毒理,zt 状态,xisl 消耗数量 from ypxhtj");
        }
        public DataSet select_medicine_info()
        {
            string coAdims_str = "select medicine_number 药品编号," +
               "medicine_name 药品名称,phonetic_prefix 拼音字头,toxicology 毒理,state 状态," +
               "dosagy_form 剂型,specification 规格,produce_time 生产日期,deadline 有效日期," +
               "batch_number 批次,origin_place 产地 from Adims_medicine_info";
            return dBConn.GetDataSet(coAdims_str);
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
        #region   //手术间药品管理
        public DataSet select_room_stock(int roomid)
        {
            string coAdims_str = "select Adims_room_stock.medicine_number 药品编号, medicine_name 药品名称,count 数量," +
                "toxicology 毒理,state 状态,dosagy_form 剂型,specification 规格,batch_number 批次,deadline 有效日期 " +
                "from Adims_room_stock,Adims_medicine_info where Adims_medicine_info.medicine_number=Adims_room_stock.medicine_number" +
                " And room_id='" + roomid + "'";
            return dBConn.GetDataSet(coAdims_str);
        }
        public int Add_room_stock(room_input input)
        {
            string coAdims_str = "insert into adims_room_stock(room_id,medicine_number,count) values('" +
                input.room_id + "','" + input.medicine_number + "','" + input.input_count + "')";
            return dBConn.ExecuteNonQuery(coAdims_str);
        }
        public int Add_room_input(room_input input)
        {
            string coAdims_str = "insert into adims_room_input(surgery_id,room_id,medicine_number,input_count,input_time,confirm_person) values('" +
        input.surgery_id + "','" + input.room_id + "','" + input.medicine_number + "','" + input.input_count + "','" + input.input_time + "','" + input.confirm_person + "')";
            return dBConn.ExecuteNonQuery(coAdims_str);
        }
        public int UpdateAdd_room_stock(room_input input)
        {
            string coAdims_str = "select count(*) from adims_room_stock where medicine_number='" + input.medicine_number + "'" +
                " And room_id='" + input.room_id + "'";
            int count = (int)dBConn.ExecuteScalar(coAdims_str);
            if (0 == count) { return Add_room_stock(input); }
            else
            {
                coAdims_str = "update adims_room_stock set count=count+'" + input.input_count + "'" + " where medicine_number='" +
               input.medicine_number + "'" + " And room_id='" + input.room_id + "'";
                return dBConn.ExecuteNonQuery(coAdims_str);
            }
        }
        public DataSet select_room_input()
        {
            string coAdims_str = "select Adims_room_input.surgery_id 手术室号,Adims_room_input.room_id 手术间号,Adims_room_input.medicine_number 药品编号," +
                "Adims_room_input.input_count 输入数量,Adims_room_input.input_time 输入时间,Adims_room_input.confirm_person 确认人," +
                "medicine_name 药品名称,phonetic_prefix 拼音字头,toxicology 毒理,state 状态," +
                "dosagy_form 剂型,specification 规格,produce_time 生产日期,deadline 有效日期," +
                "batch_number 批次,origin_place 产地 from adims_medicine_info,adims_room_input " +
                "where adims_medicine_info.medicine_number=adims_room_input.medicine_number order by Adims_room_input.input_time DESC";
            return dBConn.GetDataSet(coAdims_str);
        }
        public int TestSub_room_stock(room_output output)
        {
            string coAdims_str = "select count(*) from adims_room_stock where medicine_number='" + output.medicine_number + "'" +
                " And room_id='" + output.room_id + "'";
            int count = (int)dBConn.ExecuteScalar(coAdims_str);
            if (count == 1)
            {
                coAdims_str = "select count(*) from adims_room_stock where medicine_number='" + output.medicine_number + "'" +
                    " And room_id='" + output.room_id + "'" +
                   " And count>='" + output.output_count + "'";
                count = (int)dBConn.ExecuteScalar(coAdims_str);
                if (count == 1) return 2;
                else return 1;
            }

            else return 0;
        }
        public int Add_room_output(room_output output)
        {
            if (TestSub_room_stock(output) == 2)
            {
                string coAdims_str = "insert into adims_room_output(room_id,patid,medicine_number,output_count,output_time,confirm_person) values('" +
        output.room_id + "','" + output.patid + "','" + output.medicine_number + "','" + output.output_count + "','" + output.output_time + "','" + output.confirm_person + "')";
                return dBConn.ExecuteNonQuery(coAdims_str);
            }
            else
                return -2;
        }
        public int UpdateSub_room_stock(room_output output)
        {
            if (TestSub_room_stock(output) == 2)
            {
                string coAdims_str = "update adims_room_stock set count=count-'" + output.output_count + "'" + " where medicine_number='" +
                output.medicine_number + "'" + " And room_id='" + output.room_id + "'";
                return dBConn.ExecuteNonQuery(coAdims_str);
            }
            return 0;
        }
        public DataSet select_room_output()
        {
            string coAdims_str = "select Adims_room_output.room_id 手术间号,Adims_room_output.patid 医师编号,Adims_room_output.medicine_number 药品编号," +
                "Adims_room_output.output_count 输出数量,Adims_room_output.output_time 输出时间,Adims_room_output.confirm_person 确认人," +
                "medicine_name 药品名称,phonetic_prefix 拼音字头,toxicology 毒理,state 状态," +
                "dosagy_form 剂型,specification 规格,produce_time 生产日期,deadline 有效日期," +
                "batch_number 批次,origin_place 产地 from adims_medicine_info,adims_room_output " +
                "where adims_medicine_info.medicine_number=adims_room_output.medicine_number order by Adims_room_output.output_time DESC";
            return dBConn.GetDataSet(coAdims_str);
        }
        public int Test_room_checkin(room_checkin checkin)
        {
            string coAdims_str = "select count(*) from adims_room_output where room_id='" + checkin.room_id +
                "' And patid='" + checkin.patid + "' And medicine_number='" + checkin.medicine_number + "'";
            int count = (int)dBConn.ExecuteScalar(coAdims_str);
            if (count > 0) return 1;
            else return 0;
            // 这只是对核销的有效性进行的简单测试，还有待完善   
        }
        public int Add_room_checkin(room_checkin checkin)
        {
            if (Test_room_checkin(checkin) == 1)
            {
                string coAdims_str = "insert into adims_room_checkin(room_id,patid,medicine_number,count,confirm_person,checkin_time) values('" +
                  checkin.room_id + "','" + checkin.patid + "','" + checkin.medicine_number + "','" + checkin.count + "','" + checkin.confirm_person +
                  "','" + checkin.checkin_time + "')";
                return dBConn.ExecuteNonQuery(coAdims_str);
            }
            else
                return 0;
        }
        public int Update_room_stock(room_checkin checkin)
        {
            if (Test_room_checkin(checkin) == 1)
            {
                string coAdims_str = "update adims_room_stock set count=count+'" + checkin.count + "' where medicine_number='" +
              checkin.medicine_number + "' And room_id='" + checkin.room_id + "'";
                return dBConn.ExecuteNonQuery(coAdims_str);
            }
            return 0;
        }
        public DataSet select_room_checkin()
        {
            string coAdims_str = "select Adims_room_checkin.room_id 手术室号,Adims_room_checkin.patid 医师编号,Adims_room_checkin.medicine_number 药品编号," +
               "Adims_room_checkin.count 核销数量,Adims_room_checkin.checkin_time 核销时间,Adims_room_checkin.confirm_person 确认人," +
               "medicine_name 药品名称,phonetic_prefix 拼音字头,toxicology 毒理,state 状态," +
               "dosagy_form 剂型,specification 规格,produce_time 生产日期,deadline 有效日期," +
               "batch_number 批次,origin_place 产地 from adims_medicine_info,adims_room_checkin " +
               "where adims_medicine_info.medicine_number=adims_room_checkin.medicine_number order by Adims_room_checkin.checkin_time DESC";
            return dBConn.GetDataSet(coAdims_str);
        }
        #endregion
        #region   //病人用药管理

        public DataTable SSpatInfo_table()
        {
            string select = "select A.patid,B.Patname,A.id,B.Patsex,B.Patage from adims_mzjld as A ,Adims_OTypesetting as B where A.patid=B.patID ";
            return dBConn.GetDataTable(select);
        }
        public DataTable ydyUseInfo(int  mzid)//诱导药
        {
            string select = "select mzjldid 麻醉记录单号,ydyname 药品名字,yl 用量,dw 单位 from Adims_ydyUse where mzjldid='" + mzid + "'";
            return dBConn.GetDataTable(select);
        }
        public DataTable tsyyUseInfo(int mzid)//特殊用药
        {
            return dBConn.GetDataTable("select mzjldid 麻醉记录单号,name 药品名字,yl 用量,dw 单位 from Adims_Tsyy where mzjldid='" + mzid + "'");
        }
        public DataTable sqyyUseInfo(int mzid)//术前用药
        {
            return dBConn.GetDataTable("select mzjldid 麻醉记录单号,ypName 药品名字,yl 用量,dw 单位 from Adims_sqyyUse where mzjldid='" + mzid + "'");
        }
        public DataTable jtjtsyUseInfo(int mzid)//晶体胶体输血
        {
            string select = "select mzjldid 麻醉记录单号,name 药品名字,jl as 用量,dw 单位 from Adims_jmyUSE where mzjldid='"+mzid+"'";
            return dBConn.GetDataTable(select);
        }
          public DataTable ytUseInfo(int mzid)//液体使用输血
        {
            string select = "select mzjldid 麻醉记录单号,ytname 药品名字,yl as 用量,dw 单位 from Adims_Ytuse where mzjldid='" + mzid + "'";
            return dBConn.GetDataTable(select);
        }
          public DataTable QtUseInfo(int mzid)//气体使用
          {
              string select = "select mzjldid 麻醉记录单号,qtname 药品名字,yl*DATEDIFF ( mi , sytime , jstime ) as 用量,dw as 单位 from Adims_Qtuse where mzjldid='" + mzid + "'";
              return dBConn.GetDataTable(select);
          }

        

        #endregion
        #endregion
        #region //麻醉记录单查询
        public DataTable query_mzjld(string condition)
        {
            string str = " select adims_mzjld.patid, adims_mzjld.id, patname , patsex , patage , ssss , ssys , mzys , mzfa , otime  ,patdpm , szzd , sqzd ,tw,mzxg from adims_mzjld,adims_otypesetting where adims_mzjld.patid=adims_otypesetting.patid ";
            str += condition;
            return dBConn.GetDataTable(str);
        }
        public DataTable query_mzzongjie(string condition)
        {
            string str = " select adims_mzzongjie.mzjldID, PATID, patname ,mzys , AddTime  ,patdpm from adims_mzzongjie,adims_otypesetting where adims_mzzongjie.mazjldid=adims_otypesetting.patid ";
            str += condition;
            return dBConn.GetDataTable(str);
        }

        public DataTable select_doctors()
        {
            string str = "select MedicalNo 编号,MedicalName 姓名 from adims_surgerystaff ";
            return dBConn.GetDataTable(str);
        }

        public DataTable select_keshi()
        {
            string str = "select Name  from keshi";
            return dBConn.GetDataTable(str);
        }
        public DataTable select_shoushuyisheng()
        {
            string str = "select name  from shoushuyisheng";
            return dBConn.GetDataTable(str);
        }
        #endregion
        #region <<合并方法>>
        /// <summary>
        /// 从数据库获取排班内容
        /// </summary>
        /// <param name="riqi"></param>
        /// <returns></returns>
        public static string getcontent(string riqi)
        {

            string content = adims_DAL.AdimsProvider.GetContent(riqi);
            return content;
        }

        /// <summary>
        /// 增加排班
        /// </summary>
        /// <param name="neirong"></param>
        /// <param name="riqi"></param>
        public static void AddData(string time, string Date, string mzyi, string fs)
        {
            adims_DAL.AdimsProvider.AddData(time, Date, mzyi, fs);
        }

        /// <summary>
        /// 更新排班内容
        /// </summary>
        /// <param name="neirong"></param>
        /// <param name="riqi"></param>
        public static void UpdateData(string time, string Date, string mzyi, string fs, string ID)
        {
            adims_DAL.AdimsProvider.UpdateData(time, Date, mzyi, fs, ID);
        }

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
        /// 想某个数据库里面取出数据
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        public static DataTable getData(string table)
        {
            DataTable dt = adims_DAL.AdimsProvider.GetData(table);
            return dt;
        }

        /// <summary>
        /// 向table表里插入name数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="table"></param>
        public static void AddData1(string name, string table)
        {
            adims_DAL.AdimsProvider.AddData1(name, table);
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

        /// <summary>
        /// 查看麻醉明细
        /// </summary>
        /// <param name="StartRiQi"></param>
        /// <param name="EndRiQi"></param>
        /// <returns></returns>
        public static DataTable getdata(string StartRiQi, string EndRiQi)
        {
            return adims_DAL.AdimsProvider.GetData1("select * from mazuimingxi where riqi >= '" + StartRiQi + "' and " + "riqi <= '" + EndRiQi + "'");
        }

        public static DataTable getdata1()
        {

            return adims_DAL.AdimsProvider.GetData1("select MedicalName from Adims_SurgeryStaff WHERE PostType = 0");
        }

        public static DataTable getdata2()
        {

            return adims_DAL.AdimsProvider.GetData1("select MedicalName from Adims_SurgeryStaff WHERE PostType = 1");
        }

        public static DataTable getdata3()
        {

            return adims_DAL.AdimsProvider.GetData1("select Name from qingjialeixing");
        }

        public static DataTable getdata4()
        {

            return adims_DAL.AdimsProvider.GetData1("select Name from shijian");
        }

        public static void UpdateData1(string ID, string Name, string table)
        {
            adims_DAL.AdimsProvider.UpdateData1(ID, Name, table);
        }

        public static void DeleteData(string ID, string table)
        {
            adims_DAL.AdimsProvider.DeleteData(ID, table);
        }

        /// <summary>
        /// 得到麻醉医生所对应的手术信息
        /// </summary>
        /// <param name="YiSheng"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DataTable getPacu1(string YiSheng, string date)
        {
            DataTable dt;
            dt = adims_DAL.AdimsProvider.GetData1("Select * from Pacu_1 where MaZuiYiSheng='" + YiSheng + "'" + " and ShouShuRiQi='" + date + "'");
            return dt;
        }

        /// <summary>
        /// 提取麻醉医生所对应的手术信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static DataTable getPacu1_1(int ID)
        {
            DataTable dt;
            dt = adims_DAL.AdimsProvider.GetData1("Select ShouShuDanHao,ZhuYuanHao,XinMing,XinBei,ChuangHao,ShouShuMingCheng,ShuZhongZhengDuan from Pacu_1 where ID='" + ID + "'");
            return dt;
        }

        /// <summary>
        /// 判断是否登陆成功
        /// </summary>
        /// <param name="gonghao">工号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static Dictionary<string, string> DengLu(string gonghao, string password)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();//工号和 角色名
            string sql = "select User_ID,User_pwd,User_Name,User_roles  from users where User_ID='" + gonghao + "' " + "and " + "User_pwd='" + password + "'";

            DataTable dt = adims_DAL.AdimsProvider.GetData1(sql);

            if (dt.Rows.Count > 0)
            {

                string User_roles = dt.Rows[0]["User_roles"].ToString();
                string User_ID = dt.Rows[0]["User_ID"].ToString();
                dic.Add(User_ID, User_roles);
            }
            return dic;



        }

        public static DataTable getQuanXian(string UserID)
        {
            string sql = "select Role_QuanXin  from Roles where Role_Name='" + UserID + "'";
            DataTable dt = adims_DAL.AdimsProvider.GetData1(sql);
            return dt;
        }

        public static DataTable getUsers()
        {

            DataTable dt = adims_DAL.AdimsProvider.GetData1("select User_ID,User_pwd,User_Name  from users");
            return dt;
        }

        #endregion

        #region <<方法>>

        /// <summary>
        /// 术前访视病人信息查询
        /// </summary>
        /// <returns></returns>
        public DataSet Sqfs(string dtime)
        {
            return dBConn.GetDataSet("select patid 病人ID,patname 姓名,patage 年龄,patsex 性别,patdpm 病区,patbedno 床号,"
                + "oname 拟实施手术,pattmd 主要诊断,os 手术医师,oname 手术名字,amethod 麻醉方法,ap1 主麻医师,ap2 副麻医师1,"
                + "ap3 副麻医师2,on1 洗手护士1,on2 洗手护士2,sn1 巡回护士1,sn2 巡回护士2,remarks 备注 from Adims_OTypesetting "
                + "WITH (NOLOCK) where CONVERT(varchar, Odate , 23 )  ='" + dtime + "'");
        }
        public DataTable Sqfs1(string dtime)
        {
            string sqls="select patzhuyuanid 住院号,patbedno 床号,patname 姓名,patdpm 病区,patage 年龄,patsex 性别,"
                + "oname 拟实施手术,pattmd 主要诊断,os 手术医师,oname 手术名字,amethod 麻醉方法,ap1 主麻医师,on1 器械护士,sn1 巡回护士,"
                + "patid 病人编号 from Adims_OTypesetting WITH (NOLOCK) where CONVERT(varchar, Odate , 23 )  ='" + dtime + "'";
               
            return dBConn.GetDataTable(sqls);
        }

        /// <summary>
        /// 添加术前访视
        /// </summary>
        /// <param name="sqfs"></param>
        /// <returns></returns>
        public int InsertSqfs(adims_MODEL.sqfs sqfs)
        {
            string sql = "INSERT INTO Adims_Sqfs(patid,weight,xy,xl,mb,hx,tw,ys,zqojz,xtbsjzlyw,"
                + "ssmzs,gms,tjb,kqzk,kqyc,xftz,asa,e,jljgj,gjyc,jljt,qt,wzjm,jzzk,xfgnfj,fgn,xdt,xphxt,ggn,"
                + "sgn,xhdb,hxb,xxbbr,xxxb,fg,aptt,nxmysj,xj,xn,xuelv,xt,xqfx,mzfa,fhcs,fsys,date) VALUES"
                + "('" + sqfs.Patid + "','" + sqfs.Tz + "','" + sqfs.Xy + "','" + sqfs.Xl + "','" + sqfs.Mb
                + "','" + sqfs.Hx + "','" + sqfs.Tw + "','" + sqfs.Ys + "','" + sqfs.Zqojz + "','" + sqfs.Xtbs
                + "','" + sqfs.Ssmzs + "','" + sqfs.Gms + "','" + sqfs.Tjb + "','" + sqfs.Kqzk + "','" + sqfs.Kqyc
                + "','" + sqfs.Xftz + "','" + sqfs.Asa + "','" + sqfs.E + "','" + sqfs.Jljgj + "','" + sqfs.Gjyc
                + "','" + sqfs.Jljt + "','" + sqfs.Qt + "','" + sqfs.Wzjm + "','" + sqfs.Jzzk + "','" + sqfs.Xfgnfj
                + "','" + sqfs.Fgn + "','" + sqfs.Xdt + "','" + sqfs.Xphxt + "','" + sqfs.Ggn + "','" + sqfs.Sgn
                + "','" + sqfs.Xhdb + "','" + sqfs.Hxb + "','" + sqfs.Xxbbr + "','" + sqfs.Xxxb + "','" + sqfs.Fg
                + "','" + sqfs.Aptt + "','" + sqfs.Nxmysj + "','" + sqfs.Xj + "','" + sqfs.Xn + "','" + sqfs.Xuelv
                + "','" + sqfs.Xt + "','" + sqfs.Xqfx + "','" + sqfs.Mzfa + "','" + sqfs.Fhcs + "','" + sqfs.Fsys
                + "','" + sqfs.Date + "')";
            return dBConn.ExecuteNonQuery(sql);
        }


        /// <summary>
        /// 查询手术信息
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public DataSet Osel(string str)
        {
            return dBConn.GetDataSet("select onname 手术名称,olevel 手术等级,ospell 检索拼音 from OperationName where ospell like'%" + str + "%' ");
        }

        /// <summary>
        /// 气体使用情况
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <returns></returns>
        public DataSet qtsy(int mzjldid)
        {
            return dBConn.GetDataSet("select qtname,yl,dw,sytime,jstime,flags from qtuse where mzjldid='" + mzjldid + "'");
        }

        /// <summary>
        /// 已排版对应麻醉医生的患者
        /// </summary>
        /// <param name="ap"></param>
        /// <returns></returns>
        public DataTable xssbr(string ap,string dt)
        {
            string sql = "SELECT patzhuyuanid 麻醉编号,patid 病人编号,Patname 病人姓名,Oroom 手术间名称,patsex 性别, patage 年龄,pattmd 主要诊断,"
                + "oname 手术名称,patdpm 科室 from Adims_OTypesetting WITH (NOLOCK) where "
            +"CONVERT(varchar, Odate , 23 ) = '" + dt + "' and (ap1='" + ap + "'or ap2='" + ap + "' or ap3='" + ap + "') ";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 已排版对应麻醉医生的患者
        /// </summary>
        /// <param name="ap"></param>
        /// <returns></returns>
        public DataTable xssbr1( string dt)
        {
            string sql = "SELECT Oroom 手术间名称,patdpm 科室,patzhuyuanid 住院号,patbedno 床位号, Patname 病人姓名,patsex 性别, patage 年龄,pattmd 主要诊断,"
                + "oname 手术名称,patid 病人编号 from Adims_OTypesetting WITH (NOLOCK) where "
            + "CONVERT(varchar, Odate , 23 ) = '" + dt + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable xssbr2(string dt)
        {
            string sql = "SELECT Oroom 手术间名称,patdpm 科室,patzhuyuanid 住院号,patbedno 床位号,Patname 病人姓名,patsex 性别, patage 年龄,pattmd 主要诊断,oname 手术名称,A.patid 病人编号,B.id 麻醉编号 from Adims_OTypesetting AS A Inner join adims_mzjld as B  ON A.patid=B.patid  where CONVERT(varchar, Odate , 23 ) = '" + dt + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 继续患者手术病人信息查询
        /// </summary>
        /// <param name="ap"></param>
        /// <returns></returns>
        public DataTable SelectOldPatInfo(int mzid)
        {
            string sql = "SELECT * from Adims_Mzjld where  id='" + mzid + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable SelectOldPatInfo(string pid)
        {
            string sql = "SELECT * from Adims_Mzjld where  patid='" + pid + "'";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 获取病人基本信息
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="pat"></param>
        /// 
        public DataTable SelectPatInfo(string patid,DateTime dt)
        {
            string sql = "SELECT * from Adims_OTypesetting where  patid='" + patid + "'and odate='" + dt + "'";
            return dBConn.GetDataTable(sql);
        }

        public DataTable SelectPatInfo(string patid)
        {
            string sql = "SELECT * from Adims_OTypesetting where  patid='" + patid + "'";
            return dBConn.GetDataTable(sql);
        }
        public void GetPatInfo(string patID, adims_MODEL.paiban pat)
        {
            DataTable dt = new DataTable();
            dt = dBConn.GetDataTable("select patid,PatZhuYuanID,PatHeight,PatBloodType,PatWeight,patname,patage,patsex,patdpm,patbedno,oname,pattmd,os,oa1,oa2,oa3,oa4,"
                + "amethod,ap1,ap2,ap3,on1,on2,sn1,sn2,asa,asae,Odate,xueya,maibo,huxi,tiwen from Adims_OTypesetting where patid='" + patID + "'");
            if (dt.Rows.Count == 0) return;
            pat.Patid = Convert.ToString(dt.Rows[0]["patid"]);
            pat.Patname = Convert.ToString(dt.Rows[0]["patname"]);
            pat.Patage = Convert.ToInt32(dt.Rows[0]["patage"]);
            pat.Patsex = Convert.ToString(dt.Rows[0]["patsex"]);
            pat.Height = Convert.ToString(dt.Rows[0]["PatHeight"]);
            pat.Weight = Convert.ToString(dt.Rows[0]["PatWeight"]);
            //pat.Xueya = Convert.ToInt32(dt.Rows[0]["xueya"]);
            //pat.Maibo = Convert.ToInt32(dt.Rows[0]["maibo"]);
            //pat.Huxi = Convert.ToInt32(dt.Rows[0]["huxi"]);
            //pat.Tiwen = Convert.ToDouble(dt.Rows[0]["tiwen"]);
            pat.Bloodtype = Convert.ToString(dt.Rows[0]["PatBloodType"]);
            pat.ZhuyuanNo = Convert.ToString(dt.Rows[0]["PatZhuYuanID"]);
            pat.Department = Convert.ToString(dt.Rows[0]["patdpm"]);
            pat.Bednumber = Convert.ToString(dt.Rows[0]["patbedno"]);
            pat.Oname = Convert.ToString(dt.Rows[0]["oname"]);
            pat.TMD1 = Convert.ToString(dt.Rows[0]["pattmd"]);
            pat.Os = Convert.ToString(dt.Rows[0]["os"]);
            pat.On1 = Convert.ToString(dt.Rows[0]["On1"]);
            pat.On2 = Convert.ToString(dt.Rows[0]["On2"]);
            pat.Sn1 = Convert.ToString(dt.Rows[0]["Sn1"]);
            pat.Sn2 = Convert.ToString(dt.Rows[0]["Sn2"]);
            pat.Oa1 = Convert.ToString(dt.Rows[0]["oa1"]);
            pat.Oa2 = Convert.ToString(dt.Rows[0]["oa2"]);
            pat.Oa3 = Convert.ToString(dt.Rows[0]["oa3"]);
            pat.Oa4 = Convert.ToString(dt.Rows[0]["oa4"]);
            pat.Amethod = Convert.ToString(dt.Rows[0]["amethod"]);
            pat.Ap1 = Convert.ToString(dt.Rows[0]["ap1"]);
            pat.Ap2 = Convert.ToString(dt.Rows[0]["ap2"]);
            pat.Ap3 = Convert.ToString(dt.Rows[0]["ap3"]);
            pat.Asa = Convert.ToString(dt.Rows[0]["asa"]);
            pat.Asae = Convert.ToInt32(dt.Rows[0]["asae"]);
            pat.Odate = Convert.ToDateTime(dt.Rows[0]["Odate"]);
        }

        /// <summary>
        /// 创建麻醉记录单
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        ///  
        //public int addmzjld(int mzjldid, string patid, DateTime otime)
        // {
        //    return dBConn.ExecuteNonQuery("INSERT INTO Adims_Mzjld(id,patid,otime) values('" + mzjldid + "','" + patid + "','" + otime + "')");
        // }
        public int addmzjld(string patid, DateTime otime)
        {
            return dBConn.ExecuteNonQuery("INSERT INTO Adims_Mzjld(patid,otime) values('" + patid + "','" + otime + "')");
        }
         public DataTable selectmzjld(int mzjldid,string patid)
         {
            return dBConn.GetDataTable("select * from Adims_Mzjld where id='" + mzjldid + "'and patid='" + patid + "'");
         }
         public DataTable selectmzjld(int mzjldid)
         {
             return dBConn.GetDataTable("select * from Adims_Mzjld where id='" + mzjldid + "'");
         }
         public DataTable selectSinglemzjld(string patid,DateTime dt)
         {
             string sql = "select id from Adims_Mzjld where  patid='" + patid + "' and otime='" + dt + "'";
             
             return dBConn.GetDataTable(sql);
         }
        public DataTable selectSinglemzjld1(string patid)
         {
             string sql = "select id from Adims_Mzjld where  patid='" + patid + "' ";
             return dBConn.GetDataTable(sql);
         }
        public int cmzjld(int patID, bool flags)
        {
            int i;
            object obj = dBConn.ExecuteScalar("select id from Adims_Mzjld where patid='" + patID + "'");
            if (obj == null || flags)
            {
                dBConn.ExecuteNonQuery("INSERT INTO Adims_Mzjld(patid) values('" + patID + "')");
                i = (int)dBConn.ExecuteScalar("select max(id) from Adims_Mzjld where patid='" + patID + "'");
            }
            else
                i = (int)obj;
            return i;
        }
        /// <summary>
        /// 创建麻醉记录单-本地
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public int cmzjld2(int patID, bool flags)
        {
            int i=0;
            //object obj = sqlitehelper.ExecuteScalar("select id from Adims_Mzjld where patid='" + patID + "'");
            //if (obj == null || flags)
            //{
            //    sqlitehelper.ExecuteNonQuery("INSERT INTO Adims_Mzjld(patid) values('" + patID + "')");
            //    i = (int)sqlitehelper.ExecuteScalar("select max(id) from Adims_Mzjld where patid='" + patID + "'");
            //}
            //else
            //    i = (int)obj;
            return i;
        }

        /// <summary>
        /// 添加气体
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="qt"></param>
        /// <returns></returns>
        public int addqt(int mzjldid, adims_MODEL.mzqt qt)
        {
            return dBConn.ExecuteNonQuery("INSERT INTO Adims_Qtuse(mzjldid,qtname,yl,dw,sytime,flags) "
                + "values('" + mzjldid + "','" + qt.Qtname + "','" + qt.Yl + "','" + qt.Dw + "','" + qt.Sysj + "',1)");
        }       
        public int addqtPACU(int mzjldid, adims_MODEL.mzqt qt)
        {
            return dBConn.ExecuteNonQuery("INSERT INTO Adims_PACU_qtUse(mzjldid,qtname,yl,dw,sytime,flags) "
                + "values('" + mzjldid + "','" + qt.Qtname + "','" + qt.Yl + "','" + qt.Dw + "','" + qt.Sysj + "',1)");
        }
        public DataTable select_qtPACU(int mzjldid)
        {
            string SQL = "SELECT * FROM Adims_PACU_qtUse WHERE mzjldid = '" + mzjldid + "'order by qtname asc";
            return dBConn.GetDataTable(SQL);
        }
        /// <summary>
        /// 结束气体
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="qt"></param>
        /// <returns></returns>
        public int endqt(int mzjldid, DateTime dt,int id)
        {
            return dBConn.ExecuteNonQuery("update Adims_Qtuse set jstime='" + dt + "',flags='2' where id='" + id + "'");
        }

        public int endqtPACU(int mzjldid, DateTime dt, int id)
        {
            return dBConn.ExecuteNonQuery("update Adims_PACU_qtUse set jstime='" + dt + "',flags='2' where id='" + id + "'");
        }

        /// <summary>
        /// 删除气体
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="qt"></param>
        /// <returns></returns>
        public int delqt(int mzjldid, int id)
        {
            return dBConn.ExecuteNonQuery("DELETE FROM Adims_Qtuse WHERE mzjldid='" + mzjldid + "'and id = '" + id + "' ");
        }
        public int delqtPACU(int mzjldid, int id)
        {
            return dBConn.ExecuteNonQuery("DELETE FROM Adims_PACU_qtUse WHERE mzjldid='" + mzjldid + "'and id = '" + id + "' ");
        }
        /// <summary>
        /// 添加特殊用药
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="qt"></param>
        /// <returns></returns>
        public int addtsyy(int mzjldid, adims_MODEL.tsyy ts)
        {
            return dBConn.ExecuteNonQuery("INSERT INTO Adims_Tsyy(mzjldid,name,yl,dw,yyfs,time) "
                + "values('" + mzjldid + "','" + ts.Name + "','" + ts.Yl + "','" + ts.Dw + "','" + ts.Yyfs + "','"+ ts.D+"')");
        }

        /// <summary>
        /// 增加诱导药
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="yt"></param>
        /// <returns></returns>
        public int addyt1(int mzjldid, adims_MODEL.mzyt yt)
        {
            string sql="INSERT INTO Adims_ydyUSE(mzjldid,ydyname,yl,dw,yyfs,cxyy,sytime,flags) "
                + "values('" + mzjldid + "','" + yt.Ytname + "','" + yt.Yl + "','" + yt.Dw + "','" + yt.Yyfs + "','" + (yt.Cxyy ? 1 : 0) + "','" + yt.Sysj + "','1')";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int addyt2(int mzjldid, adims_MODEL.mzyt yt)
        {
            string sql = "INSERT INTO Adims_ydyUSE(mzjldid,ydyname,yl,dw,yyfs,cxyy,sytime,jstime,flags) "
                + "values('" + mzjldid + "','" + yt.Ytname + "','" + yt.Yl + "','" + yt.Dw + "','" + yt.Yyfs + "','" + (yt.Cxyy ? 1 : 0) + "','" + yt.Sysj + "','" + yt.Sysj + "','1')";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int xgydYL(string yl,int id)
        {
            string sql = "update  Adims_ydyUSE set yl='" + yl + "'where id='"+id+"'";
            return dBConn.ExecuteNonQuery(sql);
        }

        public DataTable select_qt(int mzjldid)
        {
            string SQL = "SELECT * FROM Adims_qtUSE WHERE mzjldid = '" + mzjldid + "'order by qtname asc";
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
         /// <summary>
        /// 结束诱导药
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="yt"></param>
        /// <returns></returns>
        public int endyt(int mzjldid, int id)
        {
            return dBConn.ExecuteNonQuery("update Adims_ydyUSE set jstime='" + DateTime.Now + "',flags='2' where mzjldid='" + mzjldid + "'and id='" + id+ "'");
        }

        /// <summary>
        /// 删除诱导药
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="yt"></param>
        /// <returns></returns>
        public int delyt(int mzjldid, int id)
        {
            return dBConn.ExecuteNonQuery("DELETE FROM Adims_ydyUSE WHERE mzjldid='" + mzjldid + "'and id = '" + id + "' ");
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
            string SQL = "INSERT INTO Adims_shuxueUse_PACU(mzjldid,shuxuename,jl,dw,zrfs,kssj,flags) values('" + mzjldid + "','" + jt.Name + "','" + jt.Jl + "','" + jt.Dw + "','" + jt.Zrfs + "','" + jt.Kssj + "','1')";
            return dBConn.ExecuteNonQuery(SQL);
        }
        public int addshuye(int mzjldid, adims_MODEL.shuye jt)//添加输液
        {
            string SQL="INSERT INTO Adims_shuyeUse(mzjldid,shuyename,jl,dw,zrfs,kssj,flags) values('" + mzjldid + "','" + jt.Name + "','" + jt.Jl + "','" + jt.Dw + "','" + jt.Zrfs + "','" + jt.Kssj + "','1')";
            return dBConn.ExecuteNonQuery(SQL);
        }
        public DataTable selectSX(int mzjldid)//查找输血
        {
            string SQL = "select * from Adims_shuxueUse where mzjldid='" + mzjldid + "'";
            return dBConn.GetDataTable(SQL);
        }
         public DataTable selectSX1(int mzjldid)//查找输血
        {
            string SQL = "select id,mzjldid,shuxuename,jl,dw,zrfs,kssj from Adims_shuxueUse where mzjldid='" + mzjldid + "'";
            return dBConn.GetDataTable(SQL);
        }
         public DataTable selectSY1(int mzjldid)//查找输血
         {
             string SQL = "select id,mzjldid,shuyename,jl,dw,zrfs,kssj from Adims_shuyeUse where mzjldid='" + mzjldid + "'";
             return dBConn.GetDataTable(SQL);
         }
         public DataTable selectSX_pacu(int mzjldid)//查找输血
         {
             string SQL = "select id,mzjldid,shuxuename,jl,dw,zrfs,kssj from Adims_shuxueUse_PACU where mzjldid='" + mzjldid + "'";
             return dBConn.GetDataTable(SQL);
         }
         public DataTable selectSY_pacu(int mzjldid)//查找输血
         {
             string SQL = "select id,mzjldid,shuyename,jl,dw,zrfs,kssj from Adims_shuyeUse_PACU where mzjldid='" + mzjldid + "'";
             return dBConn.GetDataTable(SQL);
         }

        
        public DataTable selectSY(int mzjldid)//查找输液
        {
            string SQL = "select * from Adims_shuyeUse where mzjldid='" + mzjldid + "'";
            return dBConn.GetDataTable(SQL);
        }
        public int addshuyePACU(int mzjldid, adims_MODEL.shuye jt)//添加输液
        {
            string SQL = "INSERT INTO Adims_shuyeUse_PACU(mzjldid,shuyename,jl,dw,zrfs,kssj,flags) values('" + mzjldid + "','" + jt.Name + "','" + jt.Jl + "','" + jt.Dw + "','" + jt.Zrfs + "','" + jt.Kssj + "','1')";
            return dBConn.ExecuteNonQuery(SQL);
        }
        //public int addsxsy_PACU(int mzjldid, adims_MODEL.shuxue jt)//添加晶体胶体输血
        //{
        //    return dBConn.ExecuteNonQuery("INSERT INTO Adims_PACU_sxsy(mzjldid,name,jl,dw,zrfs,kssj,flags) values('" + mzjldid + "','" + jt.Name + "','" + jt.Jl + "','" + jt.Dw + "','" + jt.Zrfs + "','" + jt.Kssj + "','1')");
        //}

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
        public int endshuye(int mzjldid, int id)//结束输液
        {
            return dBConn.ExecuteNonQuery("update Adims_shuyeUSE set jssj='" + DateTime.Now + "',flags='2' where id='" + id + "'");
        }
        public int endshuyePACU(int mzjldid, adims_MODEL.shuye jt)//结束输液
        {
            return dBConn.ExecuteNonQuery("update Adims_shuyeUSE_PACU set jssj='" + jt.Jssj + "',flags='2' where mzjldid='" + mzjldid + "'and shuyename='" + jt.Name + "'");
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

        public int xgqtKssj(int mzjldid, adims_MODEL.mzqt qt)//修改气体
        {
            return dBConn.ExecuteNonQuery("update Adims_Qtuse set sytime='" + qt.Sysj + "' where mzjldid='" + mzjldid + "'and id='" + qt.Id + "'");
        }
        
        public int xgqtJssj(int mzjldid, adims_MODEL.mzqt qt)//修改气体
        {
            return dBConn.ExecuteNonQuery("update Adims_Qtuse set jstime='" + qt.Jssj + "' where mzjldid='" + mzjldid + "'and id='" + qt.Id + "'");
        }
        public int xgqtPACU(int mzjldid, adims_MODEL.mzqt qt)//修改气体
        {
            return dBConn.ExecuteNonQuery("update Adims_PACU_qtUse set sytime='" + qt.Sysj + "' where mzjldid='" + mzjldid + "'and id='" + qt.Id+ "'");
        }
        public int xgqtPACU1(int mzjldid, adims_MODEL.mzqt qt)//修改气体
        {
            return dBConn.ExecuteNonQuery("update Adims_PACU_qtUse set jstime='" + qt.Sysj + "' where mzjldid='" + mzjldid + "'and id='" + qt.Id + "'");
        }
        
        public int xgshuyeKSSJ(int mzjldid, adims_MODEL.shuye qt)//修改输液时间
        {
            return dBConn.ExecuteNonQuery("update Adims_shuyeUSE set KSSJ='" + qt.Kssj + "',jssj='" + qt.Kssj + "' where mzjldid='" + mzjldid + "'and id='" + qt.Id + "'");
        }
        public int xgshuxueKSSJ(int mzjldid, adims_MODEL.shuxue qt)//修改输血时间
        {
            return dBConn.ExecuteNonQuery("update Adims_shuxueUSE set KSSJ='" + qt.Kssj + "',jssj='" + qt.Kssj + "' where mzjldid='" + mzjldid + "'and id='" + qt.Id + "'");
        }
        public int xgszsjTime(int mzjldid, adims_MODEL.szsj szsj1)//修改输血时间
        {
            return dBConn.ExecuteNonQuery("update Adims_szsj set time='" + szsj1.D + "' where mzjldid='" + mzjldid + "'and id='" + szsj1.Id + "'");
        }
        public int xgszsjTimePacu(int mzjldid, adims_MODEL.szsj szsj1,DateTime DT)//修改输血时间
        {
           string update=" update Adims_pacu_shijian set time='" + DT + "' where id='" + szsj1.Id + "'";

           return dBConn.ExecuteNonQuery(update);
        }
        public int xgtsyyTime(int mzjldid, adims_MODEL.tsyy tsyy1)//修改输血时间
        {
            return dBConn.ExecuteNonQuery("update Adims_tsyy set time='" + tsyy1.D + "' where mzjldid='" + mzjldid + "'and id='" + tsyy1.Id + "'");
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

        public int xgytKssj(int mzjldid, adims_MODEL.mzyt yt)//修改液体
        {
            string sql = "update Adims_ydyUSE set sytime='" + yt.Sysj + "' where mzjldid='" + mzjldid + "'and id='" + yt.Id + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int xgytKssjJssj(int mzjldid, adims_MODEL.mzyt yt)//修改液体
        {
            return dBConn.ExecuteNonQuery("update Adims_ydyUSE set sytime='" + yt.Sysj + "',jstime='" + yt.Sysj + "' where id='" + yt.Id + "'");
        }
        public int xgytJssj(int mzjldid, adims_MODEL.mzyt yt)//修改液体
        {
            string sql = "update Adims_ydyUSE set jstime='" + yt.Jssj + "' where mzjldid='" + mzjldid + "'and id='" + yt.Id + "'";
            return dBConn.ExecuteNonQuery(sql);
        }

        public int xgjt(int mzjldid, adims_MODEL.jtytsx jt)//修改晶体等
        {
            return dBConn.ExecuteNonQuery("update Adims_jmyUSE set kssj='" + jt.Kssj + "' where mzjldid='" + mzjldid + "'and name='" + jt.Name + "'");
        }
        public int xg_PACU_sx(int mzjldid, adims_MODEL.shuxue jt)//修改输血
        {
            return dBConn.ExecuteNonQuery("update Adims_shuxueUSE_PACU set kssj='" + jt.Kssj + "' where mzjldid='" + mzjldid + "'and id='" + jt.Id + "'");
        }
        public int xg_PACU_sy(int mzjldid, adims_MODEL.shuye jt)//修改输液
        {
            return dBConn.ExecuteNonQuery("update Adims_shuyeUSE_PACU set kssj='" + jt.Kssj + "' where mzjldid='" + mzjldid + "'and id='" + jt.Id + "'");
        }
        public DataTable GetPoint(int mzjldid)//查询监测点
        {
            string insert = "select mzjldid,RecordTime,NIBPS,NIBPD,RRC,Pulse,SpO2,ETCO2 from Adims_mzjld_Point where mzjldid='" + mzjldid + "' ORDER BY RecordTime ASC";
            return dBConn.GetDataTable(insert);
        }
        public DataTable GetPointPacu(int mzjldid)//查询监测点
        {
            string insert = "select mzjldid,RecordTime,NIBPS,NIBPD,RRC,Pulse,SpO2,ETCO2 from Adims_pacu_Point where mzjldid='" + mzjldid + "' ORDER BY RecordTime ASC";
            return dBConn.GetDataTable(insert);
        }
        public DataTable GetMaxPoint(int mzjldid)//查询监测点
        {
            string insert = "select max(RecordTime) from Adims_mzjld_Point where mzjldid='" + mzjldid + "'";
            return dBConn.GetDataTable(insert);
        }
        public DataTable GetMaxPointPacu(int mzjldid)//查询监测点
        {
            string insert = "select max(RecordTime) from Adims_Pacu_Point where mzjldid='" + mzjldid + "'";
            return dBConn.GetDataTable(insert);
        }
        public DataTable GetPointSingle(int mzjldid,DateTime dt)//查询监测点
        {
            string insert = "select mzjldid,RecordTime,NIBPS,NIBPD,RRC,Pulse,SpO2,ETCO2 from Adims_mzjld_Point where mzjldid='" + mzjldid + "' and RecordTime='"+dt+"'";
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
        public int AddPoint(int mzjldid, DateTime dt, string nibps, string nibpd, string rrc, string pulse, string spo2,string etco2)//增加监测点
        {
            string insert = "insert into Adims_mzjld_Point(mzjldid,RecordTime,NIBPS,NIBPD,RRC,Pulse,SpO2,ETCO2) values ('" + mzjldid + "','" + dt + "','" + nibps + "',"
            + "'" + nibpd + "','" + rrc + "','" + pulse + "','" + spo2 + "','" + etco2 + "')";
            return dBConn.ExecuteNonQuery(insert);
        }
        public int AddPointPacu(int mzjldid, DateTime dt, string nibps, string nibpd, string rrc, string pulse, string spo2, string etco2)//增加监测点
        {
            string insert = "insert into Adims_Pacu_Point(mzjldid,RecordTime,NIBPS,NIBPD,RRC,Pulse,SpO2,ETCO2) values ('" + mzjldid + "','" + dt + "','" + nibps + "',"
            + "'" + nibpd + "','" + rrc + "','" + pulse + "','" + spo2 + "','" + etco2 + "')";
            return dBConn.ExecuteNonQuery(insert);
        }
        public int UpdatePoint(int mzjldid, DateTime dt, string nibps, string nibpd, string rrc, string pulse, string spo2, string etco2)//修改监测点
        {
            string insert = "UPDATE  Adims_mzjld_Point set NIBPS = '" + nibps + "',NIBPD= '" + nibpd + "',RRC= '" + rrc + "',Pulse= '" + pulse + "',"
            + "SpO2= '" + spo2 + "',ETCO2= '" + etco2 + "' where mzjldid='" + mzjldid + "' and RecordTime='" + dt + "'";
            return dBConn.ExecuteNonQuery(insert);
        }
        public int UpdatePointPACU(int mzjldid, DateTime dt, string nibps, string nibpd, string rrc, string pulse, string spo2, string etco2)//修改监测点
        {
            string insert = "UPDATE  Adims_PACU_Point set NIBPS = '" + nibps + "',NIBPD= '" + nibpd + "',RRC= '" + rrc + "',Pulse= '" + pulse + "',"
            + "SpO2= '" + spo2 + "',ETCO2= '" + etco2 + "' where mzjldid='" + mzjldid + "' and RecordTime='" + dt + "'";
            return dBConn.ExecuteNonQuery(insert);
        }
        public int DeletePoint(int mzjldid, DateTime dt)//删除监测点
        {
            string insert = "delete from  Adims_mzjld_Point where mzjldid='" + mzjldid + "' and RecordTime='" + dt + "'";
            return dBConn.ExecuteNonQuery(insert);
        }
        public int DeletePointPACU(int mzjldid, DateTime dt)//删除监测点
        {
            string insert = "delete from  Adims_PACU_Point where mzjldid='" + mzjldid + "' and RecordTime='" + dt + "'";
            return dBConn.ExecuteNonQuery(insert);
        }

        public int xgMZJLDpoint(int mzjldid, adims_MODEL.point p)//修改点
        {
            string SQL="";
            if (p.Lx==1)
                SQL = "update Adims_mzjld_Point set NIBPS='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            else if (p.Lx==2)
                SQL = "update Adims_mzjld_Point set NIBPD='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            else if (p.Lx == 3)
                SQL = "update Adims_mzjld_Point set pulse='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            else if (p.Lx == 4)
                SQL = "update Adims_mzjld_Point set RRC='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            else if (p.Lx == 5)
                SQL = "update Adims_mzjld_Point set TEMP='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            //else if (p.Lx == 6)
            //    SQL = "update Adims_mzjld_Point set SpO2='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            //else if (p.Lx == 7)
            //    SQL = "update Adims_mzjld_Point set ETCO2='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            //else if (p.Lx == 8)
            //    SQL = "update Adims_mzjld_Point set CVP='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            //else if (p.Lx == 9)
            //    SQL = "update Adims_mzjld_Point set TOF='" + p.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + p.D + "'";
            
            return dBConn.ExecuteNonQuery(SQL);
        }
        public int xgpoint(int mzjldid, adims_MODEL.point p)//修改点
        {
            return dBConn.ExecuteNonQuery("update Adims_Point set value='" + p.V + "' where mzjldid='" + mzjldid + "'and lx='" + p.Lx + "' and time='" + p.D + "'");
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
        public int addSljlLog(int mzjldid,int lx, string xgyy,int xgqvalue,int xghvalue,DateTime xgdsj,DateTime nowtime )//添加生理记录修改日志
        {
            string insertSTR="INSERT INTO Adime_SljlxgLog(mzjldid,xgdlx,xgyy,xgqValue,xghValue,xgdTime,UpdateTime) values ('"+mzjldid+"','" + lx + "','" + xgyy + "','" + xgqvalue + "','" + xghvalue + "','" + xgdsj + "','" + nowtime + "')";
            return dBConn.ExecuteNonQuery(insertSTR);
        }

        //public int addjhxm(int mzjldid, adims_MODEL.jhxm jh)//添加监护数据
        //{
        //    return dBConn.ExecuteNonQuery("INSERT INTO Adims_jhxm(mzjldid,lx,value,time) values('" + mzjldid + "','" + jh.Sy + "','" + jh.V + "','" + jh.D + "')");
        //}
        public int addjhxm_PACU(int mzjldid, adims_MODEL.jhxm jh)//添加监护数据
        {
            return dBConn.ExecuteNonQuery("INSERT INTO Adims_jhxm_PACU(mzjldid,lx,value,time) values('" + mzjldid + "','" + jh.Sy + "','" + jh.V + "','" + jh.D + "')");
        }

        public int xgjhsj(int mzjldid, adims_MODEL.jhxm jh,int type)//修改监护数据
        {

            string sqlU="";
            if (type==0)
            {
                if (jh.Sy == "SpO2")
                sqlU = "update Adims_mzjld_Point set spo2='" + jh.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + jh.D + "'";
                else if (jh.Sy == "ETCO2")
                sqlU = "update Adims_mzjld_Point set spo2='" + jh.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + jh.D + "'";
             
            }
            if (type == 1)
            {
                if (jh.Sy == "SpO2")
                    sqlU = "update Adims_pacu_Point set spo2='" + jh.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + jh.D + "'";
                else if (jh.Sy == "ETCO2")
                    sqlU = "update Adims_pacu_Point set spo2='" + jh.V + "' where mzjldid='" + mzjldid + "' and RecordTime='" + jh.D + "'";
            }
            return dBConn.ExecuteNonQuery(sqlU);
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
        public int addclcxqt_PACU(int mzjldid, adims_MODEL.clcxqt cl)//添加出血量和尿量
        {
            return dBConn.ExecuteNonQuery("INSERT INTO Adims_PACU_cl(mzjldid,lx,value,time) values('" + mzjldid + "','" + cl.Lx + "','" + cl.V + "','" + cl.D + "')");
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

        /// <summary>
        /// 术后小结麻醉信息
        /// </summary>
        /// <param name="m"></param>
        /// <param name="patid"></param>
        public void selmzjld(adims_MODEL.shxjmzxx m, int patid)
        {
            DataTable dt = dBConn.GetDataTable("select top 1 id,ssmc,szzd,mzfa,yssss,jhxm,tw,mzxg from Adims_Mzjld where patid='" + patid + "' order by id desc");
            if (dt.Rows.Count > 0)
            {
                m.Mzjldid = Convert.ToInt32(dt.Rows[0]["id"]);
                m.Ssmc = Convert.ToString(dt.Rows[0]["ssmc"]);
                m.Szzd = Convert.ToString(dt.Rows[0]["szzd"]);
                m.Mzfa = Convert.ToString(dt.Rows[0]["mzfa"]);
                m.Yssss = Convert.ToString(dt.Rows[0]["yssss"]);
                m.Jhxm = Convert.ToString(dt.Rows[0]["jhxm"]);
                m.Tw = Convert.ToString(dt.Rows[0]["tw"]);
                m.Mzxg = Convert.ToString(dt.Rows[0]["mzxg"]);
            }
        }

        public void seloroomstate(List<adims_MODEL.oroomstate> oroom) //手术室个数
        {
            DataSet ds = new DataSet();
            ds = dBConn.GetDataSet("select oname,state,mzjldid,patid from ssjstate");
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                adims_MODEL.oroomstate o = new adims_MODEL.oroomstate();

                o.Mzjldid = r["mzjldid"].ToString();
                o.Ostate = Convert.ToInt32(r["state"].ToString());
                o.Oname = r["oname"].ToString();
                o.Patid = r["patid"].ToString();
                //DataTable DT1 = new DataTable();
                //string sql="select Adims_mzjld.PATID,Patname,Oname,AP1,AP2,AP3,MZkssj from Adims_OTypesetting INNER JOIN Adims_mzjld ON Adims_OTypesetting.PATID=Adims_mzjld.PATID WHERE Adims_mzjld.id='" + o.Mzjldid + "'";
                //DT1 = dBConn.GetDataTable(sql);
                //o.Mzys = DT1.Rows[0]["AP1"].ToString() + "," + DT1.Rows[0]["AP2"].ToString() + "," + DT1.Rows[0]["AP3"].ToString();
                //o.Kssj = Convert.ToDateTime(DT1.Rows[0]["MZkssj"]);
                //o.Oname = DT1.Rows[0]["Oname"].ToString();
                //o.Patid = DT1.Rows[0]["PATID"].ToString();
                oroom.Add(o);
            }

        }
        public DataTable slectOroomINFO(string oroom) //手术室信息
        {
            string sql = "select state,mzjldid,patid from ssjstate where  oname='" + oroom + "'";
            return dBConn.GetDataTable(sql);
        }
        //public void oroombaseinfo(adims_MODEL.ssqk ssqk) //手术室情况
        //{
        //    DataSet ds = new DataSet();
        //    ds = dBConn.GetDataSet("select Adims_OTypesetting.patname,Adims_Mzjld.yssss,Adims_mzks.time,Adims_Mzjld.mzys from Adims_OTypesetting,Adims_Mzjld,Adims_mzks where Adims_Mzjld.id='" + ssqk.Mzjldid + "'and Adims_Mzjld.patid = Adims_OTypesetting.id and Adims_mzks.mzjldid=Adims_Mzjld.id and Adims_mzks.sj=1");
        //    if (ds.Tables[0].Rows.Count == 0) return;
        //    ssqk.Hzname = ds.Tables[0].Rows[0][0].ToString();
        //    ssqk.Oname = ds.Tables[0].Rows[0][1].ToString();
        //    ssqk.Kssj = Convert.ToDateTime(ds.Tables[0].Rows[0][2]);
        //    ssqk.Mzys = ds.Tables[0].Rows[0][3].ToString();

        //}

        public int xgsqzd(string patid, string sqzd) //修改术前诊断
        {
            return dBConn.ExecuteNonQuery("update Adims_OTypesetting set pattmd='" + sqzd + "' where patid='" + patid + "'");
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
            return dBConn.ExecuteNonQuery("update Adims_OTypesetting set oname='" + nssss + "' where patid='" + patid + "'");
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
            return dBConn.ExecuteNonQuery("UPDATE Adims_OTypesetting WITH (ROWLOCK) set ap1='',ap2='',ap3='' where CONVERT(varchar, Odate , 23 )='" + d + "'");
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
