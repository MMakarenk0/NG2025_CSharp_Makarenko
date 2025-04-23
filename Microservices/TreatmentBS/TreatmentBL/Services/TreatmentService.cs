using AutoMapper;
using DAL_Core.Entities;
using TreatmentBL.Models;
using TreatmentBL.Services.Interfaces;
using TreatmentDal.UoF;

namespace TreatmentBL.Services;
public class TreatmentService : ITreatmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPetAdoptionService _petAdoptionService;

    public TreatmentService(IUnitOfWork unitOfWork, IMapper mapper, IPetAdoptionService petAdoptionService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _petAdoptionService = petAdoptionService;
    }

    public async Task<bool> GetTreatmentStatus(Guid id)
    {
        var _healthCareRepository = _unitOfWork.HealthCareRepository;

        var healthCare = await _healthCareRepository.GetHealthCareWithDetails(id);

        if (healthCare == null)
        {
            throw new Exception($"TE: Treatment with ID={id} not found");
        }

        var treatment = _mapper.Map<TreatmentDto>(healthCare);

        return treatment.IsExpired;
    }

    public async Task<TreatmentDto> GetTreatment(Guid id)
    {
        var _healthCareRepository = _unitOfWork.HealthCareRepository;

        var healthCare = await _healthCareRepository.GetHealthCareWithDetails(id);

        if (healthCare == null)
        {
            throw new Exception($"TE: Treatment with ID={id} not found");
        }

        return _mapper.Map<TreatmentDto>(healthCare);
    }

    public async Task<List<TreatmentDto>> GetAllTreatments()
    {
        var _healthCareRepository = _unitOfWork.HealthCareRepository;

        var healthCare = await _healthCareRepository.GetAllAsync();

        return _mapper.Map<List<TreatmentDto>>(healthCare);
    }

    public async Task<List<TreatmentDto>> GetManyTreatmentByIds(IEnumerable<Guid> ids)
    {
        var _healthCareRepository = _unitOfWork.HealthCareRepository;

        var healthCares = await _healthCareRepository.GetManyHealhCaresByIds(ids);

        return _mapper.Map<List<TreatmentDto>>(healthCares);
    }

    public async Task<Guid> CreateTreatment(CreateTreatmentDto treatmentDto)
    {
        var _healthCareRepository = _unitOfWork.HealthCareRepository;

        var healthCare = await _healthCareRepository.GetAsync(treatmentDto.Id);

        if (healthCare != null)
        {
            throw new Exception($"TE: Treatment with ID={treatmentDto.Id} already exists");
        }

        healthCare = _mapper.Map<HealthCare>(treatmentDto);

        await _healthCareRepository.CreateAsync(healthCare);
        await _unitOfWork.SaveChangesAsync();
        return healthCare.Id;
    }

    public async Task DeleteTreatment(Guid id)
    {
        var _healthCareRepository = _unitOfWork.HealthCareRepository;

        await _healthCareRepository.DeleteAsync(id);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Guid> UpdateTreatment(CreateTreatmentDto treatmentDto)
    {
        var _healthCareRepository = _unitOfWork.HealthCareRepository;

        var healthCare = await _healthCareRepository.GetHealthCareWithDetails(treatmentDto.Id);

        if (healthCare == null)
        {
            throw new Exception($"TE: Treatment with ID={treatmentDto.Id} not found");
        }

        _mapper.Map(treatmentDto, healthCare);

        if (treatmentDto.PetId != Guid.Empty)
        {
            var pet = await _petAdoptionService.GetPetById(treatmentDto.PetId);
            if (pet == null)
            {
                throw new Exception($"TE: Pet with ID={treatmentDto.PetId} not found");
            }

            healthCare.Pet = _mapper.Map<Pet>(pet);
        }

        await _healthCareRepository.UpdateAsync(healthCare);
        await _unitOfWork.SaveChangesAsync();
        return healthCare.Id;
    }
}
