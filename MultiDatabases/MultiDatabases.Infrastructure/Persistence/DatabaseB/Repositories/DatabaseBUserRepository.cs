using MultiDatabases.Application.Features.Users;
using MultiDatabases.Domain.Entities;

namespace MultiDatabases.Infrastructure.Persistence.DatabaseB.Repositories;

public class DatabaseBUserRepository : IUsersRepository
{
    private readonly DbContextB _dbContext;

    public DatabaseBUserRepository(DbContextB dbContext)
    {
        _dbContext = dbContext;
    }

    public string ConnectionString => _dbContext.ConnectionString;

    public async Task<IEnumerable<User>> GetAllUsers(CancellationToken token = default)
    {
        var usuarios = await Task.FromResult(_dbContext.Usuaris.ToList());

        return usuarios.Select(u => new User
        {
            Id = u.IdUsuari,
            Name = u.Nom,
            LastName = u.Cognom
        });
    }
}