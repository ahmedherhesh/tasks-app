using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskApp.Application.Shared.Responses;
using TaskApp.Application.Tasks.Responses;
using TaskApp.Infrastructure.Extensions;
using TaskApp.Infrastructure.Persistence;

namespace TaskApp.Application.Tasks.Queries
{
    public class GetTasksQuery : IRequest<Response<List<TaskResponse>>>
    {
        public string TextSearch { get; set; } = string.Empty;
    }

    public class GetTasksHandler(ApplicationDbContext db) : IRequestHandler<GetTasksQuery, Response<List<TaskResponse>>>
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<Response<List<TaskResponse>>> Handle(GetTasksQuery query, CancellationToken cancellationToken)
        {
            var tasks = await _db.Tasks
                .When(!string.IsNullOrWhiteSpace(query.TextSearch), (q) => q.Where(t => t.Title.Contains(query.TextSearch)))
                .Select(task => task.ToResponse())
                .ToListAsync(cancellationToken);

            return new Response<List<TaskResponse>>(tasks);
        }
    }
}
