namespace Auth_Api.DTOs.Requests;
public class SignupRequestDto
{
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
}