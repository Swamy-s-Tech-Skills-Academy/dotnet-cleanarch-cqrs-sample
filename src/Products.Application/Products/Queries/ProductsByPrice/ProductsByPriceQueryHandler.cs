using AutoMapper;
using MediatR;
using Products.Application.DTOs;
using Products.Domain.Interfaces.Repositories;

namespace Products.Application.Products.Queries.ProductsByPrice;

public class ProductsByPriceQueryHandler(IProductsRepository productRepository, ICategoriesRepository categoryRepository, IMapper mapper) : IRequestHandler<ProductsByPriceQuery, List<ProductDto>>
{
    private readonly IProductsRepository _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    private readonly ICategoriesRepository _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public Task<List<ProductDto>> Handle(ProductsByPriceQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
