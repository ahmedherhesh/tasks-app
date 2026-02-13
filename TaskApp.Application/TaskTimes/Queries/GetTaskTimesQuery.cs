using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskApp.Application.Shared.Responses;
using TaskApp.Application.TaskTimes.Responses;
using TaskApp.Infrastructure.Extensions;
using TaskApp.Infrastructure.Persistence;

namespace TaskApp.Application.TaskTimes.Queries
{
    public class GetTaskTimesQuery : IRequest<Response<List<TaskTimeResponse>>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class GetTaskTimesHandler(ApplicationDbContext db) : IRequestHandler<GetTaskTimesQuery, Response<List<TaskTimeResponse>>>
    {
        public async Task<Response<List<TaskTimeResponse>>> Handle(GetTaskTimesQuery request, CancellationToken cancellationToken)
        {
            var taskTimes = await db.TaskTimes
                .When(request.StartDate != default, q => q.Where(t => t.Start >= request.StartDate))
                .When(request.EndDate != default, q => q.Where(t => t.Start <= request.EndDate))
                .Select(taskTime => taskTime.ToResponse()).ToListAsync(cancellationToken);

            return new Response<List<TaskTimeResponse>>(taskTimes);
        }
    }
}