namespace Functoso.UnitTests.Common;

/// <summary>
/// Mocking DelegatingHandler to use in Unit Tests requiring HttpClient mmocking.
/// </summary>
public class MockingHandler : HttpMessageHandler
{
    readonly Func<HttpRequestMessage, Task<HttpResponseMessage>> _f;

    /// <summary>
    /// Use this constructor when jus an Status Code ie expected
    /// </summary>
    /// <param name="expected">Expected Status code</param>
    public MockingHandler(HttpResponseMessage response)
        => _f = r => Task.FromResult(response);


    /// <summary>
    /// Use this constructor when jus an Status Code ie expected
    /// </summary>
    /// <param name="expected">Expected Status code</param>
    public MockingHandler(System.Net.HttpStatusCode expected) : this(new HttpResponseMessage(expected))
    { }

    /// <summary>
    /// Use this constructor when the response does not depend on the request.
    /// </summary>
    /// <param name="f">HttpResponseMessage factory function that returns the expected response.</param>
    public MockingHandler(Func<Task<HttpResponseMessage>> f)
        => _f = r => f();

    /// <summary>
    /// Use this constructor when the response depends on some request values.
    /// </summary>
    /// <param name="f">HttpResponseMessage factory function that return the expected response</param>
    public MockingHandler(Func<HttpRequestMessage, Task<HttpResponseMessage>> f)
        => _f = f;

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        => _f(request);
}
