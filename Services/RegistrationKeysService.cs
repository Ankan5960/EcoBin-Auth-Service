using EcoBin_Auth_Service.Extensions.Exceptions;
using EcoBin_Auth_Service.Model.DTOs.Requests;
using EcoBin_Auth_Service.Model.DTOs.Response;
using EcoBin_Auth_Service.Model.Entities;
using EcoBin_Auth_Service.Repositories.Contracts;
using EcoBin_Auth_Service.Services.Contracts;

namespace EcoBin_Auth_Service.Services;

public class RegistrationKeysService : IRegistrationKeysService
{
    private readonly IRepositoryManager _repositoryManager;

    public RegistrationKeysService(IRepositoryManager repository)
    {
        _repositoryManager = repository;
    }

    public async Task<RegistrationKeyResponseDto?> CreateRegistrationKeyAsync(RegistrationKeyRequestDto registrationKeyRequestDto)
    {
        RoleEntity? role = await _repositoryManager.RoleRepository.GetRoleRepoByIdAsync(registrationKeyRequestDto.RoleId);
        if (role == null)
        {
            throw new NotFoundException("Invalid role ID");
        }

        if ((role.RoleName.Equals("Admin") || role.RoleName.Equals("Collector")) && string.IsNullOrEmpty(registrationKeyRequestDto.AreaOfService))
        {
            throw new BadRequestException("Area of service is required for Admin and Collector roles");
        }

        var newKey = new RegistrationKeyEntity
        {
            RegistrationKey = GenerateUniqueKey(),
            RoleId = registrationKeyRequestDto.RoleId,
            AreaOfService = registrationKeyRequestDto.AreaOfService,
            IsUsed = false,
            ExpiresAt = DateTime.UtcNow.AddYears(1),
            CreatedAt = DateTime.UtcNow
        };

        var createdKeyId = await _repositoryManager.RegistrationKeyRepository.AddRegistrationKeyAsync(newKey);
        return new RegistrationKeyResponseDto
        {
            KeyId = createdKeyId,
            RegistrationKey = newKey.RegistrationKey,
            RoleId = newKey.RoleId,
            AreaOfService = newKey.AreaOfService,
            IsUsed = newKey.IsUsed,
            ExpiresAt = newKey.ExpiresAt,
            CreatedAt = newKey.CreatedAt,
            DeleteAt = newKey.DeleteAt
        };
    }

    private string GenerateUniqueKey()
    {
        return Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N").Substring(0, 16);
    }
}
