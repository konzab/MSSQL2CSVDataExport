using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.IO;
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
    /// Data Download
    /// </summary>
    internal class DataDownload : IDataDownload
    {
        public DataDownload()
        { }

        /// <summary>
        /// Read membership from database table
        /// </summary>
        /// <param name="district">district from which members reside</param>
        /// <returns></returns>
        public async Task ExportMembersListing(string district)
        {
            IEnumerable<dynamic> sqldump = new List<dynamic>();
            try
            {
                var sqlString = $@"SELECT DISTINCT RTRIM(Member.[strMemberNumber]) AS [Member Number], RTRIM(Member.[strOmangNumber]) AS [Omang Number], RTRIM(Member.[strSurName])  AS [Surname],RTRIM(Member.[strMiddleName]) AS [Middlename], RTRIM(Member.[strFirstName]) AS [Firstname], RTRIM([mrefTittles].[strTittleDescription]) AS [Tittle],Member.[dteBirthDate] AS [Birthdate], RTRIM(Member.[strPhysicalAddress]) AS [Physical Address], RTRIM(Member.[strPhysicalAddressLocation]) AS [Physical Address Location],RTRIM(Member.[strPhysicalAddressCity]) AS [Physical Address City], RTRIM(Member.[strPhysicalAddressCountry]) AS [Physical Address Country],RTRIM(Member.[strPostalAddress]) AS [Postal Address], RTRIM(Member.[strPostalAddressLocation]) AS  [Postal Address Location],RTRIM(Member.[strPostalAddressCity]) AS [Postal Address City],RTRIM(Member.[strPostalAddressCountry]) AS [Postal Address Country],RTRIM([mrefMembershipTypes].[strMembershipTypeDescription]) AS [Membership Type],RTRIM(Member.[strTelephoneHome]) AS [Tel. Home],RTRIM(Member.[strTelephoneWork]) AS Tel, Member.[strTelephoneMobile] AS Mobile,RTRIM(Member.[strTelephoneFax]) AS Fax,RTRIM([mrefDistricts].strDistricts_Description]) AS Region,(SELECT TOP 1 RTRIM([mrefDistrictConstituents].[strDistrictConstituentsDescription]) FROM [mrefDistrictConstituents] WHERE  [mrefDistrictConstituents].[intDistricts_ID] = Member.[intMemberConstituentId]) AS Constituent,(SELECT TOP 1 RTRIM([mrefWards].[strWardName]) FROM [dbo].[mrefWards] WHERE [mrefWards].[intWardID] = Member.[intMemberWardId])  AS Ward,CASE WHEN Member.[bitMaritalStatus] = 0 THEN 'Single'ELSE 'Married' END AS [Marital Status],Member.[dteDateJoined] AS [Joined Date],Member.[dteMembershipExpiry]  AS [Membership Expiry],RTRIM([dbo].[mrefOccupations].[strOccupationDescription]),CASE WHEN Member.[bitGender] = 0 THEN 'Female' ELSE 'Male' END AS Gender,Member.[dblAccountBalance],CASE WHEN Member.[bitMembershipDeactivated] =0 THEN 'False' ELSE 'True' END AS [Membership Deactivated],Member.[dteCapturedDate] AS [Captured Date]FROM [dbo].[dtMembers] Member  LEFT JOIN [dbo].[mrefTittles] ON (Member.[intTittle]= [mrefTittles].[intTittleId]) LEFT JOIN [dbo].[mrefMembershipTypes] ON (Member.[intMembershipType] = [mrefMembershipTypes].[intMembershipType]) LEFT JOIN [dbo].[mrefOccupations] ON (Member.[intOccupationId] = [dbo].[mrefOccupations].[intOccupationId]) LEFT JOIN [dbo].[mrefDistricts] ON (Member.[intMemberRegionId] = [mrefDistricts].[intDistricts_ID]) WHERE RTRIM([mrefDistricts].[strDistricts_Description]) = '{district}'";

                using (SqlConnection conn = new SqlConnection(ConfigurationReader.GetDatabaseConnectionString()))
                {
                    conn.Open();
                    sqldump = await conn.QueryAsync<dynamic>(sqlString, CommandType.Text);
                }

                ExportData(sqldump, district);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Export Data
        /// </summary>
        /// <param name="sqldump">data dump</param>
        /// <param name="district">district data being exported from</param>
        private void ExportData(IEnumerable<dynamic> sqldump, string district)
        {
            try
            {
                var filePath = Utility.GetExportFilePath(district);

                var csvConfiguration = new CsvConfiguration(CultureInfo.CurrentCulture);
                csvConfiguration.Delimiter = ",";

                using (var sr = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    using (var csv = new CsvWriter(sr, csvConfiguration))
                        csv.WriteRecords(sqldump);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}