using Dapper;
using System.Reflection;

namespace EcoBin_Auth_Service.Extensions.Mapper;

public class SnakeCaseMapper : SqlMapper.ITypeMap
{
    private readonly PropertyInfo[] _properties;

    public SnakeCaseMapper(Type type)
    {
        _properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
    }

    public ConstructorInfo? FindConstructor(string[] names, Type[] types)
    {
        var constructors = _properties.FirstOrDefault()?.DeclaringType?.GetConstructors();
        if (constructors != null)
        {
            foreach (var ctor in constructors)
            {
                var parameters = ctor.GetParameters();
                if (parameters.Length == names.Length &&
                    parameters.Select(p => p.ParameterType).SequenceEqual(types))
                {
                    return ctor;
                }
            }
        }
        return null;
    }

    public ConstructorInfo? FindExplicitConstructor()
    {
        return _properties.FirstOrDefault()?.DeclaringType?.GetConstructors().FirstOrDefault();
    }

    public SqlMapper.IMemberMap? GetConstructorParameter(ConstructorInfo constructor, string columnName)
    {
        var parameter = constructor.GetParameters()
            .FirstOrDefault(p => string.Equals(ConvertToPascalCase(columnName), p.Name, StringComparison.OrdinalIgnoreCase));

        if (parameter != null)
        {
            return new SnakeCaseMemberMap(parameter);
        }

        return null;
    }

    public SqlMapper.IMemberMap? GetMember(string columnName)
    {
        var property = _properties.FirstOrDefault(p =>
            string.Equals(p.Name, ConvertToPascalCase(columnName), StringComparison.OrdinalIgnoreCase));

        if (property != null)
        {
            return new SnakeCaseMemberMap(property);
        }

        return null;
    }

    private static string ConvertToPascalCase(string snakeCase)
    {
        return string.Join(string.Empty, snakeCase.ToLowerInvariant().Split('_')
            .Select(word => char.ToUpperInvariant(word[0]) + word.Substring(1)));
    }
}
