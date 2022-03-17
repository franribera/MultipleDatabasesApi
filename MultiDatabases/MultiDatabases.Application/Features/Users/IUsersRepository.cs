using MultiDatabases.Domain.Entities;

namespace MultiDatabases.Application.Features.Users;

public interface IUsersRepository
{
    string ConnectionString { get; }
    Task<IEnumerable<User>> GetAllUsers(CancellationToken token = default);
}