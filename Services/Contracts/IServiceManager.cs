namespace Auth_Api.Services.Contracts;

public interface IServiceManager
{
    IAuthService AuthService { get; }
    IRegistrationKeysService RegistrationKeysService { get; }
    IMigrationService MigrationService { get; }
}