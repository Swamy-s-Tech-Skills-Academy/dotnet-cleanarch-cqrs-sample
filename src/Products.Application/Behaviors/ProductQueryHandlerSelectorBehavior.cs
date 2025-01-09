using MediatR;
using Products.Application.DTOs;
using Products.Application.Products.Queries;
using Products.Application.Products.Queries.ProductsByDate;
using Products.Application.Products.Queries.ProductsByPrice;

namespace Products.Application.Behaviors;

public class ProductQueryHandlerSelectorBehavior<TRequest, TResponse>(IMediator mediator) :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : GetProductsQuery, IRequest<TResponse>
    where TResponse : List<ProductDto>
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        IRequest<List<ProductDto>> queryToSend = default!;

        if (request.MinPrice.HasValue && request.MaxPrice.HasValue)
        {
            queryToSend = new ProductsByPriceQuery
            {
                MinPrice = request.MinPrice.Value,
                MaxPrice = request.MaxPrice.Value,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
        else if (request.StartDate.HasValue && request.EndDate.HasValue)
        {
            queryToSend = new ProductsByDateQuery
            {
                StartDate = request.StartDate.Value,
                EndDate = request.EndDate.Value,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }

        return (TResponse)await _mediator.Send(queryToSend, cancellationToken);
    }
}
