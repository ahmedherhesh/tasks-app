using MediatR;
using TaskApp.Infrastructure.Persistence;
using TaskApp.Domain.Entities;
using TaskApp.Application.Tasks.Responses;
using TaskApp.Application.Shared.Responses;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace TaskApp.Application.Tasks.Commands
{
    public class CreateTaskCommand() : IRequest<Response<TaskResponse>>
    {
        public Guid ProjectId { get; set; }
        public string Title { get; set; } = null!;
    }

    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(x => x.ProjectId).NotEmpty().WithMessage("Project id is required.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
        }
    }
    public class CreateTaskCommandHandler(ApplicationDbContext db) : IRequestHandler<CreateTaskCommand, Response<TaskResponse>>
    {
        public async Task<Response<TaskResponse>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var project = await db.Projects.FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken) ?? throw new KeyNotFoundException($"Project with Id {request.ProjectId} not found");

            var task = new TaskItem { Title = request.Title };
            project.Tasks.Add(task);
            await db.SaveChangesAsync(cancellationToken);

            return new Response<TaskResponse>(task.ToResponse());
        }
    }
}
