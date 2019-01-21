using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using SI.Infrastructure.Service.Dependency;

namespace SI.Infrastructure.Service
{
    public abstract class ServiceBase
    {
        protected ServiceBase()
        {
            var callAsmFileName = new FileInfo(Assembly.GetCallingAssembly().Location).Name;
            if (AppDbContext.DbType == DatabaseType.SqlServer)
            {
                if (callAsmFileName.Contains("HisService.Pharmacy"))
                {
                    
                    callAsmFileName = callAsmFileName.Replace(".dll", ".MSSQL.dll");
                }
            }
            else
            {
                throw new InvalidOperationException(string.Format("DbType {0} has not been implemented", AppDbContext.DbType.ToString()));
            }

            var asm = Assembly.LoadFrom(Path.Combine(HttpRuntime.AppDomainAppPath, "bin", callAsmFileName));

            var dalTypes = from type in asm.GetTypes()
                           where typeof(ITransientDependency).IsAssignableFrom(type)
                           select type;

            ConventionalTypes = dalTypes.ToList();
        }

        protected List<Type> ConventionalTypes { get; private set; }

        protected object CreateInstance<T>()
        {
            //排除I开始的接类
            var type = ConventionalTypes.FirstOrDefault(t => typeof(T).IsAssignableFrom(t) && !t.Name.StartsWith("I"));
            if (type == null)
            {
                throw new InvalidOperationException(string.Format("Can not find type implement type: {0}.", typeof(T).FullName));
            }

            var ctor = type.GetConstructor(new Type[] { });

            if (ctor == null)
            {
                throw new InvalidOperationException("Can not find type with default ctor.");
            }

            var obj = ctor.Invoke(new object[] { });
            return obj;
        }
    }
}
