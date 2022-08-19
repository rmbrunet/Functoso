using System.Net.Http.Json;
using Functoso.Infrastructure.Errors;

namespace Functoso.Infrastructure.Services;

public class ServiceBase
{
    private readonly HttpClient _httpClient;

    public ServiceBase(HttpClient httpClient)
        => _httpClient = httpClient;


    protected Aff<T> Get<T>(Uri uri)
        => from a in GetHttpResponse(uri)
           from b in Get<T>(a)
           select b;

    static Aff<T> Get<T>(HttpResponseMessage response)
        => AffMaybe<T>(async () =>
        {
            T? t = await response.Content.ReadFromJsonAsync<T>();
            return Optional(t).ToFin();
        });

    Aff<HttpResponseMessage> GetHttpResponse(Uri uri)
        => AffMaybe<HttpResponseMessage>(async () =>
                await _httpClient.GetAsync(uri) switch
                {
                    { IsSuccessStatusCode: true } m => m,
                    var m => new HttpError(m)
                });

}
