using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskApp.Application.Projects.Responses;
using TaskApp.Application.Shared.Responses;
using TaskApp.Infrastructure.Extensions;
using TaskApp.Infrastructure.Persistence;

namespace TaskApp.Application.Projects.Queries
{
    public class GetProjectQuery : IRequest<Response<ProjectResponse>>
    {
        public Guid Id { get; set; }
    }

    public class GetProjectHandler(ApplicationDbContext db) : IRequestHandler<GetProjectQuery, Response<ProjectResponse>>
    {
        public async Task<Response<ProjectResponse>> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await db.Projects.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken) ?? throw new KeyNotFoundException($"Project with Id {request.Id} not found");
            return new Response<ProjectResponse>(project.ToDetailsResponse());
        }
    }
}