using TreatmentBL.Clients;
using TreatmentBL.Models;
using TreatmentBL.Services.Interfaces;

namespace TreatmentBL.Services;

public class PetAdoptionService : IPetAdoptionService
{
    private readonly IPetAdoptionClient _petAdoptionClient;

    public PetAdoptionService(IPetAdoptionClient petAdoptionClient)
    {
        _petAdoptionClient = petAdoptionClient;
    }

    public async Task<List<PetDto>> GetAllPets()
    {
        var pets = await _petAdoptionClient.GetAllPets();
        return pets;
    }

    public async Task<PetDto> GetPetById(Guid id)
    {
        var pet = await _petAdoptionClient.GetPetById(id);
        return pet;
    }

    public async Task<Guid> CreatePet(CreatePetDto petDto)
    {
        var petId = await _petAdoptionClient.CreatePet(petDto);
        return petId;
    }
}

