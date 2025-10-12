using TaskOrchestrator.Domain.Common;

namespace TaskOrchestrator.Domain.Entities;

public class Project : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; } = true;
    public Guid OwnerId { get; set; }
    
    public User Owner { get; set; } = null!;
    public ICollection<WorkTask> Tasks { get; set; } = new List<WorkTask>();
    public ICollection<Team> Teams { get; set; } = new List<Team>();
}
