using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskApp.Application.Shared.Responses;
using TaskApp.Application.Tasks.Responses;
using TaskApp.Infrastructure.Persistence;

namespace TaskApp.Application.Tasks.Queries
{
    public class GetTaskQuery : IRequest<Response<TaskDetailsResponse>>
    {
        public Guid Id { get; set; }
    }

    public class GetTaskHandler(ApplicationDbContext db) : IRequestHandler<GetTaskQuery, Response<TaskDetailsResponse>>
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<Response<TaskDetailsResponse>> Handle(GetTaskQuery query, CancellationToken cancellationToken)
        {
            var task = await _db.Tasks.FirstOrDefaultAsync(t => t.Id == query.Id, cancellationToken) ?? throw new KeyNotFoundException($"Task with Id {query.Id} not found");
            return new Response<TaskDetailsResponse>(task.ToDetailsResponse());
        }
    }
}
