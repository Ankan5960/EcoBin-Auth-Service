namespace Auth_Api.Model.DTOs.Response;

public class RegistrationKeyResponseDto
{
    public Guid KeyId { get; set; }
    public string RegistrationKey { get; set; } = string.Empty;
    public Guid RoleId { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeleteAt { get; set; }
}