using adims_MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace adims_DAL
{
    /// <summary>
    /// 耗材使用
    /// </summary>
    public class ConsumablesUseDal
    {
        private DBConn dBConn = new DBConn();

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Model1"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string delete = "delete from Mzjld_ConsumablesUse  where Id='" + Id + "'";
            return dBConn.ExecuteNonQuery(string.Format(delete));
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="QXName"></param>
        /// <param name="XGName"></param>
        /// <returns></returns>
        public int Update(ConsumablesUseModel mod)
        {
            string update = @"update Mzjld_ConsumablesUse set "
                            + " Name='" + mod.Name + "',Dosage='" + mod.Dosage + "',"
                            + " Unit='" + mod.Unit + "',Price='" + mod.Price + "',IsCost='" + mod.IsCost + "'"
                            + " where Id='" + mod.Id + "'";
            return dBConn.ExecuteNonQuery(string.Format(update));
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public int Insert(ConsumablesUseModel mod)
        {
            string Insert = string.Format("Insert into Mzjld_ConsumablesUse(MzjldId,PatId,Name,Dosage,Unit,IsCost,Price,UseTime) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}') "
                , mod.MzjldId, mod.PatId, mod.Name, mod.Dosage, mod.Unit, mod.IsCost, mod.Price, mod.UseTime);
            return dBConn.ExecuteNonQuery(Insert);
        }

        /// <summary>
        /// 查询耗材使用
        /// </summary>
        /// <returns>麻醉记录单ID</returns>
        public DataTable GetByMzjldId(int MzjldID)
        {
            string str = "select Id,Name,Dosage,Price,Unit,IsCost,UseTime from Mzjld_ConsumablesUse where MzjldID ='" + MzjldID + "'";
            return dBConn.GetDataTable(str);
        }

        public DataTable GetKeshi()
        {
            string str = "select  distinct Patdpm from  Adims_OTypesetting  where Patdpm is not null ";
            return dBConn.GetDataTable(str);
        }
        public DataTable GetConsumablesUseGoupBy(ConsumablesUseGetInput input)
        {
            string str = @"select b.PatDpm,c.Name,c.Dosage,SUM(c.Dosage*c.Price *c.IsCost) sumPrice
                                    from Mzjld_ConsumablesUse c
                                    inner join  Adims_OTypesetting b on c.patid=b.patid 
                                    inner join Adims_Mzjld a on c.MzjldID=a.Id 
                                    where a.Otime between '{0}' and '{1}' 
                                    group by b.PatDpm,c.Name,c.Dosage";
            string sql = string.Format(str, input.BeginTime, input.EndTime);

            return dBConn.GetDataTable(sql);
        }
        public DataTable GetConsumablesUseList(ConsumablesUseGetInput input)
        {
            string str = @"select c.Name,c.Dosage,c.Unit,c.IsCost,c.Price,c.UseTime ,b.PatID,b.Patname,b.PatZhuYuanID
                                    from Mzjld_ConsumablesUse c
                                    inner join  Adims_OTypesetting b on c.patid=b.patid 
                                    inner join Adims_Mzjld a on c.MzjldID=a.Id 
                                    where a.Otime between '{0}' and '{1}' ";
            string sql = string.Format(str, input.BeginTime, input.EndTime);
            if (!string.IsNullOrEmpty(input.PatZhuyuanId))
            {
                sql += string.Format("and ( b.PatZhuyuanId ='{0}')", input.PatZhuyuanId);
            }
            if (!string.IsNullOrEmpty(input.PatDpm))
            {
                sql += string.Format("and ( b.PatDpm ='{0}')", input.PatDpm);
            }
            if (!string.IsNullOrEmpty(input.PatName))
            {
                sql += string.Format("and ( b.PatName ='{0}')", input.PatName);
            }
            return dBConn.GetDataTable(sql);
        }
        /// <summary>
        /// 查询耗材使用
        /// </summary>
        /// <returns>病人ID</returns>
        public DataTable GetByPatId(string PatId)
        {
            string str = "select * from Mzjld_ConsumablesUse where  PatId='" + PatId + "'";
            return dBConn.GetDataTable(str);
        }

        /// <summary>
        /// 查询所有耗材
        /// </summary>
        /// <returns></returns>
        public DataTable GetConsumablesAll()
        {
            string str = "select Id,Name,Price,Unit from Consumables";
            return dBConn.GetDataTable(str);
        }
        public DataTable GetConsumablesByName(string name)
        {
            string str = "select * from Consumables Where name='" + name + "'";
            return dBConn.GetDataTable(str);
        }

        public int InsertConsumables(string name, string price, string unit)
        {
            string Insert = string.Format("Insert into Consumables(Name,Price,Unit) values('{0}','{1}','{2}')", name, price, unit);

            return dBConn.ExecuteNonQuery(Insert);
        }

        public int UpdateConsumables(string name, int id)
        {
            string upd = string.Format("Update  Consumables set Name='{0}' where id='{1}'", name, id);

            return dBConn.ExecuteNonQuery(upd);
        }

        public int DeleteConsumables(int Id)
        {
            string delete = "delete from Consumables  where Id='" + Id + "'";
            return dBConn.ExecuteNonQuery(string.Format(delete));
        }
    }
}
