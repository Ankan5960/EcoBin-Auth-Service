using System.Data;
using EcoBin_Auth_Service.Model.Entities;
using EcoBin_Auth_Service.Repositories;
using EcoBin_Auth_Service.Repositories.Contracts;

namespace User_Auth_API.Repositories;

public class UserRoleRepository : RepositoryBase<UserRoleEntity>, IUserRoleRepository
{
    public UserRoleRepository(IDbConnection dbConnection) : base(dbConnection) { }

    public async Task AddUserRoleAsync(UserRoleEntity userRole)
    {
        var query = "INSERT INTO UserRoles (user_id, role_id) VALUES (@UserId, @RoleId)";
        var parameters = new { userRole.UserId, userRole.RoleId };
        await ExecuteAsync(query, parameters);
    }

    public async Task DeleteUserRoleAsync(Guid userId, Guid roleId)
    {
        var query = "DELETE FROM UserRoles WHERE user_id = @UserId AND role_id = @RoleId";
        await ExecuteAsync(query, new { UserId = userId, RoleId = roleId });
    }

    public async Task<UserRoleEntity?> GetUserRolesByUserIdAsync(Guid userId)
    {
        var query = "SELECT * FROM UserRoles WHERE user_id = @UserId";
        return await QueryFirstOrDefaultAsync(query, new { UserId = userId });
    }

    public async Task<IEnumerable<UserRoleEntity>> GetUserRolesByRoleIdAsync(Guid roleId)
    {
        var query = "SELECT * FROM UserRoles WHERE role_id = @RoleId";
        return await QueryAsync(query, new { RoleId = roleId });
    }
}
