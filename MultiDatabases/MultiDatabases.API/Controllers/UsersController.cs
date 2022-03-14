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

    [HttpGet("{databaseName}")]
    public async Task<IEnumerable<User>> Get([FromRoute] string databaseName, CancellationToken cancellationToken)
    {
        return await _userRepositoryResolver.Resolve(databaseName).GetAllUsers(cancellationToken);
    }
}