using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TaskOrchestrator.Application.DTOs;
using TaskOrchestrator.Application.Interfaces;
using TaskOrchestrator.API.Hubs;

namespace TaskOrchestrator.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly IHubContext<TaskHub> _hubContext;
    private readonly ILogger<TasksController> _logger;

    public TasksController(ITaskService taskService, IHubContext<TaskHub> hubContext, ILogger<TasksController> logger)
    {
        _taskService = taskService;
        _hubContext = hubContext;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetAllTasks()
    {
        try
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all tasks");
            return StatusCode(500, "An error occurred while retrieving tasks");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskDto>> GetTaskById(Guid id)
    {
        try
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting task {TaskId}", id);
            return StatusCode(500, "An error occurred while retrieving the task");
        }
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> CreateTask(CreateTaskDto taskDto)
    {
        try
        {
            var task = await _taskService.CreateTaskAsync(taskDto);
            await _hubContext.Clients.All.SendAsync("TaskCreated", new { task.Id, task.Title });
            
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating task");
            return StatusCode(500, "An error occurred while creating the task");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TaskDto>> UpdateTask(Guid id, UpdateTaskDto taskDto)
    {
        try
        {
            var task = await _taskService.UpdateTaskAsync(id, taskDto);
            if (task == null)
                return NotFound();

            await _hubContext.Clients.All.SendAsync("TaskUpdated", new { task.Id, task.Title, Status = task.Status.ToString() });
            
            return Ok(task);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating task {TaskId}", id);
            return StatusCode(500, "An error occurred while updating the task");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTask(Guid id)
    {
        try
        {
            var result = await _taskService.DeleteTaskAsync(id);
            if (!result)
                return NotFound();

            await _hubContext.Clients.All.SendAsync("TaskDeleted", new { Id = id });
            
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting task {TaskId}", id);
            return StatusCode(500, "An error occurred while deleting the task");
        }
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksByUserId(Guid userId)
    {
        try
        {
            var tasks = await _taskService.GetTasksByUserIdAsync(userId);
            return Ok(tasks);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting tasks for user {UserId}", userId);
            return StatusCode(500, "An error occurred while retrieving tasks");
        }
    }

    [HttpGet("project/{projectId}")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksByProjectId(Guid projectId)
    {
        try
        {
            var tasks = await _taskService.GetTasksByProjectIdAsync(projectId);
            return Ok(tasks);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting tasks for project {ProjectId}", projectId);
            return StatusCode(500, "An error occurred while retrieving tasks");
        }
    }
}
