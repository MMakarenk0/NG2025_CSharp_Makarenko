namespace SentinelBusinessLayer.Models;

public class CreateCustomerDto
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Phone { get; set; }

    public ICollection<Guid>? PetIds { get; set; }
}