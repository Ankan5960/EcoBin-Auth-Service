using EcoBin_Auth_Service.Model.DTOs.Response;

namespace EcoBin_Auth_Service.Services.Contracts;

public interface IMigrationService
{
    Task<MigrationResponseDto> RunMigrationAsync(string key);
}
