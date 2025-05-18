namespace EcoBin_Auth_Service.Repositories.Contracts;

public interface IRepositoryManager
{
    IRoleRepository RoleRepository { get; }
    IUserRepository UserRepository { get; }
    IUserRoleRepository UserRoleRepository { get; }
    IRegistrationKeyRepository RegistrationKeyRepository { get; }

}