using AutoMapper;
using DAL_Core.Entities;
using PetAdoptionBL.Models;
using PetAdoptionBL.Services.Interfaces;
using PetAdoptionDal.UoF;

namespace PetAdoptionBL.Services;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;


    public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ICollection<CustomerDto>> GetAllCustomersAsync()
    {
        var customerRepository = _unitOfWork.CustomerRepository;

        var customers = await customerRepository.GetAllCustomersWithPetsAsync();
        return _mapper.Map<ICollection<CustomerDto>>(customers);
    }
    public async Task<CustomerDto?> GetCustomerByIdAsync(Guid id)
    {
        var customerRepository = _unitOfWork.CustomerRepository;

        var customer = await customerRepository.GetCustomerWithPetsAsync(id);
        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<Guid> CreateCustomerAsync(CreateCustomerDto customerDto)
    {
        var customerRepository = _unitOfWork.CustomerRepository;

        var customer = _mapper.Map<Customer>(customerDto);

        if (customerDto.PetIds != null && customerDto.PetIds.Any())
        {
            var petRepository = _unitOfWork.PetRepository;
            var pets = await petRepository.GetManyPetsByIdsAsync(customerDto.PetIds);
            customer.Pets = pets.ToList();
        }
        await customerRepository.CreateAsync(customer);
        await _unitOfWork.SaveChangesAsync();
        return customer.Id;
    }

    public async Task DeleteCustomerAsync(Guid id)
    {
        var customerRepository = _unitOfWork.CustomerRepository;
        await customerRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateCustomerAsync(CreateCustomerDto customerDto)
    {
        var customerRepository = _unitOfWork.CustomerRepository;
        var customer = await customerRepository.GetCustomerWithPetsAsync(customerDto.Id);

        if (customer == null)
        {
            throw new Exception($"Customer with Id {customerDto.Id} not found");
        }

        _mapper.Map(customerDto, customer);

        var petRepository = _unitOfWork.PetRepository;

        if (customerDto.PetIds != null)
        {
            var newPetIds = new HashSet<Guid>(customerDto.PetIds);
            var existingPetIds = customer.Pets.Select(p => p.Id).ToList();

            var petsToRemove = customer.Pets
                .Where(p => !newPetIds.Contains(p.Id))
                .ToList();

            foreach (var pet in petsToRemove)
            {
                customer.Pets.Remove(pet);
                pet.CustomerId = null;
            }

            var petIdsToAdd = newPetIds.Except(existingPetIds).ToList();
            if (petIdsToAdd.Any())
            {
                var petsToAdd = await petRepository.GetManyPetsByIdsAsync(petIdsToAdd);
                foreach (var pet in petsToAdd)
                {
                    customer.Pets.Add(pet);
                }
            }
        }

        await customerRepository.UpdateAsync(customer);
        await _unitOfWork.SaveChangesAsync();
    }

}

