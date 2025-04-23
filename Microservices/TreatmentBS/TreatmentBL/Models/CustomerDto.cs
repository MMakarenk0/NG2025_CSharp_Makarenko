namespace TreatmentBL.Models;

public class CustomerDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Phone { get; set; }

    public ICollection<PetDto> Pets { get; set; }
}