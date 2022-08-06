namespace Functoso.UnitTests.Common;

/// <summary>
/// Mocking DelegatingHandler to use in Unit Tests requiring HttpClient mmocking.
/// </summary>
public class MockingHandler : System.Net.Http.DelegatingHandler
{
    readonly Func<HttpRequestMessage, Task<HttpResponseMessage>> _f;

    /// <summary>
    /// Use this constructor when the response does not depend on the request.
    /// </summary>
    /// <param name="f">Function that returns the expected response.</param>
    public MockingHandler(Func<Task<HttpResponseMessage>> f)
        => _f = r => f();

    /// <summary>
    /// Use this constructor when the response depends on some request values.
    /// </summary>
    /// <param name="f">Function that return the expected response</param>
    public MockingHandler(Func<HttpRequestMessage, Task<HttpResponseMessage>> f)
        => _f = f;

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        => _f(request);
}
