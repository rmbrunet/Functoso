namespace Functoso.Domain;

public class Company
{
    public Company(string name)
    {
        Name = name;
    }

    public string Name { get; init; }
    public string? CatchPhrase { get; init; }
}
