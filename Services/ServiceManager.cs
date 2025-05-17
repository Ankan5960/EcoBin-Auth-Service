using Auth_Api.Extensions.Helpers;
using Auth_Api.Repositories.Contracts;
using Auth_Api.Services.Contracts;

namespace Auth_Api.Services;
public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAuthService> _authService;
    public IAuthService AuthService => _authService.Value;
   
    public ServiceManager(IRepositoryManager repository, IJwtHelper jwtHelper)
    {
        _authService = new Lazy<IAuthService>(() => new AuthService(repository, jwtHelper));
    }
}
