﻿using MediatR;
using Products.Application.DTOs;

namespace Products.Application.Products.Queries;

public class GetProductsQuery : IRequest<List<ProductDto>>
{
    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public Guid? CategoryId { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}