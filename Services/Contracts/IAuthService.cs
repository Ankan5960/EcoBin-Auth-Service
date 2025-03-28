using Auth_Api.DTOs.Requests;
using Auth_Api.Model.DTOs;

namespace Auth_Api.Services.Contracts;

public interface IAuthService
{
    Task<Guid> SignupAsync(SignupRequestDto signupRequest);
    Task<AuthDto> LoginAsync(LoginRequestDto loginRequest);
}
