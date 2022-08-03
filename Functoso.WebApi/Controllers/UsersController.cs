using Functoso.Application.Features;
using Functoso.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Functoso.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Returns a list of Users
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> Get()
         => _mediator.Send(new GetUsersFeature.Query()).ToActionResultAsync(HttpContext.Request.Path);

    /// <summary>
    /// Returns a User by Id
    /// </summary>
    /// <param name="id">The User identifier</param>
    /// <returns>User</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> Get(int id)
        => _mediator.Send(new GetUserFeature.Query(id)).ToActionResultAsync(HttpContext.Request.Path);
}
