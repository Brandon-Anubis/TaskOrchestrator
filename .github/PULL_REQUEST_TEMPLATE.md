## Description

Please include a summary of the changes and the related issue. Include relevant motivation and context.

Fixes # (issue)

## Type of Change

Please delete options that are not relevant.

- [ ] feat: New feature (non-breaking change which adds functionality)
- [ ] fix: Bug fix (non-breaking change which fixes an issue)
- [ ] refactor: Code refactoring (no functional changes)
- [ ] test: Adding or updating tests
- [ ] docs: Documentation only changes
- [ ] chore: Changes to build process, tooling, or dependencies
- [ ] style: Code style changes (formatting, etc.)
- [ ] perf: Performance improvements
- [ ] ci: CI/CD configuration changes

## Checklist

- [ ] My code follows the conventional commit format
- [ ] I have made atomic commits (each commit is independently buildable and testable)
- [ ] I have performed a self-review of my code
- [ ] I have commented my code, particularly in hard-to-understand areas
- [ ] I have made corresponding changes to the documentation
- [ ] My changes generate no new warnings
- [ ] I have added tests that prove my fix is effective or that my feature works
- [ ] New and existing unit tests pass locally with my changes
- [ ] Any dependent changes have been merged and published in downstream modules

## Commit Organization

Please ensure your commits follow this pattern:

1. **Domain Layer**: Domain models, entities, enums
2. **Application Layer**: Interfaces, services, DTOs
3. **Infrastructure Layer**: Repositories, data access, migrations
4. **API Layer**: Controllers, hubs, configuration
5. **Tests**: Unit tests, integration tests
6. **Documentation**: README, API docs, comments

## Testing

- [ ] `dotnet build` passes
- [ ] `dotnet test` passes
- [ ] Manual testing completed (if applicable)

## Additional Notes

Add any other context about the pull request here.
