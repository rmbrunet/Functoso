using FluentValidation.Results;

namespace Functoso.Application.Errors;

public record ValidationError(string Message) : Expected(Message, (int)System.Net.HttpStatusCode.BadRequest)
{
    public ValidationError(ValidationResult result) : this(result.ToString()) { }
}
