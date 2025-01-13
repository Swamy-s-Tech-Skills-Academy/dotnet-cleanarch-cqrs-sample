using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Products.Queries;
using Products.Application.Products.Queries.ProductsByDate;
using Products.Application.Products.Queries.ProductsByPrice;
using Products.Shared.DTOs;

namespace Products.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IMediator mediator, ILogger<ProductsController> logger) : ControllerBase // Inject ILogger
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly ILogger<ProductsController> _logger = logger ?? throw new ArgumentNullException(nameof(logger)); // Store logger

    [HttpPost]
    public async Task<ActionResult<List<ProductDto>>> GetProducts([FromBody] GetProductsQuery request)
    {
        IRequest<List<ProductDto>> queryToSend = request;

        if (request.MinPrice.HasValue && request.MaxPrice.HasValue)
        {
            queryToSend = new ProductsByPriceQuery
            {
                MinPrice = request.MinPrice.Value,
                MaxPrice = request.MaxPrice.Value,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SortColumn = request.SortColumn,
                SortDirection = request.SortDirection
            };
        }
        else if (request.StartDate.HasValue && request.EndDate.HasValue)
        {
            queryToSend = new ProductsByDateQuery
            {
                StartDate = request.StartDate.Value,
                EndDate = request.EndDate.Value,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SortColumn = request.SortColumn,
                SortDirection = request.SortDirection
            };
        }

        try
        {
            List<ProductDto>? products = await _mediator.Send(queryToSend);
            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the GetProducts request."); // Log the exception with ILogger

            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}