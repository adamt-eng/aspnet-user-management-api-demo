using UserManagementAPI.Models;

namespace UserManagementAPI;

public interface IUserRepository
{
    IEnumerable<User> GetAll(string? search = null, string? department = null, string? role = null);
    User? GetById(Guid id);
    User Add(User user);
    bool Update(User user);
    bool Delete(Guid id);
    bool EmailExists(string email, Guid? excludingId = null);
}
