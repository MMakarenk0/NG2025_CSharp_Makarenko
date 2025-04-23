using DAL_Core.Enums;
using TreatmentBL.Models;

namespace TreatmentBL.Services.Interfaces
{
    public interface IVendorService
    {
        Task<Guid> CreateVendor(CreateVendorDto vendorDto);
        Task DeleteVendor(Guid id);
        Task<List<VendorDto>> GetAllVendors();
        Task<List<VendorDto>> GetManyVendorsByIds(IEnumerable<Guid> ids);
        Task<VendorDto> GetVendorById(Guid id);
        Task<List<TreatmentDto>> GetVendorHealthCares(Guid vendorId);
        Task<List<VendorDto>> GetVendorsByContractType(ContractType type);
        Task<Guid> UpdateVendor(CreateVendorDto vendorDto);
    }
}