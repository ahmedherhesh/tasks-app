using TaskApp.Domain.Entities;

namespace TaskApp.Application.Projects.Responses
{
    public class ProjectDetailsResponse : ProjectResponse
    {
    }

    public static class ProjectDetailsResponseExtension
    {
        public static ProjectDetailsResponse ToDetailsResponse(this Project project)
        {
            return new ProjectDetailsResponse
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