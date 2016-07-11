using System;

namespace PrtgSensorSharp
{
    internal static class EnumExtensions
    {
        public static string TypeName(this Enum @enum) => @enum.GetType().Name;
        public static string ValueName(this Enum @enum) => Enum.GetName(@enum.GetType(), @enum);
    }
}