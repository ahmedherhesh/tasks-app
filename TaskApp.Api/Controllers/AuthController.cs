
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskApp.Application.Authentication.Commands;
using TaskApp.Application.Authentication.Responses;
using TaskApp.Application.Shared.Responses;

namespace TaskApp.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Response<RegisterResponse>>> Register([FromBody] RegisterCommand command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}