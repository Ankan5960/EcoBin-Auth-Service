using EcoBin_Auth_Service.Repositories.Contracts;
using EcoBin_Auth_Service.Services.Contracts;

namespace EcoBin_Auth_Service.Services;

public class LogoutService : ILogoutService
{
    private readonly IRepositoryManager _repositoryManager;

    public LogoutService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<bool> LogoutAsync(Guid userId, string refreshToken)
    {
        var token = await _repositoryManager.RefreshTokenRepository.GetRefreshTokenAsync(refreshToken);

        if (token == null || token.UserId != userId || token.IsRevoked || token.ExpiresAt <= DateTime.Now)
        {
            throw new UnauthorizedAccessException("Invalid or expired refresh token");
        }

        token.IsRevoked = true;
        token.UpdatedAt = DateTime.Now;

        await _repositoryManager.RefreshTokenRepository.UpdateRefreshTokenAsync(token);

        return true;
    }

    public async Task GlobalLogoutAsync(Guid userId)
    {
        await _repositoryManager.RefreshTokenRepository.RevokeAllTokensForUserAsync(userId);
    }
}