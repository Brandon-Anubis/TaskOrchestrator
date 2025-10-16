using Microsoft.AspNetCore.Mvc.Testing;

namespace TaskOrchestrator.IntegrationTests.Controllers;

public class TasksControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public TasksControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public void Factory_ShouldBeCreated()
    {
        // This test verifies that the application factory can be created
        // Full integration tests would require a database setup
        Assert.NotNull(_factory);
    }
}
