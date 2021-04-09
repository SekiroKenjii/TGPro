using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGPro.Data.EF;
using TGPro.Data.Entities;
using TGPro.Service.Common;
using TGPro.Service.DTOs.Demands;

namespace TGPro.Service.Catalog.Demands
{
    public class DemandService : IDemandService
    {
        private readonly TGProDbContext _db;
        public DemandService(TGProDbContext db)
        {
            _db = db;
        }
        public async Task<ApiResponse<string>> Create(DemandRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return new ApiErrorResponse<string>(ConstantStrings.emptyNameFieldError);
            var demand = new Demand()
            {
                Name = request.Name
            };
            _db.Demands.Add(demand);
            await _db.SaveChangesAsync();
            return new ApiSuccessResponse<string>(ConstantStrings.addSuccessfully);
        }

        public async Task<ApiResponse<string>> Delete(int demandId)
        {
            var demandFromDb = await _db.Demands.FindAsync(demandId);
            if (demandFromDb == null)
                return new ApiErrorResponse<string>(ConstantStrings.FindByIdError(demandId));
            _db.Demands.Remove(demandFromDb);
            await _db.SaveChangesAsync();
            return new ApiSuccessResponse<string>(ConstantStrings.deleteSuccessfully);
        }

        public async Task<ApiResponse<Demand>> GetById(int demandId)
        {
            var demandFromDb = await _db.Demands.FindAsync(demandId);
            if (demandFromDb == null)
                return new ApiErrorResponse<Demand>(ConstantStrings.FindByIdError(demandId));
            return new ApiSuccessResponse<Demand>(demandFromDb);
        }

        public async Task<ApiResponse<List<Demand>>> GetListDemand()
        {
            List<Demand> lstDemand = await _db.Demands.OrderBy(d => d.Name).ToListAsync();
            if (lstDemand.Count == 0)
                return new ApiErrorResponse<List<Demand>>(ConstantStrings.getAllError);
            return new ApiSuccessResponse<List<Demand>>(lstDemand);
        }

        public async Task<ApiResponse<string>> Update(int demandId, DemandRequest request)
        {
            var demandFromDb = await _db.Demands.FindAsync(demandId);
            if (demandFromDb == null)
                return new ApiErrorResponse<string>(ConstantStrings.FindByIdError(demandId));
            if (string.IsNullOrEmpty(request.Name))
                return new ApiErrorResponse<string>(ConstantStrings.emptyNameFieldError);
            demandFromDb.Name = request.Name;
            _db.Demands.Update(demandFromDb);
            await _db.SaveChangesAsync();
            return new ApiSuccessResponse<string>(ConstantStrings.editSuccessfully);
        }
    }
}
