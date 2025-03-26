using System.Reflection;
using Dapper;

namespace Auth_Api.Extensions.Mapper;
public class SnakeCaseMemberMap : SqlMapper.IMemberMap
{
    private readonly MemberInfo? _memberInfo;
    private readonly ParameterInfo? _parameterInfo;

    public SnakeCaseMemberMap(MemberInfo member)
    {
        _memberInfo = member;
    }

    public SnakeCaseMemberMap(ParameterInfo parameter)
    {
        _parameterInfo = parameter;
    }

    public string ColumnName => _memberInfo?.Name ?? _parameterInfo?.Name ?? string.Empty;

    public Type MemberType => _memberInfo is PropertyInfo property
    ? property.PropertyType
    : _parameterInfo?.ParameterType ?? typeof(object);

    public FieldInfo? Field => _memberInfo as FieldInfo;

    public PropertyInfo? Property => _memberInfo as PropertyInfo;

    public ParameterInfo? Parameter => _parameterInfo;
}