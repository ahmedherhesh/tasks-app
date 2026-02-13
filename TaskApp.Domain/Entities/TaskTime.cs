using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApp.Domain.Entities
{
    public class TaskTime
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TaskItemId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public TimeSpan? Duration => (End ?? DateTime.UtcNow) - Start;
        public string? Notes { get; set; }
        public TaskItem? TaskItem { get; set; }
    }
}