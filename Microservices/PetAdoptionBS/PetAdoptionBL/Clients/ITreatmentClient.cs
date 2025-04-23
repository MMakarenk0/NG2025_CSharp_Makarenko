using PetAdoptionBL.Models;
using Refit;

namespace PetAdoptionBL.Clients;

public interface ITreatmentClient
{
    [Get("/api/treatment")]
    Task<List<TreatmentDto>> GetAllTreatments();

    [Get("/api/treatment/{id}")]
    Task<TreatmentDto> GetTreatmentById(Guid id);

    [Post("/api/treatment")]
    Task<Guid> CreateTreatment([Body] CreateTreatmentDto treatmentDto);

    [Delete("/api/treatment/{id}")]
    Task DeleteTreatment(Guid id);

    [Get("/treatment/many")]
    Task<List<TreatmentDto>> GetManyTreatmentsAsync([Query(CollectionFormat.Multi)] IEnumerable<Guid> ids);
}

