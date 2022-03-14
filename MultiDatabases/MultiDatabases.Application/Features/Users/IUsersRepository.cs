using MultiDatabases.Domain.Entities;

namespace MultiDatabases.Application.Features.Users;

public interface IUsersRepository
{
    Task<IEnumerable<User>> GetAllUsers(CancellationToken token = default);
}