namespace Functoso.Domain;

public class User
{
    public User(int id, string name, string userName)
    {
        Id = id;
        Name = name;
        UserName = userName;
    }

    public int Id { get; init; }
    public string Name { get; init; }
    public string UserName { get; init; }

    public string? Phone { get; set; }
    public string? WebSite { get; set; }
    public Company? Company { get; set; }
}
