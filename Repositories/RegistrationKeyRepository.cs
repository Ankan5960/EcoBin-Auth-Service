using System.Data;
using EcoBin_Auth_Service.Model.Entities;
using EcoBin_Auth_Service.Repositories.Contracts;

namespace EcoBin_Auth_Service.Repositories;

public class RegistrationKeyRepository : RepositoryBase<RegistrationKeyEntity>, IRegistrationKeyRepository
{
    public RegistrationKeyRepository(IDbConnection dbConnection) : base(dbConnection) { }

    public async Task<Guid> AddRegistrationKeyAsync(RegistrationKeyEntity registrationKey)
    {
        var query = @"INSERT INTO RegistrationKeys 
                    (registration_key, role_id, area_of_service, is_used, expires_at, delete_at) 
                  VALUES 
                    (@RegistrationKey, @RoleId, @AreaOfService, @IsUsed, @ExpiresAt, @DeleteAt) 
                  RETURNING key_id";

        var parameters = new
        {
            registrationKey.RegistrationKey,
            registrationKey.RoleId,
            registrationKey.AreaOfService,
            registrationKey.IsUsed,
            registrationKey.ExpiresAt,
            registrationKey.DeleteAt
        };

        Guid newKeyId = await ExecuteScalarAsync(query, parameters);
        return newKeyId;
    }

    public async Task DeleteRegistrationKeyAsync(Guid id)
    {
        var query = "DELETE FROM RegistrationKeys WHERE key_id = @Id";
        await ExecuteAsync(query, new { Id = id });
    }

    public async Task<RegistrationKeyEntity?> GetRegistrationKeyByIdAsync(Guid id)
    {
        var query = "SELECT * FROM RegistrationKeys WHERE key_id = @Id";
        return await QueryFirstOrDefaultAsync(query, new { Id = id });
    }

    public async Task<RegistrationKeyEntity?> GetRegistrationKeyByRegistrationKeyAsync(string key)
    {
        var query = @"
        SELECT * 
        FROM RegistrationKeys 
        WHERE registration_key = @Key 
          AND delete_at IS NULL";

        return await QueryFirstOrDefaultAsync(query, new { Key = key });
    }

    public async Task<IEnumerable<RegistrationKeyEntity>> GetRegistrationKeysAsync()
    {
        var query = "SELECT * FROM RegistrationKeys";
        return await QueryAsync(query);
    }

    public async Task UpdateRegistrationKeyAsync(RegistrationKeyEntity registrationKey)
    {
        var query = "UPDATE RegistrationKeys SET registration_key = @RegistrationKey, role_id = @RoleId, area_of_service = @AreaOfService, " +
                    "is_used = @IsUsed, expires_at = @ExpiresAt, delete_at = @DeleteAt WHERE key_id = @KeyId";
        await ExecuteAsync(query, new { registrationKey.RegistrationKey, registrationKey.RoleId, registrationKey.AreaOfService, registrationKey.IsUsed, registrationKey.ExpiresAt, registrationKey.DeleteAt, registrationKey.KeyId });
    }
}
