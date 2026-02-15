using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskApp.Application.Projects.Commands;
using TaskApp.Application.Projects.Queries;
using TaskApp.Application.Projects.Responses;
using TaskApp.Application.Shared.Responses;

namespace TaskApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<Response<List<ProjectResponse>>>> GetProjects([FromQuery] GetProjectsQuery query)
        {
            return Ok(await mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<ProjectResponse>>> GetProject(Guid id)
        {
            return Ok(await mediator.Send(new GetProjectQuery { Id = id }));
        }
        [HttpPost]
        public async Task<ActionResult<Response<ProjectResponse>>> CreateProject([FromBody] CreateProjectCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPut]
        public async Task<ActionResult<Response<ProjectResponse>>> UpdateProject([FromBody] UpdateProjectCommand command)
        {
            return Ok(await mediator.Send(command));
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<bool>>> DeleteProject(Guid id)
        {
            return Ok(await mediator.Send(new DeleteProjectCommand { Id = id }));
        }
    }
}