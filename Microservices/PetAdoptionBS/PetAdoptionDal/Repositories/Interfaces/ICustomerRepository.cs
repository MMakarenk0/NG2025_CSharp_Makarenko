using DAL_Core.Entities;

namespace PetAdoptionDal.Repositories.Interfaces;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<ICollection<Customer>> GetAllCustomersWithPetsAsync();
    Task<Customer?> GetCustomerWithPetsAsync(Guid id);
}

