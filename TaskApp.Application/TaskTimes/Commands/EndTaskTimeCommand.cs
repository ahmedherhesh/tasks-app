using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskApp.Application.Shared.Responses;
using TaskApp.Application.TaskTimes.Responses;
using TaskApp.Domain.Entities;
using TaskApp.Infrastructure.Persistence;

namespace TaskApp.Application.TaskTimes.Commands
{
    public class EndTaskTimeCommand : IRequest<Response<TaskTimeDetailsResponse>>
    {
        public Guid TaskId { get; set; }
    }

    public class EndTaskTimeCommandValidator : AbstractValidator<EndTaskTimeCommand>
    {
        public EndTaskTimeCommandValidator()
        {
            RuleFor(x => x.TaskId).NotEmpty().WithMessage("TaskId is required");
        }
    }

    public class EndTaskTimeCommandHandler(ApplicationDbContext db) : IRequestHandler<EndTaskTimeCommand, Response<TaskTimeDetailsResponse>>
    {
        public async Task<Response<TaskTimeDetailsResponse>> Handle(EndTaskTimeCommand request, CancellationToken cancellationToken)
        {
            var taskExists = await db.Tasks.AnyAsync(t => t.Id == request.TaskId, cancellationToken);

            if (!taskExists)
                throw new KeyNotFoundException($"Task with Id {request.TaskId} not found");

            var runningTaskTime = await db.TaskTimes.FirstOrDefaultAsync(t => t.TaskItemId == request.TaskId && t.End == null, cancellationToken) ?? throw new Exception("There is no a running task time");

            runningTaskTime.End = DateTime.UtcNow;

            await db.SaveChangesAsync(cancellationToken);
            return new Response<TaskTimeDetailsResponse>(runningTaskTime.ToDetailsResponse());
        }
    }
}