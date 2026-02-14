
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskApp.Application.Shared.Responses;
using TaskApp.Application.TaskTimes.Commands;
using TaskApp.Application.TaskTimes.Queries;
using TaskApp.Application.TaskTimes.Responses;

namespace TaskApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskTimesController(IMediator mediator) : ControllerBase
    {
        [HttpGet("")]
        public async Task<ActionResult<Response<List<TaskTimeResponse>>>> GetTaskTimes([FromQuery] GetTaskTimesQuery query)
        {
            return Ok(await mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<TaskTimeResponse>>> GetTaskTime(Guid id)
        {
            return Ok(await mediator.Send(new GetTaskTimeQuery { Id = id }));
        }

        [HttpPost("Start")]
        public async Task<ActionResult<Response<TaskTimeDetailsResponse>>> StartTaskTime([FromBody] StartTaskTimeCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPut("End")]
        public async Task<ActionResult<Response<TaskTimeDetailsResponse>>> EndTaskTime([FromBody] EndTaskTimeCommand command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}