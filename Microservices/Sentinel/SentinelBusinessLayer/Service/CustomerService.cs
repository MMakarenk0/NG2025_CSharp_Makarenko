using SentinelBusinessLayer.Clients;
using SentinelBusinessLayer.Models;
using SentinelBusinessLayer.Service.Interface;

namespace SentinelBusinessLayer.Service;

public class CustomerService : ICustomerService
{
    private readonly ICustomerClient _customerClient;
    public CustomerService(ICustomerClient customerClient)
    {
        _customerClient = customerClient;
    }
    public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
    {
        return await _customerClient.GetAllAsync();
    }
    public async Task<CustomerDto> GetCustomerByIdAsync(Guid id)
    {
        return await _customerClient.GetByIdAsync(id);
    }
    public async Task<Guid> CreateCustomerAsync(CreateCustomerDto dto)
    {
        return await _customerClient.CreateAsync(dto);
    }
    public async Task<Guid> UpdateCustomerAsync(CreateCustomerDto dto)
    {
        return await _customerClient.UpdateAsync(dto);
    }
    public async Task DeleteCustomerAsync(Guid id)
    {
        await _customerClient.DeleteAsync(id);
    }
}

