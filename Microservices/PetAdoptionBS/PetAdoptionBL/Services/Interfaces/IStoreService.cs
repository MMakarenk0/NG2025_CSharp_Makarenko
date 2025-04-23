using PetAdoptionBL.Models;

namespace PetAdoptionBL.Services.Interfaces
{
    public interface IStoreService
    {
        Task<Guid> CreateStoreAsync(CreateStoreDto storeDto);
        Task DeleteStoreAsync(Guid id);
        Task<ICollection<StoreDto>> GetAllStoresAsync();
        Task<StoreDto?> GetStoreByIdAsync(Guid id);
        Task<ICollection<PetDto>> GetStorePetsAsync(Guid id);
        Task<ICollection<TreatmentDto>> GetStorePetsTreatmentsAsync(Guid storeId);
        Task<Guid> UpdateStoreAsync(CreateStoreDto storeDto);
    }
}