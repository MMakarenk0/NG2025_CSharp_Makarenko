namespace DAL_Core.Entities;

public class Store : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Location => $"{City}, {Address}";

    public string Address { get; set; }

    public string City { get; set; }

    public virtual ICollection<Pet> Pets { get; set; }
}
