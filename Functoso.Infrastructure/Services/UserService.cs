using System.Net.Http.Json;
using Functoso.Infrastructure.Common;

namespace Functoso.Infrastructure;

/// <summary>
/// User Service implementation.
/// </summary>
public class UserService : IUserService
{
    private readonly HttpClient _client;

    public UserService(HttpClient client)
    {
        _client = client;
    }

    /// <summary>
    /// GetUsers Method
    /// </summary>
    /// <returns>Collection of User objects</returns>
    public Aff<IEnumerable<User>> GetUsers()
        => Get<IEnumerable<User>>(_client, "users");

    /// <summary>
    /// GetUser Method
    /// </summary>
    /// <param name="id">User identifier</param>
    /// <returns>The usewr corresponding to the id or an Expected 404 Error</returns>
    public Aff<User> GetUser(int id)
        => Get<User>(_client, $"users/{id}");

    private static Aff<T> Get<T>(HttpClient client, string url)
        => from a in GetHttpResponse(client, url)
           from b in Get<T>(a)
           select b;

    private static Aff<T> Get<T>(HttpResponseMessage response)
        => AffMaybe<T>(async () =>
        {
            T? t = await response.Content.ReadFromJsonAsync<T>();
            return Optional(t).ToFin();
        });

    private static Aff<HttpResponseMessage> GetHttpResponse(HttpClient client, string url)
        => AffMaybe<HttpResponseMessage>(async () =>
            await client.GetAsync(url) switch
            {
                { IsSuccessStatusCode: true } m => m,
                var m => new HttpError(m)
            });
}
