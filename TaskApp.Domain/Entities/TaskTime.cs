namespace TaskApp.Domain.Entities
{
    public class TaskTime
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TaskItemId { get; set; }
        public ApplicationUser CreatedBy { get; set; } = null!;
        public TaskItem? TaskItem { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public TimeSpan? Duration => (End ?? DateTime.UtcNow) - Start;
        public string? Notes { get; set; }
    }
}