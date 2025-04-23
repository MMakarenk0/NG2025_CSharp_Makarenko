using DAL_Core;
using DAL_Core.Entities;
using Microsoft.EntityFrameworkCore;
using PetAdoptionDal.Repositories.Interfaces;

namespace PetAdoptionDal.Repositories;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    private readonly PetStoreDbContext _context;
    public CustomerRepository(PetStoreDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<ICollection<Customer>> GetAllCustomersWithPetsAsync()
    {
        return await _context.Customers
            .Include(c => c.Pets)
            .ToListAsync();
    }
    public async Task<Customer?> GetCustomerWithPetsAsync(Guid id)
    {
        return await _context.Customers
            .Include(c => c.Pets)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}

