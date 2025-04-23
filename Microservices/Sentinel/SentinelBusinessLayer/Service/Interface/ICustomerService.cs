using SentinelBusinessLayer.Models;

namespace SentinelBusinessLayer.Service.Interface;

public interface ICustomerService
{
    Task<Guid> CreateCustomerAsync(CreateCustomerDto dto);
    Task DeleteCustomerAsync(Guid id);
    Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
    Task<CustomerDto> GetCustomerByIdAsync(Guid id);
    Task<Guid> UpdateCustomerAsync(CreateCustomerDto dto);
}