using System.Collections.Generic;
using System.Threading.Tasks;
using TGPro.Data.Entities;
using TGPro.Service.ViewModel.Demands;

namespace TGPro.Service.Catalog.Demands
{
    public interface IDemandService
    {
        Task<int> Create(DemandRequest request);

        Task<int> Update(int demandId, DemandRequest request);

        Task<int> Delete(int demandId);

        Task<Demand> GetById(int demandId);

        Task<List<Demand>> GetListDemand();
    }
}
