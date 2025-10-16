using TaskOrchestrator.Application.DTOs;

namespace TaskOrchestrator.Application.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
    Task<ProjectDto?> GetProjectByIdAsync(Guid id);
    Task<ProjectDto> CreateProjectAsync(CreateProjectDto projectDto);
    Task<bool> DeleteProjectAsync(Guid id);
}
