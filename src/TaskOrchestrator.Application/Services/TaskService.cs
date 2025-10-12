using AutoMapper;
using TaskOrchestrator.Application.DTOs;
using TaskOrchestrator.Application.Interfaces;
using TaskOrchestrator.Domain.Entities;

namespace TaskOrchestrator.Application.Services;

public class TaskService : ITaskService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TaskService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
    {
        var tasks = await _unitOfWork.Repository<WorkTask>().GetAllAsync();
        return _mapper.Map<IEnumerable<TaskDto>>(tasks);
    }

    public async Task<TaskDto?> GetTaskByIdAsync(Guid id)
    {
        var task = await _unitOfWork.Repository<WorkTask>().GetByIdAsync(id);
        return task != null ? _mapper.Map<TaskDto>(task) : null;
    }

    public async Task<TaskDto> CreateTaskAsync(CreateTaskDto taskDto)
    {
        var task = _mapper.Map<WorkTask>(taskDto);
        await _unitOfWork.Repository<WorkTask>().AddAsync(task);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<TaskDto>(task);
    }

    public async Task<TaskDto?> UpdateTaskAsync(Guid id, UpdateTaskDto taskDto)
    {
        var task = await _unitOfWork.Repository<WorkTask>().GetByIdAsync(id);
        if (task == null) return null;

        _mapper.Map(taskDto, task);
        task.UpdatedAt = DateTime.UtcNow;
        await _unitOfWork.Repository<WorkTask>().UpdateAsync(task);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<TaskDto>(task);
    }

    public async Task<bool> DeleteTaskAsync(Guid id)
    {
        var task = await _unitOfWork.Repository<WorkTask>().GetByIdAsync(id);
        if (task == null) return false;

        await _unitOfWork.Repository<WorkTask>().DeleteAsync(task);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<TaskDto>> GetTasksByUserIdAsync(Guid userId)
    {
        var tasks = await _unitOfWork.Repository<WorkTask>()
            .FindAsync(t => t.AssignedToId == userId);
        return _mapper.Map<IEnumerable<TaskDto>>(tasks);
    }

    public async Task<IEnumerable<TaskDto>> GetTasksByProjectIdAsync(Guid projectId)
    {
        var tasks = await _unitOfWork.Repository<WorkTask>()
            .FindAsync(t => t.ProjectId == projectId);
        return _mapper.Map<IEnumerable<TaskDto>>(tasks);
    }
}
