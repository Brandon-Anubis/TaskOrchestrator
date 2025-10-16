using Microsoft.AspNetCore.SignalR;

namespace TaskOrchestrator.API.Hubs;

public class TaskHub : Hub
{
    public async Task SendTaskUpdate(string message)
    {
        await Clients.All.SendAsync("ReceiveTaskUpdate", message);
    }

    public async Task NotifyTaskCreated(Guid taskId, string title)
    {
        await Clients.All.SendAsync("TaskCreated", new { TaskId = taskId, Title = title });
    }

    public async Task NotifyTaskUpdated(Guid taskId, string title, string status)
    {
        await Clients.All.SendAsync("TaskUpdated", new { TaskId = taskId, Title = title, Status = status });
    }

    public async Task NotifyTaskDeleted(Guid taskId)
    {
        await Clients.All.SendAsync("TaskDeleted", new { TaskId = taskId });
    }
}
