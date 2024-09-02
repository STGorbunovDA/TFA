using Microsoft.AspNetCore.Mvc;
using TFA.API.Models;
using TFA.Domain.UseCases.GetForums;

namespace TFA.API.Controllers;

[ApiController]
[Route("forums")]
public class ForumController : ControllerBase
{
    /// <summary>
    /// Get list of every forum
    /// </summary>
    /// <param name="useCase"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(ForumDto[]))]
    public async Task<IActionResult> GetForums(
        [FromServices] IGetForumsUseCase useCase,
        CancellationToken cancellationToken)
    {
        var forums = await useCase.Execute(cancellationToken);
        return Ok(forums.Select(f => new ForumDto
        {
            Id = f.Id,
            Title = f.Title
        }));
    }
}