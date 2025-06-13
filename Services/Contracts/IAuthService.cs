using EcoBin_Auth_Service.DTOs.Requests;
using EcoBin_Auth_Service.Model.DTOs;
using EcoBin_Auth_Service.Model.DTOs.Requests;

namespace EcoBin_Auth_Service.Services.Contracts;

public interface IAuthService
{
    Task<Guid> SignupAsync(SignupRequestDto signupRequest);
    Task<AuthDto> LoginAsync(LoginRequestDto loginRequest);
    Task<AuthDto> RefreshAccessTokenAsync(RefreshTokenRequestDto refreshTokenRequest);
}
