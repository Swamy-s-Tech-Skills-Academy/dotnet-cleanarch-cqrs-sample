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


public class ProductsQueryByPrice : IRequest<List<ProductDto>>
{
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class ProductsQueryByDate : IRequest<List<ProductDto>>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class ProductsQueryByCategory : IRequest<List<ProductDto>>
{
    public string Category { get; set; } = default!;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetAllProductsQuery : IRequest<List<ProductDto>> 
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}


Okay, you want to implement four query use cases for retrieving products: by price range, by date range, by category name, and all products with pagination. Here's how you can implement these in your Application layer, along with necessary adjustments to the Infrastructure and Domain layers:

**1. Domain Layer Changes (ProductFilter):**

It's better to create a single filter object to encapsulate all filtering criteria. This makes your repository methods more flexible.

```csharp
// Products.Domain/Filters/ProductFilter.cs
public class ProductFilter
{
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid? CategoryId { get; set; } // Use Guid for CategoryId
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
```

**2. Infrastructure Layer Changes (Repository):**

Modify the `IProductsRepository` and `ProductsRepository` to use the `ProductFilter`:

```csharp
// Products.Domain/Interfaces/Repositories/IProductsRepository.cs
public interface IProductsRepository
{
    Task<List<Product>> GetProductsAsync(ProductFilter filter);
    // ... other methods
}

// Products.Infrastructure/Persistence/ProductsRepository.cs
public async Task<List<Product>> GetProductsAsync(ProductFilter filter)
{
    IQueryable<Product> query = _storeDbContext.Products.Include(p => p.Category);

    if (filter.MinPrice.HasValue)
    {
        query = query.Where(p => p.Price >= filter.MinPrice);
    }

    if (filter.MaxPrice.HasValue)
    {
        query = query.Where(p => p.Price <= filter.MaxPrice);
    }

    if (filter.StartDate.HasValue && filter.EndDate.HasValue)
    {
        query = query.Where(p => p.CreatedDate >= filter.StartDate && p.CreatedDate <= filter.EndDate);
    }

    if (filter.CategoryId.HasValue)
    {
        query = query.Where(p => p.CategoryId == filter.CategoryId);
    }

    return await query
        .Skip((filter.PageNumber - 1) * filter.PageSize)
        .Take(filter.PageSize)
        .ToListAsync();
}
```

**3. Application Layer (Queries, Handlers, DTOs):**

```csharp
// Products.Application/DTOs/ProductDto.cs
public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
}

// Products.Application/Queries/ProductsQueryByPrice.cs
public class ProductsQueryByPrice : IRequest<List<ProductDto>>
{
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

// Products.Application/Handlers/ProductsQueryByPriceHandler.cs
public class ProductsQueryByPriceHandler : IRequestHandler<ProductsQueryByPrice, List<ProductDto>>
{
    private readonly IProductsRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    public ProductsQueryByPriceHandler(IProductsRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    public async Task<List<ProductDto>> Handle(ProductsQueryByPrice request, CancellationToken cancellationToken)
    {
        var filter = new ProductFilter
        {
            MinPrice = request.MinPrice,
            MaxPrice = request.MaxPrice,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
        var products = await _productRepository.GetProductsAsync(filter);
        var productDtos = _mapper.Map<List<ProductDto>>(products);
        foreach (var productDto in productDtos)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(productDto.CategoryId);
            productDto.CategoryName = category?.Name;
        }
        return productDtos;
    }
}

// Products.Application/Queries/ProductsQueryByDate.cs
public class ProductsQueryByDate : IRequest<List<ProductDto>>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

// Products.Application/Handlers/ProductsQueryByDateHandler.cs
public class ProductsQueryByDateHandler : IRequestHandler<ProductsQueryByDate, List<ProductDto>>
{
    private readonly IProductsRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    public ProductsQueryByDateHandler(IProductsRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    public async Task<List<ProductDto>> Handle(ProductsQueryByDate request, CancellationToken cancellationToken)
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
        foreach (var productDto in productDtos)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(productDto.CategoryId);
            productDto.CategoryName = category?.Name;
        }
        return productDtos;
    }
}

// Products.Application/Queries/ProductsQueryByCategory.cs
public class ProductsQueryByCategory : IRequest<List<ProductDto>>
{
    public Guid CategoryId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

// Products.Application/Handlers/ProductsQueryByCategoryHandler.cs
public class ProductsQueryByCategoryHandler : IRequestHandler<ProductsQueryByCategory, List<ProductDto>>
{
    private readonly IProductsRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    public ProductsQueryByCategoryHandler(IProductsRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    public async Task<List<ProductDto>> Handle(ProductsQueryByCategory request, CancellationToken cancellationToken)
    {
        var filter = new ProductFilter
        {
            CategoryId = request.CategoryId,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
        var products = await _productRepository.GetProductsAsync(filter);
        var productDtos = _mapper.Map<List<ProductDto>>(products);
        foreach (var productDto in productDtos)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(productDto.CategoryId);
            productDto.CategoryName = category?.Name;
        }
        return productDtos;
    }
}

// Products.Application/Queries/GetAllProductsQuery.cs
public class GetAllProductsQuery : IRequest<List<ProductDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

// Products.Application/Handlers/GetAllProductsQueryHandler.cs
public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
{
    private readonly IProductsRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    public GetAllProductsQueryHandler(IProductsRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var filter = new ProductFilter
        {
            