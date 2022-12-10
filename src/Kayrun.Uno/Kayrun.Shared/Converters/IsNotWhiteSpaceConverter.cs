// Adam Dernis 2022

namespace Kayrun.Converters
{
    public class IsNotWhiteSpaceConverter
    {
        public static bool Convert(string? item1) => !string.IsNullOrWhiteSpace(item1);
    }
}
