using System.Collections.Generic;
using System.Threading.Tasks;
using TGPro.Data.Entities;
using TGPro.Service.Common;
using TGPro.Service.DTOs.Demands;

namespace TGPro.Service.Catalog.Demands
{
    public interface IDemandService
    {
        Task<ApiResponse<string>> Create(DemandRequest request);

        Task<ApiResponse<string>> Update(int demandId, DemandRequest request);

        Task<ApiResponse<string>> Delete(int demandId);

        Task<ApiResponse<Demand>> GetById(int demandId);

        Task<ApiResponse<List<Demand>>> GetListDemand();
    }
}
