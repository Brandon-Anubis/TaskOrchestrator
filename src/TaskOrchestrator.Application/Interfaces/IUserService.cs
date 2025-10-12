using TaskOrchestrator.Application.DTOs;

namespace TaskOrchestrator.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(Guid id);
    Task<UserDto> CreateUserAsync(CreateUserDto userDto);
    Task<bool> DeleteUserAsync(Guid id);
}
