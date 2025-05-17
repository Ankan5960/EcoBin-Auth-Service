using Auth_Api.Extensions.Helpers;
using Auth_Api.Repositories.Contracts;
using Auth_Api.Services.Contracts;

namespace Auth_Api.Services;

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
