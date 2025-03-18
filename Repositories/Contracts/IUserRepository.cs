using Auth_Api.Model.Entities;

namespace Auth_Api.Repositories.Contracts;
public interface IUserRepository : IRepositoryBase<UserEntity>
{
    Task<Guid> AddUserAsync(UserEntity user);
    Task<IEnumerable<UserEntity>> GetUsersAsync();
    Task<UserEntity?> GetUserByIdAsync(Guid id);
    Task<UserEntity?> GetUserByEmailAsync(string email);
    Task UpdateUserAsync(UserEntity user);
    Task DeleteUserAsync(Guid id);
}
