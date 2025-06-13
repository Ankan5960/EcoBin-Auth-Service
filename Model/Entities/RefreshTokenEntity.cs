namespace EcoBin_Auth_Service.Model.Entities;

public class RefreshTokenEntity
{
    public Guid TokenId { get; set; }

    public Guid UserId { get; set; }

    public string RefreshToken { get; set; } = string.Empty;

    public string? DeviceInfo { get; set; }

    public string? IpAddress { get; set; }

    public DateTime ExpiresAt { get; set; }

    public bool IsRevoked { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
