using DAL_Core.Enums;

namespace TreatmentBL.Models;

public class CreatePetDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Breed { get; set; }

    public PetTypes Type { get; set; }

    public Guid StoreId { get; set; }

    public Guid? CustomerId { get; set; }

    public ICollection<Guid>? TreatmentIds { get; set; }
}