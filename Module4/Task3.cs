using System.Text.RegularExpressions;

namespace Module4
{
    public static class Task3
    {
        private const string datePattern = "(:?0[1-9]|[12][0-9]|3[01])-" +
            "(?:0[0-9]|1[0-2])-" +
            "(?:[0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]|[0-9][1-9][0-9]{2}|[1-9][0-9]{3})"; 

        public static void Run()
        {
            var text = "2008 год наступит 64-01-2008 01-13-2008 01-01-2008";
            ShowInfoIfTextContainsDateString(text);
        }

        private static void ShowInfoIfTextContainsDateString(string text)
        {
            var match = Regex.Match(text, datePattern);
            if (match.Success)
            {
                Console.WriteLine("Данный текст содержит дату: {0}", match.Value);
                return;
            }
            Console.WriteLine("Данный текст не содержит дату");
        }
    }
}
