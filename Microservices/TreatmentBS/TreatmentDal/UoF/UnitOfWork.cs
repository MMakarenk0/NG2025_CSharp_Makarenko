using DAL_Core;
using TreatmentDal.Repositories.Interfaces;

namespace TreatmentDal.UoF;

public class UnitOfWork : IUnitOfWork
{
    private readonly PetStoreDbContext _context;

    public IHealthCareRepository HealthCareRepository { get; }
    public IVendorRepository VendorRepository { get; }

    public UnitOfWork(PetStoreDbContext context, IHealthCareRepository healthCareRepository, IVendorRepository vendorRepository)
    {
        _context = context;
        HealthCareRepository = healthCareRepository;
        VendorRepository = vendorRepository;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

