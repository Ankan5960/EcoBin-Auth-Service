using System.Data;
using Auth_Api.Repositories.Contracts;

namespace Auth_Api.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly IDbConnection _dbConnection;
    private readonly Lazy<IRoleRepository> _roleRepo;

    public RepositoryManager(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
        _roleRepo = new Lazy<IRoleRepository>(() => new RoleRepository(_dbConnection));
    }

    public IRoleRepository RoleRepository => _roleRepo.Value;
}
