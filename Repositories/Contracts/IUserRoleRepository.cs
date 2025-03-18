using Auth_Api.Model.Entities;

namespace Auth_Api.Repositories.Contracts;
public interface IUserRoleRepository : IRepositoryBase<UserRoleEntity>
{
    Task AddUserRoleAsync(UserRoleEntity userRole);
    Task<UserRoleEntity?> GetUserRolesByUserIdAsync(Guid userId);
    Task<IEnumerable<UserRoleEntity>> GetUserRolesByRoleIdAsync(Guid roleId);
    Task DeleteUserRoleAsync(Guid userId, Guid roleId);
}
