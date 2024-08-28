using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExport.Helper
{
    /// <summary>
    /// data sanitizer
    /// </summary>
    internal static class Sanitizer
    {
        /// <summary>
        /// Checks if connection variables has null values
        /// </summary>
        /// <returns></returns>
        public static bool CheckNullConnectionValues()
        {
            try
            {
                bool valuesAvailable = new[]
                {
                    ConfigurationReader.SERVERNAME,
                    ConfigurationReader.USERNAME,
                    ConfigurationReader.PASSWORD
                }.All(value => !string.IsNullOrEmpty(value));

                return valuesAvailable;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}