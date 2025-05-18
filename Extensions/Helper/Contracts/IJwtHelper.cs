using EcoBin_Auth_Service.Model.DTOs;

namespace EcoBin_Auth_Service.Extensions.Helpers;

public interface IJwtHelper
{
    string GenerateToken(AuthDto authDto, int expireTimeInMinutes);
    string GenerateRefreshToken();
}