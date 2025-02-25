﻿using AutoMapper;
using MediatR;
using Products.Domain.Entities;
using Products.Domain.Filters;
using Products.Domain.Interfaces.Repositories;
using Products.Shared.DTOs;

namespace Products.Application.Products.Queries.ProductsByPrice;

public class ProductsByPriceQueryHandler(IProductsRepository productRepository, ICategoriesRepository categoryRepository, IMapper mapper) : IRequestHandler<ProductsByPriceQuery, List<ProductDto>>
{
    private readonly IProductsRepository _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    private readonly ICategoriesRepository _categoriesRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<List<ProductDto>> Handle(ProductsByPriceQuery request, CancellationToken cancellationToken)
    {
        ProductFilter filter = new()
        {
            MinPrice = request.MinPrice,
            MaxPrice = request.MaxPrice,
            SortColumn = request.SortColumn,
            SortDirection = request.SortDirection,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        List<Product>? products = await _productRepository.GetProductsAsync(filter);

        List<ProductDto> productDtos = _mapper.Map<List<ProductDto>>(products);

        return productDtos;
    }
}
