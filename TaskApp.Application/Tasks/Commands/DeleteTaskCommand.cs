using MediatR;
using TaskApp.Infrastructure.Persistence;
using TaskApp.Domain.Entities;
using TaskApp.Application.Tasks.Responses;
using Microsoft.EntityFrameworkCore;
using TaskApp.Application.Shared.Responses;

namespace TaskApp.Application.Tasks.Commands
{
    public class DeleteTaskCommand() : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteTaskCommandHandler(ApplicationDbContext db) : IRequestHandler<DeleteTaskCommand, Response<bool>>
    {

        public async Task<Response<bool>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await db.Tasks.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken) ?? throw new KeyNotFoundException($"Task with Id {request.Id} not found");
            db.Tasks.Remove(task);
            await db.SaveChangesAsync(cancellationToken);

            return new Response<bool>(true);
        }
    }

}
