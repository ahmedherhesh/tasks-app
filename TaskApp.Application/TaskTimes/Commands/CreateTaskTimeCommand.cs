using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskApp.Application.Shared.Responses;
using TaskApp.Application.TaskTimes.Responses;
using TaskApp.Domain.Entities;
using TaskApp.Infrastructure.Persistence;

namespace TaskApp.Application.TaskTimes.Commands
{
    public class CreateTaskTimeCommand : IRequest<Response<TaskTimeDetailsResponse>>
    {
        public Guid TaskId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string? Notes { get; set; }
    }

    public class CreateTaskTimeCommandValidator : AbstractValidator<CreateTaskTimeCommand>
    {
        public CreateTaskTimeCommandValidator()
        {
            RuleFor(x => x.TaskId).NotEmpty().WithMessage("TaskId is required");
            RuleFor(x => x.Start).NotEmpty().WithMessage("Start is required");
        }
    }

    public class CreateTaskTimeCommandHandler(ApplicationDbContext db) : IRequestHandler<CreateTaskTimeCommand, Response<TaskTimeDetailsResponse>>
    {
        public async Task<Response<TaskTimeDetailsResponse>> Handle(CreateTaskTimeCommand request, CancellationToken cancellationToken)
        {
            var task = await db.Tasks.FirstOrDefaultAsync(t => t.Id == request.TaskId, cancellationToken) ?? throw new KeyNotFoundException($"Task with Id {request.TaskId} not found");
            
            var taskTime = new TaskTime { TaskItemId = request.TaskId, Start = request.Start };
            db.TaskTimes.Add(taskTime);
            await db.SaveChangesAsync(cancellationToken);
            return new Response<TaskTimeDetailsResponse>(taskTime.ToDetailsResponse());
        }
    }
}