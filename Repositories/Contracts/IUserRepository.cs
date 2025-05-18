using EcoBin_Auth_Service.Model.Entities;

namespace EcoBin_Auth_Service.Repositories.Contracts;

public interface IUserRepository : IRepositoryBase<UserEntity>
{
    Task<Guid> AddUserAsync(UserEntity user);
    Task<IEnumerable<UserEntity>> GetUsersAsync();
    Task<UserEntity?> GetUserByIdAsync(Guid id);
    Task<UserEntity?> GetUserByEmailAsync(string email);
    Task UpdateUserAsync(UserEntity user);
    Task DeleteUserAsync(Guid id);
}
