using DAL_Core.Enums;
using PetAdoptionBL.Models;

namespace PetAdoptionBL.Services.Interfaces
{
    public interface IPetService
    {
        Task AdoptPetAsync(Guid petId, Guid customerId);
        Task<Guid> CreatePetAsync(CreatePetDto petDto);
        Task DeletePetAsync(Guid id);
        Task<ICollection<PetDto>> GetAllPetsAsync();
        Task<ICollection<PetDto>> GetAllPetsByStoreIdAsync(Guid storeId);
        Task<PetDto?> GetPetByIdAsync(Guid id);
        Task<ICollection<PetDto>> GetPetsByTypeAsync(PetTypes type);
        Task<Guid> UpdatePetAsync(CreatePetDto petDto);
    }
}