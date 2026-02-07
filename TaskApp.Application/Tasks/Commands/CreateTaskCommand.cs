using MediatR;
using TaskApp.Infrastructure.Persistence;
using TaskApp.Domain.Entities;
using TaskApp.Application.Tasks.Responses;
using TaskApp.Application.Shared.Responses;

namespace TaskApp.Application.Tasks.Commands
{
    public class CreateTaskCommand() : IRequest<Response<TaskResponse>>
    {
        public string Title { get; set; } = null!;
    }

    public class CreateTaskCommandHandler(ApplicationDbContext db) : IRequestHandler<CreateTaskCommand, Response<TaskResponse>>
    {
        private readonly ApplicationDbContext _db = db;


        public async Task<Response<TaskResponse>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new TaskItem { Title = request.Title };
            _db.Tasks.Add(task);
            await _db.SaveChangesAsync(cancellationToken);

            return new Response<TaskResponse>(task.ToResponse());
        }
    }

}
