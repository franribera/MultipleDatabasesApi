namespace MultiDatabases.Application.Features.Users;

public interface IUserRepositoryResolver
{
    IUsersRepository Resolve(string databaseName);
}

public class UserRepositoryResolver : IUserRepositoryResolver
{
    private readonly IDictionary<string, IUsersRepository> repositories;

    public UserRepositoryResolver(IDictionary<string, IUsersRepository> repositories)
    {
        this.repositories = repositories;
    }

    public IUsersRepository Resolve(string databaseName)
    {
        try
        {
            return repositories[databaseName];
        }
        catch (KeyNotFoundException exception)
        {
            // hacer lo que convenga
            throw;
        }
    }
}
