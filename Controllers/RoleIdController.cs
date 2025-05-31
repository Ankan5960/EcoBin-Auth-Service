using EcoBin_Auth_Service.Extensions.Exceptions;
using EcoBin_Auth_Service.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoBin_Auth_Service.Controllers;

[ApiController]
[Route("user-auth/[controller]")]
public class RoleIdController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public RoleIdController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("get-role-id")]
    public async Task<IActionResult> GetRoleId()
    {
        var key = await _serviceManager.RoleIdService.GetRoleIdAsync();
        return Ok(key);
    }
}
