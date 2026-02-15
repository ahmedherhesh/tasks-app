using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskApp.Application.Projects.Responses;
using TaskApp.Application.Shared.Responses;
using TaskApp.Domain.Entities;
using TaskApp.Infrastructure.Persistence;

namespace TaskApp.Application.Projects.Commands
{
    public class UpdateProjectCommand : IRequest<Response<ProjectResponse>>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.").MaximumLength(2000);
        }
    }

    public class UpdateProjectCommandHandler(ApplicationDbContext db) : IRequestHandler<UpdateProjectCommand, Response<ProjectResponse>>
    {
        public async Task<Response<ProjectResponse>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await db.Projects.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken) ?? throw new KeyNotFoundException($"Project with Id {request.Id} not found");

            project.Title = request.Title;
            project.Description = request.Description;

            await db.SaveChangesAsync(cancellationToken);
            return new Response<ProjectResponse>(project.ToResponse());
        }
    }
}