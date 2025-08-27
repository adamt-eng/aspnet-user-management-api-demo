using System.Collections.Concurrent;
using UserManagementAPI.Models;

namespace UserManagementAPI;

public class InMemoryUserRepository : IUserRepository
{
    private readonly ConcurrentDictionary<Guid, User> _store = new();

    public IEnumerable<User> GetAll(string? search = null, string? department = null, string? role = null)
    {
        var query = _store.Values.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var s = search.Trim().ToLowerInvariant();
            query = query.Where(u =>
                u.FirstName.ToLower().Contains(s) ||
                u.LastName.ToLower().Contains(s) ||
                u.Email.ToLower().Contains(s));
        }

        if (!string.IsNullOrWhiteSpace(department))
            query = query.Where(u => string.Equals(u.Department, department, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(role))
            query = query.Where(u => string.Equals(u.Role, role, StringComparison.OrdinalIgnoreCase));

        return query.OrderBy(u => u.LastName).ThenBy(u => u.FirstName);
    }

    public User? GetById(Guid id) => _store.TryGetValue(id, out var user) ? user : null;

    public User Add(User user)
    {
        user.CreatedAtUtc = DateTime.UtcNow;
        user.UpdatedAtUtc = DateTime.UtcNow;
        _store[user.Id] = user;
        return user;
    }

    public bool Update(User user)
    {
        if (!_store.ContainsKey(user.Id)) return false;
        user.UpdatedAtUtc = DateTime.UtcNow;
        _store[user.Id] = user;
        return true;
    }

    public bool Delete(Guid id) => _store.TryRemove(id, out _);

    public bool EmailExists(string email, Guid? excludingId = null)
    {
        var e = email.Trim().ToLowerInvariant();
        return _store.Values.Any(u => u.Email.ToLowerInvariant() == e && (!excludingId.HasValue || u.Id != excludingId.Value));
    }
}
