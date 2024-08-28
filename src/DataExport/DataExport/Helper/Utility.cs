using DataExport.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExport.Helper
{
    internal static class Utility
    {
        /// <summary>
        /// Get a name of the export file
        /// </summary>
        /// <param name="district">name of district</param>
        /// <returns></returns>
        public static string GetDistrictExportFileName(string district) => $"{DateTime.Now:yyyyMMdd}-{district}-datadump.csv";

        public static string GetExportFilePath(string district)
        {
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), GetDistrictExportFileName(district));

                if (File.Exists(filePath))
                    File.Delete(filePath);

                return filePath;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}