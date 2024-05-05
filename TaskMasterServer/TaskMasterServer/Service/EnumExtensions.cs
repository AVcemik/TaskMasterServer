using System.ComponentModel;

namespace TaskMasterServer.Service
{
    /// <summary>
    /// Класс позволяющий читать атрибут Description у Enum
    /// </summary>
    internal static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = Attribute.GetCustomAttribute(field!, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
