using Microsoft.AspNetCore.Mvc;
using UserIdentifierService.Models;
using UserIdentifierService.Services;

namespace UserIdentifierService.Controllers;

[ApiController]
[Route("avatar")]
public class AvatarController : ControllerBase
{
    private readonly ILogger<AvatarController> _logger;
    private readonly AvatarService _service;

    public AvatarController(ILogger<AvatarController> logger, AvatarService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet(Name = "GetUserProfileImage")]
    public ActionResult<AvatarProfile> GetUserProfileImage([FromQuery] string userIdentifier)
    {
        if (string.IsNullOrEmpty(userIdentifier))
        {
            _logger.LogWarning("User identifier is missing.");
            return BadRequest("User identifier is required.");
        }

        var avatar = _service.GetAvatarUrl(userIdentifier);
        _logger.LogInformation("Generated image URL: {ImageUrl}", avatar.Url);

        return Ok(avatar);
    }
}