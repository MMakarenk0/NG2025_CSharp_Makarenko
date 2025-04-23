using PetAdoptionBL.Models;

namespace PetAdoptionBL.Services.Interfaces;

public interface ITreatmentService
{
    Task<Guid> CreateTreatment(CreateTreatmentDto treatmentDto);
    Task DeleteTreatment(Guid id);
    Task<List<TreatmentDto>> GetAllTreatments();
    Task<List<TreatmentDto>> GetManyTreatmentsAsync(IEnumerable<Guid> ids);
    Task<TreatmentDto> GetTreatmentById(Guid id);
}