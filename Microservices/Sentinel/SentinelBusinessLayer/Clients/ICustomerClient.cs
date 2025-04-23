using Refit;
using SentinelBusinessLayer.Models;

namespace SentinelBusinessLayer.Clients;

public interface ICustomerClient
{
    [Get("/api/Customer")]
    Task<List<CustomerDto>> GetAllAsync();

    [Get("/api/Customer/{id}")]
    Task<CustomerDto> GetByIdAsync(Guid id);

    [Post("/api/Customer")]
    Task<Guid> CreateAsync([Body] CreateCustomerDto dto);

    [Put("/api/Customer")]
    Task<Guid> UpdateAsync([Body] CreateCustomerDto dto);

    [Delete("/api/Customer/{id}")]
    Task DeleteAsync(Guid id);
}
