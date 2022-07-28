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
              { Code: 404 } => new NotFoundObjectResult($"Resource \"{path}\" not found."),
              { Code: 400 } => new BadRequestObjectResult(l.Message),
              _ => new ObjectResult(l.Message) { StatusCode = 500 }
          });
    }

    public static async Task<IActionResult> ToActionResult<R>(this Task<Either<Error, R>> @this, string path)
    {
        var either = await @this;
        return either.ToActionResult(path);
    }
}
