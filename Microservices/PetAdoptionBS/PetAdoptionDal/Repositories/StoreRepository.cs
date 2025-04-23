using DAL_Core;
using DAL_Core.Entities;
using Microsoft.EntityFrameworkCore;
using PetAdoptionDal.Repositories.Interfaces;

namespace PetAdoptionDal.Repositories;

public class StoreRepository : Repository<Store>, IStoreRepository
{
    private readonly PetStoreDbContext _context;
    public StoreRepository(PetStoreDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<ICollection<Store>> GetAllStoresWithPetsAsync()
    {
        return await _context.Stores
            .Include(s => s.Pets)
            .ToListAsync();
    }
    public async Task<Store?> GetStoreWithPetsAsync(Guid id)
    {
        return await _context.Stores
            .Include(s => s.Pets)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<ICollection<Pet>> GetAllPetsByStoreIdAsync(Guid storeId)
    {
        return await _context.Pets
            .Where(p => p.StoreId == storeId)
            .ToListAsync();
    }
}

