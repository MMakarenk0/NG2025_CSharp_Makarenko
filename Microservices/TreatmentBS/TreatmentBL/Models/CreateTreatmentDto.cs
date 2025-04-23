namespace TreatmentBL.Models;

public class CreateTreatmentDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime InjectedAt { get; set; }

    public DateTime ExpirationDate { get; set; }

    public Guid PetId { get; set; }

    public Guid VendorId { get; set; }
}

