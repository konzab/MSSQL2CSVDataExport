using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataExport.Helper;
using Dapper;
using DataExport.Model;
using DataExport.DataProcessing.Interfaces;

namespace DataExport.DataProcessing
{
    /// <summary>
    /// District Table data Extracts
    /// </summary>
    internal class Districts : IDistricts
    {
        /// <summary>
        /// Get a list of Districts to extract the data from
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetList()
        {
            List<string> districtList = new List<string>();
            try
            {
                IEnumerable<District> sqldump = new List<District>();
                string query = "SELECT DISTINCT [strDistricts_Description] as Name FROM [dbo].[mrefDistricts] ORDER BY [strDistricts_Description]";
                using (SqlConnection conn = ConfigurationReader.GetSqlConnection())
                {
                    conn.Open();
                    sqldump = await conn.QueryAsync<District>(query, CommandType.Text);
                }

                districtList = sqldump
                    .Where(item => !string.IsNullOrEmpty(item.Name))
                    .Select(item => item.Name.Trim())
                    .ToList();
                return districtList;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
    }
}