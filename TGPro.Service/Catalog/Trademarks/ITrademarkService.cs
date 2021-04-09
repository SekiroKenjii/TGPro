using System.Collections.Generic;
using System.Threading.Tasks;
using TGPro.Data.Entities;
using TGPro.Service.Common;
using TGPro.Service.DTOs.Trademarks;

namespace TGPro.Service.Catalog.Trademarks
{
    public interface ITrademarkService
    {
        Task<ApiResponse<string>> Create(TrademarkRequest request);

        Task<ApiResponse<string>> Update(int trademarkId, TrademarkRequest request);

        Task<ApiResponse<string>> Delete(int trademarkId);

        Task<ApiResponse<Trademark>> GetById(int trademarkId);

        Task<ApiResponse<List<Trademark>>> GetListTrademark();
    }
}
