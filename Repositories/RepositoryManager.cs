using System.Data;
using Auth_Api.Repositories.Contracts;
using User_Auth_API.Repositories;

namespace Auth_Api.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly IDbConnection _dbConnection;
    private readonly Lazy<IRoleRepository> _roleRepo;
    private readonly Lazy<IRegistrationKeyRepository> _registrationKeyRepo;
    private readonly Lazy<IUserRepository> _userRepo;
    private readonly Lazy<IUserRoleRepository> _userRoleRepo;

    public RepositoryManager(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
        _roleRepo = new Lazy<IRoleRepository>(() => new RoleRepository(_dbConnection));
        _registrationKeyRepo = new Lazy<IRegistrationKeyRepository>(()=> new RegistrationKeyRepository(_dbConnection));
        _userRepo = new Lazy<IUserRepository>(()=> new UserRepository(_dbConnection));   
        _userRoleRepo = new Lazy<IUserRoleRepository>(()=> new UserRoleRepository(_dbConnection));
    }

    public IRoleRepository RoleRepository => _roleRepo.Value;
    public IRegistrationKeyRepository RegistrationKeyRepository => _registrationKeyRepo.Value;
    public IUserRepository UserRepository => _userRepo.Value;
    public IUserRoleRepository UserRoleRepository => _userRoleRepo.Value;
}
