using EcoBin_Auth_Service.Model.DTOs.Response;
using EcoBin_Auth_Service.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EcoBin_Auth_Service.Controllers;

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
