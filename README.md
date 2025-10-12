# Task Orchestrator

A production-grade Real-Time Enterprise Task Orchestrator built with ASP.NET Core 8.

## Overview

Task Orchestrator is a comprehensive task management system that provides real-time updates, RESTful APIs, and a clean architecture design. It enables teams to efficiently manage tasks, projects, and team collaborations with real-time notifications.

## Features

- **Task Management**: Create, update, delete, and track tasks with priorities and statuses
- **User Management**: Manage users and their assignments
- **Project Management**: Organize tasks within projects
- **Team Collaboration**: Support for teams and team members
- **Real-Time Updates**: SignalR-based real-time notifications for task changes
- **RESTful API**: Well-documented API endpoints with Swagger/OpenAPI
- **Clean Architecture**: Separation of concerns with Domain, Application, Infrastructure, and API layers
- **Entity Framework Core**: Database abstraction with repository pattern and Unit of Work
- **Health Checks**: Built-in health check endpoints for monitoring
- **Comprehensive Testing**: Unit and integration tests included

## Architecture

The project follows Clean Architecture principles:

```
TaskOrchestrator/
├── src/
│   ├── TaskOrchestrator.Domain/         # Domain entities and enums
│   ├── TaskOrchestrator.Application/    # Business logic, services, DTOs
│   ├── TaskOrchestrator.Infrastructure/ # Data access, repositories
│   └── TaskOrchestrator.API/            # Web API, controllers, SignalR hubs
└── tests/
    ├── TaskOrchestrator.UnitTests/      # Unit tests
    └── TaskOrchestrator.IntegrationTests/ # Integration tests
```

## Technologies

- **Framework**: .NET 8
- **Database**: SQL Server (Entity Framework Core)
- **Real-Time**: SignalR
- **API Documentation**: Swagger/OpenAPI
- **Mapping**: AutoMapper
- **Testing**: xUnit, Moq, FluentAssertions
- **Authentication**: JWT (ready for implementation)

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB or full instance)

### Installation

1. Clone the repository:
```bash
git clone https://github.com/Brandon-Anubis/TaskOrchestrator.git
cd TaskOrchestrator
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Update the connection string in `src/TaskOrchestrator.API/appsettings.json` if needed.

4. Build the solution:
```bash
dotnet build
```

5. Run the tests:
```bash
dotnet test
```

6. Run the API:
```bash
cd src/TaskOrchestrator.API
dotnet run
```

The API will be available at `https://localhost:7xxx` and `http://localhost:5xxx`.

### Database Setup

The application will create the database automatically on first run. If you need to manually manage migrations:

```bash
# Add a migration
dotnet ef migrations add InitialCreate --project src/TaskOrchestrator.Infrastructure --startup-project src/TaskOrchestrator.API

# Update database
dotnet ef database update --project src/TaskOrchestrator.Infrastructure --startup-project src/TaskOrchestrator.API
```

## API Endpoints

### Tasks
- `GET /api/tasks` - Get all tasks
- `GET /api/tasks/{id}` - Get task by ID
- `POST /api/tasks` - Create new task
- `PUT /api/tasks/{id}` - Update task
- `DELETE /api/tasks/{id}` - Delete task
- `GET /api/tasks/user/{userId}` - Get tasks by user
- `GET /api/tasks/project/{projectId}` - Get tasks by project

### Users
- `GET /api/users` - Get all users
- `GET /api/users/{id}` - Get user by ID
- `POST /api/users` - Create new user
- `DELETE /api/users/{id}` - Delete user

### Projects
- `GET /api/projects` - Get all projects
- `GET /api/projects/{id}` - Get project by ID
- `POST /api/projects` - Create new project
- `DELETE /api/projects/{id}` - Delete project

### SignalR Hub
- `/hubs/tasks` - Real-time task notifications

## Real-Time Events

The SignalR hub broadcasts the following events:
- `TaskCreated` - When a new task is created
- `TaskUpdated` - When a task is updated
- `TaskDeleted` - When a task is deleted

## Health Checks

- `GET /health` - Application health status

## API Documentation

Swagger UI is available at `/swagger` when running in development mode.

## Testing

Run all tests:
```bash
dotnet test
```

Run specific test project:
```bash
dotnet test tests/TaskOrchestrator.UnitTests
dotnet test tests/TaskOrchestrator.IntegrationTests
```

## Domain Models

### Task
- Title, Description
- Status (Pending, InProgress, Completed, Cancelled, OnHold)
- Priority (Low, Medium, High, Critical)
- Due Date
- Assignment to User
- Project association

### User
- UserName, Email, Full Name
- Department
- Active status

### Project
- Name, Description
- Start/End dates
- Owner
- Associated tasks and teams

### Team
- Name, Description
- Project association
- Team members

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the MIT License.

## Contact

For questions or support, please open an issue on GitHub.
