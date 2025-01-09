namespace Products.Application.DTOs;

public record CategoryDto
{
    public Guid Id { get; init; } // Use init-only setters for records

    public string Name { get; init; } = string.Empty;
}
