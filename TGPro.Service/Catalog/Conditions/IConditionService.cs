using System.Collections.Generic;
using System.Threading.Tasks;
using TGPro.Data.Entities;
using TGPro.Service.ViewModel.Conditions;

namespace TGPro.Service.Catalog.Conditions
{
    public interface IConditionService
    {
        Task<int> Create(ConditionRequest request);

        Task<int> Update(int conditionId, ConditionRequest request);

        Task<int> Delete(int conditionId);

        Task<Condition> GetById(int conditionId);

        Task<List<Condition>> GetListCondition();
    }
}
