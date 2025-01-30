using Application.Commands.CreateTask;
using Application.Commands.DeleteTask;
using Application.Commands.UpdateTask;
using Application.Queries.GetTask;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers;

[Route("api/tasks")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly ISender _sender;

    public TasksController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var response = await _sender.Send(new GetTaskQuery(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateTaskCommand request)
    {
        var response = await _sender.Send(request);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateTaskRequest request)
    {
        UpdateTaskCommand command = new(id);
        request.Adapt(command);
        var response = await _sender.Send(command);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var response = await _sender.Send(new DeleteTaskCommand(id));
        return Ok(response);
    }
}