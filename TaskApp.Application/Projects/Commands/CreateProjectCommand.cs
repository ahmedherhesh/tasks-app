using FluentValidation;
using MediatR;
using TaskApp.Application.Projects.Responses;
using TaskApp.Application.Shared.Responses;
using TaskApp.Domain.Entities;
using TaskApp.Infrastructure.Persistence;

namespace TaskApp.Application.Projects.Commands
{
    public class CreateProjectCommand : IRequest<Response<ProjectResponse>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.").MaximumLength(2000);
        }
    }

    public class CreateProjectCommandHandler(ApplicationDbContext db) : IRequestHandler<CreateProjectCommand, Response<ProjectResponse>>
    {
        public async Task<Response<ProjectResponse>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project { Title = request.Title, Description = request.Description };
            db.Projects.Add(project);
            await db.SaveChangesAsync(cancellationToken);
            return new Response<ProjectResponse>(project.ToResponse());
        }
    }
}