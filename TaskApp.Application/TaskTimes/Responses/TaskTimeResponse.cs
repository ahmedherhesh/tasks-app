using TaskApp.Domain.Entities;

namespace TaskApp.Application.TaskTimes.Responses
{
    public class TaskTimeResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public TimeSpan? Duration { get; set; }
    }

    public static class TaskTimeResponseExtenstion
    {
        public static TaskTimeResponse ToResponse(this TaskTime taskTime)
        {
            return new()
            {
                Id = taskTime.Id,
                Start = taskTime.Start,
                End = taskTime.End,
                Duration = taskTime.Duration,
            };
        }
    }
}