using Refit;
using SentinelBusinessLayer.Models;

namespace SentinelBusinessLayer.Clients;
public interface ITreatmentClient
{
    [Get("/api/treatment")]
    Task<List<TreatmentDto>> GetAllTreatments();

    [Get("/api/treatment/{id}")]
    Task<TreatmentDto> GetTreatmentById(Guid id);

    [Get("/api/treatment/status/{id}")]
    Task<bool> GetTreatmentStatus(Guid id);

    [Get("/api/treatment/many")]
    Task<List<TreatmentDto>> GetManyTreatments([Query(CollectionFormat.Multi)] IEnumerable<Guid> ids);

    [Post("/api/treatment")]
    [Headers("Content-Type: multipart/form-data")]
    Task<Guid> CreateTreatment([Body(BodySerializationMethod.UrlEncoded)] CreateTreatmentDto treatmentDto);

    [Put("/api/treatment")]
    [Headers("Content-Type: multipart/form-data")]
    Task<Guid> UpdateTreatment([Body(BodySerializationMethod.UrlEncoded)] CreateTreatmentDto treatmentDto);

    [Delete("/api/treatment/{id}")]
    Task DeleteTreatment(Guid id);
}
