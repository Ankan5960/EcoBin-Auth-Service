namespace EcoBin_Auth_Service.Model.DTOs;

public class ErrorDetailsDto
{
    public int StatusCode { get; set; }
    public string ErrorType { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? DetailedMessage { get; set; }
    public List<string>? InnerExceptions { get; set; }
}
