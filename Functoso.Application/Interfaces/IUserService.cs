namespace Functoso.Application.Interfaces;

public interface IUserService
{
    Aff<User> GetUser(int id);

    Aff<IEnumerable<User>> Users { get; }
}
