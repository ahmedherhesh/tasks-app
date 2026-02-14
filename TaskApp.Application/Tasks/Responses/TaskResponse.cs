using TaskApp.Domain.Entities;
using TaskApp.Domain.Enums;

namespace TaskApp.Application.Tasks.Responses
{
    public class TaskResponse
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public TaskItemStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public static class TaskResponseExtension
    {
        public static TaskResponse ToResponse(this TaskItem task)
        {
            return new TaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Status = task.Status,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            };
        }
    }
}
