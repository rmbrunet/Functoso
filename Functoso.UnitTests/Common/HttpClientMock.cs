using System.Net;

namespace Functoso.UnitTests.Common;

public static class HttpClientMock
{
    private const string UriString = "http://fakeurl.com";

    static readonly Uri FakeUri = new(UriString);

    public static HttpClient WithBaseUrl(this HttpClient @this, Uri uri)
    {
        @this.BaseAddress = uri;
        return @this;
    }
    /// <summary>
    /// Creates HttpClient that returns the provided response
    /// </summary>
    /// <param name="response">Expected HttpResponseMessage</param>
    /// <returns></returns>
    public static HttpClient CreateHttpClient(HttpResponseMessage response)
        => new HttpClient(new MockingHandler(response)).WithBaseUrl(FakeUri);

    /// <summary>
    /// Creates HttpClient that returns a response with the provided HttpStatusCode
    /// </summary>
    /// <param name="code">Expected HttpStatusCode</param>
    /// <returns></returns>
    public static HttpClient CreateHttpClient(HttpStatusCode code)
        => new HttpClient(new MockingHandler(code)).WithBaseUrl(FakeUri);

    /// <summary>
    /// Creates HttpClient that returns a response with the requested HttpResponseMessage
    /// </summary>
    /// <param name="responseFactory">HttpResponseMessage Factory</param>
    /// <returns></returns>
    public static HttpClient CreateHttpClient(Func<Task<HttpResponseMessage>> responseFactory)
        => new HttpClient(new MockingHandler(responseFactory)).WithBaseUrl(FakeUri);

    /// <summary>
    /// Creates HttpClient that returns a response with the requested HttpResponseMessage
    /// </summary>
    /// <param name="responseFactory">HttpResponseMessage Factory (with request)</param>
    /// <returns></returns>
    public static HttpClient CreateHttpClient(Func<HttpRequestMessage, Task<HttpResponseMessage>> responseFactory)
        => new HttpClient(new MockingHandler(responseFactory)).WithBaseUrl(FakeUri);

}

