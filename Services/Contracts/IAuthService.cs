using Auth_Api.DTOs.Requests;

namespace Auth_Api.Services.Contracts;

public interface IAuthService
{
    Task<Guid> SignupAsync(SignupRequestDto signupRequest);
}
