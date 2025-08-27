using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.DTOs;

// Copilot suggested and added
// validation attributes and
// checks to enforce valid user input: [Required], [MaxLength], and [EmailAddress] attributes were added to the User, CreateUserDto, and UpdateUserDto models.
// Does your code include additional functionality like validation to process only valid user data? Yes

public class CreateUserDto
{
    [Required, MaxLength(80)]
    public string FirstName { get; set; } = string.Empty;

    [Required, MaxLength(80)]
    public string LastName { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Department { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Role { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}
