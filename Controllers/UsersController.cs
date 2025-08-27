using Microsoft.AspNetCore.Mvc;
using UserManagementAPI;
using UserManagementAPI.DTOs;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers;

// Does your code include CRUD endpoints for managing users like GET, POST, PUT, and DELETE? Yes
// Did you use Copilot to debug your code? Yes

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repo;

    public UsersController(IUserRepository repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// GET /api/users
    /// Optional filters: ?search=&department=&role=
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers([FromQuery] string? search, [FromQuery] string? department, [FromQuery] string? role)
    {
        var users = _repo.GetAll(search, department, role);
        return Ok(users);
    }

    /// <summary>
    /// GET /api/users/{id}
    /// </summary>
    [HttpGet("{id:guid}")]
    public ActionResult<User> GetUser(Guid id)
    {
        var user = _repo.GetById(id);
        if (user is null) return NotFound();
        return Ok(user);
    }

    /// <summary>
    /// POST /api/users
    /// </summary>
    [HttpPost]
    public ActionResult<User> CreateUser([FromBody] CreateUserDto dto)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        if (_repo.EmailExists(dto.Email))
            return Conflict(new { message = "Email already exists." });

        var user = new User
        {
            FirstName = dto.FirstName.Trim(),
            LastName = dto.LastName.Trim(),
            Email = dto.Email.Trim(),
            Department = dto.Department?.Trim() ?? string.Empty,
            Role = dto.Role?.Trim() ?? string.Empty,
            IsActive = dto.IsActive
        };

        _repo.Add(user);

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    /// <summary>
    /// PUT /api/users/{id}
    /// Replaces the user's details.
    /// </summary>
    [HttpPut("{id:guid}")]
    public IActionResult UpdateUser(Guid id, [FromBody] UpdateUserDto dto)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var existing = _repo.GetById(id);
        if (existing is null) return NotFound();

        if (_repo.EmailExists(dto.Email, excludingId: id))
            return Conflict(new { message = "Email already exists." });

        existing.FirstName = dto.FirstName.Trim();
        existing.LastName = dto.LastName.Trim();
        existing.Email = dto.Email.Trim();
        existing.Department = dto.Department?.Trim() ?? string.Empty;
        existing.Role = dto.Role?.Trim() ?? string.Empty;
        existing.IsActive = dto.IsActive;

        var updated = _repo.Update(existing);
        if (!updated) return NotFound();

        return NoContent();
    }

    /// <summary>
    /// DELETE /api/users/{id}
    /// </summary>
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteUser(Guid id)
    {
        var ok = _repo.Delete(id);
        return ok ? NoContent() : NotFound();
    }
}
