using System.Text.RegularExpressions;

namespace Module5.Task5
{
    public static class StringExtension
    {
        private const string intNumberPattern = "^-?[0-9]+$";

        public static bool IsInt(this string str)
        {
            return Regex.IsMatch(str, intNumberPattern);
        }
    }
}
