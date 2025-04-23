namespace SentinelBusinessLayer.Models;

public class StoreDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Location => $"{City}, {Address}";

    public string Address { get; set; }

    public string City { get; set; }

    public ICollection<PetDto> Pets { get; set; }
}