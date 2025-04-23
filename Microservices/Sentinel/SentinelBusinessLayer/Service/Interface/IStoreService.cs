using SentinelBusinessLayer.Models;

namespace SentinelBusinessLayer.Service.Interface
{
    public interface IStoreService
    {
        Task CreateStoreAsync(CreateStoreDto dto);
        Task DeleteStoreAsync(Guid id);
        Task<ICollection<StoreDto>> GetAllStoresAsync();
        Task<ICollection<PetDto>> GetPetsByStoreIdAsync(Guid id);
        Task<ICollection<TreatmentDto>> GetPetsTreatmentsByStoreIdAsync(Guid id);
        Task<StoreDto> GetStoreByIdAsync(Guid id);
        Task UpdateStoreAsync(CreateStoreDto dto);
    }
}