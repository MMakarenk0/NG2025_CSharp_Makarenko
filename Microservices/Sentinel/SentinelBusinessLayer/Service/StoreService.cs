using SentinelBusinessLayer.Clients;
using SentinelBusinessLayer.Models;
using SentinelBusinessLayer.Service.Interface;

namespace SentinelBusinessLayer.Service;

public class StoreService : IStoreService
{
    private readonly IStoreClient _storeClient;
    public StoreService(IStoreClient storeClient)
    {
        _storeClient = storeClient;
    }
    public async Task<ICollection<StoreDto>> GetAllStoresAsync()
    {
        return await _storeClient.GetAllAsync();
    }
    public async Task<StoreDto> GetStoreByIdAsync(Guid id)
    {
        return await _storeClient.GetByIdAsync(id);
    }
    public async Task CreateStoreAsync(CreateStoreDto dto)
    {
        await _storeClient.CreateAsync(dto);
    }
    public async Task UpdateStoreAsync(CreateStoreDto dto)
    {
        await _storeClient.UpdateAsync(dto);
    }
    public async Task DeleteStoreAsync(Guid id)
    {
        await _storeClient.DeleteAsync(id);
    }
    public async Task<ICollection<PetDto>> GetPetsByStoreIdAsync(Guid id)
    {
        return await _storeClient.GetPetsAsync(id);
    }
    public async Task<ICollection<TreatmentDto>> GetPetsTreatmentsByStoreIdAsync(Guid id)
    {
        return await _storeClient.GetPetsTreatmentsAsync(id);
    }
}

