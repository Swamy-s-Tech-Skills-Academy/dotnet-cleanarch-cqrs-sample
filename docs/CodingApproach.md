# Coding Approach

Yes, absolutely. Focusing solely on the `Product` entity and related functionalities for now is an excellent way to keep things simple and manageable, especially when learning or demonstrating Clean Architecture and CQRS.

Here's a suggested approach for focusing on just products:

**1. Products.Domain:**

- **Entities:**
  - `Product.cs`:
    ```csharp
    namespace Products.Domain.Entities
    {
        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; } = default!;
            public decimal Price { get; set; }
            public string Category { get; set; } = default!;
            public DateTime CreatedDate { get; set; }
            // Add other relevant product properties (e.g., Description, ImageUrl)
        }
    }
    ```
- **Interfaces/Repositories:**

  - `IProductRepository.cs`:

    ```csharp
    using Products.Domain.Entities;
    using Products.Domain.Filters;

    namespace Products.Domain.Interfaces.Repositories
    {
        public interface IProductRepository
        {
            Task<List<Product>> GetProductsAsync(ProductFilter filter);
            Task<Product?> GetProductByIdAsync(int id);
            Task AddProductAsync(Product product);
            Task UpdateProductAsync(Product product);
            Task DeleteProductAsync(int id);
        }
    }
    ```

- **Filters:**
  - `ProductFilter.cs`:
    ```csharp
    namespace Products.Domain.Filters
    {
        public class ProductFilter
        {
            public decimal? MinPrice { get; set; }
            public decimal? MaxPrice { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public string? Category { get; set; }
            public int PageNumber { get; set; } = 1;
            public int PageSize { get; set; } = 10;
        }
    }
    ```

**2. Products.Application:**

- **DTOs:**
  - `ProductDto.cs`:
    ```csharp
    namespace Products.Application.DTOs
    {
        public class ProductDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = default!;
            public decimal Price { get; set; }
            public string Category { get; set; } = default!;
            public DateTime CreatedDate { get; set; }
            // ... other properties you want to expose
        }
    }
    ```
- **Queries:**

  - `GetProductsQuery.cs`:

    ```csharp
    using MediatR;
    using Products.Application.DTOs;
    using Products.Domain.Filters;

    namespace Products.Application.Queries
    {
        public class GetProductsQuery : IRequest<List<ProductDto>>
        {
            public ProductFilter Filter { get; set; } = new();
        }
    }
    ```

  - `GetProductByIdQuery.cs`:

    ```csharp
    using MediatR;
    using Products.Application.DTOs;

    namespace Products.Application.Queries
    {
        public class GetProductByIdQuery : IRequest<ProductDto?>
        {
            public int Id { get; set; }
        }
    }
    ```

- **Handlers:**

  - `GetProductsQueryHandler.cs`:
    ```csharp
    // ... (Implementation as before, using _productRepository.GetProductsAsync(request.Filter))
    ```
  - `GetProductByIdQueryHandler.cs`:

    ```csharp
    using AutoMapper;
    using MediatR;
    using Products.Application.DTOs;
    using Products.Application.Queries;
    using Products.Domain.Interfaces.Repositories;

    namespace Products.Application.Handlers
    {
        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;

            public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetProductByIdAsync(request.Id);
                return _mapper.Map<ProductDto?>(product);
            }
        }
    }
    ```

- **Validators:**

  - `ProductsRequestValidator.cs`: (As before, validating `GetProductsQuery.Filter`)
  - `GetProductByIdQueryValidator.cs`:

    ```csharp
    using FluentValidation;
    using Products.Application.Queries;

    namespace Products.Application.Validators
    {
        public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
        {
            public GetProductByIdQueryValidator()
            {
                RuleFor(x => x.Id).GreaterThan(0);
            }
        }
    }
    ```

- **Commands (Optional for now):** You can add commands later for creating, updating, and deleting products.

**3. Products.Infrastructure:**

- **Persistence:**
  - `ProductDbContext.cs`: (As before)
  - `ProductsRepository.cs`: Implement the `IProductRepository` interface.

**4. Products.API:**

- **Controllers:**
  - `ProductsController.cs`: Add actions for getting products (using `GetProductsQuery` and `GetProductByIdQuery`).

**Example Controller Actions:**

```csharp
[HttpGet("products")]
public async Task<IActionResult> GetProducts([FromQuery] GetProductsQuery query)
{
    // Validate and handle as before
}

[HttpGet("products/{id}")]
public async Task<IActionResult> GetProductById(int id)
{
    var query = new GetProductByIdQuery { Id = id };
    var validationResult = await _validator.ValidateAsync(query);

    if (!validationResult.IsValid)
    {
        return BadRequest(validationResult.Errors);
    }

    var product = await _mediator.Send(query);

    if (product == null)
    {
        return NotFound();
    }

    return Ok(product);
}
```

By focusing on just the `Product` entity, you can create a working example of Clean Architecture with CQRS and MediatR without getting overwhelmed by too many domain concepts. Once you have this working, it will be much easier to add other entities and features as needed. This incremental approach is highly recommended.
