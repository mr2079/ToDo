using System.Diagnostics;
using Application.Extensions;
using Application.Models;
using Domain.Repositories.Base;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Queries.GetTasks;

public class GetTasksQuery : SearchQuery, IRequest<ApiResponse<PagedResult<TaskDto>>>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsCompleted { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class GetTasksQueryHandler 
    : IRequestHandler<GetTasksQuery, ApiResponse<PagedResult<TaskDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetTasksQueryHandler> _logger;

    public GetTasksQueryHandler(
        IUnitOfWork unitOfWork,
        ILogger<GetTasksQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<ApiResponse<PagedResult<TaskDto>>> Handle(
        GetTasksQuery request,
        CancellationToken cancellationToken)
    {
        var query = _unitOfWork.TaskRepository.GetQueryable();

        #region Filters

        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            query = query.Where(t => t.Title.Contains(request.Title));
        }

        if (!string.IsNullOrWhiteSpace(request.Description))
        {
            query = query.Where(t => t.Description.Contains(request.Description));
        }

        if (request.IsCompleted.HasValue)
        {
            query = query.Where(t => t.IsCompleted == request.IsCompleted.Value);
        }

        if (request.StartDate != null)
        {
            query = query.Where(t => t.DueDate >= request.StartDate);
        }

        if (request.EndDate != null)
        {
            query = query.Where(t => t.DueDate <= request.EndDate);
        }

        #endregion

        var totalItems = await query.CountAsync(cancellationToken);

        var tasks = await query
            .Page(request.Page.Number, request.Page.Size)
            .ToListAsync(cancellationToken);

        var items = tasks.Adapt<List<TaskDto>>();

        PagedResult<TaskDto> result = new(
            items,
            totalItems,
            request.Page.Number,
            request.Page.Size);

        return Response.IsSuccess(result);
    }
}
