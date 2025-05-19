using EcoBin_Auth_Service.Extensions.Helpers;
using EcoBin_Auth_Service.Repositories.Contracts;
using EcoBin_Auth_Service.Services.Contracts;

namespace EcoBin_Auth_Service.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAuthService> _authService;
    private readonly Lazy<IRegistrationKeysService> _registrationKeyService;
    private readonly Lazy<IMigrationService> _migrationService;

    public IAuthService AuthService => _authService.Value;
    public IRegistrationKeysService RegistrationKeysService => _registrationKeyService.Value;
    public IMigrationService MigrationService => _migrationService.Value;

    public ServiceManager(IRepositoryManager repository, IJwtHelper jwtHelper, IConfiguration configuration)
    {
        _authService = new Lazy<IAuthService>(() => new AuthService(repository, jwtHelper));
        _registrationKeyService = new Lazy<IRegistrationKeysService>(() => new RegistrationKeysService(repository));
        _migrationService = new Lazy<IMigrationService>(() => new MigrationService(repository, configuration, this));
    }
}
