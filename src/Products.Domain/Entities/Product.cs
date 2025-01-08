using System.ComponentModel.DataAnnotations;

namespace Products.Domain.Entities;

public class Product : Entity
{
    [MaxLength(255)]
    [Required]
    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public Guid CategoryId { get; set; }

    public Category Category { get; set; } = default!;
}
