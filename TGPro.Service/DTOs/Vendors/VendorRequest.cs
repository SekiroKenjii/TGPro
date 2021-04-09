using TGPro.Data.Enums;

namespace TGPro.Service.DTOs.Vendors
{
    public class VendorRequest
    {
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string HomePage { get; set; }
        public Status Status { get; set; }

    }
}
