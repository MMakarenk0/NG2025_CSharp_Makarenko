namespace PetAdoptionBL.Models;

public class TreatmentDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public bool IsExpired { get; }

    public DateTime InjectedAt { get; set; }

    public DateTime ExpirationDate { get; set; }

    public Guid PetId { get; set; }

    public Guid VendorId { get; set; }
}