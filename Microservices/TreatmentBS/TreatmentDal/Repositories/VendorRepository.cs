using DAL_Core;
using DAL_Core.Entities;
using DAL_Core.Enums;
using Microsoft.EntityFrameworkCore;
using TreatmentDal.Repositories.Interfaces;

namespace TreatmentDal.Repositories;
public class VendorRepository : Repository<Vendor>, IVendorRepository
{
    private readonly PetStoreDbContext _context;

    public VendorRepository(PetStoreDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ICollection<Vendor>> GetVendorsByContractType(ContractType type)
    {
        var vendors = await _context.Vendors
            .Where(x => x.ContractType == type)
            .ToListAsync();
        return vendors;
    }

    public async Task<ICollection<HealthCare>> GetVendorHealthCares(Guid vendorId)
    {
        var healthCares = await _context.HealthCares
            .Where(x => x.VendorId == vendorId)
            .ToListAsync();
        return healthCares;
    }

    public async Task<ICollection<Vendor>> GetVendorsByIds(IEnumerable<Guid> ids)
    {
        var vendors = await _context.Vendors
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
        return vendors;
    }
}
