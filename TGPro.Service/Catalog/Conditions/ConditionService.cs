using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGPro.Data.EF;
using TGPro.Data.Entities;
using TGPro.Service.Common;
using TGPro.Service.SystemResources;
using TGPro.Service.ViewModel.Conditions;

namespace TGPro.Service.Catalog.Conditions
{
    public class ConditionService : IConditionService
    {
        private readonly TGProDbContext _db;
        public ConditionService(TGProDbContext db)
        {
            _db = db;
        }

        public async Task<ApiResponse<string>> Create(ConditionRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return new ApiErrorResponse<string>(ConstantStrings.emptyNameFieldError);
            var condition = new Condition()
            {
                Name = request.Name,
                Description = request.Description
            };
            _db.Conditions.Add(condition);
            await _db.SaveChangesAsync();
            return new ApiSuccessResponse<string>(ConstantStrings.addSuccessfully);
        }

        public async Task<ApiResponse<string>> Delete(int conditionId)
        {
            var conditionFromDb = await _db.Conditions.FindAsync(conditionId);
            if (conditionFromDb == null)
                return new ApiErrorResponse<string>(ConstantStrings.FindByIdError(conditionId));
            _db.Conditions.Remove(conditionFromDb);
            await _db.SaveChangesAsync();
            return new ApiSuccessResponse<string>(ConstantStrings.deleteSuccessfully);
        }

        public async Task<ApiResponse<Condition>> GetById(int conditionId)
        {
            var conditionFromDb = await _db.Conditions.FindAsync(conditionId);
            if (conditionFromDb == null)
                return new ApiErrorResponse<Condition>(ConstantStrings.FindByIdError(conditionId));
            return new ApiSuccessResponse<Condition>(conditionFromDb);
        }

        public async Task<ApiResponse<List<Condition>>> GetListCondition()
        {
            List<Condition> lstCondition = await _db.Conditions.OrderBy(c => c.Name).ToListAsync();
            if (lstCondition.Count == 0)
                return new ApiErrorResponse<List<Condition>>(ConstantStrings.getAllError);
            return new ApiSuccessResponse<List<Condition>>(lstCondition);
        }

        public async Task<ApiResponse<string>> Update(int conditionId, ConditionRequest request)
        {
            var conditionFromDb = await _db.Conditions.FindAsync(conditionId);
            if (conditionFromDb == null)
                return new ApiErrorResponse<string>(ConstantStrings.getAllError);
            if (string.IsNullOrEmpty(request.Name))
                return new ApiErrorResponse<string>(ConstantStrings.emptyNameFieldError);
            conditionFromDb.Name = request.Name;
            conditionFromDb.Description = request.Description;
            _db.Conditions.Update(conditionFromDb);
            await _db.SaveChangesAsync();
            return new ApiSuccessResponse<string>(ConstantStrings.editSuccessfully);
        }
    }
}
