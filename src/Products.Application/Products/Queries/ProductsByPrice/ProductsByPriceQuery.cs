using MediatR;
using Products.Application.DTOs;

namespace Products.Application.Products.Queries.ProductsByPrice;

public class ProductsByPriceQuery : IRequest<List<ProductDto>>
{
    public decimal MinPrice { get; set; }

    public decimal MaxPrice { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}
