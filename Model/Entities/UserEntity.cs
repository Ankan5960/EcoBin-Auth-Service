namespace EcoBin_Auth_Service.Model.Entities;

public class UserEntity
{
    public Guid UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string? LastName { get; set; }

    public string? AreaOfService { get; set; }

    public string PasswordHash { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? DeletedAt { get; set; }

    public bool IsVerified { get; set; } = false;

    public Guid? RegistrationKeyId { get; set; }
}
