using SentinelBusinessLayer.Models;

namespace SentinelBusinessLayer.Service.Interface;
public interface ITreatmentService
{
    Task<Guid> CreateTreatment(CreateTreatmentDto treatmentDto);
    Task DeleteTreatment(Guid id);
    Task<List<TreatmentDto>> GetAllTreatments();
    Task<List<TreatmentDto>> GetManyTreatmentByIds(IEnumerable<Guid> ids);
    Task<TreatmentDto> GetTreatment(Guid id);
    Task<bool> GetTreatmentStatus(Guid id);
    Task<Guid> UpdateTreatment(CreateTreatmentDto treatmentDto);
}
