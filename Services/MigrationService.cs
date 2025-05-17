using Auth_Api.Extensions.Script;
using Auth_Api.Model.DTOs.Response;
using Auth_Api.Model.Entities;
using Auth_Api.Model.Enums;
using Auth_Api.Repositories.Contracts;
using Auth_Api.Services.Contracts;
using User_Auth_API.Extensions.Helpers;

namespace Auth_Api.Services;

public class MigrationService : IMigrationService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IServiceManager _serviceManager;
    private readonly IConfiguration _configuration;

    public MigrationService(IRepositoryManager repository, IConfiguration configuration, IServiceManager serviceManager)
    {
        _repositoryManager = repository;
        _configuration = configuration;
        _serviceManager = serviceManager;
    }

    public async Task<MigrationResponseDto> RunMigrationAsync(string key)
    {
        string? migrationKey = _configuration["Migrations:Key"];
        string? migrationUserEmail = _configuration["Migrations:Email"];
        string? migrationUserPassword = _configuration["Migrations:Password"];

        if (migrationKey == null)
        {
            throw new InvalidOperationException("Migrations:Key is missing from configuration");
        }

        if (!key.Equals(migrationKey))
        {
            throw new UnauthorizedAccessException("Invalid migration key");
        }

        if (migrationUserEmail == null || migrationUserPassword == null)
        {
            throw new InvalidOperationException("Migrations user Email or Migrations user Password is missing from configuration");
        }

        string migrationScript = MigrationScript.MigrationScriptString;
        await _repositoryManager.UserRepository.ExecuteAsync(migrationScript);

        RoleEntity? role = await _repositoryManager.RoleRepository.GetRoleRepoByNameAsync(Roles.Admin.GetRoleName());
        if (role == null)
        {
            throw new InvalidOperationException("Admin role is missing from database");
        }

        RegistrationKeyResponseDto? registrationKeyData = await _serviceManager.RegistrationKeysService.CreateRegistrationKeyAsync(role.RoleId);
        if (registrationKeyData == null)
        {
            throw new InvalidOperationException("Registration key could not be created");
        }

        UserEntity user = new UserEntity
        {
            UserName = "admin_12345",
            Email = migrationUserEmail,
            FirstName = "Admin",
            LastName = "",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(migrationUserPassword),
            IsVerified = false,
            RegistrationKeyId = registrationKeyData.KeyId
        };

        Guid userId = await _repositoryManager.UserRepository.AddUserAsync(user);

        if (userId == Guid.Empty)
        {
            throw new InvalidOperationException("User could not be created");
        }

        UserRoleEntity userRole = new UserRoleEntity
        {
            UserId = userId,
            RoleId = role.RoleId
        };

        await _repositoryManager.UserRoleRepository.AddUserRoleAsync(userRole);

        RegistrationKeyEntity? registrationKeyEntity = new RegistrationKeyEntity
        {
            RegistrationKey = registrationKeyData.RegistrationKey,
            RoleId = role.RoleId,
            IsUsed = true,
            ExpiresAt = registrationKeyData.ExpiresAt,
            DeleteAt = registrationKeyData.DeleteAt,
            CreatedAt = registrationKeyData.CreatedAt,
            KeyId = registrationKeyData.KeyId,
        };

        await _repositoryManager.RegistrationKeyRepository.UpdateRegistrationKeyAsync(registrationKeyEntity);

        return new MigrationResponseDto
        {
            Email = migrationUserEmail,
            Password = migrationUserPassword,
        };
    }
}
