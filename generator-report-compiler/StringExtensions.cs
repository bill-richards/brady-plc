namespace Brady
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhitespace(this string value) => string.IsNullOrWhiteSpace(value);
    }
}