using Microsoft.EntityFrameworkCore;
using TaskOrchestrator.Application.Interfaces;
using TaskOrchestrator.Application.Services;
using TaskOrchestrator.Infrastructure.Data;
using TaskOrchestrator.Infrastructure.Repositories;
using TaskOrchestrator.API.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? "Server=(localdb)\\mssqllocaldb;Database=TaskOrchestratorDb;Trusted_Connection=true;MultipleActiveResultSets=true",
        b => b.MigrationsAssembly("TaskOrchestrator.Infrastructure")));

// Register Repositories and Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Register Application Services
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectService, ProjectService>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(TaskOrchestrator.Application.Mappings.MappingProfile));

// Add SignalR
builder.Services.AddSignalR();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "Task Orchestrator API", 
        Version = "v1",
        Description = "A production-grade Real-Time Enterprise Task Orchestrator API"
    });
});

// Add Health Checks
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Orchestrator API v1"));
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.MapHub<TaskHub>("/hubs/tasks");
app.MapHealthChecks("/health");

app.Run();

// Make Program class accessible to integration tests
public partial class Program { }

