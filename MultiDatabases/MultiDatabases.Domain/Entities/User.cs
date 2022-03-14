namespace MultiDatabases.Domain.Entities;

public class User
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }

    public User()
    {
        Name = string.Empty;
        LastName = string.Empty;
    }
}