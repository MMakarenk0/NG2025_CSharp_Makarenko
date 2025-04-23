using PetAdoptionDal.Repositories.Interfaces;

namespace PetAdoptionDal.UoF
{
    public interface IUnitOfWork
    {
        IPetRepository PetRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IStoreRepository StoreRepository { get; }

        Task SaveChangesAsync();
    }
}