using Microsoft.EntityFrameworkCore;
using MultiDatabases.Infrastructure.Persistence.DatabaseB.Models;

namespace MultiDatabases.Infrastructure.Persistence.DatabaseB;

public class DbContextB : DbContext
{
    public string ConnectionString { get; }
    public IEnumerable<Usuari> Usuaris { get; }

    public DbContextB(string connectionString)
    {
        ConnectionString = connectionString;

        Usuaris = new List<Usuari>
        {
            new ()
            {
                IdUsuari = 31,
                Nom = "Usuari B31",
                Cognom = "Cognom B31"
            },
            new ()
            {
                IdUsuari = 32,
                Nom = "Usuari B32",
                Cognom = "Cognom B32"
            },
            new ()
            {
                IdUsuari = 33,
                Nom = "Usuari B33",
                Cognom = "Cognom B33"
            }
        };
    }
}