using System.ComponentModel;
using System.Reflection;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        var field = enumValue.GetType().GetField(enumValue.ToString());
        if (field == null) return enumValue.ToString();

        var attr = field.GetCustomAttribute<DescriptionAttribute>();
        return attr?.Description ?? enumValue.ToString();
    }
}
