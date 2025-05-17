using Auth_Api.DTOs.Requests;
using Auth_Api.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Auth_Api.Controllers;
[ApiController]
[Route("user-auth/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public AuthController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupRequestDto signupRequest)
    {
        var userId = await _serviceManager.AuthService.SignupAsync(signupRequest);
        return Ok(new { UserId = userId });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
        var result = await _serviceManager.AuthService.LoginAsync(loginRequest);
        return Ok(result);
    }
}
