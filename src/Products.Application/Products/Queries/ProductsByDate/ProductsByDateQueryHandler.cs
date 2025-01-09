using AutoMapper;
using MediatR;
using Products.Application.DTOs;
using Products.Domain.Filters;
using Products.Domain.Interfaces.Repositories;

namespace Products.Application.Products.Queries.ProductsByDate;

public class ProductsByDateQueryHandler(IProductsRepository productRepository, ICategoriesRepository categoryRepository, IMapper mapper) : IRequestHandler<ProductsByDateQuery, List<ProductDto>>
{
    private readonly IProductsRepository _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    private readonly ICategoriesRepository _categoriesRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<List<ProductDto>> Handle(ProductsByDateQuery request, CancellationToken cancellationToken)
    {
        var filter = new ProductFilter
        {
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        var products = await _productRepository.GetProductsAsync(filter);

        var productDtos = _mapper.Map<List<ProductDto>>(products);

        return productDtos;
    }
}