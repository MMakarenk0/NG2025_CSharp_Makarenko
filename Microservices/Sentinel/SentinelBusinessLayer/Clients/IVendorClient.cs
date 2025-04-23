using Refit;
using SentinelBusinessLayer.Models;

namespace SentinelBusinessLayer.Clients;

public interface IVendorClient
{
    [Get("/api/Vendor")]
    Task<List<VendorDto>> GetAllVendorsAsync();

    [Get("/api/Vendor/{id}")]
    Task<VendorDto> GetVendorByIdAsync(Guid id);

    [Post("/api/Vendor")]
    Task CreateVendorAsync([Body] CreateVendorDto dto);

    [Put("/api/Vendor")]
    Task UpdateVendorAsync([Body] CreateVendorDto dto);

    [Delete("/api/Vendor/{id}")]
    Task DeleteVendorAsync(Guid id);

    [Get("/api/Vendor/many")]
    Task<List<VendorDto>> GetVendorsByIdsAsync([Query(CollectionFormat.Multi)] IEnumerable<Guid> ids);

    [Get("/api/Vendor/contractType/{type}")]
    Task<List<VendorDto>> GetVendorsByContractTypeAsync(string type);

    [Get("/api/Vendor/{vendorId}/healthcares")]
    Task<List<TreatmentDto>> GetHealthCaresByVendorAsync(Guid vendorId);
}

