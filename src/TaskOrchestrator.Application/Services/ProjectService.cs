using AutoMapper;
using TaskOrchestrator.Application.DTOs;
using TaskOrchestrator.Application.Interfaces;
using TaskOrchestrator.Domain.Entities;

namespace TaskOrchestrator.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
    {
        var projects = await _unitOfWork.Repository<Project>().GetAllAsync();
        return _mapper.Map<IEnumerable<ProjectDto>>(projects);
    }

    public async Task<ProjectDto?> GetProjectByIdAsync(Guid id)
    {
        var project = await _unitOfWork.Repository<Project>().GetByIdAsync(id);
        return project != null ? _mapper.Map<ProjectDto>(project) : null;
    }

    public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto projectDto)
    {
        var project = _mapper.Map<Project>(projectDto);
        await _unitOfWork.Repository<Project>().AddAsync(project);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ProjectDto>(project);
    }

    public async Task<bool> DeleteProjectAsync(Guid id)
    {
        var project = await _unitOfWork.Repository<Project>().GetByIdAsync(id);
        if (project == null) return false;

        await _unitOfWork.Repository<Project>().DeleteAsync(project);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
