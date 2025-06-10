namespace EcoBin_Auth_Service.Model.DTOs.Requests;

public class RefreshTokenRequestDto
{
    public string RefreshToken { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public string DeviceInfo { get; set; } = string.Empty;
}
