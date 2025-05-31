using EcoBin_Auth_Service.Model.DTOs.Response;
using EcoBin_Auth_Service.Repositories.Contracts;
using EcoBin_Auth_Service.Services.Contracts;

namespace EcoBin_Auth_Service.Services;

public class RoleIdService : IRoleIdService
{
    private readonly IRepositoryManager _repositoryManager;

    public RoleIdService(IRepositoryManager repository)
    {
        _repositoryManager = repository;
    }

    public async Task<IEnumerable<RoleIdResponseDto>> GetRoleIdAsync()
    {
        var roleId = await _repositoryManager.RoleRepository.GetRolesRepoAsync();
        var roleIdDtos = roleId.Select(role => new RoleIdResponseDto
        {
            RoleId = role.RoleId,
            RoleName = role.RoleName
        });
        return roleIdDtos;
    }
}