using DAL_Core.Entities;

namespace TreatmentDal.Repositories.Interfaces;
public interface IHealthCareRepository : IRepository<HealthCare>
{
    Task<ICollection<HealthCare>> GetExpiringHealthCares();
    Task<ICollection<HealthCare>> GetHealhtCaresByPet(Guid petId);
    Task<ICollection<HealthCare>> GetHealthCaresByVendor(Guid vendorId);
    Task<HealthCare?> GetHealthCareWithDetails(Guid id);
    Task<ICollection<HealthCare>> GetManyHealhCaresByIds(IEnumerable<Guid> ids);
}
