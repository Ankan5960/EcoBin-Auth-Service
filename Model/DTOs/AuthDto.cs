namespace Auth_Api.Model.DTOs;
public class AuthDto
{
    public Guid UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string? LastName { get; set; }

    public Guid RoleId { get; set; }

    public string RoleName { get; set; } = string.Empty;

    public string AccessToken { get; set; } = string.Empty;

   // public string RefreshToken { get; set; } = string.Empty;
}