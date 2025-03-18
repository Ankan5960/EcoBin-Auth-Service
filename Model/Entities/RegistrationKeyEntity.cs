namespace Auth_Api.Model.Entities;
public class RegistrationKeyEntity
{
    public Guid KeyId { get; set; }

    public string RegistrationKey { get; set; } = string.Empty;

    public Guid RoleId { get; set; }

    public bool IsUsed { get; set; } = false;

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? DeleteAt { get; set; }
}
