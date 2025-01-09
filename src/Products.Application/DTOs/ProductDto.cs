namespace Products.Application.DTOs;

public record ProductDto
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public decimal Price { get; init; }

    public CategoryDto Category { get; init; } = default!; // Embed CategoryDto

    public DateTime CreatedDate { get; init; }
}
