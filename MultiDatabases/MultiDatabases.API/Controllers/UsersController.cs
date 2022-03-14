using Microsoft.AspNetCore.Mvc;
using MultiDatabases.Application.Features.Users;
using MultiDatabases.Domain.Entities;

namespace MultiDatabases.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IGetAllUsersQuery _allUsersQuery;

    public UsersController(IGetAllUsersQuery allUsersQuery)
    {
        _allUsersQuery = allUsersQuery;
    }

    [HttpGet("{databaseName}")]
    public async Task<IEnumerable<User>> Get([FromRoute] string databaseName, CancellationToken cancellationToken)
    {
        return await _allUsersQuery.Execute(databaseName, cancellationToken);
    }
}