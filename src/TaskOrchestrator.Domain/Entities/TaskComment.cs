using TaskOrchestrator.Domain.Common;

namespace TaskOrchestrator.Domain.Entities;

public class TaskComment : BaseEntity
{
    public Guid TaskId { get; set; }
    public string Content { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    
    public WorkTask Task { get; set; } = null!;
}
