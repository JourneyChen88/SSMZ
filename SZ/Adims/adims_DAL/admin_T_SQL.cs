using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using adims_MODEL;
namespace adims_DAL
{

    public class admin_T_SQL
    {
        private DBConn dBConn = new DBConn();

        /// <summary>
        /// 器械清点的删除QiXieQD
        /// </summary>
        /// <param name="Model1"></param>
        /// <returns></returns>
        public int DeleteqxqdModel1(string Model1)
        {
            string delete = "delete [Adims_qxqdModel] where [Model]='" + Model1 + "'";
            return dBConn.ExecuteNonQuery(string.Format(delete));
        }
        /// <summary>
        /// 器械清点的删除QiXieQD
        /// </summary>
        /// <param name="Model1"></param>
        /// <returns></returns>
        public int DeleteqxModelName(string Model)
        {
            string delete = "delete [adims_qxModelName] where [Modelname]='" + Model + "'";
            return dBConn.ExecuteNonQuery(string.Format(delete));
        }
        /// <summary>
        /// 修改器械模板器械
        /// </summary>
        /// <param name="QXName"></param>
        /// <param name="XGName"></param>
        /// <returns></returns>
        public int updateQXMC(string ModelName, string XGName)
        {
            string update = "update Adims_qxqdModel set model='" + XGName + "' where model='" + ModelName + "'";
            return dBConn.ExecuteNonQuery(string.Format(update));
        }
        /// <summary>
        /// 修改器械模板器械
        /// </summary>
        /// <param name="QXName"></param>
        /// <param name="XGName"></param>
        /// <returns></returns>
        public int updateModelName(string ModelName, string XGName)
        {
            string update = "update adims_qxModelName set modelname='" + XGName + "' where modelname='" + ModelName + "'";
            return dBConn.ExecuteNonQuery(string.Format(update));
        }
        /// <summary>
        /// 增减器械空模板
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public int InsertqxqdModel(string mod)
        {
            string Insert = "Insert into adims_qxModelName(ModelName) values('" + mod + "') ";
            return dBConn.ExecuteNonQuery(string.Format(Insert));
        }
        /// <summary>
        /// 增减器械和模板
        /// </summary>
        /// <param name="QXmc"></param>
        /// <param name="mod"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int InsertqxqdModelQXMC(string QXmc, string mod, int count)
        {
            string Insert = "Insert into Adims_qxqdModel(qxmc,Model,qxCount) values('" + QXmc + "','" + mod + "','" + count + "') ";
            return dBConn.ExecuteNonQuery(string.Format(Insert));
        }
        /// <summary>
        /// 药品进库查询
        /// </summary>
        /// <returns></returns>
        public DataSet select_surgery_input(string sql_where)
        {
            string coAdims_str = "select adims_room_input.room_id 手术间号,Adims_surgery_input.medicine_number 药品编号," +
                "Adims_surgery_input.input_count 输入数量,Adims_surgery_input.input_time 输入时间,Adims_surgery_input.confirm_person 确认人," +
                "medicine_name 药品名称,phonetic_prefix 拼音字头,toxicology 毒理,state 状态," +
                "dosagy_form 剂型,specification 规格,produce_time 生产日期,deadline 有效日期," +
                "batch_number 批次,origin_place 产地 from Adims_medicine_info,Adims_surgery_input " +
                "where Adims_medicine_info.medicine_number=Adims_surgery_input.medicine_number" + sql_where + " order by Adims_surgery_input.input_time DESC";
            coAdims_str = "select adims_room_input.room_id 手术间号,adims_room_input.medicine_number 药品编号,adims_room_input.input_count 输入数量,adims_room_input.input_time 输入时间"
                + ",adims_room_input.confirm_person 确认人,medicine_name 药品名称,phonetic_prefix 拼音字头,toxicology 毒理,state 状态,dosagy_form 剂型,specification 规格,Convert(varchar ,produce_time,23) 生产日期"
                + ",Convert(varchar ,deadline,23) 有效日期,batch_number 批次,origin_place 产地  from Adims_medicine_info, adims_room_input "
                + "where Adims_medicine_info.medicine_number=adims_room_input.medicine_number" + sql_where + " order by adims_room_input.input_time DESC";
            return dBConn.GetDataSet(coAdims_str);
        }
        #region 删除误建的麻醉记录单
        /// <summary>
        /// 删除多余的新麻醉记录单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int delectadmin_mzjld(int id)
        {
            string delectmzjldcxtj = "delete from  Adims_mzjld where id= '" + id + "'";
            return dBConn.ExecuteNonQuery(delectmzjldcxtj);
        }
        /// <summary>
        /// 删除多余的新麻醉记录单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object select_zt(int id, string time)
        {
            string delectmzjldcxtj = " select ZT from dbo.Adims_Mzjld where id='" + id + "'";
            return dBConn.ExecuteScalar(delectmzjldcxtj);
        }
        /// <summary>
        /// 麻醉记录单的状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public int update_mzjld_ZT(int id)
        {
            string sql = " update dbo.Adims_Mzjld set ZT=1 where  id='" + id + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        #endregion
        #region 修改手术间
        /// <summary>
        /// 修改排班完成的手术间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int updatePaibanOroom(string patid, string oroom, string second)
        {
            string sql = "update  Adims_OTypesetting  set oroom='" + oroom + "',second= '" + second + "' where patid= '" + patid + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        #endregion
        #region ASA等级
        /// <summary>
        /// 麻醉月报统计
        /// </summary>
        /// <returns></returns>
        public object MZ_Y_TJ_I(string t1, string t2)
        {
            string TJ_I = "select COUNT(*)  from Adims_mzjld where  CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "' and ASA='Ⅰ'";
            return dBConn.ExecuteScalar(TJ_I);
        }
        /// <summary>
        /// Ⅱ级
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZ_Y_TJ_Ⅱ(string t1, string t2)
        {
            string TJ_Ⅱ = "select COUNT(*)   from Adims_mzjld where  CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "' and  ASA='Ⅱ'";
            return dBConn.ExecuteScalar(TJ_Ⅱ);

        }
        /// <summary>
        /// Ⅲ级
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZ_Y_TJ_Ⅲ(string t1, string t2)
        {
            string TJ_Ⅲ = "select COUNT(*)   from Adims_mzjld where  CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "' and  ASA='Ⅲ'";
            return dBConn.ExecuteScalar(TJ_Ⅲ);
        }
        /// <summary>
        /// Ⅳ级
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZ_Y_TJ_Ⅳ(string t1, string t2)
        {
            string TJ_Ⅳ = "select COUNT(*)   from Adims_mzjld where  CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "' and  ASA='Ⅳ'";
            return dBConn.ExecuteScalar(TJ_Ⅳ);
        }
        /// <summary>
        /// Ⅴ级
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZ_Y_TJ_Ⅴ(string t1, string t2)
        {
            string TJ_Ⅴ = "select COUNT(*)   from Adims_mzjld where  CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "' and  ASA='Ⅴ'";
            return dBConn.ExecuteScalar(TJ_Ⅴ);
        }
        /// <summary>
        /// E的统计
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZ_Y_TJ_E(string t1, string t2)
        {
            string TJ_E = "select COUNT(*)   from Adims_mzjld where  CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "' and  ASAE='1'";
            return dBConn.ExecuteScalar(TJ_E);
        }
        /// <summary>
        /// pacu统计
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZ_Y_TJ_RR(string t1, string t2)
        {
            string TJ_E = "select count(*) from Adims_mzjld where  CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "' and  brqx='pacu'";
            return dBConn.ExecuteScalar(TJ_E);
        }
        #endregion
        #region 老年麻醉
        /// <summary>
        /// 麻醉总数
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object LaoNianMazui(string t1, string t2)
        {
            string TJ_ALL = "select count(*) from Adims_mzjld as M inner join Adims_OTypesetting as O on O.patid=M.patid where  CONVERT(varchar, M.otime , 23 ) between '" + t1 + "' and '" + t2 + "' and patage>'70'";
            return dBConn.ExecuteScalar(TJ_ALL);
        }
        #endregion
        #region 少儿麻醉
        /// <summary>
        /// 麻醉总数
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object ShaoErMazui(string t1, string t2)
        {
            string TJ_ALL = "select count(*) from Adims_mzjld as M inner join Adims_OTypesetting as O on O.patid=M.patid where  CONVERT(varchar, M.otime , 23 ) between '" + t1 + "' and '" + t2 + "' and (O.patage<'12' or O.ageDw='月')";
            return dBConn.ExecuteScalar(TJ_ALL);
        }
        #endregion
        #region 产科麻醉
        /// <summary>
        /// 麻醉总数
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object ChankeMazui(string t1, string t2)
        {
            string TJ_ALL = "select count(*) from Adims_mzjld as M inner join Adims_OTypesetting as O on O.patid=M.patid where  CONVERT(varchar, M.otime , 23 ) between '" + t1 + "' and '" + t2 + "' and patdpm='盛泽妇幼产科'";
            return dBConn.ExecuteScalar(TJ_ALL);
        }
        #endregion
        #region 动脉穿刺麻醉
        /// <summary>
        /// 麻醉总数
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object DongMaiCC(string t1, string t2)
        {
            string TJ_ALL = "select count(*) from Adims_mzjld as M inner join Adims_mzzongjie_SZ as O on M.id=O.mzjldid where  CONVERT(varchar, M.otime , 23 ) between '" + t1 + "' and '" + t2 + "' and DMchuanci>'1'";
            return dBConn.ExecuteScalar(TJ_ALL);
        }

        public object ShenJingMaiCC(string t1, string t2)//深静脉穿刺
        {
            string TJ_ALL = "select count(*) from Adims_mzjld as M inner join Adims_mzzongjie_SZ as O on M.id=O.mzjldid where  CONVERT(varchar, M.otime , 23 ) between '" + t1 + "' and '" + t2 + "' and SJMchuanci>'1'";
            return dBConn.ExecuteScalar(TJ_ALL);
        }
        #endregion
        #region 手术室内麻醉
        /// <summary>
        /// 麻醉总数
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZ_Y_TJ_ALL(string t1, string t2)
        {
            string TJ_ALL = "select count(*) from Adims_mzjld where  CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "'";
            return dBConn.ExecuteScalar(TJ_ALL);
        }
        /// <summary>
        /// 吸入式麻醉
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZ_Y_XRMZ_TJ(string t1, string t2)
        {
            string XRTJ = "select count(*) from Adims_mzjld where   CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "' and  mzfa like '%吸入麻醉%'";
            return dBConn.ExecuteScalar(XRTJ);
        }
        /// <summary>
        /// 静脉麻醉
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZ_Y_JMMZ_TJ(string t1, string t2)
        {
            string JMTJ = "select count(*) from Adims_mzjld where   CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "' and  mzfa like '%静脉麻醉%' or mzfa like '%静脉%'";
            return dBConn.ExecuteScalar(JMTJ);
        }
        /// <summary>
        /// 静吸复合麻醉
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZ_Y_JXFHMZ_TJ(string t1, string t2)
        {
            string JXFHTJ = "select count(*) from Adims_mzjld where   CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "' and  mzfa like '%静吸%' or mzfa='%静吸复合麻醉%'";
            return dBConn.ExecuteScalar(JXFHTJ);
        }

        /// <summary>
        /// 联合麻醉
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZ_Y_LHMZ_TJ(string t1, string t2)
        {
            string LHMZTJ = "select count(*) from Adims_mzjld where   CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "' and  zzff like '%联合麻醉%'";
            return dBConn.ExecuteScalar(LHMZTJ);
        }
        /// <summary>
        /// 腰麻
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZ_Y_YMZ_TJ(string t1, string t2)
        {
            string YMTJ = "select count(*) from Adims_mzjld where   CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "' and  zzff like '%腰麻%'";
            return dBConn.ExecuteScalar(YMTJ);
        }
        /// <summary>
        /// 硬膜外
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZ_Y_YMWMZ_TJ(string t1, string t2)
        {
            string YMWMZTJ = "select count(*) from Adims_mzjld where   CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "' and  zzff like '%硬膜外%'";
            return dBConn.ExecuteScalar(YMWMZTJ);
        }
        /// <summary>
        /// 骶骨
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZ_Y_DGMZ_TJ(string t1, string t2)
        {
            string DGMZTJ = "select count(*) from Adims_mzjld where   CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "' and  zzff like '%骶骨%' ";
            return dBConn.ExecuteScalar(DGMZTJ);
        }
        /// <summary>
        /// 腰硬联合
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZ_Y_YYLHMZ_TJ(string t1, string t2)
        {
            string YYLHMZ = "select count(*) from Adims_mzjld where   CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "' and  zzff like '%腰硬联合%' ";
            return dBConn.ExecuteScalar(YYLHMZ);
        }
        /// <summary>
        /// 半身麻醉
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZ_Y_BSMZ_TJ(string t1, string t2)
        {
            string BSMZ = "select count(*) from Adims_mzjld where   CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "' and  zzff like '%半身麻醉%' ";
            return dBConn.ExecuteScalar(BSMZ);
        }
        /// <summary>
        /// 臂丛神经阻滞
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZ_Y_BCSJZZMZ_TJ(string t1, string t2)
        {
            string BCSJZZMZ = "select count(*) from Adims_mzjld where   CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "' and  zzff like '%臂丛神经阻滞%' or zzff like '%臂丛%' ";
            return dBConn.ExecuteScalar(BCSJZZMZ);
        }
        /// <summary>
        /// 局部浸润
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZ_Y_JBJRMZ_TJ(string t1, string t2)
        {
            string JBJRMZ = "select count(*) from Adims_mzjld where   CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "' and  zzff like '%局部浸润%' ";
            return dBConn.ExecuteScalar(JBJRMZ);
        }
        /// <summary>
        /// 其他麻醉
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZ_Y_QTMZ_TJ(string t1, string t2)
        {
            string QTMZTJ = "select count(*) from Adims_mzjld where   CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "' and zzff ='' and mzfa ='' ";
            return dBConn.ExecuteScalar(QTMZTJ);
        }
        /// <summary>
        /// 全麻例数
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZ_Y_QM_TJ(string t1, string t2)
        {
            string QMTJ = "select count(*)  from Adims_mzjld where   CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "' and  mzfa != ''";
            return dBConn.ExecuteScalar(QMTJ);
        }
        #endregion
        #region 专科麻醉统计
        /// <summary>
        /// 耳鼻喉科
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZYBTJ_EBHK(string t1, string t2)
        {
            string sql = " SELECT count(*) FROM Adims_mzjld as A inner join Adims_OTypesetting as B on a.patid=b.patid"
                + " where patdpm like '%耳鼻喉科%' and  CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "'";
            return dBConn.ExecuteScalar(sql);
        }
        /// <summary>
        /// 泌尿外科
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZYBTJ_MNWK(string t1, string t2)
        {
            string sql = " SELECT count(*) FROM Adims_mzjld as A inner join Adims_OTypesetting as B on a.patid=b.patid "
                + " where patdpm like '%泌尿外科%' and  CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "'";
            return dBConn.ExecuteScalar(sql);
        }
        /// <summary>
        /// 肛肠外科
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZYBTJ_GCWK(string t1, string t2)
        {
            string sql = " SELECT count(*) FROM Adims_mzjld as A inner join Adims_OTypesetting as B on a.patid=b.patid"
                + " where patdpm like '%肛肠外科%' and  CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "'";
            return dBConn.ExecuteScalar(sql);
        }
        /// <summary>
        /// 甲乳外科
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZYBTJ_JRWK(string t1, string t2)
        {
            string sql = " SELECT count(*) FROM Adims_mzjld as A inner join Adims_OTypesetting as B on a.patid=b.patid "
                + " where patdpm like '%甲乳外科%' and  CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "'";
            return dBConn.ExecuteScalar(sql);
        }
        /// <summary>
        /// 神经外科
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZYBTJ_SJWK(string t1, string t2)
        {
            string sql = " SELECT count(*) FROM Adims_mzjld as A inner join Adims_OTypesetting as B on a.patid=b.patid "
                + "where patdpm like '%神经外科%' and  CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "'";
            return dBConn.ExecuteScalar(sql);
        }
        /// <summary>
        /// 整形美容烧伤科
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZYBTJ_ZXMRSHWK(string t1, string t2)
        {
            string sql = " SELECT count(*) FROM Adims_mzjld as A inner join Adims_OTypesetting as B on a.patid=b.patid "
                + "where patdpm like '%整形美容烧伤科%' and  CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "'";
            return dBConn.ExecuteScalar(sql);
        }
        /// <summary>
        /// 普外科一
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZYBTJ_P1(string t1, string t2)
        {
            string sql = "SELECT count(*) FROM Adims_mzjld as A inner join Adims_OTypesetting as B on a.patid=b.patid"
                + " where patdpm like '%普外科一%' and  CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "'";
            return dBConn.ExecuteScalar(sql);
        }
        /// <summary>
        /// 盛泽普外科二
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZYBTJ_P2(string t1, string t2)
        {
            string sql = " SELECT count(*) FROM Adims_mzjld as A inner join Adims_OTypesetting as B on a.patid=b.patid"
                + " where patdpm like '%普外科二%' and  CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "'";
            return dBConn.ExecuteScalar(sql);
        }
        /// <summary>
        /// 盛泽普外科三
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZYBTJ_P3(string t1, string t2)
        {
            string sql = " SELECT count(*) FROM Adims_mzjld as A inner join Adims_OTypesetting as B on a.patid=b.patid"
                + " where patdpm like '%普外科三%' and  CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "'";
            return dBConn.ExecuteScalar(sql);
        }
        /// <summary>
        /// 骨科
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZYBTJ_GK(string t1, string t2)
        {
            string sql = " SELECT count(*) FROM Adims_mzjld as A inner join Adims_OTypesetting as B on a.patid=b.patid"
                + " where patdpm like '%骨科%' and  CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "'";
            return dBConn.ExecuteScalar(sql);
        }

        /// <summary>
        /// 妇幼妇产科
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZYBTJ_FYFCK(string t1, string t2)
        {
            string sql = " SELECT count(*) FROM Adims_mzjld as A inner join Adims_OTypesetting as B on a.patid=b.patid "
                + "where   CONVERT(varchar, A.otime , 23 ) between '" + t1 + "' and '" + t2 + "' and (B.patdpm like '%妇幼产科%' or B.patdpm like '%妇幼妇科%')";
            return dBConn.ExecuteScalar(sql);
        }
        /// <summary>
        /// 消化内科
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZYBTJ_XHNK(string t1, string t2)
        {
            string sql = " SELECT count(*) FROM Adims_mzjld as A inner join Adims_OTypesetting as B on a.patid=b.patid "
                + "where patdpm like '%消化内科%' and  CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "'";
            return dBConn.ExecuteScalar(sql);
        }
        /// <summary>
        /// 胸心外科
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object MZYBTJ_XXWK(string t1, string t2)
        {
            string sql = " SELECT count(*) FROM Adims_mzjld as A inner join Adims_OTypesetting as B on a.patid=b.patid"
                + " where patdpm like '%胸心外科%' and  CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "'";
            return dBConn.ExecuteScalar(sql);
        }

        #endregion
        #region 硬膜外阻滞成功率
        /// <summary>
        ///  硬膜外阻滞总数
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object YMWZZ_TJ(string t1, string t2)
        {
            string YMWZZTJ = "select count(*) from adims_mzjld where zzff like '%硬膜外阻滞%' and  CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "'";
            return dBConn.ExecuteScalar(YMWZZTJ);
        }
        /// <summary>
        ///  硬膜外阻滞成功数
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object YMWZZCGL_TJ(string t1, string t2)
        {
            string YMWZZCGL = "select count(*) from adims_mzjld where zzff like '%硬膜外阻滞%' and mzxg='优'  and   CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "'";
            return dBConn.ExecuteScalar(YMWZZCGL);
        }
        #endregion
        #region 术前检查评估准备率
        /// <summary>
        ///  术前的总数
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object SQFSZS_Y_TJ(string t1, string t2)
        {
            string SQFSZS = "select count(*) from Adims_BeforeVisit_HS as hs left join Adims_BeforeVisit_YS as ys on hs.patid=ys.patid where "
            + "CONVERT(varchar, hs.visitDate , 23 ) between '" + t1 + "' and '" + t2 + "'";
            return dBConn.ExecuteScalar(SQFSZS);
        }
        /// <summary>
        ///  术前的准备数
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object SQFSZBS_Y_TJ(string t1, string t2)
        {
            string SQFSZBZS = "select count(*) from Adims_BeforeVisit_HS as hs inner join Adims_BeforeVisit_YS as ys on hs.patid=ys.patid where "
            + "CONVERT(varchar, hs.visitDate , 23 ) between '" + t1 + "' and '" + t2 + "'";
            return dBConn.ExecuteScalar(SQFSZBZS);
        }
        #endregion
        #region 术后访视率

        /// <summary>
        ///  术后访视总数
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object SHFSZS_Y_TJ(string t1, string t2)
        {
            string SHFSZS = "select count(*) from Adims_AfterVisit_SZ where CONVERT(varchar, visitdate , 23 ) between '" + t1 + "' and '" + t2 + "'";
            return dBConn.ExecuteScalar(SHFSZS);
        }
        /// <summary>
        ///  术后访视访视列数
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object SHFSLS_Y_TJ(string t1, string t2)
        {
            string SHFSLS = " select count(*) from Adims_AfterVisit_SZ as aa join adims_mzjld as am on aa.patid =am.patid "
                + " where CONVERT(varchar, visitdate , 23 ) between '" + t1 + "' and '" + t2 + "'";
            return dBConn.ExecuteScalar(SHFSLS);
        }
        #endregion
        #region 全麻术中知晓率
        /// <summary>
        ///  全身麻醉的总数
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object QMZSZS_Y_TJ(string t1, string t2)
        {
            string QMZSZS = " select count(*) from Adims_mzjld where mzfa != ''  and "
            + "CONVERT(varchar, otime , 23 ) between '" + t1 + "' and '" + t2 + "' ";
            return dBConn.ExecuteScalar(QMZSZS);
        }
        /// <summary>
        ///  知晓数
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public object QMZSLS_Y_TJ(string t1, string t2)
        {
            string QMZSLS = "select  distinct count(*) from Adims_mzzongjie_SZ as aa inner join Adims_mzjld as am on aa.patid =am.patid"
                + " where tongyishu = '有' and mzfa !='' and CONVERT(varchar,otime , 23 )  between '" + t1 + "' and '" + t2 + "'";
            return dBConn.ExecuteScalar(QMZSLS);
        }
        #endregion
        #region 手术暂停
        /// <summary>
        /// 手术暂停
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public int ZTSS(string patid)
        //{
        //    string ZT = "select ostate from Adims_OTypesetting where patid='" + patid + "'";
        //    return dBConn.ExecuteNonQuery(ZT);
        //}
        public int ZTSS1(string patid)
        {
            string ZT = "select count(*) from Adims_mzjld where patid='" + patid + "'";
            return dBConn.ExecuteNonQuery(ZT);
        }
        public int deleteZT(string id)
        {
            string ZT = "delete from Adims_OTypesetting where id ='" + id + "'";
            return dBConn.ExecuteNonQuery(ZT);
        }

        public int UpdateZT(string id)
        {
            string ZT = "update Adims_OTypesetting set ostate ='2' where id ='" + id + "'";
            return dBConn.ExecuteNonQuery(ZT);
        }
       
        #endregion
        #region 科室
        /// <summary>
        /// 查询所有科室
        /// </summary>
        /// <returns></returns>
        public DataTable select_Patdpm()
        {
            string str = "select id 科室编号,name 科室名称  from keshi ";
            return dBConn.GetDataTable(str);
        }
        /// <summary>
        /// 查询所有科室
        /// </summary>
        /// <returns></returns>
        public DataTable select_Patdpm1(string sqlwhere)
        {
            string str = "select id 科室编号,name 科室名称  from keshi  where name='" + sqlwhere + "'";
            return dBConn.GetDataTable(str);
        }
        /// <summary>
        /// 删除科室
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int delete_patdpm(string id)
        {
            string str = "delete keshi where id ='" + id + "' ";
            return dBConn.ExecuteNonQuery(str);
        }
        /// <summary>
        /// 修改科室
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int update_patdpm(string id, string KSname)
        {
            string str = "update keshi set name='" + KSname + "'where id ='" + id + "' ";
            return dBConn.ExecuteNonQuery(str);
        }
        /// <summary>
        /// 增加科室
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int inset_patdpm(string KSname)
        {
            string str = "insert into keshi (name) values('" + KSname + "')";
            return dBConn.ExecuteNonQuery(str);
        }

        #endregion
        #region 万能查询下拉框的绑定
        /// <summary>
        /// 麻醉单查询（查询所有手术医生）
        /// </summary>
        /// <returns></returns>
        public DataTable Select_SSYS()
        {
            string sql = "select distinct os  from Adims_OTypesetting where os!=''";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 麻醉单查询（查询所有手术麻醉医生）
        /// </summary>
        /// <returns></returns>
        public DataTable Select_MZYS()
        {
            string sql = "select user_name as 姓名 from adims_user where type!='2' and type!=0";
            return dBConn.GetDataTable(sql);
        }

        /// <summary>
        /// 麻醉单查询（查询所有麻醉方式）
        /// </summary>
        /// <returns></returns>
        public DataTable Select_amethod()
        {
            string sql = "select distinct amethod from Adims_OTypesetting ";
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 麻醉单查询（查询所有体位）
        /// </summary>
        /// <returns></returns>
        public DataTable Select_TW()
        {
            string sql = "select distinct tw from adims_mzjld  where  tw!=''";
            return dBConn.GetDataTable(sql);
        }


        #endregion
        #region PACU保存
        public int update_PACU(string Weight, string zid, string pid)
        {
            string sql = "update Adims_OTypesetting set patweight='" + Weight + "' where patzhuyuanID='" + zid + "' and patid='" + pid + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        #endregion
        #region 手动控制麻醉记录单打印时间
        public int update_otime(string time, string id)
        {
            string sql = "update Adims_Mzjld set otime='" + time + "' where id='" + id + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int update_otimePACU(string time, string mzid)
        {
            string sql = "update Adims_PACU_SZ set otime='" + time + "' where mzjldid='" + mzid + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        #endregion
        #region 一个病人只能打开用一次

        #endregion
        #region 药品管理banding麻醉师编号
        //public DataTable GetUID()
        //{
        //    string sql = "select uid  from dbo.Adims_User  where type=1";
        //    return dBConn.GetDataTable(sql);
        //}
        #endregion
        #region 打开麻醉记录单的病人去向
        public object BDbrqx(int mzid)
        {
            string sql = "select brqx from adims_mzjld where id='" + mzid + "'";
            return dBConn.ExecuteScalar(sql);
        }
        #endregion
        #region 药编号
        public DataTable Getmedicine_number()
        {
            string sql = "select medicine_number  from adims_medicine_info";
            return dBConn.GetDataTable(sql);
        }
        #endregion
        public object GetCardno(string pID)
        {
            string sql = "select cardno from Adims_OTypesetting where patid='" + pID + "'";
            return dBConn.ExecuteScalar(sql);
        }
        public object GeApplyid(string pID)
        {
            string sql = "select applyid from Adims_OTypesetting where patid='" + pID + "'";
            return dBConn.ExecuteScalar(sql);
        }
        public DataTable GetMzjldPointInServer(DateTime dt, int mzjldid)
        {
            string sql = "select RecordTime from Adims_mzjld_Point where RecordTime='" + dt + "' and  mzjldid='" + mzjldid + "'";
            return dBConn.GetDataTable(sql);
        }
        public object getCountPACU_Porint(DateTime dt, int mzjldid)
        {
            string sql = "select count(*) from Adims_Pacu_Point where RecordTime='" + dt + "'and  mzjldid='" + mzjldid + "'";
            return dBConn.ExecuteScalar(sql);
        }
    }
}
