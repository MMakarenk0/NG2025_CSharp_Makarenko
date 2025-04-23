using DAL_Core.Enums;
using SentinelBusinessLayer.Models;

namespace SentinelBusinessLayer.Service.Interface;

public interface IPetService
{
    Task AdoptPetAsync(AdoptPetDto dto);
    Task<Guid> CreatePetAsync(CreatePetDto dto);
    Task DeletePetAsync(Guid id);
    Task<IEnumerable<PetDto>> GetAllPetsAsync();
    Task<PetDto> GetPetByIdAsync(Guid id);
    Task<IEnumerable<PetDto>> GetPetsByStoreIdAsync(Guid storeId);
    Task<IEnumerable<PetDto>> GetPetsByTypeAsync(PetTypes type);
    Task<Guid> UpdatePetAsync(CreatePetDto dto);
}