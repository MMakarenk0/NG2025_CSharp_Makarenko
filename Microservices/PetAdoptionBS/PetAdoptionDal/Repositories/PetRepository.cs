using DAL_Core;
using DAL_Core.Entities;
using DAL_Core.Enums;
using Microsoft.EntityFrameworkCore;
using PetAdoptionDal.Repositories.Interfaces;

namespace PetAdoptionDal.Repositories;

public class PetRepository : Repository<Pet>, IPetRepository
{
    private readonly PetStoreDbContext _context;
    public PetRepository(PetStoreDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ICollection<Pet>> GetAllPetsWithDetailsAsync()
    {
        return await _context.Pets
            .Include(p => p.Store)
            .Include(p => p.Customer)
            .Include(p => p.HealthCares)
            .ToListAsync();
    }

    public async Task<Pet?> GetPetWithDetailsAsync(Guid id)
    {
        return await _context.Pets
            .Include(p => p.Store)
            .Include(p => p.Customer)
            .Include(p => p.HealthCares)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<ICollection<Pet>> GetAllPetsByStoreIdAsync(Guid StoreId)
    {
        return await _context.Pets
            .Where(p => p.StoreId == StoreId)
            .ToListAsync();
    }

    public async Task<ICollection<Pet>> GetPetsByType(PetTypes type)
    {
        return await _context.Pets
            .Where(p => p.Type == type)
            .ToListAsync();
    }

    public async Task<ICollection<Pet>> GetManyPetsByIdsAsync(ICollection<Guid> ids)
    {
        return await _context.Pets
            .Where(p => ids.Contains(p.Id))
            .ToListAsync();
    }
}

