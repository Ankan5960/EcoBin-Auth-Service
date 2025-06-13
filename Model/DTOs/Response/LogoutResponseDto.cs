namespace EcoBin_Auth_Service.Model.DTOs.Requests;

public class LogoutResponseDto
{
    public bool IsLoggedOut { get; set; }
    public string Message { get; set; } = string.Empty;
}
