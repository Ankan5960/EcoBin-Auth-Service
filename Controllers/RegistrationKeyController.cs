using EcoBin_Auth_Service.Extensions.Exceptions;
using EcoBin_Auth_Service.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoBin_Auth_Service.Controllers;

[ApiController]
[Route("user-auth/[controller]")]
public class RegistrationKeyController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public RegistrationKeyController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateRegistrationKey([FromBody] Guid roleId)
    {
        if (roleId == Guid.Empty)
            throw new BadRequestException("Role ID is required.");

        var key = await _serviceManager.RegistrationKeysService.CreateRegistrationKeyAsync(roleId);
        return Ok(key);
    }
}
