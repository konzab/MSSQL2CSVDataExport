using System.Threading.Tasks;

namespace DataExport.DataProcessing.Interfaces
{
    internal interface IDataDownload
    {
        Task ExportMembersListing(string district);
    }
}