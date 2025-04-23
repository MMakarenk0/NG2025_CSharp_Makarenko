using Refit;
using TreatmentBL.Models;

namespace TreatmentBL.Clients;

public interface IPetAdoptionClient
{
    [Get("/api/Pet")]
    Task<List<PetDto>> GetAllPets();

    [Get("/api/Pet/{id}")]
    Task<PetDto> GetPetById(Guid id);

    [Post("/api/Pet")]
    Task<Guid> CreatePet([Body] CreatePetDto petDto);
}

