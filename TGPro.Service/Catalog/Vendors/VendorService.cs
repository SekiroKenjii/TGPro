using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGPro.Data.EF;
using TGPro.Data.Entities;
using TGPro.Service.Exceptions;
using TGPro.Service.SystemResources;
using TGPro.Service.ViewModel.Vendors;

namespace TGPro.Service.Catalog.Vendors
{
    public class VendorService : IVendorService
    {
        private TGProDbContext _db;
        public VendorService(TGProDbContext db)
        {
            _db = db;
        }
        public async Task<int> Create(VendorRequest request)
        {
            if (request.Name == string.Empty) return ConstantStrings.TGBadRequest;
            var vendor = new Vendor()
            {
                Name = request.Name,
                ContactName = request.ContactName,
                ContactTitle = request.ContactTitle,
                Address = request.Address,
                City = request.City,
                Country = request.Country,
                PhoneNumber = request.PhoneNumber,
                HomePage = request.HomePage,
                Status = request.Status
            };
            _db.Vendors.Add(vendor);
            return await _db.SaveChangesAsync();
        }
        public async Task<int> Delete(int vendorId)
        {
            var vendorFromDb = await _db.Vendors.FindAsync(vendorId);
            if (vendorFromDb == null) return ConstantStrings.TGNotFound;
            _db.Vendors.Remove(vendorFromDb);
            return await _db.SaveChangesAsync();
        }

        public async Task<Vendor> GetById(int vendorId)
        {
            var vendorFromDb = await _db.Vendors.FindAsync(vendorId);
            if (vendorFromDb == null) return null;
            return vendorFromDb;
        }

        public async Task<List<Vendor>> GetListVendor()
        {
            List<Vendor> lstVendor = await _db.Vendors.OrderBy(v => v.Name).ToListAsync();
            if (lstVendor.Count == 0) return null;
            return lstVendor;
        }

        public async Task<int> Update(int vendorId, VendorRequest request)
        {
            var vendorFromDb = await _db.Vendors.FindAsync(vendorId);
            if (vendorFromDb == null) return ConstantStrings.TGNotFound;
            if (request.Name == null) return ConstantStrings.TGBadRequest;
            vendorFromDb.Name = request.Name;
            vendorFromDb.ContactName = request.ContactName;
            vendorFromDb.ContactTitle = request.ContactTitle;
            vendorFromDb.Address = request.Address;
            vendorFromDb.City = request.City;
            vendorFromDb.Country = request.Country;
            vendorFromDb.PhoneNumber = request.PhoneNumber;
            vendorFromDb.HomePage = request.HomePage;
            vendorFromDb.Status = request.Status;
            _db.Vendors.Update(vendorFromDb);
            return await _db.SaveChangesAsync();
        }
    }
}
