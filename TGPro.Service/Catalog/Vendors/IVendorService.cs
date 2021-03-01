using System.Collections.Generic;
using System.Threading.Tasks;
using TGPro.Data.Entities;
using TGPro.Service.ViewModel.Vendors;

namespace TGPro.Service.Catalog.Vendors
{
    public interface IVendorService
    {
        Task<int> Create(VendorRequest request);

        Task<int> Update(int vendorId, VendorRequest request);

        Task<int> Delete(int vendorId);

        Task<Vendor> GetById(int vendorId);

        Task<List<Vendor>> GetListVendor();
    }
}
