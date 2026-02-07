
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskApp.Application.Tasks.Commands;
using TaskApp.Application.Tasks.Queries;
using TaskApp.Application.Tasks.Responses;
namespace TaskApp.Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("")]
    public async Task<ActionResult<List<TaskResponse>>> GetTasks([FromQuery] GetTasksQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskResponse>> GetTask(Guid id)
    {
        var result = await _mediator.Send(new GetTaskQuery { Id = id });
        return Ok(result);
    }

    [HttpPost("")]
    public async Task<ActionResult<TaskResponse>> CreateTask([FromBody] CreateTaskCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("")]
    public async Task<ActionResult<TaskResponse>> UpdateTask([FromBody] UpdateTaskCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteTask(Guid id)
    {
        var result = await _mediator.Send(new DeleteTaskCommand { Id = id });
        return Ok(result);
    }
}
