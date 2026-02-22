namespace TaskApp.Domain.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; } = null!;
        public List<TaskItem> Tasks { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}