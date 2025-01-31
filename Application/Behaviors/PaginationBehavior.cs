using Application.Queries;
using MediatR;

namespace Application.Behaviors;

public class PaginationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : class
{
    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is SearchQuery searchQuery)
        {
            if (searchQuery.Page.Number == null
                || searchQuery.Page.Number < 1)
                searchQuery.Page.Number = 1;

            if (searchQuery.Page.Size == null
                || searchQuery.Page.Size < 1)
                searchQuery.Page.Size = int.MaxValue;
        }

        return await next();
    }
}