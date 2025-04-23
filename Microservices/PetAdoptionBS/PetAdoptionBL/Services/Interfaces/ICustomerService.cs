using PetAdoptionBL.Models;

namespace PetAdoptionBL.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<Guid> CreateCustomerAsync(CreateCustomerDto customerDto);
        Task DeleteCustomerAsync(Guid id);
        Task<ICollection<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto?> GetCustomerByIdAsync(Guid id);
        Task UpdateCustomerAsync(CreateCustomerDto customerDto);
    }
}