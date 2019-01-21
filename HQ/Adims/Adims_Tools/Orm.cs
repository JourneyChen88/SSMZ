using adims_MODEL;
using SI.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Adims_Tools
{
    public  static class Orm
    {



        public static List<MonitorRecord> GetAdims_MonitorRecord()
        {
            using (var context = AppDbContext.HeYiAdimsContext())
            {
                var strSQL = @"           
                    select b.*  from adims_mzjld a 
inner join Adims_MonitorRecord b on a.id=b.mzjldid
where a.IsZoom=1";
                var list = context.Sql(strSQL)
                     .QueryMany<MonitorRecord>();

                return list;
            }
        }
        public static List<Mzjld_Point> GetAdims_mzjld_Point()
        {
            using (var context = AppDbContext.HeYiAdimsContext())
            {
                string sql = @"select b.*  from adims_mzjld a 
inner join Adims_mzjld_Point b on a.id=b.mzjldid
where a.IsZoom=1";
                var list = context.Sql(sql)
                     .QueryMany<Mzjld_Point>();

                return list;
            }
        }

    }
}
