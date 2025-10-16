using AutoMapper;
using TaskOrchestrator.Application.DTOs;
using TaskOrchestrator.Domain.Entities;

namespace TaskOrchestrator.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<WorkTask, TaskDto>().ReverseMap();
        CreateMap<CreateTaskDto, WorkTask>();
        CreateMap<UpdateTaskDto, WorkTask>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<CreateUserDto, User>();
        
        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<CreateProjectDto, Project>();
    }
}
