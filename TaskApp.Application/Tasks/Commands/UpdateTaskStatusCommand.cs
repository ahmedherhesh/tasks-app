using MediatR;
using TaskApp.Infrastructure.Persistence;
using TaskApp.Domain.Entities;
using TaskApp.Application.Tasks.Responses;
using Microsoft.EntityFrameworkCore;
using TaskApp.Application.Shared.Responses;
using TaskApp.Domain.Enums;
using FluentValidation;

namespace TaskApp.Application.Tasks.Commands
{
    public class UpdateTaskStatusCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public TaskItemStatus Status { get; set; }
    }

    public class UpdateTaskStatusCommandValidator : AbstractValidator<UpdateTaskStatusCommand>
    {
        public UpdateTaskStatusCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.Status)
            .NotEqual(TaskItemStatus.Pending)
            .WithMessage("Status cannot be pending")
            .IsInEnum()
            .WithMessage("Invalid status");
        }
    }

    public class UpdateTaskStatusCommandHandler(ApplicationDbContext db) : IRequestHandler<UpdateTaskStatusCommand, Response<bool>>
    {

        public async Task<Response<bool>> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
        {
            var task = await db.Tasks.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken) ?? throw new KeyNotFoundException($"Task with Id {request.Id} not found");
            task.Status = request.Status;

            if(request.Status == TaskItemStatus.Completed) 
            {
                var taskTimes = await db.TaskTimes.Where(t => t.TaskItemId == request.Id).ToListAsync(cancellationToken);
                foreach (var taskTime in taskTimes)
                {
                    taskTime.End = DateTime.UtcNow;
                }
            }
            await db.SaveChangesAsync(cancellationToken);

            return new Response<bool>(true, message: "Task status updated successfully");
        }
    }

}
