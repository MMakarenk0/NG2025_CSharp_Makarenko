using DAL_Core.Enums;

namespace TreatmentBL.Models;

public class CreateVendorDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime? SignedAt { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public ContractType ContractType { get; set; }
}

    