using System.Net.Http.Json;

namespace Functoso.Infrastructure;

public class UserService : IUserService
{
    private readonly HttpClient _client;

    public UserService(HttpClient client)
    {
        _client = client;
    }

    public Aff<IEnumerable<User>> GetUsers()
        => Get<IEnumerable<User>>(_client, "users");

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
        {
            HttpResponseMessage? response = await client.GetAsync(url);
            return response.IsSuccessStatusCode
                ? response
                : Error.New((int)response.StatusCode, response.ReasonPhrase);
        });
}
