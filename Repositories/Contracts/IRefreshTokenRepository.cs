using EcoBin_Auth_Service.Model.Entities;

namespace EcoBin_Auth_Service.Repositories.Contracts;

public interface IRefreshTokenRepository : IRepositoryBase<RefreshTokenEntity>
{
    Task<Guid> AddRefreshTokenAsync(RefreshTokenEntity refreshToken);
    Task<IEnumerable<RefreshTokenEntity>> GetRefreshTokensAsync();
    Task<RefreshTokenEntity?> GetRefreshTokenByIdAsync(Guid id);
    Task<IEnumerable<RefreshTokenEntity>> GetRefreshTokensByUserIdAsync(Guid userId);
    Task UpdateRefreshTokenAsync(RefreshTokenEntity refreshToken);
    Task DeleteRefreshTokenAsync(Guid id);
    Task<RefreshTokenEntity?> GetRefreshTokenAsync(string refreshToken);
    Task InvalidateRefreshTokenAsync(Guid tokenId);
    Task RevokeAllTokensForUserAsync(Guid userId);
}
