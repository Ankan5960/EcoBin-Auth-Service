using System.Data;
using EcoBin_Auth_Service.Model.Entities;
using EcoBin_Auth_Service.Repositories.Contracts;

namespace EcoBin_Auth_Service.Repositories;

public class RefreshTokenRepository : RepositoryBase<RefreshTokenEntity>, IRefreshTokenRepository
{
    public RefreshTokenRepository(IDbConnection dbConnection) : base(dbConnection) { }

    public async Task<Guid> AddRefreshTokenAsync(RefreshTokenEntity refreshToken)
    {
        var query = "INSERT INTO RefreshTokens (user_id, refresh_token, device_info, ip_address, expires_at, is_revoked) " +
                    "VALUES (@UserId, @RefreshToken, @DeviceInfo, @IpAddress, @ExpiresAt, @IsRevoked) RETURNING token_id";
        var parameters = new { refreshToken.UserId, refreshToken.RefreshToken, refreshToken.DeviceInfo, refreshToken.IpAddress, refreshToken.ExpiresAt, refreshToken.IsRevoked };
        Guid newTokenId = await ExecuteScalarAsync(query, parameters);
        return newTokenId;
    }

    public async Task DeleteRefreshTokenAsync(Guid id)
    {
        var query = "DELETE FROM RefreshTokens WHERE token_id = @Id";
        await ExecuteAsync(query, new { Id = id });
    }

    public async Task<RefreshTokenEntity?> GetRefreshTokenByIdAsync(Guid id)
    {
        var query = "SELECT * FROM RefreshTokens WHERE token_id = @Id";
        return await QueryFirstOrDefaultAsync(query, new { Id = id });
    }

    public async Task<IEnumerable<RefreshTokenEntity>> GetRefreshTokensAsync()
    {
        var query = "SELECT * FROM RefreshTokens";
        return await QueryAsync(query);
    }

    public async Task<IEnumerable<RefreshTokenEntity>> GetRefreshTokensByUserIdAsync(Guid userId)
    {
        var query = "SELECT * FROM RefreshTokens WHERE user_id = @UserId";
        return await QueryAsync(query, new { UserId = userId });
    }

    public async Task UpdateRefreshTokenAsync(RefreshTokenEntity refreshToken)
    {
        var query = "UPDATE RefreshTokens SET refresh_token = @RefreshToken, device_info = @DeviceInfo, " +
                    "ip_address = @IpAddress, expires_at = @ExpiresAt, is_revoked = @IsRevoked WHERE token_id = @TokenId";
        await ExecuteAsync(query, new { refreshToken.RefreshToken, refreshToken.DeviceInfo, refreshToken.IpAddress, refreshToken.ExpiresAt, refreshToken.IsRevoked, refreshToken.TokenId });
    }

    public async Task<RefreshTokenEntity?> GetRefreshTokenAsync(string refreshToken)
    {
        string query = "SELECT * FROM RefreshTokens WHERE refresh_token = @RefreshToken";
        return await QueryFirstOrDefaultAsync(query, new { RefreshToken = refreshToken });
    }

    public async Task InvalidateRefreshTokenAsync(Guid tokenId)
    {
        string query = "UPDATE RefreshTokens SET is_revoked = TRUE, updated_at = @UpdatedAt WHERE token_id = @TokenId";
        await ExecuteAsync(query, new { TokenId = tokenId, UpdatedAt = DateTime.Now });
    }

    public async Task RevokeAllTokensForUserAsync(Guid userId)
    {
        string query = "UPDATE RefreshTokens SET is_revoked = TRUE, updated_at = @UpdatedAt WHERE user_id = @UserId AND expires_at > @UpdatedAt";
        await ExecuteAsync(query, new { UserId = userId, UpdatedAt = DateTime.Now });
    }
}
