using EcoBin_Auth_Service.DTOs.Requests;
using EcoBin_Auth_Service.Model.DTOs.Requests;
using EcoBin_Auth_Service.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EcoBin_Auth_Service.Controllers;

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

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
    {
        var result = await _serviceManager.AuthService.RefreshAccessTokenAsync(request);
        return Ok(result);
    }
}
