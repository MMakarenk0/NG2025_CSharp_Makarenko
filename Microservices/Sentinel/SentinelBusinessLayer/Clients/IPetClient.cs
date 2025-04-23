using DAL_Core.Enums;
using Refit;
using SentinelBusinessLayer.Models;

namespace SentinelBusinessLayer.Clients;

public interface IPetClient
{
    [Get("/api/Pet")]
    Task<List<PetDto>> GetAllAsync();

    [Get("/api/Pet/{id}")]
    Task<PetDto> GetByIdAsync(Guid id);

    [Multipart]
    [Post("/api/Pet")]
    Task<Guid> CreateAsync(CreatePetDto petDto);

    [Multipart]
    [Put("/api/Pet")]
    Task<Guid> UpdateAsync(CreatePetDto petDto);

    [Delete("/api/Pet/{id}")]
    Task DeleteAsync(Guid id);

    [Multipart]
    [Post("/api/Pet/adopt")]
    Task AdoptAsync(AdoptPetDto adoptPetDto);

    [Get("/api/Pet/type/{type}")]
    Task<List<PetDto>> GetByTypeAsync(PetTypes type);

    [Get("/api/Pet/store/{storeId}")]
    Task<List<PetDto>> GetByStoreIdAsync(Guid storeId);
}

