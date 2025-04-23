namespace DAL_Core.Entities;

public class Customer : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public virtual ICollection<Pet> Pets { get; set; }
}

