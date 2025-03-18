using System.Data;
using Auth_Api.Model.Entities;
using Auth_Api.Repositories.Contracts;

namespace Auth_Api.Repositories;

public class RoleRepository : RepositoryBase<RoleEntity>, IRoleRepository
{
    public RoleRepository(IDbConnection dbConnection) : base(dbConnection) { }

    public async Task<Guid> AddRoleRepoAsync(RoleEntity role)
    {
        var query = "INSERT INTO Roles (role_name) VALUES (@RoleName) RETURNING role_id";
        var parameters = new { role.RoleName };
        Guid newRoleId = await ExecuteScalarAsync(query, parameters);
        return newRoleId;
    }

    public async Task<RoleEntity?> GetRoleRepoByIdAsync(Guid id)
    {
        var query = "SELECT role_id as RoleId, role_name as RoleName FROM Roles WHERE role_id = @Id";
        return await QueryFirstOrDefaultAsync(query, new { Id = id });
    }

    public async Task<RoleEntity?> GetRoleRepoByNameAsync(string roleName)
    {
        var query = "SELECT role_id as RoleId, role_name as RoleName FROM Roles WHERE role_name = @RoleName";
        return await QueryFirstOrDefaultAsync(query, new { RoleName = roleName });
    }

    public async Task<IEnumerable<RoleEntity>> GetRolesRepoAsync()
    {
        var query = "SELECT role_id as RoleId, role_name as RoleName FROM Roles";
        return await QueryAsync(query);
    }

    public async Task<RoleEntity?> GetRoleByRegistrationKey(Guid registrationKeyId)
    {
        string query = @"
        SELECT 
            Roles.role_id as RoleId,
            Roles.role_name as RoleName
        FROM 
            RegistrationKeys
        JOIN 
            Roles
        ON 
            RegistrationKeys.role_id = Roles.role_id
        WHERE 
            RegistrationKeys.key_id = @RegistrationKeyId";

        var parameters = new { RegistrationKeyId = registrationKeyId };
        return await QueryFirstOrDefaultAsync(query, parameters);
    }
}
