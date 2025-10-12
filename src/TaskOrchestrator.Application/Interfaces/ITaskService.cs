using TaskOrchestrator.Application.DTOs;

namespace TaskOrchestrator.Application.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetAllTasksAsync();
    Task<TaskDto?> GetTaskByIdAsync(Guid id);
    Task<TaskDto> CreateTaskAsync(CreateTaskDto taskDto);
    Task<TaskDto?> UpdateTaskAsync(Guid id, UpdateTaskDto taskDto);
    Task<bool> DeleteTaskAsync(Guid id);
    Task<IEnumerable<TaskDto>> GetTasksByUserIdAsync(Guid userId);
    Task<IEnumerable<TaskDto>> GetTasksByProjectIdAsync(Guid projectId);
}
