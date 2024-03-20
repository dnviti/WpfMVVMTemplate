namespace MyApp.Helpers
{
    public static class StringHelper
    {
        public static string? ToNull(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            else
                return value;
        }
    }
}
