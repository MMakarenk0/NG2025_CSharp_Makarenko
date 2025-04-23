using DAL_Core.Enums;
using SentinelBusinessLayer.Clients;
using SentinelBusinessLayer.Models;
using SentinelBusinessLayer.Service.Interface;

namespace SentinelBusinessLayer.Service;

public class PetService : IPetService
{
    private readonly IPetClient _petClient;
    public PetService(IPetClient petClient)
    {
        _petClient = petClient;
    }
    public async Task<IEnumerable<PetDto>> GetAllPetsAsync()
    {
        return await _petClient.GetAllAsync();
    }
    public async Task<PetDto> GetPetByIdAsync(Guid id)
    {
        return await _petClient.GetByIdAsync(id);
    }
    public async Task<Guid> CreatePetAsync(CreatePetDto dto)
    {
        return await _petClient.CreateAsync(dto);
    }
    public async Task<Guid> UpdatePetAsync(CreatePetDto dto)
    {
        return await _petClient.UpdateAsync(dto);
    }
    public async Task DeletePetAsync(Guid id)
    {
        await _petClient.DeleteAsync(id);
    }
    public async Task AdoptPetAsync(AdoptPetDto dto)
    {
        await _petClient.AdoptAsync(dto);
    }
    public async Task<IEnumerable<PetDto>> GetPetsByTypeAsync(PetTypes type)
    {
        return await _petClient.GetByTypeAsync(type);
    }
    public async Task<IEnumerable<PetDto>> GetPetsByStoreIdAsync(Guid storeId)
    {
        return await _petClient.GetByStoreIdAsync(storeId);
    }
}

