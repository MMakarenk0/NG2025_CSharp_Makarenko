using DAL_Core.Entities;
using DAL_Core.Enums;

namespace TreatmentDal.Repositories.Interfaces;
public interface IVendorRepository : IRepository<Vendor>
{
    Task<ICollection<HealthCare>> GetVendorHealthCares(Guid vendorId);
    Task<ICollection<Vendor>> GetVendorsByContractType(ContractType type);
    Task<ICollection<Vendor>> GetVendorsByIds(IEnumerable<Guid> ids);
}
