using DAL_Core;
using DAL_Core.Entities;
using Microsoft.EntityFrameworkCore;
using TreatmentDal.Repositories.Interfaces;

namespace TreatmentDal.Repositories;
public class HealthCareRepository : Repository<HealthCare>, IHealthCareRepository
{
    private readonly PetStoreDbContext _context;

    public HealthCareRepository(PetStoreDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<HealthCare?> GetHealthCareWithDetails(Guid id)
    {
        var treatment = await _context.HealthCares
            .Include(x => x.Vendor)
            .Include(x => x.Pet)
            .FirstOrDefaultAsync(x => x.Id == id);

        return treatment;
    }

    public async Task<ICollection<HealthCare>> GetHealhtCaresByPet(Guid petId)
    {
        var healthCares = await _context.HealthCares
            .Where(x => x.PetId == petId)
            .ToListAsync();

        return healthCares;
    }

    public async Task<ICollection<HealthCare>> GetHealthCaresByVendor(Guid vendorId)
    {
        var healthCares = await _context.HealthCares
            .Where(x => x.VendorId == vendorId)
            .ToListAsync();

        return healthCares;
    }

    public async Task<ICollection<HealthCare>> GetExpiringHealthCares()
    {
        var healthCares = await _context.HealthCares
            .Where(x => x.ExpirationDate < DateTime.UtcNow.AddDays(7))
            .ToListAsync();
        return healthCares;
    }

    public async Task<ICollection<HealthCare>> GetManyHealhCaresByIds(IEnumerable<Guid> ids)
    {
        var healthCares = await _context.HealthCares
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
        return healthCares;
    }
}