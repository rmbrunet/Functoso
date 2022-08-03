namespace Functoso.UnitTests.Common;

public class MockingHandler : DelegatingHandler
{
    private readonly Func<Task<HttpResponseMessage>> _f;

    public MockingHandler(Func<Task<HttpResponseMessage>> f)
    {
        _f = f;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return _f();
    }
}
