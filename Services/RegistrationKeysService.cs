using Auth_Api.Extensions.Exceptions;
using Auth_Api.Model.DTOs.Response;
using Auth_Api.Model.Entities;
using Auth_Api.Repositories.Contracts;
using Auth_Api.Services.Contracts;

namespace Auth_Api.Services;

public class RegistrationKeysService : IRegistrationKeysService
{
    private readonly IRepositoryManager _repositoryManager;

    public RegistrationKeysService(IRepositoryManager repository)
    {
        _repositoryManager = repository;
    }

    public async Task<RegistrationKeyResponseDto?> CreateRegistrationKeyAsync(Guid roleId)
    {
        RoleEntity? role = await _repositoryManager.RoleRepository.GetRoleRepoByIdAsync(roleId);
        if (role == null)
        {
            throw new NotFoundException("Invalid role ID");
        }

        var newKey = new RegistrationKeyEntity
        {
            RegistrationKey = GenerateUniqueKey(),
            RoleId = roleId,
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
