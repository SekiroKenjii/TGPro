using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGPro.Data.EF;
using TGPro.Data.Entities;
using TGPro.Service.Exceptions;
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

        public async Task<int> Create(ConditionRequest request)
        {
            if (request.Name == string.Empty) throw new TGProException("Name cannot be null");
            else
            {
                var condition = new Condition()
                {
                    Name = request.Name,
                    Description = request.Description
                };
                _db.Conditions.Add(condition);
                return await _db.SaveChangesAsync();
            }
        }

        public async Task<int> Delete(int conditionId)
        {
            var conditionFromDb = await _db.Conditions.FindAsync(conditionId);
            if (conditionFromDb == null) throw new TGProException($"Cannot find any condition with Id: {conditionId}");
            else
            {
                _db.Conditions.Remove(conditionFromDb);
                return await _db.SaveChangesAsync();
            }
        }

        public async Task<Condition> GetById(int conditionId)
        {
            var conditionFromDb = await _db.Conditions.FindAsync(conditionId);
            if (conditionFromDb == null) throw new TGProException($"Cannot find any condition with Id: {conditionId}");
            else
            {
                return conditionFromDb;
            }
        }

        public async Task<List<Condition>> GetListCondition()
        {
            List<Condition> lstCondition = await _db.Conditions.OrderBy(c => c.Name).ToListAsync();
            if (lstCondition.Count == 0) throw new TGProException("Cannot find any condition");
            else
            {
                return lstCondition;
            }
        }

        public async Task<int> Update(int conditionId, ConditionRequest request)
        {
            var conditionFromDb = await _db.Conditions.FindAsync(conditionId);
            if (conditionFromDb == null) throw new TGProException($"Cannot find any condition with Id: {conditionId}");
            else
            {
                conditionFromDb.Name = request.Name;
                conditionFromDb.Description = request.Description;
                _db.Conditions.Update(conditionFromDb);
                return await _db.SaveChangesAsync();
            }
        }
    }
}
