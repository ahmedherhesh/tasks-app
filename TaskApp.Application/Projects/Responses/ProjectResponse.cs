using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApp.Domain.Entities;

namespace TaskApp.Application.Projects.Responses
{
    public class ProjectResponse
    {
        public Guid Id;
        public string Title = string.Empty;
        public string Description = string.Empty;
        public DateTime CreatedAt;
        public DateTime UpdatedAt;

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