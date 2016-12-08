using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Configuration;

namespace Mysoft.Core
{
    public class DbProvider
    {
        private static string appName = ConfigurationManager.AppSettings["DbName"];
        private static string connStr = ConfigurationManager.ConnectionStrings[appName].ConnectionString;
        private static string providerName = ConfigurationManager.ConnectionStrings[appName].ProviderName;
       
        public static DbProviderFactory DbProviderFactory = DbProviderFactories.GetFactory(providerName);

        static DbProvider()
        {
           
        }
        public static DbConnection NewConnection()
        {
            var conn = DbProviderFactory.CreateConnection();
            conn.ConnectionString = connStr;
            return conn;
        }
        public static DbDataAdapter NewAdapter()
        {
            return DbProviderFactory.CreateDataAdapter();
        }
        
    }
}
