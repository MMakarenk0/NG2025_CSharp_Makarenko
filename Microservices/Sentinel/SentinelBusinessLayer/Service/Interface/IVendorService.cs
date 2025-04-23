using SentinelBusinessLayer.Models;

namespace SentinelBusinessLayer.Service.Interface
{
    public interface IVendorService
    {
        Task CreateVendor(CreateVendorDto vendorDto);
        Task DeleteVendor(Guid id);
        Task<List<VendorDto>> GetAllVendors();
        Task<VendorDto> GetVendor(Guid id);
        Task<List<VendorDto>> GetVendorsByContractType(string type);
        Task<List<VendorDto>> GetVendorsByIds(IEnumerable<Guid> ids);
        Task UpdateVendor(CreateVendorDto vendorDto);
    }
}