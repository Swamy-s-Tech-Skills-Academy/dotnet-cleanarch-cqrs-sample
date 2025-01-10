using MediatR;
using Products.Application.DTOs;
using Products.Domain.Enums;

namespace Products.Application.Products.Queries.ProductsByDate;

public class ProductsByDateQuery : IRequest<List<ProductDto>>
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public SortColumn SortColumn { get; set; } = SortColumn.Id;

    public SortDirection SortDirection { get; set; } = SortDirection.Asc;

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}