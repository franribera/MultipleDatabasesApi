using MultiDatabases.Infrastructure.Persistence.DatabaseA.Models;

namespace MultiDatabases.Infrastructure.Persistence.DatabaseA;

public class DbContextA
{
    public string ConnectionString { get; }
    public IEnumerable<Usuario> Usuarios { get; }

    public DbContextA(string connectionString)
    {
        ConnectionString = connectionString;

        var databaseId = connectionString == "AAA01" ? 1 : 2;

        Usuarios = new List<Usuario>
        {
            new ()
            {
                IdUsuario = 11,
                Nombre = $"Usuario A{databaseId}1",
                Apellidos = $"Apellidos A{databaseId}1"
            },
            new ()
            {
                IdUsuario = 12,
                Nombre = $"Usuario A{databaseId}2",
                Apellidos = $"Apellidos A{databaseId}2"
            },
            new ()
            {
                IdUsuario = 13,
                Nombre = $"Usuario A{databaseId}3",
                Apellidos = $"Apellidos A{databaseId}3"
            }
        };
    }
}