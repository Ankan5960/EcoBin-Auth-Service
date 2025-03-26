using Auth_Api.Model.Entities;

namespace Auth_Api.Repositories.Contracts;

public interface IRegistrationKeyRepository : IRepositoryBase<RegistrationKeyEntity>
{
    Task<Guid> AddRegistrationKeyAsync(RegistrationKeyEntity registrationKey);
    Task<IEnumerable<RegistrationKeyEntity>> GetRegistrationKeysAsync();
    Task<RegistrationKeyEntity?> GetRegistrationKeyByRegistrationKeyAsync(string key);
    Task UpdateRegistrationKeyAsync(RegistrationKeyEntity registrationKey);
    Task DeleteRegistrationKeyAsync(Guid id);
}
