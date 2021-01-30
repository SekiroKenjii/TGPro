using System.Collections.Generic;
using System.Threading.Tasks;
using TGPro.Data.Entities;
using TGPro.Service.ViewModel.Trademarks;

namespace TGPro.Service.Catalog.Trademarks
{
    public interface ITrademarkService
    {
        Task<int> Create(TrademarkRequest request);

        Task<int> Update(int trademarkId, TrademarkRequest request);

        Task<int> Delete(int trademarkId);

        Task<Trademark> GetById(int trademarkId);

        Task<List<Trademark>> GetListTrademark();
    }
}
