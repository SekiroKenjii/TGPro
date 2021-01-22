using System.IO;
using System.Threading.Tasks;

namespace TGPro.Service.Common
{
    public interface IStorageService
    {
        string GetProductFileUrl(string fileName);
        string GetTrademarkFileUrl(string fileName);
        Task SaveProductFileAsync(Stream mediaBinaryStream, string fileName);
        Task SaveTrademarkFileAsync(Stream mediaBinaryStream, string fileName);
        Task DeleteProductFileAsync(string fileName);
        Task DeleteTrademarkFileAsync(string fileName);
    }
}
