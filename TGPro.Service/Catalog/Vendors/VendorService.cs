using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGPro.Data.EF;
using TGPro.Data.Entities;
using TGPro.Service.Common;
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
        public async Task<ApiResponse<string>> Create(VendorRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return new ApiErrorResponse<string>(ConstantStrings.emptyNameFieldError);
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
            await _db.SaveChangesAsync();
            return new ApiSuccessResponse<string>(ConstantStrings.addSuccessfully);
        }
        public async Task<ApiResponse<string>> Delete(int vendorId)
        {
            var vendorFromDb = await _db.Vendors.FindAsync(vendorId);
            if (vendorFromDb == null)
                return new ApiErrorResponse<string>(ConstantStrings.FindByIdError(vendorId));
            _db.Vendors.Remove(vendorFromDb);
            await _db.SaveChangesAsync();
            return new ApiSuccessResponse<string>(ConstantStrings.deleteSuccessfully);
        }

        public async Task<ApiResponse<Vendor>> GetById(int vendorId)
        {
            var vendorFromDb = await _db.Vendors.FindAsync(vendorId);
            if (vendorFromDb == null)
                return new ApiErrorResponse<Vendor>(ConstantStrings.FindByIdError(vendorId));
            return new ApiSuccessResponse<Vendor>(vendorFromDb);
        }

        public async Task<ApiResponse<List<Vendor>>> GetListVendor()
        {
            List<Vendor> lstVendor = await _db.Vendors.OrderBy(v => v.Name).ToListAsync();
            if (lstVendor.Count == 0)
                return new ApiErrorResponse<List<Vendor>>(ConstantStrings.getAllError);
            return new ApiSuccessResponse<List<Vendor>>(lstVendor);
        }

        public async Task<ApiResponse<string>> Update(int vendorId, VendorRequest request)
        {
            var vendorFromDb = await _db.Vendors.FindAsync(vendorId);
            if (vendorFromDb == null)
                return new ApiErrorResponse<string>(ConstantStrings.FindByIdError(vendorId));
            if (string.IsNullOrEmpty(request.Name))
                return new ApiErrorResponse<string>(ConstantStrings.emptyNameFieldError);
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
            await _db.SaveChangesAsync();
            return new ApiSuccessResponse<string>(ConstantStrings.editSuccessfully);
        }
    }
}
