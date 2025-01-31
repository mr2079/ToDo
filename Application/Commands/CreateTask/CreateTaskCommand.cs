using Application.Models;
using Domain.Repositories.Base;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.CreateTask;

public class CreateTaskCommand : IRequest<ApiResponse<Guid>>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
}

public class CreateTaskCommandHandler 
    : IRequestHandler<CreateTaskCommand, ApiResponse<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateTaskCommandHandler> _logger;

    public CreateTaskCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<CreateTaskCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<ApiResponse<Guid>> Handle(
        CreateTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = request.Adapt<TaskEntity>();

        await _unitOfWork.TaskRepository.AddAsync(task);

        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Response.IsSuccess(task.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError($"exception : {ex.Message}" +
                             $" - inner exception : {ex.InnerException?.Message}");

            List<string> messages = [TaskErrors.CreateFailed];

            return Response.IsFailure(messages);
        }
    }
}