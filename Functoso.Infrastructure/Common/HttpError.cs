namespace Functoso.Infrastructure.Common;

public record HttpError(string Message, int Code) : LanguageExt.Common.Expected(Message, Code)
{
    public HttpError(HttpResponseMessage msg) : this(msg.ReasonPhrase ?? "HTTP error", (int)msg.StatusCode) { }
}
