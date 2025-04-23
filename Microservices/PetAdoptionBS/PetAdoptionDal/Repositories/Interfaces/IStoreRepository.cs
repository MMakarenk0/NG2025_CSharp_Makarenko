using DAL_Core.Entities;

namespace PetAdoptionDal.Repositories.Interfaces;

public interface IStoreRepository : IRepository<Store>
{
    Task<ICollection<Pet>> GetAllPetsByStoreIdAsync(Guid storeId);
    Task<ICollection<Store>> GetAllStoresWithPetsAsync();
    Task<Store?> GetStoreWithPetsAsync(Guid id);
}

