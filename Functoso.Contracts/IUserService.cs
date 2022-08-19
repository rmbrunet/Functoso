namespace Functoso.Contracts;

public interface IUserService
{
    Aff<User> GetUser(int id);

    Aff<IEnumerable<User>> Users { get; }
}
