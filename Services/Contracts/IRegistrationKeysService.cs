using EcoBin_Auth_Service.Model.DTOs.Response;

namespace EcoBin_Auth_Service.Services.Contracts;

public interface IRegistrationKeysService
{
    Task<RegistrationKeyResponseDto?> CreateRegistrationKeyAsync(Guid roleId);
}