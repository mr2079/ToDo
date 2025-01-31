using Application.Models;
using Domain.Repositories.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Commands.DeleteTask;

public class DeleteTaskCommand : IRequest<ApiResponse>
{
    public DeleteTaskCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}

public class DeleteTaskCommandHandler 
    : IRequestHandler<DeleteTaskCommand, ApiResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteTaskCommandHandler> _logger;

    public DeleteTaskCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<DeleteTaskCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<ApiResponse> Handle(
        DeleteTaskCommand request,
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

        _unitOfWork.TaskRepository.Remove(task);

        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Response.IsSuccess();
        }
        catch (Exception ex)
        {
            _logger.LogError($"exception : {ex.Message}" +
                             $" - inner exception : {ex.InnerException?.Message}");

            messages = [TaskErrors.DeleteFailed];

            return Response.IsFailure(messages);
        }
    }
}
