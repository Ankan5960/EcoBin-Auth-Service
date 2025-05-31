namespace EcoBin_Auth_Service.Services.Contracts;

public interface IServiceManager
{
    IAuthService AuthService { get; }
    IRegistrationKeysService RegistrationKeysService { get; }
    IMigrationService MigrationService { get; }
    IRoleIdService RoleIdService { get; }
}