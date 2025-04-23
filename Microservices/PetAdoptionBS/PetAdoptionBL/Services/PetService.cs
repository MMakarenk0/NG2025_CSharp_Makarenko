using AutoMapper;
using DAL_Core.Entities;
using DAL_Core.Enums;
using PetAdoptionBL.Models;
using PetAdoptionBL.Services.Interfaces;
using PetAdoptionDal.UoF;

namespace PetAdoptionBL.Services;

public class PetService : IPetService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITreatmentService _treatmentService;

    public PetService(IMapper mapper, IUnitOfWork unitOfWork, ITreatmentService treatmentService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _treatmentService = treatmentService;
    }

    public async Task<ICollection<PetDto>> GetAllPetsAsync()
    {
        var petRepository = _unitOfWork.PetRepository;

        var pets = await petRepository.GetAllPetsWithDetailsAsync();

        return _mapper.Map<ICollection<PetDto>>(pets);
    }
    
    public async Task<PetDto?> GetPetByIdAsync(Guid id)
    {
        var petRepository = _unitOfWork.PetRepository;

        var pet = await petRepository.GetPetWithDetailsAsync(id);

        return _mapper.Map<PetDto>(pet);
    }

    public async Task<Guid> CreatePetAsync(CreatePetDto petDto)
    {
        var petRepository = _unitOfWork.PetRepository;

        var pet = _mapper.Map<Pet>(petDto);

        await CheckStoreAsync(petDto, pet);
        await CheckCustomerAsync(petDto, pet);
        await CheckTreatmentsAsync(petDto, pet);

        await petRepository.CreateAsync(pet);

        await _unitOfWork.SaveChangesAsync();

        return pet.Id;
    }

    public async Task DeletePetAsync(Guid id)
    {
        var petRepository = _unitOfWork.PetRepository;

        await petRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<ICollection<PetDto>> GetAllPetsByStoreIdAsync(Guid storeId)
    {
        var petRepository = _unitOfWork.PetRepository;
        var pets = await petRepository.GetAllPetsByStoreIdAsync(storeId);
        return _mapper.Map<ICollection<PetDto>>(pets);
    }

    public async Task<ICollection<PetDto>> GetPetsByTypeAsync(PetTypes type)
    {
        var petRepository = _unitOfWork.PetRepository;

        var pets = await petRepository.GetPetsByType(type);

        return _mapper.Map<ICollection<PetDto>>(pets);
    }

    public async Task AdoptPetAsync(Guid petId, Guid customerId)
    {
        var petRepository = _unitOfWork.PetRepository;
        var customerRepository = _unitOfWork.CustomerRepository;

        var pet = await petRepository.GetAsync(petId);
        if (pet == null)
        {
            throw new Exception($"Pet with Id {petId} not found");
        }
        var customer = await customerRepository.GetAsync(customerId);
        if (customer == null)
        {
            throw new Exception($"Customer with Id {customerId} not found");
        }
        pet.CustomerId = customerId;
        customer.Pets.Add(pet);

        await petRepository.UpdateAsync(pet);
        await customerRepository.UpdateAsync(customer);

        await _unitOfWork.SaveChangesAsync();
    }
    public async Task<Guid> UpdatePetAsync(CreatePetDto petDto)
    {
        var petRepository = _unitOfWork.PetRepository;
        var pet = await petRepository.GetPetWithDetailsAsync(petDto.Id);
        if (pet == null)
        {
            throw new Exception($"Pet with {petDto.Id}");
        }

        _mapper.Map(petDto, pet);

        await CheckStoreAsync(petDto, pet);
        await CheckCustomerAsync(petDto, pet);
        await UpdateTreatmentsAsync(petDto, pet);

        await petRepository.UpdateAsync(pet);
        await _unitOfWork.SaveChangesAsync();
        return pet.Id;
    }

    private async Task UpdateTreatmentsAsync(CreatePetDto petDto, Pet pet)
    {
        if (petDto.TreatmentIds != null && petDto.TreatmentIds.Any())
        {
            var newTreatmentIds = new HashSet<Guid>(petDto.TreatmentIds);
            var existingTreatmentIds = pet.HealthCares.Select(t => t.Id).ToList();

            var treatmentsToRemove = pet.HealthCares
                .Where(t => !newTreatmentIds.Contains(t.Id))
                .ToList();

            foreach (var treatment in treatmentsToRemove)
            {
                pet.HealthCares.Remove(treatment);
                await _treatmentService.DeleteTreatment(treatment.Id);
            }

            var treatmentIdsToAdd = newTreatmentIds.Except(existingTreatmentIds).ToList();
            if (treatmentIdsToAdd.Any())
            {
                var treatmentsToAdd = await _treatmentService.GetManyTreatmentsAsync(treatmentIdsToAdd);

                foreach (var treatment in treatmentsToAdd)
                {
                    var healthCare = new HealthCare
                    {
                        Id = treatment.Id,
                        TreatmentName = treatment.Name,
                        InjectedAt = treatment.InjectedAt,
                        ExpirationDate = treatment.ExpirationDate,
                        PetId = pet.Id,
                        VendorId = treatment.VendorId,
                    };

                    pet.HealthCares.Add(healthCare);
                }
            }
        }
    }

    private async Task CheckTreatmentsAsync(CreatePetDto petDto, Pet pet)
    {
        if (petDto.TreatmentIds != null && petDto.TreatmentIds.Any())
        {
            var treatmentsToAdd = await _treatmentService.GetManyTreatmentsAsync(petDto.TreatmentIds);

            pet.HealthCares = _mapper.Map<ICollection<HealthCare>>(treatmentsToAdd);
        }
    }

    private async Task CheckCustomerAsync(CreatePetDto petDto, Pet pet)
    {
        if (petDto.CustomerId != null)
        {
            var customerRepository = _unitOfWork.CustomerRepository;
            var customer = await customerRepository.GetAsync(petDto.CustomerId.Value);
            if (customer == null)
            {
                throw new Exception($"Customer with {petDto.CustomerId} not found");
            }

            pet.Customer = customer;
        }
    }
    private async Task CheckStoreAsync(CreatePetDto petDto, Pet pet)
    {
        var storeRepository = _unitOfWork.StoreRepository;

        var store = await storeRepository.GetAsync(petDto.StoreId);

        if (store == null)
        {
            throw new Exception($"Store with {petDto.StoreId} not found");
        }

        pet.Store = store;
    }
}

