using Auth_Api.Model.DTOs.Response;

namespace Auth_Api.Services.Contracts;

public interface IMigrationService
{
    Task<MigrationResponseDto> RunMigrationAsync(string key);
}
