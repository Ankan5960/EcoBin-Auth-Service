namespace EcoBin_Auth_Service.Model.DTOs.Requests;

public class LogoutRequestDto
{
    public string RefreshToken { get; set; } = string.Empty;
}
