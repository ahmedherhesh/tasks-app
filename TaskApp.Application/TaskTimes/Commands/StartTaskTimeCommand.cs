using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskApp.Application.Shared.Responses;
using TaskApp.Application.TaskTimes.Responses;
using TaskApp.Domain.Entities;
using TaskApp.Infrastructure.Persistence;

namespace TaskApp.Application.TaskTimes.Commands
{
    public class StartTaskTimeCommand : IRequest<Response<TaskTimeDetailsResponse>>
    {
        public Guid TaskId { get; set; }
    }

    public class StartTaskTimeCommandValidator : AbstractValidator<StartTaskTimeCommand>
    {
        public StartTaskTimeCommandValidator()
        {
            RuleFor(x => x.TaskId).NotEmpty().WithMessage("TaskId is required");
        }
    }

    public class StartTaskTimeCommandHandler(ApplicationDbContext db) : IRequestHandler<StartTaskTimeCommand, Response<TaskTimeDetailsResponse>>
    {
        public async Task<Response<TaskTimeDetailsResponse>> Handle(StartTaskTimeCommand request, CancellationToken cancellationToken)
        {
            var taskExists = await db.Tasks.AnyAsync(t => t.Id == request.TaskId, cancellationToken);
            if (!taskExists)
                throw new KeyNotFoundException($"Task with Id {request.TaskId} not found");

            var runningTaskTime = await db.TaskTimes.AnyAsync(t => t.TaskItemId == request.TaskId, cancellationToken);

            if (runningTaskTime) throw new Exception("There is already a running task time");

            var taskTime = new TaskTime { TaskItemId = request.TaskId, Start = DateTime.UtcNow };
            await db.TaskTimes.AddAsync(taskTime, cancellationToken);

            await db.SaveChangesAsync(cancellationToken);
            return new Response<TaskTimeDetailsResponse>(taskTime.ToDetailsResponse());
        }
    }
}