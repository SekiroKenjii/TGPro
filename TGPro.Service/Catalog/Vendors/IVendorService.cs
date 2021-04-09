using System.Collections.Generic;
using System.Threading.Tasks;
using TGPro.Data.Entities;
using TGPro.Service.Common;
using TGPro.Service.DTOs.Vendors;

namespace TGPro.Service.Catalog.Vendors
{
    public interface IVendorService
    {
        Task<ApiResponse<string>> Create(VendorRequest request);

        Task<ApiResponse<string>> Update(int vendorId, VendorRequest request);

        Task<ApiResponse<string>> Delete(int vendorId);

        Task<ApiResponse<Vendor>> GetById(int vendorId);

        Task<ApiResponse<List<Vendor>>> GetListVendor();
    }
}
