using EcoBin_Auth_Service.Model.Entities;

namespace EcoBin_Auth_Service.Repositories.Contracts;

public interface IUserRoleRepository : IRepositoryBase<UserRoleEntity>
{
    Task AddUserRoleAsync(UserRoleEntity userRole);
    Task<UserRoleEntity?> GetUserRolesByUserIdAsync(Guid userId);
    Task<IEnumerable<UserRoleEntity>> GetUserRolesByRoleIdAsync(Guid roleId);
    Task DeleteUserRoleAsync(Guid userId, Guid roleId);
}
