
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskApp.Application.Shared.Responses;
using TaskApp.Application.Tasks.Commands;
using TaskApp.Application.Tasks.Queries;
using TaskApp.Application.Tasks.Responses;
namespace TaskApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController(IMediator mediator) : ControllerBase
{
    [HttpGet("")]
    public async Task<ActionResult<Response<List<TaskResponse>>>> GetTasks([FromQuery] GetTasksQuery query)
    {
        return Ok(await mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Response<TaskResponse>>> GetTask(Guid id)
    {
        return Ok(await mediator.Send(new GetTaskQuery { Id = id }));
    }

    [HttpPost("")]
    public async Task<ActionResult<Response<TaskResponse>>> CreateTask([FromBody] CreateTaskCommand command)
    {
        return Ok(await mediator.Send(command));
    }

    [HttpPut("")]
    public async Task<ActionResult<Response<TaskResponse>>> UpdateTask([FromBody] UpdateTaskCommand command)
    {
        return Ok(await mediator.Send(command));
    }

    [HttpPut("Status")]
    public async Task<ActionResult<Response<bool>>> UpdateTaskStatus([FromBody] UpdateTaskStatusCommand command)
    {
        return Ok(await mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Response<bool>>> DeleteTask(Guid id)
    {
        return Ok(await mediator.Send(new DeleteTaskCommand { Id = id }));
    }
}
