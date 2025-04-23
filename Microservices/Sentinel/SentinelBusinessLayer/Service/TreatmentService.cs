using SentinelBusinessLayer.Clients;
using SentinelBusinessLayer.Models;
using SentinelBusinessLayer.Service.Interface;

namespace SentinelBusinessLayer.Service;
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

    public async Task<TreatmentDto> GetTreatment(Guid id)
    {
        return await _treatmentClient.GetTreatmentById(id);
    }

    public async Task<bool> GetTreatmentStatus(Guid id)
    {
        return await _treatmentClient.GetTreatmentStatus(id);
    }

    public async Task<List<TreatmentDto>> GetManyTreatmentByIds(IEnumerable<Guid> ids)
    {
        return await _treatmentClient.GetManyTreatments(ids);
    }

    public async Task<Guid> CreateTreatment(CreateTreatmentDto treatmentDto)
    {
        return await _treatmentClient.CreateTreatment(treatmentDto);
    }

    public async Task<Guid> UpdateTreatment(CreateTreatmentDto treatmentDto)
    {
        return await _treatmentClient.UpdateTreatment(treatmentDto);
    }

    public async Task DeleteTreatment(Guid id)
    {
        await _treatmentClient.DeleteTreatment(id);
    }
}

