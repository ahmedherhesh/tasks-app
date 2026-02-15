using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskApp.Application.Projects.Responses;
using TaskApp.Application.Shared.Responses;
using TaskApp.Infrastructure.Extensions;
using TaskApp.Infrastructure.Persistence;

namespace TaskApp.Application.Projects.Queries
{
    public class GetProjectsQuery : IRequest<Response<List<ProjectResponse>>>
    {
        public string TextSearch { get; set; } = string.Empty;
    }

    public class GetProjectsHandler(ApplicationDbContext db) : IRequestHandler<GetProjectsQuery, Response<List<ProjectResponse>>>
    {
        public async Task<Response<List<ProjectResponse>>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await db.Projects
                .When(!string.IsNullOrWhiteSpace(request.TextSearch), (q) => q.Where(t => t.Title.Contains(request.TextSearch)))
                .Select(project => project.ToResponse())
                .ToListAsync(cancellationToken);

            return new Response<List<ProjectResponse>>(projects);
        }
    }
}