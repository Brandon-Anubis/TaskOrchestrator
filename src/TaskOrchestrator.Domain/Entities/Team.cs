using TaskOrchestrator.Domain.Common;

namespace TaskOrchestrator.Domain.Entities;

public class Team : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid? ProjectId { get; set; }
    
    public Project? Project { get; set; }
    public ICollection<TeamMember> Members { get; set; } = new List<TeamMember>();
}
