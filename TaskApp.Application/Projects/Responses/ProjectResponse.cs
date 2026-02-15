using TaskApp.Domain.Entities;

namespace TaskApp.Application.Projects.Responses
{
    public class ProjectResponse
    {
        public Guid Id {get; set;}
        public string Title {get; set;}
        public string Description {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}

    }

    public static class ProjectResponseExtension
    {
        public static ProjectResponse ToResponse(this Project project)
        {
            return new ProjectResponse
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatedAt
            };
        }
    }


}