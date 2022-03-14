using MultiDatabases.Application.Features.Users;
using MultiDatabases.Infrastructure.Persistence.DatabaseA;
using MultiDatabases.Infrastructure.Persistence.DatabaseA.Repositories;
using MultiDatabases.Infrastructure.Persistence.DatabaseB;
using MultiDatabases.Infrastructure.Persistence.DatabaseB.Repositories;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddScoped<IGetAllUsersQuery, GetAllUsersQuery>();
services.AddScoped<IUserRepositoryResolver, UserRepositoryResolver>();
services.AddScoped<IDictionary<string, IUsersRepository>>(_ => new Dictionary<string, IUsersRepository>
{
    {"AAA01", new DatabaseAUserRepository(new DbContextA(configuration.GetConnectionString("AAA01")))},
    {"AAA02", new DatabaseAUserRepository(new DbContextA(configuration.GetConnectionString("AAA02")))},
    {"BBB", new DatabaseBUserRepository(new DbContextB(configuration.GetConnectionString("BBB")))},
});

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
