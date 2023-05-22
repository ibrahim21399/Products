namespace products.Domain.Entities;

public class BaseEntity
{
	public Guid Id { get; set; }
	public bool Deleted { get; set; }
	public string CreatedById { get; set; }
	public DateTime CreationDate { get; set; }
	public string ModifiedById { get; set; }
	public DateTime? ModificationDate { get; set; }
}

