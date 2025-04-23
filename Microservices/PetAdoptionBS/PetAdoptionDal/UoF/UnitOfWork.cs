using DAL_Core;
using PetAdoptionDal.Repositories.Interfaces;

namespace PetAdoptionDal.UoF;

public class UnitOfWork : IUnitOfWork
{
    private readonly PetStoreDbContext _context;

    public IPetRepository PetRepository { get; }
    public ICustomerRepository CustomerRepository { get; }
    public IStoreRepository StoreRepository { get; }
    public UnitOfWork(
        PetStoreDbContext context,
        IPetRepository petRepository,
        ICustomerRepository customerRepository,
        IStoreRepository storeRepository)
    {
        _context = context;
        PetRepository = petRepository;
        CustomerRepository = customerRepository;
        StoreRepository = storeRepository;
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

