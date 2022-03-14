using MultiDatabases.Domain.Entities;

namespace MultiDatabases.Application.Features.Users;

public interface IGetAllUsersQuery
{
    Task<IEnumerable<User>> Execute(string databaseName, CancellationToken cancellationToken = default);
}

public class GetAllUsersQuery : IGetAllUsersQuery
{
    private readonly IUserRepositoryResolver _repositoryResolver;

    public GetAllUsersQuery(IUserRepositoryResolver repositoryResolver)
    {
        _repositoryResolver = repositoryResolver;
    }

    public async Task<IEnumerable<User>> Execute(string databaseName, CancellationToken cancellationToken = default)
    {
        var repository = _repositoryResolver.Resolve(databaseName);

        return await repository.GetAllUsers(cancellationToken);
    }
}