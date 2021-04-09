using System.Collections.Generic;
using System.Threading.Tasks;
using TGPro.Data.Entities;
using TGPro.Service.Common;
using TGPro.Service.ViewModel.Conditions;

namespace TGPro.Service.Catalog.Conditions
{
    public interface IConditionService
    {
        Task<ApiResponse<string>> Create(ConditionRequest request);

        Task<ApiResponse<string>> Update(int conditionId, ConditionRequest request);

        Task<ApiResponse<string>> Delete(int conditionId);

        Task<ApiResponse<Condition>> GetById(int conditionId);

        Task<ApiResponse<List<Condition>>> GetListCondition();
    }
}
