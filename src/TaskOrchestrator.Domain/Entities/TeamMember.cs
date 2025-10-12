using TaskOrchestrator.Domain.Common;

namespace TaskOrchestrator.Domain.Entities;

public class TeamMember : BaseEntity
{
    public Guid TeamId { get; set; }
    public Guid UserId { get; set; }
    public string Role { get; set; } = "Member";
    
    public Team Team { get; set; } = null!;
    public User User { get; set; } = null!;
}
