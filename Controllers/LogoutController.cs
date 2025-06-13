using System.Security.Claims;
using EcoBin_Auth_Service.Model.DTOs.Requests;
using EcoBin_Auth_Service.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoBin_Auth_Service.Controllers;

[ApiController]
[Route("user-auth/[controller]")]
public class LogoutController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public LogoutController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout([FromBody] LogoutRequestDto request)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            throw new UnauthorizedAccessException("Invalid user.");
        }

        var res = await _serviceManager.LogoutService.LogoutAsync(Guid.Parse(userId), request.RefreshToken);
        return Ok(res);
    }
}
