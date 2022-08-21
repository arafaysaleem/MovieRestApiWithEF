namespace MovieRestApiWithEF.Extensions
{
    public static class EnumExtensions
    {
        public static string Name(this Enum enumValue)
        {
            return enumValue.ToString("g");
        }

        public static int Value(this Enum enumValue)
        {
            return Convert.ToInt32(enumValue);
        }
    }
}
