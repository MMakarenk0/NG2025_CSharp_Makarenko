using PetAdoptionBL.Clients;
using PetAdoptionBL.Models;
using PetAdoptionBL.Services.Interfaces;

namespace PetAdoptionBL.Services;

public class TreatmentService : ITreatmentService
{
    private readonly ITreatmentClient _treatmentClient;

    public TreatmentService(ITreatmentClient treatmentClient)
    {
        _treatmentClient = treatmentClient;
    }

    public async Task<List<TreatmentDto>> GetAllTreatments()
    {
        return await _treatmentClient.GetAllTreatments();
    }

    public async Task<TreatmentDto> GetTreatmentById(Guid id)
    {
        var treatment = await _treatmentClient.GetTreatmentById(id);
        return treatment;
    }

    public async Task<Guid> CreateTreatment(CreateTreatmentDto treatmentDto)
    {
        var treatmentId = await _treatmentClient.CreateTreatment(treatmentDto);
        return treatmentId;
    }

    public async Task DeleteTreatment(Guid id)
    {
        await _treatmentClient.DeleteTreatment(id);
    }

    public async Task<List<TreatmentDto>> GetManyTreatmentsAsync(IEnumerable<Guid> ids)
    {
        var treatments = await _treatmentClient.GetManyTreatmentsAsync(ids);
        return treatments;
    }
}

