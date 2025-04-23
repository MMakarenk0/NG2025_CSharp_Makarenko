using DAL_Core.Entities;
using DAL_Core.Enums;

namespace PetAdoptionDal.Repositories.Interfaces;

public interface IPetRepository : IRepository<Pet>
{
    Task<ICollection<Pet>> GetAllPetsByStoreIdAsync(Guid StoreId);
    Task<ICollection<Pet>> GetAllPetsWithDetailsAsync();
    Task<ICollection<Pet>> GetManyPetsByIdsAsync(ICollection<Guid> ids);
    Task<ICollection<Pet>> GetPetsByType(PetTypes type);
    Task<Pet?> GetPetWithDetailsAsync(Guid id);
}

