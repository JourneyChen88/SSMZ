using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using FluentData;
using System.Windows.Forms;

namespace SI.Infrastructure.Service
{
    public enum DatabaseType
    {
        SqlServer,
        MySql,
        Oracle,
        DB2
    }

    public static class AppDbContext
    {
        private static ConnectionStringSettingsCollection _connectionStrings;

        static AppDbContext()
        {
            DbType = DatabaseType.SqlServer;
            GetConnectionStrings();
        }

        public static DatabaseType DbType { get; set; }

        private static void GetConnectionStrings()
        {
            try
            {
                string sFilePath = Application.StartupPath + @"\Connect.config";
                if (System.IO.File.Exists(sFilePath))
                {
                    ExeConfigurationFileMap file = new ExeConfigurationFileMap();
                    file.ExeConfigFilename = sFilePath;
                    Configuration config = ConfigurationManager.OpenMappedExeConfiguration(file, ConfigurationUserLevel.None);
                    _connectionStrings = config.ConnectionStrings.ConnectionStrings;
                }
                else
                {
                    _connectionStrings = ConfigurationManager.ConnectionStrings;
                }
            }
            catch (Exception)
            {
                _connectionStrings = ConfigurationManager.ConnectionStrings;
            }
        }

        private static IDbProvider CreateAppDbProvider()
        {
            IDbProvider dbProvider = null;

            switch (DbType)
            {
                case DatabaseType.SqlServer:
                    dbProvider = new SqlServerProvider();
                    break;
                case DatabaseType.MySql:
                    dbProvider = new MySqlProvider();
                    break;
                case DatabaseType.Oracle:
                    dbProvider = new OracleProvider();
                    break;
                default:
                    dbProvider = new SqlServerProvider();
                    break;
            }

            return dbProvider;
        }

        private static IDbContext CreateDbContext(string connectionName)
        {
            return new DbContext().ConnectionString(_connectionStrings[connectionName].ConnectionString, CreateAppDbProvider());
        }

        public static IDbContext HeYiAdimsContext()
        {
            return CreateDbContext("HeYiAdims");
        }

  
        
    }
}
