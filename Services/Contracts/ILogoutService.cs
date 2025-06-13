using EcoBin_Auth_Service.Model.DTOs.Requests;

namespace EcoBin_Auth_Service.Services.Contracts;

public interface ILogoutService
{
    Task<LogoutResponseDto> LogoutAsync(Guid userId, string refreshToken);
    Task GlobalLogoutAsync(Guid userId);
}
