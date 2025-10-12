# Contributing to Task Orchestrator

Thank you for your interest in contributing to Task Orchestrator! This document provides guidelines for contributing to the project.

## Commit Message Guidelines

We follow the [Conventional Commits](https://www.conventionalcommits.org/) specification for all commit messages. This helps maintain a clear and structured commit history.

### Commit Format

Each commit message should follow this format:

```
<type>: <description>

[optional body]

[optional footer]
```

### Commit Types

- **feat**: A new feature for the user or application
  - Example: `feat: add user authentication endpoint`
  - Example: `feat: implement SignalR hub for real-time task updates`

- **fix**: A bug fix
  - Example: `fix: resolve null reference in TaskService`
  - Example: `fix: correct database connection string handling`

- **refactor**: Code changes that neither fix a bug nor add a feature
  - Example: `refactor: extract task validation logic into separate method`
  - Example: `refactor: simplify repository pattern implementation`

- **test**: Adding or modifying tests
  - Example: `test: add unit tests for TaskService`
  - Example: `test: add integration tests for Projects API`

- **docs**: Documentation changes
  - Example: `docs: update API endpoint documentation`
  - Example: `docs: add architecture diagram to README`

- **chore**: Changes to build process, tooling, or dependencies
  - Example: `chore: update Entity Framework Core to 8.0.1`
  - Example: `chore: configure Docker for development`

- **style**: Code style changes (formatting, missing semi-colons, etc.)
  - Example: `style: apply consistent indentation`

- **perf**: Performance improvements
  - Example: `perf: optimize database query in GetAllTasksAsync`

- **ci**: Changes to CI/CD configuration
  - Example: `ci: add GitHub Actions workflow for automated testing`

### Atomic Commits

Each commit should be **atomic** - it should represent a single logical change that:

1. **Is independently buildable**: The codebase should build successfully after the commit
2. **Is independently testable**: All tests should pass after the commit
3. **Has a single purpose**: Addresses one specific concern or feature
4. **Is reversible**: Can be reverted without affecting other changes

### Examples of Atomic Commits

#### Good Examples

```
feat: add Project entity and repository

- Create Project domain model
- Add IProjectRepository interface
- Implement ProjectRepository
- Add database configuration for Projects table
```

```
test: add unit tests for ProjectService

- Test CreateProject method
- Test GetAllProjects method
- Test GetProjectById method
- Test UpdateProject method
- Test DeleteProject method
```

```
refactor: extract validation logic from controllers

- Create ValidationHelper class
- Move task validation to helper
- Move user validation to helper
- Update controllers to use ValidationHelper
```

#### Bad Examples (Non-Atomic)

```
feat: add multiple features and fix bugs

- Add Project entity
- Fix TaskService bug
- Update documentation
- Refactor controller code
```

This commit should be split into:
- `feat: add Project entity and repository`
- `fix: resolve null reference in TaskService`
- `docs: update API documentation`
- `refactor: simplify controller implementation`

### Commit Organization Workflow

When working on a feature, organize your commits logically:

1. **Domain Layer First**: Start with domain models and enums
   ```
   feat: add Task domain model and enums
   ```

2. **Application Layer**: Add interfaces and services
   ```
   feat: add ITaskService interface and implementation
   ```

3. **Infrastructure Layer**: Add data access and repositories
   ```
   feat: add Task repository and database configuration
   ```

4. **API Layer**: Add controllers and endpoints
   ```
   feat: add TasksController with CRUD endpoints
   ```

5. **Tests**: Add tests for the new feature
   ```
   test: add unit tests for TaskService
   test: add integration tests for Tasks API
   ```

6. **Documentation**: Update documentation
   ```
   docs: add Tasks API documentation to README
   ```

### Commit Author

All commits should be authored by the contributor making the changes. Configure your git identity:

```bash
git config user.name "Your Name"
git config user.email "your.email@example.com"
```

For this project, core commits are authored by:
```bash
git config user.name "Brandon-Anubis"
git config user.email "brandon@ankhstudio.com"
```

## Pull Request Guidelines

1. **Fork the repository** and create a feature branch from `main`
2. **Follow the commit guidelines** outlined above
3. **Ensure all tests pass**: Run `dotnet test` before submitting
4. **Ensure the build succeeds**: Run `dotnet build` before submitting
5. **Write meaningful PR descriptions**: Explain what changes were made and why
6. **Reference related issues**: Use `Closes #123` or `Fixes #123` in the PR description

## Development Workflow

### Setting Up the Development Environment

1. Clone the repository:
   ```bash
   git clone https://github.com/Brandon-Anubis/TaskOrchestrator.git
   cd TaskOrchestrator
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the solution:
   ```bash
   dotnet build
   ```

4. Run tests:
   ```bash
   dotnet test
   ```

### Making Changes

1. Create a feature branch:
   ```bash
   git checkout -b feat/your-feature-name
   ```

2. Make your changes following the atomic commit guidelines

3. Build and test frequently:
   ```bash
   dotnet build
   dotnet test
   ```

4. Commit your changes with conventional commit messages:
   ```bash
   git add .
   git commit -m "feat: add your feature description"
   ```

5. Push to your fork:
   ```bash
   git push origin feat/your-feature-name
   ```

6. Create a Pull Request

### Code Style

- Follow C# coding conventions and best practices
- Use meaningful variable and method names
- Add XML documentation comments for public APIs
- Keep methods focused and concise
- Use LINQ where appropriate
- Follow SOLID principles

### Testing

- Write unit tests for all services and business logic
- Write integration tests for API endpoints
- Aim for high code coverage
- Use meaningful test names that describe what is being tested
- Follow the Arrange-Act-Assert pattern

## Questions or Issues?

If you have questions or run into issues, please:
1. Check existing issues on GitHub
2. Open a new issue if needed
3. Join discussions in pull requests

Thank you for contributing to Task Orchestrator!
