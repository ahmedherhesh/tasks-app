using MediatR;
using TaskApp.Infrastructure.Persistence;
using TaskApp.Domain.Entities;
using TaskApp.Application.Tasks.Responses;
using TaskApp.Application.Shared.Responses;
using FluentValidation;

namespace TaskApp.Application.Tasks.Commands
{
    public class CreateTaskCommand() : IRequest<Response<TaskResponse>>
    {
        public string Title { get; set; } = null!;
    }

    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
        }
    }
    public class CreateTaskCommandHandler(ApplicationDbContext db) : IRequestHandler<CreateTaskCommand, Response<TaskResponse>>
    {
        public async Task<Response<TaskResponse>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new TaskItem { Title = request.Title };
            db.Tasks.Add(task);
            await db.SaveChangesAsync(cancellationToken);

            return new Response<TaskResponse>(task.ToResponse());
        }
    }

}
