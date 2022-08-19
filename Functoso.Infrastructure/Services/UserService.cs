namespace Functoso.Infrastructure.Services;

/// <summary>
/// User Service implementation.
/// </summary>
public class UserService : ServiceBase, IUserService
{

    public UserService(HttpClient client) : base(client)
    {

    }

    /// <summary>
    /// GetUsers Method
    /// </summary>
    /// <returns>Collection of User objects</returns>
    public Aff<IEnumerable<User>> Users => Get<IEnumerable<User>>(new Uri("users", UriKind.Relative));

    /// <summary>
    /// GetUser Method
    /// </summary>
    /// <param name="id">User identifier</param>
    /// <returns>The usewr corresponding to the id or an Expected 404 Error</returns>
    public Aff<User> GetUser(int id)
        => Get<User>(new Uri($"users/{id}", UriKind.Relative));

}
