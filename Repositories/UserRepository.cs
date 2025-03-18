using System.Data;
using Auth_Api.Model.Entities;
using Auth_Api.Repositories;
using Auth_Api.Repositories.Contracts;

namespace User_Auth_API.Repositories;
public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
{
    public UserRepository(IDbConnection dbConnection) : base(dbConnection) { }

    public async Task<Guid> AddUserAsync(UserEntity user)
    {
        var query = "INSERT INTO \"User\" (user_name, email, first_name, last_name, password_hash, is_verified, registration_key_id) " +
                    "VALUES (@Username, @Email, @Firstname, @Lastname, @PasswordHash, @IsVerified, @RegistrationKeyId) RETURNING user_id";
        var parameters = new { user.UserName, user.Email, user.FirstName, user.LastName, user.PasswordHash, user.IsVerified, user.RegistrationKeyId };
        Guid newUserId = await ExecuteScalarAsync(query, parameters);
        return newUserId;
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var query = "DELETE FROM \"User\" WHERE user_id = @Id";
        await ExecuteAsync(query, new { Id = id });
    }

    public async Task<UserEntity?> GetUserByEmailAsync(string email)
    {
        var query = "SELECT * FROM \"User\" WHERE email = @Email AND deleted_at IS NULL";

        return await QueryFirstOrDefaultAsync(query, new { Email = email });
    }

    public async Task<UserEntity?> GetUserByIdAsync(Guid id)
    {
        var query = "SELECT * FROM \"User\" WHERE user_id = @Id";
        return await QueryFirstOrDefaultAsync(query, new { Id = id });
    }

    public async Task<IEnumerable<UserEntity>> GetUsersAsync()
    {
        var query = "SELECT * FROM \"User\"";
        return await QueryAsync(query);
    }

    public async Task UpdateUserAsync(UserEntity user)
    {
        var query = "UPDATE \"User\" SET user_name = @UserName, email = @Email, first_name = @FirstName, " +
                    "last_name = @LastName, password_hash = @PasswordHash, is_verified = @IsVerified, registration_key_id = @RegistrationKeyId, updated_at = @UpdatedAt " +
                    "WHERE user_id = @UserId";
        await ExecuteAsync(query, new { user.UserName, user.Email, user.FirstName, user.LastName, user.PasswordHash, user.IsVerified, user.RegistrationKeyId, user.UserId, UpdatedAt = DateTime.Now });
    }
}
