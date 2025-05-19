using EcoBin_Auth_Service.Model.Entities;

namespace EcoBin_Auth_Service.Repositories.Contracts;

public interface IRegistrationKeyRepository : IRepositoryBase<RegistrationKeyEntity>
{
    Task<Guid> AddRegistrationKeyAsync(RegistrationKeyEntity registrationKey);
    Task<IEnumerable<RegistrationKeyEntity>> GetRegistrationKeysAsync();
    Task<RegistrationKeyEntity?> GetRegistrationKeyByRegistrationKeyAsync(string key);
    Task UpdateRegistrationKeyAsync(RegistrationKeyEntity registrationKey);
    Task DeleteRegistrationKeyAsync(Guid id);
}
