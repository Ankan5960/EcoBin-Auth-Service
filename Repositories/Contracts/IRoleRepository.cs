using Auth_Api.Model.Entities;

namespace Auth_Api.Repositories.Contracts;

public interface IRoleRepository : IRepositoryBase<RoleEntity>
{
    Task<Guid> AddRoleRepoAsync(RoleEntity role);
    Task<IEnumerable<RoleEntity>> GetRolesRepoAsync();
    Task<RoleEntity?> GetRoleRepoByIdAsync(Guid id);
    Task<RoleEntity?> GetRoleRepoByNameAsync(string roleName);
    Task<RoleEntity?> GetRoleByRegistrationKey(Guid registrationKey);
}
