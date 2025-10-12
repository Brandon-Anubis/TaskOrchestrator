using AutoMapper;
using FluentAssertions;
using Moq;
using TaskOrchestrator.Application.DTOs;
using TaskOrchestrator.Application.Interfaces;
using TaskOrchestrator.Application.Services;
using TaskOrchestrator.Domain.Entities;
using TaskOrchestrator.Domain.Enums;

namespace TaskOrchestrator.UnitTests.Services;

public class TaskServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly TaskService _taskService;

    public TaskServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _taskService = new TaskService(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetAllTasksAsync_ReturnsAllTasks()
    {
        // Arrange
        var tasks = new List<WorkTask>
        {
            new WorkTask { Id = Guid.NewGuid(), Title = "Task 1" },
            new WorkTask { Id = Guid.NewGuid(), Title = "Task 2" }
        };
        var taskDtos = new List<TaskDto>
        {
            new TaskDto { Id = tasks[0].Id, Title = "Task 1" },
            new TaskDto { Id = tasks[1].Id, Title = "Task 2" }
        };

        var repositoryMock = new Mock<IRepository<WorkTask>>();
        repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(tasks);
        _unitOfWorkMock.Setup(u => u.Repository<WorkTask>()).Returns(repositoryMock.Object);
        _mapperMock.Setup(m => m.Map<IEnumerable<TaskDto>>(tasks)).Returns(taskDtos);

        // Act
        var result = await _taskService.GetAllTasksAsync();

        // Assert
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(taskDtos);
    }

    [Fact]
    public async Task GetTaskByIdAsync_WithValidId_ReturnsTask()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var task = new WorkTask { Id = taskId, Title = "Test Task" };
        var taskDto = new TaskDto { Id = taskId, Title = "Test Task" };

        var repositoryMock = new Mock<IRepository<WorkTask>>();
        repositoryMock.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync(task);
        _unitOfWorkMock.Setup(u => u.Repository<WorkTask>()).Returns(repositoryMock.Object);
        _mapperMock.Setup(m => m.Map<TaskDto>(task)).Returns(taskDto);

        // Act
        var result = await _taskService.GetTaskByIdAsync(taskId);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(taskId);
        result.Title.Should().Be("Test Task");
    }

    [Fact]
    public async Task CreateTaskAsync_CreatesAndReturnsTask()
    {
        // Arrange
        var createDto = new CreateTaskDto { Title = "New Task", Description = "Description" };
        var task = new WorkTask { Id = Guid.NewGuid(), Title = "New Task", Description = "Description" };
        var taskDto = new TaskDto { Id = task.Id, Title = "New Task", Description = "Description" };

        var repositoryMock = new Mock<IRepository<WorkTask>>();
        repositoryMock.Setup(r => r.AddAsync(It.IsAny<WorkTask>())).ReturnsAsync(task);
        _unitOfWorkMock.Setup(u => u.Repository<WorkTask>()).Returns(repositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);
        _mapperMock.Setup(m => m.Map<WorkTask>(createDto)).Returns(task);
        _mapperMock.Setup(m => m.Map<TaskDto>(task)).Returns(taskDto);

        // Act
        var result = await _taskService.CreateTaskAsync(createDto);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be("New Task");
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteTaskAsync_WithValidId_ReturnsTrue()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var task = new WorkTask { Id = taskId, Title = "Task to Delete" };

        var repositoryMock = new Mock<IRepository<WorkTask>>();
        repositoryMock.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync(task);
        repositoryMock.Setup(r => r.DeleteAsync(task)).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.Repository<WorkTask>()).Returns(repositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _taskService.DeleteTaskAsync(taskId);

        // Assert
        result.Should().BeTrue();
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteTaskAsync_WithInvalidId_ReturnsFalse()
    {
        // Arrange
        var taskId = Guid.NewGuid();

        var repositoryMock = new Mock<IRepository<WorkTask>>();
        repositoryMock.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync((WorkTask?)null);
        _unitOfWorkMock.Setup(u => u.Repository<WorkTask>()).Returns(repositoryMock.Object);

        // Act
        var result = await _taskService.DeleteTaskAsync(taskId);

        // Assert
        result.Should().BeFalse();
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
    }
}
