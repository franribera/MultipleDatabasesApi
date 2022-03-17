using MultiDatabases.Application.Features.Users;
using MultiDatabases.Domain.Entities;

namespace MultiDatabases.Infrastructure.Persistence.DatabaseA.Repositories;

public class DatabaseAUserRepository : IUsersRepository
{
    private readonly DbContextA _dbContext;

    public DatabaseAUserRepository(DbContextA dbContext)
    {
        _dbContext = dbContext;
    }

    public string ConnectionString => _dbContext.ConnectionString;

    public async Task<IEnumerable<User>> GetAllUsers(CancellationToken token = default)
    {
        var usuarios = await Task.FromResult(_dbContext.Usuarios.ToList());

        return usuarios.Select(u => new User
        {
            Id = u.IdUsuario,
            Name = u.Nombre,
            LastName = u.Apellidos
        });
    }
}