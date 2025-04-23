using TreatmentBL.Models;

namespace TreatmentBL.Services.Interfaces;
public interface ITreatmentService
{
    Task<bool> GetTreatmentStatus(Guid id);
    Task<TreatmentDto> GetTreatment(Guid id);
    Task<List<TreatmentDto>> GetAllTreatments();
    Task<Guid> CreateTreatment(CreateTreatmentDto treatmentDto);
    Task<Guid> UpdateTreatment(CreateTreatmentDto treatmentDto);
    Task DeleteTreatment(Guid id);
    Task<List<TreatmentDto>> GetManyTreatmentByIds(IEnumerable<Guid> ids);
}
