using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TaskOrchestrator.Application.DTOs;
using TaskOrchestrator.Application.Interfaces;
using TaskOrchestrator.API.Hubs;

namespace TaskOrchestrator.API.Controllers;

/// <summary>
/// Controller for managing tasks
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly IHubContext<TaskHub> _hubContext;
    private readonly ILogger<TasksController> _logger;

    /// <summary>
    /// Initializes a new instance of the TasksController
    /// </summary>
    public TasksController(ITaskService taskService, IHubContext<TaskHub> hubContext, ILogger<TasksController> logger)
    {
        _taskService = taskService;
        _hubContext = hubContext;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves all tasks
    /// </summary>
    /// <returns>A list of all tasks</returns>
    /// <response code="200">Returns the list of tasks</response>
    /// <response code="500">If an error occurred while retrieving tasks</response>
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

    /// <summary>
    /// Retrieves a specific task by ID
    /// </summary>
    /// <param name="id">The ID of the task to retrieve</param>
    /// <returns>The requested task</returns>
    /// <response code="200">Returns the requested task</response>
    /// <response code="404">If the task is not found</response>
    /// <response code="500">If an error occurred while retrieving the task</response>
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

    /// <summary>
    /// Creates a new task
    /// </summary>
    /// <param name="taskDto">The task data to create</param>
    /// <returns>The created task</returns>
    /// <response code="201">Returns the newly created task</response>
    /// <response code="500">If an error occurred while creating the task</response>
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

    /// <summary>
    /// Updates an existing task
    /// </summary>
    /// <param name="id">The ID of the task to update</param>
    /// <param name="taskDto">The updated task data</param>
    /// <returns>The updated task</returns>
    /// <response code="200">Returns the updated task</response>
    /// <response code="404">If the task is not found</response>
    /// <response code="500">If an error occurred while updating the task</response>
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

    /// <summary>
    /// Deletes a task
    /// </summary>
    /// <param name="id">The ID of the task to delete</param>
    /// <returns>No content</returns>
    /// <response code="204">If the task was successfully deleted</response>
    /// <response code="404">If the task is not found</response>
    /// <response code="500">If an error occurred while deleting the task</response>
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

    /// <summary>
    /// Retrieves all tasks assigned to a specific user
    /// </summary>
    /// <param name="userId">The ID of the user</param>
    /// <returns>A list of tasks assigned to the user</returns>
    /// <response code="200">Returns the list of tasks</response>
    /// <response code="500">If an error occurred while retrieving tasks</response>
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

    /// <summary>
    /// Retrieves all tasks associated with a specific project
    /// </summary>
    /// <param name="projectId">The ID of the project</param>
    /// <returns>A list of tasks in the project</returns>
    /// <response code="200">Returns the list of tasks</response>
    /// <response code="500">If an error occurred while retrieving tasks</response>
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
