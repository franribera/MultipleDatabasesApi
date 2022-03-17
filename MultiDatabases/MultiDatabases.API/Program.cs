using Microsoft.EntityFrameworkCore;
using MultiDatabases.Application.Features.Users;
using MultiDatabases.Infrastructure.Persistence.DatabaseA;
using MultiDatabases.Infrastructure.Persistence.DatabaseA.Repositories;
using MultiDatabases.Infrastructure.Persistence.DatabaseB;
using MultiDatabases.Infrastructure.Persistence.DatabaseB.Repositories;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

services.AddScoped(provider =>
{
    var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
    var dbName = httpContextAccessor?.HttpContext?.Request.Query["databaseName"].FirstOrDefault();

    string connectionString;

    if (dbName == null)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    else
    {
        connectionString = configuration.GetConnectionString("AAA");
        connectionString = connectionString.Replace("{dbName}", dbName);
    }

    return new DbContextA(connectionString);
});

services.AddScoped(_ => new DbContextB(configuration.GetConnectionString("BBB")));

services.AddScoped<IGetAllUsersQuery, GetAllUsersQuery>();
services.AddScoped<IUserRepositoryResolver, UserRepositoryResolver>();
services.AddScoped<IDictionary<string, IUsersRepository>>((services) => new Dictionary<string, IUsersRepository>
{
    {"AAA01", new DatabaseAUserRepository(services.GetRequiredService<DbContextA>())},
    {"AAA02", new DatabaseAUserRepository(services.GetRequiredService<DbContextA>())},
    {"BBB", new DatabaseBUserRepository(services.GetRequiredService<DbContextB>())},
});

//services.AddDbContext<DbContextA>((serviceProvider, optionsBuilder) =>
//{
//    var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
//    var dbName = httpContextAccessor?.HttpContext?.Request.Query["database"].First();

//    string connectionString;

//    if (dbName == null)
//    {
//        connectionString = configuration.GetConnectionString("DefaultConnection");
//    }
//    else
//    {
//        connectionString = configuration.GetConnectionString("AAA");
//        connectionString = connectionString.Replace("{dbName}", dbName);
//    }

//    optionsBuilder.UseSqlServer(connectionString);
//});

//services.AddDbContext<DbContextB>(optionsBuilder =>
//{
//    optionsBuilder.UseSqlServer(configuration.GetConnectionString("BBB"));
//});

//builder.Services.AddTransient<ServiceResolver>(serviceProvider => key =>
//{
//    switch (key)
//    {
//        case "A":
//            return serviceProvider.GetService<ServiceA>();
//        case "B":
//            return serviceProvider.GetService<ServiceB>();
//        case "C":
//            return serviceProvider.GetService<ServiceC>();
//        default:
//            throw new KeyNotFoundException();
//    }
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
