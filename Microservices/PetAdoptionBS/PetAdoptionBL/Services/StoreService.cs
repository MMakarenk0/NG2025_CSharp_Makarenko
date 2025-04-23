using AutoMapper;
using DAL_Core.Entities;
using PetAdoptionBL.Models;
using PetAdoptionBL.Services.Interfaces;
using PetAdoptionDal.UoF;

namespace PetAdoptionBL.Services;

public class StoreService : IStoreService
{
    IMapper _mapper;
    IUnitOfWork _unitOfWork;
    ITreatmentService _treatmentService;

    public StoreService(IMapper mapper, IUnitOfWork unitOfWork, ITreatmentService treatmentService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _treatmentService = treatmentService;
    }

    public async Task<ICollection<StoreDto>> GetAllStoresAsync()
    {
        var storeRepository = _unitOfWork.StoreRepository;

        var stores = await storeRepository.GetAllAsync();
        return _mapper.Map<ICollection<StoreDto>>(stores);
    }

    public async Task<StoreDto?> GetStoreByIdAsync(Guid id)
    {
        var storeRepository = _unitOfWork.StoreRepository;

        var store = await storeRepository.GetStoreWithPetsAsync(id);
        return _mapper.Map<StoreDto>(store);
    }

    public async Task<Guid> CreateStoreAsync(CreateStoreDto storeDto)
    {
        var storeRepository = _unitOfWork.StoreRepository;

        var store = _mapper.Map<Store>(storeDto);

        if (storeDto.PetIds != null && storeDto.PetIds.Any())
        {
            var petRepository = _unitOfWork.PetRepository;
            var pets = await petRepository.GetManyPetsByIdsAsync(storeDto.PetIds);
            store.Pets = pets.ToList();
        }

        await storeRepository.CreateAsync(store);
        await _unitOfWork.SaveChangesAsync();
        return store.Id;
    }

    public async Task DeleteStoreAsync(Guid id)
    {
        var storeRepository = _unitOfWork.StoreRepository;

        await storeRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<ICollection<PetDto>> GetStorePetsAsync(Guid id)
    {
        var storeRepository = _unitOfWork.StoreRepository;

        var pets = await storeRepository.GetAllPetsByStoreIdAsync(id);

        return _mapper.Map<ICollection<PetDto>>(pets);
    }

    public async Task<ICollection<TreatmentDto>> GetStorePetsTreatmentsAsync(Guid storeId)
    {
        var storeRepository = _unitOfWork.StoreRepository;

        var pets = await storeRepository.GetAllPetsByStoreIdAsync(storeId);

        var allTreatmentIds = pets
            .SelectMany(p => p.HealthCares)
            .Select(hc => hc.Id)
            .Distinct()
            .ToList();

        if (!allTreatmentIds.Any())
            return new List<TreatmentDto>();

        var treatments = await _treatmentService.GetManyTreatmentsAsync(allTreatmentIds);

        return treatments.ToList();
    }


    public async Task<Guid> UpdateStoreAsync(CreateStoreDto storeDto)
    {
        var storeRepository = _unitOfWork.StoreRepository;
        var store = await storeRepository.GetStoreWithPetsAsync(storeDto.Id);

        if (store == null)
        {
            throw new Exception($"Store with Id {storeDto.Id} not found");
        }

        _mapper.Map(storeDto, store);

        await UpdateStorePetsAsync(storeDto, store);

        await _unitOfWork.SaveChangesAsync();
        return store.Id;
    }

    private async Task UpdateStorePetsAsync(CreateStoreDto storeDto, Store store)
    {
        if (storeDto.PetIds != null && storeDto.PetIds.Any())
        {
            var petRepository = _unitOfWork.PetRepository;

            var newPetIds = new HashSet<Guid>(storeDto.PetIds);
            var existingPetIds = store.Pets.Select(p => p.Id).ToList();

            var petsToRemove = store.Pets
                .Where(p => !newPetIds.Contains(p.Id))
                .ToList();

            foreach (var pet in petsToRemove)
            {
                store.Pets.Remove(pet);
                await petRepository.DeleteAsync(pet.Id);
            }

            var petIdsToAdd = newPetIds.Except(existingPetIds).ToList();
            if (petIdsToAdd.Any())
            {
                var petsToAdd = await petRepository.GetManyPetsByIdsAsync(petIdsToAdd);

                foreach (var pet in petsToAdd)
                {
                    store.Pets.Add(pet);
                }
            }
        }
    }
}

