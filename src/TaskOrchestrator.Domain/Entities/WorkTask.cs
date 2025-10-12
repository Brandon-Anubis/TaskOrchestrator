using TaskOrchestrator.Domain.Common;
using TaskOrchestrator.Domain.Enums;

namespace TaskOrchestrator.Domain.Entities;

public class WorkTask : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Enums.TaskStatus Status { get; set; } = Enums.TaskStatus.Pending;
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public DateTime? DueDate { get; set; }
    public Guid? AssignedToId { get; set; }
    public Guid? ProjectId { get; set; }
    
    public User? AssignedTo { get; set; }
    public Project? Project { get; set; }
    public ICollection<TaskComment> Comments { get; set; } = new List<TaskComment>();
}
