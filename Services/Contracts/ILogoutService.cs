namespace EcoBin_Auth_Service.Services.Contracts;

public interface ILogoutService
{
    Task<bool> LogoutAsync(Guid userId, string refreshToken);
    Task GlobalLogoutAsync(Guid userId);
}
