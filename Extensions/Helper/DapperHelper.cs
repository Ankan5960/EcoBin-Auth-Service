using System.Reflection;
using Dapper;
using EcoBin_Auth_Service.Extensions.Mapper;

namespace EcoBin_Auth_Service.Extensions.Helpers;

public static class DapperHelper
{
    public static void SetSnakeCaseMapping()
    {
        foreach (var type in Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsClass && t.Namespace == "Auth_Api.Model.Entities"))
        {
            try
            {
                SqlMapper.SetTypeMap(type, new SnakeCaseMapper(type));
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to set mapper", ex);
            }
        }
    }
}
