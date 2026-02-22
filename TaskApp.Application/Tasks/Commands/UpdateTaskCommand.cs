using MediatR;
using TaskApp.Application.Tasks.Commands;
using TaskApp.Application.Tasks.Responses;
using TaskApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using TaskApp.Application.Shared.Responses;

namespace TaskApp.Application.Tasks.Commands
{
    public class UpdateTaskCommand() : IRequest<Response<TaskResponse>>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
    }

    public class UpdateTaskHandler(ApplicationDbContext db) : IRequestHandler<UpdateTaskCommand, Response<TaskResponse>>
    {
        public async Task<Response<TaskResponse>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await db.Tasks.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken) ?? throw new KeyNotFoundException($"Task with Id {request.Id} not found");

            if (!string.IsNullOrEmpty(request.Title))
                task.Title = request.Title;

            await db.SaveChangesAsync(cancellationToken);

            return new Response<TaskResponse>(task.ToResponse());
        }
    }
}
