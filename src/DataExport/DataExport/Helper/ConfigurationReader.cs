using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DataExport.Helper
{
    internal static class ConfigurationReader
    {
        public static string USERNAME = ConfigurationManager.AppSettings["username"] != null ? ConfigurationManager.AppSettings["username"].ToString() : string.Empty;
        public static string PASSWORD = ConfigurationManager.AppSettings["password"] != null ? ConfigurationManager.AppSettings["password"].ToString() : string.Empty;
        public static string SERVERNAME => ConfigurationManager.AppSettings["ServerName"] != null ? ConfigurationManager.AppSettings["ServerName"].ToString() : string.Empty;

        public static string GetDatabaseConnectionString() => $@"Data Source={SERVERNAME};Initial Catalog=Members;User ID={USERNAME};Password={PASSWORD}";
    }
}