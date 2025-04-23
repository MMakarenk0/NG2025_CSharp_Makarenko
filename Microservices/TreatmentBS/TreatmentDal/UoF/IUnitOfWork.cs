using TreatmentDal.Repositories.Interfaces;

namespace TreatmentDal.UoF;

public interface IUnitOfWork
{
    IHealthCareRepository HealthCareRepository { get; }
    IVendorRepository VendorRepository { get; }

    Task SaveChangesAsync();
}