namespace PetAdoptionBL.Models;

public class CreateStoreDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Location => $"{City}, {Address}";

    public string Address { get; set; }

    public string City { get; set; }

    public ICollection<Guid>? PetIds { get; set; }
}

