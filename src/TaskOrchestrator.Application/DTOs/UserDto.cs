using System.ComponentModel.DataAnnotations;

namespace TaskOrchestrator.Application.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? Department { get; set; }
    public bool IsActive { get; set; }
}

public class CreateUserDto
{
    [Required(ErrorMessage = "UserName is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "UserName must be between 1 and 100 characters")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(200, ErrorMessage = "Email cannot exceed 200 characters")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "FullName is required")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "FullName must be between 1 and 200 characters")]
    public string FullName { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "Department cannot exceed 100 characters")]
    public string? Department { get; set; }
}
