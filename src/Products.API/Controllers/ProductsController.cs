using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.DTOs;
using Products.Application.Products.Queries;

namespace Products.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost]
    public async Task<ActionResult<List<ProductDto>>> GetProducts([FromBody] GetProductsQuery request)
    {
        var products = await _mediator.Send(request);

        return Ok(products);
    }
}
