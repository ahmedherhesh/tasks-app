using TaskApp.Domain.Entities;

namespace TaskApp.Application.Tasks.Responses
{
    public class TaskDetailsResponse : TaskResponse
    {
        public List<TaskTime> TaskTimes { get; set; } = [];
    }

    public static class TaskDetailsResponseExtension
    {
        public static TaskDetailsResponse ToDetailsResponse(this TaskItem task)
        {
            return new TaskDetailsResponse
            {
                Id = task.Id,
                Title = task.Title,
                IsCompleted = task.IsCompleted,
                TaskTimes = task.TaskTimes,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt,
            };
        }
    }
}
