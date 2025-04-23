using TreatmentBL.Models;

namespace TreatmentBL.Services.Interfaces;

public interface IPetAdoptionService
{
    Task<Guid> CreatePet(CreatePetDto petDto);
    Task<List<PetDto>> GetAllPets();
    Task<PetDto> GetPetById(Guid id);
}