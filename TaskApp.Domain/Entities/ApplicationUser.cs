using Microsoft.AspNetCore.Identity;
using TaskApp.Domain.Enums;

namespace TaskApp.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public UserType UserType { get; set; } = UserType.User;
        public virtual ICollection<Project> Projects { get; set; } = [];
        public virtual ICollection<TaskItem> Tasks { get; set; } = [];
        public virtual ICollection<TaskTime> TaskTimes { get; set; } = [];
    }
}