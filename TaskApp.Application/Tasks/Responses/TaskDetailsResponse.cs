using System.Text.Json.Serialization;
using TaskApp.Domain.Entities;

namespace TaskApp.Application.Tasks.Responses
{
    public class TaskDetailsResponse : TaskResponse
    {
        [JsonPropertyOrder(6)]
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
                Status = task.Status,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt,
                TaskTimes = task.TaskTimes,
            };
        }
    }
}
