using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGPro.Data.EF;
using TGPro.Data.Entities;
using TGPro.Service.Exceptions;
using TGPro.Service.ViewModel.Demands;

namespace TGPro.Service.Catalog.Demands
{
    public class DemandService : IDemandService
    {
        private readonly TGProDbContext _db;
        public DemandService(TGProDbContext db)
        {
            _db = db;
        }
        public async Task<int> Create(DemandRequest request)
        {
            if (request.Name == string.Empty) throw new TGProException("Name cannot be null");
            else
            {
                var demand = new Demand()
                {
                    Name = request.Name
                };
                _db.Demands.Add(demand);
                return await _db.SaveChangesAsync();
            }
        }

        public async Task<int> Delete(int demandId)
        {
            var demandFromDb = await _db.Demands.FindAsync(demandId);
            if (demandFromDb == null) throw new TGProException($"Cannot find any demand with Id: {demandId}");
            else
            {
                _db.Demands.Remove(demandFromDb);
                return await _db.SaveChangesAsync();
            }
        }

        public async Task<Demand> GetById(int demandId)
        {
            var demandFromDb = await _db.Demands.FindAsync(demandId);
            if (demandFromDb == null) throw new TGProException($"Cannot find any demand with Id: {demandId}");
            else
            {
                return demandFromDb;
            }
        }

        public async Task<List<Demand>> GetListDemand()
        {
            List<Demand> lstDemand = await _db.Demands.OrderBy(d => d.Name).ToListAsync();
            if (lstDemand.Count == 0) throw new TGProException("Cannot find any customer demand");
            else
            {
                return lstDemand;
            }
        }

        public async Task<int> Update(int demandId, DemandRequest request)
        {
            var demandFromDb = await _db.Demands.FindAsync(demandId);
            if (demandFromDb == null) throw new TGProException($"Cannot find any demand with Id: {demandId}");
            else
            {
                demandFromDb.Name = request.Name;
                _db.Demands.Update(demandFromDb);
                return await _db.SaveChangesAsync();
            }
        }
    }
}
