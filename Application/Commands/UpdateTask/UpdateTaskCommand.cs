using Application.Models;
using Domain.Repositories.Base;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Commands.UpdateTask;

public class UpdateTaskCommand : IRequest<ApiResponse>
{
    public UpdateTaskCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime DueDate { get; set; }
}

public class UpdateTaskCommandHandler 
    : IRequestHandler<UpdateTaskCommand, ApiResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateTaskCommandHandler> _logger;

    public UpdateTaskCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<UpdateTaskCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<ApiResponse> Handle(
        UpdateTaskCommand request,
        CancellationToken cancellationToken)
    {
        List<string>? messages;

        var task = await _unitOfWork.TaskRepository
            .GetQueryable()
            .FirstOrDefaultAsync(
                t => t.Id == request.Id,
                cancellationToken);

        if (task == null)
        {
            _logger.LogError($"there isn't any Task with Id : {request.Id}");

            messages = [TaskErrors.NotFound];

            return Response.IsFailure(messages);
        }

        if (task.DueDate != request.DueDate 
            && request.DueDate <= DateTime.Now)
        {
            _logger.LogError("new DueDate should be after present");

            messages = [TaskErrors.InvalidDueDate];

            return Response.IsFailure(messages);
        }

        request.Adapt(task);

        _unitOfWork.TaskRepository.Update(task);

        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Response.IsSuccess();
        }
        catch (Exception ex)
        {
            _logger.LogError($"exception : {ex.Message}" +
                             $" - inner exception : {ex.InnerException?.Message}");

            messages = [TaskErrors.UpdateFailed];

            return Response.IsFailure(messages);
        }
    }
}
