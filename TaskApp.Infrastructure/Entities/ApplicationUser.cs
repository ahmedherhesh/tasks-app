using Microsoft.AspNetCore.Identity;
using TaskApp.Domain.Entities;
using TaskApp.Infrastructure.Enums;

namespace TaskApp.Infrastructure.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public UserType UserType { get; set; } = UserType.User;
        public virtual ICollection<Project> Projects { get; set; } = [];
    }
}