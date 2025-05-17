using Auth_Api.Model.DTOs.Response;

namespace Auth_Api.Services.Contracts;

public interface IRegistrationKeysService
{
    Task<RegistrationKeyResponseDto?> CreateRegistrationKeyAsync(Guid roleId);
}