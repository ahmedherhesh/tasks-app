using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskApp.Application.Projects.Responses;
using TaskApp.Application.Shared.Responses;
using TaskApp.Domain.Entities;
using TaskApp.Infrastructure.Persistence;

namespace TaskApp.Application.Projects.Commands
{
    public class DeleteProjectCommand : IRequest<Response<ProjectResponse>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
    {
        public DeleteProjectCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        }
    }

    public class DeleteProjectCommandHandler(ApplicationDbContext db) : IRequestHandler<DeleteProjectCommand, Response<ProjectResponse>>
    {
        public async Task<Response<ProjectResponse>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await db.Projects.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken) ?? throw new KeyNotFoundException($"Project with Id {request.Id} not found");

            db.Projects.Remove(project);

            await db.SaveChangesAsync(cancellationToken);
            return new Response<ProjectResponse>(project.ToResponse());
        }
    }
}