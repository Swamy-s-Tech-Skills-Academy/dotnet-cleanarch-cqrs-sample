using MediatR;
using Products.Application.DTOs;

namespace Products.Application.Products.Queries.ProductsByDate;

public class ProductsByDateQuery : IRequest<List<ProductDto>>
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}