using Auth_Api.Model.DTOs;

namespace Auth_Api.Extensions.Helpers;

public interface IJwtHelper
{
    string GenerateToken(AuthDto authDto, int expireTimeInMinutes);
    string GenerateRefreshToken();
}