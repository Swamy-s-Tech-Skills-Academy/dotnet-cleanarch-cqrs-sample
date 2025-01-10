using MediatR;
using Products.Application.DTOs;
using Products.Domain.Enums;

namespace Products.Application.Products.Queries;

public class GetProductsQuery : IRequest<List<ProductDto>>
{
    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public Guid? CategoryId { get; set; }

    public SortColumn SortColumn { get; set; } = SortColumn.Id;

    public SortDirection SortDirection { get; set; } = SortDirection.Asc;

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}