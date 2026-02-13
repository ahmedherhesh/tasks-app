
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskApp.Application.Shared.Responses;
using TaskApp.Application.TaskTimes.Queries;
using TaskApp.Application.TaskTimes.Responses;

namespace TaskApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskTimesController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<TaskTimeResponse>>> GetTaskTime(Guid id)
        {
            return Ok(await mediator.Send(new GetTaskTimeQuery { Id = id }));
        }
    }
}