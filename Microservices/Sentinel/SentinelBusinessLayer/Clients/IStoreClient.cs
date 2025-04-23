using Refit;
using SentinelBusinessLayer.Models;

namespace SentinelBusinessLayer.Clients;

public interface IStoreClient
{
    [Get("/api/Store")]
    Task<List<StoreDto>> GetAllAsync();

    [Get("/api/Store/{id}")]
    Task<StoreDto> GetByIdAsync(Guid id);

    [Post("/api/Store")]
    Task<Guid> CreateAsync([Body] CreateStoreDto dto);

    [Put("/api/Store")]
    Task<Guid> UpdateAsync([Body] CreateStoreDto dto);

    [Delete("/api/Store/{id}")]
    Task DeleteAsync(Guid id);

    [Get("/api/Store/{id}/pets")]
    Task<List<PetDto>> GetPetsAsync(Guid id);

    [Get("/api/Store/{id}/pets/treatments")]
    Task<List<TreatmentDto>> GetPetsTreatmentsAsync(Guid id);
}

