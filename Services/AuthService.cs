using Auth_Api.DTOs.Requests;
using Auth_Api.Model.Entities;
using Auth_Api.Repositories.Contracts;
using Auth_Api.Services.Contracts;

namespace Auth_Api.Services;
public class AuthService : IAuthService
{
    private readonly IRepositoryManager _repositoryManager;

    public AuthService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Guid> SignupAsync(SignupRequestDto signupRequest)
    {
        RegistrationKeyEntity? registrationKeyData = null;
        RoleEntity? userRoleData = null;

        if (!string.IsNullOrEmpty(signupRequest.SecretKey) && !string.IsNullOrWhiteSpace(signupRequest.SecretKey))
        {
            registrationKeyData = await _repositoryManager.RegistrationKeyRepository.GetRegistrationKeyByRegistrationKeyAsync(signupRequest.SecretKey);
            if (registrationKeyData == null)
            {
                throw new UnauthorizedAccessException("Secret-key is invalid");
            }

            if (registrationKeyData.ExpiresAt < DateTime.Now)
            {
                throw new UnauthorizedAccessException("Secret-key has expired");
            }

            if (registrationKeyData.IsUsed)
            {
                throw new UnauthorizedAccessException("Secret-key has already been used");
            }
        }
        else
        {
            userRoleData = await _repositoryManager.RoleRepository.GetRoleRepoByNameAsync("User");
            ArgumentNullException.ThrowIfNull(userRoleData);
        }

        string username = string.Concat(signupRequest.FirstName.ToLower(), "_", GenerateUniqueRandomString());

        var user = new UserEntity
        {
            UserName = username,
            Email = signupRequest.Email,
            FirstName = signupRequest.FirstName,
            LastName = signupRequest.LastName,
            PasswordHash = HashPassword(signupRequest.Password),
            RegistrationKeyId = registrationKeyData == null ? null : registrationKeyData.KeyId,
        };

        Guid createdUserId = await _repositoryManager.UserRepository.AddUserAsync(user);

        if (registrationKeyData != null)
        {
            registrationKeyData.IsUsed = true;
            await _repositoryManager.RegistrationKeyRepository.UpdateRegistrationKeyAsync(registrationKeyData);
        }

        UserRoleEntity userRole = new UserRoleEntity
        {
            UserId = createdUserId,
            RoleId = registrationKeyData == null ? userRoleData.RoleId : registrationKeyData.RoleId,
        };

        await _repositoryManager.UserRoleRepository.AddUserRoleAsync(userRole);
        return createdUserId;
    }

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private string GenerateUniqueRandomString()
    {
        Random random = new Random();
        int randomNumber = random.Next(10000, 100000);
        return randomNumber.ToString();
    }
}
