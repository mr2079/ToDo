using Application.Models;
using Domain.Repositories.Base;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Queries.GetTask;

public class GetTaskQuery : IRequest<ApiResponse<TaskDto>>
{
    public GetTaskQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}

public class GetTaskQueryHandler 
    : IRequestHandler<GetTaskQuery, ApiResponse<TaskDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetTaskQueryHandler> _logger;

    public GetTaskQueryHandler(
        IUnitOfWork unitOfWork,
        ILogger<GetTaskQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<ApiResponse<TaskDto>> Handle(
        GetTaskQuery request,
        CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.TaskRepository
            .GetQueryable()
            .FirstOrDefaultAsync(
                t => t.Id == request.Id,
                cancellationToken);

        if (task == null)
        {
            _logger.LogError($"there isn't any Task with Id : {request.Id}");

            List<string> messages = [TaskErrors.NotExist];

            return Response.IsFailure(messages);
        }

        var taskDto = task.Adapt<TaskDto>();

        return Response.IsSuccess(taskDto);
    }
}
