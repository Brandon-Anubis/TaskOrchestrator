using TaskOrchestrator.Domain.Common;

namespace TaskOrchestrator.Domain.Entities;

public class User : BaseEntity
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? Department { get; set; }
    public bool IsActive { get; set; } = true;
    
    public ICollection<WorkTask> AssignedTasks { get; set; } = new List<WorkTask>();
    public ICollection<Project> OwnedProjects { get; set; } = new List<Project>();
    public ICollection<TeamMember> TeamMemberships { get; set; } = new List<TeamMember>();
}
