using EcoBin_Auth_Service.DTOs.Requests;
using EcoBin_Auth_Service.Extensions.Exceptions;
using EcoBin_Auth_Service.Extensions.Helpers;
using EcoBin_Auth_Service.Model.DTOs;
using EcoBin_Auth_Service.Model.Entities;
using EcoBin_Auth_Service.Model.Enums;
using EcoBin_Auth_Service.Repositories.Contracts;
using EcoBin_Auth_Service.Services.Contracts;

namespace EcoBin_Auth_Service.Services;

public class AuthService : IAuthService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IJwtHelper _jwtHelper;
    private readonly int accessTokenExpireTimeInMinute = 180;

    public AuthService(IRepositoryManager repositoryManager, IJwtHelper jwtHelper)
    {
        _repositoryManager = repositoryManager;
        _jwtHelper = jwtHelper;
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
            userRoleData = await _repositoryManager.RoleRepository.GetRoleRepoByNameAsync(Roles.User.GetRoleName());
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

    public async Task<AuthDto> LoginAsync(LoginRequestDto loginRequest)
    {
        UserEntity? user = await _repositoryManager.UserRepository.GetUserByEmailAsync(loginRequest.Email);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        if (!VerifyPassword(loginRequest.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Password didn't match");
        }

        // if (user.RegistrationKeyId == null)
        // {
        //     throw new UnauthorizedAccessException("User is not registered");
        // }

        RoleEntity role = await GetRoleInfo(user.UserId);

        AuthDto authDto = new AuthDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            RoleId = role.RoleId,
            RoleName = role.RoleName
        };

        authDto.AccessToken = _jwtHelper.GenerateToken(authDto, accessTokenExpireTimeInMinute);

        // var refreshToken = new RefreshTokenEntity
        // {
        //     UserId = user.UserId,
        //     RefreshToken = _jwtHelper.GenerateRefreshToken(),
        //     IpAddress = loginRequest.IpAddress,
        //     DeviceInfo = loginRequest.DeviceInfo,
        //     ExpiresAt = DateTime.Now.AddMinutes(refreshTokenExpireTimeInMinute)
        // };

        // await _repositoryManager.RefreshTokenRepository.AddRefreshTokenAsync(refreshToken);

        // authDto.RefreshToken = refreshToken.RefreshToken;

        return authDto;
    }

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private bool VerifyPassword(string password, string PasswordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
    }

    private string GenerateUniqueRandomString()
    {
        Random random = new Random();
        int randomNumber = random.Next(10000, 100000);
        return randomNumber.ToString();
    }

    private async Task<RoleEntity> GetRoleInfo(Guid UserId)
    {
        var userRole = await _repositoryManager.UserRoleRepository.GetUserRolesByUserIdAsync(UserId);

        if (userRole == null)
        {
            throw new UnauthorizedAccessException("User Role is not found");
        }

        var role = await _repositoryManager.RoleRepository.GetRoleRepoByIdAsync(userRole.RoleId);
        if (role == null)
        {
            throw new UnauthorizedAccessException("User Role is not found");
        }

        return role;
    }
}
