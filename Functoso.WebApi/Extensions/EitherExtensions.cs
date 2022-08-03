using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace Functoso.WebApi.Extensions;

public static class EitherExtensions
{
    public static IActionResult ToActionResult<R>(this Either<Error, R> @this, string path)
    {
        return @this.Match<IActionResult>(
          Right: r => new OkObjectResult(r),
          Left: l => l switch
          {
              { Code: StatusCodes.Status404NotFound } => new NotFoundObjectResult($"Resource '{path}' not found."),
              { Code: StatusCodes.Status400BadRequest } => new BadRequestObjectResult(l.Message),
              _ => new ObjectResult(l.Message) { StatusCode = StatusCodes.Status500InternalServerError }
          });
    }

    public static async Task<IActionResult> ToActionResultAsync<R>(this Task<Either<Error, R>> @this, string path)
    {
        Either<Error, R> either = await @this;
        return either.ToActionResult(path);
    }
}
