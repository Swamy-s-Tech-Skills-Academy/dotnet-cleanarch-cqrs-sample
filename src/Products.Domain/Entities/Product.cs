namespace Products.Domain.Entities;

public class Product : Entity
{
    public string Name { get; set; } = default!;

    public decimal Price { get; set; }

    public Guid CategoryId { get; set; }

    public Category Category { get; set; } = default!;
}
