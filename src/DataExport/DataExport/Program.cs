using DataExport.DataProcessing;
using DataExport.DataProcessing.Interfaces;
using DataExport.Helper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DataExport
{
    internal class Program
    {
        public static IDistricts _districts;
        public static IDataDownload _dataDownload;

        private static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            try
            {
                _districts = new Districts();
                _dataDownload = new DataDownload();

                var table = new DataTable();

                if (!Sanitizer.CheckNullConnectionValues())
                    throw new ArgumentException("One or more database connection values missing...");

                List<string> districts = _districts.GetList().Result;

                await DownloadData(districts);

                Log.Logger.Information("Data Export Completed...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"An error occured: {ex.Message}");
                Console.ReadKey();
                Log.CloseAndFlush();
            }
        }

        /// <summary>
        /// Download Data based on the districts in the database
        /// </summary>
        /// <param name="districts"></param>
        /// <returns></returns>
        private static async Task DownloadData(List<string> districts)
        {
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    foreach (var district in districts)
                    {
                        string currentDistrict = district.Trim().Replace(@"/", "_").ToUpper();
                        try
                        {
                            Log.Logger.Information($@"Downloading data for : {currentDistrict}...");
                            _dataDownload.ExportMembersListing(currentDistrict).Wait();
                            Log.Logger.Information($@"{currentDistrict} data downloading completed...");
                        }
                        catch (Exception ex)
                        {
                            Log.Logger.Error($@"An error occured while processing {currentDistrict}, {ex.Message}");
                        }
                    }
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}