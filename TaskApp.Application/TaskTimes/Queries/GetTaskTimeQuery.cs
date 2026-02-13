using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskApp.Application.Shared.Responses;
using TaskApp.Application.TaskTimes.Responses;
using TaskApp.Infrastructure.Persistence;

namespace TaskApp.Application.TaskTimes.Queries
{
    public class GetTaskTimeQuery : IRequest<Response<TaskTimeDetailsResponse>>
    {
        public Guid Id { set; get; }
    }

    public class GetTaskTimeHandler(ApplicationDbContext db) : IRequestHandler<GetTaskTimeQuery, Response<TaskTimeDetailsResponse>>
    {
        public async Task<Response<TaskTimeDetailsResponse>> Handle(GetTaskTimeQuery request, CancellationToken cancellationToken)
        {
            var taskTime = await db.TaskTimes.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken) ?? throw new KeyNotFoundException($"TaskTime with Id {request.Id} not found");
            return new Response<TaskTimeDetailsResponse>(taskTime.ToDetailsResponse());
        }
    }
}