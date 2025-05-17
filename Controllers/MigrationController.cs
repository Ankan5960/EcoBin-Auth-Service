using Auth_Api.Model.DTOs.Response;
using Auth_Api.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Auth_Api.Controllers;

[ApiController]
[Route("user-auth/[controller]")]
public class MigrationController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public MigrationController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost("db-migrate")]
    public async Task<IActionResult> Migrate([FromBody] string key)
    {
        MigrationResponseDto response = await _serviceManager.MigrationService.RunMigrationAsync(key);
        return Ok(response);
    }
}
