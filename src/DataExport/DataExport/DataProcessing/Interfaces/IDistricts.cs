using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataExport.DataProcessing.Interfaces
{
    internal interface IDistricts
    {
        Task<List<string>> GetList();
    }
}