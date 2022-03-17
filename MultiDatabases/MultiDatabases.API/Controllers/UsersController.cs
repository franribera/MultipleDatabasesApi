using Microsoft.AspNetCore.Mvc;
using MultiDatabases.Application.Features.Users;
using MultiDatabases.Domain.Entities;

namespace MultiDatabases.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepositoryResolver _userRepositoryResolver;

    public UsersController(IUserRepositoryResolver userRepositoryResolver)
    {
        _userRepositoryResolver = userRepositoryResolver;
    }

    [HttpGet]
    public async Task<UsersResponse> Get([FromQuery] string databaseName, CancellationToken cancellationToken)
    {
        var repository = _userRepositoryResolver.Resolve(databaseName);

        var users = await repository.GetAllUsers(cancellationToken);

        return new UsersResponse
        {
            ConnectionString = repository.ConnectionString,
            Users = users
        };
    }
}

public class UsersResponse
{
    public string ConnectionString { get; set; }
    public IEnumerable<User> Users { get; set; }
}