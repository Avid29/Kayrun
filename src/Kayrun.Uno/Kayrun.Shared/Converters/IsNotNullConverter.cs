// Adam Dernis 2022

namespace Kayrun.Converters
{
    public class IsNotNullConverter
    {
        public static bool Convert(object? item1) => item1 is not null;
    }
}
